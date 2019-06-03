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

public partial class frmInscricaoConfirmada : System.Web.UI.Page
{
    ClsFuncoes oclsfuncoes = new ClsFuncoes();

    Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();
    Evento SessionEvento;
    // EventoCad oEventoCad = new EventoCad();

    Pedido SessionPedido;

    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    SqlConnection SessionCnn;

    String SessionIdioma;


    String SessionTipoSistema;

    string tpRotina;

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

            if ((Request["tpSist"] != null) &&
                    (Request["tpSist"].ToString().Trim().ToUpper() != ""))
            {
                SessionTipoSistema = Request["tpSist"];
            }
            else
            {
                SessionTipoSistema = (String)Session["SessionTipoSistema"];
                if (SessionTipoSistema == null)
                    SessionTipoSistema = "NRM";
            }
            Session["SessionTipoSistema"] = SessionTipoSistema;

            if (SessionParticipante == null)
                SessionParticipante = (Participante)Session["SessionParticipante"];
            else
                Session["SessionParticipante"] = SessionParticipante;

            tpRotina = Request["tpRotina"];
            tpRotina = (String)Session["tpRotina"];
            if (tpRotina == null)
                tpRotina = "";
            Session["tpRotina"] = tpRotina;

            if (SessionEvento == null)
                SessionEvento = (Evento)Session["SessionEvento"];
            else
                Session["SessionEvento"] = SessionEvento;

            if (SessionPedido == null)
                SessionPedido = (Pedido)Session["SessionPedido"];
            else
                Session["SessionPedido"] = SessionPedido;


            if ((SessionEvento == null) && (tpRotina == ""))
                Server.Transfer("frmSessaoExpirada.aspx", true);

            Button1.Visible = false;
            Button2.Visible = false;

            lblMsg.Text = EnviarEmailConfirmaCadastro(SessionEvento, SessionParticipante, SessionCnn);

            lblMsg.Focus();

            
            
        }
        else
        {
            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            SessionIdioma = (String)Session["SessionIdioma"];

            SessionTipoSistema = (String)Session["SessionTipoSistema"];

            tpRotina = (string)Session["tpRotina"];

        }

        if (SessionEvento == null)
            Server.Transfer("frmSessaoExpirada.aspx", true);
    }

    protected void verificarIdioma(string prmIdioma)
    {
        //if (prmIdioma == "PTBR")
        //{
        //    SessionIdioma = "PTBR";
        //    Session["SessionIdioma"] = SessionIdioma;

        //    lblTituloPagina.Text = "Matrículas / Inscrições";
        //    lblTituloResumo.Text = "Resumo do Pedido";


        //    lblPart.Text = "Participante:";
        //    lblCateg.Text = "Categoria:";

        //    lblResPed.Text = "Pedido nº";
        //    lblResItens.Text = "Itens";
        //    lblResVlr.Text = "Valor";
        //    lblResDesc.Text = "Desconto";
        //    lblResVlrTotal.Text = "Total";

        //    lblMsgParticipante.Text =
        //        "Prezado participante, o PayPal é a forma escolhida para gerenciar nossos recebimentos.<br /> " +
        //        "Para concluir sua inscrição você será direcionado para o PayPal, onde poderá selecionar a melhor forma de pagamento para você.";

        //    btnContinuar.Text = "Continuar";

        //}
        //else if (prmIdioma == "ENUS")
        //{
        //    SessionIdioma = "ENUS";
        //    Session["SessionIdioma"] = SessionIdioma;

        //    lblTituloPagina.Text = "Payment";
        //    lblTituloResumo.Text = "Order summary";

        //    lblPart.Text = "Participant:";
        //    lblCateg.Text = "Category:";

        //    lblResPed.Text = "Order No.";
        //    lblResItens.Text = "Items";
        //    lblResVlr.Text = "Value";
        //    lblResDesc.Text = "Discount";
        //    lblResVlrTotal.Text = "Total";

        //    lblMsgParticipante.Text =
        //        "Dear participant, PayPal is the way chosen to manage our collections. <br />" +
        //        "To complete your registration you will be directed to PayPal where you can select the best payment method for you.";

        //    btnContinuar.Text = "Continue";
        //}
        //else if (prmIdioma == "ESP")
        //{
        //    SessionIdioma = "ESP";
        //    Session["SessionIdioma"] = SessionIdioma;

        //    lblTituloPagina.Text = "El pago";
        //    lblTituloResumo.Text = "Resumen de la solicitud";

        //    lblPart.Text = "Participante:";
        //    lblCateg.Text = "Categoría";

        //    lblResPed.Text = "Solicitud Nº";
        //    lblResItens.Text = "Artículos";
        //    lblResVlr.Text = "Valor";
        //    lblResDesc.Text = "Descuento";
        //    lblResVlrTotal.Text = "Total";

        //    lblMsgParticipante.Text =
        //        "Estimado participante, PayPal es la forma elegida para administrar nuestras colecciones.<br />" +
        //        "Para completar su inscripción será dirigido a PayPal, donde puede seleccionar el mejor método de pago para usted.";

        //    btnContinuar.Text = "Continuar";
        //}
        //else if (prmIdioma == "FRA")
        //{
        //    SessionIdioma = "FRA";
        //    Session["SessionIdioma"] = SessionIdioma;

        //    lblTituloPagina.Text = "Paiement";
        //    lblTituloResumo.Text = "Résumé de la demande";

        //    lblPart.Text = "Participant:";
        //    lblCateg.Text = "Catégorie:";

        //    lblResPed.Text = "Demande no";
        //    lblResItens.Text = "Articles";
        //    lblResVlr.Text = "Valeur";
        //    lblResDesc.Text = "Réduction";
        //    lblResVlrTotal.Text = "Total";

        //    lblMsgParticipante.Text =
        //        "Cher participant, PayPal est le moyen choisi pour gérer nos collections.<br />" +
        //        "Pour compléter votre inscription, vous serez dirigé vers PayPal, où vous pouvez choisir la meilleure méthode de paiement pour vous.";

        //    btnContinuar.Text = "Continuer";
        //}
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //Server.Transfer(string.Format("frmCadastroAuto.aspx?cdMatricula={0}",
        //                                   SessionParticipante.CdParticipante), true);
        //Server.Transfer(string.Format("frmCadastroAuto.aspx"), true);

        //Response.Write("<script>window.open('frmCadastroAuto.aspx?cdMatricula=" + SessionParticipante.CdParticipante + "','_self');</script>");
        //Response.Write("<script>window.open('frmCadastroAuto.aspx','_self');</script>");
        //if (SessionEvento.CdEvento == "005502")
        //    Server.Transfer(string.Format("frmCertificado.aspx?p={0}",
        //                               cllEventos.Crypto.EncryptStringAES(SessionParticipante.CdParticipante)), true);

        //if ((SessionEvento.CdEvento == "006501") && (SessionEvento.DsLinkRedirecionamento != ""))
        //    Response.Write("<script>window.open('" + SessionEvento.DsLinkRedirecionamento + "','_self');</script>");
    }

    public string EnviarEmailConfirmaCadastro(Evento prmEvento, Participante prmParticipante, SqlConnection prmCnn)
    {
        

        EventoCad oEventoCad = new EventoCad();
        EventoDados oEventoDados = oEventoCad.PesquisarDadosEvento(prmEvento.CdEvento, prmCnn);
        EventoCarteira oEventoCarteira = oEventoCad.PesquisarCarteira(prmEvento.CdEvento, prmCnn);

        if ((prmParticipante.DsIdioma != "") && (prmParticipante.DsIdioma != "PTBR"))
        {
            return EnviarEmailConfirmaCadastroOutrosIdiomas(prmEvento, prmParticipante, prmCnn);
           

        }

       

        bool atividadeEscolhida = false;

        string tmpTratamento = prmEvento.CdCliente != "0003" ? "Senhor(a)" : "Congressista";
        string tmpGenero = ((prmEvento.CdCliente == "0013") || (prmEvento.CdCliente == "0003")) ? "no" : prmEvento.CdEvento == "003003" ? "para participar da" : "no Evento:";

        string body = "";
        



        if ((prmEvento.CdEvento != "004801") && (prmEvento.CdEvento != "005503") && (prmEvento.CdEvento != "008701") && (prmEvento.CdEvento != "009401"))
        {
            body +=
                             "<br/><p style='font-family:\"Arial\"; font-size: 12pt;'>Prezado(a) " + tmpTratamento + ", <b>" + prmParticipante.NoParticipante + "</b>,<br/></p>" +

                             "<p style='font-family:\"Arial\"; font-size: 12pt;'>Você está <b>inscrito(a)</b> " + tmpGenero + " <b>" + prmEvento.NoEvento + "</b>. <br/></p>";
            if ((prmEvento.CdCliente != "0003") && (prmEvento.CdEvento != "001305"))
            {
                body +=
                                 "<p style='font-family:\"Arial\"; font-size: 12pt;'>" +
                                 "Para facilitar o processo de seu credenciamento e evitar filas, tenha seu código de cadastro abaixo e apresente-o juntamente com seu documento de identificação no dia do evento.</p><br/> " +
                                 "<center> " +
                                 "<div style='font-weight: bold; font-size: 14pt; vertical-align: middle; width: 296px; " +
                                     "color: white; font-family: Arial; height: 38px; background-color: gray; text-align: center; padding-top: 10px;'> CÓDIGO: " +
                                     prmParticipante.CdParticipante +
                                 "</div> " +
                                 "</center> ";
            }
            else if (prmEvento.CdCliente == "0003")
            {
                body += "<p style='font-family:\"Arial\"; font-size: 12pt;'>" +
                                 "Durante o credenciamento será solicitado o número do CPF.</p>";
            }
            

            body +=
                             "<br /> ";
            // "<p style='font-family:\"Arial\"; font-size: 12pt;'>" +
            //  "Informe este número junto com um <b>documento de identificação</b> no balcão de credenciamento</p><br/><br/> " +
            if (prmEvento.CdCliente != "0003")
                body += DetalhesDaMatricula(prmParticipante, prmCnn) + "<br/>";


            if (prmEvento.CdEvento == "001004")
            {
                Inscricoes oInscricoes = new Inscricoes();
                DataTable oDTPed = oInscricoes.ListarAtividadesDoParticipante(prmParticipante, prmCnn);

                if (oDTPed != null)
                {
                    for (int i = 0; i < oDTPed.Rows.Count; i++)
                    {
                        if (((oDTPed.DefaultView[i]["cdAtividade"].ToString() == "001002002") ||
                             (oDTPed.DefaultView[i]["cdAtividade"].ToString() == "001002003")) ||
                            ((oDTPed.DefaultView[i]["cdAtividade"].ToString() == "001004002") ||
                             (oDTPed.DefaultView[i]["cdAtividade"].ToString() == "001004003") ||
                             (oDTPed.DefaultView[i]["cdAtividade"].ToString() == "001004004") ||
                             (oDTPed.DefaultView[i]["cdAtividade"].ToString() == "001004005") ||
                             (oDTPed.DefaultView[i]["cdAtividade"].ToString() == "001004006") ||
                             (oDTPed.DefaultView[i]["cdAtividade"].ToString() == "001004007"))
                            )
                        {
                            body += "<br /><p><span style='font-family:Arial'>1ª Opção de hotel: " + prmParticipante.DsAuxiliar18 + "</span>";
                            body += "<br /><span style='font-family:Arial'>2ª Opção de hotel: " + prmParticipante.DsAuxiliar19 + "</span></p>";
                        }
                    }
                }
            }

            body += "<br/><br/><p style='font-family:\"Arial\"; font-size: 12pt;'>";

            if (prmEvento.CdCliente != "0014")
            {
                if (prmEvento.CdCliente != "0003")
                    body += "Agradecemos pela participação.</p><br/><br/> ";
                else
                    body += "Seja bem-vindo(a)!</p><br/><br/> ";// ao  <b>" + prmEvento.NoEvento + "</b>! </p><br/><br/> ";
            }
            

        }
        else if (prmEvento.CdEvento == "004801")
        {
            body +=
                             "<p style='font-family:\"Arial\"; font-size: 12pt;'>Saudações, </p><br/><br/>" +

                             "<p style='font-family:\"Arial\"; font-size: 12pt;'>Confirmamos a sua inscrição para participar do I Congresso do PRB Mulher – O Futuro da Mulher em Pauta, que será realizado nos dias 08, 09 e 10 de agosto no Centro de Convenções Ulisses Guimarães em Brasília. </p>" +

                             "<p style='font-family:\"Arial\"; font-size: 12pt;'>" +
                             "Para facilitar o processo de seu credenciamento, tenha seu código de cadastro abaixo e apresente-o juntamente com seu documento de identificação no dia do evento.</p><br/> " +
                             "<center> " +
                             "<div style='font-weight: bold; font-size: 14pt; vertical-align: middle; width: 296px; " +
                                 "color: white; font-family: Arial; height: 38px; background-color: gray; text-align: center; padding-top: 10px;'> CÓDIGO: " +
                                 prmParticipante.CdParticipante +
                             "</div> " +
                             "</center> " +

                             "<br/><br/>" +

                             "<p style='font-family:\"Arial\"; font-size: 12pt;'> " +

                            "Avante Republicanas!</p><br/><br/> ";
        }
        else if (prmEvento.CdEvento == "005503")
        {
            body +=
                "<p>&nbsp;</p> " +
                "<p style=\"text-align: center;\"><strong>CONFIRMA&Ccedil;&Atilde;O DE INSCRI&Ccedil;&Atilde;O</strong></p> " +
                "<p><br />Prezado(a), #NOME#</p> " +
                "<p><br />Confirmamos a sua inscri&ccedil;&atilde;o para o SEMIN&Aacute;RIO NACIONAL NTU 2016, que acontecer&aacute; nos dias 23 e 24 de agosto, " +
                "no Royal Tulip Bras&iacute;lia Alvorada, em Bras&iacute;lia-DF.</p> " +
                "<p><br />Nome: <strong>#NOME#</strong><br />Categoria: <strong>#CATEGORIA#</strong><br />Raz&atilde;o social: <strong>#INSTITUICAO#</strong> " +
                "<br />Endere&ccedil;o:&nbsp;<strong>#ENDERECO#</strong><br />Bairro: <strong>#BAIRRO#</strong><br />Cidade / UF:&nbsp;<strong>#CIDADE# - #UF#</strong>" +
                "<br />Fone:&nbsp;<strong>#FONE#</strong></p> " +
                "#ATIVIDADE#" +
                "<p><br />Atenciosamente,</p> " +
                "<p>Coordena&ccedil;&atilde;o do Semin&aacute;rio Nacional NTU</p> " +
                "<p>Informa&ccedil;&otilde;es sobre o evento:<br />Fone: +55 61 2103-9293<br />E-mail: seminario@ntu.org.br</p> " +
                "<p>&nbsp;</p> ";

            body = body.Replace("#NOME#", prmParticipante.NoParticipante);
            body = body.Replace("#CATEGORIA#", prmParticipante.Categoria.NoCategoria);
            body = body.Replace("#INSTITUICAO#", prmParticipante.NoInstituicao);
            body = body.Replace("#ENDERECO#", prmParticipante.DsEndereco + " " + prmParticipante.DsComplementoEndereco);
            body = body.Replace("#BAIRRO#", prmParticipante.NoBairro);
            body = body.Replace("#CIDADE#", prmParticipante.NoCidade);
            body = body.Replace("#UF#", prmParticipante.DsUF);
            body = body.Replace("#FONE#", oclsfuncoes.MascaraGerar(prmParticipante.DsFone1, "(99) 9999999999"));

            if (prmParticipante.CdCategoria == "00550301")
            {
                Inscricoes oInscricoes = new Inscricoes();
                DataTable oDTPed = oInscricoes.ListarAtividadesDoParticipante(prmParticipante, prmCnn);

                if ((oDTPed != null) && (oDTPed.Rows.Count == 2))
                {
                    atividadeEscolhida = true;
                    body = body.Replace("#ATIVIDADE#", "<p>Atividade escolhida: <strong>" + oDTPed.DefaultView[1]["noTitulo"].ToString() + "</strong></p>");
                }
                else
                    body = body.Replace("#ATIVIDADE#", "");
            }
            else
                body = body.Replace("#ATIVIDADE#", "");

        }
        //else if (prmEvento.CdEvento == "008701")
        //{
        //    body += CorpoEmailABIMDE(prmParticipante);
        //}
        //else if (prmEvento.CdEvento == "009401")
        //{
        //    body += CorpoEmailEXPOEPI(prmParticipante, prmCnn)
        //        .Replace("#LOCAL_EVENTO", prmEvento.DsEvento);
        //}

        body += prmEvento.DsCorpoMensagensEmail;


        return body;


    }

    public string EnviarEmailConfirmaCadastroOutrosIdiomas(Evento prmEvento, Participante prmParticipante, SqlConnection prmCnn)
    {
        

        EventoCad oEventoCad = new EventoCad();
        EventoDados oEventoDados = oEventoCad.PesquisarDadosEvento(prmEvento.CdEvento, prmCnn);
        EventoCarteira oEventoCarteira = oEventoCad.PesquisarCarteira(prmEvento.CdEvento, prmCnn);

        

        string tmpTratamento = prmEvento.CdCliente != "0003" ? "Senhor(a)" : "Congressista";
        string tmpGenero = prmEvento.CdCliente == "0013" ? "no" : "no Evento:";

        string body = "";

        if ((prmEvento.CdCliente != "0070") && (prmEvento.CdEvento != "008701"))
        {
            if (prmParticipante.DsIdioma == "ENUS")
            {
                body += "<p>Dear Mr. (Mrs.) <b>" + prmParticipante.NoParticipante + "</b>, congratulations!</p><br/>" +

                                    "<p>You are enrolled in the Event: <b>" + prmEvento.NoEvento + "</b>. </p>" +

                                    "<p style='font-family:\"Arial\";'>" +
                                    "To expedite the process of accreditation at the event, following your registration code:</p><br/> " +
                                    "<center> " +
                                    "<div style='font-weight: bold; font-size: 14pt; vertical-align: middle; width: 296px; " +
                                        "color: white; font-family: Arial; height: 38px; background-color: gray; text-align: center; padding-top: 10px;'> " +
                                        prmParticipante.CdParticipante +
                                    "</div> " +
                                    "</center> " +
                                    "<br /> " +
                                    "<p style='font-family:\"Arial\";'>" +
                                    "Inform this number along with an <b>identification document</b> at the counter of accreditation</p><br/><br/> " +

                                    DetalhesDaMatricula(prmParticipante, prmCnn) + "<br/>" +

                                    "<p style='font-family:\"Arial\";'>";

                body += "Thanks for participating.</p><br/><br/> ";
            }
            else if (prmParticipante.DsIdioma == "ESP")
            {
                body += "<p>Estimado Sr. (Sra.) <b>" + prmParticipante.NoParticipante + "</b>, ¡felicitaciones!</p><br/>" +

                                    "<p>Está inscrito en el evento: <b>" + prmEvento.NoEvento + "</b>. </p>" +

                                    "<p style='font-family:\"Arial\";'>" +
                                    "Para acelerar el proceso de acreditación en el evento, a raíz de su código de registro:</p><br/> " +
                                    "<center> " +
                                    "<div style='font-weight: bold; font-size: 14pt; vertical-align: middle; width: 296px; " +
                                        "color: white; font-family: Arial; height: 38px; background-color: gray; text-align: center; padding-top: 10px;'> " +
                                        prmParticipante.CdParticipante +
                                    "</div> " +
                                    "</center> " +
                                    "<br /> " +
                                    "<p style='font-family:\"Arial\";'>" +
                                    "Informe este número junto con un <b>documento de identificación</b> en el mostrador de acreditación</p><br/><br/> " +

                                    DetalhesDaMatricula(prmParticipante, prmCnn) + "<br/>" +

                                    "<p style='font-family:\"Arial\";'>";

                body += "Gracias por participar.</p><br/><br/> ";
            }
            else if (prmParticipante.DsIdioma == "FRA")
            {
                body += "<p>Chers M. (Mme) <b>" + prmParticipante.NoParticipante + "</b>, félicitations!</p><br/>" +

                                    "<p>Vous êtes inscrit à l'événement: <b>" + prmEvento.NoEvento + "</b>. </p>" +

                                    "<p style='font-family:\"Arial\";'>" +
                                    "Pour accélérer le processus d'accréditation lors de l'événement, à la suite de votre code d'enregistrement:</p><br/> " +
                                    "<center> " +
                                    "<div style='font-weight: bold; font-size: 14pt; vertical-align: middle; width: 296px; " +
                                        "color: white; font-family: Arial; height: 38px; background-color: gray; text-align: center; padding-top: 10px;'> " +
                                        prmParticipante.CdParticipante +
                                    "</div> " +
                                    "</center> " +
                                    "<br /> " +
                                    "<p style='font-family:\"Arial\";'>" +
                                    "Entrez dans ce numéro avec un <b>document d'identification</b> au comptoir d'accréditation</p><br/><br/> " +

                                    DetalhesDaMatricula(prmParticipante, prmCnn) + "<br/>" +

                                    "<p style='font-family:\"Arial\";'>";

                body += "Merci de votre participation.</p><br/><br/> ";
            }

            //prmEvento.DsCorpoMensagensEmail +
        }
        else if (prmEvento.CdCliente == "0070")
        {
            PedidoCad opedCad = new PedidoCad();
            Pedido prmPedido = opedCad.SelUltimoPedido(prmEvento.CdEvento, prmParticipante.CdParticipante, prmCnn);

            body +=
                "<p><span style=\"font-family: helvetica, arial, sans-serif; font-size: 10pt;\">&nbsp;</span></p> " +
                "<p><span style=\"font-family: helvetica, arial, sans-serif; font-size: 10pt;\">PLEASE RETAIN THIS E-MAIL FOR YOUR RECORDS.</span></p> " +
                "<p><span style=\"font-family: helvetica, arial, sans-serif; font-size: 10pt;\">&nbsp;&nbsp;</span></p> " +
                "<p><span style=\"font-family: helvetica, arial, sans-serif; font-size: 10pt;\">Dear Mr/Ms #NOME #SOBRENOME,</span></p> " +
                "<p><span style=\"font-family: helvetica, arial, sans-serif; font-size: 10pt;\">&nbsp;</span></p> " +
                "<p><span style=\"font-family: helvetica, arial, sans-serif; font-size: 10pt;\">Thank you for registering for the 2<sup>nd</sup> BIO Latin America (BLA), to be held on October 14<sup>th</sup>-16<sup>th</sup> 2015 at the Sheraton Rio Hotel &amp; Resort in Rio de Janeiro, Brazil.</span></p> " +
                "<p><span style=\"font-family: helvetica, arial, sans-serif; font-size: 10pt;\">&nbsp;</span></p> ";

            if (prmPedido != null)
            {
                body +=
                    "<p><span style=\"font-family: helvetica, arial, sans-serif; font-size: 10pt; color: #339966;\"><strong>The payment of your registration has been confirmed.</strong></span></p> " +
                    "<p><span style=\"font-family: helvetica, arial, sans-serif; font-size: 10pt;\">&nbsp;</span></p> ";
            }

            body +=
                "<p><span style=\"font-family: helvetica, arial, sans-serif; font-size: 10pt;\"><strong>Find below your registration details:</strong></span></p> " +
                "<p><span style=\"font-family: helvetica, arial, sans-serif; font-size: 10pt;\">CPF/Passport Number/CNPJ: #CPF_PASSPORT_CNPJ</span></p> " +
                "<p><span style=\"font-family: helvetica, arial, sans-serif; font-size: 10pt;\">First Name: #NOME</span></p> " +
                "<p><span style=\"font-family: helvetica, arial, sans-serif; font-size: 10pt;\">Middle Name: #MEIO_NOME</span></p> " +
                "<p><span style=\"font-family: helvetica, arial, sans-serif; font-size: 10pt;\">Last Name: #SOBRENOME</span></p> " +
                "<p><span style=\"font-family: helvetica, arial, sans-serif; font-size: 10pt;\">Category: #CATEGORIA</span></p> " +
                "<p><span style=\"font-family: helvetica, arial, sans-serif; font-size: 10pt;\">Company Name: #EMPRESA</span></p> " +
                "<p><span style=\"font-family: helvetica, arial, sans-serif; font-size: 10pt;\">Job Title: #CARGO</span></p> " +
                "<p><span style=\"font-family: helvetica, arial, sans-serif; font-size: 10pt;\">Address: #ENDERECO</span></p> " +
                "<p><span style=\"font-family: helvetica, arial, sans-serif; font-size: 10pt;\">City/State/Province: #CIDADE</span></p> " +
                "<p><span style=\"font-family: helvetica, arial, sans-serif; font-size: 10pt;\">ZIP/Postal Code: #CEP</span></p> " +
                "<p><span style=\"font-family: helvetica, arial, sans-serif; font-size: 10pt;\">Country: #PAIS</span></p> " +
                "<p><span style=\"font-family: helvetica, arial, sans-serif; font-size: 10pt;\">Phone Number(s): #TELEFONE</span></p> " +
                "<p><span style=\"font-family: helvetica, arial, sans-serif; font-size: 10pt;\">E-mail: #EMAIL</a></span></p> " +
                "<p><span style=\"font-family: helvetica, arial, sans-serif; font-size: 10pt;\">Website: #WEBSITE</span></p> " +
                "<p><span style=\"font-family: helvetica, arial, sans-serif; font-size: 10pt;\">Order Number: #NUM_PEDIDO</span></p> " +
                "<p><span style=\"font-family: helvetica, arial, sans-serif; font-size: 10pt;\">Total: #VALOR_PEDIDO</span></p> " +
                "<p><span style=\"font-family: helvetica, arial, sans-serif; font-size: 10pt;\">&nbsp;</span></p> " +
                "<p><span style=\"font-family: helvetica, arial, sans-serif; font-size: 10pt;\">When checking-in at the Conference, please inform the registration code below and present an identification document.</span></p> " +
                "<p><span style=\"font-family: helvetica, arial, sans-serif; font-size: 10pt;\">&nbsp;</span></p> " +
                "<center> " +
                "<div style=\"font-weight: bold; font-size: 14pt; vertical-align: middle; width: 296px; color: white; font-family: Arial; height: 38px; background-color: gray; text-align: center; padding-top: 10px;\">#CODIGO</div> " +
                "</center> " +
                "<p><span style=\"font-family: helvetica, arial, sans-serif; font-size: 10pt;\">&nbsp;</span></p> " +
                "<p><span style=\"font-family: helvetica, arial, sans-serif; font-size: 10pt;\">Your login details for the BIO One-on-One Partnering&trade; system will be sent to your e-mail early September.</span></p> " +
                "<p><span style=\"font-family: helvetica, arial, sans-serif; font-size: 10pt;\">&nbsp;</span></p> " +
                "<p><span style=\"font-family: helvetica, arial, sans-serif; font-size: 10pt;\">All registration fees are nonrefundable.  Cancelation requests received on or before <b>September 9, 2015</b> will receive a 30% credit to apply to next year's conference or a conference in 2015.  Requests received after <b>September 9, 2015</b> will <b>NOT</b> be honored.  Email cancelation requests to registration.bla@biominas.org.br.</span></p> " +
                "<p><span style=\"font-family: helvetica, arial, sans-serif; font-size: 10pt;\">&nbsp;</span></p> " +
                "<p><span style=\"font-family: helvetica, arial, sans-serif; font-size: 10pt;\">In case of doubts, please, do not hesitate to contact <a href=\"mailto:bla@biominas.org.br\">bla@biominas.org.br</a>.</span></p> " +
                "<p><span style=\"font-family: helvetica, arial, sans-serif; font-size: 10pt;\">&nbsp;</span></p> " +
                "<p><span style=\"font-family: helvetica, arial, sans-serif; font-size: 10pt;\">We look forward to seeing you at BLA 2015!</span></p> " +
                "<p><span style=\"font-family: helvetica, arial, sans-serif; font-size: 10pt;\">&nbsp;</span></p> " +
                "<p><span style=\"font-family: helvetica, arial, sans-serif; font-size: 10pt;\">&nbsp;</span></p> " +
                "<p><span style=\"font-family: helvetica, arial, sans-serif; font-size: 10pt;\">Our very best regards,</span></p> " +
                "<p><span style=\"font-family: helvetica, arial, sans-serif; font-size: 10pt;\">BIO Latin America Team</span></p> " +
                "<p>&nbsp;</p> ";


            decimal tmpVlFinalPed = prmPedido.VlTotalPedido;
            if (prmPedido.pedidoCupomDesconto != null)
                tmpVlFinalPed = prmPedido.VlTotalPedido - prmPedido.pedidoCupomDesconto.VlDescontoCalculado;


            body = body.Replace("#NOME", prmParticipante.NoParticipante);
            body = body.Replace("#MEIO_NOME", prmParticipante.DsAuxiliar1);
            body = body.Replace("#SOBRENOME", prmParticipante.DsAuxiliar2);
            body = body.Replace("#CPF_PASSPORT_CNPJ", prmParticipante.DsAuxiliar3);
            body = body.Replace("#CATEGORIA", prmParticipante.Categoria.NoCategoria);
            body = body.Replace("#EMPRESA", prmParticipante.NoInstituicao);
            body = body.Replace("#CARGO", prmParticipante.NoCargo);
            body = body.Replace("#ENDERECO", prmParticipante.DsAuxiliar5);
            body = body.Replace("#CIDADE", prmParticipante.NoCidade + " - " + prmParticipante.DsAuxiliar6);
            body = body.Replace("#CEP", prmParticipante.DsAuxiliar7);
            body = body.Replace("#PAIS", BuscarPaisIdioma(prmParticipante.NoPais, prmParticipante.DsIdioma, prmCnn));
            body = body.Replace("#TELEFONE", prmParticipante.DsFone1 + " / " + prmParticipante.DsFone2);
            body = body.Replace("#EMAIL", prmParticipante.DsEmail + ", " + prmParticipante.DsAuxiliar4);
            body = body.Replace("#WEBSITE", prmParticipante.DsAuxiliar8);

            if (prmPedido != null)
            {
                body = body.Replace("#NUM_PEDIDO", prmPedido.CdPedido);
                body = body.Replace("#VALOR_PEDIDO", tmpVlFinalPed.ToString("N2"));
            }
            body = body.Replace("#CODIGO", prmParticipante.CdParticipante);
        }
        //else if (prmEvento.CdEvento == "008701")
        //{
        //    body += CorpoEmailABIMDE(prmParticipante);
        //}


        return body;
        
    }

    public string DetalhesDaMatricula(Participante prmParticipante, SqlConnection prmCnn)
    {

        Inscricoes oInscricoes = new Inscricoes();
        DataTable oDTPed = oInscricoes.ListarAtividadesDoParticipante(prmParticipante, prmCnn);


        string titulo_resumo = prmParticipante.DsIdioma == "PTBR" ? "DADOS DE INSCRIÇÃO" : prmParticipante.DsIdioma == "ENUS" ? "SUMMARY OF THE ORDER" : prmParticipante.DsIdioma == "ESP" ? "RESUMEN DE LA ORDEN" : "RÉSUMÉ DE L'ORDRE";
        string nome = prmParticipante.DsIdioma == "PTBR" ? "Participante" : prmParticipante.DsIdioma == "ENUS" ? "Participant" : prmParticipante.DsIdioma == "ESP" ? "Participante" : "Participant";
        string Categoria = prmParticipante.DsIdioma == "PTBR" ? "Categoria" : prmParticipante.DsIdioma == "ENUS" ? "Category" : prmParticipante.DsIdioma == "ESP" ? "Categoría" : "Catégorie";
        string categ = prmParticipante.DsIdioma == "PTBR" ? prmParticipante.Categoria.NoCategoria : prmParticipante.DsIdioma == "ENUS" ? prmParticipante.Categoria.NoCategoriaIngles : prmParticipante.DsIdioma == "ESP" ? prmParticipante.Categoria.NoCategoriaEspanhol : prmParticipante.Categoria.NoCategoriaFrances;

        string titulo_grid = prmParticipante.DsIdioma == "PTBR" ? "Itens do Pedido" : prmParticipante.DsIdioma == "ENUS" ? "Items" : prmParticipante.DsIdioma == "ESP" ? "Artículos" : "Articles";
        string lbltpItem = prmParticipante.DsIdioma == "PTBR" ? "Tipo" : prmParticipante.DsIdioma == "ENUS" ? "Type" : prmParticipante.DsIdioma == "ESP" ? "Tipo" : "Type";
        string lbldeItem = prmParticipante.DsIdioma == "PTBR" ? "De" : prmParticipante.DsIdioma == "ENUS" ? "From" : prmParticipante.DsIdioma == "ESP" ? "De" : "De";
        string lblateItem = prmParticipante.DsIdioma == "PTBR" ? "a" : prmParticipante.DsIdioma == "ENUS" ? "To" : prmParticipante.DsIdioma == "ESP" ? "a" : "à";
        string lbllocalItem = prmParticipante.DsIdioma == "PTBR" ? "Local" : prmParticipante.DsIdioma == "ENUS" ? "Site" : prmParticipante.DsIdioma == "ESP" ? "Local" : "Local";

        string body = "";

        body +=

            "<p><span style='font-family:Arial; font-size: 12pt;'><b>" + titulo_resumo + "</b></span><br/>";

        if ((prmParticipante.DsIdioma == "PTBR") && (prmParticipante.NuCPFCNPJ != ""))
            body += "<p><span style='font-family:Arial; font-size: 12pt;'>CPF: </span>" +
                            "<b><span style='font-family:Arial; font-size: 12pt;'>" + oclsfuncoes.CPFCNPJMascarar(prmParticipante.NuCPFCNPJ) + "</span></b></p> ";

        body += "<p><span style='font-family:Arial; font-size: 12pt;'>" + nome + ": </span>" +
                            "<b><span style='font-family:Arial; font-size: 12pt;'>" + prmParticipante.NoParticipante + "</span></b></p> " +
            "<p><span style='font-family:Arial; font-size: 12pt;'>" + Categoria + ": </span>" +
                            "<b><span style='font-family:Arial; font-size: 12pt;'>" + categ;

        if (prmParticipante.CdEvento == "005503")
            body += " / " + prmParticipante.NoInstituicao;

        body += "</span></b></p> ";

        if ((prmParticipante.DsIdioma == "PTBR") && (prmParticipante.CdEvento != "008701"))
            body += "<p><span style='font-family:Arial; font-size: 12pt;'>Fone(s): </span>" +
                    "<b><span style='font-family:Arial; font-size: 12pt;'>" + oclsfuncoes.MascaraGerar(prmParticipante.DsFone1, "(99) 999999999") + " / " +
                                                oclsfuncoes.MascaraGerar(prmParticipante.DsFone2, "(99) 999999999") + "</span></b></p> ";

        body += "<p><span style='font-family:Arial; font-size: 12pt;'>E-mail: </span>" +
                            "<b><span style='font-family:Arial; font-size: 12pt;'>" + prmParticipante.DsEmail + "</span></b></p> ";








        if ((oDTPed != null) && (oDTPed.Rows.Count > 0) && (prmParticipante.CdEvento != "003003"))
        {
            body += "<table border='' bordercolor=#3366CC' cellspacing='0' style='width: 660px;'> " +
                   "<tr> " +
                       "<td bgcolor='#3366CC' style='font-weight: bold; font-size: 10pt; width: 100px; font-family: Arial; ForeColor:White' colspan='3'> " +
                           "<center><b><font color='#FFFFFF'>" +
                                               titulo_grid +
                                       "</font></b></center></td> " +
                   "</tr> ";

            // string link = "";
            for (int i = 0; i < oDTPed.Rows.Count; i++)
            {
                string VlTotalItem = (((decimal.Parse(oDTPed.DefaultView[i]["vlAtividade"].ToString())) -
                                       (decimal.Parse(oDTPed.DefaultView[i]["vlDesconto"].ToString()))) *
                                      (int.Parse(oDTPed.DefaultView[i]["vlQuantidade"].ToString()))).ToString("N2");
                if ((i % 2) == 0)
                    body += "<tr> ";
                else
                    body += "<tr bgcolor='#99CCFF'> ";
                body += "<td  valign='middle' align='center' style='width:90px'> ";
                if (oDTPed.DefaultView[i]["dsCaminhoImgWEB"].ToString() != "")
                {
                    body += "<img src='" + oDTPed.DefaultView[i]["dsCaminhoImgWEB"].ToString() + "' alt='imgAtv' width='90px' height='100'/>";
                }

                if (prmParticipante.CdEvento == "006501")
                {
                    if (oDTPed.DefaultView[i]["cdTipoAtividade"].ToString() == "59")
                        lbltpItem = "";
                    else
                        lbltpItem = "Macrogrupo";
                }
                body += "   </td> " +
                        "<td style='width:360px' colspan='2'> " +
                            lbltpItem + ": <b>" + oDTPed.DefaultView[i]["noTipoAtividade"].ToString() + "</b><br /> " +
                            "- <b>" + oDTPed.DefaultView[i]["noTitulo"].ToString() + "</b><br /> ";
                if (oDTPed.DefaultView[i]["noProfessor"].ToString() != "")
                    body += "<b>" + oDTPed.DefaultView[i]["noProfessor"].ToString() + "</b><br /> ";

                if (bool.Parse(oDTPed.DefaultView[i]["flRequerQuantidade"].ToString()))
                    body += "Quantidade: <b>" + oDTPed.DefaultView[i]["vlQuantidade"].ToString() + "</b><br /> ";


                if ((oDTPed.DefaultView[i]["cdAtividade"].ToString() != "001405002") && (oDTPed.DefaultView[i]["cdAtividade"].ToString() != "003603001"))
                {
                    body += lbllocalItem + ": <b>" + oDTPed.DefaultView[i]["noLocal"].ToString() + "</b><br /> ";
                    if ((prmParticipante.CdEvento.Substring(0, 4) != "0003") && (prmParticipante.CdEvento != "001405") && (prmParticipante.CdEvento != "001603"))
                        body += lbldeItem + ": <b>" + oDTPed.DefaultView[i]["dtIni"].ToString() + "</b>  " + lblateItem + ": <b>" + oDTPed.DefaultView[i]["dtTermino"].ToString() + "</b><br /> ";
                    if (prmParticipante.CdEvento == "001405")
                        body += "Dia : <b>16/10 18h30 às 21h30</b><br />" +
                                "Dias: <b>17 e 18/10 das 8h30 às 18h30</b><br /> ";
                }
                body += "</td>" +



                      "</tr>";
            }
        }
        body += "</table><br/>";


        return body;
    }

    public static String BuscarPaisIdioma(String prmNoPaisPTBR, String prmIdioma, SqlConnection prmCnn)
    {
        if (prmCnn == null)
        {
            //_Rc = true;
            //_RcMsg = "Conexão inválida ou inexistente";
            return "-1";
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
                return "-2";
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
                          "cdPais, " +
                          "dbo.TIRA_ACENTO(dsPais) as dsPais, " +
                          "dbo.TIRA_ACENTO(dsPaisIngles) as dsPaisIngles, " +
                          "dbo.TIRA_ACENTO(dsPaisEspanhol) as dsPaisEspanhol, " +
                          "dbo.TIRA_ACENTO(dsPaisFrances) as dsPaisFrances " +
                        "FROM " +
                          "dbo.tbPaises  " +
                        "WHERE dbo.TIRA_ACENTO(dsPais) = '" + prmNoPaisPTBR + "' ", prmCnn);


                Dap = new SqlDataAdapter(comando);

                Dap.TableMappings.Add("MUNICIPIO", "tbMunicipios");
                Dap.Fill(DT);


                if ((DT != null) && (DT.Rows.Count > 0))
                {
                    if (prmIdioma == "PTBR")
                        return DT.DefaultView[0]["dsPais"].ToString();
                    else if (prmIdioma == "ENUS")
                        return DT.DefaultView[0]["dsPaisIngles"].ToString();
                    else if (prmIdioma == "ESP")
                        return DT.DefaultView[0]["dsPaisEspanhol"].ToString();
                    else if (prmIdioma == "FRA")
                        return DT.DefaultView[0]["dsPaisFrances"].ToString();


                }


                return "";


            }
            catch (Exception Ex)
            {
                //_Rc = true;
                //_RcMsg = "Erro ao selecionar Municipios!\n" + Ex.Message;
                return "";

            }
        }
        finally
        {
            //_cnn.Close();
        }


    }

}
