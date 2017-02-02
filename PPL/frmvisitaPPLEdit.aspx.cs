using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class PPL_frmvisitaPPLEdit : System.Web.UI.Page
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
                lbltitulo.Text = "Modificar Visitante";
                Array.Resize(ref objparam, 2);
                objparam[0] = 0;
                objparam[1] = "4";
                fun.cargarCombos(ddltipodoc, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
                ddltipodoc.Items.RemoveAt(0);

                Array.Resize(ref objparam, 5);
                objparam[0] = "24";
                objparam[1] = "69";
                objparam[2] = 1;
                objparam[3] = "";
                objparam[4] = "";
                dsData = fun.consultarDatos("spCarParaDetalle", objparam, Page, (String[])Session["constrring"]);
                Session["SolicitaDocu"] = dsData.Tables[0].Rows[0][2].ToString();

                Array.Resize(ref objparam, 5);
                objparam[0] = "24";
                objparam[1] = "64";
                objparam[2] = 1;
                objparam[3] = "";
                objparam[4] = "";
                dsData = fun.consultarDatos("spCarParaDetalle", objparam, Page, (String[])Session["constrring"]);
                Session["ValidarCedula"] = dsData.Tables[0].Rows[0][2].ToString();

                funCargarpost(Session["codvisitante"].ToString());
            }
        }
        catch (Exception ex)
        {
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", ex.Message);
        }
    }
    #endregion

    #region Funciones y Procedimientos
    protected void funCargarpost(String strCodigoVis)
    {
        txtcodigo.Text = strCodigoVis;
        Array.Resize(ref objparam, 1);
        objparam[0] = strCodigoVis;
        dsData = fun.consultarDatos("spCarVisDatRelacion", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0)
        {
            ddltipodoc.SelectedValue = dsData.Tables[0].Rows[0][0].ToString();
            txtndocu.Text = dsData.Tables[0].Rows[0][1].ToString();
            Session["numdocuante"] = txtndocu.Text;
            txtnombre1.Text = dsData.Tables[0].Rows[0][2].ToString();
            txtnombre2.Text = dsData.Tables[0].Rows[0][3].ToString();
            txtapellido1.Text = dsData.Tables[0].Rows[0][4].ToString();
            txtapellido2.Text = dsData.Tables[0].Rows[0][5].ToString();
            txtobserva.Text = dsData.Tables[0].Rows[0][6].ToString();
            chkestado.Checked = bool.Parse(dsData.Tables[0].Rows[0][7].ToString());
            chkestado.Text = dsData.Tables[0].Rows[0][7].ToString() == "True" ? "Activo" : "Inactivo";
        }
    }

    #endregion

    #region Botones y Eventos
    protected void btngrabar_Click(object sender, ImageClickEventArgs e)
    {
        if (txtndocu.Text != Session["numdocuante"].ToString())
        {
            Array.Resize(ref objparam, 1);
            objparam[0] = txtndocu.Text;
            dsData = fun.consultarDatos("spVerificaCed", objparam, Page, (String[])Session["constrring"]);
            if (dsData.Tables[0].Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Número de Documento ya existe');", true);
                return;
            }
        }
        Array.Resize(ref objparam, 11);
        objparam[0] = txtcodigo.Text;
        objparam[1] = ddltipodoc.SelectedValue;
        objparam[2] = txtndocu.Text;
        objparam[3] = txtnombre1.Text.ToUpper();
        objparam[4] = txtnombre2.Text.ToUpper();
        objparam[5] = txtapellido1.Text.ToUpper();
        objparam[6] = txtapellido2.Text.ToUpper();
        objparam[7] = chkestado.Checked;
        objparam[8] = txtobserva.Text.ToUpper();
        objparam[9] = Session["usuCodigo"].ToString();
        objparam[10] = Session["MachineName"].ToString();
        dsData = fun.consultarDatos("spEditRelaVisita", objparam, Page, (String[])Session["constrring"]);
        ClientScript.RegisterStartupScript(GetType(), "pop", "javascritp:window.opener.location='frmvisitaRelacion.aspx';window.close();", true);
    }
    protected void ddltipodoc_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtndocu.Text = "";
        if (ddltipodoc.SelectedValue == "C")
        {
            txtndocu_FilteredTextBoxExtender.FilterMode = AjaxControlToolkit.FilterModes.ValidChars;
            txtndocu_FilteredTextBoxExtender.InvalidChars = ".-";
            txtndocu_FilteredTextBoxExtender.FilterType = AjaxControlToolkit.FilterTypes.Numbers;
        }
        else
        {
            txtndocu_FilteredTextBoxExtender.FilterMode = AjaxControlToolkit.FilterModes.InvalidChars;
            txtndocu_FilteredTextBoxExtender.InvalidChars = ".-*/{{}}[[]]\\";
            txtndocu_FilteredTextBoxExtender.FilterType = AjaxControlToolkit.FilterTypes.Custom;
        }
    }
    protected void chkestado_CheckedChanged(object sender, EventArgs e)
    {
        chkestado.Text = chkestado.Checked == true ? "Activo" : "Inactivo";
    }
    protected void btneliminar_Click(object sender, ImageClickEventArgs e)
    {
        Array.Resize(ref objparam, 2);
        objparam[0] = Session["CodigoPPL"].ToString();
        objparam[1] = txtcodigo.Text;
        dsData = fun.consultarDatos("spElimiRelacVisiPPL", objparam, Page, (String[])Session["constrring"]);
        ClientScript.RegisterStartupScript(GetType(), "pop", "javascritp:window.opener.location='frmvisitaRelacion.aspx';window.close();", true);
    }
    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        ClientScript.RegisterStartupScript(GetType(), "pop", "javascritp:window.close();", true);
    }
    #endregion
}