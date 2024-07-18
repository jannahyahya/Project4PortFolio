using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate;
using AutoRSN.Models;
using AutoRSN.Security;
using AutoRSN.Repositories;

namespace AutoRSN.Controllers
{
    public class SalesOrderController : Controller
    {
        SalesOrderRepository cr = new SalesOrderRepository();

        public ActionResult Index()
        {
            return View(Enumerable.Empty<SalesOrder>());
        }
        public ActionResult Index(System.Web.Mvc.FormCollection frmCollection)
        {
            string FilterByCCMGCriteria = frmCollection["FilterByCCMG"].ToString();

            return View(cr.GetSalesOrderBySearch(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), FilterByCCMGCriteria));
        }
    }
}