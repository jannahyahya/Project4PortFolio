using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PrivatePage_Ship_set_Ship_Set_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        var day = DateTime.Now.Day.ToString();
        var month = DateTime.Now.Month.ToString();
        var year = DateTime.Now.Year.ToString();
        var hour = DateTime.Now.Hour.ToString();
        var minute = DateTime.Now.Minute.ToString();

        var ExePath = AppDomain.CurrentDomain.BaseDirectory;
        //var Path = ExePath + "sqlqueryZodiac.txt";
        String sqlfromtext = "Select * from CUST_SHIP_CONTROL_QA";

        var dateNow = day + month + year + hour + minute;
        int cols;
        //open file 
        //var ExePath = AppDomain.CurrentDomain.BaseDirectory;
        var filename = ExePath + "PrivatePage\\Ship_set\\Report\\" + dateNow + ".csv";
        StreamWriter wr = new StreamWriter(filename);

        var cs = @"Data Source = (DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.195.80.42) (PORT = 1521)))(CONNECT_DATA = (SID = odcse01))); User ID = hs; Password=o5hstcp;";
        var con = new Oracle.ManagedDataAccess.Client.OracleConnection(cs);
        con.Open();
        var cmd = con.CreateCommand();

        cmd.CommandText = sqlfromtext;
        //cursor
        var dr = cmd.ExecuteReader();   //execute the command can capture query result

        for (int i = 0; i < dr.FieldCount; i++)
        {
            wr.Write(dr.GetName(i) + ",");
        }
        wr.WriteLine();

        while (dr.Read())                //loop until read() is false
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                wr.Write(dr.GetValue(i).ToString() + ",");
            }
            wr.WriteLine();
        }
        //close the cursor an connection
        wr.Close();
        dr.Close();
        con.Close();

        HyperLink1.Text = dateNow.ToString();
        HyperLink1.NavigateUrl = "Report/" + dateNow.ToString() + ".csv";
        HyperLink1.Visible = true;
    }
}