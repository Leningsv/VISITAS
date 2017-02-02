using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Menu : System.Web.UI.Page
{
    #region Variables
    SIBDDNET.BDD objbdd = new SIBDDNET.BDD();
    pry_visita.SIWebSeg objseg = new pry_visita.SIWebSeg();    
    String[] strparam = { "0" };
    Object objcon = new Object();
    String strres = "";
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                objbdd.subRegistroBDD("ESISEG", ref strparam, ref strres);
                if (strres == "c")
                {
                    objbdd.subConectarBDD(strparam, ref objcon, ref strres);
                    if (strres == "c")
                    {
                        Session["constrring"] = strparam;
                        Session["coneccion"] = objcon;
                    }
                    else
                    {
                        SIFunBasicas.Basicas.PresentarMensaje(Page, Title.ToString(), "Ocurrio un error al cargar el menu");
                    }
                    objseg = new pry_visita.SIWebSeg(ref trvmenu, ref objcon, Session["usuCodigo"].ToString(), ref strres);
                    if (strres != "c")
                    {
                        SIFunBasicas.Basicas.PresentarMensaje(Page, Title.ToString(), "Ocurrio un error al cargar el menu");
                    }
                    else
                    {
                        lblUser.Text = Session["nombre"].ToString();
                        lblFecha.Text = DateTime.Now.ToLongDateString();
                    }
                }
                objbdd.subDesconectarBDD(ref objcon, ref strres);
                if (strres != "c")
                {
                    SIFunBasicas.Basicas.PresentarMensaje(Page, Title.ToString(), "Ocurrio un error al cargar el menu");
                }
            }
        }
        catch
        {
            objbdd.subDesconectarBDD(ref objcon, ref strres);
            SIFunBasicas.Basicas.PresentarMensaje(Page, Title.ToString(), "Ocurrio un error al cargar el menu");
        }
    }
    protected void trvmenu_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            if (trvmenu.SelectedNode.NavigateUrl == "")
            {
                trvmenu.CollapseAll();
                trvmenu.SelectedNode.Expand();
            }
        }
        catch (Exception ex)
        {
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrio un error al cargar el MENU", ex.Message);
        }
    }
}