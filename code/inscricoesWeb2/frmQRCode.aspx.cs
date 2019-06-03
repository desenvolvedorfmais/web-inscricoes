using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using cllEventos;
using CLLFuncoes;

using System.Data.SqlClient;

using ZXing;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using ZXing.Common;

public partial class frmQRCode : System.Web.UI.Page
{

    Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    SqlConnection SessionCnn;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            
            if (SessionCnn == null)
                SessionCnn = (SqlConnection) Session["SessionCnn"];
            else
                Session["SessionCnn"] = SessionCnn;

            //SessionIdioma = (String) Session["SessionIdioma"];
            //if (SessionIdioma == null)
            //    SessionIdioma = "PTBR";
            //Session["SessionIdioma"] = SessionIdioma;

            if (SessionParticipante == null)
                SessionParticipante = (Participante) Session["SessionParticipante"];
            else
                Session["SessionParticipante"] = SessionParticipante;

            if (SessionEvento == null)
                SessionEvento = (Evento) Session["SessionEvento"];
            else
                Session["SessionEvento"] = SessionEvento;

            if (SessionEvento == null)
            {
                //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                //                "003",
                //                oEventoCad.RcMsg), true);

                Server.Transfer("frmSessaoExpirada.aspx", true);

                
            }


            //gerarQRCode();
            preencherMsg();
        }
        else
        {
            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];
            
        }

        if (SessionEvento == null)
            Server.Transfer("frmSessaoExpirada.aspx", true);

        //verificarIdioma(SessionIdioma);
    }

    protected void preencherMsg()
    {

        //margin-left:auto; margin-right:auto;


        lblMsgConfirmacao.Text =
            "<span style=\"font-family: verdana, geneva, sans-serif; font-size: 12pt;\"><p>&nbsp;</p> " +
            "<p><strong>#nome</strong></p> " +
            "<p>&nbsp;</p> " +
            "<p>&nbsp;</p> " +
            "<p>Sua presen&ccedil;a est&aacute; confirmada para o lan&ccedil;amento da nossa nova marca.</p> " +
            "<p>&nbsp;</p> " +
            "<p>" +
            "Cidade:&nbsp;&nbsp;&nbsp;<strong>#cidade</strong><br/> " +
            "Local:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong>#local</strong><br/> " +
            "Data:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong>#data</strong><br/> " +
            "Horário:&nbsp;<strong>#hora</strong></p> " +
            "<p>&nbsp;</p> " +
            "<p><strong>Apresente a imagem a seguir na recep&ccedil;&atilde;o do evento.</strong></p> " +
            "<p>&nbsp;</p> " +
            "<p><img style=\"display:block; \" src=\"#urlImagem\" width=\"150\" height=\"150\"/></p> " +
            "<p>&nbsp;</p> " +
            "<p>Voc&ecirc; pode imprimir&nbsp;ou salvar&nbsp;a imagem&nbsp;no seu celular. S&oacute; n&atilde;o&nbsp;pode&nbsp;esquecer de apresent&aacute;-la.</p> " +
            "<p>&nbsp;</p> " +
            "<p>&nbsp;</p> " +
            "<p>Esperamos&nbsp;por&nbsp;voc&ecirc;&nbsp;na&nbsp;era do encontro!</p> " +
            "<p>&nbsp;</p> " +
            "<p>&nbsp;</p></span> ";

        string urlImagem = "https://fazendomais.azurewebsites.net/imagensgeral/" + SessionEvento.CdEvento + "/QRCode/img" + SessionParticipante.CdParticipante +".jpg";

        lblMsgConfirmacao.Text =
            lblMsgConfirmacao.Text.Replace("#nome", SessionParticipante.NoParticipante)
                .Replace("#cidade", SessionParticipante.DsAuxiliar1)
                .Replace("#local", SessionParticipante.DsAuxiliar2)
                .Replace("#data", SessionParticipante.DsAuxiliar3)
                .Replace("#hora", SessionParticipante.DsAuxiliar5)
                .Replace("#urlImagem", gerarQRCode());//urlImagem);
    }

    protected string gerarQRCode()
    {
        string code = SessionParticipante.CdParticipante;

        var qrGenerator = new BarcodeWriter();
        qrGenerator.Format = BarcodeFormat.QR_CODE;
        qrGenerator.Options = new EncodingOptions() { Height = 200, Width = 200, Margin = 0 };

        //QRCodeGenerator qrGenerator = new QRCodeGenerator();
        //QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);


        //System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
        //imgBarCode.Height = 150;
        //imgBarCode.Width = 150;
        using (Bitmap bitMap = qrGenerator.Write(code))// qrCode.GetGraphic(20))
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] byteImage = ms.ToArray();
                //imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);

                return "data:image/png;base64," + Convert.ToBase64String(byteImage);

                //string caminho = Server.MapPath("");//.Replace("\\inscricoestrabalhos", "");
                //                                    //caminho += "\\imagensgeral\\" + SessionEvento.CdEvento + "\\qrcode\\";
                //caminho += "\\imagensgeral\\" + SessionEvento.CdEvento + "\\qrcode\\";

                //bitMap.Save(caminho + "img" + SessionParticipante.CdParticipante + ".jpg", ImageFormat.Jpeg);
                //imgBarCode.Image = byteImage);
            }
            //plBarCode.Controls.Add(imgBarCode);


        }
    }
}