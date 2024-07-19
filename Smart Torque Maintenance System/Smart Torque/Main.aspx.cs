using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Data;
using System.Windows.Forms;

namespace Smart_Torque
{
    public partial class Main : System.Web.UI.Page
    {
        string ACTCustomer, DBID, Pass, DBhost, SID;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["user"] == null)
            {
                Session["forcelog"] = "true";
                Response.Redirect("Login.aspx");
            }
            //retrieve DB data from Login page
            if (Session["Info1"] != null && Session["Info2"] != null && Session["Info3"] != null && Session["Info4"] != null && Session["Info5"] != null)
            {
                ACTCustomer = (string)Session["Info1"];
                DBID = (string)Session["Info2"];
                Pass = (string)Session["Info3"];
                DBhost = (string)Session["Info4"];
                SID = (string)Session["Info5"];
                Info.Text = "Current Active Customer : " + ACTCustomer;
            }

            Notice.Text = "*Please only use the logout button to return to login page";

        } 
        public bool Check()
        {
            Session["EntryMod"] = MODEL.Text.ToUpper();
            Session["EntryPro"] = PROGRAMID.Text.ToUpper();
            Session["EntrySta"] = STATION.Text.ToUpper();
            string Connection = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=    (PROTOCOL=TCP)(HOST=" + DBhost + ")(PORT=1521)))(CONNECT_DATA=(SID = " + SID + ")));User ID=" + DBID + ";Password=" + Pass + ";";
            OracleConnection conn = new OracleConnection(Connection);
            conn.Open();

            OracleCommand entrycheck = new OracleCommand("SELECT * FROM CUST_TB_SMART_TORQUE WHERE MODEL = :Model AND PROGRAMID = :ProgramID AND STATION = :Station");
            entrycheck.Connection = conn;
            entrycheck.Parameters.Add("Model", OracleType.VarChar).Value = (string)Session["EntryMod"];
            entrycheck.Parameters.Add("ProgramID", OracleType.VarChar).Value = (string)Session["EntryPro"];
            entrycheck.Parameters.Add("Station", OracleType.VarChar).Value = (string)Session["EntrySta"];
            OracleDataReader Isexist = entrycheck.ExecuteReader();

            if (Isexist.HasRows)
            {
                conn.Close();
                return true;
            }
            else
            {
                conn.Close();
                return false;
            }
        }
        protected void CheEntryCheck(object sender, EventArgs e)
        {
            if (MODEL.Text != "" && PROGRAMID.Text != "" && STATION.Text != "")
            {
                if (Check())
                {
                    Response.Write("<script>alert('Error! Data already exists in the database!')</script>");
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "", "CreConfirm();", true);
                }
            }
            else
            {
                Response.Write("<script>alert('Please make sure Model, ProgramID and Station are filled in!')</script>");
            }
        }
        protected void DelEntryCheck(object sender, EventArgs e)
        {
            if (MODEL.Text != "" && PROGRAMID.Text != "" && STATION.Text != "")
            {
                if (!Check())
                {
                    Response.Write("<script>alert('Error! Data does not exist in the database!')</script>");
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "", "DelConfirm();", true);
                }
            }
            else
            {
                Response.Write("<script>alert('Please make sure Model, ProgramID and Station are filled in!')</script>");
            }
        }
        //Adds the entry into the table
        public void Insert(object sender, EventArgs e)
        {
            string Connection = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=    (PROTOCOL=TCP)(HOST=" + DBhost + ")(PORT=1521)))(CONNECT_DATA=(SID = " + SID + ")));User ID=" + DBID + ";Password=" + Pass + ";";
            OracleConnection conn = new OracleConnection(Connection);
            conn.Open();

            OracleCommand Cre = new OracleCommand("INSERT INTO CUST_TB_SMART_TORQUE (MODEL, PROGRAMID, STATION) VALUES (:Model, :ProgramID, :Station)");
            Cre.Connection = conn;
            Cre.Parameters.Add("Model", OracleType.VarChar).Value = Session["EntryMod"];
            Cre.Parameters.Add("ProgramID", OracleType.VarChar).Value = Session["EntryPro"];
            Cre.Parameters.Add("Station", OracleType.VarChar).Value = Session["EntrySta"];

            Cre.ExecuteNonQuery();

            Response.Write("<script>alert('Entry Created!')</script>");
            Clear();
            conn.Close();
        }
        //Removes the entry from the table
        public void Remove(object sender, EventArgs e)
        {
            string Connection = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=    (PROTOCOL=TCP)(HOST=" + DBhost + ")(PORT=1521)))(CONNECT_DATA=(SID = " + SID + ")));User ID=" + DBID + ";Password=" + Pass + ";";
            OracleConnection conn = new OracleConnection(Connection);
            conn.Open();

            OracleCommand Del = new OracleCommand("DELETE FROM CUST_TB_SMART_TORQUE WHERE MODEL = :Model AND PROGRAMID = :ProgramID AND STATION = :Station");
            Del.Connection = conn;
            Del.Parameters.Add("Model", OracleType.VarChar).Value = Session["EntryMod"];
            Del.Parameters.Add("ProgramID", OracleType.VarChar).Value = Session["EntryPro"];
            Del.Parameters.Add("Station", OracleType.VarChar).Value = Session["EntrySta"];

            Del.ExecuteNonQuery();

            Response.Write("<script>alert('Entry Deleted!')</script>");
            Clear();
            conn.Close();
        }
        //Returns to the Login Page
        public void LogoutBtn(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            System.Web.Security.FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
        }
        public void Clear()
        {
            MODEL.Text = "";
            PROGRAMID.Text = "";
            STATION.Text = "";
        }

    }

}