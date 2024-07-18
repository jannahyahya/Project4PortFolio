using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PublicPage_EmpInfo_Emp_info_register_defect : System.Web.UI.Page
{
    string cs = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            refreshData();
            refreshData1();
            fillDropDown();
        }
    }

    private void refreshData()
    {
        new dbRead(ConString.csHoney).fillListbox(ListBox1, "select defect_code||' : '||description from defect_code order by defect_code asc");
    }

    private void refreshData1()
    {
        ListBox2.Items.Clear();
        new dbRead(ConString.csHoney).fillListbox(ListBox2, string.Format(@"select resource_name from station_resource where resource_name like 'PR_%' group by resource_name"));

    }

    protected void Remove_Click(object sender, EventArgs e)
    {
        var desc = ListBox1.SelectedItem.Text.Split(':')[0].Trim();
        var pro = ListBox3.SelectedItem.Text;

        new dbWrite(ConString.csHoney).dbNonQuery(string.Format(@"delete from CUST_TB_GRAPE_DEFECT_STATION where DEFECT_CODE = '{0}' and STATION = '{1}'", desc, pro));
        ListBox3.Items.Clear();
        new dbRead(ConString.csHoney).fillListbox(ListBox3, "SELECT DS.STATION FROM CUST_TB_GRAPE_DEFECT_STATION DS, STATION_RESOURCE SR where DS.STATION=SR.RESOURCE_NAME AND DEFECT_CODE = '" + desc + "'");
    }

    protected void Search_Click(object sender, EventArgs e)
    {
        ListBox1.Items.Clear();
        var t1 = TextBox1.Text;

        new dbRead(ConString.csHoney).fillListbox(ListBox1, (string.Format(@"select defect_code||' : '||description from defect_code where upper(defect_code) like '%{0}%' or upper(description) like '%{0}%' order by defect_code asc", t1.ToUpper())));
    }
    protected void Search1_Click(object sender, EventArgs e)
    {
        ListBox2.Items.Clear();
        var t2 = TextBox2.Text;
        new dbRead(ConString.csHoney).fillListbox(ListBox2, (string.Format(@"select station||' : '||station_name from station_master where upper(station) like '%{0}%' or upper(station_name) like '%{0}%' order by station asc", t2.ToUpper())));
    }

    protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        var def = ListBox1.SelectedItem.Text;
        var df = def.Split(':')[0].Trim();
        ListBox3.Items.Clear();
        new dbRead(ConString.csHoney).fillListbox(ListBox3, (string.Format(@"SELECT STATION FROM CUST_TB_GRAPE_DEFECT_STATION where DEFECT_CODE = '" + df + "'")));
    }

    protected void Bind_Click(object sender, EventArgs e)
    {
        var defect = ListBox1.SelectedIndex;
        var def = ListBox1.SelectedItem.Text;
        var df = def.Split(':')[0].Trim();

        var process = ListBox2.SelectedIndex;
        var proc = ListBox2.SelectedItem.Text;
        var pro = proc.Split(':')[0].Trim();

        if (!(new dbRead(ConString.csHoney).hasRowChecker(string.Format(@"select STATION from CUST_TB_GRAPE_DEFECT_STATION where DEFECT_CODE = '{0}' and STATION = '{1}'", df, pro))))
        {
            new dbWrite(ConString.csHoney).dbNonQuery(string.Format(@"insert into CUST_TB_GRAPE_DEFECT_STATION (DEFECT_CODE, STATION, DATETIME) values ('" + df + "', '" + pro + "', sysdate )"));
            ListBox3.Items.Clear();
            new dbRead(ConString.csHoney).fillListbox(ListBox3, "SELECT STATION FROM CUST_TB_GRAPE_DEFECT_STATION where DEFECT_CODE = '" + df + "'");
            ListBox2.BorderColor = Color.LightGray;
            error.Visible = false;
        }
        else
        {
            ListBox2.BorderColor = Color.Red;
            error.Visible = true;
        }
    }

    protected void Reset_Click(object sender, EventArgs e)
    {
        TextBox1.Text = "";
        ListBox1.Items.Clear();
        refreshData();
    }

    protected void Reset1_Click(object sender, EventArgs e)
    {
        TextBox2.Text = "";
        ListBox2.ClearSelection();
        refreshData1();
    }

    protected void Reset2_Click(object sender, EventArgs e)
    {
        TextBox1.Text = "";
        TextBox2.Text = "";
        ListBox1.ClearSelection();
        ListBox2.ClearSelection();
        ListBox2.BorderColor = Color.LightGray;
        ListBox1.Items.Clear();
        ListBox2.Items.Clear();
        ListBox3.Items.Clear();
        error.Visible = false;
        refreshData();
        refreshData1();
    }

    //_________________________________________________________________________________________________________________________________________________________________


    protected void fillListBox()
    {
        var res_name = dropdown11.SelectedItem.Text;       
        listbox11.Items.Clear();
        new dbRead(ConString.csHoney).fillListbox(listbox11, string.Format(@"select description from station_resource where resource_name = '{0}' order by station", res_name));

    }

    protected void Remove1_Click(object sender, EventArgs e)
    {
        var index = listbox11.SelectedIndex;
        var desc = listbox11.SelectedItem.Text;
        var res = dropdown11.SelectedItem.ToString();
        new dbWrite(ConString.csHoney).dbNonQuery(string.Format(@"delete from station_resource where resource_name = '{0}' and description = '{1}'", res, desc));
        listbox11.Items.RemoveAt(index);
        fillListBox();
    }

    protected void Copy_Click(object sender, EventArgs e)
    {
        rname.Text = dropdown11.SelectedItem.Text;
    }

    protected void Insert_Click(object sender, EventArgs e)
    {
        if (dropwdown22.SelectedItem.ToString() != "Please select one")
        {
            var selected = dropwdown22.SelectedItem.ToString();
            ListItem item = new ListItem(selected);
            if (!(listbox22.Items.Contains(item)))
            {
                var station = dropwdown22.SelectedItem.ToString();
                listbox22.Items.Add(station);
            }
        }
        dropwdown22.Focus();
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        var res = "PR_" + rname.Text;
        var desc = des.Text;

            foreach (var listBoxItem in listbox22.Items)
            {

                new dbWrite(ConString.csHoney).dbNonQuery(string.Format(@"insert into STATION_RESOURCE (STATION, RESOURCE_NAME, DESCRIPTION, ACTOR, DATETIME, STATUS, CREATE_DATE) values('{0}','{1}','{2}','{3}',sysdate,'A',sysdate)", listBoxItem, res, listBoxItem + " : " + desc, Session["empNo"]));
            }
        clearBox();
        fillDropDown();
    }

    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillListBox();
    }

    protected void fillDropDown()
    {
        //var res_name = dropdown11.SelectedItem.Text;
        //res_name = res_name.Substring(2);
        dropdown11.Items.Clear();
        new dbRead(ConString.csHoney).fillDropdown(dropdown11, string.Format(@"select distinct(resource_name) from station_resource where resource_name like 'PR_%' order by resource_name"));
        dropwdown22.Items.Clear();
        new dbRead(ConString.csHoney).fillDropdown(dropwdown22, string.Format(@"select distinct(station) from station_master order by station"));

    }

    protected void Reset4_Click(object sender, EventArgs e)
    {
        clearBox();
    }

    protected void clearBox()
    {
        rname.Text = "";
        des.Text = "";
        listbox11.Items.Clear();
        listbox22.Items.Clear();
        dropdown11.ClearSelection();
        dropwdown22.ClearSelection();
    }

}