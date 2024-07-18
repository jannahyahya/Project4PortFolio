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
    public class ServerRepository : IServerRepository
    {
        public IEnumerable<Server> GetServer(bool IsActive= false)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                var p = new OracleDynamicParameters();
                p.Add("SERVER", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                p.Add("P_STATUS", value: (IsActive)? "Y":"N", dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                return conn.Query<Server>("RSN_ST_SERVER_GETLIST", p, commandType: CommandType.StoredProcedure);
               
            }
        }
    
        public IEnumerable<Server> GetServerBySearch(string server = null)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                var p = new OracleDynamicParameters();
                p.Add("SERVER", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                p.Add("P_FILTERBYSERVER", value: server, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                return conn.Query<Server>("RSN_ST_SERVER_GETSEARCH", p, commandType: CommandType.StoredProcedure);
            }
        }

        public Server GetServerByServerId(int ServerId)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                var p = new OracleDynamicParameters();
                p.Add("SERVER", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                p.Add("P_SERVERID", value: ServerId, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                return conn.Query<Server>("RSN_ST_SERVER_GETBYSERVERID", p, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }

        public Server IsExistServer(string server=null)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                var p = new OracleDynamicParameters();
                p.Add("SERVER", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                p.Add("P_SERVER", value: server, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                return conn.Query<Server>("RSN_ST_SERVER_EXIST", p, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }

        public int UpdateServerByServerId(Server objServer)
        {

            using (var conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        var p = new OracleDynamicParameters();
                        p.Add("P_SERVERID", value: objServer.SERVERID, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                        p.Add("P_SERVER", value: objServer.SERVER, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_STATUS", value: objServer.STATUS, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_LOGINUSER", value: objServer.MODIFIEDBY, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("O_SERVERID", dbType: OracleDbType.Int32, direction: ParameterDirection.Output);
                        conn.Execute("RSN_ST_SERVER_UPDATE", p, null, 0, CommandType.StoredProcedure);
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

        public int InsertServer(Server objServer)
        {
            using (var conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        var p = new OracleDynamicParameters();
                        p.Add("O_SERVERID", dbType: OracleDbType.Int32, direction: ParameterDirection.Output);
                        p.Add("P_SERVER", value: objServer.SERVER, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_STATUS", value: objServer.STATUS, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_LOGINUSER", value: objServer.CREATEDBY, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        conn.Execute("RSN_ST_SERVER_INSERT", p, null, 0, CommandType.StoredProcedure);
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