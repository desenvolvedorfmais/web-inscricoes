using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using cllEventos;
using CLLFuncoes;

using System.Data;
using System.Data.SqlClient;

public partial class frmTrabalhosLista : System.Web.UI.Page
{
    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    SqlConnection SessionCnn;

    Participante SessionParticipante;
    //ParticipanteCad oParticipanteCad = new ParticipanteCad();
    Evento SessionEvento;
    //EventoCad oEventoCad = new EventoCad();

    //Tese SessionTese;
    TeseCad oTeseCad = new TeseCad();

    DataTable oDT = new DataTable();

    TeseRepresentante SessionTeseRepresentante;
    TeseRepresentanteCad oTeseRepresentanteCad = new TeseRepresentanteCad();

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

            if (SessionEvento == null)
                SessionEvento = (Evento)Session["SessionEvento"];
            else
                Session["SessionEvento"] = SessionEvento;

            if (SessionEvento == null)
                Server.Transfer("frmSessaoExpirada.aspx", true);

            if (SessionParticipante == null)
                SessionParticipante = (Participante)Session["SessionParticipante"];
            else
                Session["SessionParticipante"] = SessionParticipante;
            
            //if (SessionTese == null)
            //    SessionTese = (Tese)Session["SessionTese"];
            //else
            //    Session["SessionTese"] = SessionTese;


            PesquisarTeses();


            SessionTeseRepresentante = (TeseRepresentante)Session["SessionTeseRepresentante"];
            if (SessionTeseRepresentante == null)
                SessionTeseRepresentante = oTeseRepresentanteCad.PesquisarPorEmail(SessionEvento.CdEvento, SessionParticipante.DsEmail, SessionCnn);
            
            Session["SessionTeseRepresentante"] = SessionTeseRepresentante;

            if ((SessionEvento.teseConfig.DtFechamentoInscrWeb == null) ||
                (SessionEvento.teseConfig.DtFechamentoInscrWeb < Geral.datahoraServidor(SessionCnn)))
            {
                btnNovo.Visible = false;
            }

        }
        else
        {
            //SessionTese = (Tese)Session["SessionTese"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            SessionIdioma = (String)Session["SessionIdioma"];


            if (SessionEvento == null)
                Server.Transfer("frmSessaoExpirada.aspx", true);

            PesquisarTeses();



            SessionTeseRepresentante = (TeseRepresentante)Session["SessionTeseRepresentante"];

        }

        if (SessionEvento == null)
            Server.Transfer("frmSessaoExpirada.aspx", true);

        
        verificarIdioma(SessionIdioma);

        //if (Geral.datahoraServidor(SessionCnn) > DateTime.Parse("10/11/2013 23:59:59"))
        //    btnNovo.Visible = false;
    }


    

    protected void verificarIdioma(string prmIdioma)
    {
        string url = "";
        if (prmIdioma == "PTBR")
        {
            SessionIdioma = "PTBR";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Trabalhos";

            lblSubTitulo.Text="ATENÇÃO";


            if (SessionEvento.CdEvento == "008501")
                url = "http://fazendomais.azurewebsites.net/imagensgeral/008501/Documento_de_orientacoes_para_trabalhos_PT.pdf";

            lblTxtInstrucao.Text = 
                "Antes de submeter seu trabalho é importante ler com atenção as instruções contidas nas Normas Científicas. " +
                "<br /><br /> " +
                "<a id=\"A1\" href=\""+url+"\" target=\"_blank\">Clique aqui</a> para acessar as normas.";

            if ((SessionEvento.teseConfig.DtFechamentoInscrWeb == null) ||
                (SessionEvento.teseConfig.DtFechamentoInscrWeb < Geral.datahoraServidor(SessionCnn)))
            {
                lblTxtInstrucao.Text = "Para enviar o pôster clique em cima do título do seu trabalho aprovado.";
            }

            btnNovo.Text = "Cadastrar Novo Trabalho";            

        }
        else if (prmIdioma == "ENUS")
        {
            SessionIdioma = "ENUS";
            Session["SessionIdioma"] = SessionIdioma;


            lblTituloPagina.Text = "Paper Registration";

            lblSubTitulo.Text = "ATTENTION";


            if (SessionEvento.CdEvento == "008501")
                url = "http://fazendomais.azurewebsites.net/imagensgeral/008501/Documento_de_orientacoes_para_trabalhos_ING.pdf";

            lblTxtInstrucao.Text =
                "Before submitting you paper it is important to read carefully the instructions. " +
                "<br /><br /> " +
                "<a id=\"A1\" href=\"" + url + "\" target=\"_blank\">Click here</a> to access instructions.";


            if ((SessionEvento.teseConfig.DtFechamentoInscrWeb == null) ||
                (SessionEvento.teseConfig.DtFechamentoInscrWeb < Geral.datahoraServidor(SessionCnn)))
            {
                lblTxtInstrucao.Text = "To send the poster click on the title of your approved paper.";
            }

            btnNovo.Text = "Register New Paper";            

            
        }
        else if (prmIdioma == "ESP")
        {
            SessionIdioma = "ESP";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Inscripción de trabajos";

            lblSubTitulo.Text = "ATENCIÓN";


            if (SessionEvento.CdEvento == "008501")
                url = "http://fazendomais.azurewebsites.net/imagensgeral/008501/Documento_de_orientacoes_para_trabalhos_ESP.pdf";

            lblTxtInstrucao.Text =
                "Antes de enviar su trabajo lea con atención las instrucciones en las orientaciones para trabajos. " +
                "<br /><br /> " +
                "<a id=\"A1\" href=\"" + url + "\" target=\"_blank\">Enlace aqui</a> para accesar las normas.";

            if ((SessionEvento.teseConfig.DtFechamentoInscrWeb == null) ||
                (SessionEvento.teseConfig.DtFechamentoInscrWeb < Geral.datahoraServidor(SessionCnn)))
            {
                lblTxtInstrucao.Text = "Para enviar el póster, haga clic en el título del trabajo aprobado.";
            }

            btnNovo.Text = "Registrar nuevo Trabajo";            

        }
        /*else if (prmIdioma == "FRA")
        {
            SessionIdioma = "FRA";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Demande d'inscription";
            lblTituloResumo.Text = "Résumé de la demande";

            lblPart.Text = "Participant:";
            lblCateg.Text = "Catégorie:";

            lblResPed.Text = "Demande no";
            lblResItens.Text = "Articles";
            lblResVlr.Text = "Valeur";
            lblResDesc.Text = "Réduction";
            lblResVlrTotal.Text = "Total";

            btnAvancar.Text = "Terminer";
            btnVerItensPedido.Text = "Continuer d`inscription";
            btnVoltarParaItens.Text = "Retour";

            lblFiltro.Text = "Filtre";
            lblTipoFiltro.Text = "Type";
            lblDtInicioFiltro.Text = "Accueil";

            lblTituloGrid1.Text = "Articles disponibles";
        }*/
    }
    protected void PesquisarTeses()
    {
        grdTeses.DataSource = null;
        grdTeses.DataBind();


        oDT = oTeseCad.ListarParticpantesModalidade(SessionParticipante.CdEvento, SessionParticipante.CdParticipante, SessionCnn);

        if (oDT == null)
        {
            return;
        }

        grdTeses.DataSource = oDT;
        grdTeses.DataBind();


        if ((SessionEvento.CdEvento == "008501") && (oDT.Rows.Count >= 3))
            btnNovo.Visible = false;

        //if (oDT.Rows.Count == 2)//(oTeseCad.TotalTesesInscrita(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn) <= 2)
        //    btnNovo.Visible = false;

        //if (Geral.datahoraServidor(SessionCnn) > DateTime.Parse("10/11/2013 23:59:59"))
        //    btnNovo.Visible = false;

    }
    protected void grdTeses_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {


            if (SessionIdioma == "PTBR")
            {
                e.Row.Cells[0].Text = "ID";
                e.Row.Cells[1].Text = "Modalidade";
                e.Row.Cells[2].Text = "Sub Eixo";
                e.Row.Cells[3].Text = "Título/Assunto";
                e.Row.Cells[4].Text = "Situação";
            }
            else if (SessionIdioma == "ENUS")
            {
                e.Row.Cells[0].Text = "ID";
                e.Row.Cells[1].Text = "Modality";
                e.Row.Cells[2].Text = "Sub theme";
                e.Row.Cells[3].Text = "Title of Paper";
                e.Row.Cells[4].Text = "Situation";
            }
            else if (SessionIdioma == "ESP")
            {
                e.Row.Cells[0].Text = "ID";
                e.Row.Cells[1].Text = "Modalidad";
                e.Row.Cells[2].Text = "Sub-eje Temático";
                e.Row.Cells[3].Text = "Título / Asunto";
                e.Row.Cells[4].Text = "Situación";
            }
            else if (SessionIdioma == "FRA")
            {
                //e.Row.Cells[0].Text = "n° Ordre";
                //e.Row.Cells[1].Text = "Demande";
                //e.Row.Cells[2].Text = "Échéance";
                //e.Row.Cells[3].Text = "Total";
                //e.Row.Cells[4].Text = "parcelle n°";
            }

        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Cells[0].Attributes.Add("onclick", "window.open('frmCadastrarTrabalhosTecnicos.aspx?cdTese=" + e.Row.Cells[0].Text + "','_self');");
            e.Row.Cells[1].Attributes.Add("onclick", "window.open('frmCadastrarTrabalhosTecnicos.aspx?cdTese=" + e.Row.Cells[0].Text + "','_self');");
            e.Row.Cells[2].Attributes.Add("onclick", "window.open('frmCadastrarTrabalhosTecnicos.aspx?cdTese=" + e.Row.Cells[0].Text + "','_self');");
            e.Row.Cells[3].Attributes.Add("onclick", "window.open('frmCadastrarTrabalhosTecnicos.aspx?cdTese=" + e.Row.Cells[0].Text + "','_self');");
            e.Row.Cells[4].Attributes.Add("onclick", "window.open('frmCadastrarTrabalhosTecnicos.aspx?cdTese=" + e.Row.Cells[0].Text + "','_self');");
            e.Row.Cells[5].Attributes.Add("onclick", "window.open('frmCadastrarTrabalhosTecnicos.aspx?cdTese=" + e.Row.Cells[0].Text + "','_self');");
            //e.Row.Cells[4].Attributes.Add("onclick", "window.open('frmCadastrarHoteis.aspx?cdHotel=" + e.Row.Cells[0].Text + "','_self');");
            //e.Row.Cells[5].Attributes.Add("onclick", "window.open('frmCadastrarHoteis.aspx?cdHotel=" + e.Row.Cells[0].Text + "','_self');");

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
    protected void btnNovo_Click(object sender, EventArgs e)
    {

       
        Session["SessionTese"] = null;

        //Server.Transfer("frmCadastrarTrabalhosTecnicos.aspx", false);
        Response.Write("<script>window.open('frmCadastrarTrabalhosTecnicos.aspx','_self');</script>");
    }
}