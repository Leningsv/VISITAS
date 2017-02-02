<%@ Page Title="" Language="C#" MasterPageFile="~/Mantenimientos.master" AutoEventWireup="true" CodeFile="frmingPoliciaEdit.aspx.cs" Inherits="Policias_frmingPoliciaEdit" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width: 100%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: center; text-decoration: underline;">
        <asp:Label ID="lbltitulo" runat="server"></asp:Label>
    </div>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <hr />
    <script type="text/javascript">
        function Refrescar() {
            __doPostBack('btnrefrescar_Click', '');
        }
    </script>
    <div>

        <table style="width: 100%; color:black">
            <tr>
                <td></td>
                <td style="width:100%">

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>

                            <table style="width: 100%; color:black">
                                <tr>
                                    <td rowspan="11">
                                        <asp:Image ID="imgfoto" runat="server" Height="250px" Width="300px" ImageUrl="~/Images/sinfoto.jpg" />
                                    </td>
                                    <td style="text-align: left; width: 21px;">
                                        &nbsp;</td>
                                    <td style="text-align: left; width: 145px;">
                                        <asp:Label ID="Label1" runat="server" Text="*Tipo Documento:"></asp:Label>
                                    </td>
                                    <td style="text-align: left">

                                        <asp:DropDownList ID="ddlTipoDocumento" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoDocumento_SelectedIndexChanged" Width="267px">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: left">

                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 21px;">
                                        &nbsp;</td>
                                    <td style="text-align: left; width: 145px;">
                                        <asp:Label ID="Label2" runat="server" Text="*No. Documento:"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtNroDoc" runat="server" AutoPostBack="True" MaxLength="16" Width="260px"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="txtNroDoc_FilteredTextBoxExtender" runat="server" Enabled="True" FilterType="Numbers" InvalidChars=".-" TargetControlID="txtNroDoc">
                                        </asp:FilteredTextBoxExtender>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtNroDoc" ErrorMessage="Campo Requerido!." ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                   
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 21px;">
                                        &nbsp;</td>
                                    <td style="text-align: left; width: 145px;">
                                        <asp:Label ID="Label3" runat="server" Text="*Nombres:"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtnombres" runat="server" CssClass="upperCase" Height="19px" MaxLength="30" Width="260px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtnombres" ErrorMessage="Campo Requerido!." ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 21px;">
                                        &nbsp;</td>
                                    <td style="text-align: left; width: 145px;">
                                        <asp:Label ID="Label4" runat="server" Text="*Apellidos:"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtapellidos" runat="server" CssClass="upperCase" Height="19px" MaxLength="30" Width="260px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtapellidos" ErrorMessage="Campo Requerido!." ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 21px;">
                                        &nbsp;</td>
                                    <td style="text-align: left; width: 145px;">
                                        <asp:Label ID="Label5" runat="server" Text="*Fecha de Nacimiento:"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtFechaNac" runat="server" Width="260px"></asp:TextBox>
                                        <ajaxToolkit:MaskedEditExtender ID="txtFechaNac_MaskedEditExtender" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtFechaNac">
                                        </ajaxToolkit:MaskedEditExtender>
                                        <ajaxToolkit:CalendarExtender ID="txtFechaNac_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaNac">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                    <td style="text-align: left">
                                        &nbsp;</td>
                                    
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 21px;">
                                        &nbsp;</td>
                                    <td style="text-align: left; width: 145px;">
                                        <asp:Label ID="Label6" runat="server" Text="Celular:"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtCelular" runat="server" CssClass="upperCase" Height="19px" MaxLength="80" Width="260px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left">
                                        &nbsp;</td>
                                    
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 21px;">
                                        &nbsp;</td>
                                    <td style="text-align: left; width: 145px;">
                                        <asp:Label ID="lblcontrasenia" runat="server" Text="*Fecha Ingreso:"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtFechaIng" runat="server" Width="260px"></asp:TextBox>
                                        <ajaxToolkit:MaskedEditExtender ID="txtFechaIng_MaskedEditExtender" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtFechaIng">
                                        </ajaxToolkit:MaskedEditExtender>
                                        <ajaxToolkit:CalendarExtender ID="txtFechaIng_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaIng">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                    <td style="text-align: left">
                                        &nbsp;</td>
                                    
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 21px; height: 19px;"></td>
                                    <td style="text-align: left; width: 145px; height: 19px;">
                                        <asp:Label ID="lblcontrasenia0" runat="server" Text="*Fecha Salida:"></asp:Label>
                                    </td>
                                    <td style="text-align: left; height: 19px;">
                                        <asp:TextBox ID="txtFechaSal" runat="server" Width="260px"></asp:TextBox>
                                        <ajaxToolkit:MaskedEditExtender ID="txtFechaSal_MaskedEditExtender" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtFechaSal">
                                        </ajaxToolkit:MaskedEditExtender>
                                        <ajaxToolkit:CalendarExtender ID="txtFechaSal_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaSal">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                    <td style="text-align: left; height: 19px;">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 21px;">&nbsp;</td>
                                    <td style="text-align: left; width: 145px;">
                                        <asp:Label ID="lblcontrasenia1" runat="server" Text="*Rango:"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:DropDownList ID="ddlRango" runat="server" Width="267px">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: left">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 21px;">&nbsp;</td>
                                    <td style="text-align: left; width: 145px;">
                                        <asp:Label ID="lblcontrasenia2" runat="server" Text="*Area Asignada:"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:DropDownList ID="ddlArea" runat="server" Width="267px">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: left">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 21px;">
                                        &nbsp;</td>
                                    <td style="text-align: left; width: 145px;">
                                        <asp:Label ID="Label10" runat="server" Text="Observación:"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtObservacion" runat="server" Height="38px" MaxLength="80" TextMode="MultiLine" Width="264px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left">&nbsp;</td>
                                    
                                </tr>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="btnrefrescar" runat="server" Height="22px" ImageUrl="~/Botones/Refrescar.png" Visible="False" OnClick="btnrefrescar_Click" />
                                    </td>
                                    <td style="text-align: left; width: 21px;">
                                        &nbsp;</td>
                                    <td style="text-align: left; width: 145px;">
                                        <asp:Label ID="Label11" runat="server" Text="*Estado:"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:CheckBox ID="chkestado" runat="server" Checked="True" Enabled="False" Text="Activo" AutoPostBack="True" OnCheckedChanged="chkestado_CheckedChanged" />
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td style="text-align: left; width: 21px;">
                                        &nbsp;</td>
                                    <td style="text-align: left; width: 145px;">&nbsp;</td>
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
                <td style="width:100%">

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
                                <td style="width: 25%">
                                    <asp:ImageButton ID="btngrabar" runat="server" Height="60px" ImageUrl="~/Botones/Grabar.png" Visible="False" OnClick="btngrabar_Click" />
                                </td>
                                <td style="width: 25%">
                                    <asp:ImageButton ID="btncamara" runat="server" Height="60px" ImageUrl="~/Botones/camara.png" CausesValidation="False" Visible="False" OnClick="btncamara_Click" />
                                </td>
                                <td style="width: 25%">
                                    <asp:ImageButton ID="btnhuella" runat="server" Height="60px" ImageUrl="~/Botones/huella.png" CausesValidation="False" Visible="False" OnClick="btnhuella_Click" />
                                </td>
                                <td style="width: 25%">
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

    </div>
</asp:Content>

