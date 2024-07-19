using AutoRSN.Models;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AutoRSN.Repositories
{
    public class StorageRepository
    {
        public List<StorageCode> getAllStorageType()
        {
            List<StorageCode> typeList = new List<StorageCode>();
            var conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=rsn;Password=o5rsntcp;");
            conn.Open();

            OracleCommand InsertCommand = new OracleCommand();
            InsertCommand.Connection = conn;
            InsertCommand.CommandType = CommandType.Text;
            InsertCommand.CommandText = "select storagetype from z_t_storagetype";
            OracleDataReader read = InsertCommand.ExecuteReader();

            int row = 1;
            while(read.Read())
            {
                StorageCode type = new StorageCode();
                type.no = row++;
                type.storageType = read.GetValue(0).ToString();
                typeList.Add(type);
            }

            conn.Dispose();
            conn.Close();

            return typeList;

        }

        public void insertStorageType(StorageCode strgtype)
        {

            //List<StorageCode> typeList = new List<StorageCode>();
            var conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=rsn;Password=o5rsntcp;");
            conn.Open();

            OracleCommand InsertCommand = new OracleCommand();
            InsertCommand.Connection = conn;
            InsertCommand.CommandType = CommandType.Text;
            InsertCommand.CommandText = string.Format("insert into z_t_storagetype(STORAGETYPE,BIN,PUTAWAY,AVAILABLE) values('{0}','{1}','{2}','{3}')", strgtype.storageType.Trim(),strgtype.bin.Trim(), strgtype.isputaway,strgtype.isinventory);
            InsertCommand.ExecuteNonQuery();

            conn.Dispose();
            conn.Close();

        }

        public bool updateStorageType(StorageCode strgtype)
        {

            bool valid = false;
            //List<StorageCode> typeList = new List<StorageCode>();
            var conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=rsn;Password=o5rsntcp;");
            conn.Open();

            OracleCommand InsertCommand = new OracleCommand();
            InsertCommand.Connection = conn;
            InsertCommand.CommandType = CommandType.Text;
            InsertCommand.CommandText = string.Format("update z_t_storagetype set STORAGETYPE='{0}',BIN = '{1}',PUTAWAY = '{2}',AVAILABLE='{3}' where STORAGETYPE='{4}'", strgtype.storageType.Trim(), strgtype.bin.Trim(), strgtype.isputaway, strgtype.isinventory , strgtype.storageType.Trim());
          

            try
            {
                    InsertCommand.ExecuteNonQuery();
                valid = true;
            }
            catch (Exception ex)
            {
                valid = false;
            }
            finally
            {

             conn.Dispose();
              conn.Close();
            }

            return valid;
          

        }

        public StorageCode getStorageType(string strgeType)
        {
            StorageCode strgeCode = new StorageCode();

            //List<StorageCode> typeList = new List<StorageCode>();
            var conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=rsn;Password=o5rsntcp;");
            conn.Open();

            OracleCommand InsertCommand = new OracleCommand();
            InsertCommand.Connection = conn;
            InsertCommand.CommandType = CommandType.Text;
            InsertCommand.CommandText = "select * from z_t_storagetype where STORAGETYPE='" + strgeType + "'";
            OracleDataReader reader = InsertCommand.ExecuteReader();

            while (reader.Read())
            {
                strgeCode.storageType = reader.GetValue(0).ToString().Trim();
                strgeCode.bin = reader.GetValue(1).ToString().Trim();
                strgeCode.isputaway = Convert.ToBoolean(reader.GetValue(2).ToString().Trim());
                strgeCode.isinventory = Convert.ToBoolean(reader.GetValue(3).ToString().Trim());
            }


            conn.Dispose();
            conn.Close();

            return strgeCode;
        }

        public bool deleteStorageType(string strgtype)
        {
            bool valid = false;
            //List<StorageCode> typeList = new List<StorageCode>();
            var conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=rsn;Password=o5rsntcp;");
            conn.Open();

            OracleCommand InsertCommand = new OracleCommand();
            InsertCommand.Connection = conn;
            InsertCommand.CommandType = CommandType.Text;
            InsertCommand.CommandText = string.Format("delete from z_t_storagetype where storagetype = '{0}'", strgtype);
            try
            {
                InsertCommand.ExecuteNonQuery();
                valid = true;
            }

            catch
            {

            }

            conn.Dispose();
            conn.Close();

            return valid;
        }



        public bool checkStorageType(StorageCode storageType)
        {
            System.Diagnostics.Debug.WriteLine(storageType.storageType);
            List<StorageCode> typeList = new List<StorageCode>();
            var conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=rsn;Password=o5rsntcp;");
            conn.Open();

            OracleCommand InsertCommand = new OracleCommand();
            InsertCommand.Connection = conn;
            InsertCommand.CommandType = CommandType.Text;
            InsertCommand.CommandText = string.Format("select storagetype from z_t_storagetype where storagetype = '{0}'", storageType.storageType);
            OracleDataReader read = InsertCommand.ExecuteReader();

            if (read.HasRows)
                return true;
            else
                return false;

            conn.Dispose();
            conn.Close();

        }
    }
}