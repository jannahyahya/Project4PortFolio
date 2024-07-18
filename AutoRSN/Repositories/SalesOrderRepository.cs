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
    public class SalesOrderRepository
    {
        public IEnumerable<Customer> GetSalesOrderBySearch(int ServerId, string CCMG = null)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                var p = new OracleDynamicParameters();
                p.Add("CUSTOMER", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                p.Add("P_SERVERID", value: ServerId, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                p.Add("P_FILTERBYCCMG", value: CCMG, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                return conn.Query<Customer>("RSN_ST_CUSTOMER_GETSEARCH", p, commandType: CommandType.StoredProcedure);
            }
        }

    }
}