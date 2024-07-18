<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sbAdmin2_EmpInfo.master" AutoEventWireup="true" CodeFile="Emp_Info.aspx.cs" Inherits="PublicPage_Emp_Info" MaintainScrollPositionOnPostback="true"%>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style4 {
            color: #FF3300;
        }

        .auto-style5 {
            font-size: large;
            color: #FF0000;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Page Heading -->
    <h1 class="h3 mb-1 text-gray-800">Emp Info</h1>
    <p class="mb-4">Please input Employee Number in the text box and click Find to start looking for the employee details</p>

    <!-- Content Row -->
    <div class="row">
        <div class="col-lg-9">
            <div class="card shadow mb-4">
                <div class="card-header py-3">
                    <h6 class="m-0 font-weight-bold text-primary">Employee Details</h6>
                </div>
                <div class="card-body">
                    <div class="form-group">
                        <div class="form-row">
                            <div class="col-md-6">
                                <label for="exampleInputName">Employee number</label>
                                <asp:TextBox ID="TextBox1" runat="server" class="form-control" placeholder="Enter Emp Number"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="exampleInputName">_</label>
                                <div class="col-md-6 mt-1">
                                    <strong>
                                        <asp:Label ID="Label1" runat="server" Text="*Emp Number not exist" CssClass="auto-style4" Visible="False"></asp:Label>
                                    </strong>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="form-row">
                            <div class="col-md-6">
                                <label for="exampleInputName">Full Name</label>
                                <asp:TextBox ID="TextBox3" runat="server" class="form-control" ReadOnly="True"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="exampleInputEmail1">Email</label>
                                <asp:TextBox ID="TextBox2" runat="server" class="form-control" type="email" ReadOnly="True"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="form-row">
                            <div class="col-md-6">
                                <label for="exampleInputPassword1">Supervisor</label>
                                <asp:TextBox ID="TextBox4" runat="server" class="form-control" ReadOnly="True"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="exampleConfirmPassword">Manager</label>
                                <asp:TextBox ID="TextBox5" runat="server" class="form-control" ReadOnly="True"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="form-row">
                            <div class="col-md-8">
                                <asp:Button ID="Button1" runat="server" class="btn btn-primary btn-block" type="submit" Text="Find" OnClick="Button1_Click" />
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="Button2" runat="server" class="btn btn-success btn-block" type="submit" Text="Cert List" OnClick="Button2_Click" />
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="Button3" runat="server" class="btn btn-success btn-block" type="submit" Text="Grape Chart" OnClick="Button3_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <div class="col">
            <div class="card shadow mb-4">
                <div class="card-header py-3">
                    <h6 class="m-0 font-weight-bold text-primary">Badge Picture</h6>
                </div>
                <div class="card-body">
                    <div class="d-flex justify-content-center">
                        <asp:Image ID="Image1" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class="row">
        <div class="col-lg">
            <div class="card shadow mb-4" id="cert" runat="server">
                <div class="card-header py-3">
                    <h6 class="m-0 font-weight-bold text-primary">Valid Cert <span class="auto-style5">*A 30 days grace period is given and re-certification can be conducted within this 30 days grace period after the certification is due </span></h6>
                </div>
                <div class="card-body">
                    <div class="table-responsive col-12">
                        <asp:GridView ID="dataTable" runat="server" Width="100%" class="table table-bordered" ClientIDMode="Static" OnRowDataBound="dataTable_RowDataBound">
                        </asp:GridView>
                    </div>
                </div>
                <div class="card-footer small text-muted">Last Updated <%= DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") %></div>
            </div>

            <div class="card shadow mb-4" id="grape" runat="server">
                <div class="card-header py-3">
                    <h6 class="m-0 font-weight-bold text-primary">Grape Chart</h6>
                </div>
                <div class="card-body">
                    <div class="form-group">
                        <div class="form-row">
                            <div class="col-md-4">
                                <%-----------------------------------------------------------------------%>
                                <asp:Label ID="Label2" runat="server" Text="Process : "></asp:Label>
                                <asp:DropDownList ID="DropDownList1" class="form-control" runat="server" >
                                </asp:DropDownList>
                                <hr />
                                <asp:Calendar ID="Calendar1" runat="server" OnDayRender="Calendar1_DayRender" OnVisibleMonthChanged="Calendar1_VisibleMonthChanged" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="100%">
                                    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                                    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                                    <OtherMonthDayStyle ForeColor="#999999" />
                                    <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                                    <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                                    <TodayDayStyle BackColor="#CCCCCC" />
                                </asp:Calendar>
                                <%-----------------------------------------------------------------------%>
                            </div>
                            <div class="col-md-4">
                                <br />
                                <h5 class="m-0 font-weight-bold text-warning">Summary</h5>
                                
                                <hr />
                                <asp:GridView ID="GridView2" runat="server" Width="100%" ></asp:GridView>
                            </div>
                            <div class="col-md-4">
                                <br />
                                <h5 class="m-0 font-weight-bold text-warning">SN Affected</h5>
                                
                                <hr />
                                <asp:GridView ID="GridView1" runat="server" Width="100%" ></asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card-footer small text-muted">Last Updated <%= DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") %></div>
            </div>
        </div>
    </div>
</asp:Content>

