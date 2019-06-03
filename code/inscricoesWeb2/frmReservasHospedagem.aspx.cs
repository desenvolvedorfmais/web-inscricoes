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

public partial class pages_frmReservasHospedagem : System.Web.UI.Page
{
    Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    SqlConnection SessionCnn;

    Atividade SessionAtividade;
    AtividadeCad oAtividadeCad = new AtividadeCad();

    Pedido SessionPedido;
    PedidoCad oPedidoCad = new PedidoCad();

    //Categoria SessionCategoria;

    Inscricoes SessionInscricoes = new Inscricoes();

    //ClsCongresso clOperador;

    DataTable oDTAtividadesParticipante = new DataTable();

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
                //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                //                "003",
                //                oEventoCad.RcMsg), true);

                Server.Transfer("frmSessaoExpirada.aspx", true);
            }

            if ((Request["cdMatricula"] != null) &&
                (Request["cdMatricula"] != ""))
            {

                //txtMatricula.Text = Request["cdMatricula"];

                SessionParticipante = oParticipanteCad.Pesquisar(SessionEvento.CdEvento, Request["cdMatricula"], SessionCnn);
                Session["SessionParticipante"] = SessionParticipante;
                //pesquisar(Request["cdMatricula"]);

            }
            //else
            //{
            //    if ((SessionParticipante != null) && (SessionParticipante.CdParticipante != ""))
            //    {
            //        pesquisar(SessionParticipante.CdParticipante);
            //    }
            //    else
            //    {
            //        SessionParticipante = new Participante();
            //        Session["SessionEvento"] = SessionEvento;
            //    }


            //    //else
            //    //{
            //    //    SqlDataSource1.ConnectionString = SessionCnn.ConnectionString;

            //    //}
            //}

            if (SessionParticipante == null)
                SessionParticipante = (Participante)Session["SessionParticipante"];
            else
                Session["SessionParticipante"] = SessionParticipante;

            lblIdentificador.Text = SessionParticipante.CdParticipante.Trim();
            lblNoParticipante.Text = SessionParticipante.NoParticipante.Trim();

            //CategoriaCad oCategoriaCad = new CategoriaCad();
            //SessionCategoria = oCategoriaCad.Pesquisar(SessionEvento.CdEvento, SessionParticipante.CdCategoria, SessionCnn);

            //Session["SessionCategoria"] = SessionCategoria;
            lblCategoria.Text = SessionParticipante.Categoria.NoCategoria.Trim();

            if ((Request["cdAtividade"] != null) &&
                (Request["cdAtividade"] != ""))
            {

                //txtMatricula.Text = Request["cdMatricula"];

                SessionAtividade = oAtividadeCad.Pesquisar(SessionEvento.CdEvento, Request["cdAtividade"], SessionCnn);
                Session["SessionAtividade"] = SessionAtividade;
                //pesquisar(Request["cdMatricula"]);

                lblAtividade.Text = SessionAtividade.NoSubTitulo;
            }

            if (SessionParticipante.DtAcompanhantes != null)
            {
                Label2part.Visible = true;
                lblSegundoParticipante.Visible = true;
                lblSegundoParticipante.Text = SessionParticipante.DtAcompanhantes.DefaultView[0]["dsAcompanhante"].ToString();
            }

            VerificarHospedagem();

            //if (SessionParticipante.HotelReserva != null)
            //{ //listar Reserva
            //    Hoteis(false);
            //    txtHotel.SelectedValue = SessionParticipante.HotelReserva.CdHotel;
            //    txtHotel.Enabled = false;

            //    TiposAcomodacao(false);
            //    txtTipoAcomodacao.SelectedValue = SessionParticipante.HotelReserva.CdAcomodacao;
            //    txtTipoAcomodacao.Enabled = false;

            //    Quartos(false);
            //    txtQuarto.SelectedValue = SessionParticipante.HotelReserva.CdQuarto;
            //    txtQuarto.Enabled = false;
            //}
            //else
            //{//cadstrar Reserva
            //    Hoteis(true);
            //}
            
        }
        else
        {
            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            SessionAtividade = (Atividade)Session["SessionAtividade"];

            if (SessionEvento == null)
                Server.Transfer("frmSessaoExpirada.aspx", true);
        }

        
    }
        

    protected void VerificarHospedagem()
    {
        SessionParticipante = oParticipanteCad.Pesquisar(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);
        Session["SessionParticipante"] = SessionParticipante;

        

        if (SessionPedido == null)
            SessionPedido = (Pedido)Session["SessionPedido"];
        else
            Session["SessionPedido"] = SessionPedido;


        
        SessionPedido = oPedidoCad.SelUltimoPedido(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);
        if (SessionPedido != null)
        {// tem pedido em aberto
            Session["SessionPedido"] = SessionPedido;

            Inscricoes oInscricoes = new Inscricoes();

            DataTable dt = oInscricoes.ListarAtividadesDoPedido(SessionParticipante, SessionPedido.CdPedido, SessionCnn);

            if ((dt != null) && (dt.Rows.Count > 0))
            {


                bool tmpHospedagem = false;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (
                        ((dt.DefaultView[i]["cdAtividade"].ToString() == "001002002") || (dt.DefaultView[i]["cdAtividade"].ToString() == "001002003")) ||
                        ((dt.DefaultView[i]["cdAtividade"].ToString() == "001004002") || (dt.DefaultView[i]["cdAtividade"].ToString() == "001004003") || (dt.DefaultView[i]["cdAtividade"].ToString() == "001004004") || (dt.DefaultView[i]["cdAtividade"].ToString() == "001004005"))
                        )
                    {
                        tmpHospedagem = true;
                        lblAtividade.Text = dt.DefaultView[i]["noTitulo"].ToString();
                    }
                }

                if (!tmpHospedagem)
                {
                    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                           "023",
                                           oEventoCad.RcMsg), true);
                    return;
                }
            }
            else
            {
                DataTable dt2 = oInscricoes.ListarAtividadesDoParticipante(SessionParticipante, SessionCnn);
                if ((dt2 != null) || (dt2.Rows.Count <= 0)) 
                {
                    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                           "023",
                                           oEventoCad.RcMsg), true);
                    return;
                }

                bool tmpHospedagem = false;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (
                        ((dt2.DefaultView[i]["cdAtividade"].ToString() == "001002002") || (dt2.DefaultView[i]["cdAtividade"].ToString() == "001002003")) ||
                        ((dt.DefaultView[i]["cdAtividade"].ToString() == "001004002") || (dt.DefaultView[i]["cdAtividade"].ToString() == "001004003") || (dt.DefaultView[i]["cdAtividade"].ToString() == "001004004") || (dt.DefaultView[i]["cdAtividade"].ToString() == "001004005"))
                        )
                    {
                        tmpHospedagem = true;
                        lblAtividade.Text = dt2.DefaultView[i]["noTitulo"].ToString();
                    }
                }

                if (!tmpHospedagem)
                {
                    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                           "023",
                                           oEventoCad.RcMsg), true);
                    return;
                }
            }
                
        }
        else
        {
            Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                           "023",
                                           oEventoCad.RcMsg), true);
                return;
        }

        

        

        if (SessionParticipante.HotelReserva != null)
        { //listar Reserva

            HotelCad oHotelCad = new HotelCad();
            Hotel oHotel = oHotelCad.Pesquisar(SessionParticipante.HotelReserva.CdHotel, SessionCnn);

            HotelAcomodacaoCad oHotelAcomodacaoCad = new HotelAcomodacaoCad();
            HotelAcomodacao oHotelAcomodacao = oHotelAcomodacaoCad.Pesquisar(SessionParticipante.HotelReserva.CdHotel, SessionParticipante.HotelReserva.CdAcomodacao, SessionCnn);

            txtHotel.Text = oHotel.NoHotel;

            txtTipoAcomodacao.Text = oHotelAcomodacao.DsTipoAcomodacao;
            
            lblSituacao.Text = SessionParticipante.HotelReserva.DsSituacaoLocacao;

            if (SessionParticipante.HotelReserva.DsSituacaoLocacao == "Reservado")
            {
                btnConfResrva.Visible = true;
            }
            else
            {
                btnConfResrva.Visible = false;
            }
            
        }
        else
        {//não tem direito à hospedagem
            
            
                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                         "026",
                                         ""), true);
            

        }
    }

    protected void btnConfResrva_Click(object sender, EventArgs e)
    {
        Session["SessionParticipante"] = SessionParticipante;

        Session["SessionEvento"] = SessionEvento;

        Session["SessionCnn"] = SessionCnn;

        Server.Transfer(string.Format("frmConfReservaHospedagem.aspx"), true);
    }
}