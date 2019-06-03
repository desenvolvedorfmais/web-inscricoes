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

public partial class frmAcompanhante : System.Web.UI.Page
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

    AcompanhanteCad oAcompanhanteCad = new AcompanhanteCad();

    DataTable oDTAcompanhantes = new DataTable();

    int totalAcomp = 0;

    string cdBoleto = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        

        if (!Page.IsPostBack)
        {
            if (SessionCnn == null)
                SessionCnn = (SqlConnection)Session["SessionCnn"];
            else
                Session["SessionCnn"] = SessionCnn;

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
                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                "003",
                                oEventoCad.RcMsg), true);
            }

            //if ((SessionEvento.CdEvento == "001002") ||(SessionEvento.CdEvento == "001004"))//abert
            //{
                if (SessionPedido == null)
                    SessionPedido = (Pedido)Session["SessionPedido"];
                else
                    Session["SessionPedido"] = SessionPedido;


                if (SessionPedido != null) // caso venha de formas de pagamento
                {
                    totalAcomp = oPedidoCad.TotalDeAcompanhantesParaCadastro(SessionPedido.CdEvento, SessionPedido.CdPedido, SessionCnn);
                    if (totalAcomp >= 1)
                    {
                        lblTotalInscr.Text = lblTotalInscr.Text.Replace("#total#", totalAcomp.ToString());
                    }
                    else
                    {
                        //Informa que categoria não tem direito de cadastrar acompanhate
                        Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                    "022",
                                    oEventoCad.RcMsg), true);
                    }
                }
                else
                {
                    // caso não venha de formas de pagamento
                    SessionPedido = oPedidoCad.SelUltimoPedido(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);
                    if (SessionPedido != null)
                    {// tem pedido em aberto
                        Session["SessionPedido"] = SessionPedido;
                        totalAcomp = oPedidoCad.TotalDeAcompanhantesParaCadastro(SessionPedido.CdEvento, SessionPedido.CdPedido, SessionCnn);
                        if (totalAcomp >= 1)
                        {
                            lblTotalInscr.Text = lblTotalInscr.Text.Replace("#total#", totalAcomp.ToString());
                        }
                        else
                        {
                            //verificar se tem matricula e tem atividade com acompanhate
                            totalAcomp = oInscricoes.TotalDeAcompanhantesParaCadastro(SessionPedido.CdEvento, SessionParticipante.CdParticipante, SessionCnn);
                            if (totalAcomp >= 1)
                            {
                                lblTotalInscr.Text = lblTotalInscr.Text.Replace("#total#", totalAcomp.ToString());
                            }
                            else
                            {
                                //Informa que categoria não tem direito de cadastrar acompanhate
                                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                            "022",
                                            oEventoCad.RcMsg), true);
                            }
                        }
                    }
                    else
                    {
                        //Informa que categoria não tem direito de cadastrar acompanhate
                        Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                    "022",
                                    oEventoCad.RcMsg), true);
                    }

                }

                Session["totalAcomp"] = totalAcomp;

                if ((Request["p"] != null) && (Request["p"].ToString().Trim() != ""))
                    cdBoleto = cllEventos.Crypto.DecryptStringAES(Request["p"]);

                Session["cdBoleto"] = cdBoleto;
            //}
            //else if (SessionEvento.CdEvento == "002902")//ENAPA
            //{
            //    RFVCPF.Enabled = false;

            //    DataTable dttemp = oInscricoes.ListarAtividadesDoParticipante(SessionParticipante, "002902024", SessionCnn);

            //    if ((dttemp != null) && (dttemp.Rows.Count > 0))
            //    {
            //        totalAcomp = 2;
            //        Session["totalAcomp"] = totalAcomp;
            //        lblTotalInscr.Text = "O Participante pode inscrever até 2 participantes para o ENAPINHA";
            //    }
            //    else
            //        {
            //            //Informa que categoria não tem direito de cadastrar acompanhate
            //            Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
            //                        "022",
            //                        oEventoCad.RcMsg), true);
            //        }
            //}

            lblIdentificador.Text = SessionParticipante.CdParticipante.Trim();
            lblNoParticipante.Text = SessionParticipante.NoParticipante.Trim();

            //CategoriaCad oCategoriaCad = new CategoriaCad();
            //SessionCategoria = oCategoriaCad.Pesquisar(SessionEvento.CdEvento, SessionParticipante.CdCategoria, SessionCnn);

            //Session["SessionCategoria"] = SessionCategoria;
            lblCategoria.Text = SessionParticipante.Categoria.NoCategoria.Trim();



            //oDTAcompanhantes.Columns.Add("");
            oDTAcompanhantes.Columns.Add("cdAcompanhante");
            oDTAcompanhantes.Columns.Add("dsAcompanhante");
            oDTAcompanhantes.Columns.Add("noEtiqueta");
            
       
            CarregarAcompanhantesGrade();
           // Session["oDTAtividadesParticipante"] = oDTAtividadesParticipante;


        }
        else
        {
            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];


            //if ((SessionEvento.CdEvento == "001002") || (SessionEvento.CdEvento == "001004"))
            //{
                SessionPedido = (Pedido)Session["SessionPedido"];
                cdBoleto = (string)Session["cdBoleto"];
            //}

            totalAcomp = (int)Session["totalAcomp"];

            oDTAcompanhantes = (DataTable)Session["oDTAcompanhantes"];

            

        }

        TSManager1.RegisterPostBackControl(btnContinuar);
    }

    private void CarregarAcompanhantesGrade()
    {


        DataTable DTAcompanhantes;

        DTAcompanhantes = oAcompanhanteCad.Listar(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);



        if ((DTAcompanhantes != null) && (DTAcompanhantes.Rows.Count > 0))
        {
           // oDTAcompanhantes.Rows.Clear();


            for (int i = 0; i < DTAcompanhantes.DefaultView.Count; i++)
            {


                oDTAcompanhantes.Rows.Add(
                                    DTAcompanhantes.DefaultView[i]["cdAcompanhante"].ToString().Trim(),
                                    DTAcompanhantes.DefaultView[i]["dsAcompanhante"].ToString().Trim(),
                                    DTAcompanhantes.DefaultView[i]["noEtiqueta"].ToString().Trim());

            }
        }



        Session["oDTAcompanhantes"] = oDTAcompanhantes;

        grd.DataSource = oDTAcompanhantes.DefaultView;
        grd.DataBind();

        if (oDTAcompanhantes.Rows.Count >= totalAcomp)
        {
            btnNovo.Visible = false;
            lblTotalInscr.Text = "O Participante já inscreveu todos os participantes que tem direito!";
            
           // if ((SessionEvento.CdEvento == "001002") || (SessionEvento.CdEvento == "001004"))
                if (cdBoleto.Trim() != "")
                    btnContinuar.Visible = true;
        }

    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        pesquisar(grd.DataKeys[e.NewSelectedIndex].Values[0].ToString());
    }
    protected void btnConfirmar_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        if ((!btnNovo.Visible) && (lblIdAcopanhante.Text == ""))
        {
            lblMsg.Text = "O Participante já inscreveu todos os participantes que tem direito!";
            return;
        }

        if (txtNome.Text == "")
        {
            lblMsg.Text = "Informe o nome do participante!";
            return;
        }

        if (txtNomeEtiqueta.Text == "")
        {
            lblMsg.Text = "Informe o nome da credencial do participante!";
            return;
        }
        if ((SessionEvento.CdEvento == "001002") || (SessionEvento.CdEvento == "001004"))
            if (TXTDsCPF.Text == "")
            {
                lblMsg.Text = "Informe o CPF!";
                return;
            }

        Acompanhante oAcompanhante =
            new Acompanhante(
                SessionEvento.CdEvento,
                SessionParticipante.CdParticipante,
                lblIdAcopanhante.Text,
                txtNome.Text.ToUpper(),
                txtNomeEtiqueta.Text.ToUpper(),
                true,
                TXTDsCPF.Text,
                "",
                0);

        oAcompanhante = oAcompanhanteCad.Gravar(oAcompanhante, SessionCnn);

        if (oAcompanhante != null)
        {
            pesquisar(oAcompanhante.CdAcompanhante);
            
            lblMsg.Text = "Operação Realizada com sucesso!";

        }
        else
        {
            lblMsg.Text = oAcompanhanteCad.RcMsg;
            return;
        }

        CarregarAcompanhantesGrade();



    }
    protected void btnNovo_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";

        lblIdAcopanhante.Text = "";
        txtNome.Text = "";
        txtNomeEtiqueta.Text = "";

        txtNome.Focus();
    }

    protected void pesquisar(string prmCdAcompanhante)
    {
        lblMsg.Text = "";

        lblIdAcopanhante.Text = "";
        txtNome.Text = "";
        txtNomeEtiqueta.Text = "";
        TXTDsCPF.Text = "";
        
        Acompanhante oAcompanhante = oAcompanhanteCad.Pesquisar(SessionEvento.CdEvento, SessionParticipante.CdParticipante, prmCdAcompanhante, SessionCnn);

        if (oAcompanhante == null)
        {
            lblMsg.Text = oAcompanhanteCad.RcMsg;
            return;
        }

        lblIdAcopanhante.Text = oAcompanhante.CdAcompanhante;
        txtNome.Text = oAcompanhante.DsAcompanhante;
        txtNomeEtiqueta.Text = oAcompanhante.NoEtiqueta;
        TXTDsCPF.Text = oClsFuncoes.CPFCNPJMascarar( oAcompanhante.NuCPF);
    }
    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        if ((SessionPedido == null) || (SessionPedido.TpPagamento == ""))
        {
            //Server.Transfer(string.Format("frm_formapagamento.aspx?cdMatricula={0}", SessionParticipante.CdParticipante), true);

            if (((SessionEvento.FlEmiteRecibo)))// && (SessionIdioma == "PTBR")) ||
                //((SessionEvento.FlEmiteRecibo) && (SessionParticipante.Categoria.NoCategoria.Contains("BRAZILIAN"))))
                Server.Transfer(string.Format("frmDadosReciboNF.aspx?cdMatricula={0}", SessionParticipante.CdParticipante), true);
            else
                Server.Transfer(string.Format("frm_formapagamento.aspx?cdMatricula={0}", SessionParticipante.CdParticipante), true);
        }
        else if (SessionPedido.QtdParcelas == 1)
        {

            Server.Transfer(string.Format("frmBoleto.aspx?e={0}&b={1}",
                            cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento),
                            cllEventos.Crypto.EncryptStringAES(cdBoleto)), true);

            //Response.Write("<script>window.open('frmBoleto.aspx?e=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + "&b=" + cllEventos.Crypto.EncryptStringAES(oBoleto.CdBoleto.Substring(6, 6)) + "','_self');</script>");
        }
        else
        {

            Server.Transfer(string.Format("frmListaBoletosPedido.aspx?p={0}",
                            cllEventos.Crypto.EncryptStringAES(SessionPedido.CdPedido.Substring(15, 3))), true);
        }
    }
}