
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AutoRSN.Repositories
{
    public class SPRepository
    {
        public string getModel(string sn , string connstr)
        {

            string model = "";

            OracleConnection conn = new OracleConnection(connstr);
            conn.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"select model from product_master where productid = (select productid from so_master where so = (select so from sn_master where sn = '{sn}' AND status = 'INPROCESS'))";
            OracleDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                model = reader.GetValue(0).ToString();
            }


            conn.Dispose();
            conn.Close();


            return model;

        }

    }
}