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

public partial class AtividadesAbrasel : System.Web.UI.Page
{
    Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    Pedido SessionPedido;
    PedidoCad oPedidoCad = new PedidoCad();

    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    SqlConnection SessionCnn;
    
    String SessionIdioma;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            btnGravar.Attributes.Add("onclick", "document.body.style.cursor = 'wait'; this.value='Aguarde, Enviando...'; this.disabled = true; " + ClientScript.GetPostBackEventReference(btnGravar, string.Empty) + ";");
            btnGravar2.Attributes.Add("onclick", "document.body.style.cursor = 'wait'; this.value='Aguarde, Enviando...'; this.disabled = true; " + ClientScript.GetPostBackEventReference(btnGravar2, string.Empty) + ";");

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

            SessionParticipante = (Participante)Session["SessionParticipante"];
            SessionParticipante = oParticipanteCad.Pesquisar(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);
            Session["SessionParticipante"] = SessionParticipante;

            if (SessionPedido == null)
                SessionPedido = (Pedido)Session["SessionPedido"];
            else
                Session["SessionPedido"] = SessionPedido;

            if (SessionParticipante.FlConfirmacaoInscricao)
            {
                btnVoltar2.Visible = true;
                Button1.Visible = true;
            }

            lblIdentificador.Text = SessionParticipante.CdParticipante.Trim();
            lblNoParticipante.Text = SessionParticipante.NoParticipante.Trim();

            if (SessionIdioma == "PTBR")
                lblCategoria.Text = SessionParticipante.Categoria.NoCategoria.Trim();
            else if (SessionIdioma == "ENUS")
                lblCategoria.Text = SessionParticipante.Categoria.NoCategoriaIngles.Trim();
            else if (SessionIdioma == "ESP")
                lblCategoria.Text = SessionParticipante.Categoria.NoCategoriaEspanhol.Trim();
            else if (SessionIdioma == "FRA")
                lblCategoria.Text = SessionParticipante.Categoria.NoCategoriaFrances.Trim();

            prpHabilitaTABs();


            CarregarAtividadesParticipanteGrade();
        }
        else
        {
            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            SessionIdioma = (String)Session["SessionIdioma"];

            SessionPedido = (Pedido)Session["SessionPedido"];

            if (SessionEvento == null)
                Server.Transfer("frmSessaoExpirada.aspx", true);

        }

        CarregarAtividadesGrade();
        

        ToolkitScriptManager1.RegisterPostBackControl(btnGravar);
        ToolkitScriptManager1.RegisterPostBackControl(btnGravar2);
    }
    /*
    override protected void OnInit(EventArgs e)
    {
        if (SessionParticipante == null)
            SessionParticipante = (Participante)Session["SessionParticipante"];
        else
            Session["SessionParticipante"] = SessionParticipante;
    }
    */

    private void prpHabilitaTABs()
    {
        string sqlTxt =
            "SELECT m.cdAtividade, " +
            "       a.noTitulo, " +
            "       l.noLocal, " +
            "       CONVERT(CHAR(10), a.dtInicio,103) + ' ' + CONVERT(CHAR(5), a.dtInicio,8) dtIni, " +
            "       CONVERT(CHAR(10), a.dtTermino,103) + ' ' + CONVERT(CHAR(5), a.dtTermino,8) dtTermino, " +
            "       a.dsTema, " +
            "       a.cdTipoAtividade, " +
            "       ta.noTipoAtividade  " +
            "FROM tbPedidos p  " +
            "join tbPedidosAtividades m on p.cdPedido = m.cdPedido  " +
            //tbMatriculas m " +
            "join tbAtividades a " +
            "  on m.cdAtividade = a.cdAtividade " +
           // " and m.cdEvento = a.cdEvento " +

            "join tbLocais l " +
            "  on a.cdLocal = l.cdLocal   " +
            "join tbTiposAtividades ta " +
            "  on a.cdTipoAtividade = ta.cdTipoAtividade " +

            "where a.cdEvento = '" + SessionEvento.CdEvento + "'  " +
            "and p.cdParticipante = '" + SessionParticipante.CdParticipante + "'  " +
            //"AND m.cdAtividade <= '001606009' " +
            "AND m.cdAtividade in ('001606002','001606003','001606008','001606009') " +
            (SessionPedido != null ? "and p.cdPedido = '" + SessionPedido.CdPedido + "'  " : "") +

            "union " +

            "SELECT m.cdAtividade, " +
            "       a.noTitulo, " +
            "       l.noLocal, " +
            "       CONVERT(CHAR(10), a.dtInicio,103) + ' ' + CONVERT(CHAR(5), a.dtInicio,8) dtIni, " +
            "       CONVERT(CHAR(10), a.dtTermino,103) + ' ' + CONVERT(CHAR(5), a.dtTermino,8) dtTermino, " +
            "       a.dsTema, " +
            "       a.cdTipoAtividade, " +
            "       ta.noTipoAtividade  " +
            "FROM tbMatriculas m " +
            "join tbAtividades a " +
            "  on m.cdAtividade = a.cdAtividade " +
            // " and m.cdEvento = a.cdEvento " +

            "join tbLocais l " +
            "  on a.cdLocal = l.cdLocal   " +
            "join tbTiposAtividades ta " +
            "  on a.cdTipoAtividade = ta.cdTipoAtividade " +

            "where a.cdEvento = '" + SessionEvento.CdEvento + "'  " +
            "and m.cdParticipante = '" + SessionParticipante.CdParticipante + "'  " +

            //"AND m.cdAtividade <= '001606009' ";
            "AND m.cdAtividade in ('001606002','001606003','001606008','001606009') ";

        SqlCommand comando = new SqlCommand(
                    sqlTxt, SessionCnn);

        SqlDataAdapter Dap;
        DataTable DTAtividades = new DataTable();

        Dap = new SqlDataAdapter(comando);

        Dap.TableMappings.Add("Atividades", "tbAtividades");
        Dap.Fill(DTAtividades);



        //workshop.Visible = false;
        //Mesa.Visible = false;
        

        if ((DTAtividades != null) && (DTAtividades.DefaultView.Count > 0))
        {
            for (int i = 0; i < DTAtividades.DefaultView.Count; i++)
            {
                if (DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606009")
                {
                    //workshop.Visible = true;
                    Mesa.Visible = true;

                    chkTodosDiasWS.Checked = true;
                    prpchkDia13WS();
                    TabContainer1.ActiveTabIndex = 0;

                    
                    //chkTodosDiasM.Checked = true;
                    prpchkDia13M();
                }

                if (DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606008")
                {
                    //workshop.Visible = true;
                    Mesa.Visible = true;

                    chkDia12WS.Visible = true;
                    chkDia12WS.Checked = true;
                    chkDia13WS.Visible = true;
                    prpchkDia13WS();
                    TabContainer1.ActiveTabIndex = 0;

                    Mesa.Visible = true;
                    //chkDia12WS.Checked = true;
                    //chkDia12WS.Visible = true;
                    //chkDia13WS.Visible = true;
                    prpchkDia13M();
                }

                if (DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606007")
                {
                    //workshop.Visible = true;
                    chkTodosDiasWS.Checked = true;
                    prpchkDia13WS();
                    TabContainer1.ActiveTabIndex = 0;

                    Mesa.Visible = false;
                }

                if (DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606006")
                {
                    //workshop.Visible = true;
                    chkDia12WS.Visible = true;
                    chkDia12WS.Checked = true;
                    chkDia13WS.Visible = true;
                    prpchkDia13WS();
                    TabContainer1.ActiveTabIndex = 0;

                    Mesa.Visible = false;
                }

                if (DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606003")
                {
                    Mesa.Visible = true;
                    workshop.Visible = false;
                    chkTodosDiasWS.Checked = true;
                    prpchkDia13WS();
                    TabContainer1.ActiveTabIndex = 0;

                    
                }

                if (DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606002")
                {
                    
                    Mesa.Visible = true;
                    workshop.Visible = false;
                    
                    chkDia12WS.Checked = true;
                    chkDia12WS.Visible = true;
                    chkDia13WS.Visible = true;
                    //chkDia13WS.Checked = true;
                    prpchkDia13WS();
                    TabContainer1.ActiveTabIndex = 0;

                    
                }



                //if (DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim() == "001606010")
                //{
                    
                //    if (workshop.Visible)
                //        TabContainer1.ActiveTabIndex = 0;
                //    else if (Mesa.Visible)
                //        TabContainer1.ActiveTabIndex = 1;
                //    else
                //        TabContainer1.ActiveTabIndex = 2;
                //}
            }
        }
        else
        {
            if (!SessionParticipante.FlConfirmacaoInscricao)
                Response.Redirect("frm_formapagamento.aspx?cdMatricula=" + SessionParticipante.CdParticipante, false);
            else
            {
                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                               "008",
                               ""), true);
            }
            return;
        }

        //Mesa.Visible = false;
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

    private void CarregarAtividadesGrade()
    {        

        string sqlTxt =
            "select a.cdAtividade, " +
            "       a.noTitulo, " +
            "       l.noLocal, " +
            "       CONVERT(CHAR(10), a.dtInicio,103) + ' ' + CONVERT(CHAR(5), a.dtInicio,8) dtIni, " +
            "       CONVERT(CHAR(10), a.dtTermino,103) + ' ' + CONVERT(CHAR(5), a.dtTermino,8) dtTermino, " +
            "       l.vlCapacidade, " +
            "       l.vlCapacidade - coalesce(mat.total_mat,0) - 0 vagas_em_aberto, " +
            "       a.dsTema, " +
            "       a.cdTipoAtividade, " +
            "       ta.noTipoAtividade, " +
            "       a.noTituloWeb " +

            "from tbAtividades a " +

            "join tbLocais l " +
            "  on a.cdLocal = l.cdLocal " +

            "join ( " +
            "	select a.cdEvento, a.cdAtividade, SUM(case when(m.cdAtividade is null) then 0 else CASE WHEN (m.flAtivo = 1) THEN m.vlQuantidade ELSE 0 END end) total_mat " +
            "	from tbAtividades a " +
            "	left join tbMatriculas m on a.cdAtividade = m.cdAtividade " +

            "	group by a.cdEvento, a.cdAtividade " +
            ") mat  on a.cdEvento =  mat.cdEvento " +
            "          and a.cdAtividade = mat.cdAtividade   " +

            "join tbTiposAtividades ta " +
              "on a.cdTipoAtividade = ta.cdTipoAtividade  " +

            "where a.cdEvento = '" + SessionEvento.CdEvento +"' " +

            //"and a.flAtivo = 1 " +

            "AND a.cdAtividade >= 001606010 " +

            "group by a.cdAtividade, " +
            "       a.noTitulo, " +
            "       l.noLocal, " +
            "       CONVERT(CHAR(10), a.dtInicio,103) + ' ' + CONVERT(CHAR(5), a.dtInicio,8), " +
            "       CONVERT(CHAR(10), a.dtTermino,103) + ' ' + CONVERT(CHAR(5), a.dtTermino,8), " +
            "       l.vlCapacidade,   " +
            "	   mat.total_mat, " +
            "       a.dsTema, " +
            "       a.cdTipoAtividade, " +
            "       a.vlAtividade, " +
            "       ta.noTipoAtividade, " +
            "       a.noTituloWeb " +

            "order by cdAtividade  ";

        SqlCommand comando = new SqlCommand(
                    sqlTxt, SessionCnn);

        SqlDataAdapter Dap;
        DataTable DTAtividades = new DataTable();

        Dap = new SqlDataAdapter(comando);

        Dap.TableMappings.Add("Atividades", "tbAtividades");
        Dap.Fill(DTAtividades);

        

       

        if ((DTAtividades != null) && (DTAtividades.DefaultView.Count > 0))
        {
            for (int i = 0; i < DTAtividades.DefaultView.Count; i++)
            {
                //if (((int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) >= 1606010) && (int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) <= 1606018))
                //   // ||  ((int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) == 1604072) || (int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) == 1604073))
                //    )
                //{
                //    Control chk = FindControlRecursive(this.Page, "W_" + DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim());
                //    if (chk != null)
                //    {
                //        if (int.Parse(DTAtividades.DefaultView[i]["vagas_em_aberto"].ToString().Trim()) > 0)
                //            (chk as CheckBox).Text = DTAtividades.DefaultView[i]["noTitulo"].ToString().Trim().Replace("#", "");
                //        else
                //            (chk as CheckBox).Text = DTAtividades.DefaultView[i]["noTitulo"].ToString().Trim().Replace("#", "<font color='red'>  ( Vagas Esgotadas )</font>");

                //    }
                //}
                if ((int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) >= 1606019) && (int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) <= 1606030))
                {
                    Control chk = FindControlRecursive(this.Page, "F_" + DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim());
                    if (chk != null)
                    {
                        if (int.Parse(DTAtividades.DefaultView[i]["vagas_em_aberto"].ToString().Trim()) > 0)
                            (chk as CheckBox).Text = DTAtividades.DefaultView[i]["noTituloWeb"].ToString().Trim().Replace("#", "");
                        else
                            (chk as CheckBox).Text = DTAtividades.DefaultView[i]["noTituloWeb"].ToString().Trim().Replace("#", "<font color='red'>  ( Vagas Esgotadas )</font>");

                        if ((chk as CheckBox).Text == "")
                            (chk as CheckBox).Visible = false;

                        if ((chk as CheckBox).Text.ToUpper().Contains("EM BREVE"))
                            (chk as CheckBox).Enabled = false;
                    }
                }
                if ((int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) >= 1606031) && (int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) <= 1606066))
                {
                    Control chk = FindControlRecursive(this.Page, "M_" + DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim());
                    if (chk != null)
                    {
                        if (int.Parse(DTAtividades.DefaultView[i]["vagas_em_aberto"].ToString().Trim()) > 0)
                            (chk as CheckBox).Text = DTAtividades.DefaultView[i]["noTituloWeb"].ToString().Trim().Replace("#", "");
                        else
                            (chk as CheckBox).Text = DTAtividades.DefaultView[i]["noTituloWeb"].ToString().Trim().Replace("#", "<font color='red'>  ( Vagas Esgotadas )</font>");

                        if ((chk as CheckBox).Text == "")
                            (chk as CheckBox).Visible = false;

                        if ((chk as CheckBox).Text.ToUpper().Contains("EM BREVE"))
                            (chk as CheckBox).Enabled = false;
                    }
                }
                //if ((int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) >= 1604068) && (int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) <= 1604071))
                //{
                //    Control chk = FindControlRecursive(this.Page, "V_" + DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim());
                //    if (chk != null)
                //    {
                //        if (int.Parse(DTAtividades.DefaultView[i]["vagas_em_aberto"].ToString().Trim()) > 0)
                //            (chk as CheckBox).Text = DTAtividades.DefaultView[i]["noTitulo"].ToString().Trim().Replace("#", "");
                //        else
                //            (chk as CheckBox).Text = DTAtividades.DefaultView[i]["noTitulo"].ToString().Trim().Replace("#", "<font color='red'>  ( Vagas Esgotadas )</font>");
                //    }
                //}
            }
        }
        
    }
    private void CarregarAtividadesParticipanteGrade()
    {
        string sqlTxt =
            "SELECT m.cdAtividade, " +
            "       a.noTitulo, " +
            "       l.noLocal, " +
            "       CONVERT(CHAR(10), a.dtInicio,103) + ' ' + CONVERT(CHAR(5), a.dtInicio,8) dtIni, " +
            "       CONVERT(CHAR(10), a.dtTermino,103) + ' ' + CONVERT(CHAR(5), a.dtTermino,8) dtTermino, " +
            "       a.dsTema, " +
            "       a.cdTipoAtividade, " +
            "       ta.noTipoAtividade  " +
            "FROM tbMatriculas m " +
            "join tbAtividades a " +
            "  on m.cdAtividade = a.cdAtividade " +
            " and m.cdEvento = a.cdEvento " +

            "join tbLocais l " +
            "  on a.cdLocal = l.cdLocal   " +
            "join tbTiposAtividades ta " +
            "  on a.cdTipoAtividade = ta.cdTipoAtividade " +

            "where a.cdEvento = '" + SessionEvento.CdEvento + "'  " +
            "and m.cdParticipante = '" + SessionParticipante.CdParticipante + "'  " +
            "AND a.cdAtividade >= '001606010' ";

        SqlCommand comando = new SqlCommand(
                    sqlTxt, SessionCnn);

        SqlDataAdapter Dap;
        DataTable DTAtividades = new DataTable();

        Dap = new SqlDataAdapter(comando);

        Dap.TableMappings.Add("Atividades", "tbAtividades");
        Dap.Fill(DTAtividades);
        
        if ((DTAtividades != null) && (DTAtividades.DefaultView.Count > 0))
        {
            for (int i = 0; i < DTAtividades.DefaultView.Count; i++)
            {
                //if ((int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) >= 1606011) && (int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) <= 1606017))
                //{
                //    Control chk = FindControlRecursive(this.Page, "W_" + DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim());
                //    if (chk != null)
                //    {
                //        (chk as CheckBox).Checked = true;
                //    }
                //}
                if ((int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) >= 1606019) && (int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) <= 1606030))
                {
                    Control chk = FindControlRecursive(this.Page, "F_" + DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim());
                    if (chk != null)
                    {
                        (chk as CheckBox).Checked = true;
                    }
                }
                if ((int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) >= 1606031) && (int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) <= 1606066))
                {
                    Control chk = FindControlRecursive(this.Page, "M_" + DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim());
                    if (chk != null)
                    {
                        (chk as CheckBox).Checked = true;
                    }
                }
                //if ((int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) >= 1604068) && (int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) <= 1604071))
                //{
                //    Control chk = FindControlRecursive(this.Page, "V_" + DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim());
                //    if (chk != null)
                //    {
                //        (chk as CheckBox).Checked = true;
                //    }
                //}
            }
        }
    }

    private int verificarVagas(String prmCdAtividade)
    {

        string sqlTxt =
            "select a.cdAtividade, " +
            "       a.noTitulo, " +
            "       l.noLocal, " +
            "       CONVERT(CHAR(10), a.dtInicio,103) + ' ' + CONVERT(CHAR(5), a.dtInicio,8) dtIni, " +
            "       CONVERT(CHAR(10), a.dtTermino,103) + ' ' + CONVERT(CHAR(5), a.dtTermino,8) dtTermino, " +
            "       l.vlCapacidade, " +
            "       l.vlCapacidade - coalesce(mat.total_mat,0) - 0 vagas_em_aberto, " +
            "       a.dsTema, " +
            "       a.cdTipoAtividade, " +
            "       ta.noTipoAtividade " +

            "from tbAtividades a " +

            "join tbLocais l " +
            "  on a.cdLocal = l.cdLocal " +

            "join ( " +
            "	select a.cdEvento, a.cdAtividade, SUM(case when(m.cdAtividade is null) then 0 else CASE WHEN (m.flAtivo = 1) THEN m.vlQuantidade ELSE 0 END end) total_mat " +
            "	from tbAtividades a " +
            "	left join tbMatriculas m on a.cdAtividade = m.cdAtividade " +

            "	group by a.cdEvento, a.cdAtividade " +
            ") mat  on a.cdEvento =  mat.cdEvento " +
            "          and a.cdAtividade = mat.cdAtividade   " +

            "join tbTiposAtividades ta " +
              "on a.cdTipoAtividade = ta.cdTipoAtividade  " +

            "where a.cdEvento = '" + SessionEvento.CdEvento + "' " +

            //"and a.flAtivo = 1 " +

            "AND a.cdAtividade = '" + prmCdAtividade + "' " +

            "group by a.cdAtividade, " +
            "       a.noTitulo, " +
            "       l.noLocal, " +
            "       CONVERT(CHAR(10), a.dtInicio,103) + ' ' + CONVERT(CHAR(5), a.dtInicio,8), " +
            "       CONVERT(CHAR(10), a.dtTermino,103) + ' ' + CONVERT(CHAR(5), a.dtTermino,8), " +
            "       l.vlCapacidade,   " +
            "	   mat.total_mat, " +
            "       a.dsTema, " +
            "       a.cdTipoAtividade, " +
            "       a.vlAtividade, " +
            "       ta.noTipoAtividade " +

            "order by cdAtividade  ";

        SqlCommand comando = new SqlCommand(
                    sqlTxt, SessionCnn);

        SqlDataAdapter Dap;
        DataTable DTAtividades = new DataTable();

        Dap = new SqlDataAdapter(comando);

        Dap.TableMappings.Add("Atividades", "tbAtividades");
        Dap.Fill(DTAtividades);

        if ((DTAtividades != null) && (DTAtividades.DefaultView.Count > 0))
        {
            return int.Parse(DTAtividades.DefaultView[0]["vagas_em_aberto"].ToString().Trim());
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
            return 0;
        }

    }



    protected void chkDia13WS_CheckedChanged(object sender, EventArgs e)
    {
        prpchkDia13WS();
    }
    protected void prpchkDia13WS()
    {
        if (chkDia12WS.Checked)
        {
            d12_WS.Visible = true;
            
            d13_WS.Visible = false;
            F_001606026.Checked = false;
            F_001606028.Checked = false;
            
            F_001606025.Checked = false;
            F_001606027.Checked = false;
            F_001606029.Checked = false;
        }
        else if (chkDia13WS.Checked)
        {
            d12_WS.Visible = false;
            F_001606021.Checked = false;
            F_001606025.Checked = false;

            F_001606019.Checked = false;
            F_001606020.Checked = false;
            F_001606022.Checked = false;
            F_001606024.Checked = false;

            d13_WS.Visible = true;            
        }
        else if (chkTodosDiasWS.Checked)
        {
            d12_WS.Visible = true;
            

            d13_WS.Visible = true;
            
        }


        if (chkDia12WS.Checked)
        {
            d12_M.Visible = true;
            d13_M.Visible = false;

            M_001606050.Checked = false;
            M_001606051.Checked = false;
            M_001606052.Checked = false;
            M_001606053.Checked = false;
            M_001606054.Checked = false;
            M_001606055.Checked = false;
            //M_001606056.Checked = false;
            M_001606057.Checked = false;
            M_001606058.Checked = false;
            M_001606059.Checked = false;
            M_001606060.Checked = false;
            M_001606061.Checked = false;
            M_001606062.Checked = false;
            M_001606063.Checked = false;
            M_001606064.Checked = false;
            M_001606065.Checked = false;
            M_001606066.Checked = false;

            if (!workshop.Visible)
                TabContainer1.ActiveTabIndex = 0;

        }
        else if (chkDia13WS.Checked)
        {
            d12_M.Visible = false;
            d13_M.Visible = true;

            M_001606031.Checked = false;
            M_001606032.Checked = false;
            M_001606033.Checked = false;
            M_001606034.Checked = false;
            M_001606035.Checked = false;
            M_001606036.Checked = false;
            M_001606037.Checked = false;
            M_001606038.Checked = false;
            M_001606039.Checked = false;
            M_001606040.Checked = false;
            M_001606041.Checked = false;
            M_001606042.Checked = false;
            M_001606043.Checked = false;
            M_001606044.Checked = false;
            M_001606045.Checked = false;
            M_001606046.Checked = false;
            M_001606047.Checked = false;

            if (!workshop.Visible)
                TabContainer1.ActiveTabIndex = 0;
        }
        else if (chkTodosDiasWS.Checked)
        {
            d12_M.Visible = true;


            d13_M.Visible = true;

        }
    }
        
    protected bool prpVerificarChoqueHorario(string prmAtividade1, string prmAtividade2)
    {
        AtividadeCad oAtividadeCad = new AtividadeCad();
        Atividade atv1 = oAtividadeCad.Pesquisar(SessionEvento.CdEvento, prmAtividade1, SessionCnn);
        Atividade atv2 = oAtividadeCad.Pesquisar(SessionEvento.CdEvento, prmAtividade2, SessionCnn);


        if ((atv2.DtInicio >= atv1.DtInicio) && (atv2.DtInicio <= atv1.DtTermino))
            return true;

        if ((atv2.DtTermino >= atv1.DtInicio) && (atv2.DtTermino <= atv1.DtTermino))
            return true;

        //if ((atv1.DtInicio >= atv2.DtInicio) && (atv1.DtInicio <= atv2.DtTermino))
        //    return true;

        //if ((atv1.DtTermino >= atv2.DtInicio) && (atv1.DtTermino <= atv2.DtTermino))
        //    return true;

        return false;
    }

    protected CheckBox prpVerificarSelecaoMesa(string prmCdAtividadeBase)
    {
        foreach (Control ctrltab in TabContainer1.Controls)
        {
            if (ctrltab is TabPanel)
            {
                //if ((ctrltab as TabPanel).ID.ToUpper() == "MESA")
                //{
                    foreach (Control ctrlcontent in ctrltab.Controls)
                    {
                        foreach (Control ctrldivs in ctrlcontent.Controls)
                        {
                            if ((ctrldivs.ID != null) && (!ctrldivs.ID.ToUpper().Contains("DIAS")))
                            {
                                foreach (Control ctrlchk in ctrldivs.Controls)
                                {

                                    if (ctrlchk is CheckBox)
                                    {
                                        if ((!(ctrlchk as CheckBox).ID.Contains(prmCdAtividadeBase))  && ((ctrlchk as CheckBox).Checked))
                                        {
                                            if (prpVerificarChoqueHorario(prmCdAtividadeBase, (ctrlchk as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")))
                                            {
                                                string txt = "";

                                                txt = (ctrlchk as CheckBox).Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "").Replace("<font color='navy'>", "").Replace("</font>", "");

                                                txt = "\"" + (ctrltab as TabPanel).HeaderText + ": " + txt.Substring(0, txt.IndexOf(") ")).Trim() + "...";

                                                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                                                    "<script type='text/javascript'> " +
                                                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                                                    txt +
                                                    "\\n\\nVocê de desmarcá-la antes.'); " +
                                                    "</script>", false);

                                                return (ctrlchk as CheckBox);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                //}
            }
        }

        return null;
            //Control txtcpf = FindControlRecursive(this.Mesa, "M_001606031");
    }

    protected void F_001606019_CheckedChanged(object sender, EventArgs e)
    {
        if ((sender as CheckBox).Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                (sender as CheckBox).Checked = false; return;
            }
            object temp = prpVerificarSelecaoMesa((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", ""));

            if (temp != null)
            {
                (sender as CheckBox).Checked = false;
            }
        }


        TabContainer1.ActiveTabIndex = 0;

        /*
        //if (F_001606019.Checked)
        if ((sender as CheckBox).Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                //ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                //    "<script type='text/javascript'> " +
                //    "alert('Não há mais vagas para esta atividade!'); " +
                //    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }

            object temp = prpVerificarSelecaoMesa((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", ""));

            //if ((M_001606031.Checked) || (M_001606032.Checked))
            if (temp != null) 
            {
                string txt = "";

                txt = (temp as CheckBox).Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "").Replace("<font color='navy'>", "").Replace("</font>","");

                ////if (M_001606031.Checked)
                ////    txt = M_001606031.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                ////else if (M_001606032.Checked)
                ////    txt = M_001606032.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                
                ////txt = "\"" + txt.Substring(0, txt.IndexOf(") ")).Trim() + "...\\n\\n\"";

                //ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                //    "<script type='text/javascript'> " +
                //    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                //    txt +
                //    "\\n\\nVocê de desmarcá-la antes.'); " +
                //    "</script>", false);
                ////F_001606019.Checked = false;
                (sender as CheckBox).Checked = false;
            }
        }*/
    }
    protected void F_001606020_CheckedChanged(object sender, EventArgs e)
    {
        if (F_001606020.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            
            //F_001606021.Checked = false;

            if ((M_001606031.Checked) || (M_001606032.Checked) || (M_001606033.Checked) || (M_001606034.Checked))
            {
                string txt = "";

                if (M_001606031.Checked) 
                    txt = M_001606031.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>","");
                else if (M_001606032.Checked)
                    txt = M_001606032.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001606033.Checked)
                    txt = M_001606033.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001606034.Checked)
                    txt = M_001606034.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");

                txt = "\"" + txt.Substring(0, txt.IndexOf(") ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                F_001606020.Checked = false;
            }
        }
    }
    protected void F_001606021_CheckedChanged(object sender, EventArgs e)
    {
        if (F_001606021.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            
            //F_001606020.Checked = false;

            if ((M_001606035.Checked) || (M_001606036.Checked) || (M_001606037.Checked) ||
                (M_001606038.Checked) || (M_001606039.Checked) || (M_001606040.Checked))
            {
                string txt = "";

                if (M_001606035.Checked)
                    txt = M_001606035.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001606036.Checked)
                    txt = M_001606036.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001606037.Checked)
                    txt = M_001606037.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001606038.Checked)
                    txt = M_001606038.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001606039.Checked)
                    txt = M_001606039.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001606040.Checked)
                    txt = M_001606040.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");

                txt = "\"" + txt.Substring(0, txt.IndexOf(") ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                F_001606021.Checked = false;
            }
        }
    }
    
    protected void F_001606022_CheckedChanged(object sender, EventArgs e)
    {
        if (F_001606022.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }

            //F_001606023.Checked = false;

            if ((M_001606038.Checked) || (M_001606039.Checked) || (M_001606040.Checked) || (M_001606041.Checked) || (M_001606042.Checked) || (M_001606043.Checked))
            {
                string txt = "";

                if (M_001606038.Checked)
                    txt = M_001606038.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001606039.Checked)
                    txt = M_001606039.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001606040.Checked)
                    txt = M_001606040.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001606041.Checked)
                    txt = M_001606041.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001606042.Checked)
                    txt = M_001606042.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001606043.Checked)
                    txt = M_001606043.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");

                txt = "\"" + txt.Substring(0, txt.IndexOf(") ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                F_001606022.Checked = false;
            }
        }
    }
    protected void F_001606023_CheckedChanged(object sender, EventArgs e)
    {
        if (F_001606023.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }

            //F_001606022.Checked = false;

            if ((M_001606041.Checked) || (M_001606042.Checked) || (M_001606043.Checked) || (M_001606044.Checked) || (M_001606045.Checked) || (M_001606046.Checked))
            {
                string txt = "";

                if (M_001606041.Checked)
                    txt = M_001606041.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001606042.Checked)
                    txt = M_001606042.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001606043.Checked)
                    txt = M_001606043.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001606044.Checked)
                    txt = M_001606044.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001606045.Checked)
                    txt = M_001606045.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001606046.Checked)
                    txt = M_001606046.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");

                txt = "\"" + txt.Substring(0, txt.IndexOf(") ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                F_001606023.Checked = false;
            }
        }
    }

    protected void F_001606024_CheckedChanged(object sender, EventArgs e)
    {
        if (F_001606024.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            
            //F_001606025.Checked = false;

            if ((M_001606045.Checked) || (M_001606046.Checked) || (M_001606047.Checked))
            {
                string txt = "";
                if (M_001606045.Checked)
                    txt = M_001606045.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001606046.Checked)
                    txt = M_001606046.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001606047.Checked)
                    txt = M_001606047.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");

                txt = "\"" + txt.Substring(0, txt.IndexOf(") ")).Trim() + "...\\n\\n\"";

                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);
                F_001606024.Checked = false;
            }
        }
    }
    protected void F_001606025_CheckedChanged(object sender, EventArgs e)
    {
        if (F_001606025.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            
            //F_001606024.Checked = false;

            if ((M_001606050.Checked) || (M_001606051.Checked))
            {
                string txt = "";
                if (M_001606050.Checked)
                    txt = M_001606050.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001606051.Checked)
                    txt = M_001606051.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                
                txt = "\"" + txt.Substring(0, txt.IndexOf(") ")).Trim() + "...\\n\\n\"";

                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);
                F_001606025.Checked = false;
            }
        }
    }
     
    protected void F_001606026_CheckedChanged(object sender, EventArgs e)
    {
        if (F_001606026.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            
            //F_001606025.Checked = false;

            if ((M_001606051.Checked) || (M_001606052.Checked) || (M_001606053.Checked) || (M_001606054.Checked) || (M_001606055.Checked))
            {
                string txt = "";

                if (M_001606051.Checked)
                    txt = M_001606051.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001606052.Checked)
                    txt = M_001606052.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001606053.Checked)
                    txt = M_001606053.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001606054.Checked)
                    txt = M_001606054.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001606054.Checked)
                    txt = M_001606054.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                
                txt = "\"" + txt.Substring(0, txt.IndexOf(") ")).Trim() + "...\\n\\n\"";

                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                F_001606026.Checked = false;
            }
        }
    }

    protected void F_001606027_CheckedChanged(object sender, EventArgs e)
    {
        if (F_001606027.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            
            //F_001606028.Checked = false;

            if ((M_001606054.Checked) || (M_001606055.Checked) || (M_001606057.Checked) || (M_001606058.Checked) || (M_001606059.Checked))
            {
                string txt = "";

                if (M_001606054.Checked)
                    txt = M_001606054.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001606055.Checked)
                    txt = M_001606055.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                //else if (M_001606056.Checked)
                //    txt = M_001606056.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001606057.Checked)
                    txt = M_001606057.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001606058.Checked)
                    txt = M_001606058.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001606059.Checked)
                    txt = M_001606059.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                
                txt = "\"" + txt.Substring(0, txt.IndexOf(") ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                F_001606027.Checked = false;
            }
        }
    }
    protected void F_001606028_CheckedChanged(object sender, EventArgs e)
    {
        if (F_001606028.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            
            //F_001606027.Checked = false;

            if ((M_001606057.Checked) || (M_001606058.Checked) || (M_001606059.Checked) || (M_001606060.Checked) || (M_001606061.Checked) || (M_001606062.Checked))
            {
                string txt = "";

                if (M_001606057.Checked)
                    txt = M_001606057.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001606058.Checked)
                    txt = M_001606058.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001606059.Checked)
                    txt = M_001606059.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");                    
                else if (M_001606060.Checked)
                    txt = M_001606060.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");                    
                else if (M_001606061.Checked)
                    txt = M_001606061.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001606062.Checked)
                    txt = M_001606062.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");

                txt = "\"" + txt.Substring(0, txt.IndexOf(") ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                F_001606028.Checked = false;
            }
        }
    }

    protected void F_001606029_CheckedChanged(object sender, EventArgs e)
    {
        if (F_001606029.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }

            //F_001606030.Checked = false;

            if ((M_001606060.Checked) || (M_001606061.Checked) || (M_001606062.Checked) || (M_001606063.Checked) || (M_001606064.Checked) || (M_001606065.Checked))
            {
                string txt = "";

                if (M_001606060.Checked)
                    txt = M_001606060.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001606061.Checked)
                    txt = M_001606061.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001606062.Checked)
                    txt = M_001606062.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001606063.Checked)
                    txt = M_001606063.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001606064.Checked)
                    txt = M_001606064.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001606065.Checked)
                    txt = M_001606065.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");

                txt = "\"" + txt.Substring(0, txt.IndexOf(") ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                F_001606029.Checked = false;
            }
        }
    }
    protected void F_001606030_CheckedChanged(object sender, EventArgs e)
    {
        if (F_001606030.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }

            //F_001606029.Checked = false;

            if ((M_001606064.Checked) || (M_001606065.Checked) || (M_001606066.Checked))
            {
                string txt = "";

                if (M_001606064.Checked)
                    txt = M_001606064.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001606065.Checked)
                    txt = M_001606065.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001606066.Checked)
                    txt = M_001606066.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");

                txt = "\"" + txt.Substring(0, txt.IndexOf(") ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                F_001606030.Checked = false;
            }
        }
    }
    protected void chkDia13M_CheckedChanged(object sender, EventArgs e)
    {
        prpchkDia13M();
    }
    protected void prpchkDia13M()
    {
        //if (chkDia12M.Checked)
        //{
        //    d12_M.Visible = true;
        //    d13_M.Visible = false;

        //    M_001606050.Checked = false;
        //    M_001606051.Checked = false;
        //    M_001606052.Checked = false;
        //    M_001606053.Checked = false;
        //    M_001606054.Checked = false;
        //    M_001606055.Checked = false;
        //    M_001606056.Checked = false;
        //    M_001606057.Checked = false;
        //    M_001606058.Checked = false;
        //    M_001606059.Checked = false;
        //    M_001606060.Checked = false;
        //    M_001606061.Checked = false;
        //    M_001606062.Checked = false;
        //    M_001606063.Checked = false;
        //    M_001606064.Checked = false;
        //    M_001606065.Checked = false;
        //    M_001606066.Checked = false;

        //}
        //else if (chkDia13M.Checked)
        //{
        //    d12_M.Visible = false;
        //    d13_M.Visible = true;

        //    M_001606031.Checked = false;
        //    M_001606032.Checked = false;
        //    M_001606033.Checked = false;
        //    M_001606034.Checked = false;
        //    M_001606035.Checked = false;
        //    M_001606036.Checked = false;
        //    M_001606037.Checked = false;
        //    M_001606038.Checked = false;
        //    M_001606039.Checked = false;
        //    M_001606040.Checked = false;
        //    M_001606041.Checked = false;
        //    M_001606042.Checked = false;
        //    M_001606043.Checked = false;
        //    M_001606044.Checked = false;
        //    M_001606045.Checked = false;
        //    M_001606046.Checked = false;
        //    M_001606047.Checked = false;

        //}
        //else if (chkTodosDiasM.Checked)
        //{
        //    d12_M.Visible = true;


        //    d13_M.Visible = true;

        //}
    }
    protected void M_001606031_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001606031.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            M_001606032.Checked = false;

            

            if ((F_001606019.Checked) || (F_001606020.Checked))// || (M_001606032.Checked))
            {
                string txt = "";
                if (F_001606019.Checked)
                    txt = F_001606019.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (F_001606020.Checked)
                    txt = F_001606020.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                //else if (M_001606032.Checked)
                //    txt = M_001606032.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");

                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";

                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", 
                    "<script type='text/javascript'> "+
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n"+
                    txt +
                    "\\n\\nVocê de desmarcá-la antes.'); " +
                    "</script>", false);
                M_001606031.Checked = false;
            }
        }
    }
    protected void M_001606032_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001606032.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            M_001606031.Checked = false;
            if ((F_001606019.Checked) || (F_001606020.Checked))
            {
                string txt = "";

                if (F_001606019.Checked)
                    txt = F_001606019.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (F_001606020.Checked)
                    txt = F_001606020.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                //else if (M_001606031.Checked)
                //    txt = M_001606031.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");

                
                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001606032.Checked = false;
            }
        }
    }
    protected void M_001606033_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001606033.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }

            M_001606034.Checked = false;

            if (F_001606020.Checked)// || (F_001606021.Checked))
            {
                string txt = "";

                if (F_001606020.Checked)
                    txt = F_001606020.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                //else if (F_001606021.Checked)
                //    txt = F_001606021.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");


                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001606033.Checked = false;
            }
        }
    }
    protected void M_001606034_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001606034.Checked)
        {

            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }


            M_001606033.Checked = false;
            M_001606035.Checked = false;

            if ((F_001606020.Checked) || (F_001606021.Checked))
            {
                string txt = "";

                if (F_001606020.Checked)
                    txt = F_001606020.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (F_001606021.Checked)
                    txt = F_001606021.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");


                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001606034.Checked = false;
            }
        }
    }
    protected void M_001606035_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001606035.Checked)
        {

            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }

            M_001606036.Checked = false;
            M_001606037.Checked = false;

            if (F_001606021.Checked) //|| (F_001606021.Checked))
            {
                string txt = "";

                if (F_001606020.Checked)
                    txt = F_001606020.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                //else if (F_001606021.Checked)
                //    txt = F_001606021.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");


                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001606035.Checked = false;
            }
        } 
    }
    protected void M_001606036_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001606036.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }

            M_001606035.Checked = false;
            M_001606037.Checked = false;

            if (F_001606021.Checked) //|| (F_001606021.Checked))
            {
                string txt = "";

                if (F_001606020.Checked)
                    txt = F_001606020.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                //else if (F_001606021.Checked)
                //    txt = F_001606021.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");


                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001606036.Checked = false;
            }
        }
    }
    protected void M_001606037_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001606037.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }

            M_001606035.Checked = false;
            M_001606036.Checked = false;
            M_001606038.Checked = false;

            if (F_001606021.Checked) //|| (F_001606021.Checked))
            {
                string txt = "";

                if (F_001606020.Checked)
                    txt = F_001606020.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                //else if (F_001606021.Checked)
                //    txt = F_001606021.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");


                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001606037.Checked = false;
            }
        }   
    }
    protected void M_001606038_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001606038.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }

            M_001606037.Checked = false;
            M_001606039.Checked = false;
            M_001606040.Checked = false;

            if ((F_001606021.Checked) || (F_001606022.Checked))
            {
                //string txt = F_001606021.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");

                string txt = "";

                if (F_001606021.Checked)
                    txt = F_001606021.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (F_001606022.Checked)
                    txt = F_001606022.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");


                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001606038.Checked = false;
            }
        }
    }
    protected void M_001606039_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001606039.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
                        
            M_001606038.Checked = false;
            M_001606040.Checked = false;

            if ((F_001606021.Checked) || (F_001606022.Checked))
            {
                //string txt = F_001606021.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");

                string txt = "";

                if (F_001606021.Checked)
                    txt = F_001606021.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (F_001606022.Checked)
                    txt = F_001606022.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");


                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001606039.Checked = false;
            }
        }
    }
    protected void M_001606040_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001606040.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }

            M_001606038.Checked = false;
            M_001606039.Checked = false;
            M_001606041.Checked = false;

            if ((F_001606021.Checked) || (F_001606022.Checked))
            {
                //string txt = F_001606021.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");

                string txt = "";

                if (F_001606021.Checked)
                    txt = F_001606021.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (F_001606022.Checked)
                    txt = F_001606022.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");


                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001606040.Checked = false;
            }
        }
    }
    protected void M_001606041_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001606041.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }

            M_001606040.Checked = false;
            M_001606042.Checked = false;
            M_001606043.Checked = false;

            if ((F_001606022.Checked) || (F_001606023.Checked))
            {
                string txt = "";

                if (F_001606022.Checked)
                    txt = F_001606022.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (F_001606023.Checked)
                    txt = F_001606023.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");


                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001606041.Checked = false;
            }
        }
    }
    protected void M_001606042_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001606042.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }

            M_001606041.Checked = false;
            M_001606043.Checked = false;

            if ((F_001606022.Checked) || (F_001606023.Checked))
            {
                string txt = "";

                if (F_001606022.Checked)
                    txt = F_001606022.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (F_001606023.Checked)
                    txt = F_001606023.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");


                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001606042.Checked = false;
            }
        }
            
    }
    protected void M_001606043_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001606043.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }

            M_001606041.Checked = false;
            M_001606042.Checked = false;
            M_001606044.Checked = false;

            if ((F_001606022.Checked) || (F_001606023.Checked))
            {
                string txt = "";

                if (F_001606022.Checked)
                    txt = F_001606022.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (F_001606023.Checked)
                    txt = F_001606023.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");


                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001606043.Checked = false;
            }
        }
            
    }
    protected void M_001606044_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001606044.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }

            M_001606043.Checked = false;
            M_001606045.Checked = false;
            M_001606046.Checked = false;

            if (F_001606023.Checked)
            {
                string txt = "";

                if (F_001606023.Checked)
                    txt = F_001606023.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                

                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001606044.Checked = false;
            }
        }
    }
    protected void M_001606045_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001606045.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }

            M_001606044.Checked = false;
            M_001606046.Checked = false;

            if ((F_001606023.Checked) || (F_001606024.Checked))
            {
                string txt = "";

                if (F_001606023.Checked)
                    txt = F_001606023.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (F_001606024.Checked)
                    txt = F_001606024.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");


                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001606045.Checked = false;
            }
        }
    }
    protected void M_001606046_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001606046.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }

            M_001606044.Checked = false;
            M_001606045.Checked = false;

            if ((F_001606023.Checked) || (F_001606024.Checked))
            {
                string txt = "";

                if (F_001606023.Checked)
                    txt = F_001606023.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (F_001606024.Checked)
                    txt = F_001606024.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");


                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001606046.Checked = false;
            }
        }
    }
    protected void M_001606047_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001606047.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            if (F_001606024.Checked)
            {
                string txt = F_001606024.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                    txt.Substring(0, txt.IndexOf(": ")) + "..." +
                    "\\n\\nVocê de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001606047.Checked = false;
            }
        }
    }
    
    protected void M_001606050_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001606050.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }

            M_001606051.Checked = false;

            if (F_001606025.Checked)// || (F_001606028.Checked))
            {
                string txt = "";

                if (F_001606025.Checked)
                    txt = F_001606027.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                //else if (F_001606028.Checked)
                //    txt = F_001606028.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");

                

                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001606050.Checked = false;
            }
        }
    }
    protected void M_001606051_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001606051.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }

            M_001606050.Checked = false;
            
            if ((F_001606025.Checked) || (F_001606026.Checked))
            {
                string txt = "";

                if (F_001606025.Checked)
                    txt = F_001606025.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (F_001606026.Checked)
                    txt = F_001606026.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                                

                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001606051.Checked = false;
            }
        }
    }
    protected void M_001606052_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001606052.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            
            M_001606053.Checked = false;

            if (F_001606026.Checked)// || (F_001606028.Checked))
            {
                string txt = "";

                if (F_001606026.Checked)
                    txt = F_001606026.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                //else if (F_001606028.Checked)
                //    txt = F_001606028.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                    
                

                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001606052.Checked = false;
            }
        }
    }
    protected void M_001606053_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001606053.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }

            M_001606052.Checked = false;
            M_001606054.Checked = false;

            if (F_001606026.Checked)// || (F_001606028.Checked))
            {
                string txt = "";

                if (F_001606026.Checked)
                    txt = F_001606026.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                //else if (F_001606028.Checked)
                //    txt = F_001606028.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");



                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001606053.Checked = false;
            }
        }
            
    }
    protected void M_001606054_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001606054.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }

            M_001606053.Checked = false;
            M_001606055.Checked = false;
            //M_001606056.Checked = false;

            if ((F_001606026.Checked) || (F_001606027.Checked))
            {
                string txt = "";

                if (F_001606026.Checked)
                    txt = F_001606026.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (F_001606027.Checked)
                    txt = F_001606027.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");



                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001606054.Checked = false;
            }
        }
            
    }
    protected void M_001606055_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001606055.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }

            M_001606054.Checked = false;
            //M_001606056.Checked = false;

            if ((F_001606026.Checked) || (F_001606027.Checked))
            {
                string txt = "";

                if (F_001606026.Checked)
                    txt = F_001606026.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (F_001606027.Checked)
                    txt = F_001606027.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");



                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001606055.Checked = false;
            }
        }
    }
    protected void M_001606056_CheckedChanged(object sender, EventArgs e)
    {
        //if (M_001606056.Checked)
        //{
        //    if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
        //    {
        //        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
        //            "<script type='text/javascript'> " +
        //            "alert('Não há mais vagas para esta atividade!'); " +
        //            "</script>", false);
        //        (sender as CheckBox).Checked = false; return;
        //    }
            
        //    M_001606054.Checked = false;
        //    M_001606055.Checked = false;
        //    M_001606057.Checked = false;


        //    if (F_001606027.Checked)// || (F_001606030.Checked))
        //    {
        //        //string txt = F_001606021.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");

        //        string txt = "";

        //        if (F_001606027.Checked)
        //            txt = F_001606027.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
        //        //else if (F_001606030.Checked)
        //        //    txt = F_001606030.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");


        //        txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


        //        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
        //            "<script type='text/javascript'> " +
        //            "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
        //            txt +
        //            "Você de desmarcá-la antes.'); " +
        //            "</script>", false);

        //        //M_001606056.Checked = false;
        //    }
        //}
    }
    protected void M_001606057_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001606057.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            
            //M_001606056.Checked = false;
            M_001606058.Checked = false;
            M_001606059.Checked = false;

            if ((F_001606027.Checked) || (F_001606028.Checked))
            {
                //string txt = F_001606021.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");

                string txt = "";

                if (F_001606027.Checked)
                    txt = F_001606027.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (F_001606028.Checked)
                    txt = F_001606028.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");


                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001606057.Checked = false;
            }
        }
    }
    protected void M_001606058_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001606058.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }

            M_001606057.Checked = false;
            M_001606059.Checked = false;

            if ((F_001606027.Checked) || (F_001606028.Checked))
            {
                string txt = "";

                if (F_001606027.Checked)
                    txt = F_001606027.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (F_001606028.Checked)
                    txt = F_001606028.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");


                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001606058.Checked = false;
            }
        }
    }
    protected void M_001606059_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001606059.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }

            M_001606057.Checked = false;
            M_001606058.Checked = false;
            M_001606060.Checked = false;

            if ((F_001606027.Checked) || (F_001606028.Checked))
            {
                string txt = "";

                if (F_001606027.Checked)
                    txt = F_001606027.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (F_001606028.Checked)
                    txt = F_001606028.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");


                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001606059.Checked = false;
            }
        }
    }
    protected void M_001606060_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001606060.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }

            M_001606059.Checked = false;
            M_001606061.Checked = false;
            M_001606062.Checked = false;

            if ((F_001606028.Checked) || (F_001606029.Checked))
            {
                string txt = "";

                if (F_001606028.Checked)
                    txt = F_001606028.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (F_001606029.Checked)
                    txt = F_001606029.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");


                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001606060.Checked = false;
            }
        }
    }
    protected void M_001606061_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001606061.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }

            M_001606060.Checked = false;
            M_001606062.Checked = false;

            if ((F_001606028.Checked) || (F_001606029.Checked))
            {
                string txt = "";

                if (F_001606028.Checked)
                    txt = F_001606028.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (F_001606029.Checked)
                    txt = F_001606029.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");


                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001606060.Checked = false;
            }
        }
            
    }
    protected void M_001606062_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001606062.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }

            M_001606060.Checked = false;
            M_001606061.Checked = false;
            M_001606063.Checked = false;

            if ((F_001606028.Checked) || (F_001606029.Checked))
            {
                string txt = "";

                if (F_001606028.Checked)
                    txt = F_001606028.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (F_001606029.Checked)
                    txt = F_001606029.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");


                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001606062.Checked = false;
            }
        }
    }
    protected void M_001606063_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001606063.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }

            M_001606062.Checked = false;
            M_001606064.Checked = false;
            M_001606065.Checked = false;

            if (F_001606029.Checked) //|| (W_001606017.Checked))
            {
                string txt = "";

                if (F_001606029.Checked)
                    txt = F_001606029.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                //else if (W_001606017.Checked)
                //    txt = W_001606017.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");


                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001606063.Checked = false;
            }
        }
    }
    protected void M_001606064_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001606064.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }

            M_001606063.Checked = false;
            M_001606065.Checked = false;

            if ((F_001606029.Checked) || (F_001606030.Checked))
            {
                string txt = "";

                if (F_001606029.Checked)
                    txt = F_001606029.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (F_001606030.Checked)
                    txt = F_001606030.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");


                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001606064.Checked = false;
            }
        }
    }
    protected void M_001606065_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001606065.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }

            M_001606063.Checked = false;
            M_001606064.Checked = false;

            if (F_001606030.Checked)
            {
                string txt = "";

                if (F_001606029.Checked)
                    txt = F_001606029.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (F_001606030.Checked)
                    txt = F_001606030.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");


                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001606065.Checked = false;
            }
        }
    }
    protected void M_001606066_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001606066.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            if (F_001606030.Checked)
            {
                string txt = F_001606030.Text.Replace("<b>", "").Replace("</b>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");

                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item está em conflito com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001606066.Checked = false;
            }
        }
    }
    
    
    protected void btnGravar_Click(object sender, EventArgs e)
    {
        try
        {
            if (SessionParticipante.FlConfirmacaoInscricao)
            {
                string sqlCmdDel =
                    "DELETE FROM [tbMatriculas] " +
                    "      WHERE cdEvento = '" + SessionEvento.CdEvento + "' " +
                    "	  and cdParticipante = '" + SessionParticipante.CdParticipante + "' " +
                    "	  and cdAtividade >= 1606010";

                SqlCommand comandoDel = new SqlCommand(
                            sqlCmdDel, SessionCnn);

                comandoDel.ExecuteNonQuery();


                string sqlTxt =
                    "select a.cdAtividade, " +
                    "       a.noTitulo " +

                    "from tbAtividades a " +

                    "where a.cdEvento = '" + SessionEvento.CdEvento + "' " +

                   // "and a.flAtivo = 1 " +

                    "AND a.cdAtividade >= 1606010 ";

                SqlCommand comando = new SqlCommand(
                            sqlTxt, SessionCnn);

                SqlDataAdapter Dap;
                DataTable DTAtividades = new DataTable();

                Dap = new SqlDataAdapter(comando);

                Dap.TableMappings.Add("Atividades", "tbAtividades");
                Dap.Fill(DTAtividades);

                Inscricoes oInscricoes = new Inscricoes();

                if ((DTAtividades != null) && (DTAtividades.DefaultView.Count > 0))
                {
                    for (int i = 0; i < DTAtividades.DefaultView.Count; i++)
                    {
                        //if (((int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) >= 1606010) && (int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) <= 1606017)) ||
                        //    ((int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) == 1606072) || (int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) == 1606073)))
                        //{
                        //    Control chk = FindControlRecursive(this.Page, "W_" + DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim());
                        //    if ((chk != null) && ((chk as CheckBox).Checked))
                        //    {
                        //        oInscricoes.MatriculasGravar(SessionEvento.CdEvento, SessionParticipante.CdParticipante, DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim(), 0, 1, "000000002", SessionCnn);
                        //    }
                        //}
                        if ((int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) >= 1606019) && (int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) <= 1606030))
                        {
                            Control chk = FindControlRecursive(this.Page, "F_" + DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim());
                            if ((chk != null) && ((chk as CheckBox).Checked))
                            {
                                oInscricoes.MatriculasGravar(SessionEvento.CdEvento, SessionParticipante.CdParticipante, DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim(), 0, 1, "000000002", SessionCnn);
                            }
                        }
                        if ((int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) >= 1606031) && (int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) <= 1606066))
                        {
                            Control chk = FindControlRecursive(this.Page, "M_" + DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim());
                            if ((chk != null) && ((chk as CheckBox).Checked))
                            {
                                oInscricoes.MatriculasGravar(SessionEvento.CdEvento, SessionParticipante.CdParticipante, DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim(), 0, 1, "000000002", SessionCnn);
                            }
                        }
                        //if ((int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) >= 1604068) && (int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) <= 1604071))
                        //{
                        //    Control chk = FindControlRecursive(this.Page, "V_" + DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim());
                        //    if ((chk != null) && ((chk as CheckBox).Checked))
                        //    {
                        //        oInscricoes.MatriculasGravar(SessionEvento.CdEvento, SessionParticipante.CdParticipante, DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim(), 0, 1, "000000002", SessionCnn);
                        //    }
                        //}
                    }

                    txtMsg.Text = "Operação realizada com sucesso!";
                }
            }
            else
            {

                string sqlCmdDel =
                    "DELETE FROM [tbPedidosAtividades] " +
                    "      WHERE cdPedido = '" + SessionPedido.CdPedido + "' " +
                    "	  and cdAtividade >= 1606010";

                SqlCommand comandoDel = new SqlCommand(
                            sqlCmdDel, SessionCnn);

                comandoDel.ExecuteNonQuery();


                string sqlTxt =
                    "select a.cdAtividade, " +
                    "       a.noTitulo " +

                    "from tbAtividades a " +

                    "where a.cdEvento = '" + SessionEvento.CdEvento + "' " +

                   // "and a.flAtivo = 1 " +

                    "AND a.cdAtividade >= 1606010 ";

                SqlCommand comando = new SqlCommand(
                            sqlTxt, SessionCnn);

                SqlDataAdapter Dap;
                DataTable DTAtividades = new DataTable();

                Dap = new SqlDataAdapter(comando);

                Dap.TableMappings.Add("Atividades", "tbAtividades");
                Dap.Fill(DTAtividades);

                Inscricoes oInscricoes = new Inscricoes();

                if ((DTAtividades != null) && (DTAtividades.DefaultView.Count > 0))
                {
                    for (int i = 0; i < DTAtividades.DefaultView.Count; i++)
                    {
                        //if (((int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) >= 1606010) && (int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) <= 1606017)) ||
                        //    ((int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) == 1606072) || (int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) == 1606073)))
                        //{
                        //    Control chk = FindControlRecursive(this.Page, "W_" + DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim());
                        //    if ((chk != null) && ((chk as CheckBox).Checked))
                        //    {
                        //        oInscricoes.MatriculasGravar(SessionEvento.CdEvento, SessionParticipante.CdParticipante, DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim(), 0, 1, "000000002", SessionCnn);
                        //    }
                        //}
                        if ((int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) >= 1606019) && (int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) <= 1606030))
                        {
                            Control chk = FindControlRecursive(this.Page, "F_" + DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim());
                            if ((chk != null) && ((chk as CheckBox).Checked))
                            {
                                oPedidoCad.GravarAtividadePedido(SessionPedido.CdPedido, DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim(), 0, 0, 1, SessionCnn);
                            }
                        }
                        if ((int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) >= 1606031) && (int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) <= 1606066))
                        {
                            Control chk = FindControlRecursive(this.Page, "M_" + DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim());
                            if ((chk != null) && ((chk as CheckBox).Checked))
                            {
                                oPedidoCad.GravarAtividadePedido(SessionPedido.CdPedido, DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim(), 0, 0, 1, SessionCnn);
                            }
                        }
                        //if ((int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) >= 1604068) && (int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) <= 1604071))
                        //{
                        //    Control chk = FindControlRecursive(this.Page, "V_" + DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim());
                        //    if ((chk != null) && ((chk as CheckBox).Checked))
                        //    {
                        //        oInscricoes.MatriculasGravar(SessionEvento.CdEvento, SessionParticipante.CdParticipante, DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim(), 0, 1, "000000002", SessionCnn);
                        //    }
                        //}
                    }

                    txtMsg.Text = "Operação realizada com sucesso!";

                    Response.Redirect("frm_formapagamento.aspx?cdMatricula=" + SessionParticipante.CdParticipante, false);
                    //Server.Transfer(string.Format("frm_formapagamento.aspx?cdMatricula={0}",
                    //                                SessionParticipante.CdParticipante), true);
                    return;
                }

            }

        }
        catch
        {
            txtMsg.Text = "Erro no precessamento!";
        }
    }


}
