using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;

using CLLFuncoes;
using cllEventos;

//using MSXML2;

using System.Xml;
//using Recaptcha;
using MSCaptcha;

public partial class frmEmitirCertificado : System.Web.UI.Page
{
    Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();

    CategoriaCad oCategoriaCad = new CategoriaCad();

    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    SqlConnection SessionCnn;

    String SessionIdioma;

    string tpRotina;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if ((Request["codEvento"] == null) || (Request["tpRotina"] == null))
            {
                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                "003",
                                oEventoCad.RcMsg), true);
                return;
            }


            //if ((SqlConnection)Session["SessionCnn"] == null)
            //{
                //local 1
                //SessionCnn = new SqlConnection(cllEventos.Crypto.DecryptStringAES(cllEventos.Crypto.DecryptStringAES("UkdGMFlTQlRiM1Z5WTJVOWEzSnJjMkV0Y0dNN1NXNXBkR2xoYkNCRFlYUmhiRzluUFdSaVJYWmxiblJ2YzE5R1RUdFFaWEp6YVhOMElGTmxZM1Z5YVhSNUlFbHVabTg5VkhKMVpUdFZjMlZ5SUVsRVBYTmhPMUJoYzNOM2IzSmtQV3R5YTNOaE1UY3g=")));

                //local 2
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

            //}
            //else
             //   SessionCnn = (SqlConnection)Session["SessionCnn"];

            Session["SessionCnn"] = SessionCnn;


            string cdEvento = cllEventos.Crypto.DecryptStringAES(Request.QueryString["codEvento"]);
            string tpRotina = Request.QueryString["tpRotina"];

            string cdCategoria = Request.QueryString["cat"];
            string Entidade = Request.QueryString["ent"];




            Session["tpRotina"] = tpRotina;


            Geral oGeral = new Geral();
            if (oGeral.verificarSiteManutencao("1", SessionCnn))
            {
                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                "05",
                                ""), true);
            }

            

            //if ((Evento)Session["SessionEvento"] == null)
            //{
                if ((cdEvento.Trim() == "") || (cdEvento.Trim().Length != 6))
                {
                    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}",
                                    "04"), true);
                }

                SessionEvento = oEventoCad.Pesquisar(cdEvento.Substring(0, 4), cdEvento, SessionCnn);
                Session["SessionEvento"] = SessionEvento;


                if (SessionEvento == null)
                {
                    
                    Server.Transfer("frmSessaoExpirada.aspx", true);
                }

                
            //}
            //else
            //{
            //    SessionEvento = (Evento)Session["SessionEvento"];
            //    Session["SessionEvento"] = SessionEvento;
            //}

            if (!SessionEvento.FlLiberarCertificacaoWeb)
            {
                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                    "039",
                    ""), true);
            }


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





            //if (tpRotina.ToUpper() != "EMTCERT")
            //{
            //    if ((SessionEvento.DtFinalEvento == null) ||
            //            (SessionEvento.DtFinalEvento < DateTime.Parse(DateTime.Today.ToString("dd/MM/yyyy"))))
            //    {
            //        Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
            //                        "006",
            //                        oEventoCad.RcMsg + cdEvento.Substring(0, 4)), true);
            //    }
            //}

            //if (SessionEvento.FlSuspenderInscricaoWeb)
            //{
            //    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
            //                    "007",
            //                    oEventoCad.RcMsg + cdEvento.Substring(0, 4)), true);
            //}

            //if ((SessionEvento.DtAberturaInscrWeb == null) ||
            //    (SessionEvento.DtAberturaInscrWeb > DateTime.Parse(DateTime.Today.ToString("dd/MM/yyyy"))))
            //{
            //    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
            //                    "004",
            //                    oEventoCad.RcMsg + cdEvento.Substring(0, 4)), true);
            //}

            //SessionIdioma = (String)Session["SessionIdioma"];
            //if (SessionIdioma == null)
            //    SessionIdioma = "PTBR";
            //Session["SessionIdioma"] = SessionIdioma;




            /*
            if (SessionCnn == null)
                SessionCnn = (SqlConnection)Session["SessionCnn"];
            else
                Session["SessionCnn"] = SessionCnn;

            

            



            //verificarIdioma(SessionIdioma);

            //if (SessionParticip == null)
            //    SessionParticip = (Participante)Session["SessionParticip"];
            //else
            //    Session["SessionParticip"] = SessionParticip;


            if (SessionEvento == null)
                SessionEvento = (Evento)Session["SessionEvento"];
            else
                Session["SessionEvento"] = SessionEvento;

            if (SessionEvento == null)
                Server.Transfer("frmSessaoExpirada.aspx", true);


            */



            if (cdEvento != "000565")
                txtCPF.Focus();
            else
            {
                tbCPFPesquisa.Visible = false;
                dadosparticipante.Visible = false;

                CustomValidator2.Enabled = false;
                RFVCPF.Enabled = false;
                if (Entidade == null)
                    Entidade = "";
                ListarCertificados(cdEvento, Entidade, cdCategoria);

            }

            //if ((SessionEvento.CdCliente == "0013") || (SessionEvento.CdEvento == "005102"))
            //{
            
            //}
            //lincaracteres.Visible = false;
        }
        else
        {
            //SessionParticipante = (Participante)Session["SessionParticipante"];

            //SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            //SessionIdioma = (String)Session["SessionIdioma"];

            tpRotina = (string)Session["tpRotina"];
        }

        ToolkitScriptManager1.RegisterPostBackControl(Button1);

        //if (SessionEvento == null)
        //    Server.Transfer("frmSessaoExpirada.aspx", true);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        if (!Page.IsValid)

            lblMsg.Text = "";
        if (txtCPF.Text.Replace(".", "").Replace("-", "").Length < 11)
        {
            lblMsg.Text = "CPF Inválido";
            return;
        }

        
                

        DataTable dtTemp = ListarCertificados(txtCPF.Text.Replace(".", "").Replace("-", ""));;
        DataList1.DataSource = dtTemp;
        DataList1.DataBind();

        if ((dtTemp != null) && (dtTemp.Rows.Count > 0))
        {
            lblIdentificador.Text = dtTemp.DefaultView[0]["cdParticipante"].ToString();
            lblNoParticipante.Text = dtTemp.DefaultView[0]["noParticipante"].ToString();
            lblCategoria.Text = dtTemp.DefaultView[0]["noCategoria"].ToString();
        }

        
        
    }


    public DataTable ListarCertificados(string prmCPF)
    {

        //if (prmData == "")
        //    return;

        lblMsg.Visible = false;
        if (SessionCnn == null)
        {
            //_erroNoCampo = true;
            lblMsg.Visible = true;
            lblMsg.Text = "Conexão inválida ou inexistente";
            return null;
        }
        if (SessionCnn.State != ConnectionState.Open)
        {
            try
            {
                SessionCnn.Open();
            }
            catch
            {
                //_erroNoCampo = true;
                lblMsg.Visible = true;
                lblMsg.Text = "Conexão inválida";
                return null;
            }
        }

        DataTable DT = new DataTable();
        SqlDataAdapter Dap;
        try
        {
            try
            {
                string cmd =
                    "SELECT year(e.dtInicioEvento) ano, e.noEvento, e.cdEVento, e.dsEvento, p.cdParticipante, p.noParticipante, c.noCategoria " +
                    "  FROM [dbo].[tbParticipantes] p " +
                    "  JOIN dbo.tbClientesEventos e on p.cdEvento = e.cdEvento " +
                    "  join dbo.TbCategorias c on p.cdEvento = c.cdEvento and p.cdCategoria = c.cdCategoria " +
                    "  where p.cdEvento = '001301_1' " +
                    "  and p.cdCredencial <> '0' " +
                    "  and p.flAtivo = 1 " +
                    "  and c.flCertificado = 1 " +
                    "  and p.nuCPFCNPJ = '" + txtCPF.Text.Replace(".","").Replace("-","") + "' " +

                    "UNION " +

                    "SELECT year(e.dtInicioEvento) ano, e.noEvento, e.cdEVento, e.dsEvento, p.cdParticipante, p.noParticipante, c.noCategoria " +
                    "  FROM [dbo].[tbParticipantes] p " +
                    "  JOIN dbo.tbClientesEventos e on p.cdEvento = e.cdEvento " +
                    "  join dbo.TbCategorias c on p.cdEvento = c.cdEvento and p.cdCategoria = c.cdCategoria " +
                    "  where p.cdEvento = '001302_2' " +
                    "  and p.cdCredencial <> '0' " +
                    "  and p.flAtivo = 1 " +
                    "  and c.flCertificado = 1 " +
                    "  and p.nuCPFCNPJ = '" + txtCPF.Text.Replace(".","").Replace("-","") + "' " +

                    "UNION " +

                    "SELECT year(e.dtInicioEvento) ano, e.noEvento, e.cdEVento, e.dsEvento, p.cdParticipante, p.noParticipante, c.noCategoria " +
                    "  FROM [dbo].[tbParticipantes] p " +
                    "  JOIN dbo.tbClientesEventos e on p.cdEvento = e.cdEvento " +
                    "  join dbo.TbCategorias c on p.cdEvento = c.cdEvento and p.cdCategoria = c.cdCategoria " +
                    "  where p.cdEvento = '001303' " +
                    "  and p.cdCredencial <> '0' " +
                    "  and p.flAtivo = 1 " +
                    "  and c.flCertificado = 1 " +
                    "  and p.nuCPFCNPJ = '" + txtCPF.Text.Replace(".","").Replace("-","") + "' " +

                    "UNION " +

                    "SELECT year(e.dtInicioEvento) ano, e.noEvento, e.cdEVento, e.dsEvento, p.cdParticipante, p.noParticipante, c.noCategoria " +
                    "  FROM [dbo].[tbParticipantes] p " +
                    "  JOIN dbo.tbClientesEventos e on p.cdEvento = e.cdEvento " +
                    "  join dbo.TbCategorias c on p.cdEvento = c.cdEvento and p.cdCategoria = c.cdCategoria " +
                    "  where p.cdEvento = '001304' " +
                    "  and p.cdCredencial <> '0' " +
                    "  and p.flAtivo = 1 " +
                    "  and c.flCertificado = 1 " +
                    "  and p.nuCPFCNPJ = '" + txtCPF.Text.Replace(".","").Replace("-","") + "' " +

                    "UNION " +

                    "SELECT year(e.dtInicioEvento) ano, e.noEvento, e.cdEVento, e.dsEvento, p.cdParticipante, p.noParticipante, c.noCategoria " +
                    "  FROM [dbo].[tbParticipantes] p " +
                    "  JOIN dbo.tbClientesEventos e on p.cdEvento = e.cdEvento " +
                    "  join dbo.TbCategorias c on p.cdEvento = c.cdEvento and p.cdCategoria = c.cdCategoria " +
                    "  where p.cdEvento = '001305' " +
                    "  and p.cdCredencial <> '0' " +
                    "  and p.flAtivo = 1 " +
                    "  and c.flCertificado = 1 " +
                    "  and p.nuCPFCNPJ = '" + txtCPF.Text.Replace(".","").Replace("-","") + "' " +

                    "order by 1";                    
                                       
                    

                SqlCommand comando = new SqlCommand(
                     cmd, SessionCnn);


                Dap = new SqlDataAdapter(comando);

                Dap.TableMappings.Add("Certificados", "certificados");
                Dap.Fill(DT);

                SessionCnn.Close();

                //BindingSource bsufs = new BindingSource();

                DataTable oDataTable = new DataTable();



                oDataTable.Columns.Add("cdEvento");
                oDataTable.Columns.Add("noEvento");
                oDataTable.Columns.Add("cdParticipante");
                oDataTable.Columns.Add("noParticipante");
                oDataTable.Columns.Add("dsLinkCertificado");
                oDataTable.Columns.Add("noCategoria");



                if (DT != null)
                {
                    for (int i = 0; i < DT.DefaultView.Count; i++)
                    {
                        oDataTable.Rows.Add(
                            DT.DefaultView[i]["cdEVento"],
                            DT.DefaultView[i]["ano"] + " - " + DT.DefaultView[i]["noEvento"] + " - " + DT.DefaultView[i]["dsEvento"],
                            DT.DefaultView[i]["cdParticipante"],
                            DT.DefaultView[i]["noParticipante"],
                            "frmCertificadoGeral.aspx?e="+cllEventos.Crypto.EncryptStringAES(DT.DefaultView[i]["cdEVento"].ToString())+"&p="+cllEventos.Crypto.EncryptStringAES(DT.DefaultView[i]["cdParticipante"].ToString()),
                            DT.DefaultView[i]["noCategoria"]
                            );
                    }

                    return oDataTable;
                }
                else
                    return DT;

                               

            }
            catch (Exception Ex)
            {
                SessionCnn.Close();
                //_erroNoCampo = true;
                lblMsg.Visible = true;
                lblMsg.Text = "Erro ao selecionar Certificados!\n" + Ex.Message;
                return null;
            }
        }
        finally
        {
            SessionCnn.Close();
        }


    }

    public DataTable ListarCertificados(string prmEvento, string prmGrupo, string prmCategoria)
    {

        //if (prmData == "")
        //    return;

        lblMsg.Visible = false;
        if (SessionCnn == null)
        {
            //_erroNoCampo = true;
            lblMsg.Visible = true;
            lblMsg.Text = "Conexão inválida ou inexistente";
            return null;
        }
        if (SessionCnn.State != ConnectionState.Open)
        {
            try
            {
                SessionCnn.Open();
            }
            catch
            {
                //_erroNoCampo = true;
                lblMsg.Visible = true;
                lblMsg.Text = "Conexão inválida";
                return null;
            }
        }

        DataTable DT = new DataTable();
        SqlDataAdapter Dap;
        try
        {
            try
            {
                string cmd =
                    "SELECT p.[cdEvento] " +
                    "      ,[cdParticipante]	   " +
                    "      ,[noInstituicao] " +
                    "      ,[dsAuxiliar8] NrCertificado  " +
                    "      ,[noCategoria] " +//,case when (p.cdCategoria in ('00056501','00056502')) then c.[noCategoria] else '' end [noCategoria] " +
                    "      ,[noParticipante]    " +
                    "	  ,coalesce((select top 1 convert(varchar,dtEntrega,103) + ' ' + convert(varchar,dtEntrega,108) dtEntrega  from tbEntregaCertificados where cdEvento = p.cdEvento and cdParticipante = p.cdParticipante),'') dtEntrega " +
                    "  FROM tbParticipantes p " +
                    "  join tbCategorias c on p.cdEvento = c.cdEvento and p.cdCategoria = c.cdCategoria " +
                    "  where p.cdEVENTO = '" + prmEvento + "' " +
                    "  and cdCredencial <> '0' " +
                    "  and dsAuxiliar19  like  '%" + prmGrupo + "%' " + 
                    "  and c.flCertificado = '1' " +
                    "  and p.cdCategoria like '%" + prmCategoria + "%' "+
                    "  order by noParticipante";



                SqlCommand comando = new SqlCommand(
                     cmd, SessionCnn);


                Dap = new SqlDataAdapter(comando);

                Dap.TableMappings.Add("Certificados", "certificados");
                Dap.Fill(DT);

                SessionCnn.Close();

                //BindingSource bsufs = new BindingSource();

                DataTable oDataTable = new DataTable();



                oDataTable.Columns.Add("cdEvento");
                oDataTable.Columns.Add("cdParticipante");
                oDataTable.Columns.Add("noInstituicao");
                oDataTable.Columns.Add("noCategoria");
                oDataTable.Columns.Add("NrCertificado");
                oDataTable.Columns.Add("noParticipante");
                oDataTable.Columns.Add("dsLinkCertificado");
                oDataTable.Columns.Add("dtEntrega");

              
                //lblTotReg.Text = "0";

                if (DT != null)
                {
                    for (int i = 0; i < DT.DefaultView.Count; i++)
                    {
                        oDataTable.Rows.Add(
                            DT.DefaultView[i]["cdEVento"],
                            DT.DefaultView[i]["cdParticipante"],
                            DT.DefaultView[i]["noInstituicao"],
                            DT.DefaultView[i]["noCategoria"],
                            DT.DefaultView[i]["NrCertificado"],
                            DT.DefaultView[i]["noParticipante"],
                            "frmCertificado.aspx?e=" + cllEventos.Crypto.EncryptStringAES(DT.DefaultView[i]["cdEVento"].ToString()) + "&p=" + cllEventos.Crypto.EncryptStringAES(DT.DefaultView[i]["cdParticipante"].ToString()),
                            DT.DefaultView[i]["dtEntrega"]
                            );
                    }

                    DataList1.DataSource = oDataTable;
                    DataList1.DataBind();

                    //lblTotReg.Text = oDataTable.DefaultView.Count.ToString();
                    
                    return oDataTable;
                }
                else
                    return DT;



            }
            catch (Exception Ex)
            {
                //_erroNoCampo = true;
                lblMsg.Visible = true;
                lblMsg.Text = "Erro ao selecionar Certificados!\n" + Ex.Message;
                SessionCnn.Close();
                return null;
            }
        }
        finally
        {
            SessionCnn.Close();
        }


    }

    protected string ValidarCPF(string prmCdParticipante, string prmCdEvento, string prmCPF, string prmCdCategoria, SqlConnection prmCnn)
    {
        if ((!oClsFuncoes.CPFCNPJValidar(prmCPF)))
        {
            CategoriaCad oCategoriaCad = new CategoriaCad();
            Categoria oCategoria = oCategoriaCad.Pesquisar(prmCdEvento, prmCdCategoria, prmCnn);
            if (((SessionEvento != null) && (SessionEvento.FlLiberarCPFCoringa)))// || (!oCategoria.FlCPFCNPJObrigatorio))
                return "";

            return "CPF Inválido!";
        }
        else
            return "";
        //{
        //    string tmpCPF = prmCPF.Replace(".", "").Replace("-", "");

        //    /*if ((tmpCPF == "11111111111") ||
        //        (tmpCPF == "22222222222") ||
        //        (tmpCPF == "33333333333") ||
        //        (tmpCPF == "44444444444") ||
        //        (tmpCPF == "55555555555") ||
        //        (tmpCPF == "66666666666") ||
        //        (tmpCPF == "77777777777") ||
        //        (tmpCPF == "88888888888") ||
        //        (tmpCPF == "99999999999") ||
        //        (tmpCPF == "00000000000"))
        //    {
        //        CategoriaCad oCategoriaCad = new CategoriaCad();
        //        Categoria oCategoria = oCategoriaCad.Pesquisar(prmCdEvento, prmCdCategoria, prmCnn);
        //        if (((SessionEvento != null) && (SessionEvento.FlLiberarCPFCoringa)) || (!oCategoria.FlCPFCNPJObrigatorio) || (oCategoria.FlLiberarCPFCNPJCoringa))
        //            return "";
        //        return "CPF Inválido!";
        //    }
        //    else
        //    {*/
        //    return oParticipanteCad.verificarDuplicidadeCPF(prmCdParticipante, prmCdEvento, tmpCPF, prmCdCategoria, prmCnn);
        //    //}

        //}

    }

    protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
    {

        if (txtCPF.Text != "")
        {

            args.IsValid = true;

        }
        else
        {

            args.IsValid = false;

        }
    }
}