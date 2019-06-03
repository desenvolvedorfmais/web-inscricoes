<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmPesquisaVinculadaAbertura.aspx.cs" Inherits="frmPesquisaVinculadaAbertura" 
    Title="Inscri&ccedil;&otilde;es Web"  EnableEventValidation="false" ValidateRequest="false"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="scripts/MSGAguarde.js" type="text/javascript"></script>
    <div id="frmCad_auto">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
            EnableScriptLocalization="true" CombineScripts="false">
        </asp:ToolkitScriptManager>
        <h3>
            <asp:Label ID="Label1" runat="server" Text="Pesquisa" CssClass="titulo"></asp:Label></h3>
        <br />
        <br />
        <div id="MensagemChamada">
        
            <center>
                <br />
                <asp:Label ID="lblTitulo" runat="server" Text="Prêmio IEL de Estágio"></asp:Label>
                <br />
                <br />
                <br />
                <asp:Label ID="lblChamada1" runat="server" 
                    Text="Participe da nossa pesquisa de avaliação."></asp:Label>
                <br />
                <asp:Label ID="lblChamada2" runat="server" 
                    Text="Sua opinião é muito importante para nós."></asp:Label>
                <br />
                <br />
                <asp:Label ID="lblChamada3" runat="server" visible="false"
                    Text=""></asp:Label>
                <br />
                <br />
                <asp:Label ID="Label2" runat="server" Text="Informe seu CPF: " CssClass="lbl"></asp:Label>
                <br />
                                    <asp:TextBox ID="TXTDsCPF" MaxLength="14" runat="server" 
                    CssClass="txt_upper" onkeypress="return Mascarar(this, event, '999.999.999-99')"
                                        ReadOnly="false" TabIndex="1" Width="132px"></asp:TextBox>
                                    <br />
                                    <asp:RequiredFieldValidator
                                            ControlToValidate="TXTDsCPF" Display="Dynamic" ErrorMessage="Campo requerido"
                                            ID="RFVCPF" runat="server" CssClass="lbl"></asp:RequiredFieldValidator>
                <br />
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
        
                            <asp:Label ID="txtMsg" runat="server" ForeColor="Red" CssClass="lblErro"></asp:Label>
            
                    </ContentTemplate>
                </asp:UpdatePanel> 
                <br />
                <asp:Button ID="btnContinuarPesquisaVinculada" runat="server" CssClass="botoes" 
                    Text="Continuar" Visible="true" 
                    onclick="btnContinuar_Click" Height="68px" Width="208px" 
                    ViewStateMode="Enabled" />
                    
                <asp:Button ID="btnLimparPesquisaVinculada" runat="server" CssClass="botoes" 
                    Text="Limpar" Visible="true" 
                    onclick="btnLimpar_Click" Height="68px" Width="208px" 
                    ViewStateMode="Enabled" />
                    
                <br />
                <br />
            </center>                
                
                <div id="telacad_info">
                    <asp:Label ID="lblInstrucoesRapidas" runat="server" Visible="False"></asp:Label>
                    </div>

            <br />
               
        
        </div>
    </div>



    </div>



</asp:Content>

