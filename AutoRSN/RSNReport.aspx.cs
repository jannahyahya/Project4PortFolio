using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using AutoRSN.Models;
using System.IO;
using AutoRSN.Repositories;
using AutoRSN.Services;

namespace AutoRSN
{
    public partial class RSNReport : System.Web.UI.Page
    {

        private List<List<AutoRSN.Models.Openso>> masterList;
        private IEnumerable<Openso> soList;
        private EmailServices mailService = new EmailServices();
        OpensoRepository cr = new OpensoRepository();
            

        private static Random random = new Random((int)DateTime.Now.Ticks);
        protected void Page_Load(object sender, EventArgs e)
        {

            if ((IsPostBack) == false)
            {
                string host = HttpContext.Current.Request.Url.Host;
                int port = HttpContext.Current.Request.Url.Port;


                logo.ImageUrl = "http://localhost:61357/Resources/logo.png";
                System.Diagnostics.Debug.WriteLine("http://" + host + ":" + port + "/" + "Resources/logo.png");

                //string reportFileName = RandomString(4) + RandomString(4);
                // string generatedReportPath =  Server.MapPath("~") + "\\" + "GeneratedReport\\" + reportFileName;


                soList = cr.GetOpensoByRSNNO(Convert.ToInt32(Session["ServerId"].ToString().ToUpper()), Request.QueryString["RSNNO"]);
                int row = 1;

                // int reportNum = Convert.ToInt32((string)Request.QueryString["reportNum"]);
                // System.Diagnostics.Debug.WriteLine("reportNum ==>" + Request.QueryString["reportNum"]);



                // if (Request.QueryString["rsnList"] != null)
                // {
                //     masterList =
                //new JavaScriptSerializer().Deserialize<List<List<AutoRSN.Models.Openso>>>(Request.QueryString["rsnList"]);
                //     list = masterList.ElementAt(0);
                // }RSNNO
                // else {
                //     masterList =
                //new JavaScriptSerializer().Deserialize<List<List<AutoRSN.Models.Openso>>>((string)Session["MasterList"]);
                //     list = masterList.ElementAt(reportNum);
                // }

                forwarderLbl.Text = cr.getShippingData(Command.getForwarder, soList.ElementAt(0).KUNNR1);
                shipDateLbl.Text = DateTime.Now.ToString();

                RSNNO.Text = soList.ElementAt(0).RSNNO;
                issueDateLbl.Text = DateTime.Now.ToString();
                rsnBarCode.Attributes.Add("OnLoad", "JsBarcode('#" + rsnBarCode.ID + "','" + soList.ElementAt(0).RSNNO + "');");

                billToName.Text = cr.getShippingData(Command.getCustName, soList.ElementAt(0).KUNNR1);
                addressA1Lbl.Text = cr.getShippingData(Command.getAddress1, soList.ElementAt(0).KUNNR1);
                addressA2Lbl.Text = cr.getShippingData(Command.getAddress2, soList.ElementAt(0).KUNNR1);

                System.Diagnostics.Debug.WriteLine(soList.ElementAt(0).KUNNR2);
                shipToName.Text = cr.getShippingData(Command.getCustName, soList.ElementAt(0).KUNNR2);
                addressB1Lbl.Text = cr.getShippingData(Command.getAddress1, soList.ElementAt(0).KUNNR2);
                addressB2Lbl.Text = cr.getShippingData(Command.getAddress2, soList.ElementAt(0).KUNNR2);

                telNoLbl.Text = cr.getContactNo(soList.ElementAt(0).KUNNR2);
                
                chargesChk.Checked = true;

                if (soList.ElementAt(0).EXPORTCONTROL.Equals("Y"))
                    exportControlCheckYes.Checked = true;
                else
                    exportControlCheckNo.Checked = false;


                foreach (AutoRSN.Models.Openso model in soList)
                {

                    RSNNO.Text = model.RSNNO;


                    TableRow tRow = new TableRow(); //each row
                    string poID = RandomString(4) + RandomString(4);
                    string soID = RandomString(4) + RandomString(4);
                    string clsID = RandomString(4) + RandomString(4);
                    string descID = RandomString(4) + RandomString(4);
                    string countryID = RandomString(4) + RandomString(4);
                    string fromLocationID =  RandomString(4) + RandomString(4);                   
                    string bomPNID = RandomString(4) + RandomString(4);
                    string qtyID = RandomString(4) + RandomString(4);


                    TableCell poCell = new TableCell();
                    TableCell soCell = new TableCell();
                    TableCell clsCell = new TableCell();
                    TableCell descCell = new TableCell();
                    TableCell countryCell = new TableCell();
                    TableCell fromLocationCell = new TableCell();
                    TableCell bomPNCell = new TableCell();
                    TableCell qtyCell = new TableCell();
                    TableCell uomCell = new TableCell();
                    TableCell unitPriceCell = new TableCell();
                    TableCell amountCell = new TableCell();
                    TableCell currencyCell = new TableCell();
                    TableCell revCell = new TableCell();
                    //poCell.Style.Add("text-align", "center");
                    //string bla = "<svg id='test" + "" + "'></svg>";

                    var poBar = new HtmlGenericControl("svg");
                    var so = new HtmlGenericControl("span");
                    var clsBar = new HtmlGenericControl("svg");     
                    var desc = new HtmlGenericControl("span");
                    var country = new HtmlGenericControl("span");
                    var fromLocation = new HtmlGenericControl("span");
                    var bomPN = new HtmlGenericControl("span");
                    var qtyPN = new HtmlGenericControl("span");

                    var uom = new HtmlGenericControl("span");
                    var unitPrice = new HtmlGenericControl("span");
                    var amount = new HtmlGenericControl("span");
                    var currency = new HtmlGenericControl("span");


                    poBar.ID = poID;
                    poBar.Attributes.Add("OnLoad", "JsBarcode('#" + poID + "','" + model.BSTNK + "');");
                    poBar.Attributes.Add("style","height:70px");
                    so.InnerText = model.VBELN;
                    clsBar.ID = clsID;
                    clsBar.Attributes.Add("OnLoad", "JsBarcode('#" + clsID + "','" + model.KDMAT + "');");
                    clsBar.Attributes.Add("style", "height:70px");
                    poCell.Controls.Add(poBar); 
                    soCell.Controls.Add(so);
                    clsCell.Controls.Add(clsBar);
                    desc.InnerText = model.ARKTX;
                    descCell.Controls.Add(desc);
                    country.InnerText = "MALAYSIA";
                    countryCell.Controls.Add(country);
                    fromLocation.InnerText = "MALAYSIA";
                    fromLocationCell.Controls.Add(fromLocation);
                    bomPN.InnerText = model.MATNR;
                    bomPNCell.Controls.Add(bomPN);
                    qtyPN.InnerText = model.OPENQTY;
                    qtyCell.Controls.Add(qtyPN);
                    uom.InnerText = "EA";
                    uomCell.Controls.Add(uom);
                    unitPrice.InnerText = model.NETPR ;
                    unitPriceCell.Controls.Add(unitPrice);
                    amount.InnerText = Convert.ToString(Convert.ToDouble(model.NETPR)* Convert.ToInt32(model.OPENQTY));
                    amountCell.Controls.Add(amount);
                    currency.InnerText = "USD";
                    currencyCell.Controls.Add(amount);

                    tRow.Controls.Add(poCell); //1
                    tRow.Controls.Add(soCell); //2
                    tRow.Controls.Add(clsCell); //3
                    tRow.Controls.Add(descCell); //4
                    tRow.Controls.Add(countryCell); //5
                    tRow.Controls.Add(fromLocationCell); //6
                    tRow.Controls.Add(bomPNCell); //7
                    tRow.Controls.Add(qtyCell); //8

                    tRow.Controls.Add(uomCell); //9
                    tRow.Controls.Add(unitPriceCell); //10
                    tRow.Controls.Add(amountCell); //11
                    tRow.Controls.Add(currencyCell); //12
                    tRow.Controls.Add(revCell); //13

                    mainTable.Rows.AddAt(row, tRow);
                    string runScript = "javascript:JsBarcode('#" + poID + "','" + model.BSTNK + "');";
                    System.Diagnostics.Debug.WriteLine(runScript);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "script", runScript, true);
                }


              ;

            }

        }

        protected override void Render(HtmlTextWriter writer)
        {
            // *** Write the HTML into this string builder
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            HtmlTextWriter hWriter = new HtmlTextWriter(sw);
            base.Render(hWriter);

            // *** store to a string
            string PageResult = sb.ToString(); //PageResult contains the HTML
                                               //System.Diagnostics.Debug.WriteLine(PageResult);

            string reportFileName = RandomString(4) + RandomString(4) + ".htm";
            string generatedReportPath = Server.MapPath("~") + "\\" + "GeneratedReport\\" + reportFileName;

            System.IO.File.WriteAllText(generatedReportPath, PageResult);

            OpensoRepository.updateReportID(Convert.ToInt32(Session["SERVERID"].ToString().ToUpper()), reportFileName, Request.QueryString["RSNNO"]);

            //mailService.sendMail(soList, reportFileName);
            // *** Write it back to the server
            writer.Write(PageResult);
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            string path = HttpContext.Current.Request.Url.AbsolutePath; 
            string host = HttpContext.Current.Request.Url.Host;
            int port = HttpContext.Current.Request.Url.Port;

        
        }

        [WebMethod(EnableSession = true)]
        public static void SetSession(string sessionval)
        {
            System.Diagnostics.Debug.WriteLine("reportNum ==>" + sessionval);
            HttpContext.Current.Session["reportNum"] = sessionval;
       
        }


        private string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }



    }
}
