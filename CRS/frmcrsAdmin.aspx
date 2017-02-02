<%@ Page Title="" Language="C#" MasterPageFile="~/Mantenimientos.master" AutoEventWireup="true" CodeFile="frmcrsAdmin.aspx.cs" Inherits="CRS_frmcrsAdmin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../Controles/BuscarGrilla.ascx" tagname="BuscarGrilla" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width: 90%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: center; text-decoration: underline;">
        <asp:Label ID="lbltitulo" runat="server"></asp:Label>
    </div>
    <asp:ToolkitScriptManager ID="smmantenimiento" runat="server"></asp:ToolkitScriptManager>
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
                    <tr style="vertical-align:top">
                        <td style="height: 560px; ">
                            <table runat="server" id="tblprincipal" style="width:100%">
                                <tr>
                                    <td style="width: 100%; text-align:left; background-color:#FFFFFF">
                                        <asp:UpdatePanel ID="uppbuscagrilla" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>     
                                                <%--<asp:Panel ID="Panel1" runat="server" Height="320px" ScrollBars="Vertical" Width="100%" Wrap="False">--%>                                                                                                    
                                                <asp:GridView ID="grdvDatos" SkinID="grillamant" runat="server" 
                                                    AutoGenerateColumns="False" CaptionAlign="Left"  backcolor="White"
                                                    Width="100%" EmptyDataText="La búsqueda no obtuvo ningún resultado" 
                                                    ForeColor="#333333" CellPadding="2" CssClass="Texto_General" AllowPaging="True" DataKeyNames="Codigo" OnPageIndexChanging="grdvDatos_PageIndexChanging"  > 
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Seleccionar">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnselecc" runat="server" Height="25px" ImageUrl="~/Botones/seleccionar.png" OnClick="btnselecc_Click" />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GVFixedHeader" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="CRS" HeaderText="CRS" >
                                                            <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                            <HeaderStyle CssClass="GVFixedHeader" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="Ciudad" HeaderText="Ciudad" >
                                                            <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                            <HeaderStyle CssClass="GVFixedHeader" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Estado" HeaderText="Estado" >
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
                                            </ContentTemplate>
                                        </asp:UpdatePanel>                            
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 90%">
                                        <div id="menuaccions" style="border-style: ridge double double ridge; width: 100%; background-color: #FFFFFF; border-top-width: 4px; border-top-color: #FF0000; border-left-color: #FF0000;">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 39%">
                                                        <asp:ImageButton ID="btningreso" runat="server" Height="60px" ImageUrl="~/Botones/Nuevo.png" OnClick="btningreso_Click" Visible="False"  />
                                                    </td>
                                                    
                                                    <td style="width:22%">
                                                        <asp:ImageButton ID="btnsalir" runat="server" Height="60px" ImageUrl="~/Botones/Salir.png" OnClick="btnsalir_Click" />
                                                    </td>
                                                    <td>&nbsp;</td>
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

