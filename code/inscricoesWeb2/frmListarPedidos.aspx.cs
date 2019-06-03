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
using Gerencianet.SDK;
using BoletoFacilSDK;
using BoletoFacilSDK.Enums;
using BoletoFacilSDK.Model.Entities;

public partial class frmListarPedidos : System.Web.UI.Page
{
    Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    SqlConnection SessionCnn;

    Pedido SessionPedido;
    PedidoCad oPedidoCad = new PedidoCad();

    MeuBoletoCad oMeuBoletoCad = new MeuBoletoCad();

    //Categoria SessionCategoria;

    DataTable oDT = new DataTable();

    String SessionIdioma;

    DateTime dataSvr;

    protected void Page_Load(object sender, EventArgs e)
    {
        dataSvr = Geral.datahoraServidor(SessionCnn).Date;

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
                Session["SessionParticipante"] = SessionEvento;

            if (SessionEvento == null)
            {
                //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                //                "003",
                //                oEventoCad.RcMsg), true);

                //if (SessionEvento == null)
                    Server.Transfer("frmSessaoExpirada.aspx", true);
            }

            lblIdentificador.Text = SessionParticipante.CdParticipante.Trim();
            lblNoParticipante.Text = SessionParticipante.NoParticipante.Trim();

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
            

            lblEditarPedido.Visible = SessionEvento.FlPermitirEditarPedido;
            imgEditarPedido.Visible = SessionEvento.FlPermitirEditarPedido;

            if (SessionEvento.CdCliente == "0013")
            {
                imgLegCancelarPed.Visible = false;
                lblLegCancel.Visible = false;
            }

            if (SessionEvento.CdCliente == "0016")
            {
                imgLegBoleto.Visible = false;
                lblLegBol.Visible = false;
            }

            

            grd.Focus();

            //CategoriaCad oCategoriaCad = new CategoriaCad();
            //SessionCategoria = oCategoriaCad.Pesquisar(SessionEvento.CdEvento, SessionParticipante.CdCategoria, SessionCnn);

            //if (!SessionCategoria.FlAtividades)
            //{
            //    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
            //                    "008",
            //                    oEventoCad.RcMsg), true);
            //}

            //Session["SessionCategoria"] = SessionCategoria;
            //lblCategoria.Text = SessionParticipante.Categoria.NoCategoria.Trim();


            PesquisarPedidos();

            if (SessionEvento.CdEvento == "003401")
            {
                grd.Columns[3].Visible = false;
                grd.Columns[4].Visible = false;
                grd.Columns[5].Visible = false;
                grd.Columns[6].Visible = false;
                grd.Columns[7].Visible = false;
            }

        }
        else
        {
            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            SessionPedido = (Pedido)Session["SessionPedido"];

            SessionIdioma = (String)Session["SessionIdioma"];

            if (SessionEvento == null)
                Server.Transfer("frmSessaoExpirada.aspx", true);
        }

        

        verificarIdioma(SessionIdioma);
    }

    protected void verificarIdioma(string prmIdioma)
    {
        if (prmIdioma == "PTBR")
        {
            SessionIdioma = "PTBR";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Meus Pedidos";

            lblID.Text = "Nr Cadastro:";
            lblPart.Text = "Participante:";
            lblCateg.Text = "Categoria:";

            lblLegenda.Text = "Legenda:";
            lblLegBol.Visible = true;
            imgLegBoleto.Visible = true;

            if (SessionEvento.CdCliente == "0016")
            {
                imgLegBoleto.Visible = false;
                lblLegBol.Visible = false;
            }

            if (!TiposPagamentoCad.VerificarFormaPagamento(SessionEvento.CdEvento, "BOLETO", SessionCnn))
            {
                imgLegBoleto.Visible = false;
                lblLegBol.Visible = false;
            }

            lblLegImprimir.Visible = true;
            imgLegImprimirRec.Visible = true;
            lblLegVisualizar.Text = "Visualizar Pedido";
            lblLegCancel.Text = "Cancelar Pedido";

            if (SessionEvento.CdEvento == "003401")
            {
                lblTituloPagina.Text = "Acompanhar pré-inscrição no Eixo";
                lblLegBol.Visible = false;
                imgLegBoleto.Visible = false;
                lblLegImprimir.Visible = false;
                imgLegImprimirRec.Visible = false;
            }

        }
        else if (prmIdioma == "ENUS")
        {
            SessionIdioma = "ENUS";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "My Orders";

            lblID.Text = "Registration no.";
            lblPart.Text = "Participant:";
            lblCateg.Text = "Category:";

            lblLegenda.Text = "Subtitles";
            lblLegBol.Visible = false;
            imgLegBoleto.Visible = false;
            lblLegImprimir.Visible = false;
            imgLegImprimirRec.Visible = false;
            lblLegVisualizar.Text = "See my order";
            lblEditarPedido.Text = "Alter order items";
            lblLegCancel.Text = "Cancel Order";

        }
        else if (prmIdioma == "ESP")
        {
            SessionIdioma = "ESP";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Mis pedidos";

            lblID.Text = "Nº de Registro";
            lblPart.Text = "Participante:";
            lblCateg.Text = "Categoría";

            lblLegenda.Text = "Leyenda";
            lblLegBol.Visible = false;
            imgLegBoleto.Visible = false;
            lblLegImprimir.Visible = false;
            imgLegImprimirRec.Visible = false;
            lblLegVisualizar.Text = "Ver el pedido";
            lblEditarPedido.Text = "Modificación de los artículos del pedido";
            lblLegCancel.Text = "Cancelar el pedido";
        }
        else if (prmIdioma == "FRA")
        {
            SessionIdioma = "FRA";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Mes commandes";

            lblPart.Text = "Participant:";
            lblCateg.Text = "Catégorie:";

            lblLegenda.Text = "Légende";
            lblLegBol.Visible = false;
            imgLegBoleto.Visible = false;
            lblLegImprimir.Visible = false;
            imgLegImprimirRec.Visible = false;
            lblLegVisualizar.Text = "Affichage commande articles";
            lblEditarPedido.Text = "Modifier des éléments de commande";
            lblLegCancel.Text = "Annuler la demande";
        }
    }

    protected void PesquisarPedidos()
    {
        grd.DataSource = null;
        grd.DataBind();

        grd2.DataSource = null;
        grd2.DataBind();


        oDT = oPedidoCad.Listar(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);

        if (oDT == null)
        {
            return;
        }

        DataTable dt = oDT.Clone();

        DataRow[] dr = oDT.Select("flAtivo = 1 and (dtVencimentoPedido >= '" + dataSvr + "' or (dtVencimentoPedido < '" + dataSvr + "' and flPago = 1))");

        foreach (DataRow drSimples in dr)
        {
            dt.ImportRow(drSimples);
        }
        oDT = dt;

        grd.DataSource = oDT;
        grd.DataBind();

        grd2.DataSource = oDT;
        grd2.DataBind();

    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {

            
            if (SessionIdioma == "PTBR")
            {
               e.Row.Cells[0].Text = "Pedido nº";
               e.Row.Cells[2].Text = "Dt Pedido";
               e.Row.Cells[3].Text = "Vencimento";
               e.Row.Cells[4].Text = "Total";
               e.Row.Cells[5].Text = "Nº Parc";
               e.Row.Cells[6].Text = "Forma Pagamento";
               e.Row.Cells[7].Text = "Pago";
               e.Row.Cells[8].Text = "Ativo";
            }
            else if (SessionIdioma == "ENUS")
            {
                e.Row.Cells[0].Text = "Order No.";
                e.Row.Cells[2].Text = "Order Date";
                e.Row.Cells[3].Text = "Expiration";
                e.Row.Cells[4].Text = "Total($)";
                e.Row.Cells[5].Text = "Installments";
                e.Row.Cells[6].Text = "Payment method";
                e.Row.Cells[7].Text = "Paid";
                e.Row.Cells[8].Text = "Active";
            }
            else if (SessionIdioma == "ESP")
            {
                e.Row.Cells[0].Text = "Pedido nº";
                e.Row.Cells[2].Text = "Dt Pedido";
                e.Row.Cells[3].Text = "Vencimiento";
                e.Row.Cells[4].Text = "Total($)";
                e.Row.Cells[5].Text = "Nº de Pagos";
                e.Row.Cells[6].Text = "Forma de Pago";
                e.Row.Cells[7].Text = "Pago";
                e.Row.Cells[8].Text = "Activo";
            }
            else if (SessionIdioma == "FRA")
            {
                e.Row.Cells[0].Text = "n° Ordre";
                e.Row.Cells[2].Text = "Demande";
                e.Row.Cells[3].Text = "Échéance";
                e.Row.Cells[4].Text = "Total";
                e.Row.Cells[5].Text = "parcelle n°";
                e.Row.Cells[6].Text = "Mode de paiement";
                e.Row.Cells[7].Text = "Payé";
                e.Row.Cells[8].Text = "Active";
            }

        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            

            int qtdBoletosRecebidos = oMeuBoletoCad.QtdBoletosRecebidosPedido(SessionEvento.CdEvento, e.Row.Cells[0].Text, SessionCnn);
           // string tpPagamento = e.Row.Cells[5].Text.Trim().ToUpper().Replace("&#195;", "A");
            ImageButton btncancelarpedido = (ImageButton)e.Row.FindControl("btnCancelarPedido");

            if ((e.Row.Cells[7].Text != "") && (Boolean.Parse(e.Row.Cells[7].Text) == true)) //pago completo
            {
                if ((SessionEvento.FlEventoComRecebimentos) && (SessionEvento.FlEmiteRecibo) && (SessionIdioma == "PTBR"))
                    {
                        btncancelarpedido.ImageUrl = "~/img/print 18x18.png";
                        btncancelarpedido.PostBackUrl = "~/frmRecibo.aspx?p=" + cllEventos.Crypto.EncryptStringAES(e.Row.Cells[0].Text);//.Substring(15, 3));
                        btncancelarpedido.ToolTip = "Imprimir Recibo";
                        btncancelarpedido.Visible = true;
                    }
                    else
                        btncancelarpedido.Visible = false;
            }
            else
            if ((Boolean.Parse(e.Row.Cells[8].Text) == true) && //ativo
                (qtdBoletosRecebidos == 0))
                //(!(e.Row.Cells[4].Text.Trim().ToUpper().Replace("&#195;", "A").Contains("CARTAO"))))
            {
                
                //if (Boolean.Parse(e.Row.Cells[5].Text) == false) //não pago
                //{
                    btncancelarpedido.ImageUrl = "~/img/delete 18x18.png";
                    //btncancelarpedido.Attributes.Add("OnClick", "return confirm('Deseja cancelar o pedido?');");
                    //btncancelarpedido.ToolTip = "Cancelar Pedido";

                    if (SessionIdioma == "PTBR")
                    {
                        btncancelarpedido.Attributes.Add("OnClick", "return confirm('Deseja cancelar o pedido?');");
                        btncancelarpedido.ToolTip = "Cancelar Pedido";
                    }
                    else if (SessionIdioma == "ENUS")
                    {
                        btncancelarpedido.Attributes.Add("onclick", "return confirm ('Do you want to cancel the order?');");
                        btncancelarpedido.ToolTip = "Cancel Order";
                    }
                    else if (SessionIdioma == "ESP")
                    {
                        btncancelarpedido.Attributes.Add("onclick", "return confirm ('¿Quiere cancelar el pedido?');");
                        btncancelarpedido.ToolTip = "Cancerlar pedido";
                    }
                    else if (SessionIdioma == "FRA")
                    {
                        btncancelarpedido.Attributes.Add("onclick", "return confirm ('Voulez-vous annuler la commande?');");
                        btncancelarpedido.ToolTip = "Annuler la demande";
                    }
                    
                    //btncancelarpedido.PostBackUrl = "~/frmListaBoletosPedido.aspx?cdPedido=" + e.Row.Cells[0].Text;
                    if (SessionEvento.CdCliente != "0013")
                        btncancelarpedido.Visible = true;
                    else
                        btncancelarpedido.Visible = false;
                //}
                //else
                //{
                    
                
                
            }
            else
                btncancelarpedido.Visible = false;

            ImageButton btnboleto = (ImageButton)e.Row.FindControl("btnBoleto");
            if (((e.Row.Cells[7].Text != "") && (Boolean.Parse(e.Row.Cells[7].Text) == false)) && 
                (Boolean.Parse(e.Row.Cells[8].Text) == true) &&
                ((e.Row.Cells[6].Text.Trim().ToUpper().Contains("BOLETO")) ||
                 ((SessionEvento.CdCliente == "0013") && (e.Row.Cells[6].Text.ToUpper().Contains("EMPENHO")))))
            {
                
                btnboleto.ImageUrl = "~/img/barcode18x18.png";
                //string tmp = e.Row.Cells[0].Text.Substring(15, 3);
                btnboleto.PostBackUrl = "~/frmListaBoletosPedido.aspx?p=" + cllEventos.Crypto.EncryptStringAES(e.Row.Cells[0].Text.Substring(15, 3));
                //btnboleto.PostBackUrl = "~/frmListaBoletosPedido.aspx?p=" + e.Row.Cells[0].Text.Substring(15, 3);
                btnboleto.ToolTip = "Impressão de Boleto";
                btnboleto.Visible = true;
            }
            else
                btnboleto.Visible = false;

            ImageButton btnverpedido = (ImageButton)e.Row.FindControl("btnVerPedido");
            //if (Boolean.Parse(e.Row.Cells[8].Text) == true)
            //{
                btnverpedido.ImageUrl = "~/img/visualizar_pedido18x18.png";
                btnverpedido.PostBackUrl = "~/frmAtividadesPedido.aspx?p=" + cllEventos.Crypto.EncryptStringAES(e.Row.Cells[0].Text.Substring(15, 3));
                //btneditarpedido.PostBackUrl = "~/frmAtividadesPedido.aspx?p=" + e.Row.Cells[0].Text.Substring(15, 3);
                //btneditarpedido.ToolTip = "Visualizar itens do Pedido";

                if (SessionIdioma == "PTBR")
                    btnverpedido.ToolTip = "Visualizar itens do Pedido";
                else if (SessionIdioma == "ENUS")
                    btnverpedido.ToolTip = "View order items";
                else if (SessionIdioma == "ESP")
                    btnverpedido.ToolTip = "Ver el pedido";
                else if (SessionIdioma == "FRA")
                    btnverpedido.ToolTip = "Affichage commande articles";

                btnverpedido.Visible = true;
            //}
            //else
            //    btnverpedido.Visible = false;

            ImageButton btneditarpedido = (ImageButton)e.Row.FindControl("btnEditarPedido");
            if (   (SessionEvento.FlPermitirEditarPedido == true) //Evento permite alterar pedido
                && (Boolean.Parse(e.Row.Cells[8].Text) == true) //pedido Ativo
                && (DateTime.Parse(oDT.DefaultView[e.Row.RowIndex]["dtVencimentoPedido"].ToString()) >= dataSvr) //(DateTime.Parse(e.Row.Cells[3].Text) >= dataSvr) // se já venceu não pode alterar
                && ((e.Row.Cells[7].Text != "") && (Boolean.Parse(e.Row.Cells[7].Text) == false))
                && (qtdBoletosRecebidos == 0)
                )
            {
                btneditarpedido.ImageUrl = "~/img/editar_18x18.png";
                btneditarpedido.PostBackUrl = "~/frmSelAtividades.aspx";//?p=" + cllEventos.Crypto.EncryptStringAES(e.Row.Cells[0].Text.Substring(15, 3));
                //btneditarpedido.PostBackUrl = "~/frmAtividadesPedido.aspx?p=" + e.Row.Cells[0].Text.Substring(15, 3);
                //btneditarpedido.ToolTip = "Visualizar itens do Pedido";

                if (SessionIdioma == "PTBR")
                    btneditarpedido.ToolTip = "Altear itens do Pedido";
                else if (SessionIdioma == "ENUS")
                    btneditarpedido.ToolTip = "Alter order items";
                else if (SessionIdioma == "ESP")
                    btneditarpedido.ToolTip = "Ver el pedido";
                else if (SessionIdioma == "FRA")
                    btneditarpedido.ToolTip = "Modifier des éléments de commande";

                btneditarpedido.Visible = true;
            }
            else
                btneditarpedido.Visible = false;


            if ((e.Row.Cells[7].Text != "") && (e.Row.Cells[7].Text.Trim() == "True"))
            {
                if (SessionIdioma == "PTBR")
                    e.Row.Cells[7].Text = "Sim";
                else if (SessionIdioma == "ENUS")
                    e.Row.Cells[7].Text = "Yes";
                else if (SessionIdioma == "ESP")
                    e.Row.Cells[7].Text = "Sí";
                else if (SessionIdioma == "FRA")
                    e.Row.Cells[7].Text = "Oui";
            }
            else
            {
                if (SessionIdioma == "PTBR")
                    e.Row.Cells[7].Text = "Não";
                else if (SessionIdioma == "ENUS")
                    e.Row.Cells[7].Text = "Not";
                else if (SessionIdioma == "ESP")
                    e.Row.Cells[7].Text = "No";
                else if (SessionIdioma == "FRA")
                    e.Row.Cells[7].Text = "Pas";
            }

            if (e.Row.Cells[8].Text.Trim() == "True")
                {
                if (SessionIdioma == "PTBR")
                    e.Row.Cells[8].Text = "Sim";
                else if (SessionIdioma == "ENUS")
                    e.Row.Cells[8].Text = "Yes";
                else if (SessionIdioma == "ESP")
                    e.Row.Cells[8].Text = "Sí";
                else if (SessionIdioma == "FRA")
                    e.Row.Cells[8].Text = "Oui";
            }
            else
            {
                if (SessionIdioma == "PTBR")
                    e.Row.Cells[8].Text = "Não";
                else if (SessionIdioma == "ENUS")
                    e.Row.Cells[8].Text = "Not";
                else if (SessionIdioma == "ESP")
                    e.Row.Cells[8].Text = "No";
                else if (SessionIdioma == "FRA")
                    e.Row.Cells[8].Text = "Pas";
            }
               

            e.Row.Cells[0].Text = e.Row.Cells[0].Text.Substring(15, 3);

            if (e.Row.Cells[1].Text.Trim() == "0")
                e.Row.Cells[1].Text = "Atividade";
            else
                e.Row.Cells[1].Text = "Acompanhante";


            //String temp = "this.style.backgroundColor='white'; this.style.color='#333333'";
            //if ((e.Row.DataItemIndex % 2) == 0)
            //{
            //    temp = "this.style.backgroundColor='#EFF3FB'; this.style.color='#333333'";
            //}



            ////e.Row.Attributes.Add("style", "cursor:hand");
            //e.Row.Attributes.Add("onMouseOver", "this.style.backgroundColor='#409DD0'; this.style.color='white' ");
            //e.Row.Attributes.Add("onMouseOut", temp);

        }
    }
    protected void btnCancelarPedido_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
	    //((GridView)row.NamingContainer).SelectedIndex = row.RowIndex;

        SessionPedido = oPedidoCad.Pesquisar(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionEvento.CdEvento + SessionParticipante.CdParticipante + row.Cells[0].Text, SessionCnn);
        Session["SessionPedido"] = SessionPedido;
        // this.MessageBox1.ShowConfirmation("Deseja cancelar o pedido?", "Cancelar", true, false);

        SessionPedido = oPedidoCad.AtivarDasativar(SessionPedido, SessionCnn);
        Session["SessionPedido"] = SessionPedido;

        if ((SessionPedido != null) && (!SessionPedido.FlAtivo) && (SessionPedido.TpPagamento.Contains("BOLETO")))
        {
            TiposPagamentoCad oTiposPagamentoCad = new TiposPagamentoCad();
            TiposPagamento tpPagamento = oTiposPagamentoCad.PesquisarPorDsTipoPagamento(SessionEvento.CdEvento, SessionPedido.TpPagamento, SessionCnn);

            if (tpPagamento != null)
            {
                if (tpPagamento.DsTecnologia.ToUpper() == "BOLETOFACIL")
                {
                    MeuBoletoCad oMeuBoletoCad = new MeuBoletoCad();
                    MeuBoleto oMeuBoleto = oMeuBoletoCad.PequisarBoletoDoPedido(SessionEvento.CdEvento, SessionPedido.CdPedido, SessionCnn);
                    CancelarBoletoFacil(oMeuBoleto.CdBoletoExterno);
                }
                else if (tpPagamento.DsTecnologia.ToUpper() == "GERENCIANET")
                {
                    MeuBoletoCad oMeuBoletoCad = new MeuBoletoCad();
                    MeuBoleto oMeuBoleto = oMeuBoletoCad.PequisarBoletoDoPedido(SessionEvento.CdEvento, SessionPedido.CdPedido, SessionCnn);
                    CancelarGerenciaNet(oMeuBoleto.CdBoletoExterno);
                }
            }
        }

        Geral oGeral = new Geral();
        oGeral.EnviarEmailCancelaPedido(SessionEvento, SessionParticipante, SessionPedido, SessionCnn);

        PesquisarPedidos();

    }

    private void CancelarGerenciaNet(Int64 prmCdBoletoExterno)
    {
        try
        {
            var clientId = SessionEvento.boletoConfig.FlgProducao ? SessionEvento.boletoConfig.NumClientToken : SessionEvento.boletoConfig.NumClient_TokenTestes;
            var clientSecret = SessionEvento.boletoConfig.FlgProducao ? SessionEvento.boletoConfig.CodClientSecret : SessionEvento.boletoConfig.CodClientSecretTestes;
            var sandBox = SessionEvento.boletoConfig.FlgProducao ? false : true;

            dynamic endpoints = new Endpoints(clientId, clientSecret, sandBox);

            var param = new
            {
                id = prmCdBoletoExterno
            };

            var response = endpoints.CancelCharge(param);


            //return Ok(response);

        }
        catch (GnException e)
        {

            //Console.WriteLine(e.ErrorType);
            //Console.WriteLine(e.Message);
        }
    }

    private void CancelarBoletoFacil(Int64 prmCdBoletoExterno)
    {
        try
        {
            // Cria uma instância do SDK que irá enviar requisições ao ambiente de testes do Boleto Fácil (Sandbox) //012601, 012601000005
            BoletoFacil boletoFacil;
            if (SessionEvento.boletoConfig.FlgProducao)
            {
                boletoFacil = new BoletoFacil(BoletoFacilEnvironment.Production,
                    SessionEvento.boletoConfig.NumClientToken);
                //    "3BD9A27F5B001F531A6133FCB1815F99FE9FD125C322054F63B561387730A315"); // XYZ12345 is the API key
            }
            else
            {
                boletoFacil = new BoletoFacil(BoletoFacilEnvironment.Sandbox,
                    SessionEvento.boletoConfig.NumClient_TokenTestes);
                //"EE8C9E869B3CA4D1AF8BBAF1933E79B4B8AECCA1C034072BA91C85B8983BE9BD");
            }
            Charge charge = new Charge();
            charge.Code = prmCdBoletoExterno.ToString();
            var response = boletoFacil.CancelCharge(charge);
            // foreach (Charge c in response.Data.Charges) {
            //     Console.WriteLine (c);
            // }
            // string resultBoletoResponseJson = JsonConvert.SerializeObject(response.Data.Charges, Formatting.Indented);
            //     var transactation = JsonConvert.DeserializeObject<Charge>(resultBoletoResponseJson);
            //return Ok(response.Success);
        }
        catch (Exception ex)
        {

            //return BadRequest();
        }
    }

    protected void msgBox_YesChoosed(object sender, string Key)
    {
        if (Key == "Cancelar")
        {
            oPedidoCad.AtivarDasativar(SessionPedido, SessionCnn);

            PesquisarPedidos();

            //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
            //                    "018",
            //                    "pedidos"), true);

        }
    }
    protected void grd2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label labelCodPedido = (Label)e.Row.FindControl("labelCodPedido");
            Label lblCodPedido = (Label)e.Row.FindControl("lblCodPedido");           

            Label labelDtPedido = (Label)e.Row.FindControl("labelDtPedido");
            Label lblDtPedido = (Label)e.Row.FindControl("lblDtPedido");
            lblDtPedido.Text = DateTime.Parse(lblDtPedido.Text).ToString("dd/MM/yyyy");

            Label labelVlrPedido = (Label)e.Row.FindControl("labelVlrPedido");
            Label lblVlrPedido = (Label)e.Row.FindControl("lblVlrPedido");

            Label labelQtdParcela = (Label)e.Row.FindControl("labelQtdParcela");
            Label lblQtdParcela = (Label)e.Row.FindControl("lblQtdParcela");

            Label labelFormaPgto = (Label)e.Row.FindControl("labelFormaPgto");
            Label lblFormaPgto = (Label)e.Row.FindControl("lblFormaPgto");

            Label labelPago = (Label)e.Row.FindControl("labelPago");
            Label lblPago = (Label)e.Row.FindControl("lblPago");

            Label labelAtivo = (Label)e.Row.FindControl("labelAtivo");
            Label lblAtivo = (Label)e.Row.FindControl("lblAtivo");


            HtmlGenericControl BotoesPedido = (HtmlGenericControl)e.Row.FindControl("BotoesPedido");
            HtmlGenericControl boletoImg = (HtmlGenericControl)e.Row.FindControl("boletoImg");
            HtmlGenericControl VerPedidoImg = (HtmlGenericControl)e.Row.FindControl("VerPedidoImg");
            HtmlGenericControl AlterarPedidoImg = (HtmlGenericControl)e.Row.FindControl("AlterarPedidoImg");
            HtmlGenericControl ImprimirReciboImg = (HtmlGenericControl)e.Row.FindControl("ImprimirReciboImg");
            HtmlGenericControl CancelarPedidoImg = (HtmlGenericControl)e.Row.FindControl("CancelarPedidoImg");

            if (SessionIdioma == "PTBR")
            {
                labelCodPedido.Text = "Pedido nº";
                labelDtPedido.Text = "Dt Pedido";
                //e.Row.Cells[3].Text = "Vencimento";
                labelVlrPedido.Text = "Total";
                labelQtdParcela.Text = "Nº Parc";
                labelFormaPgto.Text = "Forma Pagamento";
                labelPago.Text = "Pago";
                labelAtivo.Text = "Ativo";
            }
            else if (SessionIdioma == "ENUS")
            {
                labelCodPedido.Text = "Order No.";
                labelDtPedido.Text = "Request";
                //e.Row.Cells[3].Text = "Expiration";
                labelVlrPedido.Text = "Total";
                labelQtdParcela.Text = "Parcel No.";
                labelFormaPgto.Text = "Payment";
                labelPago.Text = "Paid";
                labelAtivo.Text = "Active";
            }
            else if (SessionIdioma == "ESP")
            {
                labelCodPedido.Text = "Orden Nº";
                labelDtPedido.Text = "Solicitud";
                //e.Row.Cells[3].Text = "Vencimiento";
                labelVlrPedido.Text = "Total";
                labelQtdParcela.Text = "Parcela Nº";
                labelFormaPgto.Text = "Tipo de Pago";
                labelPago.Text = "Pagado";
                labelAtivo.Text = "Activo";
            }
            else if (SessionIdioma == "FRA")
            {
                labelCodPedido.Text = "n° Ordre";
                labelDtPedido.Text = "Demande";
                //e.Row.Cells[3].Text = "Échéance";
                labelVlrPedido.Text = "Total";
                labelQtdParcela.Text = "parcelle n°";
                labelFormaPgto.Text = "Mode de paiement";
                labelPago.Text = "Payé";
                labelAtivo.Text = "Active";
            }


            int qtdBoletosRecebidos = oMeuBoletoCad.QtdBoletosRecebidosPedido(SessionEvento.CdEvento, lblCodPedido.Text, SessionCnn);
            // string tpPagamento = e.Row.Cells[5].Text.Trim().ToUpper().Replace("&#195;", "A");
            ImageButton btncancelarpedido = (ImageButton)e.Row.FindControl("btnCancelarPedido2");

            if ((lblPago.Text != "") && (Boolean.Parse(lblPago.Text) == true)) //pago completo
            {
                if ((SessionEvento.FlEventoComRecebimentos) && (SessionEvento.FlEmiteRecibo))
                {
                    btncancelarpedido.ImageUrl = "~/img/print 18x18.png";
                    btncancelarpedido.PostBackUrl = "~/frmRecibo.aspx?p=" + cllEventos.Crypto.EncryptStringAES(lblCodPedido.Text);//.Substring(15, 3));
                    btncancelarpedido.ToolTip = "Imprimir Recibo";
                    //btncancelarpedido.Visible = true;
                    ImprimirReciboImg.Visible = true;

                }
                else
                    ImprimirReciboImg.Visible = false;
                //btncancelarpedido.Visible = false;
            }
            else
                if ((Boolean.Parse(lblAtivo.Text) == true) && //ativo
                    (qtdBoletosRecebidos == 0))
                //(!(e.Row.Cells[4].Text.Trim().ToUpper().Replace("&#195;", "A").Contains("CARTAO"))))
                {

                    //if (Boolean.Parse(e.Row.Cells[5].Text) == false) //não pago
                    //{
                    btncancelarpedido.ImageUrl = "~/img/delete 18x18.png";
                    //btncancelarpedido.Attributes.Add("OnClick", "return confirm('Deseja cancelar o pedido?');");
                    //btncancelarpedido.ToolTip = "Cancelar Pedido";

                    if (SessionIdioma == "PTBR")
                    {
                        btncancelarpedido.Attributes.Add("OnClick", "return confirm('Deseja cancelar o pedido?');");
                        btncancelarpedido.ToolTip = "Cancelar Pedido";
                    }
                    else if (SessionIdioma == "ENUS")
                    {
                        btncancelarpedido.Attributes.Add("onclick", "return confirm ('Do you want to cancel the order?');");
                        btncancelarpedido.ToolTip = "Cancel Order";
                    }
                    else if (SessionIdioma == "ESP")
                    {
                        btncancelarpedido.Attributes.Add("onclick", "return confirm ('¿Quiere cancelar el pedido?');");
                        btncancelarpedido.ToolTip = "Cancelar la solicitud";
                    }
                    else if (SessionIdioma == "FRA")
                    {
                        btncancelarpedido.Attributes.Add("onclick", "return confirm ('Voulez-vous annuler la commande?');");
                        btncancelarpedido.ToolTip = "Annuler la demande";
                    }

                    //btncancelarpedido.PostBackUrl = "~/frmListaBoletosPedido.aspx?cdPedido=" + lblCodPedido2.Text;
                    if (SessionEvento.CdCliente != "0013")
                        CancelarPedidoImg.Visible = true;
                    //btncancelarpedido.Visible = true;
                    else
                        CancelarPedidoImg.Visible = false;
                    //btncancelarpedido.Visible = false;
                    //}
                    //else
                    //{



                }
                else
                    CancelarPedidoImg.Visible = false;
                    //btncancelarpedido.Visible = false;

            ImageButton btnboleto = (ImageButton)e.Row.FindControl("btnBoleto2");
            if (((lblPago.Text != "") && (Boolean.Parse(lblPago.Text) == false)) &&
                (Boolean.Parse(lblAtivo.Text) == true) &&
                ((lblFormaPgto.Text.Trim().ToUpper().Contains("BOLETO")) ||
                 ((SessionEvento.CdCliente == "0013") && (lblFormaPgto.Text.ToUpper().Contains("EMPENHO")))))
            {

                btnboleto.ImageUrl = "~/img/barcode18x18.png";
                //string tmp = lblCodPedido2.Text.Substring(15, 3);
                btnboleto.PostBackUrl = "~/frmListaBoletosPedido.aspx?p=" + cllEventos.Crypto.EncryptStringAES(lblCodPedido.Text.Substring(15, 3));
                //btnboleto.PostBackUrl = "~/frmListaBoletosPedido.aspx?p=" + lblCodPedido2.Text.Substring(15, 3);
                btnboleto.ToolTip = "Impressão de Boleto";
                //btnboleto.Visible = true;
                boletoImg.Visible = true;
            }
            else
                //btnboleto.Visible = false;
                boletoImg.Visible = false;

            ImageButton btnverpedido = (ImageButton)e.Row.FindControl("btnVerPedido2");
            //if (Boolean.Parse(lblAtivo.Text) == true)
            //{
            btnverpedido.ImageUrl = "~/img/visualizar_pedido18x18.png";
            btnverpedido.PostBackUrl = "~/frmAtividadesPedido.aspx?p=" + cllEventos.Crypto.EncryptStringAES(lblCodPedido.Text.Substring(15, 3));
            //btneditarpedido.PostBackUrl = "~/frmAtividadesPedido.aspx?p=" + lblCodPedido2.Text.Substring(15, 3);
            //btneditarpedido.ToolTip = "Visualizar itens do Pedido";

            if (SessionIdioma == "PTBR")
                btnverpedido.ToolTip = "Visualizar itens do Pedido";
            else if (SessionIdioma == "ENUS")
                btnverpedido.ToolTip = "View order items";
            else if (SessionIdioma == "ESP")
                btnverpedido.ToolTip = "Mostrar artículos Solicitud";
            else if (SessionIdioma == "FRA")
                btnverpedido.ToolTip = "Affichage commande articles";

            btnverpedido.Visible = true;
            
            //}
            //else
            //    btnverpedido.Visible = false;

            ImageButton btneditarpedido = (ImageButton)e.Row.FindControl("btnEditarPedido");
            if ((SessionEvento.FlPermitirEditarPedido == true) //Evento permite alterar pedido
                && (Boolean.Parse(lblAtivo.Text) == true) //pedido Ativo
                && (DateTime.Parse(oDT.DefaultView[e.Row.RowIndex]["dtVencimentoPedido"].ToString()) >= dataSvr) //(DateTime.Parse(e.Row.Cells[3].Text) >= dataSvr) // se já venceu não pode alterar
                && ((lblPago.Text != "") && (Boolean.Parse(lblPago.Text) == false))
                && (qtdBoletosRecebidos == 0)
                )
            {
                btneditarpedido.ImageUrl = "~/img/editar_18x18.png";
                btneditarpedido.PostBackUrl = "~/frmSelAtividades.aspx";//?p=" + cllEventos.Crypto.EncryptStringAES(lblCodPedido2.Text.Substring(15, 3));
                //btneditarpedido.PostBackUrl = "~/frmAtividadesPedido.aspx?p=" + lblCodPedido2.Text.Substring(15, 3);
                //btneditarpedido.ToolTip = "Visualizar itens do Pedido";

                if (SessionIdioma == "PTBR")
                    btneditarpedido.ToolTip = "Altear itens do Pedido";
                else if (SessionIdioma == "ENUS")
                    btneditarpedido.ToolTip = "Alter order items";
                else if (SessionIdioma == "ESP")
                    btneditarpedido.ToolTip = "Mostrar artículos Solicitud";
                else if (SessionIdioma == "FRA")
                    btneditarpedido.ToolTip = "Modifier des éléments de commande";

                //btneditarpedido.Visible = true;
                AlterarPedidoImg.Visible = true;
            }
            else
                AlterarPedidoImg.Visible = false;
                //btneditarpedido.Visible = false;


            if ((lblPago.Text != "") && (lblPago.Text.Trim() == "True"))
            {
                if (SessionIdioma == "PTBR")
                    lblPago.Text = "Sim";
                else if (SessionIdioma == "ENUS")
                    lblPago.Text = "Yes";
                else if (SessionIdioma == "ESP")
                    lblPago.Text = "Sí";
                else if (SessionIdioma == "FRA")
                    lblPago.Text = "Oui";
            }
            else
            {
                if (SessionIdioma == "PTBR")
                    lblPago.Text = "Não";
                else if (SessionIdioma == "ENUS")
                    lblPago.Text = "Not";
                else if (SessionIdioma == "ESP")
                    lblPago.Text = "No";
                else if (SessionIdioma == "FRA")
                    lblPago.Text = "Pas";
            }

            if (lblAtivo.Text.Trim() == "True")
            {
                if (SessionIdioma == "PTBR")
                    lblAtivo.Text = "Sim";
                else if (SessionIdioma == "ENUS")
                    lblAtivo.Text = "Yes";
                else if (SessionIdioma == "ESP")
                    lblAtivo.Text = "Sí";
                else if (SessionIdioma == "FRA")
                    lblAtivo.Text = "Oui";
            }
            else
            {
                if (SessionIdioma == "PTBR")
                    lblAtivo.Text = "Não";
                else if (SessionIdioma == "ENUS")
                    lblAtivo.Text = "Not";
                else if (SessionIdioma == "ESP")
                    lblAtivo.Text = "No";
                else if (SessionIdioma == "FRA")
                    lblAtivo.Text = "Pas";
            }


            lblCodPedido.Text = lblCodPedido.Text.Substring(15, 3);

            //if (e.Row.Cells[1].Text.Trim() == "0")
            //    e.Row.Cells[1].Text = "Atividade";
            //else
            //    e.Row.Cells[1].Text = "Acompanhante";


            //String temp = "this.style.backgroundColor='white'; this.style.color='#333333'";
            //if ((e.Row.DataItemIndex % 2) == 0)
            //{
            //    temp = "this.style.backgroundColor='#EFF3FB'; this.style.color='#333333'";
            //}



            ////e.Row.Attributes.Add("style", "cursor:hand");
            //e.Row.Attributes.Add("onMouseOver", "this.style.backgroundColor='#409DD0'; this.style.color='white' ");
            //e.Row.Attributes.Add("onMouseOut", temp);

        }
    }
}
