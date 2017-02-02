<%@ Page Title="" Language="C#" MasterPageFile="~/Mantenimientos.master" AutoEventWireup="true" CodeFile="frmvisitarelaAdmin.aspx.cs" Inherits="PPL_frmvisitarelaAdmin" %>
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
        </ContentTemplate>
    </asp:UpdatePanel>
            <hr />
            <div style="width: 100%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-align: left;">
                <asp:Label ID="Label1" runat="server" Font-Size="14pt" ForeColor="Red">Para crear una nueva relación presione el botón NUEVO</asp:Label>
            </div>
            <div style="width: 100%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: left; text-decoration: underline;">
                <asp:Label ID="lbleti3" runat="server" Font-Size="12pt">Lista de Visitantes</asp:Label>
            </div>
            <table style="height:100%; width:100%">
                <tr style="vertical-align:top">
                    <td style="height: 560px; ">
                        <table runat="server" id="tblprincipal" style="width:100%">
                            <tr>
                                <td style="width: 100%; text-align:left; background-color:#FFFFFF">     
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                    <asp:Panel ID="Panel1" runat="server" Height="200px" ScrollBars="Vertical" Width="100%">                                                                                                    
                                            <asp:GridView ID="grdvDatos" SkinID="grillamant" runat="server" 
                                                AutoGenerateColumns="False" CaptionAlign="Left"  backcolor="White"
                                                Width="100%" EmptyDataText="No existen visitantes registrados - Ingrese uno Nuevo" 
                                                ForeColor="#333333" 
                                                DataKeyNames="Codigo" CellPadding="2" 
                                                PageSize="15" CssClass="Texto_General" AllowPaging="True" OnPageIndexChanging="grdvDatos_PageIndexChanging" > 
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Seleccionar">
                                                        <HeaderStyle CssClass="GVFixedHeader" />
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnselec" runat="server" Height="25px" ImageUrl="~/Botones/seleccionar.png" OnClick="btnselec_Click" />
                                                        </ItemTemplate>
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
                                                    <asp:BoundField DataField="Estado" HeaderText="Estado" >
                                                        <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                        <HeaderStyle CssClass="GVFixedHeader" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CantSanciones" HeaderText="Cant. Sanciones" >
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
                                       </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                                                 
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%">
                                    <div id="menuaccions" style="border-style: ridge double double ridge; width: 100%; background-color: #FFFFFF; border-top-width: 4px; border-top-color: #FF0000; border-left-color: #FF0000;">
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 385px">
                                                    <asp:ImageButton ID="btnnuevo" runat="server" Height="60px" ImageUrl="~/Botones/Nuevo.png" CausesValidation="False" OnClick="btnnuevo_Click" />
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

                    </td>
                </tr>

            </table>

    <script type="text/javascript">
        function Cerrar() {
            window.opener.location.reload();
            //window.opener.location = "frmvisitaRelacion.aspx";
            window.close();
        }
        function CerrarSolo() {
            window.close();
        }
    </script>
</asp:Content>

