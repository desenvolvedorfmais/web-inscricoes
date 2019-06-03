<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmPesquisaVinculada.aspx.cs" Inherits="frmPesquisaVinculada" 
   Title="Inscri&ccedil;&otilde;es Web"  EnableEventValidation="true" ValidateRequest="false"  %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="scripts/MSGAguarde.js" type="text/javascript"></script>
    <div id="frmCad_auto">
        <div id="fromCadastro" class="centralizarForm">
            <h3>
                <asp:Label ID="lblTitulo" runat="server" Text="Pesquisa" CssClass="titulo"></asp:Label></h3>
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
                EnableScriptLocalization="true" CombineScripts="false" EnablePartialRendering="true" EnablePageMethods="true">
            </asp:ToolkitScriptManager>
           <asp:ImageButton ID="btnFoco" runat="server" ImageUrl="~/img/vazio.png" />
            <asp:UpdatePanel ID="up1" runat="server" Width="100%">
                <ContentTemplate>
                <div id="Questionario" class="clsQuestionario">
                    
                    <div id="ParticipantePesquisa" class="clsParticipantePesquisa">
                        <asp:Label ID="lblID" runat="server" Text="Participante:" CssClass="lblLabel"></asp:Label>
                        &nbsp;
                        <asp:Label ID="lblIdentificador" runat="server" CssClass="lblParticipantePesquisa"></asp:Label> 
                        &nbsp;- 
                        <asp:Label ID="lblNoParticipante" runat="server" CssClass="lblParticipantePesquisa"></asp:Label>
                    
                    </div>
                    
                    <asp:Panel ID="pnlPequisa" class=".clspnlPequisa" runat="server" Width="100%">

                        <asp:DataList ID="DataList0" runat="server" DataKeyField="cdGrupoPergunta" 
                             OnItemDataBound="DataList0_ItemDataBound">
                            <FooterTemplate>                                       
                            </FooterTemplate>
                            <ItemTemplate>                                    
                                <asp:Table ID="tbGrupo" runat="server" >
                                    <asp:TableRow ID="lg0" runat="server" CssClass="colTituloGrupoPesquisa" >
                                        <asp:TableCell ID="col000" runat="server">
                                            <asp:HiddenField ID="HiddenField5" runat="server" 
                                                Value='<%# Eval("cdQuestionario") %>' />
                                            <asp:Label ID="lbl_cdGrupoPergunta" runat="server" CssClass="lblTituloGrupoPesquisa" 
                                                Text='<%# Eval("cdGrupoPergunta") %>' Visible="False"></asp:Label>
                                            <asp:Label ID="lbl_dsGrupoPergunta" runat="server" CssClass="lblTituloGrupoPesquisa" 
                                                Text='<%# Eval("dsGrupoPergunta") %>'></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow ID="TableRow1" runat="server" CssClass="linhaPesquisaGeral">
                                        <asp:TableCell ID="TableCell1" runat="server">
                                            <asp:DataList ID="DataList1" runat="server" DataKeyField="cdQuestionario" 
                                                OnItemDataBound="DataList1_ItemDataBound">
                                                <FooterTemplate>                              
                                                    <%--<br />
                                                    <asp:HiddenField ID="HiddenField50" runat="server" Value='<%# Eval("cdQuestionario") %>' />
                                                    <asp:Button ID="btnConcluirParcial" runat="server" CssClass="botoes" Text="Salvar" Visible="true" Width="150px"   
                                                    CausesValidation="false" onclick="btnConcluirParcial_Click"  />
                                                    <br />  --%>             
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Table ID="tb1" runat="server" CssClass="linhaPesquisa">
                                                        <asp:TableRow ID="linha1" runat="server" CssClass="colTituloQuestao">
                                                            <asp:TableCell ID="col11" runat="server">
                                                                <asp:Label ID="cdQuestaoLabel" runat="server" CssClass="lblTituloQuestao" 
                                                                    Text='<%# Eval("cdQuestao") %>' Visible="False"></asp:Label>
                                                                <asp:Label ID="dsQuestaoLabel" runat="server" CssClass="lblTituloQuestao" 
                                                                    Text='<%# Eval("dsQuestao") %>'></asp:Label>
                                                            
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                        <asp:TableRow ID="linha2" runat="server" CssClass="linhaQuestao">
                                                            <asp:TableCell ID="col21" runat="server">
                                                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" CssClass="RadioButtonListPesquisa">
                                                                </asp:RadioButtonList>
                                                                <asp:CheckBoxList ID="CheckBoxList1" runat="server"  CssClass="CheckBoxListPesquisa">
                                                                </asp:CheckBoxList>
                                                                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="DropDownListPesquisa"
                                                                    onselectedindexchanged="DropDownList1_SelectedIndexChanged" > 
                                                                    </asp:DropDownList>
                                                                <asp:TextBox ID="TextBox1" runat="server" Columns="50" Rows="5" CssClass="TextBoxPesquisa"
                                                                    TextMode="MultiLine"></asp:TextBox>
                                                                <asp:HiddenField ID="HiddenField1" runat="server" 
                                                                    Value='<%# Eval("TpQuestao") %>' />
                                                                <asp:HiddenField ID="HiddenField2" runat="server" 
                                                                    Value='<%# Eval("cdQuestionario") %>' />
                                                                <asp:HiddenField ID="HiddenField3" runat="server" 
                                                                    Value='<%# Eval("cdGrupoPergunta") %>' />
                                                                <asp:HiddenField ID="HiddenField4" runat="server" 
                                                                    Value='<%# Eval("nrColunas") %>' />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                                    ControlToValidate="RadioButtonList1" Display="Dynamic" 
                                                                    ErrorMessage="Selecione uma das alternativas" Visible="True" ></asp:RequiredFieldValidator>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                                                    ControlToValidate="DropDownList1" Display="Dynamic"  
                                                                    ErrorMessage="Selecione uma das alternativas" Visible="True" ></asp:RequiredFieldValidator>
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                    </asp:Table>
                                                </ItemTemplate>
                                                <ItemStyle Font-Strikeout="False" />
                                                <HeaderStyle />
                                                <HeaderTemplate>
                                                    <center>
                                                        <asp:Label ID="dsGrupoLabel" runat="server" Visible="true"></asp:Label>
                                                    </center>
                                                </HeaderTemplate>
                                            </asp:DataList>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </ItemTemplate>
                            <HeaderTemplate>
                            </HeaderTemplate>
                        </asp:DataList>
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
                            <asp:TableRow ID="line2" runat="server"  CssClasss="colTituloQuestao">
                                <asp:TableCell ID="cel02" runat="server">
                                    <asp:Label ID="cdQuestaoLabel" runat="server"  Visible="False"
                                        CssClass="lblTituloQuestao"></asp:Label>
                                    <asp:Label ID="dsQuestaoLabel" runat="server" CssClass="tituloQuestao"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow ID="line3" runat="server">
                                <asp:TableCell ID="cel03" runat="server"  class="ckecklist">
                                    <asp:RadioButtonList ID="optResp" runat="server" RepeatColumns="1" >
                                    </asp:RadioButtonList>
                                    <asp:CheckBoxList ID="chkResp" runat="server" RepeatColumns="1" >
                                    </asp:CheckBoxList>
                                    <asp:DropDownList ID="ddlResp" runat="server" > 
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
                    </asp:Panel>

                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                    <br />
                    <asp:Button ID="btnContinuarPesquisa" runat="server" CssClass="botoes" 
                            Text="Continuar" Visible="true" onclick="btnContinuar_Click" />
                    <asp:Button ID="btnConcluirPesquisa" runat="server" 
                        CssClass="botoes" 
                        Text="Concluir" Visible="False" Width="150px" 
                        onclick="btnConcluir_Click1" />
                                <asp:Button ID="btnAbandonarPesquisa" runat="server" BackColor="Red" 
                        CausesValidation="False" CssClass="botoes" onclick="btnAbandonar_Click" 
                        Text="Abandonar" Visible="False" />
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server">
                    </asp:SqlDataSource>
                                <asp:SqlDataSource ID="SqlDataSource2" runat="server">
                    </asp:SqlDataSource>
                                <br />
                </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        <br />
        </div>
    </div>
</asp:Content>

