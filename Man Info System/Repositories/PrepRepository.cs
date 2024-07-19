using AutoRSN.Models;
using Newtonsoft.Json.Linq;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace AutoRSN.Controllers
{
    public class PrepRepository
    {
        public List<MIS> getAllMIS(string project)
        {
            List<MIS> misList = new List<MIS>();
            var conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=goodrich;Password=o5goodrichtcp;");
            conn.Open();


            OracleCommand InsertCommand = new OracleCommand();
            InsertCommand.Connection = conn;
            InsertCommand.CommandType = CommandType.Text;
            //InsertCommand.CommandText = $"select id,CREATEDON,part_number,qty,requestedby,id_scan,stype,sbin,tr_number,to_number,createdby,receivedby,REQUESTEREXT,CONFIRMEDON,RECEIVEDON,LOC_OWNER,LOC_STYPE,STATUS from mis where (tr_number is null or to_number is null or CONFIRMEDON is null) and receivedon is null order by id desc";

            InsertCommand.CommandText = $"select id,CREATEDON,part_number,qty,requestedby,id_scan,stype,sbin,tr_number,to_number,createdby,receivedby,REQUESTEREXT,CONFIRMEDON,RECEIVEDON,LOC_OWNER,LOC_STYPE,STATUS,COMMENTS,STATUS,PREP_STATUS,SN,MODEL,REFDES from mis where status = 'DONE' and prep_status not in ('REJECT','DONE','N/A') and project = '{project}' AND IS_KITTING = 1 AND IS_KANBAN = 0 order by id desc";
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
                mis.STATUS = Convert.ToString(read.GetValue(19));
                mis.PREP_STATUS = Convert.ToString(read.GetValue(20));
                mis.SN = Convert.ToString(read.GetValue(21));
                mis.MODEL = Convert.ToString(read.GetValue(22));
                mis.REFDES = Convert.ToString(read.GetValue(23));


                // mis.= Convert.ToString(read.GetValue(21));


                if (mis.STATUS.Equals("NOT FINISH") && mis.PREP_STATUS.Equals("NOT FINISH"))
                {
                    mis.ROW_COLOR = "background-color:white";
                    mis.DISABLED = "disabled";
                }

                else if (mis.STATUS.Equals("REJECT") && mis.PREP_STATUS.Equals("NOT FINISH"))
                {
                    mis.ROW_COLOR = "background-color:red";
                    mis.DISABLED = "disabled";

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


                // rebuildStatus(ref mis);

                //if (mis.STATUS.Equals("REJECT"))
                //    mis.PREP_STATUS_COLOR = "background-color:red";

                //else if (mis.STATUS.Equals("NOT FINISH"))
                //    mis.STATUS_COLOR = "background-color:yellow";

                //else if (mis.STATUS.Equals("DONE") && mis.PREP_STATUS_COLOR.Equals("NOT FINISH"))
                //    mis.STATUS_COLOR = "background-color:greenyellow";

                misList.Add(mis);
            }

            conn.Dispose();
            conn.Close();

            return misList;
        }


        public bool checkAdmin(string userid,string project)
        {
            bool valid = false;
            var conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=goodrich;Password=o5goodrichtcp;");
            conn.Open();

            OracleCommand InsertCommand = new OracleCommand();
            InsertCommand.Connection = conn;
            InsertCommand.CommandType = CommandType.Text;
            InsertCommand.CommandText = $"select empid from mis_prep_admin where upper(empid) = '{userid.ToUpper()}' and project = '{project}'";

            OracleDataReader read = InsertCommand.ExecuteReader();
            while (read.Read())
            {
                valid = true;

            }

            conn.Dispose();
            conn.Close();

            return valid;

        }

        public JObject getPrepData( string stype , string project)
        {
    
            JObject json = new JObject();

            var conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=goodrich;Password=o5goodrichtcp;");
            conn.Open();
            OracleCommand InsertCommand = new OracleCommand();
            InsertCommand.Connection = conn;
            InsertCommand.CommandType = CommandType.Text;
            InsertCommand.CommandText = $"select stype,owner,project from  mis_prep_control where stype = '{stype}' and project = '{project}'";

            OracleDataReader read = InsertCommand.ExecuteReader();
            while (read.Read())
            {

                json.Add("STYPE", read.GetValue(0).ToString());
                json.Add("OWNER", read.GetValue(1).ToString());
                json.Add("PROJECT", read.GetValue(2).ToString());
            


            }

            conn.Dispose();
            conn.Close();

            return json;

        }

        public MIS getMIS(string project,string id)
        {
            MIS mis = new MIS();
            var conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=goodrich;Password=o5goodrichtcp;");
            conn.Open();


            OracleCommand InsertCommand = new OracleCommand();
            InsertCommand.Connection = conn;
            InsertCommand.CommandType = CommandType.Text;
            InsertCommand.CommandText = $"select id,CREATEDON,part_number,qty,requestedby,id_scan,stype,sbin,tr_number,to_number,createdby,receivedby,REQUESTEREXT,CONFIRMEDON,RECEIVEDON,STATUS,PREP_BY from mis where id = {id} and project='{project}' and prep_sta='NOT DONE'";

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

            }

            conn.Dispose();
            conn.Close();

            return mis;
        }

        public int updateData(string id, string tr, string to, string prepby, string comments, string prepstatus)
        {
            int valid = 0;
            var conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=goodrich;Password=o5goodrichtcp;");
            conn.Open();


            OracleCommand InsertCommand = new OracleCommand();
            InsertCommand.Connection = conn;
            InsertCommand.CommandType = CommandType.Text;
            InsertCommand.CommandText = $"update MIS set tr_number  = '{tr}',to_number  = '{to}', PREP_COMMENT = '{comments}' ,PREP_BY  = '{prepby}', PREP_DATE = sysdate , prep_status = '{prepstatus}' where id = {id}";

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



    }
}