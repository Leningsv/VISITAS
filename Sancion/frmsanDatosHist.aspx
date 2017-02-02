<%@ Page Title="" Language="C#" MasterPageFile="~/Mantenimientos.master" AutoEventWireup="true" CodeFile="frmsanDatosHist.aspx.cs" Inherits="Sancion_frmsanDatosHist" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width: 100%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: center; text-decoration: underline;">
        <asp:Label ID="lbltitulo" runat="server"></asp:Label>
    </div>    
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <hr />
    <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <div style="width: 100%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: left; text-decoration: underline;">
        <asp:Label ID="lbleti1" runat="server" Font-Size="12pt">datos del visitante</asp:Label>
    </div>  
    <table style="width: 100%; height: 100%">
        <tr style="vertical-align: top">
            <td style="border-style: double groove groove double; width: 100%; border-top-width: 1px; border-top-color: #FFFFFF; border-left-width: 1px; border-left-color: #FFFFFF; ">
                <div class="buscador" style="height:70px; width:100%; background-color: #F4F4F4;">
                <table runat="server" id="tblprincipal" style="width: 100%;  color: #000000;">
                    <tr>
                        <td style="text-align: right; width: 275px;">
                            &nbsp;</td>
                        <td style="text-align: right; width: 326px;">
                            <asp:Label ID="Label9" runat="server" Text="Tipo Documento:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 309px;">
                            <asp:DropDownList ID="ddltipodoc" runat="server" Enabled="False" Width="267px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: right; width: 326px;">
                            <asp:Label ID="Label1" runat="server" Text="Nro. Documento:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 253px;">
                            <asp:TextBox ID="txtnumerodoc" runat="server" Width="264px" MaxLength="16" AutoPostBack="True" Enabled="False"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="txtnumerodoc_FilteredTextBoxExtender" runat="server" Enabled="True" TargetControlID="txtnumerodoc" FilterType="Numbers">
                            </asp:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: left; width:275px" >
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 275px;">
                            &nbsp;</td>
                        <td style="text-align: right; width: 326px;">
                            <asp:Label ID="Label2" runat="server" Text="Nombres:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 309px;">
                            <asp:TextBox ID="txtnombres" runat="server" Width="264px" MaxLength="30" CssClass="upperCase" Enabled="False"></asp:TextBox>
                        </td>
                        <td style="text-align: right; width: 326px;">
                            <asp:Label ID="Label4" runat="server" Text="Apellidos:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 253px;">
                            <asp:TextBox ID="txtapellidos" runat="server" Width="264px" MaxLength="30" CssClass="upperCase" Enabled="False"></asp:TextBox>
                        </td>
                        <td style="text-align: left">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 275px; height: 18px;">
                            &nbsp;</td>
                        <td style="text-align: right; width: 326px; height: 18px;">
                            &nbsp;</td>
                        <td style="text-align: right; width: 309px; height: 18px;">
                            &nbsp;</td>
                        <td style="text-align: right; width: 326px; height: 18px;">
                            &nbsp;</td>
                        <td style="height: 18px; text-align: left; width: 253px;">
                            &nbsp;</td>
                        <td style="height: 18px; text-align: left;">
                            &nbsp;</td>
                    </tr>
                    </table>
                </div>
            </td>

        </tr>
        
    </table>
    <hr />
    <div style="width: 100%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: left; text-decoration: underline;">
        <asp:Label ID="Label3" runat="server" Font-Size="12pt">Historial de Sanciones</asp:Label>
    </div> 

                <table style="height:100%; width:100%">
                <tr  style="vertical-align:top">
                    <td style="height: 560px; ">
                        <table runat="server" id="Table1" style="width:100%">
                            <tr>
                                <td style="width: 100%; text-align:left; background-color:#FFFFFF">
                                   <%-- <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Width="100%" Height="320px">--%>
                                        <asp:GridView ID="grdvDatos" SkinID="grillamant" runat="server" 
                                                AutoGenerateColumns="False" CaptionAlign="Left"  backcolor="White"
                                                Width="100%" AllowSorting="True" EmptyDataText="No existe Historial de sanciones" 
                                                ForeColor="#333333" CellPadding="2" 
                                                CssClass="Texto_General" AllowPaging="True" OnPageIndexChanging="grdvDatos_PageIndexChanging" DataKeyNames="Codigo" > 
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Seleccionar">
                                                        <HeaderStyle CssClass="GVFixedHeader" />
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnselect" runat="server" Height="25px" ImageUrl="~/Botones/seleccionar.png" OnClick="btnselect_Click" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="GrupoSan" HeaderText="Grupo Sanción" >
                                                        <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                        <HeaderStyle CssClass="GVFixedHeader" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="TipoSan" HeaderText="Tipo Sanción" >
                                                        <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                        <HeaderStyle CssClass="GVFixedHeader" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="FecInicio" HeaderText="Fecha Inicio" >
                                                        <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                        <HeaderStyle CssClass="GVFixedHeader" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="FechaFin" HeaderText="Fecha Fin" >
                                                        <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                        <HeaderStyle CssClass="GVFixedHeader" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="FechaEfec" HeaderText="Fecha Efectiva" >
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
                                <td style="width: 90%">
                                    <div id="menuaccions" style="border-style: ridge double double ridge; width: 100%; background-color: #FFFFFF; border-top-width: 4px; border-top-color: #FF0000; border-left-color: #FF0000;">
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 385px">
                                                    &nbsp;</td>
                                                <td style="width: 133px">
                                                    <asp:ImageButton ID="btnsalir" runat="server" Height="60px" ImageUrl="~/Botones/Salir.png" OnClick="btnsalir_Click"  />
                                                </td>
                                                <td style="width:312px">
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


    
 
<%--        </ContentTemplate>
    </asp:UpdatePanel>--%>


    <script type="text/javascript">

        function CerrarSolo() {
            window.close();
        }
    </script>
</asp:Content>

