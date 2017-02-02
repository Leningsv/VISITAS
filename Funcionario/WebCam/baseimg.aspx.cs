using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;

public partial class Funcionario_WebCam_baseimg : System.Web.UI.Page
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
        //String rutasrv = Session["rutafoto"].ToString();
        String rutasrv = Server.MapPath(Session["rutafoto"].ToString());

        //if (Session["fotoanterior"] != null)
        //{
        //    if (File.Exists(rutasrv + Session["fotoanterior"].ToString()))
        //    {
        //        File.Delete(rutasrv + Session["fotoanterior"].ToString());
        //    }
        //}

        Session["Imagename"] = Request["codigofuncio"] + "_" + fecha + ".png";

        Session["capturedImageURL"] = Server.MapPath(Session["rutafoto"].ToString() + Session["Imagename"].ToString());
        //Traer el nombre del foto anterior
        drawing(xmlData.Replace("imgBase64=data:image/png;base64,", String.Empty), Session["capturedImageURL"].ToString());
        Session["fotoanterior"] = Session["Imagename"].ToString();
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