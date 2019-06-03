<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmDadosExpoepi.aspx.cs" Inherits="frmDadosExpoepi" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptLocalization="true"
        CombineScripts="false">
    </asp:ToolkitScriptManager>
    <div id="frmCad_auto">
         <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <h3>
                    <asp:Label ID="lblTituloGerarSenha" runat="server" 
                        Text="Pesquisar Dados de Voo" CssClass="titulo"></asp:Label></h3>
                <br />
                <br />
                <asp:Label ID="lblDesctela" runat="server" Text="Atenção!<br/><br/>Os participantes convidados que tiveram 
                suas passagens aéreas emitidas pela Secretaria de 
                Vigilância em Saúde poderão consultar seus dados de voo. Basta digitar seu CPF."></asp:Label>
                <br />
                <br />
                <br />
                <br />
                <asp:Label ID="txtMsg" runat="server" ForeColor="Red"></asp:Label>
                <br />
                <asp:Label ID="LBLCONTA" runat="server" Text="CPF:"></asp:Label><asp:TextBox ID="TXTDsCPF"
                        runat="server" CssClass="text" MaxLength="14" onkeypress="return Mascarar(this, event, '999.999.999-99')"
                        ReadOnly="false" TabIndex="0"></asp:TextBox><asp:RequiredFieldValidator
                                ID="RFVCPF" runat="server" ControlToValidate="TXTDsCPF" Display="Dynamic" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator><br />
                <br />
                &nbsp;<asp:Button ID="btnPesquisarDadosVoo" runat="server" 
                    Text="Verificar Dados de Voo" OnClick="btnPesquisarDadosVoo_Click1"
                    CssClass="botoes" />&nbsp;
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

