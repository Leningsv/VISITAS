using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class PPL_frmvisitaPPLNew : System.Web.UI.Page
{
    #region Variables
    DataSet dsData = new DataSet();
    Object[] objparam = new Object[1];
    Funciones fun = new Funciones();
    #endregion

    #region Load

    protected void funCargarpost()
    {
        objparam[0] = 0;
        dsData = fun.consultarDatos("spVisitRelacionAdmin", objparam, Page, (String[])Session["constrring"]);
        grdvDatos.DataSource = dsData;
        grdvDatos.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                lbltitulo.Text = "relación visitante ppl - " + Session["nomppl"].ToString();
                Array.Resize(ref objparam, 2);
                objparam[0] = 0;
                objparam[1] = "4";
                fun.cargarCombos(ddltipodoc, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
                ddltipodoc.Items.RemoveAt(0);

                Array.Resize(ref objparam, 2);
                objparam[0] = 0;
                objparam[1] = "15";
                fun.cargarCombos(ddlparentesco, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
                ddlparentesco.Items.RemoveAt(0);

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

                Array.Resize(ref objparam, 1);
                objparam[0] = "2";
                dsData = fun.consultarDatos("spSecuencialGeneral", objparam, Page, (String[])Session["constrring"]);
                Session["codigotempVisitante"] = dsData.Tables[0].Rows[0][0].ToString();

                funCargarpost();
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
        int intCantidad = 0;
        String strTipoVis = "";
        Array.Resize(ref objparam, 3);
        objparam[0] = Session["CodigoPPL"].ToString();
        objparam[1] = Session["codigotempVisitante"].ToString();
        objparam[2] = ddlparentesco.SelectedValue;
        dsData = fun.consultarDatos("spCargParenRelaTipVis", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0)
        {
            intCantidad = int.Parse(dsData.Tables[0].Rows[0][0].ToString());
            strTipoVis = dsData.Tables[0].Rows[0][1].ToString();
        }
        if (strTipoVis == "M")
        {
            if (intCantidad >= int.Parse(Session["cantifam"].ToString()))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('A superado la cantidad máxima de visitas familiares');", true);
                return;
            }
        }
        if (strTipoVis == "C")
        {
            if (intCantidad == int.Parse(Session["canticon"].ToString()))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('A superado la cantidad máxima de visitas conyugales');", true);
                return;
            }
        }
        if (strTipoVis == "L")
        {
            if (intCantidad >= int.Parse(Session["cantilegal"].ToString()))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('A superado la cantidad máxima de visitas legales');", true);
                return;
            }
        }

        //if (txtndocu.Text != "")
        //{
        //    Array.Resize(ref objparam, 1);
        //    objparam[0] = txtndocu.Text;
        //    dsData = fun.consultarDatos("spVerificaCed", objparam, Page, (String[])Session["constrring"]);
        //    if (dsData.Tables[0].Rows.Count > 0)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Número de Documento ya existe');", true);
        //        return;
        //    }
        //}
        Array.Resize(ref objparam, 14);
        objparam[0] = Session["codigotempVisitante"].ToString();
        objparam[1] = ddltipodoc.SelectedValue;
        objparam[2] = txtndocu.Text;
        objparam[3] = txtnombre1.Text.ToUpper();
        objparam[4] = txtnombre2.Text.ToUpper();
        objparam[5] = txtapellido1.Text.ToUpper();
        objparam[6] = txtapellido2.Text.ToUpper();
        objparam[7] = txtobserva.Text.ToUpper();
        objparam[8] = true;
        objparam[9] = ddlparentesco.SelectedValue;
        objparam[10] = strTipoVis;
        objparam[11] = Session["CodigoPPL"].ToString();
        objparam[12] = Session["usuCodigo"].ToString();
        objparam[13] = Session["MachineName"].ToString();
        dsData = fun.consultarDatos("spInsertVisitante", objparam, Page, (String[])Session["constrring"]);
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Relación Creada');", true);
        ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascritp:window.opener.location='frmvisitaRelacion.aspx?verificavis='+'NO';window.close();", true);
    }
    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascritp:window.opener.location='frmvisitaRelacion.aspx';window.close();", true);
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
    #endregion
}