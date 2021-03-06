﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Mantenimientos.master" AutoEventWireup="true" CodeFile="frmpoliEdit.aspx.cs" Inherits="Politicas_frmpoliEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width: 100%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: center; text-decoration: underline;">
        <asp:Label ID="lbltitulo" runat="server"></asp:Label>
    </div>
    <asp:ScriptManager ID="smmantenimiento" runat="server" AsyncPostBackTimeout="0"></asp:ScriptManager>

    <hr />
    <asp:UpdatePanel ID="uppbuscagrilla" runat="server" UpdateMode="Conditional">
        <ContentTemplate> 
        <table >
        <tr style="vertical-align: central">
            <td>
                <table runat="server" id="tblprincipal" style="width: 100%;  color: #000000;">
                    <tr>
                        <td style="text-align: left; width: 126px;">
                            &nbsp;</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtcodigo" runat="server" Width="264px" MaxLength="80" Enabled="False" Visible="False" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 126px;">
                            <asp:Label ID="Label1" runat="server" Text="*Parámetro:"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtparametro" runat="server" Width="264px" MaxLength="80" CssClass="upperCase" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtparametro" ErrorMessage="Campo Requerido..!" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 126px;">
                            <asp:Label ID="Label4" runat="server" Text="*Descripción:"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtdescrip" runat="server" Width="264px" MaxLength="255" CssClass="upperCase" TextMode="MultiLine" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtdescrip" ErrorMessage="Campo Requerido..!" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 126px;">
                            <asp:CheckBox ID="chkeliminar" runat="server" Enabled="False" Text="Permitir Eliminar" />
                        </td>
                        <td style="text-align: left">
                            <asp:CheckBox ID="chkcrear" runat="server" Enabled="False" Text="Permitir Crear" />
                            <asp:CheckBox ID="chkmodificar" runat="server" Enabled="False" Text="Permitir Modificar" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 126px;">
                            <asp:Label ID="Label3" runat="server" Text="Estado:"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:CheckBox ID="chkestado" runat="server" AutoPostBack="True" OnCheckedChanged="chkestado_CheckedChanged" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center; ">
                            <asp:Label ID="lblerror" runat="server" ForeColor="Red" Text="El nombre de la Política ingresada ya existe" Visible="False"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>

        </tr>
        
    </table>

        <table style="width: 100%">
        <tr>
            <td>
                <table runat="server" id="tblsecundaria" style="width: 100%">
                    <tr>
                        <td style="width: 100%; text-align:left; background-color:#FFFFFF">                              
                            <%--<asp:Panel ID="Panel2" runat="server" ScrollBars="Vertical" Height="320px" Width="100%">--%>                               
                                <asp:GridView ID="grdvDatos" runat="server" SkinID="grillamant" CaptionAlign="Left" BackColor="White" 
                                    EmptyDataText="No Existen Parámetros Ingresados" Width="100%" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" DataKeyNames="Codigo" AllowPaging="True" OnPageIndexChanging="grdvDatos_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Seleccionar">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnselecc" runat="server" Height="25px" ImageUrl="~/Botones/seleccionar.png" OnClick="btnselecc_Click" />
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="GVFixedHeader" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Nombre" HeaderText="Parámetro" >
                                            <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                            <HeaderStyle CssClass="GVFixedHeader" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField >
                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" >
                                            <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                            <HeaderStyle CssClass="GVFixedHeader" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField >
                                        <asp:BoundField DataField="Estado" HeaderText="Estado" >
                                            <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                            <HeaderStyle CssClass="GVFixedHeader" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField >
                                        <asp:TemplateField HeaderText="Subir Nivel">
                                            <HeaderStyle CssClass="GVFixedHeader" />
                                            <ItemTemplate>                                                    
                                                <asp:ImageButton ID="imgSubirNivel" runat="server" Height="20px" ImageUrl="~/Botones/activada up.png" Width="20px" OnClick="imgSubirNivel_Click" />
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="GVFixedHeader" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bajar Nivel">
                                            <HeaderStyle CssClass="GVFixedHeader" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgBajarNivel" runat="server" Height="20px" ImageUrl="~/Botones/activada down.png" Width="20px" OnClick="imgBajarNivel_Click" />
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
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%">
                            <div id="menuaccions" style="border-style: ridge double double ridge; width: 100%; background-color: #FFFFFF; border-top-width: 4px; border-top-color: #FF0000; border-left-color: #FF0000;">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 33%">
                                            <asp:ImageButton ID="btngrabar" runat="server" Height="60px" ImageUrl="~/Botones/Grabar.png" OnClick="btngrabar_Click" />
                                        </td>
                                        <td style="width: 33%">
                                            <asp:ImageButton ID="btningresar" runat="server" Height="60px" ImageUrl="~/Botones/Nuevo.png" OnClick="btningresar_Click" />
                                        </td>
                                        <td style="width:34%">
                                            <asp:ImageButton ID="btnsalir" runat="server" Height="60px" ImageUrl="~/Botones/Salir.png" OnClick="btnsalir_Click"  />
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

