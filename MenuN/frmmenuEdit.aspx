<%@ Page Title="" Language="C#" MasterPageFile="~/Mantenimientos.master" AutoEventWireup="true" CodeFile="frmmenuEdit.aspx.cs" Inherits="MenuN_frmmenuEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width: 100%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: center; text-decoration: underline;">
        <asp:Label ID="lbltitulo" runat="server"></asp:Label>
    </div>
    <%--<asp:ScriptManager ID="smmantenimiento" runat="server" AsyncPostBackTimeout="0"></asp:ScriptManager>--%>
    <asp:ToolkitScriptManager ID="smmantenimiento" runat="server"></asp:ToolkitScriptManager>
    <hr />

    <table>
        <tr style="vertical-align: central">
            <td>
                <table runat="server" id="tblprincipal" style="width: 100%;  color: #000000;">
                    <tr>
                        <td style="text-align: left; width: 124px;">
                            &nbsp;</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtcodigo" runat="server" Width="264px" MaxLength="80" Enabled="False" Visible="False" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 124px;">
                            <asp:Label ID="Label1" runat="server" Text="*Nombre del Menú:"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtnombre" runat="server" Width="264px" MaxLength="80" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtnombre" ErrorMessage="Campo Requerido..!" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 124px;">
                            <asp:Label ID="Label3" runat="server" Text="Estado:"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:CheckBox ID="chkestado" runat="server" AutoPostBack="True" OnCheckedChanged="chkestado_CheckedChanged" />
                        </td>
                    </tr>
                </table>
                <asp:Label ID="lblerror" runat="server" Text="El nombre del menú ingresado ya existe" ForeColor="Red" Visible="False"></asp:Label>
            </td>

        </tr>
        
    </table>

    <table style="width: 100%; color:black">
        <tr>
            <td>SELECCIONE LAS TAREAS PARA ESTE MENU</td>
        </tr>
    </table>


    <table style="width: 100%">
        <tr>
            <td>
                <table runat="server" id="tblsecundaria" style="width: 100%">
                    <tr>
                        <td style="width: 100%; text-align:left; background-color:#FFFFFF">                          
<%--                            <asp:UpdatePanel ID="uppbuscagrilla" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>   --%>  
                                    <asp:Panel ID="Panel2" runat="server" ScrollBars="Vertical" Height="320px" Width="100%">                               
                                        <asp:GridView ID="grdvDatos" runat="server" SkinID="grillamant" CaptionAlign="Left" BackColor="White" 
                                            EmptyDataText="No Existen Tareas Ingresadas" Width="100%" AutoGenerateColumns="False" CellPadding="2" ForeColor="#333333" PageSize="100" DataKeyNames="Checking,CodigoTarea">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Agregar??">
                                                    <HeaderStyle CssClass="GVFixedHeader" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkAgregar" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CodigoTarea" HeaderText="Codigo Tarea" SortExpression="CodigoTarea" Visible="false" >
                                                    <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                    <HeaderStyle CssClass="GVFixedHeader" />
                                                    <ItemStyle HorizontalAlign="Left" />                                                    
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Tarea" HeaderText="Tarea" SortExpression="Tarea" >
                                                    <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                    <HeaderStyle CssClass="GVFixedHeader" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>    
                                                <asp:BoundField DataField="Ruta" HeaderText="Ruta del Programa" SortExpression="Ruta" >
                                                    <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                    <HeaderStyle CssClass="GVFixedHeader" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="EstadoTarea" HeaderText="Estado Tarea" >
                                                    <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                    <HeaderStyle CssClass="GVFixedHeader" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>  
                                                <asp:TemplateField HeaderText="Subir Nivel">
                                                    <HeaderStyle CssClass="GVFixedHeader" />
                                                    <ItemTemplate>                                                    
                                                        <asp:ImageButton ID="imgSubirNivel" runat="server" Height="20px" ImageUrl="~/Botones/activada up.png" Width="20px" OnClick="imgSubirNivel_Click" />
                                                    </ItemTemplate>
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
                                    </asp:Panel>                                      
<%--                                </ContentTemplate>
                            </asp:UpdatePanel>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%">
                            <div id="menuaccions" style="border-style: ridge double double ridge; width: 100%; background-color: #FFFFFF; border-top-width: 4px; border-top-color: #FF0000; border-left-color: #FF0000;">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 385px">
                                            <asp:ImageButton ID="btningreso" runat="server" Height="60px" ImageUrl="~/Botones/Grabar.png" OnClick="btningreso_Click" />
                                        </td>
                                        <td style="width: 133px">
                                            &nbsp;</td>
                                        <td style="width:312px">
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
</asp:Content>

