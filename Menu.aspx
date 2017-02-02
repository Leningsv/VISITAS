<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Menu.aspx.cs" Inherits="Menu" Theme="admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript" src="JS/Master.js">
    </script>
    <script type="text/javascript">
        window.onload = window_onload;
    </script>
    <style type="text/css">
        .auto-style1
        {
            width: 200px;
            height: 40px;
        }
    </style>
</head>
<body style="text-align: left; background-image:url('Images/banner_lateral.jpg')">
<%--<body>--%>
    <form id="form1" runat="server">
    <div style="text-align:left" >
        <table border="0" cellpadding="0" cellspacing="0" style="border-style: none; background-image: url('Images/banner_lateral.jpg'); height: 100%; width: 200px;">
            <tr><td></td></tr><tr><td></td></tr>
            <tr>
                <td style="width: 200px; text-align: center">
                    <asp:Image ID="Image1" runat="server" Height="32px" ImageUrl="~/Images/isologotipo.png"
                        Width="40px" />
                </td>
            </tr>
            <tr>
                <td style="text-align: center; " class="auto-style1">
                    <br />
                    <h2 style="left: 16px; width: 186px; top: 56px; text-align: center; color: #20365F;">
                        Sistema de Visitas</h2>
                    
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td style="width: 200px; vertical-align: top; height: 348px; text-align: left;">
                    <asp:TreeView id="trvmenu" runat="server" SkinID="trvmain" OnSelectedNodeChanged="trvmenu_SelectedNodeChanged">
                    </asp:TreeView>

                </td>
            </tr>
            <tr>
                <td style="width: 200px; height: auto; text-align: center; color: #20365F;">
                    <strong>Usuario:</strong><br />
                    &nbsp;<asp:Label ID="lblUser" runat="server"></asp:Label><br />
                    <br />
                    <asp:Label ID="lblFecha" runat="server"></asp:Label></td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
