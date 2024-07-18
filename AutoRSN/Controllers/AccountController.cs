using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using System.Threading;
using System.DirectoryServices;
using AutoRSN.Models;
using AutoRSN.Repositories.Abstract;
using AutoRSN.Repositories;
using System.Web.Security;
using System.Net;
using AutoRSN.Utility;
using Newtonsoft.Json;
using System.Collections;
using System.Web.SessionState;
using System.Reflection;
using System.Diagnostics;

namespace AutoRSN.Controllers
{
    public class AccountController : Controller
    {
        IServerRepository objIServerData;
       
        public AccountController()
        {
            objIServerData = new ServerRepository();
        }

        // GET: /Account/z
        [HttpGet]
        [AllowAnonymous]
        public ActionResult login()
        {

           // WebClient client = new WebClient();
            //string reply = client.DownloadString("http://www.google.com");


           // Declare the return object
           Account objAccount = new Account();

            //List<Server> ListOfServer = new List<Server>();
            //ListOfServer = new List<Server>()
            //{
            //    new Server { SERVERID = 0, SERVER ="-- PLEASE SELECT SERVER --"}
            //};
            
            //foreach (var item in objIServerData.GetServer(true))
            //{
            //    Server sm = new Server();
            //    sm.SERVERID = item.SERVERID;
            //    sm.SERVER = item.SERVER;
            //    ListOfServer.Add(sm);
            //}

            //objAccount.LISTSERVER = ListOfServer;

            
           // return View(objAccount);
            return View();
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult login(Account AccountData)
        //{

        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            if (AccountData.SERVERID == 0)
        //            {
        //                ModelState.AddModelError("SERVERMESSAGE", "PLEASE SELECT SERVER");
        //                Method(AccountData);
        //                return View(AccountData);
        //            }
        //            else
        //            {
        //                UserRepository usr = new UserRepository();
        //                User user = usr.getUserData(AccountData);

        //                if (user != null)
        //                {
        //                    System.Diagnostics.Debug.WriteLine("success login");
        //                    Session["UserLevel"] = user.LVL;
        //                    Session["ServerId"] = Convert.ToString(user.SERVERID);
        //                    Session["UserId"] = user.USERNAME;
        //                    Session["UserPass"] = user.PASSWORD;
        //                    Session["Name"] = user.FULLNAME;
        //                    Session["Email"] = user.EMAIL;
        //                   FormsAuthentication.SetAuthCookie(user.FULLNAME, true);
        //                    System.Diagnostics.Debug.WriteLine("Cookie set==>",true);
        //                    return Redirect("~/ItemNotes/Index");

        //                }

        //                else {
        //                    Method(AccountData);
        //                    return View(AccountData);

        //                }




        //            }

        //        }
        //        else
        //        {
        //            Method(AccountData);
        //            return View(AccountData);

        //        }
        //    }
        //    catch(Exception e)
        //    {
        //        System.Diagnostics.Debug.WriteLine("Error login.." + e.InnerException);
        //        ModelState.AddModelError("Error", "Logon failure: unknown username or bad password. " + e.Message);
        //        Method(AccountData);
        //        return View(AccountData);

        //    }

        //    }

            public ActionResult CloseSession(string windowsid)
        {

           // Debug.WriteLine(windowsid);
            //StaticSession.sessionList.Remove(windowsid);
            return Redirect("~");
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
       // [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult login( System.Web.Mvc.FormCollection frmCollection)
        {
            Account AccountData = new Account();
            AccountData.SERVERID = 1;
            AccountData.USERNAME = frmCollection["username"].ToString();
            AccountData.PASSWORD = frmCollection["password"].ToString() ;
            Debug.WriteLine(frmCollection["username"]);
            Debug.WriteLine(frmCollection["password"]);
            try
            {
                if (ModelState.IsValid)
                {
                    if (AccountData.SERVERID == 0)
                    {
                        ModelState.AddModelError("SERVERMESSAGE", "PLEASE SELECT SERVER");
                        Method(AccountData);
                        return View(AccountData);
                    }
                    else
                    {

                        System.Diagnostics.Debug.WriteLine(AccountData.USERNAME + AccountData.PASSWORD);
                        System.Diagnostics.Debug.WriteLine("");
                        DirectoryEntry SearchRoot = new DirectoryEntry("LDAP://ASIA.AD.CELESTICA.COM", AccountData.USERNAME, AccountData.PASSWORD);
                        // PropertyCollection props = SearchRoot.Properties;
                       
                        DirectorySearcher Searcher = new DirectorySearcher(SearchRoot);
                        Searcher.PropertiesToLoad.Add("cn");
                        Searcher.PropertiesToLoad.Add("mail");
                        Searcher.Filter = "(sAMAccountName=" + AccountData.USERNAME + ")";

                        SearchResult result = Searcher.FindOne();

                        if (result != null)
                        {

                            Debug.WriteLine("result is not nulll ...!!!!");
                           // AccountRepository accRepo = new AccountRepository();

                            UserRepository usr = new UserRepository();
                            User user = usr.getUserData(AccountData.USERNAME.Trim());

                            if (user != null)
                                {

                                //if (StaticSession.sessionList.Contains(AccountData.USERNAME))
                                //{
                                //    ModelState.AddModelError("Error", "Logon failure: You have another active session..");
                                //    Method(AccountData);
                                //    return View(AccountData);
                                //}

                                // else
                                // {
                               
                                    Session["UserLevel"] = user.LVL;
                                    Session["ServerId"] = AccountData.SERVERID.ToString().ToUpper();
                                    Session["UserId"] = AccountData.USERNAME;
                                    System.Web.HttpContext.Current.Session["UserId"] = AccountData.USERNAME;
                                    Session["UserPass"] = AccountData.PASSWORD;
                                //Session["Name"] = result.GetDirectoryEntry().Properties["cn"].Value.ToString().Split(':')[0].ToUpper();
                                Session["NameFromAD"] = result.GetDirectoryEntry().Properties["cn"].Value.ToString().Split(':')[0].ToUpper();
                                Session["Name"] = user.FULLNAME;
                                Session["Email"] = result.GetDirectoryEntry().Properties["mail"].Value.ToString();
                                   // Session["CustomerGroup"] = JsonConvert.SerializeObject(user.CUSTOMERGROUP);
                                    Session["PermissionList"] = JsonConvert.SerializeObject(user.PermissionHolderList);
                                    Session["Level"] = user.LVL; //0- admin, 1-user
                                    FormsAuthentication.SetAuthCookie(result.GetDirectoryEntry().Properties["cn"].Value.ToString().Split(':')[0].ToUpper(), true);

                                Debug.WriteLine(Session["UserLevel"]);
                                Debug.WriteLine(Session["ServerId"]);
                                Debug.WriteLine(Session["UserId"]);
                                Debug.WriteLine(Session["UserId"]);
                                Debug.WriteLine(Session["UserPass"]);
                                Debug.WriteLine(Session["Name"]);
                                Debug.WriteLine(Session["Email"]);
                                //Debug.WriteLine(Session["CustomerGroup"]);
                                Debug.WriteLine(Session["PermissionList"]);
                                Debug.WriteLine(Session["Level"]);
                                //StaticSession.sessionList.Add(AccountData.USERNAME);

                                //foreach(string user1 in getOnlineUsers())
                                // {
                                //   System.Diagnostics.Debug.WriteLine(user1);
                                // }
                                //for (int i = 0; i < Session.Count; i++)
                                //{
                                //    string val = "Session[" + i.ToString() + "]: Name = " + Session.Keys[i] + "; Value = " + Session[i].ToString();
                                //    System.Diagnostics.Debug.WriteLine(val);

                                //}

                                // startTimer(AccountData.USERNAME);
                                return Redirect("~/Openso/Index");
                               // }
                                
                            }
                            else {
                                //Method(AccountData);
                                return View();

                            }

                        }
                        else
                        {

                            Debug.WriteLine("result is nulll ...!!!!");
                           // Method(AccountData);
                            return View();
                        }
                    }
                }
                else
                {

                   // Debug.WriteLine("result is nulll ...!!!!");
                    //Method(AccountData);
                    return View();
                }
            }
            catch
            {
                ModelState.AddModelError("Error", "Logon failure: unknown username or bad password.");
               // Method(AccountData);
                return View();
            }
        }

        private List<String> getOnlineUsers()
        {
            List<String> activeSessions = new List<String>();
            object obj = typeof(HttpRuntime).GetProperty("CacheInternal", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null, null);
            object[] obj2 = (object[])obj.GetType().GetField("_caches", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(obj);
            for (int i = 0; i < obj2.Length; i++)
            {
                Hashtable c2 = (Hashtable)obj2[i].GetType().GetField("_entries", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(obj2[i]);
                foreach (DictionaryEntry entry in c2)
                {
                    object o1 = entry.Value.GetType().GetProperty("Value", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(entry.Value, null);
                    if (o1.GetType().ToString() == "System.Web.SessionState.InProcSessionState")
                    {
                        SessionStateItemCollection sess = (SessionStateItemCollection)o1.GetType().GetField("_sessionItems", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(o1);
                        if (sess != null)
                        {
                            if (sess["UserId"] != null)
                            {
                                activeSessions.Add(sess["UserId"].ToString());
                            }
                        }
                    }
                }
            }
            return activeSessions;
        }

        public void startTimer(string user)
        {
            Thread thread = new Thread(() => timerThread(user));
            thread.Start();
        }

        public void timerThread(string user)
        {
             System.Diagnostics.Debug.WriteLine(Session["UserId"]);
     
                while (true)
                {
                    if (Session["UserId"] == null)
                      {
                        break;
                    }

                    else
                    {
                            Thread.Sleep(1000);
                    }
                    
                
                   }
            //StaticSession.sessionList.Remove(user);

        }


        public ActionResult signout()
        {

            FormsAuthentication.SignOut();
            return RedirectToAction("login", "Account");
        }


        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public void Method(Account objAccount)
        {
            ModelState.Remove("LISTSERVER");
            List<Server> listServer = new List<Server>();
            listServer = new List<Server>() { new Server { SERVERID = 0, SERVER = "-- PLEASE SELECT SERVER --" } };

            foreach (var item in objIServerData.GetServer(true))
            {
                Server sm = new Server();
                sm.SERVERID = item.SERVERID;
                sm.SERVER = item.SERVER;
                listServer.Add(sm);
            }


            objAccount.LISTSERVER = listServer;
        }
    }
}