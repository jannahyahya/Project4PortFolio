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
    public class EmailRepository
    {
        public IEnumerable<Email> GetEmail(int ServerId, bool IsActive = false)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                var p = new OracleDynamicParameters();
                p.Add("EMAIL", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                p.Add("P_SERVERID", value: ServerId, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                p.Add("P_STATUS", value: (IsActive) ? "Y" : "N", dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                return conn.Query<Email>("RSN_ST_EMAIL_GETLIST", p, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Email> GetEmailBySearch(int ServerId, string CCMG = null)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                var p = new OracleDynamicParameters();
                p.Add("EMAIL", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                p.Add("P_SERVERID", value: ServerId, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                p.Add("P_FILTERCCET", value: CCMG, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                return conn.Query<Email>("RSN_ST_EMAIL_GETSEARCH", p, commandType: CommandType.StoredProcedure);
            }
        }

        public Email GetEmailByEmailId(int ServerId,int EmailId)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                var p = new OracleDynamicParameters();
                p.Add("EMAIL", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                p.Add("P_SERVERID", value: ServerId, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                p.Add("P_EMAILID", value: EmailId, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                return conn.Query<Email>("RSN_ST_EMAIL_GETBYEMAILID", p, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }

        public Email IsExistEmail(int ServerId, string CustomerCode = null, string EmailTitle = null)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                var p = new OracleDynamicParameters();
                p.Add("EMAIL", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                p.Add("P_SERVERID", value: ServerId, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                p.Add("P_CUSTOMERCODE", value: CustomerCode, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                p.Add("P_EMAILTITLE", value: EmailTitle, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                return conn.Query<Email>("RSN_ST_EMAIL_EXIST", p, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }

        public int UpdateEmail(int ServerId, Email objEmail)
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
                        p.Add("P_CUSTOMERCODE", value: objEmail.CUSTOMERCODE, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_EMAILID", value: objEmail.EMAILID, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                        p.Add("P_EMAILTITLE", value: objEmail.EMAILTITLE, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_ATTENTIONNAME", value: objEmail.ATTENTIONNAME, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_EMAILTO", value: objEmail.EMAILTO, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_EMAILCC", value: objEmail.EMAILCC, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_STATUS", value: objEmail.STATUS, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_LOGINUSER", value: objEmail.MODIFIEDBY, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        conn.Execute("RSN_ST_EMAIL_UPDATE", p, null, 0, CommandType.StoredProcedure);
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

        public int InsertEmail(int ServerId, Email objEmail)
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
                        p.Add("P_CUSTOMERCODE", value: objEmail.CUSTOMERCODE, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_EMAILTITLE", value: objEmail.EMAILTITLE, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_ATTENTIONNAME", value: objEmail.ATTENTIONNAME, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_EMAILTO", value: objEmail.EMAILTO, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_EMAILCC", value: objEmail.EMAILCC, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_STATUS", value: objEmail.STATUS, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_LOGINUSER", value: objEmail.CREATEDBY, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        conn.Execute("RSN_ST_EMAIL_INSERT", p, null, 0, CommandType.StoredProcedure);
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