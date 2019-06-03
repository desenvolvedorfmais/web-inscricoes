using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Collections;

using MSXML2;

using cllEventos;

//using System.Security.Cryptography;
using System.Text;

using System.Data.SqlClient;


public partial class EnviarSMS : System.Web.UI.Page 
{
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    SqlConnection SessionCnn;

    protected void Page_Load(object sender, EventArgs e)
    {


        if (!Page.IsPostBack)
        {

            string tmpCelular = "";
            if ((Request["Celular"] == null) ||
                (Request["Celular"].ToString().Trim() == ""))
            {
                return;
            }
            else
                tmpCelular = Request["Celular"].ToString().Trim();


            string tmpMensagem = "";
            if ((Request["Mensagem"] == null) ||
                (Request["Mensagem"].ToString().Trim() == ""))
            {
                return;
            }
            else
                tmpMensagem = Request["Mensagem"].ToString().Trim();

            string tmpRementente = "";
            if ((Request["Remetente"] == null) ||
                (Request["Remetente"].ToString() == ""))
            {
                return;
            }
            else
                tmpRementente = Request["Remetente"].ToString();


            //SessionCnn = new SqlConnection("Data Source=187.45.196.23;Initial Catalog=nv2systems3;Persist Security Info=True;User ID=nv2systems3;Password=Fzd0M@15");
            //SessionCnn = new SqlConnection("Data Source=dbsq0012.whservidor.com;Initial Catalog=fazendomai2;Persist Security Info=True;User ID=fazendomai2;Password=Qu454rFm");
            //SessionCnn = new SqlConnection("Data Source=krksa-pc;Initial Catalog=dbEventos_FM;Persist Security Info=True;User ID=sa;Password=krksa171");
            //SessionCnn = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=dbEventos_FM;Persist Security Info=True;User ID=sa;Password=RaC982973");
            SessionCnn = new SqlConnection("Data Source=189.38.95.72;Initial Catalog=fazendomais;Persist Security Info=True;User ID=fazendomais;Password=Qu454rFm");



            //Session["SessionCnn"] = SessionCnn;


            if (SessionCnn == null)
                SessionCnn = (SqlConnection)Session["SessionCnn"];
            else
                Session["SessionCnn"] = SessionCnn;

            string cdEvento = "";
            if ((Request["codEvento"] != null) &&
                (Request["codEvento"].ToString().Trim().ToUpper() != ""))
            {
                cdEvento = cllEventos.Crypto.DecryptStringAES(Request["codEvento"]);
            }
            if ((cdEvento.Trim() == "") || (cdEvento.Trim().Length != 6))
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Evento não encontrado!'); </script>", false);
                return;
                //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}",
                //                "003"), true);
            }
            SessionEvento = oEventoCad.Pesquisar(cdEvento.Substring(0, 4), cdEvento, SessionCnn);
            Session["SessionEvento"] = SessionEvento;
            if (SessionEvento == null)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Evento não encontrado!'); </script>", false);
                return;
                //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                //                "003",
                //                oEventoCad.RcMsg + cdEvento.Substring(0, 4)), true);
            }

            if ((SessionEvento.DtFinalEvento == null) ||
                (SessionEvento.DtFinalEvento < DateTime.Parse(DateTime.Today.ToString("dd/MM/yyyy"))))
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Evento encerrado!'); </script>", false);
                return;
                //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                //                "006",
                //                oEventoCad.RcMsg + cdEvento.Substring(0, 4)), true);
            }

            if (!verificarTotalSMSEnviado(cdEvento, SessionCnn))
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Atenção", "<script type='text/javascript'> alert('Limite de envio de SMS esgotado!'); </script>", false);
                return;
               
            }

            XMLHTTP http = new XMLHTTP();
            //http.open("GET", "http://fazendomais.com.br/teste/sendmail.aspx", false, null, null); 
            http.open("GET", "http://www.drsac.com.br/upload/SmsGravaEnviaAuto.asp?Celular=" + tmpCelular + "&Mensagem=" + tmpMensagem + "&Usuario=Fazendo&Senha=Fazendo&SubGrupo=" + tmpRementente, false, null, null);
            http.send(null);

            IncrementarEnvio(cdEvento, SessionCnn);
        }
    }

    public int IncrementarEnvio(
            String prmcdEvento,
             SqlConnection prmCnn)
    {


        if (prmCnn == null)
        {
            //lblMsg.Text = "Conexão inválida ou inexistente";
            return -1;
        }
        if (prmCnn.State != ConnectionState.Open)
        {
            try
            {
                prmCnn.Open();
            }
            catch
            {
                //lblMsg.Text = "Conexão inválida";
                return -1;
            }
        }

        // DataTable DTCampanhaSMSEnviado = new DataTable();
        //SqlDataAdapter Dap;
        try
        {
            try
            {
                string comandoSQL =
                   "UPDATE [dbo].[tmpEnvioSMS] " +
                   "   SET [vlQtdEnviado] = vlQtdEnviado + 1 " +
                   " WHERE [cdEvento] = '" + prmcdEvento + "'";
                SqlCommand comando = new SqlCommand(
                    comandoSQL, prmCnn);
                comando.ExecuteNonQuery();

                return 1;

            }
            catch (Exception ex)
            {
                //lblMsg.Text = "Erro ao selecionar Participantes!\n" + ex.Message;
                return -1;
            }




        }
        finally
        {
            //   prmCnn.Close();
        }
    }

    public bool verificarTotalSMSEnviado(
        String prmcdEvento,
            SqlConnection prmCnn)
    {
        //--- verifica se já enviado o sms da campanha par o participante
        //--- se sim retorna false não true

        //if (prmCdEvento.Trim() == "")
        //{

        //    lblMsg.Text = "Informe o clliente";
        //    return null;
        //}

        if (prmCnn == null)
        {
            //lblMsg.Text = "Conexão inválida ou inexistente";
            return false;
        }
        if (prmCnn.State != ConnectionState.Open)
        {
            try
            {
                prmCnn.Open();
            }
            catch
            {
                //lblMsg.Text = "Conexão inválida";
                return false;
            }
        }

        DataTable DTCampanhaSMSEnviado = new DataTable();
        SqlDataAdapter Dap;
        try
        {
            try
            {
                string comandoSQL =
                   "SELECT vlQtdEnvio, vlQtdEnviado " +
                     " FROM [dbo].[tmpEnvioSMS] " +
                    "where [cdEvento] = '" + prmcdEvento + "'";

                SqlCommand comando = new SqlCommand(
                    comandoSQL, prmCnn);

                //lblMsg.Text = comandoSQL;
                Dap = new SqlDataAdapter(comando);

                Dap.TableMappings.Add("CampnhaSMSEnviado", "tbCampnhaSMSEnviado");
                Dap.Fill(DTCampanhaSMSEnviado);

                if ((DTCampanhaSMSEnviado != null) && (DTCampanhaSMSEnviado.Rows.Count > 0))
                {
                    if (int.Parse(DTCampanhaSMSEnviado.DefaultView[0]["vlQtdEnvio"].ToString()) > int.Parse(DTCampanhaSMSEnviado.DefaultView[0]["vlQtdEnviado"].ToString()))
                    {
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;//ainda não foi enviado sms




            }
            catch (Exception ex)
            {
                //lblMsg.Text = "Erro ao selecionar Participantes!\n" + ex.Message;
                return false;
            }




        }
        finally
        {
            //   prmCnn.Close();
        }
    }
}
