using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
/// <summary>
/// Descripción breve de Seguridad
/// </summary>
public class SIWinSeg
{
    SegPrincipal objfun = new SegPrincipal();
    public SIWinSeg(ref System.Windows.Forms.MainMenu objmenu, ref object objBDD, string strUserID, ref string strres)
    {

        SIBDDNET.BDD myConn = new SIBDDNET.BDD();
        DataSet dsData = new DataSet();
        DataSet dsDataMenu = new DataSet();
        Object[] objParametros;
        try
        {
            objParametros = new Object[1];


            objParametros[0] = strUserID;
            dsDataMenu = myConn.GetDatosXStoredProcedureXParametros("spCarMenXUsr", objParametros, ref objBDD, ref strres);
            if (strres == "c")
            {

                objfun.crea_menu(dsDataMenu.Tables[0], ref objmenu);
            }

        }
        catch (Exception ex)
        {
            strres = ex.ToString();
        }

    }

}
public class SIWebSeg
{
    SegPrincipal objfun = new SegPrincipal();
    public SIWebSeg(ref System.Web.UI.WebControls.TreeView objmenu, ref object objBDD, string strUserID, ref string strres)
    {
        SIBDDNET.BDD myConn = new SIBDDNET.BDD();

        DataSet dsData = new DataSet();
        DataSet dsDataMenu = new DataSet();
        Object[] objParametros;
        try
        {
            objParametros = new Object[1];
            ;
            objParametros[0] = strUserID;
            dsDataMenu = myConn.GetDatosXStoredProcedureXParametros("spCarMenXUsr", objParametros, ref objBDD, ref strres);
            if (strres == "c")
            {
                objfun.crea_menuweb(dsDataMenu.Tables[0], ref objmenu);

            }

        }
        catch (Exception ex)
        {
            strres = ex.ToString();
        }
    }

    public SIWebSeg()
    {
        // TODO: Complete member initialization
    }

}