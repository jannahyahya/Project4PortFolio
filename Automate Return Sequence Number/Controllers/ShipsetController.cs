using AutoRSN.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoRSN.Controllers
{
    public class ShipsetController : Controller
    {
        ShipsetRepositories rp = new ShipsetRepositories();
        // GET: Shipset
        public ActionResult Index()
        {
            return View(rp.getShipsetList());
        }

        public ActionResult Create()
        {
            Models.Shipset shipset = new Models.Shipset();
            return View(shipset);
        }

        [HttpPost]
        public ActionResult Index(System.Web.Mvc.FormCollection frmCollection)
        {
           // string cust_pn = frmCollection["cust_pn"].ToString();
            string cls_pn = frmCollection["cls_pn"].ToString();


            return View(rp.getShipsetListByClsPN(cls_pn));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.Shipset shipset, FormCollection collection)
        {

            System.Diagnostics.Debug.WriteLine(shipset.CUST_PN);
            System.Diagnostics.Debug.WriteLine(shipset.CLS_PN);
            rp.insertShipset(shipset);

            TempData["Message"] = "New record has been created.";
            return View(shipset);

        }
    }
}