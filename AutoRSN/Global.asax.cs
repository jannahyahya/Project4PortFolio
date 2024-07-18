using AutoRSN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AutoRSN
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //var cors = new EnableCorsAttribute("*", "*", "*");
           // config.EnableCors(cors);
            //GlobalFilters.Filters.Add(new RequireHttpsAttribute());
        }

        void Session_Start(object sender, EventArgs e)
        {
            Session.Timeout = 120;
        }

        protected void Session_OnEnd(object sender, EventArgs e)
        {

           // HttpContext.Current.Response.Write("<script>alert('Session Timeout'); location.href = '/AutoRSN'; </script>");

            // insert code here
           //StaticSession.sessionList.Remove(Session["UserId"].ToString());
            //System.Diagnostics.Debug.WriteLine(Session["UserId"] + " : ended");
        }
    }
}
