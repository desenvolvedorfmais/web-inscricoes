<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmMeetingConviteRec.aspx.cs" Inherits="frmMeetingConviteRec" title="Inscri&ccedil;&otilde;es Web"
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
        <asp:Label ID="lblTituloPagina" runat="server" Text="Convite Recebido" CssClass="titulo"></asp:Label>
        <br />
    </h3>
    <asp:ImageButton ID="btnFoco" runat="server" ImageUrl="~/img/vazio.png" />
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate> 
            <asp:Label ID="txtMsg" runat="server" CssClass="lblMsg" Visible="false"></asp:Label>
                    
            <div id="pedidos_esq">
                <asp:Panel ID="pnlDadosConvite" runat="server">      
                    <div style="clear: both;">
                        <div id="divDtConviteMeeting" class="clsDtConviteMeeting">
                            <asp:Label ID="DtConvite" runat="server" Width="130px" CssClass="label" Text="Dt Convite:"></asp:Label>
                            <asp:Label ID="lblDtConvite" runat="server" Font-Bold="True"></asp:Label>
                        </div>
                        <div id="divStatusConviteMeeting" class="clsStatusConviteMeeting">
                            <asp:Label ID="StatusConvite" runat="server" Width="130px" CssClass="label" Text="Status Convite:"></asp:Label>
                            <asp:Label ID="lblStatusConvite" runat="server" Font-Bold="True" Text='<%# Eval("dsStatusConvite") %>'></asp:Label>
                            &nbsp;&nbsp;
                        </div>
                    </div>
                    <div style="clear: both;">
                        <asp:Label ID="lblID" runat="server" Width="130px" CssClass="label" Text="Nr Convite:"></asp:Label>                    
                        &nbsp;<asp:Label ID="lblIdentificador" runat="server" Font-Bold="True"> </asp:Label></div>

                    <div <%--style="float: left;"--%>>
                        <asp:Label ID="lblEmpr" runat="server" Width="130px" CssClass="label" Text="Intituição/Empresa:"></asp:Label>                    
                        &nbsp;<asp:Label ID="lblEmpresa" runat="server" Font-Bold="True"> </asp:Label></div>

                    <div <%--style="float: left;"--%>>
                        <asp:Label ID="lbl_pais" runat="server" Width="130px" CssClass="label" Text="País:"></asp:Label>                    
                        &nbsp;<asp:Label ID="lblNoPais" runat="server" Font-Bold="True"> </asp:Label></div>

                    <div <%--style="float: left;"--%>>
                        <asp:Label ID="lblArea" runat="server" Width="130px" CssClass="label" Text="Área de Atuação:"></asp:Label>                    
                        &nbsp;<asp:Label ID="lblAreaAtuacao" runat="server" Font-Bold="True"> </asp:Label></div>

                    <div <%--style="float: left;"--%>>
                        <asp:Label ID="lblSite" runat="server" Width="130px" CssClass="label" Text="Website:"></asp:Label>                    
                        &nbsp;<asp:Label ID="lblWebSite" runat="server" Font-Bold="True"> </asp:Label></div>

                    <div <%--style="float: left;"--%>>
                        <asp:Label ID="lblPart" runat="server" Width="130px" CssClass="label" Text="Representante:"></asp:Label>                    
                        &nbsp;<asp:Label ID="lblNoParticipante" runat="server" Font-Bold="True"> </asp:Label></div>

                    <div <%--style="float: left;"--%>>
                        <%--<asp:Label ID="lblCargo" runat="server" Width="130px" CssClass="label" Text="Cargo:"></asp:Label>                    
                        &nbsp;<asp:Label ID="lblNoCargo" runat="server" Font-Bold="True" ForeColor="Navy"> </asp:Label>--%>

                    </div>
                </asp:Panel> 
            </div> 

            <div id="dados_perfilB2B" >
                <h4>
                    <asp:Label ID="lblInstrucoes" runat="server" Text="Detalhes sobre o perfil de negócios da empresa:"></asp:Label><br /><br />
                </h4>
                <div id="perfilB2B" class="clsperfilB2B">
                    <asp:Label ID="lblPerfilB2B" runat="server" Font-Bold="True"> </asp:Label>
                </div>
                &nbsp;
    
            </div>   
            <div id="SugerirHorario" class="clsSugerirHorario">
                <asp:Panel ID="pnlSugerirHorario" runat="server" Visible="false">
                    <asp:Panel ID="pnlSelHorario" runat="server">
                        <h4>
                            <asp:Label ID="lblInstrucoesSugerirHorario" runat="server" Text="Aceite ou informe os horários que você está disponível (você pode escolher mais que um) :"></asp:Label><br /><br />
                        </h4>                    
                        <div class="StatusFiltroChk">
                            <asp:Label ID="lblData" runat="server" Width="130px" CssClass="label" Text="Data:"></asp:Label><br />
                            <asp:DropDownList ID="txtData" runat="server" AutoPostBack="True" OnSelectedIndexChanged="txtData_SelectedIndexChanged" Width="120px"></asp:DropDownList>
                        </div>
                        <div class="StatusFiltroChk">                        
                            <asp:Label ID="lblHora" runat="server" Width="130px" CssClass="label" Text="Hora:"></asp:Label><br />
                            <asp:DropDownList ID="txtHora" runat="server" Width="100px"></asp:DropDownList>
                            &nbsp;<asp:Button ID="btnIncluirHorario" runat="server" Text="OK" CssClass="botoes" OnClick="btnIncluirHorario_Click"  />                        
                        </div>
                    </asp:Panel>
                    
                    <div id="gridHorarios" style="clear: both;">                        
                        <asp:Label ID="lblMsg" runat="server" CssClass="lblMsg" Visible="false"></asp:Label>
                        
                        <div id="grdHorarioConvidado" class="clsperfilGridHorarios">
                            <br />&nbsp;
                            <asp:GridView ID="grdConvidado" runat="server" AutoGenerateColumns="False" Caption="Sugeridos pelo Representante" CellPadding="4" CellSpacing="2" GridLines="None" OnRowCommand="grdConvidado_RowCommand" OnRowDataBound="grdgrdConvidado_RowDataBound">
                                <RowStyle CssClass="grdlinhaimpar" />
                                <Columns>
                                    <asp:BoundField DataField="dsDataHoraSugestao" DataFormatString="{0:dd/MM/yyyy HH:mm}" HeaderText="Horário">
                                        <HeaderStyle Width="160px" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="160px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cdConvite" HeaderText="Convite" Visible="False">
                                        <HeaderStyle CssClass="td_meuspedidos" HorizontalAlign="Left" Width="130px" />
                                        <ItemStyle CssClass="td_meuspedidos" HorizontalAlign="Center" Width="130px" />
                                    </asp:BoundField>
                                    <asp:ButtonField ButtonType="Image" CommandName="Selecionar" HeaderText="Aceitar" ImageUrl="~/img/accept18x18.png" Text="Aceitar">
                                        <HeaderStyle Width="75px" />
                                        <ItemStyle HorizontalAlign="Center" Width="75px" />
                                    </asp:ButtonField>
                                </Columns>
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle CssClass="grdHeader" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle CssClass="grdlinhapar" />
                            </asp:GridView>
                        </div>
                        <div id="grdHorarioConvite" class="clsperfilGridHorarios">
                            <br />&nbsp;
                            <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="4" CellSpacing="2" GridLines="None" Caption="Sugeridos por Você" OnRowCommand="grd_RowCommand" OnRowDataBound="grd_RowDataBound">
                                <RowStyle CssClass="grdlinhaimpar" />
                                <Columns>
                                    <asp:BoundField DataField="dsDataHoraSugestao" HeaderText="Horário" DataFormatString="{0:dd/MM/yyyy HH:mm}">
                                        <HeaderStyle Width="160px" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="160px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cdConvite" HeaderText="Convite" Visible="False">
                                    <HeaderStyle CssClass="td_meuspedidos" HorizontalAlign="Left" Width="130px" />
                                    <ItemStyle HorizontalAlign="Center" Width="130px" CssClass="td_meuspedidos" />
                                    </asp:BoundField>
                                    <asp:ButtonField ButtonType="Image" CommandName="Remover" HeaderText="Excluir" ImageUrl="~/img/delete 18x18.png" Text="Button">
                                    <HeaderStyle Width="75px" />
                                    <ItemStyle HorizontalAlign="Center" Width="75px" />
                                    </asp:ButtonField>
                                    <asp:ButtonField ButtonType="Image" CommandName="Selecionar" HeaderText="Selecionar" ImageUrl="~/img/accept18x18.png" Text="Selecionar" Visible="False">
                                        <HeaderStyle Width="80px" />
                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                                    </asp:ButtonField>
                                </Columns>
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle CssClass="grdHeader" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle CssClass="grdlinhapar" />
                            </asp:GridView>
                        </div>

                    </div>
                    
                </asp:Panel>
            </div>
            <br />
            <div style="clear: both;">
                &nbsp;<br />
                <asp:Button ID="btnVoltar" runat="server" Text="Voltar" CssClass="botoes" PostBackUrl="~/frmMeetingConvitesRecebidos.aspx" />    
    
                <asp:Button ID="btnEnviar" runat="server" Text="Aceitar Convite" CssClass="botoes" OnClick="btnEnviar_Click" />    
    
                <asp:Button ID="btnCancelar" runat="server" Text="Recusar Convite" CssClass="botoesDestaque" OnClick="btnCancelar_Click" />    
    
            </div>    
            <br /><br /> &nbsp;
        </ContentTemplate>
    </asp:UpdatePanel>
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

