using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using cllEventos;
using CLLFuncoes;


using System.Data;
using System.Data.SqlClient;

using System.IO;

using AjaxControlToolkit;

using Uol.PagSeguro;


public partial class frmPagSeguroNotificacoes : System.Web.UI.Page
{

    Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();
    SqlConnection SessionCnn;

    Pedido SessionPedido;
    PedidoCad oPedidoCad = new PedidoCad();

    String SessionIdioma;

    /// <summary>
    /// Quando o cliente for redirecionado de volta para a loja pelo PagSeguro, ele cairá nessa página,
    /// onde será verificado o status da transação 

    /// </summary>
    /// <param name='sender'>
    /// Sender.
    /// </param>
    /// <param name='e'>
    /// E.
    /// </param>
    protected void Page_Load(object sender, EventArgs e)
    {
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

            SessionIdioma = (String)Session["SessionIdioma"];
            if (SessionIdioma == null)
                SessionIdioma = "PTBR";
            Session["SessionIdioma"] = SessionIdioma;

            SessionEvento = (Evento)Session["SessionEvento"];
            if (SessionEvento == null)
            {
                if ((Request.QueryString["e"] != null) &&
                    (Request.QueryString["e"] != ""))
                {
                    string cd_Evento = Request.QueryString["e"].ToString();// cllEventos.Crypto.DecryptStringAES(Request.QueryString["e"].ToString());

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

            //string notificationCode = Request.QueryString["transaction_id"];
            string notificationCode = Request.Form["notificationCode"]; 

            AccountCredentials credentials = new AccountCredentials(
                    SessionEvento.pagSeguroConfig.ContaPS,
                    SessionEvento.pagSeguroConfig.CodAcessoPS
                );

            // string notificationCode = "45FDA1-ED1181118196-0334CF2FB2BA-EB54DE";
            Transaction transaction = NotificationService.CheckTransaction(
                credentials,
                notificationCode
            );

            SessionPedido = (Pedido)Session["SessionPedido"];

            SessionParticipante = (Participante)Session["SessionParticipante"];

            if (SessionPedido == null)
            {
                //if ((Request.QueryString["referencia"] != null) &&
                //    (Request.QueryString["referencia"] != ""))
                //{
                    //string cd_pedido = Request.QueryString["referencia"].ToString();

                    SessionPedido = oPedidoCad.Pesquisar(SessionEvento.CdEvento, transaction.Reference, SessionCnn);
                    Session["SessionPedido"] = SessionPedido;
                    if (SessionPedido == null)
                    {
                        return;
                        //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                        //                 "04",
                        //                 ""), true);

                    }

                    //if (SessionEvento == null)
                    //{
                    //    SessionEvento = oEventoCad.Pesquisar(SessionPedido.CdEvento, SessionCnn);
                    //    Session["SessionEvento"] = SessionEvento;
                    //    if (SessionEvento == null)
                    //    {
                    //        return;
                    //        //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                    //        //            "003",
                    //        //            ""), true);

                    //    }
                    //}





                    if (SessionParticipante == null)
                    {
                        SessionParticipante = oParticipanteCad.Pesquisar(SessionEvento.CdEvento, SessionPedido.CdParticipante, SessionCnn);
                        Session["SessionParticipante"] = SessionParticipante;
                        if (SessionParticipante == null)
                        {
                            return;
                            //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                            //                 "04",
                            //                 ""), true);

                        }

                    }
                //}
            }

                       

            
            
            if (!SessionPedido.FlPago)
            {               

                if ((transaction.TransactionStatus == 3) || (transaction.TransactionStatus == 4))
                {//transação confirmada e/ou recebida

                    SessionPedido.TpPagamento = "PAGSEGURO";
                    SessionPedido.CdTransacaoOutrosTpPgto = transaction.Code;

                    SessionPedido = oPedidoCad.Gravar(SessionPedido, SessionCnn);
                    Session["SessionPedido"] = SessionPedido;


                    oPedidoCad.MarcarPedidoComoPago(SessionPedido, SessionCnn);


                    ReciboCad oReciboCad = new ReciboCad();
                    Recibo oRecibo = oReciboCad.Gravar(
                               SessionEvento.CdEvento,
                               SessionParticipante.CdParticipante,
                               SessionPedido.CdPedido,
                               "",
                               SessionPedido.VlTotalPedido,
                               0,
                               0,
                               SessionPedido.TpPagamento,
                               "PSTrans.ID: " + transaction.Code,
                               "Transação realizada pelo Pagseguro no dia " + transaction.Date.ToString("dd/MM/yyyy hh:mm") + " Nome: " + transaction.Sender.Name + "PSTrans.ID:" + transaction.Code,
                               "000000001",
                               "",
                               SessionPedido.VlTotalPedido,
                               "",
                               0,
                               "",
                               SessionCnn);
                                        
                    oGeral.EnviarEmailConfirmaCadastro(SessionEvento, SessionParticipante, SessionCnn);

                    //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                    //                "014",
                    //                ""), true);
                }
                else if ((transaction.TransactionStatus == 1) || (transaction.TransactionStatus == 2))
                {
                    oGeral.EnviarEmailAvulso(
                        SessionEvento, 
                        SessionParticipante.NoParticipante, 
                        SessionParticipante.DsEmail,
                        "Processamos sua solicitação de inscrição! <br />" +
                                "Estamos aguardando informações do pagamento pelo PagSeguro.<br/>" +
                                "Solcitamos que acompanhe as notificações enviadas ao se e-mail sobre o andamento do processo de confirmação do pagamento.<br/><br>", 
                        SessionCnn);
                }
                else if (transaction.TransactionStatus == 7) 
                {
                    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                    "03300,0",
                                    ""), true);
                }

            }           
             
        }
        else
        {
            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            SessionPedido = (Pedido)Session["SessionPedido"];

        }
        
        
    }

    /// <summary>
    /// Aqui uma pequena demonstração de como obter os dados do NVP.
    /// </summary>
    /// <param name='nvp'>
    /// Nvp.
    /// </param>
    /*
    private void createAndPopulateTable(NameValueCollection nvp)
    {
        Table table;

        table = createTableWithCaption("Dados do Cliente");

        createTableRowWithNameAndValue(table, "Nome do cliente", nvp["FIRSTNAME"] + " " + nvp["LASTNAME"]);
        createTableRowWithNameAndValue(table, "Email", nvp["EMAIL"]);
        createTableRowWithNameAndValue(table, "ID do cliente no PayPal", nvp["PAYERID"]);

        //table = createTableWithCaption("Dados de Entrega");

        //createTableRowWithNameAndValue(table, "Entregar para", nvp["PAYMENTREQUEST_0_SHIPTONAME"]);
        //createTableRowWithNameAndValue(table, "Endereço", nvp["PAYMENTREQUEST_0_SHIPTOSTREET"]);
        //createTableRowWithNameAndValue(table, "Cidade", nvp["PAYMENTREQUEST_0_SHIPTOCITY"]);
        //createTableRowWithNameAndValue(table, "Estado", nvp["PAYMENTREQUEST_0_SHIPTOSTATE"]);
        //createTableRowWithNameAndValue(table, "CEP", nvp["PAYMENTREQUEST_0_SHIPTOZIP"]);
        //createTableRowWithNameAndValue(table, "País", nvp["PAYMENTREQUEST_0_SHIPTOCOUNTRYNAME"]);
    }
    */
    private Table createTableWithCaption(string caption)
    {
        Table table = new Table();

        TableHeaderRow captionRow = new TableHeaderRow();
        TableHeaderCell captionCell = new TableHeaderCell();

        captionCell.Text = caption;
        captionCell.HorizontalAlign = HorizontalAlign.Left;
        captionCell.ColumnSpan = 2;

        captionRow.Cells.Add(captionCell);

        table.Rows.Add(captionRow);

        data.Controls.Add(table);

        return table;
    }

    private void createTableRowWithNameAndValue(Table table, string name, string value)
    {
        TableRow row = new TableRow();
        TableCell nameCell = new TableCell();
        TableCell valueCell = new TableCell();

        nameCell.Text = name;
        nameCell.Width = 300;

        valueCell.Text = value;
        valueCell.Width = 400;

        row.Cells.Add(nameCell);
        row.Cells.Add(valueCell);

        table.Rows.Add(row);
    }
}