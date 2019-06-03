<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmAtividadesPedido.aspx.cs" Inherits="frmAtividadesPedido" Title="Inscri&ccedil;&otilde;es Web" %>

<%@ Register Assembly="ControlMessageBox" Namespace="ControlMessageBox" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="false"
        EnableScriptLocalization="true">
    </asp:ToolkitScriptManager>
    <div id="frmCad_auto">
        <h3>
            <asp:Label ID="lblTituloPagina" runat="server" Text="Pedido de Inscrição" CssClass="titulo"></asp:Label><br />
            <br />
        </h3>
        <div id="pedidos_esq">                
            <div style="float: left;">
                <asp:Label ID="lblID" runat="server" Width="100px" CssClass="label" Text="Nr Cadastro:"></asp:Label>                    
                &nbsp;<asp:Label ID="lblIdentificador" runat="server" Font-Bold="False"></asp:Label></div>
            <div style="float: left;">
                <asp:Label ID="lblPart" runat="server" Width="100px" CssClass="label" Text="Participante:"></asp:Label>                    
                &nbsp;<asp:Label ID="lblNoParticipante" runat="server" Font-Bold="False"></asp:Label></div>
            <div style="float: left;">
                <asp:Label ID="lblCateg" runat="server" Width="100px" CssClass="label" Text="Categoria:"></asp:Label>                    
                &nbsp;<asp:Label ID="lblCategoria" runat="server" Font-Bold="False"></asp:Label></div>
        </div> 
        <br />
        <div id="carrinho_pedidos">
            <h4>
                <asp:Label ID="lblTituloResumo" runat="server" Text="Resumo do Pedido" CssClass="title"></asp:Label></h4>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    
                <ContentTemplate>
                    <table id="carrinho" width="100%">
                        <tbody>
                            <tr>
                                <td colspan="4">
                                    <asp:Label ID="lblResPed" runat="server" Text="Pedido Nº"></asp:Label>
                                    &nbsp;<asp:Label ID="lblNrPedido" runat="server" Font-Bold="False"></asp:Label>
                                </td>
                                <td rowspan="4" style="vertical-align:middle;" Align="left">&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td id="titQtdItens" runat="server" style="width: 120px">
                                    <asp:Label ID="lblResItens" runat="server" Text="Itens"></asp:Label>
                                </td>
                                <td id="titVlrParc" runat="server" style="width: 120px; height: 16px;">
                                    <asp:Label ID="lblResVlr" runat="server" Text="Valor"></asp:Label>
                                </td>
                                <td id="titVlrDesc" runat="server" style="width: 120px">
                                    <asp:Label ID="lblResDesc" runat="server" Text="Descontos"></asp:Label>
                                </td>
                                <td id="titVlrTot" runat="server" style="width: 120px;">
                                    <asp:Label ID="lblResVlrTotal" runat="server" Text="Vlr do Pedido" Font-Bold="True"
                                        Font-Italic="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>                                    
                                <td id="resQtdItens" runat="server" style="width: 133px" align="center">
                                    <asp:Label ID="vlItens" runat="server" Font-Bold="False">0</asp:Label>
                                </td>
                                <td id="resVlrParc" runat="server" style="width: 133px; height: 16px" align="center">
                                    <asp:Label ID="vlTotalAtiv" runat="server" Font-Bold="False" Font-Strikeout="False">0,00</asp:Label>
                                </td>
                                <td id="resVlrDesc" runat="server" style="width: 133px" align="center">
                                    <asp:Label ID="vlTotalDesc" runat="server" Font-Bold="False">0,00</asp:Label>
                                </td>
                                <td id="resVlrTot" runat="server" style="width: 133px;" align="center">
                                    <asp:Label ID="vlTotalPedido" runat="server" Font-Bold="False">0,00</asp:Label>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <%--<div style="width: 18px; height: 18px; left: 700px; position: absolute; top: 5px;">
        </div>--%>
        <br />
        <asp:Panel ID="p1" runat="server">
            <asp:Button ID="btnVerDadosRecibo" runat="server" CausesValidation="False" CssClass="botoes"
                Text="Dados do Recibo" TabIndex="1" PostBackUrl="~/frmDadosRecibo.aspx" />
            &nbsp;<asp:Button ID="btnVoltar" runat="server" CausesValidation="False"
                CssClass="botoes" Font-Bold="False" PostBackUrl="~/frmListarPedidos.aspx"
                TabIndex="26" Text="Voltar" />
            <br /><br />
            
            <div  id="itensDisponiveisGeral" class="clsitensDisponiveisGeral" runat="server" visible="true">
                <div id="TituloGrid2" class="clsTituloGrid" runat="server" visible="false">
                    <asp:Label ID="lblTituloGrid1" runat="server" CssClass="titulo2" Text="Itens do Pedido"></asp:Label>
                </div>
                <div id="divItensDisponiveis" class="clsDivItensDisponiveis">
                    <asp:GridView ID="grdAtvParticipante" runat="server" AutoGenerateColumns="False"
                       DataKeyNames="noTipoAtividade,noTitulo,dsTema,noLocal,dtIni,dtTermino,cdAtividade,noProfessor,cdTipoAtividade,vlAtividade,flAtivo,flUsado,dtMatricula,vlDesconto,vlMatricula,flInscricaoObrigatoria,flPodeChocarHorario,dsCaminhoImgWEB,vlQuantidade,flRequerQuantidade,nrLinha,vlQuantidadeMaxima,flPodeRepetirPedido,dsTurno"
                         OnRowDataBound="grdAtvParticipante_RowDataBound"
                        OnRowDeleting="grdAtvParticipante_RowDeleting" ShowHeader="False" Visible="true"
                      >
                        <RowStyle  />
                        <Columns>                                        
                            <asp:TemplateField>
                                <ItemTemplate>
                                                                                                    
                                    <div id="imgAtividade" class="clsImgAtividade">
                                        <asp:Image ID="imgAtivProf" runat="server" Height="100px" ImageUrl='<%# Eval("dsCaminhoImgWEB") %>'
                                            Width="96px" />
                                    </div>
                                    <div id="informAtividade" class="clsInformAtividade">
                                        <div id="divTipoAtvSel" class="clsDivTipAtvSel">
                                            <asp:Label ID="lblTpItem" runat="server" __designer:wfdid="w43" Text="Tipo:"></asp:Label>
                                            <asp:Label ID="lblTipo" runat="server" __designer:wfdid="w25" Font-Bold="False"
                                                Text='<%# Eval("noTipoAtividade") %>'></asp:Label>
                                            <asp:Label ID="lblCdTipoAtvSel" runat="server" __designer:wfdid="w25" Font-Bold="False" Visible="False"
                                                Text='<%# Eval("cdTipoAtividade") %>'></asp:Label>
                                            <asp:Label ID="lblCdAtvSel" runat="server" __designer:wfdid="w25" Font-Bold="False" Visible="False"
                                                Text='<%# Eval("cdAtividade") %>'></asp:Label>
                                        </div>
                                        <div id="divDescAtvSel" class="clsDivDescAtvSel">
                                            <asp:Label ID="lblAtividade" CssClass="lblDescAtividade" runat="server" Text='<%# Eval("noTitulo") %>'></asp:Label>
                                        </div>
                                        <div id="divProfAtvSel" class="clsDivProfAtvSel">
                                            <asp:Label ID="lblNoProfessor" runat="server" __designer:wfdid="w29" Font-Bold="False" Text='<%# Eval("noProfessor") %>'></asp:Label>
                                        </div>
                                        <div id="divLocalAtvSel" class="clsDivLocalAtvSel">
                                            <asp:Label ID="lblLocalItem" runat="server" __designer:wfdid="w48" Text="Local:"></asp:Label>
                                            <asp:Label ID="lblLocal" runat="server" __designer:wfdid="w29" Font-Bold="False"
                                                Text='<%# Eval("noLocal") %>'></asp:Label>
                                        </div>
                                        <div id="divPeriodoAtvSel" class="clsDivPeriodoAtvSel">
                                            <asp:Label ID="lblDeitem" runat="server" __designer:wfdid="w50" Text="De: "></asp:Label>
                                            &nbsp;<asp:Label ID="lblDtIni" runat="server" __designer:wfdid="w31" Font-Bold="False" Text='<%# Eval("dtIni") %>'></asp:Label>
                                            <asp:Label ID="lblAteItem" runat="server" __designer:wfdid="w52" Text=" a: "></asp:Label>
                                            &nbsp;<asp:Label ID="lblDtTermino" runat="server" __designer:wfdid="w33" Font-Bold="False" Text='<%# Eval("dtTermino") %>'></asp:Label>                                                                    
                                        <asp:CheckBox ID="chkRequerQTD" runat="server" Checked='<%# Eval("flRequerQuantidade") %>'
                                            Visible="False" />
                                        </div>
                                    </div>
                                    <div id="vlr_item_pedido" class="valor_item_pedido">
                                        <asp:Panel ID="pnlResumoVlrItens" runat="server">
                                                        
                                            <div id="vlrItem" class="clsVlrItem" runat="server">
                                                <asp:Label ID="lblValorItem" runat="server" Text="Valor"></asp:Label>
                                                <br />
                                                <asp:Label ID="lblVlAtividade" runat="server" Font-Bold="False"
                                                    Font-Strikeout="False" Text='<%# Eval("vlAtividade") %>' ></asp:Label>
                                            </div>                                                                                                                                
                                            <div id="vlrDescItem" class="clsVlrDescItem" runat="server">
                                                <asp:Label ID="lblDescItem" runat="server" Text="Desconto"></asp:Label>
                                                <br />
                                                (&nbsp;-&nbsp;)
                                                <asp:Label ID="lblVlDescontoReal" runat="server" Font-Bold="False" Text='<%# Eval("vlDesconto") %>' ></asp:Label>
                                            </div>
                                            <div id="vlrQtdItem" class="clsVlrQtdItem" runat="server">
                                                <asp:Label ID="lblQtdItem" runat="server" Text="Quantidade"></asp:Label>
                                                <br />
                                                (&nbsp;x&nbsp;)
                                                <asp:Label ID="lblVlQuantidade" runat="server"  Font-Strikeout="False" Text='<%# Eval("vlQuantidade") %>' Visible="True"></asp:Label>
                                            </div>
                                            <div id="vlrTotalItem" class="clsVlrTotalItem" runat="server">
                                                <asp:Label ID="lblVlrTotalItem" runat="server" Text="Vl Inscri" Font-Bold="False"></asp:Label>
                                                <br />
                                                (&nbsp;=&nbsp;)
                                                <asp:Label ID="lblVlTotInscri" runat="server" Font-Bold="False" Text='<%# Eval("vlMatricula") %>'></asp:Label>
                                            </div>
                                                            
                                        </asp:Panel>
                                    </div>                                                        
                                                
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="noTipoAtividade" HeaderText="Tipo" Visible="False" />
                            <asp:BoundField DataField="noTitulo" HeaderText="Atividade" Visible="False" />
                            <asp:BoundField DataField="dsTema" HeaderText="Tema" Visible="False" />
                            <asp:BoundField DataField="noLocal" HeaderText="Local" Visible="False" />
                            <asp:BoundField DataField="dtIni" HeaderText="Início" Visible="False" />
                            <asp:BoundField DataField="dtTermino" HeaderText="Término" Visible="False" />
                            <asp:BoundField DataField="cdAtividade" HeaderText="cdAtividade" Visible="False" />
                            <asp:BoundField DataField="noProfessor" HeaderText="noProfessor" Visible="False" />
                            <asp:BoundField DataField="cdTipoAtiviade" HeaderText="cdTipoAtiviade" Visible="False" />
                            <asp:BoundField DataField="vlAtividade" HeaderText="Valor" Visible="False" />
                            <asp:BoundField DataField="flAtivo" HeaderText="flAtivo" Visible="False" />
                            <asp:BoundField DataField="flUsado" HeaderText="flUsado" Visible="False" />
                            <asp:BoundField DataField="dtMatricula" HeaderText="dtMatricula" Visible="False" />
                            <asp:BoundField DataField="vlDesconto" HeaderText="Descto" Visible="False" />
                            <asp:BoundField DataField="vlMatricula" HeaderText="vlMatricula" Visible="False" />
                        </Columns>
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle Font-Bold="False" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle CssClass="grdlinhapar" />
                    </asp:GridView>
                </div>
            </div>
        </asp:Panel>
        <cc1:MessageBox ID="MessageBox1" runat="server" />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" Visible="False" />
    </div>
</asp:Content>
