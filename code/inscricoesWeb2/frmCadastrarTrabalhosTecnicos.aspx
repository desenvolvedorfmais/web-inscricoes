<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmCadastrarTrabalhosTecnicos.aspx.cs" Inherits="frmCadastrarTrabalhosTecnicos" Title="Inscri&ccedil;&otilde;es Web"
    
    EnableEventValidation="true"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="frmCad_auto">

<%--        <script type="text/javascript">
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

        </script>--%>

    <ajaxToolkit:ToolkitScriptManager ID="TSManager1" runat="server" EnableScriptLocalization="true"
        CombineScripts="true" ScriptMode="Release">
        <%--<ControlBundles>
            <ajaxToolkit:ControlBundle Name="Outros"/>
        </ControlBundles>--%>
    </ajaxToolkit:ToolkitScriptManager>  

<%--       <ajaxToolkit:ModalPopupExtender ID="modalExtender" runat="server" TargetControlID="pnlAguarde"
            PopupControlID="pnlAguarde" DropShadow="true" BackgroundCssClass="modalProgressGreyBackground">
        </ajaxToolkit:ModalPopupExtender>
        
        <asp:Panel ID="pnlAguarde" runat="server" CssClass="modalPopup">
            <asp:UpdatePanel ID="UPpnlAguarde" runat="server">
                <ContentTemplate>
                    <asp:UpdateProgress runat="server" id="ModalProgress" DisplayAfter="0" DynamicLayout="true">
                        <ProgressTemplate>
                            <div class="loading" align="center">
                                <br />Aguarde!... Processando sua solicitação<br />
                                <br />
                                <img src="imagensgeral/loader.gif" alt="" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </ContentTemplate>
             </asp:UpdatePanel>
        </asp:Panel>--%>
        <h1>
            <asp:Label ID="lblTituloPagina" runat="server" CssClass="titulo" 
                Text="Cadastro de Trabalho"></asp:Label>
            <br />
        </h1>

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate> 
                <table  id="Table2"  border="0" cellpadding="0" cellspacing="10" width="100%" runat="server"> 
                    <tr id="linhaTituloDocumento" runat="server" visible="false">
                        <td align="center" style="width: 100%">                            
                            <asp:Label ID="lblTituloEnvioDoc" runat="server" Text="Envio de Documento" CssClass="titulo"></asp:Label>
                        </td>
                   </tr>               
                    <tr id="linhaArquivo" runat="server" visible="false">
                                <td align="left" >
                                <asp:Label ID="lblInstrucaoUpLoad" runat="server" 
                                        Text="Selecione o arquivo para enviar (somente arquivos na extensão: .pdf | .doc | .docx | .rtf | .odt | .png | .jpg)"></asp:Label>
                                    <br />
                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="botoes" 
                                        Width="488px" />
                                    &nbsp;<asp:Button ID="btnEnviarTrabalho" runat="server" CssClass="botoes" TabIndex="32" Text="Enviar" OnClick="btnEnviarTrabalho_Click" />
                                    &nbsp;&nbsp;<br /><asp:Image ID="imgTrabEnviado" runat="server" ImageUrl="~/img/accept18x18.png" Visible="False" />
                                    <asp:Label ID="lblTrabEnviado" runat="server" BackColor="#33CC33" Font-Bold="True" ForeColor="White" Text="&amp;nbsp;&amp;nbsp;Trabalho já enviado&amp;nbsp;&amp;nbsp;" Visible="False"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Image ID="imgTrabBaixado" runat="server" ImageUrl="~/img/accept18x18.png" Visible="False" />
                                    <asp:Label ID="lblTrabBaixado" runat="server" BackColor="#33CC33" Font-Bold="True" ForeColor="White" Text="&amp;nbsp;&amp;nbsp;Trabalho já processado pela comissão organizadora&amp;nbsp;&amp;nbsp;" Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr id="linhaDeAcordo" runat="server" visible="false">
                                <td align="right" >
                                
                                    <asp:CheckBox ID="chkDeAcordo" runat="server" 
                                        Text="  Li e concordo com as normas do evento" TabIndex="29" />
                                
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Button ID="btnVoltar" runat="server" CausesValidation="False" 
                                        CssClass="botoes" TabIndex="30" Text="Voltar para listagem" 
                                        PostBackUrl="~/frmTrabalhosLista.aspx" />
                                    <asp:Button ID="btnNovo" runat="server" CausesValidation="False" 
                                        CssClass="botoes" onclick="btnNovo_Click" TabIndex="31" Text="Nova Proposta" Visible="False" />
                                    <asp:Button ID="btnGravar" runat="server" CssClass="botoes" 
                                        onclick="btnGravar_Click" TabIndex="32" Text="Gravar Proposta" />
                                    <asp:Button ID="btnExcluirProposta" runat="server" CssClass="botoes" 
                                        TabIndex="33" Text="Excluir Proposta" BackColor="Red" 
                                        onclick="btnExcluirProposta_Click" ForeColor="White" />
                                </td>
                            </tr>
                            <tr>
                                
                                        <td align="left" style="height: 26px;">
                                            <asp:Label ID="lblMsg" runat="server" CssClass="lblMsg" Visible="False" Width="100%"></asp:Label>
                                        </td>
                                    
                            </tr>
                </table>        
            </ContentTemplate>
        </asp:UpdatePanel>  
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
                <table  id="dadosTrabTecnicos"  border="0" cellpadding="0" cellspacing="10" width="100%" runat="server">
                    <tr>
                        <td align="left" style="width: 100%">
                            <asp:Label ID="lblMsg0" runat="server" CssClass="lblMsg" Visible="False" Width="100%"></asp:Label>
                            </td>
                    </tr>
                    <tr ID="linhalblRegrasPainel" runat="server" visible="false">
                        <td align="left" style="width: 100%">
                            <asp:Label ID="lblRegrasPainel" runat="server"></asp:Label>
                        </td>
                        
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%">
                            <br />
                            <asp:Label ID="lblID" runat="server" Text="ID"></asp:Label>
                            &nbsp;<asp:Label ID="txtCdTese" runat="server" Font-Bold="False" Font-Size="Larger" ForeColor="Red"></asp:Label>
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                            <asp:Label ID="lblSit" runat="server" Text="Situação"></asp:Label>
                            &nbsp;<asp:Label ID="lblSituacao" runat="server" Font-Bold="True" Font-Size="Larger" ForeColor="Blue"></asp:Label>
                        </td>
                    </tr>
                    <tr id="linhaDetalhesTrabalho" runat="server">
                        <td align="center" style="width: 100%">
                            <asp:Label ID="lblTituloDetalheTrab" runat="server" CssClass="titulo" Text="Detalhes do Trabalho"></asp:Label>
                        </td>
                    </tr>
                                        
                    
                    <tr id="linhaModalidadeTipo" runat="server" visible="true">
                        <td align="left" style="width: 100%">
                            <asp:Label ID="lblModalidadeTipo" runat="server" 
                                Text="Modalidade*"></asp:Label>
                            <br />
                            <asp:DropDownList ID="txtModalidadeTipo" runat="server" 
                                CssClass="txt campoform_enorme" TabIndex="1">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" 
                                ControlToValidate="txtModalidadeTipo" Display="Dynamic" Enabled="True" 
                                ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    
                    <tr id="linhaAreaTematica" runat="server" visible="true">
                        <td align="left" style="width: 100%">
                            <asp:Label ID="lblAreaTematica" runat="server" Text="Eixo Temático*"></asp:Label>
                            <br />
                            <asp:DropDownList ID="txtAreaTematica" runat="server" 
                                CssClass="txt campoform_enorme" TabIndex="1" AutoPostBack="True" OnSelectedIndexChanged="txtAreaTematica_SelectedIndexChanged">
                                
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" 
                                ControlToValidate="txtAreaTematica" Display="Dynamic" Enabled="True" 
                                ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="linhaAreaTematicaFilho" runat="server" visible="true">
                        <td align="left" style="width: 100%">
                            <asp:Label ID="lblAreaTematicaFilho" runat="server" Text="Sub Eixo Temático*"></asp:Label>
                            <br />
                            <asp:DropDownList ID="txtAreaTematicaFilho" runat="server" 
                                CssClass="txt campoform_enorme" TabIndex="1">
                                
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" 
                                ControlToValidate="txtAreaTematicaFilho" Display="Dynamic" Enabled="True" 
                                ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="linhaTituloAssunto" runat="server" visible="true">
                        <td align="left" style="width: 100%">
                            <asp:Label ID="lblTitulo" runat="server" 
                                Text="Título do trabalho* "></asp:Label>
                            <br />
                            <asp:TextBox ID="txtTitulo" runat="server" 
                                onkeyup="return contarCaracteres(this, 'ctl00_ContentPlaceHolder1_lblContCarct1', 250)"
                                TabIndex="2" Height="48px" MaxLength="250" TextMode="MultiLine" CssClass="txtNormal campoform_enorme"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                ControlToValidate="txtTitulo" Display="Dynamic" Enabled="True" 
                                ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>                            
                            <div id="lblContCarct1" runat="server" class="contaCaracteres">( 0 de 250 )</div>
                        </td>
                    </tr>
                    <tr id="linhaDescricao" runat="server" visible="true">
                        <td align="left" style="width: 100%">
                            <asp:Label ID="lblDsTrabalho" runat="server" Text="Descrição do trabalho* (máximo de 2.500 caracteres)"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtDsTrabalho" runat="server" TabIndex="3" 
                                onkeyup="return contarCaracteres(this, 'ctl00_ContentPlaceHolder1_lblContCarct2', 2500)" Height="133px" TextMode="MultiLine" CssClass="txtNormal campoform_enorme" MaxLength="2500" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                ControlToValidate="txtDsTrabalho" Display="Dynamic" Enabled="True" 
                                ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>                            
                            <div id="lblContCarct2" runat="server" class="contaCaracteres">( 0 de 2500 )</div>
                        </td>
                    </tr>

                    <tr id="linhaDadosAutor" runat="server">
                        <td align="center" style="width: 100%">
                            <asp:Label ID="lblTituloDadosAutor" runat="server" CssClass="titulo" Text="Dados do Autor"></asp:Label>
                        </td>
                    </tr>
                    <tr  id="linhaTpCadastro" runat="server" visible="false">
                        <td align="left" style="width: 100%">
                            <asp:Panel ID="pnlTipoDoc" runat="server">
                                <asp:RadioButton ID="chkCPF" runat="server" Text="CPF" Checked="True" 
                                    TabIndex="3" AutoPostBack="True" 
                                    oncheckedchanged="chkCPF_CheckedChanged" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="chkPassaporte" runat="server" Text="Passaporte" 
                                    TabIndex="4" AutoPostBack="True" 
                                    oncheckedchanged="chkPassaporte_CheckedChanged" />
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr  id="linhaCPF" runat="server" visible="true">
                        <td align="left" style="width: 100%">
                            <asp:Label ID="lblDsCPF" runat="server" Text="CPF*"></asp:Label>
                            <br />
                            <asp:TextBox ID="TXTDsCPF" runat="server" CssClass="txt campoform_pequeno" MaxLength="14" 
                                onkeypress="return Mascarar(this, event, '999.999.999-99')" ReadOnly="false" 
                                TabIndex="4" AutoPostBack="True" Enabled="False" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" 
                                ControlToValidate="TXTDsCPF" Display="Dynamic" Enabled="True" 
                                ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                            <asp:Label ID="lblMsgBuscaCPF" runat="server" CssClass="lblMsg" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr  id="linhaPassaporte" runat="server" visible = "false">
                        <td align="left" style="width: 100%">
                            <asp:Label ID="Label162" runat="server" Text="Passaporte*"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtPassaporte" runat="server" TabIndex="5" CssClass="txt campoform_pequeno"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" 
                                ControlToValidate="txtPassaporte" Display="Dynamic" Enabled="True" 
                                ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="linhaNome" runat="server">
                        <td align="left" style="width: 100%">
                            <asp:Label ID="lblNome" runat="server" Text="Nome*"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtNome" runat="server" CssClass="txt campoform_grande" 
                                TabIndex="6" Enabled="False"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="txtNome" Display="Dynamic" Enabled="True" 
                                ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                          
                    <tr  id="linhaSexo" runat="server" visible="false">
                        <td align="left" style="width: 100%">
                            <asp:Label ID="Label7" runat="server" Text="Sexo*"></asp:Label>
                            <br />
                            <asp:DropDownList ID="txtSexo" runat="server" CssClass="txt campoform_pequeno" 
                                    TabIndex="7">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>MASCULINO</asp:ListItem>
                                <asp:ListItem>FEMININO</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
                                ControlToValidate="txtSexo" Display="Dynamic" Enabled="True" 
                                ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="linhaEmail" runat="server" visible="false">
                        <td align="left" style="width: 100%">
                            <asp:Label ID="Label151" runat="server" 
                                Text="Email*"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="email campoform_grande" 
                                TabIndex="8"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" 
                                ControlToValidate="txtEmail" Display="Dynamic" Enabled="True" 
                                ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                            <br />
                        </td>
                    </tr>                    
                    <tr id="linhaPais" runat="server" visible="false">
                        <td align="left" style="width: 100%">
                            <asp:Label ID="Label167" runat="server" Text="País*"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtPais" runat="server" CssClass="txt campoform_grande" TabIndex="9">BRASIL</asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" 
                                ControlToValidate="txtPais" Display="Dynamic" Enabled="True" 
                                ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                                        
                    <tr  id="linhaCEP" runat="server" visible="false">
                        <td align="left" style="width: 100%">
                            <asp:Label ID="Label12" runat="server" Text="CEP*"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtCEPRecibo" runat="server" MaxLength="10" CssClass="txt campoform_pequeno"
                                onkeypress="return Mascarar(this, event, '99.999-999')" TabIndex="9"></asp:TextBox>&nbsp;
                            <asp:Button ID="btnCEP" runat="server" CausesValidation="False" 
                                CssClass="botoes" Text="Preencher" TabIndex="10" onclick="btnCEP_Click" />
                            &nbsp;<asp:RegularExpressionValidator
                                    ID="RegularExpressionValidator12" runat="server" ControlToValidate="txtCEPRecibo"
                                    Display="Dynamic" ErrorMessage="Valor do campo inválido." ValidationExpression="[\.\-\d]{1,10}"
                                    Enabled="false"></asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator12"
                                        runat="server" ControlToValidate="txtCEPRecibo" Display="Dynamic" Enabled="True"
                                        ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                            <asp:Label ID="lblMsgCEP" runat="server" 
                                Text="CEP não encontrado!" Visible="False" CssClass="lblMsg"></asp:Label>
                            <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                <ProgressTemplate>
                                    Aguarde, pesquisando...
                                </ProgressTemplate>
                            </asp:UpdateProgress>--%>
                        </td>
                    </tr>
                    <tr  id="linhaEndereco" runat="server" visible="false">
                        <td align="left" style="width: 100%">
                            <asp:Label ID="Label13" runat="server" Text="Endereço*"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtEnderecoRecibo" runat="server" TabIndex="11" 
                                MaxLength="125" CssClass="txt campoform_grande"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtEnderecoRecibo"
                                    Display="Dynamic" Enabled="True" ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr  id="linhaComplemento" runat="server" visible="false">
                        <td  align="left" style="width: 100%">
                            <asp:Label ID="Label4" runat="server" Text="Complemento*"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtComplementoEnderecoRecibo" runat="server" CssClass="txt campoform_grande" 
                                MaxLength="125" TabIndex="12"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" 
                                ControlToValidate="txtComplementoEnderecoRecibo" Display="Dynamic" 
                                Enabled="True" ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr  id="linhaBairro" runat="server" visible="false">
                        <td  align="left" style="width: 100%">
                            <asp:Label ID="Label150" runat="server" Text="Bairro*"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtBairroRecibo" runat="server" TabIndex="13" 
                                MaxLength="72" CssClass="txt campoform_grande"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBairroRecibo"
                                    Display="Dynamic" Enabled="True" ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr  id="linhaUF" runat="server" visible="false">
                        <td  align="left" style="width: 100%">
                            <asp:Label ID="Label10" runat="server" Text="UF*"></asp:Label>
                            <br />
                            <asp:DropDownList ID="txtUF" runat="server" CssClass="txt campoform_mini" 
                                TabIndex="14">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtUF" Display="Dynamic"
                                    Enabled="True" ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr  id="linhaCidade" runat="server" visible="false">
                        <td  align="left" style="width: 100%">
                            <asp:Label ID="Label11" runat="server" Text="Cidade*"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtCidadeRecibo" runat="server" MaxLength="60" TabIndex="15" CssClass="txt campoform_medio"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtCidadeRecibo"
                                    Display="Dynamic" Enabled="True" ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="linhaFone" runat="server" visible="false">
                        <td  align="left" style="width: 100%">
                            <asp:Label ID="Label163" runat="server" Text="Fone*"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtFone" runat="server" CssClass="txt campoform_pequeno" MaxLength="60" 
                                onkeypress="return Mascarar(this, event, '(99) 999999999')" 
                                TabIndex="16"></asp:TextBox>
                        </td>
                    </tr>

                    <tr id="linhaTitulacao" runat="server" visible="false">
                        <td align="left" style="width: 100%">
                            <asp:Label ID="Label164" runat="server" 
                                Text="Titulação* (Escolha a titulação máxima do seu currículo)"></asp:Label>
                            <br />
                            <asp:DropDownList ID="txtTitulacao" runat="server" 
                                CssClass="txt campoform_grande" TabIndex="17">
                                <asp:ListItem></asp:ListItem>                                
                                <asp:ListItem>SUPERIOR INCOMPLETO</asp:ListItem>
                                <asp:ListItem>SUPERIOR COMPLETO</asp:ListItem>
                                <asp:ListItem>PÓS-GRADUAÇÃO INCOMPLETO</asp:ListItem>
                                <asp:ListItem>PÓS-GRADUAÇÃO COMPLETO</asp:ListItem>
                                <asp:ListItem>MESTRE INCOMPLETO</asp:ListItem>
                                <asp:ListItem>MESTRE COMPLETO</asp:ListItem>
                                <asp:ListItem>DOUTOR INCOMPLETO</asp:ListItem>
                                <asp:ListItem>DOUTOR COMPLETO</asp:ListItem>
                                <asp:ListItem>PÓS-DOCTOR INCOMPLETO</asp:ListItem>
                                <asp:ListItem>PÓS-DOCTOR COMPLETO</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" 
                                ControlToValidate="txtTitulacao" Display="Dynamic" Enabled="True" 
                                ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="linhaInstituicaoGraduacao" runat="server" visible="false">
                        <td align="left" style="width: 100%">
                            <asp:Label ID="Label20" runat="server" 
                                Text="Instituição da Titulação* "></asp:Label>
                            <br />
                            <asp:TextBox ID="txtInstituicaoGraduacao" runat="server" CssClass="txt campoform_grande" MaxLength="250" 
                                TabIndex="17" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" 
                                ControlToValidate="txtInstituicaoGraduacao" Display="Dynamic" Enabled="True" 
                                ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="linhaTpInstituicao" runat="server" visible="false">
                        <td align="left" style="width: 100%">
                            <asp:Label ID="Label15" runat="server" 
                                Text="Instituição Governamental* "></asp:Label>
                            <br />                            
                            <asp:DropDownList ID="txtTpInstituicao" runat="server" 
                                CssClass="text campoform_mini" TabIndex="20" AutoPostBack="True" OnTextChanged="txtTpInstituicao_TextChanged">
                                <asp:ListItem></asp:ListItem>                                
                                <asp:ListItem>SIM</asp:ListItem>
                                <asp:ListItem>NÃO</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                ControlToValidate="txtTpInstituicao" Display="Dynamic" Enabled="True" 
                                ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="linhaNivelGeverno" runat="server" visible="false">
                        <td align="left" style="width: 100%">
                            <asp:Label ID="Label16" runat="server" 
                                Text="Nível de Governo* "></asp:Label>
                            <br />                           
                            <asp:DropDownList ID="txtNivelGoverno" runat="server" 
                                CssClass="text campoform_pequeno" TabIndex="21" >
                                <asp:ListItem></asp:ListItem>                                
                                <asp:ListItem>FEDERAL</asp:ListItem>
                                <asp:ListItem>ESTADUAL</asp:ListItem>
                                <asp:ListItem>MUNICIPAL</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                                ControlToValidate="txtNivelGoverno" Display="Dynamic" Enabled="True" 
                                ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="linhaPoderGoverno" runat="server" visible="false">
                        <td align="left" style="width: 100%">
                            <asp:Label ID="Label17" runat="server" 
                                Text="Poder de Governo* "></asp:Label>
                            <br />                          
                            <asp:DropDownList ID="txtPoderGoverno" runat="server" 
                                CssClass="text campoform_pequeno" TabIndex="22" >
                                <asp:ListItem></asp:ListItem>                                
                                <asp:ListItem>EXECUTIVO</asp:ListItem>
                                <asp:ListItem>LEGISLATIVO</asp:ListItem>
                                <asp:ListItem>JUDICIARIO</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                                ControlToValidate="txtPoderGoverno" Display="Dynamic" Enabled="True" 
                                ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="linhaTipoSetorPrivado" runat="server" visible="false">
                        <td align="left" style="width: 100%">
                            <asp:Label ID="Label18" runat="server" 
                                Text="Tipo* "></asp:Label>
                            <br />                         
                            <asp:DropDownList ID="txtTipoSetorPrivado" runat="server" 
                                CssClass="text campoform_medio" TabIndex="23" >
                                <asp:ListItem></asp:ListItem>                                
                                <asp:ListItem>EMPRESA PRIVADA</asp:ListItem>
                                <asp:ListItem>TERCEIRO SETOR</asp:ListItem>
                                <asp:ListItem>ORGANIZAÇÃO INTERNACIONAL</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" 
                                ControlToValidate="txtTipoSetorPrivado" Display="Dynamic" Enabled="True" 
                                ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="linhaInstituicao" runat="server" visible="false">
                        <td align="left" style="width: 100%">
                            <asp:Label ID="Label165" runat="server" 
                                Text="Instituição*"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtInstituicao" runat="server" CssClass="txt campoform_grande" MaxLength="250" 
                                TabIndex="18"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" 
                                ControlToValidate="txtInstituicao" Display="Dynamic" Enabled="True" 
                                ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="linhaAreaAtuacao" runat="server" visible="false">
                        <td align="left" style="width: 100%">
                            <asp:Label ID="Label9" runat="server" 
                                Text="Cargo*"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtAreaAtuacao" runat="server" CssClass="txt campoform_grande" MaxLength="250" 
                                TabIndex="19"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                ControlToValidate="txtAreaAtuacao" Display="Dynamic" Enabled="True" 
                                ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                                
                    
                    
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate> 
                <asp:Panel ID="pnlCoautores" runat="server" Visible="true">
                    <table  id="Table1"  border="0" cellpadding="0" cellspacing="10" width="100%" runat="server">
                        <tr>
                            <td align="center" style="width: 100%">                            
                                <asp:Label ID="lblTituloOutrosAutores" runat="server" Text="Outros Autores" CssClass="titulo"></asp:Label>
                            </td>
                        </tr>                                     
                        <tr id="linhaCPFCoautor" runat="server" visible="true">
                            <td align="left" style="width: 100%">
                                <asp:Label ID="Label8" runat="server" Text="CPF*"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtCPFCoautor" runat="server" CssClass="txt campoform_pequeno" MaxLength="14" 
                                onkeypress="return Mascarar(this, event, '999.999.999-99')" ReadOnly="false" 
                                TabIndex="23" AutoPostBack="True" OnTextChanged="txtCPFCoautor_TextChanged"></asp:TextBox>
                            <asp:Label ID="lblMsgBuscaCPF2" runat="server" CssClass="lblMsg" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr id="linhaNomeCoautor" runat="server" visible="true">
                            <td align="left"  style="width: 100%">
                                <asp:Label ID="lblNomeCoautor" runat="server" Text="Nome"></asp:Label>                                        
                                <br />                                        
                                <asp:TextBox ID="txtNoCoautor" runat="server" TabIndex="24" CssClass="txt_upper campoform_grande" MaxLength="100"></asp:TextBox>
                                &nbsp;
                                <br />
                                
                            </td>
                        </tr>
                        <tr id="linhaApresentador" runat="server" visible="false">
                            <td align="left"  style="width: 100%">
                                <asp:CheckBox ID="chkApresentador" runat="server" Text="Apresentador" TabIndex="25" />
                                &nbsp;&nbsp;&nbsp;
                                <asp:Label ID="Label173" runat="server" Font-Size="Small" ForeColor="Red">(marcar apenas se o autor do trabalho não for o apresentador)</asp:Label>
                            </td>
                        </tr>
                        <tr id="linhaBtnsCoautor" runat="server" visible="true">
                            <td align="left"  style="width: 100%">
                                <asp:Button ID="btnGravarSubscritor" runat="server" CausesValidation="False" CssClass="botoes" onclick="btnGravarSubscritor_Click" TabIndex="26" Text="Incluir" />
                                <asp:Button ID="btnExcluirTodosCoautores" runat="server" BackColor="Red" CausesValidation="False" CssClass="botoes" onclick="btnExcluirTodosCoautores_Click" TabIndex="27" Text="Remover Todos" ForeColor="White" />
                                <br />
                                <asp:Label ID="lblObsCoautores" runat="server" 
                                    Text="*Após a inclusão de coautores deve-se gravar a proposta." Font-Bold="True" Font-Size="12pt" ForeColor="Red"></asp:Label>
                                <br />
                                <asp:Label ID="lblMsgCoautor" runat="server" CssClass="lblMsg" Visible="False" Width="100%"></asp:Label>
                            </td>
                        </tr>
                        
                        <tr>
                            <td align="left"  style="height: 24px;">
                                <asp:GridView ID="grdSubscritor" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="None" Width="100%" CellSpacing="1" TabIndex="28" 
                                    DataKeyNames="noParticipanteTese" onrowcommand="grdSubscritor_RowCommand" 
                                    onrowdeleting="grdSubscritor_RowDeleting" OnRowDataBound="grdSubscritor_RowDataBound">
                                    <RowStyle BackColor="#EFF3FB" />
                                    <Columns>
                                        <asp:BoundField DataField="ordem" SortExpression="ordem">
                                            <HeaderStyle Width="70px" />
                                            <ItemStyle Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nuCPF" HeaderText="CPF" SortExpression="nuCPF">
                                            <HeaderStyle Width="130px" />
                                            <ItemStyle Width="130px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="noParticipanteTese" HeaderText="Nome" 
                                            SortExpression="noParticipanteTese">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Height="22px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tpParticipacao1" HeaderText="tpParticipacao1" SortExpression="tpParticipacao1" Visible="False"></asp:BoundField>
                                        <asp:CheckBoxField DataField="flApresentador" HeaderText="Apresentador" SortExpression="flApresentador" Visible="False">
                                            <HeaderStyle Width="70px" />
                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:CheckBoxField>
                                        <asp:CommandField DeleteImageUrl="~/img/delete 18x18.png" ShowDeleteButton="True" CausesValidation="False" DeleteText="Remover" ButtonType="Image" SelectImageUrl="~/img/delete 18x18.png">
                                        <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        </asp:CommandField>
                                        <asp:BoundField DataField="cdParticipanteTese" HeaderText="cdParticipanteTese" SortExpression="cdParticipanteTese" Visible="False"></asp:BoundField>
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
