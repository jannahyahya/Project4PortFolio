using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;


namespace AutoRSN.Models
{
    public class ShipSet
    {
       public string SHIP_SET { get; set; }
       public string CCA_PN { get; set; }
       public string CCA_REV { get; set; }
       public string QTY { get; set; }
       public string SET_PN { get; set; }
       public string SET_PN_REV { get; set; }
       public string REMARKS { get; set; }
    }
}