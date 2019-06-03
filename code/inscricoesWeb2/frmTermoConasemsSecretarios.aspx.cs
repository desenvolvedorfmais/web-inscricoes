using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using cllEventos;
using CLLFuncoes;

using System.Data.SqlClient;

public partial class frmTermoConasemsSecretarios : System.Web.UI.Page
{
    Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    SqlConnection SessionCnn;

    String SessionIdioma;

    String SessionCateg;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                SessionCnn = (SqlConnection)Session["SessionCnn"];
                Session["SessionCnn"] = SessionCnn;

                SessionParticipante = (Participante)Session["SessionParticipante"];
                Session["SessionParticipante"] = SessionParticipante;

                SessionEvento = (Evento)Session["SessionEvento"];

                SessionCateg = (String)Session["SessionCateg"];
                if (SessionCateg == null)
                    SessionCateg = "";
                Session["SessionCateg"] = SessionCateg;

                Session["SessionEvento"] = SessionEvento;

                if (SessionEvento == null)
                    Server.Transfer("frmSessaoExpirada.aspx", true);

                SessionIdioma = (String)Session["SessionIdioma"];
                if ((SessionIdioma == null) || (SessionIdioma == ""))
                    SessionIdioma = "PTBR";
                Session["SessionIdioma"] = SessionIdioma;

                lblTermoAceite.Text = SessionEvento.DsInformacoesCompletasWeb_fra;

            }
            catch
            {
                //txtMsg.Text = "erro";
            }
        }
        else
        {
            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            SessionIdioma = (String)Session["SessionIdioma"];

            SessionCateg = (String)Session["SessionCateg"];

            if (SessionEvento == null)
                Server.Transfer("frmSessaoExpirada.aspx", true);
        }
        try
        {           

            TSManager1.RegisterPostBackControl(btnCadastrar2);
        }
        catch
        {
            //txtMsg.Text = "erro";
        }
    }

    protected void btnCadastrar_Click(object sender, EventArgs e)
    {

        SessionParticipante.DsAuxiliar19 = "SIM";

        SessionParticipante = oParticipanteCad.Gravar(SessionParticipante, SessionCnn);
        Session["SessionParticipante"] = SessionParticipante;

        if ((Geral.datahoraServidor(SessionCnn) < DateTime.Parse("30/07/2015 00:00:00")))
        {
            PedidoCad oPedidoCad = new PedidoCad();
            Pedido tempPedido = oPedidoCad.VerificaPedidoNaoPagoWeb(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);

            if ((!SessionParticipante.FlDocumentoConfirmado) && ((SessionParticipante.participanteDocEnviado == null) || (SessionParticipante.participanteDocEnviado.DsSituacao == "INDEFERIDO")))
            {
                Response.Write("<script>window.open('frmEnviarDocumento.aspx','_self');</script>");
            }

            if ((SessionParticipante.Categoria.FlAtividades) && (!SessionParticipante.FlConfirmacaoInscricao) && ((tempPedido == null) || (tempPedido.TpPagamento == "")))
                Response.Write("<script>window.open('frmSelAtividades.aspx','_self');</script>");
            else
            {                
                

                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                    "014",
                                    ""), true);
            }
        }
        else
        {
            if ((Geral.datahoraServidor(SessionCnn) < DateTime.Parse("04/08/2015 00:00:00")))
            {
                if ((!SessionParticipante.FlDocumentoConfirmado) && ((SessionParticipante.participanteDocEnviado == null) || (SessionParticipante.participanteDocEnviado.DsSituacao == "INDEFERIDO")))
                {
                    Response.Write("<script>window.open('frmEnviarDocumento.aspx','_self');</script>");
                }
            }

            if (SessionParticipante.CdCategoria != "00130501")
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

            Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                            "014",
                                            (SessionParticipante.Categoria.FlPagamento ? "Dirija-se ao caixa no local do evento para concluir sua inscrição." : "Não esqueça de imprimir sua credencial.")
                                            ), true);

            //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
            //                    "014",
            //                    ""), true);
        }

        //if ((SessionEvento.FlPesquisaCPFReceita) && (SessionIdioma == "PTBR"))
        //{
        //    CategoriaCad oCategoriaCad = new CategoriaCad();
        //    Categoria tmpCategoria = oCategoriaCad.Pesquisar(SessionEvento.CdEvento, SessionCateg, SessionCnn);

        //    if ((tmpCategoria == null) || (!tmpCategoria.FlCPFCNPJObrigatorio))
        //        Response.Write("<script>window.open('frmCadastroAuto.aspx','_self');</script>");
        //    else
        //        Response.Write("<script>window.open('frmBuscaCPFReceita.aspx','_self');</script>");
        //}
        //else
        //    Response.Write("<script>window.open('frmCadastroAuto.aspx','_self');</script>");
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        btnCadastrar2.Enabled = !btnCadastrar2.Enabled;
    }
}