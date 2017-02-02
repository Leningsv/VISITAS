<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_Login.aspx.cs" Inherits="Frm_Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="css/login.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="JS/Master.js">

    </script>
    <style type="text/css">
        .auto-style1
        {
            width: 583px;
        }
        .auto-style2
        {
            height: 233px;
            }
        .auto-style3
        {
            color: #000000;
            text-align: left;
        }
    </style>
</head>
<body style="background-position:center 3%; background-repeat:no-repeat; ">
    <br />
        <form id="form1" runat="server">
            <table style="height: 550px" >
                <tr >
                    <td class="auto-style1"></td>
                    <td style="width:316px">
                        <div class="header"></div>
                        <center>
                            <table border="0" cellpadding="0" cellspacing="0" style="width:100%" >
                                <tr>
                                    <td align="left" valign="top" class="auto-style2">
                                    </td>
                                    <td style="width: 102%; height: 233px">
                                        <center>
                                            <table>
                                                <tr>
                                                    <td colspan="2">
                                                        <h3 style="width: 155px">
                                                            <span style="font-size:14pt" class="auto-style3">Iniciar Sesión</span><span class="auto-style3"> </span>

                                                        </h3>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width:100px; text-align: left;">
                                                        <strong><span class="auto-style3">Usuario</span>:</strong>
                                                    </td>
                                                    <td style="width:100px">
                                                        <asp:TextBox ID="txtUsuario" runat="server" Width="155px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width:100px; text-align: left;">
                                                        <strong><span class="auto-style3">Contraseña</span>:</strong>
                                                    </td>
                                                    <td style="width:100px">

                                                        <asp:TextBox ID="txtClave" runat="server" Width="155px" TextMode="Password"></asp:TextBox>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>

                                                        &nbsp;</td>
                                                    <td style="text-align: left">

                                                        <asp:Button ID="btnIngresar" runat="server" Height="29px" OnClick="btnIngresar_Click" Text="Ingresar" Width="76px" />

                                                    </td>
                                                </tr>
                                            </table>
                                        </center>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <asp:Label ID="lblError" runat="server"></asp:Label>
                            <br />
                            <asp:Label ID="lblmensaje" runat="server" BackColor="Gold" ForeColor="Black"></asp:Label>
                            <br />
                        </center>
                    </td>
                </tr>                    
            </table>
        <div>
        </div>
        </form>
</body>
</html>
