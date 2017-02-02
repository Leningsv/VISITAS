<%@ Page Title="" Language="C#" MasterPageFile="~/Mantenimientos.master" AutoEventWireup="true" CodeFile="frmusuEdit.aspx.cs" Inherits="UsuarioN_frmusuEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width: 90%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: center; text-decoration: underline;">
        <asp:Label ID="lbltitulo" runat="server"></asp:Label>
    </div>
    <hr />
    <table>
        <tr style="vertical-align: top">
            <td>
                <table runat="server" id="tblprincipal" style="width: 90%;  color: #000000;">
                    <tr>
                        <td style="text-align: left; width: 138px;">
                            &nbsp;</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtcodigo" runat="server" Height="19px" Width="264px" MaxLength="80" Enabled="False" Visible="False" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 138px;">
                            <asp:Label ID="Label1" runat="server" Text="Elija Departamento:"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddldepartamento" runat="server" Width="267px">
                            </asp:DropDownList>                            
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 138px; height: 18px;">
                            <asp:Label ID="lblnombres" runat="server" Text="*Nombres:"></asp:Label>
                        </td>
                        <td style="height: 18px; text-align: left;">
                            <asp:TextBox ID="txtnombres" runat="server" Height="19px" Width="264px" MaxLength="80" CssClass="upperCase" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtnombres" ErrorMessage="Campor Requerido!" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 138px; height: 18px;">
                            <asp:Label ID="lblapellidos" runat="server" Text="*Apellidos:"></asp:Label>
                        </td>
                        <td style="height: 18px; text-align: left;">
                            <asp:TextBox ID="txtapellidos" runat="server" Height="19px" Width="264px" MaxLength="80" CssClass="upperCase"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtapellidos" ErrorMessage="Campor Requerido!" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 138px; height: 18px;">
                            <asp:Label ID="lblusuario" runat="server" Text="*Nombre de Usuario:"></asp:Label>
                        </td>
                        <td style="height: 18px; text-align: left;">
                            <asp:TextBox ID="txtusuario" runat="server" Height="19px" Width="264px" MaxLength="16"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtusuario" ErrorMessage="Campor Requerido!" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 138px; height: 18px;">
                            <asp:Label ID="lblcontrasenia" runat="server" Text="*Contraseña:"></asp:Label>
                        </td>
                        <td style="height: 18px; text-align: left;">
                            <asp:TextBox ID="txtcontrasenia" runat="server" Height="19px" Width="264px" MaxLength="16" TextMode="Password"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtcontrasenia" ErrorMessage="Campor Requerido!" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 138px; height: 18px;">
                            <asp:Label ID="lblperfil" runat="server" Text="*Perfil:"></asp:Label>
                        </td>
                        <td style="height: 18px; text-align: left;">
                            <asp:DropDownList ID="ddlperfil" runat="server" Width="267px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 138px;">
                            <asp:Label ID="Label3" runat="server" Text="Estado:"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:CheckBox ID="chkestado" runat="server" AutoPostBack="True" OnCheckedChanged="chkestado_CheckedChanged" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 138px;">
                            <asp:Label ID="Label4" runat="server" Text="Eliminar Huellas:"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:CheckBox ID="chkeliminar" runat="server" AutoPostBack="True" OnCheckedChanged="chkeliminar_CheckedChanged" Text="NO" />
                        </td>
                    </tr>
                </table>
                <asp:Label ID="lblerror" runat="server" Text="Ya existe Usuario Creado, por favor ingrese otro " ForeColor="Red" Visible="False"></asp:Label>
            </td>

        </tr>
        <tr >
            <td style="width: 90%">
                <div id="menuaccions" style="border-style: ridge double double ridge; width: 100%; background-color: #FFFFFF; border-top-width: 4px; border-top-color: #FF0000; border-left-color: #FF0000;">
                    <table style="width: 100%">
                        <tr>
                            <td>

                                <asp:ImageButton ID="btngrabar" runat="server" Height="60px" ImageUrl="~/Botones/Grabar.png" OnClick="btngrabar_Click" />

                            </td>
                            <td>

                                <asp:ImageButton ID="btnsalir" runat="server" Height="60px" ImageUrl="~/Botones/Salir.png" CausesValidation="False" OnClick="btnsalir_Click" />

                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>        
    </table>
    <script type="text/javascript">
        function isNumberKey(strcampo) {
            //document.getElementById(strcampo).value
            if (event.keyCode > 48 && event.keyCode < 58 || event.keyCode > 64 && event.keyCode < 91
                || event.keyCode > 96 && event.keyCode < 123) {
                event.returnValue = true;
            }
            else {
                alert("No se aceptan carácteres especiales");
                event.returnValue = false;
            }
        }
  </script>
</asp:Content>

