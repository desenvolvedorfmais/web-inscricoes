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

using System.Text.RegularExpressions;



public partial class frmMeetingConvitesRecebidos : System.Web.UI.Page
{
    Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    SqlConnection SessionCnn;
    
    String SessionIdioma;

    MeetingConviteCad oMeetingConviteCad = new MeetingConviteCad();

    DataTable oDT = new DataTable();
    
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
                lblCategoria.Text = SessionParticipante.Categoria.NoCategoria.Trim();
            else if (SessionIdioma == "ENUS")
                lblCategoria.Text = SessionParticipante.Categoria.NoCategoriaIngles.Trim();
            else if (SessionIdioma == "ESP")
                lblCategoria.Text = SessionParticipante.Categoria.NoCategoriaEspanhol.Trim();
            else if (SessionIdioma == "FRA")
                lblCategoria.Text = SessionParticipante.Categoria.NoCategoriaFrances.Trim();

            ListarConvites();

            ListarPaises();
            ListarAreaAtuacao();

            //txtPerfilB2B.Text = (SessionParticipante.meetingParticipante == null ? "" : SessionParticipante.meetingParticipante.DsPerfilEmpresaHTML);
            //lblEmpresa.Text = (SessionParticipante.meetingParticipante == null ? "" : SessionParticipante.meetingParticipante.NoInstituicao);
            //lblNoPais.Text = (SessionParticipante.meetingParticipante == null ? "" : SessionParticipante.meetingParticipante.NoPais);
            //lblAreaAtuacao.Text = (SessionParticipante.meetingParticipante == null ? "" : SessionParticipante.meetingParticipante.NoAreaAtuacao);
            //lblWebSite.Text = (SessionParticipante.meetingParticipante == null ? "" : SessionParticipante.meetingParticipante.DsWebsite);
            //lblNoCargo.Text = (SessionParticipante.meetingParticipante == null ? "" : SessionParticipante.meetingParticipante.NoCargo);

        }
        else
        {
            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            SessionIdioma = (String)Session["SessionIdioma"];

            if (SessionEvento == null)
                Server.Transfer("frmSessaoExpirada.aspx", true);
            ListarConvites();
        }

        verificarIdioma(SessionIdioma);

        
    }

    protected void verificarIdioma(string prmIdioma)
    {
        if (prmIdioma == "PTBR")
        {
            SessionIdioma = "PTBR";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Convites Recebidos";

            lblID.Text = "Nr Cadastro:";
            lblPart.Text = "Participante:";
            lblCateg.Text = "Categoria:";


            lblFiltro.Text = "Filtros:";
            lblEmpresFiltro.Text = "Intituição/Empresa:";
            lblPaisFiltro.Text = "País";
            lblAreaAtuacaoFiltro.Text = "Área Atuação";

            btnFiltrar.Text = "Filtrar";
            btnLimparFiltro.Text = "Limpar Filtro";

            //lblInstrucoes.Text = "Informe mais detalhes sobre o perfil de negócios de sua empresa:";

            //btnGravar.Text = "Gravar";

            
        }
        else if (prmIdioma == "ENUS")
        {
            SessionIdioma = "ENUS";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Invitations Received";

            lblID.Text = "Registration no.";
            lblPart.Text = "Participant:";
            lblCateg.Text = "Category:";

            lblFiltro.Text = "Filters:";
            lblEmpresFiltro.Text = "Institution / Company:";
            lblPaisFiltro.Text = "Country:";
            lblAreaAtuacaoFiltro.Text = "Main Practice Areas:";


            chkEmAberto.Text = "Waiting";
            chkAceito.Text = "Accepted";
            chkAgendado.Text = "Scheduled";
            chkCancelado.Text = "Canceled";
            chkRecusado.Text = "Declined";

            btnFiltrar.Text = "Go";
            btnLimparFiltro.Text = "Clear filter";

            //lblInstrucoes.Text = "Tell more details about your company's business profile:";

            

            //btnGravar.Text = "Save";


            

        }
        else if (prmIdioma == "ESP")
        {
            SessionIdioma = "ESP";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Datos de la Empresa";

            lblID.Text = "Registro nr:";
            lblPart.Text = "Participante:";
            lblCateg.Text = "Categoría";

            //lblInstrucoes.Text = "Introduzca el perfil de negocio de su empresa:";

            //btnGravar.Text = "Cambiar contraseña";

            
        }
        else if (prmIdioma == "FRA")
        {
            SessionIdioma = "FRA";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Profil de la Société";

            lblPart.Text = "Participant:";
            lblCateg.Text = "Catégorie:";
            lblID.Text = "N° d'enregistrement:";

            //lblInstrucoes.Text = "Entrez le profil d'entreprise de votre entreprise:";

            //btnGravar.Text = "Confirmer";

            
        }
    }

    protected void ListarConvites()
    {
        grdConvites.DataSource = null;
        grdConvites.DataBind();


        oDT = oMeetingConviteCad.ListarRecebidos(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);

        if (oDT == null)
        {
            return;
        }

        oDT.DefaultView.Sort = "cdConvite desc";
        oDT.DefaultView.RowFilter = filtros();

        grdConvites.DataSource = oDT;
        grdConvites.DataBind();

    }
    
    protected string filtros()
    {
        string retorno = "";

        if (txtEmpresaFiltro.Text.Trim() != "")
            retorno = "noInstituicaoParticipanteConvite like '%" + txtEmpresaFiltro.Text + "%' ";

        if (txtPaisFiltro.Text.Trim() != "")
        {
            if (retorno == "")
                retorno = "noPaisParticipanteConvite like '%" + txtPaisFiltro.Text + "%' ";
            else
                retorno += " and noPaisParticipanteConvite like '%" + txtPaisFiltro.Text + "%' ";
        }

        if (txtAreaAtuacaoFiltro.Text.Trim() != "")
        {
            if (retorno == "")
                retorno = "noAreaAtuacaoParticipanteConvite like '%" + txtAreaAtuacaoFiltro.Text + "%' ";
            else
                retorno += " and noAreaAtuacaoParticipanteConvite like '%" + txtAreaAtuacaoFiltro.Text + "%' ";
        }

        if ((chkEmAberto.Checked) || (chkAceito.Checked) || (chkAgendado.Checked) || (chkCancelado.Checked) || (chkRecusado.Checked))
        {
            string situacao = retorno == "" ? " ( " : " and ( ";
            string tmpSituacao = "";

            if (chkEmAberto.Checked)
                tmpSituacao += " dsStatusConvite = 'EM ABERTO' ";

            if (chkAceito.Checked)
                tmpSituacao += (tmpSituacao != "" ? " OR " : "") + " dsStatusConvite = 'ACEITO' ";

            if (chkAgendado.Checked)
                tmpSituacao += (tmpSituacao != "" ? " OR " : "") + " dsStatusConvite = 'AGENDADO' ";

            if (chkCancelado.Checked)
                tmpSituacao += (tmpSituacao != "" ? " OR " : "") + " dsStatusConvite = 'CANCELADO' ";

            if (chkRecusado.Checked)
                tmpSituacao += (tmpSituacao != "" ? " OR " : "") + " dsStatusConvite = 'RECUSADO' ";

            situacao += tmpSituacao + "  ) ";

            retorno += situacao;
        }

        return retorno;
    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblCdConvite = (Label)e.Row.FindControl("lblCdConvite");
            
            Label lbldtconvite = (Label)e.Row.FindControl("lblDtConvite");
            lbldtconvite.Text = DateTime.Parse(lbldtconvite.Text).ToString("dd/MM/yyyy");            

            Button btndetalhe = (Button)e.Row.FindControl("btnMaisInf");
            btndetalhe.PostBackUrl = "~/frmMeetingConviteRec.aspx?cdConviteRec=" + lblCdConvite.Text;

            Label lblStatusConvite = (Label)e.Row.FindControl("lblStatusConvite");
            if (lblStatusConvite.Text.ToUpper() == "EM ABERTO")
            {
                lblStatusConvite.BackColor = System.Drawing.Color.Yellow;
                lblStatusConvite.ForeColor = System.Drawing.Color.Black;
            }
            else if (lblStatusConvite.Text.ToUpper() == "ACEITO")
            {
                lblStatusConvite.BackColor = System.Drawing.Color.Green;
                lblStatusConvite.ForeColor = System.Drawing.Color.White;
            }
            else if ((lblStatusConvite.Text.ToUpper() == "CANCELADO") || (lblStatusConvite.Text.ToUpper() == "RECUSADO"))
            {
                lblStatusConvite.BackColor = System.Drawing.Color.Red;
                lblStatusConvite.ForeColor = System.Drawing.Color.White;
            }
            else if (lblStatusConvite.Text.ToUpper() == "AGENDADO")
            {
                lblStatusConvite.BackColor = System.Drawing.Color.Blue;
                lblStatusConvite.ForeColor = System.Drawing.Color.White;
            }

            if (SessionIdioma != "PTBR")
            {

                Label Empresa = (Label)e.Row.FindControl("Empresa");
                Label Pais = (Label)e.Row.FindControl("Pais");
                Label AreaAtuacao = (Label)e.Row.FindControl("AreaAtuacao");
                Label Representante = (Label)e.Row.FindControl("Representante");

                Label DtConvite = (Label)e.Row.FindControl("DtConvite");
                Label StatusConvite = (Label)e.Row.FindControl("StatusConvite");

                Empresa.Text = "Institution / Company:";
                Pais.Text = "Country:";
                AreaAtuacao.Text = "Main Practice Areas:";
                Representante.Text = "Representative:";

                DtConvite.Text = "Dt Invitation:";
                StatusConvite.Text = "Status Invitation:";

                btndetalhe.Text = "Detail";

                lbldtconvite.Text = DateTime.Parse(lbldtconvite.Text).ToString("MM/dd/yyyy");  

                if (lblStatusConvite.Text.ToUpper() == "EM ABERTO")
                {
                    lblStatusConvite.Text = "WAITING";
                    lblStatusConvite.BackColor = System.Drawing.Color.Yellow;
                    lblStatusConvite.ForeColor = System.Drawing.Color.Black;
                }
                else if (lblStatusConvite.Text.ToUpper() == "ACEITO")
                {
                    lblStatusConvite.Text = "ACCEPTED";
                    lblStatusConvite.BackColor = System.Drawing.Color.Green;
                    lblStatusConvite.ForeColor = System.Drawing.Color.White;
                }
                else if ((lblStatusConvite.Text.ToUpper() == "CANCELADO") || (lblStatusConvite.Text.ToUpper() == "RECUSADO"))
                {
                    lblStatusConvite.Text = "CANCELED";
                    if (lblStatusConvite.Text.ToUpper() == "RECUSADO")
                        lblStatusConvite.Text = "DECLINED";

                    lblStatusConvite.BackColor = System.Drawing.Color.Red;
                    lblStatusConvite.ForeColor = System.Drawing.Color.White;
                }
                else if (lblStatusConvite.Text.ToUpper() == "AGENDADO")
                {
                    lblStatusConvite.Text = "SCHEDULED";
                    lblStatusConvite.BackColor = System.Drawing.Color.Blue;
                    lblStatusConvite.ForeColor = System.Drawing.Color.White;
                }
            }  
            
        }
    }
    protected void btnFiltrar_Click(object sender, EventArgs e)
    {

    }
    protected void btnLimparFiltro_Click(object sender, EventArgs e)
    {
        txtEmpresaFiltro.Text = "";
        txtPaisFiltro.Text = "";
        txtAreaAtuacaoFiltro.Text = "";
        chkAgendado.Checked = chkEmAberto.Checked = chkAceito.Checked = true;
        chkCancelado.Checked = chkRecusado.Checked = false;

        ListarConvites();
    }
    protected void btnNovoConvite_Click(object sender, EventArgs e)
    {

    }

    public void ListarPaises()
    {
        txtMsg.Visible = false;
        if (SessionCnn == null)
        {
            //_erroNoCampo = true;
            txtMsg.Visible = true;
            txtMsg.Text = "Conexão inválida ou inexistente";
            return;
        }
        if (SessionCnn.State != ConnectionState.Open)
        {
            try
            {
                SessionCnn.Open();
            }
            catch
            {
                //_erroNoCampo = true;
                txtMsg.Visible = true;
                txtMsg.Text = "Conexão inválida";
                return;
            }
        }

        DataTable DT = new DataTable();
        SqlDataAdapter Dap;
        try
        {
            try
            {
                string cmd = "SELECT distinct " +
                      "cdPais, " +
                      "dbo.TIRA_ACENTO(dsPais) as dsPais, " +
                      "dbo.TIRA_ACENTO(dsPaisIngles) as dsPaisIngles, " +
                      "dbo.TIRA_ACENTO(dsPaisEspanhol) as dsPaisEspanhol, " +
                      "dbo.TIRA_ACENTO(dsPaisFrances) as dsPaisFrances " +
                    "FROM " +
                      " dbo.tbMeetingParticipantes pm " +
                    "join dbo.tbPaises p on dbo.TIRA_ACENTO(p.dsPais) = dbo.TIRA_ACENTO(pm.noPais) " +
                    "ORDER BY ";

                cmd += SessionIdioma == "PTBR" ? "2" : SessionIdioma == "ENUS" ? "3" : SessionIdioma == "ESP" ? "4" : "5";

                SqlCommand comando = new SqlCommand(
                     cmd, SessionCnn);


                Dap = new SqlDataAdapter(comando);

                Dap.TableMappings.Add("PAISES", "tbPaises");
                Dap.Fill(DT);

                SessionCnn.Close();

                //BindingSource bsufs = new BindingSource();

                DataTable oDataTable = new DataTable();



                oDataTable.Columns.Add("dsPais");
                oDataTable.Columns.Add("dsPaisIngles");
                oDataTable.Columns.Add("dsPaisEspanhol");
                oDataTable.Columns.Add("dsPaisFrances");

                oDataTable.Rows.Add("", "", "", "");

                for (int i = 0; i < DT.DefaultView.Count; i++)
                {
                    oDataTable.Rows.Add(DT.DefaultView[i]["dsPais"],
                                        DT.DefaultView[i]["dsPaisIngles"],
                                        DT.DefaultView[i]["dsPaisEspanhol"],
                                        DT.DefaultView[i]["dsPaisFrances"]);
                }


                //bsufs.DataSource = oDataTable.DefaultView;
                txtPaisFiltro.DataSource = oDataTable.DefaultView;
                if (SessionIdioma == "PTBR")
                    txtPaisFiltro.DataTextField = "dsPais";
                else if (SessionIdioma == "ENUS")
                    txtPaisFiltro.DataTextField = "dsPaisIngles";
                else if (SessionIdioma == "ESP")
                    txtPaisFiltro.DataTextField = "dsPaisEspanhol";
                else if (SessionIdioma == "FRA")
                    txtPaisFiltro.DataTextField = "dsPaisFrances";

                txtPaisFiltro.DataValueField = "dsPais";
                txtPaisFiltro.DataBind();
                //this.SelectedIndex = 0;

                //this.DropDownStyle = ComboBoxStyle.DropDownList;

            }
            catch (Exception Ex)
            {
                //_erroNoCampo = true;
                txtMsg.Visible = true;
                txtMsg.Text = "Erro ao selecionar Países!\n" + Ex.Message;
                SessionCnn.Close();
            }
        }
        finally
        {
            SessionCnn.Close();
        }


    }

    public void ListarAreaAtuacao()
    {
        txtMsg.Visible = false;
        if (SessionCnn == null)
        {
            //_erroNoCampo = true;
            txtMsg.Visible = true;
            txtMsg.Text = "Conexão inválida ou inexistente";
            return;
        }
        if (SessionCnn.State != ConnectionState.Open)
        {
            try
            {
                SessionCnn.Open();
            }
            catch
            {
                //_erroNoCampo = true;
                txtMsg.Visible = true;
                txtMsg.Text = "Conexão inválida";
                return;
            }
        }

        DataTable DT = new DataTable();
        SqlDataAdapter Dap;
        try
        {
            try
            {
                string cmd = "SELECT distinct noAreaAtuacao " +
                    "FROM " +
                      " dbo.tbMeetingParticipantes pm " +
                    "ORDER BY 1 ";

                SqlCommand comando = new SqlCommand(
                     cmd, SessionCnn);


                Dap = new SqlDataAdapter(comando);

                Dap.TableMappings.Add("AreaAtuacao", "tbAreaAtuacao");
                Dap.Fill(DT);

                SessionCnn.Close();

                //BindingSource bsufs = new BindingSource();

                DataTable oDataTable = new DataTable();



                oDataTable.Columns.Add("noAreaAtuacao");

                oDataTable.Rows.Add("");

                for (int i = 0; i < DT.DefaultView.Count; i++)
                {
                    oDataTable.Rows.Add(DT.DefaultView[i]["noAreaAtuacao"]);
                }


                //bsufs.DataSource = oDataTable.DefaultView;
                txtAreaAtuacaoFiltro.DataSource = oDataTable.DefaultView;
                txtAreaAtuacaoFiltro.DataTextField = "noAreaAtuacao";
                txtAreaAtuacaoFiltro.DataValueField = "noAreaAtuacao";
                txtAreaAtuacaoFiltro.DataBind();
                //this.SelectedIndex = 0;

                //this.DropDownStyle = ComboBoxStyle.DropDownList;

            }
            catch (Exception Ex)
            {
                //_erroNoCampo = true;
                txtMsg.Visible = true;
                txtMsg.Text = "Erro ao selecionar Países!\n" + Ex.Message;
                SessionCnn.Close();
            }
        }
        finally
        {
            SessionCnn.Close();
        }


    }
}
