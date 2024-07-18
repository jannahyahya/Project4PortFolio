using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PrivatePage_Ship_set_Ship_Set_Add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    protected void Reset_Click(object sender, EventArgs e)
    {
        txtModel.Text = "";
        txtEsir.Text = "";
        txtSn.Text = "";
        txtPickNo.Text = "";
        txtStatus.Text = "";
    }
    protected void Submit_Click(object sender, EventArgs e)
    {
        if (txtModel.Text == "")
        {
            MsgBox("Please Enter The Model");
        }
        else if (txtEsir.Text == "")
        {
            MsgBox("Please Enter The ESIR");
        }
        else if (txtSn.Text == "")
        {
            MsgBox("Please Enter The Serial Number");
        }
        else if (txtPickNo.Text == "")
        {
            MsgBox("Please Enter The Pick Number");
        }
        else if (txtStatus.Text == "")
        {
            MsgBox("Please Enter The Status");
        }
        else
        {
            var model = txtModel.Text.ToUpper();
            var esir = txtEsir.Text.ToUpper();
            var sn = txtSn.Text.ToUpper();
            var pickno = txtPickNo.Text.ToUpper();
            var status = txtStatus.Text.ToUpper();

            //String query = "insert into CUST_SHIP_CONTROL_QA(MODEL,ESIR,SN,PICK_NO,STATUS) values('" + txtModel.Text + "','" + txtEsir.Text + "','" + txtSn.Text + "'," + txtPickNo.Text + ",'" + txtStatus.Text + "')";
            String query = "insert into CUST_SHIP_CONTROL_QA(MODEL,ESIR,SN,PICK_NO,STATUS) values('" + model + "','" + esir + "','" + sn + "'," + pickno + ",'" + status + "')";

            OracleConnection con = new OracleConnection(@"Data Source = (DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.195.80.42) (PORT = 1521)))(CONNECT_DATA = (SID = odcse01))); User ID = hs; Password=o5hstcp;");
            con.Open();
            OracleCommand cmd = new OracleCommand();


            String myquery = "Select * from CUST_SHIP_CONTROL_QA where SN='" + sn + "'";
            cmd.CommandText = myquery;
            cmd.Connection = con;
            OracleDataAdapter da = new OracleDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                MsgBox("This Serial Number is Taken");
                txtModel.Text = "";
                txtEsir.Text = "";
                txtSn.Text = "";
                txtPickNo.Text = "";
                txtStatus.Text = "";

            }
            else
            {
                cmd.CommandText = query;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                con.Close();
                MsgBox("Item Successfully Stored!");
                txtModel.Text = "";
                txtEsir.Text = "";
                txtSn.Text = "";
                txtPickNo.Text = "";
                txtStatus.Text = "";
                
            }
            con.Close();
        }

    }

    protected void ClearAll()
    {
        txtModel.Text = "";
        txtEsir.Text = "";
        txtSn.Text = "";
        txtPickNo.Text = "";
        txtStatus.Text = "";
    }

    private void MsgBox(string v)
    {
        string msg = null;
        msg = "<script language='javascript'>";
        msg += "alert('" + v + "');";
        msg += "<" + "/script>";
        Response.Write(msg);
    }
}