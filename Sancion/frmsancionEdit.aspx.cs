using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Sancion_frmsancionEdit : System.Web.UI.Page
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
                Array.Resize(ref objparam, 2);
                objparam[0] = 0;
                objparam[1] = "22";
                fun.cargarCombos(ddlgrupo, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
                ddlgrupo.Items.RemoveAt(0);
                objparam[0] = 0;
                objparam[1] = "21";
                fun.cargarCombos(ddlgravedad, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
                ddlgravedad.Items.RemoveAt(0);

                funCargaMantenimiento(Request["codsancion"].ToString());
            }
        }
        catch (Exception ex)
        {
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", ex.Message);
        }
    }
    #endregion

    #region Procedimientos y Funciones
    protected void funCargaMantenimiento(String strCodigoSancion)
    {
        txtcodigo.Text = strCodigoSancion;
        Array.Resize(ref objparam, 1);
        objparam[0] = strCodigoSancion;
        dsData = fun.consultarDatos("spCargSancioRead", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0)
        {
            ddlgrupo.SelectedValue = dsData.Tables[0].Rows[0][0].ToString();
            txtdescrip.Text = dsData.Tables[0].Rows[0][1].ToString();
            ddlgravedad.SelectedValue = dsData.Tables[0].Rows[0][2].ToString();
            txttiempo.Text = dsData.Tables[0].Rows[0][3].ToString();
            chkestado.Text = Convert.ToString(dsData.Tables[0].Rows[0][4].ToString()) == "True" ? "Activo" : "Inactivo";
            chkestado.Checked = Convert.ToBoolean(dsData.Tables[0].Rows[0][4].ToString());
            Session["Descripante"] = txtdescrip.Text;
        }
    }
    #endregion

    #region Botones y Eventos
    protected void btngrabar_Click(object sender, ImageClickEventArgs e)
    {
        Array.Resize(ref objparam, 9);
        objparam[0] = txtcodigo.Text;
        objparam[1] = ddlgrupo.SelectedValue;
        objparam[2] = txtdescrip.Text.ToString().ToUpper();
        objparam[3] = Session["Descripante"].ToString();
        objparam[4] = ddlgravedad.SelectedValue;
        objparam[5] = txttiempo.Text;
        objparam[6] = chkestado.Checked;
        objparam[7] = Session["usuCodigo"].ToString();
        objparam[8] = Session["MachineName"].ToString();
        dsData = fun.consultarDatos("spModifSancion", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows[0][0].ToString() == "Existe")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ya existe Sanción Creada, por favor Cree otra');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='frmsancionAdmin.aspx?mensajeRetornado=Guardado Con éxito" + "';window.close();", true);
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