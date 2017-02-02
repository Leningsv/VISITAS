using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class PerfilN_frmperfEdit : System.Web.UI.Page
{
    #region Variables
    DataSet dsData = new DataSet();
    Object[] objparam = new Object[1];
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
                lbltitulo.Text = "Editar Perfil";
                Session["CodPerfil"] = Request["perfCodigo"].ToString();
                funCargaMantenimiento(Session["CodPerfil"].ToString());
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
    protected void funCargaMantenimiento(String strCodigoPerfil)
    {
        int contar = 0;
        Array.Resize(ref objparam, 1);
        objparam[0] = strCodigoPerfil;
        dsData = fun.consultarDatos("spCargPerfil", objparam, Page, (String[])Session["constrring"]);
        txtcodigo.Text = strCodigoPerfil;
        txtnombre.Text = dsData.Tables[0].Rows[0][0].ToString();
        txtdescripcion.Text = dsData.Tables[0].Rows[0][1].ToString();
        chkestado.Text = dsData.Tables[0].Rows[0][2].ToString()=="True"?"Activo":"Inactivo";
        chkestado.Checked = dsData.Tables[0].Rows[0][2].ToString() == "True" ? true : false;
        Session["NomPerfil"] = txtnombre.Text;
        Array.Resize(ref objparam, 1);
        objparam[0] = strCodigoPerfil;
        dsData = fun.consultarDatos("spnPerfEditRead", objparam, Page, (String[])Session["constrring"]);
        grdvDatos.DataSource = dsData;
        grdvDatos.DataBind();
        Session["grdvDatos"] = grdvDatos.DataSource;
        foreach (GridViewRow row in grdvDatos.Rows)
        {
            chkAgregar = row.FindControl("chkAgregar") as CheckBox;
            if (dsData.Tables[0].Rows[contar][0].ToString() == "Activo")chkAgregar.Checked = true;
            else chkAgregar.Checked = false;
            contar++;
        }
    }
    #endregion

    #region Botones y Eventos
    protected void btningreso_Click(object sender, ImageClickEventArgs e)
    {
        String ok = "Ok";
        CheckBox chkAgregar;
        Int16 validar = 0;
        if (Session["NomPerfil"].ToString().ToUpper() == txtnombre.Text.Trim().ToUpper())
        {
            validar = 1;
        }
        Array.Resize(ref objparam, 6);
        objparam[0] = txtcodigo.Text;
        objparam[1] = txtnombre.Text.ToUpper();
        objparam[2] = txtdescripcion.Text.ToUpper();
        objparam[3] = chkestado.Checked == true ? 1 : 0;
        objparam[4] = validar;
        objparam[5] = Session["usuCodigo"];
        dsData = fun.consultarDatos("spnPerfEditUpdate", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows[0][0].ToString() == "Existe")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('El Nombre del Perfil ingresado ya existe, por favor ingrese otro ');", true);
            return;
        }

        Array.Resize(ref objparam, 3);
        objparam[0] = txtcodigo.Text;
        objparam[1] = Session["usuCodigo"];
        foreach (GridViewRow row in grdvDatos.Rows)
        {
            chkAgregar = row.FindControl("chkAgregar") as CheckBox;
            if (chkAgregar.Checked == true)
            {
                objparam[2] = grdvDatos.Rows[row.RowIndex].Cells[2].Text;
                ok = fun.insertarDatos("spnPerfEditUpdateRow", objparam, Page, (String[])Session["constrring"]);
                if (ok == "Error")
                {
                    break;
                }
            }
        }
        if (ok == "Ok")
        {
            Response.Redirect("frmperfAdmin.aspx?MensajeRetornado='Guardado con Éxito'", false);
        }
        else
        {
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Grabar Datos", "Consulte con el Administrador");
        }
    }
    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("frmperfAdmin.aspx");
    }
    protected void chkestado_CheckedChanged(object sender, EventArgs e)
    {
        chkestado.Text = chkestado.Checked == true ? "Activo" : "Inactivo";
    }
    #endregion
}