using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class CRS_frmcrsEdit : System.Web.UI.Page
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
                lbltitulo.Text = "Modificar CRS";
                txtcodigo.Text = Request["crsCodigo"].ToString();
                objparam[0] = "";
                fun.cargarCombos(ddlpais, "spCarPaisCmb", objparam, Page, (String[])Session["constrring"]);
                ddlpais.Items.RemoveAt(0);

                Array.Resize(ref objparam, 1);
                objparam[0] = ddlpais.SelectedValue;
                fun.cargarCombos(ddlprovincia, "spCarProvincia", objparam, Page, (String[])Session["constrring"]);                
                funCargarDatos(txtcodigo.Text);
            }
        }
        catch (Exception ex)
        {
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", ex.Message);
        }
    }

    #region Procedimientos y Funciones
    protected void funCargarDatos(String strCodigoCaf)
    {
        try
        {
            Array.Resize(ref objparam, 1);
            objparam[0] = strCodigoCaf;
            dsData = fun.consultarDatos("spEditReadCRS", objparam, Page, (String[])Session["constrring"]);
            if (dsData != null)
            {
                if (dsData.Tables[0].Rows.Count > 0)
                {
                    txtnombre.Text = dsData.Tables[0].Rows[0][0].ToString();
                    txtdirector.Text = dsData.Tables[0].Rows[0][1].ToString();
                    txtdireccion.Text = dsData.Tables[0].Rows[0][2].ToString();
                    txtfono1.Text = dsData.Tables[0].Rows[0][3].ToString();
                    txtfono2.Text = dsData.Tables[0].Rows[0][4].ToString();
                    txtcelular.Text = dsData.Tables[0].Rows[0][5].ToString();
                    chkestado.Checked = bool.Parse(dsData.Tables[0].Rows[0][6].ToString());
                    var dato = dsData.Tables[0].Rows[0][6].ToString();
                    chkestado.Text = dsData.Tables[0].Rows[0][6].ToString() == "True" ? "Activo" : "Inactivo";
                    ddlprovincia.SelectedValue = dsData.Tables[0].Rows[0][7].ToString();
                    Array.Resize(ref objparam, 2);
                    objparam[0] = ddlprovincia.SelectedValue;
                    objparam[1] = 1;
                    fun.cargarCombos(ddlciudad, "spCarCiudadweb", objparam, Page, (String[])Session["constrring"]);
                    ddlciudad.SelectedValue = dsData.Tables[0].Rows[0][8].ToString();
                }
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
        int valida = 0;
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
        objparam[9] = chkestado.Checked;
        objparam[10] = Session["usuCodigo"].ToString();
        objparam[11] = Session["MachineName"].ToString();
        objparam[12] = txtcodigo.Text;
        objparam[13] = 1;
        objparam[14] = valida;
        dsData = fun.consultarDatos("spInsertCRS", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows[0][0].ToString() == "Existe")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ya existe CRS Creado, por favor ingrese otro');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='frmcrsAdmin.aspx?mensajeRetornado=Guardado Con éxito" + "';window.close();", true);
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
        ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.close();", true);
    }
    protected void chkestado_CheckedChanged(object sender, EventArgs e)
    {
        chkestado.Text = chkestado.Checked == true ? "Activo" : "Inactivo";
    }
    #endregion
}