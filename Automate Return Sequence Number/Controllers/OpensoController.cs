using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate;
using AutoRSN.Models;
using AutoRSN.Security;
using AutoRSN.Repositories;
using System.Web.Script.Serialization;
using System.Net;
using System.Net.Mail;
using AutoRSN.Services;
using Newtonsoft.Json;
using Oracle.DataAccess.Client;
using System.Data;
using ClosedXML.Excel;
using System.IO;
using AutoRSN.Utility;
using System.Diagnostics;
using System.Globalization;
using OfficeOpenXml;
using System.Text;
using System.Threading;
using System.Web.Services;

namespace AutoRSN.Controllers
{
    [AuthorizeExceptionHandler]
    public class OpensoController : Controller
    {


        OpensoRepository cr = new OpensoRepository();

        // GET: CustomerMaster
        public ActionResult Index()
        {
            string currentUrl = AutoRSN.Utility.Util.getWebUrl();
            System.Diagnostics.Debug.WriteLine(currentUrl);

            //Response.Write($"<Script> alert('{cr.getCurrentRSNNO()}'); </Script>");
            return View(Enumerable.Empty<Openso>());
        }

        [HttpPost]
        public ActionResult Index(System.Web.Mvc.FormCollection frmCollection)
        {
            
            string FilterByCCMGCriteria = frmCollection["FilterByCCMG"].ToString().Trim();
            string FilterByCCMG1Criteria = frmCollection["FilterByCCMG1"].ToString().Trim();
            string FilterByCCMG2Criteria = frmCollection["FilterByCCMG2"].ToString().Trim();
            string FilterByCCMG3Criteria = frmCollection["FilterByCCMG3"].ToString().Trim();
            string FilterByCCMG4Criteria = frmCollection["FilterByCCMG4"].ToString().Trim();
            string FilterByCCMG5Criteria = frmCollection["FilterByCCMG5"].ToString().Trim();
            string FilterByCCMG6Criteria = frmCollection["FilterByCCMG6"].ToString().Trim();
            string groupName = frmCollection["groupName"].ToString();
            Session["SelectedCust"] = frmCollection["groupName"].ToString();

            System.Diagnostics.Debug.WriteLine(FilterByCCMGCriteria);
           
            return View(cr.GetOpensoBySearch(groupName, 1, FilterByCCMGCriteria, FilterByCCMG1Criteria, FilterByCCMG2Criteria, FilterByCCMG3Criteria, FilterByCCMG4Criteria, FilterByCCMG5Criteria, FilterByCCMG6Criteria));
        }

        public void checkItemDup()
        {


        }

        [HttpGet]
        public string EditQuantityCall(int quantity, string rsnno, string uniqel, string user, string email)
        {
            string result = "";
            using (OracleConnection conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=rsn;Password=o5rsntcp;"))
            {
                conn.Open();
                int oriQty = 0;
                string so = "";
                string soline = "";

               


                OracleCommand chkshipcmd = conn.CreateCommand();
                chkshipcmd.CommandText = $"select shipstatus from z_t_rsn_final where uniqel = '{uniqel}'";
                chkshipcmd.CommandType = CommandType.Text;
                OracleDataReader chkreader = chkshipcmd.ExecuteReader();

                while (chkreader.Read())
                {
                    if(chkreader.GetValue(0).ToString().Equals("YES"))
                    {
                        result = "false:You cannot update quantity for RSN that has been shipped.";
                        //chkshipcmd.Dispose();
                        conn.Dispose();
                        conn.Close();

                        return result; 
                    }
                }



                OracleCommand originalQty = new OracleCommand();
                originalQty.Connection = conn;
                originalQty.CommandType = CommandType.Text;
                originalQty.CommandText = string.Format("select shipqty,vbeln,posnr from z_t_rsn_final where rsnno = '{0}' and uniqel = '{1}'", rsnno, uniqel);
                OracleDataReader oriqtyreader = originalQty.ExecuteReader();

                while (oriqtyreader.Read())
                {
                    oriQty = Convert.ToInt32(oriqtyreader.GetValue(0).ToString());
                    so = Convert.ToString(oriqtyreader.GetValue(1).ToString());
                    soline = Convert.ToString(oriqtyreader.GetValue(2).ToString());
                }

                originalQty.Dispose();
                
                   ///////////////////////

                   //  if (quantity > oriQty)
                   //  {

                   // HttpContext.Current.Response.Write("false");
                   //      result = "false";
                   //return;

                   //  }

                   // else
                   //{

                   OracleCommand updateQty = new OracleCommand();
                    updateQty.Connection = conn;
                    updateQty.CommandType = CommandType.Text;
                    updateQty.CommandText = string.Format("update z_t_rsn_final set shipqty={0},MODIFIEDBY = '{3}',MODIFIEDON = sysdate where rsnno = '{1}' and uniqel = '{2}'", quantity, rsnno, uniqel, user);
                    updateQty.ExecuteNonQuery();

                    new Thread(() =>
                    {
                        //new MailHandler().sendMailToDataEntry(rsnno, so, soline, user, email, oriQty, quantity);

                    }).Start();

                    new Thread(() =>
                    {
                        //new MailHandler().sendMailToSelf(rsnno, so, soline, user, email, oriQty, quantity);

                    }).Start();

                    //HttpContext.Current.Response.Write("true");
                    result  = "true:OK";
                //return;

                // }
                conn.Dispose();
                conn.Close();
            }

            return result;
        }


        [HttpGet]
        public ActionResult DELETESO(string uniqueid, string rsnno)
        {
            cr.deleteSO(uniqueid, rsnno);
            string groupName = Session["SelectedCust"].ToString();
            //Session["SelectedCust"] = frmCollection["groupName"].ToString();
            return View(cr.GetOpensoBySearch(groupName, 1, "", "","", "", "", ""));
        }
        //public ActionResult GenerateRSN(string uniqueIDstr, string identifier)
        //{ // for 1 rsn each item
        //    List<Openso> list = new List<Openso>();
        //     List<List<Openso>> masterList = new List<List<Openso>>();
        //    List<List<Openso>> masterList2 = new List<List<Openso>>(); // for check shipto
        //    List<List<Openso>> masterList3 = new List<List<Openso>>(); // for check licen
        //    List<List<Openso>> realMasterList = new List<List<Openso>>();
        //    EmailServices sender = new EmailServices();

            //   // OpensoRepository cr = new OpensoRepository();

            //       string[] uniqueIDs = uniqueIDstr.Split(':');

            //        string RSNNO; ///= cr.getRSNNO();
            //        List<Openso> mainList = new List<Openso>();
            //        List<ClusterSo> clusterSoList = new List<ClusterSo>();

            //        List<string> listOfCustGroup = new List<string>();
            //        List<string> listOfShipTo = new List<string>();
            //        List<string> listOfLicence = new List<string>();
            //        List<Openso> cluster = new List<Openso>();  
            //        Openso tempSO = new Openso();
            //        int counter = 0;
            //        System.Diagnostics.Debug.WriteLine("ids length==>" + uniqueIDs.Length);

            //        for (int y = 0; y < uniqueIDs.Length; y++)
            //        {
            //        System.Diagnostics.Debug.WriteLine(uniqueIDs[y]);
            //        tempSO = cr.getSingleSO(Convert.ToInt32(Session["SERVERID"].ToString().ToUpper()), uniqueIDs[y]);


            //            ClusterSo clusterso1 = new ClusterSo();
            //            clusterso1.custGroupName = cr.getCustomerGroup(tempSO.KUNNR1);
            //            clusterso1.kunnr2 = tempSO.KUNNR2;
            //            clusterso1.exportControl = tempSO.EXPORTCONTROL;
            //            clusterso1.auart = tempSO.AUART;
            //            clusterso1.rsnno = identifier + cr.getRSNNO();
            //            clusterso1.link = @"http://myodcweb/AutoRSN/RSNPrintOut.aspx?reportNum=0&RSNNO=" + clusterso1.rsnno;
            //            clusterso1.remarks = tempSO.REMARKS;
            //            System.Diagnostics.Debug.WriteLine(clusterso1.rsnno);
            //            cr.generateRSNNO(Convert.ToInt32(Session["SERVERID"].ToString().ToUpper()), tempSO.UNIQEL, clusterso1.rsnno);
            //            tempSO.RSNNO = clusterso1.rsnno;
            //            clusterso1.listItem.Add(tempSO);
            //            clusterSoList.Add(clusterso1);


            //        masterList.Add(clusterso1.listItem);
            //        }

            //    System.Diagnostics.Debug.WriteLine("size:==> " + masterList.Count);

            //    string masterlistStr = JsonConvert.SerializeObject(masterList);
            //    System.Diagnostics.Debug.WriteLine(masterList);
            //    ExportExcel(masterList);
            //   // string handlerUrl = Util.getWebUrl() + "Handler/ExportExcel.ashx?uniqueIDstr=" + uniqueIDstr;
            //   // string response = (new WebClient()).DownloadString(handlerUrl);

            //    return View(masterList);

            //}


          public ActionResult Upload()
           {

            var file = Request.Files[0];
            if (file != null && file.ContentLength > 0)
            {
                if (cr.getCurrentRSNSeq().Length > 5) //if next 500 rsn sequence is > 5 then reset to 1 back
                {
                    cr.resetRSNSeq();
                }

                FileInfo fi = new FileInfo(file.FileName);
                string ext = fi.Extension;

                if (ext.Contains(".txt"))
                {

                    OpensoRepository cr = new OpensoRepository();
                    List<Openso> objsoList = new List<Openso>();

                    StreamReader sr = new StreamReader(file.InputStream);
                    bool skip = true;
                    while (sr.Peek() >= 0)
                    {
                        string[] datas = sr.ReadLine().Split('\t');
                        Debug.WriteLine("first row = " + datas[0]);
                        if (datas[0].ToUpper().Contains("SALES ORDER"))
                        {


                        }

                        else if(datas[0].Trim().Length>0)
                        {

                            Debug.WriteLine(string.Join(",", datas));
                            Openso objOpenso = new Openso();
                            for (int column = 0; column < datas.Length; column++)
                            {
                                switch (column)
                                {
                                    case 0:
                                        objOpenso.VBELN = datas[column];
                                        break;

                                    case 1:
                                        objOpenso.POSNR = datas[column];
                                        break;

                                    case 2:
                                        objOpenso.KUNNR1 = datas[column].ToString().ToUpper().Trim();
                                        break;

                                    case 3:
                                        objOpenso.KUNNR2 = datas[column].ToString().ToUpper().Trim();
                                        break;

                                    case 4:
                                        objOpenso.MATNR = datas[column];
                                        break;

                                    case 5:
                                        objOpenso.KDMAT = datas[column];
                                        break;

                                    case 6:
                                        objOpenso.ARKTX = datas[column];
                                        break;

                                    case 7:
                                        objOpenso.BSTNK = datas[column];
                                        break;

                                    case 8:
                                        objOpenso.POSEX = datas[column];
                                        break;


                                    case 9:
                                        objOpenso.AUART = datas[column];
                                        break;


                                    case 10:
                                        objOpenso.CCO = datas[column];
                                        break;


                                    case 11:
                                        objOpenso.SHIPDATE = datas[column];
                                        try
                                        {
                                            Debug.WriteLine(datas[column]);
                                            DateTime shipdate = DateTime.ParseExact(datas[column], "MM/dd/yyyy", CultureInfo.InvariantCulture);
                                            objOpenso.EDATU = shipdate.ToString("yyyyMMdd");
                                            objOpenso.SHIPDATE = shipdate.ToString("dd/MM/yyyy");
                                            //objOpenso.SHIPDATE = shipdate.ToString("yyyyMMdd");

                                        }
                                        catch (Exception ex)
                                        {
                                            TempData["Message"] = "Manual upload file contains incorrect date forma, date must be MM/dd/yyyy format.." + ex.Message;
                                            return RedirectToAction("index");
                                        }
                                        break;


                                    case 12:
                                        objOpenso.REMARKS = datas[column];
                                        break;


                                    case 13:
                                        objOpenso.SHIPQTY = Convert.ToInt32(datas[column]);
                                        break;


                                    case 14:
                                        objOpenso.NETPR = datas[column].Replace("\"","").Replace(",","");
                                        break;


                                }

                            }

                            //objOpenso.RSNNO = "0";
                            objOpenso.CURRENCY = "USD";
                            objOpenso.REPORTID = "0";
                            objOpenso.CREATEDBY = Session["Name"].ToString();
                            objOpenso.CREATEDBYID = Session["UserId"].ToString();
                            objOpenso.CREATEDON = DateTime.Now;
                            //objOpenso.GENERATEDON = DateTime.Now.ToString();
                            // objOpenso.MODIFIEDBY = Session["Name"].ToString();
                            objOpenso.OPENQTY = objOpenso.SHIPQTY.ToString();
                            objOpenso.STATUS = "Y";
                            objsoList.Add(objOpenso);

                        }


                    } // while peek


                    //if ((cr.getCurrentRSNNO() + objsoList.Count )  > 10) //reset rsn no test
                    //{

                    //}

                    StringBuilder uniqueStr = new StringBuilder();
                    StringBuilder rsnnoStr = new StringBuilder();
                    int count = 0;

                    ClusterSo prevCluster = null;

                    List<Openso> listAll = new List<Openso>();

                    List<List<Openso>> listOfList = new List<List<Openso>>();
                    List<ClusterSo> clusterCollection = new List<ClusterSo>();

                    foreach (Openso openso in objsoList)
                    {

                        ClusterSo current = new ClusterSo();
                        current.kunnr1 = openso.KUNNR1;
                        current.kunnr2 = openso.KUNNR2;
                        current.auart = openso.AUART;
                        current.shipdate = openso.SHIPDATE;
                        current.remarks = openso.REMARKS;



                        bool same = false;
                        foreach (ClusterSo cluster in clusterCollection)
                        {


                            Debug.WriteLine("current = " + current.kunnr1);
                            Debug.WriteLine("current =" + current.kunnr2);
                            Debug.WriteLine("current =" + current.auart);
                            Debug.WriteLine("current =" + current.shipdate);
                            Debug.WriteLine("current =" + current.remarks);

                            Debug.WriteLine("cluster" + cluster.kunnr1);
                            Debug.WriteLine("cluster" + cluster.kunnr2);
                            Debug.WriteLine("cluster" + cluster.auart);
                            Debug.WriteLine("cluster" + cluster.shipdate);
                            Debug.WriteLine("cluster" + cluster.remarks);


                            if (cluster.kunnr1.Equals(current.kunnr1) && cluster.kunnr2.Equals(cluster.kunnr2) && cluster.auart.Equals(current.auart) && cluster.shipdate.Equals(current.shipdate) && cluster.remarks.Equals(current.remarks) && cluster.listItem.Count < 3)
                            {

                                openso.UNIQEL = (openso.MATNR + openso.VBELN + openso.POSNR + openso.BSTNK + openso.POSEX).ToString().Replace(" ", "").Trim() + cr.getID();
                                openso.RSNNO = cluster.rsnno;

                                cluster.listItem.Add(openso);
                                listAll.Add(openso);
                                same = true;
                                break;
                            }
                        }

                        if (!same)
                        {
                            openso.UNIQEL = (openso.MATNR + openso.VBELN + openso.POSNR + openso.BSTNK + openso.POSEX).ToString().Replace(" ", "").Trim() + cr.getID();
                            openso.RSNNO = (2).ToString() + cr.getRSNNO();

                            current.rsnno = openso.RSNNO;
                            current.listItem.Add(openso);

                            listAll.Add(openso);
                            clusterCollection.Add(current);

                        }




                        // openso.RSNNO = (2).ToString() + cr.getRSNNO();
                        //cr.updateShipDate(openso.RSNNO, openso.SHIPDATE);
                        cr.InsertOpenso(1, openso);

                        //if (count == objsoList.Count)
                        //    uniqueStr.Add(openso.UNIQEL);
                        //else
                        //if (count < (objsoList.Count - 1))
                        //{

                        //    uniqueStr.Append(openso.UNIQEL + ':');
                        //    rsnnoStr.Append(openso.RSNNO + ':');
                        //}

                        //else
                        //{
                        //    uniqueStr.Append(openso.UNIQEL);
                        //    rsnnoStr.Append(openso.RSNNO);
                        //}

                        count++;

                    } // end of openso loop
                       foreach(ClusterSo cluster in clusterCollection)
                        {
                            listOfList.Add(cluster.listItem);
                        }

            
                        ExportExcel(listOfList);
                        return View(clusterCollection);
                }

                else
                {
                    Response.Write("<Script> alert('Wrong File, Please upload a .txt (Tab delimited) file'); </Script>");
                }
            }

             return RedirectToAction("index");
            
            
            }


        public ActionResult GenerateManualRSN(string uniqstr, string rsnnoStr ) // for batch manual rsn
        {
            if (cr.getCurrentRSNSeq().Length > 5) //if next 500 rsn sequence is > 5 then reset to 1 back
            {
                cr.resetRSNSeq();
            }

            string[] uniques = uniqstr.Split(':');
            string[] rsnno = rsnnoStr.Split(':');
            var conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=rsn;Password=o5rsntcp;");
            conn.Open();

           AutoRSN.Repositories.WebService gtswebservice = new AutoRSN.Repositories.WebService();
            OracleCommand updateRSNFinal12 = conn.CreateCommand();

            List<List<Openso>> masterList = new List<List<Openso>>();

           
            for (int a = 0; a< uniques.Length;a++)
            {
                List<ClusterSo> clusterSoList = new List<ClusterSo>();
                ClusterSo clusterso1 = new ClusterSo();

                Openso openso = cr.getSingleSObyRSN(1, uniques[a], rsnno[a]);
              //  openso.RSNNO = (2).ToString() + cr.getRSNNO(); 
               // cr.generateRSNNO(Convert.ToInt32(Session["SERVERID"].ToString().ToUpper()), openso.UNIQEL, openso.RSNNO);
                //cr.updateShipDate(openso.RSNNO, openso.SHIPDATE);

                Debug.WriteLine(JsonConvert.SerializeObject(openso));
                //clusterso1.custGroupName = cr.getCustomerGroup(openso.KUNNR1);
                //clusterso1.kunnr2 = openso.KUNNR2;
                //clusterso1.exportControl = openso.EXPORTCONTROL;
                //clusterso1.auart = openso.AUART;
               // clusterso1.rsnno = (2).ToString() + cr.getRSNNO();
                //clusterso1.link = @"http://myodcweb/AutoRSN/RSNPrintOut.aspx?reportNum=0&RSNNO=" + clusterso1.rsnno;

               

                if (gtswebservice.checkGTS(openso.MATNR))
                {
                    Debug.WriteLine("gtsmaster yes");
                    openso.EXPORTCONTROL = "Y";
                    updateRSNFinal12.CommandText = string.Format("update z_t_rsn_final set exportcontrol='Y' WHERE MATNR = '{0}'", openso.MATNR);
                }
                else
                {
                    Debug.WriteLine("gtsmaster no");
                    openso.EXPORTCONTROL = "N";
                    updateRSNFinal12.CommandText = string.Format("update z_t_rsn_final set exportcontrol='N' WHERE MATNR = '{0}'", openso.MATNR);
                }

                updateRSNFinal12.CommandType = CommandType.Text;
                updateRSNFinal12.ExecuteNonQuery();


               // cr.generateRSNNO(Convert.ToInt32(Session["SERVERID"].ToString().ToUpper()), openso.UNIQEL, openso.RSNNO);

                masterList.Add(new List<Openso> {openso});

                //clusterso1.listItem.Add(openso);

                //clusterSoList.Add(clusterso1);
               // convertToPDF("http://myodcweb/AutoRSN/RSNPrintOut.aspx?reportNum=0&RSNNO=" + openso.);

            }

            string masterlistStr = JsonConvert.SerializeObject(masterList);
            System.Diagnostics.Debug.WriteLine(masterList);
            ExportExcel(masterList);
            TempData["Message"] = "Manual RSN successfully created ..";

          
            return View(masterList);
            //return RedirectToAction("/GenerateRSN?uniqueIDstr=" + objOpenso.UNIQEL + "&identifier=2");
            //return RedirectToAction("GenerateRSN", new { uniqueIDstr = objOpenso.UNIQEL, identifier = 2, shipmentDate = objOpenso.EDATU, remarks = objOpenso.REMARKS });

        }

        public ActionResult GenerateRSN(string uniqueIDstr, string identifier,string shipmentDate, string remarks , string forwarder , string accountno , string billto , string servicetype)
        { //generate by grouping


            if (cr.getCurrentRSNSeq().Length > 5) //if next 500 rsn sequence is > 5 then reset to 1 back
            {
                cr.resetRSNSeq();
            }

            string host = HttpContext.Request.Url.Host;
            int port = HttpContext.Request.Url.Port;
            string absolutepath = HttpContext.Request.Url.AbsolutePath;

            Debug.WriteLine(remarks);
            CustomerGroupRepository custGRepo = new CustomerGroupRepository();

            List<Openso> list = new List<Openso>();
            List<List<Openso>> masterList = new List<List<Openso>>();
            List<List<Openso>> masterList2 = new List<List<Openso>>(); // for check shipto
            List<List<Openso>> masterList3 = new List<List<Openso>>(); // for check licen
            List<List<Openso>> realMasterList = new List<List<Openso>>();
            EmailServices sender = new EmailServices();

            // OpensoRepository cr = new OpensoRepository();

            string[] uniqueIDs = uniqueIDstr.Split(':');

            string RSNNO; ///= cr.getRSNNO();
            List<Openso> mainList = new List<Openso>();
            List<ClusterSo> clusterSoList = new List<ClusterSo>();

           // List<string> listOfCustGroup = new List<string>();
           // List<string> listOfShipTo = new List<string>();
                    
            List<Openso> cluster = new List<Openso>();
            Openso tempSO = new Openso();
            int counter = 0;
            System.Diagnostics.Debug.WriteLine("ids length==>" + uniqueIDs.Length);
            DateTime shipdate = new DateTime();
            try
            {
              shipdate = DateTime.ParseExact(shipmentDate, "yyyyMMdd", CultureInfo.InvariantCulture);
            }
            catch(Exception ex)
            {
                shipdate = DateTime.ParseExact(shipmentDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            }
            for (int y = 0; y < uniqueIDs.Length; y++)
            {
                System.Diagnostics.Debug.WriteLine(uniqueIDs[y]);
                tempSO = cr.getSingleSO(Convert.ToInt32(Session["SERVERID"].ToString().ToUpper()), uniqueIDs[y]);
              
                
                // System.Diagnostics.Debug.WriteLine(uniqueIDs[y]);
                try
                {
                    string tempCustGroup = cr.getCustomerGroup(tempSO.KUNNR1);
                    System.Diagnostics.Debug.WriteLine(tempCustGroup);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(uniqueIDs[y] + " is " + ex.Message);
                }


                bool isSame = false;
                string customerGroupName = cr.getCustomerGroup(tempSO.KUNNR1);
                //System.Diagnostics.Debug.WriteLine(tempSO.REMARKS);
                if (tempSO.REMARKS == null || tempSO.REMARKS == "")
                {
                    foreach (ClusterSo clusterso in clusterSoList)
                    {

                        if (clusterso.remarks == null && clusterso.custGroupName.Equals(cr.getCustomerGroup(tempSO.KUNNR1)) && clusterso.kunnr2.Equals(tempSO.KUNNR2) && clusterso.auart.Equals(tempSO.AUART) && clusterso.exportControl.Equals(tempSO.EXPORTCONTROL))
                        {
                            if (clusterso.listItem.Count < custGRepo.getCustomerGroup(customerGroupName).LINECOUNT)
                            {
                                isSame = true;
                                tempSO.RSNNO = clusterso.rsnno;
                                cr.generateRSNNO(Convert.ToInt32(Session["SERVERID"].ToString().ToUpper()), tempSO.UNIQEL, clusterso.rsnno);
                                cr.updateShipDate(clusterso.rsnno, shipdate.ToString("dd/MM/yyyy"));
                                tempSO.REMARKS = remarks;
                                clusterso.listItem.Add(tempSO);
                                clusterso.link = $"http://{host}:{port}{Request.ApplicationPath}/AutoRSN/RSNPrintOut.aspx?reportNum=0&RSNNO=" + clusterso.rsnno;
                                break;
                            }

                        }
                  
                    }

                    if (!isSame)
                    {

                        ClusterSo clusterso1 = new ClusterSo();
                        clusterso1.custGroupName = cr.getCustomerGroup(tempSO.KUNNR1);
                        clusterso1.kunnr2 = tempSO.KUNNR2;
                        clusterso1.exportControl = tempSO.EXPORTCONTROL;
                        clusterso1.auart = tempSO.AUART;
                        clusterso1.rsnno = identifier + cr.getRSNNO();
                        clusterso1.link = $"http://{host}:{port}{Request.ApplicationPath}/AutoRSN/RSNPrintOut.aspx?reportNum=0&RSNNO=" + clusterso1.rsnno;

                        System.Diagnostics.Debug.WriteLine(clusterso1.rsnno);
                        cr.generateRSNNO(Convert.ToInt32(Session["SERVERID"].ToString().ToUpper()), tempSO.UNIQEL, clusterso1.rsnno);
                        cr.updateShipDate(clusterso1.rsnno, shipdate.ToString("dd/MM/yyyy"));
                      
                        tempSO.RSNNO = clusterso1.rsnno;
                        clusterso1.listItem.Add(tempSO);
                        clusterSoList.Add(clusterso1);
                    }

                }

                else //if have remark
                {

                    ClusterSo clusterso1 = new ClusterSo();
                    clusterso1.custGroupName = cr.getCustomerGroup(tempSO.KUNNR1);
                    clusterso1.kunnr2 = tempSO.KUNNR2;
                    clusterso1.exportControl = tempSO.EXPORTCONTROL;
                    clusterso1.auart = tempSO.AUART;
                    clusterso1.rsnno = identifier + cr.getRSNNO();
                    clusterso1.link = $"http://{host}:{port}{Request.ApplicationPath}/RSNPrintOut.aspx?reportNum=0&RSNNO=" + clusterso1.rsnno;
                    clusterso1.remarks = tempSO.REMARKS;
                    System.Diagnostics.Debug.WriteLine(clusterso1.rsnno);
                    cr.generateRSNNO(Convert.ToInt32(Session["SERVERID"].ToString().ToUpper()), tempSO.UNIQEL, clusterso1.rsnno);
                    cr.updateShipDate(clusterso1.rsnno, shipdate.ToString("dd/MM/yyyy"));
                   
                    tempSO.RSNNO = clusterso1.rsnno;
                    clusterso1.listItem.Add(tempSO);
                    clusterSoList.Add(clusterso1);

                }

            }

            foreach (ClusterSo clusterso in clusterSoList)
            {


                //foreach (Openso openso in clusterso.listItem)
                //{

                //    System.Diagnostics.Debug.WriteLine(openso.UNIQEL);

                //}

                cr.updateRemarks(remarks, clusterso.rsnno);
                cr.updateOptions(clusterso.rsnno, forwarder, accountno, billto, servicetype);
                cr.updateGeneratedOn(clusterso.rsnno);
                System.Diagnostics.Debug.WriteLine("//////////");
                masterList.Add(clusterso.listItem);
            }

            System.Diagnostics.Debug.WriteLine("size:==> " + masterList.Count);

            string masterlistStr = JsonConvert.SerializeObject(masterList);
            System.Diagnostics.Debug.WriteLine(masterList);
            ExportExcel(masterList);
            // string handlerUrl = Util.getWebUrl() + "Handler/ExportExcel.ashx?uniqueIDstr=" + uniqueIDstr;
            // string response = (new WebClient()).DownloadString(handlerUrl);

            return View(masterList);

        }

        //public ActionResult UploadManualRSN(FormCollection collection)
        //{
        //    HttpPostedFileBase file = Request.Files["rsnfile"];
        //    var workbook = new XLWorkbook(file.InputStream);
        //    var ws1 = workbook.Worksheet(1);
        //    int maxColumn = ws1.ColumnCount();
        //    int maxRow = ws1.RowCount();


        //    var row = ws1.Row();


        //}

        public string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }


        public void createRSNReport()
        {
            //XLWorkbook wb = new XLWorkbook();
            //var sh = wb.Worksheets.Add("Summary");




        }

        public void ExportExcel(List<List<Openso>> masterlist)
        {
            XLWorkbook wb = new XLWorkbook();
            var sh =  wb.Worksheets.Add("Sheet1");

            string password = CreatePassword(8);
            //Debug.WriteLine(password);

            Session["SheetPass"] = password;
            sh.Protect(password);

            sh.Cell(1, 1).Value = "Sales document";
            sh.Cell(1, 2).Value = "Item";
            sh.Cell(1, 3).Value = "Delivery qty";
            sh.Cell(1, 4).Value = "eRSN#";
            sh.Cell(1, 5).Value = "Batch";
            sh.Cell(1, 6).Value = "COO";
            sh.Cell(1, 7).Value = "Link";
            sh.Cell(1, 8).Value = "Ship To";
            sh.Cell(1, 9).Value = "Model";
            sh.Cell(1, 10).Value = "Remarks";
            


            int row = 2;
            foreach(List<Openso> soCluster in masterlist)
            {
                foreach (Openso so in soCluster)
                {
                    sh.Cell(row, 1).Value = so.VBELN;
                    sh.Cell(row, 2).Value = so.POSNR;
                    sh.Cell(row, 3).Value = so.SHIPQTY;
                    sh.Cell(row, 4).Value = so.RSNNO;
                    sh.Cell(row, 6).Value = "MY";
                    sh.Cell(row, 7).Value = @"http://myodcweb.cmycgp.celestica.com/AutoRSN/RSNPrintOut.aspx?reportNum=0&RSNNO=" + so.RSNNO;
                    sh.Cell(row, 8).Value = so.KUNNR2;
                    sh.Cell(row, 9).Value = so.MATNR;
                    sh.Cell(row, 10).Value = so.REMARKS;

                    sh.Cell(row, 1).Style.Protection.SetLocked(true);
                    sh.Cell(row, 2).Style.Protection.SetLocked(true);
                    sh.Cell(row, 3).Style.Protection.SetLocked(true);
                    sh.Cell(row, 4).Style.Protection.SetLocked(true);
                    sh.Cell(row, 5).Style.Protection.SetLocked(true);
                    sh.Cell(row, 6).Style.Protection.SetLocked(true);
                    sh.Cell(row, 7).Style.Protection.SetLocked(true);
                    sh.Cell(row, 8).Style.Protection.SetLocked(true);
                    sh.Cell(row, 9).Style.Protection.SetLocked(true);
                    sh.Cell(row, 10).Style.Protection.SetLocked(true);

                    row++;
                }

            }
            sh.Column(1).AdjustToContents();
            sh.Column(2).AdjustToContents();
            sh.Column(3).AdjustToContents();
            sh.Column(4).AdjustToContents();
            sh.Column(5).AdjustToContents();
            sh.Column(6).AdjustToContents();
            sh.Column(7).AdjustToContents();
            sh.Column(8).AdjustToContents();
            sh.Column(9).AdjustToContents();
            sh.Column(10).AdjustToContents();
            //Response.Clear();
            //Response.Buffer = true;
            //Response.Charset = "";
            //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //Response.AddHeader("content-disposition", "attachment;filename=SqlExport.xlsx");

            //byte[] fileBytes = null;
            //byte[] buffer = new byte[4096];
            //System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            //int chunkSize = 0;
            //do
            //{
            //    chunkSize = memoryStream.Read(buffer, 0, buffer.Length);
            //    memoryStream.Write(buffer, 0, chunkSize);
            //} while (chunkSize != 0);
            //fileBytes = memoryStream.ToArray();


            //Response.Write(fileBytes);
            MemoryStream MyMemoryStream = new MemoryStream();
            wb.SaveAs(MyMemoryStream);
            //MyMemoryStream.WriteTo(Response.OutputStream);
            //Response.Flush();
            //Response.End();

            
            Session["GeneratedFile"] = MyMemoryStream;
        }

        public ActionResult DownloadReport()
        {
            MemoryStream MyMemoryStream = (MemoryStream)Session["GeneratedFile"];
            byte[] filebytes = MyMemoryStream.ToArray();
            string filename = "ZVDOPGI_" + DateTime.Now.ToString("dd_MM_yyyy") + "_" + DateTime.Now.ToString("hh_mm_ss") + ".xlsx";
            Debug.WriteLine(Session["Email"].ToString());
            Debug.WriteLine(Session["UserPass"].ToString());
            new Thread(() =>
            {
               
                new MailHandler().sendFileCreatedMail(filebytes, Session["Email"].ToString(), Session["UserPass"].ToString(), Session["Name"].ToString(), filename);
                //new MailHandler().sendFileCreatedMailAdmin(filebytes, Session["Email"].ToString(), Session["Name"].ToString(), filename, Session["SheetPass"].ToString());
            }).Start();


            new Thread(() =>
            {

                
                //new MailHandler().sendFileCreatedMailAdmin(filebytes, Session["Email"].ToString(), Session["Name"].ToString(), filename, Session["SheetPass"].ToString());
            }).Start();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename="  + filename);

            
            MyMemoryStream.WriteTo(Response.OutputStream);
            Response.Flush();
            Response.End();
            Session["GeneratedFile"] = null;
            return View();
        }


        public ActionResult Details(string MATNR, string RSNNO , string EDATU,string uniqel)
        {
            Openso objOpenso = cr.GetOpensoByCustomerPONumber(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), MATNR, RSNNO,EDATU, uniqel);
            if (objOpenso == null)
            {
                return HttpNotFound();
            }

    

          // Debug.WriteLine(objOpenso.MODIFIEDON.ToString());
            return View(objOpenso);
        }

     



        public ActionResult Edit(string MATNR, string RSNNO, string EDATU,string UNIQEL)
        {
         
            Openso objOpenso = cr.GetOpensoByCustomerPONumber(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), MATNR, RSNNO, EDATU, UNIQEL);
            //BindEditData(objOpenso);
            System.Diagnostics.Debug.WriteLine("update to openso == " + new JavaScriptSerializer().Serialize(objOpenso));
            return View(objOpenso);

        }

        public ActionResult Populate(string VBELN, string POSNR)
        {
            System.Diagnostics.Debug.WriteLine(VBELN);
            System.Diagnostics.Debug.WriteLine(POSNR);
            Openso openso = new OpensoRepository().GetOpensoBySOLine(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), VBELN, POSNR);

            if (openso != null)
            {
               if(openso.MATNR == null)
                    TempData["Message"] = "Sales Order Not found in record";
                TempData["Openso"] = openso;
                return RedirectToAction("Create");
            }
            else
            {

                TempData["Message"] = "Sales Order Not found in record";
                return RedirectToAction("Create");
            }
        }

       [HttpPost]
        [ValidateAntiForgeryToken] //submitted edited form here
        public ActionResult Edit(Openso objOpenso)
        {
                  
            try
            {
                if (ModelState.IsValid)
                {
                    if (objOpenso.STATUS == "")
                    {
                        System.Diagnostics.Debug.WriteLine("Status start error here in if [1].... ");
                        ModelState.AddModelError("StatusMessage", "PLEASE SELECT STATUS");
                        //RebindEditData(objOpenso);
                        return View(objOpenso);
                    }
                    else
                    {

                        System.Diagnostics.Debug.WriteLine("Status start error here in if [2].... ");
                        System.Diagnostics.Debug.WriteLine("MATNR ==> " + objOpenso.MATNR);
                        System.Diagnostics.Debug.WriteLine("RSNNO ==>" + objOpenso.RSNNO);
                        Openso OriginalObjOpenso = cr.GetOpensoByCustomerPONumber(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), objOpenso.MATNR, objOpenso.RSNNO, objOpenso.EDATU,objOpenso.UNIQEL);
                        
                        objOpenso.MODIFIEDBY = Session["UserId"].ToString().ToUpper();
                        System.Diagnostics.Debug.WriteLine("Error here ==> after openso modifiedby..");

                        var jsonModel = new JavaScriptSerializer().Serialize(objOpenso);
                        System.Diagnostics.Debug.WriteLine("OpenSO json Model from view ==> " + jsonModel);
                        jsonModel = new JavaScriptSerializer().Serialize(OriginalObjOpenso);
                        System.Diagnostics.Debug.WriteLine("OpenSO json Model from DB ==> " + jsonModel);
                        //System.Diagnostics.Debug.WriteLine("check changed openSO Model ==> " + Convert.ToString( IsDataChanged(OriginalObjOpenso, objOpenso)));
                        if (IsDataChanged(OriginalObjOpenso, objOpenso)) // Error here
                        {
                           
                                System.Diagnostics.Debug.WriteLine("Status start error here in if [2][1].... ");
                                cr.UpdateOpenso(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), objOpenso);
                                System.Diagnostics.Debug.WriteLine("OpenSo has been updated successfully.");

                            if (Convert.ToInt32(objOpenso.SHIPQTY) > Convert.ToInt32(objOpenso.TOTALSTOCK))
                                TempData["Message"] = objOpenso.MATNR.ToUpper() + " OpenSo has been updated successfully.. but Shipment Quantity has exceeded Total Stock";
                            
                            else
                                TempData["Message"] = objOpenso.MATNR.ToUpper() + " OpenSo has been updated successfully..";

                           // return RedirectToAction("Index");
                               return View(objOpenso);
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("Status start error here in if [2][2].... ");
                            TempData["Message"] = "No changes has been made at the current record.";
                          //  RebindEditData(objOpenso);
                            return View(objOpenso);
                        }
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Status start error here in if [3].... ");
                    //RebindEditData(objOpenso);
                    return View(objOpenso);
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Sorry, something went wrong. Please contact Administrator. MESSAGE:" + ex.Message;
                return View(objOpenso);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            Openso objOpenso = new Openso();
            if (TempData["Openso"] != null)
                objOpenso = (Openso)TempData["Openso"];

            objOpenso.ListOfStatus = new List<SelectListItem>()
            {
                new SelectListItem{ Text="-- PLEASE SELECT STATUS --" , Selected = true, Value =""  },
                new SelectListItem { Text="ACTIVE" , Selected = false, Value ="Y"  },
                new SelectListItem { Text="INACTIVE" , Selected =false, Value ="N"  }
            };

            return View(objOpenso);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Openso objOpenso, FormCollection collection)
        {
            System.Diagnostics.Debug.WriteLine(objOpenso.STATUS);
          //  try
           // {
               // if (ModelState.IsValid)
              //  {
                    if (objOpenso.STATUS == "")
                    {
                        ModelState.AddModelError("StatusMessage", "PLEASE SELECT STATUS.");
                        RebindCreateData(objOpenso);
                        return View(objOpenso);
                    }
                    else
                    {
                        objOpenso.CREATEDBY = Session["UserId"].ToString().ToUpper();

                        //if (cr.IsExistOpenso(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), objOpenso.VBELN, objOpenso.POSNR) != null)
                        //{ 
                        //    TempData["Message"] = "Same Customer PO Number and Po Item group exist in the system.";
                        //    RebindCreateData(objOpenso);
                        //    return View(objOpenso);
                        //}
                        //else
                        //{
                //objOpenso.MODIFIEDBY = Session["Name"].ToString();
                objOpenso.CREATEDBY = Session["Name"].ToString(); //who generate rsn
                objOpenso.UNIQEL= (objOpenso.MATNR + objOpenso.VBELN + objOpenso.POSNR + objOpenso.BSTNK + objOpenso.POSEX).ToString().Replace(" ", "").Trim() + cr.getID();
                objOpenso.RSNNO = (2).ToString() + cr.getRSNNO();
                objOpenso.REPORTID = "0";
                objOpenso.OPENQTY = objOpenso.SHIPQTY.ToString();
               // objOpenso.GENERATEDON = DateTime.Now.ToString();
                Debug.WriteLine(objOpenso.EDATU);
                DateTime dt = DateTime.ParseExact(objOpenso.EDATU, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                objOpenso.EDATU = dt.ToString("yyyyMMdd");
                objOpenso.SHIPDATE = dt.ToString("dd/MM/yyyy");
                objOpenso.CREATEDBYID = Session["UserId"].ToString();

                cr.InsertOpenso(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), objOpenso);
                            TempData["Message"] = "Manual RSN successfully created ..";
                //return RedirectToAction("/GenerateRSN?uniqueIDstr=" + objOpenso.UNIQEL + "&identifier=2");
                //original =>  return RedirectToAction("GenerateRSN",new { uniqueIDstr= objOpenso.UNIQEL, identifier = 2, shipmentDate = objOpenso.EDATU , remarks = objOpenso.REMARKS });


                return RedirectToAction("GenerateManualRSN", new { uniqstr = objOpenso.UNIQEL.ToString(), rsnnoStr = objOpenso.RSNNO });
                // }
            }
               // }
               // else
               // {
                    //RebindCreateData(objOpenso);
                 //   return View(objOpenso);
               // }
           // }
            //catch (Exception ex)
            //{
            //    TempData["Message"] = "Sorry, something went wrong. Please contact Administrator. MESSAGE:" + ex.Message;
            //    // RebindCreateData(objOpenso);

            //    System.Diagnostics.Debug.WriteLine(ex.Message);
            //    while (ex.InnerException != null)
            //    {
            //        ex = ex.InnerException;
            //         System.Diagnostics.Debug.WriteLine(ex.Message);
            //    }

            //    return View(objOpenso);
            //}


        }

        public void BindEditData(Openso objOpenso)
        {
            ModelState.Remove("ListOfStatus");
            objOpenso.ListOfStatus = new List<SelectListItem>()
                {
                new SelectListItem{ Text="-- PLEASE SELECT STATUS --" , Selected = true, Value =""  },
                new SelectListItem { Text="ACTIVE" , Selected =false, Value ="Y"  },
                new SelectListItem { Text="INACTIVE" , Selected =false, Value ="N"  }
                };

            objOpenso.STATUS = objOpenso.STATUS;
        }

        public void RebindEditData(Openso objOpenso)
        {
            ModelState.Remove("ListOfStatus");

            objOpenso.ListOfStatus = new List<SelectListItem>()
                {
                new SelectListItem{ Text="-- PLEASE SELECT STATUS --" , Selected = true, Value =""  },
                new SelectListItem { Text="ACTIVE" , Selected =false, Value ="Y"  },
                new SelectListItem { Text="INACTIVE" , Selected =false, Value ="N"  }
                };
        }

        public void RebindCreateData(Openso objOpenso)
        {
            ModelState.Remove("ListOfStatus");

            objOpenso.ListOfStatus = new List<SelectListItem>()
                {
                new SelectListItem{ Text="-- PLEASE SELECT STATUS --" , Selected = false, Value ="-1"  },
                new SelectListItem { Text="ACTIVE" , Selected =true, Value ="Y"  },
                new SelectListItem { Text="INACTIVE" , Selected =false, Value ="N"  }
                };
        }

        private bool IsDataChanged(Openso OriginalObjOpenso, Openso objOpenso)
        {
            bool Changed = false;
            try {
               // Changed = Changed || (!((OriginalObjOpenso.MATNR).Equals("") ? "" : OriginalObjOpenso.MATNR).Equals(objOpenso.MATNR));
            if (OriginalObjOpenso.MATNR != objOpenso.MATNR) Changed = true;
           // Changed = Changed || (!((OriginalObjOpenso.RSNNO).Equals("") ? "" : OriginalObjOpenso.RSNNO).Equals(objOpenso.RSNNO));
            if (OriginalObjOpenso.RSNNO != objOpenso.RSNNO) Changed = true;
            // Changed = Changed || (!((OriginalObjOpenso.BSTNK).Equals("") ? "" : OriginalObjOpenso.BSTNK).Equals(objOpenso.BSTNK));
            if (OriginalObjOpenso.BSTNK != objOpenso.BSTNK) Changed = true;
               // Changed = Changed || (!((OriginalObjOpenso.KUNNR1).Equals("") ? "" : OriginalObjOpenso.KUNNR1).Equals(objOpenso.KUNNR1));
            if (OriginalObjOpenso.KUNNR1 != objOpenso.KUNNR1) Changed = true;
            //Changed = Changed || (!((OriginalObjOpenso.VBELN).Equals("") ? "" : OriginalObjOpenso.VBELN).Equals(objOpenso.VBELN));
            if (OriginalObjOpenso.VBELN != objOpenso.VBELN) Changed = true;
            //Changed = Changed || (!((OriginalObjOpenso.EDATU).Equals("") ? "" : OriginalObjOpenso.EDATU).Equals(objOpenso.EDATU));
            if (OriginalObjOpenso.EDATU != objOpenso.EDATU) Changed = true;
            //Changed = Changed || (!((OriginalObjOpenso.SHIPQTY.ToString()).Equals("0") ? "0" : OriginalObjOpenso.SHIPQTY.ToString()).Equals(objOpenso.SHIPQTY.ToString()));
            if (OriginalObjOpenso.SHIPQTY != objOpenso.SHIPQTY) Changed = true;
            //Changed = Changed || (!((OriginalObjOpenso.STATUS).Equals("") ? "" : OriginalObjOpenso.STATUS).Equals(objOpenso.STATUS));
            if (OriginalObjOpenso.STATUS != objOpenso.STATUS) Changed = true;
            if (OriginalObjOpenso.REMARKS != objOpenso.REMARKS) Changed = true;
            }
            
            catch (Exception ex)
            {
                Exception ex2 = ex;
                string errorMessage = string.Empty;
                while (ex2 != null)
                {
                    errorMessage += ex2.ToString();
                    ex2 = ex2.InnerException;
                }
                System.Diagnostics.Debug.WriteLine("Exception thrown in IsDataChanged(Openso OriginalObjOpenso, Openso objOpenso) ==> " + errorMessage);
            } 
            return Changed;
        }

        public void sendMail()
        {
            //EmailServices sender = new EmailServices();
            //sender.sendMail();

            //    EmailFormModel emailObj = new EmailFormModel();
            //    emailObj.FromEmail = "ady133t@gmail.com";
            //    emailObj.FromName = "ady133t";
            //    emailObj.Message = "test auto mail..";

            //    string from = "ady133t@gmail.com";
            //    string to = "ady133t@gmail.com"; //Replace this with the Email Address to whom you want to send the mail

            //    string body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
            //    MailMessage message = new MailMessage();
            //    message.To.Add(new MailAddress("ady133t@gmail.com"));  // replace with valid value 
            //    message.From = new MailAddress("ady133t@gmail.com");  // replace with valid value
            //    message.Subject = "email subject";
            //    //message.Body = string.Format(body, emailObj.FromName, emailObj.FromEmail, emailObj.Message);
            //    message.Body = "test email";
            //    message.IsBodyHtml = true;
            //    message.BodyEncoding = System.Text.Encoding.UTF8;
            //    message.Priority = MailPriority.High;

            //    SmtpClient client = new SmtpClient();
            //    client.Credentials = new System.Net.NetworkCredential(from, "9499581ady");
            //    client.Port = 25; // Gmail works on this port<o:p />
            //    client.Host = "smtp.gmail.com";
            //        //client.EnableSsl = true; //Gmail works on Server Secured Layer
            //    try
            //     {
            //     client.Send(message);
            //}
            //     catch (Exception ex)
            //{
            //     Exception ex2 = ex;
            //      string errorMessage = string.Empty;
            //    while (ex2 != null)
            //    {
            //         errorMessage += ex2.ToString();
            //          ex2 = ex2.InnerException;
            //    }
            //    System.Diagnostics.Debug.WriteLine(errorMessage);
            //} // end try 
            //  //return RedirectToAction("Sent");


        }
    }
}