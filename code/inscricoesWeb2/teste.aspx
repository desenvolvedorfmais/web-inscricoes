<%@ Page Language="C#" AutoEventWireup="true" CodeFile="teste.aspx.cs" Inherits="teste" %>
<%--<%@ Register TagPrefix="recaptcha" Namespace="Recaptcha" Assembly="Recaptcha" %>--%>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
*{margin:0; padding:0}
        </style>

    <%--<script type="text/javascript">
    var RecaptchaOptions = {
        lang : 'pt',
    };
</script>--%>
</head>
<body>
<div id="fb-root"></div>
<script>    (function (d, s, id) {
        var js, fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) return;
        js = d.createElement(s); js.id = id;
        js.src = "//connect.facebook.net/pt_BR/all.js#xfbml=1";
        fjs.parentNode.insertBefore(js, fjs);
    } (document, 'script', 'facebook-jssdk'));</script>

<div id="Div1"></div>
<script>    (function (d, s, id) {
        var js, fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) return;
        js = d.createElement(s); js.id = id;
        js.src = "//connect.facebook.net/pt_BR/all.js#xfbml=1";
        fjs.parentNode.insertBefore(js, fjs);
    } (document, 'script', 'facebook-jssdk'));</script>

    <form id="form1" runat="server">
    <div>
        &nbsp;<br />
        <br />
        <br />
        cdEvento&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblIdioma" runat="server" Text="Idioma"></asp:Label>
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             <asp:DropDownList ID="txtIdioma" runat="server" 
                                    >
                 <asp:ListItem Value="PTBR">Protuguês-Brasil</asp:ListItem>
                 <asp:ListItem Value="ENUS">English</asp:ListItem>
                 <asp:ListItem Value="ESP">Español</asp:ListItem>
                 <asp:ListItem Value="FRA">Français</asp:ListItem>
             </asp:DropDownList>
        <br />
        cdCategoria 
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <br />
        cdAtividade 
        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        <br />
        <br />
        cdAutorização 
        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Tipo Sistema"></asp:Label>
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             <asp:DropDownList ID="txtTipoSistema" runat="server" 
                                    >
                 <asp:ListItem Value="NRM">Normal</asp:ListItem>
                 <asp:ListItem Value="CRD">Credenciamento</asp:ListItem>
                 <asp:ListItem Value="PSQVINC">Pesquisa Vinculada</asp:ListItem>
                 <asp:ListItem Value="PSQAVS">Pesquisa Avulsa</asp:ListItem>
                 <asp:ListItem Value="VRFCAD">Verificar Cadastro</asp:ListItem>
             </asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="Label8" runat="server" Text="Nova Inscrição"></asp:Label>
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             <asp:DropDownList ID="txtTipoAcesso" runat="server" Width="60px" 
                                    >
                 <asp:ListItem Value="NRM">Nâo</asp:ListItem>
                 <asp:ListItem Value="NOVA">Sim</asp:ListItem>
             </asp:DropDownList>
        <br />
        <br />
        Inscrição de Referência 
        <asp:TextBox ID="txtInscrRef" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
        <br />
        <br />
        
        <div style="font-weight: bold; font-size: 18pt; vertical-align: middle; width: 296px;
            color: white; font-family: 'Arial Narrow'; height: 38px; background-color: gray; text-align: center; left: 0px; padding-top: 10px; position: relative; top: 0px;">
            000000009</div>
        <br />
        <%--<recaptcha:RecaptchaControl
            ID="recaptcha"
            runat="server"
            PublicKey="6Ld13OESAAAAAIGBV8eTtoWCNUCV5-q8CgO8DKIW"
            PrivateKey="6Ld13OESAAAAALsjQSVBofvOGAF_i436k2ttHLc1" Language="pt"
            />--%>

       <%-- <cc1:CaptchaControl ID="Captcha1" runat="server"
             CaptchaBackgroundNoise="Low" CaptchaLength="5"
             CaptchaHeight="60" CaptchaWidth="200"
             CaptchaLineNoise="None" CaptchaMinTimeout="5"
             CaptchaMaxTimeout="240" FontColor = "#529E00" />

        <asp:TextBox ID="txtCaptcha" runat="server"></asp:TextBox>--%>
        <br />

        <asp:Button ID="Button3" runat="server" Text="Button" onclick="Button3_Click" />
        <asp:Label ID="lblNome" runat="server"></asp:Label>
        <br />
        <br />
        <br />
        TRABALHOS TECNICOS<br />

        <asp:Button ID="Button5" runat="server" Text="Button" onclick="Button5_Click" />
        <br />
        <br />
        Programação<br />

        <asp:Button ID="Button7" runat="server" Text="Button" onclick="Button7_Click" />
        <br />
        <br />
        Certificado<br />

        <asp:Button ID="Button8" runat="server" Text="Button" onclick="Button8_Click" />
        <br />
        <br />
        Certificado_2<br />

        <asp:Button ID="Button9" runat="server" Text="Button" onclick="Button9_Click" />
        <br />
        <br />
        Invoice<br />

        <asp:Button ID="Button11" runat="server" Text="Button" onclick="Button11_Click" />
        <br />
        <br />
        Dados Confirmação de Inscrição<br />

        <asp:Button ID="Button10" runat="server" Text="Button" OnClick="Button10_Click" />
        <br />
        <div class="fb-like" data-href="http://www.facebook.com/fazendo.mais" data-send="true" data-width="450" data-show-faces="true"></div>
        <br />
        <div class="fb-facepile" data-href="http://www.facebook.com/fazendo.mais" data-max-rows="1" data-width="300"></div>

        
        <br />
        <asp:Button ID="Button6" runat="server" Text="Button" onclick="Button6_Click" />
        <br />
        <br />
        <br />

        <asp:Button ID="Button4" runat="server" Text="Registro de Presença" OnClick="Button4_Click" />

        <br />
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>
        <br />
    </div>
    </form>
</body>
</html>
