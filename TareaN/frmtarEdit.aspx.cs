using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class TareaN_frmtarEdit : System.Web.UI.Page
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
                lbltitulo.Text = "Editar Tareas";
                funCargaMantenimiento(Request["codigo"].ToString());
            }
        }
        catch (Exception ex)
        {
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar la Página", ex.Message);
        }
    }
    #endregion

    #region Procedimientos y Funciones
    protected void funCargaMantenimiento(String strCodigoTarea)
    {
        txtcodigo.Text = strCodigoTarea;
        Array.Resize(ref objparam, 2);
        objparam[0] = 0;
        objparam[1] = strCodigoTarea;
        dsData = fun.consultarDatos("spnTareaEditRead", objparam, Page, (String[])Session["constrring"]);
        txttarea.Text = dsData.Tables[0].Rows[0][1].ToString();
        txtruta.Text = dsData.Tables[0].Rows[0][2].ToString();
        chkestado.Text = dsData.Tables[0].Rows[0][3].ToString();
        chkestado.Checked = dsData.Tables[0].Rows[0][3].ToString() == "Activo" ? true : false;
        Session["descripcion"] = txttarea.Text;
    }
    #endregion

    #region Botones y Eventos
    protected void btngrabar_Click(object sender, ImageClickEventArgs e)
    {
        if (fun.ruta_bien_escrita(txtruta.Text))
        {
            int valida = 0;
            if (Session["descripcion"].ToString().ToUpper() == txttarea.Text.Trim().ToUpper())
            {
                valida = 1;
            }
            Array.Resize(ref objparam, 7);
            objparam[0] = txtcodigo.Text;
            objparam[1] = txttarea.Text;
            objparam[2] = txtruta.Text;
            objparam[3] = chkestado.Checked;
            objparam[4] = Session["usuCodigo"];
            objparam[5] = Session["MachineName"];
            objparam[6] = valida;
            dsData = fun.consultarDatos("spnTareaEditUpdate", objparam, Page, (String[])Session["constrring"]);
            if (dsData.Tables[0].Rows[0][0].ToString() == "Existe")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ya existe la Tarea, por favor Cree otra');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='frmtarAdmin.aspx?mensajeRetornado=Guardado Con éxito" + "';window.close();", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Nombre de la página mal escrito');", true);
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