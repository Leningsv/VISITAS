using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class UsuarioN_frmusuEdit : System.Web.UI.Page
{
    #region Variables
    SICriptoDotNet001.Criptografia objEncripta = new SICriptoDotNet001.Criptografia();
    DataSet dsData = new DataSet();
    ListItem listadatos = new ListItem();
    Object[] objparam = new Object[1];
    Funciones fun = new Funciones();
    String strObt = "";
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
                objparam[1] = "1";
                strObt = fun.ValidarCampoClave(txtcontrasenia, Page, "spCargarParametros", objparam, (String[])Session["constrring"]);
                String[] sessiones = strObt.Split('|');
                Session["exigirD"] = sessiones[0].ToString();
                Session["minimocar"] = sessiones[1].ToString();
                if (!fun.IsNumber(Session["minimocar"].ToString())) Session["minimocar"] = "6";
                lbltitulo.Text = "Ingresar Nuevo Usuario";
                lbltitulo.Text = "Modificar Usuario";
                Array.Resize(ref objparam, 1);
                objparam[0] = "";
                fun.cargarCombos(ddldepartamento, "spCarDEpartament", objparam, Page, (String[])Session["constrring"]);
                fun.cargarCombos(ddlperfil, "spnUsuNewReadPerfil", objparam, Page, (String[])Session["constrring"]);
                funCargarpost();
            }
        }
        catch (Exception ex)
        {
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", ex.Message);
        }
    }
    #endregion

    #region Funciones y Procedimientos
    protected void funCargarpost()
    {
        Array.Resize(ref objparam, 1);
        objparam[0] = Request["usuCodigo"];
        dsData = fun.consultarDatos("spnUsuEditRead", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0)
        {
            txtcodigo.Text = dsData.Tables[0].Rows[0][0].ToString();
            ddldepartamento.SelectedValue = dsData.Tables[0].Rows[0][3].ToString();
            txtnombres.Text = dsData.Tables[0].Rows[0][1].ToString();
            txtapellidos.Text = dsData.Tables[0].Rows[0][2].ToString();
            txtusuario.Text = dsData.Tables[0].Rows[0][4].ToString();
            txtcontrasenia.Attributes.Add("Value", objEncripta.funDesencriptacion(dsData.Tables[0].Rows[0][5].ToString(), 1, "ESISEG"));
            ddlperfil.SelectedValue = dsData.Tables[0].Rows[0][6].ToString();
            chkestado.Text = dsData.Tables[0].Rows[0][7].ToString();
            chkestado.Checked = dsData.Tables[0].Rows[0][7].ToString() == "Activo" ? true : false;
            chkeliminar.Checked = dsData.Tables[0].Rows[0][8].ToString() == "S" ? true : false;
            chkeliminar.Text = dsData.Tables[0].Rows[0][8].ToString() == "S" ? "SI" : "NO";
            Session["userante"] = txtusuario.Text;
        }
    }
    #endregion

    #region Botones y Eventos
    protected void btngrabar_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["exigirD"].ToString() == "SI")
        {
            if (txtcontrasenia.Text.Trim().Length < Convert.ToInt16(Session["minimocar"]))
            {
                lblerror.Text = "Longitud mínima de clave es: " + Session["minimocar"].ToString();
                lblerror.Visible = true;
                return;
            }
        }
        Int16 validar = 0;
        if (Session["userante"].ToString().ToUpper() == txtusuario.Text.Trim().ToUpper())
        {
            validar = 1;
        }
        Array.Resize(ref objparam, 13);
        objparam[0] = ddlperfil.SelectedValue;
        objparam[1] = txtnombres.Text.ToUpper();
        objparam[2] = txtapellidos.Text.ToUpper();
        objparam[3] = ddldepartamento.SelectedValue;
        objparam[4] = txtusuario.Text;
        objparam[5] = objEncripta.funEncriptacion(txtcontrasenia.Text, 1, "ESISEG");
        objparam[6] = chkestado.Checked;
        objparam[7] = Session["usuCodigo"].ToString();
        objparam[8] = Session["MachineName"].ToString();
        objparam[9] = 0;
        objparam[10] = txtcodigo.Text;
        objparam[11] = validar;
        objparam[12] = chkeliminar.Checked == true ? "S" : "N";
        dsData = fun.consultarDatos("spnUsuNewCreate", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows[0][0].ToString() == "Existe")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ya existe Usuario Creado, por favor ingrese otro ');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='frmusuAdmin.aspx?mensajeRetornado=Guardado Con éxito" + "';window.close();", true);
        }
    }
    #endregion

    #region Botones y Eventos
    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.close();", true);
    }
    protected void chkestado_CheckedChanged(object sender, EventArgs e)
    {
        chkestado.Text = chkestado.Checked == true ? "Activo" : "Inactivo";
    }
    protected void chkeliminar_CheckedChanged(object sender, EventArgs e)
    {
        chkeliminar.Text = chkeliminar.Checked == true ? "SI" : "NO";
    }
    #endregion
}