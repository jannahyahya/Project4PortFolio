using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PublicPage_Emp_info_CertExpired : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        fillTable();
    }

    protected void fillTable()
    {
        if (ViewState["DataRecord"] == null)
        {
            var ds = new DataSet();
            new dbRead(ConString.csCentre).dynamicDataSet(ds, string.Format("select rownum as no,cert_name,total from(select nvl(TRAIN_REQUIREMENT_NAME, 'Total') as cert_name, count(TRAIN_REQUIREMENT_NAME) as total from TRAINING_CERT_CURRENT_ACTIVE where train_cert_expiration_date is not null and train_cert_expiration_date < sysdate group by rollup(TRAIN_REQUIREMENT_NAME) order by total)"));

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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string HyperLinkValue = "emp_info_certexpired_detail.aspx?cert_name=" + e.Row.Cells[1].Text;
            HyperLink myLink = new HyperLink();
            myLink.NavigateUrl = HyperLinkValue;
            myLink.Text = e.Row.Cells[2].Text;
            e.Row.Cells[2].Controls.Add(myLink);
        }
    }
}