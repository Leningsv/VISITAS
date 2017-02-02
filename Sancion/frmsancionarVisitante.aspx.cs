using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Sancion_frmsancionarVisitante : System.Web.UI.Page
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

                lbltitulo.Text = "asignar sacion a visitante";
                txtfechainicio.Text = DateTime.Now.ToString("dd/MM/yyyy");

                Array.Resize(ref objparam, 2);
                objparam[0] = 0;
                objparam[1] = "22";
                fun.cargarCombos(ddlgruposan, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
                ddlgruposan.Items.RemoveAt(0);

                Array.Resize(ref objparam, 2);
                objparam[0] = 0;
                objparam[1] = "4";
                fun.cargarCombos(ddltipodoc, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
                ddltipodoc.Items.RemoveAt(0);

                funCargaMantenimiento(Session["CodVisitante"].ToString());

            }
        }
        catch (Exception ex)
        {
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", ex.Message);
        }
    }
    #endregion

    #region Funciones y Procedimientos
    protected void funCargaMantenimiento(String strCodigoVis)
    {
        Array.Resize(ref objparam, 1);
        objparam[0] = strCodigoVis;
        dsData = fun.consultarDatos("spCargaDatosVisitante", objparam, Page, (String[])Session["constrring"]);
        ddltipodoc.SelectedValue = dsData.Tables[0].Rows[0][0].ToString();
        txtnumerodoc.Text = dsData.Tables[0].Rows[0][1].ToString();
        txtnombres.Text = dsData.Tables[0].Rows[0][4].ToString() + " " + dsData.Tables[0].Rows[0][5].ToString();
        txtapellidos.Text = dsData.Tables[0].Rows[0][2].ToString() + " " + dsData.Tables[0].Rows[0][3].ToString();
        txtsancionadopor.Text = Session["nombre"].ToString();
    }
    #endregion

    #region Botones y Eventos
    protected void btngrabar_Click(object sender, ImageClickEventArgs e)
    {
        Array.Resize(ref objparam, 9);
        objparam[0] = Session["Codigo_Visita"].ToString();
        objparam[1] = Session["CodVisitante"].ToString();
        objparam[2] = txtnumerodoc.Text;
        objparam[3] = ddltipodoc.SelectedValue;
        objparam[4] = ddlgruposan.SelectedValue;
        objparam[5] = txtobservacion.Text.ToUpper();
        objparam[6] = txtsancionadopor.Text.ToUpper();
        objparam[7] = Session["usuCodigo"].ToString();
        objparam[8] = Session["MachineName"].ToString();
        dsData = fun.consultarDatos("spInsSancionVis", objparam, Page, (String[])Session["constrring"]);
        ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='../SalidaVisita/frmsalidavisAdmin.aspx';window.close();", true);
    }
    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.close();", true);
    }
    #endregion
}