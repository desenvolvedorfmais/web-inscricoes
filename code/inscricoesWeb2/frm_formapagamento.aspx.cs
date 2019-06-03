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

using System.Data.SqlClient;

using MSXML2;

using System.Xml;

using Cielo;
//using Cielo.Configuration;
//using Cielo.Enums;
//using Cielo.Requests;
//using Cielo.Requests.Entities;
//using Cielo.Responses;
//using Cielo.Responses.Exceptions;


using FM.Ecommerce.Sipag.Card;
using FM.Ecommerce.Sipag.Card.Configuration;
using FM.Ecommerce.Sipag.Card.Enums;
using FM.Ecommerce.Sipag.Card.Requests;
using FM.Ecommerce.Sipag.Card.Requests.Entities;
using FM.Ecommerce.Sipag.Card.Responses;
using FM.Ecommerce.Sipag.Card.Responses.Exceptions;

using System.Text.RegularExpressions;
using CreditCard = Cielo.CreditCard;

using Rede.Acquiring.SDK.Rest;
using Rede.Acquiring.SDK.Rest.Model;

using BoletoFacilSDK;
using BoletoFacilSDK.Model;
using BoletoFacilSDK.Enums;
using BoletoFacilSDK.Model.Entities;
using BoletoFacilSDK.Model.Response;
using System.Threading;
using System.Globalization;
using Gerencianet.SDK;
using Newtonsoft.Json;

public partial class frm_formapagamento : BaseWebUi //System.Web.UI.Page
{
    Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    ReciboCad oReciboCad = new ReciboCad();

    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    SqlConnection SessionCnn;

    Pedido SessionPedido;
    PedidoCad oPedidoCad = new PedidoCad();

    Categoria SessionCategoria;

    MeuBoletoCad oBoletoCad = new MeuBoletoCad();

    MeuBoleto oBoleto = null;

    String SessionIdioma;
  

    //private CieloService _cieloService;    
    //private CustomCieloConfiguration _configuration;

    private SipagService _sipagService;
    private CustomSipagConfiguration _configurationSIPAG;
    private static FM.Ecommerce.Sipag.Card.Responses.CreateTransactionResponse _response;

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

            string tmpMsgAguarde = "Aguarde...";
            if (SessionIdioma == "ENUS")
                tmpMsgAguarde = "Wait...";
            if (SessionIdioma == "ESP")
                tmpMsgAguarde = "Esperar...";
            if (SessionIdioma == "FRA")
                tmpMsgAguarde = "Attends...";
            btnConfirmarPgto.Attributes.Add("onclick", "document.body.style.cursor = 'wait'; this.value='"+tmpMsgAguarde+"'; this.disabled = true; " + ClientScript.GetPostBackEventReference(btnConfirmarPgto, string.Empty) + ";");
            // btnConfirmar.Attributes.Add("onclick", "document.body.style.cursor = 'wait'; this.value='Aguarde, Enviando...'; this.disabled = true; " + ClientScript.GetPostBackEventReference(btnCupomDesconto, string.Empty) + ";");

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

            if ((!SessionParticipante.Categoria.FlPagamento) && (SessionParticipante.Categoria.FlConfirmacaoCadWeb))
            {
                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                "009",
                                oEventoCad.RcMsg), true);
            }

            


            lblIdentificador.Text = SessionParticipante.CdParticipante.Trim();
            lblNoParticipante.Text = SessionParticipante.NoParticipante.Trim();

            if (SessionEvento.CdEvento == "007002")
            {
                lblNoParticipante.Text = SessionParticipante.DsAuxiliar2 + ", " + SessionParticipante.NoParticipante.Trim();
            }

            //if ((SessionEvento.CdCliente == "0070") || (SessionEvento.CdCliente == "0013") || (SessionEvento.CdEvento == "000540"))
            if ((SessionEvento.FlCupomDesconto) && (SessionParticipante.Categoria.FlDescontos))
            {
                divCupomDesconto.Visible = true;
            }

            CategoriaCad oCategoriaCad = new CategoriaCad();
            SessionCategoria = oCategoriaCad.Pesquisar(SessionEvento.CdEvento, SessionParticipante.CdCategoria, SessionCnn);

            //if (!SessionCategoria.FlAtividades)
            //{
            //    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
            //                    "008",
            //                    oEventoCad.RcMsg), true);
            //}

            Session["SessionCategoria"] = SessionCategoria;
            if (SessionIdioma == "PTBR")
            {
                lblCategoria.Text = SessionCategoria.NoCategoria.Trim();
                if (SessionEvento.CdEvento == "005503")
                    lblCategoria.Text += "<br />" + SessionParticipante.NoInstituicao;
            }
            else if (SessionIdioma == "ENUS")
                lblCategoria.Text = SessionCategoria.NoCategoriaIngles.Trim();
            else if (SessionIdioma == "ESP")
                lblCategoria.Text = SessionCategoria.NoCategoriaEspanhol.Trim();
            else if (SessionIdioma == "FRA")
                lblCategoria.Text = SessionCategoria.NoCategoriaFrances.Trim();

            if (SessionPedido == null)
                SessionPedido = (Pedido)Session["SessionPedido"];
            else
                Session["SessionPedido"] = SessionPedido;


            if ((SessionEvento.CdEvento == "004401") //cispod
                 && (SessionIdioma != "PTBR"))
            {
                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                "014",
                                ""), true);
            }

            //FormasDePagamento();
            FormasDePagamento2();
            QuantidadeParcelas();

            

            CarregarAnoValidadeCartao();
            //ListarUFRecibo();

            //ListarCidades("");

            //PesquisarDadosRecibo();

            //ListarPaises(txtPaisRecibo);


            //if (SessionEvento.CdCliente == "0003")
            //{
            //    if (SessionParticipante.DsAuxiliar2 != "")
            //        TxtFormaPagamento.SelectedValue = SessionParticipante.DsAuxiliar2.ToUpper() == "BOLETO" ? "01" : SessionParticipante.DsAuxiliar2.ToUpper() == "EMPENHO" ? "02" : "03";

            //    if (TxtFormaPagamento.SelectedItem.Text.ToUpper().Contains("EMPENHO"))
            //    {
            //        pnlResponsavelFinanceiro.Visible = true;
            //    }

            //    TxtFormaPagamento.Enabled = false;
            //}

            if ((SessionEvento.CdCliente == "0003") || (SessionEvento.CdCliente == "0013"))
            {
                lblqtdParcelas.Visible = false;
                txtQtdParcelas.Visible = false;
            }

            //if (SessionEvento.CdCliente == "0013")
            //{
            //    btnDadosInstituicaoRecibo.Visible = true;
            //}
            //pnlDadosRecibo.Visible = false;
            //if ((!SessionEvento.FlEmiteRecibo) || (SessionIdioma != "PTBR"))
            //{
            //    pnlDadosRecibo.Visible = false;

            //    //txtCPFCNPJRecibo.Text = oClsFuncoes.CPFCNPJMascarar(SessionParticipante.NuCPFCNPJ);
            //    //txtNomeRecibo.Text = SessionParticipante.NoParticipante;
            //    //txtUFRecibo.Text = SessionParticipante.DsUF;
            //    //txtCidadeRecibo.Text = SessionParticipante.NoCidade;
            //    //txtEnderecoRecibo.Text = SessionParticipante.DsEndereco;
            //    //txtCEPRecibo.Text = oClsFuncoes.MascaraGerar(SessionParticipante.NuCEP, "99.999-999");
            //}

            //if ((SessionParticipante.CdCategoria == "00700201") || (SessionParticipante.CdCategoria == "00700202"))
            //if (SessionParticipante.Categoria.NoCategoria.Contains("BRAZILIAN"))
            //{
            //   // pnlDadosRecibo.Visible = true;
            //}

            //if (SessionEvento.CdCliente == "0014")
            //{
            //    lblTituloDadosRecibo.Text = "Dados para nota fiscal";
            //}

            //if (!SessionEvento.FlPesquisaCPFReceita)
            //{
            //    btnDadosParticipanteRecibo.Visible = false;
            //   // btnDadosInstituicaoRecibo.Visible = false;
            //}


            //SessionPedido = oPedidoCad.VerificaPedidoNaoPagoWeb(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);
            //Session["SessionPedido"] = SessionPedido;
            if (SessionPedido != null)
            {
                if ((SessionPedido.TpPagamento.Trim() != "") && (!SessionEvento.FlPermitirEditarPedido))
                {
                    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                    "015",
                                    ""), true);
                }

                lblNrPedido.Text = SessionPedido.CdPedido;

                decimal vlDesconto = 0;

                DescontoCupom oDescontoCupom;
                DescontoCupomCad oDescontoCupomCad = new DescontoCupomCad();

                if ((SessionPedido.pedidoCupomDesconto != null) && (SessionPedido.pedidoCupomDesconto.CdCupomDesconto != ""))
                {


                    oDescontoCupom = oDescontoCupomCad.PesquisarPorCupom(SessionEvento.CdEvento, SessionPedido.pedidoCupomDesconto.DsCupomDesconto.ToUpper(), SessionCnn);

                    if (oDescontoCupom == null)
                    {
                        if (SessionIdioma == "PTBR")
                            lblMsgCupom.Text = "Cupom de desconto inválido.";
                        else if (SessionIdioma == "ENUS")
                            lblMsgCupom.Text = "Invalid discount code.";
                        else if (SessionIdioma == "ESP")
                            lblMsgCupom.Text = "Cupón de descuento no es válido.";

                        lblMsgCupom.Visible = true;
                    }
                    else if ((oDescontoCupom.DtValidadeDesconto != null) && (oDescontoCupom.DtValidadeDesconto < Geral.datahoraServidor(SessionCnn)))
                    {
                        if (SessionIdioma == "PTBR")
                            lblMsgCupom.Text = "Cupom de desconto expirado.";
                        else if (SessionIdioma == "ENUS")
                            lblMsgCupom.Text = "Discount code expired.";
                        else if (SessionIdioma == "ESP")
                            lblMsgCupom.Text = "Cupón de descuento expirado.";

                        lblMsgCupom.Visible = true;
                    }
                    //else if ((oDescontoCupom.QtdLimteUso != 0) && (oDescontoCupom.QtdLimteUso <= oDescontoCupom.QtdUtilizado))
                    //{
                    //    if (SessionIdioma == "PTBR")
                    //        lblMsgCupom.Text = "Cupom de desconto já utilizado.";
                    //    else //if (SessionIdioma == "PTBR")
                    //        lblMsgCupom.Text = "Discount code already used.";

                    //    lblMsgCupom.Visible = true;
                    //}
                    else
                    {
                        txtCupomDesconto.Text = SessionPedido.pedidoCupomDesconto.DsCupomDesconto;

                        vlDesconto = oDescontoCupom.VlDesconto;

                        if (oDescontoCupom.TpDesconto.ToUpper() != "REAL")
                        {
                            vlDesconto = Math.Round(SessionPedido.VlTotalPedido * (vlDesconto / 100), 2);
                        }

                        btnAlterarCupomDesconto.Visible = true;
                        lblAvisoDescontoCalculado.Visible = true;
                        txtCupomDesconto.Enabled = false;
                        btnCupomDesconto.Visible = false;

                        if ((SessionPedido.VlTotalPedido - vlDesconto) <= 0)
                        {
                            //pnlDadosRecibo.Visible = false;
                            pnlInfoPgto.Visible = false;
                        }
                    }

                    
                }

                DataTable dtpedido = oPedidoCad.TotalizarPedido(SessionPedido.CdPedido, SessionCnn);
                if (dtpedido != null)
                {
                    vlItensPgto.Text = dtpedido.DefaultView[0]["itens"].ToString();
                    vlTotalAtivPgto.Text = decimal.Parse(dtpedido.DefaultView[0]["tol_Atv"].ToString()).ToString("N2");
                    vlTotalDesc.Text = (decimal.Parse(dtpedido.DefaultView[0]["tot_desc"].ToString()) + vlDesconto).ToString("N2");

                    vlTotalAtivPgto.Font.Strikeout = (decimal.Parse(vlTotalDesc.Text) > 0);
                }

                vlTotalPedidoPgto.Text = (SessionPedido.VlTotalPedido - vlDesconto).ToString("N2");

            }


            if (SessionEvento.CdEvento == "007701")
                lblMsgWCIT.Visible = true;

            TxtFormaPagamento.Focus();

            //if (SessionEvento.CdEvento == "008501")
            //{
            //    pnlDadosRecibo.Visible = false;
            //}

            //if (SessionIdioma == "PTBR")
            //{
            //    linhaPais.Visible = false;
            //}
            //else if (SessionIdioma == "ENUS")
            //{
            //    txtTipoPessoaRecibo.Items.Clear();

            //    ListItem item = new ListItem("A PERSON", "PF", true);
            //    txtTipoPessoaRecibo.Items.Add(item);

            //    ListItem item2 = new ListItem("A COMPANY", "PJ");
            //    txtTipoPessoaRecibo.Items.Add(item2);

            //    btnCEP.Visible = false;
            //    linhaCPF.Visible = false;
            //    linhaIE.Visible = false;
            //    linhaCNPJ.Visible = false;
            //    linhaBairro.Visible = false;

            //    linhaUF.Visible = false;
            //    linhaCidade.Visible = false;
            //    linhaRamalRespFin2.Visible = false;
            //}
            //else if (SessionIdioma == "ESP")
            //{
            //    txtTipoPessoaRecibo.Items.Clear();

            //    ListItem item = new ListItem("UNA PERSONA", "PF", true);
            //    txtTipoPessoaRecibo.Items.Add(item);

            //    ListItem item2 = new ListItem("UNA EMPRESA", "PJ");
            //    txtTipoPessoaRecibo.Items.Add(item2);

            //    btnCEP.Visible = false;
            //    linhaCPF.Visible = false;
            //    linhaIE.Visible = false;
            //    linhaCNPJ.Visible = false;
            //    linhaBairro.Visible = false;

            //    linhaUF.Visible = false;
            //    linhaCidade.Visible = false;
            //    linhaRamalRespFin2.Visible = false;
            //}
        }
        else
        {
            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            SessionPedido = (Pedido)Session["SessionPedido"];

            

            SessionIdioma = (String)Session["SessionIdioma"];
        }

        TSManager1.RegisterPostBackControl(btnConfirmarPgto);
        TSManager1.RegisterPostBackControl(btnCupomDesconto);
        TSManager1.RegisterPostBackControl(btnAlterarCupomDesconto);

        if (SessionEvento == null)
            Server.Transfer("frmSessaoExpirada.aspx", true);

        
        verificarIdioma(SessionIdioma);

       // FormasDePagamento3();
    }

    protected void verificarIdioma(string prmIdioma)
    {
        if (prmIdioma == "PTBR")
        {
            SessionIdioma = "PTBR";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Formas de Pagamento";
                        
            lblID.Text = "Nr Particpante:";
            lblPart.Text = "Participante:";
            lblCateg.Text = "Categoria:";


            lblTituloResumoPgto.Text = "Resumo do Pedido";
            lblResPedPgto.Text = "Pedido nº";
            lblResItensPgto.Text = "Itens";
            lblResVlrPgto.Text = "Valor";
            lblResDescPgto.Text = "Desconto";
            
            lblResVlrTotalPgto.Text = "Total (R$)";
            if ((SessionParticipante.NoPais == "BRASIL") ||
                (SessionParticipante.NoPais == "BRAZIL") ||
                (SessionParticipante.NoPais == "BRASIL") ||
                (SessionParticipante.NoPais == "BRÉSIL"))
                lblResVlrTotalPgto.Text = "Total (R$)";
            else
                lblResVlrTotalPgto.Text = "Total ($)";

            //lblTituloFormaPgto.Text = "Selecione a forma de pagamento";

            lblFormaPgto.Text = "Pagamento";

            btnConfirmarPgto.Text = "Concluir";

            lblTituloCupom.Text = "Informe o seu Cupom de Desconto";
            btnCupomDesconto.Text = "Aplicar";

            btnAlterarCupomDesconto.Text = "Alterar Código";
            lblAvisoDescontoCalculado.Text = "(Valor do desconto já calculado)";

            lblNomeTitularCartao.Text = "Nome do Titular";
            lblNrCartao.Text = "Número do Cartão";
            lblMesValidadeCartao.Text = "Mês de Validade";
            lblAnoValidadeCartao.Text = "Ano de Validade";
            lblCodSegCartao.Text = "Cód de Segurança do Cartão";

            if (SessionEvento.CdEvento == "007701")
            {
                btnConfirmarPgto.Text = "Enviar";
                //lblMsgWCIT.Text = ""
            }

            if (SessionEvento.CdEvento == "008501")
            {
               // lblTituloDadosRecibo.Text = "Dados para Nota Empenho";
            }

        }
        else if (prmIdioma == "ENUS")
        {
            SessionIdioma = "ENUS";
            Session["SessionIdioma"] = SessionIdioma;
                       

            lblTituloPagina.Text = "Payment Methods";

            lblID.Text = "Registration no.:";
            lblPart.Text = "Participant:";
            lblCateg.Text = "Category:";

            lblTituloResumoPgto.Text = "Summary";
            lblResPedPgto.Text = "Order No.";
            lblResItensPgto.Text = "Items";
            lblResVlrPgto.Text = "Fee";
            lblResDescPgto.Text = "Discount";


            lblResVlrTotalPgto.Text = "Total ($)";
            if ((SessionParticipante.NoPais == "BRASIL") ||
                (SessionParticipante.NoPais == "BRAZIL") ||
                (SessionParticipante.NoPais == "BRASIL") ||
                (SessionParticipante.NoPais == "BRÉSIL"))
                lblResVlrTotalPgto.Text = "Total (R$)";
            else
                lblResVlrTotalPgto.Text = "Total ($)";

            

            if (SessionParticipante.Categoria.NoCategoria.Contains("BRAZILIAN"))
                lblResVlrTotalPgto.Text = "Total (R$)";
            else
                lblResVlrTotalPgto.Text = "Total ($)";

            //lblTituloFormaPgto.Text = "Select a payment method";

            lblFormaPgto.Text = "Payment";

            btnConfirmarPgto.Text = "Finish";

            lblTituloCupom.Text = "Enter Discount Code";
            btnCupomDesconto.Text = "Go";

            btnAlterarCupomDesconto.Text = "Change Code";
            lblAvisoDescontoCalculado.Text = "(Discount amount already calculated)";

            lblNomeTitularCartao.Text = "Name of the card holder";
            lblNrCartao.Text = "Card Number";
            lblMesValidadeCartao.Text = "Expiration Month";
            lblAnoValidadeCartao.Text = "Expiration Year";
            lblCodSegCartao.Text = "Security Code";

            lblqtdParcelas.Text = "Installments";

            if (SessionEvento.CdEvento == "007701")
            {
                btnConfirmarPgto.Text = "Send";
                lblMsgWCIT.Text = "Code generated by the WCIT 2016 Brazil team for discounts for groups or any promotional activities. For more information, please contact us by phone +55 61 3327.1288 or by email wcit2016@assespro.org.br.";
            }

            //lblTituloDadosRecibo.Text = "Please choose the option below and fill in the form for the invoice:";
            
            //{
            //    lblTipoPessoa.Text = "Invoice for *";
            //    lblNome.Text = "Name *";
            //    Label15.Text = "Country *";
            //    Label12.Text = "ZIP/Postal Code *";
            //    Label13.Text = "Address *";
            //    Label151.Text = "City/State/Province *";
            //    Label8.Text = "E-mail to send the Invoice *";
            //    Label9.Text = "Telephone Number * - Enter including country code (e.g. +55 61 1234-5678)";
            //}

            //RequiredFieldValidator2.ErrorMessage = "Required field";
            //RequiredFieldValidator3.ErrorMessage = "Required field";
            //RequiredFieldValidator12.ErrorMessage = "Required field";
            //RequiredFieldValidator5.ErrorMessage = "Required field";
            //RequiredFieldValidator11.ErrorMessage = "Required field";
            //RequiredFieldValidator14.ErrorMessage = "Required field";
            //RequiredFieldValidator9.ErrorMessage = "Required field";
            //RequiredFieldValidator10.ErrorMessage = "Required field";
            //RequiredFieldValidator3.ErrorMessage = "Required field";
            //RequiredFieldValidator4.ErrorMessage = "Required field";
            //RequiredFieldValidator8.ErrorMessage = "Required field";
            //RequiredFieldValidator6.ErrorMessage = "Required field";
            //RequiredFieldValidator7.ErrorMessage = "Required field";
        }
        else if (prmIdioma == "ESP")
        {
            SessionIdioma = "ESP";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Formas de Pago";

            lblID.Text = "Nº de Registro:";
            lblPart.Text = "Participante:";
            lblCateg.Text = "Categoría:";

            lblTituloResumoPgto.Text = "Resúmen del pedido";
            lblResPedPgto.Text = "Pedido nº";
            lblResItensPgto.Text = "Itens";
            lblResVlrPgto.Text = "Valor";
            lblResDescPgto.Text = "Descuento";

            //if ((SessionIdioma == "PTBR") || ((SessionEvento.CdEvento == "007002") &&
            //    ((SessionParticipante.CdCategoria == "00700201") || (SessionParticipante.CdCategoria == "00700202") || (SessionParticipante.CdCategoria == "00700207")))
            //    //((SessionParticipante.NoPais == "BRASIL") ||
            //    // (SessionParticipante.NoPais == "BRAZIL") ||
            //    // (SessionParticipante.NoPais == "BRASIL") ||
            //    // (SessionParticipante.NoPais == "BRÉSIL"))

            //    )
            //    lblResVlrTotalPgto.Text = "Total ($)";
            //else
            //    lblResVlrTotalPgto.Text = "Total ($)";


            lblResVlrTotalPgto.Text = "Total ($)";
            if ((SessionParticipante.NoPais == "BRASIL") ||
                (SessionParticipante.NoPais == "BRAZIL") ||
                (SessionParticipante.NoPais == "BRASIL") ||
                (SessionParticipante.NoPais == "BRÉSIL"))
                lblResVlrTotalPgto.Text = "Total (R$)";
            else
                lblResVlrTotalPgto.Text = "Total ($)";

            //lblTituloFormaPgto.Text = "Seleccione forma de pago";

            lblFormaPgto.Text = "Pago";

            btnConfirmarPgto.Text = "Confirmar";

            lblTituloCupom.Text = "Tiene cupón de descuento?";
            btnCupomDesconto.Text = "Aplicar";

            btnAlterarCupomDesconto.Text = "Cambio de Cupón";
            lblAvisoDescontoCalculado.Text = "(El importe de descuento ya aplicado)";

            lblNomeTitularCartao.Text = "Nombre del titular";
            lblNrCartao.Text = "Número de la tarjeta";
            lblMesValidadeCartao.Text = "Mes de validad";
            lblAnoValidadeCartao.Text = "Año de validad";
            lblCodSegCartao.Text = "Código de seguridad de la tarjeta";

            lblqtdParcelas.Text = "Nº de pagos";

            //lblTituloDadosRecibo.Text = "Elija la opción siguiente y complete el formulario de la factura:";

            //lblTipoPessoa.Text = "Factura para *";
            //lblNome.Text = "Nombre *";
            //Label15.Text = "País *";
            //Label12.Text = "Código postal *";
            //Label13.Text = "Dirección *";
            //Label151.Text = "Ciudad/Estado/Provincia *";
            //Label8.Text = "E-mail para enviar la factura *";
            //Label9.Text = "Número de teléfono * - ingrese el código de país incluido (por ejemplo, +55 61 1234-5678)";

            
        }
        /*else if (prmIdioma == "FRA")
        {
            SessionIdioma = "FRA";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Demande d'inscription";
            lblTituloResumo.Text = "Résumé de la demande";

            lblPart.Text = "Participant:";
            lblCateg.Text = "Catégorie:";

            lblResPed.Text = "Demande no";
            lblResItens.Text = "Articles";
            lblResVlr.Text = "Valeur";
            lblResDesc.Text = "Réduction";
            lblResVlrTotal.Text = "Total";

            btnAvancar.Text = "Terminer";
            btnVerItensPedido.Text = "Continuer d`inscription";
            btnVoltarParaItens.Text = "Retour";

            lblFiltro.Text = "Filtre";
            lblTipoFiltro.Text = "Type";
            lblDtInicioFiltro.Text = "Accueil";

            lblTituloGrid1.Text = "Articles disponibles";
        }*/
    }


    protected void FormasDePagamento2()
    {

        TiposPagamentoCad oTiposPagamentoCad = new TiposPagamentoCad();
        //TxtFormaPagamento.DataSource = oTiposPagamentoCad.ListarWeb(SessionEvento.CdEvento, SessionCnn);
        DataTable dt = oTiposPagamentoCad.ListarWeb(SessionEvento.CdEvento, SessionCnn);
        this.TxtFormaPagamento.Items.Clear();

        if ((SessionEvento.CdEvento == "008501") && (SessionParticipante.CdCategoria != "00850102"))
        {
           dt.DefaultView.RowFilter ="dsTipoPagamento not like '%EMPENHO%'";
        }

        if ((SessionIdioma != "PTBR") || (SessionParticipante.NoPais.ToUpper() != "BRASIL"))
        {
            dt.DefaultView.RowFilter = "dsTipoPagamento not like '%EMPENHO%' AND dsTipoPagamento not like '%DÉBITO%'  AND dsTipoPagamento not like '%BOLETO%'";
        }

        if (dt != null)
        {
            for (int i = 0; i < dt.DefaultView.Count; i++)
            {
                ListItem item = new ListItem(
                    (
                    (!(dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("CARTAO")) && !(dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("CARTÃO")) ? dt.DefaultView[i]["dsTipoPagamento"].ToString() :
                     (SessionIdioma == "PTBR") ? dt.DefaultView[i]["dsTipoPagamento"].ToString() : (SessionIdioma == "ENUS") ? "CREDIT CARD" : "TARJETA DE CRÉDITO"))
                     ,
                    dt.DefaultView[i]["cdTipoPagamento"].ToString());

                if (dt.DefaultView[i]["dsTipoPagamento"].ToString().Trim() == "")
                {
                    if ((SessionIdioma == "PTBR") || ((SessionParticipante.CdCategoria == "00700201") || (SessionParticipante.CdCategoria == "00700202") || (SessionParticipante.CdCategoria == "00700207")))
                        item.Attributes.Add("data-description", "Selecione a forma de pagamento");
                    else if (SessionIdioma == "ENUS")
                        item.Attributes.Add("data-description", "Select a payment method");
                    else if (SessionIdioma == "ESP")
                        item.Attributes.Add("data-description", "Seleccione forma de pago");
                }

                if (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("BOLETO"))
                    item.Attributes.Add("data-image", ResolveUrl("~/img/") + "boleto.png");//idioma.Sigla + ".gif");
                else if ((dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("CARTAO")) || (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("CARTÃO")))
                    item.Attributes.Add("data-image", ResolveUrl("~/img/") + "cartao_credito.png");
                else if ((dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("EMPENHO")) || (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("DEPÓSITO")) || (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("DEPOSITO")) || (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("TRANSFERÊNCIA")) || (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("BANK TRANSFER")))
                    item.Attributes.Add("data-image", ResolveUrl("~/img/") + "empenho.png");
                else if (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("PAYPAL"))
                    item.Attributes.Add("data-image", ResolveUrl("~/img/") + "paypal.png");
                else if (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("PAGSEGURO"))
                    item.Attributes.Add("data-image", ResolveUrl("~/img/") + "pagseguro.png");
                else
                    item.Attributes.Add("data-image", ResolveUrl("~/img/") + "empenho.png");


                //this.TxtFormaPagamento.Items.Add(item);

                if (((SessionEvento.CdEvento != "007002") && (SessionEvento.CdEvento != "007701") && (SessionEvento.CdEvento != "008501") && (SessionEvento.CdEvento != "010801")))
                    this.TxtFormaPagamento.Items.Add(item);
                else if (SessionEvento.CdEvento == "007002")
                {
                    if ((dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("BOLETO")) || (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("EMPENHO")) || (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("DEPÓSITO")) || (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("DEPOSITO")) || (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("TRANSFERÊNCIA")))
                    {
                        //if ((SessionParticipante.NoPais == "BRASIL") || (SessionParticipante.NoPais == "BRAZIL") || (SessionParticipante.NoPais == "BRASIL") || (SessionParticipante.NoPais == "BRÉSIL"))
                        //if ((SessionParticipante.CdCategoria == "00700201") || (SessionParticipante.CdCategoria == "00700202") || (SessionParticipante.CdCategoria == "00700207"))
                        if (SessionParticipante.Categoria.NoCategoria.Contains("BRAZILIAN"))
                            this.TxtFormaPagamento.Items.Add(item);
                    }
                    else if (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("BANK TRANSFER"))
                    {
                        //if ((SessionParticipante.NoPais != "BRASIL") && (SessionParticipante.NoPais != "BRAZIL") && (SessionParticipante.NoPais != "BRASIL") && (SessionParticipante.NoPais != "BRÉSIL"))
                        //if ((SessionParticipante.CdCategoria != "00700201") && (SessionParticipante.CdCategoria != "00700202") && (SessionParticipante.CdCategoria != "00700207"))
                        if (!SessionParticipante.Categoria.NoCategoria.Contains("BRAZILIAN"))
                            this.TxtFormaPagamento.Items.Add(item);
                    }
                    else
                        this.TxtFormaPagamento.Items.Add(item);
                }
                else if (SessionEvento.CdEvento == "007701")
                {
                    if ((dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("BOLETO")) || (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("EMPENHO")) || (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("DEPÓSITO")) || (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("DEPOSITO")) || (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("TRANSF")))
                    {
                        if ((SessionParticipante.NoPais == "BRASIL") || (SessionParticipante.NoPais == "BRAZIL") || (SessionParticipante.NoPais == "BRASIL") || (SessionParticipante.NoPais == "BRÉSIL"))
                            //if ((SessionParticipante.CdCategoria == "00700201") || (SessionParticipante.CdCategoria == "00700202") || (SessionParticipante.CdCategoria == "00700207"))
                            this.TxtFormaPagamento.Items.Add(item);
                    }
                    else if ((dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("CARTAO")) || (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("CARTÃO")))
                    {
                        //if ((SessionParticipante.NoPais != "BRASIL") && (SessionParticipante.NoPais != "BRAZIL") && (SessionParticipante.NoPais != "BRASIL") && (SessionParticipante.NoPais != "BRÉSIL"))
                        this.TxtFormaPagamento.Items.Add(item);
                    }
                    else
                        this.TxtFormaPagamento.Items.Add(item);
                }
                else if (SessionEvento.CdEvento == "008501")
                {
                    if ((SessionIdioma == "PTBR") &&
                        ((dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("BOLETO")) ||
                        (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("EMPENHO")) ||
                        (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("DEPÓSITO")) ||
                        (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("DEPOSITO")) ||
                        (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("TRANSF"))))
                    {
                        //if ((SessionParticipante.NoPais == "BRASIL") || (SessionParticipante.NoPais == "BRAZIL") || (SessionParticipante.NoPais == "BRASIL") || (SessionParticipante.NoPais == "BRÉSIL"))
                        //if ((SessionParticipante.CdCategoria == "00700201") || (SessionParticipante.CdCategoria == "00700202") || (SessionParticipante.CdCategoria == "00700207"))
                        this.TxtFormaPagamento.Items.Add(item);
                    }
                    else if ((dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("CARTAO")) || (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("CARTÃO")) || (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("CREDIT")) || (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("TARJETA")))
                    {
                        //if ((SessionParticipante.NoPais != "BRASIL") && (SessionParticipante.NoPais != "BRAZIL") && (SessionParticipante.NoPais != "BRASIL") && (SessionParticipante.NoPais != "BRÉSIL"))
                        this.TxtFormaPagamento.Items.Add(item);
                    }
                    else
                        this.TxtFormaPagamento.Items.Add(item);
                }
                else if ((SessionEvento.CdEvento == "010801") || (SessionEvento.CdEvento == "000103"))
                {
                    if ((SessionIdioma == "PTBR") &&
                        ((dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("BOLETO")) ||
                        (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("EMPENHO")) ||
                        (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("DEPÓSITO")) ||
                        (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("DEPOSITO")) ||
                        (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("TRANSFERÊNCIA")) ||
                        (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("DÉBITO"))
                        ))
                    {
                        //if ((SessionParticipante.NoPais == "BRASIL") || (SessionParticipante.NoPais == "BRAZIL") || (SessionParticipante.NoPais == "BRASIL") || (SessionParticipante.NoPais == "BRÉSIL"))
                        //if ((SessionParticipante.CdCategoria == "00700201") || (SessionParticipante.CdCategoria == "00700202") || (SessionParticipante.CdCategoria == "00700207"))
                        this.TxtFormaPagamento.Items.Add(item);
                    }
                    else if ((dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("CARTAO")) || (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("CARTÃO")) || (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("CREDIT")) || (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("TARJETA")))
                    {
                        //if ((SessionParticipante.NoPais != "BRASIL") && (SessionParticipante.NoPais != "BRAZIL") && (SessionParticipante.NoPais != "BRASIL") && (SessionParticipante.NoPais != "BRÉSIL"))
                        this.TxtFormaPagamento.Items.Add(item);
                    }
                    else if (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("BANK TRANSFER"))
                    {
                        //if ((SessionParticipante.NoPais != "BRASIL") && (SessionParticipante.NoPais != "BRAZIL") && (SessionParticipante.NoPais != "BRASIL") && (SessionParticipante.NoPais != "BRÉSIL"))
                        //if ((SessionParticipante.CdCategoria != "00700201") && (SessionParticipante.CdCategoria != "00700202") && (SessionParticipante.CdCategoria != "00700207"))
                        if ((SessionParticipante.NoPais != "BRASIL") && (SessionParticipante.NoPais != "BRAZIL"))
                            this.TxtFormaPagamento.Items.Add(item);
                    }
                    else
                        this.TxtFormaPagamento.Items.Add(item);
                }
            }

            if (TxtFormaPagamento.Items.Count >= 1)
            {
                EventArgs ev = new EventArgs();
                TxtFormaPagamento_SelectedIndexChanged(TxtFormaPagamento, ev);
            }
        }
        
        


        //TxtFormaPagamento.DataTextField = "dsTipoPagamento";
        //TxtFormaPagamento.DataValueField = "cdTipoPagamento";
        //TxtFormaPagamento.DataBind();
    }

    protected void FormasDePagamento3()
    {

        TiposPagamentoCad oTiposPagamentoCad = new TiposPagamentoCad();
        
        DataTable dt = oTiposPagamentoCad.ListarWeb(SessionEvento.CdEvento, SessionCnn);

        if (dt != null)
        {
            if ((SessionEvento.CdEvento == "007002") ||
                (SessionEvento.CdEvento == "007701"))
            {
                DataTable dt2 = new DataTable();
                dt2.Columns.Add("dsTipoPagamento");
                dt2.Columns.Add("cdTipoPagamento");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if ((dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("BOLETO")) || (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("EMPENHO")) || (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("DEPÓSITO")) || (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("DEPOSITO")) || (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("TRANSFERÊNCIA")))
                    {
                        if ((SessionEvento.CdEvento == "007701") && ((SessionParticipante.NoPais == "BRASIL") || (SessionParticipante.NoPais == "BRAZIL") || (SessionParticipante.NoPais == "BRASIL") || (SessionParticipante.NoPais == "BRÉSIL")))
                            dt2.Rows.Add(dt.DefaultView[i]["dsTipoPagamento"].ToString(), dt.DefaultView[i]["cdTipoPagamento"].ToString());
                        //if ((SessionParticipante.CdCategoria == "00700201") || (SessionParticipante.CdCategoria == "00700202") || (SessionParticipante.CdCategoria == "00700207"))
                        if (SessionParticipante.Categoria.NoCategoria.Contains("BRAZILIAN"))
                            dt2.Rows.Add(dt.DefaultView[i]["dsTipoPagamento"].ToString(), dt.DefaultView[i]["cdTipoPagamento"].ToString());
                    }
                    else if (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("BANK TRANSFER"))
                    {
                        //if ((SessionParticipante.NoPais != "BRASIL") && (SessionParticipante.NoPais != "BRAZIL") && (SessionParticipante.NoPais != "BRASIL") && (SessionParticipante.NoPais != "BRÉSIL"))
                        //if ((SessionParticipante.CdCategoria != "00700201") && (SessionParticipante.CdCategoria != "00700202") && (SessionParticipante.CdCategoria != "00700207"))
                        if ((!SessionParticipante.Categoria.NoCategoria.Contains("BRAZILIAN")) )
                            dt2.Rows.Add(dt.DefaultView[i]["dsTipoPagamento"].ToString(), dt.DefaultView[i]["cdTipoPagamento"].ToString());
                    }
                    else
                        dt2.Rows.Add(dt.DefaultView[i]["dsTipoPagamento"].ToString(), dt.DefaultView[i]["cdTipoPagamento"].ToString());
                }
                dt = dt2;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                try
                {
                    if (dt.DefaultView[i]["dsTipoPagamento"].ToString().Trim() == "")
                    {
                        if ((SessionIdioma == "PTBR") || ((SessionParticipante.CdCategoria == "00700201") || (SessionParticipante.CdCategoria == "00700202") || (SessionParticipante.CdCategoria == "00700207")))
                            TxtFormaPagamento.Items[i].Attributes.Add("data-description", "Selecione a forma de pagamento");
                        else if (SessionIdioma == "ENUS")
                            TxtFormaPagamento.Items[i].Attributes.Add("data-description", "Select a payment method");
                        else if (SessionIdioma == "ESP")
                            TxtFormaPagamento.Items[i].Attributes.Add("data-description", "Seleccionar el pago");
                    }

                    if (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("BOLETO"))
                        TxtFormaPagamento.Items[i].Attributes.Add("data-image", ResolveUrl("~/img/") + "boleto.png");//idioma.Sigla + ".gif");
                    else if ((dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("CARTAO")) || (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("CARTÃO")))
                        TxtFormaPagamento.Items[i].Attributes.Add("data-image", ResolveUrl("~/img/") + "cartao_credito.png");
                    else if ((dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("EMPENHO")) || (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("DEPÓSITO")) || (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("DEPOSITO")) || (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("TRANSFERÊNCIA")) || (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("BANK TRANSFER")))
                        TxtFormaPagamento.Items[i].Attributes.Add("data-image", ResolveUrl("~/img/") + "empenho.png");
                    else if (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("PAYPAL"))
                        TxtFormaPagamento.Items[i].Attributes.Add("data-image", ResolveUrl("~/img/") + "paypal.png");
                    else if (dt.DefaultView[i]["dsTipoPagamento"].ToString().ToUpper().Contains("PAGSEGURO"))
                        TxtFormaPagamento.Items[i].Attributes.Add("data-image", ResolveUrl("~/img/") + "pagseguro.png");
                    else
                        TxtFormaPagamento.Items[i].Attributes.Add("data-image", ResolveUrl("~/img/") + "empenho.png");
                }
                catch
                { }
            }

            if (TxtFormaPagamento.Items.Count >= 1)
            {
                EventArgs ev = new EventArgs();
                TxtFormaPagamento_SelectedIndexChanged(TxtFormaPagamento, ev);
            }

        }



        //TxtFormaPagamento.DataTextField = "dsTipoPagamento";
        //TxtFormaPagamento.DataValueField = "cdTipoPagamento";
        //TxtFormaPagamento.DataBind();
    }

    protected void FormasDePagamento()
    {
        TiposPagamentoCad oTiposPagamentoCad = new TiposPagamentoCad();
        TxtFormaPagamento.DataSource = oTiposPagamentoCad.ListarWeb(SessionEvento.CdEvento, SessionCnn);
        TxtFormaPagamento.DataTextField = "dsTipoPagamento";
        TxtFormaPagamento.DataValueField = "cdTipoPagamento";
        TxtFormaPagamento.DataBind();
    }

    protected void QuantidadeParcelas()
    {
        txtQtdParcelas.DataSource = null;
        txtQtdParcelas.DataBind();
        txtQtdParcelas.Items.Clear();
        QuantidadeParcelasCad oQuantidadeParcelasCad = new QuantidadeParcelasCad();
        DataTable dtParcelas = oQuantidadeParcelasCad.ListarValidas(SessionEvento.CdEvento, TxtFormaPagamento.Text, SessionCnn);
        if (dtParcelas == null)
        {
            dtParcelas = new DataTable();
            dtParcelas.Columns.Add("qtdParcelas");
            dtParcelas.Rows.Add("1");
        }
        txtQtdParcelas.DataSource = dtParcelas;
        txtQtdParcelas.DataTextField = "qtdParcelas";
        txtQtdParcelas.DataValueField = "qtdParcelas";
        txtQtdParcelas.DataBind();
    }

    public void CarregarAnoValidadeCartao()
    {
        int anoPadrao = DateTime.Now.Year;// -2000;
        txtAnoValidadeCartao.Items.Add("");
        txtAnoValidadeCartao.Items.Add(anoPadrao.ToString());
        for (int i = 1; i < 20; i++)
            txtAnoValidadeCartao.Items.Add((anoPadrao + i).ToString());
    }

    //public void ListarUFRecibo()
    //{

    //    Geral oGeral = new Geral();
    //    txtUFRecibo.DataSource = oGeral.ListarUFs(SessionCnn);
    //    txtUFRecibo.DataTextField = "dsUF";
    //    txtUFRecibo.DataValueField = "dsUF";
    //    txtUFRecibo.DataBind();

    //}

    protected void ListarCidades(string prmUF)
    {
        //Geral oGeral = new Geral();

        //txtCidadeRecibo.DataSource = oGeral.ListarCidades(prmUF, SessionCnn);
        //txtCidadeRecibo.DataTextField = "dsMunicipio";
        //txtCidadeRecibo.DataValueField = "dsMunicipio";
        //txtCidadeRecibo.DataBind();
    }

    //protected void PesquisarDadosRecibo()
    //{

    //    if (SessionParticipante == null)
    //        return;



    //    //PedidoCad oPedidoCad = new PedidoCad();

    //    //Pedido oPedido = oPedidoCad.Pesquisar(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionEvento.CdEvento + prmCdParticipante + "001", SessionCnn);

    //    if ((SessionPedido == null) || (SessionPedido.DsNomeRecibo.Trim() == ""))
    //    {
    //        //txtCPFCNPJRecibo.Text = oClsFuncoes.CPFCNPJMascarar(SessionParticipante.NuCPFCNPJ);
    //        //txtNomeRecibo.Text = SessionParticipante.NoParticipante;
    //        //txtUFRecibo.Text = SessionParticipante.DsUF;
    //        //txtCidadeRecibo.Text = SessionParticipante.NoCidade;
    //        //txtEnderecoRecibo.Text = SessionParticipante.DsEndereco;
    //        //txtCEPRecibo.Text = oClsFuncoes.MascaraGerar(SessionParticipante.NuCEP, "99.999-999");
    //    }
    //    else
    //    {
    //        txtTipoPessoaRecibo.SelectedValue = SessionPedido.TpPessoa;
    //        if (txtTipoPessoaRecibo.SelectedValue.ToString() == "PF")
    //        {
    //            linhaCPF.Visible = true;
    //            linhaCNPJ.Visible = false;
    //            linhaIE.Visible = false;
    //            lblNome.Text = "Nome*";
    //            txtCPFRecibo.Text = oClsFuncoes.CPFCNPJMascarar(SessionPedido.NuCPFCNPJRecibo);
    //        }
    //        else if (txtTipoPessoaRecibo.SelectedValue.ToString() == "PJ")
    //        {
    //            linhaCPF.Visible = false;
    //            linhaCNPJ.Visible = true;
    //            linhaIE.Visible = true;
    //            lblNome.Text = "Razão Social*";
    //            txtCNPJRecibo.Text = oClsFuncoes.CPFCNPJMascarar(SessionPedido.NuCPFCNPJRecibo);
    //            txtIE.Text = SessionPedido.DsInscricaoEstadualRecibo;
    //        }

    //        txtTipoPessoaRecibo.AutoPostBack = true;

    //        TxtFormaPagamento.Text = SessionPedido.TpPagamento;
    //        txtNomeRecibo.Text = SessionPedido.DsNomeRecibo;
    //        txtUFRecibo.Text = SessionPedido.DsUFRecibo;
    //        txtCidadeRecibo.Text = SessionPedido.NoCidadeRecibo;
    //        txtEnderecoRecibo.Text = SessionPedido.DsEnderecoRecibo;
    //        txtComplementoEnderecoRecibo.Text = SessionPedido.DsComplementoEnderRecibo;
    //        txtBairroRecibo.Text = SessionPedido.NoBairroRecibo;
    //        txtCEPRecibo.Text = oClsFuncoes.MascaraGerar(SessionPedido.NuCEPRecibo, "99.999-999");

    //        txtNomeRespFin.Text = SessionPedido.DsNomeResponsavelFinanceiro;
    //        txtEmailRespFin.Text = SessionPedido.DsEmailResponsavelFinanceiro;
    //        txtFoneRespFin.Text = oClsFuncoes.MascaraGerar(SessionPedido.DsFoneResponsavelFinanceiro, "(99) 999999999"); ;
    //        txtRamalRespFin.Text = SessionPedido.DsRamalResponsavelFinanceiro;

    //    }
    //}

    protected void vlCategoria(string prmCdCategoria)
    {
        //CategoriaCad oCategoriaCad = new CategoriaCad();
        //Categoria oCategoria = oCategoriaCad.Pesquisar(SessionEvento.CdEvento, prmCdCategoria, SessionCnn);
        //lblMsg.Text = "";
        //decimal tmpVlPgto = 0;
        //if (oCategoria != null)
        //{
        //    tmpVlPgto = oCategoria.VlPagamento;

        //    if ((prmCdCategoria != "00020103") && (prmCdCategoria != "00020104"))
        //    {
        //        DescontoCad oDescontoCad = new DescontoCad();
        //        Desconto oDescontoParticipante = oDescontoCad.PesquisarDescParticipante(SessionEvento.CdEvento, txtMatricula.Text, SessionCnn);
        //        if (oDescontoParticipante != null)
        //        {
        //            if (oDescontoParticipante.TpDesconto == "0")
        //                tmpVlPgto = tmpVlPgto - oDescontoParticipante.VlDesconto;//desc R$
        //            else
        //                tmpVlPgto = tmpVlPgto - Decimal.Round((tmpVlPgto * (oDescontoParticipante.VlDesconto / 100)), 2);//desc %
        //        }
        //    }
        //    //txtValorTotal.Text = decimal.Parse(oCategoria.VlPagamento.ToString()).ToString("N2");
        //}
        //txtValorTotal.Text = decimal.Parse(tmpVlPgto.ToString()).ToString("N2");
    }

    protected void btnConfirmar_Click(object sender, EventArgs e)
    {
        //this.MessageBox1.ShowConfirmation("Deseja gravar os dados?", "Gravar", true, false);

        lblMsgPgto.Visible = false;

        if ((pnlInfoPgto.Visible) && (TxtFormaPagamento.SelectedItem.Text.Trim() == ""))
        {
            lblMsgPgto.Visible = true;
            lblMsgPgto.Text = "Selecione uma forma de pagamento!";
            TxtFormaPagamento.Focus();
            return;
        }

        if (SessionEvento.CdEvento == "007002")
        {
            if ((TxtFormaPagamento.SelectedItem.Text.ToUpper().Contains("BOLETO")) || (TxtFormaPagamento.SelectedItem.Text.ToUpper().Contains("EMPENHO")) || (TxtFormaPagamento.SelectedItem.Text.ToUpper().Contains("DEPÓSITO")) || (TxtFormaPagamento.SelectedItem.Text.Contains("DEPOSITO")) || (TxtFormaPagamento.SelectedItem.Text.ToUpper().Contains("TRANSFEREN")))
            {
                //if ((SessionParticipante.NoPais != "BRASIL") && (SessionParticipante.NoPais != "BRAZIL") && (SessionParticipante.NoPais != "BRASIL") && (SessionParticipante.NoPais != "BRÉSIL"))
                //if ((SessionParticipante.CdCategoria != "00700201") && (SessionParticipante.CdCategoria != "00700202") || (SessionParticipante.CdCategoria == "00700207"))
                if (!SessionParticipante.Categoria.NoCategoria.Contains("BRAZILIAN"))
                {
                    lblMsgPgto.Visible = true;
                    lblMsgPgto.Text = "Payment only for Brazilians.";
                    TxtFormaPagamento.Focus();
                    return;
                }
            }
            else if (TxtFormaPagamento.SelectedItem.Text.ToUpper().Contains("BANK TRANSFER"))
            {
                //if ((SessionParticipante.NoPais == "BRASIL") || (SessionParticipante.NoPais == "BRAZIL") || (SessionParticipante.NoPais == "BRASIL") || (SessionParticipante.NoPais == "BRÉSIL"))
                //if ((SessionParticipante.CdCategoria == "00700201") || (SessionParticipante.CdCategoria == "00700202"))
                if (SessionParticipante.Categoria.NoCategoria.Contains("BRAZILIAN"))
                {
                    lblMsgPgto.Visible = true;
                    lblMsgPgto.Text = "Payment denied for Brazilians.";
                    TxtFormaPagamento.Focus();
                    return;
                }
            }
            
        }


        prp_Gravar();
    }
    protected void MessageBox1_YesChoosed(object sender, string Key)
    {
        if (Key == "Gravar")
        {
            prp_Gravar();
        }
    }
    
    protected void prp_Gravar()
    {
        lblMsgPgto.Text = "";
        if (lblIdentificador.Text.Trim() == "")
            return;

        if ((pnlInfoPgto.Visible) && (TxtFormaPagamento.SelectedItem.Text.Trim() == ""))
        {
            lblMsgPgto.Visible = true;
            lblMsgPgto.Text = "Selecione uma forma de pagamento!";
            TxtFormaPagamento.Focus();
            return;
        }

        //if (TxtFormaPagamento.SelectedItem.Text.ToUpper().Contains("EMPENHO"))
        //{
        //    if (txtNomeRespFin.Text == "")
        //    {
        //        lblMsgPgto.Visible = true;
        //        lblMsgPgto.Text = "Informe o nome do departamento responsável pelo pagamento!";
        //        txtNomeRespFin.Focus();
        //        return;
        //    }
        //    if (txtEmailRespFin.Text == "")
        //    {
        //        lblMsgPgto.Visible = true;
        //        lblMsgPgto.Text = "Informe o e-mail do departamento responsável pelo pagamento!";
        //        txtEmailRespFin.Focus();
        //        return;
        //    }

        //    if (txtFoneRespFin.Text == "")
        //    {
        //        lblMsgPgto.Visible = true;
        //        lblMsgPgto.Text = "Informe o fone do departamento responsável pelo pagamento!";
        //        txtFoneRespFin.Focus();
        //        return;
        //    }
        //}

        TiposPagamentoCad oTiposPagamentoCad = new TiposPagamentoCad();
        TiposPagamento tpPagamento = oTiposPagamentoCad.Pesquisar(SessionEvento.CdEvento, TxtFormaPagamento.SelectedValue.ToString(), SessionCnn);

       // SessionPedido.TpPessoa = txtTipoPessoaRecibo.SelectedValue.ToString();
       // SessionPedido.DsNomeRecibo = txtNomeRecibo.Text;
       // SessionPedido.NuCPFCNPJRecibo = txtTipoPessoaRecibo.SelectedValue.ToString() == "PF" ? txtCPFRecibo.Text.Replace(".", "").Replace("-", "").Replace("/", "") : txtCNPJRecibo.Text.Replace(".", "").Replace("-", "").Replace("/", "");
       // SessionPedido.DsUFRecibo = txtUFRecibo.Text;
       // SessionPedido.NoCidadeRecibo = txtCidadeRecibo.Text;
       // SessionPedido.DsEnderecoRecibo = txtEnderecoRecibo.Text;
       // SessionPedido.DsComplementoEnderRecibo = txtComplementoEnderecoRecibo.Text;
       // SessionPedido.NoBairroRecibo = txtBairroRecibo.Text;
       // SessionPedido.NuCEPRecibo = txtCEPRecibo.Text;
       // SessionPedido.DsInscricaoEstadualRecibo = txtIE.Text;
       //// SessionPedido.TpPagamento = TxtFormaPagamento.SelectedItem.Text;


       // SessionPedido.DsNomeResponsavelFinanceiro = txtNomeRespFin.Text.ToUpper();
       // SessionPedido.DsEmailResponsavelFinanceiro = txtEmailRespFin.Text.ToLower();
       // SessionPedido.DsFoneResponsavelFinanceiro = txtFoneRespFin.Text.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "");
       // SessionPedido.DsRamalResponsavelFinanceiro = txtRamalRespFin.Text.ToUpper();

        //if ((TxtFormaPagamento.SelectedItem.Text.ToUpper().Contains("BANK TRANSFER")) && ((SessionEvento.CdEvento == "010801") || (SessionEvento.CdEvento == "000103")))
        //{
        //     SessionPedido.TpPessoa = txtTipoPessoaRecibo.SelectedValue.ToString();
        //     SessionPedido.DsNomeRecibo = txtNomeRecibo.Text;
        //    // SessionPedido.NuCPFCNPJRecibo = txtTipoPessoaRecibo.SelectedValue.ToString() == "PF" ? txtCPFRecibo.Text.Replace(".", "").Replace("-", "").Replace("/", "") : txtCNPJRecibo.Text.Replace(".", "").Replace("-", "").Replace("/", "");
        //    // SessionPedido.DsUFRecibo = txtUFRecibo.Text;
        //    // SessionPedido.NoCidadeRecibo = txtCidadeRecibo.Text;
        //     SessionPedido.DsEnderecoRecibo = txtEnderecoRecibo.Text;
        //     SessionPedido.DsComplementoEnderRecibo = txtComplementoEnderecoRecibo.Text;
        //    // SessionPedido.NoBairroRecibo = txtBairroRecibo.Text;
        //     SessionPedido.NuCEPRecibo = txtCEPRecibo.Text;
        //    // SessionPedido.DsInscricaoEstadualRecibo = txtIE.Text;
            
        //    // SessionPedido.DsNomeResponsavelFinanceiro = txtNomeRespFin.Text.ToUpper();
        //     SessionPedido.DsEmailResponsavelFinanceiro = txtEmailRespFin2.Text.ToLower();
        //     SessionPedido.DsFoneResponsavelFinanceiro = txtFoneRespFin2.Text.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "");
        //    // SessionPedido.DsRamalResponsavelFinanceiro = txtRamalRespFin.Text.ToUpper();
        //}

        decimal tmpVlDescontoCupom = 0;

        if ((divCupomDesconto.Visible) && (txtCupomDesconto.Text != ""))
        {
            decimal vlDesconto = 0;

            DescontoCupom oDescontoCupom;
            DescontoCupomCad oDescontoCupomCad = new DescontoCupomCad();

            oDescontoCupom = oDescontoCupomCad.PesquisarPorCupom(SessionEvento.CdEvento, txtCupomDesconto.Text.ToUpper(), SessionCnn);

            if (oDescontoCupom == null)
            {
                if (SessionIdioma == "PTBR")
                    lblMsgCupom.Text = "Cupom de desconto inválido.";
                else if (SessionIdioma == "ENUS")
                    lblMsgCupom.Text = "Invalid discount code.";
                else if (SessionIdioma == "ESP")
                    lblMsgCupom.Text = "Cupón de descuento no es válido.";

                lblMsgCupom.Visible = true;
                txtCupomDesconto.Focus();
                return;
            }
            else if ((oDescontoCupom.DtValidadeDesconto != null) && (oDescontoCupom.DtValidadeDesconto < Geral.datahoraServidor(SessionCnn)))
            {
                if (SessionIdioma == "PTBR")
                    lblMsgCupom.Text = "Cupom de desconto expirado.";
                else if (SessionIdioma == "ENUS")
                    lblMsgCupom.Text = "Discount code expired.";
                else if (SessionIdioma == "ESP")
                    lblMsgCupom.Text = "Cupón de descuento expirado.";

                lblMsgCupom.Visible = true;
                txtCupomDesconto.Focus();
                return;
            }
            else if ((oDescontoCupom.QtdLimteUso != 0) && (oDescontoCupom.QtdLimteUso <= oDescontoCupom.QtdUtilizado))
            {
                if (((oDescontoCupom.QtdLimteUso == 1) && (oDescontoCupom.CdParticipanteUso != SessionParticipante.CdParticipante)) ||
                    ((SessionPedido.pedidoCupomDesconto == null) || (SessionPedido.pedidoCupomDesconto.CdCupomDesconto == "")))
                {
                    if (SessionIdioma == "PTBR")
                        lblMsgCupom.Text = "Cupom de desconto já utilizado.";
                    else if (SessionIdioma == "ENUS")
                        lblMsgCupom.Text = "Discount code already used.";
                    else if (SessionIdioma == "ESP")
                        lblMsgCupom.Text = "Cupón de descuento ya se utiliza.";

                    lblMsgCupom.Visible = true;
                    txtCupomDesconto.Focus();
                    return;
                }
            }

            vlDesconto = oDescontoCupom.VlDesconto;

            if (oDescontoCupom.TpDesconto.ToUpper() != "REAL")
            {
                vlDesconto = Math.Round(SessionPedido.VlTotalPedido * (vlDesconto / 100), 2);
            }

            if (oDescontoCupom.QtdLimteUso == 1)
            {
                oDescontoCupomCad.DescontoCupomMarcarParticipanteUso(SessionEvento.CdEvento, oDescontoCupom.CdCupomDesconto, SessionParticipante.CdParticipante, SessionCnn);
            }

            if ((SessionPedido.pedidoCupomDesconto == null) || (SessionPedido.pedidoCupomDesconto.CdCupomDesconto == ""))
            {
                oDescontoCupomCad.IncrementaUso(SessionEvento.CdEvento, oDescontoCupom.CdCupomDesconto, SessionCnn);
            }

            tmpVlDescontoCupom = vlDesconto;

            oPedidoCad.oPedidoCupomDescontoCad.GravarPedidoCupomDesconto(
                SessionPedido.CdEvento, 
                SessionPedido.CdPedido, 
                oDescontoCupom.CdCupomDesconto,
                oDescontoCupom.DsCupomDesconto,
                vlDesconto,
                SessionCnn);

            SessionParticipante.DsCampoExtraPreCad = oDescontoCupom.DsCupomDesconto; 
            if ((oDescontoCupom.CdCategoria != "") && (oDescontoCupom.CdCategoria != SessionParticipante.CdCategoria))
            {
                SessionParticipante.CdCategoria = oDescontoCupom.CdCategoria;                
            }

            SessionParticipante = oParticipanteCad.Gravar(SessionParticipante, SessionCnn);
            Session["SessionParticipante"] = SessionParticipante;
            
        }



        
        if (SessionPedido.pedidoCupomDesconto != null)
            tmpVlDescontoCupom = SessionPedido.pedidoCupomDesconto.VlDescontoCalculado;

        bool nrParcelasMudado = false;

        if (SessionPedido.QtdParcelas != int.Parse(txtQtdParcelas.Text))
            nrParcelasMudado = true;

        SessionPedido.QtdParcelas = int.Parse(txtQtdParcelas.Text);

        if ((SessionEvento.CdEvento == "007002") && (SessionPedido.TpPessoa == "PJ") && ((SessionIdioma == "PTBR") || (SessionParticipante.Categoria.NoCategoria.Contains("BRAZILIAN"))))
        {
            SessionPedido.VlPIS = CalcularVlPIS((SessionPedido.VlTotalPedido - tmpVlDescontoCupom));
            SessionPedido.VlCOFINS = CalcularVlCOFINS((SessionPedido.VlTotalPedido - tmpVlDescontoCupom));
            SessionPedido.VlISS = CalcularVlISS((SessionPedido.VlTotalPedido - tmpVlDescontoCupom), SessionPedido.NoCidadeRecibo);
        }
        else
        {
            SessionPedido.VlPIS = 0;
            SessionPedido.VlCOFINS = 0;
            SessionPedido.VlISS = 0;
        }

        SessionPedido = oPedidoCad.Gravar(SessionPedido, SessionCnn);
        Session["SessionPedido"] = SessionPedido;

        Geral oGeral = new Geral();

               

        


        if ((SessionPedido.VlTotalPedido - tmpVlDescontoCupom) == 0)
        {
            SessionPedido.TpPagamento = SessionIdioma == "PTBR" ? "CORTESIA" : SessionIdioma == "ENUS" ? "DISCOUNT" : "CORTESÍA" ; 
            SessionPedido = oPedidoCad.Gravar(SessionPedido, SessionCnn);
            Session["SessionPedido"] = SessionPedido;

            oReciboCad.IncluirCursosPedidoNaMatricula(SessionEvento.CdEvento, SessionPedido.CdPedido, SessionParticipante.CdParticipante, "000000001", SessionCnn);
            
            oPedidoCad.MarcarPedidoComoPago(SessionPedido, SessionCnn);

            if (!SessionParticipante.Categoria.FlConfirmacaoCadWeb)
                oGeral.EnviarEmailConfirmaCadastro(SessionEvento, SessionParticipante, SessionCnn);

            SessionParticipante = oParticipanteCad.Pesquisar(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);
            Session["SessionParticipante"] = SessionParticipante;

            Response.Redirect("frm_mensagens.aspx?cdMensagem=014&dsMensagem=", false);
            return;
            //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
            //                    "014",
            //                    ""), true);
        }

        decimal vlPgto = (SessionPedido.VlTotalPedido - tmpVlDescontoCupom) - SessionPedido.VlPIS - SessionPedido.VlCOFINS - SessionPedido.VlISS;
        decimal vlBoleto = vlPgto / (decimal)int.Parse(txtQtdParcelas.Text);
        DateTime dtVenc = DateTime.Parse(SessionPedido.DtVencimentoPedido.ToString());// Geral.datahoraServidor(SessionCnn).AddDays(SessionEvento.NuDiasPrimeiroVencimento);

        /*        
        if (dtVenc > SessionEvento.DtFechamentoInscrWeb)
            dtVenc = DateTime.Parse(SessionEvento.DtFechamentoInscrWeb.ToString());
        else if (dtVenc > SessionPedido.DtVencimentoPedido)
            dtVenc = DateTime.Parse(SessionPedido.DtVencimentoPedido.ToString());
        */
        if ((TxtFormaPagamento.SelectedItem.Text.ToUpper().Contains("BOLETO")) ||
            ((SessionEvento.CdCliente == "0013") && (TxtFormaPagamento.SelectedItem.Text.ToUpper().Contains("EMPENHO"))))
        {
            #region BOLETO

            //if (dtVenc > SessionEvento.DtLimitePagamentoBoleto)
            //    dtVenc = DateTime.Parse(SessionEvento.DtLimitePagamentoBoleto.ToString());

            //if ((SessionEvento.CdCliente == "0013") && (TxtFormaPagamento.SelectedItem.Text.ToUpper().Contains("EMPENHO")))//CONASEMS
            //    dtVenc = DateTime.Parse(SessionEvento.DtLimitePagamentoBoleto.ToString());

            DateTime dtMenorVencDesconto = DescontoCad.datahoraMenorVencimentoDesconto(SessionEvento.CdEvento, SessionCnn);
            if ((dtMenorVencDesconto != null) && (dtVenc > dtMenorVencDesconto))
                dtVenc = dtMenorVencDesconto;

            if (dtVenc > SessionEvento.DtLimitePagamentoBoleto)
                dtVenc = DateTime.Parse(SessionEvento.DtLimitePagamentoBoleto.ToString());

            if ((SessionEvento.CdCliente == "0013") && (TxtFormaPagamento.SelectedItem.Text.ToUpper().Contains("EMPENHO")))//CONASEMS
                dtVenc = DateTime.Parse(SessionEvento.DtLimitePagamentoBoleto.ToString());

            DateTime dtVencBoleto = Geral.datahoraServidor(SessionCnn).AddDays(SessionEvento.NuDiasPrimeiroVencimento);

            if (dtVencBoleto > SessionEvento.DtLimitePagamentoBoleto)
                dtVencBoleto = DateTime.Parse(SessionEvento.DtLimitePagamentoBoleto.ToString());

            if ((SessionEvento.CdCliente == "0013") && (TxtFormaPagamento.SelectedItem.Text.ToUpper().Contains("EMPENHO")))//CONASEMS
                dtVencBoleto = DateTime.Parse(SessionEvento.DtLimitePagamentoBoleto.ToString());


            //TiposPagamento tpPagamento = oTiposPagamentoCad.Pesquisar(SessionEvento.CdEvento, TxtFormaPagamento.SelectedValue.ToString(), SessionCnn);

            string tmpDt = "";
            int dia = int.Parse(txtDiaVcto.Text);
            int mes = dtVenc.AddMonths(1).Month;
            int ano = DateTime.Today.Year;


            //Apagar boletos existentes
            if ((nrParcelasMudado) || (!SessionEvento.FlPermitirEditarPedido))
                oBoletoCad.ApagarBoletoPedido(SessionEvento.CdEvento, SessionPedido.CdPedido, SessionCnn);

            for (int i = 1; i <= int.Parse(txtQtdParcelas.Text); i++)
            {
                oBoleto = oBoletoCad.PequisarBoletoDoPedido(SessionEvento.CdEvento, SessionPedido.CdPedido, i.ToString().PadLeft(3, '0'), SessionCnn);

                if (oBoleto == null)
                    oBoleto = new MeuBoleto(SessionParticipante.CdEvento, "", SessionPedido.CdPedido, DateTime.Today, dtVencBoleto, vlBoleto, 0, false, null, "", i.ToString().PadLeft(3, '0'), TxtFormaPagamento.SelectedItem.Text, true, "", false, false, null, false, null,false,"",0);
                else
                {
                    oBoleto.DtVencimento = dtVencBoleto;
                    oBoleto.VlBoleto = vlBoleto;

                    oBoleto.FlBoletoRegistrado = false;
                    oBoleto.FlBoletoAlterado = true;
                    oBoleto.DtBoletoAlterado = Geral.datahoraServidor(SessionCnn);

                    //oBoleto.CdParcela = 
                }

                

                oBoleto = oBoletoCad.Gravar(oBoleto, SessionCnn);

                if ((tpPagamento.DsTecnologia.ToUpper() == "BOLETOFACIL") || (tpPagamento.DsTecnologia.ToUpper() == "GERENCIANET"))
                    oBoleto = oBoletoCad.MarcarComoRegsistradoBanco(oBoleto.CdEvento, oBoleto.CdBoleto, SessionCnn);

                if (tpPagamento.FlEscolheDia)
                {
                    if (dtVencBoleto.AddMonths(1).Year > ano)
                        ano = dtVenc.AddMonths(1).Year; //previnindo virada de ano

                    if ((dtVencBoleto.AddMonths(1).Month == 2) && (int.Parse(txtDiaVcto.Text) > 28))
                        dia = 28;//previnindo mes fevereiro

                    mes = dtVencBoleto.AddMonths(1).Month;

                    tmpDt = dia.ToString().PadLeft(2, '0') + "/" + mes.ToString().PadLeft(2, '0') + "/" + ano.ToString() + " 00:00";

                    dtVencBoleto = DateTime.Parse(tmpDt);
                }
                else
                    dtVencBoleto = dtVencBoleto.AddDays(30);

                if (dtVencBoleto > SessionEvento.DtLimitePagamentoBoleto)
                    dtVencBoleto = DateTime.Parse(SessionEvento.DtLimitePagamentoBoleto.ToString());

                //if (dtVenc > SessionEvento.DtFechamentoInscrWeb)
                //    dtVenc = DateTime.Parse(SessionEvento.DtFechamentoInscrWeb.ToString());

                //lblMsg.Text = oBoletoCad.RcMsg;// "Operação realizada com sucesso!";
            }

            //if ((SessionEvento.CdCliente == "0013") && (TxtFormaPagamento.SelectedItem.Text.ToUpper().Contains("EMPENHO")))//CONASEMS
            if ((SessionEvento.CdCliente == "0013") || (SessionEvento.CdCliente == "0008") || (SessionEvento.CdEvento == "007701"))
                dtVenc = DateTime.Parse(SessionEvento.DtLimitePagamentoBoleto.ToString());

            SessionPedido.DtVencimentoPedido = dtVenc;
            SessionPedido.TpPagamento = TxtFormaPagamento.SelectedItem.Text;
            SessionPedido = oPedidoCad.Gravar(SessionPedido, SessionCnn);
            Session["SessionPedido"] = SessionPedido;


            /***** COLOCAR ROTINA DE REGISTRAR BOLETO ON LINE BRADESCO AQUI AQUI *****/

            //if ((SessionEvento.CdCliente == "0013") && (TxtFormaPagamento.SelectedItem.Text.ToUpper().Contains("EMPENHO")))//CONASEMS
            //    oGeral.EnviarEmailPedidoNE(SessionEvento, SessionParticipante, SessionPedido, SessionCnn);
            //else
            //    oGeral.EnviarEmailPedidoBoleto(SessionEvento, SessionParticipante, SessionPedido, SessionCnn);

            if (int.Parse(txtQtdParcelas.Text) == 1)
            {
                //rptBoleto.aspx?e=" + oBoleto.CdEvento + "&b=" + oBoleto.CdBoleto + "&p=" + oBoleto.CdPedido

                if ((SessionEvento.CdEvento == "001002") || (SessionEvento.CdEvento == "001004")) //ABERT
                {
                    AcompanhanteCad oAcompanhanteCad = new AcompanhanteCad();
                    DataTable DTAcompanhantes;

                    DTAcompanhantes = oAcompanhanteCad.Listar(SessionEvento.CdEvento,
                        SessionParticipante.CdParticipante, SessionCnn);

                    if ((DTAcompanhantes == null) ||
                        (oPedidoCad.TotalDeAcompanhantesParaCadastro(SessionPedido.CdEvento, SessionPedido.CdPedido,
                             SessionCnn) < DTAcompanhantes.Rows.Count))
                    {
                        Server.Transfer(string.Format("frmAcompanhante.aspx?e={0}&b={1}",
                            cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento),
                            cllEventos.Crypto.EncryptStringAES(oBoleto.CdBoleto.Substring(6, 6))), true);
                    }
                }

                //if (SessionEvento.CdEvento == "007701")
                //{
                //    Response.Redirect("frm_mensagens.aspx?cdMensagem=036&dsMensagem=", false);
                //    return;
                //}

                //if (SessionEvento.CdCliente == "0041")
                //{
                //    Server.Transfer(string.Format("frmBoleto.aspx?e={0}&b={1}",
                //        cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento),
                //        cllEventos.Crypto.EncryptStringAES(oBoleto.CdBoleto.Substring(6, 6))), true);
                //}
                //else 
                if (tpPagamento.DsTecnologia.ToUpper() == "BOLETOFACIL")
                {
                    try
                    {
                        // Cria uma instância do SDK que irá enviar requisições ao ambiente de testes do Boleto Fácil (Sandbox)
                        BoletoFacil boletoFacil;
                        if (SessionEvento.boletoConfig.FlgProducao)
                        {
                            boletoFacil = new BoletoFacil(BoletoFacilEnvironment.Production,
                                SessionEvento.boletoConfig.NumClientToken);
                            //    "3BD9A27F5B001F531A6133FCB1815F99FE9FD125C322054F63B561387730A315"); // XYZ12345 is the API key
                        }
                        else
                        {
                            boletoFacil = new BoletoFacil(BoletoFacilEnvironment.Sandbox,
                                SessionEvento.boletoConfig.NumClient_TokenTestes);
                            //"EE8C9E869B3CA4D1AF8BBAF1933E79B4B8AECCA1C034072BA91C85B8983BE9BD");
                        }

                        Payer payer = new Payer();
                        payer.Name = SessionPedido.DsNomeRecibo != ""
                            ? SessionPedido.DsNomeRecibo
                            : SessionParticipante.NoParticipante;
                        payer.CpfCnpj = SessionPedido.NuCPFCNPJRecibo != ""
                            ? SessionPedido.NuCPFCNPJRecibo
                            : SessionParticipante.NuCPFCNPJ;
                        //payer.Email = SessionPedido.DsEmailResponsavelFinanceiro != ""
                        //    ? SessionPedido.DsEmailResponsavelFinanceiro
                        //    : SessionParticipante.DsEmail;


                        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

                        Charge charge = new Charge();
                        charge.Description = "Referente à inscrição de " + SessionParticipante.NoParticipante +
                                             " para " + SessionEvento.NoEvento;
                        charge.Reference = oBoleto.CdBoleto;
                        charge.DueDate = oBoleto.DtVencimento.Value;
                        charge.Amount = oBoleto.VlBoleto;
                        charge.Fine = 0;
                        charge.Interest = 0;
                        charge.NotificationUrl = SessionEvento.boletoConfig.DesUrlNotification;
                        charge.Payer = payer;

                        ChargeResponse response = boletoFacil.IssueCharge(charge);

                        oBoleto.CdBoletoExterno = Int64.Parse(response.Data.Charges[0].Code);
                        oBoleto.DsLinkBoletoExterno = response.Data.Charges[0].Link;

                        oBoleto = oBoletoCad.Gravar(oBoleto, SessionCnn);

                        oGeral.EnviarEmailPedidoBoleto(SessionEvento, SessionParticipante, SessionPedido, SessionCnn);


                        //Response.Write("<script>window.open('" + response.Data.Charges[0].Link + "','_blank');</script>");
                        Response.Redirect("frm_mensagens.aspx?cdMensagem=014&dsMensagem=BLT" + oBoleto.CdBoleto, false);
                        return;
                    }
                    catch (Exception ex)
                    {
                        return;
                        // return BadRequest();
                    }



                }
                else if (tpPagamento.DsTecnologia.ToUpper() == "GERENCIANET")
                {
                    try
                    {
                        var clientId = SessionEvento.boletoConfig.FlgProducao ? SessionEvento.boletoConfig.NumClientToken : SessionEvento.boletoConfig.NumClient_TokenTestes;
                        var clientSecret = SessionEvento.boletoConfig.FlgProducao ? SessionEvento.boletoConfig.CodClientSecret : SessionEvento.boletoConfig.CodClientSecretTestes;
                        var sandBox = SessionEvento.boletoConfig.FlgProducao ? false : true;


                        dynamic endpoints = new Endpoints(clientId, clientSecret, sandBox);

                        var body = new
                        {
                            items = new[] {
                                new {
                                name = "Inscrição para: " + SessionEvento.NoEvento ,
                                value = Int16.Parse(oBoleto.VlBoleto.ToString().Replace(",","").Replace(".","")), //valor 10,00 = 1000
                                amount = 1,//quantidade

                                }
                                },
                            //shippings = new[] {
                            //        new {
                            //        name = "Frete",
                            //        value = 100
                            //        }
                            //        },

                            metadata = new
                            {
                                custom_id = oBoleto.CdBoleto,
                                notification_url = SessionEvento.boletoConfig.DesUrlNotification
                            }

                        };

                        string response = JsonConvert.SerializeObject(endpoints.CreateCharge(null, body), Newtonsoft.Json.Formatting.Indented);

                        var transactation = JsonConvert.DeserializeObject<GerenciaNet>(response);
                        if (transactation.Data != null)
                        {
                           // var body_payment = new ;
                            //if ((SessionPedido.TpPessoa == "PF") || (SessionPedido.TpPessoa == ""))
                            
                                var body_paymentPF = new
                                {
                                    payment = new
                                    {
                                        banking_billet = new
                                        {
                                            expire_at = oBoleto.DtVencimento.Value.ToString("yyyy-MM-dd"),//DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"),
                                            customer = new
                                            {
                                                name = (SessionPedido.DsNomeRecibo != "")
                                                        ? SessionPedido.DsNomeRecibo
                                                        : SessionParticipante.NoParticipante,
                                                email = "oldbuck@gerencianet.com.br",
                                                cpf = SessionPedido.NuCPFCNPJRecibo != ""
                                                    ? SessionPedido.NuCPFCNPJRecibo
                                                    : SessionParticipante.NuCPFCNPJ,
                                                birth = "1977-01-15",
                                                phone_number = "5144916523"
                                            },
                                            message = @"Referente a inscrição de: " + SessionParticipante.NoParticipante + "\npara: " + SessionEvento.NoEvento
                                        }
                                    }

                                };
                            
                            
                               var body_paymentPJ = new
                                {
                                    payment = new
                                    {
                                        banking_billet = new
                                        {
                                            expire_at = oBoleto.DtVencimento.Value.ToString("yyyy-MM-dd"),//DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"),
                                            customer = new
                                            {
                                                name = (SessionPedido.DsNomeRecibo != "")
                                                        ? SessionPedido.DsNomeRecibo
                                                        : SessionParticipante.NoParticipante,
                                                email = "oldbuck@gerencianet.com.br",
                                                cpf = SessionPedido.NuCPFCNPJRecibo != ""
                                                    ? SessionPedido.NuCPFCNPJRecibo
                                                    : SessionParticipante.NuCPFCNPJ,
                                                birth = "1977-01-15",
                                                phone_number = "5144916523",

                                                juridical_person = new
                                                {
                                                    corporate_name = SessionPedido.DsNomeRecibo,
                                                    cnpj = SessionPedido.NuCPFCNPJRecibo
                                                }
                                            },
                                            message = @"Referente a inscrição de: " + SessionParticipante.NoParticipante + "\npara: " + SessionEvento.NoEvento
                                        }
                                    }

                                };
                            

                            var param = new
                            {
                                id = transactation.Data.Charge_Id
                            };

                            var resultPayCharge = (SessionPedido.TpPessoa == "PJ")
                                ? JsonConvert.SerializeObject(endpoints.PayCharge(param, body_paymentPJ), Newtonsoft.Json.Formatting.Indented) 
                                : JsonConvert.SerializeObject(endpoints.PayCharge(param, body_paymentPF), Newtonsoft.Json.Formatting.Indented);

                            var gerenciaNet = JsonConvert.DeserializeObject<GerenciaNet>(resultPayCharge);

                            oBoleto.CdBoletoExterno = gerenciaNet.Data.Charge_Id;
                            oBoleto.DsLinkBoletoExterno = gerenciaNet.Data.Link;

                            oBoleto = oBoletoCad.Gravar(oBoleto, SessionCnn);

                            oGeral.EnviarEmailPedidoBoleto(SessionEvento, SessionParticipante, SessionPedido, SessionCnn);


                            //Response.Write("<script>window.open('" + gerenciaNet.Data.Link + "','_blank');</script>");
                            //ResponseHelper.Redirect(gerenciaNet.Data.Link, "_blank", "");
                            //return;

                            Response.Redirect("frm_mensagens.aspx?cdMensagem=014&dsMensagem=BLT"+ oBoleto.CdBoleto, false);
                            return;
                        }

                    }
                    catch (GnException e)
                    {
                        lblMsgPgto.Visible = true;
                        lblMsgPgto.Text =  e.ErrorType + "\n"+ e.Message;
                        return;
                    }
                    
                }
                else
                {
                    oGeral.EnviarEmailPedidoBoleto(SessionEvento, SessionParticipante, SessionPedido, SessionCnn);

                    Response.Redirect("frm_mensagens.aspx?cdMensagem=041&dsMensagem=", false);
                    return;
                }

                //Response.Write("<script>window.open('frmBoleto.aspx?e=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + "&b=" + cllEventos.Crypto.EncryptStringAES(oBoleto.CdBoleto.Substring(6, 6)) + "','_self');</script>");
            }
            else
            {
                //Server.Transfer(string.Format("frmListaBoletosPedido.aspx?p={0}",
                //                cllEventos.Crypto.EncryptStringAES(SessionPedido.CdPedido.Substring(15, 3), "3")), true);

                if ((SessionEvento.CdEvento == "001002") || (SessionEvento.CdEvento == "001004"))//ABERT
                {
                    AcompanhanteCad oAcompanhanteCad = new AcompanhanteCad();
                    DataTable DTAcompanhantes;

                    DTAcompanhantes = oAcompanhanteCad.Listar(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);

                    if ((DTAcompanhantes == null) ||
                        (oPedidoCad.TotalDeAcompanhantesParaCadastro(SessionPedido.CdEvento, SessionPedido.CdPedido, SessionCnn) < DTAcompanhantes.Rows.Count))
                    {
                        Server.Transfer(string.Format("frmAcompanhante.aspx?p={0}",
                                cllEventos.Crypto.EncryptStringAES(SessionPedido.CdPedido.Substring(15, 3))), true);
                    }
                }

                if (tpPagamento.DsTecnologia.ToUpper() == "BOLETOFACIL")
                {
                    Server.Transfer(string.Format("frmListaBoletosPedido.aspx?p={0}",
                                    cllEventos.Crypto.EncryptStringAES(SessionPedido.CdPedido.Substring(15, 3))), true);
                }
                else
                {
                    Response.Redirect("frm_mensagens.aspx?cdMensagem=041&dsMensagem=", false);
                    return;
                }
            }
            
            #endregion
        }//fim boleto
        else
        {
            if (dtVenc > SessionEvento.DtFechamentoInscrWeb)
                dtVenc = DateTime.Parse(SessionEvento.DtFechamentoInscrWeb.ToString());
            else if (dtVenc > SessionPedido.DtVencimentoPedido)
                dtVenc = DateTime.Parse(SessionPedido.DtVencimentoPedido.ToString());

            if (TxtFormaPagamento.SelectedItem.Text.ToUpper().Contains("EMPENHO"))
            {
                dtVenc = DateTime.Parse(SessionEvento.DtLimitePagamentoBoleto.ToString());

                SessionPedido.DtVencimentoPedido = dtVenc;// SessionEvento.DtFechamentoInscrWeb;
                SessionPedido.TpPagamento = TxtFormaPagamento.SelectedItem.Text;
                SessionPedido = oPedidoCad.Gravar(SessionPedido, SessionCnn);
                Session["SessionPedido"] = SessionPedido;

                oGeral.EnviarEmailPedidoNE(SessionEvento, SessionParticipante, SessionPedido, SessionCnn);

            }
            else if (TxtFormaPagamento.SelectedItem.Text.ToUpper().Contains("TRANSFEREN"))
            {
                SessionPedido.DtVencimentoPedido = dtVenc;// SessionEvento.DtFechamentoInscrWeb;
                SessionPedido.TpPagamento = TxtFormaPagamento.SelectedItem.Text;
                SessionPedido = oPedidoCad.Gravar(SessionPedido, SessionCnn);
                Session["SessionPedido"] = SessionPedido;

                oGeral.EnviarEmailPedidoTransfBanc(SessionEvento, SessionParticipante, SessionPedido, SessionCnn);

            }
            else if ((TxtFormaPagamento.SelectedItem.Text.ToUpper().Contains("AUTORIZAÇÃO DE DEBITO")) || (TxtFormaPagamento.SelectedItem.Text.ToUpper().Contains("AUTORIZAÇÃO DE DÉBITO")))
            {
                SessionPedido.DtVencimentoPedido = dtVenc;// SessionEvento.DtFechamentoInscrWeb;
                SessionPedido.TpPagamento = TxtFormaPagamento.SelectedItem.Text;
                SessionPedido = oPedidoCad.Gravar(SessionPedido, SessionCnn);
                Session["SessionPedido"] = SessionPedido;

                //oGeral.EnviarEmailPedidoTransfBanc(SessionEvento, SessionParticipante, SessionPedido, SessionCnn);

            }
            else if ((TxtFormaPagamento.SelectedItem.Text.ToUpper().Contains("DÉBITO")))// || (TxtFormaPagamento.SelectedItem.Text.ToUpper().Contains("DEBITO")))
            {
                //SessionPedido.DtVencimentoPedido = dtVenc;// SessionEvento.DtFechamentoInscrWeb;
                SessionPedido.TpPagamento = TxtFormaPagamento.SelectedItem.Text;
                //SessionPedido = oPedidoCad.Gravar(SessionPedido, SessionCnn);
                //Session["SessionPedido"] = SessionPedido;

                //oGeral.EnviarEmailPedidoDebito(SessionEvento, SessionParticipante, SessionPedido, SessionCnn);

                if (tpPagamento.DsTecnologia.ToUpper() == "CIELO")
                {
                    #region CIELO

                    string sURL = Request.Url.ToString().ToLower();
                    string tmpXpto = "";
                    if (sURL.Contains("localhost"))
                        tmpXpto = "http://" + Request.Url.Authority;
                    else
                        tmpXpto = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
                    //"https://inscricoesweb.fazendomais.com";

                    /*_cieloService = new CieloService(); DESCONTINUADO*/

                    ICieloApi api = new CieloApi(CieloEnvironment.Production, new Merchant(Guid.Parse(SessionEvento.cieloConfig.NumConta), SessionEvento.cieloConfig.CodAcesso));

                    

                    CardBrand cartaoCredito = new CardBrand();
                    if (chkVisa.Checked)
                        cartaoCredito = CardBrand.Visa;
                    else if (chkMaster.Checked)
                        cartaoCredito = CardBrand.Master;
                    else if (chkAmex.Checked)
                        cartaoCredito = CardBrand.Amex;
                    else if (chkDiners.Checked)
                        cartaoCredito = CardBrand.Diners;
                    else if (chkJCB.Checked)
                        cartaoCredito = CardBrand.JCB;
                    else if (chkAura.Checked)
                        cartaoCredito = CardBrand.Aura;
                    else if (chkElo.Checked)
                        cartaoCredito = CardBrand.Elo;
                    else if (chkDiscover.Checked)
                        cartaoCredito = CardBrand.Discover;

                    DateTime validExpirationDate = new DateTime(Convert.ToInt16(txtAnoValidadeCartao.Text), Convert.ToByte(txtMesValidadeCartao.Text), 1);
                    var customer = new Customer(name: SessionParticipante.NoParticipante);

                    
                    decimal vlrPgto = (SessionPedido.VlTotalPedido - tmpVlDescontoCupom) * SessionPedido.VlConversao;
                    
                    var debitCard = new DebitCard(                        
                        cardNumber: txtNrCartao.Text,
                        holder: txtNomeTitularCartao.Text,
                        expirationDate: validExpirationDate,
                        securityCode: txtCodSegCartao.Text,
                        brand: cartaoCredito);

                    var payment = new Cielo.Payment(
                        type: PaymentType.DebitCard,
                        returnUrl: "" ,
                        amount: vlrPgto,
                        currency: Currency.BRL,
                        installments: int.Parse(txtQtdParcelas.SelectedValue.ToString()),
                        capture: true,
                        softDescriptor: SessionEvento.DsSMS_Remetente,
                        //Authenticate: true,
                        debitCard: debitCard);

                    var transaction = new Transaction(
                        merchantOrderId: SessionPedido.CdPedido,
                        customer: customer,
                        payment: payment);

                    


                    try
                    {
                        

                        Session["response"] = null;

                        var response = api.CreateTransaction(Guid.NewGuid(), transaction);

                        if ((response.Payment.Tid == null) || (response.Payment.Tid == ""))
                        {
                            lblMsgPgto.Visible = true;
                            lblMsgPgto.Text = "Não foi possível efetuar a transação!<br />Favor ligar para o Helpdesk e informar o Codigo de erro 001-325.";
                            if (SessionIdioma == "ENUS")
                                lblMsgPgto.Text = "Could not complete the transaction <br /> Please call the Helpdesk and report the error Code 001-325.";
                            else if (SessionIdioma == "ESP")
                                lblMsgPgto.Text = "No se pudo completar la transacción <br /> Por favor, llame al servicio de asistencia e informar del error de código 001-325.";
                            return;
                        }
                        Session["tid"] = response.Payment.Tid;

                        Session["response"] = response;

                        Server.Transfer(string.Format("frmCieloRetorno.aspx?cdMatricula={0}",
                                                            SessionParticipante.CdParticipante), true);

                        return;
                        //return Redirect("CheckStatus");
                    }
                    catch (Exception ex)
                    {
                        lblMsgPgto.Visible = true;
                        lblMsgPgto.Text = "<br />Não foi possível efetuar a transação!<br />";// + CieloService.MensagemErro(ex.Message)+"<br />";
                        if (SessionIdioma == "ENUS")
                            lblMsgPgto.Text = "Could not complete the transaction <br /> ";// + CieloService.MensagemErro(ex.Message) + "<br />";
                        else if (SessionIdioma == "ESP")
                            lblMsgPgto.Text = "No se pudo completar la transacción <br /> ";// + CieloService.MensagemErro(ex.Message) + "<br />";
                        return;
                    }

                    #endregion
                }

            }
            else if ((TxtFormaPagamento.SelectedItem.Text.ToUpper().Contains("DEPÓSITO")) || (TxtFormaPagamento.SelectedItem.Text.ToUpper().Contains("DEPOSITO")))
            {
                SessionPedido.DtVencimentoPedido = dtVenc;// SessionEvento.DtFechamentoInscrWeb;
                SessionPedido.TpPagamento = TxtFormaPagamento.SelectedItem.Text;
                SessionPedido = oPedidoCad.Gravar(SessionPedido, SessionCnn);
                Session["SessionPedido"] = SessionPedido;

                oGeral.EnviarEmailPedidoDeposito(SessionEvento, SessionParticipante, SessionPedido, SessionCnn);

            }
            else if (TxtFormaPagamento.SelectedItem.Text.ToUpper().Contains("BANK TRANSFER"))
            {
                SessionPedido.DtVencimentoPedido = dtVenc;// SessionEvento.DtFechamentoInscrWeb;
                SessionPedido.TpPagamento = TxtFormaPagamento.SelectedItem.Text;
                SessionPedido = oPedidoCad.Gravar(SessionPedido, SessionCnn);
                Session["SessionPedido"] = SessionPedido;

                if (SessionEvento.CdEvento == "007002")
                {
                    if (!SessionParticipante.Categoria.NoCategoria.Contains("BRAZILIAN"))
                    {
                        GerarInVoice(SessionEvento.CdEvento, SessionPedido.CdPedido, SessionCnn);
                    }
                }
                else
                    GerarInVoice(SessionEvento.CdEvento, SessionPedido.CdPedido, SessionCnn);


                oGeral.EnviarEmailPedidoInvoice(SessionEvento, SessionParticipante, SessionPedido, SessionCnn);

            }
            else if ((!TxtFormaPagamento.SelectedItem.Text.ToUpper().Contains("PAYPAL")) &&
                    ((TxtFormaPagamento.SelectedItem.Text.ToUpper().Contains("CREDITO")) || 
                     (TxtFormaPagamento.SelectedItem.Text.ToUpper().Contains("CRÉDITO")) || 
                     (TxtFormaPagamento.SelectedItem.Text.ToUpper().Contains("CARTÃO")) || 
                     (TxtFormaPagamento.SelectedItem.Text.ToUpper().Contains("CARTAO")) || 
                     (TxtFormaPagamento.SelectedItem.Text.ToUpper().Contains("CREDIT CARD")) || 
                     (TxtFormaPagamento.SelectedItem.Text.ToUpper().Contains("TARJETA")))
                )
            {

                #region CARTÃO DE CRÉDITO

                #region Validações

                if ((!chkVisa.Checked) && (!chkMaster.Checked) && (!chkAmex.Checked) && (!chkDiners.Checked) &&
                    (!chkHipercard.Checked) && (!chkJCB.Checked) && (!chkAura.Checked) && (!chkSoro.Checked) &&
                    (!chkElo.Checked) && (!chkDiscover.Checked) && (!chkCabal.Checked))
                {
                    lblMsgPgto.Visible = true;
                    if (SessionIdioma == "PTBR")
                        lblMsgPgto.Text = "Informe o cartão utilizado para pagamento!";
                    else if (SessionIdioma == "ENUS")
                        lblMsgPgto.Text = "Enter the card used for payment";
                    else if (SessionIdioma == "ESP")
                        lblMsgPgto.Text = "Introduzca la tarjeta utilizada para el pago";
                    else if (SessionIdioma == "FRA")
                        lblMsgPgto.Text = "Saisissez la carte utilisée pour le paiement";
                    return;
                }

                if (txtNomeTitularCartao.Text == "")
                {
                    lblMsgPgto.Visible = true;
                    lblMsgPgto.Text = "Informe o Nome do Titular do Cartão!";

                    if (SessionIdioma == "PTBR")
                        lblMsgPgto.Text = "Informe o Nome do Titular do Cartão!";
                    else if (SessionIdioma == "ENUS")
                        lblMsgPgto.Text = "Enter the Cardholder Name";
                    else if (SessionIdioma == "ESP")
                        lblMsgPgto.Text = "Introduzca el nombre del titular";
                    else if (SessionIdioma == "FRA")
                        lblMsgPgto.Text = "Dites Nom du détenteur";

                    return;
                }

                if (((chkVisa.Checked) && (txtNrCartao.Text.Replace(".", "").Replace("-", "").Length != 13) && (txtNrCartao.Text.Replace(".", "").Replace("-", "").Length != 16) && (txtNrCartao.Text.Replace(".", "").Replace("-", "").Length != 19)) ||
                    ((chkAmex.Checked) && (txtNrCartao.Text.Replace(".", "").Replace("-", "").Length != 15)) ||
                    ((chkDiners.Checked) && (txtNrCartao.Text.Replace(".", "").Replace("-", "").Length != 14) && (txtNrCartao.Text.Replace(".", "").Replace("-", "").Length != 16)) ||
                    ((chkHipercard.Checked) && (txtNrCartao.Text.Replace(".", "").Replace("-", "").Length != 13) && (txtNrCartao.Text.Replace(".", "").Replace("-", "").Length != 16) && (txtNrCartao.Text.Replace(".", "").Replace("-", "").Length != 19)) ||
                    ((!chkVisa.Checked) && (!chkAmex.Checked) && (!chkDiners.Checked) && (!chkHipercard.Checked) && (txtNrCartao.Text.Replace(".", "").Replace("-", "").Length != 16)))
                {
                    lblMsgPgto.Visible = true;
                    lblMsgPgto.Text = "Número do cartão inválido";

                    if (SessionIdioma == "PTBR")
                        lblMsgPgto.Text = "Número de cartão inválido!";
                    else if (SessionIdioma == "ENUS")
                        lblMsgPgto.Text = "Number of invalid card";
                    else if (SessionIdioma == "ESP")
                        lblMsgPgto.Text = "Número de tarjeta no válida";
                    else if (SessionIdioma == "FRA")
                        lblMsgPgto.Text = "Nombre de carte invalide";
                    return;
                }

                if (txtMesValidadeCartao.SelectedItem.Text.Trim() == "")
                {
                    lblMsgPgto.Visible = true;
                    lblMsgPgto.Text = "Mês de validade do cartão inválido";

                    if (SessionIdioma == "PTBR")
                        lblMsgPgto.Text = "Mês de validade do cartão inválido!";
                    else if (SessionIdioma == "ENUS")
                        lblMsgPgto.Text = "Invalid card expiration month";
                    else if (SessionIdioma == "ESP")
                        lblMsgPgto.Text = "Inválido meses caducidad de la tarjeta";
                    else if (SessionIdioma == "FRA")
                        lblMsgPgto.Text = "Invalid mois d'expiration de carte";
                    return;
                }

                if (txtAnoValidadeCartao.SelectedItem.Text.Trim() == "")
                {
                    lblMsgPgto.Visible = true;
                    lblMsgPgto.Text = "Ano de validade do cartão inválido";

                    if (SessionIdioma == "PTBR")
                        lblMsgPgto.Text = "Ano de validade do cartão inválido!";
                    else if (SessionIdioma == "ENUS")
                        lblMsgPgto.Text = "Invalid card expiration year";
                    else if (SessionIdioma == "ESP")
                        lblMsgPgto.Text = "Inválido año caducidad de la tarjeta";
                    else if (SessionIdioma == "FRA")
                        lblMsgPgto.Text = "Invalid année d'expiration de carte";
                    return;
                }

                //if ((txtCodSegCartao.Text.Trim() == "") || (txtCodSegCartao.Text.Length < 3))
                //{
                //    lblMsg.Text = "Código de segurança inválido";
                //    return;
                //}
                
                #endregion


                if (tpPagamento.DsTecnologia.ToUpper() == "COBREBEM")
                {
                    #region COBREBEM

                    string bandeira = "";
                    if (chkVisa.Checked)
                        bandeira = "VISA";
                    else if (chkMaster.Checked)
                        bandeira = "MASTERCARD";
                    else if (chkAmex.Checked)
                        bandeira = "AMEX";
                    else if (chkDiners.Checked)
                        bandeira = "DINERS";
                    else if (chkHipercard.Checked)
                        bandeira = "HIPERCARD";
                    else if (chkJCB.Checked)
                        bandeira = "JCB";
                    else if (chkAura.Checked)
                        bandeira = "AURA";
                    else if (chkSoro.Checked)
                        bandeira = "SORO CRED";
                    else if (chkSoro.Checked)
                        bandeira = "ELO";
                    else if (chkSoro.Checked)
                        bandeira = "DISCOVER";
                    else if (chkCabal.Checked)
                        bandeira = "CABAL";

                    frmCCred frmccred = new frmCCred();
                    string resultado = frmccred.CCred(
                        SessionEvento.CodCobreBem,
                        SessionPedido.CdPedido,
                        SessionPedido.VlTotalPedido - tmpVlDescontoCupom,
                        txtQtdParcelas.Text,
                        txtNrCartao.Text,
                        txtMesValidadeCartao.Text,
                        txtAnoValidadeCartao.Text,
                        txtCodSegCartao.Text,
                        txtNomeTitularCartao.Text.ToUpper());


                    string[] retornos = resultado.Split(';');
                    if (retornos[0].ToUpper().Contains("CONFIRMADO"))
                    {
                        // DateTime dtVenc = DateTime.Today.AddDays(SessionEvento.NuDiasPrimeiroVencimento);

                        SessionPedido.DtVencimentoPedido = dtVenc;


                        SessionPedido.NoBandeira = bandeira;
                        SessionPedido.TpPagamento = TxtFormaPagamento.SelectedItem.Text;
                        SessionPedido = oPedidoCad.Gravar(SessionPedido, SessionCnn);
                        Session["SessionPedido"] = SessionPedido;

                        oPedidoCad.MarcarPedidoComoPago(SessionPedido, SessionCnn);

                        tmpVlDescontoCupom = 0;
                        String tmpDsCupomDesconto = "";
                        if (SessionPedido.pedidoCupomDesconto != null)
                        {
                            tmpVlDescontoCupom = SessionPedido.pedidoCupomDesconto.VlDescontoCalculado;
                            tmpDsCupomDesconto = SessionPedido.pedidoCupomDesconto.DsCupomDesconto;
                        }

                        Recibo oRecibo = oReciboCad.Gravar(
                                   SessionEvento.CdEvento,
                                   SessionParticipante.CdParticipante,
                                   SessionPedido.CdPedido,
                                   "",
                                   SessionPedido.VlTotalPedido,
                                   tmpVlDescontoCupom,
                                   0,
                                   SessionPedido.TpPagamento,
                                   retornos[1],
                                   SessionPedido.NoBandeira + "\n" +
                                   "Autorização - " + retornos[1] + "\n" +
                                   "Transação - " + retornos[2] + "\n" +
                                   "Nr Parcelas - " + txtQtdParcelas.Text +
                                   tmpDsCupomDesconto != "" ? "\nCupom Desconto - " + tmpDsCupomDesconto : "",
                                   "000000001",
                                   "",
                                   SessionPedido.VlTotalPedido - tmpVlDescontoCupom,
                                   "",
                                   0,
                                   "",
                                   SessionCnn);


                        oGeral.EnviarEmailPedidoRecebCartao(SessionEvento, SessionParticipante, SessionPedido, SessionCnn);
                        oGeral.EnviarEmailConfirmaCadastro(SessionEvento, SessionParticipante, SessionCnn);



                    }
                    else
                    {
                        SessionPedido.TpPagamento = "";
                        SessionPedido.NoBandeira = "";
                        SessionPedido = oPedidoCad.Gravar(SessionPedido, SessionCnn);
                        Session["SessionPedido"] = SessionPedido;

                        lblMsgPgto.Visible = true;
                        lblMsgPgto.Text = retornos[0] + "<br/>" + (retornos.Length > 1 ? retornos[1] : "");

                        return;
                    }
                    #endregion
                }
                else if (tpPagamento.DsTecnologia.ToUpper() == "CIELO")
                {
                    #region CIELO

                    string sURL = Request.Url.ToString().ToLower();
                    string tmpXpto = "";
                    if (sURL.Contains("localhost"))
                        tmpXpto = "http://" + Request.Url.Authority;
                    else
                        tmpXpto = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
                    //"https://inscricoesweb.fazendomais.com";

                    /*_cieloService = new CieloService(); DESCONTINUADO*/

                    ICieloApi api = new CieloApi(CieloEnvironment.Production, new Merchant(Guid.Parse(SessionEvento.cieloConfig.NumConta), SessionEvento.cieloConfig.CodAcesso));

                    /********    DESCONTINUADO    **********
                    _configuration = new CustomCieloConfiguration
                    {
                        
                        CurrencyId = "986", //SessionIdioma == "PTBR" ? "986" : "840",
                        CustomerId = SessionEvento.cieloConfig.NumConta,//"1006993069",
                        CustomerKey = SessionEvento.cieloConfig.CodAcesso,//"25fbb99741c739dd84d7b06ec78c9bac718838630f30b112d033ce2e621b34f3",
                        Language = Cielo.Enums.Language.Portuguese, //SessionIdioma == "PTBR" ? Cielo.Enums.Language.Portuguese : Cielo.Enums.Language.English,
                        ReturnUrl = tmpXpto + "/frmCieloRetorno.aspx?pd=" + SessionPedido.CdPedido + "&e=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + "&p=" + cllEventos.Crypto.EncryptStringAES(SessionParticipante.CdParticipante)
                    };
                    *********    DESCONTINUADO          ****** */

                    /******    DESCONTINUADO    **********    
                     Cielo.Enums.CreditCard cartaoCredito = new Cielo.Enums.CreditCard();
                     if (chkVisa.Checked)
                         cartaoCredito = Cielo.Enums.CreditCard.Visa;
                     else if (chkMaster.Checked)
                         cartaoCredito = Cielo.Enums.CreditCard.MasterCard;
                     else if (chkAmex.Checked)
                         cartaoCredito = Cielo.Enums.CreditCard.Amex;
                     else if (chkDiners.Checked)
                         cartaoCredito = Cielo.Enums.CreditCard.Diners;
                     else if (chkJCB.Checked)
                         cartaoCredito = Cielo.Enums.CreditCard.Jcb;
                     else if (chkAura.Checked)
                         cartaoCredito = Cielo.Enums.CreditCard.Aura;
                     else if (chkElo.Checked)
                         cartaoCredito = Cielo.Enums.CreditCard.Elo;
                     else if (chkDiscover.Checked)
                         cartaoCredito = Cielo.Enums.CreditCard.Discover;



                     var yearExpiration = Convert.ToInt16(txtAnoValidadeCartao.Text);
                     var monthExpiration = Convert.ToByte(txtMesValidadeCartao.Text);
                     ***********    DESCONTINUADO    ***************************************/

                    CardBrand cartaoCredito = new CardBrand();
                    if (chkVisa.Checked)
                        cartaoCredito = CardBrand.Visa;
                    else if (chkMaster.Checked)
                        cartaoCredito = CardBrand.Master;
                    else if (chkAmex.Checked)
                        cartaoCredito = CardBrand.Amex;
                    else if (chkDiners.Checked)
                        cartaoCredito = CardBrand.Diners;
                    else if (chkJCB.Checked)
                        cartaoCredito = CardBrand.JCB;
                    else if (chkAura.Checked)
                        cartaoCredito = CardBrand.Aura;
                    else if (chkElo.Checked)
                        cartaoCredito = CardBrand.Elo;
                    else if (chkDiscover.Checked)
                        cartaoCredito = CardBrand.Discover;

                    DateTime validExpirationDate = new DateTime(Convert.ToInt16(txtAnoValidadeCartao.Text), Convert.ToByte(txtMesValidadeCartao.Text), 1);
                    var customer = new Customer(name: SessionParticipante.NoParticipante);

                    //decimal vlrPgto = SessionPedido.VlTotalPedido - tmpVlDescontoCupom;
                    //if ((SessionIdioma != "PTBR") || (SessionParticipante.NoPais.ToUpper() != "BRASIL"))
                    //{
                    //if (SessionEvento.CdEvento == "007701")
                    //    vlrPgto = (SessionPedido.VlTotalPedido - tmpVlDescontoCupom) * decimal.Parse("3,4");
                    //else if (SessionEvento.CdEvento == "008501")
                    //    vlrPgto = (SessionPedido.VlTotalPedido - tmpVlDescontoCupom) * decimal.Parse("3,5");
                    //else if (SessionEvento.CdEvento == "010801")
                    //    vlrPgto = (SessionPedido.VlTotalPedido - tmpVlDescontoCupom) * decimal.Parse("3,6");
                    //}

                    Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");

                    decimal vlrPgto = (SessionPedido.VlTotalPedido - tmpVlDescontoCupom) * SessionPedido.VlConversao;
                    vlrPgto = decimal.Parse(vlrPgto.ToString("N2"));
                    var creditCard = new CreditCard(
                        cardNumber: txtNrCartao.Text,
                        holder: txtNomeTitularCartao.Text,
                        expirationDate: validExpirationDate,
                        securityCode: txtCodSegCartao.Text,
                        brand: cartaoCredito);

                    var payment = new Cielo.Payment(
                        amount: vlrPgto,
                        currency: Currency.BRL,
                        installments: int.Parse(txtQtdParcelas.SelectedValue.ToString()),
                        capture: true,
                        softDescriptor: SessionEvento.DsSMS_Remetente,
                        creditCard: creditCard);
                    
                    var transaction = new Transaction(
                        merchantOrderId: SessionPedido.CdPedido,
                        customer: customer,
                        payment: payment);
                    //return;
                    //var returnTransaction = api.CreateTransaction(Guid.NewGuid(), transaction);

                    /***********    DESCONTINUADO    ***************************************
                    var order = new Cielo.Requests.Entities.Order(SessionPedido.CdPedido, vlrPgto, DateTime.Now, SessionEvento.NoEvento);
                    var paymentMethod = new Cielo.Requests.Entities.PaymentMethod(cartaoCredito, (int.Parse(txtQtdParcelas.SelectedValue.ToString()) == 1 ? Cielo.Enums.PurchaseType.Credit : Cielo.Enums.PurchaseType.StoreInstallmentPayment), int.Parse(txtQtdParcelas.SelectedValue.ToString()));
                    var options = new Cielo.Requests.Entities.CreateTransactionOptions(Cielo.Enums.AuthorizationType.AuthorizeSkippingAuthentication, capture: true);
                    var creditCardData = new Cielo.Requests.Entities.CreditCardData(txtNrCartao.Text, new Cielo.Requests.Entities.CreditCardExpiration(yearExpiration, monthExpiration), Cielo.Enums.SecurityCodeIndicator.Sent, txtCodSegCartao.Text);
                    
                    var createTransactionRequest = new Cielo.Requests.CreateTransactionRequest(order, paymentMethod, options, creditCardData, _configuration);
                     ***********    DESCONTINUADO    ***************************************/


                    try
                    {
                        /***********    DESCONTINUADO    ***************************************
                        var response = _cieloService.CreateTransaction(createTransactionRequest);
                        ***********    DESCONTINUADO    ***************************************/

                        Session["response"] = null;

                        var response = api.CreateTransaction(Guid.NewGuid(), transaction);

                        if ((response.Payment.Tid == null) || (response.Payment.Tid == ""))
                        {
                            lblMsgPgto.Visible = true;
                            lblMsgPgto.Text = "Não foi possível efetuar a transação!<br />Favor ligar para o Helpdesk e informar o Codigo de erro 001-325.";
                            if (SessionIdioma == "ENUS")
                                lblMsgPgto.Text = "Could not complete the transaction <br /> Please call the Helpdesk and report the error Code 001-325.";
                            else if (SessionIdioma == "ESP")
                                lblMsgPgto.Text = "No se pudo completar la transacción <br /> Por favor, llame al servicio de asistencia e informar del error de código 001-325.";
                            return;
                        }
                        Session["tid"] = response.Payment.Tid;
                        
                        Session["response"] = response;

                        Server.Transfer(string.Format("frmCieloRetorno.aspx?cdMatricula={0}",
                                                            SessionParticipante.CdParticipante), true);

                        return;
                        //return Redirect("CheckStatus");
                    }
                    catch (Exception ex)
                    {
                        lblMsgPgto.Visible = true;
                        lblMsgPgto.Text = "<br />Não foi possível efetuar a transação!<br />";// + CieloService.MensagemErro(ex.Message)+"<br />";
                        if (SessionIdioma == "ENUS")
                            lblMsgPgto.Text = "Could not complete the transaction <br /> ";// + CieloService.MensagemErro(ex.Message) + "<br />";
                        else if (SessionIdioma == "ESP")
                            lblMsgPgto.Text = "No se pudo completar la transacción <br /> ";// + CieloService.MensagemErro(ex.Message) + "<br />";
                        return;
                    }

                    #endregion
                }
                else if (tpPagamento.DsTecnologia.ToUpper() == "REDE")
                {
                    #region REDE
                    try
                    {
                        string sURL = Request.Url.ToString().ToLower();
                        string tmpXpto = "";
                        if (sURL.Contains("localhost"))
                            tmpXpto = "http://" + Request.Url.Authority;
                        else
                            tmpXpto = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
                        //"https://inscricoesweb.fazendomais.com";

                        Session["cartaoCreditoBandeira"] = "";
                        if (chkVisa.Checked)
                            Session["cartaoCreditoBandeira"] = "Visa";
                        else if (chkMaster.Checked)
                            Session["cartaoCreditoBandeira"] = "Master";
                        else if (chkAmex.Checked)
                            Session["cartaoCreditoBandeira"] = "Amex";
                        else if (chkDiners.Checked)
                            Session["cartaoCreditoBandeira"] = "Diners";
                        else if (chkJCB.Checked)
                            Session["cartaoCreditoBandeira"] = "JCB";
                        else if (chkAura.Checked)
                            Session["cartaoCreditoBandeira"] = "Aura";
                        else if (chkElo.Checked)
                            Session["cartaoCreditoBandeira"] = "Elo";
                        else if (chkDiscover.Checked)
                            Session["cartaoCreditoBandeira"] = "Discover";

                        
                        Acquirer acquirer = new Acquirer(SessionEvento.redeConfig.NumConta, SessionEvento.redeConfig.CodAcesso, EnvironmentType.Production);

                        decimal vlrPgto = (SessionPedido.VlTotalPedido - tmpVlDescontoCupom) * SessionPedido.VlConversao;
                        string vlr_pgto = vlrPgto.ToString("N2").Replace(".", "").Replace(",", "");
                            
                        TransactionRequest request = new TransactionRequest();
                        request.Amount = int.Parse(vlr_pgto);

                        request.Kind = TransactionKind.Credit;
                        request.CardHolderName = (txtNomeTitularCartao.Text.Trim().Length > 30 ? txtNomeTitularCartao.Text.Trim().Substring(0,30) : txtNomeTitularCartao.Text.Trim());
                        request.CardNumber = txtNrCartao.Text;
                        request.SecurityCode = txtCodSegCartao.Text;
                        request.ExpirationMonth = Convert.ToByte(txtMesValidadeCartao.Text);
                        request.ExpirationYear = Convert.ToInt16(txtAnoValidadeCartao.Text);

                        request.Installments = int.Parse(txtQtdParcelas.SelectedValue.ToString())  > 1 ? int.Parse(txtQtdParcelas.SelectedValue.ToString()) : 0;
                        //request.SoftDescriptor = (SessionEvento.DsSMS_Remetente.Length > 13 ? SessionEvento.DsSMS_Remetente.Substring(0, 13) : SessionEvento.DsSMS_Remetente);
                        request.Reference = SessionPedido.CdPedido.Substring(6,12);

                        request.Capture = true;


                        Session["response"] = null;
                        TransactionResponse response = acquirer.Authorize(request);

                        
                        //if ((response.Tid == null) || (response.Tid == ""))
                        //{
                        //    lblMsgPgto.Visible = true;
                        //    lblMsgPgto.Text = "Não foi possível efetuar a transação!<br />Favor ligar para o Helpdesk e informar o Codigo de erro 001-325.";
                        //    if (SessionIdioma == "ENUS")
                        //        lblMsgPgto.Text = "Could not complete the transaction <br /> Please call the Helpdesk and report the error Code 001-325.";
                        //    else if (SessionIdioma == "ESP")
                        //        lblMsgPgto.Text = "No se pudo completar la transacción <br /> Por favor, llame al servicio de asistencia e informar del error de código 001-325.";
                        //    return;
                        //}
                        Session["tid"] = response.Tid;

                        Session["response"] = response;

                        Server.Transfer(string.Format("frmRedeRetorno.aspx?cdMatricula={0}", SessionParticipante.CdParticipante), true);

                        return;
                        
                    }
                    catch (Exception ex)
                    {
                        lblMsgPgto.Visible = true;
                        lblMsgPgto.Text = "<br />Não foi possível efetuar a transação!<br />";// + CieloService.MensagemErro(ex.Message)+"<br />";
                        if (SessionIdioma == "ENUS")
                            lblMsgPgto.Text = "Could not complete the transaction <br /> ";// + CieloService.MensagemErro(ex.Message) + "<br />";
                        else if (SessionIdioma == "ESP")
                            lblMsgPgto.Text = "No se pudo completar la transacción <br /> ";// + CieloService.MensagemErro(ex.Message) + "<br />";
                        return;
                    }

                    #endregion
                }
                else if (tpPagamento.DsTecnologia.ToUpper() == "SIPAG")
                {
                    #region SIPAG

                    string sURL = Request.Url.ToString().ToLower();
                    string tmpXpto = "";


                    string appPath = HttpContext.Current.Request.ApplicationPath;
                    string physicalPath = HttpContext.Current.Request.MapPath(appPath);
                    string caminhoCertficado = "";


                    if (sURL.Contains("localhost"))
                    {
                        tmpXpto = "http://" + Request.Url.Authority;
                        caminhoCertficado = @physicalPath + "imagensgeral\\" + SessionEvento.CdEvento + "\\" + SessionEvento.sipagConfig.CustomerID + ".p12";
                    }
                    else
                    {
                        tmpXpto = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
                        caminhoCertficado = @physicalPath + "\\imagensgeral\\" + SessionEvento.CdEvento + "\\" + SessionEvento.sipagConfig.CustomerID + ".p12";
                    }
                    //"https://inscricoesweb.fazendomais.com";



                    //lblMsgPgto.Text = @physicalPath + "imagensgeral\\" + SessionEvento.CdEvento + "\\" + SessionEvento.sipagConfig.CustomerID + ".p12";
                    //lblMsgPgto.Visible = true;
                    //return;


                    _configurationSIPAG = new CustomSipagConfiguration
                    {
                        CurrencyId = SessionIdioma == "PTBR" ? "986" : "840",
                        CustomerId = SessionEvento.sipagConfig.CustomerID,//"WS2719020033._.1",
                        CustomerKey = SessionEvento.sipagConfig.CustomerKey,//"Ty19bUg31d",
                        CertificateClient = caminhoCertficado,  //@physicalPath + "imagensgeral\\" + SessionEvento.CdEvento + "\\" + SessionEvento.sipagConfig.CustomerID + ".p12",
                        CertificateClientKey = SessionEvento.sipagConfig.CertificateClientKey, //"Uai8wRidMc",
                        Language = SessionIdioma == "PTBR" ? FM.Ecommerce.Sipag.Card.Enums.Language.Portuguese : FM.Ecommerce.Sipag.Card.Enums.Language.English,
                        //ReturnUrl = ""
                        ReturnUrl = tmpXpto + "/frmSipagRetorno.aspx?pd=" + SessionPedido.CdPedido + "&e=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + "&p=" + cllEventos.Crypto.EncryptStringAES(SessionParticipante.CdParticipante)
                    };


                    _sipagService = new SipagService(_configurationSIPAG, environmentProduction: true);

                    FM.Ecommerce.Sipag.Card.Enums.CreditCard cartaoCredito = new FM.Ecommerce.Sipag.Card.Enums.CreditCard();
                    if (chkVisa.Checked)
                        cartaoCredito = FM.Ecommerce.Sipag.Card.Enums.CreditCard.Visa;
                    else if (chkMaster.Checked)
                        cartaoCredito = FM.Ecommerce.Sipag.Card.Enums.CreditCard.MasterCard;
                    else if (chkSoro.Checked)
                        cartaoCredito = FM.Ecommerce.Sipag.Card.Enums.CreditCard.Amex;
                    else if (chkDiners.Checked)
                        cartaoCredito = FM.Ecommerce.Sipag.Card.Enums.CreditCard.Diners;
                    else if (chkJCB.Checked)
                        cartaoCredito = FM.Ecommerce.Sipag.Card.Enums.CreditCard.Jcb;
                    else if (chkAura.Checked)
                        cartaoCredito = FM.Ecommerce.Sipag.Card.Enums.CreditCard.Aura;
                    else if (chkElo.Checked)
                        cartaoCredito = FM.Ecommerce.Sipag.Card.Enums.CreditCard.Elo;
                    else if (chkDiscover.Checked)
                        cartaoCredito = FM.Ecommerce.Sipag.Card.Enums.CreditCard.Discover;
                    else if (chkSoro.Checked)
                        cartaoCredito = FM.Ecommerce.Sipag.Card.Enums.CreditCard.Sorocred;
                    else if (chkCabal.Checked)
                        cartaoCredito = FM.Ecommerce.Sipag.Card.Enums.CreditCard.Cabal;



                    var yearExpiration = Convert.ToInt16(txtAnoValidadeCartao.Text.Substring(2, 2));
                    var monthExpiration = Convert.ToByte(txtMesValidadeCartao.Text);

                    var order = new FM.Ecommerce.Sipag.Card.Requests.Entities.Order(SessionPedido.CdPedido, SessionPedido.VlTotalPedido - tmpVlDescontoCupom, DateTime.Now, SessionEvento.NoEvento);

                    var paymentMethod = new FM.Ecommerce.Sipag.Card.Requests.Entities.PaymentMethod(cartaoCredito, FM.Ecommerce.Sipag.Card.Enums.PurchaseType.Credit, CreditCardTxType.Sale, int.Parse(txtQtdParcelas.SelectedValue.ToString()));
                    var options = new FM.Ecommerce.Sipag.Card.Requests.Entities.CreateTransactionOptions(FM.Ecommerce.Sipag.Card.Enums.AuthorizationType.AuthorizeSkippingAuthentication, capture: true);
                    var creditCardData = new FM.Ecommerce.Sipag.Card.Requests.Entities.CreditCardData(txtNrCartao.Text, new FM.Ecommerce.Sipag.Card.Requests.Entities.CreditCardExpiration(yearExpiration, monthExpiration), FM.Ecommerce.Sipag.Card.Enums.SecurityCodeIndicator.Sent, txtCodSegCartao.Text);
                    var createTransactionRequest = new FM.Ecommerce.Sipag.Card.Requests.CreateTransactionRequest(order, paymentMethod, options, creditCardData, _configurationSIPAG);
                    //return;



                    //FM.Ecommerce.Sipag.Card.Responses.CreateTransactionResponse _response = new FM.Ecommerce.Sipag.Card.Responses.CreateTransactionResponse(null);
                    try
                    {
                        _response = _sipagService.CreateTransaction(createTransactionRequest);



                        string code = Regex.Match(_response.ProcessorApprovalCode, @"\d+").Value;
                        string code2 = Regex.Match(_response.ErrorMessage, @"\d+").Value;

                        string msg = "";
                        if (_response.Content.Contains("<faultcode>SOAP-ENV:Client</faultcode>"))
                        {
                            msg = _sipagService.MensagemErro(code2);

                        }
                        else
                        {
                            msg = _sipagService.MensagemRetornoAutorizacao(code);
                        }

                        if ((_response.IpgTransactionId == null) || (_response.IpgTransactionId == "") || _response.TransactionResult != "APPROVED")
                        {
                            lblMsgPgto.Visible = true;
                            lblMsgPgto.Text = "Não foi possível efetuar a transação!<br />" + msg;
                            return;

                        }


                        _response.Parcelas = txtQtdParcelas.SelectedValue.ToString();


                        Session["tid"] = _response.IpgTransactionId;
                        Session["SipagResponse"] = _response;
                        Session["msgSipag"] = msg;




                        Server.Transfer(string.Format("frmSipagRetorno.aspx?cdMatricula={0}",
                                                            SessionParticipante.CdParticipante), true);

                        return;
                        //return Redirect("CheckStatus");
                    }
                    catch (FM.Ecommerce.Sipag.Card.Responses.Exceptions.ResponseException ex)
                    {
                        if (_response == null)
                        {
                            lblMsgPgto.Visible = true;
                            lblMsgPgto.Text = "Não foi possível efetuar a transação!<br />" + ex.Message;
                            return;

                        }

                        string code = Regex.Match(_response.ApprovalCode, @"\d+").Value;
                        string code2 = Regex.Match(_response.ErrorMessage, @"\d+").Value;

                        string msg = "";
                        if (_response.Content.Contains("<faultcode>SOAP-ENV:Client</faultcode>"))
                        {
                            msg = _sipagService.MensagemErro(code2);

                        }
                        else
                        {
                            msg = _sipagService.MensagemRetornoAutorizacao(code);
                        }

                        lblMsgPgto.Visible = true;
                        lblMsgPgto.Text = "<br />Não foi possível efetuar a transação!<br />" + msg + "<br />";
                        return;
                    }

                    #endregion
                }
                
                #endregion
            }
            
            else if (TxtFormaPagamento.SelectedItem.Text.ToUpper().Contains("PAYPAL")) 
            {
                Server.Transfer(string.Format("frmPayPalPagamento.aspx?cdMatricula={0}",
                                                            SessionParticipante.CdParticipante), true);
            }
            else if (TxtFormaPagamento.SelectedItem.Text.ToUpper().Contains("PAGSEGURO"))
            {
                Server.Transfer(string.Format("frmPagSeguroPagamento.aspx?cdMatricula={0}",
                                                            SessionParticipante.CdParticipante), true);
            }
            else if ((TxtFormaPagamento.SelectedItem.Text.ToUpper().Contains("CAIXA DO EVENTO")) || (TxtFormaPagamento.SelectedItem.Text.ToUpper().Contains("LOCAL DO EVENTO")))
            {
                SessionPedido.DtVencimentoPedido = dtVenc;// SessionEvento.DtFechamentoInscrWeb;
                SessionPedido.TpPagamento = TxtFormaPagamento.SelectedItem.Text;
                SessionPedido = oPedidoCad.Gravar(SessionPedido, SessionCnn);
                Session["SessionPedido"] = SessionPedido;

                oGeral.EnviarEmailPedidoPagamentoLocal(SessionEvento, SessionParticipante, SessionPedido, SessionCnn);

            }
        }

        
            Response.Redirect("frm_mensagens.aspx?cdMensagem=014&dsMensagem=", false);
            return;
        
        //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
        //                        "014",
        //                        ""), true);

       
       

    }

    private decimal CalcularVlISS(decimal p, string cidade)
    {
        decimal retorno = 0;

        if ((cidade.ToUpper() == "SÃO PAULO") || (cidade.ToUpper() == "SAO PAULO"))
            retorno = p * decimal.Parse((2.5 / 100.0).ToString());

        return retorno;
    }

    private decimal CalcularVlCOFINS(decimal p)
    {
        decimal retorno = p * decimal.Parse((3 / 100.0).ToString());

        return retorno;
    }

    private decimal CalcularVlPIS(decimal p)
    {
        decimal retorno = p * decimal.Parse((0.65 / 100.0).ToString());
        if (retorno < 10)
            retorno = 0;

        return retorno;
    }

   
    
    protected void TxtFormaPagamento_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsgPgto.Visible = false;
        lblMsgPgto.Text = "";

        //pnlResponsavelFinanceiro.Visible = false;

        chkVisa.Visible = false;
        visa.Visible = false;

        chkMaster.Visible = false;
        master.Visible = false;

        chkElo.Visible = false;
        elo.Visible = false;

        chkAmex.Visible = false;
        amex.Visible = false;

        chkDiners.Visible = false;
        diners.Visible = false;

        chkHipercard.Visible = false;
        hiper.Visible = false;

        chkJCB.Visible = false;
        jcb.Visible = false;

        chkAura.Visible = false;
        aura.Visible = false;

        chkSoro.Visible = false;
        soro.Visible = false;

        chkDiscover.Visible = false;
        discover.Visible = false;

        chkCabal.Visible = false;
        cabal.Visible = false;

        pnlCartao.Visible = false;

        //pnlDadosRecibo.Visible = false;

        TiposPagamentoCad oTiposPagamentoCad = new TiposPagamentoCad();
        TiposPagamento tpPagamento = oTiposPagamentoCad.Pesquisar(SessionEvento.CdEvento, TxtFormaPagamento.SelectedValue.ToString(), SessionCnn);


        QuantidadeParcelas();

        if (tpPagamento == null)
            return;


        if ((SessionEvento.CodCobreBem.Trim() != "") && 
            ((TxtFormaPagamento.SelectedItem.Text.ToUpper().Contains("CARTÃO")) || 
            (TxtFormaPagamento.SelectedItem.Text.ToUpper().Contains("CREDIT CARD")) || 
            (TxtFormaPagamento.SelectedItem.Text.ToUpper().Contains("TARJETA DE CRÉDITO")) ||
            (TxtFormaPagamento.SelectedItem.Text.ToUpper().Contains("DÉBITO"))))
        {
            pnlCartao.Visible = true;


            DataTable dtBandeiras = oTiposPagamentoCad.ListarCartoesBandeiras(SessionEvento.CdEvento, TxtFormaPagamento.SelectedValue.ToString(), SessionCnn);
            if ((dtBandeiras != null) && (dtBandeiras.Rows.Count > 0))
            {
                for (int i = 0; i < dtBandeiras.Rows.Count; i++)
                {
                    if (dtBandeiras.DefaultView[i]["cdBandeira"].ToString() == "001")
                    {
                        chkVisa.Visible = true;
                        visa.Visible = true;
                    }
                    else if (dtBandeiras.DefaultView[i]["cdBandeira"].ToString() == "002")
                    {
                        chkMaster.Visible = true;
                        master.Visible = true;
                    }
                    else if (dtBandeiras.DefaultView[i]["cdBandeira"].ToString() == "003")
                    {
                        chkAmex.Visible = true;
                        amex.Visible = true;
                    }
                    else if (dtBandeiras.DefaultView[i]["cdBandeira"].ToString() == "004")
                    {
                        chkDiners.Visible = true;
                        diners.Visible = true;
                    }
                    else if (dtBandeiras.DefaultView[i]["cdBandeira"].ToString() == "005")
                    {
                        chkHipercard.Visible = true;
                        hiper.Visible = true;
                    }
                    else if (dtBandeiras.DefaultView[i]["cdBandeira"].ToString() == "006")
                    {
                        chkJCB.Visible = true;
                        jcb.Visible = true;
                    }
                    else if (dtBandeiras.DefaultView[i]["cdBandeira"].ToString() == "007")
                    {
                        chkAura.Visible = true;
                        aura.Visible = true;
                    }
                    else if (dtBandeiras.DefaultView[i]["cdBandeira"].ToString() == "008")
                    {
                        chkSoro.Visible = true;
                        soro.Visible = true;
                    }
                    else if (dtBandeiras.DefaultView[i]["cdBandeira"].ToString() == "009")
                    {
                        chkElo.Visible = true;
                        elo.Visible = true;
                    }
                    else if (dtBandeiras.DefaultView[i]["cdBandeira"].ToString() == "010")
                    {
                        chkDiscover.Visible = true;
                        discover.Visible = true;
                    }
                    else if (dtBandeiras.DefaultView[i]["cdBandeira"].ToString() == "011")
                    {
                        chkCabal.Visible = true;
                        cabal.Visible = true;
                    }
                }
            }
            else
            {
                lblMsgPgto.Text = oTiposPagamentoCad.RcMsg;
            }
        }

        //if (TxtFormaPagamento.SelectedItem.Text.ToUpper().Contains("EMPENHO"))
        //{
        //    pnlResponsavelFinanceiro.Visible = true;

        //    //pnlDadosRecibo.Visible = true;

        //    //txtTipoPessoaRecibo.SelectedValue = "PJ";
        //    //txtTipoPessoaRecibo.Enabled = false;
        //    //txtTipoPessoa_SelectedIndexChanged(sender, e);

        //}
        //else if (((SessionEvento.CdEvento == "010801") || (SessionEvento.CdEvento == "000103")) && (TxtFormaPagamento.SelectedItem.Text.ToUpper().Contains("BANK TRANSFER")))
        //{
        //    //pnlResponsavelFinanceiro.Visible = true;

        //    pnlDadosRecibo.Visible = true;

        //    //txtTipoPessoaRecibo.SelectedValue = "PJ";
        //    //txtTipoPessoaRecibo.Enabled = false;
        //    //txtTipoPessoa_SelectedIndexChanged(sender, e);

        //}
        //else
        //{
        //    if (SessionEvento.CdEvento == "008501")
        //    {
        //        pnlDadosRecibo.Visible = false;
        //    }
        //    //txtTipoPessoaRecibo.SelectedValue = "PF";
        //    //txtTipoPessoaRecibo.Enabled = true;
        //    //txtTipoPessoa_SelectedIndexChanged(sender, e);

        //}


       // txtQtdParcelas.SelectedValue = "1";
        if (!tpPagamento.FlParcelamento)
        {
            linhaParcelas.Visible = false;
            txtQtdParcelas.Enabled = false;
        }
        else
        {
            //if (SessionParticipante.DsIdioma == "PTBR")
            //{
            linhaParcelas.Visible = true;
            txtQtdParcelas.Enabled = true;
            //}
        }

        lblAviso.Text = tpPagamento.DsObservacaoTipo; 
        if (tpPagamento.DsObservacaoTipo.Trim() == "")
            pnlAviso.Visible = false;
        else
            pnlAviso.Visible = true;



    }
    protected void txtQtdParcelas_SelectedIndexChanged(object sender, EventArgs e)
    {
        TiposPagamentoCad oTiposPagamentoCad = new TiposPagamentoCad();
        TiposPagamento tpPagamento = oTiposPagamentoCad.Pesquisar(SessionEvento.CdEvento, TxtFormaPagamento.SelectedValue.ToString(), SessionCnn);
        
        if ((int.Parse(txtQtdParcelas.Text) > 1) && (tpPagamento.FlEscolheDia))
        {
            linhaVencimento.Visible = true;
            lblDiaVcto.Visible = true;
            txtDiaVcto.Visible = true;
        }
        else
        {
            linhaVencimento.Visible = false;
            lblDiaVcto.Visible = false;
            txtDiaVcto.Visible = false;
            txtDiaVcto.SelectedIndex = 0;
        }

    }
    //protected void btnCEP_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lblMsgCEP.Visible = false;

    //        txtEnderecoRecibo.Text = "";
    //        txtBairroRecibo.Text = "";
    //        txtCidadeRecibo.Text = "";
    //        txtUFRecibo.SelectedValue = "";

    //        if (txtCEPRecibo.Text.Replace(".", "").Replace("-", "").Trim() == "")
    //            return;


    //        WebCEP webcep = new WebCEP(txtCEPRecibo.Text.Replace(".", "").Replace("-", ""));

    //        if (webcep != null)
    //        {
    //            txtEnderecoRecibo.Text = webcep.TipoLagradouro + " " + webcep.Lagradouro;

    //            txtBairroRecibo.Text = webcep.Bairro;

    //            txtUFRecibo.SelectedValue = webcep.UF;

    //            txtCidadeRecibo.Text = webcep.Cidade;
    //            return;
    //        }
    //        else
    //        {
    //            lblMsgCEP.Visible = true;
    //            lblMsgCEP.Text = webcep.ResultadoTXT;
    //            return;
    //        }
    //    }
    //    catch
    //    {
    //        lblMsgCEP.Visible = true;
    //    }
    //    /*
    //    try
    //    {
    //        //XMLHTTP http = new XMLHTTP();

    //        //http.open("POST", "http://webservice.uni5.net/web_cep.php?auth=8739185af299ee7b1463b579a90fee5a&formato=xml&cep=" + txtCEPRecibo.Text.Replace(".", ""), false, null, null);
    //        //http.send(null);


    //        //XmlDocument xmlDoc = new XmlDocument();


    //        //xmlDoc.LoadXml(http.responseText);

    //        //XmlNode No = xmlDoc.SelectSingleNode("webservicecep");

    //        //txtEnderecoRecibo.Text = No.ChildNodes.Item(5).InnerText.ToUpper() + " " +
    //        //                         No.ChildNodes.Item(6).InnerText.ToUpper(); //logradouro

    //        //txtBairroRecibo.Text = No.ChildNodes.Item(4).InnerText.ToUpper();//bairro

    //        //txtCidadeRecibo.Text = No.ChildNodes.Item(3).InnerText.ToUpper();//cidade

    //        //txtUFRecibo.SelectedValue = No.ChildNodes.Item(2).InnerText.ToUpper();//UF

    //        string filename = @"http://webservice.uni5.net/web_cep.php?auth=8739185af299ee7b1463b579a90fee5a&formato=xml&cep=" + txtCEPRecibo.Text.Replace(".", "").Replace("-", "");
    //        XmlTextReader reader = new XmlTextReader(filename);
    //        string strTempName, strTempValue;
    //        reader.MoveToContent();

    //        string tipoLogradouro, logradouro, bairro, cidade, uf;

    //        tipoLogradouro = logradouro = bairro = cidade = uf = "";

    //        do
    //        {
    //            strTempName = reader.Name;
    //            if (reader.NodeType == XmlNodeType.Element)
    //            {
    //                reader.Read();
    //                strTempValue = reader.Value;
    //                switch (strTempName)
    //                {
    //                    case "tipo_logradouro":
    //                        tipoLogradouro = strTempValue.ToUpper();
    //                        break;
    //                    case "logradouro":
    //                        logradouro = strTempValue.ToUpper();
    //                        break;
    //                    case "bairro":
    //                        bairro = strTempValue.ToUpper();
    //                        break;
    //                    case "cidade":
    //                        cidade = strTempValue.ToUpper();
    //                        break;
    //                    case "uf":
    //                        uf = strTempValue.ToUpper();
    //                        break;
    //                    case "resultado":
    //                        if (strTempValue == "1")
    //                        {
    //                            //cep ok
    //                        }
    //                        else
    //                        {
    //                            if (strTempValue == "-1")
    //                            {
    //                                lblMsgCEP.Text = "Cep não encontrado";
    //                            }
    //                            else if (strTempValue == "-2")
    //                            {
    //                                lblMsgCEP.Text = "Formato de CEP inválido";
    //                            }
    //                            else if (strTempValue == "-3")
    //                            {
    //                                lblMsgCEP.Text = "Busca de CEP congestionada. Aguarde alguns segundos e tente novamente.";
    //                            }
    //                            else
    //                            {
    //                                lblMsgCEP.Text = "Erro na busca de CEP.";
    //                            }
    //                            lblMsgCEP.Visible = true;
    //                        }
    //                        break;
    //                }
    //            }
    //        } while (reader.Read());

    //        txtEnderecoRecibo.Text = tipoLogradouro + " " + logradouro;

    //        txtBairroRecibo.Text = bairro;

    //        txtUFRecibo.SelectedValue = uf;

    //        txtCidadeRecibo.Text = cidade;
    //    }
    //    catch
    //    {
    //        lblMsgCEP.Visible = true;
    //    }
    //     * */
    //}
    protected void CustomValidatorPgto_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (TxtFormaPagamento.SelectedItem.Text.Trim() == "")
        {
            args.IsValid = false;
        }
    }

    //protected void txtTipoPessoa_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //txtNomeRecibo.Text = "";
    //    //txtUFRecibo.Text = "";
    //    //txtCidadeRecibo.Text = "";
    //    //txtEnderecoRecibo.Text = "";
    //    //txtComplementoEnderecoRecibo.Text = "";
    //    //txtBairroRecibo.Text = "";
    //    //txtCEPRecibo.Text = "";
    //    //txtIE.Text = "";

    //    //if (txtTipoPessoaRecibo.SelectedValue.ToString() == "PF")
    //    //{
    //    //    linhaCPF.Visible = true;
    //    //    linhaCNPJ.Visible = false;
    //    //    linhaIE.Visible = false;
    //    //    lblNome.Text = "Nome*";
    //    //}
    //    //else if (txtTipoPessoaRecibo.SelectedValue.ToString() == "PJ")
    //    //{
    //    //    linhaCPF.Visible = false;
    //    //    linhaCNPJ.Visible = true;
    //    //    linhaIE.Visible = true;
    //    //    lblNome.Text = "Razão Social*";
    //    //}


    //    if ((SessionIdioma == "PTBR") || (SessionParticipante.Categoria.NoCategoria.Contains("BRAZILIAN")))
    //    {
    //        if (txtTipoPessoaRecibo.SelectedValue.ToString() == "PF")
    //        {
    //            linhaCPF.Visible = true;
    //            linhaCNPJ.Visible = false;
    //            linhaIE.Visible = false;
    //            lblNome.Text = "Nome*";
    //            btnDadosParticipanteRecibo.Visible = false;

    //        }
    //        else if (txtTipoPessoaRecibo.SelectedValue.ToString() == "PJ")
    //        {
    //            linhaCPF.Visible = false;
    //            linhaCNPJ.Visible = true;
    //            linhaIE.Visible = true;
    //            lblNome.Text = "Razão Social*";
    //        }
    //    }
    //    else
    //    {
    //        if (txtTipoPessoaRecibo.SelectedValue.ToString() == "PF")
    //        {
    //            lblNome.Text = "Complete Name*";
    //        }
    //        else if (txtTipoPessoaRecibo.SelectedValue.ToString() == "PJ")
    //        {
    //            lblNome.Text = "Complete Company Name*";
    //        }
    //    }

    //}
    //protected void btnDadosParticipante_Click(object sender, EventArgs e)
    //{
    //    if (txtCPFRecibo.Text.Replace(".", "").Replace("-", "") == SessionParticipante.NuCPFCNPJ)
    //    {
    //        txtCPFRecibo.Text = oClsFuncoes.CPFCNPJMascarar(SessionParticipante.NuCPFCNPJ);
    //        txtNomeRecibo.Text = SessionParticipante.NoParticipante;
    //        txtUFRecibo.Text = SessionParticipante.DsUF;
    //        txtCidadeRecibo.Text = SessionEvento.DsFormaGuardarMunicipio == "dsMunicipio" ? SessionParticipante.NoCidade : Geral.BuscarNomeMunicipio(SessionParticipante.NoCidade, SessionCnn);
    //        txtEnderecoRecibo.Text = SessionParticipante.DsEndereco;
    //        txtComplementoEnderecoRecibo.Text = SessionParticipante.DsComplementoEndereco;
    //        txtBairroRecibo.Text = SessionParticipante.NoBairro;
    //        txtCEPRecibo.Text = oClsFuncoes.MascaraGerar(SessionParticipante.NuCEP, "99.999-999");
    //    }
    //    else
    //    {
    //        lblMsg2Recibo.Text = "";
    //        if (txtCPFRecibo.Text.Replace(".", "").Replace("-", "").Length < 11)
    //        {
    //            lblMsgPgto.Text = "CPF Inválido";
    //            return;
    //        }

    //        string tmpCPF = ValidarCPF("", SessionEvento.CdEvento, txtCPFRecibo.Text, SessionCnn);
    //        if (tmpCPF != "")
    //        {
    //            lblMsg2Recibo.Text = tmpCPF;
    //            return;

    //        }


    //        string tempCPF = txtCPFRecibo.Text.Replace(".", "").Replace("-", "");


    //        //PESQUISAR CPF BANCO LOCAL
    //        SqlConnection SessionCnnHISTORICO = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOWJYTnpjV3d1Wm1GNlpXNWtiMjFoYVhNdVkyOXRPMGx1YVhScFlXd2dRMkYwWVd4dlp6MW1ZWHBsYm1SdmJXRnBjekU3VUdWeWMybHpkQ0JUWldOMWNtbDBlU0JKYm1adlBWUnlkV1U3VlhObGNpQkpSRDFtWVhwbGJtUnZiV0ZwY3pFN1VHRnpjM2R2Y21ROVVYVTBOVFJ5Um0wPQ==")));
    //        DataTable DTCpf = oParticipanteCad.PesquisaCPF(txtCPFRecibo.Text.Replace(".", "").Replace("-", ""), SessionCnnHISTORICO);
    //        if ((DTCpf != null) && (DTCpf.Rows.Count > 0))
    //        {

    //            txtNomeRecibo.Text = DTCpf.DefaultView[0]["Nome"].ToString();

    //        }
    //        else
    //        {
    //            //PESQUISAR CPF BANCO RECEITA
    //            int tmpSaldoPesqCPF = Geral.verificarTotalPesquisaCPF(SessionCnn);

    //            if (tmpSaldoPesqCPF > 0)
    //            {

    //                DataSet ds = new DataSet();
    //                ds.ReadXml("http://ws.fontededados.com.br/consulta.asmx/SituacaoCadastralPF?login=" +
    //                    cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("Wm1GNlpXNWtiMjFoYVhNPQ==")) +
    //                    "&senha=" + cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("Um0xQWFUVXpORGcy")) +
    //                    "&cpf=" + txtCPFRecibo.Text.Replace(".", "").Replace("-", ""));

    //                if (ds != null)
    //                {
    //                    if (ds.Tables[0].Rows.Count > 0)
    //                    {
    //                        if (ds.Tables[0].Rows[0]["MensagemErro"].ToString() != "")
    //                        {
    //                            //    lblMsg.Text = ds.Tables[0].Rows[0]["MensagemErro"].ToString();
    //                            if (ds.Tables[0].Rows[0]["ValorCobrado"].ToString() != "0")
    //                                Geral.DecrementarPesquisaCPFReceita(SessionCnn);

    //                            if ((ds.Tables[0].Rows[0]["ValorCobrado"].ToString() == "8") ||
    //                                (ds.Tables[0].Rows[0]["ValorCobrado"].ToString() == "99"))
    //                            {
    //                                //lblMsg2.Text = ds.Tables[0].Rows[0]["MensagemErro"].ToString();
    //                                lblMsg2Recibo.Text = "Não foi possível localizar os dados do CPF informado!<br/>Preencha os campos manualmente.";
    //                                lblMsg2Recibo.Visible = true;
    //                                return;
    //                            }
    //                            lblMsg2Recibo.Text = "Não foi possível localizar os dados do CPF informado!<br/>Preencha os campos manualmente.";
    //                        }
    //                        else
    //                        {
    //                            oParticipanteCad.IncluirCPF(ds.Tables[0].Rows[0]["Cpf"].ToString(), ds.Tables[0].Rows[0]["Nome"].ToString(), SessionCnnHISTORICO);
    //                            Geral.DecrementarPesquisaCPFReceita(SessionCnn);

    //                            txtNomeRecibo.Text = ds.Tables[0].Rows[0]["Nome"].ToString();

    //                            //Server.Transfer("frmCadastroAuto.aspx?nome=" + ds.Tables[0].Rows[0]["Nome"].ToString() + "&cpf=" + ds.Tables[0].Rows[0]["Cpf"].ToString(), true);
    //                            //Response.Write("<script>window.open('frmCadastroAuto.aspx?nome=" + ds.Tables[0].Rows[0]["Nome"].ToString() + "&cpf=" + ds.Tables[0].Rows[0]["Cpf"].ToString() + "','_self');</script>"); //lblMsg.Text = ds.Tables[0].Rows[0]["Nome"].ToString();
    //                        }
    //                    }
    //                }
    //            }
    //            else //if (tmpSaldoPesqCPF == 0)
    //            {
    //                lblMsg2Recibo.Text = "Não foi possível localizar os dados do CPF informado!<br/>Preencha os campos manualmente.";
    //            }
    //            //else
    //            //{
    //            //    lblMsg2.Text = "Não foi possível localizar os dados do CPF informado!<br/>Preencha os campos manualmente.";
    //            //}
    //        }
    //    }
    //}

    //protected string ValidarCPF(string prmCdParticipante, string prmCdEvento, string prmCPF, SqlConnection prmCnn)
    //{
    //    if ((!oClsFuncoes.CPFCNPJValidar(prmCPF)))
    //    {
    //        return "CPF Inválido!";
    //    }
    //    else
    //    {
    //        string tmpCPF = prmCPF.Replace(".", "").Replace("-", "");

    //        if ((tmpCPF == "11111111111") ||
    //            (tmpCPF == "22222222222") ||
    //            (tmpCPF == "33333333333") ||
    //            (tmpCPF == "44444444444") ||
    //            (tmpCPF == "55555555555") ||
    //            (tmpCPF == "66666666666") ||
    //            (tmpCPF == "77777777777") ||
    //            (tmpCPF == "88888888888") ||
    //            (tmpCPF == "99999999999") ||
    //            (tmpCPF == "00000000000"))
    //        {
    //            return "CPF Inválido!";
    //        }
    //        else
    //        {
    //            return "";
    //        }

    //    }

    //}

    //protected void btnDadosInstituicao_Click(object sender, EventArgs e)
    //{
    //    lblMsg2Recibo.Text = "";
    //    if (txtCNPJRecibo.Text == "")
    //    {
    //        lblMsg2Recibo.Text = "Campo obrigatório.";
    //        return;
    //    }


    //    //PESQUISAR CNPJ BANCO LOCAL
    //    SqlConnection SessionCnnHISTORICO = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOWJYTnpjV3d1Wm1GNlpXNWtiMjFoYVhNdVkyOXRPMGx1YVhScFlXd2dRMkYwWVd4dlp6MW1ZWHBsYm1SdmJXRnBjekU3VUdWeWMybHpkQ0JUWldOMWNtbDBlU0JKYm1adlBWUnlkV1U3VlhObGNpQkpSRDFtWVhwbGJtUnZiV0ZwY3pFN1VHRnpjM2R2Y21ROVVYVTBOVFJ5Um0wPQ==")));
    //    DataTable DTCNPJ = oParticipanteCad.PesquisaCNPJ(txtCNPJRecibo.Text.Replace(".", "").Replace("-", "").Replace("/", ""), SessionCnnHISTORICO);
    //    if ((DTCNPJ != null) && (DTCNPJ.Rows.Count > 0))
    //    {
    //        txtNomeRecibo.Text = DTCNPJ.DefaultView[0]["NomeEmpresa"].ToString();
    //        txtUFRecibo.Text = DTCNPJ.DefaultView[0]["UF"].ToString();
    //        txtCidadeRecibo.Text = DTCNPJ.DefaultView[0]["Municipio"].ToString();
    //        txtEnderecoRecibo.Text = DTCNPJ.DefaultView[0]["Logradouro"].ToString();
    //        txtComplementoEnderecoRecibo.Text = DTCNPJ.DefaultView[0]["Complemento"].ToString();
    //        txtBairroRecibo.Text = DTCNPJ.DefaultView[0]["Bairro"].ToString();
    //        txtCEPRecibo.Text = oClsFuncoes.MascaraGerar(DTCNPJ.DefaultView[0]["CEP"].ToString(), "99.999-999");
    //        txtIE.Text = DTCNPJ.DefaultView[0]["InscricaoEstadual"].ToString();

    //    }
    //    else
    //    {
    //        //PESQUISAR CNPJ BANCO RECEITA
    //        int tmpSaldoPesqCPF = Geral.verificarTotalPesquisaCPF(SessionCnn);

    //        if (tmpSaldoPesqCPF > 0)
    //        {
    //            DataSet ds = new DataSet();
    //            ds.ReadXml("http://ws.fontededados.com.br/consulta.asmx/SituacaoCadastralPJ?login=" +
    //                cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("Wm1GNlpXNWtiMjFoYVhNPQ==")) +
    //                "&senha=" + cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("Um0xQWFUVXpORGcy")) +
    //                "&cnpj=" + txtCNPJRecibo.Text.Replace(".", "").Replace("-", "").Replace("/", ""));

    //            if (ds != null)
    //            {
    //                if (ds.Tables[0].Rows.Count > 0)
    //                {
    //                    if (ds.Tables[0].Rows[0]["MensagemErro"].ToString() != "")
    //                    {
    //                        //    lblMsg.Text = ds.Tables[0].Rows[0]["MensagemErro"].ToString();
    //                        if (ds.Tables[0].Rows[0]["ValorCobrado"].ToString() != "0")
    //                            Geral.DecrementarPesquisaCPFReceita(SessionCnn);

    //                        if ((ds.Tables[0].Rows[0]["ValorCobrado"].ToString() == "8") ||
    //                            (ds.Tables[0].Rows[0]["ValorCobrado"].ToString() == "99"))
    //                        {
    //                            //lblMsg2.Text = ds.Tables[0].Rows[0]["MensagemErro"].ToString();
    //                            lblMsg2Recibo.Text = "Não foi possível localizar os dados do CNPJ informado!<br/>Preencha os campos manualmente.";
    //                            lblMsg2Recibo.Visible = true;
    //                            return;
    //                        }
    //                        lblMsg2Recibo.Text = "Não foi possível localizar os dados do CNPJ informado!<br/>Preencha os campos manualmente.";
    //                    }
    //                    else
    //                    {
    //                        oParticipanteCad.IncluirCNPJ(
    //                            ds.Tables[0].Rows[0]["CNPJ"].ToString().Replace(".", "").Replace("-", "").Replace("/", ""),
    //                            ds.Tables[0].Rows[0]["NomeEmpresa"].ToString(),
    //                            ds.Tables[0].Rows[0]["Logradouro"].ToString(),
    //                            ds.Tables[0].Rows[0]["Complemento"].ToString() + " - " + ds.Tables[0].Rows[0]["Numero"].ToString(),
    //                            ds.Tables[0].Rows[0]["Bairro"].ToString(),
    //                            ds.Tables[0].Rows[0]["Municipio"].ToString(),
    //                            ds.Tables[0].Rows[0]["CEP"].ToString().Replace(".", "").Replace("-", ""),
    //                            ds.Tables[0].Rows[0]["UF"].ToString(),
    //                            "",
    //                            SessionCnnHISTORICO);
    //                        Geral.DecrementarPesquisaCPFReceita(SessionCnn);

    //                        txtNomeRecibo.Text = ds.Tables[0].Rows[0]["NomeEmpresa"].ToString();

    //                        txtUFRecibo.Text = ds.Tables[0].Rows[0]["UF"].ToString();
    //                        txtCidadeRecibo.Text = ds.Tables[0].Rows[0]["Municipio"].ToString();
    //                        txtEnderecoRecibo.Text = ds.Tables[0].Rows[0]["Logradouro"].ToString();
    //                        txtComplementoEnderecoRecibo.Text = ds.Tables[0].Rows[0]["Complemento"].ToString() + " - " + ds.Tables[0].Rows[0]["Numero"].ToString();
    //                        txtBairroRecibo.Text = ds.Tables[0].Rows[0]["Bairro"].ToString();
    //                        txtCEPRecibo.Text = ds.Tables[0].Rows[0]["CEP"].ToString();


    //                        //Server.Transfer("frmCadastroAuto.aspx?nome=" + ds.Tables[0].Rows[0]["Nome"].ToString() + "&cpf=" + ds.Tables[0].Rows[0]["Cpf"].ToString(), true);
    //                        //Response.Write("<script>window.open('frmCadastroAuto.aspx?nome=" + ds.Tables[0].Rows[0]["Nome"].ToString() + "&cpf=" + ds.Tables[0].Rows[0]["Cpf"].ToString() + "','_self');</script>"); //lblMsg.Text = ds.Tables[0].Rows[0]["Nome"].ToString();
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                lblMsg2Recibo.Text = "Não foi possível localizar os dados do cnpj informado!<br/>Preencha os campos manualmente.";
    //                return;
    //            }
    //        }
    //        else
    //        {
    //            lblMsg2Recibo.Text = "Não foi possível localizar os dados do cnpj informado!<br/>Preencha os campos manualmente.";
    //        }
    //    }
    //}
    protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
    {

    }
    protected void btnCupomDesconto_Click(object sender, EventArgs e)
    {
        lblMsgCupom.Text = "";
        lblMsgCupom.Visible = false;

        if (txtCupomDesconto.Text == "")
        {
            if (SessionIdioma == "PTBR")
                lblMsgCupom.Text = "Informe o seu cupom de desconto";
            else if (SessionIdioma == "ENUS")
                lblMsgCupom.Text = "Enter discount code";
            else if (SessionIdioma == "ESP")
                lblMsgCupom.Text = "Informe a su cupón de descuento";
            
            lblMsgCupom.Visible = true;
            txtCupomDesconto.Focus();
            return;
        }

        decimal vlDesconto = 0;

        DescontoCupom oDescontoCupom;
        DescontoCupomCad oDescontoCupomCad = new DescontoCupomCad();

        if ((SessionPedido.pedidoCupomDesconto == null) || (SessionPedido.pedidoCupomDesconto.CdCupomDesconto == ""))
        {
            oDescontoCupom = oDescontoCupomCad.PesquisarPorCupom(SessionEvento.CdEvento, txtCupomDesconto.Text.ToUpper(), SessionCnn);

            if (oDescontoCupom == null)
            {
                if (SessionIdioma == "PTBR")
                    lblMsgCupom.Text = "Cupom de desconto inválido.";
                else if (SessionIdioma == "ENUS")
                    lblMsgCupom.Text = "Invalid discount code.";
                else if (SessionIdioma == "ESP")
                    lblMsgCupom.Text = "Cupón de descuento no es válido.";

                lblMsgCupom.Visible = true;
                txtCupomDesconto.Focus();
                return;
            }
            else if ((oDescontoCupom.DtValidadeDesconto != null) && (oDescontoCupom.DtValidadeDesconto < Geral.datahoraServidor(SessionCnn)))
            {
                if (SessionIdioma == "PTBR")
                    lblMsgCupom.Text = "Cupom de desconto expirado.";
                else if (SessionIdioma == "ENUS")
                    lblMsgCupom.Text = "Discount code expired.";
                else if (SessionIdioma == "ESP")
                    lblMsgCupom.Text = "Cupón de descuento expirado.";

                lblMsgCupom.Visible = true;
                txtCupomDesconto.Focus();
                return;
            }
            else if ((oDescontoCupom.QtdLimteUso != 0) && (oDescontoCupom.QtdLimteUso <= oDescontoCupom.QtdUtilizado))
            {
                if (SessionIdioma == "PTBR")
                    lblMsgCupom.Text = "Cupom de desconto já utilizado.";
                else if (SessionIdioma == "ENUS")
                    lblMsgCupom.Text = "Discount code already used.";
                else if (SessionIdioma == "ESP")
                    lblMsgCupom.Text = "Cupón de descuento ya se utiliza.";

                lblMsgCupom.Visible = true;
                txtCupomDesconto.Focus();
                return;
            }
            else if (!oDescontoCupomCad.VerificarCategoriaBlequeadaCupomDesconto(SessionEvento.CdEvento, oDescontoCupom.CdCupomDesconto, SessionParticipante.CdCategoria, SessionCnn))  
            {
                if (SessionIdioma == "PTBR")
                    lblMsgCupom.Text = "Cupom bloqueado para esta categora de participação.";
                else if (SessionIdioma == "ENUS")
                    lblMsgCupom.Text = "Coupon blocked for this category of participation.";
                else if (SessionIdioma == "ESP")
                    lblMsgCupom.Text = "Cupón bloqueado para esta categoría de participación.";

                lblMsgCupom.Visible = true;
                txtCupomDesconto.Focus();
                return;
            }

            vlDesconto = oDescontoCupom.VlDesconto;

            if (oDescontoCupom.TpDesconto.ToUpper() != "REAL")
            {
                vlDesconto = Math.Round(SessionPedido.VlTotalPedido * (vlDesconto / 100),2);
            }

            DataTable dtpedido = oPedidoCad.TotalizarPedido(SessionPedido.CdPedido, SessionCnn);
            if (dtpedido != null)
            {
                vlItensPgto.Text = dtpedido.DefaultView[0]["itens"].ToString();
                vlTotalAtivPgto.Text = decimal.Parse(dtpedido.DefaultView[0]["tol_Atv"].ToString()).ToString("N2");
                vlTotalDesc.Text = (decimal.Parse(dtpedido.DefaultView[0]["tot_desc"].ToString()) + vlDesconto).ToString("N2");

                vlTotalAtivPgto.Font.Strikeout = (decimal.Parse(vlTotalDesc.Text) > 0);
            }

            vlTotalPedidoPgto.Text = (SessionPedido.VlTotalPedido - vlDesconto).ToString("N2");
            
            if ((SessionPedido.VlTotalPedido - vlDesconto) <= 0)
            {
                //pnlDadosRecibo.Visible = false;
                pnlInfoPgto.Visible = false;
            }
            
        }

    }

    protected void btnAlterarCupomDesconto_Click(object sender, EventArgs e)
    {
        lblMsgCupom.Text = "";
        lblMsgCupom.Visible = false;

        oPedidoCad.oPedidoCupomDescontoCad.ExcluirPedidoCupomDesconto(SessionPedido.CdEvento, SessionPedido.CdPedido, SessionCnn);

        DataTable dtpedido = oPedidoCad.TotalizarPedido(SessionPedido.CdPedido, SessionCnn);
        if (dtpedido != null)
        {
            vlItensPgto.Text = dtpedido.DefaultView[0]["itens"].ToString();
            vlTotalAtivPgto.Text = decimal.Parse(dtpedido.DefaultView[0]["tol_Atv"].ToString()).ToString("N2");
            vlTotalDesc.Text = (decimal.Parse(dtpedido.DefaultView[0]["tot_desc"].ToString())).ToString("N2");

            vlTotalAtivPgto.Font.Strikeout = (decimal.Parse(vlTotalDesc.Text) > 0);
        }

        pnlInfoPgto.Visible = true;

        if (SessionEvento.FlEmiteRecibo)
        {
           // pnlDadosRecibo.Visible = true;            
        }

        vlTotalPedidoPgto.Text = (SessionPedido.VlTotalPedido).ToString("N2");

        btnAlterarCupomDesconto.Visible = false;
        lblAvisoDescontoCalculado.Visible = false;

        txtCupomDesconto.Text = "";
        txtCupomDesconto.Enabled = true;
        btnCupomDesconto.Visible = true;

        SessionPedido = oPedidoCad.Pesquisar(SessionEvento.CdEvento, SessionPedido.CdPedido, SessionCnn);
        Session["SessionPedido"] = SessionPedido;

        txtCupomDesconto.Focus();
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

                
                Dap = new SqlDataAdapter(comando);

                Dap.TableMappings.Add("PedidoInVoice", "tbPedidoInVoice");
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

    public void ListarPaises(DropDownList prmCampoListagem)
    {
        lblMsgPgto.Visible = false;
        if (SessionCnn == null)
        {
            //_erroNoCampo = true;
            lblMsgPgto.Visible = true;
            lblMsgPgto.Text = "Conexão inválida ou inexistente";
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
                lblMsgPgto.Visible = true;
                lblMsgPgto.Text = "Conexão inválida";
                return;
            }
        }

        DataTable DT = new DataTable();
        SqlDataAdapter Dap;
        try
        {
            try
            {
                string cmd = "SELECT " +
                      "cdPais, " +
                      "dbo.TIRA_ACENTO(dsPais) as dsPais, " +
                      "dbo.TIRA_ACENTO(dsPaisIngles) as dsPaisIngles, " +
                      "dbo.TIRA_ACENTO(dsPaisEspanhol) as dsPaisEspanhol, " +
                      "dbo.TIRA_ACENTO(dsPaisFrances) as dsPaisFrances " +
                    "FROM " +
                      "dbo.tbPaises  " +
                    "ORDER BY ";

                cmd += SessionIdioma == "PTBR" ? "2" : SessionIdioma == "ENUS" ? "3" : SessionIdioma == "ESP" ? "4" : "5";

                SqlCommand comando = new SqlCommand(
                     cmd, SessionCnn);


                Dap = new SqlDataAdapter(comando);

                Dap.TableMappings.Add("PAISES", "tbPaises");
                Dap.Fill(DT);

                SessionCnn.Close();

                //BindingSource bsufs = new BindingSource();

                DataTable oDataTable = new DataTable();



                oDataTable.Columns.Add("dsPais");
                oDataTable.Columns.Add("dsPaisIngles");
                oDataTable.Columns.Add("dsPaisEspanhol");
                oDataTable.Columns.Add("dsPaisFrances");

                oDataTable.Rows.Add("", "", "", "");

                for (int i = 0; i < DT.DefaultView.Count; i++)
                {
                    oDataTable.Rows.Add(DT.DefaultView[i]["dsPais"],
                                        DT.DefaultView[i]["dsPaisIngles"],
                                        DT.DefaultView[i]["dsPaisEspanhol"],
                                        DT.DefaultView[i]["dsPaisFrances"]);
                }


                //bsufs.DataSource = oDataTable.DefaultView;
                prmCampoListagem.DataSource = oDataTable.DefaultView;
                if (SessionIdioma == "PTBR")
                    prmCampoListagem.DataTextField = "dsPais";
                else if (SessionIdioma == "ENUS")
                    prmCampoListagem.DataTextField = "dsPaisIngles";
                else if (SessionIdioma == "ESP")
                    prmCampoListagem.DataTextField = "dsPaisEspanhol";
                else if (SessionIdioma == "FRA")
                    prmCampoListagem.DataTextField = "dsPaisFrances";

                prmCampoListagem.DataValueField = "dsPais";
                prmCampoListagem.DataBind();
                //this.SelectedIndex = 0;

                //this.DropDownStyle = ComboBoxStyle.DropDownList;

            }
            catch (Exception Ex)
            {
                //_erroNoCampo = true;
                lblMsgPgto.Visible = true;
                lblMsgPgto.Text = "Erro ao selecionar Países!\n" + Ex.Message;
                SessionCnn.Close();
            }
        }
        finally
        {
            SessionCnn.Close();
        }


    }
}

public static class ResponseHelper
{
    public static void Redirect(string url, string target, string windowFeatures)
    {
        HttpContext context = HttpContext.Current;

        if ((String.IsNullOrEmpty(target) ||
            target.Equals("_self", StringComparison.OrdinalIgnoreCase)) &&
            String.IsNullOrEmpty(windowFeatures))
        {

            context.Response.Redirect(url);
        }
        else
        {
            Page page = (Page)context.Handler;
            if (page == null)
            {
                throw new InvalidOperationException(
                    "Cannot redirect to new window outside Page context.");
            }
            url = page.ResolveClientUrl(url);

            string script;
            if (!String.IsNullOrEmpty(windowFeatures))
            {
                script = @"window.open(""{0}"", ""{1}"", ""{2}"");";
            }
            else
            {
                script = @"window.open(""{0}"", ""{1}"");";
            }

            script = String.Format(script, url, target, windowFeatures);
            ScriptManager.RegisterStartupScript(page,
                typeof(Page),
                "Redirect",
                script,
                true);
        }
    }
}
