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
using Newtonsoft.Json;
using System.IO;
using System.Diagnostics;

namespace AutoRSN.Repositories
{
    public class UserRepository
    {
        public IEnumerable<User> GetUser(int ServerId, bool IsActive = false)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                var p = new OracleDynamicParameters();
                p.Add("USER", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                p.Add("P_SERVERID", value: ServerId, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                p.Add("P_STATUS", value: (IsActive) ? "Y" : "N", dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                return conn.Query<User>("RSN_ST_USER_GETLIST1", p, commandType: CommandType.StoredProcedure);
            }
        }

        public User getUserData(string Username)
        {
           // System.Diagnostics.Debug.WriteLine(serverId);
           // System.Diagnostics.Debug.WriteLine(account.USERNAME);
          //  System.Diagnostics.Debug.WriteLine(account.PASSWORD);

           User user = null;
            OracleConnection conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=rsn;Password=o5rsntcp;");
            conn.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT FULLNAME,USERNAME,LVL,DEPT,STATUS,EMAIL,SERVERID,PERMISSION,SUPERVISOR,SIGNATURE FROM Z_M_USER WHERE USERNAME = '" + Username + "'";
            cmd.CommandType = CommandType.Text;

            Debug.WriteLine(cmd.CommandText);
            OracleDataReader dr = cmd.ExecuteReader();

            while(dr.Read())
            {
                try
                {
                    user = new User();
                    user.FULLNAME = Convert.ToString(dr.GetValue(0));
                    user.USERNAME = Convert.ToString(dr.GetValue(1));
                    user.LVL = Convert.ToString(dr.GetValue(2));
                    user.DEPT = Convert.ToString(dr.GetValue(3));
                    user.STATUS = Convert.ToString(dr.GetValue(4));
                    user.EMAIL = Convert.ToString(dr.GetValue(5));
                    user.SERVERID = Convert.ToInt32(dr.GetValue(6));
                     //user.CUSTOMERGROUP = JsonConvert.DeserializeObject<string[]>(Convert.ToString(dr.GetValue(7)));
                    //string permissionstr = Convert.ToString(dr.GetValue(7));
                    user.PermissionHolderList = JsonConvert.DeserializeObject<List<PermissionHolder>>(Convert.ToString(dr.GetValue(7)));
                    user.SUPERVISOR = Convert.ToString(dr.GetValue(8));
                    
                }

                catch(Exception ex)
                {

                    System.Diagnostics.Debug.WriteLine(ex.Message + " => " + Convert.ToString(dr.GetValue(7)));
                }

                System.Diagnostics.Debug.WriteLine("0=" + Convert.ToString(dr.GetValue(0)));
                System.Diagnostics.Debug.WriteLine("1=" + Convert.ToString(dr.GetValue(1)));
                System.Diagnostics.Debug.WriteLine("2=" + Convert.ToString(dr.GetValue(2)));
                System.Diagnostics.Debug.WriteLine("3=" + Convert.ToString(dr.GetValue(3)));
                System.Diagnostics.Debug.WriteLine("4=" + Convert.ToString(dr.GetValue(4)));
                System.Diagnostics.Debug.WriteLine("5=" + Convert.ToString(dr.GetValue(5)));

            }
            return user; 
        } 

        public IEnumerable<User> GetUserBySearch(int ServerId, string CCMG = null)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                var p = new OracleDynamicParameters();
                p.Add("USER", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                p.Add("P_SERVERID", value: ServerId, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                p.Add("P_FILTERBYCCMG", value: CCMG, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                return conn.Query<User>("RSN_ST_USER_GETSEARCH", p, commandType: CommandType.StoredProcedure);
            }
        }

        public bool deleteUser(string user)
        {
            bool valid = false;
            var conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=rsn;Password=o5rsntcp;");
            conn.Open();

            OracleCommand pull = conn.CreateCommand();
            pull.CommandText = string.Format("delete from Z_M_USER WHERE USERNAME= '{0}'", user);
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

        public User GetUserByUserName(int ServerId, string USERNAME = null)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                var p = new OracleDynamicParameters();
                p.Add("USER", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                p.Add("P_SERVERID", value: ServerId, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
               // p.Add("P_FULLNAME", value: FULLNAME, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                p.Add("P_USERNAME", value: USERNAME, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                return conn.Query<User>("RSN_ST_USER_GETBYCCMG", p, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }

        }

        public User IsExistUser(int ServerId, string PO = null, string SO = null)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                var p = new OracleDynamicParameters();
                p.Add("USER", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                p.Add("P_SERVERID", value: ServerId, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                p.Add("P_FULLNAME", value: PO, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                p.Add("P_USERNAME", value: SO, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                return conn.Query<User>("RSN_ST_USER_EXIST", p, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }

        public void UploadImageInDataBase(HttpPostedFileBase file, User user)
        {
            //user.SIGNATURE = file; // ConvertToBytes(file);
            //System.Diagnostics.Debug.WriteLine("image size " +  user.SIGNATURE.Length);
            //var conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=rsn;Password=o5rsntcp;");
            //conn.Open();

            //OracleCommand updateCommand = new OracleCommand();
            //updateCommand.Connection = conn;
            //updateCommand.CommandType = CommandType.Text;
           
            //updateCommand.CommandText = string.Format("UPDATE Z_M_USER SET SIGNATURE = {0} WHERE USERNAME = '{1}'",":BlobParameter",user.USERNAME);

            //OracleParameter blobParameter = new OracleParameter();
            //blobParameter.OracleDbType = OracleDbType.Blob;
            //blobParameter.ParameterName = "BlobParameter";
            //blobParameter.Value = user.SIGNATURE;

            //updateCommand.Parameters.Add(blobParameter);

            //try
            //{
            //    updateCommand.ExecuteNonQuery();
            //}
            //catch (Exception e)
            //{
              
            //    System.Diagnostics.Debug.WriteLine(e.Message);
            //}
            //conn.Dispose();
            //conn.Close();

            Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath("~/Resources/" + user.USERNAME));
            var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Resources/" + user.USERNAME), file.FileName);
            System.Diagnostics.Debug.WriteLine(path);
            file.SaveAs(path);  

            user.SIGNATURE = Util.getWebUrl() + "AutoRSN/Resources/" + user.USERNAME + "/" +   file.FileName;
            var conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=rsn;Password=o5rsntcp;");
            conn.Open();

            OracleCommand updateCommand = new OracleCommand();
            updateCommand.Connection = conn;
            updateCommand.CommandType = CommandType.Text;
            updateCommand.CommandText = string.Format("UPDATE Z_M_USER SET SIGNATURE = '{0}' WHERE USERNAME = '{1}'", user.SIGNATURE, user.USERNAME);
            try
            {
                updateCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            conn.Dispose();
            conn.Close();

        }

        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            System.IO.BinaryReader reader = new System.IO.BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }

        public int UpdateUser(int ServerId, User objUser)
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
                        // p.Add("P_FULLNAME", value: objUser.FULLNAME, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_FULLNAME", value: objUser.FULLNAME, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_EMAIL", value: objUser.EMAIL, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_USERNAME", value: objUser.USERNAME, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_PASSWORD", value: objUser.PASSWORD, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_LVL", value: objUser.LVL, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_DEPT", value: objUser.DEPT, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_MANAGER", value: objUser.MANAGER, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_MANAGEREXT", value: objUser.MANAGEREXT, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_EXTNO", value: objUser.EXTNO, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_STATUS", value: objUser.STATUS, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_LOGINUSER", value: objUser.MODIFIEDBY, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                       // p.Add("P_CUSTOMERGROUP", value: JsonConvert.SerializeObject(objUser.CUSTOMERGROUP), dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_PERMISSION", value: JsonConvert.SerializeObject(objUser.PermissionHolderList), dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_SUPERVISOR", value: objUser.SUPERVISOR, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        conn.Execute("RSN_ST_USER_UPDATE", p, null, 0, CommandType.StoredProcedure);
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

        public int InsertUser(int ServerId, User objUser)
        {
            using (var conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString()))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                      //AccountRepository usr =UserRepository();
                       // objUser.EMAIL = usr.getUserLDAPEmail(objUser);
            
                        var p = new OracleDynamicParameters();
                        p.Add("P_SERVERID", value: ServerId, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                        p.Add("P_FULLNAME", value: objUser.FULLNAME, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_USERNAME", value: objUser.USERNAME, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_PASSWORD", value: objUser.PASSWORD, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_EMAIL", value: objUser.EMAIL, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_LVL", value: objUser.LVL, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_DEPT", value: objUser.DEPT, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_MANAGER", value: objUser.MANAGER, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_MANAGEREXT", value: objUser.MANAGEREXT, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_EXTNO", value: objUser.EXTNO, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_STATUS", value: objUser.STATUS, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_LOGINUSER", value: objUser.CREATEDBY, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                       // p.Add("P_CUSTOMERGROUP", value: JsonConvert.SerializeObject(objUser.CUSTOMERGROUP), dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_PERMISSION", value: JsonConvert.SerializeObject(objUser.PermissionHolderList), dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        p.Add("P_SUPERVISOR", value: objUser.SUPERVISOR, dbType: OracleDbType.Varchar2, direction: ParameterDirection.Input);
                        conn.Execute("RSN_ST_USER_INSERT", p, null, 0, CommandType.StoredProcedure);
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