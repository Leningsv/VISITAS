<%@ Page Title="" Language="C#" MasterPageFile="~/Mantenimientos.master" AutoEventWireup="true" CodeFile="frmacceNew.aspx.cs" Inherits="CRS_frmacceNew" %>
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
            <table style="width: 100%; height: 100%">
                <tr>
                    <td style="width:100%">
                        <table runat="server" id="tblprincipal" style="width: 100%;  color: #000000;">
                            <tr>
                                <td style="text-align: right; width: 326px;">
                                    <asp:Label ID="Label1" runat="server" Text="*CRS:"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlcrs" runat="server" Width="267px">
                                    </asp:DropDownList>                            
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlcrs" ErrorMessage="Campor Requerido!" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 326px;">
                                    <asp:Label ID="Label12" runat="server" Text="Código:"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtcodigo" runat="server" MaxLength="16" Width="264px"></asp:TextBox>
                                    <asp:MaskedEditExtender ID="txtcodigo_MaskedEditExtender" runat="server" ClearMaskOnLostFocus="true" ClearTextOnInvalid="True" Mask="9999" Enabled="True" TargetControlID="txtcodigo">
                                    </asp:MaskedEditExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtcodigo" ErrorMessage="Campo Requerido!" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 326px; height: 18px;">
                                    <asp:Label ID="Label2" runat="server" Text="*Etapa:"></asp:Label>
                                </td>
                                <td style="height: 18px; text-align: left;">
                                    <asp:DropDownList ID="ddletapa" runat="server" Width="267px" AutoPostBack="True">
                                    </asp:DropDownList>                            
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddletapa" ErrorMessage="Campo Requerido!" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 326px; height: 18px;">
                                    <asp:Label ID="Label3" runat="server" Text="*Nombre Acceso:"></asp:Label>
                                </td>
                                <td style="height: 18px; text-align: left;">
                                    <asp:TextBox ID="txtnombre" runat="server" CssClass="upperCase" MaxLength="80" Width="264px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtnombre" ErrorMessage="Campo Requerido!" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 326px; height: 18px;">
                                    <asp:Label ID="Label4" runat="server" Text="Descripción:"></asp:Label>
                                </td>
                                <td style="height: 18px; text-align: left;">
                                    <asp:TextBox ID="txtdescripcion" runat="server" MaxLength="50" Width="264px" CssClass="upperCase" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 326px; height: 18px;">
                                    <asp:Label ID="Label11" runat="server" Text="Estado:"></asp:Label>
                                </td>
                                <td style="height: 18px; text-align: left;">
                                    <asp:CheckBox ID="chkestado" runat="server" Checked="True" Text="Activo" AutoPostBack="True" OnCheckedChanged="chkestado_CheckedChanged" Enabled="False" />
                                </td>
                            </tr>

                            </table>
                        <asp:Label ID="lblerror" runat="server" Text="Ya existe Acceso Creado, por favor ingrese otro" ForeColor="Red" Visible="False"></asp:Label>
                    </td>

                </tr>
                <tr >
                    <td >
                        
                        <table style="width: 100%; color:black" border="1" runat="server" id="tblequipos">
                            <tr>
                                <td style="height: 20px; width: 304px;">
                                    <asp:Label ID="Label13" runat="server" Text="Equipo"></asp:Label>
                                </td>
                                <td style="height: 20px; width: 209px;">
                                    <asp:Label ID="Label14" runat="server" Text="Dirección IP"></asp:Label>
                                </td>
                                <td style="height: 20px; width: 109px;">
                                    <asp:Label ID="Label15" runat="server" Text="Estado"></asp:Label>
                                </td>
                                <td style="height: 20px; width: 131px;">
                                    <asp:Label ID="Label16" runat="server" Text="Verificar"></asp:Label>
                                </td>
                                <td style="height: 20px; width: 217px;">

                                </td>
                            </tr>
                            <tr>
                                <td style="width: 304px">
                                    <asp:TextBox ID="txtequipo" runat="server" Width="290px" MaxLength="80" CssClass="upperCase"></asp:TextBox>
                                </td>
                                <td style="width: 209px">
                                    <asp:TextBox ID="txtdirip" runat="server" Width="200px"></asp:TextBox>
                                    <asp:MaskedEditExtender ID="txtdirip_MaskedEditExtender" runat="server" ClearMaskOnLostFocus="true" ClearTextOnInvalid="True" Filtered=". " Mask="NNNNNNNNNNNNNNN" Enabled="True" TargetControlID="txtdirip">
                                    </asp:MaskedEditExtender>
                                </td>
                                <td style="width: 109px">
                                    <asp:CheckBox ID="chkestados" runat="server" Checked="True" Text="Activo" AutoPostBack="True" OnCheckedChanged="chkestados_CheckedChanged" />
                                </td>
                                <td style="width: 131px">
                                    <asp:Button ID="btnverificar" runat="server" Text="..." OnClick="btnverificar_Click" CausesValidation="False" />
                                </td>
                                <td style="text-align: left; width: 217px;">
                                    <asp:Image ID="imgsemaforo" runat="server" Height="40px" ImageUrl="~/Images/semaforo_apagado.png" Width="27px" />
                                    <asp:Label ID="lblestado" runat="server" Text="Sin Verificar" ForeColor="Gray"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5" style="text-align: center">
                                    <asp:ImageButton ID="btnagregar" runat="server" CausesValidation="False" Height="60px" ImageUrl="~/Botones/i_agregar.png" OnClick="btnagregar_Click" />
                                    <asp:ImageButton ID="btneditar" runat="server" CausesValidation="False" Height="60px" ImageUrl="~/Botones/i_editar.png" OnClick="btneditar_Click" Visible="False" />
                                    <asp:ImageButton ID="btneliminar" runat="server" CausesValidation="False" Height="60px" ImageUrl="~/Botones/ii_eliminar.png" OnClick="btneliminar_Click" Visible="False" />
                                    <asp:Label ID="lblitem" runat="server" Visible="False"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        
                    </td>
                </tr>
                <tr>
                    <td>
                       <%-- <asp:Panel ID="Panel1" runat="server" Height="150px" ScrollBars="Vertical" Width="100%" >--%>
                            <asp:GridView ID="grdvDetalle" runat="server" 
                                AutoGenerateColumns="False" CaptionAlign="Left"  backcolor="White"
                                Width="100%" AllowSorting="True" 
                                ForeColor="#333333" CellPadding="2" PageSize="15" CssClass="Texto_General" DataKeyNames="Codigo"  > 
                                <Columns>
                                    <asp:TemplateField HeaderText="Seleccionar">
                                        <HeaderStyle CssClass="GVFixedHeader" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnselecc" runat="server" CausesValidation="False" Height="25px" ImageUrl="~/Botones/seleccionar.png" OnClick="btnselecc_Click" />
                                        </ItemTemplate>
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
                                    <asp:BoundField HeaderText="Estado" DataField="Estado" >
                                        <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                        <HeaderStyle CssClass="GVFixedHeader" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>  
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
                        <%--</asp:Panel>--%>
                        
                    </td>
                </tr>      
                <tr>
                    <td>
                        <div id="menuaccions" style="border-style: ridge double double ridge; width: 100%; background-color: #FFFFFF; border-top-width: 4px; border-top-color: #FF0000; border-left-color: #FF0000;">
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 385px">
                                        <asp:ImageButton ID="btngrabar" runat="server" Height="60px" ImageUrl="~/Botones/Grabar.png" OnClick="btngrabar_Click"  />
                                    </td>
                                    <td style="width: 133px">
                                        &nbsp;</td>
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

