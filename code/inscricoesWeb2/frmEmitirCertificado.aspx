<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmEmitirCertificado.aspx.cs" Inherits="frmEmitirCertificado" Title="Inscri&ccedil;&otilde;es Web" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Inscri&ccedil;&otilde;es Web</title>
    <meta name="encoding" http-equiv="content-type" content="text/html; charset=iso-8859-1" />
    <meta name="KeyWords" content="sistema de credenciamento, sistema de credenciamento web, credenciamento, etiqueta, sistema de credenciamento com etiqueta, sistema de credenciamento com código de barras, tecnologia em eventos" />

     
     <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE9" />
    
    <link id="Link1" runat="server" rel="shortcut icon" href="img/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="img/favicon.ico" type="image/ico" />
    <link id="Estilo" href="css/StyleSheet2.css" rel="stylesheet" type="text/css" />

    <link rel="stylesheet" type="text/css" href="css/ddsmoothmenu.css" />
    <link rel="stylesheet" type="text/css" href="css/ddsmoothmenu-v.css" />

    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <link rel="stylesheet" type="text/css" href="css/menu/menu_style.css"/>
   


    <%--<script type="text/javascript" src="../scripts/JSCFuncoes.js"></script>--%>
    <script type="text/javascript" src="scripts/JSCFuncoes.js"></script>
    <script type="text/javascript" src="scripts/jquery-1.11.1.min.js"></script>

    <script type="text/javascript" src="scripts/jquery.cj-object-scaler.min.js"></script>

   
    

    <script type="text/javascript" src="scripts/jquery.lockfixed.js"></script>
    
    <script type="text/javascript" src="scripts/jquery.dd.min.js"></script>
    <link id="Link3" href="css/dd.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="frmCad_auto">
        
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
                EnableScriptLocalization="true" CombineScripts="false">
            </asp:ToolkitScriptManager>
        
            <asp:UpdateProgress runat="server" id="UpdateProgress1" DisplayAfter="0"  >
                <ProgressTemplate>
                
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:ModalPopupExtender ID="modalExtender" runat="server" TargetControlID="UpdateProgress1"
                PopupControlID="pnlAguarde" DropShadow="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlAguarde" runat="server" CssClass="modalExtender">
                <div class="loading" align="center">
                        <br />Aguarde!... Processando sua solicitação. <br />
                        <br />
                        <img src="imagensgeral/loader.gif" alt="" />
                 </div>
            </asp:Panel>
            <h3>
            <asp:Label ID="lblTituloPagina" runat="server" Text="Certificados" CssClass="titulo"></asp:Label><br />
            <br />
        </h3>
        

            <asp:UpdatePanel ID="up1" runat="server">
                <ContentTemplate>
                    <div id="cpf"  >
                        <div ID="tbCPFPesquisa" runat="server">
                            <div ID="linCPF" runat="server">
                            
                                    <asp:Label ID="Label2" runat="server" CssClass="label" Text="Informe o CPF"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtCPF" runat="server" Width="130px" CssClass="txt" MaxLength="14" 
                                                onkeypress="return CPFMascarar(this, event)"></asp:TextBox>
                                    <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="txtCPF" ErrorMessage="Campo obrigatório." OnServerValidate="CustomValidator2_ServerValidate"></asp:CustomValidator>
                                
                                    <asp:RequiredFieldValidator ID="RFVCPF" runat="server" ControlToValidate="txtCPF" Display="Dynamic" ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                                
                            </div>                            
                            <div ID="linbtn" runat="server">
                            
                                    <br />
                                    <asp:Button ID="Button1" runat="server" CssClass="botoes" 
                                        onclick="Button1_Click" Text="Continuar" OnClientClick="AtivaDivProgress()" />
                                    <br />
                                    
                                
                            
                            </div>
                        </div>
                        <br />
                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                        <br /><br />
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div id="dadosparticipante" runat="server">
                                    <div id="pedidos_esq">                 
                                        <div style="float: left;">
                                            <asp:Label ID="lblID" runat="server" Width="100px" CssClass="label" Text="Nr Cadastro:"></asp:Label>                    
                                            &nbsp;<asp:Label ID="lblIdentificador" runat="server" Font-Bold="True" ForeColor="Navy"> </asp:Label></div>
                                        <div style="float: left;">
                                            <asp:Label ID="lblPart" runat="server" Width="100px" CssClass="label" Text="Participante:"></asp:Label>                    
                                            &nbsp;<asp:Label ID="lblNoParticipante" runat="server" Font-Bold="True" ForeColor="Navy"> </asp:Label></div>
                                        <div style="float: left;">
                                            <asp:Label ID="lblCateg" runat="server" Width="100px" CssClass="label" Text="Categoria:"></asp:Label>                    
                                            &nbsp;<asp:Label ID="lblCategoria" runat="server" Font-Bold="True" ForeColor="Navy"> </asp:Label></div>
        
                                        <br /><br />&nbsp;
                                    </div> 
                                </div>           
                                <asp:DataList ID="DataList1" runat="server" BorderWidth="1">
                                    <HeaderTemplate>
                                        <br /> 
                                        <div id="linhaInfo" style="width: 100%; background-color: #FFFFCC; vertical-align: middle;">
                                            <div id="InfEsq" style="width: 100%; float: left; text-align: left; vertical-align: middle; background-color: #FFFFCC;" >
                                                &nbsp;<asp:Label runat="server" ID="Label8" Text="Pressione Ctrl-F para buscar nome" ForeColor="Black"  Font-Size="10pt"></asp:Label>&nbsp;
                                            </div>
                                            <%--<div id="InfDir" style="width: 50%; float: right; text-align: right; background-color: #FFFFCC;">
                                                &nbsp;
                                                <asp:Label runat="server" ID="lblTotReg" Text="0" ForeColor="Black" Font-Bold="True" Font-Size="10pt"></asp:Label>&nbsp;
                                                <asp:Label runat="server" ID="Label9" Text="Registros" ForeColor="Black"  Font-Size="10pt"></asp:Label>&nbsp;
                                            </div>--%>
                                        </div>
                                        <br />&nbsp; <br /> 
                                        <div id="linhaRegH" style="background-color: black; vertical-align: middle">
                                            <br />  
                                            <%--&nbsp;-&nbsp;--%>
                                            <div id="cdPartH" style="width: 100px; float: left; text-align: center; vertical-align: middle;" >
                                                &nbsp;<asp:Label runat="server" ID="lblCdParticipante" Text="ID" ForeColor="White" Font-Bold="True" Font-Size="10pt"></asp:Label>&nbsp;
                                            </div>
                                            <div id="instH" style="width: 200px; float: left; text-align: center;">
                                                &nbsp;<asp:Label runat="server" ID="Label1" Text="Regional" ForeColor="White" Font-Bold="True" Font-Size="10pt"></asp:Label>&nbsp;
                                            </div>
                                            <div id="cdCertificadoH" style="width: 100px;float: left; text-align: center;">
                                                &nbsp;<asp:Label runat="server" ID="Label3" Text="Registro" ForeColor="White" Font-Bold="True" Font-Size="10pt"></asp:Label>&nbsp;
                                            </div>
                                            <div id="CategoriaH" style="width: 150px;float: left; text-align: center;">
                                                &nbsp;<asp:Label runat="server" ID="Label4" Text="Tipo" ForeColor="White" Font-Bold="True" Font-Size="10pt"></asp:Label>&nbsp;
                                            </div>
                                            <div id="noPartH" style="width: 400px;float: left;">
                                                &nbsp;<asp:Label runat="server" ID="Label5" Text="Nome" ForeColor="White" Font-Bold="True" Font-Size="10pt"></asp:Label>&nbsp;
                                            </div>
                                            <div id="lnkH" style="width: 160px; clear: right; float: left; text-align: center;">
                                                &nbsp;<asp:Label runat="server" ID="Label7" Text="Baixar Certificado" ForeColor="White" Font-Bold="True" Font-Size="10pt"></asp:Label>&nbsp;
                                            </div>
                                            <div id="downloadH" style="width: 150px;float: left; text-align: center;">
                                                &nbsp;<asp:Label runat="server" ID="Label6" Text="Acessado em" ForeColor="White" Font-Bold="True" Font-Size="10pt"></asp:Label>&nbsp;
                                            </div>
                                            <br />&nbsp;
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div id="linhaReg">
                                            <br />  
                                            <%--&nbsp;-&nbsp;--%>
                                            <div id="cdPart" style="width: 100px; float: left; text-align: center;">
                                                &nbsp;<asp:Label runat="server" ID="lblCdParticipante" Text='<%# Eval("cdParticipante") %>'></asp:Label>&nbsp;
                                            </div>
                                            <div id="inst" style="width: 200px; float: left; text-align: center;">
                                                &nbsp;<asp:Label runat="server" ID="Label1" Text='<%# Eval("noInstituicao") %>'  Font-Bold="True"></asp:Label>&nbsp;
                                            </div>
                                            <div id="cdCertificado" style="width: 100px;float: left; text-align: center;">
                                                &nbsp;<asp:Label runat="server" ID="Label3" Text='<%# Eval("NrCertificado") %>'></asp:Label>&nbsp;
                                            </div>
                                            <div id="Categoria" style="width: 150px;float: left; text-align: center;">
                                                &nbsp;<asp:Label runat="server" ID="Label4" Text='<%# Eval("noCategoria") %>'></asp:Label>&nbsp;
                                            </div>
                                            <div id="noPart" style="width: 400px;float: left;">
                                                &nbsp;<asp:Label runat="server" ID="Label5" Text='<%# Eval("noParticipante") %>'  Font-Bold="True"></asp:Label>&nbsp;
                                            </div>
                                            <div id="lnk" style="width: 160px; clear: right; float: left; text-align: center; ">
                                                <asp:HyperLink ID="HyperLink11" runat="server" Target="_blank" NavigateUrl='<%# Eval("dsLinkCertificado") %>' ImageUrl="~/img/download2.png" ></asp:HyperLink>
                                            </div>
                                            <div id="DTdownload" style="width: 150px;float: left; text-align: center;">
                                                &nbsp;<asp:Label runat="server" ID="Label6" Text='<%# Eval("dtEntrega") %>'></asp:Label>&nbsp;
                                            </div>
                                            <div id="divisor" style="clear: both">
                                                <hr>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>
                                <br />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <script type="text/javascript">
                Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(showPopup);
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(hidePopup);

                function showPopup(sender, args) {
                    var ModalControl = '<%= modalExtender.ClientID %>';
                    $find(ModalControl).show();
                }

                function hidePopup(sender, args) {
                    var ModalControl = '<%= modalExtender.ClientID %>';
                    $find(ModalControl).hide();
                }
        </script>

            <%--<script type="text/javascript" language="javascript">

                function AtivaDivProgress() {
                    document.getElementById("divProgress").style.display = "block";
                }

            </script>--%>

        </div>
    </form>
</body>
</html>
