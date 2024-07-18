<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sbAdmin2_EmpInfo.master" AutoEventWireup="true" CodeFile="Emp_info_escapee.aspx.cs" Inherits="PublicPage_Emp_info_certactive" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="card shadow mb-3 mx-3 my-3">
        <div class="card-header">
            <h6 class="m-0 font-weight-bold text-primary"> <i class="fa fa-table"></i> Escapee</h6>
        </div>
        <div class="card-body">
            <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" RepeatDirection="Horizontal" Width="40%">
                <asp:ListItem Selected="True">All</asp:ListItem>
                <asp:ListItem>Supervisor</asp:ListItem>
                <asp:ListItem>Manager</asp:ListItem>
            </asp:RadioButtonList>

            <div class="table-responsive">
                <asp:GridView ID="dataTable" runat="server" Width="100%" class="table table-bordered" ClientIDMode="Static" OnRowDataBound="dataTable_RowDataBound">
                </asp:GridView>
            </div>
        </div>
        <div class="card-footer small text-muted">Updated yesterday at 11:59 PM</div>
    </div>
</asp:Content>
