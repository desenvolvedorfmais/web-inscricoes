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

public partial class frmPesquisaVinculada_old : BaseWebUi //System.Web.UI.Page
{

    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    Participante SessionParticipante;

    SqlConnection SessionCnn;

    DataTable oDTPesquisa;

    private int idxPergunta = 0;
    private String[,] arrRespostas;

    String SessionTipoSistema;

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

            //SessionIdioma = (String)Session["SessionIdioma"];
            //if (SessionIdioma == null)
            //    SessionIdioma = "PTBR";
            //Session["SessionIdioma"] = SessionIdioma;

            SessionTipoSistema = (String)Session["SessionTipoSistema"];
            if (SessionTipoSistema == null)
                SessionTipoSistema = "NRM";

            Session["SessionTipoSistema"] = SessionTipoSistema;

            

            

            oDTPesquisa = new DataTable();

            if (((DataTable)Session["oDTPesquisa"] != null) && (((DataTable)Session["oDTPesquisa"]).Rows.Count > 0))
            {
                oDTPesquisa = (DataTable)Session["oDTPesquisa"];

                idxPergunta = int.Parse(Session["idxPergunta"].ToString());

                arrRespostas = (String[,])Session["arrRespostas"];
            }
            else
            {
                localizarPesquisa(SessionEvento.CdEvento);

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
                btnContinuar.Visible = false;
                btnGravar.Visible = true;
            }

            LocalizarPergunta();
        }
        else
        {
            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionTipoSistema = (String)Session["SessionTipoSistema"];

            //SessionIdioma = (String)Session["SessionIdioma"];

            oDTPesquisa = (DataTable)Session["oDTPesquisa"];

            idxPergunta = int.Parse(Session["idxPergunta"].ToString());

            arrRespostas = (String[,])Session["arrRespostas"];
        }

        if (SessionEvento == null)
            Server.Transfer("frmSessaoExpirada.aspx", true);

        

        ToolkitScriptManager1.RegisterPostBackControl(btnContinuar);
        ToolkitScriptManager1.RegisterPostBackControl(btnGravar);
    }


    private void localizarPesquisa(string prmCdEvento)
    {
        string comando =
            "SELECT q.cdQuestionario, " +
            "       q.dsQuestionario, " +
            "       g.cdGrupoPergunta, " +
            "       g.dsGrupoPergunta, " +
            "       cdQuestao,  " +
            "       dsQuestao,   " +
            "       TpQuestao,   " +
            "       p.flAtivo,   " +
            "       p.nrColunas   " +  
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
            "and (p.flAtivo = 1) ";

        if (prmCdEvento == "001304")
        {
            if (SessionParticipante.NoAreaAtuacao == "SIM")//SECRETARIO MUN SAUDE
            {
                comando += " and cdQuestao <> '008' ";
            }
            else
            {
                comando += " and cdQuestao not in ('001','002','010','012') ";
            }
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




        //SqlDataSource1.SelectCommand = cmd.CommandText;
        //DataList1.DataSourceID = "SqlDataSource1";
        //DataList1.DataBind();

        // if (((DataTable)DataList1.DataSource) == null || ((DataTable)DataList1.DataSource).Rows.Count <= 0)
        //     DataList1.Visible = false;



    }

    protected void LocalizarPergunta()
    {
        cdGrupoPergunta.Text = oDTPesquisa.DefaultView[idxPergunta]["cdGrupoPergunta"].ToString();
        dsGrupoPergunta.Text = oDTPesquisa.DefaultView[idxPergunta]["dsGrupoPergunta"].ToString();

        cdQuestaoLabel.Text = oDTPesquisa.DefaultView[idxPergunta]["cdQuestao"].ToString();
        dsQuestaoLabel.Text = oDTPesquisa.DefaultView[idxPergunta]["dsQuestao"].ToString();

        
        DataSet ds = GetDataSet(oDTPesquisa.DefaultView[idxPergunta]["cdQuestionario"].ToString(), oDTPesquisa.DefaultView[idxPergunta]["cdGrupoPergunta"].ToString(), oDTPesquisa.DefaultView[idxPergunta]["cdQuestao"].ToString());
               

        switch (oDTPesquisa.DefaultView[idxPergunta]["TpQuestao"].ToString())
        {
            case "0":
                optResp.Visible = true;
                chkResp.Visible = false;
                txtResp.Visible = false;
                optResp.DataSource = ds;
                optResp.DataTextField = "dsQuestaoItem";
                optResp.DataValueField = "cdQuestaoItem";
                optResp.RepeatColumns = int.Parse(oDTPesquisa.DefaultView[idxPergunta]["nrColunas"].ToString());
                optResp.DataBind();
                break;
            case "1":
                optResp.Visible = false;
                chkResp.Visible = true;
                txtResp.Visible = false;
                chkResp.DataSource = ds;
                chkResp.DataTextField = "dsQuestaoItem";
                chkResp.DataValueField = "cdQuestaoItem";
                chkResp.RepeatColumns = int.Parse(oDTPesquisa.DefaultView[idxPergunta]["nrColunas"].ToString());
                chkResp.DataBind();
                break;
            case "2":
                optResp.Visible = false;
                chkResp.Visible = false;
                txtResp.Visible = true;
                break;
        }
        
    }


    private DataSet GetDataSet(string prm_cdQuestionario, string prm_cdGrupoPergunta, string prm_cdQuestao)
    {
        //SqlConnection cnn = new SqlConnection("Data Source=krksa-pc;Initial Catalog=dbEventos_FM;Persist Security Info=True;User ID=sa;Password=krksa171");
        //SqlConnection cnn = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=dbEventos_FM;Persist Security Info=True;User ID=sa;Password=RaC982973");

        string comandoSQL =
            "select * from tbQuestoesItens " +
            " where cdQuestionario = @cdQuestionario " +
            "   and cdGrupoPergunta = @cdGrupoPergunta " +
            "   and cdQuestao =@cdQuestao ";

        if ((SessionEvento.CdEvento == "001304") && (prm_cdQuestionario == "001304001") && (prm_cdGrupoPergunta == "001") && (prm_cdQuestao == "007"))
        {
            if (arrRespostas[3, 4] == "01")
                comandoSQL += "   and cdQuestaoItem in ('01','02','04','09','10','15','16','17')";
            else if (arrRespostas[3, 4] == "02")
                comandoSQL += "   and cdQuestaoItem in ('05','06','07','08','09','15','16','17')";
            else if (arrRespostas[3, 4] == "03")
                comandoSQL += "   and cdQuestaoItem in ('11','12','13','14','15','16','17')";

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
    
    protected bool prpGravarPesquisa()
    {
        PesquisasResposta oPesquisasResposta = new PesquisasResposta();
        //string tmpCdParticipacao = oPesquisasRespostaAvulsa.SelProCodParticipacao(SessionEvento.CdEvento,SessionCnn);

        //if (tmpCdParticipacao == "")
        //{
        //    lblMsg.Text = oPesquisasRespostaAvulsa.RcMsg;
        //    return false;
        //}

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
        //lblMsg.Text += "Obrigado pela participação na pesquisa!";
       
    }
    protected void btnContinuar_Click(object sender, EventArgs e)
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
        }

        Session["arrRespostas"] = arrRespostas;

        idxPergunta++;

        if (SessionEvento.CdEvento == "000505")
        {
            if ((arrRespostas[0, 4] == "04") && (idxPergunta == 1))
            {
                Session["idxPergunta"] = idxPergunta;
                LocalizarPergunta();
                btnContinuar.Visible = false;
                btnGravar.Visible = true;
                return;
            }
            else if ((arrRespostas[0, 4] == "01") && (idxPergunta == 2))
            {
                Session["idxPergunta"] = idxPergunta;
                LocalizarPergunta();
                btnContinuar.Visible = false;
                btnGravar.Visible = true;
                return;
            }
            else if ((arrRespostas[0, 4] == "02") && (idxPergunta == 4))
            {
                Session["idxPergunta"] = idxPergunta;
                LocalizarPergunta();
                btnContinuar.Visible = false;
                btnGravar.Visible = true;
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
                    Session["idxPergunta"] = idxPergunta;
                    LocalizarPergunta();
                    
                    return;
                }
            }
            else
            {
                if ((idxPergunta == 4) && (arrRespostas[3, 4] == "04"))
                {
                    idxPergunta++;
                    idxPergunta++;
                    Session["idxPergunta"] = idxPergunta;
                    LocalizarPergunta();
                    return;
                }

                if ((idxPergunta == 5) && (arrRespostas[3, 4] != "03"))
                {
                    idxPergunta++;
                    Session["idxPergunta"] = idxPergunta;
                    LocalizarPergunta();
                    return;
                }
            }


        }


        Session["idxPergunta"] = idxPergunta;
        LocalizarPergunta();
        if (idxPergunta >= (oDTPesquisa.Rows.Count - 1))
        {
            idxPergunta = oDTPesquisa.Rows.Count - 1;
            btnContinuar.Visible = false;
            btnGravar.Visible = true;
        }

        //Server.Transfer("frmPesquisaAvulsaProc.aspx", true);
    }
    protected void btnGravar_Click1(object sender, EventArgs e)
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
        }

        Session["arrRespostas"] = arrRespostas;

        if (prpGravarPesquisa())
        {
            Session["oDTPesquisa"] = null;
            Session["arrRespostas"] = null;
            Session["idxPergunta"] = null;

            if (SessionEvento.CdEvento != "001304")
                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}",
                                "034"), true);
            else
            {
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

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
}