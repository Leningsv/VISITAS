using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class MenuN_frmmenuNew : System.Web.UI.Page
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
                lbltitulo.Text = "Agregar Nuevo Menú";
                funCargaMantenimiento();
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

    #region Procedimientos y Funciones
    protected void funCargaMantenimiento()
    {
        Array.Resize(ref objparam, 1);
        objparam[0] = 0;
        dsData = fun.consultarDatos("spnMenuNewRead", objparam, Page, (String[])Session["constrring"]);
        grdvDatos.DataSource = dsData;
        grdvDatos.DataBind();
        Session["grdvDatos"] = grdvDatos.DataSource;
    }
    #endregion

    #region Botones y Eventos
    protected void btningreso_Click(object sender, ImageClickEventArgs e)
    {
        String strmenuCodigo = "", strOk = "Ok";
        CheckBox chkAgregar;
        Array.Resize(ref objparam, 2);
        objparam[0] = txtnombre.Text;
        objparam[1] = Session["usuCodigo"];
        dsData = fun.consultarDatos("spnMenuNewGetMax", objparam, Page, (String[])Session["constrring"]);
        strmenuCodigo = dsData.Tables[0].Rows[0][0].ToString();
        if (strmenuCodigo == "Existe")
        {
            lblerror.Visible = true;
            return;
        }
        Array.Resize(ref objparam, 3);
        objparam[0] = Session["usuCodigo"];
        objparam[1] = strmenuCodigo;
        foreach (GridViewRow row in grdvDatos.Rows)
        {
            chkAgregar = row.FindControl("chkAgregar") as CheckBox;
            if (chkAgregar.Checked == true)
            {
                objparam[2] = grdvDatos.Rows[row.RowIndex].Cells[1].Text;
                strOk = fun.insertarDatos("spnMenuNewCreate", objparam, Page, (String[])Session["constrring"]); ;
                if (strOk == "Error")
                {
                    SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrión un Error al Grabar", "Consulte con el Administrador");
                    break;
                }
            }
        }
        if (strOk == "Ok")
        {
            Response.Redirect("frmmenuAdmin.aspx?MensajeRetornado='Guardado con Éxito'", false);
        }
    }
    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("frmmenuAdmin.aspx");
    }
    #endregion
}