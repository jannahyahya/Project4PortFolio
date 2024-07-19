using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate;
using AutoRSN.Models;
using AutoRSN.Security;
using AutoRSN.Repositories;
using System.Diagnostics;

namespace AutoRSN.Controllers
{
    [AuthorizeExceptionHandler]
    public class ItemNotesController : Controller
    {
        ItemNotesRepository ir = new ItemNotesRepository();
        ReasonRepository rr = new ReasonRepository();
        // GET: ItemNotes
        public ActionResult Index()
        {
            //ir.GetItemNotes(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), true)
            return View(ir.GetItemNotesBySO(Convert.ToInt32(Session["ServerId"].ToString().ToUpper())));
        }

        [HttpPost]
        public ActionResult Index(System.Web.Mvc.FormCollection frmCollection)
        {
            string FilterBySOCriteria = frmCollection["FilterBySO"].ToString();
            if (FilterBySOCriteria == null || FilterBySOCriteria.Equals(""))
                return View(ir.GetItemNotesBySO(Convert.ToInt32(Session["ServerId"].ToString().ToUpper())));
            else
                return View(ir.GetItemNotesBySO(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), FilterBySOCriteria));
        }

        public ActionResult Details(string SalesOrder,string soline)
        {
            List<ItemNotes> objItemNotes = ir.GetItemNotesBySO(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), SalesOrder,soline);
           
            if (objItemNotes == null)
            {
                return HttpNotFound();
            }
            return View(objItemNotes.ElementAt(0));
        }

        public ActionResult Edit(string SalesOrder,string soline)
        {
            List<ItemNotes> objItemNotes = ir.GetItemNotesBySO(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), SalesOrder,soline);
            if (objItemNotes == null)
                Debug.WriteLine("objnotes null");
            else
                Debug.WriteLine("objnotes not null");

            //BindEditData(objItemNotes);

            return View(objItemNotes.ElementAt(0));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ItemNotes objItemNotes)
        {

           // try
           // {
                if (ModelState.IsValid)
                {
                    if (objItemNotes.STATUS == "")
                    {
                      // ModelState.AddModelError("StatusMessage", "PLEASE SELECT STATUS");
                        //ReBindEditData(objItemNotes);
                        return View(objItemNotes);
                    }else if (objItemNotes.REASONID == 0)
                    {
                        ModelState.AddModelError("Reason Message", "PLEASE SELECT REASON");
                        //ReBindEditData(objItemNotes);
                        return View(objItemNotes);
                    }
                    else
                    {
                    List<ItemNotes> OriginalObjItemNotes = ir.GetItemNotesBySO(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), objItemNotes.SALESORDER,objItemNotes.SOLINE);
                        objItemNotes.MODIFIEDBY = Session["UserId"].ToString().ToUpper();
                       // if (IsDataChanged(OriginalObjItemNotes, objItemNotes))
                        //{
                            ir.UpdateItemNotesByPOSO(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), objItemNotes);
                            TempData["Message"] = " item notes has been updated successfully.";
                            return RedirectToAction("Index");
                       // }
                       // else
                       // {
                          //  TempData["Message"] = "No changes has been made at the current record.";
                          //  ReBindEditData(objItemNotes);
                         //   return View(objItemNotes);
                        //}
                    }
                }
                else
                {
                   // ReBindEditData(objItemNotes);
                    return View(objItemNotes);
                }
           // }
            //catch (Exception ex)
           // {
             //   TempData["Message"] = "Sorry, something went wrong. Please contact Administrator. MESSAGE:" + ex.Message;
              //  return View(objItemNotes);
           // }
        }

        [HttpGet]
        public ActionResult Create()
        {
            ItemNotes objItemNotes = new ItemNotes();


            List<Reason> ListReason = new List<Reason>();
            ListReason = new List<Reason>() { new Reason { REASONID = 0, REASON = "-- PLEASE SELECT REASON --" } };

            foreach (var item in rr.GetReason(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), true))
            {
                Reason reasonlist = new Reason();
                reasonlist.REASONID = item.REASONID;
                reasonlist.REASON = item.REASON;
                ListReason.Add(reasonlist);
            }

            objItemNotes.ListOfReason = ListReason;

            objItemNotes.ListOfStatus = new List<SelectListItem>()
            {
                new SelectListItem{ Text="-- PLEASE SELECT STATUS --" , Selected = true, Value =""  },
                new SelectListItem { Text = "UNBLOCK" , Selected = true, Value ="Y"  },
                new SelectListItem { Text = "BLOCK" , Selected =false, Value ="N"  }
            };

            return View(objItemNotes);
        }


        public ActionResult Delete(string so,string soline)
        {
            ir.deleteItemNote(so, soline);

         
            return RedirectToAction("Index");
            //return View(ir.GetItemNotesBySO(Convert.ToInt32(Session["ServerId"].ToString().ToUpper())));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ItemNotes objItemNotes, FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    objItemNotes.SALESORDER = objItemNotes.SALESORDER.Trim();

                    if (objItemNotes.STATUS == "")
                    {
                        ModelState.AddModelError("StatusMessage", "PLEASE SELECT STATUS.");
                        RebindCreateData(objItemNotes);
                        return View(objItemNotes);
                    }

                    else if (objItemNotes.REASONID == 0)
                    {
                        ModelState.AddModelError("StatusMessage", "PLEASE SELECT STATUS.");
                        RebindCreateData(objItemNotes);
                        return View(objItemNotes);
                    }
                    else
                    {
                        objItemNotes.CREATEDBY = Session["UserId"].ToString().ToUpper();
                        if(objItemNotes.SOLINE != null)
                        {
                            objItemNotes.SOLINE = objItemNotes.SOLINE.Trim();
                            if (ir.IsExistItemNotesbySOWithLine(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), objItemNotes.SALESORDER,objItemNotes.SOLINE))
                            {
                                TempData["Message"] = "Same customer so # and soline # exist in the system.";
                                RebindCreateData(objItemNotes);
                                return View(objItemNotes);
                            }
                            else
                            {
                                ir.InsertItemNotes(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), objItemNotes);
                                return RedirectToAction("Index");
                            }


                        }
                        
                        else
                        {
                         
                            if (ir.IsExistItemNotesbySO(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), objItemNotes.SALESORDER))
                            {
                                TempData["Message"] = "Same customer so # and soline # exist in the system.";
                                RebindCreateData(objItemNotes);
                                return View(objItemNotes);
                            }
                            else
                            {
                                ir.InsertItemNotes(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), objItemNotes);
                                return RedirectToAction("Index");
                            }
                        }

                    }
                }
                else
                {
                    RebindCreateData(objItemNotes);
                    return View(objItemNotes);
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Sorry, something went wrong. Please contact Administrator. MESSAGE:" + ex.Message;
                RebindCreateData(objItemNotes);
                return View(objItemNotes);
            }
        }



        public void BindEditData(ItemNotes objItemNotes)
        {
            ModelState.Remove("ListOfStatus");
            ModelState.Remove("ListOfReason");
            List<Reason> ListReason = new List<Reason>();
            ListReason = new List<Reason>() { new Reason { REASONID = 0, REASON = "-- PLEASE SELECT REASON --" } };

            foreach (var item in rr.GetReason(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()),true))
            {
                Reason reasonlist = new Reason();
                reasonlist.REASONID = item.REASONID;
                reasonlist.REASON = item.REASON;
                ListReason.Add(reasonlist);
            }


            objItemNotes.ListOfReason = ListReason;
            objItemNotes.REASONID = objItemNotes.REASONID;

            objItemNotes.ListOfStatus = new List<SelectListItem>()
                {
                new SelectListItem{ Text="-- PLEASE SELECT STATUS --" , Selected = true, Value =""  },
                new SelectListItem { Text="BLOCK" , Selected =true, Value ="Y"  },
                new SelectListItem { Text="UNBLOCK" , Selected =false, Value ="N"  }
                };

            objItemNotes.STATUS = objItemNotes.STATUS;

        }

        public void ReBindEditData(ItemNotes objItemNotes)
        {
            ModelState.Remove("ListOfStatus");
            ModelState.Remove("ListOfReason");
            List<Reason> ListReason = new List<Reason>();
            ListReason = new List<Reason>() { new Reason { REASONID = 0, REASON = "-- PLEASE SELECT REASON --" } };

            foreach (var item in rr.GetReason(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), true))
            {
                Reason reasonlist = new Reason();
                reasonlist.REASONID = item.REASONID;
                reasonlist.REASON = item.REASON;
                ListReason.Add(reasonlist);
            }

            objItemNotes.ListOfReason = ListReason;

            objItemNotes.ListOfStatus = new List<SelectListItem>()
                {
                new SelectListItem{ Text="-- PLEASE SELECT STATUS --" , Selected = true, Value =""  },
                new SelectListItem { Text="UNBLOCK" , Selected =true, Value ="Y"  },
                new SelectListItem { Text="BLOCK" , Selected =false, Value ="N"  }
                };
            
        }

        public void RebindCreateData(ItemNotes objItemNotes)
        {
            ModelState.Remove("ListOfStatus");
            ModelState.Remove("ListOfReason");

            List<Reason> ListReason = new List<Reason>();
            ListReason = new List<Reason>() { new Reason { REASONID = 0, REASON = "-- PLEASE SELECT REASON --" } };

            foreach (var item in rr.GetReason(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), true))
            {
                Reason reasonlist = new Reason();
                reasonlist.REASONID = item.REASONID;
                reasonlist.REASON = item.REASON;
                ListReason.Add(reasonlist);
            }

            objItemNotes.ListOfReason = ListReason;

            objItemNotes.ListOfStatus = new List<SelectListItem>()
                {
                new SelectListItem{ Text="-- PLEASE SELECT STATUS --" , Selected = false, Value =""  },




                new SelectListItem { Text="UNBLOCK" , Selected =true, Value ="Y"  },
                new SelectListItem { Text="BLOCK" , Selected =false, Value ="N"  }
                };
        }

        private bool IsDataChanged(ItemNotes OriginalObjItemNotes, ItemNotes NewObjItemNotes)
        {
            bool Changed = false;
            Changed = Changed || (!((OriginalObjItemNotes.CUSTOMERPO).Equals("") ? "" : OriginalObjItemNotes.CUSTOMERPO).Equals(NewObjItemNotes.CUSTOMERPO));
            Changed = Changed || (!((OriginalObjItemNotes.SALESORDER).Equals("") ? "" : OriginalObjItemNotes.SALESORDER).Equals(NewObjItemNotes.SALESORDER));
            Changed = Changed || (!((OriginalObjItemNotes.REASONID.ToString()).Equals("0") ? "0" : OriginalObjItemNotes.REASONID.ToString()).Equals(NewObjItemNotes.REASONID.ToString()));
            Changed = Changed || (!((OriginalObjItemNotes.REMARK).Equals("") ? "" : OriginalObjItemNotes.REMARK).Equals(NewObjItemNotes.REMARK));
            Changed = Changed || (!((OriginalObjItemNotes.STATUS).Equals("") ? "" : OriginalObjItemNotes.STATUS).Equals(NewObjItemNotes.STATUS));
            return Changed;
        }
    }
}