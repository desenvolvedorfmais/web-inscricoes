using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using cllEventos;
using CLLFuncoes;

using System.Security.Cryptography;
using System.Text;

using System.Data.SqlClient;

public partial class logoff : System.Web.UI.Page
{
    Evento SessionEvento;
    //EventoCad oEventoCad = new EventoCad();

    SqlConnection SessionCnn;

    protected void Page_Load(object sender, EventArgs e)
    {
        SessionCnn = (SqlConnection)Session["SessionCnn"];
        SessionEvento = (Evento)Session["SessionEvento"];

        Response.Redirect("index.aspx?codEvento=" + cllEventos.Crypto.EncryptStringAES(SessionEvento.CdEvento));
    }
}