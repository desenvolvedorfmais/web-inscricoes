<%@ Page Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="frm_mensagens.aspx.cs" Inherits="frm_mensagens" title="Inscri&ccedil;&otilde;es Web" %>

<%@ Register Assembly="ControlMessageBox" Namespace="ControlMessageBox" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="TSManager1" runat="server" EnableScriptLocalization="true"
            CombineScripts="false">
        </asp:ToolkitScriptManager>
<div id="frmCad_auto">
    <div id="printarea" runat="server" class="msg">
    
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
    <asp:Label ID="lblMsg2" runat="server" Visible="False" class="msg3"></asp:Label>
    <br /><br />
        &nbsp;<asp:Button ID="Button3" runat="server" Text="Ok" 
             UseSubmitBehavior="False" CssClass="botoes" Visible="False" />
    <asp:Button ID="Button1" runat="server" OnClick="Button_novo_Click" Text="Ok" 
             UseSubmitBehavior="False" CssClass="botoes" />
    <asp:Button ID="Button2" runat="server" Text="Ok" 
             UseSubmitBehavior="False" CssClass="botoes" OnClick="Button2_Click" /></div>
            <br />            
            <div id="telacad_info">
                    <asp:Label ID="lblInstrucoesRapidas" runat="server" Visible="false"></asp:Label>
                </div>
            </div>
    <br />

     <script type="text/javascript">
        $(document).on("click", "[id*=Button3]", function () {
            <%--//var cpf = $('#<%=txtCPF.ClientID%>').val();
            var nrPart = $('#<%=cdPart.ClientID%>').val();
            var nrAnoEvento = $('#<%=AnoEvento.ClientID%>').val();
            var rotina = $('#<%=lblRotina.ClientID%>').val();
            if (rotina.toUpperCase() == "2VBOLETO")
                var nrPart = $('#<%=nrBoleto.ClientID%>').val();
            //var rotina = document.getElementById('<%=lblRotina.ClientID %>').textContent;
            //var rotina = $('#<%=lblRotina.ClientID%>').text();                 
            showDialog(nrAnoEvento, nrPart, rotina);--%>
            printdiv("ctl00_ContentPlaceHolder1_printarea");
            return false;
        });
        
         function printdiv(printpage) {
             var headstr = "<html><head><title></title></head><body>";
             var footstr = "</body>";
             var newstr = document.all.item(printpage).innerHTML;
             var oldstr = document.body.innerHTML;
             document.body.innerHTML = headstr + newstr + footstr;
             window.print();
             document.body.innerHTML = oldstr;
             return false;
         }
        function PrintDiv() {
            var divToPrint = document.getElementById('ctl00_ContentPlaceHolder1_printarea');
            var popupWin = window.open('', '_blank', 'width=300,height=400,location=no,left=200px');
            popupWin.document.open();
            popupWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</html>');
            popupWin.document.close();
        }        
    </script>
</asp:Content>

