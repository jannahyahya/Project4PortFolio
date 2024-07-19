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
using System.Diagnostics;

namespace AutoRSN.Repositories
{
    public class CustomerRepository
    {
        public IEnumerable<Customer> GetCustomer(int ServerId, bool IsActive = false)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                var p = new OracleDynamicParameters();
                p.Add("CUSTOMER", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                p.Add("P_SERVERID", value: ServerId, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                p.Add("P_STATUS", value: (IsActive) ? "Y" : "N", dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                return conn.Query<Customer>("RSN_ST_CUSTOMER_GETLIST", p, commandType: CommandType.StoredProcedure);
            }
        }


        public IEnumerable<Customer> getCustomerListByGroup(int ServerId, bool IsActive = false,string customerGroup = "")
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                var p = new OracleDynamicParameters();
                p.Add("CUSTOMER", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                p.Add("P_SERVERID", value: ServerId, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                p.Add("P_STATUS", value: (IsActive) ? "Y" : "N", dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                p.Add("P_CUSTOMERGROUP", customerGroup, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);

                return conn.Query<Customer>("RSN_ST_CUSTOMER_GETLISTBYGROUP", p, commandType: CommandType.StoredProcedure);
            }
        }

        public List<CustomerGroup> getCustomerGroupList()
        {
            List<CustomerGroup> temp = new List<CustomerGroup>();
            OracleConnection conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=rsn;Password=o5rsntcp;");
            conn.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT DISTINCT CUSTOMERNAME,LINECOUNT FROM rsn_st_customer_group ORDER BY CUSTOMERNAME";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                try
                {
                    CustomerGroup custGroup = new CustomerGroup();
                   // custGroup.GROUPID = Convert.ToString(dr.GetValue(0));
                    custGroup.CUSTOMERNAME = Convert.ToString(dr.GetValue(0));
                    custGroup.LINECOUNT = Convert.ToInt32(dr.GetValue(1));
                    temp.Add(custGroup);
                }
                catch (Exception e)
                {

                }
            }

            conn.Close();
            conn.Dispose();
            return temp;

        }


        public string getCustomerNamebyCode(string custcode)
        {
            string custname = "";
            var conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=rsn;Password=o5rsntcp;");
            conn.Open();

            OracleCommand name = conn.CreateCommand();
            name.CommandText = $"select distinct(c.customergroup) from rsn_st_customer c , z_t_rsn_final rsn where rsn.kunnr1 = c.customercode and rsn.kunnr1 = '{custcode}'";
            try
            {
                OracleDataReader read = name.ExecuteReader();

                while(read.Read())
                {
                    custname = read.GetValue(0).ToString();
                }


            }
            catch (Exception ex)
            {
                //System.Diagnostics.Debug.WriteLine("Error DELETING =" + ex.Message);

            }

            conn.Dispose();
            conn.Close();
            return custname;

        }
        public bool deleteCustomer(string customer)
        {
            bool valid = false;
            var conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=rsn;Password=o5rsntcp;");
            conn.Open();

            OracleCommand pull = conn.CreateCommand();
            pull.CommandText = string.Format("delete from RSN_ST_CUSTOMER WHERE CUSTOMERCODE = '{0}'", customer);
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


        public List<string> getCustomerNameList()
        {
            List<string> temp = new List<string>();
            OracleConnection conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=rsn;Password=o5rsntcp;");
            conn.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT DISTINCT CUSTOMERNAME FROM RSN_ST_CUSTOMER";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                
                    try {
                        temp.Add(Convert.ToString(dr.GetValue(0)));
                    }
                    catch(Exception e)
                    {

                    }
            }

            conn.Close();
            conn.Dispose();
            return temp;
        }

        public IEnumerable<Customer> GetCustomerBySearch(int ServerId, string CCMG = null)
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

        public Customer GetCustomerByCustomerCodeMaterialGroup(int ServerId, string CustomerCode = null, string MaterialGroup = null)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                try
                {
                    var p = new OracleDynamicParameters();
                    p.Add("CUSTOMER", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    p.Add("P_SERVERID", value: ServerId, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                    p.Add("P_CUSTOMERCODE", value: CustomerCode, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                    p.Add("P_MATERIALGROUP", value: MaterialGroup, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                    return conn.Query<Customer>("RSN_ST_CUSTOMER_GETBYCCMG", p, commandType: CommandType.StoredProcedure).ElementAt(0);
                }

                catch(Exception e)
                {
                    Debug.WriteLine(e.Message);
                    return new Customer();
                }
                }
        }

        public Customer IsExistCustomer(int ServerId, string PO = null, string SO = null)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                var p = new OracleDynamicParameters();
                p.Add("CUSTOMER", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                p.Add("P_SERVERID", value: ServerId, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                p.Add("P_CUSTOMERCODE", value: PO, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                p.Add("P_MATERIALGROUP", value: SO, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                return conn.Query<Customer>("RSN_ST_CUSTOMER_EXIST", p, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }

        public int UpdateCustomer(int ServerId, Customer objCustomer)
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
                        p.Add("P_CUSTOMERCODE", value: objCustomer.CUSTOMERCODE, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_MATERIALGROUP", value: objCustomer.MATERIALGROUP, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_CUSTOMERNAME", value: objCustomer.CUSTOMERNAME, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_CUSTOMERGROUP", value: objCustomer.CUSTOMERGROUP, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_ADDRESS1", value: objCustomer.ADDRESS1, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_ADDRESS2", value: objCustomer.ADDRESS2, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_SHIPTO", value: objCustomer.SHIPTO, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_POSTCODE", value: objCustomer.POSTCODE, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_REGION", value: objCustomer.REGION, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_COUNTRY", value: objCustomer.COUNTRY, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_FORWARDER", value: objCustomer.FORWARDER, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_ATTENTIONNAME", value: objCustomer.ATTNNAME, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_ATTENTIONNO", value: objCustomer.ATTNCONTACTNO, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_STATUS", value: objCustomer.STATUS, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_LOGINUSER", value: objCustomer.MODIFIEDBY, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);

                        p.Add("P_ACCOUNTNO", value: objCustomer.ACCOUNTNO, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_EX_FACTORY", value: objCustomer.EX_Factory.ToString(), dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_FCA_FOB", value: objCustomer.FCA_FOB.ToString(), dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_DOOR_TO_DOOR", value: objCustomer.DOOR_TO_DOOR.ToString(), dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_DOOR_TO_PORT_DESC", value: objCustomer.DOOR_TO_PORT_DESC.ToString(), dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_DAP", value: objCustomer.DAP.ToString(), dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_DDP", value: objCustomer.DDP.ToString(), dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_BILLTO", value: objCustomer.BILLTO.ToString(), dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_SERVICETYPE", value: objCustomer.SERVICETYPE.ToString(), dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);

                        Debug.WriteLine(objCustomer.EX_Factory.ToString());
                        Debug.WriteLine(objCustomer.FCA_FOB.ToString());
                        Debug.WriteLine(objCustomer.DOOR_TO_DOOR.ToString());
                        Debug.WriteLine(objCustomer.DOOR_TO_PORT_DESC.ToString());
                        Debug.WriteLine(objCustomer.DAP.ToString());
                        Debug.WriteLine(objCustomer.DDP.ToString());
                        Debug.WriteLine(objCustomer.BILLTO.ToString());
                        Debug.WriteLine(objCustomer.SERVICETYPE.ToString());

                       

                        conn.Execute("RSN_ST_CUSTOMER_UPDATE", p, null, 0, CommandType.StoredProcedure);
                        //int ServerId = p.Get<int>("P_SERVERID");
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {

                        tran.Rollback();
                        throw;
                    }
                }
            }
            return 1;

        }

        public int InsertCustomer(int ServerId, Customer objCustomer)
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
                        p.Add("P_CUSTOMERCODE", value: objCustomer.CUSTOMERCODE, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_MATERIALGROUP", value: objCustomer.MATERIALGROUP, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_CUSTOMERNAME", value: objCustomer.CUSTOMERNAME, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_CUSTOMERGROUP", value: objCustomer.CUSTOMERGROUP, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_ADDRESS1", value: objCustomer.ADDRESS1, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_ADDRESS2", value: objCustomer.ADDRESS2, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_SHIPTO", value: objCustomer.SHIPTO, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_POSTCODE", value: objCustomer.POSTCODE, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_REGION", value: objCustomer.REGION, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_COUNTRY", value: objCustomer.COUNTRY, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_FORWARDER", value: objCustomer.FORWARDER, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_ATTENTIONNAME", value: objCustomer.ATTNNAME, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_ATTENTIONNO", value: objCustomer.ATTNCONTACTNO, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_STATUS", value: objCustomer.STATUS, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_LOGINUSER", value: objCustomer.CREATEDBY, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);

                        p.Add("P_ACCOUNTNO", value: objCustomer.ACCOUNTNO, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_EX_FACTORY", value: objCustomer.EX_Factory.ToString(), dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_FCA_FOB", value: objCustomer.FCA_FOB.ToString(), dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_DOOR_TO_DOOR", value: objCustomer.DOOR_TO_DOOR.ToString(), dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_DOOR_TO_PORT_DESC", value: objCustomer.DOOR_TO_PORT_DESC.ToString(), dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_DAP", value: objCustomer.DAP.ToString(), dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_DDP", value: objCustomer.DDP.ToString(), dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_BILLTO", value: objCustomer.BILLTO.ToString(), dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_SERVICETYPE", value: objCustomer.SERVICETYPE.ToString(), dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        conn.Execute("RSN_ST_CUSTOMER_INSERT", p, null, 0, CommandType.StoredProcedure);

                        
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