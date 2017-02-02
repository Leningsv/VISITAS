using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class SalidaVisita_frmsalidaverihuella : System.Web.UI.Page
{
    #region Variables
    DataSet dsData = new DataSet();
    DataSet dsDataTimer = new DataSet();
    DataSet dsDataUpdate = new DataSet();
    Object[] objparam = new Object[1];
    Funciones fun = new Funciones();
    #endregion

    #region Load
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #endregion

    #region Botones y Eventos
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        objparam[0] = Session["MachineName"].ToString();
        dsDataTimer = fun.consultarDatos("spVerificaHuellaVisitante", objparam, Page, (String[])Session["constrring"]);
        if (dsDataTimer.Tables[0].Rows.Count > 0)
        {
            if (dsDataTimer.Tables[0].Rows[0][0].ToString() == "LE") funCargarMatenimiento(dsDataTimer.Tables[0].Rows[0][1].ToString(), dsDataTimer.Tables[0].Rows[0][2].ToString());
            if (dsDataTimer.Tables[0].Rows[0][0].ToString() == "NR")
            {
                Label2.Text = "Presione el Botón para Activar el Lector";
                Label1.Visible = true;
                Timer1.Enabled = false;
            }
        }
    }

    protected void imgactivar_Click(object sender, ImageClickEventArgs e)
    {
        objparam[0] = Session["MachineName"].ToString();
        dsData = fun.consultarDatos("spEliminaHuellaVerifica", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows[0][0].ToString() == "Exito")
        {
            Timer1.Enabled = true;
            Label2.Text = "LECTOR ACTIVADO";
            Label1.Visible = false;
        }
        else SIFunBasicas.Basicas.PresentarMensaje(Page, "Error", "Ocurrió un Error en la Base de Datos");
    }
    #endregion

    #region Funciones y Procedimientos
    protected void funCargarMatenimiento(String strCodVisitante, String strTabla)
    {
        Array.Resize(ref objparam, 1);
        objparam[0] = Session["MachineName"].ToString();
        dsData = fun.consultarDatos("spEliminaHuellaVerifica", objparam, Page, (String[])Session["constrring"]);

        //BUSCAR EN LA TABLA DE VISITA
        Timer1.Enabled = false;
        Array.Resize(ref objparam, 3);
        objparam[0] = strCodVisitante;
        objparam[1] = DateTime.Now.ToString("dd/MM/yyyy");
        objparam[2] = strTabla;
        dsData = fun.consultarDatos("spCargaVisitaHuella", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0)
        {
            Label2.Text = "Presione el Botón para Activar el Lector";
            Label1.Visible = false;
            Session["CodVisitante"] = dsData.Tables[0].Rows[0][0].ToString();
            Session["Codigo_PPL"] = dsData.Tables[0].Rows[0][1].ToString();
            Session["Codigo_Visita"] = dsData.Tables[0].Rows[0][2].ToString();
            Session["tabla"] = strTabla;
            ScriptManager.RegisterStartupScript(this, GetType(), "Salida de Visitante", "javascript: var posicion_x; var posicion_y; posicion_x=(screen.width/2)-(900/2); posicion_y=(screen.height/2)-(600/2); window.open('frmsalidavisNew.aspx',null,'left=' + posicion_x + ', top=' + posicion_y + ', width=1024px, height=600px, status=no,resizable= yes, scrollbars=yes, toolbar=no, location=no, menubar=no');", true);
        }
        else
        {
            Label2.Text = "Presione el Botón para Activar el Lector";
            Label1.Visible = true;
        }
 
    }
    #endregion


}