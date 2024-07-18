using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoRSN.Models
{





    public class ReportGenerator
    {

       

        public class SummaryOneModel
        {
            public string Model { set; get; }
            public string ShipToParty { set; get; }
            public string Price { set; get; }
            public string[] ShipDate { set; get; }
            public string GrandTotal { set; get; }

        }

        public class SummaryTwoModel
        {
            public string Tick { set; get; }
            public string Model{ set; get; }
            public string CustModel { set; get; }
            public string CustPN { set; get; }
            public string Description { set; get; }
            public string ServiceType { set; get; }
            public string ShipMode { set; get; }
            public string Charge { set; get; }
            public string IncoTerm { set; get; }
            public string ExportCTRL { set; get; }
            public string RTV { set; get; }
            public string Stock { set; get; }
            public string RequiredDate { set; get; }
            public string ShipDate { set; get; }
            public string SOQty { set; get; }
            public string OrderNumber { set; get; }
            public string ShipToParty { set; get; }
            public string CustomerPO { set; get; }
            public string POLine { set; get; }
            public string SHCLine { set; get; }
            public string MilCTR { set; get; }
            public string Price { set; get; }
            public string Level { set; get; }
            public string BinNo { set; get; }
            public string ShipStatus { set; get; }
            public string ShipSetNotes { set; get; }
            public string Remarks { set; get; }
           // public string  { set; get; }


        }



        public void test()
        {
        //    ReportGenerator. sum = new ReportGenerator.SummaryOne();
        }


    }
}