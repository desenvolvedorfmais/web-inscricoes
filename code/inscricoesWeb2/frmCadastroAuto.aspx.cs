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

using System.Drawing;

using KrksaComponentes;
using KrksaComponentes.cllEventosClasses;
using cllEventos;
using CLLFuncoes;

using System.Data.SqlClient;

using AjaxControlToolkit;

using MSXML2;

using System.Xml;

public partial class frmCadastroAuto : BaseWebUi //System.Web.UI.Page
{
    Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();

    CategoriaCad oCategoriaCad = new CategoriaCad();

    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    SqlConnection SessionCnn;

    bool wrkLiberadoGravar = false;

    bool wrkEnviarEmail = false;

    String SessionIdioma;

    String SessionPais;

    String SessionCateg;

    String SessionAtv;

    String SessionTipoAcesso;
    String SessionInscrRef;

    String SessionChaveLibercao;

    String SessionTipoSistema;

    String SessionOperacao;

    PesquisasResposta oPesquisasResposta = new PesquisasResposta();

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

            SessionPais = (String)Session["SessionPais"];
            if ((SessionPais == null) || (SessionPais == ""))
                SessionPais = "";
            Session["SessionPais"] = SessionPais;

            SessionTipoSistema = (String)Session["SessionTipoSistema"];
            if (SessionTipoSistema == null)
                SessionTipoSistema = "NRM";
            Session["SessionTipoSistema"] = SessionTipoSistema;

            SessionOperacao = (String)Session["SessionOperacao"];
            if (SessionOperacao == null)
                SessionOperacao = "";
            Session["SessionOperacao"] = SessionOperacao;

            SessionCateg = (String)Session["SessionCateg"];
            if (SessionCateg == null)
                SessionCateg = "";
            Session["SessionCateg"] = SessionCateg;

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



            if ((Request.QueryString["refInscr"] != null) &&
                (Request.QueryString["refInscr"].ToString().Trim().ToUpper() != ""))
            {
                SessionInscrRef = Request.QueryString["refInscr"];
            }
            else
            {
                SessionInscrRef = (String)Session["SessionInscrRef"];
                if (SessionInscrRef == null)
                    SessionInscrRef = "";
            }
            Session["SessionInscrRef"] = SessionInscrRef;


            //verificarIdioma(SessionIdioma);

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

            AtividadeCad oAtividadeCad = new AtividadeCad();
            DataTable DT = oAtividadeCad.Listar(SessionEvento.CdEvento, SessionCnn);
            if ((DT == null) || (DT.Rows.Count <= 0))
            {
                btnAtividades.Visible = false;
            }
            

            if ((Request["cdMatricula"] != null) &&
                (Request["cdMatricula"] != ""))
            {

                //txtMatricula.Text = Request["cdMatricula"];


                pesquisar(Request.QueryString["cdMatricula"]);

                DataList1.Visible = false;
            }
            else
            {
                if ((SessionParticipante != null) && (SessionParticipante.CdParticipante != ""))
                {
                    pesquisar(SessionParticipante.CdParticipante);
                    //DataList1.Visible = false;
                }
                //else
                //{
                //    SqlDataSource1.ConnectionString = SessionCnn.ConnectionString;
                //    DataList1.DataSourceID = "SqlDataSource1";
                //    localizarPesquisa(SessionEvento.CdEvento, Request["cdMatricula"]);
                //}
            }

            SessionChaveLibercao = (String)Session["SessionChaveLibercao"];
            if (SessionChaveLibercao == null)
                SessionChaveLibercao = "";

            btnFoco.Focus();

            

            if ((SessionParticipante != null) && (SessionParticipante.CdParticipante == "") && ((Request.QueryString["cpf"] != null) && (Request.QueryString["cpf"].ToString() != "")))
            {
                Control txtcpf = FindControlRecursive(this.Page, "txt_nuCPFCNPJ");
                if (SessionEvento.FlUtilizarDadoEventoAnterior)
                {                    
                    if (txtcpf != null)
                    {
                        (txtcpf as TextBox).Text = oClsFuncoes.MascaraGerar(Request.QueryString["cpf"].ToString(), "999.999.999-99");
                        if ((txtcpf as TextBox).Text != "")
                            (txtcpf as TextBox).ReadOnly = true;

                        btnDadosAnteriores_Click(sender, e);
                    }
                }
                else
                {
                    string tempCPF = Request.QueryString["cpf"].ToString();
                    if ((tempCPF == "11111111111") ||
                        (tempCPF == "22222222222") ||
                        (tempCPF == "33333333333") ||
                        (tempCPF == "44444444444") ||
                        (tempCPF == "55555555555") ||
                        (tempCPF == "66666666666") ||
                        (tempCPF == "77777777777") ||
                        (tempCPF == "88888888888") ||
                        (tempCPF == "99999999999") ||
                        (tempCPF == "00000000000"))
                    {
                        if (txtcpf != null)
                        {
                            (txtcpf as TextBox).Text = oClsFuncoes.MascaraGerar(Request.QueryString["cpf"].ToString(), "999.999.999-99");
                            if ((txtcpf as TextBox).Text != "")
                                (txtcpf as TextBox).ReadOnly = true;
                        }
                    }
                    else
                    {
                        if (txtcpf != null)
                        {
                            (txtcpf as TextBox).Text = oClsFuncoes.MascaraGerar(Request.QueryString["cpf"].ToString(), "999.999.999-99");
                            if ((txtcpf as TextBox).Text != "")
                                (txtcpf as TextBox).ReadOnly = true;
                        }
                    }
                }
            }

            if ((SessionEvento.CdEvento == "008501") && (SessionIdioma == "PTBR"))
            {
                if ((SessionParticipante != null) && (SessionParticipante.CdParticipante != ""))
                {
                    Control txtcpf = FindControlRecursive(this.Page, "txt_nuCPFCNPJ");
                    Control txtNome = FindControlRecursive(this.Page, "txt_noParticipante");
                    //Control txtNomeEtiquta = FindControlRecursive(this.Page, "txt_noEtiqueta");



                    if (txtcpf != null)
                    {
                        (txtcpf as TextBox).ReadOnly = true;
                    }

                    if (txtNome != null)
                    {
                        (txtNome as TextBox).ReadOnly = true;
                    }
                }
            }

            if (((SessionEvento.FlPesquisaCPFReceita) || (SessionEvento.CdEvento == "004501")) && (SessionIdioma == "PTBR"))
            {
                Control txtcpf = FindControlRecursive(this.Page, "txt_nuCPFCNPJ");
                Control txtNome = FindControlRecursive(this.Page, "txt_noParticipante");
                Control txtNomeEtiqueta = FindControlRecursive(this.Page, "txt_noEtiqueta");
                Control txtDtNasc = FindControlRecursive(this.Page, "txt_dsNascimento");

                Control txtEstadoRpresenta = null;
                if (SessionEvento.CdEvento == "004501")
                    txtEstadoRpresenta = FindControlRecursive(this.Page, "txt_dsAuxiliar8");//evento 004501 -- MDA

                if ((SessionParticipante != null) && (SessionParticipante.CdParticipante == ""))
                {

                    if ((txtcpf != null) && (Request.QueryString["cpf"] != null) && (Request.QueryString["cpf"].ToString() != ""))
                    {
                        (txtcpf as TextBox).Text = oClsFuncoes.MascaraGerar(Request.QueryString["cpf"].ToString(), "999.999.999-99");
                        if ((txtcpf as TextBox).Text != "")
                            (txtcpf as TextBox).ReadOnly = true;
                    }

                    if (txtNome != null)
                    {
                        if ((Request.QueryString["nome"] != null) && (Request.QueryString["nome"].ToString() != ""))
                        {
                            (txtNome as TextBox).Text = Request.QueryString["nome"].ToString();
                            if ((SessionEvento.CdEvento != "004501"))// && ((txtNome as TextBox).Text != ""))
                                (txtNome as TextBox).ReadOnly = true;
                        }
                    }

                    if (txtNomeEtiqueta != null)
                    {
                        if ((Request.QueryString["nome"] != null) && (Request.QueryString["nome"].ToString() != ""))
                        {
                            (txtNomeEtiqueta as TextBox).Text = NomeReduzido(Request.QueryString["nome"].ToString());
                        }
                    }

                    if (txtEstadoRpresenta != null)
                    {
                        (txtEstadoRpresenta as DropDownList).Text = Request.QueryString["UF"].ToString().ToUpper();
                        if ((txtEstadoRpresenta as DropDownList).Text != "")
                            (txtEstadoRpresenta as DropDownList).Enabled = false;
                    }

                    if (txtDtNasc != null)
                    {
                        if ((Request.QueryString["dtnasc"] != null) && (Request.QueryString["dtnasc"].ToString() != ""))
                        {
                            (txtDtNasc as TextBox).Text = NomeReduzido(Request.QueryString["dtnasc"].ToString());
                            (txtDtNasc as TextBox).ReadOnly = true;
                        }
                    }
                }
                else
                {
                    if (txtcpf != null)
                    {                        
                        (txtcpf as TextBox).ReadOnly = true;
                    }

                    if (txtNome != null)
                    {
                        (txtNome as TextBox).ReadOnly = true;
                    }
                }
            }


            if (SessionEvento.CdEvento == "003003")
            {
                Control Categ = FindControlRecursive(this.Page, "txt_cdCategoria");
                if (Categ != null)
                {
                    if (((Categ as DropDownList).ID == "txt_cdCategoria") && 
                        ((Categ as DropDownList).Text != "00300316") &&
                        ((Categ as DropDownList).Text != "00300308") )
                    {
                        Control dsSubcateg = FindControlRecursive(this.Page, "txt_dsAuxiliar14");
                        if (dsSubcateg != null)
                        {

                            (dsSubcateg as DropDownList).Text = "";// = "teste2";
                            (dsSubcateg as DropDownList).Enabled = false;// = "teste2";
                        }

                        Control dsChave = FindControlRecursive(this.Page, "txt_dsCampoExtraPreCad");
                        if (dsChave != null)
                        {

                            (dsChave as TextBox).Text = "";// = "teste2";
                            (dsChave as TextBox).Enabled = false;// = "teste2";
                        }
                    }
                    else
                    {
                        Control dsSubcateg = FindControlRecursive(this.Page, "txt_dsAuxiliar14");
                        if (((Categ as DropDownList).Text == "00300316") && (dsSubcateg != null))
                        {

                            //(dsSubcateg as DropDownList).Text = "";// = "teste2";
                            (dsSubcateg as DropDownList).Enabled = true;// = "teste2";
                        }

                        Control dsChave = FindControlRecursive(this.Page, "txt_dsCampoExtraPreCad");
                        if (dsChave != null)
                        {

                            //(dsChave as TextBox).Text = "";// = "teste2";
                            (dsChave as TextBox).Enabled = true;// = "teste2";
                        }
                    }
                }

            }


            

            if ((SessionParticipante != null) && (SessionParticipante.CdParticipante != "") && (SessionParticipante.Categoria.FlRequerConfirmacaoDoc) && (!SessionParticipante.FlDocumentoConfirmado))
                btnEnviarDocumento.Visible = true;

            if (SessionEvento.CdCliente == "0014")
                btnEnviarDocumento.Visible = false;

            if ((SessionEvento.CdCliente == "0013") &&
                (SessionParticipante != null) &&
                (SessionParticipante.CdParticipante != ""))
            {                

                if ((SessionParticipante.NoAreaAtuacao == "SIM") && (!SessionParticipante.FlDocumentoConfirmado))
                    btnEnviarDocumento.Visible = true;


                if (Geral.datahoraServidor(SessionCnn) > DateTime.Parse("05/08/2015 23:59:59"))
                {
                    btnEnviarDocumento.Visible = false;                  
                }

                btnImprimirCredencial.Visible = false;
                if (SessionParticipante.FlAtivo)
                {
                    if (SessionParticipante.CdCategoria == "00130501")
                    {
                        if (SessionParticipante.FlConfirmacaoInscricao)
                        {
                            if (SessionParticipante.NoAreaAtuacao == "SIM")
                            {
                                if ((SessionParticipante.DsAuxiliar19 == "SIM") && (SessionParticipante.FlDocumentoConfirmado))
                                    btnImprimirCredencial.Visible = true;
                            }
                            else
                            {
                                btnImprimirCredencial.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        if (!SessionParticipante.Categoria.FlAtividades)
                            btnImprimirCredencial.Visible = true;
                        else if (SessionParticipante.FlConfirmacaoInscricao)
                            btnImprimirCredencial.Visible = true;
                    }
                }

                if (Geral.datahoraServidor(SessionCnn) > DateTime.Parse("05/08/2015 23:59:59"))
                {
                    btnImprimirCredencial.Visible = false;
                    btnEnviarDocumento.Visible = false;
                }


            }

            CategoriasFiltro();

            verificarCampos();

            if ((SessionEvento.CdEvento == "008501") && (SessionParticipante != null) && (SessionParticipante.CdParticipante == ""))
                btnVoltarSair.Visible = true;
        }
        else
        {
            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"]; 
            
            SessionIdioma = (String)Session["SessionIdioma"];

            SessionPais = (String) Session["SessionPais"];

            if (SessionCateg == null)
                SessionCateg = (String)Session["SessionCateg"];
            else
                Session["SessionCateg"] = SessionCateg;

            SessionChaveLibercao = (String)Session["SessionChaveLibercao"];

            SessionAtv = (String)Session["SessionAtv"];

            SessionTipoAcesso = (String)Session["tpAcesso"];

            SessionInscrRef = (String)Session["SessionInscrRef"];

            SessionTipoSistema = (String)Session["SessionTipoSistema"];
            SessionOperacao = (String)Session["SessionOperacao"];
        }

        if (SessionEvento == null)
            Server.Transfer("frmSessaoExpirada.aspx", true);

        //verificarCampos();

        if ((SessionCateg != null) && (SessionCateg != ""))
        {
            Categoria tmpCateg = oCategoriaCad.Pesquisar(SessionEvento.CdEvento, SessionCateg, SessionCnn);

            if ((tmpCateg != null) && (tmpCateg.FlVerificarPreCadastro) && (tmpCateg.TpVerificacaoPreCadastro == "4"))
            {

                Control dsChave = FindControlRecursive(this.Page, "txt_dsCampoExtraPreCad");
                if (dsChave != null)
                {
                    if ( ((SessionChaveLibercao == null) || (SessionChaveLibercao == "")) &&
                         ((SessionParticipante == null) || (SessionParticipante.CdParticipante == "")))
                    {
                        //(dsChave as TextBox).Text = "";// = "teste2";
                        (dsChave as TextBox).Enabled = true;// = "teste2";
                    }
                    else
                    {
                        if ((SessionChaveLibercao != null) & (SessionChaveLibercao != ""))
                            (dsChave as TextBox).Text = SessionChaveLibercao;// = "teste2";

                        (dsChave as TextBox).Enabled = false;// = "teste2";

                        if (SessionEvento.CdEvento == "008201")
                        {
                            ParticipantePreCadastro oParticipantePreCadastro = oParticipanteCad.PesquisarPreCadastro(SessionEvento.CdEvento, SessionCateg, "", "", "", SessionChaveLibercao, SessionCnn);

                            if (oParticipantePreCadastro != null)
                            {
                                Control noPart = FindControlRecursive(this.Page, "txt_noParticipante");
                                if (noPart != null)
                                {
                                    (noPart as TextBox).Text = oParticipantePreCadastro.NoParticipantePrecadastro;
                                    (noPart as TextBox).Enabled = false;
                                }

                                Control dsEmail = FindControlRecursive(this.Page, "txt_dsEmail");
                                if (dsEmail != null)
                                {
                                    (dsEmail as TextBox).Text = oParticipantePreCadastro.DsEmail;
                                    (dsEmail as TextBox).Enabled = false;
                                }
                            }

                        }

                    }
                }
            }
            else
            {

                Control dsChave = FindControlRecursive(this.Page, "txt_dsCampoExtraPreCad");
                if (dsChave != null)
                {
                    if ((SessionChaveLibercao != null) & (SessionChaveLibercao != ""))
                        (dsChave as TextBox).Text = "";// = "teste2";
                    
                    (dsChave as TextBox).Enabled = false;// = "teste2";
                }
            }
        }


        if (SessionTipoSistema == "CRD")//((SessionEvento != null) && (SessionEvento.CdEvento == "002105"))
        {
            btnGravarParticipante.Visible = false;

            //mnuCred.Visible = true;

            if ((SessionOperacao != "") && (SessionOperacao != "INICIO"))
                prp_btnhabilita(SessionOperacao);
            
                //prp_btnhabilita(SessionOperacao);
            else
            if ((SessionParticipante != null) && (SessionParticipante.CdParticipante != ""))
            {
                prp_btnhabilita(SessionParticipante.FlAtivo ? "ATIVO" : "INATIVO");

                //btnNovo.PostBackUrl = SessionEvento.DsLinkRedirecionamento;
                //btnNovo.Visible = true;

                //btnEtiqueta.Visible = true;
            }
            else
                prp_btnhabilita("INICIO");

        }

        if (SessionEvento.CdEvento == "006602")
        {
            btnDesconfirmar.Visible = true;
        }

        verificarIdioma(SessionIdioma);

        ToolkitScriptManager1.RegisterPostBackControl(btnGravarParticipante);
        ToolkitScriptManager1.RegisterPostBackControl(btnNovo);
        ToolkitScriptManager1.RegisterPostBackControl(btnEtiqueta);


        ToolkitScriptManager1.RegisterPostBackControl(btnDesconfirmar);

        //ToolkitScriptManager1.RegisterPostBackControl(btnLimpar);
        //ToolkitScriptManager1.RegisterPostBackControl(btnLocalizar);
        //ToolkitScriptManager1.RegisterPostBackControl(btnNovos);
        //ToolkitScriptManager1.RegisterPostBackControl(btnAlterar);
        //ToolkitScriptManager1.RegisterPostBackControl(btnCancelar);
        //ToolkitScriptManager1.RegisterPostBackControl(btnConfirmar);
        //ToolkitScriptManager1.RegisterPostBackControl(btnEtiquetas);
        //ToolkitScriptManager1.RegisterPostBackControl(btnAtividade);
        ////ToolkitScriptManager1.RegisterPostBackControl(btnNovos);

        ToolkitScriptManager1.RegisterPostBackControl(btnEnviarDocumento);
        ToolkitScriptManager1.RegisterPostBackControl(btnImprimirCredencial);


        if (SessionEvento.CdEvento == "005701")
        {
            lblTitulo.Text = "INSCREVA-SE!";
            btnGravarParticipante.Text = " ENVIAR ";
            AtencaoIESB.Visible = true;
            logoIESB.Visible = true;
            socialIESB.Visible = true;
        }

        if ((SessionEvento.CdCliente == "0083") && ((SessionCateg == "00830303") || 
            ((SessionParticipante != null) && (SessionParticipante.CdCategoria == "00830303")))
            )
        {

            Control dsCampo1 = FindControlRecursive(this.Page, "txt_dsAuxiliar2");//cooperativa singular?
            Control linha1 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar2");
            Control dsLblCampo1 = FindControlRecursive(this.Page, "lbl_dsAuxiliar2");

            Control dsCampo3 = FindControlRecursive(this.Page, "txt_dsAuxiliar1");//cooperativa singular?
            Control linha3 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar1");
            Control dsLblCampo3 = FindControlRecursive(this.Page, "lbl_dsAuxiliar1");

            Control dsCampo2 = FindControlRecursive(this.Page, "txt_noInstituicao");//instituição
            Control linha2 = FindControlRecursive(this.Page, "tblinha_noInstituicao");
            Control dsLblCampo2 = FindControlRecursive(this.Page, "lbl_noInstituicao");

            (dsCampo1 as DropDownList).Visible = false;
            (linha1 as TableRow).Visible = false;
            (dsLblCampo1 as Label).Visible = false;

            (dsCampo3 as DropDownList).Visible = false;
            (linha3 as TableRow).Visible = false;
            (dsLblCampo3 as Label).Visible = false;

            (dsCampo2 as TextBox).Visible = true;
            (linha2 as TableRow).Visible = true;
            (dsLblCampo2 as Label).Visible = true;
        }
    }

    protected void verificarIdioma(string prmIdioma)
    {
        if (prmIdioma == "PTBR")//(txtidioma.SelectedIndex == 0)
        {
            SessionIdioma = "PTBR";
            Session["SessionIdioma"] = SessionIdioma;
            
            lblTitulo.Text = "Cadastro";
            btnGravarParticipante.Text = "Confirmar";
            btnEnviarDocumento.Text = "Enviar Documento";


            if (SessionEvento.CdEvento == "008201")
            {
                lblTitulo.Text = "Confirmação de Presença";
                btnGravarParticipante.Text = "Gerar Confirmação";
            }

            if (SessionEvento.CdEvento == "006602")
            {
                lblTitulo.Text = "Confirmação de Presença";
                btnGravarParticipante.Text = "Gerar Confirmação";
            }


            if (SessionEvento.CdEvento == "008701")
            {
                if (SessionCateg == "00870101")
                    lblTitulo.Text = "Credenciamento de Visitante Nacional";
                else if (SessionCateg == "00870102")
                    lblTitulo.Text = "Credenciamento de Visitante Estrangeiro";
                else if (SessionCateg == "00870103")
                    lblTitulo.Text = "Credenciamento de Expositor";
                else if (SessionCateg == "00870104")
                    lblTitulo.Text = "Credenciamento de Organização Militar";
            }


            btnVoltarSair.Text = "Voltar";
        }
        else if (prmIdioma == "ENUS")//(txtidioma.SelectedIndex == 1)
        {
            SessionIdioma = "ENUS";
            Session["SessionIdioma"] = SessionIdioma;

            lblTitulo.Text = "Registration Form";
            btnGravarParticipante.Text = "Save";
            btnEnviarDocumento.Text = "Send Document";
        }
        else if (prmIdioma == "ESP")//(txtidioma.SelectedIndex == 2)
        {
            SessionIdioma = "ESP";
            Session["SessionIdioma"] = SessionIdioma;

            lblTitulo.Text = "Formulario De Inscripción";
            btnGravarParticipante.Text = "Ahorrar";
            btnVoltarSair.Text = "Vuelta";
        }
        else if (prmIdioma == "FRA")//(txtidioma.SelectedIndex == 3)
        {
            SessionIdioma = "FRA";
            Session["SessionIdioma"] = SessionIdioma;

            lblTitulo.Text = "Formulaire d'inscription";
            btnGravarParticipante.Text = "Sauver";
        }        
    }

        
    

    
    override protected void OnInit(EventArgs e)
    {

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

        if (SessionParticipante == null)
            SessionParticipante = (Participante)Session["SessionParticipante"];
        else
            Session["SessionParticipante"] = SessionParticipante;

        SessionIdioma = (String)Session["SessionIdioma"];
        if (SessionIdioma == null)
            SessionIdioma = "PTBR";
        Session["SessionIdioma"] = SessionIdioma;

        SessionPais = (String)Session["SessionPais"];
        if ((SessionPais == null) || (SessionPais == ""))
            SessionPais = "";
        Session["SessionPais"] = SessionPais;

        SessionTipoSistema = (String)Session["SessionTipoSistema"];
        if (SessionTipoSistema == null)
            SessionTipoSistema = "NRM";
        Session["SessionTipoSistema"] = SessionTipoSistema;
        
        SessionOperacao = (String)Session["SessionOperacao"];
        if (SessionOperacao == null)
            SessionOperacao = "";
        Session["SessionOperacao"] = SessionOperacao;

        SessionCateg = (String)Session["SessionCateg"];
        if (SessionCateg == null)
            SessionCateg = "";


        if ((SessionParticipante != null) && (SessionParticipante.CdParticipante != ""))
        {
            SessionCateg = SessionParticipante.CdCategoria;
            SessionPais = SessionParticipante.NoPais;
            Session["SessionPais"] = SessionPais;
        }

        if ((Request.QueryString["tpAcesso"] != null) &&
                    (Request.QueryString["tpAcesso"].ToString().Trim().ToUpper() != ""))
        {
            SessionTipoAcesso = Request.QueryString["tpAcesso"];
        }
        else
        {
            SessionTipoAcesso = (String)Session["tpAcesso"];
            if (SessionTipoAcesso == null)
                SessionTipoAcesso = "NRM";
        }
        Session["tpAcesso"] = SessionTipoAcesso;

        Session["SessionCateg"] = SessionCateg;

        verificarIdioma(SessionIdioma);

        string tmpCdPart = "";
        if ((Request["cdMatricula"] != null) &&
            (Request["cdMatricula"] != ""))
        {
            tmpCdPart = Request["cdMatricula"].ToString();
        }
        else
        {
            tmpCdPart = (SessionParticipante != null) ? SessionParticipante.CdParticipante : "";
        }
        

        Table table = new Table();
        table.ID = "tbCadastro";
        

        //table.CellSpacing = 3;  
        //table.Width = 100%;
        //table.BorderWidth = 1;

        DataTable dtCampos = new DataTable();
        if (SessionIdioma == "PTBR")
            dtCampos = localizarCamposCadastro("004", SessionCateg);
        else if (SessionIdioma == "ENUS")
            dtCampos = localizarCamposCadastro("007", SessionCateg);
        else if (SessionIdioma == "ESP")
            dtCampos = localizarCamposCadastro("008", SessionCateg);
        else if (SessionIdioma == "FRA")
            dtCampos = localizarCamposCadastro("009", SessionCateg);

        string nome_campo_anterior = "";
        string nome_campo_antes_anterior = "";

        for (int i = 0; i < dtCampos.Rows.Count; i++)
        //if (e.Row.I.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            string tmpCdEvento = dtCampos.DefaultView[i]["cdEvento"].ToString().Trim();

            string prmValores = dtCampos.DefaultView[i]["dsDetalhesCampo"].ToString().Trim();
            String[] tmpValores;
            tmpValores = prmValores.Split(';');
            // lblMsg.Text += prmValores.Text + "<br/>";
            // return;
            String[] tmpValoresLista = tmpValores[12].Split('#');
            KrksaComp compkrk_t =
                new KrksaComp(
                    Boolean.Parse(tmpValores[0]),
                    Boolean.Parse(tmpValores[1]),
                    tmpValores[2],
                    tmpValores[3],
                    tmpValores[4],
                    Boolean.Parse(tmpValores[5]),
                    tmpValores[6],
                    int.Parse(tmpValores[7]),
                    int.Parse(tmpValores[8]),
                    int.Parse(tmpValores[9]),
                    Boolean.Parse(tmpValores[10]),
                    tmpValores[11],
                    tmpValoresLista,
                    tmpValores[13],
                    Boolean.Parse(tmpValores[14]),
                    Boolean.Parse(tmpValores[15]),
                    int.Parse(tmpValores[16]),
                    int.Parse(tmpValores[17]),
                    Boolean.Parse(tmpValores[18]),
                    int.Parse(tmpValores[19]),
                    tmpValores[20],
                    Boolean.Parse(tmpValores[21]),
                    Boolean.Parse(tmpValores[22]),
                    Boolean.Parse(tmpValores[23]),
                    Boolean.Parse(tmpValores[24]),
                    Boolean.Parse(tmpValores[25]),
                    Boolean.Parse(tmpValores[26]),
                    Boolean.Parse(tmpValores[27]),
                    Boolean.Parse(tmpValores[28]),
                    tmpValores[29]);
            KrksaComp compkrk = compkrk_t;


            
            TableRow table_linha = new TableRow();
            table_linha.ID = "tblinha_"+compkrk.NomeCampoBancoDados;
            
            table_linha.Visible = compkrk.VisivelWebForm;

            //if (SessionEvento.CdEvento == "010801")
            {
                if (SessionIdioma == "PTBR")
                {
                    if (SessionPais != "BRASIL")
                    {
                        if ((compkrk.NomeCampo == "txt_nuCPFCNPJ") ||
                            (compkrk.NomeCampo == "txt_noBairro") ||
                            (compkrk.NomeCampo == "txt_dsComplementoEndereco") ||
                            (compkrk.NomeCampo == "txt_dsUF") ||
                            (compkrk.NomeCampo == "txt_noCidade"))
                        {
                            table_linha.Visible = false;
                        }
                        if ((SessionEvento.CdEvento == "010801") &&
                            ((compkrk.NomeCampo == "txt_dsAuxiliar1") ||
                             (compkrk.NomeCampo == "txt_dsAuxiliar2") ||
                             (compkrk.NomeCampo == "txt_dsAuxiliar3")))
                        {
                            table_linha.Visible = true;
                        }
                        if ((compkrk.NomeCampo == "txt_dsDocIdentificador"))
                        {
                            table_linha.Visible = true;
                            compkrk.VisivelWebForm = true;
                        }
                        if ((SessionEvento.CdEvento == "012601") && (compkrk.NomeCampo == "txt_dsAuxiliar7"))
                        {
                            table_linha.Visible = true;
                            compkrk.VisivelWebForm = true;
                        }
                    }
                    else //if (SessionPais == "BRASIL") )
                    {
                        if ((compkrk.NomeCampo == "txt_nuCPFCNPJ") ||
                            (compkrk.NomeCampo == "txt_noBairro") ||
                            (compkrk.NomeCampo == "txt_dsComplementoEndereco") ||
                            (compkrk.NomeCampo == "txt_dsUF") ||
                            (compkrk.NomeCampo == "txt_noCidade"))
                        {
                            table_linha.Visible = true;
                        }
                        if ((compkrk.NomeCampo == "txt_noPais"))
                        {
                            table_linha.Visible = false;
                        }
                        if ((SessionEvento.CdEvento == "010801") &&
                            ((compkrk.NomeCampo == "txt_dsAuxiliar1") ||
                             (compkrk.NomeCampo == "txt_dsAuxiliar2") ||
                             (compkrk.NomeCampo == "txt_dsAuxiliar3")))
                        {
                            table_linha.Visible = false;
                        }
                    }
                }
            }

            if (SessionEvento.CdEvento == "010901")
            {
                if (SessionIdioma == "PTBR")
                {
                    if (SessionPais != "BRASIL")
                    {
                        if ((compkrk.NomeCampo == "txt_nuCPFCNPJ"))
                        {
                            table_linha.Visible = false;
                        }
                        if ((compkrk.NomeCampo == "txt_dsAuxiliar1") )
                        {
                            table_linha.Visible = true;
                        }
                    }
                    else //if (SessionPais == "BRASIL") )
                    {
                        if ((compkrk.NomeCampo == "txt_nuCPFCNPJ"))
                        {
                            table_linha.Visible = true;
                        }
                        if ((compkrk.NomeCampo == "txt_noPais"))
                        {
                            table_linha.Visible = false;
                        }
                        if ((compkrk.NomeCampo == "txt_dsAuxiliar1"))
                        {
                            table_linha.Visible = false;
                        }
                    }
                }
            }

            TableCell table_coluna = new TableCell();

            TableCell table_coluna2 = new TableCell();

            //table_coluna2.BorderWidth = 1;
            

            System.Web.UI.WebControls.Label lbl = new Label();
            if (compkrk.TipoCampo != KrksaComp.TiposCampo.Label)
            {
                lbl.Text = compkrk.NomeLabelCampo;
            }
            else
            {
                //table_coluna.BackColor = Color.Navy;
                lbl.Text = "";
            }

            lbl.ID = compkrk.NomeCampo.Replace("txt","lbl");
            lbl.CssClass = "lblTitulocampo";

            lbl.Visible = false;
            table_coluna.Width = 190;

            

            //table_coluna.BorderWidth = 1;
            
            if (SessionEvento.TpFolhaCadastroWeb == "1")
            {
                table_coluna.Width = 190;
                //table_coluna.BorderWidth = 1;
                table_coluna.HorizontalAlign = HorizontalAlign.Right;
                table_coluna.Controls.Add(lbl);

                table_coluna.ID = compkrk.NomeCampo.Replace("txt", "col");
            }
            else if (SessionEvento.TpFolhaCadastroWeb == "2")
            {// = 
                //table_coluna2.BorderWidth = 1;

                if (compkrk.SubirLinha)
                {
                   // Control tmp_table_coluna = null;

                    table_coluna.ID = nome_campo_anterior.Replace("txt", "col");
                    table_coluna2.ID = compkrk.NomeCampo.Replace("txt", "col");

                    if (nome_campo_antes_anterior == "")
                    {
                       Control tmp_table_coluna = FindControlRecursive(table, "tblinha_" + nome_campo_anterior);
                        
                        if (tmp_table_coluna != null)
                        {
                            nome_campo_antes_anterior = nome_campo_anterior;
                            table_linha = (tmp_table_coluna as TableRow);
                            table_linha.Cells[0].ColumnSpan = 1;
                            table_linha.Cells[0].Width = Unit.Pixel(180);

                            //table_linha.Cells[0].ID = nome_campo_anterior.Replace("txt", "col");
                        }
                    }
                    else
                    {
                        Control tmp_table_coluna = FindControlRecursive(table, "tblinha_" + nome_campo_antes_anterior);

                        if (tmp_table_coluna != null)
                        {
                            //nome_campo_antes_anterior = nome_campo_anterior;
                            table_linha = (tmp_table_coluna as TableRow);
                            table_linha.Cells[1].ColumnSpan = 1;
                            table_linha.Cells[1].Width = Unit.Pixel(180);

                            //table_linha.Cells[1].ID = compkrk.NomeCampo.Replace("txt", "col");
                        }

                        //Control tmp_table_coluna2 = FindControlRecursive(table, "tblinha_" + nome_campo_anterior);

                        //if (tmp_table_coluna2 != null)
                        //{
                        //    table_linha = (tmp_table_coluna2 as TableRow);
                        //    table_linha.Cells[0].ColumnSpan = 2;
                        //    table_linha.Cells[0].Width = Unit.Pixel(180);
                        //}
                    }
                }
                else
                {
                    nome_campo_antes_anterior = "";
                    table_coluna2.ColumnSpan = 3;
                }
                table_coluna2.Controls.Add(lbl);

                if (compkrk.TipoCampo != KrksaComp.TiposCampo.Label)
                {
                    System.Web.UI.WebControls.Label lbl2 = new Label();
                    lbl2.Text = "<br />";
                    table_coluna2.Controls.Add(lbl2);
                }
            }


            //form1.Controls.Add(new LiteralControl("<br/>"));//<--para inserir um texto


            if (compkrk.TipoCampo == KrksaComp.TiposCampo.Label)
            {
                System.Web.UI.WebControls.Label lblsep = new Label();
                lblsep.CssClass = "lblSeparador";
                lblsep.ID = compkrk.NomeCampo;
                lblsep.Text = compkrk.NomeLabelCampo;
                lblsep.Visible = compkrk.VisivelWebForm;
                //lblsep.ForeColor = Color.White;

                if (compkrk.NomeClasseCSS != KrksaComp.NomesClasseCSS.geral)
                {
                    lblsep.CssClass += " " + compkrk.NomeClasseCSS.ToString();
                }

                
                
                //if (compkrk.VisivelWebForm)
                //    table_coluna.BackColor = Color.Navy;
                //table_coluna.HorizontalAlign = HorizontalAlign.Center;

                if (SessionEvento.TpFolhaCadastroWeb == "1") 
                {
                    table_coluna.CssClass = "colSeparador";
                    table_coluna.ColumnSpan = 2;
                    table_coluna.Controls.Add(lblsep);
                }
                else if (SessionEvento.TpFolhaCadastroWeb == "2")
                {
                   
                    table_coluna2.CssClass = "colSeparador";
                    table_coluna2.Controls.Add(lblsep);
                }

            }
            else
            if ((compkrk.TipoCampo != KrksaComp.TiposCampo.Lista) &&
                (compkrk.TipoCampo != KrksaComp.TiposCampo.Data) &&
                (compkrk.TipoCampo != KrksaComp.TiposCampo.Hora) &&
                (compkrk.TipoCampo != KrksaComp.TiposCampo.DataHora) &&
                (compkrk.TipoCampo != KrksaComp.TiposCampo.Moeda) &&
                (compkrk.TipoCampo != KrksaComp.TiposCampo.VerdadeiroFalso) &&
                (compkrk.TipoCampo != KrksaComp.TiposCampo.CheckList) &&
                (compkrk.TipoCampo != KrksaComp.TiposCampo.OptionList))
            {
                if ((compkrk.NomeCampo == "txt_cdParticipante") && (SessionTipoSistema == "NRM"))//(SessionEvento.CdEvento != "002105"))
                {
                    lbl.Visible = compkrk.VisivelWebForm;

                    Label lblCdPart = new Label();
                    lblCdPart.Visible = true;
                    lblCdPart.ID = compkrk.NomeCampo;
                    //txtbx.Width = compkrk.TamComprimento;
                    lblCdPart.Visible = compkrk.VisivelWebForm;
                    lblCdPart.ForeColor = Color.Maroon;
                    lblCdPart.Font.Size = 14;
                    lblCdPart.Font.Bold = true;
                    lblCdPart.CssClass = "txt_cdparticipante";

                    table_coluna2.Controls.Add(lblCdPart);

                }
                else if ((compkrk.NomeCampo != "txt_dsSenhaWeb") && (compkrk.NomeCampo != "txt_dsEmail"))
                {
                    lbl.Visible = compkrk.VisivelWebForm;

                    TextBox txtbx = new TextBox();
                    txtbx.Visible = compkrk.VisivelWebForm;
                    txtbx.ID = compkrk.NomeCampo;

                    
                    if (compkrk.NomeClasseCSS == KrksaComp.NomesClasseCSS.geral)
                    {
                        txtbx.Width = compkrk.TamComprimento;
                    }

                    txtbx.CssClass = compkrk.NomeClasseCSS.ToString();

                    if (compkrk.MaiusculaMinuscula == System.Windows.Forms.CharacterCasing.Upper)
                        txtbx.CssClass += " maiusculo";
                    else if (compkrk.MaiusculaMinuscula == System.Windows.Forms.CharacterCasing.Lower)
                        txtbx.CssClass += " minusculo";
                    
                    //else if (compkrk.MaiusculaMinuscula == System.Windows.Forms.CharacterCasing.Normal)
                    //    txtbx.CssClass = "txtNormal";
                    
                        
                    




                    txtbx.SkinID = compkrk.Mascara;

                    if (compkrk.ExecutarAcaoAoSair)
                    {
                        txtbx.AutoPostBack = true;
                        txtbx.TextChanged += new EventHandler(this.txt_TextChanged);
                    }

                    

                    if (compkrk.TipoCampo == KrksaComp.TiposCampo.TextoLongo)
                    {
                        txtbx.TextMode = TextBoxMode.MultiLine;
                        txtbx.Height = compkrk.TamAltura;
                    }

                    if (compkrk.NomeCampo == "txt_dsCampoExtraPreCad")
                        txtbx.ToolTip = compkrk.NomeLabelCampo;

                    txtbx.ReadOnly = compkrk.SomenteLeituraWebForm;
                    txtbx.Enabled = compkrk.HabilitadoWebForm;

                    if (SessionTipoSistema == "CRD")
                    {
                        if (((SessionOperacao == "") || (SessionOperacao == "INICIO")) && (!compkrk.AlvoDePesquisaExata))
                            txtbx.Enabled = false;
                        else if ((SessionOperacao != "") && (SessionOperacao != "INICIO") && (compkrk.NomeCampo == "txt_cdParticipante"))
                            txtbx.Enabled = false;
                    }

                    table_coluna2.Controls.Add(txtbx);
                    //if (compkrk.VisivelWebForm)
                    //{
                        if (((SessionTipoSistema == "CRD") && (compkrk.AlvoDePesquisaExata)) && 
                            ((SessionOperacao != "ALTERAR") && (SessionOperacao != "INCLUIR")))
                        {
                            ImageButton btnpesquisar = new ImageButton();
                            btnpesquisar.ID = "btnPesq" + compkrk.NomeCampo;
                            btnpesquisar.ImageUrl = "img/accept18x18_old.png";
                            //btnpesquisar.Text = "Pesquisar";
                            //btnpesquisar.CssClass = "botoes";

                            btnpesquisar.CausesValidation = false;
                            btnpesquisar.Click += new System.Web.UI.ImageClickEventHandler(btnPesquisar_Click);
                            table_coluna2.Controls.Add(btnpesquisar);

                            ToolkitScriptManager1.RegisterPostBackControl(btnpesquisar);
                            
                        }

                        if (compkrk.NomeCampo == "txt_nuCPFCNPJ")
                        {
                            CustomValidator cval = new CustomValidator();
                            cval.ID = "cpfValidar";
                            cval.ControlToValidate = compkrk.NomeCampo;
                            cval.Display = ValidatorDisplay.Dynamic;
                            cval.SetFocusOnError = true;
                            //cval.CssClass = "Validators";

                            cval.ForeColor = Color.Red;
                            if (SessionEvento.CdEvento == "005701")
                                cval.ForeColor = Color.Yellow;

                            cval.ServerValidate += new ServerValidateEventHandler(this.ServerValidar);


                            table_coluna2.Controls.Add(cval);

                            //if ((SessionEvento.CdCliente == "0013") || (SessionEvento.CdCliente == "0003"))
                            
                            //if ((SessionEvento.FlUtilizarDadoEventoAnterior) && ((Request.QueryString["cpf"] == null) || (Request.QueryString["cpf"].ToString() == "")))
                            //{
                            //    Button btndadosanterioes = new Button();
                            //    btndadosanterioes.ID = "btnDadosAnteriores";
                            //    btndadosanterioes.Text = "<- Buscar Dados em Eventos Anteriores";
                            //    btndadosanterioes.CssClass = "botoes";
                            //    btndadosanterioes.CausesValidation = false;
                            //    btndadosanterioes.Click += new System.EventHandler(btnDadosAnteriores_Click);
                            //    table_coluna2.Controls.Add(btndadosanterioes);
                            //}
                              
                        }

                        if ((compkrk.NomeCampo == "txt_nuCEP") && (SessionPais == "BRASIL"))
                        {

                            //if (SessionEvento.CdEvento == "001302")
                            //{
                            Button btncep = new Button();
                            btncep.ID = "btnCEP";
                            btncep.Text = "Verificar";
                            btncep.CssClass = "botoes";
                            btncep.CausesValidation = false;
                            btncep.Click += new System.EventHandler(btnCEP_Click);
                            table_coluna2.Controls.Add(btncep);

                            Label lblcep = new Label();
                            lblcep.ID = "lblMsgCEP";
                            lblcep.ForeColor = Color.Red;
                            lblcep.Text = "CEP não encontrado!";
                            lblcep.Visible = false;
                            table_coluna2.Controls.Add(lblcep);
                            //}
                        }

                        if (compkrk.Mascara.Trim() != "")
                        {


                            if ((compkrk.NomeLabelCampo.ToUpper().Contains("CELULAR")) || (compkrk.NomeLabelCampo.ToUpper().Contains("FONE")))
                            {
                                txtbx.Attributes["onkeydown"] = "FoneMascarar(this, event)";
                                txtbx.Attributes["onkeypress"] = "FoneMascarar(this, event)";
                                txtbx.Attributes["onkeyup"] = "FoneMascarar(this, event)";
                                //txtbx.Attributes["onkeydown"] = "Mascarar(this, event, '" + compkrk.Mascara + "')";
                                //txtbx.Attributes["onkeypress"] = "Mascarar(this, event, '" + compkrk.Mascara + "')";
                                //txtbx.Attributes["onkeyup"] = "Mascarar(this, event, '" + compkrk.Mascara + "')";

                                txtbx.MaxLength = 15;
                            }
                            else
                            {
                                txtbx.Attributes["onkeydown"] = "Mascarar(this, event, '" + compkrk.Mascara + "')";
                                txtbx.Attributes["onkeypress"] = "Mascarar(this, event, '" + compkrk.Mascara + "')";
                                txtbx.Attributes["onkeyup"] = "Mascarar(this, event, '" + compkrk.Mascara + "')";
                                txtbx.MaxLength = compkrk.Mascara.Length;
                            }



                            if (compkrk.Obrigatorio)
                            {
                                RequiredFieldValidator refv = new RequiredFieldValidator();
                                refv.ForeColor = Color.Red;

                                if (SessionEvento.CdEvento == "005701")
                                    refv.ForeColor = Color.Yellow;

                                if (SessionEvento.CdEvento == "008201")
                                    refv.ForeColor = Color.Yellow;
                                
                                refv.ErrorMessage = compkrk.ObrigatorioMensagem;
                                refv.ControlToValidate = txtbx.ID;
                                refv.Visible = compkrk.VisivelWebForm;
                                refv.Display = ValidatorDisplay.Dynamic;
                                refv.SetFocusOnError = true;
                                refv.ID = compkrk.NomeCampo + "_Req";

                                refv.CssClass = "Validators";

                                table_coluna2.Controls.Add(refv);



                                CustomValidator cval = new CustomValidator();
                                cval.ID = compkrk.NomeCampo + "_Validar";
                                cval.ControlToValidate = txtbx.ID;
                                //cval.CssClass = "Validators";
                                cval.SetFocusOnError = true;

                                cval.ForeColor = Color.Red;
                                if (SessionEvento.CdEvento == "005701")
                                    cval.ForeColor = Color.Yellow;

                                cval.Display = ValidatorDisplay.Dynamic;
                                cval.ServerValidate += new ServerValidateEventHandler(this.ServerValidar);


                                if (SessionIdioma == "PTBR")
                                    txtbx.ToolTip = "Obrigatório";
                                else if (SessionIdioma == "ENUS")
                                    txtbx.ToolTip = "Required";
                                else if (SessionIdioma == "ESP")
                                    txtbx.ToolTip = "Obligatorio";
                                else if (SessionIdioma == "FRA")
                                    txtbx.ToolTip = "Obligatoire";

                                cval.ErrorMessage = compkrk.ObrigatorioMensagem;

                                table_coluna2.Controls.Add(cval);

                                //if (compkrk.NomeCampo != "txt_nuCPFCNPJ")
                                //{
                                //    CustomValidator cval = new CustomValidator();
                                //    cval.ID = compkrk.NomeCampo + "_Validar";
                                //    cval.ControlToValidate = compkrk.NomeCampo;
                                //    cval.Display = ValidatorDisplay.Dynamic;
                                //    cval.ServerValidate += new ServerValidateEventHandler(this.ServerValidar);

                                //    table_coluna2.Controls.Add(cval);

                                //    txtbx.ToolTip = "Obrigatório";
                                //}
                            }

                            if ((compkrk.NomeLabelCampo.ToUpper().Contains("CELULAR")) && ((SessionEvento.CdEvento == "001002") || (SessionEvento.CdEvento == "001004") || (SessionEvento.CdEvento == "001008")))
                            {
                                Label lbltmp = new Label();
                                lbltmp.Text = "Uso exclusivo para envio de informações sobre o Congresso.";
                                table_coluna2.Controls.Add(lbltmp);
                            }

                            /*
                            MaskedEditExtender maskedExt = new MaskedEditExtender();
                            maskedExt.ID = "mskd_" + compkrk.NomeCampo;
                            maskedExt.TargetControlID = compkrk.NomeCampo;
                            maskedExt.MessageValidatorTip = true;
                            maskedExt.Enabled = true;
                            maskedExt.PromptCharacter = " ";

                            maskedExt.Mask = compkrk.Mascara.Replace(".", ",");
                            maskedExt.MaskType = MaskedEditType.None;
                            maskedExt.ClearMaskOnLostFocus = false;
                            
                            table_coluna2.Controls.Add(maskedExt);

                            MaskedEditValidator mskedval = new MaskedEditValidator();
                            mskedval.ID = "mskdv_" + compkrk.NomeCampo;
                            mskedval.ControlExtender = maskedExt.ID;
                            mskedval.ControlToValidate = compkrk.NomeCampo;
                            if (compkrk.Obrigatorio)
                            {
                                mskedval.IsValidEmpty = false;
                                mskedval.EmptyValueMessage = compkrk.ObrigatorioMensagem;
                                if (compkrk.NomeCampo != "txt_nuCPFCNPJ")
                                {
                                    CustomValidator cval = new CustomValidator();
                                    cval.ID = compkrk.NomeCampo + "_Validar";
                                    cval.ControlToValidate = compkrk.NomeCampo;
                                    cval.Display = ValidatorDisplay.Dynamic;
                                    cval.CssClass = "Validators";
                                    cval.ServerValidate += new ServerValidateEventHandler(this.ServerValidar);

                                    table_coluna2.Controls.Add(cval);

                                    txtbx.ToolTip = "Obrigatório";
                                }
                            }

                            //mskedval.InvalidValueMessage = "Valor Inválido";
                            mskedval.Display = ValidatorDisplay.Dynamic;


                            table_coluna2.Controls.Add(mskedval);
                            */
                        }
                        else
                        {

                            //RegularExpressionValidator rexv = new RegularExpressionValidator();
                            ////rexv.ID="RegularExpressionValidator11";
                            //rexv.ControlToValidate = txtbx.ID;
                            //rexv.Display= ValidatorDisplay.Dynamic;
                            //rexv.ErrorMessage="Valor do campo inválido.";
                            //rexv.ValidationExpression="[áéíóúàâêôãõüÁÉÍÓÚÀÂÊÔÃÕÜÇçºª@&\\-\\.\\,\\w\\s\\d´`]";
                            ////rexv.Visible = true;
                            //table_coluna2.Controls.Add(rexv);

                            //if ((compkrk.NomeLabelCampo.ToUpper().Contains("E-MAIL")) ||
                            //    (compkrk.NomeLabelCampo.ToUpper().Contains("EMAIL")))
                            //    txtbx.CssClass = "email";

                            txtbx.MaxLength = compkrk.QtdCaracteres;

                            if (compkrk.ExecutarAcaoAoSair)
                            {
                                txtbx.AutoPostBack = true;
                                txtbx.TextChanged += new EventHandler(this.txt_TextChanged);
                            }

                            if (compkrk.Obrigatorio)
                            {
                                RequiredFieldValidator refv = new RequiredFieldValidator();
                                refv.ForeColor = Color.Red;

                                if (SessionEvento.CdEvento == "005701")
                                    refv.ForeColor = Color.Yellow;

                                if (SessionEvento.CdEvento == "008201")
                                    refv.ForeColor = Color.Yellow;

                                refv.ID = compkrk.NomeCampo + "_Req";

                                //refv.CssClass = "Validators";
                                refv.ErrorMessage = compkrk.ObrigatorioMensagem;
                                refv.ControlToValidate = txtbx.ID;
                                refv.Visible = compkrk.VisivelWebForm;
                                refv.Display = ValidatorDisplay.Dynamic;
                                refv.SetFocusOnError = true;
                                table_coluna2.Controls.Add(refv);
                            }
                        }
                    //}
                }

            }
            else if (compkrk.TipoCampo == KrksaComp.TiposCampo.Lista)
            {
                DropDownList txtdrpl = new DropDownList();
                txtdrpl.Visible = compkrk.VisivelWebForm;

                //if (compkrk.ExecutarAcaoAoSair)
                //    txtdrpl.TextChanged += new EventHandler(this.txt_TextChanged);
                if (compkrk.ExecutarAcaoAoSair)
                {
                    txtdrpl.AutoPostBack = true;
                    txtdrpl.SelectedIndexChanged += new EventHandler(txtdrpl_TextChanged);
                }

                txtdrpl.ID = compkrk.NomeCampo;
                
                
                if (compkrk.NomeClasseCSS == KrksaComp.NomesClasseCSS.geral)
                {
                    txtdrpl.Width = compkrk.TamComprimento;
                }

                txtdrpl.CssClass = compkrk.NomeClasseCSS.ToString();

                if (compkrk.MaiusculaMinuscula == System.Windows.Forms.CharacterCasing.Upper)
                    txtdrpl.CssClass += " maiusculo";
                else if (compkrk.MaiusculaMinuscula == System.Windows.Forms.CharacterCasing.Lower)
                    txtdrpl.CssClass += " minusculo";

                

                lbl.Visible = compkrk.VisivelWebForm;

                txtdrpl.Enabled = compkrk.HabilitadoWebForm;

                if ((SessionTipoSistema == "CRD") && ((SessionOperacao == "") || (SessionOperacao == "INICIO")))
                    txtdrpl.Enabled = false;

                if (compkrk.AutoCarregar)
                {

                    if ((compkrk.NomeCampo != "txt_dsUF") && (compkrk.NomeCampo != "txt_noCidade"))
                    {
                        if ((SessionEvento.CdCliente != "0013") && (SessionEvento.CdCliente != "0048") && (SessionEvento.CdCliente != "0003"))
                        {
                            AutoPreenchimento(tmpCdEvento, txtdrpl, compkrk.AutoCarregar);
                        }
                        else
                        {
                            if ((compkrk.NomeCampo != "txt_dsAuxiliar8") && (compkrk.NomeCampo != "txt_dsAuxiliar9"))
                                AutoPreenchimento(tmpCdEvento, txtdrpl, compkrk.AutoCarregar);
                            else
                            {
                                CascadingDropDown cascadDD = new CascadingDropDown();
                                cascadDD.ID = "cascaDD_" + compkrk.NomeCampoBancoDados;
                                cascadDD.TargetControlID = txtdrpl.ID;
                                cascadDD.Category = (compkrk.NomeCampoBancoDados == "dsAuxiliar9" ? "dsMunicipio" : "dsUF");
                                cascadDD.ServicePath = "CascataDropDown.asmx";
                                cascadDD.ServiceMethod = "buscar_" + (compkrk.NomeCampoBancoDados == "dsAuxiliar9" ? "noCidade" : "dsUF");
                                if (compkrk.NomeCampo == "txt_dsAuxiliar9")
                                    cascadDD.ParentControlID = "txt_dsAuxiliar8";
                                table_coluna2.Controls.Add(cascadDD);                                
                            }
                        }
                    }
                    else
                    {
                        CascadingDropDown cascadDD = new CascadingDropDown();
                        cascadDD.ID = "cascaDD_" + compkrk.NomeCampoBancoDados;
                        cascadDD.TargetControlID = txtdrpl.ID;
                        cascadDD.Category = (compkrk.NomeCampoBancoDados == "noCidade" ? "dsMunicipio" : compkrk.NomeCampoBancoDados);
                        cascadDD.ServicePath = "CascataDropDown.asmx";
                        cascadDD.ServiceMethod = "buscar_" + compkrk.NomeCampoBancoDados;

                        if (compkrk.NomeCampo == "txt_noCidade")
                            cascadDD.ParentControlID = "txt_dsUF";
                        table_coluna2.Controls.Add(cascadDD);

                        //if (SessionEvento.CdEvento == "007702")
                        //{
                        //    if (compkrk.NomeCampo == "txt_dsUF")
                        //    {
                        //        UpdatePanel updPanel = new UpdatePanel();
                        //        updPanel.ID = "UpdatePanelTempLixoZero";

                        //        txtdrpl.AutoPostBack = true;
                        //        txtdrpl.SelectedIndexChanged += new System.EventHandler(this.txt_TempLixoZero_SelectedIndexChanged);


                        //        Panel pnlMsgTempLixoZero = new Panel();
                        //        pnlMsgTempLixoZero.ID = "pnlMsgTempLixoZero";
                        //        pnlMsgTempLixoZero.Width = table_coluna2.Width;
                        //        pnlMsgTempLixoZero.Visible = false;
                        //        pnlMsgTempLixoZero.CssClass = "pnlMsgCateg";
                        //        //pnlMsgCateg.BackColor= (Color)"#FFFFC0";
                        //        Label lblmsgTempLixoZero = new Label();
                        //        lblmsgTempLixoZero.ID = "lblMsgTempLixoZero";
                        //        //lblmsgcateg.ForeColor = Color.Red;
                        //        //lblmsgcateg.Font.Bold = true;
                        //        lblmsgTempLixoZero.CssClass = "lblMsgCateg";
                        //        pnlMsgTempLixoZero.Controls.Add(lblmsgTempLixoZero);

                        //        //updPanel.ContentTemplateContainer.Controls.Add(txtdrpl);
                        //        updPanel.ContentTemplateContainer.Controls.Add(pnlMsgTempLixoZero);

                        //        table_coluna2.Controls.Add(updPanel);



                        //    }
                        //    //else
                        //    //    table_coluna2.Controls.Add(txtdrpl);
                        //}
                    }
                }
                else
                {
                    CarregarListagem(txtdrpl, compkrk.ListaValores, compkrk.NomeCampoBancoDados, compkrk.MaiusculaMinuscula.ToString()); 
                    //txtchklst.Items.Clear();
                    //for (int j = 0; j < compkrk.ListaValores.Length; j++)
                    //{
                    //    txtchklst.Items.Add(compkrk.ListaValores[j].ToString());
                    //}

                }

                if (compkrk.NomeCampo == "txt_cdCategoria")
                {
                    UpdatePanel updPanel = new UpdatePanel();
                    updPanel.ID = "UpdatePanel335";

                    txtdrpl.AutoPostBack = true;
                    txtdrpl.SelectedIndexChanged += new System.EventHandler(this.txt_cdCategoria_SelectedIndexChanged);


                    Panel pnlMsgCateg = new Panel();
                    pnlMsgCateg.ID = "pnlMsgCateg";
                    pnlMsgCateg.Width = table_coluna2.Width;
                    pnlMsgCateg.Visible = false;
                    pnlMsgCateg.CssClass = "pnlMsgCateg";
                    //pnlMsgCateg.BackColor= (Color)"#FFFFC0";
                    Label lblmsgcateg = new Label();
                    lblmsgcateg.ID = "lblMsgCateg";
                    //lblmsgcateg.ForeColor = Color.Red;
                    //lblmsgcateg.Font.Bold = true;
                    lblmsgcateg.CssClass = "lblMsgCateg";
                    pnlMsgCateg.Controls.Add(lblmsgcateg);

                    updPanel.ContentTemplateContainer.Controls.Add(txtdrpl);
                    updPanel.ContentTemplateContainer.Controls.Add(pnlMsgCateg);

                    table_coluna2.Controls.Add(updPanel);

                    if ((SessionParticipante != null) && (SessionParticipante.FlConfirmacaoInscricao))
                        table_linha.Visible = false;

                }
                else
                    table_coluna2.Controls.Add(txtdrpl);

                
                
                //if (compkrk.VisivelWebForm)
                //{
                    if (compkrk.Obrigatorio)
                    {
                        RequiredFieldValidator refv = new RequiredFieldValidator();
                        refv.ForeColor = Color.Red;

                        if (SessionEvento.CdEvento == "005701")
                            refv.ForeColor = Color.Yellow;

                        refv.ID = compkrk.NomeCampo + "_Req";
                        //refv.CssClass = "Validators";
                        refv.ErrorMessage = compkrk.ObrigatorioMensagem;
                        refv.ControlToValidate = txtdrpl.ID;
                        refv.Visible = compkrk.VisivelWebForm;
                        refv.Display = ValidatorDisplay.Dynamic;

                        refv.SetFocusOnError = true;

                        table_coluna2.Controls.Add(refv);
                    }
                //}

            }
            else if (compkrk.TipoCampo == KrksaComp.TiposCampo.CheckList)
            {
                CheckBoxList txtchklst = new CheckBoxList();
                txtchklst.Visible = compkrk.VisivelWebForm;

                //if (compkrk.ExecutarAcaoAoSair)
                //    txtchklst.TextChanged += new EventHandler(this.txt_TextChanged);

                txtchklst.ID = compkrk.NomeCampo;
                //txtchklst.Width = compkrk.TamComprimento;

                if (compkrk.NomeClasseCSS == KrksaComp.NomesClasseCSS.geral)
                {
                    txtchklst.Width = compkrk.TamComprimento;
                }

                txtchklst.CssClass = compkrk.NomeClasseCSS.ToString();

                txtchklst.Height = compkrk.TamAltura;
                

                lbl.Visible = compkrk.VisivelWebForm;

                txtchklst.Enabled = compkrk.HabilitadoWebForm;

                if ((SessionTipoSistema == "CRD") && ((SessionOperacao == "") || (SessionOperacao == "INICIO")))
                    txtchklst.Enabled = false;

                txtchklst.RepeatColumns = compkrk.NomeCampoColuna;
                txtchklst.RepeatDirection = System.Web.UI.WebControls.RepeatDirection.Horizontal;
                txtchklst.RepeatLayout = RepeatLayout.Table;

                if (compkrk.AutoCarregar)
                {
                    //if ((compkrk.NomeCampo != "txt_dsUF") && (compkrk.NomeCampo != "txt_noCidade"))
                    //    AutoPreenchimento(tmpCdEvento, txtchklst, compkrk.AutoCarregar);
                    
                }
                else
                {
                    CarregarListagem(txtchklst, compkrk.ListaValores, compkrk.NomeCampoBancoDados, compkrk.MaiusculaMinuscula.ToString());
                    

                }

                table_coluna2.Controls.Add(txtchklst);

                

            }
            else if (compkrk.TipoCampo == KrksaComp.TiposCampo.OptionList)
            {
                RadioButtonList txtchklst = new RadioButtonList();
                txtchklst.Visible = compkrk.VisivelWebForm;

                //if (compkrk.ExecutarAcaoAoSair)
                //    txtchklst.TextChanged += new EventHandler(this.txt_TextChanged);

                txtchklst.ID = compkrk.NomeCampo;
                //txtchklst.Width = compkrk.TamComprimento;

                if (compkrk.NomeClasseCSS == KrksaComp.NomesClasseCSS.geral)
                {
                    txtchklst.Width = compkrk.TamComprimento;
                }

                txtchklst.CssClass = compkrk.NomeClasseCSS.ToString();

                txtchklst.Height = compkrk.TamAltura;


                lbl.Visible = compkrk.VisivelWebForm;

                txtchklst.Enabled = compkrk.HabilitadoWebForm;

                if ((SessionTipoSistema == "CRD") && ((SessionOperacao == "") || (SessionOperacao == "INICIO")))
                    txtchklst.Enabled = false;

                txtchklst.RepeatColumns = compkrk.NomeCampoColuna;
                txtchklst.RepeatDirection = System.Web.UI.WebControls.RepeatDirection.Horizontal;
                txtchklst.RepeatLayout = RepeatLayout.Table;

                if (compkrk.AutoCarregar)
                {
                    //if ((compkrk.NomeCampo != "txt_dsUF") && (compkrk.NomeCampo != "txt_noCidade"))
                    //    AutoPreenchimento(tmpCdEvento, txtchklst, compkrk.AutoCarregar);

                }
                else
                {
                    CarregarListagem(txtchklst, compkrk.ListaValores, compkrk.NomeCampoBancoDados, compkrk.MaiusculaMinuscula.ToString());


                }

                if (compkrk.Obrigatorio)
                {
                    RequiredFieldValidator refv = new RequiredFieldValidator();
                    refv.ForeColor = Color.Red;

                    

                    refv.ID = compkrk.NomeCampo + "_Req";
                    //refv.CssClass = "Validators";
                    refv.ErrorMessage = compkrk.ObrigatorioMensagem;
                    refv.ControlToValidate = txtchklst.ID;
                    refv.Visible = compkrk.VisivelWebForm;
                    refv.Display = ValidatorDisplay.Dynamic;

                    refv.SetFocusOnError = true;

                    table_coluna2.Controls.Add(refv);
                }

                table_coluna2.Controls.Add(txtchklst);



            }
            else if ((compkrk.TipoCampo == KrksaComp.TiposCampo.Data) ||
                    (compkrk.TipoCampo == KrksaComp.TiposCampo.Hora) ||
                    (compkrk.TipoCampo == KrksaComp.TiposCampo.DataHora))
            {
                TextBox txtbxdt = new TextBox();
                txtbxdt.Visible = compkrk.VisivelWebForm;
                txtbxdt.ID = compkrk.NomeCampo;
                

                if (compkrk.NomeClasseCSS == KrksaComp.NomesClasseCSS.geral)
                {
                    txtbxdt.Width = compkrk.TamComprimento;
                }

                txtbxdt.CssClass = compkrk.NomeClasseCSS.ToString();
                                

                txtbxdt.ReadOnly = compkrk.SomenteLeituraWebForm;
                txtbxdt.Enabled = compkrk.HabilitadoWebForm;

                if ((SessionTipoSistema == "CRD") && ((SessionOperacao == "") || (SessionOperacao == "INICIO")))
                    txtbxdt.Enabled = false;

                if (compkrk.ExecutarAcaoAoSair)
                {
                    txtbxdt.AutoPostBack = true;
                    txtbxdt.TextChanged += new EventHandler(this.txt_TextChanged);
                }

                lbl.Visible = compkrk.VisivelWebForm;

                table_coluna2.Controls.Add(txtbxdt);

                if (compkrk.VisivelWebForm)
                {
                    MaskedEditExtender maskedExt = new MaskedEditExtender();
                    maskedExt.ID = "mskd" + compkrk.NomeCampo;
                    maskedExt.TargetControlID = compkrk.NomeCampo;
                    maskedExt.MessageValidatorTip = true;
                    maskedExt.Enabled = true;
                    
                    // maskedExt.ClearMaskOnLostFocus = false;
                    maskedExt.PromptCharacter = " ";

                    if (compkrk.TipoCampo == KrksaComp.TiposCampo.Data)
                    {
                        maskedExt.Mask = compkrk.Mascara;
                        maskedExt.MaskType = MaskedEditType.Date;

                        table_coluna2.Controls.Add(maskedExt);
                        table_coluna2.Controls.Add(new LiteralControl(" "));

                        ImageButton imbtn = new ImageButton();
                        imbtn.ID = "ImgBntCalc";
                        imbtn.ImageUrl = "img/Calendar_scheduleHS.png";
                        imbtn.CausesValidation = false;
                        table_coluna2.Controls.Add(imbtn);


                        CalendarExtender calEx = new CalendarExtender();
                        calEx.ID = "calEx_" + compkrk.NomeCampo;
                        calEx.TargetControlID = compkrk.NomeCampo;
                        calEx.PopupButtonID = "ImgBntCalc";
                        calEx.Format = "dd/MM/yyyy";

                        table_coluna2.Controls.Add(calEx);
                    }
                    else
                    {
                        maskedExt.Mask = compkrk.Mascara;
                        if (compkrk.TipoCampo == KrksaComp.TiposCampo.Hora)
                            maskedExt.MaskType = MaskedEditType.Time;
                        else
                            maskedExt.MaskType = MaskedEditType.DateTime;
                        maskedExt.AcceptAMPM = false;
                        table_coluna2.Controls.Add(maskedExt);
                    }


                    MaskedEditValidator mskedval = new MaskedEditValidator();
                    mskedval.ID = "mskdv_" + compkrk.NomeCampo;
                    mskedval.ControlExtender = maskedExt.ID;
                    mskedval.ControlToValidate = compkrk.NomeCampo;
                    mskedval.SetFocusOnError = true;
                    if (compkrk.Obrigatorio)
                    {
                        mskedval.IsValidEmpty = false;
                        mskedval.EmptyValueMessage = compkrk.ObrigatorioMensagem;
                    }

                    //mskedval.InvalidValueMessage = "Valor Inválido";

                    if (SessionIdioma == "PTBR")
                        mskedval.InvalidValueMessage = "Valor Inválido";
                    else if (SessionIdioma == "ENUS")
                        mskedval.InvalidValueMessage = "Invalid Entry";
                    else if (SessionIdioma == "ESP")
                        mskedval.InvalidValueMessage = "Entrada no válida";
                    else if (SessionIdioma == "FRA")
                        mskedval.InvalidValueMessage = "Entrée invalide";

                    mskedval.Display = ValidatorDisplay.Dynamic;


                    table_coluna2.Controls.Add(mskedval);

                }
            }
            else
            if (compkrk.TipoCampo == KrksaComp.TiposCampo.Moeda)
            {

                TextBox txtbxdindind = new TextBox();
                txtbxdindind.Visible = compkrk.VisivelWebForm;
                txtbxdindind.ID = compkrk.NomeCampo;
                

                if (compkrk.NomeClasseCSS == KrksaComp.NomesClasseCSS.geral)
                {
                    txtbxdindind.Width = compkrk.TamComprimento;
                }

                txtbxdindind.CssClass = compkrk.NomeClasseCSS.ToString();
                                

                txtbxdindind.ReadOnly = compkrk.SomenteLeituraWebForm;
                txtbxdindind.Enabled = compkrk.HabilitadoWebForm;

                if ((SessionTipoSistema == "CRD") && ((SessionOperacao == "") || (SessionOperacao == "INICIO")))
                    txtbxdindind.Enabled = false;

                if (compkrk.ExecutarAcaoAoSair)
                    txtbxdindind.TextChanged += new EventHandler(this.txt_TextChanged);

                lbl.Visible = compkrk.VisivelWebForm;

                table_coluna2.Controls.Add(txtbxdindind);

                if (compkrk.VisivelWebForm)
                {
                    MaskedEditExtender maskedExt = new MaskedEditExtender();
                    maskedExt.ID = "mskd_" + compkrk.NomeCampo;
                    maskedExt.TargetControlID = compkrk.NomeCampo;
                    maskedExt.MessageValidatorTip = true;
                    maskedExt.Enabled = true;
                    //maskedExt.ClearMaskOnLostFocus = false;

                    maskedExt.PromptCharacter = " ";

                    maskedExt.Mask = compkrk.Mascara.Replace(",", "*").Replace(".", ",").Replace("*", ".");
                    maskedExt.MaskType = MaskedEditType.Number;
                    maskedExt.InputDirection = MaskedEditInputDirection.RightToLeft;
                    maskedExt.AcceptNegative = MaskedEditShowSymbol.Left;
                    maskedExt.DisplayMoney = MaskedEditShowSymbol.Left;
                    maskedExt.DisplayMoney = MaskedEditShowSymbol.None;

                    table_coluna2.Controls.Add(maskedExt);

                    MaskedEditValidator mskedval = new MaskedEditValidator();
                    mskedval.ID = "mskdv_" + compkrk.NomeCampo;
                    mskedval.ControlExtender = maskedExt.ID;
                    mskedval.ControlToValidate = compkrk.NomeCampo;
                    mskedval.SetFocusOnError = true;
                    if (compkrk.Obrigatorio)
                    {
                        mskedval.IsValidEmpty = false;
                        mskedval.EmptyValueMessage = compkrk.ObrigatorioMensagem;
                    }


                    table_coluna2.Controls.Add(mskedval);
                }
            }
            else
            if (compkrk.TipoCampo == KrksaComp.TiposCampo.VerdadeiroFalso)
            {

                CheckBox chk = new CheckBox();
                chk.Visible = compkrk.VisivelWebForm;
                chk.ID = compkrk.NomeCampo;
                chk.Text = compkrk.NomeLabelCampo;

                chk.Enabled = compkrk.HabilitadoWebForm;

                if ((SessionTipoSistema == "CRD") && ((SessionOperacao == "") || (SessionOperacao == "INICIO")))
                    chk.Enabled = false;

                table_coluna2.Controls.Add(chk);

            }

            

            if (compkrk.NomeCampo == "txt_dsSenhaWeb")
            {
                if (tmpCdPart == "")
                {
                    //lbl.Text = "SENHA";
                    lbl.Visible = compkrk.VisivelWebForm;

                    TextBox txtbx = new TextBox();
                    txtbx.Visible = compkrk.VisivelWebForm;
                    txtbx.ID = compkrk.NomeCampo;
                    //txtbx.Width = compkrk.TamComprimento;
                    txtbx.TextMode = TextBoxMode.Password;
                    //txtbx.CssClass = "senha";

                    if ((SessionEvento.CdEvento == "010901") && (SessionTipoAcesso != null) && (SessionTipoAcesso != "NRM"))
                        table_linha.Visible = false;

                    if (compkrk.NomeClasseCSS == KrksaComp.NomesClasseCSS.geral)
                    {
                        txtbx.Width = compkrk.TamComprimento;
                    }

                    txtbx.CssClass = compkrk.NomeClasseCSS.ToString();
                    

                    table_coluna2.Controls.Add(txtbx);
                    if (compkrk.VisivelWebForm)
                    {
                        if (compkrk.Obrigatorio)
                        {
                            RequiredFieldValidator rfv = new RequiredFieldValidator();
                            rfv.ID = "rfv_" + compkrk.NomeCampo;
                            rfv.ControlToValidate = compkrk.NomeCampo;
                            rfv.Display = ValidatorDisplay.Dynamic;
                            rfv.ForeColor = Color.Red;

                            if (SessionEvento.CdEvento == "005701")
                                rfv.ForeColor = Color.Yellow;
                            //rfv.CssClass = "Validators";
                            rfv.ErrorMessage = compkrk.ObrigatorioMensagem;

                            table_coluna2.Controls.Add(rfv);
                        }

                        PasswordStrength pws = new PasswordStrength();
                        pws.ID = "PasswordStrength2";
                        pws.TargetControlID = compkrk.NomeCampo;
                        pws.DisplayPosition = DisplayPosition.RightSide;
                        pws.StrengthIndicatorType = StrengthIndicatorTypes.BarIndicator;
                        pws.PreferredPasswordLength = 6;
                        //pws.HelpStatusLabelID="TextBox2_HelpLabel"

                        pws.StrengthStyles = "BarIndicator_TextBox2_weak;BarIndicator_TextBox2_average;BarIndicator_TextBox2_good";

                        pws.BarBorderCssClass = "BarBorder_TextBox2";
                        pws.MinimumNumericCharacters = 1;
                        pws.MinimumSymbolCharacters = 1;
                        pws.TextStrengthDescriptions = "Very Poor;Weak;Average;Strong;Excellent";
                        pws.RequiresUpperAndLowerCaseCharacters = true;
                        

                        table_coluna2.Controls.Add(pws);
                    }

                    if (SessionEvento.TpFolhaCadastroWeb == "1") 
                        table_linha.Controls.Add(table_coluna);

                    table_linha.Controls.Add(table_coluna2);

                    if ((compkrk.VisivelWebForm) && (SessionEvento.FlAutenticacaoWeb))
                    {
                        table.Controls.Add(table_linha);

                        TableRow table_linha_pw = new TableRow();

                        TableCell table_coluna_pw = new TableCell();
                        //table_coluna_pw.Width = 190;
                        //table_coluna_pw.HorizontalAlign = HorizontalAlign.Right;
                        TableCell table_coluna2_pw = new TableCell();

                        if ((SessionEvento.CdEvento == "010901") && (SessionTipoAcesso != null) && (SessionTipoAcesso != "NRM"))
                            table_linha_pw.Visible = false;

                        System.Web.UI.WebControls.Label lbl_p = new Label();
                        
                        lbl_p.CssClass = "lblTitulocampo";

                        if (SessionIdioma == "PTBR")
                            lbl_p.Text = " Repetir ";
                        else if (SessionIdioma == "ENUS")
                            lbl_p.Text = " Re-enter ";
                        else if (SessionIdioma == "ESP")
                            lbl_p.Text = " Confirmar ";
                        else if (SessionIdioma == "FRA")
                            lbl_p.Text = " Confirmer ";

                        lbl_p.Text += compkrk.NomeLabelCampo;


                        if (SessionEvento.TpFolhaCadastroWeb == "1") 
                        {
                            table_coluna_pw.Width = 190;
                            //table_coluna.BorderWidth = 1;
                            table_coluna_pw.HorizontalAlign = HorizontalAlign.Right;
                            table_coluna_pw.Controls.Add(lbl_p);
                        }
                        else
                        {
                            //table_coluna2.BorderWidth = 1;
                            table_coluna2_pw.ColumnSpan = 2;
                            table_coluna2_pw.Controls.Add(lbl_p);

                            System.Web.UI.WebControls.Label lbl2 = new Label();
                            lbl2.Text = "<br />";
                            table_coluna2_pw.Controls.Add(lbl2);
                            
                        }

                        //table_coluna_pw.Controls.Add(lbl_p);

                        TextBox txtbx2 = new TextBox();
                        txtbx2.Visible = true;
                        txtbx2.ID = compkrk.NomeCampo + "_validar";
                        //txtbx2.Width = compkrk.TamComprimento;
                        txtbx2.TextMode = TextBoxMode.Password;
                        //txtbx2.CssClass = "senha";

                        if (compkrk.NomeClasseCSS == KrksaComp.NomesClasseCSS.geral)
                        {
                            txtbx2.Width = compkrk.TamComprimento;
                        }

                        txtbx2.CssClass = compkrk.NomeClasseCSS.ToString();

                        

                        table_coluna2_pw.Controls.Add(txtbx2);

                        if (compkrk.Obrigatorio)
                        {
                            RequiredFieldValidator rfv2 = new RequiredFieldValidator();
                            rfv2.ID = "rfv2_" + compkrk.NomeCampo;
                            rfv2.ControlToValidate = compkrk.NomeCampo + "_validar";
                            rfv2.Display = ValidatorDisplay.Dynamic;
                            rfv2.ForeColor = Color.Red;
                            rfv2.SetFocusOnError = true;
                            if (SessionEvento.CdEvento == "005701")
                                rfv2.ForeColor = Color.Yellow;
                            //rfv2.CssClass = "Validators";
                            rfv2.ErrorMessage = compkrk.ObrigatorioMensagem;

                            table_coluna2_pw.Controls.Add(rfv2);
                        }

                        CompareValidator compVal = new CompareValidator();
                        compVal.ID = "compVal_" + compkrk.NomeCampo;
                        compVal.ControlToCompare = compkrk.NomeCampo;
                        compVal.ControlToValidate = compkrk.NomeCampo + "_validar";
                        compVal.ForeColor = Color.Red;
                        compVal.SetFocusOnError = true;

                        if (SessionEvento.CdEvento == "005701")
                            compVal.ForeColor = Color.Yellow;
                        //compVal.CssClass = "Validators";
                        
                        compVal.Display = ValidatorDisplay.Dynamic;

                        //compVal.ErrorMessage = compkrk.NomeLabelCampo + " e " + compkrk.NomeLabelCampo + " 2";

                        if (SessionIdioma == "PTBR")
                            compVal.ErrorMessage = compkrk.NomeLabelCampo + " e " + compkrk.NomeLabelCampo + " 2 diferentes ";
                        else if (SessionIdioma == "ENUS")
                            compVal.ErrorMessage = " Different " + compkrk.NomeLabelCampo + " and " + compkrk.NomeLabelCampo;
                        else if (SessionIdioma == "ESP")
                            compVal.ErrorMessage = " Diferente " + compkrk.NomeLabelCampo + " y " + compkrk.NomeLabelCampo;
                        else if (SessionIdioma == "FRA")
                            compVal.ErrorMessage = compkrk.NomeLabelCampo + " e " + compkrk.NomeLabelCampo + " 2 différente";

                        

                        table_coluna2_pw.Controls.Add(compVal);
                        if (SessionEvento.TpFolhaCadastroWeb == "1") 
                            table_linha_pw.Controls.Add(table_coluna_pw);

                        table_linha_pw.Controls.Add(table_coluna2_pw);

                        table.Controls.Add(table_linha_pw);
                    }
                }
            }
            else if (compkrk.NomeCampo == "txt_dsEmail")
            {
                lbl.Visible = compkrk.VisivelWebForm;

                TextBox txtbx = new TextBox();
                txtbx.Visible = compkrk.VisivelWebForm;
                txtbx.ID = compkrk.NomeCampo;
                //txtbx.Width = compkrk.TamComprimento;
                //txtbx.CssClass = "email";


                if (compkrk.NomeClasseCSS == KrksaComp.NomesClasseCSS.geral)
                {
                    txtbx.Width = compkrk.TamComprimento;
                }

                txtbx.CssClass = compkrk.NomeClasseCSS.ToString();

                if (compkrk.MaiusculaMinuscula == System.Windows.Forms.CharacterCasing.Upper)
                    txtbx.CssClass += " maiusculo";
                else if (compkrk.MaiusculaMinuscula == System.Windows.Forms.CharacterCasing.Lower)
                    txtbx.CssClass += " minusculo";

                txtbx.ReadOnly = compkrk.SomenteLeituraWebForm;
                txtbx.Enabled = compkrk.HabilitadoWebForm;

                if ((SessionTipoSistema == "CRD") && ((SessionOperacao == "") || (SessionOperacao == "INICIO")))
                    txtbx.Enabled = false;


                



                table_coluna2.Controls.Add(txtbx);


                RegularExpressionValidator rgex = new RegularExpressionValidator();
                rgex.ID = compkrk.NomeCampo + "_REGEX";
                rgex.ControlToValidate = txtbx.ID;
                rgex.ErrorMessage = "E-mail inválido";
                rgex.ValidationExpression = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                //rgex.ValidationExpression = @"^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
                rgex.SetFocusOnError = true;
                rgex.Display = ValidatorDisplay.Dynamic;
                table_coluna2.Controls.Add(rgex);

                if (compkrk.VisivelWebForm)
                {
                    if (compkrk.Obrigatorio)
                    {
                        RequiredFieldValidator rfv = new RequiredFieldValidator();
                        rfv.ID = "rfv_" + compkrk.NomeCampo;
                        rfv.ControlToValidate = compkrk.NomeCampo;
                        rfv.Display = ValidatorDisplay.Dynamic;
                        rfv.ForeColor = Color.Red;
                        rfv.SetFocusOnError = true;

                        if (SessionEvento.CdEvento == "005701")
                            rfv.ForeColor = Color.Yellow;
                        //rfv.CssClass = "Validators";
                        rfv.ErrorMessage = compkrk.ObrigatorioMensagem;

                        table_coluna2.Controls.Add(rfv);
                    }

                    CustomValidator cval = new CustomValidator();
                    cval.ID = "emailValidar";
                    cval.ControlToValidate = compkrk.NomeCampo;
                    cval.Display = ValidatorDisplay.Dynamic;
                    cval.ForeColor = Color.Red;
                    cval.SetFocusOnError = true;

                    if (SessionEvento.CdEvento == "005701")
                        cval.ForeColor = Color.Yellow;
                    //cval.CssClass = "Validators";
                    cval.ServerValidate += new ServerValidateEventHandler(this.ServerValidar);

                    table_coluna2.Controls.Add(cval);
                }

                if (SessionEvento.TpFolhaCadastroWeb == "1") 
                    table_linha.Controls.Add(table_coluna);
                
                table_linha.Controls.Add(table_coluna2);
                    
                table.Controls.Add(table_linha);
                

                if ((tmpCdPart == "") && (compkrk.VisivelWebForm))// && (SessionEvento.FlAutenticacaoWeb))
                {
                    TableRow table_linha_pw = new TableRow();

                    TableCell table_coluna_pw = new TableCell();
                    //table_coluna_pw.Width = 190;
                    //table_coluna_pw.HorizontalAlign = HorizontalAlign.Right;
                    TableCell table_coluna2_pw = new TableCell();


                    System.Web.UI.WebControls.Label lbl_p = new Label();
                    lbl_p.CssClass = "lblTitulocampo";

                    //lbl_p.Text = " Repetir " + compkrk.NomeLabelCampo;
                    if (SessionIdioma == "PTBR")
                        lbl_p.Text = " Repetir ";
                    else if (SessionIdioma == "ENUS")
                        lbl_p.Text = " Re-enter ";
                    else if (SessionIdioma == "ESP")
                        lbl_p.Text = " Confirmar ";
                    else if (SessionIdioma == "FRA")
                        lbl_p.Text = " Confirmer ";

                    lbl_p.Text += compkrk.NomeLabelCampo;

                    if (SessionEvento.TpFolhaCadastroWeb == "1") 
                    {
                        table_coluna_pw.Width = 190;
                        //table_coluna.BorderWidth = 1;
                        table_coluna_pw.HorizontalAlign = HorizontalAlign.Right;
                        table_coluna_pw.Controls.Add(lbl_p);
                    }
                    else if (SessionEvento.TpFolhaCadastroWeb == "2")
                    {
                        //table_coluna2.BorderWidth = 1;
                        table_coluna2_pw.ColumnSpan = 2;
                        table_coluna2_pw.Controls.Add(lbl_p);

                        System.Web.UI.WebControls.Label lbl2 = new Label();
                        lbl2.Text = "<br />";
                        table_coluna2_pw.Controls.Add(lbl2);

                    }

                    //table_coluna_pw.Controls.Add(lbl_p);

                    TextBox txtbx2 = new TextBox();
                    txtbx2.Visible = true;
                    txtbx2.ID = compkrk.NomeCampo + "_validar";
                    //txtbx2.Width = compkrk.TamComprimento;
                    //txtbx2.CssClass = "email";

                    if (compkrk.NomeClasseCSS == KrksaComp.NomesClasseCSS.geral)
                    {
                        txtbx2.Width = compkrk.TamComprimento;
                    }

                    txtbx2.CssClass = compkrk.NomeClasseCSS.ToString();

                    if (compkrk.MaiusculaMinuscula == System.Windows.Forms.CharacterCasing.Upper)
                        txtbx2.CssClass += " maiusculo";
                    else if (compkrk.MaiusculaMinuscula == System.Windows.Forms.CharacterCasing.Lower)
                        txtbx2.CssClass += " minusculo";

                    table_coluna2_pw.Controls.Add(txtbx2);

                    if (compkrk.Obrigatorio)
                    {
                        RequiredFieldValidator rfv2 = new RequiredFieldValidator();
                        rfv2.ID = "rfv2_" + compkrk.NomeCampo;
                        rfv2.ControlToValidate = compkrk.NomeCampo + "_validar";
                        rfv2.Display = ValidatorDisplay.Dynamic;
                        rfv2.ForeColor = Color.Red;
                        rfv2.SetFocusOnError = true;

                        if (SessionEvento.CdEvento == "005701")
                            rfv2.ForeColor = Color.Yellow;
                        //rfv2.CssClass = "Validators";
                        rfv2.ErrorMessage = compkrk.ObrigatorioMensagem;

                        table_coluna2_pw.Controls.Add(rfv2);
                    }
                    CompareValidator compVal = new CompareValidator();
                    compVal.ID = "compVal_" + compkrk.NomeCampo;
                    compVal.ControlToCompare = compkrk.NomeCampo;
                    compVal.Display = ValidatorDisplay.Dynamic;
                    compVal.ForeColor = Color.Red;
                    compVal.SetFocusOnError = true;

                    if (SessionEvento.CdEvento == "005701")
                        compVal.ForeColor = Color.Yellow;
                    //compVal.CssClass = "Validators";
                    compVal.ControlToValidate = compkrk.NomeCampo + "_validar";
                    
                    //compVal.ErrorMessage = compkrk.NomeLabelCampo + " divergente de " + compkrk.NomeLabelCampo + " 2.";
                    //compVal.ErrorMessage = compkrk.NomeLabelCampo + " e " + compkrk.NomeLabelCampo + " 2.";

                    if (SessionIdioma == "PTBR")
                        compVal.ErrorMessage = compkrk.NomeLabelCampo + " e " + compkrk.NomeLabelCampo + " 2 diferentes ";
                    else if (SessionIdioma == "ENUS")
                        compVal.ErrorMessage = " Different " + compkrk.NomeLabelCampo + " and " + compkrk.NomeLabelCampo;
                    else if (SessionIdioma == "ESP")
                        compVal.ErrorMessage = " Diferente " + compkrk.NomeLabelCampo + " y " + compkrk.NomeLabelCampo;
                    else if (SessionIdioma == "FRA")
                        compVal.ErrorMessage = compkrk.NomeLabelCampo + " e " + compkrk.NomeLabelCampo + " 2 différente";
                    
                    table_coluna2_pw.Controls.Add(compVal);

                    if (SessionEvento.TpFolhaCadastroWeb == "1") 
                        table_linha_pw.Controls.Add(table_coluna_pw);

                    table_linha_pw.Controls.Add(table_coluna2_pw);

                    table.Controls.Add(table_linha_pw);
                }
            }
            else
            {
                if (SessionEvento.TpFolhaCadastroWeb == "1") 
                    table_linha.Controls.Add(table_coluna);

                table_linha.Controls.Add(table_coluna2);
                table.Controls.Add(table_linha);
                
            }
            
            nome_campo_anterior = compkrk.NomeCampoBancoDados;
        }
        form1.Controls.Add(table);

        //SqlDataSource1.ConnectionString = SessionCnn.ConnectionString;
        //localizarPesquisa(SessionEvento.CdEvento);
        //InitializeComponent();
        
        base.OnInit(e);

        //base.OnPreRender(e);

        
    }

    private void txt_TempLixoZero_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        string cdUF = (sender as DropDownList).SelectedValue;

        Control controlePainel = FindControlRecursive(this.Page, "pnlMsgTempLixoZero");
        if (controlePainel != null)
        {
            (controlePainel as Panel).Visible = false;

            if (cdUF == "DF")
            {
                (controlePainel as Panel).Visible = true;
                Control controleLabel = FindControlRecursive(controlePainel, "lblMsgTempLixoZero");
                if (controleLabel != null)
                {
                    (controleLabel as Label).Text =
                    @"<p>O Congresso Internacional Cidades Lixo Zero &eacute; um evento gratuito cuja participa&ccedil;&atilde;o depender&aacute; de confirma&ccedil;&atilde;o pr&eacute;via.</p>
                       <p>&nbsp;</p>
                    <p>Aten&ccedil;&atilde;o, neste momento voc&ecirc; est&aacute; realizando a sua pr&eacute; inscri&ccedil;&atilde;o. O pedido de confirma&ccedil;&atilde;o que garantir&aacute; 
                    a sua entrada ser&aacute; enviado 10 dias antes do evento por e-mail. Certifique-se de que est&aacute; informando os contatos corretamente.</p>";
                }
            }
        }
        else
            return;
        
    
    }

    private DataTable localizarCamposCadastro(String prmCdSistema, String prmCdCategoria)
    {
        lblMsg.Visible = false;

        if (SessionCnn == null)
            SessionCnn = (SqlConnection)Session["SessionCnn"];
        else
            Session["SessionCnn"] = SessionCnn;

        if (SessionEvento == null)
            SessionEvento = (Evento)Session["SessionEvento"];
        else
            Session["SessionEvento"] = SessionEvento;

        if (SessionCnn.State != ConnectionState.Open)
        {
            try
            {
                SessionCnn.Open();
            }
            catch
            {
                // _erroNoCampo = true;
                lblMsg.Visible = true;
                lblMsg.Text = "Conexão inválida";
                return null;
            }
        }

        DataTable DTCampos = new DataTable();
        SqlDataAdapter Dap;

        SqlCommand cmd = new SqlCommand(
            "SELECT a.cdEvento, " +
            "       a.cdSistema, " +
            "       a.cdCampo, " +
            "       a.noCampo, " +
            "       a.dsDetalhesCampo, " +
            "       a.vlOrdem, " +
            "       a.flAtivo, " +
            "       a.cdTipoCampo" + 
            "  FROM dbo.tbConfigSistemas  a " +
            "  join dbo.tbConfigSistemaCategorias csc " +
            "    on a.cdEvento = csc.cdEvento " +
            "   and a.cdSistema = csc.cdSistema " +
            "   and a.cdCampo = csc.cdCampo " +
            "   and a.cdTipoCampo = csc.cdTipoCampo " +
            "   and csc.cdCategoria = '" + prmCdCategoria + "' " +
            " WHERE (a.cdEvento = '" + SessionEvento.CdEvento + "') " + 
            "   AND (a.cdSistema = '" + prmCdSistema + "') " +
            "   and a.noCampo <> 'Imagem' " +
            " ORDER BY a.vlOrdem", SessionCnn);

        Dap = new SqlDataAdapter(cmd);

        Dap.TableMappings.Add("Campos", "tbConfigSistemas");
        Dap.Fill(DTCampos);

        SessionCnn.Close();

        if ((DTCampos != null) && (DTCampos.Rows.Count > 0))
            return DTCampos;
        else
        {
            cmd = new SqlCommand(
            "SELECT a.cdEvento, " +
            "       a.cdSistema, " +
            "       a.cdCampo, " +
            "       a.noCampo, " +
            "       a.dsDetalhesCampo, " +
            "       a.vlOrdem, " +
            "       a.flAtivo, " +
            "       a.cdTipoCampo" +
            "  FROM tbConfigSistemas  a " +
            " WHERE (a.cdEvento = '" + SessionEvento.CdEvento + "') " +
            "   AND (a.cdSistema = '" + prmCdSistema + "') " +
            "   and a.noCampo <> 'Imagem' " +
            " ORDER BY a.vlOrdem", SessionCnn);

            Dap = new SqlDataAdapter(cmd);

            Dap.TableMappings.Add("Campos", "tbConfigSistemas");
            Dap.Fill(DTCampos);

            return DTCampos;
        }



        //SqlDataSource1.SelectCommand = cmd.CommandText;
        ////SqlDataSource1.DataBind();
        //DataList1.DataSourceID = "SqlDataSource1";
        //DataList1.DataBind();



        // SqlParameter p2 = new SqlParameter("@cdGrupoPergunta", prm_cdGrupoPergunta);
        // SqlParameter p3 = new SqlParameter("@cdQuestao", prm_cdQuestao);
        //SqlParameter p4 = new SqlParameter("@cdEvento", "000401001");
        // cmd.Parameters.Add(p1);
        // cmd.Parameters.Add(p2);
        // cmd.Parameters.Add(p3);
        //cmd.Parameters.Add(p4);
        //  SqlDataAdapter da = new SqlDataAdapter();
        //  da.SelectCommand = cmd;
        //  DataSet ds = new DataSet();
        //  da.Fill(ds, "tbQuestoesItens");
        //  return ds;
    }

    protected void ServerValidar(object source, ServerValidateEventArgs args)
    {
        //wrkLiberadoGravar = true;
        foreach (Control ctrlTable in form1.Controls)
        {
            if (ctrlTable is Table)
            {
                foreach (Control ctrlRow in ctrlTable.Controls)
                {
                    if (ctrlRow is TableRow)
                    {
                        foreach (Control ctrlCell in ctrlRow.Controls)
                        {
                            if (ctrlCell is TableCell)
                            {
                                foreach (Control ctrl in ctrlCell.Controls)
                                {
                                    if (ctrl is TextBox)
                                    {
                                        if (((ctrl as TextBox).ID == "txt_nuCPFCNPJ") && ((source as CustomValidator).ID == "cpfValidar"))
                                        {
                                            string tmp = "";
                                            string tmpCdCategoria = "";
                                            Control controleCateg = FindControlRecursive(this.Page, "txt_cdCategoria");
                                            if (controleCateg != null)
                                            {
                                                tmpCdCategoria = (controleCateg as DropDownList).SelectedValue.ToString();
                                            }
                                            if (SessionParticipante != null)
                                                tmp = SessionParticipante.CdParticipante;

                                            string tmpCPF = ValidarCPF(tmp, SessionEvento.CdEvento, (ctrl as TextBox).Text, tmpCdCategoria, SessionCnn);
                                            if (tmpCPF != "")
                                            {
                                                (source as CustomValidator).ErrorMessage = tmpCPF;
                                                args.IsValid = false;
                                                wrkLiberadoGravar = false;
                                                return;
                                            }
                                        }
                                        else if (((ctrl as TextBox).ID == "txt_dsEmail") && ((source as CustomValidator).ID == "emailValidar"))
                                        {
                                            string tmpEMail = ValidarEmail(SessionParticipante.CdParticipante, SessionEvento.CdEvento, (ctrl as TextBox).Text, SessionCnn);
                                            if (tmpEMail != "")
                                            {
                                                (source as CustomValidator).ErrorMessage = tmpEMail;
                                                args.IsValid = false;
                                                wrkLiberadoGravar = false;
                                                return;
                                            }
                                        }
                                        else if (((source as CustomValidator).ID == (ctrl as TextBox).ID + "_Validar") && ((ctrl as TextBox).ToolTip == "Obrigatório"))
                                        {
                                            if ((ctrl as TextBox).Text.Replace(".", "").Replace("-", "").Replace("(", "").Replace(")", "").Replace("/", "").Trim() == "")
                                            {
                                                (source as CustomValidator).ErrorMessage = "Campo Requerido";
                                                args.IsValid = false;
                                                wrkLiberadoGravar = false;
                                                return;
                                            }
                                        }



                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        
        
    }
    protected string ValidarCPF(string prmCdParticipante, string prmCdEvento, string prmCPF, string prmCdCategoria, SqlConnection prmCnn)
    {

        
        if ((SessionIdioma != "PTBR") || (SessionPais != "BRASIL") || (SessionPais == ""))
            return "";

        if ((!oClsFuncoes.CPFCNPJValidar(prmCPF)))
        {
            CategoriaCad oCategoriaCad = new CategoriaCad();
            Categoria oCategoria = oCategoriaCad.Pesquisar(prmCdEvento, prmCdCategoria, prmCnn);
            if (((SessionEvento != null) && (SessionEvento.FlLiberarCPFCoringa)) || (prmCdCategoria == "") || (!oCategoria.FlCPFCNPJObrigatorio))
                return "";

            return "CPF Inválido!";
        }
        else
        {
            string tmpCPF = prmCPF.Replace(".", "").Replace("-", "");
            
            /*if ((tmpCPF == "11111111111") ||
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
                CategoriaCad oCategoriaCad = new CategoriaCad();
                Categoria oCategoria = oCategoriaCad.Pesquisar(prmCdEvento, prmCdCategoria, prmCnn);
                if (((SessionEvento != null) && (SessionEvento.FlLiberarCPFCoringa)) || (!oCategoria.FlCPFCNPJObrigatorio) || (oCategoria.FlLiberarCPFCNPJCoringa))
                    return "";
                return "CPF Inválido!";
            }
            else
            {*/
                return oParticipanteCad.verificarDuplicidadeCPF(prmCdParticipante, prmCdEvento, tmpCPF, prmCdCategoria, prmCnn);
            //}
                
        }
            
    }

    protected string ValidarEmail(string prmCdParticipante, string prmCdEvento, string prmDsEmail, SqlConnection prmCnn)
    {
        if (prmDsEmail == "")
            return "";

        if (SessionEvento.FlPermitirDuplicidadeEmail)
            return "";

        Participante part = oParticipanteCad.Pesquisar(prmCdEvento, "dsEmail", prmDsEmail, prmCnn);

        if ((part != null) && (part.CdParticipante != prmCdParticipante))
        {
            if (SessionEvento.CdEvento == "001201")
            {
                oParticipanteCad.AtualizarDataCadastro(prmCdEvento, part.CdParticipante, prmCnn);
                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                           "002",
                                           ""), true);
            }
            if (SessionIdioma == "ENUS")
                return "E-mail already registered";
            else if (SessionIdioma == "ESP")
                return "E-mail ya está registrado";
            else if (SessionIdioma == "FRA")
                return "E-mail déjà enregistré";
            else
                return "E-mail já cadastrado";
        }
        else
            return "";
        

    }

    private void PreecherNomeEtiqueta(String prmNome)
    {
        //foreach (Control ctrl in DataList1.Controls)
        //{
        //    if (ctrl is TextBox)
        //    {
        //        if ((ctrl as TextBox).ID == "txt_noEtiqueta")
        //        {
        //            if ((ctrl as TextBox).Text.Trim() == "")
        //            {
        //                string prm = NomeReduzido(prmNome.Trim());
        //                if (prm.Length > (ctrl as TextBox).MaxLength)
        //                    (ctrl as TextBox).Text = prm.Substring(0, 19);
        //                else
        //                    (ctrl as TextBox).Text = prm;

        //            }
        //            break;
        //        }
        //    }
        //}

    }

    private String NomeReduzido(String prmNome)
    {
        if (prmNome.Trim() == "")
            return "";

        String final = "";

        if (prmNome.Trim().Contains(" "))
        {
            //String final = "";
            String tmpPrep = "";
            String nome = prmNome.Trim();
            String tmp = nome.Substring(0, nome.Trim().IndexOf(' '));

            final = tmp.Trim();

            nome = nome.Remove(0, tmp.Length);
            int i = 0;
            while ((nome.Trim().Contains(" ")) && (i < 200))
            {
                tmp = nome.Substring(0, nome.IndexOf(' ')).Trim();

                if ((tmp.Trim().ToUpper() == "DA") ||
                    (tmp.Trim().ToUpper() == "DAS") ||
                    (tmp.Trim().ToUpper() == "DE") ||
                    (tmp.Trim().ToUpper() == "DO") ||
                    (tmp.Trim().ToUpper() == "DOS") ||
                    (tmp.Trim().ToUpper() == "E") ||
                    (tmp.Trim().ToUpper() == "I") ||
                    (tmp.Trim().ToUpper() == "EL") ||
                    (tmp.Trim().ToUpper() == "Y") ||
                    (tmp.Trim().ToUpper() == "DU") ||
                    (tmp.Trim().ToUpper() == "DI"))
                    tmpPrep = tmp + " ";
                else
                    tmpPrep = "";

                nome = nome.Remove(0, tmp.Length + 1);
                i++;
            }

            final += ' ' + tmpPrep + nome.Trim();
            //MessageBox.Show(final);
        }
        else
        {
            final = prmNome;
        }
        return final;
    }

    public void AutoPreenchimento(String prmCdEvento, DropDownList prmCampoListagem, bool prmAutoCarregar)
    {
        if ((prmCampoListagem.ID == "txt_dsUF") &&
            (prmAutoCarregar) &&
            (prmCampoListagem.DataSource == null))
            ListarUF(prmCampoListagem);

        if ((prmCampoListagem.ID == "txt_noCidade") &&
           (prmAutoCarregar) &&
           (prmCampoListagem.DataSource == null))
            ListarCidades(prmCampoListagem);

        if ((prmCampoListagem.ID == "txt_noPais") &&
            (prmAutoCarregar) &&
            (prmCampoListagem.DataSource == null))
            ListarPaises(prmCampoListagem);

        if ((prmCampoListagem.ID == "txt_cdCategoria") &&
            (prmAutoCarregar)
            //&&(this.DataSource == null)
            )
            ListarCategorias(prmCdEvento, prmCampoListagem);

        if ((prmCampoListagem.ID != "txt_dsUF") &&
            (prmCampoListagem.ID != "txt_noCidade") &&
            (prmCampoListagem.ID != "txt_noPais") &&
            (prmCampoListagem.ID != "txt_cdCategoria") &&
            (prmAutoCarregar))
        {
            if (prmCdEvento == "008303")
                ListarOutrosSICOOB(prmCdEvento, prmCampoListagem, "");
            else if ((prmCdEvento == "010901") || (prmCdEvento == "010801"))
                ListarPaises2(prmCampoListagem);
            else
                ListarOutros(prmCdEvento, prmCampoListagem, prmCampoListagem.ID.Replace("txt_", ""));

        }

    }

    public void ListarUF(DropDownList prmCampoListagem)
    {
        lblMsg.Visible = false;
        if (SessionCnn == null)
        {
            //_erroNoCampo = true;
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
                // _erroNoCampo = true;
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
                SqlCommand comando = new SqlCommand(
                    "SELECT " +
                        "distinct dsUF " +
                    "FROM " +
                        "dbo.tbMunicipios " +
                    "ORDER BY 1 ", SessionCnn);


                Dap = new SqlDataAdapter(comando);

                Dap.TableMappings.Add("uf", "tbMunicipios");
                Dap.Fill(DT);

                SessionCnn.Close();

                // DataSource bsufs = new BindingSource();

                DataTable oDataTable = new DataTable();



                oDataTable.Columns.Add("dsUF");

                oDataTable.Rows.Add("");

                for (int i = 0; i < DT.DefaultView.Count; i++)
                {
                    oDataTable.Rows.Add(DT.DefaultView[i]["dsUF"]);
                }


                //bsufs.DataSource = oDataTable.DefaultView;
                prmCampoListagem.DataSource = oDataTable.DefaultView;
                prmCampoListagem.DataTextField = "dsUF";
                prmCampoListagem.DataValueField = "dsUF";
                prmCampoListagem.DataBind();
                //this.SelectedIndex = 0;

                //this.DropDownStyle = ComboBoxStyle.DropDownList;

            }
            catch (Exception Ex)
            {
                //  _erroNoCampo = true;
                lblMsg.Visible = true;
                lblMsg.Text = "Erro ao selecionar UFs!\n" + Ex.Message;
                SessionCnn.Close();
            }
        }
        finally
        {
            SessionCnn.Close();
        }


    }

    public void ListarCidades(DropDownList prmCampoListagem)
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
                SqlCommand comando = new SqlCommand(
                    "SELECT " +
                      "cdMunicipioIBGE, " +
                      "upper(dbo.TIRA_ACENTO(dsMunicipio)) as dsMunicipio, " +
                      "dsUF " +
                    "FROM " +
                      "dbo.tbMunicipios  " +
                    "ORDER BY 2 ", SessionCnn);


                Dap = new SqlDataAdapter(comando);

                Dap.TableMappings.Add("MUNICIPIO", "tbMunicipios");
                Dap.Fill(DT);

                SessionCnn.Close();

                DataTable oDataTable = new DataTable();


                oDataTable.Columns.Add("cdMunicipioIBGE");
                oDataTable.Columns.Add("dsMunicipio");
                oDataTable.Columns.Add("dsUF");

                oDataTable.Rows.Add("", "");

                for (int i = 0; i < DT.DefaultView.Count; i++)
                {
                    oDataTable.Rows.Add(DT.DefaultView[i]["cdMunicipioIBGE"],
                                        DT.DefaultView[i]["dsMunicipio"],
                                        DT.DefaultView[i]["dsUF"]);
                }


                //bsMunicipios.DataSource = oDataTable.DefaultView;
                prmCampoListagem.DataSource = oDataTable.DefaultView;
                prmCampoListagem.DataTextField = "dsMunicipio";
                prmCampoListagem.DataValueField = SessionEvento.DsFormaGuardarMunicipio;
                prmCampoListagem.DataBind();


                //this.DropDownStyle = ComboBoxStyle.DropDownList;


            }
            catch (Exception Ex)
            {
                //_erroNoCampo = true;
                lblMsg.Visible = true;
                lblMsg.Text = "Erro ao selecionar Municipios!\n" + Ex.Message;
                SessionCnn.Close();
            }
        }
        finally
        {
            SessionCnn.Close();
        }


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

                oDataTable.Rows.Add("","","","");

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

    public void ListarCategorias(String prmCdEvento, DropDownList prmCampoListagem)
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
                
                string tmpComando =
                    "SELECT cdEvento " +
                          ",cdCategoria " +
                          ",noCategoria " +
                          ",flPagamento " +
                          ",vlPagamento " +
                          ",flCertificado " +
                          ",flAtividades " +
                          ",flMaterial " +
                          ",flRefeicao " +
                          ",flCortesias " +
                          ",flDescontos " +
                          ",flSorteio " +
                          ",flQuestionario " +
                          ",flAtivo " +
                          ",flVisivelWeb " +
                          ",flEnviarSMS " +
                          ",flCriticar " +
                          ",flControlado " +
                          ",flConfirmacaoCadWeb " +
                          ",flVerificarPreCadastro " +
                          ",tpVerificacaoPreCadastro " +
                          ",dsMensagemPreCadastro " +
                          ",flPatrocinada " +
                          ",flRequerConfirmacaoDoc " +
                          ",dsCorCracha " +
                          ",flPreCadastroExclusivo " +
                          ",dsMensagemConfirmacaoDoc " +
                          ",dsPreCadastroCampoPadraoCadastro " +
                          ",flCPFCNPJObrigatorio " +
                          ",flLiberarCPFCNPJCoringa " +
                          ",flEnviarEmail " +
                          ",noCategoriaIngles " +
                          ",noCategoriaEspanhol " +
                          ",noCategoriaFrances " +
                          ",dsMsgPreCadastroIng " +
                          ",dsMsgPreCadastroEsp " +
                          ",dsMsgPreCadastroFra " +
                      "FROM dbo.tbCategorias " +
                     "WHERE cdEvento = '" + prmCdEvento + "' " +
                       "AND flAtivo = 1 ";// +
                if (//(SessionEvento.CdEvento == "003003") && 
                    (SessionCateg == null) || (SessionCateg == "")
                    || ((!SessionEvento.FlCadCategoriaFixa) && (SessionParticipante.CdParticipante != "") && ((SessionParticipante.CdParticipante == "") || (SessionParticipante.CdPatrocinador == ""))))
                {
                    if (SessionIdioma == "PTBR")
                        tmpComando += "AND flVisivelWeb = 1 ";
                    else if (SessionIdioma == "ENUS")
                        tmpComando += "AND flVisivelWeb = 1 AND noCategoriaIngles <> '' ";
                    else if (SessionIdioma == "ESP")
                        tmpComando += "AND flVisivelWeb = 1 AND noCategoriaEspanhol <> '' ";
                    else if (SessionIdioma == "FRA")
                        tmpComando += "AND flVisivelWeb = 1 AND noCategoriaFrances <> '' ";
                }
                    

                if (//(SessionEvento.CdEvento == "003003") && 
                    ((SessionParticipante != null) && (SessionParticipante.CdParticipante != "")))
                    tmpComando += "OR (cdCategoria = '" + SessionParticipante.CdCategoria + "') ";

                if (SessionEvento.CdEvento != "008501")
                    tmpComando += "ORDER BY noCategoria ";
                else
                    tmpComando += "ORDER BY cdCategoria ";

                SqlCommand comando = new SqlCommand(tmpComando, SessionCnn);

                Dap = new SqlDataAdapter(comando);

                Dap.TableMappings.Add("Categorias", "tbCategorias");
                Dap.Fill(DT);

                SessionCnn.Close();

                // BindingSource bsCategoria = new BindingSource();

                DataTable oDataTable = new DataTable();



                oDataTable.Columns.Add("cdCategoria");
                oDataTable.Columns.Add("noCategoria");
                oDataTable.Columns.Add("cdEvento");
                oDataTable.Columns.Add("flPagamento");
                oDataTable.Columns.Add("vlPagamento");
                oDataTable.Columns.Add("flCertificado");
                oDataTable.Columns.Add("flAtividades");
                oDataTable.Columns.Add("flMaterial");
                oDataTable.Columns.Add("flRefeicao");
                oDataTable.Columns.Add("flCortesias");
                oDataTable.Columns.Add("flDescontos");
                oDataTable.Columns.Add("flSorteio");
                oDataTable.Columns.Add("flQuestionario");
                oDataTable.Columns.Add("flAtivo");
                oDataTable.Columns.Add("flVisivelWeb");
                oDataTable.Columns.Add("flEnviarSMS");
                oDataTable.Columns.Add("flCriticar");
                oDataTable.Columns.Add("flControlado");
                oDataTable.Columns.Add("flConfirmacaoCadWeb");
                oDataTable.Columns.Add("flVerificarPreCadastro");
                oDataTable.Columns.Add("tpVerificacaoPreCadastro");
                oDataTable.Columns.Add("dsMensagemPreCadastro");
                oDataTable.Columns.Add("flPatrocinada");
                oDataTable.Columns.Add("flRequerConfirmacaoDoc");
                oDataTable.Columns.Add("dsCorCracha");
                oDataTable.Columns.Add("flPreCadastroExclusivo");
                oDataTable.Columns.Add("dsMensagemConfirmacaoDoc");
                oDataTable.Columns.Add("dsPreCadastroCampoPadraoCadastro");
                oDataTable.Columns.Add("flCPFCNPJObrigatorio");
                oDataTable.Columns.Add("flLiberarCPFCNPJCoringa");
                oDataTable.Columns.Add("flEnviarEmail");
                oDataTable.Columns.Add("noCategoriaIngles");
                oDataTable.Columns.Add("noCategoriaEspanhol");
                oDataTable.Columns.Add("noCategoriaFrances");
                oDataTable.Columns.Add("dsMsgPreCadastroIng");
                oDataTable.Columns.Add("dsMsgPreCadastroEsp");
                oDataTable.Columns.Add("dsMsgPreCadastroFra");

                if ((DT == null) || (DT.DefaultView.Count > 1))
                    oDataTable.Rows.Add("", "", "", false, 0, false, false, false, false, false, false, false, false, true, false, false, false, false, false, false, "", "", false, false, "", false, "", "", false, false, false, "", "", "", "", "", "");

                for (int i = 0; i < DT.DefaultView.Count; i++)
                {
                    oDataTable.Rows.Add(DT.DefaultView[i]["cdCategoria"],
                                        DT.DefaultView[i]["noCategoria"],
                                        DT.DefaultView[i]["cdEvento"],
                                        bool.Parse(DT.DefaultView[i]["flPagamento"].ToString()),
                                        decimal.Parse(DT.DefaultView[i]["vlPagamento"].ToString()),
                                        bool.Parse(DT.DefaultView[i]["flCertificado"].ToString()),
                                        bool.Parse(DT.DefaultView[i]["flAtividades"].ToString()),
                                        bool.Parse(DT.DefaultView[i]["flMaterial"].ToString()),
                                        bool.Parse(DT.DefaultView[i]["flRefeicao"].ToString()),
                                        bool.Parse(DT.DefaultView[i]["flCortesias"].ToString()),
                                        bool.Parse(DT.DefaultView[i]["flDescontos"].ToString()),
                                        bool.Parse(DT.DefaultView[i]["flSorteio"].ToString()),
                                        bool.Parse(DT.DefaultView[i]["flQuestionario"].ToString()),
                                        bool.Parse(DT.DefaultView[i]["flAtivo"].ToString()),
                                        bool.Parse(DT.DefaultView[i]["flVisivelWeb"].ToString()),
                                        bool.Parse(DT.DefaultView[i]["flEnviarSMS"].ToString()),
                                        bool.Parse(DT.DefaultView[i]["flCriticar"].ToString()),
                                        bool.Parse(DT.DefaultView[i]["flControlado"].ToString()),
                                        bool.Parse(DT.DefaultView[i]["flConfirmacaoCadWeb"].ToString()),
                                        bool.Parse(DT.DefaultView[i]["flVerificarPreCadastro"].ToString()),
                                        DT.DefaultView[i]["tpVerificacaoPreCadastro"].ToString(),
                                        DT.DefaultView[i]["dsMensagemPreCadastro"].ToString(),
                                        bool.Parse(DT.DefaultView[i]["flPatrocinada"].ToString()),
                                        bool.Parse(DT.DefaultView[i]["flRequerConfirmacaoDoc"].ToString()),
                                        DT.DefaultView[i]["dsCorCracha"].ToString(),
                                        bool.Parse(DT.DefaultView[i]["flPreCadastroExclusivo"].ToString()),
                                        DT.DefaultView[i]["dsMensagemConfirmacaoDoc"].ToString(),
                                        DT.DefaultView[i]["dsPreCadastroCampoPadraoCadastro"].ToString(),
                                        bool.Parse(DT.DefaultView[i]["flCPFCNPJObrigatorio"].ToString()),
                                        bool.Parse(DT.DefaultView[i]["flLiberarCPFCNPJCoringa"].ToString()),
                                        bool.Parse(DT.DefaultView[i]["flEnviarEmail"].ToString()),
                                        DT.DefaultView[i]["noCategoriaIngles"].ToString(),
                                        DT.DefaultView[i]["noCategoriaEspanhol"].ToString(),
                                        DT.DefaultView[i]["noCategoriaFrances"].ToString(),
                                        DT.DefaultView[i]["dsMsgPreCadastroIng"].ToString(),
                                        DT.DefaultView[i]["dsMsgPreCadastroEsp"].ToString(),
                                        DT.DefaultView[i]["dsMsgPreCadastroFra"].ToString()
                                        );
                }       

                // bsCategoria.DataSource = oDataTable.DefaultView;
                prmCampoListagem.DataSource = oDataTable;//bsCategoria;
                if (SessionIdioma == "PTBR")
                    prmCampoListagem.DataTextField = "noCategoria";
                else if (SessionIdioma == "ENUS")
                    prmCampoListagem.DataTextField = "noCategoriaIngles";
                else if (SessionIdioma == "ESP")
                    prmCampoListagem.DataTextField = "noCategoriaEspanhol";
                else if (SessionIdioma == "FRA")
                    prmCampoListagem.DataTextField = "noCategoriaFrances";
                prmCampoListagem.DataValueField = "cdCategoria";
                prmCampoListagem.DataBind();
                //this.SelectedIndex = 0;

                //this.DropDownStyle = ComboBoxStyle.DropDownList;


                if (//(SessionEvento.CdEvento == "003003") && 
                    (SessionCateg != null) && (SessionCateg != "")
                    && ((SessionEvento.FlCadCategoriaFixa) || (SessionParticipante.CdParticipante == "") || ((SessionParticipante.CdParticipante != "") && (SessionParticipante.CdPatrocinador != "")))
                    )
                {
                    prmCampoListagem.SelectedValue = SessionCateg;
                    prmCampoListagem.Enabled = false;
                }

               

            }
            catch (Exception Ex)
            {
                SessionCnn.Close();
                // _erroNoCampo = true;
                lblMsg.Visible = true;
                lblMsg.Text = "Erro ao selecionar Categorias!\n" + Ex.Message;

            }
        }
        finally
        {
            SessionCnn.Close();
        }


    }

    public void ListarPaises2(DropDownList prmCampoListagem)
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

    public void ListarOutros(String prmCdEvento, DropDownList prmCampoListagem, string nomeCampoBancoDeDados)
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
                SqlCommand comando = new SqlCommand(
                    " SELECT " +
                        "distinct " + nomeCampoBancoDeDados +
                    " FROM " +
                        "dbo.tbParticipantes  " +
                    "WHERE cdEvento = '" + prmCdEvento + "' " +
                    " and " + nomeCampoBancoDeDados + " <> '' " +
                    "ORDER BY 1 ", SessionCnn);


                Dap = new SqlDataAdapter(comando);

                Dap.TableMappings.Add(nomeCampoBancoDeDados, "tbParticipantes");
                Dap.Fill(DT);

                SessionCnn.Close();

                //BindingSource bsufs = new BindingSource();

                DataTable oDataTable = new DataTable();



                oDataTable.Columns.Add(nomeCampoBancoDeDados);

                oDataTable.Rows.Add("");

                for (int i = 0; i < DT.DefaultView.Count; i++)
                {
                    oDataTable.Rows.Add(DT.DefaultView[i][nomeCampoBancoDeDados]);
                }


                //bsufs.DataSource = oDataTable.DefaultView;
                prmCampoListagem.DataSource = oDataTable.DefaultView;
                prmCampoListagem.DataTextField = nomeCampoBancoDeDados;
                prmCampoListagem.DataValueField = nomeCampoBancoDeDados;
                prmCampoListagem.DataBind();

                //this.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                //this.AutoCompleteSource = AutoCompleteSource.ListItems;

            }
            catch (Exception Ex)
            {
                SessionCnn.Close();
                //_erroNoCampo = true;
                lblMsg.Visible = true;
                lblMsg.Text = "Erro ao selecionar " + nomeCampoBancoDeDados + "!\\n" + Ex.Message;

            }
        }
        finally
        {
            SessionCnn.Close();
        }


    }

    public void CarregarListagem(DropDownList prmCampoListagem, string[] listagem, string nomeCampoBancoDeDados, string prmMaiusculaMinuscula)
    {


        lblMsg.Visible = false;

        try
        {
            DataTable DT = new DataTable();

            DataTable oDataTable = new DataTable();


            oDataTable.Columns.Add(nomeCampoBancoDeDados);

            //oDataTable.Rows.Add("");

            for (int i = 0; i < listagem.Length; i++)
            {
                if (prmMaiusculaMinuscula == System.Windows.Forms.CharacterCasing.Upper.ToString())
                    oDataTable.Rows.Add(listagem[i].ToUpper());
                else if (prmMaiusculaMinuscula == System.Windows.Forms.CharacterCasing.Lower.ToString())
                    oDataTable.Rows.Add(listagem[i].ToLower());
                else if (prmMaiusculaMinuscula == System.Windows.Forms.CharacterCasing.Normal.ToString())
                    oDataTable.Rows.Add(listagem[i]);
            }


            //bsufs.DataSource = oDataTable.DefaultView;
            prmCampoListagem.DataSource = oDataTable.DefaultView;
            prmCampoListagem.DataTextField = nomeCampoBancoDeDados;
            prmCampoListagem.DataValueField = nomeCampoBancoDeDados;
            prmCampoListagem.DataBind();
            //this.SelectedIndex = 0;

            //this.DropDownStyle = ComboBoxStyle.DropDownList;

        }
        catch (Exception Ex)
        {
            //  _erroNoCampo = true;
            lblMsg.Visible = true;
            lblMsg.Text = "Erro ao carregar campos!\n" + Ex.Message;

        }
        


    }

    public void CarregarListagem(CheckBoxList prmCampoListagem, string[] listagem, string nomeCampoBancoDeDados, string prmMaiusculaMinuscula)
    {

        lblMsg.Visible = false;


        try
        {
            DataTable DT = new DataTable();

            DataTable oDataTable = new DataTable();


            oDataTable.Columns.Add(nomeCampoBancoDeDados);

            //oDataTable.Rows.Add("");

            for (int i = 0; i < listagem.Length; i++)
            {
                if (prmMaiusculaMinuscula == System.Windows.Forms.CharacterCasing.Upper.ToString())
                    oDataTable.Rows.Add(listagem[i].ToUpper());
                else if (prmMaiusculaMinuscula == System.Windows.Forms.CharacterCasing.Lower.ToString())
                    oDataTable.Rows.Add(listagem[i].ToLower());
                else if (prmMaiusculaMinuscula == System.Windows.Forms.CharacterCasing.Normal.ToString())
                    oDataTable.Rows.Add(listagem[i]);
            }


            //bsufs.DataSource = oDataTable.DefaultView;
            prmCampoListagem.DataSource = oDataTable.DefaultView;
            prmCampoListagem.DataTextField = nomeCampoBancoDeDados;
            prmCampoListagem.DataValueField = nomeCampoBancoDeDados;
            prmCampoListagem.DataBind();
            //this.SelectedIndex = 0;

            //this.DropDownStyle = ComboBoxStyle.DropDownList;

        }
        catch (Exception Ex)
        {
            //  _erroNoCampo = true;
            lblMsg.Visible = true;
            lblMsg.Text = "Erro ao carregar campos!\n" + Ex.Message;

        }



    }
    
    public void CarregarListagem(RadioButtonList prmCampoListagem, string[] listagem, string nomeCampoBancoDeDados, string prmMaiusculaMinuscula)
    {

        lblMsg.Visible = false;


        try
        {
            DataTable DT = new DataTable();

            DataTable oDataTable = new DataTable();


            oDataTable.Columns.Add(nomeCampoBancoDeDados);

            //oDataTable.Rows.Add("");

            for (int i = 0; i < listagem.Length; i++)
            {
                if (prmMaiusculaMinuscula == System.Windows.Forms.CharacterCasing.Upper.ToString())
                    oDataTable.Rows.Add(listagem[i].ToUpper());
                else if (prmMaiusculaMinuscula == System.Windows.Forms.CharacterCasing.Lower.ToString())
                    oDataTable.Rows.Add(listagem[i].ToLower());
                else if (prmMaiusculaMinuscula == System.Windows.Forms.CharacterCasing.Normal.ToString())
                    oDataTable.Rows.Add(listagem[i]);
            }


            //bsufs.DataSource = oDataTable.DefaultView;
            prmCampoListagem.DataSource = oDataTable.DefaultView;
            prmCampoListagem.DataTextField = nomeCampoBancoDeDados;
            prmCampoListagem.DataValueField = nomeCampoBancoDeDados;
            prmCampoListagem.DataBind();
            //this.SelectedIndex = 0;

            //this.DropDownStyle = ComboBoxStyle.DropDownList;

        }
        catch (Exception Ex)
        {
            //  _erroNoCampo = true;
            lblMsg.Visible = true;
            lblMsg.Text = "Erro ao carregar campos!\n" + Ex.Message;

        }



    }

    public String PegarDetalheCampo(String prmDsCampo)
    {

        //CurrencyManager currencymanager = (CurrencyManager)this.BindingContext[this.DataSource];

        //DataRowView datarowview = (DataRowView)currencymanager.Current;

        //DataRow datarow = datarowview.Row;


        //String tmp = datarow[prmDsCampo].ToString();

        //return tmp;

        return "";

    }

    private void pesquisar(String prmCdParticipante)
    {
        lblMsg.Visible = false;

        SessionParticipante = oParticipanteCad.Pesquisar(SessionEvento.CdEvento, prmCdParticipante, SessionCnn);
        Session["SessionParticipante"] = SessionParticipante;
        /*
        LimparCampos();
        btnEditar.Enabled = false;
        btnCredencial.Enabled = false;
        BtnInscrCursos.Enabled = false;
        btnIniciarCaptura.Enabled = false;

        lblAlimentacao.Visible = false;
        lblAtividade.Visible = false;
        lblCertificacao.Visible = false;
        lblMaterial.Visible = false;
        */
        if (SessionParticipante != null)
        {
            if (SessionParticipante.CdParticipante != "")
            {
                Control btn = FindControlRecursive(this.Page, "btnDadosAnteriores");
                if (btn != null)
                {
                    (btn as Button).Visible = false;
                }
            }
            foreach (Control ctrlTable in form1.Controls)
            {
                if (ctrlTable is Table)
                {
                tmp:
                    foreach (Control ctrlRow in ctrlTable.Controls)
                    {
                        if (ctrlRow is TableRow)
                        {
                            foreach (Control ctrlCell in ctrlRow.Controls)
                            {
                                if (ctrlCell is TableCell)
                                {
                                    foreach (Control ctrl in ctrlCell.Controls)
                                    {
                                        if (ctrl is Label)
                                        {
                                            string temp = BuscarValores(SessionParticipante, (ctrl as Label).ID, "");
                                            if (temp != "")
                                                (ctrl as Label).Text = temp;
                                        }
                                        else if (ctrl is TextBox)
                                        {
                                            (ctrl as TextBox).Text = BuscarValores(SessionParticipante, (ctrl as TextBox).ID, (ctrl as TextBox).SkinID);
                                            if (((ctrl as TextBox).ID == "txt_dsEmail_validar") ||
                                                ((ctrl as TextBox).ID.Contains("txt_dsSenhaWeb")))
                                            {
                                                (ctrlTable as Table).Controls.Remove(ctrlRow);
                                                goto tmp;
                                            }
                                        }
                                        else if (ctrl is DropDownList)
                                        {
                                            if (((SessionEvento.CdCliente == "0013") && (int.Parse(SessionEvento.CdEvento) >= 1303)) || (SessionEvento.CdCliente == "0048") || (SessionEvento.CdCliente == "0003"))
                                            {
                                                if (((ctrl as DropDownList).ID != "txt_dsUF") && ((ctrl as DropDownList).ID != "txt_noCidade") && ((ctrl as DropDownList).ID != "txt_dsAuxiliar8") && ((ctrl as DropDownList).ID != "txt_dsAuxiliar9"))
                                                    (ctrl as DropDownList).SelectedValue = BuscarValores(SessionParticipante, (ctrl as DropDownList).ID, "");
                                            }
                                            else if (((ctrl as DropDownList).ID != "txt_dsUF") && ((ctrl as DropDownList).ID != "txt_noCidade"))
                                            {
                                                if ((SessionEvento.CdEvento == "008701") && ((ctrl as DropDownList).ID == "txt_dsAuxiliar3"))
                                                {

                                                    ListarPatentesBID(SessionEvento.CdEvento, (ctrl as DropDownList), "");
                                                }

                                                if ((SessionEvento.CdEvento == "009401") && ((ctrl as DropDownList).ID == "txt_noInstituicao"))
                                                {

                                                    ListarCamposExpoepi(SessionEvento.CdEvento, (ctrl as DropDownList), "");
                                                }

                                                (ctrl as DropDownList).SelectedValue = BuscarValores(SessionParticipante, (ctrl as DropDownList).ID, "");
                                            }

                                        }
                                        else if (ctrl is CheckBoxList)
                                        {

                                            string tmpLstChed = BuscarValores(SessionParticipante, (ctrl as CheckBoxList).ID, "");
                                            if (tmpLstChed.Trim() != "")
                                            {
                                                String[] tmpValoresLista = tmpLstChed.Split('#');
                                                for (int i = 0; i < tmpValoresLista.Length; i++)
                                                {
                                                    for (int j = 0; j < (ctrl as CheckBoxList).Items.Count; j++)
                                                    {

                                                        if ((ctrl as CheckBoxList).Items[j].Text.ToUpper() == tmpValoresLista[i].ToString().ToUpper())
                                                        {
                                                            (ctrl as CheckBoxList).Items[j].Selected = true;
                                                        }
                                                    }
                                                }
                                            }

                                        }
                                        else if (ctrl is RadioButtonList)
                                        {

                                            string tmpLstChed = BuscarValores(SessionParticipante, (ctrl as RadioButtonList).ID, "");
                                            if (tmpLstChed.Trim() != "")
                                            {
                                                //String[] tmpValoresLista = tmpLstChed.Split('#');
                                                //for (int i = 0; i < tmpValoresLista.Length; i++)
                                                //{
                                                for (int j = 0; j < (ctrl as RadioButtonList).Items.Count; j++)
                                                    {

                                                        if ((ctrl as RadioButtonList).Items[j].Text.ToUpper() == tmpLstChed.ToUpper())
                                                        {
                                                            (ctrl as RadioButtonList).Items[j].Selected = true;
                                                        }
                                                    }
                                                //}
                                            }

                                        }
                                        else if (ctrl is CascadingDropDown)
                                        {
                                            if ((ctrl as CascadingDropDown).ID == "cascaDD_dsUF")
                                            {
                                                (ctrl as CascadingDropDown).SelectedValue = BuscarValores(SessionParticipante, "txt_dsUF", "");
                                            }
                                            else if ((ctrl as CascadingDropDown).ID == "cascaDD_noCidade")
                                            {
                                                (ctrl as CascadingDropDown).SelectedValue = BuscarValores(SessionParticipante, "txt_noCidade", "");
                                            }
                                            if (((SessionEvento.CdCliente == "0013") && (int.Parse(SessionEvento.CdEvento) >= 1303)) || (SessionEvento.CdCliente == "0048") || (SessionEvento.CdCliente == "0003"))
                                            {
                                                if ((ctrl as CascadingDropDown).ID == "cascaDD_dsAuxiliar8")
                                                {
                                                    (ctrl as CascadingDropDown).SelectedValue = BuscarValores(SessionParticipante, "txt_dsAuxiliar8", "");
                                                }
                                                else if ((ctrl as CascadingDropDown).ID == "cascaDD_dsAuxiliar9")
                                                {
                                                    (ctrl as CascadingDropDown).SelectedValue = BuscarValores(SessionParticipante, "txt_dsAuxiliar9", "");
                                                }
                                            }
                                        }
                                        else if (ctrl is CheckBox)
                                        {
                                            (ctrl as CheckBox).Checked = Boolean.Parse(BuscarValores(SessionParticipante, (ctrl as CheckBox).ID, ""));


                                        }
                                        else if (ctrl is UpdatePanel)
                                        {
                                            foreach (Control ctrlContent in ctrl.Controls)
                                            {
                                                if (ctrlContent is Control)
                                                {
                                                    foreach (Control ctrPanel in ctrlContent.Controls)
                                                    {
                                                        if (ctrPanel is DropDownList)
                                                        {
                                                            (ctrPanel as DropDownList).SelectedValue = BuscarValores(SessionParticipante, (ctrPanel as DropDownList).ID, "");

                                                            if (SessionEvento.FlEventoComRecebimentos)
                                                            {
                                                                PedidoCad oPedidoCad = new PedidoCad();
                                                                Pedido tempPedido = oPedidoCad.VerificaPedidoNaoPagoWeb(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);

                                                                if (tempPedido != null)
                                                                {
                                                                    //(ctrPanel as DropDownList).Enabled = false;
                                                                    //lblMsg.Visible = true;
                                                                    //lblMsg.Text = "Constam Pedidos de Atividade/Curso/Produto. Não é possivel alterar a categoria!";
                                                                }

                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            
            if (SessionTipoSistema == "CRD")//((SessionEvento != null) && (SessionEvento.CdEvento == "002105"))
            {
                btnGravarParticipante.Visible = false;

                //mnuCred.Visible = true;

                if ((SessionOperacao != "") && (SessionOperacao != "INICIO"))
                    prp_btnhabilita(SessionOperacao);

                    //prp_btnhabilita(SessionOperacao);
                else
                    if ((SessionParticipante != null) && (SessionParticipante.CdParticipante != ""))
                    {
                        prp_btnhabilita(SessionParticipante.FlAtivo ? "ATIVO" : "INATIVO");

                        //btnNovo.PostBackUrl = SessionEvento.DsLinkRedirecionamento;
                        //btnNovo.Visible = true;

                        //btnEtiqueta.Visible = true;
                    }
                    else
                        prp_btnhabilita("INICIO");

            }


            /*                                                
                                                            
                                                            
                                                            ll)
                    {
                        (linha4 as TableRow).Visible = false;//Qual Area Atuacao
                    }
                }
            }

            if ((sender as DropDownList).ID == "txt_dsAuxiliar4") //É uma pessoa com deficiência
            {
                Control dsCampo5 = FindControlRecursive(this.Page, "txt_dsAuxiliar4");//É uma pessoa com deficiência
                Control linha5 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar4");
                Control dsCampo6 = FindControlRecursive(this.Page, "txt_dsAuxiliar5");//Qual deficiência
                Control linha6 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar5");

                if (((sender as DropDownList).Text.ToUpper() == "SIM") || ((sender as DropDownList).Text.ToUpper() == "YES"))
                {
                    if (dsCampo6 != null)
                    {
                        (linha6 as TableRow).Visible = true;//Qual deficiência
                    }
                }
                else
                {
                    if (dsCampo6 != null)
                    {
                        (linha6 as TableRow).Visible = false;//Qual deficiência
                    }
                }
            }

            if ((sender as DropDownList).ID == "txt_dsAuxiliar9") //representa alguma entidade
            {
                Control dsCampo5 = FindControlRecursive(this.Page, "txt_dsAuxiliar10");//Entidade que representa
                Control linha5 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar10");

                if (((sender as DropDownList).Text.ToUpper() == "SIM") || ((sender as DropDownList).Text.ToUpper() == "YES"))
                {
                    if (dsCampo5 != null)
                    {
                        (linha5 as TableRow).Visible = true;//Entidade que representa
                    }
                }
                else
                {
                    if (dsCampo5 != null)
                    {
                        (linha5 as TableRow).Visible = false;//Entidade que representa
                    }
                }
            }
        }

        #endregion

        #region MDA - CONDRAF
        if (SessionEvento.CdEvento == "004501")
        {

            if ((sender as DropDownList).ID == "txt_dsAuxiliar3") //Representatividade
            {
                Control dsCampo1 = FindControlRecursive(this.Page, "txt_dsAuxiliar4");//Sociedade Civil
                Control linha1 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar4");
                Control dsCampo2 = FindControlRecursive(this.Page, "txt_dsAuxiliar5");//Sociedade Civil - Outros
                Control linha2 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar5");

                Control dsCampo3 = FindControlRecursive(this.Page, "txt_dsAuxiliar6");//GOVERNO
                Control linha3 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar6");
                Control dsCampo4 = FindControlRecursive(this.Page, "txt_dsAuxiliar7");//GOVERNO - ORGAO
                Control linha4 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar7");

                if ((sender as DropDownList).Text.ToUpper() == "SOCIEDADE CIVIL")
                {
                    if (dsCampo1 != null)
                    {
                        (linha1 as TableRow).Visible = true;//Sociedade Civil
                        if ((dsCampo1 as DropDownList).Text.ToUpper() == "OUTRO")
                            (linha2 as TableRow).Visible = true;//Sociedade Civil - Outros
                        else
                            (linha2 as TableRow).Visible = false;//Sociedade Civil - Outros

                        (dsCampo3 as DropDownList).Text = "";
                        (linha3 as TableRow).Visible = false;//GOVERNO

                        (dsCampo4 as TextBox).Text = "";
                        (linha4 as TableRow).Visible = false;//GOVERNO - ORGAO
                    }
                }
                else if ((sender as te, SessionCnn);
                                                                
                                                                if (tempPedido != null)
                                                                {
                                                                    (ctrPanel as DropDownList).Enabled = false;
                                                                    lblMsg.Text = "Constam Pedidos de Atividade/Curso/Produto. Não é possivel alterar a categoria!";
                                                                }

                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            */

            //btnEditar.Enabled = true;
            //btnCredencial.Enabled = true;
            //BtnInscrCursos.Enabled = true;
            //btnIniciarCaptura.Enabled = true;

            //Inscricoes oInscricoes = new Inscricoes();
            //DataTable oDtInscr = oInscricoes.ListarAtividadesDoParticipante(oParticipante, Cnn);

            //if ((oDtInscr != null) && (oDtInscr.Rows.Count > 0))
            //    lblConfimMatricula.Visible = true;
        }
        else
        {
            lblMsg.Visible = true;
            lblMsg.Text = oParticipanteCad.RcMsg;
        }

    }
    
    public string BuscarValores(Participante prmParticipante, string campo, string mascara)
    {
        string retorno = "";
        switch (campo)
        {
            case "txt_cdParticipante":
                retorno = prmParticipante.CdParticipante;
                break;
            case "txt_cdCategoria":
                retorno = prmParticipante.CdCategoria;
                break;
            case "txt_noParticipante":
                retorno = prmParticipante.NoParticipante;
                break;
            case "txt_noEtiqueta":
                if (SessionTipoSistema == "NRM")
                    retorno = prmParticipante.NoEtiqueta;
                else if (SessionTipoSistema == "CRD")
                {
                    if (prmParticipante.NoEtiqueta == "")
                        retorno = NomeReduzido(prmParticipante.NoParticipante);
                    else
                        retorno = prmParticipante.NoEtiqueta;
                }
                break;
            case "txt_nuCPFCNPJ":
                retorno = (mascara == "") ? prmParticipante.NuCPFCNPJ : oClsFuncoes.MascaraGerar(prmParticipante.NuCPFCNPJ, mascara);
                break;
            case "txt_dsDocIdentificador":
                retorno = prmParticipante.DsDocIdentificador;
                break;
            case "txt_dsEmail":
                retorno = prmParticipante.DsEmail;
                break;
            case "txt_nuCEP":
                retorno = (mascara == "") ? prmParticipante.NuCEP : oClsFuncoes.MascaraGerar(prmParticipante.NuCEP, mascara);
                break;
            case "txt_dsEndereco":
                retorno = prmParticipante.DsEndereco;
                break;
            case "txt_noBairro":
                retorno = prmParticipante.NoBairro;
                break;
            case "txt_noCidade":
                retorno = prmParticipante.NoCidade;
                break;
            case "txt_dsUF":
                retorno = prmParticipante.DsUF;
                break;
            case "txt_noPais":
                retorno = SessionParticipante.NoPais;
                break;
            case "txt_dsFone1":
                retorno = (mascara == "") ? prmParticipante.DsFone1 : oClsFuncoes.MascaraGerar(prmParticipante.DsFone1, mascara);
                break;
            case "txt_dsFone2":
                retorno = (mascara == "") ? prmParticipante.DsFone2 : oClsFuncoes.MascaraGerar(prmParticipante.DsFone2, mascara);
                break;
            case "txt_dsFone3":
                retorno = (mascara == "") ? prmParticipante.DsFone3 : oClsFuncoes.MascaraGerar(prmParticipante.DsFone3, mascara);
                break;
            case "txt_noInstituicao":
                retorno = prmParticipante.NoInstituicao;
                break;
            case "txt_noCargo":
                retorno = prmParticipante.NoCargo;
                break;
            case "txt_noAreaAtuacao":
                retorno = prmParticipante.NoAreaAtuacao;
                break;
            case "txt_dsSexo":
                retorno = prmParticipante.DsSexo;
                break;
            case "txt_dsNascimento":
                retorno = (mascara == "") ? prmParticipante.DsNascimento : oClsFuncoes.MascaraGerar(prmParticipante.DsNascimento, mascara);
                break;
            case "txt_dsObservacoes":
                retorno = prmParticipante.DsObservacoes;
                break;
            case "txt_dsAuxiliar1":
                retorno = (mascara == "") ? prmParticipante.DsAuxiliar1 : oClsFuncoes.MascaraGerar(prmParticipante.DsAuxiliar1, mascara);
                break;
            case "txt_dsAuxiliar2":
                retorno = (mascara == "") ? prmParticipante.DsAuxiliar2 : oClsFuncoes.MascaraGerar(prmParticipante.DsAuxiliar2, mascara);
                break;
            case "txt_dsAuxiliar3":
                retorno = (mascara == "") ? prmParticipante.DsAuxiliar3 : oClsFuncoes.MascaraGerar(prmParticipante.DsAuxiliar3, mascara);
                break;
            case "txt_dsAuxiliar4":
                retorno = (mascara == "") ? prmParticipante.DsAuxiliar4 : oClsFuncoes.MascaraGerar(prmParticipante.DsAuxiliar4, mascara);
                break;
            case "txt_dsAuxiliar5":
                retorno = (mascara == "") ? prmParticipante.DsAuxiliar5 : oClsFuncoes.MascaraGerar(prmParticipante.DsAuxiliar5, mascara);
                break;
            case "txt_dsAuxiliar6":
                retorno = (mascara == "") ? prmParticipante.DsAuxiliar6 : oClsFuncoes.MascaraGerar(prmParticipante.DsAuxiliar6, mascara);
                break;
            case "txt_dsAuxiliar7":
                retorno = (mascara == "") ? prmParticipante.DsAuxiliar7 : oClsFuncoes.MascaraGerar(prmParticipante.DsAuxiliar7, mascara);
                break;
            case "txt_dsAuxiliar8":
                retorno = (mascara == "") ? prmParticipante.DsAuxiliar8 : oClsFuncoes.MascaraGerar(prmParticipante.DsAuxiliar8, mascara);
                break;
            case "txt_dsAuxiliar9":
                retorno = (mascara == "") ? prmParticipante.DsAuxiliar9 : oClsFuncoes.MascaraGerar(prmParticipante.DsAuxiliar9, mascara);
                break;
            case "txt_dsAuxiliar10":
                retorno = (mascara == "") ? prmParticipante.DsAuxiliar10 : oClsFuncoes.MascaraGerar(prmParticipante.DsAuxiliar10, mascara);
                break;
            case "txt_dsAuxiliar11":
                retorno = (mascara == "") ? prmParticipante.DsAuxiliar11 : oClsFuncoes.MascaraGerar(prmParticipante.DsAuxiliar11, mascara);
                break;
            case "txt_dsAuxiliar12":
                retorno = (mascara == "") ? prmParticipante.DsAuxiliar12 : oClsFuncoes.MascaraGerar(prmParticipante.DsAuxiliar12, mascara);
                break;
            case "txt_dsAuxiliar13":
                retorno = (mascara == "") ? prmParticipante.DsAuxiliar13 : oClsFuncoes.MascaraGerar(prmParticipante.DsAuxiliar13, mascara);
                break;
            case "txt_dsAuxiliar14":
                retorno = (mascara == "") ? prmParticipante.DsAuxiliar14 : oClsFuncoes.MascaraGerar(prmParticipante.DsAuxiliar14, mascara);
                break;
            case "txt_dsAuxiliar15":
                retorno = (mascara == "") ? prmParticipante.DsAuxiliar15 : oClsFuncoes.MascaraGerar(prmParticipante.DsAuxiliar15, mascara);
                break;
            case "txt_dsAuxiliar16":
                retorno = (mascara == "") ? prmParticipante.DsAuxiliar16 : oClsFuncoes.MascaraGerar(prmParticipante.DsAuxiliar16, mascara);
                break;
            case "txt_dsAuxiliar17":
                retorno = (mascara == "") ? prmParticipante.DsAuxiliar17 : oClsFuncoes.MascaraGerar(prmParticipante.DsAuxiliar16, mascara);
                break;
            case "txt_dsAuxiliar18":
                retorno = (mascara == "") ? prmParticipante.DsAuxiliar18 : oClsFuncoes.MascaraGerar(prmParticipante.DsAuxiliar18, mascara);
                break;
            case "txt_dsAuxiliar19":
                retorno = (mascara == "") ? prmParticipante.DsAuxiliar19 : oClsFuncoes.MascaraGerar(prmParticipante.DsAuxiliar19, mascara);
                break;
            case "txt_cdPatrocinador":
                retorno = prmParticipante.CdPatrocinador;
                break;
            case "txt_dsCampoExtraPreCad":
                retorno = prmParticipante.DsCampoExtraPreCad;
                break;
            case "txt_flAceiteSMS":
                retorno = prmParticipante.FlAceiteSMS.ToString();
                break;
            case "txt_dsComplementoEndereco":
                retorno = prmParticipante.DsComplementoEndereco;
                break;
            case "txt_dsCampoMultiplaEscolha1":
                retorno = prmParticipante.DsCampoMultiplaEscolha1;
                break;
            case "txt_dsCampoMultiplaEscolha2":
                retorno = prmParticipante.DsCampoMultiplaEscolha2;
                break;
            case "txt_dsCampoMultiplaEscolha3":
                retorno = prmParticipante.DsCampoMultiplaEscolha3;
                break;

            case "txt_dsPassagemAerea":
                retorno = prmParticipante.DsPassagemAerea;
                break;
            case "txt_dsVooCiaAereaIda":
                retorno = prmParticipante.DsVooCiaAereaIda;
                break;
            case "txt_dsVooOrigemIda":
                retorno = prmParticipante.DsVooOrigemIda;
                break;
            case "txt_dsVooDestinoIda":
                retorno = prmParticipante.DsVooDestinoIda;
                break;
            case "txt_dsVooDataSaidaIda":
                retorno = prmParticipante.DsVooDataSaidaIda;
                break;
            case "txt_dsVooHoraSaidaIda":
                retorno = prmParticipante.DsVooHoraSaidaIda;
                break;
            case "txt_dsVooDataChegadaIda":
                retorno = prmParticipante.DsVooDataChegadaIda;
                break;
            case "txt_dsVooHoraChegadaIda":
                retorno = prmParticipante.DsVooHoraChegadaIda;
                break;
            case "txt_dsVooIda":
                retorno = prmParticipante.DsVooIda;
                break;
            case "txt_dsVooLocalizadorIda":
                retorno = prmParticipante.DsVooLocalizadorIda;
                break;
            case "txt_dsVooCiaAereaVolta":
                retorno = prmParticipante.DsVooCiaAereaVolta;
                break;
            case "txt_dsVooOrigemVolta":
                retorno = prmParticipante.DsVooOrigemVolta;
                break;
            case "txt_dsVooDestinoVolta":
                retorno = prmParticipante.DsVooDestinoVolta;
                break;
            case "txt_dsVooDataSaidaVolta":
                retorno = prmParticipante.DsVooDataSaidaVolta;
                break;
            case "txt_dsVooHoraSaidaVolta":
                retorno = prmParticipante.DsVooHoraSaidaVolta;
                break;
            case "txt_dsVooDataChegadaVolta":
                retorno = prmParticipante.DsVooDataChegadaVolta;
                break;
            case "txt_dsVooHoraChegadaVolta":
                retorno = prmParticipante.DsVooHoraChegadaVolta;
                break;
            case "txt_dsVooVolta":
                retorno = prmParticipante.DsVooVolta;
                break;
            case "txt_dsVooLocalizadorVolta":
                retorno = prmParticipante.DsVooLocalizadorVolta;
                break;

            default:
                break;

        }
        return retorno;
    }

    private string PrpValorGravarMultiplaEscolha(CheckBoxList prmChkLstBox)
    {
        string retorno = "";

        if (prmChkLstBox.Items.Count <= 0)
            return retorno;

        for (int i = 0; i < prmChkLstBox.Items.Count; i++)
        {
            if (prmChkLstBox.Items[i].Selected)
            {
                if (retorno == "")
                    retorno = prmChkLstBox.Items[i].ToString();
                else
                    retorno += "#" + prmChkLstBox.Items[i].ToString();
            }
        }

        return retorno;
    }

    private string PrpValorGravarEscolhaLista(RadioButtonList prmOptLstBox)
    {
        string retorno = "";

        if (prmOptLstBox.Items.Count <= 0)
            return retorno;

        for (int i = 0; i < prmOptLstBox.Items.Count; i++)
        {
            if (prmOptLstBox.Items[i].Selected)
            {
                retorno = prmOptLstBox.Items[i].ToString();
            }
        }

        return retorno;
    }

    private bool prpGravar()
    {
        //if (!CriticarCampos())
        //    return false;
        lblMsg.Visible = false;
        lblMsg.Text = "";
        string tmpIdentificador = "";
        string tmpCPFPreCadastro = "";
        string tmpEmailPreCadastro = "";
        string tmpCdCategoria = "";
        string tmpDsCampoExtra = "";
        string tmpDsLabelNomeCampoExtra = "";
        string tmpCdPartPreCadastro = "";
        string tmpCdPatrocinador = "";

        ParticipantePreCadastro oParticipantePreCadastro = null;

        foreach (Control ctrlTable in form1.Controls)
        {
            if (ctrlTable is Table)
            {
                foreach (Control ctrlRow in ctrlTable.Controls)
                {
                    if (ctrlRow is TableRow)
                    {
                        foreach (Control ctrlCell in ctrlRow.Controls)
                        {
                            if (ctrlCell is TableCell)
                            {
                                foreach (Control ctrl in ctrlCell.Controls)
                                {
                                    if (ctrl is Label)
                                    {
                                        if (ctrl.ID == "txt_cdParticipante")
                                        {
                                            tmpIdentificador = (ctrl as Label).Text.Trim();
                                            //break;
                                        }
                                    }
                                    else if (ctrl is DropDownList)
                                    {
                                        if (ctrl.ID == "txt_cdCategoria")
                                        {
                                            tmpCdCategoria = (ctrl as DropDownList).SelectedValue.ToString();
                                            //break;
                                        }
                                    }
                                    else if (ctrl is TextBox)
                                    {
                                        if (ctrl.ID == "txt_nuCPFCNPJ")
                                        {
                                            tmpCPFPreCadastro = (ctrl as TextBox).Text.Trim();
                                            //break;
                                        }
                                        else
                                        if (ctrl.ID == "txt_dsEmail")
                                        {
                                            tmpEmailPreCadastro = (ctrl as TextBox).Text.Trim();
                                            //break;
                                        }
                                        else if (ctrl.ID == "txt_dsCampoExtraPreCad")
                                        {
                                            tmpDsCampoExtra = (ctrl as TextBox).Text.Trim();
                                            tmpDsLabelNomeCampoExtra = (ctrl as TextBox).ToolTip.ToString();
                                            //break;
                                        }
                                    }
                                    else if (ctrl is UpdatePanel)
                                    {
                                        foreach (Control ctrlContent in ctrl.Controls)
                                        {
                                            if (ctrlContent is Control)
                                            {
                                                foreach (Control ctrPanel in ctrlContent.Controls)
                                                {
                                                    if (ctrPanel is DropDownList)
                                                    {
                                                        if (ctrPanel.ID == "txt_cdCategoria")
                                                        {
                                                            tmpCdCategoria = (ctrPanel as DropDownList).SelectedValue.ToString();
                                                            //break;
                                                        }


                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        if (tmpCdCategoria != "")
        {
            if ((SessionParticipante != null) && (SessionParticipante.CdParticipante != "") && (SessionParticipante.CdCategoria != tmpCdCategoria))
            {
                PedidoCad oPedidoCad = new PedidoCad();
                Pedido tempPedido = oPedidoCad.VerificaPedidoNaoPagoWeb(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);
                if ((tempPedido != null))// || (tempPedido.TpPagamento == ""))
                {
                    oPedidoCad.AtivarDasativar(tempPedido, SessionCnn);
                }
            }

            CategoriaCad oCategoriaCad = new CategoriaCad();
            Categoria oCategoria = oCategoriaCad.Pesquisar(SessionEvento.CdEvento, tmpCdCategoria, SessionCnn);

            if ((oCategoria.QtdLimiteInscricao > 0) &&
                ((SessionParticipante == null) || (SessionParticipante.CdCategoria != tmpCdCategoria))
                )

            {
                int tmp = oParticipanteCad.TotalCadastradoCategoria(SessionEvento.CdEvento, tmpCdCategoria, SessionCnn);

                if (tmp < 0)
                {
                    lblMsg.Text = oParticipanteCad.RcMsg;
                    lblMsg.Visible = true;
                    return false;
                }
                else if (oCategoria.QtdLimiteInscricao == tmp)
                {
                    lblMsg.Text = "Limite máximo de inscrição para a categoria foi alcançado!\nNão é possível cadastrar mais nesta categoria.";
                    lblMsg.Visible = true;
                    return false;
                }
            }

            if (oCategoria.FlVerificarPreCadastro)
            {
                //DataTable DTPre = null;
                

                if (oCategoria.TpVerificacaoPreCadastro == "1")//CPF
                {
                    if (tmpCPFPreCadastro.Replace(".", "").Replace("-", "") == "")
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "Campo CPF obrigatório!";
                        return false;
                    }
                    oParticipantePreCadastro = oParticipanteCad.PesquisarPreCadastro(SessionEvento.CdEvento, tmpCdCategoria, tmpCPFPreCadastro.Replace(".", "").Replace("-", ""), "", "", "", SessionCnn);
                }
                else if (oCategoria.TpVerificacaoPreCadastro == "2")//E-mail
                {
                    if (tmpEmailPreCadastro.Replace(".", "").Replace("-", "") == "")
                    {
                        lblMsg.Visible = true;
                        if (SessionIdioma == "ENUS")
                            lblMsg.Text = "E-mail Required Field";
                        else if (SessionIdioma == "ESP")
                            lblMsg.Text = "E-mail requerida";
                        else if (SessionIdioma == "FRA")
                            lblMsg.Text = "E-mail Champ obligatoire";
                        else
                            lblMsg.Text = "Campo E-mail obrigatório!";

                        
                        return false;
                    }
                    oParticipantePreCadastro = oParticipanteCad.PesquisarPreCadastro(SessionEvento.CdEvento, tmpCdCategoria, "", "", tmpEmailPreCadastro, "", SessionCnn);
                }
                //else if (oCategoria.TpVerificacaoPreCadastro == "3")//Nome
                //    DTPre = oParticipanteCad.PesquisarPreCadastro(SessionEvento.CdEvento, tmpCdCategoria, tmpCPFPreCadastro, "", tmpEmailPreCadastro, "", SessionCnn);
                else if (oCategoria.TpVerificacaoPreCadastro == "4")//Extra
                {
                    if (tmpDsCampoExtra.Replace(".", "").Replace("-", "") == "")
                    {
                        lblMsg.Visible = true;
                        if (SessionIdioma == "ENUS")
                            lblMsg.Text = "Required Field";
                        else if (SessionIdioma == "ESP")
                            lblMsg.Text = "Campo obligatorio";
                        else if (SessionIdioma == "FRA")
                            lblMsg.Text = "Champs obligatoires";
                        else
                            lblMsg.Text = "Campo " + tmpDsLabelNomeCampoExtra + " obrigatório!";
                        
                        return false;
                    }
                    oParticipantePreCadastro = oParticipanteCad.PesquisarPreCadastro(SessionEvento.CdEvento, tmpCdCategoria, "", "", "", tmpDsCampoExtra, SessionCnn);
                }

                if (oParticipantePreCadastro == null) //((DTPre == null) || (DTPre.Rows.Count <= 0))
                {
                    lblMsg.Visible = true;
                    if (SessionIdioma == "ENUS")
                        lblMsg.Text = oCategoria.DsMsgPreCadastroIng;
                    else if (SessionIdioma == "ESP")
                        lblMsg.Text = oCategoria.DsMsgPreCadastroEsp;
                    else if (SessionIdioma == "FRA")
                        lblMsg.Text = oCategoria.DsMsgPreCadastroFra;
                    else
                        lblMsg.Text = oCategoria.DsMensagemPreCadastro;// "Não foi encontrado nenhum registro de ex-aluno para o CPF informado!";
                    return false;
                }
                else
                {
                    tmpCdPatrocinador = oParticipantePreCadastro.CdPatrocinador; //DTPre.DefaultView[0]["cdPatrocinador"].ToString();
                    if (oCategoria.FlPreCadastroExclusivo)
                    {
                        tmpCdPartPreCadastro = oParticipantePreCadastro.CdPartPreCadastro; //DTPre.DefaultView[0]["cdPartPreCadastro"].ToString();

                        if ((tmpIdentificador != "") && (oParticipantePreCadastro.CdParticipanteUso != "") && //(DTPre.DefaultView[0]["cdParticipanteUso"].ToString() != "") &&
                            (tmpIdentificador != oParticipantePreCadastro.CdParticipanteUso)) //DTPre.DefaultView[0]["cdParticipanteUso"].ToString()))
                        {
                            lblMsg.Visible = true;
                            if (SessionIdioma == "ENUS")
                                lblMsg.Text = "Pre-registration has already been used";
                            else if (SessionIdioma == "ESP")
                                lblMsg.Text = "Pre-inscripción ya se ha utilizado";
                            else if (SessionIdioma == "FRA")
                                lblMsg.Text = "Pré-inscription a déjà été utilisé";
                            else
                                lblMsg.Text = "Chave de Validação já utilizado por outro(a) Participante!";

                            btnFoco.Focus();
                            return false;
                        }

                        int totPrecadastradoChave = oParticipanteCad.TotalPreCadastradoChave(SessionEvento.CdEvento, tmpDsCampoExtra, SessionCnn);

                        if ((tmpIdentificador == "") && (oParticipantePreCadastro.CdParticipanteUso != "") &&
                            ((oParticipantePreCadastro.QtdLimiteInscricaoChave <= 0) ||
                             (totPrecadastradoChave >= oParticipantePreCadastro.QtdLimiteInscricaoChave))) //DTPre.DefaultView[0]["cdParticipanteUso"].ToString()))
                        {
                            lblMsg.Visible = true;
                            if (SessionIdioma == "ENUS")
                                lblMsg.Text = "Pre-registration has already been used";
                            else if (SessionIdioma == "ESP")
                                lblMsg.Text = "Pre-inscripción ya se ha utilizado";
                            else if (SessionIdioma == "FRA")
                                lblMsg.Text = "Pré-inscription a déjà été utilisé";
                            else
                                lblMsg.Text = "Chave de Validação já utilizado por outro(a) Participante!";
                            return false;
                        }
                    }
                    else
                    {
                        if ((tmpIdentificador == "") && (oParticipantePreCadastro.QtdLimiteInscricaoChave > 0)) //(int.Parse(DTPre.DefaultView[0]["qtdLimiteInscricaoChave"].ToString()) > 0))
                        {
                            int totPrecadastradoChave = oParticipanteCad.TotalPreCadastradoChave(SessionEvento.CdEvento, tmpDsCampoExtra, SessionCnn);

                            if (totPrecadastradoChave >= oParticipantePreCadastro.QtdLimiteInscricaoChave) //int.Parse(DTPre.DefaultView[0]["qtdLimiteInscricaoChave"].ToString()))
                            {
                                lblMsg.Visible = true;
                                lblMsg.Text = "Limite total de Pré-cadastro para esta chave foi atingido!<br/>Não é possível efetuar inscrição.";
                                return false;
                            }
                        }
                    }

                    //if ((DTPre.DefaultView[0]["dtValidade"].ToString() != "") && (DateTime.Parse(DTPre.DefaultView[0]["dtValidade"].ToString()) <= Geral.datahoraServidor(SessionCnn)))
                    if ((oParticipantePreCadastro.DtValidade != null) && (oParticipantePreCadastro.DtValidade.Value.Date <= Geral.datahoraServidor(SessionCnn).Date))
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = oCategoria.DsMensagemPreCadastro;// "Não foi encontrado nenhum registro de ex-aluno para o CPF informado!";
                        return false;
                    }
                }
            }
        }

        wrkEnviarEmail = false;

        if (tmpIdentificador == "")
        {
            wrkEnviarEmail = true;

            SessionParticipante = new Participante();
            SessionParticipante.CdEvento = SessionEvento.CdEvento;
        }

        if ((SessionEvento.CdEvento == "006602") && (SessionParticipante.DsAuxiliar4 != "SIM"))
            wrkEnviarEmail = true;

        foreach (Control ctrlTable in form1.Controls)
        {
            if (ctrlTable is Table)
            {
                foreach (Control ctrlRow in ctrlTable.Controls)
                {
                    if (ctrlRow is TableRow)
                    {
                        foreach (Control ctrlCell in ctrlRow.Controls)
                        {
                            if (ctrlCell is TableCell)
                            {
                                foreach (Control ctrl in ctrlCell.Controls)
                                {
                                    if (ctrl is Label)
                                    {
                                        if ((ctrl as Label).ID == "txt_cdParticipante")
                                            SessionParticipante = CarregarGravar((ctrl as Label).ID, (ctrl as Label).Text, SessionParticipante);
                                    }
                                    else if (ctrl is TextBox)
                                    {
                                        string valorCampo = (ctrl as TextBox).Text.Trim();

                                        if (((ctrl as TextBox).CssClass == "txt") || ((ctrl as TextBox).CssClass.Contains("maiusculo")))
                                            valorCampo = valorCampo.ToUpper();
                                        else if (((ctrl as TextBox).CssClass == "txtMinusculo") || ((ctrl as TextBox).CssClass.Contains("minusculo")))
                                            valorCampo = valorCampo.ToLower();

                                        SessionParticipante = CarregarGravar((ctrl as TextBox).ID, valorCampo, SessionParticipante);
                                        //SessionParticipante = CarregarGravar((ctrl as TextBox).ID, (ctrl as TextBox).Text, SessionParticipante);
                                    }
                                    else if (ctrl is DropDownList)
                                    {
                                        SessionParticipante = CarregarGravar((ctrl as DropDownList).ID, (ctrl as DropDownList).SelectedValue, SessionParticipante);

                                    }
                                    else if (ctrl is CheckBoxList)
                                    {
                                        SessionParticipante = CarregarGravar((ctrl as CheckBoxList).ID, PrpValorGravarMultiplaEscolha((ctrl as CheckBoxList)), SessionParticipante);

                                    }
                                    else if (ctrl is RadioButtonList)
                                    {
                                        SessionParticipante = CarregarGravar((ctrl as RadioButtonList).ID, PrpValorGravarEscolhaLista((ctrl as RadioButtonList)), SessionParticipante);

                                    }
                                    else if (ctrl is CheckBox)
                                    {
                                        SessionParticipante = CarregarGravar((ctrl as CheckBox).ID, (ctrl as CheckBox).Checked.ToString(), SessionParticipante);

                                    }
                                    else if (ctrl is UpdatePanel)
                                    {
                                        foreach (Control ctrlContent in ctrl.Controls)
                                        {
                                            if (ctrlContent is Control)
                                            {
                                                foreach (Control ctrPanel in ctrlContent.Controls)
                                                {
                                                    if (ctrPanel is DropDownList)
                                                    {
                                                        SessionParticipante = CarregarGravar((ctrPanel as DropDownList).ID, (ctrPanel as DropDownList).SelectedValue, SessionParticipante);



                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        #region Conasems
        //if (SessionEvento.CdCliente == "0013")
        //{
            //if ((SessionEvento.CdEvento == "001302") || (SessionEvento.CdEvento == "001303"))
            //{
            //    string tmpCargo = Geral.verificarCargoJaPreenchido(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionParticipante.NoCargo, SessionParticipante.DsAuxiliar8, SessionParticipante.DsAuxiliar9, SessionCnn);
            //    if (tmpCargo != "")
            //    {
            //        lblMsg.Visible = true;
            //        lblMsg.Text = tmpCargo;
            //        return false;
            //    }
            //}

            //if ((SessionEvento.CdEvento == "001304") || (SessionEvento.CdEvento == "001305"))
            //{
            //    if (SessionParticipante.NoAreaAtuacao == "SIM")
            //    {
            //        string tmpCargo = Geral.verificarCargoJaPreenchido(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionParticipante.DsAuxiliar8, SessionParticipante.DsAuxiliar9, SessionCnn);
            //        if (tmpCargo != "")
            //        {
            //            lblMsg.Visible = true;
            //            lblMsg.Text = tmpCargo;
            //            return false;
            //        }
            //    }
            //}


            ////if (SessionParticipante.NoCargo == "OUTROS")
            ////{
            ////    if (SessionParticipante.DsAuxiliar2.Trim() == "")
            ////    {
            ////        lblMsg.Text = "Campo \"Cargo(OUTROS)\" é de preenchimento obrigatório";
            ////        return false;
            ////    }
            ////    //if (SessionParticipante.DsAuxiliar3.Trim() == "")
            ////    //{
            ////    //    lblMsg.Text = "Campo \"Profissão\" é de preenchimento obrigatório";
            ////    //    return false;
            ////    //}
            ////}
            ////else 
            //{
            //    if ((SessionParticipante.NoCargo == "SECRETÁRIO(A) MUNICIPAL DE SAÚDE") ||
            //         (SessionParticipante.NoCargo == "PREFEITO(A) MUNICIPAL") ||
            //         (SessionParticipante.NoCargo == "VICE-PREFEITO(A) MUNICIPAL") ||
            //         (SessionParticipante.NoCargo == "CONSELHEIRO MUNICIPAL DE SAÚDE") ||
            //         (SessionParticipante.NoCargo == "VEREADOR"))
            //    {
            //        if ((SessionParticipante.DsAuxiliar8.Trim() == "") || (SessionParticipante.DsAuxiliar9.Trim() == ""))
            //        {
            //            lblMsg.Visible = true;
            //            lblMsg.Text = "Campos \"UF e Município (Cargo)\" são de preenchimento obrigatórios para o cargo selecionado";
            //            return false;
            //        }
            //    }
            //    else if ((SessionParticipante.NoCargo == "GOVERNADOR(A) DE ESTADO") ||
            //         (SessionParticipante.NoCargo == "VICE-GOVERNADOR(A) DE ESTADO") ||
            //         (SessionParticipante.NoCargo == "SECRETÁRIO(A) DE ESTADO DA SAÚDE") ||
            //         (SessionParticipante.NoCargo == "SENADOR(A) DA REPÚBLICA") ||
            //         (SessionParticipante.NoCargo == "DEPUTADO(A) FEDERAL") ||
            //         (SessionParticipante.NoCargo == "DEPUTADO(A) ESTADUAL/DISTRITAL") ||
            //         (SessionParticipante.NoCargo == "PRESIDENTE DE COSEMS") ||
            //         (SessionParticipante.NoCargo == "CONSELHEIRO ESTADUAL DE SAÚDE"))
            //    {
            //        if ((SessionParticipante.DsAuxiliar8.Trim() == ""))
            //        {
            //            lblMsg.Visible = true;
            //            lblMsg.Text = "Campos \"UF (Cargo)\" é de preenchimento obrigatório para o cargo selecionado";
            //            return false;
            //        }
            //    }

            //}

        //}

        #endregion

        #region SICOOB
        if (SessionEvento.CdEvento == "008303")
        {
            if (SessionParticipante.CdCategoria == "00830301") //CENTRAL
            {
                if (!VerificarTotalCentralSICOOB(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionParticipante.DsAuxiliar1))
                {
                    return false;
                }
                SessionParticipante.DsAuxiliar2 = "";
                SessionParticipante.NoInstituicao = "";
            }

            if (SessionParticipante.CdCategoria == "00830302") //SINGULAR
            {
                if (!VerificarTotalSingularSICOOB(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionParticipante.DsAuxiliar1, SessionParticipante.DsAuxiliar2))
                {
                    return false;
                }

                SessionParticipante.NoInstituicao = "";
            }

            if ((SessionParticipante.CdCategoria == "00830308") || (SessionParticipante.CdCategoria == "00830303") || (SessionParticipante.CdCategoria == "00830304")) //sicoob confederação
            {
                SessionParticipante.DsAuxiliar1 = "";
                SessionParticipante.DsAuxiliar2 = "";
            }
        }
        #endregion

        #region rehuna
        //if (SessionEvento.CdEvento == "008501")
        //{


        //    if (((SessionParticipante.CdCategoria == "00850108") || (SessionParticipante.CdCategoria == "00850115")))// && ((sender as TextBox).ID == "txt_dsAuxiliar1") && ((sender as TextBox).Text.Trim() != ""))
        //    {

        //        if (SessionParticipante.DsAuxiliar1 == "")
        //        {
        //            if (SessionIdioma == "PTBR")
        //                lblMsg.Text = "Campo obrigatório";
        //            else if (SessionIdioma == "ENUS")
        //                lblMsg.Text = "Requerid field";
        //            else if (SessionIdioma == "ENUS")
        //                lblMsg.Text = "Campo obligatorio";
        //            lblMsg.Visible = true;

        //            return false;
        //        }

        //        //SessionParticipante.DsAuxiliar1(sender as TextBox).Text = (sender as TextBox).Text.Trim().PadLeft(9, '0');
        //        Participante tmpPart = null;

        //        if (SessionIdioma == "PTBR")
        //            tmpPart = oParticipanteCad.Pesquisar(SessionEvento.CdEvento, "nuCPFCNPJ", SessionParticipante.DsAuxiliar1.Trim().Replace(".", "").Replace("-", ""), SessionCnn);
        //        else
        //            tmpPart = oParticipanteCad.Pesquisar(SessionEvento.CdEvento, "dsEmail", SessionParticipante.DsAuxiliar1.Trim(), SessionCnn);

                

        //        if ((tmpPart == null) || ((tmpPart != null) && ((tmpPart.CdCategoria == "00850108") || (tmpPart.CdCategoria == "00850115"))))
        //        {
        //            lblMsg.Text = "Nenhum participante principal encontrado para o número informado.";
        //            if (SessionIdioma == "ENUS")
        //                lblMsg.Text = "Main participant not found for informed e-mail.";


        //            if (SessionIdioma == "ESP")
        //                lblMsg.Text = "Actor principal no encontrado para el e-mail informado.";

        //            lblMsg.Visible = true;
                    
        //            return false;
        //        }
        //        else
        //            SessionParticipante.DsAuxiliar2 = tmpPart.CdParticipante;
        //    }
        //}
        #endregion

        if (SessionEvento.CdEvento == "003003")
        {
            if ((SessionParticipante.NuCPFCNPJ == "") && (SessionParticipante.DsAuxiliar1 == ""))
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Você deve Preencher CPF ou PASSAPORTE";
                return false;
            }

            if (SessionParticipante.DsCampoMultiplaEscolha1 == "")
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Você deve selecionar pelo menos um Tipo de Participação";
                return false;
            }

            if ((SessionParticipante.CdCategoria == "00300316") && (SessionParticipante.DsAuxiliar14 == ""))
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Você deve informar a subcategoria";
                return false;
            }
        }

        if (SessionEvento.CdEvento == "001003")
        {
            if ((SessionParticipante.NuCPFCNPJ == "") && (SessionParticipante.DsAuxiliar1 == ""))
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Você deve Preencher CPF ou PASSAPORTE";
                return false;
            }
        }

        //if (SessionEvento.CdEvento == "007701")
        //{
        if (SessionIdioma == "PTBR")
        {
            if ((SessionPais == "BRASIL"))// || (SessionPais == ""))
                SessionParticipante.NoPais = "BRASIL";
            else if (SessionPais != "")
                SessionParticipante.NuCPFCNPJ = "11111111111";

            if (SessionEvento.CdEvento == "007701")
                SessionParticipante.DsAuxiliar6 = "55";
            if (SessionEvento.CdEvento == "010801")
            {
                if (SessionPais == "BRASIL")
                {
                    SessionParticipante.DsAuxiliar2 = "55";
                }
                else
                {
                    SessionParticipante.NoCidade = SessionParticipante.DsAuxiliar3;
                }
            }
        }
        else //(SessionIdioma == "ENUS")
        {
            SessionParticipante.NuCPFCNPJ = "11111111111";
        }
        //}

        if (SessionEvento.CdEvento == "001605")
        {
            //if ((SessionParticipante.CdCategoria == "00160501") || (SessionParticipante.CdCategoria == "00160513"))
            //{
            //    if (SessionParticipante.NoInstituicao == "")
            //    {
            //        lblMsg.Visible = true;
            //        lblMsg.Text = "Você deve preencher o campo Razão Social";
            //        btnFoco.Focus();
            //        return false;
            //    }

            //    if (SessionParticipante.DsAuxiliar2 == "")
            //    {
            //        lblMsg.Visible = true;
            //        lblMsg.Text = "Você deve preencher o campo CNPJ";
            //        btnFoco.Focus();
            //        return false;
            //    }
            //}
        }

        if (oParticipantePreCadastro != null)
        {
            DescontoCad oDescontoCad = new DescontoCad();
            Desconto tmpDesc = oDescontoCad.PesquisarDescPatrocinadorCategoria(SessionEvento.CdEvento, oParticipantePreCadastro.CdPatrocinador, oParticipantePreCadastro.CdCategoria, SessionCnn);

            if (tmpDesc != null)
            {
                SessionParticipante.TpInscrPatrocinada = tmpDesc.CdDesconto;
            }

        }

        SessionParticipante.CdOperador = "000000001";//Participante WEB
        SessionParticipante.DsIdioma = SessionIdioma;

        if ((tmpCdPatrocinador != "") && ((SessionParticipante.CdPatrocinador == null) || (SessionParticipante.CdPatrocinador == "")))
            SessionParticipante.CdPatrocinador = tmpCdPatrocinador;


        if (SessionEvento.CdEvento == "006602")
            SessionParticipante.DsAuxiliar4 = "SIM";

        SessionParticipante = oParticipanteCad.Gravar(SessionParticipante, SessionCnn);

        //if (SessionEvento.CdEvento == "007701")
        if ((SessionEvento.FlMeeting) && (SessionParticipante.Categoria.FlParticiparMeeting))
        {
            MeetingParticipante tmpMeetingParticipante = new MeetingParticipante();

            if (SessionParticipante.meetingParticipante != null)
            {
                tmpMeetingParticipante = SessionParticipante.meetingParticipante;                                
            }

            
            tmpMeetingParticipante.CdEvento = SessionEvento.CdEvento;
            tmpMeetingParticipante.CdParticipante = SessionParticipante.CdParticipante;
            tmpMeetingParticipante.NoInstituicao = SessionParticipante.NoInstituicao;
            tmpMeetingParticipante.NoPais = SessionParticipante.NoPais;
            tmpMeetingParticipante.NoAreaAtuacao = ((SessionParticipante.NoAreaAtuacao.ToUpper().Contains("OUTROS")) || (SessionParticipante.NoAreaAtuacao.ToUpper().Contains("OTHER"))) ? SessionParticipante.DsAuxiliar3 : SessionParticipante.NoAreaAtuacao;
            tmpMeetingParticipante.DsWebsite = SessionParticipante.DsAuxiliar4;
            tmpMeetingParticipante.NoCargo = ((SessionParticipante.NoCargo.ToUpper().Contains("OUTROS")) || (SessionParticipante.NoCargo.ToUpper().Contains("OTHER"))) ? SessionParticipante.DsAuxiliar5 : SessionParticipante.NoCargo; 
            

            MeetingParticipante tmpMeetingParticipante2 = oParticipanteCad.oMeetingParticipanteCad.Gravar(tmpMeetingParticipante, SessionCnn);
        }

        if (((SessionEvento.CdEvento == "008501") && (SessionParticipante.CdCategoria != "00850108") && (SessionParticipante.CdCategoria != "00850115")))
        {

            TeseRepresentante SessionTeseRepresentante;
            TeseRepresentanteCad oTeseRepresentanteCad = new TeseRepresentanteCad();

            TeseRepresentante tmpTeseRepresentante = new TeseRepresentante();

            TeseRepresentante tmpTeseRepr = oTeseRepresentanteCad.PesquisarPorEmail(SessionEvento.CdEvento, SessionParticipante.DsEmail, SessionCnn);
            string prmCdRepresentante = "";
            if (tmpTeseRepr != null)
                prmCdRepresentante = tmpTeseRepr.CdRepresentante;

            tmpTeseRepresentante.CdRepresentante = prmCdRepresentante;

            tmpTeseRepresentante.CdEvento = SessionEvento.CdEvento;

            tmpTeseRepresentante.NuCPFRepresentante = SessionParticipante.NuCPFCNPJ.Replace(".", "").Replace("-", "");

            tmpTeseRepresentante.NoRepresentante = SessionParticipante.NoParticipante.ToUpper();
            tmpTeseRepresentante.DsSexo = SessionParticipante.DsSexo.ToUpper();
            tmpTeseRepresentante.NrPassaporte = SessionParticipante.DsDocIdentificador.ToUpper();
            tmpTeseRepresentante.DsEmail = SessionParticipante.DsEmail.ToLower();
            tmpTeseRepresentante.NoPais = SessionParticipante.NoPais.ToUpper();
            tmpTeseRepresentante.NuCEP = SessionParticipante.NuCEP.Replace(".", "").Replace("-", "");
            tmpTeseRepresentante.DsEndereco = SessionParticipante.DsEndereco.ToUpper();
            tmpTeseRepresentante.DsComplementoEndereco = SessionParticipante.DsComplementoEndereco.ToUpper();
            tmpTeseRepresentante.NoBairro = SessionParticipante.NoBairro.ToUpper();
            tmpTeseRepresentante.NoCidade = SessionParticipante.NoCidade.ToUpper();
            tmpTeseRepresentante.DsUF = SessionParticipante.DsUF.ToUpper();
            tmpTeseRepresentante.DsFone1 = SessionParticipante.DsFone1.Replace("(", "").Replace(")", "").Replace(" ", "");
            tmpTeseRepresentante.NoTitulacao = SessionParticipante.Categoria.NoCategoria;
            tmpTeseRepresentante.NoInstituicaoGraduacao = "";


            tmpTeseRepresentante.NoInstituicao = SessionParticipante.NoInstituicao.ToUpper();
            tmpTeseRepresentante.NoAreaAtuacao = SessionParticipante.NoAreaAtuacao.ToUpper();

            tmpTeseRepresentante.TpInstituicao = "";
            tmpTeseRepresentante.DsNivelGoverno = "";
            tmpTeseRepresentante.DsPoderGoverno = "";
            tmpTeseRepresentante.DsTipoSetorPrivado ="";


            tmpTeseRepresentante.DsSenhaAcesso = SessionParticipante.DsSenhaWeb;


            SessionTeseRepresentante = oTeseRepresentanteCad.Gravar(tmpTeseRepresentante, SessionCnn);

            Session["SessionTeseRepresentante"] = SessionTeseRepresentante;

        }


        if (tmpCdPartPreCadastro.Trim() != "")
            oParticipanteCad.PreCadastroMarcarParticipanteUso(SessionEvento.CdEvento, tmpCdPartPreCadastro, SessionParticipante.CdParticipante, SessionCnn);

        if (SessionParticipante != null)
        {
            pesquisar(SessionParticipante.CdParticipante);
            prpGravarPesquisa();

            lblMsg.Visible = true;
            if (SessionIdioma == "ENUS")
                lblMsg.Text = "Operation successful";
            else if (SessionIdioma == "ESP")
                lblMsg.Text = "Operación éxito";
            else if (SessionIdioma == "FRA")
                lblMsg.Text = "Opération réussie";
            else
                lblMsg.Text = "Operação Realizada com sucesso!";

            return true;

        }
        else
        {
            lblMsg.Visible = true;
            lblMsg.Text = oParticipanteCad.RcMsg;
            return false;
        }
    }

    public Participante CarregarGravar(string ctrl, String prmValorCtrl, Participante prmParticip)
    {
        
        switch (ctrl)
        {
            case "txt_cdParticipante":
                {
                    if (prmValorCtrl.Trim() != "")
                        prmValorCtrl = prmValorCtrl.PadLeft(9, '0');

                    prmParticip.CdParticipante = prmValorCtrl;
                    break;
                }
            case "txt_cdCategoria":
                prmParticip.CdCategoria = prmValorCtrl;
                break;
            case "txt_noParticipante":
                prmParticip.NoParticipante = prmValorCtrl;
                break;
            case "txt_noEtiqueta":
                prmParticip.NoEtiqueta = prmValorCtrl;
                break;
            case "txt_nuCPFCNPJ":
                prmParticip.NuCPFCNPJ = prmValorCtrl;
                break;
            case "txt_dsDocIdentificador":
                prmParticip.DsDocIdentificador = prmValorCtrl;
                break;
            case "txt_dsEmail":
                prmParticip.DsEmail = prmValorCtrl;
                break;
            case "txt_nuCEP":
                prmParticip.NuCEP = prmValorCtrl;
                break;
            case "txt_dsEndereco":
                prmParticip.DsEndereco = prmValorCtrl;
                break;
            case "txt_noBairro":
                prmParticip.NoBairro = prmValorCtrl;
                break;
            case "txt_noCidade":
                prmParticip.NoCidade = prmValorCtrl;
                break;
            case "txt_dsUF":
                prmParticip.DsUF = prmValorCtrl;
                break;
            case "txt_noPais":
                prmParticip.NoPais = prmValorCtrl;
                break;
            case "txt_dsFone1":
                prmParticip.DsFone1 = prmValorCtrl.Replace(" ","").Trim();
                break;
            case "txt_dsFone2":
                prmParticip.DsFone2 = prmValorCtrl.Replace(" ", "").Trim();
                break;
            case "txt_dsFone3":
                prmParticip.DsFone3 = prmValorCtrl.Replace(" ", "").Trim();
                break;
            case "txt_noInstituicao":
                prmParticip.NoInstituicao = prmValorCtrl;
                break;
            case "txt_noCargo":
                prmParticip.NoCargo = prmValorCtrl;
                break;
            case "txt_noAreaAtuacao":
                prmParticip.NoAreaAtuacao = prmValorCtrl;
                break;
            case "txt_dsSexo":
                prmParticip.DsSexo = prmValorCtrl;
                break;
            case "txt_dsNascimento":
                prmParticip.DsNascimento = prmValorCtrl;
                break;
            case "txt_dsObservacoes":
                prmParticip.DsObservacoes = prmValorCtrl;
                break;
            case "txt_dsAuxiliar1":
                prmParticip.DsAuxiliar1 = prmValorCtrl;
                break;
            case "txt_dsAuxiliar2":
                prmParticip.DsAuxiliar2 = prmValorCtrl;
                break;
            case "txt_dsAuxiliar3":
                prmParticip.DsAuxiliar3 = prmValorCtrl;
                break;
            case "txt_dsAuxiliar4":
                prmParticip.DsAuxiliar4 = prmValorCtrl;
                break;
            case "txt_dsAuxiliar5":
                prmParticip.DsAuxiliar5 = prmValorCtrl;
                break;
            case "txt_dsAuxiliar6":
                prmParticip.DsAuxiliar6 = prmValorCtrl;
                break;
            case "txt_dsAuxiliar7":
                prmParticip.DsAuxiliar7 = prmValorCtrl;
                break;
            case "txt_dsAuxiliar8":
                prmParticip.DsAuxiliar8 = prmValorCtrl;
                break;
            case "txt_dsAuxiliar9":
                prmParticip.DsAuxiliar9 = prmValorCtrl;
                break;
            case "txt_dsAuxiliar10":
                prmParticip.DsAuxiliar10 = prmValorCtrl;
                break;
            case "txt_dsAuxiliar11":
                prmParticip.DsAuxiliar11 = prmValorCtrl;
                break;
            case "txt_dsAuxiliar12":
                prmParticip.DsAuxiliar12 = prmValorCtrl;
                break;
            case "txt_dsAuxiliar13":
                prmParticip.DsAuxiliar13 = prmValorCtrl;
                break;
            case "txt_dsAuxiliar14":
                prmParticip.DsAuxiliar14 = prmValorCtrl;
                break;
            case "txt_dsAuxiliar15":
                prmParticip.DsAuxiliar15 = prmValorCtrl;
                break;
            case "txt_dsAuxiliar16":
                prmParticip.DsAuxiliar16 = prmValorCtrl;
                break;
            case "txt_dsAuxiliar17":
                prmParticip.DsAuxiliar17 = prmValorCtrl;
                break;
            case "txt_dsAuxiliar18":
                prmParticip.DsAuxiliar18 = prmValorCtrl;
                break;
            case "txt_dsAuxiliar19":
                prmParticip.DsAuxiliar19 = prmValorCtrl;
                break;
            case "txt_cdPatrocinador":
                prmParticip.CdPatrocinador = prmValorCtrl;
                break;
            case "txt_dsSenhaWeb":
                prmParticip.DsSenhaWeb = prmValorCtrl;
                break;
            case "txt_dsCampoExtraPreCad":
                prmParticip.DsCampoExtraPreCad = prmValorCtrl;
                break;
            case "txt_flAceiteSMS":
                prmParticip.FlAceiteSMS = bool.Parse(prmValorCtrl);
                break;
            case "txt_dsComplementoEndereco":
                prmParticip.DsComplementoEndereco = prmValorCtrl;
                break;
            case "txt_dsCampoMultiplaEscolha1":
                prmParticip.DsCampoMultiplaEscolha1 = prmValorCtrl;
                break;
            case "txt_dsCampoMultiplaEscolha2":
                prmParticip.DsCampoMultiplaEscolha2 = prmValorCtrl;
                break;
            case "txt_dsCampoMultiplaEscolha3":
                prmParticip.DsCampoMultiplaEscolha3 = prmValorCtrl;
                break;

            case "txt_dsPassagemAerea":
                prmParticip.DsPassagemAerea = prmValorCtrl;
                break;
            case "txt_dsVooCiaAereaIda":
                prmParticip.DsVooCiaAereaIda = prmValorCtrl;
                break;
            case "txt_dsVooOrigemIda":
                prmParticip.DsVooOrigemIda = prmValorCtrl;
                break;
            case "txt_dsVooDestinoIda":
                prmParticip.DsVooDestinoIda = prmValorCtrl;
                break;
            case "txt_dsVooDataSaidaIda":
                prmParticip.DsVooDataSaidaIda = prmValorCtrl;
                break;
            case "txt_dsVooHoraSaidaIda":
                prmParticip.DsVooHoraSaidaIda = prmValorCtrl;
                break;
            case "txt_dsVooDataChegadaIda":
                prmParticip.DsVooDataChegadaIda = prmValorCtrl;
                break;
            case "txt_dsVooHoraChegadaIda":
                prmParticip.DsVooHoraChegadaIda = prmValorCtrl;
                break;
            case "txt_dsVooIda":
                prmParticip.DsVooIda = prmValorCtrl;
                break;
            case "txt_dsVooLocalizadorIda":
                prmParticip.DsVooLocalizadorIda = prmValorCtrl;
                break;
            case "txt_dsVooCiaAereaVolta":
                prmParticip.DsVooCiaAereaVolta = prmValorCtrl;
                break;
            case "txt_dsVooOrigemVolta":
                prmParticip.DsVooOrigemVolta = prmValorCtrl;
                break;
            case "txt_dsVooDestinoVolta":
                prmParticip.DsVooDestinoVolta = prmValorCtrl;
                break;
            case "txt_dsVooDataSaidaVolta":
                prmParticip.DsVooDataSaidaVolta = prmValorCtrl;
                break;
            case "txt_dsVooHoraSaidaVolta":
                prmParticip.DsVooHoraSaidaVolta = prmValorCtrl;
                break;
            case "txt_dsVooDataChegadaVolta":
                prmParticip.DsVooDataChegadaVolta = prmValorCtrl;
                break;
            case "txt_dsVooHoraChegadaVolta":
                prmParticip.DsVooHoraChegadaVolta = prmValorCtrl;
                break;
            case "txt_dsVooVolta":
                prmParticip.DsVooVolta = prmValorCtrl;
                break;
            case "txt_dsVooLocalizadorVolta":
                prmParticip.DsVooLocalizadorVolta = prmValorCtrl;
                break;

            default:
                break;
        }
        return prmParticip;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        btnGravarParticipante.Enabled = false;

        lblMsg.Visible = false;
        lblMsg.Text = "";

        wrkLiberadoGravar = true;
        ValidarCamposObrigatorios();
        if (!wrkLiberadoGravar)
        {
            btnGravarParticipante.Enabled = true;
            return;

        }

        if (!prpGravar())
        {
            btnGravarParticipante.Enabled = true;
            return;

        }

       // Response.Redirect("http://site.portalcomunidadevip.com.br");
        
        //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
        //                        "014",
        //                        ""), true);

        Session["SessionParticipante"] = SessionParticipante;

        Session["SessionEvento"] = SessionEvento;

        Session["SessionCnn"] = SessionCnn;

        if (SessionTipoSistema == "NRM")
        {
            //CategoriaCad oCategoriaCad = new CategoriaCad();
            //Categoria oCategoria = oCategoriaCad.Pesquisar(SessionEvento.CdEvento, SessionParticipante.CdCategoria, SessionCnn);
            
            //wrkEnviarEmail = true;

            if ((wrkEnviarEmail == true) && (SessionEvento.FlEnviarMensagensEmail) && (SessionParticipante.Categoria.FlEnviarEmail))
            {
                Geral oGeral = new Geral();
                if ((SessionParticipante.Categoria.FlAtividades) && (SessionEvento.FlEventoComRecebimentos) &&
                    (SessionEvento.CdCliente != "0029"))
                    oGeral.EnviarEmailCadastro(SessionEvento, SessionParticipante, SessionCnn);
                else
                {
                    if (SessionEvento.CdEvento == "006602")
                    {
                        oGeral.EnviarEmailApenasCadastroQRCode(SessionEvento, SessionParticipante, SessionCnn);

                        Response.Write("<script>window.open('frmQRCode.aspx','_self');</script>");
                        return;
                    }
                    else
                    {
                        oGeral.EnviarEmailApenasCadastro(SessionEvento, SessionParticipante, SessionCnn);
                    }
                }
            }

            wrkEnviarEmail = true;//forçando acontecer

            if (wrkEnviarEmail == true)
            {
                if ((SessionEvento.CdCliente != "0013"))// && (SessionEvento.CdEvento != "004802"))
                {
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

                    if ((SessionParticipante.Categoria != null) && (SessionParticipante.Categoria.FlAtividades) && (!SessionParticipante.FlConfirmacaoInscricao))
                    {
                        PedidoCad oPedidoCad = new PedidoCad();
                        Pedido tempPedido = oPedidoCad.VerificaPedidoNaoPagoWeb(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);
                        if ((tempPedido == null) || (tempPedido.TpPagamento == ""))
                        {
                            if ((SessionEvento.CdEvento == "008501") && (SessionParticipante.CdCategoria != "00850108") && (SessionParticipante.CdCategoria != "00850115"))
                                Response.Write("<script>window.open('frmEscolherAcao.aspx','_self');</script>");
                            else
                                Response.Write("<script>window.open('frmSelAtividades.aspx','_self');</script>");
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
                        Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                            "002",
                                            ""), true);
                    }
                }
                //else if (SessionEvento.CdEvento == "004802")
                //{
                //    Response.Write("<script>window.open('frmEnviarDocumento2.aspx','_self');</script>");
                //    return;
                //}
                else if (SessionEvento.CdCliente == "0013")
                {
                    if (SessionParticipante.Categoria.FlQuestionario)
                    {
                        PesquisasDeOpiniao oPesquisasDeOpiniao = new PesquisasDeOpiniao();
                        DataTable dtPesquisas = oPesquisasDeOpiniao.ListarPesquisasDoParticipanteWeb(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionParticipante.DsIdioma, SessionCnn);
                        
                        if ((dtPesquisas != null) && (dtPesquisas.Rows.Count > 0))
                        {
                            Session["oDTPesquisa"] = null;
                            Response.Write("<script>window.open('frmPesquisaVinculada.aspx','_self');</script>");
                        }
                    }

                    if ((SessionParticipante.NoAreaAtuacao == "SIM") && (SessionParticipante.DsAuxiliar19 != "SIM"))
                    {//SECRETÁRIOS MUNICIPAIS DE SAÚDE DEVEM RESPONDER A DECLARAÇÃO
                        Response.Write("<script>window.open('frmTermoConasemsSecretarios.aspx','_self');</script>");
                        return;
                    }

                    if ((Geral.datahoraServidor(SessionCnn) < DateTime.Parse("30/07/2015 00:00:00"))) 
                    {
                        if ((SessionParticipante.Categoria.FlAtividades) && (!SessionParticipante.FlConfirmacaoInscricao))
                        {
                            PedidoCad oPedidoCad = new PedidoCad();
                            Pedido tempPedido = oPedidoCad.VerificaPedidoNaoPagoWeb(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);
                            if ((tempPedido == null) || (tempPedido.TpPagamento == ""))
                            {
                                Response.Write("<script>window.open('frmSelAtividades.aspx','_self');</script>");
                            }

                            if ((SessionParticipante.NoAreaAtuacao == "SIM") && (!SessionParticipante.FlDocumentoConfirmado) && (SessionParticipante.participanteDocEnviado == null))
                            {//SECRETÁRIOS MUNICIPAIS DE SAÚDE DEVEM ENVIAR A DECLARAÇÃO
                                Response.Write("<script>window.open('frmEnviarDocumento.aspx','_self');</script>");
                            }

                            //else
                            //{
                            //    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                            //                        "014",
                            //                        ""), true);
                            //}
                        }
                        //else
                        //{
                            Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                                "014",
                                                ""), true);
                        //}
                    }
                    else
                    {
                        if ((Geral.datahoraServidor(SessionCnn) < DateTime.Parse("04/08/2015 00:00:00")))
                        {
                            if ((SessionParticipante.NoAreaAtuacao == "SIM") && (!SessionParticipante.FlDocumentoConfirmado) && (SessionParticipante.participanteDocEnviado == null))
                            {//SECRETÁRIOS MUNICIPAIS DE SAÚDE DEVEM ENVIAR A DECLARAÇÃO
                                Response.Write("<script>window.open('frmEnviarDocumento.aspx','_self');</script>");
                            }
                            if (SessionParticipante.CdCategoria != "00130501")
                            {
                                if ((SessionParticipante.Categoria.FlAtividades) && (!SessionParticipante.FlConfirmacaoInscricao))
                                {
                                    PedidoCad oPedidoCad = new PedidoCad();
                                    Pedido tempPedido = oPedidoCad.VerificaPedidoNaoPagoWeb(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);
                                    if ((tempPedido == null) || (tempPedido.TpPagamento == ""))
                                    {
                                        Response.Write("<script>window.open('frmSelAtividades.aspx','_self');</script>");
                                    }                                   
                                }
                            }
                        }

                        Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                            "014",
                                            (SessionParticipante.Categoria.FlPagamento ? "Dirija-se ao caixa no local do evento para concluir sua inscrição." : "Não esqueça de imprimir sua credencial.")
                                            ), true);
                    }
                }
            }
            else
                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                        "002",
                                        ""), true);
        }
        else
        {
            SessionOperacao = "INICIO";
            Session["SessionOperacao"] = SessionOperacao;

            if (SessionParticipante.Categoria.FlAtividades)
                Response.Write("<script>window.open('frmSelAtividades.aspx','_self');</script>");
            else
            {
                Response.Write("<script>window.open('frmRedirecionaNovo.aspx','_self');</script>");
            }
        }
        /*
        //return;
        foreach (Control ctrlTable in form1.Controls)
        {
            if (ctrlTable is Table)
            {
                foreach (Control ctrlRow in ctrlTable.Controls)
                {
                    if (ctrlRow is TableRow)
                    {
                        foreach (Control ctrlCell in ctrlRow.Controls)
                        {
                            if (ctrlCell is TableCell)
                            {
                                foreach (Control ctrl in ctrlCell.Controls)
                                {
                                    if (ctrl is TextBox)
                                    {
                                        if ((ctrl as TextBox).ID == "txt_cdParticipante")
                                            lblMsg.Text += (ctrl as TextBox).Text + "<br/>";
                                        //lblMsg.Text += ((TextBox)this.Page.FindControl("txt_cdParticipante")).Text + "<br/>";
                                        else
                                            if ((ctrl as TextBox).ID == "txt_nuCPFCNPJ")
                                                lblMsg.Text += (ctrl as TextBox).Text + "<br/>";
                                    }
                                    else if (ctrl is DropDownList)
                                    {
                                        if ((ctrl as DropDownList).ID == "txt_cdCategoria")
                                            lblMsg.Text += (ctrl as DropDownList).Text + "<br/>";
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }*/
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        //Session["SessionParticipante"] = SessionParticipante;

        //Session["SessionEvento"] = SessionEvento;

        //Session["SessionCnn"] = SessionCnn;

        //Response.Write("<script>window.open('Default7.aspx','_self');</script>");

        //Server.Transfer("Default7.aspx", true);
    }

    private void localizarPesquisa(string prmCdEvento, string prmCdParticipante)
    {
        string comando =
            "SELECT q.cdQuestionario, " +
            "       q.dsQuestionario, " +
            "       g.cdGrupoPergunta, " +
            "       g.dsGrupoPergunta, " +
            "       cdQuestao,  " +
            "       dsQuestao,   " +
            "       TpQuestao,   " +
            "       p.flAtivo   " +
            "  FROM tbquestionarios q " +
            "  join tbGruposPerguntas g " +
            "    on q.cdQuestionario = g.cdQuestionario " +
            "  join tbQuestoes p " +
            "    on q.cdQuestionario = p.cdQuestionario " +
            "   and g.cdGrupoPergunta = p.cdGrupoPergunta " +
            "WHERE q.cdEvento = '" + prmCdEvento + "'  " +
            "and (q.flAtivo = 1)  " +
            "and (g.flAtivo = 1)  " + 
            "and (q.flVisivelWeb = 1)  " +
            "and (g.flVisivelWeb = 1)  " +
            "and (p.flAtivo = 1) ";
        if ((prmCdParticipante != null) && (prmCdParticipante != ""))
        {
            comando += "  AND (q.cdQuestionario + g.cdGrupoPergunta NOT IN " +
                     "(SELECT cdQuestionario + cdGrupoPergunta " +
                        "FROM tbQuestoesRespostas AS tqr " +
                       "WHERE (cdParticipante = '" + prmCdParticipante + "') " +
                        " AND (cdEvento = '" + prmCdEvento + "'))) ";
        }

        SqlCommand cmd = new SqlCommand(comando);

        SqlDataSource1.SelectCommand = cmd.CommandText;
        DataList1.DataSourceID = "SqlDataSource1";
        DataList1.DataBind();

       // if (((DataTable)DataList1.DataSource) == null || ((DataTable)DataList1.DataSource).Rows.Count <= 0)
       //     DataList1.Visible = false;

        

    }

    protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HiddenField anstype = (HiddenField)e.Item.FindControl("HiddenField1");
            HiddenField cdPesquisa = (HiddenField)e.Item.FindControl("HiddenField2");
            HiddenField cdGrupoPesquisa = (HiddenField)e.Item.FindControl("HiddenField3");
            Label questionid = (Label)e.Item.FindControl("cdQuestaoLabel");
            RadioButtonList rbl = (RadioButtonList)e.Item.FindControl("RadioButtonList1");
            CheckBoxList cbl = (CheckBoxList)e.Item.FindControl("CheckBoxList1");
            TextBox txt = (TextBox)e.Item.FindControl("TextBox1");
            DataSet ds = GetDataSet(cdPesquisa.Value.ToString(), cdGrupoPesquisa.Value.ToString(), questionid.Text);
            switch (anstype.Value)
            {
                case "0":
                    rbl.Visible = true;
                    cbl.Visible = false;
                    txt.Visible = false;
                    rbl.DataSource = ds;
                    rbl.DataTextField = "dsQuestaoItem";
                    rbl.DataValueField = "cdQuestaoItem";
                    rbl.DataBind();
                    break;
                case "1":
                    rbl.Visible = false;
                    cbl.Visible = true;
                    txt.Visible = false;
                    cbl.DataSource = ds;
                    cbl.DataTextField = "dsQuestaoItem";
                    cbl.DataValueField = "cdQuestaoItem";
                    cbl.DataBind();
                    break;
                case "2":
                    rbl.Visible = false;
                    cbl.Visible = false;
                    txt.Visible = true;
                    break;
            }
        }
    }

    private DataSet GetDataSet(string prm_cdQuestionario, string prm_cdGrupoPergunta, string prm_cdQuestao)
    {
        //SqlConnection cnn = new SqlConnection("Data Source=krksa-pc;Initial Catalog=dbEventos_FM;Persist Security Info=True;User ID=sa;Password=krksa171");
        //SqlConnection cnn = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=dbEventos_FM;Persist Security Info=True;User ID=sa;Password=RaC982973");
        SqlCommand cmd = new SqlCommand("select * from tbQuestoesItens " +
                                        "where cdQuestionario = @cdQuestionario " +
                                        "  and cdGrupoPergunta = @cdGrupoPergunta " +
                                        "  and cdQuestao =@cdQuestao ", SessionCnn);
        SqlParameter p1 = new SqlParameter("@cdQuestionario", prm_cdQuestionario);
        SqlParameter p2 = new SqlParameter("@cdGrupoPergunta", prm_cdGrupoPergunta);
        SqlParameter p3 = new SqlParameter("@cdQuestao", prm_cdQuestao);
        //SqlParameter p4 = new SqlParameter("@cdEvento", "000401001");
        cmd.Parameters.Add(p1);
        cmd.Parameters.Add(p2);
        cmd.Parameters.Add(p3);
        //cmd.Parameters.Add(p4);
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        DataSet ds = new DataSet();
        da.Fill(ds, "tbQuestoesItens");
        return ds;
    }

    protected void btnGravar_Click(object sender, EventArgs e)
    {

    }

    protected void prpGravarPesquisa()
    {
        lblMsg.Visible = false;
        foreach (DataListItem item in DataList1.Items)
        {
            if (item.ItemType == ListItemType.Item | item.ItemType == ListItemType.AlternatingItem)
            {
                string cdQuestionario = ((HiddenField)item.FindControl("HiddenField2")).Value.ToString();
                string cdGrupoPergunta = ((HiddenField)item.FindControl("HiddenField3")).Value.ToString();
                string cdQuestao = ((Label)item.FindControl("cdQuestaoLabel")).Text;
                string cdResposta = "";
                string dsResposta = "";

                HiddenField tpQuestao = (HiddenField)item.FindControl("HiddenField1");
                switch (tpQuestao.Value)
                {
                    case "0"://"S"
                        RadioButtonList rbl = (RadioButtonList)item.FindControl("RadioButtonList1");
                        cdResposta = rbl.SelectedValue.ToString();                        
                        lblMsg.Text += tpQuestao.Value + "/" + cdQuestionario + "/" + cdGrupoPergunta + "/" + cdQuestao + "/" + cdResposta + "/" + dsResposta + "<br/>";
                        //SaveAnswer(cdQuestionario, cdGrupoPergunta, cdQuestao, cdResposta, dsResposta);
                        oPesquisasResposta.IncluirRespostaItem(SessionEvento.CdEvento, SessionParticipante.CdParticipante, 
                            cdQuestionario, cdGrupoPergunta, cdQuestao, cdResposta, dsResposta, SessionCnn);
                        break;
                    case "1"://"M"
                        CheckBoxList cbl = (CheckBoxList)item.FindControl("CheckBoxList1");
                        //Label2.Text = cbl.Items.Count.ToString();
                        for (int i = 0; i <= cbl.Items.Count - 1; i++)
                        {
                            if (cbl.Items[i].Selected)
                            {
                                cdResposta = cbl.Items[i].Value;
                                lblMsg.Text += tpQuestao.Value + "/" + cdQuestionario + "/" + cdGrupoPergunta + "/" + cdQuestao + "/" + cdResposta + "/" + dsResposta + "<br/>";
                                //SaveAnswer(cdQuestionario, cdGrupoPergunta, cdQuestao, cdResposta, dsResposta);
                                oPesquisasResposta.IncluirRespostaItem(SessionEvento.CdEvento, SessionParticipante.CdParticipante,
                                cdQuestionario, cdGrupoPergunta, cdQuestao, cdResposta, dsResposta, SessionCnn);
                            }
                        }

                        break;
                    case "2"://"T"
                        TextBox txt = (TextBox)item.FindControl("TextBox1");
                        dsResposta = txt.Text;
                        cdResposta = "01";
                        //SaveAnswer(cdQuestionario, cdGrupoPergunta, cdQuestao, "01", dsResposta);
                        oPesquisasResposta.IncluirRespostaItem(SessionEvento.CdEvento, SessionParticipante.CdParticipante,
                            cdQuestionario, cdGrupoPergunta, cdQuestao, cdResposta, dsResposta, SessionCnn);
                        break;
                }

            }
        }
        //DataList1.Visible = false;
        lblMsg.Visible = true;
        lblMsg.Text += "Obrigado pela participação na pesquisa!";

    }

    protected void ValidarCamposObrigatorios()
    {
        lblMsg.Visible = false;
        lblMsg.Text = "";
        //wrkLiberadoGravar = true;
        foreach (Control ctrlTable in form1.Controls)
        {
            if (ctrlTable is Table)
            {
                foreach (Control ctrlRow in ctrlTable.Controls)
                {
                    if (ctrlRow is TableRow)
                    {
                        foreach (Control ctrlCell in ctrlRow.Controls)
                        {
                            if (ctrlCell is TableCell)
                            {
                                foreach (Control ctrl in ctrlCell.Controls)
                                {
                                    if (ctrl is TextBox)
                                    {
                                        //conasems
                                        if ((SessionEvento.CdCliente == "0013") && ((ctrl as TextBox).ID == "txt_dsAuxiliar4") && ((ctrl as TextBox).Text == ""))
                                        {
                                            Control controleLabel = FindControlRecursive(ctrlTable, "txt_dsAuxiliar1");
                                            if ( (controleLabel != null) && ((controleLabel as DropDownList).Text != "OUTROS"))
                                            {
                                                lblMsg.Visible = true;
                                                lblMsg.Text += "<br/>" + "CNPJ Requerido!";
                                                wrkLiberadoGravar = false;
                                                return;
                                            }
                                        }//-- fim conasems
                                        else if ((ctrl as TextBox).ID == "txt_nuCPFCNPJ") 
                                        {
                                            string tmp = "";
                                            string tmpCdCategoria = "";
                                            Control controleCateg = FindControlRecursive(this.Page, "txt_cdCategoria");
                                            if (controleCateg != null)
                                            {
                                                tmpCdCategoria = (controleCateg as DropDownList).SelectedValue.ToString();
                                            }
                                            if (SessionParticipante != null)
                                                tmp = SessionParticipante.CdParticipante;

                                            string tmpCPF = ValidarCPF(tmp, SessionEvento.CdEvento, (ctrl as TextBox).Text, tmpCdCategoria, SessionCnn);
                                            if (tmpCPF != "")
                                            {
                                                lblMsg.Visible = true;
                                                lblMsg.Text += "<br/>" + tmpCPF;
                                                wrkLiberadoGravar = false;
                                                return;
                                            }
                                        }
                                        else if ((ctrl as TextBox).ID == "txt_dsEmail") 
                                        {
                                            string tmpEMail = ValidarEmail(SessionParticipante.CdParticipante, SessionEvento.CdEvento, (ctrl as TextBox).Text, SessionCnn);
                                            if (tmpEMail != "")
                                            {
                                                lblMsg.Visible = true;
                                                lblMsg.Text += "<br/>"+tmpEMail;
                                                wrkLiberadoGravar = false;
                                                return;
                                            }
                                        }
                                        else //if (((ctrl as TextBox).ID == "txt_nuCEP") && ((source as CustomValidator).ID == "txt_nuCEP_Validar") && ((ctrl as TextBox).ToolTip == "Obrigatório"))
                                            if (((ctrl as TextBox).ToolTip == "Obrigatório") && (ctrl as TextBox).Visible != false)
                                            {
                                                if ((ctrl as TextBox).Text.Replace(".", "").Replace("-", "").Replace("(", "").Replace(")", "").Trim().Replace("/", "").Trim() == "")
                                                {
                                                    lblMsg.Visible = true;
                                                    lblMsg.Text += "<br/>" + "Campo Requerido";
                                                    wrkLiberadoGravar = false;
                                                    return;
                                                }
                                            }



                                    }


                                }
                            }
                        }
                    }
                }
            }
        }


    }

    protected void txt_cdCategoria_SelectedIndexChanged(object sender, EventArgs e)
    {
        string cdCateg = (sender as DropDownList).SelectedValue;

        Control controlePainel = FindControlRecursive(this.Page, "pnlMsgCateg");
        if (controlePainel != null)
        {
            (controlePainel as Panel).Visible = false;
        }

        //SessionCateg = cdCateg;
        //Session["SessionCateg"] = cdCateg;

        CategoriaCad oCategoriaCad = new CategoriaCad();
        Categoria tempCategoria = oCategoriaCad.Pesquisar(SessionEvento.CdEvento, cdCateg, SessionCnn);

        if (tempCategoria != null)
        {
            if (((tempCategoria.FlRequerConfirmacaoDoc) || (tempCategoria.DsMensagemConfirmacaoDoc != "")) &&
                ((SessionParticipante == null) ||
                 (!SessionParticipante.FlDocumentoConfirmado)))
            {
                //controlePainel = FindControlRecursive(this.Page, "pnlMsgCateg");
                if (controlePainel != null)
                {
                    (controlePainel as Panel).Visible = true;
                    Control controleLabel = FindControlRecursive(controlePainel, "lblMsgCateg");
                    {
                        if (controleLabel != null)
                        {
                            if (SessionEvento.CdEvento != "008501")
                                (controleLabel as Label).Text = tempCategoria.DsMensagemConfirmacaoDoc;
                            else
                            {
                                if (SessionIdioma != "ESP")
                                    (controleLabel as Label).Text = tempCategoria.DsMensagemConfirmacaoDoc;
                                else
                                    (controleLabel as Label).Text = tempCategoria.DsMsgPreCadastroEsp;
                            }
                           // return;
                        }
                    }
                }
            }

            if ((SessionEvento.CdEvento == "007701") && (tempCategoria.FlPagamento))
            {
                PedidoCad oPedidoCad = new PedidoCad();
                Pedido tempPedido = oPedidoCad.VerificaPedidoNaoPagoWeb(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);

                if (tempPedido != null) 
                {
                    //(ctrPanel as DropDownList).Enabled = false;
                    //lblMsg.Visible = true;
                    //lblMsg.Text = "Constam Pedidos de Atividade/Curso/Produto. Não é possivel alterar a categoria!";

                    if (controlePainel != null)
                    {
                        (controlePainel as Panel).Visible = true;
                        Control controleLabel = FindControlRecursive(controlePainel, "lblMsgCateg");
                        {
                            if (controleLabel != null)
                            {
                                if (SessionIdioma == "PTBR")
                                    (controleLabel as Label).Text = tempCategoria.DsMensagemConfirmacaoDoc + "<br /><br /><b>ATENÇÃO:</B> Já extiste um pedido de inscrição em andamento. A alteração de categoria implica no cancelamento deste pedido e geração de outro.";
                                else if (SessionIdioma == "ENUS")
                                    (controleLabel as Label).Text = tempCategoria.DsMensagemConfirmacaoDoc;// +"<br /><br /><b>WARNING:</B> There is already an application for registration in progress. Changing category implies the cancellation of this request and generation of another.";
                                return;
                            }
                        }
                    }

                }
            }

        }

        if (SessionEvento.CdEvento == "008303")
        {
            Control dsCampo1 = FindControlRecursive(this.Page, "txt_dsAuxiliar2");//cooperativa singular?
            Control linha1 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar2");
            Control dsLblCampo1 = FindControlRecursive(this.Page, "lbl_dsAuxiliar2");

            Control dsCampo3 = FindControlRecursive(this.Page, "txt_dsAuxiliar1");//cooperativa singular?
            Control linha3 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar1");
            Control dsLblCampo3 = FindControlRecursive(this.Page, "lbl_dsAuxiliar1");

            Control dsCampo2 = FindControlRecursive(this.Page, "txt_noInstituicao");//instituição
            Control linha2 = FindControlRecursive(this.Page, "tblinha_noInstituicao");
            Control dsLblCampo2 = FindControlRecursive(this.Page, "lbl_noInstituicao");

            if (cdCateg == "00830302") //categoria: cooperativa singular?
            {
                if (linha1 != null)
                {
                    (dsCampo1 as DropDownList).Visible = true;
                    (linha1 as TableRow).Visible = true;
                    (dsLblCampo1 as Label).Visible = true;

                    (dsCampo3 as DropDownList).Visible = true;
                    (linha3 as TableRow).Visible = true;
                    (dsLblCampo3 as Label).Visible = true;

                    (dsCampo2 as TextBox).Visible = false;
                    (linha2 as TableRow).Visible = false;
                    (dsLblCampo2 as Label).Visible = false;
                }
            }
            else
            {
                if (linha1 != null)
                {
                    (dsCampo1 as DropDownList).Visible = false;
                    (linha1 as TableRow).Visible = false;
                    (dsLblCampo1 as Label).Visible = false;

                    (dsCampo2 as TextBox).Visible = false;
                    (linha2 as TableRow).Visible = false;
                    (dsLblCampo2 as Label).Visible = false;
                }
            }

            if (cdCateg == "00830301")
            {
                (dsCampo3 as DropDownList).Visible = true;
                (linha3 as TableRow).Visible = true;
                (dsLblCampo3 as Label).Visible = true;
            }

            if ((cdCateg == "00830308") || (cdCateg == "00830303"))
            {
                (dsCampo1 as DropDownList).Visible = false;
                (linha1 as TableRow).Visible = false;
                (dsLblCampo1 as Label).Visible = false;

                (dsCampo3 as DropDownList).Visible = false;
                (linha3 as TableRow).Visible = false;
                (dsLblCampo3 as Label).Visible = false;

                (dsCampo2 as TextBox).Visible = true;
                (linha2 as TableRow).Visible = true;
                (dsLblCampo2 as Label).Visible = true;
            }

        }

        #region rehuna
        if (SessionEvento.CdEvento == "008501")
        {
            Control dsCampo1 = FindControlRecursive(this.Page, "txt_dsAuxiliar1");//Nr Inscrição do Participante principal *
            Control linha1 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar1");
            Control dsLblCampo1 = FindControlRecursive(this.Page, "lbl_dsAuxiliar1");

            Control dsCampo2 = FindControlRecursive(this.Page, "txt_noInstituicao");//Instituicao
            Control linha2 = FindControlRecursive(this.Page, "tblinha_noInstituicao");
            Control dsLblCampo2 = FindControlRecursive(this.Page, "lbl_noInstituicao");

            Control dsCampo3 = FindControlRecursive(this.Page, "txt_noAreaAtuacao");//noAreaAtuacao
            Control linha3 = FindControlRecursive(this.Page, "tblinha_noAreaAtuacao");
            Control dsLblCampo3 = FindControlRecursive(this.Page, "lbl_noAreaAtuacao");

            if ((cdCateg == "00850108") || (cdCateg == "00850115")) //categoria: cooperativa singular?
            {
                if (linha1 != null)
                {
                    (dsCampo1 as TextBox).Visible = true;
                    (linha1 as TableRow).Visible = true;
                    (dsLblCampo1 as Label).Visible = true;

                }
                if (linha2 != null)
                {
                    (dsCampo2 as TextBox).Visible = false;
                    (dsCampo2 as TextBox).Text = "";
                    (linha2 as TableRow).Visible = false;
                    (dsLblCampo2 as Label).Visible = false;

                }
                if (linha1 != null)
                {
                    (dsCampo3 as TextBox).Visible = false;
                    (dsCampo3 as TextBox).Text = "";
                    (linha3 as TableRow).Visible = false;
                    (dsLblCampo3 as Label).Visible = false;

                }
            }
            else
            {
                if (linha1 != null)
                {
                    (dsCampo1 as TextBox).Visible = false;
                    (dsCampo1 as TextBox).Text = "";
                    (linha1 as TableRow).Visible = false;
                    (dsLblCampo1 as Label).Visible = false;
                }
                if (linha2 != null)
                {
                    (dsCampo2 as TextBox).Visible = true;
                    (linha2 as TableRow).Visible = true;
                    (dsLblCampo2 as Label).Visible = true;

                }
                if (linha1 != null)
                {
                    (dsCampo3 as TextBox).Visible = true;
                    (linha3 as TableRow).Visible = true;
                    (dsLblCampo3 as Label).Visible = true;

                }
            }

        }
        #endregion

        if (SessionEvento.CdEvento == "004401")
        {
            Control dsCampo1 = FindControlRecursive(this.Page, "txt_dsAuxiliar8");//Responsável por pessoa com defeiciência?
            Control linha1 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar8");

            Control dsCampo2 = FindControlRecursive(this.Page, "txt_dsAuxiliar9");//Representante de Entidade de Assistência?
            Control linha2 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar9");

            Control dsCampo3 = FindControlRecursive(this.Page, "txt_dsAuxiliar10");//Informe qual entidade representa
            Control linha3 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar10");
            
            if (cdCateg != "00440105") //categoria: REPRESENTANTES e  RESPONSÁVEIS
            {
                if (linha1 != null)
                {
                    (dsCampo1 as DropDownList).Visible = false;
                    (linha1 as TableRow).Visible = false;
                }
                if (linha2 != null)
                {
                    (dsCampo2 as DropDownList).Visible = false;
                    (linha2 as TableRow).Visible = false;
                }
                if (linha3 != null)
                {
                    (dsCampo3 as TextBox).Visible = false;
                    (linha3 as TableRow).Visible = false;
                }
            }
            else
            {
                if (linha1 != null)
                {
                    (dsCampo1 as DropDownList).Visible = true;
                    (linha1 as TableRow).Visible = true;
                }
                if (linha2 != null)
                {
                    (dsCampo2 as DropDownList).Visible = true;
                    (linha2 as TableRow).Visible = true;
                }
                if (linha3 != null)
                {
                    (dsCampo3 as DropDownList).Visible = true;
                    (linha3 as TableRow).Visible = true;
                }
            }


            Control dsCampo45 = FindControlRecursive(this.Page, "txt_dsCampoExtraPreCad");//Informe qual entidade representa
            Control linha45 = FindControlRecursive(this.Page, "tblinha_dsCampoExtraPreCad");

            if (cdCateg != "00440106") //categoria: REPRESENTANTES e  RESPONSÁVEIS
            {
                if (linha45 != null)
                {
                    (linha45 as TableRow).Visible = false;
                }
            }
            else
            {
                if (linha45 != null)
                {
                    (linha45 as TableRow).Visible = true;
                }
            }
        }
        
        
    }

    private Control FindControlRecursive(Control root, string id)
    {
        if (root.ID == id)
        {
            return root;
        }

        foreach (Control c in root.Controls)
        {
            Control t = FindControlRecursive(c, id);
            if (t != null)
            {
                return t;
            }
        }

        return null;
    }

    private void btnPesquisar_Click(object sender, ImageClickEventArgs e)
    {
        lblMsg.Visible = false;
        lblMsg.Text = "";

        
        string tmpPesq = "";
        if ((sender as ImageButton).ID == "btnPesqtxt_cdParticipante")
        {
            Control txt = FindControlRecursive(this.Page, "txt_cdParticipante");
            if ((txt as TextBox).Text.Trim() == "")
                return;

            (txt as TextBox).Text = (txt as TextBox).Text.PadLeft(9, '0');

            tmpPesq = (txt as TextBox).Text;
        }
        else
        {
            Control txt = FindControlRecursive(this.Page, "txt_nuCPFCNPJ");
            if ((txt as TextBox).Text.Trim() == "")
                return;

            tmpPesq = (txt as TextBox).Text.Replace(".", "").Replace("-", "").Replace("/", "");

       
        }

        Participante tempPart = oParticipanteCad.Pesquisar(SessionEvento.CdEvento, (sender as ImageButton).ID.Replace("btnPesqtxt_", ""), tmpPesq, SessionCnn);

        if (tempPart == null)
        {


            //lblMsg.Text = oParticipanteCad.RcMsg;
            //return;

            //Response.Write(@"<script language='javascript'>alert('" + oParticipanteCad.RcMsg + "');</script>");

            SessionParticipante = new Participante();
            Session["SessionParticipante"] = SessionParticipante;

            SessionOperacao = "INICIO";
            Session["SessionOperacao"] = SessionOperacao;

            Response.Write("<script>window.open('frmRedirecionaNovo.aspx','_self');</script>");
            
            return;
        }

        SessionParticipante = tempPart;
        Session["SessionParticipante"] = SessionParticipante;

        SessionOperacao = "INICIO";
        Session["SessionOperacao"] = SessionOperacao;


        pesquisar(tempPart.CdParticipante);
        //Response.Write("<script>window.open('frmRedirecionaNovo.aspx','_self');</script>");

    }

    private void btnDadosAnteriores_Click(object sender, EventArgs e)
    {
        prpDadosAnteriores();
    }
    private bool prpDadosAnteriores()
    {
        lblMsg.Text = "";
        lblMsg.Visible = false;
        string nuCPF = "";

        Control controlePainel = FindControlRecursive(this.Page, "txt_nuCPFCNPJ");
        if (controlePainel != null)
        {
            nuCPF = (controlePainel as TextBox).Text;
        }

        if (nuCPF == "")
        {
            lblMsg.Visible = true;
            lblMsg.Text = "Informe o CPF";
            return false;
        }

        
        if (nuCPF != null)
        {

            //string cpftemp = CPF.Replace(".", "").Replace("-", "").Trim();

            string tmpCPF = ValidarCPF("", SessionEvento.CdEvento, nuCPF, SessionCateg, SessionCnn);
            if (tmpCPF != "")
            {
                lblMsg.Visible = true;
                lblMsg.Text = tmpCPF;
                return false;
            }
        }

        SqlConnection SessionCnnEvAnt = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOWJYTnpjV3d1Wm1GNlpXNWtiMjFoYVhNdVkyOXRPMGx1YVhScFlXd2dRMkYwWVd4dlp6MW1ZWHBsYm1SdmJXRnBjekU3VUdWeWMybHpkQ0JUWldOMWNtbDBlU0JKYm1adlBWUnlkV1U3VlhObGNpQkpSRDFtWVhwbGJtUnZiV0ZwY3pFN1VHRnpjM2R2Y21ROVVYVTBOVFJ5Um0wPQ==")));

        Participante oParticipante = oParticipanteCad.PesquisarEventosAnteriores(SessionEvento.CdEvento, nuCPF, SessionCnnEvAnt);


        if (oParticipante != null)
        {

            foreach (Control ctrlTable in form1.Controls)
            {
                if (ctrlTable is Table)
                {
                tmp:
                    foreach (Control ctrlRow in ctrlTable.Controls)
                    {
                        if (ctrlRow is TableRow)
                        {
                            foreach (Control ctrlCell in ctrlRow.Controls)
                            {
                                if (ctrlCell is TableCell)
                                {
                                    foreach (Control ctrl in ctrlCell.Controls)
                                    {
                                        if (ctrl is Label)
                                        {
                                            if ((ctrl as Label).ID != "txt_cdParticipante")
                                            {
                                                string tmpval = BuscarValores(oParticipante, (ctrl as Label).ID, "");
                                                if (tmpval != "")
                                                    (ctrl as Label).Text = tmpval;
                                            }
                                        }
                                        else if (ctrl is TextBox)
                                        {
                                            if (((ctrl as TextBox).ID != "txt_dsCampoExtraPreCad") &&
                                                 (!(ctrl as TextBox).ID.Contains("dsVoo")))
                                            {
                                                //if (SessionEvento.FlPesquisaCPFReceita)
                                                //{
                                                //    if ((ctrl as TextBox).ID != "txt_noParticipante")
                                                //        (ctrl as TextBox).Text = BuscarValores(oParticipante, (ctrl as TextBox).ID, (ctrl as TextBox).SkinID);
                                                //}
                                                //else
                                                (ctrl as TextBox).Text = BuscarValores(oParticipante, (ctrl as TextBox).ID, (ctrl as TextBox).SkinID);
                                            }
                                            //if (((ctrl as TextBox).ID == "txt_dsEmail_validar") ||
                                            //    ((ctrl as TextBox).ID.Contains("txt_dsSenhaWeb")))
                                            //{
                                            //    (ctrlTable as Table).Controls.Remove(ctrlRow);
                                            //    goto tmp;
                                            //}
                                        }
                                        else if (ctrl is DropDownList)
                                        {
                                            if (((ctrl as DropDownList).ID != "txt_cdCategoria") &&
                                                ((ctrl as DropDownList).ID != "txt_dsUF") &&
                                                ((ctrl as DropDownList).ID != "txt_noCidade") &&
                                                ((((SessionEvento.CdCliente == "0013") && (int.Parse(SessionEvento.CdEvento) >= 1303)) || (SessionEvento.CdCliente == "0003")) &&
                                                 ((ctrl as DropDownList).ID != "txt_dsAuxiliar8") &&
                                                 ((ctrl as DropDownList).ID != "txt_dsAuxiliar9") &&
                                                 ((ctrl as DropDownList).ID != "txt_dsPassagemAerea") &&
                                                 (!(ctrl as DropDownList).ID.Contains("dsVoo"))
                                                ) &&
                                                (((SessionEvento.CdCliente == "0013") && (int.Parse(SessionEvento.CdEvento) >= 1303)) &&
                                                 ((ctrl as DropDownList).ID != "txt_noAreaAtuacao")
                                                )

                                                )
                                                (ctrl as DropDownList).SelectedValue = BuscarValores(oParticipante, (ctrl as DropDownList).ID, "");


                                        }
                                        else if (ctrl is CascadingDropDown)
                                        {
                                            if ((ctrl as CascadingDropDown).ID == "cascaDD_dsUF")
                                            {
                                                (ctrl as CascadingDropDown).SelectedValue = BuscarValores(oParticipante, "txt_dsUF", "");
                                            }
                                            else if ((ctrl as CascadingDropDown).ID == "cascaDD_noCidade")
                                            {
                                                (ctrl as CascadingDropDown).SelectedValue = BuscarValores(oParticipante, "txt_noCidade", "");
                                            }

                                            if (((SessionEvento.CdCliente == "0013") && (int.Parse(SessionEvento.CdEvento) >= 1303)) || (SessionEvento.CdCliente == "0003"))
                                            {
                                                if ((ctrl as CascadingDropDown).ID == "cascaDD_dsAuxiliar8")
                                                {
                                                    (ctrl as CascadingDropDown).SelectedValue = BuscarValores(SessionParticipante, "txt_dsAuxiliar8", "");
                                                }
                                                else if ((ctrl as CascadingDropDown).ID == "cascaDD_dsAuxiliar9")
                                                {
                                                    (ctrl as CascadingDropDown).SelectedValue = BuscarValores(SessionParticipante, "txt_dsAuxiliar9", "");
                                                }
                                            }
                                        }
                                        else if (ctrl is CheckBox)
                                        {
                                            (ctrl as CheckBox).Checked = Boolean.Parse(BuscarValores(oParticipante, (ctrl as CheckBox).ID, ""));


                                        }
                                        else if (ctrl is UpdatePanel)
                                        {
                                            foreach (Control ctrlContent in ctrl.Controls)
                                            {
                                                if (ctrlContent is Control)
                                                {
                                                    foreach (Control ctrPanel in ctrlContent.Controls)
                                                    {
                                                        if (ctrPanel is DropDownList)
                                                        {
                                                            if ((ctrPanel as DropDownList).ID != "txt_cdCategoria")
                                                                (ctrPanel as DropDownList).SelectedValue = BuscarValores(oParticipante, (ctrPanel as DropDownList).ID, "");

                                                            //if (SessionEvento.FlEventoComRecebimentos)
                                                            //{
                                                            //    PedidoCad oPedidoCad = new PedidoCad();
                                                            //    Pedido tempPedido = oPedidoCad.VerificaPedidoNaoPagoWeb(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);

                                                            //    if (tempPedido != null)
                                                            //    {
                                                            //        (ctrPanel as DropDownList).Enabled = false;
                                                            //        lblMsg.Text = "Constam Pedidos de Atividade/Curso/Produto. Não é possivel alterar a categoria!";
                                                            //    }

                                                            //}
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return true;
        }
        else
        {
            lblMsg.Visible = true;
            lblMsg.Text = oParticipanteCad.RcMsg;
            return false;
        }
    }

    protected void btnCEP_Click(object sender, EventArgs e)
    {
        Control lblmsgCEP = FindControlRecursive(this.Page, "lblMsgCEP");
        if (lblmsgCEP != null)
            lblmsgCEP.Visible = false;

        Control txtdsEndereco = FindControlRecursive(this.Page, "txt_dsEndereco");
        if (txtdsEndereco != null)
            (txtdsEndereco as TextBox).Text = "";

        Control txtnoBairro = FindControlRecursive(this.Page, "txt_noBairro");
        if (txtnoBairro != null)
            (txtnoBairro as TextBox).Text = "";

        Control txtdsUF = FindControlRecursive(this.Page, "cascaDD_dsUF");
        if (txtdsUF != null)
            (txtdsUF as CascadingDropDown).SelectedValue = "";

        Control txtnoCidade = FindControlRecursive(this.Page, "cascaDD_noCidade");
        if (txtnoCidade != null)
            (txtnoCidade as CascadingDropDown).PromptText = "";

        Control txtnuCEP = FindControlRecursive(this.Page, "txt_nuCEP");
        if ((txtnuCEP == null) || ((txtnuCEP as TextBox).Text == ""))
            return;
        
        try
        {
            //XMLHTTP http = new XMLHTTP();

            //http.open("POST", "http://webservice.uni5.net/web_cep.php?auth=8739185af299ee7b1463b579a90fee5a&formato=xml&cep=" + (txtnuCEP as TextBox).Text.Replace(".", ""), false, null, null);
            //http.send(null);


            //XmlDocument xmlDoc = new XmlDocument();


            //xmlDoc.LoadXml(http.responseText);

            //XmlNode No = xmlDoc.SelectSingleNode("webservicecep");


            //if (txtdsEndereco != null)
            //    (txtdsEndereco as TextBox).Text = No.ChildNodes.Item(5).InnerText.ToUpper() + " " +
            //                                      No.ChildNodes.Item(6).InnerText.ToUpper(); //logradouro

            //if (txtnoBairro != null)
            //    (txtnoBairro as TextBox).Text = No.ChildNodes.Item(4).InnerText.ToUpper();//bairro

            //if (txtdsUF != null)
            //    (txtdsUF as CascadingDropDown).SelectedValue = No.ChildNodes.Item(2).InnerText.ToUpper();//UF

            //string noCidade = No.ChildNodes.Item(3).InnerText.ToUpper();//cidade
            //noCidade = noCidade.Replace("'",":");
            //noCidade = oClsFuncoes.RemoveAcento(noCidade);
            //noCidade = noCidade.Replace(":","`");

            //if (txtnoCidade != null)
            //    (txtnoCidade as CascadingDropDown).PromptText = noCidade;


            /*
            string filename = @"http://webservice.uni5.net/web_cep.php?auth=8739185af299ee7b1463b579a90fee5a&formato=xml&cep=" + (txtnuCEP as TextBox).Text.Replace(".", "");
            XmlTextReader reader = new XmlTextReader(filename);
            string strTempName, strTempValue;
            reader.MoveToContent();

            string tipoLogradouro, logradouro, bairro, cidade, uf;

            tipoLogradouro = logradouro = bairro = cidade = uf = "";

            do
            {
                strTempName = reader.Name;
                if (reader.NodeType == XmlNodeType.Element)
                {
                    reader.Read();
                    strTempValue = reader.Value;
                    switch (strTempName)
                    {
                        case "tipo_logradouro":
                            tipoLogradouro = strTempValue.ToUpper();
                            break;
                        case "logradouro":
                            logradouro = strTempValue.ToUpper();
                            break;
                        case "bairro":
                            bairro = strTempValue.ToUpper();
                            break;
                        case "cidade":
                            cidade = strTempValue.ToUpper();
                            break;
                        case "uf":
                            uf = strTempValue.ToUpper();
                            break;
                        case "resultado":
                            if (strTempValue == "1")
                            {
                                //cep ok
                            }
                            else
                            {
                                if (strTempValue == "-1")
                                {
                                    (lblmsgCEP as Label).Text= "CEP não encontrado";
                                }
                                else if (strTempValue == "-2")
                                {
                                    (lblmsgCEP as Label).Text = "Formato de CEP inválido";
                                }
                                else if (strTempValue == "-3")
                                {
                                    (lblmsgCEP as Label).Text= "Busca de CEP congestionada. Aguarde alguns segundos e tente novamente.";
                                }
                                else
                                {
                                    (lblmsgCEP as Label).Text = "CEP não encontrado.";
                                }
                                lblmsgCEP.Visible = true;
                            }
                            break;
                    }
                }
            } while (reader.Read());
            */
            WebCEP webcep = new WebCEP((txtnuCEP as TextBox).Text.Replace(".", "").Replace("-", ""));

            if (webcep != null)
            {
                if (txtdsEndereco != null)
                    (txtdsEndereco as TextBox).Text = webcep.TipoLagradouro + " " +
                                                      webcep.Lagradouro; //logradouro

                if (txtnoBairro != null)
                    (txtnoBairro as TextBox).Text = webcep.Bairro;//bairro

                if (txtdsUF != null)
                    (txtdsUF as CascadingDropDown).SelectedValue = webcep.UF;//UF

                string noCidade = webcep.Cidade;//cidade
                noCidade = noCidade.Replace("'", ":");
                noCidade = oClsFuncoes.RemoveAcento(noCidade);
                noCidade = noCidade.Replace(":", "`");

                if (SessionEvento.DsFormaGuardarMunicipio == "cdMunicipioIBGE")
                {
                    noCidade = Geral.BuscarCodigoIBGE(webcep.UF, noCidade.ToUpper(), SessionCnn);
                    if (int.Parse(noCidade) > 0)
                    {
                        if (txtnoCidade != null)
                            (txtnoCidade as CascadingDropDown).SelectedValue = noCidade.ToUpper();
                    }
                }
                else
                    (txtnoCidade as CascadingDropDown).SelectedValue = noCidade.ToUpper();
            }
            else
            {
                (lblmsgCEP as Label).Text = "CEP não localizado!";
                lblmsgCEP.Visible = true;
            }
        }
        catch
        {
            if (lblmsgCEP != null)
            {
                (lblmsgCEP as Label).Text = "Erro ao pesquisar CEP!";
                lblmsgCEP.Visible = true;
            }
        }
    }

    protected void txt_TextChanged(object sender, EventArgs e)
    {
        lblMsg.Visible = false;

        if (((sender as TextBox).ID == "txt_noParticipante") && ((sender as TextBox).Text.Trim() != ""))
        {
            Control nomeetiqueta = FindControlRecursive(this.Page, "txt_noEtiqueta");
            if (nomeetiqueta != null)
            {
                if ((nomeetiqueta as TextBox).Text == "")
                {
                    (nomeetiqueta as TextBox).Text = NomeReduzido((sender as TextBox).Text);
                    (nomeetiqueta as TextBox).Focus();
                    return;
                }
            }
            
        }


        if (((sender as TextBox).ID == "txt_nuCPFCNPJ") &&
            ((sender as TextBox).Text.Trim() != "") &&
            ((sender as TextBox).Text.Replace(".", "").Replace("-", "").Trim().Length == 11))
        {
            #region Buscar dados Eventos Anteriores
            if (SessionEvento.FlUtilizarDadoEventoAnterior)
            {
                if (prpDadosAnteriores())
                    return;

            }
            #endregion

            #region Pesquisar CPF Receita

            if (SessionEvento.FlPesquisaCPFReceita)
            {

                Control CPF = FindControlRecursive(this.Page, "txt_nuCPFCNPJ");
                if (CPF != null)
                {

                    string cpftemp = (sender as TextBox).Text.Replace(".", "").Replace("-", "").Trim();

                    string tmpCPF = ValidarCPF("", SessionEvento.CdEvento, cpftemp, SessionCateg, SessionCnn);
                    if (tmpCPF != "")
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = tmpCPF;
                        return;
                    }



                    string tempCPF = cpftemp;

                    if ((tempCPF == "11111111111") ||
                        (tempCPF == "22222222222") ||
                        (tempCPF == "33333333333") ||
                        (tempCPF == "44444444444") ||
                        (tempCPF == "55555555555") ||
                        (tempCPF == "66666666666") ||
                        (tempCPF == "77777777777") ||
                        (tempCPF == "88888888888") ||
                        (tempCPF == "99999999999") ||
                        (tempCPF == "00000000000"))
                    {
                        CategoriaCad oCategoriaCad = new CategoriaCad();
                        Categoria oCategoria = oCategoriaCad.Pesquisar(SessionEvento.CdEvento, SessionCateg, SessionCnn);
                        if (((SessionEvento != null) && (SessionEvento.FlLiberarCPFCoringa)) || ((oCategoria != null) && ((!oCategoria.FlCPFCNPJObrigatorio) || (oCategoria.FlLiberarCPFCNPJCoringa))))
                        {
                            //SessionEvento.FlPesquisaCPFReceita = false;
                            //SessionEvento.FlUtilizarDadoEventoAnterior = false;
                            //Session["SessionEvento"] = SessionEvento;
                            //Server.Transfer("frmCadastroAuto.aspx?nome=&cpf=11111111111", true);
                        }
                        else
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "CPF Inválido!";
                        }
                    }
                    else
                    {
                        //PESQUISAR CPF BANCO LOCAL
                        SqlConnection SessionCnnHISTORICO = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOWJYTnpjV3d1Wm1GNlpXNWtiMjFoYVhNdVkyOXRPMGx1YVhScFlXd2dRMkYwWVd4dlp6MW1ZWHBsYm1SdmJXRnBjekU3VUdWeWMybHpkQ0JUWldOMWNtbDBlU0JKYm1adlBWUnlkV1U3VlhObGNpQkpSRDFtWVhwbGJtUnZiV0ZwY3pFN1VHRnpjM2R2Y21ROVVYVTBOVFJ5Um0wPQ==")));
                        DataTable DTCpf = oParticipanteCad.PesquisaCPF(tempCPF, SessionCnnHISTORICO);
                        if ((DTCpf != null) && (DTCpf.Rows.Count > 0))
                        {

                            //Server.Transfer("frmCadastroAuto.aspx?nome=" + DTCpf.DefaultView[0]["Nome"].ToString() + "&cpf=" + DTCpf.DefaultView[0]["nuCPF"].ToString(), true);
                            Control nome = FindControlRecursive(this.Page, "txt_noParticipante");
                            if (nome != null)
                            {
                                if ((nome as TextBox).Text == "")
                                {
                                    (nome as TextBox).Text = DTCpf.DefaultView[0]["Nome"].ToString();
                                    (nome as TextBox).Focus();
                                }
                            }
                            Control nomeetiqueta = FindControlRecursive(this.Page, "txt_noEtiqueta");
                            if (nomeetiqueta != null)
                            {
                                if ((nomeetiqueta as TextBox).Text == "")
                                {
                                    (nomeetiqueta as TextBox).Text = NomeReduzido(DTCpf.DefaultView[0]["Nome"].ToString());
                                    (nomeetiqueta as TextBox).Focus();
                                }
                            }

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
                                    "&cpf=" + tempCPF.Replace("-", ""));

                                if (ds != null)
                                {
                                    if (ds.Tables[0].Rows.Count > 0)
                                    {
                                        if (ds.Tables[0].Rows[0]["MensagemErro"].ToString() != "")
                                        {
                                            lblMsg.Visible = true;
                                            lblMsg.Text = ds.Tables[0].Rows[0]["MensagemErro"].ToString();
                                        }
                                        else
                                        {
                                            oParticipanteCad.IncluirCPF(ds.Tables[0].Rows[0]["Cpf"].ToString(), ds.Tables[0].Rows[0]["Nome"].ToString(), SessionCnnHISTORICO);
                                            Control nome = FindControlRecursive(this.Page, "txt_noParticipante");
                                            if (nome != null)
                                            {
                                                if ((nome as TextBox).Text == "")
                                                {
                                                    (nome as TextBox).Text = ds.Tables[0].Rows[0]["Nome"].ToString();
                                                    (nome as TextBox).Focus();
                                                }
                                            }
                                            Control nomeetiqueta = FindControlRecursive(this.Page, "txt_noEtiqueta");
                                            if (nomeetiqueta != null)
                                            {
                                                if ((nomeetiqueta as TextBox).Text == "")
                                                {
                                                    (nomeetiqueta as TextBox).Text = NomeReduzido(ds.Tables[0].Rows[0]["Nome"].ToString());
                                                    (nomeetiqueta as TextBox).Focus();
                                                }
                                            }
                                            Geral.DecrementarPesquisaCPFReceita(SessionCnn);
                                            //Server.Transfer("frmCadastroAuto.aspx?nome=" + ds.Tables[0].Rows[0]["Nome"].ToString() + "&cpf=" + ds.Tables[0].Rows[0]["Cpf"].ToString(), true);
                                            //Response.Write("<script>window.open('frmCadastroAuto.aspx?nome=" + ds.Tables[0].Rows[0]["Nome"].ToString() + "&cpf=" + ds.Tables[0].Rows[0]["Cpf"].ToString() + "','_self');</script>"); //lblMsg.Text = ds.Tables[0].Rows[0]["Nome"].ToString();
                                        }
                                    }
                                }
                            }
                            //else if (tmpSaldoPesqCPF == 0)
                            //{
                            //    Server.Transfer("frmCadastroAuto.aspx?nome=&cpf=" + txtCPF.Text.Replace(".", "").Replace("-", ""), true);
                            //}
                            //else
                            //{
                            //    lblMsg.Text = "Não foi possível localizar os dados do CPF informado!<br/>Favor entrar em contato com a coordenação do evento.";
                            //}
                        }
                    }


                }
            }
            #endregion

            else
            {
                Control CPF = FindControlRecursive(this.Page, "txt_nuCPFCNPJ");
                if (CPF != null)
                {

                    string cpftemp = (sender as TextBox).Text.Replace(".", "").Replace("-", "").Trim();

                    string tmpCPF = ValidarCPF("", SessionEvento.CdEvento, cpftemp, SessionCateg, SessionCnn);
                    if (tmpCPF != "")
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = tmpCPF;
                        return;
                    }
                }
            }
        }
        #region NTU2016
        if (SessionEvento.CdEvento == "005503")
        {
            if ((sender as TextBox).ID == "txt_dsCampoExtraPreCad")
            {
                Control tmpCdCategoria = FindControlRecursive(this.Page, "txt_cdCategoria");

                if ((tmpCdCategoria != null) && ((tmpCdCategoria as DropDownList).SelectedValue.ToString() != ""))
                {

                    CategoriaCad oCategoriaCad = new CategoriaCad();
                    Categoria oCategoria = oCategoriaCad.Pesquisar(SessionEvento.CdEvento, (tmpCdCategoria as DropDownList).SelectedValue.ToString(), SessionCnn);
                    if ((oCategoria != null) && (oCategoria.FlVerificarPreCadastro))
                    {
                        string tmpCampoExtra = (sender as TextBox).Text;
                        ParticipanteCad oParticipanteCad = new ParticipanteCad();
                        ParticipantePreCadastro oParticipantePreCadastro = oParticipanteCad.PesquisarPreCadastro(SessionEvento.CdEvento, oCategoria.CdCategoria, "", "", "", tmpCampoExtra, SessionCnn);


                        if (oParticipantePreCadastro == null) //((DTPre == null) || (DTPre.Rows.Count <= 0))
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = oCategoria.DsMensagemPreCadastro;// "Não foi encontrado nenhum registro de ex-aluno para o CPF informado!";
                            return;
                        }
                        else
                        {
                            Control dsRazaoSocial = FindControlRecursive(this.Page, "txt_noInstituicao");
                            if (dsRazaoSocial != null)
                            {
                                (dsRazaoSocial as TextBox).Text = oParticipantePreCadastro.NoParticipantePrecadastro;
                            }
                        }

                    }
                }
            }
        }
        #endregion


        #region rehuna
        if (SessionEvento.CdEvento == "008501")
        {
            Control Categoria = FindControlRecursive(this.Page, "txt_cdCategoria");
            string cdCateg = (Categoria as DropDownList).SelectedValue;

            if (((cdCateg == "00850108") || (cdCateg == "00850115")) && ((sender as TextBox).ID == "txt_dsAuxiliar1") && ((sender as TextBox).Text.Trim() != ""))
            {
                (sender as TextBox).Text = (sender as TextBox).Text.Trim().PadLeft(9, '0');
                Participante tmpPart = null;

                if (SessionIdioma == "PTBR") 
                    tmpPart = oParticipanteCad.Pesquisar(SessionEvento.CdEvento, "nuCPFCNPJ", (sender as TextBox).Text.Trim().Replace(".","").Replace("-",""), SessionCnn);
                else
                    tmpPart = oParticipanteCad.Pesquisar(SessionEvento.CdEvento, "dsEmail", (sender as TextBox).Text.Trim(), SessionCnn);

                if ((tmpPart == null) || ((tmpPart != null) && ((tmpPart.CdCategoria == "00850108") || (tmpPart.CdCategoria == "00850115"))))
                {
                    lblMsg.Text = "Nenhum participante principal encontrado para o número informado.";
                    if (SessionIdioma == "ENUS")
                        lblMsg.Text = "Main participant not found for informed e-mail.";


                    if (SessionIdioma == "ESP")
                        lblMsg.Text = "Actor principal no encontrado para el e-mail informado.";

                    lblMsg.Visible = true;

                }
            }
        }
        #endregion
    }
    /*
    protected string ValidarCPF(string prmCdParticipante, string prmCdEvento, string prmCPF, string prmCdCategoria, SqlConnection prmCnn)
    {
        if ((!oClsFuncoes.CPFCNPJValidar(prmCPF)))
        {
            CategoriaCad oCategoriaCad = new CategoriaCad();
            Categoria oCategoria = oCategoriaCad.Pesquisar(prmCdEvento, prmCdCategoria, prmCnn);
            if (((SessionEvento != null) && (SessionEvento.FlLiberarCPFCoringa)))// || (!oCategoria.FlCPFCNPJObrigatorio))
                return "";

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
                CategoriaCad oCategoriaCad = new CategoriaCad();
                Categoria oCategoria = oCategoriaCad.Pesquisar(prmCdEvento, prmCdCategoria, prmCnn);
                if (((SessionEvento != null) && (SessionEvento.FlLiberarCPFCoringa)) || ((oCategoria != null) && ((!oCategoria.FlCPFCNPJObrigatorio) || (oCategoria.FlLiberarCPFCNPJCoringa))))
                    return "";
                return "CPF Inválido!";
            }
            else
            {
                return oParticipanteCad.verificarDuplicidadeCPF(prmCdParticipante, prmCdEvento, tmpCPF, prmCdCategoria, prmCnn);
            }

        }

    }
    */
    protected void txtdrpl_TextChanged(object sender, EventArgs e)
    {
        //if (((sender as DropDownList).ID == "txt_dsUF") && (sender as DropDownList).Text == "DF")
        //    lblMsg.Text = "teste";

        #region FJ - EXPOEPI
        //if (SessionEvento.CdEvento == "003003")
        //{
        //    if (((sender as DropDownList).ID == "txt_cdCategoria") &&
        //        ((sender as DropDownList).Text != "00300316") &&
        //        ((sender as DropDownList).Text != "00300308"))
        //    {
        //        Control dsSubcateg = FindControlRecursive(this.Page, "txt_dsAuxiliar14");
        //        if (dsSubcateg != null)
        //        {

        //            (dsSubcateg as DropDownList).Text = "";// = "teste2";
        //            (dsSubcateg as DropDownList).Enabled = false;// = "teste2";
        //        }

        //        Control dsChave = FindControlRecursive(this.Page, "txt_dsCampoExtraPreCad");
        //        if (dsChave != null)
        //        {

        //            (dsChave as TextBox).Text = "";// = "teste2";
        //            (dsChave as TextBox).Enabled = false;// = "teste2";
        //        }
        //    }
        //    else
        //    {
        //        Control dsSubcateg = FindControlRecursive(this.Page, "txt_dsAuxiliar14");
        //        if (dsSubcateg != null)
        //        {

        //            //(dsSubcateg as DropDownList).Text = "";// = "teste2";
        //            (dsSubcateg as DropDownList).Enabled = true;// = "teste2";
        //        }

        //        Control dsChave = FindControlRecursive(this.Page, "txt_dsCampoExtraPreCad");
        //        if (dsChave != null)
        //        {

        //            //(dsChave as TextBox).Text = "";// = "teste2";
        //            (dsChave as TextBox).Enabled = true;// = "teste2";
        //        }
        //    }

        //}
        #endregion

        #region CONSAD       
        
        //if (SessionEvento.CdCliente == "0003")
        //{
        //    if ((sender as DropDownList).ID == "txt_cdCategoria")
        //    {
        //        if (((sender as DropDownList).Text != "00030406") && 
        //            ((sender as DropDownList).Text != "00030506") && ((sender as DropDownList).Text != "00030512")  &&
        //            ((sender as DropDownList).Text != "00030606") && ((sender as DropDownList).Text != "00030612") &&
        //            ((sender as DropDownList).Text != "00030706") && ((sender as DropDownList).Text != "00030712"))
        //        {

        //            Control dsChave = FindControlRecursive(this.Page, "txt_dsCampoExtraPreCad");
        //            if (dsChave != null)
        //            {
        //                (dsChave as TextBox).Text = "";
        //                (dsChave as TextBox).Enabled = false;
        //            }

        //            Control dsLin = FindControlRecursive(this.Page, "tblinha_dsCampoExtraPreCad");
        //            if (dsLin != null)
        //            {
        //                (dsLin as TableRow).Visible = false;
        //            }
        //        }
        //        else
        //        {

        //            Control dsChave = FindControlRecursive(this.Page, "txt_dsCampoExtraPreCad");
        //            if (dsChave != null)
        //            {
        //                (dsChave as TextBox).Enabled = true;
        //            }

        //            Control dsLin = FindControlRecursive(this.Page, "tblinha_dsCampoExtraPreCad");
        //            if (dsLin != null)
        //            {
        //                (dsLin as TableRow).Visible = true;
        //            }
        //        }

        //        if (((sender as DropDownList).Text == "00030506") || ((sender as DropDownList).Text == "00030512") ||
        //            ((sender as DropDownList).Text == "00030606") || ((sender as DropDownList).Text == "00030612") ||
        //            ((sender as DropDownList).Text == "00030706") || ((sender as DropDownList).Text == "00030712"))
        //        {

        //            Control dsChave = FindControlRecursive(this.Page, "txt_dsAuxiliar2");
        //            if (dsChave != null)
        //            {
        //                (dsChave as DropDownList).Text = (((sender as DropDownList).Text == "00030506") || ((sender as DropDownList).Text == "00030606") || ((sender as DropDownList).Text == "00030706") ? "" : "EMPENHO");
        //                (dsChave as DropDownList).Enabled = false;
        //            }

        //            Control dsLin = FindControlRecursive(this.Page, "tblinha_dsAuxiliar2");
        //            if (dsLin != null)
        //            {
        //                (dsLin as TableRow).Visible = false;
        //            }
        //        }
        //        else
        //        {

        //            Control dsChave = FindControlRecursive(this.Page, "txt_dsAuxiliar2");
        //            if (dsChave != null)
        //            {
        //                (dsChave as DropDownList).Enabled = true;
        //            }

        //            Control dsLin = FindControlRecursive(this.Page, "tblinha_dsAuxiliar2");
        //            if (dsLin != null)
        //            {
        //                (dsLin as TableRow).Visible = true;
        //            }
        //        }
        //    }

        //    if ((sender as DropDownList).ID == "txt_dsAuxiliar3")
        //    {

        //        if (((sender as DropDownList).Text == "OUTROS") || ((sender as DropDownList).Text == "NÃO SE APLICA"))
        //        {

        //            Control txtdsAuxiliar1 = FindControlRecursive(this.Page, "txt_dsAuxiliar1");
        //            if (txtdsAuxiliar1 != null)
        //            {
        //                (txtdsAuxiliar1 as DropDownList).Text = "";
        //            }
                    
        //            //Control lbldsAuxiliar1 = FindControlRecursive(this.Page, "lbl_dsAuxiliar1");
        //            //if (lbldsAuxiliar1 != null)
        //            //{
        //            //    (lbldsAuxiliar1 as Label).Visible = false;
        //            //}

        //            Control coldsAuxiliar1 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar1");
        //            if (coldsAuxiliar1 != null)
        //            {
        //                (coldsAuxiliar1 as TableRow).Visible = false;
        //            }
        //        }
        //        else
        //        {
        //            //Control txtdsAuxiliar1 = FindControlRecursive(this.Page, "txt_dsAuxiliar1");
        //            //if (txtdsAuxiliar1 != null)
        //            //{
        //            //    (txtdsAuxiliar1 as DropDownList).Visible = true;
        //            //}

        //            //Control lbldsAuxiliar1 = FindControlRecursive(this.Page, "lbl_dsAuxiliar1");
        //            //if (lbldsAuxiliar1 != null)
        //            //{
        //            //    (lbldsAuxiliar1 as Label).Visible = true;
        //            //}

        //            Control coldsAuxiliar1 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar1");
        //            if (coldsAuxiliar1 != null)
        //            {
        //                (coldsAuxiliar1 as TableRow).Visible = true;
        //            }
        //        }
        //    }

        //}
        #endregion

        #region wcit2016
        //if ((SessionEvento.CdCliente == "0077") && (int.Parse(SessionEvento.CdEvento) == 7701))
        //{
        //    if ((sender as DropDownList).ID == "txt_noAreaAtuacao") 
        //    {
        //        Control dsCampo1 = FindControlRecursive(this.Page, "txt_dsAuxiliar3");//complemento da área de atuacao
        //        Control linha1 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar3");

        //        if (((sender as DropDownList).Text.ToUpper() == "OUTROS - ESPECIFICAR") ||
        //            ((sender as DropDownList).Text.ToUpper() == "OTHER - SPECIFY"))
        //        {
        //            if (linha1 != null)
        //            {
        //                (linha1 as TableRow).Visible = true;
        //            }
        //        }
        //        else
        //        {
        //            if (linha1 != null)
        //            {
        //                (dsCampo1 as TextBox).Text = "";
        //                (linha1 as TableRow).Visible = false;
        //            }
        //        }
        //    }

        //    if ((sender as DropDownList).ID == "txt_noCargo")
        //    {
        //        Control dsCampo2 = FindControlRecursive(this.Page, "txt_dsAuxiliar5");//complemento da área de atuacao
        //        Control linha2 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar5");

        //        if (((sender as DropDownList).Text.ToUpper() == "OUTROS - ESPECIFICAR") ||
        //            ((sender as DropDownList).Text.ToUpper() == "OTHER - SPECIFY"))
        //        {
        //            if (linha2 != null)
        //            {
        //                (linha2 as TableRow).Visible = true;
        //            }
        //        }
        //        else
        //        {
        //            if (linha2 != null)
        //            {
        //                (dsCampo2 as TextBox).Text = "";
        //                (linha2 as TableRow).Visible = false;
        //            }
        //        }
        //    }
        //}
        #endregion


        #region Conasems 2013
        //if ((SessionEvento.CdCliente == "0013") && (int.Parse(SessionEvento.CdEvento) == 1303))
        //{
           

        //    if ((sender as DropDownList).ID == "txt_noAreaAtuacao") //SE É SECRETÁRIO MUNICIPAL SAUDE
        //    {
        //        Control dsCampo1 = FindControlRecursive(this.Page, "txt_dsAuxiliar11");//atualmente trabalha onde
        //        Control linha1 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar11");
        //        Control dsCampo2 = FindControlRecursive(this.Page, "txt_noCargo");//cargo
        //        Control linha2 = FindControlRecursive(this.Page, "tblinha_noCargo");
        //        Control dsCampo3 = FindControlRecursive(this.Page, "txt_dsAuxiliar16");//instituição de origem
        //        Control linha3 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar16");
        //        Control dsCampo4 = FindControlRecursive(this.Page, "txt_dsAuxiliar13");//Atuação no cosems
        //        Control linha4 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar13");

        //        Control dsCampo5 = FindControlRecursive(this.Page, "cascaDD_dsAuxiliar8");//UF CARGO
        //        Control linha5 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar8");
        //        Control dsCampo6 = FindControlRecursive(this.Page, "cascaDD_dsAuxiliar9");//MUNICIPIO CARGO
        //        Control linha6 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar9");

        //        Control dsCampo41 = FindControlRecursive(this.Page, "txt_dsAuxiliar14");//É a primeira vez...
        //        Control linha41 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar14");
        //        Control dsCampo42 = FindControlRecursive(this.Page, "txt_dsAuxiliar17");//Tempo de experiência...
        //        Control linha42 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar17");

        //        Control dsCampo43 = FindControlRecursive(this.Page, "txt_dsAuxiliar15");//uso da internet...
        //        Control linha43 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar15");

        //        if ((sender as DropDownList).Text.ToUpper() == "SIM")
        //        {
        //            if (dsCampo1 != null)
        //            {
        //                (dsCampo1 as DropDownList).Text = "MUNICÍPIO";
        //                (dsCampo1 as DropDownList).Enabled = false;
        //            }
        //            if (dsCampo2 != null)
        //            {
        //                (linha2 as TableRow).Visible = true;
        //                (linha5 as TableRow).Visible = true;
        //                (linha6 as TableRow).Visible = true;
        //                (dsCampo2 as DropDownList).Items.Clear();
        //                (dsCampo2 as DropDownList).Items.Add("SECRETÁRIO MUNICIPAL DE SAÚDE");
        //                (dsCampo2 as DropDownList).Enabled = false;
        //            }
        //            if (dsCampo3 != null)
        //            {
        //                (dsCampo3 as DropDownList).Text = "";
        //                (linha3 as TableRow).Visible = false;
        //            }
        //            if (dsCampo4 != null)
        //            {
        //                (dsCampo4 as DropDownList).Text = "";
        //                (linha4 as TableRow).Visible = true;
        //            }
        //            if (linha41 != null)
        //            {
        //                (linha41 as TableRow).Visible = true;
        //            }
        //            if (linha42 != null)
        //            {
        //                (linha42 as TableRow).Visible = true;
        //            }
        //            if (linha43 != null)
        //            {
        //                (linha43 as TableRow).Visible = true;
        //            }
        //        }
        //        else
        //        {
        //            if (dsCampo1 != null)
        //            {
        //                (dsCampo1 as DropDownList).Text = "";
        //                (dsCampo1 as DropDownList).Enabled = true;
        //            }
        //            if (dsCampo2 != null)
        //            {
        //                (dsCampo2 as DropDownList).Items.Clear();
        //                (dsCampo2 as DropDownList).Enabled = true;
        //            }
        //            if (dsCampo3 != null)
        //            {
        //                (dsCampo3 as DropDownList).Text = "";
        //                (linha3 as TableRow).Visible = false;
        //            }
        //            if (dsCampo4 != null)
        //            {
        //                (dsCampo4 as DropDownList).Text = "";
        //                (linha4 as TableRow).Visible = false;
        //            }
        //            if (linha41 != null)
        //            {
        //                (dsCampo41 as DropDownList).Text = "";
        //                (linha41 as TableRow).Visible = false;
        //            }
        //            if (linha42 != null)
        //            {
        //                (dsCampo42 as DropDownList).Text = "";
        //                (linha42 as TableRow).Visible = false;
        //            }
        //            if (linha43 != null)
        //            {
        //                (dsCampo43 as DropDownList).Text = "";
        //                (linha43 as TableRow).Visible = false;
        //            }
        //        }

        //    }

        //    if ((sender as DropDownList).ID == "txt_dsAuxiliar11")
        //    {
        //        Control dsCampo2 = FindControlRecursive(this.Page, "txt_noCargo");//cargo                
        //        Control linha2 = FindControlRecursive(this.Page, "tblinha_noCargo");

        //        Control dsCampo5 = FindControlRecursive(this.Page, "cascaDD_dsAuxiliar8");//UF CARGO
        //        Control linha5 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar8");
        //        Control dsCampo6 = FindControlRecursive(this.Page, "cascaDD_dsAuxiliar9");//MUNICIPIO CARGO                
        //        Control linha6 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar9");

        //        Control dsCampo3 = FindControlRecursive(this.Page, "txt_dsAuxiliar16");//instituicao de origem                
        //        Control linha3 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar16");

        //        if ((sender as DropDownList).Text == "MUNICÍPIO")
        //        {
        //            if (dsCampo2 != null)
        //            {
        //                (dsCampo2 as DropDownList).Items.Clear();
        //                (dsCampo2 as DropDownList).Items.Add("");
        //                (dsCampo2 as DropDownList).Items.Add("PREFEITO (A) MUNICIPAL");
        //                (dsCampo2 as DropDownList).Items.Add("VICE-PREFEITO (A) MUNICIPAL");
        //                (dsCampo2 as DropDownList).Items.Add("CONSELHEIRO MUNICIPAL DE SAÚDE");
        //                (dsCampo2 as DropDownList).Items.Add("DIRETORES/COORDENADORES/GERENTES DE PROGRAMAS OU UNIDADES");
        //                (dsCampo2 as DropDownList).Items.Add("PROFISSIONAIS DE SAÚDE");
        //                (dsCampo2 as DropDownList).Items.Add("PROFISSIONAIS ADMINISTRATIVOS");
        //                (dsCampo2 as DropDownList).Items.Add("OUTROS");
        //                (linha2 as TableRow).Visible = true;
        //                (linha5 as TableRow).Visible = true;//uf
        //                (linha6 as TableRow).Visible = true;//municipio

        //                (linha3 as TableRow).Visible = false;//instituicao de origem   
        //                (dsCampo3 as DropDownList).Text = "";

        //                (dsCampo5 as CascadingDropDown).SelectedValue = "";
        //                (dsCampo6 as CascadingDropDown).SelectedValue = "";
        //            }
        //        }
        //        else if ((sender as DropDownList).Text == "ESTADO")
        //        {
        //            if (dsCampo2 != null)
        //            {
        //                (dsCampo2 as DropDownList).Items.Clear();
        //                (dsCampo2 as DropDownList).Items.Add("");
        //                (dsCampo2 as DropDownList).Items.Add("GOVERNADOR");
        //                (dsCampo2 as DropDownList).Items.Add("VICE-GOVERNADOR");
        //                (dsCampo2 as DropDownList).Items.Add("SECRETÁRIO (A) DE ESTADO DA SAÚDE");
        //                (dsCampo2 as DropDownList).Items.Add("CONSELHEIRO ESTADUAL DE SAÚDE");
        //                (dsCampo2 as DropDownList).Items.Add("DIRETORES/COORDENADORES/GERENTES DE PROGRAMAS OU UNIDADES");
        //                (dsCampo2 as DropDownList).Items.Add("PROFISSIONAIS DE SAÚDE");
        //                (dsCampo2 as DropDownList).Items.Add("PROFISSIONAIS ADMINISTRATIVOS");
        //                (dsCampo2 as DropDownList).Items.Add("OUTROS");
                        
        //                (linha2 as TableRow).Visible = true;
        //                (linha5 as TableRow).Visible = true;//uf
        //                (dsCampo5 as CascadingDropDown).SelectedValue = "";
        //                (linha6 as TableRow).Visible = false;//municipio
        //                (dsCampo6 as CascadingDropDown).SelectedValue = "";

        //                (linha3 as TableRow).Visible = false;//instituicao de origem   
        //                (dsCampo3 as DropDownList).Text = "";
        //            }
        //        }
        //        else if ((sender as DropDownList).Text == "GOVERNO FEDERAL")
        //        {
        //            if (dsCampo2 != null)
        //            {
        //                (dsCampo2 as DropDownList).Items.Clear();
        //                (dsCampo2 as DropDownList).Items.Add("");
        //                (dsCampo2 as DropDownList).Items.Add("SECRETÁRIO/DIRETOR/COORDENADOR/GERENTES DE PROGRAMAS OU UNIDADES");
        //                (dsCampo2 as DropDownList).Items.Add("TÉCNICOS ESPECIALIZADOS");
        //                (dsCampo2 as DropDownList).Items.Add("CONSULTORES");
        //                (dsCampo2 as DropDownList).Items.Add("APOIADORES");
        //                (dsCampo2 as DropDownList).Items.Add("PROFISSIONAIS DE SAÚDE");
        //                (dsCampo2 as DropDownList).Items.Add("PROFISSIONAIS ADMINISTRATIVOS");
        //                (dsCampo2 as DropDownList).Items.Add("OUTROS");
                        
        //                (linha2 as TableRow).Visible = true;

        //                (linha5 as TableRow).Visible = false;//uf
        //                (dsCampo5 as CascadingDropDown).SelectedValue = "";
        //                (linha6 as TableRow).Visible = false;//municipio
        //                (dsCampo6 as CascadingDropDown).SelectedValue = "";

        //                (linha3 as TableRow).Visible = true;//instituicao de origem   
        //            }
        //        }
        //        else if ((sender as DropDownList).Text == "PRIVADO")
        //        {
        //            (dsCampo2 as DropDownList).Items.Clear();
        //            (dsCampo2 as DropDownList).Items.Add("");
        //            (dsCampo2 as DropDownList).Items.Add("OUTROS");
                    
        //            (dsCampo2 as DropDownList).Text = "";
        //            (linha2 as TableRow).Visible = false;

        //            (linha3 as TableRow).Visible = false;//instituicao de origem   
        //            (dsCampo3 as DropDownList).Text = "";
                    
        //            (dsCampo5 as CascadingDropDown).SelectedValue = "";                    
        //            (dsCampo6 as CascadingDropDown).SelectedValue = "";

        //            (linha5 as TableRow).Visible = false;//uf
        //            (linha6 as TableRow).Visible = false;//municipio
                    
        //        }


        //    }

            



        //    //if ((sender as DropDownList).ID == "txt_noCargo")
        //    //{
        //    //    Control dsCampo1 = FindControlRecursive(this.Page, "txt_dsAuxiliar2");//cargos (outros)
        //    //    Control dsCampo2 = FindControlRecursive(this.Page, "txt_dsAuxiliar8");//uf (cargo)
        //    //    Control dsCampo3 = FindControlRecursive(this.Page, "txt_dsAuxiliar9");//municipio (cargo)
        //    //    if ((sender as DropDownList).Text.ToUpper() != "OUTROS")
        //    //    {
        //    //        if (dsCampo1 != null)
        //    //        {
        //    //            (dsCampo1 as TextBox).Enabled = false;
        //    //        }
        //    //        if (dsCampo2 != null)
        //    //        {
        //    //            (dsCampo2 as DropDownList).Enabled = true;
        //    //        }
        //    //        if (dsCampo3 != null)
        //    //        {
        //    //            (dsCampo3 as DropDownList).Enabled = true;
        //    //        }
        //    //    }
        //    //    else
        //    //    {
        //    //        if (dsCampo1 != null)
        //    //        {
        //    //            (dsCampo1 as TextBox).Enabled = true;
        //    //        }
        //    //        if (dsCampo2 != null)
        //    //        {
        //    //            (dsCampo2 as DropDownList).Enabled = true;
        //    //        }
        //    //        if (dsCampo3 != null)
        //    //        {
        //    //            (dsCampo3 as DropDownList).Enabled = true;
        //    //        }
        //    //    }
        //    //}

        //    if ((sender as DropDownList).ID == "txt_dsPassagemAerea")
        //    {

        //        Control dsCampo30 = FindControlRecursive(this.Page, "tblinha_dsVooIda");
        //        Control dsCampo31 = FindControlRecursive(this.Page, "tblinha_dsVooLocalizadorIda");

        //        Control dsCampo15 = FindControlRecursive(this.Page, "txt_dsVooIda");
        //        Control dsCampo16 = FindControlRecursive(this.Page, "txt_dsVooLocalizadorIda");

        //        Control dsCampo17 = FindControlRecursive(this.Page, "txt_dsVooVolta");
        //        Control dsCampo18 = FindControlRecursive(this.Page, "txt_dsVooLocalizadorVolta");

        //        Control dsCampo1 = FindControlRecursive(this.Page, "txt_dsVooCiaAereaIda");
        //        Control dsCampo2 = FindControlRecursive(this.Page, "txt_dsVooOrigemIda");
        //        Control dsCampo3 = FindControlRecursive(this.Page, "txt_dsVooDestinoIda");
        //        Control dsCampo4 = FindControlRecursive(this.Page, "txt_dsVooDataSaidaIda");
        //        Control dsCampo5 = FindControlRecursive(this.Page, "txt_dsVooHoraSaidaIda");
        //        Control dsCampo6 = FindControlRecursive(this.Page, "txt_dsVooDataChegadaIda");
        //        Control dsCampo7 = FindControlRecursive(this.Page, "txt_dsVooHoraChegadaIda");
        //        Control dsCampo8 = FindControlRecursive(this.Page, "txt_dsVooCiaAereaVolta");
        //        Control dsCampo9 = FindControlRecursive(this.Page, "txt_dsVooOrigemVolta");
        //        Control dsCampo10 = FindControlRecursive(this.Page, "txt_dsVooDestinoVolta");
        //        Control dsCampo11 = FindControlRecursive(this.Page, "txt_dsVooDataSaidaVolta");
        //        Control dsCampo12 = FindControlRecursive(this.Page, "txt_dsVooHoraSaidaVolta");
        //        Control dsCampo13 = FindControlRecursive(this.Page, "txt_dsVooDataChegadaVolta");
        //        Control dsCampo14 = FindControlRecursive(this.Page, "txt_dsVooHoraChegadaVolta");

        //        if ((sender as DropDownList).Text.ToUpper() == "SIM")
        //        {
        //            //if (dsCampo30 != null)
        //            //    (dsCampo30 as TableRow).Visible = true;
        //            //if (dsCampo31 != null)
        //            //    (dsCampo31 as TableRow).Visible = true;

        //            if (((dsCampo15 != null) && ((dsCampo15 as TextBox).Text == "")) &&
        //                 ((dsCampo16 != null) && ((dsCampo16 as TextBox).Text == "")))
        //            {
        //                if (dsCampo1 != null)
        //                {
        //                    (dsCampo1 as TextBox).Enabled = true;
        //                }
        //                if (dsCampo2 != null)
        //                {
        //                    (dsCampo2 as DropDownList).Enabled = true;
        //                }
        //                if (dsCampo3 != null)
        //                {
        //                    (dsCampo3 as DropDownList).Enabled = true;
        //                }
        //                if (dsCampo4 != null)
        //                {
        //                    (dsCampo4 as TextBox).Enabled = true;
        //                }
        //                if (dsCampo5 != null)
        //                {
        //                    (dsCampo5 as TextBox).Enabled = true;
        //                }
        //                if (dsCampo6 != null)
        //                {
        //                    (dsCampo6 as TextBox).Enabled = true;
        //                }
        //                if (dsCampo7 != null)
        //                {
        //                    (dsCampo7 as TextBox).Enabled = true;
        //                }
        //            }

        //            if (((dsCampo17 != null) && ((dsCampo17 as TextBox).Text == "")) &&
        //                 ((dsCampo18 != null) && ((dsCampo18 as TextBox).Text == "")))
        //            {
        //                if (dsCampo8 != null)
        //                {
        //                    (dsCampo8 as TextBox).Enabled = true;
        //                }
        //                if (dsCampo9 != null)
        //                {
        //                    (dsCampo9 as DropDownList).Enabled = true;
        //                }
        //                if (dsCampo10 != null)
        //                {
        //                    (dsCampo10 as DropDownList).Enabled = true;
        //                }
        //                if (dsCampo11 != null)
        //                {
        //                    (dsCampo11 as TextBox).Enabled = true;
        //                }
        //                if (dsCampo12 != null)
        //                {
        //                    (dsCampo12 as TextBox).Enabled = true;
        //                }
        //                if (dsCampo13 != null)
        //                {
        //                    (dsCampo13 as TextBox).Enabled = true;
        //                }
        //                if (dsCampo14 != null)
        //                {
        //                    (dsCampo14 as TextBox).Enabled = true;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            //if (dsCampo30 != null)
        //            //    (dsCampo30 as TableRow).Visible = false;
        //            //if (dsCampo31 != null)
        //            //    (dsCampo31 as TableRow).Visible = false;

        //            if (dsCampo1 != null)
        //            {
        //                (dsCampo1 as TextBox).Enabled = false;
        //            }
        //            if (dsCampo2 != null)
        //            {
        //                (dsCampo2 as DropDownList).Enabled = false;
        //            }
        //            if (dsCampo3 != null)
        //            {
        //                (dsCampo3 as DropDownList).Enabled = false;
        //            }
        //            if (dsCampo4 != null)
        //            {
        //                (dsCampo4 as TextBox).Enabled = false;
        //            }
        //            if (dsCampo5 != null)
        //            {
        //                (dsCampo5 as TextBox).Enabled = false;
        //            }
        //            if (dsCampo6 != null)
        //            {
        //                (dsCampo6 as TextBox).Enabled = false;
        //            }
        //            if (dsCampo7 != null)
        //            {
        //                (dsCampo7 as TextBox).Enabled = false;
        //            }
        //            if (dsCampo8 != null)
        //            {
        //                (dsCampo8 as TextBox).Enabled = false;
        //            }
        //            if (dsCampo9 != null)
        //            {
        //                (dsCampo9 as DropDownList).Enabled = false;
        //            }
        //            if (dsCampo10 != null)
        //            {
        //                (dsCampo10 as DropDownList).Enabled = false;
        //            }
        //            if (dsCampo11 != null)
        //            {
        //                (dsCampo11 as TextBox).Enabled = false;
        //            }
        //            if (dsCampo12 != null)
        //            {
        //                (dsCampo12 as TextBox).Enabled = false;
        //            }
        //            if (dsCampo13 != null)
        //            {
        //                (dsCampo13 as TextBox).Enabled = false;
        //            }
        //            if (dsCampo14 != null)
        //            {
        //                (dsCampo14 as TextBox).Enabled = false;
        //            }
        //        }
        //    }

            

        //}
               
        #endregion

        #region Conasems 2014
        //if ((SessionEvento.CdCliente == "0013") && (int.Parse(SessionEvento.CdEvento) >= 1304))
        //{


        //    //if ((sender as DropDownList).ID == "txt_noAreaAtuacao") //SE É SECRETÁRIO MUNICIPAL SAUDE
        //    //{
        //    //    Control dsCampo5 = FindControlRecursive(this.Page, "cascaDD_dsAuxiliar8");//UF CARGO
        //    //    Control linha5 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar8");
        //    //    Control dsCampo6 = FindControlRecursive(this.Page, "cascaDD_dsAuxiliar9");//MUNICIPIO CARGO
        //    //    Control linha6 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar9");
                               

        //    //    if ((sender as DropDownList).Text.ToUpper() == "SIM")
        //    //    {
        //    //        (linha5 as TableRow).Visible = true;
        //    //        (linha6 as TableRow).Visible = true;
        //    //    }
        //    //    else
        //    //    {
        //    //        (linha5 as TableRow).Visible = false;
        //    //        (linha6 as TableRow).Visible = false;
        //    //    }

        //    //}

            
        //    if ((sender as DropDownList).ID == "txt_dsPassagemAerea")
        //    {

        //        Control dsCampo30 = FindControlRecursive(this.Page, "tblinha_dsVooIda");
        //        Control dsCampo31 = FindControlRecursive(this.Page, "tblinha_dsVooLocalizadorIda");

        //        Control dsCampo15 = FindControlRecursive(this.Page, "txt_dsVooIda");
        //        Control dsCampo16 = FindControlRecursive(this.Page, "txt_dsVooLocalizadorIda");

        //        Control dsCampo17 = FindControlRecursive(this.Page, "txt_dsVooVolta");
        //        Control dsCampo18 = FindControlRecursive(this.Page, "txt_dsVooLocalizadorVolta");

        //        Control dsCampo1 = FindControlRecursive(this.Page, "txt_dsVooCiaAereaIda");
        //        Control dsCampo2 = FindControlRecursive(this.Page, "txt_dsVooOrigemIda");
        //        Control dsCampo3 = FindControlRecursive(this.Page, "txt_dsVooDestinoIda");
        //        Control dsCampo4 = FindControlRecursive(this.Page, "txt_dsVooDataSaidaIda");
        //        Control dsCampo5 = FindControlRecursive(this.Page, "txt_dsVooHoraSaidaIda");
        //        Control dsCampo6 = FindControlRecursive(this.Page, "txt_dsVooDataChegadaIda");
        //        Control dsCampo7 = FindControlRecursive(this.Page, "txt_dsVooHoraChegadaIda");
        //        Control dsCampo8 = FindControlRecursive(this.Page, "txt_dsVooCiaAereaVolta");
        //        Control dsCampo9 = FindControlRecursive(this.Page, "txt_dsVooOrigemVolta");
        //        Control dsCampo10 = FindControlRecursive(this.Page, "txt_dsVooDestinoVolta");
        //        Control dsCampo11 = FindControlRecursive(this.Page, "txt_dsVooDataSaidaVolta");
        //        Control dsCampo12 = FindControlRecursive(this.Page, "txt_dsVooHoraSaidaVolta");
        //        Control dsCampo13 = FindControlRecursive(this.Page, "txt_dsVooDataChegadaVolta");
        //        Control dsCampo14 = FindControlRecursive(this.Page, "txt_dsVooHoraChegadaVolta");

        //        if ((sender as DropDownList).Text.ToUpper() == "SIM")
        //        {
        //            //if (dsCampo30 != null)
        //            //    (dsCampo30 as TableRow).Visible = true;
        //            //if (dsCampo31 != null)
        //            //    (dsCampo31 as TableRow).Visible = true;

        //            if (((dsCampo15 != null) && ((dsCampo15 as TextBox).Text == "")) &&
        //                 ((dsCampo16 != null) && ((dsCampo16 as TextBox).Text == "")))
        //            {
        //                if (dsCampo1 != null)
        //                {
        //                    (dsCampo1 as TextBox).Enabled = true;
        //                }
        //                if (dsCampo2 != null)
        //                {
        //                    (dsCampo2 as DropDownList).Enabled = true;
        //                }
        //                if (dsCampo3 != null)
        //                {
        //                    (dsCampo3 as DropDownList).Enabled = true;
        //                }
        //                if (dsCampo4 != null)
        //                {
        //                    (dsCampo4 as TextBox).Enabled = true;
        //                }
        //                if (dsCampo5 != null)
        //                {
        //                    (dsCampo5 as TextBox).Enabled = true;
        //                }
        //                if (dsCampo6 != null)
        //                {
        //                    (dsCampo6 as TextBox).Enabled = true;
        //                }
        //                if (dsCampo7 != null)
        //                {
        //                    (dsCampo7 as TextBox).Enabled = true;
        //                }
        //            }

        //            if (((dsCampo17 != null) && ((dsCampo17 as TextBox).Text == "")) &&
        //                 ((dsCampo18 != null) && ((dsCampo18 as TextBox).Text == "")))
        //            {
        //                if (dsCampo8 != null)
        //                {
        //                    (dsCampo8 as TextBox).Enabled = true;
        //                }
        //                if (dsCampo9 != null)
        //                {
        //                    (dsCampo9 as DropDownList).Enabled = true;
        //                }
        //                if (dsCampo10 != null)
        //                {
        //                    (dsCampo10 as DropDownList).Enabled = true;
        //                }
        //                if (dsCampo11 != null)
        //                {
        //                    (dsCampo11 as TextBox).Enabled = true;
        //                }
        //                if (dsCampo12 != null)
        //                {
        //                    (dsCampo12 as TextBox).Enabled = true;
        //                }
        //                if (dsCampo13 != null)
        //                {
        //                    (dsCampo13 as TextBox).Enabled = true;
        //                }
        //                if (dsCampo14 != null)
        //                {
        //                    (dsCampo14 as TextBox).Enabled = true;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            //if (dsCampo30 != null)
        //            //    (dsCampo30 as TableRow).Visible = false;
        //            //if (dsCampo31 != null)
        //            //    (dsCampo31 as TableRow).Visible = false;

        //            if (dsCampo1 != null)
        //            {
        //                (dsCampo1 as TextBox).Enabled = false;
        //            }
        //            if (dsCampo2 != null)
        //            {
        //                (dsCampo2 as DropDownList).Enabled = false;
        //            }
        //            if (dsCampo3 != null)
        //            {
        //                (dsCampo3 as DropDownList).Enabled = false;
        //            }
        //            if (dsCampo4 != null)
        //            {
        //                (dsCampo4 as TextBox).Enabled = false;
        //            }
        //            if (dsCampo5 != null)
        //            {
        //                (dsCampo5 as TextBox).Enabled = false;
        //            }
        //            if (dsCampo6 != null)
        //            {
        //                (dsCampo6 as TextBox).Enabled = false;
        //            }
        //            if (dsCampo7 != null)
        //            {
        //                (dsCampo7 as TextBox).Enabled = false;
        //            }
        //            if (dsCampo8 != null)
        //            {
        //                (dsCampo8 as TextBox).Enabled = false;
        //            }
        //            if (dsCampo9 != null)
        //            {
        //                (dsCampo9 as DropDownList).Enabled = false;
        //            }
        //            if (dsCampo10 != null)
        //            {
        //                (dsCampo10 as DropDownList).Enabled = false;
        //            }
        //            if (dsCampo11 != null)
        //            {
        //                (dsCampo11 as TextBox).Enabled = false;
        //            }
        //            if (dsCampo12 != null)
        //            {
        //                (dsCampo12 as TextBox).Enabled = false;
        //            }
        //            if (dsCampo13 != null)
        //            {
        //                (dsCampo13 as TextBox).Enabled = false;
        //            }
        //            if (dsCampo14 != null)
        //            {
        //                (dsCampo14 as TextBox).Enabled = false;
        //            }
        //        }
        //    }

            

        //}

        #endregion

        #region PRB
        //if (SessionEvento.CdCliente == "0048")
        //{
        //    if ((sender as DropDownList).ID == "txt_dsPassagemAerea")
        //    {
                                                
        //        Control dslCampo31 = FindControlRecursive(this.Page, "tblinha_dsLabelSeparador5");
        //        Control dslCampo15 = FindControlRecursive(this.Page, "tblinha_dsVooIda");
        //        Control dslCampo16 = FindControlRecursive(this.Page, "tblinha_dsVooLocalizadorIda");
        //        Control dslCampo1 = FindControlRecursive(this.Page, "tblinha_dsVooCiaAereaIda");
        //        Control dslCampo2 = FindControlRecursive(this.Page, "tblinha_dsVooOrigemIda");
        //        Control dslCampo3 = FindControlRecursive(this.Page, "tblinha_dsVooDestinoIda");
        //        Control dslCampo4 = FindControlRecursive(this.Page, "tblinha_dsVooDataSaidaIda");
        //        Control dslCampo5 = FindControlRecursive(this.Page, "tblinha_dsVooHoraSaidaIda");
        //        Control dslCampo6 = FindControlRecursive(this.Page, "tblinha_dsVooDataChegadaIda");
        //        Control dslCampo7 = FindControlRecursive(this.Page, "tblinha_dsVooHoraChegadaIda");

        //        Control dslCampo33 = FindControlRecursive(this.Page, "tblinha_dsLabelSeparador6");
        //        Control dslCampo17 = FindControlRecursive(this.Page, "tblinha_dsVooVolta");
        //        Control dslCampo18 = FindControlRecursive(this.Page, "tblinha_dsVooLocalizadorVolta");
        //        Control dslCampo8 = FindControlRecursive(this.Page, "tblinha_dsVooCiaAereaVolta");
        //        Control dslCampo9 = FindControlRecursive(this.Page, "tblinha_dsVooOrigemVolta");
        //        Control dslCampo10 = FindControlRecursive(this.Page, "tblinha_dsVooDestinoVolta");
        //        Control dslCampo11 = FindControlRecursive(this.Page, "tblinha_dsVooDataSaidaVolta");
        //        Control dslCampo12 = FindControlRecursive(this.Page, "tblinha_dsVooHoraSaidaVolta");
        //        Control dslCampo13 = FindControlRecursive(this.Page, "tblinha_dsVooDataChegadaVolta");
        //        Control dslCampo14 = FindControlRecursive(this.Page, "tblinha_dsVooHoraChegadaVolta");


        //        if ((sender as DropDownList).Text.ToUpper() == "AÉREA")
        //        {
        //            if (dslCampo33 != null)
        //                (dslCampo33 as TableRow).Visible = true;
        //            if (dslCampo31 != null)
        //                (dslCampo31 as TableRow).Visible = true;

                    

        //            if (dslCampo15 != null)
        //                (dslCampo15 as TableRow).Visible = true;
        //            if (dslCampo16 != null)
        //                (dslCampo16 as TableRow).Visible = true;

        //            if (dslCampo1 != null)
        //                (dslCampo1 as TableRow).Visible = true;

        //            if (dslCampo2 != null)
        //                (dslCampo2 as TableRow).Visible = true;

        //            if (dslCampo3 != null)
        //                (dslCampo3 as TableRow).Visible = true;
        //            if (dslCampo4 != null)
        //                (dslCampo4 as TableRow).Visible = true;
        //            if (dslCampo5 != null)
        //                (dslCampo5 as TableRow).Visible = true;
        //            if (dslCampo6 != null)
        //                (dslCampo6 as TableRow).Visible = true;
        //            if (dslCampo7 != null)
        //                (dslCampo7 as TableRow).Visible = true;

        //            if (dslCampo17 != null)
        //                (dslCampo17 as TableRow).Visible = true;
        //            if (dslCampo18 != null)
        //                (dslCampo18 as TableRow).Visible = true;

        //            if (dslCampo8 != null)
        //                (dslCampo8 as TableRow).Visible = true;
        //            if (dslCampo9 != null)
        //                (dslCampo9 as TableRow).Visible = true;
        //            if (dslCampo10 != null)
        //                (dslCampo10 as TableRow).Visible = true;
        //            if (dslCampo11 != null)
        //                (dslCampo11 as TableRow).Visible = true;
        //            if (dslCampo12 != null)
        //                (dslCampo12 as TableRow).Visible = true;
        //            if (dslCampo13 != null)
        //                (dslCampo13 as TableRow).Visible = true;
        //            if (dslCampo14 != null)
        //                (dslCampo14 as TableRow).Visible = true;

        //        }
        //        else
        //        {
        //            if (dslCampo31 != null)
        //                (dslCampo31 as TableRow).Visible = false;
        //            if (dslCampo33 != null)
        //                (dslCampo33 as TableRow).Visible = false;
                                        
        //            if (dslCampo15 != null)
        //                (dslCampo15 as TableRow).Visible = false;
        //            if (dslCampo16 != null)
        //                (dslCampo16 as TableRow).Visible = false;

        //            if (dslCampo1 != null)
        //                (dslCampo1 as TableRow).Visible = false;

        //            if (dslCampo2 != null)
        //                (dslCampo2 as TableRow).Visible = false;

        //            if (dslCampo3 != null)
        //                (dslCampo3 as TableRow).Visible = false;
        //            if (dslCampo4 != null)
        //                (dslCampo4 as TableRow).Visible = false;
        //            if (dslCampo5 != null)
        //                (dslCampo5 as TableRow).Visible = false;
        //            if (dslCampo6 != null)
        //                (dslCampo6 as TableRow).Visible = false;
        //            if (dslCampo7 != null)
        //                (dslCampo7 as TableRow).Visible = false;

        //            if (dslCampo17 != null)
        //                (dslCampo17 as TableRow).Visible = false;
        //            if (dslCampo18 != null)
        //                (dslCampo18 as TableRow).Visible = false;

        //            if (dslCampo8 != null)
        //                (dslCampo8 as TableRow).Visible = false;
        //            if (dslCampo9 != null)
        //                (dslCampo9 as TableRow).Visible = false;
        //            if (dslCampo10 != null)
        //                (dslCampo10 as TableRow).Visible = false;
        //            if (dslCampo11 != null)
        //                (dslCampo11 as TableRow).Visible = false;
        //            if (dslCampo12 != null)
        //                (dslCampo12 as TableRow).Visible = false;
        //            if (dslCampo13 != null)
        //                (dslCampo13 as TableRow).Visible = false;
        //            if (dslCampo14 != null)
        //                (dslCampo14 as TableRow).Visible = false;
        //        }
        //    }
        //}
        #endregion

        #region CISPOD
        //if (SessionEvento.CdEvento == "004401")
        //{            
            


        //    if ((sender as DropDownList).ID == "txt_dsAuxiliar1") //Inscrito em Conselho profissional
        //    {
        //        Control dsCampo1 = FindControlRecursive(this.Page, "txt_dsAuxiliar1");//Inscrito em Conselho profissional
        //        Control linha1 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar1");
        //        Control dsCampo2 = FindControlRecursive(this.Page, "txt_dsAuxiliar2");//Qual Conselho?
        //        Control linha2 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar2");

        //        if (((sender as DropDownList).Text.ToUpper() == "SIM") || ((sender as DropDownList).Text.ToUpper() == "YES"))
        //        {
        //            if (dsCampo2 != null)
        //            {
        //                (linha2 as TableRow).Visible = true;//Qual Conselho?
        //            }
        //        }
        //        else
        //        {
        //            if (dsCampo2 != null)
        //            {
        //                (linha2 as TableRow).Visible = false;//Qual Conselho?
        //            }
        //        }
        //    }
            
        //    if ((sender as DropDownList).ID == "txt_noAreaAtuacao") //Area Atuacao
        //    {
        //        Control dsCampo3 = FindControlRecursive(this.Page, "txt_noAreaAtuacao");//Area Atuacao
        //        Control linha3 = FindControlRecursive(this.Page, "tblinha_noAreaAtuacao");
        //        Control dsCampo4 = FindControlRecursive(this.Page, "txt_dsAuxiliar3");//Qual Area Atuacao
        //        Control linha4 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar3");

        //        if (((sender as DropDownList).Text.ToUpper() == "OUTRAS") || ((sender as DropDownList).Text.ToUpper() == "OTHER"))
        //        {
        //            if (dsCampo4 != null)
        //            {
        //                (linha4 as TableRow).Visible = true;//Qual Area Atuacao
        //            }
        //        }
        //        else
        //        {
        //            if (dsCampo4 != null)
        //            {
        //                (linha4 as TableRow).Visible = false;//Qual Area Atuacao
        //            }
        //        }
        //    }

        //    if ((sender as DropDownList).ID == "txt_dsAuxiliar4") //É uma pessoa com deficiência
        //    {
        //        Control dsCampo5 = FindControlRecursive(this.Page, "txt_dsAuxiliar4");//É uma pessoa com deficiência
        //        Control linha5 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar4");
        //        Control dsCampo6 = FindControlRecursive(this.Page, "txt_dsAuxiliar5");//Qual deficiência
        //        Control linha6 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar5");

        //        if (((sender as DropDownList).Text.ToUpper() == "SIM") || ((sender as DropDownList).Text.ToUpper() == "YES"))
        //        {
        //            if (dsCampo6 != null)
        //            {
        //                (linha6 as TableRow).Visible = true;//Qual deficiência
        //            }
        //        }
        //        else
        //        {
        //            if (dsCampo6 != null)
        //            {
        //                (linha6 as TableRow).Visible = false;//Qual deficiência
        //            }
        //        }
        //    }

        //    if ((sender as DropDownList).ID == "txt_dsAuxiliar9") //representa alguma entidade
        //    {
        //        Control dsCampo5 = FindControlRecursive(this.Page, "txt_dsAuxiliar10");//Entidade que representa
        //        Control linha5 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar10");

        //        if (((sender as DropDownList).Text.ToUpper() == "SIM") || ((sender as DropDownList).Text.ToUpper() == "YES"))
        //        {
        //            if (dsCampo5 != null)
        //            {
        //                (linha5 as TableRow).Visible = true;//Entidade que representa
        //            }
        //        }
        //        else
        //        {
        //            if (dsCampo5 != null)
        //            {
        //                (linha5 as TableRow).Visible = false;//Entidade que representa
        //            }
        //        }
        //    }
        //}

        #endregion

        #region MDA - CONDRAF
        //if (SessionEvento.CdEvento == "004501")
        //{

        //    if ((sender as DropDownList).ID == "txt_dsAuxiliar3") //Representatividade
        //    {
        //        Control dsCampo1 = FindControlRecursive(this.Page, "txt_dsAuxiliar4");//Sociedade Civil
        //        Control linha1 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar4");
        //        Control dsCampo2 = FindControlRecursive(this.Page, "txt_dsAuxiliar5");//Sociedade Civil - Outros
        //        Control linha2 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar5");

        //        Control dsCampo3 = FindControlRecursive(this.Page, "txt_dsAuxiliar6");//GOVERNO
        //        Control linha3 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar6");
        //        Control dsCampo4 = FindControlRecursive(this.Page, "txt_dsAuxiliar7");//GOVERNO - ORGAO
        //        Control linha4 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar7");

        //        if ((sender as DropDownList).Text.ToUpper() == "SOCIEDADE CIVIL")
        //        {
        //            if (dsCampo1 != null)
        //            {
        //                (linha1 as TableRow).Visible = true;//Sociedade Civil
        //                if ((dsCampo1 as DropDownList).Text.ToUpper() == "OUTRO")
        //                    (linha2 as TableRow).Visible = true;//Sociedade Civil - Outros
        //                else
        //                    (linha2 as TableRow).Visible = false;//Sociedade Civil - Outros

        //                (dsCampo3 as DropDownList).Text = "";
        //                (linha3 as TableRow).Visible = false;//GOVERNO

        //                (dsCampo4 as TextBox).Text = "";
        //                (linha4 as TableRow).Visible = false;//GOVERNO - ORGAO
        //            }
        //        }
        //        else if ((sender as DropDownList).Text.ToUpper() == "SETOR PÚBLICO")
        //        {
        //            if (dsCampo2 != null)
        //            {
        //                (dsCampo1 as DropDownList).Text = "";
        //                (linha1 as TableRow).Visible = false;//Sociedade Civil

        //                (dsCampo2 as TextBox).Text = "";
        //                (linha2 as TableRow).Visible = false;//Sociedade Civil - Outros

        //                (linha3 as TableRow).Visible = true;//GOVERNO

        //                (linha4 as TableRow).Visible = true;//GOVERNO - ORGAO
        //            }
        //        }
        //        else 
        //        {
        //            (dsCampo1 as DropDownList).Text = "";
        //            (linha1 as TableRow).Visible = false;//Sociedade Civil

        //            (dsCampo2 as TextBox).Text = "";
        //            (linha2 as TableRow).Visible = false;//Sociedade Civil - Outros

        //            (dsCampo3 as DropDownList).Text = "";
        //            (linha3 as TableRow).Visible = false;//GOVERNO

        //            (dsCampo4 as TextBox).Text = "";
        //            (linha4 as TableRow).Visible = false;//GOVERNO - ORGAO
                    
        //        }
        //    }


        //    if ((sender as DropDownList).ID == "txt_dsAuxiliar4") //Segmento Sociedade Civil == outros
        //    {
        //        Control dsCampo2 = FindControlRecursive(this.Page, "txt_dsAuxiliar5");//Sociedade Civil - Outros
        //        Control linha2 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar5");

        //        if ((sender as DropDownList).Text.ToUpper() == "OUTRO")
        //        {
        //            if (dsCampo2 != null)
        //            {
        //                (linha2 as TableRow).Visible = true;//Sociedade Civil - Outros
        //            }
        //        }
        //        else 
        //        {
        //            if (dsCampo2 != null)
        //            {
        //                (dsCampo2 as TextBox).Text = "";
        //                (linha2 as TableRow).Visible = false;//Sociedade Civil - Outros

        //            }
        //        }
        //    }

        //    if ((sender as DropDownList).ID == "txt_dsAuxiliar9") //SCategoria de Participação == outra
        //    {
        //        Control dsCampo2 = FindControlRecursive(this.Page, "txt_dsAuxiliar10");//Categoria de Participação - Outra
        //        Control linha2 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar10");

        //        if ((sender as DropDownList).Text.ToUpper() == "OUTRA")
        //        {
        //            if (dsCampo2 != null)
        //            {
        //                (linha2 as TableRow).Visible = true;//Categoria de Participação - Outros
        //            }
        //        }
        //        else
        //        {
        //            if (dsCampo2 != null)
        //            {
        //                (dsCampo2 as TextBox).Text = "";
        //                (linha2 as TableRow).Visible = false;//Categoria de Participação - Outros

        //            }
        //        }
        //    }

            
        //}

        #endregion


        #region IESB
        //if (SessionEvento.CdEvento == "005701")
        //{



        //    if ((sender as DropDownList).ID == "txt_noInstituicao") //Escola
        //    {
        //        Control dsCampo1 = FindControlRecursive(this.Page, "txt_dsAuxiliar2");//Escola outra
        //        Control lblCampo1 = FindControlRecursive(this.Page, "lbl_dsAuxiliar2");

        //        if (((sender as DropDownList).Text.ToUpper() == "OUTRO") || ((sender as DropDownList).Text.ToUpper() == "OUTRA"))
        //        {
        //            if (dsCampo1 != null)
        //            {
        //                (dsCampo1 as TextBox).Visible = true;//ESCOLA NÃO ESTÁ NA LISTA 
        //                (lblCampo1 as Label).Visible = true;
        //            }
        //        }
        //        else
        //        {
        //            if (dsCampo1 != null)
        //            {
        //                (dsCampo1 as TextBox).Text = "";
        //                (dsCampo1 as TextBox).Visible = false;//ESCOLA ESTÁ NA LISTA
        //                (lblCampo1 as Label).Visible = false;
        //            }
        //        }
        //    }

        //    if ((sender as DropDownList).ID == "txt_dsAuxiliar1") //Séire
        //    {
        //        Control dsCampo2 = FindControlRecursive(this.Page, "txt_dsAuxiliar3");//Séire outra
        //        Control lblCampo2 = FindControlRecursive(this.Page, "lbl_dsAuxiliar3");

        //        if (((sender as DropDownList).Text.ToUpper() == "OUTRO") || ((sender as DropDownList).Text.ToUpper() == "OUTRA"))
        //        {
        //            if (dsCampo2 != null)
        //            {
        //                (dsCampo2 as TextBox).Visible = true;//SÉIRE NÃO ESTÁ NA LISTA 
        //                (lblCampo2 as Label).Visible = true;
        //            }
        //        }
        //        else
        //        {
        //            if (dsCampo2 != null)
        //            {
        //                (dsCampo2 as TextBox).Text = "";
        //                (dsCampo2 as TextBox).Visible = false;//SÉRIE ESTÁ NA LISTA
        //                (lblCampo2 as Label).Visible = false;
        //            }
        //        }
        //    }

            
        //}

        #endregion

        #region NTU2016
        //if (SessionEvento.CdEvento == "005503")
        //{
        //    if ((sender as DropDownList).ID == "txt_cdCategoria")
        //    {
        //        Control dsRazaoSocial = FindControlRecursive(this.Page, "txt_noInstituicao");
        //        Control lnRazaoSocial = FindControlRecursive(this.Page, "tblinha_noInstituicao");
                

        //        Control dsChave = FindControlRecursive(this.Page, "txt_dsCampoExtraPreCad");
        //        Control dsLin = FindControlRecursive(this.Page, "tblinha_dsCampoExtraPreCad");
        //        Control lbldsChave = FindControlRecursive(this.Page, "lbl_dsCampoExtraPreCad");



        //        Control dsExpositora = FindControlRecursive(this.Page, "txt_dsAuxiliar1");
        //        Control lnExpositora = FindControlRecursive(this.Page, "tblinha_dsAuxiliar1");



        //        if (((sender as DropDownList).Text == "00550301") ||  ((sender as DropDownList).Text == "00550302") ||
        //            ((sender as DropDownList).Text == "00550303") || ((sender as DropDownList).Text == "00550304"))
        //        {
                   
        //            if ((sender as DropDownList).Text == "00550301")
        //                (lbldsChave as Label).Text = "CNPJ da associada *";
        //            else if ((sender as DropDownList).Text == "00550302")
        //                (lbldsChave as Label).Text = "CNPJ de não associadas *";
        //            else if ((sender as DropDownList).Text == "00550303")
        //                (lbldsChave as Label).Text = "CNPJ do fornecedor *"; 


        //            if (lnRazaoSocial != null)
        //            {
        //                (lnRazaoSocial as TableRow).Visible = true;
        //            }

        //            if ((sender as DropDownList).Text != "00550304")
        //            {
        //                if (dsLin != null)
        //                {
        //                    (dsLin as TableRow).Visible = true;
        //                }
        //            }
        //            else
        //            {
        //                if (dsLin != null)
        //                {
        //                    (dsLin as TableRow).Visible = false;
        //                }
        //            }


        //            if (((sender as DropDownList).Text == "00550301") ||  ((sender as DropDownList).Text == "00550302"))
        //            {
        //                if (lnRazaoSocial != null)
        //                    lnExpositora.Visible = true;
        //            }
        //            else
        //            {
        //                 if (lnRazaoSocial != null)
        //                    lnExpositora.Visible = false;
        //            }
        //        }
        //        else
        //        {
        //            if (lnRazaoSocial != null)
        //            {
        //                (lnRazaoSocial as TableRow).Visible = false;
        //            }

        //            //if (dsChave != null)
        //            //{
        //            //    (dsChave as TextBox).Text = "";
        //            //    (dsChave as TextBox).Enabled = false;
        //            //}

        //            if (dsLin != null)
        //            {
        //                (dsLin as TableRow).Visible = false;
        //            }
        //        }
                
        //    }
            
        //}
        #endregion

        #region SICOOB
        if (SessionEvento.CdEvento == "008303")
        {
            Control Categoria = FindControlRecursive(this.Page, "txt_cdCategoria");
            string cdCateg = (Categoria as DropDownList).SelectedValue;

            if (((sender as DropDownList).ID == "txt_dsAuxiliar1"))
            {
                Control dsCampo1 = FindControlRecursive(this.Page, "txt_dsAuxiliar1");//CENTRAL
                Control dsCampo2 = FindControlRecursive(this.Page, "txt_dsAuxiliar2");//SINGULAR

                if (cdCateg == "00830301")
                {
                    if (!VerificarTotalCentralSICOOB(SessionEvento.CdEvento, SessionParticipante.CdParticipante, (dsCampo1 as DropDownList).Text))
                    {
                        return;
                    }
                }

                if (cdCateg == "00830302")
                    ListarOutrosSICOOB(SessionEvento.CdEvento, (dsCampo2 as DropDownList), (dsCampo1 as DropDownList).Text);

            }

            if (((sender as DropDownList).ID == "txt_dsAuxiliar2"))
            {
                Control dsCampo1 = FindControlRecursive(this.Page, "txt_dsAuxiliar1");//CENTRAL
                Control dsCampo2 = FindControlRecursive(this.Page, "txt_dsAuxiliar2");//SINGULAR

                if (cdCateg == "00830302")
                {
                    if (!VerificarTotalSingularSICOOB(SessionEvento.CdEvento, SessionParticipante.CdParticipante, (dsCampo1 as DropDownList).Text, (dsCampo2 as DropDownList).Text))
                    {
                        return;
                    }
                }



            }
        }
        #endregion
        
        #region ABIMDE 4 BID
        //if (SessionEvento.CdEvento == "008701")
        //{
        //    Control Categoria = FindControlRecursive(this.Page, "txt_cdCategoria");
        //    string cdCateg = (Categoria as DropDownList).SelectedValue;

        //    if (((sender as DropDownList).ID == "txt_dsAuxiliar1"))
        //    {
        //        Control dsCampo1 = FindControlRecursive(this.Page, "txt_dsAuxiliar1");//tipo Credenciamento

        //        Control dsCampo2 = FindControlRecursive(this.Page, "txt_dsAuxiliar2");//Categoria Militar
        //        Control dsLblCampo2 = FindControlRecursive(this.Page, "lbl_dsAuxiliar2");
        //        Control linha2 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar2");
        //        Control refv2 = FindControlRecursive(this.Page, "txt_dsAuxiliar2_Req");

        //        Control dsCampo3 = FindControlRecursive(this.Page, "txt_dsAuxiliar3");//Patente
        //        Control dsLblCampo3 = FindControlRecursive(this.Page, "lbl_dsAuxiliar3");
        //        Control linha3 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar3");
        //        Control refv3 = FindControlRecursive(this.Page, "txt_dsAuxiliar3_Req");

        //        Control dsCampo4 = FindControlRecursive(this.Page, "txt_dsAuxiliar5");//Patente Outros
        //        Control dsLblCampo4 = FindControlRecursive(this.Page, "lbl_dsAuxiliar5");
        //        Control linha4 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar5");
        //        Control refv4 = FindControlRecursive(this.Page, "txt_dsAuxiliar5_Req");


        //        Control dsCampo5 = FindControlRecursive(this.Page, "txt_noCargo");//Cargo
        //        Control dsLblCampo5 = FindControlRecursive(this.Page, "lbl_noCargo");
        //        Control linha5 = FindControlRecursive(this.Page, "tblinha_noCargo");
        //        Control refv5 = FindControlRecursive(this.Page, "txt_noCargo_Req");


        //        Control dsCampo6 = FindControlRecursive(this.Page, "txt_noAreaAtuacao");//Grau Hierárquico
        //        Control dsLblCampo6 = FindControlRecursive(this.Page, "lbl_noAreaAtuacao");
        //        Control linha6 = FindControlRecursive(this.Page, "tblinha_noAreaAtuacao");
        //        Control refv6 = FindControlRecursive(this.Page, "txt_noAreaAtuacao_Req");

        //        if (dsCampo1 != null)
        //        {
        //            if (((dsCampo1 as DropDownList).Text != "MILITAR") && ((dsCampo1 as DropDownList).Text != "MILITARY"))
        //            {
        //                (dsCampo2 as DropDownList).Visible = false;
        //                (dsCampo2 as DropDownList).Text = "";
        //                (linha2 as TableRow).Visible = false;

        //                (dsCampo3 as DropDownList).Visible = false;
        //                (dsCampo3 as DropDownList).Text = "";
        //                (linha3 as TableRow).Visible = false;

        //                (dsCampo4 as TextBox).Visible = false;
        //                (dsCampo4 as TextBox).Text = "";
        //                (linha4 as TableRow).Visible = false;


        //                if ((dsCampo1 as DropDownList).Text == "CIVIL")
        //                {
        //                    (dsCampo5 as TextBox).Visible = true;
        //                    (dsLblCampo5 as Label).Visible = true;
        //                    (linha5 as TableRow).Visible = true;
        //                    (refv5 as RequiredFieldValidator).Visible = true;

        //                    (dsCampo6 as DropDownList).Visible = true;
        //                    (dsLblCampo6 as Label).Visible = true;
        //                    (linha6 as TableRow).Visible = true;
        //                    (refv6 as RequiredFieldValidator).Visible = true;
        //                }
        //                else
        //                {
        //                    (dsCampo5 as TextBox).Visible = false;
        //                    (dsCampo5 as TextBox).Text = "";
        //                    (linha5 as TableRow).Visible = false;

        //                    (dsCampo6 as DropDownList).Visible = false;
        //                    (dsCampo6 as DropDownList).Text = "";
        //                    (linha6 as TableRow).Visible = false;
        //                }
        //            }
        //            else //if ((dsCampo1 as DropDownList).Text == "MILITAR")
        //            {                       

        //                if (cdCateg != "00870102")
        //                {
        //                    (dsCampo2 as DropDownList).Visible = true;
        //                    (dsLblCampo2 as Label).Visible = true;
        //                    (linha2 as TableRow).Visible = true;
        //                    (refv2 as RequiredFieldValidator).Visible = true;

        //                    (dsCampo3 as DropDownList).Visible = true;
        //                    (dsLblCampo3 as Label).Visible = true;
        //                    (linha3 as TableRow).Visible = true;
        //                    (refv3 as RequiredFieldValidator).Visible = true;
        //                }
        //                else
        //                {
        //                    (dsCampo2 as DropDownList).Visible = false;
        //                    (dsLblCampo2 as Label).Visible = false;
        //                    (linha2 as TableRow).Visible = false;

        //                    (dsCampo3 as DropDownList).Visible = false;
        //                    (dsLblCampo3 as Label).Visible = false;
        //                    (linha3 as TableRow).Visible = false;

        //                    (dsCampo4 as TextBox).Visible = true;
        //                    (dsLblCampo4 as Label).Visible = true;
        //                    (linha4 as TableRow).Visible = true;
        //                    (refv4 as RequiredFieldValidator).Visible = true;
        //                }

        //                (dsCampo5 as TextBox).Visible = false;
        //                (dsCampo5 as TextBox).Text = "";
        //                (linha5 as TableRow).Visible = false;

        //                (dsCampo6 as DropDownList).Visible = false;
        //                (dsCampo6 as DropDownList).Text = "";
        //                (linha6 as TableRow).Visible = false;
        //            }
        //        }
                
        //    }
            
        //    if (((sender as DropDownList).ID == "txt_dsAuxiliar2"))
        //    {
                
        //        Control dsCampo2 = FindControlRecursive(this.Page, "txt_dsAuxiliar2");//Categoria Militar
        //        Control dsLblCampo2 = FindControlRecursive(this.Page, "lbl_dsAuxiliar2");
        //        Control linha2 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar2");
        //        Control dsCampo3 = FindControlRecursive(this.Page, "txt_dsAuxiliar3");//Patente
        //        Control dsLblCampo3 = FindControlRecursive(this.Page, "lbl_dsAuxiliar3");
        //        Control linha3 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar3");

        //        if (dsCampo2 != null)
        //        {

        //            ListarPatentesBID(SessionEvento.CdEvento, (dsCampo3 as DropDownList), (dsCampo2 as DropDownList).Text);

                                       
        //        }

        //    }

        //    if (((sender as DropDownList).ID == "txt_dsAuxiliar3"))
        //    {                
        //        Control dsCampo3 = FindControlRecursive(this.Page, "txt_dsAuxiliar3");//Patente
        //        Control dsLblCampo3 = FindControlRecursive(this.Page, "lbl_dsAuxiliar3");
        //        Control linha3 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar3");

        //        Control dsCampo4 = FindControlRecursive(this.Page, "txt_dsAuxiliar5");//Patente Outros
        //        Control dsLblCampo4 = FindControlRecursive(this.Page, "lbl_dsAuxiliar5");
        //        Control linha4 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar5");
        //        Control refv4 = FindControlRecursive(this.Page, "txt_dsAuxiliar5_Req");

        //        if (dsCampo3 != null)
        //        {

        //            if (((dsCampo3 as DropDownList).Text != "OUTRA") && ((dsCampo3 as DropDownList).Text != "OTHER"))
        //            {                        
        //                (dsCampo4 as TextBox).Visible = false;
        //                (dsCampo4 as TextBox).Text = "";
        //                (linha4 as TableRow).Visible = false;
        //            }
        //            else //if ((dsCampo1 as DropDownList).Text == "MILITAR")
        //            {
        //                (dsCampo4 as TextBox).Visible = true;
        //                (dsLblCampo4 as Label).Visible = true;
        //                (linha4 as TableRow).Visible = true;
        //                (refv4 as RequiredFieldValidator).Visible = true;
        //            }


        //        }

        //    }

        //    if (((sender as DropDownList).ID == "txt_noAreaAtuacao")) //Grau Hierárquico - outro
        //    {
        //        Control dsCampo3 = FindControlRecursive(this.Page, "txt_dsAuxiliar6");//Grau Hierárquico - outro
        //        Control dsLblCampo3 = FindControlRecursive(this.Page, "lbl_dsAuxiliar6");
        //        Control linha3 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar6");
        //        Control refv3 = FindControlRecursive(this.Page, "txt_dsAuxiliar6_Req");

        //        if (dsCampo3 != null)
        //        {

        //            if (((sender as DropDownList).Text != "OUTRO") && ((sender as DropDownList).Text != "OTHER"))
        //            {
        //                (dsCampo3 as TextBox).Visible = false;
        //                (dsCampo3 as TextBox).Text = "";
        //                (linha3 as TableRow).Visible = false;
        //            }
        //            else //if ((dsCampo1 as DropDownList).Text == "MILITAR")
        //            {
        //                (dsCampo3 as TextBox).Visible = true;
        //                (dsLblCampo3 as Label).Visible = true;
        //                (linha3 as TableRow).Visible = true;
        //                (refv3 as RequiredFieldValidator).Visible = true;
        //            }


        //        }

        //    }

        //}
        #endregion

        #region Expoepi2017

        //if (SessionEvento.CdEvento == "009401")
        //{
        //    //Control Categoria = FindControlRecursive(this.Page, "txt_cdCategoria");
        //    //string cdCateg = (Categoria as DropDownList).SelectedValue;

        //    if (((sender as DropDownList).ID == "txt_noInstituicao"))
        //    {
        //        Control dsCampo1 = FindControlRecursive(this.Page, "txt_noInstituicao"); //Instituicao

        //        Control dsCampo2 = FindControlRecursive(this.Page, "txt_dsAuxiliar4"); //Instituicao Complemento
        //        Control dsLblCampo2 = FindControlRecursive(this.Page, "lbl_dsAuxiliar4");
        //        Control linha2 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar4");
        //        Control refv2 = FindControlRecursive(this.Page, "txt_dsAuxiliar4_Req");

        //        Control dsCampo3 = FindControlRecursive(this.Page, "txt_dsAuxiliar5"); //Instituicao Outro
        //        Control dsLblCampo3 = FindControlRecursive(this.Page, "lbl_dsAuxiliar5");
        //        Control linha3 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar5");
        //        Control refv3 = FindControlRecursive(this.Page, "txt_dsAuxiliar5_Req");



        //        if (dsCampo1 != null)
        //        {
        //            if (((dsCampo1 as DropDownList).Text == "MINISTÉRIO DA SAÚDE") ||
        //                    ((dsCampo1 as DropDownList).Text == "ORGANISMO INTERNACIONAL"))
        //            {
        //                (dsCampo2 as DropDownList).Visible = true;
        //                (dsCampo2 as DropDownList).Text = "";
        //                (dsLblCampo2 as Label).Visible = true;
        //                (linha2 as TableRow).Visible = true;
        //                (refv2 as RequiredFieldValidator).Visible = true;

        //                (dsCampo3 as TextBox).Visible = false;
        //                (dsCampo3 as TextBox).Text = "";
        //                (dsLblCampo3 as Label).Visible = false;
        //                (linha3 as TableRow).Visible = false;
        //                (refv3 as RequiredFieldValidator).Visible = false;

        //                ListarCamposExpoepi(SessionEvento.CdEvento, (dsCampo2 as DropDownList),
        //                    (dsCampo1 as DropDownList).Text);
        //            }
        //            else if (((dsCampo1 as DropDownList).Text == "SES") || ((dsCampo1 as DropDownList).Text == "SMS") || ((dsCampo1 as DropDownList).Text == "COSEMS"))
        //            {
        //                (dsCampo2 as DropDownList).Visible = false;
        //                (dsCampo2 as DropDownList).Text = "";
        //                (dsLblCampo2 as Label).Visible = false;
        //                (linha2 as TableRow).Visible = false;
        //                (refv2 as RequiredFieldValidator).Visible = false;

        //                (dsCampo3 as TextBox).Visible = false;
        //                (dsCampo3 as TextBox).Text = "";
        //                (dsLblCampo3 as Label).Visible = false;
        //                (linha3 as TableRow).Visible = false;
        //                (refv3 as RequiredFieldValidator).Visible = false;
        //            }
        //            else if ((dsCampo1 as DropDownList).Text == "OUTRO")
        //            {
        //                (dsCampo3 as TextBox).Visible = true;
        //                (dsCampo3 as TextBox).Text = "";
        //                (dsLblCampo3 as Label).Visible = true;
        //                (linha3 as TableRow).Visible = true;
        //                (refv3 as RequiredFieldValidator).Visible = true;

        //                (dsCampo2 as DropDownList).Visible = false;
        //                (dsCampo2 as DropDownList).Text = "";
        //                (dsLblCampo2 as Label).Visible = true;
        //                (linha2 as TableRow).Visible = false;
        //                (refv2 as RequiredFieldValidator).Visible = false;

        //            }


        //        }

        //    }

        //}




        #endregion

        #region CNTC

        if (SessionEvento.CdEvento == "010101")
        {
            //Control Categoria = FindControlRecursive(this.Page, "txt_cdCategoria");
            //string cdCateg = (Categoria as DropDownList).SelectedValue;

            if (((sender as DropDownList).ID == "txt_cdCategoria"))
            {
                Control dsCampo1 = FindControlRecursive(this.Page, "txt_cdCategoria"); //Categoria

                Control dsCampo2 = FindControlRecursive(this.Page, "txt_noCargo"); //Cargo
                Control dsLblCampo2 = FindControlRecursive(this.Page, "lbl_noCargo");
                Control linha2 = FindControlRecursive(this.Page, "tblinha_noCargo");
                Control refv2 = FindControlRecursive(this.Page, "txt_noCargo_Req");

                Control dsCampo3 = FindControlRecursive(this.Page, "txt_dsAuxiliar1"); //Semestre
                Control dsLblCampo3 = FindControlRecursive(this.Page, "lbl_dsAuxiliar1");
                Control linha3 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar1");
                Control refv3 = FindControlRecursive(this.Page, "txt_dsAuxiliar1_Req");




                if ((dsCampo1 as DropDownList).SelectedValue.ToString() == "01010102")//"DIRIGENTES SINDICAIS")
                {
                    (dsCampo2 as TextBox).Visible = true;
                    (dsCampo2 as TextBox).Text = "";
                    (dsLblCampo2 as Label).Visible = true;
                    (linha2 as TableRow).Visible = true;
                    (refv2 as RequiredFieldValidator).Visible = true;

                    (dsCampo3 as TextBox).Visible = false;
                    (dsCampo3 as TextBox).Text = "";
                    (dsLblCampo3 as Label).Visible = false;
                    (linha3 as TableRow).Visible = false;
                    (refv3 as RequiredFieldValidator).Visible = false;

                   
                }
                else if ((dsCampo1 as DropDownList).SelectedValue.ToString() == "01010103")//"ESTUDANTES DE DIREITO")
                {
                    (dsCampo2 as TextBox).Visible = false;
                    (dsCampo2 as TextBox).Text = "";
                    (dsLblCampo2 as Label).Visible = false;
                    (linha2 as TableRow).Visible = false;
                    (refv2 as RequiredFieldValidator).Visible = false;

                    (dsCampo3 as TextBox).Visible = true;
                    (dsCampo3 as TextBox).Text = "";
                    (dsLblCampo3 as Label).Visible = true;
                    (linha3 as TableRow).Visible = true;
                    (refv3 as RequiredFieldValidator).Visible = true;
                }
                else 
                {
                    (dsCampo3 as TextBox).Visible = false;
                    (dsCampo3 as TextBox).Text = "";
                    (dsLblCampo3 as Label).Visible = false;
                    (linha3 as TableRow).Visible = false;
                    (refv3 as RequiredFieldValidator).Visible = false;

                    (dsCampo2 as TextBox).Visible = false;
                    (dsCampo2 as TextBox).Text = "";
                    (dsLblCampo2 as Label).Visible = true;
                    (linha2 as TableRow).Visible = false;
                    (refv2 as RequiredFieldValidator).Visible = false;

                }




            }

        }




        #endregion

        #region Toccata Evento Embaixada

        if (SessionEvento.CdEvento == "010501")
        {
            //Control Categoria = FindControlRecursive(this.Page, "txt_cdCategoria");
            //string cdCateg = (Categoria as DropDownList).SelectedValue;

            if (((sender as DropDownList).ID == "txt_dsAuxiliar2"))
            {
                Control dsCampo1 = FindControlRecursive(this.Page, "txt_dsAuxiliar2"); //Tipo Documento

                Control dsCampo2 = FindControlRecursive(this.Page, "txt_nuCPFCNPJ");//CPF
                Control dsLblCampo2 = FindControlRecursive(this.Page, "lbl_nuCPFCNPJ");
                Control linha2 = FindControlRecursive(this.Page, "tblinha_nuCPFCNPJ");
                Control refv2 = FindControlRecursive(this.Page, "txt_nuCPFCNPJ_Req");

                Control dsCampo3 = FindControlRecursive(this.Page, "txt_dsAuxiliar1"); //PASSAPORTE
                Control dsLblCampo3 = FindControlRecursive(this.Page, "lbl_dsAuxiliar1");
                Control linha3 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar1");
                Control refv3 = FindControlRecursive(this.Page, "txt_dsAuxiliar1_Req");




                if ((dsCampo1 as DropDownList).SelectedValue.ToString() == "CPF")
                {
                    (dsCampo2 as TextBox).Visible = true;
                    (dsCampo2 as TextBox).Text = "";
                    (dsLblCampo2 as Label).Visible = true;
                    (linha2 as TableRow).Visible = true;
                    (refv2 as RequiredFieldValidator).Visible = true;

                    (dsCampo3 as TextBox).Visible = false;
                    (dsCampo3 as TextBox).Text = "";
                    (dsLblCampo3 as Label).Visible = false;
                    (linha3 as TableRow).Visible = false;
                    (refv3 as RequiredFieldValidator).Visible = false;


                }
                else if ((dsCampo1 as DropDownList).SelectedValue.ToString() == "PASSAPORTE")
                {
                    (dsCampo2 as TextBox).Visible = false;
                    (dsCampo2 as TextBox).Text = "";
                    (dsLblCampo2 as Label).Visible = false;
                    (linha2 as TableRow).Visible = false;
                    (refv2 as RequiredFieldValidator).Visible = false;

                    (dsCampo3 as TextBox).Visible = true;
                    (dsCampo3 as TextBox).Text = "";
                    (dsLblCampo3 as Label).Visible = true;
                    (linha3 as TableRow).Visible = true;
                    (refv3 as RequiredFieldValidator).Visible = true;
                }
            }

        }




        #endregion


        #region ABERT 2018

        if (SessionEvento.CdEvento == "001008")
        {
            Control Categoria = FindControlRecursive(this.Page, "txt_cdCategoria");
            string cdCateg = (Categoria as DropDownList).SelectedValue;

            if (((sender as DropDownList).ID == "txt_cdCategoria"))
            {
                Control dsCampo1 = FindControlRecursive(this.Page, "tblinha_dsLabelSeparador1"); //Separador info hotéis
                Control dslblCampo1 = FindControlRecursive(this.Page, "txt_dsLabelSeparador1");

                Control dsCampo2 = FindControlRecursive(this.Page, "txt_dsAuxiliar18");//hotel 1
                Control dsLblCampo2 = FindControlRecursive(this.Page, "lbl_dsAuxiliar18");
                Control linha2 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar18");
                Control refv2 = FindControlRecursive(this.Page, "txt_dsAuxiliar18_Req");

                Control dsCampo3 = FindControlRecursive(this.Page, "txt_dsAuxiliar19"); //hotel 2
                Control dsLblCampo3 = FindControlRecursive(this.Page, "lbl_dsAuxiliar19");
                Control linha3 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar19");
                Control refv3 = FindControlRecursive(this.Page, "txt_dsAuxiliar19_Req");

                Control dsCampo4 = FindControlRecursive(this.Page, "txt_dsAuxiliar1"); //tipo de cama
                Control dsLblCampo4 = FindControlRecursive(this.Page, "lbl_dsAuxiliar1");
                Control linha4 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar1");
                Control refv4 = FindControlRecursive(this.Page, "txt_dsAuxiliar1_Req");




                if (cdCateg == "00100801")
                {

                    if (dsCampo1 != null)
                    {

                        (dsCampo1 as TableRow).Visible = true;
                        (dslblCampo1 as Label).Visible = true;

                        (dsCampo2 as DropDownList).Visible = true;
                        (dsCampo2 as DropDownList).Text = "";
                        (dsLblCampo2 as Label).Visible = true;
                        (linha2 as TableRow).Visible = true;
                        (refv2 as RequiredFieldValidator).Visible = true;

                        (dsCampo3 as DropDownList).Visible = true;
                        (dsCampo3 as DropDownList).Text = "";
                        (dsLblCampo3 as Label).Visible = true;
                        (linha3 as TableRow).Visible = true;
                        //(refv3 as RequiredFieldValidator).Visible = true;
                                                
                        (dsCampo4 as DropDownList).Visible = true;
                        (dsCampo4 as DropDownList).Text = "";
                        (dsLblCampo4 as Label).Visible = true;
                        (linha4 as TableRow).Visible = true;
                        (refv4 as RequiredFieldValidator).Visible = true;
                    }

                }
                else //if ()
                {
                    if (dsCampo1 != null)
                    {
                        (dsCampo1 as TableRow).Visible = false;
                        (dslblCampo1 as Label).Visible = false;

                        (dsCampo2 as DropDownList).Visible = false;
                        (dsCampo2 as DropDownList).Text = "";
                        (dsLblCampo2 as Label).Visible = false;
                        (linha2 as TableRow).Visible = false;
                        (refv2 as RequiredFieldValidator).Visible = false;

                        (dsCampo3 as DropDownList).Visible = false;
                        (dsCampo3 as DropDownList).Text = "";
                        (dsLblCampo3 as Label).Visible = false;
                        (linha3 as TableRow).Visible = false;
                        //(refv3 as RequiredFieldValidator).Visible = false;

                        (dsCampo4 as DropDownList).Visible = false;
                        (dsCampo4 as DropDownList).Text = "";
                        (dsLblCampo4 as Label).Visible = false;
                        (linha4 as TableRow).Visible = false;
                        (refv4 as RequiredFieldValidator).Visible = false;

                    }
                }
                
            }

        }




        #endregion

        #region Neuronus

        if (SessionEvento.CdEvento == "012601")
        {
            //Control Categoria = FindControlRecursive(this.Page, "txt_cdCategoria");
            //string cdCateg = (Categoria as DropDownList).SelectedValue;

            if (((sender as DropDownList).ID == "txt_dsAuxiliar1"))
            {
                Control dsCampo1 = FindControlRecursive(this.Page, "txt_dsAuxiliar1"); //Área de Formação

                Control dsCampo2 = FindControlRecursive(this.Page, "txt_dsAuxiliar2"); //Área de Formação - Outro
                Control dsLblCampo2 = FindControlRecursive(this.Page, "lbl_dsAuxiliar2");
                Control linha2 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar2");
                Control refv2 = FindControlRecursive(this.Page, "txt_dsAuxiliar2_Req");

                Control dsCampo3 = FindControlRecursive(this.Page, "txt_dsAuxiliar3"); //Área de Formação - Complemento
                Control dsLblCampo3 = FindControlRecursive(this.Page, "lbl_dsAuxiliar3");
                Control linha3 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar3");
                Control refv3 = FindControlRecursive(this.Page, "txt_dsAuxiliar3_Req");

                Control dsCampo4 = FindControlRecursive(this.Page, "txt_dsAuxiliar4"); //Área de Formação - Complemento (Outro)
                Control dsLblCampo4 = FindControlRecursive(this.Page, "lbl_dsAuxiliar4");
                Control linha4 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar4");
                Control refv4 = FindControlRecursive(this.Page, "txt_dsAuxiliar4_Req");



                if (dsCampo1 != null)
                {
                    if (((dsCampo1 as DropDownList).Text == "OUTRA") || ((dsCampo1 as DropDownList).Text == "OTHER"))
                    {
                        (dsCampo2 as TextBox).Visible = true;
                        (dsCampo2 as TextBox).Text = "";
                        (dsLblCampo2 as Label).Visible = true;
                        (linha2 as TableRow).Visible = true;
                        (refv2 as RequiredFieldValidator).Visible = true;

                        (dsCampo3 as DropDownList).Visible = false;
                        (dsCampo3 as DropDownList).Text = "";
                        (dsLblCampo3 as Label).Visible = false;
                        (linha3 as TableRow).Visible = false;
                        (refv3 as RequiredFieldValidator).Visible = false;

                        (dsCampo4 as TextBox).Visible = false;
                        (dsCampo4 as TextBox).Text = "";
                        (dsLblCampo4 as Label).Visible = false;
                        (linha4 as TableRow).Visible = false;
                        (refv4 as RequiredFieldValidator).Visible = false;

                       
                    }
                    else if (((dsCampo1 as DropDownList).Text == "MEDICINA") || ((dsCampo1 as DropDownList).Text == "MEDICINE"))
                    {
                        (dsCampo2 as TextBox).Visible = false;
                        (dsCampo2 as TextBox).Text = "";
                        (dsLblCampo2 as Label).Visible = false;
                        (linha2 as TableRow).Visible = false;
                        (refv2 as RequiredFieldValidator).Visible = false;

                        (dsCampo3 as DropDownList).Visible = true;
                        (dsCampo3 as DropDownList).Text = "";
                        (dsLblCampo3 as Label).Visible = true;
                        (linha3 as TableRow).Visible = true;
                        (refv3 as RequiredFieldValidator).Visible = true;

                        (dsCampo4 as TextBox).Visible = false;
                        (dsCampo4 as TextBox).Text = "";
                        (dsLblCampo4 as Label).Visible = false;
                        (linha4 as TableRow).Visible = false;
                        (refv4 as RequiredFieldValidator).Visible = false;
                    }
                    else 
                    {
                        (dsCampo2 as TextBox).Visible = false;
                        (dsCampo2 as TextBox).Text = "";
                        (dsLblCampo2 as Label).Visible = false;
                        (linha2 as TableRow).Visible = false;
                        (refv2 as RequiredFieldValidator).Visible = false;

                        (dsCampo3 as DropDownList).Visible = false;
                        (dsCampo3 as DropDownList).Text = "";
                        (dsLblCampo3 as Label).Visible = false;
                        (linha3 as TableRow).Visible = false;
                        (refv3 as RequiredFieldValidator).Visible = false;

                        (dsCampo4 as TextBox).Visible = false;
                        (dsCampo4 as TextBox).Text = "";
                        (dsLblCampo4 as Label).Visible = false;
                        (linha4 as TableRow).Visible = false;
                        (refv4 as RequiredFieldValidator).Visible = false;
                    }
                }

            }

            if (((sender as DropDownList).ID == "txt_dsAuxiliar3"))
            {
                Control dsCampo1 = FindControlRecursive(this.Page, "txt_dsAuxiliar3"); //Área de Formação - Complemento

                Control dsCampo4 = FindControlRecursive(this.Page, "txt_dsAuxiliar4"); //Área de Formação - Complemento (Outra)
                Control dsLblCampo4 = FindControlRecursive(this.Page, "lbl_dsAuxiliar4");
                Control linha4 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar4");
                Control refv4 = FindControlRecursive(this.Page, "txt_dsAuxiliar4_Req");



                if (dsCampo1 != null)
                {
                    if (((dsCampo1 as DropDownList).Text == "OUTRA") || ((dsCampo1 as DropDownList).Text == "OTHER"))
                    {
                        (dsCampo4 as TextBox).Visible = true;
                        (dsCampo4 as TextBox).Text = "";
                        (dsLblCampo4 as Label).Visible = true;
                        (linha4 as TableRow).Visible = true;
                        (refv4 as RequiredFieldValidator).Visible = true;


                    }
                    else 
                    {
                        (dsCampo4 as TextBox).Visible = false;
                        (dsCampo4 as TextBox).Text = "";
                        (dsLblCampo4 as Label).Visible = false;
                        (linha4 as TableRow).Visible = false;
                        (refv4 as RequiredFieldValidator).Visible = false;
                    }
                }

            }

        }


        #endregion

    }

    protected void verificarCampos()
    {
        #region CONSAD

        //if (SessionEvento.CdCliente == "0003")
        //{
        //    if ((SessionParticipante != null) && (SessionParticipante.CdParticipante != ""))
        //    {
        //       //SessionParticipante.Categoria.FlPreCadastroExclusivo
        //        if ((SessionParticipante.CdCategoria != "00030406") && 
        //            (SessionParticipante.CdCategoria != "00030506") && (SessionParticipante.CdCategoria != "00030512") &&
        //            (SessionParticipante.CdCategoria != "00030606") && (SessionParticipante.CdCategoria != "00030612") &&
        //            (SessionParticipante.CdCategoria != "00030706") && (SessionParticipante.CdCategoria != "00030712"))
        //        {

        //            Control dsChave = FindControlRecursive(this.Page, "txt_dsCampoExtraPreCad");
        //            if (dsChave != null)
        //            {
        //                (dsChave as TextBox).Text = "";
        //                (dsChave as TextBox).Enabled = false;
        //            }

        //            Control dsLin = FindControlRecursive(this.Page, "tblinha_dsCampoExtraPreCad");
        //            if (dsLin != null)
        //            {
        //                (dsLin as TableRow).Visible = false;
        //            }
        //        }
        //        else
        //        {

        //            Control dsCateg = FindControlRecursive(this.Page, "txt_cdCategoria");
        //            if (dsCateg != null)
        //            {

        //                (dsCateg as DropDownList).Enabled = false;// = "teste2";
        //            }

        //            if ((SessionParticipante.DsCampoExtraPreCad == null) || (SessionParticipante.DsCampoExtraPreCad == ""))
        //            {
        //                Control dsChave = FindControlRecursive(this.Page, "txt_dsCampoExtraPreCad");
        //                if (dsChave != null)
        //                {

        //                    (dsChave as TextBox).Enabled = true;
        //                }

        //                Control dsLin = FindControlRecursive(this.Page, "tblinha_dsCampoExtraPreCad");
        //                if (dsLin != null)
        //                {
        //                    (dsLin as TableRow).Visible = true;
        //                }
        //            }
        //            else
        //            {
        //                Control dsChave = FindControlRecursive(this.Page, "txt_dsCampoExtraPreCad");
        //                if (dsChave != null)
        //                {

        //                    (dsChave as TextBox).Enabled = false;
        //                }

        //                Control dsLin = FindControlRecursive(this.Page, "tblinha_dsCampoExtraPreCad");
        //                if (dsLin != null)
        //                {
        //                    (dsLin as TableRow).Visible = true;
        //                }
        //            }
        //        }

        //        if ((SessionParticipante.CdCategoria == "00030506") || (SessionParticipante.CdCategoria == "00030512") ||
        //            (SessionParticipante.CdCategoria == "00030606") || (SessionParticipante.CdCategoria == "00030612") ||
        //            (SessionParticipante.CdCategoria == "00030706") || (SessionParticipante.CdCategoria == "00030712"))
        //        {

        //            Control dsChave = FindControlRecursive(this.Page, "txt_dsAuxiliar2");
        //            if (dsChave != null)
        //            {
        //                (dsChave as DropDownList).Text = ((SessionParticipante.CdCategoria == "00030506") || (SessionParticipante.CdCategoria == "00030606") || (SessionParticipante.CdCategoria == "00030706") ? "" : "EMPENHO");
        //                (dsChave as DropDownList).Enabled = false;
        //            }

        //            Control dsLin = FindControlRecursive(this.Page, "tblinha_dsAuxiliar2");
        //            if (dsLin != null)
        //            {
        //                (dsLin as TableRow).Visible = false;
        //            }
        //        }
        //        else
        //        {

        //            Control dsChave = FindControlRecursive(this.Page, "txt_dsAuxiliar2");
        //            if (dsChave != null)
        //            {
        //                (dsChave as DropDownList).Enabled = true;
        //            }

        //            Control dsLin = FindControlRecursive(this.Page, "tblinha_dsAuxiliar2");
        //            if (dsLin != null)
        //            {
        //                (dsLin as TableRow).Visible = true;
        //            }
        //        }


        //        if ((SessionParticipante.DsAuxiliar3 == "OUTROS") || (SessionParticipante.DsAuxiliar3 == "NÃO SE APLICA"))
        //        {
        //            Control coldsAuxiliar1 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar1");
        //            if (coldsAuxiliar1 != null)
        //            {
        //                (coldsAuxiliar1 as TableRow).Visible = false;
        //            }
        //        }
        //        else
        //        {

        //            Control coldsAuxiliar1 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar1");
        //            if (coldsAuxiliar1 != null)
        //            {
        //                (coldsAuxiliar1 as TableRow).Visible = true;
        //            }
        //        }

        //    }
        //    else
        //    {
        //        if ((SessionCateg == null) || (SessionCateg == ""))
        //        {
        //            Control dsLin = FindControlRecursive(this.Page, "tblinha_dsCampoExtraPreCad");
        //            if (dsLin != null)
        //            {
        //                (dsLin as TableRow).Visible = false;
        //            }
        //        }
        //        else
        //        {
        //            Control dsLin = FindControlRecursive(this.Page, "tblinha_dsCampoExtraPreCad");
        //            if (dsLin != null)
        //            {
        //                (dsLin as TableRow).Visible = true;
        //            }

        //            if ((SessionCateg == "00030506") || (SessionCateg == "00030512") ||
        //                (SessionCateg == "00030606") || (SessionCateg == "00030612") ||
        //                (SessionCateg == "00030706") || (SessionCateg == "00030712"))
        //            {

        //                Control dsChave = FindControlRecursive(this.Page, "txt_dsAuxiliar2");
        //                if (dsChave != null)
        //                {
        //                    (dsChave as DropDownList).Text = ((SessionCateg == "00030506") || (SessionCateg == "00030606") || (SessionCateg == "00030706") ? "" : "EMPENHO");
        //                    (dsChave as DropDownList).Enabled = false;
        //                }

        //                Control dsLin2 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar2");
        //                if (dsLin2 != null)
        //                {
        //                    (dsLin2 as TableRow).Visible = false;
        //                }
        //            }
        //            else
        //            {

        //                Control dsChave = FindControlRecursive(this.Page, "txt_dsAuxiliar2");
        //                if (dsChave != null)
        //                {
        //                    (dsChave as DropDownList).Enabled = true;
        //                }

        //                Control dsLin3 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar2");
        //                if (dsLin3 != null)
        //                {
        //                    (dsLin3 as TableRow).Visible = true;
        //                }
        //            }

        //            if ((SessionParticipante.DsAuxiliar3 == "OUTROS") || (SessionParticipante.DsAuxiliar3 == "NÃO SE APLICA"))
        //            {
        //                Control coldsAuxiliar1 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar1");
        //                if (coldsAuxiliar1 != null)
        //                {
        //                    (coldsAuxiliar1 as TableRow).Visible = false;
        //                }
        //            }
        //            else
        //            {

        //                Control coldsAuxiliar1 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar1");
        //                if (coldsAuxiliar1 != null)
        //                {
        //                    (coldsAuxiliar1 as TableRow).Visible = true;
        //                }
        //            }
        //        }
        //    }

        //}
        #endregion

        #region wcit2016
        //if ((SessionEvento.CdCliente == "0077") && (int.Parse(SessionEvento.CdEvento) == 7701))
        //{
        //    if ((SessionParticipante != null))// && (SessionParticipante.CdParticipante != ""))
        //    {
        //        //if ((sender as DropDownList).ID == "txt_noAreaAtuacao")
        //        //{
        //            Control dsCampo1 = FindControlRecursive(this.Page, "txt_dsAuxiliar3");//complemento da área de atuacao
        //            Control linha1 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar3");

        //            if (SessionParticipante.NoAreaAtuacao.ToUpper() == "OUTROS - ESPECIFICAR")
        //            {
        //                if (linha1 != null)
        //                {
        //                    (linha1 as TableRow).Visible = true;
        //                }
        //            }
        //            else
        //            {
        //                if (linha1 != null)
        //                {
        //                    (dsCampo1 as TextBox).Text = "";
        //                    (linha1 as TableRow).Visible = false;
        //                }
        //            }
        //        //}

        //        //if ((sender as DropDownList).ID == "txt_noCargo")
        //        //{
        //            Control dsCampo2 = FindControlRecursive(this.Page, "txt_dsAuxiliar5");//complemento da área de atuacao
        //            Control linha2 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar5");

        //            if (SessionParticipante.NoCargo.ToUpper() == "OUTROS - ESPECIFICAR")
        //            {
        //                if (linha2 != null)
        //                {
        //                    (linha2 as TableRow).Visible = true;
        //                }
        //            }
        //            else
        //            {
        //                if (linha2 != null)
        //                {
        //                    (dsCampo2 as TextBox).Text = "";
        //                    (linha2 as TableRow).Visible = false;
        //                }
        //            }
        //        //}
        //    }
        //}
        #endregion


        #region CONASEMS 2013
        //if (SessionEvento.CdEvento == "001303")
        //{
        //    if ((SessionParticipante != null) && (SessionParticipante.CdParticipante != ""))
        //    {
        //        Control dsCampo1 = FindControlRecursive(this.Page, "txt_dsAuxiliar11");//atualmente trabalha onde
        //        Control linha1 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar11");
        //        Control dsCampo2 = FindControlRecursive(this.Page, "txt_noCargo");//cargo
        //        Control linha2 = FindControlRecursive(this.Page, "tblinha_noCargo");
        //        Control dsCampo3 = FindControlRecursive(this.Page, "txt_dsAuxiliar16");//instituição de origem
        //        Control linha3 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar16");
        //        Control dsCampo4 = FindControlRecursive(this.Page, "txt_dsAuxiliar13");//Atuação no cosems
        //        Control linha4 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar13");

        //        Control dsCampo5 = FindControlRecursive(this.Page, "cascaDD_dsAuxiliar8");//UF CARGO
        //        Control linha5 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar8");
        //        Control dsCampo6 = FindControlRecursive(this.Page, "cascaDD_dsAuxiliar9");//MUNICIPIO CARGO
        //        Control linha6 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar9");

        //        Control dsCampo41 = FindControlRecursive(this.Page, "txt_dsAuxiliar14");//É a primeira vez...
        //        Control linha41 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar14");
        //        Control dsCampo42 = FindControlRecursive(this.Page, "txt_dsAuxiliar17");//Tempo de experiência...
        //        Control linha42 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar17");

        //        Control linha43 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar15");

        //        if (SessionParticipante.NoAreaAtuacao == "SIM") //SE É SECRETÁRIO MUNICIPAL SAUDE
        //        {
        //            if (dsCampo1 != null)
        //            {
        //                (dsCampo1 as DropDownList).Text = "MUNICÍPIO";
        //                (dsCampo1 as DropDownList).Enabled = false;
        //            }
        //            if (dsCampo2 != null)
        //            {
        //                (linha2 as TableRow).Visible = true;
        //                (linha5 as TableRow).Visible = true;
        //                (linha6 as TableRow).Visible = true;
        //                (dsCampo2 as DropDownList).Items.Clear();
        //                (dsCampo2 as DropDownList).Items.Add("SECRETÁRIO MUNICIPAL DE SAÚDE");
        //                (dsCampo2 as DropDownList).Enabled = false;
        //            }
        //            if (dsCampo3 != null)
        //            {
        //                (linha3 as TableRow).Visible = false;
        //            }
        //            if (dsCampo4 != null)
        //            {
        //                (linha4 as TableRow).Visible = true;
        //            }
        //            if (linha41 != null)
        //            {
        //                (linha41 as TableRow).Visible = true;
        //            }
        //            if (linha42 != null)
        //            {
        //                (linha42 as TableRow).Visible = true;
        //            }
        //            if (linha43 != null)
        //            {
        //                (linha43 as TableRow).Visible = true;
        //            }
        //        }
        //        else //if (SessionParticipante.NoAreaAtuacao == "NÃO")
        //        {
        //            if (dsCampo1 != null)
        //            {
        //                (dsCampo1 as DropDownList).Enabled = true;
        //            }
        //            if (dsCampo2 != null)
        //            {
        //                (dsCampo2 as DropDownList).Enabled = true;
        //            }
        //            if (dsCampo3 != null)
        //            {
        //                (linha3 as TableRow).Visible = false;
        //            }
        //            if (dsCampo4 != null)
        //            {
        //                (linha4 as TableRow).Visible = false;
        //            }
        //            if (linha41 != null)
        //            {
        //                (linha41 as TableRow).Visible = false;
        //            }
        //            if (linha42 != null)
        //            {
        //                (linha42 as TableRow).Visible = false;
        //            }
        //            if (linha43 != null)
        //            {
        //                (linha43 as TableRow).Visible = false;
        //            }


        //            if (SessionParticipante.DsAuxiliar11 == "MUNICÍPIO")
        //            {
        //                if (dsCampo2 != null)
        //                {
        //                    (dsCampo2 as DropDownList).Items.Clear();
        //                    (dsCampo2 as DropDownList).Items.Add("");
        //                    (dsCampo2 as DropDownList).Items.Add("PREFEITO (A) MUNICIPAL");
        //                    (dsCampo2 as DropDownList).Items.Add("VICE-PREFEITO (A) MUNICIPAL");
        //                    (dsCampo2 as DropDownList).Items.Add("CONSELHEIRO MUNICIPAL DE SAÚDE");
        //                    (dsCampo2 as DropDownList).Items.Add("DIRETORES/COORDENADORES/GERENTES DE PROGRAMAS OU UNIDADES");
        //                    (dsCampo2 as DropDownList).Items.Add("PROFISSIONAIS DE SAÚDE");
        //                    (dsCampo2 as DropDownList).Items.Add("PROFISSIONAIS ADMINISTRATIVOS");
        //                    (dsCampo2 as DropDownList).Items.Add("OUTROS");

        //                    (dsCampo2 as DropDownList).Text = SessionParticipante.NoCargo;

        //                    (linha2 as TableRow).Visible = true;
        //                    (linha5 as TableRow).Visible = true;//uf
        //                    (linha6 as TableRow).Visible = true;//municipio

        //                    (linha3 as TableRow).Visible = false;//instituicao de origem                       
        //                }
        //            }
        //            else if (SessionParticipante.DsAuxiliar11 == "ESTADO")
        //            {
        //                if (dsCampo2 != null)
        //                {
        //                    (dsCampo2 as DropDownList).Items.Clear();
        //                    (dsCampo2 as DropDownList).Items.Add("");
        //                    (dsCampo2 as DropDownList).Items.Add("GOVERNADOR");
        //                    (dsCampo2 as DropDownList).Items.Add("VICE-GOVERNADOR");
        //                    (dsCampo2 as DropDownList).Items.Add("SECRETÁRIO (A) DE ESTADO DA SAÚDE");
        //                    (dsCampo2 as DropDownList).Items.Add("CONSELHEIRO ESTADUAL DE SAÚDE");
        //                    (dsCampo2 as DropDownList).Items.Add("DIRETORES/COORDENADORES/GERENTES DE PROGRAMAS OU UNIDADES");
        //                    (dsCampo2 as DropDownList).Items.Add("PROFISSIONAIS DE SAÚDE");
        //                    (dsCampo2 as DropDownList).Items.Add("PROFISSIONAIS ADMINISTRATIVOS");
        //                    (dsCampo2 as DropDownList).Items.Add("OUTROS");

        //                    (dsCampo2 as DropDownList).Text = SessionParticipante.NoCargo;

        //                    (linha2 as TableRow).Visible = true;
        //                    (linha5 as TableRow).Visible = true;//uf
        //                    (linha6 as TableRow).Visible = false;//municipio

        //                    (linha3 as TableRow).Visible = false;//instituicao de origem   

        //                }
        //            }
        //            else if (SessionParticipante.DsAuxiliar11 == "GOVERNO FEDERAL")
        //            {
        //                if (dsCampo2 != null)
        //                {
        //                    (dsCampo2 as DropDownList).Items.Clear();
        //                    (dsCampo2 as DropDownList).Items.Add("");
        //                    (dsCampo2 as DropDownList).Items.Add("SECRETÁRIO/DIRETOR/COORDENADOR/GERENTES DE PROGRAMAS OU UNIDADES");
        //                    (dsCampo2 as DropDownList).Items.Add("TÉCNICOS ESPECIALIZADOS");
        //                    (dsCampo2 as DropDownList).Items.Add("CONSULTORES");
        //                    (dsCampo2 as DropDownList).Items.Add("APOIADORES");
        //                    (dsCampo2 as DropDownList).Items.Add("PROFISSIONAIS DE SAÚDE");
        //                    (dsCampo2 as DropDownList).Items.Add("PROFISSIONAIS ADMINISTRATIVOS");
        //                    (dsCampo2 as DropDownList).Items.Add("OUTROS");

        //                    (dsCampo2 as DropDownList).Text = SessionParticipante.NoCargo;

        //                    (linha2 as TableRow).Visible = true;

        //                    (linha5 as TableRow).Visible = false;//uf
        //                    (linha6 as TableRow).Visible = false;//municipio

        //                    (linha3 as TableRow).Visible = true;//instituicao de origem   
        //                }
        //            }
        //            else if (SessionParticipante.DsAuxiliar11 == "PRIVADO")
        //            {
        //                (linha2 as TableRow).Visible = false;

        //                (linha3 as TableRow).Visible = false;//instituicao de origem   

        //                (linha5 as TableRow).Visible = false;//uf
        //                (linha6 as TableRow).Visible = false;//municipio

        //            }
        //        }
        //    }


           
        //    if ( ((SessionParticipante != null) && (SessionParticipante.CdParticipante == "") && (SessionChaveLibercao != null) && (SessionChaveLibercao != "")) ||
        //         ((SessionParticipante != null) && (SessionParticipante.CdParticipante != "") && (SessionParticipante.DsCampoExtraPreCad != "")))
        //    {
        //        string tmpDsCampoExtra = (((SessionChaveLibercao != null) && (SessionChaveLibercao != "")) ? SessionChaveLibercao : SessionParticipante.DsCampoExtraPreCad);
        //        ParticipantePreCadastro oParticipantePreCadastro = null;

        //        oParticipantePreCadastro = oParticipanteCad.PesquisarPreCadastroPorChave(SessionEvento.CdEvento, tmpDsCampoExtra, SessionCnn);

        //        if ((oParticipantePreCadastro != null) && (!oParticipantePreCadastro.FlPassagemAerea))
        //        {
        //            Control dsCampo30 = FindControlRecursive(this.Page, "tblinha_dsLabelSeparador4");
        //            Control dsCampo32 = FindControlRecursive(this.Page, "tblinha_dsPassagemAerea");

        //            Control dsCampo31 = FindControlRecursive(this.Page, "tblinha_dsLabelSeparador5");
        //            Control dsCampo15 = FindControlRecursive(this.Page, "tblinha_dsVooIda");
        //            Control dsCampo16 = FindControlRecursive(this.Page, "tblinha_dsVooLocalizadorIda");
        //            Control dsCampo1 = FindControlRecursive(this.Page, "tblinha_dsVooCiaAereaIda");
        //            Control dsCampo2 = FindControlRecursive(this.Page, "tblinha_dsVooOrigemIda");
        //            Control dsCampo3 = FindControlRecursive(this.Page, "tblinha_dsVooDestinoIda");
        //            Control dsCampo4 = FindControlRecursive(this.Page, "tblinha_dsVooDataSaidaIda");
        //            Control dsCampo5 = FindControlRecursive(this.Page, "tblinha_dsVooHoraSaidaIda");
        //            Control dsCampo6 = FindControlRecursive(this.Page, "tblinha_dsVooDataChegadaIda");
        //            Control dsCampo7 = FindControlRecursive(this.Page, "tblinha_dsVooHoraChegadaIda");

        //            Control dsCampo33 = FindControlRecursive(this.Page, "tblinha_dsLabelSeparador6");
        //            Control dsCampo17 = FindControlRecursive(this.Page, "tblinha_dsVooVolta");
        //            Control dsCampo18 = FindControlRecursive(this.Page, "tblinha_dsVooLocalizadorVolta");
        //            Control dsCampo8 = FindControlRecursive(this.Page, "tblinha_dsVooCiaAereaVolta");
        //            Control dsCampo9 = FindControlRecursive(this.Page, "tblinha_dsVooOrigemVolta");
        //            Control dsCampo10 = FindControlRecursive(this.Page, "tblinha_dsVooDestinoVolta");
        //            Control dsCampo11 = FindControlRecursive(this.Page, "tblinha_dsVooDataSaidaVolta");
        //            Control dsCampo12 = FindControlRecursive(this.Page, "tblinha_dsVooHoraSaidaVolta");
        //            Control dsCampo13 = FindControlRecursive(this.Page, "tblinha_dsVooDataChegadaVolta");
        //            Control dsCampo14 = FindControlRecursive(this.Page, "tblinha_dsVooHoraChegadaVolta");

                    
        //            if (dsCampo30 != null)
        //                (dsCampo30 as TableRow).Visible = false;
        //            if (dsCampo32 != null)
        //                (dsCampo32 as TableRow).Visible = false;
        //            if (dsCampo31 != null)
        //                (dsCampo31 as TableRow).Visible = false;

        //            if (dsCampo33 != null)
        //                (dsCampo33 as TableRow).Visible = false;

        //            if (dsCampo15 != null) 
        //                (dsCampo15 as TableRow).Visible = false;
        //            if (dsCampo16 != null)
        //                (dsCampo16 as TableRow).Visible = false;
                             
        //            if (dsCampo1 != null)
        //                (dsCampo1 as TableRow).Visible = false;
                    
        //            if (dsCampo2 != null)
        //                (dsCampo2 as TableRow).Visible = false;
                    
        //            if (dsCampo3 != null)
        //                (dsCampo3 as TableRow).Visible = false;
        //            if (dsCampo4 != null)
        //                (dsCampo4 as TableRow).Visible = false;
        //            if (dsCampo5 != null)
        //                (dsCampo5 as TableRow).Visible = false;
        //            if (dsCampo6 != null)
        //                (dsCampo6 as TableRow).Visible = false;
        //            if (dsCampo7 != null)
        //                (dsCampo7 as TableRow).Visible = false;
                        
        //            if (dsCampo17 != null)
        //                (dsCampo17 as TableRow).Visible = false;
        //            if (dsCampo18 != null)
        //                (dsCampo18 as TableRow).Visible = false;
                        
        //            if (dsCampo8 != null)
        //                (dsCampo8 as TableRow).Visible = false;
        //            if (dsCampo9 != null)
        //                (dsCampo9 as TableRow).Visible = false;
        //            if (dsCampo10 != null)
        //                (dsCampo10 as TableRow).Visible = false;
        //            if (dsCampo11 != null)
        //                (dsCampo11 as TableRow).Visible = false;
        //            if (dsCampo12 != null)
        //                (dsCampo12 as TableRow).Visible = false;
        //            if (dsCampo13 != null)
        //                (dsCampo13 as TableRow).Visible = false;
        //            if (dsCampo14 != null)
        //                (dsCampo14 as TableRow).Visible = false;
                    
                        
        //        }

        //    }

        //}
        #endregion


        #region CONASEMS 2014/2015
        //if ((SessionEvento.CdEvento == "001304") || (SessionEvento.CdEvento == "001305"))
        //{   
        //    if ((SessionParticipante != null) && (SessionParticipante.CdParticipante != ""))
        //    {
                
        //        //Control dsCampo5 = FindControlRecursive(this.Page, "cascaDD_dsAuxiliar8");//UF CARGO
        //        //Control linha5 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar8");
        //        //Control dsCampo6 = FindControlRecursive(this.Page, "cascaDD_dsAuxiliar9");//MUNICIPIO CARGO
        //        //Control linha6 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar9");
                                
        //        //if (SessionParticipante.NoAreaAtuacao == "SIM") //SE É SECRETÁRIO MUNICIPAL SAUDE
        //        //{

        //        //    (linha5 as TableRow).Visible = true;//uf
        //        //    (linha6 as TableRow).Visible = true;//municipio

                    
        //        //}
        //        //else //if (SessionParticipante.NoAreaAtuacao == "NÃO")
        //        //{

        //        //    (linha5 as TableRow).Visible = false;//uf
        //        //    (linha6 as TableRow).Visible = false;//municipio
                    
        //        //}
                
            
            

        //        if ((!SessionParticipante.Categoria.FlVerificarPreCadastro) && (SessionParticipante.Categoria.TpVerificacaoPreCadastro != "4"))
        //        {

        //            Control dsChave = FindControlRecursive(this.Page, SessionParticipante.Categoria.DsPreCadastroCampoPadraoCadastro);
        //            if (dsChave != null)
        //            {
        //                (dsChave as TextBox).Text = "";
        //                (dsChave as TextBox).Enabled = false;
        //            }

        //            Control dsLin = FindControlRecursive(this.Page, SessionParticipante.Categoria.DsPreCadastroCampoPadraoCadastro.Replace("txt","tblinha"));
        //            if (dsLin != null)
        //            {
        //                (dsLin as TableRow).Visible = false;
        //            }

        //            Control dsLin2 = FindControlRecursive(this.Page, "tblinha_dsCampoExtraPreCad");
        //            if (dsLin2 != null)
        //            {
        //                (dsLin2 as TableRow).Visible = false;
        //            }
        //        }
        //        else
        //        {

        //            Control dsCateg = FindControlRecursive(this.Page, "txt_cdCategoria");
        //            if (dsCateg != null)
        //            {

        //                (dsCateg as DropDownList).Enabled = false;// = "teste2";
        //            }

        //            if ((SessionParticipante.DsCampoExtraPreCad == null) || (SessionParticipante.DsCampoExtraPreCad == ""))
        //            {
        //                Control dsChave = FindControlRecursive(this.Page, SessionParticipante.Categoria.DsPreCadastroCampoPadraoCadastro);
        //                if (dsChave != null)
        //                {

        //                    (dsChave as TextBox).Enabled = true;
        //                }

        //                Control dsLin = FindControlRecursive(this.Page, SessionParticipante.Categoria.DsPreCadastroCampoPadraoCadastro.Replace("txt", "tblinha"));
        //                if (dsLin != null)
        //                {
        //                    (dsLin as TableRow).Visible = true;
        //                }
        //            }
        //            else
        //            {
        //                Control dsChave = FindControlRecursive(this.Page, SessionParticipante.Categoria.DsPreCadastroCampoPadraoCadastro);
        //                if (dsChave != null)
        //                {

        //                    (dsChave as TextBox).Enabled = false;
        //                }

        //                Control dsLin = FindControlRecursive(this.Page, SessionParticipante.Categoria.DsPreCadastroCampoPadraoCadastro.Replace("txt", "tblinha"));
        //                if (dsLin != null)
        //                {
        //                    (dsLin as TableRow).Visible = true;
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if ((SessionCateg == null) || (SessionCateg == ""))
        //        {
        //            Control dsLin = FindControlRecursive(this.Page, "tblinha_dsCampoExtraPreCad");
        //            if (dsLin != null)
        //            {
        //                (dsLin as TableRow).Visible = false;
        //            }
        //        }
        //        else
        //        {
        //            Categoria tmpCateg = oCategoriaCad.Pesquisar(SessionEvento.CdEvento, SessionCateg, SessionCnn);

        //            if ((tmpCateg != null) && (tmpCateg.FlVerificarPreCadastro) && (tmpCateg.TpVerificacaoPreCadastro == "4"))
        //            {
        //                Control dsLin = FindControlRecursive(this.Page, "tblinha_dsCampoExtraPreCad");
        //                if (dsLin != null)
        //                {
        //                    (dsLin as TableRow).Visible = true;
        //                }
        //            }
        //            else
        //            {
        //                Control dsLin = FindControlRecursive(this.Page, "tblinha_dsCampoExtraPreCad");
        //                if (dsLin != null)
        //                {
        //                    (dsLin as TableRow).Visible = false;
        //                }
        //            }
        //        }
        //    }

        //    if (((SessionParticipante != null) && (SessionParticipante.CdParticipante == "") && (SessionChaveLibercao != null) && (SessionChaveLibercao != "")) ||
        //        ((SessionParticipante != null) && (SessionParticipante.CdParticipante != "") && (SessionParticipante.DsCampoExtraPreCad != "")) ||
        //        ((SessionParticipante != null) && (SessionParticipante.CdParticipante != "") && (SessionParticipante.DsPassagemAerea == "SIM"))
        //        )
        //    {
        //        string tmpDsCampoExtra = (((SessionChaveLibercao != null) && (SessionChaveLibercao != "")) ? SessionChaveLibercao : SessionParticipante.DsCampoExtraPreCad);
        //        ParticipantePreCadastro oParticipantePreCadastro = null;

        //        oParticipantePreCadastro = oParticipanteCad.PesquisarPreCadastroPorChave(SessionEvento.CdEvento, tmpDsCampoExtra, SessionCnn);

                
        //        if (((oParticipantePreCadastro != null) && (!oParticipantePreCadastro.FlPassagemAerea)) ||
        //            ((SessionParticipante != null) && (SessionParticipante.DsPassagemAerea != "SIM"))
        //            )
        //        {
        //            Control dsCampo30 = FindControlRecursive(this.Page, "tblinha_dsLabelSeparador4");
        //            Control dsCampo32 = FindControlRecursive(this.Page, "tblinha_dsPassagemAerea");

        //            Control dsCampo31 = FindControlRecursive(this.Page, "tblinha_dsLabelSeparador5");
        //            Control dsCampo15 = FindControlRecursive(this.Page, "tblinha_dsVooIda");
        //            Control dsCampo16 = FindControlRecursive(this.Page, "tblinha_dsVooLocalizadorIda");
        //            Control dsCampo1 = FindControlRecursive(this.Page, "tblinha_dsVooCiaAereaIda");
        //            Control dsCampo2 = FindControlRecursive(this.Page, "tblinha_dsVooOrigemIda");
        //            Control dsCampo3 = FindControlRecursive(this.Page, "tblinha_dsVooDestinoIda");
        //            Control dsCampo4 = FindControlRecursive(this.Page, "tblinha_dsVooDataSaidaIda");
        //            Control dsCampo5 = FindControlRecursive(this.Page, "tblinha_dsVooHoraSaidaIda");
        //            Control dsCampo6 = FindControlRecursive(this.Page, "tblinha_dsVooDataChegadaIda");
        //            Control dsCampo7 = FindControlRecursive(this.Page, "tblinha_dsVooHoraChegadaIda");

        //            Control dsCampo33 = FindControlRecursive(this.Page, "tblinha_dsLabelSeparador6");
        //            Control dsCampo17 = FindControlRecursive(this.Page, "tblinha_dsVooVolta");
        //            Control dsCampo18 = FindControlRecursive(this.Page, "tblinha_dsVooLocalizadorVolta");
        //            Control dsCampo8 = FindControlRecursive(this.Page, "tblinha_dsVooCiaAereaVolta");
        //            Control dsCampo9 = FindControlRecursive(this.Page, "tblinha_dsVooOrigemVolta");
        //            Control dsCampo10 = FindControlRecursive(this.Page, "tblinha_dsVooDestinoVolta");
        //            Control dsCampo11 = FindControlRecursive(this.Page, "tblinha_dsVooDataSaidaVolta");
        //            Control dsCampo12 = FindControlRecursive(this.Page, "tblinha_dsVooHoraSaidaVolta");
        //            Control dsCampo13 = FindControlRecursive(this.Page, "tblinha_dsVooDataChegadaVolta");
        //            Control dsCampo14 = FindControlRecursive(this.Page, "tblinha_dsVooHoraChegadaVolta");


        //            if (dsCampo30 != null)
        //                (dsCampo30 as TableRow).Visible = false;
        //            if (dsCampo32 != null)
        //                (dsCampo32 as TableRow).Visible = false;
        //            if (dsCampo31 != null)
        //                (dsCampo31 as TableRow).Visible = false;

        //            if (dsCampo33 != null)
        //                (dsCampo33 as TableRow).Visible = false;

        //            if (dsCampo15 != null)
        //                (dsCampo15 as TableRow).Visible = false;
        //            if (dsCampo16 != null)
        //                (dsCampo16 as TableRow).Visible = false;

        //            if (dsCampo1 != null)
        //                (dsCampo1 as TableRow).Visible = false;

        //            if (dsCampo2 != null)
        //                (dsCampo2 as TableRow).Visible = false;

        //            if (dsCampo3 != null)
        //                (dsCampo3 as TableRow).Visible = false;
        //            if (dsCampo4 != null)
        //                (dsCampo4 as TableRow).Visible = false;
        //            if (dsCampo5 != null)
        //                (dsCampo5 as TableRow).Visible = false;
        //            if (dsCampo6 != null)
        //                (dsCampo6 as TableRow).Visible = false;
        //            if (dsCampo7 != null)
        //                (dsCampo7 as TableRow).Visible = false;

        //            if (dsCampo17 != null)
        //                (dsCampo17 as TableRow).Visible = false;
        //            if (dsCampo18 != null)
        //                (dsCampo18 as TableRow).Visible = false;

        //            if (dsCampo8 != null)
        //                (dsCampo8 as TableRow).Visible = false;
        //            if (dsCampo9 != null)
        //                (dsCampo9 as TableRow).Visible = false;
        //            if (dsCampo10 != null)
        //                (dsCampo10 as TableRow).Visible = false;
        //            if (dsCampo11 != null)
        //                (dsCampo11 as TableRow).Visible = false;
        //            if (dsCampo12 != null)
        //                (dsCampo12 as TableRow).Visible = false;
        //            if (dsCampo13 != null)
        //                (dsCampo13 as TableRow).Visible = false;
        //            if (dsCampo14 != null)
        //                (dsCampo14 as TableRow).Visible = false;


        //        }
        //        else if ((SessionParticipante != null) && (SessionParticipante.DsPassagemAerea == "SIM"))
        //        {

        //            Control dsCampo30 = FindControlRecursive(this.Page, "tblinha_dsVooIda");
        //            Control dsCampo31 = FindControlRecursive(this.Page, "tblinha_dsVooLocalizadorIda");

        //            Control dsCampo15 = FindControlRecursive(this.Page, "txt_dsVooIda");
        //            Control dsCampo16 = FindControlRecursive(this.Page, "txt_dsVooLocalizadorIda");

        //            Control dsCampo17 = FindControlRecursive(this.Page, "txt_dsVooVolta");
        //            Control dsCampo18 = FindControlRecursive(this.Page, "txt_dsVooLocalizadorVolta");

        //            Control dsCampo1 = FindControlRecursive(this.Page, "txt_dsVooCiaAereaIda");
        //            Control dsCampo2 = FindControlRecursive(this.Page, "txt_dsVooOrigemIda");
        //            Control dsCampo3 = FindControlRecursive(this.Page, "txt_dsVooDestinoIda");
        //            Control dsCampo4 = FindControlRecursive(this.Page, "txt_dsVooDataSaidaIda");
        //            Control dsCampo5 = FindControlRecursive(this.Page, "txt_dsVooHoraSaidaIda");
        //            Control dsCampo6 = FindControlRecursive(this.Page, "txt_dsVooDataChegadaIda");
        //            Control dsCampo7 = FindControlRecursive(this.Page, "txt_dsVooHoraChegadaIda");
        //            Control dsCampo8 = FindControlRecursive(this.Page, "txt_dsVooCiaAereaVolta");
        //            Control dsCampo9 = FindControlRecursive(this.Page, "txt_dsVooOrigemVolta");
        //            Control dsCampo10 = FindControlRecursive(this.Page, "txt_dsVooDestinoVolta");
        //            Control dsCampo11 = FindControlRecursive(this.Page, "txt_dsVooDataSaidaVolta");
        //            Control dsCampo12 = FindControlRecursive(this.Page, "txt_dsVooHoraSaidaVolta");
        //            Control dsCampo13 = FindControlRecursive(this.Page, "txt_dsVooDataChegadaVolta");
        //            Control dsCampo14 = FindControlRecursive(this.Page, "txt_dsVooHoraChegadaVolta");

        //            if (((dsCampo15 != null) && ((dsCampo15 as TextBox).Text == "")) &&
        //                 ((dsCampo16 != null) && ((dsCampo16 as TextBox).Text == "")))
        //            {
        //                if (dsCampo1 != null)
        //                {
        //                    (dsCampo1 as TextBox).Enabled = true;
        //                }
        //                if (dsCampo2 != null)
        //                {
        //                    (dsCampo2 as DropDownList).Enabled = true;
        //                }
        //                if (dsCampo3 != null)
        //                {
        //                    (dsCampo3 as DropDownList).Enabled = true;
        //                }
        //                if (dsCampo4 != null)
        //                {
        //                    (dsCampo4 as TextBox).Enabled = true;
        //                }
        //                if (dsCampo5 != null)
        //                {
        //                    (dsCampo5 as TextBox).Enabled = true;
        //                }
        //                if (dsCampo6 != null)
        //                {
        //                    (dsCampo6 as TextBox).Enabled = true;
        //                }
        //                if (dsCampo7 != null)
        //                {
        //                    (dsCampo7 as TextBox).Enabled = true;
        //                }
        //            }

        //            if (((dsCampo17 != null) && ((dsCampo17 as TextBox).Text == "")) &&
        //                 ((dsCampo18 != null) && ((dsCampo18 as TextBox).Text == "")))
        //            {
        //                if (dsCampo8 != null)
        //                {
        //                    (dsCampo8 as TextBox).Enabled = true;
        //                }
        //                if (dsCampo9 != null)
        //                {
        //                    (dsCampo9 as DropDownList).Enabled = true;
        //                }
        //                if (dsCampo10 != null)
        //                {
        //                    (dsCampo10 as DropDownList).Enabled = true;
        //                }
        //                if (dsCampo11 != null)
        //                {
        //                    (dsCampo11 as TextBox).Enabled = true;
        //                }
        //                if (dsCampo12 != null)
        //                {
        //                    (dsCampo12 as TextBox).Enabled = true;
        //                }
        //                if (dsCampo13 != null)
        //                {
        //                    (dsCampo13 as TextBox).Enabled = true;
        //                }
        //                if (dsCampo14 != null)
        //                {
        //                    (dsCampo14 as TextBox).Enabled = true;
        //                }
        //            }
        //        }

        //    }
        //    else
        //    {
        //        Control dsCampo30 = FindControlRecursive(this.Page, "tblinha_dsLabelSeparador4");
        //        Control dsCampo32 = FindControlRecursive(this.Page, "tblinha_dsPassagemAerea");

        //        Control dsCampo31 = FindControlRecursive(this.Page, "tblinha_dsLabelSeparador5");
        //        Control dsCampo15 = FindControlRecursive(this.Page, "tblinha_dsVooIda");
        //        Control dsCampo16 = FindControlRecursive(this.Page, "tblinha_dsVooLocalizadorIda");
        //        Control dsCampo1 = FindControlRecursive(this.Page, "tblinha_dsVooCiaAereaIda");
        //        Control dsCampo2 = FindControlRecursive(this.Page, "tblinha_dsVooOrigemIda");
        //        Control dsCampo3 = FindControlRecursive(this.Page, "tblinha_dsVooDestinoIda");
        //        Control dsCampo4 = FindControlRecursive(this.Page, "tblinha_dsVooDataSaidaIda");
        //        Control dsCampo5 = FindControlRecursive(this.Page, "tblinha_dsVooHoraSaidaIda");
        //        Control dsCampo6 = FindControlRecursive(this.Page, "tblinha_dsVooDataChegadaIda");
        //        Control dsCampo7 = FindControlRecursive(this.Page, "tblinha_dsVooHoraChegadaIda");

        //        Control dsCampo33 = FindControlRecursive(this.Page, "tblinha_dsLabelSeparador6");
        //        Control dsCampo17 = FindControlRecursive(this.Page, "tblinha_dsVooVolta");
        //        Control dsCampo18 = FindControlRecursive(this.Page, "tblinha_dsVooLocalizadorVolta");
        //        Control dsCampo8 = FindControlRecursive(this.Page, "tblinha_dsVooCiaAereaVolta");
        //        Control dsCampo9 = FindControlRecursive(this.Page, "tblinha_dsVooOrigemVolta");
        //        Control dsCampo10 = FindControlRecursive(this.Page, "tblinha_dsVooDestinoVolta");
        //        Control dsCampo11 = FindControlRecursive(this.Page, "tblinha_dsVooDataSaidaVolta");
        //        Control dsCampo12 = FindControlRecursive(this.Page, "tblinha_dsVooHoraSaidaVolta");
        //        Control dsCampo13 = FindControlRecursive(this.Page, "tblinha_dsVooDataChegadaVolta");
        //        Control dsCampo14 = FindControlRecursive(this.Page, "tblinha_dsVooHoraChegadaVolta");


        //        if (dsCampo30 != null)
        //            (dsCampo30 as TableRow).Visible = false;
        //        if (dsCampo32 != null)
        //            (dsCampo32 as TableRow).Visible = false;
        //        if (dsCampo31 != null)
        //            (dsCampo31 as TableRow).Visible = false;

        //        if (dsCampo33 != null)
        //            (dsCampo33 as TableRow).Visible = false;

        //        if (dsCampo15 != null)
        //            (dsCampo15 as TableRow).Visible = false;
        //        if (dsCampo16 != null)
        //            (dsCampo16 as TableRow).Visible = false;

        //        if (dsCampo1 != null)
        //            (dsCampo1 as TableRow).Visible = false;

        //        if (dsCampo2 != null)
        //            (dsCampo2 as TableRow).Visible = false;

        //        if (dsCampo3 != null)
        //            (dsCampo3 as TableRow).Visible = false;
        //        if (dsCampo4 != null)
        //            (dsCampo4 as TableRow).Visible = false;
        //        if (dsCampo5 != null)
        //            (dsCampo5 as TableRow).Visible = false;
        //        if (dsCampo6 != null)
        //            (dsCampo6 as TableRow).Visible = false;
        //        if (dsCampo7 != null)
        //            (dsCampo7 as TableRow).Visible = false;

        //        if (dsCampo17 != null)
        //            (dsCampo17 as TableRow).Visible = false;
        //        if (dsCampo18 != null)
        //            (dsCampo18 as TableRow).Visible = false;

        //        if (dsCampo8 != null)
        //            (dsCampo8 as TableRow).Visible = false;
        //        if (dsCampo9 != null)
        //            (dsCampo9 as TableRow).Visible = false;
        //        if (dsCampo10 != null)
        //            (dsCampo10 as TableRow).Visible = false;
        //        if (dsCampo11 != null)
        //            (dsCampo11 as TableRow).Visible = false;
        //        if (dsCampo12 != null)
        //            (dsCampo12 as TableRow).Visible = false;
        //        if (dsCampo13 != null)
        //            (dsCampo13 as TableRow).Visible = false;
        //        if (dsCampo14 != null)
        //            (dsCampo14 as TableRow).Visible = false;


        //    }
            

        //}
        #endregion

        #region CISPOD
        //if (SessionEvento.CdEvento == "004401")
        //{
        //    if ((SessionParticipante != null) && (SessionParticipante.CdParticipante != ""))
        //    {
        //        Control dsCampo1 = FindControlRecursive(this.Page, "txt_dsAuxiliar1");//Inscrito em Conselho profissional
        //        Control linha1 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar1");
        //        Control dsCampo2 = FindControlRecursive(this.Page, "txt_dsAuxiliar2");//Qual Conselho?
        //        Control linha2 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar2");
        //        Control dsCampo3 = FindControlRecursive(this.Page, "txt_noAreaAtuacao");//Area Atuacao
        //        Control linha3 = FindControlRecursive(this.Page, "tblinha_noAreaAtuacao");
        //        Control dsCampo4 = FindControlRecursive(this.Page, "txt_dsAuxiliar3");//Qual Area Atuacao
        //        Control linha4 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar3");

        //        Control dsCampo5 = FindControlRecursive(this.Page, "txt_dsAuxiliar4");//É uma pessoa com deficiência
        //        Control linha5 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar4");
        //        Control dsCampo6 = FindControlRecursive(this.Page, "txt_dsAuxiliar5");//Qual deficiência
        //        Control linha6 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar5");


        //        if (SessionParticipante.DsAuxiliar1 == "SIM") //Inscrito em Conselho profissional
        //        {
        //            if (dsCampo2 != null)
        //            {
        //                (linha2 as TableRow).Visible = true;//Qual Conselho?
        //            }
        //        }
        //        else
        //        {
        //            if (dsCampo2 != null)
        //            {
        //                (linha2 as TableRow).Visible = false;//Qual Conselho?
        //            }
        //        }

        //        if ((SessionParticipante.NoAreaAtuacao == "OUTRAS") || (SessionParticipante.NoAreaAtuacao == "OTHER")) //Area Atuacao
        //        {
        //            if (dsCampo4 != null)
        //            {
        //                (linha4 as TableRow).Visible = true;//Qual Area Atuacao
        //            }
        //        }
        //        else
        //        {
        //            if (dsCampo4 != null)
        //            {
        //                (linha4 as TableRow).Visible = false;//Qual Area Atuacao
        //            }
        //        }

        //        if ((SessionParticipante.DsAuxiliar4 == "SIM") || (SessionParticipante.DsAuxiliar4 == "YES")) //É uma pessoa com deficiência
        //        {
        //            if (dsCampo6 != null)
        //            {
        //                (linha6 as TableRow).Visible = true;//Qual deficiência
        //            }
        //        }
        //        else
        //        {
        //            if (dsCampo6 != null)
        //            {
        //                (linha6 as TableRow).Visible = false;//Qual deficiência
        //            }
        //        }
                
        //        //****** categoria: REPRESENTANTES e  RESPONSÁVEIS ****
        //        Control dsCampo41 = FindControlRecursive(this.Page, "txt_dsAuxiliar8");//Responsável por pessoa com defeiciência?
        //        Control linha41 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar8");

        //        Control dsCampo42 = FindControlRecursive(this.Page, "txt_dsAuxiliar9");//Representante de Entidade de Assistência?
        //        Control linha42 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar9");

        //        Control dsCampo43 = FindControlRecursive(this.Page, "txt_dsAuxiliar10");//Informe qual entidade representa
        //        Control linha43 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar10");

        //        if (SessionParticipante.CdCategoria != "00440105") //categoria: REPRESENTANTES e  RESPONSÁVEIS
        //        {
        //            if (linha41 != null)
        //            {
        //                (linha41 as TableRow).Visible = false;
        //            }
        //            if (linha42 != null)
        //            {
        //                (linha42 as TableRow).Visible = false;
        //            }
        //            if (linha43 != null)
        //            {
        //                (linha43 as TableRow).Visible = false;
        //            }
        //        }
        //        else
        //        {
        //            if (linha41 != null)
        //            {
        //                (linha41 as TableRow).Visible = true;
        //            }
        //            if (linha42 != null)
        //            {
        //                (linha42 as TableRow).Visible = true;
        //            }
        //            if (linha43 != null)
        //            {
        //                if ((SessionParticipante.DsAuxiliar9 == "SIM") || (SessionParticipante.DsAuxiliar9 == "YES"))
        //                    (linha43 as TableRow).Visible = true;
        //                else
        //                    (linha43 as TableRow).Visible = false;
        //            }
        //        }

        //        Control dsCampo45 = FindControlRecursive(this.Page, "txt_dsCampoExtraPreCad");//Informe qual entidade representa
        //        Control linha45 = FindControlRecursive(this.Page, "tblinha_dsCampoExtraPreCad");

        //        if (SessionParticipante.CdCategoria != "00440106") //categoria: REPRESENTANTES e  RESPONSÁVEIS
        //        {
        //            if (linha45 != null)
        //            {
        //                (linha45 as TableRow).Visible = false;
        //            }
        //        }
        //        else
        //        {
        //            if (linha45 != null)
        //            {
        //                (linha45 as TableRow).Visible = true;
        //            }
        //        }

        //        //*****
                  
        //    }
        //    else
        //    {
        //        //****** categoria: REPRESENTANTES e  RESPONSÁVEIS ****
        //        Control dsCampo41 = FindControlRecursive(this.Page, "txt_dsAuxiliar8");//Responsável por pessoa com defeiciência?
        //        Control linha41 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar8");

        //        Control dsCampo42 = FindControlRecursive(this.Page, "txt_dsAuxiliar9");//Representante de Entidade de Assistência?
        //        Control linha42 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar9");

        //        Control dsCampo43 = FindControlRecursive(this.Page, "txt_dsAuxiliar10");//Informe qual entidade representa
        //        Control linha43 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar10");

        //        if (SessionCateg != "00440105") //categoria: REPRESENTANTES e  RESPONSÁVEIS
        //        {
        //            if (linha41 != null)
        //            {
        //                (dsCampo41 as DropDownList).Visible = false; 
        //                (linha41 as TableRow).Visible = false;
        //            }
        //            if (linha42 != null)
        //            {
        //                (dsCampo42 as DropDownList).Visible = false;
        //                (linha42 as TableRow).Visible = false;
        //            }
        //            if (linha43 != null)
        //            {
        //                (dsCampo43 as TextBox).Visible = false;
        //                (linha43 as TableRow).Visible = false;
        //            }
        //        }
        //        else
        //        {
        //            if (linha41 != null)
        //            {
        //                (dsCampo41 as DropDownList).Visible = true;
        //                (linha41 as TableRow).Visible = true;
        //            }
        //            if (linha42 != null)
        //            {
        //                (dsCampo42 as DropDownList).Visible = true;
        //                (linha42 as TableRow).Visible = true;
        //            }
        //            if (linha43 != null)
        //            {
        //                (dsCampo43 as TextBox).Visible = true;
        //                (linha43 as TableRow).Visible = true;
        //            }
        //        }

        //        Control dsCampo45 = FindControlRecursive(this.Page, "txt_dsCampoExtraPreCad");//Informe qual entidade representa
        //        Control linha45 = FindControlRecursive(this.Page, "tblinha_dsCampoExtraPreCad");

        //        if (SessionCateg != "00440106") //categoria: REPRESENTANTES e  RESPONSÁVEIS
        //        {
        //            if (linha45 != null)
        //            {
        //                (linha45 as TableRow).Visible = false;
        //            }
        //        }
        //        else
        //        {
        //            if (linha45 != null)
        //            {
        //                (linha45 as TableRow).Visible = true;
        //            }
        //        }
            
        //        //*****
        //    }

        //}
        #endregion

        #region PRB
        //if (SessionEvento.CdCliente == "0048")
        //{
        //    if (((SessionParticipante != null) && (SessionParticipante.CdParticipante == "")) ||
        //        ((SessionParticipante != null) && (SessionParticipante.CdParticipante != "")))// && (SessionParticipante.DsPassagemAerea.ToUpper() != "AÉREA")))
                
        //       // ((sender as DropDownList).ID == "txt_dsPassagemAerea")
        //    {

        //        Control dslCampo31 = FindControlRecursive(this.Page, "tblinha_dsLabelSeparador5");
        //        Control dslCampo15 = FindControlRecursive(this.Page, "tblinha_dsVooIda");
        //        Control dslCampo16 = FindControlRecursive(this.Page, "tblinha_dsVooLocalizadorIda");
        //        Control dslCampo1 = FindControlRecursive(this.Page, "tblinha_dsVooCiaAereaIda");
        //        Control dslCampo2 = FindControlRecursive(this.Page, "tblinha_dsVooOrigemIda");
        //        Control dslCampo3 = FindControlRecursive(this.Page, "tblinha_dsVooDestinoIda");
        //        Control dslCampo4 = FindControlRecursive(this.Page, "tblinha_dsVooDataSaidaIda");
        //        Control dslCampo5 = FindControlRecursive(this.Page, "tblinha_dsVooHoraSaidaIda");
        //        Control dslCampo6 = FindControlRecursive(this.Page, "tblinha_dsVooDataChegadaIda");
        //        Control dslCampo7 = FindControlRecursive(this.Page, "tblinha_dsVooHoraChegadaIda");

        //        Control dslCampo33 = FindControlRecursive(this.Page, "tblinha_dsLabelSeparador6");
        //        Control dslCampo17 = FindControlRecursive(this.Page, "tblinha_dsVooVolta");
        //        Control dslCampo18 = FindControlRecursive(this.Page, "tblinha_dsVooLocalizadorVolta");
        //        Control dslCampo8 = FindControlRecursive(this.Page, "tblinha_dsVooCiaAereaVolta");
        //        Control dslCampo9 = FindControlRecursive(this.Page, "tblinha_dsVooOrigemVolta");
        //        Control dslCampo10 = FindControlRecursive(this.Page, "tblinha_dsVooDestinoVolta");
        //        Control dslCampo11 = FindControlRecursive(this.Page, "tblinha_dsVooDataSaidaVolta");
        //        Control dslCampo12 = FindControlRecursive(this.Page, "tblinha_dsVooHoraSaidaVolta");
        //        Control dslCampo13 = FindControlRecursive(this.Page, "tblinha_dsVooDataChegadaVolta");
        //        Control dslCampo14 = FindControlRecursive(this.Page, "tblinha_dsVooHoraChegadaVolta");


        //        if (SessionParticipante.DsPassagemAerea.ToUpper() == "AÉREA")
        //        {
        //            if (dslCampo33 != null)
        //                (dslCampo33 as TableRow).Visible = true;
        //            if (dslCampo31 != null)
        //                (dslCampo31 as TableRow).Visible = true;



        //            if (dslCampo15 != null)
        //                (dslCampo15 as TableRow).Visible = true;
        //            if (dslCampo16 != null)
        //                (dslCampo16 as TableRow).Visible = true;

        //            if (dslCampo1 != null)
        //                (dslCampo1 as TableRow).Visible = true;

        //            if (dslCampo2 != null)
        //                (dslCampo2 as TableRow).Visible = true;

        //            if (dslCampo3 != null)
        //                (dslCampo3 as TableRow).Visible = true;
        //            if (dslCampo4 != null)
        //                (dslCampo4 as TableRow).Visible = true;
        //            if (dslCampo5 != null)
        //                (dslCampo5 as TableRow).Visible = true;
        //            if (dslCampo6 != null)
        //                (dslCampo6 as TableRow).Visible = true;
        //            if (dslCampo7 != null)
        //                (dslCampo7 as TableRow).Visible = true;

        //            if (dslCampo17 != null)
        //                (dslCampo17 as TableRow).Visible = true;
        //            if (dslCampo18 != null)
        //                (dslCampo18 as TableRow).Visible = true;

        //            if (dslCampo8 != null)
        //                (dslCampo8 as TableRow).Visible = true;
        //            if (dslCampo9 != null)
        //                (dslCampo9 as TableRow).Visible = true;
        //            if (dslCampo10 != null)
        //                (dslCampo10 as TableRow).Visible = true;
        //            if (dslCampo11 != null)
        //                (dslCampo11 as TableRow).Visible = true;
        //            if (dslCampo12 != null)
        //                (dslCampo12 as TableRow).Visible = true;
        //            if (dslCampo13 != null)
        //                (dslCampo13 as TableRow).Visible = true;
        //            if (dslCampo14 != null)
        //                (dslCampo14 as TableRow).Visible = true;

        //        }
        //        else
        //        {
        //            if (dslCampo31 != null)
        //                (dslCampo31 as TableRow).Visible = false;
        //            if (dslCampo33 != null)
        //                (dslCampo33 as TableRow).Visible = false;

        //            if (dslCampo15 != null)
        //                (dslCampo15 as TableRow).Visible = false;
        //            if (dslCampo16 != null)
        //                (dslCampo16 as TableRow).Visible = false;

        //            if (dslCampo1 != null)
        //                (dslCampo1 as TableRow).Visible = false;

        //            if (dslCampo2 != null)
        //                (dslCampo2 as TableRow).Visible = false;

        //            if (dslCampo3 != null)
        //                (dslCampo3 as TableRow).Visible = false;
        //            if (dslCampo4 != null)
        //                (dslCampo4 as TableRow).Visible = false;
        //            if (dslCampo5 != null)
        //                (dslCampo5 as TableRow).Visible = false;
        //            if (dslCampo6 != null)
        //                (dslCampo6 as TableRow).Visible = false;
        //            if (dslCampo7 != null)
        //                (dslCampo7 as TableRow).Visible = false;

        //            if (dslCampo17 != null)
        //                (dslCampo17 as TableRow).Visible = false;
        //            if (dslCampo18 != null)
        //                (dslCampo18 as TableRow).Visible = false;

        //            if (dslCampo8 != null)
        //                (dslCampo8 as TableRow).Visible = false;
        //            if (dslCampo9 != null)
        //                (dslCampo9 as TableRow).Visible = false;
        //            if (dslCampo10 != null)
        //                (dslCampo10 as TableRow).Visible = false;
        //            if (dslCampo11 != null)
        //                (dslCampo11 as TableRow).Visible = false;
        //            if (dslCampo12 != null)
        //                (dslCampo12 as TableRow).Visible = false;
        //            if (dslCampo13 != null)
        //                (dslCampo13 as TableRow).Visible = false;
        //            if (dslCampo14 != null)
        //                (dslCampo14 as TableRow).Visible = false;
        //        }
        //    }
        //}
        #endregion


        #region MDA - CONDRAF
        //if (SessionEvento.CdEvento == "004501")
        //{
        //    if ((SessionParticipante != null) && (SessionParticipante.CdParticipante != ""))
        //    {

        //        Control dsCampo1 = FindControlRecursive(this.Page, "txt_dsAuxiliar4");//Sociedade Civil
        //        Control linha1 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar4");
        //        Control dsCampo2 = FindControlRecursive(this.Page, "txt_dsAuxiliar5");//Sociedade Civil - Outros
        //        Control linha2 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar5");

        //        Control dsCampo3 = FindControlRecursive(this.Page, "txt_dsAuxiliar6");//GOVERNO
        //        Control linha3 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar6");
        //        Control dsCampo4 = FindControlRecursive(this.Page, "txt_dsAuxiliar7");//GOVERNO - ORGAO
        //        Control linha4 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar7");

        //        if (SessionParticipante.DsAuxiliar3.ToUpper() == "SOCIEDADE CIVIL")
        //        {
        //            if (dsCampo1 != null)
        //            {
        //                (linha1 as TableRow).Visible = true;//Sociedade Civil
        //                if ((dsCampo1 as DropDownList).Text.ToUpper() == "OUTRO")
        //                    (linha2 as TableRow).Visible = true;//Sociedade Civil - Outros
        //                else
        //                    (linha2 as TableRow).Visible = false;//Sociedade Civil - Outros

        //                (linha3 as TableRow).Visible = false;//GOVERNO
        //                (linha4 as TableRow).Visible = false;//GOVERNO - ORGAO
        //            }
        //        }
        //        else if (SessionParticipante.DsAuxiliar3.ToUpper() == "SETOR PÚBLICO")
        //        {
        //            if (dsCampo2 != null)
        //            {
        //                (linha1 as TableRow).Visible = false;//Sociedade Civil
        //                (linha2 as TableRow).Visible = false;//Sociedade Civil - Outros

        //                (linha3 as TableRow).Visible = true;//GOVERNO
        //                (linha4 as TableRow).Visible = true;//GOVERNO - ORGAO
        //            }
        //        }
        //        else
        //        {

        //            (linha1 as TableRow).Visible = false;//Sociedade Civil
        //            (linha2 as TableRow).Visible = false;//Sociedade Civil - Outros

        //            (linha3 as TableRow).Visible = false;//GOVERNO
        //            (linha4 as TableRow).Visible = false;//GOVERNO - ORGAO

        //        }

        //        if (SessionParticipante.DsAuxiliar4.ToUpper() == "OUTRO")
        //        {
        //            if (dsCampo2 != null)
        //            {
        //                (linha2 as TableRow).Visible = true;//Sociedade Civil - Outros
        //            }
        //        }
        //        else
        //        {
        //            if (dsCampo2 != null)
        //            {
        //                (linha2 as TableRow).Visible = false;//Sociedade Civil - Outros

        //            }
        //        }



        //        Control dsCampo5 = FindControlRecursive(this.Page, "txt_dsAuxiliar10");//Categoria de Participação - Outra
        //        Control linha5 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar10");

        //        if (SessionParticipante.DsAuxiliar9.ToUpper() == "OUTRA")
        //        {
        //            if (dsCampo5 != null)
        //            {
        //                (linha5 as TableRow).Visible = true;//Categoria de Participação - Outros
        //            }
        //        }
        //        else
        //        {
        //            if (dsCampo5 != null)
        //            {
        //                (linha5 as TableRow).Visible = false;//Categoria de Participação - Outros

        //            }
        //        }

        //    }
        //    else
        //    {
        //        Control dsCampo0 = FindControlRecursive(this.Page, "txt_dsAuxiliar3");//Representatividade

        //        Control dsCampo1 = FindControlRecursive(this.Page, "txt_dsAuxiliar4");//Sociedade Civil
        //        Control linha1 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar4");
        //        Control dsCampo2 = FindControlRecursive(this.Page, "txt_dsAuxiliar5");//Sociedade Civil - Outros
        //        Control linha2 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar5");

        //        Control dsCampo3 = FindControlRecursive(this.Page, "txt_dsAuxiliar6");//GOVERNO
        //        Control linha3 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar6");
        //        Control dsCampo4 = FindControlRecursive(this.Page, "txt_dsAuxiliar7");//GOVERNO - ORGAO
        //        Control linha4 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar7");

        //        Control dsCampo9 = FindControlRecursive(this.Page, "txt_dsAuxiliar9");//Categoria de Participação
        //        Control dsCampo5 = FindControlRecursive(this.Page, "txt_dsAuxiliar10");//Categoria de Participação == outra
        //        Control linha5 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar10");

        //        if ((dsCampo0 as DropDownList).Text.ToUpper() == "")
        //        {
        //            if (linha1 != null)
        //            {
        //                linha1.Visible = false;
        //            }
        //            if (linha2 != null)
        //            {
        //                linha2.Visible = false;
        //            }
        //            if (linha3 != null)
        //            {
        //                linha3.Visible = false;
        //            }
        //            if (linha4 != null)
        //            {
        //                linha4.Visible = false;
        //            }
        //        }


        //        if ((dsCampo9 as DropDownList).Text.ToUpper() == "")
        //        {
        //            if (linha5 != null)
        //            {
        //                linha5.Visible = false;
        //            }
        //        }


        //    }
        //}
        #endregion

        #region IESB
        //if (SessionEvento.CdEvento == "005701")
        //{

        //    if (((SessionParticipante != null) && (SessionParticipante.CdParticipante == "")) ||
        //        ((SessionParticipante != null) && (SessionParticipante.CdParticipante != "")))            
        //    {

        //        Control dsCampo1 = FindControlRecursive(this.Page, "txt_dsAuxiliar2");//Escola outra
        //        Control lblCamp1 = FindControlRecursive(this.Page, "lbl_dsAuxiliar2");

        //        if ((SessionParticipante.NoInstituicao.ToUpper() == "OUTRO") || (SessionParticipante.NoInstituicao.ToUpper() == "OUTRA")) //Escola
        //        {
        //            if (dsCampo1 != null)
        //            {
        //                (dsCampo1 as TextBox).Visible = true;//ESCOLA NÃO ESTÁ NA LISTA 
        //                (lblCamp1 as Label).Visible = true;//ESCOLA NÃO ESTÁ NA LISTA 
        //            }
        //        }
        //        else
        //        {
        //            if (dsCampo1 != null)
        //            {
        //                (dsCampo1 as TextBox).Visible = false;//ESCOLA ESTÁ NA LISTA
        //                (lblCamp1 as Label).Visible = false;//ESCOLA ESTÁ NA LISTA
        //            }
        //        }
                
        //        Control dsCampo2 = FindControlRecursive(this.Page, "txt_dsAuxiliar3");//Série outra
        //        Control lblCamp2 = FindControlRecursive(this.Page, "lbl_dsAuxiliar3");

        //        if ((SessionParticipante.DsAuxiliar1.ToUpper() == "OUTRO") || (SessionParticipante.DsAuxiliar1.ToUpper() == "OUTRA"))//Série
        //        {
        //            if (dsCampo2 != null)
        //            {
        //                (dsCampo2 as TextBox).Visible = false;//SÉRIE NÃO ESTÁ NA LISTA
        //                (lblCamp2 as Label).Visible = false;//SÉRIE NÃO ESTÁ NA LISTA
        //            }
        //        }
        //        else
        //        {
        //            if (dsCampo2 != null)
        //            {
        //                (dsCampo2 as TextBox).Visible = false;//SÉRIE ESTÁ NA LISTA
        //                (lblCamp2 as Label).Visible = false;//SÉRIE ESTÁ NA LISTA
        //            }
        //        }
                
        //    }

        //}

        #endregion

        #region NTU2016
        //if (SessionEvento.CdEvento == "005503")
        //{
        //    if (((SessionParticipante != null) && (SessionParticipante.CdParticipante == "")) ||
        //        ((SessionParticipante != null) && (SessionParticipante.CdParticipante != "")))
        //    {
                
                
        //            Control dsRazaoSocial = FindControlRecursive(this.Page, "txt_noInstituicao");
        //            Control lnRazaoSocial = FindControlRecursive(this.Page, "tblinha_noInstituicao");


        //            Control dsChave = FindControlRecursive(this.Page, "txt_dsCampoExtraPreCad");
        //            Control dsLin = FindControlRecursive(this.Page, "tblinha_dsCampoExtraPreCad");
        //            Control lbldsChave = FindControlRecursive(this.Page, "lbl_dsCampoExtraPreCad");



        //            Control dsExpositora = FindControlRecursive(this.Page, "txt_dsAuxiliar1");
        //            Control lnExpositora = FindControlRecursive(this.Page, "tblinha_dsAuxiliar1");



        //            if ((SessionParticipante.CdCategoria == "00550301") || (SessionParticipante.CdCategoria == "00550302") ||
        //                (SessionParticipante.CdCategoria == "00550303") || (SessionParticipante.CdCategoria == "00550304"))
        //            {

        //                if (SessionParticipante.CdCategoria == "00550301")
        //                    (lbldsChave as Label).Text = "CNPJ da associada *";
        //                else if (SessionParticipante.CdCategoria == "00550302")
        //                    (lbldsChave as Label).Text = "CNPJ de não associadas *";
        //                else if (SessionParticipante.CdCategoria == "00550303")
        //                    (lbldsChave as Label).Text = "CNPJ do fornecedor *";


        //                if (lnRazaoSocial != null)
        //                {
        //                    (lnRazaoSocial as TableRow).Visible = true;
        //                }

        //                if (SessionParticipante.CdCategoria != "00550304")
        //                {
        //                    if (dsLin != null)
        //                    {
        //                        (dsLin as TableRow).Visible = true;
        //                    }
        //                }
        //                else
        //                {
        //                    if (dsLin != null)
        //                    {
        //                        (dsLin as TableRow).Visible = false;
        //                    }
        //                }


        //                if ((SessionParticipante.CdCategoria == "00550301") || (SessionParticipante.CdCategoria == "00550302"))
        //                {
        //                    if (lnRazaoSocial != null)
        //                        lnExpositora.Visible = true;
        //                }
        //                else
        //                {
        //                    if (lnRazaoSocial != null)
        //                        lnExpositora.Visible = false;
        //                }
        //            }
        //            else
        //            {
        //                if (lnRazaoSocial != null)
        //                {
        //                    (lnRazaoSocial as TableRow).Visible = false;
        //                }

        //                //if (dsChave != null)
        //                //{
        //                //    (dsChave as TextBox).Text = "";
        //                //    (dsChave as TextBox).Enabled = false;
        //                //}

        //                if (dsLin != null)
        //                {
        //                    (dsLin as TableRow).Visible = false;
        //                }
        //            }

                
        //    }
        //}
        #endregion

        #region SICOOB
        if (SessionEvento.CdEvento == "008303")
        {
            if (((SessionParticipante != null) && (SessionParticipante.CdParticipante == "")) ||
                ((SessionParticipante != null) && (SessionParticipante.CdParticipante != "")))
            {
                Control dsCampo1 = FindControlRecursive(this.Page, "txt_dsAuxiliar2");//cooperativa singular?
                Control linha1 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar2");
                Control dsLblCampo1 = FindControlRecursive(this.Page, "lbl_dsAuxiliar2");

                Control dsCampo3 = FindControlRecursive(this.Page, "txt_dsAuxiliar1");//cooperativa singular?
                Control linha3 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar1");
                Control dsLblCampo3 = FindControlRecursive(this.Page, "lbl_dsAuxiliar1");

                Control dsCampo2 = FindControlRecursive(this.Page, "txt_noInstituicao");//instituição
                Control linha2 = FindControlRecursive(this.Page, "tblinha_noInstituicao");
                Control dsLblCampo2 = FindControlRecursive(this.Page, "lbl_noInstituicao");

                if (SessionParticipante.CdCategoria == "00830302") //categoria: cooperativa singular?
                {
                    if (linha1 != null)
                    {
                        (dsCampo1 as DropDownList).Visible = true;
                        (linha1 as TableRow).Visible = true;
                        (dsLblCampo1 as Label).Visible = true;

                        (dsCampo3 as DropDownList).Visible = true;
                        (linha3 as TableRow).Visible = true;
                        (dsLblCampo3 as Label).Visible = true;

                        (dsCampo2 as TextBox).Visible = false;
                        (linha2 as TableRow).Visible = false;
                        (dsLblCampo2 as Label).Visible = false;
                    }
                }
                else
                {
                    if (linha1 != null)
                    {
                        (dsCampo1 as DropDownList).Visible = false;
                        (linha1 as TableRow).Visible = false;
                        (dsLblCampo1 as Label).Visible = false;

                        (dsCampo2 as TextBox).Visible = false;
                        (linha2 as TableRow).Visible = false;
                        (dsLblCampo2 as Label).Visible = false;
                    }
                }

                if ((SessionParticipante.CdCategoria == "00830308") || (SessionParticipante.CdCategoria == "00830303"))
                {
                    (dsCampo1 as DropDownList).Visible = false;
                    (linha1 as TableRow).Visible = false;
                    (dsLblCampo1 as Label).Visible = false;

                    (dsCampo3 as DropDownList).Visible = false;
                    (linha3 as TableRow).Visible = false;
                    (dsLblCampo3 as Label).Visible = false;

                    (dsCampo2 as TextBox).Visible = true;
                    (linha2 as TableRow).Visible = true;
                    (dsLblCampo2 as Label).Visible = true;
                }
            }
        }
        #endregion

        #region rehuna
        //if (SessionEvento.CdEvento == "008501")
        //{
        //    if (((SessionParticipante != null) && (SessionParticipante.CdParticipante == "")) ||
        //        ((SessionParticipante != null) && (SessionParticipante.CdParticipante != "")))
        //    {
        //        Control dsCampo1 = FindControlRecursive(this.Page, "txt_dsAuxiliar1");//Nr Inscrição do Participante principal *
        //        Control linha1 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar1");
        //        Control dsLblCampo1 = FindControlRecursive(this.Page, "lbl_dsAuxiliar1");

        //        Control dsCampo2 = FindControlRecursive(this.Page, "txt_noInstituicao");//Instituicao
        //        Control linha2 = FindControlRecursive(this.Page, "tblinha_noInstituicao");
        //        Control dsLblCampo2 = FindControlRecursive(this.Page, "lbl_noInstituicao");

        //        Control dsCampo3 = FindControlRecursive(this.Page, "txt_noAreaAtuacao");//noAreaAtuacao
        //        Control linha3 = FindControlRecursive(this.Page, "tblinha_noAreaAtuacao");
        //        Control dsLblCampo3 = FindControlRecursive(this.Page, "lbl_noAreaAtuacao");

        //        if ((SessionParticipante.CdCategoria == "00850108") || (SessionParticipante.CdCategoria == "00850115")) //categoria: cooperativa singular?
        //        {
        //            if (linha1 != null)
        //            {
        //                (dsCampo1 as TextBox).Visible = true;
        //                (linha1 as TableRow).Visible = true;
        //                (dsLblCampo1 as Label).Visible = true;

        //            }
        //            if (linha2 != null)
        //            {
        //                (dsCampo2 as TextBox).Visible = false;
        //                (dsCampo2 as TextBox).Text = "";
        //                (linha2 as TableRow).Visible = false;
        //                (dsLblCampo2 as Label).Visible = false;

        //            }
        //            if (linha1 != null)
        //            {
        //                (dsCampo3 as TextBox).Visible = false;
        //                (dsCampo3 as TextBox).Text = "";
        //                (linha3 as TableRow).Visible = false;
        //                (dsLblCampo3 as Label).Visible = false;

        //            }
        //        }
        //        else
        //        {
        //            if (linha1 != null)
        //            {
        //                (dsCampo1 as TextBox).Visible = false;
        //                (dsCampo1 as TextBox).Text = "";
        //                (linha1 as TableRow).Visible = false;
        //                (dsLblCampo1 as Label).Visible = false;
        //            }
        //            if (linha2 != null)
        //            {
        //                (dsCampo2 as TextBox).Visible = true;
        //                (linha2 as TableRow).Visible = true;
        //                (dsLblCampo2 as Label).Visible = true;

        //            }
        //            if (linha1 != null)
        //            {
        //                (dsCampo3 as TextBox).Visible = true;
        //                (linha3 as TableRow).Visible = true;
        //                (dsLblCampo3 as Label).Visible = true;

        //            }
        //        }
        //    }

        //}
        #endregion

        #region ABIMDE 4 BID
        //if (SessionEvento.CdEvento == "008701")
        //{
        //    if (((SessionParticipante != null) && (SessionParticipante.CdParticipante == "")) ||
        //        ((SessionParticipante != null) && (SessionParticipante.CdParticipante != "")))
        //    {

        //        if (SessionCateg == "00870101")
        //        {
        //            Control dsLbldsFone2 = FindControlRecursive(this.Page, "lbl_dsFone2");
        //            Control dsLbldsFone1 = FindControlRecursive(this.Page, "lbl_dsFone1");

        //            if (dsLbldsFone2 != null)
        //                (dsLbldsFone2 as Label).Text = "Telefone Comercial*: DDD + Número";

        //            if (dsLbldsFone1 != null)
        //                (dsLbldsFone1 as Label).Text = "Celular*: DDD + Número";
        //        }

        //        if (SessionCateg == "00870102")
        //        {
        //            Control dsLbldsEndereco = FindControlRecursive(this.Page, "lbl_dsEndereco");

        //            if ((dsLbldsEndereco != null) && (SessionIdioma == "PTBR"))
        //                (dsLbldsEndereco as Label).Text = "Endereço Completo";
                                        
        //        }


        //        Control dsCampo1 = FindControlRecursive(this.Page, "txt_dsAuxiliar1");//tipo Credenciamento

        //        if (SessionCateg == "00870104")
        //        {
        //            if ((dsCampo1 != null) && ((dsCampo1 as DropDownList).ID == "txt_dsAuxiliar1"))
        //            {
        //                (dsCampo1 as DropDownList).Items.Remove("CIVIL");
        //            }
        //        }

        //        Control dsCampo2 = FindControlRecursive(this.Page, "txt_dsAuxiliar2");//Categoria Militar
        //        Control dsLblCampo2 = FindControlRecursive(this.Page, "lbl_dsAuxiliar2");
        //        Control linha2 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar2");
        //        Control dsCampo3 = FindControlRecursive(this.Page, "txt_dsAuxiliar3");//Patente
        //        Control dsLblCampo3 = FindControlRecursive(this.Page, "lbl_dsAuxiliar3");
        //        Control linha3 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar3");
        //        Control dsCampo4 = FindControlRecursive(this.Page, "txt_dsAuxiliar5");//Patente Outros
        //        Control dsLblCampo4 = FindControlRecursive(this.Page, "lbl_dsAuxiliar5");
        //        Control linha4 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar5");


        //        Control dsCampo5 = FindControlRecursive(this.Page, "txt_noCargo");//Cargo
        //        Control dsLblCampo5 = FindControlRecursive(this.Page, "lbl_noCargo");
        //        Control linha5 = FindControlRecursive(this.Page, "tblinha_noCargo");
        //        Control dsCampo6 = FindControlRecursive(this.Page, "txt_noAreaAtuacao");//Grau Hierárquico
        //        Control dsLblCampo6 = FindControlRecursive(this.Page, "lbl_noAreaAtuacao");
        //        Control linha6 = FindControlRecursive(this.Page, "tblinha_noAreaAtuacao");

        //        Control dsCampo7 = FindControlRecursive(this.Page, "txt_dsAuxiliar6");//Grau Hierárquico - outro
        //        Control dsLblCampo7 = FindControlRecursive(this.Page, "lbl_dsAuxiliar6");
        //        Control linha7 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar6");

        //        if ((dsCampo1 != null) && ((dsCampo1 as DropDownList).ID == "txt_dsAuxiliar1"))
        //        {
        //            //Control dsCampo1 = FindControlRecursive(this.Page, "txt_dsAuxiliar1");//tipo Credenciamento
        //            //Control dsCampo2 = FindControlRecursive(this.Page, "txt_dsAuxiliar2");//Categoria Militar
        //            //Control dsLblCampo2 = FindControlRecursive(this.Page, "lbl_dsAuxiliar2");
        //            //Control linha2 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar2");
        //            //Control dsCampo3 = FindControlRecursive(this.Page, "txt_dsAuxiliar3");//Patente
        //            //Control dsLblCampo3 = FindControlRecursive(this.Page, "lbl_dsAuxiliar3");
        //            //Control linha3 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar3");
        //            //Control dsCampo4 = FindControlRecursive(this.Page, "txt_dsAuxiliar5");//Patente Outros
        //            //Control dsLblCampo4 = FindControlRecursive(this.Page, "lbl_dsAuxiliar5");
        //            //Control linha4 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar5");


        //            //Control dsCampo5 = FindControlRecursive(this.Page, "txt_noCargo");//Cargo
        //            //Control dsLblCampo5 = FindControlRecursive(this.Page, "lbl_noCargo");
        //            //Control linha5 = FindControlRecursive(this.Page, "tblinha_noCargo");
        //            //Control dsCampo6 = FindControlRecursive(this.Page, "txt_noAreaAtuacao");//Grau Hierárquico
        //            //Control dsLblCampo6 = FindControlRecursive(this.Page, "lbl_noAreaAtuacao");
        //            //Control linha6 = FindControlRecursive(this.Page, "tblinha_noAreaAtuacao");

        //            if (dsCampo1 != null)
        //            {
        //                if (((dsCampo1 as DropDownList).Text != "MILITAR") && ((dsCampo1 as DropDownList).Text != "MILITARY"))
        //                {
        //                    (dsCampo2 as DropDownList).Visible = false;
        //                    //(dsCampo2 as DropDownList).Text = "";
        //                    (linha2 as TableRow).Visible = false;

        //                    (dsCampo3 as DropDownList).Visible = false;
        //                    //(dsCampo3 as DropDownList).Text = "";
        //                    (linha3 as TableRow).Visible = false;

        //                    (dsCampo4 as TextBox).Visible = false;
        //                    //(dsCampo4 as TextBox).Text = "";
        //                    (linha4 as TableRow).Visible = false;


        //                    if ((dsCampo1 as DropDownList).Text == "CIVIL")
        //                    {
        //                        (dsCampo5 as TextBox).Visible = true;
        //                        (dsLblCampo5 as Label).Visible = true;
        //                        (linha5 as TableRow).Visible = true;

        //                        (dsCampo6 as DropDownList).Visible = true;
        //                        (dsLblCampo6 as Label).Visible = true;
        //                        (linha6 as TableRow).Visible = true;
        //                    }
        //                    else
        //                    {
        //                        (dsCampo5 as TextBox).Visible = false;
        //                        (dsCampo5 as TextBox).Text = "";
        //                        (linha5 as TableRow).Visible = false;

        //                        (dsCampo6 as DropDownList).Visible = false;
        //                        (dsCampo6 as DropDownList).Text = "";
        //                        (linha6 as TableRow).Visible = false;
        //                    }
        //                }
        //                else //if ((dsCampo1 as DropDownList).Text == "MILITAR")
        //                {
        //                    //(dsCampo2 as DropDownList).Visible = true;
        //                    //(dsLblCampo2 as Label).Visible = true;
        //                    //(linha2 as TableRow).Visible = true;

        //                    //(dsCampo3 as DropDownList).Visible = true;
        //                    //(dsLblCampo3 as Label).Visible = true;
        //                    //(linha3 as TableRow).Visible = true;

        //                    if (SessionParticipante.CdCategoria != "00870102")
        //                    {
        //                        (dsCampo2 as DropDownList).Visible = true;
        //                        (dsLblCampo2 as Label).Visible = true;
        //                        (linha2 as TableRow).Visible = true;

        //                        (dsCampo3 as DropDownList).Visible = true;
        //                        (dsLblCampo3 as Label).Visible = true;
        //                        (linha3 as TableRow).Visible = true;
        //                    }
        //                    else
        //                    {
        //                        (dsCampo2 as DropDownList).Visible = false;
        //                        (dsLblCampo2 as Label).Visible = false;
        //                        (linha2 as TableRow).Visible = false;

        //                        (dsCampo3 as DropDownList).Visible = false;
        //                        (dsLblCampo3 as Label).Visible = false;
        //                        (linha3 as TableRow).Visible = false;

        //                        (dsCampo4 as TextBox).Visible = true;
        //                        (dsLblCampo4 as Label).Visible = true;
        //                        (linha4 as TableRow).Visible = true;
        //                    }

        //                    (dsCampo5 as TextBox).Visible = false;
        //                    (dsCampo5 as TextBox).Text = "";
        //                    (linha5 as TableRow).Visible = false;

        //                    (dsCampo6 as DropDownList).Visible = false;
        //                    (dsCampo6 as DropDownList).Text = "";
        //                    (linha6 as TableRow).Visible = false;
        //                }
        //            }

        //        }

        //        if ((dsCampo2 != null) && ((dsCampo2 as DropDownList).ID == "txt_dsAuxiliar2"))
        //        {

        //            //Control dsCampo2 = FindControlRecursive(this.Page, "txt_dsAuxiliar2");//Categoria Militar
        //            //Control dsLblCampo2 = FindControlRecursive(this.Page, "lbl_dsAuxiliar2");
        //            //Control linha2 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar2");
        //            //Control dsCampo3 = FindControlRecursive(this.Page, "txt_dsAuxiliar3");//Patente
        //            //Control dsLblCampo3 = FindControlRecursive(this.Page, "lbl_dsAuxiliar3");
        //            //Control linha3 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar3");

        //            if (dsCampo2 != null)
        //            {

        //                ListarPatentesBID(SessionEvento.CdEvento, (dsCampo3 as DropDownList), (dsCampo2 as DropDownList).Text);


        //            }

        //        }

        //        if ((dsCampo3 != null) && ((dsCampo3 as DropDownList).ID == "txt_dsAuxiliar3"))
        //        {
        //            //Control dsCampo3 = FindControlRecursive(this.Page, "txt_dsAuxiliar3");//Patente
        //            //Control dsLblCampo3 = FindControlRecursive(this.Page, "lbl_dsAuxiliar3");
        //            //Control linha3 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar3");
        //            //Control dsCampo4 = FindControlRecursive(this.Page, "txt_dsAuxiliar5");//Patente Outros
        //            //Control dsLblCampo4 = FindControlRecursive(this.Page, "lbl_dsAuxiliar5");
        //            //Control linha4 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar5");

        //            if (dsCampo3 != null)
        //            {

        //                if (((dsCampo3 as DropDownList).Text != "OUTRA") && ((dsCampo3 as DropDownList).Text != "OTHER"))
        //                {
        //                    (dsCampo4 as TextBox).Visible = false;
        //                    //(dsCampo4 as TextBox).Text = "";
        //                    (linha4 as TableRow).Visible = false;
        //                }
        //                else //if ((dsCampo1 as DropDownList).Text == "MILITAR")
        //                {
        //                    (dsCampo4 as TextBox).Visible = true;
        //                    (dsLblCampo4 as Label).Visible = true;
        //                    (linha4 as TableRow).Visible = true;
        //                }


        //            }

        //        }

        //        if ((dsCampo6 != null) && ((dsCampo6 as DropDownList).ID == "txt_noAreaAtuacao")) //Grau Hierárquico - outro
        //        {
        //            //Control dsCampo3 = FindControlRecursive(this.Page, "txt_dsAuxiliar6");//Grau Hierárquico - outro
        //            //Control dsLblCampo3 = FindControlRecursive(this.Page, "lbl_dsAuxiliar6");
        //            //Control linha3 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar6");

        //            if (dsCampo6 != null)
        //            {

        //                if (((dsCampo6 as DropDownList).Text != "OUTRO") && ((dsCampo6 as DropDownList).Text != "OTHER"))
        //                {
        //                    (dsCampo7 as TextBox).Visible = false;
        //                    (dsCampo7 as TextBox).Text = "";
        //                    (linha7 as TableRow).Visible = false;
        //                }
        //                else //if ((dsCampo1 as DropDownList).Text == "MILITAR")
        //                {
        //                    (dsCampo7 as TextBox).Visible = true;
        //                    (dsLblCampo7 as Label).Visible = true;
        //                    (linha7 as TableRow).Visible = true;
        //                }


        //            }

        //        }
        //    }
        //}
        #endregion



        #region Expoepi2017

        //if (SessionEvento.CdEvento == "009401")
        //{
        //    if (((SessionParticipante != null) && (SessionParticipante.CdParticipante == "")) ||
        //        ((SessionParticipante != null) && (SessionParticipante.CdParticipante != "")))
        //    {
        //        //Control Categoria = FindControlRecursive(this.Page, "txt_cdCategoria");
        //        //string cdCateg = (Categoria as DropDownList).SelectedValue;

        //        Control dsCampo1 = FindControlRecursive(this.Page, "txt_noInstituicao");

        //        if ((dsCampo1 != null) && ((dsCampo1 as DropDownList).ID == "txt_noInstituicao"))
        //        {
        //            //Control dsCampo1 = FindControlRecursive(this.Page, "txt_noInstituicao"); //Instituicao

        //            Control dsCampo2 = FindControlRecursive(this.Page, "txt_dsAuxiliar4"); //Instituicao Complemento
        //            Control dsLblCampo2 = FindControlRecursive(this.Page, "lbl_dsAuxiliar4");
        //            Control linha2 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar4");
        //            Control refv2 = FindControlRecursive(this.Page, "txt_dsAuxiliar4_Req");

        //            Control dsCampo3 = FindControlRecursive(this.Page, "txt_dsAuxiliar5"); //Instituicao Outro
        //            Control dsLblCampo3 = FindControlRecursive(this.Page, "lbl_dsAuxiliar5");
        //            Control linha3 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar5");
        //            Control refv3 = FindControlRecursive(this.Page, "txt_dsAuxiliar5_Req");



        //            if (dsCampo1 != null)
        //            {
        //                if (((dsCampo1 as DropDownList).Text == "MINISTÉRIO DA SAÚDE") ||
        //                    ((dsCampo1 as DropDownList).Text == "ORGANISMO INTERNACIONAL"))
        //                {
        //                    (dsCampo2 as DropDownList).Visible = true;
        //                    (dsCampo2 as DropDownList).Text = "";
        //                    (dsLblCampo2 as Label).Visible = true;
        //                    (linha2 as TableRow).Visible = true;
        //                    (refv2 as RequiredFieldValidator).Visible = true;

        //                    (dsCampo3 as TextBox).Visible = false;
        //                    (dsCampo3 as TextBox).Text = "";
        //                    (dsLblCampo3 as Label).Visible = false;
        //                    (linha3 as TableRow).Visible = false;
        //                    (refv3 as RequiredFieldValidator).Visible = false;

        //                    ListarCamposExpoepi(SessionEvento.CdEvento, (dsCampo2 as DropDownList),
        //                        (dsCampo1 as DropDownList).Text);
        //                }
        //                else if (((dsCampo1 as DropDownList).Text == "SES") || ((dsCampo1 as DropDownList).Text == "SMS") || ((dsCampo1 as DropDownList).Text == "COSEMS"))
        //                {
        //                    (dsCampo2 as DropDownList).Visible = false;
        //                    (dsCampo2 as DropDownList).Text = "";
        //                    (dsLblCampo2 as Label).Visible = false;
        //                    (linha2 as TableRow).Visible = false;
        //                    (refv2 as RequiredFieldValidator).Visible = false;

        //                    (dsCampo3 as TextBox).Visible = false;
        //                    (dsCampo3 as TextBox).Text = "";
        //                    (dsLblCampo3 as Label).Visible = false;
        //                    (linha3 as TableRow).Visible = false;
        //                    (refv3 as RequiredFieldValidator).Visible = false;
        //                }
        //                else if ((dsCampo1 as DropDownList).Text == "OUTRO")
        //                {
        //                    (dsCampo3 as TextBox).Visible = true;
        //                    (dsCampo3 as TextBox).Text = "";
        //                    (dsLblCampo3 as Label).Visible = true;
        //                    (linha3 as TableRow).Visible = true;
        //                    (refv3 as RequiredFieldValidator).Visible = true;

        //                    (dsCampo2 as DropDownList).Visible = false;
        //                    (dsCampo2 as DropDownList).Text = "";
        //                    (dsLblCampo2 as Label).Visible = true;
        //                    (linha2 as TableRow).Visible = false;
        //                    (refv2 as RequiredFieldValidator).Visible = false;

        //                }


        //            }

        //        }
        //    }

        //}




        #endregion

        #region CNTC

        if (SessionEvento.CdEvento == "010101")
        {
            if (((SessionParticipante != null) && (SessionParticipante.CdParticipante == "")) ||
                ((SessionParticipante != null) && (SessionParticipante.CdParticipante != "")))
            {

                Control dsCampo1 = FindControlRecursive(this.Page, "txt_cdCategoria"); //Categoria

                Control dsCampo2 = FindControlRecursive(this.Page, "txt_noCargo"); //Cargo
                Control dsLblCampo2 = FindControlRecursive(this.Page, "lbl_noCargo");
                Control linha2 = FindControlRecursive(this.Page, "tblinha_noCargo");
                Control refv2 = FindControlRecursive(this.Page, "txt_noCargo_Req");

                Control dsCampo3 = FindControlRecursive(this.Page, "txt_dsAuxiliar1"); //Semestre
                Control dsLblCampo3 = FindControlRecursive(this.Page, "lbl_dsAuxiliar1");
                Control linha3 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar1");
                Control refv3 = FindControlRecursive(this.Page, "txt_dsAuxiliar1_Req");




                if (SessionParticipante.CdCategoria == "01010102") //"DIRIGENTES SINDICAIS")
                {
                    (dsCampo2 as TextBox).Visible = true;
                   // (dsCampo2 as TextBox).Text = "";
                    (dsLblCampo2 as Label).Visible = true;
                    (linha2 as TableRow).Visible = true;
                    (refv2 as RequiredFieldValidator).Visible = true;

                    (dsCampo3 as TextBox).Visible = false;
                    //(dsCampo3 as TextBox).Text = "";
                    (dsLblCampo3 as Label).Visible = false;
                    (linha3 as TableRow).Visible = false;
                    (refv3 as RequiredFieldValidator).Visible = false;


                }
                else if (SessionParticipante.CdCategoria == "01010103")
                    //"ESTUDANTES DE DIREITO")
                {
                    (dsCampo2 as TextBox).Visible = false;
                    //(dsCampo2 as TextBox).Text = "";
                    (dsLblCampo2 as Label).Visible = false;
                    (linha2 as TableRow).Visible = false;
                    (refv2 as RequiredFieldValidator).Visible = false;

                    (dsCampo3 as TextBox).Visible = true;
                    //(dsCampo3 as TextBox).Text = "";
                    (dsLblCampo3 as Label).Visible = true;
                    (linha3 as TableRow).Visible = true;
                    (refv3 as RequiredFieldValidator).Visible = true;
                }
                else
                {
                    (dsCampo3 as TextBox).Visible = false;
                    //(dsCampo3 as TextBox).Text = "";
                    (dsLblCampo3 as Label).Visible = false;
                    (linha3 as TableRow).Visible = false;
                    (refv3 as RequiredFieldValidator).Visible = false;

                    (dsCampo2 as TextBox).Visible = false;
                    (dsCampo2 as TextBox).Text = "";
                    (dsLblCampo2 as Label).Visible = true;
                    (linha2 as TableRow).Visible = false;
                    (refv2 as RequiredFieldValidator).Visible = false;

                }

            }
        }




        #endregion

        #region Toccata Evento Embaixada

        if (SessionEvento.CdEvento == "010501")
        {
            if (((SessionParticipante != null) && (SessionParticipante.CdParticipante == "")) ||
                ((SessionParticipante != null) && (SessionParticipante.CdParticipante != "")))
            {

                Control dsCampo1 = FindControlRecursive(this.Page, "txt_dsAuxiliar2"); //Tipo Documento

                Control dsCampo2 = FindControlRecursive(this.Page, "txt_nuCPFCNPJ");//CPF
                Control dsLblCampo2 = FindControlRecursive(this.Page, "lbl_nuCPFCNPJ");
                Control linha2 = FindControlRecursive(this.Page, "tblinha_nuCPFCNPJ");
                Control refv2 = FindControlRecursive(this.Page, "txt_nuCPFCNPJ_Req");

                Control dsCampo3 = FindControlRecursive(this.Page, "txt_dsAuxiliar1"); //PASSAPORTE
                Control dsLblCampo3 = FindControlRecursive(this.Page, "lbl_dsAuxiliar1");
                Control linha3 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar1");
                Control refv3 = FindControlRecursive(this.Page, "txt_dsAuxiliar1_Req");


                if (SessionParticipante.DsAuxiliar2 == "CPF")
                {
                    (dsCampo2 as TextBox).Visible = true;
                    // (dsCampo2 as TextBox).Text = "";
                    (dsLblCampo2 as Label).Visible = true;
                    (linha2 as TableRow).Visible = true;
                    (refv2 as RequiredFieldValidator).Visible = true;

                    (dsCampo3 as TextBox).Visible = false;
                    //(dsCampo3 as TextBox).Text = "";
                    (dsLblCampo3 as Label).Visible = false;
                    (linha3 as TableRow).Visible = false;
                    (refv3 as RequiredFieldValidator).Visible = false;


                }
                else if (SessionParticipante.DsAuxiliar2 == "PASSAPORTE")
                {
                    (dsCampo2 as TextBox).Visible = false;
                    //(dsCampo2 as TextBox).Text = "";
                    (dsLblCampo2 as Label).Visible = false;
                    (linha2 as TableRow).Visible = false;
                    (refv2 as RequiredFieldValidator).Visible = false;

                    (dsCampo3 as TextBox).Visible = true;
                    //(dsCampo3 as TextBox).Text = "";
                    (dsLblCampo3 as Label).Visible = true;
                    (linha3 as TableRow).Visible = true;
                    (refv3 as RequiredFieldValidator).Visible = true;
                }

            }
        }




        #endregion


        #region ABERT 2018

        if (SessionEvento.CdEvento == "001008")
        {
            if (((SessionParticipante != null) && (SessionParticipante.CdParticipante == "")) ||
                ((SessionParticipante != null) && (SessionParticipante.CdParticipante != "")))
            {
                Control Categoria = FindControlRecursive(this.Page, "txt_cdCategoria");
                string cdCateg = (Categoria as DropDownList).SelectedValue;

                //if (((sender as DropDownList).ID == "txt_cdCategoria"))
                //{
                    Control dsCampo1 = FindControlRecursive(this.Page, "tblinha_dsLabelSeparador1"); //Separador info hotéis
                    Control dslblCampo1 = FindControlRecursive(this.Page, "txt_dsLabelSeparador1");

                    Control dsCampo2 = FindControlRecursive(this.Page, "txt_dsAuxiliar18");//hotel 1
                    Control dsLblCampo2 = FindControlRecursive(this.Page, "lbl_dsAuxiliar18");
                    Control linha2 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar18");
                    Control refv2 = FindControlRecursive(this.Page, "txt_dsAuxiliar18_Req");

                    Control dsCampo3 = FindControlRecursive(this.Page, "txt_dsAuxiliar19"); //hotel 2
                    Control dsLblCampo3 = FindControlRecursive(this.Page, "lbl_dsAuxiliar19");
                    Control linha3 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar19");
                    Control refv3 = FindControlRecursive(this.Page, "txt_dsAuxiliar19_Req");

                    Control dsCampo4 = FindControlRecursive(this.Page, "txt_dsAuxiliar1"); //hotel 2
                    Control dsLblCampo4 = FindControlRecursive(this.Page, "lbl_dsAuxiliar1");
                    Control linha4 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar1");
                    Control refv4 = FindControlRecursive(this.Page, "txt_dsAuxiliar1_Req");




                    if (cdCateg == "00100801")
                    {
                        if (dsCampo1 != null)
                        {
                            (dsCampo1 as TableRow).Visible = true;
                            (dslblCampo1 as Label).Visible = true;

                            (dsCampo2 as DropDownList).Visible = true;
                            (dsCampo2 as DropDownList).Text = "";
                            (dsLblCampo2 as Label).Visible = true;
                            (linha2 as TableRow).Visible = true;
                            (refv2 as RequiredFieldValidator).Visible = true;

                            (dsCampo3 as DropDownList).Visible = true;
                            (dsCampo3 as DropDownList).Text = "";
                            (dsLblCampo3 as Label).Visible = true;
                            (linha3 as TableRow).Visible = true;
                            //(refv3 as RequiredFieldValidator).Visible = true;

                            (dsCampo4 as DropDownList).Visible = true;
                            (dsCampo4 as DropDownList).Text = "";
                            (dsLblCampo4 as Label).Visible = true;
                            (linha4 as TableRow).Visible = true;
                            (refv4 as RequiredFieldValidator).Visible = true;

                        }
                    }
                    else //if ()
                    {
                        if (dsCampo1 != null)
                        {
                            (dsCampo1 as TableRow).Visible = false;
                            (dslblCampo1 as Label).Visible = false;

                            (dsCampo2 as DropDownList).Visible = false;
                            (dsCampo2 as DropDownList).Text = "";
                            (dsLblCampo2 as Label).Visible = false;
                            (linha2 as TableRow).Visible = false;
                            (refv2 as RequiredFieldValidator).Visible = false;

                            (dsCampo3 as DropDownList).Visible = false;
                            (dsCampo3 as DropDownList).Text = "";
                            (dsLblCampo3 as Label).Visible = false;
                            (linha3 as TableRow).Visible = false;
                            //(refv3 as RequiredFieldValidator).Visible = false;

                            (dsCampo4 as DropDownList).Visible = false;
                            (dsCampo4 as DropDownList).Text = "";
                            (dsLblCampo4 as Label).Visible = false;
                            (linha4 as TableRow).Visible = false;
                            (refv4 as RequiredFieldValidator).Visible = false;

                        }
                    }

                //}
            }
        }




        #endregion

        #region Neuronus


        if (SessionEvento.CdEvento == "012601")
        {
            if (((SessionParticipante != null) && (SessionParticipante.CdParticipante == "")) ||
                ((SessionParticipante != null) && (SessionParticipante.CdParticipante != "")))
            {

                Control dsCampo1 = FindControlRecursive(this.Page, "txt_dsAuxiliar1"); //Área de Formação
                if ((dsCampo1 != null) && ((dsCampo1 as DropDownList).ID == "txt_dsAuxiliar1"))
                {
                    Control dsCampo2 = FindControlRecursive(this.Page, "txt_dsAuxiliar2"); //Área de Formação - Outro
                    Control dsLblCampo2 = FindControlRecursive(this.Page, "lbl_dsAuxiliar2");
                    Control linha2 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar2");
                    Control refv2 = FindControlRecursive(this.Page, "txt_dsAuxiliar2_Req");

                    Control dsCampo3 = FindControlRecursive(this.Page, "txt_dsAuxiliar3"); //Área de Formação - Complemento
                    Control dsLblCampo3 = FindControlRecursive(this.Page, "lbl_dsAuxiliar3");
                    Control linha3 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar3");
                    Control refv3 = FindControlRecursive(this.Page, "txt_dsAuxiliar3_Req");

                    Control dsCampo4 = FindControlRecursive(this.Page, "txt_dsAuxiliar4"); //Área de Formação - Complemento (Outro)
                    Control dsLblCampo4 = FindControlRecursive(this.Page, "lbl_dsAuxiliar4");
                    Control linha4 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar4");
                    Control refv4 = FindControlRecursive(this.Page, "txt_dsAuxiliar4_Req");


                    if (dsCampo1 != null)
                    {
                        if (((dsCampo1 as DropDownList).Text == "OUTRA") || ((dsCampo1 as DropDownList).Text == "OTHER"))
                        {
                            (dsCampo2 as TextBox).Visible = true;
                            (dsCampo2 as TextBox).Text = "";
                            (dsLblCampo2 as Label).Visible = true;
                            (linha2 as TableRow).Visible = true;
                            (refv2 as RequiredFieldValidator).Visible = true;

                            (dsCampo3 as DropDownList).Visible = false;
                            (dsCampo3 as DropDownList).Text = "";
                            (dsLblCampo3 as Label).Visible = false;
                            (linha3 as TableRow).Visible = false;
                            (refv3 as RequiredFieldValidator).Visible = false;

                            (dsCampo4 as TextBox).Visible = false;
                            (dsCampo4 as TextBox).Text = "";
                            (dsLblCampo4 as Label).Visible = false;
                            (linha4 as TableRow).Visible = false;
                            (refv4 as RequiredFieldValidator).Visible = false;


                        }
                        else if (((dsCampo1 as DropDownList).Text == "MEDICINA") || ((dsCampo1 as DropDownList).Text == "MEDICINE"))
                        {
                            (dsCampo2 as TextBox).Visible = false;
                            (dsCampo2 as TextBox).Text = "";
                            (dsLblCampo2 as Label).Visible = false;
                            (linha2 as TableRow).Visible = false;
                            (refv2 as RequiredFieldValidator).Visible = false;

                            (dsCampo3 as DropDownList).Visible = true;
                            //(dsCampo3 as DropDownList).Text = "";
                            (dsLblCampo3 as Label).Visible = true;
                            (linha3 as TableRow).Visible = true;
                            (refv3 as RequiredFieldValidator).Visible = true;

                            (dsCampo4 as TextBox).Visible = false;
                            //(dsCampo4 as TextBox).Text = "";
                            (dsLblCampo4 as Label).Visible = false;
                            (linha4 as TableRow).Visible = false;
                            (refv4 as RequiredFieldValidator).Visible = false;
                        }
                        else
                        {
                            (dsCampo2 as TextBox).Visible = false;
                            (dsCampo2 as TextBox).Text = "";
                            (dsLblCampo2 as Label).Visible = false;
                            (linha2 as TableRow).Visible = false;
                            (refv2 as RequiredFieldValidator).Visible = false;

                            (dsCampo3 as DropDownList).Visible = false;
                            (dsCampo3 as DropDownList).Text = "";
                            (dsLblCampo3 as Label).Visible = false;
                            (linha3 as TableRow).Visible = false;
                            (refv3 as RequiredFieldValidator).Visible = false;

                            (dsCampo4 as TextBox).Visible = false;
                            (dsCampo4 as TextBox).Text = "";
                            (dsLblCampo4 as Label).Visible = false;
                            (linha4 as TableRow).Visible = false;
                            (refv4 as RequiredFieldValidator).Visible = false;
                        }
                    }

                }

                Control dsCampo5 = FindControlRecursive(this.Page, "txt_dsAuxiliar1"); //Área de Formação - Complemento
                if ((dsCampo5 != null) && ((dsCampo1 as DropDownList).ID == "txt_dsAuxiliar3"))
                {
                    Control dsCampo4 =
                        FindControlRecursive(this.Page, "txt_dsAuxiliar4"); //Área de Formação - Complemento (Outra)
                    Control dsLblCampo4 = FindControlRecursive(this.Page, "lbl_dsAuxiliar4");
                    Control linha4 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar4");
                    Control refv4 = FindControlRecursive(this.Page, "txt_dsAuxiliar4_Req");



                    if (dsCampo5 != null)
                    {
                        if (((dsCampo5 as DropDownList).Text == "OUTRA") || ((dsCampo1 as DropDownList).Text == "OTHER"))
                        {
                            (dsCampo4 as TextBox).Visible = true;
                            (dsCampo4 as TextBox).Text = "";
                            (dsLblCampo4 as Label).Visible = true;
                            (linha4 as TableRow).Visible = true;
                            (refv4 as RequiredFieldValidator).Visible = true;


                        }
                        else
                        {
                            (dsCampo4 as TextBox).Visible = false;
                            (dsCampo4 as TextBox).Text = "";
                            (dsLblCampo4 as Label).Visible = false;
                            (linha4 as TableRow).Visible = false;
                            (refv4 as RequiredFieldValidator).Visible = false;
                        }
                    }

                }

            }
        }

        //if (SessionEvento.CdEvento == "009401")
        //{
        //    if (((SessionParticipante != null) && (SessionParticipante.CdParticipante == "")) ||
        //        ((SessionParticipante != null) && (SessionParticipante.CdParticipante != "")))
        //    {
        //        //Control Categoria = FindControlRecursive(this.Page, "txt_cdCategoria");
        //        //string cdCateg = (Categoria as DropDownList).SelectedValue;

        //        Control dsCampo1 = FindControlRecursive(this.Page, "txt_noInstituicao");

        //        if ((dsCampo1 != null) && ((dsCampo1 as DropDownList).ID == "txt_noInstituicao"))
        //        {
        //            //Control dsCampo1 = FindControlRecursive(this.Page, "txt_noInstituicao"); //Instituicao

        //            Control dsCampo2 = FindControlRecursive(this.Page, "txt_dsAuxiliar4"); //Instituicao Complemento
        //            Control dsLblCampo2 = FindControlRecursive(this.Page, "lbl_dsAuxiliar4");
        //            Control linha2 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar4");
        //            Control refv2 = FindControlRecursive(this.Page, "txt_dsAuxiliar4_Req");

        //            Control dsCampo3 = FindControlRecursive(this.Page, "txt_dsAuxiliar5"); //Instituicao Outro
        //            Control dsLblCampo3 = FindControlRecursive(this.Page, "lbl_dsAuxiliar5");
        //            Control linha3 = FindControlRecursive(this.Page, "tblinha_dsAuxiliar5");
        //            Control refv3 = FindControlRecursive(this.Page, "txt_dsAuxiliar5_Req");



        //            if (dsCampo1 != null)
        //            {
        //                if (((dsCampo1 as DropDownList).Text == "MINISTÉRIO DA SAÚDE") ||
        //                    ((dsCampo1 as DropDownList).Text == "ORGANISMO INTERNACIONAL"))
        //                {
        //                    (dsCampo2 as DropDownList).Visible = true;
        //                    (dsCampo2 as DropDownList).Text = "";
        //                    (dsLblCampo2 as Label).Visible = true;
        //                    (linha2 as TableRow).Visible = true;
        //                    (refv2 as RequiredFieldValidator).Visible = true;

        //                    (dsCampo3 as TextBox).Visible = false;
        //                    (dsCampo3 as TextBox).Text = "";
        //                    (dsLblCampo3 as Label).Visible = false;
        //                    (linha3 as TableRow).Visible = false;
        //                    (refv3 as RequiredFieldValidator).Visible = false;

        //                    ListarCamposExpoepi(SessionEvento.CdEvento, (dsCampo2 as DropDownList),
        //                        (dsCampo1 as DropDownList).Text);
        //                }
        //                else if (((dsCampo1 as DropDownList).Text == "SES") || ((dsCampo1 as DropDownList).Text == "SMS") || ((dsCampo1 as DropDownList).Text == "COSEMS"))
        //                {
        //                    (dsCampo2 as DropDownList).Visible = false;
        //                    (dsCampo2 as DropDownList).Text = "";
        //                    (dsLblCampo2 as Label).Visible = false;
        //                    (linha2 as TableRow).Visible = false;
        //                    (refv2 as RequiredFieldValidator).Visible = false;

        //                    (dsCampo3 as TextBox).Visible = false;
        //                    (dsCampo3 as TextBox).Text = "";
        //                    (dsLblCampo3 as Label).Visible = false;
        //                    (linha3 as TableRow).Visible = false;
        //                    (refv3 as RequiredFieldValidator).Visible = false;
        //                }
        //                else if ((dsCampo1 as DropDownList).Text == "OUTRO")
        //                {
        //                    (dsCampo3 as TextBox).Visible = true;
        //                    (dsCampo3 as TextBox).Text = "";
        //                    (dsLblCampo3 as Label).Visible = true;
        //                    (linha3 as TableRow).Visible = true;
        //                    (refv3 as RequiredFieldValidator).Visible = true;

        //                    (dsCampo2 as DropDownList).Visible = false;
        //                    (dsCampo2 as DropDownList).Text = "";
        //                    (dsLblCampo2 as Label).Visible = true;
        //                    (linha2 as TableRow).Visible = false;
        //                    (refv2 as RequiredFieldValidator).Visible = false;

        //                }


        //            }

        //        }
        //    }

        //}




        #endregion

    }




    protected void btnAtividades0_Click(object sender, EventArgs e)
    {
        Response.Write("<script>window.open('Default7.aspx','_self');</script>");

        //Server.Transfer("Default7.aspx", true);
    }
    protected void btnNovo_Click(object sender, EventArgs e)
    {
        //Server.Transfer("frmRedirecionaNovo.aspx", true);
    }
    protected void btnEtiqueta_Click(object sender, EventArgs e)
    {
        Response.Write("<script>window.open('rptEtiqueta.aspx','_blank');</script>");
    }
    private void prp_btnhabilita(string prm_rotina)
    {

        //if ((prm_rotina == "INCLUIR") ||
        //    (prm_rotina == "ALTERAR"))
        //{           

        //    btnLimpar.Enabled = false;
        //    btnLimpar.ImageUrl = "~/img/btnLimparD.jpg";
        //    btnLocalizar.Enabled = false;
        //    btnLocalizar.ImageUrl="~/img/btnLocalizarD.jpg";
        //    btnNovos.Enabled = false;
        //    btnNovos.ImageUrl="~/img/btnNovoD.jpg"; 
        //    btnAlterar.Enabled = false;
        //    btnAlterar.ImageUrl="~/img/btnEditarD.jpg"; 

        //    btnConfirmar.Enabled = true;
        //    btnConfirmar.ImageUrl="~/img/btnConfirmar.jpg";
        //    btnCancelar.Enabled = true;
        //    btnCancelar.ImageUrl="~/img/btnCancelar.jpg";

        //    btnEtiquetas.Enabled = false;
        //    btnEtiquetas.ImageUrl="~/img/btnPrtEtiquetaD.jpg";
        //    btnAtividade.Enabled = false;
        //    btnAtividade.ImageUrl = "~/img/btnAtividadesD.jpg";
            

        //    //prp_habilitaCampos(true);

        //}
        //else
        //{
        //    //prp_habilitaCampos(false);

        //    btnLimpar.Enabled = true;
        //    btnLimpar.ImageUrl = "~/img/btnLimpar.jpg";
        //    btnLocalizar.Enabled = true;
        //    btnLocalizar.ImageUrl = "~/img/btnLocalizar.jpg";
        //    btnNovos.Enabled = true;
        //    btnNovos.ImageUrl = "~/img/btnNovo.jpg"; 
        //}

        //if (prm_rotina == "ATIVO")
        //{
        //    //btnNovo.Enabled = (oEscalaClinica == null ? true : false);
        //    //btnAlterar.Enabled = true;// (oEscalaClinica == null ? false : true);
        //    btnAlterar.Enabled = true;
        //    btnAlterar.ImageUrl = "~/img/btnEditar.jpg"; 
        //    btnEtiquetas.Enabled = true;
        //    btnEtiquetas.ImageUrl = "~/img/btnPrtEtiqueta.jpg";
        //    btnAtividade.Enabled = true;
        //    btnAtividade.ImageUrl = "~/img/btnAtividades.jpg";

        //    btnConfirmar.Enabled = false;
        //    btnConfirmar.ImageUrl = "~/img/btnConfirmarD.jpg";
        //    btnCancelar.Enabled = false;
        //    btnCancelar.ImageUrl = "~/img/btnCancelarD.jpg";
        //}


        //if (prm_rotina == "INATIVO")
        //{
        //    btnConfirmar.Enabled = false;
        //    btnConfirmar.ImageUrl = "~/img/btnConfirmarD.jpg";
        //    btnCancelar.Enabled = false;
        //    btnCancelar.ImageUrl = "~/img/btnCancelarD.jpg";

        //    //btnNovo.Enabled = false;
        //    btnAlterar.Enabled = false;
        //    btnAlterar.ImageUrl = "~/img/btnEditarD.jpg";
           

        //    btnEtiquetas.Enabled = false;
        //    btnEtiquetas.ImageUrl = "~/img/btnPrtEtiquetaD.jpg";
        //    btnAtividade.Enabled = false;
        //    btnAtividade.ImageUrl = "~/img/btnAtividadesD.jpg";
                        
        //}

        //if ((prm_rotina == "INICIO") || (prm_rotina == ""))
        //{
        //    btnLimpar.Enabled = true;
        //    btnLimpar.ImageUrl = "~/img/btnLimpar.jpg";
        //    btnLocalizar.Enabled = true;
        //    btnLocalizar.ImageUrl = "~/img/btnLocalizar.jpg";
        //    btnNovos.Enabled = true;
        //    btnNovos.ImageUrl = "~/img/btnNovo.jpg";

        //    btnAlterar.Enabled = false;
        //    btnAlterar.ImageUrl = "~/img/btnEditarD.jpg";

        //    btnConfirmar.Enabled = false;
        //    btnConfirmar.ImageUrl = "~/img/btnConfirmarD.jpg";
        //    btnCancelar.Enabled = false;
        //    btnCancelar.ImageUrl = "~/img/btnCancelarD.jpg";

        //    btnEtiquetas.Enabled = false;
        //    btnEtiquetas.ImageUrl = "~/img/btnPrtEtiquetaD.jpg";
        //    btnAtividade.Enabled = false;
        //    btnAtividade.ImageUrl = "~/img/btnAtividadesD.jpg";
        //}


    }
    protected void btnLimpar_Click(object sender, ImageClickEventArgs e)
    {
        SessionOperacao = "INICIO";
        Session["SessionOperacao"] = SessionOperacao;

        SessionParticipante = new Participante();
        Session["SessionParticipante"] = SessionParticipante;
        Response.Write("<script>window.open('frmRedirecionaNovo.aspx','_self');</script>");
    }
    protected void btnLocalizar_Click(object sender, ImageClickEventArgs e)
    {
        //pnlCampos_ModalPopupExtender.Show();
    }
    protected void btnNovos_Click(object sender, ImageClickEventArgs e)
    {
        SessionOperacao = "INCLUIR";
        Session["SessionOperacao"] = SessionOperacao;
        Response.Write("<script>window.open('frmRedirecionaNovo.aspx','_self');</script>");
        //Server.Transfer("frmRedirecionaNovo.aspx", true);
    }
    protected void btnAlterar_Click(object sender, ImageClickEventArgs e)
    {
        SessionOperacao = "ALTERAR";
        Session["SessionOperacao"] = SessionOperacao;
        Session["SessionParticipante"] = SessionParticipante;
        Response.Write("<script>window.open('frmRedirecionaNovo.aspx','_self');</script>");
    }
    protected void btnConfirmar_Click(object sender, ImageClickEventArgs e)
    {
        Button1_Click(sender, e);
    }
    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        SessionOperacao = "INICIO";
        Session["SessionOperacao"] = SessionOperacao;
        Session["SessionParticipante"] = SessionParticipante;
        Response.Write("<script>window.open('frmRedirecionaNovo.aspx','_self');</script>");
    }
    protected void btnAtividade_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void btnEtiquetas_Click(object sender, ImageClickEventArgs e)
    {
        lblMsg.Visible = false;

        if (SessionParticipante == null)
        {
            lblMsg.Visible = true;
            lblMsg.Text = "Informe um participante válido!";
            return;
        }

        if (SessionParticipante.Categoria.FlPagamento)
        {

            if (!oParticipanteCad.VerificarAtividadeObrigatoria(SessionParticipante, SessionCnn))
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Para emitir a credencial é preciso estar vinculado à atividade principal!";
                return;
            }
        }

        //if (SessionParticipante.CdCredencial != "0")
        //{
        //    Response.Write(@"<script language='javascript'>return confirm('Credencial já emitida para este Participante!\n" +
        //                     "Deseja emitir outra?');</script>");

        //    return;
        //    //if (MessageBox.Show("Credencial já emitida para este Participante!\n" +
        //    //                 "Deseja emitir outra?", "Atenção", MessageBoxButtons.YesNo) == DialogResult.No)
        //    //{
        //    //    //if (SessionEvento.FlImprimirCertificadoCredenciamento)
        //    //    //    imprimirCert();

        //    //    return;
        //    //}
        //}
        //else
        {
            if (SessionParticipante != null)
            {
                if (!SessionParticipante.Categoria.FlAtividades)
                    goto imprimir;// return;

                Inscricoes oInscricoes = new Inscricoes();
                if (oInscricoes.verificarAtividadeInscricao(SessionParticipante.CdEvento, SessionParticipante.CdParticipante, SessionParticipante.CdCategoria, "inscrlocal", SessionCnn) > 0)
                {
                    DataTable DTAtiv = oInscricoes.ListarAtividadesDisponiveis(SessionParticipante, null, "inscrlocal", SessionCnn);

                    if (DTAtiv != null)
                    {
                        if (DTAtiv.Rows.Count == 1)
                        {
                            string cdAtividade = DTAtiv.DefaultView[0]["cdAtividade"].ToString().Trim();
                            decimal vlAtv = decimal.Parse(DTAtiv.DefaultView[0]["vlAtividade"].ToString().Trim());
                            decimal vlDesc = decimal.Parse(DTAtiv.DefaultView[0]["vlDescontoReal"].ToString().Trim());
                            decimal vlTotInscri = decimal.Parse(DTAtiv.DefaultView[0]["vlTotInscri"].ToString().Trim());
                            //string tpPagto = SessionCategoria.NoCategoria;
                            if (oInscricoes.MatriculasGravar(SessionParticipante.CdEvento, SessionParticipante.CdParticipante, cdAtividade, 0, 1, "000000001", SessionCnn))
                            {//evento gratuito
                                vlAtv = 0;
                                vlDesc = 0;
                                vlTotInscri = 0;
                                //tpPagto = "";

                                if (oInscricoes.verificarVagas(SessionEvento.CdEvento, cdAtividade, SessionCnn) <= 0)
                                {
                                    lblMsg.Visible = true;
                                    lblMsg.Text = "Não há mais vagas!";
                                    return;
                                }

                                DataTable DTVerificaMatricla = oInscricoes.ListarAtividadesDoParticipante(SessionParticipante, cdAtividade, SessionCnn);
                                if ((DTVerificaMatricla == null) || (DTVerificaMatricla.Rows.Count <= 0))
                                {
                                    //return; //já cadastrado



                                    //gerar matricula
                                    if (oInscricoes.MatriculasGravar(SessionParticipante.CdEvento, SessionParticipante.CdParticipante, cdAtividade, 0, 1, "000000001", SessionCnn))
                                    {
                                        //enviar email

                                        //-----
                                        //return; //inclusão em curso com suscesso
                                    }
                                    else
                                    {
                                        lblMsg.Visible = true;
                                       lblMsg.Text = "Não foi possível efetuar a inscrição.\n" +
                                            "Tente de novo, se persistir o problema entre em contato o administrador do sistema.";

                                        return;
                                    }
                                }
                            }
                        }
                    }

                    //BtnInscrCursos_Click(sender, e);
                }
            }

        }

        imprimir:



        if (oParticipanteCad.GerarCredencial(SessionParticipante, SessionCnn))
        {
            Response.Write("<script>window.open('rptEtiqueta.aspx','_blanck');</script>");
        }
        else
        {
            lblMsg.Visible = true;
            lblMsg.Text = oParticipanteCad.RcMsg;
        }
    }
    protected void grdParticpante_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //grdParticpante.PageIndex = e.NewPageIndex;
        //carregarGrdParticipantes();
    }
    protected void grdParticpante_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        /*
        //lblCdStand.Text = grdEstandes.DataKeys[e.NewSelectedIndex].Values[0].ToString();
        //Pesquisar(lblCdStand.Text);
        //pnlCampos_ModalPopupExtender.Show();

        lblMsg.Visible = false;
        lblMsg.Text = "";


        string tmpPesq = grdParticpante.DataKeys[e.NewSelectedIndex].Values[0].ToString();

        SessionOperacao = "INICIO";
        Session["SessionOperacao"] = SessionOperacao;

        pesquisar(tmpPesq);

        //Participante tempPart = oParticipanteCad.Pesquisar(SessionEvento.CdEvento, "cdParticipante", tmpPesq, SessionCnn);

        //if (tempPart == null)
        //{
        //    SessionParticipante = new Participante();
        //    Session["SessionParticipante"] = SessionParticipante;

        //    SessionOperacao = "INICIO";
        //    Session["SessionOperacao"] = SessionOperacao;

        //    Response.Write("<script>window.open('frmRedirecionaNovo.aspx','_self');</script>");
        //    return;
        //}

        //SessionParticipante = tempPart;
        //Session["SessionParticipante"] = SessionParticipante;



        ////pnlCampos_ModalPopupExtender.Show();

        //Response.Write("<script>window.open('frmRedirecionaNovo.aspx','_self');</script>");
        */
    }
    protected void btnFiltrarParticipantes_Click(object sender, ImageClickEventArgs e)
    {/*
        lblMsgPesq.Text = "";
        if ((txtFiltroNome1.Text.Trim() == "") &&
            (txtFiltroNome2.Text.Trim() == "") &&
            (txtFiltroNome3.Text.Trim() == "") &&
            (txtFiltroEmail.Text.Trim() == "") &&
            (txtFiltroDocIdent.Text.Trim() == "") &&
            (txtFiltroInstOrgao.Text.Trim() == "") &&
            (txtFiltroCateg.Text.Trim() == ""))
        {
            lblMsgPesq.Text = "Informe pelo menos um argumento de pesquisa!";
            pnlCampos_ModalPopupExtender.Show();
            txtFiltroNome1.Focus();
        }
        else
        {            
            carregarGrdParticipantes();

            pnlCampos_ModalPopupExtender.Show();
            grdParticpante.Focus();
        }*/
    }
    protected void btnLimparFiltro_Click(object sender, ImageClickEventArgs e)
    {/*
        txtFiltroNome1.Text = "";
        txtFiltroNome2.Text = "";
        txtFiltroNome3.Text = "";
        txtFiltroEmail.Text = "";
        txtFiltroDocIdent.Text = "";
        txtFiltroInstOrgao.Text = "";
        txtFiltroCateg.Text = "";

        lblMsgPesq.Text = "";

        grdParticpante.DataSource = null;
        grdParticpante.DataBind();

        pnlCampos_ModalPopupExtender.Show();
        txtFiltroNome1.Focus();*/
    }

    protected void CategoriasFiltro()
    {/*
        CategoriaCad oCategoriaCad = new CategoriaCad();
        DataTable tmpDTCateg = oCategoriaCad.ListarFlAtivo(SessionEvento, true, SessionCnn);

        DataTable tmpDt = new DataTable();
        tmpDt.Columns.Add("cdCategoria");
        tmpDt.Columns.Add("noCategoria");
        tmpDt.Rows.Add("", "");

        if (tmpDTCateg == null)
        {
            //tmpDt.Rows.Add("", "");
            txtFiltroCateg.DataSource = tmpDt;
            txtFiltroCateg.DataBind();
            return;
        }

        for (int i = 0; i < tmpDTCateg.Rows.Count; i++)
        {
            //if (bool.Parse(DTEvento.DefaultView[i]["flAtivo"].ToString()))
            //{
            tmpDt.Rows.Add(tmpDTCateg.DefaultView[i]["cdCategoria"].ToString(),
                           tmpDTCateg.DefaultView[i]["noCategoria"].ToString());
            //}
        }

        txtFiltroCateg.DataSource = tmpDt;
        txtFiltroCateg.DataTextField = "noCategoria";
        txtFiltroCateg.DataValueField = "cdCategoria";
        txtFiltroCateg.DataBind();*/
    }
    DataTable oDT;
    protected void carregarGrdParticipantes()
    {/*
        grdParticpante.DataSource = null;
        grdParticpante.DataBind();



        oDT = oParticipanteCad.Listar(SessionEvento.CdEvento, txtFiltroNome1.Text, txtFiltroNome2.Text, txtFiltroNome3.Text, txtFiltroEmail.Text, txtFiltroDocIdent.Text, txtFiltroInstOrgao.Text, txtFiltroCateg.SelectedValue.ToString(), SessionCnn);
        //oStandCad.ListarEstandes(SessionEvento.CdEvento, txtDsEmpresaFiltro.Text, txtCodEstandeFiltro.Text, txtStatusFiltro.Text, txtAtivoFiltro.SelectedValue.ToString(), SessionCnn);

        if (oDT == null)
        {
            return;
        }

        grdParticpante.DataSource = oDT;
        grdParticpante.DataBind();

        */

    }
    protected void btnEnviarDocumento_Click(object sender, EventArgs e)
    {

    }

    public void ListarOutrosSICOOB(String prmCdEvento, DropDownList prmCampoListagem, string prmFiltroSistema)
    {
        lblMsg.Visible = false;
        lblMsg.Text = "";

        if (SessionCnn == null)
        {
            //_erroNoCampo = true;
            lblMsg.Text = "Conexão inválida ou inexistente";
            lblMsg.Visible = true;
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
                lblMsg.Text = "Conexão inválida";
                lblMsg.Visible = true;
                return;
            }
        }

        DataTable DT = new DataTable();
        SqlDataAdapter Dap;
        try
        {
            try
            {
                SqlCommand comando = new SqlCommand(
                    "SELECT        COMPE, SISTEMA, CLASSE, UF, NOME, CNPJ " +
                    "FROM            tmpSICOOB " +
                    "WHERE       (CLASSE = 'SINGULAR') AND (SISTEMA LIKE '%" + prmFiltroSistema + "%')  ", SessionCnn);


                Dap = new SqlDataAdapter(comando);

                Dap.TableMappings.Add("Simgular", "tmpSICOOB");
                Dap.Fill(DT);

                SessionCnn.Close();

                //BindingSource bsufs = new BindingSource();

                DataTable oDataTable = new DataTable();



                oDataTable.Columns.Add("COMPE");
                oDataTable.Columns.Add("SISTEMA");
                oDataTable.Columns.Add("CLASSE");
                oDataTable.Columns.Add("UF");
                oDataTable.Columns.Add("NOME");
                oDataTable.Columns.Add("CNPJ");

                oDataTable.Rows.Add("", "", "", "", "", "");

                for (int i = 0; i < DT.DefaultView.Count; i++)
                {
                    oDataTable.Rows.Add(
                        DT.DefaultView[i]["COMPE"],
                        DT.DefaultView[i]["SISTEMA"],
                        DT.DefaultView[i]["CLASSE"],
                        DT.DefaultView[i]["UF"],
                        DT.DefaultView[i]["NOME"],
                        DT.DefaultView[i]["CNPJ"]);
                }


                //bsufs.DataSource = oDataTable.DefaultView;
                prmCampoListagem.DataSource = oDataTable.DefaultView;
                prmCampoListagem.DataTextField = "NOME";
                prmCampoListagem.DataValueField = "NOME";
                prmCampoListagem.DataBind();

                //this.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                //this.AutoCompleteSource = AutoCompleteSource.ListItems;

            }
            catch (Exception Ex)
            {
                SessionCnn.Close();
                //_erroNoCampo = true;
                lblMsg.Text = "Erro ao selecionar SINGULARES!\n" + Ex.Message;
                lblMsg.Visible = true;
            }
        }
        finally
        {
            SessionCnn.Close();
        }


    }

    public Boolean VerificarTotalCentralSICOOB(String prmCdEvento, String prmCdParticipante, String prmNomeCentral)
    {
        lblMsg.Visible = false;
        lblMsg.Text = "";

        if (SessionCnn == null)
        {
            lblMsg.Visible = true;
            //_erroNoCampo = true;
            lblMsg.Text = "Conexão inválida ou inexistente";
            return false;
        }


        if (SessionCnn.State != ConnectionState.Open)
        {
            try
            {
                SessionCnn.Open();
            }
            catch
            {
                lblMsg.Visible = true;
                //_erroNoCampo = true;
                lblMsg.Text = "Conexão inválida";
                return false;
            }
        }

        DataTable DT = new DataTable();
        SqlDataAdapter Dap;
        try
        {
            
            try
            {
                SqlCommand comando = new SqlCommand(
                    "SELECT count(*) total, " +
                    "      (SELECT qtdCentral     " + 
                      "      FROM [dbo].[tmpSICOOB] " +
                      "      where [SISTEMA] = '" + prmNomeCentral + "' " +
                    "        and CLASSE = 'CENTRAL') totalgeral " +
                    "  FROM [tbParticipantes] " +
                    "  WHERE cdEvento = '" + prmCdEvento + "' " +
                    "  AND flAtivo = 1 " +
                    "  AND cdCategoria = '00830301' " +
                    "  AND dsAuxiliar1 = '" + prmNomeCentral + "'", SessionCnn);


                Dap = new SqlDataAdapter(comando);

                Dap.TableMappings.Add("Central", "tmpSICOOB");
                Dap.Fill(DT);

                
                if (DT == null)
                {
                    SessionCnn.Close();
                    return true;
                }



                if (int.Parse(DT.DefaultView[0]["total"].ToString()) >= int.Parse(DT.DefaultView[0]["totalgeral"].ToString()))
                {
                    if (prmCdParticipante == "")
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "Limite de inscrições para esta Cooperativa Central foi atingido.";
                        return false;
                    }
                    else
                    {
                        DataTable DT2 = new DataTable();
                        SqlDataAdapter Dap2;

                        SqlCommand comando2 = new SqlCommand(
                        "SELECT        	cdParticipante " +
                        "  FROM [tbParticipantes] " +
                        "  WHERE cdEvento = '" + prmCdEvento + "' " +
                        "  AND flAtivo = 1 " +
                        "  AND cdCategoria = '00830301' " +
                        "  AND dsAuxiliar1 = '" + prmNomeCentral + "' " +
                        "  AND cdParticipante = '" + prmCdParticipante + "' ", SessionCnn);


                        Dap2 = new SqlDataAdapter(comando2);

                        Dap2.TableMappings.Add("Central2", "tmpSICOOB2");
                        Dap2.Fill(DT2);

                        SessionCnn.Close();

                        if ((DT2 == null) || (DT2.Rows.Count == 0))
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "Limite de inscrições para esta Cooperativa Central foi atingido.";
                            return false;
                        }
                        else
                            return true;
                    }
                }

                return true;
                

            }
            catch (Exception Ex)
            {
                SessionCnn.Close();
                //_erroNoCampo = true;
                lblMsg.Text = "Erro ao verificar COOPERATIVA CENTRAL!\n" + Ex.Message;
                return false;
            }
        }
        finally
        {
            SessionCnn.Close();
        }
    }

    public Boolean VerificarTotalSingularSICOOB(String prmCdEvento, String prmCdParticipante, String prmNomeCentral, String prmNomeSingular)
    {
        lblMsg.Visible = false;
        lblMsg.Text = "";

        if (SessionCnn == null)
        {
            lblMsg.Visible = true;
            //_erroNoCampo = true;
            lblMsg.Text = "Conexão inválida ou inexistente";
            return false;
        }


        if (SessionCnn.State != ConnectionState.Open)
        {
            try
            {
                SessionCnn.Open();
            }
            catch
            {
                lblMsg.Visible = true;
                //_erroNoCampo = true;
                lblMsg.Text = "Conexão inválida";
                return false;
            }
        }

        DataTable DT = new DataTable();
        SqlDataAdapter Dap;
        try
        {
            try
            {
                SqlCommand comando = new SqlCommand(
                    "SELECT        	count(*) total, " +
                    "      (SELECT qtdSingular     " +
                      "      FROM [dbo].[tmpSICOOB] " +
                      "      where [SISTEMA] = '" + prmNomeCentral + "' " +
                      "      and  NOME =  '" + prmNomeSingular + "' " +
                    "        and CLASSE <> 'CENTRAL') totalgeral " +
                    "  FROM [tbParticipantes] " +
                    "  WHERE cdEvento = '" + prmCdEvento + "' " +
                    "  AND flAtivo = 1 " +
                    "  AND cdCategoria = '00830302' " +
                    "  AND dsAuxiliar1 = '" + prmNomeCentral + "'" +
                    "  AND dsAuxiliar2 = '" + prmNomeSingular + "'", SessionCnn);


                Dap = new SqlDataAdapter(comando);

                Dap.TableMappings.Add("Singular", "tmpSICOOB");
                Dap.Fill(DT);

                

                if (DT == null)
                {
                    SessionCnn.Close();

                    return true;
                }



                if (int.Parse(DT.DefaultView[0]["total"].ToString()) >= int.Parse(DT.DefaultView[0]["totalgeral"].ToString()))
                {
                    if (prmCdParticipante == "")
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "Limite de inscrições para esta Cooperativa Singular foi atingido.";
                        return false;
                    }
                    else
                    {
                        DataTable DT2 = new DataTable();
                        SqlDataAdapter Dap2;

                        SqlCommand comando2 = new SqlCommand(
                        "SELECT        	cdParticipante " +
                        "  FROM [tbParticipantes] " +
                        "  WHERE cdEvento = '" + prmCdEvento + "' " +
                        "  AND flAtivo = 1 " +
                        "  AND cdCategoria = '00830302' " +
                        "  AND dsAuxiliar1 = '" + prmNomeCentral + "' " +
                        "  AND dsAuxiliar2 = '" + prmNomeSingular + "' " +
                        "  AND cdParticipante = '" + prmCdParticipante + "' ", SessionCnn);


                        Dap2 = new SqlDataAdapter(comando2);

                        Dap2.TableMappings.Add("Singular2", "tmpSICOOB2");
                        Dap2.Fill(DT2);

                        SessionCnn.Close(); 

                        if ((DT2 == null) || (DT2.Rows.Count == 0))
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "Limite de inscrições para esta Cooperativa Singular foi atingido.";
                            return false;
                        }
                        else
                            return true;
                    }
                }

                return true;


            }
            catch (Exception Ex)
            {
                SessionCnn.Close();
                //_erroNoCampo = true;
                lblMsg.Text = "Erro ao verificar COOPERATIVA SINGULAR!\n" + Ex.Message;
                return false;
            }
        }
        finally
        {
            SessionCnn.Close();
        }
    }

    public void ListarPatentesBID(String prmCdEvento, DropDownList prmCampoListagem, string prmFiltroCATEGORIA_MILITAR)
    {
        lblMsg.Visible = false;
        lblMsg.Text = "";

        if (SessionCnn == null)
        {
            //_erroNoCampo = true;
            lblMsg.Text = "Conexão inválida ou inexistente";
            lblMsg.Visible = true;
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
                lblMsg.Text = "Conexão inválida";
                lblMsg.Visible = true;
                return;
            }
        }

        DataTable DT = new DataTable();
        SqlDataAdapter Dap;
        try
        {
            try
            {
                SqlCommand comando = new SqlCommand(

                    "SELECT        CATEGORIA_MILITAR, PATENTE " +
                    "FROM            tmpABIMD_4BID " +
                    "WHERE       (CATEGORIA_MILITAR LIKE '%" + prmFiltroCATEGORIA_MILITAR + "%')  ", SessionCnn);

                Dap = new SqlDataAdapter(comando);

                Dap.TableMappings.Add("CATEGORIA_MILITAR", "tmpABIMD_4BID");
                Dap.Fill(DT);

                SessionCnn.Close();

                //BindingSource bsufs = new BindingSource();

                DataTable oDataTable = new DataTable();



                oDataTable.Columns.Add("CATEGORIA_MILITAR");
                oDataTable.Columns.Add("PATENTE");

                oDataTable.Rows.Add("", "");

                for (int i = 0; i < DT.DefaultView.Count; i++)
                {
                    oDataTable.Rows.Add(
                        DT.DefaultView[i]["CATEGORIA_MILITAR"],
                        DT.DefaultView[i]["PATENTE"]);
                }


                //bsufs.DataSource = oDataTable.DefaultView;
                prmCampoListagem.DataSource = oDataTable.DefaultView;
                prmCampoListagem.DataTextField = "PATENTE";
                prmCampoListagem.DataValueField = "PATENTE";
                prmCampoListagem.DataBind();

                //this.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                //this.AutoCompleteSource = AutoCompleteSource.ListItems;

            }
            catch (Exception Ex)
            {
                SessionCnn.Close();

                //_erroNoCampo = true;
                lblMsg.Text = "Erro ao selecionar CATEGORIA MILITAR!\n" + Ex.Message;
                lblMsg.Visible = true;
            }
        }
        finally
        {
            SessionCnn.Close();
        }


    }

    public void ListarCamposExpoepi(String prmCdEvento, DropDownList prmCampoListagem, string prmFiltroCATEGORIA)
    {
        lblMsg.Visible = false;
        lblMsg.Text = "";

        if (SessionCnn == null)
        {
            //_erroNoCampo = true;
            lblMsg.Text = "Conexão inválida ou inexistente";
            lblMsg.Visible = true;
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
                lblMsg.Text = "Conexão inválida";
                lblMsg.Visible = true;
                return;
            }
        }

        DataTable DT = new DataTable();
        SqlDataAdapter Dap;
        try
        {
            try
            {
                SqlCommand comando = new SqlCommand(
                    "SELECT       Categoria, Subcategoria " +
                    "FROM            dbo.tmpEXPOEPI " +
                    "WHERE       (Categoria LIKE '%" + prmFiltroCATEGORIA + "%')  ", SessionCnn);

                Dap = new SqlDataAdapter(comando);

                Dap.TableMappings.Add("Subcategoria", "tmpEXPOEPI");
                Dap.Fill(DT);

                SessionCnn.Close();

                //BindingSource bsufs = new BindingSource();

                DataTable oDataTable = new DataTable();



                oDataTable.Columns.Add("Subcategoria");
                //oDataTable.Columns.Add("Subcategoria");

                oDataTable.Rows.Add("");//, "");

                for (int i = 0; i < DT.DefaultView.Count; i++)
                {
                    oDataTable.Rows.Add(
                        //DT.DefaultView[i]["Subcategoria"],
                        DT.DefaultView[i]["Subcategoria"]
                        );
                }


                //bsufs.DataSource = oDataTable.DefaultView;
                prmCampoListagem.DataSource = oDataTable.DefaultView;
                prmCampoListagem.DataTextField = "Subcategoria";
                prmCampoListagem.DataValueField = "Subcategoria";
                prmCampoListagem.DataBind();

                //this.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                //this.AutoCompleteSource = AutoCompleteSource.ListItems;

            }
            catch (Exception Ex)
            {
                SessionCnn.Close();
                //_erroNoCampo = true;
                lblMsg.Text = "Erro ao selecionar SUBCATEGORIA!\n" + Ex.Message;
                lblMsg.Visible = true;
            }
        }
        finally
        {
            SessionCnn.Close();
        }


    }

    
    protected void btnDesconfirmar_Click(object sender, EventArgs e)
    {
        if (SessionEvento.CdEvento == "006602")
            SessionParticipante.DsAuxiliar4 = "NÃO";

        SessionParticipante = oParticipanteCad.Gravar(SessionParticipante, SessionCnn);
        Session["SessionParticipante"] = SessionParticipante;

        Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                                "040",
                                                ""), true);
    }
}
