﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;

public partial class Reportes_frmrepVisitas : System.Web.UI.Page
{
    #region Variables
    DataSet dsData = new DataSet();
    Object[] objparam = new Object[1];
    Funciones fun = new Funciones();
    ListItem liTodos = new ListItem("Todos", "T");
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbltitulo.Text = "Reporte de Visitas";
            Array.Resize(ref objparam, 2);
            objparam[0] = 0;
            objparam[1] = "2";//ETAPA
            fun.cargarCombos(ddlEtapa, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
            ddlEtapa.Items.RemoveAt(0);
            ddlEtapa.Items.Add(liTodos);
            ddlEtapa.SelectedValue = "T";
            ddlPabellon.Items.Add(liTodos);
            ddlPabellon.SelectedValue = "T";
            Array.Resize(ref objparam, 1);
            objparam[0] = 0;
            fun.cargarCombos(ddlTurnos, "spCarTurnos", objparam, Page, (String[])Session["constrring"]);
            ddlTurnos.Items.RemoveAt(0);
            ddlTurnos.Items.Add(liTodos);
            ddlTurnos.SelectedValue = "T";
            ddlAla.Items.Add(liTodos);
            ddlAla.SelectedValue = "T";

            //ReportViewer1.Visible = false;
        }
    }

    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Index1.aspx");
    }

    protected void btProcesar_Click(object sender, ImageClickEventArgs e)
    {
        string horaDesde, horaHasta;
        try
        {
            lblerror.Visible = false;
            if (txtFechaDesde.Text == "" || txtFechaHasta.Text == "")
            {
                lblerror.Visible = true;
                lblerror.Text = "Ingrese la Fecha Inicio y Fecha Fin para realizar la consulta";
                return;
            }

            if (DateTime.Parse(txtFechaDesde.Text) > DateTime.Parse(txtFechaHasta.Text))
            {
                lblerror.Visible = true;
                lblerror.Text = "La Fecha Inicio no puede ser mayor a la Fecha Fin";
                return;
            }

            if (ddlTurnos.SelectedValue == "T")
            {
                horaDesde = "00:00";
                horaHasta = "23:59";
            }
            else
            {
                horaDesde = ddlTurnos.SelectedItem.Text.Substring(0, 5);
                horaHasta = ddlTurnos.SelectedItem.Text.Substring(8, 5);
            }

            Array.Resize(ref objparam, 7);
            objparam[0] = txtFechaDesde.Text;
            objparam[1] = txtFechaHasta.Text;
            objparam[2] = horaDesde;
            objparam[3] = horaHasta;
            objparam[4] = ddlEtapa.SelectedValue;
            objparam[5] = ddlPabellon.SelectedValue;
            objparam[6] = ddlAla.SelectedValue;
            dsData = fun.consultarDatos("spRepVisitas", objparam, Page, (String[])Session["constrring"]);
            if (dsData.Tables[0].Rows.Count > 0)
            {
                ReportViewer1.Visible = true;
                ReportDataSource dataSourceReporte1 = new ReportDataSource("DataSet1", ObtenerLista(dsData));
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(dataSourceReporte1);
                ReportViewer1.LocalReport.Refresh();
            }
            else
            {
                lblerror.Visible = true;
                lblerror.Text = "No existen datos para la busqueda seleccionada";
                ReportViewer1.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblerror.Visible = true;
            lblerror.Text = "Error: " + ex.Message;
        }
    }

    private object ObtenerLista(DataSet DSconsulta)
    {
        dsrepVisitas.dtrepVisitasDataTable dtReporte1 = new dsrepVisitas.dtrepVisitasDataTable();
        int intCont = 0;
        while (intCont < DSconsulta.Tables[0].Rows.Count)
        {
            dsrepVisitas.dtrepVisitasRow row = dtReporte1.NewdtrepVisitasRow();
            row.tipoDoc = DSconsulta.Tables[0].Rows[intCont][0].ToString();
            row.nroDoc = DSconsulta.Tables[0].Rows[intCont][1].ToString();
            row.nombreVisitante = DSconsulta.Tables[0].Rows[intCont][2].ToString();
            row.tipoVisita = DSconsulta.Tables[0].Rows[intCont][3].ToString();
            row.fechaIngreso = DSconsulta.Tables[0].Rows[intCont][4].ToString();
            row.horaIngreso = DSconsulta.Tables[0].Rows[intCont][5].ToString();
            row.fechaSalida = DSconsulta.Tables[0].Rows[intCont][6].ToString();
            row.horaSalida = DSconsulta.Tables[0].Rows[intCont][7].ToString();
            row.nombrePPL = DSconsulta.Tables[0].Rows[intCont][8].ToString();
            row.etapa = DSconsulta.Tables[0].Rows[intCont][9].ToString();
            row.pabellon = DSconsulta.Tables[0].Rows[intCont][10].ToString();
            row.ala = DSconsulta.Tables[0].Rows[intCont][11].ToString();
            dtReporte1.Rows.Add(row);
            intCont++;
        }
       
        return dtReporte1;
    }

    protected void ddlEtapa_SelectedIndexChanged(object sender, EventArgs e)
    {
        Array.Resize(ref objparam, 1);
        objparam[0] = ddlEtapa.SelectedValue;
        fun.cargarCombos(ddlPabellon, "spCarPabellon", objparam, Page, (String[])Session["constrring"]);
        ddlPabellon.Items.RemoveAt(0);
        ddlPabellon.Items.Add(liTodos);
        ddlPabellon.SelectedValue = "T";

        Array.Resize(ref objparam, 2);
        objparam[0] = ddlEtapa.SelectedValue;
        objparam[1] = ddlPabellon.SelectedValue;
        fun.cargarCombos(ddlAla, "spCarAla", objparam, Page, (String[])Session["constrring"]);
        ddlAla.Items.RemoveAt(0);
        ddlAla.Items.Add(liTodos);
        ddlAla.SelectedValue = "T";
    }

    protected void ddlPabellon_SelectedIndexChanged(object sender, EventArgs e)
    {
        Array.Resize(ref objparam, 2);
        objparam[0] = ddlEtapa.SelectedValue;
        objparam[1] = ddlPabellon.SelectedValue;
        fun.cargarCombos(ddlAla, "spCarAla", objparam, Page, (String[])Session["constrring"]);
        ddlAla.Items.RemoveAt(0);
        ddlAla.Items.Add(liTodos);
        ddlAla.SelectedValue = "T";
    }
}