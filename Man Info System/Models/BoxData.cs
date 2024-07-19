using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace AutoRSN.Models
{
    public class BoxData
    {
        public string BOXID { get; set; }
        public string PACK_DATE { get; set; }
        public string FAMILY { get; set; }
    }
}