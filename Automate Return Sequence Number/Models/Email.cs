using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;


namespace AutoRSN.Models
{
    public class Email
    {
        [Key]
        [Required]
        public int SERVERID { get; set; }
        [Key]
        [Required]
        [Display(Name = "Customer #")]
        public string CUSTOMERCODE { get; set; }
        [Key]
        [Required]
        public int EMAILID { get; set; }
        [Required]
        [Display(Name = "Email Title")]
        public string EMAILTITLE { get; set; }
        [Required]
        [Display(Name = "Attention To")]
        public string ATTENTIONNAME { get; set; }
        [Required]
        [Display(Name = "Email To")]
        public string EMAILTO { get; set; }
        [Display(Name = "Email Cc")]
        public string EMAILCC { get; set; }
        [Display(Name = "Status")]
        [ValidateStatus(ErrorMessage = "The Status field is required.")]
        public string STATUS { get; set; }
        public string MODIFIEDBY { get; set; }
        public DateTime MODIFIEDON { get; set; }
        public string CREATEDBY { get; set; }
        public DateTime CREATEDON { get; set; }


        [NotMapped]
        public IEnumerable<SelectListItem> ListOfStatus { get; set; }

        [NotMapped]
        public IEnumerable<Customer> ListOfCustomer { get; set; }
    }
}