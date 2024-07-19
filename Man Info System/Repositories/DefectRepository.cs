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
    public class DefectRepository
    {

        public IEnumerable<Defect> GetDefectList(bool Revised = false, string UserGroup=null)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                var p = new OracleDynamicParameters();
                return conn.Query<Defect>("SELECT C.ID,C.RSN_NO,C.PO_NO,C.MODEL,C.BOXID,TO_CHAR(C.PACK_DATE, 'DD-MON-YYYY') As PACK_DATE,C.FAMILY,C.SN,C.DEFECT,C.PMP,C.DOCUMENT,AC1.FULL_NAME AS CREATEDBY,C.CREATEDON,AC2.FULL_NAME AS REVISEDBY,C.REVISEDON,AC3.FULL_NAME AS REVIEWEDBY,C.REVIEWEDON,AC4.FULL_NAME AS PULLEDBY,C.PULLEDON,AC5.FULL_NAME AS UNPACKEDBY,C.UNPACKEDON,C.CLOSED FROM CUST_DEFECT_SELF_RELEASE C LEFT JOIN ACL_MASTER AC1 ON C.CREATEDBY=AC1.USER_ID LEFT JOIN ACL_MASTER AC2 ON C.REVISEDBY=AC2.USER_ID LEFT JOIN ACL_MASTER AC3 ON C.REVIEWEDBY=AC3.USER_ID LEFT JOIN ACL_MASTER AC4 ON C.PULLEDBY=AC4.USER_ID LEFT JOIN ACL_MASTER AC5 ON C.UNPACKEDBY=AC5.USER_ID WHERE C.CLOSED='N'", p, commandType: CommandType.Text);
            }
        }


        public IEnumerable<Defect> FilterByBOXIDSN(string boxidsn = "", string UserGroup = "")
        {
            string Sql = "";
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                var p = new OracleDynamicParameters();
                if (boxidsn.Equals("") || boxidsn == null) {
                    Sql = "SELECT C.ID,C.RSN_NO,C.PO_NO,C.MODEL,C.BOXID,TO_CHAR(C.PACK_DATE, 'DD-MON-YYYY') As PACK_DATE,C.FAMILY,C.SN,C.DEFECT,C.PMP,C.DOCUMENT,AC1.FULL_NAME AS CREATEDBY,C.CREATEDON,AC2.FULL_NAME AS REVISEDBY,C.REVISEDON,AC3.FULL_NAME AS REVIEWEDBY,C.REVIEWEDON,AC4.FULL_NAME AS PULLEDBY,C.PULLEDON,AC5.FULL_NAME AS UNPACKEDBY,C.UNPACKEDON,C.CLOSED FROM CUST_DEFECT_SELF_RELEASE C LEFT JOIN ACL_MASTER AC1 ON C.CREATEDBY=AC1.USER_ID LEFT JOIN ACL_MASTER AC2 ON C.REVISEDBY=AC2.USER_ID LEFT JOIN ACL_MASTER AC3 ON C.REVIEWEDBY=AC3.USER_ID LEFT JOIN ACL_MASTER AC4 ON C.PULLEDBY=AC4.USER_ID LEFT JOIN ACL_MASTER AC5 ON C.UNPACKEDBY=AC5.USER_ID WHERE C.CLOSED='N'";
                }
                else
                {
                    Sql = "SELECT C.ID,C.RSN_NO,C.PO_NO,C.MODEL,C.BOXID,TO_CHAR(C.PACK_DATE, 'DD-MON-YYYY') As PACK_DATE,C.FAMILY,C.SN,C.DEFECT,C.PMP,C.DOCUMENT,AC1.FULL_NAME AS CREATEDBY,C.CREATEDON,AC2.FULL_NAME AS REVISEDBY,C.REVISEDON,AC3.FULL_NAME AS REVIEWEDBY,C.REVIEWEDON,AC4.FULL_NAME AS PULLEDBY,C.PULLEDON,AC5.FULL_NAME AS UNPACKEDBY,C.UNPACKEDON,C.CLOSED FROM CUST_DEFECT_SELF_RELEASE C LEFT JOIN ACL_MASTER AC1 ON C.CREATEDBY=AC1.USER_ID LEFT JOIN ACL_MASTER AC2 ON C.REVISEDBY=AC2.USER_ID LEFT JOIN ACL_MASTER AC3 ON C.REVIEWEDBY=AC3.USER_ID LEFT JOIN ACL_MASTER AC4 ON C.PULLEDBY=AC4.USER_ID LEFT JOIN ACL_MASTER AC5 ON C.UNPACKEDBY=AC5.USER_ID WHERE (UPPER(C.BOXID) LIKE '%" + boxidsn + "%' OR UPPER(C.SN) LIKE '%" + boxidsn + "%' OR UPPER(C.RSN_NO) LIKE '%"+ boxidsn +"%')";
                }
                return conn.Query<Defect>(Sql, p, commandType: CommandType.Text);
            }
        }


        public Defect GetDefectById(string CaseId)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                var p = new OracleDynamicParameters();
                p.Add(":CaseId", value: CaseId, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                return conn.Query<Defect>("SELECT C.ID,C.RSN_NO,C.PO_NO,C.MODEL,C.BOXID,TO_CHAR(C.PACK_DATE, 'DD-MON-YYYY') As PACK_DATE,C.FAMILY,C.SN,C.DEFECT,C.PMP,C.DOCUMENT,AC1.FULL_NAME AS CREATEDBY,C.CREATEDON,AC2.FULL_NAME AS REVISEDBY,C.REVISEDON,AC3.FULL_NAME AS REVIEWEDBY,C.REVIEWEDON,AC4.FULL_NAME AS PULLEDBY,C.PULLEDON,AC5.FULL_NAME AS UNPACKEDBY,C.UNPACKEDON,C.CLOSED FROM CUST_DEFECT_SELF_RELEASE C LEFT JOIN ACL_MASTER AC1 ON C.CREATEDBY=AC1.USER_ID LEFT JOIN ACL_MASTER AC2 ON C.REVISEDBY=AC2.USER_ID LEFT JOIN ACL_MASTER AC3 ON C.REVIEWEDBY=AC3.USER_ID LEFT JOIN ACL_MASTER AC4 ON C.PULLEDBY=AC4.USER_ID LEFT JOIN ACL_MASTER AC5 ON C.UNPACKEDBY=AC5.USER_ID WHERE C.ID=:CaseId", p, commandType: CommandType.Text).SingleOrDefault();
            }
        }

        public BoxData GetBoxDataByBoxId(string BoxId)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                var p = new OracleDynamicParameters();
                p.Add(":BOXID", value: BoxId, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                return conn.Query<BoxData>("SELECT BOXID,TO_CHAR(DATETIME, 'DD-MON-YYYY') AS PACK_DATE, FAMILY FROM CUST_E_COC_APPROVAL WHERE BOXID=:BOXID AND DATETIME = (SELECT MAX(datetime) FROM  CUST_E_COC_APPROVAL WHERE BOXID=:BOXID)", p, commandType: CommandType.Text).SingleOrDefault();
            }
        }

        public int ReviseDefect(Defect objDefect, string UserGroup)
        {
            string Sql = "";
            using (var conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        var p = new OracleDynamicParameters();
                        p.Add(":ID", value: objDefect.ID, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add(":REVISEDBY", value: objDefect.REVISEDBY, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add(":CLOSED", value: objDefect.CLOSED, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        if (UserGroup.Equals("PRODUCTION"))
                            Sql = "UPDATE CUST_DEFECT_SELF_RELEASE SET PULLEDBY=:REVISEDBY, PULLEDON=SYSDATE, CLOSED=:CLOSED WHERE ID=:ID";
                        else if (UserGroup.Equals("REVISER"))
                            Sql = "UPDATE CUST_DEFECT_SELF_RELEASE SET REVISEDBY=:REVISEDBY, REVISEDON=SYSDATE, CLOSED=:CLOSED WHERE ID=:ID";
                        else if (UserGroup.Equals("IT"))
                            Sql = "UPDATE CUST_DEFECT_SELF_RELEASE SET UNPACKEDBY=:REVISEDBY, UNPACKEDON=SYSDATE, CLOSED=:CLOSED WHERE ID=:ID";
                        else if (UserGroup.Equals("PQE") || UserGroup.Equals("SELF_RELEASE"))
                            Sql = "UPDATE CUST_DEFECT_SELF_RELEASE SET REVIEWEDBY=:REVISEDBY, REVIEWEDON=SYSDATE, CLOSED=:CLOSED  WHERE ID=:ID";
                        conn.Execute(Sql, p, null, 0, CommandType.Text);
                        //int ServerId = p.Get<int>("O_SERVERID");
                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
            return 1;
        }

        public int InsertDefect(Defect objDefect)
        {
            using (var conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        var p = new OracleDynamicParameters();
                        p.Add(":ID", value: objDefect.ID, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add(":RSN_NO", value: objDefect.RSN_NO, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add(":PO_NO", value: objDefect.PO_NO, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add(":MODEL", value: objDefect.MODEL, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add(":BOXID", value: objDefect.BOXID, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add(":PACK_DATE", value: objDefect.PACK_DATE, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add(":FAMILY", value: objDefect.FAMILY, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add(":SN", value: objDefect.SN, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add(":DEFECT", value: objDefect.DEFECT, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add(":PMP", value: objDefect.PMP, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add(":DOCUMENT", value: objDefect.DOCUMENT, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add(":CREATEDBY", value: objDefect.CREATEDBY, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        conn.Execute("INSERT INTO CUST_DEFECT_SELF_RELEASE(ID,RSN_NO,PO_NO,MODEL,BOXID,PACK_DATE,FAMILY,SN,DEFECT,PMP,DOCUMENT,CREATEDBY,CREATEDON,CLOSED) VALUES(:ID,:RSN_NO,:PO_NO,:MODEL,:BOXID,:PACK_DATE,:FAMILY,:SN,:DEFECT,:PMP,:DOCUMENT,:CREATEDBY,SYSDATE,'N')", p, null, 0, CommandType.Text);
                        //int ServerId = p.Get<int>("O_SERVERID");
                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
            return 1;
        }
    }
}