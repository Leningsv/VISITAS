using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Policias_CapturarHuellaPol : System.Web.UI.Page
{
    #region Variables
    DataSet dsData = new DataSet();
    Object[] objparam = new Object[1];
    Funciones fun = new Funciones();
    String ruta = @"C:\Temp\";
    String srvhuella = "";
    String template = "";
    String lblmensaje = "";
    #endregion

    #region Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Array.Resize(ref objparam, 8);
            objparam[0] = Session["MachineName"].ToString();
            objparam[1] = Request["CodigoPolicia"].ToString();
            objparam[2] = Session["tabla"].ToString();
            objparam[3] = Session["usuElimina"].ToString();
            objparam[4] = Session["canthuellas"].ToString();
            objparam[5] = "NUMHUE";
            objparam[6] = Session["capturar"].ToString();
            objparam[7] = Session["verificar"].ToString();
           
          

            dsData = fun.consultarDatos("spInserHuella", objparam, Page, (String[])Session["constrring"]);
        }
    }
    #endregion

    #region Botones y Eventos
    protected void btngrabar_Click(object sender, ImageClickEventArgs e)
    {
        Array.Resize(ref objparam, 4);
        objparam[0] = Session["MachineName"].ToString();
        objparam[1] = Request["CodigoPolicia"].ToString();
        objparam[2] = "Policia";
        objparam[3] = Session["usuCodigo"].ToString();
          
        dsData = fun.consultarDatos("spGrabHuella", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows[0][0].ToString() == "Exito") lblmensaje = "Huellas almacenadas con Éxito";
        else lblmensaje = "No se almancenó ninguna huella";
        //srvhuella = Server.MapPath(Session["rutahuella"].ToString());
        //if (Directory.Exists(ruta) && Directory.Exists(srvhuella))
        //{
        //    int archivos = 0;
        //    String valores = "";
        //    Array.Resize(ref objparam, 6);
        //    objparam[0] = Request["CodigoPolicia"].ToString();
        //    objparam[1] = Session["usuCodigo"].ToString();
        //    objparam[2] = Session["MachineName"].ToString();
        //    for (int i = 0; i <= 10; i++)
        //    {
        //        File.Delete(srvhuella + Request["CodigoPolicia"].ToString() + "_" + i.ToString() + ".jpg");
        //        if (File.Exists(ruta + i.ToString() + ".jpg"))
        //        {
        //            File.Move(ruta + i.ToString() + ".jpg", srvhuella + Request["CodigoPolicia"].ToString() + "_" + i.ToString() + ".jpg");
        //        }
        //        if (File.Exists(ruta + i.ToString() + ".txt"))
        //        {
        //            archivos++;
        //            valores = valores + "," + "1";
        //            template = File.ReadAllText(ruta + i.ToString() + ".txt");
        //            File.Delete(ruta + i.ToString() + ".txt");
        //            objparam[3] = i;
        //            objparam[4] = Request["CodigoPolicia"].ToString() + "_" + i.ToString() + ".jpg";
        //            objparam[5] = template;
        //            dsData = fun.consultarDatos("spInsHuellaPolicia", objparam, Page, (String[])Session["constrring"]);
        //            Session["Huella"] = "S";
        //        }
        //        else
        //        {
        //            valores = valores + "," + "0";
        //        }  
        //    }
        //    valores = valores.Substring(1);
        //    String[] createtext = { valores };
        //    File.WriteAllLines(ruta + "huellas.dat", createtext);
        //    if (archivos > 0)
        //    {
        //        Array.Resize(ref objparam, 1);
        //        objparam[0] = Request["CodigoPolicia"].ToString();
        //        dsData = fun.consultarDatos("spActHuellaPolicia", objparam, Page, (String[])Session["constrring"]);
        //        lblmensaje = "Huellas almacenadas con Éxito";
        //    }
        //    else lblmensaje = "No se almancenó ninguna huella";
        ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.opener.location='frmingPoliciaEdit.aspx?CodPol=" + Request["CodigoPolicia"].ToString() + "&mensajeRetornado=" + lblmensaje + "';window.close();", true);
        ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.close();", true);
        
        //}
    }

    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "pop", "javascript:window.close();", true);
    }
    #endregion
}