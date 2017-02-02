using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class CRS_frmcrsAdmin : System.Web.UI.Page
{
    #region Variables
    SIBDDNET.BDD objbdd = new SIBDDNET.BDD();
    String[] strparam = { "0" };
    Object objcon = new Object();
    DataSet dsData = new DataSet();
    String strres = "";
    Object[] objparam = new Object[1];
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                lbltitulo.Text = "Administrador de CRS";
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

    #region Funciones y Procedimientos
    protected void funCargaMantenimiento()
    {
        try
        {
            strparam = (String[])Session["constrring"];
            objbdd.subConectarBDD(strparam, ref objcon, ref strres);
            if (strres == "c")
            {
                Array.Resize(ref objparam, 1);
                objparam[0] = 0;
                dsData = objbdd.GetDatosXStoredProcedureXParametros("spCarCRSAdmin", objparam, ref objcon, ref strres);
                if (strres == "c")
                {
                    grdvDatos.DataSource = dsData;
                    grdvDatos.DataBind();
                    Session["grdvDatos"] = grdvDatos.DataSource;
                    ctrlbuscar.GrdGrillaBusqueda = grdvDatos;
                    ctrlbuscar.CargarComponente();
                    objbdd.subDesconectarBDD(ref objcon, ref strres);
                    if (strres != "c")
                    {
                        SIFunBasicas.Basicas.PresentarMensaje(Page, "Error al Cargar BDD", strres);
                    }
                }
                else
                {
                    SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", strres);
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

    #region Botones y Eventos
    protected void btningreso_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("frmcrsNew.aspx");
    }
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
    protected void btnselecc_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        int intIndex = gvRow.RowIndex;
        String strCodigoCrs = grdvDatos.DataKeys[intIndex].Values["Codigo"].ToString();
        ScriptManager.RegisterStartupScript(this.UpdatePanel1, GetType(), "Editar_CRS", "javascript: var posicion_x; var posicion_y; posicion_x=(screen.width/2)-(900/2); posicion_y=(screen.height/2)-(600/2); window.open('frmcrsEdit.aspx?crsCodigo=" + strCodigoCrs + "',null,'left=' + posicion_x + ', top=' + posicion_y + ', width=1024px, height=500px, status=no,resizable= yes, scrollbars=yes, toolbar=no, location=no, menubar=no,titlebar=0');", true);
    }
    #endregion
}