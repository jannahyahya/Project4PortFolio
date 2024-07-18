using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace AutoRSN.Models
{
    public class ItemNotes
    {
        [Key]
        [Required]
        public int SERVERID { get; set; }

        [Display(Name = "Customer PO #")]
        public string CUSTOMERPO { get; set; }

        [Key]
        [Required]
        [Display(Name = "Sales Order #")]
        public string SALESORDER { get; set; }

        [Display(Name = "LINE #")]
        public string SOLINE{ get; set; }


        [Display(Name = "Status")]
        //[ValidateStatus(ErrorMessage = "The Status field is required.")]
        public string STATUS { get; set; }

        [Display(Name = "Reason")]
        [ValidateReason(ErrorMessage = "The Reason field is required.")]
        public int REASONID { get; set; }

        public string REASON { get; set; }

     
        [Display(Name = "Remark")]
        public string REMARK { get; set; }

        public string MODIFIEDBY { get; set; }
        public virtual DateTime MODIFIEDON { get; set; }
        public virtual string CREATEDBY { get; set; }
        public virtual DateTime CREATEDON { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> ListOfStatus { get; set; }

        [NotMapped]
        public IEnumerable<Reason> ListOfReason { get; set; }
    }

    public class ValidateReason : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (Convert.ToInt32(value) == 0 || Convert.ToInt32(value) == null)
                return false;
            else
                return true;
        }

    }
}