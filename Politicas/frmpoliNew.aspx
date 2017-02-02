<%@ Page Title="" Language="C#" MasterPageFile="~/Mantenimientos.master" AutoEventWireup="true" CodeFile="frmpoliNew.aspx.cs" Inherits="Politicas_frmpoliNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width: 90%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: center; text-decoration: underline;">
        <asp:Label ID="lbltitulo" runat="server"></asp:Label>
    </div>
    <hr />
    <table>
        <tr style="vertical-align: central">
            <td>
                <table runat="server" id="tblprincipal" style="color: #000000;">
                    <tr>
                        <td style="text-align: left; width: 137px;">
                            <asp:Label ID="Label1" runat="server" Text="*Política:"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtnombre" runat="server" Width="264px" MaxLength="80" CssClass="upperCase" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtnombre" ErrorMessage="Campo Requerido..!" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 137px; height: 18px;">
                            <asp:Label ID="Label2" runat="server" Text="*Descripción:"></asp:Label>
                        </td>
                        <td style="height: 18px; text-align: left;">
                            <asp:TextBox ID="txtdescri" runat="server" Width="264px" MaxLength="255" CssClass="upperCase" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtdescri" ErrorMessage="Campo Requerido..!" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 137px; height: 18px;">
                            <asp:CheckBox ID="chkeliminar" runat="server" Enabled="False" Text="Permitir Eliminar" />
                        </td>
                        <td style="height: 18px; text-align: left;">
                            <asp:CheckBox ID="chkcrear" runat="server" Enabled="False" Text="Permitir Crear" />
                            <asp:CheckBox ID="chkmodificar" runat="server" Enabled="False" Text="Permite Modificar" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 137px;">
                            <asp:Label ID="Label3" runat="server" Text="Estado:"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:CheckBox ID="chkestado" runat="server" AutoPostBack="True" Checked="True" Enabled="False" Text="Activo" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; " colspan="2">
                <asp:Label ID="lblerror" runat="server" Text="Ya existe Política Creada, Por favor Cree otra" ForeColor="Red" Visible="False"></asp:Label>
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
                            <td style="width: 385px">
                                <asp:ImageButton ID="btngrabar" runat="server" Height="60px" ImageUrl="~/Botones/Grabar.png" OnClick="btngrabar_Click" />
                            </td>
                            <td style="width: 133px">
                                &nbsp;</td>
                            <td style="width:312px">
                                <asp:ImageButton ID="btnsalir" runat="server" Height="60px" ImageUrl="~/Botones/Salir.png" CausesValidation="False" OnClick="btnsalir_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>        
    </table>
</asp:Content>

