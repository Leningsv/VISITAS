using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class VisitaPPL_frmvistpplNew : System.Web.UI.Page
{
    #region Variables
    DataSet dsData = new DataSet();
    Object[] objparam = new Object[1];
    Funciones fun = new Funciones();
    #endregion

    #region Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["codigotempVisitante"] = null;
                Session["grabohuella"] = null;
                lbltitulo.Text = "Visitantes Autorizados por ppl ";

                lblerror.Visible = false;
                Session["codpplnew"] = Request["pplCodigo"];
                if (Request["mensajeRetornado"] != null)
                {
                    SIFunBasicas.Basicas.PresentarMensaje(Page, "::VISITA PPL::", Request["MensajeRetornado"]);
                }
                Array.Resize(ref objparam, 2);
                objparam[0] = 0;
                objparam[1] = "4";
                fun.cargarCombos(ddltipodoc, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);
                ddltipodoc.Items.RemoveAt(0);

                Array.Resize(ref objparam, 2);
                objparam[0] = 0;
                objparam[1] = "14";
                fun.cargarCombos(ddltipovisita, "spCargarParametros", objparam, Page, (String[])Session["constrring"]);

                //CARGAR EL TIEMPO DE GRACIA QUE TIENE EL VISITANTE
                Session["minutosGracia"] = "0";
                Array.Resize(ref objparam, 2);
                objparam[0] = "79";
                objparam[1] = "20";
                dsData = fun.consultarDatos("spCargaTotalParaDeta", objparam, Page, (String[])Session["constrring"]);
                if (fun.IsNumber(dsData.Tables[0].Rows[0][0].ToString())) Session["minutosGracia"] = dsData.Tables[0].Rows[0][0].ToString();

                Session["minutoregistro"] = "0";
                Array.Resize(ref objparam, 2);
                objparam[0] = "145";
                objparam[1] = "20";
                dsData = fun.consultarDatos("spCargaTotalParaDeta", objparam, Page, (String[])Session["constrring"]);
                if (fun.IsNumber(dsData.Tables[0].Rows[0][0].ToString())) Session["minutoregistro"] = dsData.Tables[0].Rows[0][0].ToString();

                Array.Resize(ref objparam, 1);
                objparam[0] = "80";
                dsData = fun.consultarDatos("spCargTiempGracia", objparam, Page, (String[])Session["constrring"]);
                if (dsData.Tables[0].Rows[0][0].ToString() == "SI")
                {
                    lbltitu1.Visible = true;
                    tblbuscar.Visible = true;
                    btnnuevo.Visible = true;
                }
                //CARGAR CUANTOAS VISITAS TIENE
                Array.Resize(ref objparam, 1);
                objparam[0] = Session["codpplnew"].ToString();
                dsData = fun.consultarDatos("spCargarCantVisitasPPL", objparam, Page, (String[])Session["constrring"]);
                Label19.Text = dsData.Tables[0].Rows[0][0].ToString();
                Label20.Text = dsData.Tables[0].Rows[0][1].ToString();
                Label21.Text = dsData.Tables[0].Rows[0][2].ToString();

                funCargaMantenimiento(Session["codpplnew"].ToString());
            }

        }
        catch (Exception ex)
        {
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", ex.Message);
        }
    }
    #endregion

    #region Funciones y Procedimientos
    protected void funCargaMantenimiento(String strCodPPl)
    {
        Session["horavis"] = "";
        String stretapa = "";
        Array.Resize(ref objparam, 1);
        objparam[0] = strCodPPl;
        dsData = fun.consultarDatos("spCargaPPLIndiVisita", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0)
        {
            txtnombres.Text = dsData.Tables[0].Rows[0][0].ToString() + ' ' + dsData.Tables[0].Rows[0][1].ToString();
            txtapellidos.Text = dsData.Tables[0].Rows[0][2].ToString() + ' ' + dsData.Tables[0].Rows[0][3].ToString();
            txtetapa.Text = dsData.Tables[0].Rows[0][4].ToString();
            txtpabellon.Text = dsData.Tables[0].Rows[0][5].ToString();
            stretapa = dsData.Tables[0].Rows[0][6].ToString();
            Session["etapappl"] = dsData.Tables[0].Rows[0][6].ToString();

            //Ingresar el menor de edad por etapa
            Array.Resize(ref objparam, 2);
            objparam[0] = "29";
            objparam[1] = Session["etapappl"].ToString();
            dsData = fun.consultarDatos("spCargMenorEtapa", objparam, Page, (String[])Session["constrring"]);
            Session["ingmenor"] = dsData.Tables[0].Rows[0][0].ToString();

            //Edad del menor de edad por etapa
            Array.Resize(ref objparam, 2);
            objparam[0] = "30";
            objparam[1] = Session["etapappl"].ToString();
            dsData = fun.consultarDatos("spCargEdadEtapa", objparam, Page, (String[])Session["constrring"]);
            Session["Edad"] = dsData.Tables[0].Rows[0][0].ToString();

            //CANTIDAD DE VISITAS SIMULTANEAS QUE PUEDE TENER EL PPL
            Array.Resize(ref objparam, 2);
            objparam[0] = "31";
            objparam[1] = Session["etapappl"].ToString();
            dsData = fun.consultarDatos("spCargMenorEtapa", objparam, Page, (String[])Session["constrring"]);
            if (fun.IsNumber(dsData.Tables[0].Rows[0][0].ToString()))
            {
                Session["numvisitaactual"] = int.Parse(dsData.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                Session["numvisitaactual"] = int.Parse("0");
            }
        }
        //CANTIDAD VISITAS FAMILIARES QUE PUEDE TENER
        Array.Resize(ref objparam, 2);
        objparam[0] = "16";
        objparam[1] = stretapa;
        dsData = fun.consultarDatos("spCargParamRela", objparam, Page, (String[])Session["constrring"]);
        Session["cantifam"] = dsData.Tables[0].Rows[0][0].ToString();

        //CANTIDAD VISITAS CONYUGALES QUE PUEDE TENER
        Array.Resize(ref objparam, 2);
        objparam[0] = "25";
        objparam[1] = stretapa;
        dsData = fun.consultarDatos("spCargParamRela", objparam, Page, (String[])Session["constrring"]);
        Session["canticon"] = dsData.Tables[0].Rows[0][0].ToString();

        //CANTIDAD VISITAS LEGALES QUE PUEDE TENER
        Array.Resize(ref objparam, 2);
        objparam[0] = "38";
        objparam[1] = stretapa;
        dsData = fun.consultarDatos("spCargParamRela", objparam, Page, (String[])Session["constrring"]);
        Session["cantilegal"] = dsData.Tables[0].Rows[0][0].ToString();

        Array.Resize(ref objparam, 1);
        objparam[0] = strCodPPl;
        dsData = fun.consultarDatos("spCargarvispplNew", objparam, Page, (String[])Session["constrring"]);
        grdvDatos.DataSource = dsData;
        grdvDatos.DataBind();
        Session["grdvDatos"] = grdvDatos.DataSource;

        Array.Resize(ref objparam, 1);
        objparam[0] = Session["codpplnew"].ToString();
        dsData = fun.consultarDatos("spCargPlanifiVisita", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows.Count > 0)
        {
            DateTime horaIniReg = Convert.ToDateTime(dsData.Tables[0].Rows[0][1]);
            DateTime horaFinReg = Convert.ToDateTime(dsData.Tables[0].Rows[0][2]);
            DateTime horaIni = Convert.ToDateTime(dsData.Tables[0].Rows[0][1]);
            DateTime horaFin = Convert.ToDateTime(dsData.Tables[0].Rows[0][2]);
            DateTime horactu = DateTime.Now;
            ddltipovisita.SelectedValue = dsData.Tables[0].Rows[0][3].ToString();
            horaIni = horaIni.AddMinutes(-(int.Parse(Session["minutoregistro"].ToString())));
            horaFin = horaFin.AddMinutes(int.Parse(Session["minutosGracia"].ToString()));
            if (horactu > horaFin || horactu < horaIni)
            {
                lblerror.Text = "La Hora de visita es De: " + horaIni.ToString("HH:mm") + " - " + horaFin.ToString("HH:mm");
                lblerror.Visible = true;
                ddltipodoc.Enabled = false;
                txtnumdocu.Enabled = false;
                grdvDatos.Enabled = false;
                Session["horavis"] = "H";
            }
        }
        else
        {
            lblerror.Text = "No tiene planificada visitas para el día de hoy";
            lblerror.Visible = true;
            ddltipodoc.Enabled = false;
            txtnumdocu.Enabled = false;
            grdvDatos.Enabled = false;
            Session["horavis"] = "N";

        }
        Session["nombrevisita"] = ddltipovisita.SelectedItem;

    }

    private void funGrabarComboVisita(String strCodVis, DropDownList ddltipvisita, DropDownList ddlparent)
    {
        //ddlparent.Enabled = true;
        //Array.Resize(ref objparam, 8);
        //objparam[0] = Session["CodigoPPL"].ToString();
        //objparam[1] = strCodVis;
        //objparam[2] = ddltipvisita.SelectedValue;
        //objparam[3] = ddlparent.SelectedValue;
        //objparam[4] = int.Parse(Session["cantifam"].ToString());
        //objparam[5] = int.Parse(Session["canticon"].ToString());
        //objparam[6] = int.Parse(Session["cantifun"].ToString());
        //objparam[7] = 0;
        //dsData = fun.consultarDatos("spCamRelaciVisPPL", objparam, Page, (String[])Session["constrring"]);
        //if (dsData.Tables[0].Rows[0][0].ToString() == "Update")
        //{

        //}
        //else
        //{
        //    ddltipvisita.SelectedValue = dsData.Tables[0].Rows[0][0].ToString();
        //}
        //if (ddltipvisita.SelectedValue == "C")
        //{
        //    ddlparent.SelectedValue = "C";
        //    ddlparent.Enabled = false;
        //}
        //if (ddltipvisita.SelectedValue == "F")
        //{
        //    ddlparent.SelectedValue = "F";
        //    ddlparent.Enabled = false;
        //}
    }

    private void funAgregarNewVisita(String strCodigoVisita)
    {
        Array.Resize(ref objparam, 5);
        objparam[0] = Session["CodigoPPL"].ToString();
        objparam[1] = strCodigoVisita;
        objparam[2] = int.Parse(Session["cantifam"].ToString());
        objparam[3] = Session["usuCodigo"].ToString();
        objparam[4] = Session["MachineName"].ToString();
        dsData = fun.consultarDatos("spInserRelaciVistaAdmin", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows[0][0].ToString() == "Maxima")
        {
            lblerror.Text = "A superado la cantidad máxima de familiares";
            lblerror.Visible = true;
        }
        if (dsData.Tables[0].Rows[0][0].ToString() == "Existe")
        {
            lblerror.Text = "El Visitante ya está asociado al PPL";
            lblerror.Visible = true;
        }
    }
    #endregion

    #region Botones y Procedimientos
    protected void grdvDatos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ImageButton imgboton = new ImageButton();
        if (e.Row.RowIndex >= 0)
        {
            imgboton = (ImageButton)(e.Row.Cells[1].FindControl("imgrojo"));

            String strCodigo = grdvDatos.DataKeys[e.Row.RowIndex].Value.ToString();
            //buscar si el visitante ya se encuentra andetro y pintar la celda
            Array.Resize(ref objparam, 1);
            objparam[0] = strCodigo;
            dsData = fun.consultarDatos("spCarVisitinVisita", objparam, Page, (String[])Session["constrring"]);
            if (dsData.Tables[0].Rows[0][0].ToString() == "P" || dsData.Tables[0].Rows[0][0].ToString() == "C")
            {
                imgboton.Visible = true;
            }
        }
    }

    protected void btnnuevo_Click(object sender, ImageClickEventArgs e)
    {
        Session["codigotempVisitante"] = null;
        //PREGUNTAR SI TIENE LA CANTIDAD DE VISITANTES SEGUN PARAMETRO
        Array.Resize(ref objparam, 2);
        objparam[0] = Session["codpplnew"].ToString();
        objparam[1] = 1;
        dsData = fun.consultarDatos("spCarNumVisitasPPL", objparam, Page, (String[])Session["constrring"]);
        if (int.Parse(dsData.Tables[0].Rows[0][0].ToString()) >= int.Parse(Session["numvisitaactual"].ToString()))
        {
            String msj = "El PPL solo puede recibir " + Session["numvisitaactual"].ToString() + " Visita(s) familiares ";
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + msj + "')", true);
            return;
        }
        //CONSULTAR CANTIDAD DE VISITANTES RELACIONADO CON EL PPL SEGUN TIPO DE VISITA
        Array.Resize(ref objparam, 2);
        objparam[0] = Session["codpplnew"].ToString();
        objparam[1] = ddltipovisita.SelectedValue;
        dsData = fun.consultarDatos("spCarCantRelaPPL", objparam, Page, (String[])Session["constrring"]);
        if (ddltipovisita.SelectedValue == "M")
        {
            if (int.Parse(Session["cantifam"].ToString()) == int.Parse(dsData.Tables[0].Rows[0][0].ToString()))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('A superado la cantidad máxima de visitas familiares');", true);
                return;
            }
        }
        if (ddltipovisita.SelectedValue == "C")
        {
            if (int.Parse(Session["canticon"].ToString()) == int.Parse(dsData.Tables[0].Rows[0][0].ToString()))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('A superado la cantidad máxima de visitas conyugales');", true);
                return;
            }
        }

        if (ddltipovisita.SelectedValue == "A")
        {
            if (int.Parse(Session["cantilegal"].ToString()) == int.Parse(dsData.Tables[0].Rows[0][0].ToString()))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('A superado la cantidad máxima de visitas legales');", true);
                return;
            }
        }

        Session["tipovisConyugue"] = ddltipovisita.SelectedValue;
        //PERGUNTAR SI TIENE HORARIO DE VISITA
        if (Session["horavis"].ToString() == "H")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Está fuera del horario de visitas');", true);
            return;
        }
        if (Session["horavis"].ToString() == "N")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('No tiene planificada visitas para el día de hoy');", true);
            return;
        }        
        ScriptManager.RegisterStartupScript(this.uppbuscagrilla, GetType(), "Camara y Huella", "javascript: var posicion_x; var posicion_y; posicion_x=(screen.width/2)-(900/2); posicion_y=(screen.height/2)-(600/2); window.open('frmingresoporVisitanteNew.aspx" + "',null,'left=' + posicion_x + ', top=' + posicion_y + ', width=1024px, height=600px, status=no,resizable= yes, scrollbars=yes, toolbar=no, location=no, menubar=no,titlebar=0');", true);
    }

    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        Session["horavis"] = null;
        Response.Redirect("frmvisitaporpplAdmin.aspx");
    }

    protected void txtnumdocu_TextChanged(object sender, EventArgs e)
    {
        if (ddltipodoc.SelectedValue == "C")
        {
            if (Session["ssvalcedu"].ToString() == "SI")
            {
                Boolean correcto = fun.cedulaBienEscrita(txtnumdocu.Text);
                if (correcto == false)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Cédula Incorrecta');", true);
                    txtnumdocu.Text = "";
                    return;
                }
            }
        }

        //CONSULTAR CANTIDAD DE VISITANTES RELACIONADO CON EL PPL SEGUN TIPO DE VISITA
        Array.Resize(ref objparam, 2);
        objparam[0] = Session["codpplnew"].ToString();
        objparam[1] = ddltipovisita.SelectedValue;
        dsData = fun.consultarDatos("spCarCantRelaPPL", objparam, Page, (String[])Session["constrring"]);
        if (ddltipovisita.SelectedValue == "M")
        {
            if (int.Parse(Session["cantifam"].ToString()) == int.Parse(dsData.Tables[0].Rows[0][0].ToString()))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('A superado la cantidad máxima de visitas familiares');", true);
                return;
            }
        }
        if (ddltipovisita.SelectedValue == "C")
        {
            if (int.Parse(Session["canticon"].ToString()) == int.Parse(dsData.Tables[0].Rows[0][0].ToString()))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('A superado la cantidad máxima de visitas conyugales');", true);
                return;
            }
        }

        if (ddltipovisita.SelectedValue == "A")
        {
            if (int.Parse(Session["cantilegal"].ToString()) == int.Parse(dsData.Tables[0].Rows[0][0].ToString()))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('A superado la cantidad máxima de visitas legales');", true);
                return;
            }
        }

        Array.Resize(ref objparam, 5);
        objparam[0] = Session["codpplnew"].ToString();
        objparam[1] = txtnumdocu.Text;
        objparam[2] = ddltipovisita.SelectedValue;
        objparam[3] = Session["usuCodigo"].ToString();
        objparam[4] = Session["MachineName"].ToString();
        dsData = fun.consultarDatos("spCarVerifiVisitPPL", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows[0][0].ToString() == "Existe")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('El Visitante ya está asociado al PPL');", true);
            return;
        }
        if (dsData.Tables[0].Rows[0][0].ToString() == "NoExiste")
        {
            Session["numerdocunuew"] = txtnumdocu.Text;
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Visitante no Existe, Cree uno nuevo por favor');", true);
            txtnumdocu.Text = "";
            return;
        }

        Array.Resize(ref objparam, 1);
        objparam[0] = Session["codpplnew"].ToString();
        dsData = fun.consultarDatos("spCargarvispplNew", objparam, Page, (String[])Session["constrring"]);
        grdvDatos.DataSource = dsData;
        grdvDatos.DataBind();
        Session["grdvDatos"] = grdvDatos.DataSource;


    }

    protected void ddltipodoc_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtnumdocu.Text = "";
        if (ddltipodoc.SelectedValue == "C")
        {
            txtnumdocu_FilteredTextBoxExtender.FilterMode = AjaxControlToolkit.FilterModes.ValidChars;
            txtnumdocu_FilteredTextBoxExtender.InvalidChars = ".-";
            txtnumdocu_FilteredTextBoxExtender.FilterType = AjaxControlToolkit.FilterTypes.Numbers;
        }
        else
        {
            txtnumdocu_FilteredTextBoxExtender.FilterMode = AjaxControlToolkit.FilterModes.InvalidChars;
            txtnumdocu_FilteredTextBoxExtender.InvalidChars = ".-*/{{}}[[]]\\";
            txtnumdocu_FilteredTextBoxExtender.FilterType = AjaxControlToolkit.FilterTypes.Custom;
        }
    }

    protected void btnselect_Click(object sender, ImageClickEventArgs e)
    {
        lblerror.Visible = false;
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        int intIndex = gvRow.RowIndex;

        Session["VisCodigoin"] = grdvDatos.DataKeys[intIndex].Values["Codigo"];
        Session["tipovisConyugue"] = grdvDatos.DataKeys[intIndex].Values["TipoVisita"];

        //CUANTOS VISITANTES ESTAN ADENTRO
        Array.Resize(ref objparam, 2);
        objparam[0] = Session["codpplnew"].ToString();
        objparam[1] = 1;
        dsData = fun.consultarDatos("spCarNumVisitasPPL", objparam, Page, (String[])Session["constrring"]);
        if (int.Parse(dsData.Tables[0].Rows[0][0].ToString()) >= int.Parse(Session["numvisitaactual"].ToString()))
        {
            String msj = "El PPL solo puede recibir " + Session["numvisitaactual"].ToString() + " Visita(s) familiares ";
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + msj + "')", true);
            return;
        }

        Session["nDocumento"] = grdvDatos.Rows[intIndex].Cells[3].Text;
        Session["parentesco"] = grdvDatos.DataKeys[intIndex].Values["Parente"];

        Array.Resize(ref objparam, 1);
        objparam[0] = Session["VisCodigoin"].ToString();
        dsData = fun.consultarDatos("spCarVisitinVisita", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows[0][0].ToString() == "P" || dsData.Tables[0].Rows[0][0].ToString() == "C")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Visitante se encuentra sancionado');", true);
            return;
        }

        //CARGAR PLANIFICACION DE VISITA
        var strivisita = Session["tipovisConyugue"].ToString();
        if (ddltipovisita.SelectedValue == "C" && Session["tipovisConyugue"].ToString() == "M")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Este día está planificado para visita conyugal');", true);
            return;
        }
        if (ddltipovisita.SelectedValue == "L" && Session["tipovisConyugue"].ToString() != "L")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Este día está planificado para visita legal');", true);
            return;
        }

        //preguntar si tiene foto y huella y mandar a la pantalla frmvistpplCamaraNew
        Array.Resize(ref objparam, 1);
        objparam[0] = Session["VisCodigoin"].ToString();
        dsData = fun.consultarDatos("spCarValidaFoto", objparam, Page, (String[])Session["constrring"]);
        ScriptManager.RegisterStartupScript(this.uppbuscagrilla, GetType(), "Ingreso de Visita", "javascript: var posicion_x; var posicion_y; posicion_x=(screen.width/2)-(900/2); posicion_y=(screen.height/2)-(600/2); window.open('frmingresonuevoVisitante.aspx?codigovisitante=" + Session["VisCodigoin"].ToString() + "',null,'left=' + posicion_x + ', top=' + posicion_y + ', width=1024px, height=600px, status=no,resizable= yes, scrollbars=yes, toolbar=no, location=no, menubar=no');", true);
    }

    protected void btnbuscar_Click(object sender, ImageClickEventArgs e)
    {
        if (ddltipodoc.SelectedValue == "C")
        {
            if (Session["ssvalcedu"].ToString() == "SI")
            {
                Boolean correcto = fun.cedulaBienEscrita(txtnumdocu.Text);
                if (correcto == false)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Cédula Incorrecta');", true);
                    txtnumdocu.Text = "";
                    return;
                }
            }
        }

        //CONSULTAR CANTIDAD DE VISITANTES RELACIONADO CON EL PPL SEGUN TIPO DE VISITA
        Array.Resize(ref objparam, 2);
        objparam[0] = Session["codpplnew"].ToString();
        objparam[1] = ddltipovisita.SelectedValue;
        dsData = fun.consultarDatos("spCarCantRelaPPL", objparam, Page, (String[])Session["constrring"]);
        if (ddltipovisita.SelectedValue == "M")
        {
            if (int.Parse(Session["cantifam"].ToString()) == int.Parse(dsData.Tables[0].Rows[0][0].ToString()))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('A superado la cantidad máxima de visitas familiares');", true);
                return;
            }
        }
        if (ddltipovisita.SelectedValue == "C")
        {
            if (int.Parse(Session["canticon"].ToString()) == int.Parse(dsData.Tables[0].Rows[0][0].ToString()))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('A superado la cantidad máxima de visitas conyugales');", true);
                return;
            }
        }

        if (ddltipovisita.SelectedValue == "A")
        {
            if (int.Parse(Session["cantilegal"].ToString()) == int.Parse(dsData.Tables[0].Rows[0][0].ToString()))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('A superado la cantidad máxima de visitas legales');", true);
                return;
            }
        }

        Array.Resize(ref objparam, 5);
        objparam[0] = Session["codpplnew"].ToString();
        objparam[1] = txtnumdocu.Text;
        objparam[2] = ddltipovisita.SelectedValue;
        objparam[3] = Session["usuCodigo"].ToString();
        objparam[4] = Session["MachineName"].ToString();
        dsData = fun.consultarDatos("spCarVerifiVisitPPL", objparam, Page, (String[])Session["constrring"]);
        if (dsData.Tables[0].Rows[0][0].ToString() == "Existe")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('El Visitante ya está asociado al PPL');", true);
            return;
        }
        if (dsData.Tables[0].Rows[0][0].ToString() == "NoExiste")
        {
            Session["numerdocunuew"] = txtnumdocu.Text;
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Visitante no Existe, Cree uno nuevo por favor');", true);
            txtnumdocu.Text = "";
            return;
        }

        Array.Resize(ref objparam, 1);
        objparam[0] = Session["codpplnew"].ToString();
        dsData = fun.consultarDatos("spCargarvispplNew", objparam, Page, (String[])Session["constrring"]);
        grdvDatos.DataSource = dsData;
        grdvDatos.DataBind();
        Session["grdvDatos"] = grdvDatos.DataSource;
    }
    #endregion
    
}