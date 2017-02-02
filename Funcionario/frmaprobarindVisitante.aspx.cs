using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Funcionario_frmaprobarindVisitante : System.Web.UI.Page
{
    #region Variables
    DataSet dsData = new DataSet();
    Object[] objparam = new Object[1];
    Funciones fun = new Funciones();
    //String ruta = @"C:\Temp\huellas.dat";
    String strObt = "";
    String foto = "";
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                lbltitulo.Text = "Ingresar Nuevo visitante externo";
                //txtfechadesde.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //txtfechahasta.Text = DateTime.Now.ToString("dd/MM/yyyy");

                Array.Resize(ref objparam, 2);
                objparam[0] = 0;
                objparam[1] = "4";
                fun.cargarCombos(ddltipodoc, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
                ddltipodoc.Items.RemoveAt(0);

                objparam[0] = 0;
                objparam[1] = "17";
                fun.cargarCombos(ddlgenero, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
                ddlgenero.Items.RemoveAt(0);

                Array.Resize(ref objparam, 1);
                objparam[0] = "";
                fun.cargarCombos(ddldepartamento, "spCarDEpartament", objparam, Page, (String[])Session["constrring"]);
                ddldepartamento.Items.RemoveAt(0);

                Array.Resize(ref objparam, 1);
                objparam[0] = "";
                fun.cargarCombos(ddlperfil, "spCarPerfilCab", objparam, Page, (String[])Session["constrring"]);
                ddlperfil.Items.RemoveAt(0);

                Array.Resize(ref objparam, 1);
                objparam[0] = 0;
                fun.cargarCombos(ddlfuncionario, "spCarFuncioInterno", objparam, Page, (String[])Session["constrring"]);
                ddlfuncionario.Items.RemoveAt(0);
                ddlfuncionario.SelectedValue = Session["codfuncienlace"].ToString();

                Array.Resize(ref objparam, 2);
                objparam[0] = 0;
                objparam[1] = "27";
                fun.cargarCombos(ddlcargo, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
                ddlcargo.Items.RemoveAt(0);

                objparam[0] = 0;
                objparam[1] = "36";
                fun.cargarCombos(ddltipofun, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
                ddltipofun.Items.RemoveAt(0);

                objparam[0] = 0;
                objparam[1] = "8";
                strObt = fun.ObtenerRutasFun(Page, "spCargarParametros", objparam, (String[])Session["constrring"]);
                String[] session = strObt.Split('|');
                Session["rutafoto"] = session[0].ToString();
                Session["rutahuella"] = session[1].ToString();

                funCargaMantenimiento(Session["codvisitante"].ToString());
                txtcodigo.Text = Session["codvisitante"].ToString();

            }
        }
        catch (Exception ex)
        {
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", ex.Message);
        }
    }

    #region Funciones y Procedimientos
    protected void funCargaMantenimiento(String strCodigoFun)
    {
        String rutasrv = Server.MapPath(Session["rutafoto"].ToString());
        Array.Resize(ref objparam, 3);
        objparam[0] = strCodigoFun;
        objparam[1] = Session["codfuncienlace"].ToString();
        objparam[2] = Session["codigoevento"].ToString();
        dsData = fun.consultarDatos("spCarDatoVistAprobar", objparam, Page, (String[])Session["constrring"]);
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
            txtfecnaci.Text = dsData.Tables[0].Rows[0][6].ToString();
            ddlgenero.SelectedValue = dsData.Tables[0].Rows[0][7].ToString();
            txtentidad.Text = dsData.Tables[0].Rows[0][8].ToString();
            ddldepartamento.SelectedValue = dsData.Tables[0].Rows[0][9].ToString();
            ddlcargo.SelectedValue = dsData.Tables[0].Rows[0][10].ToString();
            ddltipofun.SelectedValue = dsData.Tables[0].Rows[0][11].ToString();
            ddlfuncionario.SelectedValue = dsData.Tables[0].Rows[0][12].ToString();
            txtfechadesde.Text = dsData.Tables[0].Rows[0][13].ToString();
            txtfechahasta.Text = dsData.Tables[0].Rows[0][14].ToString();
            txtobserva.Text = dsData.Tables[0].Rows[0][15].ToString();
            foto = dsData.Tables[0].Rows[0][16].ToString();
        }
        //if (File.Exists(ruta))
        //{
        //    File.Delete(ruta);
        //}
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
    protected void btnaprobar_Click(object sender, ImageClickEventArgs e)
    {
        String strPerfil = "", strAbierto = "", strRestringido = "", strEliminar = "";

        if (txtfechadesde.Text != "" || txtfechahasta.Text != "")
        {
            if (Convert.ToDateTime(txtfechadesde.Text) > Convert.ToDateTime(txtfechahasta.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('La Fecha de Ingreso no puede ser mayor a la Fecha de Salida');", true);
                return;
            }

            if (Convert.ToDateTime(txtfechahasta.Text + " 23:59:59") < DateTime.Now)//se suma la hora porque se compara tambien las horas
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('La Fecha de Salida no puede ser menor a la Fecha Actual');", true);
                return;
            }
        }
        //TREAR LOS DATOS DEL PERFIL
        Array.Resize(ref objparam, 1);
        objparam[0] = ddlperfil.SelectedValue;
        dsData = fun.consultarDatos("spCarPerfFuncio", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0)
        {
            strPerfil = dsData.Tables[0].Rows[0][0].ToString();
            strAbierto = dsData.Tables[0].Rows[0][1].ToString();
            strRestringido = dsData.Tables[0].Rows[0][2].ToString();
            strEliminar = dsData.Tables[0].Rows[0][3].ToString();
        }
        Array.Resize(ref objparam, 12);
        objparam[0] = Session["codfuncienlace"].ToString();
        objparam[1] = txtcodigo.Text;
        objparam[2] = txtobserva.Text.ToUpper();
        objparam[3] = txtfechadesde.Text;
        objparam[4] = txtfechahasta.Text;
        objparam[5] = strPerfil;
        objparam[6] = strAbierto;
        objparam[7] = strRestringido;
        objparam[8] = strEliminar;
        objparam[9] = Session["usuCodigo"].ToString();
        objparam[10] = Session["MachineName"].ToString();
        objparam[11] = Session["codigoevento"].ToString();
        dsData = fun.consultarDatos("spInserTmpVisitFun", objparam, Page, (String[])Session["constrring"]);
        ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='frmfuncionaAproAdmin.aspx';window.close();", true);
    }

    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.close();", true);
    }

    protected void btncanelar_Click(object sender, ImageClickEventArgs e)
    {
        //REALIZA LA SALIDA DEL FUNCIONARIO Y BORRADO DEL TEMPORAL
        Array.Resize(ref objparam, 5);
        objparam[0] = Session["codfuncienlace"].ToString();
        objparam[1] = txtcodigo.Text;
        objparam[2] = Session["usuCodigo"].ToString();
        objparam[3] = Session["MachineName"].ToString();
        objparam[4] = Session["codigoevento"].ToString();
        dsData = fun.consultarDatos("spUpdateTempFuncio", objparam, Page, (String[])Session["constrring"]);
        ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='frmfuncionaAproAdmin.aspx';window.close();", true);
    }

    protected void btnequipos_Click(object sender, EventArgs e)
    {
        string codAcceso = ddlperfil.SelectedValue;
        string nomAcceso = ddlperfil.SelectedItem.Text;
        String pagina = "frmlistaEquipos.aspx?codAcceso=" + codAcceso + "&NombreAcceso=" + nomAcceso;
        String newpag = "window.open('" + pagina + "', 'popup_window', 'width=950,height=500,left=50,top=50,resizable=yes');";
        ScriptManager.RegisterStartupScript(this.UpdatePanel1, GetType(), "pop", newpag, true);
    }
    #endregion
}