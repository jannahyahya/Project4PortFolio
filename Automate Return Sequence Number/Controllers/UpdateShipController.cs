using AutoRSN.Models;
using AutoRSN.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AutoRSN.Controllers
{
    public class UpdateShipController : Controller
    {
        ShipStatusRepository repo = new ShipStatusRepository();
        // GET: UpdateShip
        public ActionResult Index()
        {
            List<ShipStatus> shipstatus = new List<ShipStatus>();
            for (int a = 1; a <= 10; a++)
            {
                shipstatus.Add(new ShipStatus());

            }

                //shipstatus.STATUS = "YES";
                return View(shipstatus);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(List<ShipStatus> shipstatus, FormCollection collection)
        {
            // shipstatus.SHIPSTATUS = collection["SHIPSTATUS"].ToString();
            //Debug.WriteLine(collection["shipstatus"].ToString());
         
            StringBuilder builder = new StringBuilder();

            builder.Append("Successfully update ship status for rsnno = ");
            foreach(ShipStatus shipobj in shipstatus)
            {

                if(shipobj.RSNNO == null)
                { }
                
                   else
                {
                    if (repo.updateShipStatus(shipobj.RSNNO.Trim()))
                        builder.Append($"{shipobj.RSNNO}/ ");


                    else
                        builder.Append($"ERROR {shipobj.RSNNO}/ ");

                }
                
                
            }



            TempData["Message"] = builder.ToString();

            List<ShipStatus> shiplist = new List<ShipStatus>();
            for (int a = 1; a <= 10; a++)
            {
                shiplist.Add(new ShipStatus());

            }
            return View(shiplist);
        }
    }
}