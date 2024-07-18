using AutoRSN.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoRSN
{
    /// <summary>
    /// Summary description for ShipsetHandler
    /// </summary>
    public class ShipsetHandler : IHttpHandler
    {
          
        ShipsetRepositories rp = new ShipsetRepositories();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string custPn = context.Request.Form["custPn"];
            string clsPn = context.Request.Form["clsPn"];

            System.Diagnostics.Debug.WriteLine(custPn);
            System.Diagnostics.Debug.WriteLine(clsPn);

             rp.DeleteShipset(custPn,clsPn);
            context.Response.ContentType = "text/plain";
            context.Response.Write("success delete");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}