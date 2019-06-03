using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using cllEventos;
using CLLFuncoes;



using System.Data;
using System.Data.SqlClient;

using System.IO;

using AjaxControlToolkit;

using System.Collections.Specialized;


using MSXML2;

using System.Xml;

using Cielo;
//using Cielo.Configuration;
//using Cielo.Enums;
//using Cielo.Requests;
//using Cielo.Requests.Entities;
//using Cielo.Responses;
//using Cielo.Responses.Exceptions;


public partial class frmCieloRetorno : System.Web.UI.Page
{

    private Participante SessionParticipante;
    private ParticipanteCad oParticipanteCad = new ParticipanteCad();
    private Evento SessionEvento;
    private EventoCad oEventoCad = new EventoCad();
    private SqlConnection SessionCnn;

    private Pedido SessionPedido;
    private PedidoCad oPedidoCad = new PedidoCad();

    private DataTable oDTAtividadesParticipante = new DataTable();

    private Inscricoes SessionInscricoes = new Inscricoes();

    private String SessionIdioma;

    /// <summary>
    /// Quando o cliente for redirecionado de volta para a loja pelo cielo, ele cairá nessa página,
    /// onde chamaremos o método finalize
    /// pelo PayPal.
    /// </summary>
    /// <param name='sender'>
    /// Sender.
    /// </param>
    /// <param name='e'>
    /// E.
    /// </param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            SessionCnn = (SqlConnection) Session["SessionCnn"];
            if (SessionCnn == null)
            {
                //local 1
                //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOWEzSnJjMkV0Y0dNN1NXNXBkR2xoYkNCRFlYUmhiRzluUFdSaVJYWmxiblJ2YzE5R1RUdFFaWEp6YVhOMElGTmxZM1Z5YVhSNUlFbHVabTg5VkhKMVpUdFZjMlZ5SUVsRVBYTmhPMUJoYzNOM2IzSmtQV3R5YTNOaE1UY3g=")));

                //local 2 note novo
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
                SessionCnn =
                    new SqlConnection(
                        cllEventos.Crypto.DecryptStringAES(
                            cllEventos.Crypto.DecryptStringAES(
                                "UkdGMFlTQlRiM1Z5WTJVOWFHcDBZWEF3YjNkcWJpNWtZWFJoWW1GelpTNTNhVzVrYjNkekxtNWxkRHRKYm1sMGFXRnNJRU5oZEdGc2IyYzlaR0pmWlhabGJuUnZjMFpOTzFCbGNuTnBjM1FnVTJWamRYSnBkSGtnU1c1bWJ6MVVjblZsTzFWelpYSWdTVVE5UTI5dVlYTmxiWE03VUdGemMzZHZjbVE5UXpCdU5EVmxiVFV5TURFMg==")));

                //Site-producao - IP
                //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOU1UZzVMak00TGprd0xqRXdPVHRKYm1sMGFXRnNJRU5oZEdGc2IyYzlabUY2Wlc1a2IyMWhhWE03VUdWeWMybHpkQ0JUWldOMWNtbDBlU0JKYm1adlBWUnlkV1U3VlhObGNpQkpSRDFtWVhwbGJtUnZiV0ZwY3p0UVlYTnpkMjl5WkQxUmRUUTFOSEpHYlUxRVFRPT0=")));

                Session["SessionCnn"] = SessionCnn;

            }

            Geral oGeral = new Geral();

            //if (oGeral.verificarSiteManutencao("1", SessionCnn))
            //{
            //    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
            //                    "05",
            //                    ""), true);
            //}

            SessionIdioma = (String) Session["SessionIdioma"];
            if (SessionIdioma == null)
                SessionIdioma = "PTBR";
            Session["SessionIdioma"] = SessionIdioma;

            SessionEvento = (Evento) Session["SessionEvento"];
            if (SessionEvento == null)
            {
                if ((Request.QueryString["e"] != null) &&
                    (Request.QueryString["e"] != ""))
                {
                    string cd_Evento = cllEventos.Crypto.DecryptStringAES(Request.QueryString["e"].ToString());

                    SessionEvento = oEventoCad.Pesquisar(cd_Evento, SessionCnn);
                    Session["SessionEvento"] = SessionEvento;
                    if (SessionEvento == null)
                    {
                        return;
                        //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                        //            "003",
                        //            ""), true);

                    }
                }


            }


            if (SessionEvento.DsLinkRedirecionamento != "")
                btnOkCielo.PostBackUrl = SessionEvento.DsLinkRedirecionamento;

            if (SessionEvento.CdEvento == "010801")
            {
                if (SessionIdioma == "PTBR")
                    btnOkCielo.PostBackUrl = SessionEvento.DsLinkRedirecionamento + "bike-parade-pt";
                else if (SessionIdioma == "ENUS")
                    btnOkCielo.PostBackUrl = SessionEvento.DsLinkRedirecionamento + "copia-bike-parade-pt";
            }

            if (SessionEvento.CdCliente == "0085")
                btnOkCielo.PostBackUrl = "~/frmEscolherAcao.aspx";

           

            SessionParticipante = (Participante) Session["SessionParticipante"];
            if (SessionParticipante == null)
            {
                if ((Request.QueryString["p"] != null) &&
                    (Request.QueryString["p"] != ""))
                {
                    string cd_participante = cllEventos.Crypto.DecryptStringAES(Request.QueryString["p"].ToString());

                    SessionParticipante = oParticipanteCad.Pesquisar(SessionEvento.CdEvento, cd_participante, SessionCnn);
                    Session["SessionParticipante"] = SessionParticipante;
                    if (SessionParticipante == null)
                    {
                        return;
                        //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                        //                 "04",
                        //                 ""), true);

                    }
                }
            }

            SessionPedido = (Pedido) Session["SessionPedido"];
            if (SessionPedido == null)
            {
                if ((Request.QueryString["pd"] != null) &&
                    (Request.QueryString["pd"] != ""))
                {
                    string cd_pedido = Request.QueryString["pd"].ToString();
                    // cllEventos.Crypto.DecryptStringAES(Request.QueryString["pd"].ToString());

                    SessionPedido = oPedidoCad.Pesquisar(SessionEvento.CdEvento, cd_pedido, SessionCnn);
                    Session["SessionPedido"] = SessionPedido;
                    if (SessionPedido == null)
                    {
                        return;
                        //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                        //                 "04",
                        //                 ""), true);

                    }
                }
            }

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


            string sURL = Request.Url.ToString().ToLower();
            string tmpXpto = "";
            if (sURL.Contains("localhost"))
                tmpXpto = "http://" + Request.Url.Authority;
            else
                tmpXpto = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
            //"https://inscricoesweb.fazendomais.com";

            decimal tmpVlDescontoCupom = 0;
            String tmpDsCupomDesconto = "";

            if (!SessionPedido.FlPago)
            {
                try
                {


                    //if ((Session["tid"] == null) || (Session["tid"].ToString() == ""))
                    if ((Session["response"] == null) || (Session["response"].ToString() == ""))
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text =
                            "Não foi possível concluir a transação!<br />Favor ligar para o Helpdesk e informar o Codigo de erro 001-328.";
                        if (SessionIdioma == "ENUS")
                            lblMsg.Text =
                                "Could not complete the transaction <br /> Please call the Helpdesk and report the error Code 001-328.";
                        else if (SessionIdioma == "ESP")
                            lblMsg.Text =
                                "No se pudo completar la transacción <br /> Por favor, llame al servicio de asistencia e informar del error de código 001-328.";

                        pedidos_esq.Visible = false;
                        carrinho_pedidos.Visible = false;
                        itensDisponiveisGeral.Visible = false;
                        btnVoltar.Visible = true;
                        //Session["tid"] = "";
                        Session["response"] = null;
                        return;
                    }
                    else
                    {
                        /***********    DESCONTINUADO    ***************************************
                        CieloService _cieloService;
                        CustomCieloConfiguration _configuration;

                        _cieloService = new CieloService();

                        _configuration = new CustomCieloConfiguration
                        {
                            CurrencyId = SessionIdioma == "PTBR" ? "986" : "840",
                            CustomerId = SessionEvento.cieloConfig.NumConta,//"1006993069",
                            CustomerKey = SessionEvento.cieloConfig.CodAcesso,//"25fbb99741c739dd84d7b06ec78c9bac718838630f30b112d033ce2e621b34f3",
                            Language = SessionIdioma == "PTBR" ? Language.Portuguese : Language.English,
                            ReturnUrl = tmpXpto + "/frmCieloRetorno.aspx?pd=" + SessionPedido.CdPedido + "&e=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + "&p=" + cllEventos.Crypto.EncryptStringAES(SessionParticipante.CdParticipante)
                        };

                        
                        var checkTransactionRequest = new CheckTransactionRequest((string)Session["tid"], _configuration);
                        CheckTransactionResponse response = _cieloService.CheckTransaction(checkTransactionRequest);
                        ***********    DESCONTINUADO    ***************************************/

                        var response = (Transaction) Session["response"];

                        if (response.Payment.Status != Status.PaymentConfirmed)
                        {
                            //lblMsg.Text = response.Payment.ReturnMessage;
                            if (SessionIdioma == "PTBR")
                            {
                                lblMsg.Text = MensagemRetornoCieloPTBR(response.Payment.ReturnCode);
                            }
                            else
                            {
                                lblMsg.Text = MensagemRetornoCieloENUS(response.Payment.ReturnCode);
                            }
                            //lblMsg.Text = CieloService.MensagemRetornoAutorizacao(response.Lr);
                            
                            pedidos_esq.Visible = false;
                            carrinho_pedidos.Visible = false;
                            itensDisponiveisGeral.Visible = false;
                            btnVoltar.Visible = true;
                            Session["tid"] = "";
                            Session["response"] = null;

                            return;
                        }
                        else
                        {

                            if (SessionIdioma == "PTBR")
                                lblMsg.Text = "<br />Operação ralizada com sucesso<br />";
                            else if (SessionIdioma == "ENUS")
                                lblMsg.Text = "<br />Operation was successful<br/>";
                            else if (SessionIdioma == "ESP")
                                lblMsg.Text = "<br />Operación fue un éxito<br/>";
                            else if (SessionIdioma == "FRA")
                                lblMsg.Text = "<br />Opération a réussi<br/>";





                            //oPedidoCad.MarcarPedidoComoPago(SessionPedido, SessionCnn);


                            SessionPedido.TpPagamento = (response.Payment.Type == Cielo.PaymentType.CreditCard ? "CARTÃO DE CRÉDITO" : "DÉBITO");
                            SessionPedido.NoBandeira = response.Payment.CreditCard.Brand.ToString().ToUpper();
                            SessionPedido.CdTransacaoOutrosTpPgto = response.Payment.PaymentId.ToString();

                            SessionPedido = oPedidoCad.Gravar(SessionPedido, SessionCnn);
                            Session["SessionPedido"] = SessionPedido;

                            oPedidoCad.MarcarPedidoComoPago(SessionPedido, SessionCnn);

                            
                            if (SessionPedido.pedidoCupomDesconto != null)
                            {
                                tmpVlDescontoCupom = SessionPedido.pedidoCupomDesconto.VlDescontoCalculado;
                                tmpDsCupomDesconto = SessionPedido.pedidoCupomDesconto.DsCupomDesconto;
                            }


                            //response.Payment.CapturedDate.Value;
                            
                                                            
                            ReciboCad oReciboCad = new ReciboCad();
                            Recibo oRecibo = oReciboCad.Gravar(
                                SessionEvento.CdEvento,
                                SessionParticipante.CdParticipante,
                                SessionPedido.CdPedido,
                                "",
                                SessionPedido.VlTotalPedido * SessionPedido.VlConversao,
                                tmpVlDescontoCupom * SessionPedido.VlConversao,
                                0,
                                SessionPedido.TpPagamento,
                                response.Payment.Tid, //"Aut: " + response.Payment.AuthorizationCode,
                                "Cartão: " + (response.Payment.Type == Cielo.PaymentType.CreditCard ? response.Payment.CreditCard.Brand.ToString().ToUpper() : response.Payment.DebitCard.Brand.ToString().ToUpper()) + " - " +
                                (response.Payment.Type == Cielo.PaymentType.CreditCard ? response.Payment.CreditCard.CardNumber.ToUpper() : response.Payment.DebitCard.CardNumber.ToUpper()) + "\n" +
                                "Transação: " + response.Payment.Tid + "\n" +
                                "Autorização: " + response.Payment.AuthorizationCode + "\n" +
                                "Nr Parcelas: " + response.Payment.Installments.ToString() + "\n" +
                                "Valor Operação: " + (SessionPedido.VlTotalPedido - tmpVlDescontoCupom).ToString() + " - Moeda: " + SessionPedido.NoMoeda + " - Taxa: " + SessionPedido.VlConversao.ToString() +
                                (tmpDsCupomDesconto != "" ? "\nCupom Desconto - " + tmpDsCupomDesconto : ""),
                                "000000001",
                                "",
                                (SessionPedido.VlTotalPedido - tmpVlDescontoCupom) * SessionPedido.VlConversao,
                                "",
                                0,
                                "",
                                SessionCnn);

                            GerarParcelasCartao(SessionPedido, response.Payment.Tid, SessionCnn);

                            oGeral.EnviarEmailPedidoRecebCartao(SessionEvento, SessionParticipante, SessionPedido,
                                SessionCnn);

                            if ((SessionEvento.CdEvento != "009902") && (SessionEvento.CdEvento != "011201"))
                            {
                                oGeral.EnviarEmailConfirmaCadastro(SessionEvento, SessionParticipante, SessionCnn);
                            }
                            else
                            {
                                GerarIngressos(SessionParticipante, SessionPedido.CdPedido, SessionCnn);
                            }

                            Session["tid"] = "";
                            Session["response"] = null;
                        }
                    }
                }
                catch
                {
                    lblMsg.Visible = true;
                    lblMsg.Text =
                        "Não foi possível concluir a transação!<br />Favor ligar para o Helpdesk e informar o Codigo de erro 001-435.";
                    if (SessionIdioma == "ENUS")
                        lblMsg.Text =
                            "Could not complete the transaction <br /> Please call the Helpdesk and report the error Code 001-435.";
                    else if (SessionIdioma == "ESP")
                        lblMsg.Text =
                            "No se pudo completar la transacción <br /> Por favor, llame al servicio de asistencia e informar del error de código 001-435.";

                    pedidos_esq.Visible = false;
                    carrinho_pedidos.Visible = false;
                    itensDisponiveisGeral.Visible = false;
                    btnVoltar.Visible = true;
                    Session["tid"] = "";
                    return;
                }
            }
            btnOkCielo.Visible = true;

            lblNrPedido.Text = SessionPedido.CdPedido;

            if (SessionPedido.pedidoCupomDesconto != null)
                tmpVlDescontoCupom = SessionPedido.pedidoCupomDesconto.VlDescontoCalculado;
            
            DataTable dtpedido = oPedidoCad.TotalizarPedido(SessionPedido.CdPedido, SessionCnn);
            if (dtpedido != null)
            {
                vlItens.Text = dtpedido.DefaultView[0]["itens"].ToString();
                vlTotalAtiv.Text = decimal.Parse(dtpedido.DefaultView[0]["tol_Atv"].ToString()).ToString("N2");
                vlTotalDesc.Text = (decimal.Parse(dtpedido.DefaultView[0]["tot_desc"].ToString()) + tmpVlDescontoCupom).ToString("N2");

                vlTotalAtiv.Font.Strikeout = (decimal.Parse(vlTotalDesc.Text) > 0);
            }

            vlTotalPedido.Text = (SessionPedido.VlTotalPedido - tmpVlDescontoCupom).ToString("N2");


            oDTAtividadesParticipante.Columns.Add("noTipoAtividade");
            oDTAtividadesParticipante.Columns.Add("noTitulo");
            oDTAtividadesParticipante.Columns.Add("dsTema");
            oDTAtividadesParticipante.Columns.Add("noLocal");
            oDTAtividadesParticipante.Columns.Add("dtIni", System.Type.GetType("System.DateTime"));
            oDTAtividadesParticipante.Columns.Add("dtTermino", System.Type.GetType("System.DateTime"));
            oDTAtividadesParticipante.Columns.Add("cdAtividade");
            oDTAtividadesParticipante.Columns.Add("noProfessor");
            oDTAtividadesParticipante.Columns.Add("cdTipoAtividade");
            oDTAtividadesParticipante.Columns.Add("vlAtividade", System.Type.GetType("System.Decimal"));
            oDTAtividadesParticipante.Columns.Add("flAtivo", System.Type.GetType("System.Boolean"));
            oDTAtividadesParticipante.Columns.Add("flUsado", System.Type.GetType("System.Boolean"));
            oDTAtividadesParticipante.Columns.Add("dtMatricula", System.Type.GetType("System.DateTime"));
            oDTAtividadesParticipante.Columns.Add("vlDesconto", System.Type.GetType("System.Decimal"));
            oDTAtividadesParticipante.Columns.Add("vlMatricula", System.Type.GetType("System.Decimal"));
            oDTAtividadesParticipante.Columns.Add("flInscricaoObrigatoria", System.Type.GetType("System.Boolean"));
            oDTAtividadesParticipante.Columns.Add("dtValidade");
            oDTAtividadesParticipante.Columns.Add("flPodeChocarHorario", System.Type.GetType("System.Boolean"));
            oDTAtividadesParticipante.Columns.Add("dsCaminhoImgWEB");
            oDTAtividadesParticipante.Columns.Add("vlQuantidade");
            oDTAtividadesParticipante.Columns.Add("flRequerQuantidade", System.Type.GetType("System.Boolean"));
            oDTAtividadesParticipante.Columns.Add("nrLinha");
            oDTAtividadesParticipante.Columns.Add("vlQuantidadeMaxima");
            oDTAtividadesParticipante.Columns.Add("flPodeRepetirPedido");
            oDTAtividadesParticipante.Columns.Add("dsTurno");

           // CarregarAtividadesParticipanteGrade();

            itensDisponiveisGeral.Visible = false;
        }
        else
        {
            SessionParticipante = (Participante) Session["SessionParticipante"];

            SessionEvento = (Evento) Session["SessionEvento"];

            SessionCnn = (SqlConnection) Session["SessionCnn"];

            SessionPedido = (Pedido) Session["SessionPedido"];

            SessionIdioma = (String) Session["SessionIdioma"];

        }

        if (SessionEvento == null)
            Server.Transfer("frmSessaoExpirada.aspx", true);

        verificarIdioma(SessionIdioma);
    }

    protected void verificarIdioma(string prmIdioma)
    {
        if (prmIdioma == "PTBR")
        {
            SessionIdioma = "PTBR";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Resumo do Pagamento";
            lblTituloResumo.Text = "Resumo do Pedido";


            lblPart.Text = "Participante:";
            lblCateg.Text = "Categoria:";

            lblResPed.Text = "Pedido nº";
            lblResItens.Text = "Itens";
            lblResVlr.Text = "Valor";
            lblResDesc.Text = "Desconto";
            lblResVlrTotal.Text = "Total (R$)";
            if ((SessionParticipante.NoPais == "BRASIL") ||
                (SessionParticipante.NoPais == "BRAZIL") ||
                (SessionParticipante.NoPais == "BRASIL") ||
                (SessionParticipante.NoPais == "BRÉSIL"))
                lblResVlrTotal.Text = "Total (R$)";
            else
                lblResVlrTotal.Text = "Total ($)";

            btnVoltar.Text = "Voltar";

            lblTituloGrid1.Text = "Iten(s) Solicitado(s)";
        }
        else if (prmIdioma == "ENUS")
        {
            SessionIdioma = "ENUS";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Summary of Payment";
            lblTituloResumo.Text = "Order summary";

            lblID.Text = "Registration no.";
            lblPart.Text = "Participant:";
            lblCateg.Text = "Category:";

            lblResPed.Text = "Order No.";
            lblResItens.Text = "Items";
            lblResVlr.Text = "Value";
            lblResDesc.Text = "Discount";
            lblResVlrTotal.Text = "Total ($)";
            if ((SessionParticipante.NoPais == "BRASIL") ||
                (SessionParticipante.NoPais == "BRAZIL") ||
                (SessionParticipante.NoPais == "BRASIL") ||
                (SessionParticipante.NoPais == "BRÉSIL"))
                lblResVlrTotal.Text = "Total (R$)";
            else
                lblResVlrTotal.Text = "Total ($)";

            btnVoltar.Text = "Back";

            lblTituloGrid1.Text = "Items Request";
        }
        else if (prmIdioma == "ESP")
        {
            SessionIdioma = "ESP";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Resúmen de Pago";
            lblTituloResumo.Text = "Resúmen del pedido";

            lblID.Text = "Nº de Registro:";
            lblPart.Text = "Participante:";
            lblCateg.Text = "Categoría:";


            lblResPed.Text = "Pedido nº";
            lblResItens.Text = "Itens";
            lblResVlr.Text = "Valor";
            lblResDesc.Text = "Descuentos";



            lblResVlrTotal.Text = "Total ($)";
            if ((SessionParticipante.NoPais == "BRASIL") ||
                (SessionParticipante.NoPais == "BRAZIL") ||
                (SessionParticipante.NoPais == "BRASIL") ||
                (SessionParticipante.NoPais == "BRÉSIL"))
                lblResVlrTotal.Text = "Total (R$)";
            else
                lblResVlrTotal.Text = "Total ($)";

            btnVoltar.Text = "Volver";

            lblTituloGrid1.Text = "Itens";
        }
        else if (prmIdioma == "FRA")
        {
            SessionIdioma = "FRA";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Résumé de paiement";
            lblTituloResumo.Text = "Résumé de la demande";

            lblPart.Text = "Participant:";
            lblCateg.Text = "Catégorie:";

            lblResPed.Text = "Demande no";
            lblResItens.Text = "Articles";
            lblResVlr.Text = "Valeur";
            lblResDesc.Text = "Réduction";
            lblResVlrTotal.Text = "Total";

            btnVoltar.Text = "Retour";

            lblTituloGrid1.Text = "Articles";
        }
    }

    private void CarregarAtividadesParticipanteGrade()
    {


        DataTable DTAtividadesp;
        DTAtividadesp = SessionInscricoes.ListarAtividadesDoPedido(SessionParticipante, lblNrPedido.Text, SessionCnn);

        //cdAtiv = "";

        if ((DTAtividadesp != null) && (DTAtividadesp.Rows.Count > 0))
        {
            oDTAtividadesParticipante.Rows.Clear();

            vlTotalAtiv.Text = "0,00";
            vlTotalDesc.Text = "0,00";
            vlTotalPedido.Text = "0,00";


            //vlItens.Text = "0"; ;

            for (int i = 0; i < DTAtividadesp.DefaultView.Count; i++)
            {


                oDTAtividadesParticipante.Rows.Add(
                    DTAtividadesp.DefaultView[i]["noTipoAtividade"].ToString().Trim(),
                    DTAtividadesp.DefaultView[i]["noTitulo"].ToString().Trim(),
                    DTAtividadesp.DefaultView[i]["dsTema"].ToString().Trim(),
                    DTAtividadesp.DefaultView[i]["noLocal"].ToString().Trim(),
                    DateTime.Parse(DTAtividadesp.DefaultView[i]["dtIni"].ToString().Trim()).ToString("dd/MM/yyyy HH:mm"),
                    DateTime.Parse(DTAtividadesp.DefaultView[i]["dtTermino"].ToString().Trim())
                        .ToString("dd/MM/yyyy HH:mm"),
                    DTAtividadesp.DefaultView[i]["cdAtividade"].ToString().Trim(),
                    DTAtividadesp.DefaultView[i]["noProfessor"].ToString().Trim(),
                    DTAtividadesp.DefaultView[i]["cdTipoAtividade"].ToString().Trim(),
                    decimal.Parse(DTAtividadesp.DefaultView[i]["vlAtividade"].ToString().Trim()),
                    Boolean.Parse(DTAtividadesp.DefaultView[i]["flAtivo"].ToString().Trim()),
                    Boolean.Parse(DTAtividadesp.DefaultView[i]["flUsado"].ToString().Trim()),
                    DateTime.Parse(DTAtividadesp.DefaultView[i]["dtMatricula"].ToString().Trim()).ToString("dd/MM/yyyy"),
                    decimal.Parse(DTAtividadesp.DefaultView[i]["vlDesconto"].ToString().Trim()),
                    decimal.Parse(DTAtividadesp.DefaultView[i]["vlMatricula"].ToString().Trim()),
                    Boolean.Parse(DTAtividadesp.DefaultView[i]["flInscricaoObrigatoria"].ToString().Trim()),
                    DTAtividadesp.DefaultView[i]["dtValidade"].ToString().Trim(),
                    Boolean.Parse(DTAtividadesp.DefaultView[i]["flPodeChocarHorario"].ToString().Trim()),
                    DTAtividadesp.DefaultView[i]["dsCaminhoImgWEB"].ToString(),
                    DTAtividadesp.DefaultView[i]["vlQuantidade"].ToString().Trim(),
                    Boolean.Parse(DTAtividadesp.DefaultView[i]["flRequerQuantidade"].ToString().Trim()),
                    i.ToString(),
                    DTAtividadesp.DefaultView[i]["vlQuantidadeMaxima"].ToString().Trim(),
                    DTAtividadesp.DefaultView[i]["flPodeRepetirPedido"].ToString().Trim(),
                    DTAtividadesp.DefaultView[i]["dsTurno"].ToString().Trim());

                vlTotalAtiv.Text = (decimal.Parse(vlTotalAtiv.Text) +
                                    (decimal.Parse(DTAtividadesp.DefaultView[i]["vlAtividade"].ToString().Trim())*
                                     int.Parse(DTAtividadesp.DefaultView[i]["vlQuantidade"].ToString()))
                    ).ToString("N2");
                vlTotalDesc.Text = (decimal.Parse(vlTotalDesc.Text) +
                                    (decimal.Parse(DTAtividadesp.DefaultView[i]["vlDesconto"].ToString().Trim())*
                                     int.Parse(DTAtividadesp.DefaultView[i]["vlQuantidade"].ToString()))
                    ).ToString("N2");
                vlTotalPedido.Text =
                    (decimal.Parse(vlTotalPedido.Text) +
                     decimal.Parse(DTAtividadesp.DefaultView[i]["vlMatricula"].ToString().Trim())).ToString("N2");


                //vlTotalAtiv.Text = (decimal.Parse(vlTotalAtiv.Text) + decimal.Parse(DTAtividadesp.DefaultView[i]["vlAtividade"].ToString().Trim())).ToString("N2");
                //vlTotalDesc.Text = (decimal.Parse(vlTotalDesc.Text) + decimal.Parse(DTAtividadesp.DefaultView[i]["vlDesconto"].ToString().Trim())).ToString("N2");
                //vlTotalPedido.Text = (decimal.Parse(vlTotalPedido.Text) + decimal.Parse(DTAtividadesp.DefaultView[i]["vlMatricula"].ToString().Trim())).ToString("N2");
            }
        }



        Session["oDTAtividadesParticipante"] = oDTAtividadesParticipante;

        grdAtvParticipante.DataSource = oDTAtividadesParticipante.DefaultView;
        grdAtvParticipante.DataBind();

        vlItens.Text = grdAtvParticipante.Rows.Count.ToString();



    }


    protected void grdAtvParticipante_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            Label lbldtini = (Label) e.Row.FindControl("lblDtIni");
            lbldtini.Text = DateTime.Parse(lbldtini.Text).ToString("dd/MM/yyyy HH:mm");
            Label lbldttermino = (Label) e.Row.FindControl("lblDtTermino");
            lbldttermino.Text = DateTime.Parse(lbldttermino.Text).ToString("dd/MM/yyyy HH:mm");

            Label lbltpItem = (Label) e.Row.FindControl("lblTpItem");
            Label lbldeItem = (Label) e.Row.FindControl("lblDeItem");
            Label lblateItem = (Label) e.Row.FindControl("lblAteItem");
            Label lblvagasItem = (Label) e.Row.FindControl("lblVagasItem");
            Label lbllocalItem = (Label) e.Row.FindControl("lblLocalItem");

            Label lblvalorItem = (Label) e.Row.FindControl("lblValorItem");
            Label lbldescItem = (Label) e.Row.FindControl("lblDescItem");
            Label lblqtdItem = (Label) e.Row.FindControl("lblQtdItem");
            Label lblvlTotalItem = (Label) e.Row.FindControl("lblVlrTotalItem");

            Label lblLocal = (Label) e.Row.FindControl("lblLocal");
            Label lblVagas = (Label) e.Row.FindControl("lblVagas");

            if (SessionIdioma == "PTBR")
            {
                lbltpItem.Text = "Tipo: ";
                lbldeItem.Text = "De: ";
                lblateItem.Text = " a: ";
                lbllocalItem.Text = "Local: ";

                lblvalorItem.Text = "Valor ";
                lbldescItem.Text = "Desconto ";
                lblqtdItem.Text = "Quantidade ";
                lblvlTotalItem.Text = "Vlr Total ";
            }
            else if (SessionIdioma == "ENUS")
            {
                lbltpItem.Text = "Type: ";
                lbldeItem.Text = "From: ";
                lblateItem.Text = " to: ";
                lbllocalItem.Text = "Local: ";

                lblvalorItem.Text = "Price ";
                lbldescItem.Text = "Discount ";
                lblqtdItem.Text = "Amount ";
                lblvlTotalItem.Text = "Total ";
            }
            else if (SessionIdioma == "ESP")
            {
                lbltpItem.Text = "Tipo: ";
                lbldeItem.Text = "De: ";
                lblateItem.Text = " a: ";
                lbllocalItem.Text = "Local: ";

                lblvalorItem.Text = "Precio ";
                lbldescItem.Text = "Descuento ";
                lblqtdItem.Text = "Cantidad ";
                lblvlTotalItem.Text = "Total ";
            }
            else if (SessionIdioma == "FRA")
            {
                lbltpItem.Text = "Type: ";
                lbldeItem.Text = "De: ";
                lblateItem.Text = " à: ";
                lbllocalItem.Text = "Local: ";

                lblvalorItem.Text = "Price ";
                lbldescItem.Text = "Réduction ";
                lblqtdItem.Text = "Montant ";
                lblvlTotalItem.Text = "Total ";
            }

            if ((SessionEvento.CdCliente == "0003") || (SessionEvento.CdEvento == "003401"))
            {
                lbldtini.Visible = false;
                lbldttermino.Visible = false;
                lbldeItem.Visible = false;
                lblateItem.Visible = false;
                lbllocalItem.Visible = false;
                lblLocal.Visible = false;

            }

            if (SessionEvento.CdCliente == "0016")
            {
                lbldtini.Visible = false;
                lbldttermino.Visible = false;
                lbldeItem.Visible = false;
                lblateItem.Visible = false;
                //lbllocalItem.Visible = false;
                //lblLocal.Visible = false;

            }

            Label lblatv = (Label) e.Row.FindControl("lblVlAtividade");
            Label lbldec = (Label) e.Row.FindControl("lblVlDescontoReal");
            lblatv.Font.Strikeout = (decimal.Parse(lbldec.Text) > 0);

            Image imgprofatv = (Image) e.Row.FindControl("imgAtivProf");
            imgprofatv.Visible = (imgprofatv.ImageUrl != "");

            if ((SessionEvento.CdCliente == "0013") || (SessionEvento.CdCliente == "0003") ||
                (SessionEvento.CdEvento == "002902") || (SessionEvento.CdEvento == "003401"))
            {

                Panel pnlresumovlritens = (Panel) e.Row.FindControl("pnlResumoVlrItens");
                pnlresumovlritens.Visible = false;
            }
        }

    }

    /// <summary>
    /// Aqui uma pequena demonstração de como obter os dados do NVP.
    /// </summary>
    /// <param name='nvp'>
    /// Nvp.
    /// </param>
    private void createAndPopulateTable(NameValueCollection nvp)
    {
        Table table;

        table = createTableWithCaption("Dados do Cliente");

        createTableRowWithNameAndValue(table, "Nome do cliente", nvp["FIRSTNAME"] + " " + nvp["LASTNAME"]);
        createTableRowWithNameAndValue(table, "Email", nvp["EMAIL"]);
        createTableRowWithNameAndValue(table, "ID do cliente no PayPal", nvp["PAYERID"]);

        //table = createTableWithCaption("Dados de Entrega");

        //createTableRowWithNameAndValue(table, "Entregar para", nvp["PAYMENTREQUEST_0_SHIPTONAME"]);
        //createTableRowWithNameAndValue(table, "Endereço", nvp["PAYMENTREQUEST_0_SHIPTOSTREET"]);
        //createTableRowWithNameAndValue(table, "Cidade", nvp["PAYMENTREQUEST_0_SHIPTOCITY"]);
        //createTableRowWithNameAndValue(table, "Estado", nvp["PAYMENTREQUEST_0_SHIPTOSTATE"]);
        //createTableRowWithNameAndValue(table, "CEP", nvp["PAYMENTREQUEST_0_SHIPTOZIP"]);
        //createTableRowWithNameAndValue(table, "País", nvp["PAYMENTREQUEST_0_SHIPTOCOUNTRYNAME"]);
    }

    private Table createTableWithCaption(string caption)
    {
        Table table = new Table();

        TableHeaderRow captionRow = new TableHeaderRow();
        TableHeaderCell captionCell = new TableHeaderCell();

        captionCell.Text = caption;
        captionCell.HorizontalAlign = HorizontalAlign.Left;
        captionCell.ColumnSpan = 2;

        captionRow.Cells.Add(captionCell);

        table.Rows.Add(captionRow);

        // data.Controls.Add(table);

        return table;
    }

    private void createTableRowWithNameAndValue(Table table, string name, string value)
    {
        TableRow row = new TableRow();
        TableCell nameCell = new TableCell();
        TableCell valueCell = new TableCell();

        nameCell.Text = name;
        nameCell.Width = 300;

        valueCell.Text = value;
        valueCell.Width = 400;

        row.Cells.Add(nameCell);
        row.Cells.Add(valueCell);

        table.Rows.Add(row);
    }

    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        Server.Transfer(string.Format("frm_formapagamento.aspx?cdMatricula={0}",
            SessionParticipante.CdParticipante), true);
    }

    private string MensagemRetornoCieloPTBR(string PrmCodRetornoCielo)
    {
        string retorno = "";
        switch (PrmCodRetornoCielo)
        {
            case "0": retorno = "Criada"; break;
            case "1": retorno = "Em andamento"; break;
            case "2": retorno = "Autenticada"; break;
            case "3": retorno = "Não Autenticada"; break;
            case "4": retorno = "Autorizada"; break;
            case "5": retorno = "Não Autorizada"; break;
            case "6": retorno = "Capturada"; break;
            case "9": retorno = "Cancelada"; break;
            case "10": retorno = "Em autenticação"; break;
            //case "12": retorno = "Em cancelamento"; break;
            case "00": retorno = "Transação autorizada com sucesso."; break;
            case "000": retorno = "Transação autorizada com sucesso."; break;
            case "01": retorno = "Transação não autorizada. Transação referida."; break;
            case "02": retorno = "Transação não autorizada. Transação referida."; break;
            case "03": retorno = "Transação não permitida. Erro no cadastramento do código do estabelecimento no arquivo de configuração do TEF"; break;
            case "04": retorno = "Transação não autorizada. Cartão bloqueado pelo banco emissor."; break;
            case "05": retorno = "Transação não autorizada. Cartão inadimplente (Do not honor)."; break;
            case "06": retorno = "Transação não autorizada. Cartão cancelado."; break;
            case "07": retorno = "Transação negada. Reter cartão condição especial"; break;
            case "08": retorno = "Transação não autorizada. Código de segurança inválido."; break;
            case "11": retorno = "Transação autorizada com sucesso para cartão emitido no exterior"; break;
            case "12": retorno = "Transação inválida, erro no cartão."; break;
            case "13": retorno = "Transação não permitida. Valor da transação Inválido."; break;
            case "14": retorno = "Transação não autorizada. Cartão Inválido"; break;
            case "15": retorno = "Banco emissor indisponível ou inexistente."; break;
            case "19": retorno = "Refaça a transação ou tente novamente mais tarde."; break;
            case "21": retorno = "Cancelamento não efetuado. Transação não localizada."; break;
            case "22": retorno = "Parcelamento inválido. Número de parcelas inválidas."; break;
            case "23": retorno = "Transação não autorizada. Valor da prestação inválido."; break;
            case "24": retorno = "Quantidade de parcelas inválido."; break;
            case "25": retorno = "Pedido de autorização não enviou número do cartão"; break;
            case "28": retorno = "Arquivo temporariamente indisponível."; break;
            case "30": retorno = "Transação não autorizada. Decline Message"; break;
            case "39": retorno = "Transação não autorizada. Erro no banco emissor."; break;
            case "41": retorno = "Transação não autorizada. Cartão bloqueado por perda."; break;
            case "43": retorno = "Transação não autorizada. Cartão bloqueado por roubo."; break;
            case "51": retorno = "Transação não autorizada. Limite excedido/sem saldo."; break;
            case "52": retorno = "Cartão com dígito de controle inválido."; break;
            case "53": retorno = "Transação não permitida. Cartão poupança inválido"; break;
            case "54": retorno = "Transação não autorizada. Cartão vencido"; break;
            case "55": retorno = "Transação não autorizada. Senha inválida"; break;
            case "57": retorno = "Transação não permitida para o cartão"; break;
            case "58": retorno = "Transação não permitida. Opção de pagamento inválida."; break;
            case "59": retorno = "Transação não autorizada. Suspeita de fraude."; break;
            case "60": retorno = "Transação não autorizada."; break;
            case "61": retorno = "Banco emissor indisponível."; break;
            case "62": retorno = "Transação não autorizada. Cartão restrito para uso doméstico"; break;
            case "63": retorno = "Transação não autorizada. Violação de segurança"; break;
            case "64": retorno = "Transação não autorizada. Valor abaixo do mínimo exigido pelo banco emissor."; break;
            case "65": retorno = "Transação não autorizada. Excedida a quantidade de transações para o cartão."; break;
            case "67": retorno = "Transação não autorizada. Cartão bloqueado para compras hoje."; break;
            case "70": retorno = "Transação não autorizada. Limite excedido/sem saldo."; break;
            case "72": retorno = "Cancelamento não efetuado. Saldo disponível para cancelamento insuficiente."; break;
            case "74": retorno = "Transação não autorizada. A senha está vencida."; break;
            case "75": retorno = "Senha bloqueada. Excedeu tentativas de cartão."; break;
            case "76": retorno = "Cancelamento não efetuado. Banco emissor não localizou a transação original"; break;
            case "77": retorno = "Cancelamento não efetuado. Não foi localizado a transação original"; break;
            case "78": retorno = "Transação não autorizada. Cartão bloqueado primeiro uso."; break;
            case "80": retorno = "Transação não autorizada. Divergencia na data de transação/pagamento."; break;
            case "82": retorno = "Transação não autorizada. Cartão inválido."; break;
            case "83": retorno = "Transação não autorizada. Erro no controle de senhas"; break;
            case "85": retorno = "Transação não permitida. Falha da operação."; break;
            case "86": retorno = "Transação não permitida. Falha da operação."; break;
            case "89": retorno = "Erro na transação."; break;
            case "90": retorno = "Transação não permitida. Falha da operação."; break;
            case "91": retorno = "Transação não autorizada. Banco emissor temporariamente indisponível."; break;
            case "92": retorno = "Transação não autorizada. Tempo de comunicação excedido."; break;
            case "93": retorno = "Transação não autorizada. Violação de regra - Possível erro no cadastro."; break;
            case "96": retorno = "Falha no processamento."; break;
            case "97": retorno = "Valor não permitido para essa transação."; break;
            case "98": retorno = "Sistema/comunicação indisponível."; break;
            case "99": retorno = "Sistema/comunicação indisponível."; break;
            case "999": retorno = "Sistema/comunicação indisponível."; break;
            case "AA": retorno = "Tempo Excedido"; break;
            case "AC": retorno = "Transação não permitida. Cartão de débito sendo usado com crédito. Use a função débito."; break;
            case "AE": retorno = "Tente Mais Tarde"; break;
            case "AF": retorno = "Transação não permitida. Falha da operação."; break;
            case "AG": retorno = "Transação não permitida. Falha da operação."; break;
            case "AH": retorno = "Transação não permitida. Cartão de crédito sendo usado com débito. Use a função crédito."; break;
            case "AI": retorno = "Transação não autorizada. Autenticação não foi realizada."; break;
            case "AJ": retorno = "Transação não permitida. Transação de crédito ou débito em uma operação que permite apenas Private Label. Tente novamente selecionando a opção Private Label."; break;
            case "AV": retorno = "Transação não autorizada. Dados Inválidos"; break;
            case "BD": retorno = "Transação não permitida. Falha da operação."; break;
            case "BL": retorno = "Transação não autorizada. Limite diário excedido."; break;
            case "BM": retorno = "Transação não autorizada. Cartão Inválido"; break;
            case "BN": retorno = "Transação não autorizada. Cartão ou conta bloqueado."; break;
            case "BO": retorno = "Transação não permitida. Falha da operação."; break;
            case "BP": retorno = "Transação não autorizada. Conta corrente inexistente."; break;
            case "BV": retorno = "Transação não autorizada. Cartão vencido"; break;
            case "CF": retorno = "Transação não autorizada.C79:J79 Falha na validação dos dados."; break;
            case "CG": retorno = "Transação não autorizada. Falha na validação dos dados."; break;
            case "DA": retorno = "Transação não autorizada. Falha na validação dos dados."; break;
            case "DF": retorno = "Transação não permitida. Falha no cartão ou cartão inválido."; break;
            case "DM": retorno = "Transação não autorizada. Limite excedido/sem saldo."; break;
            case "DQ": retorno = "Transação não autorizada. Falha na validação dos dados."; break;
            case "DS": retorno = "Transação não permitida para o cartão"; break;
            case "EB": retorno = "Transação não autorizada. Limite diário excedido."; break;
            case "EE": retorno = "Transação não permitida. Valor da parcela inferior ao mínimo permitido."; break;
            case "EK": retorno = "Transação não permitida para o cartão"; break;
            case "FA": retorno = "Transação não autorizada."; break;
            case "FC": retorno = "Transação não autorizada. Ligue Emissor"; break;
            case "FD": retorno = "Transação negada. Reter cartão condição especial"; break;
            case "FE": retorno = "Transação não autorizada. Divergencia na data de transação/pagamento."; break;
            case "FF": retorno = "Cancelamento OK"; break;
            case "FG": retorno = "Transação não autorizada. Ligue AmEx."; break;
           // case "FG": retorno = "Ligue 08007285090"; break;
            case "GA": retorno = "Aguarde Contato"; break;
            case "HJ": retorno = "Transação não permitida. Código da operação inválido."; break;
            case "IA": retorno = "Transação não permitida. Indicador da operação inválido."; break;
            case "JB": retorno = "Transação não permitida. Valor da operação inválido."; break;
            case "KA": retorno = "Transação não permitida. Falha na validação dos dados."; break;
            case "KB": retorno = "Transação não permitida. Selecionado a opção incorrente."; break;
            case "KE": retorno = "Transação não autorizada. Falha na validação dos dados."; break;
            case "N7": retorno = "Transação não autorizada. Código de segurança inválido."; break;
            case "R1": retorno = "Transação não autorizada. Cartão inadimplente (Do not honor)."; break;
            case "U3": retorno = "Transação não permitida. Falha na validação dos dados."; break;
            case "GD": retorno = "Transação não permitida"; break;


            default:
                retorno = "";
                break;
        }

        return retorno;
    }

    private string MensagemRetornoCieloENUS(string PrmCodRetornoCielo)
    {
        string retorno = "";
        switch (PrmCodRetornoCielo)
        {
            case "00": retorno = "Successfully authorized transaction."; break;
            case "000": retorno = "Successfully authorized transaction."; break;
            case "01": retorno = "Unauthorized transaction. Referred transaction."; break;
            case "02": retorno = "Unauthorized transaction. Referred transaction."; break;
            case "03": retorno = "Transaction not allowed. Error in registering the establishment code in the TEF configuration file"; break;
            case "04": retorno = "Unauthorized transaction. Card blocked by issuing bank."; break;
            case "05": retorno = "Unauthorized transaction. Defaulting card (Do not honor)."; break;
            case "06": retorno = "Unauthorized transaction. Card canceled."; break;
            case "07": retorno = "Transaction denied. Hold special condition card"; break;
            case "08": retorno = "Unauthorized transaction. Invalid security code."; break;
            case "11": retorno = "Successfully authorized transaction for card issued abroad"; break;
            case "12": retorno = "Invalid transaction, card error."; break;
            case "13": retorno = "Transaction not allowed. Invalid transaction value."; break;
            case "14": retorno = "Unauthorized transaction. Invalid Card"; break;
            case "15": retorno = "Issuing bank unavailable or non-existent."; break;
            case "19": retorno = "Redo the transaction or try again later."; break;
            case "21": retorno = "Cancellation not done. Non-localized transaction."; break;
            case "22": retorno = "Invalid installment. Invalid number of installments."; break;
            case "23": retorno = "Unauthorized transaction. Invalid installment value."; break;
            case "24": retorno = "Invalid number of installments."; break;
            case "25": retorno = "Request for authorization did not send card number"; break;
            case "28": retorno = "File temporarily unavailable."; break;
            case "30": retorno = "Unauthorized transaction. Decline Message"; break;
            case "39": retorno = "Unauthorized transaction. Error at the issuing bank."; break;
            case "41": retorno = "Unauthorized transaction. Card locked for loss."; break;
            case "43": retorno = "Unauthorized transaction. Card locked for theft."; break;
            case "51": retorno = "Unauthorized transaction. Limit exceeded/no balance."; break;
            case "52": retorno = "Card with invalid control digit."; break;
            case "53": retorno = "Transaction not allowed. Invalid Savings card"; break;
            case "54": retorno = "Unauthorized transaction. Expired card"; break;
            case "55": retorno = "Unauthorized transaction. Invalid password"; break;
            case "57": retorno = "Transaction not allowed for the card"; break;
            case "58": retorno = "Transaction not allowed. Invalid payment option."; break;
            case "59": retorno = "Unauthorized transaction. Suspected fraud."; break;
            case "60": retorno = "Unauthorized transaction."; break;
            case "61": retorno = "Issuing bank unavailable."; break;
            case "62": retorno = "Unauthorized transaction. Card restricted for home use"; break;
            case "63": retorno = "Unauthorized transaction. Security breach"; break;
            case "64": retorno = "Unauthorized transaction. Value below the minimum required by the issuing bank."; break;
            case "65": retorno = "Unauthorized transaction. Exceeded the number of transactions for the card."; break;
            case "67": retorno = "Unauthorized transaction. Card locked for shopping today."; break;
            case "70": retorno = "Unauthorized transaction. Limit exceeded/no balance."; break;
            case "72": retorno = "Cancellation not done. Not enough available balance for cancellation."; break;
            case "74": retorno = "Unauthorized transaction. The password is expired."; break;
            case "75": retorno = "Password locked. Exceeded card attempts."; break;
            case "76": retorno = "Cancellation not done. Issuer bank did not locate the original transaction"; break;
            case "77": retorno = "Cancellation not done. The original transaction was not found"; break;
            case "78": retorno = "Unauthorized transaction. Locked card first use."; break;
            case "80": retorno = "Unauthorized transaction. Divergence on transaction/payment date."; break;
            case "82": retorno = "Unauthorized transaction. Invalid card."; break;
            case "83": retorno = "Unauthorized transaction. Error in password control"; break;
            case "85": retorno = "Transaction not allowed. Operation failed."; break;
            case "86": retorno = "Transaction not allowed. Operation failed."; break;
            case "89": retorno = "Transaction error."; break;
            case "90": retorno = "Transaction not allowed. Operation failed."; break;
            case "91": retorno = "Unauthorized transaction. Issuing bank temporarily unavailable."; break;
            case "92": retorno = "Unauthorized transaction. Communication time exceeded."; break;
            case "93": retorno = "Unauthorized transaction. Rule violation - Possible error in register."; break;
            case "96": retorno = "Processing failed."; break;
            case "97": retorno = "Value not allowed for this transaction."; break;
            case "98": retorno = "System/communication unavailable."; break;
            case "99": retorno = "System/communication unavailable."; break;
            case "999": retorno = "System/communication unavailable."; break;
            case "AA": retorno = "Time Exceeded"; break;
            case "AC": retorno = "Transaction not allowed. Debit card being used as credit. Use the debit function."; break;
            case "AE": retorno = "Try later"; break;
            case "AF": retorno = "Transaction not allowed. Operation failed."; break;
            case "AG": retorno = "Transaction not allowed. Operation failed."; break;
            case "AH": retorno = "Transaction not allowed. Credit card being used as debit. Use the credit function."; break;
            case "AI": retorno = "Unauthorized transaction. Authentication was not performed."; break;
            case "AJ": retorno = "Transaction not allowed. Credit or debit transaction in an operation that allows only Private Label. Try again by selecting the Private Label option."; break;
            case "AV": retorno = "Unauthorized transaction. Invalid data"; break;
            case "BD": retorno = "Transaction not allowed. Operation failed."; break;
            case "BL": retorno = "Unauthorized transaction. Daily limit exceeded."; break;
            case "BM": retorno = "Unauthorized transaction. Invalid card"; break;
            case "BN": retorno = "Unauthorized transaction. Card or account locked."; break;
            case "BO": retorno = "Transaction not allowed. Operation failed."; break;
            case "BP": retorno = "Unauthorized transaction. Non-existent checking account."; break;
            case "BV": retorno = "Unauthorized transaction. Expired card"; break;
            case "CF": retorno = "Unauthorized transaction.C79:J79 Data validation failed."; break;
            case "CG": retorno = "Unauthorized transaction. Data validation failed."; break;
            case "DA": retorno = "Unauthorized transaction. Data validation failed."; break;
            case "DF": retorno = "Transaction not allowed. Invalid card or card failure."; break;
            case "DM": retorno = "Unauthorized transaction. Limit exceeded/no balance."; break;
            case "DQ": retorno = "Unauthorized transaction. Data validation failed."; break;
            case "DS": retorno = "Transaction not allowed for the card"; break;
            case "EB": retorno = "Unauthorized transaction. Daily limit exceeded."; break;
            case "EE": retorno = "Transaction not allowed. Installment value below the minimum allowed."; break;
            case "EK": retorno = "Transaction not allowed for the card"; break;
            case "FA": retorno = "Unauthorized transaction."; break;
            case "FC": retorno = "Unauthorized transaction. Call the Issuer"; break;
            case "FD": retorno = "Transaction denied. Hold special condition card"; break;
            case "FE": retorno = "Unauthorized transaction. Divergence on transaction/payment date."; break;
            case "FF": retorno = "Cancellation OK"; break;
            case "FG": retorno = "Unauthorized transaction. Call AmEx."; break;
            //case "FG": retorno = "Call 08007285090"; break;
            case "GA": retorno = "Wait for contact"; break;
            case "HJ": retorno = "Transaction not allowed. Invalid operation code."; break;
            case "IA": retorno = "Transaction not allowed. Invalid operation indicator."; break;
            case "JB": retorno = "Transaction not allowed. Invalid operation value."; break;
            case "KA": retorno = "Transaction not allowed. Data validation failed."; break;
            case "KB": retorno = "Transaction not allowed. Incurred option selected."; break;
            case "KE": retorno = "Unauthorized transaction. Data validation failed."; break;
            case "N7": retorno = "Unauthorized transaction. Invalid security code."; break;
            case "R1": retorno = "Unauthorized transaction. Default card (Do not honor)."; break;
            case "U3": retorno = "Transaction not allowed. Data validation failed."; break;
            case "GD": retorno = "Transaction not allowed"; break;

            default:
                retorno = "";
                break;
        }

        return retorno;
    }


    public void GerarIngressos(Participante prmParticipante, String prmCdPedido, SqlConnection prmCnn)
    {
        if (SessionCnn == null)
        {
            //_erroNoCampo = true;
            //lblMsg.Text = "Conexão inválida ou inexistente";
            return;
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
                //lblMsg.Text = "Conexão inválida";
                return;
            }
        }


        try
        {
            try
            {

                Inscricoes oInscricoes = new Inscricoes();
                DataTable dtAtvPed = oInscricoes.ListarAtividadesDoPedido(prmParticipante, prmCdPedido, prmCnn);

                if (dtAtvPed != null)
                {
                    string cmd = "";
                    string cmd2 = "";
                    string cmd3 = "";
                    Boolean wrkParticipanteAtualizado = (prmParticipante.DsAuxiliar19 != "" ? true : false);
                    DataTable DT = new DataTable();
                    SqlDataAdapter Dap;
                    string tmpGuid = "";
                    for (int j = 0; j < dtAtvPed.Rows.Count; j++)
                    {
                        for (int i = 0; i < int.Parse(dtAtvPed.DefaultView[j]["vlQuantidade"].ToString()); i++)
                        {

                            if (SessionCnn.State != ConnectionState.Open)
                            {
                                try
                                {
                                    SessionCnn.Open();
                                }
                                catch
                                {
                                    //_erroNoCampo = true;
                                    //lblMsg.Text = "Conexão inválida";
                                    return;
                                }
                            }

                            cmd2 = @"SELECT NEWID() cdIngresso";

                            SqlCommand comando2 = new SqlCommand(cmd2, prmCnn);
                            Dap = new SqlDataAdapter(comando2);

                            Dap.TableMappings.Add("Guid", "Guid");
                            Dap.Fill(DT);

                            if (DT != null)
                                tmpGuid = DT.DefaultView[i]["cdIngresso"].ToString();

                            cmd = @"
                                INSERT INTO dbo.tbIngressos
                                (
                                  cdEvento
                                 ,cdParticipante
                                 ,cdAtividade
                                 ,cdIngresso
                                 ,cdOrdem
                                 ,cdPedido
                                 ,dtCadastro
                                )
                                VALUES
                                (
                                  @cdEvento
                                 ,@cdParticipante
                                 ,@cdAtividade
                                 ,@cdIngresso
                                 ,@cdOrdem
                                 ,@cdPedido
                                 ,GETDATE()
                                )";

                            cmd = cmd.Replace("@cdEvento", "'" + prmParticipante.CdEvento + "'").
                                      Replace("@cdParticipante", "'" + prmParticipante.CdParticipante + "'").
                                      Replace("@cdAtividade", "'" + dtAtvPed.DefaultView[j]["cdAtividade"].ToString() + "'").
                                      Replace("@cdIngresso", "'" + tmpGuid + "'").
                                      Replace("@cdOrdem", "'" + (i + 1).ToString().PadLeft(5, '0') + "'").
                                      Replace("@cdPedido", "'" + prmCdPedido + "'");

                            SqlCommand comando = new SqlCommand(cmd, prmCnn);

                            comando.ExecuteNonQuery();

                            if (!wrkParticipanteAtualizado)
                            {
                                cmd3 = @"
                                    UPDATE tbParticipantes
                                    SET dsAuxiliar19 = @cdIngresso
                                       ,dsAuxiliar18 = @cdPedido
                                       ,dsAuxiliar17 = @cdAtividade
                                       ,dsAuxiliar16 = @cdOrdem
                                       ,cdCredencial = '1'
                                       ,dtCredencial = getdate()
                                       ,dtPrimeiraCredencial = getdate()
                                    WHERE cdEvento = @cdEvento
                                    AND cdParticipante = @cdParticipante
                                    ";

                                cmd3 = cmd3.Replace("@cdEvento", "'" + prmParticipante.CdEvento + "'").
                                      Replace("@cdParticipante", "'" + prmParticipante.CdParticipante + "'").
                                      Replace("@cdIngresso", "'" + tmpGuid + "'").
                                      Replace("@cdAtividade", "'" + dtAtvPed.DefaultView[j]["cdAtividade"].ToString() + "'").
                                      Replace("@cdOrdem", "'" + (i + 1).ToString().PadLeft(5, '0') + "'").
                                      Replace("@cdPedido", "'" + prmCdPedido + "'");

                                SqlCommand comando3 = new SqlCommand(cmd3, prmCnn);

                                comando3.ExecuteNonQuery();

                                wrkParticipanteAtualizado = true;
                            }
                            else
                            {
                                Participante tmpPart = new Participante();

                                tmpPart.CdEvento = SessionEvento.CdEvento;
                                tmpPart.NoParticipante = "Ref. " + SessionParticipante.NoParticipante;
                                tmpPart.CdCategoria = SessionParticipante.CdCategoria;
                                tmpPart.DsEmail = SessionParticipante.DsEmail;
                                tmpPart.DsFone1 = SessionParticipante.DsFone1;

                                tmpPart.DsAuxiliar15 = SessionParticipante.CdParticipante;
                                tmpPart.DsAuxiliar16 = (i + 1).ToString().PadLeft(5, '0');
                                tmpPart.DsAuxiliar17 = dtAtvPed.DefaultView[j]["cdAtividade"].ToString();
                                tmpPart.DsAuxiliar18 = prmCdPedido;
                                tmpPart.DsAuxiliar19 = tmpGuid;

                                tmpPart.CdOperador = "000000001";
                                tmpPart.DsIdioma = SessionParticipante.DsIdioma;
                                tmpPart.NoPais = SessionParticipante.NoPais;

                                tmpPart = oParticipanteCad.Gravar(tmpPart, SessionCnn);
                                oParticipanteCad.GerarCredencial(tmpPart, SessionCnn);
                                tmpPart = oParticipanteCad.ConfirmarInscricao(tmpPart, "000000001", SessionCnn);
                                oInscricoes.MatriculasGravar(tmpPart.CdEvento, tmpPart.CdParticipante, dtAtvPed.DefaultView[j]["cdAtividade"].ToString(), 0, 1, "000000001", SessionCnn);
                            }
                        }
                    }

                    Geral oGeral = new Geral();
                    oGeral.EnviarEmailQrCodesIngressos(SessionEvento, SessionParticipante, prmCdPedido, SessionCnn);

                }




            }
            catch (Exception Ex)
            {
                //_erroNoCampo = true;
                //lblMsg.Text = "Erro ao selecionar ESCOLAS!\n" + Ex.Message;

            }
        }
        finally
        {
            SessionCnn.Close();
        }


    }

    public void GerarParcelasCartao(Pedido prmPedido, String prmTID, SqlConnection prmCnn)
    {
        if (prmCnn == null)
        {
            //_erroNoCampo = true;
            //lblMsg.Text = "Conexão inválida ou inexistente";
            return;
        }


        if (prmCnn.State != ConnectionState.Open)
        {
            try
            {
                prmCnn.Open();
            }
            catch
            {
                //_erroNoCampo = true;
                //lblMsg.Text = "Conexão inválida";
                return;
            }
        }


        try
        {
            try
            {

                int intevalo = 30;

                decimal tmpVlDescontoCupom = 0;

                if (prmPedido.pedidoCupomDesconto != null)
                {
                    tmpVlDescontoCupom = prmPedido.pedidoCupomDesconto.VlDescontoCalculado;
                }

                decimal vlrTotalPagamento = (prmPedido.VlTotalPedido - tmpVlDescontoCupom) * prmPedido.VlConversao;

                decimal vlrParcela = Math.Truncate(100 * (vlrTotalPagamento / prmPedido.QtdParcelas)) / 100;
                decimal vlrParcelaAcumulada = 0;

                DateTime dtPrevisao = DateTime.Parse(prmPedido.DtPedido.Value.ToString("dd/MM/yyyy") + " 23:59:59");

                string cmd = "";
                for (int i = 1; i <= prmPedido.QtdParcelas; i++)
                {
                    if (i == prmPedido.QtdParcelas)
                        vlrParcela = vlrTotalPagamento - vlrParcelaAcumulada;

                    cmd = @" SET DATEFORMAT DMY;
                            INSERT INTO dbo.tbPedidoCartaoParcelas
                            (
                              cdEvento
                             ,cdPedido
                             ,cdParcela
                             ,vlParcela
                             ,dtPrevisaoTransfBanco
                             ,flConciliadoBanco
                             ,vlConciliadoBanco
                             ,dtEntradaBanco
                             ,cdOperadorConciliacao
                             ,dtOperacao
                             ,TID
                            )
                            VALUES
                            (
                              @cdEvento 
                             ,@cdPedido
                             ,@cdParcela 
                             ,@vlParcela 
                             ,@dtPrevisaoTransfBanco 
                             ,0 
                             ,0 
                             ,null 
                             ,'' 
                             ,null 
                             ,@TID
                            )";

                    cmd = cmd.Replace("@cdEvento", "'" + prmPedido.CdEvento + "'").
                              Replace("@cdPedido", "'" + prmPedido.CdPedido + "'").
                              Replace("@cdParcela", "'" + (i).ToString().PadLeft(3, '0') + "'").
                              Replace("@vlParcela", vlrParcela.ToString().Replace(",",".") ).
                              Replace("@dtPrevisaoTransfBanco", "'" + dtPrevisao.AddDays(intevalo * i) + "'").
                              Replace("@TID", "'" + prmTID + "'");

                    SqlCommand comando = new SqlCommand(cmd, prmCnn);

                    comando.ExecuteNonQuery();

                    vlrParcelaAcumulada += vlrParcela;
                }






            }
            catch (Exception Ex)
            {
                //_erroNoCampo = true;
                //lblMsg.Text = "Erro ao selecionar ESCOLAS!\n" + Ex.Message;

            }
        }
        finally
        {
            prmCnn.Close();
        }


    }
    
}