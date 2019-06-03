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


public partial class frmSelAtividades : System.Web.UI.Page//BaseWebUi //
{
    Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    SqlConnection SessionCnn;

    Pedido SessionPedido;
    PedidoCad oPedidoCad = new PedidoCad();

    Categoria SessionCategoria;

    String SessionAtv;

    String SessionTipoAcesso;

    Inscricoes SessionInscricoes = new Inscricoes();

    //ClsCongresso clOperador;

    DataTable oDTAtividades;// = new DataTable();
    DataTable oDTAtividadesParticipante = new DataTable();

    string cdAtiv = "";
    string varFiltroChoqueHorario = "";

    String SessionIdioma;
    private String NoMoeda;
    private Decimal VlConversao;

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

            btnAvancarAtividades.Attributes.Add("onclick", "document.body.style.cursor = 'wait'; this.value='"+tmpMsgAguarde+"'; this.disabled = true; " + ClientScript.GetPostBackEventReference(btnAvancarAtividades, string.Empty) + ";");
            btnAvancar2.Attributes.Add("onclick", "document.body.style.cursor = 'wait'; this.value='" + tmpMsgAguarde + "'; this.disabled = true; " + ClientScript.GetPostBackEventReference(btnAvancar2, string.Empty) + ";");


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

            SessionAtv = (String)Session["SessionAtv"];
            if (SessionAtv == null)
                SessionAtv = "";
            Session["SessionAtv"] = SessionAtv;



            if ((Request["tpAcesso"] != null) &&
                    (Request["tpAcesso"].ToString().Trim().ToUpper() != ""))
            {
                SessionTipoAcesso = Request["tpAcesso"];
            }
            else
            {
                SessionTipoAcesso = (String)Session["tpAcesso"];
                if (SessionTipoAcesso == null)
                    SessionTipoAcesso = "NRM";
            }
            Session["tpAcesso"] = SessionTipoAcesso;



            if ((SessionEvento.DtFechamentoInscrWeb == null) ||
                (SessionEvento.DtFechamentoInscrWeb < Geral.datahoraServidor(SessionCnn)))//DateTime.Parse(DateTime.Today.ToString("dd/MM/yyyy"))))
            {

                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                "005",
                                oEventoCad.RcMsg + SessionEvento.CdEvento.Substring(0, 4)), true);
                
            }


            NoMoeda = "BRL";
            VlConversao = 1;

            if ((SessionParticipante.NoPais != "BRASIL") && (SessionParticipante.NoPais != ""))
            {
                NoMoeda = "USD";
                VlConversao = (Geral.BuscarMoedaValorConversao(SessionEvento.CdEvento, NoMoeda, SessionCnn));

                if (VlConversao < 0)
                {//erro
                    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                "043",
                                ""), true);
                }
                if (VlConversao == 0)
                {//não cadastrado
                    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                "044",
                                ""), true);
                }
            }

            if (SessionEvento.CdEvento == "007002")
            {
                NoMoeda = "BRL";
                VlConversao = 1;

                if (!SessionParticipante.Categoria.NoCategoria.Contains("BRAZILIAN"))
                {
                    NoMoeda = "USD";
                    VlConversao = (Geral.BuscarMoedaValorConversao(SessionEvento.CdEvento, NoMoeda, SessionCnn));

                    if (VlConversao < 0)
                    {//erro
                        Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                    "043",
                                    ""), true);
                    }
                    if (VlConversao == 0)
                    {//não cadastrado
                        Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                    "044",
                                    ""), true);
                    }
                }
            }

            Session["NoMoeda"] = NoMoeda;
            Session["VlConversao"] = VlConversao;

            grdAtv.Focus();


            if ((SessionParticipante.Categoria != null) && (SessionParticipante.Categoria.FlQuestionario))
            {
                if ((SessionTipoAcesso == null) || (SessionTipoAcesso == "NRM"))
                {
                    PesquisasDeOpiniao oPesquisasDeOpiniao = new PesquisasDeOpiniao();
                    DataTable dtPesquisas = oPesquisasDeOpiniao.ListarPesquisasDoParticipanteWeb(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionParticipante.DsIdioma, SessionCnn);

                    if ((dtPesquisas != null) && (dtPesquisas.Rows.Count > 0))
                    {
                        Session["oDTPesquisa"] = null;
                        Session["SessionFormulario"] = "frmPesquisaVinculada";
                        Response.Write("<script>window.open('frmPesquisaVinculada.aspx','_self');</script>");
                    }
                }
            }

            if (SessionEvento.CdCliente == "0013")//Conasems
            {
                lblDtInicioFiltro.Visible = false;
                txtFDtInicio.Visible = false;
                if ((SessionEvento.CdEvento == "001304") || (SessionEvento.CdEvento == "001305"))
                {
                    PesquisasDeOpiniao oPesquisasDeOpiniao = new PesquisasDeOpiniao();
                    DataTable dtPesquisas = oPesquisasDeOpiniao.ListarPesquisasDoParticipante(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionParticipante.DsIdioma, SessionCnn);

                    if ((dtPesquisas != null) && (dtPesquisas.Rows.Count > 0))
                    {
                        Session["oDTPesquisa"] = null;
                        Response.Write("<script>window.open('frmPesquisaVinculada.aspx','_self');</script>");
                    }

                    if ((SessionParticipante.NoAreaAtuacao == "SIM") && (SessionParticipante.DsAuxiliar19 != "SIM"))
                    {//SECRETÁRIOS MUNICIPAIS DE SAÚDE DEVEM RESPONDER A DECLARAÇÃO
                        Response.Write("<script>window.open('frmTermoConasemsSecretarios.aspx','_self');</script>");
                        return;
                    }

                    if (SessionEvento.CdEvento == "001304")
                    {
                        lblDtInicioFiltro.Visible = true;
                        txtFDtInicio.Visible = true;
                    }
                }

                if (SessionEvento.CdEvento == "001303")
                {                    
                    lblDtInicioFiltro.Visible = true;
                    txtFDtInicio.Visible = true;
                }
                
            }

            if ((SessionEvento.CdCliente == "0003") || (SessionEvento.CdEvento == "003401"))//Consad
            {
                lblFiltro.Text = "Consulte a relação de painéis selecionados e indique de 5 a 10 que despertou interesse em participar";

                //lblFiltro.Visible = false;

                lblDtInicioFiltro.Visible = false;
                txtFDtInicio.Visible = false;

                lblTipoFiltro.Visible = false;
                TxtFTipo.Visible = false;
            }

            if ((!SessionEvento.FlEventoComRecebimentos) || (SessionEvento.CdEvento == "002903"))
            {
                //lblFiltro.Text = "Selecione o Eixo Temático que deseja participar";

                lblResVlr.Visible = false;
                lblResDesc.Visible = false;
                lblResVlrTotal.Visible = false;

                vlTotalAtiv.Visible = false;
                vlTotalDesc.Visible = false;
                vlTotalPedido.Visible = false;
            }

            if (SessionEvento.CdEvento == "003401")
            {
                lblFiltro.Text = "Selecione o Eixo Temático que deseja participar";

                lblResVlr.Visible = false;
                lblResDesc.Visible = false;
                lblResVlrTotal.Visible = false;

                vlTotalAtiv.Visible = false;
                vlTotalDesc.Visible = false;
                vlTotalPedido.Visible = false;
            }

            if ((SessionEvento.CdEvento == "001603") &&
                ((SessionParticipante != null) && (SessionParticipante.CdCategoria == "00160301")))
            {
                lblResVlr.Visible = false;
                lblResDesc.Visible = false;
                lblResVlrTotal.Visible = false;

                vlTotalAtiv.Visible = false;
                vlTotalDesc.Visible = false;
                vlTotalPedido.Visible = false;
            }

            lblIdentificador.Text = SessionParticipante.CdParticipante.Trim();
            lblNoParticipante.Text = SessionParticipante.NoParticipante.Trim();

            CategoriaCad oCategoriaCad = new CategoriaCad();
            SessionCategoria = oCategoriaCad.Pesquisar(SessionEvento.CdEvento, SessionParticipante.CdCategoria, SessionCnn);

            if (!SessionCategoria.FlAtividades)
            {
                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                "008",
                                oEventoCad.RcMsg), true);
            }

            Session["SessionCategoria"] = SessionCategoria;
            if (SessionIdioma == "PTBR")
            {
                lblCategoria.Text = SessionCategoria.NoCategoria.Trim();
                if (SessionEvento.CdEvento == "005503")
                    lblCategoria.Text += " / " + SessionParticipante.NoInstituicao;
            }
            else if (SessionIdioma == "ENUS")
                lblCategoria.Text = SessionCategoria.NoCategoriaIngles.Trim();
            else if (SessionIdioma == "ESP")
                lblCategoria.Text = SessionCategoria.NoCategoriaEspanhol.Trim();
            else if (SessionIdioma == "FRA")
                lblCategoria.Text = SessionCategoria.NoCategoriaFrances.Trim();


            preencherMsgExtra(SessionEvento.CdEvento, SessionParticipante.CdCategoria);


            DataTable DTAtiv = SessionInscricoes.ListarAtividadesDisponiveis(SessionParticipante, null, "inscrweb", SessionCnn);

            if ((SessionEvento.CdEvento == "007701") && (SessionIdioma == "PTBR"))
            {
                if (SessionAtv != "")
                    DTAtiv.DefaultView.RowFilter = " cdAtividade = '007701001'";
                else if (SessionTipoAcesso == "NOVA")
                    DTAtiv.DefaultView.RowFilter = " cdAtividade <> '007701001'";
            }

            if ((DTAtiv != null) && (DTAtiv.Rows.Count >= 1))
            {
                if ((DTAtiv.DefaultView.Count == 1) && (SessionEvento.CdEvento != "005102"))
                {//evento com apenas uma atividade

                    string cdAtividade = DTAtiv.DefaultView[0]["cdAtividade"].ToString().Trim();
                    decimal vlAtv = decimal.Parse(DTAtiv.DefaultView[0]["vlAtividade"].ToString().Trim());
                    decimal vlDesc = decimal.Parse(DTAtiv.DefaultView[0]["vlDescontoReal"].ToString().Trim());
                    decimal vlTotInscri = decimal.Parse(DTAtiv.DefaultView[0]["vlTotInscri"].ToString().Trim());
                    
                    string tpPagto = "";

                    if ((SessionEvento.CdEvento == "010801") && (cdAtividade == "010801001"))
                    {
                        if (SessionIdioma != "PTBR")
                        {
                            //noTitulo = Geral.atividadeVeloCity(DTAtividades.DefaultView[i]["cdAtividade"].ToString(), SessionIdioma, "TITULO", "", "", "", "", null);
                            //noLocal = Geral.atividadeVeloCity(DTAtividades.DefaultView[i]["cdAtividade"].ToString(), SessionIdioma, "LOCAL", "", "", "", "", null);
                            vlAtv = vlTotInscri = decimal.Parse(Geral.atividadeVeloCity(cdAtividade, SessionIdioma, "VALOR", SessionParticipante.NoPais, SessionParticipante.CdEvento, "USD", SessionParticipante.CdCategoria, SessionCnn));
                        }
                        else
                        {
                            vlAtv = vlTotInscri = decimal.Parse(Geral.atividadeVeloCity(cdAtividade, SessionIdioma, "VALOR", SessionParticipante.NoPais, SessionParticipante.CdEvento, "USD", SessionParticipante.CdCategoria, SessionCnn));
                        }
                    }

                    if (SessionEvento.CdCliente == "0003")//consad
                    {
                        if (SessionParticipante.DsAuxiliar2.Trim().ToUpper() == "EMPENHO")
                        {
                            vlAtv = 300;
                            vlDesc = 100;
                            vlTotInscri = 200;
                        }
                    }

                    if (((SessionEvento.CdEvento == "007701") &&
                         (SessionParticipante.NoPais != "") && (SessionParticipante.NoPais != "BRASIL")) ||
                        ((SessionEvento.CdEvento == "007701") && (SessionIdioma != "PTBR")))
                    {


                        //if (SessionParticipante.DsAuxiliar7.ToUpper() == "YES")
                        //    vlAtv = 750;
                        //else
                        //    vlAtv = Geral.verficarPaisEmergente(SessionParticipante.NoPais, DTAtiv.DefaultView[0]["cdAtividade"].ToString().Trim(), SessionCnn);

                        decimal vlDescTemp = Geral.verficarPaisEmergente(SessionParticipante.NoPais, DTAtiv.DefaultView[0]["cdAtividade"].ToString().Trim(), SessionCnn);


                        vlAtv = 1250;
                        vlDesc = vlDescTemp != 0 ? vlDescTemp > vlDesc ? vlDescTemp : vlDesc : vlDesc;
                        //vlDesc = vlDescTemp != 0 ? vlDescTemp : vlDesc;
                        vlTotInscri = vlAtv - vlDesc;
                    }

                    if (SessionEvento.CdEvento == "007001")//biominas
                    {
                        vlAtv = Geral.calcValorBiominas(SessionParticipante, DTAtiv.DefaultView[0]["cdAtividade"].ToString().Trim(), "VALOR", SessionCnn);
                        vlDesc = Geral.calcValorBiominas(SessionParticipante, DTAtiv.DefaultView[0]["cdAtividade"].ToString().Trim(), "DESCONTO", SessionCnn);
                        vlTotInscri = vlAtv - vlDesc;
                    }

                    if ((SessionEvento.CdEvento == "004401") //cispod
                        && (SessionIdioma != "PTBR"))
                    {
                        
                        vlAtv = 500;
                        vlDesc = 0;
                        vlTotInscri = 500;
                        tpPagto = "LOCAL";
                    }

                    DateTime? dtValidade = null;
                    if (oClsFuncoes.DataValidar(DTAtiv.DefaultView[0]["dtValidade"].ToString()))
                        dtValidade = DateTime.Parse(DTAtiv.DefaultView[0]["dtValidade"].ToString());
                    if (dtValidade == null)
                    {
                        dtValidade = DateTime.Today.AddDays(10);

                        //if (dtValidade > SessionEvento.DtFechamentoInscrWeb)
                        //    dtValidade = DateTime.Parse(SessionEvento.DtFechamentoInscrWeb.ToString());
                    }

                    DateTime dtMenorVencDesconto = DescontoCad.datahoraMenorVencimentoDesconto(SessionEvento.CdEvento, SessionCnn);
                    if ((dtMenorVencDesconto != null) && (dtValidade > dtMenorVencDesconto))
                        dtValidade = dtMenorVencDesconto;

                    if (dtValidade > SessionEvento.DtFechamentoInscrWeb)
                        dtValidade = DateTime.Parse(SessionEvento.DtFechamentoInscrWeb.ToString());

                    // SessionCategoria.NoCategoria;
                    if (!SessionEvento.FlEventoComRecebimentos)
                    {//evento sem recebimentos

                        vlAtv = 0;
                        vlDesc = 0;
                        vlTotInscri = 0;
                        tpPagto = "";
                    }
                    if ((SessionCategoria.FlConfirmacaoCadWeb) || ((SessionEvento.FlEventoComRecebimentos) && (vlTotInscri > 0)))
                    {//evento com confimacao na organização do evento ou com recebimento
                        #region Evento pago ou com confirmação pela organização

                        #region verifica se já consta um pedido em aberto
                        
                        
                        SessionPedido = oPedidoCad.VerificaPedidoNaoPagoWeb(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);
                        if ((SessionPedido != null) && (SessionPedido.TpPagamento.Trim() != "") && (!SessionEvento.FlPermitirEditarPedido))
                        {
                            if ((!SessionEvento.FlEventoComRecebimentos) ||//evento sem recebimento
                                (!SessionCategoria.FlPagamento) //categoria não requer pagamento
                               )
                            {
                                if (SessionCategoria.FlConfirmacaoCadWeb)
                                {
                                    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                                    "009",
                                                    ""), true);
                                }
                                else
                                {
                                    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                                    "002",
                                                    ""), true);
                                }
                            }
                            else
                            {
                                //if (SessionPedido.TpPagamento != "")
                                //{
                                //    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                //                    "015",
                                //                    ""), true);
                                //}
                                //else
                                //{
                                //    Session["SessionPedido"] = SessionPedido;
                                //    Server.Transfer(string.Format("frm_formapagamento.aspx?cdMatricula={0}",
                                //       SessionParticipante.CdParticipante), true);
                                //}
                            }
                        }
                        #endregion
                        else
                        if ((SessionPedido == null) || (SessionPedido.TpPagamento.Trim() == "") || (SessionEvento.FlPermitirEditarPedido))
                        {
                            if (SessionInscricoes.verificarVagas(SessionEvento.CdEvento, cdAtividade, SessionCnn) <= 0)
                            {
                                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                               "011",
                                               ""), true);
                            }

                            if (!SessionEvento.FlMostrarAtivQdoUmItem)
                            {
                                //gerar pedido de matricula
                                if (SessionPedido == null)
                                    SessionPedido = new Pedido(SessionEvento.CdEvento, SessionParticipante.CdParticipante, "", "0", null, vlTotInscri, false, true, tpPagto, "", "", "", "", "", "", "", "", dtValidade, 1, "","","","","","","","",NoMoeda,VlConversao,SessionParticipante.NoPais,0,0,0);
                                else
                                {
                                    SessionPedido.VlTotalPedido = vlTotInscri;
                                    SessionPedido.TpPagamento = tpPagto;
                                    SessionPedido.DtVencimentoPedido = dtValidade;
                                    SessionPedido.NoMoeda = NoMoeda;
                                    SessionPedido.VlConversao = VlConversao;
                                }

                                SessionPedido = oPedidoCad.Gravar(SessionPedido, SessionCnn);
                                if (SessionPedido == null)
                                {
                                    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                                   "012",
                                                   ""), true);
                                    return;
                                }
                                oPedidoCad.GravarAtividadePedido(
                                                   SessionPedido.CdPedido,
                                                   cdAtividade,
                                                   vlAtv,
                                                   vlDesc,
                                                   1,
                                                   SessionCnn);
                                if ((!SessionEvento.FlEventoComRecebimentos) ||//evento sem recebimento
                                    (!SessionCategoria.FlPagamento) //categoria não requer pagamento
                                   )
                                {
                                    if (SessionCategoria.FlConfirmacaoCadWeb)
                                    {
                                        //enviar email
                                        if (SessionEvento.FlEnviarMensagensEmail)
                                        {
                                            Geral oGeral = new Geral();

                                            oGeral.EnviarEmailConfirmaCadastroConf(SessionEvento, SessionParticipante, SessionPedido, SessionCnn);
                                        }
                                        //-----
                                        if (SessionEvento.CdEvento == "004802")
                                        {
                                            Response.Write("<script>window.open('frmEnviarDocumento2.aspx','_self');</script>");
                                            return;
                                        }
                                        else
                                            Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                                    "013",
                                                    ""), true);
                                    }
                                }
                                else
                                {
                                    Session["SessionPedido"] = SessionPedido;
                                    //Server.Transfer(string.Format("frm_formapagamento.aspx?cdMatricula={0}", SessionParticipante.CdParticipante), true);

                                    if (((SessionEvento.FlEmiteRecibo)))// && (SessionIdioma == "PTBR")) ||
                                        //((SessionEvento.FlEmiteRecibo) && (SessionParticipante.Categoria.NoCategoria.Contains("BRAZILIAN"))))
                                        Server.Transfer(string.Format("frmDadosReciboNF.aspx?cdMatricula={0}", SessionParticipante.CdParticipante), true);
                                    else
                                        Server.Transfer(string.Format("frm_formapagamento.aspx?cdMatricula={0}", SessionParticipante.CdParticipante), true);
                                }
                            }
                        }
                        else if (!SessionEvento.FlMostrarAtivQdoUmItem)
                        {
                            Session["SessionPedido"] = SessionPedido;
                            //Server.Transfer(string.Format("frm_formapagamento.aspx?cdMatricula={0}", SessionParticipante.CdParticipante), true);

                            if (((SessionEvento.FlEmiteRecibo)))// && (SessionIdioma == "PTBR")) ||
                                //((SessionEvento.FlEmiteRecibo) && (SessionParticipante.Categoria.NoCategoria.Contains("BRAZILIAN"))))
                                Server.Transfer(string.Format("frmDadosReciboNF.aspx?cdMatricula={0}", SessionParticipante.CdParticipante), true);
                            else
                                Server.Transfer(string.Format("frm_formapagamento.aspx?cdMatricula={0}", SessionParticipante.CdParticipante), true);
                        }
                        #endregion
                    }
                    else
                    {//evento sem recebimentos ou com o valor igual a zero
                        #region evento sem recebimentos ou com o valor igual a zero
                        
                        
                        DataTable DTVerificaMatricla = SessionInscricoes.ListarAtividadesDoParticipante(SessionParticipante, cdAtividade, SessionCnn);
                        if ((DTVerificaMatricla != null) && (DTVerificaMatricla.Rows.Count > 0))
                        {
                            Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                            "010",
                                            ""), true);
                            //Server.Transfer(string.Format("frmCadastroAuto.aspx?cdMatricula={0}",
                            //              SessionParticipante.CdParticipante), true);
                        }

                        if (SessionInscricoes.verificarVagas(SessionEvento.CdEvento, cdAtividade, SessionCnn) <= 0)
                        {
                            Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                           "011",
                                           ""), true);
                        }
                        
                        if (!SessionEvento.FlMostrarAtivQdoUmItem)
                        {
                            //gerar matricula

                            if (SessionInscricoes.MatriculasGravar(SessionParticipante.CdEvento,
                                SessionParticipante.CdParticipante, cdAtividade, 0, 1, "000000001", SessionCnn))
                            {


                                //enviar email
                                if (SessionEvento.FlEnviarMensagensEmail)
                                {
                                    Geral oGeral = new Geral();

                                    oGeral.EnviarEmailConfirmaCadastro(SessionEvento, SessionParticipante, SessionCnn);
                                }
                                //-----
                                if (SessionEvento.CdEvento == "004802")
                                {
                                    Response.Write("<script>window.open('frmEnviarDocumento2.aspx','_self');</script>");
                                    return;
                                }
                                else
                                    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                        "002",
                                        ""), true);
                            }
                            else
                            {
                                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                    "012",
                                    ""), true);
                                return;
                            }
                        }

                        #endregion
                    }
                }
            }
            else
            {//evento de simples cadastro
                if (SessionEvento.DsLinkRedirecionamento.Trim() == "")
                {
                    if (SessionEvento.CdEvento == "010101")
                    {
                        Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                                "002",
                                                ""), true);
                    }

                    DataTable DTAtivMatricula = SessionInscricoes.ListarAtividadesDoParticipante(SessionParticipante, SessionCnn);
                    if (DTAtivMatricula.Rows.Count <= 0)
                    {
                        Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                                "020",
                                                ""), true);
                    }
                    else
                    {
                        Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                                "002",
                                                ""), true);
                    }
                }
                else
                    Response.Redirect(SessionEvento.DsLinkRedirecionamento.Trim());
                    //Server.Transfer(string.Format(SessionEvento.DsLinkRedirecionamento.Trim()), true);


            }



            lblNrPedido.Visible = lblResPed.Visible = /*pnlResumo.Visible = */SessionEvento.FlEventoComRecebimentos;
            
            if  ((SessionCategoria.FlConfirmacaoCadWeb) || (SessionEvento.FlEventoComRecebimentos))
            {
                //lblNrPedido.Text = "";
                //if ((Request["cdPedido"] != null) &&
                //    (Request["cdPedido"] != ""))
                //{
                //    lblNrPedido.Text = Request["cdPedido"].ToString();
                //}
                //if (lblNrPedido.Text == "")
                    SessionPedido = oPedidoCad.VerificaPedidoNaoPagoWeb(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);
                //else
                //    SessionPedido = oPedidoCad.Pesquisar(SessionEvento.CdEvento, SessionParticipante.CdParticipante, lblNrPedido.Text, SessionCnn);

                    if (SessionPedido != null)
                    {
                        if (!SessionEvento.FlPermitirEditarPedido)
                        {
                            if ((SessionPedido.TpPagamento.Trim() == "") && (SessionEvento.CdEvento != "003401"))
                                lblNrPedido.Text = SessionPedido.CdPedido;
                            else
                                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                                "015",
                                                ""), true);
                        }
                        else
                        {
                            lblNrPedido.Text = SessionPedido.CdPedido;
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('\"A  T  E  N  Ç  Ã  O\"\\n\\nConsta processo de inscrição em aberto!\\n\\nSó altere este pedido de inscrição se o mesmo ainda não foi pago.\\n\\nCaso já tenha efetuado o pagamento deste pedido de inscrição, espere o processamento e baixa do mesmo para efetuar um novo pedido ou entre em contato com a organização do evento para maiores informações.'); </script>", false);
                        }


                    }
                    else
                    {
                        
                    }

                
            }
            else
            {
                
            }

            CarregarTipoDeAtividades();
            //FiltrarTemaDeCursos("");
            CarregarDtInicioDeAtividades("");

            oDTAtividades = new DataTable();

            oDTAtividades.Columns.Add("noTipoAtividade");//0
            oDTAtividades.Columns.Add("noTitulo");//1
            oDTAtividades.Columns.Add("dsTema");//2
            oDTAtividades.Columns.Add("noLocal");//3
            oDTAtividades.Columns.Add("dtIni", System.Type.GetType("System.DateTime"));//4
            oDTAtividades.Columns.Add("dtTermino", System.Type.GetType("System.DateTime"));//5
            oDTAtividades.Columns.Add("vagas_em_aberto");//6
            oDTAtividades.Columns.Add("cdAtividade");//7
            oDTAtividades.Columns.Add("noProfessor");//8
            oDTAtividades.Columns.Add("vlCapacidade");//9
            oDTAtividades.Columns.Add("cdTipoAtividade");//10
            oDTAtividades.Columns.Add("vlAtividade", System.Type.GetType("System.Decimal"));//11
            oDTAtividades.Columns.Add("vlDescontoReal", System.Type.GetType("System.Decimal"));//12
            oDTAtividades.Columns.Add("vlTotInscri", System.Type.GetType("System.Decimal"));//13
            oDTAtividades.Columns.Add("flInscricaoObrigatoria", System.Type.GetType("System.Boolean"));//14
            oDTAtividades.Columns.Add("dtValidade");//15
            oDTAtividades.Columns.Add("flPodeChocarHorario", System.Type.GetType("System.Boolean"));//16
            oDTAtividades.Columns.Add("dsCaminhoImgWEB");//17
            oDTAtividades.Columns.Add("vlQuantidade");//18
            oDTAtividades.Columns.Add("flRequerQuantidade", System.Type.GetType("System.Boolean"));//19
            oDTAtividades.Columns.Add("nrLinha");//20
            oDTAtividades.Columns.Add("vlQuantidadeMaxima");//21
            oDTAtividades.Columns.Add("flPodeRepetirPedido");//22
            oDTAtividades.Columns.Add("dsTurno");//23


            oDTAtividadesParticipante.Columns.Add("noTipoAtividade");//0
            oDTAtividadesParticipante.Columns.Add("noTitulo");//1
            oDTAtividadesParticipante.Columns.Add("dsTema");//2
            oDTAtividadesParticipante.Columns.Add("noLocal");//3
            oDTAtividadesParticipante.Columns.Add("dtIni", System.Type.GetType("System.DateTime"));//4
            oDTAtividadesParticipante.Columns.Add("dtTermino", System.Type.GetType("System.DateTime"));//5
            oDTAtividadesParticipante.Columns.Add("cdAtividade");//6
            oDTAtividadesParticipante.Columns.Add("noProfessor");//7
            oDTAtividadesParticipante.Columns.Add("cdTipoAtividade");//8
            oDTAtividadesParticipante.Columns.Add("vlAtividade", System.Type.GetType("System.Decimal"));//9
            oDTAtividadesParticipante.Columns.Add("flAtivo", System.Type.GetType("System.Boolean"));//10
            oDTAtividadesParticipante.Columns.Add("flUsado", System.Type.GetType("System.Boolean"));//11
            oDTAtividadesParticipante.Columns.Add("dtMatricula", System.Type.GetType("System.DateTime"));//12
            oDTAtividadesParticipante.Columns.Add("vlDesconto", System.Type.GetType("System.Decimal"));//13
            oDTAtividadesParticipante.Columns.Add("vlMatricula", System.Type.GetType("System.Decimal"));//14
            oDTAtividadesParticipante.Columns.Add("flInscricaoObrigatoria", System.Type.GetType("System.Boolean"));//15
            oDTAtividadesParticipante.Columns.Add("dtValidade");//16
            oDTAtividadesParticipante.Columns.Add("flPodeChocarHorario", System.Type.GetType("System.Boolean"));//17
            oDTAtividadesParticipante.Columns.Add("dsCaminhoImgWEB");//18
            oDTAtividadesParticipante.Columns.Add("vlQuantidade");//19
            oDTAtividadesParticipante.Columns.Add("flRequerQuantidade", System.Type.GetType("System.Boolean"));//20
            oDTAtividadesParticipante.Columns.Add("nrLinha");//21
            oDTAtividadesParticipante.Columns.Add("vlQuantidadeMaxima");//22
            oDTAtividadesParticipante.Columns.Add("flPodeRepetirPedido");//23
            oDTAtividadesParticipante.Columns.Add("dsTurno");//24

            Session["cdAtiv"] = "";

            CarregarAtividadesGrade();
            Session["oDTAtividades"] = oDTAtividades;

            CarregarAtividadesParticipanteGrade();
            Session["oDTAtividadesParticipante"] = oDTAtividadesParticipante;

            Session["varFiltroChoqueHorario"] = varFiltroChoqueHorario;
            
            if (grdAtv.Rows.Count <= 0)
            {
                //btnAvancar.Visible = true;
                //btnVerItensPedido.Text = "Continuar Comprando";
                //btnVerItensPedido.Visible = false;
                //grdAtv.Visible = false;
                //grdAtvParticipante.Visible = true;
                //btnVoltarParaItens.Visible = true;
            }


            

        }
        else
        {
            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            SessionCategoria = (Categoria)Session["SessionCategoria"];

            //SessionPedido = (Pedido)Session["SessionPedido"];

            oDTAtividades = (DataTable)Session["oDTAtividades"];

            oDTAtividadesParticipante = (DataTable)Session["oDTAtividadesParticipante"];

            cdAtiv = (string)Session["cdAtiv"];

            varFiltroChoqueHorario = (string)Session["varFiltroChoqueHorario"];

            SessionIdioma = (String)Session["SessionIdioma"];

            SessionAtv = (String)Session["SessionAtv"];

            SessionTipoAcesso = (String)Session["tpAcesso"];

            NoMoeda = (String)Session["NoMoeda"];
            VlConversao = (Decimal)Session["VlConversao"];

        }

        if (SessionEvento == null)
            Server.Transfer("frmSessaoExpirada.aspx", true);

        verificarIdioma(SessionIdioma);


        if (SessionEvento.CdEvento == "006501")
        {
            lblTituloPagina.Text = "Seleção de Grupos";
            lblTipoFiltro.Text = "Macrogrupos";
            lblTituloGrid1.Text = "Microgrupos";

            MsgExtra.Visible = true;

        }

        TSManager1.RegisterPostBackControl(btnAvancarAtividades);
        TSManager1.RegisterPostBackControl(btnAvancar2);
    }

    protected void verificarIdioma(string prmIdioma)
    {
        if (prmIdioma == "PTBR")
        {
            SessionIdioma = "PTBR";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Pedido de Inscrição";
            lblTituloResumo.Text = "Resumo do Pedido";

            lblID.Text = "Nr Particpante:";
            lblID.Visible = false;
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

            btnAvancarAtividades.Text = "Continuar";
            btnAvancar2.Text = "Continuar";
            //btnVerItensPedido.Text = "Continuar Inscrição";
            //btnVoltarParaItens.Text = "Voltar";

            lblFiltro.Text = "Filtro";
            if (SessionEvento.CdCliente == "0003") //Consad
            {
                lblFiltro.Text = "Consulte a relação de painéis selecionados e indique de 5 a 10 que despertou interesse em participar";
            }

            if (SessionEvento.CdEvento == "003401")
            {
                lblTituloPagina.Text = "Pré-inscrição no Eixo Temático";
                //lblTituloResumo.Visible = false;

                lblFiltro.Text = "Selecione o Eixo Temático que deseja participar<br /><br />" +
                    "<font color='red'>A confirmação do pedido de inscrição no eixo temático observará as vagas disponíveis, quantidade de pessoas de um mesmo Órgão/unidade por eixo e proporcionalidade entre cargos.</font>";
            }

            lblTipoFiltro.Text = "Tipo ";
            lblDtInicioFiltro.Text = "Período";// "Dt Início ";


            lblTituloGrid1.Text = "Itens Disponíveis";

            if (SessionEvento.CdEvento == "005503")
                lblTituloGrid1.Text = "Escolha o colégio da NTU que você irá Participar";

        }
        else if (prmIdioma == "ENUS")
        {
            SessionIdioma = "ENUS";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Enrollment request";
            lblTituloResumo.Text = "Order summary";

            lblID.Text = "Registration no.:";

            lblPart.Text = "Participant:";
            lblCateg.Text = "Category:";

            lblResPed.Text = "Order No.";
            lblResItens.Text = "Items";
            lblResVlr.Text = "Rate";
            lblResDesc.Text = "Discount";
            lblResVlrTotal.Text = "Total ($)";
            //if ((SessionParticipante.NoPais == "BRASIL") ||
            //    (SessionParticipante.NoPais == "BRAZIL") ||
            //    (SessionParticipante.NoPais == "BRASIL") ||
            //    (SessionParticipante.NoPais == "BRÉSIL"))
            //    lblResVlrTotal.Text = "Total (R$)";
            //else
            //    lblResVlrTotal.Text = "Total ($)";

            btnAvancarAtividades.Text = "Finish";
            btnAvancar2.Text = "Finish";
            //btnVerItensPedido.Text = "Continue Registration";
            //btnVoltarParaItens.Text = "Go Back";

            lblFiltro.Text = "Filter";
            lblTipoFiltro.Text = "type";
            lblDtInicioFiltro.Text = "Start";

            lblTituloGrid1.Text = "items Available";
        }
        else if (prmIdioma == "ESP")
        {
            SessionIdioma = "ESP";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Pedido de Registro";
            lblTituloResumo.Text = "Resúmen del pedido";

            lblID.Text = "Nº de Registro:";
            lblPart.Text = "Participante:";
            lblCateg.Text = "Categoría";

            lblResPed.Text = "Solicitud Nº";
            lblResItens.Text = "Artículos";
            lblResVlr.Text = "Valor";
            lblResDesc.Text = "Descuento";
            lblResVlrTotal.Text = "Total ($)";
            if ((SessionParticipante.NoPais == "BRASIL") ||
                (SessionParticipante.NoPais == "BRAZIL") ||
                (SessionParticipante.NoPais == "BRASIL") ||
                (SessionParticipante.NoPais == "BRÉSIL"))
                lblResVlrTotal.Text = "Total (R$)";
            else
                lblResVlrTotal.Text = "Total ($)";

            btnAvancarAtividades.Text = "Finalizar";
            btnAvancar2.Text = "Finalizar";
            //btnVerItensPedido.Text = "continuar con el registro";
            //btnVoltarParaItens.Text = "Volver";

            lblFiltro.Text = "Filtro";
             lblTipoFiltro.Text = "Tipo";
             lblDtInicioFiltro.Text = "Inicio";

             lblTituloGrid1.Text = "Elementos Disponibles";
        }
        else if (prmIdioma == "FRA")
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

            btnAvancarAtividades.Text = "Terminer";
            btnAvancar2.Text = "Terminer";
            //btnVerItensPedido.Text = "Continuer d`inscription";
            //btnVoltarParaItens.Text = "Retour";

            lblFiltro.Text = "Filtre";
             lblTipoFiltro.Text = "Type";
             lblDtInicioFiltro.Text = "Accueil";

             lblTituloGrid1.Text = "Articles disponibles";
        }
    }

    private void CarregarTipoDeAtividades()
    {

        DataTable DTTipo;

        DTTipo = SessionInscricoes.ListarTiposAtividade(SessionEvento.CdEvento, SessionCnn);

        DataTable oDataTable = new DataTable();

        oDataTable.Columns.Add("cdTipoAtividade");
        oDataTable.Columns.Add("noTipoAtividade");

        oDataTable.Rows.Add("", "");

        if ((SessionEvento.CdCliente == "0013") && ((SessionEvento.CdEvento == "001302") || (SessionEvento.CdEvento == "001304")))// || (SessionEvento.CdEvento == "001305")))
        {

            oDataTable.Rows.Add("('05')",
                                "CONGRESSO");
            oDataTable.Rows.Add("('02','06','21','25','28','44','45')",
                                "CONGRESSO/ATIVIDADES");

        }
        else
        {
            for (int i = 0; i < DTTipo.DefaultView.Count; i++)
            {
                if (SessionEvento.CdCliente != "0013")
                {
                    if ((SessionEvento.CdEvento != "006501") || (DTTipo.DefaultView[i]["cdTipoAtividade"].ToString().Trim() != "59"))
                        oDataTable.Rows.Add(DTTipo.DefaultView[i]["cdTipoAtividade"].ToString().Trim(),
                                           DTTipo.DefaultView[i]["noTipoAtividade"].ToString().Trim());
                }
                else
                {
                    if ((DTTipo.DefaultView[i]["cdTipoAtividade"].ToString().Trim() != "05"))
                        oDataTable.Rows.Add(DTTipo.DefaultView[i]["cdTipoAtividade"].ToString().Trim(),
                                           DTTipo.DefaultView[i]["noTipoAtividade"].ToString().Trim());
                }

            }
        }

       // bsTipo.DataSource = oDataTable.DefaultView;

        TxtFTipo.DataSource = oDataTable.DefaultView;
        TxtFTipo.DataTextField = "noTipoAtividade";
        TxtFTipo.DataValueField = "cdTipoAtividade";
        TxtFTipo.DataBind();
        TxtFTipo.SelectedIndex = 0;

    }
    
    private void CarregarTemaDeAtividades(String prmTipo)
    {
        /*
        DataTable DTTema;

        DTTema = null; // clCongresso.ListarFiltrosDeTemaCursos(prmTipo);

        DataTable oDTRecibos = new DataTable();

        oDTRecibos.Columns.Add("tb010_ds_tema_pt");

        oDTRecibos.Rows.Add("");
        for (int i = 0; i < DTTema.DefaultView.Count; i++)
        {
            oDTRecibos.Rows.Add(DTTema.DefaultView[i]["tb010_ds_tema_pt"].ToString().Trim());
        }

        bsTema.DataSource = oDTRecibos.DefaultView;

        txtFTema.DataSource = bsTema;
        txtFTema.DisplayMember = "tb010_ds_tema_pt";
        txtFTema.ValueMember = "tb010_ds_tema_pt";
        txtFTema.SelectedIndex = 0;
        */
    }
    
    private void CarregarDtInicioDeAtividades(String prmTipo)//, String prmTema)
    {

        DataTable DTHorario;

        DTHorario = SessionInscricoes.ListarHorarios(SessionEvento.CdEvento, prmTipo, SessionCnn);

        DataTable oDataTable = new DataTable();

        oDataTable.Columns.Add("dtInicio");
        oDataTable.Columns.Add("datInicio");

        oDataTable.Rows.Add("");
        oDataTable.Rows.Add("");
        if (SessionEvento.CdCliente == "0013")
        {
            if (SessionEvento.CdEvento == "001303")
            {
                oDataTable.Rows.Add("('01','02','03','09','15','19','20')",
                                    "ATIVIDADES DO DIA 7");
                oDataTable.Rows.Add("('21','16','17','18','10','11','12','13','14','04','05','06','07','08')",
                                    "ATIVIDADES DO DIA 7 E 8");
                oDataTable.Rows.Add("('22','23','24','25','26','27','28')",
                                    "ATIVIDADES DIA 8");
            }
            if (SessionEvento.CdEvento == "001304")
            {
                oDataTable.Rows.Add("('05','07','15','22')", "ATIVIDADES DO DIA 1");
                oDataTable.Rows.Add("('02','03','04','06','25','26')", "ATIVIDADES DO DIA 1 e 2");
                oDataTable.Rows.Add("('01','14','16','17','20','21','23')", "ATIVIDADES DO DIA 1 a 3 ");
                oDataTable.Rows.Add("('18','28')", "ATIVIDADES DO DIA 2");
                oDataTable.Rows.Add("('12,'24')", "ATIVIDADES DO DIA 2 e 3");
                oDataTable.Rows.Add("('08','09','10','11','13','19','29')", "ATIVIDADES DO DIA 3");
                oDataTable.Rows.Add("('26')", "ATIVIDADES DO DIA 4");
            }
            if (SessionEvento.CdEvento == "001305")
            {
                oDataTable.Rows.Add("('05','07','15','22')", "ATIVIDADES DO DIA 1");
                oDataTable.Rows.Add("('02','03','04','06','25','26')", "ATIVIDADES DO DIA 1 e 2");
                oDataTable.Rows.Add("('01','14','16','17','20','21','23')", "ATIVIDADES DO DIA 1 a 3 ");
                oDataTable.Rows.Add("('18','28')", "ATIVIDADES DO DIA 2");
                oDataTable.Rows.Add("('12,'24')", "ATIVIDADES DO DIA 2 e 3");
                oDataTable.Rows.Add("('08','09','10','11','13','19','29')", "ATIVIDADES DO DIA 3");
                oDataTable.Rows.Add("('26')", "ATIVIDADES DO DIA 4");
            }
        }
        else
        {
            for (int i = 0; i < DTHorario.DefaultView.Count; i++)
            {
                oDataTable.Rows.Add(DTHorario.DefaultView[i]["dtInicio"].ToString().Trim());
            }
        }

        //bsHorario.DataSource = oDataTable.DefaultView;

        txtFDtInicio.DataSource = oDataTable.DefaultView;
        txtFDtInicio.DataTextField = "datInicio";
        txtFDtInicio.DataValueField = "dtInicio";
        txtFDtInicio.DataBind();
        txtFDtInicio.SelectedIndex = 0;

    }
    protected void TxtFTipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        CarregarDtInicioDeAtividades(TxtFTipo.SelectedValue.ToString().Trim());//, txtFTema.Text.Trim());
        prpFiltrarAtividades();
    }
    protected void txtFDtInicio_SelectedIndexChanged(object sender, EventArgs e)
    {
        prpFiltrarAtividades();
    }
    private void prpFiltrarAtividades()
    {
        string filtro = "";
        if ((SessionEvento.CdCliente != "0013") || ((SessionEvento.CdCliente == "0013") && (SessionEvento.CdEvento == "001305")))
        {
            if (TxtFTipo.SelectedValue.ToString().Trim() != "")
            {
                if (filtro.Length == 0)
                    filtro += " (cdTipoAtividade like '%" + TxtFTipo.SelectedValue.ToString().Trim() + "%')";
                else
                    filtro += " and (cdTipoAtividade like '%" + TxtFTipo.SelectedValue.ToString().Trim() + "%')";
            }
        }
        else
        {
            if (((SessionEvento.CdCliente == "0013") && (SessionEvento.CdEvento == "001303")) || ((SessionEvento.CdCliente == "0013") && (SessionEvento.CdEvento == "001304")))// || ((SessionEvento.CdCliente == "0013") && (SessionEvento.CdEvento == "001305")))
            {
                if (TxtFTipo.SelectedValue.ToString().Trim() != "")
                {
                    if (filtro.Length == 0)
                        filtro += " (cdTipoAtividade in " + TxtFTipo.SelectedValue.ToString().Trim() + ")";
                    else
                        filtro += " and (cdTipoAtividade like " + TxtFTipo.SelectedValue.ToString().Trim() + ")";
                }
            }
            else if (TxtFTipo.SelectedValue.ToString().Trim() != "")
            {
                if (filtro.Length == 0)
                    filtro += " (dsTema in " + TxtFTipo.SelectedValue.ToString().Trim() + ")";
                else
                    filtro += " and (dsTema in " + TxtFTipo.SelectedValue.ToString().Trim() + ")";
            }
        }

        //if (txtFTema.SelectedValue.ToString().Trim() != "")
        //{
        //    if (filtro.Length == 0)
        //        filtro += " (tb010_ds_tema_pt like '%" + txtFTema.Text.Trim() + "%')";
        //    else
        //        filtro += " and (tb010_ds_tema_pt like '%" + txtFTema.Text.Trim() + "%')";
        //}

        if (SessionEvento.CdCliente != "0013") //|| ((SessionEvento.CdCliente == "0013") && (SessionEvento.CdEvento == "001302")))
        {
            if (txtFDtInicio.Text.ToString().Trim() != "")
            {
                if (filtro.Length == 0)
                    filtro += " (dtIni >= '" + txtFDtInicio.Text.Trim() + "' and dtIni <= '" + txtFDtInicio.Text.Trim() + "')";
                else
                    filtro += " and (dtIni >= '" + txtFDtInicio.Text.Trim() + "' and dtIni <= '" + txtFDtInicio.Text.Trim() + "')";
            }
        }
        else
        {
            if (txtFDtInicio.SelectedValue.ToString().Trim() != "")
            {
                if (filtro.Length == 0)
                    filtro += " (dsTema in " + txtFDtInicio.SelectedValue.ToString().Trim() + ")";
                else
                    filtro += " and (dsTema in " + txtFDtInicio.SelectedValue.ToString().Trim() + ")";
            }
        }

        //if (filtro.Length == 0)
        //    filtro += " (flInscricaoObrigatoria = 0) ";
        //else
        //    filtro += " and (flInscricaoObrigatoria = 0) ";

        /*
        if (varFiltroChoqueHorario.ToString().Trim() != "")
        {
            if (filtro.Length == 0)
                filtro += varFiltroChoqueHorario;
            else
                filtro += " and " + varFiltroChoqueHorario;
        }
        
        
        if (filtro.Length == 0)
            filtro += " cdAtividade not in ('" + cdAtiv.Trim().Replace(",", "','") + "')";
        else
            filtro += " and  cdAtividade not in ('" + cdAtiv.Trim().Replace(",", "','") + "')";
        */
        
        DataTable dt = oDTAtividades.Clone();

        DataRow[] dr = oDTAtividades.Select(filtro);

        foreach (DataRow drSimples in dr)
        {
            dt.ImportRow(drSimples);
        }


        grdAtv.DataSource = dt;

        grdAtv.DataBind();

        vlTotalAtiv.Font.Strikeout = (decimal.Parse(vlTotalDesc.Text) > 0);
    }
    private void CarregarAtividadesGrade()
    {
        oDTAtividades.Rows.Clear();

        DataTable DTAtividades = SessionInscricoes.ListarAtividadesDisponiveis(SessionParticipante, null, "inscrweb", SessionCnn);

        if ((SessionEvento.CdEvento == "007701") && (SessionIdioma == "PTBR"))
        {
            if (SessionAtv != "")
                DTAtividades.DefaultView.RowFilter = " cdAtividade = '007701001'";
            else if (SessionTipoAcesso == "NOVA")
                DTAtividades.DefaultView.RowFilter = " cdAtividade <> '007701001'";
        }

        int tmpNrItemObrig = 0;

        if ((DTAtividades != null) && (DTAtividades.DefaultView.Count > 0))
        {
            decimal desconto = 0;
            decimal vlrTotal = 0;
            int vlrVagas = 0;
            string noTitulo = "";
            string noLocal = "";
            string noTipoAtv = "";
            decimal vlrAtividade = 0;

            for (int i = 0; i < DTAtividades.DefaultView.Count; i++)
            {
                vlrAtividade = decimal.Parse(DTAtividades.DefaultView[i]["vlAtividade"].ToString().Trim());
                desconto = decimal.Parse(DTAtividades.DefaultView[i]["vlDescontoReal"].ToString().Trim());
                vlrTotal = decimal.Parse(DTAtividades.DefaultView[i]["vlTotInscri"].ToString().Trim());
                vlrVagas = int.Parse(DTAtividades.DefaultView[i]["vagas_em_aberto"].ToString().Trim());
                
                noTitulo = DTAtividades.DefaultView[i]["noTitulo"].ToString().Trim();
                noLocal = DTAtividades.DefaultView[i]["noLocal"].ToString().Trim();

                noTipoAtv = DTAtividades.DefaultView[i]["noTipoAtividade"].ToString().Trim();

                if ((SessionEvento.CdCliente == "0013") && ((SessionParticipante.CdCategoria == "00130201") || (SessionParticipante.CdCategoria == "00130301") || (SessionParticipante.CdCategoria == "00130401") || (SessionParticipante.CdCategoria == "00130501")))//conasems
                {
                    desconto = Geral.calcDescontoConasems(SessionParticipante, DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim(), SessionCnn);
                    vlrTotal = decimal.Parse(DTAtividades.DefaultView[i]["vlAtividade"].ToString().Trim()) - desconto;
                }

                if (SessionEvento.CdEvento == "007001")//biominas
                {
                    vlrAtividade = Geral.calcValorBiominas(SessionParticipante, DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim(), "VALOR", SessionCnn);
                    desconto = Geral.calcValorBiominas(SessionParticipante, DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim(),"DESCONTO", SessionCnn);
                    vlrTotal = vlrAtividade - desconto;
                }

                if ((SessionEvento.CdCliente == "0003")//consad
                    && (SessionParticipante.CdPatrocinador.Trim() == ""))//se tiver codigo do patrocinador ele pega o desconto do patrocinador
                {
                    if ((SessionParticipante.DsAuxiliar2.Trim().ToUpper() == "EMPENHO") && 
                        (( DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim() == "000304001") ||
                        (DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim() == "000305001") ||
                        (DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim() == "000306001") ||
                        (DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim() == "000307001"))
                        )
                    {
                        desconto = 100;
                        vlrTotal = 200;
                    }
                }

                if ((SessionEvento.CdEvento == "002902") //Enapa
                    && ( DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim() == "002902024"))
                {
                    AcompanhanteCad oAcompanhanteCad = new AcompanhanteCad();
                    DataTable DTAcompanhantes;

                    DTAcompanhantes = oAcompanhanteCad.Listar(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);

                    if ((DTAcompanhantes != null) && (DTAcompanhantes.Rows.Count > 0))
                    {
                        vlrVagas = int.Parse(DTAtividades.DefaultView[i]["vlCapacidade"].ToString().Trim()) - DTAcompanhantes.Rows.Count;
                    }
                }


                if ((SessionEvento.CdEvento == "008501") && (SessionIdioma != "PTBR") && (int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString()) > 8501004))
                {
                    noTitulo = Geral.atividadeRehuna(DTAtividades.DefaultView[i]["cdAtividade"].ToString(),SessionIdioma, "TITULO");
                    noLocal = Geral.atividadeRehuna(DTAtividades.DefaultView[i]["cdAtividade"].ToString(), SessionIdioma, "LOCAL"); 
                    vlrAtividade = vlrTotal = decimal.Parse(Geral.atividadeRehuna(DTAtividades.DefaultView[i]["cdAtividade"].ToString(),SessionIdioma, "VALOR"));
                }

                if ((SessionEvento.CdEvento == "012601") && (SessionIdioma != "PTBR") || (SessionParticipante.NoPais != "BRASIL"))
                {
                    noTitulo = atividadeNeuronus(DTAtividades.DefaultView[i]["cdAtividade"].ToString(), SessionIdioma, "TITULO");
                    noLocal = atividadeNeuronus(DTAtividades.DefaultView[i]["cdAtividade"].ToString(), SessionIdioma, "LOCAL");
                    vlrAtividade = decimal.Parse(atividadeNeuronus(DTAtividades.DefaultView[i]["cdAtividade"].ToString(), SessionIdioma, "VALOR"));
                    
                    DataTable DtDescontoCalc = ValorDesconto_012601(
                        SessionEvento.CdEvento, 
                        "D" + int.Parse(DTAtividades.DefaultView[i]["cdTipoAtividade"].ToString()).ToString(), SessionCnn);

                    if ((DtDescontoCalc != null) && (DtDescontoCalc.Rows.Count > 0))
                    {
                        desconto = decimal.Parse(DtDescontoCalc.DefaultView[0]["vlDesconto"].ToString());
                    }

                    vlrTotal = vlrAtividade - desconto;
                }

                if (SessionEvento.CdEvento == "010801") 
                {
                    if (SessionIdioma != "PTBR")
                    {
                        noTitulo = Geral.atividadeVeloCity(DTAtividades.DefaultView[i]["cdAtividade"].ToString(), SessionIdioma, "TITULO", "", "", "", "", null);
                        noLocal = Geral.atividadeVeloCity(DTAtividades.DefaultView[i]["cdAtividade"].ToString(), SessionIdioma, "LOCAL", "", "", "", "", null);

                        if (noTitulo == "")
                            noTitulo = DTAtividades.DefaultView[i]["noTitulo"].ToString().Trim();
                        if (noLocal == "")
                            noLocal = DTAtividades.DefaultView[i]["noLocal"].ToString().Trim();

                        if (DTAtividades.DefaultView[i]["cdAtividade"].ToString() == "010801001")
                            vlrAtividade = vlrTotal = decimal.Parse(Geral.atividadeVeloCity(DTAtividades.DefaultView[i]["cdAtividade"].ToString(), SessionIdioma, "VALOR", SessionParticipante.NoPais, SessionParticipante.CdEvento, "USD", SessionParticipante.CdCategoria, SessionCnn));
                    }
                    else
                    {
                        if (DTAtividades.DefaultView[i]["cdAtividade"].ToString() == "010801001")
                            vlrAtividade = vlrTotal = decimal.Parse(Geral.atividadeVeloCity(DTAtividades.DefaultView[i]["cdAtividade"].ToString(), SessionIdioma, "VALOR", SessionParticipante.NoPais, SessionParticipante.CdEvento, "USD", SessionParticipante.CdCategoria, SessionCnn));
                    }
                }

                if ((SessionEvento.CdEvento == "010901") && (SessionIdioma != "PTBR"))
                {
                    noTitulo = Geral.atividadeBrazilCyberDefence(DTAtividades.DefaultView[i]["cdAtividade"].ToString(), SessionIdioma, "TITULO");
                    noLocal = Geral.atividadeBrazilCyberDefence(DTAtividades.DefaultView[i]["cdAtividade"].ToString(), SessionIdioma, "LOCAL");
                    //vlrAtividade = vlrTotal = decimal.Parse(Geral.atividadeRehuna(DTAtividades.DefaultView[i]["cdAtividade"].ToString(), SessionIdioma, "VALOR"));

                    if (DTAtividades.DefaultView[i]["cdTipoAtividade"].ToString().Trim() == "67")
                        noTipoAtv = "FREE";
                    else if (DTAtividades.DefaultView[i]["cdTipoAtividade"].ToString().Trim() == "68")
                        noTipoAtv = "OPTIONAL PAYMENT";
                }

                if ((SessionAtv == "") ||
                    ((SessionAtv != "") && (SessionAtv == DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim())))
                {

                    oDTAtividades.Rows.Add(
                                        noTipoAtv,
                                        noTitulo,
                                        DTAtividades.DefaultView[i]["dsTema"].ToString().Trim(),
                                        noLocal,
                                        DateTime.Parse(DTAtividades.DefaultView[i]["dtIni"].ToString().Trim()).ToString("dd/MM/yyyy HH:mm"),
                                        DateTime.Parse(DTAtividades.DefaultView[i]["dtTermino"].ToString().Trim()).ToString("dd/MM/yyyy HH:mm"),
                                        vlrVagas.ToString(),
                                        DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim(),
                                        DTAtividades.DefaultView[i]["noProfessor"].ToString().Trim(),
                                        DTAtividades.DefaultView[i]["vlCapacidade"].ToString().Trim(),
                                        DTAtividades.DefaultView[i]["cdTipoAtividade"].ToString().Trim(),
                                        vlrAtividade,
                                        desconto,
                                        vlrTotal,
                                        bool.Parse(DTAtividades.DefaultView[i]["flInscricaoObrigatoria"].ToString().Trim()),
                                        DTAtividades.DefaultView[i]["dtValidade"].ToString().Trim(),
                                        bool.Parse(DTAtividades.DefaultView[i]["flPodeChocarHorario"].ToString().Trim()),
                                        DTAtividades.DefaultView[i]["dsCaminhoImgWEB"].ToString(),
                                        DTAtividades.DefaultView[i]["vlQuantidade"].ToString().Trim(),
                                        bool.Parse(DTAtividades.DefaultView[i]["flRequerQuantidade"].ToString().Trim()),
                                        i.ToString(),
                                        DTAtividades.DefaultView[i]["vlQuantidadeMaxima"].ToString().Trim(),
                                        DTAtividades.DefaultView[i]["flPodeRepetirPedido"].ToString().Trim(),
                                        DTAtividades.DefaultView[i]["dsTurno"].ToString().Trim());

                    if ((Boolean.Parse(DTAtividades.DefaultView[i]["flInscricaoObrigatoria"].ToString().Trim())) || ((SessionEvento.CdEvento == "009902") && (DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim() == "009902001") ))
                    {
                        tmpNrItemObrig++;
                        if (int.Parse(DTAtividades.DefaultView[i]["vagas_em_aberto"].ToString()) <= 0)
                        {
                            Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                                "011",
                                                ""), true);
                        }

                        oDTAtividadesParticipante.Rows.Add(
                                                DTAtividades.DefaultView[i]["noTipoAtividade"].ToString().Trim(),
                                                DTAtividades.DefaultView[i]["noTitulo"].ToString().Trim(),
                                                DTAtividades.DefaultView[i]["dsTema"].ToString().Trim(),
                                                DTAtividades.DefaultView[i]["noLocal"].ToString().Trim(),
                                                DateTime.Parse(DTAtividades.DefaultView[i]["dtIni"].ToString().Trim()).ToString("dd/MM/yyyy HH:mm"),
                                                DateTime.Parse(DTAtividades.DefaultView[i]["dtTermino"].ToString().Trim()).ToString("dd/MM/yyyy HH:mm"),
                                                DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim(),
                                                DTAtividades.DefaultView[i]["noProfessor"].ToString().Trim(),
                                                DTAtividades.DefaultView[i]["cdTipoAtividade"].ToString().Trim(),
                                                vlrAtividade,                                                 
                                                Boolean.Parse("True"),
                                                Boolean.Parse("False"),
                                                DateTime.Now,
                                                desconto,
                                                vlrTotal,
                                                Boolean.Parse(DTAtividades.DefaultView[i]["flInscricaoObrigatoria"].ToString().Trim()),
                                                DTAtividades.DefaultView[i]["dtValidade"].ToString().Trim(),
                                                Boolean.Parse(DTAtividades.DefaultView[i]["flPodeChocarHorario"].ToString().Trim()),
                                                DTAtividades.DefaultView[i]["dsCaminhoImgWEB"].ToString(),
                                                DTAtividades.DefaultView[i]["vlQuantidade"].ToString().Trim(),
                                                Boolean.Parse(DTAtividades.DefaultView[i]["flRequerQuantidade"].ToString().Trim()),
                                                oDTAtividadesParticipante.Rows.Count == 0 ? "0" : (oDTAtividadesParticipante.Rows.Count).ToString(),
                                                DTAtividades.DefaultView[i]["vlQuantidadeMaxima"].ToString().Trim(),
                                                DTAtividades.DefaultView[i]["flPodeRepetirPedido"].ToString().Trim(),
                                                DTAtividades.DefaultView[i]["dsTurno"].ToString().Trim());
                        
                        ////(SessionEvento.CdCliente == "0070" ? vlrAtividade : decimal.Parse(DTAtividades.DefaultView[i]["vlAtividade"].ToString().Trim())),
                        
                        //vlItens.Text = grdAtvParticipante.Rows.Count.ToString();
                        vlTotalAtiv.Text = (decimal.Parse(vlTotalAtiv.Text) + vlrTotal).ToString("N2");
                        vlTotalDesc.Text = (decimal.Parse(vlTotalDesc.Text) + desconto).ToString("N2");// decimal.Parse(DTAtividades.DefaultView[i]["vlDescontoReal"].ToString().Trim())).ToString("N2");
                        vlTotalPedido.Text = (decimal.Parse(vlTotalPedido.Text) + vlrTotal).ToString("N2");// decimal.Parse(DTAtividades.DefaultView[i]["vlTotInscri"].ToString().Trim())).ToString("N2");



                        if (cdAtiv.Trim() == "")
                            cdAtiv = DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim();//cdAtividade
                        else
                            cdAtiv = cdAtiv + "," + DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim();//cdAtividade

                        Session["cdAtiv"] = cdAtiv;
                        Session["oDTAtividadesParticipante"] = oDTAtividadesParticipante;
                    }
                }
            }
        }
        else
            Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                            "016",
                                            ""), true);

        Session["oDTAtividades"] = oDTAtividades;

        grdAtv.DataSource = oDTAtividades.DefaultView;
        grdAtv.DataBind();

        prpFiltrarAtividades();

        if (SessionEvento.CdEvento == "012601")
        {
            if ((SessionIdioma != "PTBR") || (SessionParticipante.NoPais != "BRASIL"))
                prpCalcularDescontosPorTipoAtividade_012601_ENUS();
        }

        //if ((tmpNrItemObrig > 0) && (grdAtv.Rows.Count > 0) && (SessionEvento.CdCliente != "0029") && (SessionEvento.CdCliente != "0016") && (SessionEvento.CdCliente != "0065"))
        //    if (tmpNrItemObrig == 1)
        //    {
        //        if (SessionIdioma == "PTBR")
        //            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('A atividade CONGRESSO tem caráter obrigatório e foi incluído automáticamente em seu pedido de inscrição!\\nÉ facultado ao participante selecionar outro(s) item(ns).'); </script>", false);
        //        else if (SessionIdioma == "ENUS")
        //            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('The Congress is binding activity and was included automatically in your application for registration!\\nIs allowed to partipant select another activity.'); </script>", false);
        //        else if (SessionIdioma == "ESP")
        //            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('¡Una actividad obligatoria se incluyó automáticamente en su solicitud de registro!\\nEl participante podrá seleccionar otras actividades.'); </script>", false);
        //        else if (SessionIdioma == "FRA")
        //            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Une activité de liaison a été automatiquement inclus dans votre demande d`inscription!\\n Le participant peut choisir d`autres activités.'); </script>", false);
                
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Os itens de caráter obrigatório foram incluídos automáticamente em seu pedido! \\n Selecione outras Atividade/Curso/Produtos e clique em continuar.'); </script>", false);
        //    }
    }
    private void CarregarAtividadesParticipanteGrade()
    {

        
        DataTable DTAtividadesp;
        if (!SessionEvento.FlEventoComRecebimentos)
            DTAtividadesp = SessionInscricoes.ListarAtividadesDoParticipante(SessionParticipante, SessionCnn);
        else
            DTAtividadesp = SessionInscricoes.ListarAtividadesDoPedido(SessionParticipante, lblNrPedido.Text, SessionCnn);

        //cdAtiv = "";

        if ((DTAtividadesp != null) && (DTAtividadesp.Rows.Count > 0))
        {
            oDTAtividadesParticipante.Rows.Clear();
            
            vlTotalAtiv.Text = "0,00";
            vlTotalDesc.Text = "0,00";
            vlTotalPedido.Text = "0,00";


            decimal desconto = 0;
            decimal vlrTotal = 0;

            //vlItens.Text = "0"; ;

            for (int i = 0; i < DTAtividadesp.DefaultView.Count; i++)
            {
                desconto = decimal.Parse(DTAtividadesp.DefaultView[i]["vlDesconto"].ToString().Trim());
                vlrTotal = decimal.Parse(DTAtividadesp.DefaultView[i]["vlMatricula"].ToString().Trim());

                if ((SessionEvento.CdCliente == "0003")//consad
                    && (SessionParticipante.CdPatrocinador.Trim() == ""))//se tiver codigo do patrocinador ele pega o desconto do patrocinador
                {
                    if ((SessionParticipante.DsAuxiliar2.Trim().ToUpper() == "EMPENHO") && (DTAtividadesp.DefaultView[i]["cdAtividade"].ToString().Trim() == "000304001"))
                    {
                        //desconto = 100;
                        //vlrTotal = 200;
                        if ((SessionPedido != null) && (SessionPedido.TpPagamento != "") && (SessionPedido.CdPedido == ""))
                        {
                            vlTotalDesc.Text = "100,00";
                            vlTotalPedido.Text = "-100,00";
                        }
                    }
                }

                if ((SessionEvento.CdCliente == "0013") && ((SessionParticipante.CdCategoria == "00130201") || (SessionParticipante.CdCategoria == "00130301") || (SessionParticipante.CdCategoria == "00130401") || (SessionParticipante.CdCategoria == "00130501")))//conasems
                {
                    desconto = Geral.calcDescontoConasems(SessionParticipante, DTAtividadesp.DefaultView[i]["cdAtividade"].ToString().Trim(), SessionCnn);
                    vlrTotal = decimal.Parse(DTAtividadesp.DefaultView[i]["vlAtividade"].ToString().Trim()) - desconto;
                }

                if (cdAtiv.Trim() == "")
                    cdAtiv = DTAtividadesp.DefaultView[i]["cdAtividade"].ToString().Trim();
                else
                    cdAtiv = cdAtiv + "," + DTAtividadesp.DefaultView[i]["cdAtividade"].ToString().Trim();

                if (SessionEvento.CdEvento == "001604")
                {
                    if (DTAtividadesp.DefaultView[i]["cdAtividade"].ToString().Trim() == "001604009")
                        cdAtiv += ",001604002,001604003,001604004,001604005,001604006,001604007,001604008";
                    else if (DTAtividadesp.DefaultView[i]["cdAtividade"].ToString().Trim() == "001604008")
                        cdAtiv += ",001604003,001604005,001604007,001604009";
                    else if (DTAtividadesp.DefaultView[i]["cdAtividade"].ToString().Trim() == "001604003")
                        cdAtiv += ",001604002,001604008,001604009";
                    else if (DTAtividadesp.DefaultView[i]["cdAtividade"].ToString().Trim() == "001604002")
                        cdAtiv += ",001604003,001604009";
                    else if (DTAtividadesp.DefaultView[i]["cdAtividade"].ToString().Trim() == "001604005")
                        cdAtiv += ",001604004,001604008,001604009";
                    else if (DTAtividadesp.DefaultView[i]["cdAtividade"].ToString().Trim() == "001604004")
                        cdAtiv += ",001604005,001604009";
                    else if (DTAtividadesp.DefaultView[i]["cdAtividade"].ToString().Trim() == "001604007")
                        cdAtiv += ",001604006,001604008,001604009";
                    else if (DTAtividadesp.DefaultView[i]["cdAtividade"].ToString().Trim() == "001604006")
                        cdAtiv += ",001604007,001604009";
                }

                //if (SessionEvento.CdEvento == "001606")
                //{
                //    if (DTAtividadesp.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606009")
                //        cdAtiv += ",001606002,001606003,001606004,001606005,001606006,001606007,001606008";
                //    else if (DTAtividadesp.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606008")
                //        cdAtiv += ",001606003,001606005,001606007,001606009";
                //    else if (DTAtividadesp.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606003")
                //        cdAtiv += ",001606002,001606008,001606009";
                //    else if (DTAtividadesp.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606002")
                //        cdAtiv += ",001606003,001606009";
                //    else if (DTAtividadesp.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606005")
                //        cdAtiv += ",001606004,001606008,001606009";
                //    else if (DTAtividadesp.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606004")
                //        cdAtiv += ",001606005,001606009";
                //    else if (DTAtividadesp.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606007")
                //        cdAtiv += ",001606006,001606008,001606009";
                //    else if (DTAtividadesp.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606006")
                //        cdAtiv += ",001606007,001606009";
                //}

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
                                    desconto,//decimal.Parse(DTAtividadesp.DefaultView[i]["vlDesconto"].ToString().Trim()),
                                    vlrTotal,//decimal.Parse(DTAtividadesp.DefaultView[i]["vlMatricula"].ToString().Trim()),
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
                vlTotalDesc.Text = (decimal.Parse(vlTotalDesc.Text) + (desconto * //decimal.Parse(DTAtividadesp.DefaultView[i]["vlDesconto"].ToString().Trim()) *
                                                                       int.Parse(DTAtividadesp.DefaultView[i]["vlQuantidade"].ToString()))
                                   ).ToString("N2");
                vlTotalPedido.Text = (decimal.Parse(vlTotalPedido.Text) + vlrTotal).ToString("N2");// decimal.Parse(DTAtividadesp.DefaultView[i]["vlMatricula"].ToString().Trim())).ToString("N2");

                if (!bool.Parse(DTAtividadesp.DefaultView[i]["flPodeChocarHorario"].ToString()))
                {
                    //alimetar a varialvel de filtro de datas
                    if (varFiltroChoqueHorario.Trim() != "")
                        varFiltroChoqueHorario += " and";

                    string dsturno = DTAtividadesp.DefaultView[i]["dsTurno"].ToString();
                    string wrk_dsturno = "";

                    if ((dsturno != "") && (dsturno != "I"))
                    {
                        wrk_dsturno = " AND ( dsTurno IN ('','I') OR dsTurno LIKE '%" + dsturno + "%'";

                        if (dsturno.Length == 2)
                        {
                            wrk_dsturno += " OR dsTurno LIKE '%" + dsturno.Substring(0, 1) + "%' OR dsTurno LIKE '%" + dsturno.Substring(1, 1) + "%' ";
                        }

                        wrk_dsturno += ") ";
                    }

                    varFiltroChoqueHorario += " ((flPodeChocarHorario = 'true') or " +
                                              "   ((flPodeChocarHorario = 'false') and not " +
                                              "    (((dtIni >= '" + DateTime.Parse(DTAtividadesp.DefaultView[i]["dtIni"].ToString()).ToString("dd/MM/yyyy HH:mm") + "') and " +
                                              "      (dtIni <= '" + DateTime.Parse(DTAtividadesp.DefaultView[i]["dtTermino"].ToString()).ToString("dd/MM/yyyy HH:mm") + "') " + wrk_dsturno + " ) or " +
                                              "     ((dtTermino >= '" + DateTime.Parse(DTAtividadesp.DefaultView[i]["dtIni"].ToString()).ToString("dd/MM/yyyy HH:mm") + "') and " +
                                              "      (dtTermino <= '" + DateTime.Parse(DTAtividadesp.DefaultView[i]["dtTermino"].ToString()).ToString("dd/MM/yyyy HH:mm") + "') " + wrk_dsturno + " ) or " +

                                              "     ((dtIni <= '" + DateTime.Parse(DTAtividadesp.DefaultView[i]["dtIni"].ToString()).ToString("dd/MM/yyyy HH:mm") + "') and " +
                                              "      (dtIni <= '" + DateTime.Parse(DTAtividadesp.DefaultView[i]["dtTermino"].ToString()).ToString("dd/MM/yyyy HH:mm") + "') and " +
                                              "      (dtTermino >= '" + DateTime.Parse(DTAtividadesp.DefaultView[i]["dtIni"].ToString()).ToString("dd/MM/yyyy HH:mm") + "') and " +
                                              "      (dtTermino >= '" + DateTime.Parse(DTAtividadesp.DefaultView[i]["dtTermino"].ToString()).ToString("dd/MM/yyyy HH:mm") + "') " + wrk_dsturno + " )) " +
                                              "))";
                    
                    Session["varFiltroChoqueHorario"] = varFiltroChoqueHorario;
                }
            }
        }

        //--VERIFICAR SE JÁ TÁ MATRICULADO E TIRAR DO GRID
        DataTable DTAtividadesMatricula = SessionInscricoes.ListarAtividadesDoParticipante(SessionParticipante, SessionCnn);


        if ((DTAtividadesMatricula != null) && (DTAtividadesMatricula.Rows.Count > 0))
        {

            for (int i = 0; i < DTAtividadesMatricula.DefaultView.Count; i++)
            {
                if (!bool.Parse(DTAtividadesMatricula.DefaultView[i]["flPodeChocarHorario"].ToString()))
                {
                    //alimetar a varialvel de filtro de datas
                    if (varFiltroChoqueHorario.Trim() != "")
                        varFiltroChoqueHorario += " and";

                    string dsturno = DTAtividadesMatricula.DefaultView[i]["dsTurno"].ToString();
                    string wrk_dsturno = "";

                    if ((dsturno != "") && (dsturno != "I"))
                    {
                        wrk_dsturno = " AND ( dsTurno IN ('','I') OR dsTurno LIKE '%" + dsturno + "%'";

                        if (dsturno.Length == 2)
                        {
                            wrk_dsturno += " OR dsTurno LIKE '%" + dsturno.Substring(0, 1) + "%' OR dsTurno LIKE '%" + dsturno.Substring(1, 1) + "%' ";
                        }

                        wrk_dsturno += ") ";
                    }

                    varFiltroChoqueHorario += " ((flPodeChocarHorario = 'true') or " +
                                              "   ((flPodeChocarHorario = 'false') and not " +
                                              "    (((dtIni >= '" + DateTime.Parse(DTAtividadesMatricula.DefaultView[i]["dtIni"].ToString()).ToString("dd/MM/yyyy HH:mm") + "') and " +
                                              "      (dtIni <= '" + DateTime.Parse(DTAtividadesMatricula.DefaultView[i]["dtTermino"].ToString()).ToString("dd/MM/yyyy HH:mm") + "') " + wrk_dsturno + " ) or " +
                                              "     ((dtTermino >= '" + DateTime.Parse(DTAtividadesMatricula.DefaultView[i]["dtIni"].ToString()).ToString("dd/MM/yyyy HH:mm") + "') and " +
                                              "      (dtTermino <= '" + DateTime.Parse(DTAtividadesMatricula.DefaultView[i]["dtTermino"].ToString()).ToString("dd/MM/yyyy HH:mm") + "') " + wrk_dsturno + " ) or " +

                                              "     ((dtIni <= '" + DateTime.Parse(DTAtividadesMatricula.DefaultView[i]["dtIni"].ToString()).ToString("dd/MM/yyyy HH:mm") + "') and " +
                                              "      (dtIni <= '" + DateTime.Parse(DTAtividadesMatricula.DefaultView[i]["dtTermino"].ToString()).ToString("dd/MM/yyyy HH:mm") + "') and " +
                                              "      (dtTermino >= '" + DateTime.Parse(DTAtividadesMatricula.DefaultView[i]["dtIni"].ToString()).ToString("dd/MM/yyyy HH:mm") + "') and " +
                                              "      (dtTermino >= '" + DateTime.Parse(DTAtividadesMatricula.DefaultView[i]["dtTermino"].ToString()).ToString("dd/MM/yyyy HH:mm") + "') " + wrk_dsturno + " )) " +
                                              "))";
                }
               
            }

            Session["varFiltroChoqueHorario"] = varFiltroChoqueHorario;
        }
        //------
        //---VERIFICAR PEDIDO PRÉ-PAGOS EM ABERTO E TIRAR AS ATIVIDADES CONFLITANTES DO GRID
        DataTable DTAtividadesPedidosPrePagos = SessionInscricoes.ListarAtividadesDoPedidosPrePagos(SessionParticipante, SessionCnn);


        if ((DTAtividadesPedidosPrePagos != null) && (DTAtividadesPedidosPrePagos.Rows.Count > 0))
        {

            for (int i = 0; i < DTAtividadesPedidosPrePagos.DefaultView.Count; i++)
            {
                if (!bool.Parse(DTAtividadesPedidosPrePagos.DefaultView[i]["flPodeChocarHorario"].ToString()))
                {
                    //alimetar a varialvel de filtro de datas
                    if (varFiltroChoqueHorario.Trim() != "")
                        varFiltroChoqueHorario += " and";

                    string dsturno = DTAtividadesPedidosPrePagos.DefaultView[i]["dsTurno"].ToString();
                    string wrk_dsturno = "";

                    if ((dsturno != "") && (dsturno != "I"))
                    {
                        wrk_dsturno = " AND ( dsTurno IN ('','I') OR dsTurno LIKE '%" + dsturno + "%'";

                        if (dsturno.Length == 2)
                        {
                            wrk_dsturno += " OR dsTurno LIKE '%" + dsturno.Substring(0, 1) + "%' OR dsTurno LIKE '%" + dsturno.Substring(1, 1) + "%' ";
                        }

                        wrk_dsturno += ") ";
                    }

                    varFiltroChoqueHorario += " ((flPodeChocarHorario = 'true') or " +
                                              "   ((flPodeChocarHorario = 'false') and not " +
                                              "    (((dtIni >= '" + DateTime.Parse(DTAtividadesPedidosPrePagos.DefaultView[i]["dtIni"].ToString()).ToString("dd/MM/yyyy HH:mm") + "') and " +
                                              "      (dtIni <= '" + DateTime.Parse(DTAtividadesPedidosPrePagos.DefaultView[i]["dtTermino"].ToString()).ToString("dd/MM/yyyy HH:mm") + "') " + wrk_dsturno + " ) or " +
                                              "     ((dtTermino >= '" + DateTime.Parse(DTAtividadesPedidosPrePagos.DefaultView[i]["dtIni"].ToString()).ToString("dd/MM/yyyy HH:mm") + "') and " +
                                              "      (dtTermino <= '" + DateTime.Parse(DTAtividadesPedidosPrePagos.DefaultView[i]["dtTermino"].ToString()).ToString("dd/MM/yyyy HH:mm") + "') " + wrk_dsturno + " ) or " +

                                              "     ((dtIni <= '" + DateTime.Parse(DTAtividadesPedidosPrePagos.DefaultView[i]["dtIni"].ToString()).ToString("dd/MM/yyyy HH:mm") + "') and " +
                                              "      (dtIni <= '" + DateTime.Parse(DTAtividadesPedidosPrePagos.DefaultView[i]["dtTermino"].ToString()).ToString("dd/MM/yyyy HH:mm") + "') and " +
                                              "      (dtTermino >= '" + DateTime.Parse(DTAtividadesPedidosPrePagos.DefaultView[i]["dtIni"].ToString()).ToString("dd/MM/yyyy HH:mm") + "') and " +
                                              "      (dtTermino >= '" + DateTime.Parse(DTAtividadesPedidosPrePagos.DefaultView[i]["dtTermino"].ToString()).ToString("dd/MM/yyyy HH:mm") + "') " + wrk_dsturno + " )) " +
                                              "))";
                }
                else
                {
                    if (!bool.Parse(DTAtividadesPedidosPrePagos.DefaultView[i]["flPodeRepetirPedido"].ToString()))
                    {
                        if (cdAtiv.Trim() == "")
                            cdAtiv = DTAtividadesPedidosPrePagos.DefaultView[i]["cdAtividade"].ToString().Trim();
                        else
                            cdAtiv = cdAtiv + "," + DTAtividadesPedidosPrePagos.DefaultView[i]["cdAtividade"].ToString().Trim();

                        if (SessionEvento.CdEvento == "001604")
                        {
                            if (DTAtividadesPedidosPrePagos.DefaultView[i]["cdAtividade"].ToString().Trim() == "001604009")
                                cdAtiv += ",001604002,001604003,001604004,001604005,001604006,001604007,001604008";
                            else if (DTAtividadesPedidosPrePagos.DefaultView[i]["cdAtividade"].ToString().Trim() == "001604008")
                                cdAtiv += ",001604003,001604005,001604007,001604009";
                            else if (DTAtividadesPedidosPrePagos.DefaultView[i]["cdAtividade"].ToString().Trim() == "001604003")
                                cdAtiv += ",001604002,001604008,001604009";
                            else if (DTAtividadesPedidosPrePagos.DefaultView[i]["cdAtividade"].ToString().Trim() == "001604002")
                                cdAtiv += ",001604003,001604009";
                            else if (DTAtividadesPedidosPrePagos.DefaultView[i]["cdAtividade"].ToString().Trim() == "001604005")
                                cdAtiv += ",001604004,001604008,001604009";
                            else if (DTAtividadesPedidosPrePagos.DefaultView[i]["cdAtividade"].ToString().Trim() == "001604004")
                                cdAtiv += ",001604005,001604009";
                            else if (DTAtividadesPedidosPrePagos.DefaultView[i]["cdAtividade"].ToString().Trim() == "001604007")
                                cdAtiv += ",001604006,001604008,001604009";
                            else if (DTAtividadesPedidosPrePagos.DefaultView[i]["cdAtividade"].ToString().Trim() == "001604006")
                                cdAtiv += ",001604007,001604009";
                        }
                    }
                }

            }

            Session["varFiltroChoqueHorario"] = varFiltroChoqueHorario;
        }


        //------


        Session["oDTAtividadesParticipante"] = oDTAtividadesParticipante;

        grdAtvParticipante.DataSource = oDTAtividadesParticipante.DefaultView;
        grdAtvParticipante.DataBind();

        vlItens.Text = grdAtvParticipante.Rows.Count.ToString();

        Session["cdAtiv"] = cdAtiv;

        prpFiltrarAtividades();
    }

    protected bool verficarChoqueHorario(String prmCdAtividadeRef, String prmDtIni, string prmDtTermino, string prmTurno)
    {
        for (int i = 0; i < oDTAtividadesParticipante.Rows.Count; i++)
        {
            if (prmCdAtividadeRef != oDTAtividadesParticipante.DefaultView[i]["cdAtividade"].ToString())
            {
                if (!bool.Parse(oDTAtividadesParticipante.DefaultView[i]["flPodeChocarHorario"].ToString()))//se tem alguma atv q tb não pode chocar horarios
                {   //Verifica horarios se coincidir tanto inicio com témino mostra a mensagem que não pode 
                    if (((DateTime.Parse(prmDtIni) >= DateTime.Parse(oDTAtividadesParticipante.DefaultView[i]["dtIni"].ToString())) &&
                         (DateTime.Parse(prmDtIni) <= DateTime.Parse(oDTAtividadesParticipante.DefaultView[i]["dtTermino"].ToString()))) ||
                        ((DateTime.Parse(prmDtTermino) >= DateTime.Parse(oDTAtividadesParticipante.DefaultView[i]["dtIni"].ToString())) &&
                         (DateTime.Parse(prmDtTermino) <= DateTime.Parse(oDTAtividadesParticipante.DefaultView[i]["dtTermino"].ToString()))) ||

                        ((DateTime.Parse(prmDtIni) <= DateTime.Parse(oDTAtividadesParticipante.DefaultView[i]["dtIni"].ToString())) &&
                         (DateTime.Parse(prmDtIni) <= DateTime.Parse(oDTAtividadesParticipante.DefaultView[i]["dtTermino"].ToString())) &&
                         (DateTime.Parse(prmDtTermino) >= DateTime.Parse(oDTAtividadesParticipante.DefaultView[i]["dtIni"].ToString())) &&
                         (DateTime.Parse(prmDtTermino) >= DateTime.Parse(oDTAtividadesParticipante.DefaultView[i]["dtTermino"].ToString())))
                       )
                    {
                        string dsturno1 = prmTurno;
                        string dsturno2 = oDTAtividadesParticipante.DefaultView[i]["dsTurno"].ToString();

                        if ((dsturno1 == "") ||
                            (dsturno1 == "I") ||
                            (dsturno2.Contains(dsturno1)) ||
                            ((dsturno1.Length == 2) && (dsturno2.Contains(dsturno1.Substring(0, 1)) || dsturno2.Contains(dsturno1.Substring(1, 1)))))
                        {
                            return true;
                        }
                    }
                }

                if (SessionEvento.CdEvento == "001606")
                {
                    if (oDTAtividadesParticipante.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606009")
                    {
                        if ((prmCdAtividadeRef == "001606002") || (prmCdAtividadeRef == "001606003") || (prmCdAtividadeRef == "001606004") || (prmCdAtividadeRef == "001606005") || (prmCdAtividadeRef == "001606006") || (prmCdAtividadeRef == "001606007") || (prmCdAtividadeRef == "001606008"))
                            return true;
                    }
                    else if (oDTAtividadesParticipante.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606008")
                    {
                        if ((prmCdAtividadeRef == "001606003") || (prmCdAtividadeRef == "001606005") || (prmCdAtividadeRef == "001606007") || (prmCdAtividadeRef == "001606009"))
                            return true;
                    }                        
                    else if (oDTAtividadesParticipante.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606003")
                    {
                        if ((prmCdAtividadeRef == "001606002") || (prmCdAtividadeRef == "001606008") || (prmCdAtividadeRef == "001606009"))
                            return true;
                    }
                    else if (oDTAtividadesParticipante.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606002")
                    {
                        if ((prmCdAtividadeRef == "001606003") || (prmCdAtividadeRef == "001606009"))
                            return true;
                    }
                    else if (oDTAtividadesParticipante.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606005")
                    {
                        if ((prmCdAtividadeRef == "001606004") || (prmCdAtividadeRef == "001606008") || (prmCdAtividadeRef == "001606009"))
                            return true;
                    }
                    else if (oDTAtividadesParticipante.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606004")
                    {
                        if ((prmCdAtividadeRef == "001606005") ||(prmCdAtividadeRef == "001606009"))
                            return true;
                    }
                    else if (oDTAtividadesParticipante.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606007")
                    {
                        if ((prmCdAtividadeRef == "001606006") || (prmCdAtividadeRef == "001606008") || (prmCdAtividadeRef == "001606009"))
                            return true;
                    }
                    else if (oDTAtividadesParticipante.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606006")
                    {
                        if ((prmCdAtividadeRef == "001606007") || (prmCdAtividadeRef == "001606009"))
                            return true;
                    }                        
                }
            }
        }

        DataTable tempDTAtividadesp = SessionInscricoes.ListarAtividadesDoParticipante(SessionParticipante, SessionCnn);
        if (tempDTAtividadesp != null)
        {
            for (int i = 0; i < tempDTAtividadesp.Rows.Count; i++)
            {
                if (prmCdAtividadeRef != tempDTAtividadesp.DefaultView[i]["cdAtividade"].ToString())
                {
                    if (!bool.Parse(tempDTAtividadesp.DefaultView[i]["flPodeChocarHorario"].ToString()))//se tem alguma atv q tb não pode chocar horarios
                    {   //Verifica horarios se coincidir tanto inicio com témino mostra a mensagem que não pode 
                        if (((DateTime.Parse(prmDtIni) >= DateTime.Parse(tempDTAtividadesp.DefaultView[i]["dtIni"].ToString())) &&
                             (DateTime.Parse(prmDtIni) <= DateTime.Parse(tempDTAtividadesp.DefaultView[i]["dtTermino"].ToString()))) ||
                            ((DateTime.Parse(prmDtTermino) >= DateTime.Parse(tempDTAtividadesp.DefaultView[i]["dtIni"].ToString())) &&
                             (DateTime.Parse(prmDtTermino) <= DateTime.Parse(tempDTAtividadesp.DefaultView[i]["dtTermino"].ToString()))) ||

                            ((DateTime.Parse(prmDtIni) <= DateTime.Parse(tempDTAtividadesp.DefaultView[i]["dtIni"].ToString())) &&
                             (DateTime.Parse(prmDtIni) <= DateTime.Parse(tempDTAtividadesp.DefaultView[i]["dtTermino"].ToString())) &&
                             (DateTime.Parse(prmDtTermino) >= DateTime.Parse(tempDTAtividadesp.DefaultView[i]["dtIni"].ToString())) &&
                             (DateTime.Parse(prmDtTermino) >= DateTime.Parse(tempDTAtividadesp.DefaultView[i]["dtTermino"].ToString())))
                          )
                        {
                            string dsturno1 = prmTurno;
                            string dsturno2 = tempDTAtividadesp.DefaultView[i]["dsTurno"].ToString();

                            if ((dsturno1 == "") ||
                                (dsturno1 == "I") ||
                                (dsturno2.Contains(dsturno1)) ||
                                ((dsturno1.Length == 2) && (dsturno2.Contains(dsturno1.Substring(0, 1)) || dsturno2.Contains(dsturno1.Substring(1, 1)))))
                            {
                                return true;
                            }
                        }
                    }
                }

                if (SessionEvento.CdEvento == "001606")
                {
                    if (tempDTAtividadesp.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606009")
                    {
                        if ((prmCdAtividadeRef == "001606002") || (prmCdAtividadeRef == "001606003") || (prmCdAtividadeRef == "001606004") || (prmCdAtividadeRef == "001606005") || (prmCdAtividadeRef == "001606006") || (prmCdAtividadeRef == "001606007") || (prmCdAtividadeRef == "001606008"))
                            return true;
                    }
                    else if (tempDTAtividadesp.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606008")
                    {
                        if ((prmCdAtividadeRef == "001606003") || (prmCdAtividadeRef == "001606005") || (prmCdAtividadeRef == "001606007") || (prmCdAtividadeRef == "001606009"))
                            return true;
                    }
                    else if (tempDTAtividadesp.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606003")
                    {
                        if ((prmCdAtividadeRef == "001606002") || (prmCdAtividadeRef == "001606008") || (prmCdAtividadeRef == "001606009"))
                            return true;
                    }
                    else if (tempDTAtividadesp.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606002")
                    {
                        if ((prmCdAtividadeRef == "001606003") || (prmCdAtividadeRef == "001606009"))
                            return true;
                    }
                    else if (tempDTAtividadesp.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606005")
                    {
                        if ((prmCdAtividadeRef == "001606004") || (prmCdAtividadeRef == "001606008") || (prmCdAtividadeRef == "001606009"))
                            return true;
                    }
                    else if (tempDTAtividadesp.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606004")
                    {
                        if ((prmCdAtividadeRef == "001606005") || (prmCdAtividadeRef == "001606009"))
                            return true;
                    }
                    else if (tempDTAtividadesp.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606007")
                    {
                        if ((prmCdAtividadeRef == "001606006") || (prmCdAtividadeRef == "001606008") || (prmCdAtividadeRef == "001606009"))
                            return true;
                    }
                    else if (tempDTAtividadesp.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606006")
                    {
                        if ((prmCdAtividadeRef == "001606007") || (prmCdAtividadeRef == "001606009"))
                            return true;
                    }
                }
            }
        }

        return false;
    }
    protected void grdAtv_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

        
        if (grdAtv.Rows.Count <= 0)
            return;
        mensagem.Visible = false;
        lblMsg.Text = "";

        if (cdAtiv.Contains(grdAtv.DataKeys[e.NewSelectedIndex].Values[7].ToString()))
        {
            DataRow[] foundRows;
            foundRows = oDTAtividadesParticipante.Select("cdAtividade = '"+grdAtv.DataKeys[e.NewSelectedIndex].Values[7].ToString()+"'");

            grdAtvParticipante_removerlinha(foundRows[0]);


            grdAtv.Rows[e.NewSelectedIndex].Cells[0].Focus();
            return;
        }

        if (int.Parse(grdAtv.DataKeys[e.NewSelectedIndex].Values[6].ToString()) <= 0)//vagas_em_aberto
        {
            //lblMsg.Text = "";
            if (SessionIdioma == "PTBR")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Não há mais vagas para esta atividade!'); </script>", false);
            else if (SessionIdioma == "ENUS")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('There are no more vacancies for this activity!'); </script>", false);
            else if (SessionIdioma == "ESP")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('¡No hay vacantes no más para esta actividad!'); </script>", false);
            else if (SessionIdioma == "FRA")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Il ya des postes vacants pas plus pour cette activité!'); </script>", false);
            
            return;
        }

        if ((SessionEvento.CdCliente == "0003") && (int.Parse(vlItens.Text) >= 11 ))//CONSAD -- total de atividade para o participante atingido
        {
            //lblMsg.Text = "";
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('O numero máximo de painéis por participante foi atingido!'); </script>", false);

            return;
        }

        /*if (((SessionEvento.CdEvento == "001002") &&
             ((grdAtv.DataKeys[e.NewSelectedIndex].Values[7].ToString() == "001002002") ||
              (grdAtv.DataKeys[e.NewSelectedIndex].Values[7].ToString() == "001002003"))) ||
            ((SessionEvento.CdEvento == "001004") &&
            ((grdAtv.DataKeys[e.NewSelectedIndex].Values[7].ToString() == "001004002") ||
             (grdAtv.DataKeys[e.NewSelectedIndex].Values[7].ToString() == "001004003") || 
             (grdAtv.DataKeys[e.NewSelectedIndex].Values[7].ToString() == "001004004") ||
             (grdAtv.DataKeys[e.NewSelectedIndex].Values[7].ToString() == "001004005")))
            )
        {
            //lblMsg.Text = "";
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Devido ao grande sucesso do evento, as hospedagens a partir de agora serão realizadas mediante consulta de disponibilidades nos hotéis.\\n\\nNão perca tempo.'); </script>", false);

           //return;
        }*/

        //verificar choques de horários
        if (!bool.Parse(grdAtv.DataKeys[e.NewSelectedIndex].Values[15].ToString()))//se não pode chocar horario
        {
            for (int i = 0; i < oDTAtividadesParticipante.Rows.Count; i++)
            {
                if (!bool.Parse(oDTAtividadesParticipante.DefaultView[i]["flPodeChocarHorario"].ToString()))//se tem alguma atv q tb não pode chocar horarios
                {   //Verifica horarios se coincidir tanto inicio com témino mostra a mensagem que não pode 
                    if (((DateTime.Parse(grdAtv.DataKeys[e.NewSelectedIndex].Values[4].ToString()) >= DateTime.Parse(oDTAtividadesParticipante.DefaultView[i]["dtIni"].ToString())) &&
                         (DateTime.Parse(grdAtv.DataKeys[e.NewSelectedIndex].Values[4].ToString()) <= DateTime.Parse(oDTAtividadesParticipante.DefaultView[i]["dtTermino"].ToString()))) ||
                        ((DateTime.Parse(grdAtv.DataKeys[e.NewSelectedIndex].Values[5].ToString()) >= DateTime.Parse(oDTAtividadesParticipante.DefaultView[i]["dtIni"].ToString())) &&
                         (DateTime.Parse(grdAtv.DataKeys[e.NewSelectedIndex].Values[5].ToString()) <= DateTime.Parse(oDTAtividadesParticipante.DefaultView[i]["dtTermino"].ToString()))) ||

                        ((DateTime.Parse(grdAtv.DataKeys[e.NewSelectedIndex].Values[4].ToString()) <= DateTime.Parse(oDTAtividadesParticipante.DefaultView[i]["dtIni"].ToString())) &&
                         (DateTime.Parse(grdAtv.DataKeys[e.NewSelectedIndex].Values[4].ToString()) <= DateTime.Parse(oDTAtividadesParticipante.DefaultView[i]["dtTermino"].ToString())) &&
                         (DateTime.Parse(grdAtv.DataKeys[e.NewSelectedIndex].Values[5].ToString()) >= DateTime.Parse(oDTAtividadesParticipante.DefaultView[i]["dtIni"].ToString())) &&
                         (DateTime.Parse(grdAtv.DataKeys[e.NewSelectedIndex].Values[5].ToString()) >= DateTime.Parse(oDTAtividadesParticipante.DefaultView[i]["dtTermino"].ToString())))
                       )
                    {
                        string dsturno1 = grdAtv.DataKeys[e.NewSelectedIndex].Values[22].ToString();
                        string dsturno2 = oDTAtividadesParticipante.DefaultView[i]["dsTurno"].ToString();

                        if ((dsturno1 == "") ||
                            (dsturno1 == "I") ||
                            (dsturno2.Contains(dsturno1)) ||
                            ((dsturno1.Length == 2) && (dsturno2.Contains(dsturno1.Substring(0, 1)) || dsturno2.Contains(dsturno1.Substring(1, 1)))))
                        {

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Atividade/Curso com choque de horários!'); </script>", false);

                            return;
                        }
                    }

                    
                }
            }

            DataTable tempDTAtividadesp = SessionInscricoes.ListarAtividadesDoParticipante(SessionParticipante, SessionCnn);
            if (tempDTAtividadesp != null)
            {
                for (int i = 0; i < tempDTAtividadesp.Rows.Count; i++)
                {
                    if (!bool.Parse(tempDTAtividadesp.DefaultView[i]["flPodeChocarHorario"].ToString()))//se tem alguma atv q tb não pode chocar horarios
                    {   //Verifica horarios se coincidir tanto inicio com témino mostra a mensagem que não pode 
                        if (((DateTime.Parse(grdAtv.DataKeys[e.NewSelectedIndex].Values[4].ToString()) >= DateTime.Parse(tempDTAtividadesp.DefaultView[i]["dtIni"].ToString())) &&
                             (DateTime.Parse(grdAtv.DataKeys[e.NewSelectedIndex].Values[4].ToString()) <= DateTime.Parse(tempDTAtividadesp.DefaultView[i]["dtTermino"].ToString()))) ||
                            ((DateTime.Parse(grdAtv.DataKeys[e.NewSelectedIndex].Values[5].ToString()) >= DateTime.Parse(tempDTAtividadesp.DefaultView[i]["dtIni"].ToString())) &&
                             (DateTime.Parse(grdAtv.DataKeys[e.NewSelectedIndex].Values[5].ToString()) <= DateTime.Parse(tempDTAtividadesp.DefaultView[i]["dtTermino"].ToString()))) ||

                            ((DateTime.Parse(grdAtv.DataKeys[e.NewSelectedIndex].Values[4].ToString()) <= DateTime.Parse(tempDTAtividadesp.DefaultView[i]["dtIni"].ToString())) &&
                             (DateTime.Parse(grdAtv.DataKeys[e.NewSelectedIndex].Values[4].ToString()) <= DateTime.Parse(tempDTAtividadesp.DefaultView[i]["dtTermino"].ToString())) &&
                             (DateTime.Parse(grdAtv.DataKeys[e.NewSelectedIndex].Values[5].ToString()) >= DateTime.Parse(tempDTAtividadesp.DefaultView[i]["dtIni"].ToString())) &&
                             (DateTime.Parse(grdAtv.DataKeys[e.NewSelectedIndex].Values[5].ToString()) >= DateTime.Parse(tempDTAtividadesp.DefaultView[i]["dtTermino"].ToString())))
                          )
                        {
                            string dsturno1 = grdAtv.DataKeys[e.NewSelectedIndex].Values[22].ToString();
                            string dsturno2 = tempDTAtividadesp.DefaultView[i]["dsTurno"].ToString();

                            if ((dsturno1 == "") ||
                                (dsturno1 == "I") ||
                                (dsturno2.Contains(dsturno1)) ||
                                ((dsturno1.Length == 2) && (dsturno2.Contains(dsturno1.Substring(0, 1)) || dsturno2.Contains(dsturno1.Substring(1, 1)))))
                            {
                                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Atividade/Curso com choque de horários!'); </script>", false);
                                return;
                            }
                        }
                    }
                }
            }

            //alimetar a varialvel de filtro de datas
            if (varFiltroChoqueHorario.Trim() != "")
                varFiltroChoqueHorario += " and";
            
            string dsturno = grdAtv.DataKeys[e.NewSelectedIndex].Values[22].ToString();
            string wrk_dsturno = "";

            if ((dsturno != "") && (dsturno != "I"))
            {
                wrk_dsturno = " AND ( dsTurno IN ('','I') OR dsTurno LIKE '%" + dsturno + "%'";

                if (dsturno.Length == 2)
                {
                    wrk_dsturno += " OR dsTurno LIKE '%" + dsturno.Substring(0, 1) + "%' OR dsTurno LIKE '%" + dsturno.Substring(1, 1) + "%' ";
                }

                wrk_dsturno += ") ";
            }

            varFiltroChoqueHorario += " ((flPodeChocarHorario = 'true') or " +
                                      "   ((flPodeChocarHorario = 'false') and not " +
                                      "    (((dtIni >= '" + DateTime.Parse(grdAtv.DataKeys[e.NewSelectedIndex].Values[4].ToString()).ToString("dd/MM/yyyy HH:mm") + "') and " +
                                      "      (dtIni <= '" + DateTime.Parse(grdAtv.DataKeys[e.NewSelectedIndex].Values[5].ToString()).ToString("dd/MM/yyyy HH:mm") + "') " + wrk_dsturno + " ) or " +
                                      "     ((dtTermino >= '" + DateTime.Parse(grdAtv.DataKeys[e.NewSelectedIndex].Values[4].ToString()).ToString("dd/MM/yyyy HH:mm") + "') and " +
                                      "      (dtTermino <= '" + DateTime.Parse(grdAtv.DataKeys[e.NewSelectedIndex].Values[5].ToString()).ToString("dd/MM/yyyy HH:mm") + "') " + wrk_dsturno + " ) or " +

                                      "     ((dtIni <= '" + DateTime.Parse(grdAtv.DataKeys[e.NewSelectedIndex].Values[4].ToString()).ToString("dd/MM/yyyy HH:mm") + "') and " +
                                      "      (dtIni <= '" + DateTime.Parse(grdAtv.DataKeys[e.NewSelectedIndex].Values[5].ToString()).ToString("dd/MM/yyyy HH:mm") + "') and " +
                                      "      (dtTermino >= '" + DateTime.Parse(grdAtv.DataKeys[e.NewSelectedIndex].Values[4].ToString()).ToString("dd/MM/yyyy HH:mm") + "') and " +
                                      "      (dtTermino >= '" + DateTime.Parse(grdAtv.DataKeys[e.NewSelectedIndex].Values[5].ToString()).ToString("dd/MM/yyyy HH:mm") + "') " + wrk_dsturno + " )) " +
                                      "))";

            Session["varFiltroChoqueHorario"] = varFiltroChoqueHorario;
        }

        #region 001606
        if (SessionEvento.CdEvento == "001606")
        {
            string prmCdAtividadeRef = grdAtv.DataKeys[e.NewSelectedIndex].Values[7].ToString();
            for (int i = 0; i < oDTAtividadesParticipante.Rows.Count; i++)
            {
                if (oDTAtividadesParticipante.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606009")
                {
                    if ((prmCdAtividadeRef == "001606002") || (prmCdAtividadeRef == "001606003") || (prmCdAtividadeRef == "001606004") || (prmCdAtividadeRef == "001606005") || (prmCdAtividadeRef == "001606006") || (prmCdAtividadeRef == "001606007") || (prmCdAtividadeRef == "001606008"))
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Atividade/Curso com choque de horários!'); </script>", false);
                        return;
                    }
                }
                else if (oDTAtividadesParticipante.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606008")
                {
                    if ((prmCdAtividadeRef == "001606003") || (prmCdAtividadeRef == "001606005") || (prmCdAtividadeRef == "001606007") || (prmCdAtividadeRef == "001606009"))
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Atividade/Curso com choque de horários!'); </script>", false);
                        return;
                    }
                }
                else if (oDTAtividadesParticipante.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606003")
                {
                    if ((prmCdAtividadeRef == "001606002") || (prmCdAtividadeRef == "001606008") || (prmCdAtividadeRef == "001606009"))
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Atividade/Curso com choque de horários!'); </script>", false);
                        return;
                    }
                }
                else if (oDTAtividadesParticipante.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606002")
                {
                    if ((prmCdAtividadeRef == "001606003") || (prmCdAtividadeRef == "001606009"))
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Atividade/Curso com choque de horários!'); </script>", false);
                        return;
                    }
                }
                else if (oDTAtividadesParticipante.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606005")
                {
                    if ((prmCdAtividadeRef == "001606004") || (prmCdAtividadeRef == "001606008") || (prmCdAtividadeRef == "001606009"))
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Atividade/Curso com choque de horários!'); </script>", false);
                        return;
                    }
                }
                else if (oDTAtividadesParticipante.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606004")
                {
                    if ((prmCdAtividadeRef == "001606005") || (prmCdAtividadeRef == "001606009"))
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Atividade/Curso com choque de horários!'); </script>", false);
                        return;
                    }
                }
                else if (oDTAtividadesParticipante.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606007")
                {
                    if ((prmCdAtividadeRef == "001606006") || (prmCdAtividadeRef == "001606008") || (prmCdAtividadeRef == "001606009"))
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Atividade/Curso com choque de horários!'); </script>", false);
                        return;
                    }
                }
                else if (oDTAtividadesParticipante.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606006")
                {
                    if ((prmCdAtividadeRef == "001606007") || (prmCdAtividadeRef == "001606009"))
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Atividade/Curso com choque de horários!'); </script>", false);
                        return;
                    }
                }
            }


            DataTable tempDTAtividadesp = SessionInscricoes.ListarAtividadesDoParticipante(SessionParticipante, SessionCnn);
            if (tempDTAtividadesp != null)
            {
                for (int i = 0; i < tempDTAtividadesp.Rows.Count; i++)
                {
                    if (tempDTAtividadesp.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606009")
                    {
                        if ((prmCdAtividadeRef == "001606002") || (prmCdAtividadeRef == "001606003") || (prmCdAtividadeRef == "001606004") || (prmCdAtividadeRef == "001606005") || (prmCdAtividadeRef == "001606006") || (prmCdAtividadeRef == "001606007") || (prmCdAtividadeRef == "001606008"))
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Atividade/Curso com choque de horários!'); </script>", false);
                            return;
                        }
                    }
                    else if (tempDTAtividadesp.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606008")
                    {
                        if ((prmCdAtividadeRef == "001606003") || (prmCdAtividadeRef == "001606005") || (prmCdAtividadeRef == "001606007") || (prmCdAtividadeRef == "001606009"))
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Atividade/Curso com choque de horários!'); </script>", false);
                            return;
                        }
                    }
                    else if (tempDTAtividadesp.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606003")
                    {
                        if ((prmCdAtividadeRef == "001606002") || (prmCdAtividadeRef == "001606008") || (prmCdAtividadeRef == "001606009"))
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Atividade/Curso com choque de horários!'); </script>", false);
                            return;
                        }
                    }
                    else if (tempDTAtividadesp.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606002")
                    {
                        if ((prmCdAtividadeRef == "001606003") || (prmCdAtividadeRef == "001606009"))
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Atividade/Curso com choque de horários!'); </script>", false);
                            return;
                        }
                    }
                    else if (tempDTAtividadesp.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606005")
                    {
                        if ((prmCdAtividadeRef == "001606004") || (prmCdAtividadeRef == "001606008") || (prmCdAtividadeRef == "001606009"))
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Atividade/Curso com choque de horários!'); </script>", false);
                            return;
                        }
                    }
                    else if (tempDTAtividadesp.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606004")
                    {
                        if ((prmCdAtividadeRef == "001606005") || (prmCdAtividadeRef == "001606009"))
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Atividade/Curso com choque de horários!'); </script>", false);
                            return;
                        }
                    }
                    else if (tempDTAtividadesp.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606007")
                    {
                        if ((prmCdAtividadeRef == "001606006") || (prmCdAtividadeRef == "001606008") || (prmCdAtividadeRef == "001606009"))
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Atividade/Curso com choque de horários!'); </script>", false);
                            return;
                        }
                    }
                    else if (tempDTAtividadesp.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606006")
                    {
                        if ((prmCdAtividadeRef == "001606007") || (prmCdAtividadeRef == "001606009"))
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Atividade/Curso com choque de horários!'); </script>", false);
                            return;
                        }
                    }
                }
            }
        }
        #endregion

        if ( ( (!SessionEvento.FlEventoComRecebimentos) && (!SessionCategoria.FlConfirmacaoCadWeb) ) ||
             ((SessionEvento.FlEventoComRecebimentos) && (!SessionCategoria.FlPagamento)) ||
             ((!SessionEvento.FlEventoComRecebimentos) && (!SessionCategoria.FlPagamento))
            )
        { // se EVENTO "SEM" recebimento < E > CATEGORIA "SEM" confirmação de inscrição < OU > EVENTO "COM" recebimento < E > CATEGORIA "SEM" pagamento 

            if (SessionInscricoes.MatriculasGravar(SessionParticipante.CdEvento, SessionParticipante.CdParticipante, grdAtv.DataKeys[e.NewSelectedIndex].Values[7].ToString(), 0, int.Parse(grdAtv.DataKeys[e.NewSelectedIndex].Values[17].ToString().Trim()), "000000001", SessionCnn))//cdAtividade
            {
                TxtFTipo.SelectedIndex = 0;
                //txtFTema.SelectedIndex = 0;
                txtFDtInicio.SelectedIndex = 0;

                //lblMsg.Text = SessionInscricoes.RcMsg;

                CarregarAtividadesGrade();
                CarregarAtividadesParticipanteGrade();


            }
            else
            {
                lblMsg.Text = SessionInscricoes.RcMsg;
                mensagem.Visible = true;
            }
        }
        else
        {

            oDTAtividadesParticipante.Rows.Add(
                                    grdAtv.DataKeys[e.NewSelectedIndex].Values[0].ToString().Trim(),
                                    grdAtv.DataKeys[e.NewSelectedIndex].Values[1].ToString().Trim(),
                                    grdAtv.DataKeys[e.NewSelectedIndex].Values[2].ToString().Trim(),
                                    grdAtv.DataKeys[e.NewSelectedIndex].Values[3].ToString().Trim(),
                                    DateTime.Parse(grdAtv.DataKeys[e.NewSelectedIndex].Values[4].ToString().Trim()).ToString("dd/MM/yyyy HH:mm"),
                                    DateTime.Parse(grdAtv.DataKeys[e.NewSelectedIndex].Values[5].ToString().Trim()).ToString("dd/MM/yyyy HH:mm"),
                                    grdAtv.DataKeys[e.NewSelectedIndex].Values[7].ToString().Trim(),
                                    grdAtv.DataKeys[e.NewSelectedIndex].Values[8].ToString().Trim(),
                                    grdAtv.DataKeys[e.NewSelectedIndex].Values[10].ToString().Trim(),
                                    decimal.Parse(grdAtv.DataKeys[e.NewSelectedIndex].Values[11].ToString().Trim()),
                                    Boolean.Parse("True"),
                                    Boolean.Parse("False"),
                                    DateTime.Now,
                                    decimal.Parse(grdAtv.DataKeys[e.NewSelectedIndex].Values[12].ToString().Trim()),//tmpVlDesc,
                                    decimal.Parse(grdAtv.DataKeys[e.NewSelectedIndex].Values[13].ToString().Trim()),//tmpVlMatricula);
                                    Boolean.Parse(grdAtv.DataKeys[e.NewSelectedIndex].Values[14].ToString().Trim()),
                                    "",
                                    Boolean.Parse(grdAtv.DataKeys[e.NewSelectedIndex].Values[15].ToString().Trim()),
                                    grdAtv.DataKeys[e.NewSelectedIndex].Values[16].ToString().Trim(),
                                    grdAtv.DataKeys[e.NewSelectedIndex].Values[17].ToString().Trim(),
                                    Boolean.Parse(grdAtv.DataKeys[e.NewSelectedIndex].Values[18].ToString().Trim()),
                                    oDTAtividadesParticipante.Rows.Count == 0 ? "0" : (oDTAtividadesParticipante.Rows.Count).ToString(),
                                    grdAtv.DataKeys[e.NewSelectedIndex].Values[20].ToString().Trim(),
                                    grdAtv.DataKeys[e.NewSelectedIndex].Values[21].ToString().Trim(),
                                    grdAtv.DataKeys[e.NewSelectedIndex].Values[22].ToString().Trim());
                                    
            
            vlTotalAtiv.Text = (decimal.Parse(vlTotalAtiv.Text) + (decimal.Parse(grdAtv.DataKeys[e.NewSelectedIndex].Values[11].ToString().Trim())  *
                                                                               int.Parse(grdAtv.DataKeys[e.NewSelectedIndex].Values[17].ToString()))).ToString("N2");
            vlTotalDesc.Text = (decimal.Parse(vlTotalDesc.Text) + (decimal.Parse(grdAtv.DataKeys[e.NewSelectedIndex].Values[12].ToString().Trim()))  *
                                                                               int.Parse(grdAtv.DataKeys[e.NewSelectedIndex].Values[17].ToString())).ToString("N2");
            vlTotalPedido.Text = (decimal.Parse(vlTotalPedido.Text) + decimal.Parse(grdAtv.DataKeys[e.NewSelectedIndex].Values[13].ToString().Trim())).ToString("N2");



            if (cdAtiv.Trim() == "")
                cdAtiv = grdAtv.DataKeys[e.NewSelectedIndex].Values[7].ToString().Trim();//cdAtividade
            else
                cdAtiv = cdAtiv + "," + grdAtv.DataKeys[e.NewSelectedIndex].Values[7].ToString().Trim();//cdAtividade


            if (SessionEvento.CdEvento == "001604")
            {
                if (grdAtv.DataKeys[e.NewSelectedIndex].Values[7].ToString().Trim() == "001604009")
                    cdAtiv += ",001604002,001604003,001604004,001604005,001604006,001604007,001604008";
                else if (grdAtv.DataKeys[e.NewSelectedIndex].Values[7].ToString().Trim() == "001604008")
                    cdAtiv += ",001604003,001604005,001604007,001604009";
                else if (grdAtv.DataKeys[e.NewSelectedIndex].Values[7].ToString().Trim() == "001604003")
                    cdAtiv += ",001604002,001604008,001604009";
                else if (grdAtv.DataKeys[e.NewSelectedIndex].Values[7].ToString().Trim() == "001604002")
                    cdAtiv += ",001604003,001604009";
                else if (grdAtv.DataKeys[e.NewSelectedIndex].Values[7].ToString().Trim() == "001604005")
                    cdAtiv += ",001604004,001604008,001604009";
                else if (grdAtv.DataKeys[e.NewSelectedIndex].Values[7].ToString().Trim() == "001604004")
                    cdAtiv += ",001604005,001604009";
                else if (grdAtv.DataKeys[e.NewSelectedIndex].Values[7].ToString().Trim() == "001604007")
                    cdAtiv += ",001604006,001604008,001604009";
                else if (grdAtv.DataKeys[e.NewSelectedIndex].Values[7].ToString().Trim() == "001604006")
                    cdAtiv += ",001604007,001604009";
            }


            Session["cdAtiv"] = cdAtiv;

            grdAtvParticipante.DataSource = oDTAtividadesParticipante.DefaultView;
            grdAtvParticipante.DataBind();

            vlItens.Text = grdAtvParticipante.Rows.Count.ToString();

            TxtFTipo.SelectedValue = "";
            txtFDtInicio.SelectedValue = "";

           //grdAtv.DataKeys[e.NewSelectedIndex].Values[15] = "";

            prpFiltrarAtividades();

            if (grdAtv.Rows.Count <= 0)
            {
                //btnAvancar.Visible = true;
                //btnVerItensPedido.Visible = false;
                //grdAtv.Visible = false;
                //grdAtvParticipante.Visible = true;
                //btnVoltarParaItens.Visible = true;
            }
            if (SessionEvento.CdEvento != "012601")
                prpCalcularDescontosPorTipoAtividade();
            else
            {
                if ((SessionIdioma == "PTBR") && (SessionParticipante.NoPais == "BRASIL"))
                    prpCalcularDescontosPorTipoAtividade_012601();
                else
                    prpCalcularDescontosPorTipoAtividade_012601_ENUS();
            }

            grdAtv.Rows[e.NewSelectedIndex].Cells[0].Focus();
        }
    }

    protected void prpCalcularDescontosPorTipoAtividade()
    {
        if (!SessionEvento.FlEventoComRecebimentos)
            return;
        
        DescontoCad oDescontoCad = new DescontoCad();

        DataTable oDTDescontos = oDescontoCad.ListarDescontosPorTipoAtividade(SessionParticipante.CdEvento, SessionParticipante.CdParticipante, SessionParticipante.CdCategoria, null, SessionCnn);


        if ((oDTDescontos == null) || (oDTDescontos.Rows.Count < 1))
            return;

        DataTable tmpDT = null;
        //DataRow[] dr;
        for (int j = 0; j < oDTDescontos.Rows.Count; j++ )
        {
            tmpDT = oDTAtividadesParticipante.Clone();
            string tmpFiltro = " cdTipoAtividade = '"+oDTDescontos.DefaultView[j]["cdTipoAtividade"].ToString() + "'";
            DataRow[] dr = oDTAtividadesParticipante.Select(tmpFiltro);// + oDTDescontos.DefaultView[j]["cdTipoAtividade"].ToString() + "'");
            foreach (DataRow drSimples in dr)
            {
                tmpDT.ImportRow(drSimples);
            }
            if (tmpDT.Rows.Count >= int.Parse(oDTDescontos.DefaultView[j]["vlQuantidade"].ToString()))
            {
                vlTotalAtiv.Text = "0,00";
                vlTotalDesc.Text = "0,00";
                vlTotalPedido.Text = "0,00";

                for (int i = 0; i < oDTAtividadesParticipante.Rows.Count; i++)
                {
                    if (oDTAtividadesParticipante.DefaultView[i]["cdTipoAtividade"].ToString() == oDTDescontos.DefaultView[j]["cdTipoAtividade"].ToString())
                    {
                        oDTAtividadesParticipante.DefaultView[i]["vlDesconto"] = oDTDescontos.DefaultView[j]["vlDescontoReal"].ToString();
                        oDTAtividadesParticipante.DefaultView[i]["vlMatricula"] = (decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlAtividade"].ToString()) - decimal.Parse(oDTDescontos.DefaultView[j]["vlDescontoReal"].ToString())).ToString("N2");
                            //oDTDescontos.DefaultView[j]["vlDescontoReal"].ToString();
                        oDTAtividadesParticipante.DefaultView[i]["dtValidade"] = oDTDescontos.DefaultView[j]["dtValidade"].ToString();
                    }
                    vlTotalAtiv.Text = (decimal.Parse(vlTotalAtiv.Text) + decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlAtividade"].ToString().Trim())).ToString("N2");
                    vlTotalDesc.Text = (decimal.Parse(vlTotalDesc.Text) + decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlDesconto"].ToString().Trim())).ToString("N2");
                    vlTotalPedido.Text = (decimal.Parse(vlTotalPedido.Text) + decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlMatricula"].ToString().Trim())).ToString("N2");

                }
            }
            else
            {
                vlTotalAtiv.Text = "0,00";
                vlTotalDesc.Text = "0,00";
                vlTotalPedido.Text = "0,00";

                for (int i = 0; i < oDTAtividadesParticipante.Rows.Count; i++)
                {
                    if (oDTAtividadesParticipante.DefaultView[i]["cdTipoAtividade"].ToString() == oDTDescontos.DefaultView[j]["cdTipoAtividade"].ToString())
                    {
                        oDTAtividadesParticipante.DefaultView[i]["vlDesconto"] = oDTDescontos.DefaultView[j]["vlDescontoOutros"].ToString();
                        oDTAtividadesParticipante.DefaultView[i]["vlMatricula"] = (decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlAtividade"].ToString()) - decimal.Parse(oDTDescontos.DefaultView[j]["vlDescontoOutros"].ToString())).ToString("N2");//vlDescontoReal
                        oDTAtividadesParticipante.DefaultView[i]["dtValidade"] = oDTDescontos.DefaultView[j]["dtValidadeOutros"].ToString();
                    }
                    vlTotalAtiv.Text = (decimal.Parse(vlTotalAtiv.Text) + decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlAtividade"].ToString().Trim())).ToString("N2");
                    vlTotalDesc.Text = (decimal.Parse(vlTotalDesc.Text) + decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlDesconto"].ToString().Trim())).ToString("N2");
                    vlTotalPedido.Text = (decimal.Parse(vlTotalPedido.Text) + decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlMatricula"].ToString().Trim())).ToString("N2");

                }
            }

        }

        grdAtvParticipante.DataSource = oDTAtividadesParticipante.DefaultView;
        grdAtvParticipante.DataBind();

        vlItens.Text = grdAtvParticipante.Rows.Count.ToString();
    }

    protected void prpCalcularDescontosPorTipoAtividade_012601()
    {
        if (!SessionEvento.FlEventoComRecebimentos)
            return;

        if ((oDTAtividadesParticipante == null) || (oDTAtividadesParticipante.Rows.Count <= 0))
            return;

        vlTotalAtiv.Text = "0,00";
        vlTotalDesc.Text = "0,00";
        vlTotalPedido.Text = "0,00";

        int soma = 0;
        for (int i = 0; i < oDTAtividadesParticipante.Rows.Count; i++)
        {
            if ((oDTAtividadesParticipante.DefaultView[i]["cdTipoAtividade"].ToString() != "05"))
            {
                soma = soma + int.Parse(oDTAtividadesParticipante.DefaultView[i]["cdTipoAtividade"].ToString());
            }
        }

        for (int i = 0; i < oDTAtividadesParticipante.Rows.Count; i++)
        {
            if ((oDTAtividadesParticipante.DefaultView[i]["cdTipoAtividade"].ToString() == "05") || (soma == 30) || (soma == 2))
            {
                DataTable DtDescontoVoltar = ValorDescontoOutros_012601(
                    SessionEvento.CdEvento, 
                    SessionParticipante.CdCategoria,
                    oDTAtividadesParticipante.DefaultView[i]["cdAtividade"].ToString(),
                    oDTAtividadesParticipante.DefaultView[i]["cdTipoAtividade"].ToString(), 
                    SessionCnn);

                oDTAtividadesParticipante.DefaultView[i]["vlDesconto"] = DtDescontoVoltar.DefaultView[0]["vlDescontoOutros"].ToString();
                oDTAtividadesParticipante.DefaultView[i]["vlMatricula"] = (decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlAtividade"].ToString()) - decimal.Parse(DtDescontoVoltar.DefaultView[0]["vlDescontoOutros"].ToString())).ToString("N2");//vlDescontoReal
                oDTAtividadesParticipante.DefaultView[i]["dtValidade"] = DtDescontoVoltar.DefaultView[0]["dtValidadeOutros"].ToString();

                vlTotalAtiv.Text = (decimal.Parse(vlTotalAtiv.Text) + decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlAtividade"].ToString().Trim())).ToString("N2");
                vlTotalDesc.Text = (decimal.Parse(vlTotalDesc.Text) + decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlDesconto"].ToString().Trim())).ToString("N2");
                vlTotalPedido.Text = (decimal.Parse(vlTotalPedido.Text) + decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlMatricula"].ToString().Trim())).ToString("N2");

                
            }
            else
            {
                //buscar valor do desconto
                DataTable DtDescontoCalc = ValorDesconto_012601(SessionEvento.CdEvento, "R" + soma.ToString(), SessionCnn);
                if ((DtDescontoCalc != null) && (DtDescontoCalc.Rows.Count > 0))
                {
                    oDTAtividadesParticipante.DefaultView[i]["vlDesconto"] = DtDescontoCalc.DefaultView[0]["vlDesconto"].ToString();
                    oDTAtividadesParticipante.DefaultView[i]["vlMatricula"] = (decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlAtividade"].ToString()) - decimal.Parse(DtDescontoCalc.DefaultView[0]["vlDesconto"].ToString())).ToString("N2");
                    //oDTDescontos.DefaultView[j]["vlDescontoReal"].ToString();
                    oDTAtividadesParticipante.DefaultView[i]["dtValidade"] = DtDescontoCalc.DefaultView[0]["dtValidade"].ToString();
                }

                vlTotalAtiv.Text = (decimal.Parse(vlTotalAtiv.Text) + decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlAtividade"].ToString().Trim())).ToString("N2");
                vlTotalDesc.Text = (decimal.Parse(vlTotalDesc.Text) + decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlDesconto"].ToString().Trim())).ToString("N2");
                vlTotalPedido.Text = (decimal.Parse(vlTotalPedido.Text) + decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlMatricula"].ToString().Trim())).ToString("N2");

                //verificar parte decimal do desconto
                decimal numero = decimal.Parse(vlTotalDesc.Text);
                int fracao = Convert.ToInt32((Math.Abs(numero) % 1).ToString().Substring(2));
                if (fracao == 1)
                {
                    vlTotalDesc.Text = (decimal.Parse(vlTotalDesc.Text) - 0.01m).ToString("N2");
                    oDTAtividadesParticipante.DefaultView[i]["vlDesconto"] = (decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlDesconto"].ToString()) - 0.01m).ToString("N2");
                }

                if (fracao == 99)
                {
                    vlTotalDesc.Text = (decimal.Parse(vlTotalDesc.Text) + 0.01m).ToString("N2");
                    oDTAtividadesParticipante.DefaultView[i]["vlDesconto"] = (decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlDesconto"].ToString()) + 0.01m).ToString("N2");
                }

                //verificar parte decimal do Total pedido
                decimal numero2 = decimal.Parse(vlTotalPedido.Text);
                int fracao2 = Convert.ToInt32((Math.Abs(numero2) % 1).ToString().Substring(2));
                if (fracao2 == 1)
                {
                    vlTotalPedido.Text = (decimal.Parse(vlTotalPedido.Text) - 0.01m).ToString("N2");
                    oDTAtividadesParticipante.DefaultView[i]["vlMatricula"] = (decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlMatricula"].ToString()) - 0.01m).ToString("N2");
                }

                if (fracao2 == 99)
                {
                    vlTotalPedido.Text = (decimal.Parse(vlTotalPedido.Text) + 0.01m).ToString("N2");
                    oDTAtividadesParticipante.DefaultView[i]["vlMatricula"] = (decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlMatricula"].ToString()) + 0.01m).ToString("N2");
                }
            }
        }

        

        grdAtvParticipante.DataSource = oDTAtividadesParticipante.DefaultView;
        grdAtvParticipante.DataBind();

        vlItens.Text = grdAtvParticipante.Rows.Count.ToString();
    }

    protected void prpCalcularDescontosPorTipoAtividade_012601_ENUS()
    {
        if (!SessionEvento.FlEventoComRecebimentos)
            return;

        if ((oDTAtividadesParticipante == null) || (oDTAtividadesParticipante.Rows.Count <= 0))
            return;

        vlTotalAtiv.Text = "0,00";
        vlTotalDesc.Text = "0,00";
        vlTotalPedido.Text = "0,00";

        int soma = 0;
        for (int i = 0; i < oDTAtividadesParticipante.Rows.Count; i++)
        {
            //if ((oDTAtividadesParticipante.DefaultView[i]["cdTipoAtividade"].ToString() != "05"))
            {
                soma = soma + int.Parse(oDTAtividadesParticipante.DefaultView[i]["cdTipoAtividade"].ToString());
            }
        }

        for (int i = 0; i < oDTAtividadesParticipante.Rows.Count; i++)
        {
            
            {
                int soma2 = soma;
                if ((soma == 7) || (soma == 35))
                    soma2 = int.Parse(oDTAtividadesParticipante.DefaultView[i]["cdTipoAtividade"].ToString());
                else
                {
                    if (soma != 5)
                    {
                        if (oDTAtividadesParticipante.DefaultView[i]["cdTipoAtividade"].ToString() == "05")
                            soma2 = 5;
                        else
                            soma2 = soma - 5;
                    }
                }


                //buscar valor do desconto
                DataTable DtDescontoCalc = ValorDesconto_012601(SessionEvento.CdEvento, "D" + soma2.ToString(), SessionCnn);

                decimal vlrAtividade = decimal.Parse(atividadeNeuronus(oDTAtividadesParticipante.DefaultView[i]["cdAtividade"].ToString(), SessionIdioma, "VALOR"));

                if ((DtDescontoCalc != null) && (DtDescontoCalc.Rows.Count > 0))
                {
                    oDTAtividadesParticipante.DefaultView[i]["vlDesconto"] = DtDescontoCalc.DefaultView[0]["vlDesconto"].ToString();
                    oDTAtividadesParticipante.DefaultView[i]["vlMatricula"] = (vlrAtividade - decimal.Parse(DtDescontoCalc.DefaultView[0]["vlDesconto"].ToString())).ToString("N2");
                    //oDTDescontos.DefaultView[j]["vlDescontoReal"].ToString();
                    oDTAtividadesParticipante.DefaultView[i]["dtValidade"] = DtDescontoCalc.DefaultView[0]["dtValidade"].ToString();
                }


                vlTotalAtiv.Text = (decimal.Parse(vlTotalAtiv.Text) + vlrAtividade).ToString("N2");
                vlTotalDesc.Text = (decimal.Parse(vlTotalDesc.Text) + decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlDesconto"].ToString().Trim())).ToString("N2");
                vlTotalPedido.Text = (decimal.Parse(vlTotalPedido.Text) + decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlMatricula"].ToString().Trim())).ToString("N2");

                //verificar parte decimal do desconto
                decimal numero = decimal.Parse(vlTotalDesc.Text);
                int fracao = Convert.ToInt32((Math.Abs(numero) % 1).ToString().Substring(2));
                if (fracao == 1)
                {
                    vlTotalDesc.Text = (decimal.Parse(vlTotalDesc.Text) - 0.01m).ToString("N2");
                    oDTAtividadesParticipante.DefaultView[i]["vlDesconto"] = (decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlDesconto"].ToString()) - 0.01m).ToString("N2");
                }

                if (fracao == 99)
                {
                    vlTotalDesc.Text = (decimal.Parse(vlTotalDesc.Text) + 0.01m).ToString("N2");
                    oDTAtividadesParticipante.DefaultView[i]["vlDesconto"] = (decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlDesconto"].ToString()) + 0.01m).ToString("N2");
                }

                //verificar parte decimal do Total pedido
                decimal numero2 = decimal.Parse(vlTotalPedido.Text);
                int fracao2 = Convert.ToInt32((Math.Abs(numero2) % 1).ToString().Substring(2));
                if (fracao2 == 1)
                {
                    vlTotalPedido.Text = (decimal.Parse(vlTotalPedido.Text) - 0.01m).ToString("N2");
                    oDTAtividadesParticipante.DefaultView[i]["vlMatricula"] = (decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlMatricula"].ToString()) - 0.01m).ToString("N2");
                }

                if (fracao2 == 99)
                {
                    vlTotalPedido.Text = (decimal.Parse(vlTotalPedido.Text) + 0.01m).ToString("N2");
                    oDTAtividadesParticipante.DefaultView[i]["vlMatricula"] = (decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlMatricula"].ToString()) + 0.01m).ToString("N2");
                }
            }
        }

        

        grdAtvParticipante.DataSource = oDTAtividadesParticipante.DefaultView;
        grdAtvParticipante.DataBind();

        vlItens.Text = grdAtvParticipante.Rows.Count.ToString();
    }

    public DataTable ValorDesconto_012601(string prmCdEvento, string prmChave, SqlConnection prmCnn)
    {
        
        if (prmCnn == null)
        {
            //_Rc = true;
            //_RcMsg = "Conexão inválida ou inexistente";
            return null;
        }
        if (prmCnn.State != ConnectionState.Open)
        {
            try
            {
                prmCnn.Open();
            }
            catch
            {
                //_Rc = true;
                //_RcMsg = "Conexão inválida";
                return null;
            }
        }

        DataTable DT = new DataTable();
        SqlDataAdapter Dap;
        try
        {
            try
            {
                SqlCommand comando = new SqlCommand(
                    @"SELECT dsc.cdEvento,
	                   dsc.cdDesconto,
	                   dsc.dsDesconto,
	                   dsc.tpDesconto,
                     vlDesconto,
	                   dsc.dtInicio,
	                   dsc.dtValidade
	   
                  from dbo.tbDescontos dsc 
 
                where dsc.cdEvento = @cdEvento 

                and cast(getdate() as int) between cast(dsc.dtInicio as int) and cast(dsc.dtValidade as int)
                  AND dsc.dsDesconto LIKE @prmChave+'%'", prmCnn);

                comando.Parameters.Add("@cdEvento", SqlDbType.NVarChar);
                comando.Parameters["@cdEvento"].Value = prmCdEvento;

                comando.Parameters.Add("@prmChave", SqlDbType.NVarChar);
                comando.Parameters["@prmChave"].Value = prmChave;

                Dap = new SqlDataAdapter(comando);

                Dap.TableMappings.Add("desconto", "tbDescontos");
                Dap.Fill(DT);

                
                return DT;

                

            }
            catch (Exception Ex)
            {
                //_Rc = true;
                //_RcMsg = "Erro ao selecionar UFs!\n" + Ex.Message;
                return null;

            }
        }
        finally
        {
            prmCnn.Close();
        }



    }

    public DataTable ValorDescontoOutros_012601(string prmCdEvento, string prmCdCategoira, string prmCdAtividade, string prmCdTipoAtividade, SqlConnection prmCnn)
    {

        if (prmCnn == null)
        {
            //_Rc = true;
            //_RcMsg = "Conexão inválida ou inexistente";
            return null;
        }
        if (prmCnn.State != ConnectionState.Open)
        {
            try
            {
                prmCnn.Open();
            }
            catch
            {
                //_Rc = true;
                //_RcMsg = "Conexão inválida";
                return null;
            }
        }

        DataTable DT = new DataTable();
        SqlDataAdapter Dap;
        try
        {
            try
            {
                SqlCommand comando = new SqlCommand(
                    @"SELECT dsc.cdEvento,
                               (dbo.fcDescontoCategAtiv(dsc.cdEvento,@cdParticipante,@cdAtividade,@cdCategoria,@cdTipoAtividade) ) as vlDescontoOutros,
                               (dbo.fcDtValidadeDesconto(dsc.cdEvento,@cdParticipante,@cdAtividade,@cdCategoria,@cdTipoAtividade) ) as dtValidadeOutros
                          from dbo.tbAtividades dsc 
                        where dsc.cdEvento = @cdEvento 
                          AND cdAtividade = @cdAtividade", prmCnn);

                comando.Parameters.Add("@cdEvento", SqlDbType.NVarChar);
                comando.Parameters["@cdEvento"].Value = prmCdEvento;

                comando.Parameters.Add("@cdParticipante", SqlDbType.NVarChar);
                comando.Parameters["@cdParticipante"].Value = "";

                comando.Parameters.Add("@cdAtividade", SqlDbType.NVarChar);
                comando.Parameters["@cdAtividade"].Value = prmCdAtividade;

                comando.Parameters.Add("@cdCategoria", SqlDbType.NVarChar);
                comando.Parameters["@cdCategoria"].Value = prmCdCategoira;

                comando.Parameters.Add("@cdTipoAtividade", SqlDbType.NVarChar);
                comando.Parameters["@cdTipoAtividade"].Value = prmCdTipoAtividade;

                Dap = new SqlDataAdapter(comando);

                Dap.TableMappings.Add("desconto", "tbDescontos");
                Dap.Fill(DT);


                return DT;



            }
            catch (Exception Ex)
            {
                //_Rc = true;
                //_RcMsg = "Erro ao selecionar UFs!\n" + Ex.Message;
                return null;

            }
        }
        finally
        {
            prmCnn.Close();
        }



    }

    public static string atividadeNeuronus(string prmCdAtividade, string prmIdioma, string prmResposta)
    {
        string retorno = "";
        switch (prmIdioma)
        {
            #region ENUS
            case "ENUS":
                {
                    switch (prmCdAtividade)
                    {
                        #region 012601001
                        case "012601001":
                            {
                                switch (prmResposta)
                                {
                                    case "TITULO":
                                        {
                                            retorno = "Registration – SEP 19, 20 and 21 – I International Conference on Neuroscience and Rehabilitation.";
                                            break;
                                        }
                                    case "LOCAL":
                                        {
                                            retorno = "Goiânia - GO, Brazil";
                                            break;
                                        }
                                    case "VALOR":
                                        {
                                            retorno = "200,00";
                                            break;
                                        }

                                }
                                break;
                            }
                        #endregion
                        #region 012601002
                        case "012601002":
                            {
                                switch (prmResposta)
                                {
                                    case "TITULO":
                                        {
                                            retorno = "C1.01 – 17/09 – 08:00 – 18:00 <br/>Introduction to Neurofeedback: Basic Principles, Research and Clinical Applications – Workload: 8h <br/>Diana Martinez<br/>Room 01";
                                            break;
                                        }
                                    case "LOCAL":
                                        {
                                            retorno = "Room 01";
                                            break;
                                        }
                                    case "VALOR":
                                        {
                                            retorno = "150,00";
                                            break;
                                        }

                                }
                                break;
                            }
                        #endregion
                        #region 012601003
                        case "012601003":
                            {
                                switch (prmResposta)
                                {
                                    case "TITULO":
                                        {
                                            retorno = "C1.02 – 17/09 – 08:00 – 18:00 <br/>Verbal and non-verbal Listening in the psychodiagnosis of sexual abused children: therapeutic paths to a reintegration process – Workload: 8h <br/>Viviane Teles <br/>Room 02";
                                            break;
                                        }
                                    case "LOCAL":
                                        {
                                            retorno = "Room 02";
                                            break;
                                        }
                                    case "VALOR":
                                        {
                                            retorno = "150,00";
                                            break;
                                        }

                                }
                                break;
                            }
                        #endregion
                        #region 012601004
                        case "012601004":
                            {
                                switch (prmResposta)
                                {
                                    case "TITULO":
                                        {
                                            retorno = "C1.03 – 17/09 – 08:00 – 12:00 <br/>Intervention in Early Childhood: Interfaces of Neuropsychological Rehabilitation, ABA and Cognitive Behavioral Therapy – Workload: 4h <br/>Marina Nery <br/>Room 03";
                                            break;
                                        }
                                    case "LOCAL":
                                        {
                                            retorno = "Room 03";
                                            break;
                                        }
                                    case "VALOR":
                                        {
                                            retorno = "125,00";
                                            break;
                                        }

                                }
                                break;
                            }
                        #endregion
                        #region 012601005
                        case "012601005":
                            {
                                switch (prmResposta)
                                {
                                    case "TITULO":
                                        {
                                            retorno = "C1.04 – 17/09 – 08:00 – 12:00 <br/>Use of tDCS in Language Therapy in children diagnosed with Autistic Spectrum Disorder (ASD) – Workload: 4h <br/>Henrique Salvador<br/>Room 04";
                                            break;
                                        }
                                    case "LOCAL":
                                        {
                                            retorno = "Room 04";
                                            break;
                                        }
                                    case "VALOR":
                                        {
                                            retorno = "125,00";
                                            break;
                                        }

                                }
                                break;
                            }
                        #endregion
                        #region 012601006
                        case "012601006":
                            {
                                switch (prmResposta)
                                {
                                    case "TITULO":
                                        {
                                            retorno = "C1.05 – 17/09 – 12:00 – 18:00 <br/>Neuropsychodiagnosis of Major Depression Disorder (MDD) – Workload: 4h <br/>Hercílio Jr.<br/>Room 03";
                                            break;
                                        }
                                    case "LOCAL":
                                        {
                                            retorno = "Room 03";
                                            break;
                                        }
                                    case "VALOR":
                                        {
                                            retorno = "125,00";
                                            break;
                                        }

                                }
                                break;
                            }
                        #endregion
                        #region 012601007
                        case "012601007":
                            {
                                switch (prmResposta)
                                {
                                    case "TITULO":
                                        {
                                            retorno = "C1.06 – 17/09 – 12:00 – 18:00 <br/>Neuropsychological Memory Rehabilitation in Brain Injury – Workload: 4h <br/>Sandra Schewinski <br/>Room 04";
                                            break;
                                        }
                                    case "LOCAL":
                                        {
                                            retorno = "Room 04";
                                            break;
                                        }
                                    case "VALOR":
                                        {
                                            retorno = "125,00";
                                            break;
                                        }

                                }
                                break;
                            }
                        #endregion
                        #region 012601008
                        case "012601008":
                            {
                                switch (prmResposta)
                                {
                                    case "TITULO":
                                        {
                                            retorno = "C2.01 – 18/09 – 08:00 – 18:00 <br/>Holistic Neuropsychological Rehabilitation (Goal settings and Outcomes Measurement) – Workload: 8h <br/>Andrew Bateman <br/>Room 01";
                                            break;
                                        }
                                    case "LOCAL":
                                        {
                                            retorno = "Room 01";
                                            break;
                                        }
                                    case "VALOR":
                                        {
                                            retorno = "150,00";
                                            break;
                                        }

                                }
                                break;
                            }
                        #endregion

                        #region 012601009
                        case "012601009":
                            {
                                switch (prmResposta)
                                {
                                    case "TITULO":
                                        {
                                            retorno = "C2.02 – 18/09 – 08:00 – 12:00 <br/>Neuromodulation Foundations: TMS and TDCS - From the beginning to the clinical applications - Workload: 4h <br/>André Brunoni <br/>Room 02";
                                            break;
                                        }
                                    case "LOCAL":
                                        {
                                            retorno = "Room 02";
                                            break;
                                        }
                                    case "VALOR":
                                        {
                                            retorno = "125,00";
                                            break;
                                        }

                                }
                                break;
                            }
                        #endregion
                        #region 012601010
                        case "012601010":
                            {
                                switch (prmResposta)
                                {
                                    case "TITULO":
                                        {
                                            retorno = "C2.03 – 18/09 – 08:00 – 12:00 <br/>Neuromodulation Foundations: TMS and TDCS - From the beginning to the clinical applications - Workload: 4h <br/>Paola Marangolo <br/>Room 03";
                                            break;
                                        }
                                    case "LOCAL":
                                        {
                                            retorno = "Room 03";
                                            break;
                                        }
                                    case "VALOR":
                                        {
                                            retorno = "125,00";
                                            break;
                                        }

                                }
                                break;
                            }
                        #endregion
                        #region 012601011
                        case "012601011":
                            {
                                switch (prmResposta)
                                {
                                    case "TITULO":
                                        {
                                            retorno = "C2.04 – 18/09 – 08:00 – 12:00 <br/>Phantom Pain Treatment for Amputees - Workload: 4h <br/>André Sugawara <br/>Room 04";
                                            break;
                                        }
                                    case "LOCAL":
                                        {
                                            retorno = "Room 04";
                                            break;
                                        }
                                    case "VALOR":
                                        {
                                            retorno = "125,00";
                                            break;
                                        }

                                }
                                break;
                            }
                        #endregion

                        #region 012601012
                        case "012601012":
                            {
                                switch (prmResposta)
                                {
                                    case "TITULO":
                                        {
                                            retorno = "C2.05 – 18/09 – 14:00 – 18:00 <br/>Non-invasive Brain Stimulation in the Rehabilitation of Patients with Movement Disorders (Parkinson's and Dystonia), Stroke, Chronic Pain, and other Applications in the Clinical Practice of Neurofunctional Physiotherapy - Workload: 4h <br/>Kátia Monte-silva <br/>Room 02";
                                            break;
                                        }
                                    case "LOCAL":
                                        {
                                            retorno = "Room 02";
                                            break;
                                        }
                                    case "VALOR":
                                        {
                                            retorno = "125,00";
                                            break;
                                        }

                                }
                                break;
                            }
                        #endregion
                        #region 012601013
                        case "012601013":
                            {
                                switch (prmResposta)
                                {
                                    case "TITULO":
                                        {
                                            retorno = "C2.06 – 18/09 – 14:00 – 18:00 <br/>Use of intentional activity as a care resource in dementia syndromes - exclusive to Occupational Therapists - Workload: 4h <br/>Márcia Novelli <br/>Room 03";
                                            break;
                                        }
                                    case "LOCAL":
                                        {
                                            retorno = "Room 03";
                                            break;
                                        }
                                    case "VALOR":
                                        {
                                            retorno = "125,00";
                                            break;
                                        }

                                }
                                break;
                            }
                        #endregion
                        #region 012601014
                        case "012601014":
                            {
                                switch (prmResposta)
                                {
                                    case "TITULO":
                                        {
                                            retorno = "C2.07 – 18/09 – 14:00 – 18:00 <br/>Executive Functions Assessment in Moderate Cognitive Impairment and Dementia - Workload: 4h <br/>Analucy Oliveira, PhD <br/>Room 04";
                                            break;
                                        }
                                    case "LOCAL":
                                        {
                                            retorno = "Room 4";
                                            break;
                                        }
                                    case "VALOR":
                                        {
                                            retorno = "125,00";
                                            break;
                                        }

                                }
                                break;
                            }
                        #endregion
                       
                    }
                    break;
                }
            #endregion

           
        }

        return retorno;
    }
    protected void grdAtvParticipante_removerlinha(DataRow linha)
    {
        if (grdAtvParticipante.Rows.Count <= 0)
            return;

        mensagem.Visible = false;
        lblMsg.Text = "";

        if (Boolean.Parse(linha[15].ToString().Trim()))// grdAtvParticipante.DataKeys[e.RowIndex].Values[15].ToString().Trim()))//inscricao obrigatoria
        {
            if (SessionIdioma == "PTBR")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Item de inscrição obrigatória, não pode ser removido!'); </script>", false);
            else if (SessionIdioma == "ENUS")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Compulsory item can not be removed!'); </script>", false);
            else if (SessionIdioma == "ESP")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('¡Elemento obligatorio no se puede eliminar!'); </script>", false);
            else if (SessionIdioma == "FRA")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Élément obligatoire ne peut être enlevé!'); </script>", false);

            return;
        }

        if ((!SessionEvento.FlEventoComRecebimentos) && (!SessionCategoria.FlConfirmacaoCadWeb))
        {

            SessionInscricoes.ExcluirAtividade(SessionEvento.CdEvento, SessionParticipante.CdParticipante, linha[6].ToString(), SessionCnn);

            TxtFTipo.SelectedIndex = 0;
            //txtFTema.SelectedIndex = 0;
            txtFDtInicio.SelectedIndex = 0;

            CarregarAtividadesGrade();
            CarregarAtividadesParticipanteGrade();

            prpFiltrarAtividades();
        }
        else
        {
            cdAtiv = cdAtiv.Replace("," + linha[6].ToString().Trim(), "");
            cdAtiv = cdAtiv.Replace(linha[6].ToString().Trim() + ",", "");
            cdAtiv = cdAtiv.Replace(linha[6].ToString().Trim(), "");



            if (SessionEvento.CdEvento == "001604")
            {
                if (linha[6].ToString().Trim() == "001604009")
                {
                    cdAtiv = cdAtiv.Replace(",001604002,001604003,001604004,001604005,001604006,001604007,001604008", "");
                    cdAtiv = cdAtiv.Replace("001604002,001604003,001604004,001604005,001604006,001604007,001604008", "");
                }
                else if (linha[6].ToString().Trim() == "001604008")
                {
                    cdAtiv = cdAtiv.Replace(",001604003,001604005,001604007,001604009", "");
                    cdAtiv = cdAtiv.Replace("001604003,001604005,001604007,001604009", "");
                }
                else if (linha[6].ToString().Trim() == "001604003")
                {
                    cdAtiv = cdAtiv.Replace(",001604002,001604008,001604009", "");
                    cdAtiv = cdAtiv.Replace("001604002,001604008,001604009", "");
                }
                else if (linha[6].ToString().Trim() == "001604002")
                {
                    cdAtiv = cdAtiv.Replace(",001604003,001604009", "");
                    cdAtiv = cdAtiv.Replace("001604003,001604009", "");
                }
                else if (linha[6].ToString().Trim() == "001604005")
                {
                    cdAtiv = cdAtiv.Replace(",001604004,001604008,001604009", "");
                    cdAtiv = cdAtiv.Replace("001604004,001604008,001604009", "");
                }
                else if (linha[6].ToString().Trim() == "001604004")
                {
                    cdAtiv = cdAtiv.Replace(",001604005,001604009", "");
                    cdAtiv = cdAtiv.Replace("001604005,001604009", "");
                }
                else if (linha[6].ToString().Trim() == "001604007")
                {
                    cdAtiv = cdAtiv.Replace(",001604006,001604008,001604009", "");
                    cdAtiv = cdAtiv.Replace("001604006,001604008,001604009", "");
                }
                else if (linha[6].ToString().Trim() == "001604006")
                {
                    cdAtiv = cdAtiv.Replace(",001604007,001604009", "");
                    cdAtiv = cdAtiv.Replace("001604007,001604009", "");
                }

            }

            Session["cdAtiv"] = cdAtiv;
            
            vlTotalAtiv.Text = (decimal.Parse(vlTotalAtiv.Text) - (decimal.Parse(linha[9].ToString().Trim()) * decimal.Parse(linha[19].ToString().Trim()))).ToString("N2");
            vlTotalDesc.Text = (decimal.Parse(vlTotalDesc.Text) - (decimal.Parse(linha[13].ToString().Trim()) * decimal.Parse(linha[19].ToString().Trim()))).ToString("N2");
            vlTotalPedido.Text = (decimal.Parse(vlTotalPedido.Text) - decimal.Parse(linha[14].ToString().Trim())).ToString("N2");

            string dsturno = linha[23].ToString();
            string wrk_dsturno = "";

            if ((dsturno != "") && (dsturno != "I"))
            {
                wrk_dsturno = " AND ( dsTurno IN ('','I') OR dsTurno LIKE '%" + dsturno + "%'";

                if (dsturno.Length == 2)
                {
                    wrk_dsturno += " OR dsTurno LIKE '%" + dsturno.Substring(0, 1) + "%' OR dsTurno LIKE '%" + dsturno.Substring(1, 1) + "%' ";
                }

                wrk_dsturno += ") ";
            }

            varFiltroChoqueHorario =
                varFiltroChoqueHorario.Replace(" and ((flPodeChocarHorario = 'true') or " +
                                          "   ((flPodeChocarHorario = 'false') and not " +
                                          "    (((dtIni >= '" + DateTime.Parse(linha[4].ToString()).ToString("dd/MM/yyyy HH:mm") + "') and " +
                                          "      (dtIni <= '" + DateTime.Parse(linha[5].ToString()).ToString("dd/MM/yyyy HH:mm") + "') " + wrk_dsturno + " ) or " +
                                          "     ((dtTermino >= '" + DateTime.Parse(linha[4].ToString()).ToString("dd/MM/yyyy HH:mm") + "') and " +
                                          "      (dtTermino <= '" + DateTime.Parse(linha[5].ToString()).ToString("dd/MM/yyyy HH:mm") + "') " + wrk_dsturno + " ) or " +

                                          "     ((dtIni <= '" + DateTime.Parse(linha[4].ToString()).ToString("dd/MM/yyyy HH:mm") + "') and " +
                                          "      (dtIni <= '" + DateTime.Parse(linha[5].ToString()).ToString("dd/MM/yyyy HH:mm") + "') and " +
                                          "      (dtTermino >= '" + DateTime.Parse(linha[4].ToString()).ToString("dd/MM/yyyy HH:mm") + "') and " +
                                          "      (dtTermino >= '" + DateTime.Parse(linha[5].ToString()).ToString("dd/MM/yyyy HH:mm") + "') " + wrk_dsturno + " )) " +
                                          "))", "");


            varFiltroChoqueHorario =
            varFiltroChoqueHorario.Replace(" ((flPodeChocarHorario = 'true') or " +
                                          "   ((flPodeChocarHorario = 'false') and not " +
                                          "    (((dtIni >= '" + DateTime.Parse(linha[4].ToString()).ToString("dd/MM/yyyy HH:mm") + "') and " +
                                          "      (dtIni <= '" + DateTime.Parse(linha[5].ToString()).ToString("dd/MM/yyyy HH:mm") + "') " + wrk_dsturno + " ) or " +
                                          "     ((dtTermino >= '" + DateTime.Parse(linha[4].ToString()).ToString("dd/MM/yyyy HH:mm") + "') and " +
                                          "      (dtTermino <= '" + DateTime.Parse(linha[5].ToString()).ToString("dd/MM/yyyy HH:mm") + "') " + wrk_dsturno + " ) or " +

                                          "     ((dtIni <= '" + DateTime.Parse(linha[4].ToString()).ToString("dd/MM/yyyy HH:mm") + "') and " +
                                          "      (dtIni <= '" + DateTime.Parse(linha[5].ToString()).ToString("dd/MM/yyyy HH:mm") + "') and " +
                                          "      (dtTermino >= '" + DateTime.Parse(linha[4].ToString()).ToString("dd/MM/yyyy HH:mm") + "') and " +
                                          "      (dtTermino >= '" + DateTime.Parse(linha[5].ToString()).ToString("dd/MM/yyyy HH:mm") + "') " + wrk_dsturno + " )) " +
                                          "))", "");

            if ((varFiltroChoqueHorario.Trim() != "") && (varFiltroChoqueHorario.Substring(0, 4) == " and"))
                varFiltroChoqueHorario = varFiltroChoqueHorario.Remove(0, 4);

            Session["varFiltroChoqueHorario"] = varFiltroChoqueHorario;

            oDTAtividadesParticipante.Rows.Remove(linha);

            calcularValores();

            grdAtvParticipante.DataSource = oDTAtividadesParticipante.DefaultView;
            grdAtvParticipante.DataBind();

            vlItens.Text = grdAtvParticipante.Rows.Count.ToString();

            prpFiltrarAtividades();

            if (grdAtvParticipante.Rows.Count <= 0)
            {
                //btnAvancar.Visible = false;
                //btnVerItensPedido.Text = "Ver Itens Pedido";
                //btnVerItensPedido.Visible = true;
                //grdAtv.Visible = true;
                //grdAtvParticipante.Visible = false;
                //btnVoltarParaItens.Visible = false;
            }
            else if (grdAtvParticipante.Rows.Count == 1)
            {
                //if (Boolean.Parse(linha[15].ToString().Trim()))//inscricao obrigatoria
                //{
                    //btnAvancar.Visible = false;
                    //btnVerItensPedido.Text = "Ver Itens Pedido";
                    //btnVerItensPedido.Visible = true;
                    //grdAtv.Visible = true;
                    //grdAtvParticipante.Visible = false;
                    //btnVoltarParaItens.Visible = false;
                //}
            }

            if (SessionEvento.CdEvento != "012601")
                prpCalcularDescontosPorTipoAtividade();
            else
            {
                if ((SessionIdioma == "PTBR") && (SessionParticipante.NoPais == "BRASIL"))
                    prpCalcularDescontosPorTipoAtividade_012601();
                else
                    prpCalcularDescontosPorTipoAtividade_012601_ENUS();
            }
        }
    }

    protected void grdAtvParticipante_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (grdAtvParticipante.Rows.Count <= 0)
            return;

        mensagem.Visible = false;
        lblMsg.Text = "";

        if (Boolean.Parse(grdAtvParticipante.DataKeys[e.RowIndex].Values[15].ToString().Trim()))//inscricao obrigatoria
        {
            if (SessionIdioma == "PTBR")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Item de inscrição obrigatória, não pode ser removido!'); </script>", false);
            else if (SessionIdioma == "ENUS")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Compulsory item can not be removed!'); </script>", false);
            else if (SessionIdioma == "ESP")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('¡Elemento obligatorio no se puede eliminar!'); </script>", false);
            else if (SessionIdioma == "FRA")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Élément obligatoire ne peut être enlevé!'); </script>", false);

            return;
        }

        if ((!SessionEvento.FlEventoComRecebimentos) && (!SessionCategoria.FlConfirmacaoCadWeb))
        {
           
            SessionInscricoes.ExcluirAtividade(SessionEvento.CdEvento, SessionParticipante.CdParticipante, grdAtvParticipante.DataKeys[e.RowIndex].Values[6].ToString(), SessionCnn);

            TxtFTipo.SelectedIndex = 0;
            //txtFTema.SelectedIndex = 0;
            txtFDtInicio.SelectedIndex = 0;

            CarregarAtividadesGrade();
            CarregarAtividadesParticipanteGrade();

            prpFiltrarAtividades();
        }
        else
        {
            cdAtiv = cdAtiv.Replace("," + grdAtvParticipante.DataKeys[e.RowIndex].Values[6].ToString().Trim(), "");
            cdAtiv = cdAtiv.Replace(grdAtvParticipante.DataKeys[e.RowIndex].Values[6].ToString().Trim() + ",", "");
            cdAtiv = cdAtiv.Replace(grdAtvParticipante.DataKeys[e.RowIndex].Values[6].ToString().Trim(), "");

            

            if (SessionEvento.CdEvento == "001604")
            {
                if (grdAtvParticipante.DataKeys[e.RowIndex].Values[6].ToString().Trim() == "001604009")
                {
                    cdAtiv = cdAtiv.Replace(",001604002,001604003,001604004,001604005,001604006,001604007,001604008", "");
                    cdAtiv = cdAtiv.Replace("001604002,001604003,001604004,001604005,001604006,001604007,001604008", "");
                }
                else if (grdAtvParticipante.DataKeys[e.RowIndex].Values[6].ToString().Trim() == "001604008")
                {
                    cdAtiv = cdAtiv.Replace(",001604003,001604005,001604007,001604009", "");
                    cdAtiv = cdAtiv.Replace("001604003,001604005,001604007,001604009", "");
                }
                else if (grdAtvParticipante.DataKeys[e.RowIndex].Values[6].ToString().Trim() == "001604003")
                {
                    cdAtiv = cdAtiv.Replace(",001604002,001604008,001604009", "");
                    cdAtiv = cdAtiv.Replace("001604002,001604008,001604009", "");
                }
                else if (grdAtvParticipante.DataKeys[e.RowIndex].Values[6].ToString().Trim() == "001604002")
                {
                    cdAtiv = cdAtiv.Replace(",001604003,001604009", "");
                    cdAtiv = cdAtiv.Replace("001604003,001604009", "");
                }
                else if (grdAtvParticipante.DataKeys[e.RowIndex].Values[6].ToString().Trim() == "001604005")
                {
                    cdAtiv = cdAtiv.Replace(",001604004,001604008,001604009", "");
                    cdAtiv = cdAtiv.Replace("001604004,001604008,001604009", "");
                }
                else if (grdAtvParticipante.DataKeys[e.RowIndex].Values[6].ToString().Trim() == "001604004")
                {
                    cdAtiv = cdAtiv.Replace(",001604005,001604009", "");
                    cdAtiv = cdAtiv.Replace("001604005,001604009", "");
                }
                else if (grdAtvParticipante.DataKeys[e.RowIndex].Values[6].ToString().Trim() == "001604007")
                {
                    cdAtiv = cdAtiv.Replace(",001604006,001604008,001604009", "");
                    cdAtiv = cdAtiv.Replace("001604006,001604008,001604009", "");
                }
                else if (grdAtvParticipante.DataKeys[e.RowIndex].Values[6].ToString().Trim() == "001604006")
                {
                    cdAtiv = cdAtiv.Replace(",001604007,001604009", "");
                    cdAtiv = cdAtiv.Replace("001604007,001604009", "");
                }

            }

            Session["cdAtiv"] = cdAtiv;

            vlTotalAtiv.Text = (decimal.Parse(vlTotalAtiv.Text) - decimal.Parse(grdAtvParticipante.DataKeys[e.RowIndex].Values[9].ToString().Trim())).ToString("N2");
            vlTotalDesc.Text = (decimal.Parse(vlTotalDesc.Text) - decimal.Parse(grdAtvParticipante.DataKeys[e.RowIndex].Values[13].ToString().Trim())).ToString("N2");
            vlTotalPedido.Text = (decimal.Parse(vlTotalPedido.Text) - decimal.Parse(grdAtvParticipante.DataKeys[e.RowIndex].Values[14].ToString().Trim())).ToString("N2");

            string dsturno = grdAtvParticipante.DataKeys[e.RowIndex].Values[23].ToString();
            string wrk_dsturno = "";

            if ((dsturno != "") && (dsturno != "I"))
            {
                wrk_dsturno = " AND ( dsTurno IN ('','I') OR dsTurno LIKE '%" + dsturno + "%'";

                if (dsturno.Length == 2)
                {
                    wrk_dsturno += " OR dsTurno LIKE '%" + dsturno.Substring(0, 1) + "%' OR dsTurno LIKE '%" + dsturno.Substring(1, 1) + "%' ";
                }

                wrk_dsturno += ") ";
            }

            varFiltroChoqueHorario =
                varFiltroChoqueHorario.Replace(" and ((flPodeChocarHorario = 'true') or " +
                                          "   ((flPodeChocarHorario = 'false') and not " +
                                          "    (((dtIni >= '" + DateTime.Parse(grdAtvParticipante.DataKeys[e.RowIndex].Values[4].ToString()).ToString("dd/MM/yyyy HH:mm") + "') and " +
                                          "      (dtIni <= '" + DateTime.Parse(grdAtvParticipante.DataKeys[e.RowIndex].Values[5].ToString()).ToString("dd/MM/yyyy HH:mm") + "') " + wrk_dsturno + " ) or " +
                                          "     ((dtTermino >= '" + DateTime.Parse(grdAtvParticipante.DataKeys[e.RowIndex].Values[4].ToString()).ToString("dd/MM/yyyy HH:mm") + "') and " +
                                          "      (dtTermino <= '" + DateTime.Parse(grdAtvParticipante.DataKeys[e.RowIndex].Values[5].ToString()).ToString("dd/MM/yyyy HH:mm") + "') " + wrk_dsturno + " ) or " +

                                          "     ((dtIni <= '" + DateTime.Parse(grdAtvParticipante.DataKeys[e.RowIndex].Values[4].ToString()).ToString("dd/MM/yyyy HH:mm") + "') and " +
                                          "      (dtIni <= '" + DateTime.Parse(grdAtvParticipante.DataKeys[e.RowIndex].Values[5].ToString()).ToString("dd/MM/yyyy HH:mm") + "') and " +
                                          "      (dtTermino >= '" + DateTime.Parse(grdAtvParticipante.DataKeys[e.RowIndex].Values[4].ToString()).ToString("dd/MM/yyyy HH:mm") + "') and " +
                                          "      (dtTermino >= '" + DateTime.Parse(grdAtvParticipante.DataKeys[e.RowIndex].Values[5].ToString()).ToString("dd/MM/yyyy HH:mm") + "') " + wrk_dsturno + " )) " +
                                          "))", "");


            varFiltroChoqueHorario =
            varFiltroChoqueHorario.Replace(" ((flPodeChocarHorario = 'true') or " +
                                          "   ((flPodeChocarHorario = 'false') and not " +
                                          "    (((dtIni >= '" + DateTime.Parse(grdAtvParticipante.DataKeys[e.RowIndex].Values[4].ToString()).ToString("dd/MM/yyyy HH:mm") + "') and " +
                                          "      (dtIni <= '" + DateTime.Parse(grdAtvParticipante.DataKeys[e.RowIndex].Values[5].ToString()).ToString("dd/MM/yyyy HH:mm") + "') " + wrk_dsturno + " ) or " +
                                          "     ((dtTermino >= '" + DateTime.Parse(grdAtvParticipante.DataKeys[e.RowIndex].Values[4].ToString()).ToString("dd/MM/yyyy HH:mm") + "') and " +
                                          "      (dtTermino <= '" + DateTime.Parse(grdAtvParticipante.DataKeys[e.RowIndex].Values[5].ToString()).ToString("dd/MM/yyyy HH:mm") + "') " + wrk_dsturno + " ) or " +

                                          "     ((dtIni <= '" + DateTime.Parse(grdAtvParticipante.DataKeys[e.RowIndex].Values[4].ToString()).ToString("dd/MM/yyyy HH:mm") + "') and " +
                                          "      (dtIni <= '" + DateTime.Parse(grdAtvParticipante.DataKeys[e.RowIndex].Values[5].ToString()).ToString("dd/MM/yyyy HH:mm") + "') and " +
                                          "      (dtTermino >= '" + DateTime.Parse(grdAtvParticipante.DataKeys[e.RowIndex].Values[4].ToString()).ToString("dd/MM/yyyy HH:mm") + "') and " +
                                          "      (dtTermino >= '" + DateTime.Parse(grdAtvParticipante.DataKeys[e.RowIndex].Values[5].ToString()).ToString("dd/MM/yyyy HH:mm") + "') " + wrk_dsturno + " )) " +
                                          "))", "");
           
            if ((varFiltroChoqueHorario.Trim() != "") && (varFiltroChoqueHorario.Substring(0, 4) == " and"))
                varFiltroChoqueHorario = varFiltroChoqueHorario.Remove(0, 4);

            Session["varFiltroChoqueHorario"] = varFiltroChoqueHorario;

            oDTAtividadesParticipante.Rows.RemoveAt(e.RowIndex);

            calcularValores();

            grdAtvParticipante.DataSource = oDTAtividadesParticipante.DefaultView;
            grdAtvParticipante.DataBind();

            vlItens.Text = grdAtvParticipante.Rows.Count.ToString();

            prpFiltrarAtividades();

            if (grdAtvParticipante.Rows.Count <= 0)
            {
                //btnAvancar.Visible = false;
                //btnVerItensPedido.Text = "Ver Itens Pedido";
                //btnVerItensPedido.Visible = true;
                //grdAtv.Visible = true;
                //grdAtvParticipante.Visible = false;
                //btnVoltarParaItens.Visible = false;
            }
            else if (grdAtvParticipante.Rows.Count == 1)
            {
                if (Boolean.Parse(grdAtvParticipante.DataKeys[0].Values[15].ToString().Trim()))//inscricao obrigatoria
                {
                    //btnAvancar.Visible = false;
                    //btnVerItensPedido.Text = "Ver Itens Pedido";
                    //btnVerItensPedido.Visible = true;
                    //grdAtv.Visible = true;
                    //grdAtvParticipante.Visible = false;
                    //btnVoltarParaItens.Visible = false;
                }
            }

            if (SessionEvento.CdEvento != "012601")
                prpCalcularDescontosPorTipoAtividade();
            else
            {
                if ((SessionIdioma == "PTBR") && (SessionParticipante.NoPais == "BRASIL"))
                    prpCalcularDescontosPorTipoAtividade_012601();
                else
                    prpCalcularDescontosPorTipoAtividade_012601_ENUS();
            }
        }
    }

    protected void grdAtv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((!SessionEvento.FlEventoComRecebimentos) && (!SessionParticipante.Categoria.FlPagamento))
            {

                ImageButton MyButton = (ImageButton)e.Row.FindControl("btnIncluir");
                if (SessionIdioma == "PTBR")
                    MyButton.Attributes.Add("onclick", "javascript:return " + "confirm('Confirma a inscrição do Item?')");
                else if (SessionIdioma == "ENUS")
                    MyButton.Attributes.Add("onclick", "javascript:return " + "confirm('Confirm enrollment Item?')");
                else if (SessionIdioma == "ESP")
                    MyButton.Attributes.Add("onclick", "javascript:return " + "confirm('¿Confirmar la inscripción?')");
                else if (SessionIdioma == "FRA")
                    MyButton.Attributes.Add("onclick", "javascript:return " + "confirm('Confirmer Point d`inscription?')");
            }

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
                lblvagasItem.Text = "Vagas: ";
                lbllocalItem.Text = "Local: ";

                lblvalorItem.Text = "Valor ";
                lbldescItem.Text = "Desconto ";
                lblqtdItem.Text = "Quantidade ";
                lblvlTotalItem.Text = "Vlr Total (R$)";

                if ((SessionParticipante.NoPais == "BRASIL") ||
                (SessionParticipante.NoPais == "BRAZIL") ||
                (SessionParticipante.NoPais == "BRASIL") ||
                (SessionParticipante.NoPais == "BRÉSIL"))
                    lblvlTotalItem.Text = "Vlr Total (R$)";
                else
                    lblvlTotalItem.Text = "Vlr Total ($)";
            }
            else if (SessionIdioma == "ENUS")
            {
                lbltpItem.Text = "Type: ";
                 lbldeItem.Text = "From: ";
                 lblateItem.Text = " to: ";
                 lblvagasItem.Text = "Limit: ";
                 lbllocalItem.Text = "Site: ";

                 lblvalorItem.Text = "Price ";
                 lbldescItem.Text = "Discount ";
                 lblqtdItem.Text = "Amount ";
                 lblvlTotalItem.Text = "Total($) ";

                 //if ((SessionParticipante.NoPais == "BRASIL") ||
                 //(SessionParticipante.NoPais == "BRAZIL") ||
                 //(SessionParticipante.NoPais == "BRASIL") ||
                 //(SessionParticipante.NoPais == "BRÉSIL"))
                 //    lblvlTotalItem.Text = "Total (R$)";
                 //else
                 //    lblvlTotalItem.Text = "Total ($)";
            }
            else if (SessionIdioma == "ESP")
            {
                lbltpItem.Text = "Tipo: ";
                 lbldeItem.Text = "De: ";
                 lblateItem.Text = " a: ";
                 lblvagasItem.Text = "Vacantes: ";
                 lbllocalItem.Text = "Local: ";

                 lblvalorItem.Text = "Precio ";
                 lbldescItem.Text = "Descuento ";
                 lblqtdItem.Text = "Cantidad ";
                 lblvlTotalItem.Text = "Total($) ";

                 //if ((SessionParticipante.NoPais == "BRASIL") ||
                 //(SessionParticipante.NoPais == "BRAZIL") ||
                 //(SessionParticipante.NoPais == "BRASIL") ||
                 //(SessionParticipante.NoPais == "BRÉSIL"))
                 //    lblvlTotalItem.Text = "Total (R$)";
                 //else
                 //    lblvlTotalItem.Text = "Total ($)";
            }
            else if (SessionIdioma == "FRA")
            {
                lbltpItem.Text = "Type: ";
                lbldeItem.Text = "De: ";
                lblateItem.Text = " à: ";
                lblvagasItem.Text = "Limiter: ";
                lbllocalItem.Text = "Local: ";

                lblvalorItem.Text = "Price ";
                lbldescItem.Text = "Réduction ";
                lblqtdItem.Text = "Montant ";
                lblvlTotalItem.Text = "Total ";
            }

            if ((SessionEvento.CdCliente == "0003") || (SessionEvento.CdEvento == "005102"))
            {
                lbldtini.Visible = false;
                lbldttermino.Visible = false;              
                lbldeItem.Visible = false;                
                lblateItem.Visible = false;                
                lblvagasItem.Visible = false;               
                lblVagas.Visible = false;                
                lbllocalItem.Visible = false;                
                lblLocal.Visible = false;

            }

            

            if (SessionEvento.CdCliente == "0016")
            {
                lbldtini.Visible = false;
                lbldttermino.Visible = false;
                lbldeItem.Visible = false;
                lblateItem.Visible = false;
                lblvagasItem.Visible = false;
                lblVagas.Visible = false;
                //lbllocalItem.Visible = false;
                //lblLocal.Visible = false;
                if ((SessionParticipante != null) && (SessionParticipante.CdCategoria == "00160301"))
                {
                    Panel pnlresumovlritens = (Panel)e.Row.FindControl("pnlResumoVlrItens");
                    pnlresumovlritens.Visible = false;
                }

            }

            if (SessionEvento.CdEvento == "003401")
            {
                lbldtini.Visible = false;
                lbldttermino.Visible = false;
                lbldeItem.Visible = false;
                lblateItem.Visible = false;
                lblvagasItem.Visible = false;
                lblVagas.Visible = false;
                lbllocalItem.Visible = false;
                lblLocal.Visible = false;

            }

            if (SessionEvento.CdEvento == "001405")
            {
                if (grdAtv.DataKeys[e.Row.RowIndex].Values[7].ToString() == "001405002")
                {
                    lbldtini.Visible = false;
                    lbldttermino.Visible = false;
                    lbldeItem.Visible = false;
                    lblateItem.Visible = false;
                    lblvagasItem.Visible = false;
                    lblVagas.Visible = false;
                    lbllocalItem.Visible = false;
                    lblLocal.Visible = false;
                }
            }

            if (SessionEvento.CdEvento == "006501")
            {
                lbltpItem.Text = "Macrogrupo";
            }
            
            Label lblatv = (Label)e.Row.FindControl("lblVlAtividade");
            Label lbldec = (Label)e.Row.FindControl("lblVlDescontoReal");
            lbldec.Text = decimal.Parse(lbldec.Text).ToString("N2");
            lblatv.Font.Strikeout = (decimal.Parse(lbldec.Text) > 0);
            Label lbltotalinsc = (Label)e.Row.FindControl("lblVlTotInscri");

            Image imgprofatv = (Image)e.Row.FindControl("imgAtivProf");
            imgprofatv.Visible = (imgprofatv.ImageUrl != "");

            CheckBox chkSelecionado = (CheckBox)e.Row.FindControl("chkSelecionado");
            chkSelecionado.Checked = cdAtiv.Contains(grdAtv.DataKeys[e.Row.RowIndex].Values[7].ToString());
            
            
            CheckBox chkflrequerqtd = (CheckBox)e.Row.FindControl("chkRequerQTD");
            Label lblvlquantidade = (Label)e.Row.FindControl("lblVlQuantidade");
            TextBox txtvlquantidade = (TextBox)e.Row.FindControl("txtVlQuantidade");            

            lblvlquantidade.Visible = !chkflrequerqtd.Checked;
            txtvlquantidade.Visible = chkflrequerqtd.Checked;

            txtvlquantidade.Rows = e.Row.RowIndex;//.ToString();
            txtvlquantidade.AutoPostBack = true;
            //txtvlquantidade..TextChanged += new System.EventHandler(this.txtVlQuantidade_TextChanged);


            if ((oDTAtividadesParticipante != null) && (chkflrequerqtd.Checked) && (chkSelecionado.Checked))
            {
                DataRow[] dr = oDTAtividadesParticipante.Select("cdAtividade = '" + grdAtv.DataKeys[e.Row.RowIndex].Values[7].ToString() + "'");
                //oDTAtividadesParticipante.DefaultView.Sort = "cdAtividade";
                string tmpCdAtividade = grdAtv.DataKeys[e.Row.RowIndex].Values[7].ToString();
               // DataRowView[] dr = oDTAtividadesParticipante.DefaultView.FindRows(grdAtv.DataKeys[e.Row.RowIndex].Values[7].ToString());
                if (dr.Length > 0)
                {
                    txtvlquantidade.Text = dr[0][19].ToString();

                    lbltotalinsc.Text = ((decimal.Parse(lblatv.Text) - decimal.Parse(lbldec.Text)) * decimal.Parse(txtvlquantidade.Text)).ToString("N2");
                }
            }

            ImageButton btnincluir = (ImageButton)e.Row.FindControl("btnIncluir");
            

            
            btnincluir.ImageUrl = "~/img/atv_euquero.png";
            if ((!SessionEvento.FlEventoComRecebimentos) || (SessionEvento.CdCliente == "0029") || (SessionEvento.CdCliente == "0013") || (SessionEvento.CdCliente == "0003") || (SessionEvento.CdEvento == "002902") || (SessionEvento.CdEvento == "001602") || (SessionEvento.CdEvento == "003401"))
            {

                //btnincluir.ImageUrl = "~/img/accept18x18.png";

                Panel pnlresumovlritens = (Panel)e.Row.FindControl("pnlResumoVlrItens");
                pnlresumovlritens.Visible = false;
            }

            if (SessionEvento.CdEvento == "005102")
                btnincluir.ImageUrl = "~/imagensgeral/desejo_participar.png";


            if (chkSelecionado.Checked)
            {
                btnincluir.ImageUrl = "~/img/atv_remover2.png";
                //btn.CssClass = "fa fa-check-square-o";
                //btn.Text = "\xF135";
                //btnincluir.ImageUrl = "~/img/check-square_18_x_18.png";
            }

            if (verficarChoqueHorario(
                    grdAtv.DataKeys[e.Row.RowIndex].Values[7].ToString(),
                    lbldtini.Text, 
                    lbldttermino.Text, 
                    grdAtv.DataKeys[e.Row.RowIndex].Values[22].ToString()))
            {
            
                btnincluir.ImageUrl = "~/img/atv_conflito.png";
                //btnincluir.ImageUrl ="~/img/uncheck-square_18_x_18.png";
               
            }

            if (int.Parse(lblVagas.Text) <= 0)
            {
                //imgprofatv.Visible = true;
                //if (SessionIdioma == "PTBR")
                btnincluir.ImageUrl = "~/imagensgeral/vagas_encerradas_96x37_" + SessionIdioma + ".png";
                //else if (SessionIdioma == "ENUS")
                //    btnincluir.ImageUrl = "https://inscricoesweb.fazendomais.com/imagensgeral/vagas_encerradas_96x37_EN.png";

            }

            if ((SessionEvento.CdEvento == "009902") || (SessionEvento.CdEvento == "011202"))
            {
                chkSelecionado.Visible = btnincluir.Visible = false;
            }
        }
    }

    protected void grdAtvParticipante_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (!SessionEvento.FlEventoComRecebimentos)
            {
                ImageButton MyButton = (ImageButton)e.Row.FindControl("btnRemove");
                
                if (SessionIdioma == "PTBR")
                    MyButton.Attributes.Add("onclick", "javascript:return " + "confirm('Confirma a exclusão do Item?')");
                else if (SessionIdioma == "ENUS")
                    MyButton.Attributes.Add("onclick", "javascript:return " + "confirm('Confirm the exclusion of the Item?')");
                else if (SessionIdioma == "ESP")
                    MyButton.Attributes.Add("onclick", "javascript:return " + "confirm('¿Confirme la eliminación de elemento?')");
                else if (SessionIdioma == "FRA")
                    MyButton.Attributes.Add("onclick", "javascript:return " + "confirm('Confirmez la suppression de l'article?')");
            }

            Label lbldtini = (Label)e.Row.FindControl("lblDtIni");
            lbldtini.Text = DateTime.Parse(lbldtini.Text).ToString("dd/MM/yyyy HH:mm");
            Label lbldttermino = (Label)e.Row.FindControl("lblDtTermino");
            lbldttermino.Text = DateTime.Parse(lbldttermino.Text).ToString("dd/MM/yyyy HH:mm");

            Label lbltpItem = (Label)e.Row.FindControl("lblTpItem");
            Label lbldeItem = (Label)e.Row.FindControl("lblDeItem");
            Label lblateItem = (Label)e.Row.FindControl("lblAteItem");
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

                //if ((SessionParticipante.NoPais == "BRASIL") ||
                //(SessionParticipante.NoPais == "BRAZIL") ||
                //(SessionParticipante.NoPais == "BRASIL") ||
                //(SessionParticipante.NoPais == "BRÉSIL"))
                //    lblvlTotalItem.Text = "Vlr Total (R$)";
                //else
                //    lblvlTotalItem.Text = "Vlr Total ($)";
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
                lblvlTotalItem.Text = "Total($) ";

                //if ((SessionParticipante.NoPais == "BRASIL") ||
                //(SessionParticipante.NoPais == "BRAZIL") ||
                //(SessionParticipante.NoPais == "BRASIL") ||
                //(SessionParticipante.NoPais == "BRÉSIL"))
                //    lblvlTotalItem.Text = "Total (R$)";
                //else
                    //lblvlTotalItem.Text = "Total ($)";
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
                lblvlTotalItem.Text = "Total($) ";

                //if ((SessionParticipante.NoPais == "BRASIL") ||
                //(SessionParticipante.NoPais == "BRAZIL") ||
                //(SessionParticipante.NoPais == "BRASIL") ||
                //(SessionParticipante.NoPais == "BRÉSIL"))
                //    lblvlTotalItem.Text = "Total (R$)";
                //else
                //    lblvlTotalItem.Text = "Total ($)";
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

            if ((SessionEvento.CdCliente == "0003") || (SessionEvento.CdEvento == "005102"))
            {
                lbldtini.Visible = false;
                lbldttermino.Visible = false;
                lbldeItem.Visible = false;
                lblateItem.Visible = false;
                //lblVagas.Visible = false;
                lbllocalItem.Visible = false;
                lblLocal.Visible = false;

            }


            if (SessionEvento.CdCliente == "0065")
            {
                Label lblCdAtvSel = (Label)e.Row.FindControl("lblCdAtvSel");
                if (lblCdAtvSel.Text != "006501001")
                {
                    lbltpItem.Text = "Macrogrupo";
                    lbldtini.Visible = false;
                    lbldttermino.Visible = false;
                    lbldeItem.Visible = false;
                    lblateItem.Visible = false;
                }
                else
                {
                    lbltpItem.Visible = false;
                    lbldtini.Text = DateTime.Parse(lbldtini.Text).ToString("dd/MM/yyyy");
                    lbldttermino.Text = DateTime.Parse(lbldttermino.Text).ToString("dd/MM/yyyy");
                }

            }
            

            if (SessionEvento.CdCliente == "0016")
            {
                lbldtini.Visible = false;
                lbldttermino.Visible = false;
                lbldeItem.Visible = false;
                lblateItem.Visible = false;
                //lblVagas.Visible = false;
                //lbllocalItem.Visible = false;
                //lblLocal.Visible = false;
                if ((SessionParticipante != null) && (SessionParticipante.CdCategoria == "00160301"))
                {
                    Panel pnlresumovlritens = (Panel)e.Row.FindControl("pnlResumoVlrItens");
                    pnlresumovlritens.Visible = false;
                }

            }

            if (SessionEvento.CdEvento == "003401")
            {
                lbldtini.Visible = false;
                lbldttermino.Visible = false;
                lbldeItem.Visible = false;
                lblateItem.Visible = false;
                //lblVagas.Visible = false;
                lbllocalItem.Visible = false;
                lblLocal.Visible = false;

            }

            if (SessionEvento.CdEvento == "001405")
            {
                if (grdAtvParticipante.DataKeys[e.Row.RowIndex].Values[6].ToString() == "001405002")
                {
                    lbldtini.Visible = false;
                    lbldttermino.Visible = false;
                    lbldeItem.Visible = false;
                    lblateItem.Visible = false;
                    //lblvagasItem.Visible = false;
                    //lblVagas.Visible = false;
                    lbllocalItem.Visible = false;
                    lblLocal.Visible = false;
                }
            }

            Label lblatv = (Label)e.Row.FindControl("lblVlAtividade");
            Label lbldec = (Label)e.Row.FindControl("lblVlDescontoReal");
            lbldec.Text = decimal.Parse(lbldec.Text).ToString("N2");
            lblatv.Font.Strikeout = (decimal.Parse(lbldec.Text) > 0);

            Image imgprofatv = (Image)e.Row.FindControl("imgAtivProf");
            imgprofatv.Visible = (imgprofatv.ImageUrl != "");
            

            CheckBox chkflrequerqtd = (CheckBox)e.Row.FindControl("chkRequerQTD");
            Label lblvlquantidade = (Label)e.Row.FindControl("lblVlQuantidade");
            TextBox txtvlquantidade = (TextBox)e.Row.FindControl("txtVlQuantidade");

            lblvlquantidade.Visible = !chkflrequerqtd.Checked;
            txtvlquantidade.Visible = chkflrequerqtd.Checked;

            txtvlquantidade.Rows = e.Row.RowIndex;//.ToString();
            txtvlquantidade.AutoPostBack = true;
            //txtvlquantidade..TextChanged += new System.EventHandler(this.txtVlQuantidade_TextChanged);

            if ((!SessionEvento.FlEventoComRecebimentos) || (SessionEvento.CdCliente == "0029") || (SessionEvento.CdCliente == "0013") || (SessionEvento.CdCliente == "0003") || (SessionEvento.CdEvento == "002902") || (SessionEvento.CdEvento == "001602") || (SessionEvento.CdEvento == "003401"))
            {
                Panel pnlresumovlritens = (Panel)e.Row.FindControl("pnlResumoVlrItens");
                pnlresumovlritens.Visible = false;
            }
        }
        
    }
    

    protected void btnAvancar_Click1(object sender, EventArgs e)
    {
        mensagem.Visible = false;
        lblMsg.Text = "";
        if (grdAtvParticipante.Rows.Count <= 0)
        {
            // ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Nenhum item selecionado!'); </script>", false);
            
            if (SessionIdioma == "PTBR")
                lblMsg.Text = "Nenhum item selecionado!";
            else if (SessionIdioma == "ENUS")
                lblMsg.Text = "No item selected!";
            else if (SessionIdioma == "ESP")
                lblMsg.Text = "¡Ninguna selección!";
            else if (SessionIdioma == "FRA")
                lblMsg.Text = "Aucun élément sélectionné!";

            mensagem.Visible = true;
            return;
        }

        if ((SessionEvento.CdCliente == "0003") && (int.Parse(vlItens.Text) < 6))//CONSAD -- total minima de atividade para o participante atingido
        {
            //lblMsg.Text = "";
            //ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Deve ser selecionado no mínimo 5 painéis!'); </script>", false);
            lblMsg.Text = "Deve ser selecionado no mínimo 5 painéis!";
            mensagem.Visible = true;
            return;
        }

        if ((SessionEvento.CdCliente == "0016") && (int.Parse(vlItens.Text) == 1) && (decimal.Parse(vlTotalPedido.Text) == 0) && (decimal.Parse(vlTotalDesc.Text) == 0))
        {
            lblMsg.Text = "Deve ser selecionado pelo menos uma atividade!";
            mensagem.Visible = true;
            return;
        }

        if ((SessionEvento.CdCliente == "0065") && (int.Parse(vlItens.Text) == 1) && (decimal.Parse(vlTotalPedido.Text) == 0) && (decimal.Parse(vlTotalDesc.Text) == 0))
        {
            lblMsg.Text = "Deve ser selecionado um Grupo de participação!";
            mensagem.Visible = true;
            return;
        }

        if ((SessionCategoria.FlConfirmacaoCadWeb) || ((SessionEvento.FlEventoComRecebimentos) && (decimal.Parse(vlTotalPedido.Text) > 0)))//pode entrar com conf e com receb/ sem conf e com receb / com conf e sem receb
        {
            #region Com confirmação ou (com recebimento e com valor maior que zero)

            DateTime? dtVencPedido = Geral.datahoraServidor(SessionCnn).AddDays(SessionEvento.NuDiasPrimeiroVencimento);
                //SessionEvento.DtFechamentoInscrWeb;

            //if (SessionEvento.CdCliente == "0014")
            //    dtVencPedido = Geral.datahoraServidor(SessionCnn).AddDays(5);
            
            decimal vlPedidoTotal = 0;

            for (int i = 0; i < oDTAtividadesParticipante.Rows.Count; i++)
            {
                if (oDTAtividadesParticipante.DefaultView[i]["dtValidade"].ToString() != "")
                {
                    if (dtVencPedido > DateTime.Parse(oDTAtividadesParticipante.DefaultView[i]["dtValidade"].ToString()))
                        dtVencPedido = DateTime.Parse(oDTAtividadesParticipante.DefaultView[i]["dtValidade"].ToString());

                    
                }

                vlPedidoTotal +=
                        ((decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlAtividade"].ToString()) -
                          decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlDesconto"].ToString())) *
                          decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlQuantidade"].ToString()));
            }

            DateTime dtMenorVencDesconto = DescontoCad.datahoraMenorVencimentoDesconto(SessionEvento.CdEvento, SessionCnn);
            if ((dtMenorVencDesconto != null) && (dtVencPedido > dtMenorVencDesconto))
                dtVencPedido = dtMenorVencDesconto;

            if ((SessionPedido == null) && (lblNrPedido.Text == ""))
            {
                //SessionPedido = new Pedido(SessionEvento.CdEvento, SessionParticipante.CdParticipante, lblNrPedido.Text, "0", null, decimal.Parse(vlTotalPedido.Text), false, true, "", "", "", "", "", "", "", "", "", dtVencPedido, 1, "", "", "", "");
                SessionPedido = new Pedido();

                SessionPedido.CdEvento = SessionEvento.CdEvento;
                SessionPedido.CdParticipante = SessionParticipante.CdParticipante;
                SessionPedido.CdPedido = lblNrPedido.Text;
                SessionPedido.TpPedido = "0";
                SessionPedido.DtPedido = null;
                SessionPedido.VlTotalPedido = vlPedidoTotal;// decimal.Parse(vlTotalPedido.Text);
                SessionPedido.FlPago = false;
                SessionPedido.FlAtivo = true;
                SessionPedido.TpPagamento = "";
                SessionPedido.DsNomeRecibo = "";
                SessionPedido.NuCEPRecibo = "";
                SessionPedido.DsEnderecoRecibo = "";
                SessionPedido.NoBairroRecibo = "";
                SessionPedido.NoCidadeRecibo = "";
                SessionPedido.DsUFRecibo = "";
                SessionPedido.NuCPFCNPJRecibo = "";
                SessionPedido.DsInscricaoEstadualRecibo = "";
                SessionPedido.DtVencimentoPedido = dtVencPedido;
                SessionPedido.QtdParcelas = 1;
                SessionPedido.NoBandeira = "";
                SessionPedido.DsComplementoEnderRecibo = "";
                SessionPedido.TpPessoa = "";
                SessionPedido.CdTransacaoOutrosTpPgto = "";
                SessionPedido.NoMoeda = NoMoeda;
                SessionPedido.VlConversao = VlConversao;

                //SessionPedido = oPedidoCad.Gravar(SessionPedido, SessionCnn);

            }
            else
            {
                SessionPedido = oPedidoCad.Pesquisar(SessionEvento.CdEvento, lblNrPedido.Text, SessionCnn);

                SessionPedido.CdPedido = lblNrPedido.Text;
                SessionPedido.VlTotalPedido = decimal.Parse(vlTotalPedido.Text);
                SessionPedido.FlPago = false;
                SessionPedido.FlAtivo = true;
                SessionPedido.TpPagamento = (!SessionEvento.FlPermitirEditarPedido) ? "" : SessionPedido.TpPagamento;
                SessionPedido.DtVencimentoPedido = dtVencPedido;
                SessionPedido.NoMoeda = NoMoeda;
                SessionPedido.VlConversao = VlConversao;
            }

            SessionPedido = oPedidoCad.Gravar(SessionPedido, SessionCnn);
            if (SessionPedido == null)
            {
                Response.Redirect("frm_mensagens.aspx?cdMensagem=012&dsMensagem=", false);
                //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                //           "012",
                //           ""), true);
                return;
            }
            Session["SessionPedido"] = SessionPedido;
            if (oPedidoCad.ApagarTodasAtividadePedido(SessionPedido, SessionCnn))
            {

                for (int i = 0; i < oDTAtividadesParticipante.Rows.Count; i++)
                {
                    if (SessionEvento.CdCliente != "0003")
                    {
                        if (!oPedidoCad.GravarAtividadePedido(
                                SessionPedido.CdPedido,
                                oDTAtividadesParticipante.DefaultView[i]["cdAtividade"].ToString(),
                                decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlAtividade"].ToString()),
                                decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlDesconto"].ToString()),
                                int.Parse(oDTAtividadesParticipante.DefaultView[i]["vlQuantidade"].ToString()),
                                SessionCnn))
                        {
                            Response.Redirect("frm_mensagens.aspx?cdMensagem=012&dsMensagem=", false);
                            //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                            //        "012",
                            //        ""), true);
                            return;
                        }
                    }
                    else
                    {
                        if ((oDTAtividadesParticipante.DefaultView[i]["cdAtividade"].ToString() != "000305052") &&
                            (oDTAtividadesParticipante.DefaultView[i]["cdAtividade"].ToString() != "000305053") &&
                            (oDTAtividadesParticipante.DefaultView[i]["cdAtividade"].ToString() != "000305054") &&
                            (oDTAtividadesParticipante.DefaultView[i]["cdAtividade"].ToString() != "000306054"))
                        {
                            if (!oPedidoCad.GravarAtividadePedido(
                                    SessionPedido.CdPedido,
                                    oDTAtividadesParticipante.DefaultView[i]["cdAtividade"].ToString(),
                                    decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlAtividade"].ToString()),
                                    decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlDesconto"].ToString()),
                                    int.Parse(oDTAtividadesParticipante.DefaultView[i]["vlQuantidade"].ToString()),
                                    SessionCnn))
                            {
                                Response.Redirect("frm_mensagens.aspx?cdMensagem=012&dsMensagem=", false);
                                //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                //        "012",
                                //        ""), true);
                                return;
                            }
                        }
                    }
                }

                Inscricoes oInscricoes = new Inscricoes();
                int totalAcompanhante = oInscricoes.TotalDeAcompanhantesParaCadastro(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn) + oPedidoCad.TotalDeAcompanhantesParaCadastro(SessionEvento.CdEvento, SessionPedido.CdPedido, SessionCnn);
                if (totalAcompanhante <= 0)
                {
                    AcompanhanteCad oAcompanhanteCad = new AcompanhanteCad();
                    oAcompanhanteCad.ApagarAcompanhantes(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);

                }

                //if ((SessionEvento.CdEvento == "009902") || (SessionEvento.CdEvento == "011201"))
                //    GerarIngressos(SessionParticipante, SessionPedido.CdPedido, SessionCnn);

                if (!SessionEvento.FlEventoComRecebimentos)
                {
                    if (SessionCategoria.FlConfirmacaoCadWeb)
                    {
                        //enviar email
                        if (SessionEvento.FlEnviarMensagensEmail)
                        {
                            Geral oGeral = new Geral();

                            oGeral.EnviarEmailConfirmaCadastroConf(SessionEvento, SessionParticipante, SessionPedido, SessionCnn);
                        }
                        //-----
                        Response.Redirect("frm_mensagens.aspx?cdMensagem=013&dsMensagem=", false);
                        //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                        //            "013",
                        //            ""), true);
                    }
                }
                else
                {

                    Session["SessionPedido"] = SessionPedido;

                    if (SessionEvento.CdEvento != "001601") 
                    {
                        if (((SessionEvento.CdEvento == "001002") || (SessionEvento.CdEvento == "001004") || (SessionEvento.CdEvento == "001008"))//ABERT
                             || (SessionEvento.CdEvento == "000804"))
                        {

                            DataTable DTAcompanhantes;
                            AcompanhanteCad oAcompanhanteCad = new AcompanhanteCad();
                            DTAcompanhantes = oAcompanhanteCad.Listar(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);
                            
                            int totAcomp = oPedidoCad.TotalDeAcompanhantesParaCadastro(SessionPedido.CdEvento, SessionPedido.CdPedido, SessionCnn);

                            if ((DTAcompanhantes == null) || (DTAcompanhantes.Rows.Count < totAcomp))
                            {
                                if (totAcomp >= 1)
                                {
                                    Response.Redirect("frmAcompanhante.aspx?p=" + cllEventos.Crypto.EncryptStringAES(SessionPedido.CdPedido.Substring(15, 3)), false);
                                    //Server.Transfer(string.Format("frmAcompanhante.aspx?p={0}",
                                    //        cllEventos.Crypto.EncryptStringAES(SessionPedido.CdPedido.Substring(15, 3))), true);//
                                    return;
                                }
                            }
                        }

                        if (SessionEvento.CdEvento == "003002")//FJ
                        {
                            //SessionPedido.TpPagamento = "PAYPAL";
                            //SessionPedido = oPedidoCad.Gravar(SessionPedido, SessionCnn);
                            //Session["SessionPedido"] = SessionPedido;

                            Response.Redirect("frmPayPalPagamento.aspx?cdMatricula=" + SessionParticipante.CdParticipante, false);
                            //Server.Transfer(string.Format("frmPayPalPagamento.aspx?cdMatricula={0}",
                            //                            SessionParticipante.CdParticipante), true);
                        }

                        if ((SessionCategoria.FlConfirmacaoCadWeb) && (decimal.Parse(vlTotalPedido.Text) == 0))
                        {
                            
                            SessionPedido.TpPagamento = "CORTESIA";
                            SessionPedido = oPedidoCad.Gravar(SessionPedido, SessionCnn);

                            //enviar email
                            if (SessionEvento.FlEnviarMensagensEmail)
                            {
                                Geral oGeral = new Geral();

                                oGeral.EnviarEmailConfirmaCadastroConf(SessionEvento, SessionParticipante, SessionPedido, SessionCnn);
                            }
                            //-----

                            Response.Redirect("frm_mensagens.aspx?cdMensagem=013&dsMensagem=", false);
                            //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                            //            "013",
                            //            ""), true);
                        }

                        /*
                        if ((SessionEvento.CdCliente == "0003") && (int.Parse(SessionEvento.CdEvento) >= 305))
                        {
                            DataTable oDTCurso2 = oInscricoes.ListarAtividadesDoParticipante(SessionParticipante, "000305052", SessionCnn);
                            DataTable oDTCurso3 = oInscricoes.ListarAtividadesDoParticipante(SessionParticipante, "000305053", SessionCnn);
                            DataTable oDTCurso4 = oInscricoes.ListarAtividadesDoParticipante(SessionParticipante, "000305054", SessionCnn);

                            if ( ((oDTCurso2 == null) || (oDTCurso2.Rows.Count <= 0)) &&
                                 ((oDTCurso3 == null) || (oDTCurso3.Rows.Count <= 0)) &&
                                 ((oDTCurso4 == null) || (oDTCurso4.Rows.Count <= 0)))
                                Server.Transfer(string.Format("frmCONSADOutrasAtividades.aspx?cdMatricula={0}",
                                                            SessionParticipante.CdParticipante), true);
                            else
                                Server.Transfer(string.Format("frm_formapagamento.aspx?cdMatricula={0}",
                                                        SessionParticipante.CdParticipante), true);
                        }
                         * */

                        if ((SessionEvento.CdCliente == "0016") && (int.Parse(SessionEvento.CdEvento) >= 1604))
                        {
                            Response.Redirect("AtividadesAbrasel.aspx?cdMatricula=" + SessionParticipante.CdParticipante, false);
                            //Server.Transfer(string.Format("AtividadesAbrasel.aspx?cdMatricula={0}",
                            //                            SessionParticipante.CdParticipante), true);
                            return;
                        }

                        //Response.Redirect("frm_formapagamento.aspx?cdMatricula=" + SessionParticipante.CdParticipante, false);

                        if (((SessionEvento.FlEmiteRecibo)))// && (SessionIdioma == "PTBR")) ||
                            //((SessionEvento.FlEmiteRecibo) && (SessionParticipante.Categoria.NoCategoria.Contains("BRAZILIAN"))))
                            Response.Redirect("frmDadosReciboNF.aspx?cdMatricula=" + SessionParticipante.CdParticipante, false);
                        else
                            Response.Redirect("frm_formapagamento.aspx?cdMatricula=" + SessionParticipante.CdParticipante, false);
                        
                        //Server.Transfer(string.Format("frm_formapagamento.aspx?cdMatricula={0}",
                        //                                SessionParticipante.CdParticipante), true);
                        return;
                    }
                    else
                    {
                        SessionPedido.TpPagamento = "ITAU";
                        SessionPedido = oPedidoCad.Gravar(SessionPedido, SessionCnn);
                        Session["SessionPedido"] = SessionPedido;


                        string pedido = SessionPedido.CdPedido.Substring(10, 8);


                        string vlTotPedido = SessionPedido.VlTotalPedido.ToString("N2");
                        string dtVenc = DateTime.Parse(SessionPedido.DtVencimentoPedido.ToString()).ToString("ddMMyyyy");

                        string codEmp = "J0293638680001380000013107";
                        string chave = "4BRAS3LN4CI0N4L0";

                        Itaucripto.cripto crpt = new Itaucripto.cripto(); //Server.CreateObject("Itaucripto.cripto");

                        string dados = crpt.geraDados(codEmp, pedido, vlTotPedido, "", chave, SessionParticipante.NoParticipante, "01", SessionParticipante.NuCPFCNPJ, SessionParticipante.DsEndereco, SessionParticipante.NoBairro, SessionParticipante.NuCEP, SessionParticipante.NoCidade, SessionParticipante.DsUF, dtVenc, "", "", "", "");

                        //Response.Write("<script>window.open('frmPagItau.asp?prmDados=" + dados + "','_self');</script>");

                        Response.Redirect("frmPagamentoItau.aspx?prmDados=" + dados, false);
                        //Server.Transfer(string.Format("frmPagamentoItau.aspx?prmDados={0}",
                        //                                dados), true);

                        //Server.Transfer("frmPagamentoItau.aspx", true);

                        return;
                    }
                }
            }
            else
            {
                Response.Redirect("frm_mensagens.aspx?cdMensagem=012&dsMensagem=", false);
                //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                //       "012",
                //       ""), true);
                return;
            }

            #endregion
        }
        else
        {
            Boolean tmpENAPINHA = false;
            for (int i = 0; i < oDTAtividadesParticipante.Rows.Count; i++)
            {
                //gerar matricula
                if (SessionInscricoes.MatriculasGravar(SessionParticipante.CdEvento, SessionParticipante.CdParticipante, oDTAtividadesParticipante.DefaultView[i]["cdAtividade"].ToString(), 0, int.Parse(oDTAtividadesParticipante.DefaultView[i]["vlQuantidade"].ToString()), "000000001", SessionCnn))
                {
                    
                    if ((SessionEvento.CdEvento == "002902")//ACONCHEGO
                        && (oDTAtividadesParticipante.DefaultView[i]["cdAtividade"].ToString() == "002902024"))
                        tmpENAPINHA = true;
                    //enviar email

                    //-----
                    //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                    //               "002",
                    //               ""), true);
                }
                else
                {
                    Response.Redirect("frm_mensagens.aspx?cdMensagem=012&dsMensagem=", false);
                    //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                    //               "012",
                    //               ""), true);
                    return;
                }
                //-----

            }
            

            //enviar e-mail para participante
            if ((SessionEvento.FlEnviarMensagensEmail) && (SessionParticipante.Categoria.FlEnviarEmail))
            {
                Geral oGeral = new Geral();
               // if (SessionEvento.CdEvento != "007702")
                    oGeral.EnviarEmailConfirmaCadastro(SessionEvento, SessionParticipante, SessionCnn);
                //else
                //{
                //    if (SessionParticipante.DsUF != "DF")
                //        oGeral.EnviarEmailConfirmaCadastro(SessionEvento, SessionParticipante, SessionCnn);
                //}
            }


            if ((SessionEvento.CdEvento == "002902")//ACONCHEGO
                && (tmpENAPINHA == true))
            {

                DataTable DTAcompanhantes;
                AcompanhanteCad oAcompanhanteCad = new AcompanhanteCad();
                DTAcompanhantes = oAcompanhanteCad.Listar(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);

                
                if (DTAcompanhantes == null)
                {
                    Response.Redirect("frmAcompanhante.aspx?Z=", false);
                    //Server.Transfer(string.Format("frmAcompanhante.aspx?Z={0}",""), true);//     
                    return;
                }
            }

            //if (SessionEvento.CdEvento == "001604")
            //{
            //    Response.Redirect("AtividadesAbrasel.aspx", false);
            //    //Server.Transfer("AtividadesAbrasel.aspx", true);
            //}
            if ((SessionEvento.CdCliente == "0016") && (int.Parse(SessionEvento.CdEvento) >= 1604))
            {
                Response.Redirect("AtividadesAbrasel.aspx?cdMatricula=" + SessionParticipante.CdParticipante, false);
                //Server.Transfer(string.Format("AtividadesAbrasel.aspx?cdMatricula={0}",
                //                            SessionParticipante.CdParticipante), true);
                return;
            }

            Response.Redirect("frm_mensagens.aspx?cdMensagem=014&dsMensagem=", false);

            //-----
            //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
            //    "014",
            //    ""), true);

            return;
        }
    }
    protected void btnVerItensPedido_Click(object sender, EventArgs e)
    {
        mensagem.Visible = false;
        if (grdAtvParticipante.Rows.Count <= 0)
        {            
            if (SessionIdioma == "PTBR")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Nenhum item selecionado!'); </script>", false);
            else if (SessionIdioma == "ENUS")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('No item selected!'); </script>", false);
            else if (SessionIdioma == "ESP")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('¡Ninguna selección!'); </script>", false);
            else if (SessionIdioma == "FRA")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Aucun élément sélectionné!'); </script>", false);
            return;
        }

        if ((SessionEvento.CdCliente == "0016") && (int.Parse(vlItens.Text) == 1) && (decimal.Parse(vlTotalPedido.Text) == 0) && (decimal.Parse(vlTotalDesc.Text) == 0))
        {
            lblMsg.Text = "Deve ser selecionado pelo menos uma atividade!";
            mensagem.Visible = true;
            return;
        }

        //btnAvancar.Visible = true;
        //btnVerItensPedido.Visible = false;
        //grdAtv.Visible = false;
        //grdAtvParticipante.Visible = true;
        //btnVoltarParaItens.Visible = true;
        
        Session["varFiltroChoqueHorario"] = varFiltroChoqueHorario;
    }
    protected void txtVlQuantidade_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int.Parse((sender as TextBox).Text);
        }
        catch
        {
           
            if (SessionIdioma == "PTBR")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Quantidade inválida!'); </script>", false);
            else if (SessionIdioma == "ENUS")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Invalid number!'); </script>", false);
            else if (SessionIdioma == "ESP")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('¡Cantidad inválido!'); </script>", false);
            else if (SessionIdioma == "FRA")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Nombre incorrect!'); </script>", false);

            //return;
            (sender as TextBox).Text = "1";
        }

        if (((sender as TextBox).Text == "") || (int.Parse((sender as TextBox).Text) <= 0))
        {
            if (SessionIdioma == "PTBR")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Quantidade inválida!'); </script>", false);
            else if (SessionIdioma == "ENUS")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Invalid number!'); </script>", false);
            else if (SessionIdioma == "ESP")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('¡Cantidad inválido!'); </script>", false);
            else if (SessionIdioma == "FRA")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Nombre incorrect!'); </script>", false);
            //return;
            (sender as TextBox).Text = "1";
        }
        
        //int nrLinha = int.Parse(grdAtv.DataKeys[int.Parse((sender as TextBox).CssClass)].Values[19].ToString().Trim());
        //int qtdMaxima = int.Parse(grdAtv.DataKeys[int.Parse((sender as TextBox).CssClass)].Values[20].ToString().Trim());
        int nrLinha = int.Parse(grdAtv.DataKeys[(sender as TextBox).Rows].Values[19].ToString().Trim());
        int qtdMaxima = int.Parse(grdAtv.DataKeys[(sender as TextBox).Rows].Values[20].ToString().Trim());

        if (int.Parse((sender as TextBox).Text) > qtdMaxima)
        {
            if (SessionIdioma == "PTBR")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Quantidade máxima permitida é de " + qtdMaxima.ToString() + " por participante!'); </script>", false);
            else if (SessionIdioma == "ENUS")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Maximum allowed is " + qtdMaxima.ToString() + " per participant!'); </script>", false);
            else if (SessionIdioma == "ESP")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('¡Máximo permitido es de " + qtdMaxima.ToString() + " por participante!'); </script>", false);
            else if (SessionIdioma == "FRA")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Maximale autorisée est de " + qtdMaxima.ToString() + " par participant!'); </script>", false);
            //return;
            (sender as TextBox).Text = qtdMaxima.ToString();
        }


        
        int qtdVagasEmAberto = int.Parse(grdAtv.DataKeys[(sender as TextBox).Rows].Values[6].ToString().Trim());

        if (int.Parse((sender as TextBox).Text) > qtdVagasEmAberto)
        {
            if (SessionIdioma == "PTBR")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Quantidade ultrapassa o limite vagas!'); </script>", false);
            else if (SessionIdioma == "ENUS")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Quantity exceeds the limit vacancies!'); </script>", false);
            else if (SessionIdioma == "ESP")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('¡La cantidad supera el límite vacante!'); </script>", false);
            else if (SessionIdioma == "FRA")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('La quantité dépasse les limites des postes vacants!'); </script>", false);
            //return;
            (sender as TextBox).Text = qtdVagasEmAberto.ToString();
        }


        oDTAtividades.Rows[nrLinha]["vlQuantidade"] = (sender as TextBox).Text;
        oDTAtividades.Rows[nrLinha]["vlTotInscri"] = (decimal.Parse(oDTAtividades.Rows[nrLinha]["vlAtividade"].ToString()) -
                                                                                decimal.Parse(oDTAtividades.Rows[nrLinha]["vlDescontoReal"].ToString())) *
                                                                               int.Parse((sender as TextBox).Text);
        

        Session["oDTAtividades"] = oDTAtividades;


        int qtdDif = 0;
        decimal vlrATiv = 0;
        decimal vlrTotal = 0;
        decimal vlrDesc = 0;

        if ((oDTAtividadesParticipante != null))
        {
            DataRow[] dr = oDTAtividadesParticipante.Select("cdAtividade = '" + oDTAtividades.Rows[nrLinha]["cdAtividade"] + "'");
            //oDTAtividadesParticipante.DefaultView.Sort = "cdAtividade";
            //string tmpCdAtividade = grdAtv.DataKeys[e.Row.RowIndex].Values[7].ToString();
            // DataRowView[] dr = oDTAtividadesParticipante.DefaultView.FindRows(grdAtv.DataKeys[e.Row.RowIndex].Values[7].ToString());
            if (dr.Length > 0)
            {
                int nrLinha2 = int.Parse(dr[0]["nrLinha"].ToString());

               // qtdDif = int.Parse((sender as TextBox).Text) - int.Parse(oDTAtividadesParticipante.Rows[nrLinha2]["vlQuantidade"].ToString());

                //vlrDif = (decimal.Parse(oDTAtividades.Rows[nrLinha]["vlTotInscri"].ToString()) - decimal.Parse(oDTAtividadesParticipante.Rows[nrLinha2]["vlMatricula"].ToString()));


                vlrATiv =  (decimal.Parse(oDTAtividades.Rows[nrLinha]["vlAtividade"].ToString()) * int.Parse(oDTAtividades.Rows[nrLinha]["vlQuantidade"].ToString()));
                vlrDesc =  (decimal.Parse(oDTAtividades.Rows[nrLinha]["vlDescontoReal"].ToString()) * int.Parse((sender as TextBox).Text));
                vlrTotal =  (decimal.Parse(oDTAtividades.Rows[nrLinha]["vlAtividade"].ToString()) -
                                                                                        decimal.Parse(oDTAtividades.Rows[nrLinha]["vlDescontoReal"].ToString())) *
                                                                                       int.Parse(oDTAtividades.Rows[nrLinha]["vlQuantidade"].ToString());

                


                //if (int.Parse((sender as TextBox).Text) >= int.Parse(oDTAtividadesParticipante.Rows[nrLinha2]["vlQuantidade"].ToString()))
                //    vlrDescDif = vlrDescDif * (-1);

                oDTAtividadesParticipante.Rows[nrLinha2]["vlQuantidade"] = (sender as TextBox).Text;
                oDTAtividadesParticipante.Rows[nrLinha2]["vlMatricula"] = (decimal.Parse(oDTAtividades.Rows[nrLinha]["vlAtividade"].ToString()) -
                                                                                        decimal.Parse(oDTAtividades.Rows[nrLinha]["vlDescontoReal"].ToString())) *
                                                                                       int.Parse(oDTAtividades.Rows[nrLinha]["vlQuantidade"].ToString());
                Session["oDTAtividadesParticipante"] = oDTAtividadesParticipante;


                grdAtvParticipante.DataSource = oDTAtividadesParticipante.DefaultView;
                grdAtvParticipante.DataBind();


               

                //txtvlquantidade.Text = dr[0][19].ToString();

                //lbltotalinsc.Text = ((decimal.Parse(lblatv.Text) - decimal.Parse(lbldec.Text)) * decimal.Parse(txtvlquantidade.Text)).ToString("N2");

                
            }
        }

        grdAtv.DataSource = oDTAtividades.DefaultView;
        grdAtv.DataBind();

        Session["varFiltroChoqueHorario"] = varFiltroChoqueHorario;
        prpFiltrarAtividades();

        if (!cdAtiv.Contains(grdAtv.DataKeys[nrLinha].Values[7].ToString()))
        {
            GridViewSelectEventArgs ev = new GridViewSelectEventArgs(nrLinha);
            grdAtv_SelectedIndexChanging(sender, ev);
        }
        /*else if (int.Parse((sender as TextBox).Text) <= 0)
        {
            
                DataRow[] foundRows;
                foundRows = oDTAtividadesParticipante.Select("cdAtividade = '" + grdAtv.DataKeys[nrLinha].Values[7].ToString() + "'");

                grdAtvParticipante_removerlinha(foundRows[0]);


                //grdAtv.Rows[nrLinha].Cells[0].Focus();
                //return;
            
        }*/


        //vlTotalAtiv.Text = (vlrATiv).ToString("N2");// (decimal.Parse(vlTotalAtiv.Text) + (vlrDif)).ToString("N2");
        //vlTotalDesc.Text = (vlrDesc).ToString("N2");//(decimal.Parse(vlTotalDesc.Text) + (vlrDescDif)).ToString("N2");
        //vlTotalPedido.Text = (vlrTotal).ToString("N2"); //(decimal.Parse(vlTotalPedido.Text) + (vlrDif)).ToString("N2");

        if ((oDTAtividadesParticipante != null))
        {
            vlrATiv = 0;
            vlrDesc = 0;
            vlrTotal = 0;
            for (int i = 0; i < oDTAtividadesParticipante.Rows.Count; i++)
            {
                vlrATiv += (decimal.Parse(oDTAtividadesParticipante.Rows[i]["vlAtividade"].ToString()) * int.Parse(oDTAtividadesParticipante.Rows[i]["vlQuantidade"].ToString()));
                vlrDesc += (decimal.Parse(oDTAtividadesParticipante.Rows[i]["vlDesconto"].ToString()) * int.Parse(oDTAtividadesParticipante.Rows[i]["vlQuantidade"].ToString()));
                vlrTotal += (decimal.Parse(oDTAtividadesParticipante.Rows[i]["vlAtividade"].ToString()) -
                                                                                        decimal.Parse(oDTAtividadesParticipante.Rows[i]["vlDesconto"].ToString())) *
                                                                                       int.Parse(oDTAtividadesParticipante.Rows[i]["vlQuantidade"].ToString());

                
            }

            vlTotalAtiv.Text = (vlrATiv).ToString("N2");
            vlTotalDesc.Text = (vlrDesc).ToString("N2");
            vlTotalPedido.Text = (vlrTotal).ToString("N2"); 
        }
        
    }
    protected void txtVlQuantidade_TextChanged1(object sender, EventArgs e)
    {
        try
        {
            int.Parse((sender as TextBox).Text);
        }
        catch
        {

            if (SessionIdioma == "PTBR")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Quantidade inválida!'); </script>", false);
            else if (SessionIdioma == "ENUS")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Invalid number!'); </script>", false);
            else if (SessionIdioma == "ESP")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('¡Cantidad inválido!'); </script>", false);
            else if (SessionIdioma == "FRA")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Nombre incorrect!'); </script>", false);

            //return;
            (sender as TextBox).Text = "1";
        }

        if (((sender as TextBox).Text == "") || (int.Parse((sender as TextBox).Text) <= 0))
        {
            if (SessionIdioma == "PTBR")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Quantidade inválida!'); </script>", false);
            else if (SessionIdioma == "ENUS")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Invalid number!'); </script>", false);
            else if (SessionIdioma == "ESP")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('¡Cantidad inválido!'); </script>", false);
            else if (SessionIdioma == "FRA")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Nombre incorrect!'); </script>", false);
            //return;
            (sender as TextBox).Text = "1";
        }

        //int nrLinha = int.Parse(grdAtv.DataKeys[int.Parse((sender as TextBox).CssClass)].Values[19].ToString().Trim());
        //int qtdMaxima = int.Parse(grdAtv.DataKeys[int.Parse((sender as TextBox).CssClass)].Values[20].ToString().Trim());
        int nrLinha = int.Parse(grdAtv.DataKeys[(sender as TextBox).Rows].Values[19].ToString().Trim());
        int qtdMaxima = int.Parse(grdAtv.DataKeys[(sender as TextBox).Rows].Values[20].ToString().Trim());

        if (int.Parse((sender as TextBox).Text) > qtdMaxima)
        {
            if (SessionIdioma == "PTBR")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Quantidade máxima permitida é de " + qtdMaxima.ToString() + " por participante!'); </script>", false);
            else if (SessionIdioma == "ENUS")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Maximum allowed is " + qtdMaxima.ToString() + " per participant!'); </script>", false);
            else if (SessionIdioma == "ESP")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('¡Máximo permitido es de " + qtdMaxima.ToString() + " por participante!'); </script>", false);
            else if (SessionIdioma == "FRA")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Maximale autorisée est de " + qtdMaxima.ToString() + " par participant!'); </script>", false);
            //return;
            (sender as TextBox).Text = qtdMaxima.ToString();
        }

        oDTAtividadesParticipante.Rows[nrLinha]["vlQuantidade"] = (sender as TextBox).Text;
        oDTAtividadesParticipante.Rows[nrLinha]["vlMatricula"] = (decimal.Parse(oDTAtividadesParticipante.Rows[nrLinha]["vlAtividade"].ToString()) -
                                                                                decimal.Parse(oDTAtividadesParticipante.Rows[nrLinha]["vlDesconto"].ToString())) *
                                                                               int.Parse(oDTAtividadesParticipante.Rows[nrLinha]["vlQuantidade"].ToString());

        calcularValores();

        Session["oDTAtividadesParticipante"] = oDTAtividadesParticipante;

        grdAtvParticipante.DataSource = oDTAtividadesParticipante.DefaultView;
        grdAtvParticipante.DataBind();
        
        Session["varFiltroChoqueHorario"] = varFiltroChoqueHorario;
        prpFiltrarAtividades();
    }

    private void calcularValores()
    {

        if ((oDTAtividadesParticipante != null) && (oDTAtividadesParticipante.Rows.Count > 0))
        {
            
            vlTotalAtiv.Text = "0,00";
            vlTotalDesc.Text = "0,00";
            vlTotalPedido.Text = "0,00";



            for (int i = 0; i < oDTAtividadesParticipante.DefaultView.Count; i++)
            {

                vlTotalAtiv.Text = (decimal.Parse(vlTotalAtiv.Text) + (decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlAtividade"].ToString().Trim()) *
                                                                       int.Parse(oDTAtividadesParticipante.DefaultView[i]["vlQuantidade"].ToString()))
                                   ).ToString("N2");
                vlTotalDesc.Text = (decimal.Parse(vlTotalDesc.Text) + (decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlDesconto"].ToString().Trim()) *
                                                                       int.Parse(oDTAtividadesParticipante.DefaultView[i]["vlQuantidade"].ToString()))
                                   ).ToString("N2");
                vlTotalPedido.Text = (decimal.Parse(vlTotalPedido.Text) + decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlMatricula"].ToString().Trim())).ToString("N2");


            }


        }

    }



    protected void btnVoltarParaItens_Click(object sender, EventArgs e)
    {
       
        if (grdAtv.Rows.Count <= 0)
        {
            if (SessionEvento.CdCliente != "0013")
            {
                if (SessionIdioma == "PTBR")
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Não há mais itens para ser selecionado!'); </script>", false);
                else if (SessionIdioma == "ENUS")
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('No more items to be selected!'); </script>", false);
                else if (SessionIdioma == "ESP")
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('¡No hay más elementos para su selección!'); </script>", false);
                else if (SessionIdioma == "FRA")
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Aucun élément de plus pour être sélectionnés!'); </script>", false);
            }
            else //conasems
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Devido ao choque de horários não há mais atividade(s) para ser(em) selecionada(s)!'); </script>", false);
            return;
        }
        
        //btnAvancar.Visible = false;
        //btnVerItensPedido.Visible = true;
        //grdAtv.Visible = true;
        //grdAtvParticipante.Visible = false;
        //btnVoltarParaItens.Visible = false;    
        
        Session["varFiltroChoqueHorario"] = varFiltroChoqueHorario;
    }


    protected void preencherMsgExtra(string prmCdEvento, string prmCdCategoria)
    {
        if (prmCdEvento == "006501")
        {
            MsgExtra.Visible = true;
            lblMsgExtra.Font.Size = 14;
            lblMsgExtra.ForeColor= System.Drawing.Color.Navy;
            lblMsgExtra.Font.Bold = true;
            lblMsgExtra.Text="<br /><br /><p>Você já está inscrito nos Painéis 1,2, 3 e na Reunião Geral. Agora, precisa definir  um Macrogrupo e um Minigrupo para os debates de temas pontuais.</p><br /><p>Selecione abaixo um Macrogrupo</p>";
                
        }
        if (prmCdEvento == "000540")
        {
            MsgExtra.Visible = true;
            if ((prmCdCategoria == "00054001") || (prmCdCategoria == "00054003"))
               // lblMsgExtra.Text = "<table style=\"height: 255px;\" border=\"0\" width=\"320\"><tbody><tr><td width=\"132\">&nbsp;</td><td style=\"width: 142px; background-color: #7a9fcc;\" width=\"142\"><p style=\"text-align: center;\"><span style=\"color: #ffffff;\"><strong>IV Congresso Brasileiro FENAESS</strong></span></p><p style=\"text-align: center;\"><span style=\"color: #ffffff;\"><strong>(20 e 21 de agosto)</strong></span></p></td><td style=\"width: 123px; background-color: #7a9fcc;\" width=\"123\"><p style=\"text-align: center;\"><span style=\"color: #ffffff;\"><strong>II Encontro Jur&iacute;dico FENAESS</strong></span></p><p><span style=\"color: #ffffff;\"><strong>(21 de agosto)</strong></span></p></td></tr><tr style=\"background-color: #b3d8e8;\"><td style=\"width: 132px;\" width=\"132\"><p style=\"text-align: center;\"><span style=\"color: #025587;\"><strong>At&eacute; 20 de julho</strong></span></p></td><td width=\"142\"><p style=\"text-align: center;\"><span style=\"color: #025587;\">R$ 600,00</span></p></td><td width=\"123\"><p style=\"text-align: center;\"><span style=\"color: #025587;\">R$ 220,00</span></p></td></tr><tr style=\"background-color: #7a9fcc;\"><td style=\"width: 132px;\" width=\"132\"><p style=\"text-align: center;\"><span style=\"color: #ffffff;\"><strong>De 21 de julho a 14 de agosto</strong></span></p></td><td width=\"142\"><p style=\"text-align: center;\"><span style=\"color: #ffffff;\">R$ 650,00</span></p></td><td width=\"123\"><p style=\"text-align: center;\"><span style=\"color: #ffffff;\">R$ 280,00</span></p></td></tr><tr style=\"background-color: #b3d8e8;\"><td style=\"width: 132px;\" width=\"132\"><p style=\"text-align: center;\"><span style=\"color: #025587;\"><strong>A partir de 15 de agosto, apenas com cart&atilde;o e no local do evento</strong></span></p></td><td width=\"142\"><p style=\"text-align: center;\"><span style=\"color: #025587;\">R$ 700,00</span></p></td><td style=\"border-color: #000000; width: 123px;\" width=\"123\"><p style=\"text-align: center;\"><span style=\"color: #025587;\">R$ 330,00</span></p></td></tr></tbody></table>";
                lblMsgExtra.Text = "<table style=\"height: 255px;\" border=\"0\" width=\"320\"><tbody><tr><td width=\"132\">&nbsp;</td><td style=\"width: 142px; background-color: #7a9fcc;\" width=\"142\"><p style=\"text-align: center;\"><span style=\"color: #ffffff;\"><strong>IV Congresso Brasileiro FENAESS + II Encontro Jur&iacute;dico</strong></span></p><p style=\"text-align: center;\"><span style=\"color: #ffffff;\"><strong>(20 e 21 de agosto)</strong></span></p></td></tr><tr style=\"background-color: #b3d8e8;\"><td style=\"width: 132px;\" width=\"132\"><p style=\"text-align: center;\"><span style=\"color: #025587;\"><strong>At&eacute; 20 de julho</strong></span></p></td></td><td width=\"142\"><p style=\"text-align: center;\"><span style=\"color: #025587;\">R$ 600,00</span></p></td></tr><tr style=\"background-color: #7a9fcc;\"><td style=\"width: 132px;\" width=\"132\"><p style=\"text-align: center;\"><span style=\"color: #ffffff;\"><strong>De 21 de julho a 14 de agosto</strong></span></p></td><td width=\"142\"><p style=\"text-align: center;\"><span style=\"color: #ffffff;\">R$ 650,00</span></p></td></tr><tr style=\"background-color: #b3d8e8;\"><td style=\"width: 132px;\" width=\"132\"><p style=\"text-align: center;\"><span style=\"color: #025587;\"><strong>A partir de 15 de agosto, apenas com cart&atilde;o e no local do evento</strong></span></p></td><td width=\"142\"><p style=\"text-align: center;\"><span style=\"color: #025587;\">R$ 700,00</span></p></td></tr></tbody></table>";
                
            else if (prmCdCategoria == "00054002")
                //lblMsgExtra.Text = 
                //    "<p><span style=\"font-family: arial, helvetica, sans-serif; font-size: 12pt; color: #ff0000;\">Os inscritos na categoria \"ESTUDANTE\" dever&atilde;o enviar documento de comprova&ccedil;&atilde;o atrav&eacute;s do menu: \"ENVIAR ARQUIVO\".</span></p><p><span style=\"font-family: arial, helvetica, sans-serif; font-size: 12pt;\">&nbsp;</span></p> " +
                //    "<table style=\"height: 255px;\" border=\"0\" width=\"320\"><tbody><tr><td width=\"132\">&nbsp;</td><td style=\"width: 142px; background-color: #7a9fcc;\" width=\"142\"><p style=\"text-align: center;\"><span style=\"color: #ffffff;\"><strong>Estudantes &ndash; IV&nbsp; Congresso Brasileiro FENAESS</strong></span></p><p style=\"text-align: center;\"><span style=\"color: #ffffff;\"><strong>(20 e 21 de agosto)</strong></span></p></td><td style=\"width: 123px; background-color: #7a9fcc;\" width=\"123\"><p style=\"text-align: center;\"><span style=\"color: #ffffff;\"><strong>Estudantes &ndash; II Encontro Jur&iacute;dico FENAESS</strong></span></p><p style=\"text-align: center;\"><span style=\"color: #ffffff;\"><strong>(21 de agosto)</strong></span></p></td></tr><tr style=\"background-color: #b3d8e8;\"><td style=\"width: 132px;\" width=\"132\"><p style=\"text-align: center;\"><span style=\"color: #025587;\"><strong>At&eacute; 20 de julho</strong></span></p></td><td width=\"142\"><p style=\"text-align: center;\"><span style=\"color: #025587;\">R$ 400,00</span></p></td><td width=\"123\"><p style=\"text-align: center;\"><span style=\"color: #025587;\">R$ 200,00</span></p></td></tr><tr style=\"background-color: #7a9fcc;\"><td style=\"width: 132px;\" width=\"132\"><p style=\"text-align: center;\"><span style=\"color: #ffffff;\"><strong>De 21 de julho a 14 de agosto</strong></span></p></td><td width=\"142\"><p style=\"text-align: center;\"><span style=\"color: #ffffff;\">R$ 450,00</span></p></td><td width=\"123\"><p style=\"text-align: center;\"><span style=\"color: #ffffff;\">R$ 260,00</span></p></td></tr><tr style=\"background-color: #b3d8e8;\"><td style=\"width: 132px;\" width=\"132\"><p style=\"text-align: center;\"><span style=\"color: #025587;\"><strong>A partir de 15 de agosto, apenas com cart&atilde;o e no local do evento</strong></span></p></td><td width=\"142\"><p style=\"text-align: center;\"><span style=\"color: #025587;\">R$ 500,00</span></p></td><td style=\"border-color: #000000; width: 123px;\" width=\"123\"><p style=\"text-align: center;\"><span style=\"color: #025587;\">R$ 310,00</span></p></td></tr></tbody></table>";
                lblMsgExtra.Text =
                    "<p><span style=\"font-family: arial, helvetica, sans-serif; font-size: 12pt; color: #ff0000;\">Os inscritos na categoria \"ESTUDANTE\" dever&atilde;o enviar documento de comprova&ccedil;&atilde;o atrav&eacute;s do menu: \"ENVIAR ARQUIVO\".</span></p><p><span style=\"font-family: arial, helvetica, sans-serif; font-size: 12pt;\">&nbsp;</span></p> " +
                    "<table style=\"height: 255px;\" border=\"0\" width=\"320\"><tbody><tr><td width=\"132\">&nbsp;</td><td style=\"width: 142px; background-color: #7a9fcc;\" width=\"142\"><p style=\"text-align: center;\"><span style=\"color: #ffffff;\"><strong>Estudantes &ndash; IV&nbsp; Congresso Brasileiro FENAESS + II Encontro Jur&iacute;dico</strong></span></p><p style=\"text-align: center;\"><span style=\"color: #ffffff;\"><strong>(20 e 21 de agosto)</strong></span></p></td></tr><tr style=\"background-color: #b3d8e8;\"><td style=\"width: 132px;\" width=\"132\"><p style=\"text-align: center;\"><span style=\"color: #025587;\"><strong>At&eacute; 20 de julho</strong></span></p></td><td width=\"142\"><p style=\"text-align: center;\"><span style=\"color: #025587;\">R$ 400,00</span></p></td></tr><tr style=\"background-color: #7a9fcc;\"><td style=\"width: 132px;\" width=\"132\"><p style=\"text-align: center;\"><span style=\"color: #ffffff;\"><strong>De 21 de julho a 14 de agosto</strong></span></p></td><td width=\"142\"><p style=\"text-align: center;\"><span style=\"color: #ffffff;\">R$ 450,00</span></p></td></tr><tr style=\"background-color: #b3d8e8;\"><td style=\"width: 132px;\" width=\"132\"><p style=\"text-align: center;\"><span style=\"color: #025587;\"><strong>A partir de 15 de agosto, apenas com cart&atilde;o e no local do evento</strong></span></p></td><td width=\"142\"><p style=\"text-align: center;\"><span style=\"color: #025587;\">R$ 500,00</span></p></td></tr></tbody></table>";
                    //<td style=\"width: 123px; background-color: #7a9fcc;\" width=\"123\"><p style=\"text-align: center;\"><span style=\"color: #ffffff;\"><strong>Estudantes &ndash; II Encontro Jur&iacute;dico FENAESS</strong></span></p><p style=\"text-align: center;\"><span style=\"color: #ffffff;\"><strong>(21 de agosto)</strong></span></p></td>
                    //    <td width=\"123\"><p style=\"text-align: center;\"><span style=\"color: #025587;\">R$ 200,00</span></p></td>
                    //        <td width=\"123\"><p style=\"text-align: center;\"><span style=\"color: #ffffff;\">R$ 260,00</span></p></td>
                    //<td style=\"border-color: #000000; width: 123px;\" width=\"123\"><p style=\"text-align: center;\"><span style=\"color: #025587;\">R$ 310,00</span></p></td></tr></tbody></table>";
            else
                MsgExtra.Visible = false;

        }

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
   
}
