using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Policias_frmaprPoliciaEdit : System.Web.UI.Page
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
                lbltitulo.Text = "Aprobación de Ingreso de Policía";

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

                objparam[0] = 0;
                objparam[1] = "40";//rangos
                fun.cargarCombos(ddlRango, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
                ddlRango.Items.RemoveAt(0);

                objparam[0] = 0;
                objparam[1] = "41";//area asignada
                fun.cargarCombos(ddlArea, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
                ddlArea.Items.RemoveAt(0);

                Array.Resize(ref objparam, 1);
                objparam[0] = 0;
                fun.cargarCombos(ddlAcceso, "spCargarAccesos", objparam, Page, (String[])Session["constrring"]);
                ddlAcceso.Items.RemoveAt(0);

                if (Request["mensajeRetornado"] != null)
                    SIFunBasicas.Basicas.PresentarMensaje(Page, "::VISITA PPL::", Request["MensajeRetornado"]);

                funCargaMantenimiento(Request["CodPol"].ToString());

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
        btngrabar.Visible = true;
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

    protected void btngrabar_Click(object sender, ImageClickEventArgs e)
    {
        string estado = "AP";

        Array.Resize(ref objparam, 5);
        objparam[0] = Request["CodPol"].ToString();
        objparam[1] = ddlAcceso.SelectedValue;
        objparam[2] = estado;
        objparam[3] = Session["usuCodigo"].ToString();
        objparam[4] = txtObservacion.Text;
        dsData = fun.consultarDatos("spAprobarPolicia", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows[0][0].ToString() != "OK")
        {
            lblmsj1.Text = "Error al aprobar policia";
            lblmsj1.Visible = true;
            return;
        }
        //inicio - genera el evento
        Array.Resize(ref objparam, 7);
        objparam[0] = fun.SecuencialSiguiente("117", (String[])Session["constrring"]);
        objparam[1] = Request["CodPol"].ToString();
        objparam[2] = ddlAcceso.SelectedValue;
        objparam[3] = txtFechaIng.Text;
        objparam[4] = txtFechaSal.Text;
        objparam[5] = Session["usuCodigo"].ToString();
        objparam[6] = Session["MachineName"].ToString();
        dsData = fun.consultarDatos("spInsEventoPolicia", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows[0][0].ToString() != "OK")
        {
            lblmsj1.Text = "error al insertar evento";
            lblmsj1.Visible = true;
            return;
        }
        Response.Redirect("frmaprPoliciaAdmin.aspx?mensajeRetornado='Guardado con Éxito'");
    }

    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("frmaprPoliciaAdmin.aspx");
    }

    protected void btnrefrescar_Click(object sender, ImageClickEventArgs e)
    {
        RefrescarFoto(Request["CodPol"].ToString());
    }

    protected void btnRechazar_Click(object sender, ImageClickEventArgs e)
    {
        string estado = "PI";

        if (txtObservacion.Text.Trim() == "")
        {
            lblmsj1.Visible = true;
            lblmsj1.Text = "Es necesario ingresar una observacion";
            return;
        }

        Array.Resize(ref objparam, 5);
        objparam[0] = Request["CodPol"].ToString();
        objparam[1] = "";//acceso
        objparam[2] = estado;
        objparam[3] = Session["usuCodigo"].ToString();
        objparam[4] = txtObservacion.Text;
        dsData = fun.consultarDatos("spAprobarPolicia", objparam, Page, (String[])Session["constrring"]);
        switch (dsData.Tables[0].Rows[0][0].ToString())
        {
            case "Existe":
                lblmsj1.Text = "Policía ya existe registrado con el número de documento";
                lblmsj1.Visible = true;
                return;
        }

        Response.Redirect("frmaprPoliciaAdmin.aspx?mensajeRetornado='Guardado con Éxito'");
    }

    protected void btVerEquipos_Click(object sender, EventArgs e)
    {
        string codAcceso = ddlAcceso.SelectedValue;
        string nomAcceso = ddlAcceso.SelectedItem.Text;
        ScriptManager.RegisterStartupScript(this.UpdatePanel1, GetType(), "Lista de Equipos", "javascript: var posicion_x; var posicion_y; posicion_x=(screen.width/2)-(900/2); posicion_y=(screen.height/2)-(600/2); window.open('frmListaEquipos.aspx?codAcceso=" + codAcceso + "&NombreAcceso=" + nomAcceso + "',null,'left=' + posicion_x + ', top=' + posicion_y + ', width=900px, height=600px, status=no,resizable= yes, scrollbars=yes, toolbar=no, location=no, menubar=no');", true);
    }
    #endregion
    
}