using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class TareaN_frmtarNew : System.Web.UI.Page
{
    #region Variables
    DataSet dsData = new DataSet();
    Object[] objparam = new Object[1];
    Funciones fun = new Funciones();
    #endregion

    #region Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbltitulo.Text = "Agregar Nueva Tarea";
        }
    }
    #endregion

    #region Botones y Eventos
    protected void btngrabar_Click(object sender, ImageClickEventArgs e)
    {
        if (fun.ruta_bien_escrita(txtruta.Text))
        {
            Array.Resize(ref objparam, 5);
            objparam[0] = txttarea.Text;
            objparam[1] = txtruta.Text;
            objparam[2] = 1;
            objparam[3] = Session["usuCodigo"];
            objparam[4] = Session["MachineName"];
            dsData = fun.consultarDatos("spnTareaNewCreate", objparam, Page, (String[])Session["constrring"]);
            if (dsData.Tables[0].Rows[0][0].ToString() == "Existe")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ya existe la Tarea Creada, por favor Cree otra');", true);
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='frmtarAdmin.aspx?mensajeRetornado=Guardado Con éxito" + "';window.close();", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Nombre de la página mal escrito');", true);
        }
    }
    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.close();", true);
    }
    #endregion
}