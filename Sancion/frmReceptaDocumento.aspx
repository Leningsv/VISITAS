<%@ Page Title="" Language="C#" MasterPageFile="~/Mantenimientos.master" AutoEventWireup="true" CodeFile="frmReceptaDocumento.aspx.cs" Inherits="Sancion_frmReceptaDocumento" %>
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
            <div style="width: 90%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: left; text-decoration: underline;">
                <asp:Label ID="lbleti2" runat="server" Font-Size="12pt">Documentos Recibidos</asp:Label>
            </div>
            <div>
                <table style="align-content:center; color: #000000;">
                    <tr>
                        <td style="text-align: right; width: 102px; height: 22px;">
                            <asp:Label ID="Label1" runat="server" Text="Cédulas:"></asp:Label>
                        </td>
                        <td style="text-align: center; width: 50px; height: 22px;">
                            <asp:Label ID="lblcedula" runat="server" Font-Size="Larger" Text="0"></asp:Label>
                        </td>
                        <td style="width: 85px; text-align: right; height: 22px;">
                            <asp:Label ID="Label2" runat="server" Text="Pasaportes:"></asp:Label>
                        </td>
                        <td style="height: 22px; text-align: center; width: 48px;">
                            <asp:Label ID="lblpasaporte" runat="server" Font-Size="Larger" Text="0"></asp:Label>
                        </td>
                        <td style="height: 22px; text-align: right; width: 55px;">
                            <asp:Label ID="lblcedula1" runat="server" Text="Otros:"></asp:Label>
                        </td>
                        <td style="height: 22px; text-align: center; width: 51px;">
                            <asp:Label ID="lblotros" runat="server" Font-Size="Larger" Text="0"></asp:Label>
                        </td>
                    </tr>
                </table>

            </div>
            <hr />
            <table style="height:100%; width:100%">
                <tr  style="vertical-align:top">
                    <td style="height: 560px; ">
                        <table runat="server" id="tblprincipal" style="width:100%">
                            <tr>
                                <td style="width: 100%; text-align:left; background-color:#FFFFFF">
                                    <%--<asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Width="100%" Height="320px">--%>
                                        <asp:GridView ID="grdvDatos" SkinID="grillamant" runat="server" 
                                                AutoGenerateColumns="False" CaptionAlign="Left"  backcolor="White"
                                                Width="100%" AllowSorting="True" EmptyDataText="No existen visitas ingresadas" 
                                                ForeColor="#333333" CellPadding="2" 
                                                CssClass="Texto_General" AllowPaging="True" OnPageIndexChanging="grdvDatos_PageIndexChanging" DataKeyNames="CodigoVis,CodigoPPl,Etapa,TipoVisita,CodVisitante" > 
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Seleccionar">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnselect" runat="server" Height="25px" ImageUrl="~/Botones/seleccionar.png" OnClick="btnselect_Click" />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GVFixedHeader" />
                                                    </asp:TemplateField>
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
                                                    <asp:BoundField DataField="PPL" HeaderText="PPL a Visitar" >
                                                        <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                        <HeaderStyle CssClass="GVFixedHeader" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Motivo" HeaderText="Motivo" >
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
                                                <td style="width:100%">
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

