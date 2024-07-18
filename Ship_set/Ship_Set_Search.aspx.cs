using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PrivatePage_Ship_set_Ship_Set_Search : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtModel0.ReadOnly = true;
        txtEsir0.ReadOnly = true;
        txtSn0.ReadOnly = true;
        txtPickNo0.ReadOnly = true;
        txtStatus0.ReadOnly = true;
        txtSearch123.ReadOnly = false;
    }

    protected void searchbtn_Click(object sender, EventArgs e)
    {

        var search = txtSearch123.Text.ToUpper();
        String myquery = "Select * from CUST_SHIP_CONTROL_QA where SN='" + search + "'";
        OracleConnection con = new OracleConnection(@"Data Source = (DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.195.80.42) (PORT = 1521)))(CONNECT_DATA = (SID = odcse01))); User ID = hs; Password=o5hstcp;");
        OracleCommand cmd = new OracleCommand();
        cmd.CommandText = myquery;
        cmd.Connection = con;
        OracleDataAdapter da = new OracleDataAdapter();
        da.SelectCommand = cmd;
        DataSet ds = new DataSet();
        da.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            lblMessage.Text = "Record Found";
            txtModel0.Text = ds.Tables[0].Rows[0]["MODEL"].ToString();
            txtEsir0.Text = ds.Tables[0].Rows[0]["ESIR"].ToString();
            txtSn0.Text = ds.Tables[0].Rows[0]["SN"].ToString();
            txtPickNo0.Text = ds.Tables[0].Rows[0]["PICK_NO"].ToString();
            txtStatus0.Text = ds.Tables[0].Rows[0]["STATUS"].ToString();

        }
        else
        {
            lblMessage.Text = "No record found";
            txtModel0.Text = "";
            txtEsir0.Text = "";
            txtSn0.Text = "";
            txtPickNo0.Text = "";
            txtStatus0.Text = "";
        }
        con.Close();

        if (txtModel0.Text != "")
        {
            txtModel0.ReadOnly = false;
            txtEsir0.ReadOnly = false;
            txtSn0.ReadOnly = false;
            txtPickNo0.ReadOnly = false;
            txtStatus0.ReadOnly = false;
            txtSearch123.ReadOnly = true;
        }
    }

    protected void update_Click(object sender, EventArgs e)
    {
        var model = txtModel0.Text.ToUpper();
        var esir = txtEsir0.Text.ToUpper();
        var sn = txtSn0.Text.ToUpper();
        var pickno = txtPickNo0.Text.ToUpper();
        var status = txtStatus0.Text.ToUpper();
        var search = txtSearch123.Text.ToUpper();

        //String updatedata = "Update CUST_SHIP_CONTROL_QA set MODEL='" + txtModel0.Text + "', ESIR='" + txtEsir0.Text + "', SN='" + txtSn0.Text + "', PICK_NO=" + txtPickNo0.Text + ", STATUS='" + txtStatus0.Text + "' where SN='" + txtSearch123.Text + "'";
        String updatedata = "Update CUST_SHIP_CONTROL_QA set MODEL='" + model + "', ESIR='" + esir + "', SN='" + sn + "', PICK_NO=" + pickno + ", STATUS='" + status + "' where SN='" + search + "'";
        OracleConnection con = new OracleConnection(@"Data Source = (DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.195.80.42) (PORT = 1521)))(CONNECT_DATA = (SID = odcse01))); User ID = hs; Password=o5hstcp;");
        OracleCommand cmd = new OracleCommand();
        con.Open();

        if (txtSearch123.Text == "")
        {
            MsgBox("Please search for data before update");
        }
        else
        {
            if (search != sn)
            {
                String myquery123 = "Select * from CUST_SHIP_CONTROL_QA where SN='" + sn + "'";
                cmd.CommandText = myquery123;
                cmd.Connection = con;
                OracleDataAdapter da = new OracleDataAdapter();
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    MsgBox("The Serial Number is existed, Try Again");
                    txtModel0.Text = "";
                    txtEsir0.Text = "";
                    txtSn0.Text = "";
                    txtPickNo0.Text = "";
                    txtStatus0.Text = "";
                }
                else
                {
                    cmd.CommandText = updatedata;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    MsgBox("Data Has Been Update");
                    txtModel0.Text = "";
                    txtEsir0.Text = "";
                    txtSn0.Text = "";
                    txtPickNo0.Text = "";
                    txtStatus0.Text = "";
                    txtSearch123.Text = "";
                    lblMessage.Text = "";
                }
                con.Close();
            }
            else
            {
                cmd.CommandText = updatedata;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                MsgBox("Data Has Been Update");
                txtModel0.Text = "";
                txtEsir0.Text = "";
                txtSn0.Text = "";
                txtPickNo0.Text = "";
                txtStatus0.Text = "";
                txtSearch123.Text = "";
                lblMessage.Text = "";
            }

        }
    }

    protected void delete_Click(object sender, EventArgs e)
    {
        var search = txtSearch123.Text.ToUpper();
        String deletedata = "delete From CUST_SHIP_CONTROL_QA where SN='" + search + "'";
        OracleConnection con = new OracleConnection(@"Data Source = (DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.195.80.42) (PORT = 1521)))(CONNECT_DATA = (SID = odcse01))); User ID = hs; Password=o5hstcp;");

        if (txtSearch123.Text == "")
        {
            MsgBox("Please search for data before delete");
        }
        else
        {
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = deletedata;
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            MsgBox("Data Has Been Deleted");
            txtSearch123.Text = "";
            txtModel0.Text = "";
            txtEsir0.Text = "";
            txtSn0.Text = "";
            txtPickNo0.Text = "";
            txtStatus0.Text = "";
            lblMessage.Text = "";
        }
    }

    protected void reset_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        txtModel0.Text = "";
        txtEsir0.Text = "";
        txtSn0.Text = "";
        txtPickNo0.Text = "";
        txtStatus0.Text = "";
        txtSearch123.Text = "";
    }

    private void MsgBox(string v)
    {
        string msg = null;
        msg = "<script language='javascript'>";
        msg += "alert('" + v + "');";
        msg += "<" + "/script>";
        Response.Write(msg);
    }

    protected void clearbtn_Click(object sender, EventArgs e)
    {
        txtSearch123.Text = "";
        lblMessage.Text = "";
    }
}