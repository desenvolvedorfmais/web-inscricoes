using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using cllEventos;

public partial class frmRedirecionaNovo : System.Web.UI.Page
{
    string SessionOperacao;
    Participante SessionParticipante;
    protected void Page_Load(object sender, EventArgs e)
    {
        SessionOperacao = (String)Session["SessionOperacao"];
        if ((SessionOperacao == "INCLUIR"))
        {
            SessionParticipante = new Participante();
            Session["SessionParticipante"] = SessionParticipante;
        }
        else if ((SessionOperacao == "ALTERAR") || (SessionOperacao == "INCLUIR") || (SessionOperacao == "INICIO") || (SessionOperacao == ""))
        {
            SessionParticipante = (Participante)Session["SessionParticipante"];
            Session["SessionParticipante"] = SessionParticipante;
        }

        Session["SessionOperacao"] = SessionOperacao;
        Server.Transfer("frmCadastroAuto.aspx", true);
    }
}