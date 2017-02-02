using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CookComputing.XmlRpc;
/// <summary>
/// Descripción breve de ListPPLXmlRpc
/// </summary>
[XmlRpcUrl("http://192.168.1.33:8069/xmlrpc/object")]
public interface ListPPLXmlRpc
{
    [XmlRpcMethod("execute")]
    Object list(String BaseDatos, int token, String Username, String object1, String method, Object[] domain);

    [XmlRpcMethod("execute")]
    XmlRpcStruct[] listComplete(String dataBase, int token, String user, String object1, String method, int[] ids, Object[] fields);

    [XmlRpcMethod("execute")]
    XmlRpcStruct[] writeValues(String dataBase, int token, String user, String object1, String method, int[] ids, XmlRpcStruct fields);
}