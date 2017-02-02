using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Sancion_frmsancVisitante : System.Web.UI.Page
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

                funCargaMantenimiento(Session["CodVisitante"].ToString(), "m");

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
        if (strTipo == "m")
        {
            String rutasrv = Server.MapPath(Session["rutafoto"].ToString());
            Array.Resize(ref objparam, 1);
            objparam[0] = strCodigoVis;
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

    }
    #endregion

    #region Botones y Eventos
    protected void btningresar_Click(object sender, ImageClickEventArgs e)
    {
        String strPerfil = "", strAbierto = "", strRestringido = "", strEliminar = "", strTurnoCodigo = "", strHoraDesde = "",
    strHoraHasta = "", strCodigoEve = "";

        //OBTNER PERFIL DEL PPL SEGUN LA ETAPA Y TRER LOS VALORES DEL ACCESO DEL PERFIL
        Array.Resize(ref objparam, 1);
        objparam[0] = Session["etapappl"].ToString();
        dsData = fun.consultarDatos("spCarPerfiAcceso", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0)
        {
            strPerfil = dsData.Tables[0].Rows[0][0].ToString();
            strAbierto = dsData.Tables[0].Rows[0][1].ToString();
            strRestringido = dsData.Tables[0].Rows[0][2].ToString();
            strEliminar = dsData.Tables[0].Rows[0][3].ToString();
        }
        else
        {
            lblmsj1.Text = "No existe ningún perfil activo para el visitante";
            lblmsj1.Visible = true;
            return;
        }

        ////OBTENER HORARIOS TURNOS Y LOS VALORES DEL PERFIL
        String constipoVisita = Session["tipovisita"].ToString();
        if (Session["tipovisita"].ToString() == "C") constipoVisita = "M";
        Array.Resize(ref objparam, 2);
        objparam[0] = Session["etapappl"].ToString();
        objparam[1] = constipoVisita;
        dsData = fun.consultarDatos("spCarTurnoPlanifi", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0)
        {
            strTurnoCodigo = dsData.Tables[0].Rows[0][0].ToString();
            strHoraDesde = dsData.Tables[0].Rows[0][1].ToString();
            strHoraHasta = dsData.Tables[0].Rows[0][2].ToString();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('No existe ningún turno activo para el visitante');", true);
            return;
        }

        if (constipoVisita == "M") strCodigoEve = "118";
        if (constipoVisita == "C") strCodigoEve = "119";
        if (constipoVisita == "L") strCodigoEve = "120";

        String strFechadesde = DateTime.Now.ToString("dd/MM/yyyy") + " " + strHoraDesde;
        String strFechahasta = DateTime.Now.ToString("dd/MM/yyyy") + " " + strHoraHasta;

        DateTime dtFechadesde = Convert.ToDateTime(strFechadesde);
        DateTime dtFechahasta = Convert.ToDateTime(strFechahasta);

        Array.Resize(ref objparam, 13);
        objparam[0] = Session["CodVisitante"].ToString();
        objparam[1] = Session["Codigo_Visita"].ToString();
        objparam[2] = Session["tipovisita"].ToString();
        objparam[3] = ddltipodoc.SelectedValue;
        objparam[4] = strPerfil;
        objparam[5] = strAbierto;
        objparam[6] = strRestringido;
        objparam[7] = strEliminar;
        objparam[8] = dtFechadesde;
        objparam[9] = dtFechahasta;
        objparam[10] = Session["usuCodigo"].ToString();
        objparam[11] = Session["MachineName"].ToString();
        objparam[12] = fun.SecuencialSiguiente(strCodigoEve, (String[])Session["constrring"]);
        dsData = fun.consultarDatos("spInsEntreDocu", objparam, Page, (String[])Session["constrring"]);
        ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='frmReceptaDocumento.aspx';window.close();", true);
    }
    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.close();", true);
    }
    protected void btnsancionar_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("frmsancionarVisitante.aspx");
    }
    #endregion
}