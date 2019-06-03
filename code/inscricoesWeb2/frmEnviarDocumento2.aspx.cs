using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using cllEventos;
using CLLFuncoes;

using System.Data;
using System.Data.SqlClient;

using System.IO;

public partial class frmEnviarDocumento2 : System.Web.UI.Page
{

    Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    SqlConnection SessionCnn;

    //String SessionIdioma;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (SessionCnn == null)
                SessionCnn = (SqlConnection)Session["SessionCnn"];
            else
                Session["SessionCnn"] = SessionCnn;

            //SessionIdioma = (String)Session["SessionIdioma"];
            //if (SessionIdioma == null)
            //    SessionIdioma = "PTBR";
            //Session["SessionIdioma"] = SessionIdioma;

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

            

            //Session["SessionCategoria"] = SessionCategoria;
            //if (SessionIdioma == "PTBR")
                lblCategoria.Text = SessionParticipante.Categoria.NoCategoria.Trim();
            //else if (SessionIdioma == "ENUS")
            //    lblCategoria.Text = SessionCategoria.NoCategoriaIngles.Trim();
            //else if (SessionIdioma == "ESP")
            //    lblCategoria.Text = SessionCategoria.NoCategoriaEspanhol.Trim();
            //else if (SessionIdioma == "FRA")
            //    lblCategoria.Text = SessionCategoria.NoCategoriaFrances.Trim();


                if ((SessionParticipante.participanteDocEnviado != null) && (SessionParticipante.participanteDocEnviado.NoDocumento != ""))
                {
                    lblDocEnviado.Visible = true;
                    imgDocEnviado.Visible = true;

                    Label3.Visible = true;
                    FileUpload1.Visible = true;
                    FileUpload2.Visible = true;
                    FileUpload3.Visible = true;
                    btnEnviarDocumento.Visible = true;

                    if (SessionParticipante.participanteDocEnviado.FlArquivoBaixado)
                    {
                        Label3.Visible = false;
                        FileUpload1.Visible = false;
                        FileUpload2.Visible = false;
                        FileUpload3.Visible = false;
                        btnEnviarDocumento.Visible = false;

                        lblDocBaixado.Visible = true;
                        imgDocBaixado.Visible = true;
                    }

                    lblSituacao.Visible = true;
                    if (SessionParticipante.participanteDocEnviado.DsSituacao == "NÃO ANALISADO")
                    {
                        lblSituacao.Text = "  NÃO AVALIADO  ";
                        lblSituacao.BackColor = System.Drawing.Color.Blue;

                        lblTextoEnvioDoc.Text = "Pezado Sr(a), verificamos que o " + (SessionEvento.CdCliente != "0013" ? "documento" : "Termo de Nomeação de Secretário Municipal de Saúde ou detentor de função ou cargo equivalente,") + " já foi enviado. Aguarde a análise pela comissão organizadora.";
                    }
                    else if (SessionParticipante.participanteDocEnviado.DsSituacao == "DEFERIDO")
                    {
                        lblSituacao.Text = "  DEFERIDO  ";
                        lblSituacao.BackColor = System.Drawing.Color.Green;

                        lblTextoEnvioDoc.Text = "";
                    }
                    else //(SessionParticipante.participanteDocEnviado.DsSituacao == "NÃO AVALIADO")
                    {
                        lblSituacao.Text = "  INDEFERIDO  ";
                        lblSituacao.BackColor = System.Drawing.Color.Red;

                        lblTextoEnvioDoc.Text = "Pezado Sr(a), o " + (SessionEvento.CdCliente != "0013" ? "documento" : "Termo de Nomeação de Secretário Municipal de Saúde ou detentor de função ou cargo equivalente,") + " enviado não foi aceito pela comissão organizadora. Favor enviar novo documento conforme especificações necessárias.";
                    }
                }
                else
                {
                    lblDocEnviado.Visible = false;
                    imgDocEnviado.Visible = false;

                    lblTextoEnvioDoc.Text = 
                        "Pezado Sr(a), verificamos que " + (SessionEvento.CdCliente != "0013" ? "o(s) documento(s)" : "o Termo de Nomeação de Secretário Municipal de Saúde ou detentor de função ou cargo equivalente,")+ " necessário(s) para a confirmação da sua inscrição ainda " +
                        "não foi(ram) enviado(s). Solicitamos que faça o envio do(s) documento(s), para isto selecione o(s) arquivo(s) e clique no botão enviar.";
                }

                if (SessionEvento.CdEvento == "001305")
                {
                    if ((Geral.datahoraServidor(SessionCnn) < DateTime.Parse("30/07/2015 00:00:00")) ||
                        ((Geral.datahoraServidor(SessionCnn) < DateTime.Parse("04/08/2015 00:00:00")) && (SessionParticipante.CdCategoria == "00130501")))
                    {
                        btnEnviarDocumento0.Visible = false;
                    }
                }
            
        }
        else
        {
            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];


            //SessionIdioma = (String)Session["SessionIdioma"];

        }

        if (SessionEvento == null)
            Server.Transfer("frmSessaoExpirada.aspx", true);


        TSManager1.RegisterPostBackControl(btnEnviarDocumento);
        TSManager1.RegisterPostBackControl(btnEnviarDocumento0);
    }
    
    protected void btnEnviarDocumento_Click(object sender, EventArgs e)
    {
        string msgArquivo = "";

        msgArquivo = enviarArquivo(SessionParticipante, FileUpload1, "001");
        if (msgArquivo != "")
        {
            lblMsg.Text += msgArquivo;
            lblMsg.Visible = true;
            return;
        }

        if (FileUpload2.PostedFile.FileName != "")
        {
            msgArquivo = enviarArquivo(SessionParticipante, FileUpload2, "002");
            if (msgArquivo != "")
            {
                lblMsg.Text += msgArquivo;
                lblMsg.Visible = true;
                return;
            }
        }

        if (FileUpload3.PostedFile.FileName != "")
        {
            msgArquivo = enviarArquivo(SessionParticipante, FileUpload3, "003");
            if (msgArquivo != "")
            {
                lblMsg.Text += msgArquivo;
                lblMsg.Visible = true;
                return;
            }
        }

        if ((SessionParticipante.participanteDocEnviado != null) && (SessionParticipante.participanteDocEnviado.NoDocumento != ""))
        {
            lblDocEnviado.Visible = true;
            imgDocEnviado.Visible = true;

            FileUpload1.Visible = true;
            FileUpload2.Visible = true;
            FileUpload3.Visible = true;
            btnEnviarDocumento.Visible = true;

            if (SessionParticipante.participanteDocEnviado.FlArquivoBaixado)
            {
                FileUpload1.Visible = false;
                FileUpload2.Visible = false;
                FileUpload3.Visible = false;
                btnEnviarDocumento.Visible = false;

                lblDocBaixado.Visible = true;
                imgDocBaixado.Visible = true;
            }

            lblSituacao.Visible = true;
            if (SessionParticipante.participanteDocEnviado.DsSituacao == "NÃO ANALISADO")
            {
                lblSituacao.Text = "  NÃO AVALIADO  ";
                lblSituacao.BackColor = System.Drawing.Color.Blue;
            }
            else if (SessionParticipante.participanteDocEnviado.DsSituacao == "DEFERIDO")
            {
                lblSituacao.Text = "  DEFERIDO  ";
                lblSituacao.BackColor = System.Drawing.Color.Green;
            }
            else //(SessionParticipante.participanteDocEnviado.DsSituacao == "NÃO AVALIADO")
            {
                lblSituacao.Text = "  INDEFERIDO  ";
                lblSituacao.BackColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            lblDocEnviado.Visible = false;
            imgDocEnviado.Visible = false;
        }

        lblMsg.Text = "Seu documento foi gravado com sucesso";

        
        lblMsg.Visible = true;
    }

    protected string enviarArquivo(Participante prmParticipante, FileUpload prmFileUpload, string prmCdDocumento)
    {
        if (prmParticipante == null)
        {
            //lblMsg.Text = "Tese inválida";
            return "Documento nulo";
        }

        string sURL = Request.Url.ToString().ToLower();
        string caminho = "";
        if (sURL.Contains("localhost"))
        {
            caminho = Server.MapPath("");///inscricoesWeb2(1)");
            caminho += "\\documentos\\" + SessionEvento.CdEvento + "\\";
        }
        else
        {
            caminho = Server.MapPath("");//.Replace("\\inscricoestrabalhos", "");
            caminho += "\\documentos\\" + SessionEvento.CdEvento + "\\";
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
                string extension = Path.GetExtension(prmFileUpload.PostedFile.FileName);
                
                if ((extension.ToLower() != ".odt") && (extension.ToLower() != ".doc") && (extension.ToLower() != ".rtf") && (extension.ToLower() != ".docx") &&
                   (extension.ToLower() != ".odp") && (extension.ToLower() != ".ppt") && (extension.ToLower() != ".pptx") && (extension.ToLower() != ".pdf") &&
                   (extension.ToLower() != ".jpg") && (extension.ToLower() != ".png"))
                {
                    //lblMsg.Text = "Arquivo inválido!";
                    //lblMsg0.Text = "Arquivo inválido!";
                    return "Arquivo inválido!";
                }



                File.Delete(caminho + "doc_" + prmParticipante.CdParticipante + "_" + prmCdDocumento + "_CPF_" + prmParticipante.NuCPFCNPJ + "_" + prmParticipante.NoParticipante + ".odt");//word do brOffice
                File.Delete(caminho + "doc_" + prmParticipante.CdParticipante + "_" + prmCdDocumento + "_CPF_" + prmParticipante.NuCPFCNPJ + "_" + prmParticipante.NoParticipante + ".doc");
                File.Delete(caminho + "doc_" + prmParticipante.CdParticipante + "_" + prmCdDocumento + "_CPF_" + prmParticipante.NuCPFCNPJ + "_" + prmParticipante.NoParticipante + ".rtf");
                File.Delete(caminho + "doc_" + prmParticipante.CdParticipante + "_" + prmCdDocumento + "_CPF_" + prmParticipante.NuCPFCNPJ + "_" + prmParticipante.NoParticipante + ".docx");

                File.Delete(caminho + "doc_" + prmParticipante.CdParticipante + "_" + prmCdDocumento + "_CPF_" + prmParticipante.NuCPFCNPJ + "_" + prmParticipante.NoParticipante + ".pdf");

                File.Delete(caminho + "doc_" + prmParticipante.CdParticipante + "_" + prmCdDocumento + "_CPF_" + prmParticipante.NuCPFCNPJ + "_" + prmParticipante.NoParticipante + ".jpg");

                File.Delete(caminho + "doc_" + prmParticipante.CdParticipante + "_" + prmCdDocumento + "_CPF_" + prmParticipante.NuCPFCNPJ + "_" + prmParticipante.NoParticipante + ".png");

                File.Delete(caminho + "doc_" + prmParticipante.CdParticipante + "_" + prmCdDocumento + "_CPF_" + prmParticipante.NuCPFCNPJ + "_" + prmParticipante.NoParticipante + ".odp");//power point do brOffice
                File.Delete(caminho + "doc_" + prmParticipante.CdParticipante + "_" + prmCdDocumento + "_CPF_" + prmParticipante.NuCPFCNPJ + "_" + prmParticipante.NoParticipante + ".ppt");
                File.Delete(caminho + "doc_" + prmParticipante.CdParticipante + "_" + prmCdDocumento + "_CPF_" + prmParticipante.NuCPFCNPJ + "_" + prmParticipante.NoParticipante + ".pptx");

                prmFileUpload.SaveAs(caminho + "doc_" + prmParticipante.CdParticipante + "_" + prmCdDocumento + "_CPF_" + prmParticipante.NuCPFCNPJ + "_" + prmParticipante.NoParticipante + extension);

                string tmpDsNomeArquivo = "doc_" + prmParticipante.CdParticipante + "_" + prmCdDocumento + "_CPF_" + prmParticipante.NuCPFCNPJ + "_" + prmParticipante.NoParticipante + extension;
                SessionParticipante.participanteDocEnviado = oParticipanteCad.oParticipanteDocEnviadoCad.GravarDocumento(
                    SessionParticipante.CdEvento, 
                    SessionParticipante.CdParticipante,
                    tmpDsNomeArquivo,
                    prmCdDocumento,
                    "",
                    "",
                    SessionCnn);

                

                return "";

                


            }
            catch (Exception ex)
            {
                

                return "Ocorreu ERRO AO ENVIAR DOCUMENTO: " + ex.Message.ToString(); //false;
            }
        }
        else
        {
            

            return "Nehum arquivo foi selecionado para envio.";
        }

    }
    protected void btnEnviarDocumento0_Click(object sender, EventArgs e)
    {
        if (SessionEvento.CdEvento == "001305")
        {
            if ((Geral.datahoraServidor(SessionCnn) < DateTime.Parse("30/07/2015 00:00:00")) || 
                ((Geral.datahoraServidor(SessionCnn) < DateTime.Parse("04/08/2015 00:00:00")) && (SessionParticipante.CdCategoria == "00130501")))
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

        Response.Write("<script>window.open('frmCadastroAuto.aspx','_self');</script>");
    }
}

