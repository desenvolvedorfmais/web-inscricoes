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

using System.Security.Cryptography;
using System.Text;

using System.Data.SqlClient;

using MSXML2;

using System.Xml;

public partial class frmDadosExpoepi : System.Web.UI.Page
{
    Participante SessionParticipante;
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    SqlConnection SessionCnn;
    SqlConnection SessionCnn2;

    String SessionIdioma;

    protected void Page_Load(object sender, EventArgs e)
    {


        if (!Page.IsPostBack)
        {
            SessionParticipante = new Participante();
            Session["SessionParticipante"] = SessionParticipante;

            //local 1
            //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOWEzSnJjMkV0Y0dNN1NXNXBkR2xoYkNCRFlYUmhiRzluUFdSaVJYWmxiblJ2YzE5R1RUdFFaWEp6YVhOMElGTmxZM1Z5YVhSNUlFbHVabTg5VkhKMVpUdFZjMlZ5SUVsRVBYTmhPMUJoYzNOM2IzSmtQV3R5YTNOaE1UY3g=")));

            //local 2 note novo
            //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOUxseHJjbXR6WVhCak8wbHVhWFJwWVd3Z1EyRjBZV3h2Wnoxa1lrVjJaVzUwYjNOZlJrMDdVR1Z5YzJsemRDQlRaV04xY21sMGVTQkpibVp2UFZSeWRXVTdWWE5sY2lCSlJEMXpZVHRRWVhOemQyOXlaRDFyY210ellURTNNUT09")));

            //servidor
            //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOVptRjZaVzVrYjIxaGFYTTdTVzVwZEdsaGJDQkRZWFJoYkc5blBXUmlSWFpsYm5SdmMxOUdUVHRRWlhKemFYTjBJRk5sWTNWeWFYUjVJRWx1Wm04OVZISjFaVHRWYzJWeUlFbEVQWE5oTzFCaGMzTjNiM0prUFV0eWEzTmhNVGN4")));

            //MinSaude
            //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOUxseHpjV3hsZUhCeVpYTnpPMGx1YVhScFlXd2dRMkYwWVd4dlp6MWtZa1YyWlc1MGIzTmZSazA3VUdWeWMybHpkQ0JUWldOMWNtbDBlU0JKYm1adlBWUnlkV1U3VlhObGNpQkpSRDF6WVR0UVlYTnpkMjl5WkQxTGNtdHpZVEUzTVE9PQ==")));

            //Site
            //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOU1UZzVMak00TGprMUxqY3lPMGx1YVhScFlXd2dRMkYwWVd4dlp6MW1ZWHBsYm1SdmJXRnBjenRRWlhKemFYTjBJRk5sWTNWeWFYUjVJRWx1Wm04OVZISjFaVHRWYzJWeUlFbEVQV1poZW1WdVpHOXRZV2x6TzFCaGMzTjNiM0prUFZGMU5EVTBja1p0")));

            //Site2-historico
            //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOWJYTnpjV3d1Wm1GNlpXNWtiMjFoYVhNdVkyOXRPMGx1YVhScFlXd2dRMkYwWVd4dlp6MW1ZWHBsYm1SdmJXRnBjekU3VUdWeWMybHpkQ0JUWldOMWNtbDBlU0JKYm1adlBWUnlkV1U3VlhObGNpQkpSRDFtWVhwbGJtUnZiV0ZwY3pFN1VHRnpjM2R2Y21ROVVYVTBOVFJ5Um0wPQ==")));

            //Site-producao
            //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOWJYTnpjV3d1Wm1GNlpXNWtiMjFoYVhNdVkyOXRPMGx1YVhScFlXd2dRMkYwWVd4dlp6MW1ZWHBsYm1SdmJXRnBjenRRWlhKemFYTjBJRk5sWTNWeWFYUjVJRWx1Wm04OVZISjFaVHRWYzJWeUlFbEVQV1poZW1WdVpHOXRZV2x6TzFCaGMzTjNiM0prUFZGMU5EVTBja1p0VFVSQg==")));

            //Site-producao - AZURE
            SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOWFHcDBZWEF3YjNkcWJpNWtZWFJoWW1GelpTNTNhVzVrYjNkekxtNWxkRHRKYm1sMGFXRnNJRU5oZEdGc2IyYzlaR0pmWlhabGJuUnZjMFpOTzFCbGNuTnBjM1FnVTJWamRYSnBkSGtnU1c1bWJ6MVVjblZsTzFWelpYSWdTVVE5UTI5dVlYTmxiWE03VUdGemMzZHZjbVE5UXpCdU5EVmxiVFV5TURFMg==")));

            //krksa-vaio
            //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOU1UZzVMak00TGprd0xqRXdPVHRKYm1sMGFXRnNJRU5oZEdGc2IyYzlabUY2Wlc1a2IyMWhhWE03VUdWeWMybHpkQ0JUWldOMWNtbDBlU0JKYm1adlBWUnlkV1U3VlhObGNpQkpSRDFtWVhwbGJtUnZiV0ZwY3p0UVlYTnpkMjl5WkQxUmRUUTFOSEpHYlUxRVFRPT0=")));


            //Session["SessionCnn"] = SessionCnn;


            if (SessionCnn == null)
                SessionCnn = (SqlConnection)Session["SessionCnn"];
            else
                Session["SessionCnn"] = SessionCnn;

            //SessionIdioma = (String)Session["SessionIdioma"];
            //if (SessionIdioma == null)
            //{
            if ((Request["cdLng"] != null) &&
                (Request["cdLng"].ToString().Trim().ToUpper() != ""))
            {
                SessionIdioma = Request["cdLng"];
            }
            else
            {
                SessionIdioma = (String)Session["SessionIdioma"];
                if (SessionIdioma == null)
                    SessionIdioma = "PTBR";
            }
            //}
            Session["SessionIdioma"] = SessionIdioma;

            string cdEvento = "";
            if ((Request["codEvento"] != null) &&
                (Request["codEvento"].ToString().Trim().ToUpper() != ""))
            {
                cdEvento = cllEventos.Crypto.DecryptStringAES(Request["codEvento"]);
            }
            if ((cdEvento.Trim() == "") || (cdEvento.Trim().Length != 6))
            {
                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}",
                                "003"), true);
            }
            SessionEvento = oEventoCad.Pesquisar(cdEvento.Substring(0, 4), cdEvento, SessionCnn);
            Session["SessionEvento"] = SessionEvento;
            if (SessionEvento == null)
            {
                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                "003",
                                oEventoCad.RcMsg + cdEvento.Substring(0, 4)), true);
            }

            cdEvento = SessionEvento.CdEvento;

            if (SessionEvento.DsCon != "")
            {
                SessionCnn2 = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES(SessionEvento.DsCon)));
                Session["SessionCnn"] = SessionCnn2;

                SessionEvento = oEventoCad.Pesquisar(cdEvento.Substring(0, 4), cdEvento, SessionCnn2);
                Session["SessionEvento"] = SessionEvento;
                if (SessionEvento == null)
                {
                    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                    "003",
                                    oEventoCad.RcMsg + cdEvento.Substring(0, 4)), true);
                }
            }



            if ((SessionEvento.DtFinalEvento == null) ||
                (SessionEvento.DtFinalEvento < Geral.datahoraServidor(SessionCnn)))// DateTime.Parse(DateTime.Today.ToString("dd/MM/yyyy"))))
            {
                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                "006",
                                oEventoCad.RcMsg + cdEvento.Substring(0, 4)), true);
            }



            if (SessionEvento.FlSuspenderInscricaoWeb)
            {
                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                "007",
                                oEventoCad.RcMsg + cdEvento.Substring(0, 4)), true);
            }

            if ((SessionEvento.DtAberturaInscrWeb == null) ||
                (SessionEvento.DtAberturaInscrWeb > Geral.datahoraServidor(SessionCnn)))//DateTime.Parse(DateTime.Today.ToString("dd/MM/yyyy"))))
            {
                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                "004",
                                oEventoCad.RcMsg + cdEvento.Substring(0, 4)), true);
            }

            if ((SessionEvento.DtFechamentoInscrWeb == null) ||
                (SessionEvento.DtFechamentoInscrWeb < Geral.datahoraServidor(SessionCnn)))//DateTime.Parse(DateTime.Today.ToString("dd/MM/yyyy"))))
            {
                if (!SessionEvento.FlLiberarCertificacaoWeb)
                {
                    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                    "005",
                                    oEventoCad.RcMsg + cdEvento.Substring(0, 4)), true);
                }
            }


            
        }
        else
        {
            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            SessionIdioma = (String)Session["SessionIdioma"];
        }

        verificarIdioma();

        ToolkitScriptManager1.RegisterPostBackControl(btnPesquisarDadosVoo);
    }


    protected void verificarIdioma()
    {
        //DropDownList txtidioma = (DropDownList)Page.Master.FindControl("txtIdioma");
        //if (txtidioma != null)
        //{
       /* if (SessionIdioma == "PTBR")//(txtidioma.SelectedIndex == 0)
        {
            SessionIdioma = "PTBR";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloTelaEsq.Text = "Já sou cadastrado";
            lblTiuloTelaDir.Text = "Ainda não sou cadastrado";
            RFVCPF.Text = "Campo requerido";
            revTxtEmail.ErrorMessage = "E-mail inválido";
            rfvTxtEmail.ErrorMessage = "Campo requerido";
            LBLSENHA.Text = "Senha";
            REVTxtSenha.Text = "Senha inválida";
            btnCadastrar.Text = "Quero me cadastrar";
            btn_Login.Text = "Entrar";
            btnLembrarSenha.Text = "Esqueci minha senha";
            btnCadastrar2.Text = "Quero me cadastrar";
        }
        else if (SessionIdioma == "ENUS")//(txtidioma.SelectedIndex == 1)
        {
            SessionIdioma = "ENUS";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloTelaEsq.Text = "I am already registered";
            lblTiuloTelaDir.Text = "I am not registered";
            RFVCPF.Text = "Required field";
            revTxtEmail.ErrorMessage = "E-mail is invalid";
            rfvTxtEmail.ErrorMessage = "Required field";
            LBLSENHA.Text = "Password";
            REVTxtSenha.Text = "Invalid password";
            btnCadastrar.Text = "Sign up";
            btn_Login.Text = "Log in";
            btnLembrarSenha.Text = "I forgot my password";
            btnCadastrar2.Text = "Sign up";
        }
        else if (SessionIdioma == "ESP")//(txtidioma.SelectedIndex == 2)
        {
            SessionIdioma = "ESP";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloTelaEsq.Text = "Ya estoy registrado";
            lblTiuloTelaDir.Text = "No estoy registrado";
            RFVCPF.Text = "Campo obligatorio";
            revTxtEmail.ErrorMessage = "E-mail no es válido";
            rfvTxtEmail.ErrorMessage = "Campo obligatorio";
            LBLSENHA.Text = "Contraseña";
            REVTxtSenha.Text = "Contraseña no válida";
            btnCadastrar.Text = "Quiero registrarme";
            btn_Login.Text = "Entrar";
            btnLembrarSenha.Text = "He olvidado mi contraseña";
            btnCadastrar2.Text = "Quiero registrarme";
        }
        else if (SessionIdioma == "FRA")//(txtidioma.SelectedIndex == 3)
        {
            SessionIdioma = "FRA";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloTelaEsq.Text = "Je suis déjà inscrit";
            lblTiuloTelaDir.Text = "Je ne suis pas inscrit";
            RFVCPF.Text = "Champs obligatoires";
            revTxtEmail.ErrorMessage = "E-mail n'est pas valide";
            rfvTxtEmail.ErrorMessage = "Champs obligatoires";
            LBLSENHA.Text = "Mot de passe";
            REVTxtSenha.Text = "Mot de passe invalide";
            btnCadastrar.Text = "Je veux m'inscrire";
            btn_Login.Text = "Entrer";
            btnLembrarSenha.Text = "J'ai oublié mon mot de passe";
            btnCadastrar2.Text = "Je veux m'inscrire";
        }
        //}*/
    }

    protected void btnPesquisarDadosVoo_Click1(object sender, EventArgs e)
    {
        ParticipanteCad oParticipanteCad = new ParticipanteCad();
        
        SessionParticipante = oParticipanteCad.Pesquisar(SessionEvento.CdEvento, TXTDsCPF.Text.Replace(".", "").Replace("-", "").Trim(), SessionCnn);
        

        if (SessionParticipante != null)
        {

            Session["SessionParticipante"] = SessionParticipante;


            Response.Write("<script>window.open('frmDadosHospedagemExpoepi.aspx?p=" + cllEventos.Crypto.EncryptStringAES(SessionParticipante.CdParticipante) + "','_self');</script>");
            
            
        }
        else
        {
            //if (SessionIdioma == "PTBR")
                txtMsg.Text = "Participante não cadastrado";
            /*else if (SessionIdioma == "ENUS")
                txtMsg.Text = "Participant is not registered or invalid password";
            else if (SessionIdioma == "ESP")
                txtMsg.Text = "Participante no está registrado o la contraseña no válida";
            else if (SessionIdioma == "FRA")
                txtMsg.Text = "Participant n'est pas inscrit ou mot de passe invalide";*/
        }
    }
}