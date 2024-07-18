using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoRSN.Models
{
    public class Account
    {
        [Display(Name = "Server")]
        //[ValidateServer(ErrorMessage = "The Server field is required.")]
        public int SERVERID { get; set; }
        [Required]
        [Display(Name = "Username")]
        public string USERNAME { get; set; }

           // [Required]
            [Display(Name = "USER LVL")]
            public string LEVEL { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string PASSWORD { get; set; }


        [NotMapped]
        public IEnumerable<Server> LISTSERVER { get; set; }
    }

    public class ValidateServer : ValidationAttribute
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