<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPagamentoItau.aspx.cs"
    Inherits="frmPagamentoItau" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Inscri&ccedil;&otilde;es Web</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="img/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="img/favicon.ico" type="image/ico" />
    <link id="Estilo" href="css/StyleSheet2.css" rel="stylesheet" type="text/css" />
    <%--<script type="text/javascript" src="../scripts/JSCFuncoes.js"></script>--%>

    <script type="text/javascript" src="scripts/JSCFuncoes.js"></script>

    <script type="text/JavaScript">
    <!--
    function MM_preloadImages() { //v3.0
      var d=document; 
      if(d.images)
      { 
        if(!d.MM_p) 
            d.MM_p=new Array();
        
        var i,j=d.MM_p.length,a=MM_preloadImages.arguments; 
        for(i=0; i<a.length; i++)
        if (a[i].indexOf("#")!=0)
        { 
            d.MM_p[j]=new Image; 
            d.MM_p[j++].src=a[i];
        }
      }
    }

    function MM_swapImgRestore() { //v3.0
      var i,x,a=document.MM_sr; 
      for(i=0;a&&i<a.length&&(x=a[i])&&x.oSrc;i++) 
        x.src=x.oSrc;
    }

    function MM_findObj(n, d) { //v4.01
      var p,i,x;  
      if(!d) 
        d=document; 
      if((p=n.indexOf("?"))>0&&parent.frames.length) 
      {
        d=parent.frames[n.substring(p+1)].document; 
        n=n.substring(0,p);
      }
      if(!(x=d[n])&&d.all) 
        x=d.all[n]; 
      for (i=0;!x&&i<d.forms.length;i++) 
        x=d.forms[i][n];
      for(i=0;!x&&d.layers&&i<d.layers.length;i++) 
        x=MM_findObj(n,d.layers[i].document);
      if(!x && d.getElementById) 
        x=d.getElementById(n); 
        
      return x;
    }

    function MM_swapImage() { //v3.0
      var i,j=0,x,a=MM_swapImage.arguments; 
      document.MM_sr=new Array; 
      for(i=0;i<(a.length-2);i+=3)
        if ((x=MM_findObj(a[i]))!=null)
        {
          document.MM_sr[j++]=x; 
          if(!x.oSrc) 
            x.oSrc=x.src; 
            
          x.src=a[i+2];
        }
    }

    // Comma separated list of images to rotate 
    var imgs = new Array('images/1.jpg','images/2.jpg','images/3.jpg','images/4.jpg','images/5.jpg');
    // delay in milliseconds between image swaps 1000 = 1 second 
    var delay = 7000;
    var counter = 0;

    function preloadImgs(){
	    for(var i=0;i<imgs.length;i++){
		    MM_preloadImages(imgs[i]);
	    }
    }

    function randomImages(){
	    if(counter == (imgs.length)){
		    counter = 0;
	    }
	    MM_swapImage('rotator', '', imgs[counter++]);
	    setTimeout('randomImages()', delay);
    }

    //-->
    </script>

</head>
<body>
    <%--<%
        string dados = Request["prmDados"];
    %>--%>
    <form id="form1" runat="server" method="post" action="https://shopline.itau.com.br/shopline/shopline.asp"
        onsubmit="carregabrw()" target="SHOPLINE" name="form1">
        <div id="geral" style="left: 3px; top: 3px">
            <div id="topo">
                <asp:Image ID="img_Topo" runat="server" Width="1240px" Height="130" />
            </div>
            <div id="conteudo">
                <div id="conteudoEsq" style="left: 0px; top: 0px">
                    <h4>
                        <asp:Label ID="lblRealizacao" runat="server" Text="Realização" Font-Size="16px"></asp:Label></h4>
                    <p>
                        <asp:Image ID="imgRealizacao" runat="server" />&nbsp;</p>
                </div>
                <div id="conteudo_meio">
                    <div id="frmCad_auto">
                        <h3>
                            Formas de Pagamento<br />
                        </h3>
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        &nbsp; &nbsp; &nbsp;&nbsp;
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/img/shopline_234x60.gif" />
                        <br /><br /><br />
                        <p><span style='font-family:Arial'>Prezado participante o Itaú é o banco responsável para gerenciar nossos recebimentos.</span></p>
                        <br />
                        <p><span style='font-family:Arial'>Para concluir sua inscrição você será direcionado para o Itaú Shopline, 
                        onde poderá selecionar a melhor forma de pagamento para você.</span></p><br /><br />
                        <input id="crip" type="hidden" name="DC" value="<%=Request["prmDados"] %>"/>
                        <%--<asp:TextBox ID="DC" runat="server" Visible="false"></asp:TextBox>--%>
                        <br />
                        <input type="submit" name="Shopline" value="Continuar"/>
                        <%--<asp:Button ID="Button1" runat="server" Text="Button" />--%>
                    </div>
                </div>
                <div id="conteudoDir" style="right: 5px">
                    <img src="img/vazia18x18.png" alt="rotator" id="rotator" />
                </div>
            </div>
        </div>
    </form>
    <div id="rodape">
            <%--<a href="http://www.fazendomais.com.br" target="_blank">--%>
            <asp:image ID="LogoF" runat="server" ImageUrl="~/img/etiqueta_fm.jpg" TabIndex="10" />&nbsp;<%--</a>--%></div>
</body>
</html>
