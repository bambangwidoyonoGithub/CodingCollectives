<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebApplication1.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Index Page</title>
</head>
<body>
    <script type="text/javascript" src="Script/JavaScript/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="Script/JavaScript/bootstrap.min.js"></script>
    <script type="text/javascript" src="Script/JavaScript/jquery.min.js" ></script>
    <script type="text/javascript" src="Script/JavaScript/jquery.dataTables.min.js"></script>
    <link rel="stylesheet" href="Script/CSS/bootstrap.min.css" />
    <link  rel="stylesheet" type="text/css" href="Script/CSS/jquery.dataTables.css"/>
    <link  rel="stylesheet" type="text/css" href="Script/CSS/jquery.dataTables.min.css"/>
    
    <script type="text/javascript">
        
        $(function () {
            $.ajax({
                type: "GET",
                url: "Index.aspx/GetListEmployeeFix",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    alert(response);
                },
                error: function (response) {
                    alert(response);
                }
            });
        });
        function OnSuccess(response) {
            alert("Error: "+response.d);
            alert("Error: "+response.d.EmployeeName);
            $("#gvEmployee").DataTable(
            {
                bLengthChange: true,
                lengthMenu: [[5, 10, -1], [5, 10, "All"]],
                bFilter: true,
                bSort: true,
                bPaginate: true,
                data: response.d,
                columns: [{ 'data': 'EmployeeName' },
                          { 'data': 'Jobtitle' },
                          { 'data': 'Salary'}]
            });
        };
        
    </script>
    <form id="form1" runat="server" class="tab-content">
        <div class="navbar-header">
        <h2 class="form-signin-heading">Welcome</h2>
        <h3 class="form-signin-heading"><asp:Label ID="lblUser" runat="server" /></h3>
            <div class="top-right">
            <asp:Button ID="btnLogout" Text="Logout" runat="server" OnClick="LogOut" Class="btn btn-danger"/>
            </div>
        <br />
        <br />
            <div style="max-width: 700px;">
                <asp:GridView ID="gvEmployee" runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="EmployeeName" HeaderText="EmployeeName" />
                        <asp:BoundField DataField="Jobtitle" HeaderText="Jobtitle" />
                        <asp:BoundField DataField="Salary" HeaderText="Salary" />
                    </Columns>
                </asp:GridView>
            </div>
            <br />
        </div>
    </form>
</body>
</html>
