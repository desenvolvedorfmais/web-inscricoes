<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmMeetingConvitesRecebidos.aspx.cs" Inherits="frmMeetingConvitesRecebidos" title="Inscri&ccedil;&otilde;es Web"
    ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="scripts/MSGAguarde.js" type="text/javascript"></script>

    <div id="frmCad_auto">

        <asp:UpdateProgress runat="server" id="UpdateProgress1" DisplayAfter="0" >
            <ProgressTemplate>
                
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:ModalPopupExtender ID="modalExtender" runat="server" TargetControlID="UpdateProgress1"
            PopupControlID="pnlAguarde" DropShadow="true" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>
        <asp:Panel ID="pnlAguarde" runat="server" CssClass="modalExtender">
            <div class="loading" align="center">
                    <br />&nbsp;Aguarde!... Processando sua solicitação&nbsp;&nbsp;<br />
                    <br />
                    <img src="imagensgeral/loader.gif" alt="" />
                    <br />Wait! ... Processing your request<br />
                    <br />
                </div>
        </asp:Panel>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"  EnableScriptGlobalization="true"
            EnableScriptLocalization="true" CombineScripts="false">

    </asp:ToolkitScriptManager>
    <h3>
        <asp:Label ID="lblTituloPagina" runat="server" Text="Convites Recebidos" CssClass="titulo"></asp:Label>
        <br />
    </h3>
    
    <div id="pedidos_esq">
                
        <div <%--style="float: left;"--%>>
            <asp:Label ID="lblID" runat="server" Width="130px" CssClass="label" Text="Nr Cadastro:"></asp:Label>                    
            &nbsp;<asp:Label ID="lblIdentificador" runat="server" Font-Bold="True" > </asp:Label></div>

        <div <%--style="float: left;"--%>>
            <asp:Label ID="lblPart" runat="server" Width="130px" CssClass="label" Text="Participante:"></asp:Label>                    
            &nbsp;<asp:Label ID="lblNoParticipante" runat="server" Font-Bold="True" > </asp:Label></div>

        <div <%--style="float: left;"--%>>
            <asp:Label ID="lblCateg" runat="server" Width="130px" CssClass="label" Text="Categoria:"></asp:Label>                    
            &nbsp;<asp:Label ID="lblCategoria" runat="server" Font-Bold="True" > </asp:Label></div>

    </div> 
    
        &nbsp;
    <div id="dados_perfilB2B" >

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate> 
                <div id="filtroMeeting" class="clsFiltroMeeting">
                    <h4>
                        <asp:Label ID="lblFiltro" runat="server" Width="130px" CssClass="label" Text="Filtros:"></asp:Label>
                    </h4>
                    <div id="EmpresaFiltro" class="clsEmpresaFiltro">
                        <asp:Label ID="lblEmpresFiltro" runat="server"  CssClass="label" Text="Empresa:"></asp:Label><br />
                        <asp:TextBox ID="txtEmpresaFiltro" CssClass="campoform_grande" runat="server"></asp:TextBox>
                    </div>
                    <div id="PaisFiltro" class="clsPaisFiltro">
                        <asp:Label ID="lblPaisFiltro" runat="server"  CssClass="label" Text="País:"></asp:Label><br />
                        <asp:DropDownList ID="txtPaisFiltro" CssClass="campoform_medio" runat="server"></asp:DropDownList>
                    </div>
                    <div id="AreaAtuacaoFiltro" class="clsAreaAtuacaoFiltro">
                        <asp:Label ID="lblAreaAtuacaoFiltro" runat="server"  CssClass="label" Text="Área Atuação:"></asp:Label><br />
                        <asp:DropDownList ID="txtAreaAtuacaoFiltro" CssClass="campoform_medio" runat="server"></asp:DropDownList>
                    </div>
                    
                    <div id="StatusFiltro" class="clsStatusFiltro">
                        <asp:Label ID="lblStatusFiltro" runat="server"  CssClass="label" Text="Status:"></asp:Label><br />
                        <div class="StatusFiltroChk"><asp:CheckBox ID="chkEmAberto" runat="server" Text="Em Aberto" Checked="true" Width="100px"/></div>
                        <div class="StatusFiltroChk"><asp:CheckBox ID="chkAceito" runat="server" Text="Aceito" Checked="true" Width="100px"/></div>
                        <div class="StatusFiltroChk"><asp:CheckBox ID="chkAgendado" runat="server" Text="Agendado" Width="100px" Checked="True"/></div>
                        <div class="StatusFiltroChk"><asp:CheckBox ID="chkCancelado" runat="server" Text="Cancelado" Width="100px"/></div>
                        <div class="StatusFiltroChk"><asp:CheckBox ID="chkRecusado" runat="server" Text="Recusado" Width="100px"/></div>
                    </div>                    
                    <div class="clsBtnMeeting">                                    
                        <asp:Button ID="btnFiltrar" CssClass="botoes" runat="server" Text="Filtrar" OnClick="btnFiltrar_Click"></asp:Button>                                    
                        <asp:Button ID="btnLimparFiltro" CssClass="botoes" runat="server" Text="Limpar" OnClick="btnLimparFiltro_Click"></asp:Button>                                    
                        &nbsp;&nbsp;
                    </div>
                </div>
                <asp:Label ID="txtMsg" runat="server" CssClass="lblMsg" Visible="false"></asp:Label>
                <asp:GridView ID="grdConvites" runat="server" AutoGenerateColumns="False" CellPadding="4" CellSpacing="2" GridLines="None" OnRowDataBound="grd_RowDataBound" Width="100%">
                    <RowStyle CssClass="grdlinhaimpar" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <div id="divDtConviteMeeting" class="clsDtConviteMeeting">
                                    <asp:Label ID="DtConvite" runat="server" Text="Dt Convite:"></asp:Label>
                                    <asp:Label ID="lblDtConvite" runat="server" Font-Bold="True" Text='<%# Eval("dtConvite") %>'></asp:Label>
                                </div>
                                <div id="divStatusConviteMeeting" class="clsStatusConviteMeeting">
                                    <asp:Label ID="StatusConvite" runat="server" Text="Status Convite:"></asp:Label>
                                    <asp:Label ID="lblStatusConvite" runat="server" Font-Bold="True" Text='<%# Eval("dsStatusConvite") %>'></asp:Label>
                                    &nbsp;&nbsp;
                                </div>
                                <div id="divEmpresaMeeting" class="clsEmpresaMeeting">
                                    <asp:Label ID="Empresa" runat="server" Text="Empresa:"></asp:Label>
                                    <asp:Label ID="lblEmpresa" runat="server" Font-Bold="True" Text='<%# Eval("noInstituicaoParticipanteConvite") %>'></asp:Label>
                                </div>
                                <div id="divPaisMeeting" class="clsEmpresaMeeting">
                                    <asp:Label ID="Pais" runat="server" Text="País:"></asp:Label>
                                    <asp:Label ID="lblPais" runat="server" Font-Bold="True" Text='<%# Eval("noPaisParticipanteConvite") %>'></asp:Label>
                                </div>
                                <div id="divAreaAtuacaoMeeting" class="clsAreaAtuacaoMeeting">
                                    <asp:Label ID="AreaAtuacao" runat="server" Text="Área de Atuacao:"></asp:Label>
                                    <asp:Label ID="lblAreaAtuacao" runat="server" Font-Bold="True" Text='<%# Eval("noAreaAtuacaoParticipanteConvite") %>'></asp:Label>
                                    &nbsp;&nbsp;
                                </div>
                                <div id="divRepresentanteMeeting" class="clsRepresentanteMeeting">
                                    <asp:Label ID="Representante" runat="server" Text="Representante:"></asp:Label>
                                    <asp:Label ID="lblRepresentante" runat="server" Font-Bold="True" Text='<%# Eval("noParticipanteConvite") %>'></asp:Label>
                                </div>
                                <div id="divBtnMeeting" class="clsBtnMeeting">                                    
                                    <asp:Label ID="lblCdConvite" runat="server" Visible="False" Text='<%# Eval("cdConvite") %>'></asp:Label>
                                    <asp:Button ID="btnMaisInf" CssClass="botoes" runat="server" Text="Detalhar..."></asp:Button>
                                    &nbsp;&nbsp;
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="cdConvite" HeaderText="Convite" Visible="False">
                            <HeaderStyle CssClass="td_meuspedidos" HorizontalAlign="Left" />
                            <ItemStyle CssClass="td_meuspedidos" HorizontalAlign="Center" Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="dtConvite" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Dt Convite" Visible="False">
                            <HeaderStyle CssClass="td_meuspedidos" HorizontalAlign="Left" />
                            <ItemStyle CssClass="td_meuspedidos" HorizontalAlign="Center" Width="70px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="noInstituicaoParticipanteConvite" HeaderText="Empresa" Visible="False">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="75px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="noPaisParticipanteConvite" HeaderText="País" Visible="False" />
                        <asp:BoundField DataField="noAreaAtuacaoParticipanteConvite" HeaderText="Área de Atuação" Visible="False">
                            <HeaderStyle CssClass="td_meuspedidos" />
                            <ItemStyle CssClass="td_meuspedidos" HorizontalAlign="Right" Width="68px" />
                            <HeaderStyle CssClass="td_meuspedidos" HorizontalAlign="Left" />
                            <ItemStyle CssClass="td_meuspedidos" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="noParticipanteConvite" HeaderText="Representante" Visible="False">
                            <ItemStyle CssClass="td_meuspedidos" HorizontalAlign="Center" Width="55px" />
                            <HeaderStyle CssClass="td_meuspedidos" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="dsStatusConvite" HeaderText="Status" Visible="False">
                            <HeaderStyle CssClass="td_meuspedidos" />
                            <ItemStyle CssClass="td_meuspedidos" HorizontalAlign="Center" VerticalAlign="Middle" Width="15px" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle CssClass="grdHeader" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle CssClass="grdlinhapar" />
                </asp:GridView>
                <br />
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
    
    </div>    
</div>

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

