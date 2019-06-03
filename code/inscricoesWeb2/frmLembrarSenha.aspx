<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmLembrarSenha.aspx.cs" Inherits="frmLembrarSenha" Title="Inscri&ccedil;&otilde;es Web" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptLocalization="true"
        CombineScripts="false">
    </asp:ToolkitScriptManager>
    <div id="frmCad_auto">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <h3>
                    <asp:Label ID="lblTituloGerarSenha" runat="server" Text="Gerar nova senha" CssClass="titulo"></asp:Label></h3>
                <br />
                <br />
                <asp:Label ID="lblDesctela" runat="server" Text="Aten��o!<br/><br/>Por motivo de seguran�a todas
                as senhas s�o codificadas por rotinas que n�o permitem a decodifica��o.
                Tendo isto em vista esta rotina ir� gerar uma nova senha e enviar� para o e-mail
                do participante j� cadastrado."></asp:Label>
                <br />
                <br />
                <br />
                <br />
                <asp:Label ID="txtMsg" runat="server" ForeColor="Red"></asp:Label>
                <br />
                <asp:Label ID="LBLCONTA" runat="server" Text="CPF:"></asp:Label><asp:Label ID="lblEmail"
                    runat="server" Text="Email:" Visible="False"></asp:Label><asp:TextBox ID="TXTDsCPF"
                        runat="server" CssClass="text" MaxLength="14" onkeypress="return Mascarar(this, event, '999.999.999-99')"
                        ReadOnly="false" TabIndex="0"></asp:TextBox><asp:TextBox ID="txtEmail_lembrar_senha" runat="server"
                            CssClass="email" MaxLength="100" ReadOnly="false" Visible="False" Width="292px"></asp:TextBox><asp:RequiredFieldValidator
                                ID="RFVCPF" runat="server" ControlToValidate="TXTDsCPF" Display="Dynamic" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                    ID="revTxtEmail" runat="server" ControlToValidate="txtEmail_lembrar_senha" Display="Dynamic"
                                    ErrorMessage="E-mail inv�lido" ValidationExpression="[�������������������������纪@&\-\.\,\w\s\d]{1,100}"
                                    Visible="False"></asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="rfvTxtEmail"
                                        runat="server" ControlToValidate="txtEmail_lembrar_senha" Display="Dynamic" ErrorMessage="Campo requerido"
                                        Visible="False"></asp:RequiredFieldValidator><br />
                <br />
                &nbsp;<asp:Button ID="btnGerarNovaSenha" runat="server" Text="Gerar nova senha" OnClick="Button1_Click"
                    CssClass="botoes" />&nbsp;<asp:Button ID="btnQueroCadastrar" runat="server" CssClass="botoes"
                        OnClick="btnCadastrar_Click" Text="Quero me Inscrever" CausesValidation="False"
                        TabIndex="7" Visible="False" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
