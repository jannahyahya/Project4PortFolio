﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoRSN.Models;
using System.Diagnostics;
using AutoRSN.Repositories;
using ClosedXML.Excel;
using System.IO;
using System.Globalization;

namespace AutoRSN.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            //return View(Enumerable.Empty<Customer>());
            Report report = new Report();
            report.DATE= DateTime.Now.ToString("MM/dd/yyyy");
            return View(new Report());
        }


        [HttpPost]
        public ActionResult Index(Report report, FormCollection collection)
        {
            //string FilterByCCMGCriteria = frmCollection["FilterByCCMG"].ToString();

            string report_option = collection["report_option"].ToString();

            string genDateFrom = collection["genDateFrom"].ToString();
            string genDateTo = collection["genDateTo"].ToString();

            string shipDateFrom = collection["shipDateFrom"].ToString();
            string shipDateTo = collection["shipDateTo"].ToString();

            string groupName = collection["groupName"];



            //if (report.DATE == null)
            //{
            //    report.DATE = DateTime.Now.ToString("MM/dd/yyyy");
            //}

            DateTime dateFrom = new DateTime();
            DateTime dateTo = new DateTime();

            //DateTime rsnTime = new DateTime();
            DateTime temp = DateTime.ParseExact("07/01/2018", "MM/dd/yyyy", CultureInfo.InvariantCulture);

            if (report_option.Equals("generatedon"))
            {
                dateFrom = DateTime.ParseExact(genDateFrom, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                dateTo = DateTime.ParseExact(genDateTo, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            }
            else if (report_option.Equals("shipon"))
            {
                dateFrom = DateTime.ParseExact(shipDateFrom, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                dateTo = DateTime.ParseExact(shipDateTo, "MM/dd/yyyy", CultureInfo.InvariantCulture);

            }

            
            Debug.WriteLine(groupName);

            if ((temp - dateFrom).Days <= 0) // check not less than specific date
            {

                ReportRepository rr = new ReportRepository();
                MemoryStream dataStream = ExportExcel(rr.getReport(dateFrom.ToString("MM/dd/yyyy"), dateTo.ToString("MM/dd/yyyy"),  Session["Name"].ToString(), Session["NameFromAD"].ToString(),groupName, report_option));
                Debug.WriteLine(report.DATE);

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=" + "RSN_Summary_" + dateFrom.ToString("MM/dd/yyyy") + "-" + dateTo.ToString("MM/dd/yyyy") + "_" + Session["Name"].ToString() + ".xlsx");

                dataStream.WriteTo(Response.OutputStream);

                Response.Flush();
                Response.End();
            }
            else
            {
                TempData["Message"]= "Summary only available starting from 1 July 2018 onward..";

              
            }

            return View(report);
        }


        public MemoryStream ExportExcel(List<Report> reportList)
        {
            XLWorkbook wb = new XLWorkbook();
            var sh = wb.Worksheets.Add("Sheet1");


            // Session["SheetPass"] = password;
            //sh.Protect("123456789");

            sh.Cell(1, 1).Value = "MODEL";
            sh.Cell(1, 2).Value = "CUS PN";
            sh.Cell(1, 3).Value = "DESCRIPTION";
            sh.Cell(1, 4).Value = "SERVICE TYPE";
            sh.Cell(1, 5).Value = "SHIP MODE";
            sh.Cell(1, 6).Value = "CHARGE ";
            sh.Cell(1, 7).Value = "INCOTERM";
            sh.Cell(1, 8).Value = "EXPORT CTRL";
            sh.Cell(1, 9).Value = "RTV";
            sh.Cell(1, 10).Value = "RMA";
            sh.Cell(1, 11).Value = "STOCK";
            sh.Cell(1, 12).Value = "REQUIRED DATE";
            sh.Cell(1, 13).Value = "SHIP DATE";
            sh.Cell(1, 14).Value = "SO QTY";
            sh.Cell(1, 15).Value = "ORDER NUMBER";
            sh.Cell(1, 16).Value = "SHIP TO PARTY";
            sh.Cell(1, 17).Value = "CUSTOMER PO";
            sh.Cell(1, 18).Value = "PO LINE";
            sh.Cell(1, 19).Value = "SHC LINE";
            sh.Cell(1, 20).Value = "PRICE";
            sh.Cell(1, 21).Value = "STATUS";
            sh.Cell(1, 22).Value = "SHIP SET NOTES";
            sh.Cell(1, 23).Value = "REMARKS";
            sh.Cell(1, 24).Value = "INDICATOR";
            sh.Cell(1, 25).Value = "FORWARDER";
            sh.Cell(1, 26).Value = "TIMING";
            sh.Cell(1, 27).Value = "AMOUNT";
            sh.Cell(1, 28).Value = "USER NAME";
            sh.Cell(1, 29).Value = "DATE_TIME";
            sh.Cell(1, 30).Value = "CUSTOMER";
            sh.Cell(1, 31).Value = "SHIP";
            sh.Cell(1, 32).Value = "SHIP DATETIME";

            int row = 2;
            foreach (Report report in reportList)
            {

                sh.Cell(row, 1).Value = report.MODEL;
                sh.Cell(row, 2).Value = report.CUSPN;
                sh.Cell(row, 3).Value = report.DESC;
                sh.Cell(row, 4).Value = report.SERVICETYPE;
                sh.Cell(row, 5).Value = report.SHIPMODE;
                sh.Cell(row, 6).Value = report.CHARGE;
                sh.Cell(row, 7).Value = report.INCOTERM;
                    sh.Cell(row, 8).Value = report.EXPORTCTRL;
                sh.Cell(row,9).Value = report.RTV;
                sh.Cell(row, 10).Value = report.RMA;
                sh.Cell(row, 11).Value = report.STOCK;
                sh.Cell(row, 12).Value = report.REQUIRED_DATE;
                sh.Cell(row, 13).Value = report.SHIPDATE;
                sh.Cell(row, 14).Value = report.SOQTY;
                    sh.Cell(row, 15).Value = report.SO;
                sh.Cell(row, 16).Value = report.SHIPTOPARTY;
                sh.Cell(row, 17).Value = report.PO;
                sh.Cell(row, 18).Value = report.POLINE;
                sh.Cell(row, 19).Value = report.SOLINE;
                sh.Cell(row, 20).Value = report.PRICE;

                if (report.STATUS.Contains("FULL"))
                    sh.Cell(row, 21).Style.Fill.BackgroundColor = XLColor.Green;
                else if (report.STATUS.Contains("PARTIAL"))
                    sh.Cell(row, 21).Style.Fill.BackgroundColor = XLColor.Yellow;
                else if (report.STATUS.Contains("ZERO"))
                    sh.Cell(row, 21).Style.Fill.BackgroundColor = XLColor.Red;


                sh.Cell(row, 21).Value = report.STATUS;


                sh.Cell(row, 22).Value = report.SHIPSETNOTES;
                sh.Cell(row, 23).Value = report.REMARKS;
                sh.Cell(row, 24).Value = report.INDICATOR;
                sh.Cell(row, 25).Value = report.FORWARDER;
                sh.Cell(row, 26).Value = report.TIMING;
                sh.Cell(row, 27).Value = report.AMOUNT;
                sh.Cell(row, 28).Value = report.USERNAME;
                sh.Cell(row, 29).Value = report.DATE_TIME;
                sh.Cell(row, 30).Value = report.CUSTOMER;
                sh.Cell(row, 31).Value = report.SHIPSTATUS;
                sh.Cell(row, 32).Value = report.SHIPSTATUSON;
                row++;


            }
            for (int a = 1; a <= 32; a++)
                sh.Column(a).AdjustToContents();


            MemoryStream MyMemoryStream = new MemoryStream();
            wb.SaveAs(MyMemoryStream);
            return MyMemoryStream;
            // Session["GeneratedFile"] = MyMemoryStream;
        }
    }
}