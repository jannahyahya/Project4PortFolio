using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoRSN.Repositories;
using AutoRSN.Models;
using AutoRSN.Security;

namespace AutoRSN.Controllers
{
    [AuthorizeExceptionHandler]
    public class ServerController : Controller
    {
        ServerRepository sr = new ServerRepository();

        public ActionResult Index()
        {
            //sr.GetServer(true)
            return View(Enumerable.Empty<Server>());
        }

        [HttpPost]
        public ActionResult Index(System.Web.Mvc.FormCollection frmCollection)
        {
            string FilterByServerCriteria = frmCollection["FilterByServer"].ToString();

            return View(sr.GetServerBySearch(FilterByServerCriteria));
        }

        public ActionResult Details(int ServerId)
        {
            Server objServer = sr.GetServerByServerId(ServerId);
            if (objServer == null)
            {
                return HttpNotFound();
            }
            return View(objServer);
        }

        public ActionResult Edit(int ServerId)
        {
            var objServer = sr.GetServerByServerId(ServerId);
            BindEditData(objServer);
            return View(objServer);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Server objServer)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    if (objServer.STATUS == "")
                    {
                        ModelState.AddModelError("StatusMessage", "PLEASE SELECT STATUS");
                        REBINDEDITDATA(objServer);
                        return View(objServer);
                    }
                    else
                    {
                        Server OriginalObjServer = sr.GetServerByServerId(objServer.SERVERID);
                        objServer.MODIFIEDBY = Session["UserId"].ToString().ToUpper();
                        if (IsDataChanged(OriginalObjServer,objServer))
                        {
                            sr.UpdateServerByServerId(objServer);
                            TempData["Message"] = objServer.SERVER.ToUpper() +" server has been updated successfully.";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["Message"] = "No changes has been made at the current record. ";
                            REBINDEDITDATA(objServer);
                            return View(objServer);
                        }
                        
                    }
                }
                else
                {
                    REBINDEDITDATA(objServer);
                    return View(objServer);
                }
            }
            catch(Exception ex)
            {
                TempData["Message"] = "Sorry, something went wrong. Please contact Administrator. MESSAGE:" + ex.Message;
                return View(objServer);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            Server objServer = new Server();

            objServer.ListOfStatus = new List<SelectListItem>()
            {
                new SelectListItem{ Text="-- PLEASE SELECT STATUS --" , Selected = true, Value =""  },
                new SelectListItem { Text="ACTIVE" , Selected = false, Value ="Y"  },
                new SelectListItem { Text="INACTIVE" , Selected =false, Value ="N"  }
            };
            
            return View(objServer);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Server ObjServer, FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (ObjServer.STATUS == "")
                    {
                        ModelState.AddModelError("StatusMessage", "PLEASE SELECT STATUS.");
                        REBINDCREATEDATA(ObjServer);
                        return View(ObjServer);
                    }
                    else
                    {
                        ObjServer.CREATEDBY = Session["UserId"].ToString().ToUpper();

                        if (sr.IsExistServer(ObjServer.SERVER.ToString()) != null)
                        {
                            TempData["Message"] = "Same server exist in the system.";
                            REBINDCREATEDATA(ObjServer);
                            return View(ObjServer);
                        }
                        else
                        {
                            sr.InsertServer(ObjServer);
                            TempData["Message"] = "New server has been created.";
                            return RedirectToAction("Index");
                        }
                      
                    }
                }
                else
                {
                    REBINDCREATEDATA(ObjServer);
                    return View(ObjServer);
                }
            }
            catch(Exception ex)
            {
                TempData["Message"] = "Sorry, something went wrong. Please contact Administrator. MESSAGE:" + ex.Message;
                REBINDCREATEDATA(ObjServer);
                return View(ObjServer);
            }
        }

        public void BindEditData(Server objServer)
        {
            ModelState.Remove("ListOfStatus");
            objServer.ListOfStatus = new List<SelectListItem>()
                {
                new SelectListItem{ Text="-- PLEASE SELECT STATUS --" , Selected = true, Value =""  },
                new SelectListItem { Text="ACTIVE" , Selected =false, Value ="Y"  },
                new SelectListItem { Text="INACTIVE" , Selected =false, Value ="N"  }
                };

            objServer.STATUS = objServer.STATUS;
        }

        public void REBINDEDITDATA(Server objServer)
        {
            ModelState.Remove("ListOfStatus");

            objServer.ListOfStatus = new List<SelectListItem>()
                {
                new SelectListItem{ Text="-- PLEASE SELECT STATUS --" , Selected = true, Value =""  },
                new SelectListItem { Text="ACTIVE" , Selected =false, Value ="Y"  },
                new SelectListItem { Text="INACTIVE" , Selected =false, Value ="N"  }
                };
        }

        public void REBINDCREATEDATA(Server objServer)
        {
            ModelState.Remove("ListOfStatus");

            objServer.ListOfStatus = new List<SelectListItem>()
                {
                new SelectListItem{ Text="-- PLEASE SELECT STATUS --" , Selected = false, Value =""  },
                new SelectListItem { Text="ACTIVE" , Selected =true, Value ="Y"  },
                new SelectListItem { Text="INACTIVE" , Selected =false, Value ="N"  }
                };
        }


        private bool IsDataChanged(Server OriginalObjServer, Server NewObjServer)
        {
            bool Changed = false;

            Changed = Changed || (!((OriginalObjServer.SERVER).Equals("")? "":OriginalObjServer.SERVER).Equals(NewObjServer.SERVER));
            Changed = Changed || (!((OriginalObjServer.STATUS).Equals("") ? "" : OriginalObjServer.STATUS).Equals(NewObjServer.STATUS));

            return Changed;
        }


    }
}