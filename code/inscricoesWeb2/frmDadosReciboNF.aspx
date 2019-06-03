<%@ Page Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true"
    CodeFile="frmDadosReciboNF.aspx.cs" Inherits="frmDadosReciboNF" Title="Inscri&ccedil;&otilde;es Web" 
    enableEventValidation="false"%>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
       <script src="scripts/MSGAguarde.js" type="text/javascript"></script>
    
    <div id="frmCad_auto">
        <h3>
            <asp:Label ID="lblTituloPagina" runat="server" Text="Detalhes do Pedido" CssClass="titulo"></asp:Label>
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
                                        <asp:Label ID="lblResVlrTotalPgto" runat="server" Text="Vlr do Pedido" Font-Bold="False"
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
        <br />
        
        <br />&nbsp;<asp:Label ID="lblMsg" runat="server" Font-Size="Medium" ForeColor="Red" CssClass="lblMsg" Visible="False"></asp:Label>
                &nbsp;<asp:UpdatePanel ID="upd2" runat="server">
            <ContentTemplate>
            <asp:Panel ID="pnlDadosRecibo" runat="server">
                <div id="dadosRecibo" class="clsDadosRecibo" width="100%">
                    <div>
                        <h3>
                            <asp:Label ID="lblTituloDadosRecibo" runat="server" Text="Dados para recibo" CssClass="title"></asp:Label>
                        </h3>
                        <br />
                        <asp:Label ID="lblMsgDadosRecibo" runat="server" Text="(preencha com atenção, pois será o seu documento fiscal)" Visible="False"></asp:Label>
                
                        </div>
                    <div id="linhatipPessoa" runat="server" class="linha_campo">
                        <asp:Label ID="lblTipoPessoa" runat="server" Text="Tipo Pessoa*" Visible="True" CssClass="lblTitulocampo"></asp:Label>
                        <br/>
                        <asp:DropDownList ID="txtTipoPessoaRecibo" runat="server" AutoPostBack="True" CssClass="campoform_pequeno"
                                onselectedindexchanged="txtTipoPessoa_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="PF">PESSOA FÍSICA</asp:ListItem>
                                <asp:ListItem Value="PJ">PESSOA JURÍDICA</asp:ListItem>
                        </asp:DropDownList>                
                    </div>
                    <div id="linhaCPF" runat="server" class="linha_campo">
                        <asp:Label ID="Label152" runat="server" Text="CPF*" CssClass="lblTitulocampo"></asp:Label>
                        <br/>
                        <asp:TextBox ID="txtCPFRecibo" runat="server" CssClass="campoform_pequeno" MaxLength="14" 
                                onkeypress="return CPFMascarar(this, event)" TabIndex="1" ></asp:TextBox>
                            &nbsp;<asp:Button ID="btnDadosParticipanteRecibo" runat="server" CausesValidation="False" 
                                CssClass="botoes" onclick="btnDadosParticipante_Click" TabIndex="2" 
                                Text="Buscar" Visible="False" />
                            <asp:RegularExpressionValidator ID="REVTxtCPF0" runat="server" 
                                ControlToValidate="txtCPFRecibo" Display="Dynamic" Enabled="false" 
                                ErrorMessage="Valor inválido." ValidationExpression="[-\.\d\/]{1,18}"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="txtCPFRecibo" Display="Dynamic" 
                                ErrorMessage="Campo obrigatório." SetFocusOnError="True"></asp:RequiredFieldValidator>                
                    </div>
                    <div id="linhaCNPJ" runat="server" visible="false" class="linha_campo">
                        <asp:Label ID="Label5" runat="server" Text="CNPJ*" CssClass="lblTitulocampo"></asp:Label>
                        <br/>
                        <asp:TextBox ID="txtCNPJRecibo" runat="server" CssClass="campoform_pequeno" MaxLength="18"
                                onkeypress="return CNPJMascarar(this, event)" TabIndex="3" ></asp:TextBox>&nbsp;<asp:Button ID="btnDadosInstituicaoRecibo" 
                                runat="server" CausesValidation="False" CssClass="botoes" 
                                onclick="btnDadosInstituicao_Click" Text="Buscar" TabIndex="4" />
                            <asp:RegularExpressionValidator
                                    ID="REVTxtCPF" runat="server" ControlToValidate="txtCNPJRecibo" Display="Dynamic"
                                    ErrorMessage="Valor inválido." ValidationExpression="[-\.\d\/]{1,18}" Enabled="false"></asp:RegularExpressionValidator><asp:RequiredFieldValidator
                                        ID="RFVCPF" runat="server" ControlToValidate="txtCNPJRecibo" Display="Dynamic"
                                        ErrorMessage="Campo obrigatório." SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:Label ID="lblMsg2Recibo" runat="server" ForeColor="Red"></asp:Label>                
                    </div>
                    <div id="linhaNome" runat="server" class="linha_campo">
                        <asp:Label ID="lblNome" runat="server" Text="Nome*" CssClass="lblTitulocampo"></asp:Label>
                        <br/>
                        <asp:TextBox ID="txtNomeRecibo" runat="server" CssClass="campoform_grande maiusculo" MaxLength="100" 
                                TabIndex="5" ></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                ControlToValidate="txtNomeRecibo" Display="Dynamic" 
                                ErrorMessage="Valor do campo inválido." 
                        
                                ValidationExpression="[áéíóúàâêôãõüÁÉÍÓÚÀÂÊÔÃÕÜÇçºª@&amp;\-\.\,\w\s\d\\\/]{1,100}"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="txtNomeRecibo" Display="Dynamic" 
                                ErrorMessage="Campo obrigatório." SetFocusOnError="True"></asp:RequiredFieldValidator>                
                    </div>
                    <div id="linhaIE" runat="server" visible="false" class="linha_campo">
                        <asp:Label ID="Label6" runat="server" Text="Inscr Estatual" CssClass="lblTitulocampo"></asp:Label>
                        <br/>
                        <asp:TextBox ID="txtIE" runat="server" CssClass="campoform_pequeno" MaxLength="50" 
                                TabIndex="6" ></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                ControlToValidate="txtIE" Display="Dynamic" 
                                ErrorMessage="Valor do campo inválido." 
                        
                                ValidationExpression="[áéíóúàâêôãõüÁÉÍÓÚÀÂÊÔÃÕÜÇçºª@&amp;\-\.\,\w\s\d\\\/]{1,100}" Enabled="False"></asp:RegularExpressionValidator>                
                    </div>
                    <div id="linhaPais" runat="server" class="linha_campo">
                        <asp:Label ID="Label1" runat="server" Text="País*" CssClass="lblTitulocampo"></asp:Label>
                        <br />
                        <asp:DropDownList ID="txtPaisRecibo" runat="server" CssClass="campoform_medio maiusculo" 
                                TabIndex="7">
                            </asp:DropDownList>
                        <asp:RegularExpressionValidator
                                ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtPaisRecibo"
                                Display="Dynamic" ErrorMessage="Valor do campo inválido." ValidationExpression="[áéíóúàâêôãõüÁÉÍÓÚÀÂÊÔÃÕÜÇçºª@&\-\.\,\w\s\d]{1,100}"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtPaisRecibo"
                                    Display="Dynamic" ErrorMessage="Campo obrigatório." SetFocusOnError="True"></asp:RequiredFieldValidator>                
                    </div>
                    <div id="linhaCep" runat="server" class="linha_campo">                
                        <asp:Label ID="Label12" runat="server" Text="CEP*" CssClass="lblTitulocampo"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtCEPRecibo" runat="server" MaxLength="10" CssClass="campoform_pequeno maiusculo" 
                                TabIndex="8"></asp:TextBox>&nbsp;
                            <asp:Button ID="btnCEP" runat="server" CausesValidation="False" 
                                CssClass="botoes" onclick="btnCEP_Click" Text="Verificar" TabIndex="9" />
                                &nbsp;<asp:RegularExpressionValidator
                                    ID="RegularExpressionValidator12" runat="server" ControlToValidate="txtCEPRecibo"
                                    Display="Dynamic" ErrorMessage="Valor do campo inválido." ValidationExpression="[\.\-\d]{1,10}"
                                    Enabled="false"></asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator12"
                                        runat="server" ControlToValidate="txtCEPRecibo" Display="Dynamic"
                                        ErrorMessage="Campo obrigatório." SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:Label ID="lblMsgCEP" runat="server" ForeColor="Red" 
                                Text="CEP não encontrado!" Visible="False"></asp:Label>                
                    </div>
                    <div id="linhaEndereco" runat="server" class="linha_campo">
                        <asp:Label ID="Label13" runat="server" Text="Endereço*" CssClass="lblTitulocampo"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtEnderecoRecibo" runat="server"  TabIndex="10" 
                                MaxLength="125" CssClass="campoform_grande maiusculo"></asp:TextBox><asp:RegularExpressionValidator
                                ID="RegularExpressionValidator11" runat="server" ControlToValidate="txtEnderecoRecibo"
                                Display="Dynamic" ErrorMessage="Valor do campo inválido." 
                                ValidationExpression="[áéíóúàâêôãõüÁÉÍÓÚÀÂÊÔÃÕÜÇçºª@&amp;\-\.\,\w\s\d\\\/]{1,100}"></asp:RegularExpressionValidator><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtEnderecoRecibo"
                                    Display="Dynamic" ErrorMessage="Campo obrigatório." SetFocusOnError="True"></asp:RequiredFieldValidator>                
                    </div>
                    <div id="linhaComplemento" runat="server" class="linha_campo">
                        <asp:Label ID="Label151" runat="server" Text="Complemento*" CssClass="lblTitulocampo"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtComplementoEnderecoRecibo" runat="server" CssClass="campoform_grande maiusculo" 
                                MaxLength="125" TabIndex="11"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator14" 
                                runat="server" ControlToValidate="txtComplementoEnderecoRecibo" 
                                Display="Dynamic" ErrorMessage="Valor do campo inválido." 
                        
                                ValidationExpression="[áéíóúàâêôãõüÁÉÍÓÚÀÂÊÔÃÕÜÇçºª@&amp;\-\.\,\w\s\d\\\/]{1,100}"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" 
                                ControlToValidate="txtComplementoEnderecoRecibo" Display="Dynamic" ErrorMessage="Campo obrigatório." SetFocusOnError="True"></asp:RequiredFieldValidator>                
                    </div>
                    <div id="linhaBairro" runat="server" class="linha_campo">                
                            <asp:Label ID="Label150" runat="server" Text="Bairro*" CssClass="lblTitulocampo"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtBairroRecibo" runat="server"  TabIndex="12" 
                                MaxLength="72" CssClass="campoform_medio maiusculo"></asp:TextBox><asp:RegularExpressionValidator
                                ID="RegularExpressionValidator13" runat="server" ControlToValidate="txtBairroRecibo"
                                Display="Dynamic" ErrorMessage="Valor do campo inválido." 
                        
                                ValidationExpression="[áéíóúàâêôãõüÁÉÍÓÚÀÂÊÔÃÕÜÇçºª@&amp;\-\.\,\w\s\d\\\/]{1,100}"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtBairroRecibo"
                                    Display="Dynamic" ErrorMessage="Campo obrigatório." SetFocusOnError="True"></asp:RequiredFieldValidator>                
                    </div>
                    <div id="linhaUF" runat="server" class="linha_campo">
                        <asp:Label ID="Label10" runat="server" Text="UF*" CssClass="lblTitulocampo"></asp:Label>
                        <br />
                        <asp:DropDownList ID="txtUFRecibo" runat="server" CssClass="campoform_mini maiusculo" 
                                TabIndex="13">
                            </asp:DropDownList>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtUFRecibo"
                                Display="Dynamic" ErrorMessage="Valor do campo inválido." ValidationExpression="[áéíóúàâêôãõüÁÉÍÓÚÀÂÊÔÃÕÜÇçºª@&\-\.\,\w\s\d]{1,100}"></asp:RegularExpressionValidator><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtUFRecibo" Display="Dynamic" ErrorMessage="Campo obrigatório." SetFocusOnError="True"></asp:RequiredFieldValidator>      
                        
                            <asp:CascadingDropDown runat="server" ID="cascaDD_txtUF" TargetControlID="txtUFRecibo" Category="dsUF" ServicePath = "CascataDropDown.asmx" ServiceMethod = "buscar_dsUF"/>          
                    </div>
                    <div id="linhaCidade" runat="server" class="linha_campo">
                        <asp:Label ID="Label11" runat="server" Text="Cidade*" CssClass="lblTitulocampo"></asp:Label>
                        <br />
                        <asp:DropDownList ID="txtCidadeRecibo" runat="server" CssClass="campoform_medio maiusculo" 
                                TabIndex="14">
                            </asp:DropDownList>
                        <asp:CascadingDropDown runat="server" ID="cascaDD_txtMunicipio" TargetControlID="txtCidadeRecibo" Category="dsUF" ServicePath = "CascataDropDown.asmx" ServiceMethod = "buscar_noCidade" ParentControlID = "txtUFRecibo"/>
                        <%--<asp:TextBox ID="txtCidadeRecibo" runat="server" MaxLength="60" TabIndex="11" 
                                    CssClass="campoform_medio maiusculo"></asp:TextBox>--%>
                        <asp:RegularExpressionValidator
                                ID="RegularExpressionValidator10" runat="server" ControlToValidate="txtCidadeRecibo"
                                Display="Dynamic" ErrorMessage="Valor do campo inválido." ValidationExpression="[áéíóúàâêôãõüÁÉÍÓÚÀÂÊÔÃÕÜÇçºª@&\-\.\,\w\s\d]{1,100}"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtCidadeRecibo"
                                    Display="Dynamic" ErrorMessage="Campo obrigatório." SetFocusOnError="True"></asp:RequiredFieldValidator>                
                    </div>
                    
                    <div id="linhaEmailRespFin" runat="server" class="linha_campo">
                            <asp:Label ID="Label3" runat="server" Text="Email*" CssClass="lblTitulocampo"></asp:Label>
                            <br/>
                            <asp:TextBox ID="txtEmailRespFin" runat="server" CssClass="campoform_grande minusculo" MaxLength="100" 
                                    TabIndex="15" ></asp:TextBox>                                
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                    ControlToValidate="txtEmailRespFin" Display="Dynamic" 
                                    ErrorMessage="Campo obrigatório." SetFocusOnError="True"></asp:RequiredFieldValidator>                
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" ControlToValidate="txtEmailRespFin" ErrorMessage="E-mail inválido" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        </div>
                    <div id="linhaFoneRespFin" runat="server" class="linha_campo">
                        <asp:Label ID="Label4" runat="server" Text="Fone*" CssClass="lblTitulocampo"></asp:Label>
                        <br/>
                        <asp:TextBox ID="txtFoneRespFin" runat="server" CssClass="campoform_pequeno maiusculo" MaxLength="20" 
                                 TabIndex="16" ></asp:TextBox>                                
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                ControlToValidate="txtFoneRespFin" Display="Dynamic" 
                                ErrorMessage="Campo obrigatório." SetFocusOnError="True"></asp:RequiredFieldValidator>                
                    </div>
                    <div id="linhaRamalRespFin" runat="server" class="linha_campo">
                        <asp:Label ID="Label7" runat="server" Text="Ramal" CssClass="lblTitulocampo"></asp:Label>
                        <br/>
                        <asp:TextBox ID="txtRamalRespFin" runat="server" CssClass="campoform_mini maiusculo" MaxLength="10" 
                                TabIndex="17" ></asp:TextBox>                                
                                       
                    </div>
                </div>

                
            </asp:Panel>
        <br />&nbsp;
        <div id="btns" runat="server">
            <asp:Button ID="btnConfirmarDadosRecibo" CssClass="botoesDestaque" runat="server" Text="Continuar" OnClick="btnConfirmar_Click"
                        TabIndex="18" />                
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
