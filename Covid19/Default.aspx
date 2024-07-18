<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sbAdmin2_Covid.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="PublicPage_Covid19_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script>

        $(document).ready(function () {

          <%--  $('#<%= TextBox2.ClientID %>').focus(function () {

                alert("test");

            });--%>


            $('#<%= TextBox2.ClientID %>').keypress(function (e) {

                if (e.keyCode == 13) {
                    e.preventDefault();
                    checkTemp();
                }
            });

        });


        function clear() {

            $('#<%= TextBox1.ClientID %>').val("");
            $('#<%= TextBox2.ClientID %>').val("");
            $('#<%= TextBox5.ClientID %>').val("");
            $('#<%= TextBox4.ClientID %>').val("");
            $('#<%= TextBox6.ClientID %>').val("");
            $('#<%= TextBox7.ClientID %>').val("");

        }


        function checkTemp() {


            $.ajax({
                type: "POST",
                url: "Default.aspx/checkTemp",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (response) {
                    //alert(JSON.stringify(response));
                    data = JSON.parse(response.d);
                    //alert(data.email_data);

                    temp = parseFloat($('#<%= TextBox2.ClientID %>').val());


                    if ((temp >= parseFloat(data.MIN_TEMP)) && (temp <= parseFloat(data.MAX_TEMP))) {
                        $('#txtarea').val(data.MSG_DSP);

                        $('#myModal').modal('show');


                        $('#myModal').on('shown.bs.modal', function (e) {
                            $('#yesbtn').focus();
                        })

                        //setTimeout(function () {
                        //    $('#yesbtn').focus();
                        //}, 200)

                        //$.playSound('/Sound/analog.mp3');
                        //alert("out of range")
                    }

                    else
                        alert("Temperature Out of healthy range. Please rest for a while")
                },
                failure: function (response) {
                    alert(response);
                }
            });


        }


        function submit_data(remark) {

            if ($('#<%= TextBox4.ClientID %>').val().trim() != "") {
                $.ajax({
                    type: "POST",
                    url: "Default.aspx/submit",
                    data: "{id:'" + $('#<%= TextBox1.ClientID %>').val() + "',temp:'" + $('#<%= TextBox2.ClientID %>').val() + "',remark:'" + remark + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    success: function (response) {
                        //alert(JSON.stringify(response));
                        data = JSON.parse(response.d);
                        //alert(data.email_data);

                        //alert(data.result);
                    },
                    failure: function (response) {
                        alert("ERROR : " + response);
                    }
                });

                $('#myModal').modal('hide');
                clear();


                if (remark == "YES") {
                    $('#yesModal').modal('show');
                    $('#yesModal').on('shown.bs.modal', function (e) {
                        $('#yesOK').focus();
                    })
                    $('#yesModal').on('hide.bs.modal', function (e) {
                        $('#<%= TextBox1.ClientID %>').focus();
                    })
                }
                else {
                    $('#noModal').modal('show');
                    $('#noModal').on('shown.bs.modal', function (e) {
                        $('#noOK').focus();
                    })
                    $('#noModal').on('hide.bs.modal', function (e) {
                        $('#<%= TextBox1.ClientID %>').focus();
                    })
                }

                $('#<%= TextBox1.ClientID %>').focus();

            }


            else {

                alert("Cannot find user record.");
            }


        }


    </script>


    <h1 class="h3 mb-1 text-gray-800">Emp Info</h1>
    <p class="mb-4">Please input Employee Number in the text box and click Find to start looking for the employee details</p>

    

    <div class="row">
        <div class="col-lg-6">
            <div class="card shadow mb-4">

                <div class="card-body">

                    <div class="form-group">
                        <div class="form-row">
                            <div class="col-md-6">
                                <label for="exampleInputName">Employee number or HID</label>
                                <div class="spinner-grow text-danger" role="status" ID="AlertRed4" runat="server" Visible="False">
                                    <span class="sr-only">Loading...</span>
                                </div>
                                <asp:TextBox ID="TextBox1" runat="server" class="form-control" placeholder="Enter Emp Number / HID"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="exampleInputName">.</label>
                                <div class="col-md mt-1">
                                    <strong>
                                        <asp:Label ID="Label1" runat="server" Text="*Emp Number not exist" CssClass="auto-style4" Visible="False" Style="color: #FF0000"></asp:Label>
                                        <asp:Label ID="Label2" runat="server" Text="*No Test within 14 days" CssClass="auto-style4" Visible="False" Style="color: #FF0000"></asp:Label>
                                        <asp:Label ID="Label3" runat="server" Text="*Done Self Test" CssClass="auto-style4" Visible="False" Style="color: green"></asp:Label>
                                    </strong>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="alert alert-danger" role="alert" ID="AlertRed" runat="server" Visible="False">
                        Alert!! Employee not done Covid Test within 14 days
                    </div>
                    <div class="alert alert-success" role="alert" ID="AlertGreen" runat="server" Visible="False">
                        Done Covid19 Test
                    </div>
                    <div class="form-group">
                        <div class="form-row">
                            <div class="col-md">
                                <asp:Button ID="Button1" runat="server" class="btn btn-primary btn-block" type="submit" Text="Find" OnClick="Button1_Click" />
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
        <%---------------------------------------------------------------------------------------------------------------------------------------%>
        <div class="col-lg-6">
            <div class="card shadow mb-4">

                <div class="card-body">

                    <asp:Panel ID="panel" runat="server">
                        <div class="form-group">
                            <div class="form-row">
                                <div class="col-md">
                                    <label for="exampleInputName">Temperature Value</label>
                                    <div class="spinner-grow text-danger" role="status" ID="AlertRed3" runat="server" Visible="False">
                                        <span class="sr-only">Loading...</span>
                                    </div>
                                    <asp:TextBox ID="TextBox2" runat="server" class="form-control" placeholder="Enter Temperature" TextMode="Number" step="any"></asp:TextBox>
                                </div>

                            </div>
                        </div>

                        <div class="alert alert-danger" role="alert" ID="AlertRed2" runat="server" Visible="False">
                            Alert!! Employee need to have a valid form to proceed
                        </div>

                        <div class="form-group">
                            <div class="form-row">
                                <div class="col-md">
                                    <%-- <asp:Button ID="Button2" runat="server" class="btn btn-success btn-block" type="submit" Text="Submit" OnClick="Button2_Click"/>--%>
                                    <input type="submit" onclick="checkTemp(); return false;" class="btn btn-success btn-block" value="Check" />
                                </div>
                                <%--<div class="col-md-4">
                                     <input type="button" onclick="checkTemp()" class="btn btn-success btn-block"  value="check"/>
                                </div>--%>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-lg">
            <div class="card shadow mb-4">
                <div class="card-header py-3">
                    <h6 class="m-0 font-weight-bold text-primary">Employee Details</h6>
                </div>
                <div class="card-body">

                    <div class="form-group">
                        <div class="form-row">
                            <div class="col-md-6">
                                <label for="exampleInputName">Full Name</label>
                                <asp:TextBox ID="TextBox4" runat="server" class="form-control" ReadOnly="True"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="exampleInputEmail1">Email</label>
                                <asp:TextBox ID="TextBox5" runat="server" class="form-control" type="email" ReadOnly="True"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="form-row">
                            <div class="col-md-6">
                                <label for="exampleInputPassword1">Supervisor</label>
                                <asp:TextBox ID="TextBox6" runat="server" class="form-control" ReadOnly="True"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="exampleConfirmPassword">Manager</label>
                                <asp:TextBox ID="TextBox7" runat="server" class="form-control" ReadOnly="True"></asp:TextBox>
                            </div>
                        </div>
                    </div>


                </div>
            </div>
        </div>

        <!-- Modal -->
        <div class="modal fade" id="myModal" role="dialog">
            <div class="modal-dialog modal-lg">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h1 class="modal-title" style="color: #FF0000">Employee Declaration</h1>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>

                    </div>
                    <div class="modal-body">
                        <%--<div class="form-group">
                            <div class="form-group">
                                <textarea id="txtarea" class="form-control"></textarea>
                            </div>
                        </div>--%>
                        <h2 style="color: black"><%=msg_dsp %></h2>
                    </div>

                    <div class="modal-footer">
                        <input type="button" class="btn btn-default btn-success" id="yesbtn" onclick="submit_data('YES')" value="YES" />
                        <input type="button" class="btn btn-default btn-danger" onclick="submit_data('NO')" value="NO" />
                    </div>
                </div>

            </div>
        </div>

        <!-- Modal -->
        <div class="modal fade" id="yesModal" role="dialog">
            <div class="modal-dialog modal-lg">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h1 class="modal-title" style="color: #FF0000">Employee Declaration</h1>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>

                    </div>
                    <div class="modal-body">
                        <%--<div class="form-group">
                            <div class="form-group">
                                <textarea id="txtarea" class="form-control"></textarea>
                            </div>
                        </div>--%>
                        <h2 style="color: black">Please proceed to your working area. Sila bergerak ke kawasan kerja anda</h2>
                    </div>

                    <div class="modal-footer">
                        <input type="button" class="btn btn-default btn-primary" id="yesOK" value="OK" data-dismiss="modal" />
                    </div>
                </div>

            </div>
        </div>

        <!-- Modal -->
        <div class="modal fade" id="noModal" role="dialog">
            <div class="modal-dialog modal-lg">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h1 class="modal-title" style="color: #FF0000">Employee Declaration</h1>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>

                    </div>
                    <div class="modal-body">
                        <%--<div class="form-group">
                            <div class="form-group">
                                <textarea id="txtarea" class="form-control"></textarea>
                            </div>
                        </div>--%>
                        <h2 style="color: black">You need to go to securty room to fill form. Sila pergi ke Bilik sekuriti untuk mengisi borang</h2>
                    </div>

                    <div class="modal-footer">
                        <input type="button" class="btn btn-default btn-primary" id="noOK" value="OK" data-dismiss="modal" />
                    </div>
                </div>

            </div>
        </div>

    </div>

</asp:Content>

