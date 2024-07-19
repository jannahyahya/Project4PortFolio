using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace AutoRSN.Models
{
    public class Rsn
    {
        public int SERVERID { get; set; }
        [Key]
        [Required]
        [Display(Name = "Customer PO Number #")]
        public string BSTNK { get; set; }
      
        [Display(Name = "Cust PO Item No")]
        public string POSEX { get; set; }
     
        [Display(Name = "Customer PO Date")]
        public DateTime BSTDK { get; set; }

        [Display(Name = "Sales Org")]
        public string VKORG { get; set; }
       
        
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