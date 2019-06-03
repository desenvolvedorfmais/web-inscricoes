using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Data;
using System.Data.SqlClient;

using CLLFuncoes;
using cllEventos;

//using MSXML2;

using System.Xml;
//using Recaptcha;
using MSCaptcha;

public partial class frmRegistrarPresenca : System.Web.UI.Page
{
    Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();

    CategoriaCad oCategoriaCad = new CategoriaCad();

    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    SqlConnection SessionCnn;

    String SessionIdioma = "";

    String SessionCateg = "";
    String SessionAtv = "";

    String SessionTipoSistema = "";

    String SessionTipoAcesso = "";

    String tpRotina = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //if ((Request["codEvento"] == null) || (Request["tpRotina"] == null))
            //{
            //    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
            //                    "003",
            //                    oEventoCad.RcMsg), true);
            //    return;
            //}

            
            //if ((SqlConnection)Session["SessionCnn"] == null)
            //{
                //local 1
                //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOWEzSnJjMkV0Y0dNN1NXNXBkR2xoYkNCRFlYUmhiRzluUFdSaVJYWmxiblJ2YzE5R1RUdFFaWEp6YVhOMElGTmxZM1Z5YVhSNUlFbHVabTg5VkhKMVpUdFZjMlZ5SUVsRVBYTmhPMUJoYzNOM2IzSmtQV3R5YTNOaE1UY3g=")));

                //local 2
                //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOUxseHJjbXR6WVhCak8wbHVhWFJwWVd3Z1EyRjBZV3h2Wnoxa1lrVjJaVzUwYjNOZlJrMDdVR1Z5YzJsemRDQlRaV04xY21sMGVTQkpibVp2UFZSeWRXVTdWWE5sY2lCSlJEMXpZVHRRWVhOemQyOXlaRDFyY210ellURTNNUT09")));

                //servidor
                //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOVptRjZaVzVrYjIxaGFYTTdTVzVwZEdsaGJDQkRZWFJoYkc5blBXUmlSWFpsYm5SdmMxOUdUVHRRWlhKemFYTjBJRk5sWTNWeWFYUjVJRWx1Wm04OVZISjFaVHRWYzJWeUlFbEVQWE5oTzFCaGMzTjNiM0prUFV0eWEzTmhNVGN4")));

                //MinSaude
                //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOUxseHpjV3hsZUhCeVpYTnpPMGx1YVhScFlXd2dRMkYwWVd4dlp6MWtZa1YyWlc1MGIzTmZSazA3VUdWeWMybHpkQ0JUWldOMWNtbDBlU0JKYm1adlBWUnlkV1U3VlhObGNpQkpSRDF6WVR0UVlYTnpkMjl5WkQxU1lVTTVPREk1TnpNPQ==")));

                //Site
                //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOU1UZzVMak00TGprMUxqY3lPMGx1YVhScFlXd2dRMkYwWVd4dlp6MW1ZWHBsYm1SdmJXRnBjenRRWlhKemFYTjBJRk5sWTNWeWFYUjVJRWx1Wm04OVZISjFaVHRWYzJWeUlFbEVQV1poZW1WdVpHOXRZV2x6TzFCaGMzTjNiM0prUFZGMU5EVTBja1p0")));

                //Site2-historico
                //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOWJYTnpjV3d1Wm1GNlpXNWtiMjFoYVhNdVkyOXRPMGx1YVhScFlXd2dRMkYwWVd4dlp6MW1ZWHBsYm1SdmJXRnBjekU3VUdWeWMybHpkQ0JUWldOMWNtbDBlU0JKYm1adlBWUnlkV1U3VlhObGNpQkpSRDFtWVhwbGJtUnZiV0ZwY3pFN1VHRnpjM2R2Y21ROVVYVTBOVFJ5Um0wPQ==")));

                //Site-producao
                //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOWJYTnpjV3d1Wm1GNlpXNWtiMjFoYVhNdVkyOXRPMGx1YVhScFlXd2dRMkYwWVd4dlp6MW1ZWHBsYm1SdmJXRnBjenRRWlhKemFYTjBJRk5sWTNWeWFYUjVJRWx1Wm04OVZISjFaVHRWYzJWeUlFbEVQV1poZW1WdVpHOXRZV2x6TzFCaGMzTjNiM0prUFZGMU5EVTBja1p0VFVSQg==")));

                //Site-producao - AZURE
                SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOWFHcDBZWEF3YjNkcWJpNWtZWFJoWW1GelpTNTNhVzVrYjNkekxtNWxkRHRKYm1sMGFXRnNJRU5oZEdGc2IyYzlaR0pmWlhabGJuUnZjMFpOTzFCbGNuTnBjM1FnVTJWamRYSnBkSGtnU1c1bWJ6MVVjblZsTzFWelpYSWdTVVE5UTI5dVlYTmxiWE03VUdGemMzZHZjbVE5UXpCdU5EVmxiVFV5TURFMg==")));

            //Site-producao - IP
            //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOU1UZzVMak00TGprd0xqRXdPVHRKYm1sMGFXRnNJRU5oZEdGc2IyYzlabUY2Wlc1a2IyMWhhWE03VUdWeWMybHpkQ0JUWldOMWNtbDBlU0JKYm1adlBWUnlkV1U3VlhObGNpQkpSRDFtWVhwbGJtUnZiV0ZwY3p0UVlYTnpkMjl5WkQxUmRUUTFOSEpHYlUxRVFRPT0=")));

            //}
            //else
            //    SessionCnn = (SqlConnection)Session["SessionCnn"];


            //if (SessionCnn.State != ConnectionState.Open)
            //{
            //    try
            //    {
            //        SessionCnn.Open();
            //    }
            //    catch
            //    {
            //        //_Rc = true;
            //        //_RcMsg = "Conexão inválida";
            //        return;
            //    }
            //}

            Session["SessionCnn"] = SessionCnn;

            

            //string cdEvento = cllEventos.Crypto.DecryptStringAES(Request.QueryString["codEvento"]);
            tpRotina = Request.QueryString["tpRotina"];
            Session["tpRotina"] = tpRotina;


            if ((Request.QueryString["cdLng"] != null) &&
                (Request.QueryString["cdLng"].ToString().Trim().ToUpper() != ""))
            {
                SessionIdioma = Request.QueryString["cdLng"];
            }
            else
            {
                SessionIdioma = (String)Session["SessionIdioma"];
                if (SessionIdioma == null)
                    SessionIdioma = "PTBR";
            }

            Session["SessionIdioma"] = SessionIdioma;


            if ((Request.QueryString["tpSist"] != null) &&
                    (Request.QueryString["tpSist"].ToString().Trim().ToUpper() != ""))
            {
                SessionTipoSistema = Request.QueryString["tpSist"];
            }
            else
            {
                SessionTipoSistema = (String)Session["SessionTipoSistema"];
                if (SessionTipoSistema == null)
                    SessionTipoSistema = "NRM";
            }
            Session["SessionTipoSistema"] = SessionTipoSistema;



            if ((Request.QueryString["tpAcesso"] != null) &&
                    (Request.QueryString["tpAcesso"].ToString().Trim().ToUpper() != ""))
            {
                SessionTipoAcesso = Request.QueryString["tpAcesso"];
            }
            else
            {
                SessionTipoAcesso = (String)Session["tpAcesso"];
                if (SessionTipoAcesso == null)
                    SessionTipoAcesso = "NRM";
            }
            Session["tpAcesso"] = SessionTipoAcesso;



            string cdEvento = "";
            if ((Request.QueryString["codEvento"] != null) &&
                (Request.QueryString["codEvento"].ToString().Trim().ToUpper() != ""))
            {
                cdEvento = cllEventos.Crypto.DecryptStringAES(Request.QueryString["codEvento"]);
            }
            if ((cdEvento.Trim() == "") || (cdEvento.Trim().Length != 6))
            {
                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}",
                                "003"), true);
            }

            SessionCateg = "";
            if ((Request.QueryString["cat"] != null) &&//codigo da categoria
                (Request.QueryString["cat"].ToString().Trim().ToUpper() != ""))
            {
                SessionCateg = Request.QueryString["cat"].ToString();
            }

            Session["SessionCateg"] = SessionCateg;


            SessionAtv = "";
            if ((Request.QueryString["atv"] != null) &&//codigo da atividade
                    (Request.QueryString["atv"].ToString().Trim().ToUpper() != ""))
            {
                SessionAtv = Request.QueryString["atv"].ToString();
            }
            Session["sessionAtv"] = SessionAtv;

            
            Response.Cookies["EventoInfo"]["eventoCod"] = cdEvento;
            Response.Cookies["EventoInfo"]["lngCod"] = SessionIdioma;
            Response.Cookies["EventoInfo"]["categCod"] = SessionCateg;
            Response.Cookies["EventoInfo"]["atividadeCod"] = SessionAtv;
            Response.Cookies["EventoInfo"]["tpSistema"] = SessionTipoSistema;
            Response.Cookies["EventoInfo"]["tpRotina"] = tpRotina;
            Response.Cookies["EventoInfo"].Expires = DateTime.Now.AddDays(1);

                

            //if ((Evento)Session["SessionEvento"] == null)
            //{
            if ((cdEvento.Trim() == "") || (cdEvento.Trim().Length != 6))
                {
                    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}",
                                    "04"), true);
                }

                SessionEvento = oEventoCad.Pesquisar(cdEvento.Substring(0, 4), cdEvento, SessionCnn);
                Session["SessionEvento"] = SessionEvento;


                if (SessionEvento == null)
                {
                    //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                    //                "003",
                    //                oEventoCad.RcMsg + cdEvento.Substring(0, 4)), true);

                    Server.Transfer("frmSessaoExpirada.aspx", true);
                }

            //if ((SessionEvento.DtFechamentoInscrWeb == null) ||
            //    (SessionEvento.DtFechamentoInscrWeb < DateTime.Parse(DateTime.Today.ToString("dd/MM/yyyy"))))
            //{
            //    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
            //                    "005",
            //                    oEventoCad.RcMsg + cdEvento.Substring(0, 4)), true);
            //}
            //}
            //else
            //{
            //    SessionEvento = (Evento)Session["SessionEvento"];
            //    Session["SessionEvento"] = SessionEvento;
            //}

            Geral oGeral = new Geral();
            if (oGeral.verificarSiteManutencao("1", SessionCnn))
            {
                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                "05",
                                ""), true);
            }

            AnoEvento.Value = SessionEvento.CdEvento;
            lblRotina.Value = Request.QueryString["tpRotina"]; 

            if (SessionEvento.DsCon != "")
            {
                SqlConnection SessionCnn2 = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES(SessionEvento.DsCon)));
                Session["SessionCnn"] = SessionCnn2;
                SessionCnn = SessionCnn2;
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
                (SessionEvento.DtFinalEvento < DateTime.Parse(DateTime.Today.ToString("dd/MM/yyyy"))))
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
                (SessionEvento.DtAberturaInscrWeb > DateTime.Parse(DateTime.Today.ToString("dd/MM/yyyy"))))
            {
                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                "004",
                                oEventoCad.RcMsg + cdEvento.Substring(0, 4)), true);
            }


            if ((Request.QueryString["cdLng"] != null) &&
                   (Request.QueryString["cdLng"].ToString().Trim().ToUpper() != ""))
            {
                SessionIdioma = Request.QueryString["cdLng"];
            }
            else
            {
                SessionIdioma = (String)Session["SessionIdioma"];
                if (SessionIdioma == null)
                    SessionIdioma = "PTBR";
            }

            //SessionIdioma = (String)Session["SessionIdioma"];
            if (SessionIdioma == null)
                SessionIdioma = "PTBR";
            Session["SessionIdioma"] = SessionIdioma;


            txtCdInscricao.Text = "000000000";
            txtCdInscricao.Focus();
            

            /*
            if (SessionCnn == null)
                SessionCnn = (SqlConnection)Session["SessionCnn"];
            else
                Session["SessionCnn"] = SessionCnn;

            

            
            


            //verificarIdioma(SessionIdioma);

            //if (SessionParticip == null)
            //    SessionParticip = (Participante)Session["SessionParticip"];
            //else
            //    Session["SessionParticip"] = SessionParticip;


            if (SessionEvento == null)
                SessionEvento = (Evento)Session["SessionEvento"];
            else
                Session["SessionEvento"] = SessionEvento;

            if (SessionEvento == null)
                Server.Transfer("frmSessaoExpirada.aspx", true);


            */

            //if ((SessionEvento.DsCampoAutenticacao.ToUpper().Replace("-", "") == "EMAIL") || (SessionIdioma != "PTBR"))
            //{
            //    lblIdInscr.Visible = txtEmail.Visible = revTxtEmail.Visible = rfvTxtEmail.Visible = true;
            //    lblCPF.Visible = txtCPF.Visible = CustomValidator2.Visible =  RFVCPF.Visible = false;
            //    txtEmail.Focus();

                
            //}
            //else
            //    txtCPF.Focus();

            //if ((SessionEvento.CdCliente == "0013") || (SessionEvento.CdEvento == "005102"))
            //{
                //linSenha.Visible = false;
                //btnLembrarSenha.Visible = false;

                //lincaracteres.Visible = false;
                //divCaptcha.Visible = false;
            //}
            //lincaracteres.Visible = false;
        }
        else
        {
            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"]; 
            
            SessionIdioma = (String)Session["SessionIdioma"];

            tpRotina = (string)Session["tpRotina"];
        }

        verificarIdioma(SessionIdioma);

        ToolkitScriptManager1.RegisterPostBackControl(Button1);
        ToolkitScriptManager1.RegisterPostBackControl(btnLimpar);

        if (SessionEvento == null)
            Server.Transfer("frmSessaoExpirada.aspx", true);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        if (!Page.IsValid)


        

        nrcadastro.Text = "";
        //cdPart.Value = "";
        NoParticipante.Text = "";
        nomeCategoria.Text = "";
        CPF.Text = "";
        Cidade.Text = "";
        Local.Text = "";
        Data.Text = "";
        Hora.Text = "";

        lblMsg.Text = "";
        //if (txtCPF.Visible)
        if ((txtCdInscricao.Text == "") || (txtCdInscricao.Text == "000000000"))
        {
            if (txtCPF.Text.Replace(".", "").Replace("-", "").Length < 11)
            {
                lblMsg.Text = "Nr Inscrição ou CPF Inválido";
                divMsg.Attributes["class"] = "msgFalha";
                return;
            }
                      

            string tmpCPF = ValidarCPF("", SessionEvento.CdEvento, txtCPF.Text, "", SessionCnn);
            if (tmpCPF != "")
            {
                lblMsg.Text = tmpCPF;
                divMsg.Attributes["class"] = "msgFalha";
                return;
            }

        }
        


        

        ParticipanteCad oParticipanteCad = new ParticipanteCad();

        

        if ((txtCdInscricao.Text == "") || (txtCdInscricao.Text == "000000000"))
        {
            SessionParticipante = oParticipanteCad.Pesquisar(SessionEvento.CdEvento, "nuCPFCNPJ",
                txtCPF.Text.Replace(".", "").Replace("-", "").Trim(), SessionCnn);
        }
        else //if (txtEmail.Visible)
        {
            SessionParticipante = oParticipanteCad.Pesquisar(SessionEvento.CdEvento, "cdParticipante",
                txtCdInscricao.Text.Trim().PadLeft(9,'0'), SessionCnn);
        }





        if (SessionParticipante != null)
        {
            pedidos_esq.Visible = true;

            nrcadastro.Text = SessionParticipante.CdParticipante;
            //cdPart.Value = SessionParticipante.CdParticipante;
            NoParticipante.Text = SessionParticipante.NoParticipante;
            nomeCategoria.Text = SessionParticipante.Categoria.NoCategoria;
            CPF.Text = oClsFuncoes.CPFCNPJMascarar(SessionParticipante.NuCPFCNPJ);
            Cidade.Text = SessionParticipante.DsAuxiliar1;
            Local.Text = SessionParticipante.DsAuxiliar2;
            Data.Text = SessionParticipante.DsAuxiliar3;
            Hora.Text = SessionParticipante.DsAuxiliar5;

            if (!SessionParticipante.FlAtivo)
            {
                if (SessionIdioma == "PTBR")
                    lblMsg.Text = "Cadastro inativo! Favor entrar em contato com a organização do evento.";
                else if (SessionIdioma == "ENUS")
                    lblMsg.Text = "Register idle! Please contact the event organizers.";
                else if (SessionIdioma == "ESP")
                    lblMsg.Text = "Registrarse inactivada! Póngase en contacto con los organizadores del evento.";
                else if (SessionIdioma == "FRA")
                    lblMsg.Text = "Inscription inactivé! S'il vous plaît contacter les organisateurs de l'événement.";


                divMsg.Attributes["class"] = "msgFalha";
                return;
            }

            Session["SessionEvento"] = SessionEvento;

            Session["SessionParticipante"] = SessionParticipante;

            if (SessionParticipante.DsAuxiliar4 != "SIM")
            {
                SessionParticipante.DsAuxiliar4 = "SIM";
                SessionParticipante.CdOperador = "000000000";
                SessionParticipante = oParticipanteCad.Gravar(SessionParticipante, SessionCnn);
            }

            if (SessionParticipante.CdCredencial == "0")
            {
                if (oParticipanteCad.GerarCredencial(SessionParticipante, SessionCnn))
                {
                    lblMsg.Text = "Presença registrada com sucesso!";
                    divMsg.Attributes["class"] = "msgOk";
                }
                else
                {
                   
                    lblMsg.Text = oParticipanteCad.RcMsg;
                    divMsg.Attributes["class"] = "msgFalha";
                }
            }
            else
            {
                lblMsg.Text = "Presença registrada com sucesso!";
                divMsg.Attributes["class"] = "msgOk";
            }
            //if (!SessionParticipante.FlValidarEmail)
            //{
            //    Geral oGeral = new Geral();
            //    oGeral.ValidarEmailParticipante(SessionParticipante, SessionCnn);
            //}





        }
        else
        {
            pedidos_esq.Visible = false;

            nrcadastro.Text = "";
            //cdPart.Value = "";
            NoParticipante.Text = "";
            nomeCategoria.Text = "";
            CPF.Text = "";
            Cidade.Text = "";
            Local.Text = "";
            Data.Text = "";
            Hora.Text = "";

            if (SessionIdioma == "PTBR")
                lblMsg.Text = "Participante não cadastrado";
            else if (SessionIdioma == "ENUS")
                lblMsg.Text = "Participant is not registered";
            else if (SessionIdioma == "ESP")
                lblMsg.Text = "Participante no está registrado";
            else if (SessionIdioma == "FRA")
                lblMsg.Text = "Participant n'est pas inscrit";

           
            divMsg.Attributes["class"] = "msgFalha";
        }
        
    }

    protected string ValidarCPF(string prmCdParticipante, string prmCdEvento, string prmCPF, string prmCdCategoria, SqlConnection prmCnn)
    {
        if ((!oClsFuncoes.CPFCNPJValidar(prmCPF)))
        {
            CategoriaCad oCategoriaCad = new CategoriaCad();
            Categoria oCategoria = oCategoriaCad.Pesquisar(prmCdEvento, prmCdCategoria, prmCnn);
            if (((SessionEvento != null) && (SessionEvento.FlLiberarCPFCoringa)))// || (!oCategoria.FlCPFCNPJObrigatorio))
                return "";

            return "CPF Inválido!";
        }
        else
            return "";
        //{
        //    string tmpCPF = prmCPF.Replace(".", "").Replace("-", "");

        //    /*if ((tmpCPF == "11111111111") ||
        //        (tmpCPF == "22222222222") ||
        //        (tmpCPF == "33333333333") ||
        //        (tmpCPF == "44444444444") ||
        //        (tmpCPF == "55555555555") ||
        //        (tmpCPF == "66666666666") ||
        //        (tmpCPF == "77777777777") ||
        //        (tmpCPF == "88888888888") ||
        //        (tmpCPF == "99999999999") ||
        //        (tmpCPF == "00000000000"))
        //    {
        //        CategoriaCad oCategoriaCad = new CategoriaCad();
        //        Categoria oCategoria = oCategoriaCad.Pesquisar(prmCdEvento, prmCdCategoria, prmCnn);
        //        if (((SessionEvento != null) && (SessionEvento.FlLiberarCPFCoringa)) || (!oCategoria.FlCPFCNPJObrigatorio) || (oCategoria.FlLiberarCPFCNPJCoringa))
        //            return "";
        //        return "CPF Inválido!";
        //    }
        //    else
        //    {*/
        //    return oParticipanteCad.verificarDuplicidadeCPF(prmCdParticipante, prmCdEvento, tmpCPF, prmCdCategoria, prmCnn);
        //    //}

        //}

    }

    protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
    {
        
        if (txtCPF.Text != "")
        {

            args.IsValid = true;

        }
        else
        {

            args.IsValid = false;

        }
    }
    public int ContarEntradas(string prmcdEvento, string prmCdParticipante)
    {

        //if (prmData == "")
        //    return;

        //lblMsg.Visible = false;
        if (SessionCnn == null)
        {
            //_erroNoCampo = true;
            lblMsg.Visible = true;
            lblMsg.Text = "Conexão inválida ou inexistente";
            return -1;
        }
        if (SessionCnn.State != ConnectionState.Open)
        {
            try
            {
                SessionCnn.Open();
            }
            catch
            {
                //_erroNoCampo = true;
                lblMsg.Visible = true;
                lblMsg.Text = "Conexão inválida";
                return -1;
            }
        }

        DataTable DT = new DataTable();
        SqlDataAdapter Dap;
        try
        {
            try
            {
                string cmd =
                    "SELECT distinct " +
                    "    cdParticipante, " +
                    "       convert(varchar(10),[dtEntrada],103) dat, " +
                    "       substring(convert(varchar(10),[dtEntrada],108),1,2) turn, " +
                    "       case when (substring(convert(varchar(10),[dtEntrada],108),1,2) > 07 and substring(convert(varchar(10),[dtEntrada],108),1,2) <= 12) " +
                    "            then 'M'  " +
                    "            else case when (substring(convert(varchar(10),[dtEntrada],108),1,2) > 12 and substring(convert(varchar(10),[dtEntrada],108),1,2) <= 18) " +
                    "            then 'T' " +
                    "            else 'N' " +
                    "        end end turno " +
                    "  FROM [dbo].[tbFrequencias] " +
                    "  where cdEvento = '" + prmcdEvento + "' " +
                    "  and [cdParticipante] = '" + prmCdParticipante + "' ";


                SqlCommand comando = new SqlCommand(
                     cmd, SessionCnn);


                Dap = new SqlDataAdapter(comando);

                Dap.TableMappings.Add("Hora", "tbFrequencia");
                Dap.Fill(DT);

                SessionCnn.Close();

                if (DT != null)
                {
                    return DT.DefaultView.Count;
                }
                else
                    return 0;




            }
            catch (Exception Ex)
            {
                //_erroNoCampo = true;
                lblMsg.Visible = true;
                lblMsg.Text = "Erro ao selecionar Horários!\n" + Ex.Message;
                SessionCnn.Close();
                return -1;
            }
        }
        finally
        {
            SessionCnn.Close();
        }


    }


    protected void verificarIdioma(string prmIdioma)
    {
        if (prmIdioma == "PTBR")//(txtidioma.SelectedIndex == 0)
        {
            SessionIdioma = "PTBR";
            Session["SessionIdioma"] = SessionIdioma;

            lblCPF.Text = "Informe o CPF:";
            lblIdInscr.Text = "Nr Inscrição:";
            Button1.Text = "Verificar";

            lblID.Text = "Nr Cadastro:";
            lblPart.Text = "Participante:";
            lblCateg.Text = "Categoria:";

            //CustomValidator2.ErrorMessage = "Campo obrigatório.";
            //RFVCPF.ErrorMessage = "Campo obrigatório.";
            //revTxtEmail.ErrorMessage = "E-mail inválido";
            //rfvTxtEmail.ErrorMessage = "Campo requerido";

            
           // Button2.Text="Fazer Download";
        }
        else if (prmIdioma == "ENUS")//(txtidioma.SelectedIndex == 1)
        {
            SessionIdioma = "ENUS";
            Session["SessionIdioma"] = SessionIdioma;

            //lblCPF.Text = "Informe o CPF:";
            lblIdInscr.Text = "Nr Register:";
            Button1.Text = "Check";

            lblID.Text = "Nr Register:";
            lblPart.Text = "Participant:";
            lblCateg.Text = "Category:";

            //CustomValidator2.ErrorMessage = "Required field.";
            //RFVCPF.ErrorMessage = "Required field.";
            //revTxtEmail.ErrorMessage = "Invalid email";
            //rfvTxtEmail.ErrorMessage = "Required field.";

            btnLimpar.Text = "Download";
        }
        else if (prmIdioma == "ESP")//(txtidioma.SelectedIndex == 2)
        {
            SessionIdioma = "ESP";
            Session["SessionIdioma"] = SessionIdioma;

            //lblCPF.Text = "Informe o CPF:";
            lblIdInscr.Text = "Nº Registro:";
            Button1.Text = "Verificar";

            lblID.Text = "Nº Registro:";
            lblPart.Text = "Nome:";
            lblCateg.Text = "Categoría:";

            //CustomValidator2.ErrorMessage = "Campo obligatorio.";
            //RFVCPF.ErrorMessage = "Campo obligatorio.";
            //revTxtEmail.ErrorMessage = "E-mail no válido";
            //rfvTxtEmail.ErrorMessage = "Campo obligatorio.";

            btnLimpar.Text = "Descargar";
        }
        else if (prmIdioma == "FRA")//(txtidioma.SelectedIndex == 3)
        {
            SessionIdioma = "FRA";
            Session["SessionIdioma"] = SessionIdioma;

            
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        pedidos_esq.Visible = false;

        nrcadastro.Text = "";
        //cdPart.Value = "";
        NoParticipante.Text = "";
        nomeCategoria.Text = "";
        CPF.Text = "";
        Cidade.Text = "";
        Local.Text = "";
        Data.Text = "";
        Hora.Text = "";


        txtCPF.Text = "";
        txtCdInscricao.Text = "000000000";
        txtCdInscricao.Focus();
        divMsg.Attributes["class"] = "msgLimpar";
    }
}