<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmRegistrarPresenca.aspx.cs" 
Inherits="frmRegistrarPresenca" Title="Inscri&ccedil;&otilde;es Web"%>

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
        
        <asp:UpdateProgress runat="server" id="UpdateProgress1" DisplayAfter="0"  >
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
        </asp:Panel>
        <%--<div id="divProgress" class="modalPopUpBackground" align="center">
            <div id="loading" class="modalExtender">
                    <br />Aguarde!... Processando sua solicitação. <br />
                    <br />
                    <img src="imagensgeral/loader.gif" alt="" />
            </div>
        </div>--%>

        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
                <div id="CamposPesquisa" class="camposPesquisa">
                    <div ID="tbCPFPesquisa" runat="server">
                        <div ID="linCPF" runat="server">
                            <div style="float: left; clear: both;">
                                <asp:Label ID="lblIdInscr" runat="server" Text="Nr Inscrição" Visible="true"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtCdInscricao" runat="server"  CssClass="txt" MaxLength="9" ReadOnly="false" TabIndex="5"  Width="110px" placeholder="000000000" Text="000000000"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp;
                                <asp:Label ID="Label4" runat="server" Text="ou" Visible="true"></asp:Label>
                                &nbsp;&nbsp;&nbsp;
                                <%--<asp:Button ID="Button3" runat="server" CssClass="botoes" 
                                    onclick="Button1_Click" Text="Verificar" OnClientClick="AtivaDivProgress()" />--%>
                                <br />
                            </div>
                            <div style="float: left; clear: right;">
                                <asp:Label ID="lblCPF" runat="server" CssClass="label" Text="Informe o CPF"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtCPF" runat="server" Width="140px" CssClass="txt" MaxLength="14" 
                                            onkeypress="return CPFMascarar(this, event)"></asp:TextBox>
                                &nbsp;
                                <asp:Button ID="Button1" runat="server" CssClass="botoes" 
                                    onclick="Button1_Click" Text="Verificar" OnClientClick="AtivaDivProgress()" />
                                <br />
                            </div>
                        </div>
                        <div id="pedidos_esq" runat="server" visible="false" style="float: left; clear: both;">
                            <br/><br/>      
                            <div>
                                <asp:Label ID="lblID" runat="server" CssClass="label" Text="Nr Cadastro:" Width="100px"></asp:Label>
                                &nbsp;<asp:Label ID="nrcadastro" runat="server" Font-Bold="True" ForeColor="Navy"></asp:Label>
                            </div>
                            <div>
                                <asp:Label ID="Label2" runat="server" CssClass="label" Text="CPF:" Width="100px"></asp:Label>
                                &nbsp;<asp:Label ID="CPF" runat="server" Font-Bold="True" ForeColor="Navy"></asp:Label>
                            </div>
                            <div>
                                <asp:Label ID="lblPart" runat="server" CssClass="label" Text="Participante:" Width="100px"></asp:Label>
                                &nbsp;<asp:Label ID="NoParticipante" runat="server" Font-Bold="True" ForeColor="Navy"></asp:Label>
                            </div>
                            <div>
                                <asp:Label ID="lblCateg" runat="server" CssClass="label" Text="Categoria:" Width="100px"></asp:Label>
                                &nbsp;<asp:Label ID="nomeCategoria" runat="server" Font-Bold="True" ForeColor="Navy"></asp:Label>
                            </div>
                            <div>
                                <asp:Label ID="Label1" runat="server" CssClass="label" Text="Cidade:" Width="100px"></asp:Label>
                                &nbsp;<asp:Label ID="Cidade" runat="server" Font-Bold="True" ForeColor="Navy"></asp:Label>
                            </div>
                            <div>
                                <asp:Label ID="Label3" runat="server" CssClass="label" Text="Local:" Width="100px"></asp:Label>
                                &nbsp;<asp:Label ID="Local" runat="server" Font-Bold="True" ForeColor="Navy"></asp:Label>
                            </div>
                            <div>
                                <asp:Label ID="Label5" runat="server" CssClass="label" Text="Data:" Width="100px"></asp:Label>
                                &nbsp;<asp:Label ID="Data" runat="server" Font-Bold="True" ForeColor="Navy"></asp:Label>
                            </div>
                            <div>
                                <asp:Label ID="Label7" runat="server" CssClass="label" Text="Horário:" Width="100px"></asp:Label>
                                &nbsp;<asp:Label ID="Hora" runat="server" Font-Bold="True" ForeColor="Navy"></asp:Label>
                            </div>
                                    
                                <asp:HiddenField ID="AnoEvento" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="lblRotina" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="cdPart" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="nrBoleto" runat="server" />
                        </div>
                        
                        <div ID="linbtn" runat="server" style="float: left; clear: both; ">
                            
                                <br />                                
                                <asp:Button ID="btnLimpar" runat="server" CssClass="botoes"  Text="Limpar e Fazer Nova Pesquisa" OnClick="Button2_Click" />
                                <br /> <br /> 
                                
                            
                        </div>
                        <div ID="divMsg" runat="server" >
                            <br /> <br />
                                <asp:Label ID="lblMsg" style="color: white; font-size: 12pt;" runat="server"></asp:Label>
                            <br /> <br /> 
                        </div>
                    </div>
                    <br />
                    
                    <br />
                    &nbsp;
                    <br />
                    &nbsp;
                    
                </div>
                <div id="divId" style="clear:both; float:left; vertical-align: top; max-height: 400px; overflow: hidden; width: 500px;"></div>
            </ContentTemplate>
        </asp:UpdatePanel>

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

        

        
        <%--<script>

            
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
                //tituloPagina,
                debugger;
                $("#divId").html('<iframe id="modalIframeId" width="860px" height="100%" marginWidth="0" marginHeight="0" frameBorder="0"  scrolling="none" />').dialog(
                    {
                        title: tituloPagina,
                        width: 900,
                        height: 450,
                        modal: true
                    });

                debugger;
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

                //?codEvento=" + nrEvento + "&codParticipante=" + nrPart);
                //}
                return false;
            }
        </script>--%>


    </div>
</asp:Content>

