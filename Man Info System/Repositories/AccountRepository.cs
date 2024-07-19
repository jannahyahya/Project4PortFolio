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
    public class AccountRepository
    {
        public Account GetUserByUsername(string Username)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                var p = new OracleDynamicParameters();
                p.Add(":Username", value: Username.ToUpper(), dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                return conn.Query<Account>("SELECT AM.USER_ID AS USERNAME,AM.PASSWORD,AM.FULL_NAME,AM.EN,AM.EMAIL,AM.AD_MODE,AM.AD_USER,CU.EMP_CODE,CU.USER_GROUP,AM.STATUS FROM ACL_MASTER AM INNER JOIN CUST_E_COC_USER CU ON AM.EN = CU.EMPID WHERE (UPPER(AM.USER_ID)=:Username OR UPPER(AM.AD_USER)=:Username) AND LENGTH(AM.USER_ID)>=8", p, commandType: CommandType.Text).SingleOrDefault();
            }
        }
    }
}