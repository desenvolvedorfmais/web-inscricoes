<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmFiltroPrecadastro.aspx.cs" 
Inherits="frmFiltroPrecadastro" Title="Inscri&ccedil;&otilde;es Web"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="frmCad_auto">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
            EnableScriptLocalization="true" CombineScripts="false">
        </asp:ToolkitScriptManager>
        
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
                <div id="cpfCaptcha" class="cpfReceitaCaptcha" >
                    <asp:Label ID="Label2" runat="server" CssClass="label" Text="Informe o CPF"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtCPF" runat="server" Width="175px" CssClass="txt" MaxLength="14" 
                        onkeypress="return CPFMascarar(this, event)"></asp:TextBox>
                    <br />
                    <br />
                    <asp:Button ID="Button1" runat="server" CssClass="botoes" 
                        onclick="Button1_Click" Text="Continuar" />
                    <br />
                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                    <br />
                    <hr />
                    <br />
                    <asp:Label ID="Label1" runat="server" CssClass="label" Text="Informe o Nome"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtNome" runat="server" Width="464px" CssClass="txt" 
                        MaxLength="100"></asp:TextBox>
                    <br />
                    <br />
                    <asp:Button ID="btnPesquisarNome" runat="server" CssClass="botoes" 
                         Text="Continuar" onclick="btnPesquisarNome_Click" />
                    <br />
                    <asp:Label ID="lblMsg2" runat="server" ForeColor="Red"></asp:Label>
                    <br />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

