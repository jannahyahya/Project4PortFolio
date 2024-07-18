using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace AutoRSN
{
    /// <summary>
    /// Summary description for GetGTSData
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class GetGTSData : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string HelloWorld()
        {
            Context.Response.Output.Write("Hello World");
            Context.Response.End();
            return string.Empty;
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetMessage()   // Home.CS 
        {
            //return "GOGO";
            Context.Response.Output.Write("Hello World");
            Context.Response.End();
            return string.Empty;
        }

        [WebMethod]
       [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Xml)]
        public static string testservice()
        {
           

            try
            {


               // JavaScriptSerializer js = new JavaScriptSerializer();
               // Context.Response.Clear();
               // Context.Response.ContentType = "application/text";
               // Context.Response.Write("dsadsada");

            }
            catch (Exception ex)
            {

            }
            finally
            {
               // departmentBL = null;
            }


            return "test";
        }


    }
}
