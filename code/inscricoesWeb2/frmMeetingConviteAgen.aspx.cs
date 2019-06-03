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
using CLLFuncoes;

using System.Data.SqlClient;

using AjaxControlToolkit;

using System.Text.RegularExpressions;

public partial class frmMeetingConviteAgen : System.Web.UI.Page
{
    Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    MeetingConvite SessionMeetingConviteAgen;
    MeetingConviteCad oMeetingConviteCad = new MeetingConviteCad();

    MeetingMesaReuniaoCad oMeetingMesaReuniaoCad = new MeetingMesaReuniaoCad();

    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    SqlConnection SessionCnn;
    
    String SessionIdioma;

   
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            if (SessionCnn == null)
                SessionCnn = (SqlConnection)Session["SessionCnn"];
            else
                Session["SessionCnn"] = SessionCnn;


            if (SessionParticipante == null)
                SessionParticipante = (Participante)Session["SessionParticipante"];
            else
                Session["SessionParticipante"] = SessionParticipante;
                       

            SessionIdioma = (String)Session["SessionIdioma"];
            if (SessionIdioma == null)
                SessionIdioma = "PTBR";
            Session["SessionIdioma"] = SessionIdioma;


            if (SessionEvento == null)
                SessionEvento = (Evento)Session["SessionEvento"];
            else
                Session["SessionEvento"] = SessionEvento;

            if (SessionEvento == null)
                Server.Transfer("frmSessaoExpirada.aspx", true);

            if ((Request.QueryString["cdConviteAgen"] == null) &&
                (Request.QueryString["cdConviteAgen"].ToString().Trim().ToUpper() == ""))
            {
                pnlDadosConvite.Visible = false;
                txtMsg.Text = "Convite não encontrado";
            }
            else
            {
                PesquisarConvite(Request.QueryString["cdConviteAgen"]);
            }
            Session["SessionMeetingConviteRec"] = SessionMeetingConviteAgen;

            


            
        }
        else
        {

            SessionMeetingConviteAgen = (MeetingConvite)Session["SessionMeetingConviteRec"];

            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            SessionIdioma = (String)Session["SessionIdioma"];

            if (SessionEvento == null)
                Server.Transfer("frmSessaoExpirada.aspx", true);

        }

        verificarIdioma(SessionIdioma);

        //ToolkitScriptManager1.RegisterPostBackControl(grdMesas);
    }

    private void PesquisarConvite(string prmCdConvite)
    {
        SessionMeetingConviteAgen = oMeetingConviteCad.Pesquisar(SessionEvento.CdEvento, prmCdConvite, SessionCnn);
        Session["SessionMeetingConviteRec"] = SessionMeetingConviteAgen;

        lblIdentificador.Text = SessionMeetingConviteAgen.CdConvite.Trim();
        lblNoParticipante.Text = SessionMeetingConviteAgen.ParticipanteConvite.NoParticipante.Trim();



        //if (SessionIdioma == "PTBR")
        //    lblCategoria.Text = SessionParticipante.Categoria.NoCategoria.Trim();
        //else if (SessionIdioma == "ENUS")
        //    lblCategoria.Text = SessionParticipante.Categoria.NoCategoriaIngles.Trim();
        //else if (SessionIdioma == "ESP")
        //    lblCategoria.Text = SessionParticipante.Categoria.NoCategoriaEspanhol.Trim();
        //else if (SessionIdioma == "FRA")
        //    lblCategoria.Text = SessionParticipante.Categoria.NoCategoriaFrances.Trim();

        if (SessionParticipante.CdParticipante == SessionMeetingConviteAgen.ParticipanteConvidado.CdParticipante)
        {
            lblPerfilB2B.Text = (SessionMeetingConviteAgen.ParticipanteConvite.meetingParticipante == null ? "" : SessionMeetingConviteAgen.ParticipanteConvite.meetingParticipante.DsPerfilEmpresaHTML);
            lblEmpresa.Text = (SessionMeetingConviteAgen.ParticipanteConvite.meetingParticipante == null ? "" : SessionMeetingConviteAgen.ParticipanteConvite.meetingParticipante.NoInstituicao);
            lblNoPais.Text = (SessionMeetingConviteAgen.ParticipanteConvite.meetingParticipante == null ? "" : SessionMeetingConviteAgen.ParticipanteConvite.meetingParticipante.NoPais);
            lblAreaAtuacao.Text = (SessionMeetingConviteAgen.ParticipanteConvite.meetingParticipante == null ? "" : SessionMeetingConviteAgen.ParticipanteConvite.meetingParticipante.NoAreaAtuacao);
            lblWebSite.Text = (SessionMeetingConviteAgen.ParticipanteConvite.meetingParticipante == null ? "" : SessionMeetingConviteAgen.ParticipanteConvite.meetingParticipante.DsWebsite);

            //lblNoCargo.Text = (SessionMeetingConviteEnv.ParticipanteConvite.meetingParticipante == null ? "" : SessionMeetingConviteEnv.ParticipanteConvite.meetingParticipante.NoCargo);

            lblStatusConvite.Text = "FUI CONVIDADO";
        }
        else
        {
            lblPerfilB2B.Text = (SessionMeetingConviteAgen.ParticipanteConvidado.meetingParticipante == null ? "" : SessionMeetingConviteAgen.ParticipanteConvidado.meetingParticipante.DsPerfilEmpresaHTML);
            lblEmpresa.Text = (SessionMeetingConviteAgen.ParticipanteConvidado.meetingParticipante == null ? "" : SessionMeetingConviteAgen.ParticipanteConvidado.meetingParticipante.NoInstituicao);
            lblNoPais.Text = (SessionMeetingConviteAgen.ParticipanteConvidado.meetingParticipante == null ? "" : SessionMeetingConviteAgen.ParticipanteConvidado.meetingParticipante.NoPais);
            lblAreaAtuacao.Text = (SessionMeetingConviteAgen.ParticipanteConvidado.meetingParticipante == null ? "" : SessionMeetingConviteAgen.ParticipanteConvidado.meetingParticipante.NoAreaAtuacao);
            lblWebSite.Text = (SessionMeetingConviteAgen.ParticipanteConvidado.meetingParticipante == null ? "" : SessionMeetingConviteAgen.ParticipanteConvidado.meetingParticipante.DsWebsite);

            //lblNoCargo.Text = (SessionMeetingConviteAgen.ParticipanteConvidado.meetingParticipante == null ? "" : SessionMeetingConviteAgen.ParticipanteConvidado.meetingParticipante.NoCargo);

            lblStatusConvite.Text = "EU CONVIDEI";
            
        }


        MeetingAgendaCad oMeetingAgendaCad = new MeetingAgendaCad();
        MeetingAgenda oMeetingAgenda = oMeetingAgendaCad.Pesquisar(SessionEvento.CdEvento, SessionMeetingConviteAgen.CdConvite, SessionCnn);

        lblDtConvite.Text = oMeetingAgenda.MeetingMesaReuniao.DtMesaReuniaoIni.Value.ToString("dd/MM/yyyy HH:mm") + " - " + oMeetingAgenda.MeetingMesaReuniao.DtMesaReuniaoFim.Value.ToString("HH:mm");
        if (SessionIdioma != "PTBR")
        {
            System.Globalization.CultureInfo enUS = new System.Globalization.CultureInfo("en-US");

            lblDtConvite.Text = oMeetingAgenda.MeetingMesaReuniao.DtMesaReuniaoIni.Value.ToString("MM/dd/yyyy HH:mm tt", enUS).ToLower() + " - " +
                                oMeetingAgenda.MeetingMesaReuniao.DtMesaReuniaoFim.Value.ToString("HH:mm tt", enUS).ToLower();           
           
        }

        lblMesa.Text = oMeetingAgenda.MeetingMesaReuniao.DsMesaReuniao;



        if (lblStatusConvite.Text.ToUpper() == "EU CONVIDEI")
        {
            if (SessionIdioma != "PTBR")
                lblStatusConvite.Text = "I INVITED";

            lblStatusConvite.BackColor = System.Drawing.Color.Yellow;
            lblStatusConvite.ForeColor = System.Drawing.Color.Black;
                                   

        }
        else if (lblStatusConvite.Text.ToUpper() == "FUI CONVIDADO")
        {
            if (SessionIdioma != "PTBR")
                lblStatusConvite.Text = "I WAS INVITED";

            lblStatusConvite.BackColor = System.Drawing.Color.Blue;
            lblStatusConvite.ForeColor = System.Drawing.Color.White;

            
        }
    }

    protected void verificarIdioma(string prmIdioma)
    {
        if (prmIdioma == "PTBR")
        {
            SessionIdioma = "PTBR";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Detalhe da Agenda";

            lblID.Text = "Nr Convite:";
            lblPart.Text = "Representante:";
            //lblCateg.Text = "Categoria:";

            lblInstrucoes.Text = "Detalhes sobre o perfil de negócios da empresa:";

            btnVoltar.Text = "Voltar";

            
        }
        else if (prmIdioma == "ENUS")
        {
            SessionIdioma = "ENUS";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Detail Agenda";

            lblID.Text = "No. invitation";
            lblPart.Text = "Representative:";
            //lblCateg.Text = "Category:";

            lblInstrucoes.Text = "Details about the company's business profile:";

            lblEmpr.Text = "Institution / Company:";
            lbl_pais.Text = "Country:";
            lblArea.Text = "Main Practice Areas:";

            Mesa.Text = "Spot";

            DtConvite.Text = "Dt Meeting:";
            StatusConvite.Text = "Status Invitation:";

            btnVoltar.Text = "Return";


            

        }
        else if (prmIdioma == "ESP")
        {
            SessionIdioma = "ESP";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Invitación Enviada";

            lblID.Text = "Invitación Nr:";
            lblPart.Text = "Representante:";
            //lblCateg.Text = "Categoría";

            lblInstrucoes.Text = "Los detalles sobre el perfil de negocio de la compañía:";

            btnVoltar.Text = "Volver";

            
        }
        else if (prmIdioma == "FRA")
        {
            SessionIdioma = "FRA";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Profil de la Société";

            lblPart.Text = "Participant:";
            //lblCateg.Text = "Catégorie:";
            lblID.Text = "N° d'enregistrement:";

            lblInstrucoes.Text = "Entrez le profil d'entreprise de votre entreprise:";

            btnVoltar.Text = "Confirmer";

            
        }
    }

    
   
    
}
