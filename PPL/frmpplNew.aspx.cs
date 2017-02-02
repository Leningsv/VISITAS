using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class PPL_frmpplNew : System.Web.UI.Page
{
    #region Variables
    DataSet dsData = new DataSet();
    ListItem listadatos = new ListItem();
    Object[] objparam = new Object[1];
    String foto = "";
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
                    RefrescarFoto(Session["codigotempPPL"].ToString());
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
                txtndocu.Attributes.Add("onchange", "Validar_Cedula();");
                lbltitulo.Text = "Ingresar Nuevo PPL";
                AccesoInternet();
                //TRAER EL NUMERO DEL SECUENCIAL SI NO TIENE YA CAPTURADO
                if (Session["codigotempPPL"] != null)
                {
                    ddltipodoc.SelectedValue = Session["tdoc"].ToString();
                    txtndocu.Text = Session["ndoc"].ToString();
                    txtnombre1.Text = Session["nom1"].ToString();
                    txtnombre2.Text = Session["nom2"].ToString();
                    txtapellido1.Text = Session["ape1"].ToString();
                    txtapellido2.Text = Session["ape2"].ToString();
                    txtobserva.Text = Session["observa"].ToString();
                }
                else
                {
                    Array.Resize(ref objparam, 1);
                    objparam[0] = "4";
                    dsData = fun.consultarDatos("spSecuencialGeneral", objparam, Page, (String[])Session["constrring"]);
                    Session["codigotempPPL"] = dsData.Tables[0].Rows[0][0].ToString();
                }

                Array.Resize(ref objparam, 2);
                objparam[0] = 0;
                objparam[1] = "4";
                fun.cargarCombos(ddltipodoc, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
                ddltipodoc.Items.RemoveAt(0);

                Array.Resize(ref objparam, 2);
                objparam[0] = 0;
                objparam[1] = "9";
                fun.cargarCombos(ddletapa, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
                ddletapa.Items.RemoveAt(0);

                Array.Resize(ref objparam, 2);
                objparam[0] = 0;
                objparam[1] = "10";
                fun.cargarCombos(ddlpabellon, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
                ddlpabellon.Items.RemoveAt(0);

                Array.Resize(ref objparam, 2);
                objparam[0] = 0;
                objparam[1] = "11";
                fun.cargarCombos(ddlala, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
                ddlala.Items.RemoveAt(0);

                Array.Resize(ref objparam, 2);
                objparam[0] = 0;
                objparam[1] = "12";
                fun.cargarCombos(ddlpiso, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
                ddlpiso.Items.RemoveAt(0);

                objparam[0] = 0;
                objparam[1] = "8";
                Session["rutafoto"] = fun.ObtenerRutasPPL(Page, "spCargarParametros", objparam, (String[])Session["constrring"]);

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
    private void RefrescarFoto(string codPPL)
    {
        Array.Resize(ref objparam, 1);
        objparam[0] = codPPL;
        dsData = fun.consultarDatos("spCargaFotoPPL", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0) foto = dsData.Tables[0].Rows[0][0].ToString();
        if (dsData.Tables[0].Rows.Count == 0) foto = Session["Imagename"].ToString();

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
    protected void btngrabar_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["Imagename"] == null) Session["Imagename"] = "";
        Array.Resize(ref objparam, 18);
        objparam[0] = Session["codigotempPPL"].ToString();
        objparam[1] = Session["codigocrs"].ToString();
        objparam[2] = ddltipodoc.SelectedValue;
        objparam[3] = txtndocu.Text;
        objparam[4] = txtnombre1.Text.ToUpper();
        objparam[5] = txtnombre2.Text.ToUpper();
        objparam[6] = txtapellido1.Text.ToUpper();
        objparam[7] = txtapellido2.Text.ToUpper();
        objparam[8] = ddletapa.SelectedValue;
        objparam[9] = ddlpabellon.SelectedValue;
        objparam[10] = ddlala.SelectedValue;
        objparam[11] = ddlpiso.SelectedValue;
        objparam[12] = txtcelda.Text;
        objparam[13] = txtobserva.Text.ToUpper();
        objparam[14] = chkestado.Checked == true ? "1" : "0";
        objparam[15] = Session["Imagename"].ToString();
        objparam[16] = Session["usuCodigo"].ToString();
        objparam[17] = Session["MachineName"].ToString();
        dsData = fun.consultarDatos("spInserPPLDatos", objparam, Page, (String[])Session["constrring"]);
        Session["Imagename"] = null;
        Response.Redirect("frmpplAdmin.aspx?mensajeRetornado='Guardado con Éxito'");
    }
    protected void btncamara_Click(object sender, ImageClickEventArgs e)
    {
        String pagina = "../VisitaPPL/WebCam/CaptureImage.aspx?codigovisitante=" + Session["codigotempPPL"].ToString();
        String newpag = "window.open('" + pagina + "', 'popup_window', 'width=950,height=500,left=50,top=50,resizable=yes');";
        ClientScript.RegisterStartupScript(GetType(), "pop", newpag, true);
    }
    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("frmpplAdmin.aspx");
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
    #region Funciones y Procedimientos
    protected void funCargaDatosRegCivil()
    {

        //LLAMAR A LA FUNCION PARA CONSULTAR DATOS
        String Datos = fun.DatosBSG_RegistroCivil("0801693813", "https://www.bsg.gob.ec/sw/RC/BSGSW01_Consultar_Cedula?wsdl", "testroot", "Sti1DigS21", txtndocu.Text);

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