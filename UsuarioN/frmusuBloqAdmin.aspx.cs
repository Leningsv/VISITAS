using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class UsuarioN_frmusuBloqAdmin : System.Web.UI.Page
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
                lbltitulo.Text = "Desbloquear Usuarios";

                Array.Resize(ref objparam, 2);
                objparam[0] = "5";
                objparam[1] = "1";
                dsData = fun.consultarDatos("spCargaTotalParaDeta", objparam, Page, (String[])Session["constrring"]);
                if (dsData.Tables[0].Rows.Count == 0) Session["intentos"] = "3";
                else Session["intentos"] = dsData.Tables[0].Rows[0][0].ToString();
                if (!fun.IsNumber(Session["intentos"].ToString())) Session["intentos"] = "3";

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
        objparam[0] = int.Parse(Session["intentos"].ToString());
        dsData = fun.consultarDatos("spUsuAdminReadDesblo", objparam, Page, (String[])Session["constrring"]);
        grdvDatos.DataSource = dsData;
        grdvDatos.DataBind();
        Session["grdvDatos"] = grdvDatos.DataSource;
        ctrlbuscar.GrdGrillaBusqueda = grdvDatos;
        ctrlbuscar.CargarComponente();
    }

    #endregion

    #region Botones y Eventos
    protected void btnselecc_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        int intIndex = gvRow.RowIndex;
        String strCodigoUsu = grdvDatos.DataKeys[intIndex].Values["Codigo"].ToString();
        String strUsuLogin = grdvDatos.DataKeys[intIndex].Values["usuLogin"].ToString();
        ScriptManager.RegisterStartupScript(this.UpdatePanel1, GetType(), "Editar_Usuario", "javascript: var posicion_x; var posicion_y; posicion_x=(screen.width/2)-(900/2); posicion_y=(screen.height/2)-(600/2); window.open('frmusuDesbloEdit.aspx?usuCodigo=" + strCodigoUsu + "&usuLogin=" + strUsuLogin + "',null,'left=' + posicion_x + ', top=' + posicion_y + ', width=1024px, height=500px, status=no,resizable= yes, scrollbars=yes, toolbar=no, location=no, menubar=no,titlebar=0');", true);
    }
    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Index1.aspx");
    }
    #endregion
}