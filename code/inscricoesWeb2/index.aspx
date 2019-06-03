<%@ Page Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true"
    CodeFile="index.aspx.cs" Inherits="index"  %>

<%@ Register Assembly="ControlMessageBox" Namespace="ControlMessageBox" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptLocalization="true"
        CombineScripts="false">
    </asp:ToolkitScriptManager>
    <div id="frmCad" class="mylogin">

        <%--<div id="divProgress" class="modalPopUpBackground" align="center">
            <div id="loading" class="modalExtender">
                    <br />Aguarde!... Processando sua solicitação. <br />
                    <br />
                    <img src="imagensgeral/loader.gif" alt="" />
            </div>
        </div>--%>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="conteudoTopo">
                    <div id="telacad_direita">
                        <asp:Panel ID="tela_cad_direita" runat="server">                            
                            <h3>
                                <asp:Label ID="lblTiuloTelaDir" runat="server" Text="Inscreva-se" CssClass="titulo"></asp:Label>
                            </h3>
                            <div class="imgCadIndex">
                                &nbsp;
                            </div>
                            <div class="msgCadIndex">
                                <p>
                                    <asp:Label ID="lblMsgCadIndex" runat="server" Text="Faça o seu cadastro para inscrever-se no  I Congresso Internacional de Neurociência e Reabilitação" CssClass="clsMsgCadIndex"></asp:Label>
                                </p>
                            </div>
                            <div id="cadastro" >
                                <div id="linhaCadastrar" runat="server" class="clslinhaCadastrar">
                            
                                        <asp:Label ID="lblChaveLiberacao0" runat="server" Text="Sem Chave de Libera&amp;ccedil;&amp;atilde;o!"
                                            Visible="False"></asp:Label>
                                        <br />
                                        <asp:Button ID="btnCadastrar" runat="server" CausesValidation="False" CssClass="botoes"
                                            OnClick="btnCadastrar_Click" TabIndex="1" Text="Quero me Inscrever" />
                                        <br />
                            
                                </div>
                                <div id="linhaChaveLiberacao" runat="server" visible="false" class="clslinhaChaveLiberacao">
                                    <asp:Panel ID="pnlChaveLiberacao" runat="server" Visible="False">
                                                <div>
                                                    <asp:Label ID="lblChaveLiberacao" runat="server" 
                                                            Text="Caso possua, informe aqui sua<br />&quot;Chave de Libera&amp;ccedil;&amp;atilde;o&quot;"></asp:Label>                                            
                                                </div>
                                                <div>                                            
                                                    <asp:TextBox ID="txtChaveLiberacao" runat="server" CssClass="txt" MaxLength="50"
                                                        Style="text-align: center" TabIndex="2" Width="200px"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Button ID="btnCadastrar3" runat="server" CausesValidation="False" CssClass="botoes"
                                                        OnClick="btnCadastrar3_Click" TabIndex="3" Text="Quero me Inscrever" />
                                            
                                                </div>
                                    
                                            <br />
                                            <asp:Label ID="lblMsg2" runat="server" ForeColor="Red"></asp:Label>
                                            <br />
                                        </asp:Panel>
                            
                                </div>
                                <div id="linhahlnk_Documentos" runat="server" visible="false" class="clslinhahlnk_Documentos"> 
                           
                                    <asp:HyperLink ID="hlnk_Documentos" runat="server" CssClass="links"
                                        NavigateUrl="~/frmDocumentos.aspx" Target="_blank" Text="<br />ATEN&#199;&#195;O<br /><br />Clique aqui para acessar os documentos para instru&#231;&#227;o do processo de EMPENHO."></asp:HyperLink>
                                    <br /><br /><br />
                                                
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <div id="telacad_esquerda">
                        <asp:Panel ID="tela_cad_esquerda" runat="server" DefaultButton="btn_Login">
                            <h3>
                                <asp:Label ID="lblTituloTelaEsq" runat="server" Text="J&aacute; sou cadastrado" CssClass="titulo"></asp:Label>
                            </h3>   
                            <h2 class="msgJaCad">
                                <asp:Label ID="lblMsgJaCad" runat="server" Text="Gerencie as suas inscrições em todas as atividades do I Congresso Internacional de Neurociência e Reabilitação" CssClass="clsMsgJaCad"></asp:Label>
                            <h2>
                            <asp:Label ID="txtMsg" runat="server" ForeColor="Red"></asp:Label>
                            <div id="login">
                                <div id="contalogin" class="campologin">
                                
                                        <asp:Label ID="LBLCONTA" runat="server" Text="CPF"></asp:Label><asp:Label ID="lblEmail"
                                            runat="server" Text="E-Mail" Visible="False"></asp:Label>                                
                                
                                        <asp:TextBox ID="TXTDsCPF" MaxLength="14" runat="server" CssClass="txt" onkeypress="return Mascarar(this, event, '999.999.999-99')"
                                            ReadOnly="false" TabIndex="4"  placeholder="Digite seu cpf cadastrado"></asp:TextBox>
                                        <asp:TextBox ID="txtEmail_login" runat="server" CssClass="email" MaxLength="100" TabIndex="5"
                                            ReadOnly="false" Width="274px" Visible="False" placeholder="Digite seu e-mail cadastrado"></asp:TextBox>
                                        <br />
                                        <asp:RequiredFieldValidator
                                                ControlToValidate="TXTDsCPF" Display="Dynamic" ErrorMessage="Campo requerido"
                                                ID="RFVCPF" runat="server"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                    ID="revTxtEmail" runat="server" ControlToValidate="txtEmail_login" Display="Dynamic"
                                                    ErrorMessage="E-mail inválido" ValidationExpression="[áéíóúàâêôãõüÁÉÍÓÚÀÂÊÔÃÕÜÇçºª@&\-\.\,\w\s\d]{1,100}"
                                                    Visible="False"></asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="rfvTxtEmail"
                                                        runat="server" ControlToValidate="txtEmail_login" Display="Dynamic" ErrorMessage="Campo requerido"
                                                        Visible="False"></asp:RequiredFieldValidator>
                                
                                </div>
                                <div id="senhalogin" class="campologin">
                                    <asp:Label ID="LBLSENHA" runat="server" Text="Senha"></asp:Label>
                                    <asp:TextBox ID="txtSenha" runat="server" CssClass="senha" MaxLength="20" TextMode="Password"
                                            TabIndex="6" placeholder="Digite sua senha"></asp:TextBox>
                                    <br />
                                    <asp:RegularExpressionValidator ID="REVTxtSenha" runat="server"
                                                ControlToValidate="txtSenha" ErrorMessage="Senha inválida" ValidationExpression="[áéíóúàâêôãõüÁÉÍÓÚÀÂÊÔÃÕÜÇçºª@&\-\.\,\w\s\d]{1,100}"
                                                Display="Dynamic"></asp:RegularExpressionValidator>
                                
                                </div>
                                <div id="linklogin">                                
                                    <br />    
                                    <asp:Button ID="btn_Login" runat="server" CssClass="botoes" OnClick="btnLogin_Click1" OnClientClick="AtivaDivProgress()"
                                            Text="Entrar" TabIndex="7" />&nbsp; &nbsp;<br />
                                        <br />
                                    <span class="clsLembrarSenha">
                                        <asp:LinkButton ID="btnLembrarSenha" runat="server" PostBackUrl="~/frmLembrarSenha.aspx" CssClass="links"
                                            CausesValidation="False" TabIndex="8">Esqueci minha senha</asp:LinkButton>
                                    </span>
                                
                                </div>
                                <div>                                
                                    <asp:Button ID="btnCadastrar2" runat="server" CssClass="botoes" OnClick="btnCadastrar_Click"
                                        Text="Quero me Inscrever" CausesValidation="False" TabIndex="7" Visible="False" />                                
                                </div>
                                
                            </div>
                        </asp:Panel>
                </div>
                    <div id="linkboleto" runat="server" visible="false" class="link_boleto">
                            <%--<asp:HyperLink ID="lnk2aViaBoleto" runat="server" Font-Bold="False" Target="_self"  CssClass="links"
                                    Text="Imprima seu boleto" 
                                    Visible="False"></asp:HyperLink>--%>
                        <div id="div_lblInfoBoleto">
                            <asp:Label ID="lblInfoBoleto" runat="server" Text="Deseja reimprimir o seu boleto?" CssClass="title"></asp:Label>
                        </div>
                        <div id="div_lnk2aViaBoleto">
                            <asp:Button ID="lnk2aViaBoleto" runat="server" CssClass="botoesBoleto" 
                                Text="Reimprimir Boleto" CausesValidation="False" TabIndex="7" /> 
                        </div>
                                
                    </div>
                    
                    <br />
                </div>
                
                <div id="telacad_info">
                    <asp:Label ID="lblInstrucoesRapidas" runat="server"></asp:Label>
                </div>

                
            </ContentTemplate>
        </asp:UpdatePanel>

        <script type="text/javascript" language="javascript">

            function AtivaDivProgress() {
                document.getElementById("divProgress").style.display = "block";
            }

        </script>
    </div>
</asp:Content>
