using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class VisitaPPL_CapturaHuella : System.Web.UI.Page
{
    #region Variables
    DataSet dsData = new DataSet();
    Object[] objparam = new Object[1];
    Funciones fun = new Funciones();
    //String ruta = @"C:\Temp\";
    //String rutahuell = "C:/Temp/";
    //String srvhuella = "";
    //String template = "";
    String lblmensaje = "";
    #endregion

    #region Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Array.Resize(ref objparam, 8);
            objparam[0] = Session["MachineName"].ToString();
            objparam[1] = Session["codigotempVisitante"].ToString();
            objparam[2] = Session["tabla"].ToString();
            objparam[3] = Session["usuElimina"].ToString();
            objparam[4] = Session["canthuellas"].ToString();
            objparam[5] = "NUMHUE";
            objparam[6] = Session["capturar"].ToString();
            objparam[7] = Session["verificar"].ToString();
            var hola = Session["constrring"];
            dsData = fun.consultarDatos("spInserHuella", objparam, Page, (String[])Session["constrring"]);
        }
    }
    #endregion

    #region Botones y Eventos
    protected void btngrabar_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["capturar"].ToString() == "S")
        {
            Array.Resize(ref objparam, 4);
            objparam[0] = Session["MachineName"].ToString();
            objparam[1] = Session["codigotempVisitante"].ToString();
            objparam[2] = Session["tabla"].ToString();
            objparam[3] = Session["usuCodigo"].ToString();
            dsData = fun.consultarDatos("spGrabHuella", objparam, Page, (String[])Session["constrring"]);
            if (dsData.Tables[0].Rows[0][0].ToString() == "Exito")
            {
                Session["grabohuella"] = "S";
                lblmensaje = "Huellas almacenadas con Éxito";
            }
            else
            {
                Session["grabohuella"] = "N";
                lblmensaje = "No se almancenó ninguna huella";
            }
        }
        
        if (Session["verificar"].ToString() == "S")
        {
            //CONSULTAR SI LA HUELLA ES CORRECTA
            Array.Resize(ref objparam, 1);
            objparam[0] = Session["MachineName"].ToString();
            dsData = fun.consultarDatos("spCargHuellaVerifi", objparam, Page, (String[])Session["constrring"]);
            if (dsData.Tables[0].Rows.Count == 0)
            {
                Session["grabohuella"] = "N";
                lblmensaje = "No se verificó ninguna Huella";
            }
            else
            {
                Session["grabohuella"] = "S";
                lblmensaje = "Huella(s) verificada(s) con éxito";
            }
        }
        //if (Session["redirec"].ToString() == "1") ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='frmingresoporVisitanteNew.aspx?mensajeRetornado=" + lblmensaje + "';window.close();", true);
        if (Session["redirec"].ToString() == "1") Response.Redirect("frmingresoporVisitanteNew.aspx?mensajeRetornado=" + lblmensaje);
        if (Session["redirec"].ToString() == "2") ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='../Visita/frmvisitanteNew.aspx?mensajeRetornado=" + lblmensaje + "';window.close();", true);
        if (Session["redirec"].ToString() == "3") ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='../Visita/frmvisitanteEdit.aspx?visCodigo=" + Session["codigotempVisitante"].ToString() + "&mensajeRetornado=" + lblmensaje + "';window.close();", true);
        //if (Session["redirec"].ToString() == "4") ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='../VisitaPPL/frmingresonuevoVisitante.aspx?mensajeRetornado=" + lblmensaje + "';window.close();", true);
        if (Session["redirec"].ToString() == "4") Response.Redirect("frmingresonuevoVisitante.aspx?mensajeRetornado=" + lblmensaje);
        //if (Session["redirec"].ToString() == "5") ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='../VisitaPPL/frmvistpplCamaraNew.aspx?mensajeRetornado=" + lblmensaje + "';window.close();", true);
        if (Session["redirec"].ToString() == "5") Response.Redirect("frmvistpplCamaraNew.aspx?mensajeRetornado=" + lblmensaje);
        if (Session["redirec"].ToString() == "6") ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='../Funcionario/frmfuncionaingNew.aspx?mensajeRetornado=" + lblmensaje + "';window.close();", true);
        if (Session["redirec"].ToString() == "7") ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='../Funcionario/frmfuncionaEdit.aspx?codigofun=" + Session["codigotempVisitante"].ToString() + "&mensajeRetornado=" + lblmensaje + "';window.close();", true);
        //if (Session["redirec"].ToString() == "8") ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='../Funcionario/frmfuncionaingreNew.aspx?codigofun=" + Session["codigotempVisitante"].ToString() + "&mensajeRetornado=" + lblmensaje + "';window.close();", true);
        if (Session["redirec"].ToString() == "8") Response.Redirect("../Funcionario/frmfuncionaingreNew.aspx?codigofun=" + Session["codigotempVisitante"].ToString() + "&mensajeRetornado=" + lblmensaje);
        //if (Session["redirec"].ToString() == "9") ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='../Funcionario/frmfuncionatotIngre.aspx?mensajeRetornado=" + lblmensaje + "';window.close();", true);
        if (Session["redirec"].ToString() == "9") Response.Redirect("../Funcionario/frmfuncionatotIngre.aspx?mensajeRetornado=" + lblmensaje);
        if (Session["redirec"].ToString() == "10") ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='../Funcionario/frmvisitanteingNew.aspx?mensajeRetornado=" + lblmensaje + "';window.close();", true);
        if (Session["redirec"].ToString() == "11") ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='../Funcionario/frmvisitanteextEdit.aspx?codigofun=" + Session["codigotempVisitante"].ToString() + "&mensajeRetornado=" + lblmensaje + "';window.close();", true);
        //if (Session["redirec"].ToString() == "12") ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='../Funcionario/frmvistingtotNew.aspx?mensajeRetornado=" + lblmensaje + "';window.close();", true);
        if (Session["redirec"].ToString() == "12") Response.Redirect("../Funcionario/frmvistingtotNew.aspx?mensajeRetornado=" + lblmensaje);
        //if (Session["redirec"].ToString() == "13") ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='../Funcionario/frmvisitanteingresoNew.aspx?codigofun=" + Session["codigotempVisitante"].ToString() + "&mensajeRetornado=" + lblmensaje + "';window.close();", true);
        if (Session["redirec"].ToString() == "13") Response.Redirect("../Funcionario/frmvisitanteingresoNew.aspx?codigofun=" + Session["codigotempVisitante"].ToString() + "&mensajeRetornado=" + lblmensaje);
        //if (Session["redirec"].ToString() == "14") ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='../SalidaVisita/frmsalidavisNew.aspx?mensajeRetornado=" + lblmensaje + "';window.close();", true);
        if (Session["redirec"].ToString() == "14") Response.Redirect("../SalidaVisita/frmsalidavisNew.aspx?mensajeRetornado=" + lblmensaje);
    }
    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        Array.Resize(ref objparam, 1);
        objparam[0] = Session["MachineName"].ToString();
        dsData = fun.consultarDatos("spElimiHuellaSalir", objparam, Page, (String[])Session["constrring"]);
        ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.close();", true);
    }
    #endregion
}