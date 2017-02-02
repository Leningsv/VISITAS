<%@ Page Title="" Language="C#" MasterPageFile="~/Mantenimientos.master" AutoEventWireup="true" CodeFile="frmsancionAplicar.aspx.cs" Inherits="Sancion_frmsancionAplicar" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width: 100%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: center; text-decoration: underline;">
        <asp:Label ID="lbltitulo" runat="server"></asp:Label>
    </div>    
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <%--<hr />--%>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <div style="width: 100%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: left; text-decoration: underline;" runat="server" visible="false">
        <asp:Label ID="lbleti1" runat="server" Font-Size="12pt">datos del visitante</asp:Label>
    </div>  
    <table style="width: 100%; height: 100%">
        <tr style="vertical-align: top">
            <td style="border-style: double groove groove double; width: 100%; border-top-width: 1px; border-top-color: #FFFFFF; border-left-width: 1px; border-left-color: #FFFFFF; ">
                <div class="buscador" style="height:80px; width:100%; background-color: #F4F4F4;" runat="server" visible="false">
                <table runat="server" id="tblprincipal" style="width: 100%;  color: #000000;">
                    <tr>
                        <td style="text-align: right; width: 275px;">
                            &nbsp;</td>
                        <td style="text-align: right; width: 326px;">
                            <asp:Label ID="Label9" runat="server" Text="Tipo Documento:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 309px;">
                            <asp:DropDownList ID="ddltipodoc" runat="server" Enabled="False" Width="267px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: right; width: 326px;">
                            <asp:Label ID="Label1" runat="server" Text="Nro. Documento:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 253px;">
                            <asp:TextBox ID="txtnumerodoc" runat="server" Width="264px" MaxLength="16" AutoPostBack="True" Enabled="False"></asp:TextBox>
                        </td>
                        <td style="text-align: left; width:275px" >
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 275px;">
                            &nbsp;</td>
                        <td style="text-align: right; width: 326px;">
                            <asp:Label ID="Label2" runat="server" Text="Nombres:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 309px;">
                            <asp:TextBox ID="txtnombres" runat="server" Width="264px" MaxLength="30" CssClass="upperCase" Enabled="False"></asp:TextBox>
                        </td>
                        <td style="text-align: right; width: 326px;">
                            <asp:Label ID="Label4" runat="server" Text="Apellidos:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 253px;">
                            <asp:TextBox ID="txtapellidos" runat="server" Width="264px" MaxLength="30" CssClass="upperCase" Enabled="False"></asp:TextBox>
                        </td>
                        <td style="text-align: left">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 275px; height: 18px;">
                            &nbsp;</td>
                        <td style="text-align: right; width: 326px; height: 18px;">
                            &nbsp;</td>
                        <td style="text-align: right; width: 309px; height: 18px;">
                            &nbsp;</td>
                        <td style="text-align: right; width: 326px; height: 18px;">
                            &nbsp;</td>
                        <td style="height: 18px; text-align: left; width: 253px;">
                            &nbsp;</td>
                        <td style="height: 18px; text-align: left;">
                            &nbsp;</td>
                    </tr>
                    </table>
                </div>
            </td>

        </tr>
        
    </table>
    <hr />
    <div style="width: 100%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: left; text-decoration: underline;">
        <asp:Label ID="Label3" runat="server" Font-Size="12pt">datos de sancion</asp:Label>
    </div> 

    <table style="width: 100%; height: 100%">
        <tr style="vertical-align: top">
            <td style="border-style: double groove groove double; width: 100%; border-top-width: 1px; border-top-color: #FFFFFF; border-left-width: 1px; border-left-color: #FFFFFF; ">
                <div class="buscador" style="height:100px; width:100%; background-color: #F4F4F4;">
                <table runat="server" id="Table1" style="width: 100%;  color: #000000;">
                    <tr>
                        <td style="text-align: right; width: 275px;">
                            &nbsp;</td>
                        <td style="text-align: left; width: 158px;">
                            <asp:Label ID="Label5" runat="server" Text="Grupo Sanción:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 309px;">
                            <asp:DropDownList ID="ddlgruposan" runat="server" Width="267px" Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: right; width: 326px;">
                            <asp:Label ID="Label6" runat="server" Text="Fecha Inicio:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 253px;">
                            <asp:TextBox ID="txtfechainicio" runat="server" Width="264px" MaxLength="16" AutoPostBack="True" Enabled="False"></asp:TextBox>
                        </td>
                        <td style="text-align: left; width:275px" >
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 275px;">
                            &nbsp;</td>
                        <td style="text-align: left; width: 158px;">
                            <asp:Label ID="Label15" runat="server" Text="Sancionado Por:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 309px;">
                            <asp:TextBox ID="txtsanciopor" runat="server" Width="264px" MaxLength="30" CssClass="upperCase" Enabled="False"></asp:TextBox>
                        </td>
                        <td style="text-align: right; width: 326px;">
                            &nbsp;</td>
                        <td style="text-align: left; width: 253px;">
                            &nbsp;</td>
                        <td style="text-align: left; width:275px" >
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 275px; height: 19px;">
                            </td>
                        <td style="text-align: left; width: 158px; height: 19px;">
                            <asp:Label ID="Label7" runat="server" Text="Observación:"></asp:Label>
                        </td>
                        <td style="text-align: left; height: 19px;" colspan="3">
                            <asp:TextBox ID="txtobservacion" runat="server" Width="500px" MaxLength="255" CssClass="upperCase" Enabled="False" TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td style="text-align: left; height: 19px;">
                            </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 275px; height: 18px;">
                            &nbsp;</td>
                        <td style="text-align: right; width: 158px; height: 18px;">
                            &nbsp;</td>
                        <td style="text-align: right; width: 309px; height: 18px;">
                            &nbsp;</td>
                        <td style="text-align: right; width: 326px; height: 18px;">
                            &nbsp;</td>
                        <td style="height: 18px; text-align: left; width: 253px;">
                            &nbsp;</td>
                        <td style="height: 18px; text-align: left;">
                            &nbsp;</td>
                    </tr>
                    </table>
                </div>
            </td>

        </tr>
        
    </table>
    <hr />
    <div style="width: 100%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: left; text-decoration: underline;">
        <asp:Label ID="Label8" runat="server" Font-Size="12pt">tiempo sanción</asp:Label>
    </div>
    <table style="width: 100%; height: 100%">
        <tr style="vertical-align: top">
            <td style="border-style: double groove groove double; width: 100%; border-top-width: 1px; border-top-color: #FFFFFF; border-left-width: 1px; border-left-color: #FFFFFF; ">
                <div class="buscador" style="height:130px; width:100%; background-color: #F4F4F4;">
                <table runat="server" id="Table2" style="width: 100%;  color: #000000;">
                    <tr>
                        <td style="text-align: right; width: 275px;">
                            &nbsp;</td>
                        <td style="text-align: left; width: 152px;">
                            <asp:Label ID="Label10" runat="server" Text="Tipo Sanción:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 309px;">
                            <asp:DropDownList ID="ddltiposanc" runat="server" Width="267px" AutoPostBack="True" OnSelectedIndexChanged="ddltiposanc_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; width: 349px;">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddltiposanc" ErrorMessage="Campo Requerido!" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                        <td style="text-align: right; width: 326px;">
                            &nbsp;</td>
                        <td style="text-align: left; width: 253px;">
                            &nbsp;</td>
                        <td style="text-align: left; width:275px" >
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 275px;">
                            &nbsp;</td>
                        <td style="text-align: left; width: 152px;">
                            <asp:Label ID="Label13" runat="server" Text="Tiempo:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 309px;">
                            <asp:TextBox ID="txttiempo" runat="server" Width="264px" MaxLength="16" AutoPostBack="True" Enabled="False"></asp:TextBox>
                        </td>
                        <td style="text-align: left; width: 349px;">
                            <asp:Label ID="Label14" runat="server" Text="días"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 326px;">
                            <asp:Label ID="Label11" runat="server" Text="Fecha Fin:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 253px;">
                            <asp:TextBox ID="txtfechafin" runat="server" Width="264px" MaxLength="16" AutoPostBack="True" Enabled="False"></asp:TextBox>
                        </td>
                        <td style="text-align: left; width:275px" >
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 275px; height: 19px;">
                            </td>
                        <td style="text-align: left; width: 152px; height: 19px;">
                            <asp:Label ID="Label16" runat="server" Text="Responsable:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 309px; height: 19px;">
                            <asp:TextBox ID="txtnombresan" runat="server" Width="264px" MaxLength="120" AutoPostBack="True" CssClass="upperCase" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td style="text-align: left; width: 349px; height: 19px;">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtnombresan" ErrorMessage="Ingrese Nombre" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                        <td style="text-align: right; width: 326px; height: 19px;">
                        </td>
                        <td style="text-align: left; width: 253px; height: 19px;">
                        </td>
                        <td style="text-align: left; width:275px; height: 19px;" >
                            </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 275px; height: 19px;">
                            </td>
                        <td style="text-align: left; width: 152px; height: 19px;">
                            <asp:Label ID="Label12" runat="server" Text="Observación:"></asp:Label>
                        </td>
                        <td style="text-align: left; height: 19px;" colspan="4">
                            <asp:TextBox ID="txtobservacion1" runat="server" Width="500px" MaxLength="255" CssClass="upperCase" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtobservacion1" ErrorMessage="Ingrese Observación" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                        <td style="text-align: left; height: 19px;">
                            </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 275px; height: 18px;">
                            &nbsp;</td>
                        <td style="text-align: right; width: 152px; height: 18px;">
                            &nbsp;</td>
                        <td style="text-align: left; width: 309px; height: 18px;">
                            &nbsp;</td>
                        <td style="text-align: right; width: 349px; height: 18px;">
                            &nbsp;</td>
                        <td style="text-align: right; width: 326px; height: 18px;">
                            &nbsp;</td>
                        <td style="height: 18px; text-align: left; width: 253px;">
                            &nbsp;</td>
                        <td style="height: 18px; text-align: left;">
                            &nbsp;</td>
                    </tr>
                    </table>
                </div>
            </td>

        </tr>
        
    </table>
    
        <table style="width: 100%; height: 100%">
            <tr >
                <td style="width: 100%">
                    <div id="menuaccions" style="border-style: ridge double double ridge; width: 100%; background-color: #FFFFFF; border-top-width: 4px; border-top-color: #FF0000; border-left-color: #FF0000;">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 385px">
                                    <asp:ImageButton ID="btngrabar" runat="server" Height="60px" ImageUrl="~/Botones/Grabar.png" OnClick="btngrabar_Click" />
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
            </ContentTemplate>
    </asp:UpdatePanel>


    <script type="text/javascript">
        function Cerrar() {
            window.opener.location.reload();
            window.close();
        }
    </script>
</asp:Content>

