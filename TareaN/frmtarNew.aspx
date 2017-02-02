<%@ Page Title="" Language="C#" MasterPageFile="~/Mantenimientos.master" AutoEventWireup="true" CodeFile="frmtarNew.aspx.cs" Inherits="TareaN_frmtarNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width: 90%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: center; text-decoration: underline;">
        <asp:Label ID="lbltitulo" runat="server"></asp:Label>
    </div>
    <hr />
    <table>
        <tr style="vertical-align: central">
            <td>
                <table runat="server" id="tblprincipal" style="width: 91%;  color: #000000;">
                    <tr>
                        <td style="text-align: left; width: 138px;">
                            <asp:Label ID="Label1" runat="server" Text="*Nombre de la Tarea:"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txttarea" runat="server" Width="264px" MaxLength="80" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txttarea" ErrorMessage="Campo Requerido..!" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 138px; height: 18px;">
                            <asp:Label ID="Label2" runat="server" Text="*Ruta/Páginas.aspx:"></asp:Label>
                        </td>
                        <td style="height: 18px; text-align: left;">
                            <asp:TextBox ID="txtruta" runat="server" Width="264px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtruta" ErrorMessage="Campo Requerido..!" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 138px;">
                            <asp:Label ID="Label3" runat="server" Text="Estado:"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:CheckBox ID="chkestado" runat="server" AutoPostBack="True" Checked="True" Enabled="False" Text="Activo" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; " colspan="2">
                <asp:Label ID="lblerror" runat="server" Text="Ya existe la Tarea Creada, por favor Cree otra " ForeColor="Red" Visible="False"></asp:Label>
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

                                <asp:ImageButton ID="btnsalir" runat="server" Height="60px" ImageUrl="~/Botones/Salir.png" CausesValidation="False" OnClick="btnsalir_Click" />

                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        
    </table>
</asp:Content>

