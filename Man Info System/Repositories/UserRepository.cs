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
    public class UserRepository
    {
        public IEnumerable<User> GetUser(bool IsActive = false)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                var p = new OracleDynamicParameters();
                p.Add(":STATUS", value: (IsActive) ? "A" : "D", dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                return conn.Query<User>("SELECT CU.* FROM CUST_E_COC_USER CU INNER JOIN ACL_MASTER AM ON CU.EMPID=AM.EN WHERE AM.STATUS=:STATUS AND CU.USER_GROUP IN ('CQE','CSQR') AND LENGTH(AM.USER_ID)=8", p, commandType: CommandType.Text);
            }
        }

        public IEnumerable<User> GetUserBySearch(string user = null)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                var p = new OracleDynamicParameters(); 
                p.Add(":USERID", value: "%"+ user.ToUpper()+ "%", dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                return conn.Query<User>("SELECT CU.* FROM CUST_E_COC_USER CU INNER JOIN ACL_MASTER AM ON CU.EMPID=AM.EN WHERE (CU.EMPID LIKE :USERID OR UPPER(CU.FULL_NAME) LIKE :USERID)  AND LENGTH(AM.USER_ID)=8", p, commandType: CommandType.Text);
            }
        }

        public User IsExistUser(string EmployeeId = null)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                var p = new OracleDynamicParameters();
                p.Add(":EMPLOYEEID", value: EmployeeId, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                return conn.Query<User>("SELECT * FROM CUST_E_COC_USER WHERE EMPID=:EMPLOYEEID", p, commandType: CommandType.Text).SingleOrDefault();
            }
        }

        public User GetUserByEmployeeid(string Employeeid)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                var p = new OracleDynamicParameters();
                p.Add(":EMPLOYEEID", value: Employeeid, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                return conn.Query<User>("SELECT CU.* FROM CUST_E_COC_USER CU INNER JOIN ACL_MASTER AM ON CU.EMPID=AM.EN WHERE CU.EMPID=:EMPLOYEEID AND LENGTH(AM.USER_ID)=8", p, commandType: CommandType.Text).SingleOrDefault();
            }
        }

        public int UpdateUserByEmployeeId(User objUser)
        {
            using (var conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        var p = new OracleDynamicParameters();
                        p.Add(":EMPLOYEEID", value: objUser.EMPID, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add(":FULL_NAME", value: objUser.FULL_NAME, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add(":EMPLOYEECODE", value: objUser.EMP_CODE, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add(":USER_GROUP", value: objUser.USER_GROUP, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        conn.Execute("UPDATE CUST_E_COC_USER SET FULL_NAME=:FULL_NAME,EMP_CODE=:EMPLOYEECODE,USER_GROUP=:USER_GROUP WHERE EMPID=:EMPLOYEEID", p, null, 0, CommandType.Text);
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

        public int InsertUser(User objUser)
        {
            using (var conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        var p = new OracleDynamicParameters();
                        p.Add(":EMPLOYEEID", value: objUser.EMPID, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add(":FULL_NAME", value: objUser.FULL_NAME, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add(":EMPLOYEECODE", value: objUser.EMP_CODE, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add(":USER_GROUP", value: objUser.USER_GROUP, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        conn.Execute("INSERT INTO CUST_E_COC_USER(EMPID,FULL_NAME,EMP_CODE,USER_GROUP) VALUES (:EMPLOYEEID,:FULL_NAME,:EMPLOYEECODE,:USER_GROUP)", p, null, 0, CommandType.Text);
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