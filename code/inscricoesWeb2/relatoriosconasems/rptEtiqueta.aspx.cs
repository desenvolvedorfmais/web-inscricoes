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

public partial class rptEtiqueta : System.Web.UI.Page 
{
    static rptEtiqueta()
	{
		StiConfig.InitWeb();
		StiReport.HideExceptions = false;
		StiReport.HideMessages = false;
	}

    Evento SEvento;
    EventoCad oEventoCad = new EventoCad();

    Participante SParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();

    SqlConnection SCnn;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (StiWebViewer1.IsImageRequest) return;

        if (!Page.IsPostBack)
        {
            if (SCnn == null)
            {
                //Site-producao 
                //SCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOWJYTnpjV3d1Wm1GNlpXNWtiMjFoYVhNdVkyOXRPMGx1YVhScFlXd2dRMkYwWVd4dlp6MW1ZWHBsYm1SdmJXRnBjekk3VUdWeWMybHpkQ0JUWldOMWNtbDBlU0JKYm1adlBWUnlkV1U3VlhObGNpQkpSRDFtWVhwbGJtUnZiV0ZwY3pJN1VHRnpjM2R2Y21ROVF6QnVORFZsYlRVeU1ERTI=")));

                //Site-producao - AZURE
                SCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOWFHcDBZWEF3YjNkcWJpNWtZWFJoWW1GelpTNTNhVzVrYjNkekxtNWxkRHRKYm1sMGFXRnNJRU5oZEdGc2IyYzlaR0pmWlhabGJuUnZjMFpOTzFCbGNuTnBjM1FnVTJWamRYSnBkSGtnU1c1bWJ6MVVjblZsTzFWelpYSWdTVVE5UTI5dVlYTmxiWE03VUdGemMzZHZjbVE5UXpCdU5EVmxiVFV5TURFMg==")));


                //Site-producao - IP
                //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOU1UZzVMak00TGprd0xqRXdPVHRKYm1sMGFXRnNJRU5oZEdGc2IyYzlabUY2Wlc1a2IyMWhhWE03VUdWeWMybHpkQ0JUWldOMWNtbDBlU0JKYm1adlBWUnlkV1U3VlhObGNpQkpSRDFtWVhwbGJtUnZiV0ZwY3p0UVlYTnpkMjl5WkQxUmRUUTFOSEpHYlUxRVFRPT0=")));
            }

            Session["SCnn"] = SCnn;


            SEvento = (Evento)Session["SEvento"];
            if (SEvento == null)
            {
                if ((Request["e"] != null) &&
                    (Request["e"] != ""))
                {
                    string cd_Evento = cllEventos.Crypto.DecryptStringAES(Request["e"].ToString());

                    SEvento = oEventoCad.Pesquisar(cd_Evento, SCnn);
                    Session["SEvento"] = SEvento;
                    if (SEvento == null)
                    {
                        //Label1.Text = cd_Evento + "-" + Request.UserHostAddress + "-" + SCnn.ConnectionString + "\n" + oEventoCad.RcMsg;
                        return;

                    }
                }


            }

            if ((Request["p"] != null) &&
                (Request["p"] != ""))
            {
                string cd_participante = cllEventos.Crypto.DecryptStringAES(Request["p"].ToString());

                SParticipante = oParticipanteCad.Pesquisar(SEvento.CdEvento, cd_participante, SCnn);
                Session["SParticipante"] = SParticipante;
                if (SParticipante == null)
                {
                    Label1.Text = "saiu 2";
                    return;
                    //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                    //                 "04",
                    //                 ""), true);

                }
            }


            //if (SessionEvento == null)
            //    SessionEvento = (Evento)Session["SessionEvento"];
            //else
            //    Session["SessionEvento"] = SessionEvento;

            //if (SessionParticipante == null)
            //    SessionParticipante = (Participante)Session["SParticipante"];
            //else
            //    Session["SessionParticipante"] = SessionParticipante;
        }
        else
        {
            SParticipante = (Participante)Session["SParticipante"];

            SEvento = (Evento)Session["SEvento"];

            SCnn = (SqlConnection)Session["SCnn"];

        }

        StiReport stirep = new StiReport();
        string caminho = "";

        if (Request.Url.ToString().ToLower().Contains("localhost"))
            caminho = Server.MapPath("");
        else
            caminho = Server.MapPath("");

        
            //if (SParticipante.DsSecretario == "SIM")
            //    stirep.Load(@caminho + "\\relatorios\\rptEtiqueta" + SEvento.CdEvento + "_Gestor.mrt");
            //else
            //    stirep.Load(@caminho + "\\relatorios\\rptEtiqueta" + SEvento.CdEvento + "_Participante.mrt");

            Label1.Text = @caminho + "\\relatorios\\rptEtiqueta" + SEvento.CdEvento + "_Participante.mrt";

        stirep.Dictionary.Databases.Clear();
        stirep.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("cnnCongresso", SCnn.ConnectionString));
        
        //"Password=krksa171;Persist Security Info=True;Data Source=krksa-pc;Integrated " +
        //  "Security=False;Initial Catalog=dbEventos_FM;User ID=sa"));
        //StiWebViewer1.Report = StiWebReport1.GetReport(); 
        //DataSet ds = new DataSet();
        //stirep.RegData("Evento");
        //stirep.Dictionary.Synchronize();
        //stirep.Compile();
        stirep.Compile();
        //stirep["@cdEvento"] = SessionEvento.CdEvento;// "001001";
        //stirep["@cdParticipante"] = SessionParticipante.CdParticipante;

        stirep.CompiledReport.DataSources["qry1"].Parameters["@CdEvento"].ParameterValue = SParticipante.CdEvento;
        stirep.CompiledReport.DataSources["qry1"].Parameters["@cdParticipante"].ParameterValue = SParticipante.CdParticipante;

        //stirep.CompiledReport.DataSources["dependentes"].Parameters["@cdEvento"].ParameterValue = SessionParticipante.CdEvento;
        //stirep.CompiledReport.DataSources["dependentes"].Parameters["@cdParticipante"].ParameterValue = SessionParticipante.CdParticipante;

        stirep.Render();

        //stirep.Show();
        StiWebViewer1.Report = stirep;

        //if (SessionEvento.CdEvento != "001305")
        //{
        //    StiWebViewer1.PrintDestination = StiPrintDestination.PopupWindow;
        //    StiWebViewer1.PrintMode = StiPrintMode.CurrentPage;
        //    StiWebViewer1.PrintToDirect();
        //}
        //else
        //{
        //StiWebViewer1.show
           StiWebViewer1.PrintToPdf();

           StiWebViewer1.Visible = false;
           // Server.Transfer("frmCredencial.aspx", true);
        //}
        //Server.Transfer("frmCadastroAuto.aspx", true);

        

    }
}
