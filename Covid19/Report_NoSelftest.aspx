<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sbAdmin2_Covid.master" AutoEventWireup="true" CodeFile="Report_NoSelftest.aspx.cs" Inherits="PublicPage_Covid19_Report_NoSelftest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <h1 class="h3 mb-1 text-gray-800">No Self Test Report</h1>
    <p class="mb-4">The list is the person that not do self test within 14 days</p>

    <div class="row">
        <div class="col-lg">
            <div class="card shadow mb-4">
                <div class="card-header py-3">
                    <h6 class="m-0 font-weight-bold text-primary">Reporting</h6>
                </div>
                <div class="table-responsive">
                    <div class="card-body">

                        <div class="form-group">
                            <div class="form-row">
                                <div class="col-md-4">
                                    <asp:Label ID="Label11" runat="server" Text="Site"></asp:Label>
                                    <asp:DropDownList ID="DropDownList1" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">

                                        <asp:ListItem>CMY Only</asp:ListItem>
                                        <asp:ListItem>GBS Only</asp:ListItem>
                                        <asp:ListItem>All</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="col-md-4">
                                    <asp:Label ID="Label1" runat="server" Text="Download the Report"></asp:Label>
                                    <asp:Button ID="Button1" runat="server" class="btn btn-primary btn-block" type="submit" Text="Download" OnClick="Button1_Click" />
                                </div>

                            </div>
                        </div>

                        <div class="form-group">
                            <asp:GridView ID="dataTable" runat="server" class="table table-bordered" ClientIDMode="Static">
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="card-footer small text-muted">
                        <div class="form-row">
                            <div class="col-md-3">
                                Last Updated <%= DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") %>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>

