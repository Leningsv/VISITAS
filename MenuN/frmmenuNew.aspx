<%@ Page Title="" Language="C#" MasterPageFile="~/Mantenimientos.master" AutoEventWireup="true" CodeFile="frmmenuNew.aspx.cs" Inherits="MenuN_frmmenuNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width: 100%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: center; text-decoration: underline;">
        <asp:Label ID="lbltitulo" runat="server"></asp:Label>
    </div>
    <asp:ScriptManager ID="smmantenimiento" runat="server" AsyncPostBackTimeout="0"></asp:ScriptManager>
    <hr />
        <table>
        <tr style="vertical-align: central">
            <td >
                <table runat="server" id="tblprincipal" style="width: 100%;  color: #000000;">
                    <tr>
                        <td style="text-align: left; width: 130px;">
                            <asp:Label ID="Label1" runat="server" Text="*Nombre del Menú:"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtnombre" runat="server" Width="264px" MaxLength="80" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtnombre" ErrorMessage="Campo Requerido..!" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 130px;">
                            <asp:Label ID="Label3" runat="server" Text="Estado:"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:CheckBox ID="chkestado" runat="server" AutoPostBack="True" Checked="True" Enabled="False" Text="Activo" />
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
                            <asp:UpdatePanel ID="uppbuscagrilla" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>     
                                    <asp:Panel ID="Panel2" runat="server" ScrollBars="Vertical" Height="320px" Width="100%">                               
                                        <asp:GridView ID="grdvDatos" runat="server" SkinID="grillamant" CaptionAlign="Left" 
                                            BackColor="White" EmptyDataText="No Existen Tareas Ingresadas" Width="100%" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" PageSize="100">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Agregar??">
                                                    <HeaderStyle CssClass="GVFixedHeader" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkAgregar" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CodigoTarea" HeaderText="Codigo Tarea" SortExpression="CodigoTarea" >
                                                    <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                    <HeaderStyle CssClass="GVFixedHeader" />
                                                    <ItemStyle HorizontalAlign="Left" />                                                    
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Tarea" HeaderText="Descripción" SortExpression="Tarea" >
                                                    <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                    <HeaderStyle CssClass="GVFixedHeader" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>    
                                                <asp:BoundField DataField="RutaTarea" HeaderText="Ruta/Pagina ASPX" SortExpression="RutaTarea" >
                                                    <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                    <HeaderStyle CssClass="GVFixedHeader" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="EstadoTarea" HeaderText="Estado" SortExpression="EstadoTarea" >
                                                    <ControlStyle ForeColor="Black" Font-Size="7pt" />
                                                    <HeaderStyle CssClass="GVFixedHeader" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                            </Columns>
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="GVFixedHeader" />  
                                        <RowStyle BackColor="#E3E4F2" Font-Names="Verdana" />
                                        <AlternatingRowStyle BackColor="White" Font-Names="Verdana" />         
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />        
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="White" Font-Bold="True" ForeColor="#333333" />       
                                        <EditRowStyle BackColor="#2461BF" />
                                        <PagerSettings Mode="NumericFirstLast" />
                                        </asp:GridView>  
                                    </asp:Panel>                                      
                                </ContentTemplate>
                            </asp:UpdatePanel>                            
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
                                            <asp:ImageButton ID="btnsalir" runat="server" Height="60px" ImageUrl="~/Botones/Salir.png" OnClick="btnsalir_Click" CausesValidation="False"  />
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

