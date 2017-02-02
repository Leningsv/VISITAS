using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class SalidaVisita_frmsalidavisNew : System.Web.UI.Page
{
    #region Variables
    DataSet dsData = new DataSet();
    Object[] objparam = new Object[1];
    Funciones fun = new Funciones();
    //String ruta = @"C:\Temp\huellas.dat";
    String strObt = "";
    String foto = "";
    #endregion

    #region Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                lbltitulo.Text = "verificación de datos";

                if (Request["mensajeRetornado"] != null)
                {
                    SIFunBasicas.Basicas.PresentarMensaje(Page, "::VISITA PPL::", Request["MensajeRetornado"]);
                }

                Array.Resize(ref objparam, 2);
                objparam[0] = 0;
                objparam[1] = "4";
                fun.cargarCombos(ddltipodoc, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
                ddltipodoc.Items.RemoveAt(0);

                objparam[0] = 0;
                objparam[1] = "8";
                if (Session["tabla"].ToString() == "V") strObt = fun.ObtenerRutas(Page, "spCargarParametros", objparam, (String[])Session["constrring"]);
                if (Session["tabla"].ToString() == "F") strObt = fun.ObtenerRutasFun(Page, "spCargarParametros", objparam, (String[])Session["constrring"]);
                
                String[] session = strObt.Split('|');
                Session["rutafoto"] = session[0].ToString();
                Session["rutahuella"] = session[1].ToString();

                objparam[0] = 0;
                objparam[1] = "17";
                fun.cargarCombos(ddlgenero, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
                ddlgenero.Items.RemoveAt(0);
                //CONSULTAR QUE ETAPA ESTA EL PPL

                //TRAER SI SE VERIFICA LA HUELLA POR ETAPA
                //Array.Resize(ref objparam, 1);
                //objparam[0] = Session["Codigo_PPL"].ToString();
                //dsData = fun.consultarDatos("spCarEtapaPPL", objparam, Page, (String[])Session["constrring"]);
                //String etapa = dsData.Tables[0].Rows[0][0].ToString();

                //Array.Resize(ref objparam, 2);
                //objparam[0] = "23";
                //objparam[1] = etapa;
                //dsData = fun.consultarDatos("spCargMenorEtapa", objparam, Page, (String[])Session["constrring"]);
                //Session["verificarHuella"] = dsData.Tables[0].Rows[0][0].ToString();
                //if (dsData.Tables[0].Rows[0][0].ToString() == "SI") btnverifica.Visible = true;

                funCargaMantenimiento(Session["CodVisitante"].ToString(), Session["tabla"].ToString());

            }
        }
        catch (Exception ex)
        {
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", ex.Message);
        }
    }
    #endregion

    #region Funciones y Procedimientos
    protected void funCargaMantenimiento(String strCodigoVis, String strTipo)
    {

        String rutasrv = Server.MapPath(Session["rutafoto"].ToString());
        Array.Resize(ref objparam, 1);
        objparam[0] = strCodigoVis;
        if (strTipo == "V")
        {
            dsData = fun.consultarDatos("spCargaDatosVisitante", objparam, Page, (String[])Session["constrring"]);
            if (dsData.Tables[0].Rows.Count > 0)
            {
                ddltipodoc.SelectedValue = dsData.Tables[0].Rows[0][0].ToString();
                txtndocu.Text = dsData.Tables[0].Rows[0][1].ToString();
                txtapellido1.Text = dsData.Tables[0].Rows[0][2].ToString();
                txtapellido2.Text = dsData.Tables[0].Rows[0][3].ToString();
                txtnombre1.Text = dsData.Tables[0].Rows[0][4].ToString();
                txtnombre2.Text = dsData.Tables[0].Rows[0][5].ToString();
                txtobserva.Text = dsData.Tables[0].Rows[0][6].ToString();
                foto = dsData.Tables[0].Rows[0][7].ToString();
                ddlgenero.SelectedValue = dsData.Tables[0].Rows[0][8].ToString();

                objparam[0] = Session["Codigo_PPL"].ToString();
                dsData = fun.consultarDatos("spCargPPLDatos", objparam, Page, (String[])Session["constrring"]);
                txtppl.Text = dsData.Tables[0].Rows[0][1].ToString();
            }
        }
        if (strTipo == "F")
        {
            btnsancionar.Visible = false;
            dsData = fun.consultarDatos("spCargaFuncioVeriHuella", objparam, Page, (String[])Session["constrring"]);
            if (dsData.Tables[0].Rows.Count > 0)
            {
                ddltipodoc.SelectedValue = dsData.Tables[0].Rows[0][0].ToString();
                txtndocu.Text = dsData.Tables[0].Rows[0][1].ToString();
                txtnombre1.Text = dsData.Tables[0].Rows[0][2].ToString();
                ddlgenero.SelectedValue = dsData.Tables[0].Rows[0][3].ToString();
                txtobserva.Text = Session["Codigo_PPL"].ToString(); //dsData.Tables[0].Rows[0][4].ToString();
                foto = dsData.Tables[0].Rows[0][5].ToString();
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
            else
            {
                imgfoto.ImageUrl = "~/Images/sinfoto.jpg";
            }
        }

    }
    #endregion

    #region Botones y Eventos
    protected void btngrabar_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["grabohuella"] == null) Session["grabohuella"] = "N";
        //if (Session["verificarHuella"].ToString() == "SI")
        //{            
        //    if (Session["grabohuella"].ToString() == "N")
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Verifique las Huellas del visitante');", true);
        //        return;
        //    }
        //}
        Session["grabohuella"] = null;
        Session["redirec"] = null;
        Session["capturar"] = null;
        Session["verificar"] = null;
        if (Session["tabla"].ToString() == "V")
        {
            Array.Resize(ref objparam, 5);
            objparam[0] = Session["CodVisitante"].ToString();
            objparam[1] = Session["Codigo_PPL"].ToString();
            objparam[2] = Session["Codigo_Visita"].ToString();
            objparam[3] = Session["usuCodigo"].ToString();
            objparam[4] = Session["MachineName"].ToString();
            dsData = fun.consultarDatos("spInsSalidaVis", objparam, Page, (String[])Session["constrring"]);
        }
        if (Session["tabla"].ToString() == "F")
        {
            Array.Resize(ref objparam, 2);
            objparam[0] = Session["CodVisitante"].ToString();
            objparam[1] = Session["Codigo_Visita"].ToString();
            dsData = fun.consultarDatos("spUpdateSalidaFun", objparam, Page, (String[])Session["constrring"]);
        }
        String script = "<script language='javascript'>CerrarSolo();</script>";
        ClientScript.RegisterStartupScript(GetType(), "pop", script);
    }
    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        Session["codigotempVisitante"] = null;
        Session["grabohuella"] = null;
        Session["redirec"] = null;
        Session["capturar"] = null;
        Session["verificar"] = null;
        String script = "<script language='javascript'>CerrarSolo();</script>";
        ClientScript.RegisterStartupScript(GetType(), "pop", script);
    }
    protected void btnsancionar_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Sancion/frmsancionarVisitante.aspx");
    }
    //protected void btnverifica_Click(object sender, ImageClickEventArgs e)
    //{
    //    Session["codigotempVisitante"] = Session["CodVisitante"].ToString();
    //    Session["tabla"] = "Visitante";
    //    Session["redirec"] = "14";
    //    Session["capturar"] = "N";
    //    Session["verificar"] = "S";
    //    //String pagina = "../VisitaPPL/CapturaHuella.aspx";
    //    //String newpag = "window.open('" + pagina + "', 'popup_window', 'width=950,height=600,left=50,top=50,resizable=yes');";
    //    //ClientScript.RegisterStartupScript(GetType(), "pop", newpag, true);
    //    Response.Redirect("../VisitaPPL/CapturaHuella.aspx");
    //}
    #endregion
}