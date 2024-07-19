using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoRSN.Models;

using System.Configuration;
using Dapper;
using AutoRSN.Utility;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace AutoRSN.Repositories
{
    public class DashBoardRepository
    {

        public IEnumerable<DashBoard> GetDetailByTodayDate()
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
                {
                    var p = new OracleDynamicParameters();
                    return conn.Query<DashBoard>("SELECT APP.BOXID,CASE WHEN APP.IPQA_APPROVED='Y' THEN 'APPROVED' WHEN APP.IPQA_APPROVED='N' THEN 'PENDING' ELSE 'REJECTED' END AS QA_APPROVER_STATUS,CASE WHEN QCU.FULL_NAME IS NULL THEN '-' ELSE QCU.FULL_NAME END AS QA_APPROVER_USER, CASE WHEN APP.IPQA_REJECT_REMARK IS NULL THEN '-' ELSE APP.IPQA_REJECT_REMARK END  AS QA_APPROVER_COMMENT,CASE WHEN APP.APPROVED='Y' THEN 'APPROVED' WHEN APP.APPROVED='N' THEN 'PENDING' ELSE 'REJECTED' END AS CQE_CSQR_APPROVER_STATUS,CASE WHEN CCU.FULL_NAME IS NULL THEN '-' ELSE CCU.FULL_NAME END AS CQE_CSQR_APPROVER_USER, CASE WHEN APP.REJECT_REMARK IS NULL THEN '-' ELSE APP.REJECT_REMARK END AS CQE_CSQR_APPROVER_COMMENT " +
                                                 "FROM CUST_E_COC_APPROVAL APP LEFT JOIN CUST_E_COC_USER QCU ON APP.IPQA_EMP_NO = QCU.EMPID " +
                                                 "LEFT JOIN CUST_E_COC_USER CCU ON APP.EMP_NO = CCU.EMPID " +
                                                 "WHERE TO_CHAR(APP.DATETIME, 'dd/mm/rrrr') = TO_CHAR(sysdate, 'dd/mm/rrrr') ORDER BY APP.DATETIME DESC", null, commandType: CommandType.Text);
                }
            }
            catch
            {
                throw;
            }

        }
    }
}