using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace AutoRSN.Models
{
    public class Server
    {
        [Key]
        public int SERVERID { get; set; }
        [Required]
        [Display(Name = "Server")]
        public string SERVER { get; set; }
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


    public class ValidateStatus : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (Convert.ToString(value) == "" || Convert.ToString(value) == null)
                return false;
            else
                return true;
        }
    }
}