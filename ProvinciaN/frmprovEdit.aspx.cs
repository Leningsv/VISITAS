using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class ProvinciaN_frmprovEdit : System.Web.UI.Page
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
                lbltitulo.Text = "Modificar Provincia";
                txtcodigo.Text = Request["proest_codigo"].ToString();
                funCargarpost(txtcodigo.Text);
            }
        }
        catch (Exception ex)
        {
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", ex.Message);
        }
    }
    #endregion

    #region Funciones y Procedimientos

    protected void funCargarpost(String codigoProv)
    {
        Array.Resize(ref objparam, 1);
        objparam[0] = codigoProv;
        dsData = fun.consultarDatos("spCargProv", objparam, Page, (String[])Session["constrring"]);
        //Session["provincia"] = Request["descrip"].ToString();
        txtprovincia.Text = dsData.Tables[0].Rows[0][1].ToString();
        Session["provincia"] = txtprovincia.Text;
        objparam[0] = "";
        fun.cargarCombos(ddlpais, "spCarPaisCmb", objparam, Page, (String[])Session["constrring"]);
        Session["codpais"] = dsData.Tables[0].Rows[0][0].ToString();
        ddlpais.SelectedValue = Session["codpais"].ToString();
    }

    #endregion

    #region Botones y Eventos
    protected void btngrabar_Click(object sender, ImageClickEventArgs e)
    {
        Int16 validar = 0;
        if (Session["codpais"].ToString() == ddlpais.SelectedValue && txtprovincia.Text.ToUpper() == Session["provincia"].ToString())
        {
            validar = 1;
        }
        Array.Resize(ref objparam, 5);
        objparam[0] = txtcodigo.Text;
        objparam[1] = ddlpais.SelectedValue;
        objparam[2] = txtprovincia.Text.ToUpper();
        objparam[3] = Session["usuCodigo"].ToString();
        objparam[4] = validar;
        dsData = fun.consultarDatos("spEditProvincia", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows[0][0].ToString() == "Existe")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ya existe Provincia Creada, por favor ingrese otra');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='frmprovAdmin.aspx?mensajeRetornado=Guardado Con éxito" + "';window.close();", true);
        }
    }
    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.close();", true);
    }
    #endregion
}