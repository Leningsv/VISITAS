<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CapturaHuella.aspx.cs" Inherits="VisitaPPL_CapturaHuella" %>
<%@ Register src="../Controles/BuscarGrilla.ascx" tagname="BuscarGrilla" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
table {
	font-size: 12px;
	/*background-color: #123456;*/
	border:0;
}

.buscador
{
	background-color: #E3E4F2;
	margin: 0;
	padding: 0;
	text-align: center;
	font-family: Arial;
	font-size: 0.7em;
	font-size: 2pc;
	vertical-align: middle;
	color: #20365F;
	width: 75%;
}

a {
	text-decoration: none;
	color: #FFFFFF;
	font-size: 12px;
}

.menuaccion
{
	background-color: #E3E4F2;
	margin: 0;
	padding: 0;
	text-align: center;
	font-family: Arial;
	font-size: 0.7em;
	font-size: 2pc;
	vertical-align: middle;
	color: #20365F;
	width: 75%;
}
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 396px;
        }
        .style3
        {
            width: 126px;
        }
        .style4
        {
            width: 129px;
        }
        .auto-style1
        {
            width: 327px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="smmantenimiento"  runat="server" AsyncPostBackTimeout="0" >
    </asp:ScriptManager>
<asp:UpdatePanel ID="uppbuscagrilla" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <OBJECT ID="SICapVerHuellaZK"
            CLASSID="CLSID:7AA53E11-9DD8-46D6-82A2-A7B5E8251327"
            CODEBASE="xLectorHuella001o.CAB#version=1,0,0,21">
        </OBJECT>
    </ContentTemplate>
 </asp:UpdatePanel>
        <div>
            <table class="style1">
                <tr>
                    <td class="auto-style1">
                        &nbsp;</td>
                    <td class="style3">
                        <asp:ImageButton ID="btngrabar" runat="server" Height="50px" 
                            ImageUrl="~/Botones/Grabar.png" style="margin-left: 0px" Width="90px" OnClick="btngrabar_Click" />
                    </td>
                    <td class="style4">
                        <asp:ImageButton ID="btnsalir" runat="server" 
                            ImageUrl="~/Botones/Salir.png" Height="50px" Width="90px" OnClick="btnsalir_Click" />
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        &nbsp;</td>
                    <td class="style3">
                        &nbsp;</td>
                    <td class="style4">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
    </div>
        </form>
</body>


</html>

