<%@ Page Language="C#" MasterPageFile="MasterPageboleto.master" AutoEventWireup="true" CodeFile="frmBoleto.aspx.cs" Inherits="frmBoleto" Title="Inscri&ccedil;&otilde;es Web" %>

<%@ Register Assembly="ControlMessageBox" Namespace="ControlMessageBox" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="TSManager1" runat="server" EnableScriptLocalization="true"
            CombineScripts="false">
        </asp:ToolkitScriptManager>
    <asp:Button ID="btnImprimir" runat="server" OnClick="btnImprimir_Click" 
        Text="Imprimir" BackColor="Red" BorderColor="Red" Font-Bold="False" />
    &nbsp;<asp:Button ID="btnVoltar" runat="server" OnClick="btnVoltar_Click" 
        Text="Voltar" BackColor="Red" BorderColor="Red" Font-Bold="False" />&nbsp;
    <asp:Panel ID="panelBoleto" runat="server">
    </asp:Panel>
</asp:Content>

