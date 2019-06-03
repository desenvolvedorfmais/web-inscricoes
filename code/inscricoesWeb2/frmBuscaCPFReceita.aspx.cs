using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Data;
using System.Data.SqlClient;

using CLLFuncoes;
using cllEventos;

//using MSXML2;

using System.Xml;
//using Recaptcha;
using MSCaptcha;

public partial class frmBuscaCPFReceita : System.Web.UI.Page
{
    Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();

    CategoriaCad oCategoriaCad = new CategoriaCad();

    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    SqlConnection SessionCnn;

    String SessionIdioma;

    String SessionCateg;

    String SessionAtv;

    String SessionChaveLibercao;

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

            SessionCateg = (String)Session["SessionCateg"];
            if (SessionCateg == null)
                SessionCateg = "";
            Session["SessionCateg"] = SessionCateg;

            SessionAtv = (String)Session["SessionAtv"];
            if (SessionAtv == null)
                SessionAtv = "";
            Session["SessionAtv"] = SessionAtv;



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
                Server.Transfer("frmSessaoExpirada.aspx", true);
            
            SessionChaveLibercao = (String)Session["SessionChaveLibercao"];
            if (SessionChaveLibercao == null)
                SessionChaveLibercao = "";


            if (SessionEvento.CdEvento == "006501")
                msgPreviaCaptcha.Text = "<p>&nbsp;</p><p><span style=\"font-family: helvetica, arial, sans-serif; font-size: 68px; color: #999999;\">INSCRI&Ccedil;&Otilde;ES</span></p><p>&nbsp;</p><p>As inscri&ccedil;&otilde;es v&atilde;o at&eacute; o dia 18 de maio.<br />D&uacute;vidas podem ser encaminhadas para o e-mail <a href=\"mailto:seminariosanabb@anabb.org.br\">seminariosanabb@anabb.org.br</a></p><p>&nbsp;</p>";


            txtCPF.Focus();

        }
        else
        {
            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"]; 
            
            SessionIdioma = (String)Session["SessionIdioma"];

            SessionCateg = (String)Session["SessionCateg"];

            SessionChaveLibercao = (String)Session["SessionChaveLibercao"];

            SessionAtv = (String)Session["SessionAtv"];
        }

        ToolkitScriptManager1.RegisterPostBackControl(Button1);

        if (SessionEvento == null)
            Server.Transfer("frmSessaoExpirada.aspx", true);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        if (txtCPF.Text.Replace(".", "").Replace("-", "").Length < 11)
        {
            lblMsg.Text = "CPF Inválido";
            return;
        }

        if (txtDtNascimento.Text.Replace("/", "").Replace(" ", "").Length < 8)
        {
            lblMsg.Text = "Data Inválida";
            return;
        }

        try 
        {
           DateTime.Parse(txtDtNascimento.Text);
        }
        catch
        {
            lblMsg.Text = "Data Inválida";
            return;
        }

        //if (txtCaptcha.Text.Trim() == "")
        //{
        //    lblMsg.Text = "Caracteres inválidos";
        //    return;
        //}

        //Captcha1.ValidateCaptcha(txtCaptcha.Text.Trim());

        //if (!Captcha1.UserValidated)
        //{
        //    lblMsg.Text = "Caracteres inválidos!";
        //    lblMsg.Visible = true;
        //    return;
        //}

        string tmpCPF = ValidarCPF("", SessionEvento.CdEvento, txtCPF.Text, SessionCateg, SessionCnn);
        if (tmpCPF != "")
        {
            if (SessionEvento.CdEvento != "006501")
            {
                lblMsg.Text = tmpCPF;
                return;
            }
            else
            {
                if (tmpCPF == "CPF já cadastrado")
                {
                    SessionParticipante = oParticipanteCad.Pesquisar(SessionEvento.CdEvento, txtCPF.Text.Replace(".", "").Replace("-", "").Trim(), SessionCnn);                    
                    if (SessionParticipante.Categoria.FlAtividades)
                    {
                        Inscricoes oInscricoes = new Inscricoes();
                        DataTable dtTemp = oInscricoes.ListarAtividadesDoParticipante(SessionParticipante, SessionCnn);
                        if ((dtTemp == null) || (dtTemp.Rows.Count < 2))
                        {
                            Session["SessionParticipante"] = SessionParticipante;
                            Response.Write("<script>window.open('frmSelAtividades.aspx','_self');</script>");
                            return;
                        }
                    }
                    
                }
                

                lblMsg.Text = tmpCPF;
                return;
                
            }
        }

        
        string tempCPF = txtCPF.Text.Replace(".", "").Replace("-", "");

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
                SessionEvento.FlPesquisaCPFReceita = false;
                SessionEvento.FlUtilizarDadoEventoAnterior = false;
                Session["SessionEvento"] = SessionEvento;
                Server.Transfer("frmCadastroAuto.aspx?nome=&cpf=11111111111", true);
            }
            else
            {
                lblMsg.Text = "CPF Inválido!";
            }
        }
        else
        {
            //PESQUISAR CPF BANCO LOCAL
            SqlConnection SessionCnnHISTORICO = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOWJYTnpjV3d1Wm1GNlpXNWtiMjFoYVhNdVkyOXRPMGx1YVhScFlXd2dRMkYwWVd4dlp6MW1ZWHBsYm1SdmJXRnBjekU3VUdWeWMybHpkQ0JUWldOMWNtbDBlU0JKYm1adlBWUnlkV1U3VlhObGNpQkpSRDFtWVhwbGJtUnZiV0ZwY3pFN1VHRnpjM2R2Y21ROVVYVTBOVFJ5Um0wPQ==")));
            DataTable DTCpf = oParticipanteCad.PesquisaCPF(txtCPF.Text.Replace(".", "").Replace("-", ""), SessionCnnHISTORICO);
            if ((DTCpf != null) && (DTCpf.Rows.Count > 0))
            {

                Server.Transfer("frmCadastroAuto.aspx?nome=" + DTCpf.DefaultView[0]["Nome"].ToString() + "&cpf=" + DTCpf.DefaultView[0]["nuCPF"].ToString() + "&dtnasc=" + txtDtNascimento.Text, true);

            }
            else
            {
                //PESQUISAR CPF BANCO RECEITA
                int tmpSaldoPesqCPF = Geral.verificarTotalPesquisaCPF(SessionCnn);

                if (tmpSaldoPesqCPF > 0)
                {
                    try
                    {

                        

                        DataSet ds = new DataSet();
                        ds.ReadXml("http://ws.fontededados.com.br/consulta.asmx/SituacaoCadastralPF?login=" +
                            cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("Wm1GNlpXNWtiMjFoYVhNPQ==")) +
                            "&senha=" + cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("Um0xQWFUVXpORGcy")) +
                            "&cpf=" + txtCPF.Text.Replace(".", "").Replace("-", "") +
                            "&dataNascimento=" + DateTime.Parse(txtDtNascimento.Text).ToString("yyyy/MM/dd") );

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
                                        lblMsg.Text = ds.Tables[0].Rows[0]["MensagemErro"].ToString();
                                        lblMsg.Visible = true;
                                        return;
                                    }
                                    Server.Transfer("frmCadastroAuto.aspx?nome=&cpf=" + txtCPF.Text.Replace(".", "").Replace("-", ""), true);
                                    //Server.Transfer("frmCadastroRepresentante.aspx?nome=&cpf=" + txtCPF.Text.Replace(".", "").Replace("-", ""), true);
                                }
                                else
                                {
                                    oParticipanteCad.IncluirCPF(ds.Tables[0].Rows[0]["Cpf"].ToString().Replace(".", "").Replace("-", ""), ds.Tables[0].Rows[0]["Nome"].ToString(), SessionCnnHISTORICO);
                                    Geral.DecrementarPesquisaCPFReceita(SessionCnn);
                                    Server.Transfer("frmCadastroAuto.aspx?nome=" + ds.Tables[0].Rows[0]["Nome"].ToString() + "&cpf=" + ds.Tables[0].Rows[0]["Cpf"].ToString() + "&dtnasc=" + ds.Tables[0].Rows[0]["DataNascimento"].ToString(), true);
                                    //Response.Write("<script>window.open('frmCadastroAuto.aspx?nome=" + ds.Tables[0].Rows[0]["Nome"].ToString() + "&cpf=" + ds.Tables[0].Rows[0]["Cpf"].ToString() + "','_self');</script>"); //lblMsg.Text = ds.Tables[0].Rows[0]["Nome"].ToString();
                                }
                            }
                        }
                    }
                    catch
                    { 
                        Server.Transfer("frmCadastroAuto.aspx?nome=&cpf=" + txtCPF.Text.Replace(".", "").Replace("-", ""), true); 
                    }
                }
                else if (tmpSaldoPesqCPF == 0)
                {
                    Server.Transfer("frmCadastroAuto.aspx?nome=&cpf=" + txtCPF.Text.Replace(".", "").Replace("-", ""), true);
                }
                else
                {
                    lblMsg.Text = "Não foi possível localizar os dados do CPF informado!<br/>Favor entrar em contato com a coordenação do evento.";
                }
                
                    
            }
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
}