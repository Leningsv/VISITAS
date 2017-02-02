using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Sancion_frmsancionAplicar : System.Web.UI.Page
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

                Array.Resize(ref objparam, 1);
                objparam[0] = ddlgruposan.SelectedValue;
                fun.cargarCombos(ddltiposanc, "spCarTipoSan", objparam, Page, (String[])Session["constrring"]);
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
        txtnombresan.Text = Session["nombre"].ToString();
        Array.Resize(ref objparam, 1);
        objparam[0] = strCodigoVis;
        dsData = fun.consultarDatos("spCargaDatosVisitante", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0)
        {
            ddltipodoc.SelectedValue = dsData.Tables[0].Rows[0][0].ToString();
            txtnumerodoc.Text = dsData.Tables[0].Rows[0][1].ToString();
            txtnombres.Text = dsData.Tables[0].Rows[0][4].ToString() + " " + dsData.Tables[0].Rows[0][5].ToString();
            txtapellidos.Text = dsData.Tables[0].Rows[0][2].ToString() + "" + dsData.Tables[0].Rows[0][3].ToString();
        }
        //Traer datos de la primera sancion
        Array.Resize(ref objparam, 1);
        objparam[0] = strCodigoVis;
        dsData = fun.consultarDatos("spCarDatosSancion", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0)
        {
            ddlgruposan.SelectedValue = dsData.Tables[0].Rows[0][0].ToString();
            txtobservacion.Text = dsData.Tables[0].Rows[0][1].ToString();
            txtfechainicio.Text = dsData.Tables[0].Rows[0][2].ToString();
            txtsanciopor.Text = dsData.Tables[0].Rows[0][3].ToString();
        }
    }
    #endregion

    #region Botones y Eventos
    protected void btngrabar_Click(object sender, ImageClickEventArgs e)
    {
        Array.Resize(ref objparam, 5);
        objparam[0] = Session["CodVisitante"].ToString();
        objparam[1] = ddltiposanc.SelectedValue;
        objparam[2] = txtfechafin.Text;
        objparam[3] = txtobservacion1.Text.ToUpper();
        objparam[4] = txtnombresan.Text.ToUpper();
        dsData = fun.consultarDatos("spActuEstSancion", objparam, Page, (String[])Session["constrring"]);
        ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascritp:window.opener.location='frmasignarsanAdmin.aspx';window.close();", true);
    }
    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascritp:window.close();", true);
    }
    protected void ddltiposanc_SelectedIndexChanged(object sender, EventArgs e)
    {
        objparam[0] = ddltiposanc.SelectedValue;
        dsData = fun.consultarDatos("spCarDatoTipSanc", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0)
        {
            txttiempo.Text = dsData.Tables[0].Rows[0][0].ToString();
            DateTime nuevafecha = DateTime.Now.AddDays(Convert.ToDouble(txttiempo.Text));
            txtfechafin.Text = nuevafecha.ToString("dd/MM/yyyy");
        }
    }
    #endregion
}