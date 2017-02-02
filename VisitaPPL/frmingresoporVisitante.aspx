<%@ Page Title="" Language="C#" MasterPageFile="~/Mantenimientos.master" AutoEventWireup="true" CodeFile="frmingresoporVisitante.aspx.cs" Inherits="VisitaPPL_frmingresoporVisitante" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <script type="text/javascript">
        function Refrescar() {
            __doPostBack('btnrefrescar_Click', '');
        }

    </script>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div style="width: 90%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: center; text-decoration: underline;">
                <asp:Label ID="lbltitulo" runat="server"></asp:Label>
            </div>

            <hr />
            <div style="width: 100%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: left; text-decoration: underline;">
                <asp:Label ID="lbleti2" runat="server" Font-Size="12pt">datos ppl</asp:Label>
            </div>
            <div class="buscador" style="height: 110px; width: 100%; background-color: #F4F4F4;">
                <table style="color: #000000; align-content :center">
                    <tr>
                        <td style="width: 92px; text-align: left;">
                            <asp:Label ID="Label8" runat="server" Text="Nombres:"></asp:Label>
                        </td>
                        <td style="width: 175px; text-align: left;">
                            <asp:TextBox ID="txtnombres" runat="server" AutoPostBack="True" Width="264px" Enabled="False"></asp:TextBox>
                        </td>
                        <td style="width: 68px; text-align: left;">
                            <asp:Label ID="Label18" runat="server" Text="Apellidos:"></asp:Label>
                        </td>
                        <td style="width: 170px; text-align: left;">
                            <asp:TextBox ID="txtapellidos" runat="server" AutoPostBack="True" Width="264px" Enabled="False"></asp:TextBox>
                        </td>
                        
                    </tr>
                    <tr>
                        <td style="width: 92px; text-align: left;">
                            <asp:Label ID="Label13" runat="server" Text="Etapa:"></asp:Label>
                        </td>
                        <td style="width: 175px; text-align: left;">
                            <asp:TextBox ID="txtetapa" runat="server" Width="264px" AutoPostBack="True" Enabled="False"></asp:TextBox>
                        </td>
                        <td style="width: 68px; text-align: left;">
                            <asp:Label ID="Label16" runat="server" Text="Pabellón:"></asp:Label>
                        </td>
                        <td style="width: 170px; text-align: left;">
                            <asp:TextBox ID="txtpabellon" runat="server" AutoPostBack="True" Width="264px" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 92px; text-align: left; height: 19px;">
                            <asp:Label ID="Label19" runat="server" Text="Tipo de Visita:"></asp:Label>
                        </td>
                        <td style="width: 175px; text-align: right; height: 19px;">
                            <asp:TextBox ID="txttipovisita" runat="server" Width="264px" AutoPostBack="True" Enabled="False"></asp:TextBox>
                        </td>
                        <td style="width: 68px; height: 19px;"></td>
                        <td style="width: 170px; text-align: left; height: 19px;"></td>
                        
                    </tr>
                    <tr>
                        <td colspan="5" style="height: 19px; text-align: center;">
                            <asp:Label ID="lblmsj2" runat="server" ForeColor="#000099" Text="Label" Visible="False"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <hr />
            <div style="width: 100%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: left; text-decoration: underline;">
                <asp:Label ID="Label17" runat="server" Font-Size="12pt">datos Visitante</asp:Label>
            </div>
            <table style="width: 100%; color: black">
                <tr>
                    <td></td>
                    <td style="width: 90%">
                        <table style="width: 100%; color: black">
                            <tr>
                                <td rowspan="9">
                                    <asp:Image ID="imgfoto" runat="server" Height="261px" Width="254px" ImageUrl="~/Images/sinfoto.jpg" />
                                </td>
                                <td style="text-align: left; width: 21px;">&nbsp;</td>
                                <td style="text-align: left; width: 145px;">
                                    <asp:Label ID="Label1" runat="server" Text="Tipo Documento:"></asp:Label>
                                </td>
                                <td style="text-align: left">

                                    <asp:DropDownList ID="ddltipodoc" runat="server" Width="267px" OnSelectedIndexChanged="ddltipodoc_SelectedIndexChanged" AutoPostBack="True" Enabled="False">
                                    </asp:DropDownList>

                                </td>
                                <td style="text-align: left">

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddltipodoc" ErrorMessage="Campo Requerido!." ForeColor="Red"></asp:RequiredFieldValidator>

                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 21px;">&nbsp;</td>
                                <td style="text-align: left; width: 145px;">
                                    <asp:Label ID="Label2" runat="server" Text="*No. Documento:"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtndocu" runat="server" MaxLength="16" Width="264px" Enabled="False"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="txtndocu_FilteredTextBoxExtender" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtndocu">
                                    </asp:FilteredTextBoxExtender>
                                </td>
                                <td style="text-align: left">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtndocu" ErrorMessage="Campo Requerido!." ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>

                            </tr>
                            <tr>
                                <td style="text-align: left; width: 21px;">&nbsp;</td>
                                <td style="text-align: left; width: 145px;">
                                    <asp:Label ID="Label5" runat="server" Text="*Primer Nombre:"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtnombre1" runat="server" CssClass="upperCase" MaxLength="30" Width="264px" Enabled="False"></asp:TextBox>
                                </td>
                                <td style="text-align: left">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtnombre1" ErrorMessage="Campo Requerido!." ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>

                            </tr>
                            <tr>
                                <td style="text-align: left; width: 21px;">&nbsp;</td>
                                <td style="text-align: left; width: 145px;">
                                    <asp:Label ID="Label6" runat="server" Text="Segundo Nombre:"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtnombre2" runat="server" CssClass="upperCase" MaxLength="30" Width="264px" Enabled="False"></asp:TextBox>
                                </td>
                                <td style="text-align: left">&nbsp;</td>

                            </tr>
                            <tr>
                                <td style="text-align: left; width: 21px;">&nbsp;</td>
                                <td style="text-align: left; width: 145px;">
                                    <asp:Label ID="Label3" runat="server" Text="*Apellido Paterno:"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtapellido1" runat="server" CssClass="upperCase" MaxLength="30" Width="264px" Enabled="False"></asp:TextBox>
                                </td>
                                <td style="text-align: left">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtapellido1" ErrorMessage="Campo Requerido!." ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>

                            </tr>
                            <tr>
                                <td style="text-align: left; width: 21px;">&nbsp;</td>
                                <td style="text-align: left; width: 145px;">
                                    <asp:Label ID="Label4" runat="server" Text="Apellido Materno:"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtapellido2" runat="server" CssClass="upperCase" MaxLength="30" Width="264px" Enabled="False"></asp:TextBox>
                                </td>
                                <td style="text-align: left">&nbsp;</td>

                            </tr>
                            <tr>
                                <td style="text-align: left; width: 21px;">&nbsp;</td>
                                <td style="text-align: left; width: 145px;">
                                    <asp:Label ID="Label7" runat="server" Text="Fecha Nacimiento (dd/mm/aaaa):"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtfecnaci" runat="server" Width="264px" Enabled="False"></asp:TextBox>
                                    <asp:MaskedEditExtender ID="txtfecnaci_MaskedEditExtender" runat="server" ClearMaskOnLostFocus="false" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" TargetControlID="txtfecnaci">
                                    </asp:MaskedEditExtender>
                                    <asp:CalendarExtender ID="txtfecnaci_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtfecnaci" Format="dd/MM/yyyy">
                                    </asp:CalendarExtender>
                                </td>
                                <td style="text-align: left">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtfecnaci" ErrorMessage="Fecha Incorrecta!." ForeColor="Red" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$"></asp:RegularExpressionValidator>
                                </td>

                            </tr>
                            <tr>
                                <td style="text-align: left; width: 21px;">&nbsp;</td>
                                <td style="text-align: left; width: 145px;">
                                    <asp:Label ID="Label9" runat="server" Text="Parentesco:"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlparentesco" runat="server" Width="267px" Enabled="False">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: left">&nbsp;</td>

                            </tr>
                            <tr>
                                <td style="text-align: left; width: 21px;">&nbsp;</td>
                                <td style="text-align: left; width: 145px;">
                                    <asp:Label ID="Label10" runat="server" Text="Observación:"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtobserva" runat="server" CssClass="upperCase" MaxLength="255" TextMode="MultiLine" Width="264px"></asp:TextBox>
                                </td>
                                <td style="text-align: left">&nbsp;</td>

                            </tr>
                        </table>
                    </td>
                    <td></td>
                </tr>
            </table>
            <table style="width: 100%; color: black" runat="server" id="tblmenor" visible="false">
                <tr>
                    <td></td>
                    <td style="width: 100%">
                        <table style="width: 100%; color: black">
                            <tr>
                                <td style="width: 232px"></td>
                                <td style="width: 105px">&nbsp;</td>
                                <td style="width: 153px; text-align: left;">

                                    <asp:CheckBox ID="chkmenor" runat="server" Text="Menor de Edad" AutoPostBack="True" OnCheckedChanged="chkmenor_CheckedChanged" />

                                </td>
                                <td style="width: 266px"></td>
                                <td style="width: 125px"></td>
                                
                            </tr>
                            <tr>
                                <td style="width: 232px">&nbsp;</td>
                                <td style="width: 105px">&nbsp;</td>
                                <td style="width: 153px; text-align: left;">
                                    <asp:Label ID="Label11" runat="server" Text="Edad:"></asp:Label>
                                </td>
                                <td style="width: 266px; text-align: left;">
                                    <asp:TextBox ID="txtedad" runat="server" Enabled="False" MaxLength="2" Width="264px"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="txtedad_FilteredTextBoxExtender" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtedad">
                                    </asp:FilteredTextBoxExtender>
                                </td>
                                <td style="width: 125px; text-align: left;">
                                    <asp:Label ID="lblmsj3" runat="server" ForeColor="Red" Text="Campo Requerido!." Visible="False"></asp:Label>
                                </td>
                                
                            </tr>
                            <tr>
                                <td style="width: 232px; height: 26px;"></td>
                                <td style="width: 105px; height: 26px;"></td>
                                <td style="width: 153px; text-align: left; height: 26px;">
                                    <asp:Label ID="Label12" runat="server" Text="Nombres:"></asp:Label>
                                </td>
                                <td style="width: 266px; text-align: left; height: 26px;">
                                    <asp:TextBox ID="txtnomedad" runat="server" CssClass="upperCase" Enabled="False" MaxLength="80" Width="264px"></asp:TextBox>
                                </td>
                                <td style="width: 125px; text-align: left; height: 26px;">
                                    <asp:Label ID="lblmsj4" runat="server" ForeColor="Red" Text="Campo Requerido!." Visible="False"></asp:Label>
                                </td>
                                
                            </tr>
                            <tr>
                                <td style="width: 232px">&nbsp;</td>
                                <td style="width: 105px">&nbsp;</td>
                                <td style="width: 153px; text-align: left;">
                                    <asp:Label ID="Label14" runat="server" Text="Género:"></asp:Label>
                                </td>
                                <td style="width: 266px; text-align: left;">
                                    <asp:DropDownList ID="ddlgeneroedad" runat="server" Enabled="False" Width="267px">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 125px">&nbsp;</td>
                                
                            </tr>
                            <tr>
                                <td style="width: 232px">&nbsp;</td>
                                <td style="width: 105px">&nbsp;</td>
                                <td style="width: 153px; text-align: left;">
                                    <asp:Label ID="Label15" runat="server" Text="Observación:"></asp:Label>
                                </td>
                                <td style="width: 266px; text-align: left;">
                                    <asp:TextBox ID="txtobsermenor" runat="server" CssClass="upperCase" Enabled="False" MaxLength="255" TextMode="MultiLine" Width="264px"></asp:TextBox>
                                </td>
                                <td style="width: 125px">&nbsp;</td>
                                
                            </tr>
                        </table>
                    </td>
                    <td></td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table style="width: 100%; color: black">
        <tr>
            <td></td>
            <td>
                <div id="menuaccions" style="border-style: ridge double double ridge; width: 100%; background-color: #FFFFFF; border-top-width: 4px; border-top-color: #FF0000; border-left-color: #FF0000;">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 1590px">
                                <asp:ImageButton ID="btningresar" runat="server" Height="60px" ImageUrl="~/Botones/Ingresar.png" OnClick="btningresar_Click" />
                            </td>
                            <td style="width: 966px">
                                <asp:ImageButton ID="btnverificar" runat="server" Height="60px" ImageUrl="~/Botones/verificarHuella.png" CausesValidation="False" Visible="False" OnClick="btnverificar_Click" />
                            </td>
                            <td style="width: 899px">
                                &nbsp;</td>
                            <td style="width: 312px">
                                <asp:ImageButton ID="btnsalir" runat="server" Height="60px" ImageUrl="~/Botones/Salir.png" CausesValidation="False" OnClick="btnsalir_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td></td>
        </tr>
    </table>


    <script type="text/javascript">
        function Validar_Cedula() {
            var ddltipodoc = document.getElementById("<%=ddltipodoc.ClientID%>").value;
            var cedula = document.getElementById("<%=txtndocu.ClientID%>").value;
            var digito_region = cedula.substring(0, 2);
            if (ddltipodoc == "C") {
                arreglo = cedula.split("");
                num = cedula.length;
                if (digito_region >= 1 && digito_region <= 24) {
                    if (num == 10) {
                        //validar cedula
                        digito = (arreglo[9] * 1);
                        total = 0;
                        for (i = 0; i < (num - 1) ; i++) {
                            if ((i % 2) != 0) {
                                total = total + (arreglo[i] * 1);
                            } else {
                                mult = arreglo[i] * 2;
                                if (mult > 9) {
                                    total = total + (mult - 9);
                                } else {
                                    total = total + mult;
                                }
                            }
                        }
                        decena = total / 10;
                        decena = Math.floor(decena);
                        decena = (decena + 1) * 10;
                        final = (decena - total);
                        if (final == 10) {
                            final = 0;
                        }
                        if (digito == final) {
                            return true;
                        } else {
                            alert("Cédula Incorrecta");
                            document.getElementById("<%=txtndocu.ClientID%>").value = "";
                            return false;
                        }
                    }
                    else {
                        alert("Cédula Incorrecta");
                        document.getElementById("<%=txtndocu.ClientID%>").value = "";
                        document.getElementById("<%=txtndocu.ClientID%>").focus;
                    }
                }
                else {
                    document.getElementById("<%=txtndocu.ClientID%>").value = "";
                    document.getElementById("<%=txtndocu.ClientID%>").focus;
                    alert("Cédula Incorrecta");
                }
            }

        }

    </script>
</asp:Content>

