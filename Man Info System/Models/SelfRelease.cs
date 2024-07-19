using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;


namespace AutoRSN.Models
{
    public class SelfRelease
    {
        [Key]
        public string BOXID { get; set; }
        public string PACK_PROFILE_ID { get; set; }
        public string SHIP_TO { get; set; }
        public string FAMILY { get; set; }
        [Key]
        public string SEQ { get; set; }
        [Key]
        public string SN { get; set; }
        public string IS_FAI { get; set; }
        public string SO { get; set; }
        public string MODEL { get; set; }
        public string REVISION { get; set; }
        public string DRAWING_REV { get; set; }
        public string PARTLIST_REV { get; set; }
        public string CUST_PN { get; set; }
        public string DRAWING_NO { get; set; }
        public string CMY_PN { get; set; }
        public string SELF_RELEASE_STATUS { get; set; }
        public string SCD_REV { get; set; }
        public string PO_NUM { get; set; }
        public string LINE_ITEM_NO { get; set; }
        public string PACKING_SLIP_NO { get; set; }
        public string CCA_RMRA { get; set; }
        public string PRE_SHIP_MRB { get; set; }
        // COMP_RMRA, SHIPSET_REVISION ( Retrive From other method )
        public string COMP_RMRA { get; set; }
        public string SHIPSET_REVISION { get; set; }
        public string CQE_APPROVE { get; set; }
        public string IPQA_APPROVE { get; set; }
        public string REMARK { get; set; }
    }
}