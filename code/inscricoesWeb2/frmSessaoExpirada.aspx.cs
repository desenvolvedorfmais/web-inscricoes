using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmSessaoExpirada : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {



        /***** Recuperar cookies *****/
        //System.Collections.Specialized.NameValueCollection UserInfoCookieCollection;

        //UserInfoCookieCollection = Request.Cookies["EventoInfo"].Values;
        //string codEvento = "";
        //try
        //{
        //    codEvento = Server.HtmlEncode(UserInfoCookieCollection["eventoCod"]);
        //}
        //catch (Exception)
        //{

        //}

        //string codLng = "";
        //try
        //{
        //    codLng = Server.HtmlEncode(UserInfoCookieCollection["lngCod"]);
        //}
        //catch (Exception)
        //{

        //}

        //string codCateg = "";
        //try
        //{
        //    codCateg = Server.HtmlEncode(UserInfoCookieCollection["categCod"]);
        //}
        //catch (Exception)
        //{

        //}

        //string codAtividade = "";
        //try
        //{
        //    codAtividade = Server.HtmlEncode(UserInfoCookieCollection["atividadeCod"]);
        //}
        //catch (Exception)
        //{

        //}

        //string tpSistema = "";
        //try
        //{
        //    tpSistema = Server.HtmlEncode(UserInfoCookieCollection["tpSistema"]);
        //}
        //catch (Exception)
        //{

        //}

        //string tpRotina = "";
        //try
        //{
        //    tpRotina = Server.HtmlEncode(UserInfoCookieCollection["tpRotina"]);
        //}
        //catch (Exception)
        //{

        //}

        string codEvento = "";
        string codLng = "";
        string codCateg = "";
        string codAtividade = "";
        string tpSistema = "";
        string tpRotina = "";
        if (Request.Cookies["EventoInfo"] != null)
        {
            codEvento = Server.HtmlEncode(Request.Cookies["EventoInfo"]["eventoCod"]);
            codLng = Server.HtmlEncode(Request.Cookies["EventoInfo"]["lngCod"]);
            codCateg = Server.HtmlEncode(Request.Cookies["EventoInfo"]["categCod"]);
            codAtividade = Server.HtmlEncode(Request.Cookies["EventoInfo"]["atividadeCod"]);
            tpSistema = Server.HtmlEncode(Request.Cookies["EventoInfo"]["tpSistema"]);
            tpRotina = Server.HtmlEncode(Request.Cookies["EventoInfo"]["tpRotina"]);

            if (codEvento == "006602")
                Response.Redirect(
                    "http://eradoencontro.parcorretora.com.br/frmVerificaCPFCadastro.aspx?codEvento=MDA2NjAy&cdLng=PTBR&cat=&atv=&keyAut=&tpSist=NRM&tpRotina=");
            else
            {
                string tipoRotina = (String)Session["tpRotina"];

                if (!string.IsNullOrEmpty(tipoRotina))
                    tpRotina = tipoRotina;

                if ((tpRotina == null) || (tpRotina == ""))
                    Response.Redirect("http://fazendomais4.azurewebsites.net/inscr.aspx?codEvento=" +
                                      cllEventos.Crypto.EncryptStringAES(codEvento) + "&cdLng=" + codLng + "&cat=" +
                                      codCateg + "&atv=" + codAtividade + "&keyAut=" + "&tpSist=" + tpSistema);
                else
                    Response.Redirect("http://fazendomais4.azurewebsites.net/outrarotina.aspx?codEvento=" +
                                      cllEventos.Crypto.EncryptStringAES(codEvento) + "&cdLng=" + codLng + "&cat=" +
                                      codCateg + "&atv=" + codAtividade + "&keyAut=" + "&tpSist=" + tpSistema +
                                      "&tpRotina=" +
                                      tpRotina);
            }
        }
        else
        {
            Response.Redirect("http://fazendomais4.azurewebsites.net/inscr.aspx?codEvento=" +
                                      "&cdLng=" + "&cat=" + "&atv=" + "&keyAut=" + "&tpSist=" );
         //   Response.Redirect("http://eradoencontro.parcorretora.com.br/frmVerificaCPFCadastro.aspx?codEvento=MDA2NjAy&cdLng=PTBR&cat=&atv=&keyAut=&tpSist=NRM&tpRotina=");
        }





        //if (codEvento == "000540")
        //    Response.Redirect("http://congressobrasileirofenaess.org.br/2015/");
        //else
        ////if (codEvento != "001305")
        //    //Response.Redirect("http://www.fazendomais.com/inscricoes/inscr.aspx?codEvento=" + cllEventos.Crypto.EncryptStringAES(codEvento) + "&cdLng=" + codLng + "&cat=" + codCateg + "&atv=" + codAtividade + "&keyAut=" + "&tpSist=" + tpSistema + "");
        //    Response.Redirect("http://fazendomais4.azurewebsites.net/inscr.aspx?codEvento=" + cllEventos.Crypto.EncryptStringAES(codEvento) + "&cdLng=" + codLng + "&cat=" + codCateg + "&atv=" + codAtividade + "&keyAut=" + "&tpSist=" + tpSistema + "");
        ////else
        ////{            
        ////    Response.Redirect("index.aspx?codEvento=" + cllEventos.Crypto.EncryptStringAES(codEvento) + "&cdLng=" + codLng + "&cat=" + codCateg + "&atv=" + codAtividade + "&keyAut=" + "&tpSist=" + tpSistema + "");
        ////}






    }
}