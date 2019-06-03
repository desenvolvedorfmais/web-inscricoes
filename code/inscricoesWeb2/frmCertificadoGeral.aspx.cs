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


public partial class frmCertificadoGeral : System.Web.UI.Page 
{
    static frmCertificadoGeral()
	{
        StiOptions.Engine.FullTrust = false;

		StiConfig.InitWeb();
		StiReport.HideExceptions = false;
		StiReport.HideMessages = false;
	}

    //Participante SessionParticipante;
    //ParticipanteCad oParticipanteCad = new ParticipanteCad();
    Evento SEvento;
    EventoCad oEventoCad = new EventoCad();
    SqlConnection SessionCnn;

    Participante SParticipante;
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

                //Site-producao - IP
                //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOU1UZzVMak00TGprd0xqRXdPVHRKYm1sMGFXRnNJRU5oZEdGc2IyYzlabUY2Wlc1a2IyMWhhWE03VUdWeWMybHpkQ0JUWldOMWNtbDBlU0JKYm1adlBWUnlkV1U3VlhObGNpQkpSRDFtWVhwbGJtUnZiV0ZwY3p0UVlYTnpkMjl5WkQxUmRUUTFOSEpHYlUxRVFRPT0=")));

                
                Session["SessionCnn"] = SessionCnn;

            }

            

            Geral oGeral = new Geral();

            if (oGeral.verificarSiteManutencao("1", SessionCnn))
            {
                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                "05",
                                ""), true);
            }
            
            //SessionEvento = (Evento)Session["SessionEvento"];
            //if (SEvento == null)
            //{
                btnVoltar.Visible = false;
                if ((Request["e"] != null) &&
                    (Request["e"] != ""))
                {
                    string cd_Evento = cllEventos.Crypto.DecryptStringAES(Request["e"].ToString());

                    SEvento = oEventoCad.Pesquisar(cd_Evento, SessionCnn);
                    Session["SEvento"] = SEvento;
                    if (SEvento == null)
                    {
                        return;                       

                    }
                }
                
                
            //}

            if ((Request["p"] != null) &&
                (Request["p"] != ""))
            {
                string cd_participante = cllEventos.Crypto.DecryptStringAES(Request["p"].ToString());

                SParticipante = oParticipanteCad.Pesquisar(SEvento.CdEvento, cd_participante, SessionCnn);
                Session["SParticipante"] = SParticipante;
                if (SParticipante == null)
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

            SEvento = (Evento)Session["SEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            SParticipante = (Participante)Session["SParticipante"];

        }

        StiReport stirep = new StiReport();
        //string caminho = Server.MapPath("/inscricoesWeb");
        string caminho = Server.MapPath("");

        

        if ((SEvento.CdEvento == "000543") &&
            (SParticipante.CdCategoria == "00054306"))
        {
            caminho = caminho + "\\relatorios\\rptCertificado" + SEvento.CdEvento + "Palestrantes.mrt";
        }
        else
            caminho = caminho + "\\relatorios\\rptCertificado" + SEvento.CdEvento + ".mrt";

        stirep.Load(caminho);
        stirep.Dictionary.Databases.Clear();
        stirep.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("cnnCongresso", SessionCnn.ConnectionString)); 
            
        //CertificadosCad oCertificadosCad = new CertificadosCad();
        //oCertificadosCad.ConfirmarImpressoes(SEvento.CdEvento, SParticipante.CdParticipante, SEvento.CdEvento+"001", SessionCnn);

        stirep.ReportAlias = "Certificado - " + SEvento.NoEvento + "-" + SParticipante.NoParticipante;

        stirep.Compile();        

        stirep["@cdEvento"] = SEvento.CdEvento;// "001001";
        stirep["@cdParticipante"] = SParticipante.CdParticipante;// SessionEvento.CdEvento;// "001001";
        stirep.Render();

        //stirep.Show();
        StiWebViewer1.Report = stirep;
        StiWebViewer1.PrintToPdf();

        
        
            

        if (SEvento.CdEvento == "002902")
        {
            StiWebViewer1.PrintToPdf();
            Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                    "030", ""), true);
        }
       
    }
    protected void btnVoltar_Click(object sender, EventArgs e)
    {
       
    }
    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        StiWebViewer1.PrintToPdf();// Report.Print(true);
        Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                       "030", ""), true);
    }
}
