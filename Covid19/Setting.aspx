<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sbAdmin2_Covid.master" AutoEventWireup="true" CodeFile="Setting.aspx.cs" Inherits="PublicPage_Covid19_Setting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <h1 class="h3 mb-1 text-gray-800">System Setting</h1>
    <p class="mb-4">System setting where all the value need to set and change to according to current purpose</p>

    <div class="row">
        <div class="col-lg">
            <div class="card shadow mb-4">
                <div class="card-header py-3">
                    <h6 class="m-0 font-weight-bold text-primary">Upload Covid Test Data</h6>
                </div>
                <div class="card-body">
                    <div class="form-row">
                        <div class="input-group col-md-6">
                            <div class="custom-file">
                                <asp:FileUpload ID="FileUploadControl" class="form-control" runat="server" />
                            </div>
                            <div class="input-group-append">
                                <asp:Button ID="UploadButton" runat="server" class="btn btn-primary btn-block" OnClick="UploadButton_Click" Text="Upload" Width="100px" />
                            </div>
                        </div>
                        <div class="input-group col-md-3">
                            <asp:Label ID="StatusLabel" runat="server" Text="Upload status: " />
                        </div>
                        <div class="input-group col-md-3">
                            <asp:Label ID="Label2" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-lg">
            <div class="card shadow mb-4">
                <div class="card-header py-3">
                    <h6 class="m-0 font-weight-bold text-primary">Dashboard Setting</h6>
                </div>
                <div class="card-body">
                    <div class="form-group">
                        <div class="form-row">
                            <div class="col-md-6">
                                <label for="exampleInputName">Total Morning Employee</label>
                                <asp:TextBox ID="TextBox3" runat="server" class="form-control" placeholder="Total in Number" TextMode="Number"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="exampleInputName">Max Morning Employee(%)</label>

                                <div class="input-group">
                                    <asp:TextBox ID="TextBox4" runat="server" class="form-control" placeholder="Max in %" TextMode="Number"></asp:TextBox>
                                    <div class="input-group-append">
                                        <span class="input-group-text">%</span>
                                    </div>
                                </div>


                            </div>

                        </div>
                        <div class="form-row">
                            <div class="col-md-6">
                                <label for="exampleInputName">Total Night Employee</label>
                                <asp:TextBox ID="TextBox6" runat="server" class="form-control" placeholder="Total in Number" TextMode="Number"></asp:TextBox>
                            </div>

                            <div class="col-md-6">
                                <label for="exampleInputName">Max Night Employee(%)</label>

                                <div class="input-group mb-3">
                                    <asp:TextBox ID="TextBox5" runat="server" class="form-control" placeholder="Max in %" TextMode="Number"></asp:TextBox>
                                    <div class="input-group-append">
                                        <span class="input-group-text">%</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="form-row">
                            <div class="col-md">
                                <asp:Button ID="Button2" runat="server" class="btn btn-primary btn-block" type="submit" Text="Update" OnClick="Button1_Click" />
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>

    <div class="row">
        <div class="col-lg">
            <div class="card shadow mb-4">
                <div class="card-header py-3">
                    <h6 class="m-0 font-weight-bold text-primary">Temperature Setting</h6>
                </div>
                <div class="card-body">
                    <div class="form-group">
                        <div class="form-row">
                            <div class="col-md-6">
                                <label for="exampleInputName">Minimum Temperature</label>
                                <asp:TextBox ID="TextBox1" runat="server" class="form-control" placeholder="Enter Min Temp"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="exampleInputName">maximum Temperature</label>
                                <asp:TextBox ID="TextBox2" runat="server" class="form-control" placeholder="Enter Max Temp"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="form-row">
                            <div class="col-md">
                                <label for="exampleInputName">Message Display</label>
                                <textarea id="TextArea1" runat="server" rows="5" class="form-control"></textarea>
                            </div>

                        </div>
                    </div>

                    <div class="form-group">
                        <div class="form-row">
                            <div class="col-md">
                                <asp:Button ID="Button1" runat="server" class="btn btn-primary btn-block" type="submit" Text="Update" OnClick="Button1_Click" />
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>



    </div>

</asp:Content>

