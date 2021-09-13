<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="Library_Management_System.HomePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="User Registration" Font-Bold="True"></asp:Label>
        </div>
        <asp:Label ID="Label2" runat="server" Text="Name"></asp:Label>
        <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
        <asp:Label ID="Label3" runat="server" Text="Email"></asp:Label>
        <asp:TextBox ID="UserEmail" runat="server"></asp:TextBox>
        <asp:Label ID="registrationerror" runat="server" ForeColor="Red" Text="You are already registered" Visible="False"></asp:Label>
        <p>
            <asp:Button ID="Registration_Btn" runat="server" OnClick="Registration_Btn_Click" Text="Sign Up" />
            <asp:Label ID="Signupmsg" runat="server" ForeColor="#009933" Text="Your are Successfully Sign Up" Visible="False"></asp:Label>
        </p>
        <asp:Label ID="Label4" runat="server" Text="Login" Font-Bold="True"></asp:Label>
        <p>
            <asp:Label ID="Label5" runat="server" Text="Email"></asp:Label>
            <asp:TextBox ID="loginemail" runat="server"></asp:TextBox>
            <asp:Label ID="yourarenotregister" runat="server" Text="Your are not register" Visible="False" ForeColor="Red"></asp:Label>
        </p>
        <asp:Button ID="Sign_In" runat="server" Text="Sign In" OnClick="Sign_In_Click" />
        <p>
            <asp:Button ID="Admin_Page" runat="server" Height="52px" OnClick="Admin_Page_Click" Text="Admin Page" Width="120px" />
        </p>
    </form>
</body>
</html>
