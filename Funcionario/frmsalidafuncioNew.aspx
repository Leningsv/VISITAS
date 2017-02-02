<%@ Page Title="" Language="C#" MasterPageFile="~/Mantenimientos.master" AutoEventWireup="true" CodeFile="frmsalidafuncioNew.aspx.cs" Inherits="Funcionario_frmsalidafuncioNew" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width: 100%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: center; text-decoration: underline;">
        <asp:Label ID="lbltitulo" runat="server"></asp:Label>
    </div>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <hr />
    <div style="width: 100%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: left; text-decoration: underline;">
        <asp:Label ID="Label17" runat="server" Font-Size="12pt">datos funcionario/visitante</asp:Label>
    </div>
        <table style="width: 100%; color:black">
            <tr>
                <td></td>
                <td style="width:90%">

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>

                            <table style="width: 100%; color:black">
                                <tr>
                                    <td rowspan="13">
                                        <asp:Image ID="imgfoto" runat="server" Height="261px" Width="254px" ImageUrl="~/Images/sinfoto.jpg" />
                                    </td>
                                    <td style="text-align: left; width: 21px;">
                                        &nbsp;</td>
                                    <td style="text-align: left; width: 145px;">
                                        <asp:Label ID="Label26" runat="server" Text="Código:" Visible="False"></asp:Label>
                                    </td>
                                    <td style="text-align: left">

                                        <asp:TextBox ID="txtcodigo" runat="server" MaxLength="16" Width="264px" Enabled="False" Visible="False"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="txtcodigo_FilteredTextBoxExtender" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtcodigo">
                                        </asp:FilteredTextBoxExtender>

                                     </td>
                                    <td style="text-align: left">

                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 21px;">&nbsp;</td>
                                    <td style="text-align: left; width: 145px;">
                                        <asp:Label ID="Label1" runat="server" Text="Tipo Documento:"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:DropDownList ID="ddltipodoc" runat="server" Width="267px" Enabled="False">
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
                                        <asp:Label ID="Label5" runat="server" Text="Primer Nombre:"></asp:Label>
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
                                        <asp:Label ID="Label6" runat="server" Text="Segundo Nombre:"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtnombre2" runat="server" CssClass="upperCase" MaxLength="30" Width="264px" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left">
                                        &nbsp;</td>
                                    
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 21px;">
                                        &nbsp;</td>
                                    <td style="text-align: left; width: 145px;">
                                        <asp:Label ID="Label3" runat="server" Text="Apellido Paterno:"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtapellido1" runat="server" CssClass="upperCase" MaxLength="30" Width="264px" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left">
                                        &nbsp;</td>
                                    
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 21px;">
                                        &nbsp;</td>
                                    <td style="text-align: left; width: 145px;">
                                        <asp:Label ID="Label4" runat="server" Text="Apellido Materno:"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtapellido2" runat="server" CssClass="upperCase" MaxLength="30" Width="264px" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left">
                                        &nbsp;</td>
                                    
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 21px;">&nbsp;</td>
                                    <td style="text-align: left; width: 145px;">
                                        <asp:Label ID="Label19" runat="server" Text="Departamento:"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:DropDownList ID="ddldepartamento" runat="server" Width="267px" Enabled="False">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: left">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 21px;">&nbsp;</td>
                                    <td style="text-align: left; width: 145px;">
                                        <asp:Label ID="Label20" runat="server" Text="Cargo:"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:DropDownList ID="ddlcargo" runat="server" Width="267px" Enabled="False">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: left">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 21px;">&nbsp;</td>
                                    <td style="text-align: left; width: 145px;">
                                        <asp:Label ID="Label27" runat="server" Text="Fecha Ingreso:"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtfechainicio" runat="server" Width="264px" Enabled="False"></asp:TextBox>
                                        <asp:MaskedEditExtender ID="txtfechainicio_MaskedEditExtender" runat="server" ClearMaskOnLostFocus="false" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" TargetControlID="txtfechainicio">
                                        </asp:MaskedEditExtender>
                                        <asp:CalendarExtender ID="txtfechainicio_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtfechainicio">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td style="text-align: left">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 21px;">&nbsp;</td>
                                    <td style="text-align: left; width: 145px;">
                                        <asp:Label ID="Label28" runat="server" Text="Fecha Salida:"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtfechafin" runat="server" Width="264px" Enabled="False"></asp:TextBox>
                                        <asp:MaskedEditExtender ID="txtfechafin_MaskedEditExtender" runat="server" ClearMaskOnLostFocus="false" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" TargetControlID="txtfechafin">
                                        </asp:MaskedEditExtender>
                                        <asp:CalendarExtender ID="txtfechafin_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtfechafin">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td style="text-align: left">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 21px; height: 40px;"></td>
                                    <td style="text-align: left; width: 145px; height: 40px;">
                                        <asp:Label ID="Label10" runat="server" Text="Observación:"></asp:Label>
                                    </td>
                                    <td style="text-align: left; height: 40px;">
                                        <asp:TextBox ID="txtobserva" runat="server" CssClass="upperCase" MaxLength="255" Width="264px" TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left; height: 40px;"></td>
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
                                    <td>
                                        &nbsp;</td>
                                    <td style="text-align: left; width: 21px;">&nbsp;</td>
                                    <td style="text-align: left; width: 145px;">
                                        &nbsp;</td>
                                    <td style="text-align: left">
                                        &nbsp;</td>
                                </tr>
                            </table>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </td>
                <td></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="width:90%">

                    <asp:Label ID="lblmsj1" runat="server" Text="Label" ForeColor="Red" Visible="False"></asp:Label>

                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="width:90%; text-align: left;">

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
                                <td style="width: 1635px">
                                    <asp:ImageButton ID="btningresar" runat="server" Height="50px" ImageUrl="~/Botones/permitir_salida.png" OnClick="btningresar_Click" />
                                </td>
                                <td style="width: 937px">
                                    &nbsp;</td>
                                <td style="width: 855px">
                                    <asp:ImageButton ID="btnsalir" runat="server" Height="60px" ImageUrl="~/Botones/Salir.png" CausesValidation="False" OnClick="btnsalir_Click" />
                                </td>
                                <td style="width:312px">
                                    &nbsp;</td>
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

