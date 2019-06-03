using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
using System.Dynamic;
using System.Web.Helpers;
using System.Web.Services;
using AjaxControlToolkit;
using MSXML2;
using System.Xml;
using Newtonsoft.Json;

public partial class frmPesquisaAvulsa : BaseWebUi //System.Web.UI.Page
{
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();
    public const string QUESTIONARIO_EVENTO = "questionario_";
    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    //Participante SessionParticipante;

    SqlConnection SessionCnn;

    DataTable oDTPesquisa;

    DataTable oDTGrupoPesquisa;

    private int idxPergunta = 0;
    private String[,] arrRespostas;

    String SessionTipoSistema;

    //String SessionGrupoPesquisa = "";

    PesquisasDeOpiniao SessionPesquisasDeOpiniao = new PesquisasDeOpiniao();
    public static HttpResponse CurrentResponse { get; set; }
    public static Page CurrentPage { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentResponse = Response;

        if (!Page.IsPostBack)
        {
            if (SessionCnn == null)
            {
                //Session["SessionCnn"] = new SqlConnection(@"Server=(local);Database=dbEventos_FMLocal;Trusted_Connection=True;ConnectRetryCount=0");
                SessionCnn = (SqlConnection)Session["SessionCnn"];
            }
            //SessionCnn = (SqlConnection) Session["SessionCnn"];
            else
                Session["SessionCnn"] = SessionCnn;

            if (SessionEvento == null)
            {
                Session["SessionEvento"] = oEventoCad.Pesquisar("013101", SessionCnn);
                SessionEvento = (Evento)Session["SessionEvento"];
            }
                
            else
                Session["SessionEvento"] = SessionEvento;


            if (SessionEvento == null)
            {
                //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                //                "003",
                //                oEventoCad.RcMsg), true);

                Server.Transfer("frmSessaoExpirada.aspx", true);
            }

            //SessionParticipante = (Participante)Session["SessionParticipante"];

            //if (SessionParticipante != null)
            //{
            //    lblIdentificador.Text = SessionParticipante.CdParticipante;
            //    lblNoParticipante.Text = SessionParticipante.NoParticipante;
            //}

            //SessionIdioma = (String)Session["SessionIdioma"];
            //if (SessionIdioma == null)
            //    SessionIdioma = "PTBR";
            //Session["SessionIdioma"] = SessionIdioma;

            SessionTipoSistema = (String) Session["SessionTipoSistema"];
            if (SessionTipoSistema == null)
                SessionTipoSistema = "NRM";

            Session["SessionTipoSistema"] = SessionTipoSistema;

            //PesquisasGrupoPerguntas oPesquisasGrupoPerguntas = new PesquisasGrupoPerguntas();
            //oDTGrupoPesquisa = oPesquisasGrupoPerguntas.ListarGruposPerguntas("001304001", SessionCnn);
            //Session["oDTGrupoPesquisa"] = oDTGrupoPesquisa;


            //SqlDataSource1.ConnectionString = SessionCnn.ConnectionString;
            //DataList1.DataSourceID = "SqlDataSource1";

            btnAbandonarPesquisa.PostBackUrl = "http://www.fazendomais.com/inscricoes/inscr.aspx?codEvento=" +
                                               cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) + "&cdLng=" +
                                               "PTBR" + "&cat=" + "" + "&atv=" + "" + "&keyAut=" + "" + "&tpSist=" +
                                               SessionTipoSistema;

            DataTable dtPesquisas =
                SessionPesquisasDeOpiniao.ListarPesquisasDeOpiniao(SessionEvento.CdEvento, SessionCnn);

            lblTitulo.Focus();


            if ((dtPesquisas != null) && (dtPesquisas.Rows.Count > 0))
            {
                SessionPesquisasDeOpiniao = SessionPesquisasDeOpiniao.LocalizarPesquisaDeOpiniao(SessionEvento.CdEvento,
                    dtPesquisas.DefaultView[0]["cdQuestionario"].ToString(), SessionCnn);
                Session["SessionPesquisasDeOpiniao"] = SessionPesquisasDeOpiniao;
                // --- comentado só para pesquisa mobile conasems
                if (SessionPesquisasDeOpiniao.TpFolhaResposta == "COMPLETA")
                {
                    Session["oDTPesquisa"] = null;
                    if (dtPesquisas.Rows.Count == 1)
                        DataList0.DataSource = localizarGrupoPesquisa(SessionEvento.CdEvento,
                            dtPesquisas.DefaultView[0]["cdQuestionario"].ToString());
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

                    if (((DataTable) Session["oDTPesquisa"] != null) &&
                        (((DataTable) Session["oDTPesquisa"]).Rows.Count > 0))
                    {
                        oDTPesquisa = (DataTable) Session["oDTPesquisa"];

                        idxPergunta = int.Parse(Session["idxPergunta"].ToString());

                        arrRespostas = (String[,]) Session["arrRespostas"];
                    }
                    else
                    {
                        //DataTable dtTemp = localizarGrupoPesquisa(SessionEvento.CdEvento, dtPesquisas.DefaultView[0]["cdQuestionario"].ToString());

                        localizarPesquisa(SessionEvento.CdEvento,
                            dtPesquisas.DefaultView[0]["cdQuestionario"].ToString(), "", null);

                        Session["idxPergunta"] = idxPergunta;

                        arrRespostas = new String[oDTPesquisa.Rows.Count, 6];

                        for (int i = 0; i < oDTPesquisa.Rows.Count; i++)
                        {
                            arrRespostas[i, 0] = ""; //cdQuestinario
                            arrRespostas[i, 1] = ""; //cdGrupoPerguntas
                            arrRespostas[i, 2] = ""; //cdQuestao
                            arrRespostas[i, 3] = ""; //tpQuestao(somente uma/mais que uma/texto)
                            arrRespostas[i, 4] = ""; //cdResposta(opt/chk)
                            arrRespostas[i, 5] = ""; //dsResposta
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

            var cookie =
                HttpContext.Current.Request.Cookies.AllKeys.FirstOrDefault(x => x.Contains(QUESTIONARIO_EVENTO));
            if (cookie != null)
            {
                HttpCookie currentUserCookie = HttpContext.Current.Request.Cookies[cookie];
                HttpContext.Current.Response.Cookies.Remove(cookie);
                currentUserCookie.Expires = DateTime.Now.AddDays(-10);
                currentUserCookie.Value = null;
                HttpContext.Current.Response.SetCookie(currentUserCookie);
            }
        }
        else
        {
            SessionEvento = (Evento) Session["SessionEvento"];

            SessionCnn = (SqlConnection) Session["SessionCnn"];

            //SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionTipoSistema = (String) Session["SessionTipoSistema"];

            //SessionIdioma = (String)Session["SessionIdioma"];

            oDTPesquisa = (DataTable) Session["oDTPesquisa"];

            idxPergunta = (Session["idxPergunta"] == null ? 0 : int.Parse(Session["idxPergunta"].ToString()));

            arrRespostas = (String[,]) Session["arrRespostas"];

            //oDTGrupoPesquisa = (DataTable)Session["oDTGrupoPesquisa"];

            SessionPesquisasDeOpiniao = (PesquisasDeOpiniao) Session["SessionPesquisasDeOpiniao"];
        }

        if (SessionEvento == null)
            Server.Transfer("frmSessaoExpirada.aspx", true);


        ToolkitScriptManager1.RegisterPostBackControl(btnContinuarPesquisa);
        ToolkitScriptManager1.RegisterPostBackControl(btnConcluirPesquisa);
        ToolkitScriptManager1.RegisterPostBackControl(btnAbandonarPesquisa);
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
            //"and (q.flVisivelLocal = 1)  " +
            //"and (g.flVisivelLocal = 1)  " +
            "AND getdate() > q.dtLiberacao "; // +

        //"and (q.cdAtividade = ''  " +
        //" OR q.cdAtividade IN (SELECT cdAtividade  " +
        //"                  FROM tbMatriculas AS m  " +
        //"                 WHERE (cdParticipante = '" + SessionParticipante.CdParticipante + "')  " +
        //"                   AND (cdEvento = '" + prmCdEvento + "' ))) ";

        comando +=
            " and q.cdQuestionario = '" + prmCdPesquisa + "' ";

        //if ((SessionParticipante != null) && (SessionParticipante.CdParticipante != ""))
        //{
        //    comando += "  AND (q.cdQuestionario + g.cdGrupoPergunta NOT IN " +
        //             "(SELECT cdQuestionario + cdGrupoPergunta " +
        //                "FROM tbQuestoesRespostas AS tqr " +
        //               "WHERE (cdParticipante = '" + SessionParticipante.CdParticipante + "') " +
        //                " AND (cdEvento = '" + prmCdEvento + "'))) ";
        //}

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
            "AND getdate() > q.dtLiberacao "; // +

        //"and (q.cdAtividade = ''  " +
        //    " OR q.cdAtividade IN (SELECT cdAtividade  " +
        //    "                  FROM tbMatriculas AS m  " +
        //    "                 WHERE (cdParticipante = '" + SessionParticipante.CdParticipante + "')  " +
        //    "                   AND (cdEvento = '" + prmCdEvento + "' ))) ";


        //if ((SessionParticipante != null) && (SessionParticipante.CdParticipante != ""))
        //{
        //    comando += "  AND (q.cdQuestionario + g.cdGrupoPergunta NOT IN " +
        //             "(SELECT cdQuestionario + cdGrupoPergunta " +
        //                "FROM tbQuestoesRespostas AS tqr " +
        //               "WHERE (cdParticipante = '" + SessionParticipante.CdParticipante + "') " +
        //                " AND (cdEvento = '" + prmCdEvento + "'))) ";
        //}

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
            "       TpQuestao,   " + //--- comentado só para pesquisa mobile conasems
            //"       case when ((q.cdQuestionario = '001304001') and (cdQuestao = '011')) then 1 else 0 end TpQuestao,   " +
            "       p.flAtivo,   " +
            "       p.nrColunas   " + //--- comentado só para pesquisa mobile conasems
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
            "AND getdate() > q.dtLiberacao "; // +

        //"and (q.cdAtividade = ''  " +
        //" OR q.cdAtividade IN (SELECT cdAtividade  " +
        //"                  FROM tbMatriculas AS m  " +
        //"                 WHERE (cdParticipante = '" + SessionParticipante.CdParticipante + "')  " +
        //"                   AND (cdEvento = '" + prmCdEvento + "' ))) ";

        comando +=
            " and p.cdQuestionario = '" + prmCdPesquisa + "' " +
            " and p.cdGrupoPergunta like '%" + prmCdGrupo + "%' ";

        //if ((SessionParticipante != null) && (SessionParticipante.CdParticipante != ""))
        //{
        //    comando += "  AND (q.cdQuestionario + g.cdGrupoPergunta NOT IN " +
        //             "(SELECT cdQuestionario + cdGrupoPergunta " +
        //                "FROM tbQuestoesRespostas AS tqr " +
        //               "WHERE (cdParticipante = '" + SessionParticipante.CdParticipante + "') " +
        //                " AND (cdEvento = '" + prmCdEvento + "'))) ";
        //}

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
            DataList DataList1 = (DataList) prmItem.FindControl("DataList1");
            DataList1.DataSource = DTPesq; // sqldatasouce;//.DataSourceID = "SqlDataSource1";
            DataList1.DataBind();
        }

        // if (((DataTable)DataList1.DataSource) == null || ((DataTable)DataList1.DataSource).Rows.Count <= 0)
        //     DataList1.Visible = false;
    }

    protected void LocalizarPergunta()
    {
        cdGrupoPergunta.Text = oDTPesquisa.DefaultView[idxPergunta]["cdGrupoPergunta"].ToString();
        line1.Attributes["data-cdGrupoPergunta"] = cdGrupoPergunta.Text;

        dsGrupoPergunta.Text = oDTPesquisa.DefaultView[idxPergunta]["dsGrupoPergunta"].ToString();

        cdQuestaoLabel.Text = oDTPesquisa.DefaultView[idxPergunta]["cdQuestao"].ToString();
        line2.Attributes["data-cdQuestaoLabel"] = cdQuestaoLabel.Text;

        dsQuestaoLabel.Text = oDTPesquisa.DefaultView[idxPergunta]["dsQuestao"].ToString();


        DataSet ds = GetDataSet(
            oDTPesquisa.DefaultView[idxPergunta]["cdQuestionario"].ToString(),
            oDTPesquisa.DefaultView[idxPergunta]["cdGrupoPergunta"].ToString(),
            oDTPesquisa.DefaultView[idxPergunta]["cdQuestao"].ToString(),
            oDTPesquisa.DefaultView[idxPergunta]["TpQuestao"].ToString(),
            "");
        if ((SessionPesquisasDeOpiniao.TpFolhaResposta != "COMPLETA") && (SessionEvento.CdEvento == "001304") &&
            (oDTPesquisa.DefaultView[idxPergunta]["cdQuestionario"].ToString() == "001304001") &&
            (oDTPesquisa.DefaultView[idxPergunta]["cdGrupoPergunta"].ToString() == "001") &&
            (oDTPesquisa.DefaultView[idxPergunta]["cdQuestao"].ToString() == "007"))
        {
            ds = GetDataSet(
                oDTPesquisa.DefaultView[idxPergunta]["cdQuestionario"].ToString(),
                oDTPesquisa.DefaultView[idxPergunta]["cdGrupoPergunta"].ToString(),
                oDTPesquisa.DefaultView[idxPergunta]["cdQuestao"].ToString(),
                oDTPesquisa.DefaultView[idxPergunta]["TpQuestao"].ToString(),
                arrRespostas[idxPergunta - 1, 4]);
        }

        switch (oDTPesquisa.DefaultView[idxPergunta]["TpQuestao"].ToString())
        {
            case "0":
                //if ((SessionEvento.CdEvento == "001304") && (oDTPesquisa.DefaultView[idxPergunta]["cdQuestionario"].ToString() == "001304001"))
                //{
                //    if (SessionParticipante.NoAreaAtuacao == "NÃO")
                //    {
                //        if (idxPergunta == 0)
                //        {
                //            arrRespostas[idxPergunta, 0] = oDTPesquisa.DefaultView[idxPergunta]["cdQuestionario"].ToString();
                //            arrRespostas[idxPergunta, 1] = oDTPesquisa.DefaultView[idxPergunta]["cdGrupoPergunta"].ToString();
                //            arrRespostas[idxPergunta, 2] = oDTPesquisa.DefaultView[idxPergunta]["cdQuestao"].ToString();
                //            arrRespostas[idxPergunta, 3] = oDTPesquisa.DefaultView[idxPergunta]["TpQuestao"].ToString();
                //            arrRespostas[idxPergunta, 4] = "00";

                //            idxPergunta++;
                //            arrRespostas[idxPergunta, 0] = oDTPesquisa.DefaultView[idxPergunta]["cdQuestionario"].ToString();
                //            arrRespostas[idxPergunta, 1] = oDTPesquisa.DefaultView[idxPergunta]["cdGrupoPergunta"].ToString();
                //            arrRespostas[idxPergunta, 2] = oDTPesquisa.DefaultView[idxPergunta]["cdQuestao"].ToString();
                //            arrRespostas[idxPergunta, 3] = oDTPesquisa.DefaultView[idxPergunta]["TpQuestao"].ToString();
                //            arrRespostas[idxPergunta, 4] = "00";

                //            idxPergunta++;
                //            Session["idxPergunta"] = idxPergunta;
                //            LocalizarPergunta();

                //            return;
                //        }
                //    }
                //}

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


                if ((SessionEvento.CdEvento == "005502") &&
                    (oDTPesquisa.DefaultView[idxPergunta]["cdQuestionario"].ToString() == "005502001") &&
                    (oDTPesquisa.DefaultView[idxPergunta]["cdQuestao"].ToString() == "004"))
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
                //{
                //    if (SessionParticipante.NoAreaAtuacao == "NÃO")
                //    {
                //        if (idxPergunta == 0)
                //        {
                //            arrRespostas[idxPergunta, 0] = oDTPesquisa.DefaultView[idxPergunta]["cdQuestionario"].ToString();
                //            arrRespostas[idxPergunta, 1] = oDTPesquisa.DefaultView[idxPergunta]["cdGrupoPergunta"].ToString();
                //            arrRespostas[idxPergunta, 2] = oDTPesquisa.DefaultView[idxPergunta]["cdQuestao"].ToString();
                //            arrRespostas[idxPergunta, 3] = oDTPesquisa.DefaultView[idxPergunta]["TpQuestao"].ToString();
                //            arrRespostas[idxPergunta, 4] = "00";

                //            idxPergunta++;
                //            arrRespostas[idxPergunta, 0] = oDTPesquisa.DefaultView[idxPergunta]["cdQuestionario"].ToString();
                //            arrRespostas[idxPergunta, 1] = oDTPesquisa.DefaultView[idxPergunta]["cdGrupoPergunta"].ToString();
                //            arrRespostas[idxPergunta, 2] = oDTPesquisa.DefaultView[idxPergunta]["cdQuestao"].ToString();
                //            arrRespostas[idxPergunta, 3] = oDTPesquisa.DefaultView[idxPergunta]["TpQuestao"].ToString();
                //            arrRespostas[idxPergunta, 4] = "00";

                //            idxPergunta++;
                //            Session["idxPergunta"] = idxPergunta;
                //            LocalizarPergunta();

                //            return;
                //        }
                //    }
                //}

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


        //cdGrupoPergunta.Attributes.Add("data-cdQuestao", cdQuestaoLabel.Text);
        //cdGrupoPergunta.Attributes.Add("data-dsQuestao", dsQuestaoLabel.Text);
    }

    protected void DataList0_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HiddenField cdPesquisa = (HiddenField) e.Item.FindControl("HiddenField5");
            Label lbl_cdGrupoPesquisa = (Label) e.Item.FindControl("lbl_cdGrupoPergunta");


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
            HiddenField anstype = (HiddenField) e.Item.FindControl("HiddenField1");
            HiddenField cdPesquisa = (HiddenField) e.Item.FindControl("HiddenField2");
            HiddenField cdGrupoPesquisa = (HiddenField) e.Item.FindControl("HiddenField3");
            cdGrupoPergunta.Attributes["data-cdGrupoPesquisa"] = cdGrupoPesquisa.Value;

            HiddenField nrColunas = (HiddenField) e.Item.FindControl("HiddenField4");

            Label questionid = (Label) e.Item.FindControl("cdQuestaoLabel");
            questionid.ID = "L_" + questionid.Text;
            RadioButtonList rbl = (RadioButtonList) e.Item.FindControl("RadioButtonList1");
            CheckBoxList cbl = (CheckBoxList) e.Item.FindControl("CheckBoxList1");
            DropDownList dbl = (DropDownList) e.Item.FindControl("DropDownList1");
            TextBox txt = (TextBox) e.Item.FindControl("TextBox1");
            DataSet ds = GetDataSet(cdPesquisa.Value.ToString(), cdGrupoPesquisa.Value.ToString(), questionid.Text,
                anstype.Value, "");

            Table tb = (Table) e.Item.FindControl("tb1");

            RequiredFieldValidator rf = (RequiredFieldValidator) e.Item.FindControl("RequiredFieldValidator3");
            //rf.ValidationGroup = "VG_" + cdPesquisa.Value;
            RequiredFieldValidator rf1 = (RequiredFieldValidator) e.Item.FindControl("RequiredFieldValidator4");
            //rf1.ValidationGroup = "VG_" + cdPesquisa.Value;

            tmpCdQuestionario = cdPesquisa.Value;

            switch (anstype.Value)
            {
                case "0":

                    //rf.Visible = true;
                    //rf.ControlToValidate = rbl.id;
                    //rf.Enabled = true;

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

                    if (SessionEvento.CdEvento == "006301")
                    {
                        txt.Height = 20;
                        if (questionid.Text == "009")
                        {
                            txt.Width = 160;
                            txt.Attributes["onkeydown"] = "Mascarar(this, event, '999.999.999-99')";
                            txt.Attributes["onkeypress"] = "Mascarar(this, event, '999.999.999-99')";
                            txt.Attributes["onkeyup"] = "Mascarar(this, event, '999.999.999-99')";

                            txt.MaxLength = 14;
                        }

                        if (questionid.Text == "010")
                        {
                            txt.Width = 160;
                            txt.Attributes["onkeydown"] = "FoneMascarar(this, event)";
                            txt.Attributes["onkeypress"] = "FoneMascarar(this, event)";
                            txt.Attributes["onkeyup"] = "FoneMascarar(this, event)";
                            txt.MaxLength = 15;
                        }
                    }

                    break;
                case "3":

                    rf1.Visible = true;
                    //rf1.ControlToValidate = dbl.ID;

                    dbl.ToolTip = "Q_" + cdPesquisa.Value + "_" + questionid.Text;

                    rbl.Visible = false;
                    cbl.Visible = false;
                    dbl.Visible = true;
                    txt.Visible = false;
                    dbl.DataSource = ds;
                    dbl.DataTextField = "dsQuestaoItem";
                    dbl.DataValueField = "cdQuestaoItem";

                    rf1.InitialValue = "";

                    //if (SessionEvento.CdEvento == "001304")
                    //{
                    //    DataTable dtTemp = new DataTable();
                    //    dtTemp.Columns.Add("cdQuestaoItem");
                    //    dtTemp.Columns.Add("dsQuestaoItem");

                    //    dtTemp.Rows.Add("00", "NÃO SE APLICA");

                    //    if ((cdPesquisa.Value == "001304001") && (cdGrupoPesquisa.Value == "001"))
                    //    {
                    //        if (SessionParticipante.NoAreaAtuacao == "SIM")
                    //        {

                    //            if (questionid.Text == "006")
                    //            {
                    //                dbl.SelectedValue = "01";
                    //                dbl.Enabled = false;
                    //                tb.Visible = false;
                    //            }
                    //            if (questionid.Text == "007")
                    //            {
                    //                dbl.SelectedValue = "03";
                    //                dbl.Enabled = false;
                    //                tb.Visible = false;
                    //            }

                    //            if (questionid.Text == "008")
                    //            {
                    //                dbl.DataSource = dtTemp;
                    //                dbl.Enabled = false;
                    //                tb.Visible = false;
                    //            }

                    //        }
                    //        else
                    //        {

                    //            if (questionid.Text == "006")
                    //            {

                    //                dbl.AutoPostBack = true;
                    //                dbl.SelectedIndexChanged += new EventHandler(DropDownList1_SelectedIndexChanged);
                    //            }

                    //            if (questionid.Text == "001")
                    //            {
                    //                dbl.DataSource = dtTemp;
                    //                dbl.Enabled = false;
                    //                tb.Visible = false;
                    //            }
                    //            if (questionid.Text == "002")
                    //            {
                    //                dbl.DataSource = dtTemp;
                    //                dbl.Enabled = false;
                    //                tb.Visible = false;
                    //            }
                    //            if (questionid.Text == "010")
                    //            {
                    //                dbl.DataSource = dtTemp;
                    //                dbl.Enabled = false;
                    //                tb.Visible = false;
                    //            }
                    //            if (questionid.Text == "012")
                    //            {
                    //                dbl.DataSource = dtTemp;
                    //                dbl.Enabled = false;
                    //                tb.Visible = false;
                    //            }
                    //        }
                    //    }
                    //}
                    dbl.DataBind();
                    break;
            }
        }
    }

    private DataSet GetDataSet(string prm_cdQuestionario, string prm_cdGrupoPergunta, string prm_cdQuestao,
        string prm_tpQuestao, string prm_grupo)
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
        //{

        //    if (prm_grupo == "01")
        //        comandoSQL += "   and cdQuestaoItem in ('01','02','04','09','10','15','16','17')";
        //    else if (prm_grupo == "02")
        //        comandoSQL += "   and cdQuestaoItem in ('05','06','07','08','09','15','16','17')";
        //    else if (prm_grupo == "03")
        //        comandoSQL += "   and cdQuestaoItem in ('11','12','13','14','15','16','17')";


        //}

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

        if (SessionEvento.CdEvento == "001304")
        {
            DropDownList actionsDDL = sender as DropDownList;

            if (actionsDDL.ToolTip == "Q_001304001_006")
            {
                DataList DataList1 = (DataList) DataList0.Items[0].FindControl("DataList1");

                //008
                DropDownList ddlq = (DropDownList) DataList1.Items[7].FindControl("DropDownList1");
                RequiredFieldValidator rfq =
                    (RequiredFieldValidator) DataList1.Items[7].FindControl("RequiredFieldValidator4");
                Table tb1 = (Table) DataList1.Items[7].FindControl("tb1");
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
                    ddlq.DataSource = GetDataSet("001304001", "001", "008", "3", "");
                    ddlq.DataBind();
                    ddlq.SelectedValue = "";
                    ddlq.Enabled = true;
                    //ddlq.Visible = false;                
                    rfq.Visible = true;
                    tb1.Visible = true;
                }

                //007
                DropDownList ddlq2 = (DropDownList) DataList1.Items[6].FindControl("DropDownList1");
                RequiredFieldValidator rfq2 =
                    (RequiredFieldValidator) DataList1.Items[6].FindControl("RequiredFieldValidator4");
                Table tb2 = (Table) DataList1.Items[6].FindControl("tb1");
                if (actionsDDL.SelectedValue != "04")
                {
                    ddlq2.DataSource = GetDataSet("001304001", "001", "007", "3", actionsDDL.SelectedValue.ToString());
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

                    ddlq2.DataSource = dtTemp; // GetDataSet("001304001", "001", "007", "3", "");
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
        PesquisasRespostaAvulsa oPesquisasRespostaAvulsa = new PesquisasRespostaAvulsa();
        string tmpCdParticipacao = oPesquisasRespostaAvulsa.SelProCodParticipacao(SessionEvento.CdEvento, SessionCnn);

        if (tmpCdParticipacao == "")
        {
            lblMsg.Text = oPesquisasRespostaAvulsa.RcMsg;
            return false;
        }

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
                    if (!oPesquisasRespostaAvulsa.IncluirRespostaItem(SessionEvento.CdEvento, tmpCdParticipacao,
                        arrRespostas[i, 0], arrRespostas[i, 1], arrRespostas[i, 2], arrRespostas[i, 4],
                        arrRespostas[i, 5], SessionCnn))
                        erros++;
                }
                else
                {
                    for (int j = 0; j < tmpResp.Length; j++)
                    {
                        if (!oPesquisasRespostaAvulsa.IncluirRespostaItem(SessionEvento.CdEvento, tmpCdParticipacao,
                            arrRespostas[i, 0], arrRespostas[i, 1], arrRespostas[i, 2], tmpResp[j], arrRespostas[i, 5],
                            SessionCnn))
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
                DataList DataList1 = (DataList) item0.FindControl("DataList1");
                foreach (DataListItem item in DataList1.Items)
                {
                    if (item.ItemType == ListItemType.Item | item.ItemType == ListItemType.AlternatingItem)
                    {
                        string cdQuestionario = ((HiddenField) item.FindControl("HiddenField2")).Value.ToString();
                        string cdGrupoPergunta = ((HiddenField) item.FindControl("HiddenField3")).Value.ToString();
                        string cdQuestao = ((Label) item.FindControl("cdQuestaoLabel")).Text;
                        string cdResposta = "";
                        string dsResposta = "";

                        HiddenField tpQuestao = (HiddenField) item.FindControl("HiddenField1");
                        switch (tpQuestao.Value)
                        {
                            case "0": //"S"
                                RadioButtonList rbl = (RadioButtonList) item.FindControl("RadioButtonList1");
                                cdResposta = rbl.SelectedValue.ToString();
                                //lblMsg.Text += tpQuestao.Value + "/" + cdQuestionario + "/" + cdGrupoPergunta + "/" + cdQuestao + "/" + cdResposta + "/" + dsResposta + "<br/>";
                                //SaveAnswer(cdQuestionario, cdGrupoPergunta, cdQuestao, cdResposta, dsResposta);
                                //oPesquisasResposta.IncluirRespostaItem(SessionEvento.CdEvento, SessionParticipante.CdParticipante,
                                //    cdQuestionario, cdGrupoPergunta, cdQuestao, cdResposta, dsResposta, SessionCnn);
                                //tmpCargo = Geral.verificarCargoJaPreenchido(SessionEvento.CdEvento, SessionParticipante.CdParticipante, rbl.SelectedItem.ToString(), SessionParticipante.DsAuxiliar8, SessionParticipante.DsAuxiliar9, SessionCnn);
                                //if (tmpCargo != "")
                                //{
                                //    lblMsg.Text = tmpCargo;
                                //    return false;
                                //}
                                break;
                            case "1": //"M"
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
                            case "2": //"T"
                                //TextBox txt = (TextBox)item.FindControl("TextBox1");
                                //dsResposta = txt.Text;
                                //cdResposta = "01";
                                //SaveAnswer(cdQuestionario, cdGrupoPergunta, cdQuestao, "01", dsResposta);
                                //oPesquisasResposta.IncluirRespostaItem(SessionEvento.CdEvento, SessionParticipante.CdParticipante,
                                //    cdQuestionario, cdGrupoPergunta, cdQuestao, cdResposta, dsResposta, SessionCnn);
                                break;
                            case "3": //"Lista"
                                DropDownList ddl = (DropDownList) item.FindControl("DropDownList1");
                                cdResposta = ddl.SelectedValue.ToString();
                                //lblMsg.Text += tpQuestao.Value + "/" + cdQuestionario + "/" + cdGrupoPergunta + "/" + cdQuestao + "/" + cdResposta + "/" + dsResposta + "<br/>";
                                //SaveAnswer(cdQuestionario, cdGrupoPergunta, cdQuestao, cdResposta, dsResposta);
                                //oPesquisasResposta.IncluirRespostaItem(SessionEvento.CdEvento, SessionParticipante.CdParticipante,
                                //    cdQuestionario, cdGrupoPergunta, cdQuestao, cdResposta, dsResposta, SessionCnn);
                                //tmpCargo = Geral.verificarCargoJaPreenchido(SessionEvento.CdEvento, SessionParticipante.CdParticipante, ddl.SelectedItem.ToString(), SessionParticipante.DsAuxiliar8, SessionParticipante.DsAuxiliar9, SessionCnn);
                                //if (tmpCargo != "")
                                //{
                                //    lblMsg.Text = tmpCargo;
                                //    return false;
                                //}
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
        PesquisasRespostaAvulsa oPesquisasRespostaAvulsa = new PesquisasRespostaAvulsa();
        string tmpCdParticipacao = oPesquisasRespostaAvulsa.SelProCodParticipacao(SessionEvento.CdEvento, SessionCnn);

        if (tmpCdParticipacao == "")
        {
            lblMsg.Text = oPesquisasRespostaAvulsa.RcMsg;
            return;
        }

        foreach (DataListItem item0 in DataList0.Items)
        {
            if (item0.ItemType == ListItemType.Item | item0.ItemType == ListItemType.AlternatingItem)
            {
                DataList DataList1 = (DataList) item0.FindControl("DataList1");
                foreach (DataListItem item in DataList1.Items)
                {
                    if (item.ItemType == ListItemType.Item | item.ItemType == ListItemType.AlternatingItem)
                    {
                        string cdQuestionario = ((HiddenField) item.FindControl("HiddenField2")).Value.ToString();
                        string cdGrupoPergunta = ((HiddenField) item.FindControl("HiddenField3")).Value.ToString();
                        string cdQuestao = ((Label) item.FindControl("cdQuestaoLabel")).Text;
                        string cdResposta = "";
                        string dsResposta = "";

                        HiddenField tpQuestao = (HiddenField) item.FindControl("HiddenField1");
                        switch (tpQuestao.Value)
                        {
                            case "0": //"S"
                                RadioButtonList rbl = (RadioButtonList) item.FindControl("RadioButtonList1");
                                cdResposta = rbl.SelectedValue.ToString();
                                lblMsg.Text += tpQuestao.Value + "/" + cdQuestionario + "/" + cdGrupoPergunta + "/" +
                                               cdQuestao + "/" + cdResposta + "/" + dsResposta + "<br/>";
                                //SaveAnswer(cdQuestionario, cdGrupoPergunta, cdQuestao, cdResposta, dsResposta);
                                oPesquisasRespostaAvulsa.IncluirRespostaItem(SessionEvento.CdEvento, tmpCdParticipacao,
                                    cdQuestionario, cdGrupoPergunta, cdQuestao, cdResposta, dsResposta, SessionCnn);
                                break;
                            case "1": //"M"
                                CheckBoxList cbl = (CheckBoxList) item.FindControl("CheckBoxList1");
                                //Label2.Text = cbl.Items.Count.ToString();
                                for (int i = 0; i <= cbl.Items.Count - 1; i++)
                                {
                                    if (cbl.Items[i].Selected)
                                    {
                                        cdResposta = cbl.Items[i].Value;
                                        lblMsg.Text += tpQuestao.Value + "/" + cdQuestionario + "/" + cdGrupoPergunta +
                                                       "/" + cdQuestao + "/" + cdResposta + "/" + dsResposta + "<br/>";
                                        //SaveAnswer(cdQuestionario, cdGrupoPergunta, cdQuestao, cdResposta, dsResposta);
                                        oPesquisasRespostaAvulsa.IncluirRespostaItem(SessionEvento.CdEvento,
                                            tmpCdParticipacao,
                                            cdQuestionario, cdGrupoPergunta, cdQuestao, cdResposta, dsResposta,
                                            SessionCnn);
                                    }
                                }

                                break;
                            case "2": //"T"
                                TextBox txt = (TextBox) item.FindControl("TextBox1");
                                dsResposta = txt.Text;
                                cdResposta = "01";
                                //SaveAnswer(cdQuestionario, cdGrupoPergunta, cdQuestao, "01", dsResposta);
                                oPesquisasRespostaAvulsa.IncluirRespostaItem(SessionEvento.CdEvento, tmpCdParticipacao,
                                    cdQuestionario, cdGrupoPergunta, cdQuestao, cdResposta, dsResposta, SessionCnn);
                                break;
                            case "3": //"Lista"
                                DropDownList ddl = (DropDownList) item.FindControl("DropDownList1");
                                cdResposta = ddl.SelectedValue.ToString();
                                lblMsg.Text += tpQuestao.Value + "/" + cdQuestionario + "/" + cdGrupoPergunta + "/" +
                                               cdQuestao + "/" + cdResposta + "/" + dsResposta + "<br/>";
                                //SaveAnswer(cdQuestionario, cdGrupoPergunta, cdQuestao, cdResposta, dsResposta);
                                oPesquisasRespostaAvulsa.IncluirRespostaItem(SessionEvento.CdEvento, tmpCdParticipacao,
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
            if (SessionEvento.CdEvento != "001304")
                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}",
                    "034"), true);
            else
            {
                /* PesquisasDeOpiniao oPesquisasDeOpiniao = new PesquisasDeOpiniao();
                 DataTable dtPesquisas = oPesquisasDeOpiniao.ListarPesquisasDoParticipante(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);
 
                 if ((dtPesquisas != null) && (dtPesquisas.Rows.Count > 0))
                 {
                     Session["oDTPesquisa"] = null;
                     Response.Write("<script>window.open('frmPesquisaVinculada.aspx','_self');</script>");
                 }
                 else
                 {*/
                //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}",
                //            "034"), true);

                //if (Geral.datahoraServidor(SessionCnn) > DateTime.Parse("23/05/2014 23:59:00"))
                //{
                //    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                //                        "035",
                //                        ""), true);
                //}

                //if (SessionParticipante.Categoria.FlAtividades)
                //    Response.Write("<script>window.open('frmSelAtividades.aspx','_self');</script>");
                //else
                //{
                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                    "014",
                    ""), true);
                //}
                //}
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
                case "0": //"S"
                    for (int i = 0; i <= optResp.Items.Count - 1; i++)
                    {
                        if (optResp.Items[i].Selected)
                        {
                            arrRespostas[idxPergunta, 4] = optResp.Items[i].Value.ToString();
                            break;
                        }
                    }

                    break;
                case "1": //"M"

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
                case "2": //"T"

                    arrRespostas[idxPergunta, 4] = "01";
                    arrRespostas[idxPergunta, 5] = txtResp.Text;

                    break;
                case "3": //"ListBox"

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

            #region pesquisa Conasems

            /*

            if ((SessionEvento.CdEvento == "001304") && (oDTPesquisa.DefaultView[idxPergunta]["cdQuestionario"].ToString() == "001304001"))
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

                        btnContinuar.Visible = false;
                        btnConcluir.Visible = true;


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
            */

            #endregion

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
            case "0": //"S"
                for (int i = 0; i <= optResp.Items.Count - 1; i++)
                {
                    if (optResp.Items[i].Selected)
                    {
                        arrRespostas[idxPergunta, 4] = optResp.Items[i].Value.ToString();
                        break;
                    }
                }

                break;
            case "1": //"M"

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
            case "2": //"T"

                arrRespostas[idxPergunta, 4] = "01";
                arrRespostas[idxPergunta, 5] = txtResp.Text;

                break;
            case "3": //"listBox"

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

            if (SessionEvento.CdEvento != "001304")
                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}",
                    "034"), true);
            else
            {
                /*PesquisasDeOpiniao oPesquisasDeOpiniao = new PesquisasDeOpiniao();
                DataTable dtPesquisas = oPesquisasDeOpiniao.ListarPesquisasDoParticipante(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);

                if ((dtPesquisas != null) && (dtPesquisas.Rows.Count > 0))
                {
                    Session["oDTPesquisa"] = null;
                    Response.Write("<script>window.open('frmPesquisaVinculada.aspx','_self');</script>");
                }
                else
                {*/
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
                //}
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
        lblMsg.Text = (sender as Button).ID.Replace("btn_", "");
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

    [WebMethod(EnableSession = true)]
    public static string SalvarSessionQuestion(string cdGrupo = null, string cdQuestao = null, string reposta = null)
    {
        var response = HttpContext.Current.Response;
        dynamic respostaMsg = new ExpandoObject();
        // Verificando se já existe o cookie na máquina do usuário
        HttpCookie cookie = HttpContext.Current.Request.Cookies[QUESTIONARIO_EVENTO + cdQuestao];
        if (cookie == null)
        {
            
            // Criando a Instância do cookie
            cookie = new HttpCookie(QUESTIONARIO_EVENTO + cdQuestao);
            //Adicionando a propriedade "Nome" no cookie
            cookie.Values.Add("cdGrupo", cdGrupo);
            cookie.Values.Add("cdQuestao", cdQuestao);
            cookie.Values.Add("reposta", reposta);
            cookie.Values.Add("acertos", null);

            ////colocando o cookie para expirar em 365 dias
            //cookie.Expires = DateTime.Now.AddDays(365);
            // Definindo a segurança do nosso cookie
            //cookie.HttpOnly = true;

            var totalAcertos = 0;
            var totalQuestao = 4;
            var totalErros = 0;
            
           
            foreach (var allCookiesAllKey in HttpContext.Current.Request.Cookies.AllKeys)
            {
                {
                    var MyCookieValues = HttpContext.Current.Request.Cookies[allCookiesAllKey];
                    if (MyCookieValues["acertos"] != null)
                    {
                        var valueAcerto = MyCookieValues["acertos"];


                        if (!string.IsNullOrEmpty(valueAcerto))
                            totalAcertos = Convert.ToInt32(valueAcerto);
                    }
                }

               
                
            }

            totalErros = totalQuestao - totalAcertos;


            switch (Convert.ToInt32(cdQuestao))
            {
                case 1:
                    respostaMsg.msg =
                        @"De acordo com o Instituto Brasileiro de Sustentabilidade, ações sustentáveis na organização contribuem com a redução de custos evitando desperdícios, melhorando o aproveitamento de matéria-prima e podendo atrair a atenção também de um novo perfil de cliente.";

                    if (Convert.ToInt32(reposta).Equals(2))
                    {
                        respostaMsg.correto = true;
                       var acertos =  cookie.Values["acertos"] as string;
                        cookie.Values["acertos"] = Convert.ToInt32(acertos + 1).ToString();
                    }
                    else
                    {
                        respostaMsg.errou = 1;
                    }

                    break;

                case 2:
                    respostaMsg.msg =
                        @"A ONU estabeleceu esses pilares de sustentabilidade internacional durante a Cúpula Mundial sobre o Desenvolvimento Sustentável, realizada em Joanesburgo, África do Sul, em 2010.";

                    if (Convert.ToInt32(reposta).Equals(1))
                    {
                        
                        respostaMsg.correto = true;
                        var acertos = cookie.Values["acertos"] as string;
                        cookie.Values["acertos"] = Convert.ToInt32(acertos + 1).ToString();
                    }
                    else
                    {
                        respostaMsg.errou = 1;
                    }

                    ;
                    break;

                case 3:
                    respostaMsg.msg =
                        @"A certificação de Responsabilidade Social foi criada em 1989 pela Social Accountability International (SAI) e pode ser aplicada a qualquer empresa, de qualquer tamanho, em todo o mundo.";

                    if (Convert.ToInt32(reposta).Equals(2))
                    {
                        respostaMsg.correto = true;
                        
                        var acertos = cookie.Values["acertos"] as string;
                        cookie.Values["acertos"] = Convert.ToInt32(acertos + 1).ToString();
                    }
                    else
                    {
                        respostaMsg.errou = 1;
                    }
                    break;

                case 4:
                    respostaMsg.msg =
                        @"Segundo o site do INMETRO, a norma publicada no dia 1º de novembro de 2010 tem fins consultivos e fornece para as empresas regras e diretrizes para conscientização e controle de seus impactos na sociedade e no meio ambiente.";
                    respostaMsg.concluido = true;
                    if (Convert.ToInt32(reposta).Equals(1))
                    {
                        respostaMsg.correto = true;
                        var acertos = cookie.Values["acertos"] as string;
                        cookie.Values["acertos"] = Convert.ToInt32(acertos + 1).ToString();
                    }
                    else
                    {
                        respostaMsg.errou = 1;
                    }

                    ;
                    break;
            }
        }

        response.Cookies.Add(cookie);
        string json = JsonConvert.SerializeObject(respostaMsg, Newtonsoft.Json.Formatting.Indented);
        return json;
    }
}