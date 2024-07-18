using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PublicPage_emp_info_certactive_detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        fillTable();
    }

    protected void fillTable()
    {
        var cert_name = Request.QueryString["cert_name"];

        if (ViewState["DataRecord"] == null)
        {
            var ds = new DataSet();
            if (cert_name == "Total")
                new dbRead(ConString.csCentre).dynamicDataSet(ds, string.Format("select * from training_cert_current_active where TRAIN_CERT_EXPIRATION_DATE is not null and TRAIN_CERT_EXPIRATION_DATE > sysdate"));
            else
                new dbRead(ConString.csCentre).dynamicDataSet(ds, string.Format("select * from training_cert_current_active where TRAIN_CERT_EXPIRATION_DATE is not null and TRAIN_CERT_EXPIRATION_DATE > sysdate and TRAIN_REQUIREMENT_NAME = '{0}'", cert_name));


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

    protected void dataTable_RowDataBound(object sender, GridViewRowEventArgs e)
    {

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
        string FileName = "Certificate_Active_List.xls";
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