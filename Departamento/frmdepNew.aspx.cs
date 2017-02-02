using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Departamento_frmdepNew : System.Web.UI.Page
{
    #region Variables
    DataSet dsData = new DataSet();
    Object[] objparam = new Object[1];
    Funciones fun = new Funciones();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbltitulo.Text = "Agregar Nuevo Departamento";
        }
    }

    #region Botones y Eventos
    protected void btngrabar_Click(object sender, ImageClickEventArgs e)
    {
        Array.Resize(ref objparam, 6);
        objparam[0] = txtdepartamento.Text.ToUpper();
        objparam[1] = 0;
        objparam[2] = 0;
        objparam[3] = 1;
        objparam[4] = Session["usuCodigo"];
        objparam[5] = Session["MachineName"];
        dsData = fun.consultarDatos("spInsDepartamentoweb", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows[0][0].ToString() == "Existe")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ya existe el Departamento Creado, por favor Cree otro');", true);
            return;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='frmdepAdmin.aspx?mensajeRetornado=Guardado Con éxito" + "';window.close();", true);
        }
    }
    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.close();", true);
    }
    #endregion
}