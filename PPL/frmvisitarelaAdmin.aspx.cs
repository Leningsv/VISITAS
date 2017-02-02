using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class PPL_frmvisitarelaAdmin : System.Web.UI.Page
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
                lbltitulo.Text = "Visitantes registrados";
                var codppl = Session["CodigoPPL"].ToString();
                funCargarpost();
                Response.Redirect("frmvisitaPPLNew.aspx");
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
        dsData = fun.consultarDatos("spVisitRelacionAdmin", objparam, Page, (String[])Session["constrring"]);
        grdvDatos.DataSource = dsData;
        grdvDatos.DataBind();
        Session["grdvDatos"] = grdvDatos.DataSource;
        ctrlbuscar.GrdGrillaBusqueda = grdvDatos;
        ctrlbuscar.CargarComponente();
    }
    #endregion

    #region Botones y Eventos
    protected void btnnuevo_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("frmvisitaPPLNew.aspx");
    }
    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascritp:window.opener.location='frmvisitaRelacion.aspx';window.close();", true);
    }
    protected void grdvDatos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdvDatos.PageIndex = e.NewPageIndex;
        ctrlbuscar.GrdGrillaBusqueda = grdvDatos;
        grdvDatos.DataBind();
    }
    protected void btnselec_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        int intIndex = gvRow.RowIndex;

        Session["NuevoVisitante"] = grdvDatos.DataKeys[intIndex].Values["Codigo"];
        ScriptManager.RegisterStartupScript(this.UpdatePanel2, GetType(), "pop", "javascritp:window.opener.location='frmvisitaRelacion.aspx?verificavis='+'SI';window.close();", true);
    }
    #endregion
}