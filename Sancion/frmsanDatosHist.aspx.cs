using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Sancion_frmsanDatosHist : System.Web.UI.Page
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

                lbltitulo.Text = "Detalle Historial de sanción";

                Array.Resize(ref objparam, 2);
                objparam[0] = 0;
                objparam[1] = "4";
                fun.cargarCombos(ddltipodoc, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
                ddltipodoc.Items.RemoveAt(0);

                funCargaMantenimiento(Session["CodigoVisitanteH"].ToString(), "m");

            }
            else
            {
                grdvDatos.DataSource = Session["grdvDatos"];
            }
        }
        catch (Exception ex)
        {
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", ex.Message);
        }
    }
    #endregion

    #region Funciones y Procedimientos
    protected void funCargaMantenimiento(String strCodigoVis, String strTipo)
    {
        if (strTipo == "m")
        {
            Array.Resize(ref objparam, 1);
            objparam[0] = strCodigoVis;
            dsData = fun.consultarDatos("spCargaDatosVisitante", objparam, Page, (String[])Session["constrring"]);
            if (dsData.Tables[0].Rows.Count > 0)
            {
                ddltipodoc.SelectedValue = dsData.Tables[0].Rows[0][0].ToString();
                txtnumerodoc.Text = dsData.Tables[0].Rows[0][1].ToString();
                txtnombres.Text = dsData.Tables[0].Rows[0][4].ToString() + " " + dsData.Tables[0].Rows[0][5].ToString();
                txtapellidos.Text = dsData.Tables[0].Rows[0][2].ToString() + " " + dsData.Tables[0].Rows[0][3].ToString();
            }
            Array.Resize(ref objparam, 2);
            objparam[0] = strCodigoVis;
            objparam[1] = 0;
            dsData = fun.consultarDatos("spCargHistoVisita", objparam, Page, (String[])Session["constrring"]);
            if (dsData.Tables[0].Rows.Count == 0)
            {
                Array.Resize(ref objparam, 2);
                objparam[0] = strCodigoVis;
                objparam[1] = 1;
                dsData = fun.consultarDatos("spCargHistoVisita", objparam, Page, (String[])Session["constrring"]);
            }
            grdvDatos.DataSource = dsData;
            grdvDatos.DataBind();
            Session["grdvDatos"] = grdvDatos.DataSource;
        }

    }
    #endregion

    #region Botones y Eventos
    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascritp:window.close();", true);
    }
    protected void grdvDatos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdvDatos.PageIndex = e.NewPageIndex;
        grdvDatos.DataBind();
    }
    protected void btnselect_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        int intIndex = gvRow.RowIndex;

        Session["CodVisitanteD"] = grdvDatos.DataKeys[intIndex].Values["Codigo"];
        String pagina = "frmsanDatosHistDeta.aspx";
        String newpag = "window.open('" + pagina + "', 'popup_window', 'width=950,height=600,left=50,top=50,resizable=yes,scrollbars=yes');";
        ClientScript.RegisterStartupScript(GetType(), "pop", newpag, true);
    }
    #endregion
}