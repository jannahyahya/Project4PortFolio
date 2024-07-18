using AutoRSN.Models;
using AutoRSN.Repositories;
using AutoRSN.Services;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace AutoRSN
{
    public partial class RSNPrintOut : System.Web.UI.Page
    {
        CustomerRepository custRepo = new CustomerRepository();
        private List<List<AutoRSN.Models.Openso>> masterList;
        private IEnumerable<Openso> soList;
        private EmailServices mailService = new EmailServices();
        OpensoRepository cr = new OpensoRepository();


        private static Random random = new Random((int)DateTime.Now.Ticks);


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if(Session["UserId"] == null)
                {
                    // Session["RequestFromReport"] = true;
                    Response.Write("<script> alert('You must login first...');</script>");
                    Response.Redirect("/AutoRSN/");
                    return;
                }



                string currentUrl = AutoRSN.Utility.Util.getWebUrl();

                

                string host = HttpContext.Current.Request.Url.Host;
                int port = HttpContext.Current.Request.Url.Port;
                logo.ImageUrl = "http://"+host + "/AutoRSN/Resources/customLogo.png";


                soList = cr.GetOpensoByRSNNO(1, Request.QueryString["RSNNO"]);

                //foreach (Openso openso in soList)
                //{
                //    Debug.WriteLine(openso.UNIQEL);
                //    Debug.WriteLine(openso.SHIPDATE);

                //}
                //This portion is to select freight data // 
                Customer customer = new Customer();
                Debug.WriteLine(soList.ElementAt(0).KUNNR1);
                customer = custRepo.GetCustomerByCustomerCodeMaterialGroup(1, soList.ElementAt(0).KUNNR1, "data");

                customer.EX_Factory = Convert.ToBoolean(customer.EX_Factory);
                customer.FCA_FOB = Convert.ToBoolean(customer.FCA_FOB);
                customer.DOOR_TO_DOOR = Convert.ToBoolean(customer.DOOR_TO_DOOR);
                customer.DOOR_TO_PORT_DESC = Convert.ToBoolean(customer.DOOR_TO_PORT_DESC);
                customer.DAP = Convert.ToBoolean(customer.DAP);
                customer.DDP = Convert.ToBoolean(customer.DDP);

                ChkBox_ex.Checked = customer.EX_Factory;
                ChkBox_fob.Checked = customer.FCA_FOB;
                ChkBox_d2d.Checked = customer.DOOR_TO_DOOR;

                ChkBox_d2p.Checked = customer.DOOR_TO_PORT_DESC;
                ChkBox_dap.Checked = customer.DAP;
                ChkBox_ddp.Checked = customer.DDP;

                Debug.WriteLine(customer.CUSTOMERCODE);
                Debug.WriteLine(customer.EX_Factory);
                Debug.WriteLine(customer.FCA_FOB);
                Debug.WriteLine(customer.DOOR_TO_DOOR);

                Debug.WriteLine(customer.DOOR_TO_PORT_DESC);
                Debug.WriteLine(customer.DAP);
                Debug.WriteLine(customer.DDP);

                TrixellRemark.Text = "";
                if (soList.ElementAt(0).KUNNR2.Contains("TRIXELL")) //check whether this shipto address is to TRIXELL
                {
                    TrixellRemark.Text= "HS Code #902290200 - Only for destination clearance purpose , Shipment must be palletized";
                    TrixellRemark.ForeColor = System.Drawing.Color.Blue;
                }
              
                ////////////////////////////


                System.Diagnostics.Debug.WriteLine(Request.QueryString["RSNNO"]);
                System.Diagnostics.Debug.WriteLine(soList.Count());
                remarksLbl.Text = "  " + soList.ElementAt(0).REMARKS;
                //remarksLbl.Attributes.Add("style", "color:");
                    int row = 1;

                if(soList.ElementAt(0).CUSTOMFORWARDER != null  || soList.ElementAt(0).CUSTOMACCOUNTNO != null || soList.ElementAt(0).CUSTOMBILLTO != null || soList.ElementAt(0).CUSTOMSERVICETYPE != null)
                {
                    try {
                        forwarder.Text = soList.ElementAt(0).CUSTOMFORWARDER;
                        Label5.Text = soList.ElementAt(0).CUSTOMACCOUNTNO;

                        if (soList.ElementAt(0).CUSTOMBILLTO.Contains("Bill to recipient"))
                            CheckBox1.Checked = true;
                        else if (soList.ElementAt(0).CUSTOMBILLTO.Contains("Bill to 3rd Party"))
                            CheckBox2.Checked = true;
                        else if (soList.ElementAt(0).CUSTOMBILLTO.Contains("Bill to Account"))
                            CheckBox3.Checked = true;


                        if (soList.ElementAt(0).CUSTOMSERVICETYPE.Contains("Normal"))
                            CheckBox16.Checked = true;
                        else if (soList.ElementAt(0).CUSTOMSERVICETYPE.Contains("Express"))
                            CheckBox17.Checked = true;
                    }
                    catch
                    {


                    }




                }

                else
                {
                   
                   // else
                   // {
                       forwarder.Text = cr.getShippingData(Command.getForwarder, soList.ElementAt(0).KUNNR2);
                  //  }
                   

                    Debug.WriteLine(soList.ElementAt(0).KUNNR2);
                    Debug.WriteLine(forwarder.Text);

                    if (custRepo.getCustomerNamebyCode(soList.ElementAt(0).KUNNR1).Contains("THALES"))
                    {
                        // forwarder.Text = cr.getShippingData(Command.getForwarder, soList.ElementAt(0).KUNNR1);
                        Label5.Text = custRepo.GetCustomerByCustomerCodeMaterialGroup(1, soList.ElementAt(0).KUNNR2, "data").ACCOUNTNO;
                    }
                    else
                    {
                        Label5.Text = customer.ACCOUNTNO;
                    }
                
                        
                    if (customer.BILLTO != null)
                    {
                        if (customer.BILLTO.Contains("Bill to recipient"))
                        {
                            CheckBox1.Checked = true;
                        }

                        if (customer.BILLTO.Contains("Bill to 3rd Party"))
                        {
                            CheckBox2.Checked = true;
                        }

                        if (customer.BILLTO.Contains("Bill to Account"))
                        {
                            CheckBox3.Checked = true;
                        }

                    }


                    if (customer.SERVICETYPE != null)
                    {
                        if (customer.SERVICETYPE.Contains("Normal"))
                        {
                            CheckBox16.Checked = true;
                        }

                        if (customer.SERVICETYPE.Contains("Express"))
                        {
                            CheckBox17.Checked = true;
                        }


                    }
                }



                //try
                //{
                //    shipdate.Text = DateTime.ParseExact(soList.ElementAt(0).EDATU, "yyyyMMdd", CultureInfo.InvariantCulture).ToShortDateString();
                //}
                //catch(Exception dateEx)
                //{
                //    shipdate.Text = DateTime.ParseExact(soList.ElementAt(0).EDATU, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToShortDateString();
                //}

                Debug.WriteLine(soList.ElementAt(0).SHIPDATE);
                shipdate.Text = soList.ElementAt(0).SHIPDATE;
                RSNNO.Text = soList.ElementAt(0).RSNNO;
                issueDateLbl.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //rsnBarCode.Attributes.Add("OnLoad", "JsBarcode('#" + rsnBarCode.ClientID + "','" + soList.ElementAt(0).RSNNO + "');");

                billtoname.Text = cr.getShippingData(Command.getCustName, soList.ElementAt(0).KUNNR1);
                addressA1Lbl.Text = cr.getShippingData(Command.getAddress1, soList.ElementAt(0).KUNNR1);
                addressA2Lbl.Text = cr.getShippingData(Command.getAddress2, soList.ElementAt(0).KUNNR1);
                poscodeAlbl.Text = cr.getShippingData(Command.getPoscode, soList.ElementAt(0).KUNNR1);
                regionAlbl.Text = cr.getShippingData(Command.getRegion, soList.ElementAt(0).KUNNR1);
                countryAlbl.Text = cr.getShippingData(Command.getcountry, soList.ElementAt(0).KUNNR1);

                System.Diagnostics.Debug.WriteLine(soList.ElementAt(0).KUNNR2);
                shipToName.Text = cr.getShippingData(Command.getCustName, soList.ElementAt(0).KUNNR2);
                addressB1Lbl.Text = cr.getShippingData(Command.getAddress1, soList.ElementAt(0).KUNNR2);
                addressB2Lbl.Text = cr.getShippingData(Command.getAddress2, soList.ElementAt(0).KUNNR2);
                poscodeBlbl.Text = cr.getShippingData(Command.getPoscode, soList.ElementAt(0).KUNNR2);
                regionBlbl.Text = cr.getShippingData(Command.getRegion, soList.ElementAt(0).KUNNR2);
                countryBlbl.Text = cr.getShippingData(Command.getcountry, soList.ElementAt(0).KUNNR2);

                telNoLbl.Text = cr.getContactNo(soList.ElementAt(0).KUNNR2);

                if (soList.ElementAt(0).RSNNO.Contains("2180704436") || soList.ElementAt(0).RSNNO.Contains("2180704515") || soList.ElementAt(0).RSNNO.Contains("2180704516"))
                    CheckBox5.Checked = true;
                else
                    chargesChk.Checked = true;

                try
                {
                    UserRepository usr = new UserRepository();
                User user = usr.getUserData(soList.ElementAt(0).CREATEDBYID); //user who generated rsn

                requestLbl.Text = user.FULLNAME;
               // user = usr.getUserData(user.SUPERVISOR);
                approvedLbl.Text = new UserRepository().getUserData(user.SUPERVISOR).FULLNAME;

                }
                catch(Exception ex)
                {

                }
               

                if(soList.ElementAt(0).EXPORTCONTROL.Equals("Y"))
                {
                    gtsimage1.Visible = true;
                       exportControlCheckYes.Checked = true;
                }
                    
                else
                {
                    gtsimage1.Visible = false;
                    exportControlCheckNo.Checked = true;
                }

                if (soList.ElementAt(0).AUART != null)
                {
                    if (soList.ElementAt(0).AUART.Equals("ZYRM"))
                        rmaYes.Checked = true;
                    else
                        rmaNo.Checked = false;
                }

                else
                {
                    rmaNo.Checked = false;

                }

                rtvrNo.Checked = true;
               // ChkBox_fob.Checked = true;

                var RSNBar = new HtmlGenericControl("svg");
                RSNBar.ID = "rsnID";
                RSNBar.Attributes.Add("OnLoad", "JsBarcode('#rsnID','" + soList.ElementAt(0).RSNNO + "',{height:35});");
                rsnBarCell.Controls.Add(RSNBar);
                //rsnBarCell.Attributes.Add("style", "height:70px");


                int totalAllQty = 0;
                double totalAllPrice = 0;

                foreach (AutoRSN.Models.Openso model in soList)
                     {

                        HtmlTableRow tRow = new HtmlTableRow();
                         tRow.  Align = "center";
                        string poID = RandomString(4) + RandomString(4);
                        string soID = RandomString(4) + RandomString(4);
                        string clsID = RandomString(4) + RandomString(4);
                        string descID = RandomString(4) + RandomString(4);
                        string countryID = RandomString(4) + RandomString(4);
                        string fromLocationID = RandomString(4) + RandomString(4);
                        string bomPNID = RandomString(4) + RandomString(4);
                        string qtyID = RandomString(4) + RandomString(4);


                         HtmlTableCell poCell = new HtmlTableCell();
                         HtmlTableCell soCell = new HtmlTableCell();
                         HtmlTableCell clsCell = new HtmlTableCell();
                          HtmlTableCell descCell = new HtmlTableCell();
                          HtmlTableCell countryCell = new HtmlTableCell();
                          HtmlTableCell fromLocationCell = new HtmlTableCell();
                         HtmlTableCell bomPNCell = new HtmlTableCell();
                         HtmlTableCell qtyCell = new HtmlTableCell();
                         HtmlTableCell uomCell = new HtmlTableCell();
                         HtmlTableCell unitPriceCell = new HtmlTableCell();
                         HtmlTableCell amountCell = new HtmlTableCell();
                         HtmlTableCell currencyCell = new HtmlTableCell();
                         HtmlTableCell revCell = new HtmlTableCell();

                   
                    var poBar = new HtmlGenericControl("svg");
                    var poTxt = new HtmlGenericControl("span");
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
                    poBar.Attributes.Add("OnLoad", "JsBarcode('#" + poID + "','" + model.BSTNK + "',{height:35,displayValue: false,marginLeft: 25,marginRight:25});");
                    poBar.Attributes.Add("style", "height:70px");
                    poTxt.InnerText = model.BSTNK + " / " + model.POSEX;
                    so.InnerText = model.VBELN + "/ " + model.POSNR;

                    clsBar.ID = clsID;
                    System.Diagnostics.Debug.WriteLine(model.KDMAT);
                    clsBar.Attributes.Add("OnLoad", "JsBarcode('#" + clsID + "','" + model.KDMAT + "',{height:35});");

                    clsBar.Attributes.Add("style", "height:70px");
                    poCell.Controls.Add(poBar);
                    poCell.Controls.Add(new LiteralControl("<br/>"));
                    poTxt.Attributes.Add("style", "font-size: 17px;font-weight: bold;");
                    poCell.Controls.Add(poTxt);

                    soCell.Controls.Add(so);
                    clsCell.Controls.Add(clsBar);
                    desc.InnerText = model.ARKTX;
                    descCell.Controls.Add(desc);
                    country.InnerText = model.CCO;
                    countryCell.Controls.Add(country);
                    fromLocation.InnerText = model.STORAGELOC;
                    fromLocationCell.Controls.Add(fromLocation);
                    bomPN.InnerText = model.MATNR;
                    bomPNCell.Controls.Add(bomPN);
                    qtyPN.InnerText = model.SHIPQTY.ToString();
                    qtyCell.Controls.Add(qtyPN);
                    uom.InnerText = "EA";
                    uomCell.Controls.Add(uom);
                    Debug.WriteLine(model.NETPR);
                    double  unitpr = Convert.ToDouble(model.NETPR);

                    if (unitpr.ToString().Trim().Contains('.'))
                    {
                        string[] netprArr = unitpr.ToString().Trim().Split('.');
                        if (netprArr[1].Length == 1)
                        {
                            unitPrice.InnerText = string.Format("{0:0.00}", Math.Round(unitpr, 2, MidpointRounding.AwayFromZero));

                        }
                        else if (netprArr[1].Length > 1)
                        {

                            unitPrice.InnerText = unitpr.ToString();
                        }


                    }

                    else
                    {
                        unitPrice.InnerText = string.Format("{0:0.00}", Math.Round(unitpr, 2, MidpointRounding.AwayFromZero));


                    }
                   
                       // if (unitpr.ToString().Contains('.'))
                       // unitPrice.InnerText = string.Format("{0:0.00}",Math.Round(unitpr, 2,MidpointRounding.AwayFromZero));
                    //else
                       // unitPrice.InnerText = string.Format("{0:0.00}", Math.Round(unitpr, 2, MidpointRounding.AwayFromZero));
                    //   Debug.WriteLine(Math.Round(unitpr, 2, MidpointRounding.AwayFromZero));
                    unitPriceCell.Controls.Add(unitPrice);
                    //amount.InnerText = string.Format("{0:0.00}", Math.Round(unitpr * Convert.ToInt32(model.SHIPQTY), 2, MidpointRounding.AwayFromZero));

                    double amountPrice = unitpr * Convert.ToInt32(model.SHIPQTY); 
                    if (amountPrice.ToString().Trim().Contains('.'))
                    {
                        string[] amountArr = amountPrice.ToString().Trim().Split('.');
                        if (amountArr[1].Length == 1)
                        {
                            amount.InnerText = string.Format("{0:0.00}", Math.Round(amountPrice, 2, MidpointRounding.AwayFromZero));

                        }
                        else if (amountArr[1].Length > 1)
                        {

                            amount.InnerText = amountPrice.ToString();
                        }


                    }

                    else
                    {
                        amount.InnerText = string.Format("{0:0.00}", Math.Round(amountPrice, 2, MidpointRounding.AwayFromZero));


                    }

                    // Debug.WriteLine(Math.Round(unitpr * Convert.ToInt32(model.SHIPQTY), 2, MidpointRounding.AwayFromZero));
                    amountCell.Controls.Add(amount);
                    currency.InnerText = model.CURRENCY;
                    currencyCell.Controls.Add(currency);

                    tRow.Height = "35px";
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

                    ItemID.Rows.Insert(row, tRow);

                    totalAllQty += model.SHIPQTY;
                    totalAllPrice += amountPrice;
                }

                Label2.Text = totalAllQty.ToString();
                // Label3.Text = "$ " + string.Format("{0:0.00}", totalAllPrice);

                string currencyLbl = "";

                if (soList.ElementAt(0).CURRENCY.Equals("USD"))
                    currencyLbl = "$ ";
                else if (soList.ElementAt(0).CURRENCY.Equals("MYR"))
                    currencyLbl = "RM ";

                if (totalAllPrice.ToString().Trim().Contains('.'))
                {
                    string[] totalAllPriceArr = totalAllPrice.ToString().Trim().Split('.'); 

                    if (totalAllPriceArr[1].Length == 1)
                    {
                        Label3.Text = currencyLbl + string.Format("{0:0.00}", Math.Round(totalAllPrice, 2, MidpointRounding.AwayFromZero));

                    }
                    else if (totalAllPriceArr[1].Length > 1)
                    {

                        Label3.Text = currencyLbl + totalAllPrice.ToString();
                    }


                }

                else
                {
                    Label3.Text = currencyLbl + string.Format("{0:0.00}", Math.Round(totalAllPrice, 2, MidpointRounding.AwayFromZero));


                }


            }
        }


        protected override void Render(HtmlTextWriter writer)
        {
            //*******Original**************************
            // *** Write the HTML into this string builder
            //StringBuilder sb = new StringBuilder();
            //StringWriter sw = new StringWriter(sb);

            //HtmlTextWriter hWriter = new HtmlTextWriter(sw);
            //base.Render(hWriter);

            //// *** store to a string
            //string PageResult = sb.ToString(); //PageResult contains the HTML
            //                                   //System.Diagnostics.Debug.WriteLine(PageResult);

            //string reportFileName = RandomString(4) + RandomString(4) + ".htm";
            //string generatedReportPath = Server.MapPath("~") + "\\" + "GeneratedReport\\" + reportFileName;

            ////System.IO.File.WriteAllText(generatedReportPath, PageResult);

            //OpensoRepository.updateReportID(Convert.ToInt32(Session["SERVERID"].ToString().ToUpper()),reportFileName, Request.QueryString["RSNNO"]);

            ////mailService.sendMail(soList, reportFileName);
            //// *** Write it back to the server
            //writer.Write(PageResult);
            //string url = HttpContext.Current.Request.Url.AbsoluteUri;
            //string path = HttpContext.Current.Request.Url.AbsolutePath;
            //string host = HttpContext.Current.Request.Url.Host;
            //int port = HttpContext.Current.Request.Url.Port;
            //CreatePDFDocument(sb.ToString());

            //**********************************************

            MemoryStream mem = new MemoryStream();
            StreamWriter twr = new StreamWriter(mem);
            HtmlTextWriter myWriter = new HtmlTextWriter(twr);
            base.Render(myWriter);
            myWriter.Flush();
            myWriter.Dispose();
            StreamReader strmRdr = new StreamReader(mem);
            strmRdr.BaseStream.Position = 0;
            string pageContent = strmRdr.ReadToEnd();
            strmRdr.Dispose();
            mem.Dispose();
            writer.Write(pageContent);
          //  CreatePDFDocument(pageContent);


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

        public  void CreatePDFDocument(string strHtml)
    {

            string host = HttpContext.Current.Request.Url.Host;
            int port = HttpContext.Current.Request.Url.Port;


            string strFileName = Server.MapPath(@"\test.pdf");
        Debug.WriteLine("http://" + host + "/AutoRSN/Resources/customLogo.png");
        // step 1: creation of a document-object
        Document document = new Document();
        // step 2:
        // we create a writer that listens to the document
        PdfWriter.GetInstance(document, new FileStream(strFileName, FileMode.Create));
        StringReader se = new StringReader(strHtml);
        HTMLWorker obj = new HTMLWorker(document);
        document.Open();
        obj.Parse(se);
        document.Close();
        ShowPdf(strFileName);
     
   
     
    }
    public void ShowPdf(string strFileName)
    {
        Response.ClearContent();
        Response.ClearHeaders();
        Response.AddHeader("Content-Disposition", "inline;filename=" + strFileName);
        Response.ContentType = "application/pdf";
        Response.WriteFile(strFileName);
        Response.Flush();
        Response.Clear();
    }

    }
}