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
    public class RolesRepository
    {

        public IEnumerable<Roles> GetRoles(bool IsActive = false)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                var p = new OracleDynamicParameters();
                p.Add(":STATUS", value: (IsActive) ? "A" : "D", dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                return conn.Query<Roles>("SELECT CR.*,CU.FULL_NAME FROM CUST_E_COC_ROLES CR LEFT JOIN CUST_E_COC_USER CU ON CR.EMPID=CU.EMPID ORDER BY FAMILY", p, commandType: CommandType.Text);
            }
        }
        
        public IEnumerable<Roles> GetRolesNotExistByEmployeeId(string EmployeeId)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                var p = new OracleDynamicParameters();
                p.Add(":EMPLOYEEID", value: EmployeeId , dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                return conn.Query<Roles>("SELECT DISTINCT CATAGORY AS EMPID , CATAGORY AS FAMILY FROM PRODUCT_MASTER WHERE CATAGORY NOT IN (SELECT FAMILY FROM CUST_E_COC_ROLES WHERE EMPID=:EMPLOYEEID) ORDER BY CATAGORY", p, commandType: CommandType.Text);
            }
        }


        public IEnumerable<Roles> GetRolesByEmployeeId(string EmployeeId)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                var p = new OracleDynamicParameters();
                p.Add(":EMPLOYEEID", value: EmployeeId, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                return conn.Query<Roles>("SELECT CR.*,CU.FULL_NAME FROM CUST_E_COC_ROLES CR LEFT JOIN CUST_E_COC_USER CU ON CR.EMPID=CU.EMPID WHERE CR.EMPID=:EMPLOYEEID ORDER BY FAMILY", p, commandType: CommandType.Text);
            }
        }

        public Roles GetRolesByEmpIdFamily(string EmployeeId, string Family)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                var p = new OracleDynamicParameters();
                p.Add(":EMPLOYEEID", value: EmployeeId, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                p.Add(":FAMILY", value: Family, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                return conn.Query<Roles>("SELECT CR.*,CU.FULL_NAME FROM CUST_E_COC_ROLES CR LEFT JOIN CUST_E_COC_USER CU ON CR.EMPID=CU.EMPID WHERE CR.EMPID=:EMPLOYEEID AND CR.FAMILY=:FAMILY  ORDER BY FAMILY", p, commandType: CommandType.Text).SingleOrDefault();
            }
        }
        

        public Roles IsExistRoles(string EmployeeId, string Family)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                var p = new OracleDynamicParameters();
                p.Add(":EMPLOYEEID", value: EmployeeId, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                p.Add(":FAMILY", value: Family, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                return conn.Query<Roles>("SELECT CR.*,CU.FULL_NAME FROM CUST_E_COC_ROLES CR LEFT JOIN CUST_E_COC_USER CU ON CR.EMPID=CU.EMPID WHERE CR.EMPID=:EMPLOYEEID AND CR.FAMILY=:FAMILY  ORDER BY FAMILY", p, commandType: CommandType.Text).SingleOrDefault();
            }
        }

        public int InsertRolesByEmployeeId(Roles objRoles)
        {
            using (var conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        var p = new OracleDynamicParameters();
                        p.Add(":EMPLOYEEID", value: objRoles.EMPID, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add(":FAMILY", value: objRoles.FAMILY, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        conn.Execute("INSERT INTO CUST_E_COC_ROLES(EMPID,FAMILY) VALUES (:EMPLOYEEID,:FAMILY)", p, null, 0, CommandType.Text);
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

        public int DeleteRolesByEmployeeId(string EmployeeId, string Family)
        {
            using (var conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        var p = new OracleDynamicParameters();
                        p.Add(":EMPLOYEEID", value: EmployeeId, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add(":FAMILY", value: Family, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        conn.Execute("DELETE FROM CUST_E_COC_ROLES WHERE EMPID=:EMPLOYEEID AND FAMILY=:FAMILY", p, null, 0, CommandType.Text);
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