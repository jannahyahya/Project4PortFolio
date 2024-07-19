using AutoRSN.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace AutoRSN.Repositories
{
    public class OtherRepository
    {


        public List<string> adminList()
        {
            List<string> adminlist = new List<string>();

            var conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=goodrich;Password=o5goodrichtcp;");
            conn.Open();


            OracleCommand InsertCommand = new OracleCommand();
            InsertCommand.Connection = conn;
            InsertCommand.CommandType = CommandType.Text;
            InsertCommand.CommandText = $"select * from mis_admin";
            OracleDataReader read = InsertCommand.ExecuteReader();

            while (read.Read())
            {
                adminlist.Add(read.GetValue(0).ToString().Trim().ToUpper());

            }

            conn.Dispose();
            conn.Close();

            return adminlist;


        }
        public int updateData(string id, string tr, string to, string createdby, string receivedby)
        {
            int valid = 0;
            var conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=goodrich;Password=o5goodrichtcp;");
            conn.Open();


            OracleCommand InsertCommand = new OracleCommand();
            InsertCommand.Connection = conn;
            InsertCommand.CommandType = CommandType.Text;
            InsertCommand.CommandText = $"update mis_other set tr_number  = '{tr}',to_number  = '{to}',createdby  = '{createdby}',receivedby  = '{receivedby}' where id = {id}";

            Debug.WriteLine(InsertCommand.CommandText);
            try
            {
                InsertCommand.ExecuteNonQuery();
                valid = 1;
            }

            catch
            { }

            conn.Dispose();
            conn.Close();

            return valid;

        }
        public List<MIS> getAllMIS()
        {
            List<MIS> misList = new List<MIS>();
            var conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=goodrich;Password=o5goodrichtcp;");
            conn.Open();


            OracleCommand InsertCommand = new OracleCommand();
            InsertCommand.Connection = conn;
            InsertCommand.CommandType = CommandType.Text;
            InsertCommand.CommandText = $"select id,datetime,part_number,qty,requestedby,id_scan,stype,sbin,tr_number,to_number,createdby,receivedby from mis_other order by id";

            OracleDataReader read = InsertCommand.ExecuteReader();

            int row = 1;
            while (read.Read())
            {

                MIS mis = new MIS();
                mis.NO = (row++).ToString();
                mis.ID = read.GetValue(0).ToString();
                mis.DATETIME = read.GetValue(1).ToString();
                mis.PART_NUMBER = read.GetValue(2).ToString();
                mis.QTY = read.GetValue(3).ToString();
                mis.REQUESTEDBY = read.GetValue(4).ToString();
                mis.ID_SCAN = read.GetValue(5).ToString();
                mis.STYPE = read.GetValue(6).ToString();
                mis.SBIN = read.GetValue(7).ToString();
                mis.TR_NUMBER = read.GetValue(8).ToString();
                mis.TO_NUMBER = read.GetValue(9).ToString();
                mis.CREATEDBY = read.GetValue(10).ToString();
                mis.RECEIVEDBY = read.GetValue(11).ToString();

                misList.Add(mis);
            }

            conn.Dispose();
            conn.Close();

            return misList;
        }

        public bool deleteMIS(string id)
        {
            bool valid = false;
            var conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=goodrich;Password=o5goodrichtcp;");
            conn.Open();


            OracleCommand InsertCommand = new OracleCommand();
            InsertCommand.Connection = conn;
            InsertCommand.CommandType = CommandType.Text;
            InsertCommand.CommandText = $"delete from mis_other where id = {id}";

            try
            {
                InsertCommand.ExecuteNonQuery();
                valid = true;
            }

            catch
            { }

            conn.Dispose();
            conn.Close();

            return valid;


        }


        public bool insertMIS(MIS mis)
        {
            bool valid = false;
            var conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=goodrich;Password=o5goodrichtcp;");
            conn.Open();


            OracleCommand InsertCommand = new OracleCommand();
            InsertCommand.Connection = conn;
            InsertCommand.CommandType = CommandType.Text;
            InsertCommand.CommandText = $"insert into mis_other(id,datetime,part_number,qty,requestedby,id_scan,stype,sbin,tr_number,to_number,createdby,receivedby) " +
                $"values(ID_OTHER.NEXTVAL,'{mis.DATETIME}','{mis.PART_NUMBER}','{mis.QTY}','{mis.REQUESTEDBY}','{mis.ID_SCAN}','{mis.STYPE}','{mis.SBIN}','{mis.TR_NUMBER}','{mis.TO_NUMBER}','{mis.CREATEDBY}','{mis.RECEIVEDBY}')";

            try
            {
                InsertCommand.ExecuteNonQuery();
                valid = true;
            }

            catch
            { }

            conn.Dispose();
            conn.Close();

            return valid;
        }
    }
}