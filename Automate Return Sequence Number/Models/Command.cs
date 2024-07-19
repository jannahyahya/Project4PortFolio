using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoRSN.Models
{
    public class Command
    {

        public static string getForwarder = "select forwarder from rsn_st_customer where customercode=";
        public static string getCustName = "select customername from rsn_st_customer where customercode=";

        public static string getAddress1 = "select address1 from rsn_st_customer where customercode=";
        public static string getAddress2 = "select address2 from rsn_st_customer where customercode=";

        public static string getRegion = "select region from rsn_st_customer where customercode=";
        public static string getPoscode = "select postcode from rsn_st_customer where customercode=";
        public static string getcountry = "select country from rsn_st_customer where customercode=";
        //  public static string getContactNo = "select ATTNNAME,ATTNCONTACTNO from rsn_st_customer where customercode=";
    }
}