using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class PerfilA_frmperfaAdmin : System.Web.UI.Page
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

                lbltitulo.Text = "Administrador de Perfil de Accesos";
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
        Array.Resize(ref objparam, 1);
        objparam[0] = 0;
        dsData = fun.consultarDatos("spnPerfAcceRead", objparam, Page, (String[])Session["constrring"]);
        grdvDatos.DataSource = dsData;
        grdvDatos.DataBind();
        Session["grdvDatos"] = grdvDatos.DataSource;
        ctrlbuscar.GrdGrillaBusqueda = grdvDatos;
        ctrlbuscar.CargarComponente();
    }
    #endregion

    #region Botones y Eventos
    protected void grdvDatos_Sorting(object sender, GridViewSortEventArgs e)
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

    protected void btningreso_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/PerfilA/frmperfaNew.aspx");
    }

    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Index1.aspx");
    }
    #endregion
}