<%@ Page Title="" Language="C#" MasterPageFile="~/Mantenimientos.master" AutoEventWireup="true" CodeFile="frmvistpplNew.aspx.cs" Inherits="VisitaPPL_frmvistpplNew" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width: 100%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: center; text-decoration: underline;">
        <asp:Label ID="lbltitulo" runat="server"></asp:Label>
    </div>
    
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <hr /> 
            <div style="width: 100%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: left; text-decoration: underline;">
                <asp:Label ID="lbleti2" runat="server" Font-Size="12pt">datos ppl</asp:Label>
            </div>
            <div class="buscador" style="height:110px; width:100%; background-color: #F4F4F4;">
                <table style=" align-content:center; color: #000000;">
                    <tr >
                        <td style="width: 89px; text-align: left;">
                            <asp:Label ID="Label1" runat="server" Text="Nombres:"></asp:Label>
                        </td>
                        <td style="width: 203px; text-align: left;">
                            <asp:TextBox ID="txtnombres" runat="server" AutoPostBack="True" Enabled="False" OnTextChanged="txtnumdocu_TextChanged" Width="264px"></asp:TextBox>
                        </td>
                        <td style="width: 75px; text-align: left;">
                            <asp:Label ID="Label15" runat="server" Text="Apellidos:"></asp:Label>
                        </td>
                        <td style="width: 112px; text-align: left;">
                            <asp:TextBox ID="txtapellidos" runat="server" AutoPostBack="True" Enabled="False" OnTextChanged="txtnumdocu_TextChanged" Width="264px"></asp:TextBox>
                        </td>
                        
                    </tr>
                    <tr>
                        <td style="width: 89px; text-align: left;">
                            <asp:Label ID="Label2" runat="server" Text="Etapa:"></asp:Label>
                        </td>
                        <td style="width: 203px; text-align: left;">
                            <asp:TextBox ID="txtetapa" runat="server" AutoPostBack="True" Enabled="False" OnTextChanged="txtnumdocu_TextChanged" Width="264px"></asp:TextBox>
                        </td>
                        <td style="width: 75px; text-align: left;">
                            <asp:Label ID="Label13" runat="server" Text="Pabellón:"></asp:Label>
                        </td>
                        <td style="width: 112px; text-align: left;">
                            <asp:TextBox ID="txtpabellon" runat="server" AutoPostBack="True" Enabled="False" OnTextChanged="txtnumdocu_TextChanged" Width="264px"></asp:TextBox>
                        </td>
                        
                    </tr>
                    <tr>
                        
                        <td style="width: 89px; text-align: left;">
                            <asp:Label ID="Label14" runat="server" Text="Tipo Visita:"></asp:Label>
                        </td>
                        <td style="width: 203px; text-align: left;">
                            <asp:DropDownList ID="ddltipovisita" runat="server" Enabled="False" Width="267px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 75px">
                            &nbsp;</td>
                        <td style="width: 112px; text-align: left;">&nbsp;</td>
                        
                    </tr>
                    <tr>
                        <td colspan="6">
                            <asp:Label ID="lblerror" runat="server" ForeColor="Red" Text="No existe Visitante. Por favor Ingrese uno nuevo" Visible="False"></asp:Label>
                        </td>
                    </tr>
                </table>                
            </div>
            <hr />
            <div style="width: 100%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: left; text-decoration: underline;">
                <asp:Label ID="Label3" runat="server" Font-Size="12pt">visitantes relacionados</asp:Label>
            </div>
            <table style=" align-content:center; color: #000000;" runat="server" id="Table1" >
                <tr>
                    <td style="width: 86px; text-align: right;">
                        <asp:Label ID="Label16" runat="server" Text="Familiares:"></asp:Label>
                    </td>
                    <td style="width: 55px; text-align: center;">
                        <asp:Label ID="Label19" runat="server" Text="0"></asp:Label>
                    </td>
                    <td style="width: 92px; text-align: right;">
                        <asp:Label ID="Label17" runat="server" Text="Conyugales:"></asp:Label>
                    </td>
                    <td style="width: 42px; text-align: center;">
                        <asp:Label ID="Label20" runat="server" Text="0"></asp:Label>
                    </td>
                    <td style="text-align: right; width: 76px;">
                        <asp:Label ID="Label18" runat="server" Text="Legales:"></asp:Label>
                    </td>
                    <td style="text-align: center; width: 61px;">
                        <asp:Label ID="Label21" runat="server" Text="0"></asp:Label>
                    </td>
                </tr>
                </table>
            <hr />
            <div style="width: 100%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: left; text-decoration: underline;">
                <asp:Label ID="lbltitu1" runat="server" Font-Size="12pt" Visible="False">Buscar Visitante</asp:Label>
            </div>
<%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <table style="align-content:center; color: #000000;" runat="server" id="tblbuscar" visible="false" >
                <tr>
                    <td style="width: 175px; text-align: right;">
                        <asp:Label ID="Label11" runat="server" Text="Elija Tipo Documento:"></asp:Label>
                    </td>
                    <td style="width: 152px; text-align: left;">
                        <asp:DropDownList ID="ddltipodoc" runat="server" Width="267px" AutoPostBack="True" OnSelectedIndexChanged="ddltipodoc_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 170px">&nbsp;</td>
                    
                </tr>
                <tr>
                    <td style="width: 175px; text-align: right;">
                        <asp:Label ID="Label12" runat="server" Text="Nro. Documento:"></asp:Label>
                    </td>
                    <td style="width: 152px">
                        <asp:TextBox ID="txtnumdocu" runat="server" Width="264px" OnTextChanged="txtnumdocu_TextChanged" AutoPostBack="True"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="txtnumdocu_FilteredTextBoxExtender" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtnumdocu">
                        </asp:FilteredTextBoxExtender>
                    </td>
                    <td style="width: 170px; text-align: left;">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtnumdocu" ErrorMessage="Campo Requerido!." ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    
                </tr>
                </table>
<%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
            
            <hr />
            <div style="width: 100%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: left; text-decoration: underline;">
                <asp:Label ID="lbleti3" runat="server" Font-Size="12pt">Visitantes Autorizados</asp:Label>
            </div>
            <table style="height:100%; width:100%">
                <tr style="vertical-align:top">
                    <td style="height: 560px; ">
                        <table runat="server" id="tblprincipal" style="width:100%">
                            <tr>
                                <td style="width: 100%; text-align:left; background-color:#FFFFFF">
                                    <asp:UpdatePanel ID="uppbuscagrilla" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>     
                                            <%--<asp:Panel ID="Panel1" runat="server" Height="320px" ScrollBars="Both" Width="100%"> --%>                                                                                                   
                                                <asp:GridView ID="grdvDatos" SkinID="grillamant" runat="server" 
                                                AutoGenerateColumns="False" CaptionAlign="Left"  backcolor="White"
                                                Width="100%" EmptyDataText="No existen Visitantes Relacionados" 
                                                ForeColor="#333333" 
                                                DataKeyNames="Codigo,TipoVisita,NumDocu,Parente" CellPadding="2" CssClass="Texto_General" OnRowDataBound="grdvDatos_RowDataBound" AllowPaging="True" > 
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Ingreso de Visita">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnselect" runat="server" Height="25px" ImageUrl="~/Botones/seleccionar_verde.png" OnClick="btnselect_Click" CausesValidation="False" />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GVFixedHeader" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Sancionado">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgrojo" runat="server" Height="20px" ImageUrl="~/Botones/botonrojo.jpg" Visible="False" />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GVFixedHeader" />
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="Documento" HeaderText="Tipo Documento" >
                                                        <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                        <HeaderStyle CssClass="GVFixedHeader"  />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>

                                                    <asp:BoundField DataField="NumDocu" HeaderText="Nro. Documento" >
                                                        <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                        <HeaderStyle CssClass="GVFixedHeader"  />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>

                                                    <asp:BoundField DataField="Visitante" HeaderText="Visitante" >
                                                        <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                        <HeaderStyle CssClass="GVFixedHeader"  />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>

                                                    <asp:BoundField DataField="VisTipo" HeaderText="Relación Visita" >
                                                        <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                        <HeaderStyle CssClass="GVFixedHeader"  />
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
                                        </ContentTemplate>
                                    </asp:UpdatePanel>                            
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; color: #FF0000;">

                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 100%">
                                    <div id="menuaccions" style="border-style: ridge double double ridge; width: 100%; background-color: #FFFFFF; border-top-width: 4px; border-top-color: #FF0000; border-left-color: #FF0000;">
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 41%">
                                                    <asp:ImageButton ID="btnnuevo" runat="server" Height="60px" ImageUrl="~/Botones/Nuevo.png" CausesValidation="False" OnClick="btnnuevo_Click" Visible="False" />
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:ImageButton ID="btnsalir" runat="server" Height="60px" ImageUrl="~/Botones/Salir.png" CausesValidation="False" OnClick="btnsalir_Click" />
                                                </td>
                                                <td style="width: 50%">
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>

                    </td>
                </tr>
            </table>
</asp:Content>

