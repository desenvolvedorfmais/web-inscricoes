<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmAcompanhante.aspx.cs" Inherits="frmAcompanhante" Title="Inscri&ccedil;&otilde;es Web"
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
            Cadastro de Outros Participantes</h3>
        <br />
        <br />
        <div>
            <asp:Label ID="Label4" runat="server" Text="ID:" Width="100px" CssClass="label"></asp:Label>
            <asp:Label ID="lblIdentificador" runat="server" ForeColor="Navy" Font-Bold="True"></asp:Label>
        </div>
        <div>
            <asp:Label ID="Label13" runat="server" Text="Participante" Width="100px" CssClass="label"></asp:Label>
            <asp:Label ID="lblNoParticipante" runat="server" ForeColor="Navy" Font-Bold="True"></asp:Label>
        </div>
        <div>
            <asp:Label ID="Label22" runat="server" Text="Categoria" Width="100px" CssClass="label"></asp:Label>
            <asp:Label ID="lblCategoria" runat="server" ForeColor="Navy" Font-Bold="True"></asp:Label>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <br />
                <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                <br />
                <div id="CadastoAcompanhante">
                    <div id="TituloCadastroAcomp">
                        
                            <asp:Label ID="lblTotalInscr" runat="server" Text="O Participante deve inscrever mais &quot;#total#&quot; Participante(s)"
                                Font-Bold="True" Font-Size="Small"></asp:Label>
                            <br />
                            &nbsp;
                            <br />
                        
                    </div>
                    <div id="idAcomp">                        
                        <asp:Label ID="Label25" runat="server" Text="Id" CssClass="lblTitulocampo"></asp:Label>
                        <br/>
                        <asp:Label ID="lblIdAcopanhante" runat="server" CssClass="titulo2"></asp:Label>                        
                    </div>
                    <div id="cpfAcomp">
                        <asp:Label ID="Label26" runat="server" Text="CPF" CssClass="lblTitulocampo"></asp:Label>
                        <br/>
                        <asp:TextBox ID="TXTDsCPF" runat="server" CssClass="text campoform_pequeno" MaxLength="14" 
                                onkeypress="return Mascarar(this, event, '999.999.999-99')" ReadOnly="false" 
                                TabIndex="1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVCPF" runat="server" 
                                ControlToValidate="TXTDsCPF" Display="Dynamic" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>
                    </div>
                    <div id="nomeAcomp">
                        <asp:Label ID="Label23" runat="server" Text="Nome" CssClass="lblTitulocampo"></asp:Label>
                        <br/>
                        <asp:TextBox ID="txtNome" runat="server" MaxLength="100" 
                                CssClass="campoform_grande maiusculo" TabIndex="2"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNome"
                                Display="Dynamic" ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtNome"
                                Display="Dynamic" ErrorMessage="Valor inválido para o campo." ValidationExpression="[áéíóúàâêôãõüÁÉÍÓÚÀÂÊÔÃÕÜÇçºª@&amp;\-\.\,\w\s\d]{1,100}"></asp:RegularExpressionValidator>
                    </div>
                    <div id="nomeEtiquetaAcomp">
                        <asp:Label ID="Label24" runat="server" Text="Nome da Credencial" CssClass="lblTitulocampo"></asp:Label>
                        <br/>
                        <asp:TextBox ID="txtNomeEtiqueta" runat="server" MaxLength="50" 
                                CssClass="campoform_grande maiusculo" TabIndex="3"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNomeEtiqueta"
                                Display="Dynamic" ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtNomeEtiqueta"
                                Display="Dynamic" ErrorMessage="Valor inválido para o campo." ValidationExpression="[áéíóúàâêôãõüÁÉÍÓÚÀÂÊÔÃÕÜÇçºª@&amp;\-\.\,\w\s\d]{1,100}"></asp:RegularExpressionValidator>
                        
                    </div>
                    <div id="btnsAcomp">
                        <br />
                        
                            <asp:Button ID="btnNovo" runat="server" CausesValidation="False" CssClass="botoes"
                                Font-Bold="False" TabIndex="4" Text="Novo" Width="112px" 
                                OnClick="btnNovo_Click" />
                            &nbsp;<asp:Button ID="btnConfirmar" runat="server"
                                CssClass="botoes" TabIndex="5" Text="Gravar" Width="112px"
                                OnClick="btnConfirmar_Click" />
                            &nbsp;<asp:Button ID="btnContinuar" runat="server" 
                                CausesValidation="False" CssClass="botoesDestaque" 
                                OnClick="btnContinuar_Click" TabIndex="6" Text="Continuar" Visible="False" 
                                Width="112px" />
                            <br />
                            <br />
                        
                    </div>
                    <div>
                        
                            <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                CellSpacing="2" DataKeyNames="cdAcompanhante" GridLines="None" OnSelectedIndexChanging="grd_SelectedIndexChanging"
                                Width="100%" TabIndex="7">
                                <RowStyle CssClass="grdlinhaimpar" />
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" SelectText="Alterar">
                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                    </asp:CommandField>
                                    <asp:BoundField DataField="cdAcompanhante" HeaderText="cdAcompanhante" Visible="False">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="dsAcompanhante" HeaderText="Nome" SortExpression="dsAcompanhante">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="noEtiqueta" HeaderText="Nome Etiqueta"  Visible="False" SortExpression="noEtiqueta">
                                        <HeaderStyle CssClass="td_meuspedidos" HorizontalAlign="Left" />
                                        <ItemStyle CssClass="td_meuspedidos" HorizontalAlign="Left" Width="300px" />
                                    </asp:BoundField>
                                </Columns>
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle CssClass="grdlinhaimpar" />
                                <AlternatingRowStyle CssClass="grdlinhapar" />
                            </asp:GridView>
                        
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <br />

</asp:Content>
