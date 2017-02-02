<%@ Page Title="" Language="C#" MasterPageFile="~/Mantenimientos.master" AutoEventWireup="true" CodeFile="frmplaNew.aspx.cs" Inherits="Planificador_frmplaNew" %>
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
                <table runat="server" id="tblprincipal" style="width: 69%;  color: #000000;">
                    <tr>
                        <td style="text-align: left; width: 127px;">
                            <asp:Label ID="Label1" runat="server" Text="*Descripción:"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtDescripcion" runat="server" Width="264px" MaxLength="80" CssClass="upperCase" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDescripcion" ErrorMessage="Campo Requerido..!" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 127px;">
                            <asp:Label ID="Label4" runat="server" Text="*Mes/Año:"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtFechaDesde" runat="server" Width="260px"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="txtFechaDesde_MaskedEditExtender" runat="server" ClearMaskOnLostFocus="False" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99/9999" TargetControlID="txtFechaDesde">
                            </ajaxToolkit:MaskedEditExtender>
                            <ajaxToolkit:CalendarExtender ID="txtFechaDesde_CalendarExtender" runat="server" Enabled="True" Format="MM/yyyy" TargetControlID="txtFechaDesde">
                            </ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtFechaDesde" ErrorMessage="Campo Requerido..!" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 127px;">
                            <asp:Label ID="Label5" runat="server" Text="*Fecha Hasta:" Visible="False"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtFechaHasta" runat="server" Width="260px" Visible="False"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="txtFechaHasta_MaskedEditExtender" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtFechaHasta"></ajaxToolkit:MaskedEditExtender>
                            <ajaxToolkit:CalendarExtender ID="txtFechaHasta_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaHasta"></ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 127px;">
                            &nbsp;</td>
                        <td style="text-align: left">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: center; " colspan="2">
                <asp:Label ID="lblerror" runat="server" Text="Ya existe la Planificacion para este  Mes" ForeColor="Red" Visible="False"></asp:Label>
                        </td>
                    </tr>
                </table>
    <hr />
                <br />
                <table runat="server" id="tblDetalle" style="width: 100%;  color: #000000;" visible="False">
                    <tr>
                        <td style="text-align: left; width: 11%;">
                            &nbsp;</td>
                        <td style="text-align: left; width: 17%;">
                            <asp:Label ID="Label6" runat="server" Text="Día:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 30%;">
                            <asp:DropDownList ID="ddlDia" runat="server" Width="230px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; width: 20%;">
                            <asp:Label ID="Label10" runat="server" Text="Etapa:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 45%;">
                            <asp:DropDownList ID="ddlEtapa" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEtapa_SelectedIndexChanged" Width="230px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 11%;">
                            &nbsp;</td>
                        <td style="text-align: left; width: 17%;">
                            <asp:Label ID="Label15" runat="server" Text="Turno:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 30%;">
                            <asp:DropDownList ID="ddlTurnos" runat="server" Width="230px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; width: 20%;">
                            <asp:Label ID="Label11" runat="server" Text="Pabellon:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 45%;">
                            <asp:DropDownList ID="ddlPabellon" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPabellon_SelectedIndexChanged" Width="230px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 11%;">
                            &nbsp;</td>
                        <td style="text-align: left; width: 17%;">
                            <asp:Label ID="Label9" runat="server" Text="Tipo de Visita"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 30%;">
                            <asp:DropDownList ID="ddlTipoVisita" runat="server" Width="230px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; width: 20%;">
                            <asp:Label ID="Label12" runat="server" Text="Ala:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 45%;">
                            <asp:DropDownList ID="ddlAla" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAla_SelectedIndexChanged" Width="230px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 11%;">
                            &nbsp;</td>
                        <td style="text-align: left; width: 17%;">
                            &nbsp;</td>
                        <td style="text-align: left; width: 30%;">
                            &nbsp;</td>
                        <td style="text-align: left; width: 20%;">
                            Piso:</td>
                        <td style="text-align: left; width: 45%;">
                            <asp:DropDownList ID="ddlPiso" runat="server"  AutoPostBack="True" Width="230px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 11%; height: 18px;">
                            </td>
                        <td style="text-align: left; width: 17%; height: 18px;">
                            <asp:Label ID="Label13" runat="server" Text="Apellidos Desde:" Visible="False"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 30%; height: 18px;">
                            <asp:DropDownList ID="ddlDesde" runat="server" Width="50px" Visible="False">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; width: 20%; height: 18px;">
                            <asp:Label ID="Label16" runat="server" Text="Apellidos Hasta:" Visible="False"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 45%; height: 18px;">
                            <asp:DropDownList ID="ddlHasta" runat="server" Width="50px" Visible="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; " colspan="5">

                                <asp:ImageButton ID="btNuevo" runat="server" Height="60px" ImageUrl="~/Botones/i_agregar.png" OnClick="btNuevo_Click" />

                                <asp:ImageButton ID="btActualizar" runat="server" Height="60px" ImageUrl="~/Botones/i_editar.png" OnClick="btActualizar_Click" />

                                <asp:ImageButton ID="btEliminar" runat="server" Height="60px" ImageUrl="~/Botones/ii_eliminar.png" OnClick="btEliminar_Click" />

                        </td>
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
                                DataKeyNames="codigo" EmptyDataText="La búsqueda no obtuvo ningún resultado" ForeColor="#333333" 
                                OnSelectedIndexChanged="grdvDatos_SelectedIndexChanged" PageSize="15" SkinID="grillamant" 
                                Width="100%" OnRowDataBound="grdvDatos_RowDataBound">
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
                                    <asp:HyperLinkField DataTextField="Piso" HeaderText="Piso">
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
                                            <asp:Button ID="btVerPPL" runat="server" Font-Size="X-Small" OnClick="btVerPPL_Click" Text="Ver PPL" />
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
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
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

