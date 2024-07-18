<%@ Page Title="" MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/MasterPage/Emp_info.master" AutoEventWireup="true" CodeFile="Emp_info_register_defect.aspx.cs" Inherits="PublicPage_EmpInfo_Emp_info_register_defect" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-12">
            <h1>Grape Chart</h1>
            <hr />
        </div>
    </div>
    <asp:Panel ID="Panel10" runat="server">
        <div class="card mx-3 my-10">
            <div class="card-header">Defect Process Editor</div>
            <div class="card-body">

                <div class="form-group">
                    <div class="form-row">

                        <div class="col-md-6">
                            <asp:Panel ID="Panel" runat="server" DefaultButton="Search">
                                <div class="footer-widget instagram-widget">
                                    <h3><span>Defect</span> </h3>
                                </div>

                                <asp:ListBox ID="ListBox1" Height="170px" class="form-control" runat="server" OnSelectedIndexChanged="ListBox1_SelectedIndexChanged" AutoPostBack="True" Width="100%"></asp:ListBox>

                                <div class="form-row">
                                    <div class="col-md-8">
                                        <asp:TextBox ID="TextBox1" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Button ID="Search" class="btn btn-info btn-block" runat="server" Text="Search" OnClick="Search_Click" />
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Button ID="Reset" class="btn btn-warning btn-block" runat="server" Text="Reset" OnClick="Reset_Click" />
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>

                        <div class="col-md-6">
                            <asp:Panel ID="Panel1" runat="server" DefaultButton="Search1">
                                <div class="footer-widget instagram-widget">
                                    <h3><span>Process</span> </h3>
                                </div>
                                <asp:ListBox ID="ListBox2" Height="170px" class="form-control" runat="server" AutoPostBack="True" Width="100%"></asp:ListBox>

                                <div class="form-row">
                                    <div class="col-md-8">
                                        <asp:TextBox ID="TextBox2" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Button ID="Search1" class="btn btn-info btn-block" runat="server" Text="Search" OnClick="Search_Click" />
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Button ID="Reset1" class="btn btn-warning btn-block" runat="server" Text="Reset" OnClick="Reset_Click" />
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>

                <div class="form-group">
                    <div class="form-row">

                        <div class="col-md-6">
                            <asp:ListBox ID="ListBox3" Height="200px" class="form-control" runat="server" BackColor="#c8c8c8" Width="100%"></asp:ListBox>
                            <div class="form-row">
                                <div class="col-md-8">
                                    <asp:Button ID="Bind" Width="100%" class="btn btn-outline-dark" runat="server" Text="Bind" OnClick="Bind_Click" />
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="Remove" Width="100%" class="btn btn-danger btn-block" runat="server" Text="Remove" OnClick="Remove_Click" />
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="Reset2" class="btn btn-warning btn-block" runat="server" Text="Reset" OnClick="Reset2_Click" />
                                </div>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <strong>
                                <asp:Label ID="error" runat="server" ForeColor="Red" Text="*Process binded is existed" CssClass="auto-style4" Visible="False"></asp:Label>
                            </strong>
                        </div>

                    </div>
                </div>

            </div>
        </div>
    </asp:Panel>

    <br />

    <asp:Panel ID="Panel2" runat="server">
        <div class="card mx-3 my-10">
            <div class="card-header">Station Group Editor </div>
            <div class="card-body">

                <div class="form-group">
                    <div class="form-row">

                        <div class="col-md-6">
                            <asp:Panel ID="Panel3" runat="server" DefaultButton="Search">
                                <div class="footer-widget instagram-widget">
                                    <h3><span>Station Group</span></h3>
                                </div>

                                <div class="form-row">
                                    <div class="col-md-6">
                                        <asp:DropDownList ID="dropdown11" class="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                                        <asp:ListItem>Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:ListBox ID="listbox11" runat="server" class="form-control" Height="200px"></asp:ListBox>
                                        <br />
                                        <asp:Button ID="remove1" runat="server" class="btn btn-danger btn-block" OnClick="Remove1_Click" Text="Remove Seleted" />
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>

                        <div class="col-md-6">
                            <asp:Panel ID="Panel4" runat="server" DefaultButton="Search1">
                                <div class="footer-widget instagram-widget">
                                    <h3><span>Register Station</span></h3>
                                </div>

                                <div class="form-row">
                                    <div class="col-md-3">
                                        <asp:Label ID="lblresource" runat="server">Resource Name</asp:Label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="rname" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Button ID="copy" runat="server" class="btn btn-primary btn-block" OnClick="Copy_Click" Text="Copy" />
                                    </div>
                                </div>
                                <br />
                                <div class="form-row">
                                    <div class="col-md-3">
                                        <asp:Label ID="desc" runat="server">Description</asp:Label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="des" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <div class="form-row">
                                    <div class="col-md-3">
                                        <asp:Label ID="lblstation" runat="server">Station</asp:Label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:DropDownList ID="dropwdown22" runat="server" class="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Button ID="insert" runat="server" class="btn btn-primary btn-block" OnClick="Insert_Click" Text="Insert" />
                                    </div>
                                </div>
                                <br />
                                <div class="form-row">
                                    <div class="col-md-3">
                                        <asp:Label ID="lblselstation" runat="server">Station Selected</asp:Label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:ListBox ID="listbox22" runat="server" class="form-control"></asp:ListBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Button ID="submit" runat="server" class="btn btn-success btn-block" OnClick="Submit_Click" Text="Submit" />
                                        <asp:Button ID="reset4" runat="server" class="btn btn-warning btn-block" OnClick="Reset4_Click" Text="Reset" />

                                    </div>
                                </div>

                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="jsThisPage" runat="Server">
</asp:Content>

