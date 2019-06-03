<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmCadastrarTrabalhosTecnicos_old.aspx.cs" Inherits="frmCadastrarTrabalhosTecnicos_old" Title="Inscri&ccedil;&otilde;es Web"
    
    EnableEventValidation="true"  %>

<%@ Register Assembly="ControlMessageBox" Namespace="ControlMessageBox" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="frmCad_auto">
    <asp:ToolkitScriptManager ID="TSManager1" runat="server" EnableScriptLocalization="true"
        CombineScripts="false" ScriptMode="Release">
    </asp:ToolkitScriptManager>
    
    
        <h1>
            <asp:Label ID="lblTituloPagina" runat="server" CssClass="titulo" 
                Text="Trabalho Técnico - Cadastro"></asp:Label>
            <br />
        </h1>
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
                <table  id="dadosTrabTecnicos"  border="0" cellpadding="0" cellspacing="10" width="710" runat="server">
                    <tr>
                        <td align="left" style="width: 700px">
                            <asp:Label ID="lblMsg0" runat="server" Font-Size="Small" ForeColor="Red"></asp:Label>
                            </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 700px">
                            <br />
                            <asp:Label ID="Label1" runat="server" Text="ID"></asp:Label>
                            &nbsp;<asp:Label ID="txtCdTese" runat="server" Font-Bold="False" Font-Size="Larger" 
                                ForeColor="Red"></asp:Label>
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label171" runat="server" Text="Situação"></asp:Label>
                            &nbsp;<asp:Label ID="lblSitucao" runat="server" Font-Bold="True" 
                                ForeColor="Blue" Font-Size="Larger"></asp:Label>
                        </td>
                        
                    </tr>
                    <tr  id="linhaTpCadastro" runat="server" visible="false">
                        <td align="left" style="width: 700px">
                            <asp:Panel ID="pnlTipoDoc" runat="server">
                                <asp:RadioButton ID="chkCPF" runat="server" Text="CPF" Checked="True" 
                                    TabIndex="1" AutoPostBack="True" 
                                    oncheckedchanged="chkCPF_CheckedChanged" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="chkPassaporte" runat="server" Text="Passaporte" 
                                    TabIndex="2" AutoPostBack="True" 
                                    oncheckedchanged="chkPassaporte_CheckedChanged" />
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr  id="linhaCPF" runat="server" visible="false">
                        <td align="left" style="width: 700px">
                            <asp:Label ID="Label161" runat="server" Text="CPF*"></asp:Label>
                            <br />
                            <asp:TextBox ID="TXTDsCPF" runat="server" CssClass="text" MaxLength="14" 
                                onkeypress="return Mascarar(this, event, '999.999.999-99')" ReadOnly="false" 
                                TabIndex="3"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" 
                                ControlToValidate="TXTDsCPF" Display="Dynamic" Enabled="True" 
                                ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                            <asp:Label ID="lblMsgBuscaCPF" runat="server" Font-Size="Small" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr  id="linhaPassaporte" runat="server" visible = "false">
                        <td align="left" style="width: 700px">
                            <asp:Label ID="Label162" runat="server" Text="Passaporte*"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtPassaporte" runat="server" Width="211px" TabIndex="4"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" 
                                ControlToValidate="txtPassaporte" Display="Dynamic" Enabled="True" 
                                ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 700px">
                            <asp:Label ID="Label2" runat="server" Text="Nome*"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtNome" runat="server" Width="500px" CssClass="txt" 
                                TabIndex="5" ReadOnly="True"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="txtNome" Display="Dynamic" Enabled="True" 
                                ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr  id="linhaSexo" runat="server" visible="false">
                        <td align="left" style="width: 700px">
                            <asp:Label ID="Label7" runat="server" Text="Sexo*"></asp:Label>
                            <br />
                            <asp:DropDownList ID="txtSexo" runat="server" CssClass="text" 
                                 TabIndex="6" Width="151px">
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
                        <td align="left" style="width: 700px">
                            <asp:Label ID="Label151" runat="server" 
                                Text="Email*"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="email" Width="500px" 
                                TabIndex="7"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" 
                                ControlToValidate="txtEmail" Display="Dynamic" Enabled="True" 
                                ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                            <br />
                        </td>
                    </tr>
                    
                    <tr id="linhaPais" runat="server" visible="false">
                        <td align="left" style="width: 700px">
                            <asp:Label ID="Label167" runat="server" Text="País*"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtPais" runat="server" CssClass="txt" TabIndex="8" 
                                Width="500px">BRASIL</asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" 
                                ControlToValidate="txtPais" Display="Dynamic" Enabled="True" 
                                ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    
                    <tr  id="linhaCEP" runat="server" visible="false">
                        <td align="left" style="width: 700px">
                            <asp:Label ID="Label12" runat="server" Text="CEP*"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtCEPRecibo" runat="server" MaxLength="10" CssClass="txt" Width="100px"
                                onkeypress="return Mascarar(this, event, '99.999-999')" TabIndex="8"></asp:TextBox>&nbsp;
                            <asp:Button ID="btnCEP" runat="server" CausesValidation="False" 
                                CssClass="botoes" Text="Verificar" TabIndex="9" onclick="btnCEP_Click" />
                            &nbsp;<asp:RegularExpressionValidator
                                    ID="RegularExpressionValidator12" runat="server" ControlToValidate="txtCEPRecibo"
                                    Display="Dynamic" ErrorMessage="Valor do campo inválido." ValidationExpression="[\.\-\d]{1,10}"
                                    Enabled="false"></asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator12"
                                        runat="server" ControlToValidate="txtCEPRecibo" Display="Dynamic" Enabled="True"
                                        ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                            <asp:Label ID="lblMsgCEP" runat="server" ForeColor="Red" 
                                Text="CEP não encontrado!" Visible="False"></asp:Label><asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                        <ProgressTemplate>
                                            Aguarde, pesquisando...
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                        </td>
                    </tr>
                    <tr  id="linhaEndereco" runat="server" visible="false">
                        <td align="left" style="width: 700px">
                            <asp:Label ID="Label13" runat="server" Text="Endereço*"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtEnderecoRecibo" runat="server" Width="500px" TabIndex="10" 
                                MaxLength="125" CssClass="txt"></asp:TextBox><asp:RegularExpressionValidator
                                ID="RegularExpressionValidator11" runat="server" ControlToValidate="txtEnderecoRecibo"
                                Display="Dynamic" ErrorMessage="Valor do campo inválido." 
                                ValidationExpression="[áéíóúàâêôãõüÁÉÍÓÚÀÂÊÔÃÕÜÇçºª@&amp;\-\.\,\w\s\d\/\&quot;()?|!+=:]{1,100}"></asp:RegularExpressionValidator><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtEnderecoRecibo"
                                    Display="Dynamic" Enabled="True" ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr  id="linhaComplemento" runat="server" visible="false">
                        <td  align="left" style="width: 700px">
                            <asp:Label ID="Label4" runat="server" Text="Complemento*"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtComplementoEnderecoRecibo" runat="server" CssClass="txt" 
                                MaxLength="125" TabIndex="11" Width="500px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator14" 
                                runat="server" ControlToValidate="txtComplementoEnderecoRecibo" 
                                Display="Dynamic" ErrorMessage="Valor do campo inválido." 
                                
                                ValidationExpression="[áéíóúàâêôãõüÁÉÍÓÚÀÂÊÔÃÕÜÇçºª@&amp;\-\.\,\w\s\d\/\&quot;()?|!+=:]{1,100}"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" 
                                ControlToValidate="txtComplementoEnderecoRecibo" Display="Dynamic" 
                                Enabled="True" ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr  id="linhaBairro" runat="server" visible="false">
                        <td  align="left" style="width: 700px">
                            <asp:Label ID="Label150" runat="server" Text="Bairro*"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtBairroRecibo" runat="server" Width="370" TabIndex="12" 
                                MaxLength="72" CssClass="txt"></asp:TextBox><asp:RegularExpressionValidator
                                ID="RegularExpressionValidator13" runat="server" ControlToValidate="txtBairroRecibo"
                                Display="Dynamic" ErrorMessage="Valor do campo inválido." 
                                
                                ValidationExpression="[áéíóúàâêôãõüÁÉÍÓÚÀÂÊÔÃÕÜÇçºª@&amp;\-\.\,\w\s\d\/\&quot;()?|!+=:]{1,100}"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBairroRecibo"
                                    Display="Dynamic" Enabled="True" ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr  id="linhaUF" runat="server" visible="false">
                        <td  align="left" style="width: 700px">
                            <asp:Label ID="Label10" runat="server" Text="UF*"></asp:Label>
                            <br />
                            <asp:DropDownList ID="txtUFRecibo" runat="server" CssClass="text" Width="62px" 
                                TabIndex="13">
                            </asp:DropDownList>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtUFRecibo"
                                Display="Dynamic" ErrorMessage="Valor do campo inválido." ValidationExpression="[áéíóúàâêôãõüÁÉÍÓÚÀÂÊÔÃÕÜÇçºª@&\-\.\,\w\s\d]{1,100}"></asp:RegularExpressionValidator><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtUFRecibo" Display="Dynamic"
                                    Enabled="True" ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr  id="linhaCidade" runat="server" visible="false">
                        <td  align="left" style="width: 700px">
                            <asp:Label ID="Label11" runat="server" Text="Cidade*"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtCidadeRecibo" runat="server" MaxLength="60" TabIndex="14" 
                                Width="370" CssClass="txt"></asp:TextBox><asp:RegularExpressionValidator
                                ID="RegularExpressionValidator10" runat="server" ControlToValidate="txtCidadeRecibo"
                                Display="Dynamic" ErrorMessage="Valor do campo inválido." 
                                ValidationExpression="[áéíóúàâêôãõüÁÉÍÓÚÀÂÊÔÃÕÜÇçºª@&amp;\-\.\,\w\s\d\/\&quot;()?|!+=:]{1,100}"></asp:RegularExpressionValidator><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtCidadeRecibo"
                                    Display="Dynamic" Enabled="True" ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="linhaFone" runat="server" visible="false">
                        <td  align="left" style="width: 700px">
                            <asp:Label ID="Label163" runat="server" Text="Fone*"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtFone" runat="server" CssClass="txt" MaxLength="60" 
                                TabIndex="15" Width="170px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="linhaTitulacao" runat="server" visible="false">
                        <td align="left" style="width: 700px">
                            <asp:Label ID="Label164" runat="server" 
                                Text="Titulação* (Escolha a titulação máxima do seu currículo)"></asp:Label>
                            <br />
                            <asp:DropDownList ID="txtTitulacao" runat="server" 
                                CssClass="text" TabIndex="16" Width="369px">
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
                    <tr id="linhaInstituicao" runat="server" visible="false">
                        <td align="left" style="width: 700px">
                            <asp:Label ID="Label165" runat="server" 
                                Text="Instituição* (Digite a instituição da formação mais alta)"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtInstituicao" runat="server" CssClass="txt" MaxLength="250" 
                                TabIndex="17" Width="500px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" 
                                ControlToValidate="txtInstituicao" Display="Dynamic" Enabled="True" 
                                ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator15" 
                                runat="server" ControlToValidate="txtTitulacao" Display="Dynamic" 
                                ErrorMessage="Valor do campo inválido." 
                                ValidationExpression="[áéíóúàâêôãõüÁÉÍÓÚÀÂÊÔÃÕÜÇçºª@&amp;\-\.\,\w\s\d\/\&quot;()?|!+=:]{1,100}"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr id="linhaModalidade" runat="server" visible="true">
                        <td align="left" style="width: 700px">
                            <asp:Label ID="Label166" runat="server" 
                                Text="Modalidade* (Escolha a modalidade do seu trabalho)"></asp:Label>
                            <br />
                            <asp:DropDownList ID="txtModalidade" runat="server" 
                                CssClass="text" TabIndex="18" Width="369px">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>COMUNICAÇÃO ORAL</asp:ListItem>
                                <asp:ListItem>PÔSTER</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" 
                                ControlToValidate="txtModalidade" Display="Dynamic" Enabled="True" 
                                ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="linhaAreaAtuacao" runat="server" visible="true">
                        <td align="left" style="width: 700px">
                            <asp:Label ID="Label168" runat="server" Text="Área* "></asp:Label>
                            <br />
                            <asp:DropDownList ID="txtArea" runat="server" 
                                CssClass="text" TabIndex="18" Width="369px">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>MEDICINA</asp:ListItem>
                                <asp:ListItem>ODONTOLOGIA</asp:ListItem>
                                <asp:ListItem>NUTRIÇÃO</asp:ListItem>
                                <asp:ListItem>PSICOLOGIA</asp:ListItem>
                                <asp:ListItem>EDUCAÇÃO FÍSICA</asp:ListItem>
                                <asp:ListItem>FISIOTERAPIA</asp:ListItem>
                                <asp:ListItem>TERAPIA OCUPACIONAL</asp:ListItem>
                                <asp:ListItem>FONOAUDIOLOGIA</asp:ListItem>
                                <asp:ListItem>ENFERMAGEM</asp:ListItem>
                                <asp:ListItem>FARMÁCIA</asp:ListItem>
                                <asp:ListItem>BIOMEDICINA</asp:ListItem>
                                <asp:ListItem>OUTRAS</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" 
                                ControlToValidate="txtArea" Display="Dynamic" Enabled="True" 
                                ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="linhaTituloAssunto" runat="server" visible="true">
                        <td align="left" style="width: 700px">
                            <asp:Label ID="Label169" runat="server" Text="Título do Trabalho* "></asp:Label>
                            <br />
                            <asp:TextBox ID="txtTituloTrab" runat="server" MaxLength="250" Rows="3" 
                                TabIndex="10" Width="700px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" 
                                ControlToValidate="txtTituloTrab" Display="Dynamic" Enabled="True" 
                                ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator16" 
                                runat="server" ControlToValidate="txtTituloTrab" Display="Dynamic" 
                                ErrorMessage="Valor do campo inválido." 
                                ValidationExpression="[áéíóúàâêôãõüÁÉÍÓÚÀÂÊÔÃÕÜÇçºª@&amp;\-\.\,\w\s\d\/\&quot;()?|!+=:]{1,100}"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate> 
                <table  id="Table1"  border="0" cellpadding="0" cellspacing="10" width="710" runat="server">
                    <tr>
                        <td align="left" style="width: 700px">
                            <h4 align="left">
                            <asp:Label ID="Label170" runat="server" Text="Coautores"></asp:Label></h4>
                            <br />
                            <asp:Panel ID="pnlCoautores" runat="server" Visible="true">
                                <table border="0" cellpadding="0" cellspacing="10" width="710">
                                    <tr id="linhaNomeCoautor" runat="server" visible="true">
                                        <td align="left" colspan="1" valign="top">
                                            <asp:Label ID="Label156" runat="server" Text="Nome"></asp:Label>
                                        
                                            <br />
                                        
                                            <asp:TextBox ID="txtNoCoautor" runat="server" TabIndex="18" 
                                                Width="441px" CssClass="txt_upper" MaxLength="100"></asp:TextBox>
                                            &nbsp;<asp:Button ID="btnGravarSubscritor" runat="server" CssClass="botoes" 
                                                TabIndex="20" Text="Incluir" CausesValidation="False" 
                                                onclick="btnGravarSubscritor_Click" />
                                            <asp:Button ID="btnExcluirTodosCoautores" runat="server" BackColor="Red" 
                                                CausesValidation="False" CssClass="botoes" 
                                                onclick="btnExcluirTodosCoautores_Click" TabIndex="20" 
                                                Text="Remover Todos" />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator17" 
                                                runat="server" ControlToValidate="txtNoCoautor" Display="Dynamic" 
                                                ErrorMessage="Valor do campo inválido." 
                                                ValidationExpression="[áéíóúàâêôãõüÁÉÍÓÚÀÂÊÔÃÕÜÇçºª@&amp;\-\.\,\w\s\d\/\&quot;()?|!+=:]{1,100}"></asp:RegularExpressionValidator>
                                            <br />
                                            <asp:Label ID="Label172" runat="server" 
                                                Text="*Após a inclusão de coautores deve-se gravar a proposta."></asp:Label>
                                            <br />
                                            <asp:Label ID="lblMsgCoautor" runat="server" Font-Size="Small" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left"  style="height: 24px;">
                                            <asp:GridView ID="grdSubscritor" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                ForeColor="#333333" GridLines="None" Width="707px" CellSpacing="1" 
                                                Caption="Coautores" TabIndex="22" 
                                                DataKeyNames="noParticipanteTese" onrowcommand="grdSubscritor_RowCommand" 
                                                onrowdeleting="grdSubscritor_RowDeleting">
                                                <RowStyle BackColor="#EFF3FB" />
                                                <Columns>
                                                    <asp:BoundField DataField="noParticipanteTese" HeaderText="  Nome" 
                                                        SortExpression="noParticipanteTese">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Height="22px" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:CommandField DeleteImageUrl="~/img/delete 18x18.png" 
                                                        HeaderText=" Remover " ShowDeleteButton="True" CausesValidation="False">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    </asp:CommandField>
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
                        </td>
                    </tr>
                </table> 
            </ContentTemplate>
        </asp:UpdatePanel> 
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate> 
                <table  id="Table2"  border="0" cellpadding="0" cellspacing="10" width="710" runat="server"> 
                            <tr id="linhaArquivo" runat="server" visible="true">
                                <td align="left" >
                                <asp:Label ID="Label3" runat="server" 
                                        Text="Selecione o arquivo para enviar (Somente arquivos com extensão .doc, .rtf)"></asp:Label>
                                    <br />
                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="botoes" 
                                        Width="488px" />
                                </td>
                            </tr>
                            <tr id="linhaDeAcordo" runat="server" visible="false">
                                <td align="left" >
                                
                                    <asp:CheckBox ID="chkDeAcordo" runat="server" 
                                        Text="  Li e concordo com as normas do evento" />
                                
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Button ID="btnVoltar" runat="server" CausesValidation="False" 
                                        CssClass="botoes" TabIndex="20" Text="Voltar para listagem" 
                                        PostBackUrl="~/frmTrabalhosLista.aspx" />
                                    <asp:Button ID="btnNovo" runat="server" CausesValidation="False" 
                                        CssClass="botoes" onclick="btnNovo_Click" TabIndex="20" Text="Novo" />
                                    <asp:Button ID="btnGravar" runat="server" CssClass="botoes" 
                                        onclick="btnGravar_Click" TabIndex="20" Text="Gravar e Enviar Proposta" />
                                    <asp:Button ID="btnExcluirProposta" runat="server" CssClass="botoes" 
                                        TabIndex="20" Text="Excluir Proposta" BackColor="Red" 
                                        onclick="btnExcluirProposta_Click" />
                                </td>
                            </tr>
                            <tr>
                                
                                        <td align="left" style="height: 26px;">
                                            <asp:Label ID="lblMsg" runat="server" Font-Size="Small" ForeColor="Red"></asp:Label>
                                        </td>
                                    
                            </tr>
                </table>        
            </ContentTemplate>
        </asp:UpdatePanel>    
            
    </div>
</asp:Content>
