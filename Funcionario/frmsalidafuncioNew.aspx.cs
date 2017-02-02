using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Funcionario_frmsalidafuncioNew : System.Web.UI.Page
{
    #region Variables
    DataSet dsData = new DataSet();
    Object[] objparam = new Object[1];
    Funciones fun = new Funciones();
    String ruta = @"C:\Temp\huellas.dat";
    String strObt = "";
    String foto = "";
    #endregion

    #region Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                lbltitulo.Text = "salida de funcionarios internos / visitantes externos";

                txtfechainicio.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtfechafin.Text = DateTime.Now.ToString("dd/MM/yyyy");

                Array.Resize(ref objparam, 2);
                objparam[0] = 0;
                objparam[1] = "4";
                fun.cargarCombos(ddltipodoc, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
                ddltipodoc.Items.RemoveAt(0);

                Array.Resize(ref objparam, 1);
                objparam[0] = "";
                fun.cargarCombos(ddldepartamento, "spCarDEpartament", objparam, Page, (String[])Session["constrring"]);
                ddldepartamento.Items.RemoveAt(0);

                Array.Resize(ref objparam, 2);
                objparam[0] = 0;
                objparam[1] = "27";
                fun.cargarCombos(ddlcargo, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
                ddlcargo.Items.RemoveAt(0);

                objparam[0] = 0;
                objparam[1] = "8";
                strObt = fun.ObtenerRutasFun(Page, "spCargarParametros", objparam, (String[])Session["constrring"]);
                String[] session = strObt.Split('|');
                Session["rutafoto"] = session[0].ToString();
                Session["rutahuella"] = session[1].ToString();

                Array.Resize(ref objparam, 1);
                objparam[0] = "104";
                dsData = fun.consultarDatos("spCargValorDetalle", objparam, Page, (String[])Session["constrring"]);
                Session["ssvalidar"] = dsData.Tables[0].Rows[0][0].ToString();

                Session["codigogenfun"] = Request["codigofun"].ToString();
                funCargaMantenimiento(Session["codigogenfun"].ToString());

            }
        }
        catch (Exception ex)
        {
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", ex.Message);
        }
    }
    #endregion

    #region Funciones y Procedimientos
    protected void funCargaMantenimiento(String strCodigoFun)
    {
        txtcodigo.Text = strCodigoFun;
        String rutasrv = Server.MapPath(Session["rutafoto"].ToString());
        Array.Resize(ref objparam, 2);
        objparam[0] = strCodigoFun;
        objparam[1] = Session["codigovisfun"].ToString();
        dsData = fun.consultarDatos("spCargDatosFunSalida", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0)
        {
            ddltipodoc.SelectedValue = dsData.Tables[0].Rows[0][0].ToString();
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
            txtndocu.Text = dsData.Tables[0].Rows[0][1].ToString();
            txtnombre1.Text = dsData.Tables[0].Rows[0][2].ToString();
            txtnombre2.Text = dsData.Tables[0].Rows[0][3].ToString();
            txtapellido1.Text = dsData.Tables[0].Rows[0][4].ToString();
            txtapellido2.Text = dsData.Tables[0].Rows[0][5].ToString();
            ddldepartamento.SelectedValue = dsData.Tables[0].Rows[0][6].ToString();
            ddlcargo.SelectedValue = dsData.Tables[0].Rows[0][7].ToString();
            txtobserva.Text = dsData.Tables[0].Rows[0][8].ToString();
            foto = dsData.Tables[0].Rows[0][9].ToString();
            Session["codigoevento"] = dsData.Tables[0].Rows[0][10].ToString();

        }
        if (foto == "")
        {
            imgfoto.ImageUrl = "~/Images/sinfoto.jpg";
        }
        else
        {
            if (File.Exists(rutasrv + foto))
            {
                imgfoto.ImageUrl = Session["rutafoto"].ToString() + foto;
                imgfoto.Width = 300;
                imgfoto.Height = 300;
            }
            else
            {
                imgfoto.ImageUrl = "~/Images/sinfoto.jpg";
            }
        }
    }

    #endregion

    #region Botones y Eventos
    protected void btningresar_Click(object sender, ImageClickEventArgs e)
    {
        Array.Resize(ref objparam, 2);
        objparam[0] = txtcodigo.Text;
        objparam[1] = Session["codigoevento"].ToString();
        dsData = fun.consultarDatos("spUpdateSalidaFun", objparam, Page, (String[])Session["constrring"]);
        ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='frmsalidavisitanteAdmin.aspx';window.close();", true);
    }

    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.close();", true);
    }
    #endregion
}