using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Funcionario_frmvisitanteingresoNew : System.Web.UI.Page
{
    #region Variables
    DataSet dsData = new DataSet();
    Object[] objparam = new Object[1];
    Funciones fun = new Funciones();
    String strObt = "";
    String foto = "";
    //String rutahuell = "C:/Temp/";
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
                lbltitulo.Text = "Ingresar Nuevo visitante externo";
                txtfechadesde.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtfechahasta.Text = DateTime.Now.ToString("dd/MM/yyyy");

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

                Session["capturahuella"] = "SI";
                Array.Resize(ref objparam, 2);
                objparam[0] = "101";
                objparam[1] = "37";
                dsData = fun.consultarDatos("spCargaTotalParaDeta", objparam, Page, (String[])Session["constrring"]);
                Session["capturahuella"] = dsData.Tables[0].Rows[0][0].ToString();

                ddlfuncionario.SelectedValue= Session["rfuncionario"].ToString();
                txtfechadesde.Text = Session["rfechadesde"].ToString();
                txtfechahasta.Text = Session["rfechahasta"].ToString();
                txtobserva.Text = Session["robserva"].ToString();

                Array.Resize(ref objparam, 2);
                objparam[0] = "43";
                objparam[1] = "129";
                dsData = fun.consultarDatos("spCarParamDetaTotal", objparam, Page, (String[])Session["constrring"]);
                if (dsData.Tables[0].Rows[0][0].ToString() == "SI")
                {
                    ddltipodoc.Enabled = true;
                    txtndocu.Enabled = true;
                    txtnombre1.Enabled = true;
                    txtnombre2.Enabled = true;
                    txtapellido1.Enabled = true;
                    txtapellido2.Enabled = true;
                    ddlgenero.Enabled = true;
                    txtentidad.Enabled = true;
                    ddldepartamento.Enabled = true;
                    ddlcargo.Enabled = true;
                    ddltipofun.Enabled = true;
                }

                if (Request["mensajeRetornado"] != null)
                {
                    SIFunBasicas.Basicas.PresentarMensaje(Page, "::VISITA PPL::", Request["MensajeRetornado"]);
                }

                txtcodigo.Text = Request["codigofun"].ToString();
                funCargaMantenimiento(txtcodigo.Text);
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
        String rutasrv = Server.MapPath(Session["rutafoto"].ToString());
        Array.Resize(ref objparam, 1);
        objparam[0] = strCodigoFun;
        dsData = fun.consultarDatos("spCarDatosFunInter", objparam, Page, (String[])Session["constrring"]);
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
            //txtfecnaci.Text = dsData.Tables[0].Rows[0][6].ToString();
            ddlgenero.SelectedValue = dsData.Tables[0].Rows[0][7].ToString();
            ddldepartamento.SelectedValue = dsData.Tables[0].Rows[0][8].ToString();
            ddlcargo.SelectedValue = dsData.Tables[0].Rows[0][9].ToString();
            ddltipofun.SelectedValue = dsData.Tables[0].Rows[0][10].ToString();
            txtentidad.Text = dsData.Tables[0].Rows[0][14].ToString();
            foto = dsData.Tables[0].Rows[0][12].ToString();
            Session["fotoanterior"] = foto;
            Session["numdocante"] = txtndocu.Text;
        }

        if (File.Exists(rutasrv + foto))
        {
            imgfoto.ImageUrl = Session["rutafoto"].ToString() + foto;
            imgfoto.Width = 300;
            imgfoto.Height = 300;
        }
        else imgfoto.ImageUrl = "~/Images/sinfoto.jpg";
    }

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
        //Session["fototemporal"] = foto;
    }
    #endregion

    #region Botones y Eventos
    protected void btningresar_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["grabohuella"] == null) Session["grabohuella"] = "N";
        if (Session["Imagename"] != null) Session["fotoanterior"] = Session["Imagename"].ToString();
        //if (!fun.IsFechaNacimiento(txtfecnaci.Text))
        //{
        //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Fecha de Nacimiento Incorrecta');", true);
        //    return;
        //}

        if (Session["numdocante"].ToString() != txtndocu.Text)
        {
            Array.Resize(ref objparam, 1);
            objparam[0] = txtndocu.Text;
            dsData = fun.consultarDatos("spCargFuncioDocu", objparam, Page, (String[])Session["constrring"]);
            if (dsData.Tables[0].Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Número de Documento ya existe');", true);
                return;
            }
        }

        if (Session["capturahuella"].ToString() == "SI")
        {
            if (Session["grabohuella"].ToString() == "N")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Verifique la(s) Huella(s) del visitante');", true);
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
        objparam[0] = txtcodigo.Text;
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
        objparam[13] = txtobserva.Text.ToUpper();
        objparam[14] = true;
        objparam[15] = foto;
        objparam[16] = Session["grabohuella"].ToString();
        objparam[17] = Session["usuCodigo"].ToString();
        objparam[18] = Session["MachineName"].ToString();
        dsData = fun.consultarDatos("spModivisitanExterno", objparam, Page, (String[])Session["constrring"]);

        Session["fotoanterior"] = null;
        Session["numdocante"] = null;
        Session["Imagename"] = null;
        Session["grabohuella"] = null;
        Session["rfuncionario"] = null;
        Session["rfechadesde"] = null;
        Session["rfechahasta"] = null;
        Session["robserva"] = null;

        Array.Resize(ref objparam, 8);
        objparam[0] = ddlfuncionario.SelectedValue;
        objparam[1] = txtcodigo.Text;
        objparam[2] = fun.SecuencialSiguiente("121", (String[])Session["constrring"]);
        objparam[3] = txtfechadesde.Text;
        objparam[4] = txtfechahasta.Text;
        objparam[5] = txtobserva.Text.ToUpper();
        objparam[6] = Session["usuCodigo"].ToString();
        objparam[7] = Session["MachineName"].ToString();
        dsData = fun.consultarDatos("spInserVisTempExt", objparam, Page, (String[])Session["constrring"]);
        ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='frmvisitanteingreAdmin.aspx?mensajeRetornado=Ingreso Correcto al CRS';window.close();", true);
    }

    protected void btncamara_Click(object sender, ImageClickEventArgs e)
    {
        String pagina = "WebCam/CaptureImage.aspx?codigofun=" + txtcodigo.Text;
        String newpag = "window.open('" + pagina + "', 'popup_window', 'width=950,height=500,left=50,top=50,resizable=yes');";
        ClientScript.RegisterStartupScript(GetType(), "pop", newpag, true);
    }

    protected void btnhuella_Click(object sender, ImageClickEventArgs e)
    {
        Session["rfuncionario"]=ddlfuncionario.SelectedValue;
        Session["rfechadesde"]=txtfechadesde.Text;
        Session["rfechahasta"]=txtfechahasta.Text;
        Session["robserva"]=txtobserva.Text;
        
        //String valores = "";
        //for (int i = 1; i <= 10; i++)
        //{
        //    Array.Resize(ref objparam, 2);
        //    objparam[0] = txtcodigo.Text;
        //    objparam[1] = i;
        //    dsData = fun.CrearArchivodat("spCargHuellaFun", objparam, Page, (String[])Session["constrring"]);
        //    if (dsData.Tables[0].Rows.Count == 0) valores = valores + "," + "0";
        //    if (dsData.Tables[0].Rows.Count > 0) valores = valores + "," + "1";
        //}
        //valores = valores.Substring(1);
        //String[] createtext = { valores };
        //File.WriteAllLines(rutahuell + "huellas.dat", createtext);
        Session["capturar"] = "S";
        Session["verificar"] = "N";
        Array.Resize(ref objparam, 1);
        objparam[0] = txtcodigo.Text;
        dsData = fun.consultarDatos("spCargFuncioHuella", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0)
        {
            Session["capturar"] = "N";
            Session["verificar"] = "S";
        }
        Session["tabla"] = "Funcionario";
        Session["redirec"] = "13";
        Session["codigotempVisitante"] = txtcodigo.Text;
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
        Session["fotoanterior"] = null;
        Session["numdocante"] = null;
        Session["Imagename"] = null;
        Session["grabohuella"] = null;
        Session["rfuncionario"] = null;
        Session["rfechadesde"] = null;
        Session["rfechahasta"] = null;
        Session["robserva"] = null;
        ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.close();", true);
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
}