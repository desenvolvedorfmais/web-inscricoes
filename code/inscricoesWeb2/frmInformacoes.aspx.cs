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

using KrksaComponentes;
using KrksaComponentes.cllEventosClasses;
using cllEventos;
using CLLFuncoes;

using System.Data.SqlClient;

public partial class frmInformacoes : System.Web.UI.Page
{
    Participante SessionParticipante;
    ParticipanteCad oParticipanteCad = new ParticipanteCad();
    Evento SessionEvento;
    EventoCad oEventoCad = new EventoCad();

    ClsFuncoes oClsFuncoes = new ClsFuncoes();

    SqlConnection SessionCnn;

    String SessionIdioma;
    String SessionPais;

    String SessionCateg;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {

                //if (SessionCnn == null)
                SessionCnn = (SqlConnection)Session["SessionCnn"];
                //else
                Session["SessionCnn"] = SessionCnn;

                //if (SessionParticipante == null)
                SessionParticipante = (Participante)Session["SessionParticipante"];
                //else
                Session["SessionParticipante"] = SessionParticipante;


                //if (SessionEvento == null)
                SessionEvento = (Evento)Session["SessionEvento"];

                SessionCateg = (String)Session["SessionCateg"];
                if (SessionCateg == null)
                    SessionCateg = "";
                Session["SessionCateg"] = SessionCateg;

                //else
                Session["SessionEvento"] = SessionEvento;

                if (SessionEvento == null)
                    Server.Transfer("frmSessaoExpirada.aspx", true);

                SessionIdioma = (String)Session["SessionIdioma"];
                if ((SessionIdioma == null) || (SessionIdioma == ""))
                    SessionIdioma = "PTBR";
                Session["SessionIdioma"] = SessionIdioma;

                SessionPais = (String)Session["SessionPais"];
                if ((SessionPais == null) || (SessionPais == ""))
                    SessionPais = "";
                Session["SessionPais"] = SessionPais;

                if (SessionIdioma == "PTBR")
                    lblInformacoesCompletas.Text = SessionEvento.DsInformacoesCompletasWeb_ptbr;
                else if (SessionIdioma == "ENUS")
                    lblInformacoesCompletas.Text = SessionEvento.DsInformacoesCompletasWeb_enus;
                else if (SessionIdioma == "ESP")
                    lblInformacoesCompletas.Text = SessionEvento.DsInformacoesCompletasWeb_esp;
                else if (SessionIdioma == "FRA")
                    lblInformacoesCompletas.Text = SessionEvento.DsInformacoesCompletasWeb_fra;

                if (SessionEvento.CdEvento == "002501")
                {
                    btnCadastrar.Text = "Cadastro para comprar ingresso";
                    btnCadastrar2.Text = "Cadastro para comprar ingresso";
                }

                if (SessionEvento.CdEvento == "007002")
                {
                    if ((SessionCateg == "00700202") || (SessionCateg == "00700204") || (SessionCateg == "00700206"))
                        Response.Write("<script>window.open('frmCadastroAuto.aspx','_self');</script>");
                    else
                        lblInformacoesCompletas.Text = lblInformacoesCompletas.Text.Replace("#inforCateg", Geral.TermosBiominas(SessionCateg));
                }
            }
            catch
            {
                //txtMsg.Text = "erro";
            }
        }
        else
        {
            SessionParticipante = (Participante)Session["SessionParticipante"];

            SessionEvento = (Evento)Session["SessionEvento"];

            SessionCnn = (SqlConnection)Session["SessionCnn"];

            SessionIdioma = (String)Session["SessionIdioma"];

            SessionPais = (String)Session["SessionPais"];

            SessionCateg = (String)Session["SessionCateg"];

            if (SessionEvento == null)
                Server.Transfer("frmSessaoExpirada.aspx", true);
        }
        try
        {
            verificarIdioma(SessionIdioma);

            TSManager1.RegisterPostBackControl(btnCadastrar2);
            TSManager1.RegisterPostBackControl(btnCadastrar);
        }
        catch
        {
            //txtMsg.Text = "erro";
        }
    }
    protected void btnCadastrar_Click(object sender, EventArgs e)
    {

        if ((SessionEvento.FlPesquisaCPFReceita) && (SessionIdioma == "PTBR"))
        {
            CategoriaCad oCategoriaCad = new CategoriaCad();
            Categoria tmpCategoria = oCategoriaCad.Pesquisar(SessionEvento.CdEvento, SessionCateg, SessionCnn);

            if ((tmpCategoria == null) || (!tmpCategoria.FlCPFCNPJObrigatorio))
                Response.Write("<script>window.open('frmCadastroAuto.aspx','_self');</script>");
            else
                Response.Write("<script>window.open('frmBuscaCPFReceita.aspx','_self');</script>");
        }
        else
            Response.Write("<script>window.open('frmCadastroAuto.aspx','_self');</script>");
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        btnCadastrar.Enabled = !btnCadastrar.Enabled;
        btnCadastrar2.Enabled = !btnCadastrar2.Enabled;
    }

    protected void verificarIdioma(string prmIdioma)
    {
        //DropDownList txtidioma = (DropDownList)Page.Master.FindControl("txtIdioma");
        // if (txtidioma != null)
        //{

        if ((prmIdioma == null) || (prmIdioma == ""))
        {
            prmIdioma = "PTBR";
            SessionIdioma = "PTBR";
        }
        Session["SessionIdioma"] = SessionIdioma;

        if (prmIdioma == "PTBR")//(txtidioma.SelectedIndex == 0)
        {
            SessionIdioma = "PTBR";
            Session["SessionIdioma"] = SessionIdioma;


            CheckBox1.Text = " Li e concordo com os termos acima.";

            btnCadastrar.Text = btnCadastrar2.Text = "Quero me Inscrever";
        }
        else if (prmIdioma == "ENUS")//(txtidioma.SelectedIndex == 1)
        {
            SessionIdioma = "ENUS";
            Session["SessionIdioma"] = SessionIdioma;

            CheckBox1.Text = " I have read and agree to the terms above.";

            btnCadastrar.Text = btnCadastrar2.Text = "Sign up";
        }
        else if (prmIdioma == "ESP")//(txtidioma.SelectedIndex == 2)
        {
            SessionIdioma = "ESP";
            Session["SessionIdioma"] = SessionIdioma;

            CheckBox1.Text = " He leido y estoy de acuerdo con los términos anteriores.";
            btnCadastrar.Text = btnCadastrar2.Text = "Quiero registrarme";
        }
        else if (prmIdioma == "FRA")//(txtidioma.SelectedIndex == 3)
        {
            SessionIdioma = "FRA";
            Session["SessionIdioma"] = SessionIdioma;

            CheckBox1.Text = " J'ai lu et j'accepte les conditions ci-dessus.";
            btnCadastrar.Text = btnCadastrar2.Text = "Je veux m'inscrire";
        }
        // }
    }

    private string TermosBiominas(string prmCdCategoria)
    {
        string retorno = "";


        switch (prmCdCategoria)
        {
            case "00700201":
                {
                    retorno = @"
                        <p><strong>BRAZIL &ndash; BIO/BIOMINAS MEMBER &ndash; GENERAL REGISTRATION</strong></p>
                        <p><strong> R$ 1.100,00 (Early-Bird) | R$ 1.300,00 (Regular)</strong></p>
                        <p>&nbsp;</p>
                        <p><strong>IMPORTANT INFORMATION:</strong></p>
                        <p><strong>BIO/Biominas member</strong></p>
                        <p>To find out if your company is a BIO member please review the&nbsp;<a href='https://www.bio.org/articles/bio-members-web-site-links'>BIO Member Directory</a>.&nbsp;</p>
                        <p>Will be considered as a Biominas member the companies participated in the BioStartup Lab program, are accelerated at GroWbio, or have incubated/graduated at Habitat Incubator.</p>
                        <p>Please contact our team if you have any questions or need assistance: <a href='mailto:bla@biominas.org.br'>bla@biominas.org.br</a>.</p>";
                    break;
                }
            case "00700203":
                {
                    retorno = @"
                        <p><strong>LATIN AMERICA &ndash; BIO/BIOMINAS MEMBER &ndash; GENERAL REGISTRATION</strong></p>
                        <p><strong> USD 350.00 (Early-Bird) | USD 450.00 (Regular)</strong></p>
                        <p>&nbsp;</p>
                        <p><strong>IMPORTANT INFORMATION:</strong></p>
                        <p><strong>BIO/Biominas member</strong></p>
                        <p>To find out if your company is a BIO member please review the&nbsp;<a href='https://www.bio.org/articles/bio-members-web-site-links'>BIO Member Directory</a>.&nbsp;</p>
                        <p>Will be considered as a Biominas member the companies participated in the BioStartup Lab program, are accelerated at GroWbio, or have incubated/graduated at Habitat Incubator.</p>
                        <p>Please contact our team if you have any questions or need assistance: <a href='mailto:bla@biominas.org.br'>bla@biominas.org.br</a>.</p>";
                    break;
                }
            case "00700205":
                {
                    retorno = @"
                        <p><strong>INTERNATIONAL &ndash; BIO/BIOMINAS MEMBER &ndash; GENERAL REGISTRATION</strong></p>
                        <p><strong> USD 1,100.00 (Early-Bird) | USD 1,300.00 (Regular)</strong></p>
                        <p>&nbsp;</p>
                        <p><strong>IMPORTANT INFORMATION:</strong></p>
                        <p><strong>BIO/Biominas member</strong></p>
                        <p>To find out if your company is a BIO member please review the&nbsp;<a href='https://www.bio.org/articles/bio-members-web-site-links'>BIO Member Directory</a>.&nbsp;</p>
                        <p>Will be considered as a Biominas member the companies participated in the BioStartup Lab program, are accelerated at GroWbio, or have incubated/graduated at Habitat Incubator.</p>
                        <p>Please contact our team if you have any questions or need assistance: <a href='mailto:bla@biominas.org.br'>bla@biominas.org.br</a>.</p>";
                    break;
                }
            case "00700207":
                {
                    retorno = @"
                    <p><strong>BRAZIL &ndash; BIO/BIOMINAS MEMBER &ndash; ACADEMIC/GOVERNMENT/NON-PROFIT REGISTRATION*</strong></p>
                    <p><strong> R$ 600,00 (Early-Bird) | R$ 800,00 (Regular)</strong></p>
                    <p>&nbsp;</p>
                    <p><strong>IMPORTANT INFORMATION:</strong></p>
                    <p><strong>BIO/Biominas member</strong></p>
                    <p>To find out if your company is a BIO member please review the&nbsp;<a href='https://www.bio.org/articles/bio-members-web-site-links'>BIO Member Directory</a>.&nbsp;</p>
                    <p>Will be considered as a Biominas member the companies participated in the BioStartup Lab program, are accelerated at GroWbio, or have incubated/graduated at Habitat Incubator.</p>
                    <p>Please contact our team if you have any questions or need assistance: <a href='mailto:bla@biominas.org.br'>bla@biominas.org.br</a>.</p>                    
                    <p><strong>*Academic/Government/Non-profit registration</strong></p>
                    <p>If your institution is categorized on this group and if you select this category, please be advised that you may be asked to provide more information about your institution. You have to register using your institutional information, including your institutional e-mail address.</p>
                    <p>All academic/government/non-profit registrations will be analyzed case-by-case and a notification will be sent within 48 hours. Your registration will only be confirmed after the producer&rsquo;s approval. We reserve the right to not approve a registration if we conclude that the institution doesn&rsquo;t fit in this category.&nbsp; In this case, you will be asked to pay the pricing for the correct category.</p>
                    <p>Given that this category already has a special pricing, it is not cumulative with any other discount.</p>";
                    break;
                }
            case "00700208":
                {
                    retorno = @"
                    <p><strong>BRAZIL – BIO/BIOMINAS NON-MEMBER – ACADEMIC/GOVERNMENT/NON-PROFIT REGISTRATION*</strong></p>
                    <p><strong> R$ 700,00 (Early-Bird) | R$ 900,00 (Regular)</strong></p>
                    <p>&nbsp;</p>
                    <p><strong>IMPORTANT INFORMATION:</strong></p>
                    <p><strong>*Academic/Government/Non-profit registration</strong></p>
                    <p>If your institution is categorized on this group and if you select this category, please be advised that you may be asked to provide more information about your institution. You have to register using your institutional information, including your institutional e-mail address.</p>
                    <p>All academic/government/non-profit registrations will be analyzed case-by-case and a notification will be sent within 48 hours. Your registration will only be confirmed after the producer&rsquo;s approval. We reserve the right to not approve a registration if we conclude that the institution doesn&rsquo;t fit in this category.&nbsp; In this case, you will be asked to pay the pricing for the correct category.</p>
                    <p>Given that this category already has a special pricing, it is not cumulative with any other discount.</p>";
                    break;
                }
            case "00700213":
                {
                    retorno = @"
                    <p><strong> BRAZIL &ndash; BIO/BIOMINAS MEMBER &ndash; STARTUP REGISTRATION* </strong></p>
                    <p><strong> R$ 500,00 (Early-Bird) | R$ 700,00 (Regular)</strong></p>
                    <p>&nbsp;</p>
                    <p><strong>IMPORTANT INFORMATION:</strong></p>
                    <p><strong>BIO/Biominas member</strong></p>
                    <p>To find out if your company is a BIO member please review the&nbsp;<a href='https://www.bio.org/articles/bio-members-web-site-links'>BIO Member Directory</a>.&nbsp;</p>
                    <p>Will be considered as a Biominas member the companies participated in the BioStartup Lab program, are accelerated at GroWbio, or have incubated/graduated at Habitat Incubator.</p>
                    <p>Please contact our team if you have any questions or need assistance: <a href='mailto:bla@biominas.org.br'>bla@biominas.org.br</a>.</p>
                    <p><strong>*Startup registration</strong></p>
                    <p>We consider as &ldquo;startup&rdquo;&nbsp;an&nbsp;organization&nbsp;formed to&nbsp;search&nbsp;for a&nbsp;repeatable&nbsp;and scalable business model with high potential to grow. If your organization fits this criteria and you select this registration category, please be advised that you may be asked to provide more information about your startup.</p>
                    <p>All startup registrations will be analyzed case-by-case and a notification will be sent within 48 hours. Your registration will only be confirmed after the producer&rsquo;s approval. We reserve the right to not approve a registration if we conclude that the institution doesn&rsquo;t fit in this category.&nbsp; In this case, you will be asked to pay the pricing for the correct category.</p>
                    <p>Given that this category already has a special pricing, it is not cumulative with any other discount.</p>";
                    break;
                }
            case "00700214":
                {
                    retorno = @"
                    <p><strong> BRAZIL &ndash; BIO/BIOMINAS NON-MEMBER &ndash; STARTUP REGISTRATION* </strong></p>
                    <p><strong> R$ 600,00 (Early-Bird) | R$ 900,00 (Regular)</strong></p>
                    <p>&nbsp;</p>
                    <p><strong>IMPORTANT INFORMATION:</strong></p>
                    <p><strong>*Startup registration</strong></p>
                    <p>We consider as &ldquo;startup&rdquo;&nbsp;an&nbsp;organization&nbsp;formed to&nbsp;search&nbsp;for a&nbsp;repeatable&nbsp;and scalable business model with high potential to grow. If your organization fits this criteria and you select this registration category, please be advised that you may be asked to provide more information about your startup.</p>
                    <p>All startup registrations will be analyzed case-by-case and a notification will be sent within 48 hours. Your registration will only be confirmed after the producer&rsquo;s approval. We reserve the right to not approve a registration if we conclude that the institution doesn&rsquo;t fit in this category.&nbsp; In this case, you will be asked to pay the pricing for the correct category.</p>
                    <p>Given that this category already has a special pricing, it is not cumulative with any other discount.</p>";
                    break;
                }
            case "00700215":
                {
                    retorno = @"
                    <p><strong>LATIN AMERICA &ndash; BIO/BIOMINAS MEMBER &ndash; ACADEMIC/GOVERNMENT/NON-PROFIT REGISTRATION*</strong></p>
                    <p><strong> USD 200,00 (Early-Bird) | R$ 280,00 (Regular)</strong></p>
                    <p>&nbsp;</p>
                    <p><strong>IMPORTANT INFORMATION:</strong></p>
                    <p><strong>BIO/Biominas member</strong></p>
                    <p>To find out if your company is a BIO member please review the&nbsp;<a href='https://www.bio.org/articles/bio-members-web-site-links'>BIO Member Directory</a>.&nbsp;</p>
                    <p>Will be considered as a Biominas member the companies participated in the BioStartup Lab program, are accelerated at GroWbio, or have incubated/graduated at Habitat Incubator.</p>
                    <p>Please contact our team if you have any questions or need assistance: <a href='mailto:bla@biominas.org.br'>bla@biominas.org.br</a>.</p>                    
                    <p><strong>*Academic/Government/Non-profit registration</strong></p>
                    <p>If your institution is categorized on this group and if you select this category, please be advised that you may be asked to provide more information about your institution. You have to register using your institutional information, including your institutional e-mail address.</p>
                    <p>All academic/government/non-profit registrations will be analyzed case-by-case and a notification will be sent within 48 hours. Your registration will only be confirmed after the producer&rsquo;s approval. We reserve the right to not approve a registration if we conclude that the institution doesn&rsquo;t fit in this category.&nbsp; In this case, you will be asked to pay the pricing for the correct category.</p>
                    <p>Given that this category already has a special pricing, it is not cumulative with any other discount.</p>";
                    break;
                }
            case "00700216":
                {
                    retorno = @"
                    <p><strong>LATIN AMERICA – BIO/BIOMINAS NON-MEMBER – ACADEMIC/GOVERNMENT/NON-PROFIT REGISTRATION*</strong></p>
                    <p><strong> USD 230,00 (Early-Bird) | USD 300,00 (Regular)</strong></p>
                    <p>&nbsp;</p>
                    <p><strong>IMPORTANT INFORMATION:</strong></p>
                    <p><strong>*Academic/Government/Non-profit registration</strong></p>
                    <p>If your institution is categorized on this group and if you select this category, please be advised that you may be asked to provide more information about your institution. You have to register using your institutional information, including your institutional e-mail address.</p>
                    <p>All academic/government/non-profit registrations will be analyzed case-by-case and a notification will be sent within 48 hours. Your registration will only be confirmed after the producer&rsquo;s approval. We reserve the right to not approve a registration if we conclude that the institution doesn&rsquo;t fit in this category.&nbsp; In this case, you will be asked to pay the pricing for the correct category.</p>
                    <p>Given that this category already has a special pricing, it is not cumulative with any other discount.</p>";
                    break;
                }
            case "00700221":
                {
                    retorno = @"
                    <p><strong> LATIN AMERICA &ndash; BIO/BIOMINAS MEMBER &ndash; STARTUP REGISTRATION* </strong></p>
                    <p><strong> USD 170,00 (Early-Bird) | USD 230,00 (Regular)</strong></p>
                    <p>&nbsp;</p>
                    <p><strong>IMPORTANT INFORMATION:</strong></p>
                    <p><strong>BIO/Biominas member</strong></p>
                    <p>To find out if your company is a BIO member please review the&nbsp;<a href='https://www.bio.org/articles/bio-members-web-site-links'>BIO Member Directory</a>.&nbsp;</p>
                    <p>Will be considered as a Biominas member the companies participated in the BioStartup Lab program, are accelerated at GroWbio, or have incubated/graduated at Habitat Incubator.</p>
                    <p>Please contact our team if you have any questions or need assistance: <a href='mailto:bla@biominas.org.br'>bla@biominas.org.br</a>.</p>
                    <p><strong>*Startup registration</strong></p>
                    <p>We consider as &ldquo;startup&rdquo;&nbsp;an&nbsp;organization&nbsp;formed to&nbsp;search&nbsp;for a&nbsp;repeatable&nbsp;and scalable business model with high potential to grow. If your organization fits this criteria and you select this registration category, please be advised that you may be asked to provide more information about your startup.</p>
                    <p>All startup registrations will be analyzed case-by-case and a notification will be sent within 48 hours. Your registration will only be confirmed after the producer&rsquo;s approval. We reserve the right to not approve a registration if we conclude that the institution doesn&rsquo;t fit in this category.&nbsp; In this case, you will be asked to pay the pricing for the correct category.</p>
                    <p>Given that this category already has a special pricing, it is not cumulative with any other discount.</p>";
                    break;
                }
            case "00700222":
                {
                    retorno = @"
                    <p><strong> LATIN AMERICA &ndash; BIO/BIOMINAS NON-MEMBER &ndash; STARTUP REGISTRATION* </strong></p>
                    <p><strong> USD 200,00 (Early-Bird) | USD 280,00 (Regular)</strong></p>
                    <p>&nbsp;</p>
                    <p><strong>IMPORTANT INFORMATION:</strong></p>
                    <p><strong>*Startup registration</strong></p>
                    <p>We consider as &ldquo;startup&rdquo;&nbsp;an&nbsp;organization&nbsp;formed to&nbsp;search&nbsp;for a&nbsp;repeatable&nbsp;and scalable business model with high potential to grow. If your organization fits this criteria and you select this registration category, please be advised that you may be asked to provide more information about your startup.</p>
                    <p>All startup registrations will be analyzed case-by-case and a notification will be sent within 48 hours. Your registration will only be confirmed after the producer&rsquo;s approval. We reserve the right to not approve a registration if we conclude that the institution doesn&rsquo;t fit in this category.&nbsp; In this case, you will be asked to pay the pricing for the correct category.</p>
                    <p>Given that this category already has a special pricing, it is not cumulative with any other discount.</p>";
                    break;
                }
            default:
                break;
        }

        return retorno;
    }
}
