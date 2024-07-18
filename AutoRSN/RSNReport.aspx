<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RSNReport.aspx.cs" Inherits="AutoRSN.RSNReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://drive.google.com/uc?export=download&id=0B0_0xQQQZDQnS1IxeGVuUWZULWs" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <style type="text/css">

        .myHeight15 {
            height: 15px;
        }
        .myHeight15 th{
            height: 15px;
            padding:0px;
        }
        .myHeight10 {
            height: 10px;
        }
        .myHeight10 td{
            height: 10px;
            padding:0px;
        }

        .auto-style16 {
            height: 32px;
            width: 252px;
        }
        
        .no-top-border{
            border-top-style:none;
        }
        .no-bottom-border{
            border-bottom-style:none;
        }

        .no-right-border{
            border-right-style:none;
        }

        .no-left-border{
            border-left-style:none;
        }
        .align-center{
            text-align:center;

        }

        .bold-text{
            font-weight:bold;
        }
        .grey-text{
            color:gray;
        }

        .right {
             position: absolute;
             right: 0px;
             width: 300px;
             border: 3px solid #73AD21;
             padding: 10px;
            }
        td{

            height:40px;
        }

        .highlight{

            background-color:yellow;
        }

        .tableHeight{
            height:10%;
        }
        .rightFloat{
            float:right;
        }

    </style>
</head>
<body>
   <form id="form1" runat="server">
    <div>

      
<asp:Image ID="logo" runat="server" src="http://localhost:61357/Resources/logo.PNG" class="auto-style16" />  &nbsp;
            <asp:Table CssClass="rightFloat" runat="server" Width="515px" GridLines="Both" Height="16px"> 
            <asp:TableRow>
                <asp:TableCell CssClass="bold-text">
                      1. RSN NO:  
                </asp:TableCell>
                 <asp:TableCell CssClass="bold-text">
                    <asp:Label ID="RSNNO" runat="server"> [RSNNO] </asp:Label>
                </asp:TableCell>
                 <asp:TableCell CssClass="bold-text">
                    2. ISSUE DATE:
                </asp:TableCell>
                <asp:TableCell CssClass="bold-text">
                    <asp:Label ID="issueDateLbl" runat="server">[issue date]</asp:Label>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell ColumnSpan="4" Font-Size="Smaller" CssClass="grey-text">
                    This RSN  will be VOIDED if not submitted to Shipping within 1 week the RSN issuance date 
                </asp:TableCell>

            </asp:TableRow>

        </asp:Table>

      
           
        
          <br/> <asp:Label runat="server" CssClass="bold-text">& 16 , Jalan Hi-Tech 2/3 Phase 1,Kulim, Hi-Tech Park,09000 Kulim , Kedah , Malaysia.</asp:Label> <br />
    </asp> 
        <br/>chargesChk
        <br />
        <span style="font-weight:bold">REQUEST SHIPMENT NOTICE &ensp; &ensp; &ensp; &ensp; &ensp; Attn to Shipping /Logistics </span>
         <asp:Table  BorderWidth="1" runat="Server" GridLines="Both" Width="1329px" Height="153px"> 

             <asp:TableRow runat="server">
                 <asp:TableCell CssClass="bold-text">3) FORWARDER NAME :  <asp:Label runat="server" ID="forwarderLbl" > [CONTENT] </asp:Label><br />
                                    ( Pls fill-in if the forwarder is appointed by consignee or 3rd Party ) </asp:TableCell>

                  <asp:TableCell CssClass="bold-text"> 5) SHIP DATE    :  <asp:Label ID="shipDateLbl" runat="server" ></asp:Label></asp:TableCell>

                 <asp:TableCell RowSpan="3"><asp:Label runat="server" ID="rsnBarCode">[rsn bar code]</asp:Label></asp:TableCell>
             </asp:TableRow>

               <asp:TableRow runat="server">
                 <asp:TableCell CssClass="bold-text">4) SHIP MODE (Air/Sea/Land/<span style ="color:antiquewhite;">Courier</span> /Handcarry) : <asp:Label runat="server"> [CONTENT] </asp:Label> <br/>
                                FOR COURIER SERVICE NOT PAYABLE BY CELESTICA : -  </asp:TableCell>
                 
                   <asp:TableCell CssClass="bold-text">6) PAYMENT TERMS  :  Per LTC</asp:TableCell>
                  

             </asp:TableRow>

               <asp:TableRow runat="server">
                 <asp:TableCell><asp:CheckBox runat="server" ID="receiptChk" /><span>Bill to recipeint</span> &ensp;&ensp;&ensp; <asp:CheckBox runat="server" /><span> Bill to 3rd Party</span> &ensp;&ensp;&ensp;<asp:CheckBox runat="server" /><span>Bill To Account#  </span> </asp:TableCell>
                  
                     <asp:TableCell CssClass="bold-text">  7)  REQUIRED ETA  : <asp:Label ID="etaLbl" runat="server" >[system date + 2]</asp:Label> <br/> ( if  required , Pls check with shipping the transit time )  </asp:TableCell>
              
                    </asp:TableRow>
         </asp:Table>

        <asp:Table runat="server" GridLines="Both" Width="1036px">
              <asp:TableRow> 
                  <asp:TableCell VerticalAlign="Top">8 & 9 ) THE GOODS &ensp; </br> <asp:CheckBox runat="server" ID="chargesChk" /> CHARGES  <br /> <asp:CheckBox runat="server" ID="noChargesChk" />NO CHARGES </asp:TableCell>
                  <asp:TableCell VerticalAlign="Top"> BILL TO PARTY ( NAME & ADDRESS) </br> <asp:Label runat="server" ID="billToName"> bill name</asp:Label> <br/> <asp:Label runat="server" ID="addressA1Lbl">[addressA1]</asp:Label> <br /><asp:Label runat="server" ID="addressA2Lbl">[addressA2]</asp:Label> </asp:TableCell>
                    <asp:TableCell VerticalAlign="Top"> 10) SHIP TO PARTY ( NAME & ADDRESS)  </br> <asp:Label runat="server" ID="shipToName"> SHIP TO name</asp:Label> <br/> <asp:Label runat="server" ID="addressB1Lbl">[addressB1]</asp:Label> <br /><asp:Label runat="server" ID="addressB2Lbl">[addressB2]</asp:Label> </br> 11) ATTN TO PERSON /TEL#  <asp:Label runat="server" ID="telNoLbl">[tel no]</asp:Label> </asp:TableCell>
                <asp:TableCell VerticalAlign="Top"> 12)  NOTIFY PARTY /BROKER AT IMPORT POINT </asp:TableCell>
              </asp:TableRow>
              <asp:TableRow> </asp:TableRow>
              <asp:TableRow> </asp:TableRow>
        </asp:Table>

        <br />

        <asp:Table ID="mainTable" Font-Size="Smaller" runat="Server" Width="1909px" GridLines="Both" CssClass="align-center myHeight10">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell Width ="30px">PO#</asp:TableHeaderCell>
                <asp:TableHeaderCell>SO# / Line#</asp:TableHeaderCell>
                <asp:TableHeaderCell>CUSTOMER PART NO</asp:TableHeaderCell>
                 <asp:TableHeaderCell>DESCRIPTION</asp:TableHeaderCell>
                 <asp:TableHeaderCell>COUNTRY OF ORIGIN</asp:TableHeaderCell>
                 <asp:TableHeaderCell>FROM LOCATION</asp:TableHeaderCell>
                 <asp:TableHeaderCell>IN-HOUSE BOM P/N</asp:TableHeaderCell>
                 <asp:TableHeaderCell>QTY SHIP</asp:TableHeaderCell>
                 <asp:TableHeaderCell>UOM</asp:TableHeaderCell>
                 <asp:TableHeaderCell>UNIT PRICE</asp:TableHeaderCell>
                 <asp:TableHeaderCell>AMOUNT</asp:TableHeaderCell>
                 <asp:TableHeaderCell>CURRENCY</asp:TableHeaderCell>
                 <asp:TableHeaderCell>Rev</asp:TableHeaderCell>
        
            </asp:TableHeaderRow>

      

            <asp:TableRow CssClass="no-bottom-border no-left-border no-right-border no-top-border">
                <asp:TableCell Width="20px" HorizontalAlign="Left" ColumnSpan="4"  BorderStyle="None" BorderWidth="0">
                    26) FREIGHT TERMS 

                </asp:TableCell>
                <asp:TableCell Width="20px" CssClass="bold-text" ColumnSpan ="2" RowSpan="11" VerticalAlign="Top" HorizontalAlign="Left">
                    27) For RTV ( Return to Vendor) <br/> RTVR &nbsp; YES<asp:CheckBox runat="server" /> &nbsp;   NO<asp:CheckBox runat="server" />
                    (if Yes, &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;)
                    <br /> <br /><br /><br />RMA# &nbsp YES<asp:CheckBox runat="server" />  &nbsp NO<asp:CheckBox runat="server" />  
                    (if Yes, RMA)
                    <br /> <br /><br /><br /> <br /><br /><br /> <br /><br /><br /> Any other reference# :
                </asp:TableCell>
                
               <asp:TableCell Width="20px" CssClass="bold-text highlight" ColumnSpan="8" HorizontalAlign="Left">TOTAL :&emsp;&emsp;&emsp;&emsp; [value] &emsp;&emsp;&emsp;&emsp; [value] </asp:TableCell>
                </asp:TableRow>

            <asp:TableRow>
                 <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="no-bottom-border no-left-border no-right-border no-top-border" >
                         FREIGHT COLLECT
                </asp:TableCell>
                <asp:TableCell ColumnSpan="2" HorizontalAlign="Left"  CssClass="no-bottom-border no-left-border no-right-border no-top-border" >
                          FREIGHT PREPAID
                </asp:TableCell>
                 
                <asp:TableCell>PALLET#/CARTON </asp:TableCell>
                <asp:TableCell ColumnSpan="2">DIMENSION (cm) /Volume </asp:TableCell>
                <asp:TableCell>Nett Weight (Kg ) </asp:TableCell>
                 <asp:TableCell ColumnSpan="4">#value! </asp:TableCell>     
            </asp:TableRow>

            <asp:TableRow>
                 <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="no-bottom-border no-left-border no-right-border no-top-border">
                        <asp:CheckBox runat="server" />  EX-FACTORY
                </asp:TableCell>
                <asp:TableCell ColumnSpan="2" HorizontalAlign="Left"  CssClass="no-bottom-border no-left-border no-right-border no-top-border">
                       <asp:CheckBox runat="server" />   DOOR TO  DOOR 
                </asp:TableCell>
                 <asp:TableCell></asp:TableCell>
                 <asp:TableCell></asp:TableCell>
                 <asp:TableCell></asp:TableCell>
                 <asp:TableCell></asp:TableCell>
                 <asp:TableCell></asp:TableCell>
                 <asp:TableCell></asp:TableCell>
                 <asp:TableCell></asp:TableCell>
            </asp:TableRow>

              <asp:TableRow>
                 <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="no-bottom-border no-left-border no-right-border no-top-border">
                        <asp:CheckBox runat="server" /> OB  / Free Carrier  (FCA) at origin
                </asp:TableCell>
                <asp:TableCell ColumnSpan="2"  HorizontalAlign="Left" CssClass="no-bottom-border no-left-border no-right-border no-top-border">
                       <asp:CheckBox runat="server" />   DOOR TO PORT DESTINATION  
                </asp:TableCell>
                 <asp:TableCell></asp:TableCell>
                 <asp:TableCell></asp:TableCell>
                 <asp:TableCell></asp:TableCell>
                 <asp:TableCell></asp:TableCell>
                 <asp:TableCell></asp:TableCell>
                 <asp:TableCell></asp:TableCell>
                 <asp:TableCell></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow CssClass="myHeight10">
                <asp:TableCell HorizontalAlign ="Left" VerticalAlign="Top" ColumnSpan="4"> 28) REMARKS /SPECIAL INSTRUCTION </asp:TableCell>
                      <asp:TableCell></asp:TableCell>
                 <asp:TableCell></asp:TableCell>
                 <asp:TableCell></asp:TableCell>
                 <asp:TableCell></asp:TableCell>
                 <asp:TableCell></asp:TableCell>
                 <asp:TableCell></asp:TableCell>
                 <asp:TableCell></asp:TableCell>
            </asp:TableRow>
             <asp:TableRow>
                <asp:TableCell ColumnSpan="4">  </asp:TableCell>
                      <asp:TableCell></asp:TableCell>
                 <asp:TableCell></asp:TableCell>
                 <asp:TableCell></asp:TableCell>
                 <asp:TableCell></asp:TableCell>
                 <asp:TableCell></asp:TableCell>
                 <asp:TableCell></asp:TableCell>
                 <asp:TableCell></asp:TableCell>
            </asp:TableRow>
             <asp:TableRow>
                <asp:TableCell ColumnSpan="4"> </asp:TableCell>
                  <asp:TableCell RowSpan="2"></asp:TableCell>
                 <asp:TableCell RowSpan="2"></asp:TableCell>
                 <asp:TableCell RowSpan="2"></asp:TableCell>
                 <asp:TableCell RowSpan="2"></asp:TableCell>
                 <asp:TableCell RowSpan="2"></asp:TableCell>
                 <asp:TableCell RowSpan="2"></asp:TableCell>
                 <asp:TableCell RowSpan="2"></asp:TableCell>
            </asp:TableRow>

             <asp:TableRow>
                <asp:TableCell ColumnSpan="4"> </asp:TableCell>
                
            </asp:TableRow>

             <asp:TableRow>
                <asp:TableCell ColumnSpan="4"> </asp:TableCell>
                 <asp:TableCell RowSpan="2"></asp:TableCell>
                 <asp:TableCell RowSpan="2"></asp:TableCell>
                 <asp:TableCell RowSpan="2"></asp:TableCell>
                 <asp:TableCell RowSpan="2"></asp:TableCell>
                 <asp:TableCell RowSpan="2"></asp:TableCell>
                 <asp:TableCell RowSpan="2"></asp:TableCell>
                 <asp:TableCell RowSpan="2"></asp:TableCell>
            </asp:TableRow>
             <asp:TableRow>
                <asp:TableCell ColumnSpan="2" CssClass="no-right-border no-bottom-border tableHeight"> 29) EXPORT CONTROL GOODS :    </asp:TableCell>
                 <asp:TableCell ColumnSpan="2" CssClass="no-left-border no-bottom-border">   <asp:CheckBox ID="exportControlCheckYes" runat="server"/> YES  </asp:TableCell>
               
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" CssClass="no-right-border no-top-border">> ( Please tick √ )  </asp:TableCell>
                <asp:TableCell ColumnSpan="2" CssClass="no-left-border no-top-border"> <asp:CheckBox ID="exportControlCheckNo" runat="server"/> NO </asp:TableCell>

                  <asp:TableCell Height="100" HorizontalAlign="Left" VerticalAlign="Top">Total Packages ( Pcs) </asp:TableCell>
                 <asp:TableCell Height="100" ColumnSpan="2" HorizontalAlign="Left" VerticalAlign="Top">Total Vol Weight (KG)</asp:TableCell>
              
                 <asp:TableCell Height="100" HorizontalAlign="Left" VerticalAlign="Top">TOTAL  NETT ( KG ) </asp:TableCell>
                 <asp:TableCell Height="100" ColumnSpan="3" HorizontalAlign="Left" VerticalAlign="Top">TOTAL  GROSS ( KG ) </asp:TableCell>
         
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell ColumnSpan="3" Height="100" HorizontalAlign="Left" VerticalAlign="Top">
                    30)  REQUESTED BY : <br /> 
                    &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<asp:Image runat="server" />
                </asp:TableCell>   <%-- signature 1--%>

                <asp:TableCell ColumnSpan="3" RowSpan="2" HorizontalAlign="Left" VerticalAlign="Top">  
                    32)  SERVICE TYPE : &emsp;&emsp;&emsp;  NORMAL <asp:CheckBox runat="server" /> &emsp;&emsp;&emsp;  EXPRESS <asp:CheckBox runat="server" />
                    <br />  33)   IF EXPRESS PAID BY CMY <br /><br />  REASON : __________
                    <br /> APPROVED BY :__________________________________ ( Manager's name & signature) 
                </asp:TableCell >  <%-- SERVICE TYPE--%>

                <asp:TableCell CssClass="bold-text" ColumnSpan="7" RowSpan="2" HorizontalAlign="Left" VerticalAlign="Top">
                   FOR  LOGISTICS INTERNAL USE :      
                    <br/> RSN received Date/Time
                    <br/> Invoice#:&emsp;&emsp;&emsp;&emsp;&emsp; &emsp; &emsp; &emsp; &emsp;  Processed by
                    <br/><span class="highlight">Remarks: Strategic Trade Act / Dangerous Good Confirmations :
                    <br /> (Please tick √) &emsp;&emsp;&emsp;&emsp;&emsp; STA <asp:CheckBox runat="server" /> &emsp;&emsp;&emsp;&emsp;&emsp; DGR  <asp:CheckBox runat="server" /> </span>
                </asp:TableCell>
            </asp:TableRow>
              <asp:TableRow>
                <asp:TableCell ColumnSpan="3" Height="100" HorizontalAlign="Left" VerticalAlign="Top">
                    31) APPROVED BY :  <br /><br />  
                    &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<asp:Image runat="server" />
                   
                </asp:TableCell>   <%-- signature 2--%>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell ColumnSpan="13"  HorizontalAlign="Left" VerticalAlign="Top" Font-Size="10" ForeColor="Red">
                     TAKE NOTE 
                    <br/>It is the responsible of RSN requestor to ensure the above information provided by him/her are correct or accurate,   this is important to ensure smooth shipment processing 
                    <br/> /customs clearance & delivery  to destination/consignee 
                    <br />DHANA ( 3831 ) S/Dial*73432
                </asp:TableCell>

            </asp:TableRow>
        </asp:Table>



           <br />

        
    
    </div>


    </form>
</body>
</html>
