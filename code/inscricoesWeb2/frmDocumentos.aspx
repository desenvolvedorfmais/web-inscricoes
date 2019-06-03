<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmDocumentos.aspx.cs" Inherits="frmDocumentos" title="Inscri&ccedil;&otilde;es Web" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="scripts/MSGAguarde.js" type="text/javascript"></script>
    
    <div id="frmCad_auto">
    <asp:ToolkitScriptManager ID="TSManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" CombineScripts="false">
    </asp:ToolkitScriptManager>
    <div id="carrinho_geral" style="background-color: White;">
            <div id="pedidos_esq">
                <h1>
                    <asp:Label ID="lblTituloPagina" runat="server" Text="Documentos" CssClass="titulo"></asp:Label><br />
                    <br /><br />
                </h1>
              
               
            </div>
            <div id="filtro">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <br />
                    <div id="filtro_pedidos">
                        <label class="title">
                            <asp:Label ID="lblFiltro" runat="server" Text="Documentos para instrução do processo de empenho"></asp:Label>
                        </label>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br /><br />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>            
                        <asp:DataList ID="DataList1" runat="server">
                            <ItemTemplate>
                                <br />
                                &nbsp;-&nbsp;
                                <asp:HyperLink ID="HyperLink11" runat="server" Target="_blank" NavigateUrl='<%# Eval("dsURLDoc") %>' Text='<%# Eval("dsDocumento") %>'></asp:HyperLink>
                                <br />
                            </ItemTemplate>
                        </asp:DataList>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        &nbsp;
                        <asp:HyperLink ID="HyperLink1" runat="server" 
                            NavigateUrl="~/imagensgeral/000305/Estatuto_do_Consad.pdf" Target="_blank" 
                            Visible="False"> 
                        - Estatuto do Consad</asp:HyperLink>
                        <br />
                        <br />
                        &nbsp;
                        <asp:HyperLink ID="HyperLink10" runat="server" 
                            NavigateUrl="~/imagensgeral/000305/Razao_Social.pdf" Target="_blank" 
                            Visible="False"> - 
                        Razão Social</asp:HyperLink>
                        <br />
                        <br />
                        &nbsp;
                        <asp:HyperLink ID="HyperLink2" runat="server" 
                            NavigateUrl="~/imagensgeral/000305/Ata_Eleicao_do_Presidente_Consad.pdf" 
                            Target="_blank" Visible="False"> - Ata Eleição do Presidente - Consad</asp:HyperLink>
                        <br />
                        <br />
                        &nbsp;
                        <asp:HyperLink ID="HyperLink3" runat="server" 
                            NavigateUrl="~/imagensgeral/000305/Certidao_Negativa_de_Debito_GDF.pdf" 
                            Target="_blank" Visible="False"> - Certidão Negativa de Débito - GDF</asp:HyperLink>
                        <br />
                        <br />
                        &nbsp;
                        <asp:HyperLink ID="HyperLink4" runat="server" 
                            NavigateUrl="~/imagensgeral/000305/Certidao_Negativa_de_Debito_Previdencia_Social.pdf" 
                            Target="_blank" Visible="False"> - Certidão Negativa de Débito - Previdência Social</asp:HyperLink>
                        <br />
                        <br />
                        &nbsp;
                        <asp:HyperLink ID="HyperLink5" runat="server" 
                            NavigateUrl="~/imagensgeral/000305/Certidao_Negativa_de_Debito_Tributos_Federais.pdf" 
                            Target="_blank" Visible="False"> - Certidão Negativa de Débito - Tributos Federais</asp:HyperLink>
                        <br />
                        <br />
                        &nbsp;
                        <asp:HyperLink ID="HyperLink6" runat="server" 
                            NavigateUrl="~/imagensgeral/000305/Certificado_de_Regularidade_do_FGTS.pdf" 
                            Target="_blank" Visible="False"> - Certificado de Regularidade do FGTS</asp:HyperLink>
                        <br />
                        <br />
                        &nbsp;
                        <asp:HyperLink ID="HyperLink7" runat="server" 
                            NavigateUrl="~/imagensgeral/000305/Comprovante_de_Inscricao_CNPJ.pdf" 
                            Target="_blank" Visible="False"> - Comprovante de Inscrição CNPJ</asp:HyperLink>
                        <br />
                        <br />
                        &nbsp;
                        <asp:HyperLink ID="HyperLink8" runat="server" 
                            NavigateUrl="~/imagensgeral/000305/Declaracao_de_nao_Contrar_Menor.pdf" 
                            Target="_blank" Visible="False"> - Declaração de não Contrar Menor</asp:HyperLink>
                        <br />
                        <br />
                        &nbsp;
                        <asp:HyperLink ID="HyperLink9" runat="server" 
                            NavigateUrl="~/imagensgeral/000305/Declaracao_Sem_Fins_Lucrativos.pdf" 
                            Target="_blank" Visible="False">- Declaração Sem Fins Lucrativos</asp:HyperLink>
                        <br />
                        <br />

                </ContentTemplate>
            </asp:UpdatePanel>
            </div>
        </div>
    </div>

</asp:Content>

