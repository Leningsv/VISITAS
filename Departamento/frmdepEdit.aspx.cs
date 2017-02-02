using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Departamento_frmdepEdit : System.Web.UI.Page
{
    #region Variables
    DataSet dsData = new DataSet();
    Object[] objparam = new Object[1];
    Funciones fun = new Funciones();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                lbltitulo.Text = "Modificar Departamento";
                funCargarpost(Request["CodDep"].ToString());
            }
        }
        catch (Exception ex)
        {
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", ex.Message);
        }
    }

    #region Procedimientos y Funciones
    protected void funCargarpost(String strCodigoDep)
    {
        txtcodigo.Text = strCodigoDep;
        Array.Resize(ref objparam, 1);
        objparam[0] = strCodigoDep;
        dsData = fun.consultarDatos("spnDeparEditRead", objparam, Page, (String[])Session["constrring"]);
        txtdepartamento.Text = dsData.Tables[0].Rows[0][1].ToString();
        Session["deparatamento"] = txtdepartamento.Text;
        chkestado.Text = dsData.Tables[0].Rows[0][2].ToString() == "True" ? "Activo" : "Inactivo";
        chkestado.Checked = dsData.Tables[0].Rows[0][2].ToString() == "True" ? true : false;
    }
    #endregion

    #region Botones y Eventos
    protected void btngrabar_Click(object sender, ImageClickEventArgs e)
    {
        int valida = 0;
        if (Session["deparatamento"].ToString().ToUpper() == txtdepartamento.Text.Trim().ToUpper())
        {
            valida = 1;
        }
        Array.Resize(ref objparam, 6);
        objparam[0] = txtcodigo.Text;
        objparam[1] = txtdepartamento.Text.ToUpper();
        objparam[2] = chkestado.Checked;
        objparam[3] = Session["usuCodigo"];
        objparam[4] = Session["MachineName"];
        objparam[5] = valida;
        dsData = fun.consultarDatos("spnDeparEditUpdate", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows[0][0].ToString() == "Existe")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ya existe el Departamento Creado, por favor Cree otro');", true);
            return;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='frmdepAdmin.aspx?mensajeRetornado=Guardado Con éxito" + "';window.close();", true);
        }
    }
    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.close();", true);
    }
    protected void chkestado_CheckedChanged(object sender, EventArgs e)
    {
        chkestado.Text = chkestado.Checked == true ? "Activo" : "Inactivo";
    }
    #endregion
}