using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Funcionario_frmfuncionaingreNew : System.Web.UI.Page
{
    #region Variables
    DataSet dsData = new DataSet();
    Object[] objparam = new Object[1];
    Funciones fun = new Funciones();
    String strObt = "";
    String foto = "";
    //String rutahuell = "C:/Temp/";
    String[] mis_datos;
    #endregion

    #region Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack)
            {
                if (Page.Request.Params["__EVENTTARGET"] == "btnrefrescar_Click")
                {
                    RefrescarFoto(Request["codigofun"].ToString());
                }
            }
            txtndocu.Attributes.Add("onchange", "Validar_Cedula();");
            if (!IsPostBack)
            {

                if (Request["mensajeRetornado"] != null)
                {
                    SIFunBasicas.Basicas.PresentarMensaje(Page, "::VISITA PPL::", Request["MensajeRetornado"]);
                }

                lbltitulo.Text = "ingreso de funcionario interno";

                //TRAER PARAMETRO PARA SABER SI SE MODIFICAN LOS DATOS DEL FUNCIONARIO

                txtfechainicio.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtfechafin.Text = DateTime.Now.ToString("dd/MM/yyyy");

                Array.Resize(ref objparam, 2);
                objparam[0] = 0;
                objparam[1] = "4";
                fun.cargarCombos(ddltipodoc, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
                ddltipodoc.Items.RemoveAt(0);

                objparam[0] = 0;
                objparam[1] = "17";
                fun.cargarCombos(ddlgenero, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
                ddlgenero.Items.RemoveAt(0);

                Array.Resize(ref objparam, 2);
                objparam[0] = "43";
                objparam[1] = "128";
                dsData = fun.consultarDatos("spCarParamDetaTotal", objparam, Page, (String[])Session["constrring"]);
                if (dsData.Tables[0].Rows[0][0].ToString() == "SI")
                {
                    ddltipodoc.Enabled = true;
                    txtndocu.Enabled = true;
                    txtnombre1.Enabled = true;
                    txtnombre2.Enabled = true;
                    txtapellido1.Enabled = true;
                    txtapellido2.Enabled = true;
                    //txtfecnaci.Enabled = true;
                    ddlgenero.Enabled = true;
                    txtcelular.Enabled = true;
                    txtemail.Enabled = true;
                    ddllugar.Enabled = true;
                    ddldepartamento.Enabled = true;
                    ddlcargo.Enabled = true;
                    ddltipofun.Enabled = true;
                }

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
                objparam[1] = "28";
                fun.cargarCombos(ddltipofun, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
                ddltipofun.Items.RemoveAt(0);

                objparam[0] = 0;
                objparam[1] = "33";
                fun.cargarCombos(ddllugar, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
                ddllugar.Items.RemoveAt(0);

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

                txtobserva.Text = Session["observa"].ToString();
                txtfechafin.Text = Session["fechahasta"].ToString();
                txtfechainicio.Text = Session["fechadesde"].ToString();
            }
        }
        catch (Exception ex)
        {
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", ex.Message);
        }
    }
    #endregion

    #region Funciones y Procedimientos
    protected void funCargaMantenimiento(String strCodigoFun)
    {
        txtcodigo.Text = strCodigoFun;
        String rutasrv = Server.MapPath(Session["rutafoto"].ToString());
        Array.Resize(ref objparam, 1);
        objparam[0] = strCodigoFun;
        dsData = fun.consultarDatos("spCarDatosFun", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0)
        {
            ddltipodoc.SelectedValue = dsData.Tables[0].Rows[0][0].ToString();
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
            txtndocu.Text = dsData.Tables[0].Rows[0][1].ToString();
            txtnombre1.Text = dsData.Tables[0].Rows[0][2].ToString();
            txtnombre2.Text = dsData.Tables[0].Rows[0][3].ToString();
            txtapellido1.Text = dsData.Tables[0].Rows[0][4].ToString();
            txtapellido2.Text = dsData.Tables[0].Rows[0][5].ToString();
            ddlgenero.SelectedValue = dsData.Tables[0].Rows[0][7].ToString();
            txtcelular.Text = dsData.Tables[0].Rows[0][8].ToString();
            txtemail.Text = dsData.Tables[0].Rows[0][9].ToString();
            ddldepartamento.SelectedValue = dsData.Tables[0].Rows[0][10].ToString();
            ddlcargo.SelectedValue = dsData.Tables[0].Rows[0][11].ToString();
            ddltipofun.SelectedValue = dsData.Tables[0].Rows[0][12].ToString();
            ddllugar.SelectedValue = dsData.Tables[0].Rows[0][13].ToString();
            Session["observacion"] = dsData.Tables[0].Rows[0][14].ToString();
            foto = dsData.Tables[0].Rows[0][15].ToString();
            Session["ValorEntidad"] = dsData.Tables[0].Rows[0][17].ToString();
            Session["fotoanterior"] = foto;
            Session["numdocuante"] = txtndocu.Text;
            Session["autorizar"] = dsData.Tables[0].Rows[0][19].ToString();

            Array.Resize(ref objparam, 2);
            objparam[0] = ddltipofun.SelectedValue == "N" ? "95" : "96";
            objparam[1] = "35";
            dsData = fun.consultarDatos("spCargaTotalParaDeta", objparam, Page, (String[])Session["constrring"]);
            Session["capturahuella"] = dsData.Tables[0].Rows[0][0].ToString();

        }
        if (foto == "")
        {
            imgfoto.ImageUrl = "~/Images/sinfoto.jpg";
        }
        else
        {
            if (File.Exists(rutasrv + foto))
            {
                imgfoto.ImageUrl = Session["rutafoto"].ToString() + foto;
                imgfoto.Width = 300;
                imgfoto.Height = 300;
            }
            else imgfoto.ImageUrl = "~/Images/sinfoto.jpg";
        }
    }

    private void RefrescarFoto(string strCodigoFun)
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
        String strPerfil = "", strAbierto = "", strRestringido = "", strEliminar = "";
        if (Session["grabohuella"] == null) Session["grabohuella"] = "N";
        if (Session["Imagename"] != null) Session["fotoanterior"] = Session["Imagename"].ToString();

        if (txtfechainicio.Text != "" || txtfechafin.Text != "")
        {
            if (Convert.ToDateTime(txtfechainicio.Text) > Convert.ToDateTime(txtfechafin.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('La Fecha de Ingreso no puede ser mayor a la Fecha de Salida');", true);
                return;
            }

            if (Convert.ToDateTime(txtfechafin.Text + " 23:59:59") < DateTime.Now)//se suma la hora porque se compara tambien las horas
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('La Fecha de Salida no puede ser menor a la Fecha Actual');", true);
                return;
            }
        }

        //CARGAR QUE PERFIL TIENE EL FUNCIONARIO
        Array.Resize(ref objparam, 2);
        objparam[0] = ddltipofun.SelectedValue;
        objparam[1] = "34";
        dsData = fun.consultarDatos("spCargParame", objparam, Page, (String[])Session["constrring"]);
        Session["perfilfun"] = dsData.Tables[0].Rows[0][0].ToString();

        //TREAR LOS DATOS DEL PERFIL
        Array.Resize(ref objparam, 1);
        objparam[0] = Session["perfilfun"].ToString();
        dsData = fun.consultarDatos("spCarPerfFuncio", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0)
        {
            strPerfil = dsData.Tables[0].Rows[0][0].ToString();
            strAbierto = dsData.Tables[0].Rows[0][1].ToString();
            strRestringido = dsData.Tables[0].Rows[0][2].ToString();
            strEliminar = dsData.Tables[0].Rows[0][3].ToString();
        }

        //SI ES FUNCIONARIO MAESTRO INGRESAR DE UNA VEZ
        if (ddltipofun.SelectedValue == "M")
        {
            Array.Resize(ref objparam, 11);
            objparam[0] = txtcodigo.Text;
            objparam[1] = txtobserva.Text.ToUpper();
            objparam[2] = txtfechainicio.Text;
            objparam[3] = txtfechafin.Text;
            objparam[4] = strPerfil;
            objparam[5] = strAbierto;
            objparam[6] = strRestringido;
            objparam[7] = strEliminar;
            objparam[8] = Session["usuCodigo"].ToString();
            objparam[9] = Session["MachineName"].ToString();
            objparam[10] = fun.SecuencialSiguiente("121", (String[])Session["constrring"]);
            dsData = fun.consultarDatos("spInserVisitaFunInter", objparam, Page, (String[])Session["constrring"]);
            ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='frmfuncionaingreAdmin.aspx?mensajeRetornado=Ingreso Correcto al CRS';window.close();", true);
        }
        else
        {
            if (Session["capturahuella"].ToString() == "SI")
            {
                if (Session["grabohuella"].ToString() == "N")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Verifique la(s) Huella(s) del funcionario');", true);
                    return;
                }
            }

            if (Session["capturaFoto"].ToString() == "SI")
            {
                if (Session["fotoanterior"].ToString() == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Capture la foto del visitante');", true);
                    return;
                }
            }
            //PREGUNTAR SI YA EXISTE EL DOCUMENTO SI SE CAMBIO EL DATO
            if (Session["numdocuante"].ToString() != txtndocu.Text)
            {
                Array.Resize(ref objparam, 1);
                objparam[0] = txtndocu.Text;
                dsData = fun.consultarDatos("spCargFuncioDocu", objparam, Page, (String[])Session["constrring"]);
                if (dsData.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Número de Documento ya Existe');", true);
                    return;
                }
            }
            Array.Resize(ref objparam, 23);
            objparam[0] = txtcodigo.Text;
            objparam[1] = ddltipodoc.SelectedValue;
            objparam[2] = txtndocu.Text;
            objparam[3] = txtnombre1.Text.ToUpper();
            objparam[4] = txtnombre2.Text.ToUpper();
            objparam[5] = txtapellido1.Text.ToUpper();
            objparam[6] = txtapellido2.Text.ToUpper();
            objparam[7] = DateTime.Now;//txtfecnaci.Text;
            objparam[8] = ddlgenero.SelectedValue;
            objparam[9] = txtcelular.Text;
            objparam[10] = txtemail.Text;
            objparam[11] = Session["ValorEntidad"].ToString();
            objparam[12] = ddldepartamento.SelectedValue;
            objparam[13] = ddlcargo.SelectedValue;
            objparam[14] = ddltipofun.SelectedValue;
            objparam[15] = ddllugar.SelectedValue;
            objparam[16] = Session["observacion"].ToString();
            objparam[17] = true;
            objparam[18] = Session["fotoanterior"].ToString();
            objparam[19] = Session["grabohuella"].ToString();
            objparam[20] = Session["usuCodigo"].ToString();
            objparam[21] = Session["MachineName"].ToString();
            objparam[22] = Session["autorizar"].ToString();
            dsData = fun.consultarDatos("spModiFunciInterno", objparam, Page, (String[])Session["constrring"]);

            Array.Resize(ref objparam, 11);
            objparam[0] = txtcodigo.Text;
            objparam[1] = txtobserva.Text.ToUpper();
            objparam[2] = txtfechainicio.Text;
            objparam[3] = txtfechafin.Text;
            objparam[4] = strPerfil;
            objparam[5] = strAbierto;
            objparam[6] = strRestringido;
            objparam[7] = strEliminar;
            objparam[8] = Session["usuCodigo"].ToString();
            objparam[9] = Session["MachineName"].ToString();
            objparam[10] = fun.SecuencialSiguiente("121", (String[])Session["constrring"]);
            dsData = fun.consultarDatos("spInserVisitaFunInter", objparam, Page, (String[])Session["constrring"]);

            Session["observa"] = null;
            Session["fechadesde"] = null;
            Session["fechahasta"] = null;
            Session["ValorEntidad"] = null;
            Session["grabohuella"] = null;
            Session["fotoanterior"] = null;
            Session["numdocuante"] = null;
            Session["autorizar"] = null;
            Session["perfilfun"] = null;
            Session["Imagename"] = null;
            ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='frmfuncionaingreAdmin.aspx?mensajeRetornado=Ingreso Correcto al CRS';window.close();", true);
        }
    }

    protected void btncamara_Click(object sender, ImageClickEventArgs e)
    {
        String pagina = "WebCam/CaptureImage.aspx?codigofun=" + txtcodigo.Text;
        String newpag = "window.open('" + pagina + "', 'popup_window', 'width=950,height=500,left=50,top=50,resizable=yes');";
        ClientScript.RegisterStartupScript(GetType(), "pop", newpag, true);
    }

    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        Session["ValorEntidad"] = null;
        Session["grabohuella"] = null;
        Session["fotoanterior"] = null;
        Session["numdocuante"] = null;
        Session["autorizar"] = null;
        Session["perfilfun"] = null;
        Session["Imagename"] = null;
        Session["observa"] = null;
        Session["fechadesde"] = null;
        Session["fechahasta"] = null;
        ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='frmfuncionaingreAdmin.aspx';window.close();", true);
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

    protected void btnhuella_Click(object sender, ImageClickEventArgs e)
    {
        Session["capturar"] = "S";
        Session["verificar"] = "N";
        Array.Resize(ref objparam, 1);
        objparam[0] = txtcodigo.Text;
        dsData = fun.consultarDatos("spCargFuncioHuella", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0)
        {
            //Session["capturar"] = "N";
            //Session["verificar"] = "S";
        }
        Session["tabla"] = "Funcionario";
        Session["redirec"] = "8";
        Session["codigotempVisitante"] = txtcodigo.Text;

        Session["fechadesde"] = txtfechainicio.Text;
        Session["fechahasta"] = txtfechafin.Text;
        Session["observa"] = txtobserva.Text;
        Response.Redirect("../VisitaPPL/CapturaHuella.aspx");
    }
    #endregion
    #region Funciones y Procedimientos
    protected void funCargaDatosRegCivil()
    {

        //LLAMAR A LA FUNCION PARA CONSULTAR DATOS
        String Datos = fun.DatosBSG_RegistroCivil("0801693813", "https://www.bsg.gob.ec/sw/RC/BSGSW01_Consultar_Cedula?wsdl", "testroot", "Sti1DigS21", txtndocu.Text);

        mis_datos = Datos.Split('|');
        string nombrecompleto = mis_datos[0].ToString();
        nombrecompleto = "Juan Jose Leon Mera";
        Char delimiter = ' ';
        String[] substrings = nombrecompleto.Split(delimiter);
        int words = nombrecompleto.Split(delimiter).Length;
        if (mis_datos[1].ToString() == "HOMBRE")
        {
            ddlgenero.SelectedValue = "M";
        }
        else ddlgenero.SelectedValue = "F";
        if (words == 4)
        {
            txtnombre1.Text = substrings[0];
            txtnombre2.Text = substrings[1];
            txtapellido1.Text = substrings[2];
            txtapellido2.Text= substrings[4];
        }
        else if (words == 3)
        {
            txtnombre1.Text = substrings[0];
            txtnombre2.Text = substrings[1];
            txtapellido1.Text = substrings[2];
        }
        else if (words == 2)
        {
            txtnombre1.Text = substrings[0];
            txtapellido1.Text = substrings[1];
        }
        txtnombre1.Enabled = false;
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