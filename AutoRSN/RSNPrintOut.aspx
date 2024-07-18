<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RSNPrintOut.aspx.cs" Inherits="AutoRSN.RSNPrintOut" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script src="https://drive.google.com/uc?export=download&id=0B0_0xQQQZDQnS1IxeGVuUWZULWs" />
     <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <style type="text/css">

     table {
    border-collapse: collapse;
}

table, td, th {
    border: 1px solid black;
}

        .auto-style1 {
            font-size: small;
            width: 1544px;
            height: 750px;
        }
        .auto-style3 {
            width: 39%;
            height: 35px;
            font-size: small;
        }
        .auto-style11 {
            height: 19px;
        }
        .auto-style17 {
            width: 119px;
        }
        .auto-style19 {
            color: #003300;
        }
        .auto-style21 {
            width: 123px;
        }
        .auto-style23 {
            width: 119px;
            height: 8px;
        }
        .auto-style25 {
            width: 123px;
            height: 8px;
        }
        .auto-style32 {
            width: 134px;
            height: 8px;
        }
        .auto-style33 {
            width: 134px;
        }
        .auto-style34 {
            width: 191px;
        }
        .auto-style35 {
            width: 191px;
            height: 8px;
        }
        .auto-style36 {
            width: 263px;
        }
        .auto-style37 {
            width: 192px;
        }
        .auto-style38 {
            height: 21px;
        }
        .auto-style44 {
            width: 328px;
            height: 21px;
        }
        .auto-style50 {
            width: 132px;
        }
        .auto-style51 {
            width: 132px;
            height: 8px;
        }
        .auto-style52 {
            width: 474px;
            height: 21px;
        }
        .auto-style54 {
            text-align: center;
        }
        .auto-style57 {
            text-align: center;
            width: 168px;
            height: 26px;
        }
        .auto-style61 {
            text-align: center;
            width: 102px;
            height: 26px;
        }
        .auto-style67 {
            color: #FF0000;
        }
        .auto-style69 {
            text-decoration: underline;
        }
        .auto-style91 {
            text-align: center;
            width: 136px;
            height: 26px;
        }
        .auto-style103 {
            width: 100%;
            font-size: small;
        }
        .auto-style104 {
            width: 237px;
        }
        .auto-style105 {
            width: 237px;
            height: 8px;
        }
        .auto-style106 {
            height: 21px;
            color: #003300;
        }
        .auto-style107 {
            width: 328px;
            height: 18px;
        }
        .auto-style108 {
            width: 474px;
            height: 18px;
        }
        .auto-style109 {
            height: 18px;
        }
        .auto-style111 {
            background-color: #FFFF66;
        }
        .auto-style112 {
            text-align: center;
            background-color: #FFFF66;
        }
        .auto-style118 {
            text-align: center;
            height: 35px;
        }
        .auto-style120 {
            width: 328px;
            height: 17px;
        }
        .auto-style121 {
            width: 474px;
            height: 17px;
        }
        .auto-style122 {
            height: 17px;
        }
        .auto-style123 {
            width: 72%;
        }
        .auto-style125 {
            text-decoration: underline;
            width: 239px;
            text-align: left;
        }
        .auto-style126 {
            width: 239px;
            height: 18px;
            text-align: left;
            text-decoration: underline;
        }
        .auto-style127 {
            text-align: left;
        }
        .auto-style128 {
            height: 18px;
            text-align: left;
        }
        .auto-style129 {
            text-align: left;
            height: 101px;
        }
        .auto-style132 {
            text-align: center;
            height: 26px;
        }
        .auto-style137 {
            text-align: center;
            width: 79px;
            height: 26px;
        }
        .auto-style140 {
            text-align: center;
            width: 99px;
            height: 26px;
        }
        .auto-style143 {
            text-align: center;
            height: 8px;
        }
        .auto-style147 {
            height: 15px;
        }
        .auto-style148 {
            width: 100%;
            height: 163px;
        }
        .auto-style149 {
            height: 4px;
        }
        .auto-style150 {
            height: 27px;
        }
        .auto-style151 {
            height: 24px;
        }
        .auto-style152 {
            height: 15px;
            width: 36px;
        }
        .auto-style153 {
            width: 36px;
        }
        .auto-style154 {
            height: 24px;
            width: 36px;
        }
        .auto-style158 {
            height: 21px;
            font-size: small;
        }
        .auto-style159 {
            font-size: 9px;
        }
        .auto-style160 {
            width: 100%;
            font-size: 11px;
        }
        .auto-style161 {
            font-size: 11px;
        }
        .auto-style162 {
            font-size: small;
        }
        .auto-style163 {
            text-align: center;
            width: 97px;
            height: 26px;
        }
        .auto-style164 {
            text-align: center;
            width: 130px;
            height: 26px;
        }
        .auto-style165 {
            text-align: center;
            width: 76px;
            height: 26px;
        }
        .auto-style166 {
            text-align: center;
            width: 110px;
            height: 26px;
        }
        .auto-style167 {
            text-align: center;
            width: 103px;
            height: 26px;
        }
        .auto-style168 {
            text-align: center;
            width: 42px;
            height: 26px;
        }
        .auto-style169 {
            text-align: left;
            height: 101px;
            width: 167px;
        }
        .auto-style170 {
            text-align: center;
            width: 167px;
            height: 26px;
        }
    </style>
</head>
<body style="margin: 0px; padding:0px;">
    <script src="https://code.jquery.com/jquery-3.3.1.min.js"></script>
    <script src="Scripts/JsBarcode.all.min.js"></script>
    <script>    
            
        $(document).ready(function () {
           
            window.print();
        
        });

    </script>
    <form id="form1" runat="server">
    <div class="auto-style1">

       <asp:Image runat="server" ImageUrl="~/Resources/customLogo.png" id="logo" class="auto-style2" Width="142px" Height="48px" />
        <table align="right" border="1" class="auto-style3">
            <tr>
                <td class="auto-style11"><strong>1. RSN NO:&nbsp;&nbsp;<asp:Label ID="RSNNO" runat="server" Text="Label"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 2. ISSUE DATE:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="issueDateLbl" runat="server" Text="issueDateLbl"></asp:Label>
                    </strong></td>
            </tr>
            <tr>
                <td class="auto-style106">This RSN will be VOIDED if not submitted to Shipping within 1 week the RSN issuance date </td>
            </tr>
        </table>
        <span class="auto-style162">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong>
        <br />
        15 &amp; 16 , Jalan Hi-Tech 2/3 Phase 1,Kulim, Hi-Tech Park,09000 Kulim , Kedah , Malaysia.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </strong><asp:Image ID="gtsimage1" runat="server" Height="53px" Width="95px" ImageAlign="Middle" ImageUrl="~/Resources/GTS.PNG" BorderStyle="Dashed" />
        <br />
        </span><strong><span class="auto-style162">REQUEST SHIPMENT NOTICE&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Attn to Shipping /Logistics
        <br />
        <br />
        </span>
        <table border="0" class ="auto-style103">
            <tr>
                <td class="auto-style104"><strong>3) FORWARDER NAME : </strong></td>
                <td class="auto-style17">
                    <asp:Label ID="forwarder" runat="server" Text="Label"></asp:Label>
                </td>
                <td class="auto-style50">
                    <asp:Label ID="Label4" runat="server" Text="Account No."></asp:Label>
                </td>
                <td class="auto-style21">
                    <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
                </td>
                <td class="auto-style33"><strong>5) SHIP DATE : </strong></td>
                <td class="auto-style34">
                    <asp:Label ID="shipdate" runat="server" Text="shipdate"></asp:Label>
                </td>
                <td runat="server" id="rsnBarCell" class="auto-style36" rowspan="5">
                   
                </td>
            </tr>
            <tr>
                <td colspan="4">( Pls fill-in if the forwarder is appointed by consignee or 3rd Party ) </td>
                <td class="auto-style33"><strong>6) PAYMENT TERMS :</strong></td>
                <td class="auto-style34">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style105"><strong>4) SHIP MODE (Air/Sea/Land/Courier* /Handcarry) : </strong></td>
                <td class="auto-style23"></td>
                <td class="auto-style51"></td>
                <td class="auto-style25"></td>
                <td class="auto-style32"><strong>7) REQUIRED ETA : </strong></td>
                <td class="auto-style35"></td>
            </tr>
            <tr>
                <td class="auto-style19" colspan="4"><em>FOR COURIER SERVICE NOT PAYABLE BY CELESTICA : -</em></td>
                <td colspan="2">( if required , Pls check with shipping the transit time )</td>
            </tr>
            <tr>
                <td class="auto-style104">&nbsp;</td>
                <td class="auto-style17">
                    <asp:CheckBox ID="CheckBox1" runat="server" Text="Bill to recipient" TextAlign="Left" />
                </td>
                <td class="auto-style50">
                    <asp:CheckBox ID="CheckBox2" runat="server" Text="Bill to 3rd Party " TextAlign="Left" />
                </td>
                <td class="auto-style21">
                    <asp:CheckBox ID="CheckBox3" runat="server" Text="Bill to Account " TextAlign="Left" />
                </td>
                <td class="auto-style33">&nbsp;</td>
                <td class="auto-style34">&nbsp;</td>
            </tr>
        </table>
        </strong>
        <table border="1" class="auto-style103">
            <tr>
                <td class="auto-style37" rowspan="6"><strong><span class="auto-style162">8 &amp; 9 ) THE GOODS<br />
                    <br />
                    </span>
                    </strong>
                    <span class="auto-style161" ></span>
                    <asp:CheckBox ID="chargesChk" runat="server" Text="Charges" />
                    <br/><asp:CheckBox ID="CheckBox5" runat="server" Text="No Charges" />
                </td>
                <td class="auto-style44"><strong>BILL TO PARTY ( NAME &amp; ADDRESS)</strong><span class="auto-style159"></td>
              
                <td class="auto-style52"></span><strong>10) SHIP TO PARTY ( NAME &amp; ADDRESS) </strong></td>
                <td class="auto-style158"><strong>12) NOTIFY PARTY /BROKER AT IMPORT POINT</strong></td>
            </tr>
           
            <tr>
                <td class="auto-style107">
                    <asp:Label ID="billtoname" runat="server" Text="Label"></asp:Label>
                </td>
                <td class="auto-style108">
                    <asp:Label ID="shipToName" runat="server" Text="Label"></asp:Label>
                </td>
                <td class="auto-style109"></td>
            </tr>
            <tr>
                <td class="auto-style44">
                    <asp:Label ID="addressA1Lbl" runat="server" Text="Label"></asp:Label>
                </td>
                <td class="auto-style52">
                    <asp:Label ID="addressB1Lbl" runat="server" Text="Label"></asp:Label>
                </td>
                <td class="auto-style38"></td>
            </tr>
            <tr>
                <td class="auto-style107">
                    <asp:Label ID="addressA2Lbl" runat="server" Text="Label"></asp:Label>
                &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="poscodeAlbl" runat="server"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
                <td class="auto-style108">
                    <asp:Label ID="addressB2Lbl" runat="server" Text="Label"></asp:Label>
                &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="poscodeBlbl" runat="server"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
                <td class="auto-style109"></td>
            </tr>
            <tr>
                <td class="auto-style107">
                    <asp:Label ID="regionAlbl" runat="server"></asp:Label>
&nbsp;
                    <asp:Label ID="countryAlbl" runat="server"></asp:Label>
                </td>
                <td class="auto-style108">
                    <asp:Label ID="regionBlbl" runat="server" Text="Label"></asp:Label>
&nbsp;&nbsp;
                    <asp:Label ID="countryBlbl" runat="server" Text="Label"></asp:Label>
                </td>
                <td class="auto-style109"></td>
            </tr>
            <tr>
                <td class="auto-style120"></td>
                <td class="auto-style121"><strong>11) ATTN TO PERSON /TEL# :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </strong>
                    <asp:Label ID="telNoLbl" runat="server" Text="Label"></asp:Label>
                    </span>
                    </span></span>
                    </td>
                <td class="auto-style122"></td>
            </tr>
        </table>
        &nbsp;<table runat="server" border="1" class="auto-style160" id="ItemID">
            <tr>
                <td class="auto-style91"><span class="auto-style162">(13)<br />
                    PO#</td>
                <td class="auto-style168">(14)<br />
                    SO# / Line# </td>
                <td class="auto-style170">(15)<br />
                    CUSTOMER<br />
                    PART NO.<br />
                    (CPN )</td>
                <td class="auto-style57">(16)<br />
                    DESCRIPTION</td>
                <td class="auto-style167">(17)<br />
                    COO</td>
                <td class="auto-style163">(18)<br />
                    FROM
                    <br />
                    LOCATION</td>
                <td class="auto-style164">(19)<br />
                    IN-HOUSE<br />
                    BOM P/N</td>
                <td class="auto-style61">(20)<br />
                    QTY<br />
                    SHIP</td>
                <td class="auto-style165">(21)<br />
                    UOM</td>
                <td class="auto-style166">(22)<br />
                    UNIT
                    <br />
                    PRICE<br />
                </td>
                <td class="auto-style140">(23)<br />
                    AMOUNT</td>
                <td class="auto-style132">(24)<br />
                    CURRENCY </td>
                <td class="auto-style137">(25)<br />
                    Rev<br />
                    <span class="auto-style67">(Only For Raw Matl)</span></td>
            </tr>
            <tr>
                <td class="auto-style143" colspan="4">26) FREIGHT TERMS
                    <br />
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span class="auto-style69"><br />
                    <table align="center" class="auto-style123">
                        <tr>
                            <td class="auto-style128">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span class="auto-style69">FREIGHT COLLECT</span></td>
                            <td class="auto-style126">FREIGHT 
                    PREPAID</td>
                        </tr>
                        <tr>
                            <td class="auto-style127">
                    <asp:CheckBox ID="ChkBox_ex" runat="server" CssClass="auto-style69" Text="EX-FACTORY" />
                            </td>
                            <td class="auto-style125">
                    <asp:CheckBox ID="ChkBox_d2d" runat="server" Text="DOOR TO DOOR" />
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style127">
                    <asp:CheckBox ID="ChkBox_fob" runat="server" CssClass="auto-style69" Text="   FOB  / Free Carrier  (FCA) at origin" />
                            </td>
                            <td class="auto-style125">
                    <asp:CheckBox ID="ChkBox_d2p" runat="server" Text=" DOOR TO PORT DESTINATION " />
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style127">
                                &nbsp;</td>
                            <td class="auto-style125">
                                <asp:CheckBox ID="ChkBox_dap" runat="server" Text="DAP" />
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style127">
                                &nbsp;</td>
                            <td class="auto-style125">
                                <asp:CheckBox ID="ChkBox_ddp" runat="server" Text="DDP" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    &nbsp;</span>&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span class="auto-style69">
                    <br />
                    </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <span class="auto-style69">&nbsp;</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; <span class="auto-style69">
                    <br />
                    </span></td>
                <td class="auto-style127" colspan="2" rowspan="2" ><strong>27) For RTV ( Return to Vendor) </strong>
                    <br />
                    RTVR:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="rtvrYes" runat="server" Text="YES" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="rtvrNo" runat="server" Text="NO" />
                    <br />
                    ( if Yes ,&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; )
                    <br />
                    <br />
                    RMA#:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="rmaYes" runat="server" Text="YES" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="rmaNo" runat="server" Text="NO" />
                    <br />
                    ( if Yes , RMA )<br />
                    <br />
                    <br />
                    Any other reference#<br />
                </td>
                <td class="auto-style54" colspan="7" rowspan="2">
                    <table border="1" class="auto-style148">
                        <tr>
                            <td class="auto-style112" colspan="5">TOTAL:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>PALLET#/CARTON</td>
                            <td colspan="2">DIMENSION (cm) / Volume</td>
                            <td>Nett Weight(Kg)</td>
                            <td>#VALUE!</td>
                        </tr>
                        <tr>
                            <td class="auto-style147">
                                <br />
                            </td>
                            <td class="auto-style152"></td>
                            <td class="auto-style147">
                                <br />
                            </td>
                            <td class="auto-style147"></td>
                            <td class="auto-style147"></td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                            <td class="auto-style153">&nbsp;</td>
                            <td>
                                <br />
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                            <td class="auto-style153">&nbsp;</td>
                            <td>
                                <br />
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                            <td class="auto-style153">&nbsp;</td>
                            <td>
                                <br />
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style151"></td>
                            <td class="auto-style154"></td>
                            <td class="auto-style151">
                                <br />
                            </td>
                            <td class="auto-style151"></td>
                            <td class="auto-style151"></td>
                        </tr>
                        <tr>
                            <td class="auto-style150">TOTAL PACKAGE (Pcs)<br />
                                <br />
                            </td>
                            <td colspan="2" class="auto-style150">Total Vol Weight (Kg)<br />
                                <br />
                                <br />
                            </td>
                            <td class="auto-style150">Total Nett (Kg)<br />
                                <br />
                                <br />
                            </td>
                            <td class="auto-style150">Total Gross (Kg)<br />
                                <br />
                                <br />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="auto-style149"><strong>28) REMARKS /SPECIAL INSTRUCTION
                    <br />
                    <br />
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                   <asp:Label runat="server" ID="remarksLbl" CssClass="auto-style162">dsadwdwddw</asp:Label>
                      <br />
                    <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   <asp:Label runat="server" ID="TrixellRemark" CssClass="auto-style162">dsadwdwddw</asp:Label>
                    <br />
                      <br />
                    29) EXPORT CONTROL GOODS :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label1" runat="server" Text=" ( Please tick √ ) "></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="exportControlCheckYes" runat="server" Text="YES" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="exportControlCheckNo" runat="server" Text="NO" />
                    </strong></td>
            </tr>
            <tr>
                <td class="auto-style129" colspan="2"><strong>30) REQUESTED BY :&nbsp;
                    <br />

                    <br />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="requestLbl" runat="server"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <br />
                    <br />
                    </strong>
                    <br />
                </td>
                <td class="auto-style169"><strong>31) APPROVED BY :<br />
                    <br />
&nbsp;&nbsp;
                        <asp:Label ID="approvedLbl" runat="server"></asp:Label>
                    <br />
                    <br />
                    </strong>
                </td>
                <td class="auto-style129" colspan="3"><strong>32) <span class="auto-style162">SERVICE TYPE :
                    <asp:CheckBox ID="CheckBox16" runat="server" Text="Normal" />
&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="CheckBox17" runat="server" Text="Express" />
                    </span>
                    <br />
                    <br />
                    33) IF EXPRESS PAID BY CMY
                    <br />
                    <br />
                    REASON :_____________________<br />
                    <br />
                    APPROVED BY :____________________________<br />
                    ( Manager&#39;s name &amp; signature)
                    <br />
                    </strong> 
                </td>
                <td class="auto-style129" colspan="7"></span><strong><span class="auto-style162">FOR LOGISTICS INTERNAL USE :<br />
                    RSN received Date/Time:&nbsp;&nbsp;&nbsp;&nbsp;
                    <br />
                    Invoice# :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Processed by:<br />
                    <span class="auto-style111">Remarks: Strategic Trade Act / Dangerous Good Confirmations :(Please tick √)&nbsp;&nbsp;&nbsp;&nbsp; </span>
                    <asp:CheckBox ID="CheckBox18" runat="server" Text="STA" CssClass="auto-style111" />
                    <span class="auto-style111">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </span>
                    <asp:CheckBox ID="CheckBox19" runat="server" Text="DGR" CssClass="auto-style111" />
                    </span>
                    </strong></td>
            </tr>
            <tr>
                <td class="auto-style118" colspan="13"><span style="color: rgb(0, 0, 0); font-family: Arial; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: pre-wrap; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;" class="auto-style162">This is computer generated document, no manual signature required</span></td>
            </tr>
        </table>
        <span class="auto-style162">
        <br />
        <br />
        <br />
        </span>
        </div>
    </form>
</body>
</html>
