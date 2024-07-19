using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
namespace AutoRSN.Models
{
    public class ListOfSN
    {
        [Required(ErrorMessage = "Serial Number required!", AllowEmptyStrings = false)]
        public string SN { get; set; }
        [Required(ErrorMessage = "Status required!", AllowEmptyStrings = false)]
        public string STATUS { get; set; }
        [Required(ErrorMessage = "Comment required!", AllowEmptyStrings = false)]
        public string COMMENT { get; set; }
    }
}