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

public partial class frmMeetingPerfil : System.Web.UI.Page
{
    Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

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

            lblIdentificador.Text = SessionParticipante.CdParticipante.Trim();
            lblNoParticipante.Text = SessionParticipante.NoParticipante.Trim();

            

            if (SessionIdioma == "PTBR")
                lblCategoria.Text = SessionParticipante.Categoria.NoCategoria.Trim();
            else if (SessionIdioma == "ENUS")
                lblCategoria.Text = SessionParticipante.Categoria.NoCategoriaIngles.Trim();
            else if (SessionIdioma == "ESP")
                lblCategoria.Text = SessionParticipante.Categoria.NoCategoriaEspanhol.Trim();
            else if (SessionIdioma == "FRA")
                lblCategoria.Text = SessionParticipante.Categoria.NoCategoriaFrances.Trim();

            
            if (!SessionParticipante.meetingParticipante.FlAceiteTermosMeeting)
                Server.Transfer("frmMeetingInformacoes.aspx", true);

            
            txtPerfilB2B.Text = (SessionParticipante.meetingParticipante == null ? "" : SessionParticipante.meetingParticipante.DsPerfilEmpresaHTML);
            lblEmpresa.Text = (SessionParticipante.meetingParticipante == null ? SessionParticipante.NoInstituicao : SessionParticipante.meetingParticipante.NoInstituicao);
            lblNoPais.Text = (SessionParticipante.meetingParticipante == null ? SessionParticipante.NoPais : SessionParticipante.meetingParticipante.NoPais);
            lblAreaAtuacao.Text = (SessionParticipante.meetingParticipante == null ? SessionParticipante.NoAreaAtuacao : SessionParticipante.meetingParticipante.NoAreaAtuacao);
            lblWebSite.Text = (SessionParticipante.meetingParticipante == null ? SessionParticipante.DsAuxiliar4 : SessionParticipante.meetingParticipante.DsWebsite);
            lblNoCargo.Text = (SessionParticipante.meetingParticipante == null ? SessionParticipante.NoCargo : SessionParticipante.meetingParticipante.NoCargo);

        }
        else
        {
            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            SessionIdioma = (String)Session["SessionIdioma"];

            if (SessionEvento == null)
                Server.Transfer("frmSessaoExpirada.aspx", true);

        }

        verificarIdioma(SessionIdioma);

    }

    protected void verificarIdioma(string prmIdioma)
    {
        if (prmIdioma == "PTBR")
        {
            SessionIdioma = "PTBR";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Perfil da Empresa";

            lblID.Text = "Nr Cadastro:";
            lblPart.Text = "Participante:";
            lblCateg.Text = "Categoria:";
            lblEmpr.Text = "Intituição/Empresa:";
            lbl_pais.Text = "País";
            lblCargo.Text = "Cargo";


            lblInstrucoes.Text = "Informe mais detalhes sobre o perfil de negócios de sua empresa:";
            lblInstrucoes0.Text = "(Limite de 500 caracteres): ";

            btnGravar.Text = "Gravar";

            
        }
        else if (prmIdioma == "ENUS")
        {
            SessionIdioma = "ENUS";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Company Profile";

            lblID.Text = "Registration no.";
            lblPart.Text = "Participant:";
            lblCateg.Text = "Category:";

            lblEmpr.Text = "Institution / Company:";
            lbl_pais.Text = "Country";
            lblSite.Text = "Website";
            lblCargo.Text = "Role";

            lblInstrucoes.Text = "Tell more details about your company's business profile:";
            lblInstrucoes0.Text = "(Limit de 500 characters): ";



            btnGravar.Text = "Save";
            btnNovo.Text = "New invitation";
            

        }
        else if (prmIdioma == "ESP")
        {
            SessionIdioma = "ESP";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Datos de la Empresa";

            lblID.Text = "Registro nr:";
            lblPart.Text = "Participante:";
            lblCateg.Text = "Categoría";

            lblInstrucoes.Text = "Introduzca el perfil de negocio de su empresa:";

            btnGravar.Text = "Cambiar contraseña";

            
        }
        else if (prmIdioma == "FRA")
        {
            SessionIdioma = "FRA";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Profil de la Société";

            lblPart.Text = "Participant:";
            lblCateg.Text = "Catégorie:";
            lblID.Text = "N° d'enregistrement:";

            lblInstrucoes.Text = "Entrez le profil d'entreprise de votre entreprise:";

            btnGravar.Text = "Confirmer";

            
        }
    }

    protected void btnGravar_Click(object sender, EventArgs e)
    {

        //MeetingMesaReuniaoCad oMeetingMesaReuniaoCad = new MeetingMesaReuniaoCad();
        //oMeetingMesaReuniaoCad.CriarMesas(SessionEvento.CdEvento, SessionCnn);
        //return;



        txtMsg.Visible = false;

        string tmp = Server.HtmlDecode(txtPerfilB2B.Text);
        tmp = Regex.Replace(tmp, "<.*?>", "");

        if (tmp.Length > 500)
        {
            txtMsg.Visible = true;
            if (SessionIdioma == "PTBR")
                txtMsg.Text = "Total de caracteres acima do permitido!";
            else
                txtMsg.Text = "Total characters above the permitted!";
            txtPerfilB2B.Focus();
            return;
        }

        MeetingParticipante tmpMeetingParticipante = new MeetingParticipante();

        if (SessionParticipante.meetingParticipante != null)
        {
            tmpMeetingParticipante = SessionParticipante.meetingParticipante;

            tmpMeetingParticipante.DsPerfilEmpresaHTML = txtPerfilB2B.Text;
            tmpMeetingParticipante.DsPerfilEmpresaTexto = tmp;

        }
        else
        {        
            tmpMeetingParticipante.CdEvento = SessionEvento.CdEvento;
            tmpMeetingParticipante.CdParticipante = SessionParticipante.CdParticipante;
            tmpMeetingParticipante.DsPerfilEmpresaHTML = txtPerfilB2B.Text;
            tmpMeetingParticipante.DsPerfilEmpresaTexto = tmp;
            
        }

        tmpMeetingParticipante.NoInstituicao = (tmpMeetingParticipante.NoInstituicao == "" ? SessionParticipante.NoInstituicao : tmpMeetingParticipante.NoInstituicao);
        tmpMeetingParticipante.NoPais = (tmpMeetingParticipante.NoPais == "" ? SessionParticipante.NoPais : tmpMeetingParticipante.NoPais);
        tmpMeetingParticipante.NoAreaAtuacao = (tmpMeetingParticipante.NoAreaAtuacao == "" ? SessionParticipante.NoAreaAtuacao : tmpMeetingParticipante.NoAreaAtuacao);
        tmpMeetingParticipante.DsWebsite = (tmpMeetingParticipante.DsWebsite == "" ? SessionParticipante.DsAuxiliar4 : tmpMeetingParticipante.DsWebsite);
        tmpMeetingParticipante.NoCargo = (tmpMeetingParticipante.NoCargo == "" ? SessionParticipante.NoCargo : tmpMeetingParticipante.NoCargo);

        MeetingParticipante tmpMeetingParticipante2 = oParticipanteCad.oMeetingParticipanteCad.Gravar(tmpMeetingParticipante, SessionCnn);
        if (tmpMeetingParticipante2 != null)
        {
            if (SessionIdioma == "PTBR")
                txtMsg.Text = "Operação realizada com sucesso";
            else
                txtMsg.Text = "Operation was successful";

            txtMsg.Visible = true;

            SessionParticipante = oParticipanteCad.Pesquisar(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);
            Session["SessionParticipante"] = SessionParticipante;

            btnNovo.Visible = true;

            //Response.Redirect("frmMeetingPerfil.aspx", false);
            //return;
        }
        else
        {
            txtMsg.Text = oParticipanteCad.oMeetingParticipanteCad.RcMsg;
            txtMsg.Visible = true;
        }
    }
}
