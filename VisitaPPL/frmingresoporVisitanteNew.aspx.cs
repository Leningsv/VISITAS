using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class VisitaPPL_frmingresoporVisitanteNew : System.Web.UI.Page
{
    #region Variables
    DataSet dsData = new DataSet();
    DataSet dsData1 = new DataSet();
    Object[] objparam = new Object[1];
    Funciones fun = new Funciones();
    String strObt = "";
    String foto = "";
    String[] mis_datos;
    #endregion

    #region Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack)
            {
                String parametros = Request.Params.Get("__EVENTTARGET");
                if (Page.Request.Params["__EVENTTARGET"] == "btnrefrescar_Click")
                {
                    RefrescarFoto();
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
                //Session["grabohuella"] = null;
                txttipovisita.Text = Session["nombrevisita"].ToString();
                //TRAER EL NUMERO DEL SECUENCIAL SI NO TIENE YA CAPTURADO
                lbltitulo.Text = "Ingreso de visita al ppl";

                if (Session["tipovisConyugue"].ToString() == "M")
                {
                    if (Session["ingmenor"].ToString() == "SI")
                    {
                        tblmenor.Visible = true;
                        lblmsj2.Text = "Registrar Menores de Edad Hasta " + Session["Edad"].ToString() + " año(s)";
                        lblmsj2.Visible = true;                        
                    }
                }

                Session["RetenerDocu"] = "NO";
                Array.Resize(ref objparam, 1);
                objparam[0] = "63";
                dsData = fun.consultarDatos("spSolcitaDocu", objparam, Page, (String[])Session["constrring"]);
                if (dsData.Tables[0].Rows[0][0].ToString() == "SI")
                {
                    Session["RetenerDocu"] = "SI";
                    lblmsj2.Text = "Retener el documento físico del visitante";
                    lblmsj2.Visible = true;
                }

                Session["capturaFoto"] = "NO";
                Array.Resize(ref objparam, 1);
                objparam[0] = "123";
                dsData = fun.consultarDatos("spSolcitaDocu", objparam, Page, (String[])Session["constrring"]);
                if (dsData.Tables[0].Rows[0][0].ToString() == "SI") Session["capturaFoto"] = "SI";

                Session["capturahuella"] = "NO";

                Array.Resize(ref objparam, 2);
                objparam[0] = 0;
                objparam[1] = "4";
                fun.cargarCombos(ddltipodoc, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
                ddltipodoc.Items.RemoveAt(0);

                Array.Resize(ref objparam, 2);
                objparam[0] = 0;
                objparam[1] = "17";
                fun.cargarCombos(ddlgenero, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
                ddlgenero.Items.RemoveAt(0);

                //CARGAR LOS PARENTESO SEGUN EL TIPO DE VISITA
                objparam[0] = "15";
                objparam[1] = Session["tipovisConyugue"].ToString();
                fun.cargarCombos(ddlparentesco, "spCargParenPerfil", objparam, Page, (String[])Session["constrring"]);
                ddlparentesco.Items.RemoveAt(0);

                objparam[0] = 0;
                objparam[1] = "17";
                fun.cargarCombos(ddlgeneroedad, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
                ddlgeneroedad.Items.RemoveAt(0);

                objparam[0] = 0;
                objparam[1] = "8";
                strObt = fun.ObtenerRutas(Page, "spCargarParametros", objparam, (String[])Session["constrring"]);
                String[] session = strObt.Split('|');
                Session["rutafoto"] = session[0].ToString();
                Session["rutahuella"] = session[1].ToString();

                Array.Resize(ref objparam, 1);
                objparam[0] = Session["codpplnew"].ToString();
                dsData = fun.consultarDatos("spCargaPPLIndiVisita", objparam, Page, (String[])Session["constrring"]);
                if (dsData.Tables[0].Rows.Count > 0)
                {
                    txtnombres.Text = dsData.Tables[0].Rows[0][0].ToString() + ' ' + dsData.Tables[0].Rows[0][1].ToString();
                    txtapellidos.Text = dsData.Tables[0].Rows[0][2].ToString() + ' ' + dsData.Tables[0].Rows[0][3].ToString();
                    txtetapa.Text = dsData.Tables[0].Rows[0][4].ToString();
                    txtpabellon.Text = dsData.Tables[0].Rows[0][5].ToString();

                    Array.Resize(ref objparam, 2);
                    objparam[0] = dsData.Tables[0].Rows[0][6].ToString();
                    objparam[1] = "44";
                    dsData1 = fun.consultarDatos("spCarParDetaNombre", objparam, Page, (String[])Session["constrring"]);
                    if (dsData1.Tables[0].Rows[0][4].ToString() == "SI") Session["capturahuella"] = "SI";

                }

                if (Session["codigotempVisitante"] != null)
                {
                    ddltipodoc.SelectedValue = Session["tipodoc"].ToString();
                    txtndocu.Text = Session["ndoc"].ToString();
                    txtnombre1.Text = Session["nom1"].ToString();
                    txtnombre2.Text = Session["nom2"].ToString();
                    txtapellido1.Text = Session["ape1"].ToString();
                    txtapellido2.Text = Session["ape2"].ToString();
                    ddlparentesco.SelectedValue = Session["parentesco"].ToString();
                    txtobserva.Text = Session["observa"].ToString();
                    txtdireccion.Text = Session["direcc"].ToString();
                    txttelefono.Text = Session["fono1"].ToString();

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
                    objparam[0] = "2";
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

    #region Botones y Eventos
    protected void btngrabar_Click(object sender, ImageClickEventArgs e)
    {
        String strPerfil = "", strAbierto = "", strRestringido = "", strEliminar = "", strTurnoCodigo = "", strHoraDesde = "",
    strHoraHasta = "";
        try
        {
            if (Session["grabohuella"] == null) Session["grabohuella"] = "N";
            Array.Resize(ref objparam, 1);
            objparam[0] = txtndocu.Text;
            dsData = fun.consultarDatos("spVerificaCed", objparam, Page, (String[])Session["constrring"]);
            if (dsData.Tables[0].Rows.Count > 0)
            {
                txtndocu.Text = "";
                txtnombre1.Text = "";
                txtdireccion.Text = "";
                txttelefono.Text = "";
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Número de Documento ya se encuentra registrado');", true);
                return;
            }

            if (Session["capturahuella"].ToString() == "SI")
            {
                if (Session["grabohuella"].ToString() == "N")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Capture las Huellas del visitante');", true);
                    return;
                }
            }

            if (Session["capturaFoto"].ToString() == "SI")
            {
                if (Session["Imagename"] == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Capture la foto del visitante');", true);
                    return;
                }
            }

            String graMedad = "NO";
            if (chkmenor.Checked == true)
            {
                if (txtedad.Text == "")
                {
                    lblmsj3.Visible = true;
                    return;
                }
                else lblmsj3.Visible = false;
                if (txtnomedad.Text == "")
                {
                    lblmsj4.Visible = true;
                    return;
                }
                else lblmsj4.Visible = false;

                if (int.Parse(txtedad.Text) > int.Parse(Session["Edad"].ToString()))
                {
                    String mensaje = "Edad Maxima del Menor es de: " + Session["Edad"].ToString() + " año(s)";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + mensaje + "')", true);
                    return;
                }
                graMedad = "SI";
            }

            //OBTNER PERFIL DEL PPL SEGUN LA ETAPA Y TRER LOS VALORES DEL ACCESO DEL PERFIL
            Array.Resize(ref objparam, 1);
            objparam[0] = Session["etapappl"].ToString();
            dsData = fun.consultarDatos("spCarPerfiAcceso", objparam, Page, (String[])Session["constrring"]);
            if (dsData.Tables[0].Rows.Count > 0)
            {
                strPerfil = dsData.Tables[0].Rows[0][0].ToString();
                strAbierto = dsData.Tables[0].Rows[0][1].ToString();
                strRestringido = dsData.Tables[0].Rows[0][2].ToString();
                strEliminar = dsData.Tables[0].Rows[0][3].ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('No existe ningún perfil activo para el visitante');", true);
                return;
            }
            //OBTENER HORARIOS TURNOS Y LOS VALORES DEL PERFIL
            Array.Resize(ref objparam, 2);
            objparam[0] = Session["etapappl"].ToString();
            objparam[1] = Session["tipovisConyugue"].ToString();
            dsData = fun.consultarDatos("spCarTurnoPlanifi", objparam, Page, (String[])Session["constrring"]);
            if (dsData.Tables[0].Rows.Count > 0)
            {
                strTurnoCodigo = dsData.Tables[0].Rows[0][0].ToString();
                strHoraDesde = dsData.Tables[0].Rows[0][1].ToString();
                strHoraHasta = dsData.Tables[0].Rows[0][2].ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('No existe ningún turno activo para el visitante');", true);
                return;
            }
            //Converti las horas en datetime + la fecha
            String strFechadesde = DateTime.Now.ToString("dd/MM/yyyy") + " " + strHoraDesde;
            String strFechahasta = DateTime.Now.ToString("dd/MM/yyyy") + " " + strHoraHasta;

            DateTime dtFechadesde = Convert.ToDateTime(strFechadesde);
            DateTime dtFechahasta = Convert.ToDateTime(strFechahasta);

            String strCodigoEve = "";

            if (Session["tipovisConyugue"].ToString() == "M") strCodigoEve = "118";
            if (Session["tipovisConyugue"].ToString() == "C") strCodigoEve = "119";
            if (Session["tipovisConyugue"].ToString() == "L") strCodigoEve = "120";

            Session["tdoc"] = null;
            Session["ndoc"] = null;
            Session["nom1"] = null;
            Session["nom2"] = null;
            Session["ape1"] = null;
            Session["ape2"] = null;
            Session["fecnaci"] = null;
            Session["parentesco"] = null;
            Session["observa"] = null;
            Session["grabohuella"] = null;

            Array.Resize(ref objparam, 31);
            objparam[0] = Session["codpplnew"].ToString();
            objparam[1] = Session["codigotempVisitante"].ToString();
            objparam[2] = Session["tipovisConyugue"].ToString();
            objparam[3] = ddltipodoc.SelectedValue;
            objparam[4] = txtndocu.Text;
            objparam[5] = txtnomedad.Text.ToUpper();
            objparam[6] = txtedad.Text;
            objparam[7] = ddlgeneroedad.SelectedValue;
            objparam[8] = txtobsermenor.Text.ToUpper();
            objparam[9] = graMedad;
            objparam[10] = txtapellido1.Text.ToUpper();
            objparam[11] = txtapellido2.Text.ToUpper();
            objparam[12] = txtnombre1.Text.ToUpper();
            objparam[13] = txtnombre2.Text.ToUpper();
            objparam[14] = DateTime.Now;
            objparam[15] = ddlparentesco.SelectedValue;
            objparam[16] = txtobserva.Text.ToUpper();
            objparam[17] = Session["RetenerDocu"].ToString();
            objparam[18] = strPerfil;
            objparam[19] = strAbierto;
            objparam[20] = strRestringido;
            objparam[21] = strEliminar;
            objparam[22] = dtFechadesde;
            objparam[23] = dtFechahasta;
            objparam[24] = Session["usuCodigo"].ToString();
            objparam[25] = Session["MachineName"].ToString();
            objparam[26] = fun.SecuencialSiguiente(strCodigoEve, (String[])Session["constrring"]);
            objparam[27] = Session["Imagename"] == null ? "" : Session["Imagename"].ToString();
            objparam[28] = txtdireccion.Text.ToUpper();
            objparam[29] = txttelefono.Text;
            objparam[30] = ddlgenero.SelectedValue;
            dsData = fun.consultarDatos("spInserEventoOnline", objparam, Page, (String[])Session["constrring"]);
            Session["Imagename"] = null;
            if (dsData.Tables[0].Rows[0][0].ToString() == "Exito")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='frmvistpplNew.aspx?pplCodigo=" + Session["codpplnew"].ToString() + "&mensajeRetornado=Ingreso correcto al CRS" + "';window.close();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='frmvistpplNew.aspx?pplCodigo=" + Session["codpplnew"].ToString() + "&mensajeRetornado=Ocurrió un Error en la Base de Datos" + "';window.close();", true);
            }
        }
        catch (Exception ex)
        {
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Grabar Datos", ex.Message);
        }
    }
    protected void chkmenor_CheckedChanged(object sender, EventArgs e)
    {
        txtedad.Text = "";
        txtnomedad.Text = "";
        txtobsermenor.Text = "";
        if (chkmenor.Checked == true)
        {
            txtedad.Enabled = true;
            txtnomedad.Enabled = true;
            txtobsermenor.Enabled = true;
            ddlgeneroedad.Enabled = true;
        }
        else
        {
            txtedad.Enabled = false;
            txtnomedad.Enabled = false;
            txtobsermenor.Enabled = false;
            ddlgeneroedad.Enabled = false;
        }
    }
    protected void btncamara_Click(object sender, ImageClickEventArgs e)
    {
        String pagina = "WebCam/CaptureImage.aspx?codigovisitante=" + Session["codigotempVisitante"].ToString();
        String newpag = "window.open('" + pagina + "', 'popup_window', 'width=950,height=500,left=50,top=50,resizable=yes');";
        ClientScript.RegisterStartupScript(GetType(), "pop", newpag, true);
    }
    protected void btnhuella_Click(object sender, ImageClickEventArgs e)
    {
        Session["tipodoc"] = ddltipodoc.SelectedValue;
        Session["ndoc"] = txtndocu.Text;
        Session["nom1"] = txtnombre1.Text;
        Session["nom2"] = txtnombre2.Text;
        Session["ape1"] = txtapellido1.Text;
        Session["ape2"] = txtapellido2.Text;
        Session["parentesco"] = ddlparentesco.SelectedValue;
        Session["observa"] = txtobserva.Text;
        Session["direcc"] = txtdireccion.Text;
        Session["fono1"] = txttelefono.Text;
        //Crear el archivo .dat de los dedos si existe
        //String valores = "";        
        //for (int i = 1; i <= 10; i++)
        //{
        //    Array.Resize(ref objparam, 2);
        //    objparam[0] = Session["codigotempVisitante"].ToString();
        //    objparam[1] = i;
        //    dsData = fun.CrearArchivodat("spCargHuellaVis", objparam, Page, (String[])Session["constrring"]);
        //    if (dsData.Tables[0].Rows.Count == 0) valores = valores + "," + "0";
        //    if (dsData.Tables[0].Rows.Count > 0) valores = valores + "," + "1";
        //}
        //valores = valores.Substring(1);
        //String[] createtext = { valores };
        //File.WriteAllLines(rutahuell + "huellas.dat", createtext);
        Session["tabla"] = "Visitante";
        Session["redirec"] = "1";
        Session["capturar"] = "S";
        Session["verificar"] = "N";
        //String pagina = "CapturaHuella.aspx";
        //String newpag = "window.open('" + pagina + "', 'popup_window', 'width=950,height=600,left=50,top=50,resizable=yes');";
        //ClientScript.RegisterStartupScript(GetType(), "pop", newpag, true);
        Response.Redirect("CapturaHuella.aspx");
    }
    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        Session["tdoc"] = null;
        Session["ndoc"] = null;
        Session["nom1"] = null;
        Session["nom2"] = null;
        Session["ape1"] = null;
        Session["ape2"] = null;
        Session["fecnaci"] = null;
        Session["parentesco"] = null;
        Session["observa"] = null;
        Session["direcc"] = null;
        Session["fono1"] = null;
        //ANTES DE SALIR BORRAR SI EXISTE UNA FOTO EN MEMORIA Y SI EXISTE UNA HUELLA CAPTURADA           
        if (Session["Imagename"] != null)
        {
            String rutasrv = Server.MapPath(Session["rutafoto"].ToString());
            if (File.Exists(rutasrv + Session["Imagename"].ToString()))
            {
                File.Delete(rutasrv + Session["Imagename"].ToString());
            }
        }
        Array.Resize(ref objparam, 1);
        objparam[0] = Session["codigotempVisitante"].ToString();
        dsData = fun.consultarDatos("spElimiHuellaVisitante", objparam, Page, (String[])Session["constrring"]);
        Session["codigotempVisitante"] = null;
        Session["Imagename"] = null;
        //Session["fotoanterior"] = null;
        ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='frmvistpplNew.aspx?pplCodigo=" + Session["codpplnew"].ToString() + "';window.close();", true);
    }

    private void RefrescarFoto()
    {
        String rutasrv = Server.MapPath(Session["rutafoto"].ToString());
        foto = Session["Imagename"] == null ? "" : Session["Imagename"].ToString();        
        if (File.Exists(rutasrv + foto))
        {
            imgfoto.ImageUrl = Session["rutafoto"].ToString() + foto;
            imgfoto.Width = 300;
            imgfoto.Height = 250;
        }
        else imgfoto.ImageUrl = "~/Images/sinfoto.jpg";
    }
    protected void ddltipodoc_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtndocu.Text = "";
        txtnombre1.Text = "";
        txtnombre1.Enabled = true;
        ddlgenero.Enabled = true;
        ddlgenero.SelectedValue = "M";
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
        txtnombre1.Text = mis_datos[0].ToString();
        if (mis_datos[1].ToString() == "MASCULINO")
        {
            ddlgenero.SelectedValue = "M";
        }
        else ddlgenero.SelectedValue = "F";
        txtdireccion.Text = mis_datos[2].ToString();
        txtnombre1.Enabled = false;
        ddlgenero.Enabled = false;
 
    }

    #endregion
}