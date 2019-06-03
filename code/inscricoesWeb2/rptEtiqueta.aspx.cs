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

    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();

    SqlConnection SessionCnn;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (StiWebViewer1.IsImageRequest) return;

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
        }
        else
        {
            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

        }

        StiReport stirep = new StiReport();
        string caminho = "";

        if (Request.Url.ToString().ToLower().Contains("localhost"))
            caminho = Server.MapPath("");
        else
            caminho = Server.MapPath("");

        if (SessionEvento.CdEvento != "001305")
        {
            stirep.Load(@caminho + "\\relatorios\\rptEtiqueta" + SessionEvento.CdEvento + ".mrt");
        }
        else
        {
            if (SessionParticipante.NoAreaAtuacao == "SIM")
                stirep.Load(@caminho + "\\relatorios\\rptEtiqueta" + SessionEvento.CdEvento + "_Gestor.mrt");
            else
                stirep.Load(@caminho + "\\relatorios\\rptEtiqueta" + SessionEvento.CdEvento + "_Participante.mrt");
        }
        stirep.Dictionary.Databases.Clear();
        stirep.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("cnnCongresso", SessionCnn.ConnectionString));
        
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

        stirep.CompiledReport.DataSources["qry1"].Parameters["@cdEvento"].ParameterValue = SessionParticipante.CdEvento;
        stirep.CompiledReport.DataSources["qry1"].Parameters["@cdParticipante"].ParameterValue = SessionParticipante.CdParticipante;

        stirep.CompiledReport.DataSources["dependentes"].Parameters["@cdEvento"].ParameterValue = SessionParticipante.CdEvento;
        stirep.CompiledReport.DataSources["dependentes"].Parameters["@cdParticipante"].ParameterValue = SessionParticipante.CdParticipante;

        stirep.Render();

        //stirep.Show();
        StiWebViewer1.Report = stirep;

        if (SessionEvento.CdEvento != "001305")
        {
            StiWebViewer1.PrintDestination = StiPrintDestination.PopupWindow;
            StiWebViewer1.PrintMode = StiPrintMode.CurrentPage;
            StiWebViewer1.PrintToDirect();
        }
        else
        {
            StiWebViewer1.PrintToPdf();
            Server.Transfer("frmCredencial.aspx", true);
        }
        //Server.Transfer("frmCadastroAuto.aspx", true);

        

    }
}
