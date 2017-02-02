using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Funcionario_frmfuncionaEdit : System.Web.UI.Page
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
            txtndocu.Attributes.Add("onchange", "Validar_Cedula();");
            if (IsPostBack)
            {
                if (Page.Request.Params["__EVENTTARGET"] == "btnrefrescar_Click")
                {
                    RefrescarFoto(Request["codigofun"].ToString());
                }
            }

            if (!IsPostBack)
            {
                lbltitulo.Text = "moficiar datos funcionario";

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
                objparam[0] = "90";
                dsData = fun.consultarDatos("spCargValorDetalle", objparam, Page, (String[])Session["constrring"]);
                Session["ValorEntidad"] = dsData.Tables[0].Rows[0][0].ToString();

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
            //txtfecnaci.Text = dsData.Tables[0].Rows[0][6].ToString();
            ddlgenero.SelectedValue = dsData.Tables[0].Rows[0][7].ToString();
            txtcelular.Text = dsData.Tables[0].Rows[0][8].ToString();
            txtemail.Text = dsData.Tables[0].Rows[0][9].ToString();
            ddldepartamento.SelectedValue = dsData.Tables[0].Rows[0][10].ToString();
            ddlcargo.SelectedValue = dsData.Tables[0].Rows[0][11].ToString();
            ddltipofun.SelectedValue = dsData.Tables[0].Rows[0][12].ToString();
            ddllugar.SelectedValue = dsData.Tables[0].Rows[0][13].ToString();
            txtobserva.Text = dsData.Tables[0].Rows[0][14].ToString();
            foto = dsData.Tables[0].Rows[0][15].ToString();
            chkestado.Text = dsData.Tables[0].Rows[0][16].ToString() == "True" ? "Activo" : "Inactivo";
            chkestado.Checked = dsData.Tables[0].Rows[0][16].ToString() == "True" ? true : false;
            Session["grabohuella"] = dsData.Tables[0].Rows[0][18].ToString();
            chkautoriza.Checked = dsData.Tables[0].Rows[0][19].ToString() == "SI" ? true : false;
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
        Session["tabla"] = "Funcionario";
        Session["redirec"] = "7";
        Session["codigotempVisitante"] = txtcodigo.Text;
        Session["capturar"]="S";
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
        Session["capturar"] = null;
        Session["verificar"] = null;
        Session["grabohuella"] = null;
        Session["fotoantigua"] = null;
        Response.Redirect("frmfuncionaAdmin.aspx");
    }

    protected void btngrabar_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["grabohuella"] == null) Session["grabohuella"] = "N";
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
        objparam[16] = txtobserva.Text.ToUpper();
        objparam[17] = chkestado.Checked;
        objparam[18] = Session["Imagename"] == null ? Session["fotoantigua"].ToString() : Session["Imagename"].ToString();
        objparam[19] = Session["grabohuella"].ToString();
        objparam[20] = Session["usuCodigo"].ToString();
        objparam[21] = Session["MachineName"].ToString();
        objparam[22] = chkautoriza.Checked == true ? "SI" : "NO";
        dsData = fun.consultarDatos("spModiFunciInterno", objparam, Page, (String[])Session["constrring"]);
        Session["Imagename"] = null;
        Session["capturar"] = null;
        Session["verificar"] = null;
        Session["grabohuella"] = null;
        Session["fotoantigua"] = null;
        Response.Redirect("frmfuncionaAdmin.aspx?mensajeRetornado='Guardado Con éxito'", false);
    }

    protected void chkestado_CheckedChanged(object sender, EventArgs e)
    {
        chkestado.Text = chkestado.Checked == true ? "Activo" : "Inactivo";
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