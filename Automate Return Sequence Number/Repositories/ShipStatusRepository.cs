using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace AutoRSN.Repositories
{
    public class ShipStatusRepository
    {
        public bool updateShipStatus(string rsnno)
        {
            bool valid = false;
            OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString());
            conn.Open();
            OracleCommand updateRSNFinal12 = conn.CreateCommand();
            updateRSNFinal12.CommandText = $"update z_t_rsn_final set SHIPSTATUS = 'YES',SHIPSTATUSON=sysdate where rsnno = '{rsnno}'";
            updateRSNFinal12.CommandType = CommandType.Text;
            try
            {
                updateRSNFinal12.ExecuteNonQuery();
                valid = true;
            }
            catch
            {


            }
            
            conn.Dispose();
            conn.Close();

            return valid;
        }

    }

}