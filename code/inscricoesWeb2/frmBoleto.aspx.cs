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

using System.Data.SqlClient;

using cllEventos;
using CLLFuncoes;

using BoletoNet;
using Microsoft.VisualBasic;

public partial class frmBoleto : System.Web.UI.Page
{
    //Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    ClsFuncoes oClsFuncoes = new ClsFuncoes();
    SqlConnection SessionCnn;
    DataTable oDTBoleto;

    private bool pedVencido = false;

    protected void Page_Load(object sender, EventArgs e)
    {

        //if (!IsPostBack)
        //{


        if ((Request["e"] == null) || (Request["b"] == null))
        {
            Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                "003",
                oEventoCad.RcMsg), true);
            return;
        }

        if ((SqlConnection) Session["SessionCnn"] == null)
        {
            //local 1
            //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOWEzSnJjMkV0Y0dNN1NXNXBkR2xoYkNCRFlYUmhiRzluUFdSaVJYWmxiblJ2YzE5R1RUdFFaWEp6YVhOMElGTmxZM1Z5YVhSNUlFbHVabTg5VkhKMVpUdFZjMlZ5SUVsRVBYTmhPMUJoYzNOM2IzSmtQV3R5YTNOaE1UY3g=")));

            //local 2
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
            SessionCnn =
                new SqlConnection(
                    cllEventos.Crypto.DecryptStringAES(
                        cllEventos.Crypto.DecryptStringAES(
                            "UkdGMFlTQlRiM1Z5WTJVOWFHcDBZWEF3YjNkcWJpNWtZWFJoWW1GelpTNTNhVzVrYjNkekxtNWxkRHRKYm1sMGFXRnNJRU5oZEdGc2IyYzlaR0pmWlhabGJuUnZjMFpOTzFCbGNuTnBjM1FnVTJWamRYSnBkSGtnU1c1bWJ6MVVjblZsTzFWelpYSWdTVVE5UTI5dVlYTmxiWE03VUdGemMzZHZjbVE5UXpCdU5EVmxiVFV5TURFMg==")));

            //Site-producao - IP
            //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOU1UZzVMak00TGprMUxqY3lPMGx1YVhScFlXd2dRMkYwWVd4dlp6MW1ZWHBsYm1SdmJXRnBjenRRWlhKemFYTjBJRk5sWTNWeWFYUjVJRWx1Wm04OVZISjFaVHRWYzJWeUlFbEVQV1poZW1WdVpHOXRZV2x6TzFCaGMzTjNiM0prUFZGMU5EVTBja1p0VFVSQg==")));

        }
        else
            SessionCnn = (SqlConnection) Session["SessionCnn"];

        Session["SessionCnn"] = SessionCnn;

        Geral oGeral = new Geral();
        if (oGeral.verificarSiteManutencao("1", SessionCnn))
        {
            Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                "05",
                ""), true);
        }

        string tpRotina = Request["tpRotina"];
        if (tpRotina == "2vBoleto")
            btnVoltar.Visible = false;

        string cdEvento = cllEventos.Crypto.DecryptStringAES(Request["e"]);
        string cdBoleto = cllEventos.Crypto.DecryptStringAES(Request["b"]);

        if ((Evento) Session["SessionEvento"] == null)
        {
            if ((cdEvento.Trim() == "") || (cdEvento.Trim().Length != 6))
            {
                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}",
                    "04"), true);
            }

            SessionEvento = oEventoCad.Pesquisar(cdEvento.Substring(0, 4), cdEvento, SessionCnn);
            Session["SessionEvento"] = SessionEvento;


            if (SessionEvento == null)
            {
                //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                //                "003",
                //                oEventoCad.RcMsg + cdEvento.Substring(0, 4)), true);

                Server.Transfer("frmSessaoExpirada.aspx", true);
            }

            //if ((SessionEvento.DtFechamentoInscrWeb == null) ||
            //    (SessionEvento.DtFechamentoInscrWeb < DateTime.Parse(DateTime.Today.ToString("dd/MM/yyyy"))))
            //{
            //    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
            //                    "005",
            //                    oEventoCad.RcMsg + cdEvento.Substring(0, 4)), true);
            //}
        }
        else
        {
            SessionEvento = (Evento) Session["SessionEvento"];
            Session["SessionEvento"] = SessionEvento;
        }


        if ((SessionEvento.DtFinalEvento == null) ||
            (SessionEvento.DtFinalEvento < DateTime.Parse(DateTime.Today.ToString("dd/MM/yyyy"))))
        {
            Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                "006",
                oEventoCad.RcMsg + cdEvento.Substring(0, 4)), true);
        }

        if (SessionEvento.FlSuspenderInscricaoWeb)
        {
            Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                "007",
                oEventoCad.RcMsg + cdEvento.Substring(0, 4)), true);
        }

        if ((SessionEvento.DtAberturaInscrWeb == null) ||
            (SessionEvento.DtAberturaInscrWeb > DateTime.Parse(DateTime.Today.ToString("dd/MM/yyyy"))))
        {
            Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                "004",
                oEventoCad.RcMsg + cdEvento.Substring(0, 4)), true);
        }


        Inscricoes oInscricoes = new Inscricoes();

        oDTBoleto = oInscricoes.PesqBoleto(cdEvento,
            cdEvento + cllEventos.Crypto.DecryptStringAES(Request["b"].ToString()), SessionCnn);


        if ((oDTBoleto == null) || (oDTBoleto.Rows.Count <= 0))
            Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                "003",
                oEventoCad.RcMsg), true);

        if ((!Boolean.Parse(oDTBoleto.DefaultView[0]["flBoletoRegistrado"].ToString())) && (!Boolean.Parse(oDTBoleto.DefaultView[0]["flBoletoRegistroTemporario"].ToString())))
        {
            if (tpRotina == "2vBoleto")
            {
                Participante SessionParticipante = new Participante();
                Session["SessionParticipante"] = SessionParticipante;
            }
            Response.Redirect("frm_mensagens.aspx?cdMensagem=042&dsMensagem=", false);
            return;
        }

        PedidoCad oPedidoCad = new PedidoCad();
        Pedido oPedido = oPedidoCad.Pesquisar(cdEvento, oDTBoleto.DefaultView[0]["cdPedido"].ToString(), SessionCnn);
        if (oPedido != null)
        {
            MeuBoletoCad oMeuBoletoCad = new MeuBoletoCad();
            if (!oPedido.FlAtivo)
            {
                this.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Atenção",
                    "<script type='text/javascript'> alert('O pedido a que se refere este boleto encontra-se inativo!\\n Verifique em sua área exclusiva ou entre em contato com a organização do evento.'); </script>",
                    false);
                pedVencido = true;
                //Response.Redirect("index.aspx?codEvento=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento));
                return;
            }


            //if ((oPedido.DtVencimentoPedido < Geral.datahoraServidor(SessionCnn).Date) && (qtdBoletosRecebidos == 0))
            //{
            //    this.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Atenção", "<script type='text/javascript'> alert('O pedido a que se refere este boleto encontra-se VENCIDO!\\n Acesse sua área exclusiva e eftue um novo pedido de inscrição.'); </script>", false);
            //    pedVencido = true;
            //    //Response.Redirect("index.aspx?codEvento=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento));
            //    return;
            //}

        }

        //Sacado 
        Participante oParticipante = oParticipanteCad.Pesquisar(SessionEvento.CdEvento,
            oDTBoleto.DefaultView[0]["cdParticipante"].ToString(), SessionCnn);

        string tmpProduto = "";
        if (SessionEvento.CdCliente == "0041")
        {
            Inscricoes tmpInscricoes = new Inscricoes();


            DataTable oDTAtividadePedido = tmpInscricoes.ListarAtividadesDoPedido(oParticipante, oPedido.CdPedido,
                SessionCnn);
            if ((oDTAtividadePedido != null) && (oDTAtividadePedido.Rows.Count > 0))
            {
                tmpProduto = oDTAtividadePedido.DefaultView[0]["dsTema"].ToString();
            }

        }

        Session["oDTBoleto"] = oDTBoleto;

        DateTime tmpdt = Geral.datahoraServidor(SessionCnn);

        //int qtdBoletosRecebidos = oMeuBoletoCad.QtdBoletosRecebidosPedido(SessionEvento.CdEvento, oDTBoleto.DefaultView[0]["cdPedido"].ToString(), SessionCnn);
        if (oPedido.DtVencimentoPedido.Value.AddDays(SessionEvento.NuDiasPrimeiroVencimento) < tmpdt) //pedido vencido
        {
            if ((oPedido.QtdParcelas == 1) ||
                ((oPedido.QtdParcelas > 1) && (oDTBoleto.DefaultView[0]["cdParcela"].ToString() == "001"))) //se 
                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                    "027",
                    oEventoCad.RcMsg), true);
        }

        string txtCarteira = oDTBoleto.DefaultView[0]["nrCarteira"].ToString();
        string txtBanco = oDTBoleto.DefaultView[0]["cdBanco"].ToString();
        string txtVencimento = DateTime.Parse(oDTBoleto.DefaultView[0]["dtVencimento"].ToString())
            .ToString("dd/MM/yyyy");

        if ((DateTime.Parse(oDTBoleto.DefaultView[0]["dtVencimento"].ToString()) < tmpdt) &&
            (tmpdt <= SessionEvento.DtLimitePagamentoBoleto))
        {
            //if (SessionEvento.CdEvento != "001304")
            //    txtVencimento = DateTime.Today.ToString("dd/MM/yyyy");//se tiver vencido dá mais um dia.
            //else
            //{
            DateTime tmpdtvenc = DateTime.Today.AddDays(SessionEvento.NuDiasPrimeiroVencimento);
            if (tmpdtvenc > SessionEvento.DtLimitePagamentoBoleto.Value)
                tmpdtvenc = SessionEvento.DtLimitePagamentoBoleto.Value;
            txtVencimento = tmpdtvenc.ToString("dd/MM/yyyy");
            //}

            MeuBoletoCad oMeuBoletoCad = new MeuBoletoCad();
            MeuBoleto oMeuBoleto = oMeuBoletoCad.Pesquisar(SessionEvento.CdEvento,
                oDTBoleto.DefaultView[0]["cdBoleto"].ToString(), SessionCnn);

            oMeuBoleto.DtVencimento = tmpdtvenc;

            oMeuBoletoCad.Gravar(oMeuBoleto, SessionCnn);
        }



        string txtValorBoleto = oDTBoleto.DefaultView[0]["vlBoleto"].ToString();
        string txtNumeroDocumentoBoleto = oDTBoleto.DefaultView[0]["cdParticipante"].ToString();
        string txtCodigoCedente = oDTBoleto.DefaultView[0]["cdCedente"].ToString();
        string txtNossoNumeroBoleto = oDTBoleto.DefaultView[0]["cdBoleto"].ToString().Substring(6, 6);
        if (txtBanco == "001")
        {
            if ((oDTBoleto.DefaultView[0]["nrCarteira"].ToString() == "18-019") ||
                (oDTBoleto.DefaultView[0]["nrCarteira"].ToString() == "17-019") ||
                (oDTBoleto.DefaultView[0]["nrCarteira"].ToString() == "17-027"))
                txtNossoNumeroBoleto = oDTBoleto.DefaultView[0]["cdBoleto"].ToString().Substring(6, 6).PadLeft(10, '0');
            else if (oDTBoleto.DefaultView[0]["cdConvenio"].ToString().Length == 7)
                txtNossoNumeroBoleto = oDTBoleto.DefaultView[0]["cdConvenio"].ToString() +
                                       oDTBoleto.DefaultView[0]["cdBoleto"].ToString().Substring(6, 6).PadLeft(10, '0');
            else if (oDTBoleto.DefaultView[0]["cdConvenio"].ToString().Length == 6)
            {
                if (txtCodigoCedente != "0")
                {
                    int complemento = 17 - txtCodigoCedente.Length;
                    txtNossoNumeroBoleto = txtCodigoCedente +
                                           oDTBoleto.DefaultView[0]["cdBoleto"].ToString()
                                               .Substring(6, 6)
                                               .PadLeft(complemento, '0');
                }
                else
                    txtNossoNumeroBoleto = oDTBoleto.DefaultView[0]["cdConvenio"].ToString() +
                                           oDTBoleto.DefaultView[0]["cdBoleto"].ToString()
                                               .Substring(6, 6)
                                               .PadLeft(11, '0');
            }
            else
                txtNossoNumeroBoleto = oDTBoleto.DefaultView[0]["cdBoleto"].ToString().Substring(6, 6).PadLeft(17, '0');
        }
        else if (txtBanco == "237")
            txtNossoNumeroBoleto = oDTBoleto.DefaultView[0]["cdBoleto"].ToString().Substring(6, 6).PadLeft(11, '0');
        else if (txtBanco == "104")
            txtNossoNumeroBoleto = "24" +
                                   oDTBoleto.DefaultView[0]["cdBoleto"].ToString().Substring(6, 6).PadLeft(15, '0');
        else if (txtBanco == "033")
            txtNossoNumeroBoleto = oDTBoleto.DefaultView[0]["cdBoleto"].ToString();

        string txtCPFCNPJ = oClsFuncoes.CPFCNPJMascarar(oDTBoleto.DefaultView[0]["nuCNPJCedente"].ToString());
        string txtNomeCedente = oDTBoleto.DefaultView[0]["noCedente"].ToString();
        string txtAgenciaCedente = oDTBoleto.DefaultView[0]["cdAgencia"].ToString();
        string txtContaCedente = oDTBoleto.DefaultView[0]["nuContaCorrente"].ToString();
        //string txtInstrucoes = HttpUtility.HtmlDecode("Não receber após o vencimento");
        string txtInstrucoes = HttpUtility.HtmlDecode(oDTBoleto.DefaultView[0]["dsTextoBoleto"].ToString())
            .Replace("#part#",
                (SessionEvento.CdCliente != "0070"
                    ? oDTBoleto.DefaultView[0]["noParticipante"].ToString()
                    : oDTBoleto.DefaultView[0]["noParticipante"].ToString() + " " +
                      oDTBoleto.DefaultView[0]["dsAuxiliar1"].ToString() + " " +
                      oDTBoleto.DefaultView[0]["dsAuxiliar2"].ToString()))
            .Replace("#tam#", oDTBoleto.DefaultView[0]["dsAuxiliar1"].ToString())
            .Replace("#prod#", tmpProduto);
        string txtConvenio = oDTBoleto.DefaultView[0]["cdConvenio"].ToString();
        string txtTpModalidade = oDTBoleto.DefaultView[0]["tpModalidade"].ToString();


        //sacado
        string txtCPFCNPJSacado = oClsFuncoes.CPFCNPJMascarar(oDTBoleto.DefaultView[0]["nuCPFCNPJRecibo"].ToString());
        string txtNomeSacado = oDTBoleto.DefaultView[0]["dsNomeRecibo"].ToString();
        string txtEnderecoSacado = oDTBoleto.DefaultView[0]["dsEnderecoRecibo"].ToString();
        string txtCompEnderecoSacado = oDTBoleto.DefaultView[0]["dsComplementoEnderRecibo"].ToString();
        string txtBairroSacado = oDTBoleto.DefaultView[0]["noBairroRecibo"].ToString();
        string txtCidadeSacado = oDTBoleto.DefaultView[0]["noCidadeRecibo"].ToString();
        string txtCEPSacado = oDTBoleto.DefaultView[0]["nuCEPRecibo"].ToString();
        string txtUFSacado = oDTBoleto.DefaultView[0]["dsUFRecibo"].ToString();
        if (!SessionEvento.FlEmiteRecibo)
        {
            txtCPFCNPJSacado = oClsFuncoes.CPFCNPJMascarar(oParticipante.NuCPFCNPJ);
            txtNomeSacado = oParticipante.NoParticipante; // oDTBoleto.DefaultView[0]["dsNomeRecibo"].ToString();
            txtEnderecoSacado = oParticipante.DsEndereco; // oDTBoleto.DefaultView[0]["dsEnderecoRecibo"].ToString();
            txtCompEnderecoSacado = oParticipante.DsComplementoEndereco;
                // oDTBoleto.DefaultView[0]["dsComplementoEnderRecibo"].ToString();
            txtBairroSacado = oParticipante.NoBairro; // oDTBoleto.DefaultView[0]["noBairroRecibo"].ToString();
            txtCidadeSacado = oParticipante.NoCidade; // oDTBoleto.DefaultView[0]["noCidadeRecibo"].ToString();
            txtCEPSacado = oParticipante.NuCEP; // oDTBoleto.DefaultView[0]["nuCEPRecibo"].ToString();
            txtUFSacado = oParticipante.DsUF; // oDTBoleto.DefaultView[0]["dsUFRecibo"].ToString();
        }

        /*
            if (SessionEvento.CdEvento == "001002")
            {
                
                if ((oParticipante.CdCategoria == "00100201") || (oParticipante.CdCategoria == "00100206") || (oParticipante.CdCategoria == "00100208"))
                {
                    //DataTable DtEmissora = 
                    ParticipantePreCadastro oParticipantePreCadastro = oParticipanteCad.PesquisarPreCadastro(oParticipante.CdEvento, "00100201", "", "", "", oParticipante.DsCampoExtraPreCad, SessionCnn);
                    if (oParticipantePreCadastro != null)
                    {
                        txtCPFCNPJSacado = oClsFuncoes.CPFCNPJMascarar(oParticipantePreCadastro.NuCPFCNPJ);//DtEmissora.DefaultView[0]["nuCPFCNPJ"].ToString());
                        txtNomeSacado = oParticipantePreCadastro.NoParticipantePrecadastro;// DtEmissora.DefaultView[0]["noParticipantePrecadastro"].ToString();
                    }
                }
            }
        */
        //if (SessionEvento.CdCliente == "0013")
        //{
        //    //Participante oParticipante = oParticipanteCad.Pesquisar(SessionEvento.CdEvento, oDTBoleto.DefaultView[0]["cdParticipante"].ToString(), SessionCnn);
        //    if (oParticipante.DsAuxiliar1 != "OUTROS")
        //    {
        //        txtCPFCNPJSacado = oParticipante.DsAuxiliar4;
        //        txtNomeSacado = oParticipante.NoInstituicao;
        //    }
        //}



        //alterei so esses
        // Vencimento: 6/3/2009 
        //Valor: 656,40
        //Nosso Número: 0000033320071 
        // Número(Documento) : B20005446()


        //Dados do Cedente
        //CPF/CNPJ: 59.323.998/0001-08 
        //Código: 0806498
        //Nome: Uniabc()
        //Agência: 432 
        //Conta: 0806498

        //if (Request["Acao"] == "visualizar")
        //{
        //    Button1_Click(sender, e);

        //}



        try
        {

            //Remove dígito da Agência.
            int DigAgencia = 0;
            int Agencia = 0;
            if (txtAgenciaCedente.IndexOf("-") > -1)
            {
                int s = txtAgenciaCedente.IndexOf("-") + 1;
                int tam = Strings.Len(txtAgenciaCedente);
                DigAgencia = Convert.ToInt32(Strings.Right(txtAgenciaCedente, tam - s));
                int dif = tam - (s - 1);
                //incluindo o traço.
                Agencia = Convert.ToInt32(Strings.Left(txtAgenciaCedente, tam - dif));
            }
            //txtAgenciaCedente

            //Remove dígito da Conta.
            int DigConta = 0;
            int Conta = 0;
            if (txtContaCedente.IndexOf("-") > -1)
            {
                int s2 = txtContaCedente.IndexOf("-") + 1;
                int tam2 = Strings.Len(txtContaCedente);
                DigConta = Convert.ToInt32(Strings.Right(txtContaCedente, tam2 - s2));
                int dif2 = tam2 - (s2 - 1);
                //incluindo o traço.
                Conta = Convert.ToInt32(Strings.Left(txtContaCedente, tam2 - dif2));
            }
            //txtContaCedente

            //Remove dígito do Cedente.
            if (txtCodigoCedente.IndexOf("-") > -1)
            {
                int s3 = txtCodigoCedente.IndexOf("-") + 1;
                int tam3 = Strings.Len(txtCodigoCedente);
                int dif3 = tam3 - (s3 - 1);
                //incluindo o traço.
                txtCodigoCedente = Strings.Left(txtCodigoCedente, tam3 - dif3);
            }


            //Validação.
            switch (txtBanco)
            {
                case "001":
                    //Banco do Brasil.

                    //Carteira com 2 caracteres.
                    //If Len(txtCarteira) <> 2 Then
                    //Response.Write("A Carteira deve conter 2 dígitos."]
                    //Exit Sub
                    //End If

                    //Agência com 4 caracteres.
                    if (Strings.Len(Agencia) > 4)
                    {
                        Response.Write("A Agência deve conter até 4 dígitos.");
                        return;
                    }


                    //Conta com 8 caracteres.
                    if (Strings.Len(Conta) > 8)
                    {
                        Response.Write("A Conta deve conter até 8 dígitos.");
                        return;
                    }


                    //Cedente com 8 caracteres.
                    if (Strings.Len(txtCodigoCedente) > 8)
                    {
                        Response.Write("O Código do Cedente deve conter até 8 dígitos.");
                        return;
                    }


                    //Nosso Número deve ser 11 ou 17 dígitos.
                    //if (Strings.Len(txtNossoNumeroBoleto) != 11 & Strings.Len(txtNossoNumeroBoleto) != 17)
                    //{
                    //    Response.Write("O Nosso Número deve ter 11 ou 17 dígitos dependendo da Carteira.");
                    //    return;
                    //}


                    break;
                //Se Carteira 18 então NossoNumero são 17 dígitos.
                //If txtCarteira = "18" Then
                // If Len(txtNossoNumeroBoleto) <> 17 Then
                // Response.Write("O Nosso Número deve ter 17 dígitos para Carteira 18."]
                // Exit Sub
                // End If
                //Else
                // 'Senão, então NossoNumero 11 dígitos.
                // If Len(txtNossoNumeroBoleto) <> 11 Then
                // Response.Write("O Nosso Número deve ter 11 dígitos para Carteira diferente que 18."]
                // Exit Sub
                // End If
                //End If

                case "033":
                    //Santander.
                    break;

                case "070":
                    //Banco BRB.
                    break;

                case "104":
                    //Caixa Econômica Federal.
                    break;

                case "237":
                    //Banco Bradesco.
                    break;

                case "275":
                    //Banco Real.

                    //Cedente 
                    if (!string.IsNullOrEmpty(Request["CodigoCedente"]))
                    {

                    }

                    //Cobrança registrada 7 dígitos.
                    //Cobrança sem registro até 13 dígitos.
                    if (Strings.Len(txtNossoNumeroBoleto) < 7 & Strings.Len(txtNossoNumeroBoleto) < 13)
                    {
                        Response.Write("O Nosso Número deve ser entre 7 e 13 caracteres.");
                        return;
                    }


                    //Carteira
                    if (txtCarteira != "00" & txtCarteira != "20" & txtCarteira != "31" & txtCarteira != "42" &
                        txtCarteira != "47" & txtCarteira != "85" & txtCarteira != "57")
                    {
                        Response.Write("A Carteira deve ser 00,20,31,42,47,57 ou 85.");
                        return;
                    }

                    //00'- Carteira do convênio
                    //20' - Cobrança Simples
                    //31' - Cobrança Câmbio
                    //42' - Cobrança Caucionada
                    //47' - Cobr. Caucionada Crédito Imobiliário
                    //85' - Cobrança Partilhada
                    //57 - última implementação ?

                    //Agência 4 dígitos.
                    if (Strings.Len(Agencia) > 4)
                    {
                        Response.Write("A Agência deve conter até 4 dígitos.");
                        return;
                    }

                    //Número da conta 6 dígitos.
                    if (Strings.Len(Conta) > 6)
                    {
                        Response.Write("A Conta Corrente deve conter até 6 dígitos.");
                        return;
                    }


                    break;
                case "291":
                    //Banco BCN.
                    break;

                case "341":
                    //Banco Itaú.
                    break;

                case "347":
                    //Banco Sudameris.
                    break;

                case "356":
                    //Banco Real.

                    //Cedente 
                    if (!string.IsNullOrEmpty(Request["CodigoCedente"]))
                    {
                    }
                    //?


                    //Cobrança registrada 7 dígitos.
                    //Cobrança sem registro até 13 dígitos.
                    if (Strings.Len(txtNossoNumeroBoleto) < 7 & Strings.Len(txtNossoNumeroBoleto) < 13)
                    {
                        Response.Write("O Nosso Número deve ser entre 7 e 13 caracteres.");
                        return;
                    }


                    //Carteira
                    if (txtCarteira != "00" & txtCarteira != "20" & txtCarteira != "31" & txtCarteira != "42" &
                        txtCarteira != "47" & txtCarteira != "85" & txtCarteira != "57")
                    {
                        Response.Write("A Carteira deve ser 00,20,31,42,47,57 ou 85.");
                        return;
                    }

                    //00'- Carteira do convênio
                    //20' - Cobrança Simples
                    //31' - Cobrança Câmbio
                    //42' - Cobrança Caucionada
                    //47' - Cobr. Caucionada Crédito Imobiliário
                    //85' - Cobrança Partilhada

                    //Agência 4 dígitos.
                    if (Strings.Len(Agencia) > 4)
                    {
                        Response.Write("A Agência deve conter até 4 dígitos.");
                        return;
                    }

                    //Número da conta 6 dígitos.
                    if (Strings.Len(Conta) > 6)
                    {
                        Response.Write("A Conta Corrente deve conter até 6 dígitos.");
                        return;
                    }


                    break;
                case "409":
                    //Banco Unibanco.
                    break;

                case "422":
                    //Banco Safra.
                    break;

                default:

                    break;
            }


            //Informa os dados do cedente
            Cedente c = new Cedente(txtCPFCNPJ, txtNomeCedente, Agencia.ToString(), DigAgencia.ToString(),
                Conta.ToString(), DigConta.ToString());

            c.Endereco.End = SessionEvento.eventoDados.DsEndereco;
            c.Endereco.Bairro = SessionEvento.eventoDados.NomBairro;
            c.Endereco.Cidade = SessionEvento.eventoDados.NomCidade;
            c.Endereco.CEP = SessionEvento.eventoDados.NuCEP;
            c.Endereco.UF = SessionEvento.eventoDados.DsUF;


            //Dependendo da carteira, é necessário informar o código do cedente (o banco que fornece)
            c.Codigo = Convert.ToInt32(txtCodigoCedente);
            c.Convenio = Convert.ToInt32(txtConvenio);
            //Dados para preenchimento do boleto (data de vencimento, valor, carteira e nosso número)
            BoletoNet.Boleto b = new BoletoNet.Boleto(Convert.ToDateTime(txtVencimento), Convert.ToDouble(txtValorBoleto), txtCarteira,
                txtNossoNumeroBoleto, c);
            //"12345678901"

            b.TipoModalidade = txtTpModalidade;

            //b.Carteira = "1"
            //b.Banco.Codigo = "18-019"

            //Dependendo da carteira, é necessário o número do documento
            b.NumeroDocumento = txtNumeroDocumentoBoleto;
            //"12345678901"



            //Informa os dados do sacado
            b.Sacado = new Sacado(txtCPFCNPJSacado, txtNomeSacado);
            b.Sacado.Endereco.End = txtEnderecoSacado + " " + txtCompEnderecoSacado;
            b.Sacado.Endereco.Bairro = txtBairroSacado;
            b.Sacado.Endereco.Cidade = txtCidadeSacado;
            b.Sacado.Endereco.CEP = txtCEPSacado;
            b.Sacado.Endereco.UF = txtUFSacado;

            //Instrução.
            switch (txtBanco)
            {
                case "001":
                    //Banco do Brasil.
                    Instrucao_BancoBrasil i1 = new Instrucao_BancoBrasil(Convert.ToInt32(txtBanco));
                    i1.Descricao = txtInstrucoes;
                    // "Não Receber após o vencimento"
                    b.Instrucoes.Add(i1);
                    break;
                case "033":
                    //Santander.
                    Instrucao_Santander i2 = new Instrucao_Santander(Convert.ToInt32(txtBanco));
                    i2.Descricao = txtInstrucoes;
                    //"Não Receber após o vencimento"
                    b.Instrucoes.Add(i2);
                    break;
                case "070":
                    //Banco BRB.
                    Instrucao i3 = new Instrucao(Convert.ToInt32(txtBanco));
                    i3.Descricao = txtInstrucoes;
                    //"Não Receber após o vencimento"
                    b.Instrucoes.Add(i3);
                    break;
                case "104":
                    //Caixa Econômica Federal.
                    Instrucao_Caixa i4 = new Instrucao_Caixa(Convert.ToInt32(txtBanco));
                    i4.Descricao = txtInstrucoes;
                    //"Não Receber após o vencimento"
                    b.Instrucoes.Add(i4);
                    break;
                case "237":
                    //Banco Bradesco.
                    Instrucao_Bradesco i5 = new Instrucao_Bradesco(Convert.ToInt32(txtBanco));
                    i5.Descricao = txtInstrucoes;
                    //"Não Receber após o vencimento"
                    b.Instrucoes.Add(i5);
                    break;
                case "275":
                    //Banco Real.
                    Instrucao i6 = new Instrucao(Convert.ToInt32(txtBanco));
                    i6.Descricao = txtInstrucoes;
                    //"Não Receber após o vencimento"
                    b.Instrucoes.Add(i6);
                    break;
                case "291":
                    //Banco BCN.
                    Instrucao i7 = new Instrucao(Convert.ToInt32(txtBanco));
                    i7.Descricao = txtInstrucoes;
                    //"Não Receber após o vencimento"
                    b.Instrucoes.Add(i7);
                    break;
                case "341":
                    //Banco Itaú.
                    Instrucao_Itau i8 = new Instrucao_Itau(Convert.ToInt32(txtBanco));
                    i8.Descricao = txtInstrucoes;
                    //"Não Receber após o vencimento"
                    b.Instrucoes.Add(i8);
                    break;
                case "347":
                    //Banco Sudameris.
                    Instrucao i9 = new Instrucao(Convert.ToInt32(txtBanco));
                    i9.Descricao = txtInstrucoes;
                    //"Não Receber após o vencimento"
                    b.Instrucoes.Add(i9);
                    break;
                case "356":
                    //Banco Real.
                    //Dim i10 As New Instrucao(CInt(txtBanco))
                    Instrucao i10 = new Instrucao(1);
                    i10.Descricao = txtInstrucoes;
                    //"Não Receber após o vencimento"
                    b.Instrucoes.Add(i10);
                    break;
                case "409":
                    //Banco Unibanco.
                    Instrucao i11 = new Instrucao(Convert.ToInt32(txtBanco));
                    i11.Descricao = txtInstrucoes;
                    //"Não Receber após o vencimento"
                    b.Instrucoes.Add(i11);
                    break;
                case "422":
                    //Banco Safra.
                    Instrucao i12 = new Instrucao(Convert.ToInt32(txtBanco));
                    i12.Descricao = txtInstrucoes;
                    //"Não Receber após o vencimento"
                    b.Instrucoes.Add(i12);
                    break;
                default:
                    //Instrução de teste Santander.
                    Instrucao_Santander i13 = new Instrucao_Santander(Convert.ToInt32(txtBanco));
                    i13.Descricao = txtInstrucoes;
                    //"Não Receber após o vencimento"
                    b.Instrucoes.Add(i13);
                    break;
            }


            //Espécie do Documento - [R] Recibo
            switch (txtBanco)
            {
                case "001":
                    //Banco do Brasil.
                    b.EspecieDocumento = new EspecieDocumento_BancoBrasil(2);
                    break;
                //Espécie.
                //Cheque = 1, //CH – CHEQUE
                //DuplicataMercantil = 2, //DM – DUPLICATA MERCANTIL
                //DuplicataMercantilIndicacao = 3, //DMI – DUPLICATA MERCANTIL P/ INDICAÇÃO
                //DuplicataServico = 4, //DS – DUPLICATA DE SERVIÇO
                //DuplicataServicoIndicacao = 5, //DSI – DUPLICATA DE SERVIÇO P/ INDICAÇÃO
                //DuplicataRural = 6, //DR – DUPLICATA RURAL
                //LetraCambio = 7, //LC – LETRA DE CAMBIO
                //NotaCreditoComercial = 8, //NCC – NOTA DE CRÉDITO COMERCIAL
                //NotaCreditoExportacao = 9, //NCE – NOTA DE CRÉDITO A EXPORTAÇÃO
                //NotaCreditoIndustrial = 10, //NCI – NOTA DE CRÉDITO INDUSTRIAL
                //NotaCreditoRural = 11, //NCR – NOTA DE CRÉDITO RURAL
                //NotaPromissoria = 12, //NP – NOTA PROMISSÓRIA
                //NotaPromissoriaRural = 13, //NPR –NOTA PROMISSÓRIA RURAL
                //TriplicataMercantil = 14, //TM – TRIPLICATA MERCANTIL
                //TriplicataServico = 15, //TS – TRIPLICATA DE SERVIÇO
                //NotaSeguro = 16, //NS – NOTA DE SEGURO
                //Recibo = 17, //RC – RECIBO
                //Fatura = 18, //FAT – FATURA
                //NotaDebito = 19, //ND – NOTA DE DÉBITO
                //ApoliceSeguro = 20, //AP – APÓLICE DE SEGURO
                //MensalidadeEscolar = 21, //ME – MENSALIDADE ESCOLAR
                //ParcelaConsorcio = 22, //PC – PARCELA DE CONSÓRCIO
                //Outros = 23 //OUTROS

                case "033":
                    //Santander.
                    b.EspecieDocumento = new EspecieDocumento_Santander(17);
                    break;
                case "070":
                    //Banco BRB.
                    b.EspecieDocumento = new EspecieDocumento(17);
                    break;
                case "104":
                    //Caixa Econômica Federal.
                    b.EspecieDocumento = new EspecieDocumento_Caixa(17);
                    break;
                case "237":
                    //Banco Bradesco.
                    b.EspecieDocumento = new EspecieDocumento(237, 1);
                    break;
                case "275":
                    //Banco Real.
                    b.EspecieDocumento = new EspecieDocumento(17);
                    break;
                case "291":
                    //Banco BCN.
                    b.EspecieDocumento = new EspecieDocumento(17);
                    break;
                case "341":
                    //Banco Itaú.
                    b.EspecieDocumento = new EspecieDocumento_Itau(99);
                    break;
                case "347":
                    //Banco Sudameris.
                    b.EspecieDocumento = new EspecieDocumento_Sudameris(17);
                    break;
                case "356":
                    //Banco Real.
                    break;
                //Não funciona com isso.
                //b.EspecieDocumento = New EspecieDocumento_BancoBrasil(17)
                //b.EspecieDocumento = New EspecieDocumento_Itau(99)
                case "409":
                    //Banco Unibanco.
                    b.EspecieDocumento = new EspecieDocumento(17);
                    break;
                case "422":
                    //Banco Safra.
                    b.EspecieDocumento = new EspecieDocumento(17);
                    break;
                case "756":
                    //Banco Safra.
                    b.EspecieDocumento = new EspecieDocumento_Sicoob("1");
                    break;
                default:
                    //Banco de teste Santander.
                    b.EspecieDocumento = new EspecieDocumento_Santander(17);
                    break;
            }


            BoletoBancario bb = new BoletoBancario();
            bb.CodigoBanco = Convert.ToInt16(txtBanco);
            //33 '-> Referente ao código do Santander
            bb.Boleto = b;
            //bb.MostrarCodigoCarteira = True
            bb.Boleto.Valida();
            //Response.Write(bb.Boleto.CodigoBarra.Codigo);
            //Response.Write(bb.Boleto.DataVencimento.ToString());
            //Response.Write(b.Cedente.Convenio.ToString());

            //true -> Mostra o compravante de entrega
            //false -> Oculta o comprovante de entrega
            bb.MostrarComprovanteEntrega = false;

            //if (SessionEvento.CdCliente != "0041")
            //    bb.Boleto.LocalPagamento = "PAGÁVEL EM QUALQUER AGÊNCIA BANCÁRIA ATÉ O VENCIMENTO.";
            //else
            //    bb.Boleto.LocalPagamento = "PAGÁVEL PREFERENCIALMENTE NAS CASAS LOTÉRICAS ATÉ O VALOR LIMITE";

            //panelDados.Visible = false;
            panelBoleto.Controls.Clear();
            if (panelBoleto.Controls.Count == 0)
            {
                panelBoleto.Controls.Add(bb);
            }

            //03399.08063 49800.000330 32007.101028 8 41680000065640 -> correta
            //03399.08063 49800.000330 32007.101028 8 41680000065640
            //03399.08063 49800.000330 32007.101028 1 41680000065640
            //03399.08063 49800.003334 20071.301012 6 41680000065640
            //03399.08063 49800.000330 32007.101028 1 41680000065640

            //03399.08063 49800.003334 20071.301020 4 41680000065640
            //03399.08063 49800.003334 20071.301020 4 41680000065640

            //Gerar remessa.
            //Dim rdr As System.IO.Stream
            //Dim arquivo As New ArquivoRemessa(TipoArquivo.CNAB400)
            //arquivo.GerarArquivoRemessa(txtCodigoCedente, b.Banco, _
            // b.Cedente, b, rdr, 1)
            //Response.Write(rdr.ToString())

            btnImprimir.Focus();

            return;
        }
        catch (Exception ex)
        {

            Response.Write(ex);

        }

        //}
        //else
        //{         
        //    SessionEvento = (Evento)Session["SessionEvento"];

        //    SessionCnn = (SqlConnection)Session["SessionCnn"];

        //    SessionParticipante = (Participante)Session["SessionParticipante"];

        //    oDTBoleto = (DataTable)Session["oDTBoleto"];
        //}
    }

    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        //Server.Transfer(string.Format("frmListaBoletosPedido.aspx?p={0}",
        //   cllEventos.Crypto.EncryptStringAES(oDTBoleto.DefaultView[0]["cdPedido"].ToString().Substring(15, 3), "3")), true);

        if (!pedVencido)
        {
            if (SessionEvento.CdCliente != "0013")
            {
                Server.Transfer(string.Format("frmListaBoletosPedido.aspx?p={0}",
                    cllEventos.Crypto.EncryptStringAES(oDTBoleto.DefaultView[0]["cdPedido"].ToString().Substring(15, 3))), true);
            }
            else
            {
                Participante tmpParticipante = (Participante)Session["SessionParticipante"];
                if ((tmpParticipante != null) && (tmpParticipante.NoAreaAtuacao == "SIM"))
                {
                    if ((Geral.datahoraServidor(SessionCnn) < DateTime.Parse("05/08/2015 00:00:00")))
                    {
                        if ((!tmpParticipante.FlDocumentoConfirmado) && ((tmpParticipante.participanteDocEnviado == null) || (tmpParticipante.participanteDocEnviado.DsSituacao == "INDEFERIDO")))
                        {
                            Response.Write("<script>window.open('frmEnviarDocumento.aspx','_self');</script>");
                        }
                    }
                }
                else
                {
                    Server.Transfer(string.Format("frmListaBoletosPedido.aspx?p={0}",
                        cllEventos.Crypto.EncryptStringAES(oDTBoleto.DefaultView[0]["cdPedido"].ToString().Substring(15, 3))), true);
                }
            }
        }
        else
        {
            Response.Redirect("index.aspx?codEvento=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento));
        }
    }
    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        Response.Write("<script>window.print();</script>");
    }
}
