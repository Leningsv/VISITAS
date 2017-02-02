<%@ Page Title="" Language="C#" MasterPageFile="~/Mantenimientos.master" AutoEventWireup="true" CodeFile="frmciuEdit.aspx.cs" Inherits="CiudadN_frmciuEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width: 90%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: center; text-decoration: underline;">
        <asp:Label ID="lbltitulo" runat="server"></asp:Label>
    </div>
    <hr />
    <table>
        <tr style="vertical-align: central">
            <td>
                <table runat="server" id="tblprincipal" style="width: 93%;  color: #000000;">
                    <tr>
                        <td style="text-align: left; width: 52px;">
                            <asp:Label ID="Label3" runat="server" Text="Código:" Visible="False"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtcodigo" runat="server" Height="19px" Width="264px" MaxLength="80" Enabled="False" Visible="False" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 52px;">
                            <asp:Label ID="Label1" runat="server" Text="*Pais:"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlpais" runat="server" Width="267px" Enabled="False">
                            </asp:DropDownList>                            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlpais" ErrorMessage="Campor Requerido!" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 52px; height: 18px;">
                            <asp:Label ID="Label2" runat="server" Text="*Provincia:"></asp:Label>
                        </td>
                        <td style="height: 18px; text-align: left;">
                            <asp:DropDownList ID="ddlprovincia" runat="server" Width="267px">
                            </asp:DropDownList>                            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlprovincia" ErrorMessage="Campor Requerido!" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 52px; height: 18px;">
                            <asp:Label ID="lblnombres" runat="server" Text="*Ciudad:"></asp:Label>
                        </td>
                        <td style="height: 18px; text-align: left;">
                            <asp:TextBox ID="txtciudad" runat="server" Height="19px" Width="264px" MaxLength="80" CssClass="upperCase" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtciudad" ErrorMessage="Campor Requerido!" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 52px; height: 18px;">
                            <asp:Label ID="lblnombres0" runat="server" Text="Region:"></asp:Label>
                        </td>
                        <td style="height: 18px; text-align: left;">
                            <asp:DropDownList ID="ddlregion" runat="server" Width="267px">
                                <asp:ListItem Value="1">SIERRA</asp:ListItem>
                                <asp:ListItem Value="2">COSTA</asp:ListItem>
                                <asp:ListItem Value="3">ORIENTE</asp:ListItem>
                                <asp:ListItem Value="4">GALAPAGOS</asp:ListItem>
                            </asp:DropDownList>                            
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; height: 18px;" colspan="2">
                <asp:Label ID="lblerror" runat="server" Text="Ya existe la Ciudad Creada, por favor ingrese otra" ForeColor="Red" Visible="False"></asp:Label>
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

