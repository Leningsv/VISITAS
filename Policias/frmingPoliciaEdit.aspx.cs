using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Policias_frmingPoliciaEdit : System.Web.UI.Page
{
    #region Variables
    DataSet dsData = new DataSet();
    ListItem listadatos = new ListItem();
    Object[] objparam = new Object[1];
    //String ruta = @"C:\Temp\huellas.dat";
    String strObt = "";
    String foto = "";
    Funciones fun = new Funciones();
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
                    RefrescarFoto(Request["CodPol"].ToString());
                }
            }

            if (!IsPostBack)
            {
                lbltitulo.Text = "Modificación de Datos de Policía";

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

                if (Request["mensajeRetornado"] != null)
                    SIFunBasicas.Basicas.PresentarMensaje(Page, "::VISITA PPL::", Request["MensajeRetornado"]);

                funCargaMantenimiento(Request["CodPol"].ToString());
                if (Request["CodPol"] != null)
                {
                    ddlTipoDocumento.SelectedValue = Session["tdoc"].ToString();
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
                    ddlTipoDocumento.SelectedValue = Session["tdoc"].ToString();
                    txtNroDoc.Text = Session["ndoc"].ToString();
                    txtnombres.Text = Session["nom1"].ToString();
                    txtapellidos.Text = Session["ape1"].ToString();
                    txtFechaNac.Text = Session["fechan"].ToString();
                    txtCelular.Text = Session["celular"].ToString();
                    ddlRango.SelectedValue = Session["cargo"].ToString();
                    ddlArea.SelectedValue = Session["area"].ToString();
                    txtFechaIng.Text = Session["fechadesde"].ToString();
                    txtFechaSal.Text = Session["fechahasta"].ToString();
                    txtObservacion.Text = Session["observa"].ToString();
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
    protected void funCargaMantenimiento(String strCodigoPol)
    {
        String rutasrv = Server.MapPath(Session["rutafoto"].ToString());
        Array.Resize(ref objparam, 1);
        objparam[0] = strCodigoPol;
        dsData = fun.consultarDatos("spSelPolicia", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0)
        {
            ddlTipoDocumento.SelectedValue = dsData.Tables[0].Rows[0][1].ToString();
            HabilitarNumDocumento(ddlTipoDocumento.SelectedValue);
            txtNroDoc.Text = dsData.Tables[0].Rows[0][2].ToString();
            txtnombres.Text = dsData.Tables[0].Rows[0][3].ToString();
            txtapellidos.Text = dsData.Tables[0].Rows[0][4].ToString();
            txtFechaNac.Text = dsData.Tables[0].Rows[0][5].ToString().Substring(0, 10) == "01/01/9999" ? "" : dsData.Tables[0].Rows[0][5].ToString();
            txtFechaIng.Text = dsData.Tables[0].Rows[0][6].ToString().Substring(0, 10) == "01/01/9999" ? "" : dsData.Tables[0].Rows[0][6].ToString();
            txtFechaSal.Text = dsData.Tables[0].Rows[0][7].ToString().Substring(0, 10) == "01/01/9999" ? "" : dsData.Tables[0].Rows[0][7].ToString();
            ddlRango.SelectedValue = dsData.Tables[0].Rows[0][8].ToString();
            ddlArea.SelectedValue = dsData.Tables[0].Rows[0][9].ToString();
            txtCelular.Text = dsData.Tables[0].Rows[0][10].ToString();
            txtObservacion.Text = dsData.Tables[0].Rows[0][11].ToString();
            chkestado.Text = dsData.Tables[0].Rows[0][13].ToString() == "True" ? "Activo" : "Inactivo";
            chkestado.Checked = Convert.ToBoolean(dsData.Tables[0].Rows[0][13].ToString());
            foto = dsData.Tables[0].Rows[0][15].ToString();
            Session["fotoanterior"] = foto;
        }

        //if (File.Exists(ruta)) File.Delete(ruta);

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
        btncamara.Visible = true;
        btnhuella.Visible = true;              
        btngrabar.Visible = true;
        chkestado.Enabled = true;
    }

    private void RefrescarFoto(string codPolicia)
    {
        Array.Resize(ref objparam, 1);
        objparam[0] = codPolicia;
        dsData = fun.consultarDatos("spSelFotoPolicia", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0) foto = dsData.Tables[0].Rows[0][0].ToString();

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
    protected void chkestado_CheckedChanged(object sender, EventArgs e)
    {
        chkestado.Text = chkestado.Checked == true ? "Activo" : "Inactivo";
    }

    private void HabilitarNumDocumento(string tipoDoc)
    {
        if (tipoDoc == "C")
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

    protected void btngrabar_Click(object sender, ImageClickEventArgs e)
    {
        string estado;

        if (!fun.IsFechaNacimiento(txtFechaNac.Text))
        {
            lblmsj1.Text = "La Fecha de Nacimiento es incorrecta";
            lblmsj1.Visible = true;
            return;
        }

        if (txtFechaIng.Text != "" || txtFechaSal.Text != "")
        {
            if (Convert.ToDateTime(txtFechaIng.Text) > Convert.ToDateTime(txtFechaSal.Text))
            {
                lblmsj1.Text = "La Fecha de Ingreso no puede ser mayor a la Fecha de Salida";
                lblmsj1.Visible = true;
                return;
            }

            if (Convert.ToDateTime(txtFechaSal.Text + " 23:59:59") < DateTime.Now)
            {
                lblmsj1.Text = "La Fecha de Salida no puede ser menor a la Fecha Actual";
                lblmsj1.Visible = true;
                return;
            }
        }

        if (txtFechaNac.Text == "" || txtFechaIng.Text == "" || txtFechaSal.Text == "") estado = "PI";
        else estado = "IN";

        if (ddlTipoDocumento.SelectedValue == "C")
        {
            if (!fun.cedulaBienEscrita(txtNroDoc.Text))
            {
                lblmsj1.Visible = true;
                lblmsj1.Text = "La Cédula ingresada es incorrecta";
                return;
            }
        }

        Array.Resize(ref objparam, 16);
        objparam[0] = Request["CodPol"].ToString();
        objparam[1] = ddlTipoDocumento.SelectedValue;
        objparam[2] = txtNroDoc.Text;
        objparam[3] = txtnombres.Text.ToUpper();
        objparam[4] = txtapellidos.Text.ToUpper();
        objparam[5] = txtFechaNac.Text == "" ? "01/01/9999" : txtFechaNac.Text;
        objparam[6] = txtFechaIng.Text == "" ? "01/01/9999" : txtFechaIng.Text;
        objparam[7] = txtFechaSal.Text == "" ? "01/01/9999" : txtFechaSal.Text;
        objparam[8] = ddlRango.SelectedValue;
        objparam[9] = ddlArea.SelectedValue;
        objparam[10] = txtCelular.Text;
        objparam[11] = txtObservacion.Text;
        objparam[12] = estado;
        objparam[13] = chkestado.Checked;
        objparam[14] = "";//cod Acceso
        objparam[15] = Session["usuCodigo"].ToString();
        dsData = fun.consultarDatos("spActPolicia", objparam, Page, (String[])Session["constrring"]);
        switch (dsData.Tables[0].Rows[0][0].ToString())
        {
            case "Existe":
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Policía ya existe registrado con el número de documento');", true);
                return;
        }
        Session["Imagename"] = "";
        Session["capturar"] = "";
        Session["verificar"] = "";
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
        String pagina = "../Policias/WebCam/CaptureImage.aspx?codigovisitante=" + Request["CodPol"].ToString();
        String newpag = "javascript:window.open('" + pagina + "', 'popup', 'width=950,height=500,left=50,top=50,resizable=yes');";
        ClientScript.RegisterStartupScript(GetType(), "pop", newpag, true);
    }

    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        Session["fotoanterior"] = "";
        Session["Huella"] = "";
        Session["Imagename"] = "";
        Session["capturar"] = "";
        Session["verificar"] = "";
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

    protected void btnrefrescar_Click(object sender, ImageClickEventArgs e)
    {
        RefrescarFoto(Request["CodPol"].ToString());
    }

    protected void btnhuella_Click(object sender, ImageClickEventArgs e)
    {
        Session["tabla"] = "Policia";
        Session["capturar"] = "S";
        Session["verificar"] = "N";
        String pagina = "CapturarHuellaPol.aspx?CodigoPolicia=" + Request["CodPol"].ToString();
        String newpag = "window.open('" + pagina + "', 'popup_window', 'width=950,height=600,left=50,top=50,resizable=yes');";
        ClientScript.RegisterStartupScript(GetType(), "pop", newpag, true);
    }

    protected void ddlTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtNroDoc.Text = "";
        HabilitarNumDocumento(ddlTipoDocumento.SelectedValue);
    }

    #endregion   
}