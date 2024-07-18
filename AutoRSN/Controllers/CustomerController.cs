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
    public class CustomerController : Controller
    {
        CustomerRepository cr = new CustomerRepository();
 
        // GET: CustomerMaster
        public ActionResult Index()
        {
             //return View(Enumerable.Empty<Customer>());
            return View(cr.GetCustomerBySearch(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), ""));
        }

        [HttpPost]
        public ActionResult Index(System.Web.Mvc.FormCollection frmCollection)
        {
            string FilterByCCMGCriteria = frmCollection["FilterByCCMG"].ToString();

            return View(cr.GetCustomerBySearch(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), FilterByCCMGCriteria));
        }

        public ActionResult Details(string CustomerCode, string MaterialGroup)
        {
            Customer objCustomer = cr.GetCustomerByCustomerCodeMaterialGroup(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), CustomerCode, MaterialGroup);
            if (objCustomer == null)
            {
                return HttpNotFound();
            }
            return View(objCustomer);
        }


        public ActionResult DeleteConfirmation(string customer)
        {
            TempData["Message"] = "Are You Sure You Want To Delete Customer  = " + customer;
            Customer cust= new Customer();
            cust.CUSTOMERCODE= customer;
            List<Customer> list = new List<Customer>();
            list.Add(cust);
            return View(list);

        }

        public ActionResult Delete(string customer) //used in customer controller
        {
            CustomerRepository rp = new CustomerRepository();
            if (rp.deleteCustomer(customer))
            {
                TempData["Message"] = "Customer  = " + customer + " Deleted";
                return RedirectToAction("Index");
            }

            else
            {
                TempData["Message"] = "Error Deleting Customer.";

            }
            return View();

        }



        public ActionResult Edit(string CustomerCode, string MaterialGroup)
        {
            var objCustomer = cr.GetCustomerByCustomerCodeMaterialGroup(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), CustomerCode, MaterialGroup);
            Debug.WriteLine(objCustomer.BILLTO);
            BindEditData(objCustomer);
            return View(objCustomer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer objCustomer)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    if (objCustomer.STATUS == "")
                    {
                        ModelState.AddModelError("StatusMessage", "PLEASE SELECT STATUS");
                        RebindEditData(objCustomer);
                        return View(objCustomer);
                    }
                    else
                    {

                        //Customer OriginalObjCustomer = cr.GetCustomerByCustomerCodeMaterialGroup(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), objCustomer.CUSTOMERCODE, objCustomer.MATERIALGROUP);
                        objCustomer.MODIFIEDBY = Session["UserId"].ToString().ToUpper();
                        //if (IsDataChanged(OriginalObjCustomer, objCustomer))
                        //{
                            cr.UpdateCustomer(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), objCustomer);
                            TempData["Message"] = objCustomer.CUSTOMERCODE.ToUpper() + " customer has been updated successfully.";
                            return RedirectToAction("Index");
                       // }
                        //else
                        //{
                        //    TempData["Message"] = "No changes has been made at the current record.";
                        //    RebindEditData(objCustomer);
                        //    return View(objCustomer);
                        //}
                    }
                }
                else
                {
                    RebindEditData(objCustomer);
                    return View(objCustomer);
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Sorry, something went wrong. Please contact Administrator. MESSAGE:" + ex.Message;
                return View(objCustomer);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            Customer objCustomer = new Customer();
            //Session["PermissionList"] = null;

            objCustomer.ListOfStatus = new List<SelectListItem>()
            {
                new SelectListItem{ Text="-- PLEASE SELECT STATUS --" , Selected = true, Value =""  },
                new SelectListItem { Text="ACTIVE" , Selected = false, Value ="Y"  },
                new SelectListItem { Text="INACTIVE" , Selected =false, Value ="N"  }
            };

            return View(objCustomer);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer objCustomer, FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (objCustomer.STATUS == "")
                    {
                        ModelState.AddModelError("StatusMessage", "PLEASE SELECT STATUS.");
                        RebindCreateData(objCustomer);
                        return View(objCustomer);
                    }else
                    {
                        objCustomer.CREATEDBY = Session["UserId"].ToString().ToUpper();

                        if (cr.IsExistCustomer(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), objCustomer.CUSTOMERCODE, objCustomer.MATERIALGROUP) != null)
                        {
                            TempData["Message"] = "Same customer code and material group exist in the system.";
                            RebindCreateData(objCustomer);
                            return View(objCustomer);
                        }
                        else
                        {
                            cr.InsertCustomer(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), objCustomer);
                            TempData["Message"] = "New customer has been created.";
                            return RedirectToAction("Index");
                        }
                    }
                }
                else
                {
                    RebindCreateData(objCustomer);
                    return View(objCustomer);
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Sorry, something went wrong. Please contact Administrator. MESSAGE:" + ex.Message;
                RebindCreateData(objCustomer);
                return View(objCustomer);
            }
        }

        public void BindEditData(Customer objCustomer)
        {
            ModelState.Remove("ListOfStatus");
            objCustomer.ListOfStatus = new List<SelectListItem>()
                {
                new SelectListItem{ Text="-- PLEASE SELECT STATUS --" , Selected = true, Value =""  },
                new SelectListItem { Text="ACTIVE" , Selected =false, Value ="Y"  },
                new SelectListItem { Text="INACTIVE" , Selected =false, Value ="N"  }
                };

            objCustomer.STATUS = objCustomer.STATUS;
        }

        public void RebindEditData(Customer objCustomer)
        {
            ModelState.Remove("ListOfStatus");

            objCustomer.ListOfStatus = new List<SelectListItem>()
                {
                new SelectListItem{ Text="-- PLEASE SELECT STATUS --" , Selected = true, Value =""  },
                new SelectListItem { Text="ACTIVE" , Selected =false, Value ="Y"  },
                new SelectListItem { Text="INACTIVE" , Selected =false, Value ="N"  }
                };
        }

        public void RebindCreateData(Customer objCustomer)
        {
            ModelState.Remove("ListOfStatus");

            objCustomer.ListOfStatus = new List<SelectListItem>()
                {
                new SelectListItem{ Text="-- PLEASE SELECT STATUS --" , Selected = false, Value =""  },
                new SelectListItem { Text="ACTIVE" , Selected =true, Value ="Y"  },
                new SelectListItem { Text="INACTIVE" , Selected =false, Value ="N"  }
                };
        }

        private bool IsDataChanged(Customer OriginalObjCustomer, Customer objCustomer)
        {
            bool Changed = false;
            Changed = Changed || (!((OriginalObjCustomer.CUSTOMERGROUP).Equals("") ? "" : OriginalObjCustomer.CUSTOMERGROUP).Equals(objCustomer.CUSTOMERGROUP));
            Changed = Changed || (!((OriginalObjCustomer.CUSTOMERCODE).Equals("") ? "" : OriginalObjCustomer.CUSTOMERCODE).Equals(objCustomer.CUSTOMERCODE));
            Changed = Changed || (!((OriginalObjCustomer.MATERIALGROUP).Equals("") ? "" : OriginalObjCustomer.MATERIALGROUP).Equals(objCustomer.MATERIALGROUP));
            Changed = Changed || (!((OriginalObjCustomer.CUSTOMERNAME).Equals("") ? "" : OriginalObjCustomer.CUSTOMERNAME).Equals(objCustomer.CUSTOMERNAME));
            Changed = Changed || (!((OriginalObjCustomer.ADDRESS1).Equals("") ? "" : OriginalObjCustomer.ADDRESS1).Equals(objCustomer.ADDRESS1));
            Changed = Changed || (!((OriginalObjCustomer.ADDRESS2).Equals("") ? "" : OriginalObjCustomer.ADDRESS2).Equals(objCustomer.ADDRESS2));
            Changed = Changed || (!((OriginalObjCustomer.POSTCODE).Equals("") ? "" : OriginalObjCustomer.POSTCODE).Equals(objCustomer.POSTCODE));
            Changed = Changed || (!((OriginalObjCustomer.REGION).Equals("") ? "" : OriginalObjCustomer.REGION).Equals(objCustomer.REGION));
            Changed = Changed || (!((OriginalObjCustomer.COUNTRY).Equals("") ? "" : OriginalObjCustomer.COUNTRY).Equals(objCustomer.COUNTRY));
            Changed = Changed || (!((OriginalObjCustomer.FORWARDER).Equals("") ? "" : OriginalObjCustomer.FORWARDER).Equals(objCustomer.FORWARDER));
            Changed = Changed || (!((OriginalObjCustomer.ATTNNAME).Equals("") ? "" : OriginalObjCustomer.ATTNNAME).Equals(objCustomer.ATTNNAME));
            Changed = Changed || (!((OriginalObjCustomer.ATTNCONTACTNO).Equals("") ? "" : OriginalObjCustomer.ATTNCONTACTNO).Equals(objCustomer.ATTNCONTACTNO));
            Changed = Changed || (!((OriginalObjCustomer.STATUS).Equals("") ? "" : OriginalObjCustomer.STATUS).Equals(objCustomer.STATUS));
            return Changed;
        }
    }
}