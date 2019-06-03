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

public partial class frmSituacaoCadastroParticipante : System.Web.UI.Page
{
    Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    SqlConnection SessionCnn;

    Pedido SessionPedido;
    PedidoCad oPedidoCad = new PedidoCad();

    Inscricoes oInscricoes = new Inscricoes();

    

    protected void Page_Load(object sender, EventArgs e)
    {
        

        if (!Page.IsPostBack)
        {
            if (SessionCnn == null)
                SessionCnn = (SqlConnection)Session["SessionCnn"];
            else
                Session["SessionCnn"] = SessionCnn;

            //if (SessionParticipante == null)
            //    SessionParticipante = (Participante)Session["SessionParticipante"];
            //else
            //    Session["SessionParticipante"] = SessionParticipante;

            if (SessionEvento == null)
                SessionEvento = (Evento)Session["SessionEvento"];
            else
                Session["SessionEvento"] = SessionEvento;

            if (SessionEvento == null)
            {
                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                "003",
                                oEventoCad.RcMsg), true);
            }
            

        }
        else
        {
            //SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];


            

            

        }

        
    }


   

    private void Pesquisar()
    {


        lblMsg.Visible = false;

        SessionParticipante = oParticipanteCad.Pesquisar(SessionEvento.CdEvento, TXTDsCPF.Text.Replace(".","").Replace("-",""), SessionCnn);
        if (SessionParticipante == null)
        {
            lblMsg.Visible = true;
            lblMsg.Text = oParticipanteCad.RcMsg;
            return;
        }
        
        //Session["SessionParticipante"] = SessionParticipante;


        lblIdentificador.Text = SessionParticipante.CdParticipante.Trim();
        lblNoParticipante.Text = SessionParticipante.NoParticipante.Trim();       
        lblNoCategoria.Text = SessionParticipante.Categoria.NoCategoria.Trim();
        lblFormaPgto.Text = SessionParticipante.DsAuxiliar2;

        lblCadastroAtivo.Visible = true;
        if (SessionParticipante.FlAtivo)
        {
            lblCadastroAtivo.Text = "CADASTRO ATIVO";
            lblCadastroAtivo.BackColor = System.Drawing.Color.Blue;
        }
        else
        {
            lblCadastroAtivo.Text = "CADASTRO INATIVO";
            lblCadastroAtivo.BackColor = System.Drawing.Color.Red;
        }


        lblCadastroConfirmado.Visible = false;
        if ((!SessionParticipante.Categoria.FlAtividades) ||
            ((SessionParticipante.CdCategoria == "00030606") || (SessionParticipante.CdCategoria == "00030607") || 
             (SessionParticipante.CdCategoria == "00030608") || (SessionParticipante.CdCategoria == "00030609") || 
             (SessionParticipante.CdCategoria == "00030610") || (SessionParticipante.CdCategoria == "00030611") || (SessionParticipante.CdCategoria == "00030612")))
        {
            lblCadastroConfirmado.Visible = true;
            lblCadastroConfirmado.Text = "CADASTRO CONFIRMADO";
            lblCadastroConfirmado.BackColor = System.Drawing.Color.Green;            
        }
        else
        {
            DataTable DTAtividadesp;
            Inscricoes oInscricoes = new Inscricoes();
            DTAtividadesp = oInscricoes.ListarAtividadesDoParticipante(SessionParticipante, SessionCnn);

            if ((DTAtividadesp != null) && (DTAtividadesp.Rows.Count > 0))
            {
                lblCadastroConfirmado.Visible = true;
                lblCadastroConfirmado.Text = "CADASTRO CONFIRMADO";
                lblCadastroConfirmado.BackColor = System.Drawing.Color.Green;                
            }
            else
            {
                lblCadastroConfirmado.Visible = true;
                lblCadastroConfirmado.Text = "CADASTRO NÃO CONFIRMADO";
                lblCadastroConfirmado.BackColor = System.Drawing.Color.Red;  
            }
        }
    }
   
    
    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        Pesquisar();

        btnLimpar.Focus();
    }

    protected void btnLimpar_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        TXTDsCPF.Text = "";

        lblIdentificador.Text = "";
        lblNoParticipante.Text = "";
        lblNoCategoria.Text = "";
        lblFormaPgto.Text = "";

        lblCadastroAtivo.Visible = false;
        lblCadastroConfirmado.Visible = false;

        TXTDsCPF.Focus();
    }
}