using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using cllEventos;
using CLLFuncoes;


using System.Security.Cryptography;
using System.Text;


using System.Data;
using System.Data.SqlClient;

using MSXML2;

using System.Xml;


//using System.Net;
using System.IO;

public partial class frmCadastrarTrabalhosTecnicos_old : System.Web.UI.Page
{
    /*
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    SqlConnection SessionCnn;
    SqlConnection SessionCnn2;

    Tese SessionTese;
    TeseCad oTeseCad = new TeseCad();


    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    String SessionIdioma;
    //String SessionCateg;
    //String SessionAtv;
    */


    Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();

    Evento SessionEvento;


    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    SqlConnection SessionCnn;

    Tese SessionTese;
    TeseCad oTeseCad = new TeseCad();

    TeseParticipanteCad oTeseParticipanteCad = new TeseParticipanteCad();
    TeseParticipante SessionTeseParticipante;

    DataTable oDTCoautores;// = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        #region Código Desativado
        //if (!Page.IsPostBack)
        //{
            
        //    //local 1
        //    //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOWEzSnJjMkV0Y0dNN1NXNXBkR2xoYkNCRFlYUmhiRzluUFdSaVJYWmxiblJ2YzE5R1RUdFFaWEp6YVhOMElGTmxZM1Z5YVhSNUlFbHVabTg5VkhKMVpUdFZjMlZ5SUVsRVBYTmhPMUJoYzNOM2IzSmtQV3R5YTNOaE1UY3g=")));

        //    //local 2 note novo
        //    //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOUxseHJjbXR6WVhCak8wbHVhWFJwWVd3Z1EyRjBZV3h2Wnoxa1lrVjJaVzUwYjNOZlJrMDdVR1Z5YzJsemRDQlRaV04xY21sMGVTQkpibVp2UFZSeWRXVTdWWE5sY2lCSlJEMXpZVHRRWVhOemQyOXlaRDFyY210ellURTNNUT09")));

        //    //servidor
        //    //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOVptRjZaVzVrYjIxaGFYTTdTVzVwZEdsaGJDQkRZWFJoYkc5blBXUmlSWFpsYm5SdmMxOUdUVHRRWlhKemFYTjBJRk5sWTNWeWFYUjVJRWx1Wm04OVZISjFaVHRWYzJWeUlFbEVQWE5oTzFCaGMzTjNiM0prUFV0eWEzTmhNVGN4")));

        //    //MinSaude
        //    //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOUxseHpjV3hsZUhCeVpYTnpPMGx1YVhScFlXd2dRMkYwWVd4dlp6MWtZa1YyWlc1MGIzTmZSazA3VUdWeWMybHpkQ0JUWldOMWNtbDBlU0JKYm1adlBWUnlkV1U3VlhObGNpQkpSRDF6WVR0UVlYTnpkMjl5WkQxTGNtdHpZVEUzTVE9PQ==")));

        //    //Site
        //    //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOU1UZzVMak00TGprMUxqY3lPMGx1YVhScFlXd2dRMkYwWVd4dlp6MW1ZWHBsYm1SdmJXRnBjenRRWlhKemFYTjBJRk5sWTNWeWFYUjVJRWx1Wm04OVZISjFaVHRWYzJWeUlFbEVQV1poZW1WdVpHOXRZV2x6TzFCaGMzTjNiM0prUFZGMU5EVTBja1p0")));
        //    //Site2
        //    SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOWJYTnpjV3d1Wm1GNlpXNWtiMjFoYVhNdVkyOXRPMGx1YVhScFlXd2dRMkYwWVd4dlp6MW1ZWHBsYm1SdmJXRnBjekU3VUdWeWMybHpkQ0JUWldOMWNtbDBlU0JKYm1adlBWUnlkV1U3VlhObGNpQkpSRDFtWVhwbGJtUnZiV0ZwY3pFN1VHRnpjM2R2Y21ROVVYVTBOVFJ5Um0wPQ==")));

        //    //krksa-vaio
        //    //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOUxseHpkbkpmYTNKcmMyRTdTVzVwZEdsaGJDQkRZWFJoYkc5blBXUmlSWFpsYm5SdmMxOUdUVHRRWlhKemFYTjBJRk5sWTNWeWFYUjVJRWx1Wm04OVZISjFaVHRWYzJWeUlFbEVQWE5oTzFCaGMzTjNiM0prUFV0eWEzTmhNVGN4")));


        //    //Session["SessionCnn"] = SessionCnn;


        //    if (SessionCnn == null)
        //        SessionCnn = (SqlConnection)Session["SessionCnn"];
        //    else
        //        Session["SessionCnn"] = SessionCnn;

        //    //SessionIdioma = (String)Session["SessionIdioma"];
        //    //if (SessionIdioma == null)
        //    //{
        //    if ((Request["cdLng"] != null) &&
        //        (Request["cdLng"].ToString().Trim().ToUpper() != ""))
        //    {
        //        SessionIdioma = Request["cdLng"];
        //    }
        //    else
        //    {
        //        SessionIdioma = (String)Session["SessionIdioma"];
        //        if (SessionIdioma == null)
        //            SessionIdioma = "PTBR";
        //    }
        //    //}
        //    Session["SessionIdioma"] = SessionIdioma;

        //    string cdEvento = "";
        //    if ((Request["codEvento"] != null) &&
        //        (Request["codEvento"].ToString().Trim().ToUpper() != ""))
        //    {
        //        cdEvento = cllEventos.Crypto.DecryptStringAES(Request["codEvento"]);
        //    }
        //    if ((cdEvento.Trim() == "") || (cdEvento.Trim().Length != 6))
        //    {
        //        Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}",
        //                        "003"), true);
        //    }
        //    SessionEvento = oEventoCad.Pesquisar(cdEvento.Substring(0, 4), cdEvento, SessionCnn);
        //    Session["SessionEvento"] = SessionEvento;
        //    if (SessionEvento == null)
        //    {
        //        Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
        //                        "003",
        //                        oEventoCad.RcMsg + cdEvento.Substring(0, 4)), true);
        //    }

        //    cdEvento = SessionEvento.CdEvento;

                       

        //    if (SessionEvento.DsCon != "")
        //    {
        //        SessionCnn2 = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES(SessionEvento.DsCon)));
        //        Session["SessionCnn"] = SessionCnn2;

        //        SessionEvento = oEventoCad.Pesquisar(cdEvento.Substring(0, 4), cdEvento, SessionCnn2);
        //        Session["SessionEvento"] = SessionEvento;
        //        if (SessionEvento == null)
        //        {
        //            Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
        //                            "003",
        //                            oEventoCad.RcMsg + cdEvento.Substring(0, 4)), true);
        //        }
        //    }



        //    if ((SessionEvento.DtFinalEvento == null) ||
        //        (SessionEvento.DtFinalEvento < Geral.datahoraServidor(SessionCnn)))// DateTime.Parse(DateTime.Today.ToString("dd/MM/yyyy"))))
        //    {
        //        Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
        //                        "006",
        //                        oEventoCad.RcMsg + cdEvento.Substring(0, 4)), true);
        //    }



        //    if (SessionEvento.FlSuspenderInscricaoWeb)
        //    {
        //        Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
        //                        "007",
        //                        oEventoCad.RcMsg + cdEvento.Substring(0, 4)), true);
        //    }

        //    if ((SessionEvento.DtAberturaInscrWeb == null) ||
        //        (SessionEvento.DtAberturaInscrWeb > Geral.datahoraServidor(SessionCnn)))//DateTime.Parse(DateTime.Today.ToString("dd/MM/yyyy"))))
        //    {
        //        Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
        //                        "004",
        //                        oEventoCad.RcMsg + cdEvento.Substring(0, 4)), true);
        //    }

        //    if ((SessionEvento.DtFechamentoInscrWeb == null) ||
        //        (SessionEvento.DtFechamentoInscrWeb < Geral.datahoraServidor(SessionCnn)))//DateTime.Parse(DateTime.Today.ToString("dd/MM/yyyy"))))
        //    {
        //        if (!SessionEvento.FlLiberarCertificacaoWeb)
        //        {
        //            //lblChaveLiberacao0.Text = "Inscrições Encerradas!";
        //            //lblChaveLiberacao0.ForeColor = System.Drawing.Color.Red;
        //            //lblChaveLiberacao0.Visible = true;
        //            //btnCadastrar.Visible = false;

        //            Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
        //                            "005",
        //                            oEventoCad.RcMsg + cdEvento.Substring(0, 4)), true);
        //        }
        //    }

        //    /*
        //    if (!SessionEvento.FlAutenticacaoWeb)
        //    {
        //        //***** Criar cookies *****-/
        //        Response.Cookies["EventoInfo"]["eventoCod"] = SessionEvento.CdEvento;
        //        Response.Cookies["EventoInfo"]["lngCod"] = SessionIdioma;
        //        Response.Cookies["EventoInfo"]["categCod"] = SessionCateg;
        //        Response.Cookies["EventoInfo"]["atividadeCod"] = SessionAtv;
        //        Response.Cookies["EventoInfo"].Expires = DateTime.Now.AddDays(1);

        //        //RFVCPF.Enabled = false;

        //        if (SessionEvento.DsInformacoesCompletasWeb.Trim() != "")
        //            Response.Write("<script>window.open('frmInformacoes.aspx','_self');</script>");
        //        else if (SessionEvento.FlPesquisaCPFReceita)
        //            Response.Write("<script>window.open('frmBuscaCPFReceita.aspx','_self');</script>");
        //        else
        //            Response.Write("<script>window.open('frmCadastroAuto.aspx','_self');</script>");


        //    }
        //    else
        //    {
        //        if (SessionEvento.DsCampoAutenticacao.ToUpper().Replace("-", "") == "EMAIL")
        //        {
        //            lblEmail.Visible = txtEmail.Visible = revTxtEmail.Visible = rfvTxtEmail.Visible = true;
        //            LBLCONTA.Visible = TXTDsCPF.Visible = RFVCPF.Visible = false;
        //            txtEmail.Focus();
        //        }
        //        else
        //            TXTDsCPF.Focus();
        //    }

        //    */

        //    btnVoltar.PostBackUrl = SessionEvento.DsLinkRedirecionamento;

        //    ListarUF();

        //    oDTCoautores = new DataTable();
        //    carregarGrdSubscritoresTese();
        //    Session["oDTCoaut"] = oDTCoautores;
            
            
            
        //    /***** Criar cookies *****/
        //    Response.Cookies["EventoInfo"]["eventoCod"] = SessionEvento.CdEvento;
        //    Response.Cookies["EventoInfo"]["lngCod"] = SessionIdioma;
        //    Response.Cookies["EventoInfo"]["categCod"] = "";// SessionCateg;
        //    Response.Cookies["EventoInfo"]["atividadeCod"] = "";// SessionAtv;
        //    Response.Cookies["EventoInfo"].Expires = DateTime.Now.AddDays(1);


        //}
        //else
        //{

        //    //SessionParticipante = (Participante)Session["SessionParticipante"];

        //    SessionEvento = (Evento)Session["SessionEvento"];

        //    SessionCnn = (SqlConnection)Session["SessionCnn"];

        //    SessionIdioma = (String)Session["SessionIdioma"];

        //    //SessionCateg = (String)Session["SessionCateg"];

        //    //SessionAtv = (String)Session["SessionAtv"];

        //    if (SessionEvento == null)
        //        Server.Transfer("frmSessaoExpirada.aspx", true);


        //    oDTCoautores = (DataTable)Session["oDTCoaut"];

        //    grdSubscritor.DataSource = oDTCoautores.DefaultView;
        //    grdSubscritor.DataBind();
            
        //    //if (grdSubscritor.DataSource != null)
        //    //oDTCoautores = grdSubscritor.DataSource as DataTable;
        //}

        
        
        #endregion

        if (!Page.IsPostBack)
        {
            if (SessionCnn == null)
                SessionCnn = (SqlConnection)Session["SessionCnn"];
            else
                Session["SessionCnn"] = SessionCnn;

            if (SessionEvento == null)
                SessionEvento = (Evento)Session["SessionEvento"];
            else
                Session["SessionEvento"] = SessionEvento;

            if (SessionParticipante == null)
                SessionParticipante = (Participante)Session["SessionParticipante"];
            else
                Session["SessionParticipante"] = SessionParticipante;

            if (SessionTese == null)
                SessionTese = (Tese)Session["SessionTese"];
            else
                Session["SessionTese"] = SessionTese;


            if (SessionEvento == null)
                Server.Transfer("frmSessaoExpirada.aspx", true);

            ListarUF();

            if (SessionParticipante.DsAuxiliar6 == "")
            {
                linhaTitulacao.Visible = true;
                linhaInstituicao.Visible = true;
            }

            btnExcluirProposta.Visible = false;

            if ((Request["cdTese"] != null) &&
                (Request["cdTese"] != ""))
            {

                txtCdTese.Text = Request["cdTese"];

                Session["oDTCoaut"] = null;

                Pesquisar(Request["cdTese"]);

                btnExcluirProposta.Visible = true;
            }
            else if (SessionTese != null)
            {

                txtCdTese.Text = SessionTese.CdTese;

                Session["oDTCoaut"] = null;

                Pesquisar(SessionTese.CdTese);

                btnExcluirProposta.Visible = true;
            }
            else
            {
                txtNome.Text = SessionParticipante.NoParticipante;

                linhaDeAcordo.Visible = true;

                oDTCoautores = new DataTable();
                carregarGrdSubscritoresTese();
                Session["oDTCoaut"] = oDTCoautores;
            }


        }
        else
        {
            SessionTese = (Tese)Session["SessionTese"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionTeseParticipante = (TeseParticipante)Session["SessionTeseParticipante"];


            oDTCoautores = (DataTable)Session["oDTCoaut"];

            //grdSubscritor.DataSource = oDTCoautores.DefaultView;
            //grdSubscritor.DataBind();

        }

        if (SessionEvento == null)
            Server.Transfer("frmSessaoExpirada.aspx", true);

        //verificarIdioma();

        //if (Geral.datahoraServidor(SessionCnn) > DateTime.Parse("10/11/2013 23:59:59"))
        //{
        //    btnGravar.Visible = false;
        //    btnNovo.Visible = false;
        //    btnExcluirProposta.Visible = false;

        //    linhaArquivo.Visible = false;

        //    linhaNomeCoautor.Visible = false;

        //    grdSubscritor.Columns[1].Visible = false;
        //}

        btnExcluirProposta.Attributes.Add("onclick", "return confirm('Esta operação irá excluir em definitivo a proposta de Trabalho e seus coautores!\\n\\nConfirma a exclusão?');");

        TSManager1.RegisterPostBackControl(btnGravar);
        
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        //oDTCoautores = (DataTable)Session["oDTCoaut"];

        //grdSubscritor.DataSource = oDTCoautores.DefaultView;
        //grdSubscritor.DataBind();
    }
    protected void chkCPF_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCPF.Checked)
        {
            chkPassaporte.Checked = false;
            
            linhaCPF.Visible = true;
            linhaPassaporte.Visible = false;
            linhaCEP.Visible = true;
            linhaEndereco.Visible = true;
            linhaComplemento.Visible = true;
            linhaBairro.Visible = true;
            linhaUF.Visible = true;
            linhaCidade.Visible = true;

            linhaPais.Visible = false;
            txtPais.Text = "BRASIL";
        }
    }
    protected void chkPassaporte_CheckedChanged(object sender, EventArgs e)
    {
        if (chkPassaporte.Checked)
        {
            chkCPF.Checked = false;

            linhaCPF.Visible = false;
            linhaPassaporte.Visible = true;
            linhaCEP.Visible = false;
            linhaEndereco.Visible = false;
            linhaComplemento.Visible = false;
            linhaBairro.Visible = false;
            linhaUF.Visible = false;
            linhaCidade.Visible = false;

            linhaPais.Visible = true;
            txtPais.Text = "";
        }
    }
    public void ListarUF()
    {

        Geral oGeral = new Geral();
        txtUFRecibo.DataSource = oGeral.ListarUFs(SessionCnn);
        txtUFRecibo.DataTextField = "dsUF";
        txtUFRecibo.DataValueField = "dsUF";
        txtUFRecibo.DataBind();

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

                txtUFRecibo.SelectedValue = webcep.UF;

                txtCidadeRecibo.Text = webcep.Cidade;
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
            //string tmpCPF = prmCPF.Replace(".", "").Replace("-", "");

            return "";


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
            //return oParticipanteCad.verificarDuplicidadeCPF(prmCdParticipante, prmCdEvento, tmpCPF, prmCdCategoria, prmCnn);
            //}

        }

    }
    protected void TXTDsCPF_TextChanged(object sender, EventArgs e)
    {

        lblMsgBuscaCPF.Text = "";
        if (TXTDsCPF.Text.Replace(".", "").Replace("-", "").Length < 11)
        {
            lblMsgBuscaCPF.Text = "CPF Inválido";
            txtNome.Text = "";
            return;
        }


        string tmpCPF = ValidarCPF("", SessionEvento.CdEvento, TXTDsCPF.Text, "", SessionCnn);
        if (tmpCPF != "")
        {
            lblMsgBuscaCPF.Text = tmpCPF;
            txtNome.Text = "";
            return;
        }

        if (txtNome.Text != "")
            return;

        DataSet ds = new DataSet();
        ds.ReadXml("http://ws.fontededados.com.br/consulta.asmx/SituacaoCadastralPF?login=" +
            cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("Wm1GNlpXNWtiMjFoYVhNPQ==")) +
            "&senha=" + cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("Um0xQWFUVXpORGcy")) +
            "&cpf=" + TXTDsCPF.Text.Replace(".", "").Replace("-", ""));

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["MensagemErro"].ToString() != "")
                    lblMsgBuscaCPF.Text = ds.Tables[0].Rows[0]["MensagemErro"].ToString();
                else
                {
                    Geral.DecrementarPesquisaCPFReceita(SessionCnn);
                    //Server.Transfer("frmCadastroAuto.aspx?nome=" + ds.Tables[0].Rows[0]["Nome"].ToString() + "&cpf=" + ds.Tables[0].Rows[0]["Cpf"].ToString(), true);
                    txtNome.Text = ds.Tables[0].Rows[0]["Nome"].ToString();
                    //Response.Write("<script>window.open('frmCadastroAuto.aspx?nome=" + ds.Tables[0].Rows[0]["Nome"].ToString() + "&cpf=" + ds.Tables[0].Rows[0]["Cpf"].ToString() + "','_self');</script>"); //lblMsg.Text = ds.Tables[0].Rows[0]["Nome"].ToString();
                }
            }
        }
        else
        {
            lblMsgBuscaCPF.Text = "CPF não localizado";
            return;
        }

    }


    protected void Pesquisar(String prmCdTese)
    {
        txtCdTese.Text = "";

        txtTituloTrab.Text = "";
        TXTDsCPF.Text = "";

        txtNome.Text = "";
        txtSexo.Text = "";
        txtPassaporte.Text = "";
        txtEmail.Text = "";
        txtPais.Text = "";
        txtCEPRecibo.Text = "";
        txtEnderecoRecibo.Text = "";
        txtComplementoEnderecoRecibo.Text = "";
        txtBairroRecibo.Text = "";
        txtCidadeRecibo.Text = "";
        txtUFRecibo.Text = "";
        txtFone.Text = "";
        txtTitulacao.Text = "";
        txtInstituicao.Text = "";
        txtArea.Text = "";
        txtModalidade.Text = "";

        lblSitucao.Text = "";


        SessionTese = oTeseCad.Pesquisar(prmCdTese, SessionCnn);

        Session["SessionTese"] = SessionTese;

        if (SessionTese == null)
        {
            lblMsg.Text = oTeseCad.RcMsg;
            lblMsg0.Text = oTeseCad.RcMsg;
            return;
        }


        txtCdTese.Text = SessionTese.CdTese;

        txtModalidade.Text = SessionTese.TpTese;
        txtTituloTrab.Text = SessionTese.DsAssunto;

        lblSitucao.Text = SessionTese.DsSituacao;

        TXTDsCPF.Text = oClsFuncoes.MascaraGerar(SessionParticipante.NuCPFCNPJ, "999.999.999-99");

        txtNome.Text = SessionParticipante.NoParticipante;
        txtSexo.Text = SessionParticipante.DsSexo;
        txtPassaporte.Text = "";
        txtEmail.Text = "";
        txtPais.Text = "";
        txtCEPRecibo.Text = "";
        txtEnderecoRecibo.Text = "";
        txtComplementoEnderecoRecibo.Text = "";
        txtBairroRecibo.Text = "";
        txtCidadeRecibo.Text = "";
        txtUFRecibo.Text = "";
        txtFone.Text = "";
        txtTitulacao.Text = SessionParticipante.DsAuxiliar6;
        txtInstituicao.Text = SessionParticipante.DsAuxiliar7;

        txtArea.Text = SessionTese.NoAreaAtuacao;
        //txtModalidade.Text = SessionTese.DsModalidadeTese;

        linhaDeAcordo.Visible = false;

        carregarGrdSubscritoresTese();

    }

    protected void carregarGrdSubscritoresTese()
    {
        if (SessionTese == null)
        {
            grdSubscritor.DataSource = null;
            grdSubscritor.DataBind();


            oDTCoautores.Columns.Add("noParticipanteTese");

            

            grdSubscritor.DataSource = oDTCoautores.DefaultView;
            grdSubscritor.DataBind();
        }
        else
        {
            grdSubscritor.DataSource = null;
            grdSubscritor.DataBind();

            DataTable oDataTable = new DataTable();

            oDataTable = oTeseParticipanteCad.Listar(SessionTese.CdTese, SessionCnn);

            if (oDataTable == null)
            {
                if (oDTCoautores == null)
                    oDTCoautores = new DataTable();


                if (oDTCoautores.Columns.Count <= 0)
                    oDTCoautores.Columns.Add("noParticipanteTese");

                Session["oDTCoaut"] = oDTCoautores;
                grdSubscritor.DataSource = oDTCoautores.DefaultView;
                grdSubscritor.DataBind();

                return;
            }

            if (oDTCoautores == null)
                oDTCoautores = new DataTable();


            if (oDTCoautores.Columns.Count <= 0)
                //oDTCoautores.Columns.Add("cdParticipanteTese");
                oDTCoautores.Columns.Add("noParticipanteTese");
                //oDTCoautores.Columns.Add("tpParticipacao");


            if ((oDataTable != null) && (oDataTable.DefaultView.Count >= 1))
            {

                for (int i = 0; i < oDataTable.DefaultView.Count; i++)
                {
                    oDTCoautores.Rows.Add(//oDataTable.DefaultView[i]["cdParticipanteTese"].ToString(),
                                        oDataTable.DefaultView[i]["noParticipanteTese"].ToString()//,
                                        //oDataTable.DefaultView[i]["tpParticipacao"].ToString()
                    );
                }
            }
        }

        Session["oDTCoaut"] = oDTCoautores;
        grdSubscritor.DataSource = oDTCoautores.DefaultView;
        grdSubscritor.DataBind();
    }
    protected void btnGravarSubscritor_Click(object sender, EventArgs e)
    {
        lblMsgCoautor.Text = "";
        if (txtNoCoautor.Text == "")
        {
            lblMsgCoautor.Text = "Campo obrigatório";
            return;
        }

        

        //DataTable dt = new DataTable();

        if (oDTCoautores.Columns.Count <= 0)
            oDTCoautores.Columns.Add("noParticipanteTese");

        DataRow dr = oDTCoautores.NewRow();
        dr["noParticipanteTese"] = txtNoCoautor.Text.ToUpper();

        oDTCoautores.Rows.Add(dr);
        oDTCoautores.AcceptChanges();

        Session["oDTCoaut"] = oDTCoautores;

        grdSubscritor.DataSource = oDTCoautores.DefaultView;
        grdSubscritor.DataBind();

        txtNoCoautor.Text = "";
    }
    protected void grdSubscritor_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            
            oDTCoautores.Rows.RemoveAt(index);
            Session["oDTCoaut"] = oDTCoautores;
            grdSubscritor.DeleteRow(index);
            grdSubscritor.DataSource = oDTCoautores;
            grdSubscritor.DataBind();

            
        }
    }
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void grdSubscritor_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //if (oDTCoautores.Rows.Count > e.RowIndex)
        //{
        //    oDTCoautores.Rows.RemoveAt(e.RowIndex);
        //    grdSubscritor.DeleteRow(e.RowIndex);
        //    Session["oDTCoaut"] = oDTCoautores;
        //    grdSubscritor.DataSource = oDTCoautores.DefaultView;
        //    grdSubscritor.DataBind();
        //}
    }
    protected void btnGravarSubscritor0_Click(object sender, EventArgs e)
    {
        oDTCoautores.Rows.RemoveAt(0);
        //grdSubscritor.DeleteRow(e.RowIndex);
        Session["oDTCoaut"] = oDTCoautores;
        grdSubscritor.DataSource = oDTCoautores.DefaultView;
        grdSubscritor.DataBind();
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        oDTCoautores.Rows.RemoveAt(0);
        //grdSubscritor.DeleteRow(e.RowIndex);
        Session["oDTCoaut"] = oDTCoautores;
        grdSubscritor.DataSource = oDTCoautores.DefaultView;
        grdSubscritor.DataBind();
    }
    protected void btnExcluirTodosCoautores_Click(object sender, EventArgs e)
    {
        oDTCoautores.Rows.Clear();

        grdSubscritor.DataSource = oDTCoautores;
        grdSubscritor.DataBind();
    }
    protected void btnGravar_Click(object sender, EventArgs e)
    {


        if ((linhaDeAcordo.Visible) && (!chkDeAcordo.Checked))
        {
            lblMsg.Text = lblMsg0.Text = "Para continuar você deve concordar com as normas do evento.";
            return;
        }


        lblMsg.Text = "";
        lblMsg0.Text = "";
        //lblMsgErro.Text = "";

        string msgArquivo = "";

        if (!FileUpload1.HasFile)
        {
            msgArquivo = "Nehum arquivo selecionado para envio.";

            //return;
        }
        else
        {
            string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            //FileUpload1.SaveAs(caminho + FileUpload1.FileName);
            if (/*(extension != ".odt") &&*/ (extension != ".doc") && (extension != ".rtf"))// && (extension != ".docx"))
            {
                lblMsg.Text = "arquivo inválido!";
                lblMsg0.Text = "arquivo inválido!";
                return;
            }
        }

        Tese tmpTese = new Tese();


        tmpTese.CdTese = txtCdTese.Text;

        tmpTese.CdEvento = SessionEvento.CdEvento;
        tmpTese.TpTese = txtModalidade.Text;


        //tmpTese.DsAssunto = txtAssuntoTese.Text.Replace("'", "").Replace(";", ",");
        //tmpTese.DsIntroducao = txtIntroducao.Text.Replace("'", "").Replace(";", ",");
        //tmpTese.DsProposta = txtProposta.Text.Replace("'", "").Replace(";", ",");

        tmpTese.DsSituacao = "CADASTRADO";
        tmpTese.DsObservacao = "";


        tmpTese.DsAssunto = txtTituloTrab.Text;
        tmpTese.NuCPFAutor = SessionParticipante.NuCPFCNPJ;// TXTDsCPF.Text.Replace(".", "").Replace("-", "");
        tmpTese.DsIntroducao = "";
        tmpTese.DsProposta = "";
        
        tmpTese.DsObservacao = "";

        tmpTese.CdParticipante = SessionParticipante.CdParticipante;
        tmpTese.NoParticipante = txtNome.Text;
        tmpTese.DsSexo = txtSexo.Text;
        tmpTese.NrPassaporte = txtPassaporte.Text;
        tmpTese.DsEmail = txtEmail.Text;
        tmpTese.NoPais = txtPais.Text;
        tmpTese.NuCEP = txtCEPRecibo.Text.Replace(".","").Replace("-","");
        tmpTese.DsEndereco = txtEnderecoRecibo.Text;
        tmpTese.DsComplementoEndereco = txtComplementoEnderecoRecibo.Text;
        tmpTese.NoBairro = txtBairroRecibo.Text;
        tmpTese.NoCidade = txtCidadeRecibo.Text;
        tmpTese.DsUF = txtUFRecibo.Text;
        tmpTese.DsFone1 = txtFone.Text;
        tmpTese.NoTitulacao = txtTitulacao.Text;
        tmpTese.NoInstituicao = txtInstituicao.Text;
        tmpTese.NoAreaAtuacao = txtArea.Text;
        tmpTese.CdModalidadeTese = "";// txtModalidade.Text;


        if (SessionParticipante.DsAuxiliar6 == "")
        {
            SessionParticipante.DsAuxiliar6 = txtTitulacao.Text.ToUpper();
            SessionParticipante.DsAuxiliar7 = txtInstituicao.Text.ToUpper();

            SessionParticipante = oParticipanteCad.Gravar(SessionParticipante, SessionCnn);

            linhaTitulacao.Visible = false;
            linhaInstituicao.Visible = false;

        }
        SessionTese = oTeseCad.Gravar(SessionEvento, SessionParticipante, tmpTese, oDTCoautores, "000000001", SessionCnn);


        if (SessionTese != null)
        {
            btnExcluirProposta.Visible = true;

            linhaDeAcordo.Visible = false;

            txtCdTese.Text = SessionTese.CdTese;

            lblMsg.Text = "Sua proposta de trabalho foi gravada";

            msgArquivo = enviarArquivo(SessionTese);
            //if (!enviarArquivo(SessionTese))
            //    return;


            if (msgArquivo == "")
            {
                Geral oGeral = new Geral();
                oGeral.EnviarEmailCadastroTese(SessionEvento, SessionParticipante, SessionTese, SessionCnn);
                lblMsg.Text += " com sucesso!<br/>Sua proposta de Trabalho Técnico está em nossa base de dados. Aguarde a avaliação pela equipe técnica.";
            }
            else
                lblMsg.Text += ", porém " + msgArquivo;

            lblMsg0.Text = lblMsg.Text;
            
        }
        else
        {
            lblMsg.Text = oTeseCad.RcMsg;
            lblMsg0.Text = oTeseCad.RcMsg;
        }
    }

    protected string  enviarArquivo(Tese prmTese)
    {
        if (prmTese == null)
        {
            //lblMsg.Text = "Tese inválida";
            return "trabalho nulo";
        }

        string sURL = Request.Url.ToString().ToLower();
        string caminho = "";
        if (sURL.Contains("localhost"))
        {
            caminho = Server.MapPath("/inscricoesWeb");
            caminho += "\\trabalhos\\" + SessionEvento.CdEvento + "\\";
        }
        else
        {
            caminho = Server.MapPath("").Replace("\\inscricoesweb", "");
            caminho += "\\trabalhos\\" + SessionEvento.CdEvento + "\\";
            //lblMsg.Text = caminho;
            //return false;
        }

        if (!Directory.Exists(caminho))
        {
            Directory.CreateDirectory(caminho);
        }

        if (FileUpload1.HasFile)
        {
            try
            {
                string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                //FileUpload1.SaveAs(caminho + FileUpload1.FileName);
                if (/*(extension != ".odt") &&*/ (extension != ".doc") && (extension != ".rtf"))// && (extension != ".docx"))
                {
                    //lblMsg.Text = "Arquivo inválido!";
                    //lblMsg0.Text = "Arquivo inválido!";
                    return "arquivo inválido!";
                }

                //System.Drawing.Image UploadedImage = System.Drawing.Image.FromStream(FileUpload1.PostedFile.InputStream);

                //Label1.Text = "Height: " + UploadedImage.Height.ToString() + " / Width: " + UploadedImage.Width.ToString() + " / size" + FileUpload1.PostedFile.ContentLength.ToString();


                File.Delete(caminho + "trab_" + prmTese.CdTese + "_CPF_" + prmTese.NuCPFAutor + "_" + prmTese.NoParticipante + ".odt");
                File.Delete(caminho + "trab_" + prmTese.CdTese + "_CPF_" + prmTese.NuCPFAutor + "_" + prmTese.NoParticipante + ".doc");
                File.Delete(caminho + "trab_" + prmTese.CdTese + "_CPF_" + prmTese.NuCPFAutor + "_" + prmTese.NoParticipante + ".rtf");
                File.Delete(caminho + "trab_" + prmTese.CdTese + "_CPF_" + prmTese.NuCPFAutor + "_" + prmTese.NoParticipante + ".docx");

                FileUpload1.SaveAs(caminho + "trab_" + prmTese.CdTese+"_CPF_"+prmTese.NuCPFAutor+"_"+prmTese.NoParticipante+ extension);

                SessionTese.DsNomeArquivo = "trab_" + prmTese.CdTese + "_CPF_" + prmTese.NuCPFAutor + "_" + prmTese.NoParticipante + extension;
                SessionTese = oTeseCad.Gravar(SessionEvento, SessionParticipante, SessionTese, oDTCoautores, "000000001", SessionCnn);

                //Label1.Text = "Operação realizada com sucesso!";// string.Format("File: {0} {1} kb - Content Type {2} ",
                //FileUpload1.PostedFile.FileName, FileUpload1.PostedFile.ContentLength,
                //FileUpload1.PostedFile.ContentType);

                return "";// true;

                //lblMsg.Text = caminho + "trab_" + prmTese.CdTese + "_CPF_" + prmTese.NuCPFAutor + "_" + prmTese.NoParticipante + extension;
                //return false;

                
            }
            catch (Exception ex)
            {
                //lblMsg.Text = "ERRO: " + ex.Message.ToString();
                //lblMsg0.Text = "ERRO: " + ex.Message.ToString();

                return "ocorreu ERRO AO ENVIAR DOCUMENTO: " + ex.Message.ToString(); //false;
            }
        }
        else
        {
            //lblMsg.Text = "Você deve escolher um arquivo para o upload.";
            //lblMsg0.Text = "Você deve escolher um arquivo para o upload.";

            return "nehum arquivo foi selecionado para envio.";
        }

    }

    protected void btnNovo_Click(object sender, EventArgs e)
    {
        txtCdTese.Text = "";

        txtTituloTrab.Text = "";
        TXTDsCPF.Text = "";

        lblSitucao.Text = "";

        txtNome.Text = SessionParticipante.NoParticipante;
        txtSexo.Text  = "";
        txtPassaporte.Text = "";
        txtEmail.Text = "";
        txtPais.Text = "";
        txtCEPRecibo.Text = "";
        txtEnderecoRecibo.Text = "";
        txtComplementoEnderecoRecibo.Text = "";
        txtBairroRecibo.Text = "";
        txtCidadeRecibo.Text = "";
        txtUFRecibo.Text = "";
        txtFone.Text = "";
        txtTitulacao.Text = "";
        txtInstituicao.Text = "";
        txtArea.Text = "";
        txtModalidade.Text = "";

        linhaDeAcordo.Visible = true;

        SessionTese = null;

        oDTCoautores.Rows.Clear();

        grdSubscritor.DataSource = oDTCoautores;
        grdSubscritor.DataBind();

        chkCPF.Checked = true;

        TXTDsCPF.Focus();

        lblMsg.Text = "";
        lblMsg0.Text = "";
    }
    protected void btnExcluirProposta_Click(object sender, EventArgs e)
    {
        if (txtCdTese.Text == "")
            return;


        if (oTeseCad.ExcluirTese(txtCdTese.Text, SessionCnn))
        {

            string sURL = Request.Url.ToString().ToLower();
            string caminho = "";
            if (sURL.Contains("localhost"))
            {
                caminho = Server.MapPath("/inscricoesWeb");
                caminho += "\\trabalhos\\" + SessionEvento.CdEvento + "\\";
            }
            else
            {
                caminho = Server.MapPath("").Replace("\\inscricoesweb", "");
                caminho += "\\trabalhos\\" + SessionEvento.CdEvento + "\\";
                //lblMsg.Text = caminho;
                //return false;
            }
                       
        
            try
            {
               

                File.Delete(caminho + "trab_" + SessionTese.CdTese + "_CPF_" + SessionParticipante.NuCPFCNPJ + "_" + SessionParticipante.NoParticipante + ".odt");
                File.Delete(caminho + "trab_" + SessionTese.CdTese + "_CPF_" + SessionParticipante.NuCPFCNPJ + "_" + SessionParticipante.NoParticipante + ".doc");
                File.Delete(caminho + "trab_" + SessionTese.CdTese + "_CPF_" + SessionParticipante.NuCPFCNPJ + "_" + SessionParticipante.NoParticipante + ".rtf");
                File.Delete(caminho + "trab_" + SessionTese.CdTese + "_CPF_" + SessionParticipante.NuCPFCNPJ + "_" + SessionParticipante.NoParticipante + ".docx");
                
            }
            catch (Exception ex)
            {
                //lblMsg.Text = "ERRO: " + ex.Message.ToString();
                //lblMsg0.Text = "ERRO: " + ex.Message.ToString();

                //return "ocorreu ERRO AO ENVIAR DOCUMENTO: " + ex.Message.ToString(); //false;
            }


            btnNovo_Click(sender, e);
            lblMsg.Text = "Operação realizada com sucesso!";//<br/>Foi Enviado um e-mail com a senha para o patrocinador.";

            lblMsg0.Text = lblMsg.Text;
        }
        else
        {
            lblMsg.Text = oTeseCad.RcMsg;
            lblMsg0.Text = lblMsg.Text;
        }
    }
}

