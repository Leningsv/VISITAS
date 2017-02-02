using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Funcionario_frmvisitanteextEdit : System.Web.UI.Page
{
    #region Variables
    DataSet dsData = new DataSet();
    Object[] objparam = new Object[1];
    Funciones fun = new Funciones();
    String strObt = "";
    String foto = "";
    String rutahuell = "C:/Temp/";
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
                lbltitulo.Text = "modificar visitante externo";

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

                if (Request["mensajeRetornado"] != null)
                {
                    SIFunBasicas.Basicas.PresentarMensaje(Page, "::VISITA PPL::", Request["MensajeRetornado"]);
                }
                Session["codigogenfun"] = Request["codigofun"].ToString();
                funCargaMantenimiento(Session["codigogenfun"].ToString());

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
            txtobserva.Text = dsData.Tables[0].Rows[0][11].ToString();
            foto = dsData.Tables[0].Rows[0][12].ToString();
            chkestado.Checked = dsData.Tables[0].Rows[0][13].ToString() == "True" ? true : false;
            chkestado.Text = dsData.Tables[0].Rows[0][13].ToString() == "True" ? "Activo" : "Inactivo";
            txtentidad.Text = dsData.Tables[0].Rows[0][14].ToString();
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
    protected void btngrabar_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["grabohuella"] == null) Session["grabohuella"] = "N";
        if (Session["Imagename"] == null) foto = Session["fotoantigua"].ToString();
        if (Session["Imagename"] != null) foto = Session["Imagename"].ToString();
        //PREGUNTAR SI EXISTE EL FUNCIONARIO
        if (Session["numdocuante"].ToString() != txtndocu.Text)
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
        //if (!fun.IsFechaNacimiento(txtfecnaci.Text))
        //{
        //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Fecha de Nacimiento Incorrecta');", true);
        //    return;
        //}

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
        objparam[14] = chkestado.Checked;
        objparam[15] = foto;
        objparam[16] = Session["grabohuella"].ToString();
        objparam[17] = Session["usuCodigo"].ToString();
        objparam[18] = Session["MachineName"].ToString();
        dsData = fun.consultarDatos("spModivisitanExterno", objparam, Page, (String[])Session["constrring"]);

        Session["Imagename"] = null;
        Session["fotoantigua"] = null;
        Session["grabohuella"] = null;
        Session["capturar"] = null;
        Session["verificar"] = null;

        Response.Redirect("frmvisitanteexternoAdmin.aspx?mensajeRetornado='Guardado con éxito..'");
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
        Session["capturar"] = null;
        Session["verificar"] = null;
        Session["grabohuella"] = null;
        Response.Redirect("frmvisitanteexternoAdmin.aspx");
    }

    protected void chkestado_CheckedChanged(object sender, EventArgs e)
    {
        chkestado.Text = chkestado.Checked == true ? "Activo" : "Inactivo";
    }

    protected void btncamara_Click(object sender, ImageClickEventArgs e)
    {
        String pagina = "WebCam/CaptureImage.aspx?codigofun=" + txtcodigo.Text;
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
        Session["tabla"] = "Funcionario"; ;
        Session["redirec"] = "11";
        Session["capturar"] = "S";
        Session["verificar"] = "N";
        Session["codigotempVisitante"] = txtcodigo.Text;
        String pagina = "../VisitaPPL/CapturaHuella.aspx";
        String newpag = "window.open('" + pagina + "', 'popup_window', 'width=950,height=600,left=50,top=50,resizable=yes');";
        ClientScript.RegisterStartupScript(GetType(), "pop", newpag, true);
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