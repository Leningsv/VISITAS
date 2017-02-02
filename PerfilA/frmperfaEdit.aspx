<%@ Page Title="" Language="C#" MasterPageFile="~/Mantenimientos.master" AutoEventWireup="true" CodeFile="frmperfaEdit.aspx.cs" Inherits="PerfilA_frmperfaEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width: 100%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: center; text-decoration: underline;">
        <asp:Label ID="lbltitulo" runat="server"></asp:Label>
    </div>
    <asp:ToolkitScriptManager ID="smmantenimiento" runat="server"></asp:ToolkitScriptManager>
    <hr />

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%; height: 100%">
                <tr style="vertical-align: top">
                    <td style="border-style: double groove groove double; width: 100%; border-top-width: 1px; border-top-color: #FFFFFF; border-left-width: 1px; border-left-color: #FFFFFF; ">
                        <table runat="server" id="tblprincipal" style="width: 100%;  color: #000000;">
                            <tr>
                                <td style="text-align: right; width: 225px;">
                                    &nbsp;</td>
                                <td style="text-align: left; width: 155px;">
                                    <asp:Label ID="Label5" runat="server" Text="Código:"></asp:Label>
                                </td>
                                <td style="text-align: left; width: 295px;">
                                    <asp:TextBox ID="txtcodigo" runat="server" Enabled="False" MaxLength="80" Width="264px"></asp:TextBox>
                                </td>
                                <td style="text-align: left">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 225px;">
                                    &nbsp;</td>
                                <td style="text-align: left; width: 155px;">
                                    <asp:Label ID="Label1" runat="server" Text="*Nombre del Perfil:"></asp:Label>
                                </td>
                                <td style="text-align: left; width: 295px;">
                                    <asp:TextBox ID="txtnombre" runat="server" CssClass="upperCase" MaxLength="80" Width="264px"></asp:TextBox>
                                </td>
                                <td style="text-align: left">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtnombre" ErrorMessage="Campo Requerido..!" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 225px;">
                                    &nbsp;</td>
                                <td style="text-align: left; width: 155px;">
                                    <asp:Label ID="Label4" runat="server" Text="*Descripción Perfil:"></asp:Label>
                                </td>
                                <td style="text-align: left; width: 295px;">
                                    <asp:TextBox ID="txtdescripcion" runat="server" Width="264px" MaxLength="80" CssClass="upperCase" Height="38px" TextMode="MultiLine" ></asp:TextBox>
                                </td>
                                <td style="text-align: left">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtdescripcion" ErrorMessage="Campo Requerido..!" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 225px;">&nbsp;</td>
                                <td style="text-align: left; width: 155px;">
                                    <asp:CheckBox ID="chkabierto" runat="server" Text="Perfil Abierto" />
                                </td>
                                <td style="text-align: center; width: 295px;">
                                    <asp:CheckBox ID="chkrestringido" runat="server" Text="Perfil Restringido" />
                                </td>
                                <td style="text-align: left">
                                    <asp:CheckBox ID="chkeliminar" runat="server" Text="Permite Eliminar" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 225px;">
                                    &nbsp;</td>
                                <td style="text-align: left; width: 155px;">
                                    <asp:Label ID="Label3" runat="server" Text="Estado:"></asp:Label>
                                </td>
                                <td style="text-align: left; width: 295px;">
                                    <asp:CheckBox ID="chkestado" runat="server" Checked="True" Text="Activo" AutoPostBack="True" OnCheckedChanged="chkestado_CheckedChanged" />
                                </td>
                                <td style="text-align: left">&nbsp;</td>
                            </tr>
                        </table>
                        <asp:Label ID="lblerror" runat="server" Text="El Nombre del Perfil ingresado ya existe" ForeColor="Red" Visible="False"></asp:Label>
                    </td>

                </tr>        
            </table>
            <hr />
            <div style="width: 100%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: left; text-decoration: underline;">
                <asp:Label ID="Label2" runat="server" Font-Size="12pt">lista de accesos</asp:Label>
            </div>
            <table style="width: 100%">
                <tr>
                    <td>
                        <table runat="server" id="tblsecundaria" style="width: 100%">
                            <tr>
                                <td style="width: 100%; text-align:left; background-color:#FFFFFF">                             
                                    <%--<asp:Panel ID="Panel2" runat="server" ScrollBars="Vertical" Height="180px" Width="100%">--%>                               
                                        <asp:GridView ID="grdvDatos" runat="server" 
                                            AutoGenerateColumns="False" CaptionAlign="Left"  backcolor="White"
                                            Width="100%" AllowSorting="True" EmptyDataText="La búsqueda no obtuvo ningún resultado" 
                                            ForeColor="#333333" CellPadding="2" PageSize="15" CssClass="Texto_General" OnSelectedIndexChanged="grdvDatos_SelectedIndexChanged" DataKeyNames="CodigoCRS,CodigoLOC" OnRowDataBound="grdvDatos_RowDataBound"  > 
                                            <Columns>
                                                <asp:TemplateField HeaderText="Seleccionar">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkselecc" runat="server" AutoPostBack="True" OnCheckedChanged="chkselecc_CheckedChanged" />
                                                    </ItemTemplate>
                                                    <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                    <HeaderStyle CssClass="GVFixedHeader" />                                                    
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mostrar Equipos">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnselecc" runat="server" Height="25px" ImageUrl="~/Botones/seleccionar.png" OnClick="btnselecc_Click" />
                                                    </ItemTemplate>
                                                    <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                    <HeaderStyle CssClass="GVFixedHeader" /> 
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Crs" HeaderText="CRS" >
                                                    <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                    <HeaderStyle CssClass="GVFixedHeader" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField> 
                                                <asp:BoundField HeaderText="Acceso" DataField="Acceso" >
                                                    <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                    <HeaderStyle CssClass="GVFixedHeader" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>   
                                                <asp:BoundField HeaderText="Etapa" DataField="Etapa" >
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
                                <td style="width: 100%; text-align:left; background-color:#FFFFFF">  
                                    <hr />
                                    <div style="width: 100%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: left; text-decoration: underline;">
                                        <asp:Label ID="lblequipos" runat="server" Font-Size="12pt" Visible="False">Equipos</asp:Label>
                                    </div>
                                    <%--<asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="180px" Width="100%"> --%>
                                        <asp:GridView ID="grdvDetalle" runat="server" 
                                                AutoGenerateColumns="False" CaptionAlign="Left"  backcolor="White"
                                                Width="100%" AllowSorting="True" 
                                                ForeColor="#333333" CellPadding="2" PageSize="15" CssClass="Texto_General" DataKeyNames="CodigoEqu" OnRowDataBound="grdvDetalle_RowDataBound"  > 
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Seleccionar">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkequipo" runat="server" AutoPostBack="True" OnCheckedChanged="chkequipo_CheckedChanged" />
                                                        </ItemTemplate>
                                                        <ControlStyle ForeColor="Black" Font-Size="7pt" />
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
                                <td style="width: 100%">
                                <div id="menuaccions" style="border-style: ridge double double ridge; width: 100%; background-color: #FFFFFF; border-top-width: 4px; border-top-color: #FF0000; border-left-color: #FF0000;">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 385px">
                                                <asp:ImageButton ID="btngrabar" runat="server" Height="60px" ImageUrl="~/Botones/Grabar.png" OnClick="btngrabar_Click" />
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
                </td>
            </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel> 
</asp:Content>

