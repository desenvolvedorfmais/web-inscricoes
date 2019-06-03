<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmListarPedidos.aspx.cs" Inherits="frmListarPedidos" Title="Inscri&ccedil;&otilde;es Web" %>

<%--<%@ Register Assembly="ControlMessageBox" Namespace="ControlMessageBox" TagPrefix="cc1" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="false"
        EnableScriptLocalization="true">
    </asp:ToolkitScriptManager>
    <div id="frmCad_auto">
        <h3>
            <asp:Label ID="lblTituloPagina" runat="server" Text="Meus pedidos" CssClass="titulo"></asp:Label><br />
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
        
            <br /><br />&nbsp;
        </div>
        
        <div id="corpo_meuspedidos">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        GridLines="None" OnRowDataBound="grd_RowDataBound" Width="100%" CellSpacing="2">
                        <RowStyle CssClass="grdlinhaimpar" />
                        <Columns>
                            <asp:BoundField DataField="cdPedido" HeaderText="Pedido">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="75px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tpPedido" HeaderText="Tipo" Visible="False">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="dtPedido" HeaderText="Dt Pedido" DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle HorizontalAlign="Left" CssClass="td_meuspedidos" />
                                <ItemStyle HorizontalAlign="Center" Width="70px" CssClass="td_meuspedidos" />
                            </asp:BoundField>
                            <asp:BoundField DataField="dtVencimentoPedido" HeaderText="Dt Venc" 
                                DataFormatString="{0:dd/MM/yyyy}" Visible="False">
                                <HeaderStyle HorizontalAlign="Left" CssClass="td_meuspedidos" />
                                <ItemStyle HorizontalAlign="Center" Width="80px" CssClass="td_meuspedidos" />
                            </asp:BoundField>
                            <asp:BoundField DataField="vlTotalPedido" HeaderText="Vlr Total" SortExpression="vlTotalPedido">
                                <ItemStyle HorizontalAlign="Right" Width="68px" CssClass="td_meuspedidos" />
                                <HeaderStyle CssClass="td_meuspedidos" />
                            </asp:BoundField>
                            <asp:BoundField DataField="qtdParcelas" HeaderText="Parc" SortExpression="qtdParcelas">
                                <HeaderStyle HorizontalAlign="Center" CssClass="td_meuspedidos" />
                                <ItemStyle HorizontalAlign="Center" Width="55px" CssClass="td_meuspedidos" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tpPagamento" HeaderText="Forma Pagamento" SortExpression="tpPagamento">
                                <HeaderStyle HorizontalAlign="Left" CssClass="td_meuspedidos" />
                                <ItemStyle HorizontalAlign="Left" CssClass="td_meuspedidos" />
                            </asp:BoundField>
                            <asp:BoundField DataField="flPago" HeaderText="Pago">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15px" CssClass="td_meuspedidos" />
                                <HeaderStyle CssClass="td_meuspedidos" />
                            </asp:BoundField>
                            <asp:BoundField DataField="flAtivo" HeaderText="Ativo">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15px" CssClass="td_meuspedidos" />
                                <HeaderStyle CssClass="td_meuspedidos" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnBoleto" runat="server" ImageUrl="~/img/vazia18x18.png" ToolTip="Visualisar Boleto(s)" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="18px" CssClass="td_meuspedidos" />
                                <HeaderStyle CssClass="td_meuspedidos" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnVerPedido" runat="server" ImageUrl="~/img/vazia18x18.png"
                                        ToolTip="Visualizar Pedido" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="18px" CssClass="td_meuspedidos" />
                                <HeaderStyle CssClass="td_meuspedidos" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEditarPedido" runat="server" ImageUrl="~/img/vazia18x18.png"
                                        ToolTip="Alterar Pedido" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="18px" CssClass="td_meuspedidos" />
                                <HeaderStyle CssClass="td_meuspedidos" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnCancelarPedido" runat="server" ImageUrl="~/img/vazia18x18.png"
                                        OnClick="btnCancelarPedido_Click" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="18px" CssClass="td_meuspedidos" />
                                <HeaderStyle CssClass="td_meuspedidos" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle Font-Bold="True" ForeColor="White" CssClass="grdHeader" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle CssClass="grdlinhapar" />
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div id="corpo_meuspedidos2">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grd2" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        GridLines="None" Width="100%" CellSpacing="2" ShowHeader="False" OnRowDataBound="grd2_RowDataBound">
                        <RowStyle CssClass="grdlinhaimpar" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div>
                                        <asp:Label ID="labelCodPedido" runat="server" Text="Pedido:"></asp:Label>
                                        <asp:Label ID="lblCodPedido" runat="server" Font-Bold="True" Text='<%# Eval("cdPedido") %>'></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="labelDtPedido" runat="server" Text="Data:"></asp:Label>
                                        <asp:Label ID="lblDtPedido" runat="server" Font-Bold="True" Text='<%# Eval("dtPedido") %>'></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="labelVlrPedido" runat="server" Text="Valor:"></asp:Label>
                                        <asp:Label ID="lblVlrPedido" runat="server" Font-Bold="True" Text='<%# Eval("vlTotalPedido") %>'></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="labelQtdParcela" runat="server" Text="Parcelas:"></asp:Label>
                                        <asp:Label ID="lblQtdParcela" runat="server" Font-Bold="True" Text='<%# Eval("qtdParcelas") %>'></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="labelFormaPgto" runat="server" Text="Forma Pagamento:"></asp:Label>
                                        <asp:Label ID="lblFormaPgto" runat="server" Font-Bold="True" Text='<%# Eval("tpPagamento") %>'></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="labelPago" runat="server" Text="Pago:"></asp:Label>
                                        <asp:Label ID="lblPago" runat="server" Font-Bold="True" Text='<%# Eval("flPago") %>'></asp:Label>
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="labelAtivo" runat="server" Text="Ativo:"></asp:Label>
                                        <asp:Label ID="lblAtivo" runat="server" Font-Bold="True" Text='<%# Eval("flAtivo") %>'></asp:Label>                                        
                                    </div>
                                    <br /> &nbsp;
                                    <div id="BotoesPedido" class="clsBotoesPedido" runat="server">
                                        <div id="boletoImg" class="clsBoletoImg" runat="server" visible="false">
                                            <asp:ImageButton ID="btnBoleto2" runat="server" ImageUrl="~/img/barcode18x18.png" ToolTip="Boleto(s)" />                                            
                                            <%--<asp:Label ID="lblLegBol2" runat="server" Text="Boleto(s)"></asp:Label>--%>
                                        </div>
                                        <div id="VerPedidoImg" class="clsVerPedidoImg" runat="server">
                                            <asp:ImageButton ID="btnVerPedido2" runat="server" ImageUrl="~/img/visualizar_pedido18x18.png" ToolTip="Itens" />
                                            <%--<asp:Label ID="lblLegVisualizar2" runat="server" Text="Itens"></asp:Label>--%>
                                        </div>
                                        <div id="AlterarPedidoImg" class="clsAlterarPedidoImg" runat="server" visible="false">
                                            <asp:ImageButton ID="btnEditarPedido" runat="server" ImageUrl="~/img/editar_18x18.png" ToolTip="Alterar" />                                            
                                            <%--<asp:Label ID="lblEditarPedido2" runat="server" Text="Alterar"></asp:Label>--%>
                                        </div>
                                        <div id="ImprimirReciboImg" class="clsImprimirReciboImg" runat="server" visible="false">
                                            <asp:ImageButton ID="btnImprimirRecibo" runat="server" ImageUrl="~/img/print 18x18.png" ToolTip="Imprimir Boleto"  />
                                            <%--<asp:Label ID="lblLegImprimir2" runat="server" Text="Recibo"></asp:Label>--%>
                                        </div>
                                        <div id="CancelarPedidoImg" class="clsCancelarPedidoImg" runat="server" visible="false">
                                            <asp:ImageButton ID="btnCancelarPedido2" runat="server" ImageUrl="~/img/delete 18x18.png" OnClick="btnCancelarPedido_Click" />                                            
                                            <%--<asp:Label ID="lblLegCancel" runat="server" Text="Cancelar"></asp:Label>--%>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="cdPedido" HeaderText="Pedido" Visible="False">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="75px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tpPedido" HeaderText="Tipo" Visible="False">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="dtPedido" HeaderText="Dt Pedido" DataFormatString="{0:dd/MM/yyyy}" Visible="False">
                                <HeaderStyle HorizontalAlign="Left" CssClass="td_meuspedidos" />
                                <ItemStyle HorizontalAlign="Center" Width="70px" CssClass="td_meuspedidos" />
                            </asp:BoundField>
                            <asp:BoundField DataField="dtVencimentoPedido" HeaderText="Dt Venc" 
                                DataFormatString="{0:dd/MM/yyyy}" Visible="False">
                                <HeaderStyle HorizontalAlign="Left" CssClass="td_meuspedidos" />
                                <ItemStyle HorizontalAlign="Center" Width="80px" CssClass="td_meuspedidos" />
                            </asp:BoundField>
                            <asp:BoundField DataField="vlTotalPedido" HeaderText="Vlr Total" SortExpression="vlTotalPedido" Visible="False">
                                <ItemStyle HorizontalAlign="Right" Width="68px" CssClass="td_meuspedidos" />
                                <HeaderStyle CssClass="td_meuspedidos" />
                            </asp:BoundField>
                            <asp:BoundField DataField="qtdParcelas" HeaderText="Parc" SortExpression="qtdParcelas" Visible="False">
                                <HeaderStyle HorizontalAlign="Center" CssClass="td_meuspedidos" />
                                <ItemStyle HorizontalAlign="Center" Width="55px" CssClass="td_meuspedidos" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tpPagamento" HeaderText="Forma Pagamento" SortExpression="tpPagamento" Visible="False">
                                <HeaderStyle HorizontalAlign="Left" CssClass="td_meuspedidos" />
                                <ItemStyle HorizontalAlign="Left" CssClass="td_meuspedidos" />
                            </asp:BoundField>
                            <asp:BoundField DataField="flPago" HeaderText="Pago" Visible="False">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15px" CssClass="td_meuspedidos" />
                                <HeaderStyle CssClass="td_meuspedidos" />
                            </asp:BoundField>
                            <asp:BoundField DataField="flAtivo" HeaderText="Ativo" Visible="False">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15px" CssClass="td_meuspedidos" />
                                <HeaderStyle CssClass="td_meuspedidos" />
                            </asp:BoundField>
                            
                        </Columns>
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle Font-Bold="True" ForeColor="White" CssClass="grdHeader" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle CssClass="grdlinhapar" />
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <br />
        <div id="legenda">
                <asp:Label ID="lblLegenda" runat="server" Text="Legenda:" class="title"></asp:Label>            
                <br />
                <asp:Image ID="imgLegBoleto" runat="server" ImageUrl="~/img/barcode18x18.png" />
                <asp:Label ID="lblLegBol" runat="server" Text="Impressão de boleto"></asp:Label>
                &nbsp; &nbsp;<%--<br />--%>
                <asp:Image ID="imgLegPedido" runat="server" ImageUrl="~/img/visualizar_pedido18x18.png" />
                <asp:Label ID="lblLegVisualizar" runat="server" Text="Visualizar pedido"></asp:Label>
                &nbsp; &nbsp;<%--<br />--%>
                <asp:Image ID="imgEditarPedido" runat="server" ImageUrl="~/img/editar_18x18.png" />
                <asp:Label ID="lblEditarPedido" runat="server" Text="Alterar pedido"></asp:Label>
                &nbsp; &nbsp
                <%--<br />--%>
                <asp:Image ID="imgLegImprimirRec" runat="server" ImageUrl="~/img/print 18x18.png" />
                <asp:Label ID="lblLegImprimir" runat="server" Text="Imprimir recibo"></asp:Label>
                <%--<br />--%>
                &nbsp; &nbsp
                <asp:Image ID="imgLegCancelarPed" runat="server" ImageUrl="~/img/delete 18x18.png" />
                <asp:Label ID="lblLegCancel" runat="server" Text="Cancelar pedido"></asp:Label>
                &nbsp;
            </div>
    </div>
</asp:Content>
