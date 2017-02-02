using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Sancion_frmsanDatosHistDeta : System.Web.UI.Page
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
                btnsalir.Attributes.Add("onClick", "javascript:window.close()");
                lbltitulo.Text = "detalle historial de visitante";
                txtfechainicio.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtfechaefect.Text = DateTime.Now.ToString("dd/MM/yyyy");

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

                Array.Resize(ref objparam, 1);
                objparam[0] = ddlgruposan.SelectedValue;
                fun.cargarCombos(ddltiposanc, "spCarTipoSan", objparam, Page, (String[])Session["constrring"]);
                funCargaMantenimiento(Session["CodVisitanteD"].ToString(), "m");
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
            //Traer datos de las sanciones
            Array.Resize(ref objparam, 2);
            objparam[0] = strCodigoVis;
            objparam[1] = 0;
            dsData = fun.consultarDatos("spCarDatSanTot", objparam, Page, (String[])Session["constrring"]);
            if (dsData.Tables[0].Rows.Count > 0)
            {
                ddlgruposan.SelectedValue = dsData.Tables[0].Rows[0][0].ToString();
                txtobservacion.Text = dsData.Tables[0].Rows[0][1].ToString();
                txtfechainicio.Text = dsData.Tables[0].Rows[0][2].ToString();
                txtsancionado.Text = dsData.Tables[0].Rows[0][3].ToString();
                ddltiposanc.SelectedValue = dsData.Tables[0].Rows[0][4].ToString();
                txttiempo.Text = dsData.Tables[0].Rows[0][5].ToString();
                txtfechafin.Text = dsData.Tables[0].Rows[0][6].ToString();
                txtejecutado.Text = dsData.Tables[0].Rows[0][7].ToString();
                txtobservacion1.Text = dsData.Tables[0].Rows[0][8].ToString();
                txtfechaefect.Text = dsData.Tables[0].Rows[0][9].ToString();
                txtobserva2.Text = dsData.Tables[0].Rows[0][10].ToString();
                txtdesblopor.Text = dsData.Tables[0].Rows[0][11].ToString();

            }
        }
    }
    #endregion

    #region botones y Eventos
    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        String script = "<script language='javascript'>CerrarSolo();</script>";
        ClientScript.RegisterStartupScript(GetType(), "pop", script);
    }
    #endregion
}