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

using KrksaComponentes;
using KrksaComponentes.cllEventosClasses;
using cllEventos;
using CLLFuncoes;

using System.Data.SqlClient;

public partial class frmCONSADOutrasAtividades : System.Web.UI.Page
{
    Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    Pedido SessionPedido;
    PedidoCad oPedidoCad = new PedidoCad();

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

                //if (SessionCnn == null)
                SessionCnn = (SqlConnection)Session["SessionCnn"];
                //else
                Session["SessionCnn"] = SessionCnn;

                //if (SessionParticipante == null)
                SessionParticipante = (Participante)Session["SessionParticipante"];
                //else
                Session["SessionParticipante"] = SessionParticipante;


                //if (SessionEvento == null)
                SessionEvento = (Evento)Session["SessionEvento"];

                SessionCateg = (String)Session["SessionCateg"];
                if (SessionCateg == null)
                    SessionCateg = "";
                Session["SessionCateg"] = SessionCateg;

                //else
                Session["SessionEvento"] = SessionEvento;

                if (SessionEvento == null)
                    Server.Transfer("frmSessaoExpirada.aspx", true);

                SessionIdioma = (String)Session["SessionIdioma"];
                if ((SessionIdioma == null) || (SessionIdioma == ""))
                    SessionIdioma = "PTBR";
                Session["SessionIdioma"] = SessionIdioma;

                lblIdentificador.Text = SessionParticipante.CdParticipante.Trim();
                lblNoParticipante.Text = SessionParticipante.NoParticipante.Trim();
                lblCategoria.Text = SessionParticipante.Categoria.NoCategoria.Trim();

                if (SessionPedido == null)
                    SessionPedido = (Pedido)Session["SessionPedido"];
                else
                    Session["SessionPedido"] = SessionPedido;

                if (SessionPedido != null)
                {
                    if ((SessionPedido.TpPagamento.Trim() != "") && (!SessionEvento.FlPermitirEditarPedido))
                    {
                        Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                        "015",
                                        ""), true);
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

                }
                
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

            SessionPedido = (Pedido)Session["SessionPedido"];

            if (SessionEvento == null)
                Server.Transfer("frmSessaoExpirada.aspx", true);
        }
        try
        {
            verificarIdioma(SessionIdioma);

            TSManager1.RegisterPostBackControl(btnAvancar);
            TSManager1.RegisterPostBackControl(btnVoltarParaItens);
        }
        catch
        {
            //txtMsg.Text = "erro";
        }
    }
    

    protected void verificarIdioma(string prmIdioma)
    {
        //DropDownList txtidioma = (DropDownList)Page.Master.FindControl("txtIdioma");
        // if (txtidioma != null)
        //{

        if ((prmIdioma == null) || (prmIdioma == ""))
        {
            prmIdioma = "PTBR";
            SessionIdioma = "PTBR";
        }
        Session["SessionIdioma"] = SessionIdioma;

        if (prmIdioma == "PTBR")//(txtidioma.SelectedIndex == 0)
        {
            SessionIdioma = "PTBR";
            Session["SessionIdioma"] = SessionIdioma;


            
        }
        else if (prmIdioma == "ENUS")//(txtidioma.SelectedIndex == 1)
        {
            SessionIdioma = "ENUS";
            Session["SessionIdioma"] = SessionIdioma;

            
        }
        else if (prmIdioma == "ESP")//(txtidioma.SelectedIndex == 2)
        {
            SessionIdioma = "ESP";
            Session["SessionIdioma"] = SessionIdioma;

           
        }
        else if (prmIdioma == "FRA")//(txtidioma.SelectedIndex == 3)
        {
            SessionIdioma = "FRA";
            Session["SessionIdioma"] = SessionIdioma;

            
        }
        // }
    }
    protected void btnAvancar_Click1(object sender, EventArgs e)
    {

        if (RadioButtonList1.SelectedIndex < 0)
        {
            lblMsg.Text = "Selecione uma Mesa-redonda";
            return;
        }

        if (!oPedidoCad.GravarAtividadePedido(
                            SessionPedido.CdPedido,
                            RadioButtonList1.SelectedValue.ToString(),
                            0,
                            0,
                            1,
                            SessionCnn))
        {
            Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                    "012",
                    ""), true);
            return;
        }

        Server.Transfer(string.Format("frm_formapagamento.aspx?cdMatricula={0}",
                                                        SessionParticipante.CdParticipante), true);
    }
    protected void btnVoltarParaItens_Click(object sender, EventArgs e)
    {
        Server.Transfer(string.Format("frmSelAtividades.aspx?cdMatricula={0}",
                                                        SessionParticipante.CdParticipante), true);
    }
}
