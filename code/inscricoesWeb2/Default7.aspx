<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default7.aspx.cs" Inherits="Default7" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
            EnableScriptLocalization="true" CombineScripts="false">
        </asp:ToolkitScriptManager>
<div>
        <asp:DropDownList ID="webmenu" style="width:261px;" runat="server">
           <%-- <option value="" data-description="Choos your payment gateway">Payment Gateway</option>
            <option value="amex" title="img/visa.gif" data-description="My life. My card...">Amex</option>
            <option value="Discover" title="img/visa.gif" data-description="It pays to Discover...">Discover</option>--%>
            <%--<asp:ListItem Value="" data-description="Choos your payment gateway">Payment Gateway</asp:ListItem>
            <asp:ListItem value="amex" title="img/visa.gif" data-description="My life. My card...">Amex</asp:ListItem>
            <asp:ListItem value="Discover" title="img/visa.gif" data-description="It pays to Discover...">Discover</asp:ListItem>--%>
        </asp:DropDownList>
        &nbsp;asdasd<br />
        <br />
        <asp:ImageButton ID="ImageButton1" runat="server" 
            onclick="ImageButton1_Click" />
    </div>
    <script type="text/javascript" language="JavaScript">

        jQuery(document).ready(
                    function (e) {
                        //try {
                        jQuery("#ctl00_ContentPlaceHolder1_webmenu").msDropdown({ visibleRows: 4 });
                        //} catch (e) {
                        //    alert('teste123');
                       // }
                    }
        );
    </script>


</asp:Content>

