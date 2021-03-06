﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;

public partial class Policias_WebCam_baseimg : System.Web.UI.Page
{
    #region variables
    Object[] objparam = new Object[1];
    DataSet dsData = new DataSet();
    Funciones fun = new Funciones();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        StreamReader reader = new StreamReader(Request.InputStream);
        String xmlData = Server.UrlDecode(reader.ReadToEnd());
        reader.Close();

        DateTime fhm = DateTime.Now;
        String fecha = fhm.ToString("yyyymmddMMss");

        Session["Imagename"] = Request["codigovisitante"] + "_" + fecha + ".png";
        String rutasrv = Server.MapPath(Session["rutafoto"].ToString());

        //Traer el nombre del foto anterior
        if (Session["fotoanterior"].ToString() != "")
        {
            if (File.Exists(rutasrv + Session["fotoanterior"].ToString()))
            {
                File.Delete(rutasrv + Session["fotoanterior"].ToString());
            }
        }
        Session["capturedImageURL"] = Server.MapPath(Session["rutafoto"].ToString() + Session["Imagename"].ToString());
        Array.Resize(ref objparam, 2);
        objparam[0] = Request["codigovisitante"].ToString();
        objparam[1] = Session["Imagename"].ToString();
        dsData = fun.consultarDatos("spInsFotoPolicia", objparam, Page, (String[])Session["constrring"]);
        drawing(xmlData.Replace("imgBase64=data:image/png;base64,", String.Empty), Session["capturedImageURL"].ToString());
    }

    protected void drawing(String base64, String filename)
    {
        using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write))
        {
            using (BinaryWriter bw = new BinaryWriter(fs))
            {
                Byte[] data = Convert.FromBase64String(base64);
                bw.Write(data);
                bw.Close();
            }
        }

    }
}