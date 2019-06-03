<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmCredencial.aspx.cs" Inherits="frmCredencial" 
    Title="Inscri&ccedil;&otilde;es Web" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:ToolkitScriptManager ID="TSManager1" runat="server" EnableScriptLocalization="true"
        CombineScripts="false">
    </asp:ToolkitScriptManager>

    <div id="frmCad_auto">
        <%--<asp:UpdateProgress runat="server" id="UpdateProgress1" DisplayAfter="0" >
            <ProgressTemplate>
                
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:ModalPopupExtender ID="modalExtender" runat="server" TargetControlID="UpdateProgress1"
            PopupControlID="pnlAguarde" DropShadow="true" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>
        <asp:Panel ID="pnlAguarde" runat="server" CssClass="modalExtender">
            <div class="loading" align="center">
                    <br />Aguarde!... Processando sua solicitação<br />
                    <br />
                    <img src="imagensgeral/loader.gif" alt="" />
             </div>
        </asp:Panel>--%>
        <h3>
            <asp:Label ID="lblTituloPagina" runat="server" Text="Gerar Credencial" CssClass="titulo"></asp:Label><br />
            <br />
        </h3>
        <div id="carrinho_geral" class="carrinhogeral" style="background-color: White;">
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
            </div> 
            <br />
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <br />
                <asp:Label ID="lblMsg" runat="server" CssClass="lblMsg" Visible="False"></asp:Label>
                <br />
                <asp:Label ID="lblInformacoesprevias" runat="server" Text="Prezado Sr(a), clique no botão abaixo para gerar sua credencial. <br /><br />O sistema irá gerar um arquivo no formato PDF. Imprima a credencial e não se esqueça de levá-la consigo durante os dias do Congresso, pois ela irá garantir seu acesso às dependências do evento."></asp:Label><br />
                <br />
                <br />
                <asp:Button ID="btnGerarCredencial" runat="server" CssClass="botoes" TabIndex="32" Text="Gerar Credencial" OnClick="btnGerarCredencial_Click"   />
                            
            </ContentTemplate>
        </asp:UpdatePanel>  
    </div>  
</asp:Content>

