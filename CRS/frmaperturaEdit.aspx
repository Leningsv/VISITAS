<%@ Page Title="" Language="C#" MasterPageFile="~/Mantenimientos.master" AutoEventWireup="true" CodeFile="frmaperturaEdit.aspx.cs" Inherits="CRS_frmaperturaEdit" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width: 100%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: center; text-decoration: underline;">
        <asp:Label ID="lbltitulo" runat="server"></asp:Label>
    </div>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

    <hr />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Timer ID="tmpactivar" runat="server" Enabled="False" Interval="5000" OnTick="tmpactivar_Tick">
            </asp:Timer>
            <table style="width: 100%; height: 100%">
                <tr>
                    <td>
                        <table runat="server" id="tblprincipal" style="width: 100%;  color: #000000;">
                            <tr>
                                <td style="text-align: right; width: 326px; height: 28px;">
                                    <asp:Label ID="Label2" runat="server" Text="Etapa:"></asp:Label>
                                </td>
                                <td style="text-align: left; height: 28px;">
                                    <asp:DropDownList ID="ddletapa" runat="server" AutoPostBack="True" Width="267px" OnSelectedIndexChanged="ddletapa_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddletapa" ErrorMessage="Campo Requerido!" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 326px; height: 28px;">
                                    <asp:Label ID="Label17" runat="server" Text="Acceso:"></asp:Label>
                                </td>
                                <td style="text-align: left; height: 28px;">
                                    <asp:TextBox ID="txtacceso" runat="server" CssClass="upperCase" Height="20px" MaxLength="80" Width="259px"></asp:TextBox>
                                </td>
                            </tr>
                            </table>
                        <asp:Label ID="lblerror" runat="server" Text="Ya existe Acceso Creado, por favor ingrese otro" ForeColor="Red" Visible="False"></asp:Label>
                    </td>

                </tr>
 
                <tr >
                    <td >                        
                        <table style="width: 100%; color:black" border="1" runat="server" id="tblequipos" visible="false" >
                            <tr>
                                <td style="width: 200px">
                                    &nbsp;</td>
                                <td style="width: 130px">
                                    <asp:Label ID="Label14" runat="server" Text="Tiempo de Apertura"></asp:Label>
                                </td>
                                <td style="width: 53px">
                                    <asp:TextBox ID="txttiempo" runat="server" Height="17px" Width="125px" MaxLength="4"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="txttiempo_FilteredTextBoxExtender" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txttiempo">
                                    </asp:FilteredTextBoxExtender>
                                </td>
                                <td style="width: 52px">
                                    <asp:Label ID="Label18" runat="server" Text="Segundos"></asp:Label>
                                </td>
                                <td style="text-align: left; width: 124px;">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 200px">&nbsp;</td>
                                <td style="width: 130px">
                                    <asp:Label ID="Label15" runat="server" Text="Observación"></asp:Label>
                                </td>
                                <td style="width: 52px" colspan="2">
                                    <asp:TextBox ID="txtobservacion" runat="server" CssClass="upperCase" Height="16px" MaxLength="255" Width="377px"></asp:TextBox>
                                </td>
                                <td style="text-align: left; width: 124px;">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 200px">&nbsp;</td>
                                <td style="width: 130px">&nbsp;</td>
                                <td style="width: 52px" colspan="2">&nbsp;</td>
                                <td style="text-align: left; width: 124px;">&nbsp;</td>
                            </tr>
                        </table>
                        
                    </td>
                </tr>  
                              
                <tr>
                    <td>
                        <asp:Panel ID="Panel1" runat="server" Height="200px" ScrollBars="Vertical" Width="100%" Visible="false" >                        
                            <asp:GridView ID="grdvDetalle" SkinID="grillamant" runat="server" 
                            AutoGenerateColumns="False" CaptionAlign="Left"  backcolor="White"
                            Width="100%" AllowSorting="True" 
                            ForeColor="#333333" CellPadding="2" PageSize="15" CssClass="Texto_General" DataKeyNames="CodigoEquipo" OnRowDataBound="grdvDetalle_RowDataBound"  > 
                            <Columns>
                                <asp:TemplateField HeaderText="Seleccionar">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkselecc" runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="GVFixedHeader" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Equipo" DataField="Equipo" >
                                    <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                    <HeaderStyle CssClass="GVFixedHeader" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>   
                                <asp:BoundField HeaderText="Dirección IP" DataField="DirIP" >
                                    <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                    <HeaderStyle CssClass="GVFixedHeader" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>  
                                <asp:TemplateField HeaderText="Estado">
                                    <ItemTemplate>
                                        <asp:Image ID="imgestado" runat="server" Height="40px" ImageUrl="~/Botones/torno_cerrado.png" Width="40px" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="GVFixedHeader" />
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
                        </asp:Panel>
                    </td>
                </tr> 
                <tr>
                    <td style="width: 100%">
                        <div id="menuaccions" style="border-style: ridge double double ridge; width: 100%; background-color: #FFFFFF; border-top-width: 4px; border-top-color: #FF0000; border-left-color: #FF0000;">
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 385px">
                                        <asp:ImageButton ID="btngrabar" runat="server" Height="60px" ImageUrl="~/Botones/Grabar.png" OnClick="btngrabar_Click"  />
                                    </td>
                                    <td style="width: 133px">
                                        <asp:ImageButton ID="btnrefrescar" runat="server" Height="60px" ImageUrl="~/Botones/Refrescar.png" OnClick="btnrefrescar_Click" />
                                    </td>
                                    <td style="width:312px">
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

