<%@ Page Title="" Language="C#" MasterPageFile="~/Mantenimientos.master" AutoEventWireup="true" CodeFile="frmrelacionparenNew.aspx.cs" Inherits="RelacionParentesco_frmrelacionparenNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width: 100%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: center; text-decoration: underline;">
        <asp:Label ID="lbltitulo" runat="server"></asp:Label>
    </div>
    <hr />
    <table >
        <tr style="vertical-align: central">
            <td>
                <table runat="server" id="tblprincipal" style="width: 100%;  color: #000000;">
                    <tr>
                        <td style="text-align: left; width: 138px;">
                            <asp:Label ID="Label1" runat="server" Text="Parentesco:"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlparentesco" runat="server" Width="267px">
                            </asp:DropDownList>                            
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 138px; height: 18px;">
                            <asp:Label ID="lblperfil" runat="server" Text="Relación Tipo Visita:"></asp:Label>
                        </td>
                        <td style="height: 18px; text-align: left;">
                            <asp:DropDownList ID="ddltipovisita" runat="server" Width="267px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; height: 18px;" colspan="2">
                <asp:Label ID="lblerror" runat="server" Text="Ya existe una relación establecida, por favor ingrese otra" ForeColor="Red" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    </table>
            </td>

        </tr>
        <tr >
            <td style="width: 100%">
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

