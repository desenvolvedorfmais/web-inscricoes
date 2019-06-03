<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmCadastroAuto.aspx.cs" Inherits="frmCadastroAuto" Title="Inscri&ccedil;&otilde;es Web"
    EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="scripts/MSGAguarde.js" type="text/javascript"></script>
    
    <div id="frmCad_auto">
        <div id="fromCadastro" class="centralizarForm">
        <h3>
            <asp:Label ID="lblTitulo" runat="server" Text="Cadastro" CssClass="titulo"></asp:Label></h3>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
            EnableScriptLocalization="true" CombineScripts="false">
        </asp:ToolkitScriptManager>
        <asp:ImageButton ID="btnFoco" runat="server" ImageUrl="~/img/vazio.png" />
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
                <asp:Panel ID="form1" runat="server"  >
                    <%--<div ID="mnuCred" runat="server" visible="false">
                        <asp:ImageButton ID="btnLimpar" runat="server" CausesValidation="False" 
                            ImageUrl="~/img/btnLimpar.jpg" onclick="btnLimpar_Click" />
                        <asp:ImageButton ID="btnLocalizar" runat="server" 
                            ImageUrl="~/img/btnLocalizar.jpg" onclick="btnLocalizar_Click" 
                            CausesValidation="False" />
                        <asp:ImageButton ID="btnNovos" runat="server" ImageUrl="~/img/btnNovo.jpg" 
                            CausesValidation="False" onclick="btnNovos_Click" ToolTip="Novo Participante" />
                        <asp:ImageButton ID="btnAlterar" runat="server" ImageUrl="~/img/btnEditar.jpg"  
                            Enabled="False" onclick="btnAlterar_Click" CausesValidation="False"  />
                        <asp:ImageButton ID="btnConfirmar" runat="server" 
                            ImageUrl="~/img/btnConfirmar.jpg" Enabled="false" 
                            onclick="btnConfirmar_Click" />
                        <asp:ImageButton ID="btnCancelar" runat="server" 
                            ImageUrl="~/img/btnCancelar.jpg" Enabled="false" CausesValidation="False" 
                            onclick="btnCancelar_Click" />
                        <asp:ImageButton ID="btnAtividade" runat="server" 
                            ImageUrl="~/img/btnAtividades.jpg" Enabled="false" 
                            onclick="btnAtividade_Click" />
                        <asp:ImageButton ID="btnEtiquetas" runat="server" 
                            ImageUrl="~/img/btnPrtEtiqueta.jpg"  Enabled="false" 
                            onclick="btnEtiquetas_Click" CausesValidation="False"  />
                        <asp:ImageButton ID="ImageButton9" runat="server"  Enabled="false" visible="false" />
                    </div>--%>
                    
                    <asp:Label ID="lblMsg" runat="server" CssClass="lblMsg" Visible="False"></asp:Label>
                </asp:Panel>
                <asp:Panel ID="pnlPequisa" runat="server">
                    <asp:DataList ID="DataList1" runat="server" DataKeyField="cdQuestionario" DataSourceID="SqlDataSource1"
                        OnItemDataBound="DataList1_ItemDataBound">
                        <FooterTemplate>
                            <asp:Button ID="btnGravar" runat="server" OnClick="btnGravar_Click" Text="Gravar"
                                Visible="False" />
                        </FooterTemplate>
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td style="height: 34px">
                                        <asp:Label ID="cdQuestaoLabel" runat="server" Text='<%# Eval("cdQuestao") %>' Visible="False"
                                            CssClass="titulo"></asp:Label>
                                        <asp:Label ID="dsQuestaoLabel" runat="server" Text='<%# Eval("dsQuestao") %>'
                                                CssClass="tituloQuestao"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ckecklist">
                                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatColumns="1" 
                                            Width="700px">
                                        </asp:RadioButtonList>
                                        <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatColumns="2">
                                        </asp:CheckBoxList>
                                        <asp:TextBox ID="TextBox1" runat="server" Columns="50" Rows="5" TextMode="MultiLine"></asp:TextBox><asp:HiddenField
                                            ID="HiddenField1" runat="server" Value='<%# Eval("TpQuestao") %>' />
                                        <asp:HiddenField ID="HiddenField2" runat="server" Value='<%# Eval("cdQuestionario") %>' />
                                        <asp:HiddenField ID="HiddenField3" runat="server" Value='<%# Eval("cdGrupoPergunta") %>' />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="RadioButtonList1"
                                            Display="Dynamic" Enabled="True" ErrorMessage="Selecione uma das alternativas"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </ItemTemplate>
                        <ItemStyle Font-Strikeout="False" />
                        <HeaderTemplate>
                            <asp:Label ID="dsGrupoLabel" runat="server" Text='<%# Eval("dsGrupoPergunta") %>'></asp:Label>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("dsQuestao") %>'></asp:Label>
                        </HeaderTemplate>
                    </asp:DataList></asp:Panel>
                <asp:Panel ID="Panel2" runat="server">
                    <div id="AtencaoIESB" runat="server" visible="false">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/imagensgeral/005701/img-06.png" />
                        <a href="mailto:departamentodemarketing@iesb.br" id="btn-3" class="btn"></a>
                    </div>
                    <asp:Button
                            ID="btnVoltarSair" runat="server" Text="Voltar" CssClass="botoes"
                            Visible="False"  
                        PostBackUrl="~/frmSessaoExpirada.aspx" CausesValidation="False" />
                    <asp:Button ID="btnNovo" runat="server" CssClass="botoes" onclick="btnNovo_Click" Text="Novo" Visible="False" />
                    <asp:Button ID="btnGravarParticipante" runat="server" Text="Gravar" OnClick="Button1_Click" CssClass="botoes"/>
                    <asp:Button
                            ID="btnEtiqueta" runat="server" Text="Etiqueta" 
                        OnClick="btnEtiqueta_Click" CssClass="botoes"
                            Visible="False" />
                    <asp:Button ID="btnAtividades" runat="server" CssClass="botoes" 
                        OnClick="Button2_Click" Text="Atividades" Visible="False" />
                    <asp:Button ID="btnEnviarDocumento" runat="server" CssClass="botoes" PostBackUrl="~/frmEnviarDocumento.aspx" Text="Enviar Documento" Visible="False" CausesValidation="False" OnClick="btnEnviarDocumento_Click" />
                    <asp:Button ID="btnImprimirCredencial" runat="server" CssClass="botoes" PostBackUrl="~/frmCredencial.aspx" Text="Imprimir Credencial" Visible="False" CausesValidation="False" />
                    <asp:Button ID="btnDesconfirmar" runat="server" CausesValidation="False" CssClass="botoes" Text="Não Posso Ir" Visible="False" OnClick="btnDesconfirmar_Click" />
                    <br />
                    <div id="logoIESB" runat="server" visible="false">
                        <a id="logo" href="http://iesb.br" target="_blank">
                            <asp:Image ID="Image2" runat="server" ImageUrl="~/imagensgeral/005701/logo.png" />

                        </a>
                    </div>
                    <div id="socialIESB" runat="server" visible="false">
                    <asp:Image ID="Image3" runat="server" ImageUrl="~/imagensgeral/005701/social.png" />
                    <a href="https://twitter.com/IESB_OFICIAL" id="l-twitter" class="btn"></a>
                    <a href="http://facebook.com/iesb" class="btn" id="l-facebook"></a>
                    <a href="http://www.youtube.com/user/CanalIESB" class="btn" id="l-youtube"></a>
                        
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblAbrePainel" runat="server"></asp:Label>
                <asp:ModalPopupExtender ID="pnlCampos_ModalPopupExtender" runat="server" 
                    BackgroundCssClass="modalBackground" CancelControlID="btnVoltar" 
                    DropShadow="true" DynamicServicePath="" Enabled="True" OkControlID="" 
                    OnOkScript="" PopupControlID="pnlCampos" PopupDragHandleControlID="pnlCampos" 
                    TargetControlID="lblAbrePainel" X="0" Y="100">
                </asp:ModalPopupExtender>
            <br />

        <asp:Panel ID="pnlCampos" runat="server" BorderColor="#CCFFFF"  
            CssClass="modalPopup">
            <div ID="Panel3" runat="server" style="cursor: move;background-color:#DDDDDD;border:solid 1px Gray;color:Black">
                
                    <p>Pequisa Participante</p>
                
            </div>
            <table border="0" cellpadding="0" cellspacing="10" width="680">
                <tr>
                    <td align="right" style="width: 140px">
                        <asp:Label ID="Label2" runat="server" Text="1º Nome"></asp:Label>
                    </td>
                    <td style="width: 400px" align="left">
                        <asp:TextBox ID="txtFiltroNome1" runat="server" CssClass="txt" Width="400px"></asp:TextBox>
                    </td>
                    <td style="width: 140px" align="left">
                        <asp:ImageButton ID="btnFiltrarParticipantes" runat="server" 
                            ImageUrl="~/img/btnPesq.jpg" CausesValidation="False" 
                            onclick="btnFiltrarParticipantes_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 140px">
                        <asp:Label ID="Label3" runat="server" Text="2º Nome"></asp:Label>
                    </td>
                    <td style="width: 400px" align="left">
                        <asp:TextBox ID="txtFiltroNome2" runat="server" CssClass="txt" Width="400px"></asp:TextBox>
                    </td>
                    <td style="width: 140px" align="left">
                        <asp:ImageButton ID="btnLimparFiltro" runat="server" 
                            ImageUrl="~/img/btnLimp.jpg" CausesValidation="False" 
                            onclick="btnLimparFiltro_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 140px">
                        <asp:Label ID="Label4" runat="server" Text="3º Nome"></asp:Label>
                    </td>
                    <td style="width: 400px" align="left">
                        <asp:TextBox ID="txtFiltroNome3" runat="server" CssClass="txt" Width="400px"></asp:TextBox>
                    </td>
                    <td style="width: 140px" align="left">
                        <asp:ImageButton ID="btnVoltar" runat="server" 
                            ImageUrl="~/img/btnSair.jpg" CausesValidation="False" />
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 140px">
                        <asp:Label ID="Label5" runat="server" Text="E-mail"></asp:Label>
                    </td>
                    <td style="width: 400px" align="left">
                        <asp:TextBox ID="txtFiltroEmail" runat="server" CssClass="txt_lower" 
                            Width="400px"></asp:TextBox>
                    </td>
                    <td style="width: 140px" align="left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="right" style="width: 140px">
                        <asp:Label ID="Label6" runat="server" Text="Doc Identificador"></asp:Label>
                    </td>
                    <td style="width: 400px" align="left">
                        <asp:TextBox ID="txtFiltroDocIdent" runat="server" Width="400px"></asp:TextBox>
                    </td>
                    <td style="width: 140px" align="left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="right" style="width: 140px">
                        <asp:Label ID="Label7" runat="server" Text="Instituição/Órgão"></asp:Label>
                    </td>
                    <td style="width: 400px" align="left">
                        <asp:TextBox ID="txtFiltroInstOrgao" runat="server" Width="400px"></asp:TextBox>
                    </td>
                    <td style="width: 140px" align="left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="right" style="width: 140px">
                        <asp:Label ID="Label8" runat="server" Text="Categoria"></asp:Label>
                    </td>
                    <td style="width: 400px" align="left">
                        <asp:DropDownList ID="txtFiltroCateg" runat="server" Width="400px">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 140px" align="left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td  align="left" colspan="3">
                        <asp:Label ID="lblMsgPesq" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="grdParticpante" runat="server" AllowPaging="True" 
                AutoGenerateColumns="False" CellPadding="4" CellSpacing="1" 
                DataKeyNames="cdParticipante" ForeColor="#333333" GridLines="None" 
                onpageindexchanging="grdParticpante_PageIndexChanging" 
                onselectedindexchanging="grdParticpante_SelectedIndexChanging" PageSize="20" 
                TabIndex="9" Width="780px">
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:CommandField ButtonType="Image" CausesValidation="False" 
                        SelectImageUrl="~/img/editar_18x18.png" ShowSelectButton="True">
                        <HeaderStyle Width="18px" />
                    </asp:CommandField>
                    <asp:BoundField DataField="cdParticipante" HeaderText="Nr Inscrição" 
                        SortExpression="cdParticipante">
                        <HeaderStyle Width="80px" />
                        <ItemStyle Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="noParticipante" HeaderText="Nome" 
                        SortExpression="noParticipante">
                        <HeaderStyle HorizontalAlign="Left" Width="300px" />
                        <ItemStyle HorizontalAlign="Left" Width="300px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="nuCPFCNPJ" HeaderText="CPF" 
                        SortExpression="nuCPFCNPJ">
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" />
                    </asp:BoundField>
                    <asp:CheckBoxField DataField="flAtivo" HeaderText="Ativo">
                        <HeaderStyle Width="20px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:CheckBoxField>
                </Columns>
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
                <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
            </asp:GridView>
            <br />

        </asp:Panel>   
        
        </ContentTemplate>
        </asp:UpdatePanel>   --%>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
        <asp:Button
                            ID="btnAtividades0" runat="server" Text="Atividades" 
                        OnClick="btnAtividades0_Click" CssClass="botoes" 
            Visible="False" />
        <br />
        </div>
    </div>
    
</asp:Content>
