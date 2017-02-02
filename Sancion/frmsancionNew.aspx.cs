using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Sancion_frmsancionNew : System.Web.UI.Page
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
        Array.Resize(ref objparam, 7);
        objparam[0] = ddlgrupo.SelectedValue;
        objparam[1] = txtdescrip.Text.ToString().ToUpper();
        objparam[2] = ddlgravedad.SelectedValue;
        objparam[3] = txttiempo.Text;
        objparam[4] = chkestado.Checked;
        objparam[5] = Session["usuCodigo"].ToString();
        objparam[6] = Session["MachineName"].ToString();
        dsData = fun.consultarDatos("spInserSancion", objparam, Page, (String[])Session["constrring"]);
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
    #endregion
}