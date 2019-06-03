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

public partial class MasterPageboleto : System.Web.UI.MasterPage
{
    Evento SessionEvento;
    SqlConnection SessionCnn;
    Participante SessionParticipante;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SessionCnn = (SqlConnection)Session["SessionCnn"];
            Session["SessionCnn"] = SessionCnn;

            SessionEvento = (Evento)Session["SessionEvento"];
            Session["SessionEvento"] = SessionEvento;

            if (SessionParticipante == null)
                SessionParticipante = (Participante)Session["SessionParticipante"];
            else
                Session["SessionParticipante"] = SessionParticipante;


            string caminhoImagem = "img/bg_topo.jpg";
            if (SessionEvento != null)
                caminhoImagem = "https://inscricoesweb.fazendomais.com/imagensgeral/topo_boleto_" + SessionEvento.CdEvento + ".jpg";
                //caminhoImagem = "img/topo_boleto_" + SessionEvento.CdEvento + ".jpg";
            

            //img_TopoBoleto.ImageUrl = caminhoImagem;
        }
        else
        {
            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            SessionParticipante = (Participante)Session["SessionParticipante"];

           // string caminhoImagem = "img/bg_topo.jpg";
          //  if (SessionEvento != null)
          //      caminhoImagem = "img/top_idp.jpg";
           // Response.Write("<style  \"text/css\"> " +
           //"#topo{ " +
           //     "height:130px; " +
           //     //"padding:10px; " +
           //     "position:relative; " +
           //     "padding-bottom: 10px; " +
           //     "background:url(" + caminhoImagem + ") no-repeat center;" +
           // "} </style>");	
        }
    }
}
