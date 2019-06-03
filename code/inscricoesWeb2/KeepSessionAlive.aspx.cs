using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using cllEventos;

using System.Data;
using System.Data.SqlClient;

public partial class KeepSessionAlive : System.Web.UI.Page
{
    Evento SessionEvento;
    SqlConnection SessionCnn;
    Participante SessionParticipante;

    Categoria SessionCategoria;

    Pedido SessionPedido;

    Inscricoes SessionInscricoes;

    Tese SessionTese;
    TeseParticipante SessionTeseParticipante;

    Atividade SessionAtividade;

    String SessionIdioma;
    String SessionCateg;

    protected string WindowStatusText = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        // Refresh this page 60 seconds before session timeout, effectively resetting the session timeout counter.
        MetaRefresh.Attributes["content"] = Convert.ToString((Session.Timeout * 60) - 60) + ";url=KeepSessionAlive.aspx?q=" + DateTime.Now.Ticks;

        WindowStatusText = "Last refresh " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();

        SessionCnn = (SqlConnection)Session["SessionCnn"];
        if (SessionCnn != null)
            Session["SessionCnn"] = SessionCnn;

        SessionEvento = (Evento)Session["SessionEvento"];
        if (SessionEvento != null)
            Session["SessionEvento"] = SessionEvento;

        SessionParticipante = (Participante)Session["SessionParticipante"];
        if (SessionParticipante != null)
            Session["SessionParticipante"] = SessionParticipante;

        SessionCategoria = (Categoria)Session["SessionCategoria"];
        if (SessionCategoria != null)
            Session["SessionCategoria"] = SessionCategoria;

        SessionIdioma = (String)Session["SessionIdioma"];
        if (SessionIdioma == null)
            SessionIdioma = "PTBR";
        Session["SessionIdioma"] = SessionIdioma;

        SessionCateg = (String)Session["SessionCateg"];
        if (SessionCateg != null)
            Session["SessionCateg"] = SessionCateg;
        
        SessionPedido = (Pedido)Session["SessionPedido"];
        if (SessionPedido != null)
            Session["SessionPedido"] = SessionPedido;

        SessionInscricoes = (Inscricoes)Session["SessionInscricoes"];
        if (SessionInscricoes != null)
            Session["SessionInscricoes"] = SessionInscricoes;

        SessionTese = (Tese)Session["SessionTese"];
        if (SessionTese != null)
            Session["SessionTese"] = SessionTese;

        SessionTeseParticipante = (TeseParticipante)Session["SessionTeseParticipante"];
        if (SessionTeseParticipante != null)
            Session["SessionTeseParticipante"] = SessionTeseParticipante;

        SessionAtividade = (Atividade)Session["SessionAtividade"];
        if (SessionAtividade != null)
            Session["SessionAtividade"] = SessionAtividade;

    }
}