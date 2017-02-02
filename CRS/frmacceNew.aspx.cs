using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Net.Sockets;

public partial class CRS_frmacceNew : System.Web.UI.Page
{
    #region Variables
    DataSet dsData = new DataSet();
    ListItem listadatos = new ListItem();
    Object[] objparam = new Object[1];
    Funciones fun = new Funciones();
    DataTable tbini = new DataTable();
    int codigo;
    #endregion

    #region Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                tbini.Columns.Add("Codigo");
                tbini.Columns.Add("Equipo");
                tbini.Columns.Add("DirIP");
                tbini.Columns.Add("Estado");
                Session["tabsec"] = tbini;
                lbltitulo.Text = "Ingresar Nuevo Acceso";
                objparam[0] = "";
                fun.cargarCombos(ddlcrs, "spCarCRSCmb", objparam, Page, (String[])Session["constrring"]);
                ddlcrs.Items.RemoveAt(0);
                Array.Resize(ref objparam, 2);
                objparam[0] = 0;
                objparam[1] = "3";
                fun.cargarCombos(ddletapa, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
            }
        }
        catch (Exception ex)
        {
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", ex.Message);
        }
    }
    #endregion

    #region Botones y Eventos
    protected void btngrabar_Click(object sender, ImageClickEventArgs e)
    {
        Array.Resize(ref objparam, 8);
        objparam[0] = ddlcrs.SelectedValue;
        objparam[1] = txtnombre.Text.ToUpper();
        objparam[2] = ddletapa.SelectedValue;
        objparam[3] = txtdescripcion.Text.ToUpper();
        objparam[4] = 1;
        objparam[5] = Session["usuCodigo"].ToString();
        objparam[6] = Session["MachineName"].ToString();
        objparam[7] = txtcodigo.Text;
        dsData = fun.consultarDatos("spInsertAcceso", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows[0][0].ToString() == "Existe")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ya existe Acceso Creado, por favor ingrese otro');", true);
            return;
        }
        else
        {
            Array.Resize(ref objparam, 8);
            objparam[0] = Session["usuCodigo"];
            objparam[1] = Session["MachineName"];
            objparam[2] = txtcodigo.Text;
            objparam[3] = ddlcrs.SelectedValue;
            objparam[4] = ddletapa.SelectedValue;
            foreach (GridViewRow row in grdvDetalle.Rows)
            {
                objparam[5] = grdvDetalle.Rows[row.RowIndex].Cells[1].Text;
                objparam[6] = grdvDetalle.Rows[row.RowIndex].Cells[2].Text;
                objparam[7] = grdvDetalle.Rows[row.RowIndex].Cells[3].Text == "Activo" ? true : false;
                dsData = fun.consultarDatos("spInsertaEquipoAcc", objparam, Page, (String[])Session["constrring"]);
            }
        }
        Response.Redirect("frmacceAdmin.aspx?mensajeRetornado='Guardado con Éxito!.'");
    }
    protected void chkestado_CheckedChanged(object sender, EventArgs e)
    {
        chkestado.Text = chkestado.Checked == true ? "Activo" : "Inactivo";
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

        //ABRIR PUERTO
        //TcpClient clientsocket = new TcpClient();
        //clientsocket.Connect("192.168.1.26", 4370);
        //NetworkStream serverStream = clientsocket.GetStream();

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
        String strOK = "";
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
        objparam[0] = ddlcrs.SelectedValue;
        objparam[1] = txtdirip.Text;
        objparam[2] = txtequipo.Text.ToUpper();
        dsData = fun.consultarDatos("spCarEquipoAcce", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ya Existe Dirección IP');", true);
            return;
        }

        foreach (GridViewRow row in grdvDetalle.Rows)
        {
            strOK = "";
            if (txtequipo.Text.ToUpper() == grdvDetalle.Rows[row.RowIndex].Cells[1].Text || txtdirip.Text == grdvDetalle.Rows[row.RowIndex].Cells[2].Text)
            {
                strOK = "Ok";
                break;
            }
        }

        if (strOK == "Ok")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ya Existe Equipo o Dirección IP');", true);
            return;
        }

        if (Session["codigogen"] == null)
        {
            Session["codigogen"] = 1;
        }
        else Session["codigogen"] = int.Parse(Session["codigogen"].ToString()) + 1;

        DataTable tblagre = new DataTable();
        tblagre = (DataTable)Session["tabsec"];
        DataRow filagre = tblagre.NewRow();

        filagre["Codigo"] = Session["codigogen"].ToString();
        filagre["Equipo"] = txtequipo.Text.ToUpper();
        filagre["DirIP"] = txtdirip.Text;
        filagre["Estado"] = chkestados.Checked == true ? "Activo" : "Inactivo";
        tblagre.Rows.Add(filagre);
        Session["tabsec"] = tblagre;
        grdvDetalle.DataSource = tblagre;
        grdvDetalle.DataBind();
        txtequipo.Text = "";
        txtdirip.Text = "";
        imgsemaforo.ImageUrl = "~/Images/semaforo_apagado.png";
        imgsemaforo.Height = 40;
        imgsemaforo.Width = 27;
        lblestado.Text = "Sin Verificar";
        lblestado.ForeColor = Color.Gray;
    }
    protected void btneditar_Click(object sender, ImageClickEventArgs e)
    {
        String validar = "NO";
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
        if (txtequipo.Text != Session["equipoante"].ToString() && txtdirip.Text != Session["diripante"].ToString()) validar = "SI";
        if (txtequipo.Text != Session["equipoante"].ToString() && txtdirip.Text == Session["diripante"].ToString()) validar = "SI";
        if (txtequipo.Text == Session["equipoante"].ToString() && txtdirip.Text != Session["diripante"].ToString()) validar = "SI";

        if (validar == "SI")
        {
            String strOK = "";
            Array.Resize(ref objparam, 3);
            objparam[0] = ddlcrs.SelectedValue;
            objparam[1] = txtdirip.Text;
            objparam[2] = txtequipo.Text.ToUpper();
            dsData = fun.consultarDatos("spCarEquipoAcce", objparam, Page, (String[])Session["constrring"]);
            if (dsData.Tables[0].Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ya Existe agregado Equipo o Dirección IP');", true);
                return;
            }
            foreach (GridViewRow row in grdvDetalle.Rows)
            {
                String Codigo = grdvDetalle.DataKeys[row.RowIndex].Values["Codigo"].ToString();
                if (Session["codigose"].ToString() != Codigo)
                {
                    if (grdvDetalle.Rows[row.RowIndex].Cells[1].Text == txtequipo.Text.ToUpper() || grdvDetalle.Rows[row.RowIndex].Cells[2].Text == txtdirip.Text)
                    {
                        strOK = "Ok";
                        break;
                    }
                }
            }
            if (strOK == "Ok")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ya Existe agregado Equipo o Dirección IP');", true);
                return;
            }
        }
        grdvDetalle.Rows[int.Parse(lblitem.Text)].Cells[1].Text = txtequipo.Text;
        grdvDetalle.Rows[int.Parse(lblitem.Text)].Cells[2].Text = txtdirip.Text;
        grdvDetalle.Rows[int.Parse(lblitem.Text)].Cells[3].Text = chkestados.Checked == true ? "Activo" : "Inactivo";
        btnagregar.Visible = true;
        btneditar.Visible = false;
        btneliminar.Visible = false;
        txtequipo.Text = "";
        txtdirip.Text = "";
        chkestados.Checked = true;
        chkestados.Text = "Activo";
        imgsemaforo.ImageUrl = "~/Images/semaforo_apagado.png";
        imgsemaforo.Height = 40;
        imgsemaforo.Width = 27;
        lblestado.Text = "Sin Verificar";
        lblestado.ForeColor = Color.Gray;
    }
    protected void btneliminar_Click(object sender, ImageClickEventArgs e)
    {
        if (grdvDetalle.Rows.Count > 0)
        {
            tbini = (DataTable)Session["tabsec"];
            tbini.Rows.RemoveAt(int.Parse(lblitem.Text));
            Session["tabsec"] = tbini;
            grdvDetalle.DataSource = tbini;
            grdvDetalle.DataBind();
            btnagregar.Visible = true;
            btneditar.Visible = false;
            btneliminar.Visible = false;
            txtequipo.Text = "";
            txtdirip.Text = "";
            imgsemaforo.ImageUrl = "~/Images/semaforo_apagado.png";
            imgsemaforo.Height = 40;
            imgsemaforo.Width = 27;
            lblestado.Text = "Sin Verificar";
            lblestado.ForeColor = Color.Gray;
        }
    }
    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("frmacceAdmin.aspx");
    }
    protected void btnselecc_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        int intIndex = gvRow.RowIndex;

        lblitem.Text = intIndex.ToString();
        Session["codigose"] = grdvDetalle.DataKeys[intIndex].Values["Codigo"].ToString();
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