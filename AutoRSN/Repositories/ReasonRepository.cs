using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoRSN.Repositories.Abstract;
using AutoRSN.Models;
using Oracle.DataAccess.Client;
using System.Configuration;
using Dapper;
using AutoRSN.Utility;
using System.Data;

namespace AutoRSN.Repositories
{
    public class ReasonRepository : IReasonRepository
    {
        public IEnumerable<Reason> GetReason(int ServerId,bool IsActive = false)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                var p = new OracleDynamicParameters();
                p.Add("REASON", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                p.Add("P_SERVERID", value: ServerId, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                p.Add("P_STATUS", value: (IsActive) ? "Y" : "N", dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                return conn.Query<Reason>("RSN_ST_REASON_GETLIST", p, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Reason> GetReasonBySearch(int ServerId,string Reason = null)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                var p = new OracleDynamicParameters();
                p.Add("REASON", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                p.Add("P_SERVERID", value: ServerId, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                p.Add("P_FILTERBYREASON", value: Reason, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                return conn.Query<Reason>("RSN_ST_REASON_GETSEARCH", p, commandType: CommandType.StoredProcedure);
            }
        }

        public Reason GetReasonByReasonId(int ServerId ,int ReasonId)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                var p = new OracleDynamicParameters();
                p.Add("REASON", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                p.Add("P_SERVERID", value: ServerId, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                p.Add("P_REASONID", value: ReasonId, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                return conn.Query<Reason>("RSN_ST_REASON_GETBYREASONID", p, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }

        public Reason IsExistReason(int ServerId, string Reason = null)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                var p = new OracleDynamicParameters();
                p.Add("REASON", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                p.Add("P_SERVERID", value: ServerId, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                p.Add("P_REASON", value: Reason, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                return conn.Query<Reason>("RSN_ST_REASON_EXIST", p, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }


        public void deleteReason(int reasonID)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {

                conn.Open();
                OracleCommand delCmd = new OracleCommand();
                delCmd.Connection = conn;
                delCmd.CommandType = CommandType.Text;
                delCmd.CommandText = string.Format("delete from rsn_st_reason where reasonid = {0}",reasonID);
                delCmd.ExecuteNonQuery();

                conn.Dispose();
                conn.Close();
            }


        }
        public int UpdateReasonByReasonId(int ServerId, Reason objReason)
        {

            using (var conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        var p = new OracleDynamicParameters();
                        p.Add("P_SERVERID", value: ServerId, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                        p.Add("P_REASONID", value: objReason.REASONID, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                        p.Add("P_REASON", value: objReason.REASON, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_DESCRIPTION", value: objReason.DESCRIPTION, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_STATUS", value: objReason.STATUS, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_LOGINUSER", value: objReason.MODIFIEDBY, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("O_REASONID", dbType: OracleDbType.Int32, direction: ParameterDirection.Output);
                        conn.Execute("RSN_ST_REASON_UPDATE", p, null, 0, CommandType.StoredProcedure);
                        //int ServerId = p.Get<int>("P_SERVERID");
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

        public int InsertReason(int ServerId, Reason objReason)
        {
            using (var conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        var p = new OracleDynamicParameters();
                        p.Add("P_SERVERID", value: ServerId, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                        p.Add("P_REASON", value: objReason.REASON, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_DESCRIPTION", value: objReason.DESCRIPTION, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_STATUS", value: objReason.STATUS, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_LOGINUSER", value: objReason.CREATEDBY, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("O_REASONID", dbType: OracleDbType.Int32, direction: ParameterDirection.Output);
                        conn.Execute("RSN_ST_REASON_INSERT", p, null, 0, CommandType.StoredProcedure);
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