<%@ Page Title="" Language="C#" MasterPageFile="~/Mantenimientos.master" AutoEventWireup="true" CodeFile="frmvisitaporpplAdmin.aspx.cs" Inherits="VisitaPPL_frmvisitaporpplAdmin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../Controles/BuscarGrilla.ascx" tagname="BuscarGrilla" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width: 100%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: center; text-decoration: underline;">
        <asp:Label ID="lbltitulo" runat="server"></asp:Label>
    </div>
    <asp:ToolkitScriptManager ID="smmantenimiento" runat="server"></asp:ToolkitScriptManager>
    <hr />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="buscador" style="width:100%; background-color: #F4F4F4;">
               <table>
                   <tr style="vertical-align: central">
                       <td style="text-align: right">

                           <asp:Label ID="Label1" runat="server" Text="Buscar Por:"></asp:Label>

                       </td>
                       <td style="text-align: center; width: 125px;">

                           <asp:RadioButton ID="rbtvisitante" runat="server" Text="Visitante" AutoPostBack="True" OnCheckedChanged="rbtvisitante_CheckedChanged" />

                       </td>
                       <td style="text-align: left">
                           <asp:RadioButton ID="rdbppl" runat="server" Text="PPL" AutoPostBack="True" OnCheckedChanged="rdbppl_CheckedChanged" Checked="True" />
                       </td>
                   </tr>
               </table>
            </div>
            <table style="width: 100%; height: 80px" id="tblbuscador" runat="server" visible="false">
                    <tr>
                        <td>
                            <div class="buscador" style="height:58px; width:100%; background-color: #F4F4F4;">
                                <uc1:BuscarGrilla ID="ctrlbuscar" runat="server" />
                            </div>
                        </td>
                    </tr>
            </table>
            <table style="width: 100%; height: 130px" id="tblbusppl" runat="server" visible="false">
                    <tr>
                        <td>
                            <div class="buscador" style="height:70px; width:100%; background-color: #F4F4F4;">                                
                                <table>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td style="color: #000000">Buscar por:</td>
                                        <td style="text-align: left; width: 146px;">
                                            <asp:DropDownList ID="ddlseleccionar" runat="server" Width="136px">
                                                <asp:ListItem Value="NO">Nombres</asp:ListItem>
                                                <asp:ListItem Value="TD">Tipo Documento</asp:ListItem>
                                                <asp:ListItem Value="ND">Nro. Documento</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtnombres" runat="server" CssClass="upperCase" Width="120px"></asp:TextBox>
                                        </td>
                                        <td style="width: 111px">
                                            <asp:ImageButton ID="btnbuscar" runat="server" Height="65px" ImageUrl="~/Botones/Buscar.png" OnClick="btnbuscar_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 19px"></td>
                                        <td colspan="4">
                                            <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="Recuerde que para listar PPL, debe planificar las visitas"></asp:Label>
                                        </td>
                                        <td colspan="4">&nbsp;</td>
                                        <td colspan="4"></td>
                                        <td colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td style="width: 146px">&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td style="width: 111px">&nbsp;</td>
                                    </tr>
                                </table>
                                
                            </div>
                        </td>
                    </tr>
            </table>
            <div style="width: 100%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: left; text-decoration: underline;">
                <asp:Label ID="lbletiqueta" runat="server" Font-Size="12pt"></asp:Label>
            </div>
            <div>
                <table runat="server" id="tblvisitante" style="width:100%">
                            <tr>
                                <td style="width: 100%; text-align:left; background-color:#FFFFFF">
                                    <%--<asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Width="100%" Height="320px">--%>
                                        <asp:GridView ID="grdvDatos" SkinID="grillamant" runat="server" 
                                                AutoGenerateColumns="False" CaptionAlign="Left"  backcolor="White"
                                                Width="100%" AllowSorting="True" EmptyDataText="No existen visitantes registrados" 
                                                ForeColor="#333333" CellPadding="4" PageSize="13" 
                                                CssClass="Texto_General" AllowPaging="True" OnPageIndexChanging="grdvDatos_PageIndexChanging" > 
                                                <Columns>
                                                    <asp:HyperLinkField DataNavigateUrlFields="URLLINK" DataTextField="TipoDocu" HeaderText="Tipo Documento" >
                                                        <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                        <HeaderStyle CssClass="GVFixedHeader" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:HyperLinkField>
                                                    <asp:HyperLinkField DataNavigateUrlFields="URLLINK" DataTextField="NumDocu" HeaderText="Nro. Documento">
                                                        <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                        <HeaderStyle CssClass="GVFixedHeader" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:HyperLinkField>
                                                    <asp:HyperLinkField DataNavigateUrlFields="URLLINK" DataTextField="Visitante" HeaderText="Visitante">
                                                        <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                        <HeaderStyle CssClass="GVFixedHeader" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:HyperLinkField>
                                                    <asp:HyperLinkField DataNavigateUrlFields="URLLINK" DataTextField="Estado" HeaderText="Estado">
                                                        <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                        <HeaderStyle CssClass="GVFixedHeader" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:HyperLinkField>
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

                        </table>
            </div>
            <div>
                <table runat="server" id="tblppl" style="width:100%" visible="false">
                            <tr>
                                <td style="width: 100%; text-align:left; background-color:#FFFFFF">
                                    <%--<asp:Panel ID="Panel2" runat="server" ScrollBars="Vertical" Width="100%" Height="320px">--%>
                                        <asp:GridView ID="grdvPPL" SkinID="grillamant" runat="server" 
                                                AutoGenerateColumns="False" CaptionAlign="Left"  backcolor="White"
                                                Width="100%" AllowSorting="True" EmptyDataText="No existen PPL listados" 
                                                ForeColor="#333333" CellPadding="4" PageSize="13" 
                                                CssClass="Texto_General" AllowPaging="True" OnPageIndexChanging="grdvPPL_PageIndexChanging" > 
                                                <Columns>
                                                    <asp:HyperLinkField DataNavigateUrlFields="URLLINK" DataTextField="TipoDoc" HeaderText="Tipo Documento" >
                                                        <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                        <HeaderStyle CssClass="GVFixedHeader" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:HyperLinkField>
                                                    <asp:HyperLinkField DataNavigateUrlFields="URLLINK" DataTextField="NumDoc" HeaderText="Nro. Documento">
                                                        <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                        <HeaderStyle CssClass="GVFixedHeader" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:HyperLinkField>
                                                    <asp:HyperLinkField DataNavigateUrlFields="URLLINK" DataTextField="PPL" HeaderText="Nombres">
                                                        <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                        <HeaderStyle CssClass="GVFixedHeader" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:HyperLinkField>
                                                    <asp:HyperLinkField DataNavigateUrlFields="URLLINK" DataTextField="Etapa" HeaderText="Etapa">
                                                        <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                        <HeaderStyle CssClass="GVFixedHeader" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:HyperLinkField>
                                                    <asp:HyperLinkField DataNavigateUrlFields="URLLINK" DataTextField="Pabellon" SortExpression="Pabello" HeaderText="Pabellón">
                                                        <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                        <HeaderStyle CssClass="GVFixedHeader" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:HyperLinkField>
                                                    <asp:HyperLinkField DataNavigateUrlFields="URLLINK" DataTextField="Ala" HeaderText="Ala" >
                                                        <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                        <HeaderStyle CssClass="GVFixedHeader" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:HyperLinkField>
                                                    <asp:HyperLinkField DataNavigateUrlFields="URLLINK" DataTextField="Piso" HeaderText="Piso" >
                                                        <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                        <HeaderStyle CssClass="GVFixedHeader" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:HyperLinkField>
                                                    <asp:HyperLinkField DataNavigateUrlFields="URLLINK" DataTextField="Celda" HeaderText="Celda" >
                                                        <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                        <HeaderStyle CssClass="GVFixedHeader" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:HyperLinkField>
                                                    <asp:HyperLinkField DataNavigateUrlFields="URLLINK" DataTextField="Estado" HeaderText="Estado">
                                                        <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                        <HeaderStyle CssClass="GVFixedHeader" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:HyperLinkField>                                              
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

                        </table>
            </div>

            <table style="height:100%; width:100%">
                <tr>
                    <td style="width: 90%">
                        <div id="menuaccions" style="border-style: ridge double double ridge; width: 100%; background-color: #FFFFFF; border-top-width: 4px; border-top-color: #FF0000; border-left-color: #FF0000;">
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 39%">
                                        <asp:ImageButton ID="btningreso" runat="server" Height="60px" ImageUrl="~/Botones/Nuevo.png" OnClick="btningreso_Click" Visible="False"  />
                                    </td>
                                    <td style="width:26%">
                                        <asp:ImageButton ID="btnsalir" runat="server" Height="60px" ImageUrl="~/Botones/Salir.png" OnClick="btnsalir_Click"  />
                                    </td>
                                    <td style="width:50%">&nbsp;</td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>  
</asp:Content>

