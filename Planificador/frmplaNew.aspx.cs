using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Planificador_frmplaNew : System.Web.UI.Page
{
    #region Variables
    DataSet dsData = new DataSet();
    Object[] objparam = new Object[1];
    Funciones fun = new Funciones();
    DateTime DiaHoy = new DateTime();
    DateTime ffin = new DateTime();
    #endregion

    #region Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbltitulo.Text = "Agregar una nueva Planificación de Visitas";

            Array.Resize(ref objparam, 2);
            objparam[0] = "143";//OCULTAR BOTON
            objparam[1] = "45";
            dsData = fun.consultarDatos("spCargaTotalParaDeta", objparam, Page, (String[])Session["constrring"]);
            Session["mostrarboton"] = dsData.Tables[0].Rows[0][0].ToString();
        }
    }
    #endregion

    #region Botones y Eventos
    protected void btngrabar_Click(object sender, ImageClickEventArgs e)
    {
        String fecha = "01" + "/" + txtFechaDesde.Text;
        
        if (!fun.IsDate(fecha))
        {
            return;
        }
        DiaHoy = Convert.ToDateTime(fecha);
        ffin = DiaHoy.AddMonths(1);
        ffin = ffin.AddDays(-1);
        txtFechaHasta.Text = ffin.ToString("dd/MM/yyyy");
        Array.Resize(ref objparam, 5);
        objparam[0] = txtDescripcion.Text.ToUpper();
        objparam[1] = DiaHoy;
        objparam[2] = ffin;
        objparam[3] = "I";
        objparam[4] = Session["usuCodigo"];
        dsData = fun.consultarDatos("spInsPlanificacion", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows[0][0].ToString() == "Existe")
        {
            lblerror.Visible = true;
            return;
        }
        else
        {
            tblDetalle.Visible = true;
            btngrabar.Visible = false;
            lblerror.Visible = false;
            txtDescripcion.Enabled = false;
            txtFechaDesde.Enabled = false;
            txtFechaHasta.Enabled = false;
            LlenarComboDias(ddlDia, DiaHoy, ffin);
            LlenarComboLetra(ddlDesde);
            LlenarComboLetra(ddlHasta);
            ddlHasta.SelectedValue = "Z";
            Array.Resize(ref objparam, 2);
            objparam[0] = 0;
            objparam[1] = "14";//MOTIVO VISITA
            fun.cargarCombos(ddlTipoVisita, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
            ddlTipoVisita.Items.RemoveAt(0);
            objparam[0] = 0;
            objparam[1] = "2";//ETAPA
            fun.cargarCombos(ddlEtapa, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
            ddlEtapa.Items.RemoveAt(0);
            Array.Resize(ref objparam, 1);
            objparam[0] = ddlEtapa.SelectedValue;
            fun.cargarCombos(ddlPabellon, "spCarPabellon", objparam, Page, (String[])Session["constrring"]);
            ddlPabellon.Items.RemoveAt(0);
            Array.Resize(ref objparam, 1);
            objparam[0] = 0;
            fun.cargarCombos(ddlTurnos, "spCarTurnos", objparam, Page, (String[])Session["constrring"]);
            ddlTurnos.Items.RemoveAt(0);
            Array.Resize(ref objparam, 2);
            objparam[0] = ddlEtapa.SelectedValue;
            objparam[1] = ddlPabellon.SelectedValue;
            fun.cargarCombos(ddlAla, "spCarAla", objparam, Page, (String[])Session["constrring"]);
            ddlAla.Items.RemoveAt(0);
            Array.Resize(ref objparam, 3);
            objparam[0] = ddlEtapa.SelectedValue;
            objparam[1] = ddlAla.SelectedValue;
            objparam[2] = ddlPabellon.SelectedValue;
            fun.cargarCombos(ddlPiso, "spCarPiso", objparam, Page, (String[])Session["constrring"]);
            ddlPiso.Items.RemoveAt(0);
            btNuevo.Visible = true;
            btActualizar.Visible = false;
            btEliminar.Visible = false;
            btEliminarPla.Visible = true;

            //TRAER EL PARAMETRO QUE OCULTA EL BOTON DE VER PPL
            Array.Resize(ref objparam, 1);
            objparam[0] = "143";//OCULTAR BOTON
            dsData = fun.consultarDatos("spCargarParaDetalle", objparam, Page, (String[])Session["constrring"]);
            Session["mostrarboton"] = dsData.Tables[0].Rows[0][0].ToString();
            funCargaGrilla();
        }
    }

    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("frmplaAdmin.aspx");
    }

    protected void ddlEtapa_SelectedIndexChanged(object sender, EventArgs e)
    {
        Array.Resize(ref objparam, 1);
        objparam[0] = ddlEtapa.SelectedValue;
        fun.cargarCombos(ddlPabellon, "spCarPabellon", objparam, Page, (String[])Session["constrring"]);
        ddlPabellon.Items.RemoveAt(0);
        
    }

    protected void ddlPabellon_SelectedIndexChanged(object sender, EventArgs e)
    {
        Array.Resize(ref objparam, 2);
        objparam[0] = ddlEtapa.SelectedValue;
        objparam[1] = ddlPabellon.SelectedValue;
        fun.cargarCombos(ddlAla, "spCarAla", objparam, Page, (String[])Session["constrring"]);
        ddlAla.Items.RemoveAt(0);
    }
    protected void ddlAla_SelectedIndexChanged(object sender, EventArgs e)
    {
        Array.Resize(ref objparam, 3);
        objparam[0] = ddlEtapa.SelectedValue;
        objparam[1] = ddlAla.SelectedValue;
        objparam[2] = ddlPabellon.SelectedValue;
        fun.cargarCombos(ddlPiso, "spCarPiso", objparam, Page, (String[])Session["constrring"]);
        ddlPiso.Items.RemoveAt(0);
    }

    protected void btNuevo_Click(object sender, ImageClickEventArgs e)
    {
        lblerrorDetalle.Visible = false;

        if (char.Parse(ddlDesde.SelectedValue) > char.Parse(ddlHasta.SelectedValue))
        {
            lblerrorDetalle.Text = "La letra inicial no puede ser mayor a la letra final";
            lblerrorDetalle.Visible = true;
            return;
        }

        Array.Resize(ref objparam, 12);
        objparam[0] = "01" + "/" + txtFechaDesde.Text;
        objparam[1] = txtFechaHasta.Text;
        objparam[2] = ddlDia.SelectedValue;
        objparam[3] = ddlTurnos.SelectedValue;
        objparam[4] = ddlTipoVisita.SelectedValue;
        objparam[5] = ddlEtapa.SelectedValue;
        objparam[6] = ddlPabellon.SelectedValue;
        objparam[7] = ddlAla.SelectedValue;
        objparam[8] = ddlDesde.SelectedValue;
        objparam[9] = "Z";//ddlHasta.SelectedValue;
        objparam[10] = Session["usuCodigo"];
        objparam[11] = ddlPiso.SelectedValue;
        dsData = fun.consultarDatos("spInsPlanificacionDetalle", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows[0][0].ToString() == "Existe")
        {
            lblerrorDetalle.Text = "Ya existe un detalle que incluye este rango de letras para esta fecha";
            lblerrorDetalle.Visible = true;
            return;
        }
        funCargaGrilla();
        ddlHasta.SelectedValue = "Z";
        ddlDesde.SelectedValue = "A";
    }

    protected void grdvDatos_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string auxPabellon, auxAla, auxPiso;
            Int16 fila = Convert.ToInt16(grdvDatos.SelectedRow.RowIndex);

            Session["DetCodigo"] = grdvDatos.DataKeys[fila].Values["codigo"].ToString();
            Array.Resize(ref objparam, 1);
            objparam[0] = Session["DetCodigo"];
            dsData = fun.consultarDatos("spSelPlanificacionDetalle", objparam, Page, (String[])Session["constrring"]);
            if (dsData != null)
            {
                if (dsData.Tables[0].Rows.Count > 0)
                {
                    ddlDia.SelectedValue = dsData.Tables[0].Rows[0][2].ToString();
                    ddlTurnos.SelectedValue = dsData.Tables[0].Rows[0][3].ToString();
                    ddlTipoVisita.SelectedValue = dsData.Tables[0].Rows[0][4].ToString();
                    ddlEtapa.SelectedValue = dsData.Tables[0].Rows[0][5].ToString();
                    auxPabellon = dsData.Tables[0].Rows[0][6].ToString();
                    auxAla = dsData.Tables[0].Rows[0][7].ToString();
                    auxPiso = dsData.Tables[0].Rows[0][11].ToString();
                    ddlDesde.SelectedValue = dsData.Tables[0].Rows[0][8].ToString();
                    ddlHasta.SelectedValue = dsData.Tables[0].Rows[0][9].ToString();
                    Array.Resize(ref objparam, 1);
                    objparam[0] = ddlEtapa.SelectedValue;
                    fun.cargarCombos(ddlPabellon, "spCarPabellon", objparam, Page, (String[])Session["constrring"]);
                    ddlPabellon.Items.RemoveAt(0);
                    ddlPabellon.SelectedValue = auxPabellon;
                    Array.Resize(ref objparam, 2);
                    objparam[0] = ddlEtapa.SelectedValue;
                    objparam[1] = ddlPabellon.SelectedValue;
                    fun.cargarCombos(ddlAla, "spCarAla", objparam, Page, (String[])Session["constrring"]);
                    ddlAla.Items.RemoveAt(0);
                    ddlAla.SelectedValue = auxAla;
                    Array.Resize(ref objparam, 3);
                    objparam[0] = ddlEtapa.SelectedValue;
                    objparam[1] = ddlAla.SelectedValue;
                    objparam[2] = ddlPabellon.SelectedValue;
                    fun.cargarCombos(ddlPiso, "spCarPiso", objparam, Page, (String[])Session["constrring"]);
                    ddlPiso.Items.RemoveAt(0);
                    ddlPiso.SelectedValue = auxPiso;
                    btNuevo.Visible = false;
                    btActualizar.Visible = true;
                    btEliminar.Visible = true;
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void btActualizar_Click(object sender, ImageClickEventArgs e)
    {
        lblerrorDetalle.Visible = false;

        Array.Resize(ref objparam, 11);
        objparam[0] = ddlDia.SelectedValue;
        objparam[1] = ddlTurnos.SelectedValue;
        objparam[2] = ddlTipoVisita.SelectedValue;
        objparam[3] = ddlEtapa.SelectedValue;
        objparam[4] = ddlPabellon.SelectedValue;
        objparam[5] = ddlAla.SelectedValue;
        objparam[6] = ddlDesde.SelectedValue;
        objparam[7] = ddlHasta.SelectedValue;
        objparam[8] = Session["usuCodigo"];
        objparam[9] = Session["DetCodigo"];
        objparam[10] = ddlPiso.SelectedValue;
        dsData = fun.consultarDatos("spActPlanificacionDetalle", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows[0][0].ToString() == "Existe")
        {
            lblerrorDetalle.Text = "Ya existe un detalle que incluye este rango de letras para esta fecha";
            lblerrorDetalle.Visible = true;
            return;
        }
        funCargaGrilla();
        ddlHasta.SelectedValue = "A";
        ddlDesde.SelectedValue = "A";
        btNuevo.Visible = true;
        btActualizar.Visible = false;
        btEliminar.Visible = false;
    }

    protected void btEliminar_Click(object sender, ImageClickEventArgs e)
    {
        lblerrorDetalle.Visible = false;

        Array.Resize(ref objparam, 1);
        objparam[0] = Session["DetCodigo"];
        dsData = fun.consultarDatos("spEliPlanificacionDetalle", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows[0][0].ToString() != "OK")
        {
            lblerrorDetalle.Text = "No se puede eliminar el detalle";
            lblerrorDetalle.Visible = true;
            return;
        }
        funCargaGrilla();
        ddlHasta.SelectedValue = "A";
        ddlDesde.SelectedValue = "Z";
        btNuevo.Visible = true;
        btActualizar.Visible = false;
        btEliminar.Visible = false;
    }

    #endregion

    #region Funciones y Procedimientos
    private void LlenarComboDias(DropDownList dias, DateTime desde, DateTime hasta)
    {
        for (DateTime i = desde; i <= hasta; i = i.AddDays(1))
        {
            if (i > DateTime.Now.AddDays(-1))
            {
                ListItem listadatosx = new ListItem();
                listadatosx.Text = i.ToString("ddd, dd MMM yyyy");
                listadatosx.Value = i.ToString("dd/MM/yyyy");
                dias.Items.Add(listadatosx);
            }
        }
    }

    private void LlenarComboLetra(DropDownList letra)
    {
        for (char i = 'A'; i <= 'Z'; i++)
        {
            ListItem listadatosx = new ListItem();
            listadatosx.Text = i.ToString();
            listadatosx.Value = i.ToString();
            letra.Items.Add(listadatosx);
        }
    }

    protected void funCargaGrilla()
    {
        try
        {
            Array.Resize(ref objparam, 2);
            objparam[0] = "01" + "/" + txtFechaDesde.Text;
            objparam[1] = txtFechaHasta.Text;
            dsData = fun.consultarDatos("spCarPlanificacionDetalle", objparam, Page, (String[])Session["constrring"]);
            if (dsData != null)
            {
                grdvDatos.DataSource = dsData;
                grdvDatos.DataBind();
                Session["grdvDatos"] = grdvDatos.DataSource;
            }
            else
                SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", "No hay Datos");
        }
        catch (Exception ex)
        {
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", ex.Message);
        }
    }

    protected void btEliminarPla_Click(object sender, ImageClickEventArgs e)
    {
        Array.Resize(ref objparam, 2);
        objparam[0] = "01/" + txtFechaDesde.Text;
        objparam[1] = txtFechaHasta.Text;

        dsData = fun.consultarDatos("spEliPlanificacion", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows[0][0].ToString() == "Existe")
        {
            lblerrorDetalle.Text = "Ya existe un detalle que incluye este rango de letras para esta fecha";
            lblerrorDetalle.Visible = true;
            return;
        }
        Response.Redirect("~/Planificador/frmplaAdmin.aspx");
    }

    protected void btVerPPL_Click(object sender, EventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        int intIndex = gvRow.RowIndex;
        string codPlanificacion = grdvDatos.DataKeys[intIndex].Value.ToString();
        ClientScript.RegisterStartupScript(GetType(), "Lista de PPL", "javascript: var posicion_x; var posicion_y; posicion_x=(screen.width/2)-(900/2); posicion_y=(screen.height/2)-(600/2); window.open('frmListaPPL.aspx?codPlanificacion=" + codPlanificacion + "',null,'left=' + posicion_x + ', top=' + posicion_y + ', width=900px, height=600px, status=no,resizable= yes, scrollbars=yes, toolbar=no, location=no, menubar=no');", true);
    }
    protected void grdvDatos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Button btnboton = new Button();
        if (e.Row.RowIndex >= 0)
        {
            btnboton = (Button)(e.Row.Cells[10].FindControl("btVerPPL"));
            if (Session["mostrarboton"].ToString() == "NO") btnboton.Visible = false;
        }
    }
    #endregion
}