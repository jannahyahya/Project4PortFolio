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
    public class RequestRepository
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


        public int updateData(string id, string tr, string to, string createdby)
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

            return valid;

        }

     
        public List<MIS> getAllMIS(string project)
        {
            List<MIS> misList = new List<MIS>();
            var conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=goodrich;Password=o5goodrichtcp;");
            conn.Open();


            OracleCommand InsertCommand = new OracleCommand();
            InsertCommand.Connection = conn;
            InsertCommand.CommandType = CommandType.Text;
            // InsertCommand.CommandText = $"select id,CREATEDON,part_number,qty,requestedby,id_scan,stype,sbin,tr_number,to_number,createdby,receivedby,REQUESTEREXT,CONFIRMEDON,RECEIVEDON,LOC_OWNER,LOC_STYPE from mis where RECEIVEDON is null order by id";
            if (project.Contains("KANBAN"))
                InsertCommand.CommandText = $"select id,CREATEDON,part_number,qty,requestedby,id_scan,stype,sbin,tr_number,to_number,createdby,receivedby,REQUESTEREXT,CONFIRMEDON,RECEIVEDON,LOC_OWNER,LOC_STYPE,STATUS,COMMENTS,PREP_STATUS,MODEL,REFDES,IS_KITTING,PREP_COMMENT,IS_KANBAN,KANBAN_STATUS from mis where RECEIVEDON is null and project = '{project}' order by createdon desc";
            else
                InsertCommand.CommandText = $"select id,CREATEDON,part_number,qty,requestedby,id_scan,stype,sbin,tr_number,to_number,createdby,receivedby,REQUESTEREXT,CONFIRMEDON,RECEIVEDON,LOC_OWNER,LOC_STYPE,STATUS,COMMENTS,PREP_STATUS,MODEL,REFDES,IS_KITTING,PREP_COMMENT,IS_KANBAN,KANBAN_STATUS from mis where RECEIVEDON is null and project = '{project}' order by confirmedon desc NULLS LAST";

            OracleDataReader read = InsertCommand.ExecuteReader();

            int row = 1;
            while (read.Read())
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
                mis.LOC_OWNER = read.GetValue(15).ToString();
                mis.LOC_STYPE = read.GetValue(16).ToString();
                mis.STATUS = read.GetValue(17).ToString();
                mis.COMMENTS = read.GetValue(18).ToString();
                mis.PREP_STATUS = Convert.ToString(read.GetValue(19).ToString());
                mis.MODEL = Convert.ToString(read.GetValue(20).ToString());
                mis.REFDES = Convert.ToString(read.GetValue(21).ToString());
                mis.IS_KITTING = Convert.ToInt32(read.GetValue(22).ToString());
                mis.PREP_COMMENT = Convert.ToString(read.GetValue(23).ToString());
                mis.IS_KANBAN = Convert.ToInt32(read.GetValue(24).ToString());
                mis.KANBAN_STATUS = Convert.ToString(read.GetValue(25).ToString());

                if (mis.IS_KITTING == 0)
                {
                    if (mis.STATUS.Equals("NOT FINISH"))
                    {
                        mis.ROW_COLOR = "background-color:white";
                    }

                    else if (mis.STATUS.Equals("REJECT"))
                    {
                        mis.ROW_COLOR = "background-color:red";

                    }
                    else if (mis.STATUS.Equals("DONE"))
                    {
                        mis.ROW_COLOR = "background-color:greenyellow";

                    }
              
                }

                else if (mis.IS_KITTING == 1)
                {
                    if (mis.STATUS.Equals("NOT FINISH") && mis.PREP_STATUS.Equals("NOT FINISH"))
                    {
                        mis.ROW_COLOR = "background-color:white";
                    }

                    else if (mis.STATUS.Equals("REJECT") && mis.PREP_STATUS.Equals("NOT FINISH"))
                    {
                        mis.ROW_COLOR = "background-color:red";

                    }
                    else if (mis.STATUS.Equals("DONE") && mis.PREP_STATUS.Equals("NOT FINISH"))
                    {
                        mis.ROW_COLOR = "background-color:yellow";

                    }
                    else if (mis.STATUS.Equals("DONE") && mis.PREP_STATUS.Equals("REJECT"))
                    {
                        mis.ROW_COLOR = "background-color:plum";

                    }
                    else if (mis.STATUS.Equals("DONE") && mis.PREP_STATUS.Equals("DONE"))
                    {

                        mis.ROW_COLOR = "background-color:greenyellow";
                    }

                    else if (mis.STATUS.Equals("DONE") && mis.PREP_STATUS.Equals("DONE"))
                    {

                        mis.ROW_COLOR = "background-color:greenyellow";
                    }

                    else if (mis.STATUS.Equals("DONE") && mis.PREP_STATUS.Equals("N/A"))
                    {

                        mis.ROW_COLOR = "background-color:lawngreen";
                    }

                }


                if(mis.IS_KANBAN == 1)
                {
                    if(mis.KANBAN_STATUS.Equals("FINISH"))
                    {
                        mis.ROW_COLOR = "background-color:greenyellow";
                    }

                    else if (mis.KANBAN_STATUS.Equals("NOT FINISH"))
                    {
                        mis.ROW_COLOR = "background-color:white";
                    }

                }


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
            InsertCommand.CommandText = $"select id,CREATEDON,part_number,qty,requestedby,id_scan,stype,sbin,tr_number,to_number,createdby,receivedby,REQUESTEREXT,CONFIRMEDON,RECEIVEDON,COMMENTS from mis where id = {id}";

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
                mis.STATUS = read.GetValue(15).ToString();
                mis.COMMENTS = read.GetValue(16).ToString();
            }

            conn.Dispose();
            conn.Close();

            return mis;
        }


        public int updateKanbanStatus(string id)
        {
            var conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=goodrich;Password=o5goodrichtcp;");
            conn.Open();

            OracleCommand InsertCommand = new OracleCommand();
            InsertCommand.Connection = conn;
            InsertCommand.CommandType = CommandType.Text;
            InsertCommand.CommandText = $"update mis set kanban_status = 'FINISH' where id = {id}";

             int result = InsertCommand.ExecuteNonQuery();

            return result;

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



        public bool insertMIS(MIS mis,string project)
        {
            bool valid = false;
            var conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=goodrich;Password=o5goodrichtcp;");
            conn.Open();

            mis.IS_KANBAN = (project.Contains("KANBAN")) ? 1 : 0; //insert as 1 if project selected is KANBAN

            OracleCommand InsertCommand = new OracleCommand();
            InsertCommand.Connection = conn;
            InsertCommand.CommandType = CommandType.Text;
            InsertCommand.CommandText = $"insert into MIS(id,CREATEDON,part_number,qty,requestedby,id_scan,stype,sbin,tr_number,to_number,createdby,receivedby,REQUESTEREXT,LOC_OWNER,LOC_STYPE,PROJECT,IS_KITTING,SN,MODEL,REFDES,IS_KANBAN) " +
                $"values(IDSEQ.NEXTVAL,sysdate,'{mis.PART_NUMBER}','{mis.QTY}','{mis.REQUESTEDBY}','{mis.ID_SCAN}','{mis.STYPE}','{mis.SBIN}','{mis.TR_NUMBER}','{mis.TO_NUMBER}','{mis.CREATEDBY}','{mis.RECEIVEDBY}','{mis.REQUESTEREXT}','{mis.LOC_OWNER}','{mis.LOC_STYPE}','{project}',{getIs_kitting(mis.LOC_STYPE,project)},'{mis.SN}','{mis.MODEL}','{mis.REFDES}','{mis.IS_KANBAN}')";

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


        public int getIs_kitting(string stype , string project)
        {
            int is_kitting = 0;

            var conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=goodrich;Password=o5goodrichtcp;");
            conn.Open();
            OracleCommand InsertCommand = new OracleCommand();
            InsertCommand.Connection = conn;
            InsertCommand.CommandType = CommandType.Text;
            InsertCommand.CommandText = $"select case count(*) when 0 then 0 else 1 end test from  mis_prep_control where stype = '{stype}' and project = '{project}'";

            OracleDataReader reader = InsertCommand.ExecuteReader();
            while (reader.Read())
            {
                is_kitting = Convert.ToInt32(reader.GetValue(0));

            }

            conn.Dispose();
            conn.Close();

            return is_kitting;

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
            while (reader.Read())
            {
                sysdate = reader.GetValue(0).ToString();

            }

            conn.Dispose();
            conn.Close();

            return sysdate;
        }


        public bool updateReceiver(string id, string receivedby)
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