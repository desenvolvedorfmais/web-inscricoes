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

public partial class frmLembrarSenha : System.Web.UI.Page
{
    Participante SessionParticipante;
    //ParticipanteCad oParticipanteCad = new ParticipanteCad();
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

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

            SessionIdioma = (String)Session["SessionIdioma"];
            if (SessionIdioma == null)
                SessionIdioma = "PTBR";
            Session["SessionIdioma"] = SessionIdioma;

            //verificarIdioma(SessionIdioma);

            if (SessionParticipante == null)
                SessionParticipante = (Participante)Session["SessionParticipante"];
            else
                Session["SessionParticipante"] = SessionParticipante;


            if (SessionEvento == null)
                SessionEvento = (Evento)Session["SessionEvento"];
            else
                Session["SessionEvento"] = SessionEvento;

            if (SessionEvento == null)
                Server.Transfer("frmSessaoExpirada.aspx", true);

            TXTDsCPF.Focus();
            if ((SessionEvento.DsCampoAutenticacao.ToUpper().Replace("-", "") == "EMAIL") || (SessionIdioma != "PTBR"))
            {
                lblEmail.Visible = txtEmail_lembrar_senha.Visible = revTxtEmail.Visible = rfvTxtEmail.Visible = true;
                txtEmail_lembrar_senha.Focus();
                LBLCONTA.Visible = TXTDsCPF.Visible = RFVCPF.Visible = false;
            }

            if ((SessionEvento.DtFechamentoInscrWeb == null) ||
                (SessionEvento.DtFechamentoInscrWeb < Geral.datahoraServidor(SessionCnn)))//DateTime.Parse(DateTime.Today.ToString("dd/MM/yyyy"))))
            {               

                //if (!SessionEvento.FlLiberarCertificacaoWeb)
                //{
                    btnQueroCadastrar.Text = "Voltar";
                    btnQueroCadastrar.Visible = true;
                //}
            }
            else
                btnQueroCadastrar.Visible = true;

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

        ToolkitScriptManager1.RegisterPostBackControl(btnQueroCadastrar);
        ToolkitScriptManager1.RegisterPostBackControl(btnGerarNovaSenha);
    }

    protected void verificarIdioma(string prmIdioma)
    {
        //DropDownList txtidioma = (DropDownList)Page.Master.FindControl("txtIdioma");
       // if (txtidioma != null)
        //{
        if (prmIdioma == "PTBR")//(txtidioma.SelectedIndex == 0)
            {
                SessionIdioma = "PTBR";
                Session["SessionIdioma"] = SessionIdioma;

                lblTituloGerarSenha.Text = "Gerar nova senha";
                lblDesctela.Text = "Aten��o!<br/><br/>Por motivo de seguran�a todas " +
                                   "as senhas s�o codificadas por rotinas que n�o permitem a decodifica��o. " +
                                   "Tendo isto em vista esta rotina ir� gerar uma nova senha e enviar� para o e-mail " +
                                   "do participante j� cadastrado.";
                RFVCPF.Text = "Campo requerido";
                revTxtEmail.Text = "E-mail inv�lido";
                rfvTxtEmail.Text = "Campo requerido";
                
                btnGerarNovaSenha.Text = "Gerar nova senha";
                if ((SessionEvento.DtFechamentoInscrWeb == null) ||
                    (SessionEvento.DtFechamentoInscrWeb < Geral.datahoraServidor(SessionCnn)))//DateTime.Parse(DateTime.Today.ToString("dd/MM/yyyy"))))
                    btnQueroCadastrar.Text = "Voltar";
                else
                    btnQueroCadastrar.Text = "Quero me Inscrever";



            TXTDsCPF.Attributes.Add("placeholder", "Informe o CPF cadastrado");
            txtEmail_lembrar_senha.Attributes.Add("placeholder", "Informe o e-mail cadastrado");
        }
        else if (prmIdioma == "ENUS")//(txtidioma.SelectedIndex == 1)
            {
                SessionIdioma = "ENUS";
                Session["SessionIdioma"] = SessionIdioma;

                lblTituloGerarSenha.Text = "Generate new password";
                lblDesctela.Text = "Attention! <br/><br/>For security reasons all " +
����������������                           "passwords are coded routines that do not allow the decoding. " +
                                           "With this in mind this routine will generate a new password and send to the e-mail " +
����������������                           "the participant already registered.";
                RFVCPF.Text = "Required field";
                revTxtEmail.Text = "E-mail is invalid";
                rfvTxtEmail.Text = "Required field";                
                
                btnGerarNovaSenha.Text = "Generate new password";
                if ((SessionEvento.DtFechamentoInscrWeb == null) ||
                    (SessionEvento.DtFechamentoInscrWeb < Geral.datahoraServidor(SessionCnn)))//DateTime.Parse(DateTime.Today.ToString("dd/MM/yyyy"))))
                    btnQueroCadastrar.Text = "Return";
                else
                    btnQueroCadastrar.Text = "Sign up";

            txtEmail_lembrar_senha.Attributes.Add("placeholder", "Enter the registered e-mail");
        }
        else if (prmIdioma == "ESP")//(txtidioma.SelectedIndex == 2)
            {
                SessionIdioma = "ESP";
                Session["SessionIdioma"] = SessionIdioma;

                lblTituloGerarSenha.Text = "Generar nueva contrase�a";
                lblDesctela.Text = "�Atenci�n! <br/><br/>Por razones de seguridad todos los " +
����������������                           "contrase�as se codifican las rutinas que no permiten la decodificaci�n. " +
����������������                           "Con esto en mente esta rutina va a generar una nueva contrase�a y enviarla al e-mail " +
����������������                           "el participante ya est� registrado.";
                RFVCPF.Text = "Campo obligatorio";
                revTxtEmail.Text = "E-mail no es v�lido";
                rfvTxtEmail.Text = "Campo obligatorio";
                
                btnGerarNovaSenha.Text = "Generar nueva contrase�a";
                if ((SessionEvento.DtFechamentoInscrWeb == null) ||
                    (SessionEvento.DtFechamentoInscrWeb < Geral.datahoraServidor(SessionCnn)))//DateTime.Parse(DateTime.Today.ToString("dd/MM/yyyy"))))
                    btnQueroCadastrar.Text = "Volver";
                else
                    btnQueroCadastrar.Text = "Quiero registrarme";

            txtEmail_lembrar_senha.Attributes.Add("placeholder", "Ingrese el email registrado");
        }
        else if (prmIdioma == "FRA")//(txtidioma.SelectedIndex == 3)
            {
                SessionIdioma = "FRA";
                Session["SessionIdioma"] = SessionIdioma;

                lblTituloGerarSenha.Text = "G�n�rer nouveau mot de passe";
                lblDesctela.Text = "Attention! <br/><br/>Pour des raisons de s�curit� tous les " +
����������������                           "mots de passe sont cod�s routines qui ne permettent pas le d�codage. " +
                                           "Avec cela � l'esprit cette routine va g�n�rer un nouveau mot de passe et envoyer � l'adresse e-mail " +
����������������                           "le participant d�j� inscrit.";
                RFVCPF.Text = "Champs obligatoires";
                revTxtEmail.Text = "E-mail n'est pas valide";
                rfvTxtEmail.Text = "Champs obligatoires";
                
                btnGerarNovaSenha.Text = "G�n�rer nouveau mot de passe";
                if ((SessionEvento.DtFechamentoInscrWeb == null) ||
                    (SessionEvento.DtFechamentoInscrWeb < Geral.datahoraServidor(SessionCnn)))//DateTime.Parse(DateTime.Today.ToString("dd/MM/yyyy"))))
                    btnQueroCadastrar.Text = "Retour";
                else
                    btnQueroCadastrar.Text = "Je veux m'inscrire";

            txtEmail_lembrar_senha.Attributes.Add("placeholder", "Entrez votre adresse email");
        }
       // }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        ParticipanteCad oParticipanteCad = new ParticipanteCad();
        //SessionParticipante = oParticipanteCad.ValidarContaSenha(SessionEvento.CdEvento, TXTDsCPF.Text.Replace(".", "").Replace("-", "").Trim(), txtSenha.Text.Trim(), _cnn);
        if ((SessionEvento.DsCampoAutenticacao.ToUpper().Replace("-", "") == "CPF") && (SessionIdioma == "PTBR"))
        {
            SessionParticipante = oParticipanteCad.Pesquisar(SessionEvento.CdEvento, "nuCPFCNPJ", TXTDsCPF.Text.Replace(".", "").Replace("-", "").Trim(), SessionCnn);
        }
        else
        {
            SessionParticipante = oParticipanteCad.Pesquisar(SessionEvento.CdEvento, "dsEmail", txtEmail_lembrar_senha.Text.ToLower().Trim(), SessionCnn);
        }



        if (SessionParticipante != null)
        {
            oParticipanteCad.RedefinirSenha(SessionEvento, SessionParticipante, SessionIdioma, SessionCnn);

            Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                "001",
                                ""), true);
        }
        else
        {
            if (SessionIdioma == "PTBR")
                txtMsg.Text = "Nenhum registro localizado com os dados informados!";
            else if (SessionIdioma == "ENUS")
                txtMsg.Text = "No records found with the data reported!";
            else if (SessionIdioma == "ESP")
                txtMsg.Text = "No se encontraron registros con los datos reportados";
            else if (SessionIdioma == "FRA")
                txtMsg.Text = "Aucun r�sultat trouv� avec les donn�es d�clar�es";
            btnQueroCadastrar.Visible = true;
        }
    }
    protected void btnCadastrar_Click(object sender, EventArgs e)
    {

        string SessionCateg = "";
        if (SessionEvento.CdCliente == "0013")
            SessionCateg = "00130301";

        Session["SessionCateg"] = SessionCateg;

        Session["SessionChaveLibercao"] = "";
        
        //if  (SessionEvento.FlLiberarCertificacaoWeb)
        if ((SessionEvento.DtFechamentoInscrWeb == null) ||
                (SessionEvento.DtFechamentoInscrWeb < Geral.datahoraServidor(SessionCnn)))//DateTime.Parse(DateTime.Today.ToString("dd/MM/yyyy"))))
        {
            Response.Write("<script>window.open('index.aspx?codEvento=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + "','_self');</script>");
        }
        else
            if (SessionEvento.DsInformacoesCompletasWeb.Trim() != "")
                Response.Write("<script>window.open('frmInformacoes.aspx','_self');</script>");
            else if (SessionEvento.FlPesquisaCPFReceita)
                Response.Write("<script>window.open('frmBuscaCPFReceita.aspx','_self');</script>");
            else
                Response.Write("<script>window.open('frmCadastroAuto.aspx','_self');</script>");
    }
}
