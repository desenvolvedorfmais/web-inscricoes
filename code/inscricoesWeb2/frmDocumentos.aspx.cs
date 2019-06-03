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

public partial class frmDocumentos : System.Web.UI.Page
{
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();    

    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    SqlConnection SessionCnn;

    //String SessionIdioma;

    //String SessionCateg;

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

                

                //if (SessionEvento == null)
                    SessionEvento = (Evento)Session["SessionEvento"];

                Session["SessionEvento"] = SessionEvento;

                if (SessionEvento == null)
                    Server.Transfer("frmSessaoExpirada.aspx", true);

                //SessionIdioma = (String)Session["SessionIdioma"];
                //if ((SessionIdioma == null) || (SessionIdioma == ""))
                //    SessionIdioma = "PTBR";
                //Session["SessionIdioma"] = SessionIdioma;


                lblFiltro.Focus();
                
                
            }
            catch
            {
                //txtMsg.Text = "erro";
            }
        }
        else
        {
            //SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            //SessionIdioma = (String)Session["SessionIdioma"];

            //SessionCateg = (String)Session["SessionCateg"];

            //SessionPedido = (Pedido)Session["SessionPedido"];

            //if (SessionEvento == null)
            //    Server.Transfer("frmSessaoExpirada.aspx", true);
        }
        //try
        //{
        //    verificarIdioma(SessionIdioma);

        //    TSManager1.RegisterPostBackControl(btnAvancar);
        //    TSManager1.RegisterPostBackControl(btnVoltarParaItens);
        //}
        //catch
        //{
        //    //txtMsg.Text = "erro";
        //}

        DocumentoCad oDocumentoCad = new DocumentoCad();
        DataList1.DataSource = oDocumentoCad.ListarAtivos(SessionEvento.CdEvento, SessionCnn);
        DataList1.DataBind();

    }
    

    protected void verificarIdioma(string prmIdioma)
    {
        //DropDownList txtidioma = (DropDownList)Page.Master.FindControl("txtIdioma");
        // if (txtidioma != null)
        //{

        //if ((prmIdioma == null) || (prmIdioma == ""))
        //{
        //    prmIdioma = "PTBR";
        //    SessionIdioma = "PTBR";
        //}
        //Session["SessionIdioma"] = SessionIdioma;

        //if (prmIdioma == "PTBR")//(txtidioma.SelectedIndex == 0)
        //{
        //    SessionIdioma = "PTBR";
        //    Session["SessionIdioma"] = SessionIdioma;


            
        //}
        //else if (prmIdioma == "ENUS")//(txtidioma.SelectedIndex == 1)
        //{
        //    SessionIdioma = "ENUS";
        //    Session["SessionIdioma"] = SessionIdioma;

            
        //}
        //else if (prmIdioma == "ESP")//(txtidioma.SelectedIndex == 2)
        //{
        //    SessionIdioma = "ESP";
        //    Session["SessionIdioma"] = SessionIdioma;

           
        //}
        //else if (prmIdioma == "FRA")//(txtidioma.SelectedIndex == 3)
        //{
        //    SessionIdioma = "FRA";
        //    Session["SessionIdioma"] = SessionIdioma;

            
        //}
        //// }
    }
    protected void btnAvancar_Click1(object sender, EventArgs e)
    {

        //if (RadioButtonList1.SelectedIndex < 0)
        //{
        //    lblMsg.Text = "Selecione uma Mesa-redonda";
        //    return;
        //}

        //if (!oPedidoCad.GravarAtividadePedido(
        //                    SessionPedido.CdPedido,
        //                    RadioButtonList1.SelectedValue.ToString(),
        //                    0,
        //                    0,
        //                    1,
        //                    SessionCnn))
        //{
        //    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
        //            "012",
        //            ""), true);
        //    return;
        //}

        //Server.Transfer(string.Format("frm_formapagamento.aspx?cdMatricula={0}",
        //                                                SessionParticipante.CdParticipante), true);
    }
    protected void btnVoltarParaItens_Click(object sender, EventArgs e)
    {
        //Server.Transfer(string.Format("frmSelAtividades.aspx?cdMatricula={0}",
        //                                                SessionParticipante.CdParticipante), true);
    }


    protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HyperLink hyplnk = (HyperLink)e.Item.FindControl("HyperLink11");
        }
    }
    
}
