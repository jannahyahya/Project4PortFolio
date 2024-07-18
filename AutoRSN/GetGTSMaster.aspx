<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetGTSMaster.aspx.cs" Inherits="AutoRSN.GetGTSMaster" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
</head>
<body>
<head> //HTML and Ajax 
<title></title>

 <script>
     function GetMessage() {
         //Load jQuery($) to Use 
         $(function() {
             $.get("GetGTSData.asmx/GetMessage", function (data) {
                 console.log(data);
                 $("p").html(data);
             });
         });

     }
 </script>
</head>
<body>

    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <input type="button" onclick="GetMessage()" value="Get Message" />
    <p></p>
</body>
</body>
</html>