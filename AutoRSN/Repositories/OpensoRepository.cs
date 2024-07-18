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
    using AutoRSN.Extension;
    using System.Diagnostics;
    using System.Web.Script.Serialization;

    public class OpensoRepository
    {
        public IEnumerable<Openso> GetOpenso(int ServerId, bool IsActive = false)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                var p = new OracleDynamicParameters();
                p.Add("OPENSO", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                p.Add("P_SERVERID", value: ServerId, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                p.Add("P_STATUS", value: (IsActive) ? "Y" : "N", dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                return conn.Query<Openso>("RSN_ST_RSN_GETLIST", p, commandType: CommandType.StoredProcedure);
            }
        }

        public static void updateReportID(int serverid, string reportID, string rsnNo)
        {
            System.Diagnostics.Debug.WriteLine("update report parameter"+reportID + ":" + rsnNo);
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE Z_T_RSN_FINAL SET REPORTID='" + reportID + "' WHERE RSNNO='" + rsnNo + "' and serverid="+serverid;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

                conn.Close();
                conn.Dispose();
            }

        }

        public void deleteSO(string uniqueid, string rsnno)
        {

           
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = string.Format("delete from z_t_rsn_final where uniqel = '{0}' and rsnno = '{1}'", uniqueid, rsnno);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

                conn.Close();
                conn.Dispose();
            }

        }
        public IEnumerable<Openso> GetOpensoByRSNNO(int serverId, string rsnNo)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                var p = new OracleDynamicParameters();
                p.Add("OPENSO", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                p.Add("P_SERVERID", value: serverId, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                p.Add("P_RSNNO", value: rsnNo, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
               
                return conn.Query<Openso>("RSN_ST_RSN_GETSEARCHBYRSN", p, commandType: CommandType.StoredProcedure);
            }

        }

        public int getCurrentRSNNO()
        {
            int currentRsn = 0;
            OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString());
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = $"SELECT GET_CURRENTRSNNO FROM DUAL";
            cmd.CommandType = CommandType.Text;

            OracleDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                currentRsn = Convert.ToInt32(reader.GetValue(0));
            }


            conn.Close();
            conn.Dispose();

            return currentRsn;

        }

        public IEnumerable<Openso> GetOpensoBySearch(string groupName, int serverId, string CCMG = null, string CCMG1 = null, string CCMG2 = null, string CCMG3 = null, string CCMG4 = null, string CCMG5 = null , string CCMG6 = null)
        {
            //if (groupName == "")
                //groupName = null;
            if (CCMG == "")
                CCMG = null;
            if (CCMG1 == "")
                CCMG1 = null;
            if (CCMG2 == "")
                CCMG2 = null;
            if (CCMG3 == "")
                CCMG3 = null;
            if (CCMG4 == "")
                CCMG4 = null;
            if (CCMG5 == "")
                CCMG5 = null;
            if (CCMG6 == "")
                CCMG6 = null;


            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {

                var p = new OracleDynamicParameters();
                p.Add("OPENSO", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                p.Add("P_SERVERID", value: serverId, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                p.Add("P_FilterByCCMG", value: CCMG, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                p.Add("P_FilterByCCMG1", value: CCMG1, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                p.Add("P_FilterByCCMG2", value: CCMG2, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                p.Add("P_FilterByCCMG3", value: CCMG3, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                p.Add("P_FilterByCCMG4", value: CCMG4, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                p.Add("P_FilterByCCMG5", value: CCMG5, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                p.Add("P_FilterByCCMG6", value: CCMG6, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                p.Add("P_GROUPNAME", value: groupName, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                IEnumerable<Openso> tempList = conn.Query<Openso>("RSN_ST_RSN_GETSEARCH1", p, commandType: CommandType.StoredProcedure);
                System.Diagnostics.Debug.WriteLine("rsn ready list for " + groupName  + " = " + tempList.Count());
                return tempList;
            }
        }

        public string getCustomerGroup(string custCode)
        {

            OracleConnection conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=rsn;Password=o5rsntcp;");
            conn.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT CUSTOMERGROUP FROM RSN_ST_CUSTOMER WHERE CUSTOMERCODE='" + custCode + "'";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            string custName = null;
            if (dr.HasRows)
            {
                custName = Convert.ToString(dr.GetValue(0));

            }

        
            conn.Close();
            conn.Dispose();
            return Convert.ToString(custName); ;
        }


        public string getContactNo(string custCode)
        {
            string cntactInfo = "";
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select ATTNNAME,ATTNCONTACTNO from rsn_st_customer where customercode=" + "'" + custCode + "'";
                cmd.CommandType = CommandType.Text;

                OracleDataReader dr = cmd.ExecuteReader();



                if (dr.Read())
                {

                    System.Diagnostics.Debug.WriteLine(dr.GetValue(0));
                    cntactInfo = dr.GetValue(0).ToString() + " / " + dr.GetValue(1).ToString();
                  
                }

                else
                {

                    System.Diagnostics.Debug.WriteLine("could not find rsn no..");
                    cntactInfo = "null";
                }

                conn.Dispose();
                conn.Close();
            }

            return cntactInfo;

        }


        public string getShippingData(string cmdStr,string custCode)
        {
            string cntactInfo = "null";
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = cmdStr + "'" + custCode + "'";
                cmd.CommandType = CommandType.Text;

                OracleDataReader dr = cmd.ExecuteReader();
                


                if (dr.Read())
                {

                    System.Diagnostics.Debug.WriteLine(dr.GetValue(0));
                    cntactInfo = dr.GetValue(0).ToString(); 
                   // return Convert.ToString(dr.GetValue(0));
                    // foreach (string id in ids)
                    // {
                    //var p = new OracleDynamicParameters();
                    // p.Add("P_SERVERID", value: serverID, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                    //p.Add(uniqueID, value: id, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                    // p.Add("P_RSNNO", value: dr.GetValue(0), dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                    //conn.Query("RSN_ST_RSN_GENERATE", p, commandType: CommandType.StoredProcedure);
                    // }
                }

                else
                {

                    System.Diagnostics.Debug.WriteLine("could not find rsn no..");
                    cntactInfo =  "null";
                }

                conn.Dispose();
                conn.Close();
            }

            return cntactInfo;

        }

        public string getCurrentRSNSeq() // check if next 500 sequence is > 5 digits
        {
            string rsnSeq = "0";
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT last_number + 500 as next_rsn_seq FROM user_sequences WHERE sequence_name = 'SEQ_INCRE_RSN'";
                cmd.CommandType = CommandType.Text;

                OracleDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {

                    rsnSeq = Convert.ToString(dr.GetValue(0));
                }

                conn.Dispose();
                conn.Close();

                return rsnSeq;

            }

        }


        public void resetRSNSeq() //reset rsn sequence
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                conn.Open();
                OracleCommand objCmd = new OracleCommand();
                objCmd.Connection = conn;
                objCmd.CommandText = "RESET_RSNNO";
                objCmd.ExecuteNonQuery();

                conn.Dispose();
                conn.Close();


            }

        }
        public string getRSNNO()
        {

            string rsnno = "null";
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select GET_RSNNO from dual";
                cmd.CommandType = CommandType.Text;

                OracleDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {

                    rsnno = Convert.ToString(dr.GetValue(0));
                    System.Diagnostics.Debug.WriteLine(dr.GetValue(0));
                   // return Convert.ToString(dr.GetValue(0));
                    // foreach (string id in ids)
                    // {
                    //var p = new OracleDynamicParameters();
                    // p.Add("P_SERVERID", value: serverID, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                    //p.Add(uniqueID, value: id, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                    // p.Add("P_RSNNO", value: dr.GetValue(0), dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                    //conn.Query("RSN_ST_RSN_GENERATE", p, commandType: CommandType.StoredProcedure);
                    // }
                }

                else
                {

                    System.Diagnostics.Debug.WriteLine("could not find rsn no..");
                    rsnno =  "null";
                }

                conn.Dispose();
                conn.Close();


                return rsnno;
            }

        }

        public void generateRSNNO(int serverID , string id , string RSNNO)
        {
   
            string uniqueID = "uniqueID";

            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                         var p = new OracleDynamicParameters();
                         p.Add("P_SERVERID", value: serverID, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                         p.Add(uniqueID, value: id, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                         p.Add("P_RSNNO", value: RSNNO, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                         conn.Query("RSN_ST_RSN_GENERATE", p, commandType: CommandType.StoredProcedure);

                //checkDupRSNNO(serverID, RSNNO, id); //check n dup item if there is remaining stock

            }
                
          
        }
        public void updateShipDate(string RSNNO,string shipDate)
        {

            OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString());
            conn.Open();
                OracleCommand updateRSNFinal12 = conn.CreateCommand();
                updateRSNFinal12.CommandText = string.Format("update z_t_rsn_final set shipdate='{0}' where rsnno='{1}'",shipDate,RSNNO);
                updateRSNFinal12.CommandType = CommandType.Text;
                updateRSNFinal12.ExecuteNonQuery();
                conn.Dispose();
                conn.Close();
                
                //checkDupRSNNO(serverID, RSNNO, id); //check n dup item if there is remaining stock

        }


        public void updateGeneratedOn(string RSNNO)
        {

            OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString());
            conn.Open();
            OracleCommand updateRSNFinal12 = conn.CreateCommand();
            updateRSNFinal12.CommandText = string.Format("update z_t_rsn_final set GENERATEDON=sysdate where rsnno='{0}'", RSNNO);
            updateRSNFinal12.CommandType = CommandType.Text;
            updateRSNFinal12.ExecuteNonQuery();
            conn.Dispose();
            conn.Close();

            //checkDupRSNNO(serverID, RSNNO, id); //check n dup item if there is remaining stock

        }

        public void updateOptions(string rsnno , string forwarder , string accountno , string billto , string servicetype)
        {

            OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString());
            conn.Open();
            OracleCommand updateRSNFinal12 = conn.CreateCommand();
            updateRSNFinal12.CommandText = $"UPDATE Z_T_RSN_FINAL SET CUSTOMFORWARDER = '{forwarder}',CUSTOMACCOUNTNO = '{accountno}', CUSTOMBILLTO='{billto}', CUSTOMSERVICETYPE = '{servicetype}', CREATEDBY ='{HttpContext.Current.Session["Name"]}', CREATEDBYID = '{HttpContext.Current.Session["UserId"]}'  where RSNNO = '{rsnno}'";
            updateRSNFinal12.CommandType = CommandType.Text;
            Debug.WriteLine(updateRSNFinal12.CommandText);
            updateRSNFinal12.ExecuteNonQuery();
            conn.Dispose();
            conn.Close();
        }



       

        public void checkDupRSNNO(int serverID, string RSNNO,string uniqel)
        {

            string uniqueID = "uniqueID";

            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                var p = new OracleDynamicParameters();
                p.Add("p_serverid", value: serverID, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                p.Add("p_rsn", value: RSNNO, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                p.Add("p_uniqel", value: uniqel, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                conn.Query("RSN_ST_RSN_DUPLICATE", p, commandType: CommandType.StoredProcedure);

            }


        }



        public Openso getSingleSObyRSN(int serverID, string id , string rsnno)
        {
            string clsPartNo_col = "clsPartNo";
            string PO_col = "PO";
            string salesNo_col = "salesNo";
            string deliveryDate_col = "deliveryDate";
            string uniqueID = "uniqueID";

            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {

                var p = new OracleDynamicParameters();
                p.Add("OPENSO", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                p.Add("P_SERVERID", value: serverID, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                p.Add(uniqueID, value: id, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                p.Add("rsnno", value: rsnno, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                System.Diagnostics.Debug.WriteLine(id);
                System.Diagnostics.Debug.WriteLine(serverID);


                //p.Add(PO_col, value: PO, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                // p.Add(salesNo_col, value: salesNo, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                // p.Add(deliveryDate_col, value: deliveryDate, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);

                IEnumerable<Openso> openSOList = conn.Query<Openso>("RSN_ST_GETSINGLESO", p, commandType: CommandType.StoredProcedure);
                Openso temp = null;
                foreach (Openso openso in openSOList)
                {
                    temp = openso;
                    break;
                }

                if (temp == null)
                    System.Diagnostics.Debug.WriteLine("so object from uniqeid is null");
                else
                    System.Diagnostics.Debug.WriteLine("temp so has data");

                return temp;


            }


        }


        public Openso getSingleSO(int serverID, string id)
        {
            string clsPartNo_col = "clsPartNo";
            string PO_col = "PO";
            string salesNo_col = "salesNo";
            string deliveryDate_col = "deliveryDate";
            string uniqueID = "uniqueID";

            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {

                var p = new OracleDynamicParameters();
                p.Add("OPENSO", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                p.Add("P_SERVERID", value: serverID, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                p.Add(uniqueID, value: id, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);

                System.Diagnostics.Debug.WriteLine(id);
                System.Diagnostics.Debug.WriteLine(serverID);
               

                //p.Add(PO_col, value: PO, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                // p.Add(salesNo_col, value: salesNo, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                // p.Add(deliveryDate_col, value: deliveryDate, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);

                IEnumerable <Openso> openSOList = conn.Query<Openso>("RSN_ST_RSN_GENERATED_RSN", p, commandType: CommandType.StoredProcedure);
                Openso temp = null;
                foreach (Openso openso in openSOList)
                {
                    temp = openso;
                    break;
                }

                if (temp == null)
                    System.Diagnostics.Debug.WriteLine("so object from uniqeid is null");
                else
                    System.Diagnostics.Debug.WriteLine("temp so has data");

                return temp;


            }


        }

        public Openso GetOpensoByCustomerPONumber(int ServerId, string MATNR = null, string RSNNO = null, string EDATU=null , string UNIQEL=null)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                System.Diagnostics.Debug.WriteLine("ServerID=" + ServerId + ", MATNR=" + MATNR + ", RSNNO=" + RSNNO );
                var p = new OracleDynamicParameters();
                p.Add("Openso", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                p.Add("P_SERVERID", value: ServerId, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                p.Add("P_MATNR", value: MATNR, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                p.Add("P_RSNNO", value: RSNNO, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                p.Add("P_EDATU", value: EDATU, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                p.Add("P_UNIQEL", value: UNIQEL, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                IEnumerable<Openso> items = conn.Query<Openso>("RSN_ST_RSN_GETBYCCMG", p, commandType: CommandType.StoredProcedure);
                foreach(Openso item in items)
                {
                    System.Diagnostics.Debug.WriteLine("get Openso Item = " + item.UNIQEL);
                }
                return items.ElementAt(0);
                
            }
        }

        public Openso GetOpensoBySOLine(int ServerId, string vbeln, string posnr)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
               // System.Diagnostics.Debug.WriteLine("ServerID=" + ServerId + ", MATNR=" + MATNR + ", RSNNO=" + RSNNO);
                var p = new OracleDynamicParameters();
                p.Add("Openso", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                p.Add("P_SERVERID", value: ServerId, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                p.Add("P_VBELN", value: vbeln, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                p.Add("P_POSNR", value: posnr, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
              
                IEnumerable<Openso> items = conn.Query<Openso>("RSN_ST_RSN_GETBYSOLINE", p, commandType: CommandType.StoredProcedure);
                // foreach (Openso item in items)
                // {
                // System.Diagnostics.Debug.WriteLine("get Openso Item = " + item.UNIQEL);
                //}
                if (items.Count() > 0)
                    return items.ElementAt(0);
                else
                    return new Openso();

            }
        }



        public Openso IsExistOpenso(int ServerId, string so = null, string soLine = null)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                var p = new OracleDynamicParameters();
                p.Add("OPENSO", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                p.Add("P_SERVERID", value: ServerId, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                p.Add("P_SO", value: so, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                p.Add("P_SOLINE", value: soLine, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                return conn.Query<Openso>("RSN_ST_RSN_EXIST", p, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }

        public int UpdateOpenso(int ServerId, Openso objOpenso)
        {
            System.Diagnostics.Debug.WriteLine("update to openso == " + new JavaScriptSerializer().Serialize(objOpenso));
          
            using (var conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        var p = new OracleDynamicParameters();
                        p.Add("P_SERVERID", value: ServerId, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                        p.Add("P_UNIQEL", value: objOpenso.UNIQEL, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_MATNR", value: objOpenso.MATNR, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_KUNNR1", value: objOpenso.KUNNR1, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_BSTNK", value: objOpenso.BSTNK, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_RSNNO", value: objOpenso.RSNNO, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_VBELN", value: objOpenso.VBELN, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_EDATU", value: objOpenso.EDATU, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_SHIPQTY", value: objOpenso.SHIPQTY, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                        p.Add("P_REMARKS", value: objOpenso.REMARKS, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_STATUS", value: objOpenso.STATUS, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_LOGINUSER", value: objOpenso.MODIFIEDBY, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        conn.Execute("RSN_ST_RSN_UPDATE", p, null, 0, CommandType.StoredProcedure);
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

        public void updateRemarks(string remarks , string rsnno)
        {

            var conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=rsn;Password=o5rsntcp;");
            conn.Open();
            OracleCommand InsertCommand = new OracleCommand();

            InsertCommand.Connection = conn;
            InsertCommand.CommandType = CommandType.Text;
            InsertCommand.CommandText = string.Format("update z_t_rsn_final set remarks='{0}' where rsnno = '{1}'", remarks, rsnno);
            InsertCommand.ExecuteNonQuery();


            conn.Dispose();
            conn.Close();

        }

        public int getID()
        {

            int idVal = 0;
            var conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=rsn;Password=o5rsntcp;");
            conn.Open();

            //System.Diagnostics.Debug.WriteLine(objOpenso.VBELN + "/" + objOpenso.POSNR);
            OracleCommand InsertCommand = new OracleCommand();
            InsertCommand.Connection = conn;
            InsertCommand.CommandType = CommandType.Text;
            InsertCommand.CommandText = "SELECT GET_ID.NEXTVAL FROM DUAL";

            OracleDataReader intRead = InsertCommand.ExecuteReader();
            while(intRead.Read())
            {
                idVal = Convert.ToInt32(intRead.GetValue(0));

            }

            conn.Dispose();
            conn.Close();

            return idVal;

        }

        public void deleteOpenSo(string uniqel)
        {
            var conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=rsn;Password=o5rsntcp;");
            conn.Open();

            //System.Diagnostics.Debug.WriteLine(objOpenso.VBELN + "/" + objOpenso.POSNR);
            OracleCommand InsertCommand = new OracleCommand();
            InsertCommand.Connection = conn;
            InsertCommand.CommandType = CommandType.Text;

            //**delete so before creating manual rsn**// string cmds = $"delete from z_t_rsn_final where vbeln='{vbeln}' and posnr = '{posnr}' and shipqty=0 and rsnno='0'";
            string cmds = $"delete from z_t_rsn_final where uniqel='{uniqel}' and rsnno='0'";

            // //"VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',{12},{13},'{14}','{15}',{16},'{17}','{18}','{19}',{20},'{21}',sysdate,'{22}',sysdate)"
            //VBELN,POSNR,KUNNR1,KUNNR2,MATNR,ARKTX,BSTNK,POSEX,AUART,CCO,EDATU,REMARKS,OPENQTY,NETPR,STATUS,UNIQEL,SERVERID,RSNNO,REPORTID,KDMAT,SHIPQTY,MODIFIEDBY,MODIFIEDON,CREATEDBY,CREATEDON
            //
            // objOpenso.VBELN, objOpenso.POSNR, objOpenso.KUNNR1, objOpenso.KUNNR2, objOpenso.MATNR , objOpenso.ARKTX , objOpenso.BSTNK , objOpenso.POSEX, objOpenso.AUART, objOpenso.CCO, objOpenso.EDATU, objOpenso.REMARKS, objOpenso.OPENQTY, objOpenso.NETPR, objOpenso.STATUS, objOpenso.UNIQEL , ServerId, objOpenso.RSNNO, objOpenso.REPORTID , objOpenso.KDMAT,objOpenso.SHIPQTY, objOpenso.MODIFIEDBY, objOpenso.CREATEDBY);
            System.Diagnostics.Debug.WriteLine("");
            System.Diagnostics.Debug.WriteLine(cmds);
            InsertCommand.CommandText = cmds;

            InsertCommand.ExecuteNonQuery();
            conn.Dispose();
            conn.Close();  

        }

        public int InsertOpenso(int ServerId, Openso objOpenso)
        {

            
            var conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=rsn;Password=o5rsntcp;");
            conn.Open();

            System.Diagnostics.Debug.WriteLine(objOpenso.VBELN  + "/" +   objOpenso.POSNR);
            OracleCommand InsertCommand = new OracleCommand();
            InsertCommand.Connection = conn;
            InsertCommand.CommandType = CommandType.Text;
                
                string cmds = "insert into z_t_rsn_final(VBELN,POSNR,KUNNR1,KUNNR2,MATNR,ARKTX,BSTNK,POSEX,AUART,CCO,EDATU,REMARKS,OPENQTY,NETPR,STATUS,UNIQEL,SERVERID,RSNNO,REPORTID,KDMAT,SHIPQTY,CREATEDBY,CREATEDON,GENERATEDON,STORAGELOC,SHIPDATE,CREATEDBYID,CURRENCY) VALUES(" +
                $"'{objOpenso.VBELN}','{objOpenso.POSNR}','{objOpenso.KUNNR1}','{objOpenso.KUNNR2}','{objOpenso.MATNR}','{objOpenso.ARKTX}','{objOpenso.BSTNK}','{objOpenso.POSEX}','{objOpenso.AUART}','{objOpenso.CCO}','{objOpenso.EDATU}','{objOpenso.REMARKS}','{objOpenso.OPENQTY}','{objOpenso.NETPR}','{objOpenso.STATUS}','{objOpenso.UNIQEL}','{ServerId}','{objOpenso.RSNNO}','{objOpenso.REPORTID}','{objOpenso.KDMAT}',{objOpenso.SHIPQTY},'{objOpenso.CREATEDBY}',sysdate,sysdate,'{objOpenso.STORAGELOC}','{objOpenso.SHIPDATE}','{objOpenso.CREATEDBYID}','{objOpenso.CURRENCY}')";

            

            // //"VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',{12},{13},'{14}','{15}',{16},'{17}','{18}','{19}',{20},'{21}',sysdate,'{22}',sysdate)"
            //VBELN,POSNR,KUNNR1,KUNNR2,MATNR,ARKTX,BSTNK,POSEX,AUART,CCO,EDATU,REMARKS,OPENQTY,NETPR,STATUS,UNIQEL,SERVERID,RSNNO,REPORTID,KDMAT,SHIPQTY,MODIFIEDBY,MODIFIEDON,CREATEDBY,CREATEDON
            //
            // objOpenso.VBELN, objOpenso.POSNR, objOpenso.KUNNR1, objOpenso.KUNNR2, objOpenso.MATNR , objOpenso.ARKTX , objOpenso.BSTNK , objOpenso.POSEX, objOpenso.AUART, objOpenso.CCO, objOpenso.EDATU, objOpenso.REMARKS, objOpenso.OPENQTY, objOpenso.NETPR, objOpenso.STATUS, objOpenso.UNIQEL , ServerId, objOpenso.RSNNO, objOpenso.REPORTID , objOpenso.KDMAT,objOpenso.SHIPQTY, objOpenso.MODIFIEDBY, objOpenso.CREATEDBY);
            System.Diagnostics.Debug.WriteLine(objOpenso.UNIQEL);
            System.Diagnostics.Debug.WriteLine("insert command = "+cmds);
            InsertCommand.CommandText = cmds;
      
            InsertCommand.ExecuteNonQuery();
            conn.Dispose();
            conn.Close();

           // deleteOpenSo(objOpenso.UNIQEL); // to ensure there are no duplicated so
            return 1;
        }

    }
}