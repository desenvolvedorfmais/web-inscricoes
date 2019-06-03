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

public partial class frmDadosReciboNF : BaseWebUi //System.Web.UI.Page
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

            
            //ListarUFRecibo();

            //ListarCidades("");

            //PesquisarDadosRecibo();


            ListarPaises(txtPaisRecibo);

            

            
            /*
            if ((!SessionEvento.FlEmiteRecibo) || (SessionIdioma != "PTBR"))
            {
                pnlDadosRecibo.Visible = false;

                //txtCPFCNPJRecibo.Text = oClsFuncoes.CPFCNPJMascarar(SessionParticipante.NuCPFCNPJ);
                //txtNomeRecibo.Text = SessionParticipante.NoParticipante;
                //txtUFRecibo.Text = SessionParticipante.DsUF;
                //txtCidadeRecibo.Text = SessionParticipante.NoCidade;
                //txtEnderecoRecibo.Text = SessionParticipante.DsEndereco;
                //txtCEPRecibo.Text = oClsFuncoes.MascaraGerar(SessionParticipante.NuCEP, "99.999-999");
            }

            //if ((SessionParticipante.CdCategoria == "00700201") || (SessionParticipante.CdCategoria == "00700202"))
            if (SessionParticipante.Categoria.NoCategoria.Contains("BRAZILIAN"))
            {
                pnlDadosRecibo.Visible = true;
            }

            if (SessionEvento.CdCliente == "0014")
            {
                lblTituloDadosRecibo.Text = "Dados para nota fiscal";
            }

            if (!SessionEvento.FlPesquisaCPFReceita)
            {
                btnDadosParticipanteRecibo.Visible = false;
               // btnDadosInstituicaoRecibo.Visible = false;
            }
            */

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


            if (SessionEvento.CdEvento == "007002")
            {
                if (!SessionParticipante.Categoria.NoCategoria.Contains("BRAZILIAN"))                    
                {
                    txtTipoPessoaRecibo.Items.Clear();

                    ListItem item = new ListItem("A PERSON", "PF", true);
                    txtTipoPessoaRecibo.Items.Add(item);

                    ListItem item2 = new ListItem("A COMPANY", "PJ");
                    txtTipoPessoaRecibo.Items.Add(item2);

                    btnCEP.Visible = false;
                    linhaCPF.Visible = false;
                    linhaIE.Visible = false;
                    linhaCNPJ.Visible = false;
                    linhaBairro.Visible = false;
                    
                    linhaUF.Visible = false;
                    linhaCidade.Visible = false;
                    linhaRamalRespFin.Visible = false;
                }
            }
            else if (SessionIdioma == "PTBR")
            {
                linhaPais.Visible = false;
            }
            else if (SessionIdioma == "ENUS")
            {
                txtTipoPessoaRecibo.Items.Clear();

                ListItem item = new ListItem("A PERSON", "PF", true);
                txtTipoPessoaRecibo.Items.Add(item);

                ListItem item2 = new ListItem("A COMPANY", "PJ");
                txtTipoPessoaRecibo.Items.Add(item2);

                btnCEP.Visible = false;
                linhaCPF.Visible = false;
                linhaIE.Visible = false;
                linhaCNPJ.Visible = false;
                linhaBairro.Visible = false;

                linhaUF.Visible = false;
                linhaCidade.Visible = false;
                linhaRamalRespFin.Visible = false;
            }
            else if (SessionIdioma == "ESP")
            {
                txtTipoPessoaRecibo.Items.Clear();

                ListItem item = new ListItem("UNA PERSONA", "PF", true);
                txtTipoPessoaRecibo.Items.Add(item);

                ListItem item2 = new ListItem("UNA EMPRESA", "PJ");
                txtTipoPessoaRecibo.Items.Add(item2);

                btnCEP.Visible = false;
                linhaCPF.Visible = false;
                linhaIE.Visible = false;
                linhaCNPJ.Visible = false;
                linhaBairro.Visible = false;

                linhaUF.Visible = false;
                linhaCidade.Visible = false;
                linhaRamalRespFin.Visible = false;
            }


            if (SessionPedido.DsNomeRecibo != "")
            {
                
                txtTipoPessoaRecibo.Text = SessionPedido.TpPessoa;
                txtTipoPessoa_SelectedIndexChanged(sender, e);

                if (txtTipoPessoaRecibo.SelectedValue.ToString() == "PF")
                    txtCPFRecibo.Text = oClsFuncoes.CPFCNPJMascarar(SessionPedido.NuCPFCNPJRecibo);
                else
                    txtCNPJRecibo.Text = oClsFuncoes.CPFCNPJMascarar(SessionPedido.NuCPFCNPJRecibo);

                txtNomeRecibo.Text = SessionPedido.DsNomeRecibo;
                cascaDD_txtUF.SelectedValue = SessionPedido.DsUFRecibo;
                if (linhaCidade.Visible)
                    cascaDD_txtMunicipio.SelectedValue = SessionPedido.NoCidadeRecibo;
                txtEnderecoRecibo.Text = SessionPedido.DsEnderecoRecibo;
                txtComplementoEnderecoRecibo.Text = SessionPedido.DsComplementoEnderRecibo;
                txtBairroRecibo.Text = SessionPedido.NoBairroRecibo;
                txtCEPRecibo.Text = SessionPedido.NuCEPRecibo;
                txtEmailRespFin.Text = SessionPedido.DsEmailResponsavelFinanceiro;
                txtIE.Text = SessionPedido.DsInscricaoEstadualRecibo;
                txtPaisRecibo.Text = SessionPedido.NoPaisRecibo;
                txtFoneRespFin.Text = SessionPedido.DsFoneResponsavelFinanceiro;
                txtRamalRespFin.Text = SessionPedido.DsRamalResponsavelFinanceiro;
            }
            //else
            //    txtTipoPessoa_SelectedIndexChanged(sender, e);
        }
        else
        {
            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            SessionPedido = (Pedido)Session["SessionPedido"];

            

            SessionIdioma = (String)Session["SessionIdioma"];
        }

        TSManager1.RegisterPostBackControl(btnConfirmarDadosRecibo);
        

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

            lblTituloPagina.Text = "Detalhes do Pedido";
                        
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

            btnConfirmarDadosRecibo.Text = "Continuar";

            if (SessionEvento.CdEvento == "007701")
            {
                btnConfirmarDadosRecibo.Text = "Enviar";
                //lblMsgWCIT.Text = ""
            }

            if (SessionEvento.CdEvento == "008501")
            {
                lblTituloDadosRecibo.Text = "Dados para Nota Empenho";
            }

            if (SessionEvento.CdEvento == "007002")
                lblTituloDadosRecibo.Text = "Escolha abaixo sua opção e preencha os dados correspondentes PARA EMISSÃO DA NOTA FISCAL:";

        }
        else if (prmIdioma == "ENUS")
        {
            SessionIdioma = "ENUS";
            Session["SessionIdioma"] = SessionIdioma;

            btnConfirmarDadosRecibo.Text = "Next";

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


            lblTituloDadosRecibo.Text = "Please choose the option below and fill in the form for the invoice:";
            if (SessionEvento.CdEvento == "007002")
            {
                if (SessionParticipante.Categoria.NoCategoria.Contains("BRAZILIAN"))
                {
                    lblTituloDadosRecibo.Text = "Escolha abaixo sua opção e preencha os dados correspondentes PARA EMISSÃO DA NOTA FISCAL:";
                    Label3.Text = "E-mail para envio da Nota Fiscal";
                }
                else
                {
                    lblTipoPessoa.Text = "Invoice for *";
                    lblNome.Text = "Name *";
                    Label1.Text = "Country *";
                    Label12.Text = "ZIP/Postal Code *";
                    Label13.Text = "Address *";
                    Label151.Text = "City/State/Province *";
                    Label3.Text = "E-mail to send the Invoice *";
                    Label4.Text = "Telephone Number * - Enter including country code (e.g. +55 61 1234-5678)";
                }
            }
            else
            {
                lblTipoPessoa.Text = "Invoice for *";
                lblNome.Text = "Name *";
                Label1.Text = "Country *";
                Label12.Text = "ZIP/Postal Code *";
                Label13.Text = "Address *";
                Label151.Text = "City/State/Province *";
                Label3.Text = "E-mail to send the Invoice *";
                Label4.Text = "Telephone Number * - Enter including country code (e.g. +55 61 1234-5678)";
            }

            RequiredFieldValidator1.ErrorMessage = "Required field";
            RFVCPF.ErrorMessage = "Required field";
            RequiredFieldValidator2.ErrorMessage = "Required field";
            RequiredFieldValidator3.ErrorMessage = "Required field";
            RequiredFieldValidator12.ErrorMessage = "Required field";
            RequiredFieldValidator5.ErrorMessage = "Required field";
            RequiredFieldValidator11.ErrorMessage = "Required field";
            RequiredFieldValidator14.ErrorMessage = "Required field";
            RequiredFieldValidator9.ErrorMessage = "Required field";
            RequiredFieldValidator10.ErrorMessage = "Required field";
            RequiredFieldValidator3.ErrorMessage = "Required field";
            RequiredFieldValidator4.ErrorMessage = "Required field";
            
        }
        else if (prmIdioma == "ESP")
        {
            SessionIdioma = "ESP";
            Session["SessionIdioma"] = SessionIdioma;

            btnConfirmarDadosRecibo.Text = "Siguiente";

            lblTituloPagina.Text = "Formas de Pago";

            lblID.Text = "Nº de Registro:";
            lblPart.Text = "Participante:";
            lblCateg.Text = "Categoría:";

            lblTituloResumoPgto.Text = "Resúmen del pedido";
            lblResPedPgto.Text = "Pedido nº";
            lblResItensPgto.Text = "Itens";
            lblResVlrPgto.Text = "Valor";
            lblResDescPgto.Text = "Descuento";

            


            lblResVlrTotalPgto.Text = "Total ($)";
            if ((SessionParticipante.NoPais == "BRASIL") ||
                (SessionParticipante.NoPais == "BRAZIL") ||
                (SessionParticipante.NoPais == "BRASIL") ||
                (SessionParticipante.NoPais == "BRÉSIL"))
                lblResVlrTotalPgto.Text = "Total (R$)";
            else
                lblResVlrTotalPgto.Text = "Total ($)";


            lblTituloDadosRecibo.Text = "Elija la opción siguiente y complete el formulario de la factura:";

            lblTipoPessoa.Text = "Factura para *";
            lblNome.Text = "Nombre *";
            Label1.Text = "País *";
            Label12.Text = "Código postal *";
            Label13.Text = "Dirección *";
            Label151.Text = "Ciudad/Estado/Provincia *";
            Label3.Text = "E-mail para enviar la factura *";
            Label4.Text = "Número de teléfono * - ingrese el código de país incluido (por ejemplo, +55 61 1234-5678)";

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

    
    

    protected void PesquisarDadosRecibo()
    {

        if (SessionParticipante == null)
            return;



        //PedidoCad oPedidoCad = new PedidoCad();

        //Pedido oPedido = oPedidoCad.Pesquisar(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionEvento.CdEvento + prmCdParticipante + "001", SessionCnn);

        if ((SessionPedido == null) || (SessionPedido.DsNomeRecibo.Trim() == ""))
        {
            //txtCPFCNPJRecibo.Text = oClsFuncoes.CPFCNPJMascarar(SessionParticipante.NuCPFCNPJ);
            //txtNomeRecibo.Text = SessionParticipante.NoParticipante;
            //txtUFRecibo.Text = SessionParticipante.DsUF;
            //txtCidadeRecibo.Text = SessionParticipante.NoCidade;
            //txtEnderecoRecibo.Text = SessionParticipante.DsEndereco;
            //txtCEPRecibo.Text = oClsFuncoes.MascaraGerar(SessionParticipante.NuCEP, "99.999-999");
        }
        else
        {
            txtTipoPessoaRecibo.SelectedValue = SessionPedido.TpPessoa;
            if (txtTipoPessoaRecibo.SelectedValue.ToString() == "PF")
            {
                linhaCPF.Visible = true;
                linhaCNPJ.Visible = false;
                linhaIE.Visible = false;
                lblNome.Text = "Nome*";
                txtCPFRecibo.Text = oClsFuncoes.CPFCNPJMascarar(SessionPedido.NuCPFCNPJRecibo);
            }
            else if (txtTipoPessoaRecibo.SelectedValue.ToString() == "PJ")
            {
                linhaCPF.Visible = false;
                linhaCNPJ.Visible = true;
                linhaIE.Visible = true;
                lblNome.Text = "Razão Social*";
                txtCNPJRecibo.Text = oClsFuncoes.CPFCNPJMascarar(SessionPedido.NuCPFCNPJRecibo);
                txtIE.Text = SessionPedido.DsInscricaoEstadualRecibo;
            }

            txtTipoPessoaRecibo.AutoPostBack = true;

            
            txtNomeRecibo.Text = SessionPedido.DsNomeRecibo;
            txtUFRecibo.Text = SessionPedido.DsUFRecibo;
            txtCidadeRecibo.Text = SessionPedido.NoCidadeRecibo;
            txtEnderecoRecibo.Text = SessionPedido.DsEnderecoRecibo;
            txtComplementoEnderecoRecibo.Text = SessionPedido.DsComplementoEnderRecibo;
            txtBairroRecibo.Text = SessionPedido.NoBairroRecibo;
            txtCEPRecibo.Text = oClsFuncoes.MascaraGerar(SessionPedido.NuCEPRecibo, "99.999-999");

            //txtNomeRespFin.Text = SessionPedido.DsNomeResponsavelFinanceiro;
            txtEmailRespFin.Text = SessionPedido.DsEmailResponsavelFinanceiro;
            txtFoneRespFin.Text = oClsFuncoes.MascaraGerar(SessionPedido.DsFoneResponsavelFinanceiro, "(99) 999999999"); ;
            txtRamalRespFin.Text = SessionPedido.DsRamalResponsavelFinanceiro;

        }
    }

   

    protected void btnConfirmar_Click(object sender, EventArgs e)
    {

        lblMsg.Text = "";
        lblMsg.Visible = false;
        

        if (txtCPFRecibo.Text.Replace(".", "").Replace("-", "").Replace("/", "").Trim() != "")
        {
            if (ValidarCPF(txtCPFRecibo.Text.Replace(".", "").Replace("-", "").Replace("/", "").Trim()) != "")
            {
                lblMsg.Text = "CPF inválido!";
                lblMsg.Visible = true;
                return;
            }
        }

        if (txtCPFRecibo.Text.Replace(".", "").Replace("-", "").Replace("/", "").Trim() != "")
        {
            if (txtTipoPessoaRecibo.SelectedValue.ToString() == "PF")
            {
                if (ValidarCPF(txtCPFRecibo.Text.Replace(".", "").Replace("-", "").Replace("/", "").Trim()) != "")
                {
                    lblMsg.Text = "CPF inválido!";
                    lblMsg.Visible = true;
                    return;
                }
            }
            else
            {
                if ((!oClsFuncoes.CPFCNPJValidar(txtCPFRecibo.Text.Replace(".", "").Replace("-", "").Replace("/", "").Trim())))
                {
                    lblMsg.Text = "CNPJ Inválido!";
                    lblMsg.Visible = true;
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
        
        lblMsg.Text = "";
        if (lblIdentificador.Text.Trim() == "")
            return;

        
        
        SessionPedido.TpPessoa = txtTipoPessoaRecibo.SelectedValue.ToString();
        SessionPedido.DsNomeRecibo = txtNomeRecibo.Text;
        SessionPedido.NuCPFCNPJRecibo = txtTipoPessoaRecibo.SelectedValue.ToString() == "PF" ? txtCPFRecibo.Text.Replace(".", "").Replace("-", "").Replace("/", "") : txtCNPJRecibo.Text.Replace(".", "").Replace("-", "").Replace("/", "");
        SessionPedido.DsUFRecibo = cascaDD_txtUF.SelectedValue.ToString();
        SessionPedido.NoCidadeRecibo = txtCidadeRecibo.SelectedValue.ToString();//cascaDD_txtMunicipio.SelectedValue.ToString();
        SessionPedido.DsEnderecoRecibo = txtEnderecoRecibo.Text;
        SessionPedido.DsComplementoEnderRecibo = txtComplementoEnderecoRecibo.Text;
        SessionPedido.NoBairroRecibo = txtBairroRecibo.Text;
        SessionPedido.NuCEPRecibo = txtCEPRecibo.Text;
        SessionPedido.DsInscricaoEstadualRecibo = txtIE.Text;
        SessionPedido.NoPaisRecibo = txtPaisRecibo.SelectedValue.ToString();
        
        SessionPedido.DsEmailResponsavelFinanceiro = txtEmailRespFin.Text.ToLower();
        SessionPedido.DsFoneResponsavelFinanceiro = txtFoneRespFin.Text.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "");
        SessionPedido.DsRamalResponsavelFinanceiro = txtRamalRespFin.Text.ToUpper();


        SessionPedido = oPedidoCad.Gravar(SessionPedido, SessionCnn);
        Session["SessionPedido"] = SessionPedido;

        
        Response.Redirect("frm_formapagamento.aspx?cdMatricula=" + SessionParticipante.CdParticipante, false);
        
        return;
        
        

    }

   
    
    
    protected void btnCEP_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsgCEP.Visible = false;

            txtEnderecoRecibo.Text = "";
            txtBairroRecibo.Text = "";
            txtCidadeRecibo.Text = "";
            txtUFRecibo.SelectedValue = "";

            if (txtCEPRecibo.Text.Replace(".", "").Replace("-", "").Trim() == "")
                return;


            WebCEP webcep = new WebCEP(txtCEPRecibo.Text.Replace(".", "").Replace("-", ""));

            if (webcep != null)
            {
                txtEnderecoRecibo.Text = webcep.TipoLagradouro + " " + webcep.Lagradouro;

                txtBairroRecibo.Text = webcep.Bairro;

                cascaDD_txtUF.SelectedValue = webcep.UF;

                string noCidade = webcep.Cidade;//cidade
                noCidade = noCidade.Replace("'", ":");
                noCidade = oClsFuncoes.RemoveAcento(noCidade);
                noCidade = noCidade.Replace(":", "`");

                cascaDD_txtMunicipio.SelectedValue = noCidade.ToUpper();
                return;
            }
            else
            {
                lblMsgCEP.Visible = true;
                lblMsgCEP.Text = webcep.ResultadoTXT;
                return;
            }
        }
        catch
        {
            lblMsgCEP.Visible = true;
        }
        
    }
    

    protected void txtTipoPessoa_SelectedIndexChanged(object sender, EventArgs e)
    {        

        if ((SessionIdioma == "PTBR") || (SessionParticipante.Categoria.NoCategoria.Contains("BRAZILIAN")))
        {
            if (txtTipoPessoaRecibo.SelectedValue.ToString() == "PF")
            {
                linhaCPF.Visible = true;
                linhaCNPJ.Visible = false;
                linhaIE.Visible = false;
                lblNome.Text = "Nome*";
                btnDadosParticipanteRecibo.Visible = false;

            }
            else if (txtTipoPessoaRecibo.SelectedValue.ToString() == "PJ")
            {
                linhaCPF.Visible = false;
                linhaCNPJ.Visible = true;
                linhaIE.Visible = true;
                lblNome.Text = "Razão Social*";
            }
        }
        else
        {
            if (txtTipoPessoaRecibo.SelectedValue.ToString() == "PF")
            {
                lblNome.Text = "Complete Name*";
            }
            else if (txtTipoPessoaRecibo.SelectedValue.ToString() == "PJ")
            {
                lblNome.Text = "Complete Company Name*";
            }
        }


        if (SessionPedido.DsNomeRecibo == "")
        {
            /*
            //txtNomeRecibo.Text = "";
            //txtUFRecibo.Text = "";
            //txtCidadeRecibo.Text = "";
            //txtEnderecoRecibo.Text = "";
            //txtComplementoEnderecoRecibo.Text = "";
            //txtBairroRecibo.Text = "";
            //txtCEPRecibo.Text = "";
            //txtIE.Text = "";

            txtCPFRecibo.Text = oClsFuncoes.CPFCNPJMascarar(SessionParticipante.NuCPFCNPJ);
            txtNomeRecibo.Text = (txtTipoPessoaRecibo.SelectedValue.ToString() == "PF" ? SessionParticipante.NoParticipante : SessionParticipante.NoInstituicao);
            cascaDD_txtUF.SelectedValue = SessionParticipante.DsUF;
            if (linhaCidade.Visible)
                cascaDD_txtMunicipio.SelectedValue = SessionEvento.DsFormaGuardarMunicipio == "dsMunicipio" ? SessionParticipante.NoCidade : Geral.BuscarNomeMunicipio(SessionParticipante.NoCidade, SessionCnn);
            txtEnderecoRecibo.Text = SessionParticipante.DsEndereco;
            txtComplementoEnderecoRecibo.Text = SessionParticipante.DsComplementoEndereco;
            txtBairroRecibo.Text = SessionParticipante.NoBairro;
            txtCEPRecibo.Text = SessionParticipante.NuCEP;
            txtEmailRespFin.Text = SessionParticipante.DsEmail;
            txtPaisRecibo.Text = SessionParticipante.NoPais;

            if (SessionEvento.CdEvento == "007002")
            {
                if (!SessionParticipante.Categoria.NoCategoria.Contains("BRAZILIAN"))
                    txtComplementoEnderecoRecibo.Text = SessionParticipante.NoCidade + " / " + SessionParticipante.DsAuxiliar6;
                //else
                //    linhaComplemento.Visible = false;

                if (txtTipoPessoaRecibo.SelectedValue.ToString() == "PF")
                {
                    txtNomeRecibo.Text = SessionParticipante.NoParticipante + (SessionParticipante.DsAuxiliar1 != "" ? " " + SessionParticipante.DsAuxiliar1 : "") + " " + SessionParticipante.DsAuxiliar2;
                }
                else if (txtTipoPessoaRecibo.SelectedValue.ToString() == "PJ")
                {
                    txtNomeRecibo.Text = SessionParticipante.NoInstituicao;
                }
            }
            */
        }
        else
        {
            //txtCPFRecibo.Text = "";
            //txtCNPJRecibo.Text = "";
            //txtNomeRecibo.Text = "";
            //txtUFRecibo.Text = SessionPedido.DsUFRecibo;
            //if (linhaCidade.Visible)
            //    txtCidadeRecibo.Text = SessionPedido.NoCidadeRecibo;
            //txtEnderecoRecibo.Text = SessionPedido.DsEnderecoRecibo;
            //txtComplementoEnderecoRecibo.Text = SessionPedido.DsComplementoEnderRecibo;
            //txtBairroRecibo.Text = SessionPedido.NoBairroRecibo;
            //txtCEPRecibo.Text = SessionPedido.NuCEPRecibo;
            //txtEmailRespFin.Text = SessionPedido.DsEmailResponsavelFinanceiro;
            //txtIE.Text = "";
            //txtPaisRecibo.Text = SessionPedido.NoPaisRecibo;
        }
    }
    protected void btnDadosParticipante_Click(object sender, EventArgs e)
    {
        if (txtCPFRecibo.Text.Replace(".", "").Replace("-", "") == SessionParticipante.NuCPFCNPJ)
        {
            txtCPFRecibo.Text = oClsFuncoes.CPFCNPJMascarar(SessionParticipante.NuCPFCNPJ);
            txtNomeRecibo.Text = SessionParticipante.NoParticipante;
            txtUFRecibo.Text = SessionParticipante.DsUF;
            txtCidadeRecibo.Text = SessionEvento.DsFormaGuardarMunicipio == "dsMunicipio" ? SessionParticipante.NoCidade : Geral.BuscarNomeMunicipio(SessionParticipante.NoCidade, SessionCnn);
            txtEnderecoRecibo.Text = SessionParticipante.DsEndereco;
            txtComplementoEnderecoRecibo.Text = SessionParticipante.DsComplementoEndereco;
            txtBairroRecibo.Text = SessionParticipante.NoBairro;
            txtCEPRecibo.Text = oClsFuncoes.MascaraGerar(SessionParticipante.NuCEP, "99.999-999");
        }
        else
        {
            lblMsg2Recibo.Text = "";
            if (txtCPFRecibo.Text.Replace(".", "").Replace("-", "").Length < 11)
            {
                //lblMsgPgto.Text = "CPF Inválido";
                return;
            }

            string tmpCPF = ValidarCPF(txtCPFRecibo.Text);
            if (tmpCPF != "")
            {
                lblMsg2Recibo.Text = tmpCPF;
                return;

            }


            string tempCPF = txtCPFRecibo.Text.Replace(".", "").Replace("-", "");


            //PESQUISAR CPF BANCO LOCAL
            SqlConnection SessionCnnHISTORICO = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOWJYTnpjV3d1Wm1GNlpXNWtiMjFoYVhNdVkyOXRPMGx1YVhScFlXd2dRMkYwWVd4dlp6MW1ZWHBsYm1SdmJXRnBjekU3VUdWeWMybHpkQ0JUWldOMWNtbDBlU0JKYm1adlBWUnlkV1U3VlhObGNpQkpSRDFtWVhwbGJtUnZiV0ZwY3pFN1VHRnpjM2R2Y21ROVVYVTBOVFJ5Um0wPQ==")));
            DataTable DTCpf = oParticipanteCad.PesquisaCPF(txtCPFRecibo.Text.Replace(".", "").Replace("-", ""), SessionCnnHISTORICO);
            if ((DTCpf != null) && (DTCpf.Rows.Count > 0))
            {

                txtNomeRecibo.Text = DTCpf.DefaultView[0]["Nome"].ToString();

            }
            else
            {
                //PESQUISAR CPF BANCO RECEITA
                int tmpSaldoPesqCPF = Geral.verificarTotalPesquisaCPF(SessionCnn);

                if (tmpSaldoPesqCPF > 0)
                {

                    DataSet ds = new DataSet();
                    ds.ReadXml("http://ws.fontededados.com.br/consulta.asmx/SituacaoCadastralPF?login=" +
                        cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("Wm1GNlpXNWtiMjFoYVhNPQ==")) +
                        "&senha=" + cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("Um0xQWFUVXpORGcy")) +
                        "&cpf=" + txtCPFRecibo.Text.Replace(".", "").Replace("-", ""));

                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["MensagemErro"].ToString() != "")
                            {
                                //    lblMsg.Text = ds.Tables[0].Rows[0]["MensagemErro"].ToString();
                                if (ds.Tables[0].Rows[0]["ValorCobrado"].ToString() != "0")
                                    Geral.DecrementarPesquisaCPFReceita(SessionCnn);

                                if ((ds.Tables[0].Rows[0]["ValorCobrado"].ToString() == "8") ||
                                    (ds.Tables[0].Rows[0]["ValorCobrado"].ToString() == "99"))
                                {
                                    //lblMsg2.Text = ds.Tables[0].Rows[0]["MensagemErro"].ToString();
                                    lblMsg2Recibo.Text = "Não foi possível localizar os dados do CPF informado!<br/>Preencha os campos manualmente.";
                                    lblMsg2Recibo.Visible = true;
                                    return;
                                }
                                lblMsg2Recibo.Text = "Não foi possível localizar os dados do CPF informado!<br/>Preencha os campos manualmente.";
                            }
                            else
                            {
                                oParticipanteCad.IncluirCPF(ds.Tables[0].Rows[0]["Cpf"].ToString(), ds.Tables[0].Rows[0]["Nome"].ToString(), SessionCnnHISTORICO);
                                Geral.DecrementarPesquisaCPFReceita(SessionCnn);

                                txtNomeRecibo.Text = ds.Tables[0].Rows[0]["Nome"].ToString();

                                //Server.Transfer("frmCadastroAuto.aspx?nome=" + ds.Tables[0].Rows[0]["Nome"].ToString() + "&cpf=" + ds.Tables[0].Rows[0]["Cpf"].ToString(), true);
                                //Response.Write("<script>window.open('frmCadastroAuto.aspx?nome=" + ds.Tables[0].Rows[0]["Nome"].ToString() + "&cpf=" + ds.Tables[0].Rows[0]["Cpf"].ToString() + "','_self');</script>"); //lblMsg.Text = ds.Tables[0].Rows[0]["Nome"].ToString();
                            }
                        }
                    }
                }
                else //if (tmpSaldoPesqCPF == 0)
                {
                    lblMsg2Recibo.Text = "Não foi possível localizar os dados do CPF informado!<br/>Preencha os campos manualmente.";
                }
                //else
                //{
                //    lblMsg2.Text = "Não foi possível localizar os dados do CPF informado!<br/>Preencha os campos manualmente.";
                //}
            }
        }
    }

    protected string ValidarCPF(string prmCPF)//, SqlConnection prmCnn)
    {
        if ((!oClsFuncoes.CPFCNPJValidar(prmCPF)))
        {
            return "CPF Inválido!";
        }
        else
        {
            string tmpCPF = prmCPF.Replace(".", "").Replace("-", "");

            if ((tmpCPF == "11111111111") ||
                (tmpCPF == "22222222222") ||
                (tmpCPF == "33333333333") ||
                (tmpCPF == "44444444444") ||
                (tmpCPF == "55555555555") ||
                (tmpCPF == "66666666666") ||
                (tmpCPF == "77777777777") ||
                (tmpCPF == "88888888888") ||
                (tmpCPF == "99999999999") ||
                (tmpCPF == "00000000000"))
            {
                return "CPF Inválido!";
            }
            else
            {
                return "";
            }

        }

    }

    protected void btnDadosInstituicao_Click(object sender, EventArgs e)
    {
        lblMsg2Recibo.Text = "";
        if (txtCNPJRecibo.Text == "")
        {
            lblMsg2Recibo.Text = "Campo obrigatório.";
            return;
        }


        //PESQUISAR CNPJ BANCO LOCAL
        SqlConnection SessionCnnHISTORICO = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOWJYTnpjV3d1Wm1GNlpXNWtiMjFoYVhNdVkyOXRPMGx1YVhScFlXd2dRMkYwWVd4dlp6MW1ZWHBsYm1SdmJXRnBjekU3VUdWeWMybHpkQ0JUWldOMWNtbDBlU0JKYm1adlBWUnlkV1U3VlhObGNpQkpSRDFtWVhwbGJtUnZiV0ZwY3pFN1VHRnpjM2R2Y21ROVVYVTBOVFJ5Um0wPQ==")));
        DataTable DTCNPJ = oParticipanteCad.PesquisaCNPJ(txtCNPJRecibo.Text.Replace(".", "").Replace("-", "").Replace("/", ""), SessionCnnHISTORICO);
        if ((DTCNPJ != null) && (DTCNPJ.Rows.Count > 0))
        {
            txtNomeRecibo.Text = DTCNPJ.DefaultView[0]["NomeEmpresa"].ToString();
            cascaDD_txtUF.SelectedValue = DTCNPJ.DefaultView[0]["UF"].ToString();
            cascaDD_txtMunicipio.SelectedValue = DTCNPJ.DefaultView[0]["Municipio"].ToString();
            txtEnderecoRecibo.Text = DTCNPJ.DefaultView[0]["Logradouro"].ToString();
            txtComplementoEnderecoRecibo.Text = DTCNPJ.DefaultView[0]["Complemento"].ToString();
            txtBairroRecibo.Text = DTCNPJ.DefaultView[0]["Bairro"].ToString();
            txtCEPRecibo.Text = oClsFuncoes.MascaraGerar(DTCNPJ.DefaultView[0]["CEP"].ToString(), "99.999-999");
            txtIE.Text = DTCNPJ.DefaultView[0]["InscricaoEstadual"].ToString();

        }
        else
        {
            //PESQUISAR CNPJ BANCO RECEITA
            int tmpSaldoPesqCPF = Geral.verificarTotalPesquisaCPF(SessionCnn);

            if (tmpSaldoPesqCPF > 0)
            {
                DataSet ds = new DataSet();
                ds.ReadXml("http://ws.fontededados.com.br/consulta.asmx/SituacaoCadastralPJ?login=" +
                    cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("Wm1GNlpXNWtiMjFoYVhNPQ==")) +
                    "&senha=" + cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("Um0xQWFUVXpORGcy")) +
                    "&cnpj=" + txtCNPJRecibo.Text.Replace(".", "").Replace("-", "").Replace("/", ""));

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["MensagemErro"].ToString() != "")
                        {
                            //    lblMsg.Text = ds.Tables[0].Rows[0]["MensagemErro"].ToString();
                            if (ds.Tables[0].Rows[0]["ValorCobrado"].ToString() != "0")
                                Geral.DecrementarPesquisaCPFReceita(SessionCnn);

                            if ((ds.Tables[0].Rows[0]["ValorCobrado"].ToString() == "8") ||
                                (ds.Tables[0].Rows[0]["ValorCobrado"].ToString() == "99"))
                            {
                                //lblMsg2.Text = ds.Tables[0].Rows[0]["MensagemErro"].ToString();
                                lblMsg2Recibo.Text = "Não foi possível localizar os dados do CNPJ informado!<br/>Preencha os campos manualmente.";
                                lblMsg2Recibo.Visible = true;
                                return;
                            }
                            lblMsg2Recibo.Text = "Não foi possível localizar os dados do CNPJ informado!<br/>Preencha os campos manualmente.";
                        }
                        else
                        {
                            oParticipanteCad.IncluirCNPJ(
                                ds.Tables[0].Rows[0]["CNPJ"].ToString().Replace(".", "").Replace("-", "").Replace("/", ""),
                                ds.Tables[0].Rows[0]["NomeEmpresa"].ToString(),
                                ds.Tables[0].Rows[0]["Logradouro"].ToString(),
                                ds.Tables[0].Rows[0]["Complemento"].ToString() + " - " + ds.Tables[0].Rows[0]["Numero"].ToString(),
                                ds.Tables[0].Rows[0]["Bairro"].ToString(),
                                ds.Tables[0].Rows[0]["Municipio"].ToString(),
                                ds.Tables[0].Rows[0]["CEP"].ToString().Replace(".", "").Replace("-", ""),
                                ds.Tables[0].Rows[0]["UF"].ToString(),
                                "",
                                SessionCnnHISTORICO);
                            Geral.DecrementarPesquisaCPFReceita(SessionCnn);

                            txtNomeRecibo.Text = ds.Tables[0].Rows[0]["NomeEmpresa"].ToString();

                            cascaDD_txtUF.SelectedValue = ds.Tables[0].Rows[0]["UF"].ToString();
                            cascaDD_txtMunicipio.SelectedValue = ds.Tables[0].Rows[0]["Municipio"].ToString();
                            txtEnderecoRecibo.Text = ds.Tables[0].Rows[0]["Logradouro"].ToString();
                            txtComplementoEnderecoRecibo.Text = ds.Tables[0].Rows[0]["Complemento"].ToString() + " - " + ds.Tables[0].Rows[0]["Numero"].ToString();
                            txtBairroRecibo.Text = ds.Tables[0].Rows[0]["Bairro"].ToString();
                            txtCEPRecibo.Text = ds.Tables[0].Rows[0]["CEP"].ToString();


                            //Server.Transfer("frmCadastroAuto.aspx?nome=" + ds.Tables[0].Rows[0]["Nome"].ToString() + "&cpf=" + ds.Tables[0].Rows[0]["Cpf"].ToString(), true);
                            //Response.Write("<script>window.open('frmCadastroAuto.aspx?nome=" + ds.Tables[0].Rows[0]["Nome"].ToString() + "&cpf=" + ds.Tables[0].Rows[0]["Cpf"].ToString() + "','_self');</script>"); //lblMsg.Text = ds.Tables[0].Rows[0]["Nome"].ToString();
                        }
                    }
                }
                else
                {
                    lblMsg2Recibo.Text = "Não foi possível localizar os dados do cnpj informado!<br/>Preencha os campos manualmente.";
                    return;
                }
            }
            else
            {
                lblMsg2Recibo.Text = "Não foi possível localizar os dados do cnpj informado!<br/>Preencha os campos manualmente.";
            }
        }
    }
    protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
    {

    }

    public void ListarPaises(DropDownList prmCampoListagem)
    {
        lblMsg.Visible = false;
        if (SessionCnn == null)
        {
            //_erroNoCampo = true;
            lblMsg.Visible = true;
            lblMsg.Text = "Conexão inválida ou inexistente";
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
                lblMsg.Visible = true;
                lblMsg.Text = "Conexão inválida";
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
                lblMsg.Visible = true;
                lblMsg.Text = "Erro ao selecionar Países!\n" + Ex.Message;
                SessionCnn.Close();
            }
        }
        finally
        {
            SessionCnn.Close();
        }


    }
   
}
