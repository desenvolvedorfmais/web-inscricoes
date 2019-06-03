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

public partial class frmDadosRecibo : System.Web.UI.Page
{
    Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    SqlConnection SessionCnn;

    Pedido SessionPedido;
    PedidoCad oPedidoCad = new PedidoCad();

   
    

    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            if (SessionCnn == null)
                SessionCnn = (SqlConnection)Session["SessionCnn"];
            else
                Session["SessionCnn"] = SessionCnn;

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

            //CategoriaCad oCategoriaCad = new CategoriaCad();
            //SessionCategoria = oCategoriaCad.Pesquisar(SessionEvento.CdEvento, SessionParticipante.CdCategoria, SessionCnn);

            //if (!SessionCategoria.FlAtividades)
            //{
            //    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
            //                    "008",
            //                    oEventoCad.RcMsg), true);
            //}

            //Session["SessionCategoria"] = SessionCategoria;
            lblCategoria.Text = SessionParticipante.Categoria.NoCategoria.Trim();
            if (SessionEvento.CdEvento == "005503")
                lblCategoria.Text += " / " + SessionParticipante.NoInstituicao;

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

                DataTable dtpedido = oPedidoCad.TotalizarPedido(SessionPedido.CdPedido, SessionCnn);
                if (dtpedido != null)
                {
                    vlItens.Text = dtpedido.DefaultView[0]["itens"].ToString();
                    vlTotalAtiv.Text = decimal.Parse(dtpedido.DefaultView[0]["tol_Atv"].ToString()).ToString("N2");
                    vlTotalDesc.Text = decimal.Parse(dtpedido.DefaultView[0]["tot_desc"].ToString()).ToString("N2");

                    vlTotalAtiv.Font.Strikeout = (decimal.Parse(vlTotalDesc.Text) > 0);
                }

                vlTotalPedido.Text = SessionPedido.VlTotalPedido.ToString("N2");

            }
            else
            {
                SessionPedido = oPedidoCad.SelUltimoPedido(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);
                Session["SessionPedido"] = SessionPedido;

                if (SessionPedido != null)
                {
                    if (SessionPedido.TpPagamento == "")
                    {
                        Server.Transfer(string.Format("frm_formapagamento.aspx?cdMatricula={0}",
                                                        SessionParticipante.CdParticipante), true);
                    }

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

                    
                }
                else
                {
                    if (SessionParticipante != null)
                    {
                        if (SessionParticipante.Categoria.FlAtividades)
                        {
                            if ((SessionEvento.CdCliente == "0013")// &&
                                )
                            {
                                Inscricoes oInscricoes = new Inscricoes();

                                DataTable dt = oInscricoes.ListarAtividadesDoParticipante(SessionParticipante, SessionCnn);

                                if (((dt != null) && (dt.Rows.Count > 0))
                                    && (SessionParticipante.CdCategoria != "00130409") && (SessionParticipante.CdCategoria != "00130509"))
                                {
                                    Response.Write("<script>window.open('frmCadastroAuto.aspx','_self');</script>");
                                }
                                else
                                    Response.Write("<script>window.open('frmSelAtividades.aspx','_self');</script>");

                            }
                            else
                                Response.Write("<script>window.open('frmSelAtividades.aspx','_self');</script>");
                        }
                        else
                            Response.Write("<script>window.open('frmCadastroAuto.aspx','_self');</script>");

                    }
                }
            }


            ListarUF();

            //ListarCidades("");

            Pesquisar();

            if (!SessionEvento.FlPesquisaCPFReceita)
            {
                btnDadosParticipante.Visible = false;
                //btnDadosInstituicao.Visible = false;
            }

            


            txtTipoPessoa.Focus();
        }
        else
        {
            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            SessionPedido = (Pedido)Session["SessionPedido"];

            if (SessionEvento == null)
                Server.Transfer("frmSessaoExpirada.aspx", true);

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

    protected void ListarCidades(string prmUF)
    {
        //Geral oGeral = new Geral();

        //txtCidadeRecibo.DataSource = oGeral.ListarCidades(prmUF, SessionCnn);
        //txtCidadeRecibo.DataTextField = "dsMunicipio";
        //txtCidadeRecibo.DataValueField = "dsMunicipio";
        //txtCidadeRecibo.DataBind();
    }

    protected void Pesquisar()
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
            txtTipoPessoa.SelectedValue = SessionPedido.TpPessoa;
            if (txtTipoPessoa.SelectedValue.ToString() == "PF")
            {
                linhaCPF.Visible = true;
                linhaCNPJ.Visible = false;
                linhaIE.Visible = false;
                lblNome.Text = "Nome*";
                txtCPFRecibo.Text = oClsFuncoes.CPFCNPJMascarar(SessionPedido.NuCPFCNPJRecibo);
            }
            else if (txtTipoPessoa.SelectedValue.ToString() == "PJ")
            {
                linhaCPF.Visible = false;
                linhaCNPJ.Visible = true;
                linhaIE.Visible = true;
                lblNome.Text = "Razão Social*";
                txtCNPJRecibo.Text = oClsFuncoes.CPFCNPJMascarar(SessionPedido.NuCPFCNPJRecibo);
                txtIE.Text = SessionPedido.DsInscricaoEstadualRecibo;
            }

            txtTipoPessoa.AutoPostBack = true;

            txtNomeRecibo.Text = SessionPedido.DsNomeRecibo;
            txtUFRecibo.Text = SessionPedido.DsUFRecibo;
            txtCidadeRecibo.Text = SessionPedido.NoCidadeRecibo;
            txtEnderecoRecibo.Text = SessionPedido.DsEnderecoRecibo;
            txtComplementoEnderecoRecibo.Text = SessionPedido.DsComplementoEnderRecibo;
            txtBairroRecibo.Text = SessionPedido.NoBairroRecibo;
            txtCEPRecibo.Text = oClsFuncoes.MascaraGerar(SessionPedido.NuCEPRecibo, "99.999-999");

            //if (SessionPedido.TpPagamento.Contains("EMPENHO"))
            //    pnlRespFin.Visible = true;

            if (SessionPedido.TpPagamento.Contains("EMPENHO"))
            {
                pnlResponsavelFinanceiro.Visible = true;

               

                txtTipoPessoa.SelectedValue = "PJ";
                txtTipoPessoa.Enabled = false;
                //EventArgs e = new EventArgs();
                //txtTipoPessoa_SelectedIndexChanged(txtTipoPessoa, e);

            }
            else
            {
                if (SessionEvento.CdEvento == "008501")
                {
                    
                    txtTipoPessoa.SelectedValue = "PF";
                    txtTipoPessoa.Enabled = true;
                    //EventArgs e = new EventArgs();
                    //txtTipoPessoa_SelectedIndexChanged(txtTipoPessoa, e);
                }
            }

            txtNomeRespFin.Text = SessionPedido.DsNomeResponsavelFinanceiro;
            txtEmailRespFin.Text = SessionPedido.DsEmailResponsavelFinanceiro;
            txtFoneRespFin.Text = oClsFuncoes.MascaraGerar(SessionPedido.DsFoneResponsavelFinanceiro, "(99) 999999999"); ;
            txtRamalRespFin.Text = SessionPedido.DsRamalResponsavelFinanceiro;
        }
    }

   
    protected void btnConfirmar_Click(object sender, EventArgs e)
    {
        //this.MessageBox1.ShowConfirmation("Deseja gravar os dados?", "Gravar", true, false);

        
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
        lblMsg.Visible = false;
        lblMsg.Text = "";
        if (lblIdentificador.Text.Trim() == "")
            return;

        if (SessionPedido.TpPagamento.Contains("EMPENHO"))
        {
            if (txtEmailRespFin.Text == "")
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Informe o e-mail do departamento responsável pelo pagamento!";
                txtEmailRespFin.Focus();
                return;
            }

            if (txtFoneRespFin.Text == "")
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Informe o fone do departamento responsável pelo pagamento!";
                txtFoneRespFin.Focus();
                return;
            }
        }

        SessionPedido.TpPessoa = txtTipoPessoa.SelectedValue.ToString();
        SessionPedido.DsNomeRecibo = txtNomeRecibo.Text;
        SessionPedido.NuCPFCNPJRecibo = txtTipoPessoa.SelectedValue.ToString() == "PF" ? txtCPFRecibo.Text.Replace(".", "").Replace("-", "").Replace("/", "") : txtCNPJRecibo.Text.Replace(".", "").Replace("-", "").Replace("/", "");
        SessionPedido.DsUFRecibo = txtUFRecibo.Text;
        SessionPedido.NoCidadeRecibo = txtCidadeRecibo.Text;
        SessionPedido.DsEnderecoRecibo = txtEnderecoRecibo.Text;
        SessionPedido.DsComplementoEnderRecibo = txtComplementoEnderecoRecibo.Text;
        SessionPedido.NoBairroRecibo = txtBairroRecibo.Text;
        SessionPedido.NuCEPRecibo = txtCEPRecibo.Text;
        SessionPedido.DsInscricaoEstadualRecibo = txtIE.Text;

        SessionPedido.DsNomeResponsavelFinanceiro = txtNomeRespFin.Text.ToUpper();
        SessionPedido.DsEmailResponsavelFinanceiro = txtEmailRespFin.Text.ToLower();
        SessionPedido.DsFoneResponsavelFinanceiro = txtFoneRespFin.Text.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "");
        SessionPedido.DsRamalResponsavelFinanceiro = txtRamalRespFin.Text.ToUpper();
       
        

        SessionPedido = oPedidoCad.Gravar(SessionPedido, SessionCnn);
        Session["SessionPedido"] = SessionPedido;

        
        

        Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                "014-1",
                                ""), true);

       
       

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
        /*
        try
        {
            //XMLHTTP http = new XMLHTTP();

            //http.open("POST", "http://webservice.uni5.net/web_cep.php?auth=8739185af299ee7b1463b579a90fee5a&formato=xml&cep=" + txtCEPRecibo.Text.Replace(".", ""), false, null, null);
            //http.send(null);


            //XmlDocument xmlDoc = new XmlDocument();


            //xmlDoc.LoadXml(http.responseText);

            //XmlNode No = xmlDoc.SelectSingleNode("webservicecep");

            //txtEnderecoRecibo.Text = No.ChildNodes.Item(5).InnerText.ToUpper() + " " +
            //                         No.ChildNodes.Item(6).InnerText.ToUpper(); //logradouro

            //txtBairroRecibo.Text = No.ChildNodes.Item(4).InnerText.ToUpper();//bairro

            //txtCidadeRecibo.Text = No.ChildNodes.Item(3).InnerText.ToUpper();//cidade

            //txtUFRecibo.SelectedValue = No.ChildNodes.Item(2).InnerText.ToUpper();//UF

            string filename = @"http://webservice.uni5.net/web_cep.php?auth=8739185af299ee7b1463b579a90fee5a&formato=xml&cep=" + txtCEPRecibo.Text.Replace(".", "");
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
                                    lblMsgCEP.Text = "Cep não encontrado";
                                }
                                else if (strTempValue == "-2")
                                {
                                    lblMsgCEP.Text = "Formato de CEP inválido";
                                }
                                else if (strTempValue == "-3")
                                {
                                    lblMsgCEP.Text = "Busca de CEP congestionada. Aguarde alguns segundos e tente novamente.";
                                }
                                else
                                {
                                    lblMsgCEP.Text = "Erro na busca de CEP.";
                                }
                                lblMsgCEP.Visible = true;
                            }
                            break;
                    }
                }
            } while (reader.Read());

            txtEnderecoRecibo.Text = tipoLogradouro + " " + logradouro;

            txtBairroRecibo.Text = bairro;

            txtUFRecibo.SelectedValue = uf;

            txtCidadeRecibo.Text = cidade;
        }
        catch
        {
            lblMsgCEP.Visible = true;
        }
         */ 
    }


    protected void txtTipoPessoa_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtNomeRecibo.Text = "";
        txtUFRecibo.Text = "";
        txtCidadeRecibo.Text = "";
        txtEnderecoRecibo.Text = "";
        txtComplementoEnderecoRecibo.Text = "";
        txtBairroRecibo.Text = "";
        txtCEPRecibo.Text = "";
        txtIE.Text = "";

        if (txtTipoPessoa.SelectedValue.ToString() == "PF")
        {
            linhaCPF.Visible = true;
            linhaCNPJ.Visible = false;
            linhaIE.Visible = false;
            lblNome.Text = "Nome*";            
        }
        else if (txtTipoPessoa.SelectedValue.ToString() == "PJ")
        {
            linhaCPF.Visible = false;
            linhaCNPJ.Visible = true;
            linhaIE.Visible = true;
            lblNome.Text = "Razão Social*";
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
            
            lblMsg2.Text = "";
            if (txtCPFRecibo.Text.Replace(".", "").Replace("-", "").Length < 11)
            {
                lblMsg2.Text = "CPF Inválido";
                return;
            }

            string tmpCPF = ValidarCPF("", SessionEvento.CdEvento, txtCPFRecibo.Text, SessionCnn);
            if (tmpCPF != "")
            {
                lblMsg2.Text = tmpCPF;
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
                                    lblMsg2.Text = "Não foi possível localizar os dados do CPF informado!<br/>Preencha os campos manualmente.";
                                    lblMsg2.Visible = true;
                                    return;
                                }
                                lblMsg2.Text = "Não foi possível localizar os dados do CPF informado!<br/>Preencha os campos manualmente.";
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
                    lblMsg2.Text = "Não foi possível localizar os dados do CPF informado!<br/>Preencha os campos manualmente.";
                }
                //else
                //{
                //    lblMsg2.Text = "Não foi possível localizar os dados do CPF informado!<br/>Preencha os campos manualmente.";
                //}
            }

        }
    }

    protected string ValidarCPF(string prmCdParticipante, string prmCdEvento, string prmCPF, SqlConnection prmCnn)
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
        lblMsg2.Text = "";
        if (txtCNPJRecibo.Text == "")
        {
            lblMsg2.Text = "Campo obrigatório.";
            return;
        }

        //PESQUISAR CNPJ BANCO LOCAL
        SqlConnection SessionCnnHISTORICO = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOWJYTnpjV3d1Wm1GNlpXNWtiMjFoYVhNdVkyOXRPMGx1YVhScFlXd2dRMkYwWVd4dlp6MW1ZWHBsYm1SdmJXRnBjekU3VUdWeWMybHpkQ0JUWldOMWNtbDBlU0JKYm1adlBWUnlkV1U3VlhObGNpQkpSRDFtWVhwbGJtUnZiV0ZwY3pFN1VHRnpjM2R2Y21ROVVYVTBOVFJ5Um0wPQ==")));
        DataTable DTCNPJ = oParticipanteCad.PesquisaCNPJ(txtCNPJRecibo.Text.Replace(".", "").Replace("-", "").Replace("/", ""), SessionCnnHISTORICO);
        if ((DTCNPJ != null) && (DTCNPJ.Rows.Count > 0))
        {
            txtNomeRecibo.Text = DTCNPJ.DefaultView[0]["NomeEmpresa"].ToString();
            txtUFRecibo.Text = DTCNPJ.DefaultView[0]["UF"].ToString();
            txtCidadeRecibo.Text = DTCNPJ.DefaultView[0]["Municipio"].ToString();
            txtEnderecoRecibo.Text = DTCNPJ.DefaultView[0]["Logradouro"].ToString();
            txtComplementoEnderecoRecibo.Text = DTCNPJ.DefaultView[0]["Complemento"].ToString() ;
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
                                lblMsg2.Text = "Não foi possível localizar os dados do CNPJ informado!<br/>Preencha os campos manualmente.";
                                lblMsg2.Visible = true;
                                return;
                            }
                            lblMsg2.Text = "Não foi possível localizar os dados do CNPJ informado!<br/>Preencha os campos manualmente.";
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

                            txtUFRecibo.Text = ds.Tables[0].Rows[0]["UF"].ToString();
                            txtCidadeRecibo.Text = ds.Tables[0].Rows[0]["Municipio"].ToString();
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
                    lblMsg2.Text = "Não foi possível localizar os dados do cnpj informado!<br/>Preencha os campos manualmente.";
                    return;
                }
            }
            else 
            {
                lblMsg2.Text = "Não foi possível localizar os dados do cnpj informado!<br/>Preencha os campos manualmente.";
            }
        }
    }
    protected void txtCPFRecibo_TextChanged(object sender, EventArgs e)
    {
        btnDadosParticipante_Click(sender, e);
    }
}
