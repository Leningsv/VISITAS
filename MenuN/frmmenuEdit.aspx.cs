using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class MenuN_frmmenuEdit : System.Web.UI.Page
{
    #region Variables
    DataSet dsData = new DataSet();
    Object[] objparam = new Object[1];
    ImageButton imgSubir = new ImageButton();
    ImageButton imgBajar = new ImageButton();
    CheckBox chkAgregar;
    Funciones fun = new Funciones();
    #endregion

    #region Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                lbltitulo.Text = "Editar Menú";
                funCargaMantenimiento(Request["codigo"].ToString());
                if (Request["mensajeRetornado"] != null)
                {
                    SIFunBasicas.Basicas.PresentarMensaje(Page, "::VISITA PPL::", Request["MensajeRetornado"]);
                }
            }
        }
        catch (Exception ex)
        {
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrión un Error al Cargar Datos", ex.Message);
        }
    }
    #endregion

    #region Procedimientos y Funciones
    protected void funCargaMantenimiento(String strCodigoMenu)
    {
        txtcodigo.Text = strCodigoMenu;
        Array.Resize(ref objparam, 1);
        objparam[0] = strCodigoMenu;
        dsData = fun.consultarDatos("spCargMenu", objparam, Page, (String[])Session["constrring"]);
        txtnombre.Text = dsData.Tables[0].Rows[0][0].ToString();
        chkestado.Text = dsData.Tables[0].Rows[0][1].ToString() == "True" ? "Activo" : "Inactivo";
        chkestado.Checked = dsData.Tables[0].Rows[0][1].ToString() == "True" ? true : false;
        Session["nommenu"] = txtnombre.Text;

        Int16 fila = 0, contar = 0;
        Array.Resize(ref objparam, 1);
        objparam[0] = strCodigoMenu;
        dsData = fun.consultarDatos("spnMenuEditRead", objparam, Page, (String[])Session["constrring"]);
        grdvDatos.DataSource = dsData;
        grdvDatos.DataBind();
        Session["grdvDatos"] = grdvDatos.DataSource;

        imgSubir = (ImageButton)grdvDatos.Rows[0].Cells[6].FindControl("imgSubirNivel");
        imgSubir.ImageUrl = "~/Botones/desactivadaup.png";
        imgSubir.Enabled = false;
        foreach (GridViewRow row in grdvDatos.Rows)
        {
            imgSubir = row.FindControl("imgSubirNivel") as ImageButton;
            imgBajar = row.FindControl("imgBajarNivel") as ImageButton;
            chkAgregar = row.FindControl("chkAgregar") as CheckBox;
            if (dsData.Tables[0].Rows[contar][0].ToString() == "Check") chkAgregar.Checked = true;
            else chkAgregar.Checked = false;
            if (chkAgregar.Checked == false)
            {
                imgSubir.ImageUrl = "~/Botones/desactivadaup.png";
                imgBajar.ImageUrl = "~/Botones/desactivadadown.png";
                imgSubir.Enabled = false;
                imgBajar.Enabled = false;
            }
            else fila = Convert.ToInt16(row.RowIndex);
            contar++;
        }
        imgBajar = (ImageButton)grdvDatos.Rows[fila].FindControl("imgBajarNivel");
        imgBajar.ImageUrl = "~/Botones/desactivadadown.png";
        imgBajar.Enabled = false;
    }
    #endregion

    #region Botones y Eventos
    protected void btningreso_Click(object sender, ImageClickEventArgs e)
    {
        String strOk = "Ok";
        Int16 validar = 0;

        if (Session["nommenu"].ToString().ToUpper() == txtnombre.Text.Trim().ToUpper())
        {
            validar = 1;
        }
        Array.Resize(ref objparam, 5);
        objparam[0] = txtcodigo.Text;
        objparam[1] = txtnombre.Text;
        objparam[2] = chkestado.Checked;
        objparam[3] = validar;
        objparam[4] = Session["usuCodigo"];
        dsData = fun.consultarDatos("spnMenuEditDeleteRows", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows[0][0].ToString() == "Existe")
        {
            lblerror.Visible = true;
            return;
        }

        Array.Resize(ref objparam, 5);
        objparam[0] = Session["usuCodigo"];
        objparam[1] = txtcodigo.Text;
        foreach (GridViewRow row in grdvDatos.Rows)
        {
            chkAgregar = row.FindControl("chkAgregar") as CheckBox;
            objparam[2] = chkAgregar.Checked == true ? "S" : "N";
            objparam[3] = grdvDatos.DataKeys[row.RowIndex].Values["CodigoTarea"];
            objparam[4] = grdvDatos.Rows[row.RowIndex].Cells[4].Text;
            strOk = fun.insertarDatos("spnMenuEditUpdate", objparam, Page, (String[])Session["constrring"]);
            if (strOk == "Error")
            {
                SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Grabar", "Consulte con el Adminstrador");
                break;
            }
        }
        if (strOk == "Ok")
        {
            Response.Redirect("frmmenuAdmin.aspx?MensajeRetornado='Guardado con Éxito'", false);
        }
    }

    protected void imgSubirNivel_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        int intIndex = gvRow.RowIndex;

        var strCodTarea = grdvDatos.DataKeys[intIndex].Values["CodigoTarea"];
        Array.Resize(ref objparam, 3);
        objparam[0] = txtcodigo.Text;
        objparam[1] = strCodTarea;
        objparam[2] = 0;
        dsData = fun.consultarDatos("spCamOrdenMenTarea", objparam, Page, (String[])Session["constrring"]);
        funCargaMantenimiento(txtcodigo.Text);
    }
    protected void imgBajarNivel_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        int intIndex = gvRow.RowIndex;

        var strCodTarea = grdvDatos.DataKeys[intIndex].Values["CodigoTarea"];
        Array.Resize(ref objparam, 3);
        objparam[0] = txtcodigo.Text;
        objparam[1] = strCodTarea;
        objparam[2] = 1;
        dsData = fun.consultarDatos("spCamOrdenMenTarea", objparam, Page, (String[])Session["constrring"]);
        funCargaMantenimiento(txtcodigo.Text);
    }
    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("frmmenuAdmin.aspx");
    }
    protected void chkestado_CheckedChanged(object sender, EventArgs e)
    {
        chkestado.Text = chkestado.Checked == true ? "Activo" : "Inactivo";
    }

    //protected void grdvDatos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    //funCargaMantenimiento(txtcodigo.Text);
    //    grdvDatos.PageIndex = e.NewPageIndex;
    //    //ctrlbuscar.GrdGrillaBusqueda = grdvDatos;
    //    grdvDatos.DataBind();
    //}
    #endregion
}