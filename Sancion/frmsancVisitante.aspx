<%@ Page Title="" Language="C#" MasterPageFile="~/Mantenimientos.master" AutoEventWireup="true" CodeFile="frmsancVisitante.aspx.cs" Inherits="Sancion_frmsancVisitante" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" src="../JS/MantVal.js"></script>
    <script type="text/javascript" src="../JS/jquery-1.5.2.min.js"></script>
    <div style="width: 100%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: center; text-decoration: underline;">
        <asp:Label ID="lbltitulo" runat="server"></asp:Label>
    </div>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <hr />
    <div style="width: 100%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: left; text-decoration: underline;">
        <asp:Label ID="lbletiqueta" runat="server" Font-Size="12pt">datos del visitante</asp:Label>
    </div>
        <table style="width: 100%; color:black">
            <tr>
                <td></td>
                <td style="width:100%">

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>

                            <table style="width: 100%; color:black">
                                <tr>
                                    <td rowspan="8">
                                        <asp:Image ID="imgfoto" runat="server" Height="261px" Width="254px" ImageUrl="~/Images/sinfoto.jpg" />
                                    </td>
                                    <td style="text-align: left; width: 21px;">
                                        &nbsp;</td>
                                    <td style="text-align: left; width: 145px;">
                                        <asp:Label ID="Label1" runat="server" Text="Tipo Documento:"></asp:Label>
                                    </td>
                                    <td style="text-align: left">

                                        <asp:DropDownList ID="ddltipodoc" runat="server" AutoPostBack="True" Width="267px" Enabled="False">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: left">

                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 21px;">
                                        &nbsp;</td>
                                    <td style="text-align: left; width: 145px;">
                                        <asp:Label ID="Label2" runat="server" Text="No. Documento:"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtndocu" runat="server" MaxLength="16" Width="264px" Enabled="False"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="txtndocu_FilteredTextBoxExtender" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtndocu">
                                        </asp:FilteredTextBoxExtender>
                                    </td>
                                    <td style="text-align: left">
                                        &nbsp;</td>
                                   
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 21px;">
                                        &nbsp;</td>
                                    <td style="text-align: left; width: 145px;">
                                        <asp:Label ID="Label5" runat="server" Text="Apellidos/Nombres:"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtnombre1" runat="server" CssClass="upperCase" MaxLength="30" Width="264px" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left">
                                        &nbsp;</td>
                                    
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 21px;">
                                        &nbsp;</td>
                                    <td style="text-align: left; width: 145px;">
                                        <asp:Label ID="Label14" runat="server" Text="Género:"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:DropDownList ID="ddlgenero" runat="server" Width="267px" Enabled="False">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: left">
                                        &nbsp;</td>
                                    
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 21px;">
                                        &nbsp;</td>
                                    <td style="text-align: left; width: 145px;">
                                        <asp:Label ID="Label10" runat="server" Text="Observación:"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtobserva" runat="server" CssClass="upperCase" TextMode="MultiLine" Width="264px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left">
                                        &nbsp;</td>
                                    
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 21px;">&nbsp;</td>
                                    <td style="text-align: left; width: 145px;">
                                        &nbsp;</td>
                                    <td style="text-align: left">
                                        &nbsp;</td>
                                    <td style="text-align: left">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 21px;">&nbsp;</td>
                                    <td style="text-align: left; width: 145px;">
                                        <asp:Label ID="Label3" runat="server" Text="*Apellido Paterno:" Visible="False"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtapellido1" runat="server" CssClass="upperCase" MaxLength="30" Visible="False" Width="264px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 21px;">
                                        &nbsp;</td>
                                    <td style="text-align: left; width: 145px;">
                                        <asp:Label ID="Label4" runat="server" Text="Apellido Materno:" Visible="False"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtapellido2" runat="server" CssClass="upperCase" MaxLength="30" Visible="False" Width="264px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left">&nbsp;</td>
                                    
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td style="text-align: left; width: 21px;">&nbsp;</td>
                                    <td style="text-align: left; width: 145px;">
                                        <asp:Label ID="Label6" runat="server" Text="Segundo Nombre:" Visible="False"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtnombre2" runat="server" CssClass="upperCase" MaxLength="30" Visible="False" Width="264px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </td>
                <td></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="width:100%">

                    <asp:Label ID="lblmsj1" runat="server" Text="Label" ForeColor="Red" Visible="False"></asp:Label>

                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="width:100%; text-align: left;">

                    <asp:Label ID="lblmsj2" runat="server" Text="Label" ForeColor="#000099" Visible="False"></asp:Label>

                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <div id="menuaccions" style="border-style: ridge double double ridge; width: 100%; background-color: #FFFFFF; border-top-width: 4px; border-top-color: #FF0000; border-left-color: #FF0000;">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 1590px">
                                    <asp:ImageButton ID="btningresar" runat="server" Height="60px" ImageUrl="~/Botones/Permitir_Ingreso.png" OnClick="btningresar_Click" />
                                </td>
                                <td style="width: 966px">
                                    <asp:ImageButton ID="btnsancionar" runat="server" Height="60px" ImageUrl="~/Botones/Sancionar.png" OnClick="btnsancionar_Click" />
                                </td>
                                <td style="width: 899px">
                                    &nbsp;</td>
                                <td style="width:312px">
                                    <asp:ImageButton ID="btnsalir" runat="server" Height="60px" ImageUrl="~/Botones/Salir.png" CausesValidation="False" OnClick="btnsalir_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td></td>
            </tr>
        </table>
</asp:Content>

