using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class PerfilA_frmperfaNew : System.Web.UI.Page
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
                lbltitulo.Text = "Agregar Nuevo Perfil";
                funCargaMantenimiento();
            }
            else
            {
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
        dsData = fun.consultarDatos("spCarAccesosPerf", objparam, Page, (String[])Session["constrring"]);
        grdvDatos.DataSource = dsData;
        grdvDatos.DataBind();
        Session["grdvDatos"] = grdvDatos.DataSource;
        Array.Resize(ref objparam, 1);
        objparam[0] = Session["MachineName"].ToString();
        dsData = fun.consultarDatos("spElimPerfCabAcc", objparam, Page, (String[])Session["constrring"]);
    }

    protected void funCargarGrid(String strCodAcce)
    {
        objparam[0] = strCodAcce;
        dsData = fun.consultarDatos("spCarEquiAcceLoc", objparam, Page, (String[])Session["constrring"]);
        grdvDetalle.DataSource = dsData;
        grdvDetalle.DataBind();
    }
    #endregion

    #region Botones y Eventos
    protected void grdvDatos_SelectedIndexChanged(object sender, EventArgs e)
    {
        ////Session["nomperfil"] = txtnombre.Text.ToUpper();
        ////Session["desperfil"] = txtdescripcion.Text;
        //Int16 fila = Convert.ToInt16(grdvDatos.SelectedRow.RowIndex);
        //Session["codcrs"] = grdvDatos.DataKeys[fila].Values["CodigoCRS"].ToString();  
        //Session["codlocacc"] = grdvDatos.DataKeys[fila].Values["CodigoLoc"].ToString();    
        //CheckBox chkvalor = grdvDatos.SelectedRow.FindControl("chkselecc") as CheckBox;
        //if (chkvalor.Checked == true)
        //{
        //    DataTable tblagreCab = new DataTable();
        //    tblagreCab = (DataTable)Session["tblcabecera"];
        //    DataRow filagre = tblagreCab.NewRow();
        //    filagre["CodigoCRS"] = Session["codcrs"].ToString();
        //    filagre["CodigoLOC"] = Session["codcrs"].ToString();
        //    tblagreCab.Rows.Add(filagre);
        //    Session["tblcabecera"] = tblagreCab;
        //    //funCargarGrid(Session["codlocacc"].ToString());
        //    //lblerror.Visible = false;
        //    //String nomacceso = grdvDatos.Rows[fila].Cells[2].Text;
        //    //lblequipos.Visible = true;
        //    //lblequipos.Text = "Equipos del acceso " + nomacceso;
        //}
        //else
        //{
        //    lblerror.Text = "Debe elegir en el Chekbox antes de listar";
        //    lblerror.Visible = true;
        //}            
    }

    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        Session["codlocacc"] = null;
        Response.Redirect("frmperfaAdmin.aspx");
    }


    protected void grdvDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        CheckBox chkequipo = new CheckBox();
        if (e.Row.RowIndex >= 0)
        {
            chkequipo = (CheckBox)e.Row.Cells[0].FindControl("chkequipo");
            chkequipo.Checked = true;
        }

    }

    private void funConsultarEquipo(String strCodEqu, CheckBox chkvalor)
    {
        //AGREGAR EN EL DATASE CABECERA

        Array.Resize(ref objparam, 2);
        objparam[0] = Session["codlocacc"].ToString();
        objparam[1] = strCodEqu;
        dsData = fun.consultarDatos("spCarEquiTem", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0)
        {
            chkvalor.Checked = true;
        }
    }

    protected void btngrabar_Click(object sender, ImageClickEventArgs e)
    {
        CheckBox chk_cabecera = new CheckBox();
        Array.Resize(ref objparam, 8);
        objparam[0] = txtnombre.Text.ToUpper();
        objparam[1] = txtdescripcion.Text.ToUpper();
        objparam[2] = chkestado.Checked;
        objparam[3] = chkabierto.Checked == true ? "SI" : "NO";
        objparam[4] = chkrestringido.Checked == true ? "SI" : "NO";
        objparam[5] = chkeliminar.Checked == true ? "SI" : "NO";
        objparam[6] = Session["usuCodigo"].ToString();
        objparam[7] = Session["MachineName"].ToString();
        dsData = fun.consultarDatos("spInserPerfilAcce", objparam, Page, (String[])Session["constrring"]);
        String strCodigoPerf = dsData.Tables[0].Rows[0][0].ToString();

        foreach (GridViewRow row in grdvDatos.Rows)
        {
            chk_cabecera = row.FindControl("chkselecc") as CheckBox;
            if (chk_cabecera.Checked == true)
            {
                String strCodCRS = grdvDatos.DataKeys[row.RowIndex].Values["CodigoCRS"].ToString();
                String strCodAcceso = grdvDatos.DataKeys[row.RowIndex].Values["CodigoLoc"].ToString();
                Boolean blEstado = grdvDatos.Rows[row.RowIndex].Cells[5].Text == "Activo" ? true : false;
                Array.Resize(ref objparam, 4);
                objparam[0] = strCodigoPerf;
                objparam[1] = strCodCRS;
                objparam[2] = strCodAcceso;
                objparam[3] = blEstado;
                dsData = fun.consultarDatos("spInserCabAcc", objparam, Page, (String[])Session["constrring"]);
                String strCodCabe = dsData.Tables[0].Rows[0][0].ToString();
                DataTable dt = new DataTable();
                Array.Resize(ref objparam, 3);
                objparam[0] = Session["MachineName"].ToString();
                objparam[1] = strCodCRS;
                objparam[2] = strCodAcceso;
                dsData = fun.consultarDatos("spCargPerfTem", objparam, Page, (String[])Session["constrring"]);
                dt = dsData.Tables[0];
                foreach (DataRow row_det in dt.Rows)
                {
                    String strCodEquipo = Convert.ToString(row_det["EQU_CODIGO"]);
                    String strDirIp = Convert.ToString(row_det["IP_DIRECC"]);
                    Boolean blEstadoEqu = (Boolean)row_det["ESTADO_CODIGO"];
                    Array.Resize(ref objparam, 6);
                    objparam[0] = strCodigoPerf;
                    objparam[1] = strCodCRS;
                    objparam[2] = strCodAcceso;
                    objparam[3] = strCodEquipo;
                    objparam[4] = strDirIp;
                    objparam[5] = blEstadoEqu;
                    dsData = fun.consultarDatos("spInserDetAcc", objparam, Page, (String[])Session["constrring"]);
                }
            }
        }
        Array.Resize(ref objparam, 1);
        objparam[0] = Session["MachineName"].ToString();
        dsData = fun.consultarDatos("spDelePerfTemp", objparam, Page, (String[])Session["constrring"]);
        Response.Redirect("frmperfaAdmin.aspx?MensajeRetornado='Guardado con Éxito'", false);
    }
    
    protected void chkselecc_CheckedChanged(object sender, EventArgs e)
    {

        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        int intIndex = gvRow.RowIndex;
        Session["codcrs"] = grdvDatos.DataKeys[intIndex].Values["CodigoCRS"].ToString();
        Session["codlocacc"] = grdvDatos.DataKeys[intIndex].Values["CodigoLoc"].ToString();
        CheckBox chkvalor = gvRow.FindControl("chkselecc") as CheckBox;
        Array.Resize(ref objparam, 2);
        objparam[0] = Session["codcrs"].ToString();
        objparam[1] = Session["codlocacc"].ToString();
        dsData = fun.consultarDatos("spCarEquipoAcc", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0)
        {
            if (chkvalor.Checked == true)
            {
                //LLENAR EN LA TABLA TEMPORAL PERFIL_TMP_CAB
                Array.Resize(ref objparam, 3);
                objparam[0] = Session["MachineName"].ToString();
                objparam[1] = Session["codcrs"].ToString();
                objparam[2] = Session["codlocacc"].ToString();
                dsData = fun.consultarDatos("spInsDelTemp", objparam, Page, (String[])Session["constrring"]);
            }
            else
            {
                //BORRAR DE LA TEMPORAL LA SELECCION
                Array.Resize(ref objparam, 3);
                objparam[0] = Session["MachineName"].ToString();
                objparam[1] = Session["codcrs"].ToString();
                objparam[2] = Session["codlocacc"].ToString();
                dsData = fun.consultarDatos("spDeleTemp", objparam, Page, (String[])Session["constrring"]);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Acceso no tiene Equipos Asignados');", true);
            chkvalor.Checked = false;
        }
    }

    protected void chkequipo_CheckedChanged(object sender, EventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        int intIndex = gvRow.RowIndex;

        CheckBox chkvalor = gvRow.FindControl("chkequipo") as CheckBox;
        String strCodEquipo = grdvDetalle.DataKeys[intIndex].Values["CodigoEqu"].ToString();
        Boolean blEstado = grdvDetalle.Rows[intIndex].Cells[3].Text == "Activo" ? true : false;
        String direcip = grdvDetalle.Rows[intIndex].Cells[2].Text;

        Array.Resize(ref objparam, 5);
        objparam[0] = Session["codlocacc"].ToString();
        objparam[1] = strCodEquipo;
        objparam[2] = direcip;
        objparam[3] = blEstado;
        objparam[4] = chkvalor.Checked == true ? 1 : 0;
        dsData = fun.consultarDatos("spInsEliEquTmp", objparam, Page, (String[])Session["constrring"]);
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
        String CodigoLoc = grdvDatos.DataKeys[intIndex].Values["CodigoLoc"].ToString();
        String CodigoCrs = grdvDatos.DataKeys[intIndex].Values["CodigoCRS"].ToString();
        Array.Resize(ref objparam, 3);
        objparam[0] = Session["MachineName"].ToString();
        objparam[1] = CodigoCrs;
        objparam[2] = CodigoLoc;
        dsData = fun.consultarDatos("spCarEquipoTemp", objparam, Page, (String[])Session["constrring"]);

        grdvDetalle.DataSource = dsData;
        grdvDetalle.DataBind();
    }
    #endregion
}