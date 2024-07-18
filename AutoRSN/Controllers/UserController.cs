using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate;
using AutoRSN.Models;
using AutoRSN.Security;
using AutoRSN.Repositories;
using Newtonsoft.Json;
using System.Diagnostics;

namespace AutoRSN.Controllers
{
    [AuthorizeExceptionHandler]
    public class UserController : Controller
    {
        UserRepository cr = new UserRepository();

        // GET: UserMaster
        public ActionResult Index()
        {
            //return View(Enumerable.Empty<User>());
            return View(cr.GetUserBySearch(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()),""));
        }


        [HttpPost]
        public ActionResult Index(System.Web.Mvc.FormCollection frmCollection)
        {
            string FilterByCCMGCriteria = frmCollection["FilterByCCMG"].ToString();

            return View(cr.GetUserBySearch(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), FilterByCCMGCriteria));
        }

        public ActionResult Details(string FULLNAME, string USERNAME)
        {
            User ObjUser = cr.getUserData(USERNAME);
            if (ObjUser == null)
            {
                return HttpNotFound();
            }
            return View(ObjUser);
        }

        
        public ActionResult DeleteConfirmation(string username)
        {
            TempData["Message"] = "Are You Sure You Want To Delete User  = " + username;
            User user = new User();
            user.USERNAME = username;
            List<User> list = new List<User>();
            list.Add(user);
            return View(list);

        }

        public ActionResult Delete(string user) //used in customer controller
        {
            UserRepository rp = new UserRepository();
            if (rp.deleteUser(user))
            {
                TempData["Message"] = "User  = " + user + " Deleted";
                return RedirectToAction("Index");
            }

            else
            {
                TempData["Message"] = "Error Deleting user.";

            }
            return View();

        }


        public ActionResult Edit(string USERNAME)
        {


            //AutoRSN.Models.User ObjUser = cr.getUserData(USERNAME);

            //ObjUser.CUSTOMERGROUP = new string[ObjUser.PermissionHolderList.Count];
            //ObjUser.PERMISSION = new string[ObjUser.PermissionHolderList.Count];

            //for (int i = 0; i < ObjUser.PermissionHolderList.Count; i++)
            //{
            //    ObjUser.CUSTOMERGROUP[i] = ObjUser.PermissionHolderList.ElementAt(i).Customer;
            //    ObjUser.PERMISSION[i] = ObjUser.PermissionHolderList.ElementAt(i).Permission;

            //    Debug.WriteLine(ObjUser.CUSTOMERGROUP[i] + " : " + ObjUser.PERMISSION[i]);
            //}

           

            // var ObjUser = cr.GetUserByUserName(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), USERNAME);
            User ObjUser = cr.getUserData(USERNAME);
            ObjUser.PermissionHolderList = new List<PermissionHolder>();
            AutoRSN.Repositories.CustomerRepository cr1 = new AutoRSN.Repositories.CustomerRepository();
            List<AutoRSN.Models.CustomerGroup> temp = cr1.getCustomerGroupList();
            ObjUser.CUSTOMERGROUP = new string[temp.Count];
            ObjUser.PERMISSION = new string[temp.Count];

            for (int i = 0; i < temp.Count; i++)
            {
                ObjUser.CUSTOMERGROUP[i] = temp[i].CUSTOMERNAME;
                //ObjUser.PERMISSION[i] =;
            }
               
            BindEditData(ObjUser);
            return View(ObjUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User ObjUser, FormCollection collection)
        {

            try
            {
               
                    if (ObjUser.STATUS == "")
                    {
                        ModelState.AddModelError("StatusMessage", "PLEASE SELECT STATUS");
                        RebindEditData(ObjUser);
                        return View(ObjUser);
                    }
                    else
                    {
                    ObjUser.PermissionHolderList = new List<PermissionHolder>();

                    for (int i = 0; i < ObjUser.CUSTOMERGROUP.Length; i++)
                    {

                        if (ObjUser.CUSTOMERGROUP[i] != null || ObjUser.CUSTOMERGROUP[i] != string.Empty)
                        {
                            try
                            {
                                Debug.WriteLine(ObjUser.CUSTOMERGROUP[i]);
                                Debug.WriteLine(ObjUser.PERMISSION[i]);

                                PermissionHolder permissionHolder = new PermissionHolder();
                                permissionHolder.Customer = ObjUser.CUSTOMERGROUP[i];
                                permissionHolder.Permission = ObjUser.PERMISSION[i];
                                ObjUser.PermissionHolderList.Add(permissionHolder);
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine(ex.Message);
                            }

                        }



                    }

                    User OriginalObjUser = cr.getUserData(ObjUser.USERNAME);
                        ObjUser.MODIFIEDBY = Session["UserId"].ToString().ToUpper();
                       HttpPostedFileBase file = Request.Files["ImageData"];

                        //System.Diagnostics.Debug.WriteLine(Request.Files["ImageData"].ToString());
                       if (file != null)
                           ObjUser.SIGNATURE = file.FileName;

                       // if (IsDataChanged(OriginalObjUser, ObjUser))
                        //{
                            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(ObjUser));
                            cr.UpdateUser(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), ObjUser);
                            //HttpPostedFileBase file = Request.Files["ImageData"];

                            // System.Diagnostics.Debug.WriteLine(cr.ConvertToBytes(file).Length);
                            if (file != null)
                               cr.UploadImageInDataBase(file, ObjUser);

                            TempData["Message"] = " User has been updated successfully.";
                            return RedirectToAction("Index");
                       // }
                        //else
                        //{
                        //    TempData["Message"] = "No changes has been made at the current record.";
                        //    RebindEditData(ObjUser);
                        //    return View(ObjUser);
                        //}
                    }
             
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Sorry, something went wrong. Please contact Administrator. MESSAGE:" + ex.Message;
                return View(ObjUser);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            User ObjUser = new User();
            ObjUser.PermissionHolderList = new List<PermissionHolder>();
            
            AutoRSN.Repositories.CustomerRepository cr = new AutoRSN.Repositories.CustomerRepository();
            List<AutoRSN.Models.CustomerGroup> temp = cr.getCustomerGroupList();
            ObjUser.CUSTOMERGROUP = new string[temp.Count];
            ObjUser.PERMISSION = new string[temp.Count];

            for (int i = 0; i< temp.Count; i++)
            {
                ObjUser.CUSTOMERGROUP[i] = temp[i].CUSTOMERNAME;
                //ObjUser.PERMISSION[i] = new string[temp.Count];


                //PermissionHolder permissionHolder = new PermissionHolder();
                //permissionHolder.Customer = custGroup.CUSTOMERNAME;
               // permissionHolder.Permission = "";
               // ObjUser.PermissionHolderList.Add(permissionHolder);
            }

            ObjUser.ListOfStatus = new List<SelectListItem>()
            {
                new SelectListItem{ Text="-- PLEASE SELECT STATUS --" , Selected = true, Value =""  },
                new SelectListItem { Text="ACTIVE" , Selected = false, Value ="Y"  },
                new SelectListItem { Text="INACTIVE" , Selected =false, Value ="N"  }
            };



            return View(ObjUser);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User ObjUser, FormCollection collection)
        {
            try
            {
              //  if (ModelState.IsValid)
               // {
                    if (ObjUser.STATUS == "")
                    {
                        ModelState.AddModelError("StatusMessage", "PLEASE SELECT STATUS.");
                        RebindCreateData(ObjUser);
                        return View(ObjUser);
                    }
                    else
                    {
                        ObjUser.CREATEDBY = Session["UserId"].ToString().ToUpper();

                        if (cr.IsExistUser(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), ObjUser.FULLNAME, ObjUser.USERNAME) != null)
                        {
                            TempData["Message"] = "Same User code and material group exist in the system.";
                            RebindCreateData(ObjUser);
                            return View(ObjUser);
                        }
                        else
                        {


                        ObjUser.PermissionHolderList = new List<PermissionHolder>();

                        for (int i = 0; i < ObjUser.CUSTOMERGROUP.Length; i++)
                        {
                            
                                if (ObjUser.CUSTOMERGROUP[i] != null || ObjUser.CUSTOMERGROUP[i] != string.Empty)
                            {
                                try
                                {
                                    Debug.WriteLine(ObjUser.CUSTOMERGROUP[i]);
                                    Debug.WriteLine(ObjUser.PERMISSION[i]);

                                    PermissionHolder permissionHolder = new PermissionHolder();
                                    permissionHolder.Customer = ObjUser.CUSTOMERGROUP[i];
                                    permissionHolder.Permission = ObjUser.PERMISSION[i];
                                    ObjUser.PermissionHolderList.Add(permissionHolder);
                                }
                                catch (Exception ex)
                                {
                                    Debug.WriteLine(ex.Message);
                                }

                            }
                              
                            
                           
                        }

                        Debug.WriteLine(JsonConvert.SerializeObject(ObjUser.PermissionHolderList));

                        cr.InsertUser(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), ObjUser);
                            try
                            {
                                HttpPostedFileBase file = Request.Files["ImageData"];
                                    
                               // System.Diagnostics.Debug.WriteLine(cr.ConvertToBytes(file).Length);
                               if (file != null)
                                cr.UploadImageInDataBase(file, ObjUser);

                                TempData["Message"] = "New User has been created.";
                                return RedirectToAction("Index");


                            }
                            catch(Exception ex)
                            {
                                TempData["Message"] = ex.Message;
                                RebindCreateData(ObjUser);
                                return View(ObjUser);
                            }



                            
                        }
                    }
               // }
               // else
               // {
                 //   RebindCreateData(ObjUser);
                //    return View(ObjUser);
               // }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Sorry, something went wrong. Please contact Administrator. MESSAGE:" + ex.Message;
                RebindCreateData(ObjUser);
                return View(ObjUser);
            }
        }

        public void BindEditData(User ObjUser)
        {
            ModelState.Remove("ListOfStatus");
            ObjUser.ListOfStatus = new List<SelectListItem>()
                {
                new SelectListItem{ Text="-- PLEASE SELECT STATUS --" , Selected = true, Value =""  },
                new SelectListItem { Text="ACTIVE" , Selected =false, Value ="Y"  },
                new SelectListItem { Text="INACTIVE" , Selected =false, Value ="N"  }
                };

            ObjUser.STATUS = ObjUser.STATUS;
        }

        public void RebindEditData(User ObjUser)
        {
            ModelState.Remove("ListOfStatus");

            ObjUser.ListOfStatus = new List<SelectListItem>()
                {
                new SelectListItem{ Text="-- PLEASE SELECT STATUS --" , Selected = true, Value =""  },
                new SelectListItem { Text="ACTIVE" , Selected =false, Value ="Y"  },
                new SelectListItem { Text="INACTIVE" , Selected =false, Value ="N"  }
                };
        }

        public void RebindCreateData(User ObjUser)
        {
            ModelState.Remove("ListOfStatus");

            ObjUser.ListOfStatus = new List<SelectListItem>()
                {
                new SelectListItem{ Text="-- PLEASE SELECT STATUS --" , Selected = false, Value =""  },
                new SelectListItem { Text="ACTIVE" , Selected =true, Value ="Y"  },
                new SelectListItem { Text="INACTIVE" , Selected =false, Value ="N"  }
                };
        }

        private bool IsDataChanged(User OriginalObjUser, User ObjUser)
        {
            bool Changed = false;

            try
            {

                System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(OriginalObjUser));
                System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(ObjUser));

                if (!OriginalObjUser.FULLNAME.Equals(ObjUser.FULLNAME))
                    return true;
                if (!OriginalObjUser.EMAIL.Equals(ObjUser.EMAIL))
                    return true;
                if (!OriginalObjUser.LVL.Equals(ObjUser.LVL))
                    return true;
               // if (!JsonConvert.SerializeObject(OriginalObjUser.CUSTOMERGROUP).Equals(JsonConvert.SerializeObject(ObjUser.CUSTOMERGROUP)))
                   // return true;
                if (!JsonConvert.SerializeObject(OriginalObjUser.PERMISSION).Equals(JsonConvert.SerializeObject(ObjUser.PERMISSION)))
                    return true;
                if (!OriginalObjUser.DEPT.Equals(ObjUser.DEPT))
                    return true;
                if (!OriginalObjUser.STATUS.Equals(ObjUser.STATUS))
                    return true;
                if (!OriginalObjUser.SUPERVISOR.Equals(ObjUser.SUPERVISOR))
                    return true;
                 if(ObjUser.SIGNATURE != null)
                    return true;

            }

            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            
            return Changed;
        }
    }
}