<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserTestPage.aspx.cs" Inherits="tests_UserTestPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel runat="server" BackColor="LightGreen" BorderColor="Black" 
            BorderStyle="Double" BorderWidth="10px" >
            <div style="text-align:center; border-bottom-color:Black; border-bottom-style:solid; border-bottom-width:thin">
               Create User 
            </div>
            Username: <asp:TextBox runat="server" ID="CreateUserName"></asp:TextBox>
            <br />Password: <asp:TextBox runat="server" ID="CreateUserPassword" TextMode="Password"></asp:TextBox>
            <br /><asp:Button runat="server" ID="CreateUser" Text="Create User" 
                onclick="CreateUser_Click"/>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
