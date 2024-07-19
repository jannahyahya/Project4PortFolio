using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace AutoRSN.Models
{
    public class Customer
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
        [Display(Name = "Material Group")]
        public string MATERIALGROUP { get; set; }

        [Required]
        [Display(Name = "Customer Name")]
        public string CUSTOMERNAME { get; set; }

        [Required]
        [Display(Name = "Customer Group")]
         public string CUSTOMERGROUP { get; set; }

       // [Required]
        [Display(Name = "Ship To")]
        public string SHIPTO { get; set; }

        [Required]
        [Display(Name = "Address 1")]
        public string ADDRESS1 { get; set; }
        [Required]
        [Display(Name = "Address 2")]
        public string ADDRESS2 { get; set; }
        [Required]
        [Display(Name = "Postcode")]
        public string POSTCODE { get; set; }
        [Required]
        [Display(Name = "Region")]
        public string REGION { get; set; }
        [Required]
        [Display(Name = "Country")]
        public string COUNTRY { get; set; }
        [Required]
        [Display(Name = "Forwarder")]
        public string FORWARDER { get; set; }
        [Required]
        [Display(Name = "Forwarder Account No")]
        public string ACCOUNTNO { get; set; }

        [Required]
        [Display(Name = "Attention Name")]
        public string ATTNNAME { get; set; }
        [Required]
        [Display(Name = "Attention Contact No")]
        public string ATTNCONTACTNO { get; set; }
        [Display(Name = "Status")]
        [ValidateStatus(ErrorMessage = "The Status field is required.")]
        public string STATUS { get; set; }
        public string MODIFIEDBY { get; set; }
        public DateTime MODIFIEDON { get; set; }
        public string CREATEDBY { get; set; }
        public DateTime CREATEDON { get; set; }

        //Freight_Collect;//
        public bool EX_Factory { get; set; }
        public bool FCA_FOB { get; set; }

        //Freight_Prepaid//
        public bool DOOR_TO_DOOR { get; set; }
        public bool DOOR_TO_PORT_DESC { get; set; }
        public bool DAP { get; set; }
        public bool DDP{ get; set; }

        public string Freight_Prepaid { get; set; }

        [Required]
        [Display(Name = "Bill TO")]
        public string BILLTO { get; set; }
        [Required]
        [Display(Name = "Service Type")]
        public string SERVICETYPE { get; set; }


        [NotMapped]
        public IEnumerable<SelectListItem> ListOfStatus { get; set; }
    }
}