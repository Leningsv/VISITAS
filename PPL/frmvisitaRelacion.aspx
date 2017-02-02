<%@ Page Title="" Language="C#" MasterPageFile="~/Mantenimientos.master" AutoEventWireup="true" CodeFile="frmvisitaRelacion.aspx.cs" Inherits="PPL_frmvisitaRelacion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width: 100%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: center; text-decoration: underline;">
        <asp:Label ID="lbltitulo" runat="server"></asp:Label>
    </div>
    <ajaxToolkit:ToolkitScriptManager ID="smmantenimiento" runat="server">
    </ajaxToolkit:ToolkitScriptManager>

    <hr />
    <%--            <div>
                <table style="width: 100%; height: 28px;" id="tblopciones" runat="server" visible="false">
                    <tr>
                        <td style="height: 34px"></td>
                        <td style="width: 461px; height: 34px"></td>
                        <td style="width: 592px; text-align: right; height: 34px;">
                            &nbsp;</td>
                        <td style="width: 147px; text-align: left; height: 34px;">
                            &nbsp;</td>
                        <td style="width: 33px; text-align: center; height: 34px;">
                            &nbsp;</td>
                        <td style="text-align: left; width: 294px; height: 34px;">
                            &nbsp;</td>
                    </tr>
                </table>

            </div>--%>
        <div style="width: 100%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: left; text-decoration: underline;">
            <asp:Label ID="lbleti1" runat="server" Font-Size="12pt">datos ppl</asp:Label>
        </div>
            <div class="buscador" style="height:100px; width:60%; background-color: #F4F4F4;">
                <table style="width: 100%; color: #000000;">
                    <tr style="vertical-align: central">
                        <td style="text-align: left; width: 104px; height: 22px;">
                            <asp:Label ID="Label1" runat="server" Text="Tipo Documento:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 182px; height: 22px;">
                            <asp:DropDownList ID="ddltipodoc" runat="server" Enabled="False" Width="267px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 103px; text-align: left; height: 22px;">
                            <asp:Label ID="Label2" runat="server" Text="Nro. Documento:"></asp:Label>
                        </td>
                        <td style="height: 22px; text-align: left;">
                            <asp:TextBox ID="txtnumdoc" runat="server" Enabled="False" Width="264px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 104px; text-align: left; height: 26px;">
                            <asp:Label ID="Label6" runat="server" Text="Nombres:"></asp:Label>
                        </td>
                        <td style="width: 182px; text-align: left; height: 26px;">
                            <asp:TextBox ID="txtnombres" runat="server" Enabled="False" Width="264px"></asp:TextBox>
                        </td>
                        <td style="width: 103px; text-align: left; height: 26px;">
                            <asp:Label ID="Label7" runat="server" Text="Apellidos:"></asp:Label>
                        </td>
                        <td style="text-align: left; height: 26px;">
                            <asp:TextBox ID="txtapellidos" runat="server" Enabled="False" Width="264px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 104px; text-align: left;">
                            <asp:Label ID="Label8" runat="server" Text="Etapa:"></asp:Label>
                        </td>
                        <td style="width: 182px; text-align: right;">
                            <asp:TextBox ID="txtetapa" runat="server" Enabled="False" Width="264px"></asp:TextBox>
                        </td>
                        <td style="width: 103px; text-align: left;">
                            <asp:Label ID="Label9" runat="server" Text="Pabellón:"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtpabellon" runat="server" Enabled="False" Width="264px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="lblerror" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
                        </td>
                    </tr>
                </table>

            </div>
            <hr />
            <table style="color: #000000;">
                <tr style="vertical-align: central">
                    <td style="height: 18px; text-align: center; width: 152px;">
                        <asp:ImageButton ID="btnagregar" runat="server" ImageUrl="~/Botones/Aniadir.png" OnClick="btnagregar_Click" />
                    </td>
                </tr>
            </table>
            <hr />
            <div style="width: 100%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: left; text-decoration: underline;">
                <asp:Label ID="lbleti2" runat="server" Font-Size="12pt">visitantes autorizados</asp:Label>
            </div>
    <%--<asp:Panel ID="Panel1" runat="server" Height="250px" ScrollBars="Vertical" Width="100%" >--%>
            <table style="height:100%; width:100%">
                <tr style="vertical-align:top">
                    <td style="height: 560px; ">

                        <table runat="server" id="tblprincipal" style="width:100%">
                            <tr>
                                <td style="width: 100%; text-align:left; background-color:#FFFFFF">   
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                    <%--<asp:Panel ID="Panel1" runat="server" Height="250px" ScrollBars="Vertical" Width="100%" >--%>                                                                                                    
                                            <asp:GridView ID="grdvDatos" SkinID="grillamant" runat="server" 
                                                AutoGenerateColumns="False" CaptionAlign="Left"  backcolor="White"
                                                Width="100%" AllowSorting="True" EmptyDataText="La búsqueda no obtuvo ningún resultado" 
                                                ForeColor="#333333" 
                                                DataKeyNames="Codigo,TipoVisita,Parentesco" CellPadding="2" 
                                                PageSize="15" CssClass="Texto_General" OnRowDataBound="grdvDatos_RowDataBound" > 
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Seleccionar">
                                                        <HeaderStyle CssClass="GVFixedHeader" />
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnsel" runat="server" Height="25px" ImageUrl="~/Botones/seleccionar.png" OnClick="btnsel_Click" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="TipoDoc" HeaderText="Tipo Documento" >
                                                        <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                        <HeaderStyle CssClass="GVFixedHeader" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>

                                                    <asp:BoundField DataField="NumDocu" HeaderText="Num. Documento" >
                                                        <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                        <HeaderStyle CssClass="GVFixedHeader" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>

                                                    <asp:BoundField DataField="Visitante" HeaderText="Visitante" >
                                                        <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                        <HeaderStyle CssClass="GVFixedHeader" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Parentesco">
                                                        <HeaderStyle CssClass="GVFixedHeader" />
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddltipoparent" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddltipoparent_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tipo Visita">
                                                        <HeaderStyle CssClass="GVFixedHeader" />
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddltipovisita" runat="server" AutoPostBack="True" Enabled="False">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                                 
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
                                <td style="text-align: left; color: #FF0000;">

                                    <asp:Label ID="lblmsj1" runat="server" Text="Label" ForeColor="#000099"></asp:Label>

                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; color: #FF0000;">

                                    <asp:Label ID="lblmsj2" runat="server" Text="Label" ForeColor="#000099"></asp:Label>

                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; color: #FF0000;">

                                    <asp:Label ID="lblmsj3" runat="server" Text="Label" ForeColor="#000099"></asp:Label>

                                </td>
                            </tr>
                            <tr>
                                <td style="width: 90%">
                                    <div id="menuaccions" style="border-style: ridge double double ridge; width: 100%; background-color: #FFFFFF; border-top-width: 4px; border-top-color: #FF0000; border-left-color: #FF0000;">
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width:100%">
                                                    <asp:ImageButton ID="btnsalir" runat="server" Height="60px" ImageUrl="~/Botones/Salir.png" CausesValidation="False" OnClick="btnsalir_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width:100%">
                                                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="503px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="827px">
                                                        <LocalReport ReportPath="PPL\repVISITAS.rdlc">
                                                            <DataSources>
                                                                <rsweb:ReportDataSource DataSourceId="ods1" Name="DataSet1" />
                                                            </DataSources>
                                                        </LocalReport>
                                                    </rsweb:ReportViewer>
                                                    <asp:ObjectDataSource ID="ods1" runat="server" SelectMethod="Clone" TypeName="dsReporteVisitas"></asp:ObjectDataSource>
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

