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
    public class ItemNotesRepository : IItemNotesRepository
    {
       public IEnumerable<ItemNotes> GetItemNotes(int ServerId, bool IsActive = false)
        {

            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                var p = new OracleDynamicParameters();
                p.Add("ITEMNOTES", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                p.Add("P_SERVERID", value: ServerId, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                p.Add("P_STATUS", value: (IsActive) ? "Y" : "N", dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                return conn.Query<ItemNotes>("RSN_ST_ITEMNOTES_GETLIST", p, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<ItemNotes> GetItemNotesBySearch(int ServerId, string POSO = null)
        {

            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                var p = new OracleDynamicParameters();
                p.Add("ITEMNOTES", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                p.Add("P_SERVERID", value: ServerId, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                p.Add("P_FILTERBYPOSO", value: POSO, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                return conn.Query<ItemNotes>("RSN_ST_ITEMNOTES_GETSEARCH", p, commandType: CommandType.StoredProcedure);
            }
        }

        public List<ItemNotes> GetItemNotesBySO(int ServerId, string SO=null, string SOLINE = null)
        {
            List<ItemNotes> itemnoteList = new List<ItemNotes>();
          
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {

                conn.Open();

                OracleCommand InsertCommand = new OracleCommand();
                InsertCommand.Connection = conn;
                InsertCommand.CommandType = CommandType.Text;
                if (SO != null & SOLINE != null)
                    InsertCommand.CommandText = string.Format("select * from rsn_st_itemnotes where salesorder = '{0}' AND SOLINE = '{1}'", SO, SOLINE);
                     
                
                else if(SO != null & SOLINE == null)
                    InsertCommand.CommandText = string.Format("select * from rsn_st_itemnotes where salesorder = '{0}' AND SOLINE IS NULL", SO);

                else if (SO == null & SOLINE == null)
                    InsertCommand.CommandText = string.Format("select * from rsn_st_itemnotes");

                OracleDataReader read = InsertCommand.ExecuteReader();
                while (read.Read())
                {
                    ItemNotes itemNote = new ItemNotes();
                    itemNote.CREATEDBY = read.GetValue(6).ToString();
                    //itemNote.CREATEDON = DateTime.paread.GetValue(7).ToString();
                    itemNote.CUSTOMERPO = read.GetValue(1).ToString();
                    itemNote.SALESORDER = read.GetValue(2).ToString();
                    itemNote.STATUS = read.GetValue(3).ToString();
                    itemNote.REMARK = read.GetValue(5).ToString();
                    itemNote.REASONID = Convert.ToInt32(read.GetValue(4).ToString());
                    itemNote.SOLINE = read.GetValue(10).ToString();

                    itemnoteList.Add(itemNote);
                    // read.GetValue(0).ToString());

                }

                //var p = new OracleDynamicParameters();
                //p.Add("ITEMNOTES", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                //p.Add("P_SERVERID", value: ServerId, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                //p.Add("P_PO", value: PO, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                //p.Add("P_SO", value: SO, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                //return conn.Query<ItemNotes>("RSN_ST_ITEMNOTES_GETBYPOSO", p, commandType: CommandType.StoredProcedure).SingleOrDefault();
                conn.Dispose();
                conn.Close();
            }

            return itemnoteList;
        }

        public ItemNotes IsExistItemNotes(int ServerId, string PO = null, string SO = null)
        {

            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                var p = new OracleDynamicParameters();
                p.Add("ITEMNOTES", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                p.Add("P_SERVERID", value: ServerId, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                p.Add("P_PO", value: PO, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                p.Add("P_SO", value: SO, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                return conn.Query<ItemNotes>("RSN_ST_ITEMNOTES_EXIST", p, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }

        public bool IsExistItemNotesbySO(int ServerId, string SO = null)
        {
            bool valid = false;
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                conn.Open();
                OracleCommand readcmd = new OracleCommand();
                readcmd.Connection = conn;
                readcmd.CommandType = CommandType.Text;
                readcmd.CommandText = string.Format("select * from rsn_st_itemnotes where salesorder = '{0}' and soline is null",SO);
                System.Diagnostics.Debug.WriteLine(readcmd.CommandText);
                OracleDataReader reader = readcmd.ExecuteReader();

                if (reader.Read())
                    valid = true;

            }
            return valid;
        }
        public bool IsExistItemNotesbySOWithLine(int ServerId, string SO = null,string soline = null)
        {
            bool valid = false;
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                conn.Open();
                OracleCommand readcmd = new OracleCommand();
                readcmd.Connection = conn;
                readcmd.CommandType = CommandType.Text;
                readcmd.CommandText = string.Format("select * from rsn_st_itemnotes where salesorder = '{0}' and soline = '{1}'", SO,soline);
                System.Diagnostics.Debug.WriteLine(readcmd.CommandText);
                OracleDataReader reader = readcmd.ExecuteReader();

                if (reader.Read())
                    valid = true;

            }
            return valid;
        }

        public void deleteItemNote(string so, string soline)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                conn.Open();
                OracleCommand readcmd = new OracleCommand();
                OracleCommand updatecmd = new OracleCommand();

                readcmd.Connection = conn;
                updatecmd.Connection = conn;


                readcmd.CommandType = CommandType.Text;
                if (soline  == "")
                {
                    readcmd.CommandText = string.Format("delete rsn_st_itemnotes where salesorder = '{0}' and soline is null", so);
                    updatecmd.CommandText = string.Format("update z_t_rsn_final set status = 'Y' where vbeln ='{0}' and posnr not in (select soline from rsn_st_itemnotes where salesorder = '{1}')", so,so);

                }

                else //has soline to delete
                {
                    readcmd.CommandText = string.Format("delete rsn_st_itemnotes where salesorder = '{0}' and soline = '{1}'", so, soline);
                    updatecmd.CommandText = string.Format("update z_t_rsn_final set status = 'Y' where vbeln ='{0}' and posnr = '{1}'", so, soline);

                }
                readcmd.ExecuteNonQuery();

               
                updatecmd.Connection = conn;
                updatecmd.CommandType = CommandType.Text;
                
                Debug.WriteLine(updatecmd.CommandText);
                updatecmd.ExecuteNonQuery();

                conn.Dispose();
                conn.Close();

            }

        }


        public int UpdateItemNotesByPOSO(int ServerId, ItemNotes objItemNotes)
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
                        p.Add("P_PO", value: objItemNotes.CUSTOMERPO, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_SO", value: objItemNotes.SALESORDER, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_SO_LINE", value: objItemNotes.SOLINE, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_STATUS", value: objItemNotes.STATUS, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_REASONID", value: objItemNotes.REASONID, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                        p.Add("P_REMARK", value: objItemNotes.REMARK, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_LOGINUSER", value: objItemNotes.MODIFIEDBY, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        conn.Execute("RSN_ST_ITEMNOTES_UPDATE", p, null, 0, CommandType.StoredProcedure);
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

        public int InsertItemNotes(int ServerId, ItemNotes objItemNotes)
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
                        p.Add("P_PO", value: objItemNotes.CUSTOMERPO, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_SO", value: objItemNotes.SALESORDER, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_SO_LINE", value: objItemNotes.SOLINE, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_STATUS", value: 'N', dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_REASONID", value: objItemNotes.REASONID, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                        p.Add("P_REMARK", value: objItemNotes.REMARK, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_LOGINUSER", value: objItemNotes.CREATEDBY, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                       
                        conn.Execute("RSN_ST_ITEMNOTES_INSERT", p, null, 0, CommandType.StoredProcedure);
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

        public ItemNotes GetItemNotesByPOSO(int ServerId, string SO, string SOLINE = null)
        {
            throw new NotImplementedException();
        }

        public int UpdateItemNotesBySO(int ServerId, ItemNotes objItemNotes)
        {
            throw new NotImplementedException();
        }
    }
}