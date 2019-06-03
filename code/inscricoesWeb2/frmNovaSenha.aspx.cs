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

using AjaxControlToolkit;

public partial class frmNovaSenha : System.Web.UI.Page
{
    Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    SqlConnection SessionCnn;
    
    String SessionIdioma;
    
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

            SessionIdioma = (String)Session["SessionIdioma"];
            if (SessionIdioma == null)
                SessionIdioma = "PTBR";
            Session["SessionIdioma"] = SessionIdioma;


            if (SessionEvento == null)
                SessionEvento = (Evento)Session["SessionEvento"];
            else
                Session["SessionEvento"] = SessionEvento;

            if (SessionEvento == null)
                Server.Transfer("frmSessaoExpirada.aspx", true);

            lblIdentificador.Text = SessionParticipante.CdParticipante.Trim();
            lblNoParticipante.Text = SessionParticipante.NoParticipante.Trim();

            if (SessionEvento.CdEvento == "007002")
            {
                lblNoParticipante.Text = SessionParticipante.DsAuxiliar2 + ", " + SessionParticipante.NoParticipante.Trim();
            }

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

            txtDsSenhaAtual.Focus();

        }
        else
        {
            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

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

            lblTituloPagina.Text = "Mudança de senha";

            lblPart.Text = "Participante:";
            lblCateg.Text = "Categoria:";

            lblSenhaAtual.Text = "  Senha Atual:";
            lblNovaSenha.Text = " Nova Senha:";
            lblRepeteSenha.Text = "Repete Senha:";

            btnAlterarSenha.Text = "Alterar Senha";

            rfvSenhaAtual.ErrorMessage = rfvNovaSena.ErrorMessage = rfvSenhaNovaSenha2.ErrorMessage = "Campo requerido";

            CompValEmail.ErrorMessage = "Senhas diferentes";
        }
        else if (prmIdioma == "ENUS")
        {
            SessionIdioma = "ENUS";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Password Change";

            lblID.Text = "Registration no.";
            lblPart.Text = "Participant:";
            lblCateg.Text = "Category:";

            lblSenhaAtual.Text = "Current Password:";
            lblNovaSenha.Text = "New Password:";
            lblRepeteSenha.Text = "Repeat Password";

            btnAlterarSenha.Text = "Change Password";


            rfvSenhaAtual.ErrorMessage = rfvNovaSena.ErrorMessage = rfvSenhaNovaSenha2.ErrorMessage = "Required field";

            CompValEmail.ErrorMessage = "Different passwords";

        }
        else if (prmIdioma == "ESP")
        {
            SessionIdioma = "ESP";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Cambio de contraseña";

            lblID.Text = "Nº de Registro:";
            lblPart.Text = "Participante:";
            lblCateg.Text = "Categoría";

            lblSenhaAtual.Text = "Contraseña actual";
            lblNovaSenha.Text = "Nueva contraseña:";
            lblRepeteSenha.Text = "Repetir contraseña";

            btnAlterarSenha.Text = "Alterar contraseña";

            rfvSenhaAtual.ErrorMessage = rfvNovaSena.ErrorMessage = rfvSenhaNovaSenha2.ErrorMessage = "Campo obligatorio";

            CompValEmail.ErrorMessage = "Las contraseñas diferentes";
        }
        else if (prmIdioma == "FRA")
        {
            SessionIdioma = "FRA";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Changer le mot de passe";


            lblPart.Text = "Participant:";
            lblCateg.Text = "Catégorie:";

            lblSenhaAtual.Text = "Mot de passe actuel:";
            lblNovaSenha.Text = "Nouveau mot de passe:";
            lblRepeteSenha.Text = "Mot de passe Repeat";

            btnAlterarSenha.Text = "Changer mot de passe";

            rfvSenhaAtual.ErrorMessage = rfvNovaSena.ErrorMessage = rfvSenhaNovaSenha2.ErrorMessage = "Champs obligatoires";

            CompValEmail.ErrorMessage = "Mot de passe différent";
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        txtMsg.Visible = false;

        if (oParticipanteCad.AterarSenha(SessionEvento, SessionParticipante, txtDsSenhaAtual.Text, txtNovaSenha.Text, SessionCnn))
        {

            Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                "014-1",
                                ""), true);
        }
        else
        {
            txtMsg.Text = oParticipanteCad.RcMsg;
            txtMsg.Visible = true;
        }
    }
}
