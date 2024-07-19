using AutoRSN.Models;
using AutoRSN.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoRSN.Controllers
{
    public class GTSMasterController : Controller
    {
        GTSMasterRepository gtsRepo = new GTSMasterRepository();

        // GET: GTSMaster
        public ActionResult Index()
        {
            return View();
        }


        [ValidateAntiForgeryToken()]
        [HttpPost]
        public ActionResult index(FormCollection collection)
        {
            string LOGICAL_SYS_GROUP = collection["LOGICAL_SYS_GROUP"];
            string CELPN = collection["CELPN"];
            string EXPORT_CONTROL = collection["EXPORT_CONTROL"];
            string MITI_PERMIT = collection["MITI"];
            string REMARK = collection["REMARK"];


            GTSMaster gtsMaster = new GTSMaster();
            gtsMaster.LOGICAL_SYS_GROUP = Convert.ToString(LOGICAL_SYS_GROUP).Trim().ToUpper();
            gtsMaster.CELPN = Convert.ToString(CELPN).Trim().ToUpper();
            gtsMaster.EXPORT_CONTROL = Convert.ToString(EXPORT_CONTROL).Trim().ToUpper();
            gtsMaster.MITI_PERMIT = Convert.ToString(MITI_PERMIT).Trim().ToUpper();
            gtsMaster.REMARK = Convert.ToString(REMARK).Trim();


            if (!gtsRepo.isGTSExist(gtsMaster.CELPN))
            {
                if (gtsRepo.insertGTS(gtsMaster) == 1)
                   {
                        //return JsonConvert.SerializeObject(new { STATUS = "OK", MESSAGE = "GTS insert successfully" });
                       TempData["INSERT_ALERT"] = "[SUCCESS] GTS Celestica PN insert successfully";
                   }
               else
                   {
                       //return JsonConvert.SerializeObject(new { STATUS = "ERROR", MESSAGE = "GTS insert failed" });
                       TempData["INSERT_ALERT"] = "[FAILED] GTS Celestica PN insert failed!";

                  }
               
            }
            else
            {
                TempData["INSERT_ALERT"] = "[FAILED] GTS Celestica PN already exist!";

            }
            

            return View();

        }


        public ActionResult Download_Excel()
        {
            return File(gtsRepo.exportExcel(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "GTSMaster" + ".xlsx");
        }

        [HttpPost]
        public string getGTSMaster (int id)
        {
            return JsonConvert.SerializeObject(gtsRepo.getGTSMaster(id));

        }



        [HttpPost]
        public string updateGTSMaster(int ID, string LOGICAL_SYS_GROUP, string CELPN, string EXPORT_CONTROL, string MITI_PERMIT, string REMARK)
        {
            GTSMaster gtsMaster = new GTSMaster();
            gtsMaster.ID = ID;
            gtsMaster.LOGICAL_SYS_GROUP = LOGICAL_SYS_GROUP;
            gtsMaster.CELPN = CELPN;
            gtsMaster.EXPORT_CONTROL = EXPORT_CONTROL;
            gtsMaster.MITI_PERMIT = MITI_PERMIT;
            gtsMaster.REMARK = REMARK;

            if (gtsRepo.updateGTS(gtsMaster) == 1)
            {
                return JsonConvert.SerializeObject(new { STATUS = "OK", MESSAGE = "GTS update successfully" });
            }
            else
            {
                return JsonConvert.SerializeObject(new { STATUS = "ERROR", MESSAGE = "GTS update failed" });

            }
        }


        [HttpPost]
        public string deleteGTS(int id)
        {
            if(gtsRepo.deleteGTS(id) == 1)
                return JsonConvert.SerializeObject(new { STATUS = "OK", MESSAGE = "GTS delete successfully" });
            else
                return JsonConvert.SerializeObject(new { STATUS = "ERROR", MESSAGE = "GTS delete failed" });
        }

    }
}