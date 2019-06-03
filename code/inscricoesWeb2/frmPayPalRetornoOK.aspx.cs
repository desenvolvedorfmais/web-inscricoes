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

using System.Collections.Specialized;
using PayPal;
using PayPal.Api;
using System.Diagnostics;
using Utilities;

public partial class frmPayPalRetornoOK: BaseSamplePage
{
    private Order order;
    private Amount amount;


    Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();
    SqlConnection SessionCnn;

    Pedido SessionPedido;
    PedidoCad oPedidoCad = new PedidoCad();

    String SessionIdioma;

    /// <summary>
    /// Quando o cliente for redirecionado de volta para a loja pelo PayPal, ele cairá nessa página,
    /// onde chamaremos o método finalize da classe Checkout informando o token e PayerID enviados
    /// pelo PayPal.
    /// </summary>
    /// <param name='sender'>
    /// Sender.
    /// </param>
    /// <param name='e'>
    /// E.
    /// </param>

    protected override void RunSample()
    {
        //BIOMINAS
        Configuration.ClientId = "Aem2euFGP-x6qj4ZsgMNz96IKCKwd_Lx841MnbG6raLlq0ePddA889S6tdOpZMhAg1G6tZEIO_OesWcG";
        Configuration.ClientSecret = "EN9n5D-Tr5ZiW72VNIRUqrmQtw5j4m0px1TC7zbyv8ohz4J4ioutz74Mc3R0BbL9OYhjzQQwJfH-nV59";

        //fmais
        //Configuration.ClientId = "AW81vl1GNwWdFfSSOZRkAVBbu_v7DADq9Av-VR3jFfWhVGKXEf04l1y75n8LUc1id1Vnc8OPkQexzALb";
        //Configuration.ClientSecret = "EMX9lWon4R-oCm8Pnr5ZJl_GlYPghlzCtUG_N02F2Fehqch1dDlPpxAOCe2DOOt9-P4N08SrLBwEcoV4";
        var apiContext = Configuration.GetAPIContext();
        
        


      



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

            //if (oGeral.verificarSiteManutencao("1", SessionCnn))
            //{
            //    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
            //                    "05",
            //                    ""), true);
            //}

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

            string token = Request.QueryString["token"];
            string PayerID = Request.QueryString["PayerID"];



            //string email = "valdo_1342267021_biz_api1.hotmail.com";
            //string password = "1342267044";
            //string apiKey = "A2yXYR.rnKfmdjsFw84g8BDHdBsVAzDbb-edCSs1NczPjV3dUqEJrJmc";

            string email = SessionEvento.payPalConfig.ContaPP;// "valdoalves_api1.fazendomais.com.br";
            string password = SessionEvento.payPalConfig.SenhaPP;// "MUWTTN6FLEMNVDQF";
            string apiKey = SessionEvento.payPalConfig.CodAcessoPP;// "AvR0zmpDVFZxrrck1gVQuyMMq6TUAC2yxyfF8OwCxntCQGqda7aaWJKN";


            String codMoeda;

            if ((SessionParticipante.NoPais.Trim() == "") || (SessionParticipante.NoPais.ToUpper() == "BRASIL"))
                codMoeda = "BRL";
            else
                codMoeda = "USD";


            ////O método finalize, da classe Checkout, vai retornar um NameValueCollection com os dados
            ////obtidos na chamada à operação GetExpressCheckoutDetails. Utilizamos esses dados para
            ////popular a base de dados com informações do cliente, etc.
            //NameValueCollection nvp = PayPal.Checkout.finalize(
            //    AmbienteExec.PRODUCAO,
            //    email,
            //    password,
            //    apiKey,
            //    token,
            //    PayerID,
            //    codMoeda,
            //    SessionIdioma == "PTBR" ? PayPal.Enum.LocaleCode.BRAZIL : SessionIdioma == "ENUS" ? PayPal.Enum.LocaleCode.UNITED_STATES : SessionIdioma == "ESP" ? PayPal.Enum.LocaleCode.SPAIN : PayPal.Enum.LocaleCode.FRANCE);


           //var payment = Payment.Get(apiContext, SessionPedido.CdTransacaoOutrosTpPgto);

           // Execute the order

           var paymentExecution = new PaymentExecution() { payer_id = PayerID };

            var payment = new Payment() { id = SessionPedido.CdTransacaoOutrosTpPgto };
            // ^ Ignore workflow code segment
            //#region Track Workflow
            //flow.AddNewRequest("Execute payment", payment);
            //#endregion

            // Execute the order payment.
            var executedPayment = payment.Execute(apiContext, paymentExecution);

            //// ^ Ignore workflow code segment
            //#region Track Workflow
            //flow.RecordResponse(executedPayment);
            //#endregion

            // Get the information about the executed order from the returned payment object.
            //this.order = executedPayment.transactions[0].related_resources[0].order;
            //this.amount = executedPayment.transactions[0].amount;
            //state enum 
            //            The state of the payment, authorization, or order transaction.Value is:
            //created.The transaction was successfully created.
            //approved.The customer approved the transaction.
            //failed.The transaction request failed.
            //Read only.
            //Possible values: created, approved, failed.
//following
            //if (nvp["ACK"].ToString().ToUpper() == "SUCCESS")
            if (executedPayment.state.ToUpper() == "approved".ToUpper())
            {
                // Get the information about the executed order from the returned payment object.
                this.order = executedPayment.transactions[0].related_resources[0].order;
                this.order.amount.details = null;
                this.amount = executedPayment.transactions[0].amount;
                amount.details = null;
                // Once the order has been executed, an order ID is returned that can be used
                // to do one of the :
                //            state enum
                //O estado da transação do pedido.O valor é:
                //pending.O pedido foi criado, mas nenhuma autorização / captura foi feita contra o pedido.
                //authorized.O pedido só foi autorizado. Nenhuma captura foi feita contra o pedido.
                //captured.A ordem tem pelo menos uma captura iniciada.
                //completed.A ordem é concluída, pois foi feita uma captura contra a ordem is_final_capturedefinida como TRUE.Não mais autorizações / capturas podem ser feitas contra este pedido.
                //voided.O pedido foi anulado. Não mais autorizações / capturas podem ser feitas contra este pedido.
                //Somente leitura. 
                //Possible values: pending , authorized , captured , completed , voided.
                //this.AuthorizeOrder(apiContext);
                this.CaptureOrder(apiContext);
                order = Order.Get(apiContext, order.id);
                //Debug.WriteLine("State Order:" + this.order.state);
                // this.VoidOrder();
                // this.RefundOrder();

                if (order.state.ToLower() == "captured")
                {
                    oPedidoCad.MarcarPedidoComoPago(SessionPedido, SessionCnn);

                    decimal tmpVlDescontoCupom = 0;
                    String tmpDsCupomDesconto = "";
                    if (SessionPedido.pedidoCupomDesconto != null)
                    {
                        tmpVlDescontoCupom = SessionPedido.pedidoCupomDesconto.VlDescontoCalculado;
                        tmpDsCupomDesconto = SessionPedido.pedidoCupomDesconto.DsCupomDesconto;
                    }

                    SessionPedido.TpPagamento = "PAYPAL";
                    SessionPedido = oPedidoCad.Gravar(SessionPedido, SessionCnn);
                    Session["SessionPedido"] = SessionPedido;

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
                               "OrderID: " + order.id, // nvp["PAYMENTINFO_0_TRANSACTIONID"],
                               "TransactID: " + SessionPedido.CdTransacaoOutrosTpPgto + // nvp.Get("PAYMENTINFO_0_TRANSACTIONID") +
                               tmpDsCupomDesconto != "" ? "\nCupom Desconto - " + tmpDsCupomDesconto : "" + // + "\nPayPal-ID: " + nvp["PAYERID"] + " - Nome do cliente: " + nvp["FIRSTNAME"] + " " + nvp["LASTNAME"],
                               (SessionEvento.CdEvento != "007002" ? "" : (SessionPedido.TpPessoa != "PJ" ? "" : "\nImpostos(PIS: " + SessionPedido.VlPIS.ToString("N2") + " - COFINS: " + SessionPedido.VlCOFINS.ToString("N2") + " - ISS: " + SessionPedido.VlISS.ToString("N2") + ")")),
                               "000000001",
                               "",
                               SessionPedido.VlTotalPedido - tmpVlDescontoCupom - SessionPedido.VlPIS - SessionPedido.VlCOFINS - SessionPedido.VlISS,
                               "",
                               0,
                               "",
                               SessionCnn);

                    if ((SessionEvento.CdEvento == "007002") || (SessionEvento.CdEvento == "000103"))
                    {
                        if (!SessionParticipante.Categoria.NoCategoria.Contains("BRAZILIAN"))
                        {
                            GerarInVoice(SessionEvento.CdEvento, SessionPedido.CdPedido, SessionCnn);
                        }
                    }

                    if (SessionEvento.CdEvento == "003002")
                    {
                        SessionParticipante.DsComplementoEndereco = "TransactID: " + SessionPedido.CdTransacaoOutrosTpPgto; //nvp["PAYMENTINFO_0_TRANSACTIONID"];

                        SessionParticipante = oParticipanteCad.Gravar(SessionParticipante, SessionCnn);
                    }

                    oGeral.EnviarEmailPedidoPayPalPago(SessionEvento, SessionParticipante, SessionPedido, SessionCnn);

                    //if (SessionEvento.CdEvento != "007002")
                    //{
                    if (!SessionParticipante.Categoria.FlConfirmacaoCadWeb)
                        oGeral.EnviarEmailConfirmaCadastro(SessionEvento, SessionParticipante, SessionCnn);
                    //}
                    //else if ((!SessionParticipante.Categoria.NoCategoria.Contains("ACADEMIC")) && (!SessionParticipante.Categoria.NoCategoria.Contains("STARTUP")))
                    //{
                    //    oGeral.EnviarEmailConfirmaCadastro(SessionEvento, SessionParticipante, SessionCnn);
                    //}


                    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                    "014-1",
                                    ""), true);
                }
                else
                {
                    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                "031",
                                ""), true);
                }
            }
            else
            {
                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                "031",
                                ""), true);
            }
            //ACK conterá o status, caso bem sucedido, será Success.
            //header.InnerText = nvp["ACK"]; //Coloca o valor do ACK no H1

            //createAndPopulateTable(nvp);
        }
        else
        {
            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            SessionPedido = (Pedido)Session["SessionPedido"];

        }

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
     var autorize =   this.order.Authorize(apiContext);
       // Debug.WriteLine("Status Autorize: " + autorize.state);
        
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
        //Debug.WriteLine("Status Capture Depois de Autorizado: " + capture.state); 
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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="text"></param>
    /// <param name="isRequest"></param>
    /// <returns></returns>
    protected string FormatPayloadText(string text, bool isRequest)
    {
        if (string.IsNullOrEmpty(text))
        {
            return string.Format("No payload for this {0}.", (isRequest ? "request" : "response"));
        }

        return text;
    }

    /// <summary>
    /// Gets the CSS class for the specified message.
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    protected string GetMessageClass(RequestFlowItemMessage message)
    {
        switch (message.Type)
        {
            case RequestFlowItemMessageType.Error:
                return "error";
            case RequestFlowItemMessageType.Success:
                return "success";
            default:
                return string.Empty;
        }
    }

    /// <summary>
    /// Formats the message to include an accompanying icon from Font Awesome (http://fortawesome.github.io/Font-Awesome/).
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    protected string GetMessageWithMarkup(RequestFlowItemMessage message)
    {
        var iconText = "";
        switch (message.Type)
        {
            case RequestFlowItemMessageType.Error:
                iconText = "<i class=\"fa fa-times-circle\"></i>";
                break;

            case RequestFlowItemMessageType.Success:
                iconText = "<i class=\"fa fa-check-circle\"></i>";
                break;

            case RequestFlowItemMessageType.General:
                iconText = "<i class=\"fa fa-info-circle\"></i>";
                break;
        }

        return string.Format("{0} {1}", iconText, message.Message);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    private string GetStringFromContext(string key)
    {
        return this.GetFromContext<string>(key);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    private T GetFromContext<T>(string key)
    {
        if (HttpContext.Current.Items.Contains(key))
        {
            return (T) HttpContext.Current.Items[key];
        }

        return default(T);
    }

    private void GerarInVoice(string prmCdEvento, string prmCdPedido, SqlConnection prmCnn)
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

                DataTable DT = new DataTable();
                SqlDataAdapter Dap;
                string cmdSQL = @"
                                SELECT *
                                FROM dbo.tbPedidoInVoice
                                WHERE cdEvento = @cdEvento
                                  AND cdPedido = @cdPedido";

                cmdSQL = cmdSQL.Replace("@cdEvento", prmCdEvento).Replace("@cdPedido", prmCdPedido);

                SqlCommand comando = new SqlCommand(
                cmdSQL, prmCnn);

                //string mediaType = System.Net.Mime.MediaTypeNames.Image.Jpeg;

                Dap = new SqlDataAdapter(comando);

                Dap.TableMappings.Add("ESCOLA", "tmpSimpro");
                Dap.Fill(DT);

                if ((DT != null) && (DT.Rows.Count > 0))
                {
                    return;
                }



                string cmd = @"
                            INSERT INTO dbo.tbPedidoInVoice
                            (
                                cdEvento
                                ,cdInvoice
                                ,cdPedido
                            )
                            VALUES
                            (
                                @cdEvento 
                                ,(SELECT dbo.LPAD(COALESCE(MAX(cdInvoice),0)+1,4,'0') cdInvoice
                                FROM dbo.tbPedidoInVoice
                                WHERE cdEvento = @cdEvento) 
                                ,@cdPedido
                            )";

                cmd = cmd.Replace("@cdEvento", "'" + prmCdEvento + "'").
                            Replace("@cdPedido", "'" + prmCdPedido + "'");

                SqlCommand comando2 = new SqlCommand(cmd, prmCnn);

                comando2.ExecuteNonQuery();


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
//public partial class frmPayPalRetornoOK : BaseSamplePage
//{

//    Participante SessionParticipante;
//    ParticipanteCad oParticipanteCad = new ParticipanteCad();
//    Evento SessionEvento;
//    EventoCad oEventoCad = new EventoCad();
//    SqlConnection SessionCnn;

//    Pedido SessionPedido;
//    PedidoCad oPedidoCad = new PedidoCad();

//    String SessionIdioma;

//    /// <summary>
//    /// Quando o cliente for redirecionado de volta para a loja pelo PayPal, ele cairá nessa página,
//    /// onde chamaremos o método finalize da classe Checkout informando o token e PayerID enviados
//    /// pelo PayPal.
//    /// </summary>
//    /// <param name='sender'>
//    /// Sender.
//    /// </param>
//    /// <param name='e'>
//    /// E.
//    /// </param>
//    protected void Page_Load(object sender, EventArgs e)
//    {
//        if (!Page.IsPostBack)
//        {


//            SessionCnn = (SqlConnection)Session["SessionCnn"];
//            if (SessionCnn == null)
//            {
//                //local 1
//                //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOWEzSnJjMkV0Y0dNN1NXNXBkR2xoYkNCRFlYUmhiRzluUFdSaVJYWmxiblJ2YzE5R1RUdFFaWEp6YVhOMElGTmxZM1Z5YVhSNUlFbHVabTg5VkhKMVpUdFZjMlZ5SUVsRVBYTmhPMUJoYzNOM2IzSmtQV3R5YTNOaE1UY3g=")));

//                //local 2 note novo
//                //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOUxseHJjbXR6WVhCak8wbHVhWFJwWVd3Z1EyRjBZV3h2Wnoxa1lrVjJaVzUwYjNOZlJrMDdVR1Z5YzJsemRDQlRaV04xY21sMGVTQkpibVp2UFZSeWRXVTdWWE5sY2lCSlJEMXpZVHRRWVhOemQyOXlaRDFyY210ellURTNNUT09")));

//                //servidor
//                //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOVptRjZaVzVrYjIxaGFYTTdTVzVwZEdsaGJDQkRZWFJoYkc5blBXUmlSWFpsYm5SdmMxOUdUVHRRWlhKemFYTjBJRk5sWTNWeWFYUjVJRWx1Wm04OVZISjFaVHRWYzJWeUlFbEVQWE5oTzFCaGMzTjNiM0prUFV0eWEzTmhNVGN4")));

//                //MinSaude
//                //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOUxseHpjV3hsZUhCeVpYTnpPMGx1YVhScFlXd2dRMkYwWVd4dlp6MWtZa1YyWlc1MGIzTmZSazA3VUdWeWMybHpkQ0JUWldOMWNtbDBlU0JKYm1adlBWUnlkV1U3VlhObGNpQkpSRDF6WVR0UVlYTnpkMjl5WkQxU1lVTTVPREk1TnpNPQ==")));

//                //Site
//                //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOU1UZzVMak00TGprMUxqY3lPMGx1YVhScFlXd2dRMkYwWVd4dlp6MW1ZWHBsYm1SdmJXRnBjenRRWlhKemFYTjBJRk5sWTNWeWFYUjVJRWx1Wm04OVZISjFaVHRWYzJWeUlFbEVQV1poZW1WdVpHOXRZV2x6TzFCaGMzTjNiM0prUFZGMU5EVTBja1p0")));

//                //Site2-historico
//                //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOWJYTnpjV3d1Wm1GNlpXNWtiMjFoYVhNdVkyOXRPMGx1YVhScFlXd2dRMkYwWVd4dlp6MW1ZWHBsYm1SdmJXRnBjekU3VUdWeWMybHpkQ0JUWldOMWNtbDBlU0JKYm1adlBWUnlkV1U3VlhObGNpQkpSRDFtWVhwbGJtUnZiV0ZwY3pFN1VHRnpjM2R2Y21ROVVYVTBOVFJ5Um0wPQ==")));

//                //Site-producao
//                //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOWJYTnpjV3d1Wm1GNlpXNWtiMjFoYVhNdVkyOXRPMGx1YVhScFlXd2dRMkYwWVd4dlp6MW1ZWHBsYm1SdmJXRnBjenRRWlhKemFYTjBJRk5sWTNWeWFYUjVJRWx1Wm04OVZISjFaVHRWYzJWeUlFbEVQV1poZW1WdVpHOXRZV2x6TzFCaGMzTjNiM0prUFZGMU5EVTBja1p0VFVSQg==")));

//                //Site-producao - AZURE
//                SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOWFHcDBZWEF3YjNkcWJpNWtZWFJoWW1GelpTNTNhVzVrYjNkekxtNWxkRHRKYm1sMGFXRnNJRU5oZEdGc2IyYzlaR0pmWlhabGJuUnZjMFpOTzFCbGNuTnBjM1FnVTJWamRYSnBkSGtnU1c1bWJ6MVVjblZsTzFWelpYSWdTVVE5UTI5dVlYTmxiWE03VUdGemMzZHZjbVE5UXpCdU5EVmxiVFV5TURFMg==")));

//                //Site-producao - IP
//                //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOU1UZzVMak00TGprd0xqRXdPVHRKYm1sMGFXRnNJRU5oZEdGc2IyYzlabUY2Wlc1a2IyMWhhWE03VUdWeWMybHpkQ0JUWldOMWNtbDBlU0JKYm1adlBWUnlkV1U3VlhObGNpQkpSRDFtWVhwbGJtUnZiV0ZwY3p0UVlYTnpkMjl5WkQxUmRUUTFOSEpHYlUxRVFRPT0=")));

//                Session["SessionCnn"] = SessionCnn;

//            }

//            Geral oGeral = new Geral();

//            //if (oGeral.verificarSiteManutencao("1", SessionCnn))
//            //{
//            //    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
//            //                    "05",
//            //                    ""), true);
//            //}

//            SessionIdioma = (String)Session["SessionIdioma"];
//            if (SessionIdioma == null)
//                SessionIdioma = "PTBR";
//            Session["SessionIdioma"] = SessionIdioma;

//            SessionEvento = (Evento)Session["SessionEvento"];
//            if (SessionEvento == null)
//            {
//                if ((Request.QueryString["e"] != null) &&
//                    (Request.QueryString["e"] != ""))
//                {
//                    string cd_Evento = cllEventos.Crypto.DecryptStringAES(Request.QueryString["e"].ToString());

//                    SessionEvento = oEventoCad.Pesquisar(cd_Evento, SessionCnn);
//                    Session["SessionEvento"] = SessionEvento;
//                    if (SessionEvento == null)
//                    {
//                        return;
//                        //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
//                        //            "003",
//                        //            ""), true);

//                    }
//                }


//            }

//            SessionParticipante = (Participante)Session["SessionParticipante"];
//            if (SessionParticipante == null)
//            {
//                if ((Request.QueryString["p"] != null) &&
//                    (Request.QueryString["p"] != ""))
//                {
//                    string cd_participante = cllEventos.Crypto.DecryptStringAES(Request.QueryString["p"].ToString());

//                    SessionParticipante = oParticipanteCad.Pesquisar(SessionEvento.CdEvento, cd_participante, SessionCnn);
//                    Session["SessionParticipante"] = SessionParticipante;
//                    if (SessionParticipante == null)
//                    {
//                        return;
//                        //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
//                        //                 "04",
//                        //                 ""), true);

//                    }
//                }
//            }

//            SessionPedido = (Pedido)Session["SessionPedido"];
//            if (SessionPedido == null)
//            {
//                if ((Request.QueryString["pd"] != null) &&
//                    (Request.QueryString["pd"] != ""))
//                {
//                    string cd_pedido = Request.QueryString["pd"].ToString();// cllEventos.Crypto.DecryptStringAES(Request.QueryString["pd"].ToString());

//                    SessionPedido = oPedidoCad.Pesquisar(SessionEvento.CdEvento, cd_pedido, SessionCnn);
//                    Session["SessionPedido"] = SessionPedido;
//                    if (SessionPedido == null)
//                    {
//                        return;
//                        //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
//                        //                 "04",
//                        //                 ""), true);

//                    }
//                }
//            }

//            string token = Request.QueryString["token"];
//            string PayerID = Request.QueryString["PayerID"];



//            //string email = "valdo_1342267021_biz_api1.hotmail.com";
//            //string password = "1342267044";
//            //string apiKey = "A2yXYR.rnKfmdjsFw84g8BDHdBsVAzDbb-edCSs1NczPjV3dUqEJrJmc";

//            string email = SessionEvento.payPalConfig.ContaPP;// "valdoalves_api1.fazendomais.com.br";
//            string password = SessionEvento.payPalConfig.SenhaPP;// "MUWTTN6FLEMNVDQF";
//            string apiKey = SessionEvento.payPalConfig.CodAcessoPP;// "AvR0zmpDVFZxrrck1gVQuyMMq6TUAC2yxyfF8OwCxntCQGqda7aaWJKN";


//            PayPal.Enum.CurrencyCode codMoeda;

//            if ((SessionParticipante.NoPais.Trim() == "") || (SessionParticipante.NoPais.ToUpper() == "BRASIL"))
//                codMoeda = PayPal.Enum.CurrencyCode.BRAZILIAN_REAL;
//            else
//                codMoeda = PayPal.Enum.CurrencyCode.US_DOLLAR;


//            //O método finalize, da classe Checkout, vai retornar um NameValueCollection com os dados
//            //obtidos na chamada à operação GetExpressCheckoutDetails. Utilizamos esses dados para
//            //popular a base de dados com informações do cliente, etc.
//            NameValueCollection nvp = PayPal.Checkout.finalize(
//                AmbienteExec.PRODUCAO,
//                email,
//                password,
//                apiKey,
//                token,
//                PayerID,
//                codMoeda,
//                SessionIdioma == "PTBR" ? PayPal.Enum.LocaleCode.BRAZIL : SessionIdioma == "ENUS" ? PayPal.Enum.LocaleCode.UNITED_STATES : SessionIdioma == "ESP" ? PayPal.Enum.LocaleCode.SPAIN : PayPal.Enum.LocaleCode.FRANCE);


//            if (nvp["ACK"].ToString().ToUpper() == "SUCCESS")
//            {


//                oPedidoCad.MarcarPedidoComoPago(SessionPedido, SessionCnn);

//                decimal tmpVlDescontoCupom = 0;
//                String tmpDsCupomDesconto = "";
//                if (SessionPedido.pedidoCupomDesconto != null)
//                {
//                    tmpVlDescontoCupom = SessionPedido.pedidoCupomDesconto.VlDescontoCalculado;
//                    tmpDsCupomDesconto = SessionPedido.pedidoCupomDesconto.DsCupomDesconto;
//                }

//                SessionPedido.TpPagamento = "PAYPAL";
//                SessionPedido = oPedidoCad.Gravar(SessionPedido, SessionCnn);
//                Session["SessionPedido"] = SessionPedido;

//                ReciboCad oReciboCad = new ReciboCad();
//                Recibo oRecibo = oReciboCad.Gravar(
//                           SessionEvento.CdEvento,
//                           SessionParticipante.CdParticipante,
//                           SessionPedido.CdPedido,
//                           "",
//                           SessionPedido.VlTotalPedido,
//                           tmpVlDescontoCupom,
//                           0,
//                           SessionPedido.TpPagamento,
//                           "TransactID: " + nvp["PAYMENTINFO_0_TRANSACTIONID"],
//                           "TransactID: " + nvp.Get("PAYMENTINFO_0_TRANSACTIONID") +
//                           tmpDsCupomDesconto != "" ? "\nCupom Desconto - " + tmpDsCupomDesconto : ""+ // + "\nPayPal-ID: " + nvp["PAYERID"] + " - Nome do cliente: " + nvp["FIRSTNAME"] + " " + nvp["LASTNAME"],
//                           (SessionEvento.CdEvento != "007002" ? "" : (SessionPedido.TpPessoa != "PJ" ? "" : "\nImpostos(PIS: " + SessionPedido.VlPIS.ToString("N2") + " - COFINS: " + SessionPedido.VlCOFINS.ToString("N2") + " - ISS: " + SessionPedido.VlISS.ToString("N2")+")")),
//                           "000000001",
//                           "",
//                           SessionPedido.VlTotalPedido - tmpVlDescontoCupom - SessionPedido.VlPIS - SessionPedido.VlCOFINS - SessionPedido.VlISS,
//                           "",
//                           0,
//                           "",
//                           SessionCnn);

//                if (SessionEvento.CdEvento == "007002")
//                {
//                    if (!SessionParticipante.Categoria.NoCategoria.Contains("BRAZILIAN"))
//                    {
//                        GerarInVoice(SessionEvento.CdEvento, SessionPedido.CdPedido, SessionCnn);
//                    }
//                }

//                if (SessionEvento.CdEvento == "003002")
//                {
//                    SessionParticipante.DsComplementoEndereco = "TransactID: " + nvp["PAYMENTINFO_0_TRANSACTIONID"];

//                    SessionParticipante = oParticipanteCad.Gravar(SessionParticipante, SessionCnn);
//                }

//                oGeral.EnviarEmailPedidoPayPalPago(SessionEvento, SessionParticipante, SessionPedido, SessionCnn);

//                //if (SessionEvento.CdEvento != "007002")
//                //{
//                    if (!SessionParticipante.Categoria.FlConfirmacaoCadWeb)
//                        oGeral.EnviarEmailConfirmaCadastro(SessionEvento, SessionParticipante, SessionCnn);
//                //}
//                //else if ((!SessionParticipante.Categoria.NoCategoria.Contains("ACADEMIC")) && (!SessionParticipante.Categoria.NoCategoria.Contains("STARTUP")))
//                //{
//                //    oGeral.EnviarEmailConfirmaCadastro(SessionEvento, SessionParticipante, SessionCnn);
//                //}


//                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
//                                "014-1",
//                                ""), true);
//            }
//            //ACK conterá o status, caso bem sucedido, será Success.
//            //header.InnerText = nvp["ACK"]; //Coloca o valor do ACK no H1

//            //createAndPopulateTable(nvp);
//        }
//        else
//        {
//            SessionParticipante = (Participante)Session["SessionParticipante"];

//            SessionEvento = (Evento)Session["SessionEvento"];

//            SessionCnn = (SqlConnection)Session["SessionCnn"];

//            SessionPedido = (Pedido)Session["SessionPedido"];

//        }


//    }

//    private void GerarInVoice(string prmCdEvento, string prmCdPedido, SqlConnection prmCnn)
//    {
//        if (prmCnn == null)
//        {
//            //_erroNoCampo = true;
//            //lblMsg.Text = "Conexão inválida ou inexistente";
//            return;
//        }


//        if (prmCnn.State != ConnectionState.Open)
//        {
//            try
//            {
//                prmCnn.Open();
//            }
//            catch
//            {
//                //_erroNoCampo = true;
//                //lblMsg.Text = "Conexão inválida";
//                return;
//            }
//        }


//        try
//        {
//            try
//            {

//                DataTable DT = new DataTable();
//                SqlDataAdapter Dap;
//                string cmdSQL = @"
//                            SELECT *
//                            FROM dbo.tbPedidoInVoice
//                            WHERE cdEvento = @cdEvento
//                              AND cdPedido = @cdPedido";

//                cmdSQL = cmdSQL.Replace("@cdEvento", prmCdEvento).Replace("@cdPedido", prmCdPedido);

//                SqlCommand comando = new SqlCommand(
//                cmdSQL, prmCnn);

//                //string mediaType = System.Net.Mime.MediaTypeNames.Image.Jpeg;

//                Dap = new SqlDataAdapter(comando);

//                Dap.TableMappings.Add("ESCOLA", "tmpSimpro");
//                Dap.Fill(DT);

//                if ((DT != null) && (DT.Rows.Count > 0))
//                {
//                    return;
//                }



//                string cmd = @"
//                        INSERT INTO dbo.tbPedidoInVoice
//                        (
//                            cdEvento
//                            ,cdInvoice
//                            ,cdPedido
//                        )
//                        VALUES
//                        (
//                            @cdEvento 
//                            ,(SELECT dbo.LPAD(COALESCE(MAX(cdInvoice),0)+1,4,'0') cdInvoice
//                            FROM dbo.tbPedidoInVoice
//                            WHERE cdEvento = @cdEvento) 
//                            ,@cdPedido
//                        )";

//                cmd = cmd.Replace("@cdEvento", "'" + prmCdEvento + "'").
//                            Replace("@cdPedido", "'" + prmCdPedido + "'");

//                SqlCommand comando2 = new SqlCommand(cmd, prmCnn);

//                comando.ExecuteNonQuery();


//            }
//            catch (Exception Ex)
//            {
//                //_erroNoCampo = true;
//                //lblMsg.Text = "Erro ao selecionar ESCOLAS!\n" + Ex.Message;

//            }
//        }
//        finally
//        {
//            prmCnn.Close();
//        }
//    }

//    /// <summary>
//    /// Aqui uma pequena demonstração de como obter os dados do NVP.
//    /// </summary>
//    /// <param name='nvp'>
//    /// Nvp.
//    /// </param>
//    private void createAndPopulateTable(NameValueCollection nvp)
//    {
//        Table table;

//        table = createTableWithCaption("Dados do Cliente");

//        createTableRowWithNameAndValue(table, "Nome do cliente", nvp["FIRSTNAME"] + " " + nvp["LASTNAME"]);
//        createTableRowWithNameAndValue(table, "Email", nvp["EMAIL"]);
//        createTableRowWithNameAndValue(table, "ID do cliente no PayPal", nvp["PAYERID"]);

//        //table = createTableWithCaption("Dados de Entrega");

//        //createTableRowWithNameAndValue(table, "Entregar para", nvp["PAYMENTREQUEST_0_SHIPTONAME"]);
//        //createTableRowWithNameAndValue(table, "Endereço", nvp["PAYMENTREQUEST_0_SHIPTOSTREET"]);
//        //createTableRowWithNameAndValue(table, "Cidade", nvp["PAYMENTREQUEST_0_SHIPTOCITY"]);
//        //createTableRowWithNameAndValue(table, "Estado", nvp["PAYMENTREQUEST_0_SHIPTOSTATE"]);
//        //createTableRowWithNameAndValue(table, "CEP", nvp["PAYMENTREQUEST_0_SHIPTOZIP"]);
//        //createTableRowWithNameAndValue(table, "País", nvp["PAYMENTREQUEST_0_SHIPTOCOUNTRYNAME"]);
//    }

//    private Table createTableWithCaption(string caption)
//    {
//        Table table = new Table();

//        TableHeaderRow captionRow = new TableHeaderRow();
//        TableHeaderCell captionCell = new TableHeaderCell();

//        captionCell.Text = caption;
//        captionCell.HorizontalAlign = HorizontalAlign.Left;
//        captionCell.ColumnSpan = 2;

//        captionRow.Cells.Add(captionCell);

//        table.Rows.Add(captionRow);

//        data.Controls.Add(table);

//        return table;
//    }

//    private void createTableRowWithNameAndValue(Table table, string name, string value)
//    {
//        TableRow row = new TableRow();
//        TableCell nameCell = new TableCell();
//        TableCell valueCell = new TableCell();

//        nameCell.Text = name;
//        nameCell.Width = 300;

//        valueCell.Text = value;
//        valueCell.Width = 400;

//        row.Cells.Add(nameCell);
//        row.Cells.Add(valueCell);

//        table.Rows.Add(row);
//    }
//}