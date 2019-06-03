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

public partial class frmProgramacao : System.Web.UI.Page
{
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    SqlConnection SessionCnn;

    Atividade SessionAtividade;
    AtividadeCad oAtividadeCad = new AtividadeCad();

    Inscricoes SessionInscricoes = new Inscricoes();

    DataTable oDTAtividades = new DataTable();

    String SessionIdioma;

    //string sessionFiltro = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //local 1
            //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOWEzSnJjMkV0Y0dNN1NXNXBkR2xoYkNCRFlYUmhiRzluUFdSaVJYWmxiblJ2YzE5R1RUdFFaWEp6YVhOMElGTmxZM1Z5YVhSNUlFbHVabTg5VkhKMVpUdFZjMlZ5SUVsRVBYTmhPMUJoYzNOM2IzSmtQV3R5YTNOaE1UY3g=")));

            //local 2 note novo
            //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOUxseHpjV3d5TURFeVpYaHdjbVZ6Y3p0SmJtbDBhV0ZzSUVOaGRHRnNiMmM5WkdKRmRtVnVkRzl6WDBaTk8xQmxjbk5wYzNRZ1UyVmpkWEpwZEhrZ1NXNW1iejFVY25WbE8xVnpaWElnU1VROWMyRTdVR0Z6YzNkdmNtUTlTM0pyYzJFeE56RT0=")));

            //servidor
            //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOVptRjZaVzVrYjIxaGFYTTdTVzVwZEdsaGJDQkRZWFJoYkc5blBXUmlSWFpsYm5SdmMxOUdUVHRRWlhKemFYTjBJRk5sWTNWeWFYUjVJRWx1Wm04OVZISjFaVHRWYzJWeUlFbEVQWE5oTzFCaGMzTjNiM0prUFV0eWEzTmhNVGN4")));

            //MinSaude
            //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOUxseHpjV3hsZUhCeVpYTnpPMGx1YVhScFlXd2dRMkYwWVd4dlp6MWtZa1YyWlc1MGIzTmZSazA3VUdWeWMybHpkQ0JUWldOMWNtbDBlU0JKYm1adlBWUnlkV1U3VlhObGNpQkpSRDF6WVR0UVlYTnpkMjl5WkQxTGNtdHpZVEUzTVE9PQ==")));

            //Site
            //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOU1UZzVMak00TGprMUxqY3lPMGx1YVhScFlXd2dRMkYwWVd4dlp6MW1ZWHBsYm1SdmJXRnBjenRRWlhKemFYTjBJRk5sWTNWeWFYUjVJRWx1Wm04OVZISjFaVHRWYzJWeUlFbEVQV1poZW1WdVpHOXRZV2x6TzFCaGMzTjNiM0prUFZGMU5EVTBja1p0")));

            //Site2-historico
            //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOWJYTnpjV3d1Wm1GNlpXNWtiMjFoYVhNdVkyOXRPMGx1YVhScFlXd2dRMkYwWVd4dlp6MW1ZWHBsYm1SdmJXRnBjekU3VUdWeWMybHpkQ0JUWldOMWNtbDBlU0JKYm1adlBWUnlkV1U3VlhObGNpQkpSRDFtWVhwbGJtUnZiV0ZwY3pFN1VHRnpjM2R2Y21ROVVYVTBOVFJ5Um0wPQ==")));

            //Site-producao
            //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOWJYTnpjV3d1Wm1GNlpXNWtiMjFoYVhNdVkyOXRPMGx1YVhScFlXd2dRMkYwWVd4dlp6MW1ZWHBsYm1SdmJXRnBjenRRWlhKemFYTjBJRk5sWTNWeWFYUjVJRWx1Wm04OVZISjFaVHRWYzJWeUlFbEVQV1poZW1WdVpHOXRZV2x6TzFCaGMzTjNiM0prUFZGMU5EVTBja1p0VFVSQg==")));

            //Site-producao - AZURE
            SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOWFHcDBZWEF3YjNkcWJpNWtZWFJoWW1GelpTNTNhVzVrYjNkekxtNWxkRHRKYm1sMGFXRnNJRU5oZEdGc2IyYzlaR0pmWlhabGJuUnZjMFpOTzFCbGNuTnBjM1FnVTJWamRYSnBkSGtnU1c1bWJ6MVVjblZsTzFWelpYSWdTVVE5UTI5dVlYTmxiWE03VUdGemMzZHZjbVE5UXpCdU5EVmxiVFV5TURFMg==")));


            //Site-producao - IP
            //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOU1UZzVMak00TGprd0xqRXdPVHRKYm1sMGFXRnNJRU5oZEdGc2IyYzlabUY2Wlc1a2IyMWhhWE03VUdWeWMybHpkQ0JUWldOMWNtbDBlU0JKYm1adlBWUnlkV1U3VlhObGNpQkpSRDFtWVhwbGJtUnZiV0ZwY3p0UVlYTnpkMjl5WkQxUmRUUTFOSEpHYlUxRVFRPT0=")));



            //krksa-vaio
            //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOUxseHpjV3d5TURFeVpYaHdjbVZ6Y3p0SmJtbDBhV0ZzSUVOaGRHRnNiMmM5WkdKRmRtVnVkRzl6WDBaTk8xQmxjbk5wYzNRZ1UyVmpkWEpwZEhrZ1NXNW1iejFVY25WbE8xVnpaWElnU1VROWMyRTdVR0Z6YzNkdmNtUTlTM0pyYzJFeE56RT0=")));

            //Servidor Dell - Caonasems
            //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOU1UZzFMakU0TlM0eE5qZ3VNanRKYm1sMGFXRnNJRU5oZEdGc2IyYzlaR0pGZG1WdWRHOXpYMFpOVEc5allXdzdVR1Z5YzJsemRDQlRaV04xY21sMGVTQkpibVp2UFZSeWRXVTdWWE5sY2lCSlJEMXpZVHRRWVhOemQyOXlaRDFMY210ellURTNNUT09")));

            //Servidor core i7
            //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOU1UZzFMakU0TlM0eE5qZ3VNanRKYm1sMGFXRnNJRU5oZEdGc2IyYzlaR0pGZG1WdWRHOXpYMFpOVEc5allXdzdVR1Z5YzJsemRDQlRaV04xY21sMGVTQkpibVp2UFZSeWRXVTdWWE5sY2lCSlJEMXpZVHRRWVhOemQyOXlaRDFMY210ellURTNNUT09")));

            //servidor LG
            //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOU1UZzFMakU0TlM0eE5qZ3VNanRKYm1sMGFXRnNJRU5oZEdGc2IyYzlaR0pGZG1WdWRHOXpYMFpOVEc5allXdzdVR1Z5YzJsemRDQlRaV04xY21sMGVTQkpibVp2UFZSeWRXVTdWWE5sY2lCSlJEMXpZVHRRWVhOemQyOXlaRDFMY210ellURTNNUT09")));

            //Session["SessionCnn"] = SessionCnn;


            //conexao pesquisa local
            //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOU1UZzFMakU0TlM0eE5qZ3VNanRKYm1sMGFXRnNJRU5oZEdGc2IyYzlaR0pGZG1WdWRHOXpYMFpOVEc5allXdzdVR1Z5YzJsemRDQlRaV04xY21sMGVTQkpibVp2UFZSeWRXVTdWWE5sY2lCSlJEMXpZVHRRWVhOemQyOXlaRDFMY210ellURTNNUT09")));

            
            Session["SessionCnn"] = SessionCnn;

            Geral oGeral = new Geral();
            if (oGeral.verificarSiteManutencao("1", SessionCnn))
            {
                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                "05",
                                ""), true);
            }


            SessionIdioma = (String)Session["SessionIdioma"];
            if (SessionIdioma == null)
                SessionIdioma = "PTBR";
            Session["SessionIdioma"] = SessionIdioma;



            string cdEvento = "";
            if ((Request["codEvento"] != null) &&
                (Request["codEvento"].ToString().Trim().ToUpper() != ""))
            {
                cdEvento = cllEventos.Crypto.DecryptStringAES(Request["codEvento"]);
            }
            if ((cdEvento.Trim() == "") || (cdEvento.Trim().Length != 6))
            {
                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}",
                                "003"), true);
            }
            SessionEvento = oEventoCad.Pesquisar(cdEvento.Substring(0, 4), cdEvento, SessionCnn);
            Session["SessionEvento"] = SessionEvento;
            if (SessionEvento == null)
            {
                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                "003",
                                oEventoCad.RcMsg + cdEvento.Substring(0, 4)), true);
            }

            cdEvento = SessionEvento.CdEvento;


            if (SessionEvento.DsCon != "")
            {
                SqlConnection SessionCnn2 = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES(SessionEvento.DsCon)));
                Session["SessionCnn"] = SessionCnn2;

                SessionCnn = SessionCnn2;

                SessionEvento = oEventoCad.Pesquisar(cdEvento.Substring(0, 4), cdEvento, SessionCnn2);
                Session["SessionEvento"] = SessionEvento;
                if (SessionEvento == null)
                {
                    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                    "003",
                                    oEventoCad.RcMsg + cdEvento.Substring(0, 4)), true);
                }
            }

            

            if ((SessionEvento.DtFinalEvento == null) ||
                (SessionEvento.DtFinalEvento < Geral.datahoraServidor(SessionCnn)))// DateTime.Parse(DateTime.Today.ToString("dd/MM/yyyy"))))
            {
                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                "006",
                                oEventoCad.RcMsg + cdEvento.Substring(0, 4)), true);
            }
            
            //if (SessionEvento == null)
            //    SessionEvento = (Evento)Session["SessionEvento"];
            //else
            //    Session["SessionEvento"] = SessionEvento;

            if (SessionEvento == null)
            {
                //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                //                "003",
                //                oEventoCad.RcMsg), true);

                Response.Cookies["EventoInfo"]["eventoCod"] = cllEventos.Crypto.DecryptStringAES(Request["codEvento"]);
                //Response.Cookies["EventoInfo"]["lngCod"] = SessionIdioma;
                Response.Cookies["EventoInfo"].Expires = DateTime.Now.AddDays(1);

                //Server.Transfer("frmSessaoExpirada.aspx", true);
                Response.Redirect("frmProgramacao.aspx?codEvento=" + Request["codEvento"] + "&cdLng=PTBR", false);
                //Response.Redirect("../frmSessaoExpirada.aspx");
                return;
            }


            Response.Cookies["EventoInfo"]["eventoCod"] = cllEventos.Crypto.DecryptStringAES(Request["codEvento"]);
            Response.Cookies["EventoInfo"]["lngCod"] = SessionIdioma;
            Response.Cookies["EventoInfo"].Expires = DateTime.Now.AddDays(1);


            

            CarregarTipoDeAtividades();
            if ((string)Session["sessionFiltro"] != null)
                TxtFTipo.SelectedValue = (string)Session["sessionFiltro"];
            

            //FiltrarTemaDeCursos("");
            CarregarDtInicioDeAtividades("");


            oDTAtividades.Columns.Add("noTipoAtividade");
            oDTAtividades.Columns.Add("noTitulo");
            oDTAtividades.Columns.Add("dsTema");
            oDTAtividades.Columns.Add("noLocal");
            oDTAtividades.Columns.Add("dtInicio", System.Type.GetType("System.DateTime"));
            oDTAtividades.Columns.Add("dtTermino", System.Type.GetType("System.DateTime"));
            oDTAtividades.Columns.Add("cdAtividade");
            oDTAtividades.Columns.Add("noProfessor");
            oDTAtividades.Columns.Add("cdTipoAtividade");
            oDTAtividades.Columns.Add("vlAtividade", System.Type.GetType("System.Decimal"));
            oDTAtividades.Columns.Add("flAtivo", System.Type.GetType("System.Boolean"));
            oDTAtividades.Columns.Add("flInscricaoObrigatoria", System.Type.GetType("System.Boolean"));
            oDTAtividades.Columns.Add("flPodeChocarHorario", System.Type.GetType("System.Boolean"));
            oDTAtividades.Columns.Add("dsCaminhoImgWEB");
            oDTAtividades.Columns.Add("flRequerQuantidade", System.Type.GetType("System.Boolean"));
            oDTAtividades.Columns.Add("vlQuantidadeMaxima");
            oDTAtividades.Columns.Add("flPodeRepetirPedido");
            oDTAtividades.Columns.Add("dsTurno");

            oDTAtividades.Columns.Add("noTituloWEB");

            if (SessionEvento == null)
                Server.Transfer("frmSessaoExpirada.aspx", true);

            CarregarAtividadesGrade();

            

            Session["oDTAtividades"] = oDTAtividades;

            prpFiltrarAtividades();


            grdAtvParticipante.Focus();
        }
        else
        {            
            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            oDTAtividades = (DataTable)Session["oDTAtividades"];

            SessionIdioma = (String)Session["SessionIdioma"];

        }

        if (SessionEvento == null)
        {
            //Server.Transfer("frmSessaoExpirada.aspx", true);

            Response.Redirect("frmProgramacao.aspx?codEvento=" + Request["codEvento"] + "&cdLng=PTBR", false);
            //Response.Redirect("../frmSessaoExpirada.aspx");
            return;
        }

        verificarIdioma(SessionIdioma);
    }

    protected void verificarIdioma(string prmIdioma)
    {
        if (prmIdioma == "PTBR")
        {
            SessionIdioma = "PTBR";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Programação";

           

            

        }
        else if (prmIdioma == "ENUS")
        {
            SessionIdioma = "ENUS";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Enrollment / Registration";

            
        }
        else if (prmIdioma == "ESP")
        {
            SessionIdioma = "ESP";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "Inscripción / Registro";

            
        }
        else if (prmIdioma == "FRA")
        {
            SessionIdioma = "FRA";
            Session["SessionIdioma"] = SessionIdioma;

            lblTituloPagina.Text = "D'inscription / d'enregistrement";

            
        }
    }

    private void CarregarTipoDeAtividades()
    {

        DataTable DTTipo;

        DTTipo = SessionInscricoes.ListarTiposAtividadeAtivos(SessionEvento.CdEvento, SessionCnn);

        DataTable oDataTable = new DataTable();

        oDataTable.Columns.Add("cdTipoAtividade");
        oDataTable.Columns.Add("noTipoAtividade");

        oDataTable.Rows.Add("", " TODOS");

        if ((SessionEvento.CdCliente == "0013") && ((SessionEvento.CdEvento == "001302") || (SessionEvento.CdEvento == "001304")))// || (SessionEvento.CdEvento == "001305")))
        {

            oDataTable.Rows.Add("('05')",
                                "CONGRESSO");
            oDataTable.Rows.Add("('02','06','21','25','28','44','45')",
                                "CONGRESSO/ATIVIDADES");

        }
        else
        {
            for (int i = 0; i < DTTipo.DefaultView.Count; i++)
            {
                if (SessionEvento.CdCliente != "0013")
                {
                    if ((SessionEvento.CdEvento != "006501") || (DTTipo.DefaultView[i]["cdTipoAtividade"].ToString().Trim() != "59"))
                        oDataTable.Rows.Add(DTTipo.DefaultView[i]["cdTipoAtividade"].ToString().Trim(),
                                           DTTipo.DefaultView[i]["noTipoAtividade"].ToString().Trim());
                }
                //else
                //if ((Boolean.Parse(DTTipo.DefaultView[i]["flAtivo"].ToString().Trim())) &&
                //    (Boolean.Parse(DTTipo.DefaultView[i]["flVisivelProgramacao"].ToString().Trim()))
                //    )
                {
                    if ((DTTipo.DefaultView[i]["cdTipoAtividade"].ToString().Trim() != "05"))
                        oDataTable.Rows.Add(DTTipo.DefaultView[i]["cdTipoAtividade"].ToString().Trim(),
                                           DTTipo.DefaultView[i]["noTipoAtividade"].ToString().Trim());
                }

            }
        }

        // bsTipo.DataSource = oDataTable.DefaultView;
        
        oDataTable.DefaultView.Sort = "noTipoAtividade";

        TxtFTipo.DataSource = oDataTable.DefaultView;
        TxtFTipo.DataTextField = "noTipoAtividade";
        TxtFTipo.DataValueField = "cdTipoAtividade";
        TxtFTipo.DataBind();
        TxtFTipo.SelectedIndex = 0;

    }

    private void CarregarTemaDeAtividades(String prmTipo)
    {
        /*
        DataTable DTTema;

        DTTema = null; // clCongresso.ListarFiltrosDeTemaCursos(prmTipo);

        DataTable oDTRecibos = new DataTable();

        oDTRecibos.Columns.Add("tb010_ds_tema_pt");

        oDTRecibos.Rows.Add("");
        for (int i = 0; i < DTTema.DefaultView.Count; i++)
        {
            oDTRecibos.Rows.Add(DTTema.DefaultView[i]["tb010_ds_tema_pt"].ToString().Trim());
        }

        bsTema.DataSource = oDTRecibos.DefaultView;

        txtFTema.DataSource = bsTema;
        txtFTema.DisplayMember = "tb010_ds_tema_pt";
        txtFTema.ValueMember = "tb010_ds_tema_pt";
        txtFTema.SelectedIndex = 0;
        */
    }

    private void CarregarDtInicioDeAtividades(String prmTipo)//, String prmTema)
    {

        DataTable DTHorario;

        DTHorario = SessionInscricoes.ListarHorarios(SessionEvento.CdEvento, prmTipo, SessionCnn);

        DataTable oDataTable = new DataTable();

        oDataTable.Columns.Add("dtInicio");
        //oDataTable.Columns.Add("dtInicio");

        oDataTable.Rows.Add("");
        //oDataTable.Rows.Add("");
        //if (SessionEvento.CdCliente == "0013")
        //{
        //    if (SessionEvento.CdEvento == "001303")
        //    {
        //        oDataTable.Rows.Add("('01','02','03','09','15','19','20')",
        //                            "ATIVIDADES DO DIA 7");
        //        oDataTable.Rows.Add("('21','16','17','18','10','11','12','13','14','04','05','06','07','08')",
        //                            "ATIVIDADES DO DIA 7 E 8");
        //        oDataTable.Rows.Add("('22','23','24','25','26','27','28')",
        //                            "ATIVIDADES DIA 8");
        //    }
        //    if (SessionEvento.CdEvento == "001304")
        //    {
        //        oDataTable.Rows.Add("('05','07','15','22')", "ATIVIDADES DO DIA 1");
        //        oDataTable.Rows.Add("('02','03','04','06','25','26')", "ATIVIDADES DO DIA 1 e 2");
        //        oDataTable.Rows.Add("('01','14','16','17','20','21','23')", "ATIVIDADES DO DIA 1 a 3 ");
        //        oDataTable.Rows.Add("('18','28')", "ATIVIDADES DO DIA 2");
        //        oDataTable.Rows.Add("('12,'24')", "ATIVIDADES DO DIA 2 e 3");
        //        oDataTable.Rows.Add("('08','09','10','11','13','19','29')", "ATIVIDADES DO DIA 3");
        //        oDataTable.Rows.Add("('26')", "ATIVIDADES DO DIA 4");
        //    }
        //    if (SessionEvento.CdEvento == "001305")
        //    {
        //        oDataTable.Rows.Add("('05','07','15','22')", "ATIVIDADES DO DIA 1");
        //        oDataTable.Rows.Add("('02','03','04','06','25','26')", "ATIVIDADES DO DIA 1 e 2");
        //        oDataTable.Rows.Add("('01','14','16','17','20','21','23')", "ATIVIDADES DO DIA 1 a 3 ");
        //        oDataTable.Rows.Add("('18','28')", "ATIVIDADES DO DIA 2");
        //        oDataTable.Rows.Add("('12,'24')", "ATIVIDADES DO DIA 2 e 3");
        //        oDataTable.Rows.Add("('08','09','10','11','13','19','29')", "ATIVIDADES DO DIA 3");
        //        oDataTable.Rows.Add("('26')", "ATIVIDADES DO DIA 4");
        //    }
        //}
        //else
        //{
            for (int i = 0; i < DTHorario.DefaultView.Count; i++)
            {
                oDataTable.Rows.Add(DTHorario.DefaultView[i]["dtInicio"].ToString().Trim());
            }
        //}

        //bsHorario.DataSource = oDataTable.DefaultView;

        txtFDtInicio.DataSource = oDataTable.DefaultView;
        txtFDtInicio.DataTextField = "dtInicio";
        txtFDtInicio.DataValueField = "dtInicio";
        txtFDtInicio.DataBind();
        txtFDtInicio.SelectedIndex = 0;

    }
    protected void TxtFTipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        CarregarDtInicioDeAtividades(TxtFTipo.SelectedValue.ToString().Trim());//, txtFTema.Text.Trim());
        prpFiltrarAtividades();


        Session["sessionFiltro"] = TxtFTipo.SelectedValue.ToString();
    }
    protected void txtFDtInicio_SelectedIndexChanged(object sender, EventArgs e)
    {
        prpFiltrarAtividades();
    }
    private void prpFiltrarAtividades()
    {
        string filtro = "";
        //if ((SessionEvento.CdCliente != "0013") || ((SessionEvento.CdCliente == "0013") && (SessionEvento.CdEvento == "001305")))
        //{
            if (TxtFTipo.SelectedValue.ToString().Trim() != "")
            {
                if (filtro.Length == 0)
                    filtro += " (cdTipoAtividade like '%" + TxtFTipo.SelectedValue.ToString().Trim() + "%')";
                else
                    filtro += " and (cdTipoAtividade like '%" + TxtFTipo.SelectedValue.ToString().Trim() + "%')";
            }
        //}
        //else
        //{
        //    if (((SessionEvento.CdCliente == "0013") && (SessionEvento.CdEvento == "001303")) || ((SessionEvento.CdCliente == "0013") && (SessionEvento.CdEvento == "001304")))// || ((SessionEvento.CdCliente == "0013") && (SessionEvento.CdEvento == "001305")))
        //    {
        //        if (TxtFTipo.SelectedValue.ToString().Trim() != "")
        //        {
        //            if (filtro.Length == 0)
        //                filtro += " (cdTipoAtividade in " + TxtFTipo.SelectedValue.ToString().Trim() + ")";
        //            else
        //                filtro += " and (cdTipoAtividade like " + TxtFTipo.SelectedValue.ToString().Trim() + ")";
        //        }
        //    }
        //    else if (TxtFTipo.SelectedValue.ToString().Trim() != "")
        //    {
        //        if (filtro.Length == 0)
        //            filtro += " (dsTema in " + TxtFTipo.SelectedValue.ToString().Trim() + ")";
        //        else
        //            filtro += " and (dsTema in " + TxtFTipo.SelectedValue.ToString().Trim() + ")";
        //    }
        //}

        //if (txtFTema.SelectedValue.ToString().Trim() != "")
        //{
        //    if (filtro.Length == 0)
        //        filtro += " (tb010_ds_tema_pt like '%" + txtFTema.Text.Trim() + "%')";
        //    else
        //        filtro += " and (tb010_ds_tema_pt like '%" + txtFTema.Text.Trim() + "%')";
        //}

        //if (SessionEvento.CdCliente != "0013") //|| ((SessionEvento.CdCliente == "0013") && (SessionEvento.CdEvento == "001302")))
        //{
            /*if (txtFDtInicio.Text.ToString().Trim() != "")
            {
                if (filtro.Length == 0)
                    filtro += " (dtIni >= '" + txtFDtInicio.Text.Trim() + "' and dtIni <= '" + txtFDtInicio.Text.Trim() + "')";
                else
                    filtro += " and (dtIni >= '" + txtFDtInicio.Text.Trim() + "' and dtIni <= '" + txtFDtInicio.Text.Trim() + "')";
            }*/
        //}
        //else
        //{
        //    if (txtFDtInicio.SelectedValue.ToString().Trim() != "")
        //    {
        //        if (filtro.Length == 0)
        //            filtro += " (dsTema in " + txtFDtInicio.SelectedValue.ToString().Trim() + ")";
        //        else
        //            filtro += " and (dsTema in " + txtFDtInicio.SelectedValue.ToString().Trim() + ")";
        //    }
        //}

        //if (filtro.Length == 0)
        //    filtro += " (flInscricaoObrigatoria = 0) ";
        //else
        //    filtro += " and (flInscricaoObrigatoria = 0) ";

        /*
        if (varFiltroChoqueHorario.ToString().Trim() != "")
        {
            if (filtro.Length == 0)
                filtro += varFiltroChoqueHorario;
            else
                filtro += " and " + varFiltroChoqueHorario;
        }
        
        
        if (filtro.Length == 0)
            filtro += " cdAtividade not in ('" + cdAtiv.Trim().Replace(",", "','") + "')";
        else
            filtro += " and  cdAtividade not in ('" + cdAtiv.Trim().Replace(",", "','") + "')";
        */

        DataTable dt = oDTAtividades.Clone();

        DataRow[] dr = oDTAtividades.Select(filtro);

        foreach (DataRow drSimples in dr)
        {
            dt.ImportRow(drSimples);
        }


        grdAtvParticipante.DataSource = dt;

        grdAtvParticipante.DataBind();

        
    }

    private void CarregarAtividadesGrade()
    {

        
        DataTable DTAtividadesp;
        
        DTAtividadesp = oAtividadeCad.Listar2(SessionEvento.CdEvento, SessionCnn);
        


        if ((DTAtividadesp != null) && (DTAtividadesp.Rows.Count > 0))
        {
            oDTAtividades.Rows.Clear();

            DTAtividadesp.DefaultView.Sort = "dtInicio, dsTema";

            for (int i = 0; i < DTAtividadesp.DefaultView.Count; i++)
            {

                if ((Boolean.Parse(DTAtividadesp.DefaultView[i]["flAtivo"].ToString().Trim())) &&
                    (Boolean.Parse(DTAtividadesp.DefaultView[i]["flVisivelProgramacao"].ToString().Trim()))
                    // (DTAtividadesp.DefaultView[i]["cdAtividade"].ToString().Trim() != "001305001")
                    )
                {
                    oDTAtividades.Rows.Add(
                                        DTAtividadesp.DefaultView[i]["noTipoAtividade"].ToString().Trim(),
                                        DTAtividadesp.DefaultView[i]["noTitulo"].ToString().Trim(),
                                        DTAtividadesp.DefaultView[i]["dsTema"].ToString().Trim(),
                                        DTAtividadesp.DefaultView[i]["noLocal"].ToString().Trim(),
                                        DateTime.Parse(DTAtividadesp.DefaultView[i]["dtInicio"].ToString().Trim()).ToString("dd/MM/yyyy HH:mm"),
                                        DateTime.Parse(DTAtividadesp.DefaultView[i]["dtTermino"].ToString().Trim()).ToString("dd/MM/yyyy HH:mm"),
                                        DTAtividadesp.DefaultView[i]["cdAtividade"].ToString().Trim(),
                                        DTAtividadesp.DefaultView[i]["noProfessor"].ToString().Trim(),
                                        DTAtividadesp.DefaultView[i]["cdTipoAtividade"].ToString().Trim(),
                                        decimal.Parse(DTAtividadesp.DefaultView[i]["vlAtividade"].ToString().Trim()),
                                        Boolean.Parse(DTAtividadesp.DefaultView[i]["flAtivo"].ToString().Trim()), 
                                        Boolean.Parse(DTAtividadesp.DefaultView[i]["flInscricaoObrigatoria"].ToString().Trim()),
                                        Boolean.Parse(DTAtividadesp.DefaultView[i]["flPodeChocarHorario"].ToString().Trim()),
                                        DTAtividadesp.DefaultView[i]["dsCaminhoImgWEB"].ToString(),
                                        Boolean.Parse(DTAtividadesp.DefaultView[i]["flRequerQuantidade"].ToString().Trim()),
                                        DTAtividadesp.DefaultView[i]["vlQuantidadeMaxima"].ToString().Trim(),
                                        DTAtividadesp.DefaultView[i]["flPodeRepetirPedido"].ToString().Trim(),
                                        DTAtividadesp.DefaultView[i]["dsTurno"].ToString().Trim(),
                                        DTAtividadesp.DefaultView[i]["noTituloWEB"].ToString().Trim());

                }
            }
        }



        Session["oDTAtividades"] = oDTAtividades;

        grdAtvParticipante.DataSource = oDTAtividades.DefaultView;
        grdAtvParticipante.DataBind();

    }


    protected void grdAtvParticipante_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //if (grdAtvParticipante.Rows.Count <= 0)
        //    return;

        //lblMsg.Text = "";

        //if (Boolean.Parse(grdAtvParticipante.DataKeys[e.RowIndex].Values[15].ToString().Trim()))//inscricao obrigatoria
        //{
        //    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Item de inscrição obrigatória, não pode ser removido!'); </script>", false);
        //    return;
        //}

        //if ((!SessionEvento.FlEventoComRecebimentos) && (!SessionCategoria.FlConfirmacaoCadWeb))
        //{
           
        //    SessionInscricoes.ExcluirAtividade(SessionEvento.CdEvento, SessionParticipante.CdParticipante, grdAtvParticipante.DataKeys[e.RowIndex].Values[6].ToString(), SessionCnn);

        //    TxtFTipo.SelectedIndex = 0;
        //    //txtFTema.SelectedIndex = 0;
        //    txtFDtInicio.SelectedIndex = 0;

        //    CarregarAtividadesGrade();
        //    CarregarAtividadesParticipanteGrade();
        //}
        //else
        //{
        //    cdAtiv = cdAtiv.Replace("," + grdAtvParticipante.DataKeys[e.RowIndex].Values[6].ToString().Trim(), "");
        //    cdAtiv = cdAtiv.Replace(grdAtvParticipante.DataKeys[e.RowIndex].Values[6].ToString().Trim() + ",", "");
        //    cdAtiv = cdAtiv.Replace(grdAtvParticipante.DataKeys[e.RowIndex].Values[6].ToString().Trim(), "");

        //    Session["cdAtiv"] = cdAtiv;

        //    vlTotalAtiv.Text = (decimal.Parse(vlTotalAtiv.Text) - decimal.Parse(grdAtvParticipante.DataKeys[e.RowIndex].Values[9].ToString().Trim())).ToString("N2");
        //    vlTotalDesc.Text = (decimal.Parse(vlTotalDesc.Text) - decimal.Parse(grdAtvParticipante.DataKeys[e.RowIndex].Values[13].ToString().Trim())).ToString("N2");
        //    vlTotalPedido.Text = (decimal.Parse(vlTotalPedido.Text) - decimal.Parse(grdAtvParticipante.DataKeys[e.RowIndex].Values[14].ToString().Trim())).ToString("N2");

        //    oDTAtividadesParticipante.Rows.RemoveAt(e.RowIndex);

        //    grdAtvParticipante.DataSource = oDTAtividadesParticipante.DefaultView;
        //    grdAtvParticipante.DataBind();

        //    vlItens.Text = grdAtvParticipante.Rows.Count.ToString();

        //    prpFiltrarAtividades();
            
        //    prpCalcularDescontosPorTipoAtividade();
        //}
    }

    protected void grdAtvParticipante_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
            Label lbldtini = (Label)e.Row.FindControl("lblDtIni");
            lbldtini.Text = DateTime.Parse(lbldtini.Text).ToString("dd/MM/yyyy HH:mm");
            Label lbldttermino = (Label)e.Row.FindControl("lblDtTermino");
            lbldttermino.Text = DateTime.Parse(lbldttermino.Text).ToString("dd/MM/yyyy HH:mm");

            Label lblcdAtvcel = (Label)e.Row.FindControl("lblCdAtvSel");

            Label lbltpItem = (Label)e.Row.FindControl("lblTpItem");
            Label lbldeItem = (Label)e.Row.FindControl("lblDeItem");
            Label lblateItem = (Label)e.Row.FindControl("lblAteItem");
            Label lbllocalItem = (Label)e.Row.FindControl("lblLocalItem");

            Label lblvalorItem = (Label)e.Row.FindControl("lblValorItem");
            Label lbldescItem = (Label)e.Row.FindControl("lblDescItem");
            Label lblqtdItem = (Label)e.Row.FindControl("lblQtdItem");
            Label lblvlTotalItem = (Label)e.Row.FindControl("lblVlrTotalItem");

            Label lblLocal = (Label)e.Row.FindControl("lblLocal");
            Label lblVagas = (Label)e.Row.FindControl("lblVagas");

            Label lblDetAtv = (Label)e.Row.FindControl("lblAtividadeWEB");
            LinkButton btnDetAtiv = (LinkButton)e.Row.FindControl("btnDetalhesAtiv");

            if (lblDetAtv.Text != "")
            {
                btnDetAtiv.PostBackUrl = "~/frmDetalheAtividade.aspx?cdAtvDet=" + lblcdAtvcel.Text;
                btnDetAtiv.Visible = true;
            }

            if (SessionIdioma == "PTBR")
            {
                lbltpItem.Text = "Tipo: ";
                lbldeItem.Text = "De: ";
                lblateItem.Text = " a: ";
                lbllocalItem.Text = "Local: ";
            }
            else if (SessionIdioma == "ENUS")
            {
                lbltpItem.Text = "Type: ";
                lbldeItem.Text = "From: ";
                lblateItem.Text = " to: ";
                lbllocalItem.Text = "Site: ";
            }
            else if (SessionIdioma == "ESP")
            {
                lbltpItem.Text = "Tipo: ";
                lbldeItem.Text = "De: ";
                lblateItem.Text = " a: ";
                lbllocalItem.Text = "Local: ";
            }
            else if (SessionIdioma == "FRA")
            {
                lbltpItem.Text = "Type: ";
                lbldeItem.Text = "De: ";
                lblateItem.Text = " à: ";
                lbllocalItem.Text = "Local: ";
            }

            if ((SessionEvento.CdCliente == "0003") || (SessionEvento.CdEvento == "003401"))
            {
                lbldtini.Visible = false;
                lbldttermino.Visible = false;
                lbldeItem.Visible = false;
                lblateItem.Visible = false;
                lbllocalItem.Visible = false;
                lblLocal.Visible = false;

            }

            Image imgprofatv = (Image)e.Row.FindControl("imgAtivProf");
            imgprofatv.Visible = (imgprofatv.ImageUrl != "");

            
        }
    }
    protected void btnAvancar_Click(object sender, ImageClickEventArgs e)
    {

        //if (grdAtvParticipante.Rows.Count <= 0)
        //{
        //    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Nenhum item selecionado!'); </script>", false);
        //    return;
        //}
        //if ((SessionCategoria.FlConfirmacaoCadWeb) || (SessionEvento.FlEventoComRecebimentos))//pode entrar com conf e com receb/ sem conf e com receb / com conf e sem receb
        //{
        //    DateTime? dtVencPedido = SessionEvento.DtFechamentoInscrWeb;

        //    for (int i = 0; i < oDTAtividadesParticipante.Rows.Count; i++)
        //    {
        //        if (oDTAtividadesParticipante.DefaultView[i]["dtValidade"].ToString() != "")
        //        {
        //            if (dtVencPedido > DateTime.Parse(oDTAtividadesParticipante.DefaultView[i]["dtValidade"].ToString()))
        //                dtVencPedido = DateTime.Parse(oDTAtividadesParticipante.DefaultView[i]["dtValidade"].ToString());
        //        }
        //    }

        //    if ((SessionCategoria.FlConfirmacaoCadWeb) || (decimal.Parse(vlTotalPedido.Text) > 0))
        //    {
        //        SessionPedido = new Pedido(SessionEvento.CdEvento, SessionParticipante.CdParticipante, lblNrPedido.Text, "0", null, decimal.Parse(vlTotalPedido.Text), false, true, "", "", "", "", "", "", "", "", "", dtVencPedido, 1);

        //        SessionPedido = oPedidoCad.Gravar(SessionPedido, SessionCnn);
        //        if (SessionPedido == null)
        //        {
        //            Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
        //                       "012",
        //                       ""), true);
        //            return;
        //        }
        //        Session["SessionPedido"] = SessionPedido;
        //        if (oPedidoCad.ApagarTodasAtividadePedido(SessionPedido, SessionCnn))
        //        {

        //            for (int i = 0; i < oDTAtividadesParticipante.Rows.Count; i++)
        //            {
        //                if (!oPedidoCad.GravarAtividadePedido(
        //                        SessionPedido.CdPedido,
        //                        oDTAtividadesParticipante.DefaultView[i]["cdAtividade"].ToString(),
        //                        decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlAtividade"].ToString()),
        //                        decimal.Parse(oDTAtividadesParticipante.DefaultView[i]["vlDesconto"].ToString()),
        //                        SessionCnn))
        //                {
        //                    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
        //                            "012",
        //                            ""), true);
        //                    return;
        //                }
        //            }
                    
        //            Session["SessionPedido"] = SessionPedido;
        //            Server.Transfer(string.Format("frm_formapagamento.aspx?cdMatricula={0}",
        //                                            SessionParticipante.CdParticipante), true);

        //        }
        //        else
        //        {
        //            Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
        //                   "012",
        //                   ""), true);
        //            return;
        //        }
        //    }
        //    else  
        //    {
        //        for (int i = 0; i < oDTAtividadesParticipante.Rows.Count; i++)
        //        {  
        //            //gerar matricula
        //            if (SessionInscricoes.MatriculasGravar(SessionParticipante.CdEvento, SessionParticipante.CdParticipante, oDTAtividadesParticipante.DefaultView[i]["cdAtividade"].ToString(), 0, SessionCnn))
        //            {
        //                //enviar email

        //                //-----
        //                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
        //                               "002",
        //                               ""), true);
        //            }
        //            else
        //            {
        //                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
        //                               "012",
        //                               ""), true);
        //                return;
        //            }
        //            //-----
                    
        //        }
        //            //enviar e-mail para participante
        //        Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
        //            "013",
        //            ""), true);
        //    }
            
        //}
        //else
        //{
        //    //enviar email

        //    //-----
        //    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
        //                   "002",
        //                   ""), true);
        //}
    }
    
}
