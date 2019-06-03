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

public partial class frmMeetingConviteEnv : System.Web.UI.Page
{
    Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    Participante SessionParticipanteConvidado;

    MeetingConvite SessionMeetingConviteEnv;
    MeetingConviteCad oMeetingConviteCad = new MeetingConviteCad();

    MeetingMesaReuniaoCad oMeetingMesaReuniaoCad = new MeetingMesaReuniaoCad();

    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    SqlConnection SessionCnn;
    
    String SessionIdioma;

    DataTable oDT = new DataTable();
    DataTable oDTConvidado = new DataTable();
    DataTable oDTMesas = new DataTable();

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


            oDTConvidado.Columns.Add("cdConvite");//0
            oDTConvidado.Columns.Add("dsDataHoraSugestao");//1
            oDTConvidado.Columns.Add("dsDataHoraSugestaoNormal");//2

            Session["oDTConvidado"] = oDTConvidado;


            SessionMeetingConviteEnv = new MeetingConvite();
            if ((Request["cdConviteEnv"] != null) &&
                (Request["cdConviteEnv"].ToString().Trim().ToUpper() != ""))
            {
                PesquisarConvite(Request.QueryString["cdConviteEnv"]);                
            }
            else
            {
                if ((Request["cdPartConv"] != null) &&
                     (Request["cdPartConv"].ToString().Trim().ToUpper() != ""))
                {
                    PesquisarPartipanteConvite(Request["cdPartConv"]);
                }
                else
                {
                    pnlDadosConvite.Visible = false;
                    txtMsg.Text = "Participante não encontrado";
                    txtMsg.Visible = true;
                }
            }
            Session["SessionMeetingConviteEnv"] = SessionMeetingConviteEnv;



            oDT = new DataTable();

            oDT.Columns.Add("cdConvite");//0
            oDT.Columns.Add("dsDataHoraSugestao");//1
            oDT.Columns.Add("dsDataHoraSugestaoNormal");//2

            ListarHorariosConvite();

            Session["oDT"] = oDT;

            

            ListarDatas();
            ListarHorarios("");


            if (SessionIdioma == "PTBR")
            {
                btnCancelar.Attributes.Add("OnClick", "return confirm('Deseja cancelar o convite?');");
            }
            else if (SessionIdioma == "ENUS")
            {
                btnCancelar.Attributes.Add("onclick", "return confirm ('Want to cancel the invitation?');");
            }
            else if (SessionIdioma == "ESP")
            {
                btnCancelar.Attributes.Add("onclick", "return confirm ('¿Quiere cancelar la invitación?');");
            }
            else if (SessionIdioma == "FRA")
            {
                btnCancelar.Attributes.Add("onclick", "return confirm ('Vous voulez annuler l'invitation?');");
            }
        }
        else
        {

            SessionMeetingConviteEnv = (MeetingConvite)Session["SessionMeetingConviteEnv"];

            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionParticipanteConvidado = (Participante)Session["SessionParticipanteConvite"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            SessionIdioma = (String)Session["SessionIdioma"];

            oDT = (DataTable)Session["oDT"];

            oDTConvidado = (DataTable)Session["oDTConvidado"];

            if (SessionEvento == null)
                Server.Transfer("frmSessaoExpirada.aspx", true);

        }

        verificarIdioma(SessionIdioma);

        ToolkitScriptManager1.RegisterPostBackControl(btnIncluirHorario);
        ToolkitScriptManager1.RegisterPostBackControl(grd);

    }

    private void PesquisarConvite(string prmCdConvite)
    {
        SessionMeetingConviteEnv = oMeetingConviteCad.Pesquisar(SessionEvento.CdEvento, prmCdConvite, SessionCnn);
        Session["SessionMeetingConviteEnv"] = SessionMeetingConviteEnv;

        lblIdentificador.Text = SessionMeetingConviteEnv.CdConvite.Trim();
        lblNoParticipante.Text = SessionMeetingConviteEnv.ParticipanteConvidado.NoParticipante.Trim();



        //if (SessionIdioma == "PTBR")
        //    lblCategoria.Text = SessionParticipante.Categoria.NoCategoria.Trim();
        //else if (SessionIdioma == "ENUS")
        //    lblCategoria.Text = SessionParticipante.Categoria.NoCategoriaIngles.Trim();
        //else if (SessionIdioma == "ESP")
        //    lblCategoria.Text = SessionParticipante.Categoria.NoCategoriaEspanhol.Trim();
        //else if (SessionIdioma == "FRA")
        //    lblCategoria.Text = SessionParticipante.Categoria.NoCategoriaFrances.Trim();


        lblPerfilB2B.Text = (SessionMeetingConviteEnv.ParticipanteConvidado.meetingParticipante == null ? "" : SessionMeetingConviteEnv.ParticipanteConvidado.meetingParticipante.DsPerfilEmpresaHTML);
        lblEmpresa.Text = (SessionMeetingConviteEnv.ParticipanteConvidado.meetingParticipante == null ? "" : SessionMeetingConviteEnv.ParticipanteConvidado.meetingParticipante.NoInstituicao);
        lblNoPais.Text = (SessionMeetingConviteEnv.ParticipanteConvidado.meetingParticipante == null ? "" : SessionMeetingConviteEnv.ParticipanteConvidado.meetingParticipante.NoPais);
        lblAreaAtuacao.Text = (SessionMeetingConviteEnv.ParticipanteConvidado.meetingParticipante == null ? "" : SessionMeetingConviteEnv.ParticipanteConvidado.meetingParticipante.NoAreaAtuacao);
        lblWebSite.Text = (SessionMeetingConviteEnv.ParticipanteConvidado.meetingParticipante == null ? "" : SessionMeetingConviteEnv.ParticipanteConvidado.meetingParticipante.DsWebsite);

        //lblNoCargo.Text = (SessionMeetingConviteEnv.ParticipanteConvidado.meetingParticipante == null ? "" : SessionMeetingConviteEnv.ParticipanteConvidado.meetingParticipante.NoCargo);

        SessionParticipanteConvidado = SessionMeetingConviteEnv.ParticipanteConvidado;
        Session["SessionParticipanteConvite"] = SessionParticipanteConvidado;

        lblDtConvite.Text = SessionMeetingConviteEnv.DtConvite.Value.ToString("dd/MM/yyyy");

        if (SessionIdioma != "PTBR")
            lblDtConvite.Text = SessionMeetingConviteEnv.DtConvite.Value.ToString("MM/dd/yyyy");

        if (lblIdentificador.Text != "")
            pnlSugerirHorario.Visible = true;

        btnVoltar.PostBackUrl = "~/frmMeetingConvitesEnviados.aspx";

        lblStatusConvite.Text = SessionMeetingConviteEnv.DsStatusConvite;
        if (lblStatusConvite.Text.ToUpper() == "EM ABERTO")
        {
            
            lblStatusConvite.BackColor = System.Drawing.Color.Yellow;
            lblStatusConvite.ForeColor = System.Drawing.Color.Black;

            btnEnviar.Visible = true;
            btnCancelar.Visible = true;

            btnEnviar.Text = "Gravar e Reenviar Convite";

            if (SessionIdioma != "PTBR")
            {
                lblStatusConvite.Text = "WAITING";
                btnEnviar.Text = "Save and send invitation";
            }
            

            grd.Visible = true;
            grdConvidado.Visible = false;

            grd.Columns[2].Visible = true;

            pnlSelHorario.Visible = true;

            LblInfMesas.Visible = false;

        }
        else if (lblStatusConvite.Text.ToUpper() == "ACEITO")
        {
            if (SessionIdioma != "PTBR")
                lblStatusConvite.Text = "ACCEPTED";

            lblStatusConvite.BackColor = System.Drawing.Color.Green;
            lblStatusConvite.ForeColor = System.Drawing.Color.White;

            btnEnviar.Visible = false;
            btnCancelar.Visible = true;

            LblInfMesas.Visible = true;

            grd.Visible = true;
            grdConvidado.Visible = true;

            grd.Columns[2].Visible = false;

            pnlSelHorario.Visible = false;

            ListarHorariosConvidado();

            //if (grdConvidado.Rows.Count <= 0)
                grd.Columns[3].Visible = true;

                grdMesasSel.Visible = true;
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

    private void PesquisarPartipanteConvite(string prmCdParticpante)
    {
        SessionParticipanteConvidado = oParticipanteCad.Pesquisar(SessionEvento.CdEvento, prmCdParticpante, SessionCnn);
        Session["SessionParticipanteConvite"] = SessionParticipanteConvidado;

        //lblIdentificador.Text = SessionMeetingConviteEnv.CdConvite.Trim();
        lblNoParticipante.Text = SessionParticipanteConvidado.NoParticipante.Trim();



        //if (SessionIdioma == "PTBR")
        //    lblCategoria.Text = SessionParticipante.Categoria.NoCategoria.Trim();
        //else if (SessionIdioma == "ENUS")
        //    lblCategoria.Text = SessionParticipante.Categoria.NoCategoriaIngles.Trim();
        //else if (SessionIdioma == "ESP")
        //    lblCategoria.Text = SessionParticipante.Categoria.NoCategoriaEspanhol.Trim();
        //else if (SessionIdioma == "FRA")
        //    lblCategoria.Text = SessionParticipante.Categoria.NoCategoriaFrances.Trim();


        lblPerfilB2B.Text = (SessionParticipanteConvidado.meetingParticipante == null ? "" : SessionParticipanteConvidado.meetingParticipante.DsPerfilEmpresaHTML);
        lblEmpresa.Text = (SessionParticipanteConvidado.meetingParticipante == null ? "" : SessionParticipanteConvidado.meetingParticipante.NoInstituicao);
        lblNoPais.Text = (SessionParticipanteConvidado.meetingParticipante == null ? "" : SessionParticipanteConvidado.meetingParticipante.NoPais);
        lblAreaAtuacao.Text = (SessionParticipanteConvidado.meetingParticipante == null ? "" : SessionParticipanteConvidado.meetingParticipante.NoAreaAtuacao);
        lblWebSite.Text = (SessionParticipanteConvidado.meetingParticipante == null ? "" : SessionParticipanteConvidado.meetingParticipante.DsWebsite);

        //lblNoCargo.Text = (SessionParticipanteConvite.meetingParticipante == null ? "" : SessionParticipanteConvite.meetingParticipante.NoCargo);

        btnCancelar.Visible = false;
        btnVoltar.PostBackUrl = "~/frmMeetingParticipantes.aspx";
            
    }

    protected void verificarIdioma(string prmIdioma)
    {
        if (prmIdioma == "PTBR")
        {
            SessionIdioma = "PTBR";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Convite";

            lblID.Text = "Nr Convite:";
            lblPart.Text = "Representante:";
            //lblCateg.Text = "Categoria:";

            lblInstrucoes.Text = "Detalhes sobre o perfil de negócios da empresa (você pode escolher mais que um):";

            btnVoltar.Text = "Voltar";

            grd.Caption = "Sugeridos por Você";
            grdConvidado.Caption = "Sugeridos pelo Representante";

            grdMesas.Caption = "Selecione o Local da Reunião";

        }
        else if (prmIdioma == "ENUS")
        {
            SessionIdioma = "ENUS";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Invitation";

            lblID.Text = "No. invitation";
            lblPart.Text = "Representative:";
            //lblCateg.Text = "Category:";

            lblInstrucoes.Text = "Details about the company's business profile:";

            lblInstrucoesSugerirHorario.Text = "Enter the times you will be available (you can choose more than one):";

            lblEmpr.Text = "Institution / Company:";
            lbl_pais.Text = "Country:";
            lblArea.Text = "Main Practice Areas:";

           
            DtConvite.Text = "Dt Invitation:";
            StatusConvite.Text = "Status Invitation:";

            btnVoltar.Text = "Return";
            btnEnviar.Text = "Save and send invitation";
            btnCancelar.Text = "Cancel invitation";
            btnNovo.Text = "New invitation";

            lblData.Text = "Date:";
            lblHora.Text = "Time:";
            LblInfMesas.Text = "Schedule:";

            grd.Caption = "Suggested for You";
            grdConvidado.Caption = "Suggested by Representative";

            grdMesas.Caption = "Select the Meeting Place";

        }
        else if (prmIdioma == "ESP")
        {
            SessionIdioma = "ESP";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Invitación";

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

            lblTituloPagina.Text = "Profil";

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

                SessionCnn.Close();

                //Dap.TableMappings.Add("Data", "tbDatas");
                //Dap.Fill(DT);

                //BindingSource bsufs = new BindingSource();

                MeetingParticipanteCad oMeetingParticipanteCad = new MeetingParticipanteCad();
                DT = oMeetingParticipanteCad.ListarDatasDisponiveisConvidado(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionParticipanteConvidado.CdParticipante, SessionCnn);

                DataTable oDataTable = new DataTable();



                oDataTable.Columns.Add("DATA_V");
                oDataTable.Columns.Add("DATA_S");

                oDataTable.Rows.Add("","");

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

                oDataTable.Rows.Add("","");

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

        oDT.Rows.Clear();

        DataTable tempDT = oMeetingConviteCad.ListarHorariosSugeridosEnviados(SessionMeetingConviteEnv.CdConvite, SessionCnn);

        if (tempDT != null)
        {
            for(int i = 0; i < tempDT.Rows.Count; i++)
            {
                oDT.Rows.Add(
                    tempDT.DefaultView[i]["cdConvite"].ToString(),
                    tempDT.DefaultView[i]["dsDataHoraSugestao"].ToString(),
                    tempDT.DefaultView[i]["dsDataHoraSugestao"].ToString()
                    );
            }
        }

        //oDT.DefaultView.Sort = "cdConvite desc";
        

        grd.DataSource = oDT;
        grd.DataBind();

        Session["oDT"] = oDT;

    }

    protected void ListarHorariosConvidado()
    {
        grdConvidado.DataSource = null;
        grdConvidado.DataBind();

        oDTConvidado.Rows.Clear();

        DataTable tempDTConvidado = oMeetingConviteCad.ListarHorariosSugeridosRecebidos(SessionMeetingConviteEnv.CdConvite, SessionCnn);

        //if (oDTConvidado == null)
        //{
        //    return;
        //}

        if (tempDTConvidado != null)
        {
            for (int i = 0; i < tempDTConvidado.Rows.Count; i++)
            {
                oDTConvidado.Rows.Add(
                    tempDTConvidado.DefaultView[i]["cdConvite"].ToString(),
                    tempDTConvidado.DefaultView[i]["dsDataHoraSugestao"].ToString(),
                    tempDTConvidado.DefaultView[i]["dsDataHoraSugestao"].ToString()
                    );
            }
        }

        //oDTConvidado.DefaultView.Sort = "cdConvite desc";


        grdConvidado.DataSource = oDTConvidado;
        grdConvidado.DataBind();

        Session["oDTConvidado"] = oDTConvidado;

        
    }
    protected void ListarHorariosMesas()
    {
        lblMsg.Text = "";
        lblMsg.Visible = false;

        grdMesas.DataSource = null;
        grdMesas.DataBind();

        if (lblHorarioConvite.Text == "")
            return;

        //String dtconvite = lblHorarioConvite.Text;

        //CultureInfo ptBR = new CultureInfo("pt-BR");
        ////CultureInfo enUS = new CultureInfo("en-US");

        ////if (SessionIdioma == "ENUS")
        ////    dtconvite = DateTime.Parse(dtconvite).ToString("MM/dd/yyyy HH:mm tt", enUS).ToLower();
        ////else
        //    dtconvite = DateTime.Parse(dtconvite).ToString("dd/MM/yyyy HH:mm", ptBR);

           // oDTMesas = oMeetingMesaReuniaoCad.ListarPorDataInicio(SessionEvento.CdEvento, DateTime.Parse(dtconvite), SessionCnn);//DateTime.Parse(lblHorarioConvite.Text), SessionCnn);

            oDTMesas = oMeetingMesaReuniaoCad.ListarPorDataInicio(SessionEvento.CdEvento, DateTime.Parse(lblHorarioNormalConvite.Text), SessionCnn);

        if (oDTMesas == null)
        {
            lblMsg.Text = txtMsg.Text = SessionIdioma == "PTBR" ? "Nenhum mesa disponível" : "No table available";
            lblMsg.Visible = true;
            return;
        }

        //oDTConvidado.DefaultView.Sort = "cdConvite desc";


        grdMesas.DataSource = oDTMesas;
        grdMesas.DataBind();

    }

    
    protected void btnEnviar_Click(object sender, EventArgs e)
    {
        if ((oDT == null) || (oDT.Rows.Count <= 0))
        {
            lblMsg.Text = txtMsg.Text = SessionIdioma == "PTBR" ? "Você deve sugerir pelo menos um horário para a reunião." : "You should suggest at least one time for the meeting.";

            lblMsg.Visible = true;
            return;
        }


        //MeetingMesaReuniaoCad oMeetingMesaReuniaoCad = new MeetingMesaReuniaoCad();
        //oMeetingMesaReuniaoCad.CriarMesas(SessionEvento.CdEvento, SessionCnn);

        //if (lblIdentificador.Text == "")
        //{
            //cadatrar convite
            MeetingConvite tmpMeetingConvite = new MeetingConvite();
            tmpMeetingConvite.CdEvento = SessionEvento.CdEvento;
            tmpMeetingConvite.CdParticipanteConvidado = SessionParticipanteConvidado.CdParticipante;
            tmpMeetingConvite.CdParticipanteConvite = SessionParticipante.CdParticipante;
            tmpMeetingConvite.CdConvite = lblIdentificador.Text;
            tmpMeetingConvite.DsStatusConvite = "EM ABERTO";

            tmpMeetingConvite = oMeetingConviteCad.Gravar(tmpMeetingConvite, SessionCnn);

            if (tmpMeetingConvite != null)
            {

                oMeetingConviteCad.ExcluirHorarioSugeridosEnviados(SessionMeetingConviteEnv.CdConvite, SessionCnn);
                for (int i = 0; i < oDT.Rows.Count; i++)
                {
                    oMeetingConviteCad.IncluirHorarioSugeridoEnviado(tmpMeetingConvite.CdConvite, DateTime.Parse(oDT.DefaultView[i]["dsDataHoraSugestao"].ToString()), SessionCnn);
                }

                PesquisarConvite(tmpMeetingConvite.CdConvite);

                lblMsg.Text = txtMsg.Text = SessionIdioma == "PTBR" ? "Operação realizada com sucesso" : "Operation was successful";
                lblMsg.Visible =  true;
                
                oGeral.EnviarEmailConviteReuniao_Convidado(SessionEvento, SessionParticipante, tmpMeetingConvite, SessionCnn);

                //btnFoco.Focus();
            }
            else
            {
                lblMsg.Text = oMeetingConviteCad.RcMsg;
                lblMsg.Visible = true;

            }
        //}
        //else
        //{
        //    //enviar email
        //}
    }
    protected void btnIncluirHorario_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblMsg.Visible = false;
        if ((txtData.Text == "") || (txtHora.Text == ""))
            return;

        //DataTable tmpDt = oMeetingConviteCad.PesquisarHorarioSugeridoEnviado(SessionMeetingConviteEnv.CdConvite, DateTime.Parse(txtData.Text + " " + txtHora.Text), SessionCnn);
        //if ((tmpDt != null) && (tmpDt.Rows.Count > 0))
        //{
        //    lblMsg.Text = "Horário já incluido";
        //    lblMsg.Visible = true;
        //    return;
        //}


        if ((oDT != null) && (oDT.Rows.Count > 0))
        {
            DataRow[] tmpData = oDT.Select(" dsDataHoraSugestao = '" + txtData.Text + " " + txtHora.Text + "'");
            if (tmpData.Length > 0)
            {
                lblMsg.Text = txtMsg.Text = SessionIdioma == "PTBR" ? "Horário já incluido" : "Schedule already included";
                lblMsg.Visible = true;
                grd.Focus();
                return;
            }

        }


        //oMeetingConviteCad.IncluirHorarioSugeridoEnviado(SessionMeetingConviteEnv.CdConvite, DateTime.Parse(txtData.Text + " " + txtHora.Text), SessionCnn);

        oDT.Rows.Add(
                    SessionMeetingConviteEnv.CdConvite,
                    txtData.Text + " " + txtHora.Text,
                    txtData.Text + " " + txtHora.Text
                    );

        grd.DataSource = oDT;
        grd.DataBind();

        lblMsg.Text = "";
        lblMsg.Visible = false;

        grd.Focus();

        //ListarHorariosConvite();
    }
    protected void grd_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Remover")
        {
            lblMsg.Text = "";
            lblMsg.Visible = false;


            int numLinha = Convert.ToInt32(e.CommandArgument);

            //oMeetingConviteCad.ExcluirHorarioSugeridoEnviado(SessionMeetingConviteEnv.CdConvite, DateTime.Parse(grd.Rows[numLinha].Cells[0].Text.ToString()), SessionCnn);


            oDT.Rows.RemoveAt(numLinha);
            Session["oDT"] = oDT;
            
            grd.DataSource = oDT;
            grd.DataBind();

            grd.Focus();

            lblMsg.Text = "";
            lblMsg.Visible = false;

            //ListarHorariosConvite();
        }
        else 
        if (e.CommandName == "Selecionar")
        {
            lblMsg.Text = "";
            lblMsg.Visible = false;


            int numLinha = Convert.ToInt32(e.CommandArgument);

            lblHorarioConvite.Text = grd.Rows[numLinha].Cells[0].Text.ToString();// DateTime.Parse(grd.Rows[numLinha].Cells[0].Text.ToString()).ToString("dd/MM/yyyy HH:mm");
            lblHorarioNormalConvite.Text = oDT.Rows[numLinha][2].ToString();


            ListarHorariosMesas();
        }
    }
    protected void grdConvidado_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
        if (e.CommandName == "Selecionar")
        {
            lblMsg.Text = "";
            lblMsg.Visible = false;


            int numLinha = Convert.ToInt32(e.CommandArgument);

            lblHorarioConvite.Text = grdConvidado.Rows[numLinha].Cells[0].Text.ToString();// DateTime.Parse(grdConvidado.Rows[numLinha].Cells[0].Text.ToString()).ToString("dd/MM/yyyy HH:mm");
            lblHorarioNormalConvite.Text = oDTConvidado.Rows[numLinha][2].ToString();

            ListarHorariosMesas();
        }
    }
    
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        SessionMeetingConviteEnv.DsStatusConvite = "CANCELADO";

        oMeetingConviteCad.Gravar(SessionMeetingConviteEnv, SessionCnn);

        //enviar mensagem

        PesquisarConvite(SessionMeetingConviteEnv.CdConvite);

        lblMsg.Text = txtMsg.Text = SessionIdioma == "PTBR" ? "Operação realizada com sucesso" : "Operation was successful";
        lblMsg.Visible = txtMsg.Visible = true;
        btnFoco.Focus();
    }
    protected void grdMesas_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton btnagendar = (ImageButton)e.Row.FindControl("btnAgendarReuniao"); //(ImageButton)e.Row.Cells[3].Controls[0];

            if (SessionIdioma == "PTBR")
            {
                btnagendar.OnClientClick = "return confirm('Confirma o agendamento?');";
                btnagendar.ToolTip = "Agendar";
            }
            else if (SessionIdioma == "ENUS")
            {
                btnagendar.Attributes.Add("onclick", "return confirm ('Confirms the schedule?');");
                btnagendar.ToolTip = "Schedule";
            }
            else if (SessionIdioma == "ESP")
            {
                btnagendar.Attributes.Add("onclick", "return confirm ('¿Confirma la programación?');");
                btnagendar.ToolTip = "Calendario";
            }
            else if (SessionIdioma == "FRA")
            {
                btnagendar.Attributes.Add("onclick", "return confirm ('Confirme le calendrier?');");
                btnagendar.ToolTip = "Calendrier";
            }
        }
        else if (e.Row.RowType.Equals(DataControlRowType.Header))
        {
            if (SessionIdioma == "ENUS")
            {
                e.Row.Cells[0].Text = "Meeting table";
                e.Row.Cells[2].Text = "Schedule";
            }
            //else
            //    e.Row.Cells[0].Text = "Horário";

        }       
        
    }
    
    protected void btnAgendarReuniao_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;

        lblMsg.Text = "";
        lblMsg.Visible = false;

        

        int numLinha = row.RowIndex;

        MeetingAgendaCad oMeetingAgendaCad = new MeetingAgendaCad();

        MeetingAgenda oMeetingAgenda = new MeetingAgenda();

        oMeetingAgenda.CdEvento = SessionEvento.CdEvento;
        oMeetingAgenda.CdMesaReuniao = grdMesas.DataKeys[numLinha].Values["cdMesaReuniao"].ToString();
        oMeetingAgenda.CdParticipanteConvidado = SessionMeetingConviteEnv.CdParticipanteConvidado;
        oMeetingAgenda.CdParticipanteConvite = SessionMeetingConviteEnv.CdParticipanteConvite;
        oMeetingAgenda.CdConvite = SessionMeetingConviteEnv.CdConvite;
        oMeetingAgenda.CdOperador = "000000001";//operadorweb o proprio participante

        MeetingAgenda tmpMeetingAgenda = oMeetingAgendaCad.Gravar(oMeetingAgenda, "000000001", SessionCnn);

        if (tmpMeetingAgenda != null)
        {
            PesquisarConvite(SessionMeetingConviteEnv.CdConvite);

            oGeral.EnviarEmailConviteReuniao_AgendadaConvidado(SessionEvento, SessionParticipante, tmpMeetingAgenda, SessionCnn);


            lblMsg.Text = txtMsg.Text = SessionIdioma == "PTBR" ? "Operação realizada com sucesso" : "Operation was successful";
            lblMsg.Visible = txtMsg.Visible = true;
            btnFoco.Focus();
        }
        else
        {
            lblMsg.Text = oMeetingAgendaCad.RcMsg;
            lblMsg.Visible = true;
            
        }

        //lblHorarioConvite.Text = DateTime.Parse(grdMesas.Rows[numLinha].Cells[0].Text.ToString()).ToString("dd/MM/yyyy HH:mm");
        //lblMsg.Text =  grdMesas.DataKeys[numLinha].Values["cdMesaReuniao"].ToString();
        //oDTMesas.DefaultView[numLinha][0].ToString() + " - " + oDTMesas.DefaultView[numLinha][1].ToString();
        //lblMsg.Visible = true;
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
            else
                dtconvite = DateTime.Parse(dtconvite).ToString("dd/MM/yyyy HH:mm", ptBR);
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
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //oDT.Rows.RemoveAt(e.RowIndex);
        //Session["oDT"] = oDT;
        //btnIncluirHorario.Focus();
    }
}
