using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoRSN.Models;
using System.Configuration;
using Dapper;
using AutoRSN.Utility;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace AutoRSN.Repositories
{
    public class SelfReleaseRepository
    {

        public IEnumerable<SelfRelease> GetDetailByBoxId(string BoxId, string UserGroup)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
                {
                    var p = new OracleDynamicParameters();
                    p.Add(":BOXID", value: BoxId, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                    return conn.Query<SelfRelease>("SELECT DISTINCT BM.BOXID,BM.PACK_PROFILE_ID,PAM.PACK_PROFILE_ID AS SHIP_TO,CA.FAMILY AS FAMILY,BD.SEQ,BD.SN ,CASE WHEN TR.SN IS NOT NULL THEN 'Y' ELSE 'N' END AS IS_FAI ,BD.SO,PM.MODEL,PM.REVISION,PPM.PAR2 AS DRAWING_REV,PPM.PAR3 AS PARTLIST_REV,PPM.PAR4 AS CUST_PN,PPM.PAR5 AS DRAWING_NO,PPM.PAR6 AS CMY_PN,PPM.PAR30 AS SCD_REV,PPM.PAR32 AS SELF_RELEASE_STATUS,PBM.PAR1 AS PO_NUM,PBM.PAR2 AS LINE_ITEM_NO,PBM.PAR5 AS PACKING_SLIP_NO,CASE WHEN PSM.PAR4 IS NULL THEN 'NA' ELSE PSM.PAR4 END AS CCA_RMRA,PSM.PAR5 AS PRE_SHIP_MRB, CA.APPROVED AS CQE_APPROVE, CA.IPQA_APPROVED AS IPQA_APPROVE " +
                                            "FROM BOX_MASTER BM INNER JOIN BOX_DETAIL BD ON BM.BOXID = BD.BOXID LEFT JOIN SO_MASTER SM ON SM.SO = BD.SO LEFT JOIN PRODUCT_MASTER PM ON PM.PRODUCTID = SM.PRODUCTID " +
                                            "LEFT JOIN PAR_PRODUCT_MASTER PPM ON PPM.PRODUCTID = SM.PRODUCTID LEFT JOIN PAR_BOX_MASTER PBM ON PBM.BOXID = BM.BOXID LEFT JOIN PAR_SO_MASTER PSM ON PSM.SO = BD.SO " +
                                            "LEFT JOIN PACK_PROFILE_MASTER PAM ON PAM.PACK_PROFILE_ID = BM.PACK_PROFILE_ID LEFT JOIN TRACKING TR ON TR.SN = BD.SN AND TR.SO = BD.SO AND TR.STATION = '3740' AND ITERATION = '1' " +
                                            "LEFT JOIN CUST_E_COC_APPROVAL CA ON CA.BOXID = BM.BOXID WHERE BM.BOXID = :BOXID AND CA.DATETIME =(SELECT MAX(DATETIME) FROM CUST_E_COC_APPROVAL WHERE BOXID=:BOXID) ORDER BY BM.BOXID,BD.SEQ ", p, commandType: CommandType.Text);
                    // + ((UserGroup.Equals("QA")) ? " AND CA.IPQA_APPROVED='N' AND CA.APPROVED='N'" : " AND CA.IPQA_APPROVED='Y'") +
                }
            }
            catch
            {
                throw;
            }

        }

        public IEnumerable<ShipSet> GetDetailByShipSet(string Shipset)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString())){
                    var p = new OracleDynamicParameters();
                    p.Add(":ShipSet", value: Shipset, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                    return conn.Query<ShipSet>("SELECT SHIP_SET, CCA_PN, CCA_REV, QTY, SET_PN, SET_PN_REV, REMARKS FROM CUST_SHIP_SET WHERE SHIP_SET=:ShipSet", p, commandType: CommandType.Text);
                }
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<Rmra_Model> GetDetailByRMRA_Model(string model)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
                {
                    var p = new OracleDynamicParameters();
                    p.Add(":Model", value: model, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                    return conn.Query<Rmra_Model>("SELECT MODEL, PN, PN_RMRA, BATCHNO, P_FLAG FROM PN_RMRA_MODEL WHERE MODEL =:MODEL ", p, commandType: CommandType.Text);
                }
            }
            catch
            {
                throw;
            }
        }

        public bool CheckCompRMRA(string SN , string SO, string PN, string BATCHNO)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
                {
                    var p = new OracleDynamicParameters();
                    p.Add(":SN", value: SN, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                    p.Add(":SO", value: SN, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                    p.Add(":PN", value: SN, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                    p.Add(":BATCHNO", value: SN, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                    return conn.Query<RMRA_EXIST>("SELECT SC.KIT,PC.BATCHNO FROM SN_COMPONENT SC, PAR_COMP_KIT PC WHERE SC.SN=:SN AND SC.PN=:PN AND SC.KIT=PC.KIT AND PC.BATCHNO =:BATCHNO AND SC.SO=:SO", p, commandType: CommandType.Text).Count()>0;
                }
            }
            catch
            {
                throw;
            }
        }

        public Int32 CheckNumberSNInBOX(string boxId, string[] sn)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
                {
                    //var p = new OracleDynamicParameters();
                    //p.Add(":BOXID", value: BOXID, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                    //p.Add(":SN", value: SN, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                    return conn.Query<string>("SELECT SN FROM BOX_DETAIL WHERE BOXID=:BOXID AND SN IN :SNS", new { BOXID = boxId, SNS=sn }, commandType: CommandType.Text).Count();
                }
            }
            catch
            {
                throw;
            }
        }

        public int ApproveByBoxId(List<ListOfSN> ListOfSN, string Boxid ,string Comment , string employee, string approverCode , string UserGroup)
        {
            Int16 FlagFail = 0;
            using (var conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        if (UserGroup.Equals("QA"))
                        {
                            var param = new OracleDynamicParameters();
                            param.Add(":P_BOXID", value: Boxid, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                            param.Add(":P_COMMENT", value: Comment, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                            param.Add(":P_EMPLOYEENO", value: employee, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                            param.Add(":P_APPROVERCODE", value: approverCode, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                            conn.Execute("UPDATE CUST_E_COC_APPROVAL SET IPQA_APPROVED='Y',IPQA_APPROVED_DATE=sysdate,IPQA_APPROVE_CODE=:P_APPROVERCODE,IPQA_EMP_NO=:P_EMPLOYEENO,IPQA_REJECT_REMARK=:P_COMMENT WHERE BOXID=:P_BOXID AND DATETIME = (SELECT MAX(DATETIME) FROM CUST_E_COC_APPROVAL WHERE BOXID=:P_BOXID)", param, null, 0, CommandType.Text);
                        }
                        else
                        {
                            if (ListOfSN != null)
                            {
                                for (int i = 0; i < ListOfSN.Count(); i++)
                                {
                                    if (ListOfSN[i].STATUS.ToString().ToUpper().Equals("FAIL")) FlagFail = 1;
                                    var p = new OracleDynamicParameters();
                                    p.Add("P_SN", value: ListOfSN[i].SN.Trim(), dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                                    p.Add("P_COMMENT", value: employee.ToUpper() + " : " + Boxid.ToUpper().Trim() + " : " + ListOfSN[i].STATUS.ToString().ToUpper() + " : " + ListOfSN[i].COMMENT.ToUpper().Trim(), dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                                    conn.Execute("SELF_RELEASE_SN_UPDATE", p, null, 0, CommandType.StoredProcedure);
                                }
                            }
                            var param = new OracleDynamicParameters();
                            param.Add(":P_BOXID", value: Boxid, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                            param.Add(":P_COMMENT", value: Comment, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                            param.Add(":P_EMPLOYEENO", value: employee, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                            param.Add(":P_APPROVERCODE", value: approverCode, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                            if (FlagFail == 1)
                                conn.Execute("UPDATE CUST_E_COC_APPROVAL SET APPROVED='R', APPROVED_DATE=sysdate,APPROVE_CODE=:P_APPROVERCODE,EMP_NO=:P_EMPLOYEENO,REJECT_REMARK=:P_COMMENT WHERE BOXID=:P_BOXID AND DATETIME = (SELECT MAX(DATETIME) FROM CUST_E_COC_APPROVAL WHERE BOXID=:P_BOXID)", param, null, 0, CommandType.Text);
                            else
                                conn.Execute("UPDATE CUST_E_COC_APPROVAL SET APPROVED='Y',APPROVED_DATE=sysdate,APPROVE_CODE=:P_APPROVERCODE,EMP_NO=:P_EMPLOYEENO,REJECT_REMARK=:P_COMMENT WHERE BOXID=:P_BOXID AND DATETIME = (SELECT MAX(DATETIME) FROM CUST_E_COC_APPROVAL WHERE BOXID=:P_BOXID)", param, null, 0, CommandType.Text);
                        }
                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
            return FlagFail;
        }
        
        public int RejectByBoxId(List<ListOfSN> ListOfSN, string Boxid, string Comment, string employee, string approverCode, string UserGroup)
        {
            Int16 FlagFail = 0;
            using (var conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        if (UserGroup.Equals("QA"))
                        {
                            var param = new OracleDynamicParameters();
                            param.Add(":P_BOXID", value: Boxid, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                            param.Add(":P_COMMENT", value: Comment, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                            param.Add(":P_EMPLOYEENO", value: employee, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                            param.Add(":P_APPROVERCODE", value: approverCode, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                            conn.Execute("UPDATE CUST_E_COC_APPROVAL SET IPQA_APPROVED='R',IPQA_APPROVED_DATE=sysdate,IPQA_APPROVE_CODE=:P_APPROVERCODE,IPQA_EMP_NO=:P_EMPLOYEENO,IPQA_REJECT_REMARK=:P_COMMENT WHERE BOXID=:P_BOXID AND DATETIME = (SELECT MAX(DATETIME) FROM CUST_E_COC_APPROVAL WHERE BOXID=:P_BOXID)", param, null, 0, CommandType.Text);
                        }
                        else
                        {
                            if (ListOfSN!=null) {
                                for (int i = 0; i < ListOfSN.Count(); i++)
                                {
                                    if (ListOfSN[i].STATUS.ToString().ToUpper().Equals("FAIL")) FlagFail = 1;
                                    var p = new OracleDynamicParameters();
                                    p.Add("P_SN", value: ListOfSN[i].SN.Trim(), dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                                    p.Add("P_COMMENT", value: employee.ToUpper() + " : " + Boxid.ToUpper().Trim() + " : " + ListOfSN[i].STATUS.ToString().ToUpper() + " : " + ListOfSN[i].COMMENT.ToUpper().Trim(), dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                                    conn.Execute("SELF_RELEASE_SN_UPDATE", p, null, 0, CommandType.StoredProcedure);
                                }
                            }
                            var param = new OracleDynamicParameters();
                            param.Add(":P_BOXID", value: Boxid, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                            param.Add(":P_COMMENT", value: Comment, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                            param.Add(":P_EMPLOYEENO", value: employee, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                            param.Add(":P_APPROVERCODE", value: approverCode, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                            if (FlagFail==1)
                            conn.Execute("UPDATE CUST_E_COC_APPROVAL SET APPROVED='R', APPROVED_DATE=sysdate,APPROVE_CODE=:P_APPROVERCODE,EMP_NO=:P_EMPLOYEENO,REJECT_REMARK=:P_COMMENT WHERE BOXID=:P_BOXID AND DATETIME = (SELECT MAX(DATETIME) FROM CUST_E_COC_APPROVAL WHERE BOXID=:P_BOXID)", param, null, 0, CommandType.Text);
                            else
                            conn.Execute("UPDATE CUST_E_COC_APPROVAL SET APPROVED='Y',APPROVED_DATE=sysdate,APPROVE_CODE=:P_APPROVERCODE,EMP_NO=:P_EMPLOYEENO,REJECT_REMARK=:P_COMMENT WHERE BOXID=:P_BOXID AND DATETIME = (SELECT MAX(DATETIME) FROM CUST_E_COC_APPROVAL WHERE BOXID=:P_BOXID)", param, null, 0, CommandType.Text);

                        }
                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
            return FlagFail;
        }
    }
}