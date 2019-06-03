using System;



//using System.Security.Cryptography;

using CLLFuncoes;
using cllEventos;


//using MSXML2;

using System.Xml;
//using Recaptcha;
using MSCaptcha;
using BoletoFacilSDK;
using BoletoFacilSDK.Model.Entities;
using BoletoFacilSDK.Model.Response;
using BoletoFacilSDK.Enums;
using System.Diagnostics;
using System.Linq;
using System.Globalization;
using System.Threading;

public partial class teste : System.Web.UI.Page
{
    CLLFuncoes.ClsFuncoes oClsFuncoes = new ClsFuncoes();
    //Crypto crpt = new Crypto();
    protected void Page_Load(object sender, EventArgs e)
    {
        //Label2.Text = HttpContext.Current.Request.Url.Scheme;
        //Label3.Text = HttpContext.Current.Request.Url.Host;
        //Label4.Text = (HttpContext.Current.Request.Url.Port == 80 ? string.Empty : ":" + HttpContext.Current.Request.Url.Port);
        //Label5.Text = HttpContext.Current.Request.Url.ToString();
        //Label6.Text = HttpContext.Current.Request.ApplicationPath;
        //Label7.Text = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);

        //try
        //{
        //    // Cria uma instância do SDK que irá enviar requisições ao ambiente de testes do Boleto Fácil (Sandbox)
        //    BoletoFacil boletoFacil = new BoletoFacil(BoletoFacilEnvironment.Production,
        //        "3BD9A27F5B001F531A6133FCB1815F99FE9FD125C322054F63B561387730A315"); // XYZ12345 is the API key
        //    Payer payer = new Payer();
        //    payer.Name = "Pagador teste - SDK .NET";
        //    payer.CpfCnpj = "16314105129";
        //    payer.Email = "allermeest@highground.store";

        //    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        //    Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

        //    Charge charge = new Charge();
        //    charge.Description = "Cobrança teste gerada pelo SDK .NET";
        //    charge.Amount = 15.0m;//Convert.ToDecimal(5.0M, new CultureInfo("en-US"));
        //    //decimal amount = 15.0m;
        //    //int temp = 0;
        //    //try
        //    //{
        //    //    temp = Int32.Parse(amount.ToString()) ; 
        //    //    charge.Amount = Convert.ToInt32(amount);
        //    //}
        //    //catch (Exception exception)
        //    //{
        //    //    charge.Amount = amount;
        //    //}

        //    //if ((amount.ToString(CultureInfo.InvariantCulture)).Split('.')[1] == null &&
        //    //    (amount.ToString(CultureInfo.InvariantCulture)).Split('.')[1].Equals(0))
        //    //{
        //    //    charge.Amount = Convert.ToInt32(amount);
        //    //}
        //    //charge.Amount = (amount.ToString(CultureInfo.InvariantCulture)).Split('.')[1] == null ||
        //    //                (amount.ToString(CultureInfo.InvariantCulture)).Split('.')[1].Equals(0) ? Convert.ToInt32(amount) : amount;

        //    charge.Payer = payer;
            
        //    ChargeResponse response = boletoFacil.IssueCharge(charge);
        //    // foreach (Charge c in response.Data.Charges) {
        //    //     Console.WriteLine (c);
        //    // }
        //    // string resultBoletoResponseJson = JsonConvert.SerializeObject(response.Data.Charges, Formatting.Indented);
        //    //     var transactation = JsonConvert.DeserializeObject<Charge>(resultBoletoResponseJson);
        //    Debug.WriteLine(response.Data.Charges.FirstOrDefault());
        //}
        //catch (Exception ex)
        //{

        //    Debug.WriteLine(ex.Message);

       

        //}
    }

    protected void Button1_Click(object sender, EventArgs e)
        {
            Session["oDTPesquisa"] = null;
            Session["SessionCnn"] = null;
            Response.Redirect("index.aspx?codEvento=" + cllEventos.Crypto.EncryptStringAES(TextBox1.Text) +
                "&cdLng=" + txtIdioma.SelectedValue.ToString() +
                "&cat=" + TextBox2.Text +
                "&atv=" + TextBox3.Text +
                "&keyAut=" + TextBox4.Text +
                "&tpSist=" + txtTipoSistema.SelectedValue.ToString() +
                "&tpAcesso=" + txtTipoAcesso.SelectedValue.ToString() +
                (txtInscrRef.Text.Trim() != "" ? "&refInscr=" + cllEventos.Crypto.EncryptStringAES(txtInscrRef.Text) : ""));
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Session["oDTPesquisa"] = null;
            Session["SessionCnn"] = null;
            Response.Redirect("frmExpoepi.aspx?codEvento=" + cllEventos.Crypto.EncryptStringAES(TextBox1.Text) + "&cdLng=" + txtIdioma.SelectedValue.ToString() + "&cat=" + TextBox2.Text + "&atv=" + TextBox3.Text);
        }
        protected void Button3_Click(object sender, EventArgs e)
        {


            //Captcha1.ValidateCaptcha(txtCaptcha.Text.Trim());
            ////if (Page.IsValid)
            //if (Captcha1.UserValidated)
            //{
            //    string msgErro, Nome;

            //    msgErro = Nome = "";

            //    DataSet ds = new DataSet();
            //    ds.ReadXml("http://ws.fontededados.com.br/consulta.asmx/SituacaoCadastralPF?login=teste&senha=teste&cpf=61028940149");
            //    string tn = ds.Tables[0].TableName;
            //    if (ds != null)
            //    {
            //        if (ds.Tables[0].Rows.Count > 0)
            //        {
            //            if (ds.Tables[0].Rows[0]["MensagemErro"].ToString() != "")
            //                lblNome.Text = ds.Tables[0].Rows[0]["MensagemErro"].ToString();
            //            else
            //                lblNome.Text = ds.Tables[0].Rows[0]["Nome"].ToString();
            //        }
            //    }
            //}
            //else
            //    lblNome.Text = "Caracteres inválidos!";
        }
        protected void Button5_Click(object sender, EventArgs e)
        {
            Session["oDTPesquisa"] = null;
            Session["SessionCnn"] = null;
            Response.Redirect("frmCadastrarTrabalhosTecnicos.aspx?codEvento=" + cllEventos.Crypto.EncryptStringAES(TextBox1.Text) + "&cdLng=" + txtIdioma.SelectedValue.ToString() + "&cat=" + TextBox2.Text + "&atv=" + TextBox3.Text + "&keyAut=" + TextBox4.Text);
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            Session["oDTPesquisa"] = null;
            Session["SessionCnn"] = null;
            Response.Redirect("frmVerificaCPFCadastro.aspx?codEvento=" + cllEventos.Crypto.EncryptStringAES(TextBox1.Text) + "&cdLng=" + txtIdioma.SelectedValue.ToString() + "&cat=" + TextBox2.Text + "&atv=" + TextBox3.Text + "&keyAut=" + TextBox4.Text + "&tpSist=" + txtTipoSistema.SelectedValue.ToString() + "&tpRotina=");
        }
        protected void Button7_Click(object sender, EventArgs e)
        {
            Session["oDTAtividades"] = null;
            Session["SessionCnn"] = null;
            Response.Redirect("frmProgramacao.aspx?codEvento=" + cllEventos.Crypto.EncryptStringAES(TextBox1.Text) + "&cdLng=" + txtIdioma.SelectedValue.ToString());
        }



        protected void Button8_Click(object sender, EventArgs e)
        {
            Session["oDTPesquisa"] = null;
            Session["SessionCnn"] = null;
            Response.Redirect("frmVerificaCPFCadastro.aspx?codEvento=" + cllEventos.Crypto.EncryptStringAES(TextBox1.Text) + "&cdLng=" + txtIdioma.SelectedValue.ToString() + "&cat=" + TextBox2.Text + "&atv=" + TextBox3.Text + "&keyAut=" + TextBox4.Text + "&tpSist=" + txtTipoSistema.SelectedValue.ToString() + "&tpRotina=EMTCERT");
        }

        protected void Button9_Click(object sender, EventArgs e)
        {


            Session["oDTPesquisa"] = null;
            Session["SessionCnn"] = null;
            Response.Redirect("frmEmitirCertificado.aspx?codEvento=" + cllEventos.Crypto.EncryptStringAES(TextBox1.Text) + "&cdLng=" + txtIdioma.SelectedValue.ToString() + "&cat=" + TextBox2.Text + "&atv=" + TextBox3.Text + "&keyAut=" + TextBox4.Text + "&tpSist=" + txtTipoSistema.SelectedValue.ToString() + "&tpRotina=EMTCERT");
        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            Session["oDTPesquisa"] = null;
            Session["SessionCnn"] = null;
            Response.Redirect("frmRegistrarPresenca.aspx?codEvento=" + cllEventos.Crypto.EncryptStringAES(TextBox1.Text) + "&cdLng=" + txtIdioma.SelectedValue.ToString() + "&cat=" + TextBox2.Text + "&atv=" + TextBox3.Text + "&keyAut=" + TextBox4.Text + "&tpSist=" + txtTipoSistema.SelectedValue.ToString() + "&tpRotina=RegistrarPresenca");
        }
        protected void Button10_Click(object sender, EventArgs e)
        {
            Session["oDTPesquisa"] = null;
            Session["SessionCnn"] = null;
            Response.Redirect("frmVerificaCPFCadastro.aspx?codEvento=" + cllEventos.Crypto.EncryptStringAES(TextBox1.Text) + "&cdLng=" + txtIdioma.SelectedValue.ToString() + "&cat=" + TextBox2.Text + "&atv=" + TextBox3.Text + "&keyAut=" + TextBox4.Text + "&tpSist=" + txtTipoSistema.SelectedValue.ToString() + "&tpRotina=DadosConf");
        }
        protected void Button11_Click(object sender, EventArgs e)
        {
            Session["oDTPesquisa"] = null;
            Session["SessionCnn"] = null;
            Response.Redirect("frmVerificaCPFCadastro.aspx?codEvento=" + cllEventos.Crypto.EncryptStringAES(TextBox1.Text) + "&cdLng=" + txtIdioma.SelectedValue.ToString() + "&cat=" + TextBox2.Text + "&atv=" + TextBox3.Text + "&keyAut=" + TextBox4.Text + "&tpSist=" + txtTipoSistema.SelectedValue.ToString() + "&tpRotina=INVOICE");
        }
    }
