<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterPage/sbAdmin2_Shipset.master" AutoEventWireup="true" CodeFile="Ship_Set_Add.aspx.cs" Inherits="PrivatePage_Ship_set_Ship_Set_Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="row">
        <div class="col-12">
            <h1>Ship Set Registration</h1>
            <hr />
        </div>
    </div>

    <asp:Panel ID="Panel1" runat="server">
        <div class="card mx-3 my-10">
            <div class="card-header" style="font-size: x-large">Item Registration</div>
            <div class="card-body">

                    <div class="form-row">

                        <%--                        <div class="col-md-12">
                            <div class="footer-widget instagram-widget">
                                <h3><span>Item In</span></h3>
                            </div>
                        </div>--%>

                        <div class="col-md-12">

                            <div class="form-row">
                                <div class="col-md-3">
                                    <asp:Label ID="lblModel" runat="server">Model</asp:Label>
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtModel" runat="server" class="form-control" MaxLength="30"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="form-row">
                                <div class="col-md-3">
                                    <asp:Label ID="lblEsir" runat="server">ESIR</asp:Label>
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtEsir" runat="server" class="form-control" MaxLength="30"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="form-row">
                                <div class="col-md-3">
                                    <asp:Label ID="lblSn" runat="server">Serial Number</asp:Label>
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtSn" runat="server" class="form-control" MaxLength="50"></asp:TextBox>
                                </div>
                            </div>

                            <br />
                            <div class="form-row">
                                <div class="col-md-3">
                                    <asp:Label ID="lblPickNo" runat="server">Pick Number</asp:Label>
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtPickNo" runat="server" class="form-control" MaxLength="8" onkeydown="return (!(event.keyCode>=65 && event.keyCode<=90 ) && event.keyCode!=32);"></asp:TextBox>
                                </div>
                            </div>

                            <br />
                            <div class="form-row">
                                <div class="col-md-3">
                                    <asp:Label ID="lblStatus" runat="server">Status</asp:Label>
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtStatus" runat="server" class="form-control" MaxLength="10"></asp:TextBox>
                                </div>
                            </div>

                            <br />
                            <div class="form-row">
                                <div class="col-md-6">
                                    <asp:Button ID="submit" runat="server" class="btn btn-success btn-block" OnClick="Submit_Click" Text="Submit" />
                                </div>
                                <div class="col-md-6">
                                    <asp:Button ID="reset" runat="server" class="btn btn-danger btn-block pull-right" OnClick="Reset_Click" Text="Reset" />
                                </div>
                            </div>
                        </div>
                    </div>
                

            </div>
        </div>
    </asp:Panel>
    <br />
</asp:Content>

