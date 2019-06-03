<%@ Page Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true"
    CodeFile="frm_formapagamento.aspx.cs" Inherits="frm_formapagamento" Title="Inscri&ccedil;&otilde;es Web" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
       <script src="scripts/MSGAguarde.js" type="text/javascript"></script>
    
    <div id="frmCad_auto">
        <h3>
            <asp:Label ID="lblTituloPagina" runat="server" Text="Formas de Pagamento" CssClass="titulo"></asp:Label>
            <br />
            <br />
        </h3>
        <asp:ToolkitScriptManager ID="TSManager1" runat="server" EnableScriptLocalization="true"
            CombineScripts="false">
        </asp:ToolkitScriptManager>
        
        
        <asp:UpdateProgress runat="server" id="UpdateProgress1" DisplayAfter="0"  >
            <ProgressTemplate>
                
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:ModalPopupExtender ID="modalExtender" runat="server" TargetControlID="UpdateProgress1"
            PopupControlID="pnlAguarde" DropShadow="true" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>
        <asp:Panel ID="pnlAguarde" runat="server" CssClass="modalExtender">
            <div class="loading" align="center">
                    <br />Aguarde!... Processando sua solicitação. <br />
                    <br />
                    <img src="imagensgeral/loader.gif" alt="" />
             </div>
        </asp:Panel>

        <div id="pedidos_esq">
            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                <ContentTemplate> 
                    <div style="float: left;">
                        <asp:Label ID="lblID" runat="server" Width="100px" CssClass="label" Text="Nr Cadastro:"></asp:Label>                    
                        &nbsp;<asp:Label ID="lblIdentificador" runat="server" Font-Bold="False"></asp:Label></div>
                    <div style="float: left;">
                        <asp:Label ID="lblPart" runat="server" Width="100px" CssClass="label" Text="Participante:"></asp:Label>                    
                        &nbsp;<asp:Label ID="lblNoParticipante" runat="server" Font-Bold="False"></asp:Label></div>
                    <div style="float: left;">
                        <asp:Label ID="lblCateg" runat="server" Width="100px" CssClass="label" Text="Categoria:"></asp:Label>                    
                        &nbsp;<asp:Label ID="lblCategoria" runat="server" Font-Bold="False"></asp:Label></div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div> 
        <br />
        <div id="carrinho_pedidos">
                <h4>
                    <asp:Label ID="lblTituloResumoPgto" runat="server" Text="Resumo do Pedido" CssClass="title"></asp:Label></h4>
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    
                    <ContentTemplate>
                        <table id="carrinho" width="100%">
                            <tbody>
                                <tr>
                                    <td colspan="4">
                                        <asp:Label ID="lblResPedPgto" runat="server" Text="Pedido Nº"></asp:Label>
                                        &nbsp;<asp:Label ID="lblNrPedido" runat="server" Font-Bold="False"></asp:Label>
                                    </td>
                                    <td rowspan="4" style="vertical-align:middle;" Align="left"> &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td id="titQtdItensPgto" runat="server" style="width: 120px">
                                        <asp:Label ID="lblResItensPgto" runat="server" Text="Itens"></asp:Label>
                                    </td>
                                    <td id="titVlrParcPgto" runat="server" style="width: 120px; height: 16px;">
                                        <asp:Label ID="lblResVlrPgto" runat="server" Text="Valor"></asp:Label>
                                    </td>
                                    <td id="titVlrDescPgto" runat="server" style="width: 120px">
                                        <asp:Label ID="lblResDescPgto" runat="server" Text="Descontos"></asp:Label>
                                    </td>
                                    <td id="titVlrTotPgto" runat="server" style="width: 120px;">
                                        <asp:Label ID="lblResVlrTotalPgto" runat="server" Font-Bold="False"
                                            Font-Italic="False"></asp:Label>
                                    </td>
                                </tr>
                                <tr>                                    
                                    <td id="resQtdItensPgto" runat="server" style="width: 133px" align="center">
                                        <asp:Label ID="vlItensPgto" runat="server" Font-Bold="False">0</asp:Label>
                                    </td>
                                    <td id="resVlrParcPgto" runat="server" style="width: 133px; height: 16px" align="center">
                                        <asp:Label ID="vlTotalAtivPgto" runat="server" Font-Bold="False" Font-Strikeout="False">0,00</asp:Label>
                                    </td>
                                    <td id="resVlrDescPgto" runat="server" style="width: 133px" align="center">
                                        <asp:Label ID="vlTotalDesc" runat="server" Font-Bold="False">0,00</asp:Label>
                                    </td>
                                    <td id="resVlrTotPgto" runat="server" style="width: 133px;" align="center">
                                        <asp:Label ID="vlTotalPedidoPgto" runat="server" Font-Bold="False">0,00</asp:Label>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        <div id="divCupomDesconto" runat="server" visible="false">
            <asp:Panel ID="Panel2" runat="server" DefaultButton="btnCupomDesconto"> 
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                        
                            <asp:Label ID="lblTituloCupom" runat="server" Text="Possui cupom de desconto?" CssClass="title"></asp:Label>
                        
                        <asp:TextBox ID="txtCupomDesconto" runat="server" MaxLength="16" CssClass="campoform_pequeno maiusculo"></asp:TextBox>
                                                        
                        &nbsp; <asp:Button ID="btnCupomDesconto" runat="server" CausesValidation="False" 
                                            CssClass="botoes" TabIndex="1" 
                                            Text="Aplicar" OnClick="btnCupomDesconto_Click" />
                        <asp:Button ID="btnAlterarCupomDesconto" runat="server" CausesValidation="False" CssClass="botoes" TabIndex="2" Text="Alterar Código" Visible="False" OnClick="btnAlterarCupomDesconto_Click" /><br />
                        <asp:Label ID="lblAvisoDescontoCalculado" runat="server" Text="(Valor do desconto já calculado)" CssClass="title" visible="False" ForeColor="Red"></asp:Label>
                                        
                        <asp:Label ID="lblMsgWCIT" runat="server" Text="<br />Código gerado pela equipe do WCIT Brasil 2016 destinado a descontos para grupos ou eventuais ações promocionais. Para mais informações, entre em contato pelo telefone +55 61 3327.1288 ou pelo email wcit2016@assespro.org.br.<br />" CssClass="title" visible="False" ForeColor="#666666" ></asp:Label>
                                        
                        <asp:Label ID="lblMsgCupom" runat="server" CssClass="lblMsg" ForeColor="Red" Visible="False"></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
                                                        
                
            </asp:Panel>
        </div>
        <br />
        <asp:Panel ID="pnlInfoPgto" runat="server">        
                <asp:Label ID="lblMsgPgto" runat="server" ForeColor="Red" CssClass="lblMsg" Visible="False"></asp:Label>
                <div id="tbFormaPgto" class="clsTbFormaPgto" runat="server">
                    <%--<div>                            
                        <h3><asp:Label ID="lblTituloFormaPgto" runat="server" Text="Selecione a forma de pagamento" CssClass="titulo"></asp:Label></h3>
                    </div>
                    <br />&nbsp;--%>
                    <div id="linhaFormaPgto">
                        <asp:Label ID="lblFormaPgto" runat="server" Text="Forma de Pagamento" CssClass="lblTitulocampo"></asp:Label>
                        <br />
                        <asp:DropDownList ID="TxtFormaPagamento" TabIndex="3" runat="server" 
                                CssClass="campoform_medio maiusculo" AutoPostBack="True" OnSelectedIndexChanged="TxtFormaPagamento_SelectedIndexChanged">
                                    
                            </asp:DropDownList>
                            <asp:CustomValidator ID="CustomValidatorPgto" runat="server" 
                                ControlToValidate="TxtFormaPagamento" Display="Dynamic" 
                                ErrorMessage="Selecione uma Forma de Pagamento" 
                                onservervalidate="CustomValidatorPgto_ServerValidate" SetFocusOnError="True"></asp:CustomValidator>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="pnlAviso" runat="server" Width="100%" Visible="False" BackColor="#FFFFC0">
                                    &nbsp;
                                    <asp:Label ID="lblAviso" runat="server" ForeColor="Red" Font-Bold="True" CssClass="lblaviso"></asp:Label>
                                </asp:Panel>
                            </ContentTemplate>
                            </asp:UpdatePanel>                            
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlCartao" runat="server" Visible="False">                                    
                                <div id="linhaCartoes" runat="server" class="clsLinhaCartoes"> 
                                    <br />                                           
                                    <asp:Panel ID="Panel1" runat="server">
                                        <div id="visa" class="divBandCard" runat="server" Visible="False"><asp:RadioButton ID="chkVisa" runat="server" GroupName="cartao" Visible="False" TabIndex="4" Text="&lt;img src='../img/crdVisa.png' alt='Visa'/&gt;&lt;br/&gt;&lt;br/&gt;" TextAlign="Left" /></div>                                        
                                        <div id="master" class="divBandCard" runat="server" Visible="False"><asp:RadioButton ID="chkMaster" runat="server" GroupName="cartao" Visible="False" TabIndex="5" Text="<img src='../img/crdMastercard.png' alt='Master Card'/&gt;&lt;br/&gt;&lt;br/&gt;" TextAlign="Left" /></div> 
                                        <div id="elo" class="divBandCard" runat="server" Visible="False"><asp:RadioButton ID="chkElo" runat="server" GroupName="cartao" Visible="False" TabIndex="6" Text="<img src='../img/crdCielo.png' alt='Elo'/&gt;&lt;br/&gt;&lt;br/&gt;" TextAlign="Left" /></div>                                       
                                        <div id="amex" class="divBandCard" runat="server" Visible="False"><asp:RadioButton ID="chkAmex" runat="server" GroupName="cartao" Visible="False" TabIndex="7" Text="<img src='../img/crdAmex.png' alt='American-Express'/&gt;&lt;br/&gt;&lt;br/&gt;" TextAlign="Left" /></div>                                         
                                        <div id="diners" class="divBandCard" runat="server" Visible="False"><asp:RadioButton ID="chkDiners" runat="server" GroupName="cartao" Visible="False" TabIndex="8" Text="<img src='../img/crdDiners.png' alt='Diners'/&gt;&lt;br/&gt;&lt;br/&gt;" TextAlign="Left" /></div> 
                                        <div id="hiper" class="divBandCard" runat="server" Visible="False"><asp:RadioButton ID="chkHipercard" runat="server" GroupName="cartao" Visible="False" TabIndex="9" Text="<img src='../img/crdHipercard.png' alt='Hipercard'/&gt;&lt;br/&gt;&lt;br/&gt;" TextAlign="Left" /></div> 
                                       
                                        <div id="jcb" class="divBandCard" runat="server" Visible="False"><asp:RadioButton ID="chkJCB" runat="server" GroupName="cartao" Visible="False" TabIndex="10" Text="<img src='../img/crdJCB.png' alt='JCB'/&gt;&lt;br/&gt;&lt;br/&gt;" TextAlign="Left" /></div> 
                                        <div id="aura" class="divBandCard" runat="server" Visible="False"><asp:RadioButton ID="chkAura" runat="server" GroupName="cartao" Visible="False" TabIndex="11" Text="<img src='../img/crdAura.png' alt='Aura'/&gt;&lt;br/&gt;&lt;br/&gt;" TextAlign="Left" /></div> 
                                        
                                        <div id="soro" class="divBandCard" runat="server" Visible="False"><asp:RadioButton ID="chkSoro" runat="server" GroupName="cartao" Visible="False" TabIndex="12" Text="<img src='../img/crdSorocred.png' alt='Sorocred'/&gt;&lt;br/&gt;&lt;br/&gt;" TextAlign="Left" /></div>                                         
                                        <div id="discover" class="divBandCard" runat="server" Visible="False"><asp:RadioButton ID="chkDiscover" runat="server" GroupName="cartao" TabIndex="13" Visible="False" Text="<img src='../img/crdDiscover.png' alt='Discover'/&gt;&lt;br/&gt;&lt;br/&gt;" TextAlign="Left" /></div> 
                                        <div id="cabal" class="divBandCard" runat="server" Visible="False"><asp:RadioButton ID="chkCabal" runat="server" GroupName="cartao" TabIndex="14"  Visible="False"  Text="&lt;img src='../img/crdCabal.png' alt='Cabal'/&gt;&lt;br/&gt;&lt;br/&gt;" TextAlign="Left" /></div> 
                                    </asp:Panel>                                            
                                </div>
                                <br />
                                <div id="linhaNomeTitular" runat="server" class="clsLinhaNomeTitular linha_campo">
                                    <asp:Label ID="lblNomeTitularCartao" runat="server" Text="Nome do Titular" CssClass="lblTitulocampo"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtNomeTitularCartao" runat="server" MaxLength="50" TabIndex="15" CssClass="campoform_grande maiusculo"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RFVCPF0" runat="server" ControlToValidate="txtNomeTitularCartao"
                                            Display="Dynamic" ErrorMessage="Campo obrigatório." SetFocusOnError="True"></asp:RequiredFieldValidator>                                            
                                </div>
                                <div id="linhaNrCartao" runat="server" class="clsLinhaNrCartao linha_campo">
                                    <asp:Label ID="lblNrCartao" runat="server" Text="Nr do Cartão" CssClass="lblTitulocampo"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtNrCartao" runat="server" MaxLength="19" CssClass="campoform_pequeno"
                                            onkeypress="return Mascarar(this, event, '9999999999999999999')" TabIndex="16"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RFVCPF1" runat="server" ControlToValidate="txtNrCartao"
                                            Display="Dynamic" ErrorMessage="Campo obrigatório." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="REVTxtnrcart" runat="server" ControlToValidate="txtNrCartao"
                                            Display="Dynamic" ErrorMessage="Valor inválido." ValidationExpression="[\d+]{1,19}" SetFocusOnError="True"></asp:RegularExpressionValidator>                                            
                                </div>
                                <div id="linhaMesAno" runat="server" class="clsLinhaMesAno linha_campo">
                                    <div id="campoMes" runat="server" class="clsCampoMes">
                                        <asp:Label ID="lblMesValidadeCartao" runat="server" Text="Mês de Validade" CssClass="lblTitulocampo"></asp:Label>
                                        <br />                                            
                                        <asp:DropDownList ID="txtMesValidadeCartao" runat="server" CssClass="campoform_mini" TabIndex="17">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>01</asp:ListItem>
                                            <asp:ListItem>02</asp:ListItem>
                                            <asp:ListItem>03</asp:ListItem>
                                            <asp:ListItem>04</asp:ListItem>
                                            <asp:ListItem>05</asp:ListItem>
                                            <asp:ListItem>06</asp:ListItem>
                                            <asp:ListItem>07</asp:ListItem>
                                            <asp:ListItem>08</asp:ListItem>
                                            <asp:ListItem>09</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                            <asp:ListItem>12</asp:ListItem>
                                        </asp:DropDownList>
                                            
                                        <asp:RequiredFieldValidator ID="RFVCPF2" runat="server" ControlToValidate="txtMesValidadeCartao"
                                            Display="Dynamic" ErrorMessage="Campo obrigatório." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                    <div id="campoAno" runat="server" class="clsCampoAno">
                                        <asp:Label ID="lblAnoValidadeCartao" runat="server" Text="Ano" CssClass="lblTitulocampo"></asp:Label>
                                        <br />
                                        <asp:DropDownList ID="txtAnoValidadeCartao" runat="server" CssClass="campoform_mini" TabIndex="18">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RFVCPF3" runat="server" ControlToValidate="txtAnoValidadeCartao"
                                            Display="Dynamic" ErrorMessage="Campo obrigatório." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div id="linhaCodSeguranca" runat="server" class="clsLinhaCodSeguranca linha_campo">
                                    <asp:Label ID="lblCodSegCartao" runat="server" Text="Cód de Segurança do Cartão" CssClass="lblTitulocampo"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtCodSegCartao" runat="server" MaxLength="4" CssClass="campoform_mini maiusculo" 
                                            TabIndex="19" onkeypress="return Mascarar(this, event, '9999')"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RFVCPF4" runat="server" ControlToValidate="txtCodSegCartao"
                                            Display="Dynamic" ErrorMessage="Campo obrigatório." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="REVTxtCodseg" runat="server" ControlToValidate="txtCodSegCartao"
                                            Display="Dynamic" ErrorMessage="Valor inválido." ValidationExpression="[\d+]{1,4}" SetFocusOnError="True"></asp:RegularExpressionValidator>
                                            
                                </div>                                    
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                           
                </div>
                <div id="linhaParcelas" runat="server" visible="false" class="linha_campo">
                    <asp:Label ID="lblqtdParcelas" runat="server" Text="Parcelas" CssClass="lblTitulocampo"></asp:Label>
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="txtQtdParcelas" TabIndex="20" runat="server" CssClass="campoform_mini"
                                AutoPostBack="True" OnSelectedIndexChanged="txtQtdParcelas_SelectedIndexChanged">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>                            
                </div>
                <div id="linhaVencimento" runat="server" visible="false" class=" linha_campo">
                    <asp:Label ID="lblDiaVcto" runat="server" Text="Dia Vencimento" Visible="False" CssClass="lblTitulocampo" ></asp:Label>
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="txtDiaVcto" TabIndex="21" runat="server" CssClass="campoform_mini" Visible="False" >
                                <asp:ListItem>01</asp:ListItem>
                                <asp:ListItem>02</asp:ListItem>
                                <asp:ListItem>03</asp:ListItem>
                                <asp:ListItem>04</asp:ListItem>
                                <asp:ListItem>05</asp:ListItem>
                                <asp:ListItem>06</asp:ListItem>
                                <asp:ListItem>07</asp:ListItem>
                                <asp:ListItem>08</asp:ListItem>
                                <asp:ListItem>09</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                                <asp:ListItem>13</asp:ListItem>
                                <asp:ListItem>14</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>16</asp:ListItem>
                                <asp:ListItem>17</asp:ListItem>
                                <asp:ListItem>18</asp:ListItem>
                                <asp:ListItem>19</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>21</asp:ListItem>
                                <asp:ListItem>22</asp:ListItem>
                                <asp:ListItem>23</asp:ListItem>
                                <asp:ListItem>24</asp:ListItem>
                                <asp:ListItem>25</asp:ListItem>
                                <asp:ListItem>26</asp:ListItem>
                                <asp:ListItem>27</asp:ListItem>
                                <asp:ListItem>28</asp:ListItem>
                                <asp:ListItem>29</asp:ListItem>
                                <asp:ListItem>30</asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>                            
                </div>                                  
        </asp:Panel>
        <%--<br />&nbsp;
        <asp:UpdatePanel ID="pnlRespFin" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlResponsavelFinanceiro" runat="server" visible="False">
                    <div id="ResponsavelFinanceiro" class="clsResponsavelFinanceiro" width="100%">
                        <div>
                            <h3>
                                <asp:Label ID="Label1" runat="server" Text="Departamento Responsável Financeiro" CssClass="title"></asp:Label>
                            </h3>
                        </div>
                        <div id="linhaNomeRespFin" runat="server" class="linha_campo">
                            <asp:Label ID="Label2" runat="server" Text="Nome*"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtNomeRespFin" runat="server" CssClass="campoform_grande maiusculo" MaxLength="100" 
                                    TabIndex="22" Width="400px" ></asp:TextBox>                                
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                    ControlToValidate="txtNomeRespFin" Display="Dynamic" Enabled="True" 
                                    ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>                
                        </div>
                        <div id="linhaEmailRespFin" runat="server" class="linha_campo">
                            <asp:Label ID="Label3" runat="server" Text="Email*"></asp:Label>
                            <br/>
                            <asp:TextBox ID="txtEmailRespFin" runat="server" CssClass="campoform_grande minusculo" MaxLength="100" 
                                    TabIndex="23" ></asp:TextBox>                                
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                    ControlToValidate="txtEmailRespFin" Display="Dynamic" 
                                    ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>                
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" ControlToValidate="txtEmailRespFin" ErrorMessage="E-mail inválido" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        </div>
                        <div id="linhaFoneRespFin" runat="server" class="linha_campo">
                            <asp:Label ID="Label4" runat="server" Text="Fone*"></asp:Label>
                            <br/>
                            <asp:TextBox ID="txtFoneRespFin" runat="server" CssClass="campoform_pequeno maiusculo" MaxLength="20" 
                                    onkeypress="return Mascarar(this, event, '(99) 999999999')" TabIndex="24" ></asp:TextBox>                                
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                    ControlToValidate="txtFoneRespFin" Display="Dynamic" 
                                    ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>                
                        </div>
                        <div id="linhaRamalRespFin" runat="server" class="linha_campo">
                            <asp:Label ID="Label7" runat="server" Text="Ramal"></asp:Label>
                            <br/>
                            <asp:TextBox ID="txtRamalRespFin" runat="server" CssClass="campoform_mini maiusculo" MaxLength="10" 
                                    TabIndex="25" ></asp:TextBox>                                
                                       
                        </div>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>--%>
        <%--<br />&nbsp;
        <asp:UpdatePanel ID="upd2" runat="server">
            <ContentTemplate>
            <asp:Panel ID="pnlDadosRecibo" runat="server">
                <div id="dadosRecibo" class="clsDadosRecibo" width="100%">
                    <div>
                        <h3>
                            <asp:Label ID="lblTituloDadosRecibo" runat="server" Text="Dados para recibo" CssClass="title"></asp:Label>
                        </h3>
                        <asp:Label ID="lblMsgDadosRecibo" runat="server" Text="(preencha com atenção, pois será o seu documento fiscal)" Visible="False"></asp:Label>
                
                    </div>
                    <div id="linhatipPessoa" runat="server" class="linha_campo">
                        <asp:Label ID="lblTipoPessoa" runat="server" Text="Tipo Pessoa*" Visible="True"></asp:Label>
                        <br/>
                        <asp:DropDownList ID="txtTipoPessoaRecibo" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="txtTipoPessoa_SelectedIndexChanged" TabIndex="26">
                                <asp:ListItem Selected="True" Value="PF">PESSOA FÍSICA</asp:ListItem>
                                <asp:ListItem Value="PJ">PESSOA JURÍDICA</asp:ListItem>
                        </asp:DropDownList>                
                    </div>
                    <div id="linhaCPF" runat="server" class="linha_campo">
                        <asp:Label ID="Label152" runat="server" Text="CPF*"></asp:Label>
                        <br/>
                        <asp:TextBox ID="txtCPFRecibo" runat="server" CssClass="campoform_pequeno" MaxLength="14" 
                                onkeypress="return CPFMascarar(this, event)" TabIndex="27" ></asp:TextBox>
                            &nbsp;<asp:Button ID="btnDadosParticipanteRecibo" runat="server" CausesValidation="False" 
                                CssClass="botoes" onclick="btnDadosParticipante_Click" TabIndex="28" 
                                Text="Buscar" />
                            <asp:RegularExpressionValidator ID="REVTxtCPF0" runat="server" 
                                ControlToValidate="txtCPFRecibo" Display="Dynamic" Enabled="false" 
                                ErrorMessage="Valor inválido." ValidationExpression="[-\.\d\/]{1,18}"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="txtCPFRecibo" Display="Dynamic" 
                                ErrorMessage="Campo obrigatório." Enabled="False" Visible="False"></asp:RequiredFieldValidator>                
                    </div>
                    <div id="linhaCNPJ" runat="server" visible="false" class="linha_campo">
                        <asp:Label ID="Label5" runat="server" Text="CNPJ*"></asp:Label>
                        <br/>
                        <asp:TextBox ID="txtCNPJRecibo" runat="server" CssClass="campoform_pequeno" MaxLength="18"
                                onkeypress="return CNPJMascarar(this, event)" TabIndex="29" ></asp:TextBox>&nbsp;<asp:Button ID="btnDadosInstituicaoRecibo" 
                                runat="server" CausesValidation="False" CssClass="botoes" 
                                onclick="btnDadosInstituicao_Click" Text="Buscar" />
                            <asp:RegularExpressionValidator
                                    ID="REVTxtCPF" runat="server" ControlToValidate="txtCNPJRecibo" Display="Dynamic"
                                    ErrorMessage="Valor inválido." ValidationExpression="[-\.\d\/]{1,18}" Enabled="false"></asp:RegularExpressionValidator><asp:RequiredFieldValidator
                                        ID="RFVCPF" runat="server" ControlToValidate="txtCNPJRecibo" Display="Dynamic"
                                        ErrorMessage="Campo obrigatório." Enabled="False" Visible="False"></asp:RequiredFieldValidator>
                            <asp:Label ID="lblMsg2Recibo" runat="server" ForeColor="Red"></asp:Label>                
                    </div>
                    <div id="linhaNome" runat="server" class="linha_campo">
                        <asp:Label ID="lblNome" runat="server" Text="Nome*"></asp:Label>
                        <br/>
                        <asp:TextBox ID="txtNomeRecibo" runat="server" CssClass="campoform_grande maiusculo" MaxLength="100" 
                                TabIndex="30" ></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                ControlToValidate="txtNomeRecibo" Display="Dynamic" 
                                ErrorMessage="Valor do campo inválido." 
                        
                                ValidationExpression="[áéíóúàâêôãõüÁÉÍÓÚÀÂÊÔÃÕÜÇçºª@&amp;\-\.\,\w\s\d\\\/]{1,100}"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="txtNomeRecibo" Display="Dynamic" 
                                ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>                
                    </div>
                    <div id="linhaIE" runat="server" visible="false" class="linha_campo">
                        <asp:Label ID="Label6" runat="server" Text="Inscr Estatual"></asp:Label>
                        <br/>
                        <asp:TextBox ID="txtIE" runat="server" CssClass="campoform_pequeno" MaxLength="50" 
                                TabIndex="31" ></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                ControlToValidate="txtIE" Display="Dynamic" 
                                ErrorMessage="Valor do campo inválido." 
                        
                                ValidationExpression="[áéíóúàâêôãõüÁÉÍÓÚÀÂÊÔÃÕÜÇçºª@&amp;\-\.\,\w\s\d\\\/]{1,100}"></asp:RegularExpressionValidator>                
                    </div>
                    <div id="linhaPais" runat="server" class="linha_campo">
                        <asp:Label ID="Label15" runat="server" Text="País*"></asp:Label>
                        <br />
                        <asp:DropDownList ID="txtPaisRecibo" runat="server" CssClass="campoform_medio maiusculo" 
                                TabIndex="32">
                            </asp:DropDownList>
                        <asp:RegularExpressionValidator
                                ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtPaisRecibo"
                                Display="Dynamic" ErrorMessage="Valor do campo inválido." ValidationExpression="[áéíóúàâêôãõüÁÉÍÓÚÀÂÊÔÃÕÜÇçºª@&\-\.\,\w\s\d]{1,100}"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtPaisRecibo"
                                    Display="Dynamic" ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>                
                    </div>
                    <div id="linhaCep" runat="server" class="linha_campo">                
                        <asp:Label ID="Label12" runat="server" Text="CEP*"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtCEPRecibo" runat="server" MaxLength="10" CssClass="campoform_pequeno maiusculo" 
                                 TabIndex="33"></asp:TextBox>&nbsp;
                            <asp:Button ID="btnCEP" runat="server" CausesValidation="False" 
                                CssClass="botoes" onclick="btnCEP_Click" Text="Verificar" TabIndex="34" />
                                &nbsp;<asp:RegularExpressionValidator
                                    ID="RegularExpressionValidator12" runat="server" ControlToValidate="txtCEPRecibo"
                                    Display="Dynamic" ErrorMessage="Valor do campo inválido." ValidationExpression="[\.\-\d]{1,10}"
                                    Enabled="false"></asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator12"
                                        runat="server" ControlToValidate="txtCEPRecibo" Display="Dynamic"
                                        ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                            <asp:Label ID="lblMsgCEP" runat="server" ForeColor="Red" 
                                Text="CEP não encontrado!" Visible="False"></asp:Label>                
                    </div>
                    <div id="linhaEndereco" runat="server" class="linha_campo">
                        <asp:Label ID="Label13" runat="server" Text="Endereço*"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtEnderecoRecibo" runat="server"  TabIndex="35" 
                                MaxLength="125" CssClass="campoform_grande maiusculo"></asp:TextBox><asp:RegularExpressionValidator
                                ID="RegularExpressionValidator11" runat="server" ControlToValidate="txtEnderecoRecibo"
                                Display="Dynamic" ErrorMessage="Valor do campo inválido." 
                                ValidationExpression="[áéíóúàâêôãõüÁÉÍÓÚÀÂÊÔÃÕÜÇçºª@&amp;\-\.\,\w\s\d\\\/]{1,100}"></asp:RegularExpressionValidator><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtEnderecoRecibo"
                                    Display="Dynamic" ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>                
                    </div>
                    <div id="linhaComplemento" runat="server" class="linha_campo">
                        <asp:Label ID="Label151" runat="server" Text="Complemento*"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtComplementoEnderecoRecibo" runat="server" CssClass="campoform_grande maiusculo" 
                                MaxLength="125" TabIndex="36"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator14" 
                                runat="server" ControlToValidate="txtComplementoEnderecoRecibo" 
                                Display="Dynamic" ErrorMessage="Valor do campo inválido." 
                        
                                ValidationExpression="[áéíóúàâêôãõüÁÉÍÓÚÀÂÊÔÃÕÜÇçºª@&amp;\-\.\,\w\s\d\\\/]{1,100}"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" 
                                ControlToValidate="txtComplementoEnderecoRecibo" Display="Dynamic" ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>                
                    </div>
                    <div id="linhaBairro" runat="server" class="linha_campo">                
                            <asp:Label ID="Label150" runat="server" Text="Bairro*"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtBairroRecibo" runat="server"  TabIndex="37" 
                                MaxLength="72" CssClass="campoform_medio maiusculo"></asp:TextBox><asp:RegularExpressionValidator
                                ID="RegularExpressionValidator13" runat="server" ControlToValidate="txtBairroRecibo"
                                Display="Dynamic" ErrorMessage="Valor do campo inválido." 
                        
                                ValidationExpression="[áéíóúàâêôãõüÁÉÍÓÚÀÂÊÔÃÕÜÇçºª@&amp;\-\.\,\w\s\d\\\/]{1,100}"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtBairroRecibo"
                                    Display="Dynamic" Enabled="False" ErrorMessage="Campo obrigatório." Visible="False"></asp:RequiredFieldValidator>                
                    </div>
                    <div id="linhaUF" runat="server" class="linha_campo">
                        <asp:Label ID="Label10" runat="server" Text="UF*"></asp:Label>
                        <br />
                        <asp:DropDownList ID="txtUFRecibo" runat="server" CssClass="campoform_mini maiusculo" 
                                TabIndex="38">
                            </asp:DropDownList>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtUFRecibo"
                                Display="Dynamic" ErrorMessage="Valor do campo inválido." ValidationExpression="[áéíóúàâêôãõüÁÉÍÓÚÀÂÊÔÃÕÜÇçºª@&\-\.\,\w\s\d]{1,100}"></asp:RegularExpressionValidator><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtUFRecibo" Display="Dynamic"
                                    Enabled="False" ErrorMessage="Campo obrigatório." Visible="False"></asp:RequiredFieldValidator>                
                    </div>
                    <div id="linhaCidade" runat="server" class="linha_campo">
                        <asp:Label ID="Label11" runat="server" Text="Cidade*"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtCidadeRecibo" runat="server" MaxLength="60" TabIndex="39" 
                                    CssClass="campoform_medio maiusculo"></asp:TextBox><asp:RegularExpressionValidator
                                ID="RegularExpressionValidator10" runat="server" ControlToValidate="txtCidadeRecibo"
                                Display="Dynamic" ErrorMessage="Valor do campo inválido." ValidationExpression="[áéíóúàâêôãõüÁÉÍÓÚÀÂÊÔÃÕÜÇçºª@&\-\.\,\w\s\d]{1,100}"></asp:RegularExpressionValidator><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtCidadeRecibo"
                                    Display="Dynamic" Enabled="False" ErrorMessage="Campo obrigatório." Visible="False"></asp:RequiredFieldValidator>                
                    </div>
                    <div id="linhaEmailRespFin2" runat="server" class="linha_campo">
                            <asp:Label ID="Label8" runat="server" Text="Email*"></asp:Label>
                            <br/>
                            <asp:TextBox ID="txtEmailRespFin2" runat="server" CssClass="campoform_grande minusculo" MaxLength="100" 
                                    TabIndex="40" ></asp:TextBox>                                
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                    ControlToValidate="txtEmailRespFin2" Display="Dynamic" 
                                    ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>                
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtEmailRespFin2" ErrorMessage="E-mail inválido" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        </div>
                    <div id="linhaFoneRespFin2" runat="server" class="linha_campo">
                        <asp:Label ID="Label9" runat="server" Text="Fone*"></asp:Label>
                        <br/>
                        <asp:TextBox ID="txtFoneRespFin2" runat="server" CssClass="campoform_pequeno maiusculo" MaxLength="20" 
                                 TabIndex="41" ></asp:TextBox>                                
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                                ControlToValidate="txtFoneRespFin2" Display="Dynamic" 
                                ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>                
                    </div>
                    <div id="linhaRamalRespFin2" runat="server" class="linha_campo">
                        <asp:Label ID="Label14" runat="server" Text="Ramal"></asp:Label>
                        <br/>
                        <asp:TextBox ID="txtRamalRespFin2" runat="server" CssClass="campoform_mini maiusculo" MaxLength="10" 
                                TabIndex="42" ></asp:TextBox>                                
                                       
                    </div>
                    
                </div>

                
            </asp:Panel>--%>
        <br />&nbsp;
        <div id="btns" runat="server">
            <asp:Button ID="btnConfirmarPgto" CssClass="botoesDestaque" runat="server" Text="Confirmar" OnClick="btnConfirmar_Click"
                        TabIndex="43" />                
        </div>
        
            </ContentTemplate>
        </asp:UpdatePanel>
        &nbsp; &nbsp;&nbsp;
        <br />
        <span style="color: #ff0000"></span>
        
        <br />
        <br />
    </div>
    <div>
        &nbsp;
    </div>
     

    <script type="text/javascript">
        jQuery(document).ready(function (e) {
            try {
                jQuery("#ctl00_ContentPlaceHolder1_TxtFormaPagamento").msDropdown({ visibleRows: 8 });
            } catch (e) {
                alert(e.message);
            }
        });
    </script>

     <script type="text/javascript">
         Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(showPopup);
         Sys.WebForms.PageRequestManager.getInstance().add_endRequest(hidePopup);

         function showPopup(sender, args) {
             var ModalControl = '<%= modalExtender.ClientID %>';
                $find(ModalControl).show();
            }

            function hidePopup(sender, args) {
                var ModalControl = '<%= modalExtender.ClientID %>';
                $find(ModalControl).hide();
            }
    </script>
    
</asp:Content>
