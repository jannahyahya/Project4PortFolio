using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace AutoRSN.Models
{
    public class Reason
    {
        [Key]
        public int SERVERID { get; set; }
        [Key]
        public int REASONID { get; set; }
        [Required]
        [Display(Name = "Reason")]
        public string REASON { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string DESCRIPTION { get; set; }
        [Display(Name = "Status")]
        [ValidateStatus(ErrorMessage = "The Status field is required.")]
        public string STATUS { get; set; }
        public string MODIFIEDBY { get; set; }
        public DateTime MODIFIEDON { get; set; }
        public string CREATEDBY { get; set; }
        public DateTime CREATEDON { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> ListOfStatus { get; set; }
    }
}