<%@ Page Title="" Language="C#" MasterPageFile="~/Mantenimientos.master" AutoEventWireup="true" CodeFile="frmplaCopiar.aspx.cs" Inherits="Planificador_frmplaCopiar" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width: 100%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: center; text-decoration: underline;">
        <asp:Label ID="lbltitulo" runat="server"></asp:Label>
    </div>
    <ajaxToolkit:ToolkitScriptManager ID="smmantenimiento" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <hr />
    <table style=" height: 100%">
        <tr style="vertical-align: central">
            <td >
                <table runat="server" id="tblprincipal" style="width: 100%;  color: #000000;">
                    <tr>
                        <td style="text-align: left; width: 127px;">
                            <asp:Panel ID="Panel2" runat="server" Height="106px" Width="359px" GroupingText="Origien de la Copia">
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label17" runat="server" Text="Fecha desde donde desea copiar:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtcopifechaini" runat="server"></asp:TextBox>
                                            <ajaxToolkit:MaskedEditExtender ID="txtcopifechaini_MaskedEditExtender" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" TargetControlID="txtcopifechaini" Mask="99/99/9999" ClearMaskOnLostFocus="False">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <ajaxToolkit:CalendarExtender ID="txtcopifechaini_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtcopifechaini" Format="dd/MM/yyyy">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Campo Requerido!" ControlToValidate="txtcopifechaini" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label18" runat="server" Text="Fecha hasta donde desea copiar:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtcopifechahasta" runat="server"></asp:TextBox>
                                            <ajaxToolkit:MaskedEditExtender ID="txtcopifechahasta_MaskedEditExtender" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" TargetControlID="txtcopifechahasta" Mask="99/99/9999" ClearMaskOnLostFocus="False">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <ajaxToolkit:CalendarExtender ID="txtcopifechahasta_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtcopifechahasta" Format="dd/MM/yyyy">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Campo Requerido!" ControlToValidate="txtcopifechahasta" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                        <td style="text-align: left">
                            <asp:Panel ID="Panel3" runat="server" Height="107px" Width="359px" GroupingText="Destino de la Copia">
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label19" runat="server" Text="Fecha Inicio a la que se desea copiar"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtpegafechaini" runat="server"></asp:TextBox>
                                            <ajaxToolkit:MaskedEditExtender ID="txtpegafechaini_MaskedEditExtender" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" TargetControlID="txtpegafechaini" Mask="99/99/9999" ClearMaskOnLostFocus="False">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <ajaxToolkit:CalendarExtender ID="txtpegafechaini_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtpegafechaini" Format="dd/MM/yyyy">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Campo Requerido!" ControlToValidate="txtpegafechaini" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label20" runat="server" Text="Fecha Fin a la que se desea copiar"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtpegafechahasta" runat="server"></asp:TextBox>
                                            <ajaxToolkit:MaskedEditExtender ID="txtpegafechahasta_MaskedEditExtender" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" TargetControlID="txtpegafechahasta" Mask="99/99/9999" ClearMaskOnLostFocus="False">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <ajaxToolkit:CalendarExtender ID="txtpegafechahasta_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtpegafechahasta" Format="dd/MM/yyyy">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Campo Requerido!" ControlToValidate="txtpegafechahasta" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; " colspan="2">
                <asp:Label ID="lblerror" runat="server" Text="Ya existe la Planificacion para este  rango de fechas" ForeColor="Red" Visible="False"></asp:Label>
                        </td>
                    </tr>
                </table>
    <hr />

                <br />
                <table runat="server" id="tblDetalle" style="width: 100%;  color: #000000;" visible="False">
                    <tr>
                        <td style="text-align: left; width: 11%; height: 18px;">
                            </td>
                        <td style="text-align: left; width: 17%; height: 18px;">
                            &nbsp;</td>
                        <td style="text-align: left; width: 30%; height: 18px;">
                            &nbsp;</td>
                        <td style="text-align: left; width: 20%; height: 18px;">
                            &nbsp;</td>
                        <td style="text-align: left; width: 45%; height: 18px;">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: center; " colspan="5">
                <asp:Label ID="lblerrorDetalle" runat="server" Text="Ya existe la Planificacion para este  rango de fechas" ForeColor="Red" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; " colspan="5">
                            <asp:GridView ID="grdvDatos" runat="server" AllowSorting="True" AutoGenerateColumns="False" 
                                backcolor="White" CaptionAlign="Left" CellPadding="4" CssClass="Texto_General" 
                                DataKeyNames="codigo" EmptyDataText="La búsqueda no obtuvo ningún resultado" ForeColor="#333333" PageSize="15" SkinID="grillamant" 
                                Width="100%">
                                <Columns>
                                    <asp:CommandField HeaderText="Seleccionar" SelectText="-&gt;" ShowSelectButton="True">
                                    <ControlStyle Font-Size="7pt" ForeColor="Red" />
                                    <HeaderStyle CssClass="GVFixedHeader" />
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:CommandField>
                                    <asp:HyperLinkField DataTextField="Dia" HeaderText="Día" Target="_self">
                                    <ControlStyle Font-Size="7pt" ForeColor="Black" />
                                    <HeaderStyle CssClass="GVFixedHeader" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:HyperLinkField>
                                    <asp:HyperLinkField DataTextField="Turno" HeaderText="Turnos">
                                    <ControlStyle Font-Size="7pt" ForeColor="Black" />
                                    <HeaderStyle CssClass="GVFixedHeader" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:HyperLinkField>
                                     <asp:HyperLinkField DataTextField="TipoVisita" HeaderText="Tipo de Visita">
                                    <ControlStyle Font-Size="7pt" ForeColor="Black" />
                                    <HeaderStyle CssClass="GVFixedHeader" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:HyperLinkField>
                                     <asp:HyperLinkField DataTextField="Etapa" HeaderText="Etapa">
                                    <ControlStyle Font-Size="7pt" ForeColor="Black" />
                                    <HeaderStyle CssClass="GVFixedHeader" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:HyperLinkField>
                                     <asp:HyperLinkField DataTextField="Pabellon" HeaderText="Pabellon">
                                    <ControlStyle Font-Size="7pt" ForeColor="Black" />
                                    <HeaderStyle CssClass="GVFixedHeader" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:HyperLinkField>
                                     <asp:HyperLinkField DataTextField="Ala" HeaderText="Ala">
                                    <ControlStyle Font-Size="7pt" ForeColor="Black" />
                                    <HeaderStyle CssClass="GVFixedHeader" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:HyperLinkField>
                                     <asp:HyperLinkField DataTextField="ApeDesde" HeaderText="Apellido Desde" Visible="false">
                                    <ControlStyle Font-Size="7pt" ForeColor="Black" />
                                    <HeaderStyle CssClass="GVFixedHeader" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:HyperLinkField>
                                    <asp:HyperLinkField DataTextField="ApeHasta" HeaderText="Apellido Hasta" Visible="false">
                                    <ControlStyle Font-Size="7pt" ForeColor="Black" />
                                    <HeaderStyle CssClass="GVFixedHeader" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:HyperLinkField>
                                    <asp:HyperLinkField DataTextField="CantVisitas" HeaderText="Cantidad PPL">
                                    <ControlStyle Font-Size="7pt" ForeColor="Black" />
                                    <HeaderStyle CssClass="GVFixedHeader" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:HyperLinkField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btVerPPL" runat="server" Font-Size="X-Small" Text="Ver PPL" />
                                        </ItemTemplate>
                                        <ControlStyle Font-Size="7pt" ForeColor="Black" />
                                        <HeaderStyle CssClass="GVFixedHeader" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#E3E4F2" Font-Names="Verdana" />
                                <AlternatingRowStyle BackColor="White" Font-Names="Verdana" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="White" Font-Bold="True" ForeColor="#333333" />
                                <EditRowStyle BackColor="#2461BF" />
                                <PagerSettings Mode="NumericFirstLast" />
                            </asp:GridView>
                            <br />
                            <br />
                        </td>
                    </tr>
                </table>
            </td>

        </tr>
        <tr >
            <td style="width: 100%">
                <div id="menuaccions" class="menuaccions" runat="server">
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <asp:ImageButton ID="btngrabar" runat="server" Height="60px" ImageUrl="~/Botones/Grabar.png" Visible="False" />
                            </td>
                            <td>

                                <asp:ImageButton ID="btnprocesar" runat="server" Height="40px" ImageUrl="~/Botones/i_procesar.png" OnClick="btnprocesar_Click" />

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

