<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterPage/sbAdmin2_Shipset.master" AutoEventWireup="true" CodeFile="Ship_Set_Report.aspx.cs" Inherits="PrivatePage_Ship_set_Ship_Set_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="col-12">
            <h1>Create Report</h1>
            <hr />
        </div>
    </div>

    <div class="card mb-3">
        <div class="card-header">
            <b>Ship Set Details Report</b>
        </div>
        <div class="card-body">
            <div class="form-group">
                <div class="form-row">
                    <div class="col-md-2">
                        <asp:Button ID="Button1" class="btn btn-success btn-block" runat="server" Text="Create Report Link" OnClick="Button1_Click" />                     
                    </div>
                    <div class="col-md-2">
                         <asp:HyperLink ID="HyperLink1" runat="server">HyperLink</asp:HyperLink>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

