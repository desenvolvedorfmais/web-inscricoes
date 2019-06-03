<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmCadastrarTese.aspx.cs" Inherits="frmCadastrarTese" 
     Title="Inscri&ccedil;&otilde;es Web" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="frmCad_auto">
        <asp:ToolkitScriptManager ID="TSManager1" runat="server" EnableScriptLocalization="true"
            CombineScripts="false">
        </asp:ToolkitScriptManager>
        
        <h1>
            <asp:Label ID="lblTituloPagina" runat="server" CssClass="titulo" 
                Text="Tese - Cadastro"></asp:Label>
            <br />
        </h1>
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
                <table border="0" cellpadding="0" cellspacing="10" width="710">
                    <tr>
                        <td align="left" style="width: 700px">
                            <br />
                            <asp:Label ID="Label1" runat="server" Text="ID"></asp:Label>
                            <br />
                            <asp:Label ID="txtCdTese" runat="server" Font-Bold="False" Font-Size="Larger" 
                                ForeColor="Red"></asp:Label>
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp;
                            </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 700px">
                            <asp:Label ID="Label2" runat="server" Text="Eixo da Tese"></asp:Label>
                            <br />
                            <asp:DropDownList ID="txtTipoTese" runat="server" 
                                CssClass="txt_upper" 
                                TabIndex="9" Width="145px">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>EIXO I</asp:ListItem>
                                <asp:ListItem>EIXO II</asp:ListItem>
                                <asp:ListItem>EIXO III</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTipoTese"
                                Display="Dynamic" Enabled="True" ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 700px">
                            <asp:Label ID="Label5" runat="server" Text="Assunto da Tese"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtAssuntoTese" runat="server" CssClass="txt_upper" 
                                MaxLength="250" TabIndex="2"
                                Width="700px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
                                ControlToValidate="txtAssuntoTese" Display="Dynamic" Enabled="True" 
                                ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 700px">
                            <asp:Label ID="Label151" runat="server" 
                                Text="Introdução / Considerações / Abordagem sobre o Tema "></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label157" runat="server" ForeColor="Red" 
                                Text=" (Máximo de 2.100 caracteres, com espaços)"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" 
                                ControlToValidate="txtIntroducao" Display="Dynamic" Enabled="True" 
                                ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                            <br />
                            <asp:TextBox ID="txtIntroducao" runat="server" CssClass="txtarea" 
                                MaxLength="2100" TabIndex="3"
                                Width="700px" Height="400px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 700px">
                            <asp:Label ID="Label152" runat="server" 
                                Text="Proposta / Diretriz a ser debatida nos Grupos de Trabalhos"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="Label158" runat="server" ForeColor="Red" 
                                Text=" (Máximo de 400 caracteres, com espaços)"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" 
                                ControlToValidate="txtProposta" Display="Dynamic" Enabled="True" 
                                ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                            <br />
                            <asp:TextBox ID="txtProposta" runat="server" Height="105px" TabIndex="12" TextMode="MultiLine"
                                Width="700px" CssClass="txtarea" MaxLength="400"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 700px">
                            <a href="#btnBoleto"></a>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" >
                            <%--</td>
            <td>--%>
                            <asp:Button ID="btnListaTeses" runat="server" CssClass="botoes" Text="Voltar"
                                CausesValidation="False" PostBackUrl="~/frmTesesLista.aspx" Width="144px"
                                TabIndex="13" />
                            <asp:Button ID="btnNovo" runat="server" CssClass="botoes"
                                Text="Novo" CausesValidation="False" TabIndex="14" 
                                onclick="btnNovo_Click" />
                            <asp:Button ID="btnGravar" runat="server" CssClass="botoes" Text="Gravar"
                                TabIndex="15" onclick="btnGravar_Click"  />
                            <asp:Button ID="btnExcluir" runat="server" CssClass="botoes" Text="Excluir" 
                                TabIndex="16" CausesValidation="False" 
                                BackColor="Red" ForeColor="White" onclick="btnExcluir_Click"  />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="height: 26px;">
                            <asp:Label ID="lblMsg" runat="server" Font-Size="Small" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        
        <asp:UpdatePanel ID="up3" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlAcomodacoes" runat="server" Visible="false">
                    <table border="0" cellpadding="0" cellspacing="10" width="710">
                        <tr>
                            <td align="left" colspan="2" valign="top">
                                <h3 align="left">
                                    Subscritores da Tese</h3>
                                <asp:Label ID="Label3" runat="server" Text="Mínimo de 10 Servidores"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="1" style="width: 143px" valign="top">
                                <asp:Label ID="Label154" runat="server" Text="ID"></asp:Label>
                                <br />
                            </td>
                            <td>
                                <asp:Label ID="txtCdParticipanteTese" runat="server" Font-Bold="False" Font-Size="Larger"
                                    ForeColor="Red"></asp:Label>
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;
                                </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="1" style="width: 143px; height: 34px;" valign="top">
                                <asp:Label ID="Label155" runat="server" Text="Tipo"></asp:Label>
                            </td>
                            <td style="height: 34px">
                                <asp:DropDownList ID="txtTpTese" runat="server" DataValueField="value" 
                                    TabIndex="17" Width="226px" CssClass="txt_upper">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem>AUTOR</asp:ListItem>
                                    <asp:ListItem>COAUTOR</asp:ListItem>
                                    <asp:ListItem>APOIADOR</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="1" style="width: 143px" valign="top">
                                <asp:Label ID="Label156" runat="server" Text="Nome"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNoParticipanteTese" runat="server" TabIndex="18" 
                                    Width="494px" CssClass="txt_upper" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="1" style="width: 143px" valign="top">
                                <asp:Label ID="Label159" runat="server" Text="CPF"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCPFTese" runat="server" CssClass="txt" MaxLength="14" 
                                    onkeypress="return CPFCNPJMascarar(this, event)" TabIndex="18" 
                                    Width="154px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="1" style="width: 143px" valign="top">
                                <asp:Label ID="Label160" runat="server" Text="Lotação"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtLotacao" runat="server" CssClass="txt_upper" 
                                    TabIndex="18" Width="492px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="1" style="width: 143px" valign="top">
                                <br />
                            </td>
                            <td>
                                <asp:Button ID="btnNovaSubscritor" runat="server" CausesValidation="False" 
                                    CssClass="botoes" TabIndex="19" Text="Novo" 
                                     />
                                <asp:Button ID="btnGravarSubscritor" runat="server" CssClass="botoes"
                                    TabIndex="20" Text="Gravar" />
                                <asp:Button ID="btnExcluirSubscritor" runat="server" CssClass="botoes"
                                    TabIndex="21" Text="Excluir" CausesValidation="False" BackColor="Red" 
                                    ForeColor="White" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2" style="height: 24px;">
                                <asp:GridView ID="grdSubscritor" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="None" Width="707px" CellSpacing="1" 
                                    Caption="Subscritores da Tese" TabIndex="22" 
                                    DataKeyNames="cdParticipanteTese" 
                                    onselectedindexchanging="grdSubscritor_SelectedIndexChanging">
                                    <RowStyle BackColor="#EFF3FB" />
                                    <Columns>
                                        <asp:CommandField SelectText="Selecionar" ShowSelectButton="True">
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle Width="80px" />
                                        </asp:CommandField>
                                        <asp:BoundField DataField="cdParticipanteTese" HeaderText=" ID" 
                                            SortExpression="cdParticipanteTese">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle Height="22px" HorizontalAlign="Center" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="noParticipanteTese" HeaderText="Nome" 
                                            SortExpression="noParticipanteTese">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Height="22px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tpParticipacao" HeaderText="Tipo">
                                            <ItemStyle Width="100px" />
                                        </asp:BoundField>
                                    </Columns>
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle Font-Bold="True" ForeColor="White" CssClass="grdHeader" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>

