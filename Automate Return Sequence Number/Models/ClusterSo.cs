using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoRSN.Models
{
    public class ClusterSo
    {
        public string custGroupName;
        public string kunnr2; // ship to 
        public string exportControl; //y/n
        public string auart; //so type
        public string rsnno;
        public List<Openso> listItem = new List<Openso>();
        public string remarks;
        public string link;
        public string kunnr1; //used by batch upload
        public string shipdate; //used by batch upload;

    }
}