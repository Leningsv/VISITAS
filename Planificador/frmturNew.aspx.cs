using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Planificador_frmturNew : System.Web.UI.Page
{
    #region Variables
    DataSet dsData = new DataSet();
    Object[] objparam = new Object[1];
    Funciones fun = new Funciones();
    #endregion

    #region Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbltitulo.Text = "Agregar un nuevo Turno";
        }
    }
    #endregion

    #region Botones y Eventos
    protected void btngrabar_Click(object sender, ImageClickEventArgs e)
    {
        lblerror.Visible = false;

        string validar = fun.validarHoras(txtHoraIni.Text, txtHoraFin.Text);
        if (validar != "OK")
        {
            lblerror.Text = validar;
            lblerror.Visible = true;
            return;
        }

        Array.Resize(ref objparam, 5);
        objparam[0] = txtDescripcion.Text.ToUpper();
        objparam[1] = txtHoraIni.Text;
        objparam[2] = txtHoraFin.Text;
        objparam[3] = chkestado.Checked;
        objparam[4] = Session["usuCodigo"];
        dsData = fun.consultarDatos("spInsTurnos", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows[0][0].ToString() == "Existe")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('El Turno ya existe, ingrese un nuevo Turno');", true);
            return;
        }
        else
            ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='frmturAdmin.aspx?mensajeRetornado=Guardado Con éxito" + "';window.close();", true);
    }

    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.close();", true);
    }

    protected void btEliminarPla_Click(object sender, ImageClickEventArgs e)
    {
        //Array.Resize(ref objparam, 2);
        //objparam[0] = txtFechaDesde.Text;
        //objparam[1] = txtFechaHasta.Text;

        //dsData = fun.consultarDatos("spEliPlanificacion", objparam, Page, (String[])Session["constrring"]);
        //if (dsData.Tables[0].Rows[0][0].ToString() == "Existe")
        //{
        //    lblerrorDetalle.Text = "Ya existe un detalle que incluye este rango de letras para esta fecha";
        //    lblerrorDetalle.Visible = true;
        //    return;
        //}
        //Response.Redirect("~/Planificador/frmplaAdmin.aspx");
    }
    #endregion
}