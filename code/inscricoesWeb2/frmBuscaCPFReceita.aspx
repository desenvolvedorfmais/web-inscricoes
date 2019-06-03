<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmBuscaCPFReceita.aspx.cs" 
Inherits="frmBuscaCPFReceita" Title="Inscri&ccedil;&otilde;es Web"%>

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
                <div id="PreviaCaptcha" class="cls_msgPreviaCaptcha">
                    <asp:Label ID="msgPreviaCaptcha" runat="server" CssClass="label" ></asp:Label>
                </div>
                <div id="cpfCaptcha" class="cpfReceitaCaptcha" >
                    <div>
                        <asp:Label ID="Label2" runat="server" CssClass="label" Text="Informe o CPF"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtCPF" runat="server" CssClass="campoform_pequeno" MaxLength="14" 
                                    onkeypress="return CPFMascarar(this, event)" TabIndex="1"></asp:TextBox>
                    </div>
                    <br />
                    <div>
                        <asp:Label ID="Label4" runat="server" CssClass="label" 
                            Text="Data de Nascimento" Visible="true"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtDtNascimento" onkeypress="return Mascarar(this, event, '99/99/9999')" runat="server"  Visible="true" CssClass="campoform_pequeno mauisculo"  TabIndex="2"></asp:TextBox>
                    </div>
                    <br />
                     <div id="imgCtch" class="imgCaptcha" style="display:none">
                        <cc1:CaptchaControl ID="Captcha1" runat="server"
                             CaptchaBackgroundNoise="Low" CaptchaLength="5"
                             CaptchaHeight="60" CaptchaWidth="210"
                             CaptchaLineNoise="None" CaptchaMinTimeout="5"
                             CaptchaMaxTimeout="240" FontColor = "#029DCD" 
                            CaptchaChars="ACDEFGHJKLNPQRTUVXYZabcdefghjkmnopqrstuvxyz2346789"  />
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/img/refresh.png"  TabIndex="4"/>
                            <asp:Label ID="Label1" runat="server" CssClass="label" 
                        Text="Gerar novo código" ></asp:Label>
                    </div>
                    <div style="display:none">
                        <asp:Label ID="Label3" runat="server" CssClass="label" 
                            Text="Digite os caracteres da imagem" Visible="true"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtCaptcha" runat="server" MaxLength="5" Visible="true" CssClass="campoform_pequeno mauisculo"  TabIndex="2"></asp:TextBox>
                    </div>
                    <br />
                            <asp:Button ID="Button1" runat="server" CssClass="botoes" 
                                onclick="Button1_Click" Text="Continuar"  TabIndex="3" />
                            <br />
                            <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                            <br />
                   
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

