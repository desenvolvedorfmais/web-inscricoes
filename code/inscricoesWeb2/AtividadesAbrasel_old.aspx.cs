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

    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    SqlConnection SessionCnn;
    
    String SessionIdioma;
    
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

            if (SessionEvento == null)
                Server.Transfer("frmSessaoExpirada.aspx", true);

        }

        CarregarAtividadesGrade();
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
            "AND m.cdAtividade <= 001604010 ";

        SqlCommand comando = new SqlCommand(
                    sqlTxt, SessionCnn);

        SqlDataAdapter Dap;
        DataTable DTAtividades = new DataTable();

        Dap = new SqlDataAdapter(comando);

        Dap.TableMappings.Add("Atividades", "tbAtividades");
        Dap.Fill(DTAtividades);



        workshop.Visible = false;
        Mesa.Visible = false;
        Visitas.Visible = false;

        if ((DTAtividades != null) && (DTAtividades.DefaultView.Count > 0))
        {
            for (int i = 0; i < DTAtividades.DefaultView.Count; i++)
            {
                if (DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim() == "001604009")
                {
                    workshop.Visible = true;
                    chkTodosDiasWS.Checked = true;
                    prpchkDia13WS();
                    TabContainer1.ActiveTabIndex = 0;

                    Mesa.Visible = true;
                    chkTodosDiasM.Checked = true;
                    prpchkDia13M();
                }

                if (DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim() == "001604008")
                {
                    workshop.Visible = true;
                    chkDia13WS.Visible = true;
                    chkDia13WS.Checked = true;
                    chkDia14WS.Visible = true;
                    prpchkDia13WS();
                    TabContainer1.ActiveTabIndex = 0;

                    Mesa.Visible = true;
                    chkDia13M.Checked = true;
                    chkDia13M.Visible = true;
                    chkDia14M.Visible = true;
                    prpchkDia13M();
                }

                if (DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim() == "001604007")
                {
                    workshop.Visible = true;
                    chkTodosDiasWS.Checked = true;
                    prpchkDia13WS();
                    TabContainer1.ActiveTabIndex = 0;
                }

                if (DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim() == "001604006")
                {
                    workshop.Visible = true;
                    chkDia13WS.Visible = true;
                    chkDia13WS.Checked = true;
                    chkDia14WS.Visible = true;
                    prpchkDia13WS();
                    TabContainer1.ActiveTabIndex = 0;
                }

                if (DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim() == "001604003")
                {
                    Mesa.Visible = true;
                    chkTodosDiasM.Checked = true;
                    prpchkDia13M();
                    TabContainer1.ActiveTabIndex = 1;
                }

                if (DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim() == "001604002")
                {
                    Mesa.Visible = true;
                    chkDia13M.Checked = true;
                    chkDia13M.Visible = true;
                    chkDia14M.Visible = true;
                    prpchkDia13M();
                    TabContainer1.ActiveTabIndex = 1;
                }



                if (DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim() == "001604010")
                {
                    Visitas.Visible = true;
                    if (workshop.Visible)
                        TabContainer1.ActiveTabIndex = 0;
                    else if (Mesa.Visible)
                        TabContainer1.ActiveTabIndex = 1;
                    else
                        TabContainer1.ActiveTabIndex = 2;
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

            "where a.cdEvento = '" + SessionEvento.CdEvento +"' " +

            "and a.flAtivo = 1 " +

            "AND a.cdAtividade >= 001604011 " +

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
            for (int i = 0; i < DTAtividades.DefaultView.Count; i++)
            {
                if (((int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) >= 1604011) && (int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) <= 1604017)) ||
                    ((int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) == 1604072) || (int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) == 1604073)))
                {
                    Control chk = FindControlRecursive(this.Page, "W_" + DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim());
                    if (chk != null)
                    {
                        if (int.Parse(DTAtividades.DefaultView[i]["vagas_em_aberto"].ToString().Trim()) > 0)
                            (chk as CheckBox).Text = DTAtividades.DefaultView[i]["noTitulo"].ToString().Trim().Replace("#", "");
                        else
                            (chk as CheckBox).Text = DTAtividades.DefaultView[i]["noTitulo"].ToString().Trim().Replace("#", "<font color='red'>  ( Vagas Esgotadas )</font>");

                    }
                }
                if ((int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) >= 1604019) && (int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) <= 1604030))
                {
                    Control chk = FindControlRecursive(this.Page, "F_" + DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim());
                    if (chk != null)
                    {
                        if (int.Parse(DTAtividades.DefaultView[i]["vagas_em_aberto"].ToString().Trim()) > 0)
                            (chk as CheckBox).Text = DTAtividades.DefaultView[i]["noTitulo"].ToString().Trim().Replace("#", "");
                        else
                            (chk as CheckBox).Text = DTAtividades.DefaultView[i]["noTitulo"].ToString().Trim().Replace("#", "<font color='red'>  ( Vagas Esgotadas )</font>");
                    }
                }
                if ((int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) >= 1604031) && (int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) <= 1604067))
                {
                    Control chk = FindControlRecursive(this.Page, "M_" + DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim());
                    if (chk != null)
                    {
                        if (int.Parse(DTAtividades.DefaultView[i]["vagas_em_aberto"].ToString().Trim()) > 0)
                            (chk as CheckBox).Text = DTAtividades.DefaultView[i]["noTitulo"].ToString().Trim().Replace("#", "");
                        else
                            (chk as CheckBox).Text = DTAtividades.DefaultView[i]["noTitulo"].ToString().Trim().Replace("#", "<font color='red'>  ( Vagas Esgotadas )</font>");
                    }
                }
                if ((int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) >= 1604068) && (int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) <= 1604071))
                {
                    Control chk = FindControlRecursive(this.Page, "V_" + DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim());
                    if (chk != null)
                    {
                        if (int.Parse(DTAtividades.DefaultView[i]["vagas_em_aberto"].ToString().Trim()) > 0)
                            (chk as CheckBox).Text = DTAtividades.DefaultView[i]["noTitulo"].ToString().Trim().Replace("#", "");
                        else
                            (chk as CheckBox).Text = DTAtividades.DefaultView[i]["noTitulo"].ToString().Trim().Replace("#", "<font color='red'>  ( Vagas Esgotadas )</font>");
                    }
                }
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
            "AND a.cdAtividade >= 001604011 ";

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
                if ((int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) >= 1604011) && (int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) <= 1604017))
                {
                    Control chk = FindControlRecursive(this.Page, "W_" + DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim());
                    if (chk != null)
                    {
                        (chk as CheckBox).Checked = true;
                    }
                }
                if ((int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) >= 1604019) && (int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) <= 1604030))
                {
                    Control chk = FindControlRecursive(this.Page, "F_" + DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim());
                    if (chk != null)
                    {
                        (chk as CheckBox).Checked = true;
                    }
                }
                if ((int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) >= 1604031) && (int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) <= 1604067))
                {
                    Control chk = FindControlRecursive(this.Page, "M_" + DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim());
                    if (chk != null)
                    {
                        (chk as CheckBox).Checked = true;
                    }
                }
                if ((int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) >= 1604068) && (int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) <= 1604071))
                {
                    Control chk = FindControlRecursive(this.Page, "V_" + DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim());
                    if (chk != null)
                    {
                        (chk as CheckBox).Checked = true;
                    }
                }
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

            "and a.flAtivo = 1 " +

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
            return 0;

    }


    protected void chkDia13WS_CheckedChanged(object sender, EventArgs e)
    {
        prpchkDia13WS();
    }
    protected void prpchkDia13WS()
    {
        if (chkDia13WS.Checked)
        {
            d13_WS.Visible = true;
            
            d14_WS.Visible = false;
            W_001604014.Checked = false;
            W_001604015.Checked = false;
            W_001604016.Checked = false;
            W_001604017.Checked = false;  
            
            F_001604025.Checked = false;
            F_001604026.Checked = false;
            F_001604027.Checked = false;
            F_001604028.Checked = false;
            F_001604029.Checked = false;
            F_001604030.Checked = false;
        }
        else if (chkDia14WS.Checked)
        {
            d13_WS.Visible = false;
            W_001604011.Checked = false;
            W_001604012.Checked = false;
            W_001604013.Checked = false;

            F_001604019.Checked = false;
            F_001604020.Checked = false;
            F_001604021.Checked = false;
            F_001604022.Checked = false;
            F_001604023.Checked = false;
            F_001604024.Checked = false;

            d14_WS.Visible = true;            
        }
        else if (chkTodosDiasWS.Checked)
        {
            d13_WS.Visible = true;
            

            d14_WS.Visible = true;
            
        }
    }

    protected void F_001604019_CheckedChanged(object sender, EventArgs e)
    {
        if (F_001604019.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            if (M_001604031.Checked)
            {
                string txt = M_001604031.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>","");
                txt = "\"" + txt.Substring(0, txt.IndexOf(") ")).Trim() + "...\\n\\n\"";

                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);
                F_001604019.Checked = false;
            }
        }
    }
    protected void F_001604020_CheckedChanged(object sender, EventArgs e)
    {
        if (F_001604020.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            
            W_001604011.Checked = false;

            if ((M_001604032.Checked) || (M_001604033.Checked))
            {
                string txt = "";

                if (M_001604032.Checked) 
                    txt = M_001604032.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>","");
                else if (M_001604033.Checked)
                    txt = M_001604033.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");

                txt = "\"" + txt.Substring(0, txt.IndexOf(") ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                F_001604020.Checked = false;
            }
        }
    }
    protected void W_001604011_CheckedChanged(object sender, EventArgs e)
    {
        if (W_001604011.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            
            F_001604020.Checked = false;

            if ((M_001604032.Checked) || (M_001604033.Checked))
            {
                string txt = "";

                if (M_001604032.Checked)
                    txt = M_001604032.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001604033.Checked)
                    txt = M_001604033.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");

                txt = "\"" + txt.Substring(0, txt.IndexOf(") ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                W_001604011.Checked = false;
            }
        }
    }
    
    protected void F_001604021_CheckedChanged(object sender, EventArgs e)
    {
        if (F_001604021.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }

            W_001604072.Checked = false;

            if ((M_001604038.Checked) || (M_001604039.Checked) || (M_001604040.Checked))
            {
                string txt = "";

                if (M_001604038.Checked)
                    txt = M_001604038.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001604039.Checked)
                    txt = M_001604039.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001604040.Checked)
                    txt = M_001604040.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");

                txt = "\"" + txt.Substring(0, txt.IndexOf(") ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                F_001604021.Checked = false;
            }
        }
    }
    protected void W_001604072_CheckedChanged(object sender, EventArgs e)
    {
        if (W_001604072.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }

            F_001604021.Checked = false;

            if ((M_001604038.Checked) || (M_001604039.Checked) || (M_001604040.Checked))
            {
                string txt = "";

                if (M_001604038.Checked)
                    txt = M_001604038.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001604039.Checked)
                    txt = M_001604039.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001604040.Checked)
                    txt = M_001604040.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");

                txt = "\"" + txt.Substring(0, txt.IndexOf(") ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                W_001604072.Checked = false;
            }
        }
    }

    protected void F_001604022_CheckedChanged(object sender, EventArgs e)
    {
        if (F_001604022.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            
            W_001604012.Checked = false;

            if (M_001604041.Checked)
            {
                string txt = M_001604041.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                txt = "\"" + txt.Substring(0, txt.IndexOf(") ")).Trim() + "...\\n\\n\"";

                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);
                F_001604022.Checked = false;
            }
        }
    }
    protected void W_001604012_CheckedChanged(object sender, EventArgs e)
    {
        if (W_001604012.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            
            F_001604022.Checked = false;

            if (M_001604041.Checked)
            {
                string txt = M_001604041.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                txt = "\"" + txt.Substring(0, txt.IndexOf(") ")).Trim() + "...\\n\\n\"";

                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);
                W_001604012.Checked = false;
            }
        }
    }

    protected void F_001604023_CheckedChanged(object sender, EventArgs e)
    {
        if (F_001604023.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            
            W_001604013.Checked = false;

            if ((M_001604044.Checked) || (M_001604045.Checked) || (M_001604046.Checked))
            {
                string txt = "";

                if (M_001604044.Checked)
                    txt = M_001604044.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001604045.Checked)
                    txt = M_001604045.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001604046.Checked)
                    txt = M_001604046.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>","");

                txt = "\"" + txt.Substring(0, txt.IndexOf(") ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                F_001604023.Checked = false;
            }
        }
    }
    protected void W_001604013_CheckedChanged(object sender, EventArgs e)
    {
        if (W_001604013.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            
            F_001604023.Checked = false;

            if ((M_001604044.Checked) || (M_001604045.Checked))
            {
                string txt = "";

                if (M_001604044.Checked)
                    txt = M_001604044.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001604045.Checked)
                    txt = M_001604045.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");

                txt = "\"" + txt.Substring(0, txt.IndexOf(") ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                W_001604013.Checked = false;
            }
        }
    }

    protected void F_001604024_CheckedChanged(object sender, EventArgs e)
    {
        if (F_001604024.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            
            if ((M_001604047.Checked) || (M_001604048.Checked))
            {
                string txt = "";

                if (M_001604047.Checked)
                    txt = M_001604047.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001604048.Checked)
                    txt = M_001604048.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");

                txt = "\"" + txt.Substring(0, txt.IndexOf(") ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                F_001604024.Checked = false;
            }
        }
    }

    protected void F_001604025_CheckedChanged(object sender, EventArgs e)
    {
        if (F_001604025.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            
            W_001604014.Checked = false;

            if ((M_001604049.Checked)
                || (V_001604068.Checked) || (V_001604069.Checked) || (V_001604070.Checked) || (V_001604071.Checked))
            {
                string txt = "";

                if (M_001604049.Checked)
                {
                    txt = M_001604049.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                    txt = "\"" + txt.Substring(0, txt.IndexOf(") ")).Trim() + "...\\n\\n\"";
                }

                else if (V_001604068.Checked)
                {
                    txt = V_001604068.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                    txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";
                }
                else if (V_001604069.Checked)
                {
                    txt = V_001604069.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                    txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";
                }
                else if (V_001604070.Checked)
                {
                    txt = V_001604070.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                    txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";
                }
                else if (V_001604071.Checked)
                {
                    txt = V_001604071.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                    txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";
                }
                

                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                F_001604025.Checked = false;
            }
        }
    }
    protected void W_001604014_CheckedChanged(object sender, EventArgs e)
    {
        if (W_001604014.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            
            F_001604025.Checked = false;

            if ((M_001604049.Checked)
                || (V_001604068.Checked) || (V_001604069.Checked) || (V_001604070.Checked) || (V_001604071.Checked))
            {
                string txt = "";

                if (M_001604049.Checked)
                {
                    txt = M_001604049.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                    txt = "\"" + txt.Substring(0, txt.IndexOf(") ")).Trim() + "...\\n\\n\"";
                }

                else if (V_001604068.Checked)
                {
                    txt = V_001604068.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                    txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";
                }
                else if (V_001604069.Checked)
                {
                    txt = V_001604069.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                    txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";
                }
                else if (V_001604070.Checked)
                {
                    txt = V_001604070.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                    txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";
                }
                else if (V_001604071.Checked)
                {
                    txt = V_001604071.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                    txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";
                }

                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                W_001604014.Checked = false;
            }
        }
    }

    protected void F_001604026_CheckedChanged(object sender, EventArgs e)
    {
        if (F_001604026.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            
            W_001604015.Checked = false;

            if ((M_001604050.Checked) || (M_001604051.Checked) || (M_001604052.Checked)
                || (V_001604068.Checked))
            {
                string txt = "";

                if (M_001604050.Checked)
                {
                    txt = M_001604050.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                    txt = "\"" + txt.Substring(0, txt.IndexOf(") ")).Trim() + "...\\n\\n\"";
                }
                else if (M_001604051.Checked)
                {
                    txt = M_001604051.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                    txt = "\"" + txt.Substring(0, txt.IndexOf(") ")).Trim() + "...\\n\\n\"";
                }
                else if (M_001604052.Checked)
                {
                    txt = M_001604052.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                    txt = "\"" + txt.Substring(0, txt.IndexOf(") ")).Trim() + "...\\n\\n\"";
                }

                else if (V_001604068.Checked)
                {
                    txt = V_001604068.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                    txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";
                }

                


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                F_001604026.Checked = false;
            }
        }
    }
    protected void W_001604015_CheckedChanged(object sender, EventArgs e)
    {
        if (W_001604015.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            
            F_001604026.Checked = false;

            if ((M_001604050.Checked) || (M_001604051.Checked) || (M_001604052.Checked)
                || (V_001604068.Checked))
            {
                string txt = "";

                if (M_001604050.Checked)
                {
                    txt = M_001604050.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                    txt = "\"" + txt.Substring(0, txt.IndexOf(") ")).Trim() + "...\\n\\n\"";
                }
                else if (M_001604051.Checked)
                {
                    txt = M_001604051.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                    txt = "\"" + txt.Substring(0, txt.IndexOf(") ")).Trim() + "...\\n\\n\"";
                }
                else if (M_001604052.Checked)
                {
                    txt = M_001604052.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                    txt = "\"" + txt.Substring(0, txt.IndexOf(") ")).Trim() + "...\\n\\n\"";
                }

                else if (V_001604068.Checked)
                {
                    txt = V_001604068.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                    txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";
                }


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                W_001604015.Checked = false;
            }
        }
    }

    protected void F_001604027_CheckedChanged(object sender, EventArgs e)
    {
        if (F_001604027.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }

            W_001604073.Checked = false;

            if ((M_001604056.Checked) || (M_001604057.Checked) || (M_001604058.Checked))
            {
                string txt = "";

                if (M_001604056.Checked)
                    txt = M_001604056.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001604057.Checked)
                    txt = M_001604057.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001604058.Checked)
                    txt = M_001604058.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");

                txt = "\"" + txt.Substring(0, txt.IndexOf(") ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                F_001604027.Checked = false;
            }
        }
    }
    protected void W_001604073_CheckedChanged(object sender, EventArgs e)
    {
        if (W_001604073.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }

            F_001604027.Checked = false;

            if ((M_001604056.Checked) || (M_001604057.Checked) || (M_001604058.Checked))
            {
                string txt = "";

                if (M_001604056.Checked)
                    txt = M_001604056.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001604057.Checked)
                    txt = M_001604057.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001604058.Checked)
                    txt = M_001604058.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");

                txt = "\"" + txt.Substring(0, txt.IndexOf(") ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                W_001604073.Checked = false;
            }
        }
    }

    protected void F_001604028_CheckedChanged(object sender, EventArgs e)
    {
        if (F_001604028.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            
            W_001604016.Checked = false;

            if ((M_001604059.Checked) || (M_001604060.Checked))
            {
                string txt = "";

                if (M_001604059.Checked)
                    txt = M_001604059.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001604060.Checked)
                    txt = M_001604060.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");

                txt = "\"" + txt.Substring(0, txt.IndexOf(") ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                F_001604028.Checked = false;
            }        
        }
    }
    protected void W_001604016_CheckedChanged(object sender, EventArgs e)
    {
        if (W_001604016.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            
            F_001604028.Checked = false;

            if ((M_001604059.Checked) || (M_001604060.Checked))
            {
                string txt = "";

                if (M_001604059.Checked)
                    txt = M_001604059.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001604060.Checked)
                    txt = M_001604060.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");

                txt = "\"" + txt.Substring(0, txt.IndexOf(") ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                W_001604016.Checked = false;
            }    
        }
    }    

    protected void F_001604029_CheckedChanged(object sender, EventArgs e)
    {
        if (F_001604029.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            
            W_001604017.Checked = false;

            if ((M_001604062.Checked) || (M_001604063.Checked) || (M_001604064.Checked))
            {
                string txt = "";

                if (M_001604062.Checked)
                    txt = M_001604062.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001604063.Checked)
                    txt = M_001604063.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001604064.Checked)
                    txt = M_001604064.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");

                txt = "\"" + txt.Substring(0, txt.IndexOf(") ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                F_001604029.Checked = false;
            }
        }
    }
    protected void W_001604017_CheckedChanged(object sender, EventArgs e)
    {
        if (W_001604017.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            
            F_001604029.Checked = false;

            if ((M_001604062.Checked) || (M_001604063.Checked) || (M_001604064.Checked))
            {
                string txt = "";

                if (M_001604062.Checked)
                    txt = M_001604062.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001604063.Checked)
                    txt = M_001604063.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001604064.Checked)
                    txt = M_001604064.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");

                txt = "\"" + txt.Substring(0, txt.IndexOf(") ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                W_001604017.Checked = false;
            }
        }
    }

    protected void F_001604030_CheckedChanged(object sender, EventArgs e)
    {
        if (F_001604030.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            
            if ((M_001604065.Checked) || (M_001604066.Checked) || (M_001604067.Checked))
            {
                string txt = "";

                if (M_001604065.Checked)
                    txt = M_001604065.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001604066.Checked)
                    txt = M_001604066.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (M_001604067.Checked)
                    txt = M_001604067.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");

                txt = "\"" + txt.Substring(0, txt.IndexOf(") ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                F_001604030.Checked = false;
            }
        }
    }

    protected void chkDia13M_CheckedChanged(object sender, EventArgs e)
    {
        prpchkDia13M();
    }
    protected void prpchkDia13M()
    {
        if (chkDia13M.Checked)
        {
            d13_M.Visible = true;
            d14_M.Visible = false;

            M_001604049.Checked = false;
            M_001604050.Checked = false;
            M_001604051.Checked = false;
            M_001604052.Checked = false;
            M_001604053.Checked = false;
            M_001604054.Checked = false;
            M_001604055.Checked = false;
            M_001604056.Checked = false;
            M_001604057.Checked = false;
            M_001604058.Checked = false;
            M_001604059.Checked = false;
            M_001604060.Checked = false;
            M_001604061.Checked = false;
            M_001604062.Checked = false;
            M_001604063.Checked = false;
            M_001604064.Checked = false;
            M_001604065.Checked = false;
            M_001604066.Checked = false;
            M_001604067.Checked = false;
        }
        else if (chkDia14M.Checked)
        {
            d13_M.Visible = false;
            d14_M.Visible = true;

            M_001604031.Checked = false;
            M_001604032.Checked = false;
            M_001604033.Checked = false;
            M_001604034.Checked = false;
            M_001604035.Checked = false;
            M_001604036.Checked = false;
            M_001604037.Checked = false;
            M_001604038.Checked = false;
            M_001604039.Checked = false;
            M_001604040.Checked = false;
            M_001604041.Checked = false;
            M_001604042.Checked = false;
            M_001604043.Checked = false;
            M_001604044.Checked = false;
            M_001604045.Checked = false;
            M_001604046.Checked = false;
            M_001604047.Checked = false;
            M_001604048.Checked = false;
            
        }
        else if (chkTodosDiasM.Checked)
        {
            d13_M.Visible = true;


            d14_M.Visible = true;

        }
    }
    protected void M_001604031_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001604031.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            if (F_001604019.Checked)
            {
                string txt = F_001604019.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", 
                    "<script type='text/javascript'> "+
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n"+
                    txt.Substring(0,txt.IndexOf(": ")) +"..."+
                    "\\n\\nVocê de desmarcá-la antes.'); " +
                    "</script>", false);
                M_001604031.Checked = false;
            }
        }
    }
    protected void M_001604032_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001604032.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            if ((F_001604020.Checked) || (W_001604011.Checked))
            {
                string txt = "";

                if (F_001604020.Checked)
                    txt = F_001604020.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (W_001604011.Checked)
                    txt = W_001604011.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");

                
                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001604032.Checked = false;
            }
        }
    }
    protected void M_001604033_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001604033.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            if ((F_001604020.Checked) || (W_001604011.Checked))
            {
                string txt = "";

                if (F_001604020.Checked)
                    txt = F_001604020.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (W_001604011.Checked)
                    txt = W_001604011.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");


                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001604033.Checked = false;
            }
        }
    }
    protected void M_001604034_CheckedChanged(object sender, EventArgs e)
    {
        if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                "<script type='text/javascript'> " +
                "alert('Não há mais vagas para esta atividade!'); " +
                "</script>", false);
            (sender as CheckBox).Checked = false; return;
        }
            
    }
    protected void M_001604035_CheckedChanged(object sender, EventArgs e)
    {
        if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                "<script type='text/javascript'> " +
                "alert('Não há mais vagas para esta atividade!'); " +
                "</script>", false);
            (sender as CheckBox).Checked = false; return;
        }
            
    }
    protected void M_001604036_CheckedChanged(object sender, EventArgs e)
    {
        if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                "<script type='text/javascript'> " +
                "alert('Não há mais vagas para esta atividade!'); " +
                "</script>", false);
            (sender as CheckBox).Checked = false; return;
        }
            
    }
    protected void M_001604037_CheckedChanged(object sender, EventArgs e)
    {
        if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                "<script type='text/javascript'> " +
                "alert('Não há mais vagas para esta atividade!'); " +
                "</script>", false);
            (sender as CheckBox).Checked = false; return;
        }
            
    }
    protected void M_001604038_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001604038.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            if ((F_001604021.Checked) || (W_001604072.Checked))
            {
                //string txt = F_001604021.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");

                string txt = "";

                if (F_001604021.Checked)
                    txt = F_001604021.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (W_001604072.Checked)
                    txt = W_001604072.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");


                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001604038.Checked = false;
            }
        }
    }
    protected void M_001604039_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001604039.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            if ((F_001604021.Checked) || (W_001604072.Checked))
            {
                //string txt = F_001604021.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");

                string txt = "";

                if (F_001604021.Checked)
                    txt = F_001604021.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (W_001604072.Checked)
                    txt = W_001604072.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");


                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001604039.Checked = false;
            }
        }
    }
    protected void M_001604040_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001604040.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            if ((F_001604021.Checked) || (W_001604072.Checked))
            {
                //string txt = F_001604021.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");

                string txt = "";

                if (F_001604021.Checked)
                    txt = F_001604021.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (W_001604072.Checked)
                    txt = W_001604072.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");


                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001604040.Checked = false;
            }
        }
    }
    protected void M_001604041_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001604041.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            if ((F_001604022.Checked) || (W_001604012.Checked))
            {
                string txt = "";

                if (F_001604022.Checked)
                    txt = F_001604022.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (W_001604012.Checked)
                    txt = W_001604012.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");


                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001604041.Checked = false;
            }
        }
    }
    protected void M_001604042_CheckedChanged(object sender, EventArgs e)
    {
        if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                "<script type='text/javascript'> " +
                "alert('Não há mais vagas para esta atividade!'); " +
                "</script>", false);
            (sender as CheckBox).Checked = false; return;
        }
            
    }
    protected void M_001604043_CheckedChanged(object sender, EventArgs e)
    {
        if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                "<script type='text/javascript'> " +
                "alert('Não há mais vagas para esta atividade!'); " +
                "</script>", false);
            (sender as CheckBox).Checked = false; return;
        }
            
    }
    protected void M_001604044_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001604044.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            if ((F_001604023.Checked) || (W_001604013.Checked))
            {
                string txt = "";

                if (F_001604023.Checked)
                    txt = F_001604023.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (W_001604013.Checked)
                    txt = W_001604013.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");


                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001604044.Checked = false;
            }
        }
    }
    protected void M_001604045_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001604045.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            if ((F_001604023.Checked) || (W_001604013.Checked))
            {
                string txt = "";

                if (F_001604023.Checked)
                    txt = F_001604023.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (W_001604013.Checked)
                    txt = W_001604013.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");


                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001604045.Checked = false;
            }
        }
    }
    protected void M_001604046_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001604046.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            if (F_001604023.Checked)
            {
                string txt = F_001604023.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";

                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001604046.Checked = false;
            }
        }
    }
    protected void M_001604047_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001604047.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            if (F_001604024.Checked)
            {
                string txt = F_001604024.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt.Substring(0, txt.IndexOf(": ")) + "..." +
                    "\\n\\nVocê de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001604047.Checked = false;
            }
        }
    }
    protected void M_001604048_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001604048.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            if (F_001604024.Checked)
            {
                string txt = F_001604024.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt.Substring(0, txt.IndexOf(": ")) + "..." +
                    "\\n\\nVocê de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001604048.Checked = false;
            }
        }
    }
    protected void M_001604049_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001604049.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            if ((F_001604025.Checked) || (W_001604014.Checked)
                || (V_001604068.Checked))// || (V_001604069.Checked) || (V_001604070.Checked) || (V_001604071.Checked))
            {
                string txt = "";

                if (F_001604025.Checked)
                    txt = F_001604025.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (W_001604014.Checked)
                    txt = W_001604014.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");

                else if (V_001604068.Checked)
                    txt = V_001604068.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                //else if (V_001604069.Checked)
                //    txt = V_001604069.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                //else if (V_001604070.Checked)
                //    txt = V_001604070.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                //else if (V_001604071.Checked)
                //    txt = V_001604071.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");


                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001604049.Checked = false;
            }
        }
    }
    protected void M_001604050_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001604050.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            if ((F_001604026.Checked) || (W_001604015.Checked)
                || (V_001604068.Checked))
            {
                string txt = "";

                if (F_001604026.Checked)
                    txt = F_001604026.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (W_001604015.Checked)
                    txt = W_001604015.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");

                else if (V_001604068.Checked)
                    txt = V_001604068.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");


                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001604050.Checked = false;
            }
        }
    }
    protected void M_001604051_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001604051.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            if ((F_001604026.Checked) || (W_001604015.Checked)
                || (V_001604068.Checked))
            {
                string txt = "";

                if (F_001604026.Checked)
                    txt = F_001604026.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (W_001604015.Checked)
                    txt = W_001604015.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");

                else if (V_001604068.Checked)
                    txt = V_001604068.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");


                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001604051.Checked = false;
            }
        }
    }
    protected void M_001604052_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001604052.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            if ((F_001604026.Checked) || (W_001604015.Checked)
                || (V_001604068.Checked))
            {
                string txt = "";

                if (F_001604026.Checked)
                    txt = F_001604026.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (W_001604015.Checked)
                    txt = W_001604015.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                    
                else if (V_001604068.Checked)
                    txt = V_001604068.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");


                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001604052.Checked = false;
            }
        }
    }
    protected void M_001604053_CheckedChanged(object sender, EventArgs e)
    {
        if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                "<script type='text/javascript'> " +
                "alert('Não há mais vagas para esta atividade!'); " +
                "</script>", false);
            (sender as CheckBox).Checked = false; return;
        }
            
    }
    protected void M_001604054_CheckedChanged(object sender, EventArgs e)
    {
        if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                "<script type='text/javascript'> " +
                "alert('Não há mais vagas para esta atividade!'); " +
                "</script>", false);
            (sender as CheckBox).Checked = false; return;
        }
            
    }
    protected void M_001604055_CheckedChanged(object sender, EventArgs e)
    {
        if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                "<script type='text/javascript'> " +
                "alert('Não há mais vagas para esta atividade!'); " +
                "</script>", false);
            (sender as CheckBox).Checked = false; return;
        }
            
    }
    protected void M_001604056_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001604056.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            
            M_001604057.Checked = false;


            if ((F_001604027.Checked) || (W_001604073.Checked))
            {
                //string txt = F_001604021.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");

                string txt = "";

                if (F_001604027.Checked)
                    txt = F_001604027.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (W_001604073.Checked)
                    txt = W_001604073.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");


                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001604056.Checked = false;
            }
        }
    }
    protected void M_001604057_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001604057.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            
            M_001604056.Checked = false;

            if ((F_001604027.Checked) || (W_001604073.Checked))
            {
                //string txt = F_001604021.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");

                string txt = "";

                if (F_001604027.Checked)
                    txt = F_001604027.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (W_001604073.Checked)
                    txt = W_001604073.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");


                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001604057.Checked = false;
            }
        }
    }
    protected void M_001604058_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001604058.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }

            if ((F_001604027.Checked) || (W_001604073.Checked))
            {
                //string txt = F_001604021.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");

                string txt = "";

                if (F_001604027.Checked)
                    txt = F_001604027.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (W_001604073.Checked)
                    txt = W_001604073.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");


                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001604058.Checked = false;
            }
        }
    }
    protected void M_001604059_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001604059.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            if ((F_001604028.Checked) || (W_001604016.Checked))
            {
                string txt = "";

                if (F_001604028.Checked)
                    txt = F_001604028.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (W_001604016.Checked)
                    txt = W_001604016.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");


                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001604059.Checked = false;
            }
        }
    }
    protected void M_001604060_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001604060.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            if ((F_001604028.Checked) || (W_001604016.Checked))
            {
                string txt = "";

                if (F_001604028.Checked)
                    txt = F_001604028.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (W_001604016.Checked)
                    txt = W_001604016.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");


                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001604060.Checked = false;
            }
        }
    }
    protected void M_001604061_CheckedChanged(object sender, EventArgs e)
    {
        if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                "<script type='text/javascript'> " +
                "alert('Não há mais vagas para esta atividade!'); " +
                "</script>", false);
            (sender as CheckBox).Checked = false; return;
        }
            
    }
    protected void M_001604062_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001604062.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            if ((F_001604029.Checked) || (W_001604017.Checked))
            {
                string txt = "";

                if (F_001604029.Checked)
                    txt = F_001604029.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (W_001604017.Checked)
                    txt = W_001604017.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");


                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001604062.Checked = false;
            }
        }
    }
    protected void M_001604063_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001604063.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            if ((F_001604029.Checked) || (W_001604017.Checked))
            {
                string txt = "";

                if (F_001604029.Checked)
                    txt = F_001604029.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (W_001604017.Checked)
                    txt = W_001604017.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");


                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001604063.Checked = false;
            }
        }
    }
    protected void M_001604064_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001604064.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            if ((F_001604029.Checked) || (W_001604017.Checked))
            {
                string txt = "";

                if (F_001604029.Checked)
                    txt = F_001604029.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                else if (W_001604017.Checked)
                    txt = W_001604017.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");


                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001604064.Checked = false;
            }
        }
    }
    protected void M_001604065_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001604065.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            if (F_001604030.Checked)
            {
                string txt = F_001604030.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");

                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001604065.Checked = false;
            }
        }
    }
    protected void M_001604066_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001604066.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            if (F_001604030.Checked)
            {
                string txt = F_001604030.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");

                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001604066.Checked = false;
            }
        }
    }
    protected void M_001604067_CheckedChanged(object sender, EventArgs e)
    {
        if (M_001604067.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }
            if (F_001604030.Checked)
            {
                string txt = F_001604030.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");

                txt = "\"" + txt.Substring(0, txt.IndexOf(": ")).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                M_001604067.Checked = false;
            }
        }
    }
    
    
    protected void btnGravar_Click(object sender, EventArgs e)
    {
        try
        {
            string sqlCmdDel =
                "DELETE FROM [tbMatriculas] " +
                "      WHERE cdEvento = '" + SessionEvento.CdEvento + "' " +
                "	  and cdParticipante = '" + SessionParticipante.CdParticipante + "' " +
                "	  and cdAtividade >= 1604011";

            SqlCommand comandoDel = new SqlCommand(
                        sqlCmdDel, SessionCnn);

            comandoDel.ExecuteNonQuery();


            string sqlTxt =
                "select a.cdAtividade, " +
                "       a.noTitulo " +

                "from tbAtividades a " +

                "where a.cdEvento = '" + SessionEvento.CdEvento + "' " +

                "and a.flAtivo = 1 " +

                "AND a.cdAtividade >= 1604011 ";

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
                    if (((int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) >= 1604011) && (int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) <= 1604017)) ||
                        ((int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) == 1604072) || (int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) == 1604073)))
                    {
                        Control chk = FindControlRecursive(this.Page, "W_" + DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim());
                        if ((chk != null) && ((chk as CheckBox).Checked))
                        {
                            oInscricoes.MatriculasGravar(SessionEvento.CdEvento, SessionParticipante.CdParticipante, DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim(), 0, 1, "000000002", SessionCnn);
                        }
                    }
                    if ((int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) >= 1604019) && (int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) <= 1604030))
                    {
                        Control chk = FindControlRecursive(this.Page, "F_" + DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim());
                        if ((chk != null) && ((chk as CheckBox).Checked))
                        {
                            oInscricoes.MatriculasGravar(SessionEvento.CdEvento, SessionParticipante.CdParticipante, DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim(), 0, 1, "000000002", SessionCnn);
                        }
                    }
                    if ((int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) >= 1604031) && (int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) <= 1604067))
                    {
                        Control chk = FindControlRecursive(this.Page, "M_" + DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim());
                        if ((chk != null) && ((chk as CheckBox).Checked))
                        {
                            oInscricoes.MatriculasGravar(SessionEvento.CdEvento, SessionParticipante.CdParticipante, DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim(), 0, 1, "000000002", SessionCnn);
                        }
                    }
                    if ((int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) >= 1604068) && (int.Parse(DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim()) <= 1604071))
                    {
                        Control chk = FindControlRecursive(this.Page, "V_" + DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim());
                        if ((chk != null) && ((chk as CheckBox).Checked))
                        {
                            oInscricoes.MatriculasGravar(SessionEvento.CdEvento, SessionParticipante.CdParticipante, DTAtividades.DefaultView[i]["cdAtividade"].ToString().Trim(), 0, 1, "000000002", SessionCnn);
                        }
                    }
                }

                txtMsg.Text = "Operação realizada com sucesso!";
            }
        }
        catch
        {
            txtMsg.Text = "Erro no precessamento!";
        }
    }


    protected void V_001604068_CheckedChanged(object sender, EventArgs e)
    {
        if (V_001604068.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }

            V_001604069.Checked = false;
            V_001604070.Checked = false;
            V_001604071.Checked = false;

            if ((F_001604025.Checked) || (W_001604014.Checked) || (F_001604026.Checked) || (W_001604015.Checked) ||
                (M_001604049.Checked) || (M_001604050.Checked) || (M_001604051.Checked) || (M_001604052.Checked))
            {
                string txt = "";

                int pos = 0;
                if (F_001604025.Checked)
                {
                    txt = F_001604025.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                    pos = txt.IndexOf(": ");
                }
                else if (W_001604014.Checked)
                {
                    txt = W_001604014.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                    pos = txt.IndexOf(": ");
                }
                else if (F_001604026.Checked)
                {
                    txt = F_001604026.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                    pos = txt.IndexOf(": ");
                }
                else if (W_001604015.Checked)
                {
                    txt = W_001604015.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                    pos = txt.IndexOf(": ");
                }

                else if (M_001604049.Checked)
                {
                    txt = M_001604049.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                    pos = txt.IndexOf(") ");
                }
                else if (M_001604050.Checked)
                {
                    txt = M_001604050.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                    pos = txt.IndexOf(") ");
                }
                else if (M_001604051.Checked)
                {
                    txt = M_001604051.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                    pos = txt.IndexOf(") ");
                }
                else if (M_001604052.Checked)
                {
                    txt = M_001604052.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                    pos = txt.IndexOf(") ");
                }
                                                
                txt = "\"" + txt.Substring(0, pos).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                V_001604068.Checked = false;
            }
        }
    }
    protected void V_001604069_CheckedChanged(object sender, EventArgs e)
    {
        if (V_001604069.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }

            V_001604068.Checked = false;
            V_001604070.Checked = false;
            V_001604071.Checked = false;

            int pos = 0;
            if ((F_001604025.Checked) || (W_001604014.Checked) ||
                (M_001604049.Checked) )
            {
                string txt = "";

                if (F_001604025.Checked)
                {
                    txt = F_001604025.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                    pos = txt.IndexOf(": ");
                }
                else if (W_001604014.Checked)
                {
                    txt = W_001604014.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                    pos = txt.IndexOf(": ");
                }

                else if (M_001604049.Checked)
                {
                    txt = M_001604049.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                    pos = txt.IndexOf(") ");
                }


                txt = "\"" + txt.Substring(0, pos).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                V_001604069.Checked = false;
            }
        }
    }
    protected void V_001604070_CheckedChanged(object sender, EventArgs e)
    {
        if (V_001604070.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }

            V_001604068.Checked = false;
            V_001604069.Checked = false;
            V_001604071.Checked = false;

            int pos = 0;
            if ((F_001604025.Checked) || (W_001604014.Checked) ||
                (M_001604049.Checked))
            {
                string txt = "";

                if (F_001604025.Checked)
                {
                    txt = F_001604025.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                    pos = txt.IndexOf(": ");
                }
                else if (W_001604014.Checked)
                {
                    txt = W_001604014.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                    pos = txt.IndexOf(": ");
                }

                else if (M_001604049.Checked)
                {
                    txt = M_001604049.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                    pos = txt.IndexOf(") ");
                }


                txt = "\"" + txt.Substring(0, pos).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                V_001604070.Checked = false;
            }
        }
    }
    protected void V_001604071_CheckedChanged(object sender, EventArgs e)
    {
        if (V_001604071.Checked)
        {
            if (verificarVagas((sender as CheckBox).ID.Replace("F_", "").Replace("W_", "").Replace("M_", "").Replace("V_", "")) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Não há mais vagas para esta atividade!'); " +
                    "</script>", false);
                (sender as CheckBox).Checked = false; return;
            }

            V_001604068.Checked = false;
            V_001604069.Checked = false;
            V_001604070.Checked = false;

            int pos = 0;
            if ((F_001604025.Checked) || (W_001604014.Checked) ||
                (M_001604049.Checked))
            {
                string txt = "";

                if (F_001604025.Checked)
                {
                    txt = F_001604025.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                    pos = txt.IndexOf(": ");
                }
                else if (W_001604014.Checked)
                {
                    txt = W_001604014.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                    pos = txt.IndexOf(": ");
                }

                else if (M_001604049.Checked)
                {
                    txt = M_001604049.Text.Replace("<b>", "").Replace("&nbsp;", "").Replace("<br/>", "").Replace("<font color='red'>  ( Vagas Esgotadas )</font>", "");
                    pos = txt.IndexOf(") ");
                }


                txt = "\"" + txt.Substring(0, pos).Trim() + "...\\n\\n\"";


                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção",
                    "<script type='text/javascript'> " +
                    "alert('Este item conflita com outra atividade no mesmo horário!\\n\\n" +
                    txt +
                    "Você de desmarcá-la antes.'); " +
                    "</script>", false);

                V_001604071.Checked = false;
            }
        }
    }
}
