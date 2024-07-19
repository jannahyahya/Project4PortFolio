using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using PagedList;
using System.ComponentModel.DataAnnotations;
namespace AutoRSN.Models
{
    public class SalesOrder
    {


        public int SERVERID { get; set; }
        [Key]
        [Required]
        [Display(Name = "Customer PO Number #")]
        public string BSTNK { get; set; }

        public int? Page { get; set; }
        [Display(Name = "BSTNK")]
        public string ProductName { get; set; }
        public decimal? Price { get; set; }
        /**public IPagedList<BSTNK> SearchResults { get; set; } **/
        public string SearchButton { get; set; }

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
        public string AUART { get; set; }
        public string KUNNR { get; set; }
        public string MATNR { get; set; }
        public string LGORT { get; set; }
        public string LGPBE { get; set; }
        public string MENGE { get; set; }
     

        [NotMapped]
        public IEnumerable<SelectListItem> ListOfStatus { get; set; }

    }
}