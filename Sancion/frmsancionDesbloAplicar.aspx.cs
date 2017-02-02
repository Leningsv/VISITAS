using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Sancion_frmsancionDesbloAplicar : System.Web.UI.Page
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
                lbltitulo.Text = "desbloqueo de sanción a visitante";
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
                objparam[0] = Session["usuCodigo"].ToString();
                dsData = fun.consultarDatos("spCargaLogin", objparam, Page, (String[])Session["constrring"]);
                Session["strUsuLogin"] = dsData.Tables[0].Rows[0][0].ToString();
                txtdesblopor.Text = dsData.Tables[0].Rows[0][1].ToString();

                Array.Resize(ref objparam, 1);
                objparam[0] = ddlgruposan.SelectedValue;
                fun.cargarCombos(ddltiposanc, "spCarTipoSan", objparam, Page, (String[])Session["constrring"]);
                funCargaMantenimiento(Session["CodVisitante"].ToString(), "m");
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
                txtsanciopor.Text = dsData.Tables[0].Rows[0][3].ToString();
                ddltiposanc.SelectedValue = dsData.Tables[0].Rows[0][4].ToString();
                txttiempo.Text = dsData.Tables[0].Rows[0][5].ToString();
                txtfechafin.Text = dsData.Tables[0].Rows[0][6].ToString();
                txtejecupor.Text = dsData.Tables[0].Rows[0][7].ToString();
                txtobservacion1.Text = dsData.Tables[0].Rows[0][8].ToString();
                txtfechaefect.Text = txtfechafin.Text;
            }
        }
    }
    #endregion

    #region Botones y Eventos
    protected void btngrabar_Click(object sender, ImageClickEventArgs e)
    {
        //validar fecha de desbloqueo
        if (Convert.ToDateTime(txtfechaefect.Text) < Convert.ToDateTime(txtfechainicio.Text))
        {
            lblerror.Text = "Fecha desbloqueo no puede ser menor a la fecha de inicio";
            lblerror.Visible = true;
            return;
        }
        if (Convert.ToDateTime(txtfechaefect.Text) > Convert.ToDateTime(txtfechafin.Text))
        {
            lblerror.Text = "Fecha desbloqueo no puede ser mayor o igual a la fecha de inicio";
            lblerror.Visible = true;
            return;
        }

        Array.Resize(ref objparam, 7);
        objparam[0] = Session["CodVisitante"].ToString();
        objparam[1] = txtfechaefect.Text;
        objparam[2] = txtobserva2.Text.ToUpper();
        objparam[3] = txtdesblopor.Text.ToUpper();
        objparam[4] = Session["strUsuLogin"].ToString();
        objparam[5] = Session["usuCodigo"].ToString();
        objparam[6] = Session["MachineName"].ToString();
        dsData = fun.consultarDatos("spActuDesblo", objparam, Page, (String[])Session["constrring"]);
        ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascritp:window.opener.location='frmsanDesbloAdmin.aspx';window.close();", true);
    }
    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascritp:window.close();", true);
    }
    #endregion
}