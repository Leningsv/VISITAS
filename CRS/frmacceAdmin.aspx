﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Mantenimientos.master" AutoEventWireup="true" CodeFile="frmacceAdmin.aspx.cs" Inherits="CRS_frmacceAdmin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../Controles/BuscarGrilla.ascx" tagname="BuscarGrilla" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width: 90%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: center; text-decoration: underline;">
        <asp:Label ID="lbltitulo" runat="server"></asp:Label>
    </div>
    <asp:ToolkitScriptManager ID="smmantenimiento" runat="server"></asp:ToolkitScriptManager>
    <hr />

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
                                    <%--<asp:Panel ID="Panel1" runat="server" Height="320px" ScrollBars="Vertical" Width="100%">--%>                                                                                                    
                                    <asp:GridView ID="grdvDatos" SkinID="grillamant" runat="server" 
                                        AutoGenerateColumns="False" CaptionAlign="Left"  backcolor="White"
                                        Width="100%" EmptyDataText="No existen Accesos Creados" 
                                        ForeColor="#333333" CellPadding="4" CssClass="Texto_General" AllowPaging="True" OnPageIndexChanging="grdvDatos_PageIndexChanging"  > 
                                        <Columns>
                                            <asp:HyperLinkField DataNavigateUrlFields="URLLINK" DataTextField="Codigo" HeaderText="Codigo Acceso" Visible="false" >
                                                <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                <HeaderStyle CssClass="GVFixedHeader" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataNavigateUrlFields="URLLINK" DataTextField="Crs" HeaderText="CRS" >
                                                <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                <HeaderStyle CssClass="GVFixedHeader" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataNavigateUrlFields="URLLINK" DataTextField="Acceso" HeaderText="Acceso" >
                                                <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                <HeaderStyle CssClass="GVFixedHeader" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataNavigateUrlFields="URLLINK" DataTextField="Etapa" HeaderText="Etapa" >
                                                <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                <HeaderStyle CssClass="GVFixedHeader" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataNavigateUrlFields="URLLINK" DataTextField="Estado" HeaderText="Estado" >
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
                                </ContentTemplate>
                            </asp:UpdatePanel>                            
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 90%">
                            <div id="menuaccions" style="border-style: ridge double double ridge; width: 100%; background-color: #FFFFFF; border-top-width: 4px; border-top-color: #FF0000; border-left-color: #FF0000;">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 385px">
                                            <asp:ImageButton ID="btningreso" runat="server" Height="60px" ImageUrl="~/Botones/Nuevo.png" OnClick="btningreso_Click"  />
                                        </td>
                                        <td style="width: 133px">
                                            &nbsp;</td>
                                        <td style="width:312px">
                                            <asp:ImageButton ID="btnsalir" runat="server" Height="60px" ImageUrl="~/Botones/Salir.png" OnClick="btnsalir_Click" />
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
</asp:Content>

