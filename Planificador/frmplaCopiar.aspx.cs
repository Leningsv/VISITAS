using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Planificador_frmplaCopiar : System.Web.UI.Page
{
    #region Variables
    DataSet dsData = new DataSet();
    Object[] objparam = new Object[1];
    Funciones fun = new Funciones();
    DateTime DiaHoy = new DateTime();
    DateTime ffin = new DateTime();
    #endregion

    #region Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbltitulo.Text = "Copiar Planificación";
        }
    }
    #endregion

    #region Botones y Eventos
    protected void btnprocesar_Click(object sender, ImageClickEventArgs e)
    {
        //VALIDACIONES DE FECHAS
        if (!fun.IsDate(txtcopifechaini.Text))
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Fecha inicio origen de la copia Incorrecta');", true);
            return;
        }

        if (!fun.IsDate(txtcopifechahasta.Text))
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Fecha hasta origen de la copia Incorrecta');", true);
            return;
        }

        if (!fun.IsDate(txtpegafechaini.Text))
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Fecha inicio destino de la copia Incorrecta');", true);
            return;
        }

        if (!fun.IsDate(txtpegafechahasta.Text))
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Fecha fin destino de la copia Incorrecta');", true);
            return;
        }

        if (Convert.ToDateTime(txtcopifechahasta.Text) < Convert.ToDateTime(txtcopifechaini.Text))
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Rango Origen de Copia Incorrecto');", true);
            return;
        }

        if (Convert.ToDateTime(txtpegafechahasta.Text) < Convert.ToDateTime(txtpegafechaini.Text))
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Rango Destino de Copia Incorrecto');", true);
            return;
        }

        if (Convert.ToDateTime(txtpegafechaini.Text) < Convert.ToDateTime(txtcopifechaini.Text))
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Rango Destino de Copia Incorrecto');", true);
            return;
        }



        DateTime datfechaini = Convert.ToDateTime(txtcopifechaini.Text);
        DateTime datefechafin = Convert.ToDateTime(txtcopifechahasta.Text);
        DateTime datfechainicopia = Convert.ToDateTime(txtpegafechaini.Text);
        DateTime datfechafincopia = Convert.ToDateTime(txtpegafechahasta.Text);
        //buscar si ya existe rango de planificacion
        Array.Resize(ref objparam, 2);
        objparam[0] = txtpegafechaini.Text;
        objparam[1] = txtpegafechahasta.Text;
        dsData = fun.consultarDatos("spConsuPlanificaCabe", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count>0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ya existe una Planificación en el rago de fechas Destino');", true);
            return;
        }
        int intdiasdecopia;
        while (datfechainicopia < datfechafincopia)
        {
            CopiarPlanificador(datfechainicopia, datfechaini, datefechafin);
            TimeSpan timespan = (datefechafin - datfechaini);
            intdiasdecopia = Convert.ToInt16(timespan.TotalDays);
            datfechaini = datfechainicopia;
            datefechafin = datfechaini.AddDays(intdiasdecopia);
            datfechainicopia = datefechafin.AddDays(1);
        }
    }

    protected void CopiarPlanificador(DateTime parfechacopia, DateTime parfechadesde, DateTime parfechahasta)
    {
        String msj = "";
        int intTempDiaCopia;
        int intTemDiadesde;
        int intTemDiahasta;

        intTempDiaCopia = (int)parfechacopia.DayOfWeek;
        intTemDiadesde = (int)parfechadesde.DayOfWeek;
        intTemDiahasta = (int)parfechahasta.DayOfWeek;

        Array.Resize(ref objparam, 8);
        objparam[0] = parfechacopia;
        objparam[1] = parfechadesde;
        objparam[2] = parfechahasta;
        objparam[3] = txtpegafechaini.Text;
        objparam[4] = txtpegafechahasta.Text;
        objparam[5] = "NUEVA PLANIFICACION";
        objparam[6] = Session["usuCodigo"];
        objparam[7] = Session["MachineName"];
        dsData = fun.consultarDatos("spCopiarPlanificacion", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows[0][0].ToString()=="Exito") msj = "Copia Realizada con Éxito";
        else msj="No se encontró ningún dato en el rango de fecha a copiar";

        Response.Redirect("frmplaAdmin.aspx?mensajeRetornado=" + msj);   
    }

    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("frmplaAdmin.aspx");
    }
    #endregion
}