using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Data.OleDb;
using CookComputing.XmlRpc;
using System.Collections;
using System.IO;
using System.Xml;
/// <summary>
/// Descripción breve de Funciones
/// </summary>
public class Funciones
{
    #region Variables
    SIBDDNET.BDD objbdd = new SIBDDNET.BDD();
    SICriptoDotNet001.Criptografia objEncripta = new SICriptoDotNet001.Criptografia();
    String[] strparam = { "0" };
    Object objcon = new Object();
    DataSet dsData = new DataSet();
    ListItem listadatos = new ListItem();
    String strres = "";

    OleDbConnection conn;
    OleDbDataAdapter MyDataAdapter;
    DataTable dt;
    #endregion

    public Boolean ruta_bien_escrita(String rutaPagina)
    {
        if (rutaPagina.Length > 5)
            if (rutaPagina.Substring(rutaPagina.Length - 5, 5) == ".aspx")
                return true;
            else
                return false;
        else
            return false;
    }
    public string importarExcel(GridView dgv, String nombreHoja, String nombreArchivo, string[] strparam)
    {
        String ruta = nombreArchivo;
        try
        {
            conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;data source=" + ruta + ";Extended Properties='Excel 12.0 Xml;HDR=Yes'");
            MyDataAdapter = new OleDbDataAdapter("Select * from [" + nombreHoja + "$]", conn);
            dt = new DataTable("Table");
            MyDataAdapter.Fill(dt);
            AgregarNovedades(dt, strparam);
            dgv.DataSource = dt;
            dgv.DataBind();

            return "OK";
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }

    private void AgregarNovedades(DataTable dtPolicias, string[] strparam)
    {
        Object[] objparam = new Object[1];
        dtPolicias.Columns.Add("NOVEDADES");
        for (int i = 0; i < dtPolicias.Rows.Count; i++)
        {
            if (dtPolicias.Rows[i][0].ToString().Trim() == "" || dtPolicias.Rows[i][1].ToString().Trim() == "" || dtPolicias.Rows[i][2].ToString().Trim() == "" || dtPolicias.Rows[i][3].ToString().Trim() == "")
            {
                dtPolicias.Rows[i][5] = "Falta Información";
                continue;
            }

            if (dtPolicias.Rows[i][0].ToString().Substring(0, 1).ToUpper() == "C")
            {
                if (!cedulaBienEscrita(dtPolicias.Rows[i][1].ToString()))
                {
                    dtPolicias.Rows[i][5] = "Cédula Incorrecta";
                    continue;
                }
            }

            if (!IsDate(dtPolicias.Rows[i][4].ToString().Trim()))
            {
                dtPolicias.Rows[i][5] = "Formato de Fecha Incorrecta";
                continue;
            }

            Array.Resize(ref objparam, 1);
            objparam[0] = dtPolicias.Rows[i][1].ToString();
            dsData = consultarDatos("spVerificarPolicia", objparam, strparam);
            if (dsData.Tables[0].Rows[0][0].ToString() == "Existe")
            {
                dtPolicias.Rows[i][5] = "Ya Existe";
                continue;
            }
            else
            {
                dtPolicias.Rows[i][5] = "Correcto";
                continue;
            }
        }
    }

    public bool IsFechaNacimiento(string strFecha)
    {
        DateTime dtfechaNac;
        if (!IsDate(strFecha)) return false; //fecha incorrecta
        else
        {
            dtfechaNac = DateTime.Parse(strFecha);
            if (dtfechaNac < Convert.ToDateTime("01/01/1900") || (dtfechaNac > DateTime.Now.AddDays(-1))) return false;
            else return true;
        }
    }

    public bool IsDate(string strFecha)
    {
        bool bValid;
        try
        {
            DateTime myDT = DateTime.Parse(strFecha);
            bValid = true;
        }
        catch (Exception e)
        {
            bValid = false;
        }

        return bValid;
    }

    public string SecuencialSiguiente(string CodParametroSecuencial, string[] strparam)
    {
        try
        {
            Object[] objparam = new Object[1];
            Array.Resize(ref objparam, 1);
            objparam[0] = CodParametroSecuencial;
            dsData = consultarDatos("spSecuencialSiguiente", objparam, strparam);
            return dsData.Tables[0].Rows[0][0].ToString();
        }
        catch (Exception)
        {
            return "";
        }
    }

    public string SecuencialGeneral(string CodParametroSecuencial, string[] strparam)
    {
        try
        {
            Object[] objparam = new Object[1];
            Array.Resize(ref objparam, 1);
            objparam[0] = CodParametroSecuencial;
            dsData = consultarDatos("spSecuencialGeneral", objparam, strparam);
            return dsData.Tables[0].Rows[0][0].ToString();
        }
        catch (Exception)
        {
            return "";
        }
    }

    public DataSet CrearArchivodat(String sp_procedure, Object[] objparametros, System.Web.UI.Page Page, string[] strparam)
    {
        try
        {
            objbdd.subConectarBDD(strparam, ref objcon, ref strres);
            if (strres == "c")
            {
                dsData = objbdd.GetDatosXStoredProcedureXParametros(sp_procedure, objparametros, ref objcon, ref strres);
                if (strres == "c")
                {
                    objbdd.subDesconectarBDD(ref objcon, ref strres);
                    if (strres != "c")
                        SIFunBasicas.Basicas.PresentarMensaje(Page, "Error al Desconectar a la BDD", strres);
                }
                else
                    SIFunBasicas.Basicas.PresentarMensaje(Page, "Error al conectar a la BDD", strres);
            }
            else
            {
                objbdd.subDesconectarBDD(ref objcon, ref strres);
                if (strres != "c")
                    SIFunBasicas.Basicas.PresentarMensaje(Page, "Error al Desconectar a la BDD", strres);
            }
            return dsData;
        }
        catch (Exception ex)
        {
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", ex.Message);
            return null;
        }
    }
    public Boolean email_bien_escrito(String email)
    {
        String expresion;
        expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
        if (Regex.IsMatch(email, expresion))
        {
            if (Regex.Replace(email, expresion, String.Empty).Length == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public void cargarCombos(DropDownList ddl_combos, String sp_procedure, Object[] objparametros, System.Web.UI.Page Page, string[] strparam)
    {
        Int16 intCont = 0;
        try
        {
            objbdd.subConectarBDD(strparam, ref objcon, ref strres);
            if (strres == "c")
            {
                dsData = objbdd.GetDatosXStoredProcedureXParametros(sp_procedure, objparametros, ref objcon, ref strres);
                if (strres == "c")
                {
                    ddl_combos.Items.Clear();
                    listadatos.Text = "-Seleccione-";
                    listadatos.Value = "";
                    ddl_combos.Items.Add(listadatos);
                    while (intCont < dsData.Tables[0].Rows.Count)
                    {
                        ListItem listadatosx = new ListItem();
                        listadatosx.Text = dsData.Tables[0].Rows[intCont][1].ToString();
                        listadatosx.Value = dsData.Tables[0].Rows[intCont][0].ToString();
                        ddl_combos.Items.Add(listadatosx);
                        intCont++;
                    }
                    objbdd.subDesconectarBDD(ref objcon, ref strres);
                }
                else
                    SIFunBasicas.Basicas.PresentarMensaje(Page, "Error al conectar a la BDD", strres);
            }
            else
                SIFunBasicas.Basicas.PresentarMensaje(Page, "Error al conectar a la BDD", strres);
        }
        catch (Exception ex)
        {
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", ex.Message);
        }
    }

    public DataSet consultarDatos(String sp_procedure, Object[] objparametros, System.Web.UI.Page Page, string[] strparam)
    {
        try
        {
            objbdd.subConectarBDD(strparam, ref objcon, ref strres);
            if (strres == "c")
            {
                dsData = objbdd.GetDatosXStoredProcedureXParametros(sp_procedure, objparametros, ref objcon, ref strres);
                if (strres == "c")
                {
                    objbdd.subDesconectarBDD(ref objcon, ref strres);
                    if (strres != "c")
                        SIFunBasicas.Basicas.PresentarMensaje(Page, "Error al Desconectar a la BDD", strres);
                }
                else
                    SIFunBasicas.Basicas.PresentarMensaje(Page, "Error al conectar a la BDD", strres);
            }
            else
            {
                objbdd.subDesconectarBDD(ref objcon, ref strres);
                if (strres != "c")
                    SIFunBasicas.Basicas.PresentarMensaje(Page, "Error al Desconectar a la BDD", strres);
            }
            return dsData;
        }
        catch (Exception ex)
        {
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", ex.Message);
            return null;
        }
    }

    public DataSet consultarDatos(String sp_procedure, Object[] objparametros, string[] strparam)
    {
        try
        {
            objbdd.subConectarBDD(strparam, ref objcon, ref strres);
            if (strres == "c")
            {
                dsData = objbdd.GetDatosXStoredProcedureXParametros(sp_procedure, objparametros, ref objcon, ref strres);
                if (strres == "c")
                    objbdd.subDesconectarBDD(ref objcon, ref strres);
            }
            else
                objbdd.subDesconectarBDD(ref objcon, ref strres);
            return dsData;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public string ejecutarProcedimiento(String sp_procedure, Object[] objparametros, System.Web.UI.Page Page, string[] strparam)
    {
        try
        {
            objbdd.subConectarBDD(strparam, ref objcon, ref strres);
            if (strres == "c")
            {
                dsData = objbdd.GetDatosXStoredProcedureXParametros(sp_procedure, objparametros, ref objcon, ref strres);
                if (strres == "c")
                {
                    objbdd.subDesconectarBDD(ref objcon, ref strres);
                    return dsData.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Grabar", strres);
                    return "Error";
                }
            }
            else
            {
                SIFunBasicas.Basicas.PresentarMensaje(Page, "Error al Desconectar a la BDD", strres);
                return "Error";
            }
        }
        catch (Exception ex)
        {
            objbdd.subDesconectarBDD(ref objcon, ref strres);
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", ex.Message);
            return "Error";
        }
    }

    public String insertarDatos(String sp_procedure, Object[] objparametros, System.Web.UI.Page Page, string[] strparam)
    {
        try
        {
            objbdd.subConectarBDD(strparam, ref objcon, ref strres);
            if (strres == "c")
            {
                objbdd.SetDatosXStoredProcedureXParametros(sp_procedure, objparametros, ref objcon, ref strres);
                if (strres == "c")
                {
                    objbdd.subDesconectarBDD(ref objcon, ref strres);
                    return "Ok";
                }
                else
                {
                    SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Grabar", strres);
                    return "Error";
                }
            }
            else
            {
                SIFunBasicas.Basicas.PresentarMensaje(Page, "Error al Desconectar a la BDD", strres);
                return "Error";
            }
        }
        catch (Exception ex)
        {
            objbdd.subDesconectarBDD(ref objcon, ref strres);
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", ex.Message);
            return "Error";
        }
    }

    public string validarHoras(string horaInicio, string horaFin)
    {
        //Valida si el rango de horas son correctas
        //Retorna "OK" o el mensaje de error
        if (horaInicio == "__:__" || horaFin == "__:__")
            return "Debe ingresar las horas de Inicio y Fin";

        if (int.Parse(horaInicio.Substring(0, 2)) > 23 || int.Parse(horaInicio.Substring(3, 2)) > 59)
            return "Hora de Inicio incorrecto";

        if (int.Parse(horaFin.Substring(0, 2)) > 23 || int.Parse(horaFin.Substring(3, 2)) > 59)
            return "Hora Fin incorrecto";

        if (Convert.ToDateTime(horaFin) <= Convert.ToDateTime(horaInicio))
            return "La Hora de inicio debe ser menor a la Hora de fin";

        return "OK";
    }

    public String ValidarCampoClave(TextBox txtcampo, System.Web.UI.Page Page, String sp_procedure, Object[] objparametros, string[] strparam)
    {
        String strExiD = "";
        String strLongi = "6";
        String strCaraEsp = "";
        try
        {
            objbdd.subConectarBDD(strparam, ref objcon, ref strres);
            if (strres == "c")
            {
                dsData = objbdd.GetDatosXStoredProcedureXParametros(sp_procedure, objparametros, ref objcon, ref strres);
                if (dsData.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsData.Tables[0].Rows)
                    {
                        if (dr[2].ToString() == "1")
                        {
                            strExiD = dr[0].ToString();
                        }
                        if (dr[2].ToString() == "2")
                        {
                            strLongi = dr[0].ToString();
                        }
                        if (dr[2].ToString() == "3")
                        {
                            strCaraEsp = dr[0].ToString();
                        }
                    }
                }
                if (strExiD == "SI")
                {
                    if (strCaraEsp == "SI")
                    {
                        txtcampo.Attributes.Clear();
                    }
                    else if (strCaraEsp == "NO")
                    {
                        txtcampo.Attributes.Add("onkeypress", "javascript:isNumberKey('" + txtcampo.ClientID + "')");
                    }
                }
                if (strres == "c")
                {
                    objbdd.subDesconectarBDD(ref objcon, ref strres);
                }
                else
                {
                    SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Grabar", strres);
                }
            }
            else
            {
                SIFunBasicas.Basicas.PresentarMensaje(Page, "Error al Desconectar a la BDD", strres);
            }
        }
        catch (Exception ex)
        {
            objbdd.subDesconectarBDD(ref objcon, ref strres);
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", ex.Message);
        }
        return strExiD + "|" + strLongi;
    }

    public int Validar_Ping(String txtvalor, System.Web.UI.Page Page, Int16 numping)
    {
        Int16 valor = 0;
        try
        {

            IPAddress ip = IPAddress.Parse(txtvalor);
            Ping ping = new Ping();
            for (int i = 0; i < 1; i++)
            {
                PingReply pr = ping.Send(ip);

                if (pr.Status.ToString() == "Success")
                {
                    valor = 1;
                }
                else
                {
                    valor = 0;
                }
            }
        }
        catch (Exception ex)
        {
            valor = 0;
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", ex.Message);
        }
        return valor;
    }

    public Boolean cedulaBienEscrita(String cedula)
    {
        int isNumeric;
        var total = 0;
        const int tamanoLongitudCedula = 10;
        int[] coeficientes = { 2, 1, 2, 1, 2, 1, 2, 1, 2 };
        const int numeroProvincias = 24;
        const int tercerDigito = 6;

        if (int.TryParse(cedula, out isNumeric) && cedula.Length == tamanoLongitudCedula)
        {
            var provincia = Convert.ToInt32(string.Concat(cedula[0], cedula[1], string.Empty));
            var digitoTres = Convert.ToInt32(cedula[2] + string.Empty);
            if ((provincia > 0 && provincia <= numeroProvincias) && digitoTres < tercerDigito)
            {
                var digitoVerificadorRecibido = Convert.ToInt32(cedula[9] + string.Empty);
                for (var f = 0; f < coeficientes.Length; f++)
                {
                    var valor = Convert.ToInt32(coeficientes[f] + string.Empty) * Convert.ToInt32(cedula[f] + string.Empty);
                    total = valor >= 10 ? total + (valor - 9) : total + valor;
                }
                var digitoVerificadorObtenido = total >= 10 ? (total % 10) != 0 ? 10 - (total % 10) : (total % 10) : total;
                return digitoVerificadorObtenido == digitoVerificadorRecibido;
            }
            return false;
        }
        return false;
    }

    public bool VerificaCedula(String strCedula)
    {
        char[] vector = strCedula.ToArray();
        int sumatotal = 0;
        if (vector.Length == 10)
        {
            int digito = Convert.ToInt32(vector[9].ToString());
            for (int i = 0; i < vector.Length - 1; i++)
            {
                int numero = Convert.ToInt32(vector[i].ToString());
                if ((i + 1) % 2 == 1)
                {
                    numero = Convert.ToInt32(vector[i].ToString()) * 2;
                    if (numero > 9) numero = numero - 9;
                }
                sumatotal += numero;
            }
            int digito_verifica = 10 - (sumatotal % 10);
            if (digito == digito_verifica) return true;
            else return false;
        }
        else return false;
    }

    public bool IsNumber(string strNumero)
    {
        bool bValid;
        try
        {
            int myNU = int.Parse(strNumero);
            bValid = true;
        }
        catch (Exception e)
        {
            bValid = false;
        }

        return bValid;
    }

    public string ConvertSortDirection(SortDirection sortDirection)
    {
        string newSortDirection = String.Empty;

        switch (sortDirection)
        {
            case SortDirection.Ascending:
                newSortDirection = "ASC";
                break;

            case SortDirection.Descending:
                newSortDirection = "DESC";
                break;
        }

        return newSortDirection;
    }

    public String ValidarMenorEdad(System.Web.UI.Page Page, String sp_procedure, Object[] objparametros, string[] strparam)
    {
        String strExiD = "NO";
        Int16 intedad = 0;
        try
        {
            objbdd.subConectarBDD(strparam, ref objcon, ref strres);
            if (strres == "c")
            {
                dsData = objbdd.GetDatosXStoredProcedureXParametros(sp_procedure, objparametros, ref objcon, ref strres);
                if (dsData.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsData.Tables[0].Rows)
                    {
                        if (dr[2].ToString() == "51")
                        {
                            strExiD = dr[0].ToString();
                        }
                        if (dr[2].ToString() == "52")
                        {
                            intedad = Convert.ToInt16(dr[0].ToString());
                        }
                    }
                }
                if (strres == "c")
                {
                    objbdd.subDesconectarBDD(ref objcon, ref strres);
                }
                else
                {
                    SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Grabar", strres);
                }
            }
            else
            {
                SIFunBasicas.Basicas.PresentarMensaje(Page, "Error al Desconectar a la BDD", strres);
            }
        }
        catch (Exception ex)
        {
            objbdd.subDesconectarBDD(ref objcon, ref strres);
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", ex.Message);
        }
        return strExiD + "|" + intedad.ToString();
    }

    public String PoliticasVisitas(System.Web.UI.Page Page, String sp_procedure, Object[] objparametros, string[] strparam)
    {
        String cantfam = "";
        String cantCon = "";
        String cantFun = "";

        try
        {
            objbdd.subConectarBDD(strparam, ref objcon, ref strres);
            if (strres == "c")
            {
                dsData = objbdd.GetDatosXStoredProcedureXParametros(sp_procedure, objparametros, ref objcon, ref strres);
                if (dsData.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsData.Tables[0].Rows)
                    {
                        if (dr[2].ToString() == "45")
                        {
                            cantfam = dr[0].ToString();
                        }
                        if (dr[2].ToString() == "46")
                        {
                            cantCon = dr[0].ToString();
                        }
                        if (dr[2].ToString() == "47")
                        {
                            cantFun = dr[0].ToString();
                        }
                    }
                }
                if (strres == "c")
                {
                    objbdd.subDesconectarBDD(ref objcon, ref strres);
                }
                else
                {
                    SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Grabar", strres);
                }
            }
            else
            {
                SIFunBasicas.Basicas.PresentarMensaje(Page, "Error al Desconectar a la BDD", strres);
            }
        }
        catch (Exception ex)
        {
            objbdd.subDesconectarBDD(ref objcon, ref strres);
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", ex.Message);
        }
        return cantfam + "|" + cantCon + "|" + cantFun;
    }

    public String ObtenerRutas(System.Web.UI.Page Page, String sp_procedure, Object[] objparametros, string[] strparam)
    {
        String rutacamara = "";
        String rutahuella = "";

        try
        {
            objbdd.subConectarBDD(strparam, ref objcon, ref strres);
            if (strres == "c")
            {
                dsData = objbdd.GetDatosXStoredProcedureXParametros(sp_procedure, objparametros, ref objcon, ref strres);
                if (dsData.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsData.Tables[0].Rows)
                    {
                        if (dr[2].ToString() == "23")
                        {
                            rutacamara = dr[0].ToString();
                        }
                        if (dr[2].ToString() == "24")
                        {
                            rutahuella = dr[0].ToString();
                        }
                    }
                }
                if (strres == "c")
                {
                    objbdd.subDesconectarBDD(ref objcon, ref strres);
                }
                else
                {
                    SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Grabar", strres);
                }
            }
            else
            {
                SIFunBasicas.Basicas.PresentarMensaje(Page, "Error al Desconectar a la BDD", strres);
            }
        }
        catch (Exception ex)
        {
            objbdd.subDesconectarBDD(ref objcon, ref strres);
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", ex.Message);
        }
        return rutacamara + "|" + rutahuella;
    }

    public String ObtenerRutasFun(System.Web.UI.Page Page, String sp_procedure, Object[] objparametros, string[] strparam)
    {
        String rutacamara = "";
        String rutahuella = "";

        try
        {
            objbdd.subConectarBDD(strparam, ref objcon, ref strres);
            if (strres == "c")
            {
                dsData = objbdd.GetDatosXStoredProcedureXParametros(sp_procedure, objparametros, ref objcon, ref strres);
                if (dsData.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsData.Tables[0].Rows)
                    {
                        if (dr[2].ToString() == "77")
                        {
                            rutacamara = dr[0].ToString();
                        }
                        if (dr[2].ToString() == "78")
                        {
                            rutahuella = dr[0].ToString();
                        }
                    }
                }
                if (strres == "c")
                {
                    objbdd.subDesconectarBDD(ref objcon, ref strres);
                }
                else
                {
                    SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Grabar", strres);
                }
            }
            else
            {
                SIFunBasicas.Basicas.PresentarMensaje(Page, "Error al Desconectar a la BDD", strres);
            }
        }
        catch (Exception ex)
        {
            objbdd.subDesconectarBDD(ref objcon, ref strres);
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", ex.Message);
        }
        return rutacamara + "|" + rutahuella;
    }

    public String ObtenerRutasPol(System.Web.UI.Page Page, String sp_procedure, Object[] objparametros, string[] strparam)
    {
        String rutacamara = "";
        String rutahuella = "";

        try
        {
            objbdd.subConectarBDD(strparam, ref objcon, ref strres);
            if (strres == "c")
            {
                dsData = objbdd.GetDatosXStoredProcedureXParametros(sp_procedure, objparametros, ref objcon, ref strres);
                if (dsData.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsData.Tables[0].Rows)
                    {
                        if (dr[2].ToString() == "115") rutacamara = dr[0].ToString();
                        if (dr[2].ToString() == "116") rutahuella = dr[0].ToString();
                    }
                }
                if (strres == "c") objbdd.subDesconectarBDD(ref objcon, ref strres);
                else SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Grabar", strres);
            }
            else SIFunBasicas.Basicas.PresentarMensaje(Page, "Error al Desconectar a la BDD", strres);
        }
        catch (Exception ex)
        {
            objbdd.subDesconectarBDD(ref objcon, ref strres);
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", ex.Message);
        }
        return rutacamara + "|" + rutahuella;
    }

    public String ObtenerRutasPPL(System.Web.UI.Page Page, String sp_procedure, Object[] objparametros, string[] strparam)
    {
        String rutacamara = "";
        //String rutahuella = "";

        try
        {
            objbdd.subConectarBDD(strparam, ref objcon, ref strres);
            if (strres == "c")
            {
                dsData = objbdd.GetDatosXStoredProcedureXParametros(sp_procedure, objparametros, ref objcon, ref strres);
                if (dsData.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsData.Tables[0].Rows)
                    {
                        if (dr[2].ToString() == "139") rutacamara = dr[0].ToString();
                        //if (dr[2].ToString() == "116") rutahuella = dr[0].ToString();
                    }
                }
                if (strres == "c") objbdd.subDesconectarBDD(ref objcon, ref strres);
                else SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Grabar", strres);
            }
            else SIFunBasicas.Basicas.PresentarMensaje(Page, "Error al Desconectar a la BDD", strres);
        }
        catch (Exception ex)
        {
            objbdd.subDesconectarBDD(ref objcon, ref strres);
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", ex.Message);
        }
        return rutacamara;
    }
    public String PoliticasIngresoVisita(System.Web.UI.Page Page, String sp_procedure, Object[] objparametros, string[] strparam)
    {
        String strIngVis = "NO";
        String strValCed = "NO";
        String strSolDocu = "NO";

        try
        {
            objbdd.subConectarBDD(strparam, ref objcon, ref strres);
            if (strres == "c")
            {
                dsData = objbdd.GetDatosXStoredProcedureXParametros(sp_procedure, objparametros, ref objcon, ref strres);
                if (dsData.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsData.Tables[0].Rows)
                    {
                        if (dr[2].ToString() == "55")
                        {
                            strIngVis = dr[0].ToString();
                        }
                        if (dr[2].ToString() == "56")
                        {
                            strValCed = dr[0].ToString();
                        }
                        if (dr[2].ToString() == "63")
                        {
                            strSolDocu = dr[0].ToString();
                        }

                    }
                }
                if (strres == "c")
                {
                    objbdd.subDesconectarBDD(ref objcon, ref strres);
                }
                else
                {
                    SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Grabar", strres);
                }
            }
            else
            {
                SIFunBasicas.Basicas.PresentarMensaje(Page, "Error al Desconectar a la BDD", strres);
            }
        }
        catch (Exception ex)
        {
            objbdd.subDesconectarBDD(ref objcon, ref strres);
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", ex.Message);
        }
        return strIngVis + "|" + strValCed + "|" + strSolDocu;
    }

    public static async void StartClient(IPAddress ipdireccion, int port, System.Web.UI.Page Page)
    {
        var client = new TcpClient();
        try
        {
            await client.ConnectAsync(ipdireccion, port);
        }
        catch (Exception ex)
        {
            SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Cargar Datos", ex.Message);

        }
    }

    public DataSet PPL_Datos_ESIGPEN(String BaseDatos, String Usuario, String Clave, String Objeto, Object condicion, Object[] campos,
        String Username, String Terminal, System.Web.UI.Page Page, string[] strparam)
    {
        try
        {
            String nombres = "", apellidos = "", nom_etapa = "", nom_pabellon = "", nom_ala = "", nom_piso="",nom_location="",id_ppl = "", id_etapa = "", id_pabellon = "", id_ala = "",piso_id="",location_id="";
            Boolean permitevis = false;
            int token = 0;
            LoginXmlRpc proxy = XmlRpcProxyGen.Create<LoginXmlRpc>();
            Object obj = proxy.login(BaseDatos, Usuario, Clave);
            if (IsNumber(obj.ToString())) token = (int)obj;
            else token = 0;

            if (token != 0)
            {
                ListPPLXmlRpc proxy1 = XmlRpcProxyGen.Create<ListPPLXmlRpc>();
                obj = proxy1.list(BaseDatos, token, Clave, Objeto, "search", new Object[] { condicion });
                try
                {
                    int[] res = ((int[])obj);
                    //INSERTAR INICIO DE LA CONSULTA AL ESIGPEN
                    objbdd.subConectarBDD(strparam, ref objcon, ref strres);
                    if (strres == "c")
                    {
                        Object[] objparam = new Object[1];
                        Array.Resize(ref objparam, 7);
                        objparam[0] = "ESIGPEN";
                        objparam[1] = "WEB-SERVICE";
                        objparam[2] = "frmvisitaporpplAdmin.aspx";
                        objparam[3] = "Consumo WS";
                        objparam[4] = "INICIO CONSULTA AL WEB-SERVICE";
                        objparam[5] = Username;
                        objparam[6] = Terminal;
                        dsData = objbdd.GetDatosXStoredProcedureXParametros("spInserLOGAcciones", objparam, ref objcon, ref strres);
                    }
                    objbdd.subDesconectarBDD(ref objcon, ref strres);

                    XmlRpcStruct[] response = proxy1.listComplete(BaseDatos, token, Clave, Objeto, "read", res, campos);

                    //FIN DE LA CONSULTA AL ESIGPEN
                    objbdd.subConectarBDD(strparam, ref objcon, ref strres);
                    if (strres == "c")
                    {
                        Object[] objparam = new Object[1];
                        Array.Resize(ref objparam, 7);
                        objparam[0] = "ESIGPEN";
                        objparam[1] = "WEB-SERVICE";
                        objparam[2] = "frmvisitaporpplAdmin.aspx";
                        objparam[3] = "Consumo WS";
                        objparam[4] = "FIN CONSULTA AL WEB-SERVICE";
                        objparam[5] = Username;
                        objparam[6] = Terminal;
                        dsData = objbdd.GetDatosXStoredProcedureXParametros("spInserLOGAcciones", objparam, ref objcon, ref strres);
                    }
                    objbdd.subDesconectarBDD(ref objcon, ref strres);
                    
                    foreach (XmlRpcStruct resp in response)
                    {
                        IDictionaryEnumerator en = resp.GetEnumerator();
                        while (en.MoveNext())
                        {
                            if (en.Key.ToString() == "id") id_ppl = en.Value.ToString();
                            if (en.Key.ToString() == "name") nombres = en.Value.ToString();
                            if (en.Key.ToString() == "last_name") apellidos = en.Value.ToString();
                            if (en.Key.ToString() == "etapa_id")
                            {
                                try
                                {
                                    ((IEnumerable)en.Value).Cast<object>().Select(x => x.ToString()).ToArray();
                                    String[] arreglo = ((IEnumerable)en.Value).Cast<object>().Select(x => x.ToString()).ToArray();
                                    id_etapa = arreglo[0].ToString();
                                    nom_etapa = arreglo[1].ToString();
                                }
                                catch (Exception)
                                { 
                                    id_etapa = "0";
                                    nom_etapa = "Transitoria";
                                }

                            }
                            if (en.Key.ToString() == "pabellon_id")
                            {
                                try
                                {
                                    String[] arreglo = ((IEnumerable)en.Value).Cast<object>().Select(x => x.ToString()).ToArray();
                                    id_pabellon = arreglo[0].ToString();
                                    nom_pabellon = arreglo[1].ToString();
                                }
                                catch (Exception)
                                {
                                    id_pabellon = "0";
                                    nom_pabellon = "Transitoria";
                                }
                            }
                            if (en.Key.ToString() == "ala_id")
                            {
                                try
                                {
                                    String[] arreglo = ((IEnumerable)en.Value).Cast<object>().Select(x => x.ToString()).ToArray();
                                    id_ala = arreglo[0].ToString();
                                    nom_ala = arreglo[1].ToString();
                                }
                                catch (Exception)
                                {
                                    id_ala = "0";
                                    nom_ala = "Transitoria";
                                }
                            }

                            if (en.Key.ToString() == "piso_id")
                            {
                                try
                                {
                                    String[] arreglo = ((IEnumerable)en.Value).Cast<object>().Select(x => x.ToString()).ToArray();
                                    piso_id = arreglo[0].ToString();
                                    nom_piso = arreglo[1].ToString();
                                }
                                catch (Exception)
                                {
                                    piso_id = "0";
                                    nom_piso = "Transitoria";
                                }
                            }
                            if (en.Key.ToString() == "location_id")
                            {
                                try
                                {
                                    String[] arreglo = ((IEnumerable)en.Value).Cast<object>().Select(x => x.ToString()).ToArray();
                                    location_id = arreglo[0].ToString();
                                    nom_location = arreglo[1].ToString();
                                }
                                catch (Exception)
                                {
                                    location_id = "0";
                                    nom_location = "Transitoria";
                                }
                            }

                            if (en.Key.ToString() == "permite_visitas") permitevis = (Boolean)en.Value;
                        }
                        String todo = id_ppl.ToString() + "|" + nombres + "|" + apellidos + "|" + id_etapa.ToString();
                        if (todo != "")
                        {
                            objbdd.subConectarBDD(strparam, ref objcon, ref strres);
                            if (strres == "c")
                            {
                                Object[] objparam = new Object[1];
                                Array.Resize(ref objparam, 22);
                                objparam[0] = id_ppl; //codigo del ppl
                                objparam[1] = "1"; // codigo del crs
                                objparam[2] = "C"; //tipo de documento Cedula
                                objparam[3] = ""; //numero de documento
                                objparam[4] = nombres;
                                objparam[5] = "";//NOMBRE2
                                objparam[6] = apellidos;
                                objparam[7] = "";//Apellidos
                                objparam[8] = id_etapa;
                                objparam[9] = id_pabellon;
                                objparam[10] = id_ala;
                                objparam[11] = piso_id;//PISO
                                objparam[12] = location_id;//CELDA
                                objparam[13] = ""; // nom_etapa + "-" + nom_pabellon + "-" + nom_ala;
                                objparam[14] = "1";//1 = ACTIVO
                                objparam[15] = "";//FOTO
                                objparam[16] = Username;
                                objparam[17] = Terminal;
                                objparam[18] = permitevis;
                                objparam[19] = nom_etapa;
                                objparam[20] = nom_pabellon;
                                objparam[21] = nom_ala;

                                dsData = objbdd.GetDatosXStoredProcedureXParametros("spInserPPLDatos", objparam, ref objcon, ref strres);
                                if (strres == "c")
                                {
                                    objbdd.subDesconectarBDD(ref objcon, ref strres);
                                    //return dsData;
                                }
                                else
                                {
                                    SIFunBasicas.Basicas.PresentarMensaje(Page, "Ocurrió un Error al Grabar", strres);
                                    dsData = null;
                                }
                            }
                            else
                            {
                                SIFunBasicas.Basicas.PresentarMensaje(Page, "Error al Desconectar a la BDD", strres);
                                dsData = null;
                            }

                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    objbdd.subConectarBDD(strparam, ref objcon, ref strres);
                    if (strres == "c")
                    {
                        Object[] objparam = new Object[1];
                        Array.Resize(ref objparam, 7);
                        objparam[0] = "ESIGPEN";
                        objparam[1] = "CONSULTA_PPL";
                        objparam[2] = "frmvisitaporpplAdmin.aspx";
                        objparam[3] = "Consultar PPL";
                        objparam[4] = "NO SE OBTUVO NINGUN REGISTRO CON LA CONSULTA ENVIADA";
                        objparam[5] = Username;
                        objparam[6] = Terminal;
                        dsData = objbdd.GetDatosXStoredProcedureXParametros("spInserLOGAcciones", objparam, ref objcon, ref strres);
                    }
                    objbdd.subDesconectarBDD(ref objcon, ref strres);
                }
            }
            else
            {
                objbdd.subConectarBDD(strparam, ref objcon, ref strres);
                if (strres == "c")
                {
                    Object[] objparam = new Object[1];
                    Array.Resize(ref objparam, 7);
                    objparam[0] = "Visitantes";
                    objparam[1] = "Ingreso Visita";
                    objparam[2] = "frmvisitaporpplAdmin.aspx";
                    objparam[3] = "Token ESIGPEN";
                    objparam[4] = "ERROR AL OBTENER TOKEN CON ESIGPEN";
                    objparam[5] = Username;
                    objparam[6] = Terminal;
                    dsData = objbdd.GetDatosXStoredProcedureXParametros("spInserLOGAcciones", objparam, ref objcon, ref strres);
                }
                objbdd.subDesconectarBDD(ref objcon, ref strres);
            }
        }
        catch (Exception ex)
        {
            objbdd.subConectarBDD(strparam, ref objcon, ref strres);
            if (strres == "c")
            {
                Object[] objparam = new Object[1];
                Array.Resize(ref objparam, 7);
                objparam[0] = "Visitantes";
                objparam[1] = "Ingreso Visita";
                objparam[2] = "frmvisitaporpplAdmin.aspx";
                objparam[3] = "Conectar ESIGPEN";
                objparam[4] = "ERROR AL CONECTAR CON ESIGPEN";
                objparam[5] = Username;
                objparam[6] = Terminal;
                dsData = objbdd.GetDatosXStoredProcedureXParametros("spInserLOGAcciones", objparam, ref objcon, ref strres);
                SIFunBasicas.Basicas.PresentarMensaje(Page, "Error al Desconectar a la BDD", ex.Message);
                dsData = null;
            }
            objbdd.subDesconectarBDD(ref objcon, ref strres);
        }
        return dsData;
    }

    public String DatosBSG_RegistroCivil(String cedula_fun, String url, String username, String password, String cedula_consulta)
    {
        

        String Nombres = string.Empty;
        String Direccion = string.Empty;
        String Genero = string.Empty;

        ws_acceso.AccesoBSGService ws = new ws_acceso.AccesoBSGService();
        ws_acceso.validarPermisoPeticion permiso = new ws_acceso.validarPermisoPeticion();
        
        permiso.Cedula = cedula_fun;
        permiso.Urlsw = url;

        ws_acceso.validarPermisoRespuesta respuesta = ws.ValidarPermiso(permiso);

        String urlString = "<soapenv:Envelope xmlns:soapenv=";
        urlString = urlString + "\"http://schemas.xmlsoap.org/soap/envelope/\" ";
        urlString = urlString + "xmlns:con=";
        urlString = urlString + "\"http://www.registrocivil.gob.ec/ConsultaCedula\"" + ">";
        urlString = urlString + "<soapenv:Header>";
        urlString = urlString + "<wss:Security xmlns:wss=";
        urlString = urlString + "\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd\"" + ">";
        urlString = urlString + "<wss:UsernameToken xmlns:wsu=";
        urlString = urlString + "\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd\"" + ">";
        urlString = urlString + "<wss:Username>" + cedula_fun + "</wss:Username>" + "<wss:Password Type=";
        urlString = urlString + "\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordDigest\"" + ">";
        urlString = urlString + respuesta.Digest + "</wss:Password>" + "<wss:Nonce EncodingType=";
        urlString = urlString + "\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary\"" + ">";
        urlString = urlString + respuesta.Nonce + "</wss:Nonce>" + "<wsu:Created xmlns:wsu=";
        urlString = urlString + "\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd\"" + ">";
        urlString = urlString + respuesta.Fecha + "</wsu:Created>" + "</wss:UsernameToken>" + "<wsu:Timestamp xmlns:wsu=";
        urlString = urlString + "\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd\"" + ">";
        urlString = urlString + "<wsu:Created>" + respuesta.Fecha + "</wsu:Created>" + "<wsu:Expires>" + respuesta.FechaF + "</wsu:Expires>";
        urlString = urlString + "</wsu:Timestamp>" + "</wss:Security>" + "</soapenv:Header>" + "<soapenv:Body>" + "<con:BusquedaPorCedula>";
        urlString = urlString + "<Cedula>" + cedula_consulta + "</Cedula>" + "<Usuario>" + username + "</Usuario>" + "<Contrasenia>";
        urlString = urlString + password + "</Contrasenia>" + "</con:BusquedaPorCedula>" + "</soapenv:Body>" + "</soapenv:Envelope>";

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "POST";
        request.ContentType = "application/soap+xml; charset=utf-8";
        request.ContentLength = urlString.Length;

        StreamWriter strmwrite = new StreamWriter(request.GetRequestStream());
        strmwrite.Write(urlString);
        strmwrite.Close();

        try
        {
            StreamReader strReader = new StreamReader(request.GetResponse().GetResponseStream());
            XmlTextReader reader = new XmlTextReader(strReader);
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // El nodo es un Elemento.
                        if (reader.Name.Equals("CalleDomicilio"))
                            Direccion = reader.ReadString();
                        //if (reader.Name.Equals("FechaNacimiento"))
                        if (reader.Name.Equals("Genero"))
                            Genero = reader.ReadString();
                        //if (reader.Name.Equals("NombreMadre"))
                        //if (reader.Name.Equals("NombrePadre"))
                        if (reader.Name.Equals("Nombre"))
                            Nombres = reader.ReadString();
                        break;
                    case XmlNodeType.Text: //Mostrar el texto en cada elemento.
                        break;
                    case XmlNodeType.EndElement: //Mostrar fin del elemento.
                        break;
                }                
            }
            
        }
        catch (Exception ex)
        {
            Console.Write(ex.Message);
        }
        return Nombres + "|" + Genero + "|" + Direccion;
    }
    

}