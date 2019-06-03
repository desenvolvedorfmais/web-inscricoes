using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

//using System.Security.Policy;

using cllEventos;
//using CLLFuncoes;
using System.Data.SqlClient;

using Stimulsoft.Report;
using Stimulsoft.Report.Web;
using Stimulsoft.Base;
using Stimulsoft.Controls;

//using Reports;


public partial class frmDadosHospedagemExpoepi : System.Web.UI.Page 
{
    static frmDadosHospedagemExpoepi()
	{
		StiConfig.InitWeb();
		StiReport.HideExceptions = false;
		StiReport.HideMessages = false;
	}

    //Participante SessionParticipante;
    //ParticipanteCad oParticipanteCad = new ParticipanteCad();
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();
    SqlConnection SessionCnn;

    Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (StiWebViewer1.IsImageRequest) return;

        if (!Page.IsPostBack)
        {
            
            SessionCnn = (SqlConnection)Session["SessionCnn"];
            if (SessionCnn == null)
            {
                //local 1
                //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOWEzSnJjMkV0Y0dNN1NXNXBkR2xoYkNCRFlYUmhiRzluUFdSaVJYWmxiblJ2YzE5R1RUdFFaWEp6YVhOMElGTmxZM1Z5YVhSNUlFbHVabTg5VkhKMVpUdFZjMlZ5SUVsRVBYTmhPMUJoYzNOM2IzSmtQV3R5YTNOaE1UY3g=")));

                //local 2 note novo
                //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOUxseHJjbXR6WVhCak8wbHVhWFJwWVd3Z1EyRjBZV3h2Wnoxa1lrVjJaVzUwYjNOZlJrMDdVR1Z5YzJsemRDQlRaV04xY21sMGVTQkpibVp2UFZSeWRXVTdWWE5sY2lCSlJEMXpZVHRRWVhOemQyOXlaRDFyY210ellURTNNUT09")));

                //servidor
                //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOVptRjZaVzVrYjIxaGFYTTdTVzVwZEdsaGJDQkRZWFJoYkc5blBXUmlSWFpsYm5SdmMxOUdUVHRRWlhKemFYTjBJRk5sWTNWeWFYUjVJRWx1Wm04OVZISjFaVHRWYzJWeUlFbEVQWE5oTzFCaGMzTjNiM0prUFV0eWEzTmhNVGN4")));

                //MinSaude
                //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOUxseHpjV3hsZUhCeVpYTnpPMGx1YVhScFlXd2dRMkYwWVd4dlp6MWtZa1YyWlc1MGIzTmZSazA3VUdWeWMybHpkQ0JUWldOMWNtbDBlU0JKYm1adlBWUnlkV1U3VlhObGNpQkpSRDF6WVR0UVlYTnpkMjl5WkQxU1lVTTVPREk1TnpNPQ==")));

                //Site
                //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOU1UZzVMak00TGprMUxqY3lPMGx1YVhScFlXd2dRMkYwWVd4dlp6MW1ZWHBsYm1SdmJXRnBjenRRWlhKemFYTjBJRk5sWTNWeWFYUjVJRWx1Wm04OVZISjFaVHRWYzJWeUlFbEVQV1poZW1WdVpHOXRZV2x6TzFCaGMzTjNiM0prUFZGMU5EVTBja1p0")));

                //Site2-historico
                //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOWJYTnpjV3d1Wm1GNlpXNWtiMjFoYVhNdVkyOXRPMGx1YVhScFlXd2dRMkYwWVd4dlp6MW1ZWHBsYm1SdmJXRnBjekU3VUdWeWMybHpkQ0JUWldOMWNtbDBlU0JKYm1adlBWUnlkV1U3VlhObGNpQkpSRDFtWVhwbGJtUnZiV0ZwY3pFN1VHRnpjM2R2Y21ROVVYVTBOVFJ5Um0wPQ==")));

                //Site-producao
                //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOWJYTnpjV3d1Wm1GNlpXNWtiMjFoYVhNdVkyOXRPMGx1YVhScFlXd2dRMkYwWVd4dlp6MW1ZWHBsYm1SdmJXRnBjenRRWlhKemFYTjBJRk5sWTNWeWFYUjVJRWx1Wm04OVZISjFaVHRWYzJWeUlFbEVQV1poZW1WdVpHOXRZV2x6TzFCaGMzTjNiM0prUFZGMU5EVTBja1p0VFVSQg==")));

                //Site-producao - AZURE
                SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOWFHcDBZWEF3YjNkcWJpNWtZWFJoWW1GelpTNTNhVzVrYjNkekxtNWxkRHRKYm1sMGFXRnNJRU5oZEdGc2IyYzlaR0pmWlhabGJuUnZjMFpOTzFCbGNuTnBjM1FnVTJWamRYSnBkSGtnU1c1bWJ6MVVjblZsTzFWelpYSWdTVVE5UTI5dVlYTmxiWE03VUdGemMzZHZjbVE5UXpCdU5EVmxiVFV5TURFMg==")));

                
                Session["SessionCnn"] = SessionCnn;

            }

            //if (SessionParticipante == null)
            //    SessionParticipante = (Participante)Session["SessionParticipante"];
            //else
            //    Session["SessionParticipante"] = SessionParticipante;


            Geral oGeral = new Geral();

            if (oGeral.verificarSiteManutencao("1", SessionCnn))
            {
                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                "05",
                                ""), true);
            }
            
            SessionEvento = (Evento)Session["SessionEvento"];
            if (SessionEvento == null)
            {
                btnVoltar.Visible = false;
                if ((Request["e"] != null) &&
                    (Request["e"] != ""))
                {
                    string cd_Evento = cllEventos.Crypto.DecryptStringAES(Request["e"].ToString());

                    SessionEvento = oEventoCad.Pesquisar(cd_Evento, SessionCnn);
                    Session["SessionEvento"] = SessionEvento;
                    if (SessionEvento == null)
                    {
                        return;
                        //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                        //            "003",
                        //            ""), true);

                    }
                }
                
                
            }

            if ((Request["p"] != null) &&
                (Request["p"] != ""))
            {
                string cd_participante = cllEventos.Crypto.DecryptStringAES(Request["p"].ToString());

                SessionParticipante = oParticipanteCad.Pesquisar(SessionEvento.CdEvento, cd_participante, SessionCnn);
                Session["SessionParticipante"] = SessionParticipante;
                if (SessionParticipante == null)
                {
                    return;
                    //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                    //                 "04",
                    //                 ""), true);

                }
            }
        }
        else
        {
            //SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            SessionParticipante = (Participante)Session["SessionParticipante"];

        }

        StiReport stirep = new StiReport();
        //string caminho = Server.MapPath("/inscricoesWeb");
        string caminho = Server.MapPath("");

        //stirep.LoadDocumentFromUrl(caminho+"\\relatorios\\financeiro.mrt");
        
        //if (SessionEvento.CdEvento == "001302")
        //{
        //    if (SessionPedido.NuCPFCNPJRecibo.Length == 11)
        //        caminho = caminho + "\\relatorios\\rptReciboPF.mrt";
        //    else
        //        caminho = caminho + "\\relatorios\\rptReciboPJ.mrt";
        //}
        //else

        caminho = caminho + "\\relatorios\\rptDadosParticipante"+SessionEvento.CdEvento+".mrt";

        stirep.Load(caminho);
        stirep.Dictionary.Databases.Clear();
        stirep.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("cnnCongresso", SessionCnn.ConnectionString)); 
            //"Password=krksa171;Persist Security Info=True;Data Source=krksa-pc;Integrated " +
            //  "Security=False;Initial Catalog=dbEventos_FM;User ID=sa"));
        //StiWebViewer1.Report = StiWebReport1.GetReport(); 
        //DataSet ds = new DataSet();
        //stirep.RegData("Evento");
        //stirep.Dictionary.Synchronize();
        //stirep.Compile();

       // ReciboCad oReciboCad = new ReciboCad();
       // Recibo oRecibo = oReciboCad.PesquisarReciboPedido(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);

        //if (oRecibo == null)
        //    return;

        stirep.Compile();
        stirep["@cdEvento"] = SessionEvento.CdEvento;// "001001";
        stirep["@cdParticipante"] = SessionParticipante.CdParticipante;// SessionEvento.CdEvento;// "001001";
        stirep.Render();

        //stirep.Show();
        StiWebViewer1.Report = stirep;
        //if (SessionEvento.CdEvento == "002902")
        //{
            StiWebViewer1.PrintToPdf();
        //    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
        //                            "030", ""), true);
        //}
       // StiWebViewer1.Report = StiWebReport1.GetReport(); 
    }
    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        //Server.Transfer(string.Format("frmListaPedidos.aspx?p={0}",
        //   cllEventos.Crypto.EncryptStringAES(SessionPedido.CdPedido.Substring(15, 3))), true);
    }
    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        StiWebViewer1.PrintToDirect();// Report.Print(true);
    }
}
