<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterPage/sbAdmin2_Shipset.master" AutoEventWireup="true" CodeFile="Ship_Set_Search.aspx.cs" Inherits="PrivatePage_Ship_set_Ship_Set_Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="col-12">
            <h1>Search/Edit/Delete Ship Set Item</h1>
            <hr />
        </div>
    </div>
    <div class="card mb-3">
        <div class="card-header">
            <b>Search For Ship Set Details</b>
        </div>
        <div class="card-body">
            <div class="form-group">
                <div class="form-row">
                    <div class="col-md-4">
                        <label for="lblSearch">Serial Number </label>
                        <asp:TextBox ID="txtSearch123" runat="server" class="form-control" placeholder="Insert Serial Number "></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="form-group">
                <div class="form-row">
                    <div class="col-md-2">
                        <asp:Button ID="searchbtn" class="btn btn-success btn-block" runat="server" Text="Search" OnClick="searchbtn_Click" />                     
                    </div>
                    <div class="col-md-2">
                         <asp:Button ID="clearbtn" class="btn btn-danger btn-block pull-right" runat="server" Text="Clear" OnClick="clearbtn_Click"  />
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="form-row">
                    <div class="col-md-4">
                        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="card mb-3">
        <div class="card-header">
            <b>Edit / Delete</b>
        </div>
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
                                    <asp:TextBox ID="txtModel0" runat="server" class="form-control" MaxLength="30"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="form-row">
                                <div class="col-md-3">
                                    <asp:Label ID="lblEsir" runat="server">ESIR</asp:Label>
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtEsir0" runat="server" class="form-control" MaxLength="30"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="form-row">
                                <div class="col-md-3">
                                    <asp:Label ID="lblSn" runat="server">Serial Number</asp:Label>
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtSn0" runat="server" class="form-control" MaxLength="50"></asp:TextBox>
                                </div>
                            </div>

                            <br />
                            <div class="form-row">
                                <div class="col-md-3">
                                    <asp:Label ID="lblPickNo" runat="server">Pick Number</asp:Label>
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtPickNo0" runat="server" class="form-control" MaxLength="8" onkeydown="return (!(event.keyCode>=65 && event.keyCode<=90 ) && event.keyCode!=32);"></asp:TextBox>
                                </div>
                            </div>

                            <br />
                            <div class="form-row">
                                <div class="col-md-3">
                                    <asp:Label ID="lblStatus" runat="server">Status</asp:Label>
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtStatus0" runat="server" class="form-control" MaxLength="10"></asp:TextBox>
                                </div>
                            </div>

                            <br />
                            <div class="form-row">
                                <div class="col-md-4">
                                    <asp:Button ID="update" runat="server" class="btn btn-success btn-block"  Text="Update" OnClientClick="return confirm('Are you sure you wish to Update?');" OnClick="update_Click" />
                                </div>
                                <div class="col-md-4">
                                    <asp:Button ID="delete" runat="server" class="btn btn-danger btn-block pull-right"  Text="Delete" OnClientClick="return confirm('Are you sure you wish to Delete?');" OnClick="delete_Click" />
                                </div>
                                <div class="col-md-4">
                                    <asp:Button ID="reset" runat="server" class="btn btn-danger btn-block pull-right"  Text="Reset" OnClick="reset_Click" />
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="col-md-4">
                                    <asp:Label ID="lblMessage0" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                

            </div>
    </div>
</asp:Content>

