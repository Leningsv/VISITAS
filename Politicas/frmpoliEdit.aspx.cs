using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Politicas_frmpoliEdit : System.Web.UI.Page
{
    #region Variables
    DataSet dsData = new DataSet();
    Object[] objparam = new Object[1];
    ImageButton imgSubir = new ImageButton();
    ImageButton imgBajar = new ImageButton();
    Funciones fun = new Funciones();
    #endregion

    #region Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["perCodigo"].ToString() == "1")
                {
                    chkeliminar.Enabled = true;
                    chkcrear.Enabled = true;
                    chkmodificar.Enabled = true;
                }
                lbltitulo.Text = "Editar Política";
                txtcodigo.Text = Request["paraCodigo"].ToString();
                funCargaMantenimiento(txtcodigo.Text);
                if (Request["mensajeRetornado"] != null)
                {
                    SIFunBasicas.Basicas.PresentarMensaje(Page, "::VISITA PPL::", Request["MensajeRetornado"]);
                }
            }
            else
            {
                grdvDatos.DataSource = Session["grdvDatos"];
            }
        }
        catch (Exception ex)
        {
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrión un Error al Cargar Datos", ex.Message);
        }
    }
    #endregion

    #region Procedimientos y Funciones
    protected void funCargaMantenimiento(String strCodigoParam)
    {
        int fila = 0;
        
        Array.Resize(ref objparam, 1);
        objparam[0] = strCodigoParam;
        dsData = fun.consultarDatos("spnPoliEditRead", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0)
        {
            txtparametro.Text = dsData.Tables[0].Rows[0][0].ToString();
            txtdescrip.Text = dsData.Tables[0].Rows[0][1].ToString();
            chkestado.Text = Convert.ToString(dsData.Tables[0].Rows[0][2].ToString()) == "True" ? "Activo" : "Inactivo";
            chkestado.Checked = Convert.ToBoolean(dsData.Tables[0].Rows[0][2].ToString());
            chkeliminar.Checked = Convert.ToBoolean(dsData.Tables[0].Rows[0][3].ToString());
            Session["NomParam"] = txtparametro.Text;
            if (dsData.Tables[0].Rows[0][3].Equals(true))
            {
                Session["eliminar"] = "SI";
                chkeliminar.Checked = true;
            }
            else
            {
                Session["eliminar"] = "NO";
            }
            chkcrear.Checked = Convert.ToBoolean(dsData.Tables[0].Rows[0][4].ToString());
            if (dsData.Tables[0].Rows[0][4].Equals(true))
            {
                Session["crear"] = "SI";
                chkcrear.Checked = true;
            }
            else
            {
                Session["crear"] = "NO";
                btningresar.Visible = false;
            }
            chkmodificar.Checked = Convert.ToBoolean(dsData.Tables[0].Rows[0][5].ToString());
            if (dsData.Tables[0].Rows[0][5].Equals(true))
            {
                Session["modificar"] = "SI";
            }
            else
            {
                Session["modificar"] = "NO";
            }
        }
        Array.Resize(ref objparam, 3);
        objparam[0] = strCodigoParam;
        objparam[1] = "";
        objparam[2] = 0;
        dsData = fun.consultarDatos("spCarPoliDetalle", objparam, Page, (String[])Session["constrring"]);
        grdvDatos.DataSource = dsData;
        grdvDatos.DataBind();
        Session["grdvDatos"] = grdvDatos.DataSource;

        if (grdvDatos.Rows.Count > 0)
        {
            fila = grdvDatos.Rows.Count - 1;

            imgSubir = (ImageButton)grdvDatos.Rows[0].FindControl("imgSubirNivel");
            imgSubir.ImageUrl = "~/Botones/desactivadaup.png";
            imgSubir.Enabled = false;

            imgBajar = (ImageButton)grdvDatos.Rows[fila].FindControl("imgBajarNivel");
            imgBajar.ImageUrl = "~/Botones/desactivadadown.png";
            imgBajar.Enabled = false;
        }

    }
    #endregion

    #region Botones y Eventos
    protected void chkestado_CheckedChanged(object sender, EventArgs e)
    {
        chkestado.Text = chkestado.Checked == true ? "Activo" : "Inactivo";
    }

    protected void imgSubirNivel_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        int intIndex = gvRow.RowIndex;

        var strCodParDet = grdvDatos.DataKeys[intIndex].Values["Codigo"];
        Array.Resize(ref objparam, 3);
        objparam[0] = Session["CodParam"].ToString();
        objparam[1] = strCodParDet;
        objparam[2] = 0;
        dsData = fun.consultarDatos("spCamOrdenParDet", objparam, Page, (String[])Session["constrring"]);
        funCargaMantenimiento(Session["CodParam"].ToString());
    }

    protected void imgBajarNivel_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        int intIndex = gvRow.RowIndex;

        var strCodParDet = grdvDatos.DataKeys[intIndex].Values["Codigo"];
        Array.Resize(ref objparam, 3);
        objparam[0] = Session["CodParam"].ToString();
        objparam[1] = strCodParDet;
        objparam[2] = 1;
        dsData = fun.consultarDatos("spCamOrdenParDet", objparam, Page, (String[])Session["constrring"]);
        funCargaMantenimiento(Session["CodParam"].ToString());
    }

    protected void btngrabar_Click(object sender, ImageClickEventArgs e)
    {
        Array.Resize(ref objparam, 15);
        objparam[0] = txtcodigo.Text;
        objparam[1] = txtparametro.Text.ToUpper();
        objparam[2] = Session["NomParam"].ToString();
        objparam[3] = txtdescrip.Text.ToUpper();
        objparam[4] = chkeliminar.Checked;
        objparam[5] = chkcrear.Checked;
        objparam[6] = chkmodificar.Checked;
        objparam[7] = chkestado.Checked;
        objparam[8] = Session["usuCodigo"];
        objparam[9] = "Matenimiento CRS";
        objparam[10] = "Políticas CRS";
        objparam[11] = "frmpoliEdit.aspx";
        objparam[12] = "Modificar";
        objparam[13] = "MODIFICADO POR EL USUARIO  Código Cabecera(LOG_auxi1)";
        objparam[14] = Session["MachineName"].ToString();
        dsData = fun.consultarDatos("spnPoliEditUpdate", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows[0][0].ToString() == "Existe")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('El nombre de la Política ingresada ya existe');", true);
        }
        else Response.Redirect("frmpoliAdmin.aspx?MensajeRetornado='Guardado con Éxito'", false);
    }

    protected void btningresar_Click(object sender, ImageClickEventArgs e)
    {      
        ScriptManager.RegisterStartupScript(this, GetType(), "Parametro_Nuevo", "javascript: var posicion_x; var posicion_y; posicion_x=(screen.width/2)-(900/2); posicion_y=(screen.height/2)-(600/2); window.open('frmpoliNewDet.aspx?Codigo_Parametro=" + txtcodigo.Text + "&Codigo_Det=Nuevo" + "',null,'left=' + posicion_x + ', top=' + posicion_y + ', width=1024px, height=500px, status=no,resizable= yes, scrollbars=yes, toolbar=no, location=no, menubar=no,titlebar=0');", true);
    }

    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("frmpoliAdmin.aspx");
    }

    protected void btnselecc_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        int intIndex = gvRow.RowIndex;
        String strCodigoDet = grdvDatos.DataKeys[intIndex].Values["Codigo"].ToString();
        ScriptManager.RegisterStartupScript(this.uppbuscagrilla, GetType(), "Editar_Parametro", "javascript: var posicion_x; var posicion_y; posicion_x=(screen.width/2)-(900/2); posicion_y=(screen.height/2)-(600/2); window.open('frmpoliNewDet.aspx?Codigo_Parametro=" + txtcodigo.Text + "&Codigo_Det=" + strCodigoDet + "',null,'left=' + posicion_x + ', top=' + posicion_y + ', width=1024px, height=500px, status=no,resizable= yes, scrollbars=yes, toolbar=no, location=no, menubar=no,titlebar=0');", true);
    }
    protected void grdvDatos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdvDatos.PageIndex = e.NewPageIndex;
        grdvDatos.DataBind();
    }
    #endregion

}