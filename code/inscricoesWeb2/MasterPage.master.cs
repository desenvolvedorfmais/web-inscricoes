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

using System.IO;

using AjaxControlToolkit;

//using System.Web.Mvc;

public partial class MasterPage : System.Web.UI.MasterPage
{
    Evento SessionEvento;
    SqlConnection SessionCnn;
    Participante SessionParticipante;

    String SessionIdioma;
    String SessionCateg;
        
    Categoria SessionCategoria;

    Pedido SessionPedido;

    Inscricoes SessionInscricoes;

    Tese SessionTese;
    TeseParticipante SessionTeseParticipante;

    Atividade SessionAtividade;

    String SessionTipoSistema;

    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            HttpContext.Current.Response.AddHeader(
                "p3p", "CP=\"IDC DSP COR ADM DEVi TAIi PSA PSD IVAi IVDi CONi HIS OUR IND CNT\"");

            string caminhoImagem = "img/bg_topo.jpg";

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

                if (SessionParticipante == null)
                    SessionParticipante = (Participante)Session["SessionParticipante"];
                else
                    Session["SessionParticipante"] = SessionParticipante;

                SessionIdioma = (String)Session["SessionIdioma"];
                if (SessionIdioma == null)
                    SessionIdioma = "PTBR";
                Session["SessionIdioma"] = SessionIdioma;

                SessionTipoSistema = (String)Session["SessionTipoSistema"];
                if (SessionTipoSistema == null)
                    SessionTipoSistema = "NRM";
                Session["SessionTipoSistema"] = SessionTipoSistema;

                SessionCateg = (String)Session["SessionCateg"];
                if (SessionCateg == null)
                    SessionCateg = "";
                Session["SessionCateg"] = SessionCateg;

                //if (SessionIdioma == "PTBR")
                //{
                //    txtIdioma.SelectedIndex = 0;
                //}
                //else if (SessionIdioma == "ENUS")
                //{
                //    txtIdioma.SelectedIndex = 1;
                //}
                //else if (SessionIdioma == "ESP")
                //{
                //    txtIdioma.SelectedIndex = 2;
                //}
                //else if (SessionIdioma == "FRA")
                //{
                //    txtIdioma.SelectedIndex = 3;
                //}

                buscarTextoHtmlPatrocinadores();
            }
            else
            {
                SessionEvento = (Evento)Session["SessionEvento"];

                SessionCnn = (SqlConnection)Session["SessionCnn"];

                SessionParticipante = (Participante)Session["SessionParticipante"];


                SessionIdioma = (String)Session["SessionIdioma"];

                SessionCateg = (String)Session["SessionCateg"];

                SessionTipoSistema = (String)Session["SessionTipoSistema"];
            }

            if (SessionEvento.CdEvento == "005701")
            {
                s1.Visible = true;
                s2.Visible = true;
            }

            //img_Topo.ImageUrl = caminhoImagem;

           

            if (SessionEvento != null)
            {
                /*
                //caminhoImagem = "img/top_" + SessionEvento.CdEvento + ".jpg";
                caminhoImagem = "https://inscricoesweb.fazendomais.com/imagensgeral/top_" + SessionEvento.CdEvento + ".jpg";

                img_Topo.ImageUrl = caminhoImagem;
                //img_Topo.Visible = false;

                //if (SessionEvento.CdEvento == "001604")
                //    tmp_link.HRef = "http://congressoabrasel.com.br/index.php";
                //else
                //    tmp_link.HRef = "";

                if (SessionEvento.CdEvento == "001304")
                {
                    listras.Attributes.Add("onclick", "window.location='http://congresso.conasems.org.br/xxx/'");
                }

                //imgRealizacao.ImageUrl = "img/realizacao_" + SessionEvento.CdEvento + ".jpg;
                imgRealizacao.ImageUrl = "https://inscricoesweb.fazendomais.com/imagensgeral/realizacao_" + SessionEvento.CdEvento + ".jpg";

                if ((SessionEvento.CdEvento == "003401") || (SessionEvento.CdEvento == "001405") || (SessionEvento.CdEvento == "001406") || (SessionEvento.CdEvento == "001604") || (SessionEvento.CdEvento == "002904"))
                    imgRealizacao.ImageUrl = "https://inscricoesweb.fazendomais.com/imagensgeral/realizacao_" + SessionEvento.CdEvento + ".png";

                if (SessionEvento.FlMostrarRealizador)
                {
                    imgRealizacao.Visible = true;
                    //if ((SessionEvento.CdCliente != "0003") && (SessionEvento.CdCliente != "0013"))
                    //    lblRealizacao.Visible = true;
                    //else
                    lblRealizacao.Visible = false;
                }
                else
                {

                    imgRealizacao.Visible = false;
                    lblRealizacao.Visible = false;

                }*/


                


                if (SessionEvento.DsCSSEstiloWeb.Trim() != "")
                {

                    //Atribuindo o head (cabeçalho da nossa página)
                    HtmlHead head = this.Page.Header;

                    head.Controls.Remove(head.FindControl("Estilo"));
                    //head.Controls.RemoveAt(5);

                    //Instanciando um HTMLLink com o nome de link
                    HtmlLink link = new HtmlLink();


                    //bool codeBehindValue = bool.Parse(hdnResultValue.Value.ToString());
                    //if (!codeBehindValue)
                    //{
                    //    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "mensagem", "Mensagem()", true);
                    //}

                    //Adicionando os atributos
                    link.Attributes.Add("Id", SessionEvento.DsCSSEstiloWeb);
                    link.Attributes.Add("type", "text/css");
                    link.Attributes.Add("rel", "stylesheet");
                    link.Attributes.Add("href", "css/" + SessionEvento.DsCSSEstiloWeb + ".css");

                    if ((SessionEvento.CdCliente == "0083") && (SessionCateg == "00830303"))
                    {
                        link.Attributes.Add("href", "css/" + SessionEvento.DsCSSEstiloWeb + "_2.css");
                    }

                    //Adicionando o link ao head (cabeçalho)
                    head.Controls.Add(link);



                }
                //if (SessionEvento.CdCliente == "0003")
                //{
                //    img_Topo.Visible = true;
                //}
                if (SessionEvento.CdEvento == "001002")
                {
                    /*
                    HtmlHead head2 = this.Page.Header;
                    HtmlLink link2 = new HtmlLink();
                    link2.Attributes.Add("type", "text/javascript");
                    link2.Attributes.Add("src", "scripts/site.js");
                    head2.Controls.Add(link2);

                    img_Topo.Visible = false;


                    topo.InnerHtml =
                        "<div id=\"header-container\">   " +
                        "    <div class=\"header-content\"> " +
                        "        <a href=\"http://www.congressoabert.com.br\" id=\"logo-congresso\">LOGO</a> " +
                        "         <ul class=\"menu\">" +
                        "            <li><a href=\"http://www.congressoabert.com.br/evento\" id=\"evento\">Evento</a></li> " +
                        "            <li><a href=\"http://www.congressoabert.com.br/inscricao\" id=\"inscricao\">Incrição</a></li> " +
                        "            <li><a href=\"http://www.congressoabert.com.br/exposicao\" id=\"exposicao\">Edições anteriores</a></li> " +
                        "            <li><a href=\"http://www.congressoabert.com.br/saladeimprensa\" id=\"sala\">Sala de imprensa</a></li> " +
                        "            <li><a href=\"http://www.congressoabert.com.br/programacao\" id=\"programacao\">Programação</a></li> " +
                        "            <li><a href=\"http://www.congressoabert.com.br/comochegar\" id=\"como-chegar\">Como chegar</a></li> " +
                        // "            <li><a href=\"http://www.congressoabert.com.br/edicoesanteriores\" id=\"edicoes-anteriores\">Edições anteriores</a></li> " +
                        "        </ul> " +
                        "    </div> " +
                        "</div> ";

                    MenuItem menuitem1 = new MenuItem();
                    menuitem1.NavigateUrl = "~/frmAcompanhante.aspx";
                    menuitem1.Text = "Acompanhante";
                    menuitem1.Value = "Acompanhante";
                    Menu1.Items.Add(menuitem1);

                    MenuItem menuitem2 = new MenuItem();
                    menuitem2.NavigateUrl = "~/frmReservasHospedagem.aspx";
                    menuitem2.Text = "Hospedagem";
                    menuitem2.Value = "Hospedagem";
                    Menu1.Items.Add(menuitem2);
                     */ 
                }

                if (SessionEvento.CdEvento == "001003")
                {
                    /*
                    HtmlHead head2 = this.Page.Header;
                    HtmlLink link2 = new HtmlLink();
                    link2.Attributes.Add("type", "text/javascript");
                    link2.Attributes.Add("src", "scripts/site.js");
                    head2.Controls.Add(link2);

                    img_Topo.Visible = false;


                    topo.InnerHtml =
                        "<div id=\"header-container\">   " +
                        "    <div class=\"header-content\"> " +
                        "        <a href=\"http://www.wstm.com.br\" id=\"logo-congresso\">LOGO</a> " +
                        "         <ul class=\"menu\">" +
                        "            <li><a href=\"http://www.wstm.com.br/evento\" id=\"evento\"><p>Evento</p></a></li> " +
                        "            <li><a href=\"http://www.wstm.com.br/inscricao\" id=\"inscricao\"><p>Inscri&ccedil;&atilde;o</p></a></li> " +
                        "            <li><a href=\"http://www.wstm.com.br/exposicao\" id=\"exposicao\"><p>Exposi&ccedil;&atilde;o</p></a></li> " +
                        "            <li><a href=\"http://www.wstm.com.br/palestrantes-evento\" id=\"palestrantes\"><p>Palestrantes</p></a></li> " +
                        "            <li><a href=\"http://www.wstm.com.br/saladeimprensa\" id=\"sala\"><p>Sala de imprensa</p></a></li> " +
                        "            <li><a href=\"http://www.wstm.com.br/programacao\" id=\"programacao\"><p>Programa&ccedil;&atilde;o</p></a></li> " +
                        "            <li><a href=\"http://www.wstm.com.br/comochegar\" id=\"como-chegar\"><p>Como chegar</p></a></li> " +
                        // "            <li><a href=\"http://www.wstm.com.br/edicoesanteriores\" id=\"edicoes-anteriores\">Edições anteriores</a></li> " +
                        "        </ul> " +
                        "    </div> " +
                        "</div> ";

                    */
                }

                if (SessionEvento.CdEvento == "001004")
                {
                    
                    //HtmlHead head2 = this.Page.Header;
                    //HtmlLink link2 = new HtmlLink();
                    //link2.Attributes.Add("type", "text/javascript");
                    //link2.Attributes.Add("src", "abert/js/site.js");
                    //head2.Controls.Add(link2);

                    //img_Topo.Visible = false;



                    topo.InnerHtml =
                        "<div id=\"menu_abert27\"> " +
                        "   <div id=\"logo_abert27\"> " +
                        "    <a href=\"http://www.congressoabert.com.br\" id=\"logo\"> " +
                        "       <img src=\"imagensgeral/001004/logo_aberth27.jpg\" alt=\"logo abert 27\"> " +
                        "    </a> " +
                        "   </div> " +
                        "  <div id=\"menuitem\"> " +
                        "	<ul> " +
                        "    	<li> " +
                        "	        <a href=\"http://www.congressoabert.com.br/o-event\" class=\"item evento\"> " +
                        "		        <img src=\"imagensgeral/001004/transparente.fw.png\" alt=\"transparente\"> " +
                        "           </a> " +
                        "       </li> " +
                        "		<li> " +
                        "    	    <a href=\"http://www.congressoabert.com.br/inscricao\" class=\"item inscricao\"> " +
                        "        	    <img src=\"imagensgeral/001004/transparente.fw.png\" alt=\"transparente\"> " +
                        "           </a> " +
                        "       </li> " +
                        "       <li> " +
                        "           <a href=\"http://www.congressoabert.com.br/sala-de-imprensa\" class=\"item sala\"> " +
                        "    	        <img src=\"imagensgeral/001004/transparente.fw.png\" alt=\"transparente\"> " +
                        "           </a> " +
                        "       </li> " +
                        "    	<li> " +
                        "	        <a href=\"http://www.congressoabert.com.br/programacao\" class=\"item programacao\"> " +
                        "    	        <img src=\"imagensgeral/001004/transparente.fw.png\" alt=\"transparente\"> " +
                        "           </a> " +
                        "       </li> " +
                        "    	<li>  " +
                        "	        <a href=\"http://www.congressoabert.com.br/como-chegar\" class=\"item comochegar\"> " +
                        "    	        <img src=\"imagensgeral/001004/transparente.fw.png\" alt=\"transparente\"> " +
                        "           </a> " +
                        "       </li> " +
                        "	</ul> " +
                        "  </div> " +
                        "</div> ";
                                       

                    //MenuItem menuitem1 = new MenuItem();
                    //menuitem1.NavigateUrl = "~/frmAcompanhante.aspx";
                    //menuitem1.Text = "Acompanhante";
                    //menuitem1.Value = "Acompanhante";
                    //Menu1.Items.Add(menuitem1);

                    //MenuItem menuitem2 = new MenuItem();
                    //menuitem2.NavigateUrl = "~/frmReservasHospedagem.aspx";
                    //menuitem2.Text = "Hospedagem";
                    //menuitem2.Value = "Hospedagem";
                    //Menu1.Items.Add(menuitem2);
                     
                }

                if (SessionEvento.CdEvento == "006501")
                {

                    topo.InnerHtml =
                        "        <div class=\"container\"> " +
                        "            <div id=\"navbar\" class=\"navbar navbar-default\"> " +
                        "                <div class=\"navbar-header\"> " +
                        //"                    <button type=\"button\" class=\"navbar-toggle\" data-toggle=\"collapse\" data-target=\".navbar-collapse\"> " +
                        //"                        <span class=\"sr-only\">Toggle navigation</span> " +
                        //"                        <span class=\"icon-bar\"></span> " +
                        //"                        <span class=\"icon-bar\"></span> " +
                        //"                        <span class=\"icon-bar\"></span> " +
                        //"                    </button> " +
                        "                    <a class=\"navbar-brand\" href=\"http://www.anabb.org.br/seminariosanabb/index.html\"></a> " +
                        "                </div> " +
                        "                <div class=\"collapse navbar-collapse\"> " +
                        "                    <ul class=\"nav navbar-nav\"> " +
                        "                        <li><a href=\"http://www.anabb.org.br/seminariosanabb/#main-slider\"><i class=\"icon-home\"></i></a></li> " +
                        "                        <li><a href=\"http://www.anabb.org.br/seminariosanabb/#services\">Seminários</a></li> " +
                        "                        <li><a href=\"http://www.anabb.org.br/seminariosanabb/#main-slider2\">O Evento</a></li> " +
                        "                        <li><a href=\"http://www.anabb.org.br/seminariosanabb/#pricing\">Programação</a></li> " +
                        "                        <li><a href=\"http://www.anabb.org.br/seminariosanabb/#main-slider3\">Hotéis</a></li> " +
                        "                        <li class=\"active\"><a href=\"http://www.fazendomais.com/inscricoes/inscr.aspx?codEvento=MDA2NTAx&cdLng=PTBR&cat=&keyAut=&tpSist=NRM\">Inscrições</a></li> " +
                        "                        <li><a href=\"http://df.divirtasemais.com.br/\"  target=\"_blank\">Roteiro Cultural</a></li> " +
                        "                    </ul> " +
                        "                </div> " +
                        "            </div> " +
                        "        </div> ";


                        //"<div id=\"menu_abert27\"> " +
                        //"   <div id=\"logo_abert27\"> " +
                        //"    <a href=\"http://www.congressoabert.com.br\" id=\"logo\"> " +
                        //"       <img src=\"imagensgeral/001004/logo_aberth27.jpg\" alt=\"logo abert 27\"> " +
                        //"    </a> " +
                        //"   </div> " +
                        //"  <div id=\"menuitem\"> " +
                        //"	<ul> " +
                        //"    	<li> " +
                        //"	        <a href=\"http://www.congressoabert.com.br/o-event\" class=\"item evento\"> " +
                        //"		        <img src=\"imagensgeral/001004/transparente.fw.png\" alt=\"transparente\"> " +
                        //"           </a> " +
                        //"       </li> " +
                        //"		<li> " +
                        //"    	    <a href=\"http://www.congressoabert.com.br/inscricao\" class=\"item inscricao\"> " +
                        //"        	    <img src=\"imagensgeral/001004/transparente.fw.png\" alt=\"transparente\"> " +
                        //"           </a> " +
                        //"       </li> " +
                        //"       <li> " +
                        //"           <a href=\"http://www.congressoabert.com.br/sala-de-imprensa\" class=\"item sala\"> " +
                        //"    	        <img src=\"imagensgeral/001004/transparente.fw.png\" alt=\"transparente\"> " +
                        //"           </a> " +
                        //"       </li> " +
                        //"    	<li> " +
                        //"	        <a href=\"http://www.congressoabert.com.br/programacao\" class=\"item programacao\"> " +
                        //"    	        <img src=\"imagensgeral/001004/transparente.fw.png\" alt=\"transparente\"> " +
                        //"           </a> " +
                        //"       </li> " +
                        //"    	<li>  " +
                        //"	        <a href=\"http://www.congressoabert.com.br/como-chegar\" class=\"item comochegar\"> " +
                        //"    	        <img src=\"imagensgeral/001004/transparente.fw.png\" alt=\"transparente\"> " +
                        //"           </a> " +
                        //"       </li> " +
                        //"	</ul> " +
                        //"  </div> " +
                        //"</div> ";

                }

                if (SessionEvento.CdEvento == "002902")
                {
                    /*
                    MenuItem menuitem1 = new MenuItem();
                    menuitem1.NavigateUrl = "~/frmAcompanhante.aspx";
                    menuitem1.Text = "Acompanhante";
                    menuitem1.Value = "Acompanhante";
                    Menu1.Items.Add(menuitem1);
                    */
                }

                if (SessionEvento.CdEvento == "003401")
                {
                    /*
                    if ((SessionParticipante != null) && ((SessionParticipante.CdCategoria == "00340101") || (SessionParticipante.CdCategoria == "00340102") || (SessionParticipante.CdCategoria == "00340103")))
                    {
                        MenuItem menuitem1 = new MenuItem();
                        menuitem1.NavigateUrl = "~/frmTesesLista.aspx";
                        menuitem1.Text = "Cadastro de Tese";
                        menuitem1.Value = "Cadastro de Tese";
                        Menu1.Items.Add(menuitem1);
                    }
                    */
                }
                
                string sURL = Request.Url.ToString().ToLower();
                if ((sURL.Contains("index.aspx")) || 
                    ((!SessionEvento.FlEventoComRecebimentos) && (!SessionEvento.FlMostrarMenu)) || 
                    ((SessionParticipante != null) && (SessionParticipante.CdParticipante == "")) || 
                    (!SessionEvento.FlMostrarMenu) || (SessionTipoSistema != "NRM") ||
                    (sURL.Contains("frmverificacpfcadastro.aspx")) ||
                    (sURL.Contains("frmprogramacao.aspx")) ||
                    (sURL.Contains("frmemitircertificado.aspx")) ||
                    (sURL.Contains("frmdetalheatividade.aspx")) ||
                    (sURL.Contains("frminscricaoconfirmada.aspx")))
                {
                    //Menu1.Visible = false;
                    smoothmenu1.Visible = false;
                }
                else //if (sURL.Contains("/products/") || sURL.Contains("/services/"))
                {
                    //Menu1.StartingNodeOffset = -1;
                    if (SessionTipoSistema == "NRM")
                    {
                        //Menu1.Visible = true;
                        smoothmenu1.Visible = true;

                        //smoothmenu1.InnerHtml = MontaMenu();

                        if (
                            ((SessionEvento.FlEventoComRecebimentos) && (TiposPagamentoCad.VerificarFormaPagamento(SessionEvento.CdEvento, "BOLETO", SessionCnn)) &&
                            ((SessionEvento.DtFechamentoInscrWeb == null) || (SessionEvento.DtFechamentoInscrWeb >= Geral.datahoraServidor(SessionCnn))))
                            || ((SessionEvento.CdEvento == "007002") && ((SessionParticipante.NoPais == "BRASIL") || (SessionParticipante.NoPais == "BRAZIL") || (SessionParticipante.NoPais == "BRASIL") || (SessionParticipante.NoPais == "BRÉSIL")))
                            )
                            mnuBoletos.Visible = true;


                        if (SessionEvento.CdEvento == "007701")
                        {
                            mnuBoletos.Visible = false;
                        }

                        if ((SessionEvento.FlEmiteRecibo) && (SessionEvento.FlEventoComRecebimentos))
                            mnuDadosRecibos.Visible = true;


                        if ((SessionParticipante.Categoria.FlRequerConfirmacaoDoc) && (!SessionParticipante.FlDocumentoConfirmado))
                            mnuEnvioDoc.Visible = true;

                    }
                }
                
                /*
                if ((SessionEvento.CdEvento == "004401") && (Menu1.FindItem("Trabalhos Técnicos") == null) && (SessionIdioma == "PTBR"))// && (Geral.datahoraServidor(SessionCnn) < DateTime.Parse("10/11/2013 23:59:59")))// && (sURL.Contains("frmcadastrartrabalhostecnicos.aspx")))
                {
                    
                    //Menu1.Visible = false;
                    if ((SessionParticipante != null))// && ((SessionParticipante.CdCategoria == "00340101") || (SessionParticipante.CdCategoria == "00340102") || (SessionParticipante.CdCategoria == "00340103")))
                    {
                        MenuItem menuitem1 = new MenuItem();
                        menuitem1.NavigateUrl = "~/frmTrabalhosLista.aspx";
                        menuitem1.Text = "Trabalhos Técnicos";
                        menuitem1.Value = "Trabalhos Técnicos";
                        //menuitem1.
                        Menu1.Items.Add(menuitem1);
                    }
                     * 
                }
                */


                EventoCad oEventoCad = new EventoCad();
                DataTable oDTApoio = oEventoCad.ListarApoioAtivos(SessionEvento.CdEvento, SessionCnn);

                if ((oDTApoio != null) && (oDTApoio.Rows.Count > 0))
                {

                    if (!Page.ClientScript.IsStartupScriptRegistered("alert"))
                    {
                        string scrypt =
                            "<script type='text/javascript'>" +
                            "var mygallery=new fadeSlideShow({ " +
                            "wrapperid: 'fadeshow1', " +
                            ((SessionEvento.CdEvento == "001304") || (SessionEvento.CdEvento == "004103") ? "dimensions: [190, 300], " : (SessionEvento.CdEvento == "001604") ? "dimensions: [160, 556], " : (SessionEvento.CdEvento == "003603") ? "dimensions: [190, 563], " : "dimensions: [190, 350], ") +
                            "imagearray: " +
                            " [";

                        for (int i = 0; i < oDTApoio.Rows.Count; i++)
                        {
                            scrypt += " ['" + oDTApoio.DefaultView[i]["dsCaminhoImagem"].ToString() + "','" + oDTApoio.DefaultView[i]["dsLinkRedirecionamento"].ToString() + "','_blank',''] ";
                            if (i + 1 < oDTApoio.Rows.Count)
                                scrypt += ",";
                        }

                        scrypt += "]," +
                            "displaymode: {type:'auto', pause:5000, cycles:0, wraparound:false}," +
                            "persist: false, " +
                            "fadeduration: 300, " +
                            "descreveal: 'none'," +
                            "togglerid: ''" +
                            "})" +
                           "</script>";

                        Type cstype = this.GetType();
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterClientScriptBlock(
                             cstype,
                             "showSaveMessage",
                             scrypt);





                    }
                }

            }
            //Control controlePainel = FindControlRecursive(this.Page, "txt_nuCPFCNPJ");
            //if (controlePainel != null)

            /*
            if (Menu1.FindItem("Sair") == null)
            {
                MenuItem menuitem2 = new MenuItem();
                menuitem2.NavigateUrl = "~/logoff.aspx";
                if (SessionIdioma == "PTBR")
                {
                    menuitem2.Text = "Sair";
                }
                else if (SessionIdioma == "ENUS")
                {
                    menuitem2.Text = "Log out";
                }
                else if (SessionIdioma == "ESP")
                {
                    menuitem2.Text = "finalizar la sesión";
                }
                else if (SessionIdioma == "FRA")
                {
                    menuitem2.Text = "Déconnexion";
                }


                //if (txtIdioma.SelectedIndex == 0)

                //else if (txtIdioma.SelectedIndex == 1)

                //else if (txtIdioma.SelectedIndex == 2)

                //else if (txtIdioma.SelectedIndex == 3)


                menuitem2.Value = "Sair";
                Menu1.Items.Add(menuitem2);
            }
            */
            
            //if (SessionIdioma == "PTBR")//(txtIdioma.SelectedIndex == 0)
            //{
            //    SessionIdioma = "PTBR";
            //    Session["SessionIdioma"] = SessionIdioma;
                                
            //    mnuCadastro.InnerHtml = "<a href=\"frmCadastroAuto.aspx\" >Dados Cadastrais</a>";
            //    mnuAtividades.InnerHtml = "<a href=\"frmSelAtividades.aspx\" >Inscreva-se</a>";
            //    mnuEtiqueta.InnerHtml = "<a href=\"frmCredencial.aspx\" >Imprimir Credencial</a>";
            //    mnuPedidos.InnerHtml = "<a href=\"#\" >Acompanhar Inscrição</a>";
            //    mnuPedidosListas.InnerHtml = "<a href=\"frmListarPedidos.aspx\" >Pedidos de Inscrição</a>";
            //    mnuBoletos.InnerHtml = "<a href=\"frmListaBoletosPedido.aspx\" >Listar Boletos</a>";
            //    mnuDadosRecibos.InnerHtml = "<a href=\"frmDadosRecibo.aspx\" >Alterar Dados Recibo</a>";
            //    mnuInscricoes.InnerHtml = "<a href=\"frmAtividadesParticipante.aspx\" >Minhas Inscrições/Atividades</a>";
            //    mnuEnvioDoc.InnerHtml = "<a href=\"frmEnviarDocumento.aspx\" >Enviar Documento</a>";
            //    mnuSeguranca.InnerHtml = "<a href=\"frmNovaSenha.aspx\" >Alterar Senha</a>";
            //    mnuSair.InnerHtml = "<a href=\"frmSessaoExpirada.aspx\" >Sair</a>";                

            //}
            //else 

                 if (SessionIdioma == "ENUS")
            {
                SessionIdioma = "ENUS";
                Session["SessionIdioma"] = SessionIdioma;

                linhamenuENUS.Visible = true;
                linhamenuPTBR.Visible = false;
                linhamenuESP.Visible = false;

                this.Page.Title = "Registrations";
                
            }
            else if (SessionIdioma == "ESP")//(txtIdioma.SelectedIndex == 2)
            {
                SessionIdioma = "ESP";
                Session["SessionIdioma"] = SessionIdioma;

                

                linhamenuENUS.Visible = false;
                linhamenuPTBR.Visible = false;
                linhamenuESP.Visible = true;

                this.Page.Title = "Inscripciones Web";

                
            }
           /* else if (SessionIdioma == "FRA")//(txtIdioma.SelectedIndex == 3)
            {
                SessionIdioma = "FRA";
                Session["SessionIdioma"] = SessionIdioma;

                //lblIdioma.Text = "Language ";
                lblRealizacao.Text = "Réalisation";

                Menu1.Items.RemoveAt(3);

                Menu1.Items[0].Text = "Cadastre";
                Menu1.Items[1].Text = "Demande d'inscription";
                Menu1.Items[2].Text = "Mes commandes";
                Menu1.Items[3].Text = "Mes abonnements / Inscriptions";
                Menu1.Items[4].Text = "Changer mot de passe";

                Menu1.Items[Menu1.Items.Count - 1].Text = "Déconnexion";
            }
            */

            if (SessionEvento.CdEvento == "003401")
            {
                /*
                Menu1.Items[0].Text = "Cadastro";
                Menu1.Items[1].Text = "Pré-inscrição no Eixo Temático";
                Menu1.Items[2].Text = "Acompanhar Pré-inscrição";
                Menu1.Items[3].Text = "Ver Eixo Temático Inscrito";
                 */ 
            }


            if ((SessionParticipante != null) && (SessionParticipante.Categoria.FlRequerConfirmacaoDoc) && (!SessionParticipante.FlDocumentoConfirmado) )
                mnuEnvioDoc.Visible = mnuEnvioDocENUS.Visible = true;


            if (SessionEvento.CdCliente == "0014")
                mnuEnvioDoc.Visible = false;

            //if ((SessionEvento.CdCliente == "0013") &&
            //    (SessionParticipante != null))
            //{
                
            //    //Inscricoes oInscricoes = new Inscricoes();

            //    //DataTable dt = oInscricoes.ListarAtividadesDoParticipante(SessionParticipante, SessionCnn);

            //    if ((SessionParticipante.NoAreaAtuacao == "SIM") && (!SessionParticipante.FlDocumentoConfirmado))
            //        mnuEnvioDoc.Visible = true;


            //    if ((Geral.datahoraServidor(SessionCnn) > DateTime.Parse("29/07/2015 23:59:00")) && (Geral.datahoraServidor(SessionCnn) <= DateTime.Parse("03/08/2015 23:59:59")))
            //    {
            //       if (SessionParticipante.CdCategoria == "00130501")
            //            mnuAtividades.Visible = false;
            //        //mnuEtiqueta.Visible = false;
            //        //mnuPedidos.Visible = false;
            //        mnuPedidosListas.Visible = false;
            //        mnuBoletos.Visible = false;
            //        mnuDadosRecibos.Visible = false;
            //        mnuInscricoes.Visible = false;
            //        //mnuEnvioDoc.Visible = false;
            //        //mnuSeguranca.Visible = false;

            //    }
                
            //    if (Geral.datahoraServidor(SessionCnn) > DateTime.Parse("03/08/2015 23:59:59"))
            //    {
            //        mnuAtividades.Visible = false;
            //        //mnuEtiqueta.Visible = false;
            //        mnuPedidos.Visible = false;
            //        mnuPedidosListas.Visible = false;
            //        mnuBoletos.Visible = false;
            //        mnuDadosRecibos.Visible = false;
            //        //mnuInscricoes.Visible = false;
            //        mnuEnvioDoc.Visible = false;
            //        //mnuSeguranca.Visible = false;                      
            //    }

            //    //else

            //    mnuEtiqueta.Visible = false;
            //    if (SessionParticipante.FlAtivo)
            //    {
            //        if (SessionParticipante.CdCategoria == "00130501")
            //        {
            //            if (SessionParticipante.FlConfirmacaoInscricao)
            //            {
            //                if (SessionParticipante.NoAreaAtuacao == "SIM")
            //                {
            //                    if ((SessionParticipante.DsAuxiliar19 == "SIM") && (SessionParticipante.FlDocumentoConfirmado))
            //                        mnuEtiqueta.Visible = true;
            //                }
            //                else
            //                {
            //                    mnuEtiqueta.Visible = true;
            //                }

            //               // mnuInscricoes.Visible = false;
            //                mnuAtividades.Visible = false;
            //            }
            //        }
            //        else
            //        {
            //            if (!SessionParticipante.Categoria.FlAtividades)
            //            {
            //                mnuEtiqueta.Visible = true;

            //                mnuAtividades.Visible = false;
            //                mnuPedidos.Visible = false;
            //                mnuInscricoes.Visible = false;
            //            }
            //            else if (SessionParticipante.FlConfirmacaoInscricao)
            //            {
            //                mnuEtiqueta.Visible = true;
            //                mnuAtividades.Visible = false;
            //            }
            //        }

            //        if ((Geral.datahoraServidor(SessionCnn) > DateTime.Parse("29/07/2015 23:59:00")) && (Geral.datahoraServidor(SessionCnn) <= DateTime.Parse("03/08/2015 23:59:59")))
            //        {

            //            if ((SessionParticipante.CdCategoria != "00130501") && (SessionParticipante.Categoria.FlAtividades))
            //                mnuAtividades.Visible = true;

            //            //mnuEtiqueta.Visible = false;
            //            //mnuPedidos.Visible = false;
            //            mnuPedidosListas.Visible = false;
            //            mnuBoletos.Visible = false;
            //            if (Geral.datahoraServidor(SessionCnn) <= DateTime.Parse("30/07/2015 23:59:00"))
            //                mnuBoletos.Visible = true;
                        
            //            mnuDadosRecibos.Visible = false;
            //            mnuInscricoes.Visible = false;
            //            //mnuEnvioDoc.Visible = false;
            //            //mnuSeguranca.Visible = false;
            //        }
            //        else if (Geral.datahoraServidor(SessionCnn) > DateTime.Parse("04/08/2015 00:00:00"))
            //        {
            //            mnuAtividades.Visible = false;
            //            mnuEtiqueta.Visible = false;
            //            mnuPedidos.Visible = false;
            //            mnuPedidosListas.Visible = false;
            //            mnuBoletos.Visible = false;
            //            mnuDadosRecibos.Visible = false;
            //            //mnuInscricoes.Visible = false;
            //            mnuEnvioDoc.Visible = false;
            //            //mnuSeguranca.Visible = false;                      
            //        }
                    
            //    }
              

            //}

            if (SessionEvento.CdCliente == "0016")
            {
                if (SessionParticipante != null)
                {
                    if (!SessionParticipante.Categoria.FlAtividades)
                    {
                        mnuAtividades.Visible = false;
                        mnuPedidos.Visible = false;
                    }

                    if (SessionParticipante.FlConfirmacaoInscricao)
                    {
                        mnuInscricoes.Visible = true;
                        mnuAtividades.Visible = false;

                        mnuPedidosListas.Visible = false;                        

                        mnuPedidos.Visible = false;
                        mnuDadosRecibos.Visible = false;

                        if (SessionEvento.FlEmiteRecibo)
                        {
                            PedidoCad oPedidoCad = new PedidoCad();
                            DataTable tempPedido = oPedidoCad.Listar(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);
                            if ((tempPedido != null) && (tempPedido.Rows.Count > 0))
                            {
                                mnuPedidos.Visible = true;
                                if (SessionIdioma == "PTBR")
                                    mnuDadosRecibos.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        mnuAtividades.Visible = true;
                        mnuInscricoes.Visible = false;

                        mnuPedidos.Visible = false;

                        mnuPedidosListas.Visible = false;
                        
                        mnuPedidos.Visible = false;
                        mnuDadosRecibos.Visible = false;

                        if (SessionEvento.FlEmiteRecibo)
                        {
                            PedidoCad oPedidoCad = new PedidoCad();
                            DataTable tempPedido = oPedidoCad.Listar(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);
                            DataRow[] foundRows = tempPedido.Select("flAtivo = 'True'");
                            if ((foundRows != null) && (foundRows.Length > 0))
                            {
                                mnuPedidos.Visible = true;
                                
                                mnuPedidosListas.Visible = true;

                                if (SessionIdioma == "PTBR")
                                    mnuDadosRecibos.Visible = true;
                            }
                        }
                    }

                }
            }

            if ((SessionEvento.CdCliente == "0048") &&
                (SessionParticipante != null))
            {
                /*
                Inscricoes oInscricoes = new Inscricoes();

                DataTable dt = oInscricoes.ListarAtividadesDoParticipante(SessionParticipante, SessionCnn);

                if ((dt != null) && (dt.Rows.Count > 0))
                {
                    Menu1.Items.RemoveAt(2);
                    Menu1.Items.RemoveAt(1);
                    //MenuItem menuitem5 = Menu1.Items[1];
                    //menuitem5.Enabled = false;
                }
                */
            }

            //if ((!SessionEvento.FlEmiteRecibo) || (!SessionEvento.FlEventoComRecebimentos))
            //{
            //    mnuDadosRecibos.Visible = false;
            //}


            if (SessionEvento.CdEvento == "007002")
            {
                mnuPedidos.Visible = false;
                mnuInscricoes.Visible = false;
            }


            if (SessionEvento.CdEvento == "011201")
            {
                mnuInscr.Visible = false;
                if (SessionParticipante != null)
                {
                    mnuCompras.Visible = true;
                    mnuComprar.Visible = true;

                    if (SessionParticipante.FlConfirmacaoInscricao)
                        mnuMinhasCompras.Visible = true;
                }
            }

            if (SessionEvento.CdEvento == "010101")
            {
                mnuInscr.Visible = false;
                mnuSair.InnerHtml = "<a href=\"frmSessaoExpirada.aspx\" >Fazer Nova Inscrição</a>";
            }

            mnuTrabalhos.Visible = mnuTrabalhosENUS.Visible = mnuTrabalhosESP.Visible = false;
            if ((SessionEvento.CdEvento == "008501") || (SessionEvento.CdEvento == "010101"))// && (SessionParticipante.CdCategoria != "00850108") && (SessionParticipante.CdCategoria != "00850115"))
            {
                if ((SessionParticipante.CdCategoria != "01010102") && (SessionParticipante.CdCategoria != "01010103"))
                    mnuTrabalhos.Visible = mnuTrabalhosENUS.Visible = mnuTrabalhosESP.Visible = true;

                if (SessionEvento.CdEvento == "010101")
                    mnuTrabalhos.InnerHtml = "<a href=\"frmEnunciadosLista.aspx\" >Enunciados</a>";

                if (SessionIdioma == "PTBR")
                    mnuAtividades.InnerHtml = "<a href=\"frmSelAtividades.aspx\" >Pagamento</a>";
                else if (SessionIdioma == "ENUS")
                    mnuAtividadesENUS.InnerHtml = "<a href=\"frmSelAtividades.aspx\" >Payment</a>";
                else if (SessionIdioma == "ESP")
                    mnuAtividadesESP.InnerHtml = "<a href=\"frmSelAtividades.aspx\" >Pago</a>";
            }
            mnuAtividades.Visible = mnuAtividadesENUS.Visible = mnuAtividadesESP.Visible = false;
            mnuPedidos.Visible = mnuPedidosENUS.Visible = mnuPedidosESP.Visible = false;
            mnuBoletos.Visible = mnuBoletosENUS.Visible = mnuBoletosESP.Visible = false;
            if ((SessionParticipante != null) && (SessionParticipante.Categoria.FlAtividades))
            {
                //if (!SessionParticipante.FlConfirmacaoInscricao)
                //{
                    Inscricoes oInscricoes = new Inscricoes();

                    DataTable DTAtiv = oInscricoes.ListarAtividadesDisponiveis(SessionParticipante, null, "inscrweb", SessionCnn);

                    if (((DTAtiv != null) && (DTAtiv.Rows.Count >= 1)) || (!SessionParticipante.FlConfirmacaoInscricao))
                        mnuAtividades.Visible = mnuAtividadesENUS.Visible = mnuAtividadesESP.Visible = true;

                    if ((SessionEvento.CdEvento == "005503") &&
                        (SessionParticipante.Categoria.CdCategoria == "00550301") )
                    {
                        DataTable DTAtivp = oInscricoes.ListarAtividadesDoParticipante(SessionParticipante, SessionCnn);
                        if (((DTAtiv != null) && (DTAtiv.Rows.Count == 2)))
                            mnuAtividades.Visible = false;
                    }

                    PedidoCad oPedidoCad = new PedidoCad();
                    Pedido tempPedido = oPedidoCad.VerificaPedidoNaoPagoWeb(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);
                    if ((!SessionParticipante.FlConfirmacaoInscricao) && (tempPedido != null))
                    {
                        mnuPedidos.Visible = mnuPedidosENUS.Visible = mnuPedidosESP.Visible = true;

                        if ((tempPedido.TpPagamento.Contains("BOLETO")) &&
                            ((SessionEvento.FlEventoComRecebimentos) && (TiposPagamentoCad.VerificarFormaPagamento(SessionEvento.CdEvento, "BOLETO", SessionCnn)) &&
                            ((SessionEvento.DtFechamentoInscrWeb == null) || (SessionEvento.DtFechamentoInscrWeb >= Geral.datahoraServidor(SessionCnn))))
                             && ((SessionParticipante.NoPais == "") || (SessionParticipante.NoPais == "BRASIL") || (SessionParticipante.NoPais == "BRAZIL") || (SessionParticipante.NoPais == "BRASIL") || (SessionParticipante.NoPais == "BRÉSIL"))
                            )
                            mnuBoletos.Visible = mnuBoletosENUS.Visible = mnuBoletosESP.Visible = true;

                        if ((SessionEvento.CdEvento == "008501") && (tempPedido.TpPagamento != ""))
                            mnuAtividades.Visible = false;
                        
                            
                            
                            //if (SessionEvento.CdEvento == "007701")
                        //{
                        //    mnuBoletos.Visible = false;
                        //}


                    }
                //}

                mnuPedidos.Visible = mnuPedidosENUS.Visible = mnuPedidosESP.Visible = false;
                mnuDadosRecibos.Visible = mnuDadosRecibosENUS.Visible = mnuDadosRecibosESP.Visible = false;
                if ((SessionEvento.FlEmiteRecibo) && (SessionEvento.FlEventoComRecebimentos) && (SessionParticipante.Categoria.FlPagamento) && (SessionParticipante.Categoria.CdCategoria != "00550301") && (SessionParticipante.Categoria.CdCategoria != "00830303"))//(!SessionParticipante.FlConfirmacaoInscricao))
                {
                    mnuPedidos.Visible = mnuPedidosENUS.Visible = mnuPedidosESP.Visible = true;
                    if (SessionIdioma == "PTBR")
                        mnuDadosRecibos.Visible = mnuDadosRecibosENUS.Visible = mnuDadosRecibosESP.Visible = true;

                }
                else if ((!SessionEvento.FlEmiteRecibo) && (SessionEvento.FlEventoComRecebimentos) && (SessionParticipante.Categoria.FlPagamento) && (SessionParticipante.Categoria.CdCategoria != "00550301"))
                {
                    PedidoCad oPedidoCad2 = new PedidoCad();
                    DataTable tempPedido2 = oPedidoCad2.Listar(SessionEvento.CdEvento, SessionParticipante.CdParticipante, SessionCnn);

                    tempPedido2.DefaultView.RowFilter = "flAtivo = 1";

                    if ((tempPedido2 != null) && (tempPedido2.DefaultView.Count > 0))
                    {
                        mnuPedidos.Visible = true;
                       
                    }
                }

                mnuInscricoes.Visible = mnuInscricoesENUS.Visible = mnuInscricoesESP.Visible = false;
                if (SessionParticipante.FlConfirmacaoInscricao)
                    mnuInscricoes.Visible = mnuInscricoesENUS.Visible = mnuInscricoesESP.Visible = true;

                mnuEnvioDoc.Visible = mnuEnvioDocENUS.Visible = mnuEnvioDocESP.Visible = false;
                if ((SessionParticipante.Categoria.FlRequerConfirmacaoDoc) && (!SessionParticipante.FlDocumentoConfirmado))
                    mnuEnvioDoc.Visible = mnuEnvioDocENUS.Visible = mnuEnvioDocESP.Visible = true;

                mnuMeeting.Visible = mnuMeetingENUS.Visible = mnuMeetingESP.Visible = false;
                mnuMeetingAgenda.Visible = mnuMeetingAgendaENUS.Visible = mnuMeetingAgendaESP.Visible = false;
                mnuMeetingConvites.Visible = mnuMeetingConvitesENUS.Visible = mnuMeetingConvitesESP.Visible = false;
                if ((SessionEvento.FlMeeting) && (SessionParticipante.Categoria.FlParticiparMeeting) && (SessionParticipante.FlConfirmacaoInscricao))
                {
                    mnuMeeting.Visible = mnuMeetingENUS.Visible = mnuMeetingESP.Visible = true;

                    if ((SessionParticipante.meetingParticipante != null) && (SessionParticipante.meetingParticipante.DsPerfilEmpresaHTML != ""))
                    {
                        mnuMeetingAgenda.Visible = mnuMeetingAgendaENUS.Visible = mnuMeetingAgendaESP.Visible = true;
                        mnuMeetingConvites.Visible = mnuMeetingConvitesENUS.Visible = mnuMeetingConvitesESP.Visible = true;
                    }
                }
            }
            buscarTextoHtmlPatrocinadores();

        }
        catch
        { }
           
    }

    override protected void OnInit(EventArgs e)
    {

        //if (SessionCnn == null)
        //    SessionCnn = (SqlConnection)Session["SessionCnn"];
        //else
        //    Session["SessionCnn"] = SessionCnn;

        //if (SessionEvento == null)
        //    SessionEvento = (Evento)Session["SessionEvento"];
        //else
        //    Session["SessionEvento"] = SessionEvento;

        //if (SessionParticipante == null)
        //    SessionParticipante = (Participante)Session["SessionParticipante"];
        //else
        //    Session["SessionParticipante"] = SessionParticipante;

        //SessionIdioma = (String)Session["SessionIdioma"];
        //if (SessionIdioma == null)
        //    SessionIdioma = "PTBR";
        //Session["SessionIdioma"] = SessionIdioma;

        //SessionTipoSistema = (String)Session["SessionTipoSistema"];
        //if (SessionTipoSistema == null)
        //    SessionTipoSistema = "NRM";
        //Session["SessionTipoSistema"] = SessionTipoSistema;

        //SessionCateg = (String)Session["SessionCateg"];
        //if (SessionCateg == null)
        //    SessionCateg = "";
        //Session["SessionCateg"] = SessionCateg;
        
        //if (SessionIdioma == "ENUS")
        //{
        //    SessionIdioma = "ENUS";
        //    Session["SessionIdioma"] = SessionIdioma;

            /*
       mnuCad.InnerHtml = "<a href=\"frmCadastroAuto.aspx\" >Register</a>";
           mnuCadastro.InnerHtml = "<a href=\"frmCadastroAuto.aspx\" >Registration Data</a>";
           mnuEtiqueta.InnerHtml = "<a href=\"frmCredencial.aspx\" >Print Credential</a>";
           mnuEnvioDoc.InnerHtml = "<a href=\"frmEnviarDocumento.aspx\" >Send Document</a>";
           mnuSeguranca.InnerHtml = "<a href=\"frmNovaSenha.aspx\" >Change Password</a>";

       mnuInscr.InnerHtml = "<a href=\"#\" >Inscriptions</a>";
           mnuAtividades.InnerHtml = "<a href=\"frmSelAtividades.aspx\" >Sign up</a>";
           mnuPedidos.InnerHtml = "<a href=\"#\" >My Orders</a>";
               mnuPedidosListas.InnerHtml = "<a href=\"frmListarPedidos.aspx\" >My Orders List</a>";
               mnuBoletos.InnerHtml = "<a href=\"frmListaBoletosPedido.aspx\" >List Boletos</a>";
               mnuDadosRecibos.InnerHtml = "<a href=\"frmDadosRecibo.aspx\" >Change Data Receipt</a>";
           mnuInscricoes.InnerHtml = "<a href=\"frmAtividadesParticipante.aspx\" >My Enrollments</a>";

       mnuMeeting.InnerHtml = "<a href=\"#\" >Meeting (B2B)</a>"; 
           mnuMeetingPerfil.InnerHtml = "<a href=\"frmMeetingPerfil.aspx\" >Profile</a>"; 
           mnuMeetingAgenda.InnerHtml = "<a href=\"frmMeetingAgenda.aspx\" >Meetings Scheduled</a>"; 
           mnuMeetingConvites.InnerHtml = "<a href=\"#\" >Invitations Meeting</a>"; 
               mnuMeetingConvitesNovo.InnerHtml = "<a href=\"frmMeetingParticipantes.aspx\" >new Invitation</a>"; 
               mnuMeetingConvitesEnviados.InnerHtml = "<a href=\"frmMeetingConvitesEnviados.aspx\" >Invitations Sent</a>"; 
               mnuMeetingConvitesRecebidos.InnerHtml = "<a href=\"frmMeetingConvitesRecebidos.aspx\" >Received invitations</a>"; 
                                    

       mnuSair.InnerHtml = "<a href=\"frmSessaoExpirada.aspx\" >Log out</a>";      
            */
            /*
            smoothmenu1.InnerHtml =

                 "   <a class=\"toggleMenu\" href=\"#\">Menu</a>" +
                 "   <ul id=\"linhamenu\" runat=\"server\" class=\"menubar\">" +
                 "	   <li id=\"mnuCad\" runat=\"server\"><a href=\"#\">Register</a>" +
                 "		   <ul> " +
                 "			   <li id=\"mnuCadastro\" runat=\"server\"><a href=\"frmCadastroAuto.aspx\">Registration Data</a></li>" +
                 "			   <li id=\"mnuEnvioDoc\" runat=\"server\" visible=\"false\"><a href=\"frmEnviarDocumento.aspx\">Send Document</a></li>" +
                 "			   <li id=\"mnuEtiqueta\" runat=\"server\" visible=\"false\"><a href=\"frmCredencial.aspx\">Print Credential</a></li>" +
                 "			   <li id=\"mnuSeguranca\" runat=\"server\"><a href=\"frmNovaSenha.aspx\" >Change Password</a></li>" +
                 "		   </ul>" +
                 "	   </li>" +

                 "	   <li id=\"mnuInscr\" runat=\"server\" visible=\"true\"><a href=\"#\">Inscriptions</a>" +
                 "		   <ul>" +
                 "			   <li id=\"mnuAtividades\" runat=\"server\" visible=\"false\"><a href=\"frmSelAtividades.aspx\" >Sign up</a></li> " +
                 "			   <li id=\"mnuPedidos\" runat=\"server\" visible=\"false\"><a href=\"#\" >My Orders</a>" +
                 "				   <ul> " +
                 "					   <li id=\"mnuPedidosListas\" runat=\"server\"><a href=\"frmListarPedidos.aspx\" >My Orders List</a></li> " +
                 "					   <li id=\"mnuBoletos\" runat=\"server\" visible=\"false\"><a href=\"frmListaBoletosPedido.aspx\" >List Boletos</a></li> " +
                 "					   <li id=\"mnuDadosRecibos\" runat=\"server\" visible=\"false\"><a href=\"frmDadosRecibo.aspx\">Change Data Receipt</a></li> " +
                 "				   </ul>               " +
                 "			   </li>" +
                 "			   <li id=\"mnuInscricoes\" runat=\"server\" visible=\"false\"><a href=\"frmAtividadesParticipante.aspx\">My Enrollments</a></li> " +
                 "		   </ul>" +
                 "	   </li>              " +
                 "	   <li id=\"mnuMeeting\" runat=\"server\" visible=\"false\"><a href=\"#\">Meeting (B2B)</a> " +
                 "		   <ul> " +
                 "			   <li id=\"mnuMeetingPerfil\" runat=\"server\" ><a href=\"frmMeetingPerfil.aspx\">Profile</a></li> " +
                 "			   <li id=\"mnuMeetingAgenda\" runat=\"server\" visible=\"false\"><a href=\"frmMeetingAgenda.aspx\">Meetings Scheduled</a></li> " +
                 "			   <li id=\"mnuMeetingConvites\" runat=\"server\" visible=\"false\"><a href=\"#\" >Invitations Meeting</a> " +
                 "				   <ul> " +
                 "					   <li id=\"mnuMeetingConvitesNovo\" runat=\"server\" visible=\"true\"><a href=\"frmMeetingParticipantes.aspx\" >new Invitation</a></li> " +
                 "					   <li id=\"mnuMeetingConvitesEnviados\" runat=\"server\" visible=\"true\"><a href=\"frmMeetingConvitesEnviados.aspx\" >Invitations Sent</a></li> " +
                 "					   <li id=\"mnuMeetingConvitesRecebidos\" runat=\"server\" visible=\"true\"><a href=\"frmMeetingConvitesRecebidos.aspx\" >Received invitations</a></li> " +
                 "				   </ul> " +
                 "			   </li> " +
                 "		   </ul> " +
                 "	   </li>" +
                 "	   <li id=\"mnuSair\" runat=\"server\"><a href=\"frmSessaoExpirada.aspx\">Log out</a></li>" +
                 "   </ul>"
                ;*/

        //}
    }
    protected void buscarTextoHtmlPatrocinadores()
    {
        lblEspacoHtmlPatrocinadores.Visible = false;

        if (SessionEvento.CdEvento == "007002")
        {
            lblEspacoHtmlPatrocinadores.Visible = true;
            lblEspacoHtmlPatrocinadores.Text = "<div id=\"Geralsao\"><div id=\"patrocinadorGeral\"><div id=\"cohosted\">			<div id=\"seila\" ><div id=\"txtCohosted\"><strong>This conference is co-hosted by:</strong></div><div id=\"bio\"> <img src=\"../imagensgeral/007002/Logo%20BIO.jpg\" alt=\"bio\" width=\"329\" height=\"92\" /> </div>		<div id=\"biominas\"> <img src=\"../imagensgeral/007002/logo-25anos_en.jpg\" alt=\"biominas\" width=\"192\" height=\"92\" /></div></div></div><div id=\"strategicpartner\">		<div id=\"seila2\">			<div style=\"text-align: center;\"> <strong>STRATEGIC PARTNER:</strong></div><br />&nbsp;<div id=\"abiquim\"><img src=\"../imagensgeral/007002/AFLOGOABIQUIM_INGLES.png\" alt=\"abiquim\" width=\"200\" height=\"39\" /></div></div></div></div></div>";
        }

        if (SessionEvento.CdEvento == "012601")
        {
            lblEspacoHtmlPatrocinadores.Visible = true;

            if (SessionIdioma == "PTBR")
            lblEspacoHtmlPatrocinadores.Text =
                @"<div id='ropape-login' class='clsRodapePagina'>
	                <div id='texto-rodape' class='clsTextoRodapePagina'>
	                Para sanar dúvidas ou reportar problemas com o processo de inscrição 
	                escreva para a nossa equipe responsável pelo congresso e retornaremos 
	                o contato o mais brevemente possível.SAC: 
	                <a href='mailto:congresso@neuronus.com.br' class='sua-nomeclatura'>congresso@neuronus.com.br</a>
                  </div>
	              <div id='siganos' class='clsMidiaSocial'><span>Siga-nos</span>";
            else if (SessionIdioma == "ENUS")
                lblEspacoHtmlPatrocinadores.Text =
                    @"<div id='ropape-login' class='clsRodapePagina'>
	                    <div id='texto-rodape' class='clsTextoRodapePagina'>
	                    To answer questions or report problems with the registration process, write to our team responsible 
                        for the congress and we will get back to you as soon as possible..SAC: 
	                    <a href='mailto:congresso@neuronus.com.br' class='sua-nomeclatura'>congresso@neuronus.com.br</a>
                     </div>
	                 <div id='siganos' class='clsMidiaSocial'><span>Follow Us</span>";

            lblEspacoHtmlPatrocinadores.Text +=
                @"
	                <a href='http://www.twitter.com/NeuronusBR'><i class='fab fa-twitter' class='clstwitter'></i></a>
	                <a href='http://www.facebook.com/NeuronusBR'><i class='fab fa-facebook-f' class='clsfacebook'></i></a>
	                <a href='http://www.instagram.com/NeuronusBR'><i class='fab fa-instagram' class='clsinstagram'></i></a>
	               <!-- <a href='#'><i class='fab fa-youtube' class='clsyoutube'></i></a>-->
	                <div>
                </div>";
        }
        

    }

   

    protected string MontaMenu()
    {
        

        string sURL = Request.Url.ToString().ToLower();
        string tmpXpto = "";
        if (sURL.Contains("localhost"))
            tmpXpto = "http://" + Request.Url.Authority;
        
        string tmpMenu = "<a class=\"toggleMenu\" href=\"#\">Menu</a> " +
                         "<ul id=\"linhamenu\" runat=\"server\" class=\"menubar\">";

        tmpMenu += "<li id=\"mnuCadastro\" runat=\"server\"><a href=\"" + tmpXpto + "/frmCadastroAuto.aspx\" >Dados Cadastrais</a></li> ";
        if (SessionEvento.CdCliente != "0013")
        {
            tmpMenu += "<li id=\"mnuAtividades\" runat=\"server\"><a href=\"" + tmpXpto + "/frmSelAtividades.aspx\" >Inscreva-se</a></li> ";
            tmpMenu += "<li id=\"mnuPedidos\" runat=\"server\"><a href=\"" + tmpXpto + "/#\" >Acompanhar Inscrição</a> ";
            tmpMenu += "<ul> ";
            tmpMenu += "<li id=\"mnuPedidosListas\" runat=\"server\"><a href=\"" + tmpXpto + "/frmListarPedidos.aspx\" >Pedidos de Inscrição</a></li> ";
            if ((SessionEvento.FlEventoComRecebimentos) && (TiposPagamentoCad.VerificarFormaPagamento(SessionEvento.CdEvento, "BOLETO", SessionCnn)) &&
                ((SessionEvento.DtFechamentoInscrWeb == null) || (SessionEvento.DtFechamentoInscrWeb >= Geral.datahoraServidor(SessionCnn))))
            {
                tmpMenu += "<li id=\"mnuBoletos\" runat=\"server\"><a href=\"" + tmpXpto + "/frmListaBoletosPedido.aspx\" >Boletos</a></li> ";
            }
            if ((SessionEvento.FlEmiteRecibo) && (SessionEvento.FlEventoComRecebimentos))
            {
                tmpMenu += "<li id=\"mnuDadosRecibos\" runat=\"server\"><a href=\"" + tmpXpto + "/frmDadosRecibo.aspx\" >Alterar Dados Recibo</a></li> ";
            }
            tmpMenu += "</ul> ";
            tmpMenu += "</li> ";
        }
        else
        {
            if (Geral.datahoraServidor(SessionCnn) <= DateTime.Parse("30/07/2015 23:59:00"))
            {
                if (!SessionParticipante.FlConfirmacaoInscricao)
                    tmpMenu += "<li id=\"mnuAtividades\" runat=\"server\"><a href=\"" + tmpXpto + "/frmSelAtividades.aspx\" >Inscreva-se</a></li> ";
                else
                    tmpMenu += "<li id=\"mnuEtiqueta\" runat=\"server\"><a href=\"" + tmpXpto + "/frmCredencial.aspx\" >Imprima sua Credencial</a></li> ";
                tmpMenu += "<li id=\"mnuPedidos\" runat=\"server\"><a href=\"" + tmpXpto + "/#\" >Acompanhar Inscrição</a> ";
                tmpMenu += "<ul> ";
                if (!SessionParticipante.FlConfirmacaoInscricao)
                {
                    tmpMenu += "<li id=\"mnuPedidosListas\" runat=\"server\"><a href=\"" + tmpXpto + "/frmListarPedidos.aspx\" >Pedidos de Inscrição</a></li> ";
                    if ((SessionEvento.FlEventoComRecebimentos) && (TiposPagamentoCad.VerificarFormaPagamento(SessionEvento.CdEvento, "BOLETO", SessionCnn)) &&
                    ((SessionEvento.DtFechamentoInscrWeb == null) || (SessionEvento.DtFechamentoInscrWeb >= Geral.datahoraServidor(SessionCnn))))
                    {
                        tmpMenu += "<li id=\"mnuBoletos\" runat=\"server\"><a href=\"" + tmpXpto + "/frmListaBoletosPedido.aspx\" >Boletos</a></li> ";
                    }
                }

                if ((SessionEvento.FlEmiteRecibo) && (SessionEvento.FlEventoComRecebimentos))
                {
                    tmpMenu += "<li id=\"mnuDadosRecibos\" runat=\"server\"><a href=\"" + tmpXpto + "/frmDadosRecibo.aspx\" >Alterar Dados Recibo</a></li> ";
                }
                tmpMenu += "</ul> ";
                tmpMenu += "</li> ";
            }
        }

        tmpMenu += "<li id=\"mnuInscricoes\" runat=\"server\"><a href=\"" + tmpXpto + "/frmAtividadesParticipante.aspx\" >Minhas Inscrições/Atividades</a></li> ";

        //if ((SessionParticipante.Categoria.FlRequerConfirmacaoDoc) && (!SessionParticipante.FlDocumentoConfirmado))
        //    tmpMenu += "<li id=\"mnuEnvioDoc\" runat=\"server\"><a href=\"" + tmpXpto + "/frmEnviarDocumento.aspx\" >Enviar Documento</a></li> ";
        
        tmpMenu += "<li id=\"mnuSeguranca\" runat=\"server\"><a href=\"" + tmpXpto + "/frmNovaSenha.aspx\" >Alterar Senha</a></li> ";
        tmpMenu += "<li id=\"mnuSair\" runat=\"server\"><a href=\"" + tmpXpto + "/frmSessaoExpirada.aspx\" >Sair</a></li> ";
               
        tmpMenu += "</ul>";


        return tmpMenu;
    }


    protected void btnSair_Click(object sender, EventArgs e)
    {
        
       // FormsAuthentication.SignOut();
        

    }

    protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
    {
        //Response.Write("<script>window.open('frmCadastroAuto.aspx?cdMatricula=" + SessionParticipante.CdParticipante + "','_self');</script>");
    }

    private Control FindControlRecursive(Control root, string id)
    {
        if (root.ID == id)
        {
            return root;
        }

        foreach (Control c in root.Controls)
        {
            Control t = FindControlRecursive(c, id);
            if (t != null)
            {
                return t;
            }
        }

        return null;
    }
    protected void txtIdioma_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (txtIdioma.SelectedValue.ToString() == "PTBR")
        //{
        //    txtIdioma.SelectedIndex = 0;
        //    SessionIdioma = "PTBR";
        //    Session["SessionIdioma"] = SessionIdioma;
        //}
        //else if (txtIdioma.SelectedValue.ToString() == "ENUS")
        //{
        //    txtIdioma.SelectedIndex = 1;
        //    SessionIdioma = "ENUS";
        //    Session["SessionIdioma"] = SessionIdioma;
        //}
        //else if (txtIdioma.SelectedValue.ToString() == "ESP")
        //{
        //    txtIdioma.SelectedIndex = 2;
        //    SessionIdioma = "ESP";
        //    Session["SessionIdioma"] = SessionIdioma;
        //}
        //else if (txtIdioma.SelectedValue.ToString() == "FRA")
        //{
        //    txtIdioma.SelectedIndex = 3;
        //    SessionIdioma = "FRA";
        //    Session["SessionIdioma"] = SessionIdioma;
        //}
    }
    protected void txtIdioma_PreRender(object sender, EventArgs e)
    {
        //if (txtIdioma.SelectedValue.ToString() == "PTBR")
        //{
        //    txtIdioma.SelectedIndex = 0;
        //    SessionIdioma = "PTBR";
        //    Session["SessionIdioma"] = SessionIdioma;
        //}
        //else if (txtIdioma.SelectedValue.ToString() == "ENUS")
        //{
        //    txtIdioma.SelectedIndex = 1;
        //    SessionIdioma = "ENUS";
        //    Session["SessionIdioma"] = SessionIdioma;
        //}
        //else if (txtIdioma.SelectedValue.ToString() == "ESP")
        //{
        //    txtIdioma.SelectedIndex = 2;
        //    SessionIdioma = "ESP";
        //    Session["SessionIdioma"] = SessionIdioma;
        //}
        //else if (txtIdioma.SelectedValue.ToString() == "FRA")
        //{
        //    txtIdioma.SelectedIndex = 3;
        //    SessionIdioma = "FRA";
        //    Session["SessionIdioma"] = SessionIdioma;
        //}
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        try
        {
            Label1.Text = DateTime.Now.ToString();

            SessionCnn = (SqlConnection)Session["SessionCnn"];
            if (SessionCnn != null)
                Session["SessionCnn"] = SessionCnn;

            SessionEvento = (Evento)Session["SessionEvento"];
            if (SessionEvento != null)
                Session["SessionEvento"] = SessionEvento;

            SessionParticipante = (Participante)Session["SessionParticipante"];
            if (SessionParticipante != null)
                Session["SessionParticipante"] = SessionParticipante;

            SessionCategoria = (Categoria)Session["SessionCategoria"];
            if (SessionCategoria != null)
                Session["SessionCategoria"] = SessionCategoria;

            SessionIdioma = (String)Session["SessionIdioma"];
            if (SessionIdioma == null)
                SessionIdioma = "PTBR";
            Session["SessionIdioma"] = SessionIdioma;

            SessionCateg = (String)Session["SessionCateg"];
            if (SessionCateg != null)
                Session["SessionCateg"] = SessionCateg;

            SessionPedido = (Pedido)Session["SessionPedido"];
            if (SessionPedido != null)
                Session["SessionPedido"] = SessionPedido;

            SessionInscricoes = (Inscricoes)Session["SessionInscricoes"];
            if (SessionInscricoes != null)
                Session["SessionInscricoes"] = SessionInscricoes;

            SessionTese = (Tese)Session["SessionTese"];
            if (SessionTese != null)
                Session["SessionTese"] = SessionTese;

            SessionTeseParticipante = (TeseParticipante)Session["SessionTeseParticipante"];
            if (SessionTeseParticipante != null)
                Session["SessionTeseParticipante"] = SessionTeseParticipante;

            SessionAtividade = (Atividade)Session["SessionAtividade"];
            if (SessionAtividade != null)
                Session["SessionAtividade"] = SessionAtividade;
        }
        catch
        {

        }
    }

    /*
    private ActionResult SelectView(string viewName, object model, string outputType = "html")
    {
        if (outputType.ToLower() == "json")
        {
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        else
        {
            #if MOBILE
                  return View(viewName + "Mobile", model);
            #else
            if (Request.Browser.IsMobileDevice)
            {
                return View(viewName + "Mobile", model);
            }
            else
            {
                return View(viewName, model);
            }
            #endif
        }
    }
     * */
}
