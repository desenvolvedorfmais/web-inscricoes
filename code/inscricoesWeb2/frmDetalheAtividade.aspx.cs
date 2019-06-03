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

using MSXML2;

using System.Xml;

public partial class frmDetalheAtividade : System.Web.UI.Page
{
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    Atividade sAtividade;
    AtividadeCad oAtividadeCad = new AtividadeCad();

    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    SqlConnection SessionCnn;

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.NameValueCollection UserInfoCookieCollection;

        UserInfoCookieCollection = Request.Cookies["EventoInfo"].Values;
        string codEvento = Server.HtmlEncode(UserInfoCookieCollection["eventoCod"]);
        string codLng = Server.HtmlEncode(UserInfoCookieCollection["lngCod"]);
        string codCateg = Server.HtmlEncode(UserInfoCookieCollection["categCod"]);
        string codAtividade = Server.HtmlEncode(UserInfoCookieCollection["atividadeCod"]);
        string tpSistema = Server.HtmlEncode(UserInfoCookieCollection["tpSistema"]);

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

            if (SessionEvento == null)
            {
                Response.Redirect("frmProgramacao.aspx?codEvento=" + cllEventos.Crypto.EncryptStringAES(codEvento) + "&cdLng=PTBR", false);
                //Response.Redirect("../frmSessaoExpirada.aspx");
                return;
            }


            if ((Request["cdAtvDet"] != null) &&
                (Request["cdAtvDet"] != ""))
            {
                sAtividade = oAtividadeCad.Pesquisar(SessionEvento.CdEvento, Request["cdAtvDet"], SessionCnn);


                if (sAtividade != null)
                {
                    Session["sAtividade"] = sAtividade;
                    
                    //top
                    lbltipo.Text = sAtividade.NoTipoAtividade;

                    lblAtividade.Text = sAtividade.NoTitulo + sAtividade.NoTituloWEB;
                }
            }
            
        }
        else
        {


            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            sAtividade = (Atividade)Session["sAtividade"];

            if (SessionEvento == null)
                Response.Redirect("frmProgramacao.aspx?codEvento=" + cllEventos.Crypto.EncryptStringAES(codEvento) + "&cdLng=PTBR", false);
                //Response.Redirect("../frmSessaoExpirada.aspx");
        }
    }
    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        Session["SessionEvento"] = SessionEvento;
        Response.Redirect("frmProgramacao.aspx?codEvento="+ cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento) +"&cdLng=PTBR", false);
        //Server.Transfer("frmProgramacao.aspx", true);
    }
}