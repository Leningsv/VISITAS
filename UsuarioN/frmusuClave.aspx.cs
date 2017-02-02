using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class UsuarioN_frmusuClave : System.Web.UI.Page
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
                //var usunew = Session["usuLogin"].ToString();
                txtcodigo.Text = Session["usuLogin"].ToString();
                Array.Resize(ref objparam, 2);
                objparam[0] = 0;
                objparam[1] = "1";
                strObt = fun.ValidarCampoClave(txtcnueva, Page, "spCargarParametros", objparam, (String[])Session["constrring"]);
                String[] sessiones = strObt.Split('|');
                Session["exigirD"] = sessiones[0].ToString();
                Session["minimocar"] = sessiones[1].ToString();
                if (!fun.IsNumber(Session["minimocar"].ToString())) Session["minimocar"] = "6";
                lbltitulo.Text = "Cambiar Clave Actual";

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
        if (Session["exigirD"].ToString() == "SI")
        {
            if (txtcnueva.Text.Trim().Length < Convert.ToInt16(Session["minimocar"]))
            {
                String error = "Longitud mínima de clave es: " + Session["minimocar"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + error + "')", true);
                return;
            }
        }
        Array.Resize(ref objparam, 1);
        objparam[0] = txtcodigo.Text;
        dsData = fun.consultarDatos("spVerLogin", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0)
        {            
            if (objEncripta.funDesencriptacion(dsData.Tables[0].Rows[0][8].ToString(), 1, "ESISEG") != txtcanterior.Text.Trim())
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Contraseña anterior incorrecta');", true);
                return;
            }
            Array.Resize(ref objparam, 8);
            objparam[0] = txtcodigo.Text;
            objparam[1] = objEncripta.funEncriptacion(txtcnueva.Text, 1, "ESISEG");
            objparam[2] = "Seguridad";
            objparam[3] = "Cambiar Clave";
            objparam[4] = "frmusuClave.aspx";
            objparam[5] = "Actualizar"; //Puede ser Insertar / Eliminar / Actualizar / Consultar 
            objparam[6] = Session["usuCodigo"].ToString();
            objparam[7] = Session["MachineName"].ToString();
            dsData = fun.consultarDatos("spEditPassword", objparam, Page, (String[])Session["constrring"]);
            if (dsData.Tables[0].Rows[0][0].ToString() == "Grabo")
            {
                Response.Redirect("~/Reload.html");
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Contraseña incorrecta');", true);
        }
    }
    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Index1.aspx");
    }
    #endregion
}