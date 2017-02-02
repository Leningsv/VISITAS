using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class PerfilA_frmperfaEdit : System.Web.UI.Page
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
                lbltitulo.Text = "modificar Perfil";
                Array.Resize(ref objparam, 1);
                objparam[0] = Session["MachineName"].ToString();
                dsData = fun.consultarDatos("spElimPerfCabAcc", objparam, Page, (String[])Session["constrring"]);
                funCargaMantenimiento(Request["perfCodigo"].ToString());
                txtcodigo.Text = Request["perfCodigo"].ToString();
            }
        }
        catch (Exception ex)
        {
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", ex.Message);
        }
    }
    #endregion

    #region Procedimientos y Funciones
    protected void funCargaMantenimiento(String strcodperfil)
    {
        Array.Resize(ref objparam, 1);
        objparam[0] = strcodperfil;
        dsData = fun.consultarDatos("spCarPerfCabecera", objparam, Page, (String[])Session["constrring"]);
        txtnombre.Text = dsData.Tables[0].Rows[0][0].ToString();
        txtdescripcion.Text = dsData.Tables[0].Rows[0][1].ToString();
        chkestado.Checked = dsData.Tables[0].Rows[0][2].ToString() == "True" ? true : false;
        chkestado.Text = dsData.Tables[0].Rows[0][2].ToString() == "True" ? "Activo" : "Inactivo";
        chkabierto.Checked = dsData.Tables[0].Rows[0][3].ToString() == "SI" ? true : false;
        chkrestringido.Checked = dsData.Tables[0].Rows[0][4].ToString() == "SI" ? true : false;
        chkeliminar.Checked = dsData.Tables[0].Rows[0][5].ToString() == "SI" ? true : false;
        Session["nomperfante"] = txtnombre.Text;
        Session["codigoPerfil"] = strcodperfil;

        Array.Resize(ref objparam, 1);
        objparam[0] = strcodperfil;
        dsData = fun.consultarDatos("spCarLocAcesso", objparam, Page, (String[])Session["constrring"]);
        grdvDatos.DataSource = dsData;
        grdvDatos.DataBind();
    }
    #endregion

    #region Botones y Eventos
    protected void grdvDatos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        CheckBox chkseleacce = new CheckBox();

        if (e.Row.RowIndex >= 0)
        {
            //CodigoCRS,CodigoLOC
            chkseleacce = (CheckBox)(e.Row.Cells[0].FindControl("chkselecc"));

            String strCodigoCRS = grdvDatos.DataKeys[e.Row.RowIndex].Values["CodigoCRS"].ToString();
            String strCodigoLOC = grdvDatos.DataKeys[e.Row.RowIndex].Values["CodigoLOC"].ToString();
            Array.Resize(ref objparam, 3);
            objparam[0] = Session["codigoPerfil"].ToString();
            objparam[1] = strCodigoCRS;
            objparam[2] = strCodigoLOC;
            dsData = fun.consultarDatos("spCarEstAcceso", objparam, Page, (String[])Session["constrring"]);
            if (dsData.Tables[0].Rows.Count > 0)
            {
                chkseleacce.Checked = true;
                //GRABAR EN DATOS                    
            }
            if (chkseleacce.Checked == true)
            {

                Array.Resize(ref objparam, 4);
                objparam[0] = Session["MachineName"].ToString();
                objparam[1] = Session["codigoPerfil"].ToString();
                objparam[2] = strCodigoCRS;
                objparam[3] = strCodigoLOC;
                dsData = fun.consultarDatos("spInserTempAcces", objparam, Page, (String[])Session["constrring"]);
            }
        }
    }

    protected void grdvDatos_SelectedIndexChanged(object sender, EventArgs e)
    {
        //lblerror.Visible = false;
        //Int16 fila = Convert.ToInt16(grdvDatos.SelectedRow.RowIndex);
        //Session["codigolog"] = grdvDatos.DataKeys[fila].Values["Codigo"].ToString();
        //CheckBox chkselection = grdvDatos.SelectedRow.FindControl("chkselecc") as CheckBox;
        //if (chkselection.Checked == true)
        //{
        //    //TRAER EL CODIGO PRA_CODIGO
        //    Array.Resize(ref objparam, 1);
        //    objparam[0] = Session["codigolog"].ToString();
        //    dsData = fun.consultarDatos("spCargCodigoPRA", objparam, Page, (String[])Session["constrring"]);
        //    if (dsData.Tables[0].Rows.Count > 0)
        //    {
        //        Session["codigoPRA"] = dsData.Tables[0].Rows[0][0].ToString();
        //    }
        //    else
        //    {
        //        Session["codigoPRA"] = "0";
        //    }
        //    //CARGAR EQUIPOS ASOCIADOS
        //    String nomacceso = grdvDatos.Rows[fila].Cells[2].Text;
        //    lblequipos.Visible = true;
        //    lblequipos.Text = "Equipos del acceso " + nomacceso;
        //    Array.Resize(ref objparam, 3);
        //    objparam[0] = Session["codigoPerfil"].ToString();
        //    objparam[1] = Session["codigoPRA"].ToString();
        //    objparam[2] = Session["codigolog"].ToString();
        //    dsData = fun.consultarDatos("spCargaEquiAccEdit", objparam, Page, (String[])Session["constrring"]);
        //    grdvDetalle.DataSource = dsData;
        //    grdvDetalle.DataBind();                
        //}
        //else
        //{
        //    lblerror.Text = "Debe elegir en el Chekbox antes de listar";
        //    lblerror.Visible = true;
        //} 
    }

    protected void grdvDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        CheckBox chkseleacce = new CheckBox();

        if (e.Row.RowIndex >= 0)
        {
            chkseleacce = (CheckBox)(e.Row.Cells[0].FindControl("chkequipo"));
            chkseleacce.Checked = true;
            String strCodigo = grdvDetalle.DataKeys[e.Row.RowIndex].Values["CodigoEqu"].ToString();
            Array.Resize(ref objparam, 4);
            objparam[0] = Session["codigoPerfil"].ToString();
            objparam[1] = Session["CodigoCRS"].ToString();
            objparam[2] = Session["CodigoLOC"].ToString();
            objparam[3] = strCodigo;
            dsData = fun.consultarDatos("spCarDetaPerfiAcce", objparam, Page, (String[])Session["constrring"]);
            if (dsData.Tables[0].Rows.Count > 0) chkseleacce.Checked = true;
            else chkseleacce.Checked = false;
        }
    }

    protected void chkselecc_CheckedChanged(object sender, EventArgs e)
    {
        lblequipos.Visible = false;
        grdvDetalle.DataSource = null;
        grdvDetalle.DataBind();
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        CheckBox chkvalor = gvRow.FindControl("chkselecc") as CheckBox;
        int intIndex = gvRow.RowIndex;
        String strCodigoCRS = grdvDatos.DataKeys[intIndex].Values["CodigoCRS"].ToString();
        String strCodigoLOC = grdvDatos.DataKeys[intIndex].Values["CodigoLOC"].ToString();
        Array.Resize(ref objparam, 2);
        objparam[0] = strCodigoCRS;
        objparam[1] = strCodigoLOC;
        dsData = fun.consultarDatos("spCarEquiAcceLoc", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count == 0)
        {
            chkvalor.Checked = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Acceso no tiene Equipos Asignados');", true);
            return;
        }
        Array.Resize(ref objparam, 5);
        objparam[0] = Session["MachineName"].ToString();
        objparam[1] = strCodigoCRS;
        objparam[2] = strCodigoLOC;
        objparam[3] = Session["codigoPerfil"].ToString();
        objparam[4] = chkvalor.Checked == true ? 0 : 1;
        dsData = fun.consultarDatos("spInserAcceTempEdit", objparam, Page, (String[])Session["constrring"]);
    }

    protected void chkequipo_CheckedChanged(object sender, EventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        int intIndex = gvRow.RowIndex;

        CheckBox chkvalor = gvRow.FindControl("chkequipo") as CheckBox;
        String strCodEquipo = grdvDetalle.DataKeys[intIndex].Values["CodigoEqu"].ToString();
        String direcip = grdvDetalle.Rows[intIndex].Cells[2].Text;

        Array.Resize(ref objparam, 6);
        objparam[0] = Session["MachineName"].ToString();
        objparam[1] = Session["CodigoCRS"].ToString();
        objparam[2] = Session["CodigoLOC"].ToString();
        objparam[3] = strCodEquipo;
        objparam[4] = direcip;
        objparam[5] = chkvalor.Checked == true ? 1 : 0;
        dsData = fun.consultarDatos("spElimTempEqui", objparam, Page, (String[])Session["constrring"]);
    }

    protected void btngrabar_Click(object sender, ImageClickEventArgs e)
    {
        Array.Resize(ref objparam, 10);
        objparam[0] = Session["codigoPerfil"].ToString();
        objparam[1] = txtnombre.Text.ToUpper();
        objparam[2] = Session["nomperfante"].ToString();
        objparam[3] = txtdescripcion.Text.ToUpper();
        objparam[4] = chkestado.Checked;
        objparam[5] = chkabierto.Checked == true ? "SI" : "NO";
        objparam[6] = chkrestringido.Checked == true ? "SI" : "NO";
        objparam[7] = chkeliminar.Checked == true ? "SI" : "NO";
        objparam[8] = Session["usuCodigo"].ToString();
        objparam[9] = Session["MachineName"].ToString();
        dsData = fun.consultarDatos("spEditPerfilAcc", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows[0][0].ToString() == "Existe")
        {
            lblerror.Visible = true;
            return;
        }

        Array.Resize(ref objparam, 1);
        objparam[0] = Session["codigoPerfil"].ToString();
        dsData = fun.consultarDatos("spDeletePerfilAccEdit", objparam, Page, (String[])Session["constrring"]);

        DataTable dt = new DataTable();
        Array.Resize(ref objparam, 1);
        objparam[0] = Session["MachineName"].ToString();
        dsData = fun.consultarDatos("spCarTempCabePerf", objparam, Page, (String[])Session["constrring"]);
        dt = dsData.Tables[0];
        foreach (DataRow row_det in dt.Rows)
        {
            String strCodigoCRS = Convert.ToString(row_det["PFT_CODIGOCRS"]);
            String strLocCodigo = Convert.ToString(row_det["PFT_CODIGOLOC"]);
            Array.Resize(ref objparam, 4);
            objparam[0] = Session["MachineName"].ToString();
            objparam[1] = txtcodigo.Text;
            objparam[2] = strCodigoCRS;
            objparam[3] = strLocCodigo;
            dsData = fun.consultarDatos("spInserPerfCabe", objparam, Page, (String[])Session["constrring"]);
            DataTable dt1 = new DataTable();
            Array.Resize(ref objparam, 3);
            objparam[0] = Session["MachineName"].ToString();
            objparam[1] = strCodigoCRS;
            objparam[2] = strLocCodigo;
            dsData = fun.consultarDatos("spCarDetEmpAcc", objparam, Page, (String[])Session["constrring"]);
            dt1 = dsData.Tables[0];
            foreach (DataRow row_deta in dt1.Rows)
            {
                String strCodEquipo = Convert.ToString(row_deta["EQU_CODIGO"]);
                String strDirIP = Convert.ToString(row_deta["IP_DIRECC"]);
                Array.Resize(ref objparam, 6);
                objparam[0] = Session["MachineName"].ToString();
                objparam[1] = txtcodigo.Text;
                objparam[2] = strCodigoCRS;
                objparam[3] = strLocCodigo;
                objparam[4] = strCodEquipo;
                objparam[5] = strDirIP;
                dsData = fun.consultarDatos("spInserPerfDetaAcce", objparam, Page, (String[])Session["constrring"]);
            }
        }
        Response.Redirect("frmperfaAdmin.aspx?MensajeRetornado='Guardado con Éxito'", false);
    }

    protected void chkestado_CheckedChanged(object sender, EventArgs e)
    {
        chkestado.Text = chkestado.Checked == true ? "Activo" : "Inactivo";
    }

    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("frmperfaAdmin.aspx");
    }

    protected void btnselecc_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        int intIndex = gvRow.RowIndex;

        CheckBox chkvalor = gvRow.FindControl("chkselecc") as CheckBox;
        String text = grdvDatos.Rows[intIndex].Cells[3].Text;
        lblequipos.Text = "Equipos Acceso: " + grdvDatos.Rows[intIndex].Cells[3].Text + " Nombre crs: " + grdvDatos.Rows[intIndex].Cells[2].Text;
        lblequipos.Visible = true;
        if (chkvalor.Checked == false)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Porfavor Seleccione Check para Mostrar Equipos');", true);
            return;

        }
        //CodigoCRS,CodigoLOC
        Session["CodigoLOC"] = grdvDatos.DataKeys[intIndex].Values["CodigoLOC"].ToString();
        Session["CodigoCRS"] = grdvDatos.DataKeys[intIndex].Values["CodigoCRS"].ToString();
        Array.Resize(ref objparam, 3);
        objparam[0] = Session["MachineName"].ToString();
        objparam[1] = Session["CodigoCRS"].ToString();
        objparam[2] = Session["CodigoLOC"].ToString();
        dsData = fun.consultarDatos("spCarEquipoTemp", objparam, Page, (String[])Session["constrring"]);

        grdvDetalle.DataSource = dsData;
        grdvDetalle.DataBind();
    }
    #endregion
}