using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Visita_frmvisitanteAdmin : System.Web.UI.Page
{
    #region variables
    Object[] objparam = new Object[1];
    DataSet dsData = new DataSet();
    Funciones fun = new Funciones();
    #endregion

    #region Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["fotoanterior"] = null;
                Session["codigotempVisitante"] = null;
                lbltitulo.Text = "Administrador de Visitantes";
                Array.Resize(ref objparam, 2);
                objparam[0] = 0;
                objparam[1] = "20";
                String strObt = fun.PoliticasIngresoVisita(Page, "spCargarParametros", objparam, (String[])Session["constrring"]);
                String[] session = strObt.Split('|');
                Session["ssingvis"] = session[0].ToString();
                Session["ssvalcedu"] = session[1].ToString();
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
        Array.Resize(ref objparam, 1);
        objparam[0] = 0;
        dsData = fun.consultarDatos("spnVisitanteReadAll", objparam, Page, (String[])Session["constrring"]);
        grdvDatos.DataSource = dsData;
        grdvDatos.DataBind();
        Session["grdvDatos"] = grdvDatos.DataSource;
        ctrlbuscar.GrdGrillaBusqueda = grdvDatos;
        ctrlbuscar.CargarComponente();
    }


    protected void Ordenar_GridView(object sender, GridViewSortEventArgs e)
    {
        DataSet mitabla = (DataSet)Session["grdvDatos"];
        DataTable datos = mitabla.Tables[0];
        if (datos != null)
        {
            DataView dataView = new DataView(datos);
            dataView.Sort = e.SortExpression + " " + fun.ConvertSortDirection(e.SortDirection);

            grdvDatos.DataSource = dataView;
            grdvDatos.DataBind();
        }
    }
    #endregion

    #region Botones y Eventos
    protected void btningreso_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("frmvisitanteNew.aspx?visCodigo=0Nuevo");
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
    #endregion
}