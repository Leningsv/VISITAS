using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;

public partial class PPL_frmvisitaRelacion : System.Web.UI.Page
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
                lblerror.Visible = false;
                lbltitulo.Text = "Relación Visitante - PPL";
                Array.Resize(ref objparam, 2);
                objparam[0] = 0;
                objparam[1] = "4";
                fun.cargarCombos(ddltipodoc, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
                ddltipodoc.Items.RemoveAt(0);

                if (Request["pplCodigo"] != null)
                {
                    Session["CodigoPPL"] = Request["pplCodigo"].ToString();
                }
                if (Session["NuevoVisitante"] != null)
                {
                    funAgregarNewVisita(Session["NuevoVisitante"].ToString());
                }

                funCargaMantenimiento(Session["CodigoPPL"].ToString());
                if (Request["mensajeRetornado"] != null)
                {
                    SIFunBasicas.Basicas.PresentarMensaje(Page, "::VISITA PPL::", Request["MensajeRetornado"]);
                }
                imprimirActa(Session["CodigoPPL"].ToString());
            }
        }
        catch (Exception ex)
        {
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", ex.Message);
        }
    }
    #endregion

    #region Funciones y Procedimientos
    protected void funCargaMantenimiento(String strCodPPl)
    {
        String strEtapa = "";
        Array.Resize(ref objparam, 1);
        objparam[0] = strCodPPl;
        dsData = fun.consultarDatos("spCarIndiDatosPPL", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0)
        {
            ddltipodoc.SelectedValue = dsData.Tables[0].Rows[0][0].ToString();
            txtnumdoc.Text = dsData.Tables[0].Rows[0][1].ToString();
            txtnombres.Text = dsData.Tables[0].Rows[0][2].ToString() + ' ' + dsData.Tables[0].Rows[0][3].ToString(); ;
            txtapellidos.Text = dsData.Tables[0].Rows[0][4].ToString() + ' ' + dsData.Tables[0].Rows[0][5].ToString();
            txtetapa.Text = dsData.Tables[0].Rows[0][6].ToString();
            txtpabellon.Text = dsData.Tables[0].Rows[0][7].ToString();
            strEtapa = dsData.Tables[0].Rows[0][8].ToString();
            Session["nomppl"] = dsData.Tables[0].Rows[0][9].ToString();
        }

        Array.Resize(ref objparam, 2);
        objparam[0] = "16";
        objparam[1] = strEtapa;
        dsData = fun.consultarDatos("spCargParamRela", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0)
        {
            Session["cantifam"] = dsData.Tables[0].Rows[0][0].ToString();
            lblmsj1.Text = "Cantidad máxima visitas Familiares " + Session["cantifam"].ToString();
        }
        if (Session["cantifam"] == null)
        {
            Session["cantifam"] = 1;

        }

        Array.Resize(ref objparam, 2);
        objparam[0] = "25";
        objparam[1] = strEtapa;
        dsData = fun.consultarDatos("spCargParamRela", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0)
        {
            Session["canticon"] = dsData.Tables[0].Rows[0][0].ToString();
            lblmsj2.Text = "Cantidad máxima visitas Conyugales " + Session["canticon"].ToString();
        }

        Array.Resize(ref objparam, 2);
        objparam[0] = "38";
        objparam[1] = strEtapa;
        dsData = fun.consultarDatos("spCargParamRela", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0)
        {
            Session["cantilegal"] = dsData.Tables[0].Rows[0][0].ToString();
            lblmsj3.Text = "Cantidad máxima visitas Legales " + Session["cantilegal"].ToString();
        }

        Array.Resize(ref objparam, 3);
        objparam[0] = "";
        objparam[1] = strCodPPl;
        objparam[2] = "TODOS";
        dsData = fun.consultarDatos("spSelVisitanteEmpleado", objparam, Page, (String[])Session["constrring"]);
        grdvDatos.DataSource = dsData;
        grdvDatos.DataBind();
        Session["grdvDatos"] = grdvDatos.DataSource;
    }

    private void funLlenarCombos(String strCodigo, DropDownList ddltipovis, DropDownList ddlparente, String codtvisita, String codparent)
    {
        Array.Resize(ref objparam, 2);
        objparam[0] = 0;
        objparam[1] = "15";
        fun.cargarCombos(ddlparente, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
        ddlparente.Items.RemoveAt(0);
        ddlparente.SelectedValue = codparent;

        objparam[0] = 0;
        objparam[1] = "14";
        fun.cargarCombos(ddltipovis, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
        ddltipovis.Items.RemoveAt(0);
        ddltipovis.SelectedValue = codtvisita;
    }

    private void funGrabarComboVisita(String strCodVis, DropDownList ddltipvisita, DropDownList ddlparent)
    {
        ddlparent.SelectedValue = ddltipvisita.SelectedValue == "C" ? "C" : ddlparent.SelectedValue;
        ddlparent.Enabled = true;
        Array.Resize(ref objparam, 7);
        objparam[0] = Session["CodigoPPL"].ToString();
        objparam[1] = strCodVis;
        objparam[2] = ddltipvisita.SelectedValue;
        objparam[3] = ddlparent.SelectedValue;
        objparam[4] = int.Parse(Session["cantifam"].ToString());
        objparam[5] = int.Parse(Session["canticon"].ToString());
        objparam[6] = 0;
        dsData = fun.consultarDatos("spCamRelaciVisPPL", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows[0][0].ToString() != "Update") ddltipvisita.SelectedValue = dsData.Tables[0].Rows[0][0].ToString();
        if (ddltipvisita.SelectedValue == "C")
        {
            ddlparent.SelectedValue = "C";
            ddlparent.Enabled = false;
        }
    }

    private void funGrabarComboParentes(String strCodVis, DropDownList ddlparent, DropDownList ddltipvisita)
    {
        int intCantidad = 0;
        String strCodTipoVis = "", strCodpareanterior = "";
        //TRAER LA CANTIDAD DE CADA TIPO DE VISITA Y VALIDAR
        Array.Resize(ref objparam, 3);
        objparam[0] = Session["CodigoPPL"].ToString();
        objparam[1] = strCodVis;
        objparam[2] = ddlparent.SelectedValue;
        dsData = fun.consultarDatos("spCargParenRelaTipVis", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0)
        {
            intCantidad = int.Parse(dsData.Tables[0].Rows[0][0].ToString());
            strCodTipoVis = dsData.Tables[0].Rows[0][1].ToString();
            strCodpareanterior = dsData.Tables[0].Rows[0][2].ToString();
        }
        if (strCodTipoVis == "M")
        {
            if (intCantidad == int.Parse(Session["cantifam"].ToString()))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('A superado la cantidad máxima de visitas familiares');", true);
                ddlparent.SelectedValue = strCodpareanterior;
            }
        }
        if (strCodTipoVis == "C")
        {
            if (intCantidad == int.Parse(Session["canticon"].ToString()))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('A superado la cantidad máxima de visitas conyugales');", true);
                ddlparent.SelectedValue = strCodpareanterior;
            }
        }
        if (strCodTipoVis == "A")
        {
            if (intCantidad == int.Parse(Session["cantilegal"].ToString()))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('A superado la cantidad máxima de visitas legales');", true);
                ddlparent.SelectedValue = strCodpareanterior;
            }
        }
        //Traer el tipo de parentesco
        Array.Resize(ref objparam, 1);
        objparam[0] = ddlparent.SelectedValue;
        dsData = fun.consultarDatos("spCarTipoRelaParen", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0) ddltipvisita.SelectedValue = dsData.Tables[0].Rows[0][0].ToString();
        else ddltipvisita.SelectedValue = "M";

        Array.Resize(ref objparam, 4);
        objparam[0] = Session["CodigoPPL"].ToString();
        objparam[1] = strCodVis;
        objparam[2] = ddltipvisita.SelectedValue;
        objparam[3] = ddlparent.SelectedValue;
        dsData = fun.consultarDatos("spCambiRelaVisPPL", objparam, Page, (String[])Session["constrring"]);
    }

    private void funAgregarNewVisita(String strCodigoVisita)
    {
        if (Request["verificavis"] != null)
        {
            if (Request["verificavis"].ToString() == "SI")
            {
                Array.Resize(ref objparam, 4);
                objparam[0] = Session["CodigoPPL"].ToString();
                objparam[1] = strCodigoVisita;
                objparam[2] = Session["usuCodigo"].ToString();
                objparam[3] = Session["MachineName"].ToString();
                dsData = fun.consultarDatos("spInserRelaciVistaAdmin", objparam, Page, (String[])Session["constrring"]);
                if (dsData.Tables[0].Rows[0][0].ToString() == "Existe")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('El Visitante ya está relacionado con el PPL');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Relación Creada');", true);
                }
            }
        }

        Session["NuevoVisitante"] = null;
    }
    #endregion

    #region Botones y Eventos
    protected void grdvDatos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DropDownList ddlTipVisita = new DropDownList();
        DropDownList ddlTipParent = new DropDownList();

        if (e.Row.RowIndex >= 0)
        {
            ddlTipVisita = (DropDownList)(e.Row.Cells[4].FindControl("ddltipovisita"));
            ddlTipParent = (DropDownList)(e.Row.Cells[5].FindControl("ddltipoparent"));

            String strCodigo = grdvDatos.DataKeys[e.Row.RowIndex].Value.ToString();
            String strTipoVis = grdvDatos.DataKeys[e.Row.RowIndex].Values["TipoVisita"].ToString();
            String strParente = grdvDatos.DataKeys[e.Row.RowIndex].Values["Parentesco"].ToString();
            funLlenarCombos(strCodigo, ddlTipVisita, ddlTipParent, strTipoVis, strParente);
        }
    }
    protected void ddltipoparent_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList cboparentes = new DropDownList();
        DropDownList cbotipovist = new DropDownList();
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        int intIndex = gvRow.RowIndex;
        String strCodigovis = grdvDatos.DataKeys[intIndex].Values["Codigo"].ToString();
        cboparentes = (DropDownList)(grdvDatos.Rows[intIndex].Cells[4].FindControl("ddltipoparent"));
        cbotipovist = (DropDownList)(grdvDatos.Rows[intIndex].Cells[5].FindControl("ddltipovisita"));
        funGrabarComboParentes(strCodigovis, cboparentes, cbotipovist);
    }
    protected void btnagregar_Click(object sender, ImageClickEventArgs e)
    {
       
       
        Array.Resize(ref objparam, 2);
        objparam[0] = Session["CodigoPPL"].ToString();
        objparam[1] = int.Parse(Session["cantifam"].ToString());
        dsData = fun.consultarDatos("spContFamAsignado", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows[0][0].ToString() == "Maxima")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('A superado la cantidad máxima de familiares');", true);
            return;
        }
        lblerror.Visible = false;
        ScriptManager.RegisterStartupScript(this.UpdatePanel1, GetType(), "Editar-Visitante", "javascript: var posicion_x; var posicion_y; posicion_x=(screen.width/2)-(900/2); posicion_y=(screen.height/2)-(600/2); window.open('frmvisitarelaAdmin.aspx',null,'left=' + posicion_x + ', top=' + posicion_y + ', width=1024px, height=600px, status=no,resizable= yes, scrollbars=yes, toolbar=no, location=no, menubar=no');", true);
    }
    protected void btnsel_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        int intIndex = gvRow.RowIndex;

        Session["codvisitante"] = grdvDatos.DataKeys[intIndex].Values["Codigo"];
        ScriptManager.RegisterStartupScript(this.UpdatePanel1, GetType(), "Editar-Visitante", "javascript: var posicion_x; var posicion_y; posicion_x=(screen.width/2)-(900/2); posicion_y=(screen.height/2)-(450/2); window.open('frmvisitaPPLEdit.aspx',null,'left=' + posicion_x + ', top=' + posicion_y + ', width=1024px, height=450px, status=no,resizable= yes, scrollbars=yes, toolbar=no, location=no, menubar=no');", true);
    }
    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        Session["CodigoPPL"] = null;
        Session["nomppl"] = null;
        Session["NuevoVisitante"] = null;
        Response.Redirect("~/Index1.aspx");
    }
    private void imprimirActa(String strCodPPlRep)
    {
        try
        {
            lblerror.Visible = false;

            Array.Resize(ref objparam, 1);
            objparam[0] = strCodPPlRep;
            dsData = fun.consultarDatos("spRepVisitasPPL", objparam, Page, (String[])Session["constrring"]);
            if (dsData.Tables[0].Rows.Count > 0)
            {
                ReportViewer1.Visible = true;
                ReportDataSource dataSourceReporte1 = new ReportDataSource("DataSet1", ObtenerLista(dsData));
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(dataSourceReporte1);
                ReportViewer1.LocalReport.Refresh();
            }
            else
            {
                lblerror.Visible = true;
                lblerror.Text = "No existen datos para la busqueda seleccionada";
                ReportViewer1.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblerror.Visible = true;
            lblerror.Text = "Error: " + ex.Message;
        }
    }

    private object ObtenerLista(DataSet DSconsulta)
    {
        dsReporteVisitas.dtReporteVisitasDataTable dtReporte1 = new dsReporteVisitas.dtReporteVisitasDataTable();
        int intCont = 0;
        while (intCont < DSconsulta.Tables[0].Rows.Count)
        {
            dsReporteVisitas.dtReporteVisitasRow row = dtReporte1.NewdtReporteVisitasRow();
            row.codigo = DSconsulta.Tables[0].Rows[intCont][1].ToString();
            row.titulo = DSconsulta.Tables[0].Rows[intCont][2].ToString();
            row.linea1 = DSconsulta.Tables[0].Rows[intCont][3].ToString();
            row.nombreppl = DSconsulta.Tables[0].Rows[intCont][4].ToString();
            row.linea2 = DSconsulta.Tables[0].Rows[intCont][5].ToString();
            row.linea3 = DSconsulta.Tables[0].Rows[intCont][6].ToString();
            row.pabellon = DSconsulta.Tables[0].Rows[intCont][8].ToString();
            row.ala = DSconsulta.Tables[0].Rows[intCont][9].ToString();
            row.piso = DSconsulta.Tables[0].Rows[intCont][10].ToString();
            row.celda = DSconsulta.Tables[0].Rows[intCont][11].ToString();
            row.fechaentrega = DSconsulta.Tables[0].Rows[intCont][12].ToString();
            row.tipo = DSconsulta.Tables[0].Rows[intCont][15].ToString();
            row.nombre = DSconsulta.Tables[0].Rows[intCont][17].ToString();
            row.apellido = DSconsulta.Tables[0].Rows[intCont][17].ToString();
            row.parentesco = DSconsulta.Tables[0].Rows[intCont][16].ToString();
            dtReporte1.Rows.Add(row);
            intCont++;
        }

        return dtReporte1;
    }
    
    #endregion

}