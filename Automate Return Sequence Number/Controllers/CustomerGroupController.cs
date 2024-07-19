using AutoRSN.Models;
using AutoRSN.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoRSN.Controllers
{
    public class CustomerGroupController : Controller
    {
        // GET: CustomerGroup
        public ActionResult Index()
        {
            CustomerRepository cr = new CustomerRepository();

            List<CustomerGroup> custGroup = cr.getCustomerGroupList();

            return View(custGroup);
        }


        [HttpPost]
        public ActionResult Index(System.Web.Mvc.FormCollection frmCollection)
        {

            return View();
        }

        public ActionResult Create()
        {
            CustomerGroup crGroup = new CustomerGroup();
            return View(crGroup); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerGroup customerGroup, FormCollection collection)
        {
            System.Diagnostics.Debug.WriteLine(customerGroup.CUSTOMERNAME);
            try
            {
                CustomerGroupRepository rp = new CustomerGroupRepository();
                if (rp.checkCustomerGroupExist(customerGroup.CUSTOMERNAME))
                {
                    TempData["Message"] = "Customer Group already exist";

                }


                else
                {
                    if(customerGroup.LINECOUNT == null)
                    {
                        TempData["Message"] = "Please specify RSN Line quantity";
                       
                    }

                    else
                    {

                     if (rp.insertCustomerGroup(customerGroup))
                    {
                        TempData["Message"] = "New CustomerGroup Created";
                        return RedirectToAction("Index");
                    }
                       
                    else
                        TempData["Message"] = "Error Inserting Customer Group";

                    }
                   


                }

                   
            }

            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                TempData["Message"] = "Error Inserting Customer Group";
            }

                return View(customerGroup);
        }

        public ActionResult DeleteConfirmation(string customerGroup)
        {
            TempData["Message"] = "Are You Sure You Want To Delete Customer Group = " + customerGroup;
            CustomerGroup custGroup = new CustomerGroup();
            custGroup.CUSTOMERNAME = customerGroup;
            List<CustomerGroup> groupList = new List<CustomerGroup>();
            groupList.Add(custGroup);
            return View(groupList);

        }

        public ActionResult Delete(string customerGroup)
        {
            CustomerGroupRepository rp = new CustomerGroupRepository();
            if(rp.deleteCustomerGroup(customerGroup))
            {
                TempData["Message"] = "Customer Group = " + customerGroup + " Deleted";
                return RedirectToAction("Index");
            }

            else
            {
                TempData["Message"] = "Error Deleting Customer Group.";

            }
            return View();

        }
        public ActionResult ViewAndEdit(string customerGroup)
        {
            System.Diagnostics.Debug.WriteLine("passed viewnedit == >" + customerGroup);
            CustomerRepository cr = new CustomerRepository();
            IEnumerable<Customer> custList = cr.getCustomerListByGroup(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), true, customerGroup);
            return View(custList);
        }

        public ActionResult EditCustomer(string CustomerCode, string MaterialGroup)
        {
            CustomerRepository cr = new CustomerRepository();
            var objCustomer = cr.GetCustomerByCustomerCodeMaterialGroup(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), CustomerCode, MaterialGroup);
            BindEditData(objCustomer);
            return View(objCustomer);
        }


        public ActionResult ShowCustomerDetail(string CustomerCode, string MaterialGroup)
        {
            CustomerRepository cr = new CustomerRepository();
            Customer objCustomer = cr.GetCustomerByCustomerCodeMaterialGroup(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), CustomerCode, MaterialGroup);
            if (objCustomer == null)
            {
                return HttpNotFound();
            }
            return View(objCustomer);

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



    }
}