using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace AutoRSN.Models
{
    public class User
    {
        [Key]
        [Required]
        public int SERVERID { get; set; }

        [Display(Name = "FULLNAME #")]
        public string FULLNAME { get; set; }
        [Key]
        [Required]
        [Display(Name = "USERNAME")]
        public string USERNAME { get; set; }

        [Display(Name = "PASSWORD")]
        public string PASSWORD { get; set; }

        [Required]
        [Display(Name = "LEVEL")]
        public string LVL { get; set; }
        [Required]
        [Display(Name = "Department")]
        public string DEPT { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string EMAIL { get; set; }

        public string SIGNATURE { get; set; }

       
        //[Display(Name = "CUSTOMER GROUP")]
        public string[] CUSTOMERGROUP { get; set; }
        public string[] PERMISSION { get; set; }
        //[Required]
        [Display(Name = "Employe Ext No")]
        public string EXTNO { get; set; }
        //[Required]
        [Display(Name = "MANAGER")]

        public string MANAGER { get; set; }
        //[Required]
        [Display(Name = "MANAGER")]

        public string SUPERVISOR { get; set; }
        //[Required]
        [Display(Name = "SUPERVISOR")]

        public string MANAGEREXT { get; set; }
        [Display(Name = "Status")]
        [ValidateStatus(ErrorMessage = "The Status field is required.")]
        public string STATUS { get; set; }
        public string MODIFIEDBY { get; set; }
        public DateTime MODIFIEDON { get; set; }
        public string CREATEDBY { get; set; }
        public DateTime CREATEDON { get; set; }

        public string CREATEDBYID { get; set; }

        public List<PermissionHolder> PermissionHolderList  { get; set; }

    [NotMapped]
        public IEnumerable<SelectListItem> ListOfStatus { get; set; }
    }
}