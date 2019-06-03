using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

using KrksaComponentes;
using KrksaComponentes.cllEventosClasses;
using cllEventos;
using CLLFuncoes;

using System.Data.SqlClient;

using AjaxControlToolkit;

using MSXML2;

using System.Xml;


public partial class frmPesquisaAvulsaAbertura : System.Web.UI.Page
{
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    SqlConnection SessionCnn;

    String SessionTipoSistema;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (SessionCnn == null)
                SessionCnn = (SqlConnection)Session["SessionCnn"];
            else
                Session["SessionCnn"] = SessionCnn;

            if (SessionEvento == null)
                SessionEvento = (Evento)Session["SessionEvento"];
            else
                Session["SessionEvento"] = SessionEvento;

            //SessionIdioma = (String)Session["SessionIdioma"];
            //if (SessionIdioma == null)
            //    SessionIdioma = "PTBR";
            //Session["SessionIdioma"] = SessionIdioma;

            SessionTipoSistema = (String)Session["SessionTipoSistema"];
            if (SessionTipoSistema == null)
                SessionTipoSistema = "NRM";

            Session["SessionTipoSistema"] = SessionTipoSistema;

            if (SessionEvento == null)
            {
                //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                //                "003",
                //                oEventoCad.RcMsg), true);

                Server.Transfer("frmSessaoExpirada.aspx", true);
            }


            lblTitulo.Text = SessionEvento.NoEvento;

            if (SessionEvento.CdEvento == "013101")
            {
                lblTitulo.Text = "O que você sabe de sustentabilidade corporativa?";
                lblChamada1.Text = "Responda um pequeno quizz de certo ou errado e concorra a um prêmio!";
                lblChamada2.Text = "";
            }
                       
        }
        else
        {
            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            SessionTipoSistema = (String)Session["SessionTipoSistema"];

            //SessionIdioma = (String)Session["SessionIdioma"];

            
        }

        if (SessionEvento == null)
            Server.Transfer("frmSessaoExpirada.aspx", true);



        ToolkitScriptManager1.RegisterPostBackControl(btnQueroParticiparPesquisa);
    }
    protected void btnContinuar_Click(object sender, EventArgs e)
    {

        if (SessionEvento == null)
            Server.Transfer("frmSessaoExpirada.aspx", true);

        Session["oDTPesquisa"] = null;
        Response.Write("<script>window.open('frmPesquisaAvulsa.aspx','_self');</script>");
    }
}