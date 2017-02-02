<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BuscarGrilla.ascx.cs" Inherits="Controles_BuscarGrilla" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<style type="text/css">
    .auto-style1
    {
        height: 37px;
        width: 802px;
    }
    .auto-style2
    {
        width: 802px;
    }
    .auto-style3
    {
        height: 37px;
        width: 325px;
    }
</style>


<table runat="server" border="0" cellpadding="0" cellspacing="0" id="tblprinc" style="width:589px;">
  <tr >
    <td style="color: black; font-weight: bold; " class="auto-style1" >Buscar por:&nbsp;
        <asp:DropDownList ID="ddlCampos" runat="server" AutoPostBack="True" Width="136px" OnSelectedIndexChanged="ddlCampos_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:TextBox ID="txtBuscar" runat="server" OnTextChanged="txtBuscar_TextChanged"></asp:TextBox>&nbsp;
        </td>
      <td>   
        <asp:ImageButton ID="imgBuscar" runat="server" TabIndex="1" 
            OnClick="imgBuscar_Click" ImageUrl="~/Botones/Buscar2.png" 
            Height="65px" Width="55px" style="margin-left: 0px"></asp:ImageButton>
      </td>
    <td valign="top" class="auto-style3">   
        &nbsp;</td>
    <%--<td valign="top" style="width: 494px; height: 37px;">   
      <table id="tbltexto" runat="server" border="0" cellpadding="0" cellspacing="0" style="color: #137920; width: 201px;">
      </table>
    </td>--%>
  </tr>
  <tr>
  <td valign="top" class="auto-style2">
    <table id="tblfechas" runat="server" border="0" cellpadding="0" cellspacing="0">
      <tr>
        <td style="width: 100px; text-align: left" valign="top">
          <asp:Label ID="lblinicio" runat="server" Text="Fecha Desde:" ForeColor="White"></asp:Label>
        </td>
        <td style="width: 129px; text-align: center" valign="middle">
				  <asp:TextBox ID="txtFechaIni" runat="server" ReadOnly="True" Visible="False"></asp:TextBox>
				  <cc1:calendarextender id="calextFechaIni" runat="server" targetcontrolid="txtFechaIni"></cc1:calendarextender>
		</td>
	  </tr>
      <tr>
        <td style="width: 100px; text-align: left" valign="top">
          <asp:Label ID="lblFin" runat="server" Text="Fecha Hasta:" ForeColor="White"></asp:Label>
        </td>
        <td style="width: 129px; text-align: center" valign="middle">
				  <asp:TextBox ID="txtFechaFin" runat="server" ReadOnly="True" Visible="False"></asp:TextBox>
					<cc1:calendarextender id="calextFechaFin" runat="server" targetcontrolid="txtFechaFin"></cc1:calendarextender>
				</td>
      </tr>
    </table>
  </td></tr>
</table>