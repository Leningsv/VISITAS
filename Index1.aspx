<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index1.aspx.cs" Inherits="Index1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1
        {
            width: 890px;
        }
    </style>

    <script type="text/jscript">

        function salir() {
            if (confirm("Esta seguro que desea cerrar la sesion???"))
                window.parent.location = "Frm_Login.aspx";
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">

    <div style="font-size: 14px; font-style: normal; font-weight: bold; color: #000080;">
    
        <table >
            <tr>
                <td>
                    </td>
                <td class="auto-style1" >
                    </td>
                <td>
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="return salir()">Cerrar Sesión</asp:LinkButton>    
                </td>
            </tr>
        </table>
    </div>
        <asp:Panel ID="Panel1" runat="server">
            <asp:Image ID="Image1" runat="server" Height="580px" ImageUrl="Images/Ministerio_de_justicia_bienvenida.png" Width="1024px" />
        </asp:Panel>

    </form>
</body>
</html>
