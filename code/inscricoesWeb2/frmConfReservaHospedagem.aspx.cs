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

public partial class frmConfReservaHospedagem : System.Web.UI.Page
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

            SessionEvento = (Evento)Session["SessionEvento"];
            if (SessionEvento == null)
            {
                
                if ((Request["e"] != null) &&
                    (Request["e"] != ""))
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
                if ((Request["p"] != null) &&
                    (Request["p"] != ""))
                {
                    string cdparticipante = cllEventos.Crypto.DecryptStringAES(Request["p"].ToString());

                    SessionParticipante = oParticipanteCad.Pesquisar(SessionEvento.CdEvento, cdparticipante, SessionCnn);
                    Session["SessionParticipante"] = SessionParticipante;
                    if (SessionParticipante == null)
                    {
                        return;
                        //Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                        //                 "04",
                        //                 ""), true);

                    }
                }
            }

            if (SessionParticipante.HotelReserva != null)
            {
                if (SessionParticipante.HotelReserva.DsSituacaoLocacao == "Confirmado")
                {
                    Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                         "024",
                                         ""), true);
                }

                HotelReservasCad oHotelReservasCad = new HotelReservasCad();
                oHotelReservasCad.AlterarSituacaoReserva(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionParticipante.HotelReserva.CdHotel, SessionParticipante.HotelReserva.CdAcomodacao, SessionParticipante.HotelReserva.CdQuarto, "Confirmado", SessionCnn);


                SessionParticipante = oParticipanteCad.Pesquisar(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);
                Session["SessionParticipante"] = SessionParticipante;


                HotelCad oHotelCad = new HotelCad();
                Hotel oHotel = oHotelCad.Pesquisar(SessionParticipante.HotelReserva.CdHotel, SessionCnn);

                HotelAcomodacaoCad oHotelAcomodacaoCad = new HotelAcomodacaoCad();
                HotelAcomodacao oHotelAcomodacao = oHotelAcomodacaoCad.Pesquisar(SessionParticipante.HotelReserva.CdHotel, SessionParticipante.HotelReserva.CdAcomodacao, SessionCnn);


                string body = "<html> " +
                                    "<head> " +
                                    "</head> " +

                                    "<body lang=PT-BR link=blue vlink=purple> " +
                                    "<img src=\"http://www.fazendomais.com/imagensgeral/topo_email_" + SessionParticipante.CdEvento + ".jpg\" alt=\"topo_email\"/><br><br> " +
                                    "<div style='width: 670px'>" +
                                    "<p>o Participante <b>" + SessionParticipante.NoParticipante + "</b>, confirmou sua reserva de hospedagem conforme dados abaixo para o <b>" +
                                    SessionEvento.NoEvento + "</b>. </p>" +

                                    "<p style='font-family:\"Arial\";'>" +
                                    "<b>DADOS DA HOSPEDAGEM</b><br/><br/> " +

                                  
                                    "<b>-                 Hotel:</b> " + oHotel.NoHotel + "<br/>" +

                          
                                    "<b>-       Tipo Hospedagem:</b> " + oHotel.NoHotel + "<br/> " +

                    
                                    "<b>-              Check-in:</b> 06/10/2015<br/> " +

                              
                                    "<b>-             Check-out:</b> 07/10/2015<br/> " +

                
                                    "<b>-              Hóspedes:</b> " + (SessionParticipante.DtAcompanhantes != null ? "2" : "1") + "<br/> " +
            
                                    "<b>     " + SessionParticipante.NoParticipante + "</b><br/> ";
                        if (SessionParticipante.DtAcompanhantes != null)
                        {
                            body += 
                                    "<b>     " + SessionParticipante.DtAcompanhantes.DefaultView[0]["dsAcompanhante"].ToString() + "</b><br/> ";
                        }

                        body += 
                                "<b>-     Tipo de Pagamento:</b> SOMENTE DIÁRIAS<br/> " +

                               
                                "<b>- Regime de Alimentação:</b> CAFÉ DA MANHÃ</p><br/> " +



                        SessionEvento.DsCorpoMensagensEmail +

          "<br><br>" +
          "</div> " +
         "<img src=\"http://www.fazendomais.com/imagensgeral/rodape_email_" + SessionParticipante.CdEvento + ".jpg\" alt=\"topo_email\"/></br> " +
       "</body> " +
       "</html> ";

                        oGeral.EnviarEmailAvulsoGenerico(SessionEvento, "", "helpdesk@congressoabert.com.br, flavia@flaturviagens.com.br", SessionEvento.NoEvento + " - Confirmação de Reserva - CPF: " + oClsFuncoes.CPFCNPJMascarar(SessionParticipante.NuCPFCNPJ), body, "", SessionCnn);

                Server.Transfer(string.Format("frm_mensagens.aspx?cdMensagem={0}&dsMensagem={1}",
                                         "025",
                                         ""), true);

            }
        }
        else
        {
            //SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            SessionParticipante = (Participante)Session["SessionParticipante"];

        }
    }
}