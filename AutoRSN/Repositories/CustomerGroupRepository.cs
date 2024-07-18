using AutoRSN.Models;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AutoRSN.Repositories
{
    public class CustomerGroupRepository
    {
        public bool checkCustomerGroupExist(string custGroup)
        {
            var conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=rsn;Password=o5rsntcp;");
            conn.Open();

            OracleCommand pull = conn.CreateCommand();
            pull.CommandText = string.Format("select * from RSN_ST_CUSTOMER_GROUP where CUSTOMERNAME = '{0}'",custGroup);
            pull.CommandType = CommandType.Text;
            OracleDataReader read = pull.ExecuteReader();
           
            System.Diagnostics.Debug.WriteLine("pull from check function = " + custGroup);
            if (read.HasRows)
            {
                conn.Dispose();
                conn.Close();
                 return true;
            }
               
            else
            {
                conn.Dispose();
                conn.Close();
                return false;
            }
               


        }

        public bool deleteCustomerGroup(string customerGroup)
        {
            bool valid = false;
            var conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=rsn;Password=o5rsntcp;");
            conn.Open();

            OracleCommand pull = conn.CreateCommand();
            pull.CommandText = string.Format("delete from RSN_ST_CUSTOMER_GROUP WHERE CUSTOMERNAME = '{0}'", customerGroup);
            pull.CommandType = CommandType.Text;
            try
            {
                pull.ExecuteNonQuery();
                valid = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error DELETING =" + ex.Message);

            }

            conn.Dispose();
            conn.Close();
            return valid;

        }

        public CustomerGroup getCustomerGroup(string custGroupName)
        {

            CustomerGroup custGroup = new CustomerGroup();

            OracleConnection conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=rsn;Password=o5rsntcp;");
            conn.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = string.Format("SELECT DISTINCT CUSTOMERNAME,LINECOUNT FROM rsn_st_customer_group where CUSTOMERNAME='{0}'",custGroupName);
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

               // try
               // {
                   // CustomerGroup custGrouptemp = new CustomerGroup();
                    // custGroup.GROUPID = Convert.ToString(dr.GetValue(0));
                    custGroup.CUSTOMERNAME = Convert.ToString(dr.GetValue(0));
                    custGroup.LINECOUNT = Convert.ToInt32(dr.GetValue(1));
                    //return custGroup;


               // }
               // catch (Exception e)
               // {

               // }
            }

            conn.Close();
            conn.Dispose();
            return custGroup;

        }


        public bool insertCustomerGroup(CustomerGroup custGroup)
        {

            System.Diagnostics.Debug.WriteLine("pull from insert function = " + custGroup.CUSTOMERNAME);
            bool valid = false;
            var conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=rsn;Password=o5rsntcp;");
            conn.Open();

            OracleCommand pull = conn.CreateCommand();
            pull.CommandText = string.Format("insert into RSN_ST_CUSTOMER_GROUP(CUSTOMERNAME,LINECOUNT) VALUES('{0}',{1})", custGroup.CUSTOMERNAME,custGroup.LINECOUNT);
            pull.CommandType = CommandType.Text;
            try
            {
                 pull.ExecuteNonQuery();
                 valid = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error Inserting =" + ex.Message);

            }

            conn.Dispose();
            conn.Close();
            return valid;


        }

    }
}