<%@ Page Title="" Language="C#" MasterPageFile="~/Mantenimientos.master" AutoEventWireup="true" CodeFile="frmsancionNew.aspx.cs" Inherits="Sancion_frmsancionNew" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width: 100%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: left; text-decoration: underline;">
        <asp:Label ID="lbltitulo" runat="server"></asp:Label>
    </div>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <hr />
    <table style="width: 100%; height: 100%">
        <tr style="vertical-align: top">
            <td style="border-style: double groove groove double; width: 100%; border-top-width: 1px; border-top-color: #FFFFFF; border-left-width: 1px; border-left-color: #FFFFFF; ">
                <table runat="server" id="tblprincipal" style="width: 100%;  color: #000000;">
                    <tr>
                        <td style="text-align: right; width: 326px;">
                            <asp:Label ID="Label1" runat="server" Text="*Descripción:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 408px;">
                            <asp:TextBox ID="txtdescrip" runat="server" Width="264px" MaxLength="100" CssClass="upperCase" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtdescrip" ErrorMessage="Campo Requerido..!" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                        <td style="text-align: left">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 326px; height: 18px;">
                            <asp:Label ID="Label2" runat="server" Text="Grupo Sanción:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 408px; height: 18px;">
                            <asp:DropDownList ID="ddlgrupo" runat="server" Width="267px">
                            </asp:DropDownList>
                        </td>
                        <td style="height: 18px; text-align: left;">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 326px; height: 18px;">
                            <asp:Label ID="Label4" runat="server" Text="Gravedad:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 408px; height: 18px;">
                            <asp:DropDownList ID="ddlgravedad" runat="server" Width="267px">
                            </asp:DropDownList>
                        </td>
                        <td style="height: 18px; text-align: left;">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 326px; height: 18px;">
                            <asp:Label ID="Label5" runat="server" Text="Tiempo Sanción:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 408px; height: 18px;">
                            <asp:TextBox ID="txttiempo" runat="server" Width="264px"></asp:TextBox>
                            <asp:NumericUpDownExtender ID="txttiempo_NumericUpDownExtender" runat="server" Enabled="True" Maximum="365" Minimum="1" RefValues="" ServiceDownMethod="" ServiceDownPath="" ServiceUpMethod="" Tag="" TargetButtonDownID="" TargetButtonUpID="" TargetControlID="txttiempo" Width="264">
                            </asp:NumericUpDownExtender>
                            <asp:Label ID="Label6" runat="server" Text="días"></asp:Label>
                        </td>
                        <td style="height: 18px; text-align: left;">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 326px;">
                            <asp:Label ID="Label3" runat="server" Text="Estado:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 408px;">
                            <asp:CheckBox ID="chkestado" runat="server" Checked="True" Enabled="False" Text="Activo" />
                        </td>
                        <td style="text-align: left">
                            &nbsp;</td>
                    </tr>
                </table>
                <asp:Label ID="lblerror" runat="server" Text="Ya existe la Tarea Creada, por favor Cree otra " ForeColor="Red" Visible="False"></asp:Label>
            </td>

        </tr>
        <tr >
            <td style="width: 100%">
                <div id="menuaccions" class="menuaccions">
                    <table style="width: 100%">
                        <tr>
                            <td>

                                <asp:ImageButton ID="btngrabar" runat="server" Height="60px" ImageUrl="~/Botones/Grabar.png" OnClick="btngrabar_Click" />

                            </td>
                            <td>

                                <asp:ImageButton ID="btnsalir" runat="server" Height="60px" ImageUrl="~/Botones/Salir.png" CausesValidation="False" OnClick="btnsalir_Click" />

                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        
    </table>
</asp:Content>

