<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmMeetingConviteAgen.aspx.cs" Inherits="frmMeetingConviteAgen" title="Inscri&ccedil;&otilde;es Web"
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
        <asp:Label ID="lblTituloPagina" runat="server" Text="Detalhe da Agenda" CssClass="titulo"></asp:Label>
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
                            <asp:Label ID="DtConvite" runat="server" Width="130px" CssClass="label" Text="Dt Agenda:"></asp:Label>
                            <asp:Label ID="lblDtConvite" runat="server" Font-Bold="True"></asp:Label>
                        </div>
                        <div id="divLocalConviteMeeting" class="clsDtConviteMeeting">
                            <asp:Label ID="Mesa" runat="server" Width="130px" CssClass="label" Text="Local:"></asp:Label>
                            <asp:Label ID="lblMesa" runat="server" Font-Bold="True"></asp:Label>
                        </div>
                        <div id="divStatusConviteMeeting" class="clsStatusConviteMeeting">
                            <asp:Label ID="StatusConvite" runat="server" Width="130px" CssClass="label" Text="Tipo Convite:"></asp:Label>
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
            <br />
            <div style="clear: both;">
                &nbsp;<br />
                <asp:Button ID="btnVoltar" runat="server" Text="Voltar" CssClass="botoes" PostBackUrl="~/frmMeetingAgenda.aspx" />    
    
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

