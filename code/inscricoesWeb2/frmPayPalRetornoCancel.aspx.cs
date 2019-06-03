using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using cllEventos;
using CLLFuncoes;

using Itaucripto;

using System.Data;
using System.Data.SqlClient;

using System.IO;

using AjaxControlToolkit;

using System.Collections.Specialized;
using PayPal;
using PayPal.Api;


//public partial class frmPayPalRetornoCancel : BaseSamplePage
//{
//    public RequestFlow Flow { get; set; }

//    protected override void RunSample()
//    {
//        if (!IsPostBack)
//        {
//            Configuration.ClientId = "AQlRK1YGl4mDJlzkFFc2BXR2gVUA67zZRhajEblJMy3JUYlO9itL6DbG8lpRcd1sOw_TnOHA7qibcBht";
//            Configuration.ClientSecret = "EOa754PZZlqLIuW1a5sdrWXZGUnaDEggMSRoRsTW_VQMlav_H-o6EqYq-Ji9QjDpRL0AAeNm4ZrdJVsC";

//            var apiContext = Configuration.GetAPIContext();

//            Specify a Payment ID to retrieve.  For demonstration purposes, we'll be using a previously-executed payment that used a PayPal account.
//            var guid = Request.Params["guid"];
//            var paymentId = Session[guid] as string;
//             ^ Ignore workflow code segment
//            #region Track Workflow
//            this.flow.AddNewRequest("Get payment details", description: "ID: " + paymentId);
//            #endregion

//            Retrieve the details of the payment.
//            var payment = Payment.Get(apiContext, paymentId);

//             ^ Ignore workflow code segment
//            #region Track Workflow
//            this.flow.RecordResponse(payment);
//            #endregion
//        }
//    }

//    / <summary>
//    / 
//    / </summary>
//    / <param name = "text" ></ param >
//    / < param name="isRequest"></param>
//    / <returns></returns>
//    protected string FormatPayloadText(string text, bool isRequest)
//    {
//        if (string.IsNullOrEmpty(text))
//        {
//            return string.Format("No payload for this {0}.", (isRequest ? "request" : "response"));
//        }

//        return text;
//    }

//    / <summary>
//    / Gets the CSS class for the specified message.
//    / </summary>
//    / <param name = "message" ></ param >
//    / < returns ></ returns >
//    protected string GetMessageClass(RequestFlowItemMessage message)
//    {
//        switch (message.Type)
//        {
//            case RequestFlowItemMessageType.Error:
//                return "error";
//            case RequestFlowItemMessageType.Success:
//                return "success";
//            default:
//                return string.Empty;
//        }
//    }

//    / <summary>
//    / Formats the message to include an accompanying icon from Font Awesome(http://fortawesome.github.io/Font-Awesome/).
//    / </summary>
//    / <param name = "message" ></ param >
//    / < returns ></ returns >
//    protected string GetMessageWithMarkup(RequestFlowItemMessage message)
//    {
//        var iconText = "";
//        switch (message.Type)
//        {
//            case RequestFlowItemMessageType.Error:
//                iconText = "<i class=\"fa fa-times-circle\"></i>";
//                break;

//            case RequestFlowItemMessageType.Success:
//                iconText = "<i class=\"fa fa-check-circle\"></i>";
//                break;

//            case RequestFlowItemMessageType.General:
//                iconText = "<i class=\"fa fa-info-circle\"></i>";
//                break;
//        }

//        return string.Format("{0} {1}", iconText, message.Message);
//    }

//    / <summary>
//    / 
//    / </summary>
//    / <param name = "key" ></ param >
//    / < returns ></ returns >
//    private string GetStringFromContext(string key)
//    {
//        return this.GetFromContext<string>(key);
//    }

//    / <summary>
//    / 
//    / </summary>
//    / <typeparam name = "T" ></ typeparam >
//    / < param name="key"></param>
//    / <returns></returns>
//    private T GetFromContext<T>(string key)
//    {
//        if (HttpContext.Current.Items.Contains(key))
//        {
//            return (T)HttpContext.Current.Items[key];
//        }

//        return default(T);
//    }

//}

public partial class frmPayPalRetornoCancel : System.Web.UI.Page
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
    /// Quando o cliente for redirecionado de volta para a loja pelo PayPal, ele cairá nessa página,
    /// onde informaremos que não possível concluir a operação
    /// pelo PayPal.
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

            SessionParticipante = (Participante)Session["SessionParticipante"];
            if (SessionParticipante == null)
            {
                if ((Request.QueryString["p"] != null) &&
                    (Request.QueryString["p"] != ""))
                {
                    string cd_pedido = cllEventos.Crypto.DecryptStringAES(Request["p"].ToString());

                    SessionPedido = oPedidoCad.Pesquisar(SessionEvento.CdEvento, cd_pedido, SessionCnn);
                    Session["SessionPedido"] = SessionPedido;
                    if (SessionPedido == null)
                    {
                        return;
                        //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                        //                 "04",
                        //                 ""), true);

                    }
                }
            }

            SessionPedido = (Pedido)Session["SessionPedido"];
            if (SessionPedido == null)
            {
                if ((Request.QueryString["pd"] != null) &&
                    (Request.QueryString["pd"] != ""))
                {
                    string cd_pedido = Request.QueryString["pd"].ToString();// cllEventos.Crypto.DecryptStringAES(Request["p"].ToString());

                    SessionPedido = oPedidoCad.Pesquisar(SessionEvento.CdEvento, cd_pedido, SessionCnn);
                    Session["SessionPedido"] = SessionPedido;
                    if (SessionPedido == null)
                    {
                        return;
                        //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                        //                 "04",
                        //                 ""), true);

                    }
                }
            }

            Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                "031",
                                ""), true);


        }
        else
        {
            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            SessionPedido = (Pedido)Session["SessionPedido"];

        }


    }



}