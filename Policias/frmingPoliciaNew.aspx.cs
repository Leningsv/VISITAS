using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Policias_frmingPoliciaNew : System.Web.UI.Page
{
    #region Variables
    DataSet dsData = new DataSet();
    ListItem listadatos = new ListItem();
    Object[] objparam = new Object[1];
    //String ruta = @"C:\Temp\huellas.dat";
    String strObt = "";
    String foto = "";
    String[] mis_datos;
    Funciones fun = new Funciones();
    int a;

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
                  RefrescarFoto(Session["CodPol"].ToString());
                }
                if (Page.Request.Params["__EVENTTARGET"] == "txtndocu")
                {
                    if (Session["regCIVIL"].ToString() == "SI")
                    {
                        funCargaDatosRegCivil();
                    }
                }
            }
            txtNroDoc.Attributes.Add("onchange", "Validar_Cedula();");
            if (!IsPostBack)
            {
                lbltitulo.Text = "Ingreso de un nuevo Policia";
                txtFechaIng.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtFechaSal.Text = DateTime.Now.ToString("dd/MM/yyyy");
                AccesoInternet();
                Array.Resize(ref objparam, 2);
                objparam[0] = 0;
                objparam[1] = "4";//Tipo Doc
                fun.cargarCombos(ddlTipoDocumento, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
                ddlTipoDocumento.Items.RemoveAt(0);

                objparam[0] = 0;
                objparam[1] = "8";//rutas de archivos
                strObt = fun.ObtenerRutasPol(Page, "spCargarParametros", objparam, (String[])Session["constrring"]);
                String[] session = strObt.Split('|');
                Session["rutafoto"] = session[0].ToString();
                Session["rutahuella"] = session[1].ToString();

                Array.Resize(ref objparam, 2);
                objparam[0] = 0;
                objparam[1] = "40";//rangos
                fun.cargarCombos(ddlRango, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
                ddlRango.Items.RemoveAt(0);

                Array.Resize(ref objparam, 2);
                objparam[0] = 0;
                objparam[1] = "41";//area asignada
                fun.cargarCombos(ddlArea, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
                ddlArea.Items.RemoveAt(0);
                
                    Session["CodPol"] = ObtenerCodigoPolicia();
                    Session["fotoanterior"] = "";
                    Session["Huella"] = "";
                    Session["Imagename"] = "";
                   
               
           }

        }
        catch (Exception ex)
        {
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", ex.Message);
        }
    }
    #endregion

    #region Botones y Eventos
    protected void chkestado_CheckedChanged(object sender, EventArgs e)
    {
        chkestado.Text = chkestado.Checked == true ? "Activo" : "Inactivo";
    }

    protected void btnrefrescar_Click(object sender, ImageClickEventArgs e)
    {
        RefrescarFoto(Session["CodPol"].ToString());
        a = 1;

      
    }
    protected void btngrabar_Click(object sender, ImageClickEventArgs e)
    {
        string estado;

        if (txtFechaNac.Text != "")
        {
            if (!fun.IsFechaNacimiento(txtFechaNac.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('La Fecha de Nacimiento es incorrecta');", true);
                return;
            }
        }

        if (txtFechaIng.Text != "" || txtFechaSal.Text != "")
        {
            if (Convert.ToDateTime(txtFechaIng.Text) > Convert.ToDateTime(txtFechaSal.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('La Fecha de Ingreso no puede ser mayor a la Fecha de Salida');", true);
                return;
            }

            if (Convert.ToDateTime(txtFechaSal.Text + " 23:59:59") < DateTime.Now)//se suma la hora porque se compara tambien las horas
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('La Fecha de Salida no puede ser menor a la Fecha Actual');", true);
                return;
            }
        }

        if (txtFechaNac.Text == "" || txtFechaIng.Text == "" || txtFechaSal.Text == "") estado = "PI";
        else estado = "IN";

        if (ddlTipoDocumento.SelectedValue == "C")
        {
            if (!fun.cedulaBienEscrita(txtNroDoc.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('La Cédula ingresada es incorrecta');", true);
                return;
            }
        }

        Array.Resize(ref objparam, 18);
        objparam[0] = ddlTipoDocumento.SelectedValue;
        objparam[1] = txtNroDoc.Text;
        objparam[2] = txtnombres.Text.ToUpper();
        objparam[3] = txtapellidos.Text.ToUpper();
        objparam[4] = txtFechaNac.Text == "" ? "01/01/9999" : txtFechaNac.Text;
        objparam[5] = txtFechaIng.Text == "" ? "01/01/9999" : txtFechaIng.Text;
        objparam[6] = txtFechaSal.Text == "" ? "01/01/9999" : txtFechaSal.Text;
        objparam[7] = ddlRango.SelectedValue;
        objparam[8] = ddlArea.SelectedValue;
        objparam[9] = txtCelular.Text;
        objparam[10] = txtObservacion.Text;
        objparam[11] = estado;
        objparam[12] = chkestado.Checked;
        objparam[13] = "";//acceso
        objparam[14] = Session["Imagename"].ToString();//foto
        objparam[15] = Session["Huella"].ToString();//huella
        objparam[16] = Session["usuCodigo"].ToString();
        objparam[17] = Session["CodPol"].ToString();
        dsData = fun.consultarDatos("spInsPolicia", objparam, Page, (String[])Session["constrring"]);
        switch (dsData.Tables[0].Rows[0][0].ToString())
        {
            case "Existe":
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Policía ya existe registrado con el número de documento');", true);
                return;
        }
        Session["tdoc"] = null;
        Session["ndoc"] = null;
        Session["nom1"] = null;
        Session["ape1"] = null;
        Session["fechan"] = null;
        Session["celular"] = null;
        Session["cargo"] = null;
        Session["area"] = null;
        Session["fechadesde"] = null;
        Session["fechahasta"] = null;
        Session["observa"] = null;

        Response.Redirect("frmingPoliciaAdmin.aspx?mensajeRetornado='Guardado con Éxito'");
    }

    protected void btncamara_Click(object sender, ImageClickEventArgs e)
    {
        String pagina = "../Policias/WebCam/CaptureImage.aspx?codigovisitante=" + Session["CodPol"].ToString();
        String newpag = "javascript:window.open('" + pagina + "', 'popup', 'width=950,height=500,left=50,top=50,resizable=yes');";
        ClientScript.RegisterStartupScript(GetType(), "pop", newpag, true);
    }

    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["Imagename"].ToString() != "")
        {
            String rutasrv = Server.MapPath(Session["rutafoto"].ToString());
            if (File.Exists(rutasrv + Session["Imagename"].ToString()))
            {
                File.Delete(rutasrv + Session["Imagename"].ToString());
            }
        }
        Array.Resize(ref objparam, 1);
        objparam[0] = Session["CodPol"].ToString();
        dsData = fun.consultarDatos("spEliHuellaPolicia", objparam, Page, (String[])Session["constrring"]);
       
        Session["CodPol"] = "";
        Session["Imagename"] = null;
        Session["tdoc"] = null;
        Session["ndoc"] = null;
        Session["nom1"] = null;
        Session["ape1"] = null;
        Session["fechan"] = null;
        Session["celular"] = null;
        Session["cargo"] = null;
        Session["area"] = null;
        Session["fechadesde"] = null;
        Session["fechahasta"] = null;
        Session["observa"] = null;

        Response.Redirect("frmingPoliciaAdmin.aspx");
       
    }
    
    protected void btnhuella_Click(object sender, ImageClickEventArgs e)
    {
        
        Session["tdoc"] = ddlTipoDocumento.SelectedValue;
        Session["ndoc"] = txtNroDoc.Text;
        Session["nom1"] = txtnombres.Text;
        Session["ape1"] = txtapellidos.Text;
        Session["fechan"] = txtFechaNac.Text;
        Session["celular"] = txtCelular.Text;
        Session["cargo"] = ddlRango.SelectedValue;
        Session["area"] = ddlArea.SelectedValue;
        Session["fechadesde"] = txtFechaIng.Text;
        Session["fechahasta"] = txtFechaSal.Text;
        Session["observa"] = txtObservacion.Text;
        Session["tabla"] = "Policia";
        Session["capturar"] = "S";
        Session["verificar"] = "N";
        String pagina = "CapturarHuellaPol.aspx?CodigoPolicia=" + Session["CodPol"].ToString();
        String newpag = "window.open('" + pagina + "', 'popup_window', 'width=950,height=600,left=50,top=50,resizable=yes');";
        ClientScript.RegisterStartupScript(GetType(), "pop", newpag, true);

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

    protected void ddlTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtNroDoc.Text = "";
        if (ddlTipoDocumento.SelectedValue == "C")
        {
            txtNroDoc_FilteredTextBoxExtender.FilterMode = AjaxControlToolkit.FilterModes.ValidChars;
            txtNroDoc_FilteredTextBoxExtender.InvalidChars = ".-";
            txtNroDoc_FilteredTextBoxExtender.FilterType = AjaxControlToolkit.FilterTypes.Numbers;
        }
        else
        {
            txtNroDoc_FilteredTextBoxExtender.FilterMode = AjaxControlToolkit.FilterModes.InvalidChars;
            txtNroDoc_FilteredTextBoxExtender.InvalidChars = ".-*/{{}}[[]]\\";
            txtNroDoc_FilteredTextBoxExtender.FilterType = AjaxControlToolkit.FilterTypes.Custom;
        }
    }
    #endregion


    #region Funciones y Procedimientos
    private void RefrescarFoto(string codPolicia)
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

    private string ObtenerCodigoPolicia()
    {
        Array.Resize(ref objparam, 1);
        objparam[0] = "1";//codigo del parametro policia
        dsData = fun.consultarDatos("spSecuencialGeneral", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0) return dsData.Tables[0].Rows[0][0].ToString();
        else return "";
    }
    #endregion
    #region Funciones y Procedimientos
    protected void funCargaDatosRegCivil()
    {

        //LLAMAR A LA FUNCION PARA CONSULTAR DATOS
        String Datos = fun.DatosBSG_RegistroCivil("0801693813", "https://www.bsg.gob.ec/sw/RC/BSGSW01_Consultar_Cedula?wsdl", "testroot", "Sti1DigS21", txtNroDoc.Text);

        mis_datos = Datos.Split('|');
        string nombrecompleto = mis_datos[0].ToString();
           
        txtnombretemp.Text = nombrecompleto;

        txtnombretemp.Visible = true;
        txtnombretemp.Enabled = false;
        
        Button1.Enabled = true;
        Button1.Text = "Buscar Registro Civil";


    }

    #endregion
    protected void Button1_Click(object sender, EventArgs e)
    {

        funCargaDatosRegCivil();

    }
}