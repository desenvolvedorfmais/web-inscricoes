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

using System.Globalization;

public partial class frmMeetingConviteRec : System.Web.UI.Page
{
    Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    MeetingConvite SessionMeetingConviteRec;
    MeetingConviteCad oMeetingConviteCad = new MeetingConviteCad();

    MeetingMesaReuniaoCad oMeetingMesaReuniaoCad = new MeetingMesaReuniaoCad();

    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    SqlConnection SessionCnn;
    
    String SessionIdioma;

    DataTable oDT = new DataTable();
    DataTable oDTConvidado = new DataTable();


    Geral oGeral = new Geral();
    
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

            if ((Request.QueryString["cdConviteRec"] == null) &&
                (Request.QueryString["cdConviteRec"].ToString().Trim().ToUpper() == ""))
            {
                pnlDadosConvite.Visible = false;
                txtMsg.Text = "Convite não encontrado";
            }
            else
            {
                PesquisarConvite(Request.QueryString["cdConviteRec"]);
            }
            Session["SessionMeetingConviteRec"] = SessionMeetingConviteRec;

            ListarHorariosConvite();
            ListarDatas();
            ListarHorarios("");


            if (SessionIdioma == "PTBR")
            {
                btnCancelar.Attributes.Add("OnClick", "return confirm('Deseja recusar o convite?');");
            }
            else if (SessionIdioma == "ENUS")
            {
                btnCancelar.Attributes.Add("onclick", "return confirm ('Want to decline the invitation?');");
            }
            else if (SessionIdioma == "ESP")
            {
                btnCancelar.Attributes.Add("onclick", "return confirm ('¿Quieres rechazar la invitación?');");
            }
            else if (SessionIdioma == "FRA")
            {
                btnCancelar.Attributes.Add("onclick", "return confirm ('Vous voulez décliner l'invitation?');");
            }
        }
        else
        {

            SessionMeetingConviteRec = (MeetingConvite)Session["SessionMeetingConviteRec"];

            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            SessionIdioma = (String)Session["SessionIdioma"];

            if (SessionEvento == null)
                Server.Transfer("frmSessaoExpirada.aspx", true);

        }

        verificarIdioma(SessionIdioma);

        //ToolkitScriptManager1.RegisterPostBackControl(grdMesas);
    }

    private void PesquisarConvite(string prmCdConvite)
    {
        SessionMeetingConviteRec = oMeetingConviteCad.Pesquisar(SessionEvento.CdEvento, prmCdConvite, SessionCnn);
        Session["SessionMeetingConviteRec"] = SessionMeetingConviteRec;

        lblIdentificador.Text = SessionMeetingConviteRec.CdConvite.Trim();
        lblNoParticipante.Text = SessionMeetingConviteRec.ParticipanteConvite.NoParticipante.Trim();



        //if (SessionIdioma == "PTBR")
        //    lblCategoria.Text = SessionParticipante.Categoria.NoCategoria.Trim();
        //else if (SessionIdioma == "ENUS")
        //    lblCategoria.Text = SessionParticipante.Categoria.NoCategoriaIngles.Trim();
        //else if (SessionIdioma == "ESP")
        //    lblCategoria.Text = SessionParticipante.Categoria.NoCategoriaEspanhol.Trim();
        //else if (SessionIdioma == "FRA")
        //    lblCategoria.Text = SessionParticipante.Categoria.NoCategoriaFrances.Trim();


        lblPerfilB2B.Text = (SessionMeetingConviteRec.ParticipanteConvite.meetingParticipante == null ? "" : SessionMeetingConviteRec.ParticipanteConvite.meetingParticipante.DsPerfilEmpresaHTML);
        lblEmpresa.Text = (SessionMeetingConviteRec.ParticipanteConvite.meetingParticipante == null ? "" : SessionMeetingConviteRec.ParticipanteConvite.meetingParticipante.NoInstituicao);
        lblNoPais.Text = (SessionMeetingConviteRec.ParticipanteConvite.meetingParticipante == null ? "" : SessionMeetingConviteRec.ParticipanteConvite.meetingParticipante.NoPais);
        lblAreaAtuacao.Text = (SessionMeetingConviteRec.ParticipanteConvite.meetingParticipante == null ? "" : SessionMeetingConviteRec.ParticipanteConvite.meetingParticipante.NoAreaAtuacao);
        lblWebSite.Text = (SessionMeetingConviteRec.ParticipanteConvite.meetingParticipante == null ? "" : SessionMeetingConviteRec.ParticipanteConvite.meetingParticipante.DsWebsite);

        //lblNoCargo.Text = (SessionMeetingConviteEnv.ParticipanteConvite.meetingParticipante == null ? "" : SessionMeetingConviteEnv.ParticipanteConvite.meetingParticipante.NoCargo);

        lblDtConvite.Text = SessionMeetingConviteRec.DtConvite.Value.ToString("dd/MM/yyyy");
        if (SessionIdioma != "PTBR")
            lblDtConvite.Text = SessionMeetingConviteRec.DtConvite.Value.ToString("MM/dd/yyyy");

        if (lblIdentificador.Text != "")
            pnlSugerirHorario.Visible = true;

        lblStatusConvite.Text = SessionMeetingConviteRec.DsStatusConvite;
        if (lblStatusConvite.Text.ToUpper() == "EM ABERTO")
        {
            if (SessionIdioma != "PTBR")
                lblStatusConvite.Text = "WAITING";

            lblStatusConvite.BackColor = System.Drawing.Color.Yellow;
            lblStatusConvite.ForeColor = System.Drawing.Color.Black;

            
            

            btnEnviar.Visible = true;
            btnCancelar.Visible = true;

            //btnEnviar.Text = "Aceitar Convite";

            grd.Visible = true;
            grdConvidado.Visible = true;

            grd.Columns[2].Visible = true;

            pnlSelHorario.Visible = true;

            ListarHorariosConvidado();
            

        }
        else if (lblStatusConvite.Text.ToUpper() == "ACEITO")
        {
            if (SessionIdioma != "PTBR")
                lblStatusConvite.Text = "ACCEPTED";

            lblStatusConvite.BackColor = System.Drawing.Color.Green;
            lblStatusConvite.ForeColor = System.Drawing.Color.White;
                        

            btnEnviar.Visible = true;
            btnCancelar.Visible = true;

            ListarHorariosConvidado();

            grd.Visible = true;
            grdConvidado.Visible = true;

            //grd.Columns[2].Visible = false;

           // pnlSelHorario.Visible = false;

            

            //if (grdConvidado.Rows.Count <= 0)
             //   grd.Columns[3].Visible = true;
        }
        else if ((lblStatusConvite.Text.ToUpper() == "CANCELADO") || (lblStatusConvite.Text.ToUpper() == "RECUSADO"))
        {
            if (SessionIdioma != "PTBR")
            {
                lblStatusConvite.Text = "CANCELED";
                if (lblStatusConvite.Text.ToUpper() == "RECUSADO")
                    lblStatusConvite.Text = "DECLINED";
            }

            lblStatusConvite.BackColor = System.Drawing.Color.Red;
            lblStatusConvite.ForeColor = System.Drawing.Color.White;

            pnlSugerirHorario.Visible = false;

            btnEnviar.Visible = false;
            btnCancelar.Visible = false;
        }
        else if (lblStatusConvite.Text.ToUpper() == "AGENDADO")
        {
            if (SessionIdioma != "PTBR")
                lblStatusConvite.Text = "SCHEDULED";

            lblStatusConvite.BackColor = System.Drawing.Color.Blue;
            lblStatusConvite.ForeColor = System.Drawing.Color.White;

            pnlSugerirHorario.Visible = false;

            btnEnviar.Visible = false;
            btnCancelar.Visible = false;
        }
    }

    protected void verificarIdioma(string prmIdioma)
    {
        if (prmIdioma == "PTBR")
        {
            SessionIdioma = "PTBR";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Convite Recebido";

            lblID.Text = "Nr Convite:";
            lblPart.Text = "Representante:";
            //lblCateg.Text = "Categoria:";

            lblInstrucoes.Text = "Detalhes sobre o perfil de negócios da empresa (você pode escolher mais que um):";

            btnVoltar.Text = "Voltar";

            grd.Caption = "Sugeridos por Você";
            grdConvidado.Caption = "Sugeridos pelo Representante";
            
        }
        else if (prmIdioma == "ENUS")
        {
            SessionIdioma = "ENUS";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "invitation Received";

            lblID.Text = "No. invitation";
            lblPart.Text = "Representative:";
            //lblCateg.Text = "Category:";

            lblInstrucoes.Text = "Details about the company's business profile:";

            lblEmpr.Text = "Institution / Company:";
            lbl_pais.Text = "Country:";
            lblArea.Text = "Main Practice Areas:";


            DtConvite.Text = "Dt Invitation:";
            StatusConvite.Text = "Status Invitation:";

            btnVoltar.Text = "Return";
            btnEnviar.Text = "Accept invitation";
            btnCancelar.Text = "Refuse invitation";

            lblData.Text = "Date";
            lblHora.Text = "Time";

            lblInstrucoesSugerirHorario.Text = "Accept or enter the times that you are available (you can choose more than one):";
            
            grd.Caption = "Suggested for You";
            grdConvidado.Caption = "Suggested by Representative";            

        }
        else if (prmIdioma == "ESP")
        {
            SessionIdioma = "ESP";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Invitación Enviada";

            lblID.Text = "Invitación Nr:";
            lblPart.Text = "Representante:";
            //lblCateg.Text = "Categoría";

            lblInstrucoes.Text = "Los detalles sobre el perfil de negocio de la compañía:";

            btnVoltar.Text = "Volver";

            
        }
        else if (prmIdioma == "FRA")
        {
            SessionIdioma = "FRA";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Profil de la Société";

            lblPart.Text = "Participant:";
            //lblCateg.Text = "Catégorie:";
            lblID.Text = "N° d'enregistrement:";

            lblInstrucoes.Text = "Entrez le profil d'entreprise de votre entreprise:";

            btnVoltar.Text = "Confirmer";

            
        }
    }

    public void ListarDatas()
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
                string cmd =
                    "SELECT       distinct convert(varchar(10), dtMesaReuniaoIni, 103) DATA, " +
                     (SessionIdioma != "ENUS" ? " convert(varchar(10), dtMesaReuniaoIni, 103) DATA_VISIVEL " : " convert(varchar(10), dtMesaReuniaoIni, 103) DATA_VISIVEL ") +
                    "FROM            tbMeetingMesasReuniao " +
                    "where cdEvento = '" + SessionEvento.CdEvento + "' " +
                    "and flAtivo = 1 " +
                    "and dsStatus <> 'BLOQUEADA' " +
                    "order by 1 ";



                SqlCommand comando = new SqlCommand(
                     cmd, SessionCnn);


                Dap = new SqlDataAdapter(comando);

                //Dap.TableMappings.Add("Data", "tbDatas");
                //Dap.Fill(DT);

                //BindingSource bsufs = new BindingSource();

                MeetingParticipanteCad oMeetingParticipanteCad = new MeetingParticipanteCad();
                DT = oMeetingParticipanteCad.ListarDatasDisponiveisConvidado(SessionEvento.CdEvento, SessionMeetingConviteRec.ParticipanteConvite.NoParticipante, SessionParticipante.CdParticipante, SessionCnn);


                DataTable oDataTable = new DataTable();



                oDataTable.Columns.Add("DATA_V");
                oDataTable.Columns.Add("DATA_S");

                oDataTable.Rows.Add("", "");

                for (int i = 0; i < DT.DefaultView.Count; i++)
                {
                    if (SessionIdioma != "ENUS")
                    {
                        oDataTable.Rows.Add(
                            DT.DefaultView[i]["DATA"],
                            DT.DefaultView[i]["DATA_VISIVEL"]);
                    }
                    else
                    {
                        oDataTable.Rows.Add(
                            DateTime.Parse(DT.DefaultView[i]["DATA_VISIVEL"].ToString()).ToString("MM/dd/yyyy"),
                            DT.DefaultView[i]["DATA"]                            
                            );
                    }
                }


                txtData.DataSource = oDataTable.DefaultView;
                txtData.DataTextField = "DATA_V";
                txtData.DataValueField = "DATA_S";
                txtData.DataBind();

                SessionCnn.Close();
            }
            catch (Exception Ex)
            {
                //_erroNoCampo = true;
                txtMsg.Visible = true;
                txtMsg.Text = "Erro ao selecionar Datas!\n" + Ex.Message;
                SessionCnn.Close();
            }
        }
        finally
        {
            SessionCnn.Close();
        }


    }

    public void ListarHorarios(string prmData)
    {

        //if (prmData == "")
        //    return;

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
                string cmd =
                    "SELECT       distinct dtMesaReuniaoIni DATA_HORA " +
                    "             ,convert(varchar(10), dtMesaReuniaoIni, 108) HORA_NORMAL " +
                    "FROM            tbMeetingMesasReuniao " +
                    "where cdEvento = '" + SessionEvento.CdEvento + "' " +
                    "and flAtivo = 1 " +
                    "and dsStatus <> 'BLOQUEADA' " +
                    "and convert(varchar(10), dtMesaReuniaoIni, 103) = '" + prmData + "' " +
                    "order by 1";

                SqlCommand comando = new SqlCommand(
                     cmd, SessionCnn);


                Dap = new SqlDataAdapter(comando);

                Dap.TableMappings.Add("Hora", "tbHora");
                Dap.Fill(DT);

                SessionCnn.Close();

                //BindingSource bsufs = new BindingSource();

                DataTable oDataTable = new DataTable();



                oDataTable.Columns.Add("HoraS");
                oDataTable.Columns.Add("HoraV");

                oDataTable.Rows.Add("", "");

                CultureInfo ptBR = new CultureInfo("pt-BR");
                CultureInfo enUS = new CultureInfo("en-US");

                if (DT != null)
                {
                    for (int i = 0; i < DT.DefaultView.Count; i++)
                    {
                        if (SessionIdioma == "ENUS")
                        {
                            oDataTable.Rows.Add(
                                DT.DefaultView[i]["HORA_NORMAL"],
                                DateTime.Parse(DT.DefaultView[i]["DATA_HORA"].ToString()).ToString("HH:mm tt", enUS).ToLower()
                                );
                        }
                        else
                        {
                            oDataTable.Rows.Add(
                                DT.DefaultView[i]["HORA_NORMAL"],
                                DateTime.Parse(DT.DefaultView[i]["DATA_HORA"].ToString()).ToString("HH:mm", ptBR)
                                );
                        }
                    }
                }


                //bsufs.DataSource = oDataTable.DefaultView;
                txtHora.DataSource = oDataTable.DefaultView;
                txtHora.DataTextField = "HoraV";
                txtHora.DataValueField = "HoraS";
                txtHora.DataBind();
                //this.SelectedIndex = 0;

                //this.DropDownStyle = ComboBoxStyle.DropDownList;

            }
            catch (Exception Ex)
            {
                //_erroNoCampo = true;
                txtMsg.Visible = true;
                txtMsg.Text = "Erro ao selecionar Horários!\n" + Ex.Message;
                SessionCnn.Close();
            }
        }
        finally
        {
            SessionCnn.Close();
        }


    }

    protected void ListarHorariosConvite()
    {
        grd.DataSource = null;
        grd.DataBind();


        oDT = oMeetingConviteCad.ListarHorariosSugeridosRecebidos(SessionMeetingConviteRec.CdConvite, SessionCnn);

        if (oDT == null)
        {
            return;
        }

        //oDT.DefaultView.Sort = "cdConvite desc";
        

        grd.DataSource = oDT;
        grd.DataBind();

    }

    protected void ListarHorariosConvidado()
    {
        grdConvidado.DataSource = null;
        grdConvidado.DataBind();


        oDTConvidado = oMeetingConviteCad.ListarHorariosSugeridosEnviados(SessionMeetingConviteRec.CdConvite, SessionCnn);

        if (oDTConvidado == null)
        {
            return;
        }

        //oDTConvidado.DefaultView.Sort = "cdConvite desc";


        grdConvidado.DataSource = oDTConvidado;
        grdConvidado.DataBind();

    }
   
    protected void btnIncluirHorario_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblMsg.Visible = false;
        if ((txtData.Text == "") || (txtHora.Text == ""))
            return;

        DataTable tmpDt = oMeetingConviteCad.PesquisarHorarioSugeridoRecebido(SessionMeetingConviteRec.CdConvite, DateTime.Parse(txtData.Text + " " + txtHora.Text), SessionCnn);

        if ((tmpDt != null) && (tmpDt.Rows.Count > 0))
        {
            lblMsg.Text = txtMsg.Text = SessionIdioma == "PTBR" ? "Horário já incluido" : "Schedule already included";

            lblMsg.Visible = true;
            return;
        }

        oMeetingConviteCad.IncluirHorarioSugeridoRecebido(SessionMeetingConviteRec.CdConvite, DateTime.Parse(txtData.Text + " " + txtHora.Text), SessionCnn);

        lblMsg.Text = "";
        lblMsg.Visible = false;

        ListarHorariosConvite();
    }
    protected void grd_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Remover")
        {
            lblMsg.Text = "";
            lblMsg.Visible = false;
           

            int numLinha = Convert.ToInt32(e.CommandArgument);

            oMeetingConviteCad.ExcluirHorarioSugeridoRecebido(SessionMeetingConviteRec.CdConvite, DateTime.Parse(grd.Rows[numLinha].Cells[0].Text.ToString()), SessionCnn);

            lblMsg.Text = "";
            lblMsg.Visible = false;

            ListarHorariosConvite();
        }
        
    }
    protected void grdConvidado_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
        if (e.CommandName == "Selecionar")
        {
            lblMsg.Text = "";
            lblMsg.Visible = false;


            int numLinha = Convert.ToInt32(e.CommandArgument);

            //lblHorarioConvite.Text = DateTime.Parse(grdConvidado.Rows[numLinha].Cells[0].Text.ToString()).ToString("dd/MM/yyyy HH:mm");



            DataTable tmpDt = oMeetingConviteCad.PesquisarHorarioSugeridoRecebido(SessionMeetingConviteRec.CdConvite, DateTime.Parse(grdConvidado.Rows[numLinha].Cells[0].Text.ToString()), SessionCnn);

            if ((tmpDt != null) && (tmpDt.Rows.Count > 0))
            {
                lblMsg.Text = txtMsg.Text = SessionIdioma == "PTBR" ? "Horário já incluido" : "Schedule already included";
                lblMsg.Visible = true;
                return;
            }

            oMeetingConviteCad.IncluirHorarioSugeridoRecebido(SessionMeetingConviteRec.CdConvite, DateTime.Parse(grdConvidado.Rows[numLinha].Cells[0].Text.ToString()), SessionCnn);

            lblMsg.Text = "";
            lblMsg.Visible = false;

            ListarHorariosConvite();
        }
    }
    protected void btnEnviar_Click(object sender, EventArgs e)
    {
        if (grd.Rows.Count <= 0)// ((oDT == null) || (oDT.Rows.Count <= 0))
        {
            lblMsg.Text = txtMsg.Text = SessionIdioma == "PTBR" ? "Você deve sugerir ou aceitar pelo menos um horário para a reunião." : "You should suggest or mention at least one time for the meeting.";
            lblMsg.Visible = true;
            return;
        }

        SessionMeetingConviteRec.DsStatusConvite = "ACEITO";

        MeetingConvite tmpMeetingConvite = oMeetingConviteCad.Gravar(SessionMeetingConviteRec, SessionCnn);

        oGeral.EnviarEmailConviteReuniao_Aceito(SessionEvento, SessionParticipante, tmpMeetingConvite, SessionCnn);

        PesquisarConvite(SessionMeetingConviteRec.CdConvite);

        lblMsg.Text = txtMsg.Text = SessionIdioma == "PTBR" ? "Operação realizada com sucesso" : "Operation was successful";
        lblMsg.Visible = txtMsg.Visible = true;
        btnFoco.Focus();
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        SessionMeetingConviteRec.DsStatusConvite = "RECUSADO";

        MeetingConvite tmpMeetingConvite = oMeetingConviteCad.Gravar(SessionMeetingConviteRec, SessionCnn);

        oGeral.EnviarEmailConviteReuniao_Recusado(SessionEvento, SessionParticipante, tmpMeetingConvite, SessionCnn);

        PesquisarConvite(SessionMeetingConviteRec.CdConvite);

        lblMsg.Text = txtMsg.Text = SessionIdioma == "PTBR" ? "Operação realizada com sucesso" : "Operation was successful";
        lblMsg.Visible = txtMsg.Visible = true;
        btnFoco.Focus();
    }
    protected void txtData_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListarHorarios(txtData.SelectedValue.ToString());
    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            String dtconvite = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "dsDataHoraSugestao"));

            CultureInfo ptBR = new CultureInfo("pt-BR");
            CultureInfo enUS = new CultureInfo("en-US");

            if (SessionIdioma == "ENUS")
                dtconvite = DateTime.Parse(dtconvite).ToString("MM/dd/yyyy HH:mm tt", enUS).ToLower();
            else
                dtconvite = DateTime.Parse(dtconvite).ToString("dd/MM/yyyy HH:mm", ptBR);

            e.Row.Cells[0].Text = dtconvite;
        }
        else if (e.Row.RowType.Equals(DataControlRowType.Header))
        {
            if (SessionIdioma == "ENUS")
            {
                e.Row.Cells[0].Text = "Schedule";
                e.Row.Cells[2].Text = "Delete";
                e.Row.Cells[3].Text = "Select";
            }
            //else
            //{
            //    e.Row.Cells[0].Text = "Horário";
            //}
        }
    }
    protected void grdgrdConvidado_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            String dtconvite = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "dsDataHoraSugestao"));

            CultureInfo ptBR = new CultureInfo("pt-BR");
            CultureInfo enUS = new CultureInfo("en-US");

            if (SessionIdioma == "ENUS")
                dtconvite = DateTime.Parse(dtconvite).ToString("MM/dd/yyyy HH:mm tt", enUS).ToLower();
            //else
            //    dtconvite = DateTime.Parse(dtconvite).ToString("dd/MM/yyyy HH:mm", ptBR);
            e.Row.Cells[0].Text = dtconvite;
        }
        else if (e.Row.RowType.Equals(DataControlRowType.Header))
        {
            if (SessionIdioma == "ENUS")
            {
                e.Row.Cells[0].Text = "Schedule";
                e.Row.Cells[2].Text = "Select";
            }
            //else
            //    e.Row.Cells[0].Text = "Horário";

        }
    }
    
}
