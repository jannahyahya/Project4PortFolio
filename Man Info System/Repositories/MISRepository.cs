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
    public class MISRepository
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

            while(read.Read())
            {
                adminlist.Add(read.GetValue(0).ToString().Trim().ToUpper());

            }

            conn.Dispose();
            conn.Close();

            return adminlist;


        }
        public int updateData(string id,string tr,string to,string createdby)
        {
            int valid = 0;
            var conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=goodrich;Password=o5goodrichtcp;");
            conn.Open();


            OracleCommand InsertCommand = new OracleCommand();
            InsertCommand.Connection = conn;
            InsertCommand.CommandType = CommandType.Text;
            InsertCommand.CommandText = $"update MIS set tr_number  = '{tr}',to_number  = '{to}',createdby  = '{createdby}', confirmedon = sysdate where id = {id}";

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

            return valid ;

        }
        public List<MIS> getAllMIS(string days , string project)
        {
            List<MIS> misList = new List<MIS>();
            var conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=goodrich;Password=o5goodrichtcp;");
            conn.Open();

          
            OracleCommand InsertCommand = new OracleCommand();
            InsertCommand.Connection = conn;
            InsertCommand.CommandType = CommandType.Text;
            //if(days.Equals("ALL"))
            //    InsertCommand.CommandText = $"select id,CREATEDON,part_number,qty,requestedby,id_scan,stype,sbin,tr_number,to_number,createdby,receivedby,REQUESTEREXT,CONFIRMEDON,RECEIVEDON from mis where CREATEDON is not null and CONFIRMEDON is not null and RECEIVEDON is not null order by id";
            //else
            //    InsertCommand.CommandText = $"select id,CREATEDON,part_number,qty,requestedby,id_scan,stype,sbin,tr_number,to_number,createdby,receivedby,REQUESTEREXT,CONFIRMEDON,RECEIVEDON from mis where CREATEDON is not null and CONFIRMEDON is not null and RECEIVEDON is not null and (SYSDATE - RECEIVEDON) <= {days}  order by id";

            if (days.Equals("ALL"))
                InsertCommand.CommandText = $"select id,CREATEDON,part_number,qty,requestedby,id_scan,stype,sbin,tr_number,to_number,createdby,receivedby,REQUESTEREXT,CONFIRMEDON,RECEIVEDON,COMMENTS,PREP_STATUS,MODEL,REFDES from mis where RECEIVEDON is not null AND status = 'DONE' and project = '{project}' order by id desc";
            else
                InsertCommand.CommandText = $"select id,CREATEDON,part_number,qty,requestedby,id_scan,stype,sbin,tr_number,to_number,createdby,receivedby,REQUESTEREXT,CONFIRMEDON,RECEIVEDON,COMMENTS,PREP_STATUS,MODEL,REFDES from mis where RECEIVEDON is not null and status = 'DONE' and (SYSDATE - RECEIVEDON) <= {days} and project = '{project}' order by id desc";

            OracleDataReader read = InsertCommand.ExecuteReader();

            int row = 1;
            while(read.Read())
            {

                MIS mis = new MIS();
                mis.NO = (row++).ToString();
                mis.ID = read.GetValue(0).ToString();
                mis.CREATEDON = read.GetValue(1).ToString();
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
                mis.REQUESTEREXT = read.GetValue(12).ToString();
                mis.CONFIRMEDON = read.GetValue(13).ToString();
                mis.RECEIVEDON = read.GetValue(14).ToString();
                mis.COMMENTS = read.GetValue(15).ToString();
                mis.PREP_STATUS = Convert.ToString(read.GetValue(16).ToString());
                mis.MODEL = Convert.ToString(read.GetValue(17).ToString());
                mis.REFDES = Convert.ToString(read.GetValue(16).ToString());
                mis.PROJECT = Convert.ToString(read.GetValue(17).ToString());

                misList.Add(mis);
            }

            conn.Dispose();
            conn.Close();
          
            return misList;
        }


        public MIS getMIS(string id)
        {
            MIS mis = new MIS();
            var conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=goodrich;Password=o5goodrichtcp;");
            conn.Open();


            OracleCommand InsertCommand = new OracleCommand();
            InsertCommand.Connection = conn;
            InsertCommand.CommandType = CommandType.Text;
            InsertCommand.CommandText = $"select id,CREATEDON,part_number,qty,requestedby,id_scan,stype,sbin,tr_number,to_number,createdby,receivedby,REQUESTEREXT,CONFIRMEDON,RECEIVEDON from mis where id = {id}";

            OracleDataReader read = InsertCommand.ExecuteReader();

            int row = 1;
            while (read.Read())
            {

               
                mis.NO = (row++).ToString();
                mis.ID = read.GetValue(0).ToString();
                mis.CREATEDON = read.GetValue(1).ToString();
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
                mis.REQUESTEREXT = read.GetValue(12).ToString();
                mis.CONFIRMEDON = read.GetValue(13).ToString();
                mis.RECEIVEDON = read.GetValue(14).ToString();
                mis.COMMENTS = read.GetValue(15).ToString();
            }

            conn.Dispose();
            conn.Close();

            return mis;
        }


        public bool deleteMIS(string id)
        {
            bool valid = false;
            var conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=goodrich;Password=o5goodrichtcp;");
            conn.Open();


            OracleCommand InsertCommand = new OracleCommand();
            InsertCommand.Connection = conn;
            InsertCommand.CommandType = CommandType.Text;
            InsertCommand.CommandText = $"delete from mis where id = {id}";

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
            InsertCommand.CommandText = $"insert into MIS(id,CREATEDON,part_number,qty,requestedby,id_scan,stype,sbin,tr_number,to_number,createdby,receivedby,REQUESTEREXT,SN,MODEL,REFDES) " + 
                $"values(IDSEQ.NEXTVAL,sysdate,'{mis.PART_NUMBER}','{mis.QTY}','{mis.REQUESTEDBY}','{mis.ID_SCAN}','{mis.STYPE}','{mis.SBIN}','{mis.TR_NUMBER}','{mis.TO_NUMBER}','{mis.CREATEDBY}','{mis.RECEIVEDBY}','{mis.REQUESTEREXT}','{mis.SN}','{mis.MODEL}','{mis.REFDES}')";

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


        public string getCurrentSysdate()
        {
            string sysdate = "";
            var conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=goodrich;Password=o5goodrichtcp;");
            conn.Open();
            OracleCommand InsertCommand = new OracleCommand();
            InsertCommand.Connection = conn;
            InsertCommand.CommandType = CommandType.Text;
            InsertCommand.CommandText = $"select sysdate from dual";

            OracleDataReader reader = InsertCommand.ExecuteReader();
            while(reader.Read())
            {
                sysdate = reader.GetValue(0).ToString();

            }

            conn.Dispose();
            conn.Close();

            return sysdate;
        }


        public bool updateReceiver(string id,string receivedby)
        {

            bool valid = false;
            var conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=goodrich;Password=o5goodrichtcp;");
            conn.Open();


            OracleCommand InsertCommand = new OracleCommand();
            InsertCommand.Connection = conn;
            InsertCommand.CommandType = CommandType.Text;
            InsertCommand.CommandText = $"update mis set receivedby = '{receivedby}',RECEIVEDON=sysdate where id = {id}";

            Debug.WriteLine(InsertCommand.CommandText);
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