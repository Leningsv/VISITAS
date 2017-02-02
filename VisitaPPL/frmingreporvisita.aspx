<%@ Page Title="" Language="C#" MasterPageFile="~/Mantenimientos.master" AutoEventWireup="true" CodeFile="frmingreporvisita.aspx.cs" Inherits="VisitaPPL_frmingreporvisita" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width: 100%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: center; text-decoration: underline;">
        <asp:Label ID="lbltitulo" runat="server"></asp:Label>
    </div>
    <asp:ToolkitScriptManager ID="smmantenimiento" runat="server"></asp:ToolkitScriptManager>
    <hr />

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>  
        <div class="buscador" style="height:100px; width:100%; background-color: #F4F4F4;">
            <table runat="server" id="tblprincipal" style="color: #000000;">
                <tr style="vertical-align: central">
                    <td style="text-align: left; width: 60px; ">
                        <asp:Label ID="Label1" runat="server" Text="Nombres:"></asp:Label>
                    </td>
                    <td style="text-align: left; ">
                        <asp:TextBox ID="txtnombre" runat="server" Width="264px" MaxLength="80" Enabled="False" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 60px;">
                        <asp:Label ID="Label2" runat="server" Text="Parentesco:" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlparentesco" runat="server" Width="267px" Enabled="False" Visible="False">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 60px;">
                        <asp:Label ID="Label3" runat="server" Text="Tipo Visita:"></asp:Label>
                    </td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddltipovisita" runat="server" Enabled="False" Width="267px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; " colspan="2">
                        <asp:Label ID="lblerror" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>

        <hr />
        <div style="width: 100%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: left; text-decoration: underline;">
            <asp:Label ID="lbleti2" runat="server" Font-Size="12pt">ppl a los que puede visitar</asp:Label>
        </div>
        <table style="width: 100%">
        <tr>
            <td>
                <table runat="server" id="tblsecundaria" style="width: 100%">
                    <tr>
                        <td style="width: 100%; text-align:left; background-color:#FFFFFF">    
<%--                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>  --%>                                                   
                                    <%--<asp:Panel ID="Panel2" runat="server" ScrollBars="Vertical" Height="320px" Width="100%"> --%>                              
                                        <asp:GridView ID="grdvDatos" runat="server" CaptionAlign="Left" BackColor="White" EmptyDataText="No Existen Datos de PPL" 
                                            Width="100%" AutoGenerateColumns="False" CellPadding="2" ForeColor="#333333" DataKeyNames="Codigo,Eta,Parente" AllowPaging="True" OnPageIndexChanging="grdvDatos_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Ingreso de Visita">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnselect" runat="server" Height="25px" ImageUrl="~/Botones/seleccionar_verde.png" OnClick="btnselect_Click" />
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="GVFixedHeader" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="TipoDocu" HeaderText="Tipo Documento" >
                                                    <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                    <HeaderStyle CssClass="GVFixedHeader" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NumDocu" HeaderText="Num. Documento" >
                                                    <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                    <HeaderStyle CssClass="GVFixedHeader" />
                                                    <ItemStyle HorizontalAlign="Left" />                                                    
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Nombres" HeaderText="Nombres" >
                                                    <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                    <HeaderStyle CssClass="GVFixedHeader" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>    
                                                <asp:BoundField DataField="Etapa" HeaderText="Etapa" >
                                                    <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                    <HeaderStyle CssClass="GVFixedHeader" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>  
                                                <asp:BoundField DataField="Pabellon" HeaderText="Pabellon" >
                                                    <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                    <HeaderStyle CssClass="GVFixedHeader" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>  
                                                <asp:BoundField DataField="Ala" HeaderText="Ala" >
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
<%--                                </ContentTemplate>
                            </asp:UpdatePanel> --%>                                                                                                
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%">
                            <div id="menuaccions" style="border-style: ridge double double ridge; width: 100%; background-color: #FFFFFF; border-top-width: 4px; border-top-color: #FF0000; border-left-color: #FF0000;">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width:100%">
                                            <asp:ImageButton ID="btnsalir" runat="server" Height="60px" ImageUrl="~/Botones/Salir.png" CausesValidation="False" OnClick="btnsalir_Click"  />
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

