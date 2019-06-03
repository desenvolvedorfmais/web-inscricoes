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

public partial class frmExpoepi : System.Web.UI.Page
{
    Participante SessionParticipante;
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    SqlConnection SessionCnn;
    SqlConnection SessionCnn2;

    String SessionIdioma;
    String SessionCateg;

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
            //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOUxseHpkbkpmYTNKcmMyRTdTVzVwZEdsaGJDQkRZWFJoYkc5blBXUmlSWFpsYm5SdmMxOUdUVHRRWlhKemFYTjBJRk5sWTNWeWFYUjVJRWx1Wm04OVZISjFaVHRWYzJWeUlFbEVQWE5oTzFCaGMzTjNiM0prUFV0eWEzTmhNVGN4")));


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

            //SessionCateg = "";
            //if (cdEvento == "003003")
            //{
            //    if ((Request["cat"] != null) &&//codigo da categoria
            //        (Request["cat"].ToString().Trim().ToUpper() != ""))
            //    {
            //        SessionCateg = Request["cat"].ToString();
            //    }
            //}
            //Session["SessionCateg"] = SessionCateg;


            //if (SessionEvento.DsCon != "")
            //{
            //    SessionCnn2 = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES(SessionEvento.DsCon)));
            //    Session["SessionCnn"] = SessionCnn2;

            //    SessionEvento = oEventoCad.Pesquisar(cdEvento.Substring(0, 4), cdEvento, SessionCnn2);
            //    Session["SessionEvento"] = SessionEvento;
            //    if (SessionEvento == null)
            //    {
            //        Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
            //                        "003",
            //                        oEventoCad.RcMsg + cdEvento.Substring(0, 4)), true);
            //    }
            //}



            //if ((SessionEvento.DtFinalEvento == null) ||
            //    (SessionEvento.DtFinalEvento < Geral.datahoraServidor(SessionCnn)))// DateTime.Parse(DateTime.Today.ToString("dd/MM/yyyy"))))
            //{
            //    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
            //                    "006",
            //                    oEventoCad.RcMsg + cdEvento.Substring(0, 4)), true);
            //}



            //if (SessionEvento.FlSuspenderInscricaoWeb)
            //{
            //    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
            //                    "007",
            //                    oEventoCad.RcMsg + cdEvento.Substring(0, 4)), true);
            //}

            //if ((SessionEvento.DtAberturaInscrWeb == null) ||
            //    (SessionEvento.DtAberturaInscrWeb > Geral.datahoraServidor(SessionCnn)))//DateTime.Parse(DateTime.Today.ToString("dd/MM/yyyy"))))
            //{
            //    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
            //                    "004",
            //                    oEventoCad.RcMsg + cdEvento.Substring(0, 4)), true);
            //}

            //if ((SessionEvento.DtFechamentoInscrWeb == null) ||
            //    (SessionEvento.DtFechamentoInscrWeb < Geral.datahoraServidor(SessionCnn)))//DateTime.Parse(DateTime.Today.ToString("dd/MM/yyyy"))))
            //{
            //    if (!SessionEvento.FlLiberarCertificacaoWeb)
            //    {
            //        Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
            //                        "005",
            //                        oEventoCad.RcMsg + cdEvento.Substring(0, 4)), true);
            //    }
            //}


            //if (!SessionEvento.FlAutenticacaoWeb)
            //{
            //    RFVCPF.Enabled = false;

            //    if (SessionEvento.DsInformacoesCompletasWeb.Trim() != "")
            //        Response.Write("<script>window.open('frmInformacoes.aspx','_self');</script>");
            //    else
            //        Response.Write("<script>window.open('frmCadastroAuto.aspx','_self');</script>");
            //}
            //else
            //{
            //    if (SessionEvento.DsCampoAutenticacao.ToUpper().Replace("-", "") == "EMAIL")
            //    {
            //        lblEmail.Visible = txtEmail.Visible = revTxtEmail.Visible = rfvTxtEmail.Visible = true;
            //        LBLCONTA.Visible = TXTDsCPF.Visible = RFVCPF.Visible = false;
            //        txtEmail.Focus();
            //    }
            //    else
            //        TXTDsCPF.Focus();
            //}


            //if (cdEvento == "000801")
            //{
            //    btnCadastrar.Visible = false;
            //    btnCadastrar2.Visible = true;
            //}

            //if (cdEvento == "002501")
            //{
            //    btnCadastrar.Text = "Cadastro para comprar ingresso";

            //}

            ////if ((cdEvento != "000303") && (cdEvento != "002902"))
            ////    lblInstrucoesRapidas.Text = SessionEvento.DsInformacoesPreviasWeb;
            ////else
            ////{
            ////    telacad_direita.Visible = false;
            ////}

            //if ((SessionEvento.DtFechamentoInscrWeb == null) ||
            //    (SessionEvento.DtFechamentoInscrWeb < Geral.datahoraServidor(SessionCnn)))//DateTime.Parse(DateTime.Today.ToString("dd/MM/yyyy"))))
            //{
            //    if (SessionEvento.FlLiberarCertificacaoWeb)
            //    {
            //        lblTiuloTelaDir.Visible = false;
            //        btnCadastrar.Visible = false;
            //    }
            //}
            //else
            //{
            //    lblTiuloTelaDir.Visible = true;
            //    btnCadastrar.Visible = true;
            //    lblInstrucoesRapidas.Text = SessionEvento.DsInformacoesPreviasWeb;
            //}


            ////instrucoesRapidas();



        }
        else
        {
            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            SessionIdioma = (String)Session["SessionIdioma"];

            SessionCateg = (String)Session["SessionCateg"];
        }

        

    }

    protected string prpTexto(Participante prmParticipante)
    {
        string texto =
            "<div id='Geral' style='width:1000px;'> " +
            "<p class='MsoNormal' style='margin-bottom: 0.0001pt; text-align: justify; '><o:p>&nbsp;</o:p></p> " +
            "<p class='MsoNormal' style='margin-bottom: 0.0001pt; text-align: justify; '>Caro(a) participante <b>NOPARTICIPANTE</b>,<o:p /></p> " +
            "<p class='MsoNormal' style='margin-bottom: 0.0001pt; text-align: justify; '><o:p>&nbsp;</o:p></p> " +
            "<p class='MsoNormal' style='margin-bottom: 0.0001pt; text-align: justify; '>&#201; com alegria e satisfa&#231;&#227;o que lhe damos boas vindas &#224; 12&#170; EXPOEPI. Para sua tranquilidade, segue abaixo importantes informa&#231;&#245;es e orienta&#231;&#245;es sobre sua chegada e estadia em Bras&#237;lia-DF, e tamb&#233;m para seu retorno.<o:p /></p> " +
            "<p class='MsoNormal' style='margin-bottom: 0.0001pt; text-align: justify; '><o:p>&nbsp;</o:p></p> ";
        
        if (prmParticipante.DsAuxiliar4 == "SIM")
        texto +=   
            "<p class='MsoNormal' style='margin-bottom: 0.0001pt; text-align: justify; '><b>DADOS DE VOO<o:p /></b></p> " +
            "<table class='MsoTableGrid' border='1' cellspacing='0' cellpadding='0' style='border-collapse: collapse; border: none; '>  " +
            "<tbody> " +
            "<tr>   " +
            "<td width='518' valign='top' style='width: 388.45pt; border: 1pt solid windowtext; background-color: #bfbfbf; padding: 0cm 5.4pt; background-position: initial initial; background-repeat: initial initial; '>   " +
            "<p class='MsoNormal' align='center' style='margin-bottom: 0.0001pt; text-align: center; '><b>IDA<o:p /></b></p>  </td>   " +
            "<td width='518' valign='top' style='width: 388.45pt; border-style: solid solid solid none; border-top-color: windowtext; border-right-color: windowtext; border-bottom-color: windowtext; border-top-width: 1pt; border-right-width: 1pt; border-bottom-width: 1pt; background-color: #bfbfbf; padding: 0cm 5.4pt; background-position: initial initial; background-repeat: initial initial; '>   " +
            "<p class='MsoNormal' align='center' style='margin-bottom: 0.0001pt; text-align: center; '><b>VOLTA<o:p /></b></p>  </td> </tr>  " +
            "<tr>   " +
            "<td width='518' valign='top' style='width: 388.45pt; border-style: none solid solid; border-right-color: windowtext; border-bottom-color: windowtext; border-left-color: windowtext; border-right-width: 1pt; border-bottom-width: 1pt; border-left-width: 1pt; padding: 0cm 5.4pt; '>   " +
            "<p class='MsoNormal' style='margin-bottom: 0.0001pt; text-align: justify; '><b>ORIGEM: </b>ORIGEMIDA<o:p /></p>   " +
            "<p class='MsoNormal' style='margin-bottom: 0.0001pt; text-align: justify; '><b>DESTINO: </b>DESTINOIDA<o:p /></p>   " +
            "<p class='MsoNormal' style='margin-bottom: 0.0001pt; text-align: justify; '><b>DATA:</b>  DATAIDA<o:p /></p>   " +
            "<p class='MsoNormal' style='margin-bottom: 0.0001pt; text-align: justify; '><b>HORA  SA&#205;DA:</b> SAIDAIDA<o:p /></p>   " +
            "<p class='MsoNormal' style='margin-bottom: 0.0001pt; text-align: justify; '><b>HORA  CHEGADA:</b> CHEGADAIDA<o:p /></p>   " +
            "<p class='MsoNormal' style='margin-bottom: 0.0001pt; text-align: justify; '><b><span lang='EN-US'>VOO:</span></b><span lang='EN-US'> VOOIDA<o:p /></span></p>   " +
            "<p class='MsoNormal' style='margin-bottom: 0.0001pt; text-align: justify; '><b><span lang='EN-US'>E-TICKET:</span></b><span lang='EN-US'> TICKETIDA<o:p /></span></p>  </td>   " +
            "<td width='518' valign='top' style='width:388.45pt;border-top:none;border-left:  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt'>   " +
            "<p class='MsoNormal' style='margin-bottom: 0.0001pt; text-align: justify; '><b>ORIGEM:</b>  ORIGEMVOLTA<o:p /></p>   " +
            "<p class='MsoNormal' style='margin-bottom: 0.0001pt; text-align: justify; '><b>DESTINO:</b>  DESTINOVOLTA<o:p /></p>   " +
            "<p class='MsoNormal' style='margin-bottom: 0.0001pt; text-align: justify; '><b>DATA:</b>  DATAVOLTA<o:p /></p>   " +
            "<p class='MsoNormal' style='margin-bottom: 0.0001pt; text-align: justify; '><b>HORA  SA&#205;DA:</b> SAIDAVOLTA<o:p /></p>   " +
            "<p class='MsoNormal' style='margin-bottom: 0.0001pt; text-align: justify; '><b>HORA  CHEGADA:</b> -<o:p /></p>   " +
            "<p class='MsoNormal' style='margin-bottom: 0.0001pt; text-align: justify; '><b>VOO:</b>  VOOVOLTA<o:p /></p>   " +
            "<p class='MsoNormal' style='margin-bottom: 0.0001pt; text-align: justify; '><b>E-TICKET:</b>  TICKETVOLTA<o:p /></p>  </td> </tr></tbody></table> " +
            "<p class='MsoNormal' style='margin-bottom: 0.0001pt; text-align: justify; '><span style='color:#c00000'>Obs.: Seus dados de voo tamb&#233;m podem ser consultados pelo site </span><a href='http://www.expoepi.com.br/'><span style='color:#c00000'>www.expoepi.com.br</span></a><span style='color:#c00000'>, no campo Passagens/Hospedagem.<o:p /></span></p> " +
            "<p class='MsoNormal' style='margin-bottom: 0.0001pt; text-align: justify; '><o:p>&nbsp;</o:p></p> " ;

        texto +=
            "<p class='MsoNormal' style='margin-bottom: 0.0001pt; text-align: justify; '><b>TRASLADO: AEROPORTO/HOTEL/AEROPORTO<o:p /></b></p> " +
            "<p class='MsoNormal' style='margin-bottom: 0.0001pt; text-align: justify; '>Nossa Equipe de Transporte aguardar&#225; sua chegada ao Aeroporto Internacional Presidente Juscelino Kubitschek, de acordo com o hor&#225;rio acima. Caso n&#227;o identifique a mesma no port&#227;o de desembarque, voc&#234; poder&#225; direcionar-se ao balc&#227;o pr&#243;ximo &#224; Floricultura do Aeroporto - l&#225; ser&#225; o nosso ponto de refer&#234;ncia para que n&#227;o haja nenhum desencontro. Para o retorno, ser&#225; divulgado durante o evento o local, dia e hor&#225;rio de sa&#237;da para o aeroporto. N&#227;o deixe de procurar nossa Equipe de Transporte no pavilh&#227;o de exposi&#231;&#227;o do evento para se informar!<o:p /></p> " +
            "<p class='MsoNormal' style='margin-bottom: 0.0001pt; text-align: justify; '>&nbsp;<o:p /></p> ";

        if (prmParticipante.DsAuxiliar11 == "SIM")
        texto +=
            "<p class='MsoNormal' style='margin-bottom: 0.0001pt; text-align: justify; '><b>HOSPEDAGEM<o:p /></b></p> " +
            "<p class='MsoNormal' style='margin-bottom: 0.0001pt; text-align: justify; '>Voc&#234; ficar&#225; hospedado (a) no hotel <b>NOHOTEL</b>, localizado no <b>ENDERHOTEL</b>, em quarto <b>TIPOQUARTO</b>. Haver&#225; transporte Hotel/local do evento/Hotel, durante todo o per&#237;odo do evento.<o:p /></p> " +
            "<p class='MsoNormal' style='margin-bottom: 0.0001pt; text-align: justify; '><span style='color:#c00000'>Obs.: As informa&#231;&#245;es sobre sua hospedagem podem ser consultadas pelo site </span><a href='http://www.expoepi.com.br/'><span style='color:#c00000'>www.expoepi.com.br</span></a><span style='color:#c00000'>, no campo Passagens/Hospedagem.</span><o:p /></p> " +
            "<p class='MsoNormal' style='margin-bottom: 0.0001pt; text-align: justify; '><o:p>&nbsp;</o:p></p> ";

        texto +=
            "<p class='MsoNormal' style='margin-bottom: 0.0001pt; text-align: justify; '><b>PARTICIPANTES &#8220;ESPECIAIS&#8221;<o:p /></b></p> " +
            "<p class='MsoNormal' style='margin-bottom: 0.0001pt; text-align: justify; '>Aos participantes com necessidades especiais, idosos, gestantes com alguma dificuldade de locomo&#231;&#227;o, pedimos a gentileza de entrar em contato com a Comiss&#227;o Organizadora para que o seu deslocamento durante a mostra seja feito em ve&#237;culos adequados.<o:p /></p> " +
            "<p class='MsoNormal' style='margin-bottom: 0.0001pt; text-align: justify; '><o:p>&nbsp;</o:p></p> " +
            "<p class='MsoNormal' style='margin-bottom: 0.0001pt; text-align: justify; '><b>LOCAL DO EVENTO<o:p /></b></p> " +
            "<p class='MsoNormal' style='margin-bottom: 0.0001pt; text-align: justify; '>Centro de Conven&#231;&#245;es Ulysses Guimar&#227;es<o:p /></p> " +
            "<p class='MsoNormal' style='margin-bottom: 0.0001pt; text-align: justify; '>Setor de Divulga&#231;&#227;o Cultural, Eixo Monumental, lote 05, Ala Sul &#8211; Bras&#237;lia/DF.<o:p /></p> " +
            "<p class='MsoNormal' style='margin-bottom: 0.0001pt; text-align: justify; '><o:p>&nbsp;</o:p></p> " +
            "<p class='MsoNormal' style='margin-bottom: 0.0001pt; '>At&#233; breve.<o:p /></p> " +
            //"<p class='MsoNormal' style='margin-bottom: 0.0001pt; '><o:p>&nbsp;</o:p></p> " +
            "<p class='MsoNormal' style='margin-bottom: 0.0001pt; '><b><i>Comiss&#227;o Organizadora<o:p /></i></b></p> " +
            "<p class='MsoNormal' style='margin-bottom: 0.0001pt; '>expoepi.svs@saude.gov.br<o:p /></p> " +
            "<p class='MsoNormal' style='margin-bottom: 0.0001pt; '>(61) 3315-3422/3429/3919<o:p /></p> " +
           // "<p class='MsoNormal' style='margin-bottom: 0.0001pt; text-align: justify; '><o:p>&nbsp;</o:p></p> " +
            "</div> ";

        texto = texto.Replace("NOPARTICIPANTE", prmParticipante.NoParticipante);
        texto = texto.Replace("ORIGEMIDA", prmParticipante.DsAuxiliar5.ToUpper());
        texto = texto.Replace("DESTINOIDA", "BRASÍLIA");
        texto = texto.Replace("DATAIDA", prmParticipante.DsAuxiliar6.ToUpper());
        texto = texto.Replace("SAIDAIDA", prmParticipante.DsAuxiliar7.ToUpper());
        texto = texto.Replace("CHEGADAIDA", prmParticipante.DsAuxiliar15.ToUpper());
        texto = texto.Replace("VOOIDA", prmParticipante.DsAuxiliar16.ToUpper());
        texto = texto.Replace("TICKETIDA", prmParticipante.DsAuxiliar18.ToUpper());


        texto = texto.Replace("ORIGEMVOLTA", "BRASÍLIA");
        texto = texto.Replace("DESTINOVOLTA", prmParticipante.DsAuxiliar8.ToUpper());
        texto = texto.Replace("DATAVOLTA", prmParticipante.DsAuxiliar9.ToUpper());
        texto = texto.Replace("SAIDAVOLTA", prmParticipante.DsAuxiliar10.ToUpper());
        texto = texto.Replace("VOOVOLTA", prmParticipante.DsAuxiliar17.ToUpper());
        texto = texto.Replace("TICKETVOLTA", prmParticipante.DsAuxiliar19.ToUpper());

        texto = texto.Replace("NOHOTEL", prmParticipante.DsAuxiliar12.ToUpper());
        texto = texto.Replace("ENDERHOTEL", prmParticipante.NoAreaAtuacao.ToUpper());
        texto = texto.Replace("TIPOQUARTO", prmParticipante.DsAuxiliar13.ToUpper());

        return texto;
    }
    protected void Verificar_Click(object sender, EventArgs e)
    {

        ParticipanteCad oParticipanteCad = new ParticipanteCad();
        
        SessionParticipante = oParticipanteCad.Pesquisar(SessionEvento.CdEvento, TXTDsCPF.Text.Replace(".", "").Replace("-", "").Trim(), SessionCnn);
        

        if (SessionParticipante != null)
        {
            
            Session["SessionParticipante"] = SessionParticipante;
            lblInstrucoesRapidas.Text = prpTexto(SessionParticipante);

            Panel1.Visible = false;
            
        }
        else
        {
            txtMsg.Text = "Participante não encontrado!<br /><br />Caso tenha certeza que possui direito a Passagem e/ou Hospedagem, entre em contato com a Comissão Organizadora: expoepi.svs@saude.gov.br / (61) 3315-3422/3429/3919";
            return;
        }

        
    }
}