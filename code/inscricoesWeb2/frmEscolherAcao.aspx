<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmEscolherAcao.aspx.cs" Inherits="frmEscolherAcao" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="scripts/MSGAguarde.js" type="text/javascript"></script>
    
    <div id="frmCad_auto">
        <div id="fromCadastro" class="centralizarForm">
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
                EnableScriptLocalization="true" CombineScripts="false">
            </asp:ToolkitScriptManager>
            <br />
            <br />
            <br />
            <asp:Label ID="lblInstucoes" runat="server" Text="Clique na opção desejada:" CssClass="titulo" ></asp:Label>

            <br />
            <br />
            <br />
                    <asp:Button ID="btnAtividades" runat="server" Text="Gerar Pagamento" CssClass="botoes" PostBackUrl="~/frmSelAtividades.aspx" Width="220px" />
                    <asp:Button ID="btnTrabalhos" runat="server" Text="Cadastrar Trabalhos" CssClass="botoes"  PostBackUrl="~/frmTrabalhosLista.aspx" Width="220px"/>
            <br />
            <br />
        </div>
    </div>
</asp:Content>

