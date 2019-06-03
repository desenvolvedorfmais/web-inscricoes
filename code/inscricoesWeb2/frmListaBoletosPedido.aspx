<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmListaBoletosPedido.aspx.cs" Inherits="frmListaBoletosPedido" title="Inscri&ccedil;&otilde;es Web" %>

<%@ Register Assembly="ControlMessageBox" Namespace="ControlMessageBox" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="frmCad_auto">
    <asp:ToolkitScriptManager ID="TSManager1" runat="server" EnableScriptLocalization="true"
            CombineScripts="false">
        </asp:ToolkitScriptManager>
    <br />
    
    <div id="pedidos_esq">
        <h3><asp:Label ID="lblTituloPagina" runat="server" Text="Boletos do Pedido" CssClass="titulo"></asp:Label><br />
            <br />
            </h3>
        <div style="float: left;">
            <asp:Label id="lblID" runat="server" Text="Nr Cadastro:" Width="100px" CssClass="label"></asp:Label>
            <asp:Label ID="lblIdentificador" runat="server" Font-Bold="False"></asp:Label>
        </div>
        <div style="float: left;">
            <asp:Label id="Label3" runat="server" Text="Participante" Width="100px" CssClass="label"></asp:Label>                        
            <asp:Label ID="lblNoParticipante" runat="server" Font-Bold="False"></asp:Label>
        </div>
        <div style="float: left;">
            <asp:Label id="Label5" runat="server" Text="Categoria" Width="100px" CssClass="label"></asp:Label>
            <asp:Label ID="lblCategoria" runat="server" Font-Bold="False"></asp:Label>
        </div>
    
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
                                    <td rowspan="4" style="vertical-align:middle;" Align="left"> 
                                        &nbsp;
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
                                        <asp:Label ID="lblResVlrTotal" runat="server" Text="Vlr do Pedido" Font-Bold="False"
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
       <br /> 
        <div id="corpo_boletos_pedidos" class="cls_corpo_boletos_pedidos">
        <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="4" GridLines="None" OnRowDataBound="grd_RowDataBound"
            Width="100%" CellSpacing="2" DataKeyNames="dsLinkBoletoExterno">
            <RowStyle CssClass="grdlinhaimpar" />
            <Columns>
                <asp:BoundField DataField="cdParcela" HeaderText="Parcela" SortExpression="cdParcela">
                    <HeaderStyle HorizontalAlign="Center" CssClass="td_meuspedidos" />
                    <ItemStyle HorizontalAlign="Center" Width="15px" CssClass="td_meuspedidos" />
                </asp:BoundField>
                <asp:BoundField DataField="cdBoleto" HeaderText="Nr Boleto" SortExpression="cdBoleto">
                    <ItemStyle Width="108px" CssClass="td_meuspedidos" />
                    <HeaderStyle CssClass="td_meuspedidos" />
                </asp:BoundField>
                <asp:BoundField DataField="dtVencimento" HeaderText="Vencimento" SortExpression="dtVencimento" DataFormatString="{0:d}">
                    <HeaderStyle HorizontalAlign="Left" CssClass="td_meuspedidos" />
                    <ItemStyle HorizontalAlign="Center" Width="90px" 
                    CssClass="td_meuspedidos" />
                </asp:BoundField>
                <asp:BoundField DataField="vlBoleto" HeaderText="Vl Boleto" SortExpression="vlBoleto">
                    <ItemStyle HorizontalAlign="Right" Width="70px" CssClass="td_meuspedidos" />
                    <HeaderStyle CssClass="td_meuspedidos" />
                </asp:BoundField>
                <asp:BoundField DataField="dtRecebimento" HeaderText="Recebimento" SortExpression="dtRecebimento" DataFormatString="{0:d}">
                    <HeaderStyle HorizontalAlign="Center" CssClass="td_meuspedidos" />
                    <ItemStyle HorizontalAlign="Center" Width="100px" CssClass="td_meuspedidos" />
                </asp:BoundField>
                <asp:BoundField DataField="dsDocRecebimento" HeaderText="Documento" SortExpression="dsDocRecebimento" >
                    <HeaderStyle CssClass="td_meuspedidos" />
                    <ItemStyle CssClass="td_meuspedidos" />
                </asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Image ID="imgImprimir" runat="server" ImageUrl="~/img/print 18x18.png" />
                    </ItemTemplate>
                    <ItemStyle Width="22px" CssClass="td_meuspedidos" />
                    <HeaderStyle CssClass="td_meuspedidos" />
                </asp:TemplateField>
                <asp:BoundField DataField="dsLinkBoletoExterno" HeaderText="dsLinkBoletoExterno" SortExpression="dsLinkBoletoExterno" Visible="False" />
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle Font-Bold="True" ForeColor="White" CssClass="grdHeader" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle CssClass="grdlinhapar" />
        </asp:GridView>
    </div>

        
        <div id="corpo_boletos_pedidos2" class="cls_corpo_boletos_pedidos2">
            <asp:GridView ID="grd2" runat="server" AutoGenerateColumns="False" CellPadding="4" GridLines="None" OnRowDataBound="grd2_RowDataBound"
            Width="100%" CellSpacing="2" ShowHeader="False">
            <RowStyle CssClass="grdlinhaimpar" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <%--<asp:Image ID="imgImprimir" runat="server" ImageUrl="~/img/print 18x18.png" />--%>
                        <div>
                            <asp:Label ID="labelcdParcela" runat="server" Text="Parcela:"></asp:Label>
                            <asp:Label ID="lblcdParcela" runat="server" Font-Bold="True" Text='<%# Eval("cdParcela") %>'></asp:Label>
                        </div>
                        <div>
                            <asp:Label ID="labelcdBoleto" runat="server" Text="Nr Boleto:"></asp:Label>
                            <asp:Label ID="lblcdBoleto" runat="server" Font-Bold="True" Text='<%# Eval("cdBoleto") %>'></asp:Label>
                        </div>
                        <div>
                            <asp:Label ID="labeldtVencimento" runat="server" Text="Vencimento:"></asp:Label>
                            <asp:Label ID="lbldtVencimento" runat="server" Font-Bold="True" Text='<%# Eval("dtVencimento") %>'></asp:Label>
                        </div>
                        <div> 
                            <asp:Label ID="labelvlBoleto" runat="server" Text="Vlr Boleto:"></asp:Label>
                            <asp:Label ID="lblvlBoleto" runat="server" Font-Bold="True" Text='<%# Eval("vlBoleto") %>'></asp:Label>
                        </div>
                        <div>
                            <asp:Label ID="labeldtRecebimento" runat="server" Text="Recebimento:"></asp:Label>
                            <asp:Label ID="lbldtRecebimento" runat="server" Font-Bold="True" Text='<%# Eval("dtRecebimento") %>'></asp:Label>
                        </div>
                        <div>
                            <asp:Label ID="labeldsDocRecebimento" runat="server" Text="Documento:"></asp:Label>
                            <asp:Label ID="lbldsDocRecebimento" runat="server" Font-Bold="True" Text='<%# Eval("dsDocRecebimento") %>'></asp:Label>                                  
                        </div>
                        <br /> &nbsp;
                        <div id="BotoesPedido" class="clsBotoesPedido" runat="server">
                            <div id="imgImprimirboleto" class="clsImprimirBoletoImg" runat="server" visible="true">
                                <%--<asp:ImageButton ID="btnBoleto2" runat="server" ImageUrl="~/img/print 18x18.png" ToolTip="Imprimr Boleto" />--%>  
                                <asp:Image ID="imgImprimir" runat="server" ImageUrl="~/img/print 18x18.png" />                                          
                                <asp:Label ID="lblLegBol2" runat="server" Text="Imprimir"></asp:Label>
                            </div>
                        </div>
                    </ItemTemplate>
                    <ItemStyle Width="22px" CssClass="td_meuspedidos" />
                    <HeaderStyle CssClass="td_meuspedidos" />
                </asp:TemplateField>
                <asp:BoundField DataField="cdParcela" HeaderText="Parcela" SortExpression="cdParcela" Visible="False">
                    <HeaderStyle HorizontalAlign="Center" CssClass="td_meuspedidos" />
                    <ItemStyle HorizontalAlign="Center" Width="15px" CssClass="td_meuspedidos" />
                </asp:BoundField>
                <asp:BoundField DataField="cdBoleto" HeaderText="Nr Boleto" SortExpression="cdBoleto" Visible="False">
                    <ItemStyle Width="108px" CssClass="td_meuspedidos" />
                    <HeaderStyle CssClass="td_meuspedidos" />
                </asp:BoundField>
                <asp:BoundField DataField="dtVencimento" HeaderText="Vencimento" SortExpression="dtVencimento" DataFormatString="{0:d}" Visible="False">
                    <HeaderStyle HorizontalAlign="Left" CssClass="td_meuspedidos" />
                    <ItemStyle HorizontalAlign="Center" Width="90px" 
                    CssClass="td_meuspedidos" />
                </asp:BoundField>
                <asp:BoundField DataField="vlBoleto" HeaderText="Vl Boleto" SortExpression="vlBoleto" Visible="False">
                    <ItemStyle HorizontalAlign="Right" Width="70px" CssClass="td_meuspedidos" />
                    <HeaderStyle CssClass="td_meuspedidos" />
                </asp:BoundField>
                <asp:BoundField DataField="dtRecebimento" HeaderText="Recebimento" SortExpression="dtRecebimento" DataFormatString="{0:d}" Visible="False">
                    <HeaderStyle HorizontalAlign="Center" CssClass="td_meuspedidos" />
                    <ItemStyle HorizontalAlign="Center" Width="100px" CssClass="td_meuspedidos" />
                </asp:BoundField>
                <asp:BoundField DataField="dsDocRecebimento" HeaderText="Documento" SortExpression="dsDocRecebimento" Visible="False" >
                    <HeaderStyle CssClass="td_meuspedidos" />
                    <ItemStyle CssClass="td_meuspedidos" />
                </asp:BoundField>
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle Font-Bold="True" ForeColor="White" CssClass="grdHeader" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle CssClass="grdlinhapar" />
        </asp:GridView>
        </div>
    </div>
</asp:Content>

