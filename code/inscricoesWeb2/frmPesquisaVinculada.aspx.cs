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

public partial class frmPesquisaVinculada : BaseWebUi //System.Web.UI.Page
{

    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    Participante SessionParticipante;

    SqlConnection SessionCnn;

    DataTable oDTPesquisa;

    DataTable oDTGrupoPesquisa;

    private int idxPergunta = 0;
    private String[,] arrRespostas;

    String SessionTipoSistema, SessionFormulario;

    string tpRotina, SessionIdioma;

    //String SessionGrupoPesquisa = "";

    PesquisasDeOpiniao SessionPesquisasDeOpiniao = new PesquisasDeOpiniao();

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


            if (SessionEvento == null)
            {
                //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                //                "003",
                //                oEventoCad.RcMsg), true);

                Server.Transfer("frmSessaoExpirada.aspx", true);
            }

            SessionParticipante = (Participante)Session["SessionParticipante"];

            if (SessionParticipante != null)
            {
                lblIdentificador.Text = SessionParticipante.CdParticipante;
                lblNoParticipante.Text = SessionParticipante.NoParticipante;
            }

            SessionIdioma = (String)Session["SessionIdioma"];
            if (SessionIdioma == null)
                SessionIdioma = "PTBR";
            Session["SessionIdioma"] = SessionIdioma;

            SessionTipoSistema = (String)Session["SessionTipoSistema"];
            if (SessionTipoSistema == null)
                SessionTipoSistema = "NRM";

            Session["SessionTipoSistema"] = SessionTipoSistema;


            SessionFormulario = (String)Session["SessionFormulario"];
            if (SessionTipoSistema == null)
                SessionTipoSistema = "";

            Session["SessionFormulario"] = SessionFormulario;


            tpRotina = Request["tpRotina"];
            tpRotina = (String)Session["tpRotina"];
            if (tpRotina == null)
                tpRotina = "";
            Session["tpRotina"] = tpRotina;

            //PesquisasGrupoPerguntas oPesquisasGrupoPerguntas = new PesquisasGrupoPerguntas();
            //oDTGrupoPesquisa = oPesquisasGrupoPerguntas.ListarGruposPerguntas("001304001", SessionCnn);
            //Session["oDTGrupoPesquisa"] = oDTGrupoPesquisa;


            //SqlDataSource1.ConnectionString = SessionCnn.ConnectionString;
            //DataList1.DataSourceID = "SqlDataSource1";

            btnFoco.Focus();

            btnAbandonarPesquisa.PostBackUrl = "http://www.fazendomais.com/inscricoes/inscr.aspx?codEvento=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + "&cdLng=" + "PTBR" + "&cat=" + "" + "&atv=" + "" + "&keyAut=" + "" + "&tpSist=" + SessionTipoSistema;

            DataTable dtPesquisas;
            if ((SessionFormulario == null) || (SessionFormulario == ""))
                dtPesquisas = SessionPesquisasDeOpiniao.ListarPesquisasDoParticipante(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionParticipante.DsIdioma, SessionCnn);
            else
                dtPesquisas = SessionPesquisasDeOpiniao.ListarPesquisasDoParticipanteWeb(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionParticipante.DsIdioma, SessionCnn);


            
            if ((dtPesquisas != null) && (dtPesquisas.Rows.Count > 0))
            {
                SessionPesquisasDeOpiniao = SessionPesquisasDeOpiniao.LocalizarPesquisaDeOpiniao(SessionEvento.CdEvento, dtPesquisas.DefaultView[0]["cdQuestionario"].ToString(), SessionCnn);
                Session["SessionPesquisasDeOpiniao"] = SessionPesquisasDeOpiniao;
                // --- comentado só para pesquisa mobile conasems
                if (SessionPesquisasDeOpiniao.TpFolhaResposta == "COMPLETA")
                {
                    Session["oDTPesquisa"] = null;
                    if (dtPesquisas.Rows.Count == 1)                    
                        DataList0.DataSource = localizarGrupoPesquisa(SessionEvento.CdEvento, dtPesquisas.DefaultView[0]["cdQuestionario"].ToString());
                    else
                        DataList0.DataSource = localizarGrupoPesquisa(SessionEvento.CdEvento);

                    DataList0.DataBind();

                    btnContinuarPesquisa.Text = "Concluir";
                }
                else
                {
                    SessionPesquisasDeOpiniao.TpFolhaResposta = "PASSO A PASSO";
                    tbPesqLivre.Visible = true;
                    oDTPesquisa = new DataTable();

                    if (((DataTable)Session["oDTPesquisa"] != null) && (((DataTable)Session["oDTPesquisa"]).Rows.Count > 0))
                    {
                        oDTPesquisa = (DataTable)Session["oDTPesquisa"];

                        idxPergunta = int.Parse(Session["idxPergunta"].ToString());

                        arrRespostas = (String[,])Session["arrRespostas"];
                    }
                    else
                    {
                        //DataTable dtTemp = localizarGrupoPesquisa(SessionEvento.CdEvento, dtPesquisas.DefaultView[0]["cdQuestionario"].ToString());

                        localizarPesquisa(SessionEvento.CdEvento, dtPesquisas.DefaultView[0]["cdQuestionario"].ToString(), "", null);

                        Session["idxPergunta"] = idxPergunta;

                        arrRespostas = new String[oDTPesquisa.Rows.Count, 6];

                        for (int i = 0; i < oDTPesquisa.Rows.Count; i++)
                        {
                            arrRespostas[i, 0] = "";//cdQuestinario
                            arrRespostas[i, 1] = "";//cdGrupoPerguntas
                            arrRespostas[i, 2] = "";//cdQuestao
                            arrRespostas[i, 3] = "";//tpQuestao(somente uma/mais que uma/texto)
                            arrRespostas[i, 4] = "";//cdResposta(opt/chk)
                            arrRespostas[i, 5] = "";//dsResposta
                        }
                        Session["arrRespostas"] = arrRespostas;
                    }

                    if (idxPergunta == (oDTPesquisa.Rows.Count - 1))
                    {
                        idxPergunta = oDTPesquisa.Rows.Count - 1;
                        btnContinuarPesquisa.Visible = false;
                        btnConcluirPesquisa.Visible = true;
                    }
                    //else
                    LocalizarPergunta();
                }
            }
            
        }
        else
        {
            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionTipoSistema = (String)Session["SessionTipoSistema"];

            SessionFormulario = (String)Session["SessionFormulario"];

            tpRotina = (string)Session["tpRotina"];

            SessionIdioma = (String)Session["SessionIdioma"];

            oDTPesquisa = (DataTable)Session["oDTPesquisa"];

            idxPergunta = (Session["idxPergunta"] == null ? 0 : int.Parse(Session["idxPergunta"].ToString()));

            arrRespostas = (String[,])Session["arrRespostas"];

            //oDTGrupoPesquisa = (DataTable)Session["oDTGrupoPesquisa"];

            SessionPesquisasDeOpiniao = (PesquisasDeOpiniao)Session["SessionPesquisasDeOpiniao"];
        }

        if (SessionEvento == null)
            Server.Transfer("frmSessaoExpirada.aspx", true);

        verificarIdioma(SessionIdioma);

        ToolkitScriptManager1.RegisterPostBackControl(btnContinuarPesquisa);
        ToolkitScriptManager1.RegisterPostBackControl(btnConcluirPesquisa);
        ToolkitScriptManager1.RegisterPostBackControl(btnAbandonarPesquisa);
    }

    protected void verificarIdioma(string prmIdioma)
    {
        if (prmIdioma == "PTBR")//(txtidioma.SelectedIndex == 0)
        {
            SessionIdioma = "PTBR";
            Session["SessionIdioma"] = SessionIdioma;

            lblTitulo.Text = "Pesquisa";
            lblID.Text = "Participante"; 
            if (SessionPesquisasDeOpiniao.TpFolhaResposta == "COMPLETA")
                btnContinuarPesquisa.Text = "Concluir";
            else
                btnContinuarPesquisa.Text = "Continuar";

            btnConcluirPesquisa.Text = "Concluir";

           
        }
        else if (prmIdioma == "ENUS")//(txtidioma.SelectedIndex == 1)
        {
            SessionIdioma = "ENUS";
            Session["SessionIdioma"] = SessionIdioma;

            lblTitulo.Text = "Quiz";
            lblID.Text = "Participant";
            btnContinuarPesquisa.Text = "Save";
            if (SessionPesquisasDeOpiniao.TpFolhaResposta == "COMPLETA")
                btnContinuarPesquisa.Text = "Save";
            else
                btnContinuarPesquisa.Text = "Continuar";

            btnConcluirPesquisa.Text = "Save";

        }
        else if (prmIdioma == "ESP")//(txtidioma.SelectedIndex == 2)
        {
            SessionIdioma = "ESP";
            Session["SessionIdioma"] = SessionIdioma;

            lblTitulo.Text = "Formulario De Inscripción";
            
        }
        else if (prmIdioma == "FRA")//(txtidioma.SelectedIndex == 3)
        {
            SessionIdioma = "FRA";
            Session["SessionIdioma"] = SessionIdioma;

            lblTitulo.Text = "Formulaire d'inscription";
            
        }
    }

    private DataTable localizarGrupoPesquisa(string prmCdEvento, string prmCdPesquisa)
    {
        string comando =
            "SELECT q.cdQuestionario, " +
            "       q.dsQuestionario, " +
            "       g.cdGrupoPergunta, " +
            "       g.dsGrupoPergunta " +
            "  FROM tbquestionarios q " +
            "  join tbGruposPerguntas g " +
            "    on q.cdQuestionario = g.cdQuestionario " +
            "WHERE q.cdEvento = '" + prmCdEvento + "'  " +
            "and (q.flAtivo = 1)  " +
            "and (g.flAtivo = 1)  " +
            "and (q.flVisivelLocal = 1)  " +
            "and (g.flVisivelLocal = 1)  " +
            "AND getdate() > q.dtLiberacao " +

            "and (q.cdAtividade = ''  " +
            " OR q.cdAtividade IN (SELECT cdAtividade  " +
            "                  FROM tbMatriculas AS m  " +
            "                 WHERE (cdParticipante = '" + SessionParticipante.CdParticipante + "')  " +
            "                   AND (cdEvento = '" + prmCdEvento + "' ))) ";

        comando +=
            " and q.cdQuestionario = '" + prmCdPesquisa + "' ";

        if ((SessionParticipante != null) && (SessionParticipante.CdParticipante != ""))
        {
            comando += "  AND (q.cdQuestionario + g.cdGrupoPergunta NOT IN " +
                     "(SELECT cdQuestionario + cdGrupoPergunta " +
                        "FROM tbQuestoesRespostas AS tqr " +
                       "WHERE (cdParticipante = '" + SessionParticipante.CdParticipante + "') " +
                        " AND (cdEvento = '" + prmCdEvento + "'))) ";
        }

        SqlCommand cmd = new SqlCommand(
                    comando, SessionCnn);



        DataTable DTPesq = new DataTable();
        SqlDataAdapter Dap;


        Dap = new SqlDataAdapter(cmd);

        Dap.TableMappings.Add("GrupoPesquisa", "tbGrupoPesquisa");
        Dap.Fill(DTPesq);


        //oDTPesquisa = DTPesq;
        //Session["oDTPesquisa"] = oDTPesquisa;



        //SqlDataSource2.ConnectionString = SessionCnn.ConnectionString;

        //SqlDataSource2.SelectCommand = cmd.CommandText;
                
        //DataList0.DataSourceID = "SqlDataSource2";
        //DataList0.DataSource = DTPesq;
        //DataList0.DataBind();

        // if (((DataTable)DataList1.DataSource) == null || ((DataTable)DataList1.DataSource).Rows.Count <= 0)
        //     DataList1.Visible = false;

        return DTPesq;

    }

    private DataTable localizarGrupoPesquisa(string prmCdEvento)
    {
        string comando =
            "SELECT q.cdQuestionario, " +
            "       q.dsQuestionario, " +
            "       g.cdGrupoPergunta, " +
            "       g.dsGrupoPergunta " +
            "  FROM tbquestionarios q " +
            "  join tbGruposPerguntas g " +
            "    on q.cdQuestionario = g.cdQuestionario " +
            "WHERE q.cdEvento = '" + prmCdEvento + "'  " +
            "and (q.flAtivo = 1)  " +
            "and (g.flAtivo = 1)  " +
            "and (q.flVisivelLocal = 1)  " +
            "and (g.flVisivelLocal = 1)  " +
            "AND getdate() > q.dtLiberacao " +

        "and (q.cdAtividade = ''  " +
            " OR q.cdAtividade IN (SELECT cdAtividade  " +
            "                  FROM tbMatriculas AS m  " +
            "                 WHERE (cdParticipante = '" + SessionParticipante.CdParticipante + "')  " +
            "                   AND (cdEvento = '" + prmCdEvento + "' ))) ";

        //comando +=
        //    " and q.cdQuestionario = '" + prmCdPesquisa + "' ";

        if ((SessionParticipante != null) && (SessionParticipante.CdParticipante != ""))
        {
            comando += "  AND (q.cdQuestionario + g.cdGrupoPergunta NOT IN " +
                     "(SELECT cdQuestionario + cdGrupoPergunta " +
                        "FROM tbQuestoesRespostas AS tqr " +
                       "WHERE (cdParticipante = '" + SessionParticipante.CdParticipante + "') " +
                        " AND (cdEvento = '" + prmCdEvento + "'))) ";
        }

        SqlCommand cmd = new SqlCommand(
                    comando, SessionCnn);



        DataTable DTPesq = new DataTable();
        SqlDataAdapter Dap;


        Dap = new SqlDataAdapter(cmd);

        Dap.TableMappings.Add("GrupoPesquisa", "tbGrupoPesquisa");
        Dap.Fill(DTPesq);


        //oDTPesquisa = DTPesq;
        //Session["oDTPesquisa"] = oDTPesquisa;



        //SqlDataSource2.ConnectionString = SessionCnn.ConnectionString;

        //SqlDataSource2.SelectCommand = cmd.CommandText;

        //DataList0.DataSourceID = "SqlDataSource2";
        //DataList0.DataSource = DTPesq;
        //DataList0.DataBind();

        // if (((DataTable)DataList1.DataSource) == null || ((DataTable)DataList1.DataSource).Rows.Count <= 0)
        //     DataList1.Visible = false;

        return DTPesq;

    }


    private void localizarPesquisa(string prmCdEvento, string prmCdPesquisa, string prmCdGrupo, DataListItem prmItem)
    {
        string comando =
            "SELECT q.cdQuestionario, " +
            "       q.dsQuestionario, " +
            "       g.cdGrupoPergunta, " +
            "       g.dsGrupoPergunta, " +
            "       cdQuestao,  " +
            "       dsQuestao,   " +
            "       TpQuestao,   " +//--- comentado só para pesquisa mobile conasems
            //"       case when ((q.cdQuestionario = '001304001') and (cdQuestao = '011')) then 1 else 0 end TpQuestao,   " +
            "       p.flAtivo,   " +
            "       p.nrColunas   " +//--- comentado só para pesquisa mobile conasems
            //"       case when ((q.cdQuestionario = '001304001') and (cdQuestao = '011')) then 3 else 1 end nrColunas   " +
            "  FROM tbquestionarios q " +
            "  join tbGruposPerguntas g " +
            "    on q.cdQuestionario = g.cdQuestionario " +
            "  join tbQuestoes p " +
            "    on q.cdQuestionario = p.cdQuestionario " +
            "   and g.cdGrupoPergunta = p.cdGrupoPergunta " +
            "WHERE q.cdEvento = '" + prmCdEvento + "'  " +
            "and (q.flAtivo = 1)  " +
            "and (g.flAtivo = 1)  " +
            "and (q.flVisivelLocal = 1)  " +
            "and (g.flVisivelLocal = 1)  " +
            "and (p.flAtivo = 1) " +
            "AND getdate() > q.dtLiberacao ";// +

            //"and (q.cdAtividade = ''  " +
            //" OR q.cdAtividade IN (SELECT cdAtividade  " +
            //"                  FROM tbMatriculas AS m  " +
            //"                 WHERE (cdParticipante = '" + SessionParticipante.CdParticipante + "')  " +
            //"                   AND (cdEvento = '" + prmCdEvento + "' ))) ";

        comando +=
            " and p.cdQuestionario = '" + prmCdPesquisa + "' " +
            " and p.cdGrupoPergunta like '%" + prmCdGrupo + "%' ";
        
        if ((SessionParticipante != null) && (SessionParticipante.CdParticipante != ""))
        {
            comando += "  AND (q.cdQuestionario + g.cdGrupoPergunta NOT IN " +
                     "(SELECT cdQuestionario + cdGrupoPergunta " +
                        "FROM tbQuestoesRespostas AS tqr " +
                       "WHERE (cdParticipante = '" + SessionParticipante.CdParticipante + "') " +
                        " AND (cdEvento = '" + prmCdEvento + "'))) ";
        }

        SqlCommand cmd = new SqlCommand(
                    comando, SessionCnn);



        DataTable DTPesq = new DataTable();
        SqlDataAdapter Dap;


        Dap = new SqlDataAdapter(cmd);

        Dap.TableMappings.Add("Pesquisa", "tbPesquisa");
        Dap.Fill(DTPesq);


        oDTPesquisa = DTPesq;
        Session["oDTPesquisa"] = oDTPesquisa;


        SqlDataSource sqldatasouce = new SqlDataSource();
        
        
        //SqlDataSource1.ConnectionString = SessionCnn.ConnectionString;
        //SqlDataSource1.SelectCommand = cmd.CommandText;

        //sqldatasouce.ConnectionString = SessionCnn.ConnectionString;
        //sqldatasouce.SelectCommand = cmd.CommandText;
        //sqldatasouce.o

        if (prmItem != null)
        {
            DataList DataList1 = (DataList)prmItem.FindControl("DataList1");
            DataList1.DataSource = DTPesq;// sqldatasouce;//.DataSourceID = "SqlDataSource1";
            DataList1.DataBind();
        }       

        // if (((DataTable)DataList1.DataSource) == null || ((DataTable)DataList1.DataSource).Rows.Count <= 0)
        //     DataList1.Visible = false;



    }

    protected void LocalizarPergunta()
    {
        
        cdGrupoPergunta.Text = oDTPesquisa.DefaultView[idxPergunta]["cdGrupoPergunta"].ToString();
        dsGrupoPergunta.Text = oDTPesquisa.DefaultView[idxPergunta]["dsGrupoPergunta"].ToString();

        cdQuestaoLabel.Text = oDTPesquisa.DefaultView[idxPergunta]["cdQuestao"].ToString();
        dsQuestaoLabel.Text = oDTPesquisa.DefaultView[idxPergunta]["dsQuestao"].ToString();


        DataSet ds = GetDataSet(
                oDTPesquisa.DefaultView[idxPergunta]["cdQuestionario"].ToString(),
                oDTPesquisa.DefaultView[idxPergunta]["cdGrupoPergunta"].ToString(),
                oDTPesquisa.DefaultView[idxPergunta]["cdQuestao"].ToString(),
                oDTPesquisa.DefaultView[idxPergunta]["TpQuestao"].ToString(),
                "");
        //if ((SessionPesquisasDeOpiniao.TpFolhaResposta != "COMPLETA") && (SessionEvento.CdEvento == "001304") && (oDTPesquisa.DefaultView[idxPergunta]["cdQuestionario"].ToString() == "001304001") && (oDTPesquisa.DefaultView[idxPergunta]["cdGrupoPergunta"].ToString() == "001") && (oDTPesquisa.DefaultView[idxPergunta]["cdQuestao"].ToString() == "007"))
        if ((SessionPesquisasDeOpiniao.TpFolhaResposta != "COMPLETA") && (SessionEvento.CdEvento == "001305") && (oDTPesquisa.DefaultView[idxPergunta]["cdQuestionario"].ToString() == "001305001") && (oDTPesquisa.DefaultView[idxPergunta]["cdGrupoPergunta"].ToString() == "001") && (oDTPesquisa.DefaultView[idxPergunta]["cdQuestao"].ToString() == "007"))
        {
            ds = GetDataSet(
                oDTPesquisa.DefaultView[idxPergunta]["cdQuestionario"].ToString(),
                oDTPesquisa.DefaultView[idxPergunta]["cdGrupoPergunta"].ToString(),
                oDTPesquisa.DefaultView[idxPergunta]["cdQuestao"].ToString(),
                oDTPesquisa.DefaultView[idxPergunta]["TpQuestao"].ToString(),
                arrRespostas[idxPergunta - 1, 4]);
        }

        if (SessionIdioma == "PTBR")
        {
            RequiredFieldValidator9.ErrorMessage = RequiredFieldValidator10.ErrorMessage = "Selecione uma das alternativas";
        }
        else if (SessionIdioma == "PTBR")
        {
            RequiredFieldValidator9.ErrorMessage = RequiredFieldValidator10.ErrorMessage = "Select one of the alternatives";
        }

        switch (oDTPesquisa.DefaultView[idxPergunta]["TpQuestao"].ToString())
        {
            

            case "0":
                //if ((SessionEvento.CdEvento == "001304") && (oDTPesquisa.DefaultView[idxPergunta]["cdQuestionario"].ToString() == "001304001"))
                if ((SessionEvento.CdEvento == "001305") && (oDTPesquisa.DefaultView[idxPergunta]["cdQuestionario"].ToString() == "001305001"))
                {
                    if (SessionParticipante.NoAreaAtuacao == "NÃO")
                    {
                        if (idxPergunta == 0)
                        {
                            arrRespostas[idxPergunta, 0] = oDTPesquisa.DefaultView[idxPergunta]["cdQuestionario"].ToString();
                            arrRespostas[idxPergunta, 1] = oDTPesquisa.DefaultView[idxPergunta]["cdGrupoPergunta"].ToString();
                            arrRespostas[idxPergunta, 2] = oDTPesquisa.DefaultView[idxPergunta]["cdQuestao"].ToString();
                            arrRespostas[idxPergunta, 3] = oDTPesquisa.DefaultView[idxPergunta]["TpQuestao"].ToString();
                            arrRespostas[idxPergunta, 4] = "00";

                            idxPergunta++;
                            arrRespostas[idxPergunta, 0] = oDTPesquisa.DefaultView[idxPergunta]["cdQuestionario"].ToString();
                            arrRespostas[idxPergunta, 1] = oDTPesquisa.DefaultView[idxPergunta]["cdGrupoPergunta"].ToString();
                            arrRespostas[idxPergunta, 2] = oDTPesquisa.DefaultView[idxPergunta]["cdQuestao"].ToString();
                            arrRespostas[idxPergunta, 3] = oDTPesquisa.DefaultView[idxPergunta]["TpQuestao"].ToString();
                            arrRespostas[idxPergunta, 4] = "00";

                            idxPergunta++;
                            Session["idxPergunta"] = idxPergunta;
                            LocalizarPergunta();

                            return;
                        }
                    }
                }
                
                optResp.Visible = true;
                chkResp.Visible = false;
                ddlResp.Visible = false;
                txtResp.Visible = false;

                RequiredFieldValidator9.Visible = true;
                RequiredFieldValidator10.Visible = false;
                
                //ListItem l1;

                //for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                //{
                //    l1 = new ListItem();
                //    l1.Text = ds.Tables[0].DefaultView[k]["dsQuestaoItem"].ToString();
                //    l1.Value = ds.Tables[0].DefaultView[k]["cdQuestaoItem"].ToString(); 
                //    l1.ii
                //    optResp.Items.Add(l1);
                //}
                

                optResp.DataSource = ds;
                optResp.DataTextField = "dsQuestaoItem";
                optResp.DataValueField = "cdQuestaoItem";


                optResp.RepeatColumns = int.Parse(oDTPesquisa.DefaultView[idxPergunta]["nrColunas"].ToString());


                if ((SessionEvento.CdEvento == "005502") && (oDTPesquisa.DefaultView[idxPergunta]["cdQuestionario"].ToString() == "005502001") && (oDTPesquisa.DefaultView[idxPergunta]["cdQuestao"].ToString() == "004"))
                {
                    optResp.AutoPostBack = true;
                    optResp.SelectedIndexChanged += new EventHandler(this.optSelectedIndexChanged);
                }


                optResp.DataBind();
                break;
            case "1":
                optResp.Visible = false;
                chkResp.Visible = true;
                ddlResp.Visible = false;
                txtResp.Visible = false;

                RequiredFieldValidator9.Visible = false;
                RequiredFieldValidator10.Visible = false;

                chkResp.DataSource = ds;
                chkResp.DataTextField = "dsQuestaoItem";
                chkResp.DataValueField = "cdQuestaoItem";
                chkResp.RepeatColumns = int.Parse(oDTPesquisa.DefaultView[idxPergunta]["nrColunas"].ToString());
                chkResp.DataBind();
                break;
            case "2":
                optResp.Visible = false;
                chkResp.Visible = false;
                txtResp.Text = "";
                txtResp.Visible = true;

                RequiredFieldValidator9.Visible = false;
                RequiredFieldValidator10.Visible = false;

                break;
            case "3":

                //rf1.Visible = true;

                //if ((SessionEvento.CdEvento == "001304") && (oDTPesquisa.DefaultView[idxPergunta]["cdQuestionario"].ToString() == "001304001"))
                if ((SessionEvento.CdEvento == "001305") && (oDTPesquisa.DefaultView[idxPergunta]["cdQuestionario"].ToString() == "001305001"))
                {
                    if (SessionParticipante.NoAreaAtuacao == "NÃO")
                    {
                        if (idxPergunta == 0)
                        {
                            arrRespostas[idxPergunta, 0] = oDTPesquisa.DefaultView[idxPergunta]["cdQuestionario"].ToString();
                            arrRespostas[idxPergunta, 1] = oDTPesquisa.DefaultView[idxPergunta]["cdGrupoPergunta"].ToString();
                            arrRespostas[idxPergunta, 2] = oDTPesquisa.DefaultView[idxPergunta]["cdQuestao"].ToString();
                            arrRespostas[idxPergunta, 3] = oDTPesquisa.DefaultView[idxPergunta]["TpQuestao"].ToString();
                            arrRespostas[idxPergunta, 4] = "00";

                            idxPergunta++;
                            arrRespostas[idxPergunta, 0] = oDTPesquisa.DefaultView[idxPergunta]["cdQuestionario"].ToString();
                            arrRespostas[idxPergunta, 1] = oDTPesquisa.DefaultView[idxPergunta]["cdGrupoPergunta"].ToString();
                            arrRespostas[idxPergunta, 2] = oDTPesquisa.DefaultView[idxPergunta]["cdQuestao"].ToString();
                            arrRespostas[idxPergunta, 3] = oDTPesquisa.DefaultView[idxPergunta]["TpQuestao"].ToString();
                            arrRespostas[idxPergunta, 4] = "00";

                            idxPergunta++;
                            Session["idxPergunta"] = idxPergunta;
                            LocalizarPergunta();

                            return;
                        }
                    }
                }

                ddlResp.ToolTip = "Q_" + oDTPesquisa.DefaultView[idxPergunta]["cdQuestao"].ToString();

                optResp.Visible = false;
                chkResp.Visible = false;
                ddlResp.Visible = true;
                txtResp.Visible = false;

                RequiredFieldValidator9.Visible = false;
                RequiredFieldValidator10.Visible = true;

                ddlResp.DataSource = ds;
                ddlResp.DataTextField = "dsQuestaoItem";
                ddlResp.DataValueField = "cdQuestaoItem";

                ddlResp.DataBind();
                break;
        }
        
    }

    protected void DataList0_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
        {

            HiddenField cdPesquisa = (HiddenField)e.Item.FindControl("HiddenField5");
            Label lbl_cdGrupoPesquisa = (Label)e.Item.FindControl("lbl_cdGrupoPergunta");

            

            int tmpIndx = e.Item.ItemIndex;
            localizarPesquisa(SessionEvento.CdEvento, cdPesquisa.Value, lbl_cdGrupoPesquisa.Text, e.Item);

            
        }
    }
    string tmpCdQuestionario = "";
    protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        //if (e.Item.ItemType == ListItemType.Footer)
        //{
        //    HiddenField cdPesquisaR = (HiddenField)e.Item.FindControl("HiddenField50");
        //    Button btnconcluirParcial = (Button)e.Item.FindControl("btnConcluirParcial");
        //    btnconcluirParcial.ID = "btn_" + tmpCdQuestionario;
        //    btnconcluirParcial.CausesValidation = false;
        //    btnconcluirParcial.ValidationGroup = "VG_" + tmpCdQuestionario;
        //    btnconcluirParcial.CssClass = "botoes";
        //}
        if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
        {
            
            HiddenField anstype = (HiddenField)e.Item.FindControl("HiddenField1");
            HiddenField cdPesquisa = (HiddenField)e.Item.FindControl("HiddenField2");
            HiddenField cdGrupoPesquisa = (HiddenField)e.Item.FindControl("HiddenField3");

            HiddenField nrColunas = (HiddenField)e.Item.FindControl("HiddenField4");                      

            Label questionid = (Label)e.Item.FindControl("cdQuestaoLabel");
            questionid.ID = "L_" + questionid.Text;
            RadioButtonList rbl = (RadioButtonList)e.Item.FindControl("RadioButtonList1");
            CheckBoxList cbl = (CheckBoxList)e.Item.FindControl("CheckBoxList1");
            DropDownList dbl = (DropDownList)e.Item.FindControl("DropDownList1");
            TextBox txt = (TextBox)e.Item.FindControl("TextBox1");
            DataSet ds = GetDataSet(cdPesquisa.Value.ToString(), cdGrupoPesquisa.Value.ToString(), questionid.Text, anstype.Value, "");

            Table tb = (Table)e.Item.FindControl("tb1");

            RequiredFieldValidator rf = (RequiredFieldValidator)e.Item.FindControl("RequiredFieldValidator3");
            //rf.ValidationGroup = "VG_" + cdPesquisa.Value;
            RequiredFieldValidator rf1 = (RequiredFieldValidator)e.Item.FindControl("RequiredFieldValidator4");
            //rf1.ValidationGroup = "VG_" + cdPesquisa.Value;

            if (SessionIdioma == "PTBR")
            {
                rf.ErrorMessage = rf1.ErrorMessage = "Selecione uma das alternativas";
            }
            else if (SessionIdioma == "ENUS")
            {
                rf.ErrorMessage = rf1.ErrorMessage = "Select one of the alternatives";
            }

            tmpCdQuestionario = cdPesquisa.Value;

            switch (anstype.Value)
            {
                case "0":

                    //rf.Visible = true;
                    //rf.ControlToValidate = rbl.ID;

                    rbl.Visible = true;
                    cbl.Visible = false;
                    dbl.Visible = false;
                    txt.Visible = false;
                    rbl.DataSource = ds;
                    rbl.DataTextField = "dsQuestaoItem";
                    rbl.DataValueField = "cdQuestaoItem";
                    rbl.RepeatColumns = int.Parse(nrColunas.Value);                                                          

                    rbl.DataBind();
                    break;
                case "1":
                                        
                    rbl.Visible = false;
                    cbl.Visible = true;
                    dbl.Visible = false;
                    txt.Visible = false;
                    cbl.DataSource = ds;
                    cbl.DataTextField = "dsQuestaoItem";
                    cbl.DataValueField = "cdQuestaoItem";
                    cbl.RepeatColumns = int.Parse(nrColunas.Value);
                    cbl.DataBind();
                    break;
                case "2":
                    
                    rbl.Visible = false;
                    cbl.Visible = false;
                    dbl.Visible = false;
                    txt.Visible = true;
                    break;
                case "3":
                    
                    rf1.Visible = true;
                    //rf1.ControlToValidate = dbl.ID;

                    dbl.ToolTip = "Q_" + cdPesquisa.Value+"_"+questionid.Text;

                    rbl.Visible = false;
                    cbl.Visible = false;
                    dbl.Visible = true;
                    txt.Visible = false;
                    dbl.DataSource = ds;
                    dbl.DataTextField = "dsQuestaoItem";
                    dbl.DataValueField = "cdQuestaoItem";

                    rf1.InitialValue = "";

                    //if (SessionEvento.CdEvento == "001304")
                    if (SessionEvento.CdEvento == "001305")
                    {
                        DataTable dtTemp = new DataTable();
                        dtTemp.Columns.Add("cdQuestaoItem");
                        dtTemp.Columns.Add("dsQuestaoItem");

                        dtTemp.Rows.Add("00", "NÃO SE APLICA");

                        //if ((cdPesquisa.Value == "001304001") && (cdGrupoPesquisa.Value == "001"))
                        if ((cdPesquisa.Value == "001305001") && (cdGrupoPesquisa.Value == "001"))
                        {
                            if (SessionParticipante.NoAreaAtuacao == "SIM")
                            {

                                if (questionid.Text == "006")
                                {
                                    dbl.SelectedValue = "01";
                                    dbl.Enabled = false;
                                    tb.Visible = false;
                                }
                                if (questionid.Text == "007")
                                {
                                    dbl.SelectedValue = "03";
                                    dbl.Enabled = false;
                                    tb.Visible = false;
                                }

                                if (questionid.Text == "008")
                                {
                                    dbl.DataSource = dtTemp;
                                    dbl.Enabled = false;
                                    tb.Visible = false;
                                }

                            }
                            else
                            {

                                if (questionid.Text == "006")
                                {

                                    dbl.AutoPostBack = true;
                                    dbl.SelectedIndexChanged += new EventHandler(DropDownList1_SelectedIndexChanged);
                                }

                                if (questionid.Text == "001")
                                {
                                    dbl.DataSource = dtTemp;
                                    dbl.Enabled = false;
                                    tb.Visible = false;
                                }
                                if (questionid.Text == "002")
                                {
                                    dbl.DataSource = dtTemp;
                                    dbl.Enabled = false;
                                    tb.Visible = false;
                                }
                                if (questionid.Text == "010")
                                {
                                    dbl.DataSource = dtTemp;
                                    dbl.Enabled = false;
                                    tb.Visible = false;
                                }
                                if (questionid.Text == "012")
                                {
                                    dbl.DataSource = dtTemp;
                                    dbl.Enabled = false;
                                    tb.Visible = false;
                                }
                            }
                        }
                    }
                    dbl.DataBind();
                    break;
            }
        }
    }

    private DataSet GetDataSet(string prm_cdQuestionario, string prm_cdGrupoPergunta, string prm_cdQuestao, string prm_tpQuestao, string prm_grupo)
    {        
        string comandoSQL = "";
        if (prm_tpQuestao == "3")
        {
            comandoSQL +=
            "select top 1 '' [cdQuestionario] " +
            "      ,'' [cdGrupoPergunta] " +
            "      ,'' [cdQuestao] " +
            "      ,'' [cdQuestaoItem] " +
            "      ,'' [dsQuestaoItem] " +
            "      ,1 [flAtivo] " +
                //"      ,[imgQuestaoItem] " +
            "  from tbQuestoesItens " +
            " UNION ";
        }
        comandoSQL += 
            "select [cdQuestionario] " +
            "      ,[cdGrupoPergunta] " +
            "      ,[cdQuestao] " +
            "      ,[cdQuestaoItem] " +
            "      ,[dsQuestaoItem] " +
            "      ,[flAtivo] " +
           // "      ,[imgQuestaoItem] " +
            "  from tbQuestoesItens " +
            " where cdQuestionario = @cdQuestionario " +
            "   and cdGrupoPergunta = @cdGrupoPergunta " +
            "   and cdQuestao = @cdQuestao " +
            "   and [flAtivo] = 1 ";

        //if ((SessionEvento.CdEvento == "001304") && (prm_cdQuestionario == "001304001") && (prm_cdGrupoPergunta == "001") && (prm_cdQuestao == "007"))
        if ((SessionEvento.CdEvento == "001305") && (prm_cdQuestionario == "001305001"))
        {
            if (prm_cdGrupoPergunta == "001") 
            {
                if ((prm_cdQuestao == "001") || (prm_cdQuestao == "002") || (prm_cdQuestao == "010") || (prm_cdQuestao == "012"))
                {
                    comandoSQL += "   and cdQuestaoItem <> '00' ";
                }

                if (prm_cdQuestao == "007")
                {
                    if (prm_grupo == "01")
                        comandoSQL += "   and cdQuestaoItem in ('01','02','04','09','10','15','16','17')";
                    else if (prm_grupo == "02")
                        comandoSQL += "   and cdQuestaoItem in ('05','06','07','08','09','15','16','17')";
                    else if (prm_grupo == "03")
                        comandoSQL += "   and cdQuestaoItem in ('11','12','13','14','15','16','17')";
                }
            }

            

        }
        
        SqlCommand cmd = new SqlCommand(comandoSQL, SessionCnn);
        SqlParameter p1 = new SqlParameter("@cdQuestionario", prm_cdQuestionario);
        SqlParameter p2 = new SqlParameter("@cdGrupoPergunta", prm_cdGrupoPergunta);
        SqlParameter p3 = new SqlParameter("@cdQuestao", prm_cdQuestao);
        //SqlParameter p4 = new SqlParameter("@cdEvento", "000401001");
        cmd.Parameters.Add(p1);
        cmd.Parameters.Add(p2);
        cmd.Parameters.Add(p3);
        //cmd.Parameters.Add(p4);
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        DataSet ds = new DataSet();
        da.Fill(ds, "tbQuestoesItens");
        return ds;
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (SessionPesquisasDeOpiniao.TpFolhaResposta != "COMPLETA")
            return;

        //if (SessionEvento.CdEvento == "001304")
        if (SessionEvento.CdEvento == "001305")
        {

            DropDownList actionsDDL = sender as DropDownList;

            //if (actionsDDL.ToolTip == "Q_001304001_006")
            if (actionsDDL.ToolTip == "Q_001305001_006")
            {
                DataList DataList1 = (DataList)DataList0.Items[0].FindControl("DataList1");
                
                //008
                DropDownList ddlq = (DropDownList)DataList1.Items[7].FindControl("DropDownList1");
                RequiredFieldValidator rfq = (RequiredFieldValidator)DataList1.Items[7].FindControl("RequiredFieldValidator4");
                Table tb1 = (Table)DataList1.Items[7].FindControl("tb1");
                if (actionsDDL.SelectedValue != "03")
                {
                    DataTable dtTemp = new DataTable();
                    dtTemp.Columns.Add("cdQuestaoItem");
                    dtTemp.Columns.Add("dsQuestaoItem");

                    dtTemp.Rows.Add("00", "NÃO SE APLICA");
                    ddlq.DataSource = dtTemp;
                    ddlq.DataBind();

                    //ddlq.SelectedValue = "";
                    ddlq.Enabled = false;
                    //ddlq.Visible = false;                
                    rfq.Visible = false;
                    tb1.Visible = false;
                }
                else
                {
                    //ddlq.DataSource = GetDataSet("001304001", "001", "008", "3", "");
                    ddlq.DataSource = GetDataSet("001305001", "001", "008", "3", "");
                    ddlq.DataBind();
                    ddlq.SelectedValue = "";
                    ddlq.Enabled = true;
                    //ddlq.Visible = false;                
                    rfq.Visible = true;
                    tb1.Visible = true;
                }

                //007
                DropDownList ddlq2 = (DropDownList)DataList1.Items[6].FindControl("DropDownList1");
                RequiredFieldValidator rfq2 = (RequiredFieldValidator)DataList1.Items[6].FindControl("RequiredFieldValidator4");
                Table tb2 = (Table)DataList1.Items[6].FindControl("tb1");
                if (actionsDDL.SelectedValue != "04")
                {
                    //ddlq2.DataSource = GetDataSet("001304001", "001", "007", "3", actionsDDL.SelectedValue.ToString());
                    ddlq2.DataSource = GetDataSet("001305001", "001", "007", "3", actionsDDL.SelectedValue.ToString());
                    ddlq2.DataBind();
                    ddlq2.Enabled = true;
                    rfq2.Visible = true;
                    tb2.Visible = true;
                }
                else
                {
                    DataTable dtTemp = new DataTable();
                    dtTemp.Columns.Add("cdQuestaoItem");
                    dtTemp.Columns.Add("dsQuestaoItem");

                    dtTemp.Rows.Add("00", "NÃO SE APLICA");

                    ddlq2.DataSource = dtTemp;// GetDataSet("001304001", "001", "007", "3", "");
                    ddlq2.DataBind();
                    ddlq2.SelectedValue = "00";
                    ddlq2.Enabled = false;
                    rfq2.Visible = false;
                    tb2.Visible = false;
                }
            }
            
        }
    }

    private Control FindControlRecursive(Control root, string id)
    {
        if (root.ID == id)
        {
            return root;
        }

        foreach (Control c in root.Controls)
        {
            Control t = FindControlRecursive(c, id);
            if (t != null)
            {
                return t;
            }
        }

        return null;
    }

    protected bool prpGravarPesquisaSimples()
    {
        PesquisasResposta oPesquisasResposta = new PesquisasResposta();
        

        int erros = 0;
        string[] tmpResp = null;

        int totresp = arrRespostas.GetLength(0);

        for (int i = 0; i < totresp; i++)
        {
            if (arrRespostas[i, 0] != "")
            {
                tmpResp = arrRespostas[i, 4].Split(';');

                if (tmpResp.Length <= 1)
                {
                    if (!oPesquisasResposta.IncluirRespostaItem(SessionEvento.CdEvento, SessionParticipante.CdParticipante, arrRespostas[i, 0], arrRespostas[i, 1], arrRespostas[i, 2], arrRespostas[i, 4], arrRespostas[i, 5], SessionCnn))
                        erros++;
                }
                else
                {
                    for (int j = 0; j < tmpResp.Length; j++)
                    {
                        if (!oPesquisasResposta.IncluirRespostaItem(SessionEvento.CdEvento, SessionParticipante.CdParticipante, arrRespostas[i, 0], arrRespostas[i, 1], arrRespostas[i, 2], tmpResp[j], arrRespostas[i, 5], SessionCnn))
                            erros++;
                    }
                }

            }
                

            
        }
        return true;
        
       
    }

    protected bool prpVerificarGravarPesquisaCompleta()
    {
        lblMsg.Text = "";
        //PesquisasResposta oPesquisasResposta = new PesquisasResposta();
        foreach (DataListItem item0 in DataList0.Items)
        {
            if (item0.ItemType == ListItemType.Item | item0.ItemType == ListItemType.AlternatingItem)
            {
                string tmpCargo = "";
                DataList DataList1 = (DataList)item0.FindControl("DataList1");
                foreach (DataListItem item in DataList1.Items)
                {
                    if (item.ItemType == ListItemType.Item | item.ItemType == ListItemType.AlternatingItem)
                    {
                        string cdQuestionario = ((HiddenField)item.FindControl("HiddenField2")).Value.ToString();
                        string cdGrupoPergunta = ((HiddenField)item.FindControl("HiddenField3")).Value.ToString();
                        string cdQuestao = ((Label)item.FindControl("cdQuestaoLabel")).Text;
                        string cdResposta = "";
                        string dsResposta = "";

                        HiddenField tpQuestao = (HiddenField)item.FindControl("HiddenField1");
                        switch (tpQuestao.Value)
                        {
                            case "0"://"S"
                                RadioButtonList rbl = (RadioButtonList)item.FindControl("RadioButtonList1");
                                cdResposta = rbl.SelectedValue.ToString();
                                //lblMsg.Text += tpQuestao.Value + "/" + cdQuestionario + "/" + cdGrupoPergunta + "/" + cdQuestao + "/" + cdResposta + "/" + dsResposta + "<br/>";
                                //SaveAnswer(cdQuestionario, cdGrupoPergunta, cdQuestao, cdResposta, dsResposta);
                                //oPesquisasResposta.IncluirRespostaItem(SessionEvento.CdEvento, SessionParticipante.CdParticipante,
                                //    cdQuestionario, cdGrupoPergunta, cdQuestao, cdResposta, dsResposta, SessionCnn);
                                tmpCargo = Geral.verificarCargoJaPreenchido(SessionEvento.CdEvento, SessionParticipante.CdParticipante, rbl.SelectedItem.ToString(), SessionParticipante.DsAuxiliar8, SessionParticipante.DsAuxiliar9, SessionCnn);
                                if (tmpCargo != "")
                                {
                                    lblMsg.Text = tmpCargo;
                                    return false;
                                }
                                break;
                            case "1"://"M"
                                /*CheckBoxList cbl = (CheckBoxList)item.FindControl("CheckBoxList1");
                                //Label2.Text = cbl.Items.Count.ToString();
                                for (int i = 0; i <= cbl.Items.Count - 1; i++)
                                {
                                    if (cbl.Items[i].Selected)
                                    {
                                        cdResposta = cbl.Items[i].Value;
                                        lblMsg.Text += tpQuestao.Value + "/" + cdQuestionario + "/" + cdGrupoPergunta + "/" + cdQuestao + "/" + cdResposta + "/" + dsResposta + "<br/>";
                                        //SaveAnswer(cdQuestionario, cdGrupoPergunta, cdQuestao, cdResposta, dsResposta);
                                        oPesquisasResposta.IncluirRespostaItem(SessionEvento.CdEvento, SessionParticipante.CdParticipante,
                                        cdQuestionario, cdGrupoPergunta, cdQuestao, cdResposta, dsResposta, SessionCnn);
                                    }
                                }*/

                                break;
                            case "2"://"T"
                                //TextBox txt = (TextBox)item.FindControl("TextBox1");
                                //dsResposta = txt.Text;
                                //cdResposta = "01";
                                //SaveAnswer(cdQuestionario, cdGrupoPergunta, cdQuestao, "01", dsResposta);
                                //oPesquisasResposta.IncluirRespostaItem(SessionEvento.CdEvento, SessionParticipante.CdParticipante,
                                //    cdQuestionario, cdGrupoPergunta, cdQuestao, cdResposta, dsResposta, SessionCnn);
                                break;
                            case "3"://"Lista"
                                DropDownList ddl = (DropDownList)item.FindControl("DropDownList1");
                                cdResposta = ddl.SelectedValue.ToString();
                                //lblMsg.Text += tpQuestao.Value + "/" + cdQuestionario + "/" + cdGrupoPergunta + "/" + cdQuestao + "/" + cdResposta + "/" + dsResposta + "<br/>";
                                //SaveAnswer(cdQuestionario, cdGrupoPergunta, cdQuestao, cdResposta, dsResposta);
                                //oPesquisasResposta.IncluirRespostaItem(SessionEvento.CdEvento, SessionParticipante.CdParticipante,
                                //    cdQuestionario, cdGrupoPergunta, cdQuestao, cdResposta, dsResposta, SessionCnn);
                                tmpCargo = Geral.verificarCargoJaPreenchido(SessionEvento.CdEvento, SessionParticipante.CdParticipante, ddl.SelectedItem.ToString(), SessionParticipante.DsAuxiliar8, SessionParticipante.DsAuxiliar9, SessionCnn);
                                if (tmpCargo != "")
                                {
                                    lblMsg.Text = tmpCargo;
                                    return false;
                                }
                                break;
                        }

                    }
                }
            }
        }
        //DataList1.Visible = false;
        lblMsg.Text = "";
        return true;

    }

    protected void prpGravarPesquisaCompleta()
    {
        PesquisasResposta oPesquisasResposta = new PesquisasResposta();
        foreach (DataListItem item0 in DataList0.Items)
        {
            if (item0.ItemType == ListItemType.Item | item0.ItemType == ListItemType.AlternatingItem)
            {
                DataList DataList1 = (DataList)item0.FindControl("DataList1");
                foreach (DataListItem item in DataList1.Items)
                {
                    if (item.ItemType == ListItemType.Item | item.ItemType == ListItemType.AlternatingItem)
                    {
                        string cdQuestionario = ((HiddenField)item.FindControl("HiddenField2")).Value.ToString();
                        string cdGrupoPergunta = ((HiddenField)item.FindControl("HiddenField3")).Value.ToString();
                        string cdQuestao = ((Label)item.FindControl("cdQuestaoLabel")).Text;
                        string cdResposta = "";
                        string dsResposta = "";

                        HiddenField tpQuestao = (HiddenField)item.FindControl("HiddenField1");
                        switch (tpQuestao.Value)
                        {
                            case "0"://"S"
                                RadioButtonList rbl = (RadioButtonList)item.FindControl("RadioButtonList1");
                                cdResposta = rbl.SelectedValue.ToString();
                                lblMsg.Text += tpQuestao.Value + "/" + cdQuestionario + "/" + cdGrupoPergunta + "/" + cdQuestao + "/" + cdResposta + "/" + dsResposta + "<br/>";
                                //SaveAnswer(cdQuestionario, cdGrupoPergunta, cdQuestao, cdResposta, dsResposta);
                                oPesquisasResposta.IncluirRespostaItem(SessionEvento.CdEvento, SessionParticipante.CdParticipante,
                                    cdQuestionario, cdGrupoPergunta, cdQuestao, cdResposta, dsResposta, SessionCnn);
                                break;
                            case "1"://"M"
                                CheckBoxList cbl = (CheckBoxList)item.FindControl("CheckBoxList1");
                                //Label2.Text = cbl.Items.Count.ToString();
                                for (int i = 0; i <= cbl.Items.Count - 1; i++)
                                {
                                    if (cbl.Items[i].Selected)
                                    {
                                        cdResposta = cbl.Items[i].Value;
                                        lblMsg.Text += tpQuestao.Value + "/" + cdQuestionario + "/" + cdGrupoPergunta + "/" + cdQuestao + "/" + cdResposta + "/" + dsResposta + "<br/>";
                                        //SaveAnswer(cdQuestionario, cdGrupoPergunta, cdQuestao, cdResposta, dsResposta);
                                        oPesquisasResposta.IncluirRespostaItem(SessionEvento.CdEvento, SessionParticipante.CdParticipante,
                                        cdQuestionario, cdGrupoPergunta, cdQuestao, cdResposta, dsResposta, SessionCnn);
                                    }
                                }

                                break;
                            case "2"://"T"
                                TextBox txt = (TextBox)item.FindControl("TextBox1");
                                dsResposta = txt.Text;
                                cdResposta = "01";
                                //SaveAnswer(cdQuestionario, cdGrupoPergunta, cdQuestao, "01", dsResposta);
                                oPesquisasResposta.IncluirRespostaItem(SessionEvento.CdEvento, SessionParticipante.CdParticipante,
                                    cdQuestionario, cdGrupoPergunta, cdQuestao, cdResposta, dsResposta, SessionCnn);
                                break;
                            case "3"://"Lista"
                                DropDownList ddl = (DropDownList)item.FindControl("DropDownList1");
                                cdResposta = ddl.SelectedValue.ToString();
                                lblMsg.Text += tpQuestao.Value + "/" + cdQuestionario + "/" + cdGrupoPergunta + "/" + cdQuestao + "/" + cdResposta + "/" + dsResposta + "<br/>";
                                //SaveAnswer(cdQuestionario, cdGrupoPergunta, cdQuestao, cdResposta, dsResposta);
                                oPesquisasResposta.IncluirRespostaItem(SessionEvento.CdEvento, SessionParticipante.CdParticipante,
                                    cdQuestionario, cdGrupoPergunta, cdQuestao, cdResposta, dsResposta, SessionCnn);
                                break;
                        }

                    }
                }
            }
        }
        //DataList1.Visible = false;
        lblMsg.Text += "Obrigado pela participação na pesquisa!";

    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        if (SessionPesquisasDeOpiniao.TpFolhaResposta == "COMPLETA")
        {

            //if ((SessionEvento.CdEvento == "001304") && (!prpVerificarGravarPesquisaCompleta()))
            //    return;

            prpGravarPesquisaCompleta();
            //if (SessionEvento.CdEvento != "001304")
            if (SessionEvento.CdEvento != "001305")
            {
                if ((SessionFormulario == null) || (SessionFormulario == ""))
                {
                    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}", "034"), true);
                }
                else
                {
                    Response.Write("<script>window.open('frmSelAtividades.aspx','_self');</script>");
                }
            }
            else
            {
                PesquisasDeOpiniao oPesquisasDeOpiniao = new PesquisasDeOpiniao();
                DataTable dtPesquisas = oPesquisasDeOpiniao.ListarPesquisasDoParticipante(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionParticipante.DsIdioma, SessionCnn);

                if ((dtPesquisas != null) && (dtPesquisas.Rows.Count > 0))
                {
                    Session["oDTPesquisa"] = null;
                    Response.Write("<script>window.open('frmPesquisaVinculada.aspx','_self');</script>");
                }
                else
                {
                    //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}",
                    //            "034"), true);

                    if ((SessionParticipante.NoAreaAtuacao == "SIM") && (SessionParticipante.DsAuxiliar19 != "SIM"))
                    {//SECRETÁRIOS MUNICIPAIS DE SAÚDE DEVEM RESPONDER A DECLARAÇÃO
                        Response.Write("<script>window.open('frmTermoConasemsSecretarios.aspx','_self');</script>");
                        return;
                    }

                    if (Geral.datahoraServidor(SessionCnn) > DateTime.Parse("30/07/2015 00:00:00"))
                    {


                        if ((Geral.datahoraServidor(SessionCnn) < DateTime.Parse("04/08/2015 00:00:00")))
                        {
                            if (SessionParticipante.CdCategoria != "00130501")
                            {
                                if ((SessionParticipante.Categoria.FlAtividades) && (!SessionParticipante.FlConfirmacaoInscricao))
                                {
                                    PedidoCad oPedidoCad = new PedidoCad();
                                    Pedido tempPedido = oPedidoCad.VerificaPedidoNaoPagoWeb(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);
                                    if ((tempPedido == null) || (tempPedido.TpPagamento == ""))
                                    {
                                        Response.Write("<script>window.open('frmSelAtividades.aspx','_self');</script>");
                                    }
                                }
                            }
                        }


                        if (SessionEvento.DtFechamentoInscrWeb < Geral.datahoraServidor(SessionCnn))
                        {

                            DataTable dtPesquisas2 = oPesquisasDeOpiniao.ListarPesquisasDoParticipante(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionParticipante.DsIdioma, SessionCnn);

                            if ((dtPesquisas2 == null) || (dtPesquisas2.Rows.Count <= 0))
                            {

                                if (!SessionParticipante.Categoria.FlCertificado)
                                {//não possui direito à certificado
                                    lblMsg.Text = "Categoria não possui direito certificação";
                                    return;
                                }

                                Server.Transfer(string.Format("frmCertificado.aspx?p={0}",
                                                   cllEventos.Crypto.EncryptStringAES(SessionParticipante.CdParticipante)), true);

                                lblMsg.Text = "Operação realizada com sucesso!";
                                btnFoco.Focus();

                                return;
                            }

                            //Response.Redirect("http://fazendomais.azurewebsites.net/index.aspx?codEvento=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + "&cdLng=PTBR&cat=&atv=&keyAut=" + "&tpSist=PSQVINC");
                            //return;
                        }

                        Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                            "014",
                                            (SessionParticipante.Categoria.FlPagamento ? "Dirija-se ao caixa no local do evento para concluir sua inscrição." : "Não esqueça de imprimir sua credencial.")
                                            ), true);

                        //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                        //                    "035",
                        //                    ""), true);
                    }

                    if (SessionParticipante.Categoria.FlAtividades)
                        Response.Write("<script>window.open('frmSelAtividades.aspx','_self');</script>");
                    else
                    {
                        Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                            "014",
                                            ""), true);
                    }
                }
            }
        }
        else
        {
            /*************************************************************************************/
            /*************************************************************************************/



            arrRespostas[idxPergunta, 0] = oDTPesquisa.DefaultView[idxPergunta]["cdQuestionario"].ToString();
            arrRespostas[idxPergunta, 1] = oDTPesquisa.DefaultView[idxPergunta]["cdGrupoPergunta"].ToString();
            arrRespostas[idxPergunta, 2] = oDTPesquisa.DefaultView[idxPergunta]["cdQuestao"].ToString();
            arrRespostas[idxPergunta, 3] = oDTPesquisa.DefaultView[idxPergunta]["TpQuestao"].ToString();

            arrRespostas[idxPergunta, 4] = "";
            arrRespostas[idxPergunta, 5] = "";

            switch (oDTPesquisa.DefaultView[idxPergunta]["TpQuestao"].ToString())
            {
                case "0"://"S"
                    for (int i = 0; i <= optResp.Items.Count - 1; i++)
                    {
                        if (optResp.Items[i].Selected)
                        {
                            arrRespostas[idxPergunta, 4] = optResp.Items[i].Value.ToString();
                            break;
                        }
                    }
                    break;
                case "1"://"M"

                    for (int i = 0; i <= chkResp.Items.Count - 1; i++)
                    {
                        if (chkResp.Items[i].Selected)
                        {

                            if (arrRespostas[idxPergunta, 4].Trim() == "")
                            {
                                arrRespostas[idxPergunta, 4] = chkResp.Items[i].Value.ToString();
                            }
                            else
                            {
                                arrRespostas[idxPergunta, 4] += ";" + chkResp.Items[i].Value.ToString();
                            }

                        }
                    }

                    break;
                case "2"://"T"

                    arrRespostas[idxPergunta, 4] = "01";
                    arrRespostas[idxPergunta, 5] = txtResp.Text;

                    break;
                case "3"://"ListBox"

                    arrRespostas[idxPergunta, 4] = ddlResp.SelectedValue.ToString();

                    break;
            }

            Session["arrRespostas"] = arrRespostas;

            idxPergunta++;

            #region MyRegion PESQUISA COMUNICATO


            if (SessionEvento.CdEvento == "000505")
            {
                if ((arrRespostas[0, 4] == "04") && (idxPergunta == 1))
                {
                    Session["idxPergunta"] = idxPergunta;
                    LocalizarPergunta();
                    btnContinuarPesquisa.Visible = false;
                    btnContinuarPesquisa.Visible = true;
                    return;
                }
                else if ((arrRespostas[0, 4] == "01") && (idxPergunta == 2))
                {
                    Session["idxPergunta"] = idxPergunta;
                    LocalizarPergunta();
                    btnContinuarPesquisa.Visible = false;
                    btnContinuarPesquisa.Visible = true;
                    return;
                }
                else if ((arrRespostas[0, 4] == "02") && (idxPergunta == 4))
                {
                    Session["idxPergunta"] = idxPergunta;
                    LocalizarPergunta();
                    btnContinuarPesquisa.Visible = false;
                    btnContinuarPesquisa.Visible = true;
                    return;
                }
                else if ((arrRespostas[0, 4] == "03") && (idxPergunta == 3))
                {
                    idxPergunta = idxPergunta + 2;
                    Session["idxPergunta"] = idxPergunta;
                    LocalizarPergunta();
                    return;
                }
            }

            #endregion

            //if ((SessionEvento.CdEvento == "001304") && (oDTPesquisa.DefaultView[idxPergunta]["cdQuestionario"].ToString() == "001304001"))
            if ((SessionEvento.CdEvento == "001305") && (oDTPesquisa.DefaultView[idxPergunta]["cdQuestionario"].ToString() == "001305001"))
            {
                if (SessionParticipante.NoAreaAtuacao == "SIM")
                {
                    if (idxPergunta == 5)
                    {
                        arrRespostas[idxPergunta, 0] = oDTPesquisa.DefaultView[idxPergunta]["cdQuestionario"].ToString();
                        arrRespostas[idxPergunta, 1] = oDTPesquisa.DefaultView[idxPergunta]["cdGrupoPergunta"].ToString();
                        arrRespostas[idxPergunta, 2] = oDTPesquisa.DefaultView[idxPergunta]["cdQuestao"].ToString();
                        arrRespostas[idxPergunta, 3] = oDTPesquisa.DefaultView[idxPergunta]["TpQuestao"].ToString();
                        arrRespostas[idxPergunta, 4] = "01";

                        idxPergunta++;
                        arrRespostas[idxPergunta, 0] = oDTPesquisa.DefaultView[idxPergunta]["cdQuestionario"].ToString();
                        arrRespostas[idxPergunta, 1] = oDTPesquisa.DefaultView[idxPergunta]["cdGrupoPergunta"].ToString();
                        arrRespostas[idxPergunta, 2] = oDTPesquisa.DefaultView[idxPergunta]["cdQuestao"].ToString();
                        arrRespostas[idxPergunta, 3] = oDTPesquisa.DefaultView[idxPergunta]["TpQuestao"].ToString();
                        arrRespostas[idxPergunta, 4] = "03";

                        idxPergunta++;
                        arrRespostas[idxPergunta, 0] = oDTPesquisa.DefaultView[idxPergunta]["cdQuestionario"].ToString();
                        arrRespostas[idxPergunta, 1] = oDTPesquisa.DefaultView[idxPergunta]["cdGrupoPergunta"].ToString();
                        arrRespostas[idxPergunta, 2] = oDTPesquisa.DefaultView[idxPergunta]["cdQuestao"].ToString();
                        arrRespostas[idxPergunta, 3] = oDTPesquisa.DefaultView[idxPergunta]["TpQuestao"].ToString();
                        arrRespostas[idxPergunta, 4] = "00";

                        idxPergunta++;
                        Session["idxPergunta"] = idxPergunta;
                        LocalizarPergunta();

                        return;
                    }
                    if (idxPergunta == 8)
                    {
                        arrRespostas[idxPergunta, 0] = oDTPesquisa.DefaultView[idxPergunta]["cdQuestionario"].ToString();
                        arrRespostas[idxPergunta, 1] = oDTPesquisa.DefaultView[idxPergunta]["cdGrupoPergunta"].ToString();
                        arrRespostas[idxPergunta, 2] = oDTPesquisa.DefaultView[idxPergunta]["cdQuestao"].ToString();
                        arrRespostas[idxPergunta, 3] = oDTPesquisa.DefaultView[idxPergunta]["TpQuestao"].ToString();
                        arrRespostas[idxPergunta, 4] = "00";

                        idxPergunta++;
                        Session["idxPergunta"] = idxPergunta;
                        LocalizarPergunta();

                        return;
                    }
                }
                else
                {
                    if ((idxPergunta == 6) && (arrRespostas[5, 4] == "04"))
                    {

                        arrRespostas[idxPergunta, 0] = oDTPesquisa.DefaultView[idxPergunta]["cdQuestionario"].ToString();
                        arrRespostas[idxPergunta, 1] = oDTPesquisa.DefaultView[idxPergunta]["cdGrupoPergunta"].ToString();
                        arrRespostas[idxPergunta, 2] = oDTPesquisa.DefaultView[idxPergunta]["cdQuestao"].ToString();
                        arrRespostas[idxPergunta, 3] = oDTPesquisa.DefaultView[idxPergunta]["TpQuestao"].ToString();
                        arrRespostas[idxPergunta, 4] = "00";
                        idxPergunta++;

                        arrRespostas[idxPergunta, 0] = oDTPesquisa.DefaultView[idxPergunta]["cdQuestionario"].ToString();
                        arrRespostas[idxPergunta, 1] = oDTPesquisa.DefaultView[idxPergunta]["cdGrupoPergunta"].ToString();
                        arrRespostas[idxPergunta, 2] = oDTPesquisa.DefaultView[idxPergunta]["cdQuestao"].ToString();
                        arrRespostas[idxPergunta, 3] = oDTPesquisa.DefaultView[idxPergunta]["TpQuestao"].ToString();
                        arrRespostas[idxPergunta, 4] = "00";
                        idxPergunta++;

                        Session["idxPergunta"] = idxPergunta;
                        LocalizarPergunta();
                        return;
                    }

                    if ((idxPergunta == 7) && (arrRespostas[5, 4] != "03"))
                    {
                        arrRespostas[idxPergunta, 0] = oDTPesquisa.DefaultView[idxPergunta]["cdQuestionario"].ToString();
                        arrRespostas[idxPergunta, 1] = oDTPesquisa.DefaultView[idxPergunta]["cdGrupoPergunta"].ToString();
                        arrRespostas[idxPergunta, 2] = oDTPesquisa.DefaultView[idxPergunta]["cdQuestao"].ToString();
                        arrRespostas[idxPergunta, 3] = oDTPesquisa.DefaultView[idxPergunta]["TpQuestao"].ToString();
                        arrRespostas[idxPergunta, 4] = "00";

                        idxPergunta++;
                        Session["idxPergunta"] = idxPergunta;
                        LocalizarPergunta();
                        return;
                    }

                    if (idxPergunta == 9)
                    {
                        arrRespostas[idxPergunta, 0] = oDTPesquisa.DefaultView[idxPergunta]["cdQuestionario"].ToString();
                        arrRespostas[idxPergunta, 1] = oDTPesquisa.DefaultView[idxPergunta]["cdGrupoPergunta"].ToString();
                        arrRespostas[idxPergunta, 2] = oDTPesquisa.DefaultView[idxPergunta]["cdQuestao"].ToString();
                        arrRespostas[idxPergunta, 3] = oDTPesquisa.DefaultView[idxPergunta]["TpQuestao"].ToString();
                        arrRespostas[idxPergunta, 4] = "00";

                        idxPergunta++;
                        Session["idxPergunta"] = idxPergunta;

                        btnContinuarPesquisa.Visible = false;
                        btnConcluirPesquisa.Visible = true;


                        arrRespostas[idxPergunta + 1, 0] = oDTPesquisa.DefaultView[idxPergunta + 1]["cdQuestionario"].ToString();
                        arrRespostas[idxPergunta + 1, 1] = oDTPesquisa.DefaultView[idxPergunta + 1]["cdGrupoPergunta"].ToString();
                        arrRespostas[idxPergunta + 1, 2] = oDTPesquisa.DefaultView[idxPergunta + 1]["cdQuestao"].ToString();
                        arrRespostas[idxPergunta + 1, 3] = oDTPesquisa.DefaultView[idxPergunta + 1]["TpQuestao"].ToString();
                        arrRespostas[idxPergunta + 1, 4] = "00";


                        LocalizarPergunta();
                        return;
                    }

                    if (idxPergunta == 11)
                    {

                        arrRespostas[idxPergunta, 0] = oDTPesquisa.DefaultView[idxPergunta]["cdQuestionario"].ToString();
                        arrRespostas[idxPergunta, 1] = oDTPesquisa.DefaultView[idxPergunta]["cdGrupoPergunta"].ToString();
                        arrRespostas[idxPergunta, 2] = oDTPesquisa.DefaultView[idxPergunta]["cdQuestao"].ToString();
                        arrRespostas[idxPergunta, 3] = oDTPesquisa.DefaultView[idxPergunta]["TpQuestao"].ToString();
                        arrRespostas[idxPergunta, 4] = "00";


                        Session["idxPergunta"] = idxPergunta;
                        //LocalizarPergunta();
                        //return;
                    }
                }


            }


            Session["idxPergunta"] = idxPergunta;
            LocalizarPergunta();

            if (idxPergunta >= (oDTPesquisa.Rows.Count - 1))
            {
                idxPergunta = oDTPesquisa.Rows.Count - 1;
                Session["idxPergunta"] = idxPergunta;
                btnContinuarPesquisa.Visible = false;
                btnConcluirPesquisa.Visible = true;
            }



            //Server.Transfer("frmPesquisaAvulsaProc.aspx", true);
        }
          
    }
    protected void btnConcluir_Click1(object sender, EventArgs e)
    {
        
        arrRespostas[idxPergunta, 0] = oDTPesquisa.DefaultView[idxPergunta]["cdQuestionario"].ToString();
        arrRespostas[idxPergunta, 1] = oDTPesquisa.DefaultView[idxPergunta]["cdGrupoPergunta"].ToString();
        arrRespostas[idxPergunta, 2] = oDTPesquisa.DefaultView[idxPergunta]["cdQuestao"].ToString();
        arrRespostas[idxPergunta, 3] = oDTPesquisa.DefaultView[idxPergunta]["TpQuestao"].ToString();

        arrRespostas[idxPergunta, 4] = "";
        arrRespostas[idxPergunta, 5] = "";

        switch (oDTPesquisa.DefaultView[idxPergunta]["TpQuestao"].ToString())
        {
            case "0"://"S"
                for (int i = 0; i <= optResp.Items.Count - 1; i++)
                {
                    if (optResp.Items[i].Selected)
                    {
                        arrRespostas[idxPergunta, 4] = optResp.Items[i].Value.ToString();
                        break;
                    }
                }
                break;
            case "1"://"M"

                for (int i = 0; i <= chkResp.Items.Count - 1; i++)
                {
                    if (chkResp.Items[i].Selected)
                    {

                        if (arrRespostas[idxPergunta, 4].Trim() == "")
                        {
                            arrRespostas[idxPergunta, 4] = chkResp.Items[i].Value.ToString();
                        }
                        else
                        {
                            arrRespostas[idxPergunta, 4] += ";" + chkResp.Items[i].Value.ToString();
                        }

                    }
                }

                break;
            case "2"://"T"

                arrRespostas[idxPergunta, 4] = "01";
                arrRespostas[idxPergunta, 5] = txtResp.Text;

                break;
            case "3"://"listBox"

                arrRespostas[idxPergunta, 4] = ddlResp.SelectedValue.ToString();
                arrRespostas[idxPergunta, 5] = txtResp.Text;

                break;
        }

        Session["arrRespostas"] = arrRespostas;

        if (prpGravarPesquisaSimples())
        {
            Session["oDTPesquisa"] = null;
            Session["arrRespostas"] = null;
            Session["idxPergunta"] = null;

            //if (SessionEvento.CdEvento != "001304") 
            if (SessionEvento.CdEvento != "001305") 
                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}",
                                "034"), true);
            else
            {
                PesquisasDeOpiniao oPesquisasDeOpiniao = new PesquisasDeOpiniao();
                DataTable dtPesquisas = oPesquisasDeOpiniao.ListarPesquisasDoParticipante(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionParticipante.DsIdioma, SessionCnn);

                if ((dtPesquisas != null) && (dtPesquisas.Rows.Count > 0))
                {
                    Session["oDTPesquisa"] = null;
                    Response.Write("<script>window.open('frmPesquisaVinculada.aspx','_self');</script>");
                }
                else
                {
                    if (SessionEvento.DtFechamentoInscrWeb < Geral.datahoraServidor(SessionCnn))
                    {
                        Response.Redirect("http://fazendomais.azurewebsites.net/inscr.aspx?codEvento=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + "&cdLng=PTBR&cat=&atv=&keyAut=" + "&tpSist=PSQVINC");
                        return;
                    }
                    else
                        Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                            "014",
                                            ""), true);

                    //if (SessionParticipante.Categoria.FlAtividades)
                    //    Response.Write("<script>window.open('frmSelAtividades.aspx','_self');</script>");
                    //else
                    //{
                    //    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                    //                        "014",
                    //                        ""), true);
                    //}
                }
            }
        }
         
    }
    protected void btnGravar_Click(object sender, EventArgs e)
    {

    }    

    protected void btnAbandonar_Click(object sender, EventArgs e)
    {

    }

    protected void btnConcluirParcial_Click(object sender, EventArgs e)
    {
        lblMsg.Text = (sender as Button).ID.Replace("btn_","");
    }
    protected void optSelectedIndexChanged(object sender, EventArgs e)
    {
       // if (sender is RadioButton)
            lblMsg.Text = "rb";
    }
    protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "rb";
    }
}