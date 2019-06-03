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


public partial class frmCredencial : System.Web.UI.Page
{
    Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    SqlConnection SessionCnn;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (SessionCnn == null)
                SessionCnn = (SqlConnection)Session["SessionCnn"];
            else
                Session["SessionCnn"] = SessionCnn;

            //SessionIdioma = (String)Session["SessionIdioma"];
            //if (SessionIdioma == null)
            //    SessionIdioma = "PTBR";
            //Session["SessionIdioma"] = SessionIdioma;

            if (SessionParticipante == null)
                SessionParticipante = (Participante)Session["SessionParticipante"];
            else
                Session["SessionParticipante"] = SessionParticipante;

            if (SessionEvento == null)
                SessionEvento = (Evento)Session["SessionEvento"];
            else
                Session["SessionEvento"] = SessionEvento;

            if (SessionEvento == null)
            {
                //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                //                "003",
                //                oEventoCad.RcMsg), true);

                Server.Transfer("frmSessaoExpirada.aspx", true);
            }



            lblIdentificador.Text = SessionParticipante.CdParticipante.Trim();
            lblNoParticipante.Text = SessionParticipante.NoParticipante.Trim();



            //Session["SessionCategoria"] = SessionCategoria;
            //if (SessionIdioma == "PTBR")
            lblCategoria.Text = SessionParticipante.Categoria.NoCategoria.Trim();
            //else if (SessionIdioma == "ENUS")
            //    lblCategoria.Text = SessionCategoria.NoCategoriaIngles.Trim();
            //else if (SessionIdioma == "ESP")
            //    lblCategoria.Text = SessionCategoria.NoCategoriaEspanhol.Trim();
            //else if (SessionIdioma == "FRA")
            //    lblCategoria.Text = SessionCategoria.NoCategoriaFrances.Trim();


            

        }
        else
        {
            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];


            //SessionIdioma = (String)Session["SessionIdioma"];

        }

        if (SessionEvento == null)
            Server.Transfer("frmSessaoExpirada.aspx", true);


        if (SessionParticipante.CdCredencial != "0")
        {
            lblMsg.Visible = true;
            lblMsg.Text = "Já foi gerada uma credencial para esta inscrição. Caso opte por gerar uma nova, lembre-se que a credencial anterior será invalidada no sistema.";
            //return;
        }

        TSManager1.RegisterPostBackControl(btnGerarCredencial);

    }
    protected void btnGerarCredencial_Click(object sender, EventArgs e)
    {
        lblMsg.Visible = false;

        if (SessionParticipante == null)
        {
            lblMsg.Visible = true;
            lblMsg.Text = "Informe um participante válido!";
            return;
        }

        if (SessionParticipante.Categoria.FlPagamento)
        {

            if ((!SessionParticipante.FlConfirmacaoInscricao) && (!oParticipanteCad.VerificarAtividadeObrigatoria(SessionParticipante, SessionCnn)))
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Para emitir a credencial é preciso estar vinculado à atividade principal!";
                return;
            }
        }

        //if (SessionParticipante.CdCredencial != "0")
        //{
        //    Response.Write(@"<script language='javascript'>return confirm('Credencial já emitida para este Participante!\n" +
        //                     "Deseja emitir outra?');</script>");

        //    return;
            
        //}
        //else
        //{
        //    if (SessionParticipante != null)
        //    {
        //        if (!SessionParticipante.Categoria.FlAtividades)
        //            goto imprimir;// return;

        //        Inscricoes oInscricoes = new Inscricoes();
        //        if (oInscricoes.verificarAtividadeInscricao(SessionParticipante.CdEvento, SessionParticipante.CdParticipante, SessionParticipante.CdCategoria, "inscrlocal", SessionCnn) > 0)
        //        {
        //            DataTable DTAtiv = oInscricoes.ListarAtividadesDisponiveis(SessionParticipante, null, "inscrlocal", SessionCnn);

        //            if (DTAtiv != null)
        //            {
        //                if (DTAtiv.Rows.Count == 1)
        //                {
        //                    string cdAtividade = DTAtiv.DefaultView[0]["cdAtividade"].ToString().Trim();
        //                    decimal vlAtv = decimal.Parse(DTAtiv.DefaultView[0]["vlAtividade"].ToString().Trim());
        //                    decimal vlDesc = decimal.Parse(DTAtiv.DefaultView[0]["vlDescontoReal"].ToString().Trim());
        //                    decimal vlTotInscri = decimal.Parse(DTAtiv.DefaultView[0]["vlTotInscri"].ToString().Trim());
        //                    //string tpPagto = SessionCategoria.NoCategoria;
        //                    if (oInscricoes.MatriculasGravar(SessionParticipante.CdEvento, SessionParticipante.CdParticipante, cdAtividade, 0, 1, "000000001", SessionCnn))
        //                    {//evento gratuito
        //                        vlAtv = 0;
        //                        vlDesc = 0;
        //                        vlTotInscri = 0;
        //                        //tpPagto = "";

        //                        if (oInscricoes.verificarVagas(SessionEvento.CdEvento, cdAtividade, SessionCnn) <= 0)
        //                        {
        //                            lblMsg.Visible = true;
        //                            lblMsg.Text = "Não há mais vagas!";
        //                            return;
        //                        }

        //                        DataTable DTVerificaMatricla = oInscricoes.ListarAtividadesDoParticipante(SessionParticipante, cdAtividade, SessionCnn);
        //                        if ((DTVerificaMatricla == null) || (DTVerificaMatricla.Rows.Count <= 0))
        //                        {
        //                            //return; //já cadastrado



        //                            //gerar matricula
        //                            if (oInscricoes.MatriculasGravar(SessionParticipante.CdEvento, SessionParticipante.CdParticipante, cdAtividade, 0, 1, "000000001", SessionCnn))
        //                            {
        //                                //enviar email

        //                                //-----
        //                                //return; //inclusão em curso com suscesso
        //                            }
        //                            else
        //                            {
        //                                lblMsg.Visible = true;
        //                                lblMsg.Text = "Não foi possível efetuar a inscrição.\n" +
        //                                     "Tente de novo, se persistir o problema entre em contato o administrador do sistema.";

        //                                return;
        //                            }
        //                        }
        //                    }
        //                }
        //            }

        //            //BtnInscrCursos_Click(sender, e);
        //        }
        //    }

        //}

       //imprimir:



        if (oParticipanteCad.GerarCredencial(SessionParticipante, SessionCnn))
        {
            SessionParticipante = oParticipanteCad.Pesquisar(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);
            Session["SessionParticipante"] = SessionParticipante;

            lblMsg.Visible = true;
            lblMsg.Text = "Já foi gerada uma credencial para esta inscrição. Caso opte por gerar uma nova, lembre-se que a credencial anterior será invalidada no sistema.";

            //Response.Write("<script>window.open('rptEtiqueta.aspx','_blanck');</script>");
            //Response.Write("<script>window.open('rptEtiqueta.aspx');</script>");
            Server.Transfer("rptEtiqueta.aspx", true);
        }
        else
        {
            lblMsg.Visible = true;
            lblMsg.Text = oParticipanteCad.RcMsg;
        }
    }
}