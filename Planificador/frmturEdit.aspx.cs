using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Planificador_frmturEdit : System.Web.UI.Page
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
            lbltitulo.Text = "Editar Turno";
            funCargaMantenimiento(Request["CodTurno"].ToString());
        }
    }
    #endregion

    #region Funciones y Procedimientos
    protected void funCargaMantenimiento(String strCodigo)
    {
        Array.Resize(ref objparam, 1);
        objparam[0] = strCodigo;
        dsData = fun.consultarDatos("spSelTurnos", objparam, Page, (String[])Session["constrring"]);
        if (dsData != null)
        {
            if (dsData.Tables[0].Rows.Count > 0)
            {
                txtDescripcion.Text = dsData.Tables[0].Rows[0][1].ToString();
                chkestado.Checked = bool.Parse(dsData.Tables[0].Rows[0][4].ToString());
                txtHoraIni.Text = dsData.Tables[0].Rows[0][2].ToString();
                txtHoraFin.Text = dsData.Tables[0].Rows[0][3].ToString();
            }
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

        Array.Resize(ref objparam, 6);
        objparam[0] = txtDescripcion.Text.ToUpper();
        objparam[1] = txtHoraIni.Text;
        objparam[2] = txtHoraFin.Text;
        objparam[3] = chkestado.Checked;
        objparam[4] = Session["usuCodigo"];
        objparam[5] = Request["CodTurno"].ToString();
        dsData = fun.consultarDatos("spActTurnos", objparam, Page, (String[])Session["constrring"]);

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
        Array.Resize(ref objparam, 1);
        objparam[0] = Request["CodTurno"].ToString();

        dsData = fun.consultarDatos("spEliTurnos", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows[0][0].ToString() == "Planificacion")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Existe una planificación con el turno actual, no puede ser eliminado');", true);
            return;
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='frmturAdmin.aspx?mensajeRetornado=Guardado Con éxito" + "';window.close();", true);
    }
    protected void chkestado_CheckedChanged(object sender, EventArgs e)
    {
        chkestado.Text = chkestado.Checked == true ? "Activo" : "Inactivo";
    }
    #endregion

}