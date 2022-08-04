<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication1.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Page</title>
</head>
<body>
    <script type="text/javascript" src="Script/JavaScript/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="Script/JavaScript/bootstrap.min.js"></script>
    <link rel="stylesheet" href="Script/CSS/bootstrap.min.css" />
    <form id="form1" runat="server">
    <div style="max-width: 400px;">
        <h2 class="form-signin-heading">
            Login</h2>
        <label for="txtUsername">
            Username</label>
        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Enter Username"
            required />
        <br />
        <label for="txtPassword">
            Password</label>
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"
            placeholder="Enter Password" required />
        <div class="checkbox">
            <asp:CheckBox ID="chkRememberMe" Text="Remember Me" runat="server" />
        </div>
        <asp:Button ID="btnLogin" Text="Login" runat="server" OnClick="ValidateUser" Class="btn btn-primary" />
        <br />
        <br />
        <div id="dvMessage" runat="server" visible="false" class="alert alert-danger">
            <strong>Error!</strong>
            <asp:Label ID="lblMessage" runat="server" />
        </div>
    </div>
    </form>
</body>
</html>
