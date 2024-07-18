<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sbAdmin2_Covid.master" AutoEventWireup="true" CodeFile="Report.aspx.cs" Inherits="PublicPage_Covid19_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <h1 class="h3 mb-1 text-gray-800">System Setting</h1>
    <p class="mb-4">System setting where all the value need to set and change to according to current purpose</p>


    <div class="row">
        <div class="col-lg">
            <div class="card shadow mb-4">
                <div class="card-header py-3">
                    <h6 class="m-0 font-weight-bold text-primary">Reporting</h6>
                </div>
                <div class="card-body">
                    <div class="form-group">
                        <div class="form-row">
                            <div class="col-md-6">
                                <label for="exampleInputName">From Date</label>
                                <asp:TextBox ID="TextBox1" runat="server" class="form-control" placeholder="From Date" TextMode="Date"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="exampleInputName">To Date</label>
                                <asp:TextBox ID="TextBox2" runat="server" class="form-control" placeholder="To Date" TextMode="Date"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    

                    <div class="form-group">
                        <div class="form-row">
                            <div class="col-md">
                                <asp:Button ID="Button1" runat="server" class="btn btn-primary btn-block" type="submit" Text="Download" OnClick="Button1_Click" />
                            </div>

                        </div>
                    </div>

                    <div class="form-group">
                        <div class="form-row">

                            <asp:GridView ID="GridView1" runat="server">
                            </asp:GridView>


                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

