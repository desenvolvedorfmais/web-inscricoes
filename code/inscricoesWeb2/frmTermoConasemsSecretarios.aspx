<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmTermoConasemsSecretarios.aspx.cs" Inherits="frmTermoConasemsSecretarios" 
    Title="Inscri&ccedil;&otilde;es Web" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div id="frmCad_auto">
    <asp:ToolkitScriptManager ID="TSManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" CombineScripts="false">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
    <h3>
            <asp:Label ID="lblTitulo" runat="server" Text="Declaração" CssClass="titulo"></asp:Label></h3>
        <br />
    <br />
        <asp:Label ID="lblTermoAceite" runat="server"></asp:Label>
        <br />
        <br />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:CheckBox ID="CheckBox1" runat="server" 
            oncheckedchanged="CheckBox1_CheckedChanged" 
            Text=" Li e aceito os termos da Declaração" AutoPostBack="True" />
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

