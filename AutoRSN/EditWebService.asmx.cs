using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace AutoRSN
{
    /// <summary>
    /// Summary description for EditWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class EditWebService : System.Web.Services.WebService
    {

    
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void HelloWorld()
        {
            HttpContext.Current.Response.Write("hello world");
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public void EditQuantityCall(int quantity,string rsnno, string uniqel)
        {

            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                conn.Open();
                int oriQty = 0;
                
                OracleCommand originalQty = new OracleCommand();
                originalQty.Connection = conn;
                originalQty.CommandType = CommandType.Text;
                originalQty.CommandText = string.Format("select shipqty from z_t_rsn_final where rsnno = '{0}' and uniqel = '{1}'",rsnno,uniqel);
                OracleDataReader reader = originalQty.ExecuteReader();

                while (reader.Read())
                {
                    oriQty = Convert.ToInt32(reader.GetValue(0).ToString());
                }

                originalQty.Dispose();
                ///////////////////////

                if(quantity > oriQty)
                {

                    HttpContext.Current.Response.Write("false");
                    return;

                }

                else
                {

                OracleCommand updateQty = new OracleCommand();
                updateQty.Connection = conn;
                updateQty.CommandType = CommandType.Text;
                updateQty.CommandText = string.Format("update z_t_rsn_final set shipqty={0} where rsnno = '{1}' and uniqel = '{2}'", quantity, rsnno, uniqel);
                updateQty.ExecuteNonQuery();


                    HttpContext.Current.Response.Write("true");
                    return;

                }  

            }
            
        }



    }
}
