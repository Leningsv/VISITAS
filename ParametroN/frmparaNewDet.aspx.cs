using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class ParametroN_frmparaNewDet : System.Web.UI.Page
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
                lbltitulo.Text = "PARAMETRO: " + Session["NomParam"].ToString();
                Session["codigoparame"] = Request["Codigo_Parametro"].ToString();
                Session["codigodetpara"] = Request["Codigo_Det"].ToString();
                //Session["valordet"] = Request["valor"].ToString();
                //Session["nomdeta"] = Request["descripdet"].ToString();

                funCargaMantenimiento(Session["codigodetpara"].ToString());
            }
        }
        catch (Exception ex)
        {
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrión un Error al Cargar Datos", ex.Message);
        }
    }
    #endregion

    #region Procedimientos y Funciones
    protected void funCargaMantenimiento(String strCodigoDetParam)
    {
        try
        {
            if (strCodigoDetParam == "Nuevo")
            {
                chkestado.Enabled = false;
                chkestado.Checked = true;
                chkestado.Text = "Activo";
                Session["valordet"] = "";
                Session["nomdeta"] = "";
            }
            else
            {
                if (Session["eliminar"].ToString() == "SI") btneliminar.Visible = true;
                if (Session["modificar"].ToString() == "NO")
                {
                    txtnombre.Enabled = false;
                    txtdescri.Enabled = false;
                    txtvalor.Enabled = false;
                    chkestado.Enabled = false;
                }
                Array.Resize(ref objparam, 5);
                objparam[0] = Session["codigoparame"].ToString();
                objparam[1] = strCodigoDetParam;
                objparam[2] = 1;
                objparam[3] = "";
                objparam[4] = "";
                dsData = fun.consultarDatos("spCarParaDetalle", objparam, Page, (String[])Session["constrring"]);
                if (dsData.Tables[0].Rows.Count > 0)
                {
                    txtnombre.Text = dsData.Tables[0].Rows[0][0].ToString();
                    txtdescri.Text = dsData.Tables[0].Rows[0][1].ToString();
                    txtvalor.Text = dsData.Tables[0].Rows[0][2].ToString();
                    Session["valordet"] = txtvalor.Text;
                    Session["nomdeta"] = txtnombre.Text;
                    chkestado.Text = dsData.Tables[0].Rows[0][3].ToString() == "True" ? "Activo" : "Inactivo";
                    chkestado.Checked = dsData.Tables[0].Rows[0][3].ToString() == "True" ? true : false;
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
        Array.Resize(ref objparam, 15);
        objparam[0] = Session["codigoparame"].ToString();
        objparam[1] = Session["codigodetpara"].ToString();
        objparam[2] = txtnombre.Text.ToUpper();
        objparam[3] = txtdescri.Text.ToUpper();
        objparam[4] = txtvalor.Text;
        objparam[5] = chkestado.Checked;
        objparam[6] = Session["usuCodigo"];
        objparam[7] = Session["nomdeta"].ToString();
        objparam[8] = Session["valordet"].ToString();
        objparam[9] = "Parámetros";
        objparam[10] = "Parámetros Generales";
        objparam[11] = "frmparaNewDet.aspx";
        objparam[12] = "Modificar";
        objparam[13] = "MODIFICADO POR EL USUARIO  Código Cabecera(LOG_auxi1) - Código Detalle(LOG_auxi2)";
        objparam[14] = Session["MachineName"].ToString();
        dsData = fun.consultarDatos("spnParaNewCreateDet", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows[0][0].ToString() == "Existe")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ya existe el Parámetro Creado, por favor Cree otro');", true);
            return;
        }
        else if (dsData.Tables[0].Rows[0][0].ToString() == "Existe val")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Este valor ya está asignado a otro parámetro');", true);
            return;
        }        
        ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='frmparaEdit.aspx?paraCodigo=" + Session["codigoparame"].ToString() + "&mensajeRetornado=Guardado Con éxito" + "';window.close();", true);
    }

    protected void btneliminar_Click(object sender, ImageClickEventArgs e)
    {
        String strOk = "OK";
        Array.Resize(ref objparam, 9);
        objparam[0] = Session["codigoparame"].ToString();
        objparam[1] = Session["codigodetpara"].ToString();
        objparam[2] = "Parámetros";
        objparam[3] = "Parámetros Generales";
        objparam[4] = "frmparaNewDet.aspx";
        objparam[5] = "Eliminar";
        objparam[6] = "ELIMINADO POR EL USUARIO  Código Cabecera(LOG_auxi1) - Código Detalle(LOG_auxi2)";
        objparam[7] = Session["usuCodigo"].ToString();
        objparam[8] = Session["MachineName"].ToString();
        strOk = fun.insertarDatos("spEliParaDet", objparam, Page, (String[])Session["constrring"]);
        if (strOk == "Ok")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='frmparaEdit.aspx?paraCodigo=" + Session["codigoparame"].ToString() + "&mensajeRetornado=Eliminado con Éxito" + "';window.close();", true);
        }
    }
    protected void chkestado_CheckedChanged(object sender, EventArgs e)
    {
        chkestado.Text = chkestado.Checked == true ? "Activo" : "Inactivo";
    }
    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {        
        ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.close();", true);
    }
    #endregion
}