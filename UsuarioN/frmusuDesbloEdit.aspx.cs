using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class UsuarioN_frmusuDesbloEdit : System.Web.UI.Page
{
    #region Variables
    DataSet dsData = new DataSet();
    ListItem listadatos = new ListItem();
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
                lbltitulo.Text = "Desbloquear Usuario: " + Request["usuLogin"].ToString();
                txtcodigo.Text = Request["usuLogin"].ToString();
            }
        }
        catch (Exception ex)
        {   
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", ex.Message);
        }
    }
    #endregion

    #region Botones y Eventos
    protected void btngrabar_Click(object sender, ImageClickEventArgs e)
    {
        Array.Resize(ref objparam, 8);
        objparam[0] = txtcodigo.Text;
        objparam[1] = "Seguridad";
        objparam[2] = "Desbloquear Usuario";
        objparam[3] = "frmusuDesbloEdit.aspx";
        objparam[4] = "Desbloquear"; //Puede ser Insertar / Eliminar / Actualizar / Consultar 
        objparam[5] = txtobservacion.Text.ToUpper();
        objparam[6] = Session["usuCodigo"].ToString();
        objparam[7] = Session["MachineName"].ToString();
        dsData = fun.consultarDatos("spDesbloUsuario", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows[0][0].ToString() == "Exito") ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='frmusuBloqAdmin.aspx?mensajeRetornado=Guardado Con éxito" + "';window.close();", true);   
    }
    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.close();", true);
    }
    #endregion
}