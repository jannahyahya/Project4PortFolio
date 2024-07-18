using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoRSN.Models
{
    public class StorageCode
    {
        public int no { get; set; }
        public string storageType { get; set; }
        public string bin { get; set; }
        public bool isputaway { get; set; }
        public bool isinventory { get; set; }
    }
}