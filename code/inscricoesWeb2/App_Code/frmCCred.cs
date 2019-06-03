using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;




using MSXML2;

using System.Xml;

/// <summary>
/// Summary description for frmCCred
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService()]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class frmCCred : System.Web.Services.WebService {

    public frmCCred () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string CCred(string cdClienteCobreBem, string CdPedido, decimal VlTotalPedido, string txtQtdParcelas,
                        string txtNrCartao, string txtMesValidadeCartao, string txtAnoValidadeCartao, 
                        string txtCodSegCartao, string txtNomeTitularCartao)
    {
        string retorno = "";
        try
        {
            //HtmlString html = new HtmlString();
            XMLHTTP http = new XMLHTTP();

            http.open("POST", "https://www.aprovafacil.com/cgi-bin/APFW/" + cdClienteCobreBem + "/APC?" +
                "NumeroDocumento=" + CdPedido +
                "&ValorDocumento=" + VlTotalPedido.ToString().Replace(',', '.') +
                "&QuantidadeParcelas=" + txtQtdParcelas +
                "&NumeroCartao=" + txtNrCartao +
                "&MesValidade=" + txtMesValidadeCartao +
                "&AnoValidade=" + txtAnoValidadeCartao +
                "&CodigoSeguranca=" + txtCodSegCartao +
                "&NomePortadorCartao=" + txtNomeTitularCartao.ToUpper(), false, null, null);
            http.send(null);

            retorno = http.responseText;
            if (retorno.ToUpper().Contains("<TABLE"))
            {
                return "Problemas ao efetuar a transação!\nTente de novo se o problema continuar entre em contato com (61) 3486-1106.";
            }
            XmlDocument xmlDoc = new XmlDocument();


            xmlDoc.LoadXml(http.responseText);



            //Definimos a propriedade No como XmlNode a informarmos o XPath para ela que será ResultadoAPC
            XmlNode No = xmlDoc.SelectSingleNode("ResultadoAPC");

            if (No.ChildNodes.Item(0).InnerText.ToUpper() == "FALSE")
            {
                return //"Erro;" + 
                    No.ChildNodes.Item(2).InnerText;
            }



            XMLHTTP http2 = new XMLHTTP();
            http2.open("POST", "https://www.aprovafacil.com/cgi-bin/APFW/" + cdClienteCobreBem + "/CAP?" +
                "Transacao=" + No.ChildNodes.Item(3).InnerText, false, null, null);
            http2.send(null);


            XmlDocument xmlDoc2 = new XmlDocument();

            xmlDoc2.LoadXml(http2.responseText);


            XmlNode No2 = xmlDoc2.SelectSingleNode("ResultadoCAP");

            if (No2.ChildNodes.Item(0).InnerText.ToUpper().Contains("CONFIRMADO"))
            {
                return "CONFIRMADO;" +
                        No.ChildNodes.Item(1).InnerText + ";" +
                        No.ChildNodes.Item(3).InnerText + ";";
            }
            else
                return //"Erro;" + 
                    No2.ChildNodes.Item(0).InnerText;


        }
        catch (Exception ex)
        {
            return "Erro;" + //retorno +
                //System.Web.HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"] + 
                ex.Message;
        }
        
    }
    
}
