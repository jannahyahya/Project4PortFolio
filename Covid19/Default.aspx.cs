using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PublicPage_Covid19_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        fillDetails();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {

        reg_temp();
    }

    [WebMethod]
    public static string checkTemp()
    {
        JObject json = new dbRead(ConString.csGoodrich).getTempRange();

        //double minVal = (double)json.GetValue("MIN_TEMP");
        //double maxVal = (double)json.GetValue("MAX_TEMP");
        //string msg = json.GetValue("MSG_DSP").ToString();

        return json.ToString();
    }

    [WebMethod]
    public static string submit(string id, string temp, string remark)
    {
        var msg = new dbRead(ConString.csGoodrich).oneData("select value from CUST_TB_MCO_SETTING where setting_name = 'MSG_DSP'");
        new dbWrite(ConString.csGoodrich).dbNonQuery(string.Format("insert into CUST_TB_MCO_ENTRY_LOG (user_id,temperature,remark,datetime,message) values ('{0}','{1}','{2}',sysdate,'{3}')", id, temp, remark, msg));

        JObject json = new JObject();

        json.Add("result", "OK");
        return json.ToString();
    }

    protected void reg_temp()
    {
        if (TextBox1.Text != "")
        {
            new dbWrite(ConString.csGoodrich).dbNonQuery(string.Format("insert into CUST_TB_MCO_ENTRY_LOG (user_id,temperature,remark,datetime) values ('{0}','{1}','{2}',sysdate)", TextBox1.Text, TextBox2.Text, "YES"));
            //clearAll();
        }
    }
    protected void fillDetails()
    {
        Label1.Visible = false;
        Label2.Visible = false;
        Label3.Visible = false;

        AlertRed.Visible = false;
        AlertRed2.Visible = false;
        AlertRed3.Visible = false;
        AlertRed4.Visible = false;
        AlertGreen.Visible = false;

        TextBox1.CssClass = "form-control";
        TextBox2.CssClass = "form-control";

        var hid_userid = "";
        if (new dbRead(ConString.csNewBadge).hasRowChecker(string.Format("select * from employee where asset = '{0}'", TextBox1.Text)))
        {
            hid_userid = new dbRead(ConString.csNewBadge).oneData(string.Format("select empno from employee where asset = '{0}'", TextBox1.Text));
            if (hid_userid.Length < 8)
                hid_userid = "000" + hid_userid;
            TextBox1.Text = hid_userid;
        }

        var empno = TextBox1.Text;
        if (empno.Length < 8)
            empno = "000" + empno;
        TextBox1.Text = empno;


        var ds = new DataSet();

        if (new dbRead(ConString.csCentre).hasRowChecker(string.Format("select * from CENTER_HRDW_V_ALL where employee_empno_full = '{0}'", empno)))
        {
            //if (new dbRead(ConString.csGoodrich).hasRowChecker(string.Format("select * from CUST_TB_MCO_SELFTEST where  datetime > (sysdate-14) and user_id = '{0}'", empno)))
            //{
            //    //Label3.Visible = true;
            //    AlertGreen.Visible = true;
            //    TextBox1.CssClass = "form-control border-success";
            //}
            //else
            //{
            //    //Label2.Visible = true;
            //    AlertRed.Visible = true;
            //    AlertRed2.Visible = true;
            //    AlertRed3.Visible = true;
            //    AlertRed4.Visible = true;
            //    TextBox1.CssClass = "form-control border-danger";
            //    TextBox2.CssClass = "form-control border-danger";
            //}

            new dbRead(ConString.csCentre).dynamicDataSet(ds, string.Format("select a.employee_empno_full as empno,a.ee_fullname as name,a.email,(select ee_fullname from CENTER_HRDW_V_ALL where CLS_UNIQUE_KEY = a.MGR_CLS_UNIQUE_KEY) as mgr_name,(select ee_fullname from CENTER_HRDW_V_ALL where CLS_UNIQUE_KEY = a.SUP_CLS_UNIQUE_KEY) as sv_name from CENTER_HRDW_V_ALL a where employee_empno_full = '{0}'", empno));

            var name = ds.Tables[0].Rows[0]["NAME"].ToString();
            var email = ds.Tables[0].Rows[0]["EMAIL"].ToString();
            var manager = ds.Tables[0].Rows[0]["MGR_NAME"].ToString();
            var supervisor = ds.Tables[0].Rows[0]["SV_NAME"].ToString();

            TextBox4.Text = name;
            TextBox5.Text = email;
            TextBox7.Text = manager;
            TextBox6.Text = supervisor;

            
            TextBox2.Text = "";
            TextBox2.Focus();


        }
        else
        {
            Label1.Visible = true;
            clearAll();
        }
    }

    protected void clearAll()
    {
        TextBox1.Text = "";
        TextBox2.Text = "";

        TextBox4.Text = "";
        TextBox5.Text = "";
        TextBox6.Text = "";
        TextBox7.Text = "";

        TextBox1.Focus();
    }

    public string msg_dsp
    {
        get
        {
            var msg = new dbRead(ConString.csGoodrich).oneData("select value from CUST_TB_MCO_SETTING where setting_name = 'MSG_DSP'");

            //DateTime now = DateTime.Now;
            return msg.ToString();
        }
    }
}