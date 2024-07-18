

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
    [AuthorizeExceptionHandler]
    public class EmailController : Controller
    {
        EmailRepository er = new EmailRepository();
        CustomerRepository cr = new CustomerRepository();
        // GET: Email
        public ActionResult Index()
        {
                return View(Enumerable.Empty<Email>());
        }

        [HttpPost]
        public ActionResult Index(System.Web.Mvc.FormCollection frmCollection)
        {
            string FilterByCCETCriteria = frmCollection["FilterByCCET"].ToString();

            return View(er.GetEmailBySearch(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), FilterByCCETCriteria));
        }

        public ActionResult Details(int EmailId)
        {
            Email objEmail = er.GetEmailByEmailId(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), EmailId);
            if (objEmail == null)
            {
                return HttpNotFound();
            }
            return View(objEmail);
        }

        public ActionResult Edit(int EmailId)
        {
            var objEmail = er.GetEmailByEmailId(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), EmailId);
            BindEditData(objEmail);
            return View(objEmail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Email objEmail)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    if (objEmail.STATUS == "")
                    {
                        ModelState.AddModelError("StatusMessage", "PLEASE SELECT STATUS");
                        ReBindEditData(objEmail);
                        return View(objEmail);
                    }else if(objEmail.CUSTOMERCODE=="")
                    {
                        ModelState.AddModelError("CustomerMessage", "PLEASE SELECT CUSTOMERCODE");
                        ReBindEditData(objEmail);
                        return View(objEmail);
                    }
                    else
                    {
                        Email OriginalObjEmail = er.GetEmailByEmailId(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), objEmail.EMAILID);
                        objEmail.MODIFIEDBY = Session["UserId"].ToString().ToUpper();
                        if (IsDataChanged(OriginalObjEmail, objEmail))
                        {
                            er.UpdateEmail(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), objEmail);
                            TempData["Message"] = objEmail.EMAILTITLE.ToUpper() + " email has been updated successfully.";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["Message"] = "No changes has been made at the current record.";
                            ReBindEditData(objEmail);
                            return View(objEmail);
                        }
                    }
                }
                else
                {
                    ReBindEditData(objEmail);
                    return View(objEmail);
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Sorry, something went wrong. Please contact Administrator. MESSAGE:" + ex.Message;
                return View(objEmail);
            }
        }


        [HttpGet]
        public ActionResult Create()
        {
            Email objItemEmail = new Email();


            List<Customer> ListCustomer = new List<Customer>();
            ListCustomer = new List<Customer>() { new Customer { CUSTOMERCODE = "", CUSTOMERNAME = "-- PLEASE SELECT CUSTOMER --" } };

            foreach (var item in cr.GetCustomer(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), true))
            {
                Customer CustomerList = new Customer();
                CustomerList.CUSTOMERCODE = item.CUSTOMERCODE;
                CustomerList.CUSTOMERNAME = item.CUSTOMERCODE;
                ListCustomer.Add(CustomerList);
            }

            objItemEmail.ListOfCustomer = ListCustomer;

            objItemEmail.ListOfStatus = new List<SelectListItem>()
            {
                new SelectListItem{ Text="-- PLEASE SELECT STATUS --" , Selected = true, Value =""  },
                new SelectListItem { Text="ACTIVE" , Selected = false, Value ="Y"  },
                new SelectListItem { Text="INACTIVE" , Selected =false, Value ="N"  }
            };

            return View(objItemEmail);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Email objEmail, FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (objEmail.STATUS == "")
                    {
                        ModelState.AddModelError("StatusMessage", "PLEASE SELECT STATUS.");
                        RebindCreateData(objEmail);
                        return View(objEmail);
                    }
                    else if (objEmail.CUSTOMERCODE == "")
                    {
                        ModelState.AddModelError("StatusMessage", "PLEASE SELECT STATUS.");
                        RebindCreateData(objEmail);
                        return View(objEmail);
                    }
                    else
                    {
                        objEmail.CREATEDBY = Session["UserId"].ToString().ToUpper();

                        if (er.IsExistEmail(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), objEmail.CUSTOMERCODE, objEmail.EMAILTITLE) != null)
                        {
                            TempData["Message"] = "Same customer # and email title exist in the system.";
                            RebindCreateData(objEmail);
                            return View(objEmail);
                        }
                        else
                        {
                            er.InsertEmail(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), objEmail);
                            TempData["Message"] = "New record has been created.";
                            return RedirectToAction("Index");
                        }

                    }
                }
                else
                {
                    RebindCreateData(objEmail);
                    return View(objEmail);
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Sorry, something went wrong. Please contact Administrator. MESSAGE:" + ex.Message;
                RebindCreateData(objEmail);
                return View(objEmail);
            }
        }


        public void BindEditData(Email objEmail)
        {
            ModelState.Remove("ListOfStatus");
            ModelState.Remove("ListOfCustomer");
            List<Customer> ListCustomer = new List<Customer>();
            ListCustomer = new List<Customer>() { new Customer { CUSTOMERCODE ="", CUSTOMERNAME = "-- PLEASE SELECT CUSTOMER --" } };

            foreach (var item in cr.GetCustomer(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), true))
            {
                Customer CustomerList = new Customer();
                CustomerList.CUSTOMERCODE = item.CUSTOMERCODE;
                CustomerList.CUSTOMERNAME = item.CUSTOMERCODE;
                ListCustomer.Add(CustomerList);
            }


            objEmail.ListOfCustomer = ListCustomer;
            objEmail.CUSTOMERCODE = objEmail.CUSTOMERCODE;

            objEmail.ListOfStatus = new List<SelectListItem>()
                {
                new SelectListItem{ Text="-- PLEASE SELECT STATUS --" , Selected = true, Value =""  },
                new SelectListItem { Text="ACTIVE" , Selected =false, Value ="Y"  },
                new SelectListItem { Text="INACTIVE" , Selected =false, Value ="N"  }
                };

            objEmail.STATUS = objEmail.STATUS;
        }

        public void ReBindEditData(Email objEmail)
        {
            ModelState.Remove("ListOfStatus");
            ModelState.Remove("ListOfCustomer");
            List<Customer> ListCustomer = new List<Customer>();
            ListCustomer = new List<Customer>() { new Customer { CUSTOMERCODE = "", CUSTOMERNAME = "-- PLEASE SELECT CUSTOMER --" } };

            foreach (var item in cr.GetCustomer(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), true))
            {
                Customer CustomerList = new Customer();
                CustomerList.CUSTOMERCODE = item.CUSTOMERCODE;
                CustomerList.CUSTOMERNAME = item.CUSTOMERCODE;
                ListCustomer.Add(CustomerList);
            }

            objEmail.ListOfCustomer = ListCustomer;

            objEmail.ListOfStatus = new List<SelectListItem>()
                {
                new SelectListItem{ Text="-- PLEASE SELECT STATUS --" , Selected = true, Value =""  },
                new SelectListItem { Text="ACTIVE" , Selected =false, Value ="Y"  },
                new SelectListItem { Text="INACTIVE" , Selected =false, Value ="N"  }
                };

        }

        public void RebindCreateData(Email objEmail)
        {
            ModelState.Remove("ListOfStatus");
            ModelState.Remove("ListOfCustomer");

            List<Customer> ListCustomer = new List<Customer>();
            ListCustomer = new List<Customer>() { new Customer { CUSTOMERCODE = "", CUSTOMERNAME = "-- PLEASE SELECT CUSTOMER --" } };

            foreach (var item in cr.GetCustomer(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), true))
            {
                Customer CustomerList = new Customer();
                CustomerList.CUSTOMERCODE = item.CUSTOMERCODE;
                CustomerList.CUSTOMERNAME = item.CUSTOMERCODE;
                ListCustomer.Add(CustomerList);
            }

            
            objEmail.ListOfCustomer = ListCustomer;

            objEmail.ListOfStatus = new List<SelectListItem>()
                {
                new SelectListItem{ Text="-- PLEASE SELECT STATUS --" , Selected = false, Value =""  },
                new SelectListItem { Text="ACTIVE" , Selected =true, Value ="Y"  },
                new SelectListItem { Text="INACTIVE" , Selected =false, Value ="N"  }
                };
        }


        private bool IsDataChanged(Email OriginalObjEmail, Email objEmail)
        {
            bool Changed = false;
            Changed = Changed || (!((OriginalObjEmail.CUSTOMERCODE).Equals("") ? "" : OriginalObjEmail.CUSTOMERCODE).Equals(objEmail.CUSTOMERCODE));
            Changed = Changed || (!((OriginalObjEmail.EMAILTITLE).Equals("") ? "" : OriginalObjEmail.EMAILTITLE).Equals(objEmail.EMAILTITLE));
            Changed = Changed || (!((OriginalObjEmail.ATTENTIONNAME).Equals("") ? "" : OriginalObjEmail.ATTENTIONNAME).Equals(objEmail.ATTENTIONNAME));
            Changed = Changed || (!((OriginalObjEmail.EMAILTO).Equals("") ? "" : OriginalObjEmail.EMAILTO).Equals(objEmail.EMAILTO));
            Changed = Changed || (!((OriginalObjEmail.EMAILCC).Equals("") ? "" : OriginalObjEmail.EMAILCC).Equals(objEmail.EMAILCC));
            Changed = Changed || (!((OriginalObjEmail.STATUS).Equals("") ? "" : OriginalObjEmail.STATUS).Equals(objEmail.STATUS));
            return Changed;
        }


    }
}