<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmVerificaCPFCadastro.aspx.cs" 
Inherits="frmVerificaCPFCadastro" Title="Inscri&ccedil;&otilde;es Web"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="frmCad_auto">
        
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
            EnableScriptLocalization="true" CombineScripts="false">
        </asp:ToolkitScriptManager>
        
        <%--<asp:UpdateProgress runat="server" id="UpdateProgress1" DisplayAfter="0"  >
            <ProgressTemplate>
                
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:ModalPopupExtender ID="modalExtender" runat="server" TargetControlID="UpdateProgress1"
            PopupControlID="pnlAguarde" DropShadow="true" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>
        <asp:Panel ID="pnlAguarde" runat="server" CssClass="modalExtender">
            <div class="loading" align="center">
                    <br />Aguarde!... Processando sua solicitação. <br />
                    <br />
                    <img src="imagensgeral/loader.gif" alt="" />
             </div>
        </asp:Panel>--%>
        
        

        

        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
                <div id="cpfCaptcha" class="cpfReceitaCaptcha" >
                    <div ID="tbCPFPesquisa" runat="server">
                        <div ID="linCPF" runat="server">
                            
                                <asp:Label ID="lblInstucoes" runat="server" CssClass="label" Text=""></asp:Label>
                                <br />
                            <div id="divCPF" runat="server"  class="linha_campo">
                                <asp:Label ID="lblCPF" runat="server" CssClass="lblTitulocampo" Text="Informe o CPF"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtCPF" runat="server" Width="171px" CssClass="txt" MaxLength="14" onkeypress="return CPFMascarar(this, event)"></asp:TextBox>
                                 <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="txtCPF" ErrorMessage="Campo obrigatório." OnServerValidate="CustomValidator2_ServerValidate"></asp:CustomValidator>
                                
                                <asp:RequiredFieldValidator ID="RFVCPF" runat="server" ControlToValidate="txtCPF" Display="Dynamic" ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                            </div>
                            <div id="divEmail" runat="server"  class="linha_campo">
                                <asp:Label ID="lblEmail_Ou" runat="server" Text="-- Ou -- " Visible="False" CssClass="lblTitulocampo"></asp:Label>
                                <asp:Label ID="lblEmail" runat="server" Text="Informe o E-Mail" Visible="False" CssClass="lblTitulocampo"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtEmail_verif_cpf" runat="server" CssClass="email" MaxLength="100" ReadOnly="false" TabIndex="5" Visible="False" Width="274px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revTxtEmail" runat="server" ControlToValidate="txtEmail_verif_cpf" Display="Dynamic" ErrorMessage="E-mail inválido" ValidationExpression="[áéíóúàâêôãõüÁÉÍÓÚÀÂÊÔÃÕÜÇçºª@&amp;\-\.\,\w\s\d]{1,100}" Visible="False"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="rfvTxtEmail" runat="server" ControlToValidate="txtEmail_verif_cpf" Display="Dynamic" ErrorMessage="Campo requerido" Visible="False"></asp:RequiredFieldValidator>
                            </div>
                            <div id="divNrInscr" runat="server"  class="linha_campo">
                                <asp:Label ID="lblIdInscricao_Ou" runat="server" Text="-- Ou -- " Visible="False" CssClass="lblTitulocampo"></asp:Label>
                                <asp:Label ID="lblIdInscricao" runat="server" Text="Informe o seu número de inscrição" Visible="False" CssClass="lblTitulocampo"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtIDInscr" runat="server" Width="100px" CssClass="txt" MaxLength="9" onkeypress="return Mascarar(this, event,'999999999')"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvID" runat="server" ControlToValidate="txtIDInscr" Display="Dynamic" ErrorMessage="Campo obrigatório."></asp:RequiredFieldValidator>
                            </div>
                                <br />
                                <asp:Button ID="Button1" runat="server" CssClass="botoes" 
                                    onclick="Button1_Click" Text="Verificar" OnClientClick="AtivaDivProgress()" />
                               
                                
                                
                                <br />
                                <br />
                                <div id="pedidos_esq" runat="server" visible="false">
                                    <div  class="linha_campo">
                                        <asp:Label ID="lblID" runat="server" Class="lblTitulocampo" Text="Nr Cadastro:" Width="100px"></asp:Label>
                                        &nbsp;<asp:Label ID="Identificador" runat="server" Font-Bold="False"></asp:Label>
                                    </div>
                                    <div  class="linha_campo">
                                        <asp:Label ID="lblPart" runat="server" Class="lblTitulocampo" Text="Participante:" Width="100px"></asp:Label>
                                        &nbsp;<asp:Label ID="NoParticipante" runat="server" Font-Bold="False"></asp:Label>
                                    </div>
                                    <div  class="linha_campo">
                                        <asp:Label ID="lblCateg" runat="server" Class="lblTitulocampo" Text="Categoria:" Width="100px"></asp:Label>
                                        &nbsp;<asp:Label ID="nomeCategoria" runat="server" Font-Bold="False"></asp:Label>
                                    </div>
                                    
                                        <asp:HiddenField ID="AnoEvento" runat="server"></asp:HiddenField>
                                        <asp:HiddenField ID="lblRotina" runat="server"></asp:HiddenField>
                                        <asp:HiddenField ID="cdPart" runat="server"></asp:HiddenField>
                                        <asp:HiddenField ID="nrBoleto" runat="server" />
                                </div>
                        </div>
                        <div ID="linbtn" runat="server">
                            
                                <br />                                
                                <asp:Button ID="Button2" runat="server" CssClass="botoes"  Text="Fazer Download" Visible="False" OnClick="Button2_Click" />
                                <br />
                                
                            
                        </div>
                    </div>
                    <br />
                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                    <br />
                    &nbsp;
                    <br />
                    &nbsp;
                    
                </div>
                <div id="divId" style="clear:both; float:left; vertical-align: top; max-height: 400px; overflow: hidden; width: 500px;"></div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <%--<script type="text/javascript">
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

        <%--<script type="text/javascript" language="javascript">

            function AtivaDivProgress() {
                document.getElementById("divProgress").style.display = "block";
            }

        </script>--%>

        
        <script>

            
            $(document).on("click", "[id*=Button2]", function () {
               
                //var cpf = $('#<%=txtCPF.ClientID%>').val();
                var nrPart = $('#<%=cdPart.ClientID%>').val();
                var nrAnoEvento = $('#<%=AnoEvento.ClientID%>').val();
                var rotina = $('#<%=lblRotina.ClientID%>').val();
                if (rotina.toUpperCase() == "2VBOLETO")
                    var nrPart = $('#<%=nrBoleto.ClientID%>').val();
                //var rotina = document.getElementById('<%=lblRotina.ClientID %>').textContent;
                //var rotina = $('#<%=lblRotina.ClientID%>').text();                 
                showDialog(nrAnoEvento, nrPart, rotina);
                return false;
            });

            function showDialog(nrEvento, nrPart, rotina) {

                var tituloPagina = "";
                if (rotina.toUpperCase() == "CREDEMT")
                    tituloPagina = "Gerar Credencial";
                else if (rotina.toUpperCase() == "FRMEMPENHO")
                    tituloPagina = "Formulário para Empenho";
                else if (rotina.toUpperCase() == "RECIBO")
                    tituloPagina = "Recibo";
                else if (rotina.toUpperCase() == "2VBOLETO")
                    tituloPagina = "Impresão de Boleto";
                else if (rotina.toUpperCase() == "EMTCERT")
                    tituloPagina = "Certificado";
                else if (rotina.toUpperCase() == "INVOICE")
                    tituloPagina = "Invoice";
                //tituloPagina,
                //debugger;

                if (window.screen.availWidth <= 400) {

                    $("#divId").html('<iframe id="modalIframeId" width="340px" height="100%" marginWidth="0" marginHeight="0" frameBorder="0"  scrolling="none" />').dialog(
                    {
                        title: tituloPagina,
                        width: 380,
                        height: 450,
                        modal: true,
                        top: 50
                    });
                }
                else if ((window.screen.availWidth > 400) && (window.screen.availWidth <= 800)) {

                    $("#divId").html('<iframe id="modalIframeId" width="840px" height="100%" marginWidth="0" marginHeight="0" frameBorder="0"  scrolling="none" />').dialog(
                    {
                        title: tituloPagina,
                        width: 880,
                        height: 450,
                        modal: true,
                        top: 50
                    });
                }
                else if (window.screen.availWidth > 800) {

                    $("#divId").html('<iframe id="modalIframeId" width="860px" height="100%" marginWidth="0" marginHeight="0" frameBorder="0"  scrolling="none" />').dialog(
                    {
                        title: tituloPagina,
                        width: 900,
                        height: 450,
                        modal: true,
                        top: 50
                    });
                }

                //debugger;
                if (rotina.toUpperCase() == "CREDEMT")
                    $("#modalIframeId").attr("src", "frmEtiqueta.aspx?codEvento=" + nrEvento + "&codParticipante=" + nrPart);
                else if (rotina.toUpperCase() == "FRMEMPENHO")
                    $("#modalIframeId").attr("src", "frmNotaEmpenho.aspx?codEvento=" + nrEvento + "&codParticipante=" + nrPart);
                else if (rotina.toUpperCase() == "RECIBO")
                    $("#modalIframeId").attr("src", "frmRecibo.aspx?codEvento=" + nrEvento + "&codParticipante=" + nrPart);
                else if (rotina.toUpperCase() == "2VBOLETO")
                    $("#modalIframeId").attr("src", "frmBoleto.aspx?e=" + nrEvento + "&b=" + nrPart + "&tpRotina=2vBoleto");
                else if (rotina.toUpperCase() == "EMTCERT")
                    $("#modalIframeId").attr("src", "frmEmtCert.aspx?e=" + nrEvento + "&b=" + nrPart + "&tpRotina=EMTCERT");
                else if (rotina.toUpperCase() == "INVOICE")
                    $("#modalIframeId").attr("src", "frmEmtInvoice.aspx?e=" + nrEvento + "&b=" + nrPart + "&tpRotina=INVOICE");

                //?codEvento=" + nrEvento + "&codParticipante=" + nrPart);
                //}
                return false;
            }
        </script>


    </div>
</asp:Content>

