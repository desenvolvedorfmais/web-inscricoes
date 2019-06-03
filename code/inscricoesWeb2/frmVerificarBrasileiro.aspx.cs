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

public partial class frmVerificarBrasileiro : System.Web.UI.Page
{
    Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();

    CategoriaCad oCategoriaCad = new CategoriaCad();

    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    SqlConnection SessionCnn;

    String SessionIdioma;

    String SessionCateg;

    String SessionAtv;

    String SessionChaveLibercao;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            
            if (SessionCnn == null)
                SessionCnn = (SqlConnection)Session["SessionCnn"];
            else
                Session["SessionCnn"] = SessionCnn;

            SessionIdioma = (String)Session["SessionIdioma"];
            if (SessionIdioma == null)
                SessionIdioma = "PTBR";
            Session["SessionIdioma"] = SessionIdioma;

            SessionCateg = (String)Session["SessionCateg"];
            if (SessionCateg == null)
                SessionCateg = "";
            Session["SessionCateg"] = SessionCateg;

            SessionAtv = (String)Session["SessionAtv"];
            if (SessionAtv == null)
                SessionAtv = "";
            Session["SessionAtv"] = SessionAtv;



            //verificarIdioma(SessionIdioma);

            if (SessionParticipante == null)
                SessionParticipante = (Participante)Session["SessionParticipante"];
            else
                Session["SessionParticipante"] = SessionParticipante;


            if (SessionEvento == null)
                SessionEvento = (Evento)Session["SessionEvento"];
            else
                Session["SessionEvento"] = SessionEvento;

           
            
            

        }
        else
        {
            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"]; 
            
            SessionIdioma = (String)Session["SessionIdioma"];

            SessionCateg = (String)Session["SessionCateg"];

            SessionChaveLibercao = (String)Session["SessionChaveLibercao"];

            SessionAtv = (String)Session["SessionAtv"];
        }

        //ToolkitScriptManager1.RegisterPostBackControl(Button1);

        if (SessionEvento == null)
            Server.Transfer("frmSessaoExpirada.aspx", true);
    }
    


    protected void Button1_Click1(object sender, EventArgs e)
    {
        Session["SessionPais"] = "BRASIL";
        Response.Write("<script>window.open('frmCadastroAuto.aspx','_self');</script>");
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Session["SessionPais"] = "";
        Response.Write("<script>window.open('frmCadastroAuto.aspx','_self');</script>");
    }
}