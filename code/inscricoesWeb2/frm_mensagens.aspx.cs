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
using System.Linq;
using System.Windows.Forms.VisualStyles;

public partial class frm_mensagens : System.Web.UI.Page
{
    Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();
    Evento SessionEvento;
    // EventoCad oEventoCad = new EventoCad();
    public const string QUESTIONARIO_EVENTO = "questionario_";
    Pedido SessionPedido;
    
    EventoCad oEventoCad = new EventoCad();
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
            {
                SessionCnn = (SqlConnection)Session["SessionCnn"];
            }
               
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
            {
                //SessionEvento = oEventoCad.Pesquisar("013101", SessionCnn);
                SessionEvento = (Evento)Session["SessionEvento"];
            }
               
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

            lblMsg.Focus();

            if ((Request["cdMensagem"] != null) &&
                (Request["cdMensagem"] != ""))
            {


                switch (Request["cdMensagem"].ToString())
                {
                    case "001":
                        {
                            #region 001
                            if (SessionIdioma == "PTBR")
                                lblMsg.Text = "Operação realizada com sucesso!<br /><br />Foi enviado um e-mail para você com sua nova senha de acesso à área de inscrição.<br /><br />";
                            else if (SessionIdioma == "ENUS")
                                lblMsg.Text = "Operation successful <br /> <br />Was sent an email to you with your new password to access the registration area.<br /><br />";
                            else if (SessionIdioma == "ESP")
                                lblMsg.Text = "Operación con éxito <br /><br />Fue enviado un correo electrónico a usted con su nueva contraseña para acceder a la zona de registro.<br /><br />";
                            else if (SessionIdioma == "FRA")
                                lblMsg.Text = "Opération réussie!<br /> <br />Il a été envoyé un email pour vous avec votre nouveau mot de passe pour accéder à la zone d'enregistrement.<br /><br />";

                            Button1.PostBackUrl = "~/index.aspx?codEvento=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento);
                            Button1.Visible = true;
                            break;
                            #endregion
                        }

                    case "002":
                        {
                            #region 002
                            if (SessionIdioma == "PTBR")
                                lblMsg.Text = "Operação realizada com sucesso!";//<br /><br />Sua inscrição já está em nossos cadastros.<br /><br />";
                            else if (SessionIdioma == "ENUS")
                                lblMsg.Text = "Operation successful";// <br /> <br />Your subscription is already in our records.<br /><br />";
                            else if (SessionIdioma == "ESP")
                                lblMsg.Text = "Operación con éxito";// <br /><br />Su suscripción ya está en nuestros registros.<br /><br />";
                            else if (SessionIdioma == "FRA")
                                lblMsg.Text = "Opération réussie!";//<br /> <br />Votre inscription est déjà dans nos registres.<br /><br />";

                            Button1.Visible = false;
                            if (SessionEvento.DsLinkRedirecionamento != "")
                            {
                                Button1.PostBackUrl = SessionEvento.DsLinkRedirecionamento;
                                Button1.Visible = true;
                                Button1.Focus();
                                if (SessionEvento.CdEvento == "008701")
                                {
                                    if (SessionIdioma == "PTBR")
                                        Button1.Text = "Voltar para o site da 4ª Mostra BID BRASIL";
                                    else
                                        Button1.Text = "Back to the site of the 4th Mostra BID BRASIL";
                                }
                            }

                            if (SessionEvento.CdEvento == "008701")
                            {
                                lblMsg.Visible = false;
                                //if (SessionIdioma == "PTBR")
                                    //lblMsg.Text = "Recebemos sua inscrição com sucesso. Em breve você receberá um email de confirmação.<br />Compareça na Área de Credenciamento no dia do evento para retirada de sua credencial.";//

                                lblMsg2.Text = CorpoEmailABIMDE(SessionParticipante);
                                lblMsg2.Visible = true;

                                printarea.Attributes["class"] = "msg2";

                                Button2.PostBackUrl = "http://fazendomais4.azurewebsites.net/inscr.aspx?codEvento=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + "&cdLng=" + SessionIdioma + "&cat=" + SessionParticipante.CdCategoria + "" + "&atv=" + "" + "&keyAut=" + "" + "&tpSist=" + SessionTipoSistema;
                                //Button2.PostBackUrl = "http://fmaistestes.azurewebsites.net/index.aspx?codEvento=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + "&cdLng=" + SessionIdioma + "&cat=" + SessionParticipante.CdCategoria + "" + "&atv=" + "" + "&keyAut=" + "" + "&tpSist=" + SessionTipoSistema;
                                //Button2.PostBackUrl = "~/index.aspx?codEvento=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + "&cdLng=" + SessionIdioma + "&cat=" +SessionParticipante.CdCategoria + "" + "&atv=" + "" + "&keyAut=" + "" + "&tpSist=" + SessionTipoSistema;
                                Button2.Visible = true;
                                Button3.Visible = true;

                                if (SessionIdioma == "PTBR")
                                {
                                    Button2.Text = "Credenciar outro participante";
                                    Button3.Text = "Imprimir";
                                }
                                else
                                {
                                    Button2.Text = "Accredit another participant";
                                    Button3.Text = "Print";
                                }

                                
                            }


                            if (SessionEvento.CdEvento == "010101")
                            {
                                if (SessionParticipante.FlConfirmacaoInscricao)
                                   return; 

                                lblMsg.Text = "Sua inscrição foi enviada com sucesso, aguarde o email com a confirmação após análise por parte da comissão organizadora.<br/><br/>";
                                if ((SessionParticipante.CdCategoria != "01010102") &&
                                    (SessionParticipante.CdCategoria != "01010103"))
                                {
                                    lblMsg.Text += "Você já pode enviar seus enunciados.<br/>";
                                    Button1.PostBackUrl = "~/frmEnunciadosLista.aspx";
                                    Button1.Text = "Enviar Enunciados";
                                    Button1.Visible = true;
                                }
                                else
                                {
                                    lblMsg.Text +=
                                        "<br/><br/>Lembramos que o segundo dia do evento é aberto somente para advogados, juízes, representantes do Ministério Público do Trabalho e Magistrados do Trabalho.";
                                }
                            }

                            if (SessionEvento.CdEvento == "013101")
                            {
                                lblMsg.Text =
                                    @"<br/><br/>Obrigado pela sua participação!<br/><br/>
                                    Ao final do evento sortearemos um brinde Sabin e entraremos em contato caso seja premiado. Boa sorte!
                                    <br/><br/>";
                            }

                            //if (SessionEvento.CdEvento == "001603")
                            //{
                            //    lblMsg.Text += "<br/><br/>Não deixe ver seu e-mail com as instruções enviadas.";
                            //}
                            break;
                            #endregion
                        }

                    case "002-1":
                        {
                            #region 002-1
                            if (SessionIdioma == "PTBR")
                                lblMsg.Text = "Operação realizada com sucesso!<br /><br />";
                            else if (SessionIdioma == "ENUS")
                                lblMsg.Text = "Operation successful <br /> <br />";
                            else if (SessionIdioma == "ESP")
                                lblMsg.Text = "Operación con éxito <br /><br />";
                            else if (SessionIdioma == "FRA")
                                lblMsg.Text = "Opération réussie!<br /> <br />";

                            break;
                            #endregion
                        }
                    case "003":
                        {
                            if (SessionIdioma == "PTBR")
                                lblMsg.Text = "Evento não encontrado!<br /><br />" + Request["dsMensagem"];
                            else if (SessionIdioma == "ENUS")
                                lblMsg.Text = "Event not found<br /> <br />" + Request["dsMensagem"];
                            else if (SessionIdioma == "ESP")
                                lblMsg.Text = "Evento no encontrado <br /><br />" + Request["dsMensagem"];
                            else if (SessionIdioma == "FRA")
                                lblMsg.Text = "Événement non trouvé<br /> <br />" + Request["dsMensagem"];

                            break;
                        }
                    case "04":
                        {
                            if (SessionIdioma == "PTBR")
                                lblMsg.Text = "Problemas ao verificar configurações do site!<br /><br />";
                            else if (SessionIdioma == "ENUS")
                                lblMsg.Text = "Problems when checking site settings<br /> <br />";
                            else if (SessionIdioma == "ESP")
                                lblMsg.Text = "Problemas en la comprobación de la configuración del sitio<br /><br />";
                            else if (SessionIdioma == "FRA")
                                lblMsg.Text = "Problèmes lors de la vérification des paramètres du site!<br /> <br />";

                            break;
                        }
                    case "004":
                        {
                            if (SessionIdioma == "PTBR")
                                lblMsg.Text = "Inscrições ainda não estão abertas!<br /><br />";
                            else if (SessionIdioma == "ENUS")
                                lblMsg.Text = "Registrations are not open yet<br /> <br />";
                            else if (SessionIdioma == "ESP")
                                lblMsg.Text = "Las inscripciones no están abiertas todavía<br /><br />";
                            else if (SessionIdioma == "FRA")
                                lblMsg.Text = "Les inscriptions ne sont pas encore ouvert<br /> <br />";

                            break;
                        }
                    case "05":
                        {
                            if (SessionIdioma == "PTBR")
                                lblMsg.Text = "Site em manutenção!\n<br/>Tente mais tarde.<br /><br />";
                            else if (SessionIdioma == "ENUS")
                                lblMsg.Text = "Site under maintenance \n<br/>Try again later.<br /><br />";
                            else if (SessionIdioma == "ESP")
                                lblMsg.Text = "Sitio en mantenimiento \n<br/>Por favor, inténtelo de nuevo más tarde.<br /><br />";
                            else if (SessionIdioma == "FRA")
                                lblMsg.Text = "Site en cours de maintenance \n<br/>S`il vous plaît réessayer plus tard.<br /> <br />";

                            break;
                        }
                    case "005":
                        {
                            if (SessionIdioma == "PTBR")
                                lblMsg.Text = "Inscrições encerradas na Web!<br /><br />";
                            else if (SessionIdioma == "ENUS")
                                lblMsg.Text = "Registration closed on the web!<br /><br />";
                            else if (SessionIdioma == "ESP")
                                lblMsg.Text = "Registro cerrado en la web<br /><br />";
                            else if (SessionIdioma == "FRA")
                                lblMsg.Text = "Entrées fermée sur le web!<br /> <br />";

                            if (SessionEvento.CdEvento == "001003")
                                lblMsg.Text = "Inscrições encerradas via internet.<br /><br />A partir de agora as inscrições estão sendo realizadas no local do evento pela portaria norte. <br />Obs: Entrada franca!";

                            if (SessionEvento.CdEvento == "000304")
                                lblMsg.Text = "<h1>Inscrições online encerradas</h1><br /><br />Novas inscrições serão realizadas apenas no credenciamento que começa dia 16, a partir das 10 horas, no Centro de Convenções Ulisses Guimarães. <br />";

                            if (SessionEvento.CdEvento == "001305")
                                lblMsg.Text = "Inscrições pela internet encerradas!<br/> " +
                                               "Novas inscrições somente no local do evento<br/> " +
                                               "05/08/2013 – 14:00hs<br/><br/><br/>  " +
                                               "Valores:<br/> " +
                                               "R$ 350,00 – Municípios adimplentes<br/> " +
                                               "R$ 450,00 – Demais inscrições<br/> ";

                            if (SessionEvento.CdEvento == "004501")
                                lblMsg.Text = "O pré-credenciamento para a 2ª Conferência do Desenvolvimento Rural Sustentável e Solidário está encerrado. Seu credenciamento poderá ser feito no local da conferência, no dia 14 de outubro, a partir das 9 horas. Para evitar filas, solicitamos que se dirija ao credenciamento com a maior antecedência possível do horário da cerimônia de abertura.";

                            if (SessionEvento.CdEvento == "004102")
                            {
                                lblInstrucoesRapidas.Text = SessionEvento.DsInformacoesPreviasWeb;
                                lblInstrucoesRapidas.Visible = true;
                            }


                            if (SessionEvento.CdEvento == "005503")
                            {
                                lblMsg.Text = 
                                    "<p>&nbsp;</p> " + 
                                    "<p><strong>PRAZO PARA INSCRIC&Otilde;ES PELA INTERNET ENCERRADO</strong></p> " + 
                                    "<p>INSCRI&Ccedil;&Otilde;ES SOMENTE NO CREDENCIAMENTO DO EVENTO.</p> " + 
                                    "<p>Taxa de Inscri&ccedil;&atilde;o: r$ 700,00<br />(Pagamento em dinheiro ou cart&atilde;o de cr&eacute;dito e d&eacute;bito)</p> " + 
                                    "<p><br />Atenciosamente,</p> " + 
                                    "<p>Coordena&ccedil;&atilde;o do Semin&aacute;rio Nacional NTU</p> " + 
                                    "<p>Informa&ccedil;&otilde;es sobre o evento:<br />Fone: +55 61 2103-9293<br />E-mail: <a href=\"mailto:seminario@ntu.org.br\">seminario@ntu.org.br</a></p> " + 
                                    "<p>&nbsp;</p> ";
                            }

                            if (SessionEvento.CdEvento == "007701")
                            {
                                lblMsg.Text = "<p>Inscrições encerradas via internet.<br />A partir de agora as inscrições estão sendo realizadas no local do evento.</p>";
                                if (SessionIdioma == "ENUS")
                                    lblMsg.Text = "<p>Registration closed on the web!<br /></p>From now on registrations are being held at the venue.";
                                else if (SessionIdioma == "ESP")
                                    lblMsg.Text = "<p>Registro cerrado en la web<br /></p>A partir de ahora las inscripciones se encuentran detenidos en el lugar.";
                            }


                            //Novas inscrições somente dia 10 de Junho 14:00hs no local do evento, Centro de convenções Ruth Cardoso - Maceió-AL.";
                            //else if (SessionEvento.CdEvento == "002902")
                            //    lblMsg.Text += "Mais informações ligar para (61) 3963-5049";
                            //else
                            //    lblMsg.Text += "Mas você pode se inscrever na secretaria do evento";
                            break;
                        }
                    case "006":
                        {
                            if (SessionIdioma == "PTBR")
                                lblMsg.Text = "Evento encerrado!<br /><br />";
                            else if (SessionIdioma == "ENUS")
                                lblMsg.Text = "Event ended!<br /><br />";
                            else if (SessionIdioma == "ESP")
                                lblMsg.Text = "Evento terminado<br /><br />";
                            else if (SessionIdioma == "FRA")
                                lblMsg.Text = "Evénement terminé<br /> <br />";

                            break;
                        }
                    case "007":
                        {
                            if (SessionIdioma == "PTBR")
                                lblMsg.Text = "Inscrições suspensas!\n<br/>Tente mais tarde.<br /><br />";
                            else if (SessionIdioma == "ENUS")
                                lblMsg.Text = "Registration suspended\n<br/>Try again later.<br /><br />";
                            else if (SessionIdioma == "ESP")
                                lblMsg.Text = "Matrícula suspendida\n<br/>Por favor, inténtelo de nuevo más tarde.<br /><br />";
                            else if (SessionIdioma == "FRA")
                                lblMsg.Text = "Inscription suspendue\n<br/>S`il vous plaît réessayer plus tard.<br /> <br />";

                            break;
                        }
                    case "008":
                        {
                            if (SessionIdioma == "PTBR")
                                lblMsg.Text = "Categoria do Participante não permite inscrição em atividades<br /><br />";
                            else if (SessionIdioma == "ENUS")
                                lblMsg.Text = "Category of Participant does not permit registration activities<br /><br />";
                            else if (SessionIdioma == "ESP")
                                lblMsg.Text = "Categoría del participante no permite registro en las actividades<br /><br />";
                            else if (SessionIdioma == "FRA")
                                lblMsg.Text = "Catégorie du participant ne permet pas les activités d`inscription<br /> <br />";

                            lblMsg.Text = "";
                            Button1.Visible = true;
                            break;
                        }
                    case "009":
                        {
                            lblMsg.Text = "Já consta uma solicitação de inscrição para o participante!<br /><br />" +
                                "Você deve confirmar sua inscrição junto a organização do evento.";
                            break;
                        }
                    case "010":
                        {
                            if (SessionIdioma == "PTBR")
                                lblMsg.Text = "Participante já inscrito!<br /><br />";
                            else if (SessionIdioma == "ENUS")
                                lblMsg.Text = "Participant already registered!<br /><br />";
                            else if (SessionIdioma == "ESP")
                                lblMsg.Text = "Participante ya está registrado<br /><br />";
                            else if (SessionIdioma == "FRA")
                                lblMsg.Text = "Participant déjà inscrit!<br /> <br />";

                            Button1.Visible = true;
                            break;
                        }
                    case "011":
                        {
                            if (SessionIdioma == "PTBR")
                                lblMsg.Text = "Não há mais vagas!<br/>Não foi possível incluir o pedido de inscrição.<br /><br />";
                            else if (SessionIdioma == "ENUS")
                                lblMsg.Text = "No vacancies! <br/>Could not include the request for registration.<br /><br />";
                            else if (SessionIdioma == "ESP")
                                lblMsg.Text = "¡No Vacantes! <br/>No se pudo incluir la solicitud de registro.<br /><br />";
                            else if (SessionIdioma == "FRA")
                                lblMsg.Text = "Pas de postes vacants! <br/>Ne pouvait pas inclure la demande d`inscription.<br /> <br />";


                            Button1.PostBackUrl = "~/frmCadastroAuto.aspx";
                            Button1.Visible = true;
                            break;
                        }
                    case "012":
                        {
                            if (SessionIdioma == "PTBR")
                                lblMsg.Text = "Não foi possível efetuar a inscrição.<br/>" +
                                        "Tente de novo, se persistir o problema entre em contato a organização do evento.<br /><br />";
                            else if (SessionIdioma == "ENUS")
                                lblMsg.Text = "Unable to register.<br/>" +
                                              "Try again, if the problem persists please contact the event organizers.<br /><br />";
                            else if (SessionIdioma == "ESP")
                                lblMsg.Text = "No se puede registrar.<br/>" +
                                              "Inténtelo de nuevo, si el problema persiste, póngase en contacto con los organizadores del evento.<br /><br />";
                            else if (SessionIdioma == "FRA")
                                lblMsg.Text = "Impossible d`enregistrer.<br/>" +
                                              "Essayez à nouveau, si le problème persiste contactez s'il vous plaît les organisateurs de l'événement.<br /> <br />";


                            Button1.PostBackUrl = "~/frmCadastroAuto.aspx";
                            Button1.Visible = true;
                            break;
                        }
                    case "013":
                        {//inscrição com confirmação
                            lblMsg.Text = "Operação realizada com sucesso!<br/><br/>" +
                                          "Seu pedido de inscrição já está em nossos cadastros.<br />";
                            if (SessionEvento.CdEvento != "003401")
                                lblMsg.Text += "Você deve confirmar sua inscrição junto a organização do evento.";
                            else
                                lblMsg.Text = "Operação realizada com sucesso!<br/><br/>" +
                                          "Você deve aguardar a avaliação de sua inscrição no Congresso e no Eixo Temático escolhido pela comissão organizadora do evento.";

                            Button1.PostBackUrl = "~/frmCadastroAuto.aspx";
                            Button1.Visible = true;
                            break;
                        }
                    case "014":
                        {

                            if (SessionIdioma == "PTBR")
                                lblMsg.Text = "Operação realizada com sucesso! Aguarde e-mail de confirmação. Verifique também sua caixa de spam.<br /><br />" + (Request.QueryString["dsMensagem"].Contains("BLT") ? "" : Request.QueryString["dsMensagem"]);
                            else if (SessionIdioma == "ENUS")
                                lblMsg.Text = "Operation successful<br />Your registration is in our database. Be sure to see the email that was sent to you. Check also your spam box <br /> <br />";
                            else if (SessionIdioma == "ESP")
                                lblMsg.Text = "Operación con éxito<br />Su registro se encuentra en nuestra base de datos. Asegúrese de ver el correo electrónico que fue enviado a usted. También compruebe su caja de spam.<br /><br />";
                            else if (SessionIdioma == "FRA")
                                lblMsg.Text = "Opération réussie!<br />Votre inscription est dans notre base de données. Soyez sûr de voir l`e-mail qui vous a été envoyé.<br /><br /> ";

                            Button1.PostBackUrl = "~/frmCadastroAuto.aspx";
                            Button1.Visible = true;

                            if (SessionEvento.DsLinkRedirecionamento != "")
                            {
                                if (SessionEvento.CdEvento != "006501")
                                    Button1.PostBackUrl = SessionEvento.DsLinkRedirecionamento;

                                if (SessionEvento.CdEvento == "010801")
                                {
                                    if (SessionIdioma == "PTBR")
                                        Button1.PostBackUrl = SessionEvento.DsLinkRedirecionamento+"bike-parade-pt";
                                    else if (SessionIdioma == "ENUS")
                                        Button1.PostBackUrl = SessionEvento.DsLinkRedirecionamento + "copia-bike-parade-pt";
                                }

                                Button1.Visible = true;
                            }
                            else
                            {
                                if ((SessionEvento.CdEvento != "001304") && (SessionEvento.CdEvento != "001305"))
                                    Button1.PostBackUrl = "~/frmCadastroAuto.aspx";
                                else
                                {
                                    System.Collections.Specialized.NameValueCollection UserInfoCookieCollection;

                                    UserInfoCookieCollection = Request.Cookies["EventoInfo"].Values;
                                    string codEvento = Server.HtmlEncode(UserInfoCookieCollection["eventoCod"]);
                                    string codLng = Server.HtmlEncode(UserInfoCookieCollection["lngCod"]);
                                    string codCateg = Server.HtmlEncode(UserInfoCookieCollection["categCod"]);
                                    string codAtividade = Server.HtmlEncode(UserInfoCookieCollection["atividadeCod"]);
                                    string tpSistema = Server.HtmlEncode(UserInfoCookieCollection["tpSistema"]);

                                    Button1.PostBackUrl = "~/frmCadastroAuto.aspx";
                                    //Button1.PostBackUrl = "~/index.aspx?codEvento=" + cllEventos.Crypto.EncryptStringAES(codEvento) + "&cdLng=" + codLng + "&cat=" + codCateg + "&atv=" + codAtividade + "&keyAut=" + "&tpSist=" + tpSistema + "";
                                }
                                Button1.Visible = true;
                            }


                            //if (SessionEvento.CdEvento == "012601")
                            {
                                if ((Request.QueryString["dsMensagem"] != "") && (Request.QueryString["dsMensagem"].Contains("BLT")))
                                {
                                    MeuBoletoCad oMeuBoletoCad = new MeuBoletoCad();
                                    MeuBoleto oBoleto = oMeuBoletoCad.Pesquisar(SessionEvento.CdEvento, Request.QueryString["dsMensagem"].Replace("BLT",""), SessionCnn);
                                    if (oBoleto != null)
                                    {
                                        Response.Write("<script>window.open('" + oBoleto.DsLinkBoletoExterno + "','_blank');</script>");
                                    }
                                }
                            }

                            if (SessionEvento.CdEvento == "011301")
                            {
                                lblMsg.Text += "<p>\"Não haverá estacionamento disponível para o evento, dê preferência ao taxi ou transportes de aplicativo.\"</p>";


                            }

                            if (SessionEvento.CdEvento == "001603")
                            {
                                lblMsg.Text += "Não deixe ver seu e-mail com as instruções enviadas.";
                              

                            }
                            if (SessionEvento.CdEvento == "005503")
                            {
                                lblMsg.Text += "<p> Para mais informações, entre em contato com a organização pelo telefone (61) 2103-9293 ou email <a href='mailto:seminario@ntu.org.br'><strong> seminario@ntu.org.br</strong></a>.</p>";
                                if (SessionIdioma == "ENUS")
                                {
                                    lblMsg.Text += "<p> For more information, contact the organization by phone (61) 2103-9293 or email <a href='mailto:seminario@ntu.org.br'><strong> seminario@ntu.org.br</strong></a>.</ p>";
                                }
                                else if (SessionIdioma == "ESP")
                                {
                                    lblMsg.Text += "<p> Para obtener más información, póngase en contacto con la organización por teléfono (61) 2103-9293 o por correo electrónico <a href='mailto:seminario@ntu.org.br'><strong> seminario@ntu.org.br</strong></a>.</ p>";
                                }

                                else if (SessionIdioma == "FRA")
                                {
                                    lblMsg.Text = "<p> Pour plus d'informations, contacter l'organisation par téléphone (61) 2103-9293 ou par courriel <a href='mailto:seminario@ntu.org.br'><strong> seminario@ntu.org.br</strong></a>.</ p>";

                                }
                            }

                            if (SessionEvento.CdEvento == "008201")
                            {
                                lblMsg.Text = "Sua confirmação foi realizada com sucesso e foi enviada para o email cadastrado. Obrigado.";
                                Button1.Visible = false;
                            }

                            if (SessionEvento.CdEvento == "008303")
                            {
                                lblMsg.Text =
                                    "Operação realizada com sucesso!<br />" +
                                    "<p>&nbsp;</p> " +
                                    "<p><em>Agradecemos sua inscri&ccedil;&atilde;o no 4&ordm; Pense Sicoob!</em></p> ";
                                if ((SessionPedido != null) && ((SessionPedido.TpPagamento.ToUpper().Contains("DÉBITO")) || (SessionPedido.TpPagamento.ToUpper().Contains("DEBITO"))))
                                    lblMsg.Text +=
                                        "<p>&nbsp;</p> " +
                                        "<p><em>A confirma&ccedil;&atilde;o da sua inscri&ccedil;&atilde;o ser&aacute; enviada por e-mail ap&oacute;s a realiza&ccedil;&atilde;o do " + "d&eacute;bito autorizado pela sua Cooperativa.</em></p> " +
                                        "<p>O débito será feito em até 15 dias após o cadastro.</p>";

                                lblMsg.Text +=
                                    "<p>&nbsp;</p> " +
                                    "<p><em>Esclarecimentos adicionais poder&atilde;o ser obtidos por meio do correio eletr&ocirc;nico <a " + "href=\"mailto:pensesicoob@sicoob.com.br\">pensesicoob@sicoob.com.br</a> ou pelo telefone (61) 3217-5792 (Raquel Frois).</em></p> " +
                                    "<p>&nbsp;</p> ";

                                Button1.Visible = true;



                                Button2.PostBackUrl = "http://fazendomais4.azurewebsites.net/inscr.aspx?codEvento=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + "&cdLng=" + SessionIdioma + "&cat=" + "" + "&atv=" + "" + "&keyAut=" + "" + "&tpSist=" + SessionTipoSistema;
                                //Button2.PostBackUrl = "http://fmaistestes.azurewebsites.net/index.aspx?codEvento=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + "&cdLng=" + SessionIdioma + "&cat=" + "" + "&atv=" + "" + "&keyAut=" + "" + "&tpSist=" + SessionTipoSistema;
                                //Button2.PostBackUrl = "~/index.aspx?codEvento=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + "&cdLng=" + SessionIdioma + "&cat=" +SessionParticipante.CdCategoria + "" + "&atv=" + "" + "&keyAut=" + "" + "&tpSist=" + SessionTipoSistema;
                                Button2.Visible = true;
                                Button2.Text = "Credenciar outro participante";

                            }

                            if (SessionEvento.CdCliente == "0085")
                            {
                                Button1.Visible = true;
                                Button1.PostBackUrl = "~/frmEscolherAcao.aspx";
                            }


                            if (SessionEvento.CdEvento == "010901")
                            {
                                Button1.PostBackUrl = "~/frmCadastroAuto.aspx";
                                Button2.PostBackUrl = 
                                    "http://fazendomais4.azurewebsites.net/inscr.aspx?codEvento=" + 
                                    cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + 
                                    "&cdLng=" + SessionIdioma + 
                                    "&cat=" + "" + 
                                    "&atv=" + "" + 
                                    "&keyAut=" + "" + 
                                    "&tpSist=" + SessionTipoSistema + 
                                    "&tpAcesso=NOVA";
                                if (SessionIdioma == "PTBR")
                                {
                                    Button1.Text = "Voltar para o Cadastro";
                                    Button2.Text = "Cadastrar Novo Particpante";
                                }
                                else if (SessionIdioma == "ENUS")
                                {
                                    Button1.Text = "Back to register";
                                    Button2.Text = "Sign Up New Member";
                                }
                                Button1.Visible = true;
                                Button2.Visible = true;
                            }

//                            if (SessionEvento.CdEvento == "007702")
//                            {
//                                if (SessionParticipante.DsUF == "DF")
//                                {
//                                    lblMsg.Text =
//                                        @"
//                                        <p>Operação realizada com sucesso, seus dados de cadastro já est&atilde;o em nossa base de dados.</p>
//                                        <p>&nbsp;</p>
//                                        <p>O Congresso Internacional Cidades Lixo Zero &eacute; um evento gratuito cuja participa&ccedil;&atilde;o depender&aacute; de confirma&ccedil;&atilde;o pr&eacute;via.</p>
//                                        <p>&nbsp;</p>
//                                        <p>Aten&ccedil;&atilde;o, neste momento voc&ecirc; est&aacute; realizando a sua pr&eacute; inscri&ccedil;&atilde;o. O pedido de confirma&ccedil;&atilde;o que garantir&aacute; 
//                                        a sua entrada ser&aacute; enviado 10 dias antes do evento por e-mail. Lembre-se de verificar sua caixa de spam.</p>";
//                                }
//                            }


                            if ((SessionEvento.CdEvento == "007002") && (!SessionParticipante.FlConfirmacaoInscricao))
                            {
                                lblMsg.Text =
                                    @"<p>You are nearly there&hellip;</p>
                                    <p>We have received your submission correctly and the final step to secure your place in the BIO Latin America Conference 2018 is making the payment of your registration.</p>
                                    <p>This information and the payment document were sent to your e-mail. Please remember to check your spam box.</p>
                                    <p>If you have any questions, please contact us at <a href=\mailto:bla@biominas.org.br\>bla@biominas.org.br</a>.</p>
                                    <p>Best,</p>
                                    <p>BIO Latin America Team</p>";                                
                            }

                            break;
                        }
                    case "014-1":
                        {

                            if (SessionIdioma == "PTBR")
                                lblMsg.Text = "Operação realizada com sucesso!";
                            else if (SessionIdioma == "ENUS")
                                lblMsg.Text = "Operation successful";
                            else if (SessionIdioma == "ESP")
                                lblMsg.Text = "Operación con éxito";
                            else if (SessionIdioma == "FRA")
                                lblMsg.Text = "Opération réussie!";

                            Button1.PostBackUrl = "~/frmCadastroAuto.aspx";
                            Button1.Visible = true;

                            if (SessionEvento.CdEvento == "008501")
                            {
                                SessionParticipante = oParticipanteCad.Pesquisar(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);
                                Session["SessionParticipante"] = SessionParticipante;
                                Button1.PostBackUrl = "~/frmEscolherAcao.aspx";
                            }

                            if (SessionEvento.CdEvento == "007002")
                            {
                                lblMsg.Text =
                                    @"<p>Thank you!</p>
                                    <p>We have received your registration payment for the BIO Latin America Conference 2018. <strong>Your registration must now be confirmed by our team.</strong></p>
                                    <p>We will verify your qualifications for the registration category selected. If the selected category is correct, you will receive an e-mail with your registration confirmation within the next 48 hours. If the category is not correct, you will receive an e-mail with further instructions.</p>
                                    <p>Note for Brazilian registrations: the receipt document (Nota Fiscal) will be sent to informed e-mail within the next 48 hours.</p>
                                    <p>Please, wait for our contact, or, if you have any questions, contact us at <a href='mailto:bla@biominas.org.br'>bla@biominas.org.br</a>.</p>
                                    <p>Best,</p>
                                    <p>BIO Latin America Team</p>";
                                                                }

                            break;
                        }
                    case "015":
                        {
                            if (SessionIdioma == "PTBR")
                                lblMsg.Text = "Existe um pedido de inscrição em aberto!<br />" +
                                              "Para efetuar outro pedido você deve primeiro cancelar este pedido.<br /><br />";
                            else if (SessionIdioma == "ENUS")
                                lblMsg.Text = "There is a request for registration does not pay!<br />" +
                                              "To make another order, you must first cancel this request.<br /><br />";
                            else if (SessionIdioma == "ESP")
                                lblMsg.Text = "¡Hay una solicitud de registro no paga!<br />" +
                                              "Para hacer otro pedido, primero debe cancelar esta solicitud.<br /><br />";
                            else if (SessionIdioma == "FRA")
                                lblMsg.Text = "Il ya une demande d'inscription ne paie pas!<br />" +
                                              "Pour prendre un autre arrêté, vous devez d`abord annuler cette demande.<br /> <br />";


                            if (SessionEvento.CdEvento == "003401")
                                lblMsg.Text = "Existe um pedido de inscrição no Eixo Temático em aberto!<br />" +
                                              "Caso deseje alterá-lo você deve primeiro cancelar esse pedido na tela a seguir.<br /><br />";

                            Button1.PostBackUrl = "~/frmListarPedidos.aspx";
                            Button1.Visible = true;
                            break;
                        }
                    case "016":
                        {
                            if (SessionIdioma == "PTBR")
                                lblMsg.Text = "Não há atividades/cursos para inscrição!<br /><br />";
                            else if (SessionIdioma == "ENUS")
                                lblMsg.Text = "No activities / courses for registration!<br /><br />";
                            else if (SessionIdioma == "ESP")
                                lblMsg.Text = "¡No hay actividades y cursos para el registro!<br /><br />";
                            else if (SessionIdioma == "FRA")
                                lblMsg.Text = "Aucune activité ou de cours d`enregistrement!<br /> <br />";

                            break;
                        }
                    case "017":
                        {
                            lblMsg.Text = "Boleto não localizado!";
                            break;
                        }
                    case "018":
                        {
                            if (SessionIdioma == "PTBR")
                                lblMsg.Text = "Pedido cancelado com sucesso!<br /><br />";
                            else if (SessionIdioma == "ENUS")
                                lblMsg.Text = "Order canceled successfully!<br /><br />";
                            else if (SessionIdioma == "ESP")
                                lblMsg.Text = "¡Solicitud cancelada con éxito!<br /><br />";
                            else if (SessionIdioma == "FRA")
                                lblMsg.Text = "Demande d`annulation avec succès!<br /> <br />";

                            Button1.PostBackUrl = "~/frmListarPedidos.aspx";
                            Button1.Visible = true;
                            break;
                        }
                    case "019":
                        {
                            lblMsg.Text = "Operação realizada com sucesso!<br /><br />Foi enviado um e-mail para o participante informando-o para acessar o site de credenciamento e concluir sua inscrição.";
                            Button1.PostBackUrl = "~/frmCadastroAuto.aspx";
                            Button1.Visible = true;
                            break;
                        }
                    case "020":
                        {
                            lblMsg.Text = "Operação realizada com sucesso!<br / ><br />";
                            //Button1.PostBackUrl = "https://inscricoesweb.fazendomais.com/index.aspx?codEvento="+cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento);// "~/frmCadastroAuto.aspx";
                            Button1.PostBackUrl = "http://www.fazendomais.com/inscricoes/inscr.aspx?codEvento=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento);
                            //Button1.PostBackUrl = "~/index.aspx?codEvento=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento);
                            Button1.Visible = true;
                            break;
                        }
                    case "021":
                        {
                            lblMsg.Text = "Evento Inativo!";
                            break;
                        }
                    case "022":
                        {
                            lblMsg.Text = "Participante não possui pacote com direito a acompanhantes!<br/>";
                            //Button1.PostBackUrl = "~/frmCadastroAuto.aspx";
                            //Button1.Visible = true;
                            break;
                        }
                    case "023":
                        {
                            lblMsg.Text = "Inscrição sem direito à hospedagem!<br/>";
                            //Button1.PostBackUrl = "~/frmCadastroAuto.aspx";
                            //Button1.Visible = true;
                            break;
                        }
                    case "024":
                        {
                            lblMsg.Text = "Confirmação de reserva já realizada!<br/>";
                            //Button1.PostBackUrl = "~/frmCadastroAuto.aspx";
                            //Button1.Visible = true;
                            break;
                        }
                    case "025":
                        {
                            lblMsg.Text = "Confirmação de reserva realizada com sucesso!<br/>";
                            //Button1.PostBackUrl = "~/frmCadastroAuto.aspx";
                            //Button1.Visible = true;
                            break;
                        }
                    case "026":
                        {
                            lblMsg.Text = "Reserva ainda não realizada favor aguardar!<br/>";
                            //Button1.PostBackUrl = "~/frmCadastroAuto.aspx";
                            //Button1.Visible = true;
                            break;
                        }
                    case "027":
                        {
                            lblMsg.Text = "Desculpe mas seu pedido está vencido!<br / ><br />Solicitamos que efetue um novo pedido.";
                            if (SessionParticipante != null)
                                Button1.PostBackUrl = "~/frmListarPedidos.aspx";
                            else
                                Button1.PostBackUrl = "~/index.aspx?codEvento=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento);
                            Button1.Visible = true;
                            break;
                        }
                    case "028":
                        {
                            lblMsg.Text = "Participante não emitiu credencial no dia do Evento!<br / ><br />Não é possível emitir certificado. Entre em contato com a organização do evento.";
                            Button1.PostBackUrl = "~/index.aspx?codEvento=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento);
                            Button1.Visible = true;
                            break;
                        }
                    case "029":
                        {
                            lblMsg.Text = "A categoria do Participante não possui direito à certificação!<br / ><br />Entre em contato com a organização do evento.";
                            Button1.PostBackUrl = "~/index.aspx?codEvento=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento);
                            Button1.Visible = true;
                            break;
                        }
                    case "030":
                        {
                            lblMsg.Text = "Ceritificado emitido com sucesso!";
                            Button1.PostBackUrl = "~/index.aspx?codEvento=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento);
                            Button1.Visible = true;
                            break;
                        }
                    case "031":
                        { //---retorno do pagamento não processado pel PAYPAL
                            if (SessionIdioma == "PTBR")
                                lblMsg.Text = "Não foi possível completar sua transação.<br />Tente de novo.<br /><br />";
                            else if (SessionIdioma == "ENUS")
                                lblMsg.Text = "Could not complete your transaction.<br />Please try again.<br /><br />";
                            else if (SessionIdioma == "ESP")
                                lblMsg.Text = "No se pudo completar su transacción.<br />Por favor, inténtelo de nuevo.<br /><br />";
                            else if (SessionIdioma == "FRA")
                                lblMsg.Text = "Impossible de terminer votre transaction.<br />S`il vous plaît essayez de nouveau.<br /> <br />";

                            Button1.PostBackUrl = "~/frmPayPalPagamento.aspx";
                            Button1.Visible = true;
                            break;
                        }
                    case "032":
                        {
                            lblMsg.Text = "Processamos sua solicitação de inscrição! <br />" +
                                "Estamos aguardando informações do pagamento pelo PagSeguro.<br/>" +
                                "Solcitamos que acompanhe as notificações enviadas ao se e-mail sobre o andamento do processo de confirmação do pagamento.";
                            Button1.PostBackUrl = "~/frmCadastroAuto.aspx";
                            Button1.Visible = true;
                            break;
                        }
                    case "033":
                        { //---retorno do pagamento não processado pel PAYPAL
                            if (SessionIdioma == "PTBR")
                                lblMsg.Text = "Não foi possível completar sua transação.<br />Tente de novo.<br /><br />";
                            else if (SessionIdioma == "ENUS")
                                lblMsg.Text = "Could not complete your transaction.<br />Please try again.<br /><br />";
                            else if (SessionIdioma == "ESP")
                                lblMsg.Text = "No se pudo completar su transacción.<br />Por favor, inténtelo de nuevo.<br /><br />";
                            else if (SessionIdioma == "FRA")
                                lblMsg.Text = "Impossible de terminer votre transaction.<br />S`il vous plaît essayez de nouveau.<br /> <br />";

                            Button1.PostBackUrl = "~/frmPagSeguroPagamento.aspx";
                            Button1.Visible = true;
                            break;
                        }
                    case "034":
                        {
                            lblMsg.Text = "Obrigado por sua participação!!!<br / ><br />";


                            Session["oDTPesquisa"] = null;

                            

                            if ((SessionEvento.CdEvento != "005502") && (SessionEvento.CdEvento != "013101"))
                            {
                                if ((Request.Url.ToString().ToLower().Contains("localhost")) || (Request.Url.ToString().ToLower().Contains("185.185")) || (Request.Url.ToString().ToLower().Contains("192.168")))
                                    Button1.PostBackUrl = "~/index.aspx?codEvento=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + "&cdLng=" + SessionIdioma + "&cat=" + "" + "&atv=" + "" + "&keyAut=" + "" + "&tpSist=" + SessionTipoSistema;
                                else
                                {
                                    //Button1.PostBackUrl = "https://inscricoesweb.fazendomais.com/index.aspx?codEvento="+cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento);// "~/frmCadastroAuto.aspx";

                                    if (tpRotina != "EMTCERT")
                                        Button1.PostBackUrl = "https://fazendomais4.azurewebsites.net/inscr.aspx?codEvento=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + "&cdLng=" + SessionIdioma + "&cat=" + "" + "&atv=" + "" + "&keyAut=" + "" + "&tpSist=" + SessionTipoSistema + "&tpRotina=" + tpRotina;
                                    else
                                        Button1.PostBackUrl = "https://fazendomais.azurewebsites.net/frmVerificaCPFCadastro.aspx?codEvento=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + "&cdLng=" + SessionIdioma + "&cat=" + "" + "&atv=" + "" + "&keyAut=" + "" + "&tpSist=&tpRotina=" + tpRotina;
                                }
                            }


                            if (SessionEvento.CdEvento == "013101")
                            {
                                lblMsg.Text = @"<br/><br/><br/>Você acertou  @nrAcertos das @totalQuestao perguntas do nosso quizz! 
                                                <br/>Esperamos que tenha gostado de testar seus conhecimentos.<br/><br/>
                                                Para participar do sorteio você deverá preencher um cadastro, Deseja participar do sorteio?";

                                Button1.Text = "Sim";
                                //Button1.PostBackUrl = "~/index.aspx?codEvento=MDEzMTAx&cdLng=PTBR&cat=&atv=&keyAut=&tpSist=NRM&tpAcesso=NRM";
                                Button1.Visible = true;

                                Button2.Text = "Não";
                                //Button2.PostBackUrl = "~/index.aspx?codEvento=MDEzMTAx&cdLng=PTBR&cat=&atv=&keyAut=&tpSist=PSQAVS&tpAcesso=NRM";
                                Button2.Visible = true;

                                var totalAcertos = 0;
                                var totalQuestao = 4;
                                var totalErros = 0;

                                foreach (var allCookiesAllKey in HttpContext.Current.Request.Cookies.AllKeys)
                                {
                                    {
                                        var MyCookieValues = HttpContext.Current.Request.Cookies[allCookiesAllKey];
                                        if (MyCookieValues["acertos"] != null)
                                        {
                                            var valueAcerto = MyCookieValues["acertos"];

                                            if(!string.IsNullOrEmpty(valueAcerto))
                                            totalAcertos += Convert.ToInt32(valueAcerto);
                                        }
                                    }


                                    
                                }

                                totalErros = totalQuestao - totalAcertos;
                                lblMsg.Text =  lblMsg.Text.Replace("@nrAcertos", totalAcertos.ToString());
                                lblMsg.Text = lblMsg.Text.Replace("@totalQuestao", totalQuestao.ToString());


                            }

                            //Button1.PostBackUrl = "~/index.aspx?codEvento=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento);
                            Button1.Visible = true;
                            break;
                        }
                    case "035":
                        {//inscrição com confirmação
                            lblMsg.Text = "Operação realizada com sucesso!<br/><br/>" +
                                          "Seu cadastro já está em nossa base de dados. Ao chegar no local do evento dirija-se ao ";
                            if ((SessionParticipante != null) && ((SessionParticipante.CdCategoria == "00130401") || (SessionParticipante.CdCategoria == "00130417") || (SessionParticipante.CdCategoria == "00130501") || (SessionParticipante.CdCategoria == "00130517")))
                                lblMsg.Text += "\"Caixa\"";
                            else
                                lblMsg.Text += "\"Balcão de Credendiamento\"";

                            lblMsg.Text += " para concluir seu processo de inscrição";


                            Button1.Visible = false;
                            break;
                        }
                    case "036":
                        {//inscrição com confirmação temporário WICT
                            lblMsg.Text =
                                "<p>&nbsp;</p> " +
                                "<p><span>Bem-vindo ao Congresso Mundial de Tecnologia da Informa&ccedil;&atilde;o &ndash; WCIT Brasil 2016!</span></p> " +
                                "<p><span>&nbsp;</span></p> " +
                                "<p><span>Seu pedido de inscri&ccedil;&atilde;o foi realizado com sucesso.</span></p> " +
                                "<p><span>&nbsp;</span></p> " +
                                "<p><span>Em breve, voc&ecirc; receber&aacute; um boleto banc&aacute;rio referente &agrave; atividade cadastrada.</span></p> " +
                                "<p><span>&nbsp;</span></p> " +
                                "<p><span>Por favor, confirme se seu email est&aacute; apto a receber mensagens do nosso servidor. Verifique tamb&eacute;m sua caixa de SPAM.</span></p> " +
                                "<p><span>&nbsp;</span></p> " +
                                "<p><span>Aproveite o evento!</span></p> " +
                                "<p>&nbsp;</p> ";


                            Button1.Visible = false;
                            break;
                        }
                    case "037":
                        {

                            if (SessionIdioma == "PTBR")
                                lblMsg.Text = "Não há atividades para configurar!<br /><br />" + Request.QueryString["dsMensagem"];
                            //else if (SessionIdioma == "ENUS")
                            //    lblMsg.Text = "Operation successful<br />Your registration is in our database. Be sure to see the email that was sent to you. <br /> <br />";
                            //else if (SessionIdioma == "ESP")
                            //    lblMsg.Text = "Operación con éxito<br />Su registro se encuentra en nuestra base de datos. Asegúrese de ver el correo electrónico que fue enviado a usted. <br /><br />";
                            //else if (SessionIdioma == "FRA")
                            //    lblMsg.Text = "Opération réussie!<br />Votre inscription est dans notre base de données. Soyez sûr de voir l`e-mail qui vous a été envoyé.<br /><br /> ";

                            Button1.PostBackUrl = "~/frmCadastroAuto.aspx";
                            Button1.Visible = true;
                            break;
                        }
                    case "038":
                        { //---retornar  para Lista de Trabalhos
                            lblMsg.Text = "Operação realizada com sucesso!<br /><br />";

                            Button1.PostBackUrl = "~/frmTrabalhosLista.aspx";
                            Button1.Visible = true;
                            break;


                        }
                    case "039":
                        { //---retornar  para Lista de Trabalhos
                            lblMsg.Text = "Prazo para emissão de certificado encerrado. Entre em contato com a organização do evento.";

                            //Button1.PostBackUrl = "~/frmTrabalhosLista.aspx";
                            //Button1.Visible = true;
                            break;


                        }
                    case "040":
                        { //---retornar  para Lista de Trabalhos
                            lblMsg.Text =
                                "<p>&nbsp;</p> " +
                                "<p>Agradecemos por&nbsp;sua&nbsp;aten&ccedil;&atilde;o.</p> " +
                                "<p>Se voc&ecirc;&nbsp;n&atilde;o pode&nbsp;mesmo&nbsp;participar, a gente&nbsp;entende.</p> " +
                                "<p>Mas,&nbsp;caso&nbsp;queira&nbsp;confirmar sua&nbsp;presen&ccedil;a depois, &eacute;&nbsp;s&oacute; clicar&nbsp;novamente no link que enviamos&nbsp;por&nbsp;SMS.</p> " +
                                "<p>&nbsp;</p> ";

                            //Button1.PostBackUrl = "~/frmTrabalhosLista.aspx";
                            //Button1.Visible = true;
                            break;


                        }
                    case "041":
                        {//inscrição com confirmação temporário Boleto
                            lblMsg.Text =
                                "<p>&nbsp;</p> " +
                                "<p><span>Bem-vindo ao " + SessionEvento.NoEvento + ".</span></p> " +
                                "<p><span>&nbsp;</span></p> " +
                                "<p><span>Seu pedido de inscri&ccedil;&atilde;o foi realizado com sucesso.</span></p> " +
                                "<p><span>&nbsp;</span></p> " +
                                "<p><span>Seu boleto será enviado por email em até 5 dias úteis após a confirmação do registro junto a instituição financeira, segundo norma vigente da FEBRABAN. " +
                                "Por favor, confirme se seu email est&aacute; apto a receber mensagens do nosso servidor. Verifique tamb&eacute;m sua caixa de SPAM.</span></p> " +
                                "<p><span>&nbsp;</span></p> " +
                                "<p><span>Em caso de dúvida entre em contato conosco " + oClsFuncoes.MascaraGerar(SessionEvento.eventoDados.NuFone, "(99) 999999999") + ".</span></p> " +
                                "<p><span>&nbsp;</span></p> " +
                                "<p><span>Agradecemos pela participação!</span></p> " +
                                "<p>&nbsp;</p> ";

                            if (SessionEvento.CdEvento == "007002")
                            {
                                lblMsg.Text =
                                    @"<p>You are nearly there&hellip;</p>
                                    <p>We have received your submission correctly and the final step to secure your place in the BIO Latin America Conference 2018 is making the payment of your registration.</p>
                                    <p>The payment document (boleto) is being properly registered by our bank system, and the information to download it will be sent to your e-mail within the next 48 hours. Please remember to check your spam box.</p>
                                    <p>If you have any questions, please contact us at <a href=\mailto:bla@biominas.org.br\>bla@biominas.org.br</a>.</p>
                                    <p>Best,</p>
                                    <p>BIO Latin America Team</p>";
                            }

                            if (SessionEvento.DsLinkRedirecionamento != "")
                            {
                                Button1.PostBackUrl = SessionEvento.DsLinkRedirecionamento;
                            }
                            else
                            {
                                Button1.PostBackUrl = "~/frmCadastroAuto.aspx";
                            }
                            Button1.Visible = true;
                            Button1.Focus();
                            break;
                        }
                    case "042":
                        {//inscrição com confirmação temporário Boleto
                            lblMsg.Text =
                                "<p>&nbsp;</p> " +
                                "<p><span>Prezado(a) Senhor(a)</span></p> " +
                                "<p><span>&nbsp;</span></p> " +
                                "<p><span>Seu pedido de inscri&ccedil;&atilde;o foi realizado com sucesso.</span></p> " +
                                "<p><span>&nbsp;</span></p> " +
                                "<p><span>Porém, seu boleto será enviado por email em até 5 dias úteis após a confirmação do registro junto a instituição financeira, segundo norma vigente da FEBRABAN. " +
                                "Por favor, confirme se seu email est&aacute; apto a receber mensagens do nosso servidor. Verifique tamb&eacute;m sua caixa de SPAM.</span></p> " +
                                "<p><span>&nbsp;</span></p> " +
                                "<p><span>Em caso de dúvida entre em contato conosco " + oClsFuncoes.MascaraGerar(SessionEvento.eventoDados.NuFone, "(99) 999999999") + ".</span></p> " +
                                "<p><span>&nbsp;</span></p> " +
                                "<p><span>Agradecemos pela participação!</span></p> " +
                                "<p>&nbsp;</p> ";


                            if (SessionEvento.CdEvento == "007002")
                            {
                                lblMsg.Text =
                                    @"<p>You are nearly there&hellip;</p>
                                    <p>We have received your submission correctly and the final step to secure your place in the BIO Latin America Conference 2018 is making the payment of your registration.</p>
                                    <p>The payment document (boleto) is being properly registered by our bank system, and the information to download it will be sent to your e-mail within the next 48 hours. Please remember to check your spam box.</p>
                                    <p>If you have any questions, please contact us at <a href=\mailto:bla@biominas.org.br\>bla@biominas.org.br</a>.</p>
                                    <p>Best,</p>
                                    <p>BIO Latin America Team</p>";
                            }

                            //if (SessionEvento.DsLinkRedirecionamento != "")
                            //{
                            //    Button1.PostBackUrl = SessionEvento.DsLinkRedirecionamento;
                            //}
                            //else
                            //{
                            //    Button1.PostBackUrl = "~/frmCadastroAuto.aspx";
                            //}
                            //Button1.Visible = true;
                            //Button1.Focus();
                            break;
                        }
                    case "043":
                        {

                            if (SessionIdioma == "PTBR")
                                lblMsg.Text = "Erro ao buscar fator de conversão em moeda estrangeira.<br /><br />";
                            else if (SessionIdioma == "ENUS")
                                lblMsg.Text = "Error fetching foreign currency conversion factor. <br /> <br />";
                            else if (SessionIdioma == "ESP")
                                lblMsg.Text = "Error al buscar el factor de conversión en moneda extranjera. <br /><br />";
                            else if (SessionIdioma == "FRA")
                                lblMsg.Text = "Erreur lors de l'extraction du facteur de conversion de devise étrangère.<br /><br /> ";

                            Button1.PostBackUrl = "~/frmCadastroAuto.aspx";
                            Button1.Visible = true;
                            break;
                        }
                    case "044":
                        {

                            if (SessionIdioma == "PTBR")
                                lblMsg.Text = "Fator de conversão em moeda estrangeira não cadastrado.<br /><br />";
                            else if (SessionIdioma == "ENUS")
                                lblMsg.Text = "Foreign currency conversion factor not registered. <br /> <br />";
                            else if (SessionIdioma == "ESP")
                                lblMsg.Text = "Factor de conversión en moneda extranjera no registrada. <br /><br />";
                            else if (SessionIdioma == "FRA")
                                lblMsg.Text = "Facteur de conversion en monnaie étrangère non enregistré.<br /><br /> ";

                            Button1.PostBackUrl = "~/frmCadastroAuto.aspx";
                            Button1.Visible = true;
                            break;
                        }
                    case "045":
                        {

                            if (SessionIdioma == "PTBR")
                                lblMsg.Text = "Limite de vagas atingido!<br /><br />";
                            else if (SessionIdioma == "ENUS")
                                lblMsg.Text = "Boundary limit hit!<br /> <br />";
                            else if (SessionIdioma == "ESP")
                                lblMsg.Text = "¡Límite de vacantes alcanzado!<br /><br />";
                            else if (SessionIdioma == "FRA")
                                lblMsg.Text = "Limite de limite atteinte<br /><br /> ";

                            if (SessionEvento.DsLinkRedirecionamento != "")
                            {
                                Button1.PostBackUrl = SessionEvento.DsLinkRedirecionamento;

                                Button1.Visible = true;
                            }
                            break;
                        }

                }
            }

            if ((SessionEvento != null) &&(SessionEvento.CdEvento == "005701"))
                Button1.Focus();
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

    protected void Button1_Click_old(object sender, EventArgs e)
    {
        //Server.Transfer(string.Format("frmCadastroAuto.aspx?cdMatricula={0}",
        //                                   SessionParticipante.CdParticipante), true);
        //Server.Transfer(string.Format("frmCadastroAuto.aspx"), true);

        //Response.Write("<script>window.open('frmCadastroAuto.aspx?cdMatricula=" + SessionParticipante.CdParticipante + "','_self');</script>");
        //Response.Write("<script>window.open('frmCadastroAuto.aspx','_self');</script>");
        if (SessionEvento.CdEvento == "005502")
            Server.Transfer(string.Format("frmCertificado.aspx?p={0}",
                                       cllEventos.Crypto.EncryptStringAES(SessionParticipante.CdParticipante)), true);

        if ((SessionEvento.CdEvento == "006501") && (SessionEvento.DsLinkRedirecionamento != ""))
            Response.Write("<script>window.open('" + SessionEvento.DsLinkRedirecionamento + "','_self');</script>");

        if (SessionEvento.CdEvento == "013101")
        {
            
            Response.Redirect("~/index.aspx?codEvento=MDEzMTAx&cdLng=PTBR&cat=&atv=&keyAut=&tpSist=NRM&tpAcesso=NRM");
        }
    }

    private string CorpoEmailABIMDE(Participante prmParticipante)
    {

        string retorno;

        if (prmParticipante.DsIdioma == "PTBR")
        {
            retorno =
                "<p>&nbsp;</p> " +
                "<p>Confirma&ccedil;&atilde;o de Credenciamento de Visitante</p> " +
                "<p>&nbsp;</p> " +
                "<p>&nbsp;</p> " +
                "<p>Prezado (a) &nbsp;#NOME#&nbsp;<br /> <br /> Muito obrigado por se credenciar para visitar 4ª Mostra BID BRASIL<br /> <br /> <strong>Resumo da inscri&ccedil;&atilde;o</strong></p> " +
                "<p>N&ordm; Inscri&ccedil;&atilde;o:&nbsp;#NRINSCRICAO#</p> " +
                "<p>Nome:&nbsp;#NOME#</p> " +
                "<p>Nome para crach&aacute;:&nbsp;#NOMECRACHA#</p> " +
                "<p>E-mail:&nbsp;#EMAIL#</p> " +
                "<p>Empresa:&nbsp;#EMPRESA#</p> " +
                "<p><br /> Sua credencial de visitante estar&aacute; dispon&iacute;vel:</p> " +
                "<ul> " +
                "<li>Na &aacute;rea de pr&eacute;-credenciados localizada na entrada da feira;</li> " +
                "<li>de 27-29 de setembro de 2016;</li> " +
                "<li>das 14h &agrave;s 18h.</li> " +
                "</ul> " +
                "<p><br /> <strong>4ª Mostra BID BRASIL</strong><br /> Local: Centro de Conven&ccedil;&otilde;es Ulysses Guimar&atilde;es<br /> Endere&ccedil;o: Eixo Monumental - Ala Sul, " + "Bras&iacute;lia - DF<br /> <br /> Atenciosamente,</p> " +
                "<p><br /> 4ª Mostra BID BRASIL Team<br /> http://www.mostrabidbrasil.com.br/<br /> <br /> <strong>Importante:</strong><br /></p> " +
                "<ul> " +
                "<li>Estrangeiros que n&atilde;o moram no Brasil: " + "Recomendamos que verifiquem junto &agrave; Embaixada ou Consulado Brasileiro em seu pa&iacute;s a necessidade de visto de entrada no Brasil.</li>" +
                //"<li>Por se tratar de um evento de neg&oacute;cios e restrito a profissionais do setor, menores de 18 anos, mesmo que acompanhados, N&Atilde;O PODER&Atilde;O ENTRAR POR MOTIVO " + "ALGUM.</li> " +
                "<li>Menores de 18 anos, mesmo que acompanhados, N&Atilde;O PODER&Atilde;O ENTRAR POR MOTIVO " + "ALGUM.</li> " +
                "<li>O evento ser&aacute; filmado e fotografado pela organiza&ccedil;&atilde;o e as imagens poder&atilde;o ser exibidas em internet, m&iacute;dia eletr&ocirc;nica, digital e " + "impressa.</li> " +
                "<li>Poder&aacute; ser solicitada a apresenta&ccedil;&atilde;o de documento de identidade com foto para entrada no evento</li> " +
                "<li>O nome de sua empresa/organiza&ccedil;&atilde;o poder&aacute; ser utilizado nas comunica&ccedil;&otilde;es da 4ª Mostra BID BRASIL.</li> " +
                "<li>&Eacute; proibida a entrada de pessoas trajando bermuda, bon&eacute;, chinelo ou regata.</li> " +
                "</ul> " +
                "<p><img src=\"https://fazendomais.azurewebsites.net/imagensgeral/008701/proibicoes.png\" alt=\"\" width=\"265\" height=\"59\" /></p> " +
                "<p>&nbsp;</p> " +
                "<p>&nbsp;</p> ";

            if (prmParticipante.CdCategoria == "00870103")
            {
                retorno = retorno.Replace("visitante ", "expositor ");
                retorno = retorno.Replace("Visitante", "Expositor");
                retorno = retorno.Replace("para visitar", "para ");
            }
        }
        else
        {
            retorno =
                "<p>&nbsp;</p> " +
                "<p>Visitor Registration Confirmed</p> " +
                "<p>&nbsp;</p> " +
                "<p>&nbsp;</p> " +
                "<p>Dear #NOME#&nbsp;<br /> <br />We would like to thank you for registering to visit the 4ª Mostra BID BRASIL<br /> <br /><strong>Resume</strong></p> " +
                "<p>Registration code:&nbsp;#NRINSCRICAO#</p> " +
                "<p>Name:&nbsp;#NOME#</p> " +
                "<p>Name for badge:&nbsp;#NOMECRACHA#</p> " +
                "<p>E-mail:&nbsp;#EMAIL#</p> " +
                "<p>Company:&nbsp;#EMPRESA#</p> " +
                "<p><br />Your visitor&rsquo;s badge will be available:</p> " +
                "<ul> " +
                "<li>At the Pre-Registered Area located at the entrance of the exhibition;</li> " +
                "<li>27-29, September 2016;</li> " +
                "<li>from 2pm to 6pm.</li> " +
                "</ul> " +
                "<p><br /> <strong>4ª Mostra BID BRASIL</strong><br />Venue: Centro de Conven&ccedil;&otilde;es Ulysses Guimar&atilde;es<br />Address: Eixo Monumental - Ala Sul, Bras&iacute;lia - DF</p> " +
                "<p>&nbsp;</p> " +
                "<p>Please enter the registration code or your ID to collect your badge in the entrance hall of the event.</p> " +
                "<p><br />Regards ,</p> " +
                "<p><br /> 4ª Mostra BID BRASIL Team<br /> http://www.mostrabidbrasil.com.br/<br /> <br /><strong>Important:</strong><br /></p> " +
                "<ul> " +
                "<li>Foreign visitors coming from abroad: We recommend " +
                "that you check with the Brazilian Embassy or Consulate in your country as to whether you need an entry visa for Brazil.</li> " +
                "<li>Since this is a business event and restricted to industry professionals, persons under 18, even if accompanied, WILL NOT BE ALLOWED TO ENTER BY ANY MEANS.</li> " +
                "<li>The exhibition will be filmed and photographed by the organization and photos will be posted on the Internet and be available in electronic, digital and printed media.</li> " +
                "<li>A document ID with photo is required to entry the event</li> " +
                "<li>Your company&rsquo;s name may be used in the 4ª Mostra BID Brasil communication.</li> " +
                "<li>It is prohibited the entry of people wearing: shorts, hat (except militaries), flipflops or tanktop.</li> " +
                "</ul> " +
                "<p><img src=\"https://fazendomais.azurewebsites.net/imagensgeral/008701/proibicoes.png\" alt=\"\" width=\"265\" height=\"59\" /></p> " +
                "<p>&nbsp;</p> " +
                "<p>&nbsp;</p> ";
        }

        retorno = retorno.Replace("#NOME#", prmParticipante.NoParticipante);
        retorno = retorno.Replace("#NRINSCRICAO#", prmParticipante.CdParticipante);
        retorno = retorno.Replace("#NOMECRACHA#", prmParticipante.NoEtiqueta);
        retorno = retorno.Replace("#EMAIL#", prmParticipante.DsEmail);
        retorno = retorno.Replace("#EMPRESA#", prmParticipante.NoInstituicao);


        return retorno;

    }

    protected void Button_novo_Click(object sender, EventArgs e)
    {
        if (SessionEvento.CdEvento == "013101")
        {

            Response.Redirect("~/index.aspx?codEvento=MDEzMTAx&cdLng=PTBR&cat=&atv=&keyAut=&tpSist=NRM&tpAcesso=NRM");
        }
    }
    

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (SessionEvento.CdEvento == "013101")
        {

            Response.Redirect("~/index.aspx?codEvento=MDEzMTAx&cdLng=PTBR&cat=&atv=&keyAut=&tpSist=PSQAVS&tpAcesso=NRM");
        }
    }
}
