using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CookComputing.XmlRpc;
/// <summary>
/// Descripción breve de LoginXmlRpc
/// </summary>
[XmlRpcUrl("http://192.168.1.33:8069/xmlrpc/common")]
/*[XmlRpcUrl("https://192.168.1.33:8069/xmlrpc/object")]*/
public interface LoginXmlRpc
{
    [XmlRpcMethod("login")]
    Object login(String BaseDatos, String User, String Password);
}