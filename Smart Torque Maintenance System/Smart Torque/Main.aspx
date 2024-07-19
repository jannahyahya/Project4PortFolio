<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Smart_Torque.Main" EnableEventValidation="false"  %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Main Page</title>
    <script type="text/javascript">
        function CreConfirm() {
            if (confirm("This entry does not exist in the database, Confirm to create it?")) {
                __doPostBack('<%= Button1.ClientID %>', 'OnClick');
            } else {
            }
        }
        function DelConfirm() {
            if (confirm("This entry exists in the database, Confirm to delete it?")) {
                __doPostBack('<%= Button2.ClientID %>', 'OnClick');
            } else {
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Info" runat="server"></asp:Label>
        &nbsp; &nbsp;
       <asp:Button ID="logout" runat="server" Text="Logout" Onclick="LogoutBtn"/>
         <br/>
        <br/>
        </div>
        <asp:Label ID="Label1" runat="server" Text="Model :"></asp:Label>
        &nbsp;
        <asp:TextBox ID="MODEL" runat="server" AutoCompleteType="Disabled" ></asp:TextBox>
        <p>
            <asp:Label ID="Label2" runat="server" Text="Program_ID :"></asp:Label>
            &nbsp;
            <asp:TextBox ID="PROGRAMID" runat="server" AutoCompleteType="Disabled" ></asp:TextBox>
        </p>
        <asp:Label ID="Label3" runat="server" Text="Station :"></asp:Label>
        &nbsp;
        <asp:TextBox ID="STATION" runat="server" AutoCompleteType="Disabled" ></asp:TextBox>
        <p>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Create" runat="server" Text="Create" Font-Bold="True" OnClick="CheEntryCheck"/>
            <asp:LinkButton ID="Button1"  runat="server"  OnClick="Insert"></asp:LinkButton>          
            &emsp; &emsp;
            <asp:Button ID="Delete" runat="server" Text="Delete" Font-Bold="True" OnClick="DelEntryCheck"/>
            <asp:LinkButton ID="Button2"  runat="server"  OnClick="Remove"></asp:LinkButton>  
        </p>
        <br />
         <asp:Label ID="Notice" runat="server" Font-Italic="True" ForeColor="Gray"></asp:Label>
        
    </form>
</body>
</html>
