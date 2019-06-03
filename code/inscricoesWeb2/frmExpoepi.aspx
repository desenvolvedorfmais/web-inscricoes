<%@ Page Language="C#" MasterPageFile="~/MPExpoepi.master" AutoEventWireup="true" CodeFile="frmExpoepi.aspx.cs" Inherits="frmExpoepi" 
         Title="Inscri&ccedil;&otilde;es Web" %>

<%@ Register Assembly="ControlMessageBox" Namespace="ControlMessageBox" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptLocalization="true"
        CombineScripts="false">
    </asp:ToolkitScriptManager>
    <div id="frmCad" class="mylogin" >
       <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
            <ContentTemplate>
               <asp:Panel ID="Panel1" runat="server">
            <h3>
                <asp:Label ID="lblTituloTelaEsq" runat="server" Text="Verificar Passagem/Hospedagem" CssClass="titulo"></asp:Label></h3>
            <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>--%>
                    <asp:Label ID="txtMsg" runat="server" ForeColor="Red"></asp:Label>
                <%--</ContentTemplate>
            </asp:UpdatePanel>--%>
            <%--<center>--%>
                <table cellspacing="10">
                    <tr>
                        <td align="right" valign="top">
                            <asp:Label ID="LBLCONTA" runat="server" Text="Informe o 
                            CPF"></asp:Label>
                        </td>
                        <td align="left" style="width: 227px">
                            <asp:TextBox ID="TXTDsCPF" MaxLength="14" runat="server" CssClass="text" onkeypress="return Mascarar(this, event, '999.999.999-99')"
                                ReadOnly="false" TabIndex="1"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                    ControlToValidate="TXTDsCPF" Display="Dynamic" ErrorMessage="Campo requerido"
                                    ID="RFVCPF" runat="server"></asp:RequiredFieldValidator>
                        </td>
                        <td align="left">
                            <asp:Button ID="Verificar" runat="server" CssClass="botoes" TabIndex="4" 
                                Text="Verificar" onclick="Verificar_Click" />
                        </td>
                    </tr>
                </table>
           <%-- </center>--%>
        </div>
        
        <br />
        &nbsp;<%--</center>--%></div></asp:Panel>
                <div id="telacad_info" >
        <asp:Label ID="lblInstrucoesRapidas" runat="server" CssClass="tttt"></asp:Label>
    </div>
    
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

