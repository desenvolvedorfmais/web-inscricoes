<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmInformacoes.aspx.cs" Inherits="frmInformacoes" title="Inscri&ccedil;&otilde;es Web" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="scripts/MSGAguarde.js" type="text/javascript"></script>
    
    <div id="frmCad_auto">
    <asp:ToolkitScriptManager ID="TSManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" CombineScripts="false">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
    <br />&nbsp;&nbsp;&nbsp; 
        <asp:Button ID="btnCadastrar" runat="server" 
            CausesValidation="False" CssClass="botoes"
        OnClick="btnCadastrar_Click" TabIndex="5" Text="Quero me Inscrever" 
            Visible="False" Enabled="False" />
        <br />
    <br />
        <asp:Label ID="lblInformacoesCompletas" runat="server"></asp:Label>
        

        <asp:CheckBox ID="CheckBox1" runat="server" 
            oncheckedchanged="CheckBox1_CheckedChanged" 
            Text=" Li e concordo com os termos acima." AutoPostBack="True" />
        <br />
        <br />
    &nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnCadastrar2" runat="server" CausesValidation="False" CssClass="botoes"
        OnClick="btnCadastrar_Click" TabIndex="5" Text="Quero me Inscrever" 
            Enabled="False" /><br />

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>

