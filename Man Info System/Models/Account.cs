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
        [Required]
        [Display(Name = "Username")]
        public string USERNAME { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]

        public string PASSWORD { get; set; }
        public string FULL_NAME { get; set; }
        public string EN { get; set; }
        public string EMAIL { get; set; }
        public string AD_MODE { get; set; }
        public string AD_USER { get; set; }
        public string EMP_CODE { get; set; }
        public string USER_GROUP { get; set; }
        public string STATUS { get; set; }

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