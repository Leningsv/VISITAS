<%@ Page Title="" Language="C#" MasterPageFile="~/Mantenimientos.master" AutoEventWireup="true" CodeFile="frmusuDesbloEdit.aspx.cs" Inherits="UsuarioN_frmusuDesbloEdit" %>

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
                        <td style="text-align: left; width: 138px; height: 18px;">
                            <asp:Label ID="Label5" runat="server" Text="Observación:"></asp:Label>
                        </td>
                        <td style="height: 18px; text-align: left;">
                            <asp:TextBox ID="txtobservacion" runat="server" TextMode="MultiLine" Width="264px" CssClass="upperCase" Height="47px" MaxLength="255"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 138px;">
                            &nbsp;</td>
                        <td style="text-align: left">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtobservacion" ErrorMessage="Ingrese Observación!" ForeColor="Red"></asp:RequiredFieldValidator>
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
</asp:Content>

