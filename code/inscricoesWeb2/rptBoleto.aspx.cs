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
using Stimulsoft.Report;
using Stimulsoft.Report.Web;
using Stimulsoft.Report.Export;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Base;
using Reports;

public partial class rptBoleto : System.Web.UI.Page
{
    SqlConnection SessionCnn;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Request["e"] != null) && (Request["b"] != null) && (Request["p"] != null))
            {
                //Label1.Text = Request["e"].ToString();

                //SessionPacientes = (clsPacientes)Session["SessionPacientes"];

                //SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["cnnCinicaSer"].ToString());

                SessionCnn = new SqlConnection("Data Source=dbsq0012.whservidor.com;Initial Catalog=fazendomai2;Persist Security Info=True;User ID=fazendomai2;Password=Qu454rFm");
                //SessionCnn = new SqlConnection("Data Source=krksa-pc;Initial Catalog=dbEventos_FM;Persist Security Info=True;User ID=sa;Password=krksa171");
                //SessionCnn = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=dbEventos_FM;Persist Security Info=True;User ID=sa;Password=RaC982973");


                //StiReport rptboleto = new BoletoPrt();
                //rptboleto.Render();
                ////rptboleto.Compile();

                ////SqlConnection conn = new SqlConnection(connString);
                //(rptboleto.CompiledReport.Dictionary.Databases["cnnEventos"] as StiSqlDatabase).ConnectionString = SessionCnn.ConnectionString;

                ////rptboleto.

                //rptboleto.CompiledReport.DataSources["Evento"].Parameters["@cdEvento"].ParameterValue = Request["e"];

                //rptboleto.CompiledReport.DataSources["EventosImgs"].Parameters["@cdEvento"].ParameterValue = Request["e"];
                //rptboleto.CompiledReport.DataSources["boleto"].Parameters["@cdEvento"].ParameterValue = Request["e"];
                //rptboleto.CompiledReport.DataSources["boleto"].Parameters["@cdBoleto"].ParameterValue = Request["b"];
                //rptboleto.CompiledReport.DataSources["boleto"].Parameters["@cdPedido"].ParameterValue = Request["p"];

                ////rptboleto["wrkAceite"] = txtAceite.Text;
                ////rptboleto["wrkDsMsgCooperativa"] = txtDsMsgCoooperativa.Text; //txt.Text; 
                ////rptboleto["wrkDsEmpreendimento"] = TxtCdEmpreendimento.Text;
                ////if (TxtDtBaseCalculo.Text.Replace("/", "").Trim().Length < 8)
                ////rptboleto["wrkDtBaseCalc"] = TxtDtBaseCalculo.Text;
                ////else
                ////    rptboleto["wrkDtVenc"] = txtDtVenc.Text;
                ////rptboleto["url_logo"] = "http://www.kalypso.com.br/images/categorias/cat1.bmp";
                //rptboleto.Render();
                //StiReportResponse.ResponseAsPdf(this, rptboleto, false);
                ////rptboleto.Show();


                
                SqlDataAdapter Evento = new SqlDataAdapter("SELECT tbClientesEventos.* " +
                                                           "FROM tbClientesEventos " +
                                                           "WHERE tbClientesEventos.cdEvento = '" + Request["e"].ToString() + "' ", SessionCnn);
                
                SqlDataAdapter EventosImgs = new SqlDataAdapter("SELECT tbEventosImagens.* " +
                                                                "FROM tbEventosImagens " +
                                                                "WHERE cdEvento = '" + Request["e"].ToString() + "' ", SessionCnn);
                
                SqlDataAdapter DataHora = new SqlDataAdapter("SELECT GETDATE() DataHora " +
                                                              "  from dbo.tbClientesEventos a " +
                                                              "  where a.cdEvento = '" + Request["e"].ToString() + "' ", SessionCnn);

                SqlDataAdapter AtividadeBoleto = new SqlDataAdapter("select pa.cdPedido, " +
                                                                     "  pa.vlUnitario, " +
                                                                     "  pa.vlUnitarioDesconto, " +
                                                                     "  atv.* " +
                                                                "from dbo.tbPedidosAtividades pa  " + 
                                                                "join dbo.tbAtividades atv " +
                                                                 " on pa.cdAtividade = atv.cdAtividade " +
                                                               " where pa.cdPedido = '" + Request["p"].ToString() + "' ", SessionCnn);

                SqlDataAdapter boleto = new SqlDataAdapter("SELECT " +
                                                             " cdBoleto, " +
                                                             " b.cdPedido, " +
                                                             " b.cdEvento, " +
                                                             " dtEmissao,  " +
                                                             " dtVencimento, " +
                                                             " vlBoleto, " +
                                                             " b.vlTaxaBancaria, " +
                                                             " flRecebido, " +
                                                             " p.dtPedido, " +
                                                             " part.*, " +
                                                             " c.noCategoria, " +
                                                             " cc.* " +
                                                            "FROM  " +
                                                             " dbo.tbPedidosBoletos b " +
                                                            "join dbo.tbPedidos p " +
                                                             " on b.cdEvento = p.cdEvento " +
                                                             " and b.cdPedido = p.cdPedido " +
                                                            " join dbo.tbParticipantes part " +
                                                             " on p.cdEvento = part.cdEvento " +
                                                            " and p.cdParticipante = part.cdParticipante " +
                                                            " join dbo.tbCategorias c " +
                                                             " on part.cdEvento = c.cdEvento  " +
                                                             " and part.cdCategoria = c.cdCategoria " +
                                                            " left join tbEventosCarteirasCobranca cc " +
                                                              "on b.cdEvento = cc.cdEvento  " +
                                                            " WHERE b.cdEvento = '" + Request["e"].ToString() + "' " +
                                                             " AND b.cdBoleto = '" + Request["b"].ToString() + "' ", SessionCnn);

                cllEventos.DataSets.DSBloetosWeb dsboletosweb = new cllEventos.DataSets.DSBloetosWeb();

                SessionCnn.Open();
                Evento.Fill(dsboletosweb.Evento);
                EventosImgs.Fill(dsboletosweb.EventosImgs);
                DataHora.Fill(dsboletosweb.DataHora);
                boleto.Fill(dsboletosweb.boleto);
                AtividadeBoleto.Fill(dsboletosweb.AtividadeBoleto);
                SessionCnn.Close();

                StiReport rptboleto = new BoletoPrt();
                rptboleto.RegData(dsboletosweb);
                rptboleto["wrkCnn"] = SessionCnn.ConnectionString; 
                rptboleto.Render();
                StiReportResponse.ResponseAsPdf(this, rptboleto, false);

            }
        }
    }
}
