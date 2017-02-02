<%@ Page Title="" Language="C#" MasterPageFile="~/Mantenimientos.master" AutoEventWireup="true" CodeFile="frmdepNew.aspx.cs" Inherits="Departamento_frmdepNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width: 100%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: center; text-decoration: underline;">
        <asp:Label ID="lbltitulo" runat="server"></asp:Label>
    </div>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <hr />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <table>
        <tr style="vertical-align: central">
            <td>
                <table runat="server" id="tblprincipal" style="width: 93%;  color: #000000;">
                    <tr>
                        <td style="text-align: left; width: 106px;">
                            <asp:Label ID="Label1" runat="server" Text="*Departamento:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 437px;">
                            <asp:TextBox ID="txtdepartamento" runat="server" Width="264px" MaxLength="80" CssClass="upperCase" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtdepartamento" ErrorMessage="Campo Requerido..!" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 106px;">
                            <asp:Label ID="Label3" runat="server" Text="Estado:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 437px;">
                            <asp:CheckBox ID="chkestado" runat="server" Checked="True" Enabled="False" Text="Activo" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center; ">
                            <asp:Label ID="lblerror" runat="server" ForeColor="Red" Text="Ya existe el Departamento Creado, por favor Cree otro " Visible="False"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>

        </tr>
        <tr >
            <td style="width: 100%">
                <div id="menuaccions" class="menuaccions">
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <asp:ImageButton ID="btngrabar" runat="server" Height="60px" ImageUrl="~/Botones/Grabar.png" OnClick="btngrabar_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="btnsalir" runat="server" Height="60px" ImageUrl="~/Botones/Salir.png" CausesValidation="False" OnClick="btnsalir_Click"  />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        
    </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

