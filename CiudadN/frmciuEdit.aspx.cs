using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class CiudadN_frmciuEdit : System.Web.UI.Page
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
                lbltitulo.Text = "Modificar Ciudad";
                txtcodigo.Text = Request["ciu_Codigo"].ToString();
                funCargarpost(txtcodigo.Text);
            }
        }
        catch (Exception ex)
        {
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", ex.Message);
        }
    }

    #region Funciones y Procedimientos
    protected void funCargarpost(String codigoCiu)
    {
        Array.Resize(ref objparam, 1);
        objparam[0] = codigoCiu;
        dsData = fun.consultarDatos("spCargCiudad", objparam, Page, (String[])Session["constrring"]);
        txtciudad.Text = dsData.Tables[0].Rows[0][2].ToString();
        Session["ciudad"] = txtciudad.Text;        
        objparam[0] = "";
        fun.cargarCombos(ddlpais, "spCarPaisCmb", objparam, Page, (String[])Session["constrring"]);
        Session["codpais"] = dsData.Tables[0].Rows[0][0].ToString();
        objparam[0] = Session["codpais"].ToString();
        fun.cargarCombos(ddlprovincia, "spCarProvincia", objparam, Page, (String[])Session["constrring"]);
        Session["codprov"] = dsData.Tables[0].Rows[0][1].ToString();
        ddlpais.SelectedValue = Session["codpais"].ToString();
        ddlprovincia.SelectedValue = Session["codprov"].ToString();
        ddlregion.SelectedValue = dsData.Tables[0].Rows[0][3].ToString();
    }
    #endregion

    #region Botones y Eventos
    protected void btngrabar_Click(object sender, ImageClickEventArgs e)
    {
        Int16 validar = 0;
        if (Session["codpais"].ToString() == ddlpais.SelectedValue && Session["codprov"].ToString() == ddlprovincia.SelectedValue && txtciudad.Text.ToUpper() == Session["ciudad"].ToString())
        {
            validar = 1;
        }
        Array.Resize(ref objparam, 7);
        objparam[0] = txtcodigo.Text;
        objparam[1] = ddlpais.SelectedValue;
        objparam[2] = ddlprovincia.SelectedValue;
        objparam[3] = txtciudad.Text.ToUpper();
        objparam[4] = ddlregion.SelectedValue;
        objparam[5] = Session["usuCodigo"].ToString();
        objparam[6] = validar;
        dsData = fun.consultarDatos("spEditCiudad", objparam, Page, (String[])Session["constrring"]);
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