using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class CRS_frmaperturaEdit : System.Web.UI.Page
{
    #region Variables
    DataSet dsData = new DataSet();
    ListItem listadatos = new ListItem();
    Object[] objparam = new Object[1];
    Funciones fun = new Funciones();
    DataTable tbini = new DataTable();
    CheckBox chkelegir = new CheckBox();
    Int16 contar = 0;
    #endregion

    #region Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                lbltitulo.Text = "apertura automática de puertas";
                Array.Resize(ref objparam, 2);
                objparam[0] = 0;
                objparam[1] = "3";
                fun.cargarCombos(ddletapa, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);

                if (Request["codigoetapa"] != null)
                {
                    ddletapa.SelectedValue = Session["codeetapa"].ToString();
                }

                
                funCargaMantenimiento(ddletapa.SelectedValue);

            }
        }
        catch (Exception ex)
        {
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", ex.Message);
        }
    }
    #endregion
    
    #region Funciones y Procedimientos
    protected void funCargaMantenimiento(String strEtapa)
    {
        txtacceso.Text = "";
        Array.Resize(ref objparam, 2);
        objparam[0] = ddletapa.SelectedValue;
        objparam[1] = 0;
        dsData = fun.consultarDatos("spCargaAperturaPuertas", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0)
        {
            txtacceso.Text = dsData.Tables[0].Rows[0][0].ToString();
            Session["CodigoLoc"] = dsData.Tables[0].Rows[0][1].ToString();
        }

        tblequipos.Visible = false;
        Panel1.Visible = false;
        Array.Resize(ref objparam, 2);
        objparam[0] = strEtapa;
        objparam[1] = 1;
        dsData = fun.consultarDatos("spCargaAperturaPuertas", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0)
        {
            tblequipos.Visible = true;
            Panel1.Visible = true;
        }

        grdvDetalle.DataSource = dsData;
        grdvDetalle.DataBind();
    }
        
    #endregion

    #region Botones y Eventos

    protected void ddletapa_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["codeetapa"] = ddletapa.SelectedValue;
        funCargaMantenimiento(ddletapa.SelectedValue);
    }

    protected void grdvDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        Image imgEstado = new Image();
        ImageButton imgboton = new ImageButton();
        CheckBox chksel = new CheckBox();

        if (e.Row.RowIndex >= 0)
        {
            chksel = (CheckBox)(e.Row.Cells[0].FindControl("chkselecc"));
            imgEstado = (Image)(e.Row.Cells[1].FindControl("imgestado"));

            String strCodEquipo = grdvDetalle.DataKeys[e.Row.RowIndex].Values["CodigoEquipo"].ToString();
            String strCodigoLoc = Session["CodigoLoc"].ToString();
            String strCodigoEtapa = ddletapa.SelectedValue;

            //ACTUALIZAR EL ESTADO DE LAS PUERTAS
            Array.Resize(ref objparam, 4);
            objparam[0] = strCodigoEtapa;
            objparam[1] = strCodigoLoc;
            objparam[2] = strCodEquipo;
            objparam[3] = DateTime.Now;
            fun.consultarDatos("spUpdateEstApertura", objparam, Page, (String[])Session["constrring"]);

            Array.Resize(ref objparam, 3);
            objparam[0] = strCodigoEtapa;
            objparam[1] = strCodigoLoc;
            objparam[2] = strCodEquipo;
            dsData = fun.consultarDatos("spCarEstadoPuertas", objparam, Page, (String[])Session["constrring"]);
            if (dsData.Tables[0].Rows[0][0].ToString() == "A")
            {
                chksel.Checked = true;
                chksel.Enabled = false;
                imgEstado.ImageUrl = "~/Botones/torno_abierto.png";
            }
            else {
                chksel.Checked = false;
                imgEstado.ImageUrl = "~/Botones/torno_cerrado.png";
            }
        }
    }

    protected void btngrabar_Click(object sender, ImageClickEventArgs e)
    {
        foreach (GridViewRow row in grdvDetalle.Rows)
        {
            chkelegir = row.FindControl("chkselecc") as CheckBox;
            if (chkelegir.Checked == true)
            {
                contar++;
            }
        }
        if (contar == 0) 
        { 
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('No ha seleccionado ninguna puerta para Aperturar');", true);
            return;
        }

        if (txttiempo.Text != "")
        {

            if (int.Parse(txttiempo.Text) <= 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ingrese tiempo mayor que 0 (cero)');", true);
                return;
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ingrese tiempo en segundos de la apertura');", true);
            return;
        }

        if (txtobservacion.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ingrese observación de la apertura.');", true);
            return;
        }
       
        Array.Resize(ref objparam, 8);
        objparam[0] = ddletapa.SelectedValue;
        objparam[1] = Session["CodigoLoc"].ToString();
        objparam[2] = txttiempo.Text;
        objparam[3] = txtobservacion.Text.ToUpper();
        objparam[4] = Session["usuCodigo"];
        objparam[5] = Session["MachineName"].ToString();
        foreach (GridViewRow row in grdvDetalle.Rows)
        {
            chkelegir = row.FindControl("chkselecc") as CheckBox;
            if (chkelegir.Checked == true)
            {
                objparam[6] = grdvDetalle.DataKeys[row.RowIndex].Values["CodigoEquipo"];
                objparam[7] = "A";
                dsData = fun.consultarDatos("spUpdateApertura", objparam, Page, (String[])Session["constrring"]);                
            }
        }
        Response.Redirect("frmaperturaEdit.aspx");
        
    }
    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Index1.aspx");
    }

    protected void tmpactivar_Tick(object sender, EventArgs e)
    {
        //lblerror.Text = DateTime.Now.ToShortTimeString();
        //Response.Redirect("frmaperturaEdit.aspx?codigoetapa=" + ddletapa.SelectedValue);
    }
    protected void btnrefrescar_Click(object sender, ImageClickEventArgs e)
    {
        //ANTES IR A CAMBIAR EL ESTADO DE LAS PUERTAS

        Response.Redirect("frmaperturaEdit.aspx?codigoetapa=" + Session["codeetapa"].ToString());
        //Response.Redirect("frmaperturaEdit.aspx");
    }
    #endregion
}