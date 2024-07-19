using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace AutoRSN.Models
{
    public class Openso
    {
        [Key]
        [Required]
        public int SERVERID { get; set; }

        public string UNIQEL { get; set; }

        public string SHIPDATE { get; set; }

        [Key]
        [Display(Name = "RSN #")]
        public string RSNNO { get; set; }

        [Key]
        [Display(Name = "ReportID")]
        public string REPORTID { get; set; }

        [Key]
        [Required]
        [Display(Name = "Customer Name")]
        public string KUNNR1 { get; set; }

        [Display(Name = "Ship to")]
        public string KUNNR2 { get; set; }


        [Key]
        [Required]
        [Display(Name = "CLS Part #")]
        public string MATNR { get; set; }

        [Display(Name = "PO #")]
        public string BSTNK { get; set; }

        [Display(Name = "Sales Order #")]
        public string VBELN { get; set; }

        [Display(Name = "Delivery Date")]
        public string EDATU { get; set; }

        [Display(Name = "Status")]
        [ValidateStatus(ErrorMessage = "The Status field is required.")]
        public string STATUS { get; set; }

        public string MODIFIEDBY { get; set; }
        public DateTime MODIFIEDON { get; set; }
        public string CREATEDBY { get; set; }
        public DateTime CREATEDON { get; set; }
        public int SHIPQTY { get; set; }
        public string IDNRK { get; set; }
        public string MENGE { get; set; }
        public string POSNR { get; set; }
        public string OPENQTY { get; set; }
        public string TOTALSTOCK { get; set; }
        public string TOTALCOMPONENT { get; set; }
        public string EXPORTCONTROL { get; set; }
        public string ARKTX { get; set; }
        public string AUART{ get; set; }
        public string POSEX{ get; set; }
        public string KMEIN { get; set; }
        public string NETPR { get; set; }
        public string KPEIN { get; set; }
        public string KDMAT { get; set; }
        public string WAERK { get; set; }
        public string LGPBE { get; set; }
        public string SHIPAMT { get; set; }
        public string SHIPSHIFT { get; set; }
        public string SREMARK { get; set; }
        public string USERNAME { get; set; }
        public DateTime LAST_UPDATED_DATE { get; set; }
        public string REVISIONNO { get; set; }
        public string FORWARDER { get; set; }
        public string EMAILDATE { get; set; }
        public string EMAILTIME { get; set; }
        public string EMAILSTAT { get; set; }
        public string RSN_TIME { get; set; }
        public string REMARKS { get; set; }
        public string CCO { get; set; }

        public string CUSTOMFORWARDER { get; set; }
        public string CUSTOMACCOUNTNO{ get; set; }
        public string CUSTOMBILLTO { get; set; }
        public string CUSTOMSERVICETYPE { get; set; }

       public string CREATEDBYID { get; set; }

        public DateTime GENERATEDON { get; set; }

        public string SHIPSTATUS { get; set; }

        public string CURRENCY { get; set; }

        [Required]
        [Display(Name = "STORAGE LOCATION")]
        public string STORAGELOC { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> ListOfStatus { get; set; }

        public Nullable<DateTime> Published { get; set; }

    }
}