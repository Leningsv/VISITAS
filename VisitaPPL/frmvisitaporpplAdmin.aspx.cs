using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VisitaPPL_frmvisitaporpplAdmin : System.Web.UI.Page
{
    #region Variables
    DataSet dsData = new DataSet();
    DataSet dsData_1 = new DataSet();
    Object[] objparam = new Object[1];
    Funciones fun = new Funciones();
    #endregion

    #region Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["codigotempVisitante"] = null;
                Session["grabohuella"] = null;
                ////TRAER LAS POLITICAS DE INGRESO DE VISITA
                Array.Resize(ref objparam, 2);
                objparam[0] = 0;
                objparam[1] = "20";
                String strObt = fun.PoliticasIngresoVisita(Page, "spCargarParametros", objparam, (String[])Session["constrring"]);
                String[] session = strObt.Split('|');
                Session["ssingvis"] = session[0].ToString();
                Session["ssvalcedu"] = session[1].ToString();
                Session["ssdocusol"] = session[2].ToString();

                //OBTENER LOS PARAMETROS PARA CONECTARSE AL ESIGPEN
                Session["conectar"] = "SI";
                Array.Resize(ref objparam, 2);
                objparam[0] = "153";//PARAMETROS DE CONEXION ESIGPEN
                objparam[1] = "43";
                dsData = fun.consultarDatos("spCargaTotalParaDeta", objparam, Page, (String[])Session["constrring"]);
                if (dsData.Tables[0].Rows[0][0].ToString() == "NO") Session["conectar"] = "NO";

                Array.Resize(ref objparam, 2);
                objparam[0] = "147";//PARAMETROS DE CONEXION ESIGPEN
                objparam[1] = "46";
                dsData = fun.consultarDatos("spCargaTotalParaDeta", objparam, Page, (String[])Session["constrring"]);
                Session["basedatos"] = dsData.Tables[0].Rows[0][0].ToString();

                Array.Resize(ref objparam, 2);
                objparam[0] = "148";//PARAMETROS DE CONEXION ESIGPEN
                objparam[1] = "46";
                dsData = fun.consultarDatos("spCargaTotalParaDeta", objparam, Page, (String[])Session["constrring"]);
                Session["useresigpen"] = dsData.Tables[0].Rows[0][0].ToString();

                Array.Resize(ref objparam, 2);
                objparam[0] = "149";//PARAMETROS DE CONEXION ESIGPEN
                objparam[1] = "46";
                dsData = fun.consultarDatos("spCargaTotalParaDeta", objparam, Page, (String[])Session["constrring"]);
                Session["pass"] = dsData.Tables[0].Rows[0][0].ToString();

                lbltitulo.Text = "Ingreso de Visitas";
                lbletiqueta.Text = "Lista de Visitantes";
                
                funCargarpost(1);

                if (Request["mensajeRetornado"] != null)
                {
                    SIFunBasicas.Basicas.PresentarMensaje(Page, "::VISITA PPL::", Request["MensajeRetornado"]);
                }
            }
            else
            {
                if (grdvDatos.Visible == true)
                {
                    grdvDatos.DataSource = Session["grdvDatos"];
                    ctrlbuscar.GrdGrillaBusqueda = grdvDatos;
                }
                if (grdvPPL.Visible == true)
                {
                    grdvPPL.DataSource = Session["grdvPPL"];
                    ctrlbuscar.GrdGrillaBusqueda = grdvPPL;
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
    protected void funCargarpost(Int16 intTipo)
    {
        Array.Resize(ref objparam, 1);
        objparam[0] = intTipo;
        dsData = fun.consultarDatos("spCargarVisitaPPL", objparam, Page, (String[])Session["constrring"]);
        if (intTipo == 0)//por visitante
        {
            tblbuscador.Visible = true;
            tblbusppl.Visible = false;
            grdvDatos.DataSource = dsData;
            grdvDatos.DataBind();
            Session["grdvDatos"] = grdvDatos.DataSource;
            ctrlbuscar.GrdGrillaBusqueda = grdvDatos;
            ctrlbuscar.CargarComponente();
        }
        else//por ppl
        {
            Array.Resize(ref objparam, 1);
            objparam[0] = 0;
            dsData = fun.consultarDatos("spPPLReadAll", objparam, Page, (String[])Session["constrring"]);
            tblbuscador.Visible = false;
            tblbusppl.Visible = true;
            tblvisitante.Visible = false;
            tblppl.Visible = true;
            grdvDatos.DataSource = null;
            grdvDatos.DataBind();
            grdvPPL.DataSource = dsData;
            grdvPPL.DataBind();
            Session["grdvPPL"] = grdvPPL.DataSource;
            ctrlbuscar.GrdGrillaBusqueda = grdvPPL;
            ctrlbuscar.CargarComponente();
            
            /*
            //ESTO SE DESCOMENTO PARA VALIDAR CONECTIVIDAD SB 20/08/2015
            //LLAMAR AL FUNCION PARA CARGAR PPL DESDE EL ESIGPEN
            Object[] condicion = { "name", "like", "%" };
            Object[] campos = { "name", "last_name", "etapa_id", "pabellon_id", "ala_id", "piso_id", "location_id" };
            //Object[] campos = { "nombre_ppl", "apellidos_ppl", "etapa_id", "pabellon_id", "ala_id" };
            //dsData_1 = fun.PPL_Datos_ESIGPEN("esigpen_test09", "admin", "admin@esigpen", "prison.person", condicion, campos, Session["usuCodigo"].ToString(), Session["MachineName"].ToString(), Page, (String[])Session["constrring"]);
            
            //dsData_1 = fun.PPL_Datos_ESIGPEN("sgp", "admin", "admin@sgp@2015", "prison.person", condicion, campos, Session["usuCodigo"].ToString(), Session["MachineName"].ToString(), Page, (String[])Session["constrring"]);

            //dsData_1 = fun.PPL_Datos_ESIGPEN("sgp", "esiseg", "Justicia2016", "ppl.ubicacion", condicion, campos, Session["usuCodigo"].ToString(), Session["MachineName"].ToString(), Page, (String[])Session["constrring"]);
            dsData_1 = fun.PPL_Datos_ESIGPEN("sgp", "esiseg", "Justicia2016", "prison.person", condicion, campos, Session["usuCodigo"].ToString(), Session["MachineName"].ToString(), Page, (String[])Session["constrring"]);
            tblbuscador.Visible = false;
            tblbusppl.Visible = true;
            tblvisitante.Visible = false;
            tblppl.Visible = true;
            grdvDatos.DataSource = null;
            grdvDatos.DataBind();
            grdvPPL.DataSource = dsData;
            grdvPPL.DataBind();
            Session["grdvPPL"] = grdvPPL.DataSource;
            ctrlbuscar.GrdGrillaBusqueda = grdvPPL;
            ctrlbuscar.CargarComponente();
            */
        }
    }

    #endregion

    #region Botones y Eventos
    protected void rbtvisitante_CheckedChanged(object sender, EventArgs e)
    {
        if (rbtvisitante.Checked == true)
        {
            tblppl.Visible = false;
            tblvisitante.Visible = true;
            grdvPPL.DataSource = null;
            grdvPPL.DataBind();
            funCargarpost(0);
            rdbppl.Checked = false;
            lbletiqueta.Text = "Lista de Visitantes";
        }
    }

    protected void rdbppl_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbppl.Checked == true)
        {
            tblvisitante.Visible = false;
            tblppl.Visible = true;
            grdvDatos.DataSource = null;
            grdvDatos.DataBind();
            funCargarpost(1);
            rbtvisitante.Checked = false;
            lbletiqueta.Text = "Lista de PPL";
        }
    }

    protected void grdvDatos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdvDatos.PageIndex = e.NewPageIndex;
        ctrlbuscar.GrdGrillaBusqueda = grdvDatos;
        grdvDatos.DataBind();
    }

    protected void grdvPPL_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdvPPL.PageIndex = e.NewPageIndex;
        ctrlbuscar.GrdGrillaBusqueda = grdvPPL;
        grdvPPL.DataBind();
    }

    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Index1.aspx");
    }

    protected void btningreso_Click(object sender, ImageClickEventArgs e)
    {

    }
    
    protected void btnbuscar_Click(object sender, ImageClickEventArgs e)
    {
        Session["conectar"] = "NO"; //para forzar siempre la conexion al esigpen
        if (Session["conectar"].ToString() == "SI")
        {
            switch (ddlseleccionar.SelectedValue)
            {
                case "NO":
                    if (txtnombres.Text != "")
                    {
                        Object[] condicion = { "last_name", "like", txtnombres.Text.ToUpper() + "%" };
                        Object[] campos = { "name", "last_name", "etapa_id", "pabellon_id", "ala_id" };
                        dsData_1 = fun.PPL_Datos_ESIGPEN(Session["basedatos"].ToString(), Session["useresigpen"].ToString(), Session["pass"].ToString(), "prison.person", condicion, campos, Session["usuCodigo"].ToString(), Session["MachineName"].ToString(), Page, (String[])Session["constrring"]);
                    }
                    break;
            }
        }

        Array.Resize(ref objparam, 2);
        objparam[0] = txtnombres.Text.ToUpper();
        objparam[1] = ddlseleccionar.SelectedValue;
        dsData = fun.consultarDatos("spCargEsigPPL", objparam, Page, (String[])Session["constrring"]);
        grdvPPL.DataSource = dsData;
        grdvPPL.DataBind();
        Session["grdvPPL"] = grdvPPL.DataSource;
    }
    #endregion
}