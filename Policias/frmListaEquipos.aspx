<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmListaEquipos.aspx.cs" Inherits="Policias_frmListaEquipos" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">


.Texto_General
{
	font-weight: normal;
	font-size: 8pt;
	color:#316ac5 ;
	font-family: Verdana;
	text-decoration: none;
}

table {
	font-size: 12px;
	border:0;
}

        .GVFixedHeader 
        { 
            font-weight:bold; 
            background-color: #20365F; 
            position:relative; 
            top:expression(this.parentNode.parentNode.parentNode.scrollTop-1);
        }
        
.GVFixedHeader 
{ 
    font-weight:bold; 
    background-color: #20365F; 
    position:relative; 
    top:expression(this.parentNode.parentNode.parentNode.scrollTop-1);
}

a {
	text-decoration: none;
	color: #FFFFFF;
	font-size: 12px;
}


    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <div style="width: 90%; color: #20365F; font-family: Aparajita; font-size: 25px; font-weight: bold; text-transform: uppercase; text-align: left; text-decoration: underline;">
        <asp:Label ID="lbltitulo" runat="server"></asp:Label>
    </div>
        <br />
                            <asp:GridView ID="grdvDatos" runat="server" AllowSorting="True" AutoGenerateColumns="False" backcolor="White" CaptionAlign="Left" CellPadding="4" CssClass="Texto_General" EmptyDataText="No hay datos que mostrar" ForeColor="#333333" PageSize="15" SkinID="grillamant" Width="100%">
                                <Columns>
                                    <asp:HyperLinkField DataTextField="Codigo" HeaderText="Codigo" Target="_self">
                                    <ControlStyle Font-Size="7pt" ForeColor="Black" />
                                    <HeaderStyle CssClass="GVFixedHeader" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:HyperLinkField>
                                    <asp:HyperLinkField DataTextField="Nombre" HeaderText="Nombres">
                                    <ControlStyle Font-Size="7pt" ForeColor="Black" />
                                    <HeaderStyle CssClass="GVFixedHeader" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:HyperLinkField>
                                     <asp:HyperLinkField DataTextField="Ip" HeaderText="Direccion IP">
                                    <ControlStyle Font-Size="7pt" ForeColor="Black" />
                                    <HeaderStyle CssClass="GVFixedHeader" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:HyperLinkField>
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
    
    </div>
    </form>
</body>
</html>