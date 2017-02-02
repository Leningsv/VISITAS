using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class MenuN_frmmenuAdmin : System.Web.UI.Page
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
                lbltitulo.Text = "Administrador de Menú";
                funCargaMantenimiento();
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
    protected void funCargaMantenimiento()
    {
        ImageButton imgSubir = new ImageButton();
        ImageButton imgBajar = new ImageButton();

        Array.Resize(ref objparam, 1);
        objparam[0] = 0;

        dsData = fun.consultarDatos("spnMenuAdminReadAll", objparam, Page, (String[])Session["constrring"]);
        grdvDatos.DataSource = dsData;
        grdvDatos.DataBind();
        Session["grdvDatos"] = grdvDatos.DataSource;
        ctrlbuscar.GrdGrillaBusqueda = grdvDatos;

        var intlastrow = grdvDatos.Rows.Count - 1;

        imgSubir = (ImageButton)grdvDatos.Rows[0].Cells[3].FindControl("imgSubirNivel");
        imgSubir.ImageUrl = "~/Botones/desactivadaup.png";
        imgSubir.Enabled = false;

        imgBajar = (ImageButton)grdvDatos.Rows[intlastrow].Cells[3].FindControl("imgBajarNivel");
        imgBajar.ImageUrl = "~/Botones/desactivadadown.png";
        imgBajar.Enabled = false;

        ctrlbuscar.CargarComponente();

    }
    #endregion

    #region Botones y Eventos
    protected void btningreso_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("frmmenuNew.aspx");
    }
    protected void imgSubirNivel_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        int intIndex = gvRow.RowIndex;

        var strCodMenu = grdvDatos.DataKeys[intIndex].Values["Codigo"];
        Array.Resize(ref objparam, 2);
        objparam[0] = strCodMenu;
        objparam[1] = 0;
        dsData = fun.consultarDatos("spCamOrdenMenu", objparam, Page, (String[])Session["constrring"]);
        funCargaMantenimiento();
    }
    protected void imgBajarNivel_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        int intIndex = gvRow.RowIndex;

        var strCodMenu = grdvDatos.DataKeys[intIndex].Values["Codigo"];
        Array.Resize(ref objparam, 2);
        objparam[0] = strCodMenu;
        objparam[1] = 1;
        dsData = fun.consultarDatos("spCamOrdenMenu", objparam, Page, (String[])Session["constrring"]);
        funCargaMantenimiento();
    }
    protected void grdvDatos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdvDatos.PageIndex = e.NewPageIndex;
        ctrlbuscar.GrdGrillaBusqueda = grdvDatos;
        grdvDatos.DataBind();
    }
    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Index1.aspx");
    }
    protected void btnselecc_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        int intIndex = gvRow.RowIndex;
        String strCodigoMenu = grdvDatos.DataKeys[intIndex].Values["Codigo"].ToString();
        Response.Redirect("frmmenuEdit.aspx?codigo=" + strCodigoMenu);
    }
    #endregion
}