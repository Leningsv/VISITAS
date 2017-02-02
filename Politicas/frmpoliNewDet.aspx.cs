using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Politicas_frmpoliNewDet : System.Web.UI.Page
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
                lbltitulo.Text = "POLITICA: " + Session["NomParam"].ToString();
                Session["CodParam"] = Request["Codigo_Parametro".ToString()];
                Session["codigodetpara"] = Request["Codigo_Det"].ToString();
                funCargaMantenimiento(Request["Codigo_Det"].ToString());
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
            }
            else
            {
                chkestado.Enabled = true;
                if (Session["eliminar"].ToString() == "SI") btneliminar.Visible = true;
                if (Session["modificar"].ToString() == "NO")
                {
                    txtnombre.Enabled = false;
                    txtdescri.Enabled = false;
                    txtvalor.Enabled = false;
                    chkestado.Enabled = false;
                }

                Array.Resize(ref objparam, 5);
                objparam[0] = Session["CodParam"].ToString();
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
                    chkestado.Text = dsData.Tables[0].Rows[0][3].ToString() == "True" ? "Activo" : "Inactivo";
                    chkestado.Checked = dsData.Tables[0].Rows[0][3].ToString() == "True" ? true : false;
                    Session["valordet"] = txtvalor.Text;
                    Session["nomdeta"] = txtnombre.Text;
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
        objparam[0] = Session["CodParam"].ToString();
        objparam[1] = Session["codigodetpara"].ToString();
        objparam[2] = txtnombre.Text.ToUpper();
        objparam[3] = txtdescri.Text.ToUpper();
        objparam[4] = txtvalor.Text;
        objparam[5] = chkestado.Checked;
        objparam[6] = Session["usuCodigo"];
        objparam[7] = Session["nomdeta"].ToString();
        objparam[8] = Session["valordet"].ToString();
        objparam[9] = "Matenimiento CRS";
        objparam[10] = "Políticas CRS";
        objparam[11] = "frmpoliNewDet.aspx";
        objparam[12] = "Modificar";
        objparam[13] = "MODIFICADO POR EL USUARIO  Código Cabecera(LOG_auxi1) - Código Detalle(LOG_auxi2)";
        objparam[14] = Session["MachineName"].ToString();
        dsData = fun.consultarDatos("spnPoliNewCreateDet", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows[0][0].ToString() == "Existe")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ya existe la Política Creada, por favor Cree otra');", true);
            return;
        }        
        ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='frmpoliEdit.aspx?paraCodigo=" + Session["CodParam"].ToString() + "&mensajeRetornado=Guardado con Éxito" + "';window.close();", true);
    }

    protected void btneliminar_Click(object sender, ImageClickEventArgs e)
    {
        String strOk = "OK";
        Array.Resize(ref objparam, 9);
        objparam[0] = Session["CodParam"].ToString();
        objparam[1] = Session["codigodetpara"].ToString();
        objparam[2] = "Matenimiento CRS";
        objparam[3] = "Políticas CRS";
        objparam[4] = "frmpoliNewDet.aspx";
        objparam[5] = "Eliminar";
        objparam[6] = "ELIMINADO POR EL USUARIO  Código Cabecera(LOG_auxi1) - Código Detalle(LOG_auxi2)";
        objparam[7] = Session["usuCodigo"].ToString();
        objparam[8] = Session["MachineName"].ToString();
        strOk = fun.insertarDatos("spEliParaDet", objparam, Page, (String[])Session["constrring"]);
        if (strOk == "Ok")
        {
            Response.Redirect("frmpoliEdit.aspx?paraCodigo=" + Session["CodParam"].ToString() + "&descrip=" + Session["NomParam"].ToString() + "&MensajeRetornado='Eliminado con Éxito'");
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