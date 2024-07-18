using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PublicPage_Emp_Info : System.Web.UI.Page
{
    DataSet dsDate = new DataSet();
    Boolean dateDSexist = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (TextBox1.Text != "")
        {
            Image1.ImageUrl = "../../Image/" + TextBox1.Text + ".jpg";
            Image1.Width = 260;
            Image1.Height = 310;
        }
        else
        {
            Image1.ImageUrl = "../../Image/noname.jpg";
            Image1.Width = 260;
            Image1.Height = 310;
        }

        TextBox1.Focus();
        cert.Visible = false;
        grape.Visible = false;


        if (TextBox1.Text != "")
        {
            fill_calendar();
        }
    }

    protected void fill_calendar()
    {
        var empno = TextBox1.Text.Trim();
        if (empno.Length < 8)
            empno = "000" + empno;

        DropDownList1.Items.Clear();

        if (DropDownList1.Items.Count < 1)
            new dbRead(ConString.csHoney).fillDropdown(DropDownList1, string.Format("select distinct(resource_name) from (select c.sn,c.so,c.station,out_date_time,user_id,resource_name from (select * from (select a.sn,a.so,a.defect_code,b.station,b.resource_name from (select * from defect where datetime > sysdate - 90) a left join(select defect_code,resource_name,x.station from CUST_TB_GRAPE_DEFECT_STATION z left join station_resource x on z.station =  x.resource_name) b on a.defect_code = b.defect_code) where station is not null)c left join(select sn,so,station,user_id,out_date_time from tracking) d on c.sn = d.sn and c.so = d.so and c.station = d.station where user_id is not null) where user_id = '{0}'", empno));

        var process = DropDownList1.SelectedValue;
        new dbRead(ConString.csHoney).dynamicDataSet(dsDate, string.Format("select distinct(to_char(out_date_time,'dd-MM-yyyy')) as datetime from (select c.sn,c.so,c.station,out_date_time,user_id,resource_name from (select * from (select a.sn,a.so,a.defect_code,b.station,b.resource_name from (select * from defect where datetime > sysdate - 90) a left join(select defect_code,resource_name,x.station from CUST_TB_GRAPE_DEFECT_STATION z left join station_resource x on z.station =  x.resource_name) b on a.defect_code = b.defect_code) where station is not null)c left join(select sn,so,station,user_id,out_date_time from tracking) d on c.sn = d.sn and c.so = d.so and c.station = d.station where user_id is not null) where user_id = '{0}' and resource_name = '{1}'", empno, process));
        dateDSexist = true;

        var ds = new DataSet();
        new dbRead(ConString.csHoney).dynamicDataSet(ds, string.Format("select sn,so,station,to_char(out_date_time,'dd-MM-yyyy') as datetime from (select c.sn,c.so,c.station,out_date_time,user_id,resource_name from (select * from (select a.sn,a.so,a.defect_code,b.station,b.resource_name from (select * from defect where datetime > sysdate - 90) a left join(select defect_code,resource_name,x.station from CUST_TB_GRAPE_DEFECT_STATION z left join station_resource x on z.station =  x.resource_name) b on a.defect_code = b.defect_code) where station is not null)c left join(select sn,so,station,user_id,out_date_time from tracking) d on c.sn = d.sn and c.so = d.so and c.station = d.station where user_id is not null) where user_id = '{0}' and resource_name = '{1}'", empno, process));

        GridView1.DataSource = ds;
        GridView1.DataBind();

        var ds1 = new DataSet();
        new dbRead(ConString.csHoney).dynamicDataSet(ds1, string.Format("select SUBSTR(process, '4') as process,datetime,count(*) as defect  from (select resource_name as process,to_char(out_date_time,'MON-yyyy') as datetime from (select c.sn,c.so,c.station,out_date_time,user_id,resource_name from (select * from (select a.sn,a.so,a.defect_code,b.station,b.resource_name from (select * from defect where datetime > sysdate - 90) a left join(select defect_code,resource_name,x.station from CUST_TB_GRAPE_DEFECT_STATION z left join station_resource x on z.station =  x.resource_name) b on a.defect_code = b.defect_code) where station is not null)c left join(select sn,so,station,user_id,out_date_time from tracking) d on c.sn = d.sn and c.so = d.so and c.station = d.station where user_id is not null) where user_id = '{0}' )group by process,datetime order by process,datetime", empno));

        GridView2.DataSource = ds1;
        GridView2.DataBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        var empid = TextBox1.Text;
        if (!(new dbRead(ConString.csBadge).hasRowChecker(string.Format(@"select * from photo_new where empno = '{0}'", empid))))
        {
            //INSERT INTO photo_new SELECT EMPNO, TO_LOB(PHOTO),OLDEMPNO,NEWPNO FROM photo where  empno = '84450';
            new dbWrite(ConString.csBadge).dbNonQuery(string.Format(@"INSERT INTO photo_new SELECT EMPNO, TO_LOB(PHOTO),OLDEMPNO,NEWPNO FROM photo where  empno = '{0}'", empid));


            string sql = string.Format(@"select * from photo_new where empno = '{0}'", empid);
            OracleConnection con = new OracleConnection();
            con.ConnectionString = ConString.csBadge;
            con.Open();
            FileStream file;
            BinaryWriter bw;
            int buffersize = 100;
            byte[] outbyte = new byte[buffersize];
            long retval;
            long startindex = 0;
            var ExePath = AppDomain.CurrentDomain.BaseDirectory;
            OracleCommand cmd = new OracleCommand(sql, con);
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.SequentialAccess);
            while (dr.Read())
            {
                int count = 0;
                string val = dr.GetString(0);
                //name_textBox.Text = dr.GetString(1);
                var savedimagename = ExePath + "Image\\" + val + "" + ".jpg";
                count++;
                file = new FileStream(savedimagename, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                bw = new BinaryWriter(file);
                startindex = 0;
                retval = dr.GetBytes(1, startindex, outbyte, 0, buffersize);
                while (retval == buffersize)
                {
                    bw.Write(outbyte);
                    bw.Flush();
                    startindex += buffersize;
                    retval = dr.GetBytes(1, startindex, outbyte, 0, buffersize);
                }

                try
                {
                    bw.Write(outbyte, 0, (int)retval - 1);
                }
                catch (Exception ex)
                {

                }

                bw.Flush();
                bw.Close();
                file.Close();
            }
            con.Close();
            //curimage = Image.FromFile(savedimagename);
            //pictureBox1.Image = curimage;
            //pictureBox1.Invalidate();
            file = null;
            con.Close();
        }
        fillDetails();
        fillTable();
        certView();

    }

    public string certViewBtn
    {
        get
        {
            if (cert.Visible == true)
                return "btn-basic";
            else
                return "btn-info";
        }
    }
    public string grapeViewBtn
    {
        get
        {
            if (grape.Visible == true)
                return "class='btn btn-basic btn-block'";
            else
                return "class='btn btn-info btn-block'";
        }
    }

    protected void certView()
    {
        cert.Visible = true;
        grape.Visible = false;
    }
    protected void grapeView()
    {
        cert.Visible = false;
        grape.Visible = true;
        Calendar1.Focus();
    }


    protected void fillDetails()
    {
        var empno = TextBox1.Text;
        if (empno.Length < 8)
            empno = "000" + empno;

        var ds = new DataSet();

        if (new dbRead(ConString.csCentre).hasRowChecker(string.Format("select * from CENTER_HRDW_V_ALL where employee_empno_full = '{0}'", empno)))
        {
            Label1.Visible = false;
            new dbRead(ConString.csCentre).dynamicDataSet(ds, string.Format("select a.employee_empno_full as empno,a.ee_fullname as name,a.email,(select ee_fullname from CENTER_HRDW_V_ALL where CLS_UNIQUE_KEY = a.MGR_CLS_UNIQUE_KEY) as mgr_name,(select ee_fullname from CENTER_HRDW_V_ALL where CLS_UNIQUE_KEY = a.SUP_CLS_UNIQUE_KEY) as sv_name from CENTER_HRDW_V_ALL a where employee_empno_full = '{0}'", empno));

            var name = ds.Tables[0].Rows[0]["NAME"].ToString();
            var email = ds.Tables[0].Rows[0]["EMAIL"].ToString();
            var manager = ds.Tables[0].Rows[0]["MGR_NAME"].ToString();
            var supervisor = ds.Tables[0].Rows[0]["SV_NAME"].ToString();

            TextBox3.Text = name;
            TextBox2.Text = email;
            TextBox5.Text = manager;
            TextBox4.Text = supervisor;
        }
        else
        {
            Label1.Visible = true;
            clearAll();
        }
    }

    protected void fillTable()
    {
        var empno = TextBox1.Text;
        if (empno.Length < 8)
            empno = "000" + empno;

        if (ViewState["DataRecord"] == null)
        {
            var ds = new DataSet();
            new dbRead(ConString.csCentre).dynamicDataSet(ds, string.Format("select TRAIN_REQUIREMENT_NAME as \"Certificate Name\",TRAIN_CERT_COMPLETE_DATE as \"Certificate Complete\",to_char(TRAIN_CERT_EXPIRATION_DATE,'dd/mm/yyyy') as \"Certificate Expiry\" from TRAINING_CERT_CURRENT_ACTIVE where train_cert_expiration_date is not null and EMPLOYEE_NUM = '{0}'", empno));

            //dataTable.HeaderRow.TableSection = TableRowSection.TableHeader;
            dataTable.DataSource = ds;
            dataTable.DataBind();

            if (dataTable.Rows.Count > 0)
            {
                //Adds THEAD and TBODY Section.
                dataTable.HeaderRow.TableSection = TableRowSection.TableHeader;

                //Adds TH element in Header Row.  
                dataTable.UseAccessibleHeader = true;

                //Adds TFOOT section. 
                dataTable.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }
    }

    protected void clearAll()
    {
        TextBox1.Text = "";
        TextBox2.Text = "";
        TextBox3.Text = "";
        TextBox4.Text = "";
        TextBox5.Text = "";

        Image1.ImageUrl = "../../Image/noname.jpg";
        Image1.Width = 230;
        Image1.Height = 278;
    }

    protected void dataTable_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        var dbDate = new dbRead(ConString.csCentre).oneData("select to_char(sysdate,'dd/MM/yyyy') from dual");
        var todayDate = DateTime.ParseExact(dbDate, "dd/MM/yyyy", null);

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (DateTime.ParseExact(e.Row.Cells[2].Text, "dd/MM/yyyy", null) < todayDate)
            {
                e.Row.Cells[2].BackColor = Color.LightPink;
            }
        }
    }

    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {
        if (dateDSexist)
        {
            var i = 0;
            foreach (var row in dsDate.Tables[0].Rows)
            {
                var faildate = dsDate.Tables[0].Rows[i]["DATETIME"].ToString();
                if (e.Day.Date.Equals(DateTime.ParseExact(faildate, "dd-MM-yyyy", null)))
                {
                    e.Cell.BackColor = System.Drawing.Color.Red;
                }
                i++;
            }
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        certView();
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text != "")
        {
            fill_calendar();
        }
        grapeView();
    }

    protected void Calendar1_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
    {
        grapeView();
    }
}