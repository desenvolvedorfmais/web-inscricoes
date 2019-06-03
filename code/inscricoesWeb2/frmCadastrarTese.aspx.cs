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

public partial class frmCadastrarTese : System.Web.UI.Page
{

    Participante SessionParticipante;
    Evento SessionEvento;


    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    SqlConnection SessionCnn;

    Tese SessionTese;
    TeseCad oTeseCad = new TeseCad();

    TeseParticipanteCad oTeseParticipanteCad = new TeseParticipanteCad();
    TeseParticipante SessionTeseParticipante;

    DataTable oDT = new DataTable();

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

            if (SessionParticipante == null)
                SessionParticipante = (Participante)Session["SessionParticipante"];
            else
                Session["SessionParticipante"] = SessionParticipante;

            if (SessionTese == null)
                SessionTese = (Tese)Session["SessionTese"];
            else
                Session["SessionTese"] = SessionTese;


            if (SessionEvento == null)
                Server.Transfer("frmSessaoExpirada.aspx", true);


            if ((Request["cdTese"] != null) &&
                (Request["cdTese"] != ""))
            {

                txtCdTese.Text = Request["cdTese"];


                Pesquisar(Request["cdTese"]);
            }
            else if (SessionTese != null)
            {

                txtCdTese.Text = SessionTese.CdTese;


                Pesquisar(SessionTese.CdTese);
            }

           
        }
        else
        {
            SessionTese = (Tese)Session["SessionTese"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionTeseParticipante = (TeseParticipante)Session["SessionTeseParticipante"];


        }

        if (SessionEvento == null)
            Server.Transfer("frmSessaoExpirada.aspx", true);

        btnExcluir.Attributes.Add("onclick", "return confirm('Esta operação irá excluir em definitivo a tese e seus subscritores!\\n\\nConfirma a exclusão da Tese?');");
        btnExcluirSubscritor.Attributes.Add("onclick", "return confirm('Esta operação irá excluir em definitivo os dados do subscritor!\\n\\nConfirma a exclusão?');");

        if (oTeseCad.TotalTesesInscrita(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn) < 2)
            btnNovo.Visible = true;
        else
            btnNovo.Visible = false;
    }

    protected void Pesquisar(String prmCdTese)
    {
        txtCdTese.Text = "";
        txtTipoTese.Text = "";
        txtAssuntoTese.Text = "";
        txtIntroducao.Text = "";
        txtProposta.Text = "";

        lblMsg.Text = "";



        SessionTese = oTeseCad.Pesquisar(prmCdTese, SessionCnn);

        Session["SessionTese"] = SessionTese;

        if (SessionTese == null)
        {
            lblMsg.Text = oTeseCad.RcMsg;
            return;
        }


        txtCdTese.Text = SessionTese.CdTese;

        txtTipoTese.Text = SessionTese.TpTese;
        txtAssuntoTese.Text = SessionTese.DsAssunto;
        txtIntroducao.Text = SessionTese.DsIntroducao;
        txtProposta.Text = SessionTese.DsProposta;

        pnlAcomodacoes.Visible = true;
        pnlAcomodacoes.Enabled = true;


        carregarGrdSubscritoresTese();
        
    }

    protected void btnNovo_Click(object sender, EventArgs e)
    {
        if (oTeseCad.TotalTesesInscrita(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn) < 2)
        {


            txtCdTese.Text = "";
            txtTipoTese.Text = "";
            txtAssuntoTese.Text = "";
            txtIntroducao.Text = "";
            txtProposta.Text = "";

            lblMsg.Text = "";

            btnNovaSubscritor_Click(sender, e);

            grdSubscritor.DataSource = null;
            grdSubscritor.DataBind();

        }
        lblMsg.Text = "Limite de cadastro de tese para o participante foi atingido!";
        
    }
    protected void btnGravar_Click(object sender, EventArgs e)
    {
        
        lblMsg.Text = "";
        //lblMsgErro.Text = "";


        Tese tmpTese = new Tese();


        tmpTese.CdTese = txtCdTese.Text;

        tmpTese.CdEvento = SessionEvento.CdEvento;
        tmpTese.CdParticipante = SessionParticipante.CdParticipante;
        tmpTese.TpTese = txtTipoTese.Text;


        tmpTese.DsAssunto = txtAssuntoTese.Text.Replace("'","").Replace(";",",");
        tmpTese.DsIntroducao = txtIntroducao.Text.Replace("'", "").Replace(";", ",");
        tmpTese.DsProposta = txtProposta.Text.Replace("'", "").Replace(";", ",");

        tmpTese.DsSituacao = "CADASTRADA";
        tmpTese.DsObservacao = "";

        SessionTese = oTeseCad.Gravar(tmpTese, "000000001", SessionCnn);


        if (SessionTese != null)
        {

            if (txtCdTese.Text == "")
            {

                try
                {
                    TeseParticipante tmpTeseParticipante = new TeseParticipante(
                        SessionTese.CdTese,
                        "",
                        SessionParticipante.NuCPFCNPJ,
                        SessionParticipante.NoParticipante.ToUpper(),
                        "APOIADOR",
                        SessionParticipante.NoInstituicao.ToUpper(),
                        false
                        );

                    tmpTeseParticipante = oTeseParticipanteCad.Gravar(tmpTeseParticipante, SessionCnn);
                }
                catch
                { }
            }
            txtCdTese.Text = SessionTese.CdTese;

            Pesquisar(txtCdTese.Text);
            lblMsg.Text = "Operação realizada com sucesso!";//<br/>Foi Enviado um e-mail com a senha para o patrocinador.";
        }
        else
        {
            lblMsg.Text = oTeseCad.RcMsg;
        }
         
    }
    protected void btnExcluir_Click(object sender, EventArgs e)
    {
        if (txtCdTese.Text == "")
            return;


        if (oTeseCad.ExcluirTese(txtCdTese.Text, SessionCnn))
        {
            btnNovo_Click(sender, e);
            lblMsg.Text = "Operação realizada com sucesso!";//<br/>Foi Enviado um e-mail com a senha para o patrocinador.";
        }
        else
        {
            lblMsg.Text = oTeseCad.RcMsg;
        }
    }

    protected void carregarGrdSubscritoresTese()
    {

        grdSubscritor.DataSource = null;
        grdSubscritor.DataBind();



        oDT = oTeseParticipanteCad.Listar(SessionTese.CdTese, SessionCnn);

        if (oDT == null)
        {
            return;
        }

        DataTable oDataTable = new DataTable();



        oDataTable.Columns.Add("cdParticipanteTese");
        oDataTable.Columns.Add("noParticipanteTese");
        oDataTable.Columns.Add("tpParticipacao");


        if ((oDT != null) && (oDT.DefaultView.Count >= 1))
        {

            for (int i = 0; i < oDT.DefaultView.Count; i++)
            {
                oDataTable.Rows.Add(oDT.DefaultView[i]["cdParticipanteTese"].ToString(),
                                    oDT.DefaultView[i]["noParticipanteTese"].ToString(),
                                    oDT.DefaultView[i]["tpParticipacao"].ToString());
            }
        }

        grdSubscritor.DataSource = oDataTable;
        grdSubscritor.DataBind();

    }
    protected void PesquisarSubscritor(String prmCdTese, String prmCdParticipanteTese)
    {
        txtTpTese.Text = "";
        txtCdParticipanteTese.Text = "";
        txtNoParticipanteTese.Text = "";
        txtCPFTese.Text = "";
        txtLotacao.Text = "";

        lblMsg.Text = "";



        SessionTeseParticipante = oTeseParticipanteCad.Pesquisar(prmCdTese, prmCdParticipanteTese, SessionCnn);

        Session["SessionTeseParticipante"] = SessionTeseParticipante;

        if (SessionTeseParticipante == null)
        {
            lblMsg.Text = oTeseParticipanteCad.RcMsg;
            return;
        }


        txtTpTese.Text = SessionTeseParticipante.TpParticipacao1;
        txtCdParticipanteTese.Text = SessionTeseParticipante.CdParticipanteTese;
        txtNoParticipanteTese.Text = SessionTeseParticipante.NoParticipanteTese;
        txtCPFTese.Text = oClsFuncoes.MascaraGerar(SessionTeseParticipante.NuCPF, "999.999.999-99");
        txtLotacao.Text = SessionTeseParticipante.DsLotacaoAtual;

        lblMsg.Text = "";

        
        carregarGrdSubscritoresTese();

    }
    protected void btnNovaSubscritor_Click(object sender, EventArgs e)
    {
        txtTpTese.Text = "";
        txtCdParticipanteTese.Text = "";
        txtNoParticipanteTese.Text = "";
        txtCPFTese.Text = "";
        txtLotacao.Text = "";

        lblMsg.Text = "";
    }
    protected void btnGravarSubscritor_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";

        if (txtTpTese.Text == "")
        {
            lblMsg.Text = "Campo Tipo da tese obrigatório!";
            
        }
        if (txtNoParticipanteTese.Text == "")
        {
            lblMsg.Text += (lblMsg.Text == "" ? "" :"<br />") + "Campo Nome do Participante obrigatório!";
        }
        if (txtCPFTese.Text == "")
        {
            lblMsg.Text += (lblMsg.Text == "" ? "" : "<br />") + "Campo CPF obrigatório!";
        }
        if (txtLotacao.Text == "")
        {
            lblMsg.Text += (lblMsg.Text == "" ? "" : "<br />") + "Campo Lotação obrigatório!";
        }
        

        if (lblMsg.Text != "")
            return;


        if ((!oClsFuncoes.CPFCNPJValidar(txtCPFTese.Text)))
        {
            
            lblMsg.Text = "CPF Inválido!";
            return;
        }
        else
        {
            //return "";
            string tmpCPF = txtCPFTese.Text.Replace(".", "").Replace("-", "");
            
            if ((tmpCPF == "11111111111") ||
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
                lblMsg.Text = "CPF Inválido!";
                return;
            }

        }

        //if (txtCdParticipanteTese.Text == "")
        //{
        TeseParticipante tmpTeseParticipanteDuplicidade = oTeseParticipanteCad.PesquisarCPFJaCadastrado(txtCdTese.Text, txtCPFTese.Text.Replace(".", "").Replace("-", ""), SessionCnn);
             if ((tmpTeseParticipanteDuplicidade != null) && (tmpTeseParticipanteDuplicidade.CdParticipanteTese != txtCdParticipanteTese.Text))
            {

                lblMsg.Text = "CPF Já cadastrado!";
                return;
            }
        //}

        if (oTeseParticipanteCad.TotalTeseCPF(SessionEvento.CdEvento, txtCPFTese.Text.Replace(".", "").Replace("-", ""), SessionCnn) >= 4)
        {
            lblMsg.Text = "CPF Já atingiu ou limite de subscrição em Teses!";
            return;
        }

         try
        {
            TeseParticipante tmpTeseParticipante = new TeseParticipante(
                txtCdTese.Text,
                txtCdParticipanteTese.Text,
                txtCPFTese.Text.Replace(".","").Replace("-",""),
                txtNoParticipanteTese.Text.ToUpper(),
                txtTpTese.SelectedValue.ToString(),
                txtLotacao.Text.ToUpper(),
                false
                );

            tmpTeseParticipante = oTeseParticipanteCad.Gravar(tmpTeseParticipante, SessionCnn);

           // txtCdAcomodacao.Text = tmpHotelAcomodacao.CdAcomodacao;
            PesquisarSubscritor(tmpTeseParticipante.CdTese,tmpTeseParticipante.CdParticipanteTese);
            carregarGrdSubscritoresTese();
            lblMsg.Text = "Operação realizada com sucesso!";
            
        }
        catch
        {
            lblMsg.Text = oTeseParticipanteCad.RcMsg;
            //lblMsgErro.Text = oParticipanteCad.RcMsg;
        }

        
    }
    protected void btnExcluirSubscritor_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";

        if (txtCdParticipanteTese.Text == "")
            return;

        if (txtCPFTese.Text.Replace(".", "").Replace("-", "") == SessionParticipante.NuCPFCNPJ)
        {
            lblMsg.Text = "Não é possível remover subscritor, pois o mesmo é o DELEGADO CONGRESSUAL!";
            return;
        }

        if (oTeseParticipanteCad.ExcluirParticipanteTese(txtCdTese.Text, txtCdParticipanteTese.Text, SessionCnn))
        {
            btnNovaSubscritor_Click(sender, e);
            carregarGrdSubscritoresTese();
            lblMsg.Text = "Operação realizada com sucesso!";//<br/>Foi Enviado um e-mail com a senha para o patrocinador.";
        }
        else
        {
            lblMsg.Text = oTeseCad.RcMsg;
        }
    }
    protected void grdSubscritor_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        PesquisarSubscritor(txtCdTese.Text, grdSubscritor.DataKeys[e.NewSelectedIndex].Values[0].ToString());
    }
}