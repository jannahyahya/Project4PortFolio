using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoRSN.Models
{
    public class DashBoard
    {
        public string BOXID { get; set; }
        public string QA_APPROVER_STATUS { get; set; }
        public string QA_APPROVER_USER { get; set; }
        public string QA_APPROVER_COMMENT { get; set; }
        public string CQE_CSQR_APPROVER_STATUS { get; set; }
        public string CQE_CSQR_APPROVER_USER { get; set; }
        public string CQE_CSQR_APPROVER_COMMENT { get; set; }
    }
}