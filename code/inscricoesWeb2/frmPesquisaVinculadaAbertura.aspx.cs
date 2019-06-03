using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

using KrksaComponentes;
using KrksaComponentes.cllEventosClasses;
using cllEventos;
using CLLFuncoes;

using System.Data.SqlClient;

using AjaxControlToolkit;

using MSXML2;

using System.Xml;


public partial class frmPesquisaVinculadaAbertura : System.Web.UI.Page
{
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    Participante SessionParticipante;

    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    SqlConnection SessionCnn;

    String SessionTipoSistema;

    protected void Page_Load(object sender, EventArgs e)
    {
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

            SessionParticipante = new Participante();
            Session["SessionParticipante"] = SessionParticipante;

            //SessionIdioma = (String)Session["SessionIdioma"];
            //if (SessionIdioma == null)
            //    SessionIdioma = "PTBR";
            //Session["SessionIdioma"] = SessionIdioma;


            
            SessionTipoSistema = (String)Session["SessionTipoSistema"];
            if (SessionTipoSistema == null)
                SessionTipoSistema = "NRM";
            
            Session["SessionTipoSistema"] = SessionTipoSistema;

            if (SessionEvento == null)
            {
                //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                //                "003",
                //                oEventoCad.RcMsg), true);

                Server.Transfer("frmSessaoExpirada.aspx", true);
            }


            lblTitulo.Text = SessionEvento.NoEvento;

            lblInstrucoesRapidas.Text = SessionEvento.DsInformacoesPreviasWeb;

            if (SessionEvento.CdEvento == "005502")
            {
                lblChamada3.Visible = true;
                lblChamada3.ForeColor = System.Drawing.Color.Blue;
                lblChamada3.Text = "Ao final da pesquisa de avalização você poderá baixar seu certificado.<br/>Caso já tenha realizado a pesquisa, informe seu CPF para baixar seu certificado.";
            }

            if (SessionEvento.CdEvento == "006501")
            {
                lblChamada2.Text = "Sua opinião sobre o evento é muito importante para o aprimoramento das atividades da ANABB. Por isso, queremos registrá-la.";
                lblChamada1.Visible = false;
            }

            TXTDsCPF.Focus();
        }
        else
        {
            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            SessionTipoSistema = (String)Session["SessionTipoSistema"];

            SessionParticipante = (Participante)Session["SessionParticipante"];
        }

        if (SessionEvento == null)
            Server.Transfer("frmSessaoExpirada.aspx", true);



        ToolkitScriptManager1.RegisterPostBackControl(btnContinuarPesquisaVinculada);
        ToolkitScriptManager1.RegisterPostBackControl(btnLimparPesquisaVinculada);
    }
    protected void btnContinuar_Click(object sender, EventArgs e)
    {

        if (SessionEvento == null)
            Server.Transfer("frmSessaoExpirada.aspx", true);


        ParticipanteCad oParticipanteCad = new ParticipanteCad();
        

        SessionParticipante = oParticipanteCad.Pesquisar(SessionEvento.CdEvento, "nuCPFCNPJ", TXTDsCPF.Text.Replace(".", "").Replace("-", "").Trim(), SessionCnn);

        if (SessionParticipante != null)
        {

            if (!SessionParticipante.FlAtivo)
            {
                //if (SessionIdioma == "PTBR")
                    txtMsg.Text = "Cadastro inativo! Favor entrar em contato com a organização do evento.";
                //else if (SessionIdioma == "ENUS")
                //    txtMsg.Text = "Register idle! Please contact the event organizers.";
                //else if (SessionIdioma == "ESP")
                //    txtMsg.Text = "Registrarse inactivada! Póngase en contacto con los organizadores del evento.";
                //else if (SessionIdioma == "FRA")
                //    txtMsg.Text = "Inscription inactivé! S'il vous plaît contacter les organisateurs de l'événement.";

                return;
            }
                        

            if ((SessionParticipante == null) ||
                 (SessionParticipante.CdCredencial.Trim() == "0"))
            {
                txtMsg.Text = "CREDENCIAL não Emitida!";
                TXTDsCPF.Focus();
                return;
            }

            if (!SessionParticipante.Categoria.FlQuestionario)
            {
                txtMsg.Text = "Categoria não possui direito de participar de Pesquisas de Opinião";
                TXTDsCPF.Focus();
                return;
            }

            PesquisasDeOpiniao oPesquisasDeOpiniao = new PesquisasDeOpiniao();

            DataTable dtPesquisas = oPesquisasDeOpiniao.ListarPesquisasDoParticipante(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionParticipante.DsIdioma, SessionCnn);

            if ((dtPesquisas == null) || (dtPesquisas.Rows.Count <= 0))
            {
                if (SessionEvento.CdEvento != "005502")
                {
                    txtMsg.Text = "Não há pesquisa para o Participante nesse momento!\nAgradecemos a participação.";
                }
                else
                {
                    if (!SessionParticipante.Categoria.FlCertificado)
                    {//não possui direito à certificado
                        txtMsg.Text = "Categoria não possui direito certificação";
                        return;
                    }

                    Server.Transfer(string.Format("frmCertificado.aspx?p={0}",
                                       cllEventos.Crypto.EncryptStringAES(SessionParticipante.CdParticipante)), true);
                }
                TXTDsCPF.Focus();
                return;
            }

            Session["SessionParticipante"] = SessionParticipante;

            Response.Write("<script>window.open('frmPesquisaVinculada.aspx','_self');</script>");

        }
        else
        {
            //if (SessionIdioma == "PTBR")
                txtMsg.Text = "Participante não cadastrado";
            //else if (SessionIdioma == "ENUS")
            //    txtMsg.Text = "Participant is not registered or invalid password";
            //else if (SessionIdioma == "ESP")
            //    txtMsg.Text = "Participante no está registrado o la contraseña no válida";
            //else if (SessionIdioma == "FRA")
            //    txtMsg.Text = "Participant n'est pas inscrit ou mot de passe invalide";

                TXTDsCPF.Focus();
        }


        
    }
    protected void btnLimpar_Click(object sender, EventArgs e)
    {
        txtMsg.Text = "";
        TXTDsCPF.Text = "";
        TXTDsCPF.Focus();
    }
}