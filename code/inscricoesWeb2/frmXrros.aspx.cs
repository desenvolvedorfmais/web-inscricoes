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

using System.Data.SqlClient;

public partial class frmXrros : System.Web.UI.Page
{
    Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();
    Evento SessionEvento;
   // EventoCad oEventoCad = new EventoCad();


    SqlConnection SessionCnn;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (SessionCnn == null)
                SessionCnn = (SqlConnection)Session["SessionCnn"];
            else
                Session["SessionCnn"] = SessionCnn;

            if (SessionParticipante == null)
                SessionParticipante = (Participante)Session["SessionParticipante"];
            else
                Session["SessionParticipante"] = SessionParticipante;


            if (SessionEvento == null)
                SessionEvento = (Evento)Session["SessionEvento"];
            else
                Session["SessionEvento"] = SessionEvento;

            
            
            Button1.Visible = false;

            
        }
        else
        {
            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //Server.Transfer(string.Format("frmCadastroAuto.aspx?cdMatricula={0}",
        //                                   SessionParticipante.CdParticipante), true);
        //Server.Transfer(string.Format("frmCadastroAuto.aspx"), true);

        //Response.Write("<script>window.open('frmCadastroAuto.aspx?cdMatricula=" + SessionParticipante.CdParticipante + "','_self');</script>");
        //Response.Write("<script>window.open('frmCadastroAuto.aspx','_self');</script>");
    }
}
