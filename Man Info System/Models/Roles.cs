using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;


namespace AutoRSN.Models
{
    public class Roles
    {
        [Key]
        [Required]
        [Display(Name = "Employee id")]
        public string EMPID { get; set; }
        [Display(Name = "Employee name")]
        public string FULL_NAME { get; set; }
        [Key]
        [Required]
        [Display(Name = "Family")]
        public string FAMILY { get; set; }
       
        [NotMapped]
        public IEnumerable<User> ListOfEmployeeId { get; set; }
        public IEnumerable<SelectListItem> ListOfFamily { get; set; }
    }
}