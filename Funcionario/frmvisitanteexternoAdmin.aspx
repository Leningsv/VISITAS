<%@ Page Title="" Language="C#" MasterPageFile="~/Mantenimientos.master" AutoEventWireup="true" CodeFile="frmvisitanteexternoAdmin.aspx.cs" Inherits="Funcionario_frmvisitanteexternoAdmin" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../Controles/BuscarGrilla.ascx" tagname="BuscarGrilla" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width: 100%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: center; text-decoration: underline;">
        <asp:Label ID="lbltitulo" runat="server"></asp:Label>
    </div>
    
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <hr />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%; height: 80px">
                <tr>
                    <td>
                        <div class="buscador" style="height:58px; width:100%; background-color: #F4F4F4;">
                            <uc1:BuscarGrilla ID="ctrlbuscar" runat="server" />
                        </div>
                    </td>
                </tr>
            </table>

            <table style="height:100%; width:100%">
                <tr  style="vertical-align:top">
                    <td style="height: 560px; ">
                        <table runat="server" id="tblprincipal" style="width:100%">
                            <tr>
                                <td style="width: 100%; text-align:left; background-color:#FFFFFF">
                                    <%--<asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Width="100%" Height="320px">--%>
                                        <asp:GridView ID="grdvDatos" SkinID="grillamant" runat="server" 
                                                AutoGenerateColumns="False" CaptionAlign="Left"  backcolor="White"
                                                Width="100%" AllowSorting="True" EmptyDataText="No existe Visitantes Externos ingresados" 
                                                ForeColor="#333333" CellPadding="4" 
                                                CssClass="Texto_General" AllowPaging="True" OnPageIndexChanging="grdvDatos_PageIndexChanging" > 
                                                <Columns>
                                                    <asp:HyperLinkField DataNavigateUrlFields="URLLINK" DataTextField="TipoDoc" HeaderText="Tipo Documento" >
                                                        <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                        <HeaderStyle CssClass="GVFixedHeader" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:HyperLinkField>
                                                    <asp:HyperLinkField DataNavigateUrlFields="URLLINK" DataTextField="NumDoc" HeaderText="Num. Documento">
                                                        <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                        <HeaderStyle CssClass="GVFixedHeader" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:HyperLinkField>
                                                    <asp:HyperLinkField DataNavigateUrlFields="URLLINK" DataTextField="Visitante" HeaderText="Visitante">
                                                        <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                        <HeaderStyle CssClass="GVFixedHeader" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:HyperLinkField>
                                                    <asp:HyperLinkField DataNavigateUrlFields="URLLINK" DataTextField="TipoVis" HeaderText="Tipo Visitante">
                                                        <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                        <HeaderStyle CssClass="GVFixedHeader" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:HyperLinkField>
                                                    <asp:HyperLinkField DataNavigateUrlFields="URLLINK" DataTextField="Entidad" HeaderText="Institución">
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
                            <tr>
                                <td style="width: 90%">
                                    <div id="menuaccions" style="border-style: ridge double double ridge; width: 100%; background-color: #FFFFFF; border-top-width: 4px; border-top-color: #FF0000; border-left-color: #FF0000;">
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 385px">
                                                    <asp:ImageButton ID="btnnuevo" runat="server" Height="60px" ImageUrl="~/Botones/Nuevo.png" OnClick="btnnuevo_Click"  />
                                                </td>
                                                <td style="width: 133px">
                                                    &nbsp;</td>
                                                <td style="width:312px">
                                                    <asp:ImageButton ID="btnsalir" runat="server" Height="60px" ImageUrl="~/Botones/Salir.png" OnClick="btnsalir_Click"  />
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

