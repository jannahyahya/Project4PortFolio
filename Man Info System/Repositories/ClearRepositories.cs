using AutoRSN.Utility;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace AutoRSN.Repositories
{
    public class ClearRepositories
    {

        public void clearData()
        {
            var conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=goodrich;Password=o5goodrichtcp;");
            conn.Open();


            OracleCommand del1 = new OracleCommand();
            del1.Connection = conn;
            del1.CommandType = CommandType.Text;
            del1.CommandText = "delete from mis where createdon < sysdate -30";

            //OracleCommand del2 = new OracleCommand();
            //del2.Connection = conn; 
            //del2.CommandType = CommandType.Text;
           // del2.CommandText = "delete from mis where RECEIVEDON is null and createdon < TRUNC(SYSDATE) - 30";

            del1.ExecuteNonQuery();
            //del2.ExecuteNonQuery();


            conn.Dispose();
            conn.Close();



        }

        public void setComplete()
        {
            IniFile inifile = new IniFile(HttpContext.Current.Server.MapPath("~") + "\\Config.ini");
            //Debug.WriteLine(HttpContext.Current.Server.MapPath("/") + "Config.ini");
            string interval = inifile.Read("Interval","AUTOCOMPLETE"); // in hours

            Debug.WriteLine(interval);

            bool valid = false;
            var conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=goodrich;Password=o5goodrichtcp;");
            conn.Open();


            OracleCommand InsertCommand = new OracleCommand();
            InsertCommand.Connection = conn;
            InsertCommand.CommandType = CommandType.Text;
            InsertCommand.CommandText = $"update mis set receivedby = 'AUTO-SYSTEM',RECEIVEDON=sysdate where SYSDATE - confirmedon > ({interval}/24) and receivedby is null and status = 'DONE'";

            
           // Debug.WriteLine(InsertCommand.CommandText);
          //  try
          //  {
                InsertCommand.ExecuteNonQuery();
                valid = true;
           // }

          //  catch
           // { }

            conn.Dispose();
            conn.Close();

            //return valid;

        }
    }
}