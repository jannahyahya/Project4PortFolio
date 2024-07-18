using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PublicPage_Covid19_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //new dbWrite(ConString.csCentre).creatCSVFile("select a.user_id,b.ee_fullname,c.ee_fullname as SV,d.ee_fullname as MGR,a.temperature,a.remark as no_symptom,a.datetime from (select * from CUST_TB_MCO_ENTRY_LOG@goodrich where datetime between to_date('26/04/2020 17:30:50','DD/MM/YYYY HH24:MI:SS') and to_date('02/05/2020 23:59:59','DD/MM/YYYY HH24:MI:SS')) a left join CENTER_HRDW_MV b on a.user_id = b.EMPLOYEE_EMPNO_FULL left join CENTER_HRDW_MV c on b.sup_cls_unique_key = c.CLS_UNIQUE_KEY left join CENTER_HRDW_MV d on b.mgr_cls_unique_key = d.CLS_UNIQUE_KEY order by a.datetime");

        if (TextBox1.Text != "" && TextBox2.Text != "")
        {
            //from
            var chromeDateFrom = TextBox1.Text;
            var dfr = DateTime.ParseExact(TextBox1.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture);
            chromeDateFrom = dfr.ToString("dd/MM/yyyy");
            //to
            var chromeDateTo = TextBox2.Text;
            var dto = DateTime.ParseExact(TextBox2.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture);
            chromeDateTo = dto.ToString("dd/MM/yyyy");

            var ds = new DataSet();
            new dbRead(ConString.csCentre).dynamicDataSet(ds, string.Format("select a.user_id,b.ee_fullname,c.ee_fullname as SV,d.ee_fullname as MGR,a.temperature,a.remark as no_symptom,a.datetime from (select * from CUST_TB_MCO_ENTRY_LOG@goodrich where datetime between to_date('{0} 00:00:00','DD/MM/YYYY HH24:MI:SS') and to_date('{1} 23:59:59','DD/MM/YYYY HH24:MI:SS')) a left join CENTER_HRDW_MV b on a.user_id = b.EMPLOYEE_EMPNO_FULL left join CENTER_HRDW_MV c on b.sup_cls_unique_key = c.CLS_UNIQUE_KEY left join CENTER_HRDW_MV d on b.mgr_cls_unique_key = d.CLS_UNIQUE_KEY order by a.datetime", chromeDateFrom, chromeDateTo));

            GridView1.DataSource = ds;
            GridView1.DataBind();

            creatXLSDownloadfile();
        }
    }

    protected void creatXLSDownloadfile()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "Report.xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);

        GridView1.GridLines = GridLines.Both;
        GridView1.HeaderStyle.Font.Bold = true;
        GridView1.RenderControl(htmltextwrtter);

        Response.Write(strwritter.ToString());
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
}