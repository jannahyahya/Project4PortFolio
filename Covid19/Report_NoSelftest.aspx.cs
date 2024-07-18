using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class PublicPage_Covid19_Report_NoSelftest : System.Web.UI.Page
{
    string BU_CONN = ConString.csCentre;
    protected void Page_Load(object sender, EventArgs e)
    {
        var bu = DropDownList1.SelectedItem.Text;

        //if (bu == "Honeywell")
        //{
        //    BU_CONN = ConString.csHoney;
        //}
        //else if (bu == "Thales")
        //{
        //    BU_CONN = ConString.csThales;
        //}
        //else if (bu == "Collins")
        //{
        //    BU_CONN = ConString.csHs;
        //}
        //else
        //{
        //    BU_CONN = ConString.csZodiac;
        //}

        //if (!IsPostBack)
        //{
            fillTable();
        //}
    }

    protected void fillTable()
    {
        if (ViewState["DataRecord"] == null)
        {
            var ds = new DataSet();
            new dbRead(BU_CONN).dynamicDataSet(ds, string.Format("select * from NO_SELFTEST_CMY_ONLY"));

            //dataTable.HeaderRow.TableSection = TableRowSection.TableHeader;
            dataTable.DataSource = ds;
            dataTable.DataBind();

            if (dataTable.Rows.Count > 0)
            {
                //Adds THEAD and TBODY Section.
                dataTable.HeaderRow.TableSection = TableRowSection.TableHeader;

                //Adds TH element in Header Row.  
                dataTable.UseAccessibleHeader = true;

                //Adds TFOOT section. 
                dataTable.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillTable();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        creatXLSDownloadfile();
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

        dataTable.GridLines = GridLines.Both;
        dataTable.HeaderStyle.Font.Bold = true;
        dataTable.RenderControl(htmltextwrtter);

        Response.Write(strwritter.ToString());
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }

}