using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PublicPage_Emp_info_certactive : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        fillTable();
    }

    protected void fillTable()
    {
        var radIndex = RadioButtonList1.SelectedIndex.ToString();

        var sqlString = string.Format("select manager,supervisor,employee,process, count(employee) as total from(select substr(z.MGR_CLS_UNIQUE_KEY,3)|| ' : ' || y.ee_fullname as manager,substr(z.SUP_CLS_UNIQUE_KEY,3)|| ' : ' || x.ee_fullname as supervisor,substr(z.CLS_UNIQUE_KEY,3)|| ' : ' || z.ee_fullname as employee,resource_name as process from (select c.sn,c.so,c.station,out_date_time,user_id,resource_name from (select * from (select a.sn,a.so,a.defect_code,b.station,b.resource_name from (select * from defect where datetime > sysdate - 10 ) a left join(select defect_code,resource_name,x.station from CUST_TB_GRAPE_DEFECT_STATION z left join station_resource x on z.station =  x.resource_name) b on a.defect_code = b.defect_code) where station is not null)c left join(select sn,so,station,user_id,out_date_time from tracking ) d on c.sn = d.sn and c.so = d.so and c.station = d.station where user_id is not null) d left join CENTER_HRDW_MV@centre z on  d.user_id = z.employee_empno_full left join CENTER_HRDW_MV@centre x on  z.SUP_CLS_UNIQUE_KEY = x.CLS_UNIQUE_KEY left join CENTER_HRDW_MV@centre y on  z.MGR_CLS_UNIQUE_KEY = y.CLS_UNIQUE_KEY) abc where employee != ' : ' group by manager,supervisor,employee,process");

        if(radIndex == "1")
        {
            sqlString = string.Format("select manager,supervisor,process, count(employee) as total from(select substr(z.MGR_CLS_UNIQUE_KEY,3)|| ' : ' || y.ee_fullname as manager,substr(z.SUP_CLS_UNIQUE_KEY,3)|| ' : ' || x.ee_fullname as supervisor,substr(z.CLS_UNIQUE_KEY,3)|| ' : ' || z.ee_fullname as employee,resource_name as process from (select c.sn,c.so,c.station,out_date_time,user_id,resource_name from (select * from (select a.sn,a.so,a.defect_code,b.station,b.resource_name from (select * from defect where datetime > sysdate - 10 ) a left join(select defect_code,resource_name,x.station from CUST_TB_GRAPE_DEFECT_STATION z left join station_resource x on z.station =  x.resource_name) b on a.defect_code = b.defect_code) where station is not null)c left join(select sn,so,station,user_id,out_date_time from tracking ) d on c.sn = d.sn and c.so = d.so and c.station = d.station where user_id is not null) d left join CENTER_HRDW_MV@centre z on  d.user_id = z.employee_empno_full left join CENTER_HRDW_MV@centre x on  z.SUP_CLS_UNIQUE_KEY = x.CLS_UNIQUE_KEY left join CENTER_HRDW_MV@centre y on  z.MGR_CLS_UNIQUE_KEY = y.CLS_UNIQUE_KEY) abc where employee != ' : ' group by manager,supervisor,process");
        }
        else if (radIndex == "2")
        {
            sqlString = string.Format("select manager,process, count(employee) as total from(select substr(z.MGR_CLS_UNIQUE_KEY,3)|| ' : ' || y.ee_fullname as manager,substr(z.SUP_CLS_UNIQUE_KEY,3)|| ' : ' || x.ee_fullname as supervisor,substr(z.CLS_UNIQUE_KEY,3)|| ' : ' || z.ee_fullname as employee,resource_name as process from (select c.sn,c.so,c.station,out_date_time,user_id,resource_name from (select * from (select a.sn,a.so,a.defect_code,b.station,b.resource_name from (select * from defect where datetime > sysdate - 10 ) a left join(select defect_code,resource_name,x.station from CUST_TB_GRAPE_DEFECT_STATION z left join station_resource x on z.station =  x.resource_name) b on a.defect_code = b.defect_code) where station is not null)c left join(select sn,so,station,user_id,out_date_time from tracking ) d on c.sn = d.sn and c.so = d.so and c.station = d.station where user_id is not null) d left join CENTER_HRDW_MV@centre z on  d.user_id = z.employee_empno_full left join CENTER_HRDW_MV@centre x on  z.SUP_CLS_UNIQUE_KEY = x.CLS_UNIQUE_KEY left join CENTER_HRDW_MV@centre y on  z.MGR_CLS_UNIQUE_KEY = y.CLS_UNIQUE_KEY) abc where employee != ' : ' group by manager,process");
        }

        if (ViewState["DataRecord"] == null)
        {
            var ds = new DataSet();
            new dbRead(ConString.csHoney).dynamicDataSet(ds, sqlString);

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
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    string HyperLinkValue = "emp_info_certactive_detail.aspx?cert_name=" + e.Row.Cells[1].Text;
        //    HyperLink myLink = new HyperLink();
        //    myLink.NavigateUrl = HyperLinkValue;
        //    myLink.Text = e.Row.Cells[2].Text;
        //    e.Row.Cells[2].Controls.Add(myLink);
        //}
    }

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillTable();
    }
}