using AutoRSN.Models;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AutoRSN.Repositories
{
    public class ShipsetRepositories
    {
        OracleConnection conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=rsn;Password=o5rsntcp;");


        public void insertShipset(Shipset shipset)
        {
            conn.Open();

            OracleCommand deleteCmd = conn.CreateCommand();
            deleteCmd.CommandText = string.Format("INSERT INTO Z_T_SHIPSET(CUST_PN,CLS_PN) VALUES('{0}','{1}')", shipset.CUST_PN, shipset.CLS_PN);
            deleteCmd.CommandType = CommandType.Text;
            deleteCmd.ExecuteNonQuery();

            conn.Close();

        }


        public void DeleteShipset(string custPn,string celPn)
        {
            conn.Open();

            OracleCommand deleteCmd = conn.CreateCommand();
            deleteCmd.CommandText = string.Format("DELETE FROM Z_T_SHIPSET WHERE CUST_PN = '{0}' AND CLS_PN = '{1}'",custPn, celPn);
            deleteCmd.CommandType = CommandType.Text;
            deleteCmd.ExecuteNonQuery();

            conn.Close();
        }

        public List<Shipset> getShipsetList()
        {
            List<Shipset> shipSetList = new List<Shipset>();
            conn.Open();
            OracleCommand getListCommand = conn.CreateCommand();
            getListCommand.CommandText = "SELECT CUST_PN,CLS_PN FROM Z_T_SHIPSET";
            getListCommand.CommandType = CommandType.Text;
            OracleDataReader read = getListCommand.ExecuteReader();

            while(read.Read())
            {
                Shipset shipset = new Shipset();
                shipset.CUST_PN = read.GetString(0).ToString();
                try
                {
                    shipset.CLS_PN = read.GetString(1).ToString();
                }
                catch (Exception ex)
                {
                    shipset.CLS_PN = null;
                }
                   
                shipSetList.Add(shipset);
            }

            conn.Close();
            return shipSetList;
        }

        public List<Shipset> getShipsetListByClsPN(string cls_pn)
        {
            List<Shipset> shipSetList = new List<Shipset>();
            conn.Open();
            OracleCommand getListCommand = conn.CreateCommand();
            getListCommand.CommandText = "SELECT CUST_PN,CLS_PN FROM Z_T_SHIPSET where CLS_PN='" + cls_pn + "'";
            getListCommand.CommandType = CommandType.Text;
            OracleDataReader read = getListCommand.ExecuteReader();

            while (read.Read())
            {
                Shipset shipset = new Shipset();
                shipset.CUST_PN = read.GetString(0).ToString();
                try
                {
                    shipset.CLS_PN = read.GetString(1).ToString();
                }
                catch (Exception ex)
                {
                    shipset.CLS_PN = null;
                }

                shipSetList.Add(shipset);
            }

            conn.Close();
            return shipSetList;
        }

    }
}