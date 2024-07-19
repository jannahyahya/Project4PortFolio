using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace AutoRSN.Models
{
    public class Defect
    {
        [Key]
        [Display(Name = "ID")]
        public string ID { get; set; }
        [Required]
        [Display(Name = "RSN NO")]
        public string RSN_NO { get; set; }
        [Required]
        [Display(Name = "PO NO")]
        public string PO_NO { get; set; }
        [Required]
        [Display(Name = "MODEL")]
        public string MODEL { get; set; }
        [Required]
        [Display(Name = "BOX ID")]
        public string BOXID { get; set; }
        [Required]
        [Display(Name = "PACK DATE")]
        public string PACK_DATE { get; set; }
        [Required]
        [Display(Name = "FAMILY")]
        public string FAMILY { get; set; }
        public string SN { get; set; }
        public string DEFECT { get; set; }
        public string PMP { get; set; }
        public string DOCUMENT { get; set; }
        public string CREATEDBY { get; set; }
        public DateTime CREATEDON { get; set; }
        public string REVISEDBY { get; set; }
        public DateTime REVISEDON { get; set; }
        public string UNPACKEDBY { get; set; }
        public DateTime UNPACKEDON { get; set; }
        public string PULLEDBY { get; set; }
        public DateTime PULLEDON { get; set; }
        public string REVIEWEDBY { get; set; }
        public DateTime REVIEWEDON { get; set; }
        public string CLOSED { get; set; }


        [NotMapped]
        public IEnumerable<SelectListItem> ListOfClosed { get; set; }


    }
}