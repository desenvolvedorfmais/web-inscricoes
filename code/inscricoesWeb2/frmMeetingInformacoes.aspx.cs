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

using KrksaComponentes;
using KrksaComponentes.cllEventosClasses;
using cllEventos;
using CLLFuncoes;

using System.Data.SqlClient;

public partial class frmMeetingInformacoes : System.Web.UI.Page
{
    Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    SqlConnection SessionCnn;

    String SessionIdioma;

    //String SessionCateg;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {

                //if (SessionCnn == null)
                SessionCnn = (SqlConnection)Session["SessionCnn"];
                //else
                Session["SessionCnn"] = SessionCnn;

                //if (SessionParticipante == null)
                SessionParticipante = (Participante)Session["SessionParticipante"];
                //else
                Session["SessionParticipante"] = SessionParticipante;


                //if (SessionEvento == null)
                SessionEvento = (Evento)Session["SessionEvento"];

                //SessionCateg = (String)Session["SessionCateg"];
                //if (SessionCateg == null)
                //    SessionCateg = "";
                //Session["SessionCateg"] = SessionCateg;

                //else
                Session["SessionEvento"] = SessionEvento;

                if (SessionEvento == null)
                    Server.Transfer("frmSessaoExpirada.aspx", true);

                SessionIdioma = (String)Session["SessionIdioma"];
                if ((SessionIdioma == null) || (SessionIdioma == ""))
                    SessionIdioma = "PTBR";
                Session["SessionIdioma"] = SessionIdioma;

                MeetingConfigCad oMeetingConfigCad = new MeetingConfigCad();
                MeetingConfig oMeetingConfig = oMeetingConfigCad.Pesquisar(SessionEvento.CdEvento, SessionCnn);


                if (SessionIdioma == "PTBR")
                    lblInformacoesCompletas.Text = oMeetingConfig.DsRegrasPTBR;
                else if (SessionIdioma == "ENUS")
                    lblInformacoesCompletas.Text = oMeetingConfig.DsRegrasENUS;
                //else if (SessionIdioma == "ESP")
                //    lblInformacoesCompletas.Text = SessionEvento.DsInformacoesCompletasWeb_esp;
                //else if (SessionIdioma == "FRA")
                //    lblInformacoesCompletas.Text = SessionEvento.DsInformacoesCompletasWeb_fra;

                
            }
            catch
            {
                //txtMsg.Text = "erro";
            }
        }
        else
        {
            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            SessionIdioma = (String)Session["SessionIdioma"];

            //SessionCateg = (String)Session["SessionCateg"];

            if (SessionEvento == null)
                Server.Transfer("frmSessaoExpirada.aspx", true);
        }
        try
        {
            verificarIdioma(SessionIdioma);

            TSManager1.RegisterPostBackControl(btnCadastrar2);
            TSManager1.RegisterPostBackControl(btnCadastrar);
        }
        catch
        {
            //txtMsg.Text = "erro";
        }
    }
    protected void btnCadastrar_Click(object sender, EventArgs e)
    {
        MeetingParticipante tmpMeetingParticipante = new MeetingParticipante();

        if (SessionParticipante.meetingParticipante != null)
        {
            tmpMeetingParticipante = SessionParticipante.meetingParticipante;
        }


        tmpMeetingParticipante.FlAceiteTermosMeeting = true;
        MeetingParticipante tmpMeetingParticipante2 = oParticipanteCad.oMeetingParticipanteCad.Gravar(tmpMeetingParticipante, SessionCnn);


        SessionParticipante = oParticipanteCad.Pesquisar(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);
        Session["SessionParticipante"] = SessionParticipante;

        Response.Redirect("frmMeetingPerfil.aspx", false);
        return;

    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        btnCadastrar.Enabled = !btnCadastrar.Enabled;
        btnCadastrar2.Enabled = !btnCadastrar2.Enabled;
    }

    protected void verificarIdioma(string prmIdioma)
    {
        //DropDownList txtidioma = (DropDownList)Page.Master.FindControl("txtIdioma");
        // if (txtidioma != null)
        //{

        if ((prmIdioma == null) || (prmIdioma == ""))
        {
            prmIdioma = "PTBR";
            SessionIdioma = "PTBR";
        }
        Session["SessionIdioma"] = SessionIdioma;

        if (prmIdioma == "PTBR")//(txtidioma.SelectedIndex == 0)
        {
            SessionIdioma = "PTBR";
            Session["SessionIdioma"] = SessionIdioma;


            CheckBox1.Text = " Li e concordo com os termos acima.";

            btnCadastrar.Text = btnCadastrar2.Text = "Quero participar do meeting B2B";
        }
        else if (prmIdioma == "ENUS")//(txtidioma.SelectedIndex == 1)
        {
            SessionIdioma = "ENUS";
            Session["SessionIdioma"] = SessionIdioma;

            CheckBox1.Text = " I have read and agree to the terms above.";

            btnCadastrar.Text = btnCadastrar2.Text = "I want to participate in the meeting B2B";
        }
        else if (prmIdioma == "ESP")//(txtidioma.SelectedIndex == 2)
        {
            SessionIdioma = "ESP";
            Session["SessionIdioma"] = SessionIdioma;

            CheckBox1.Text = " He leido y estoy de acuerdo con los términos anteriores.";
            btnCadastrar.Text = btnCadastrar2.Text = "Quiero registrarme";
        }
        else if (prmIdioma == "FRA")//(txtidioma.SelectedIndex == 3)
        {
            SessionIdioma = "FRA";
            Session["SessionIdioma"] = SessionIdioma;

            CheckBox1.Text = " J'ai lu et j'accepte les conditions ci-dessus.";
            btnCadastrar.Text = btnCadastrar2.Text = "Je veux m'inscrire";
        }
        // }
    }
}
