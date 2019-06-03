<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmCONSADOutrasAtividades.aspx.cs" Inherits="frmCONSADOutrasAtividades" title="Inscri&ccedil;&otilde;es Web" %>

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
                    <asp:Label ID="lblTituloPagina" runat="server" Text="Mesas-Redondas" CssClass="titulo"></asp:Label><br />
                    <br />
                </h1>
                <div>
                    <asp:Label ID="lblID" runat="server" Width="100px" CssClass="label" Text="ID:"></asp:Label>&nbsp;<asp:Label
                        ID="lblIdentificador" runat="server" Font-Bold="True" ForeColor="Navy"> </asp:Label></div>
                <div>
                    <asp:Label ID="lblPart" runat="server" Width="100px" CssClass="label" Text="Participante:"></asp:Label>
                    &nbsp;<asp:Label ID="lblNoParticipante" runat="server" Font-Bold="True" ForeColor="Navy"> </asp:Label></div>
                <div>
                    <asp:Label ID="lblCateg" runat="server" Width="100px" CssClass="label" Text="Categoria:"></asp:Label>
                    &nbsp;<asp:Label ID="lblCategoria" runat="server" Font-Bold="True" ForeColor="Navy"> </asp:Label></div>
            </div>
            <div id="carrinho_pedidos">
                <h4>
                    <asp:Label ID="lblTituloResumo" runat="server" Text="Resumo do Pedido" CssClass="title"></asp:Label></h4>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    
                    <ContentTemplate>
                        <table id="carrinho">
                            <tbody>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblResPed" runat="server" Text="Pedido Nº"></asp:Label>
                                        &nbsp;<asp:Label ID="lblNrPedido" runat="server" Font-Bold="True" ForeColor="Maroon"
                                            Font-Size="Small"></asp:Label>
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
                                        <asp:Label ID="vlItens" runat="server" Font-Bold="True" ForeColor="#404040">0</asp:Label>
                                    </td>
                                    <td style="width: 57px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 120px; height: 16px;">
                                        <asp:Label ID="lblResVlr" runat="server" Text="Valor"></asp:Label>
                                    </td>
                                    <td style="width: 133px; height: 16px" align="right">
                                        <asp:Label ID="vlTotalAtiv" runat="server" Font-Bold="True" ForeColor="#404040" Font-Strikeout="False">0,00</asp:Label>
                                    </td>
                                    <td style="width: 57px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 120px">
                                        <asp:Label ID="lblResDesc" runat="server" Text="Descontos"></asp:Label>
                                    </td>
                                    <td style="width: 133px" align="right">
                                        <asp:Label ID="vlTotalDesc" runat="server" Font-Bold="True" ForeColor="Red">0,00</asp:Label>
                                    </td>
                                    <td style="width: 57px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 120px;">
                                        <asp:Label ID="lblResVlrTotal" runat="server" Text="Vlr do Pedido" Font-Bold="True"
                                            Font-Italic="False"></asp:Label>
                                    </td>
                                    <td style="width: 133px; border-top-width: 1px; border-top-style: solid; border-top-color: Gray;"
                                        align="right">
                                        <asp:Label ID="vlTotalPedido" runat="server" Font-Bold="True" ForeColor="#0000C0"
                                            Font-Size="Small">0,00</asp:Label>
                                    </td>
                                    <td style="width: 57px">
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <asp:Button ID="btnVoltarParaItens" runat="server" CausesValidation="False" CssClass="botoes"
                            Font-Bold="False" OnClick="btnVoltarParaItens_Click" TabIndex="5" 
                            Text="Voltar" Width="177px" />
                        <asp:Button ID="btnAvancar" TabIndex="5" OnClick="btnAvancar_Click1" runat="server"
                            Width="96px" Text="Continuar" CausesValidation="False" BackColor="Red" BorderColor="Red"
                            Font-Bold="False" ForeColor="White" CssClass="botoes"></asp:Button>&nbsp;
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div id="filtro">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label><br />
                    <div id="filtro_pedidos">
                        <label class="title">
                            <asp:Label ID="lblFiltro" runat="server" Text="Selecione a Mesa-redonda com a participação dos Secretários Estaduais de Administração/Gestão que deseja participar:"></asp:Label>
                        </label>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br /><br />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>            
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                            <asp:ListItem Value="000305052">
                                <b><span style="font-size:12.0pt;font-family:verdana,sans-serif;mso-fareast-font-family:times new roman;mso-bidi-font-family:arial;color:#222222;mso-fareast-language:pt-br">&quot;Gest&#227;o P&#250;blica e Tecnologia&quot;<o:p /></span></b></p>
                                <p class="MsoNormal" style="margin-left: 47.25pt; text-indent: -18pt; background-position: initial initial; background-repeat: initial initial;"><span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:symbol;mso-fareast-font-family:symbol;mso-bidi-font-family:symbol;color:#1f497d;mso-themecolor:text2;mso-fareast-language:pt-br">&#183;<span style="font-size: 7pt; font-family: times new roman;">&nbsp; </span></span><span style="font-size:12.0pt;font-family:verdana,sans-serif;mso-fareast-font-family:times new roman;mso-bidi-font-family:arial;color:#1f497d;mso-themecolor:text2;mso-fareast-language:pt-br">Secret&#225;rios do Acre, Alagoas e Rio de Janeiro<o:p /></span><br /><br /></asp:ListItem>
                            <asp:ListItem Value="000305053">
                                <b><span style="font-size:12.0pt;font-family:verdana,sans-serif;mso-fareast-font-family:times new roman;mso-bidi-font-family:arial;color:#222222;mso-fareast-language:pt-br">&quot;Participa&#231;&#227;o popular e Gest&#227;o Publica&quot;<o:p /></span></b></p>
                                <p class="MsoNormal" style="margin-left: 47.25pt; text-indent: -18pt; background-position: initial initial; background-repeat: initial initial;"><span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:symbol;mso-fareast-font-family:symbol;mso-bidi-font-family:symbol;color:#222222;mso-fareast-language:pt-br">&#183;<span style="font-size: 7pt; font-family: times new roman;">&nbsp; </span></span><span style="font-size:12.0pt;font-family:verdana,sans-serif;mso-fareast-font-family:times new roman;mso-bidi-font-family:arial;color:#1f497d;mso-themecolor:text2;mso-fareast-language:pt-br">Secret&#225;rios do Distrito Federal, Par&#225; e Para&#237;ba</span><span style="font-size:12.0pt;font-family:verdana,sans-serif;mso-fareast-font-family:times new roman;mso-bidi-font-family:arial;color:#222222;mso-fareast-language:pt-br"><o:p /></span><br /><br />
                            </asp:ListItem>
                            <asp:ListItem Value="000305054">
                                <b><span style="font-size:12.0pt;font-family:verdana,sans-serif;mso-fareast-font-family:times new roman;mso-bidi-font-family:arial;mso-fareast-language:pt-br">&quot;Leide greve e negocia&#231;&#227;o coletiva&quot;<o:p /></span></b></p>
                                <p class="MsoNormal" style="margin-left: 47.25pt; text-indent: -18pt; background-position: initial initial; background-repeat: initial initial;"><span style="font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:symbol;mso-fareast-font-family:symbol;mso-bidi-font-family:symbol;color:#1f497d;mso-themecolor:text2;mso-fareast-language:pt-br">&#183;<span style="font-size: 7pt; font-family: times new roman;">&nbsp; </span></span><span style="font-size:12.0pt;font-family:verdana,sans-serif;mso-fareast-font-family:times new roman;mso-bidi-font-family:arial;color:#1f497d;mso-themecolor:text2;mso-fareast-language:pt-br">Secret&#225;rios de Minas Gerais, Rio Grande do Sul eTocantins<o:p /></span></p>
                                <p class="MsoNormal" style="background-position: initial initial; background-repeat: initial initial;"><br /><br />
                            </asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="RadioButtonList1" ErrorMessage="RequiredFieldValidator">Selecione 
                        uma Mesa-redonda</asp:RequiredFieldValidator>
                        <br />

                </ContentTemplate>
            </asp:UpdatePanel>
            </div>
        </div>
    </div>

</asp:Content>

