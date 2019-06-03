using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using cllEventos;

using System.Data.SqlClient;

using System.IO;

using AjaxControlToolkit;

public partial class MPExpoepi : System.Web.UI.MasterPage
{
    Evento SessionEvento;
    SqlConnection SessionCnn;
    Participante SessionParticipante;

    String SessionIdioma;
    String SessionCateg;

    protected void Page_Load(object sender, EventArgs e)
    {

        string caminhoImagem = "img/bg_topo.jpg";

        if (!Page.IsPostBack)
        {


            if (SessionCnn == null)
                SessionCnn = (SqlConnection)Session["SessionCnn"];
            else
                Session["SessionCnn"] = SessionCnn;

            if (SessionEvento == null)
                SessionEvento = (Evento)Session["SessionEvento"];
            else
                Session["SessionEvento"] = SessionEvento;

            if (SessionParticipante == null)
                SessionParticipante = (Participante)Session["SessionParticipante"];
            else
                Session["SessionParticipante"] = SessionParticipante;

            SessionIdioma = (String)Session["SessionIdioma"];
            if (SessionIdioma == null)
                SessionIdioma = "PTBR";
            Session["SessionIdioma"] = SessionIdioma;

            SessionCateg = (String)Session["SessionCateg"];
            if (SessionCateg == null)
                SessionCateg = "";
            Session["SessionCateg"] = SessionCateg;

            
        }
        else
        {
            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            SessionParticipante = (Participante)Session["SessionParticipante"];


            SessionIdioma = (String)Session["SessionIdioma"];

            SessionCateg = (String)Session["SessionCateg"];
        }

        img_Topo.ImageUrl = caminhoImagem;

        if (SessionEvento != null)
        {
            caminhoImagem = "https://inscricoesweb.fazendomais.com/imagensgeral/top_" + SessionEvento.CdEvento + ".jpg";

            img_Topo.ImageUrl = caminhoImagem;

            
            //if (SessionEvento.DsCSSEstiloWeb.Trim() != "")
            //{

            //    //Atribuindo o head (cabeçalho da nossa página)
            //    HtmlHead head = this.Page.Header;

            //    head.Controls.Remove(head.FindControl("Estilo"));
            //    //head.Controls.RemoveAt(5);

            //    //Instanciando um HTMLLink com o nome de link
            //    HtmlLink link = new HtmlLink();

            //    //Adicionando os atributos
            //    link.Attributes.Add("Id", SessionEvento.DsCSSEstiloWeb);
            //    link.Attributes.Add("type", "text/css");
            //    link.Attributes.Add("rel", "stylesheet");
            //    link.Attributes.Add("href", "css/" + SessionEvento.DsCSSEstiloWeb + ".css");

            //    //Adicionando o link ao head (cabeçalho)
            //    head.Controls.Add(link);



            //}
            

            

        }
        

    }

    
}
