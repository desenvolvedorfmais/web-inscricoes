using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using cllEventos;
using CLLFuncoes;

using Itaucripto;

using System.Data;
using System.Data.SqlClient;

using System.IO;

using AjaxControlToolkit;
using PayPal.Api;
using System.Diagnostics;
using Utilities;


public partial class frmPayPalPagamento : BaseSamplePage
{
    private Order order;
    private Amount amount;


    Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    SqlConnection SessionCnn;

    Pedido SessionPedido;
    PedidoCad oPedidoCad = new PedidoCad();


    String SessionIdioma;

    protected override void RunSample()
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

            if (SessionParticipante == null)
                SessionParticipante = (Participante)Session["SessionParticipante"];
            else
                Session["SessionParticipante"] = SessionParticipante;

            if (SessionEvento == null)
                SessionEvento = (Evento)Session["SessionEvento"];
            else
                Session["SessionEvento"] = SessionEvento;

            if (SessionEvento == null)
            {
                //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                //                "003",
                //                oEventoCad.RcMsg), true);

                Server.Transfer("frmSessaoExpirada.aspx", true);
            }

            lblIdentificador.Text = SessionParticipante.CdParticipante.Trim();
            lblNoParticipante.Text = SessionParticipante.NoParticipante.Trim();
            lblCategoria.Text = SessionParticipante.Categoria.NoCategoria.Trim();

            if (SessionEvento.CdEvento == "007002")
            {
                lblNoParticipante.Text = SessionParticipante.DsAuxiliar2 + ", " + SessionParticipante.NoParticipante.Trim();
            }

            if (SessionPedido == null)
                SessionPedido = (Pedido)Session["SessionPedido"];
            else
                Session["SessionPedido"] = SessionPedido;

            //SessionPedido = oPedidoCad.VerificaPedidoNaoPagoWeb(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);
            //Session["SessionPedido"] = SessionPedido;
            if (SessionPedido != null)
            {
                //if (SessionPedido.TpPagamento.Trim() != "")
                //{
                //    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                //                    "015",
                //                    ""), true);
                //}

                lblNrPedido.Text = SessionPedido.CdPedido;

                //DataTable dtpedido = oPedidoCad.TotalizarPedido(SessionPedido.CdPedido, SessionCnn);
                //if (dtpedido != null)
                //{
                //    vlItens.Text = dtpedido.DefaultView[0]["itens"].ToString();
                //    vlTotalAtiv.Text = decimal.Parse(dtpedido.DefaultView[0]["tol_Atv"].ToString()).ToString("N2");
                //    vlTotalDesc.Text = decimal.Parse(dtpedido.DefaultView[0]["tot_desc"].ToString()).ToString("N2");

                //    vlTotalAtiv.Font.Strikeout = (decimal.Parse(vlTotalDesc.Text) > 0);
                //}

                //vlTotalPedido.Text = SessionPedido.VlTotalPedido.ToString("N2");

                decimal vlDesconto = 0;

                DescontoCupom oDescontoCupom;
                DescontoCupomCad oDescontoCupomCad = new DescontoCupomCad();

                if ((SessionPedido.pedidoCupomDesconto != null) && (SessionPedido.pedidoCupomDesconto.CdCupomDesconto != ""))
                {


                    oDescontoCupom = oDescontoCupomCad.PesquisarPorCupom(SessionEvento.CdEvento, SessionPedido.pedidoCupomDesconto.DsCupomDesconto.ToUpper(), SessionCnn);

                    if (oDescontoCupom != null)
                    {
                        vlDesconto = oDescontoCupom.VlDesconto;

                        if (oDescontoCupom.TpDesconto.ToUpper() != "REAL")
                        {
                            vlDesconto = Math.Round(SessionPedido.VlTotalPedido * (vlDesconto / 100), 2);
                        }

                    }


                }

                DataTable dtpedido = oPedidoCad.TotalizarPedido(SessionPedido.CdPedido, SessionCnn);
                if (dtpedido != null)
                {
                    vlItens.Text = dtpedido.DefaultView[0]["itens"].ToString();
                    vlTotalAtiv.Text = decimal.Parse(dtpedido.DefaultView[0]["tol_Atv"].ToString()).ToString("N2");
                    vlTotalDesc.Text = (decimal.Parse(dtpedido.DefaultView[0]["tot_desc"].ToString()) + vlDesconto).ToString("N2");

                    vlTotalAtiv.Font.Strikeout = (decimal.Parse(vlTotalDesc.Text) > 0);
                }

                vlTotalPedido.Text = (SessionPedido.VlTotalPedido - vlDesconto).ToString("N2");

            }




        }
        else
        {
            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            SessionPedido = (Pedido)Session["SessionPedido"];

            SessionIdioma = (String)Session["SessionIdioma"];

            if (SessionEvento == null)
                Server.Transfer("frmSessaoExpirada.aspx", true);

        }

        verificarIdioma(SessionIdioma);

        TSManager1.RegisterPostBackControl(btnContinuarPayPal);


        
        //}
        //else
        //{
        //    var guid = Request.Params["guid"];
        //    // ^ Ignore workflow code segment
        //    #region Track Workflow
        //    this.flow = Session["flow-" + guid] as RequestFlow;
        //    this.RegisterSampleRequestFlow();
        //    this.flow.RecordApproval("Order payment approved successfully.");
        //    #endregion

        //    // Execute the order
        //    var paymentId = Session[guid] as string;
        //    var paymentExecution = new PaymentExecution() { payer_id = payerId };
        //    var payment = new Payment() { id = paymentId };

        //    // ^ Ignore workflow code segment
        //    #region Track Workflow
        //    flow.AddNewRequest("Execute payment", payment);
        //    #endregion

        //    // Execute the order payment.
        //    var executedPayment = payment.Execute(apiContext, paymentExecution);

        //    // ^ Ignore workflow code segment
        //    #region Track Workflow
        //    flow.RecordResponse(executedPayment);
        //    #endregion

        //    // Get the information about the executed order from the returned payment object.
        //    this.order = executedPayment.transactions[0].related_resources[0].order;
        //    this.amount = executedPayment.transactions[0].amount;

        //    // Once the order has been executed, an order ID is returned that can be used
        //    // to do one of the following:
        //    this.AuthorizeOrder(apiContext);
        //    this.CaptureOrder(apiContext);
        //    this.VoidOrder(apiContext);
        //    //this.RefundOrder(apiContext);

        //    // For more information, please visit [PayPal Developer REST API Reference](https://developer.paypal.com/docs/api/).
        //}
    }

    /// <summary>
    /// Authorizes an order. This begins the process of confirming that
    /// funds are available until it is time to complete the payment
    /// transaction.
    /// 
    /// More Information:
    /// https://developer.paypal.com/webapps/developer/docs/integration/direct/create-process-order/#authorize-an-order
    /// </summary>
    private void AuthorizeOrder(APIContext apiContext)
    {
        this.order.Authorize(apiContext);
    }

    /// <summary>
    /// Captures an order. For a partial capture, you can provide a lower
    /// amount than the total payment. Additionally, you can explicitly
    /// indicate a final capture (complete the transaction and prevent
    /// future captures) by setting the is_final_capture value to true.
    /// 
    /// More Information:
    /// https://developer.paypal.com/webapps/developer/docs/integration/direct/create-process-order/#capture-an-order
    /// </summary>
    private void CaptureOrder(APIContext apiContext)
    {
        var capture = new Capture();
        capture.amount = this.amount;
        capture.is_final_capture = true;
        this.order.Capture(apiContext, capture);
    }

    /// <summary>
    /// Voids an order.
    /// 
    /// NOTE: An order cannot be voided if payment has already been
    ///       partially or fully captured.
    /// 
    /// More Information:
    /// https://developer.paypal.com/webapps/developer/docs/api/#void-an-order
    /// </summary>
    private void VoidOrder(APIContext apiContext)
    {
        this.order.Void(apiContext);
    }

    


    protected void verificarIdioma(string prmIdioma)
    {
        if (prmIdioma == "PTBR")
        {
            SessionIdioma = "PTBR";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Matrículas / Inscrições";
            lblTituloResumo.Text = "Resumo do Pedido";

            lblID.Text = "Registration no.";
            lblPart.Text = "Participante:";
            lblCateg.Text = "Categoria:";

            lblResPed.Text = "Pedido nº";
            lblResItens.Text = "Itens";
            lblResVlr.Text = "Valor";
            lblResDesc.Text = "Desconto";
            lblResVlrTotal.Text = "Total";

            lblMsgParticipante.Text =
                "Prezado participante, o PayPal é a forma escolhida para pagamento.<br /> " +
                "Para concluir sua inscrição você será direcionado para o PayPal, onde poderá selecionar a melhor forma de pagamento para você.";

            btnContinuarPayPal.Text = "Continuar";

        }
        else if (prmIdioma == "ENUS")
        {
            SessionIdioma = "ENUS";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Payment";
            lblTituloResumo.Text = "Order summary";

            lblPart.Text = "Participant:";
            lblCateg.Text = "Category:";

            lblResPed.Text = "Order No.";
            lblResItens.Text = "Items";
            lblResVlr.Text = "Value";
            lblResDesc.Text = "Discount";
            lblResVlrTotal.Text = "Total(USD)";

            if (SessionParticipante.Categoria.NoCategoria.Contains("BRAZILIAN"))
                lblResVlrTotal.Text = "Total(R$)";

            lblMsgParticipante.Text =
                "Dear participant, PayPal is the way chosen to manage our collections. <br />" +
                "To complete your registration you will be directed to PayPal where you can select the best payment method for you.";

            btnContinuarPayPal.Text = "Continue";
        }
        else if (prmIdioma == "ESP")
        {
            SessionIdioma = "ESP";
            Session["SessionIdioma"] = SessionIdioma;


            lblTituloPagina.Text = "El pago";
            lblTituloResumo.Text = "Resumen de la solicitud";

            lblID.Text = "Nº de Registro:";
            lblPart.Text = "Participante:";
            lblCateg.Text = "Categoría";

            lblResPed.Text = "Nº de Pedido";
            lblResItens.Text = "Itens";
            lblResVlr.Text = "Valor";
            lblResDesc.Text = "Descuento";
            lblResVlrTotal.Text = "Total(USD)";

            lblMsgParticipante.Text =
                "Estimado participante, PayPal es la forma elegida para administrar nuestras colecciones.<br />" +
                "Para completar su inscripción será dirigido a PayPal, donde puede seleccionar el mejor método de pago para usted.";

            btnContinuarPayPal.Text = "Continuar";
        }
        else if (prmIdioma == "FRA")
        {
            SessionIdioma = "FRA";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Paiement";
            lblTituloResumo.Text = "Résumé de la demande";

            lblPart.Text = "Participant:";
            lblCateg.Text = "Catégorie:";

            lblResPed.Text = "Demande no";
            lblResItens.Text = "Articles";
            lblResVlr.Text = "Valeur";
            lblResDesc.Text = "Réduction";
            lblResVlrTotal.Text = "Total";

            lblMsgParticipante.Text =
                "Cher participant, PayPal est le moyen choisi pour gérer nos collections.<br />" +
                "Pour compléter votre inscription, vous serez dirigé vers PayPal, où vous pouvez choisir la meilleure méthode de paiement pour vous.";

            btnContinuarPayPal.Text = "Continuer";
        }
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        //ambinte de teste
        //string email = "valdo_1342267021_biz_api1.hotmail.com";
        //string password = "1342267044";
        //string apiKey = "A2yXYR.rnKfmdjsFw84g8BDHdBsVAzDbb-edCSs1NczPjV3dUqEJrJmc";
        //string URLRetornoOK = "http://localhost:4301/InscricoesWeb/frmPayPalRetornoOK.aspx?pd=" + cllEventos.Crypto.EncryptStringAES(SessionPedido.CdPedido) + "&e=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + "&p=" + cllEventos.Crypto.EncryptStringAES(SessionParticipante.CdParticipante);
        //string UrlRetornoCancel = "http://localhost:4301/InscricoesWeb/frmPayPalRetornoCancel.aspx?pd=" + cllEventos.Crypto.EncryptStringAES(SessionPedido.CdPedido) + "&e=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + "&p=" + cllEventos.Crypto.EncryptStringAES(SessionParticipante.CdParticipante);

        //ambiente de produção
        string email = SessionEvento.payPalConfig.ContaPP;// 
        string password = SessionEvento.payPalConfig.SenhaPP;// 
        string apiKey = SessionEvento.payPalConfig.CodAcessoPP;// 

        string urlSite = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);

        //string URLRetornoOK = "https://inscricoesweb.fazendomais.com/frmPayPalRetornoOK.aspx?pd=" + SessionPedido.CdPedido + "&e=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + "&p=" + cllEventos.Crypto.EncryptStringAES(SessionParticipante.CdParticipante);
        //string UrlRetornoCancel = "https://inscricoesweb.fazendomais.com/frmPayPalRetornoCancel.aspx?pd=" + SessionPedido.CdPedido + "&e=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + "&p=" + cllEventos.Crypto.EncryptStringAES(SessionParticipante.CdParticipante);


        string URLRetornoOK = urlSite + "/frmPayPalRetornoOK.aspx?pd=" + SessionPedido.CdPedido + "&e=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + "&p=" + cllEventos.Crypto.EncryptStringAES(SessionParticipante.CdParticipante);
        string UrlRetornoCancel = urlSite + "/frmPayPalRetornoCancel.aspx?pd=" + SessionPedido.CdPedido + "&e=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + "&p=" + cllEventos.Crypto.EncryptStringAES(SessionParticipante.CdParticipante);

        string codMoeda;

        if (SessionEvento.CdEvento != "007002")
        {
            if ((SessionParticipante.NoPais.Trim() == "") || (SessionParticipante.NoPais.ToUpper() == "BRASIL") || (SessionParticipante.NoPais == "BRAZIL") || (SessionParticipante.NoPais == "BRASIL") || (SessionParticipante.NoPais == "BRÉSIL"))
                codMoeda = "BRL";
            else
                codMoeda = "USD";
        }
        else
        {
            //if ((SessionParticipante.CdCategoria == "00700201") || (SessionParticipante.CdCategoria == "00700202") || (SessionParticipante.CdCategoria == "00700207"))
            if ((SessionParticipante.Categoria.NoCategoria.Contains("BRAZILIAN")) || ((SessionEvento.CdEvento == "007002") && (SessionParticipante.CdParticipante == "000000001")))
                codMoeda = "BRL";
            else
                codMoeda = "USD";
        }

        decimal tmpVlDescontoCupom = 0;
        if (SessionPedido.pedidoCupomDesconto != null)
            tmpVlDescontoCupom = SessionPedido.pedidoCupomDesconto.VlDescontoCalculado;

        //Response.Redirect(PayPal.Checkout.start(
        //    AmbienteExec.PRODUCAO,
        //    email,
        //    password,
        //    apiKey,
        //    URLRetornoOK,
        //    UrlRetornoCancel,
        //    codMoeda,//PayPal.Enum.CurrencyCode.BRAZILIAN_REAL,
        //    ((SessionIdioma == "PTBR") || (SessionParticipante.NoPais.ToUpper() == "BRASIL") || (SessionParticipante.NoPais == "BRAZIL") || (SessionParticipante.NoPais == "BRASIL") || (SessionParticipante.NoPais == "BRÉSIL")) ? PayPal.Enum.LocaleCode.BRAZIL : SessionIdioma == "ENUS" ? PayPal.Enum.LocaleCode.UNITED_STATES : SessionIdioma == "ESP" ? PayPal.Enum.LocaleCode.SPAIN : PayPal.Enum.LocaleCode.FRANCE,
        //    SessionEvento.NoEvento,
        //    1,
        //    double.Parse((SessionPedido.VlTotalPedido - tmpVlDescontoCupom - SessionPedido.VlPIS - SessionPedido.VlCOFINS - SessionPedido.VlISS).ToString()),
        //    SessionEvento.NoEvento,
        //    ""//http://www.fazendomais.com/imagensgeral/WHO-FIC - Marca.jpg
        //));



        // ### Api Context
        // Pass in a `APIContext` object to authenticate 
        // the call and to send a unique request id 
        // (that ensures idempotency). The SDK generates
        // a request id if you do not pass one explicitly. 
        // See [Configuration.cs](/Source/Configuration.html) to know more about APIContext.

        //BIOMINAS
        Configuration.ClientId = "Aem2euFGP-x6qj4ZsgMNz96IKCKwd_Lx841MnbG6raLlq0ePddA889S6tdOpZMhAg1G6tZEIO_OesWcG";
        Configuration.ClientSecret = "EN9n5D-Tr5ZiW72VNIRUqrmQtw5j4m0px1TC7zbyv8ohz4J4ioutz74Mc3R0BbL9OYhjzQQwJfH-nV59";

        //fmais
        //Configuration.ClientId = "AW81vl1GNwWdFfSSOZRkAVBbu_v7DADq9Av-VR3jFfWhVGKXEf04l1y75n8LUc1id1Vnc8OPkQexzALb";
        //Configuration.ClientSecret = "EMX9lWon4R-oCm8Pnr5ZJl_GlYPghlzCtUG_N02F2Fehqch1dDlPpxAOCe2DOOt9-P4N08SrLBwEcoV4";

        var apiContext = Configuration.GetAPIContext();

       
        
            // ###Payer
            // A resource representing a Payer that funds a payment
            // Payment Method
            // as `paypal`
            var payer = new Payer() { payment_method = "paypal" };

        // # Redirect URLS
        //string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/frmPayPalPagamento.aspx?"; ;
        //var guid = Convert.ToString((new Random()).Next(100000));
        //var redirectUrl = baseURI; //+ "guid=" + guid;


        // ###Amount
        // Lets you specify a payment amount.
        var amount = new Amount()
        {
            currency = codMoeda,
            total = (SessionPedido.VlTotalPedido - tmpVlDescontoCupom - SessionPedido.VlPIS - SessionPedido.VlCOFINS - SessionPedido.VlISS).ToString().Replace(".", "").Replace(",",".")
        };
        

            // ###Transaction
            // A transaction defines the contract of a
            // payment - what is the payment for and who
            // is fulfilling it. 
            var transactionList = new List<Transaction>();

            // The Payment creation API requires a list of
            // Transaction; add the created `Transaction`
            // to a List
            transactionList.Add(new Transaction()
            {
                description = SessionEvento.NoEvento,
                amount = amount
            });
            var redirUrls = new RedirectUrls()
            {
                cancel_url = UrlRetornoCancel,
                return_url = URLRetornoOK
            };
            // ###Payment
            // Create a payment with the intent set to 'order'
            var payment = new Payment()
            {
                intent = "order",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            // ^ Ignore workflow code segment
            #region Track Workflow
            flow.AddNewRequest("Create payment order", payment);
            #endregion

            // Create the payment resource.
            var createdPayment = payment.Create(apiContext);

            // ^ Ignore workflow code segment
            #region Track Workflow
            flow.RecordResponse(createdPayment);
            #endregion

            // Use the `approval_url` link provided by the returned object to approve the order payment.
            var links = createdPayment.links.GetEnumerator();
        var urlSandbox = string.Empty;
            while (links.MoveNext())
            {
                var link = links.Current;
                if (link.rel.ToLower().Trim().Equals("approval_url"))
                {
                urlSandbox = link.href;
                    this.flow.RecordRedirectUrl("Redirect to PayPal to approve the order...", link.href);
                }
            }
        //Session.Add("flow-" + guid, this.flow);
        //Session.Add(guid, createdPayment.id);
        SessionPedido.CdTransacaoOutrosTpPgto = createdPayment.id;
        SessionPedido = oPedidoCad.Gravar(SessionPedido, SessionCnn);
                        Session["SessionPedido"] = SessionPedido;

        Response.Redirect(urlSandbox, true);

        //Debug.WriteLine("Valor Pay Pal ID" + Session[guid]);
    }

}

//public partial class frmPayPalPagamento : System.Web.UI.Page
//{
//    Participante SessionParticipante;
//    ParticipanteCad oParticipanteCad = new ParticipanteCad();
//    Evento SessionEvento;
//    EventoCad oEventoCad = new EventoCad();

//    ClsFuncoes oClsFuncoes = new ClsFuncoes();

//    SqlConnection SessionCnn;

//    Pedido SessionPedido;
//    PedidoCad oPedidoCad = new PedidoCad();


//    String SessionIdioma;


//    protected void Page_Load(object sender, EventArgs e)
//    {
//        if (!Page.IsPostBack)
//        {

//            if (SessionCnn == null)
//                SessionCnn = (SqlConnection)Session["SessionCnn"];
//            else
//                Session["SessionCnn"] = SessionCnn;

//            SessionIdioma = (String)Session["SessionIdioma"];
//            if (SessionIdioma == null)
//                SessionIdioma = "PTBR";
//            Session["SessionIdioma"] = SessionIdioma;

//            if (SessionParticipante == null)
//                SessionParticipante = (Participante)Session["SessionParticipante"];
//            else
//                Session["SessionParticipante"] = SessionParticipante;

//            if (SessionEvento == null)
//                SessionEvento = (Evento)Session["SessionEvento"];
//            else
//                Session["SessionEvento"] = SessionEvento;

//            if (SessionEvento == null)
//            {
//                //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
//                //                "003",
//                //                oEventoCad.RcMsg), true);

//                Server.Transfer("frmSessaoExpirada.aspx", true);
//            }

//            lblIdentificador.Text = SessionParticipante.CdParticipante.Trim();
//            lblNoParticipante.Text = SessionParticipante.NoParticipante.Trim();
//            lblCategoria.Text = SessionParticipante.Categoria.NoCategoria.Trim();

//            if (SessionEvento.CdEvento == "007002")
//            {
//                lblNoParticipante.Text = SessionParticipante.DsAuxiliar2 + ", " + SessionParticipante.NoParticipante.Trim();
//            }

//            if (SessionPedido == null)
//                SessionPedido = (Pedido)Session["SessionPedido"];
//            else
//                Session["SessionPedido"] = SessionPedido;

//            //SessionPedido = oPedidoCad.VerificaPedidoNaoPagoWeb(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);
//            //Session["SessionPedido"] = SessionPedido;
//            if (SessionPedido != null)
//            {
//                //if (SessionPedido.TpPagamento.Trim() != "")
//                //{
//                //    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
//                //                    "015",
//                //                    ""), true);
//                //}

//                lblNrPedido.Text = SessionPedido.CdPedido;

//                //DataTable dtpedido = oPedidoCad.TotalizarPedido(SessionPedido.CdPedido, SessionCnn);
//                //if (dtpedido != null)
//                //{
//                //    vlItens.Text = dtpedido.DefaultView[0]["itens"].ToString();
//                //    vlTotalAtiv.Text = decimal.Parse(dtpedido.DefaultView[0]["tol_Atv"].ToString()).ToString("N2");
//                //    vlTotalDesc.Text = decimal.Parse(dtpedido.DefaultView[0]["tot_desc"].ToString()).ToString("N2");

//                //    vlTotalAtiv.Font.Strikeout = (decimal.Parse(vlTotalDesc.Text) > 0);
//                //}

//                //vlTotalPedido.Text = SessionPedido.VlTotalPedido.ToString("N2");

//                decimal vlDesconto = 0;

//                DescontoCupom oDescontoCupom;
//                DescontoCupomCad oDescontoCupomCad = new DescontoCupomCad();

//                if ((SessionPedido.pedidoCupomDesconto != null) && (SessionPedido.pedidoCupomDesconto.CdCupomDesconto != ""))
//                {


//                    oDescontoCupom = oDescontoCupomCad.PesquisarPorCupom(SessionEvento.CdEvento, SessionPedido.pedidoCupomDesconto.DsCupomDesconto.ToUpper(), SessionCnn);

//                    if (oDescontoCupom != null)
//                    {
//                        vlDesconto = oDescontoCupom.VlDesconto;

//                        if (oDescontoCupom.TpDesconto.ToUpper() != "REAL")
//                        {
//                            vlDesconto = Math.Round(SessionPedido.VlTotalPedido * (vlDesconto / 100), 2);
//                        }

//                    }


//                }

//                DataTable dtpedido = oPedidoCad.TotalizarPedido(SessionPedido.CdPedido, SessionCnn);
//                if (dtpedido != null)
//                {
//                    vlItens.Text = dtpedido.DefaultView[0]["itens"].ToString();
//                    vlTotalAtiv.Text = decimal.Parse(dtpedido.DefaultView[0]["tol_Atv"].ToString()).ToString("N2");
//                    vlTotalDesc.Text = (decimal.Parse(dtpedido.DefaultView[0]["tot_desc"].ToString()) + vlDesconto).ToString("N2");

//                    vlTotalAtiv.Font.Strikeout = (decimal.Parse(vlTotalDesc.Text) > 0);
//                }

//                vlTotalPedido.Text = (SessionPedido.VlTotalPedido - vlDesconto).ToString("N2");

//            }




//        }
//        else
//        {
//            SessionParticipante = (Participante)Session["SessionParticipante"];

//            SessionEvento = (Evento)Session["SessionEvento"];

//            SessionCnn = (SqlConnection)Session["SessionCnn"];

//            SessionPedido = (Pedido)Session["SessionPedido"];

//            SessionIdioma = (String)Session["SessionIdioma"];

//            if (SessionEvento == null)
//                Server.Transfer("frmSessaoExpirada.aspx", true);

//        }

//        verificarIdioma(SessionIdioma);

//        TSManager1.RegisterPostBackControl(btnContinuar);
//    }


//}