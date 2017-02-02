<%@ Page Title="" Language="C#" MasterPageFile="~/Mantenimientos.master" AutoEventWireup="true" CodeFile="frmcrsNew.aspx.cs" Inherits="CRS_frmcrsNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width: 90%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: center; text-decoration: underline;">
        <asp:Label ID="lbltitulo" runat="server"></asp:Label>
    </div>
    <hr />
    <table>
        <tr style="vertical-align: central">
            <td >
                <table runat="server" id="tblprincipal" style="width: 90%;  color: #000000;">
                    <tr>
                        <td style="text-align: left; width: 100px;">
                            <asp:Label ID="Label1" runat="server" Text="*Pais:" Visible="False"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlpais" runat="server" Width="267px" Enabled="False" Visible="False">
                            </asp:DropDownList>                            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlpais" ErrorMessage="Campor Requerido!" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 100px; height: 18px;">
                            <asp:Label ID="Label2" runat="server" Text="*Provincia:"></asp:Label>
                        </td>
                        <td style="height: 18px; text-align: left;">
                            <asp:DropDownList ID="ddlprovincia" runat="server" Width="267px" AutoPostBack="True" OnSelectedIndexChanged="ddlprovincia_SelectedIndexChanged">
                            </asp:DropDownList>                            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlprovincia" ErrorMessage="Campor Requerido!" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 100px; height: 18px;">
                            <asp:Label ID="lblnombres" runat="server" Text="*Ciudad:"></asp:Label>
                        </td>
                        <td style="height: 18px; text-align: left;">
                            <asp:DropDownList ID="ddlciudad" runat="server" Width="267px">
                            </asp:DropDownList>                            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlciudad" ErrorMessage="Campor Requerido!" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 100px; height: 18px;">
                            <asp:Label ID="Label3" runat="server" Text="*Nombre CRS:"></asp:Label>
                        </td>
                        <td style="height: 18px; text-align: left;">
                            <asp:TextBox ID="txtnombre" runat="server" CssClass="upperCase" MaxLength="80" Width="264px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtnombre" ErrorMessage="Campor Requerido!" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 100px; height: 18px;">
                            <asp:Label ID="Label4" runat="server" Text="Director:"></asp:Label>
                        </td>
                        <td style="height: 18px; text-align: left;">
                            <asp:TextBox ID="txtdirector" runat="server" CssClass="upperCase" MaxLength="80" Width="264px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 100px; height: 18px;">
                            <asp:Label ID="Label5" runat="server" Text="Dirección:"></asp:Label>
                        </td>
                        <td style="height: 18px; text-align: left;">
                            <asp:TextBox ID="txtdireccion" runat="server" MaxLength="255" Width="264px" CssClass="upperCase"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 100px; height: 18px;">
                            <asp:Label ID="Label6" runat="server" Text="Telefono1:"></asp:Label>
                        </td>
                        <td style="height: 18px; text-align: left;">
                            <asp:TextBox ID="txtfono1" runat="server" MaxLength="16" Width="264px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 100px; height: 18px;">
                            <asp:Label ID="Label7" runat="server" Text="Telefono2:"></asp:Label>
                        </td>
                        <td style="height: 18px; text-align: left;">
                            <asp:TextBox ID="txtfono2" runat="server" MaxLength="16" Width="264px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 100px; height: 18px;">
                            <asp:Label ID="Label8" runat="server" Text="Celular:"></asp:Label>
                        </td>
                        <td style="height: 18px; text-align: left;">
                            <asp:TextBox ID="txtcelular" runat="server" MaxLength="16" Width="264px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 100px; height: 18px;">
                            <asp:Label ID="Label11" runat="server" Text="Estado:"></asp:Label>
                        </td>
                        <td style="height: 18px; text-align: left;">
                            <asp:CheckBox ID="chkestado" runat="server" Checked="True" Enabled="False" Text="Activo" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; height: 18px;" colspan="2">
                <asp:Label ID="lblerror" runat="server" Text="Ya existe CRS Creado, por favor ingrese otro" ForeColor="Red" Visible="False"></asp:Label>
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

