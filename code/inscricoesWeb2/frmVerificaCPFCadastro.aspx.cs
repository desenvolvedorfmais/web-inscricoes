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

public partial class frmVerificaCPFCadastro : System.Web.UI.Page
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
            //        return ;
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





            //Response.Cookies.Add(new HttpCookie("eventoCod", cdEvento));
            //Response.Cookies.Add(new HttpCookie("lngCod", SessionIdioma));
            //Response.Cookies.Add(new HttpCookie("categCod", SessionCateg));
            //Response.Cookies.Add(new HttpCookie("atividadeCod", SessionAtv));
            //Response.Cookies.Add(new HttpCookie("tpSistema", SessionTipoSistema));
            //Response.Cookies.Add(new HttpCookie("tpRotina", tpRotina));

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

            if (tpRotina.ToUpper() != "EMTCERT")
            {
                if ((SessionEvento.DtFinalEvento == null) ||
                        (SessionEvento.DtFinalEvento < DateTime.Parse(DateTime.Today.ToString("dd/MM/yyyy"))))
                {
                    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                    "006",
                                    oEventoCad.RcMsg + cdEvento.Substring(0, 4)), true);
                }
            }
            else
            {
                if (!SessionEvento.FlLiberarCertificacaoWeb)
                {
                    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                        "039",
                        ""), true);
                }
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



            if (SessionEvento.CdEvento == "006602")
            {
                lblInstucoes.Text =
                    "<p>&nbsp;</p>" +
                    "<p>Mais do que uma nova marca, um novo jeito de ser.</p>" +
                    "<p>Confirme sua presen&ccedil;a no evento que vai conectar voc&ecirc; &nbsp;a uma nova era.</p>" +
                    "<p>&nbsp;</p>";
            }
            

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

            if ((SessionEvento.DsCampoAutenticacao.ToUpper().Replace("-", "").ToUpper() == "CPF"))// || (SessionIdioma != "PTBR"))
            {
                divCPF.Visible = true;
                lblCPF.Visible = txtCPF.Visible = CustomValidator2.Visible = RFVCPF.Visible = true;
                
                divEmail.Visible = false;
                lblEmail.Visible = txtEmail_verif_cpf.Visible = revTxtEmail.Visible = rfvTxtEmail.Visible = false;

                divNrInscr.Visible = false;
                lblIdInscricao.Visible = txtIDInscr.Visible = rfvID.Visible = false;

                txtCPF.Focus();

            }
            else if ((SessionEvento.DsCampoAutenticacao.ToUpper().Replace("-", "").ToUpper() == "EMAIL") || (SessionIdioma != "PTBR"))
            {
                divCPF.Visible = false;
                lblCPF.Visible = txtCPF.Visible = CustomValidator2.Visible = RFVCPF.Visible = false;
                
                divEmail.Visible = true;
                lblEmail.Visible = txtEmail_verif_cpf.Visible = revTxtEmail.Visible = rfvTxtEmail.Visible = true;

                divNrInscr.Visible = false;
                lblIdInscricao.Visible = txtIDInscr.Visible = rfvID.Visible = false;

                txtEmail_verif_cpf.Focus();

                
            }
            else if ((SessionEvento.DsCampoAutenticacao.ToUpper().Replace("-", "").ToUpper() == "ID") || (SessionIdioma != "PTBR"))
            {
                divCPF.Visible = false;
                lblCPF.Visible = txtCPF.Visible = CustomValidator2.Visible = RFVCPF.Visible = false;

                divEmail.Visible = false;
                lblEmail.Visible = txtEmail_verif_cpf.Visible = revTxtEmail.Visible = rfvTxtEmail.Visible = false;

                divNrInscr.Visible = true;
                lblIdInscricao.Visible = txtIDInscr.Visible = rfvID.Visible = true;

                txtIDInscr.Focus();

            }
            else if ((SessionEvento.DsCampoAutenticacao.ToUpper().Replace("-", "").ToUpper() == "CPF_EMAIL") || (SessionIdioma != "PTBR"))
            {
                divCPF.Visible = true;
                lblCPF.Visible = txtCPF.Visible = true;
                CustomValidator2.Visible = RFVCPF.Visible = false;

                divEmail.Visible = true;
                lblEmail_Ou.Visible = lblEmail.Visible = txtEmail_verif_cpf.Visible = revTxtEmail.Visible = true;
                rfvTxtEmail.Visible = false;

                divNrInscr.Visible = false;
                lblIdInscricao.Visible = txtIDInscr.Visible = rfvID.Visible = false;

                txtCPF.Focus();

            }
            else if ((SessionEvento.DsCampoAutenticacao.ToUpper().Replace("-", "").ToUpper() == "CPF_ID") || (SessionIdioma != "PTBR"))
            {
                divCPF.Visible = true;
                lblCPF.Visible = txtCPF.Visible = true;
                CustomValidator2.Visible = RFVCPF.Visible = false;

                divEmail.Visible = false;
                lblEmail.Visible = txtEmail_verif_cpf.Visible = revTxtEmail.Visible = rfvTxtEmail.Visible = false;

                divNrInscr.Visible = true;
                lblIdInscricao_Ou.Visible = lblIdInscricao.Visible = txtIDInscr.Visible = true;
                rfvID.Visible = false;

                txtCPF.Focus();

            }
            else if ((SessionEvento.DsCampoAutenticacao.ToUpper().Replace("-", "").ToUpper() == "EMAIL_ID") || (SessionIdioma != "PTBR"))
            {
                divCPF.Visible = false;
                lblCPF.Visible = txtCPF.Visible = false;
                CustomValidator2.Visible = RFVCPF.Visible = false;

                divEmail.Visible = true;
                lblEmail.Visible = txtEmail_verif_cpf.Visible = revTxtEmail.Visible = true; 
                rfvTxtEmail.Visible = false;

                divNrInscr.Visible = true;
                lblIdInscricao_Ou.Visible = lblIdInscricao.Visible = txtIDInscr.Visible = true;
                rfvID.Visible = false;

                txtEmail_verif_cpf.Focus();

            }
            

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
        ToolkitScriptManager1.RegisterPostBackControl(Button2);

        if (SessionEvento == null)
            Server.Transfer("frmSessaoExpirada.aspx", true);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        //if (!Page.IsValid)
        {
            SessionParticipante = null;
            pedidos_esq.Visible = false;
            Button2.Visible = false;

            lblMsg.Text = "";
        }
        if ((txtCPF.Visible) && !((txtEmail_verif_cpf.Visible) || (txtIDInscr.Visible)))
        {
            if (txtCPF.Text.Replace(".", "").Replace("-", "").Length < 11)
            {
                lblMsg.Text = "CPF Inválido";
                return;
            }


            string tmpCPF = ValidarCPF("", SessionEvento.CdEvento, txtCPF.Text, "", SessionCnn);
            if (tmpCPF != "")
            {
                lblMsg.Text = tmpCPF;
                return;
            }

        }


        if ((txtEmail_verif_cpf.Visible) && !((txtCPF.Visible) || (txtIDInscr.Visible)))
        {
            if (txtEmail_verif_cpf.Text.Length <= 0)
            {
                lblMsg.Text = "E-mail requerido";
                if (SessionIdioma == "PTBR")
                    lblMsg.Text = "E-mail requerido";
                else if (SessionIdioma == "ENUS")
                    lblMsg.Text = "E-mail required";
                else if (SessionIdioma == "ESP")
                    lblMsg.Text = "E-mail requerido";
                else if (SessionIdioma == "FRA")
                    lblMsg.Text = "Inscription inactivé! S'il vous plaît contacter les organisateurs de l'événement.";
                return;
            }
        }

        if ((txtIDInscr.Visible) && !((txtCPF.Visible) || (txtEmail_verif_cpf.Visible)))
        {
            if (txtIDInscr.Text.Length <= 0)
            {
                lblMsg.Text = "Número de inscriçaõ Inválido";
                return;
            }
        }

        if ((txtCPF.Visible) && (txtEmail_verif_cpf.Visible))
        {
            if ((txtCPF.Text.Replace(".", "").Replace("-", "").Trim().Length <= 0) && (txtEmail_verif_cpf.Text.Length <= 0))
            {
                lblMsg.Text = "Informe o valor para apenas um dos campos";
                return;
            }
            if ((txtCPF.Text.Replace(".", "").Replace("-", "").Trim().Length > 0) && (txtEmail_verif_cpf.Text.Length > 0))
            {
                lblMsg.Text = "Informe o valor para apenas um dos campos";
                return;
            }

            if (txtCPF.Text.Replace(".", "").Replace("-", "").Trim().Length > 0)
            {
                if (txtCPF.Text.Replace(".", "").Replace("-", "").Length < 11)
                {
                    lblMsg.Text = "CPF Inválido";
                    return;
                }


                string tmpCPF = ValidarCPF("", SessionEvento.CdEvento, txtCPF.Text, "", SessionCnn);
                if (tmpCPF != "")
                {
                    lblMsg.Text = tmpCPF;
                    return;
                }

            }
        }

        if ((txtCPF.Visible) && (txtIDInscr.Visible))
        {
            if ((txtCPF.Text.Replace(".", "").Replace("-", "").Trim().Length <= 0) && (txtIDInscr.Text.Length <= 0))
            {
                lblMsg.Text = "Informe o valor para apenas um dos campos";
                return;
            }
            if ((txtCPF.Text.Replace(".", "").Replace("-", "").Trim().Length > 0) && (txtIDInscr.Text.Length > 0))
            {
                lblMsg.Text = "Informe o valor para apenas um dos campos";
                return;
            }

            if (txtCPF.Text.Replace(".", "").Replace("-", "").Trim().Length > 0)
            {
                if (txtCPF.Text.Replace(".", "").Replace("-", "").Length < 11)
                {
                    lblMsg.Text = "CPF Inválido";
                    return;
                }


                string tmpCPF = ValidarCPF("", SessionEvento.CdEvento, txtCPF.Text, "", SessionCnn);
                if (tmpCPF != "")
                {
                    lblMsg.Text = tmpCPF;
                    return;
                }

            }
        }



        if ((txtEmail_verif_cpf.Visible) && (txtIDInscr.Visible))
        {
            if ((txtEmail_verif_cpf.Text.Trim().Length <= 0) && (txtIDInscr.Text.Length <= 0))
            {
                lblMsg.Text = "Informe o valor para apenas um dos campos";
                return;
            }
            if ((txtEmail_verif_cpf.Text.Trim().Length > 0) && (txtIDInscr.Text.Length > 0))
            {
                lblMsg.Text = "Informe o valor para apenas um dos campos";
                return;
            }
        }

        //if (txtCaptcha.Text.Trim() == "")
        //{
        //    lblMsg.Text = "Caracteres inválidos";
        //    return;
        //}

        //Captcha1.ValidateCaptcha(txtCaptcha.Text.Trim());

        //if (!Captcha1.UserValidated)
        //{
        //    lblMsg.Text = "Caracteres inválidos!";
        //    return;
        //}

        ParticipanteCad oParticipanteCad = new ParticipanteCad();

        //if (SessionEvento.CdCliente != "0013")
        //{
        //SessionParticipante = oParticipanteCad.ValidarContaSenha(SessionEvento.CdEvento, TXTDsCPF.Text.Replace(".", "").Replace("-", "").Trim(), txtSenha.Text.Trim(), _cnn);
        //if ((SessionEvento.DsCampoAutenticacao.ToUpper().Replace("-", "") == "CPF") && (SessionIdioma == "PTBR"))
        //{
        //if (txtSenha.Visible)
        //{
        //    SessionParticipante = oParticipanteCad.autenticarWeb(SessionEvento.CdEvento, "CPF", txtCPF.Text.Replace(".", "").Replace("-", "").Trim(), txtSenha.Text, SessionCnn);
        //}
        //else
        if ((txtCPF.Visible) && (txtCPF.Text.Replace(".", "").Replace("-", "").Trim().Length > 0))
        {
            SessionParticipante = oParticipanteCad.Pesquisar(SessionEvento.CdEvento, "nuCPFCNPJ", txtCPF.Text.Replace(".", "").Replace("-", "").Trim(), SessionCnn);
        }
        else if ((txtIDInscr.Visible) && (txtIDInscr.Text.Length > 0))
        {
            SessionParticipante = oParticipanteCad.Pesquisar(SessionEvento.CdEvento, "cdParticipante", txtIDInscr.Text.PadLeft(9, '0').Trim(), SessionCnn);
        }
        else
        if ((txtEmail_verif_cpf.Visible) && (txtEmail_verif_cpf.Text.Trim().Length > 0))
        {
            SessionParticipante = oParticipanteCad.Pesquisar(SessionEvento.CdEvento, "dsEmail", txtEmail_verif_cpf.Text.Trim().ToLower(), SessionCnn);
        }

        //}
        //else
        //{
        //    SessionParticipante = oParticipanteCad.autenticarWeb(SessionEvento.CdEvento, "EMAIL", txtEmail.Text.ToLower().Trim(), txtSenha.Text, SessionCnn);
        //}



        if (SessionParticipante != null)
        {
            pedidos_esq.Visible = true;

            Identificador.Text = SessionParticipante.CdParticipante;
            cdPart.Value = SessionParticipante.CdParticipante;
            NoParticipante.Text = SessionParticipante.NoParticipante;
            nomeCategoria.Text = SessionParticipante.Categoria.NoCategoria;

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

                return;
            }

            Session["SessionEvento"] = SessionEvento;

            Session["SessionParticipante"] = SessionParticipante;

            if (!SessionParticipante.FlValidarEmail)
            {
                Geral oGeral = new Geral();
                oGeral.ValidarEmailParticipante(SessionParticipante, SessionCnn);
            }




            if (tpRotina.ToUpper() == "2VBOLETO")
            {
                if ((Geral.datahoraServidor(SessionCnn) > SessionEvento.DtLimitePagamentoBoleto))
                {
                    lblMsg.Text = "Não é mais possível emitir 2ª via de boleto!";
                    return;
                }

                PedidoCad oPedidoCad = new PedidoCad();
                Pedido tempPedido = oPedidoCad.VerificaPedidoNaoPagoWeb(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);
                if ((tempPedido == null) || (tempPedido.TpPagamento == ""))
                {
                    lblMsg.Text = "PedidoNão não encontrado!<br />Volte ao site e conclua seu processo de inscrição.";
                    return;
                }

                if (tempPedido.QtdParcelas > 1)
                    Response.Write("<script>window.open('frmListaBoletosPedido.aspx','_self');</script>");
                else
                {
                    MeuBoletoCad oMeuBoletoCad = new MeuBoletoCad();
                    MeuBoleto oMeuBoleto = oMeuBoletoCad.PequisarBoletoDoPedido(SessionEvento.CdEvento, tempPedido.CdPedido, "001", SessionCnn);
                    if (oMeuBoleto != null)
                    {
                        if (oMeuBoleto.DsLinkBoletoExterno == "")
                            Response.Write("<script>window.open('frmBoleto.aspx?e=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + "&b=" + cllEventos.Crypto.EncryptStringAES(oMeuBoleto.CdBoleto.Substring(6, 6)) + "&tpRotina=2vBoleto','_self');</script>");
                        else
                            Response.Write("<script>window.open('" + oMeuBoleto.DsLinkBoletoExterno + "','_blank');</script>");
                    }
                }
            }

            if (tpRotina.ToUpper() == "INVOICE")
            {


                PedidoCad oPedidoCad = new PedidoCad();
                Pedido tempPedido = oPedidoCad.SelUltimoPedido(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);
                if ((tempPedido == null) || ((tempPedido.TpPagamento != "BANK TRANSFER") && ((SessionEvento.CdEvento == "007002") && (tempPedido.TpPagamento != "PAYPAL"))))
                {
                    lblMsg.Text = "No valid registration request was found for the field informed! <br /> Go back to the site and complete your registration process.";
                    return;
                }

                Button2.Visible = true;

                return;
            }

            if (tpRotina.ToUpper() == "DADOSCONF")
            {
                if (!SessionParticipante.FlConfirmacaoInscricao)
                {
                    lblMsg.Text = "Inscrição do participante ainda não confirmada!";
                    return;
                }

                Response.Write("<script>window.open('frmInscricaoConfirmada.aspx','_self');</script>");

            }

            if (tpRotina == "ENVDOC")
            {

                Response.Write("<script>window.open('frmEnviarDocumento2.aspx','_self');</script>");
                return;

            }


            if (tpRotina.ToUpper() == "EMTCERT")
            {
                lblMsg.Text = "";

                if (!SessionEvento.FlLiberarCertificacaoWeb)
                {
                    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                        "039",
                        ""), true);
                }


                if (!SessionParticipante.Categoria.FlQuestionario)
                {
                    if (!SessionParticipante.Categoria.FlCertificado)
                    {//não possui direito à certificado
                        lblMsg.Text = "Categoria não possui direito certificação";
                        return;
                    }

                    if (SessionParticipante.CdCredencial == "0")
                    {
                        lblMsg.Text = "Participante não credenciado!";
                        return;
                    }


                    if (SessionEvento.CdEvento == "006202")
                    {
                        int totalEntradas = ContarEntradas(SessionEvento.CdEvento, SessionParticipante.CdParticipante);
                        if (totalEntradas < 0)
                            return;
                        else if (totalEntradas < 3)
                        {
                            lblMsg.Text = "Participante não atingiu 75% de presença para certificação.";
                            return;
                        }
                    }


                    Button2.Visible = true;

                    //Server.Transfer(string.Format("frmCertificado.aspx?p={0}",
                    //                   cllEventos.Crypto.EncryptStringAES(SessionParticipante.CdParticipante)), true);

                    return;
                }

                PesquisasDeOpiniao oPesquisasDeOpiniao = new PesquisasDeOpiniao();
                DataTable dtPesquisas2 = oPesquisasDeOpiniao.ListarPesquisasDoParticipante(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionParticipante.DsIdioma, SessionCnn);
                if ((dtPesquisas2 == null) || (dtPesquisas2.Rows.Count <= 0))
                {

                    if (!SessionParticipante.Categoria.FlCertificado)
                    {//não possui direito à certificado
                        lblMsg.Text = "Categoria não possui direito certificação";
                        return;
                    }

                    if (SessionParticipante.CdCredencial == "0")
                    {
                        lblMsg.Text = "Participante não credenciado!";
                        return;
                    }


                    if (SessionEvento.CdEvento == "006202")
                    {
                        int totalEntradas = ContarEntradas(SessionEvento.CdEvento, SessionParticipante.CdParticipante);
                        if (totalEntradas < 0)
                            return;
                        else if (totalEntradas < 3)
                        {
                            lblMsg.Text = "Participante não atingiu 75% de presença para certificação.";
                            return;
                        }
                    }

                    Button2.Visible = true;

                    //Server.Transfer(string.Format("frmCertificado.aspx?p={0}",
                    //                   cllEventos.Crypto.EncryptStringAES(SessionParticipante.CdParticipante)), true);

                    return;
                }

                Session["SessionTipoSistema"] = "PSQVINC";
                Session["SessionParticipante"] = SessionParticipante;

                Response.Write("<script>window.open('frmPesquisaVinculada.aspx','_self');</script>");
            }

            if (tpRotina == "credEmt")
            {
                lblMsg.Text = "";

                if (SessionParticipante.CdCategoria == "00130501")
                {
                    if (SessionParticipante.FlConfirmacaoInscricao)
                    {
                        if (SessionParticipante.NoAreaAtuacao == "SIM")
                        {
                            if ((SessionParticipante.DsAuxiliar19 == "SIM") && (SessionParticipante.FlDocumentoConfirmado))
                            {
                                if (oParticipanteCad.GerarCredencial(SessionParticipante, SessionCnn))
                                {
                                    SessionParticipante = oParticipanteCad.Pesquisar(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);
                                    Session["SessionParticipante"] = SessionParticipante;

                                    lblMsg.Visible = true;
                                    lblMsg.Text = "Já foi gerada uma credencial para esta inscrição. Caso opte por gerar uma nova, lembre-se que a credencial anterior será invalidada no sistema.";

                                    Server.Transfer("rptEtiqueta.aspx", true);
                                }
                                else
                                {
                                    lblMsg.Visible = true;
                                    lblMsg.Text = oParticipanteCad.RcMsg;
                                }
                            }
                            else
                            {
                                lblMsg.Text =
                                    "Atenção alguns dos motivos abaixo não permitem que sua credencial seja liberada:<br />" +
                                    "1 - O documento que comprova sua condição de secretário municipal de saúde ou detentor de cargo ou função equivalente não foi enviado e/ou não avaliado <br />" +
                                    "2 - Declaração do secretário municipal de saúde ainda não assinalada";
                                return;
                            }
                        }
                        else
                        {
                            if (oParticipanteCad.GerarCredencial(SessionParticipante, SessionCnn))
                            {
                                SessionParticipante = oParticipanteCad.Pesquisar(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);
                                Session["SessionParticipante"] = SessionParticipante;

                                lblMsg.Visible = true;
                                lblMsg.Text = "Já foi gerada uma credencial para esta inscrição. Caso opte por gerar uma nova, lembre-se que a credencial anterior será invalidada no sistema.";

                                Server.Transfer("rptEtiqueta.aspx", true);
                            }
                            else
                            {
                                lblMsg.Visible = true;
                                lblMsg.Text = oParticipanteCad.RcMsg;
                            }
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Recebimento da inscrição ainda não verificado";
                        return;
                    }
                }
                else
                {
                    if ((!SessionParticipante.Categoria.FlAtividades) || (SessionParticipante.FlConfirmacaoInscricao))
                    {
                        if (oParticipanteCad.GerarCredencial(SessionParticipante, SessionCnn))
                        {
                            SessionParticipante = oParticipanteCad.Pesquisar(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);
                            Session["SessionParticipante"] = SessionParticipante;

                            lblMsg.Visible = true;
                            lblMsg.Text = "Já foi gerada uma credencial para esta inscrição. Caso opte por gerar uma nova, lembre-se que a credencial anterior será invalidada no sistema.";

                            Server.Transfer("rptEtiqueta.aspx", true);
                        }
                        else
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = oParticipanteCad.RcMsg;
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Processo de Inscrição incompleto!<br />Volte ao site e conclua seu processo de inscrição.";
                        return;
                    }
                }
            }

            if (tpRotina == "ABRASEL")
                Response.Write("<script>window.open('AtividadesAbrasel.aspx','_self');</script>");

            if (SessionEvento.CdEvento == "005102")
            {
                if (!SessionParticipante.Categoria.FlAtividades)
                {
                    lblMsg.Text = "Categoria do participante não está autorizada a selecionar Grupo de Trabalho!";
                    return;
                }

                Inscricoes inscr = new Inscricoes();
                DataTable dttmp = inscr.ListarAtividadesDoParticipante(SessionParticipante, SessionCnn);
                if ((dttmp == null) || (dttmp.Rows.Count <= 0))
                    Response.Write("<script>window.open('frmSelAtividades.aspx','_self');</script>");
                else
                    lblMsg.Text = "Participante já selecionou seu Grupo de Trabalho!";
            }

            if (SessionEvento.CdEvento == "006602")
            {
                if (SessionParticipante.DsAuxiliar4 == "SIM")
                {
                    Response.Write("<script>window.open('frmQRCode.aspx','_self');</script>");
                }
                else
                {
                    Response.Write("<script>window.open('frmCadastroAuto.aspx','_self');</script>");
                }
            }
        }
        else
        {
            if (SessionIdioma == "PTBR")
                lblMsg.Text = "Participante não cadastrado";
            else if (SessionIdioma == "ENUS")
                lblMsg.Text = "Participant is not registered";
            else if (SessionIdioma == "ESP")
                lblMsg.Text = "Participante no está registrado";
            else if (SessionIdioma == "FRA")
                lblMsg.Text = "Participant n'est pas inscrit";
        }
        /*}
        else
        {
            SessionParticipante = oParticipanteCad.Pesquisar(SessionEvento.CdEvento, txtCPF.Text.Replace(".", "").Replace("-", "").Trim(),  SessionCnn);
            //}
            //else
            //{
            //    SessionParticipante = oParticipanteCad.autenticarWeb(SessionEvento.CdEvento, "EMAIL", txtEmail.Text.ToLower().Trim(), txtSenha.Text, SessionCnn);
            //}



            if (SessionParticipante != null)
            {

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

                    return;
                }

                Session["SessionEvento"] = SessionEvento;

                Session["SessionParticipante"] = SessionParticipante;

                if (!SessionParticipante.FlValidarEmail)
                {
                    Geral oGeral = new Geral();
                    oGeral.ValidarEmailParticipante(SessionParticipante, SessionCnn);
                }


                Response.Write("<script>window.open('frmListaBoletosPedido.aspx','_self');</script>");

            }
            else
            {
                if (SessionIdioma == "PTBR")
                    lblMsg.Text = "Participante não cadastrado";
                else if (SessionIdioma == "ENUS")
                    lblMsg.Text = "Participant is not registered";
                else if (SessionIdioma == "ESP")
                    lblMsg.Text = "Participante no está registrado";
                else if (SessionIdioma == "FRA")
                    lblMsg.Text = "Participant n'est pas inscrit";
            }
        }*/

        //}
        //else
        //    lblMsg.Text = "Caracteres inválidos!";
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
            lblEmail.Text = "Informe o e-mail:";
            Button1.Text = "Verificar";

            lblID.Text = "Nr Cadastro:";
            lblPart.Text = "Participante:";
            lblCateg.Text = "Categoria:";

            CustomValidator2.ErrorMessage = "Campo obrigatório.";
            RFVCPF.ErrorMessage = "Campo obrigatório.";
            revTxtEmail.ErrorMessage = "E-mail inválido";
            rfvTxtEmail.ErrorMessage = "Campo requerido";

            txtCPF.Attributes.Add("placeholder", "Informe o CPF cadastrado");
            txtEmail_verif_cpf.Attributes.Add("placeholder", "Informe o e-mail cadastrado");


            Button2.Text="Fazer Download";
        }
        else if (prmIdioma == "ENUS")//(txtidioma.SelectedIndex == 1)
        {
            SessionIdioma = "ENUS";
            Session["SessionIdioma"] = SessionIdioma;

            //lblCPF.Text = "Informe o CPF:";
            lblEmail.Text = "Enter your email:";
            Button1.Text = "Check";

            lblID.Text = "Nr Register:";
            lblPart.Text = "Participant:";
            lblCateg.Text = "Category:";

            CustomValidator2.ErrorMessage = "Required field.";
            RFVCPF.ErrorMessage = "Required field.";
            revTxtEmail.ErrorMessage = "Invalid email";
            rfvTxtEmail.ErrorMessage = "Required field.";

            txtEmail_verif_cpf.Attributes.Add("placeholder", "Enter the registered e-mail");

            Button2.Text = "Download";
        }
        else if (prmIdioma == "ESP")//(txtidioma.SelectedIndex == 2)
        {
            SessionIdioma = "ESP";
            Session["SessionIdioma"] = SessionIdioma;

            //lblCPF.Text = "Informe o CPF:";
            lblEmail.Text = "Introduce tu email:";
            Button1.Text = "Verificar";

            lblID.Text = "Nº Registro:";
            lblPart.Text = "Nome:";
            lblCateg.Text = "Categoría:";

            CustomValidator2.ErrorMessage = "Campo obligatorio.";
            RFVCPF.ErrorMessage = "Campo obligatorio.";
            revTxtEmail.ErrorMessage = "E-mail no válido";
            rfvTxtEmail.ErrorMessage = "Campo obligatorio.";

            txtEmail_verif_cpf.Attributes.Add("placeholder", "Ingrese el email registrado");

            Button2.Text = "Descargar";
        }
        else if (prmIdioma == "FRA")//(txtidioma.SelectedIndex == 3)
        {
            SessionIdioma = "FRA";
            Session["SessionIdioma"] = SessionIdioma;

            
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {

    }
}