using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoRSN.Models
{
    public class PermissionHolder //each customer has only 1 permission
    {
        public string Customer { get; set; }
        public string Permission { get; set; }
    }
}