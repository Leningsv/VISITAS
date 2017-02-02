using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Funcionario_frmvistingtotNew : System.Web.UI.Page
{
    #region Variables
    DataSet dsData = new DataSet();
    Object[] objparam = new Object[1];
    Funciones fun = new Funciones();
    String strObt = "";
    String foto = "";
    String[] mis_datos;
    //String rutahuell = "C:/Temp/";
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
                txtfechadesde.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtfechahasta.Text = DateTime.Now.ToString("dd/MM/yyyy");
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

                //OBETENER EL VALOR DE LA INSTITUCION
                Array.Resize(ref objparam, 1);
                objparam[0] = "90";
                dsData = fun.consultarDatos("spCargValorDetalle", objparam, Page, (String[])Session["constrring"]);
                Session["ValorEntidad"] = dsData.Tables[0].Rows[0][0].ToString();

                Array.Resize(ref objparam, 1);
                objparam[0] = "";
                fun.cargarCombos(ddldepartamento, "spCarDEpartament", objparam, Page, (String[])Session["constrring"]);
                ddldepartamento.Items.RemoveAt(0);

                Array.Resize(ref objparam, 1);
                objparam[0] = 0;
                fun.cargarCombos(ddlfuncionario, "spCarFuncioInterno", objparam, Page, (String[])Session["constrring"]);
                ddlfuncionario.Items.RemoveAt(0);

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

                Session["capturaFoto"] = "NO";
                Array.Resize(ref objparam, 1);
                objparam[0] = "123";
                dsData = fun.consultarDatos("spSolcitaDocu", objparam, Page, (String[])Session["constrring"]);
                if (dsData.Tables[0].Rows[0][0].ToString() == "SI") Session["captuarFoto"] = "SI";
                Session["capturaFoto"] = dsData.Tables[0].Rows[0][0].ToString();

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
                    txtentidad.Text = Session["entidad"].ToString();
                    txtfechadesde.Text = Session["fechadesde"].ToString();
                    txtfechahasta.Text = Session["fechahasta"].ToString();
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
    protected void btningresar_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["grabohuella"] == null) Session["grabohuella"] = "N";
        //if (!fun.IsFechaNacimiento(txtfecnaci.Text))
        //{
        //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Fecha de Nacimiento Incorrecta');", true);
        //    return;
        //}

        Array.Resize(ref objparam, 1);
        objparam[0] = txtndocu.Text;
        dsData = fun.consultarDatos("spCargFuncioDocu", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Número de Documento ya Existe');", true);
            return;
        }

        if (Session["grabohuella"].ToString() == "N")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Capture la(s) Huella(s) del visitante');", true);
            return;
        }

        if (Session["capturaFoto"].ToString() == "SI")
        {
            if (Session["Imagename"] == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Capture la foto del visitante');", true);
                return;
            }
        }

        if (txtfechadesde.Text != "" || txtfechahasta.Text != "")
        {
            if (Convert.ToDateTime(txtfechadesde.Text) > Convert.ToDateTime(txtfechahasta.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('La Fecha de Ingreso no puede ser mayor a la Fecha de Salida');", true);
                return;
            }

            if (Convert.ToDateTime(txtfechahasta.Text + " 23:59:59") < DateTime.Now)//se suma la hora porque se compara tambien las horas
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('La Fecha de Salida no puede ser menor a la Fecha Actual');", true);
                return;
            }
        }
        Array.Resize(ref objparam, 19);
        objparam[0] = Session["codigotempVisitante"].ToString();
        objparam[1] = ddltipodoc.SelectedValue;
        objparam[2] = txtndocu.Text;
        objparam[3] = txtnombre1.Text.ToUpper();
        objparam[4] = txtnombre2.Text.ToUpper();
        objparam[5] = txtapellido1.Text.ToUpper();
        objparam[6] = txtapellido2.Text.ToUpper();
        objparam[7] = DateTime.Now;//txtfecnaci.Text;
        objparam[8] = ddlgenero.SelectedValue;
        objparam[9] = txtentidad.Text.ToUpper();
        objparam[10] = ddldepartamento.SelectedValue;
        objparam[11] = ddlcargo.SelectedValue;
        objparam[12] = ddltipofun.SelectedValue;
        objparam[13] = "";
        objparam[14] = true;
        objparam[15] = Session["Imagename"] == null ? "" : Session["Imagename"].ToString();
        objparam[16] = Session["grabohuella"].ToString();
        objparam[17] = Session["usuCodigo"].ToString();
        objparam[18] = Session["MachineName"].ToString();
        dsData = fun.consultarDatos("spInserNuevoVisitanteExt", objparam, Page, (String[])Session["constrring"]);
        //GRABAR DATOS EN TEMPORAL
        Array.Resize(ref objparam, 8);
        objparam[0] = ddlfuncionario.SelectedValue;
        objparam[1] = Session["codigotempVisitante"].ToString();
        objparam[2] = fun.SecuencialSiguiente("121", (String[])Session["constrring"]);
        objparam[3] = txtfechadesde.Text;
        objparam[4] = txtfechahasta.Text;
        objparam[5] = txtobserva.Text.ToUpper();
        objparam[6] = Session["usuCodigo"].ToString();
        objparam[7] = Session["MachineName"].ToString();
        dsData = fun.consultarDatos("spInserVisTempExt", objparam, Page, (String[])Session["constrring"]);

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
        Session["entidad"] = null;
        Session["depar"] = null;
        Session["cargo"] = null;
        Session["funcio"] = null;
        Session["observa"] = null;
        Session["fechadesde"] = null;
        Session["fechahasta"] = null;
        Session["Imagename"] = null;
        Session["grabohuella"] = null;
        Session["capturar"] = null;
        Session["verificar"] = null;
        ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='frmvisitanteingreAdmin.aspx?mensajeRetornado=Ingreso Correcto al CRS';window.close();", true);
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
        Session["genero"] = ddlgenero.SelectedValue;
        Session["entidad"] = txtentidad.Text;
        Session["depar"] = ddldepartamento.SelectedValue;
        Session["cargo"] = ddlcargo.SelectedValue;
        Session["funcio"] = ddltipofun.SelectedValue;
        Session["fechadesde"] = txtfechadesde.Text;
        Session["fechahasta"] = txtfechahasta.Text;
        Session["observa"] = txtobserva.Text;
        //String valores = "";
        ////Crear el archivo .dat de los dedos si existe
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
        Session["redirec"] = "12";
        Session["capturar"] = "S";
        Session["verificar"] = "N";
        //String pagina = "../VisitaPPL/CapturaHuella.aspx";
        //String newpag = "window.open('" + pagina + "', 'popup_window', 'width=950,height=600,left=50,top=50,resizable=yes');";
        //ClientScript.RegisterStartupScript(GetType(), "pop", newpag, true);
        Response.Redirect("../VisitaPPL/CapturaHuella.aspx");
    }

    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["Imagename"] != null)
        {
            String rutasrv = Server.MapPath(Session["rutafoto"].ToString());
            if (File.Exists(rutasrv + Session["Imagename"].ToString()))
            {
                File.Delete(rutasrv + Session["Imagename"].ToString());
            }
        }
        Session["Imagename"] = null;
        Session["grabohuella"] = null;
        Session["capturar"] = null;
        Session["verificar"] = null;
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
        Session["entidad"] = null;
        Session["depar"] = null;
        Session["cargo"] = null;
        Session["funcio"] = null;
        Session["observa"] = null;
        Session["fechadesde"] = null;
        Session["fechahasta"] = null;
        ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.close();", true);
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