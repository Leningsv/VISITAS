using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Request.Params["Respuesta"] == "1")
                {
                    Session["ValidaPantalla"] = "1";
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    if (((string)Session["ValidaPantalla"]) != "1")
                    {
                        Session.Clear();
                        Session["ValidaPantalla"] = "1";
                        ClientScript.RegisterStartupScript(GetType(), "valida", "window.onload = window_onload;", true);
                    }
                    else
                    {
                        Session["ValidaPantalla"] = "1";
                        Response.Redirect("Frm_Login.aspx");
                    }
                }
            }
            catch
            {
                if (((string)Session["ValidaPantalla"]) != "1")
                {
                    Session.Clear();
                    Session["ValidaPantalla"] = 1;
                    ClientScript.RegisterStartupScript(GetType(), "valida", "window.onload = window_onload;", true);
                }
            }
        }
    }
}