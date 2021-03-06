﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class SalidaVisita_frmsalidavisAdmin : System.Web.UI.Page
{
    #region Variables
    DataSet dsData = new DataSet();
    Object[] objparam = new Object[1];
    Funciones fun = new Funciones();
    #endregion

    #region Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["grabohuella"] = null;
                lbltitulo.Text = "salida de visitas";
                funCargarpost();
                if (Request["mensajeRetornado"] != null)
                {
                    SIFunBasicas.Basicas.PresentarMensaje(Page, "::VISITA PPL::", Request["MensajeRetornado"]);
                }
            }
            else
            {
                grdvDatos.DataSource = Session["grdvDatos"];
                ctrlbuscar.GrdGrillaBusqueda = grdvDatos;
            }
        }
        catch (Exception ex)
        {
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", ex.Message);
        }
    }
    #endregion

    #region Funciones y Procedimientos
    protected void funCargarpost()
    {

        objparam[0] = 0;
        dsData = fun.consultarDatos("spCarCantEntDocu", objparam, Page, (String[])Session["constrring"]);
        lblcedula.Text = dsData.Tables[0].Rows[0][0].ToString();
        lblpasaporte.Text = dsData.Tables[0].Rows[0][1].ToString();
        lblotros.Text = dsData.Tables[0].Rows[0][2].ToString();

        objparam[0] = 1;
        dsData = fun.consultarDatos("spCarListVisSinDocu", objparam, Page, (String[])Session["constrring"]);
        grdvDatos.DataSource = dsData;
        grdvDatos.DataBind();
        Session["grdvDatos"] = grdvDatos.DataSource;
        ctrlbuscar.GrdGrillaBusqueda = grdvDatos;
        ctrlbuscar.CargarComponente();


    }
    #endregion

    #region Botones y Eventos
    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Index1.aspx");
    }
    protected void grdvDatos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdvDatos.PageIndex = e.NewPageIndex;
        ctrlbuscar.GrdGrillaBusqueda = grdvDatos;
        grdvDatos.DataBind();
    }
    protected void btnselect_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        int intIndex = gvRow.RowIndex;

        Session["CodVisitante"] = grdvDatos.DataKeys[intIndex].Values["CodigoVisitante"];
        Session["Codigo_PPL"] = grdvDatos.DataKeys[intIndex].Values["CodigoPPL"];
        Session["Codigo_Visita"] = grdvDatos.DataKeys[intIndex].Values["CodigoVis"];
        Session["tabla"] = "V";
        ScriptManager.RegisterStartupScript(this.UpdatePanel1, GetType(), "Salida de Visitante", "javascript: var posicion_x; var posicion_y; posicion_x=(screen.width/2)-(900/2); posicion_y=(screen.height/2)-(600/2); window.open('frmsalidavisNew.aspx',null,'left=' + posicion_x + ', top=' + posicion_y + ', width=1024px, height=600px, status=no,resizable= yes, scrollbars=yes, toolbar=no, location=no, menubar=no');", true);
    }
    #endregion
}