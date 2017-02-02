using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Visita_frmvisitanteEdit : System.Web.UI.Page
{
    #region Variables
    DataSet dsData = new DataSet();
    ListItem listadatos = new ListItem();
    Object[] objparam = new Object[1];
    String strObt = "";
    String foto = "";
    //String rutahuell = "C:/Temp/";
    Funciones fun = new Funciones();
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
                    RefrescarFoto(Request["visCodigo"].ToString());
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
                lbltitulo.Text = "Modificar datos Visitante";
                AccesoInternet();
                //TRAER EL NUMERO DEL SECUENCIAL SI NO TIENE YA CAPTURADO

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

                objparam[0] = 0;
                objparam[1] = "8";
                strObt = fun.ObtenerRutas(Page, "spCargarParametros", objparam, (String[])Session["constrring"]);
                String[] session = strObt.Split('|');
                Session["rutafoto"] = session[0].ToString();
                Session["rutahuella"] = session[1].ToString();

                if (Request["mensajeRetornado"] != null)
                {
                    SIFunBasicas.Basicas.PresentarMensaje(Page, "::VISITA PPL::", Request["MensajeRetornado"]);
                }

                funCargarPost(Request["visCodigo"].ToString());
            }
        }
        catch (Exception ex)
        {
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", ex.Message);
        }
    }
    #endregion

    #region Funciones y Procedimientos
    protected void funCargarPost(String strCodigoVisitante)
    {
        txtcodigo.Text = strCodigoVisitante;
        String rutasrv = Server.MapPath(Session["rutafoto"].ToString());
        Array.Resize(ref objparam, 1);
        objparam[0] = txtcodigo.Text;
        dsData = fun.consultarDatos("spCarVisitanDatos", objparam, Page, (String[])Session["constrring"]);
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
                txtnombre1.Enabled = true;
                txtndocu_FilteredTextBoxExtender.FilterMode = AjaxControlToolkit.FilterModes.InvalidChars;
                txtndocu_FilteredTextBoxExtender.InvalidChars = ".-*/{{}}[[]]\\";
                txtndocu_FilteredTextBoxExtender.FilterType = AjaxControlToolkit.FilterTypes.Custom;
            }
            txtndocu.Text = dsData.Tables[0].Rows[0][1].ToString();
            txtnombre1.Text = dsData.Tables[0].Rows[0][2].ToString();
            txtnombre2.Text = dsData.Tables[0].Rows[0][3].ToString();
            txtapellido1.Text = dsData.Tables[0].Rows[0][4].ToString();
            txtapellido2.Text = dsData.Tables[0].Rows[0][5].ToString();
            txtobserva.Text = dsData.Tables[0].Rows[0][7].ToString();
            chkestado.Text = dsData.Tables[0].Rows[0][8].ToString() == "True" ? "Activo" : "Inactivo";
            chkestado.Checked = Convert.ToBoolean(dsData.Tables[0].Rows[0][8].ToString());
            foto = dsData.Tables[0].Rows[0][9].ToString();
            txtdireccion.Text = dsData.Tables[0].Rows[0][10].ToString();
            txttelefono.Text = dsData.Tables[0].Rows[0][11].ToString();
            ddlgenero.SelectedValue = dsData.Tables[0].Rows[0][12].ToString();
            Session["fotoantigua"] = foto;
            Session["numdocuante"] = txtndocu.Text;
            if (txtndocu.Text != "")
            {
                ddltipodoc.Enabled = false;
                txtndocu.Enabled = false;
                txtnombre1.Enabled = false;
 
            }
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

    private void RefrescarFoto(string codVisitante)
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

    protected void funCargaDatosRegCivil()
    {
        //LLAMAR A LA FUNCION PARA CONSULTAR DATOS
        String Datos = fun.DatosBSG_RegistroCivil("0801693813", "https://www.bsg.gob.ec/sw/RC/BSGSW01_Consultar_Cedula?wsdl", "testroot", "Sti1DigS21", txtndocu.Text);

        mis_datos = Datos.Split('|');
        txtnombre1.Text = mis_datos[0].ToString();
        if (mis_datos[1].ToString() == "HOMBRE")
        {
            ddlgenero.SelectedValue = "M";
        }
        else ddlgenero.SelectedValue = "F";
        txtdireccion.Text = mis_datos[2].ToString();
        txtnombre1.Enabled = false;
        ddlgenero.Enabled = false;
        Button1.Enabled = true;
        Button1.Text = "Buscar Registro Civil";

    }
    #endregion

    #region Botones y Eventos
    protected void ddltipodoc_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtndocu.Text = "";
        ddlgenero.SelectedValue = "M";
        if (ddltipodoc.SelectedValue == "C")
        {
            txtndocu_FilteredTextBoxExtender.FilterMode = AjaxControlToolkit.FilterModes.ValidChars;
            txtndocu_FilteredTextBoxExtender.InvalidChars = ".-";
            txtndocu_FilteredTextBoxExtender.FilterType = AjaxControlToolkit.FilterTypes.Numbers;
        }
        else
        {
            txtnombre1.Enabled = true;
            ddlgenero.Enabled = true;
            txtndocu_FilteredTextBoxExtender.FilterMode = AjaxControlToolkit.FilterModes.InvalidChars;
            txtndocu_FilteredTextBoxExtender.InvalidChars = ".-*/{{}}[[]]\\";
            txtndocu_FilteredTextBoxExtender.FilterType = AjaxControlToolkit.FilterTypes.Custom;
        }
    }
    protected void chkestado_CheckedChanged(object sender, EventArgs e)
    {
        chkestado.Text = chkestado.Checked == true ? "Activo" : "Inactivo";
    }
    protected void btngrabar_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["numdocuante"].ToString() != txtndocu.Text)
        {
            Array.Resize(ref objparam, 1);
            objparam[0] = txtndocu.Text;
            dsData = fun.consultarDatos("spVerificaCed", objparam, Page, (String[])Session["constrring"]);
            if (dsData.Tables[0].Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Número de Documento ya existe');", true);
                return;
            }
        }

        Array.Resize(ref objparam, 16);
        objparam[0] = txtcodigo.Text;
        objparam[1] = ddltipodoc.SelectedValue;
        objparam[2] = txtndocu.Text;
        objparam[3] = txtapellido1.Text.ToUpper();
        objparam[4] = txtapellido2.Text.ToUpper();
        objparam[5] = txtnombre1.Text.ToUpper();
        objparam[6] = txtnombre2.Text.ToUpper();
        objparam[7] = DateTime.Now;
        objparam[8] = txtobserva.Text.ToUpper();
        objparam[9] = chkestado.Checked;
        objparam[10] = Session["Imagename"] != null ? Session["Imagename"].ToString() : Session["fotoantigua"].ToString();
        objparam[11] = Session["usuCodigo"].ToString();
        objparam[12] = Session["MachineName"].ToString();
        objparam[13] = txtdireccion.Text.ToUpper();
        objparam[14] = txttelefono.Text;
        objparam[15] = ddlgenero.SelectedValue;
        dsData = fun.consultarDatos("spUpdateVisitanteDatos", objparam, Page, (String[])Session["constrring"]);
        Session["Imagename"] = null;
        Session["fotoantigua"] = null;
        Response.Redirect("frmvisitanteAdmin.aspx?mensajeRetornado='Guardado con Éxito'");
    }
    protected void btncamara_Click(object sender, ImageClickEventArgs e)
    {
        String pagina = "../VisitaPPL/WebCam/CaptureImage.aspx?codigovisitante=" + txtcodigo.Text;
        String newpag = "window.open('" + pagina + "', 'popup_window', 'width=950,height=500,left=50,top=50,resizable=yes');";
        ClientScript.RegisterStartupScript(GetType(), "pop", newpag, true);
    }
    protected void btnhuella_Click(object sender, ImageClickEventArgs e)
    {
        //String valores = "";
        ////Crear el archivo .dat de los dedos si existe
        //for (int i = 1; i <= 10; i++)
        //{
        //    Array.Resize(ref objparam, 2);
        //    objparam[0] = txtcodigo.Text;
        //    objparam[1] = i;
        //    dsData = fun.CrearArchivodat("spCargHuellaVis", objparam, Page, (String[])Session["constrring"]);
        //    if (dsData.Tables[0].Rows.Count == 0) valores = valores + "," + "0";
        //    if (dsData.Tables[0].Rows.Count > 0) valores = valores + "," + "1";
        //}
        //valores = valores.Substring(1);
        //String[] createtext = { valores };
        //File.WriteAllLines(rutahuell + "huellas.dat", createtext);
        Session["tabla"] = "Visitante";
        Session["redirec"] = "3";
        Session["codigotempVisitante"] = txtcodigo.Text;
        Session["capturar"] = "S";
        Session["verificar"] = "N";
        String pagina = "../VisitaPPL/CapturaHuella.aspx";
        String newpag = "window.open('" + pagina + "', 'popup_window', 'width=950,height=600,left=50,top=50,resizable=yes');";
        ClientScript.RegisterStartupScript(GetType(), "pop", newpag, true);
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
        Session["fotoantigua"] = null;
        Session["capturar"] = null;
        Session["verificar"] = null;
        Session["grabohuella"] = null;
        Response.Redirect("frmvisitanteAdmin.aspx");
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
    #endregion
    protected void Button1_Click(object sender, EventArgs e)
    {
        funCargaDatosRegCivil();
    }
}