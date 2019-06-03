using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Drawing;

using KrksaComponentes;
using KrksaComponentes.cllEventosClasses;
using cllEventos;
using CLLFuncoes;

using System.Data.SqlClient;

using AjaxControlToolkit;

public partial class frmEscolherAcao : System.Web.UI.Page
{

    Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();

    CategoriaCad oCategoriaCad = new CategoriaCad();

    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    SqlConnection SessionCnn;

    

    String SessionIdioma;

    String SessionCateg;

    String SessionAtv;

    String SessionTipoAcesso;

    String SessionChaveLibercao;
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
                Session["SessionEvento"] = SessionEvento;

            if (SessionEvento == null)
            {
                //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                //                "003",
                //                oEventoCad.RcMsg), true);

                Server.Transfer("frmSessaoExpirada.aspx", true);
            }

            SessionAtv = (String)Session["SessionAtv"];
            if (SessionAtv == null)
                SessionAtv = "";
            Session["SessionAtv"] = SessionAtv;



            if ((Request["tpAcesso"] != null) &&
                    (Request["tpAcesso"].ToString().Trim().ToUpper() != ""))
            {
                SessionTipoAcesso = Request["tpAcesso"];
            }
            else
            {
                SessionTipoAcesso = (String)Session["tpAcesso"];
                if (SessionTipoAcesso == null)
                    SessionTipoAcesso = "NRM";
            }
            Session["tpAcesso"] = SessionTipoAcesso;


            if ((SessionParticipante != null) && (SessionParticipante.FlConfirmacaoInscricao))
                btnAtividades.Visible = false;

        }
        else
        {
            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            SessionIdioma = (String)Session["SessionIdioma"];

            if (SessionCateg == null)
                SessionCateg = (String)Session["SessionCateg"];
            else
                Session["SessionCateg"] = SessionCateg;

            SessionChaveLibercao = (String)Session["SessionChaveLibercao"];

            SessionAtv = (String)Session["SessionAtv"];

            SessionTipoAcesso = (String)Session["tpAcesso"];

            
        }

        if (SessionEvento == null)
            Server.Transfer("frmSessaoExpirada.aspx", true);

        verificarIdioma(SessionIdioma);

        ToolkitScriptManager1.RegisterPostBackControl(btnAtividades);
        ToolkitScriptManager1.RegisterPostBackControl(btnTrabalhos);
    }

    protected void verificarIdioma(string prmIdioma)
    {
        if (prmIdioma == "PTBR")
        {
            lblInstucoes.Text = "Clique na opção desejada:";
            btnAtividades.Text = "Gerar Pagamento";
            btnTrabalhos.Text = "Cadastrar Trabalhos";
        }
        else if (prmIdioma == "ENUS")
        {
            lblInstucoes.Text = "Click on the chosen option:";
            btnAtividades.Text = "Generate Payment";
            btnTrabalhos.Text = "Submit Paper";
        }
        else if (prmIdioma == "ESP")
        {
            lblInstucoes.Text = "Enlace en la opción que desea:";
            btnAtividades.Text = "Hacer el Pago";
            btnTrabalhos.Text = "Registrar Trabajos";
        }
    }
}