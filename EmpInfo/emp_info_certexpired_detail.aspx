<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Emp_info.master" AutoEventWireup="true" CodeFile="emp_info_certexpired_detail.aspx.cs" Inherits="PublicPage_emp_info_cerexpired_detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="card mb-3 mx-3 my-3">
        <div class="card-header">
            <i class="fa fa-table"></i> Expiration Details <asp:Button ID="Button1" runat="server" Text="Export" class="btn btn-primary btn-block col-1" OnClick="Button1_Click"/>
        </div>
        <div class="card-body">
            <div class="table-responsive">


                <asp:GridView ID="dataTable" runat="server" Width="100%" class="table table-bordered" ClientIDMode="Static" OnRowDataBound="dataTable_RowDataBound" >
                </asp:GridView>


            </div>
        </div>
        <div class="card-footer small text-muted">Updated yesterday at 11:59 PM</div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="jsThisPage" Runat="Server">
</asp:Content>

