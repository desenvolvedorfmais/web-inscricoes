<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmPesquisaAvulsa_old.aspx.cs" Inherits="frmPesquisaAvulsa_old" 
   Title="Inscri&ccedil;&otilde;es Web"  EnableEventValidation="false" ValidateRequest="false"%>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="scripts/MSGAguarde.js" type="text/javascript"></script>
    <div id="frmCad_auto">
        <h3>
            <asp:Label ID="lblTitulo" runat="server" Text="Pesquisa de Opinião" CssClass="titulo"></asp:Label></h3>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
            EnableScriptLocalization="true" CombineScripts="false">
        </asp:ToolkitScriptManager>
        
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
                            <table>
                                <tr>
                                    <td style="height: 34px; width: 698px;" align="center" bgcolor="WhiteSmoke">
                                        <br />
                                        <asp:Label ID="cdGrupoPergunta" runat="server"  Visible="False"
                                            CssClass="tituloQuestao"></asp:Label>
                                        <asp:Label ID="dsGrupoPergunta" runat="server" CssClass="tituloQuestao"></asp:Label>
                                        <br /><br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 34px; width: 698px;">
                                        <asp:Label ID="cdQuestaoLabel" runat="server"  Visible="False"
                                            CssClass="tituloQuestao"></asp:Label>
                                        <asp:Label ID="dsQuestaoLabel" runat="server" CssClass="tituloQuestao"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ckecklist" style="width: 698px">
                                        <asp:RadioButtonList ID="optResp" runat="server" RepeatColumns="1" 
                                            Width="700px">
                                        </asp:RadioButtonList>
                                        <asp:CheckBoxList ID="chkResp" runat="server" RepeatColumns="1" Width="700px">
                                        </asp:CheckBoxList>
                                        <asp:TextBox ID="txtResp" runat="server" Columns="50" Rows="5" 
                                            TextMode="MultiLine"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="optResp"
                                            Display="Dynamic" Enabled="True" 
                                            ErrorMessage="Selecione uma das alternativas"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                            <asp:Button ID="btnContinuar" runat="server" CssClass="botoes" 
                                 Text="Continuar" Visible="true" onclick="btnContinuar_Click" />
                            <asp:Button ID="btnGravar" runat="server" 
                                CssClass="botoes" Font-Bold="True" ForeColor="White"  
                                Text="Concluir" Visible="False" Width="150px" onclick="btnGravar_Click1" />
                            <br />
                            <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                            <br />
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
    </div>
</asp:Content>

