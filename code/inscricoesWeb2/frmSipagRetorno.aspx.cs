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

using FM.Ecommerce.Sipag.Card;
using FM.Ecommerce.Sipag.Card.Configuration;
using FM.Ecommerce.Sipag.Card.Enums;
using FM.Ecommerce.Sipag.Card.Requests;
using FM.Ecommerce.Sipag.Card.Requests.Entities;
using FM.Ecommerce.Sipag.Card.Responses;
using FM.Ecommerce.Sipag.Card.Responses.Exceptions;

using System.Text.RegularExpressions;


public partial class frmSipagRetorno : System.Web.UI.Page
{

    Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();
    SqlConnection SessionCnn;

    Pedido SessionPedido;
    PedidoCad oPedidoCad = new PedidoCad();

    DataTable oDTAtividadesParticipante = new DataTable();

    Inscricoes SessionInscricoes = new Inscricoes();

    String SessionIdioma;

    private FM.Ecommerce.Sipag.Card.Responses.CreateTransactionResponse response;

    /// <summary>
    /// Quando o cliente for redirecionado de volta para a loja pelo Sipag, ele cairá nessa página,
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

            SessionCnn = (SqlConnection)Session["SessionCnn"];
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
                SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOWFHcDBZWEF3YjNkcWJpNWtZWFJoWW1GelpTNTNhVzVrYjNkekxtNWxkRHRKYm1sMGFXRnNJRU5oZEdGc2IyYzlaR0pmWlhabGJuUnZjMFpOTzFCbGNuTnBjM1FnVTJWamRYSnBkSGtnU1c1bWJ6MVVjblZsTzFWelpYSWdTVVE5UTI5dVlYTmxiWE03VUdGemMzZHZjbVE5UXpCdU5EVmxiVFV5TURFMg==")));

                //Site-producao - IP
                //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOU1UZzVMak00TGprd0xqRXdPVHRKYm1sMGFXRnNJRU5oZEdGc2IyYzlabUY2Wlc1a2IyMWhhWE03VUdWeWMybHpkQ0JUWldOMWNtbDBlU0JKYm1adlBWUnlkV1U3VlhObGNpQkpSRDFtWVhwbGJtUnZiV0ZwY3p0UVlYTnpkMjl5WkQxUmRUUTFOSEpHYlUxRVFRPT0=")));

                Session["SessionCnn"] = SessionCnn;

            }
                        
            Geral oGeral = new Geral();

            if (oGeral.verificarSiteManutencao("1", SessionCnn))
            {
                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                "05",
                                ""), true);
            }

            SessionIdioma = (String)Session["SessionIdioma"];
            if (SessionIdioma == null)
                SessionIdioma = "PTBR";
            Session["SessionIdioma"] = SessionIdioma;

            SessionEvento = (Evento)Session["SessionEvento"];
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

            SessionParticipante = (Participante)Session["SessionParticipante"];
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

            SessionPedido = (Pedido)Session["SessionPedido"];
            if (SessionPedido == null)
            {
                if ((Request.QueryString["pd"] != null) &&
                    (Request.QueryString["pd"] != ""))
                {
                    string cd_pedido = Request.QueryString["pd"].ToString();// cllEventos.Crypto.DecryptStringAES(Request.QueryString["pd"].ToString());

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
                    if ((Session["tid"] == null) || (Session["tid"].ToString() == ""))
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "Não foi possível concluir a transação!<br />Favor ligar para o Helpdesk e informar o Codigo de erro 001-328.";

                        pedidos_esq.Visible = false;
                        carrinho_pedidos.Visible = false;
                        itensDisponiveisGeral.Visible = false;
                        btnVoltar.Visible = true;
                        Session["tid"] = "";
                        Session["msgSipag"] = "";
                        Session["SipagResponse"] = null;
                        return;
                    }
                    else
                    {
                        //SipagService _SipagService;
                        //CustomSipagConfiguration _configuration;

                        //string appPath = HttpContext.Current.Request.ApplicationPath;
                        //string physicalPath = HttpContext.Current.Request.MapPath(appPath);

                        //_configuration = new CustomSipagConfiguration
                        //{
                        //    CurrencyId = SessionIdioma == "PTBR" ? "986" : "840",
                        //    CustomerId = SessionEvento.sipagConfig.CustomerID,//"WS2719020033._.1",
                        //    CustomerKey = SessionEvento.sipagConfig.CustomerKey,//"Ty19bUg31d",
                        //    CertificateClient = @physicalPath + "\\imagensgeral\\" + SessionEvento.CdEvento + "\\" + SessionEvento.sipagConfig.CustomerID + ".p12",
                        //    CertificateClientKey = SessionEvento.sipagConfig.CertificateClientKey, //"Uai8wRidMc",
                        //    Language = SessionIdioma == "PTBR" ? FM.Ecommerce.Sipag.Card.Enums.Language.Portuguese : FM.Ecommerce.Sipag.Card.Enums.Language.English,
                        //    //ReturnUrl = ""
                        //    ReturnUrl = tmpXpto + "/frmSipagRetorno.aspx?pd=" + SessionPedido.CdPedido + "&e=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + "&p=" + cllEventos.Crypto.EncryptStringAES(SessionParticipante.CdParticipante)
                        //};

                        //_SipagService = new SipagService(_configuration);

                       // var checkTransactionRequest = new CheckTransactionRequest((string)Session["tid"], _configuration);
                       // CheckTransactionResponse response = _SipagService.CheckTransaction(checkTransactionRequest);


                        response = (FM.Ecommerce.Sipag.Card.Responses.CreateTransactionResponse)Session["SipagResponse"];
                        string code = Regex.Match(response.ProcessorResponseCode, @"\d+").Value;                        

                        


                        //string code = (string)Session["codeResponseSipag"];
                        string msg = (string)Session["msgSipag"];

                        if (code != "00")
                        {
                            

                            lblMsg.Text = msg;

                            pedidos_esq.Visible = false;
                            carrinho_pedidos.Visible = false;
                            itensDisponiveisGeral.Visible = false;
                            btnVoltar.Visible = true;
                            Session["tid"] = "";
                            Session["msgSipag"] = "";
                            Session["SipagResponse"] = null;
                            return;
                        }
                        else
                        {

                            if (SessionIdioma == "PTBR")
                                lblMsg.Text = "Operação ralizada com sucesso";
                            else if (SessionIdioma == "ENUS")
                                lblMsg.Text = "Operation was successful";
                            else if (SessionIdioma == "ESP")
                                lblMsg.Text = "Operación fue un éxito";
                            else if (SessionIdioma == "FRA")
                                lblMsg.Text = "Opération a réussi";





                            oPedidoCad.MarcarPedidoComoPago(SessionPedido, SessionCnn);


                            SessionPedido.TpPagamento = "CARTÃO DE CRÉDITO";
                            SessionPedido.NoBandeira = response.Brand.ToUpper();
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
                                       SessionPedido.VlTotalPedido,
                                       tmpVlDescontoCupom,
                                       0,
                                       SessionPedido.TpPagamento,
                                       "Aut: " + response.ProcessorApprovalCode,
                                       "Cartão - " + response.Brand.ToUpper() + "\n" +
                                       "Transação - " + (string)Session["tid"] + "\n" +
                                       "Autorização - " + response.ProcessorApprovalCode + "\n" +
                                       "Nr Parcelas - " + response.Parcelas +
                                       (tmpDsCupomDesconto != "" ? "\nCupom Desconto - " + tmpDsCupomDesconto : ""),
                                       "000000001",
                                       "",
                                       SessionPedido.VlTotalPedido - tmpVlDescontoCupom,
                                       "",
                                       0,
                                       "",
                                       SessionCnn);


                            oGeral.EnviarEmailPedidoRecebCartao(SessionEvento, SessionParticipante, SessionPedido, SessionCnn);
                            oGeral.EnviarEmailConfirmaCadastro(SessionEvento, SessionParticipante, SessionCnn);


                            Session["tid"] = "";
                            Session["msgSipag"] = "";
                            Session["SipagResponse"] = null;
                        }
                    }
                }
                catch
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Não foi possível concluir a transação!<br />Favor ligar para o Helpdesk e informar o Codigo de erro 001-435.";

                    pedidos_esq.Visible = false;
                    carrinho_pedidos.Visible = false;
                    itensDisponiveisGeral.Visible = false;
                    btnVoltar.Visible = true;

                    Session["tid"] = "";
                    Session["msgSipag"] = "";
                    Session["SipagResponse"] = null;
                    return;
                }
            }
            btnOk.Visible = true;

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
            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            SessionPedido = (Pedido)Session["SessionPedido"];

            SessionIdioma = (String)Session["SessionIdioma"];

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
            lblResVlrTotal.Text = "Total";

            btnVoltar.Text = "Back";

            lblTituloGrid1.Text = "Items Request";
        }
        else if (prmIdioma == "ESP")
        {
            SessionIdioma = "ESP";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Resumen de Pago";
            lblTituloResumo.Text = "Resumen de la solicitud";

            lblPart.Text = "Participante:";
            lblCateg.Text = "Categoría";

            lblResPed.Text = "Solicitud Nº";
            lblResItens.Text = "Artículos";
            lblResVlr.Text = "Valor";
            lblResDesc.Text = "Descuento";
            lblResVlrTotal.Text = "Total";

            btnVoltar.Text = "Volver";

            lblTituloGrid1.Text = "Artículos";
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
                                    DateTime.Parse(DTAtividadesp.DefaultView[i]["dtTermino"].ToString().Trim()).ToString("dd/MM/yyyy HH:mm"),
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

                vlTotalAtiv.Text = (decimal.Parse(vlTotalAtiv.Text) + (decimal.Parse(DTAtividadesp.DefaultView[i]["vlAtividade"].ToString().Trim()) *
                                                                       int.Parse(DTAtividadesp.DefaultView[i]["vlQuantidade"].ToString()))
                                   ).ToString("N2");
                vlTotalDesc.Text = (decimal.Parse(vlTotalDesc.Text) + (decimal.Parse(DTAtividadesp.DefaultView[i]["vlDesconto"].ToString().Trim()) *
                                                                       int.Parse(DTAtividadesp.DefaultView[i]["vlQuantidade"].ToString()))
                                   ).ToString("N2");
                vlTotalPedido.Text = (decimal.Parse(vlTotalPedido.Text) + decimal.Parse(DTAtividadesp.DefaultView[i]["vlMatricula"].ToString().Trim())).ToString("N2");


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
            

            Label lbldtini = (Label)e.Row.FindControl("lblDtIni");
            lbldtini.Text = DateTime.Parse(lbldtini.Text).ToString("dd/MM/yyyy HH:mm");
            Label lbldttermino = (Label)e.Row.FindControl("lblDtTermino");
            lbldttermino.Text = DateTime.Parse(lbldttermino.Text).ToString("dd/MM/yyyy HH:mm");

            Label lbltpItem = (Label)e.Row.FindControl("lblTpItem");
            Label lbldeItem = (Label)e.Row.FindControl("lblDeItem");
            Label lblateItem = (Label)e.Row.FindControl("lblAteItem");
            Label lblvagasItem = (Label)e.Row.FindControl("lblVagasItem");
            Label lbllocalItem = (Label)e.Row.FindControl("lblLocalItem");

            Label lblvalorItem = (Label)e.Row.FindControl("lblValorItem");
            Label lbldescItem = (Label)e.Row.FindControl("lblDescItem");
            Label lblqtdItem = (Label)e.Row.FindControl("lblQtdItem");
            Label lblvlTotalItem = (Label)e.Row.FindControl("lblVlrTotalItem");

            Label lblLocal = (Label)e.Row.FindControl("lblLocal");
            Label lblVagas = (Label)e.Row.FindControl("lblVagas");

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

            Label lblatv = (Label)e.Row.FindControl("lblVlAtividade");
            Label lbldec = (Label)e.Row.FindControl("lblVlDescontoReal");
            lblatv.Font.Strikeout = (decimal.Parse(lbldec.Text) > 0);

            Image imgprofatv = (Image)e.Row.FindControl("imgAtivProf");
            imgprofatv.Visible = (imgprofatv.ImageUrl != "");

            if ((SessionEvento.CdCliente == "0013") || (SessionEvento.CdCliente == "0003") || (SessionEvento.CdEvento == "002902") || (SessionEvento.CdEvento == "003401"))
            {

                Panel pnlresumovlritens = (Panel)e.Row.FindControl("pnlResumoVlrItens");
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
    
}