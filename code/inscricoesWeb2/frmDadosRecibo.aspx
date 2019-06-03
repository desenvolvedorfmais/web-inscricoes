<%@ Page Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true"
    CodeFile="frmDadosRecibo.aspx.cs" Inherits="frmDadosRecibo" Title="Inscri&ccedil;&otilde;es Web" %>

<%@ Register Assembly="ControlMessageBox" Namespace="ControlMessageBox" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="frmCad_auto">
        <h3>
            <asp:Label ID="lblTituloPagina" runat="server" Text="Dados para recibo" CssClass="titulo"></asp:Label>
            <br />
            
        </h3>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptLocalization="true"
            CombineScripts="false">
        </asp:ToolkitScriptManager>
    
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
       
        <asp:UpdatePanel ID="upd2" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblMsg" runat="server" Font-Size="Medium" ForeColor="Red" CssClass="msg" Visible ="false"></asp:Label>
                <div id="dadosRecibo" class="clsDadosRecibo" width="100%" style="margin-top: 20px;">
                    <div>
                        <asp:Label ID="lblMsgDadosRecibo" runat="server" Text="(preencha com atenção, pois será o seu documento fiscal)"></asp:Label>
                
                    </div>
                    <div id="linhatipPessoa" runat="server" class="linha_campo">
                        <asp:Label ID="lblTipoPessoa" runat="server" Text="Tipo Pessoa*" Visible="True"></asp:Label>
                        <br/>
                        <asp:DropDownList ID="txtTipoPessoa" runat="server" AutoPostBack="True" CssClass="campoform_pequeno"
                                onselectedindexchanged="txtTipoPessoa_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="PF">PESSOA FÍSICA</asp:ListItem>
                                <asp:ListItem Value="PJ">PESSOA JURÍDICA</asp:ListItem>
                        </asp:DropDownList>                
                    </div>
                    <div id="linhaCPF" runat="server" class="linha_campo">
                        <asp:Label ID="Label152" runat="server" Text="CPF*" CssClass="lblTitulocampo"></asp:Label>
                        <br/>
                        <asp:TextBox ID="txtCPFRecibo" runat="server" CssClass="campoform_pequeno" MaxLength="14" 
                                onkeypress="return CPFMascarar(this, event)" TabIndex="1" AutoPostBack="True" OnTextChanged="txtCPFRecibo_TextChanged" ></asp:TextBox>
                            &nbsp;<asp:Button ID="btnDadosParticipante" runat="server" CausesValidation="False" 
                                CssClass="botoes" onclick="btnDadosParticipante_Click" TabIndex="2" 
                                Text="Buscar" Visible="False" />
                            <asp:RegularExpressionValidator ID="REVTxtCPF0" runat="server" 
                                ControlToValidate="txtCPFRecibo" Display="Dynamic" Enabled="false" 
                                ErrorMessage="Valor inválido." ValidationExpression="[-\.\d\/]{1,18}"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RFVCPF0" runat="server" 
                                ControlToValidate="txtCPFRecibo" Display="Dynamic" 
                                ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>                
                    </div>
                    <div id="linhaCNPJ" runat="server" visible="false" class="linha_campo">
                        <asp:Label ID="Label5" runat="server" Text="CNPJ*" CssClass="lblTitulocampo"></asp:Label>
                        <br/>
                        <asp:TextBox ID="txtCNPJRecibo" runat="server" CssClass="campoform_pequeno" MaxLength="18"
                                onkeypress="return CNPJMascarar(this, event)" TabIndex="3" ></asp:TextBox>&nbsp;<asp:Button ID="btnDadosInstituicao" 
                                runat="server" CausesValidation="False" CssClass="botoes" 
                                onclick="btnDadosInstituicao_Click" Text="Buscar" TabIndex="4" />
                            <asp:RegularExpressionValidator
                                    ID="REVTxtCPF" runat="server" ControlToValidate="txtCNPJRecibo" Display="Dynamic"
                                    ErrorMessage="Valor inválido." ValidationExpression="[-\.\d\/]{1,18}" Enabled="false"></asp:RegularExpressionValidator><asp:RequiredFieldValidator
                                        ID="RFVCPF" runat="server" ControlToValidate="txtCNPJRecibo" Display="Dynamic"
                                        ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                            <asp:Label ID="lblMsg2" runat="server" ForeColor="Red"></asp:Label>                
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
                                ControlToValidate="txtNomeRecibo" Display="Dynamic" Enabled="True" 
                                ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>                
                    </div>
                    <div id="linhaIE" runat="server" visible="false" class="linha_campo">
                        <asp:Label ID="Label6" runat="server" Text="Inscr Estatual" CssClass="lblTitulocampo"></asp:Label>
                        <br/>
                        <asp:TextBox ID="txtIE" runat="server" CssClass="campoform_pequeno" MaxLength="50" 
                                TabIndex="6" ></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                ControlToValidate="txtIE" Display="Dynamic" 
                                ErrorMessage="Valor do campo inválido." 
                        
                                ValidationExpression="[áéíóúàâêôãõüÁÉÍÓÚÀÂÊÔÃÕÜÇçºª@&amp;\-\.\,\w\s\d\\\/]{1,100}"></asp:RegularExpressionValidator>                
                    </div>
                    <div id="linhaCep" runat="server" class="linha_campo">                
                        <asp:Label ID="Label12" runat="server" Text="CEP*" CssClass="lblTitulocampo"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtCEPRecibo" runat="server" MaxLength="10" CssClass="campoform_pequeno maiusculo" 
                                onkeypress="return Mascarar(this, event, '99.999-999')" TabIndex="7"></asp:TextBox>&nbsp;
                            <asp:Button ID="btnCEP" runat="server" CausesValidation="False" 
                                CssClass="botoes" onclick="btnCEP_Click" Text="Verificar" TabIndex="8" />
                                &nbsp;<asp:RegularExpressionValidator
                                    ID="RegularExpressionValidator12" runat="server" ControlToValidate="txtCEPRecibo"
                                    Display="Dynamic" ErrorMessage="Valor do campo inválido." ValidationExpression="[\.\-\d]{1,10}"
                                    Enabled="false"></asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator12"
                                        runat="server" ControlToValidate="txtCEPRecibo" Display="Dynamic" Enabled="True"
                                        ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                            <asp:Label ID="lblMsgCEP" runat="server" ForeColor="Red" 
                                Text="CEP não encontrado!" Visible="False"></asp:Label>                
                    </div>
                    <div id="linhaEndereco" runat="server" class="linha_campo">
                        <asp:Label ID="Label13" runat="server" Text="Endereço*" CssClass="lblTitulocampo"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtEnderecoRecibo" runat="server"  TabIndex="9" 
                                MaxLength="125" CssClass="campoform_grande maiusculo"></asp:TextBox><asp:RegularExpressionValidator
                                ID="RegularExpressionValidator11" runat="server" ControlToValidate="txtEnderecoRecibo"
                                Display="Dynamic" ErrorMessage="Valor do campo inválido." 
                                ValidationExpression="[áéíóúàâêôãõüÁÉÍÓÚÀÂÊÔÃÕÜÇçºª@&amp;\-\.\,\w\s\d\\\/]{1,100}"></asp:RegularExpressionValidator><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtEnderecoRecibo"
                                    Display="Dynamic" Enabled="True" ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>                
                    </div>
                    <div id="linhaComplemento" runat="server" class="linha_campo">
                        <asp:Label ID="Label151" runat="server" Text="Complemento*" CssClass="lblTitulocampo"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtComplementoEnderecoRecibo" runat="server" CssClass="campoform_grande maiusculo" 
                                MaxLength="125" TabIndex="10"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator14" 
                                runat="server" ControlToValidate="txtComplementoEnderecoRecibo" 
                                Display="Dynamic" ErrorMessage="Valor do campo inválido." 
                        
                                ValidationExpression="[áéíóúàâêôãõüÁÉÍÓÚÀÂÊÔÃÕÜÇçºª@&amp;\-\.\,\w\s\d\\\/]{1,100}"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" 
                                ControlToValidate="txtComplementoEnderecoRecibo" Display="Dynamic" 
                                Enabled="True" ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>                
                    </div>
                    <div id="linhaBairro" runat="server" class="linha_campo">                
                            <asp:Label ID="Label150" runat="server" Text="Bairro*" CssClass="lblTitulocampo"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtBairroRecibo" runat="server"  TabIndex="11" 
                                MaxLength="72" CssClass="campoform_medio maiusculo"></asp:TextBox><asp:RegularExpressionValidator
                                ID="RegularExpressionValidator13" runat="server" ControlToValidate="txtBairroRecibo"
                                Display="Dynamic" ErrorMessage="Valor do campo inválido." 
                        
                                ValidationExpression="[áéíóúàâêôãõüÁÉÍÓÚÀÂÊÔÃÕÜÇçºª@&amp;\-\.\,\w\s\d\\\/]{1,100}"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtBairroRecibo"
                                    Display="Dynamic" Enabled="True" ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>                
                    </div>
                    <div id="linhaUF" runat="server" class="linha_campo">
                        <asp:Label ID="Label10" runat="server" Text="UF*" CssClass="lblTitulocampo"></asp:Label>
                        <br />
                        <asp:DropDownList ID="txtUFRecibo" runat="server" CssClass="campoform_mini maiusculo" 
                                TabIndex="12">
                            </asp:DropDownList>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtUFRecibo"
                                Display="Dynamic" ErrorMessage="Valor do campo inválido." ValidationExpression="[áéíóúàâêôãõüÁÉÍÓÚÀÂÊÔÃÕÜÇçºª@&\-\.\,\w\s\d]{1,100}"></asp:RegularExpressionValidator><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtUFRecibo" Display="Dynamic"
                                    Enabled="True" ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>                
                    </div>
                    <div id="linhaCidade" runat="server" class="linha_campo">
                        <asp:Label ID="Label11" runat="server" Text="Cidade*" CssClass="lblTitulocampo"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtCidadeRecibo" runat="server" MaxLength="60" TabIndex="13" 
                                    CssClass="campoform_medio maiusculo"></asp:TextBox><asp:RegularExpressionValidator
                                ID="RegularExpressionValidator10" runat="server" ControlToValidate="txtCidadeRecibo"
                                Display="Dynamic" ErrorMessage="Valor do campo inválido." ValidationExpression="[áéíóúàâêôãõüÁÉÍÓÚÀÂÊÔÃÕÜÇçºª@&\-\.\,\w\s\d]{1,100}"></asp:RegularExpressionValidator><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtCidadeRecibo"
                                    Display="Dynamic" Enabled="True" ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>                
                    </div>
                    <div id="lnhaBtnConfirma" runat="server">
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                            <ProgressTemplate>
                                <span style="font-size:12.0pt;font-family:verdana,sans-serif;mso-fareast-font-family:times new roman;mso-bidi-font-family:arial;color:#D70000;">Aguarde, pesquisando...</span>
                            </ProgressTemplate>
                        </asp:UpdateProgress>                
                    </div>
                </div>
        <br />&nbsp;
        <asp:UpdatePanel ID="pnlRespFin" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlResponsavelFinanceiro" runat="server" visible="False">
                    <div id="ResponsavelFinanceiro" class="clsResponsavelFinanceiro" width="100%">
                        <div>
                            <h3>
                                <asp:Label ID="Label9" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Departamento Responsável Financeiro" CssClass="title"></asp:Label>
                            </h3>
                        </div>
                        <div id="linhaNomeRespFin" runat="server">
                            <asp:Label ID="Label1" runat="server" Text="Nome*" CssClass="lblTitulocampo"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtNomeRespFin" runat="server" CssClass="campoform_grande maiusculo" MaxLength="100" 
                                    TabIndex="14" Width="400px" ></asp:TextBox>                                
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ControlToValidate="txtNomeRespFin" Display="Dynamic" Enabled="True" 
                                    ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>                
                        </div> 
                        <div id="linhaEmailRespFin" runat="server">
                            <asp:Label ID="Label14" runat="server" Text="Email*" CssClass="lblTitulocampo"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtEmailRespFin" runat="server" CssClass="campoform_grande minusculo" MaxLength="100" 
                                    TabIndex="15" Width="400px" ></asp:TextBox>                                
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                    ControlToValidate="txtEmailRespFin" Display="Dynamic" Enabled="True" 
                                    ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>                
                        </div>
                        <div id="linhaFoneRespFin" runat="server">
                            <asp:Label ID="Label15" runat="server" Text="Fone*" CssClass="lblTitulocampo"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtFoneRespFin" runat="server" CssClass="campoform_pequeno maiusculo" MaxLength="20" 
                                    onkeypress="return Mascarar(this, event, '(99) 999999999')" TabIndex="16" ></asp:TextBox>                                
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                    ControlToValidate="txtFoneRespFin" Display="Dynamic" Enabled="True" 
                                    ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>                
                        </div>
                        <div id="linhaRamalRespFin" runat="server">
                            <asp:Label ID="Label17" runat="server" Text="Ramal" CssClass="lblTitulocampo"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtRamalRespFin" runat="server" CssClass="campoform_mini maiusculo" MaxLength="10" 
                                    TabIndex="17" Width="80px" ></asp:TextBox>                                
                                       
                        </div>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />&nbsp;
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />&nbsp;
        <div id="btns" runat="server">
            <asp:Button ID="btnConfirmarDadosRecibo" CssClass="botoesDestaque" runat="server" Text="Confirmar" OnClick="btnConfirmar_Click"
                        TabIndex="18" />
                    &nbsp;<asp:Button ID="btnVoltar" runat="server" 
                        CausesValidation="False" CssClass="botoes" Font-Bold="False" 
                        PostBackUrl="~/frmListarPedidos.aspx" TabIndex="19" Text="Voltar" 
                        Width="105px" />            
        </div>
        &nbsp; &nbsp;&nbsp;
        <br />
        
        <br />
    </div>
    <div>
        &nbsp;
    </div>
</asp:Content>
