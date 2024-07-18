<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Emp_info.master" AutoEventWireup="true" CodeFile="Emp_info_certactive.aspx.cs" Inherits="PublicPage_Emp_info_certactive" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="card mb-3 mx-3 my-3">
        <div class="card-header">
            <i class="fa fa-table"></i> Active List
        </div>
        <div class="card-body">
            <div class="table-responsive">


                <asp:GridView ID="dataTable" runat="server" Width="100%" class="table table-bordered" ClientIDMode="Static" OnRowDataBound="dataTable_RowDataBound">
                </asp:GridView>


            </div>
        </div>
        <div class="card-footer small text-muted">Updated yesterday at 11:59 PM</div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="jsThisPage" Runat="Server">
</asp:Content>

