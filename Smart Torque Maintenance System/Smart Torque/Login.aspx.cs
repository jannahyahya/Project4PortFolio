using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Data;
using System.DirectoryServices;

namespace Smart_Torque
{
    //notes : current Default DB connection info is connected from BU.xml
    // Each entry data is saved in CUST_TB_SMART_TORQUE, so this table must be present in each Customer DB connected
    public partial class Login : System.Web.UI.Page
    {
        DataSet ds = new DataSet();
        string Datainfo1, Datainfo2, Datainfo3, Datainfo4, Datainfo5;
        protected void Page_Load(object sender, EventArgs e)
        {
            ds.ReadXml(Server.MapPath("~/BU.xml"));
            
            if ((string)Session["forcelog"] == "true")
            {
                Label5.Text = "Please login!";
            }

            if (Session["user"] != null)
            {
                Response.Redirect("Main.aspx");                
            }
                if (!Page.IsPostBack)
            {
                //Displays the available Customers to User in form of DropDownList
                Customer.DataSource = ds;
                Customer.DataTextField = "CUSTOMER";
                Customer.DataValueField = "CUSTOMER";
                Customer.DataBind();
            }
        }
        //To access the Smart Torque Entry Page
        protected void ConnectDB(object sender, EventArgs e)
        {
            //Saves Session Data for DB connections later
            int Index = Customer.SelectedIndex;
            
            Datainfo1 = ds.Tables[0].Rows[Index]["CUSTOMER"].ToString();
            Datainfo2 = ds.Tables[0].Rows[Index]["DB_ID"].ToString();
            Datainfo3 = ds.Tables[0].Rows[Index]["PASSWORD"].ToString();
            Datainfo4 = ds.Tables[0].Rows[Index]["DB_HOSTNAME"].ToString();
            Datainfo5 = ds.Tables[0].Rows[Index]["SID"].ToString();

            Session["Info1"] = Datainfo1;
            Session["Info2"] = Datainfo2;
            Session["Info3"] = Datainfo3;
            Session["Info4"] = Datainfo4;
            Session["Info5"] = Datainfo5;
            string Connection = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=    (PROTOCOL=TCP)(HOST=" + Datainfo4 + ")(PORT=1521)))(CONNECT_DATA=(SID = " + Datainfo5 + ")));User ID=" + Datainfo2 + ";Password=" + Datainfo3 + ";";
            OracleConnection conn = new OracleConnection(Connection);

            OracleCommand GROUPID = new OracleCommand("SELECT * FROM USER_GROUP_PROFILE_DETAIL WHERE USER_GROUP_ID = :Data1 AND USER_ID = :Data2");
            GROUPID.Connection = conn;
                conn.Open();
          
            //Saves Session Data for DB connections later

                //Validates User Login Info
                if (ADLogin(WinID.Text, WinIDpass.Text))
                {
                
                GROUPID.Parameters.Add("Data1", OracleType.VarChar).Value = "SMART_TORQUE";
                GROUPID.Parameters.Add("Data2", OracleType.VarChar).Value = (string)HttpContext.Current.Session["empNo"];
                OracleDataReader HasAccess = GROUPID.ExecuteReader();
                if (HasAccess.HasRows)
                {
                    Session["user"] = HttpContext.Current.Session["userName"];
                    Response.Redirect("Main.aspx");
                }
                else
                {
                    conn.Close();
                    Response.Write("<script>alert('Missing Access for User Group Smart_Torque!')</script>");
                }
                }
                else if (WinID.Text == "" && WinIDpass.Text == "")
                {
                    conn.Close();
                    Response.Write("<script>alert('ID and Password cannot be blank!')</script>");
                }
                else
                {
                    conn.Close();
                    Response.Write("<script>alert('Wrong ID or Password!')</script>");
                }
            }        
        //User to login with their Window ID and Window Password
        public bool ADLogin(string username, string password)
        {
            bool valid = false;
            DirectoryEntry SearchRoot = new DirectoryEntry("LDAP://ASIA.AD.CELESTICA.COM", username, password);
            DirectorySearcher Searcher = new DirectorySearcher(SearchRoot);
            //Additional verification
            Searcher.PropertiesToLoad.Add("cn");
            Searcher.PropertiesToLoad.Add("mail");
            Searcher.PropertiesToLoad.Add("EmployeeNumber");
            Searcher.Filter = "(sAMAccountName=" + username + ")";
            try
            {
                SearchResult result = Searcher.FindOne();
                if (result != null)
                {
                    HttpContext.Current.Session["userName"] = result.GetDirectoryEntry().Properties["cn"].Value.ToString().Split(':')[0].ToUpper();
                    HttpContext.Current.Session["empNo"] = result.GetDirectoryEntry().Properties["EmployeeNumber"].Value.ToString();
                    HttpContext.Current.Session["Email"] = result.GetDirectoryEntry().Properties["mail"].Value.ToString();
                    var email = result.GetDirectoryEntry().Properties["mail"].Value.ToString();
                    valid = true;
                }
                else
                {
                    valid = false;
                }
            }

            catch (Exception ex)
            {
            }
            return valid;
        }

    } 
}
