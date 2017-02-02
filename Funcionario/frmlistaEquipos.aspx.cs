using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Funcionario_frmlistaEquipos : System.Web.UI.Page
{
    #region Variables
    DataSet dsData = new DataSet();
    Object[] objparam = new Object[1];
    Funciones fun = new Funciones();
    #endregion

    #region Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbltitulo.Text = "Lista de Equipos del " + Request["NombreAcceso"].ToString();
            funCargaMantenimiento(Request["codAcceso"].ToString());
        }
    }
    #endregion

    #region Funciones y Procedimientos
    protected void funCargaMantenimiento(String strCodigo)
    {
        Array.Resize(ref objparam, 1);
        objparam[0] = strCodigo;
        dsData = fun.consultarDatos("spCargarEquiposPerfil", objparam, Page, (String[])Session["constrring"]);
        grdvDatos.DataSource = dsData;
        grdvDatos.DataBind();
        Session["grdvDatos"] = grdvDatos.DataSource;
    }
    #endregion
}