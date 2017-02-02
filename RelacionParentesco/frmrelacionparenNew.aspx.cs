using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class RelacionParentesco_frmrelacionparenNew : System.Web.UI.Page
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
                lbltitulo.Text = "Ingresar nueva relación parentesco con tipo de visita";
                Array.Resize(ref objparam, 2);
                objparam[0] = 0;
                objparam[1] = "15";
                fun.cargarCombos(ddlparentesco, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
                ddlparentesco.Items.RemoveAt(0);
                objparam[0] = 0;
                objparam[1] = "14";
                fun.cargarCombos(ddltipovisita, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
                ddltipovisita.Items.RemoveAt(0);
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
        Array.Resize(ref objparam, 2);
        objparam[0] = ddlparentesco.SelectedValue;
        objparam[1] = ddltipovisita.SelectedValue;
        dsData = fun.consultarDatos("spInserRelaParenvis", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows[0][0].ToString() == "Existe")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ya existe una relación establecida, por favor ingrese otra');", true);
            return;
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='frmrelacionparenAdmin.aspx?mensajeRetornado=Guardado Con éxito" + "';window.close();", true);
    }
    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.close();", true);
    }
    #endregion
}