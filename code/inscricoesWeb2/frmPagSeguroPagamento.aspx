<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmPagSeguroPagamento.aspx.cs" Inherits="frmPagSeguroPagamento" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div id="frmCad_auto">
        
        <asp:ToolkitScriptManager ID="TSManager1" runat="server" EnableScriptLocalization="true"
            CombineScripts="false">
        </asp:ToolkitScriptManager>
    
        <div id="pedidos_esq">
            <h1>
            <asp:Label ID="lblTituloPagina" runat="server" Text="Forma de Pagamento" CssClass="titulo"></asp:Label>
            <br /><br />
            
        </h1>
            <div>
                <asp:Label ID="lblID" runat="server" Text="ID:" Width="100px" CssClass="label"></asp:Label>
                &nbsp;<asp:Label ID="lblIdentificador" runat="server" Font-Bold="False"></asp:Label>
            </div>
            <div>
                <asp:Label ID="lblPart" runat="server" Text="Participante:" Width="100px" 
                    CssClass="label"></asp:Label>
                &nbsp;<asp:Label ID="lblNoParticipante" runat="server" Font-Bold="False"></asp:Label>
            </div>
            <div>
                <asp:Label ID="lblCateg" runat="server" Text="Categoria:" Width="100px" 
                    CssClass="label"></asp:Label>
                &nbsp;<asp:Label ID="lblCategoria" runat="server" Font-Bold="False"></asp:Label>
            </div>
        </div>
        <div id="carrinho_pedidos">
            <h4>
                <asp:Label ID="lblTituloResumo" runat="server" 
                    Text="Resumo do Pedido" CssClass="title"></asp:Label></h4>
            <table id="carrinho">
                <tbody>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblResPed" runat="server" Text="Pedido Nº"></asp:Label>
                            &nbsp;<asp:Label ID="lblNrPedido" runat="server" Font-Bold="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px">
                            <asp:Label ID="lblResItens" runat="server" Text="Itens"></asp:Label>
                        </td>
                        <td style="width: 133px" align="right">
                            <asp:Label ID="vlItens" runat="server" Font-Bold="False">0</asp:Label>
                        </td>
                        <td style="width: 57px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; height: 16px">
                            <asp:Label ID="lblResVlr" runat="server" Text="Valor"></asp:Label>
                        </td>
                        <td style="width: 133px; height: 16px" align="right">
                            <asp:Label ID="vlTotalAtiv" runat="server" Font-Bold="False" Font-Strikeout="False">0,00</asp:Label>
                        </td>
                        <td style="width: 57px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px">
                            <asp:Label ID="lblResDesc" runat="server" Text="Descontos"></asp:Label>
                        </td>
                        <td style="width: 133px" align="right">
                            <asp:Label ID="vlTotalDesc" runat="server" Font-Bold="False">0,00</asp:Label>
                        </td>
                        <td style="width: 57px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px">
                            <asp:Label ID="lblResVlrTotal" runat="server" Text="Vlr do Pedido" 
                                Font-Bold="True" Font-Italic="False"></asp:Label>
                        </td>
                        <td style="width: 133px; border-top-width:1px; border-top-style:solid; border-top-color:Gray;" align="right">
                            <asp:Label ID="vlTotalPedido" runat="server" Font-Bold="False">0,00</asp:Label>
                        </td>
                        <td style="width: 57px">
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        
        <br />

        <asp:Panel ID="p1" CssClass="margem_topo" runat="server">
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
            <center><div><asp:Image ID="Image1" runat="server" 
                    ImageUrl="~/img/logo_pagseguro_180x41.gif" /></div></center>
            <br />
            <br />
            <asp:Label ID="lblMsgParticipante" runat="server" CssClass="label" 
                Width="752px"></asp:Label>
            <br />
            <br />
            <br />
            &nbsp;&nbsp;
            <asp:Button ID="btnContinuarPagSeguro" runat="server" CssClass="botoes" 
                onclick="btnContinuar_Click" Text="Continuar" />
            <br />
            <br />
            <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
            <br />
            </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>

</asp:Content>

