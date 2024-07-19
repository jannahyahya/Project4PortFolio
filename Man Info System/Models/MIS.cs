using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoRSN.Models
{
    public class MIS
    {

        public string NO { get; set; }
        public string DATETIME { get; set; }
        public string PART_NUMBER { get; set; }
        public string QTY { get; set; }
        public string SN { get; set; }
        public string MODEL { get; set; }
        public string REFDES { get; set; }
        public string REQUESTEDBY { get; set; }
        public string ID_SCAN { get; set; }
        public string STYPE { get; set; }
        public string SBIN { get; set; }
        public string TR_NUMBER { get; set; }
        public string TO_NUMBER { get; set; }
        public string CREATEDBY { get; set; }
        public string RECEIVEDBY { get; set; }
        public string CREATEDON { get; set; } // requester date
        public string COMMENTS { get; set; }
        public string LOC_OWNER { get; set; }
        public string LOC_STYPE { get; set; }
        public string CONFIRMEDON { get; set; } // confirmed on
        public string REQUESTEREXT { get; set; }
        public string RECEIVEDON { get; set; } // requester received item on
        public string ID { get; set; }
        public string STATUS { get; set; }
        public string PREP_STATUS { get; set; }
        public string PROJECT { get; set; }
        //public string STATUS_COLOR { get; set; }
        //public string PREP_STATUS_COLOR { get; set; }

        public string ROW_COLOR { get; set; }
        public int IS_KITTING { get; set; }

        public int IS_KANBAN { get; set; }

        public string PREP_COMMENT { get; set; }

        public string PREP_BY { get; set; }
        public string PREP_DATE { get; set; }

        public string DISABLED { get; set; }

        public string KANBAN_STATUS { get; set; }



        public SelectList STATUSLIST { get; set; }
    }
}