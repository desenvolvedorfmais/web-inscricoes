<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmVerificarBrasileiro.aspx.cs"
Inherits="frmVerificarBrasileiro" Title="Inscri&ccedil;&otilde;es Web"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
Namespace="System.Web.UI" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="frmCad_auto">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
                                  EnableScriptLocalization="true" CombineScripts="false">
        </asp:ToolkitScriptManager>
        <h3>
            <asp:Label ID="lblTituloPagina" runat="server" Text="Selecione uma Opção" CssClass="titulo"></asp:Label><br/>
            <br/>
        </h3>
        <div id="nacionalidade">
            <div id="brasileiro">
                <asp:Button ID="Button1" runat="server" CssClass="botoes" Text="Sou Brasileiro" TabIndex="1" Width="250px" OnClick="Button1_Click1"/>
            </div>
            <div id="nao-brasileiro">
                <asp:Button ID="Button2" runat="server" CssClass="botoes" Text="Não Sou Brasileiro" TabIndex="2" Width="250px" OnClick="Button2_Click"/>
            </div>
        </div>
    </div>

</asp:Content>