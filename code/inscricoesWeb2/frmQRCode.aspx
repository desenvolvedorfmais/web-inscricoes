<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmQRCode.aspx.cs" Inherits="frmQRCode" Title="Inscri&ccedil;&otilde;es Web"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="frmCad_auto">
        <div id="fromCadastro" class="centralizarForm">
            <h3>
                <asp:Label ID="lblTitulo" runat="server" Text="Confirmação" CssClass="titulo"></asp:Label>
            </h3>
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
                EnableScriptLocalization="true" CombineScripts="false">
            </asp:ToolkitScriptManager>
        
            <asp:UpdateProgress runat="server" id="UpdateProgress1" DisplayAfter="0"  >
                <ProgressTemplate>
                
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:ModalPopupExtender ID="modalExtender" runat="server" TargetControlID="UpdateProgress1"
                PopupControlID="pnlAguarde" DropShadow="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlAguarde" runat="server" CssClass="modalExtender">
                <div class="loading" align="center">
                        <br />Aguarde!... Processando sua solicitação. <br />
                        <br />
                        <img src="imagensgeral/loader.gif" alt="" />
                 </div>
            </asp:Panel>
            <br/><br/>
            <asp:Label ID="lblMsgConfirmacao" runat="server" ></asp:Label>
        </div>
    </div>
</asp:Content>

