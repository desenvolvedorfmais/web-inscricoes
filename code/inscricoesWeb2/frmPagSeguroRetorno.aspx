﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmPagSeguroRetorno.aspx.cs" Inherits="frmPagSeguroRetorno" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1 runat="server" id="header">Aguarde....</h1>
    <asp:ToolkitScriptManager ID="TSManager1" runat="server" EnableScriptLocalization="true"
            CombineScripts="false">
        </asp:ToolkitScriptManager>
	<asp:PlaceHolder ID="data" runat="server">
    
    <asp:Label ID="lblMsg" runat="server" ></asp:Label>
    
    </asp:PlaceHolder>
</asp:Content>

