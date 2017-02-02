using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class VisitaPPL_frmingreporvisita : System.Web.UI.Page
{
    #region Variables
    DataSet dsData = new DataSet();
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

                lbltitulo.Text = "Ingreso de Visita";

                Array.Resize(ref objparam, 2);
                objparam[0] = 0;
                objparam[1] = "14";
                fun.cargarCombos(ddltipovisita, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);

                Array.Resize(ref objparam, 2);
                objparam[0] = 0;
                objparam[1] = "15";
                fun.cargarCombos(ddlparentesco, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);

                //CARGAR EL TIEMPO DE GRACIA QUE TIENE EL VISITANTE
                Session["minutosGracia"] = "0";
                Array.Resize(ref objparam, 2);
                objparam[0] = "79";
                objparam[1] = "20";
                dsData = fun.consultarDatos("spCargaTotalParaDeta", objparam, Page, (String[])Session["constrring"]);
                if (fun.IsNumber(dsData.Tables[0].Rows[0][0].ToString())) Session["minutosGracia"] = dsData.Tables[0].Rows[0][0].ToString();

                Session["minutoregistro"] = "0";
                Array.Resize(ref objparam, 2);
                objparam[0] = "145";
                objparam[1] = "20";
                dsData = fun.consultarDatos("spCargaTotalParaDeta", objparam, Page, (String[])Session["constrring"]);
                if (fun.IsNumber(dsData.Tables[0].Rows[0][0].ToString())) Session["minutoregistro"] = dsData.Tables[0].Rows[0][0].ToString();

                Session["VisCodigoin"] = Request["visCodigo"];
                funCargaMantenimiento(Request["visCodigo"]);

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
    protected void funCargaMantenimiento(String strCodigoVis)
    {

        Array.Resize(ref objparam, 1);
        objparam[0] = strCodigoVis;
        dsData = fun.consultarDatos("spCarIndiDatosVis", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0)
        {
            txtnombre.Text = dsData.Tables[0].Rows[0][0].ToString();
            Session["codvisitante"] = dsData.Tables[0].Rows[0][3].ToString();
        }


        Array.Resize(ref objparam, 1);
        objparam[0] = strCodigoVis;
        dsData = fun.consultarDatos("spCargaPPLVisitante", objparam, Page, (String[])Session["constrring"]);
        grdvDatos.DataSource = dsData;
        grdvDatos.DataBind();
        Session["grdvDatos"] = grdvDatos.DataSource;
    }
    #endregion

    #region Botones y Eventos
    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Index1.aspx");
    }
    protected void btnselect_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        int intIndex = gvRow.RowIndex;

        Array.Resize(ref objparam, 1);
        objparam[0] = Session["VisCodigoin"].ToString();
        dsData = fun.consultarDatos("spCarVisitinVisita", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows[0][0].ToString() == "P" || dsData.Tables[0].Rows[0][0].ToString() == "C")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Visitante se encuentra sancionado');", true);
            //lblerror.Text = "Visitante se encuentra sancionado";
            //lblerror.Visible = true;
            return;
        }
        Session["codpplnew"] = grdvDatos.DataKeys[intIndex].Values["Codigo"];
        Session["etapappl"] = grdvDatos.DataKeys[intIndex].Values["Eta"];

        //OBTENER CANTIDAD DE VISITANTES QUE PUEDE TENER EL PPL
        int numvisitaactual = 0;
        Array.Resize(ref objparam, 2);
        objparam[0] = "31";
        objparam[1] = Session["etapappl"].ToString();
        dsData = fun.consultarDatos("spCargMenorEtapa", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0)
        {
            if (fun.IsNumber(dsData.Tables[0].Rows[0][0].ToString()))
            {
                numvisitaactual = int.Parse(dsData.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                numvisitaactual = int.Parse("0");
            }
        }
        //CUANTOS VISITANTES ESTAN ADENTRO
        Array.Resize(ref objparam, 2);
        objparam[0] = Session["codpplnew"].ToString();
        objparam[1] = 1;
        dsData = fun.consultarDatos("spCarNumVisitasPPL", objparam, Page, (String[])Session["constrring"]);
        if (int.Parse(dsData.Tables[0].Rows[0][0].ToString()) >= numvisitaactual)
        {
            String mensaje = "El PPL solo puede recibir " + numvisitaactual.ToString() + " Visita(s) familiares";
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + mensaje + "')", true);
            //lblerror.Visible = true;
            return;
        }
        Array.Resize(ref objparam, 1);
        objparam[0] = Session["codpplnew"].ToString();
        dsData = fun.consultarDatos("spCargPlanifiVisita", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0)
        {
            Session["visitaactual"] = dsData.Tables[0].Rows[0][3].ToString();
            ddltipovisita.SelectedValue = Session["visitaactual"].ToString();
            DateTime horaIni = Convert.ToDateTime(dsData.Tables[0].Rows[0][1]);
            DateTime horaFin = Convert.ToDateTime(dsData.Tables[0].Rows[0][2]);
            DateTime horactu = DateTime.Now;
            horaIni = horaIni.AddMinutes(-(int.Parse(Session["minutoregistro"].ToString())));
            horaFin = horaFin.AddMinutes(int.Parse(Session["minutosGracia"].ToString()));
            //if (int.Parse(Session["minutosGracia"].ToString()) > 0)
            //{
            //    horaFin = horaFin.AddMinutes(int.Parse(Session["minutosGracia"].ToString()));
            //}
            if (horactu > horaFin || horactu < horaIni)
            {
                String mensaje = "La Hora de visita es de: " + horaIni.ToString("HH:mm") + " - " + horaFin.ToString("HH:mm");
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + mensaje + "')", true);
                //lblerror.Visible = true;
                return;
            }
        }
        else
        {
            //lblerror.Text = "No tiene planificada visitas para el día de hoy";
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('No tiene planificada visitas para el día de hoy');", true);
            //lblerror.Visible = true;
            return;
        }

        Array.Resize(ref objparam, 2);
        objparam[0] = Session["codpplnew"].ToString();
        objparam[1] = Session["VisCodigoin"].ToString();
        dsData = fun.consultarDatos("spCargTipoVisitaPPL", objparam, Page, (String[])Session["constrring"]);
        Session["tipovisConyugue"] = dsData.Tables[0].Rows[0][0].ToString();
        if (Session["visitaactual"].ToString() == "C" && Session["tipovisConyugue"].ToString() == "M")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Este día está planificado para visita conyugal');", true);
            //lblerror.Text = "Este día está planificado para visita conyugal";
            //lblerror.Visible = true;
            return;
        }
        if (Session["visitaactual"].ToString() == "L" && Session["tipovisConyugue"].ToString() != "L")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Este día está planificado para visita legal');", true);
            //lblerror.Text = "Este día está planificado para visita legal";
            //lblerror.Visible = true;
            return;
        }

        Array.Resize(ref objparam, 2);
        objparam[0] = "16";
        objparam[1] = Session["etapappl"].ToString();
        dsData = fun.consultarDatos("spCargParamRela", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0)
        {
            Session["cantifam"] = dsData.Tables[0].Rows[0][0].ToString();
        }
        Array.Resize(ref objparam, 2);
        objparam[0] = "25";
        objparam[1] = Session["etapappl"].ToString();
        dsData = fun.consultarDatos("spCargParamRela", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0)
        {
            Session["canticon"] = dsData.Tables[0].Rows[0][0].ToString();
        }
        //Ingresar el menor de edad por etapa
        Array.Resize(ref objparam, 2);
        objparam[0] = "29";
        objparam[1] = Session["etapappl"].ToString();
        dsData = fun.consultarDatos("spCargMenorEtapa", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0)
        {
            Session["ingmenor"] = dsData.Tables[0].Rows[0][0].ToString();
        }
        //Edad del menor de edad por etapa
        Array.Resize(ref objparam, 2);
        objparam[0] = "30";
        objparam[1] = Session["etapappl"].ToString();
        dsData = fun.consultarDatos("spCargEdadEtapa", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0)
        {
            Session["Edad"] = dsData.Tables[0].Rows[0][0].ToString();
        }
        Session["TipoVisita"] = ddltipovisita.SelectedValue;
        Session["nDocumento"] = grdvDatos.Rows[intIndex].Cells[2].Text;
        Session["parentesco"] = grdvDatos.DataKeys[intIndex].Values["Parente"];
        Session["nombrevisita"] = ddltipovisita.SelectedItem;

        //preguntar si tiene foto y huella y mandar a la pantalla frmvistpplCamaraNew
        Array.Resize(ref objparam, 1);
        objparam[0] = Session["VisCodigoin"].ToString();
        dsData = fun.consultarDatos("spCarValidaFoto", objparam, Page, (String[])Session["constrring"]);
        ScriptManager.RegisterStartupScript(this.UpdatePanel1, GetType(), "Ingreso de Visita", "javascript: var posicion_x; var posicion_y; posicion_x=(screen.width/2)-(900/2); posicion_y=(screen.height/2)-(600/2); window.open('frmvistpplCamaraNew.aspx',null,'left=' + posicion_x + ', top=' + posicion_y + ', width=1024px, height=600px, status=no,resizable= yes, scrollbars=yes, toolbar=no, location=no, menubar=no');", true);
    }
    protected void grdvDatos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdvDatos.PageIndex = e.NewPageIndex;
        grdvDatos.DataBind();
    }
    #endregion
}