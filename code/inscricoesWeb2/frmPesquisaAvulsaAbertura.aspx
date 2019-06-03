<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmPesquisaAvulsaAbertura.aspx.cs" Inherits="frmPesquisaAvulsaAbertura" 
    Title="Inscri&ccedil;&otilde;es Web"  EnableEventValidation="false" ValidateRequest="false"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="scripts/MSGAguarde.js" type="text/javascript"></script>
    <div id="frmCad_auto">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
            EnableScriptLocalization="true" CombineScripts="false">
        </asp:ToolkitScriptManager>
        
        <br />
        <br />
        <div id="MensagemChamada">
        
            <center>
                <br />
                <asp:Label ID="lblTitulo" runat="server" Text="Prêmio IEL de Estágio"></asp:Label>
                <br />
                <br />
                <asp:Label ID="lblChamada1" runat="server" 
                    Text="Participe da nossa pesquisa de avaliação"></asp:Label>
                <br />
                <asp:Label ID="lblChamada2" runat="server" 
                    Text="Sua opinião é muito importante para nós"></asp:Label>
                <br />
                <br />
                <br />
                <asp:Button ID="btnQueroParticiparPesquisa" runat="server" CssClass="botoes" Text="Quero participar" Visible="true" 
                    onclick="btnContinuar_Click" Height="68px" Width="208px" />
            </center>
            <br />
        
        
        </div>
    </div>



</asp:Content>

