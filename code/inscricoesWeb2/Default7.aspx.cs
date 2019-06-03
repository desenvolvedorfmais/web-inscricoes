using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default7 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FormasDePagamento2();
        }

       // ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", "<script type='text/javascript'> $(document).ready(function (e) {try {$(\"#ctl00_ContentPlaceHolder1_webmenu\").msDropdown({ visibleRows: 4 });} catch (e) {alert(e.message);}}); </script>", false);

        

    }

    protected void FormasDePagamento2()
    {
        /*
        TiposPagamentoCad oTiposPagamentoCad = new TiposPagamentoCad();
        //TxtFormaPagamento.DataSource = oTiposPagamentoCad.ListarWeb(SessionEvento.CdEvento, SessionCnn);
        DataTable dt = oTiposPagamentoCad.ListarWeb(SessionEvento.CdEvento, SessionCnn);
        this.webmenu.Items.Clear();

        for (int i = 0; i < dt.Rows.Count; i++ )
        {
            ListItem item = new ListItem(dt.DefaultView[i]["dsTipoPagamento"].ToString(), dt.DefaultView[i]["cdTipoPagamento"].ToString());
            item.Attributes.Add("title", ResolveUrl("~/imagensgeral/") + "visa.gif");//idioma.Sigla + ".gif");
            this.webmenu.Items.Add(item);
        }  

        */



        //TxtFormaPagamento.DataTextField = "dsTipoPagamento";
        //TxtFormaPagamento.DataValueField = "cdTipoPagamento";
        //TxtFormaPagamento.DataBind();

        this.webmenu.Items.Clear();

        ListItem item = new ListItem("Forma de pagamento", "");
        item.Attributes.Add("title", ResolveUrl("~/imagensgeral/") + "visa.gif");//idioma.Sigla + ".gif");
        item.Attributes.Add("data-description", "Selecione a forma de pagamento...");
        this.webmenu.Items.Add(item);

        ListItem item2 = new ListItem("Amex", "amex");
        item2.Attributes.Add("title", ResolveUrl("~/imagensgeral/") + "visa.gif");//idioma.Sigla + ".gif");
        item2.Attributes.Add("data-description", "Minha vida. Meu cartão...");
        this.webmenu.Items.Add(item2);

        ListItem item3 = new ListItem("Visa", "visa");
        item3.Attributes.Add("title", ResolveUrl("~/imagensgeral/") + "visa.gif");//idioma.Sigla + ".gif");
        item3.Attributes.Add("data-description", "Vai de visa...");
        this.webmenu.Items.Add(item3);


        //<asp:ListItem Value="" data-description="Choos your payment gateway">Payment Gateway</asp:ListItem>
        //    <asp:ListItem value="amex" title="img/visa.gif" data-description="My life. My card...">Amex</asp:ListItem>
        //    <asp:ListItem value="Discover" title="img/visa.gif" data-description="It pays to Discover...">Discover</asp:ListItem>
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {

    }
}