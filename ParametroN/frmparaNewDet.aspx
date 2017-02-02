<%@ Page Title="" Language="C#" MasterPageFile="~/Mantenimientos.master" AutoEventWireup="true" CodeFile="frmparaNewDet.aspx.cs" Inherits="ParametroN_frmparaNewDet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width: 100%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: center; text-decoration: underline;">
        <asp:Label ID="lbltitulo" runat="server"></asp:Label>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="0"></asp:ScriptManager>
    <hr />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table>
                <tr style="vertical-align: central">
                    <td>
                        <table runat="server" id="tblprincipal" style="width: 101%;  color: #000000;">
                            <tr>
                                <td style="text-align: left; width: 84px;">
                                    <asp:Label ID="Label1" runat="server" Text="*Nombre:"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtnombre" runat="server" Width="264px" MaxLength="80" CssClass="upperCase" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtnombre" ErrorMessage="Campo Requerido..!" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 84px; height: 18px;">
                                    <asp:Label ID="Label2" runat="server" Text="*Descripción:"></asp:Label>
                                </td>
                                <td style="height: 18px; text-align: left;">
                                    <asp:TextBox ID="txtdescri" runat="server" Width="264px" CssClass="upperCase" Height="48px" MaxLength="255" TextMode="MultiLine"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtdescri" ErrorMessage="Campo Requerido..!" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 84px; height: 18px;">
                                    <asp:Label ID="Label4" runat="server" Text="*Valor:"></asp:Label>
                                </td>
                                <td style="height: 18px; text-align: left;">
                                    <asp:TextBox ID="txtvalor" runat="server" Width="264px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtvalor" ErrorMessage="Campo Requerido..!" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 84px;">
                                    <asp:Label ID="Label3" runat="server" Text="Estado:"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:CheckBox ID="chkestado" runat="server" AutoPostBack="True" OnCheckedChanged="chkestado_CheckedChanged" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center; ">
                                    <asp:Label ID="lblerror" runat="server" ForeColor="Red" Text="Ya existe el Parámetro Creado, por favor Cree otro " Visible="False"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>

                </tr>
                <tr >
                    <td style="width: 100%">
                        <div id="menuaccions" style="border-style: ridge double double ridge; width: 100%; background-color: #FFFFFF; border-top-width: 4px; border-top-color: #FF0000; border-left-color: #FF0000;">
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 33%">
                                        <asp:ImageButton ID="btngrabar" runat="server" Height="60px" ImageUrl="~/Botones/Grabar.png" OnClick="btngrabar_Click" />
                                    </td>
                                    <td style="width:33%">
                                        <asp:ImageButton ID="btneliminar" runat="server" Height="60px" ImageUrl="~/Botones/eliminar.png" Visible="False" CausesValidation="False" OnClick="btneliminar_Click" OnClientClick="return confirm('Está Seguro de Eliminar Parámetro, algunas configuraciones pueden afectarse?');" />
                                    </td>
                                    <td style="width:34%">
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
</asp:Content>

