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
        [Display(Name = "Employee Id")]
        public string EMPID { get; set; }
        [Required]
        [Display(Name = "Full Name")]
        public string FULL_NAME { get; set; }
        [Required]
        [Display(Name = "Employee Code")]
        public string EMP_CODE { get; set; }
        [Required]
        [Display(Name = "Employee Group")]
        public string USER_GROUP { get; set; }


        [NotMapped]
        public IEnumerable<SelectListItem> ListOfGroup { get; set; }

    }
}