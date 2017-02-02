using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Politicas_frmpoliNew : System.Web.UI.Page
{
    #region Variables
    SIBDDNET.BDD objbdd = new SIBDDNET.BDD();
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
                lbltitulo.Text = "Agregar Nueva Política";
                if (Session["perCodigo"].ToString() == "1")
                {
                    chkeliminar.Enabled = true;
                    chkcrear.Enabled = true;
                    chkmodificar.Enabled = true;
                }

            }
        }
        catch (Exception ex)
        {
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrión un Error al Cargar Datos", ex.Message);
        }
    }
    #endregion

    #region Botones y EVentos
    protected void btngrabar_Click(object sender, ImageClickEventArgs e)
    {
        Array.Resize(ref objparam, 7);
        objparam[0] = txtnombre.Text.ToUpper();
        objparam[1] = txtdescri.Text.ToUpper();
        objparam[2] = chkeliminar.Checked;
        objparam[3] = chkcrear.Checked;
        objparam[4] = chkmodificar.Checked;
        objparam[5] = chkestado.Checked;
        objparam[6] = Session["usuCodigo"];
        dsData = fun.consultarDatos("spnPoliNewCreate", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows[0][0].ToString() == "Existe")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ya existe Política Creada, Por favor Cree otra');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='frmpoliAdmin.aspx?mensajeRetornado=Guardado Con éxito" + "';window.close();", true);
        }
    }

    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.close();", true);
    }
    #endregion
}