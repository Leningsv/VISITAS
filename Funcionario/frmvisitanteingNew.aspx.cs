using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Funcionario_frmvisitanteingNew : System.Web.UI.Page
{
    #region Variables
    DataSet dsData = new DataSet();
    Object[] objparam = new Object[1];
    Funciones fun = new Funciones();
    String strObt = "";
    String foto = "";
    String rutahuell = "C:/Temp/";
    String[] mis_datos;
    #endregion

    #region Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack)
            {
                AccesoInternet();
                if (Page.Request.Params["__EVENTTARGET"] == "btnrefrescar_Click")
                {
                    RefrescarFoto(Session["codigotempVisitante"].ToString());
                }
                if (Page.Request.Params["__EVENTTARGET"] == "txtndocu")
                {
                    if (Session["regCIVIL"].ToString() == "SI")
                    {

                        funCargaDatosRegCivil();

                    }
                }
            }
            txtndocu.Attributes.Add("onchange", "Validar_Cedula();");
            if (!IsPostBack)
            {
                lbltitulo.Text = "Ingresar Nuevo visitante externo";
                AccesoInternet();
                Array.Resize(ref objparam, 2);
                objparam[0] = 0;
                objparam[1] = "4";
                fun.cargarCombos(ddltipodoc, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
                ddltipodoc.Items.RemoveAt(0);

                objparam[0] = 0;
                objparam[1] = "17";
                fun.cargarCombos(ddlgenero, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
                ddlgenero.Items.RemoveAt(0);

                Array.Resize(ref objparam, 1);
                objparam[0] = "";
                fun.cargarCombos(ddldepartamento, "spCarDEpartament", objparam, Page, (String[])Session["constrring"]);
                ddldepartamento.Items.RemoveAt(0);

                Array.Resize(ref objparam, 2);
                objparam[0] = 0;
                objparam[1] = "27";
                fun.cargarCombos(ddlcargo, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
                ddlcargo.Items.RemoveAt(0);

                objparam[0] = 0;
                objparam[1] = "36";
                fun.cargarCombos(ddltipofun, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
                ddltipofun.Items.RemoveAt(0);

                objparam[0] = 0;
                objparam[1] = "8";
                strObt = fun.ObtenerRutasFun(Page, "spCargarParametros", objparam, (String[])Session["constrring"]);
                String[] session = strObt.Split('|');
                Session["rutafoto"] = session[0].ToString();
                Session["rutahuella"] = session[1].ToString();

                if (Session["codigotempVisitante"] != null)
                {
                    ddltipodoc.SelectedValue = Session["tdoc"].ToString();
                    if (ddltipodoc.SelectedValue == "C")
                    {
                        txtndocu_FilteredTextBoxExtender.FilterMode = AjaxControlToolkit.FilterModes.ValidChars;
                        txtndocu_FilteredTextBoxExtender.InvalidChars = ".-";
                        txtndocu_FilteredTextBoxExtender.FilterType = AjaxControlToolkit.FilterTypes.Numbers;
                    }
                    else
                    {
                        txtndocu_FilteredTextBoxExtender.FilterMode = AjaxControlToolkit.FilterModes.InvalidChars;
                        txtndocu_FilteredTextBoxExtender.InvalidChars = ".-*/{{}}[[]]\\";
                        txtndocu_FilteredTextBoxExtender.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    }
                    txtndocu.Text = Session["ndoc"].ToString();
                    txtnombre1.Text = Session["nom1"].ToString();
                    txtnombre2.Text = Session["nom2"].ToString();
                    txtapellido1.Text = Session["ape1"].ToString();
                    txtapellido2.Text = Session["ape2"].ToString();
                    //txtfecnaci.Text = Session["fecnaci"].ToString();
                    ddlgenero.SelectedValue = Session["genero"].ToString();
                    txtentidad.Text = Session["institu"].ToString();
                    ddldepartamento.SelectedValue = Session["depar"].ToString();
                    ddlcargo.SelectedValue = Session["cargo"].ToString();
                    ddltipofun.SelectedValue = Session["funcio"].ToString();
                    txtobserva.Text = Session["observa"].ToString();
                    if (Session["Imagename"] != null)
                    {
                        foto = Session["Imagename"].ToString();
                        imgfoto.ImageUrl = Session["rutafoto"].ToString() + foto;
                        imgfoto.Width = 300;
                        imgfoto.Height = 250;
                    }
                }
                else
                {
                    Array.Resize(ref objparam, 1);
                    objparam[0] = "3";
                    dsData = fun.consultarDatos("spSecuencialGeneral", objparam, Page, (String[])Session["constrring"]);
                    Session["codigotempVisitante"] = dsData.Tables[0].Rows[0][0].ToString();
                }

                if (Request["mensajeRetornado"] != null)
                {
                    SIFunBasicas.Basicas.PresentarMensaje(Page, "::VISITA PPL::", Request["MensajeRetornado"]);
                }
            }
        }
        catch (Exception ex)
        {
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", ex.Message);
        }
    }
    #endregion

    #region Funciones y Procedimientos
    private void RefrescarFoto(string strCodigofun)
    {
        if (Session["Imagename"] != null) foto = Session["Imagename"].ToString();

        String rutasrv = Server.MapPath(Session["rutafoto"].ToString());
        if (foto == "") imgfoto.ImageUrl = "~/Images/sinfoto.jpg";
        else
        {
            if (File.Exists(rutasrv + foto))
            {
                imgfoto.ImageUrl = Session["rutafoto"].ToString() + foto;
                imgfoto.Width = 300;
                imgfoto.Height = 250;
            }
            else imgfoto.ImageUrl = "~/Images/sinfoto.jpg";
        }
    }
    #endregion

    #region Botones y Eventos
    protected void btngrabar_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["grabohuella"] == null) Session["grabohuella"] = "N";
        if (Session["Imagename"] != null) foto = Session["Imagename"].ToString();

        //if (!fun.IsFechaNacimiento(txtfecnaci.Text))
        //{
        //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Fecha de Nacimiento Incorrecta');", true);
        //    return;
        //}
        //PREGUNTAR SI EXISTE EL FUNCIONARIO
        Array.Resize(ref objparam, 1);
        objparam[0] = txtndocu.Text;
        dsData = fun.consultarDatos("spCargFuncioDocu", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Número de Documento ya existe');", true);
            return;
        }
        Session["tdoc"] = null;
        Session["ndoc"] = null;
        Session["nom1"] = null;
        Session["nom2"] = null;
        Session["ape1"] = null;
        Session["ape2"] = null;
        //Session["fecnaci"] = null;
        Session["genero"] = null;
        Session["celular"] = null;
        Session["email"] = null;
        Session["institu"] = null;
        Session["depar"] = null;
        Session["cargo"] = null;
        Session["funcio"] = null;
        Session["observa"] = null;

        Array.Resize(ref objparam, 23);
        objparam[0] = Session["codigotempVisitante"].ToString();
        objparam[1] = ddltipodoc.SelectedValue;
        objparam[2] = txtndocu.Text;
        objparam[3] = txtnombre1.Text.ToUpper();
        objparam[4] = txtnombre2.Text.ToUpper();
        objparam[5] = txtapellido1.Text.ToUpper();
        objparam[6] = txtapellido2.Text.ToUpper();
        objparam[7] = DateTime.Now;//txtfecnaci.Text;
        objparam[8] = ddlgenero.SelectedValue;
        objparam[9] = "";
        objparam[10] = "";
        objparam[11] = txtentidad.Text.ToUpper();
        objparam[12] = ddldepartamento.SelectedValue;
        objparam[13] = ddlcargo.SelectedValue;
        objparam[14] = ddltipofun.SelectedValue;
        objparam[15] = "";
        objparam[16] = txtobserva.Text.ToUpper();
        objparam[17] = true;
        objparam[18] = foto;
        objparam[19] = Session["grabohuella"].ToString();
        objparam[20] = Session["usuCodigo"].ToString();
        objparam[21] = Session["MachineName"].ToString();
        objparam[22] = "";
        dsData = fun.consultarDatos("spInseFunExterno", objparam, Page, (String[])Session["constrring"]);
        Session["grabohuella"] = null;
        Session["capturar"] = null;
        Session["verificar"] = null;
        Session["Imagename"] = null;
        Response.Redirect("frmvisitanteexternoAdmin.aspx?mensajeRetornado='Guardado Con éxito'", false);
    }

    protected void btncamara_Click(object sender, ImageClickEventArgs e)
    {
        String pagina = "WebCam/CaptureImage.aspx?codigofun=" + Session["codigotempVisitante"].ToString();
        String newpag = "window.open('" + pagina + "', 'popup_window', 'width=950,height=500,left=50,top=50,resizable=yes');";
        ClientScript.RegisterStartupScript(GetType(), "pop", newpag, true);
    }

    protected void btnhuella_Click(object sender, ImageClickEventArgs e)
    {
        Session["tdoc"] = ddltipodoc.SelectedValue;
        Session["ndoc"] = txtndocu.Text;
        Session["nom1"] = txtnombre1.Text;
        Session["nom2"] = txtnombre2.Text;
        Session["ape1"] = txtapellido1.Text;
        Session["ape2"] = txtapellido2.Text;
        //Session["fecnaci"] = txtfecnaci.Text;
        Session["genero"] = ddlgenero.SelectedValue;
        Session["institu"] = txtentidad.Text;
        Session["depar"] = ddldepartamento.SelectedValue;
        Session["cargo"] = ddlcargo.SelectedValue;
        Session["funcio"] = ddltipofun.SelectedValue;
        Session["observa"] = txtobserva.Text;
        //String valores = "";
        //Crear el archivo .dat de los dedos si existe
        //for (int i = 1; i <= 10; i++)
        //{
        //    Array.Resize(ref objparam, 2);
        //    objparam[0] = Session["codigotempVisitante"].ToString();
        //    objparam[1] = i;
        //    dsData = fun.CrearArchivodat("spCargHuellaFun", objparam, Page, (String[])Session["constrring"]);
        //    if (dsData.Tables[0].Rows.Count == 0) valores = valores + "," + "0";
        //    if (dsData.Tables[0].Rows.Count > 0) valores = valores + "," + "1";
        //}
        //valores = valores.Substring(1);
        //String[] createtext = { valores };
        //File.WriteAllLines(rutahuell + "huellas.dat", createtext);
        Session["tabla"] = "Funcionario";
        Session["redirec"] = "10";
        Session["capturar"] = "S";
        Session["verificar"] = "N";
        String pagina = "../VisitaPPL/CapturaHuella.aspx";
        String newpag = "window.open('" + pagina + "', 'popup_window', 'width=950,height=600,left=50,top=50,resizable=yes');";
        ClientScript.RegisterStartupScript(GetType(), "pop", newpag, true);
    }

    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        Session["tdoc"] = null;
        Session["ndoc"] = null;
        Session["nom1"] = null;
        Session["nom2"] = null;
        Session["ape1"] = null;
        Session["ape2"] = null;
        //Session["fecnaci"] = null;
        Session["genero"] = null;
        Session["celular"] = null;
        Session["email"] = null;
        Session["institu"] = null;
        Session["depar"] = null;
        Session["cargo"] = null;
        Session["funcio"] = null;
        Session["observa"] = null;
        Session["Imagename"] = null;
        Session["grabohuella"] = null;
        Session["capturar"] = null;
        Session["verificar"] = null;
        Response.Redirect("frmvisitanteexternoAdmin.aspx");
    }
    private bool AccesoInternet()
    {
        try
        {
            System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry("www.bsg.gob.ec/sw/RC/BSGSW01_Consultar_Cedula?wsdl");
            lblregc2.Visible = true;
            return true;
        }
        catch (Exception es)
        {
            lblregc1.Visible = true;
            return false;
        }

    }
    protected void ddltipodoc_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtndocu.Text = "";
        if (ddltipodoc.SelectedValue == "C")
        {
            txtndocu_FilteredTextBoxExtender.FilterMode = AjaxControlToolkit.FilterModes.ValidChars;
            txtndocu_FilteredTextBoxExtender.InvalidChars = ".-";
            txtndocu_FilteredTextBoxExtender.FilterType = AjaxControlToolkit.FilterTypes.Numbers;
        }
        else
        {
            txtndocu_FilteredTextBoxExtender.FilterMode = AjaxControlToolkit.FilterModes.InvalidChars;
            txtndocu_FilteredTextBoxExtender.InvalidChars = ".-*/{{}}[[]]\\";
            txtndocu_FilteredTextBoxExtender.FilterType = AjaxControlToolkit.FilterTypes.Custom;
        }
    }
    #endregion
    #region Funciones y Procedimientos
    protected void funCargaDatosRegCivil()
    {

        //LLAMAR A LA FUNCION PARA CONSULTAR DATOS
        String Datos = fun.DatosBSG_RegistroCivil("0801693813", "https://www.bsg.gob.ec/sw/RC/BSGSW01_Consultar_Cedula?wsdl", "testroot", "Sti1DigS21", txtndocu.Text);

        mis_datos = Datos.Split('|');
        string nombrecompleto = mis_datos[0].ToString();

        if (mis_datos[1].ToString() == "HOMBRE")
        {
            ddlgenero.SelectedValue = "M";
        }
        else
        {
            ddlgenero.SelectedValue = "F";
        }


        txtnombretemp.Text = nombrecompleto;

        txtnombretemp.Visible = true;
        txtnombretemp.Enabled = false;
        ddlgenero.Enabled = false;
        Button1.Enabled = true;
        Button1.Text = "Buscar Registro Civil";


    }

    #endregion
    protected void Button1_Click(object sender, EventArgs e)
    {

        funCargaDatosRegCivil();

    }

}