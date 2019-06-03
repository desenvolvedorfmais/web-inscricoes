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

public partial class frmEnviarDocumento3 : System.Web.UI.Page
{

    Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();

    private ParticipanteDocEnviado SessionParticipanteDocEnviado;
    ParticipanteDocEnviadoCad oParticipanteDocEnviadoCad = new ParticipanteDocEnviadoCad();


    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    SqlConnection SessionCnn;

    String SessionIdioma;

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
            if (SessionIdioma == "PTBR")
            {
                lblCategoria.Text = SessionParticipante.Categoria.NoCategoria.Trim();
                if (SessionEvento.CdEvento == "005503")
                    lblCategoria.Text += " / " + SessionParticipante.NoInstituicao;
            }
            else if (SessionIdioma == "ENUS")
                lblCategoria.Text = SessionParticipante.Categoria.NoCategoriaIngles.Trim();
            else if (SessionIdioma == "ESP")
                lblCategoria.Text = SessionParticipante.Categoria.NoCategoriaEspanhol.Trim();
            else if (SessionIdioma == "FRA")
                lblCategoria.Text = SessionParticipante.Categoria.NoCategoriaFrances.Trim();


            lblCdDoc.Text = Request.QueryString["cdDoc"];

            if (SessionEvento != null)
            {
                SessionParticipanteDocEnviado = oParticipanteDocEnviadoCad.PesquisarDocumento(SessionEvento.CdEvento,
                    SessionParticipante.CdParticipante, lblCdDoc.Text, SessionCnn);

                Session["SessionParticipanteDocEnviado"] = SessionParticipanteDocEnviado;

                if ((SessionParticipanteDocEnviado != null) && (SessionParticipanteDocEnviado.NoDocumento != ""))
                {
                    txtTipoTese.Text = SessionParticipanteDocEnviado.DsTema;
                    txtTitulo.Text = SessionParticipanteDocEnviado.DsDescricao;

                    lblDocEnviado.Visible = true;
                    imgDocEnviado.Visible = true;

                    Label3.Visible = true;
                    FileUpload1.Visible = true;
                    btnEnviarDocumento.Visible = true;

                    if (SessionParticipanteDocEnviado.FlArquivoBaixado)
                    {
                        Label3.Visible = false;
                        FileUpload1.Visible = false;
                        btnEnviarDocumento.Visible = false;

                        lblDocBaixado.Visible = true;
                        imgDocBaixado.Visible = true;
                    }

                    lblSituacao.Visible = true;
                    if (SessionParticipanteDocEnviado.DsSituacao == "NÃO ANALISADO")
                    {
                        lblSituacao.Text = "  NÃO AVALIADO  ";
                        lblSituacao.BackColor = System.Drawing.Color.Blue;

                        //lblTextoEnvioDoc.Text = "Pezado Sr(a), verificamos que o " + (SessionEvento.CdCliente != "0013" ? "documento" : "Termo de Nomeação de Secretário Municipal de Saúde ou detentor de função ou cargo equivalente,") + " já foi enviado. Aguarde a análise pela comissão organizadora.";

                        if (SessionIdioma == "ENUS")
                        {
                            lblSituacao.Text = "  NOT EVALUATED  ";
                            //lblTextoEnvioDoc.Text = "Dear Mr / Ms, we find that the document has already been sent. Wait for analysis by the organizing committee.";
                        }
                        else if (SessionIdioma == "ESP")
                        {
                            lblSituacao.Text = "  NO EVALUADO  ";
                            //lblTextoEnvioDoc.Text = "Estimado Sr / Sra, nos encontramos con que el documento ya ha sido enviado. Espere a que el análisis por el comité organizador.";
                        }
                    }
                    else if (SessionParticipanteDocEnviado.DsSituacao == "DEFERIDO")
                    {
                        lblSituacao.Text = "  DEFERIDO  ";
                        lblSituacao.BackColor = System.Drawing.Color.Green;

                        //lblTextoEnvioDoc.Text = "";

                        if (SessionIdioma == "ENUS")
                            lblSituacao.Text = "  ACCEPTED  ";
                        else if (SessionIdioma == "ESP")
                            lblSituacao.Text = "  CONCEDIDO  ";
                    }
                    else //(SessionParticipanteDocEnviado.DsSituacao == "NÃO AVALIADO")
                    {
                        lblSituacao.Text = "  INDEFERIDO  ";
                        lblSituacao.BackColor = System.Drawing.Color.Red;

                        //lblTextoEnvioDoc.Text = "Pezado Sr(a), o " + (SessionEvento.CdCliente != "0013" ? "documento" : "Termo de Nomeação de Secretário Municipal de Saúde ou detentor de função ou cargo equivalente,") + " enviado não foi aceito pela comissão organizadora. Favor enviar novo documento conforme especificações necessárias.";

                        if (SessionIdioma == "ENUS")
                        {
                            lblSituacao.Text = "  REJECTED  ";
                            //lblTextoEnvioDoc.Text = "Pezado Mr / Ms, the submitted document was not accepted by the organizing committee. Please send new document as required specifications.";
                        }
                        else if (SessionIdioma == "ESP")
                        {
                            lblSituacao.Text = "  RECHAZADO  ";
                            //lblTextoEnvioDoc.Text = "Pezado Sr. / Sra, el documento presentado no fue aceptada por el comité organizador. Por favor envíe nuevo documento como especificaciones requeridas.";
                        }
                    }
                }
                else
                {
                    lblDocEnviado.Visible = false;
                    imgDocEnviado.Visible = false;

                    //lblTextoEnvioDoc.Text = 
                    //    "Pezado Sr(a), verificamos que o " + (SessionEvento.CdCliente != "0013" ? "documento" : "Termo de Nomeação de Secretário Municipal de Saúde ou detentor de função ou cargo equivalente,")+ " necessário para a confirmação da sua inscrição ainda " +
                    //    "não foi enviado. Solicitamos que faça o envio do documento, para isto selecione o arquivo e clique no botão enviar.";
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
        }
        else
        {
            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];


            SessionIdioma = (String)Session["SessionIdioma"];

            SessionParticipanteDocEnviado = (ParticipanteDocEnviado)Session["SessionParticipanteDocEnviado"];

        }

        if (SessionEvento == null)
            Server.Transfer("frmSessaoExpirada.aspx", true);


        TSManager1.RegisterPostBackControl(btnEnviarDocumento);
        TSManager1.RegisterPostBackControl(btnEnviarDocumento0);

        verificarIdioma(SessionIdioma);
    }

    protected void verificarIdioma(string prmIdioma)
    {
        if (prmIdioma == "PTBR")
        {
            SessionIdioma = "PTBR";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Enviar Enunciado";

            lblID.Text = "Nº Cadastro";
            lblPart.Text = "Participante:";
            lblCateg.Text = "Categoria:";

            //lblTextoEnvioDoc.Text = "Pezado Sr(a) verificamos que o documento necessário para a confirmação da sua inscrição ainda não foi enviado. Solicitamos que faça o envio do documento, para isto selecione o arquivo e clique no botão enviar.";
            Label3.Text = "Selecione o arquivo para enviar (Somente arquivos em PDF, DOC, DOCX e RTF)";

            btnEnviarDocumento.Text = "Enviar";
            btnEnviarDocumento0.Text = "Voltar para lista de Enunciados";

            lblDocEnviado.Text = "&nbsp;&nbsp;Documento enviado&nbsp;&nbsp;";
            lblDocBaixado.Text = "&nbsp;&nbsp;Documento baixado&nbsp;&nbsp;";
            lblSituacao.Text = "&nbsp;&nbsp;Não Avaliado&nbsp;&nbsp;";

        }
        else if (prmIdioma == "ENUS")
        {
            SessionIdioma = "ENUS";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Send Document";

            lblID.Text = "Registration no.";
            lblPart.Text = "Participant:";
            lblCateg.Text = "Category:";


            //lblTextoEnvioDoc.Text = "Dear Mr/Mrs the document necessary for the confirmation of your registration has not yet been received. We request that you send the document, in order to do so select the file and click on the Send button.";
            Label3.Text = "Select the file to send (Only PDF, DOC, DOCX and RTF files)";

            btnEnviarDocumento.Text = "Send Document";
            btnEnviarDocumento0.Text = "Send Later";

            lblDocEnviado.Text = "&nbsp;&nbsp;Document Sent&nbsp;&nbsp;";
            lblDocBaixado.Text = "&nbsp;&nbsp;Document Downloaded&nbsp;&nbsp;";
            lblSituacao.Text = "&nbsp;&nbsp;Not evaluated&nbsp;&nbsp;";

        }
        else if (prmIdioma == "ESP")
        {
            SessionIdioma = "ESP";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Enviar Documento";

            lblID.Text = "Nº de Registro";
            lblPart.Text = "Participante:";
            lblCateg.Text = "Categoría";

            //lblTextoEnvioDoc.Text = "Estimado Sr(a), el documento necesario para confirmar su inscripción todavía no fue enviado. Pedimos que envíe el documento, seleccione el archivo y cargue en el enlace envíe. Seleccione el archivo para enviar (solamente archivos PDF, DOC, DOCX, RTF)";
            Label3.Text = "Seleccionar archivo (sólo archivos en PDF, DOC, DOCX y RTF)";

            btnEnviarDocumento.Text = "Enviar Documento";
            btnEnviarDocumento0.Text = "Enviar Después";

            lblDocEnviado.Text = "&nbsp;&nbsp;Documento enviado&nbsp;&nbsp;";
            lblDocBaixado.Text = "&nbsp;&nbsp;Documento descargado&nbsp;&nbsp;";
            lblSituacao.Text = "&nbsp;&nbsp;Sin Calificación&nbsp;&nbsp;";

        }
        else if (prmIdioma == "FRA")
        {
            SessionIdioma = "FRA";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Envoyer le document";

            lblPart.Text = "Participant:";
            lblCateg.Text = "Catégorie:";

            //lblTextoEnvioDoc.Text = "Cher Sr (a) vérifier que le document nécessaire pour confirmation de votre inscription n'a pas encore été envoyé. Nous demandons que fait envoyer le document à celui sélectionnez le fichier et cliquez sur le bouton soumettre.";
            Label3.Text = "Sélectionnez le fichier à télécharger (seuls les fichiers au format PDF, DOC, DOCX et RTF)";

            btnEnviarDocumento.Text = "Envoyer";
            btnEnviarDocumento0.Text = "Après Envoyer";

            lblDocEnviado.Text = "&nbsp;&nbsp;Document envoyé&nbsp;&nbsp;";
            lblDocBaixado.Text = "&nbsp;&nbsp;Documents téléchargés&nbsp;&nbsp;";
            lblSituacao.Text = "&nbsp;&nbsp;Non classéo&nbsp;&nbsp;";

        }
    }
    
    protected void btnEnviarDocumento_Click(object sender, EventArgs e)
    {
        string msgArquivo = enviarArquivo(SessionParticipante);


        if (msgArquivo != "")
        {
            lblMsg.Text += msgArquivo;
            lblMsg.Visible = true;
            return;
        }

        if ((SessionParticipanteDocEnviado != null) && (SessionParticipanteDocEnviado.NoDocumento != ""))
        {
            lblDocEnviado.Visible = true;
            imgDocEnviado.Visible = true;

            FileUpload1.Visible = true;
            btnEnviarDocumento.Visible = true;

            if (SessionParticipanteDocEnviado.FlArquivoBaixado)
            {
                FileUpload1.Visible = false;
                btnEnviarDocumento.Visible = false;

                lblDocBaixado.Visible = true;
                imgDocBaixado.Visible = true;
            }

            lblSituacao.Visible = true;
            if (SessionParticipanteDocEnviado.DsSituacao == "NÃO ANALISADO")
            {
                if (SessionIdioma == "PTBR")
                    lblSituacao.Text = "  NÃO AVALIADO  ";
                else if (SessionIdioma == "ENUS")
                    lblSituacao.Text = "  NOT EVALUATED  ";
                else if (SessionIdioma == "ESP")
                    lblSituacao.Text = "  NO EVALUADO  ";

                lblSituacao.BackColor = System.Drawing.Color.Blue;
            }
            else if (SessionParticipanteDocEnviado.DsSituacao == "DEFERIDO")
            {
                if (SessionIdioma == "PTBR")
                    lblSituacao.Text = "  DEFERIDO  ";
                else if (SessionIdioma == "ENUS")
                    lblSituacao.Text = "  GRANTED  ";
                else if (SessionIdioma == "ESP")
                    lblSituacao.Text = "  CONCEDIDO  ";

                
                lblSituacao.BackColor = System.Drawing.Color.Green;
            }
            else //(SessionParticipanteDocEnviado.DsSituacao == "NÃO AVALIADO")
            {
                if (SessionIdioma == "PTBR")
                    lblSituacao.Text = "  INDEFERIDO  ";
                else if (SessionIdioma == "ENUS")
                    lblSituacao.Text = "  REJECTED  ";
                else if (SessionIdioma == "ESP")
                    lblSituacao.Text = "  RECHAZADO  ";
                                
                lblSituacao.BackColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            lblDocEnviado.Visible = false;
            imgDocEnviado.Visible = false;
        }

        lblMsg.Text = "Confirmamos o envio do arquivo para o " + txtTipoTese.Text+".";
        if (SessionIdioma == "ENUS")
            lblMsg.Text = "Your document has been successfully saved";
        else if (SessionIdioma == "ESP")
            lblMsg.Text = "Documento gravado";

        
        lblMsg.Visible = true;
    }

    protected string enviarArquivo(Participante prmParticipante)
    {
        string retorno = "";
        if (prmParticipante == null)
        {
            //lblMsg.Text = "Tese inválida";
            retorno = "Documento nulo";
            if (SessionIdioma == "ENUS")
                retorno = "Document null";
            else if (SessionIdioma == "ESP")
                retorno = "Documento nulo";

            return retorno;
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
                string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                //FileUpload1.SaveAs(caminho + FileUpload1.FileName);
                if ((extension.ToLower() != ".odt") && (extension.ToLower() != ".doc") && (extension.ToLower() != ".rtf") && (extension.ToLower() != ".docx") &&
                   (extension.ToLower() != ".odp") && (extension.ToLower() != ".ppt") && (extension.ToLower() != ".pptx") && (extension.ToLower() != ".pdf"))
                {
                    //lblMsg.Text = "Arquivo inválido!";
                    //lblMsg0.Text = "Arquivo inválido!";
                    retorno = "Arquivo inválido.";
                    if (SessionIdioma == "ENUS")
                        retorno = "Invalid file.";
                    else if (SessionIdioma == "ESP")
                        retorno = "Archivo no válido.";

                    return retorno;
                }

                //System.Drawing.Image UploadedImage = System.Drawing.Image.FromStream(FileUpload1.PostedFile.InputStream);

                //Label1.Text = "Height: " + UploadedImage.Height.ToString() + " / Width: " + UploadedImage.Width.ToString() + " / size" + FileUpload1.PostedFile.ContentLength.ToString();

                string tempNumDoc = "001";

                DataTable tmpDt = oParticipanteDocEnviadoCad.Listar(SessionEvento.CdEvento, prmParticipante.CdParticipante, SessionCnn);
                if ((tmpDt != null) && (tmpDt.Rows.Count > 0))
                {
                    int ttp = tmpDt.Rows.Count + 1;
                    tempNumDoc = ttp.ToString().PadLeft(3, '0');
                }

                File.Delete(caminho + "doc_" + prmParticipante.CdParticipante + "_" + tempNumDoc + "_CPF_" + prmParticipante.NuCPFCNPJ + "_" + prmParticipante.NoParticipante + ".odt");//word do brOffice
                File.Delete(caminho + "doc_" + prmParticipante.CdParticipante + "_" + tempNumDoc + "_CPF_" + prmParticipante.NuCPFCNPJ + "_" + prmParticipante.NoParticipante + ".doc");
                File.Delete(caminho + "doc_" + prmParticipante.CdParticipante + "_" + tempNumDoc + "_CPF_" + prmParticipante.NuCPFCNPJ + "_" + prmParticipante.NoParticipante + ".rtf");
                File.Delete(caminho + "doc_" + prmParticipante.CdParticipante + "_" + tempNumDoc + "_CPF_" + prmParticipante.NuCPFCNPJ + "_" + prmParticipante.NoParticipante + ".docx");

                File.Delete(caminho + "doc_" + prmParticipante.CdParticipante + "_" + tempNumDoc + "_CPF_" + prmParticipante.NuCPFCNPJ + "_" + prmParticipante.NoParticipante + ".pdf");

                File.Delete(caminho + "doc_" + prmParticipante.CdParticipante + "_" + tempNumDoc + "_CPF_" + prmParticipante.NuCPFCNPJ + "_" + prmParticipante.NoParticipante + ".odp");//power point do brOffice
                File.Delete(caminho + "doc_" + prmParticipante.CdParticipante + "_" + tempNumDoc + "_CPF_" + prmParticipante.NuCPFCNPJ + "_" + prmParticipante.NoParticipante + ".ppt");
                File.Delete(caminho + "doc_" + prmParticipante.CdParticipante + "_" + tempNumDoc + "_CPF_" + prmParticipante.NuCPFCNPJ + "_" + prmParticipante.NoParticipante + ".pptx");

                FileUpload1.SaveAs(caminho + "doc_" + prmParticipante.CdParticipante + "_" + tempNumDoc + "_CPF_" + prmParticipante.NuCPFCNPJ + "_" + prmParticipante.NoParticipante + extension);

                string tmpDsNomeArquivo = "doc_" + prmParticipante.CdParticipante + "_" + tempNumDoc + "_CPF_" + prmParticipante.NuCPFCNPJ + "_" + prmParticipante.NoParticipante + extension;
                SessionParticipanteDocEnviado = oParticipanteDocEnviadoCad.GravarDocumento2(
                    SessionParticipante.CdEvento, 
                    SessionParticipante.CdParticipante,
                    tmpDsNomeArquivo,
                    lblCdDoc.Text,
                    txtTipoTese.Text,
                    txtTitulo.Text,
                    SessionCnn);

                if (SessionParticipanteDocEnviado == null)
                    retorno = oParticipanteDocEnviadoCad.RcMsg;
                else
                {
                    Session["SessionParticipanteDocEnviado"] = SessionParticipanteDocEnviado;
                    lblCdDoc.Text = SessionParticipanteDocEnviado.CdDocumento;

                }

                //Label1.Text = "Operação realizada com sucesso!";// string.Format("File: {0} {1} kb - Content Type {2} ",
                //FileUpload1.PostedFile.FileName, FileUpload1.PostedFile.ContentLength,
                //FileUpload1.PostedFile.ContentType);

                return retorno;// true;

                //lblMsg.Text = caminho + "trab_" + prmTese.CdTese + "_CPF_" + prmTese.NuCPFAutor + "_" + prmTese.NoParticipante + extension;
                //return false;


            }
            catch (Exception ex)
            {
                //lblMsg.Text = "ERRO: " + ex.Message.ToString();
                //lblMsg0.Text = "ERRO: " + ex.Message.ToString();

                retorno = "Ocorreu erro ao enviar o documento: " + ex.Message.ToString(); //false;

                if (SessionIdioma == "ENUS")
                    retorno = "An error occurred while sending the document.";
                else if (SessionIdioma == "ESP")
                    retorno = "Se ha producido un error al enviar el documento.";

                return retorno;
            }
        }
        else
        {
            //lblMsg.Text = "Você deve escolher um arquivo para o upload.";
            //lblMsg0.Text = "Você deve escolher um arquivo para o upload.";

            retorno = "Nenhum arquivo foi selecionado para envio.";

            if (SessionIdioma == "ENUS")
                retorno = "No file selected to send.";
            else if (SessionIdioma == "ESP")
                retorno = "Ningún archivo seleccionado para el envío.";

            return retorno;
        }

    }
    protected void btnEnviarDocumento0_Click(object sender, EventArgs e)
    {
        Response.Write("<script>window.open('frmEnunciadosLista.aspx','_self');</script>");
    }
    protected void btnNovo_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblMsg.Visible = false;
        lblCdDoc.Text = "";

        txtTipoTese.Text = "";
        txtTitulo.Text = "";
        txtTipoTese.Focus();

        imgDocEnviado.Visible = false;
        lblDocEnviado.Visible = false;
        imgDocBaixado.Visible = false;
        lblDocBaixado.Visible = false;
        lblSituacao.Visible = false;

        Label3.Visible = true;
        FileUpload1.Visible = true;
        btnEnviarDocumento.Visible = true;
    }
}

