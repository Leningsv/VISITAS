<%@ Page Title="" Language="C#" MasterPageFile="~/Mantenimientos.master" AutoEventWireup="true" CodeFile="frmrepRelacionPV.aspx.cs" Inherits="Reportes_frmrepRelacionPV" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width: 100%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: center; text-decoration: underline;">
        <asp:Label ID="lbltitulo" runat="server"></asp:Label>
    </div>
    <ajaxToolkit:ToolkitScriptManager ID="smmantenimiento" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <hr />
    <table >
        <tr style="vertical-align: central">
            <td >
                <table runat="server" id="tblprincipal" style="align-content :center;  color: #000000;">
                    <tr>
                        <td style="text-align: left; width: 50px;">
                            <asp:Label ID="Label4" runat="server" Text="Etapa:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 163px;">
                            <asp:DropDownList ID="ddlEtapa" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEtapa_SelectedIndexChanged" Width="230px">
                            </asp:DropDownList>
                                </td>
                        <td style="text-align: left; width: 61px;">
                            <asp:Label ID="Label5" runat="server" Text="Pabellon:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 167px;">
                            <asp:DropDownList ID="ddlPabellon" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPabellon_SelectedIndexChanged" Width="230px">
                            </asp:DropDownList>
                            </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 50px;">
                            <asp:Label ID="Label6" runat="server" Text="Ala:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 163px;">
                            <asp:DropDownList ID="ddlAla" runat="server" Width="230px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; width: 61px;">
                            &nbsp;</td>
                        <td style="text-align: left; width: 167px;">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 50px;">
                            &nbsp;</td>
                        <td style="text-align: left; width: 163px;">
                            &nbsp;</td>
                        <td style="text-align: left; width: 61px;">
                            &nbsp;</td>
                        <td style="text-align: left; width: 167px;">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: center; " colspan="4">
                                <asp:ImageButton ID="btProcesar" runat="server" Height="20px" ImageUrl="~/Botones/Procesar.png" OnClick="btProcesar_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; " colspan="4">
                <asp:Label ID="lblerror" runat="server" Text="Ya existe la Planificacion para este  rango de fechas" ForeColor="Red" Visible="False"></asp:Label>
                            <br />
                        </td>
                    </tr>
                </table>
    <hr />
                <br />
                <table runat="server" id="tblDetalle" style="width: 100%;  color: #000000;" visible="True">
                    <tr>
                        <td style="text-align: center; ">
                            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="300px" Width="100%" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                                <LocalReport ReportEmbeddedResource="pry_visita.Reportes.rptVisitas.rdlc" ReportPath="Reportes\repRelacionPV.rdlc">
                                    <DataSources>
                                        <rsweb:ReportDataSource DataSourceId="ods1" Name="DataSet1" />
                                    </DataSources>
                                </LocalReport>
                            </rsweb:ReportViewer>
                            <asp:ObjectDataSource ID="ods1" runat="server" SelectMethod="Clone" TypeName="dsrepRelacionPV"></asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; ">
                                <asp:ImageButton ID="btnsalir" runat="server" Height="60px" ImageUrl="~/Botones/Salir.png" CausesValidation="False" OnClick="btnsalir_Click"  />
                            <br />
                        </td>
                    </tr>
                </table>
            </td>

        </tr>
        <tr >
            <td style="width: 90%">
                &nbsp;</td>
        </tr>
        
    </table>
</asp:Content>

