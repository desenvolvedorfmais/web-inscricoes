<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmMeetingPerfil.aspx.cs" Inherits="frmMeetingPerfil" title="Inscri&ccedil;&otilde;es Web"
    ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="scripts/MSGAguarde.js" type="text/javascript"></script>

    <div id="frmCad_auto">

        <asp:UpdateProgress runat="server" id="UpdateProgress1" DisplayAfter="0" >
            <ProgressTemplate>
                
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:ModalPopupExtender ID="modalExtender" runat="server" TargetControlID="UpdateProgress1"
            PopupControlID="pnlAguarde" DropShadow="true" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>
        <asp:Panel ID="pnlAguarde" runat="server" CssClass="modalExtender">
            <div class="loading" align="center">
                    <br />&nbsp;Aguarde!... Processando sua solicitação&nbsp;&nbsp;<br />
                    <br />
                    <img src="imagensgeral/loader.gif" alt="" />
                    <br />Wait! ... Processing your request<br />
                    <br />
                </div>
        </asp:Panel>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"  EnableScriptGlobalization="true"
            EnableScriptLocalization="true" CombineScripts="false">

    </asp:ToolkitScriptManager>
    <h3>
        <asp:Label ID="lblTituloPagina" runat="server" Text="Perfil" CssClass="titulo"></asp:Label>
        <br />
    </h3>
    
    <div id="pedidos_esq">
                
        <div <%--style="float: left;"--%>>
            <asp:Label ID="lblID" runat="server" Width="130px" CssClass="label" Text="Nr Cadastro:"></asp:Label>                    
            &nbsp;<asp:Label ID="lblIdentificador" runat="server" Font-Bold="True" ForeColor="Navy"> </asp:Label></div>

        <div <%--style="float: left;"--%>>
            <asp:Label ID="lblEmpr" runat="server" Width="130px" CssClass="label" Text="Intituição/Empresa:"></asp:Label>                    
            &nbsp;<asp:Label ID="lblEmpresa" runat="server" Font-Bold="True" ForeColor="Navy"> </asp:Label></div>

        <div <%--style="float: left;"--%>>
            <asp:Label ID="lbl_pais" runat="server" Width="130px" CssClass="label" Text="País:"></asp:Label>                    
            &nbsp;<asp:Label ID="lblNoPais" runat="server" Font-Bold="True" ForeColor="Navy"> </asp:Label></div>

        <div <%--style="float: left;"--%>>
            <asp:Label ID="lblArea" runat="server" Width="130px" CssClass="label" Text="Área de Atuação:"></asp:Label>                    
            &nbsp;<asp:Label ID="lblAreaAtuacao" runat="server" Font-Bold="True" ForeColor="Navy"> </asp:Label></div>

        <div <%--style="float: left;"--%>>
            <asp:Label ID="lblSite" runat="server" Width="130px" CssClass="label" Text="Website:"></asp:Label>                    
            &nbsp;<asp:Label ID="lblWebSite" runat="server" Font-Bold="True" ForeColor="Navy"> </asp:Label></div>

        <div <%--style="float: left;"--%>>
            <asp:Label ID="lblPart" runat="server" Width="130px" CssClass="label" Text="Participante:"></asp:Label>                    
            &nbsp;<asp:Label ID="lblNoParticipante" runat="server" Font-Bold="True" ForeColor="Navy"> </asp:Label></div>

        <div <%--style="float: left;"--%>>
            <asp:Label ID="lblCateg" runat="server" Width="130px" CssClass="label" Text="Categoria:"></asp:Label>                    
            &nbsp;<asp:Label ID="lblCategoria" runat="server" Font-Bold="True" ForeColor="Navy"> </asp:Label></div>

        <div <%--style="float: left;"--%>>
            <asp:Label ID="lblCargo" runat="server" Width="130px" CssClass="label" Text="Cargo:"></asp:Label>                    
            &nbsp;<asp:Label ID="lblNoCargo" runat="server" Font-Bold="True" ForeColor="Navy"> </asp:Label></div>
    </div> 
    
    <br />&nbsp;
    <div id="dados_perfilB2B" >

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate> 
                <asp:Label ID="txtMsg" runat="server" CssClass="lblMsg" Visible="false"></asp:Label>
                 <br />
                <asp:Label ID="lblInstrucoes" runat="server" Text="Informe o perfil de negócios de sua empresa  "></asp:Label>
                <asp:Label ID="lblInstrucoes0" runat="server" ForeColor="#FF3300" Text="(Limite de 500 caracteres): "></asp:Label>
                <br /><br />
                <asp:TextBox ID="txtPerfilB2B" runat="server" TextMode="MultiLine" MaxLength="500" ></asp:TextBox>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="charcount">500</div>
        <%--<div id="lblContCarct2" runat="server" class="contaCaracteres">
            ( 0 de 500 caracteres )</div>
        <br />--%>
        <br />
        &nbsp;<asp:Button ID="btnGravar" runat="server" Text="Gravar" OnClick="btnGravar_Click" CssClass="botoes" />
    
                <asp:Button ID="btnNovo" runat="server" Text="Novo Convite" CssClass="botoes" PostBackUrl="~/frmMeetingParticipantes.aspx" Visible="False" />
    
    </div>    
</div>

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
</asp:Content>

