using AutoRSN.Models;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices;
using System.Linq;
using System.Web;

namespace AutoRSN.Repositories
{
    public class AccountRepository
    {
       

      public bool isAccountValid(Account account)
        {
           // List<string> temp = new List<string>();
            OracleConnection conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=rsn;Password=o5rsntcp;");
            conn.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM Z_M_USER WHERE USERNAME='"+account.USERNAME+"'";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = null;
            try {
               dr = cmd.ExecuteReader();
            }

            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.InnerException);
            }

            if (dr.HasRows)
            {

                try
                {
                    System.Diagnostics.Debug.WriteLine("login is valid: " + dr.GetValue(0));
                    conn.Close();
                    conn.Dispose();
                    //System.Diagnostics.Debug.WriteLine("login is valid: " + dr.GetValue(0));
                }

                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }

                
                 return true;
            }
              
            else
            {
                conn.Close();
                conn.Dispose();
                System.Diagnostics.Debug.WriteLine("login is invalid: ");
                return false;

            }
               

            

        }

        public string getUserLDAPEmail(Account user)
        {
            DirectoryEntry SearchRoot = new DirectoryEntry("LDAP://ASIA.AD.CELESTICA.COM", user.USERNAME, user.PASSWORD);
            DirectorySearcher Searcher = new DirectorySearcher(SearchRoot);
            Searcher.PropertiesToLoad.Add("mail");
            Searcher.Filter = "(sAMAccountName=" + user.USERNAME + ")";
            SearchResult result = Searcher.FindOne();

           
            return result.GetDirectoryEntry().Properties["mail"].Value.ToString();

        }

        public bool isLDAPUserExist(Account AccountData)
        {
            DirectoryEntry SearchRoot = new DirectoryEntry("LDAP://ASIA.AD.CELESTICA.COM", AccountData.USERNAME, AccountData.PASSWORD);
            DirectorySearcher Searcher = new DirectorySearcher(SearchRoot);
            Searcher.PropertiesToLoad.Add("mail");
            Searcher.Filter = "(sAMAccountName=" + AccountData.USERNAME + ")";
            SearchResult result = Searcher.FindOne();

            if (result != null)
                return true;
            else
                return false;

        }


        public string getUserLevel(Account account)
        {
            // List<string> temp = new List<string>();
            OracleConnection conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=rsn;Password=o5rsntcp;");
            conn.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT LVL FROM Z_M_USER WHERE USERNAME='" + account.USERNAME + "'";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            string usrlvl = "";
            if (dr.HasRows)
            {
                usrlvl = Convert.ToString(dr.GetValue(0));
               
            }

            else
            {
                usrlvl = "1";

            }

            conn.Close();
            conn.Dispose();
            return Convert.ToString(usrlvl);
        }

    }
}