using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PublicPage_Covid19_DashBoard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        displayData();
    }

    protected void displayData()
    {
        var isMorning = true;
        var currTime = Int32.Parse(DateTime.Now.ToString("HH"));

        if (currTime > 17)
        {
            isMorning = false;
        }
        else
        {
            isMorning = true;
        }

        var curMax = isMorning ? "MAX_EMP_MORNING" : "MAX_EMP_NIGHT";
        var curDay = isMorning ? "(trunc(sysdate) + 6/24) and (trunc(sysdate) + 16/24)" : "(trunc(sysdate) + 18/24) and (trunc(sysdate) + 23/24)";
        var noDayNight = isMorning ? "NO_DAY" : "NO_NIGHT";

        try
        {

            var totalEmp = new dbRead(ConString.csGoodrich).oneData("select sum(value) total from CUST_TB_MCO_SETTING where setting_name = 'MAX_EMP_MORNING' or setting_name = 'MAX_EMP_NIGHT'");
            var totalScan = new dbRead(ConString.csGoodrich).oneData("select a + b as scan from (select (select count(distinct(user_id)) totalScan from CUST_TB_MCO_ENTRY_LOG where datetime between(trunc(sysdate) + 17 / 24) and(trunc(sysdate) + 23 / 24)) a,(select count(distinct(user_id)) totalScan from CUST_TB_MCO_ENTRY_LOG where datetime between(trunc(sysdate) + 5 / 24) and(trunc(sysdate) + 16 / 24)) b from dual)");
            var yTotalScan = new dbRead(ConString.csGoodrich).oneData("select a + b as scan from (select (select count(distinct(user_id)) totalScan from CUST_TB_MCO_ENTRY_LOG where datetime between(trunc(sysdate - 1) + 17 / 24) and(trunc(sysdate - 1) + 23 / 24)) a,(select count(distinct(user_id)) totalScan from CUST_TB_MCO_ENTRY_LOG where datetime between(trunc(sysdate - 1) + 5 / 24) and(trunc(sysdate - 1) + 16 / 24)) b from dual)");
            //var curEmp = new dbRead(ConString.csGoodrich).oneData("select value from CUST_TB_MCO_SETTING where setting_name = '" + curMax + "'");
            //var curScan = new dbRead(ConString.csGoodrich).oneData("select count(distinct(user_id)) totalScan from CUST_TB_MCO_ENTRY_LOG where datetime between " + curDay );
            //var noPercent = new dbRead(ConString.csGoodrich).oneData("select value from CUST_TB_MCO_SETTING where setting_name = '" + noDayNight + "'");

            var morningEmp = new dbRead(ConString.csGoodrich).oneData("select value from CUST_TB_MCO_SETTING where setting_name = 'MAX_EMP_MORNING'");
            var morningScan = new dbRead(ConString.csGoodrich).oneData("select count(distinct(user_id)) totalScan from CUST_TB_MCO_ENTRY_LOG where datetime between (trunc(sysdate) + 5/24) and (trunc(sysdate) + 16/24)");
            var yMorningScan = new dbRead(ConString.csGoodrich).oneData("select count(distinct(user_id)) totalScan from CUST_TB_MCO_ENTRY_LOG where datetime between (trunc(sysdate - 1) + 5/24) and (trunc(sysdate - 1) + 16/24)");

            var nightEmp = new dbRead(ConString.csGoodrich).oneData("select value from CUST_TB_MCO_SETTING where setting_name = 'MAX_EMP_NIGHT'");
            var nightScan = new dbRead(ConString.csGoodrich).oneData("select count(distinct(user_id)) totalScan from CUST_TB_MCO_ENTRY_LOG where datetime between (trunc(sysdate) + 17/24) and (trunc(sysdate) + 23/24)");
            var yNightScan = new dbRead(ConString.csGoodrich).oneData("select count(distinct(user_id)) totalScan from CUST_TB_MCO_ENTRY_LOG where datetime between (trunc(sysdate - 1) + 17/24) and (trunc(sysdate - 1) + 23/24)");

            var morningPercent = (double.Parse(morningScan) / double.Parse(morningEmp)) * 100;
            var yMorningPercent = (double.Parse(yMorningScan) / double.Parse(morningEmp)) * 100;
            var nightPercent = (double.Parse(nightScan) / double.Parse(nightEmp)) * 100;
            var yNightPercent = (double.Parse(yNightScan) / double.Parse(nightEmp)) * 100;

            var inPercentDaily = (double.Parse(totalScan) / double.Parse(totalEmp)) * 100;
            var inPercentYesterday = (double.Parse(yTotalScan) / double.Parse(totalEmp)) * 100;
            //var curBalance = Int32.Parse(noPercent) - Int32.Parse(curScan);

            morningEmpDisp.Text = morningEmp;
            morningScanDisp.Text = morningScan;
            nightEmpDisp.Text = nightEmp;
            nightScanDisp.Text = nightScan;

            yMorningEmpDisp.Text = morningEmp;
            yMorningScanDisp.Text = yMorningScan;
            yNightEmpDisp.Text = nightEmp;
            yNightScanDisp.Text = yNightScan;

            Label1.Text = totalEmp;
            Label2.Text = totalScan;

            Label3.Text = totalEmp;
            Label4.Text = yTotalScan;
            //currAvailable.Text = curBalance.ToString();
            tMorning.Text = morningPercent.ToString("0.00") + "%";
            tNight.Text = nightPercent.ToString("0.00") + "%";

            yMorning.Text = yMorningPercent.ToString("0.00") + "%";
            yNight.Text = yNightPercent.ToString("0.00") + "%";

            tDay.Text = inPercentDaily.ToString("0.00") + "%";
            yDay.Text = inPercentYesterday.ToString("0.00") + "%";
        }
        catch
        {

        }
    }

    //public string totalEmp
    //{
    //    get
    //    {
    //        var totalEmp = new dbRead(ConString.csGoodrich).oneData("select value from CUST_TB_MCO_SETTING where setting_name = 'MAX_EMP'");
    //        return totalEmp;
    //    }
    //}
    //public string totalScan
    //{
    //    get
    //    {
    //        var totalScan = new dbRead(ConString.csGoodrich).oneData("select count(distinct(user_id)) totalScan from CUST_TB_MCO_ENTRY_LOG where datetime between (trunc(sysdate) + 6/24) and (trunc(sysdate) + 16/24)");
    //        return totalScan;
    //    }
    //}

    //public string totalAvailable
    //{
    //    get
    //    {
    //        var totalEmp = new dbRead(ConString.csGoodrich).oneData("select value from CUST_TB_MCO_SETTING where setting_name = 'NO_DAY'");
    //        var totalScan = new dbRead(ConString.csGoodrich).oneData("select count(distinct(user_id)) totalScan from CUST_TB_MCO_ENTRY_LOG where datetime between (trunc(sysdate) + 6/24) and (trunc(sysdate) + 16/24)");
    //        var totalBalance = Int32.Parse(totalEmp) - Int32.Parse(totalScan);
    //        return totalBalance.ToString();
    //    }
    //}
    //public string inPercentMorning
    //{
    //    get
    //    {
    //        var totalEmp = new dbRead(ConString.csGoodrich).oneData("select value from CUST_TB_MCO_SETTING where setting_name = 'MAX_EMP_MORNING'");
    //        var totalScan = new dbRead(ConString.csGoodrich).oneData("select count(distinct(user_id)) totalScan from CUST_TB_MCO_ENTRY_LOG where datetime between (trunc(sysdate) + 6/24) and (trunc(sysdate) + 16/24)");

    //        var inPercent = (double.Parse(totalScan) / double.Parse(totalEmp)) * 100;

    //        return inPercent.ToString("0.00");
    //    }
    //}
    //public string inPercentNight
    //{
    //    get
    //    {
    //        var totalEmp = new dbRead(ConString.csGoodrich).oneData("select value from CUST_TB_MCO_SETTING where setting_name = 'MAX_EMP_NIGHT'");
    //        var totalScan = new dbRead(ConString.csGoodrich).oneData("select count(distinct(user_id)) totalScan from CUST_TB_MCO_ENTRY_LOG where datetime between (trunc(sysdate) + 18/24) and (trunc(sysdate) + 23/24)");

    //        var inPercent = (double.Parse(totalScan) / double.Parse(totalEmp)) * 100;

    //        return inPercent.ToString("0.00");
    //    }
    //}
    //public string inPercentDaily
    //{
    //    get
    //    {
    //        var totalEmp = new dbRead(ConString.csGoodrich).oneData("select sum(value) total from CUST_TB_MCO_SETTING where setting_name = 'MAX_EMP_MORNING' or setting_name = 'MAX_EMP_NIGHT'");
    //        var totalScan = new dbRead(ConString.csGoodrich).oneData("select a + b as scan from (select (select count(distinct(user_id)) totalScan from CUST_TB_MCO_ENTRY_LOG where datetime between(trunc(sysdate - 1) + 18 / 24) and(trunc(sysdate - 1) + 23 / 24)) a,(select count(distinct(user_id)) totalScan from CUST_TB_MCO_ENTRY_LOG where datetime between(trunc(sysdate) + 6 / 24) and(trunc(sysdate) + 16 / 24)) b from dual)");

    //        var inPercent = (double.Parse(totalScan) / double.Parse(totalEmp)) * 100;

    //        return inPercent.ToString("0.00");
    //    }
    //}
}