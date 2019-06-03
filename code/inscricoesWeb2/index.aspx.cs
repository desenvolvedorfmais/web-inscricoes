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

using System.Security.Cryptography;
using System.Text;

using System.Data.SqlClient;
using System.Drawing;
using MSXML2;

using System.Xml;

public partial class index : System.Web.UI.Page
{
    Participante SessionParticipante;
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();



    SqlConnection SessionCnn;
    SqlConnection SessionCnn2;

    String SessionIdioma = "";
    String SessionCateg = "";
    String SessionAtv = "";

    String SessionTipoSistema = "";

    String SessionTipoAcesso = "";
    String SessionInscrRef = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        HttpContext.Current.Response.AddHeader(
                "p3p", "CP=\"IDC DSP COR ADM DEVi TAIi PSA PSD IVAi IVDi CONi HIS OUR IND CNT\"");
        

        if (!Page.IsPostBack)
        {
            SessionParticipante = new Participante();
            Session["SessionParticipante"] = SessionParticipante;

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            if (SessionCnn == null)
            {
                //local Lenovo
                SessionCnn =
                    new SqlConnection(
                        "Server=(local);Database=dbEventos_FM;Trusted_Connection=True;ConnectRetryCount=0");
                //local 1
                //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOUxqdEpibWwwYVdGc0lFTmhkR0ZzYjJjOVpHSkZkbVZ1ZEc5elgwWk5URzlqWVd3N1VHVnljMmx6ZENCVFpXTjFjbWwwZVNCSmJtWnZQVlJ5ZFdVN1ZYTmxjaUJKUkQxellUdFFZWE56ZDI5eVpEMUxjbXR6WVRFM01RPT0=")));

                //local 2 note novo
                //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOUxseHpjV3d5TURFeVpYaHdjbVZ6Y3p0SmJtbDBhV0ZzSUVOaGRHRnNiMmM5WkdKRmRtVnVkRzl6WDBaTk8xQmxjbk5wYzNRZ1UyVmpkWEpwZEhrZ1NXNW1iejFVY25WbE8xVnpaWElnU1VROWMyRTdVR0Z6YzNkdmNtUTlTM0pyYzJFeE56RT0=")));

                //servidores banco local
                SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOUxqdEpibWwwYVdGc0lFTmhkR0ZzYjJjOVpHSkZkbVZ1ZEc5elgwWk5URzlqWVd3N1VHVnljMmx6ZENCVFpXTjFjbWwwZVNCSmJtWnZQVlJ5ZFdVN1ZYTmxjaUJKUkQxellUdFFZWE56ZDI5eVpEMUxjbXR6WVRFM01RPT0=")));

                //MinSaude
                //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOUxseHpjV3hsZUhCeVpYTnpPMGx1YVhScFlXd2dRMkYwWVd4dlp6MWtZa1YyWlc1MGIzTmZSazA3VUdWeWMybHpkQ0JUWldOMWNtbDBlU0JKYm1adlBWUnlkV1U3VlhObGNpQkpSRDF6WVR0UVlYTnpkMjl5WkQxTGNtdHpZVEUzTVE9PQ==")));

                //Site
                //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOU1UZzVMak00TGprMUxqY3lPMGx1YVhScFlXd2dRMkYwWVd4dlp6MW1ZWHBsYm1SdmJXRnBjenRRWlhKemFYTjBJRk5sWTNWeWFYUjVJRWx1Wm04OVZISjFaVHRWYzJWeUlFbEVQV1poZW1WdVpHOXRZV2x6TzFCaGMzTjNiM0prUFZGMU5EVTBja1p0")));

                //Site2-historico
                //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOWJYTnpjV3d1Wm1GNlpXNWtiMjFoYVhNdVkyOXRPMGx1YVhScFlXd2dRMkYwWVd4dlp6MW1ZWHBsYm1SdmJXRnBjekU3VUdWeWMybHpkQ0JUWldOMWNtbDBlU0JKYm1adlBWUnlkV1U3VlhObGNpQkpSRDFtWVhwbGJtUnZiV0ZwY3pFN1VHRnpjM2R2Y21ROVVYVTBOVFJ5Um0wPQ==")));

                //Site-producao
                //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOWJYTnpjV3d1Wm1GNlpXNWtiMjFoYVhNdVkyOXRPMGx1YVhScFlXd2dRMkYwWVd4dlp6MW1ZWHBsYm1SdmJXRnBjenRRWlhKemFYTjBJRk5sWTNWeWFYUjVJRWx1Wm04OVZISjFaVHRWYzJWeUlFbEVQV1poZW1WdVpHOXRZV2x6TzFCaGMzTjNiM0prUFZGMU5EVTBja1p0VFVSQg==")));

                //Site-producao - AZURE
                //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOWFHcDBZWEF3YjNkcWJpNWtZWFJoWW1GelpTNTNhVzVrYjNkekxtNWxkRHRKYm1sMGFXRnNJRU5oZEdGc2IyYzlaR0pmWlhabGJuUnZjMFpOTzFCbGNuTnBjM1FnVTJWamRYSnBkSGtnU1c1bWJ6MVVjblZsTzFWelpYSWdTVVE5UTI5dVlYTmxiWE03VUdGemMzZHZjbVE5UXpCdU5EVmxiVFV5TURFMg==")));


                //Site-producao - IP
                //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOU1UZzVMak00TGprd0xqRXdPVHRKYm1sMGFXRnNJRU5oZEdGc2IyYzlabUY2Wlc1a2IyMWhhWE03VUdWeWMybHpkQ0JUWldOMWNtbDBlU0JKYm1adlBWUnlkV1U3VlhObGNpQkpSRDFtWVhwbGJtUnZiV0ZwY3p0UVlYTnpkMjl5WkQxUmRUUTFOSEpHYlUxRVFRPT0=")));



                //krksa-vaio
                 //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOUxqdEpibWwwYVdGc0lFTmhkR0ZzYjJjOVpHSkZkbVZ1ZEc5elgwWk5PMUJsY25OcGMzUWdVMlZqZFhKcGRIa2dTVzVtYnoxVWNuVmxPMVZ6WlhJZ1NVUTljMkU3VUdGemMzZHZjbVE5UzNKcmMyRXhOekU9")));

                //Servidor Dell - Caonasems
                //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOU1UZzFMakU0TlM0eE5qZ3VNanRKYm1sMGFXRnNJRU5oZEdGc2IyYzlaR0pGZG1WdWRHOXpYMFpOVEc5allXdzdVR1Z5YzJsemRDQlRaV04xY21sMGVTQkpibVp2UFZSeWRXVTdWWE5sY2lCSlJEMXpZVHRRWVhOemQyOXlaRDFMY210ellURTNNUT09")));

                //Servidor core i7
                //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOU1UZzFMakU0TlM0eE5qZ3VNanRKYm1sMGFXRnNJRU5oZEdGc2IyYzlaR0pGZG1WdWRHOXpYMFpOVEc5allXdzdVR1Z5YzJsemRDQlRaV04xY21sMGVTQkpibVp2UFZSeWRXVTdWWE5sY2lCSlJEMXpZVHRRWVhOemQyOXlaRDFMY210ellURTNNUT09")));

                //servidor LG
                //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOU1UZzFMakU0TlM0eE5qZ3VNanRKYm1sMGFXRnNJRU5oZEdGc2IyYzlaR0pGZG1WdWRHOXpYMFpOVEc5allXdzdVR1Z5YzJsemRDQlRaV04xY21sMGVTQkpibVp2UFZSeWRXVTdWWE5sY2lCSlJEMXpZVHRRWVhOemQyOXlaRDFMY210ellURTNNUT09")));

                //Session["SessionCnn"] = SessionCnn;


                //conexao pesquisa local
                //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOU1UZzFMakU0TlM0eE5qZ3VNanRKYm1sMGFXRnNJRU5oZEdGc2IyYzlaR0pGZG1WdWRHOXpYMFpOVEc5allXdzdVR1Z5YzJsemRDQlRaV04xY21sMGVTQkpibVp2UFZSeWRXVTdWWE5sY2lCSlJEMXpZVHRRWVhOemQyOXlaRDFMY210ellURTNNUT09")));


            }

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


            if ((Request.QueryString["cdLng"] != null) &&
                (Request.QueryString["cdLng"].ToString().Trim().ToUpper() != ""))
            {
                SessionIdioma = Request.QueryString["cdLng"];
            }
            else
            {
                SessionIdioma = (String) Session["SessionIdioma"];
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


            if ((Request.QueryString["refInscr"] != null) &&
                (Request.QueryString["refInscr"].ToString().Trim().ToUpper() != ""))
            {
                SessionInscrRef = Request.QueryString["refInscr"];
            }
            else
            {
                SessionInscrRef = (String)Session["SessionInscrRef"];
                if (SessionInscrRef == null)
                    SessionInscrRef = "";
            }
            Session["SessionInscrRef"] = SessionInscrRef;



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


            //SessionEvento = (Evento)Session["SessionEvento"];

            //if (SessionEvento == null)
            SessionEvento = oEventoCad.Pesquisar(cdEvento.Substring(0,4), cdEvento, SessionCnn);

            Response.Cookies["EventoInfo"]["eventoCod"] = cdEvento;
            Response.Cookies["EventoInfo"]["lngCod"] = SessionIdioma;
            Response.Cookies["EventoInfo"]["categCod"] = SessionCateg;
            Response.Cookies["EventoInfo"]["atividadeCod"] = SessionAtv;
            Response.Cookies["EventoInfo"]["tpSistema"] = SessionTipoSistema;
            Response.Cookies["EventoInfo"].Expires = DateTime.Now.AddDays(1);
            
            Session["SessionEvento"] = SessionEvento;
            if (SessionEvento == null)
            {
                Response.Cookies["EventoInfo"]["eventoCod"] = cdEvento;
                Response.Cookies["EventoInfo"]["lngCod"] = SessionIdioma;
                Response.Cookies["EventoInfo"]["categCod"] = SessionCateg;
                Response.Cookies["EventoInfo"]["atividadeCod"] = SessionAtv;
                Response.Cookies["EventoInfo"]["tpSistema"] = SessionTipoSistema;
                Response.Cookies["EventoInfo"].Expires = DateTime.Now.AddDays(1);

                Server.Transfer("frmSessaoExpirada.aspx", true);
                //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                //                "003",
                //                oEventoCad.RcMsg + cdEvento.Substring(0, 4)), true);
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
                if (SessionEvento.CdEvento == "008303")
                {
                    tela_cad_direita.Visible = false;
                }
                else
                if ((!SessionEvento.FlLiberarCertificacaoWeb) && ((SessionTipoSistema == "NRM") || (SessionTipoSistema == "CRD")))
                {
                    //lblChaveLiberacao0.Text = "Inscrições Encerradas!";
                    //lblChaveLiberacao0.ForeColor = System.Drawing.Color.Red;
                    //lblChaveLiberacao0.Visible = true;
                    //btnCadastrar.Visible = false;

                    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                    "005",
                                    oEventoCad.RcMsg + cdEvento.Substring(0, 4)), true);
                }
            }

            if ((SessionEvento.FlEventoComRecebimentos) && (TiposPagamentoCad.VerificarFormaPagamento(SessionEvento.CdEvento, "BOLETO", SessionCnn)) &&
                ((SessionEvento.DtFechamentoInscrWeb == null) ||
                 (SessionEvento.DtFechamentoInscrWeb >= Geral.datahoraServidor(SessionCnn))
                  ) && (SessionEvento.CdCliente != "0016") && (SessionEvento.CdCliente != "0070") && (SessionIdioma == "PTBR"))
            {
                lnk2aViaBoleto.PostBackUrl = "~/frmVerificaCPFCadastro.aspx?codEvento=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento)+"&tpRotina=2vBoleto";
                linkboleto.Visible = true;
                //TiposPagamentoCad oTiposPagamentoCad = new TiposPagamentoCad();
                //DataTable dtPgto = oTiposPagamentoCad.Listar(SessionEvento.CdEvento, SessionCnn);
                //dtPgto.Select()
            }

            if (Geral.datahoraServidor(SessionCnn) >= SessionEvento.DtLimitePagamentoBoleto)
            {
                linkboleto.Visible = false;
            }

            if (SessionEvento.CdEvento == "007701")
            {
                linkboleto.Visible = false;
            }

            if (!SessionEvento.FlAutenticacaoWeb)
            {
                /***** Criar cookies *****/
                Response.Cookies["EventoInfo"]["eventoCod"] = SessionEvento.CdEvento;
                Response.Cookies["EventoInfo"]["lngCod"] = SessionIdioma;
                Response.Cookies["EventoInfo"]["categCod"] = SessionCateg;
                Response.Cookies["EventoInfo"]["atividadeCod"] = SessionAtv;
                Response.Cookies["EventoInfo"]["tpSistema"] = SessionTipoSistema;
                Response.Cookies["EventoInfo"].Expires = DateTime.Now.AddDays(1);

                RFVCPF.Enabled = false;

                if (SessionEvento.CdEvento == "011301")
                {
                    ParticipanteCad oParticipanteCad = new ParticipanteCad();
                    int tmpTotalCad = oParticipanteCad.TotalCadastradoAtivos(SessionEvento.CdEvento, SessionCnn);
                    if ((SessionEvento.NuDiasPrimeiroVencimento > 0) && (SessionEvento.NuDiasPrimeiroVencimento <= tmpTotalCad))
                    {
                        Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                "045",
                                ""), true);
                    }
                }

                if ((Request.QueryString["keyAut"] != null) &&//codigo de autorizacao
                    (Request.QueryString["keyAut"].ToString().Trim().ToUpper() != "") && (!SessionEvento.FlLiberarCertificacaoWeb))
                {
                    txtChaveLiberacao.Text = Request.QueryString["keyAut"].ToString();

                    linhaCadastrar.Visible = false;

                    linhaChaveLiberacao.Visible = true;
                    pnlChaveLiberacao.Visible = true;
                    lblChaveLiberacao0.Visible = true;


                    ParticipanteCad oParticipanteCad = new ParticipanteCad();
                    ParticipantePreCadastro oParticipantePreCadastro = oParticipanteCad.PesquisarPreCadastroPorChave(SessionEvento.CdEvento, txtChaveLiberacao.Text.Trim(), SessionCnn);

                    //if (oParticipantePreCadastro == null)
                    //{
                    //    lblMsg2.Text = oParticipanteCad.RcMsg;
                    //    return;
                    //}

                    //if (oParticipantePreCadastro.FlAtivo == false)
                    //{
                    //    lblMsg2.Text = "Chave de Liberação Inativa";
                    //    return;
                    //}

                    //if ((oParticipantePreCadastro.DtValidade != null) && (oParticipantePreCadastro.DtValidade.Value.Date < Geral.datahoraServidor(SessionCnn).Date))
                    //{
                    //    lblMsg2.Text = "Chave de Liberação Vencida";
                    //    return;
                    //}

                    //int totPrecadastradoChave = oParticipanteCad.TotalPreCadastradoChave(SessionEvento.CdEvento, txtChaveLiberacao.Text, SessionCnn);

                    //if ((oParticipantePreCadastro.QtdLimiteInscricaoChave > 0) && (totPrecadastradoChave >= oParticipantePreCadastro.QtdLimiteInscricaoChave)) //int.Parse(DTPre.DefaultView[0]["qtdLimiteInscricaoChave"].ToString()))
                    //{
                    //    lblMsg2.Text = "Limite de utilização para esta chave foi atingido!";//<br/>Não é possível efetuar inscrição.";
                    //    if (SessionEvento.CdEvento == "001304")
                    //    {
                    //        ClsFuncoes oclsfuncoes = new ClsFuncoes();
                    //        if (oParticipantePreCadastro.NoParticipantePrecadastro != "")
                    //            lblMsg2.Text +=
                    //                "<br/>Favor envie e-mail para: " + oParticipantePreCadastro.DsEmail +
                    //                " ou ligue para: " + oclsfuncoes.MascaraGerar(oParticipantePreCadastro.DsCelular, "(99) 9999-9999");
                    //        ;
                    //    }
                    //    return;
                    //}
                                       

                    Session["SessionChaveLibercao"] = oParticipantePreCadastro.DsCampoExtra;
                }

                if (((SessionIdioma == "PTBR") && (SessionEvento.DsInformacoesCompletasWeb_ptbr.Trim() != "")) ||
                    ((SessionIdioma == "ENUS") && (SessionEvento.DsInformacoesCompletasWeb_enus.Trim() != "")) ||
                    ((SessionIdioma == "ESP") && (SessionEvento.DsInformacoesCompletasWeb_esp.Trim() != "")) ||
                    ((SessionIdioma == "FRA") && (SessionEvento.DsInformacoesCompletasWeb_fra.Trim() != "")))
                    Response.Write("<script>window.open('frmInformacoes.aspx','_self');</script>");
                else if ((SessionEvento.FlEventoApenasPesquisa) || (SessionTipoSistema == "PSQAVS"))
                    Response.Write("<script>window.open('frmPesquisaAvulsaAbertura.aspx','_self');</script>");
                else if (SessionTipoSistema == "PSQVINC")
                    Response.Write("<script>window.open('frmPesquisaVinculadaAbertura.aspx','_self');</script>");
                else if (SessionTipoSistema == "VRFCAD")
                    Response.Write("<script>window.open('frmSituacaoCadastroParticipante.aspx','_self');</script>");
                else if (SessionEvento.FlPesquisaCPFReceita)
                    Response.Write("<script>window.open('frmBuscaCPFReceita.aspx','_self');</script>");
                else
                {
                    //if (SessionEvento.CdEvento == "000104")
                        
                    //else
                    if ((SessionEvento.CdEvento != "004501") && (SessionEvento.CdEvento != "008701"))
                    {

                        if ((SessionIdioma == "PTBR") && (Geral.VerificarEventoIntenacional(SessionEvento.CdEvento, SessionCnn)))
                        {
                            Response.Write("<script>window.open('frmVerificarBrasileiro.aspx','_self');</script>");
                        }
                        else
                        {
                            Session["SessionEvento"] = SessionEvento;
                            Session["SessionPais"] = "";
                            if (SessionIdioma == "PTBR")
                                Session["SessionPais"] = "BRASIL";
                            Response.Write("<script>window.open('frmCadastroAuto.aspx','_self');</script>");
                        }

                        //Response.Write("<script>window.open('frmCadastroAuto.aspx','_self');</script>");
                    }
                    else if (SessionEvento.CdEvento == "004501")
                        Response.Write("<script>window.open('frmFiltroPrecadastro.aspx','_self');</script>");
                    else if (SessionCateg == "00870101")
                        Response.Write("<script>window.open('frmBuscaCPF.aspx','_self');</script>");
                    else
                        Response.Write("<script>window.open('frmCadastroAuto.aspx','_self');</script>");
                }
                
            }
            else
            {
                if (SessionTipoSistema == "NRM")
                {
                    if ((SessionEvento.DsCampoAutenticacao.ToUpper().Replace("-", "") == "EMAIL") || (SessionIdioma != "PTBR"))
                    {
                        lblEmail.Visible = txtEmail_login.Visible = revTxtEmail.Visible = rfvTxtEmail.Visible = true;
                        LBLCONTA.Visible = TXTDsCPF.Visible = RFVCPF.Visible = false;
                        //txtEmail.Focus();

                        if (txtChaveLiberacao.Visible)
                            txtChaveLiberacao.Focus();
                        else
                            btnCadastrar.Focus();
                    }
                    else
                        if (txtChaveLiberacao.Visible)
                            txtChaveLiberacao.Focus();
                        else
                            btnCadastrar.Focus();
                        //TXTDsCPF.Focus();
                }
                else if (SessionTipoSistema == "CRD")
                {
                    Response.Write("<script>window.open('frmCadastroAuto.aspx','_self');</script>");
                }
                else if (SessionTipoSistema == "PSQVINC")
                {
                    Response.Write("<script>window.open('frmPesquisaVinculadaAbertura.aspx','_self');</script>");
                }
                else if (SessionTipoSistema == "PSQAVS")
                {
                    Response.Write("<script>window.open('frmPesquisaAvulsaAbertura.aspx','_self');</script>");
                }
                else if (SessionTipoSistema == "VRFCAD")
                {
                    Response.Write("<script>window.open('frmSituacaoCadastroParticipante.aspx','_self');</script>");
                }
            }

            //pnlChaveLiberacao.Visible = false;
            //lblChaveLiberacao0.Visible = false;

            if ((SessionEvento.CdEvento == "001303") || (SessionEvento.CdEvento == "001304") || (SessionEvento.CdEvento == "001305") ||
                (SessionEvento.CdEvento == "004801"))
            {
                linhaChaveLiberacao.Visible = true;
                pnlChaveLiberacao.Visible = true;
                lblChaveLiberacao0.Visible = true;
                linhaCadastrar.Visible = false;

                
            }

            

            if ((cdEvento == "000801") || (cdEvento == "000802"))
            {
                btnCadastrar.Visible = false;
                btnCadastrar2.Visible = true;
            }

            if (cdEvento == "002501")
            {
                btnCadastrar.Text = "Cadastro para comprar ingresso";
                
            }

            //if ((cdEvento != "000303") && (cdEvento != "002902"))
            //    lblInstrucoesRapidas.Text = SessionEvento.DsInformacoesPreviasWeb;
            //else
            //{
            //    telacad_direita.Visible = false;
            //}

            //if ((SessionEvento.CdCliente == "0003") && (!SessionEvento.FlLiberarCertificacaoWeb))
           // hlnk_Documentos.Visible = false;
            //int totDocs = oEventoCad.oDocumentoCad.TotalAtivos(SessionEvento.CdEvento, SessionCnn);
            if ((SessionEvento.documentos != null) && (SessionEvento.documentos.Rows.Count > 0) && (!SessionEvento.FlLiberarCertificacaoWeb))
            {
             //   hlnk_Documentos.Visible = true;
                linhahlnk_Documentos.Visible = true;
            }
            

            if ((SessionEvento.DtFechamentoInscrWeb == null) ||
                (SessionEvento.DtFechamentoInscrWeb < Geral.datahoraServidor(SessionCnn)))//DateTime.Parse(DateTime.Today.ToString("dd/MM/yyyy"))))
            {
                if (SessionEvento.FlLiberarCertificacaoWeb)
                {                    
                    lblTiuloTelaDir.Visible = false;
                    btnCadastrar.Visible = false;
                    pnlChaveLiberacao.Visible = false;

                    txtSenha.Visible = false;
                    LBLSENHA.Visible = false;
                    btnLembrarSenha.Visible = false;
                }
            }
            else
            {
                lblTiuloTelaDir.Visible = true;
                btnCadastrar.Visible = true;
                lblInstrucoesRapidas.Text = SessionEvento.DsInformacoesPreviasWeb;

                if (SessionIdioma == "PTBR")
                    lblInstrucoesRapidas.Text = SessionEvento.DsInformacoesPreviasWeb_ptbr;
                else if (SessionIdioma == "ENUS")
                    lblInstrucoesRapidas.Text = SessionEvento.DsInformacoesPreviasWeb_enus;
                else if (SessionIdioma == "ESP")
                    lblInstrucoesRapidas.Text = SessionEvento.DsInformacoesPreviasWeb_esp;
                else if (SessionIdioma == "FRA")
                    lblInstrucoesRapidas.Text = SessionEvento.DsInformacoesPreviasWeb_fra;
            }


            //instrucoesRapidas();

            if ((Request.QueryString["keyAut"] != null) &&//codigo da categoria
                (Request.QueryString["keyAut"].ToString().Trim().ToUpper() != "") && (!SessionEvento.FlLiberarCertificacaoWeb))
            {
                txtChaveLiberacao.Text = Request.QueryString["keyAut"].ToString();

                linhaCadastrar.Visible = false;

                linhaChaveLiberacao.Visible = true;
                pnlChaveLiberacao.Visible = true;
                lblChaveLiberacao0.Visible = true;
            }



            /***** Criar cookies *****/
            Response.Cookies["EventoInfo"]["eventoCod"] = SessionEvento.CdEvento;
            Response.Cookies["EventoInfo"]["lngCod"] = SessionIdioma;
            Response.Cookies["EventoInfo"]["categCod"] = SessionCateg;
            Response.Cookies["EventoInfo"]["atividadeCod"] = SessionAtv;
            Response.Cookies["EventoInfo"]["tpSistema"] = SessionTipoSistema;
            Response.Cookies["EventoInfo"].Expires = DateTime.Now.AddDays(1);

            
            if (SessionTipoAcesso == "NOVA")
            {
                
                if ((SessionIdioma == "PTBR") && (Geral.VerificarEventoIntenacional(SessionEvento.CdEvento, SessionCnn)))
                {
                    Response.Write("<script>window.open('frmVerificarBrasileiro.aspx','_self');</script>");
                }

                Session["SessionPais"] = "";
                if (SessionIdioma == "PTBR")
                    Session["SessionPais"] = "BRASIL";

                if (SessionEvento.FlPesquisaCPFReceita)
                {
                    Session["SessionPais"] = "";
                    Response.Write("<script>window.open('frmBuscaCPFReceita.aspx','_self');</script>");
                }
                else
                {
                    Session["SessionPais"] = "";

                    if (((SessionIdioma == "PTBR") && (SessionEvento.DsInformacoesCompletasWeb_ptbr.Trim() != "")) ||
                        ((SessionIdioma == "ENUS") && (SessionEvento.DsInformacoesCompletasWeb_enus.Trim() != "")) ||
                        ((SessionIdioma == "ESP") && (SessionEvento.DsInformacoesCompletasWeb_esp.Trim() != "")) ||
                        ((SessionIdioma == "FRA") && (SessionEvento.DsInformacoesCompletasWeb_fra.Trim() != "")))
                                    Response.Write("<script>window.open('frmInformacoes.aspx','_self');</script>");
                    else
                    Response.Write("<script>window.open('frmCadastroAuto.aspx','_self');</script>");
                }
            }



            if (txtChaveLiberacao.Text.Trim() != "")
            {
                ParticipanteCad oParticipanteCad = new ParticipanteCad();
                ParticipantePreCadastro oParticipantePreCadastro = oParticipanteCad.PesquisarPreCadastroPorChave(SessionEvento.CdEvento, txtChaveLiberacao.Text.Trim(), SessionCnn);

                if (oParticipantePreCadastro == null)
                {
                    lblMsg2.Text = oParticipanteCad.RcMsg;
                    return;
                }

                if (oParticipantePreCadastro.FlAtivo == false)
                {
                    lblMsg2.Text = "Chave de Liberação Inativa";
                    return;
                }

                if ((oParticipantePreCadastro.DtValidade != null) && (oParticipantePreCadastro.DtValidade.Value.Date < Geral.datahoraServidor(SessionCnn).Date))
                {
                    lblMsg2.Text = "Chave de Liberação Vencida";
                    return;
                }

                int totPrecadastradoChave = oParticipanteCad.TotalPreCadastradoChave(SessionEvento.CdEvento, txtChaveLiberacao.Text, SessionCnn);

                if ((oParticipantePreCadastro.QtdLimiteInscricaoChave > 0) && (totPrecadastradoChave >= oParticipantePreCadastro.QtdLimiteInscricaoChave)) //int.Parse(DTPre.DefaultView[0]["qtdLimiteInscricaoChave"].ToString()))
                {
                    lblMsg2.Text = "Limite de utilização para esta chave foi atingido!";//<br/>Não é possível efetuar inscrição.";
                    if (SessionEvento.CdEvento == "001304")
                    {
                        ClsFuncoes oclsfuncoes = new ClsFuncoes();
                        if (oParticipantePreCadastro.NoParticipantePrecadastro != "")
                            lblMsg2.Text +=
                                "<br/>Favor envie e-mail para: " + oParticipantePreCadastro.DsEmail +
                                " ou ligue para: " + oclsfuncoes.MascaraGerar(oParticipantePreCadastro.DsCelular,"(99) 9999-9999");
                        ;
                    }
                    return;
                }

                Session["SessionEvento"] = SessionEvento;

                SessionCateg = oParticipantePreCadastro.CdCategoria;
                Session["SessionCateg"] = SessionCateg;

                Session["SessionChaveLibercao"] = oParticipantePreCadastro.DsCampoExtra;

                Session["SessionPais"] = "";
                if (SessionIdioma == "PTBR")
                    Session["SessionPais"] = "BRASIL";


                if (SessionEvento.FlPesquisaCPFReceita)
                    Response.Write("<script>window.open('frmBuscaCPFReceita.aspx','_self');</script>");
                else
                    Response.Write("<script>window.open('frmCadastroAuto.aspx','_self');</script>");

                
            }

            if (txtChaveLiberacao.Visible)
                txtChaveLiberacao.Focus();
            else
                btnCadastrar.Focus();


            if (SessionEvento.CdEvento == "010101")
            {
                ParticipanteCad oParticipanteCad = new ParticipanteCad();
                int tmpTotalCad = oParticipanteCad.TotalCadastradoAtivos(SessionEvento.CdEvento, SessionCnn);
                if ((SessionEvento.NuDiasPrimeiroVencimento > 0) && (SessionEvento.NuDiasPrimeiroVencimento <= tmpTotalCad))
                {
                    btnCadastrar.Visible = false;
                    lblChaveLiberacao0.Text = "Limite de vagas atingido!";
                    lblChaveLiberacao0.ForeColor = Color.Red;
                    lblChaveLiberacao0.Visible = true;
                }
            }
        }
        else
        {
           
                SessionParticipante = (Participante)Session["SessionParticipante"];

                SessionEvento = (Evento)Session["SessionEvento"];

                SessionCnn = (SqlConnection)Session["SessionCnn"];

                SessionIdioma = (String)Session["SessionIdioma"];

                SessionCateg = (String)Session["SessionCateg"];

                SessionAtv = (String)Session["SessionAtv"];

                SessionTipoSistema = (String)Session["SessionTipoSistema"];

                SessionTipoAcesso = (String)Session["tpAcesso"];

                SessionInscrRef = (String)Session["SessionInscrRef"];

                if (SessionEvento == null)
                    Server.Transfer("frmSessaoExpirada.aspx", true);
            
        }
        
        verificarIdioma();

        ToolkitScriptManager1.RegisterPostBackControl(btn_Login);
        ToolkitScriptManager1.RegisterPostBackControl(btnCadastrar);
        ToolkitScriptManager1.RegisterPostBackControl(btnCadastrar2);
        ToolkitScriptManager1.RegisterPostBackControl(btnCadastrar3);

        if (SessionEvento.CdEvento == "000306")
        {
            lblChaveLiberacao0.Text = "<br /><p><span style=\"color: #ff0000;\">Convidados da Escola de Governo!!!<br />Inscreva-se apenas pelo link enviado ao seu e-mail</span></p><br />";
            lblChaveLiberacao0.Visible = true;
        }
    }


    protected void verificarIdioma()
    {
        //DropDownList txtidioma = (DropDownList)Page.Master.FindControl("txtIdioma");
        //if (txtidioma != null)
        //{
     
        if ((SessionIdioma == null) || (SessionIdioma == ""))
        {
            SessionIdioma = "PTBR";
        }
        Session["SessionIdioma"] = SessionIdioma;

        if (SessionIdioma == "PTBR")//(txtidioma.SelectedIndex == 0)
        {
            SessionIdioma = "PTBR";
            Session["SessionIdioma"] = SessionIdioma;


            lblTituloTelaEsq.Text = "Já sou cadastrado";
            if (SessionEvento.CdCliente == "0013")
                lblTituloTelaEsq.Text = "Já Fiz Minha Inscrição";


            if (SessionEvento.CdEvento == "005503")
                lblTituloTelaEsq.Text = "Já estou inscrito no evento";

            btn_Login.Text = "Entrar";
            if (((SessionEvento.DtFechamentoInscrWeb == null) || (SessionEvento.DtFechamentoInscrWeb < Geral.datahoraServidor(SessionCnn)))//DateTime.Parse(DateTime.Today.ToString("dd/MM/yyyy"))))
                  && ((SessionEvento.FlLiberarCertificacaoWeb)))
            {
                lblTituloTelaEsq.Text = "Emitir Certificado";
                btn_Login.Text = "Emitir Certificado";
            }

            lblTiuloTelaDir.Text = "Inscreva-se";// "Ainda não sou cadastrado";

            TXTDsCPF.Attributes.Add("placeholder", "Informe o CPF cadastrado");
            txtEmail_login.Attributes.Add("placeholder", "Informe o e-mail cadastrado");
            txtSenha.Attributes.Add("placeholder", "Informe a senha cadastrada");

            RFVCPF.Text = "Campo requerido";
            revTxtEmail.ErrorMessage = "E-mail inválido";
            rfvTxtEmail.ErrorMessage = "Campo requerido";
            LBLSENHA.Text = "Senha";
            REVTxtSenha.Text = "Senha inválida";
            btnLembrarSenha.Text = "Esqueci minha senha";

            btnCadastrar.Text = "Quero me Inscrever";

            btnCadastrar2.Text = "Quero me Inscrever";

            if (SessionEvento.CdEvento == "001007")
            {
                lblTiuloTelaDir.Text = "Compre seu Ingresso";
                btnCadastrar.Text = "Comprar Ingresso";
            }

            if (SessionEvento.CdEvento == "011201")
            {
                lblTiuloTelaDir.Text = "Cadastre-se e Compre seu Ingresso";
                btnCadastrar.Text = "Quero me Cadastrar";
            }

            lblMsgCadIndex.Text = "Faça o seu cadastro para inscrever-se no " + SessionEvento.NoEvento;
            lblMsgJaCad.Text = "Gerencie as suas inscrições em todas as atividades do " + SessionEvento.NoEvento;
        }
        else if (SessionIdioma == "ENUS")//(txtidioma.SelectedIndex == 1)
        {
            SessionIdioma = "ENUS";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloTelaEsq.Text = "I have registered";
            lblTiuloTelaDir.Text = "Register";
            RFVCPF.Text = "Required field";
            revTxtEmail.ErrorMessage = "E-mail is invalid";
            rfvTxtEmail.ErrorMessage = "Required field";
            LBLSENHA.Text = "Password";
            REVTxtSenha.Text = "Invalid password";
            btnCadastrar.Text = "I want to register";
            btn_Login.Text = "Log in";
            btnLembrarSenha.Text = "Forgot my password";
            btnCadastrar2.Text = "I want to register";

            txtEmail_login.Attributes.Add("placeholder", "Enter the registered e-mail");
            txtSenha.Attributes.Add("placeholder", "Enter the registered password");

            lblMsgCadIndex.Text = "Make your registration to sign up for I International Conference on Neuroscience and Rehabilitation";
            lblMsgJaCad.Text = "Manage your enrollments in all activities of the I International Conference on Neuroscience and Rehabilitation";
        }
        else if (SessionIdioma == "ESP")//(txtidioma.SelectedIndex == 2)
        {
            SessionIdioma = "ESP";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloTelaEsq.Text = "Estoy registrado";
            lblTiuloTelaDir.Text = "Inscríbase";
            RFVCPF.Text = "Campo obligatorio";
            revTxtEmail.ErrorMessage = "E-mail no es válido";
            rfvTxtEmail.ErrorMessage = "Campo obligatorio";
            LBLSENHA.Text = "Contraseña";
            REVTxtSenha.Text = "Contraseña no válida";
            btnCadastrar.Text = "Quiero inscribirme";
            btn_Login.Text = "Entrar";
            btnLembrarSenha.Text = "Olvidé mi contraseña";
            btnCadastrar2.Text = "Quiero inscribirme";

            txtEmail_login.Attributes.Add("placeholder", "Ingrese el email registrado");
            txtSenha.Attributes.Add("placeholder", "Introduzca la contraseña registrada");
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

            txtEmail_login.Attributes.Add("placeholder", "Entrez votre adresse email");
            txtSenha.Attributes.Add("placeholder", "Entrez le mot de passe enregistré");
        }
            
        //}
    }

    protected void btnLogin_Click1(object sender, EventArgs e)
    {
        ParticipanteCad oParticipanteCad = new ParticipanteCad();

        if (!SessionEvento.FlLiberarCertificacaoWeb) 
        {

            //SessionParticipante = oParticipanteCad.ValidarContaSenha(SessionEvento.CdEvento, TXTDsCPF.Text.Replace(".", "").Replace("-", "").Trim(), txtSenha.Text.Trim(), _cnn);
            if ((SessionEvento.DsCampoAutenticacao.ToUpper().Replace("-", "") == "CPF") && (SessionIdioma == "PTBR"))
            {
                SessionParticipante = oParticipanteCad.autenticarWeb(SessionEvento.CdEvento, "CPF", TXTDsCPF.Text.Replace(".", "").Replace("-", "").Trim(), txtSenha.Text, SessionCnn);
            }
            else
            {
                SessionParticipante = oParticipanteCad.autenticarWeb(SessionEvento.CdEvento, "EMAIL", txtEmail_login.Text.ToLower().Trim(), txtSenha.Text, SessionCnn);
            }
        }
        else
        {
            SessionParticipante = oParticipanteCad.Pesquisar(SessionEvento.CdEvento, "nuCPFCNPJ", TXTDsCPF.Text.Replace(".", "").Replace("-", "").Trim(), SessionCnn);
        }
        

        if (SessionParticipante != null)
        {

            if (!SessionParticipante.FlAtivo)
            {
                if (SessionIdioma == "PTBR")
                    txtMsg.Text = "Cadastro inativo! Favor entrar em contato com a organização do evento.";
                else if (SessionIdioma == "ENUS")
                    txtMsg.Text = "Register idle! Please contact the event organizers.";
                else if (SessionIdioma == "ESP")
                    txtMsg.Text = "Registrarse inactivada! Póngase en contacto con los organizadores del evento.";
                else if (SessionIdioma == "FRA")
                    txtMsg.Text = "Inscription inactivé! S'il vous plaît contacter les organisateurs de l'événement.";

                return;
            }


            //    FormsAuthentication.RedirectFromLoginPage(txtConta.Text.Trim(), false);
           // }
           // else
          //  {

            Session["SessionEvento"] = SessionEvento;

            Session["SessionParticipante"] = SessionParticipante;

            if (!SessionParticipante.FlValidarEmail)
            {
                Geral oGeral = new Geral();
                oGeral.ValidarEmailParticipante(SessionParticipante, SessionCnn);
            }
            //Response.Write("<script>window.open('frm_cadastro.aspx?cdMatricula="+SessionParticipante.CdParticipante+"','_self');</script>");

            //Server.Transfer(string.Format("frm_cadastro.aspx?cdMatricula={0}",
            //Server.Transfer(string.Format("frmCadastroAuto.aspx?cdMatricula={0}",
            //SessionParticipante.CdParticipante),true);

            if (!SessionEvento.FlLiberarCertificacaoWeb)//((SessionEvento.CdCliente != "0003") && (SessionEvento.CdEvento != "002902") && (SessionEvento.CdEvento != "004401") && (SessionEvento.CdEvento != "003005"))
            {
                
                //if (SessionEvento.CdCliente == "0013")
                //{
                //    if (SessionParticipante.Categoria.FlQuestionario)
                //    {
                //        PesquisasDeOpiniao oPesquisasDeOpiniao = new PesquisasDeOpiniao();
                //        DataTable dtPesquisas = oPesquisasDeOpiniao.ListarPesquisasDoParticipante(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);

                //        if ((dtPesquisas != null) && (dtPesquisas.Rows.Count > 0))
                //        {
                //            Session["oDTPesquisa"] = null;
                //            Response.Write("<script>window.open('frmPesquisaVinculada.aspx','_self');</script>");
                //        }
                //    }

                //    if ((SessionParticipante.NoAreaAtuacao == "SIM") && (SessionParticipante.DsAuxiliar19 != "SIM"))
                //    {//SECRETÁRIOS MUNICIPAIS DE SAÚDE DEVEM RESPONDER A DECLARAÇÃO
                //        Response.Write("<script>window.open('frmTermoConasemsSecretarios.aspx','_self');</script>");
                //        return;
                //    }

                //    if ((Geral.datahoraServidor(SessionCnn) < DateTime.Parse("30/07/2015 00:00:00")))
                //    {
                //        if ((SessionParticipante.Categoria.FlAtividades) && (!SessionParticipante.FlConfirmacaoInscricao))
                //        {
                //            PedidoCad oPedidoCad = new PedidoCad();
                //            Pedido tempPedido = oPedidoCad.VerificaPedidoNaoPagoWeb(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);
                //            if ((tempPedido == null) || (tempPedido.TpPagamento == ""))
                //            {
                //                Response.Write("<script>window.open('frmSelAtividades.aspx','_self');</script>");
                //            }
                //        }

                //        if ((SessionParticipante.NoAreaAtuacao == "SIM") && (!SessionParticipante.FlDocumentoConfirmado) && (SessionParticipante.participanteDocEnviado == null))
                //        {//SECRETÁRIOS MUNICIPAIS DE SAÚDE DEVEM ENVIAR A DECLARAÇÃO
                //            Response.Write("<script>window.open('frmEnviarDocumento.aspx','_self');</script>");
                //        }
                //    }

                //    if ((Geral.datahoraServidor(SessionCnn) < DateTime.Parse("05/08/2015 00:00:00")))
                //    {
                //        if ((SessionParticipante.NoAreaAtuacao == "SIM") && (!SessionParticipante.FlDocumentoConfirmado) && (SessionParticipante.participanteDocEnviado == null))
                //        {//SECRETÁRIOS MUNICIPAIS DE SAÚDE DEVEM ENVIAR A DECLARAÇÃO
                //            Response.Write("<script>window.open('frmEnviarDocumento.aspx','_self');</script>");
                //        }
                //    }
                //}
                //else
                {
                    if ((SessionParticipante.Categoria.FlAtividades) && (!SessionParticipante.FlConfirmacaoInscricao) && (SessionEvento.CdEvento != "010101"))
                    {
                        PedidoCad oPedidoCad = new PedidoCad();
                        Pedido tempPedido = oPedidoCad.VerificaPedidoNaoPagoWeb(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);
                        if ((tempPedido == null) || (tempPedido.TpPagamento == ""))
                        {
                            if ((SessionEvento.CdEvento != "008501") && (SessionEvento.CdEvento != "008303"))
                                Response.Write("<script>window.open('frmSelAtividades.aspx','_self');</script>");
                            else if (SessionEvento.CdEvento == "008501")
                                Response.Write("<script>window.open('frmEscolherAcao.aspx','_self');</script>");
                            else if (SessionEvento.CdEvento == "008303")
                            {
                                if ((tempPedido != null) && (tempPedido.TpPagamento == ""))
                                    Response.Write("<script>window.open('frmSelAtividades.aspx','_self');</script>");
                                else
                                    Response.Write("<script>window.open('frmCadastroAuto.aspx','_self');</script>");
                            }
                        }
                    }
                }

                if (SessionEvento.CdEvento != "011201")
                    Response.Write("<script>window.open('frmCadastroAuto.aspx','_self');</script>");
                else
                    Response.Write("<script>window.open('frmSelAtividades.aspx','_self');</script>");
            }
            else
            {
                if ((SessionEvento.FlLiberarCertificacaoWeb) && (SessionEvento.CdEvento != "001008"))
                {
                    if (SessionParticipante.CdCredencial == "0")
                    {//não emitiu credencial
                        Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                        "028", ""), true);
                    }

                    if (!SessionParticipante.Categoria.FlCertificado)
                    {//não possui direito à certificado
                        Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                        "029", ""), true);
                    }

                    Server.Transfer(string.Format("frmCertificado.aspx?p={0}",
                                       cllEventos.Crypto.EncryptStringAES(SessionParticipante.CdParticipante)), true);
                    //Response.Write("<script>window.open('frmCertificado.aspx?p=" + cllEventos.Crypto.EncryptStringAES(SessionParticipante.CdParticipante) + "','_self');</script>");
                }
                else
                {
                    Response.Write("<script>window.open('frmCadastroAuto.aspx','_self');</script>");
                }
            }


            //if (SessionEvento.DsCampoAutenticacao.ToUpper().Replace("-", "") == "CPF")
            //{
            //    FormsAuthentication.RedirectFromLoginPage(TXTDsCPF.Text.Trim(), false);
            //}
            //else
            //{
            //    FormsAuthentication.RedirectFromLoginPage(txtEmail.Text.Trim(), false);
            //}
        }
        else
        {
            if (SessionIdioma == "PTBR")
                txtMsg.Text = "Participante não cadastrado ou senha inválida";
            else if (SessionIdioma == "ENUS")
                txtMsg.Text = "Participant is not registered or invalid password";
            else if (SessionIdioma == "ESP")
                txtMsg.Text = "Participante no está registrado o la contraseña no válida";
            else if (SessionIdioma == "FRA")
                txtMsg.Text = "Participant n'est pas inscrit ou mot de passe invalide";
        }
    }
    protected void btnCadastrar_Click(object sender, EventArgs e)
    {
        //if (SessionEvento.CdEvento == "001303")
        //{
        //    if (txtChaveLiberacao.Text.Trim() != "")
        //    {
        //        ParticipanteCad oParticipanteCad = new ParticipanteCad();
        //        ParticipantePreCadastro oParticipantePreCadastro = oParticipanteCad.PesquisarPreCadastroPorChave(SessionEvento.CdEvento, txtChaveLiberacao.Text, SessionCnn);

        //        if (oParticipantePreCadastro == null)
        //        {
        //            lblMsg2.Text = oParticipanteCad.RcMsg;
        //            return;
        //        }

        //        if (oParticipantePreCadastro.FlAtivo == false)
        //        {
        //            lblMsg2.Text = "Chave de Liberação Inativa";
        //            return;
        //        }

        //        if ((oParticipantePreCadastro.DtValidade != null) && (oParticipantePreCadastro.DtValidade.Value.Date < Geral.datahoraServidor(SessionCnn).Date))
        //        {
        //            lblMsg2.Text = "Chave de Liberação Vencida";
        //            return;
        //        }

        //        SessionCateg = oParticipantePreCadastro.CdCategoria;
        //        Session["SessionCateg"] = SessionCateg;

        //        Session["SessionChaveLibercao"] = oParticipantePreCadastro.DsCampoExtra;

        //    }
        //    else
        //    {
        //        SessionCateg = "00130301";
        //        Session["SessionCateg"] = SessionCateg;

        //        Session["SessionChaveLibercao"] = "";
        //    }

        //}
        SessionCateg = (string)Session["SessionCateg"];
        if (SessionEvento.CdCliente == "0013")
            SessionCateg = SessionEvento.CdEvento+"01";

        Session["SessionEvento"] = SessionEvento;
        
        Session["SessionCateg"] = SessionCateg;

        Session["SessionChaveLibercao"] = "";


        Session["SessionPais"] = "";
        if (SessionIdioma == "PTBR")
            Session["SessionPais"] = "BRASIL";

        RFVCPF.Enabled = false;
        if (((SessionIdioma == "PTBR") && (SessionEvento.DsInformacoesCompletasWeb_ptbr.Trim() != "")) ||
            ((SessionIdioma == "ENUS") && (SessionEvento.DsInformacoesCompletasWeb_enus.Trim() != "")) ||
            ((SessionIdioma == "ESP") && (SessionEvento.DsInformacoesCompletasWeb_esp.Trim() != "")) ||
            ((SessionIdioma == "FRA") && (SessionEvento.DsInformacoesCompletasWeb_fra.Trim() != "")) )
            Response.Write("<script>window.open('frmInformacoes.aspx','_self');</script>");
        else if (SessionEvento.FlPesquisaCPFReceita)
        {
            CategoriaCad oCategoriaCad = new CategoriaCad();
            Categoria tmpCategoria = oCategoriaCad.Pesquisar(SessionEvento.CdEvento, SessionCateg, SessionCnn);

            if ((tmpCategoria == null) || (!tmpCategoria.FlCPFCNPJObrigatorio))
                Response.Write("<script>window.open('frmCadastroAuto.aspx','_self');</script>");
            else
                Response.Write("<script>window.open('frmBuscaCPFReceita.aspx','_self');</script>");
        }
        else
        {
            if ((SessionIdioma == "PTBR") && (Geral.VerificarEventoIntenacional(SessionEvento.CdEvento, SessionCnn))) 
            {
                Response.Write("<script>window.open('frmVerificarBrasileiro.aspx','_self');</script>");
            }
            else
            {
                
                Response.Write("<script>window.open('frmCadastroAuto.aspx','_self');</script>");
            }
        }
    }



    protected void btnCadastrar3_Click(object sender, EventArgs e)
    {
        //if ((SessionEvento.CdEvento == "001303") ||  (SessionEvento.CdEvento == "004801"))
        //{
        if (((Request.QueryString["keyAut"] != null) &&//codigo da categoria
            (Request.QueryString["keyAut"].ToString().Trim().ToUpper() != "")) ||
             (txtChaveLiberacao.Visible))
        {
            if (txtChaveLiberacao.Text.Trim() != "")
            {
                ParticipanteCad oParticipanteCad = new ParticipanteCad();
                ParticipantePreCadastro oParticipantePreCadastro = oParticipanteCad.PesquisarPreCadastroPorChave(SessionEvento.CdEvento, txtChaveLiberacao.Text.Trim(), SessionCnn);

                if (oParticipantePreCadastro == null)
                {
                    lblMsg2.Text = oParticipanteCad.RcMsg;
                    return;
                }

                if (oParticipantePreCadastro.FlAtivo == false)
                {
                    lblMsg2.Text = "Chave de Liberação Inativa";
                    return;
                }

                if ((oParticipantePreCadastro.DtValidade != null) && (oParticipantePreCadastro.DtValidade.Value.Date < Geral.datahoraServidor(SessionCnn).Date))
                {
                    lblMsg2.Text = "Chave de Liberação Vencida";
                    return;
                }

                int totPrecadastradoChave = oParticipanteCad.TotalPreCadastradoChave(SessionEvento.CdEvento, txtChaveLiberacao.Text, SessionCnn);

                if ((oParticipantePreCadastro.QtdLimiteInscricaoChave > 0) && (totPrecadastradoChave >= oParticipantePreCadastro.QtdLimiteInscricaoChave)) //int.Parse(DTPre.DefaultView[0]["qtdLimiteInscricaoChave"].ToString()))
                {
                    lblMsg2.Text = "Limite de utilização para esta chave foi atingido!";//<br/>Não é possível efetuar inscrição.";
                    if (SessionEvento.CdEvento == "001304")
                    {
                        ClsFuncoes oclsfuncoes = new ClsFuncoes();
                        if (oParticipantePreCadastro.NoParticipantePrecadastro != "")
                            lblMsg2.Text +=
                                "<br/>Favor envie e-mail para: " + oParticipantePreCadastro.DsEmail +
                                " ou ligue para: " + oclsfuncoes.MascaraGerar(oParticipantePreCadastro.DsCelular,"(99) 9999-9999");
                        ;
                    }
                    return;
                }

                Session["SessionEvento"] = SessionEvento;

                SessionCateg = oParticipantePreCadastro.CdCategoria;
                Session["SessionCateg"] = SessionCateg;

                Session["SessionChaveLibercao"] = oParticipantePreCadastro.DsCampoExtra;

            }
            else
            {
                if (SessionEvento.CdEvento == "001305")
                    SessionCateg = "00130501";

                Session["SessionCateg"] = SessionCateg;

                Session["SessionChaveLibercao"] = "";

                Session["SessionEvento"] = SessionEvento;
            }

        }

        Session["SessionPais"] = "";
        if (SessionIdioma == "PTBR")
            Session["SessionPais"] = "BRASIL";

        RFVCPF.Enabled = false;
        if (((SessionIdioma == "PTBR") && (SessionEvento.DsInformacoesCompletasWeb_ptbr.Trim() != "")) ||
            ((SessionIdioma == "ENUS") && (SessionEvento.DsInformacoesCompletasWeb_enus.Trim() != "")) ||
            ((SessionIdioma == "ESP") && (SessionEvento.DsInformacoesCompletasWeb_esp.Trim() != "")) ||
            ((SessionIdioma == "FRA") && (SessionEvento.DsInformacoesCompletasWeb_fra.Trim() != "")))
            Response.Write("<script>window.open('frmInformacoes.aspx','_self');</script>");
        else if (SessionEvento.FlPesquisaCPFReceita)
        {
            CategoriaCad oCategoriaCad = new CategoriaCad();
            Categoria tmpCategoria = oCategoriaCad.Pesquisar(SessionEvento.CdEvento, SessionCateg, SessionCnn);

            

            if ((tmpCategoria == null) || (!tmpCategoria.FlCPFCNPJObrigatorio))
                Response.Write("<script>window.open('frmCadastroAuto.aspx','_self');</script>");
            else 
                Response.Write("<script>window.open('frmBuscaCPFReceita.aspx','_self');</script>");
        }
        else
            Response.Write("<script>window.open('frmCadastroAuto.aspx','_self');</script>");
    }
    
}
