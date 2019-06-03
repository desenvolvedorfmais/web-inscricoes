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

using Itaucripto;

using System.Data.SqlClient;

using System.IO;

using AjaxControlToolkit;

public partial class frmPagamentoItau : System.Web.UI.Page
{
    Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    SqlConnection SessionCnn;

    Pedido SessionPedido;
    PedidoCad oPedidoCad = new PedidoCad();



   // string dados;

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
                Session["SessionParticipante"] = SessionEvento;

            if (SessionEvento == null)
            {
                //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                //                "003",
                //                oEventoCad.RcMsg), true);

                Server.Transfer("frmSessaoExpirada.aspx", true);
            }

            if (SessionPedido == null)
                SessionPedido = (Pedido)Session["SessionPedido"];
            else
                Session["SessionPedido"] = SessionPedido;

            string caminhoImagem = "img/bg_topo.jpg";
            if (SessionEvento != null)
                caminhoImagem = "img/top_" + SessionEvento.CdEvento + ".jpg";

            img_Topo.ImageUrl = caminhoImagem;


            if (SessionEvento != null)
            {
                imgRealizacao.ImageUrl = "img/realizacao_" + SessionEvento.CdEvento + ".jpg";


                if (SessionEvento.FlMostrarRealizador)
                {
                    imgRealizacao.Visible = true;
                    lblRealizacao.Visible = true;
                }
                else
                {

                    imgRealizacao.Visible = false;
                    lblRealizacao.Visible = false;

                }

                if (SessionEvento.DsCSSEstiloWeb.Trim() != "")
                {
                    //Atribuindo o head (cabeçalho da nossa página)
                    HtmlHead head = this.Page.Header;

                    //Instanciando um HTMLLink com o nome de link
                    HtmlLink link = new HtmlLink();

                    //Adicionando os atributos
                    link.Attributes.Add("type", "text/css");
                    link.Attributes.Add("rel", "stylesheet");
                    link.Attributes.Add("href", "css/" + SessionEvento.DsCSSEstiloWeb + ".css");

                    //Adicionando o link ao head (cabeçalho)
                    head.Controls.Add(link);
                }
                string sURL = Request.Url.ToString().ToLower();
                //if ((sURL.Contains("index.aspx")) || (!SessionEvento.FlEventoComRecebimentos) || (SessionParticipante.CdParticipante == ""))
                //{
                //    Menu1.Visible = false;
                //}
                //else
                //{

                //    Menu1.Visible = true;
                //}

                EventoCad oEventoCad = new EventoCad();
                DataTable oDTApoio = oEventoCad.ListarApoioAtivos(SessionEvento.CdEvento, SessionCnn);

                if ((oDTApoio != null) && (oDTApoio.Rows.Count > 0))
                {

                    if (!Page.ClientScript.IsStartupScriptRegistered("alert"))
                    {

                        ClientScriptManager cs = Page.ClientScript;
                        for (int i = 0; i < oDTApoio.Rows.Count; i++)
                        {
                            cs.RegisterArrayDeclaration("imgs", "'" + oDTApoio.DefaultView[i]["dsCaminhoImagem"].ToString() + "'");
                        }


                        Page.ClientScript.RegisterStartupScript
                            (this.GetType(), "alert", "preloadImgs();", true);

                        Page.ClientScript.RegisterStartupScript
                            (this.GetType(), "alert2", "randomImages();", true);
                    }
                }

            }


            


            //string pedido = SessionPedido.CdPedido.Substring(10,8);

            //string vlTotalPedido = SessionPedido.VlTotalPedido.ToString("N2");
            //string dtVenc = DateTime.Parse(SessionPedido.DtVencimentoPedido.ToString()).ToString("ddMMyyyy");

            //string codEmp = "J0293638680001380000013107";
            //string chave = "4BRAS3LN4CI0N4L0";

            //cripto cript = new cripto(); //Server.CreateObject("Itaucripto.cripto");

            //dados = cript.geraDados(codEmp, pedido, vlTotalPedido, "", chave, SessionParticipante.NoParticipante, "01", SessionParticipante.NuCPFCNPJ, SessionParticipante.DsEndereco, SessionParticipante.NoBairro, SessionParticipante.NuCEP, SessionParticipante.NoCidade, SessionParticipante.DsUF, dtVenc, "", "", "", "");
            //DC.Text = dados;
        }
        else
        {
            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            SessionParticipante = (Participante)Session["SessionParticipante"];

            if (SessionEvento == null)
                Server.Transfer("frmSessaoExpirada.aspx", true);
        }


    }
}
