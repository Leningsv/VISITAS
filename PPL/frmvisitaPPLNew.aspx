<%@ Page Title="" Language="C#" MasterPageFile="~/Mantenimientos.master" AutoEventWireup="true" CodeFile="frmvisitaPPLNew.aspx.cs" Inherits="PPL_frmvisitaPPLNew" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width: 90%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: left; text-decoration: underline;">
        <asp:Label ID="lbltitulo" runat="server"></asp:Label>
    </div>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <hr />
    <table style="width: 100%; height: 100%">
        <tr style="vertical-align: top">
            <td style="border-style: double groove groove double; width: 100%; border-top-width: 1px; border-top-color: #FFFFFF; border-left-width: 1px; border-left-color: #FFFFFF; ">
                <div class="buscador" style="height:100px; width:100%; background-color: #F4F4F4;">
                <table runat="server" id="tblprincipal" style="width: 100%;  color: #000000;">
                    <tr>
                        <td style="text-align: right; width: 326px; height: 18px;">
                            <asp:Label ID="Label4" runat="server" Text="*Apellidos/Nombres:"></asp:Label>
                        </td>
                        <td style="height: 18px; text-align: left;">
                            <asp:TextBox ID="txtnombre1" runat="server" Width="264px" MaxLength="180" CssClass="upperCase"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtnombre1" ErrorMessage="Campo Requerido..!" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 326px; height: 18px;">
                            <asp:Label ID="Label9" runat="server" Text="Parentesco:"></asp:Label>
                        </td>
                        <td style="height: 18px; text-align: left;">
                            <asp:DropDownList ID="ddlparentesco" runat="server" Width="267px" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 326px; height: 18px;">
                            <asp:Label ID="Label8" runat="server" Text="Observación:"></asp:Label>
                        </td>
                        <td style="height: 18px; text-align: left;">
                            <asp:TextBox ID="txtobserva" runat="server" Width="264px" MaxLength="255" CssClass="upperCase" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 18px;" colspan="2">
                                            <asp:GridView ID="grdvDatos" SkinID="grillamant" runat="server" 
                                                AutoGenerateColumns="False" CaptionAlign="Left"  backcolor="White"
                                                Width="100%" AllowSorting="True" EmptyDataText="La búsqueda no obtuvo ningún resultado" 
                                                ForeColor="#333333" 
                                                 CellPadding="2" 
                                                PageSize="15" CssClass="Texto_General"> 
                                                <Columns>
                                                    <asp:BoundField DataField="TipoDoc" HeaderText="Tipo Documento" >
                                                        <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                        <HeaderStyle CssClass="GVFixedHeader" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>

                                                    <asp:BoundField DataField="NumDoc" HeaderText="Num. Documento" >
                                                        <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                        <HeaderStyle CssClass="GVFixedHeader" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>

                                                    <asp:BoundField DataField="Visitante" HeaderText="Visitante" >
                                                        <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                        <HeaderStyle CssClass="GVFixedHeader" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <%--<asp:TemplateField HeaderText="Parentesco">
                                                        <HeaderStyle CssClass="GVFixedHeader" />
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddltipoparent" runat="server" AutoPostBack="True">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tipo Visita">
                                                        <HeaderStyle CssClass="GVFixedHeader" />
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddltipovisita" runat="server" AutoPostBack="True" Enabled="False">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>                                                 
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
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 326px; height: 18px;">
                            <asp:Label ID="Label2" runat="server" Text="Nro. Documento:" Visible="False"></asp:Label>
                        </td>
                        <td style="height: 18px; text-align: left;">
                            <asp:TextBox ID="txtndocu" runat="server" Width="264px" MaxLength="16" AutoPostBack="True" Visible="False"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="txtndocu_FilteredTextBoxExtender" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtndocu">
                            </asp:FilteredTextBoxExtender>
                            <asp:Label ID="lblmsj2" runat="server" ForeColor="Red" Text="Campo Requerido..!" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 326px; height: 18px;">
                            <asp:Label ID="Label5" runat="server" Text="Segundo Nombre:" Visible="False"></asp:Label>
                        </td>
                        <td style="height: 18px; text-align: left;">
                            <asp:TextBox ID="txtnombre2" runat="server" Width="264px" MaxLength="30" CssClass="upperCase" Visible="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 326px; height: 18px;">
                            <asp:Label ID="Label7" runat="server" Text="Apellido Materno:" Visible="False"></asp:Label>
                        </td>
                        <td style="height: 18px; text-align: left;">
                            <asp:TextBox ID="txtapellido2" runat="server" Width="264px" MaxLength="30" CssClass="upperCase" Visible="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 326px; height: 18px;">
                            <asp:Label ID="Label1" runat="server" Text="Tipo Documento:" Visible="False"></asp:Label>
                        </td>
                        <td style="height: 18px; text-align: left;">
                            <asp:DropDownList ID="ddltipodoc" runat="server" Width="267px" AutoPostBack="True" OnSelectedIndexChanged="ddltipodoc_SelectedIndexChanged" Visible="False">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddltipodoc" ErrorMessage="Campo Requerido..!" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 326px; height: 18px;">
                            <asp:Label ID="Label6" runat="server" Text="*Apellidos:" Visible="False"></asp:Label>
                        </td>
                        <td style="height: 18px; text-align: left;">
                            <asp:TextBox ID="txtapellido1" runat="server" Width="264px" MaxLength="90" CssClass="upperCase" Visible="False"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtapellido1" ErrorMessage="Campo Requerido..!" ForeColor="Red" Visible="False"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    </table>
                </div>
            </td>

        </tr>
        <tr >
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
                                <asp:ImageButton ID="btnsalir" runat="server" Height="60px" ImageUrl="~/Botones/Salir.png" CausesValidation="False" OnClick="btnsalir_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>        
    </table>
    <script type="text/javascript">
        function Cerrar() {
            window.opener.location.reload();
            window.close();
        }
        function CerrarSolo() {
            window.close();
        }
    </script>
</asp:Content>

