using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class CRS_frmcrsNew : System.Web.UI.Page
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
                lbltitulo.Text = "Ingresar Nuevo CRS";
                objparam[0] = "";
                fun.cargarCombos(ddlpais, "spCarPaisCmb", objparam, Page, (String[])Session["constrring"]);
                ddlpais.Items.RemoveAt(0);
                Array.Resize(ref objparam, 1);
                objparam[0] = ddlpais.SelectedValue;
                fun.cargarCombos(ddlprovincia, "spCarProvincia", objparam, Page, (String[])Session["constrring"]);
                ddlprovincia.SelectedIndex = 0;
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
        Array.Resize(ref objparam, 15);
        objparam[0] = ddlpais.SelectedValue;
        objparam[1] = ddlprovincia.SelectedValue;
        objparam[2] = ddlciudad.SelectedValue;
        objparam[3] = txtnombre.Text.ToUpper();
        objparam[4] = txtdirector.Text.ToUpper();
        objparam[5] = txtdireccion.Text.ToUpper();
        objparam[6] = txtfono1.Text.ToUpper();
        objparam[7] = txtfono2.Text.ToUpper();
        objparam[8] = txtcelular.Text.ToUpper();
        objparam[9] = 1;
        objparam[10] = Session["usuCodigo"].ToString();
        objparam[11] = Session["MachineName"].ToString();
        objparam[12] = "";
        objparam[13] = 0;
        objparam[14] = 0;
        dsData = fun.consultarDatos("spInsertCRS", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows[0][0].ToString() == "Existe")
        {
            lblerror.Visible = true;
        }
        else
        {
            Response.Redirect("frmcrsAdmin.aspx?mensajeRetornado='Guardado Con éxito'", false);
        }
    }
    protected void ddlprovincia_SelectedIndexChanged(object sender, EventArgs e)
    {
        Array.Resize(ref objparam, 2);
        objparam[0] = ddlprovincia.SelectedValue;
        objparam[1] = 1;
        fun.cargarCombos(ddlciudad, "spCarCiudadweb", objparam, Page, (String[])Session["constrring"]);
    }
    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/CRS/frmcrsAdmin.aspx");
    }
    #endregion
}