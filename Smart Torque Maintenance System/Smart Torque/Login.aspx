<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Smart_Torque.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Login</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <p>
            <asp:Label ID="Label1" runat="server" Text="Login :"></asp:Label>
            &nbsp;
            <asp:TextBox ID="WinID" runat="server" placeholder="Window ID" AutoCompleteType="Disabled"></asp:TextBox>
            &nbsp;
            <asp:Label ID="Label3" runat="server" Text=" & "></asp:Label>
            &nbsp;
            <asp:TextBox ID="WinIDpass" runat="server" placeholder="Password" TextMode="Password" AutoCompleteType="Disabled"></asp:TextBox>
        </p>
        <p>
            <asp:Label ID="Label2" runat="server" Text="Customer :"></asp:Label>
            &nbsp;
            <asp:DropDownList ID="Customer" runat="server">
            </asp:DropDownList>
        </p> 
        <p>
            &nbsp;
        <asp:Button ID="Connect" runat="server" Font-Bold="True" OnClick="ConnectDB" Text="Connect" />
        </p>
       <asp:Label ID="Label5" Font-Bold="True" runat="server"></asp:Label>
    </form>
</body>
</html>
