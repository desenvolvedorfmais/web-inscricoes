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

public partial class frmAtividadesPedido : System.Web.UI.Page
{
    Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    SqlConnection SessionCnn;

    Pedido SessionPedido;
    PedidoCad oPedidoCad = new PedidoCad();

    Categoria SessionCategoria;

    Inscricoes SessionInscricoes = new Inscricoes();

    //ClsCongresso clOperador;

    DataTable oDTAtividadesParticipante = new DataTable();

    string cdAtiv = "";

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
                Session["SessionParticipante"] = SessionEvento;

            if (SessionEvento == null)
            {
                //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                //                "003",
                //                oEventoCad.RcMsg), true);

                Server.Transfer("frmSessaoExpirada.aspx", true);
            }

            lblIdentificador.Text = SessionParticipante.CdParticipante.Trim();
            lblNoParticipante.Text = SessionParticipante.NoParticipante.Trim();

            CategoriaCad oCategoriaCad = new CategoriaCad();
            SessionCategoria = oCategoriaCad.Pesquisar(SessionEvento.CdEvento, SessionParticipante.CdCategoria, SessionCnn);

            Session["SessionCategoria"] = SessionCategoria;
            if (SessionIdioma == "PTBR")
            {
                lblCategoria.Text = SessionCategoria.NoCategoria.Trim();
                if (SessionEvento.CdEvento == "005503")
                    lblCategoria.Text += " / " + SessionParticipante.NoInstituicao;
            }
            else if (SessionIdioma == "ENUS")
                lblCategoria.Text = SessionCategoria.NoCategoriaIngles.Trim();
            else if (SessionIdioma == "ESP")
                lblCategoria.Text = SessionCategoria.NoCategoriaEspanhol.Trim();
            else if (SessionIdioma == "FRA")
                lblCategoria.Text = SessionCategoria.NoCategoriaFrances.Trim();

            btnVerDadosRecibo.Visible = ((SessionEvento.FlEmiteRecibo) && (SessionIdioma == "PTBR"));

            lblNrPedido.Visible = lblResPed.Visible = /*pnlResumo.Visible = */SessionEvento.FlEventoComRecebimentos;
            
            lblNrPedido.Text = "";
            if ((Request["p"] != null) &&
                (Request["p"] != ""))
            {
                //string cd_pedido = cllEventos.Crypto.DecryptStringAES(Request["p"].ToString(),"3");
                string cd_pedido = cllEventos.Crypto.DecryptStringAES(Request["p"].ToString());
                lblNrPedido.Text = SessionEvento.CdEvento + SessionParticipante.CdParticipante + cd_pedido;
            }
            if (lblNrPedido.Text == "")
                SessionPedido = oPedidoCad.VerificaPedidoNaoPagoWeb(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);
            else
                SessionPedido = oPedidoCad.Pesquisar(SessionEvento.CdEvento, SessionParticipante.CdParticipante, lblNrPedido.Text, SessionCnn);

            if (SessionPedido != null)
            {
                Session["SessionPedido"] = SessionPedido;
                lblNrPedido.Text = SessionPedido.CdPedido;
            }


            oDTAtividadesParticipante.Columns.Add("noTipoAtividade");
            oDTAtividadesParticipante.Columns.Add("noTitulo");
            oDTAtividadesParticipante.Columns.Add("dsTema");
            oDTAtividadesParticipante.Columns.Add("noLocal");
            oDTAtividadesParticipante.Columns.Add("dtIni", System.Type.GetType("System.DateTime"));
            oDTAtividadesParticipante.Columns.Add("dtTermino", System.Type.GetType("System.DateTime"));
            oDTAtividadesParticipante.Columns.Add("cdAtividade");
            oDTAtividadesParticipante.Columns.Add("noProfessor");
            oDTAtividadesParticipante.Columns.Add("cdTipoAtividade");
            oDTAtividadesParticipante.Columns.Add("vlAtividade", System.Type.GetType("System.Decimal"));
            oDTAtividadesParticipante.Columns.Add("flAtivo", System.Type.GetType("System.Boolean"));
            oDTAtividadesParticipante.Columns.Add("flUsado", System.Type.GetType("System.Boolean"));
            oDTAtividadesParticipante.Columns.Add("dtMatricula", System.Type.GetType("System.DateTime"));
            oDTAtividadesParticipante.Columns.Add("vlDesconto", System.Type.GetType("System.Decimal"));
            oDTAtividadesParticipante.Columns.Add("vlMatricula", System.Type.GetType("System.Decimal"));
            oDTAtividadesParticipante.Columns.Add("flInscricaoObrigatoria", System.Type.GetType("System.Boolean"));
            oDTAtividadesParticipante.Columns.Add("dtValidade");
            oDTAtividadesParticipante.Columns.Add("flPodeChocarHorario", System.Type.GetType("System.Boolean"));
            oDTAtividadesParticipante.Columns.Add("dsCaminhoImgWEB");
            oDTAtividadesParticipante.Columns.Add("vlQuantidade");
            oDTAtividadesParticipante.Columns.Add("flRequerQuantidade", System.Type.GetType("System.Boolean"));
            oDTAtividadesParticipante.Columns.Add("nrLinha");
            oDTAtividadesParticipante.Columns.Add("vlQuantidadeMaxima");
            oDTAtividadesParticipante.Columns.Add("flPodeRepetirPedido");
            oDTAtividadesParticipante.Columns.Add("dsTurno");


            Session["cdAtiv"] = "";

            

            CarregarAtividadesParticipanteGrade();
            Session["oDTAtividadesParticipante"] = oDTAtividadesParticipante;

            if (SessionEvento.CdEvento == "003401")
            {
                
                lblResVlr.Visible = false;
                lblResDesc.Visible = false;
                lblResVlrTotal.Visible = false;

                vlTotalAtiv.Visible = false;
                vlTotalDesc.Visible = false;
                vlTotalPedido.Visible = false;
            }

            btnVoltar.Focus();
        }
        else
        {
            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            SessionCategoria = (Categoria)Session["SessionCategoria"];

            SessionPedido = (Pedido)Session["SessionPedido"];

            oDTAtividadesParticipante = (DataTable)Session["oDTAtividadesParticipante"];

            cdAtiv = (string)Session["cdAtiv"];

            SessionIdioma = (String)Session["SessionIdioma"];

        }

        if (SessionEvento == null)
            Server.Transfer("frmSessaoExpirada.aspx", true);

        verificarIdioma(SessionIdioma);

        this.grdAtvParticipante.Attributes.Remove("border-collapse");
    }

    protected void verificarIdioma(string prmIdioma)
    {
        if (prmIdioma == "PTBR")
        {
            SessionIdioma = "PTBR";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Atividades/Cursos do Pedido";
            lblTituloResumo.Text = "Resumo do Pedido";


            lblPart.Text = "Participante:";
            lblCateg.Text = "Categoria:";

            lblResPed.Text = "Pedido nº";
            lblResItens.Text = "Itens";
            lblResVlr.Text = "Valor";
            lblResDesc.Text = "Desconto";
            lblResVlrTotal.Text = "Total";

            lblResVlrTotal.Text = "Total (R$)";
            if ((SessionParticipante.NoPais == "BRASIL") ||
                (SessionParticipante.NoPais == "BRAZIL") ||
                (SessionParticipante.NoPais == "BRASIL") ||
                (SessionParticipante.NoPais == "BRÉSIL"))
                lblResVlrTotal.Text = "Total (R$)";
            else
                lblResVlrTotal.Text = "Total ($)";

            btnVoltar.Text = "Voltar";

            lblTituloGrid1.Text = "Iten(s) Solicitado(s)";
        }
        else if (prmIdioma == "ENUS")
        {
            SessionIdioma = "ENUS";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Activities / Courses of the Order";
            lblTituloResumo.Text = "Order summary";

            lblID.Text = "Registration no.";
            lblPart.Text = "Participant:";
            lblCateg.Text = "Category:";

            lblResPed.Text = "Order No.";
            lblResItens.Text = "Items";
            lblResVlr.Text = "Value";
            lblResDesc.Text = "Discount";
            lblResVlrTotal.Text = "Total";
            lblResVlrTotal.Text = "Total ($)";
            if ((SessionParticipante.NoPais == "BRASIL") ||
                (SessionParticipante.NoPais == "BRAZIL") ||
                (SessionParticipante.NoPais == "BRASIL") ||
                (SessionParticipante.NoPais == "BRÉSIL"))
                lblResVlrTotal.Text = "Total (R$)";
            else
                lblResVlrTotal.Text = "Total ($)";

            if (SessionParticipante.Categoria.NoCategoria.Contains("BRAZILIAN"))
                lblResVlrTotal.Text = "Total (R$)";

            btnVoltar.Text = "Back";

            lblTituloGrid1.Text = "Items Request";
        }
        else if (prmIdioma == "ESP")
        {
            SessionIdioma = "ESP";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Actividades y Cursos de la Solicitud";
            lblTituloResumo.Text = "Resumen de la solicitud";

            lblPart.Text = "Participante:";
            lblCateg.Text = "Categoría";

            lblResPed.Text = "Solicitud Nº";
            lblResItens.Text = "Artículos";
            lblResVlr.Text = "Valor";
            lblResDesc.Text = "Descuento";
            lblResVlrTotal.Text = "Total";
            lblResVlrTotal.Text = "Total ($)";
            if ((SessionParticipante.NoPais == "BRASIL") ||
                (SessionParticipante.NoPais == "BRAZIL") ||
                (SessionParticipante.NoPais == "BRASIL") ||
                (SessionParticipante.NoPais == "BRÉSIL"))
                lblResVlrTotal.Text = "Total (R$)";
            else
                lblResVlrTotal.Text = "Total ($)";

            btnVoltar.Text = "Volver";

            lblTituloGrid1.Text = "Artículos";
        }
        else if (prmIdioma == "FRA")
        {
            SessionIdioma = "FRA";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Activités et Cours Demande";
            lblTituloResumo.Text = "Résumé de la demande";

            lblPart.Text = "Participant:";
            lblCateg.Text = "Catégorie:";

            lblResPed.Text = "Demande no";
            lblResItens.Text = "Articles";
            lblResVlr.Text = "Valeur";
            lblResDesc.Text = "Réduction";
            lblResVlrTotal.Text = "Total";

            btnVoltar.Text = "Retour";

            lblTituloGrid1.Text = "Articles";
        }
    }
       
    private void CarregarAtividadesParticipanteGrade()
    {

        
        DataTable DTAtividadesp;
        if ((!SessionEvento.FlEventoComRecebimentos) && (!SessionCategoria.FlConfirmacaoCadWeb))
            DTAtividadesp = SessionInscricoes.ListarAtividadesDoParticipante(SessionParticipante, SessionCnn);
        else
            DTAtividadesp = SessionInscricoes.ListarAtividadesDoPedido(SessionParticipante, lblNrPedido.Text, SessionCnn);

        //cdAtiv = "";

        if ((DTAtividadesp != null) && (DTAtividadesp.Rows.Count > 0))
        {
            oDTAtividadesParticipante.Rows.Clear();
            
            vlTotalAtiv.Text = "0,00";
            vlTotalDesc.Text = "0,00";
            vlTotalPedido.Text = "0,00";


            //vlItens.Text = "0"; ;

            for (int i = 0; i < DTAtividadesp.DefaultView.Count; i++)
            {

                if (cdAtiv.Trim() == "")
                    cdAtiv = DTAtividadesp.DefaultView[i]["cdAtividade"].ToString().Trim();
                else
                    cdAtiv = cdAtiv + "," + DTAtividadesp.DefaultView[i]["cdAtividade"].ToString().Trim();


                oDTAtividadesParticipante.Rows.Add(
                                    DTAtividadesp.DefaultView[i]["noTipoAtividade"].ToString().Trim(),
                                    DTAtividadesp.DefaultView[i]["noTitulo"].ToString().Trim(),
                                    DTAtividadesp.DefaultView[i]["dsTema"].ToString().Trim(),
                                    DTAtividadesp.DefaultView[i]["noLocal"].ToString().Trim(),
                                    DateTime.Parse(DTAtividadesp.DefaultView[i]["dtIni"].ToString().Trim()).ToString("dd/MM/yyyy HH:mm"),
                                    DateTime.Parse(DTAtividadesp.DefaultView[i]["dtTermino"].ToString().Trim()).ToString("dd/MM/yyyy HH:mm"),
                                    DTAtividadesp.DefaultView[i]["cdAtividade"].ToString().Trim(),
                                    DTAtividadesp.DefaultView[i]["noProfessor"].ToString().Trim(),
                                    DTAtividadesp.DefaultView[i]["cdTipoAtividade"].ToString().Trim(),
                                    decimal.Parse(DTAtividadesp.DefaultView[i]["vlAtividade"].ToString().Trim()),
                                    Boolean.Parse(DTAtividadesp.DefaultView[i]["flAtivo"].ToString().Trim()),
                                    Boolean.Parse(DTAtividadesp.DefaultView[i]["flUsado"].ToString().Trim()),
                                    DateTime.Parse(DTAtividadesp.DefaultView[i]["dtMatricula"].ToString().Trim()).ToString("dd/MM/yyyy"),
                                    decimal.Parse(DTAtividadesp.DefaultView[i]["vlDesconto"].ToString().Trim()),
                                    decimal.Parse(DTAtividadesp.DefaultView[i]["vlMatricula"].ToString().Trim()),
                                    Boolean.Parse(DTAtividadesp.DefaultView[i]["flInscricaoObrigatoria"].ToString().Trim()),
                                    DTAtividadesp.DefaultView[i]["dtValidade"].ToString().Trim(),
                                    Boolean.Parse(DTAtividadesp.DefaultView[i]["flPodeChocarHorario"].ToString().Trim()),
                                    DTAtividadesp.DefaultView[i]["dsCaminhoImgWEB"].ToString(),
                                    DTAtividadesp.DefaultView[i]["vlQuantidade"].ToString().Trim(),
                                    Boolean.Parse(DTAtividadesp.DefaultView[i]["flRequerQuantidade"].ToString().Trim()),
                                    i.ToString(),
                                    DTAtividadesp.DefaultView[i]["vlQuantidadeMaxima"].ToString().Trim(),
                                    DTAtividadesp.DefaultView[i]["flPodeRepetirPedido"].ToString().Trim(),
                                    DTAtividadesp.DefaultView[i]["dsTurno"].ToString().Trim());

                vlTotalAtiv.Text = (decimal.Parse(vlTotalAtiv.Text) + (decimal.Parse(DTAtividadesp.DefaultView[i]["vlAtividade"].ToString().Trim()) *
                                                                       int.Parse(DTAtividadesp.DefaultView[i]["vlQuantidade"].ToString()))
                                   ).ToString("N2");
                vlTotalDesc.Text = (decimal.Parse(vlTotalDesc.Text) + (decimal.Parse(DTAtividadesp.DefaultView[i]["vlDesconto"].ToString().Trim()) *
                                                                       int.Parse(DTAtividadesp.DefaultView[i]["vlQuantidade"].ToString()))
                                   ).ToString("N2");
                vlTotalPedido.Text = (decimal.Parse(vlTotalPedido.Text) + decimal.Parse(DTAtividadesp.DefaultView[i]["vlMatricula"].ToString().Trim())).ToString("N2");


                //vlTotalAtiv.Text = (decimal.Parse(vlTotalAtiv.Text) + decimal.Parse(DTAtividadesp.DefaultView[i]["vlAtividade"].ToString().Trim())).ToString("N2");
                //vlTotalDesc.Text = (decimal.Parse(vlTotalDesc.Text) + decimal.Parse(DTAtividadesp.DefaultView[i]["vlDesconto"].ToString().Trim())).ToString("N2");
                //vlTotalPedido.Text = (decimal.Parse(vlTotalPedido.Text) + decimal.Parse(DTAtividadesp.DefaultView[i]["vlMatricula"].ToString().Trim())).ToString("N2");
            }
        }



        Session["oDTAtividadesParticipante"] = oDTAtividadesParticipante;

        grdAtvParticipante.DataSource = oDTAtividadesParticipante.DefaultView;
        grdAtvParticipante.DataBind();

        vlItens.Text = grdAtvParticipante.Rows.Count.ToString();

        Session["cdAtiv"] = cdAtiv;

    }

    
    protected void prpCalcularDescontosPorTipoAtividade()
    {
        if (!SessionEvento.FlEventoComRecebimentos)
            return;
        
        DescontoCad oDescontoCad = new DescontoCad();

        DataTable oDTDescontos = oDescontoCad.ListarDescontosPorTipoAtividade(SessionParticipante.CdEvento, "", SessionParticipante.CdCategoria, null, SessionCnn);


        if ((oDTDescontos == null) || (oDTDescontos.Rows.Count < 1))
            return;

        DataTable tmpDT = null;
        //DataRow[] dr;
        for (int j = 0; j < oDTDescontos.Rows.Count; j++ )
        {
            tmpDT = oDTAtividadesParticipante.Clone();
            string tmpFiltro = " cdTipoAtividade = '"+oDTDescontos.DefaultView[j]["cdTipoAtividade"].ToString() + "'";
            DataRow[] dr = oDTAtividadesParticipante.Select(tmpFiltro);// + oDTDescontos.DefaultView[j]["cdTipoAtividade"].ToString() + "'");
            foreach (DataRow drSimples in dr)
            {
                tmpDT.ImportRow(drSimples);
            }
            if (tmpDT.Rows.Count >= int.Parse(oDTDescontos.DefaultView[j]["vlQuantidade"].ToString()))
            {
                vlTotalAtiv.Text = "0,00";
                vlTotalDesc.Text = "0,00";
                vlTotalPedido.Text = "0,00";

                for (int i = 0; i < oDTAtividadesParticipante.Rows.Count; i++)
                {
                    if (oDTAtividadesParticipante.DefaultView[i]["cdTipoAtividade"].ToString() == oDTDescontos.DefaultView[j]["cdTipoAtividade"].ToString())
                    {
                        oDTAtividadesParticipante.DefaultView[i]["vlDesconto"] = oDTDescontos.DefaultView[j]["vlDescontoReal"].ToString();
                        oDTAtividadesParticipante.DefaultView[i]["vlMatricula"] = (decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlAtividade"].ToString()) - decimal.Parse(oDTDescontos.DefaultView[j]["vlDescontoReal"].ToString())).ToString("N2");
                        oDTAtividadesParticipante.DefaultView[i]["dtValidade"] = oDTDescontos.DefaultView[j]["dtValidade"].ToString();
                    }
                    vlTotalAtiv.Text = (decimal.Parse(vlTotalAtiv.Text) + decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlAtividade"].ToString().Trim())).ToString("N2");
                    vlTotalDesc.Text = (decimal.Parse(vlTotalDesc.Text) + decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlDesconto"].ToString().Trim())).ToString("N2");
                    vlTotalPedido.Text = (decimal.Parse(vlTotalPedido.Text) + decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlMatricula"].ToString().Trim())).ToString("N2");

                }
            }
            else
            {
                vlTotalAtiv.Text = "0,00";
                vlTotalDesc.Text = "0,00";
                vlTotalPedido.Text = "0,00";

                for (int i = 0; i < oDTAtividadesParticipante.Rows.Count; i++)
                {
                    if (oDTAtividadesParticipante.DefaultView[i]["cdTipoAtividade"].ToString() == oDTDescontos.DefaultView[j]["cdTipoAtividade"].ToString())
                    {
                        oDTAtividadesParticipante.DefaultView[i]["vlDesconto"] = oDTDescontos.DefaultView[j]["vlDescontoOutros"].ToString();
                        oDTAtividadesParticipante.DefaultView[i]["vlMatricula"] = oDTAtividadesParticipante.DefaultView[i]["vlAtividade"].ToString();
                        oDTAtividadesParticipante.DefaultView[i]["dtValidade"] = oDTDescontos.DefaultView[j]["dtValidadeOutros"].ToString();
                    }
                    vlTotalAtiv.Text = (decimal.Parse(vlTotalAtiv.Text) + decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlAtividade"].ToString().Trim())).ToString("N2");
                    vlTotalDesc.Text = (decimal.Parse(vlTotalDesc.Text) + decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlDesconto"].ToString().Trim())).ToString("N2");
                    vlTotalPedido.Text = (decimal.Parse(vlTotalPedido.Text) + decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlMatricula"].ToString().Trim())).ToString("N2");

                }
            }

        }

        grdAtvParticipante.DataSource = oDTAtividadesParticipante.DefaultView;
        grdAtvParticipante.DataBind();

        vlItens.Text = grdAtvParticipante.Rows.Count.ToString();
    }

    protected void grdAtvParticipante_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //if (grdAtvParticipante.Rows.Count <= 0)
        //    return;

        //lblMsg.Text = "";

        //if (Boolean.Parse(grdAtvParticipante.DataKeys[e.RowIndex].Values[15].ToString().Trim()))//inscricao obrigatoria
        //{
        //    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Item de inscrição obrigatória, não pode ser removido!'); </script>", false);
        //    return;
        //}

        //if ((!SessionEvento.FlEventoComRecebimentos) && (!SessionCategoria.FlConfirmacaoCadWeb))
        //{
           
        //    SessionInscricoes.ExcluirAtividade(SessionEvento.CdEvento, SessionParticipante.CdParticipante, grdAtvParticipante.DataKeys[e.RowIndex].Values[6].ToString(), SessionCnn);

        //    TxtFTipo.SelectedIndex = 0;
        //    //txtFTema.SelectedIndex = 0;
        //    txtFDtInicio.SelectedIndex = 0;

        //    CarregarAtividadesGrade();
        //    CarregarAtividadesParticipanteGrade();
        //}
        //else
        //{
        //    cdAtiv = cdAtiv.Replace("," + grdAtvParticipante.DataKeys[e.RowIndex].Values[6].ToString().Trim(), "");
        //    cdAtiv = cdAtiv.Replace(grdAtvParticipante.DataKeys[e.RowIndex].Values[6].ToString().Trim() + ",", "");
        //    cdAtiv = cdAtiv.Replace(grdAtvParticipante.DataKeys[e.RowIndex].Values[6].ToString().Trim(), "");

        //    Session["cdAtiv"] = cdAtiv;

        //    vlTotalAtiv.Text = (decimal.Parse(vlTotalAtiv.Text) - decimal.Parse(grdAtvParticipante.DataKeys[e.RowIndex].Values[9].ToString().Trim())).ToString("N2");
        //    vlTotalDesc.Text = (decimal.Parse(vlTotalDesc.Text) - decimal.Parse(grdAtvParticipante.DataKeys[e.RowIndex].Values[13].ToString().Trim())).ToString("N2");
        //    vlTotalPedido.Text = (decimal.Parse(vlTotalPedido.Text) - decimal.Parse(grdAtvParticipante.DataKeys[e.RowIndex].Values[14].ToString().Trim())).ToString("N2");

        //    oDTAtividadesParticipante.Rows.RemoveAt(e.RowIndex);

        //    grdAtvParticipante.DataSource = oDTAtividadesParticipante.DefaultView;
        //    grdAtvParticipante.DataBind();

        //    vlItens.Text = grdAtvParticipante.Rows.Count.ToString();

        //    prpFiltrarAtividades();
            
        //    prpCalcularDescontosPorTipoAtividade();
        //}
    }

    protected void grdAtvParticipante_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (!SessionEvento.FlEventoComRecebimentos)
            {
                ImageButton MyButton = (ImageButton)e.Row.FindControl("btnRemove");
                MyButton.Attributes.Add("onclick", "javascript:return " +
                "confirm('Confirma a exclusão do Item?')");
            }

            Label lbldtini = (Label)e.Row.FindControl("lblDtIni");
            lbldtini.Text = DateTime.Parse(lbldtini.Text).ToString("dd/MM/yyyy HH:mm");
            Label lbldttermino = (Label)e.Row.FindControl("lblDtTermino");
            lbldttermino.Text = DateTime.Parse(lbldttermino.Text).ToString("dd/MM/yyyy HH:mm");

            Label lbltpItem = (Label)e.Row.FindControl("lblTpItem");
            Label lbldeItem = (Label)e.Row.FindControl("lblDeItem");
            Label lblateItem = (Label)e.Row.FindControl("lblAteItem");
            Label lblvagasItem = (Label)e.Row.FindControl("lblVagasItem");
            Label lbllocalItem = (Label)e.Row.FindControl("lblLocalItem");

            Label lblvalorItem = (Label)e.Row.FindControl("lblValorItem");
            Label lbldescItem = (Label)e.Row.FindControl("lblDescItem");
            Label lblqtdItem = (Label)e.Row.FindControl("lblQtdItem");
            Label lblvlTotalItem = (Label)e.Row.FindControl("lblVlrTotalItem");

            Label lblLocal = (Label)e.Row.FindControl("lblLocal");
            Label lblVagas = (Label)e.Row.FindControl("lblVagas");

            if (SessionIdioma == "PTBR")
            {
                lbltpItem.Text = "Tipo: ";
                lbldeItem.Text = "De: ";
                lblateItem.Text = " a: ";
                lbllocalItem.Text = "Local: ";

                lblvalorItem.Text = "Valor ";
                lbldescItem.Text = "Desconto ";
                lblqtdItem.Text = "Quantidade ";
                lblvlTotalItem.Text = "Vlr Total ";
                lblvlTotalItem.Text = "Total (R$)";
                if ((SessionParticipante.NoPais == "BRASIL") ||
                    (SessionParticipante.NoPais == "BRAZIL") ||
                    (SessionParticipante.NoPais == "BRASIL") ||
                    (SessionParticipante.NoPais == "BRÉSIL"))
                    lblvlTotalItem.Text = "Total (R$)";
                else
                    lblvlTotalItem.Text = "Total ($)";
            }
            else if (SessionIdioma == "ENUS")
            {
                lbltpItem.Text = "Type: ";
                lbldeItem.Text = "From: ";
                lblateItem.Text = " to: ";
                lbllocalItem.Text = "Site: ";

                lblvalorItem.Text = "Price ";
                lbldescItem.Text = "Discount ";
                lblqtdItem.Text = "Amount ";
                lblvlTotalItem.Text = "Total ";
                lblvlTotalItem.Text = "Total ($)";
                if ((SessionParticipante.NoPais == "BRASIL") ||
                    (SessionParticipante.NoPais == "BRAZIL") ||
                    (SessionParticipante.NoPais == "BRASIL") ||
                    (SessionParticipante.NoPais == "BRÉSIL"))
                    lblvlTotalItem.Text = "Total (R$)";
                else
                    lblvlTotalItem.Text = "Total ($)";

                if (SessionParticipante.Categoria.NoCategoria.Contains("BRAZILIAN"))
                    lblvlTotalItem.Text = "Total (R$)";
            }
            else if (SessionIdioma == "ESP")
            {
                lbltpItem.Text = "Tipo: ";
                lbldeItem.Text = "De: ";
                lblateItem.Text = " a: ";
                lbllocalItem.Text = "Local: ";

                lblvalorItem.Text = "Precio ";
                lbldescItem.Text = "Descuento ";
                lblqtdItem.Text = "Cantidad ";
                lblvlTotalItem.Text = "Total ";
                lblvlTotalItem.Text = "Total ($)";
                if ((SessionParticipante.NoPais == "BRASIL") ||
                    (SessionParticipante.NoPais == "BRAZIL") ||
                    (SessionParticipante.NoPais == "BRASIL") ||
                    (SessionParticipante.NoPais == "BRÉSIL"))
                    lblvlTotalItem.Text = "Total (R$)";
                else
                    lblvlTotalItem.Text = "Total ($)";
            }
            else if (SessionIdioma == "FRA")
            {
                lbltpItem.Text = "Type: ";
                lbldeItem.Text = "De: ";
                lblateItem.Text = " à: ";
                lbllocalItem.Text = "Local: ";

                lblvalorItem.Text = "Price ";
                lbldescItem.Text = "Réduction ";
                lblqtdItem.Text = "Montant ";
                lblvlTotalItem.Text = "Total ";
            }

            if ((SessionEvento.CdCliente == "0003") || (SessionEvento.CdEvento == "003401"))
            {
                lbldtini.Visible = false;
                lbldttermino.Visible = false;
                lbldeItem.Visible = false;
                lblateItem.Visible = false;
                lbllocalItem.Visible = false;
                lblLocal.Visible = false;

            }

            if (SessionEvento.CdCliente == "0016") 
            {
                lbldtini.Visible = false;
                lbldttermino.Visible = false;
                lbldeItem.Visible = false;
                lblateItem.Visible = false;
                //lbllocalItem.Visible = false;
                //lblLocal.Visible = false;

            }

            Label lblatv = (Label)e.Row.FindControl("lblVlAtividade");
            Label lbldec = (Label)e.Row.FindControl("lblVlDescontoReal");
            lblatv.Font.Strikeout = (decimal.Parse(lbldec.Text) > 0);

            Image imgprofatv = (Image)e.Row.FindControl("imgAtivProf");
            imgprofatv.Visible = (imgprofatv.ImageUrl != "");

            if ((SessionEvento.CdCliente == "0013") || (SessionEvento.CdCliente == "0003") || (SessionEvento.CdEvento == "002902") || (SessionEvento.CdEvento == "003401"))
            {
                
                Panel pnlresumovlritens = (Panel)e.Row.FindControl("pnlResumoVlrItens");
                pnlresumovlritens.Visible = false;
            }
        }
        
    }
    protected void btnAvancar_Click(object sender, ImageClickEventArgs e)
    {

        //if (grdAtvParticipante.Rows.Count <= 0)
        //{
        //    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Nenhum item selecionado!'); </script>", false);
        //    return;
        //}
        //if ((SessionCategoria.FlConfirmacaoCadWeb) || (SessionEvento.FlEventoComRecebimentos))//pode entrar com conf e com receb/ sem conf e com receb / com conf e sem receb
        //{
        //    DateTime? dtVencPedido = SessionEvento.DtFechamentoInscrWeb;

        //    for (int i = 0; i < oDTAtividadesParticipante.Rows.Count; i++)
        //    {
        //        if (oDTAtividadesParticipante.DefaultView[i]["dtValidade"].ToString() != "")
        //        {
        //            if (dtVencPedido > DateTime.Parse(oDTAtividadesParticipante.DefaultView[i]["dtValidade"].ToString()))
        //                dtVencPedido = DateTime.Parse(oDTAtividadesParticipante.DefaultView[i]["dtValidade"].ToString());
        //        }
        //    }

        //    if ((SessionCategoria.FlConfirmacaoCadWeb) || (decimal.Parse(vlTotalPedido.Text) > 0))
        //    {
        //        SessionPedido = new Pedido(SessionEvento.CdEvento, SessionParticipante.CdParticipante, lblNrPedido.Text, "0", null, decimal.Parse(vlTotalPedido.Text), false, true, "", "", "", "", "", "", "", "", "", dtVencPedido, 1);

        //        SessionPedido = oPedidoCad.Gravar(SessionPedido, SessionCnn);
        //        if (SessionPedido == null)
        //        {
        //            Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
        //                       "012",
        //                       ""), true);
        //            return;
        //        }
        //        Session["SessionPedido"] = SessionPedido;
        //        if (oPedidoCad.ApagarTodasAtividadePedido(SessionPedido, SessionCnn))
        //        {

        //            for (int i = 0; i < oDTAtividadesParticipante.Rows.Count; i++)
        //            {
        //                if (!oPedidoCad.GravarAtividadePedido(
        //                        SessionPedido.CdPedido,
        //                        oDTAtividadesParticipante.DefaultView[i]["cdAtividade"].ToString(),
        //                        decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlAtividade"].ToString()),
        //                        decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlDesconto"].ToString()),
        //                        SessionCnn))
        //                {
        //                    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
        //                            "012",
        //                            ""), true);
        //                    return;
        //                }
        //            }
                    
        //            Session["SessionPedido"] = SessionPedido;
        //            Server.Transfer(string.Format("frm_formapagamento.aspx?cdMatricula={0}",
        //                                            SessionParticipante.CdParticipante), true);

        //        }
        //        else
        //        {
        //            Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
        //                   "012",
        //                   ""), true);
        //            return;
        //        }
        //    }
        //    else  
        //    {
        //        for (int i = 0; i < oDTAtividadesParticipante.Rows.Count; i++)
        //        {  
        //            //gerar matricula
        //            if (SessionInscricoes.MatriculasGravar(SessionParticipante.CdEvento, SessionParticipante.CdParticipante, oDTAtividadesParticipante.DefaultView[i]["cdAtividade"].ToString(), 0, SessionCnn))
        //            {
        //                //enviar email

        //                //-----
        //                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
        //                               "002",
        //                               ""), true);
        //            }
        //            else
        //            {
        //                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
        //                               "012",
        //                               ""), true);
        //                return;
        //            }
        //            //-----
                    
        //        }
        //            //enviar e-mail para participante
        //        Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
        //            "013",
        //            ""), true);
        //    }
            
        //}
        //else
        //{
        //    //enviar email

        //    //-----
        //    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
        //                   "002",
        //                   ""), true);
        //}
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Geral oGeral = new Geral();

        //oGeral.EnviarEmailPedidoBoleto(SessionEvento, SessionParticipante, SessionPedido, SessionCnn);
        oGeral.EnviarEmailConfirmaCadastro(SessionEvento, SessionParticipante, SessionCnn);
    }
}
