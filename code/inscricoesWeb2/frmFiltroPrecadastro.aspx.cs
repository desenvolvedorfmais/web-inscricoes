using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Data;
using System.Data.SqlClient;

using CLLFuncoes;
using cllEventos;

//using MSXML2;

using System.Xml;
//using Recaptcha;
using MSCaptcha;

public partial class frmFiltroPrecadastro : System.Web.UI.Page
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

            SessionCateg = (String)Session["SessionCateg"];
            if (SessionCateg == null)
                SessionCateg = "";
            Session["SessionCateg"] = SessionCateg;

            SessionAtv = (String)Session["SessionAtv"];
            if (SessionAtv == null)
                SessionAtv = "";
            Session["SessionAtv"] = SessionAtv;



            //verificarIdioma(SessionIdioma);

            if (SessionParticipante == null)
                SessionParticipante = (Participante)Session["SessionParticipante"];
            else
                Session["SessionParticipante"] = SessionParticipante;


            if (SessionEvento == null)
                SessionEvento = (Evento)Session["SessionEvento"];
            else
                Session["SessionEvento"] = SessionEvento;

            if (SessionEvento == null)
                Server.Transfer("frmSessaoExpirada.aspx", true);
            
            SessionChaveLibercao = (String)Session["SessionChaveLibercao"];
            if (SessionChaveLibercao == null)
                SessionChaveLibercao = "";

            
        }
        else
        {
            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"]; 
            
            SessionIdioma = (String)Session["SessionIdioma"];

            SessionCateg = (String)Session["SessionCateg"];

            SessionChaveLibercao = (String)Session["SessionChaveLibercao"];

            SessionAtv = (String)Session["SessionAtv"];
        }

        ToolkitScriptManager1.RegisterPostBackControl(Button1);
        ToolkitScriptManager1.RegisterPostBackControl(btnPesquisarNome);

        if (SessionEvento == null)
            Server.Transfer("frmSessaoExpirada.aspx", true);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        if (txtCPF.Text.Replace(".", "").Replace("-", "").Length < 11)
        {
            lblMsg.Text = "CPF Inválido";
            return;
        }

        //if (txtCaptcha.Text.Trim() == "")
        //{
        //    lblMsg.Text = "Caracteres inválidos";
        //    return;
        //}

        string tmpCPF = ValidarCPF("", SessionEvento.CdEvento, txtCPF.Text, "", SessionCnn);
        if (tmpCPF != "")
        {
            lblMsg.Text = tmpCPF;
            return;
        }


        ParticipantePreCadastro oParticipantePreCadastro = oParticipanteCad.PesquisarPreCadastro(SessionEvento.CdEvento, "", txtCPF.Text.Replace(".", "").Replace("-", ""), "", "", "", SessionCnn);

        if (oParticipantePreCadastro == null)
        {
            lblMsg.Text = "CPF não liberado para cadastro!";
            return;
        }

        Session["SessionCateg"] = oParticipantePreCadastro.CdCategoria;

        Server.Transfer("frmCadastroAuto.aspx?nome=" + oParticipantePreCadastro.NoParticipantePrecadastro + "&cpf=" + oParticipantePreCadastro.NuCPFCNPJ + "&UF=" + oParticipantePreCadastro.DsEmail.ToUpper(), true);

    }

    protected string ValidarCPF(string prmCdParticipante, string prmCdEvento, string prmCPF, string prmCdCategoria, SqlConnection prmCnn)
    {
        if ((!oClsFuncoes.CPFCNPJValidar(prmCPF)))
        {
            CategoriaCad oCategoriaCad = new CategoriaCad();
            Categoria oCategoria = oCategoriaCad.Pesquisar(prmCdEvento, prmCdCategoria, prmCnn);
            if (((SessionEvento != null) && (SessionEvento.FlLiberarCPFCoringa)))// || (!oCategoria.FlCPFCNPJObrigatorio))
                return "";

            return "CPF Inválido!";
        }
        else
        {
            string tmpCPF = prmCPF.Replace(".", "").Replace("-", "");


            


            /*if ((tmpCPF == "11111111111") ||
                (tmpCPF == "22222222222") ||
                (tmpCPF == "33333333333") ||
                (tmpCPF == "44444444444") ||
                (tmpCPF == "55555555555") ||
                (tmpCPF == "66666666666") ||
                (tmpCPF == "77777777777") ||
                (tmpCPF == "88888888888") ||
                (tmpCPF == "99999999999") ||
                (tmpCPF == "00000000000"))
            {
                CategoriaCad oCategoriaCad = new CategoriaCad();
                Categoria oCategoria = oCategoriaCad.Pesquisar(prmCdEvento, prmCdCategoria, prmCnn);
                if (((SessionEvento != null) && (SessionEvento.FlLiberarCPFCoringa)) || (!oCategoria.FlCPFCNPJObrigatorio) || (oCategoria.FlLiberarCPFCNPJCoringa))
                    return "";
                return "CPF Inválido!";
            }
            else
            {*/

            return oParticipanteCad.verificarDuplicidadeCPF(prmCdParticipante, prmCdEvento, tmpCPF, prmCdCategoria, prmCnn);

            
                 

            

            //}

        }

    }
    protected void btnPesquisarNome_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        if (txtNome.Text.Trim() == "")
        {
            lblMsg2.Text = "Nome Inválido";
            return;
        }

        //if (txtCaptcha.Text.Trim() == "")
        //{
        //    lblMsg.Text = "Caracteres inválidos";
        //    return;
        //}

        /*
        string tmpCPF = ValidarCPF("", SessionEvento.CdEvento, txtCPF.Text, "", SessionCnn);
        if (tmpCPF != "")
        {
            lblMsg.Text = tmpCPF;
            return;
        }
        */

        ParticipantePreCadastro oParticipantePreCadastro = oParticipanteCad.PesquisarPreCadastroPorNomeIgual(SessionEvento.CdEvento, "", "", txtNome.Text.ToUpper(), "", "", SessionCnn);

        if (oParticipantePreCadastro == null)
        {
            lblMsg2.Text = "Nome não liberado para cadastro!";
            return;
        }

        Session["SessionCateg"] = oParticipantePreCadastro.CdCategoria;

        Server.Transfer("frmCadastroAuto.aspx?nome=" + oParticipantePreCadastro.NoParticipantePrecadastro + "&cpf=" + oParticipantePreCadastro.NuCPFCNPJ + "&UF=" + oParticipantePreCadastro.DsEmail.ToUpper(), true);
    }
}