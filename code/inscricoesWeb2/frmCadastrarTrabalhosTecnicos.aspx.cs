using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using cllEventos;
using CLLFuncoes;


using System.Security.Cryptography;
using System.Text;


using System.Data;
using System.Data.SqlClient;

using MSXML2;

using System.Xml;


//using System.Net;
using System.IO;

public partial class frmCadastrarTrabalhosTecnicos : System.Web.UI.Page
{
    /*
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    SqlConnection SessionCnn;
    SqlConnection SessionCnn2;

    Tese SessionTese;
    TeseCad oTeseCad = new TeseCad();


    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    
    //String SessionCateg;
    //String SessionAtv;
    */


    Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();

    String SessionIdioma;

    TeseRepresentante SessionTeseRepresentante;
    TeseRepresentanteCad oTeseRepresentanteCad = new TeseRepresentanteCad();

    Evento SessionEvento;


    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    SqlConnection SessionCnn;

    Tese SessionTese;
    //Tese SessionTeseFilho;    
    TeseCad oTeseCad = new TeseCad();

    TeseParticipanteCad oTeseParticipanteCad = new TeseParticipanteCad();
    TeseParticipante SessionTeseParticipante;

    DataTable oDTCoautores;// = new DataTable();

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

            SessionIdioma = (String)Session["SessionIdioma"];
            if (SessionIdioma == null)
                SessionIdioma = "PTBR";
            Session["SessionIdioma"] = SessionIdioma;


            if (SessionIdioma == "PTBR") 
                btnGravar.Attributes.Add("onclick", "document.body.style.cursor = 'wait'; this.value='Aguarde, processando...'; this.disabled = true; " + ClientScript.GetPostBackEventReference(btnGravar, string.Empty) + ";");
            else if (SessionIdioma == "ENUS") 
                btnGravar.Attributes.Add("onclick", "document.body.style.cursor = 'wait'; this.value='Wait, processing...'; this.disabled = true; " + ClientScript.GetPostBackEventReference(btnGravar, string.Empty) + ";");
            else if (SessionIdioma == "ESP") 
                btnGravar.Attributes.Add("onclick", "document.body.style.cursor = 'wait'; this.value='Espera, procesamiento...'; this.disabled = true; " + ClientScript.GetPostBackEventReference(btnGravar, string.Empty) + ";");

            if (SessionParticipante == null)
                SessionParticipante = (Participante)Session["SessionParticipante"];
            else
                Session["SessionParticipante"] = SessionParticipante;

            if (SessionTeseRepresentante == null)
                SessionTeseRepresentante = (TeseRepresentante)Session["SessionTeseRepresentante"];
            else
                Session["SessionTeseRepresentante"] = SessionTeseRepresentante;

            if (SessionTese == null)
                SessionTese = (Tese)Session["SessionTese"];
            else
                Session["SessionTese"] = SessionTese;

            //if (SessionTeseFilho == null)
            //    SessionTeseFilho = (Tese)Session["SessionTeseFilho"];
            //else
            //    Session["SessionTeseFilho"] = SessionTeseFilho;


            if (SessionEvento == null)
                Server.Transfer("frmSessaoExpirada.aspx", true);

            if (SessionIdioma != "PTBR")
            {
                grdSubscritor.Columns[1].Visible = false;
                linhaCPFCoautor.Visible = false;
                linhaCPF.Visible = false;
            }

            ListarUF();

            ListarModalidadeTipo();

            ListarAreaTematica();

            ListarAreaTematicaFilho("");

            //DataTable oDT = oTeseCad.ListarTesesFilho(SessionTeseRepresentante.CdEvento, SessionTese.CdTese, SessionCnn);
            //if ((oDT != null) && (oDT.Rows.Count == 3))
            //    btnNovo.Visible = false;
             

            btnExcluirProposta.Visible = false;

            if ((Request["cdTese"] != null) &&
                (Request["cdTese"] != ""))
            {

                txtCdTese.Text = Request["cdTese"];

                Session["oDTCoaut"] = null;

                

                Pesquisar(Request["cdTese"]);

                btnExcluirProposta.Visible = true;
            }
            else if (SessionTese != null)
            {

                txtCdTese.Text = SessionTese.CdTese;

                Session["oDTCoaut"] = null;

                

                Pesquisar(SessionTese.CdTese);

                btnExcluirProposta.Visible = true;
            }
            else
            {
                //txtNome.Text = SessionParticipante.NoParticipante;

                TXTDsCPF.Text = oClsFuncoes.MascaraGerar(SessionTeseRepresentante.NuCPFRepresentante, "999.999.999-99");

                txtNome.Text = SessionTeseRepresentante.NoRepresentante;
                txtSexo.Text = SessionTeseRepresentante.DsSexo;
                txtPassaporte.Text = SessionTeseRepresentante.NrPassaporte;
                txtEmail.Text = SessionTeseRepresentante.DsEmail.ToLower();
                txtPais.Text = SessionTeseRepresentante.NoPais;
                txtCEPRecibo.Text = SessionTeseRepresentante.NuCEP;
                txtEnderecoRecibo.Text = SessionTeseRepresentante.DsEndereco;
                txtComplementoEnderecoRecibo.Text = SessionTeseRepresentante.DsComplementoEndereco;
                txtBairroRecibo.Text = SessionTeseRepresentante.NoBairro;
                txtCidadeRecibo.Text = SessionTeseRepresentante.NoCidade;
                txtUF.Text = SessionTeseRepresentante.DsUF;
                txtFone.Text = oClsFuncoes.MascaraGerar(SessionTeseRepresentante.DsFone1, "(99) 999999999");
                txtTitulacao.SelectedValue = SessionTeseRepresentante.NoTitulacao;
                txtInstituicaoGraduacao.Text = SessionTeseRepresentante.NoInstituicaoGraduacao;
                
                txtInstituicao.Text = SessionTeseRepresentante.NoInstituicao;
                txtAreaAtuacao.Text = SessionTeseRepresentante.NoAreaAtuacao;

                txtTpInstituicao.Text = SessionTeseRepresentante.TpInstituicao;
                txtNivelGoverno.Text = SessionTeseRepresentante.DsNivelGoverno;
                txtPoderGoverno.Text = SessionTeseRepresentante.DsPoderGoverno;
                txtTipoSetorPrivado.Text = SessionTeseRepresentante.DsTipoSetorPrivado;

                txtAreaTematica.SelectedValue = "";
                txtAreaTematicaFilho.SelectedValue = "";

                //linhaDeAcordo.Visible = true;

                oDTCoautores = new DataTable();
                carregarGrdSubscritoresTese();
                Session["oDTCoaut"] = oDTCoautores;
            }

            if ((SessionEvento.teseConfig.DtFechamentoInscrWeb == null) ||
                (SessionEvento.teseConfig.DtFechamentoInscrWeb < Geral.datahoraServidor(SessionCnn)))
            {
                btnNovo.Visible = false;
                btnGravar.Visible = false;
                btnExcluirProposta.Visible = false;

                btnCEP.Visible = false;

                linhaCPFCoautor.Visible = false;
                linhaNomeCoautor.Visible = false;
                linhaApresentador.Visible = false;
                linhaBtnsCoautor.Visible = false;

                TXTDsCPF.Enabled = false;
                txtNome.Enabled = false;
                txtSexo.Enabled = false;
                txtPassaporte.Enabled = false;
                txtEmail.Enabled = false;
                txtPais.Enabled = false;
                txtCEPRecibo.Enabled = false;
                txtEnderecoRecibo.Enabled = false;
                txtComplementoEnderecoRecibo.Enabled = false;
                txtBairroRecibo.Enabled = false;
                txtCidadeRecibo.Enabled = false;
                txtUF.Enabled = false;
                txtFone.Enabled = false;
                txtTitulacao.Enabled = false;
                txtInstituicaoGraduacao.Enabled = false;


                txtInstituicao.Enabled = false;
                txtAreaAtuacao.Enabled = false;

                txtTpInstituicao.Enabled = false;
                txtNivelGoverno.Enabled = false;
                txtPoderGoverno.Enabled = false;
                txtTipoSetorPrivado.Enabled = false;

                txtAreaTematica.Enabled = false;
                txtTitulo.ReadOnly = true;
                txtDsTrabalho.ReadOnly = true;

                txtAreaAtuacao.Enabled = false;
                txtModalidadeTipo.Enabled = false;

                grdSubscritor.Columns[5].Visible = false;
            }
            if (SessionEvento.CdCliente == "0003")
                lblRegrasPainel.Text = "<p><span style=\"color: #0000ff;\"><strong>Regras de proposi&ccedil;&atilde;o de trabalhos isolados:</strong></span><br /><span style=\"color: #0000ff;\">&nbsp; . As propostas de trabalhos (paper) isolados, ou seja, n&atilde;o agrupadas em painel, devem ser encaminhadas pelo autor. </span><br /><span style=\"color: #0000ff;\">&nbsp; . As propostas de trabalho s&oacute; podem ser encaminhadas uma &uacute;nica vez: n&atilde;o ser&atilde;o aceitas propostas de um mesmo trabalho agrupado em painel e como trabalho isolado.</span><br /><span style=\"color: #0000ff;\">&nbsp; . O Comit&ecirc; Cient&iacute;fico agrupar&aacute; os trabalhos isolados aprovados em pain&eacute;is tem&aacute;ticos, a seu crit&eacute;rio.</span></p>";
        }
        else
        {
            SessionTese = (Tese)Session["SessionTese"];

            //SessionTeseFilho = (Tese)Session["SessionTeseFilho"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            SessionIdioma = (String)Session["SessionIdioma"];

            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionTeseRepresentante = (TeseRepresentante)Session["SessionTeseRepresentante"];

            SessionTeseParticipante = (TeseParticipante)Session["SessionTeseParticipante"];


            oDTCoautores = (DataTable)Session["oDTCoaut"];

            //grdSubscritor.DataSource = oDTCoautores.DefaultView;
            //grdSubscritor.DataBind();

        }

        if (SessionEvento == null)
            Server.Transfer("frmSessaoExpirada.aspx", true);

        verificarIdioma(SessionIdioma);

        //if (Geral.datahoraServidor(SessionCnn) > DateTime.Parse("10/11/2013 23:59:59"))
        //{
        //    btnGravar.Visible = false;
        //    btnNovo.Visible = false;
        //    btnExcluirProposta.Visible = false;

        //    linhaArquivo.Visible = false;

        //    linhaNomeCoautor.Visible = false;

        //    grdSubscritor.Columns[1].Visible = false;
        //}

        if (SessionIdioma == "PTBR")
            btnExcluirProposta.Attributes.Add("onclick", "return confirm('Esta operação irá excluir em definitivo a proposta de Trabalho e seus coautores!\\n\\nConfirma a exclusão?');");
        else if (SessionIdioma == "ENUS")
            btnExcluirProposta.Attributes.Add("onclick", "return confirm('This operation will delete definitively the proposed work and his coauthors!\\n\\nConfirms exclusion?');");
        else if (SessionIdioma == "ESP")
            btnExcluirProposta.Attributes.Add("onclick", "return confirm('Esta operación eliminará definitivamente el trabajo propuesto y sus coautores!\\n\\nConfirma la exclusión?');");

        TSManager1.RegisterPostBackControl(btnGravar);
        TSManager1.RegisterPostBackControl(btnEnviarTrabalho);
        
    }

    protected void verificarIdioma(string prmIdioma)
    {
        if (prmIdioma == "PTBR")//(txtidioma.SelectedIndex == 0)
        {
            SessionIdioma = "PTBR";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Cadastro de Trabalho";
            lblTituloDetalheTrab.Text = "Detalhes do Trabalho";
            lblTituloDadosAutor.Text = "Dados do Autor";
            lblTituloOutrosAutores.Text = "Outros Autores";
            lblTituloEnvioDoc.Text = "Envio de Documento";

            lblID.Text = "ID";
            lblSit.Text = "Situação";

            lblModalidadeTipo.Text = "Modalidade*";
            lblAreaTematica.Text = "Eixo Temático*";
            lblAreaTematicaFilho.Text = "Sub Eixo Temático*";

            lblTitulo.Text = "Título do trabalho*";
            lblDsTrabalho.Text = "Descrição do trabalho* (máximo de 2.500 caracteres)";
            lblDsCPF.Text = "CPF";

            lblNome.Text = "Nome";

            lblNomeCoautor.Text = "Nome";

            btnGravarSubscritor.Text = "Incluir";
            btnExcluirTodosCoautores.Text = "Remover Todos";
            lblObsCoautores.Text = "*Após a inclusão de coautores deve-se gravar a proposta.";

            lblInstrucaoUpLoad.Text = "Selecione o arquivo para enviar (somente arquivos na extensão: .pdf | .doc | .docx | .rtf | .odt | .png | .jpg)";

            btnEnviarTrabalho.Text = "Enviar";
            lblTrabEnviado.Text = "  Trabalho já enviado  ";
            lblTrabBaixado.Text = "  Trabalho já processado pela comissão organizadora  ";

            chkDeAcordo.Text = "  Li e concordo com as normas do evento";

            btnVoltar.Text = "Voltar para listagem";
            btnNovo.Text = "Nova Proposta";
            btnGravar.Text = "Gravar Proposta";
            btnExcluirProposta.Text = "Excluir Proposta";



            //txtSexo.Text = "";
            //txtPassaporte.Text = "";
            //txtEmail.Text = "";
            //txtPais.Text = "";
            //txtCEPRecibo.Text = "";
            //txtEnderecoRecibo.Text = "";
            //txtComplementoEnderecoRecibo.Text = "";
            //txtBairroRecibo.Text = "";
            //txtCidadeRecibo.Text = "";
            //txtUF.Text = "";
            //txtFone.Text = "";
            //txtTitulacao.Text = "";
            //txtInstituicaoGraduacao.Text = "";
            //txtInstituicao.Text = "";
            //txtAreaAtuacao.Text = "";
            //txtAreaTematica.Text = "";
            //txtModalidadeTipo.Text = "";

            //txtTpInstituicao.Text = "";
            //txtNivelGoverno.Text = "";
            //txtPoderGoverno.Text = "";
            //txtTipoSetorPrivado.Text = "";

            

        }
        else if (prmIdioma == "ENUS")//(txtidioma.SelectedIndex == 1)
        {
            SessionIdioma = "ENUS";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Paper Registration";
            lblTituloDetalheTrab.Text = "Details of the Paper";
            lblTituloDadosAutor.Text = "Information about the author";
            lblTituloOutrosAutores.Text = "Other Authors";
            lblTituloEnvioDoc.Text = "Send Paper";

            lblID.Text = "ID";
            lblSit.Text = "Status";

            lblModalidadeTipo.Text = "Modality*";
            lblAreaTematica.Text = "Theme*";
            lblAreaTematicaFilho.Text = "Sub theme*";

            lblTitulo.Text = "Title of Paper*";
            lblDsTrabalho.Text = "Description of Paper* (maximum 2500 characters)";
            //lblDsCPF.Text = "CPF";

            lblNome.Text = "Name";

            lblNomeCoautor.Text = "Name";

            btnGravarSubscritor.Text = "Include";
            btnExcluirTodosCoautores.Text = "Remove all";
            lblObsCoautores.Text = "*After the inclusion of co-authors the proposition must be saved";

            lblInstrucaoUpLoad.Text = "Select the file to send (Only PDF, DOC, DOCX, RTF, PNG and JPG files)";

            btnEnviarTrabalho.Text = "Send Paper";
            lblTrabEnviado.Text = "  Paper already sent  ";
            lblTrabBaixado.Text = "  Paper already processed by the organizing committee  ";

            chkDeAcordo.Text = "  I have read and agree to the rules of the event";

            btnVoltar.Text = "Back to list";
            btnNovo.Text = "New proposition";
            btnGravar.Text = "Save proposition";
            btnExcluirProposta.Text = "Delete Proposition";

        }
        else if (prmIdioma == "ESP")//(txtidioma.SelectedIndex == 2)
        {
            SessionIdioma = "ESP";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Registro de trabajo";
            lblTituloDetalheTrab.Text = "Detalles del trabajo";
            lblTituloDadosAutor.Text = "Datos del autor";
            lblTituloOutrosAutores.Text = "Otros autores";
            lblTituloEnvioDoc.Text = "Enviar Documento";

            lblID.Text = "ID";
            lblSit.Text = "Situación";

            lblModalidadeTipo.Text = "Modalidad*";
            lblAreaTematica.Text = "Eje Temático*";
            lblAreaTematicaFilho.Text = "Sub-eje temático*";

            lblTitulo.Text = "Título del trabajo*";
            lblDsTrabalho.Text = "Descripción de trabajo (máximo 2,500 caracteres)";
            lblDsCPF.Text = "CPF";

            lblNome.Text = "Nombre";

            lblNomeCoautor.Text = "Nombre";

            btnGravarSubscritor.Text = "Incluir";
            btnExcluirTodosCoautores.Text = "Apagar todos";
            lblObsCoautores.Text = "*Después de la inclusión de coautores salvar la propuesta.";
            
            lblInstrucaoUpLoad.Text = "Seleccionar archivo (sólo archivos en: .doc | .docx | .rtf | .odt | .png | .jpg)";

            btnEnviarTrabalho.Text = "Enviar";
            lblTrabEnviado.Text = "  El trabajo ya ha sido enviada  ";
            lblTrabBaixado.Text = "  Trabajos ya procesados por el comité organizador  ";

            chkDeAcordo.Text = "  He leído y acepto las reglas del evento";

            btnVoltar.Text = "Volver para la lista";
            btnNovo.Text = "Nueva Propuesta";
            btnGravar.Text = "Guardar propuesta";
            btnExcluirProposta.Text = "Excluir propuesta";

        }
        else if (prmIdioma == "FRA")//(txtidioma.SelectedIndex == 3)
        {
            SessionIdioma = "FRA";
            Session["SessionIdioma"] = SessionIdioma;
        }
    }

    public void ListarModalidadeTipo()
    {

        TeseTipoCad oTeseTipoCad = new TeseTipoCad();

        DataTable DT = oTeseTipoCad.Listar(SessionEvento.CdEvento, SessionCnn);

        DataTable DT2 = new DataTable();
        DT2.Columns.Add("cdTipoTese");
        DT2.Columns.Add("noTipoTese");
        DT2.Columns.Add("noTipoTeseENUS");
        DT2.Columns.Add("noTipoTeseESP");
        DT2.Columns.Add("noTipoTeseFRA");
        DT2.Columns.Add("cdTipoTese2");

        DT2.Rows.Add("", "");

        if (DT == null)
        {
            txtModalidadeTipo.DataSource = DT2;
            txtModalidadeTipo.DataBind();
            return;
        }

        for (int i = 0; i < DT.Rows.Count; i++)
        {
            DT2.Rows.Add(DT.DefaultView[i]["cdTipoTese"].ToString(),
                (SessionEvento.CdCliente != "0085" ? DT.DefaultView[i]["noTipoTese"].ToString() : (!DT.DefaultView[i]["noTipoTese"].ToString().ToUpper().Contains("MOSTRA") ? DT.DefaultView[i]["noTipoTese"].ToString() : "Mostra Internacional de Boas Práticas de Gestão e Cuidado na Atenção Perinatal")),
                (SessionEvento.CdCliente != "0085" ? DT.DefaultView[i]["noTipoTeseENUS"].ToString() : (!DT.DefaultView[i]["noTipoTeseENUS"].ToString().ToUpper().Contains("INTERNATIONAL SHOWCASE") ? DT.DefaultView[i]["noTipoTeseENUS"].ToString() : "International Showcase of Good Practices in Perinatal Management and Care")),
                (SessionEvento.CdCliente != "0085" ? DT.DefaultView[i]["noTipoTeseESP"].ToString() : (!DT.DefaultView[i]["noTipoTeseESP"].ToString().ToUpper().Contains("MUESTRA INTERNACIONAL") ? DT.DefaultView[i]["noTipoTeseESP"].ToString() : "Muestra de Experiencias Exitosas de la Red Cigüeña")),
                DT.DefaultView[i]["noTipoTeseFRA"].ToString(),
                DT.DefaultView[i]["noTipoTese"].ToString()
                //DT.DefaultView[i]["noTipoTeseENUS"].ToString(),
                //DT.DefaultView[i]["noTipoTeseESP"].ToString(),
                //DT.DefaultView[i]["noTipoTeseFRA"].ToString()
                );
        }

        txtModalidadeTipo.DataSource = DT2.DefaultView;
        txtModalidadeTipo.DataTextField = "noTipoTese";
        if (SessionIdioma == "ENUS")
            txtModalidadeTipo.DataTextField = "noTipoTeseENUS";
        if (SessionIdioma == "ESP")
            txtModalidadeTipo.DataTextField = "noTipoTeseESP";
        if (SessionIdioma == "FRA")
            txtModalidadeTipo.DataTextField = "noTipoTeseFRA";

        txtModalidadeTipo.DataValueField = "cdTipoTese2";
        txtModalidadeTipo.DataBind();

    }

    public void ListarAreaTematica()
    {

        TeseModalidadeCad oTeseModalidadeCad = new TeseModalidadeCad();

        DataTable DT = oTeseModalidadeCad.ListarTesesPai(SessionEvento.CdEvento, SessionCnn);

        DataTable DT2 = new DataTable();
        DT2.Columns.Add("cdModalidadeTese");
        DT2.Columns.Add("noModalidadeTese");
        DT2.Columns.Add("noModalidadeTeseENUS");
        DT2.Columns.Add("noModalidadeTeseESP");
        DT2.Columns.Add("noModalidadeTeseFRA");

        DT2.Rows.Add("", "");

        if (DT == null)
        {
            txtAreaTematica.DataSource = DT2;
            txtAreaTematica.DataBind();
            return;
        }

        for (int i = 0; i < DT.Rows.Count; i++)
        {
            DT2.Rows.Add(DT.DefaultView[i]["cdModalidadeTese"].ToString(),
                         DT.DefaultView[i]["noModalidadeTese"].ToString(),
                         DT.DefaultView[i]["noModalidadeTeseENUS"].ToString(),
                         DT.DefaultView[i]["noModalidadeTeseESP"].ToString(),
                         DT.DefaultView[i]["noModalidadeTeseFRA"].ToString());
        }

        txtAreaTematica.DataSource = DT2.DefaultView;
        txtAreaTematica.DataTextField = "noModalidadeTese";
        if (SessionIdioma == "ENUS")
            txtAreaTematica.DataTextField = "noModalidadeTeseENUS";
        if (SessionIdioma == "ESP")
            txtAreaTematica.DataTextField = "noModalidadeTeseESP";
        if (SessionIdioma == "FRA")
            txtAreaTematica.DataTextField = "noModalidadeTeseFRA";
        
        txtAreaTematica.DataValueField = "cdModalidadeTese";
        txtAreaTematica.DataBind();

    }

    public void ListarAreaTematicaFilho(String prmCdTeseModalidadePai)
    {

        TeseModalidadeCad oTeseModalidadeCad = new TeseModalidadeCad();

        DataTable DT = oTeseModalidadeCad.ListarTesesFilho(SessionEvento.CdEvento, prmCdTeseModalidadePai, SessionCnn);

        DataTable DT2 = new DataTable();
        DT2.Columns.Add("cdModalidadeTese");
        DT2.Columns.Add("noModalidadeTese");
        DT2.Columns.Add("noModalidadeTeseENUS");
        DT2.Columns.Add("noModalidadeTeseESP");
        DT2.Columns.Add("noModalidadeTeseFRA");

        DT2.Rows.Add("", "");

        if (DT == null)
        {
            txtAreaTematicaFilho.DataSource = DT2;
            txtAreaTematicaFilho.DataBind();
            return;
        }

        for (int i = 0; i < DT.Rows.Count; i++)
        {
            DT2.Rows.Add(DT.DefaultView[i]["cdModalidadeTese"].ToString(),
                         DT.DefaultView[i]["noModalidadeTese"].ToString(),
                         DT.DefaultView[i]["noModalidadeTeseENUS"].ToString(),
                         DT.DefaultView[i]["noModalidadeTeseESP"].ToString(),
                         DT.DefaultView[i]["noModalidadeTeseFRA"].ToString());
        }

        txtAreaTematicaFilho.DataSource = DT2.DefaultView;
        txtAreaTematicaFilho.DataTextField = "noModalidadeTese";
        if (SessionIdioma == "ENUS")
            txtAreaTematicaFilho.DataTextField = "noModalidadeTeseENUS";
        if (SessionIdioma == "ESP")
            txtAreaTematicaFilho.DataTextField = "noModalidadeTeseESP";
        if (SessionIdioma == "FRA")
            txtAreaTematicaFilho.DataTextField = "noModalidadeTeseFRA";

        txtAreaTematicaFilho.DataValueField = "cdModalidadeTese";
        txtAreaTematicaFilho.DataBind();

    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        //oDTCoautores = (DataTable)Session["oDTCoaut"];

        //grdSubscritor.DataSource = oDTCoautores.DefaultView;
        //grdSubscritor.DataBind();
    }
    protected void chkCPF_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCPF.Checked)
        {
            chkPassaporte.Checked = false;
            
            linhaCPF.Visible = true;
            linhaPassaporte.Visible = false;
            linhaCEP.Visible = true;
            linhaEndereco.Visible = true;
            linhaComplemento.Visible = true;
            linhaBairro.Visible = true;
            linhaUF.Visible = true;
            linhaCidade.Visible = true;

            linhaPais.Visible = false;
            txtPais.Text = "BRASIL";
        }
    }
    protected void chkPassaporte_CheckedChanged(object sender, EventArgs e)
    {
        if (chkPassaporte.Checked)
        {
            chkCPF.Checked = false;

            linhaCPF.Visible = false;
            linhaPassaporte.Visible = true;
            linhaCEP.Visible = false;
            linhaEndereco.Visible = false;
            linhaComplemento.Visible = false;
            linhaBairro.Visible = false;
            linhaUF.Visible = false;
            linhaCidade.Visible = false;

            linhaPais.Visible = true;
            txtPais.Text = "";
        }
    }
    public void ListarUF()
    {

        Geral oGeral = new Geral();
        txtUF.DataSource = oGeral.ListarUFs(SessionCnn);
        txtUF.DataTextField = "dsUF";
        txtUF.DataValueField = "dsUF";
        txtUF.DataBind();

    }
    protected void btnCEP_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsgCEP.Visible = false;

            txtEnderecoRecibo.Text = "";
            txtBairroRecibo.Text = "";
            txtCidadeRecibo.Text = "";
            txtUF.SelectedValue = "";

            if (txtCEPRecibo.Text.Replace(".", "").Replace("-", "").Trim() == "")
                return;


            WebCEP webcep = new WebCEP(txtCEPRecibo.Text.Replace(".", "").Replace("-", ""));

            if (webcep != null)
            {
                txtEnderecoRecibo.Text = webcep.TipoLagradouro + " " + webcep.Lagradouro;

                txtBairroRecibo.Text = webcep.Bairro;

                txtUF.SelectedValue = webcep.UF;

                txtCidadeRecibo.Text = webcep.Cidade;
                return;
            }
            else
            {
                lblMsgCEP.Visible = true;
                lblMsgCEP.Text = webcep.ResultadoTXT;
                return;
            }
        }
        catch
        {
            lblMsgCEP.Visible = true;
        }
        
    }

    protected DataTable ValidarCPF(string prmCdTese, string prmCdEvento, string prmCPF, string prmCdTesePai, SqlConnection prmCnn)
    {
        DataTable DTRetorno = new DataTable();

        DTRetorno.Columns.Add("tpRetorno");
        DTRetorno.Columns.Add("MsgRetorno");
        DTRetorno.Columns.Add("nuCPF");
        DTRetorno.Columns.Add("dsNome");
        //DTRetorno.Columns.Add("tpRetorno");

        DTRetorno.Rows.Add("Ok","","","");

        if ((!oClsFuncoes.CPFCNPJValidar(prmCPF)))
        {
            DTRetorno.DefaultView[0]["tpRetorno"] = "Erro";
            DTRetorno.DefaultView[0]["MsgRetorno"] = "CPF Inválido!";

            return DTRetorno;
        }

        string cpftemp = TXTDsCPF.Text.Replace(".", "").Replace("-", "").Trim();

        string tmpCPF = oTeseCad.verificarDuplicidadeCPF(prmCdTese, prmCdEvento, cpftemp, prmCdTesePai, prmCnn);
           // ValidarCPF("", SessionEvento.CdEvento, cpftemp, SessionCateg, SessionCnn);
        if (tmpCPF != "")
        {
            DTRetorno.DefaultView[0]["tpRetorno"] = "Erro";
            DTRetorno.DefaultView[0]["MsgRetorno"] = tmpCPF;

            return DTRetorno;
        }


        if ((cpftemp == "11111111111") ||
            (cpftemp == "22222222222") ||
            (cpftemp == "33333333333") ||
            (cpftemp == "44444444444") ||
            (cpftemp == "55555555555") ||
            (cpftemp == "66666666666") ||
            (cpftemp == "77777777777") ||
            (cpftemp == "88888888888") ||
            (cpftemp == "99999999999") ||
            (cpftemp == "00000000000"))
        {

            DTRetorno.DefaultView[0]["tpRetorno"] = "Erro";
            DTRetorno.DefaultView[0]["MsgRetorno"] = "CPF Inválido!";

            return DTRetorno;
        }
        else
        {
            //PESQUISAR CPF BANCO LOCAL
            ParticipanteCad oParticipanteCad = new ParticipanteCad();
            SqlConnection SessionCnnHISTORICO = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOWJYTnpjV3d1Wm1GNlpXNWtiMjFoYVhNdVkyOXRPMGx1YVhScFlXd2dRMkYwWVd4dlp6MW1ZWHBsYm1SdmJXRnBjekU3VUdWeWMybHpkQ0JUWldOMWNtbDBlU0JKYm1adlBWUnlkV1U3VlhObGNpQkpSRDFtWVhwbGJtUnZiV0ZwY3pFN1VHRnpjM2R2Y21ROVVYVTBOVFJ5Um0wPQ==")));
            DataTable DTCpf = oParticipanteCad.PesquisaCPF(cpftemp, SessionCnnHISTORICO);
            if ((DTCpf != null) && (DTCpf.Rows.Count > 0))
            {
                DTRetorno.DefaultView[0]["tpRetorno"] = "Ok";
                DTRetorno.DefaultView[0]["MsgRetorno"] = "";
                DTRetorno.DefaultView[0]["nuCPF"] = cpftemp;
                DTRetorno.DefaultView[0]["dsNome"] = DTCpf.DefaultView[0]["Nome"].ToString();

                return DTRetorno;
                
                
            }
            else
            {/*
                //PESQUISAR CPF BANCO RECEITA
                int tmpSaldoPesqCPF = Geral.verificarTotalPesquisaCPF(SessionCnn);

                if (tmpSaldoPesqCPF > 0)
                {

                    DataSet ds = new DataSet();
                    ds.ReadXml("http://ws.fontededados.com.br/consulta.asmx/SituacaoCadastralPF?login=" +
                        cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("Wm1GNlpXNWtiMjFoYVhNPQ==")) +
                        "&senha=" + cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("Um0xQWFUVXpORGcy")) +
                        "&cpf=" + cpftemp.Replace("-", ""));

                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["MensagemErro"].ToString() != "")
                            {
                                DTRetorno.DefaultView[0]["tpRetorno"] = "Erro";
                                DTRetorno.DefaultView[0]["MsgRetorno"] = ds.Tables[0].Rows[0]["MensagemErro"].ToString();
                                return DTRetorno;
                            }
                            else
                            {
                                oParticipanteCad.IncluirCPF(ds.Tables[0].Rows[0]["Cpf"].ToString(), ds.Tables[0].Rows[0]["Nome"].ToString(), SessionCnnHISTORICO);
                                Geral.DecrementarPesquisaCPFReceita(SessionCnn);

                                DTRetorno.DefaultView[0]["tpRetorno"] = "Ok";
                                DTRetorno.DefaultView[0]["MsgRetorno"] = "";
                                DTRetorno.DefaultView[0]["nuCPF"] = ds.Tables[0].Rows[0]["Cpf"].ToString();
                                DTRetorno.DefaultView[0]["dsNome"] = ds.Tables[0].Rows[0]["Nome"].ToString();

                            }
                        }
                    }
                }
                */
                return DTRetorno;
            }
        }

    }
    protected void TXTDsCPF_TextChanged(object sender, EventArgs e)
    {

        lblMsgBuscaCPF.Text = "";
        lblMsgBuscaCPF.Visible = false;

        if (TXTDsCPF.Text.Replace(".", "").Replace("-", "") == "")
            return;

        if (TXTDsCPF.Text.Replace(".", "").Replace("-", "").Length < 11)
        {
            lblMsgBuscaCPF.Text = "CPF Inválido";
            lblMsgBuscaCPF.Visible = true;
            txtNome.Text = "";
            return;
        }

        if ((oDTCoautores != null) && (oDTCoautores.Rows.Count > 0))
        {
            for (int i = 0; i < oDTCoautores.Rows.Count; i++)
            {
                if (TXTDsCPF.Text.Replace(".", "").Replace("-", "").Trim() == oDTCoautores.DefaultView[i]["nuCPF"].ToString().Replace(".", "").Replace("-", "").Trim())
                {
                    lblMsgBuscaCPF.Text = "CPF já cadastrado como Coautor deste trabalho";
                    lblMsgBuscaCPF.Visible = true;
                    txtNome.Text = "";
                    return;
                }
            }
        }

        DataTable tmpreturnoCPF = ValidarCPF(txtCdTese.Text, SessionEvento.CdEvento, TXTDsCPF.Text, SessionTese.CdTese, SessionCnn);
        if (tmpreturnoCPF.DefaultView[0]["tpRetorno"].ToString() != "Ok")
        {
            lblMsgBuscaCPF.Text = tmpreturnoCPF.DefaultView[0]["MsgRetorno"].ToString();
            lblMsgBuscaCPF.Visible = true;
            txtNome.Text = "";
            return;
        }

        if (txtNome.Text != "")
            return;

        txtNome.Text = tmpreturnoCPF.DefaultView[0]["dsNome"].ToString();

        txtNome.Focus();

    }


    protected void Pesquisar(String prmCdTese)
    {
        lblMsg.Text = "";
        lblMsg0.Text = "";
        lblMsg.Visible = lblMsg0.Visible = false;


        txtCdTese.Text = "";

        lblSituacao.Text = "";

        txtTitulo.Text = "";
        txtDsTrabalho.Text = "";
        TXTDsCPF.Text = "";

        txtNome.Text = "";
        txtSexo.Text = "";
        txtPassaporte.Text = "";
        txtEmail.Text = "";
        txtPais.Text = "";
        txtCEPRecibo.Text = "";
        txtEnderecoRecibo.Text = "";
        txtComplementoEnderecoRecibo.Text = "";
        txtBairroRecibo.Text = "";
        txtCidadeRecibo.Text = "";
        txtUF.Text = "";
        txtFone.Text = "";
        txtTitulacao.Text = "";
        txtInstituicaoGraduacao.Text = "";
        txtInstituicao.Text = "";
        txtAreaAtuacao.Text = "";
        txtAreaTematica.Text = "";
        txtModalidadeTipo.Text = "";

        txtTpInstituicao.Text = "";
        txtNivelGoverno.Text = "";
        txtPoderGoverno.Text = "";
        txtTipoSetorPrivado.Text = "";

        txtAreaTematicaFilho.Text = "";

        SessionTese = oTeseCad.Pesquisar(prmCdTese, SessionCnn);

        Session["SessionTese"] = SessionTese;

        if (SessionTese == null)
        {
            lblMsg.Text = oTeseCad.RcMsg;
            lblMsg0.Text = oTeseCad.RcMsg;
            lblMsg.Visible = lblMsg0.Visible = true;
            return;
        }


        txtCdTese.Text = SessionTese.CdTese;

        lblSituacao.Text = SessionTese.DsSituacao;

        txtAreaTematica.SelectedValue = SessionTese.CdModalidadeTese;
        ListarAreaTematicaFilho(txtAreaTematica.SelectedValue.ToString());
        txtAreaTematicaFilho.SelectedValue = SessionTese.CdModalidadeTeseFilho;
        txtModalidadeTipo.SelectedValue = SessionTese.TpTese;
        //txtModalidade.Text = SessionTeseFilho.TpTese;
        txtTitulo.Text = SessionTese.DsAssunto;
        lblContCarct1.InnerHtml = "( " + (txtTitulo.Text.Length > 200 ? "200" : txtTitulo.Text.Length.ToString()) + " de " + txtTitulo.MaxLength.ToString() + (SessionIdioma == "PTBR" ? " caracteres )" : " )");

        txtDsTrabalho.Text = SessionTese.DsProposta;
        lblContCarct2.InnerHtml = "( " + (txtDsTrabalho.Text.Length > 1200 ? "1200" : txtDsTrabalho.Text.Length.ToString()) + " de " + txtDsTrabalho.MaxLength.ToString() + (SessionIdioma == "PTBR" ? " caracteres )" : " )");

        

        TXTDsCPF.Text = oClsFuncoes.MascaraGerar(SessionTeseRepresentante.NuCPFRepresentante, "999.999.999-99");

        txtNome.Text = SessionTeseRepresentante.NoRepresentante;
        txtSexo.Text = SessionTeseRepresentante.DsSexo;
        txtPassaporte.Text = SessionTeseRepresentante.NrPassaporte;
        txtEmail.Text = SessionTeseRepresentante.DsEmail.ToLower();
        txtPais.Text = SessionTeseRepresentante.NoPais;
        txtCEPRecibo.Text = SessionTeseRepresentante.NuCEP;
        txtEnderecoRecibo.Text = SessionTeseRepresentante.DsEndereco;
        txtComplementoEnderecoRecibo.Text = SessionTeseRepresentante.DsComplementoEndereco;
        txtBairroRecibo.Text = SessionTeseRepresentante.NoBairro;
        txtCidadeRecibo.Text = SessionTeseRepresentante.NoCidade;
        txtUF.Text = SessionTeseRepresentante.DsUF;
        txtFone.Text = oClsFuncoes.MascaraGerar(SessionTeseRepresentante.DsFone1, "(99) 999999999");
        //txtTitulacao.SelectedValue = SessionTeseRepresentante.NoTitulacao;
       // txtTitulacao.Text = SessionTeseRepresentante.NoTitulacao;
        txtInstituicaoGraduacao.Text = SessionTeseRepresentante.NoInstituicaoGraduacao;
        txtInstituicao.Text = SessionTeseRepresentante.NoInstituicao;
        txtAreaAtuacao.Text = SessionTeseRepresentante.NoAreaAtuacao;

        txtTpInstituicao.Text = SessionTeseRepresentante.TpInstituicao;
        txtNivelGoverno.Text = SessionTeseRepresentante.DsNivelGoverno;
        txtPoderGoverno.Text = SessionTeseRepresentante.DsPoderGoverno;
        txtTipoSetorPrivado.Text = SessionTeseRepresentante.DsTipoSetorPrivado;
        
        //txtModalidade.Text = SessionTese.DsModalidadeTese;

        if ((SessionTese.DsSituacao == "APROVADO") || (SessionTese.FlBancoConhecimento))
        {
            linhaTituloDocumento.Visible = true;

            if ((SessionEvento.teseConfig.DtLimiteEnvioTrabalhos >= Geral.datahoraServidor(SessionCnn)))
                linhaArquivo.Visible = true;
        }
        else
        {
            linhaTituloDocumento.Visible = false;
            linhaArquivo.Visible = false;
        }

        if (SessionTese.DsNomeArquivo != "")
        {
            lblTrabEnviado.Visible = true;
            imgTrabEnviado.Visible = true;
        }
        else
        {
            lblTrabEnviado.Visible = false;
            imgTrabEnviado.Visible = false;
        }

        if (SessionTese.FlArquivoBaixado)
        {
            lblTrabEnviado.Visible = false;
            FileUpload1.Visible = false;
            btnEnviarTrabalho.Visible = false;

            lblTrabBaixado.Visible = true;
            imgTrabBaixado.Visible = true;
        }
        else
        {
            lblTrabEnviado.Visible = true;
            FileUpload1.Visible = true;
            btnEnviarTrabalho.Visible = true;

            lblTrabBaixado.Visible = false;
            imgTrabBaixado.Visible = false;
        }

        linhaDeAcordo.Visible = false;

        if (oDTCoautores != null)
            oDTCoautores.Rows.Clear();
        carregarGrdSubscritoresTese();

    }

    protected void carregarGrdSubscritoresTese()
    {
        if (SessionTese == null)
        {
            grdSubscritor.DataSource = null;
            grdSubscritor.DataBind();

            oDTCoautores.Columns.Add("ordem");
            oDTCoautores.Columns.Add("nuCPF");
            oDTCoautores.Columns.Add("noParticipanteTese");
            oDTCoautores.Columns.Add("tpParticipacao1");
            oDTCoautores.Columns.Add("flApresentador");
            oDTCoautores.Columns.Add("cdParticipanteTese");
            

            grdSubscritor.DataSource = oDTCoautores.DefaultView;
            grdSubscritor.DataBind();
        }
        else
        {
            grdSubscritor.DataSource = null;
            grdSubscritor.DataBind();

            DataTable oDataTable = new DataTable();

            oDataTable = oTeseParticipanteCad.Listar(SessionTese.CdTese, SessionCnn);

            if (oDataTable == null)
            {
                if (oDTCoautores == null)
                    oDTCoautores = new DataTable();


                if (oDTCoautores.Columns.Count <= 0)
                {
                    oDTCoautores.Columns.Add("ordem");
                    oDTCoautores.Columns.Add("nuCPF");
                    oDTCoautores.Columns.Add("noParticipanteTese");
                    oDTCoautores.Columns.Add("tpParticipacao1");
                    oDTCoautores.Columns.Add("flApresentador");
                    oDTCoautores.Columns.Add("cdParticipanteTese");
                }

                Session["oDTCoaut"] = oDTCoautores;
                grdSubscritor.DataSource = oDTCoautores.DefaultView;
                grdSubscritor.DataBind();

                return;
            }

            if (oDTCoautores == null)
                oDTCoautores = new DataTable();


            if (oDTCoautores.Columns.Count <= 0)
            {
                oDTCoautores.Columns.Add("ordem");
                oDTCoautores.Columns.Add("nuCPF");
                oDTCoautores.Columns.Add("noParticipanteTese");
                oDTCoautores.Columns.Add("tpParticipacao1");
                oDTCoautores.Columns.Add("flApresentador");
                oDTCoautores.Columns.Add("cdParticipanteTese");
            }


            if ((oDataTable != null) && (oDataTable.DefaultView.Count >= 1))
            {

                for (int i = 0; i < oDataTable.DefaultView.Count; i++)
                {
                    oDTCoautores.Rows.Add(
                        (i + 2).ToString() + "º Autor",
                        oClsFuncoes.MascaraGerar(oDataTable.DefaultView[i]["nuCPF"].ToString(),"999.999.999-99"),
                        oDataTable.DefaultView[i]["noParticipanteTese"].ToString(),
                        oDataTable.DefaultView[i]["tpParticipacao1"].ToString(),
                        oDataTable.DefaultView[i]["flApresentador"].ToString(),
                        oDataTable.DefaultView[i]["cdParticipanteTese"].ToString()
                    );
                }
            }
        }

        Session["oDTCoaut"] = oDTCoautores;
        grdSubscritor.DataSource = oDTCoautores.DefaultView;
        grdSubscritor.DataBind();

        if (((SessionEvento.CdCliente == "0003") && (oDTCoautores.Rows.Count >= 3)) ||
            ((SessionEvento.CdCliente == "0085") && (oDTCoautores.Rows.Count >= 10)))
            btnGravarSubscritor.Visible = false;
        else
            btnGravarSubscritor.Visible = true;
    }
    protected void btnGravarSubscritor_Click(object sender, EventArgs e)
    {
        lblMsgCoautor.Text = "";
        lblMsgCoautor.Visible = false;

        lblMsgBuscaCPF2.Text = "";
        lblMsgBuscaCPF2.Visible = false;

        if (txtNoCoautor.Text == "")
        {
            lblMsgCoautor.Text = "Campo Nome do autor obrigatório";
            if (SessionIdioma == "ENUS")
                lblMsgCoautor.Text = "Name of author required";
            else if (SessionIdioma == "ESP")
                lblMsgCoautor.Text = "Nombre del autor requiere";

            lblMsgCoautor.Visible = true;
            txtNoCoautor.Focus();
            return;
        }

        if ((SessionIdioma == "PTBR") && (SessionEvento.CdCliente != "0085"))
        {
            if (txtCPFCoautor.Text == "")
            {
                lblMsgCoautor.Text = "Campo CPF do autor obrigatório";
                lblMsgCoautor.Visible = true;
                txtCPFCoautor.Focus();
                return;
            }



            if (txtCPFCoautor.Text.Replace(".", "").Replace("-", "").Length < 11)
            {
                lblMsgBuscaCPF2.Text = "CPF Inválido";
                lblMsgBuscaCPF2.Visible = true;
                txtNoCoautor.Text = "";
                txtCPFCoautor.Focus();
                return;
            }

            string cpftemp = txtCPFCoautor.Text.Replace(".", "").Replace("-", "").Trim();

            if ((!oClsFuncoes.CPFCNPJValidar(cpftemp)))
            {
                lblMsgBuscaCPF2.Text = "CPF Inválido";
                lblMsgBuscaCPF2.Visible = true;
                txtNoCoautor.Text = "";
                txtCPFCoautor.Focus();
                return;
            }


            if ((cpftemp == "11111111111") ||
                (cpftemp == "22222222222") ||
                (cpftemp == "33333333333") ||
                (cpftemp == "44444444444") ||
                (cpftemp == "55555555555") ||
                (cpftemp == "66666666666") ||
                (cpftemp == "77777777777") ||
                (cpftemp == "88888888888") ||
                (cpftemp == "99999999999") ||
                (cpftemp == "00000000000"))
            {
                lblMsgBuscaCPF2.Text = "CPF Inválido";
                lblMsgBuscaCPF2.Visible = true;
                txtNoCoautor.Text = "";
                txtCPFCoautor.Focus();
                return;
            }

            if (txtCPFCoautor.Text == TXTDsCPF.Text)
            {
                lblMsgBuscaCPF2.Text = "CPF já cadastrado como Autor deste trabalho";
                lblMsgBuscaCPF2.Visible = true;
                txtNoCoautor.Text = "";
                txtCPFCoautor.Focus();
                return;
            }

            if ((oDTCoautores != null) && (oDTCoautores.Rows.Count > 0))
            {
                for (int i = 0; i < oDTCoautores.Rows.Count; i++)
                {
                    if (txtCPFCoautor.Text.Replace(".", "").Replace("-", "").Trim() == oDTCoautores.DefaultView[i]["nuCPF"].ToString().Replace(".", "").Replace("-", "").Trim())
                    {
                        lblMsgBuscaCPF2.Text = "CPF já cadastrado como um dos autores deste trabalho";
                        lblMsgBuscaCPF2.Visible = true;
                        txtNoCoautor.Text = "";
                        txtCPFCoautor.Focus();
                        return;
                    }
                }
            }
        }
        else
        {
            if ((oDTCoautores != null) && (oDTCoautores.Rows.Count > 0))
            {
                for (int i = 0; i < oDTCoautores.Rows.Count; i++)
                {
                    if (txtNoCoautor.Text.Trim().ToUpper() == oDTCoautores.DefaultView[i]["noParticipanteTese"].ToString().Trim().ToUpper())
                    {
                        lblMsgBuscaCPF2.Text = "Nome já cadastrado como um dos autores deste trabalho";
                        if (SessionIdioma == "ENUS")
                            lblMsgBuscaCPF2.Text = "Name already registered as one of the authors of this paper";
                        else if (SessionIdioma == "ESP")
                            lblMsgBuscaCPF2.Text = "El nombre ya registrado como uno de los autores de este trabajo";

                        lblMsgBuscaCPF2.Visible = true;
                        //txtNoCoautor.Text = "";
                        txtNoCoautor.Focus();
                        return;
                    }
                }
            }
        }

        String prmCdParticipanteTese = "";
        if (txtCdTese.Text != "")
        {
            TeseParticipante oTeseParticipante = new TeseParticipante();
            TeseParticipanteCad oTeseParticipantecad = new TeseParticipanteCad();

            oTeseParticipante.CdTese = txtCdTese.Text;
            oTeseParticipante.CdParticipanteTese = "";
            oTeseParticipante.NuCPF = txtCPFCoautor.Text.Replace(".", "").Replace("-", "").Trim();
            oTeseParticipante.NoParticipanteTese = txtNoCoautor.Text;
            oTeseParticipante.TpParticipacao1 = "COAUTOR";
            oTeseParticipante.DsLotacaoAtual = "";
            oTeseParticipante.FlApresentador = chkApresentador.Checked;

            TeseParticipante tmpTeseParticipante = oTeseParticipantecad.Gravar(oTeseParticipante, SessionCnn);
            prmCdParticipanteTese = tmpTeseParticipante.CdParticipanteTese;
        }

        //DataTable dt = new DataTable();

        if (oDTCoautores.Columns.Count <= 0)
        {
            oDTCoautores.Columns.Add("ordem");
            oDTCoautores.Columns.Add("nuCPF");
            oDTCoautores.Columns.Add("noParticipanteTese");
            oDTCoautores.Columns.Add("tpParticipacao1");
            oDTCoautores.Columns.Add("flApresentador");
            oDTCoautores.Columns.Add("cdParticipanteTese");
        }

        DataRow dr = oDTCoautores.NewRow();
        dr["ordem"] = (oDTCoautores.Rows.Count + 2).ToString() + (SessionIdioma != "ENUS" ? "º Autor": "Author");
        dr["nuCPF"] = txtCPFCoautor.Text.ToUpper();
        dr["noParticipanteTese"] = txtNoCoautor.Text.ToUpper();
        dr["tpParticipacao1"] = "COAUTOR";
        dr["flApresentador"] = chkApresentador.Checked.ToString();
        dr["cdParticipanteTese"] = prmCdParticipanteTese;

        oDTCoautores.Rows.Add(dr);
        oDTCoautores.AcceptChanges();

        Session["oDTCoaut"] = oDTCoautores;

        grdSubscritor.DataSource = oDTCoautores.DefaultView;
        grdSubscritor.DataBind();        

        txtCPFCoautor.Text = "";
        txtNoCoautor.Text = "";
        chkApresentador.Checked = false;

        if (((SessionEvento.CdCliente == "0003") && (oDTCoautores.Rows.Count >= 3)) ||
            ((SessionEvento.CdCliente == "0085") && (oDTCoautores.Rows.Count >= 10)))
            btnGravarSubscritor.Visible = false;
    }
    protected void grdSubscritor_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            int index = Convert.ToInt32(e.CommandArgument);

            String tmpCdParticipanteTese = oDTCoautores.DefaultView[index]["cdParticipanteTese"].ToString();
            
            oDTCoautores.Rows.RemoveAt(index);
            Session["oDTCoaut"] = oDTCoautores;
            grdSubscritor.DeleteRow(index);
            grdSubscritor.DataSource = oDTCoautores;
            grdSubscritor.DataBind();

            if ((txtCdTese.Text != "") && (tmpCdParticipanteTese != ""))
            {
                TeseParticipanteCad oTeseParticipantecad = new TeseParticipanteCad();
                oTeseParticipantecad.ExcluirParticipanteTese(txtCdTese.Text, tmpCdParticipanteTese, SessionCnn);

                oDTCoautores.Rows.Clear();
                carregarGrdSubscritoresTese();
            }

            //if (oDTCoautores.Rows.Count < 4)
            if (((SessionEvento.CdCliente == "0003") && (oDTCoautores.Rows.Count < 4)) ||
                ((SessionEvento.CdCliente == "0085") && (oDTCoautores.Rows.Count < 11)))
                btnGravarSubscritor.Visible = true;
        }
    }

    protected void grdSubscritor_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //if (oDTCoautores.Rows.Count > e.RowIndex)
        //{
        //    oDTCoautores.Rows.RemoveAt(e.RowIndex);
        //    grdSubscritor.DeleteRow(e.RowIndex);
        //    Session["oDTCoaut"] = oDTCoautores;
        //    grdSubscritor.DataSource = oDTCoautores.DefaultView;
        //    grdSubscritor.DataBind();
        //}
    }

    //protected void btnGravarSubscritor0_Click(object sender, EventArgs e)
    //{
    //    oDTCoautores.Rows.RemoveAt(0);
    //    //grdSubscritor.DeleteRow(e.RowIndex);
    //    Session["oDTCoaut"] = oDTCoautores;
    //    grdSubscritor.DataSource = oDTCoautores.DefaultView;
    //    grdSubscritor.DataBind();

    //    if (oDTCoautores.Rows.Count < 4)
    //        btnGravarSubscritor.Visible = true;
    //}

    //protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    //{
    //    oDTCoautores.Rows.RemoveAt(0);
    //    //grdSubscritor.DeleteRow(e.RowIndex);
    //    Session["oDTCoaut"] = oDTCoautores;
    //    grdSubscritor.DataSource = oDTCoautores.DefaultView;
    //    grdSubscritor.DataBind();

    //    if (oDTCoautores.Rows.Count < 4)
    //        btnGravarSubscritor.Visible = true;
    //}
    protected void btnExcluirTodosCoautores_Click(object sender, EventArgs e)
    {
        if ((txtCdTese.Text != "") )
        {
            TeseParticipanteCad oTeseParticipantecad = new TeseParticipanteCad();
            oTeseParticipantecad.ExcluirTodosParticipanteTese(txtCdTese.Text, SessionCnn);
        }

        oDTCoautores.Rows.Clear();

        grdSubscritor.DataSource = oDTCoautores;
        grdSubscritor.DataBind();

        //if (oDTCoautores.Rows.Count < 4)
        if (((SessionEvento.CdCliente == "0003") && (oDTCoautores.Rows.Count < 4)) ||
            ((SessionEvento.CdCliente == "0085") && (oDTCoautores.Rows.Count < 11)))
            btnGravarSubscritor.Visible = true;
    }
    protected void btnGravar_Click(object sender, EventArgs e)
    {
        //if ((linhaDeAcordo.Visible) && (!chkDeAcordo.Checked))
        //{
        //    lblMsg.Text = lblMsg0.Text = "Para continuar você deve concordar com as normas do evento.";
        //    return;
        //}


        lblMsg.Text = "";
        lblMsg0.Text = "";
        lblMsgBuscaCPF.Text = "";
        //lblMsgErro.Text = "";

       
        lblMsg.Visible = lblMsg0.Visible = false;

        if ((txtAreaTematica.SelectedValue.ToString() == "") || (txtAreaTematicaFilho.SelectedValue.ToString() == "") ||
            (txtTitulo.Text == "") ||
            (txtDsTrabalho.Text == ""))
        {
            lblMsg.Visible = lblMsg0.Visible = true;

            lblMsg.Text = lblMsg0.Text = "Os campos em destaque são de caráter obrigatório!";
            if (SessionIdioma == "ENUS")
                lblMsg.Text = "The highlighted fields are mandatory";
            else if (SessionIdioma == "ESP")
                lblMsg.Text = "Los campos resaltados son obligatorios";

            return;
        }

        //if (txtDsTrabalho.Text.Length < 800)
        //{
        //    lblMsg.Text = lblMsg0.Text = "O total mínimo de caracteres da descrição do trabalho não foi atingindo!<br/>Para prosseguir deve-se ter no mínimo 800 e no máximo 1200 caracteres";
        //    lblMsg.Visible = lblMsg0.Visible = true;
        //    return;
        //}

        if (SessionIdioma == "PTBR")
        {

            string cpftemp = TXTDsCPF.Text.Replace(".", "").Replace("-", "").Trim();

            if ((!oClsFuncoes.CPFCNPJValidar(cpftemp)))
            {
                lblMsg.Text = "CPF Inválido!";
                lblMsg0.Text = lblMsg.Text;
                lblMsg.Visible = lblMsg0.Visible = true;
                return;
            }



            //string tmpCPF = oTeseCad.verificarDuplicidadeCPF(
            //    txtCdTese.Text,
            //    SessionEvento.CdEvento,
            //    TXTDsCPF.Text.Replace(".", "").Replace("-", ""),
            //     txtCdTese.Text, SessionCnn);
            //if (tmpCPF != "")
            //{
            //    lblMsg.Text = tmpCPF;
            //    lblMsg0.Text = lblMsg.Text;
            //    return;
            //}


            if ((cpftemp == "11111111111") ||
                (cpftemp == "22222222222") ||
                (cpftemp == "33333333333") ||
                (cpftemp == "44444444444") ||
                (cpftemp == "55555555555") ||
                (cpftemp == "66666666666") ||
                (cpftemp == "77777777777") ||
                (cpftemp == "88888888888") ||
                (cpftemp == "99999999999") ||
                (cpftemp == "00000000000"))
            {

                lblMsg.Text = "CPF Inválido!";
                lblMsg0.Text = lblMsg.Text;
                lblMsg.Visible = lblMsg0.Visible = true;
                return;
            }



            if ((oDTCoautores != null) && (oDTCoautores.Rows.Count > 0))
            {
                for (int i = 0; i < oDTCoautores.Rows.Count; i++)
                {
                    if (TXTDsCPF.Text.Replace(".", "").Replace("-", "").Trim() == oDTCoautores.DefaultView[i]["nuCPF"].ToString().Replace(".", "").Replace("-", "").Trim())
                    {
                        lblMsg.Text = lblMsg0.Text = lblMsgBuscaCPF.Text = "CPF já cadastrado como Coautor deste trabalho";
                        lblMsg.Visible = lblMsg0.Visible = true;
                        TXTDsCPF.Focus();
                        return;
                    }
                }
            }
        }
        //string msgArquivo = "";

        //if (!FileUpload1.HasFile)
        //{
        //    msgArquivo = "Nehum arquivo selecionado para envio.";

        //    //return;
        //}
        //else
        //{
        //    string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
        //    //FileUpload1.SaveAs(caminho + FileUpload1.FileName);
        //    if (/*(extension != ".odt") &&*/ (extension != ".doc") && (extension != ".rtf"))// && (extension != ".docx"))
        //    {
        //        lblMsg.Text = "arquivo inválido!";
        //        lblMsg0.Text = "arquivo inválido!";
        //        return;
        //    }
        //}

        bool flgEnviarEmail = false;

        Tese tmpTese = new Tese();
        if (txtCdTese.Text != "")
        {
            tmpTese = SessionTese;
        }
        else
            flgEnviarEmail = true;

        tmpTese.CdTese = txtCdTese.Text;
        tmpTese.CdTesePai = "";

        tmpTese.CdEvento = SessionEvento.CdEvento;
        tmpTese.TpTese = txtModalidadeTipo.SelectedValue.ToString();


        tmpTese.DsProposta = txtDsTrabalho.Text.Replace("'", "").Replace(";", ",");

        tmpTese.DsSituacao = "CADASTRADO";
        tmpTese.DsObservacao = "";


        tmpTese.DsAssunto = txtTitulo.Text;
        tmpTese.NuCPFAutor = TXTDsCPF.Text.Replace(".", "").Replace("-", "");
        tmpTese.DsIntroducao = "";
                
        tmpTese.DsObservacao = "";

        tmpTese.CdParticipante = SessionParticipante.CdParticipante;
        tmpTese.NoParticipante = txtNome.Text.ToUpper();
        tmpTese.DsSexo = txtSexo.Text.ToUpper();
        tmpTese.NrPassaporte = txtPassaporte.Text.ToUpper();
        tmpTese.DsEmail = txtEmail.Text.ToLower();
        tmpTese.NoPais = txtPais.Text.ToUpper();
        tmpTese.NuCEP = txtCEPRecibo.Text.Replace(".","").Replace("-","");
        tmpTese.DsEndereco = txtEnderecoRecibo.Text.ToUpper();
        tmpTese.DsComplementoEndereco = txtComplementoEnderecoRecibo.Text.ToUpper();
        tmpTese.NoBairro = txtBairroRecibo.Text.ToUpper();
        tmpTese.NoCidade = txtCidadeRecibo.Text.ToUpper();
        tmpTese.DsUF = txtUF.Text.ToUpper();
        tmpTese.DsFone1 = txtFone.Text.Replace("(", "").Replace(")", "").Replace(" ", "");
        tmpTese.NoTitulacao = SessionTeseRepresentante.NoTitulacao; 
        tmpTese.NoInstituicaoGraduacao = txtInstituicaoGraduacao.Text.ToUpper();

        tmpTese.NoInstituicao = txtInstituicao.Text.ToUpper();
        tmpTese.NoAreaAtuacao = txtAreaAtuacao.Text.ToUpper();
        tmpTese.CdModalidadeTese = txtAreaTematica.SelectedValue.ToString();

        tmpTese.CdModalidadeTeseFilho = txtAreaTematicaFilho.SelectedValue.ToString();

        tmpTese.TpInstituicao = txtTpInstituicao.Text;
        tmpTese.DsNivelGoverno = txtNivelGoverno.Text;
        tmpTese.DsPoderGoverno = txtPoderGoverno.Text;
        tmpTese.DsTipoSetorPrivado = txtTipoSetorPrivado.Text;


        SessionTese = oTeseCad.Gravar(SessionEvento, tmpTese, oDTCoautores, "000000001", SessionCnn);


        if (SessionTese != null)
        {
            //oTeseCad.ValidarPainel(SessionEvento.CdEvento, SessionTese, SessionTeseRepresentante, SessionCnn);

            if (txtCdTese.Text == "")
            {
                oTeseRepresentanteCad.vincularTeseRepresentante(SessionTese.CdTese, SessionTeseRepresentante.CdRepresentante, SessionCnn);
            }

            btnExcluirProposta.Visible = true;

            linhaDeAcordo.Visible = false;

            txtCdTese.Text = SessionTese.CdTese;

            Pesquisar(txtCdTese.Text);

            lblMsg.Text = "Sua proposta de trabalho foi gravada com sucesso";
            if (SessionIdioma == "ENUS")
                lblMsg.Text = "His paper was successfully recorded";
            else if (SessionIdioma == "ESP")
                lblMsg.Text = "Su propuesta de trabajo se registró con éxito";

            //msgArquivo = enviarArquivo(SessionTeseFilho);
            ////if (!enviarArquivo(SessionTese))
            ////    return;


            //if (msgArquivo == "")
            //{
            //    Geral oGeral = new Geral();
            //    //oGeral.EnviarEmailCadastroTese(SessionEvento, SessionParticipante, SessionTeseFilho, SessionCnn);
            //    lblMsg.Text += " com sucesso!<br/>Sua proposta de Trabalho Técnico está em nossa base de dados. Aguarde a avaliação pela equipe técnica.";
            //}
            //else
            //    lblMsg.Text += ", porém " + msgArquivo;

            lblMsg0.Text = lblMsg.Text;
            lblMsg.Visible = lblMsg0.Visible = true;

            Geral oGeral = new Geral();

            if (flgEnviarEmail)
            {
                //enviar email
                
                oGeral.EnviarEmailCadastroTrabalho(SessionEvento, SessionTeseRepresentante, SessionTese, SessionIdioma, SessionCnn);
            }

            TeseExaminadorCad oTeseExaminadorCad = new TeseExaminadorCad();
            TeseExaminador tmpTeseExaminador = oTeseCad.BuscarExaminadorParaTese(SessionTese, SessionCnn);

            if ((tmpTeseExaminador != null) && (flgEnviarEmail))
            {
                if (oTeseExaminadorCad.VincularExaminadorTese(SessionTese.CdTese, tmpTeseExaminador.CdExaminador, SessionCnn) != null)
                    oGeral.EnviarEmailExaminadorTeseCadastrada(SessionEvento, tmpTeseExaminador, SessionTese, SessionCnn);
            }

            Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}",
                               "038"), true);
        }
        else
        {
            lblMsg.Text = oTeseCad.RcMsg;
            lblMsg0.Text = oTeseCad.RcMsg;
            lblMsg.Visible = lblMsg0.Visible = true;
        }
    }

    protected string  enviarArquivo(Tese prmTese)
    {
        string retorno = "";
        if (prmTese == null)
        {
            //lblMsg.Text = "Tese inválida";
            retorno = "trabalho nulo";
            if (SessionIdioma == "ENUS")
                retorno = "null paper";
            else if (SessionIdioma == "ESP")
                retorno = "trabajo nulo";

            return retorno;
        }

        string sURL = Request.Url.ToString().ToLower();
        string caminho = "";
        if (sURL.Contains("localhost"))
        {
            caminho = Server.MapPath("/inscricoesTrabalhos");
            caminho += "\\trabalhos\\" + SessionEvento.CdEvento + "\\";
        }
        else
        {
            caminho = Server.MapPath("").Replace("\\inscricoestrabalhos", "");
            caminho += "\\trabalhos\\" + SessionEvento.CdEvento + "\\";
            //lblMsg.Text = caminho;
            //return false;
        }

        if (!Directory.Exists(caminho))
        {
            Directory.CreateDirectory(caminho);
        }

        if (FileUpload1.HasFile)
        {
            try
            {
                string extension = Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower();
                //FileUpload1.SaveAs(caminho + FileUpload1.FileName);
                if ((extension != ".odt") && (extension != ".doc") && (extension != ".rtf") && (extension != ".docx") && (extension != ".pdf") && (extension != ".jpg") && (extension != ".png") && 
                    (extension != ".odp") && (extension != ".ppt") && (extension != ".pptx"))
                {
                    //lblMsg.Text = "Arquivo inválido!";
                    //lblMsg0.Text = "Arquivo inválido!";
                    retorno = "arquivo inválido!";
                    if (SessionIdioma == "ENUS")
                        retorno = "invalid file";
                    else if (SessionIdioma == "ESP")
                        retorno = "archivo no válido";

                    return retorno;
                }

                //System.Drawing.Image UploadedImage = System.Drawing.Image.FromStream(FileUpload1.PostedFile.InputStream);

                //Label1.Text = "Height: " + UploadedImage.Height.ToString() + " / Width: " + UploadedImage.Width.ToString() + " / size" + FileUpload1.PostedFile.ContentLength.ToString();


                File.Delete(caminho + "trab_" + prmTese.CdTese + "_CPF_" + prmTese.NuCPFAutor + "_" + prmTese.NoParticipante + ".odt");//word do brOffice
                File.Delete(caminho + "trab_" + prmTese.CdTese + "_CPF_" + prmTese.NuCPFAutor + "_" + prmTese.NoParticipante + ".doc");
                File.Delete(caminho + "trab_" + prmTese.CdTese + "_CPF_" + prmTese.NuCPFAutor + "_" + prmTese.NoParticipante + ".rtf");
                File.Delete(caminho + "trab_" + prmTese.CdTese + "_CPF_" + prmTese.NuCPFAutor + "_" + prmTese.NoParticipante + ".docx");


                File.Delete(caminho + "trab_" + prmTese.CdTese + "_CPF_" + prmTese.NuCPFAutor + "_" + prmTese.NoParticipante + ".odp");//power point do brOffice
                File.Delete(caminho + "trab_" + prmTese.CdTese + "_CPF_" + prmTese.NuCPFAutor + "_" + prmTese.NoParticipante + ".ppt");
                File.Delete(caminho + "trab_" + prmTese.CdTese + "_CPF_" + prmTese.NuCPFAutor + "_" + prmTese.NoParticipante + ".pptx");

                File.Delete(caminho + "trab_" + prmTese.CdTese + "_CPF_" + prmTese.NuCPFAutor + "_" + prmTese.NoParticipante + ".pdf");

                File.Delete(caminho + "trab_" + prmTese.CdTese + "_CPF_" + prmTese.NuCPFAutor + "_" + prmTese.NoParticipante + ".png");
                File.Delete(caminho + "trab_" + prmTese.CdTese + "_CPF_" + prmTese.NuCPFAutor + "_" + prmTese.NoParticipante + ".jpg");

                FileUpload1.SaveAs(caminho + "trab_" + prmTese.CdTese + "_CPF_" + prmTese.NuCPFAutor + "_" + prmTese.NoParticipante +  extension);

                SessionTese.DsNomeArquivo = "trab_" + prmTese.CdTese + "_CPF_" + prmTese.NuCPFAutor + "_" + prmTese.NoParticipante +  extension;
                SessionTese = oTeseCad.Gravar(SessionTese, "000000001", SessionCnn);

                Pesquisar(prmTese.CdTese);

                //Label1.Text = "Operação realizada com sucesso!";// string.Format("File: {0} {1} kb - Content Type {2} ",
                //FileUpload1.PostedFile.FileName, FileUpload1.PostedFile.ContentLength,
                //FileUpload1.PostedFile.ContentType);

                return "";// true;

                //lblMsg.Text = caminho + "trab_" + prmTese.CdTese + "_CPF_" + prmTese.NuCPFAutor + "_" + prmTese.NoParticipante + extension;
                //return false;

                
            }
            catch (Exception ex)
            {
                //lblMsg.Text = "ERRO: " + ex.Message.ToString();
                //lblMsg0.Text = "ERRO: " + ex.Message.ToString();

                retorno = "Ocorreu ERRO AO ENVIAR DOCUMENTO: " + ex.Message.ToString(); //false;
                if (SessionIdioma == "ENUS")
                    retorno = "Error occurred SEND DOCUMENT";
                else if (SessionIdioma == "ESP")
                    retorno = "Un error ocurrió ENVIAR DOCUMENTO";

                return retorno;
            }
        }
        else
        {
            //lblMsg.Text = "Você deve escolher um arquivo para o upload.";
            //lblMsg0.Text = "Você deve escolher um arquivo para o upload.";

            retorno = "Nenhum arquivo foi selecionado para envio.";
            if (SessionIdioma == "ENUS")
                retorno = "No file selected to upload.";
            else if (SessionIdioma == "ESP")
                retorno = "Ningún archivo seleccionado para la carga.";

            return retorno;
        }

    }

    protected void btnNovo_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblMsg0.Text = "";

        lblMsg.Visible = lblMsg0.Visible = false;

        txtCdTese.Text = "";

        lblSituacao.Text = "";

        txtTitulo.Text = "";
        //TXTDsCPF.Text = "";

        lblContCarct1.InnerHtml = "( " + txtTitulo.Text.Length.ToString() + " de " + txtTitulo.MaxLength.ToString() + (SessionIdioma == "PTBR" ? " caracteres )" : " )");

        lblContCarct2.InnerHtml = "( " + txtDsTrabalho.Text.Length.ToString() + " de " + txtDsTrabalho.MaxLength.ToString() + (SessionIdioma == "PTBR" ? " caracteres )" : " )");

        txtDsTrabalho.Text = "";

        //txtNome.Text = "";
        //txtSexo.Text  = "";
        //txtPassaporte.Text = "";
        //txtEmail.Text = "";
        //txtPais.Text = "";
        //txtCEPRecibo.Text = "";
        //txtEnderecoRecibo.Text = "";
        //txtComplementoEnderecoRecibo.Text = "";
        //txtBairroRecibo.Text = "";
        //txtCidadeRecibo.Text = "";
        //txtUF.Text = "";
        //txtFone.Text = "";
        //txtTitulacao.Text = "";
        //txtInstituicao.Text = "";
        //txtArea.Text = "";
        //txtModalidade.Text = "";

        //linhaDeAcordo.Visible = true;
        chkDeAcordo.Checked = false;
        

        SessionTese = null;

        oDTCoautores.Rows.Clear();

        grdSubscritor.DataSource = oDTCoautores;
        grdSubscritor.DataBind();

        chkCPF.Checked = true;

        TXTDsCPF.Focus();

        lblMsgBuscaCPF.Text = "";
        lblMsgBuscaCPF.Visible = false;

        lblMsgBuscaCPF2.Text = "";
        lblMsgBuscaCPF2.Visible = false;

        lblMsg.Text = "";
        lblMsg0.Text = "";
    }
    protected void btnExcluirProposta_Click(object sender, EventArgs e)
    {
        if (txtCdTese.Text == "")
            return;

        oTeseRepresentanteCad.desvincularTeseRepresentante(txtCdTese.Text, SessionTeseRepresentante.CdRepresentante, SessionCnn);

        if (oTeseCad.ExcluirTese(txtCdTese.Text, SessionCnn))
        {

            string sURL = Request.Url.ToString().ToLower();
            string caminho = "";
            if (sURL.Contains("localhost"))
            {
                caminho = Server.MapPath("/inscricoesWeb");
                caminho += "\\trabalhos\\" + SessionEvento.CdEvento + "\\";
            }
            else
            {
                caminho = Server.MapPath("").Replace("\\inscricoesweb", "");
                caminho += "\\trabalhos\\" + SessionEvento.CdEvento + "\\";
                //lblMsg.Text = caminho;
                //return false;
            }
                       
        
            try
            {


                File.Delete(caminho + "trab_" + SessionTese.CdTese + "_CPF_" + SessionTese.NuCPFAutor + "_" + SessionTese.NoParticipante + ".odt");
                File.Delete(caminho + "trab_" + SessionTese.CdTese + "_CPF_" + SessionTese.NuCPFAutor + "_" + SessionTese.NoParticipante + ".doc");
                File.Delete(caminho + "trab_" + SessionTese.CdTese + "_CPF_" + SessionTese.NuCPFAutor + "_" + SessionTese.NoParticipante + ".rtf");
                File.Delete(caminho + "trab_" + SessionTese.CdTese + "_CPF_" + SessionTese.NuCPFAutor + "_" + SessionTese.NoParticipante + ".docx");
                File.Delete(caminho + "trab_" + SessionTese.CdTese + "_CPF_" + SessionTese.NuCPFAutor + "_" + SessionTese.NoParticipante + ".pdf");
                File.Delete(caminho + "trab_" + SessionTese.CdTese + "_CPF_" + SessionTese.NuCPFAutor + "_" + SessionTese.NoParticipante + ".png");
                File.Delete(caminho + "trab_" + SessionTese.CdTese + "_CPF_" + SessionTese.NuCPFAutor + "_" + SessionTese.NoParticipante + ".jpg");
                
            }
            catch (Exception ex)
            {
                //lblMsg.Text = "ERRO: " + ex.Message.ToString();
                //lblMsg0.Text = "ERRO: " + ex.Message.ToString();

                //return "ocorreu ERRO AO ENVIAR DOCUMENTO: " + ex.Message.ToString(); //false;
            }


            btnNovo_Click(sender, e);
            lblMsg.Text = "Operação realizada com sucesso!";//<br/>Foi Enviado um e-mail com a senha para o patrocinador.";
            if (SessionIdioma == "ENUS")
                lblMsg.Text = "Successful transaction";
            if (SessionIdioma == "ESP")
                lblMsg.Text = "Operación realizada con éxito";





            lblMsg0.Text = lblMsg.Text;

            //DataTable oDT = oTeseCad.ListarTesesFilho(SessionTeseRepresentante.CdEvento, SessionTese.CdTese, SessionCnn);
            //if ((oDT != null) && (oDT.Rows.Count == 3))
            //    btnNovo.Visible = false;
            //else
            //    btnNovo.Visible = true;

            //oTeseCad.ValidarPainel(SessionEvento.CdEvento, SessionTese, SessionTeseRepresentante, SessionCnn);
        }
        else
        {
            lblMsg.Text = oTeseCad.RcMsg;
            lblMsg0.Text = lblMsg.Text;
        }
    }

    protected void txtCPFCoautor_TextChanged(object sender, EventArgs e)
    {
        lblMsgBuscaCPF2.Text = "";
        lblMsgBuscaCPF2.Visible = false;
        if (txtCPFCoautor.Text.Replace(".", "").Replace("-", "") == "")
            return;

        if (txtCPFCoautor.Text.Replace(".", "").Replace("-", "").Length < 11)
        {
            lblMsgBuscaCPF2.Text = "CPF Inválido";
            lblMsgBuscaCPF2.Visible = true;
            txtNoCoautor.Text = "";
            return;
        }
                
        string cpftemp = txtCPFCoautor.Text.Replace(".", "").Replace("-", "").Trim();

        if ((!oClsFuncoes.CPFCNPJValidar(cpftemp)))
        {
            lblMsgBuscaCPF2.Text = "CPF Inválido";
            lblMsgBuscaCPF2.Visible = true;
            txtNoCoautor.Text = "";
            return;
        }       


        if ((cpftemp == "11111111111") ||
            (cpftemp == "22222222222") ||
            (cpftemp == "33333333333") ||
            (cpftemp == "44444444444") ||
            (cpftemp == "55555555555") ||
            (cpftemp == "66666666666") ||
            (cpftemp == "77777777777") ||
            (cpftemp == "88888888888") ||
            (cpftemp == "99999999999") ||
            (cpftemp == "00000000000"))
        {
            lblMsgBuscaCPF2.Text = "CPF Inválido";
            lblMsgBuscaCPF2.Visible = true;
            txtNoCoautor.Text = "";
            return;
        }

        if (txtCPFCoautor.Text == TXTDsCPF.Text)
        {
            lblMsgBuscaCPF2.Text = "CPF já cadastrado como Autor deste trabalho";
            lblMsgBuscaCPF2.Visible = true;
            txtNoCoautor.Text = "";
            return;
        }

        if ((oDTCoautores != null) && (oDTCoautores.Rows.Count > 0))
        {
            for (int i = 0; i < oDTCoautores.Rows.Count; i++)
            {
                if (txtCPFCoautor.Text.Replace(".", "").Replace("-", "").Trim() == oDTCoautores.DefaultView[i]["nuCPF"].ToString().Replace(".", "").Replace("-", "").Trim())
                {
                    lblMsgBuscaCPF2.Text = "CPF já cadastrado como Coautor deste trabalho";
                    lblMsgBuscaCPF2.Visible = true;
                    txtNoCoautor.Text = "";
                    return;
                }
            }
        }

        //PESQUISAR CPF BANCO LOCAL
        ParticipanteCad oParticipanteCad = new ParticipanteCad();
        SqlConnection SessionCnnHISTORICO = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOWJYTnpjV3d1Wm1GNlpXNWtiMjFoYVhNdVkyOXRPMGx1YVhScFlXd2dRMkYwWVd4dlp6MW1ZWHBsYm1SdmJXRnBjekU3VUdWeWMybHpkQ0JUWldOMWNtbDBlU0JKYm1adlBWUnlkV1U3VlhObGNpQkpSRDFtWVhwbGJtUnZiV0ZwY3pFN1VHRnpjM2R2Y21ROVVYVTBOVFJ5Um0wPQ==")));
        DataTable DTCpf = oParticipanteCad.PesquisaCPF(cpftemp, SessionCnnHISTORICO);
        if ((DTCpf != null) && (DTCpf.Rows.Count > 0))
        {
            txtNoCoautor.Text = DTCpf.DefaultView[0]["Nome"].ToString();
            return;
        }
        else
        {
            /*
            //PESQUISAR CPF BANCO RECEITA
            int tmpSaldoPesqCPF = Geral.verificarTotalPesquisaCPF(SessionCnn);

            if (tmpSaldoPesqCPF > 0)
            {

                DataSet ds = new DataSet();
                ds.ReadXml("http://ws.fontededados.com.br/consulta.asmx/SituacaoCadastralPF?login=" +
                    cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("Wm1GNlpXNWtiMjFoYVhNPQ==")) +
                    "&senha=" + cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("Um0xQWFUVXpORGcy")) +
                    "&cpf=" + cpftemp.Replace("-", ""));

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["MensagemErro"].ToString() != "")
                        {                            
                            if (ds.Tables[0].Rows[0]["ValorCobrado"].ToString() != "0")
                                Geral.DecrementarPesquisaCPFReceita(SessionCnn);

                            if ((ds.Tables[0].Rows[0]["ValorCobrado"].ToString() == "8") ||
                                (ds.Tables[0].Rows[0]["ValorCobrado"].ToString() == "99"))
                            {
                                lblMsgBuscaCPF2.Text = ds.Tables[0].Rows[0]["MensagemErro"].ToString();
                                return;
                            }

                            //lblMsgBuscaCPF2.Text = ds.Tables[0].Rows[0]["MensagemErro"].ToString();
                            //txtNoCoautor.Text = "";
                            //return;
                        }
                        else
                        {
                            oParticipanteCad.IncluirCPF(ds.Tables[0].Rows[0]["Cpf"].ToString(), ds.Tables[0].Rows[0]["Nome"].ToString(), SessionCnnHISTORICO);
                            Geral.DecrementarPesquisaCPFReceita(SessionCnn);

                            txtNoCoautor.Text = ds.Tables[0].Rows[0]["Nome"].ToString();
                            return;
                        }
                    }
                }
            }
            */

        }





        //if (tmpreturnoCPF.DefaultView[0]["tpRetorno"].ToString() != "Ok")
        //{
        //    lblMsgBuscaCPF2.Text = tmpreturnoCPF.DefaultView[0]["MsgRetorno"].ToString();
        //    txtNome.Text = "";
        //    return;
        //}

        //if (txtNoCoautor.Text != "")
        //    return;

        //txtNoCoautor.Text = tmpreturnoCPF.DefaultView[0]["dsNome"].ToString();

        txtNoCoautor.Focus();
    }

    protected void txtTpInstituicao_TextChanged(object sender, EventArgs e)
    {
        //if (txtTpInstituicao.Text == "SIM")
        //{
        //    linhaNivelGeverno.Visible = true;
        //    linhaPoderGoverno.Visible = true;
        //    linhaTipoSetorPrivado.Visible = false;
        //}
        //else if (txtTpInstituicao.Text == "NÃO")
        //{
        //    linhaNivelGeverno.Visible = false;
        //    linhaPoderGoverno.Visible = false;
        //    linhaTipoSetorPrivado.Visible = true;
        //}
        //else
        //{
        //    linhaNivelGeverno.Visible = false;
        //    linhaPoderGoverno.Visible = false;
        //    linhaTipoSetorPrivado.Visible = false;
        //}
    }
    protected void btnEnviarTrabalho_Click(object sender, EventArgs e)
    {
        //Pesquisar(txtCdTese.Text);

        string msgArquivo = enviarArquivo(SessionTese);


        if (msgArquivo != "")
        {
            lblMsg.Text += msgArquivo;
            lblMsg0.Text = lblMsg.Text;
            lblMsg.Visible = lblMsg0.Visible = true;
            return;
        }

        if (SessionTese.DsNomeArquivo != "")
        {
            lblTrabEnviado.Visible = true;
            imgTrabEnviado.Visible = true;
        }
        else
        {
            lblTrabEnviado.Visible = false;
            imgTrabEnviado.Visible = false;
        }

        lblMsg.Text = "Seu trabalho foi gravado com sucesso";

        if (SessionIdioma == "ENUS")
            lblMsg.Text = "His paper has been successfully saved";
        if (SessionIdioma == "ESP")
            lblMsg.Text = "Su trabajo ha sido guardado con éxito";

        lblMsg0.Text = lblMsg.Text;
        lblMsg.Visible = lblMsg0.Visible = true;
    }
    protected void txtAreaTematica_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListarAreaTematicaFilho(txtAreaTematica.SelectedValue.ToString());
    }
    protected void grdSubscritor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {


            if (SessionIdioma == "PTBR")
            {
                e.Row.Cells[0].Text = "";
                e.Row.Cells[1].Text = "CPF";
                e.Row.Cells[2].Text = "Nome";
                //e.Row.Cells[3].Text = "Remover";
                //e.Row.Cells[4].Text = "Situação";
            }
            else if (SessionIdioma == "ENUS")
            {
                e.Row.Cells[0].Text = "";
                e.Row.Cells[1].Text = "CPF";
                e.Row.Cells[2].Text = "Name";
            }
            else if (SessionIdioma == "ESP")
            {
                e.Row.Cells[0].Text = "";
                e.Row.Cells[1].Text = "CPF";
                e.Row.Cells[2].Text = "Nombre";
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
    }
}

