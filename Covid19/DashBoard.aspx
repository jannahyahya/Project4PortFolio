<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sbAdmin2_Covid.master" AutoEventWireup="true" CodeFile="DashBoard.aspx.cs" Inherits="PublicPage_Covid19_DashBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        setTimeout(function () {
            window.location.reload(1);
        }, 10000);
    </script>
    <%--<style>
        .col-container {
            display: flex;
            width: 100%;
        }

        .col {
            flex: 1;
            /*padding: 16px;*/
        }

        .text-large {
            font-size: 150%;
        }
    </style>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="d-sm-flex align-items-center justify-content-between">
        <h1 class="h3 mb-0 text-gray-800">Dashboard</h1>
        <a href="#" class="d-none d-sm-inline-block btn btn-sm btn-primary shadow-sm"><i class="fas fa-download fa-sm text-white-50"></i>Generate Report</a>
    </div>

    <p class="mb-4">Date : <%= DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt") %></p>


    <div class="row">
        <div class="col-lg-6">
            <div class="card shadow mb-4">
                <!-- Card Header - Dropdown -->
                <div class="card-header py-3 d-flex flex-row align-items-center justify-content-between">
                    <h6 class="m-0 mr-auto p-2 font-weight-bold text-primary">Today Daily Attendance(Max 60.0%)</h6>
                    <h6 class="m-0 p-2 font-weight-bold text-primary"># of Employee:
                            <asp:Label ID="Label1" class="mb-0 font-weight-bold text-gray-800" runat="server" Text="0"></asp:Label></h6>
                    <h6 class="m-0 p-2 font-weight-bold text-primary">Turn Up:
                            <asp:Label ID="Label2" class="mb-0 font-weight-bold text-gray-800" runat="server" Text="0"></asp:Label></h6>

                </div>
                <!-- Card Body -->
                <div class="card-body">
                    <div class="text-center small">
                        <asp:Label ID="tDay" class="display-1 mb-0 font-weight-bold text-success" runat="server" Text="0.00%"></asp:Label>
                    </div>
                </div>

            </div>
        </div>

        <div class="col-lg-6">
            <div class="card shadow mb-4">
                <!-- Card Header - Dropdown -->
                <div class="card-header py-3 d-flex flex-row align-items-center justify-content-between">
                    <h6 class="m-0 mr-auto p-2 font-weight-bold text-primary">Yesterday Daily Attendance(Max 60.0%)</h6>
                    <h6 class="m-0 p-2 font-weight-bold text-primary"># of Employee:
                            <asp:Label ID="Label3" class="mb-0 font-weight-bold text-gray-800" runat="server" Text="0"></asp:Label></h6>
                    <h6 class="m-0 p-2 font-weight-bold text-primary">Turn Up:
                            <asp:Label ID="Label4" class="mb-0 font-weight-bold text-gray-800" runat="server" Text="0"></asp:Label></h6>

                </div>
                <!-- Card Body -->
                <div class="card-body">
                    <div class="text-center small">
                        <asp:Label ID="yDay" class="display-1 mb-0 font-weight-bold text-success" runat="server" Text="0.00%"></asp:Label>
                    </div>
                </div>

            </div>
        </div>

    </div>

    <div class="row">



        <div class="col-lg-3 ">
            <div class="card shadow mb-4">
                <!-- Card Header - Dropdown -->
                <div class="card-header  d-flex flex-row align-items-center justify-content-between">
                    <h6 class="m-0 p-2 mr-auto p-2  text-primary">Morning (7am~7pm)</h6>
                    <h6 class="m-0 p-2  text-primary"># Emp:
                        <asp:Label ID="morningEmpDisp" class="mb-0 font-weight-bold text-gray-800" runat="server" Text="0"></asp:Label></h6>
                    <h6 class="m-0  text-primary">Turn Up:
                        <asp:Label ID="morningScanDisp" class="mb-0 font-weight-bold text-gray-800" runat="server" Text="0"></asp:Label></h6>
                </div>
                <!-- Card Body -->
                <div class="card-body">
                    <div class="text-center small">
                        <asp:Label ID="tMorning" class="display-4 mb-0 font-weight-bold text-success" runat="server" Text="0.00%"></asp:Label>

                    </div>
                </div>
            </div>

        </div>

        <div class="col-lg-3">
            <div class="card shadow mb-4">
                <!-- Card Header - Dropdown -->
                <div class="card-header  d-flex flex-row align-items-center justify-content-between">
                    <h6 class="m-0 mr-auto p-2  text-primary">Night (7pm~7am)</h6>
                    <h6 class="m-0 p-2  text-primary"># Emp:
                            <asp:Label ID="nightEmpDisp" class="mb-0 font-weight-bold text-gray-800" runat="server" Text="0"></asp:Label></h6>
                    <h6 class="m-0 p-2  text-primary">Turn Up:
                            <asp:Label ID="nightScanDisp" class="mb-0 font-weight-bold text-gray-800" runat="server" Text="0"></asp:Label></h6>
                </div>
                <!-- Card Body -->
                <div class="card-body">
                    <div class="text-center small">
                        <asp:Label ID="tNight" class="display-4 mb-0 font-weight-bold text-success" runat="server" Text="0.00%"></asp:Label>
                    </div>
                </div>
            </div>
        </div>


        <div class="col-lg-3 ">
            <div class="card shadow mb-4">
                <!-- Card Header - Dropdown -->
                <div class="card-header  d-flex flex-row align-items-center justify-content-between">
                    <h6 class="m-0 p-2 mr-auto p-2  text-primary">Yesterday Morning</h6>
                    <h6 class="m-0 p-2  text-primary"># Emp:
                        <asp:Label ID="yMorningEmpDisp" class="mb-0 font-weight-bold text-gray-800" runat="server" Text="0"></asp:Label></h6>
                    <h6 class="m-0  text-primary">Turn Up:
                        <asp:Label ID="yMorningScanDisp" class="mb-0 font-weight-bold text-gray-800" runat="server" Text="0"></asp:Label></h6>
                </div>
                <!-- Card Body -->
                <div class="card-body">
                    <div class="text-center small">
                        <asp:Label ID="yMorning" class="display-4 mb-0 font-weight-bold text-success" runat="server" Text="0.00%"></asp:Label>

                    </div>
                </div>
            </div>

        </div>

        <div class="col-lg-3">
            <div class="card shadow mb-4">
                <!-- Card Header - Dropdown -->
                <div class="card-header  d-flex flex-row align-items-center justify-content-between">
                    <h6 class="m-0 mr-auto p-2  text-primary">Yesterday Night</h6>
                    <h6 class="m-0 p-2  text-primary"># Emp:
                            <asp:Label ID="yNightEmpDisp" class="mb-0 font-weight-bold text-gray-800" runat="server" Text="0"></asp:Label></h6>
                    <h6 class="m-0 p-2  text-primary">Turn Up:
                            <asp:Label ID="yNightScanDisp" class="mb-0 font-weight-bold text-gray-800" runat="server" Text="0"></asp:Label></h6>
                </div>
                <!-- Card Body -->
                <div class="card-body">
                    <div class="text-center small">
                        <asp:Label ID="yNight" class="display-4 mb-0 font-weight-bold text-success" runat="server" Text="0.00%"></asp:Label>
                    </div>
                </div>
            </div>
        </div>


    </div>

</asp:Content>

