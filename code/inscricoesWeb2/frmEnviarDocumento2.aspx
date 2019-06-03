﻿<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmEnviarDocumento2.aspx.cs" Inherits="frmEnviarDocumento2" 
    Title="Inscri&ccedil;&otilde;es Web" 
    EnableEventValidation="true"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:ToolkitScriptManager ID="TSManager1" runat="server" EnableScriptLocalization="true"
        CombineScripts="false">
    </asp:ToolkitScriptManager>

    <div id="frmCad_auto">
        <asp:UpdateProgress runat="server" id="UpdateProgress1" DisplayAfter="0" >
            <ProgressTemplate>
                
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:ModalPopupExtender ID="modalExtender" runat="server" TargetControlID="UpdateProgress1"
            PopupControlID="pnlAguarde" DropShadow="true" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>
        <asp:Panel ID="pnlAguarde" runat="server" CssClass="modalExtender">
            <div class="loading" align="center">
                    <br />Aguarde!... Processando sua solicitação<br />
                    <br />
                    <img src="imagensgeral/loader.gif" alt="" />
             </div>
        </asp:Panel>
        <h3>
            <asp:Label ID="lblTituloPagina" runat="server" Text="Envio de Documento" CssClass="titulo"></asp:Label><br />
            <br />
        </h3>
        <div id="carrinho_geral" class="carrinhogeral" style="background-color: White;">
            <div id="pedidos_esq">
                
                <div style="float: left;">
                    <asp:Label ID="lblID" runat="server" Width="100px" CssClass="label" Text="Nr Cadastro:"></asp:Label>                    
                    &nbsp;<asp:Label ID="lblIdentificador" runat="server" Font-Bold="True" ForeColor="Navy"> </asp:Label></div>
                <div style="float: left;">
                    <asp:Label ID="lblPart" runat="server" Width="100px" CssClass="label" Text="Participante:"></asp:Label>                    
                    &nbsp;<asp:Label ID="lblNoParticipante" runat="server" Font-Bold="True" ForeColor="Navy"> </asp:Label></div>
                <div style="float: left;">
                    <asp:Label ID="lblCateg" runat="server" Width="100px" CssClass="label" Text="Categoria:"></asp:Label>                    
                    &nbsp;<asp:Label ID="lblCategoria" runat="server" Font-Bold="True" ForeColor="Navy"> </asp:Label></div>
            </div> 
            <br />
        </div>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate> 
                <table  id="Table2"  border="0" cellpadding="0" cellspacing="10" width="100%" runat="server"> 
                    <tr>
                        <td align="left" >
                            <br />
                             <asp:Label ID="lblTextoEnvioDoc" runat="server" 
                                 Text="Pezado Sr(a) verificamos que o documento necessário para a confirmação da sua inscrição ainda não foi enviado. Solicitamos que faça o envio do documento, para isto selecione o arquivo e clique no botão enviar.">
                             </asp:Label>
                             <br />
                        </td>
                    </tr>
                    <tr id="linhaArquivo" runat="server" visible="true">
                        <td align="left" >
                            <asp:Label ID="Label3" runat="server" Text="Selecione o arquivo para enviar (Somente arquivos em PDF, JPG, PNG, DOC, DOCX e RTF)"></asp:Label>
                            <br />
                            <br />
                            <asp:Label ID="Label4" runat="server" Text="   1 - Selecione o documento de identificação com foto"></asp:Label>
                            <br />
                            &nbsp;<asp:FileUpload ID="FileUpload1" runat="server" CssClass="botoes" />
                            <br />
                            <br />
                            <asp:Label ID="Label1" runat="server" Text="   2 - Selecione a Certidão de filiação"></asp:Label>
                            <br />
                            &nbsp;<asp:FileUpload ID="FileUpload2" runat="server" CssClass="botoes" />
                            <br />
                            <br /><asp:Label ID="Label2" runat="server" Text="   3 - Selecione a Ata devidamente registrada"></asp:Label>
                            <br />
                            &nbsp;<asp:FileUpload ID="FileUpload3" runat="server" CssClass="botoes" />
                            <br />
                            <br />
                            <asp:Button ID="btnEnviarDocumento" runat="server" CssClass="botoes" TabIndex="32" Text="Enviar" OnClick="btnEnviarDocumento_Click"  />
                            <asp:Button ID="btnEnviarDocumento0" runat="server" CssClass="botoes" OnClick="btnEnviarDocumento0_Click" TabIndex="32" Text="Enviar Depois" Visible="False" />
                            <br /><asp:Image ID="imgDocEnviado" runat="server" ImageUrl="~/img/accept18x18.png" Visible="False" />
                            <asp:Label ID="lblDocEnviado" runat="server" BackColor="#33CC33" Font-Bold="True" ForeColor="White" Text="&amp;nbsp;&amp;nbsp;Documento enviado&amp;nbsp;&amp;nbsp;" Visible="False"></asp:Label>  
                            &nbsp;&nbsp;<asp:Image ID="imgDocBaixado" runat="server" ImageUrl="~/img/accept18x18.png" Visible="False" />
                            <asp:Label ID="lblDocBaixado" runat="server" BackColor="#33CC33" Font-Bold="True" ForeColor="White" Text="&amp;nbsp;&amp;nbsp;Documento baixado&amp;nbsp;&amp;nbsp;" Visible="False"></asp:Label>  
                            
                             &nbsp;&nbsp;<asp:Label ID="lblSituacao" runat="server" BackColor="Blue" Font-Bold="True" ForeColor="White" Text="&amp;nbsp;&amp;nbsp;NÃO AVALIADO&amp;nbsp;&amp;nbsp;" Visible="False"></asp:Label>                                   
                        </td>
                    </tr>
                    <tr>                                
                        <td align="left" style="height: 26px;">
                            <asp:Label ID="lblMsg" runat="server" CssClass="lblMsg" Visible="False"></asp:Label>
                        </td>                                    
                    </tr>
                </table>        
            </ContentTemplate>
            <Triggers>

                    <asp:PostBackTrigger ControlID="btnEnviarDocumento" />

            </Triggers>
        </asp:UpdatePanel>  
        </div>  
</asp:Content>

