using AutoRSN.Models;
using ClosedXML.Excel;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace AutoRSN.Repositories
{
    public class InternalReportRepository
    {

        public List<InternalReport> getReport(string rsnCreatedOn, string user, string realname, string customergroup)
        {
            List<InternalReport> reportList = new List<InternalReport>();
            OracleConnection conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=rsn;Password=o5rsntcp;");
            conn.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;

            if (customergroup != "")
            {
                cmd.CommandText = $"select a.matnr,a.kdmat,a.arktx,b.servicetype,b.billto,'True' as CHARGE,b.ex_factory,b.fca_fob,b.door_to_door, " +
                                    "b.door_to_port_desc,a.exportcontrol,'N' as RTV,'N' AS RMA, a.shipqty,a.edatu,a.shipdate,a.openqty,a.vbeln,a.kunnr2, " +
                                    "a.bstnk,a.posex,a.posnr,nvl(netpr, 0), " +
                                    "CASE " +
                                      "WHEN a.shipqty = a.openqty THEN 'FULL' " +
                                      "WHEN a.shipqty < a.openqty AND a.shipqty > 0 THEN 'PARTIAL' " +
                                      "WHEN a.shipqty = 0  THEN 'ZERO' " +
                                    "END AS STATUS, " +
                                    "a.remarks,a.rsnno, " +
                                    "CASE " +
                                      "WHEN a.customforwarder is null THEN b.forwarder " +
                                      "WHEN a.customforwarder is not null then a.customforwarder " +
                                    "END as FORWARDER, " +
                                    "a.shipshift,(nvl(a.shipqty, 0) * nvl(a.netpr, 0)) as AMOUNT,a.createdby,b.customergroup,a.generatedon,a.SHIPSTATUS,a.SHIPSTATUSON " +
                                      $"from z_t_rsn_final a , rsn_st_customer b  where a.kunnr1 = b.customercode and kunnr1 in (select customercode from rsn_st_customer where customergroup  = '{customergroup}') " +
                                        $"and a.GENERATEDON between to_date('{rsnCreatedOn} 00:00:00', 'mm/dd/yyyy hh24: mi:ss') and to_date('{rsnCreatedOn} 23:59:00', 'mm/dd/yyyy hh24: mi:ss')";
            }
            else
            {

                cmd.CommandText = $"select a.matnr,a.kdmat,a.arktx,b.servicetype,b.billto,'True' as CHARGE,b.ex_factory,b.fca_fob,b.door_to_door,a.SHIPSTATUSON " +
                                   "b.door_to_port_desc,a.exportcontrol,'N' as RTV,'N' AS RMA, a.shipqty,a.edatu,a.shipdate,a.openqty,a.vbeln,a.kunnr2, " +
                                   "a.bstnk,a.posex,a.posnr,nvl(netpr, 0), " +
                                   "CASE " +
                                     "WHEN a.shipqty = a.openqty THEN 'FULL' " +
                                     "WHEN a.shipqty < a.openqty AND a.shipqty > 0 THEN 'PARTIAL' " +
                                     "WHEN a.shipqty = 0  THEN 'ZERO' " +
                                   "END AS STATUS, " +
                                   "a.remarks,a.rsnno, " +
                                   "CASE " +
                                     "WHEN a.customforwarder is null THEN b.forwarder " +
                                     "WHEN a.customforwarder is not null then a.customforwarder " +
                                   "END as FORWARDER, " +
                                   "a.shipshift,(nvl(a.shipqty, 0) * nvl(a.netpr, 0)) as AMOUNT,a.createdby,b.customergroup,a.generatedon,a.SHIPSTATUS,a.SHIPSTATUSON " +
                                     $"from z_t_rsn_final a , rsn_st_customer b  where a.kunnr1 = b.customercode " +
                                       $"and a.GENERATEDON between to_date('{rsnCreatedOn} 00:00:00', 'mm/dd/yyyy hh24: mi:ss') and to_date('{rsnCreatedOn} 23:59:00', 'mm/dd/yyyy hh24: mi:ss')";
            }
            Debug.WriteLine(cmd.CommandText);
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                InternalReport report = new InternalReport();
                report.MODEL = dr.GetValue(0).ToString();
                report.CUSPN = dr.GetValue(1).ToString();
                report.DESC = dr.GetValue(2).ToString();
                report.SERVICETYPE = dr.GetValue(3).ToString();
                report.SHIPMODE = dr.GetValue(4).ToString();
                report.CHARGE = dr.GetValue(5).ToString();

                if (Convert.ToBoolean(dr.GetValue(6).ToString()))
                    report.INCOTERM = "EX FACTORY";
                if (Convert.ToBoolean(dr.GetValue(7).ToString()))
                    report.INCOTERM = "FOB";
                if (Convert.ToBoolean(dr.GetValue(8).ToString()))
                    report.INCOTERM = "DOOR TO DOOR";
                if (Convert.ToBoolean(dr.GetValue(9).ToString()))
                    report.INCOTERM = "DOOR TO PORT DESC";


                report.EXPORTCTRL = dr.GetValue(10).ToString();
                report.RTV = dr.GetValue(11).ToString();
                report.RMA = dr.GetValue(12).ToString();
                report.STOCK = dr.GetValue(13).ToString();

                if (dr.GetValue(14) != null)
                    report.REQUIRED_DATE = DateTime.ParseExact(dr.GetValue(14).ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");

                report.SHIPDATE = dr.GetValue(15).ToString();
                report.SOQTY = dr.GetValue(16).ToString();

                report.SO = dr.GetValue(17).ToString();
                report.SHIPTOPARTY = dr.GetValue(18).ToString();
                report.PO = dr.GetValue(19).ToString();
                report.POLINE = dr.GetValue(20).ToString();
                report.SOLINE = dr.GetValue(21).ToString();
                report.PRICE = dr.GetValue(22).ToString();
                report.STATUS = dr.GetValue(23).ToString();
                report.REMARKS = dr.GetValue(24).ToString();
                report.INDICATOR = dr.GetValue(25).ToString();
                report.FORWARDER = dr.GetValue(26).ToString();
                report.TIMING = dr.GetValue(27).ToString();
                report.AMOUNT = dr.GetValue(28).ToString();
                report.USERNAME = dr.GetValue(29).ToString();
                report.DATE_TIME = rsnCreatedOn;
                report.CUSTOMER = dr.GetValue(30).ToString();
                report.GENERATEDON = dr.GetValue(31).ToString();
                report.SHIPSTATUS = dr.GetValue(32).ToString();
                report.SHIPSTATUSON = dr.GetValue(33).ToString();
                reportList.Add(report);



            }

            conn.Dispose();
            conn.Close();
            return reportList;

        }
    }
}