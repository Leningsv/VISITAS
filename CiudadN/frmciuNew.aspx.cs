using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class CiudadN_frmciuNew : System.Web.UI.Page
{
    #region Variables
    DataSet dsData = new DataSet();
    ListItem listadatos = new ListItem();
    Object[] objparam = new Object[1];
    Funciones fun = new Funciones();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                lbltitulo.Text = "Ingresar Nueva Ciudad";
                objparam[0] = "";
                fun.cargarCombos(ddlpais, "spCarPaisCmb", objparam, Page, (String[])Session["constrring"]);
                ddlpais.SelectedIndex = 1;
                Array.Resize(ref objparam, 1);
                objparam[0] = ddlpais.SelectedValue;
                fun.cargarCombos(ddlprovincia, "spCarProvincia", objparam, Page, (String[])Session["constrring"]);
            }
        }
        catch (Exception ex)
        {
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", ex.Message);
        }
    }

    #region Botones y Eventos
    protected void btngrabar_Click(object sender, ImageClickEventArgs e)
    {
        Array.Resize(ref objparam, 5);
        objparam[0] = ddlpais.SelectedValue;
        objparam[1] = ddlprovincia.SelectedValue;
        objparam[2] = txtciudad.Text.ToUpper();
        objparam[3] = ddlregion.SelectedValue;
        objparam[4] = Session["usuCodigo"].ToString();
        dsData = fun.consultarDatos("SpInsCiudad", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows[0][0].ToString() == "Existe")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ya existe la Ciudad Creada, por favor ingrese otra');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='frmciuAdmin.aspx?mensajeRetornado=Guardado Con éxito" + "';window.close();", true);
        }
    }
    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.close();", true);
    }
    #endregion
}