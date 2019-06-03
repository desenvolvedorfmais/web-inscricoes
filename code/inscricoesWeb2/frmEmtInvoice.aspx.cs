using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.UI.HtmlControls;

using cllEventos;
using CLLFuncoes;

using System.Data.SqlClient;

public partial class frmEmtInvoice : System.Web.UI.Page
{
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();

    SqlConnection SessionCnn;

    String SessionIdioma;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SessionCnn = (SqlConnection)Session["SessionCnn"];

            if (SessionCnn == null)
            {
                lblInformacoes.Text = "Erro ao gerar invoice!";
                return;
            }

            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            if (SessionEvento == null)
            {
                string CdEvento = Request["codEvento"];

                if ((CdEvento.Trim() == "") || (CdEvento.Trim().Length != 4))
                {
                    lblInformacoes.Text = "Erro ao gerar invoice!";
                    return;
                }

                SessionEvento = oEventoCad.Pesquisar(CdEvento, SessionCnn);
                Session["SessionEvento"] = SessionEvento;


                if (SessionEvento == null)
                {
                    lblInformacoes.Text = "Erro ao gerar invoice!";
                    return;
                }
            }


            SessionIdioma = (String)Session["SessionIdioma"];
            if (SessionIdioma == null)
                SessionIdioma = "PTBR";
            Session["SessionIdioma"] = SessionIdioma;

            if (SessionParticipante == null)
            {
                string cdParticipante = Request["codParticipante"];

                if (cdParticipante.Trim() == "") 
                {
                    lblInformacoes.Text = "Erro ao gerar invoice!";
                    return;
                }

                SessionParticipante = oParticipanteCad.Pesquisar(SessionEvento.CdEvento, "cdParticipante", cdParticipante.Trim(), SessionCnn);

                if (SessionParticipante == null)
                {
                    lblInformacoes.Text = "Erro ao gerar invoice!";
                    return;
                }
            }

            //if (oParticipanteCad.GerarCredencial(SessionParticipante, "000000001", SessionCnn))
            {
                SessionParticipante = oParticipanteCad.Pesquisar(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);
                Session["SessionParticipante"] = SessionParticipante;

                //lblMsg.Visible = true;
                //lblMsg.Text = "Foi gerada uma credencial para esta inscrição. Caso opte por gerar uma nova, lembre-se que a credencial anterior será invalidada no sistema.";

                //Server.Transfer("rptEtiqueta.aspx", true);
                //Response.Write("<script>window.open('" + "http://inscricoesweb.fazendomais.com/relatoriosconasems/rptEtiqueta.aspx?e=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + "&p=" + cllEventos.Crypto.EncryptStringAES(SessionParticipante.CdParticipante) + "','_blank');</script>");

                //   Response.Write("<script>window.open('" + "http://testesdotnet.fazendomais.com/rptEtiqueta.aspx?e=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + "&p=" + cllEventos.Crypto.EncryptStringAES(SessionParticipante.CdParticipante) + "','_blank');</script>");
                //Response.Write("<script>window.open('" + "rptEtiqueta.aspx?e=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + "&p=" + cllEventos.Crypto.EncryptStringAES(SessionParticipante.CdParticipante) + "','_blank');</script>");

                PedidoCad oPedidoCad = new PedidoCad();
                Pedido tempPedido = oPedidoCad.SelUltimoPedido(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);

                string link = "<a HREF=\"frmInvoice.aspx?p=" + cllEventos.Crypto.EncryptStringAES(tempPedido.CdPedido) + "\" TARGET=\"_blank\" >clique aqui</a> ";

                lblInformacoes.Text =
                    "<div id=\"informacoes_completas\"> " +
                    "<p><span style=\"font-family: arial, helvetica, sans-serif; font-size: 12pt;\">&nbsp;</span></p> " +
                    "<p><span style=\"font-family: arial, helvetica, sans-serif; font-size: 12pt;\">Prezado(a) Senhor(a),</span></p> " +
                    //"<p><span style=\"font-family: arial, helvetica, sans-serif; font-size: 12pt;\">&nbsp;&nbsp;</span></p> " +
                    "<p><span style=\"font-family: arial, helvetica, sans-serif; font-size: 12pt;\">Aguarde o downlod e imprima o invoice.</span><br /><br /></p> " +
                    "<p><span style=\"font-family: arial, helvetica, sans-serif; font-size: 12pt;\">Caso o download não iniciar " + link + " para baixar o invoice.</span><br /><br /></p> " +
                    "<p><span style=\"font-family: arial, helvetica, sans-serif; font-size: 12pt;\">&nbsp;</span></p> " +
                    "<p><span style=\"font-family: arial, helvetica, sans-serif; font-size: 12pt;\">Agradecemos pela participa&ccedil;&atilde;o.</span></p> " +
                    "<p><span style=\"font-family: arial, helvetica, sans-serif; font-size: 12pt;\">&nbsp;</span></p> " +
                    "</div>  ";

                if (SessionIdioma == "ENUS")
                {
                    link = "<a HREF=\"frmInvoice.aspx?p=" + cllEventos.Crypto.EncryptStringAES(tempPedido.CdPedido) + "\" TARGET=\"_blank\" >click here</a> ";
                    lblInformacoes.Text =
                    "<div id=\"informacoes_completas\"> " +
                    "<p><span style=\"font-family: arial, helvetica, sans-serif; font-size: 12pt;\">&nbsp;</span></p> " +
                    "<p><span style=\"font-family: arial, helvetica, sans-serif; font-size: 12pt;\">Dear Sir / Mrs,</span></p> " +
                    "<p><span style=\"font-family: arial, helvetica, sans-serif; font-size: 12pt;\">&nbsp;&nbsp;</span></p> " +
                    "<p><span style=\"font-family: arial, helvetica, sans-serif; font-size: 12pt;\">Wait for the download and print the invoice.</span><br /><br /></p> " +
                    "<p><span style=\"font-family: arial, helvetica, sans-serif; font-size: 12pt;\">If the download does not start " + link + " to download the invoice.</span><br /><br /></p> " +
                    "<p><span style=\"font-family: arial, helvetica, sans-serif; font-size: 12pt;\">&nbsp;</span></p> " +
                    "<p><span style=\"font-family: arial, helvetica, sans-serif; font-size: 12pt;\">Thank you for participating.</span></p> " +
                    "<p><span style=\"font-family: arial, helvetica, sans-serif; font-size: 12pt;\">&nbsp;</span></p> " +
                    "</div>  ";
                }

                if (SessionIdioma == "ESP")
                {
                    link = "<a HREF=\"frmInvoice.aspx?p=" + cllEventos.Crypto.EncryptStringAES(tempPedido.CdPedido) + "\" TARGET=\"_blank\" >haga clic aquí</a> ";
                    lblInformacoes.Text =
                    "<div id=\"informacoes_completas\"> " +
                    "<p><span style=\"font-family: arial, helvetica, sans-serif; font-size: 12pt;\">&nbsp;</span></p> " +
                    "<p><span style=\"font-family: arial, helvetica, sans-serif; font-size: 12pt;\">Estimado Señor / Señora</span></p> " +
                    "<p><span style=\"font-family: arial, helvetica, sans-serif; font-size: 12pt;\">&nbsp;&nbsp;</span></p> " +
                    "<p><span style=\"font-family: arial, helvetica, sans-serif; font-size: 12pt;\">Espera para descargar e imprimir la factura.</span><br /><br /></p> " +
                    "<p><span style=\"font-family: arial, helvetica, sans-serif; font-size: 12pt;\">Si la descarga no se inicia " + link + " para descargar la factura.</span><br /><br /></p> " +
                    "<p><span style=\"font-family: arial, helvetica, sans-serif; font-size: 12pt;\">&nbsp;</span></p> " +
                    "<p><span style=\"font-family: arial, helvetica, sans-serif; font-size: 12pt;\">Gracias por participar.</span></p> " +
                    "<p><span style=\"font-family: arial, helvetica, sans-serif; font-size: 12pt;\">&nbsp;</span></p> " +
                    "</div>  ";
                }

                //lblInformacoes.Text = lblInformacoes.Text.Replace("#NOMEEVENTO#", SessionEvento.NoEvento);

                HtmlIframe frame = new HtmlIframe();
                //Adicionando os atributos
                frame.Attributes.Add("id", "modalIframeId");
                //frame.Attributes.Add("src", "http://testesdotnet.fazendomais.com/rptEtiqueta.aspx?e=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + "&p=" + cllEventos.Crypto.EncryptStringAES(SessionParticipante.CdParticipante));
                //frame.Attributes.Add("src", "frmCertificado.aspx?e=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + "&p=" + cllEventos.Crypto.EncryptStringAES(SessionParticipante.CdParticipante));
                frame.Attributes.Add("src", "frmInvoice.aspx?p=" + cllEventos.Crypto.EncryptStringAES(tempPedido.CdPedido));
                frame.Attributes.Add("marginwidth", "0");
                frame.Attributes.Add("marginheight", "0");
                frame.Attributes.Add("frameborder", "0");
                frame.Attributes.Add("scrolling", "none");

                this.Controls.Add(frame);
            }
            //else
            //{
            //    lblInformacoes.Visible = true;
            //    lblInformacoes.Text = oParticipanteCad.RcMsg;
            //}
        }
        else
        {
            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            SessionIdioma = (String)Session["SessionIdioma"];
        }
    }
}