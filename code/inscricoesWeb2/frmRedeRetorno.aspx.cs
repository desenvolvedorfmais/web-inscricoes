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

using Rede.Acquiring.SDK.Rest;
using Rede.Acquiring.SDK.Rest.Model;


public partial class frmRedeRetorno : System.Web.UI.Page
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

            verificarIdioma(SessionIdioma);

            if (SessionEvento.CdCliente == "0085")
                btnOkRede1.PostBackUrl = "~/frmEscolherAcao.aspx";

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


                        var response = (TransactionResponse)Session["response"];

                        if (response.ReturnCode != "00")
                        {
                            //lblMsg.Text = response.Payment.ReturnMessage;
                            if (SessionIdioma == "PTBR")
                            {
                                lblMsg.Text = MensagemRetornoRedePTBR(response.ReturnCode);
                            }
                            else
                            {
                                lblMsg.Text = MensagemRetornoCieloENUS(response.ReturnCode);
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





                            oPedidoCad.MarcarPedidoComoPago(SessionPedido, SessionCnn);


                            SessionPedido.TpPagamento = "CARTÃO DE CRÉDITO";
                            SessionPedido.NoBandeira = Session["cartaoCreditoBandeira"].ToString().ToUpper();
                            SessionPedido.CdTransacaoOutrosTpPgto = response.Tid;

                            SessionPedido = oPedidoCad.Gravar(SessionPedido, SessionCnn);
                            Session["SessionPedido"] = SessionPedido;

                            oPedidoCad.MarcarPedidoComoPago(SessionPedido, SessionCnn);

                            decimal tmpVlDescontoCupom = 0;
                            String tmpDsCupomDesconto = "";
                            if (SessionPedido.pedidoCupomDesconto != null)
                            {
                                tmpVlDescontoCupom = SessionPedido.pedidoCupomDesconto.VlDescontoCalculado;
                                tmpDsCupomDesconto = SessionPedido.pedidoCupomDesconto.DsCupomDesconto;
                            }

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
                                response.Tid, //"Aut: " + response.AuthorizationCode,
                                "Cartão: " + Session["cartaoCreditoBandeira"].ToString().ToUpper() + " - " +
                                response.CardBin +"......" + response.Last4 + "\n" +
                                "Transação: " + response.Tid + "\n" +
                                "Autorização: " + response.AuthorizationCode + "\n" +
                                "Nr Parcelas: " + (response.Installments.ToString() == "" ? "1" : response.Installments.ToString()) + "\n" +
                                "Valor Operação: " + (SessionPedido.VlTotalPedido - tmpVlDescontoCupom).ToString() + " - Moeda: " + SessionPedido.NoMoeda + " - Taxa: " + SessionPedido.VlConversao.ToString() +
                                (tmpDsCupomDesconto != "" ? "\nCupom Desconto - " + tmpDsCupomDesconto : ""),
                                "000000001",
                                "",
                                (SessionPedido.VlTotalPedido - tmpVlDescontoCupom) * SessionPedido.VlConversao,
                                "",
                                0,
                                "",
                                SessionCnn);


                            GerarParcelasCartao(SessionPedido, response.Tid, SessionCnn);

                            oGeral.EnviarEmailPedidoRecebCartao(SessionEvento, SessionParticipante, SessionPedido,
                                SessionCnn);
                            oGeral.EnviarEmailConfirmaCadastro(SessionEvento, SessionParticipante, SessionCnn);

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
            btnOkRede1.Visible = true;

            if (SessionEvento.CdEvento == "010901")
            {
                btnOkRede2.Visible = true;
            }

            lblNrPedido.Text = SessionPedido.CdPedido;

            DataTable dtpedido = oPedidoCad.TotalizarPedido(SessionPedido.CdPedido, SessionCnn);
            if (dtpedido != null)
            {
                vlItens.Text = dtpedido.DefaultView[0]["itens"].ToString();
                vlTotalAtiv.Text = decimal.Parse(dtpedido.DefaultView[0]["tol_Atv"].ToString()).ToString("N2");
                vlTotalDesc.Text = decimal.Parse(dtpedido.DefaultView[0]["tot_desc"].ToString()).ToString("N2");

                vlTotalAtiv.Font.Strikeout = (decimal.Parse(vlTotalDesc.Text) > 0);
            }

            vlTotalPedido.Text = SessionPedido.VlTotalPedido.ToString("N2");


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

            CarregarAtividadesParticipanteGrade();
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
            lblResVlrTotal.Text = "Total";

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
            lblResVlrTotal.Text = "Total(USD)";

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


            lblResVlrTotal.Text = "Total(USD)";

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

        if (SessionEvento.CdEvento == "010901")
        {
            btnOkRede1.PostBackUrl = "~/frmCadastroAuto.aspx";
            btnOkRede2.PostBackUrl =
                "http://fazendomais4.azurewebsites.net/inscr.aspx?codEvento=" +
                cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) +
                "&cdLng=" + SessionIdioma +
                "&cat=" + "" +
                "&atv=" + "" +
                "&keyAut=" + "" +
                "&tpSist=NRM" +
                "&tpAcesso=NOVA";
            if (SessionIdioma == "PTBR")
            {
                btnOkRede1.Text = "Voltar para o Cadastro";
                btnOkRede2.Text = "Cadastrar Novo Particpante";
            }
            else if (SessionIdioma == "ENUS")
            {
                btnOkRede1.Text = "Back to register";
                btnOkRede2.Text = "Sign Up New Member";
            }
            btnOkRede1.Visible = true;
            btnOkRede2.Visible = true;
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

    private string MensagemRetornoRedePTBR(string PrmCodRetornoRede)
    {
        string retorno = "";
        switch (PrmCodRetornoRede)
        {

            case "00": retorno = "Success"; break;
            case "101": retorno = "Unauthorized. Problems on the card, contact the issuer."; break;
            case "102": retorno = "Unauthorized. Check the situation of the store with the issuer."; break;
            case "103": retorno = "Unauthorized. Please try again."; break;
            case "104": retorno = "Unauthorized. Please try again."; break;
            case "105": retorno = "Unauthorized. Restricted card."; break;
            case "106": retorno = "Error in issuer processing. Please try again."; break;
            case "107": retorno = "Unauthorized. Please try again."; break;
            case "108": retorno = "Unauthorized. Value not allowed for this type of card."; break;
            case "109": retorno = "Unauthorized. Nonexistent card."; break;
            case "110": retorno = "Unauthorized. Transaction type not allowed for this card."; break;
            case "111": retorno = "Unauthorized. Insufficient funds."; break;
            case "112": retorno = "Unauthorized. Expiry date expired."; break;
            case "113": retorno = "Unauthorized. Identified moderate risk by the issuer."; break;
            case "114": retorno = "Unauthorized. The card does not belong to the payment network."; break;
            case "115": retorno = "Unauthorized. Exceeded the limit of transactions allowed in the period."; break;
            case "116": retorno = "Unauthorized. Please contact the Card Issuer."; break;
            case "117": retorno = "Transaction not found."; break;
            case "118": retorno = "Unauthorized. Card locked."; break;
            case "119": retorno = "Unauthorized. Invalid security code"; break;
            case "120": retorno = "Zero dollar transaction approved successfully."; break;
            case "121": retorno = "Error processing. Please try again."; break;
            case "122": retorno = "Transaction previously sent"; break;
            case "123": retorno = "Unauthorized. Bearer requested the end of the recurrences in the issuer."; break;
            case "124": retorno = "Unauthorized. Contact Rede"; break;

            case "01": retorno = "expirationYear: Invalid parameter size"; break;
            case "02": retorno = "expirationYear: Invalid parameter format"; break;
            case "03": retorno = "expirationYear: Required parameter missing"; break;
            case "04": retorno = "cavv: Invalid parameter size"; break;
            case "05": retorno = "cavv: Invalid parameter format"; break;
            case "06": retorno = "postalCode: Invalid parameter size"; break;
            case "07": retorno = "postalCode: Invalid parameter format "; break;
            case "08": retorno = "postalCode: Required parameter missing"; break;
            case "09": retorno = "complement: Invalid parameter size"; break;
            case "10": retorno = "complement: Invalid parameter format"; break;
            case "11": retorno = "departureTax: Invalid parameter format"; break;
            case "12": retorno = "documentNumber: Invalid parameter size"; break;
            case "13": retorno = "documentNumber: Invalid parameter format"; break;
            case "14": retorno = "documentNumber: Required parameter missing"; break;
            case "15": retorno = "securityCode: Invalid parameter size"; break;
            case "16": retorno = "securityCode: Invalid parameter format"; break;
            case "17": retorno = "distributorAffiliation: Invalid parameter size"; break;
            case "18": retorno = "distributorAffiliation: Invalid parameter format"; break;
            case "19": retorno = "xid: Invalid parameter size"; break;
            case "20": retorno = "eci: Invalid parameter format"; break;
            case "21": retorno = "xid: Required parameter for Visa card is missing"; break;
            case "22": retorno = "street: Required parameter missing"; break;
            case "23": retorno = "street: Invalid parameter format"; break;
            case "24": retorno = "affiliation: Invalid parameter size"; break;
            case "25": retorno = "affiliation: Invalid parameter format"; break;
            case "26": retorno = "affiliation: Required parameter missing"; break;
            case "27": retorno = "Parameter cavv or eci missing"; break;
            case "28": retorno = "code: Invalid parameter size"; break;
            case "29": retorno = "code: Invalid parameter format"; break;
            case "30": retorno = "code: Required parameter missing"; break;
            case "31": retorno = "softdescriptor: Invalid parameter size"; break;
            case "32": retorno = "softdescriptor: Invalid parameter format"; break;
            case "33": retorno = "expirationMonth: Invalid parameter format"; break;
            case "34": retorno = "code: Invalid parameter format"; break;
            case "35": retorno = "expirationMonth: Required parameter missing"; break;
            case "36": retorno = "cardNumber: Invalid parameter size"; break;
            case "37": retorno = "cardNumber: Invalid parameter format"; break;

            case "38": retorno = "cardNumber: Required parameter missing"; break;
            case "39": retorno = "reference: Invalid parameter size"; break;
            case "40": retorno = "reference: Invalid parameter format"; break;
            case "41": retorno = "reference: Required parameter missing"; break;
            case "42": retorno = "reference: Order number already exists"; break;
            case "43": retorno = "number: Invalid parameter size"; break;
            case "44": retorno = "number: Invalid parameter format"; break;
            case "45": retorno = "number: Required parameter missing"; break;
            case "46": retorno = "installments: Not correspond to authorization transaction"; break;
            case "47": retorno = "origin: Invalid parameter format"; break;
            case "49": retorno = "The value of the transaction exceeds the authorized"; break;
            case "50": retorno = "installments: Invalid parameter format"; break;
            case "51": retorno = "Product or service disabled for this merchant. Contact Rede"; break;
            case "53": retorno = "Transaction not allowed for the issuer. Contact Rede."; break;
            case "54": retorno = "installments: Parameter not allowed for this transaction"; break;
            case "55": retorno = "cardHolderName: Invalid parameter size"; break;
            case "56": retorno = "Error in reported data. Try again."; break;
            case "57": retorno = "affiliation: Invalid merchant"; break;
            case "58": retorno = "Unauthorized. Contact issuer."; break;
            case "59": retorno = "cardHolderName: Invalid parameter format"; break;
            case "60": retorno = "street: Invalid parameter size"; break;
            case "61": retorno = "subscription: Invalid parameter format"; break;
            case "63": retorno = "softdescriptor: Not enabled for this merchant"; break;
            case "64": retorno = "Transaction not processed. Try again"; break;
            case "65": retorno = "token: Invalid token"; break;
            case "66": retorno = "departureTax: Invalid parameter size"; break;
            case "67": retorno = "departureTax: Invalid parameter format"; break;
            case "68": retorno = "departureTax: Required parameter missing"; break;
            case "69": retorno = "Transaction not allowed for this product or service."; break;
            case "70": retorno = "amount: Invalid parameter size"; break;
            case "71": retorno = "amount: Invalid parameter format"; break;
            case "72": retorno = "Contact issuer."; break;
            case "73": retorno = "amount: Required parameter missing"; break;
            case "74": retorno = "Communication failure. Try again"; break;
            case "75": retorno = "departureTax: Parameter should not be sent for this type of transaction"; break;
            case "76": retorno = "kind: Invalid parameter format"; break;
            case "78": retorno = "Transaction does not exist"; break;
            case "79": retorno = "Expired card. Transaction cannot be resubmitted. Contact issuer."; break;
            case "80": retorno = "Unauthorized. Contact issuer. (Insufficient funds)"; break;
            case "82": retorno = "Unauthorized transaction for debit card."; break;
            case "83": retorno = "Unauthorized. Contact issuer."; break;
            case "84": retorno = "Unauthorized. Transaction cannot be resubmitted. Contact issuer."; break;
            case "85": retorno = "complement: Invalid parameter size"; break;
            case "86": retorno = "Expired card"; break;

            case "87": retorno = "At least one of the following fields must be filled: tid or reference"; break;
            case "88": retorno = "Merchant not approved. Regulate your website and contact the Rede to return to transact."; break;
            case "89": retorno = "token: Invalid token"; break;
            case "97": retorno = "tid: Invalid parameter size"; break;
            case "98": retorno = "tid: Invalid parameter format"; break;
            case "150": retorno = "Timeout. Try again"; break;
            case "151": retorno = "installments: Greater than allowed"; break;
            case "153": retorno = "documentNumber: Invalid number"; break;
            case "154": retorno = "embedded: Invalid parameter format"; break;
            case "155": retorno = "eci: Required parameter missing"; break;
            case "156": retorno = "eci: Invalid parameter size"; break;
            case "157": retorno = "cavv: Required parameter missing"; break;
            case "158": retorno = "capture: Type not allowed for this transaction"; break;
            case "159": retorno = "userAgent: Invalid parameter size"; break;
            case "160": retorno = "urls: Required parameter missing (kind)"; break;
            case "161": retorno = "urls: Invalid parameter format"; break;
            case "167": retorno = "Invalid request JSON"; break;
            case "169": retorno = "Invalid Content-Type"; break;
            case "171": retorno = "Operation not allowed for this transaction"; break;
            case "172": retorno = "Transaction already captured"; break;
            case "173": retorno = "Authorization expired"; break;
            case "176": retorno = "urls: Required parameter missing (url)"; break;


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
            case "00": retorno = "Success"; break;
            case "101": retorno = "Unauthorized. Problems on the card, contact the issuer."; break;
            case "102": retorno = "Unauthorized. Check the situation of the store with the issuer."; break;
            case "103": retorno = "Unauthorized. Please try again."; break;
            case "104": retorno = "Unauthorized. Please try again."; break;
            case "105": retorno = "Unauthorized. Restricted card."; break;
            case "106": retorno = "Error in issuer processing. Please try again."; break;
            case "107": retorno = "Unauthorized. Please try again."; break;
            case "108": retorno = "Unauthorized. Value not allowed for this type of card."; break;
            case "109": retorno = "Unauthorized. Nonexistent card."; break;
            case "110": retorno = "Unauthorized. Transaction type not allowed for this card."; break;
            case "111": retorno = "Unauthorized. Insufficient funds."; break;
            case "112": retorno = "Unauthorized. Expiry date expired."; break;
            case "113": retorno = "Unauthorized. Identified moderate risk by the issuer."; break;
            case "114": retorno = "Unauthorized. The card does not belong to the payment network."; break;
            case "115": retorno = "Unauthorized. Exceeded the limit of transactions allowed in the period."; break;
            case "116": retorno = "Unauthorized. Please contact the Card Issuer."; break;
            case "117": retorno = "Transaction not found."; break;
            case "118": retorno = "Unauthorized. Card locked."; break;
            case "119": retorno = "Unauthorized. Invalid security code"; break;
            case "120": retorno = "Zero dollar transaction approved successfully."; break;
            case "121": retorno = "Error processing. Please try again."; break;
            case "122": retorno = "Transaction previously sent"; break;
            case "123": retorno = "Unauthorized. Bearer requested the end of the recurrences in the issuer."; break;
            case "124": retorno = "Unauthorized. Contact Rede"; break;

            case "01": retorno = "expirationYear: Invalid parameter size"; break;
            case "02": retorno = "expirationYear: Invalid parameter format"; break;
            case "03": retorno = "expirationYear: Required parameter missing"; break;
            case "04": retorno = "cavv: Invalid parameter size"; break;
            case "05": retorno = "cavv: Invalid parameter format"; break;
            case "06": retorno = "postalCode: Invalid parameter size"; break;
            case "07": retorno = "postalCode: Invalid parameter format "; break;
            case "08": retorno = "postalCode: Required parameter missing"; break;
            case "09": retorno = "complement: Invalid parameter size"; break;
            case "10": retorno = "complement: Invalid parameter format"; break;
            case "11": retorno = "departureTax: Invalid parameter format"; break;
            case "12": retorno = "documentNumber: Invalid parameter size"; break;
            case "13": retorno = "documentNumber: Invalid parameter format"; break;
            case "14": retorno = "documentNumber: Required parameter missing"; break;
            case "15": retorno = "securityCode: Invalid parameter size"; break;
            case "16": retorno = "securityCode: Invalid parameter format"; break;
            case "17": retorno = "distributorAffiliation: Invalid parameter size"; break;
            case "18": retorno = "distributorAffiliation: Invalid parameter format"; break;
            case "19": retorno = "xid: Invalid parameter size"; break;
            case "20": retorno = "eci: Invalid parameter format"; break;
            case "21": retorno = "xid: Required parameter for Visa card is missing"; break;
            case "22": retorno = "street: Required parameter missing"; break;
            case "23": retorno = "street: Invalid parameter format"; break;
            case "24": retorno = "affiliation: Invalid parameter size"; break;
            case "25": retorno = "affiliation: Invalid parameter format"; break;
            case "26": retorno = "affiliation: Required parameter missing"; break;
            case "27": retorno = "Parameter cavv or eci missing"; break;
            case "28": retorno = "code: Invalid parameter size"; break;
            case "29": retorno = "code: Invalid parameter format"; break;
            case "30": retorno = "code: Required parameter missing"; break;
            case "31": retorno = "softdescriptor: Invalid parameter size"; break;
            case "32": retorno = "softdescriptor: Invalid parameter format"; break;
            case "33": retorno = "expirationMonth: Invalid parameter format"; break;
            case "34": retorno = "code: Invalid parameter format"; break;
            case "35": retorno = "expirationMonth: Required parameter missing"; break;
            case "36": retorno = "cardNumber: Invalid parameter size"; break;
            case "37": retorno = "cardNumber: Invalid parameter format"; break;

            case "38": retorno = "cardNumber: Required parameter missing"; break;
            case "39": retorno = "reference: Invalid parameter size"; break;
            case "40": retorno = "reference: Invalid parameter format"; break;
            case "41": retorno = "reference: Required parameter missing"; break;
            case "42": retorno = "reference: Order number already exists"; break;
            case "43": retorno = "number: Invalid parameter size"; break;
            case "44": retorno = "number: Invalid parameter format"; break;
            case "45": retorno = "number: Required parameter missing"; break;
            case "46": retorno = "installments: Not correspond to authorization transaction"; break;
            case "47": retorno = "origin: Invalid parameter format"; break;
            case "49": retorno = "The value of the transaction exceeds the authorized"; break;
            case "50": retorno = "installments: Invalid parameter format"; break;
            case "51": retorno = "Product or service disabled for this merchant. Contact Rede"; break;
            case "53": retorno = "Transaction not allowed for the issuer. Contact Rede."; break;
            case "54": retorno = "installments: Parameter not allowed for this transaction"; break;
            case "55": retorno = "cardHolderName: Invalid parameter size"; break;
            case "56": retorno = "Error in reported data. Try again."; break;
            case "57": retorno = "affiliation: Invalid merchant"; break;
            case "58": retorno = "Unauthorized. Contact issuer."; break;
            case "59": retorno = "cardHolderName: Invalid parameter format"; break;
            case "60": retorno = "street: Invalid parameter size"; break;
            case "61": retorno = "subscription: Invalid parameter format"; break;
            case "63": retorno = "softdescriptor: Not enabled for this merchant"; break;
            case "64": retorno = "Transaction not processed. Try again"; break;
            case "65": retorno = "token: Invalid token"; break;
            case "66": retorno = "departureTax: Invalid parameter size"; break;
            case "67": retorno = "departureTax: Invalid parameter format"; break;
            case "68": retorno = "departureTax: Required parameter missing"; break;
            case "69": retorno = "Transaction not allowed for this product or service."; break;
            case "70": retorno = "amount: Invalid parameter size"; break;
            case "71": retorno = "amount: Invalid parameter format"; break;
            case "72": retorno = "Contact issuer."; break;
            case "73": retorno = "amount: Required parameter missing"; break;
            case "74": retorno = "Communication failure. Try again"; break;
            case "75": retorno = "departureTax: Parameter should not be sent for this type of transaction"; break;
            case "76": retorno = "kind: Invalid parameter format"; break;
            case "78": retorno = "Transaction does not exist"; break;
            case "79": retorno = "Expired card. Transaction cannot be resubmitted. Contact issuer."; break;
            case "80": retorno = "Unauthorized. Contact issuer. (Insufficient funds)"; break;
            case "82": retorno = "Unauthorized transaction for debit card."; break;
            case "83": retorno = "Unauthorized. Contact issuer."; break;
            case "84": retorno = "Unauthorized. Transaction cannot be resubmitted. Contact issuer."; break;
            case "85": retorno = "complement: Invalid parameter size"; break;
            case "86": retorno = "Expired card"; break;

            case "87": retorno = "At least one of the following fields must be filled: tid or reference"; break;
            case "88": retorno = "Merchant not approved. Regulate your website and contact the Rede to return to transact."; break;
            case "89": retorno = "token: Invalid token"; break;
            case "97": retorno = "tid: Invalid parameter size"; break;
            case "98": retorno = "tid: Invalid parameter format"; break;
            case "150": retorno = "Timeout. Try again"; break;
            case "151": retorno = "installments: Greater than allowed"; break;
            case "153": retorno = "documentNumber: Invalid number"; break;
            case "154": retorno = "embedded: Invalid parameter format"; break;
            case "155": retorno = "eci: Required parameter missing"; break;
            case "156": retorno = "eci: Invalid parameter size"; break;
            case "157": retorno = "cavv: Required parameter missing"; break;
            case "158": retorno = "capture: Type not allowed for this transaction"; break;
            case "159": retorno = "userAgent: Invalid parameter size"; break;
            case "160": retorno = "urls: Required parameter missing (kind)"; break;
            case "161": retorno = "urls: Invalid parameter format"; break;
            case "167": retorno = "Invalid request JSON"; break;
            case "169": retorno = "Invalid Content-Type"; break;
            case "171": retorno = "Operation not allowed for this transaction"; break;
            case "172": retorno = "Transaction already captured"; break;
            case "173": retorno = "Authorization expired"; break;
            case "176": retorno = "urls: Required parameter missing (url)"; break;

            default:
                retorno = "";
                break;
        }

        return retorno;
    }

    public void GerarParcelasCartao(Pedido prmPedido, String prmTID, SqlConnection prmCnn)
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

                    cmd = @"
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
                              Replace("@cdParcela", "'" + (i + 1).ToString().PadLeft(3, '0') + "'").
                              Replace("@vlParcela", vlrParcela.ToString().Replace(",", ".")).
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
            SessionCnn.Close();
        }


    }

}