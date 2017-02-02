<%@ Page Title="" Language="C#" MasterPageFile="~/Mantenimientos.master" AutoEventWireup="true" CodeFile="frmusuClave.aspx.cs" Inherits="UsuarioN_frmusuClave" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div style="width: 90%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: center; text-decoration: underline;">
        <asp:Label ID="lbltitulo" runat="server"></asp:Label>
    </div>
    <hr />
    <table>
        <tr style="vertical-align: top">
            <td>
                <table runat="server" id="tblprincipal" style="width: 92%;  color: #000000;">
                    <tr>
                        <td style="text-align: left; width: 142px;">
                            &nbsp;</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtcodigo" runat="server" Height="19px" Width="264px" MaxLength="80" Enabled="False" Visible="False" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 142px; height: 18px;">
                            <asp:Label ID="lblusuario" runat="server" Text="*Contraseña anterior:"></asp:Label>
                        </td>
                        <td style="height: 18px; text-align: left;">
                            <asp:TextBox ID="txtcanterior" runat="server" Height="19px" Width="264px" MaxLength="16" TextMode="Password"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtcanterior" ErrorMessage="Campo Requerido!" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 142px; height: 18px;">
                            <asp:Label ID="lblcontrasenia" runat="server" Text="*Nueva contraseña:"></asp:Label>
                        </td>
                        <td style="height: 18px; text-align: left;">
                            <asp:TextBox ID="txtcnueva" runat="server" Height="19px" Width="264px" MaxLength="16" TextMode="Password"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtcnueva" ErrorMessage="Campo Requerido!" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 142px; height: 18px;">
                            <asp:Label ID="lblperfil" runat="server" Text="*Verificar Contraseña:"></asp:Label>
                        </td>
                        <td style="height: 18px; text-align: left;">
                            <asp:TextBox ID="txtcverificar" runat="server" Height="19px" Width="264px" MaxLength="16" TextMode="Password"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtcverificar" ErrorMessage="Campo Requerido!" ForeColor="Red"></asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 142px;">
                            &nbsp;</td>
                        <td style="text-align: left">
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtcnueva" ControlToValidate="txtcverificar" ErrorMessage="Las contraseñas no coinciden" ForeColor="Red"></asp:CompareValidator>
                        </td>
                    </tr>
                </table>
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

