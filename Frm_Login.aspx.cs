using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;

public partial class Frm_Login : System.Web.UI.Page
{
    #region variables
    String mstrAppName = "ESISEG";
    SIBDDNET.BDD objbdd = new SIBDDNET.BDD();
    SICriptoDotNet001.Criptografia objEncripta = new SICriptoDotNet001.Criptografia();
    String gstrError = "";
    Int16 contador = 0;
    Object objcon = new Object();
    String strres = "";
    Object[] objparam = new Object[1];
    DataSet dsData = new DataSet();
    String[] strparam = { "0" };
    Funciones fun = new Funciones();
    #endregion

    #region Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {                
                Session["validar"] = contador;
                if (((string)Session["ValidaPantalla"]) != "1")
                {
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    String strReq = "";
                    String stradminusu = "";
                    String strLogName = "";

                    Session["usuCodigo"] = "";
                    Session["perCodigo"] = "";
                    Session["usuNombres"] = "";
                    Session["usuApellidos"] = "";
                    Session["usuDepartamento"] = "";
                    Session["usuLogin"] = "";
                    Session["usuPassword"] = "";
                    Session["usuEstatus"] = "";
                    Session["usuElimina"] = "";
                    IPHostEntry hostEntry = System.Net.Dns.GetHostEntry(Request.UserHostAddress);
                    try
                    {
                        Session["MachineName"] = hostEntry.HostName.Substring(0, hostEntry.HostName.IndexOf("."));
                    }
                    catch (Exception ex)
                    {
                        Session["MachineName"] = hostEntry.HostName;
                    }
                    strReq = Request.Browser.Browser;
                    stradminusu = Request.Params["adminusu"];

                    if (stradminusu != "1")
                    {

                    }
                    else
                    {
                        if (strReq == "MSIE")
                        {
                            /*response.redirec('pagina que no existe')*/
                        }
                        try
                        {
                            strLogName = Session["LoginName"].ToString();

                        }
                        catch
                        {
                            strLogName = "";
                        }
                        if (strLogName != "")
                        {
                            lblError.Text = "No tiene los permisos suficientes para ingresar a la página solicitada.";
                        }
                        else
                        {
                            lblError.Text = "";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", ex.Message);
            }

        }
    }

    #endregion

    #region Procedimientos y Funciones
    private Boolean ValidarUsuario(String pstrUsuario, String pstrClave, String psteError)
    {

        Boolean validado = false;

        try
        {
            objbdd.subRegistroBDD("ESISEG", ref strparam, ref strres);

            if (strres == "c")
            {
                //POLITICA SI SE CONSULTA DEL REGISTRO CIVIL
                Session["regCIVIL"] = "SI";
                Array.Resize(ref objparam, 2);
                objparam[0] = "154";//PARAMETROS DE CONEXION ESIGPEN
                objparam[1] = "43";
                dsData = fun.consultarDatos("spCargaTotalParaDeta", objparam, Page, strparam);
                if (dsData.Tables[0].Rows[0][0].ToString() == "NO") Session["regCIVIL"] = "NO";

                //CANTIDAD DE HUELLAS A TOMAR
                Array.Resize(ref objparam, 2);
                objparam[0] = "141";
                objparam[1] = "43";
                dsData = fun.consultarDatos("spCargaTotalParaDeta", objparam, Page, strparam);
                if (dsData.Tables[0].Rows.Count == 0) Session["canthuellas"] = "2";
                else Session["canthuellas"] = dsData.Tables[0].Rows[0][0].ToString();
                if (!fun.IsNumber(Session["canthuellas"].ToString())) Session["canthuellas"] = "2";

                //TRAER LAS POLITICAS DE LOS USUARIOS (CANTIDAD DE INTENTOS)
                Array.Resize(ref objparam, 2);
                objparam[0] = "5";
                objparam[1] = "1";
                dsData = fun.consultarDatos("spCargaTotalParaDeta", objparam, Page, strparam);
                if (dsData.Tables[0].Rows.Count == 0) Session["intentos"] = "3";
                else Session["intentos"] = dsData.Tables[0].Rows[0][0].ToString();
                if (!fun.IsNumber(Session["intentos"].ToString())) Session["intentos"] = "3";

                Array.Resize(ref objparam, 1);
                objparam[0] = pstrUsuario;
                dsData = fun.consultarDatos("spVerLogin", objparam, Page, strparam);
                if (dsData.Tables[0].Rows.Count > 0)
                {

                    //String miclave = objEncripta.funEncriptacion("q", 1, "ESISEG");
                    //String clave = objEncripta.funDesencriptacion(dsData.Tables[0].Rows[0][8].ToString(), 1, "ESISEG");

                    if (objEncripta.funDesencriptacion(dsData.Tables[0].Rows[0][8].ToString(), 1, "ESISEG") == pstrClave.Trim() && dsData.Tables[0].Rows[0][9].Equals(true))
                    {
                        //Session["MachineName"] = System.Environment.MachineName;
                        Session["usuCodigo"] = dsData.Tables[0].Rows[0][0];
                        Session["perCodigo"] = dsData.Tables[0].Rows[0][1];
                        Session["usuNombres"] = dsData.Tables[0].Rows[0][2];
                        Session["usuApellidos"] = dsData.Tables[0].Rows[0][3];
                        Session["nombre"] = dsData.Tables[0].Rows[0][2] + " " + dsData.Tables[0].Rows[0][3];
                        Session["usuDepartamento"] = dsData.Tables[0].Rows[0][4];
                        Session["usuHorario"] = dsData.Tables[0].Rows[0][5];
                        Session["usuLogin"] = dsData.Tables[0].Rows[0][7];
                        Session["usuPassword"] = dsData.Tables[0].Rows[0][8];
                        Session["usuEstatus"] = dsData.Tables[0].Rows[0][9];
                        Session["usuElimina"] = dsData.Tables[0].Rows[0][19];
                        Session["pagCarga"] = Boolean.TrueString;
                        validado = true;
                    }
                    else
                    {
                        gstrError = "Paswword Inválido, ingrese nuevamente";
                        validado = false;
                    }
                }
                else
                {
                    gstrError = "El Usuario que esta intentando ingresar al sistema no existe" + Environment.NewLine
                        + "Por favor comuníquese con el administrador.";
                    validado = false;
                }
            }
            else
            {
                validado = false;
            }

            return validado;
        }
        catch (Exception err)
        {
            psteError = err.Message;
            return false;
        }
    }

    private Int16 SacarContador(String usuario)
    {
        Int16 contador = 0;
        Array.Resize(ref objparam, 1);
        objparam[0] = usuario;
        dsData = fun.consultarDatos("SpActContaUsu", objparam, Page, strparam);
        if (dsData.Tables[0].Rows.Count > 0)
        {
            if (Convert.ToInt16(dsData.Tables[0].Rows[0][0]) == -1)
            {
                contador = -1;
            }
            else
            {
                contador = Convert.ToInt16(dsData.Tables[0].Rows[0][0]);
            }
        }
        return contador;
    }
    private void actualizacontador(String usuario, Int16 contador)
    {
        Array.Resize(ref objparam, 2);
        objparam[0] = usuario;
        objparam[1] = contador;
        fun.insertarDatos("spseleccionusu", objparam, Page, strparam);
    }
    #endregion

    #region Botones y Eventos
    protected void btnIngresar_Click(object sender, EventArgs e)
    {
        Boolean blnUsu = false;
        String strres = "";
        Int16 mIntusuvalido = new Int16();

        if (!IsPostBack)
        {
            Session["usuLogin"] = txtUsuario.Text;

        }
        else
        {
            contador = Convert.ToInt16(Session["validar"]);
            contador++;
            Session["validar"] = contador;
            Session["usuLogin"] = txtUsuario.Text;

            blnUsu = ValidarUsuario(txtUsuario.Text, txtClave.Text, strres);

            if (strres == String.Empty)
            {
                if (blnUsu == true)
                {
                    mIntusuvalido = SacarContador(Session["usuLogin"].ToString());
                    if (mIntusuvalido == int.Parse(Session["intentos"].ToString()))
                    {
                        actualizacontador(Session["usuLogin"].ToString(), mIntusuvalido);
                        if (((string)Session["ValidaPantalla"]) == "1")
                        {
                            lblmensaje.Text = "Usuario Bloqueado consulte con el Administrador";
                            txtUsuario.Enabled = false;
                            txtClave.Enabled = false;
                        }
                        else
                        {
                            lblmensaje.Text = "Ha Superado el limite de Intentos consulte con el Administrador";
                            txtUsuario.Enabled = false;
                            txtClave.Enabled = false;
                        }
                    }
                    if (mIntusuvalido >= 0 && mIntusuvalido < int.Parse(Session["intentos"].ToString()))
                    {
                        actualizacontador(Session["usuLogin"].ToString(), 0);
                        lblError.Visible = false;
                        Response.Redirect("Index.aspx");
                    }
                    else
                    {
                        if (Convert.ToInt16(Session["validar"]) == 1)
                        {
                            lblmensaje.Text = "Usuario Bloqueado consulte con el Administrador";
                            txtUsuario.Enabled = false;
                            txtClave.Enabled = false;
                        }
                        else
                        {
                            lblmensaje.Text = "Ha Superado el limite de Intentos consulte con el Administrador";
                            txtUsuario.Enabled = false;
                            txtClave.Enabled = false;
                        }
                    }

                }
                else
                {
                    mIntusuvalido = SacarContador(Session["usuLogin"].ToString());
                    mIntusuvalido++;
                    if (mIntusuvalido == -1)
                    {
                        txtUsuario.Enabled = false;
                        txtClave.Enabled = false;
                    }
                    else
                    {
                        if (mIntusuvalido == int.Parse(Session["intentos"].ToString()))
                        {
                            actualizacontador(Session["usuLogin"].ToString(), mIntusuvalido);
                            if (Convert.ToInt16(Session["validar"]) == 1)
                            {
                                lblmensaje.Text = "Usuario Bloqueado consulte con el Administrador";
                                txtUsuario.Enabled = false;
                                txtClave.Enabled = false;
                            }
                            else
                            {
                                lblmensaje.Text = "Ha Superado el limite de Intentos consulte con el Administrador";
                                txtUsuario.Enabled = false;
                                txtClave.Enabled = false;
                            }
                        }
                        if (mIntusuvalido >= 0 && mIntusuvalido < int.Parse(Session["intentos"].ToString()))
                        {
                            lblError.Text = " Usuario o clave incorrectas";
                            actualizacontador(Session["usuLogin"].ToString(), mIntusuvalido);
                            mIntusuvalido = SacarContador(Session["usuLogin"].ToString());
                            lblmensaje.Text = "Intento Num : " + " " + mIntusuvalido.ToString();
                        }
                        else
                        {
                            if (Convert.ToInt16(Session["validar"]) == 1)
                            {
                                lblmensaje.Text = "Usuario Bloqueado consulte con el Administrador";
                                txtUsuario.Enabled = false;
                                txtClave.Enabled = false;
                            }
                            else
                            {
                                lblmensaje.Text = "Ha Superado el limite de Intentos consulte con el Administrador";
                                txtUsuario.Enabled = false;
                                txtClave.Enabled = false;
                            }
                        }
                    }
                }

            }
            else
            {
                lblError.Text = strres;
                txtUsuario.Text = "";
                txtClave.Text = "";
            }
        }
    }
    #endregion

}