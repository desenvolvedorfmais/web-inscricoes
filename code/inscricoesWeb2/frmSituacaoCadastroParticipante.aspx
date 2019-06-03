<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmSituacaoCadastroParticipante.aspx.cs" Inherits="frmSituacaoCadastroParticipante" Title="Inscri&ccedil;&otilde;es Web"
    EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="TSManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" CombineScripts="false">
    </asp:ToolkitScriptManager>
    <div id="frmCad_auto">
        <h3>            
            <asp:Label ID="lblTituloPagina" runat="server" Text="Verificar Situação do Cadastro do Participantes" CssClass="titulo"></asp:Label><br />
        </h3>
                
        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <br />
                <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                <br />
                <table style="width: 750px;" cellspacing="2">
                    <tr>
                        <td align="right" style="width: 150px">
                            <asp:Label ID="Label26" runat="server" Text="CPF"></asp:Label>
                        </td>
                        <td align="left" style="width: 600px">
                            <asp:TextBox ID="TXTDsCPF" runat="server" CssClass="text" MaxLength="14" 
                                onkeypress="return Mascarar(this, event, '999.999.999-99')" ReadOnly="false" 
                                TabIndex="1"></asp:TextBox>
                            <asp:Button ID="btnPesquisar" runat="server" CausesValidation="False" CssClass="botoes" Font-Bold="False" OnClick="btnPesquisar_Click" TabIndex="4" Text="Pesquisar" Width="112px" />
                            <asp:Button ID="btnLimpar" runat="server" CssClass="botoes" OnClick="btnLimpar_Click" TabIndex="5" Text="Limpar" Width="112px" />
                            <asp:RequiredFieldValidator ID="RFVCPF" runat="server" 
                                ControlToValidate="TXTDsCPF" Display="Dynamic" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 150px">
                            <asp:Label ID="Label25" runat="server" Text="Id"></asp:Label>
                        </td>
                        <td align="left" style="width: 600px">
                            <asp:Label ID="lblIdentificador" runat="server" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 150px; height: 18px;" valign="middle">
                            <asp:Label ID="Label23" runat="server" Text="Nome"></asp:Label>
                        </td>
                        <td align="left" style="width: 600px; height: 18px;">
                            <asp:Label ID="lblNoParticipante" runat="server" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 150px">
                            Categoria</td>
                        <td align="left" style="width: 600px">
                            <asp:Label ID="lblNoCategoria" runat="server" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 150px">
                            Situação
                        </td>
                                          </td><td style="width: 600px">
                            <asp:Label ID="lblCadastroAtivo" runat="server" Visible="False"  
                        style="vertical-align:middle" align="center"
                        Font-Bold="True" Font-Size="Large" Height="26px" Width="386px" 
                        ForeColor="White">CADASTRO ATIVO</asp:Label><br />
                            <br />
      
                    </tr>
                    <tr>
                        <td align="right" style="width: 150px">
                            Confirmação</td>
                        <td>
                            <asp:Label ID="lblCadastroConfirmado" runat="server" Visible="False"  
                        style="vertical-align:middle" align="center"
                        Font-Bold="True" Font-Size="Large" Height="26px" Width="386px" 
                        ForeColor="White">CADASTRO CONFIRMADO</asp:Label>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 150px">Forma de Pagamento</td>
                        <td>
                            <asp:Label ID="lblFormaPgto" runat="server" ></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <br />

</asp:Content>
