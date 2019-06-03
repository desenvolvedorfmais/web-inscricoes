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

public partial class frmTesesLista : System.Web.UI.Page
{
    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    SqlConnection SessionCnn;

    Participante SessionParticipante;
    //ParticipanteCad oParticipanteCad = new ParticipanteCad();
    Evento SessionEvento;
    //EventoCad oEventoCad = new EventoCad();

    //Tese SessionTese;
    TeseCad oTeseCad = new TeseCad();

    DataTable oDT = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
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
                Server.Transfer("frmSessaoExpirada.aspx", true);

            if (SessionParticipante == null)
                SessionParticipante = (Participante)Session["SessionParticipante"];
            else
                Session["SessionParticipante"] = SessionParticipante;
            
            //if (SessionTese == null)
            //    SessionTese = (Tese)Session["SessionTese"];
            //else
            //    Session["SessionTese"] = SessionTese;


            PesquisarTeses();

            
        }
        else
        {
            //SessionTese = (Tese)Session["SessionTese"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];


            if (SessionEvento == null)
                Server.Transfer("frmSessaoExpirada.aspx", true);

            PesquisarTeses();





        }
    }

    protected void PesquisarTeses()
    {
        grdTeses.DataSource = null;
        grdTeses.DataBind();


        oDT = oTeseCad.Listar(SessionParticipante.CdEvento, SessionParticipante.CdParticipante, SessionCnn);

        if (oDT == null)
        {
            return;
        }

        grdTeses.DataSource = oDT;
        grdTeses.DataBind();

        if (oDT.Rows.Count == 2)//(oTeseCad.TotalTesesInscrita(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn) <= 2)
            btnNovo.Visible = false;

    }
    protected void grdTeses_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Cells[0].Attributes.Add("onclick", "window.open('frmCadastrarTese.aspx?cdTese=" + e.Row.Cells[0].Text + "','_self');");
            e.Row.Cells[1].Attributes.Add("onclick", "window.open('frmCadastrarTese.aspx?cdTese=" + e.Row.Cells[0].Text + "','_self');");
            e.Row.Cells[2].Attributes.Add("onclick", "window.open('frmCadastrarTese.aspx?cdTese=" + e.Row.Cells[0].Text + "','_self');");
            e.Row.Cells[3].Attributes.Add("onclick", "window.open('frmCadastrarTese.aspx?cdTese=" + e.Row.Cells[0].Text + "','_self');");
            e.Row.Cells[4].Attributes.Add("onclick", "window.open('frmCadastrarTese.aspx?cdTese=" + e.Row.Cells[0].Text + "','_self');");
            //e.Row.Cells[4].Attributes.Add("onclick", "window.open('frmCadastrarHoteis.aspx?cdHotel=" + e.Row.Cells[0].Text + "','_self');");
            //e.Row.Cells[5].Attributes.Add("onclick", "window.open('frmCadastrarHoteis.aspx?cdHotel=" + e.Row.Cells[0].Text + "','_self');");

            //String temp = "this.style.backgroundColor='white'; this.style.color='#333333'";
            //if ((e.Row.DataItemIndex % 2) == 0)
            //{
            //    temp = "this.style.backgroundColor='#E8E8E8'; this.style.color='#333333'";
            //}

            //e.Row.Attributes.Add("style", "cursor:hand");
            //e.Row.Attributes.Add("onMouseOver", "this.style.backgroundColor='#409DD0'; this.style.color='white' ");
            //e.Row.Attributes.Add("onMouseOut", temp);


        }
    
    }
    protected void btnNovo_Click(object sender, EventArgs e)
    {

       
        Session["SessionTese"] = null;

        Server.Transfer("frmCadastrarTese.aspx", true);
    }
}