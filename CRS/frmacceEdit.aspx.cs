using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class CRS_frmacceEdit : System.Web.UI.Page
{
    #region Variables
    DataSet dsData = new DataSet();
    ListItem listadatos = new ListItem();
    Object[] objparam = new Object[1];
    Funciones fun = new Funciones();
    DataTable tbini = new DataTable();
    #endregion

    #region Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                lbltitulo.Text = "Editar Acceso";
                Session["codigoloc"] = Request["locCodigo"].ToString();
                Session["codcrs"] = Request["codcrs"].ToString();
                Session["nomaccess"] = Request["descrip"].ToString();
                Session["etapacod"] = Request["codetapa"].ToString();
                objparam[0] = "";
                fun.cargarCombos(ddlcrs, "spCarCRSCmb", objparam, Page, (String[])Session["constrring"]);
                ddlcrs.SelectedValue = Request["codcrs"].ToString();
                Array.Resize(ref objparam, 2);
                objparam[0] = 0;
                objparam[1] = "3";
                fun.cargarCombos(ddletapa, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
                ddletapa.SelectedValue = Request["codetapa"].ToString();
                if (Request["mensajeRetornado"] != null)
                {
                    SIFunBasicas.Basicas.PresentarMensaje(Page, "::VISITA PPL::", Request["MensajeRetornado"]);
                }
                funCargaMantenimiento(Session["codigoloc"].ToString());
            }
        }
        catch (Exception ex)
        {
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", ex.Message);
        }
    }
    #endregion

    #region Funciones y Procedimientos
    protected void funCargaMantenimiento(String strCodigo)
    {
        Session["codigoante"] = strCodigo;
        txtcodigo.Text = strCodigo;
        Array.Resize(ref objparam, 2);
        objparam[0] = strCodigo;
        objparam[1] = Session["codcrs"].ToString();
        dsData = fun.consultarDatos("spnAccesoEditRead", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0)
        {
            txtnombre.Text = dsData.Tables[0].Rows[0][0].ToString();
            txtdescripcion.Text = dsData.Tables[0].Rows[0][1].ToString();
            chkestado.Checked = bool.Parse(dsData.Tables[0].Rows[0][2].ToString());
            chkestado.Text = dsData.Tables[0].Rows[0][2].ToString() == "True" ? "Activo" : "Inactivo";
        }
        Array.Resize(ref objparam, 2);
        objparam[0] = Session["codcrs"].ToString();
        objparam[1] = strCodigo;
        dsData = fun.consultarDatos("spCarEquiposLoc", objparam, Page, (String[])Session["constrring"]);
        grdvDetalle.DataSource = dsData;
        grdvDetalle.DataBind();
    }
    #endregion

    #region Botones y Eventos
    protected void chkestado_CheckedChanged(object sender, EventArgs e)
    {
        chkestado.Text = chkestado.Checked == true ? "Activo" : "Inactivo";
    }
    protected void btngrabar_Click(object sender, ImageClickEventArgs e)
    {
        Array.Resize(ref objparam, 12);
        objparam[0] = ddlcrs.SelectedValue;
        objparam[1] = txtnombre.Text.ToUpper();
        objparam[2] = ddletapa.SelectedValue;
        objparam[3] = txtdescripcion.Text.ToUpper();
        objparam[4] = chkestado.Checked;
        objparam[5] = txtcodigo.Text;
        objparam[6] = Session["codcrs"].ToString();
        objparam[7] = Session["nomaccess"].ToString();
        objparam[8] = Session["codigoloc"].ToString();
        objparam[9] = Session["etapacod"].ToString();
        objparam[10] = Session["usuCodigo"].ToString();
        objparam[11] = Session["MachineName"].ToString();

        //PREGUNTAR ANTES SI EXISTE ESTOS CASOS ANTES DE GRABAR
        dsData = fun.consultarDatos("spUpdateAcceso", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows[0][0].ToString() == "Existe")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ya existe Acceso Creado, por favor ingrese otro');", true);
            return;
        }
        Response.Redirect("frmacceAdmin.aspx?mensajeRetornado='Guardado Con éxito'", false); 
    }
    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("frmacceAdmin.aspx");
    }
    protected void chkestados_CheckedChanged(object sender, EventArgs e)
    {
        chkestados.Text = chkestados.Checked == true ? "Activo" : "Inactivo";
    }
    protected void btnverificar_Click(object sender, EventArgs e)
    {
        if (txtdirip.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ingrese Dirección IP');", true);
            //lblerror.Text = "Ingrese Dirección IP";
            //lblerror.Visible = true;
            return;
        }
        if (fun.Validar_Ping(txtdirip.Text, Page, 0) == 0)
        {
            lblestado.Text = "Desconectado";
            lblestado.ForeColor = Color.Red;
            lblestado.Visible = true;
            imgsemaforo.ImageUrl = "~/Images/semaforo_rojo.png";
            imgsemaforo.Height = 40;
            imgsemaforo.Width = 30;
        }
        else
        {
            lblestado.Text = "Conectado";
            lblestado.ForeColor = Color.Green;
            lblestado.Visible = true;
            imgsemaforo.ImageUrl = "~/Images/semaforo_verde.png";
            imgsemaforo.Height = 40;
            imgsemaforo.Width = 30;
        }
    }
    protected void btnagregar_Click(object sender, ImageClickEventArgs e)
    {
        if (txtequipo.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ingrese Nombre del Equipo');", true);
            return;
        }
        if (txtdirip.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ingrese Dirección IP');", true);
            return;
        }
        Array.Resize(ref objparam, 3);
        objparam[0] = Session["codcrs"].ToString();
        objparam[1] = txtdirip.Text;
        objparam[2] = txtequipo.Text.ToUpper();
        dsData = fun.consultarDatos("spCarEquipoAcce", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ya Existe Dirección IP');", true);
            return;
        }

        Array.Resize(ref objparam, 7);
        objparam[0] = Session["codcrs"].ToString();
        objparam[1] = txtcodigo.Text;
        objparam[2] = txtequipo.Text.ToUpper();
        objparam[3] = txtdirip.Text;
        objparam[4] = chkestados.Checked;
        objparam[5] = Session["usuCodigo"].ToString();
        objparam[6] = Session["MachineName"].ToString();
        dsData = fun.consultarDatos("spInserEquipEdit", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows[0][0].ToString() == "Existe ip")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ya Existe agregado Equipo o Dirección IP');", true);
            return;
        }
        if (dsData.Tables[0].Rows[0][0].ToString() == "Existe equipo")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ya Existe agregado Equipo o Dirección IP');", true);
            return;
        }
        if (dsData.Tables[0].Rows[0][0].ToString() == "correcto")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Datos Almacenados');", true);
            txtequipo.Text = "";
            txtdirip.Text = "";
            chkestados.Checked = true;
            chkestados.Text = "Activo";
            imgsemaforo.ImageUrl = "~/Images/semaforo_apagado.png";
            imgsemaforo.Height = 40;
            imgsemaforo.Width = 27;
            lblestado.Text = "Sin Verificar";
            return;
            
        }
        if (dsData.Tables[0].Rows[0][0].ToString() == "supera")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Limite de Accesos alcanzado');", true);
            return;
        }
         
    }
    protected void btneditar_Click(object sender, ImageClickEventArgs e)
    {
        if (txtequipo.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ingrese Nombre del Equipo');", true);
            return;
        }
        if (txtdirip.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ingrese Dirección IP');", true);
            return;
        }

        Array.Resize(ref objparam, 8);
        objparam[0] = Session["codcrs"].ToString();
        objparam[1] = txtcodigo.Text;
        objparam[2] = Session["codEqu"].ToString();
        objparam[3] = txtequipo.Text.ToUpper();
        objparam[4] = txtdirip.Text;
        objparam[5] = chkestados.Checked;
        objparam[6] = Session["equipoante"].ToString();
        objparam[7] = Session["diripante"].ToString();
        dsData = fun.consultarDatos("spUpdateEquAcc", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows[0][0].ToString() == "Existe")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ya Existe agregado Equipo o Dirección IP');", true);
            return;
        }

        grdvDetalle.DataSource = dsData;
        grdvDetalle.DataBind();

        txtequipo.Text = "";
        txtdirip.Text = "";
        chkestados.Checked = true;
        chkestados.Text = "Activo";
        imgsemaforo.ImageUrl = "~/Images/semaforo_apagado.png";
        imgsemaforo.Height = 40;
        imgsemaforo.Width = 27;
        lblestado.Text = "Sin Verificar";
        lblestado.ForeColor = Color.Gray;
        btneditar.Visible = false;
        btneliminar.Visible = false;
        btnagregar.Visible = true;
    }
    protected void btneliminar_Click(object sender, ImageClickEventArgs e)
    {
        Array.Resize(ref objparam, 3);
        objparam[0] = Session["codcrs"].ToString();
        objparam[1] = Session["codEqu"].ToString();
        objparam[2] = txtcodigo.Text;
        dsData = fun.consultarDatos("spDeleteEquAcce", objparam, Page, (String[])Session["constrring"]);
        grdvDetalle.DataSource = dsData;
        grdvDetalle.DataBind();

        txtequipo.Text = "";
        txtdirip.Text = "";
        chkestados.Checked = true;
        chkestados.Text = "Activo";
        imgsemaforo.ImageUrl = "~/Images/semaforo_apagado.png";
        imgsemaforo.Height = 40;
        imgsemaforo.Width = 27;
        lblestado.Text = "Sin Verificar";
        btneditar.Visible = false;
        btneliminar.Visible = false;
        btnagregar.Visible = true;
    }
    protected void btnselecc_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        int intIndex = gvRow.RowIndex;

        lblitem.Text = intIndex.ToString();
        Session["codEqu"] = grdvDetalle.DataKeys[intIndex].Values["Codigo"].ToString();
        txtequipo.Text = grdvDetalle.Rows[intIndex].Cells[1].Text;
        txtdirip.Text = grdvDetalle.Rows[intIndex].Cells[2].Text;
        Session["equipoante"] = grdvDetalle.Rows[intIndex].Cells[1].Text;
        Session["diripante"] = grdvDetalle.Rows[intIndex].Cells[2].Text;
        chkestados.Checked = grdvDetalle.Rows[intIndex].Cells[3].Text == "Activo" ? true : false;
        chkestados.Text = grdvDetalle.Rows[intIndex].Cells[3].Text == "Activo" ? "Activo" : "Inactivo";
        btneditar.Visible = true;
        btneliminar.Visible = true;
        btnagregar.Visible = false;
    }
    #endregion
 
}