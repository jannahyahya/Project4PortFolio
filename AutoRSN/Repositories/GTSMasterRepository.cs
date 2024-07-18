using AutoRSN.Models;
using Newtonsoft.Json;
using OfficeOpenXml;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace AutoRSN.Repositories
{
    public class GTSMasterRepository
    {

        public List<GTSMaster> getGTSList()
        {
            List<GTSMaster> list = new List<GTSMaster>();
            OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString());
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = $"select logical_sys_group,celpn,export_control,miti_permit,remark,ID from RSN_GTSMASTER order by ID";
            cmd.CommandType = CommandType.Text;

            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                GTSMaster gtsMaster = new GTSMaster() { LOGICAL_SYS_GROUP = Convert.ToString(reader.GetValue(0)),
                    CELPN = Convert.ToString(reader.GetValue(1)) , EXPORT_CONTROL = Convert.ToString(reader.GetValue(2)),
                     MITI_PERMIT = Convert.ToString(reader.GetValue(3)), REMARK = Convert.ToString(reader.GetValue(4)), ID = Convert.ToInt32(reader.GetValue(5))
                };

                list.Add(gtsMaster);
            }


            conn.Close();
            conn.Dispose();

            return list;

        }

        public GTSMaster getGTSMaster(int id)
        {
            GTSMaster gtsMaster = new GTSMaster();

            OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString());
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = $"select logical_sys_group,celpn,export_control,miti_permit,remark,ID from RSN_GTSMASTER where id={id}";
            cmd.CommandType = CommandType.Text;

            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                gtsMaster = new GTSMaster()
                {
                    LOGICAL_SYS_GROUP = Convert.ToString(reader.GetValue(0)),
                    CELPN = Convert.ToString(reader.GetValue(1)),
                    EXPORT_CONTROL = Convert.ToString(reader.GetValue(2)),
                    MITI_PERMIT = Convert.ToString(reader.GetValue(3)),
                    REMARK = Convert.ToString(reader.GetValue(4)),
                    ID = Convert.ToInt32(reader.GetValue(5))
                };

            }
            conn.Close();
            conn.Dispose();

            return gtsMaster;
        }

        public bool isGTSExist(string celpn)
        {

            bool exist = false;
            GTSMaster gtsMaster = new GTSMaster();

            OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString());
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = $"select logical_sys_group,celpn,export_control,miti_permit,remark,ID from RSN_GTSMASTER where celpn='{celpn}'";
            cmd.CommandType = CommandType.Text;
            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                exist = true;

            }
            conn.Close();
            conn.Dispose();

            return exist;
        }

        public int insertGTS(GTSMaster gtsMaster)
        {
            OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString());
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = $"insert into RSN_GTSMASTER(logical_sys_group,celpn,export_control,miti_permit,remark,ID) values('{gtsMaster.LOGICAL_SYS_GROUP}','{gtsMaster.CELPN}','{gtsMaster.EXPORT_CONTROL}','{gtsMaster.MITI_PERMIT}','{gtsMaster.REMARK}',GTS_MASTER_SEQ.nextval)";
            cmd.CommandType = CommandType.Text;

            int result = cmd.ExecuteNonQuery();

            conn.Close();
            conn.Dispose();

            return result;
        }


        public int updateGTS(GTSMaster gtsMaster)
        {
            OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString());
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = $"update RSN_GTSMASTER set logical_sys_group = '{gtsMaster.LOGICAL_SYS_GROUP}',celpn = '{gtsMaster.CELPN}',export_control = '{gtsMaster.EXPORT_CONTROL}',miti_permit = '{gtsMaster.MITI_PERMIT}',remark = '{gtsMaster.REMARK}' where id= {gtsMaster.ID}";
            cmd.CommandType = CommandType.Text;

            int result = cmd.ExecuteNonQuery();

            conn.Close();
            conn.Dispose();

            return result;
        }


      
        public int deleteGTS(int id)
        {
            OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ToString());
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = $"delete RSN_GTSMASTER where id={id}";
            cmd.CommandType = CommandType.Text;

            int result = cmd.ExecuteNonQuery();

            conn.Close();
            conn.Dispose();

            return result;
        }

        public byte[] exportExcel()
        {
            List<GTSMaster> list = getGTSList();

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet worksheet = pck.Workbook.Worksheets.Add("GTSMaster");
            List<string[]> headerRow = null;

            headerRow = new List<string[]>()
                {
                     new string[] { "Logical System Group", "Celestica PN", "Export Control?", "Miti Permit#", "Remark"}
                };

            // Determine the header range (e.g. A1:D1)
            string headerRange = "A1:" + Char.ConvertFromUtf32(headerRow[0].Length + 64) + "1";

            worksheet.Cells[headerRange].LoadFromArrays(headerRow);

            int row = 2;
            foreach (GTSMaster gtsMaster in list)
            {
                worksheet.Cells[row, 1].Value = Convert.ToString(gtsMaster.LOGICAL_SYS_GROUP);
                worksheet.Cells[row, 2].Value = Convert.ToString(gtsMaster.CELPN);
                worksheet.Cells[row, 3].Value = Convert.ToString(gtsMaster.EXPORT_CONTROL);
                worksheet.Cells[row, 4].Value = Convert.ToString(gtsMaster.MITI_PERMIT);
                worksheet.Cells[row, 5].Value = Convert.ToString(gtsMaster.REMARK);

                row++;

                worksheet.Column(1).AutoFit();
                worksheet.Column(2).AutoFit();
                worksheet.Column(3).AutoFit();
                worksheet.Column(4).AutoFit();
                worksheet.Column(5).AutoFit();
          
            }

            pck.Workbook.Properties.Title = "GTSMaster";
            return pck.GetAsByteArray();
        }
    }
}