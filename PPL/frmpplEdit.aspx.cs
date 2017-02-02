using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class PPL_frmpplEdit : System.Web.UI.Page
{
    #region Variables
    DataSet dsData = new DataSet();
    ListItem listadatos = new ListItem();
    Object[] objparam = new Object[1];
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
                    RefrescarFoto(Request["pplCodigo"].ToString());
                }
            }
            if (!IsPostBack)
            {
                txtndocu.Attributes.Add("onchange", "Validar_Cedula();");
                lbltitulo.Text = "Modificar datos ppl";

                //TRAER EL NUMERO DEL SECUENCIAL SI NO TIENE YA CAPTURADO

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

                funCargarPost(Request["pplCodigo"].ToString());
            }
        }
        catch (Exception ex)
        {
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", ex.Message);
        }
    }
    #endregion

    #region Funciones y Procedimientos
    protected void funCargarPost(String strCodigoPPL)
    {
        txtcodigo.Text = strCodigoPPL;
        String rutasrv = Server.MapPath(Session["rutafoto"].ToString());
        Array.Resize(ref objparam, 1);
        objparam[0] = strCodigoPPL;
        dsData = fun.consultarDatos("spCarPPLDatos", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0)
        {
            ddltipodoc.SelectedValue = dsData.Tables[0].Rows[0][0].ToString();
            txtndocu.Text = dsData.Tables[0].Rows[0][1].ToString();
            txtnombre1.Text = dsData.Tables[0].Rows[0][2].ToString();
            txtnombre2.Text = dsData.Tables[0].Rows[0][3].ToString();
            txtapellido1.Text = dsData.Tables[0].Rows[0][4].ToString();
            txtapellido2.Text = dsData.Tables[0].Rows[0][5].ToString();
            ddletapa.SelectedValue = dsData.Tables[0].Rows[0][6].ToString();
            ddlpabellon.SelectedValue = dsData.Tables[0].Rows[0][7].ToString();
            ddlala.SelectedValue = dsData.Tables[0].Rows[0][8].ToString();
            ddlpiso.SelectedValue = dsData.Tables[0].Rows[0][9].ToString();
            txtcelda.Text = dsData.Tables[0].Rows[0][10].ToString();
            txtobserva.Text = dsData.Tables[0].Rows[0][11].ToString();
            chkestado.Text = dsData.Tables[0].Rows[0][12].ToString() == "1" ? "Activo" : "Inactivo";
            chkestado.Checked = dsData.Tables[0].Rows[0][12].ToString() == "1" ? true : false;
            foto = dsData.Tables[0].Rows[0][13].ToString();
            Session["fotoantigua"] = foto;
            Session["numdocuante"] = txtndocu.Text;
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
            else
            {
                imgfoto.ImageUrl = "~/Images/sinfoto.jpg";
            }
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
    #endregion

    #region Botones y Eventos
    protected void btngrabar_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["numdocuante"].ToString() != txtndocu.Text)
        {
            Array.Resize(ref objparam, 1);
            objparam[0] = txtndocu.Text;
            dsData = fun.consultarDatos("spVeriCedPPL", objparam, Page, (String[])Session["constrring"]);
            if (dsData.Tables[0].Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Número de Documento ya existe');", true);
                return;
            }
        }

        if (Session["Imagename"] != null)
        {
            foto = Session["Imagename"].ToString();
            String rutasrv = Server.MapPath(Session["rutafoto"].ToString());
            if (File.Exists(rutasrv + Session["fotoantigua"].ToString()))
            {
                File.Delete(rutasrv + Session["fotoantigua"].ToString());
            }
        }
        else foto = Session["fotoantigua"].ToString();

        Array.Resize(ref objparam, 17);
        objparam[0] = txtcodigo.Text;
        objparam[1] = ddltipodoc.SelectedValue;
        objparam[2] = txtndocu.Text;
        objparam[3] = txtnombre1.Text.ToUpper();
        objparam[4] = txtnombre2.Text.ToUpper();
        objparam[5] = txtapellido1.Text.ToUpper();
        objparam[6] = txtapellido2.Text.ToUpper();
        objparam[7] = ddletapa.SelectedValue;
        objparam[8] = ddlpabellon.SelectedValue;
        objparam[9] = ddlala.SelectedValue;
        objparam[10] = ddlpiso.SelectedValue;
        objparam[11] = txtcelda.Text;
        objparam[12] = txtobserva.Text.ToUpper();
        objparam[13] = chkestado.Checked == true ? "1" : "0";
        objparam[14] = foto;
        objparam[15] = Session["usuCodigo"].ToString();
        objparam[16] = Session["MachineName"].ToString();
        dsData = fun.consultarDatos("spUpdaPPLDatos", objparam, Page, (String[])Session["constrring"]);
        Session["Imagename"] = null;
        Response.Redirect("frmpplAdmin.aspx?mensajeRetornado='Guardado con Éxito'");
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
    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("frmpplAdmin.aspx");
    }
    protected void btncamara_Click(object sender, ImageClickEventArgs e)
    {
        String pagina = "../VisitaPPL/WebCam/CaptureImage.aspx?codigovisitante=" + txtcodigo.Text;
        String newpag = "window.open('" + pagina + "', 'popup_window', 'width=950,height=500,left=50,top=50,resizable=yes');";
        ClientScript.RegisterStartupScript(GetType(), "pop", newpag, true);
    }
    protected void chkestado_CheckedChanged(object sender, EventArgs e)
    {
        chkestado.Text = chkestado.Checked == true ? "Activo" : "Inactivo";
    }
    #endregion
}