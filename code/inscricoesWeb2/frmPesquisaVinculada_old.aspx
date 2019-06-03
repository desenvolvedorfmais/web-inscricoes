<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmPesquisaVinculada_old.aspx.cs" Inherits="frmPesquisaVinculada_old" 
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
                <br />
                <div>
                    <asp:Label ID="lblID" runat="server" Width="80px" Text="Participante:" CssClass="label"></asp:Label>
                    <asp:Label ID="lblIdentificador" runat="server" Font-Bold="True" ForeColor="Navy"></asp:Label> 
                    &nbsp;- 
                    <asp:Label ID="lblNoParticipante" runat="server" Font-Bold="True" ForeColor="Navy"></asp:Label>
                </div>
                <br />
                            <asp:Table ID="tbPesqLivre" runat="server" Visible="false">
                                <asp:TableRow ID="line1" runat="server" CssClasss="colTituloGrupoPesquisa">
                                    <asp:TableCell ID="cel01" runat="server">
                                        <br />
                                        <asp:Label ID="cdGrupoPergunta" runat="server"  Visible="False"
                                            CssClass="lblTituloGrupoPesquisa"></asp:Label>
                                        <asp:Label ID="dsGrupoPergunta" runat="server" CssClass="lblTituloGrupoPesquisa"></asp:Label>
                                        <br /><br />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="line2" runat="server"  CssClasss="colTituloQuestao" >
                                    <asp:TableCell ID="cel02" runat="server">
                                        <asp:Label ID="cdQuestaoLabel" runat="server"  Visible="False"
                                            CssClass="lblTituloQuestao"></asp:Label>
                                        <asp:Label ID="dsQuestaoLabel" runat="server" CssClass="tituloQuestao"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="line3" runat="server" >
                                    <asp:TableCell ID="cel03" runat="server"  class="ckecklist">
                                        <asp:RadioButtonList ID="optResp" runat="server" RepeatColumns="1" 
                                            Width="700px">
                                        </asp:RadioButtonList>
                                        <asp:CheckBoxList ID="chkResp" runat="server" RepeatColumns="1" Width="700px">
                                        </asp:CheckBoxList>
                                        <asp:DropDownList ID="ddlResp" runat="server" 
                                            onselectedindexchanged="DropDownList1_SelectedIndexChanged" > 
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtResp" runat="server" Columns="50" Rows="5" 
                                            TextMode="MultiLine"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="optResp"
                                            Display="Dynamic" Enabled="True" 
                                            ErrorMessage="Selecione uma das alternativas"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlResp"
                                            Display="Dynamic" Enabled="True" 
                                            ErrorMessage="Selecione uma das alternativas"></asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
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

