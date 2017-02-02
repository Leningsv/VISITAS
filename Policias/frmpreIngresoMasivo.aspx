<%@ Page Title="" Language="C#" MasterPageFile="~/Mantenimientos.master" AutoEventWireup="true" CodeFile="frmpreIngresoMasivo.aspx.cs" Inherits="Policias_frmpreIngresoMasivo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width: 100%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: center; text-decoration: underline;">
        <asp:Label ID="lbltitulo" runat="server"></asp:Label>
    </div>
    <ajaxToolkit:ToolkitScriptManager ID="smmantenimiento" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <hr />
    <table style="width: 100%; height: 100%">
        <tr style="vertical-align: top">
            <td style="border-style: double groove groove double; width: 90%; border-top-width: 1px; border-top-color: #FFFFFF; border-left-width: 1px; border-left-color: #FFFFFF; ">
                <table runat="server" id="tblprincipal" style="align-content :center;  color: #000000;">
                    <tr>
                        <td style="text-align: right; width: 132px;">
                            <asp:Label ID="Label1" runat="server" Text="Nombre de Archivo:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 386px;">
                            <asp:FileUpload ID="FileUpload1" runat="server" />
&nbsp;
                                <asp:ImageButton ID="btProcesar" runat="server" Height="20px" ImageUrl="~/Botones/Procesar.png" OnClick="btProcesar_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; " colspan="2">
                <asp:Label ID="lblerror" runat="server" Text="Ya existe la Planificacion para este  rango de fechas" ForeColor="Red" Visible="False"></asp:Label>
                            <br />
                <asp:Label ID="lblExito" runat="server" Text="Guardado con Éxito" ForeColor="Green" Visible="False" Font-Bold="True"></asp:Label>
                            <br />
                        </td>
                    </tr>
                </table>
    <hr />
                <br />
                <table runat="server" id="tblDetalle" style="width: 100%;  color: #000000;" visible="True">
                    <tr>
                        <td style="text-align: center; ">
                            <asp:Label ID="lblDatosPolicias" runat="server" Text="Datos Policias" Font-Bold="True" Font-Size="Medium" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; ">
                            <asp:GridView ID="grdvDatos" runat="server" AllowSorting="True" AutoGenerateColumns="False" 
                                backcolor="White" CaptionAlign="Left" CellPadding="4" CssClass="Texto_General" 
                                EmptyDataText="No existen datos" ForeColor="#333333" PageSize="15" SkinID="grillamant" Width="100%">
                                <Columns>
                                    <asp:HyperLinkField DataTextField="TIPO DOCUMENTO" HeaderText="Tipo Documento" Target="_self">
                                    <ControlStyle Font-Size="7pt" ForeColor="Black" />
                                    <HeaderStyle CssClass="GVFixedHeader" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:HyperLinkField>
                                    <asp:HyperLinkField DataTextField="NRO DOCUMENTO" HeaderText="Nro. Documento">
                                    <ControlStyle Font-Size="7pt" ForeColor="Black" />
                                    <HeaderStyle CssClass="GVFixedHeader" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:HyperLinkField>
                                     <asp:HyperLinkField DataTextField="NOMBRES" HeaderText="Nombres">
                                    <ControlStyle Font-Size="7pt" ForeColor="Black" />
                                    <HeaderStyle CssClass="GVFixedHeader" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:HyperLinkField>
                                     <asp:HyperLinkField DataTextField="APELLIDOS" HeaderText="Apellidos">
                                    <ControlStyle Font-Size="7pt" ForeColor="Black" />
                                    <HeaderStyle CssClass="GVFixedHeader" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:HyperLinkField>
                                    <asp:HyperLinkField DataTextField="FECHA NACIMIENTO" HeaderText="Fecha Nacimiento">
                                    <ControlStyle Font-Size="7pt" ForeColor="Black" />
                                    <HeaderStyle CssClass="GVFixedHeader" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:HyperLinkField>
                                    <asp:HyperLinkField DataTextField="NOVEDADES" HeaderText="Novedades">
                                    <ControlStyle Font-Size="7pt" ForeColor="Black" />
                                    <HeaderStyle CssClass="GVFixedHeader" />
                                    <ItemStyle HorizontalAlign="Center" />
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
                            <br />
                        </td>
                    </tr>
                </table>
            </td>

        </tr>
        <tr >
            <td style="width: 90%">
                <div id="menuaccions" class="menuaccions">
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <asp:ImageButton ID="btngrabar" runat="server" Height="60px" ImageUrl="~/Botones/Grabar.png" OnClick="btngrabar_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="btCancelar" runat="server" Height="60px" ImageUrl="~/Botones/eliminar.png" OnClick="btEliminarPla_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="btnsalir" runat="server" Height="60px" ImageUrl="~/Botones/Salir.png" CausesValidation="False" OnClick="btnsalir_Click"  />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        
    </table>
</asp:Content>

