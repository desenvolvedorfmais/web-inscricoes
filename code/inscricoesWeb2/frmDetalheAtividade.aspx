<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmDetalheAtividade.aspx.cs" Inherits="frmDetalheAtividade"
    Title="Inscri&ccedil;&otilde;es Web" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="frmCad_auto">
        <asp:ToolkitScriptManager ID="TSManager1" runat="server" EnableScriptLocalization="true" CombineScripts="true">   
        </asp:ToolkitScriptManager>
        <h3><asp:Label ID="lblTituloPagina" runat="server" Text="Detalhe Atividade" CssClass="titulo"></asp:Label></h3>
        <br />
                    
        <asp:Button ID="btnVoltar" runat="server" 
                        CausesValidation="False" CssClass="botoes" Font-Bold="False" 
                        TabIndex="13" Text="Voltar" 
                        Width="105px" OnClick="btnVoltar_Click" />
        <br />
        <br />
        <div id="detalheAtividade">
            <asp:Label ID="Label14" runat="server" Text="Tipo Atividade:"></asp:Label> <asp:Label ID="lbltipo" runat="server" ></asp:Label>
            <br /><br />
            <asp:Label ID="lblAtividade" runat="server"></asp:Label>
            
        </div>
    </div>
</asp:Content>

