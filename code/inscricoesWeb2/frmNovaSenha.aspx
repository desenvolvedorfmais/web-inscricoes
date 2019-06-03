<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmNovaSenha.aspx.cs" Inherits="frmNovaSenha" title="Inscri&ccedil;&otilde;es Web" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div id="frmCad_auto">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"  EnableScriptGlobalization="true"
            EnableScriptLocalization="true" CombineScripts="false">

    </asp:ToolkitScriptManager>
    <h3>
        <asp:Label ID="lblTituloPagina" runat="server" Text="Mudança de senha" CssClass="titulo"></asp:Label><br />
        <br />
    </h3>
    
    <div id="pedidos_esq">
                
        <div style="float: left;">
            <asp:Label ID="lblID" runat="server" Width="100px" CssClass="label" Text="Nr Cadastro:"></asp:Label>                    
            &nbsp;<asp:Label ID="lblIdentificador" runat="server" Font-Bold="False"></asp:Label></div>
        <div style="float: left;">
            <asp:Label ID="lblPart" runat="server" Width="100px" CssClass="label" Text="Participante:"></asp:Label>                    
            &nbsp;<asp:Label ID="lblNoParticipante" runat="server" Font-Bold="False"></asp:Label></div>
        <div style="float: left;">
            <asp:Label ID="lblCateg" runat="server" Width="100px" CssClass="label" Text="Categoria:"></asp:Label>                    
            &nbsp;<asp:Label ID="lblCategoria" runat="server" Font-Bold="False"></asp:Label></div>
    </div> 
    
    <br />&nbsp;
    <div id="dados_nova_senha" >
        
        
        <br />
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate> 
                <asp:Label ID="txtMsg" runat="server" CssClass="lblMsg" Visible="false"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="linhaSenhaAtual" runat="server" class="linha_campo">
            <asp:Label ID="lblSenhaAtual" runat="server" Text="  Senha Atual:" CssClass="lblTitulocampo"></asp:Label><br />
            <asp:TextBox ID="txtDsSenhaAtual" runat="server" CssClass="senha campoform_pequeno" MaxLength="14" ReadOnly="false" TabIndex="0" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvSenhaAtual" runat="server" ControlToValidate="txtDsSenhaAtual" Display="Dynamic" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator><br />
        </div>
        <div id="linhaNovaSenha" runat="server" class="linha_campo">
            <asp:Label ID="lblNovaSenha" runat="server" Text=" Nova Senha:" CssClass="lblTitulocampo"></asp:Label><br />
            <asp:TextBox ID="txtNovaSenha" runat="server" CssClass="senha campoform_pequeno" MaxLength="14" ReadOnly="false" TabIndex="0" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvNovaSena" runat="server" ControlToValidate="txtNovaSenha" Display="Dynamic" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>
            <asp:PasswordStrength ID="PasswordStrength1" runat="server" TargetControlID="txtNovaSenha"
                DisplayPosition="RightSide" 
                StrengthIndicatorType="BarIndicator"
                PreferredPasswordLength="14"
            
                StrengthStyles="BarIndicator_TextBox2_weak;BarIndicator_TextBox2_average;BarIndicator_TextBox2_good"
            
                BarBorderCssClass="BarBorder_TextBox2"
                MinimumNumericCharacters="1"
                MinimumSymbolCharacters="1"
                TextStrengthDescriptions="Very Poor;Weak;Average;Strong;Excellent"
                RequiresUpperAndLowerCaseCharacters="true" />
        
        </div>
        <div id="linhaRepeteSenha" runat="server" class="linha_campo">
            <asp:Label ID="lblRepeteSenha" runat="server" Text="Repete Senha:" CssClass="lblTitulocampo"></asp:Label><br />
            <asp:TextBox ID="txtRepeteNovaSenha" runat="server" CssClass="senha campoform_pequeno" MaxLength="14" ReadOnly="false" TabIndex="0" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvSenhaNovaSenha2" runat="server" ControlToValidate="txtRepeteNovaSenha" Display="Dynamic" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompValEmail" runat="server" ControlToCompare="txtNovaSenha" ControlToValidate="txtRepeteNovaSenha"
                ErrorMessage="Senhas diferentes"></asp:CompareValidator>
        </div>
        <br />
        &nbsp;<asp:Button ID="btnAlterarSenha" runat="server" Text="Alterar senha" OnClick="Button1_Click" CssClass="botoes" />
    
    </div>    
</div>
</asp:Content>

