using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class PublicPage_Covid19_Setting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
            Display_Value();
    }

    protected void Display_Value()
    {
        TextBox1.Text = new dbRead(ConString.csGoodrich).oneData("select value from CUST_TB_MCO_SETTING where setting_name = 'MIN_TEMP'");
        TextBox2.Text = new dbRead(ConString.csGoodrich).oneData("select value from CUST_TB_MCO_SETTING where setting_name = 'MAX_TEMP'");
        TextArea1.Value = new dbRead(ConString.csGoodrich).oneData("select value from CUST_TB_MCO_SETTING where setting_name = 'MSG_DSP'");

        TextBox3.Text = new dbRead(ConString.csGoodrich).oneData("select value from CUST_TB_MCO_SETTING where setting_name = 'MAX_EMP_MORNING'");
        TextBox6.Text = new dbRead(ConString.csGoodrich).oneData("select value from CUST_TB_MCO_SETTING where setting_name = 'MAX_EMP_NIGHT'");
        TextBox4.Text = new dbRead(ConString.csGoodrich).oneData("select value from CUST_TB_MCO_SETTING where setting_name = 'MAX_DAY'");
        TextBox5.Text = new dbRead(ConString.csGoodrich).oneData("select value from CUST_TB_MCO_SETTING where setting_name = 'MAX_NIGHT'");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        new dbWrite(ConString.csGoodrich).dbNonQuery(string.Format("update CUST_TB_MCO_SETTING set value = '{0}' where setting_name = 'MIN_TEMP'", TextBox1.Text));
        new dbWrite(ConString.csGoodrich).dbNonQuery(string.Format("update CUST_TB_MCO_SETTING set value = '{0}' where setting_name = 'MAX_TEMP'", TextBox2.Text));
        new dbWrite(ConString.csGoodrich).dbNonQuery(string.Format("update CUST_TB_MCO_SETTING set value = '{0}' where setting_name = 'MSG_DSP'", TextArea1.Value));

        new dbWrite(ConString.csGoodrich).dbNonQuery(string.Format("update CUST_TB_MCO_SETTING set value = '{0}' where setting_name = 'MAX_EMP_MORNING'", TextBox3.Text));
        new dbWrite(ConString.csGoodrich).dbNonQuery(string.Format("update CUST_TB_MCO_SETTING set value = '{0}' where setting_name = 'MAX_EMP_NIGHT'", TextBox6.Text));
        new dbWrite(ConString.csGoodrich).dbNonQuery(string.Format("update CUST_TB_MCO_SETTING set value = '{0}' where setting_name = 'MAX_DAY'", TextBox4.Text));
        new dbWrite(ConString.csGoodrich).dbNonQuery(string.Format("update CUST_TB_MCO_SETTING set value = '{0}' where setting_name = 'MAX_NIGHT'", TextBox5.Text));

        var max_morning = Math.Round((double.Parse(TextBox4.Text.ToString()) / 100) * double.Parse(TextBox3.Text.ToString()));
        var max_night = Math.Round((double.Parse(TextBox5.Text.ToString()) / 100) * double.Parse(TextBox6.Text.ToString()));

        new dbWrite(ConString.csGoodrich).dbNonQuery(string.Format("update CUST_TB_MCO_SETTING set value = '{0}' where setting_name = 'NO_DAY'", max_morning));
        new dbWrite(ConString.csGoodrich).dbNonQuery(string.Format("update CUST_TB_MCO_SETTING set value = '{0}' where setting_name = 'NO_NIGHT'", max_night));

        Display_Value();
    }

    protected void UploadButton_Click(object sender, EventArgs e)
    {
        uploadModule("myFile2");
    }

    protected void uploadModule(string savefilename)
    {
        if (FileUploadControl.HasFile)
        {
            try
            {
                string value = System.IO.Path.GetFileName(FileUploadControl.FileName);

                var a = ".";
                var filetype = "";
                int posA = value.LastIndexOf(a);
                if (posA == -1)
                {
                    filetype = "";
                }
                int adjustedPosA = posA + a.Length;
                if (adjustedPosA >= value.Length)
                {
                    filetype = "";
                }
                filetype = value.Substring(adjustedPosA);

                if (filetype.ToUpper() == "CSV")
                {
                    FileUploadControl.SaveAs(Server.MapPath("~/") + savefilename + ".csv");
                    StatusLabel.Text = "Upload status: File uploaded!";
                    readCSV(savefilename + ".csv");
                }
                else
                {
                    StatusLabel.Text = "Wrong File type, please make sure in CSV format";
                }

            }
            catch (Exception ex)
            {
                StatusLabel.Text = "Catch an Error while uploading: " + ex.Message;
            }
        }
    }
    public void readCSV(string filename)
    {
        var tableName = "CUST_TB_MCO_SELFTEST";
        var cs = ConString.csGoodrich;

        new dbWrite(cs).dbNonQuery(string.Format(@"delete {0}", tableName));

        var ExePath = AppDomain.CurrentDomain.BaseDirectory;
        var Path = ExePath + filename;

        var file = new StreamReader(Path);
        var line = file.ReadLine();
        line = file.ReadLine();//skip header
        try
        {
            while (line != null)
            {
                var datetime = line.Split(',')[0].Trim();
                var user_id = line.Split(',')[2].Trim();
                var phone_no = line.Split(',')[3].Trim();
                var kit_sn = line.Split(',')[4].Trim();
                var result = line.Split(',')[5].Trim();

                if (user_id.Length < 8)
                    user_id = "000" + user_id;

                new dbWrite(cs).dbNonQuery(string.Format(@"insert into {0} (datetime,user_id,phone_no,kit_sn,result) values (to_date('{1}','MM/DD/YYYY HH24:MI:SS'),'{2}','{3}','{4}','{5}')", tableName, datetime, user_id, phone_no, kit_sn, result));

                line = file.ReadLine();
            }
            file.Close();
        }
        catch
        {
            file.Close();
            Label2.Text = line;
        }
    }
}