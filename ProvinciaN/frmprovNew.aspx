<%@ Page Title="" Language="C#" MasterPageFile="~/Mantenimientos.master" AutoEventWireup="true" CodeFile="frmprovNew.aspx.cs" Inherits="ProvinciaN_frmprovNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width: 90%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: center; text-decoration: underline;">
        <asp:Label ID="lbltitulo" runat="server"></asp:Label>
    </div>
    <hr />
    <table>
        <tr style="vertical-align: central">
            <td>
                <table runat="server" id="tblprincipal" style="width: 91%;  color: #000000;">
                    <tr>
                        <td style="text-align: left; width: 72px;">
                            <asp:Label ID="Label1" runat="server" Text="*Pais:"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlpais" runat="server" Width="267px">
                            </asp:DropDownList>                            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlpais" ErrorMessage="Campor Requerido!" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 72px; height: 18px;">
                            <asp:Label ID="lblnombres" runat="server" Text="*Provincia:"></asp:Label>
                        </td>
                        <td style="height: 18px; text-align: left;">
                            <asp:TextBox ID="txtprovincia" runat="server" Height="19px" Width="264px" MaxLength="80" CssClass="upperCase" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtprovincia" ErrorMessage="Campor Requerido!" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; height: 18px;" colspan="2">
                <asp:Label ID="lblerror" runat="server" Text="Ya existe Provincia Creada, por favor ingrese otra" ForeColor="Red" Visible="False"></asp:Label>
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

