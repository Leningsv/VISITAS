using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Policias_frmpreIngresoMasivo : System.Web.UI.Page
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
            lbltitulo.Text = "Pre-Ingreso de Policias";
        else
            grdvDatos.DataSource = Session["DataSet"];
    }
    #endregion

    #region Botones y Eventos
    private int InsertarPolicia(string tipoDocumento, string nroCedula, string nombres, string apellidos, string fechaNac)
    {
        if (tipoDocumento.Trim() == "" || nroCedula.Trim() == "" || nombres.Trim() == "" || apellidos.Trim() == "") return 3;//falta informacion

        if (tipoDocumento.Substring(0, 1).ToUpper() == "C")
        {
            if (!fun.cedulaBienEscrita(nroCedula)) return 2; //cedula incorrecta
        }

        if (!fun.IsFechaNacimiento(fechaNac)) return 4; //fecha incorrecta

        Array.Resize(ref objparam, 17);
        objparam[0] = tipoDocumento.Substring(0, 1).ToUpper();
        objparam[1] = nroCedula;
        objparam[2] = nombres.ToUpper();
        objparam[3] = apellidos.ToUpper();
        objparam[4] = fechaNac;
        objparam[5] = "01/01/9999";
        objparam[6] = "01/01/9999";
        objparam[7] = "";//rango
        objparam[8] = "";//area
        objparam[9] = "";//celular
        objparam[10] = "";//observacion
        objparam[11] = "PI";//estado
        objparam[12] = true;//estatus
        objparam[13] = "";//acceso
        objparam[14] = "";//foto
        objparam[15] = "";//huella
        objparam[16] = Session["usuCodigo"].ToString();
        dsData = fun.consultarDatos("spInsPoliciaMasivo", objparam, Page, (String[])Session["constrring"]);

        if (dsData.Tables[0].Rows[0][0].ToString() == "Existe") return 1; //ya existe
        else return 0; //correcto
    }

    protected void btngrabar_Click(object sender, ImageClickEventArgs e)
    {
        string TipoDoc, NroDoc, Nombres, Apellidos, FecNac;
        int CedInc = 0, YaExiste = 0, Correcto = 0, FaltaInf = 0, FechaInc = 0, validar;

        lblExito.Visible = false;

        DataTable ds = new DataTable();
        ds = (DataTable)grdvDatos.DataSource;

        foreach (GridViewRow row in grdvDatos.Rows)
        {
            TipoDoc = ds.Rows[row.RowIndex][0].ToString();
            NroDoc = ds.Rows[row.RowIndex][1].ToString();
            Nombres = ds.Rows[row.RowIndex][2].ToString();
            Apellidos = ds.Rows[row.RowIndex][3].ToString();
            FecNac = ds.Rows[row.RowIndex][4].ToString();

            validar = InsertarPolicia(TipoDoc, NroDoc, Nombres, Apellidos, FecNac);
            if (validar == 0)
            {
                Correcto++;
                ds.Rows[row.RowIndex][5] = "Guardado con Exito";
            }
            if (validar == 1)
            {
                YaExiste++;
                ds.Rows[row.RowIndex][5] = "Ya Exíste";
            }

            if (validar == 2)
            {
                CedInc++;
                ds.Rows[row.RowIndex][5] = "Cédula Incorrecta";
            }

            if (validar == 3)
            {
                FaltaInf++;
                ds.Rows[row.RowIndex][5] = "Falta Información";
            }

            if (validar == 4)
            {
                FechaInc++;
                ds.Rows[row.RowIndex][5] = "Formato de Fecha Incorrecta";
            }
        }

        if (CedInc > 0)
        {
            lblerror.Visible = true;
            lblerror.Text = CedInc.ToString() + " Cédula(s) incorrecta(s), No se guardaron";
        }

        if (YaExiste > 0)
        {
            if (CedInc > 0) lblerror.Text = lblerror.Text + "<br />" + YaExiste.ToString() + " Policía(s) ya existe(n), No se guardaron";
            else
            {
                lblerror.Visible = true;
                lblerror.Text = YaExiste.ToString() + " Policía(s) ya existe(n), No se guardaron";
            }
        }

        if (FaltaInf > 0)
        {
            if (CedInc > 0 || YaExiste > 0) lblerror.Text = lblerror.Text + "<br />" + FaltaInf.ToString() + " Registro(s) falta información, No se guardaron";
            else
            {
                lblerror.Visible = true;
                lblerror.Text = FaltaInf.ToString() + " Registro(s) falta información, No se guardaron";
            }
        }

        if (FechaInc > 0)
        {
            if (CedInc > 0 || YaExiste > 0 || FaltaInf > 0) lblerror.Text = lblerror.Text + "<br />" + FechaInc.ToString() + " Registro(s) con Fecha Incorrecta, No se guardaron";
            else
            {
                lblerror.Visible = true;
                lblerror.Text = FechaInc.ToString() + " Registro(s) con Fecha Incorrecta, No se guardaron";
            }
        }

        if (Correcto > 0)
        {
            lblExito.Visible = true;
            lblExito.Text = Correcto.ToString() + " Policía(s) Guardado(s) con Éxito";
        }

        grdvDatos.DataSource = ds;
        grdvDatos.DataBind();
    }

    protected void btnsalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Index1.aspx");
    }

    protected bool funCargaGrilla(string nombreArchivo)
    {
        string error = fun.importarExcel(grdvDatos, "Hoja1", nombreArchivo, (String[])Session["constrring"]);
        Session["DataSet"] = grdvDatos.DataSource;
        if (error != "OK")
        {
            lblerror.Visible = true;
            lblerror.Text = error.Substring(0, 200);//"Archivo Incorrecto!!!";
            return false;
        }
        else return true;
    }

    protected void btEliminarPla_Click(object sender, ImageClickEventArgs e)
    {
        //se debe limpiar la grilla y el campo del nombre del archivo
        grdvDatos.DataSource = null;
        grdvDatos.DataBind();
        lblerror.Visible = false;
        lblExito.Visible = false;
    }

    protected void btProcesar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblerror.Visible = false;
            lblExito.Visible = false;

            if (FileUpload1.PostedFile.FileName == "")
            {
                lblerror.Visible = true;
                lblerror.Text = "No seleccionaste ningun archivo";
            }
            else
            {
                //VERIFICAR LA EXTENSION
                string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                switch (extension.ToLower())
                {
                    //validas
                    case ".xls":
                    case ".xlsx":
                        break;

                    //no validas
                    default:
                        lblerror.Visible = true;
                        lblerror.Text = "Extensión no válida";
                        return;
                }
                //COPIAR EL ARCHIVO

                string archivo = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string carpeta_final = Path.Combine(Path.Combine(Request.PhysicalApplicationPath, "upload"), archivo);
                FileUpload1.PostedFile.SaveAs(carpeta_final);
                grdvDatos.Visible = true;
                lblDatosPolicias.Visible = true;
                if (funCargaGrilla(carpeta_final))
                {
                    lblExito.Visible = true;
                    lblExito.Text = "Archivo leido correctamente";
                }
            }
        }
        catch (Exception ex)
        {
            lblerror.Visible = true;
            lblerror.Text = "Error: " + ex.Message;
        }
    }
    #endregion
}