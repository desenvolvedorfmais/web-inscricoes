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

using Uol.PagSeguro;
//using PayPal.Enum;

//using MSXML2;
//using System.Xml;

using System.Net;
//using System.Text;

public partial class frmPagSeguroPagamento : System.Web.UI.Page
{
    Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    SqlConnection SessionCnn;

    Pedido SessionPedido;
    PedidoCad oPedidoCad = new PedidoCad();


    String SessionIdioma;
    

    protected void Page_Load(object sender, EventArgs e)
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

        TSManager1.RegisterPostBackControl(btnContinuarPagSeguro);
    }

    protected void verificarIdioma(string prmIdioma)
    {
        if (prmIdioma == "PTBR")
        {
            SessionIdioma = "PTBR";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Matrículas / Inscrições";
            lblTituloResumo.Text = "Resumo do Pedido";


            lblPart.Text = "Participante:";
            lblCateg.Text = "Categoria:";

            lblResPed.Text = "Pedido nº";
            lblResItens.Text = "Itens";
            lblResVlr.Text = "Valor";
            lblResDesc.Text = "Desconto";
            lblResVlrTotal.Text = "Total";

            lblMsgParticipante.Text =
                "Prezado participante, o UOL Pagseguro é a forma escolhida para pagamento.<br /> " +
                "Para concluir sua inscrição você será direcionado para o Pagseguro, onde poderá selecionar a melhor forma de pagamento para você.";

            btnContinuarPagSeguro.Text = "Continuar";

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
            lblResVlrTotal.Text = "Total";

            lblMsgParticipante.Text =
                "Dear participant, PayPal is the way chosen to manage our collections. <br />" +
                "To complete your registration you will be directed to PayPal where you can select the best payment method for you.";

            btnContinuarPagSeguro.Text = "Continue";
        }
        else if (prmIdioma == "ESP")
        {
            SessionIdioma = "ESP";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "El pago";
            lblTituloResumo.Text = "Resumen de la solicitud";

            lblPart.Text = "Participante:";
            lblCateg.Text = "Categoría";

            lblResPed.Text = "Solicitud Nº";
            lblResItens.Text = "Artículos";
            lblResVlr.Text = "Valor";
            lblResDesc.Text = "Descuento";
            lblResVlrTotal.Text = "Total";

            lblMsgParticipante.Text =
                "Estimado participante, PayPal es la forma elegida para administrar nuestras colecciones.<br />" +
                "Para completar su inscripción será dirigido a PayPal, donde puede seleccionar el mejor método de pago para usted.";

            btnContinuarPagSeguro.Text = "Continuar";
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

            btnContinuarPagSeguro.Text = "Continuer";
        }
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        
        //ambiente de produção
        //string email = SessionEvento.pagSeguroConfig.ContaPS;// 
        //string token = SessionEvento.pagSeguroConfig.CodAcessoPS;// 
        string urlSite = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);

        //string URLRetorno = "https://inscricoesweb.fazendomais.com/frmPagSeguroRetorno.aspx?pd=" + SessionPedido.CdPedido + "&e=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + "&p=" + cllEventos.Crypto.EncryptStringAES(SessionParticipante.CdParticipante);
        string URLRetorno = urlSite + "/frmPagSeguroRetorno.aspx?pd=" + SessionPedido.CdPedido + "&e=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + "&p=" + cllEventos.Crypto.EncryptStringAES(SessionParticipante.CdParticipante);
        //string URLNotificacao = "https://inscricoesweb.fazendomais.com/frmPagSeguroNotificacao.aspx?pd=" + SessionPedido.CdPedido + "&e=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + "&p=" + cllEventos.Crypto.EncryptStringAES(SessionParticipante.CdParticipante);

        try
        {
            decimal tmpVlDescontoCupom = 0;
            if (SessionPedido.pedidoCupomDesconto != null)
                tmpVlDescontoCupom = SessionPedido.pedidoCupomDesconto.VlDescontoCalculado;

            PaymentRequest payment = new PaymentRequest();

            payment.Items.Add(
                new Item(
                    "0001",
                    SessionEvento.NoEvento,
                    1,
                    SessionPedido.VlTotalPedido - tmpVlDescontoCupom)
                );

            payment.Sender = new Sender(
                    SessionParticipante.NoParticipante,
                    SessionParticipante.DsEmail
                );

            payment.Shipping = new Shipping();
            payment.Shipping.ShippingType = ShippingType.NotSpecified;

            payment.Shipping.Address = new Address(
                ((SessionParticipante.NoPais.Trim() == "") || (SessionParticipante.NoPais.ToUpper() == "BRASIL")) ? "BRA" : "",
                SessionParticipante.DsUF,
                SessionParticipante.NoCidade.Substring(0, (SessionParticipante.NoCidade.Length < 59 ? SessionParticipante.NoCidade.Length : 59)),
                SessionParticipante.NoBairro.Substring(0,  (SessionParticipante.NoBairro.Length < 59 ? SessionParticipante.NoBairro.Length : 59)),
                SessionParticipante.NuCEP,
                SessionParticipante.DsEndereco.Substring(0,  (SessionParticipante.DsEndereco.Length < 75 ? SessionParticipante.DsEndereco.Length : 75)),
                "_",
                SessionParticipante.DsComplementoEndereco.Substring(0, (SessionParticipante.DsComplementoEndereco.Length < 39 ? SessionParticipante.DsComplementoEndereco.Length : 39))
            );

            payment.Currency = Currency.Brl;
            payment.Reference = SessionPedido.CdPedido;

            Uri urlretorno = new Uri(URLRetorno);//"https://inscricoesweb.fazendomais.com/frmPagSeguroRetorno.aspx?e=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento));
            payment.RedirectUri = urlretorno;
                       

            AccountCredentials credentials = new AccountCredentials(
                SessionEvento.pagSeguroConfig.ContaPS,
                SessionEvento.pagSeguroConfig.CodAcessoPS
            );
            Uri paymentRedirectUri = PaymentService.Register(credentials, payment);


            Response.Redirect(paymentRedirectUri.AbsoluteUri, true);

        }
        catch (PagSeguroServiceException Ex)
        {
            lblMsg.Text = Ex.StatusCode.ToString();  
      
            if (Ex.StatusCode == HttpStatusCode.Unauthorized) {  
                lblMsg.Text +=  
                    "<br/> Não autorizado: " +
                    "Por favor verifique se as credenciais usadas no web sevice estão corretas<br/>";
                
            }  
      
            foreach (PagSeguroServiceError error in Ex.Errors) {  
                lblMsg.Text += error;  
            }  


        }

        
    }
}