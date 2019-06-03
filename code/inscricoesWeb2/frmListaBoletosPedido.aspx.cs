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

public partial class frmListaBoletosPedido : System.Web.UI.Page
{
    
    Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    ClsFuncoes oClsFuncoes = new ClsFuncoes();
    
    SqlConnection SessionCnn;

    Pedido SessionPedido;
    PedidoCad oPedidoCad = new PedidoCad();

    //Categoria SessionCategoria;

    DataTable oDT = new DataTable();

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
                //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                //                "003",
                //                oEventoCad.RcMsg), true);

                Server.Transfer("frmSessaoExpirada.aspx", true);
            }

            lblIdentificador.Text = SessionParticipante.CdParticipante.Trim();
            lblNoParticipante.Text = SessionParticipante.NoParticipante.Trim();


            lblCategoria.Text = SessionParticipante.Categoria.NoCategoria.Trim();
            if (SessionEvento.CdEvento == "005503")
                lblCategoria.Text += " / " + SessionParticipante.NoInstituicao;

            grd.Focus();

            if ((Request["p"] != null) &&
                (Request["p"] != ""))
            {
                //string cd_pedido = cllEventos.Crypto.DecryptStringAES(Request["p"].ToString(), "3");
                string cd_pedido = cllEventos.Crypto.DecryptStringAES(Request["p"].ToString());
                cd_pedido = SessionEvento.CdEvento + SessionParticipante.CdParticipante + cd_pedido;
                SessionPedido = oPedidoCad.Pesquisar(SessionEvento.CdEvento, SessionParticipante.CdParticipante, cd_pedido, SessionCnn);
                Session["SessionPedido"] = SessionPedido;
                if (SessionPedido != null)
                {
                    lblNrPedido.Text = SessionPedido.CdPedido;

                    DataTable dtpedido = oPedidoCad.TotalizarPedido(SessionPedido.CdPedido, SessionCnn);
                    if (dtpedido != null)
                    {
                        vlItens.Text = dtpedido.DefaultView[0]["itens"].ToString();
                        vlTotalAtiv.Text = decimal.Parse(dtpedido.DefaultView[0]["tol_Atv"].ToString()).ToString("N2");
                        vlTotalDesc.Text = decimal.Parse(dtpedido.DefaultView[0]["tot_desc"].ToString()).ToString("N2");

                        vlTotalAtiv.Font.Strikeout = (decimal.Parse(vlTotalDesc.Text) > 0);
                    }

                    vlTotalPedido.Text = SessionPedido.VlTotalPedido.ToString("N2");

                    PesquisarBoletos();
                    
                }
            }
            else
            {
                SessionPedido = oPedidoCad.VerificaPedidoNaoPagoWeb(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);
                Session["SessionPedido"] = SessionPedido;

                if (SessionPedido != null)
                {
                    if (SessionPedido.TpPagamento == "")
                    {
                        Server.Transfer(string.Format("frm_formapagamento.aspx?cdMatricula={0}",
                                                        SessionParticipante.CdParticipante), true);
                    }

                    lblNrPedido.Text = SessionPedido.CdPedido;

                    DataTable dtpedido = oPedidoCad.TotalizarPedido(SessionPedido.CdPedido, SessionCnn);
                    if (dtpedido != null)
                    {
                        vlItens.Text = dtpedido.DefaultView[0]["itens"].ToString();
                        vlTotalAtiv.Text = decimal.Parse(dtpedido.DefaultView[0]["tol_Atv"].ToString()).ToString("N2");
                        vlTotalDesc.Text = decimal.Parse(dtpedido.DefaultView[0]["tot_desc"].ToString()).ToString("N2");

                        vlTotalAtiv.Font.Strikeout = (decimal.Parse(vlTotalDesc.Text) > 0);
                    }

                    vlTotalPedido.Text = SessionPedido.VlTotalPedido.ToString("N2");

                    if (SessionEvento.CdCliente == "0013")
                    {
                        MeuBoletoCad oMeuBoletoCad = new MeuBoletoCad();
                        MeuBoleto oMeuBoleto = oMeuBoletoCad.PequisarBoletoDoPedido(SessionEvento.CdEvento, SessionPedido.CdPedido, "001", SessionCnn);
                        if (oMeuBoleto != null)
                            Response.Write("<script>window.open('frmBoleto.aspx?e=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + "&b=" + cllEventos.Crypto.EncryptStringAES(oMeuBoleto.CdBoleto.Substring(6, 6)) + "','_self');</script>");

                    }

                    PesquisarBoletos();
                }
                else
                {
                    if (SessionParticipante != null)
                    {
                        if (SessionParticipante.Categoria.FlAtividades)
                        {
                            if ((SessionEvento.CdCliente == "0013")// &&
                                )
                            {
                                Inscricoes oInscricoes = new Inscricoes();

                                DataTable dt = oInscricoes.ListarAtividadesDoParticipante(SessionParticipante, SessionCnn);

                                if (((dt != null) && (dt.Rows.Count > 0))
                                    && (SessionParticipante.CdCategoria != "00130409") && (SessionParticipante.CdCategoria != "00130509"))
                                {
                                    Response.Write("<script>window.open('frmCadastroAuto.aspx','_self');</script>");
                                }
                                else
                                    Response.Write("<script>window.open('frmSelAtividades.aspx','_self');</script>");

                            }
                            else
                                Response.Write("<script>window.open('frmSelAtividades.aspx','_self');</script>");
                        }
                        else
                            Response.Write("<script>window.open('frmCadastroAuto.aspx','_self');</script>");

                    }
                }
            }

            

        }
        else
        {
            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            SessionPedido = (Pedido)Session["SessionPedido"];

            if (SessionEvento == null)
                Server.Transfer("frmSessaoExpirada.aspx", true);

        }
    }

    protected void PesquisarBoletos()
    {
        grd.DataSource = null;
        grd.DataBind();

        grd2.DataSource = null;
        grd2.DataBind();

        MeuBoletoCad oMeuBoletoCad = new MeuBoletoCad();

        oDT = oMeuBoletoCad.ListarBoeltosPedido(SessionEvento.CdEvento, SessionPedido.CdPedido, SessionCnn);

        if (oDT == null)
        {
            return;
        }

        grd.DataSource = oDT;
        grd.DataBind();

        grd2.DataSource = oDT;
        grd2.DataBind();

    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Image imgimprimir = (Image)e.Row.FindControl("imgImprimir");
            if (e.Row.Cells[4].Text.Trim() == "&nbsp;")
            {
                if (oDT.DefaultView[e.Row.RowIndex]["dsLinkBoletoExterno"].ToString() == "")// (e.Row.Cells[7].Text == "")
                {
                    e.Row.Cells[0].Attributes.Add("onclick",
                        "window.open('frmBoleto.aspx?e=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) +
                        "&b=" + cllEventos.Crypto.EncryptStringAES(e.Row.Cells[1].Text.Substring(6, 6)) +
                        "','_self');");
                    e.Row.Cells[1].Attributes.Add("onclick",
                        "window.open('frmBoleto.aspx?e=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) +
                        "&b=" + cllEventos.Crypto.EncryptStringAES(e.Row.Cells[1].Text.Substring(6, 6)) +
                        "','_self');");
                    e.Row.Cells[2].Attributes.Add("onclick",
                        "window.open('frmBoleto.aspx?e=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) +
                        "&b=" + cllEventos.Crypto.EncryptStringAES(e.Row.Cells[1].Text.Substring(6, 6)) +
                        "','_self');");
                    e.Row.Cells[3].Attributes.Add("onclick",
                        "window.open('frmBoleto.aspx?e=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) +
                        "&b=" + cllEventos.Crypto.EncryptStringAES(e.Row.Cells[1].Text.Substring(6, 6)) +
                        "','_self');");
                    e.Row.Cells[4].Attributes.Add("onclick",
                        "window.open('frmBoleto.aspx?e=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) +
                        "&b=" + cllEventos.Crypto.EncryptStringAES(e.Row.Cells[1].Text.Substring(6, 6)) +
                        "','_self');");
                    e.Row.Cells[5].Attributes.Add("onclick",
                        "window.open('frmBoleto.aspx?e=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) +
                        "&b=" + cllEventos.Crypto.EncryptStringAES(e.Row.Cells[1].Text.Substring(6, 6)) +
                        "','_self');");
                    e.Row.Cells[6].Attributes.Add("onclick",
                        "window.open('frmBoleto.aspx?e=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) +
                        "&b=" + cllEventos.Crypto.EncryptStringAES(e.Row.Cells[1].Text.Substring(6, 6)) +
                        "','_self');");
                }
                else
                {
                    e.Row.Cells[0].Attributes.Add("onclick",
                        "window.open('" + oDT.DefaultView[e.Row.RowIndex]["dsLinkBoletoExterno"].ToString() + "','_blank');");
                    e.Row.Cells[1].Attributes.Add("onclick",
                        "window.open('" + oDT.DefaultView[e.Row.RowIndex]["dsLinkBoletoExterno"].ToString() + "','_blank');");
                    e.Row.Cells[2].Attributes.Add("onclick",
                        "window.open('" + oDT.DefaultView[e.Row.RowIndex]["dsLinkBoletoExterno"].ToString() + "','_blank');");
                    e.Row.Cells[3].Attributes.Add("onclick",
                        "window.open('" + oDT.DefaultView[e.Row.RowIndex]["dsLinkBoletoExterno"].ToString() + "','_blank');");
                    e.Row.Cells[4].Attributes.Add("onclick",
                        "window.open('" + oDT.DefaultView[e.Row.RowIndex]["dsLinkBoletoExterno"].ToString() + "','_blank');");
                    e.Row.Cells[5].Attributes.Add("onclick",
                        "window.open('" + oDT.DefaultView[e.Row.RowIndex]["dsLinkBoletoExterno"].ToString() + "','_blank');");
                    e.Row.Cells[6].Attributes.Add("onclick",
                        "window.open('" + oDT.DefaultView[e.Row.RowIndex]["dsLinkBoletoExterno"].ToString() + "','_blank');");
                }

                imgimprimir.ImageUrl = "~/img/print 18x18.png";
            }
            else
            {
                imgimprimir.ImageUrl = "~/img/accept18x18.png";
            }

            //String temp = "this.style.backgroundColor='white'; this.style.color='#333333'";
            //if ((e.Row.DataItemIndex % 2) == 0)
            //{
            //    temp = "this.style.backgroundColor='#E8E8E8'; this.style.color='#333333'";
            //}

            //e.Row.Attributes.Add("style", "cursor:hand");
            //e.Row.Attributes.Add("onMouseOver", "this.style.backgroundColor='#409DD0'; this.style.color='white' ");
            //e.Row.Attributes.Add("onMouseOut", temp);

        }
    }
    protected void grd2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label labelcdParcela = (Label)e.Row.FindControl("labelcdParcela");
            Label lblcdParcela = (Label)e.Row.FindControl("lblcdParcela");

            Label labelcdBoleto = (Label)e.Row.FindControl("labelcdBoleto");
            Label lblcdBoleto = (Label)e.Row.FindControl("lblcdBoleto");


            Label labeldtVencimento = (Label)e.Row.FindControl("labeldtVencimento");
            Label lbldtVencimento = (Label)e.Row.FindControl("lbldtVencimento");
            lbldtVencimento.Text = DateTime.Parse(lbldtVencimento.Text).ToString("dd/MM/yyyy");

            Label labelvlBoleto = (Label)e.Row.FindControl("labelvlBoleto");
            Label lblvlBoleto = (Label)e.Row.FindControl("lblvlBoleto");

            Label lblLegBol2 = (Label)e.Row.FindControl("lblLegBol2");

            Label labeldtRecebimento = (Label)e.Row.FindControl("labeldtRecebimento");
            Label lbldtRecebimento = (Label)e.Row.FindControl("lbldtRecebimento");
            if (lbldtRecebimento.Text.Trim() != "")
                lbldtRecebimento.Text = DateTime.Parse(lbldtRecebimento.Text).ToString("dd/MM/yyyy");

            Label labeldsDocRecebimento = (Label)e.Row.FindControl("labeldsDocRecebimento");
            Label lbldsDocRecebimento = (Label)e.Row.FindControl("lbldsDocRecebimento");

            


            Image imgimprimir = (Image)e.Row.FindControl("imgImprimir");
            if (lbldtRecebimento.Text.Trim() == "")
            {
                e.Row.Cells[0].Attributes.Add("onclick", "window.open('frmBoleto.aspx?e=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + "&b=" + cllEventos.Crypto.EncryptStringAES(lblcdBoleto.Text.Substring(6, 6)) + "','_self');");
                //e.Row.Cells[1].Attributes.Add("onclick", "window.open('frmBoleto.aspx?e=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + "&b=" + cllEventos.Crypto.EncryptStringAES(e.Row.Cells[1].Text.Substring(6, 6)) + "','_self');");
                //e.Row.Cells[2].Attributes.Add("onclick", "window.open('frmBoleto.aspx?e=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + "&b=" + cllEventos.Crypto.EncryptStringAES(e.Row.Cells[1].Text.Substring(6, 6)) + "','_self');");
                //e.Row.Cells[3].Attributes.Add("onclick", "window.open('frmBoleto.aspx?e=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + "&b=" + cllEventos.Crypto.EncryptStringAES(e.Row.Cells[1].Text.Substring(6, 6)) + "','_self');");
                //e.Row.Cells[4].Attributes.Add("onclick", "window.open('frmBoleto.aspx?e=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + "&b=" + cllEventos.Crypto.EncryptStringAES(e.Row.Cells[1].Text.Substring(6, 6)) + "','_self');");
                //e.Row.Cells[5].Attributes.Add("onclick", "window.open('frmBoleto.aspx?e=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + "&b=" + cllEventos.Crypto.EncryptStringAES(e.Row.Cells[1].Text.Substring(6, 6)) + "','_self');");
                //e.Row.Cells[6].Attributes.Add("onclick", "window.open('frmBoleto.aspx?e=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + "&b=" + cllEventos.Crypto.EncryptStringAES(e.Row.Cells[1].Text.Substring(6, 6)) + "','_self');");

                imgimprimir.ImageUrl = "~/img/print 18x18.png";
            }
            else
            {
                imgimprimir.ImageUrl = "~/img/accept18x18.png";
                lblLegBol2.Text = "Boleto Pago";
                
            }

            //String temp = "this.style.backgroundColor='white'; this.style.color='#333333'";
            //if ((e.Row.DataItemIndex % 2) == 0)
            //{
            //    temp = "this.style.backgroundColor='#E8E8E8'; this.style.color='#333333'";
            //}

            //e.Row.Attributes.Add("style", "cursor:hand");
            //e.Row.Attributes.Add("onMouseOver", "this.style.backgroundColor='#409DD0'; this.style.color='white' ");
            //e.Row.Attributes.Add("onMouseOut", temp);

        }
    }
}
