<%@ Page Title="" Language="C#" MasterPageFile="~/Mantenimientos.master" AutoEventWireup="true" CodeFile="frmturNew.aspx.cs" Inherits="Planificador_frmturNew" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width: 90%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: center; text-decoration: underline;">
        <asp:Label ID="lbltitulo" runat="server"></asp:Label>
    </div>
    <ajaxToolkit:ToolkitScriptManager ID="smmantenimiento" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <hr />
    <table>
        <tr style="vertical-align: central">
            <td>
                <table runat="server" id="tblprincipal" style="width: 100%;  color: #000000;">
                    <tr>
                        <td style="text-align: left; width: 97px;">
                            <asp:Label ID="Label1" runat="server" Text="*Descripción:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 402px;">
                            <asp:TextBox ID="txtDescripcion" runat="server" Width="264px" MaxLength="16" CssClass="upperCase" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDescripcion" ErrorMessage="Campo Requerido..!" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 97px;">
                            <asp:Label ID="Label4" runat="server" Text="*Hora Desde:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 402px;">
                            <asp:TextBox ID="txtHoraIni" runat="server" Height="19px" MaxLength="16" Width="40px"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="txtHoraIni_MaskedEditExtender" runat="server" ClearMaskOnLostFocus="False" ClearTextOnInvalid="True" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtHoraIni">
                            </ajaxToolkit:MaskedEditExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtHoraIni" ErrorMessage="Campo Requerido..!" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 97px;">
                            <asp:Label ID="Label5" runat="server" Text="*Hora Hasta:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 402px;">
                            <asp:TextBox ID="txtHoraFin" runat="server" Height="19px" MaxLength="16" Width="40px"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="txtHoraFin_MaskedEditExtender" runat="server" ClearMaskOnLostFocus="False" ClearTextOnInvalid="True" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtHoraFin">
                            </ajaxToolkit:MaskedEditExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtHoraFin" ErrorMessage="Campo Requerido..!" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 97px;">
                            <asp:Label ID="Label6" runat="server" Text="Estado:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 402px;">
                            <asp:CheckBox ID="chkestado" runat="server" Checked="True" Text="Activo" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; " colspan="2">
                <asp:Label ID="lblerror" runat="server" Text="Ya existe el turno" ForeColor="Red" Visible="False"></asp:Label>
                        </td>
                    </tr>
                </table>
    <hr />
                <br />
            </td>

        </tr>
        <tr >
            <td style="width: 90%">
                <div id="menuaccions" class="menuaccions">
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <asp:ImageButton ID="btngrabar" runat="server" Height="60px" ImageUrl="~/Botones/Grabar.png" OnClick="btngrabar_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="btEliminarPla" runat="server" Height="60px" ImageUrl="~/Botones/eliminar.png" OnClick="btEliminarPla_Click" Visible="False" />
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
</asp:Content>

