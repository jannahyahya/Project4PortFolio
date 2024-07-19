using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using AutoRSN.Repositories;
using AutoRSN.Models;
using AutoRSN.Security;
using AutoRSN.Repositories;

namespace AutoRSN.Controllers
{
    [AuthorizeExceptionHandler]
    public class ReasonController : Controller
    {
        ReasonRepository rr = new ReasonRepository();

        public ActionResult Index()
        {
            //rr.GetReason(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()),true)
            return View(rr.GetReasonBySearch(Convert.ToInt32(Session["ServerId"].ToString().ToUpper())));
        }

        [HttpPost]
        public ActionResult Index(System.Web.Mvc.FormCollection frmCollection)
        {
            string FilterByReasonCriteria = frmCollection["FilterByReason"].ToString();

            return View(rr.GetReasonBySearch(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), FilterByReasonCriteria));
        }

        public ActionResult Details(int ReasonId)
        {
            Reason objReason = rr.GetReasonByReasonId(1, ReasonId);
            if (objReason == null)
            {
                return HttpNotFound();
            }
            return View(objReason);
        }

        public ActionResult Edit(int ReasonId)
        {
            var objReason = rr.GetReasonByReasonId(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), ReasonId);
            BindEditData(objReason);
            return View(objReason);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Reason objReason)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    if (objReason.STATUS == "")
                    {
                        ModelState.AddModelError("StatusMessage", "PLEASE SELECT STATUS");
                        REBINDEDITDATA(objReason);
                        return View(objReason);
                    }
                    else
                    {
                        Reason OriginalObjServer = rr.GetReasonByReasonId(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), objReason.REASONID);
                        objReason.MODIFIEDBY = Session["UserId"].ToString().ToUpper();
                        if (IsDataChanged(OriginalObjServer, objReason))
                        {
                            rr.UpdateReasonByReasonId(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), objReason);
                            TempData["Message"] = objReason.REASON.ToUpper() + " reason has been updated successfully.";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["Message"] = "No changes has been made at the current record.";
                            REBINDEDITDATA(objReason);
                            return View(objReason);
                        }
                    }
                }
                else
                {
                    REBINDEDITDATA(objReason);
                    return View(objReason);
                }
            }
            catch(Exception ex)
            {
                TempData["Message"] = "Sorry, something went wrong. Please contact Administrator. MESSAGE:" + ex.Message;
                return View(objReason);
            }
        }


        public ActionResult Delete(int reasonID)
        {

 
            rr.deleteReason(reasonID);
            return RedirectToAction("Index");

        }
        [HttpGet]
        public ActionResult Create()
        {
            Reason objReason = new Reason();

            objReason.ListOfStatus = new List<SelectListItem>()
            {
                new SelectListItem{ Text="-- PLEASE SELECT STATUS --" , Selected = true, Value =""  },
                new SelectListItem { Text="ACTIVE" , Selected = false, Value ="Y"  },
                new SelectListItem { Text="INACTIVE" , Selected =false, Value ="N"  }
            };

            return View(objReason);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Reason objReason, FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (objReason.STATUS == "")
                    {
                        ModelState.AddModelError("StatusMessage", "PLEASE SELECT STATUS.");
                        REBINDCREATEDATA(objReason);
                        return View(objReason);
                    }
                    else
                    {
                        objReason.CREATEDBY = Session["UserId"].ToString().ToUpper();

                        if (rr.IsExistReason(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()),objReason.REASON.ToString()) != null)
                        {
                            TempData["Message"] = "Same reason exist in the system.";
                            REBINDCREATEDATA(objReason);
                            return View(objReason);
                        }
                        else
                        {
                            rr.InsertReason(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()),objReason);
                            TempData["Message"] = "New reason has been created.";
                            return RedirectToAction("Index");
                        }

                    }
                }
                else
                {
                    REBINDCREATEDATA(objReason);
                    return View(objReason);
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Sorry, something went wrong. Please contact Administrator. MESSAGE:" + ex.Message;
                REBINDCREATEDATA(objReason);
                return View(objReason);
            }
        }



        public void BindEditData(Reason objReason)
        {
            ModelState.Remove("ListOfStatus");
            objReason.ListOfStatus = new List<SelectListItem>()
                {
                new SelectListItem{ Text="-- PLEASE SELECT STATUS --" , Selected = true, Value =""  },
                new SelectListItem { Text="ACTIVE" , Selected =false, Value ="Y"  },
                new SelectListItem { Text="INACTIVE" , Selected =false, Value ="N"  }
                };

            objReason.STATUS = objReason.STATUS;
        }

        public void REBINDEDITDATA(Reason objReason)
        {
            ModelState.Remove("ListOfStatus");

            objReason.ListOfStatus = new List<SelectListItem>()
                {
                new SelectListItem{ Text="-- PLEASE SELECT STATUS --" , Selected = true, Value =""  },
                new SelectListItem { Text="ACTIVE" , Selected =false, Value ="Y"  },
                new SelectListItem { Text="INACTIVE" , Selected =false, Value ="N"  }
                };
        }

        public void REBINDCREATEDATA(Reason objReason)
        {
            ModelState.Remove("ListOfStatus");

            objReason.ListOfStatus = new List<SelectListItem>()
                {
                new SelectListItem{ Text="-- PLEASE SELECT STATUS --" , Selected = false, Value =""  },
                new SelectListItem { Text="ACTIVE" , Selected =true, Value ="Y"  },
                new SelectListItem { Text="INACTIVE" , Selected =false, Value ="N"  }
                };
        }

        private bool IsDataChanged(Reason OriginalObjReason, Reason NewObjReason)
        {
            bool Changed = false;
            Changed = Changed || (!((OriginalObjReason.REASON).Equals("") ? "" : OriginalObjReason.REASON).Equals(NewObjReason.REASON));
            Changed = Changed || (!((OriginalObjReason.DESCRIPTION).Equals("") ? "" : OriginalObjReason.DESCRIPTION).Equals(NewObjReason.DESCRIPTION));
            Changed = Changed || (!((OriginalObjReason.STATUS).Equals("") ? "" : OriginalObjReason.STATUS).Equals(NewObjReason.STATUS));
            return Changed;
        }

    }
}
