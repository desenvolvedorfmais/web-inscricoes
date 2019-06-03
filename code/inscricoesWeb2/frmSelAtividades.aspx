<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmSelAtividades.aspx.cs" Inherits="frmSelAtividades" Title="Inscri&ccedil;&otilde;es Web" 
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="ControlMessageBox" Namespace="ControlMessageBox" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <script src="scripts/MSGAguarde.js" type="text/javascript"></script>
    <asp:ToolkitScriptManager ID="TSManager1" runat="server" EnableScriptLocalization="true"
        CombineScripts="false">
    </asp:ToolkitScriptManager>
    
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
                    <br />Aguarde!... Processando sua solicitação<br />
                    <br />
                    <img src="imagensgeral/loader.gif" alt="" />
             </div>
        </asp:Panel>
        <h3>
            <asp:Label ID="lblTituloPagina" runat="server" Text="Pedido de Inscrição" CssClass="titulo"></asp:Label><br />
            <br />
        </h3>
        <div id="carrinho_geral" class="carrinhogeral" style="background-color: White;">
            <div id="pedidos_esq">
                
                <div style="float: left;">
                    <asp:Label ID="lblID" Visible="false" runat="server" Width="100px" CssClass="label" Text="Nr Cadastro:"></asp:Label>                    
                    &nbsp;<asp:Label ID="lblIdentificador" Visible="False" runat="server" Font-Bold="False"></asp:Label></div>
                <div style="float: left;">
                    <asp:Label ID="lblPart" runat="server" Width="100px" CssClass="label" Text="Participante:"></asp:Label>                    
                    &nbsp;<asp:Label ID="lblNoParticipante" runat="server" Font-Bold="False"></asp:Label></div>
                <div style="float: left;">
                    <asp:Label ID="lblCateg" runat="server" Width="100px" CssClass="label" Text="Categoria:"></asp:Label>                    
                    &nbsp;<asp:Label ID="lblCategoria" runat="server" Font-Bold="False"></asp:Label></div>
            </div> 
            <br />
            <div id="MsgExtra" runat="server" visible="false">
                <asp:Label ID="lblMsgExtra" runat="server">                   
                </asp:Label>
            </div>
            <div id="carrinho_pedidos">
                <h4>
                    <asp:Label ID="lblTituloResumo" runat="server" Text="Resumo do Pedido" CssClass="title"></asp:Label></h4>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    
                    <ContentTemplate>
                        <div id="resumo_carrinho">
                        <table id="carrinho" width="100%">
                            <tbody>
                                <tr>
                                    <td colspan="4">
                                        <asp:Label ID="lblResPed" runat="server" Text="Pedido Nº"></asp:Label>
                                        &nbsp;<asp:Label ID="lblNrPedido" runat="server" Font-Bold="False"></asp:Label>
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td id="titQtdItens" runat="server" style="width: 120px">
                                        <asp:Label ID="lblResItens" runat="server" Text="Itens"></asp:Label>
                                    </td>
                                    <td id="titVlrParc" runat="server" style="width: 120px; height: 16px;">
                                        <asp:Label ID="lblResVlr" runat="server" Text="Valor"></asp:Label>
                                    </td>
                                    <td id="titVlrDesc" runat="server" style="width: 120px">
                                        <asp:Label ID="lblResDesc" runat="server" Text="Descontos"></asp:Label>
                                    </td>
                                    <td id="titVlrTot" runat="server" style="width: 120px;">
                                        <asp:Label ID="lblResVlrTotal" runat="server" Text="Vlr do Pedido" Font-Bold="False"
                                            Font-Italic="False"></asp:Label>
                                    </td>
                                </tr>
                                <tr>                                    
                                    <td id="resQtdItens" runat="server" style="width: 133px" align="center">
                                        <asp:Label ID="vlItens" runat="server" Font-Bold="False">0</asp:Label>
                                    </td>
                                    <td id="resVlrParc" runat="server" style="width: 133px; height: 16px" align="center">
                                        <asp:Label ID="vlTotalAtiv" runat="server" Font-Bold="False" Font-Strikeout="False">0,00</asp:Label>
                                    </td>
                                    <td id="resVlrDesc" runat="server" style="width: 133px" align="center">
                                        <asp:Label ID="vlTotalDesc" runat="server" Font-Bold="False">0,00</asp:Label>
                                    </td>
                                    <td id="resVlrTot" runat="server" style="width: 133px;" align="center">
                                        <asp:Label ID="vlTotalPedido" runat="server" Font-Bold="False">0,00</asp:Label>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                            <div class="box-carrinho">                                        
                                        &nbsp;&nbsp;<asp:Button ID="btnAvancarAtividades" TabIndex="5" OnClick="btnAvancar_Click1" runat="server"
                                            Width="96px" Text="Continuar" CausesValidation="False"
                                            Font-Bold="False" CssClass="botoesDestaque"></asp:Button>
                            </div>
                        </div>
                        
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div id="filtro">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        
                        <div id="filtro_pedidos">
                            <label class="title">
                                <asp:Label ID="lblFiltro" runat="server" Text="Filtro" Visible="false"></asp:Label>
                            </label>
                            <div>
                                <asp:Label ID="lblTipoFiltro" runat="server" Text="Tipo" Width="29px"></asp:Label>
                                <asp:DropDownList ID="TxtFTipo" runat="server" OnSelectedIndexChanged="TxtFTipo_SelectedIndexChanged"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                            <div>
                                <asp:Label ID="lblDtInicioFiltro" runat="server" Text="Período"></asp:Label>
                                <asp:DropDownList ID="txtFDtInicio" runat="server" OnSelectedIndexChanged="txtFDtInicio_SelectedIndexChanged"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                        </div>
                        
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            
        </div>
        <br />
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <div id="mensagem" class="msg" runat="server" visible="false"><asp:Label ID="lblMsg" runat="server"></asp:Label></div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="msgANABB2" runat="server" visible="false">
            <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Navy" Font-Size="14pt"
                Text="<p>Marque abaixo um Minigrupo</p>">
            </asp:Label>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Panel ID="p1" runat="server">                    
                    <div id="Grids">      
                        <div id="itensDisponiveisGeral" class="clsitensDisponiveisGeral" runat="server">
                            <div id="TituloGrid1" class="clsTituloGrid" runat="server">
                                <asp:Label ID="lblTituloGrid1" runat="server" CssClass="titulo2" Text="Itens Disponíveis"></asp:Label>
                            </div>
                            <div id="divItensDisponiveis" class="clsDivItensDisponiveis" >
                                    <asp:GridView ID="grdAtv" runat="server" ForeColor="#333333" OnRowDataBound="grdAtv_RowDataBound" Width="100%"
                                    ShowHeader="False" DataKeyNames="noTipoAtividade,noTitulo,dsTema,noLocal,dtIni,dtTermino,vagas_em_aberto,cdAtividade,noProfessor,vlCapacidade,cdTipoAtividade,vlAtividade,vlDescontoReal,vlTotInscri,flInscricaoObrigatoria,flPodeChocarHorario,dsCaminhoImgWEB,vlQuantidade,flRequerQuantidade,nrLinha,vlQuantidadeMaxima,flPodeRepetirPedido,dsTurno"
                                    AutoGenerateColumns="False" CellPadding="4" GridLines="None" OnSelectedIndexChanging="grdAtv_SelectedIndexChanging">
                                    <RowStyle CssClass="grdlinhaimpar">
                                    </RowStyle>
                                    <Columns>
                                        <asp:TemplateField ShowHeader="False" ItemStyle-CssClass="clsItemSelectGrid">
                                            <ItemTemplate>
                                                <center>
                                                <asp:ImageButton ID="btnIncluir" runat="server" Text="Select" CausesValidation="False"
                                                    ImageUrl="~/img/carrinho28x28.png" CommandName="Select" CssClass="btnItemSelectGrid"></asp:ImageButton>
                                                </center>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="clsItemSelectGrid" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>                                                
                                                <div id="imgAtividade" class="clsImgAtividade">
                                                    <asp:Image ID="imgAtivProf" runat="server" Height="100px" ImageUrl='<%# Eval("dsCaminhoImgWEB") %>'
                                                        Width="96px" />
                                                </div>
                                                <div id="informAtividade" class="clsInformAtividade">
                                                    <div id="divTipoAtv" class="clsDivTipAtv">
                                                        <asp:Label ID="lblTpItem" runat="server" __designer:wfdid="w43" Text="Tipo:"></asp:Label>
                                                        <asp:Label ID="lblTipo" runat="server" __designer:wfdid="w44" Font-Bold="False"
                                                            Text='<%# Eval("noTipoAtividade") %>'></asp:Label>
                                                        <asp:Label ID="lblCdTipoAtv" runat="server" __designer:wfdid="w44" Font-Bold="False" Text='<%# Eval("cdTipoAtividade") %>' Visible="False"></asp:Label>
                                                        <asp:Label ID="lblCdAtv" runat="server" __designer:wfdid="w44" Font-Bold="False" Text='<%# Eval("cdAtividade") %>' Visible="False"></asp:Label>
                                                    </div>
                                                    <div id="divDescAtv" class="clsDivDescAtv">
                                                        <asp:Label ID="lblAtividade" runat="server" CssClass="lblDescAtividade" Text='<%# Eval("noTitulo") %>'></asp:Label>
                                                    </div>
                                                    <div id="divProfAtv" class="clsDivProfAtv">
                                                        <asp:Label ID="lblNoProfessor" runat="server" __designer:wfdid="w47" Font-Bold="False" Text='<%# Eval("noProfessor") %>'></asp:Label>
                                                    </div>
                                                    <div id="divLocalAtv" class="clsDivLocalAtv">
                                                        <asp:Label ID="lblLocalItem" runat="server" __designer:wfdid="w48" Text="Local:"></asp:Label>
                                                        <asp:Label ID="lblLocal" runat="server" __designer:wfdid="w49" Font-Bold="False"
                                                            Text='<%# Eval("noLocal") %>'></asp:Label>
                                                    </div>
                                                    <div id="divPeriodoAtv" class="clsDivPeriodoAtv">
                                                        <asp:Label ID="lblDeItem" runat="server" __designer:wfdid="w50" Text="De: "></asp:Label>
                                                        <asp:Label ID="lblDtIni" runat="server" __designer:wfdid="w51" Font-Bold="False"
                                                            Text='<%# Eval("dtIni") %>'></asp:Label>
                                                        &nbsp;<asp:Label ID="lblAteItem" runat="server" __designer:wfdid="w52" Text=" a: "></asp:Label>
                                                        &nbsp;<asp:Label ID="lblDtTermino" runat="server" __designer:wfdid="w53" Font-Bold="False" Text='<%# Eval("dtTermino") %>'></asp:Label>
                                                    </div>
                                                    <div id="divVagasAtv" class="clsDivVagasAtv">
                                                        <asp:Label ID="lblVagasItem" runat="server" __designer:wfdid="w54" Text="Vagas:"></asp:Label>
                                                        <asp:Label ID="lblVagas" runat="server" __designer:wfdid="w55" Font-Bold="False" Text='<%# Eval("vagas_em_aberto") %>'></asp:Label>
                                                        <asp:CheckBox ID="chkRequerQTD" runat="server" Checked='<%# Eval("flRequerQuantidade") %>'
                                                            Visible="False" />
                                                        <asp:CheckBox ID="chkSelecionado" runat="server"  Visible="False"/>
                                                    </div>
                                                </div>
                                                        
                                                <div id="vlr_item_pedido" class="valor_item_pedido">
                                                    <asp:Panel ID="pnlResumoVlrItens" runat="server">
                                                        
                                                        <div id="vlrItem" class="clsVlrItem" runat="server">
                                                            <asp:Label ID="lblValorItem" runat="server" Text="Valor"></asp:Label>
                                                            <br />
                                                            <asp:Label ID="lblVlAtividade" runat="server" Font-Bold="False"
                                                                Font-Strikeout="False" Text='<%# Eval("vlAtividade") %>' ></asp:Label>
                                                        </div>                                                                                                                                
                                                        <div id="vlrDescItem" class="clsVlrDescItem" runat="server">
                                                            <asp:Label ID="lblDescItem" runat="server" Text="Desconto"></asp:Label>
                                                            <br />
                                                            <%--<font color="red"><b>(&nbsp;-&nbsp;)</b></font>--%>
                                                            <asp:Label ID="lblVlDescontoReal" runat="server" Font-Bold="False" Text='<%# Eval("vlDescontoReal") %>' ></asp:Label>
                                                        </div>
                                                        <div id="vlrQtdItem" class="clsVlrQtdItem" runat="server">
                                                            <asp:Label ID="lblQtdItem" runat="server" Text="Quantidade"></asp:Label>
                                                            <br />
                                                            <%--<b>(&nbsp;x&nbsp;)</b>--%>
                                                            <asp:TextBox ID="txtVlQuantidade" runat="server" Text='<%# Eval("vlQuantidade") %>' OnTextChanged="txtVlQuantidade_TextChanged" CssClass="campoform_mini" Font-Bold="False" Style="text-align: center;"></asp:TextBox>
                                                            <%--<asp:Button  ID="btnConfVlrQuantidade"--%>
                                                            <asp:Label ID="lblVlQuantidade" runat="server" Font-Bold="False" Font-Strikeout="False" Text='<%# Eval("vlQuantidade") %>' Visible="False"></asp:Label>
                                                        </div>
                                                        <div id="vlrTotalItem" class="clsVlrTotalItem" runat="server">
                                                            <asp:Label ID="lblVlrTotalItem" runat="server" Text="Vl Inscri" Font-Bold="False"></asp:Label>
                                                            <br />
                                                            <%--<font color="blue"><b>&nbsp;=&nbsp;)</b></font>--%>
                                                            <asp:Label ID="lblVlTotInscri" runat="server" Font-Bold="False" Text='<%# Eval("vlTotInscri") %>'></asp:Label>
                                                        </div>
                                                            
                                                    </asp:Panel>
                                                </div>
                                                        
                                                    
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="noTipoAtividade" HeaderText="Tipo" Visible="False"></asp:BoundField>
                                        <asp:BoundField DataField="noTitulo" HeaderText="Atividade" Visible="False"></asp:BoundField>
                                        <asp:BoundField DataField="dsTema" HeaderText="Tema" Visible="False"></asp:BoundField>
                                        <asp:BoundField DataField="noLocal" HeaderText="Local" Visible="False"></asp:BoundField>
                                        <asp:BoundField DataField="dtIni" HeaderText="In&#237;cio" Visible="False"></asp:BoundField>
                                        <asp:BoundField DataField="dtTermino" HeaderText="T&#233;rmino" Visible="False">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="vagas_em_aberto" HeaderText="Vagas" Visible="False"></asp:BoundField>
                                        <asp:BoundField DataField="cdAtividade" HeaderText="cdAtividade" Visible="False">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="noProfessor" HeaderText="noProfessor" Visible="False">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="vlCapacidade" HeaderText="vlCapacidade" Visible="False">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cdTipoAtiviade" HeaderText="cdTipoAtiviade" Visible="False">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="vlAtividade" HeaderText="Valor" Visible="False"></asp:BoundField>
                                        <asp:BoundField DataField="vlDescontoReal" HeaderText="Descto" Visible="False"></asp:BoundField>
                                        <asp:BoundField DataField="vlTotInscri" HeaderText="vlr Inscr" Visible="False"></asp:BoundField>
                                        <asp:BoundField DataField="flPodeChocarHorario" HeaderText="flPodeChocarHorario"
                                            Visible="False"></asp:BoundField>
                                    </Columns>
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></FooterStyle>
                                    <PagerStyle HorizontalAlign="Center" BackColor="#2461BF" ForeColor="White"></PagerStyle>
                                    <SelectedRowStyle Font-Bold="False" ForeColor="#333333"></SelectedRowStyle>
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                    <AlternatingRowStyle CssClass="grdlinhapar"></AlternatingRowStyle> 
                                </asp:GridView>
                            </div>
                        </div>
                        <div  id="itensSelecionadosGeral" class="clsitensSelecionadosGeral" runat="server" visible="false">
                            <div id="TituloGrid2" class="clsTituloGrid" runat="server" visible="false">
                                <asp:Label ID="Label1" runat="server" CssClass="titulo2" Text="Itens Selecionados"></asp:Label>
                            </div>
                            <div id="divItensSelecionados" class="clsDivItensSelecionados">
                                <asp:GridView ID="grdAtvParticipante" runat="server" AutoGenerateColumns="False"
                                    CellPadding="4" DataKeyNames="noTipoAtividade,noTitulo,dsTema,noLocal,dtIni,dtTermino,cdAtividade,noProfessor,cdTipoAtividade,vlAtividade,flAtivo,flUsado,dtMatricula,vlDesconto,vlMatricula,flInscricaoObrigatoria,flPodeChocarHorario,dsCaminhoImgWEB,vlQuantidade,flRequerQuantidade,nrLinha,vlQuantidadeMaxima,flPodeRepetirPedido,dsTurno"
                                    ForeColor="#333333" GridLines="None" OnRowDataBound="grdAtvParticipante_RowDataBound"
                                    OnRowDeleting="grdAtvParticipante_RowDeleting" ShowHeader="False" Visible="true"
                                    Width="490px">
                                    <RowStyle BorderColor="White" BorderStyle="Solid" BorderWidth="2px" CssClass="grdlinhaimpar" />
                                    <Columns>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnRemove" runat="server" __designer:wfdid="w23" CausesValidation="False"
                                                    CommandName="Delete" ImageUrl="~/img/delete 18x18.png" Text="Remover" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <table >
                                                    <tr>
                                                        <td valign="middle">
                                                            <asp:Image ID="imgAtivProf" runat="server" Height="100px" ImageUrl='<%# Eval("dsCaminhoImgWEB") %>'
                                                                Width="96px" />
                                                        </td>
                                                        <td  style="Width:100%">
                                                            <div id="divTipoAtvSel" class="clsDivTipAtvSel">
                                                                <asp:Label ID="lblTpItem" runat="server" __designer:wfdid="w43" Text="Tipo:"></asp:Label>
                                                                <asp:Label ID="lblTipo" runat="server" __designer:wfdid="w25" Font-Bold="False"
                                                                    Text='<%# Eval("noTipoAtividade") %>'></asp:Label>
                                                                <asp:Label ID="lblCdTipoAtvSel" runat="server" __designer:wfdid="w25" Font-Bold="False" Visible="False"
                                                                    Text='<%# Eval("cdTipoAtividade") %>'></asp:Label>
                                                                <asp:Label ID="lblCdAtvSel" runat="server" __designer:wfdid="w25" Font-Bold="False" Visible="False"
                                                                    Text='<%# Eval("cdAtividade") %>'></asp:Label>
                                                            </div>
                                                            <div id="divDescAtvSel" class="clsDivDescAtvSel">
                                                                <asp:Label ID="lblAtividade" CssClass="lblDescAtividade" runat="server" Text='<%# Eval("noTitulo") %>'></asp:Label>
                                                            </div>
                                                            <div id="divProfAtvSel" class="clsDivProfAtvSel">
                                                                <asp:Label ID="lblNoProfessor" runat="server" __designer:wfdid="w29" Font-Bold="False" Text='<%# Eval("noProfessor") %>'></asp:Label>
                                                            </div>
                                                            <div id="divLocalAtvSel" class="clsDivLocalAtvSel">
                                                                <asp:Label ID="lblLocalItem" runat="server" __designer:wfdid="w48" Text="Local:"></asp:Label>
                                                                <asp:Label ID="lblLocal" runat="server" __designer:wfdid="w29" Font-Bold="False"
                                                                    Text='<%# Eval("noLocal") %>'></asp:Label>
                                                            </div>
                                                            <div id="divPeriodoAtvSel" class="clsDivPeriodoAtvSel">
                                                                <asp:Label ID="lblDeitem" runat="server" __designer:wfdid="w50" Text="De: "></asp:Label>
                                                                &nbsp;<asp:Label ID="lblDtIni" runat="server" __designer:wfdid="w31" Font-Bold="True"
                                                                    Font-Size="10px" Text='<%# Eval("dtIni") %>'></asp:Label>
                                                                <asp:Label ID="lblAteItem" runat="server" __designer:wfdid="w52" Text=" a: "></asp:Label>
                                                                &nbsp;<asp:Label ID="lblDtTermino" runat="server" __designer:wfdid="w33" Font-Bold="True"
                                                                    Font-Size="10px" Text='<%# Eval("dtTermino") %>'></asp:Label>                                                                    
                                                            <asp:CheckBox ID="chkRequerQTD" runat="server" Checked='<%# Eval("flRequerQuantidade") %>'
                                                                Visible="False" />
                                                            </div>
                                                            <div id="vlr_item_pedido" class="valor_item_pedido">
                                                                <asp:Panel ID="pnlResumoVlrItens" runat="server" Width="160px">
                                                                    <table width="300px">
                                                                        <tr>
                                                                            <td style="width: 50px">
                                                                                <asp:Label ID="lblValorItem" runat="server" __designer:wfdid="w56" Text="Valor" Width="50px"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td style="width: 50px">
                                                                                <asp:Label ID="lblDescItem" runat="server" __designer:wfdid="w57" Text="Desconto"
                                                                                    Width="50px"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td style="width: 70px">
                                                                                <asp:Label ID="lblQtdItem" runat="server" __designer:wfdid="w57" Text="Quantidade"
                                                                                    Width="70px"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td style="width: 60px; ">
                                                                                <asp:Label ID="lblVlrTotalItem" runat="server" __designer:wfdid="w58" Text="Vl Inscri"
                                                                                    Width="60px" Font-Bold="False"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" width="50px">
                                                                                <asp:Label ID="lblVlAtividade" runat="server" __designer:wfdid="w37" Font-Bold="False" Text='<%# Eval("vlAtividade") %>' Width="40px"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label19" runat="server" __designer:wfdid="w60" Font-Bold="True" Font-Size="12px"
                                                                                    ForeColor="Red" Text="-"></asp:Label>
                                                                            </td>
                                                                            <td align="right" width="50px">
                                                                                <asp:Label ID="lblVlDescontoReal" runat="server" __designer:wfdid="w39" Font-Bold="False" Text='<%# Eval("vlDesconto") %>' Width="40px"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label40" runat="server" __designer:wfdid="w60" Font-Bold="False" Font-Size="12px"
                                                                                    ForeColor="Black" Text="x"></asp:Label>
                                                                            </td>
                                                                            <td align="center" width="70px">
                                                                                <asp:TextBox ID="txtVlQuantidade" runat="server" Font-Bold="False"
                                                                                    OnTextChanged="txtVlQuantidade_TextChanged1" Text='<%# Eval("vlQuantidade") %>'
                                                                                    Width="50px"></asp:TextBox>
                                                                                <asp:Label ID="lblVlQuantidade" runat="server" __designer:wfdid="w37" Font-Bold="False" Text='<%# Eval("vlQuantidade") %>' Width="20px"></asp:Label>
                                                                            </td>
                                                                            <td >
                                                                                <asp:Label ID="Label21" runat="server" __designer:wfdid="w62" Font-Bold="False" Font-Size="11px"
                                                                                    ForeColor="Blue" Text="="></asp:Label>
                                                                            </td>
                                                                            <td align="right" width="60px">
                                                                                <asp:Label ID="lblVlTotInscri" runat="server" __designer:wfdid="w41" Font-Bold="False" Text='<%# Eval("vlMatricula") %>' Width="40px"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                            </div>
                                                        </td>
                                                        <td valign="middle" width="100px">
                                                            <%--<div id="vlr_item_pedido" class="valor_item_pedido">
                                                                <asp:Panel ID="pnlResumoVlrItens" runat="server" Width="160px">
                                                                    <table width="140px">
                                                                        <tr>
                                                                            <td style="width: 88px">
                                                                                <asp:Label ID="lblValorItem" runat="server" __designer:wfdid="w56" Text="Valor" Width="80px"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="right" width="50px">
                                                                                <asp:Label ID="lblVlAtividade" runat="server" __designer:wfdid="w37" Font-Bold="True"
                                                                                    Font-Size="11px" Text='<%# Eval("vlAtividade") %>' Width="40px"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 88px">
                                                                                <asp:Label ID="lblDescItem" runat="server" __designer:wfdid="w57" Text="Desconto"
                                                                                    Width="80px"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label19" runat="server" __designer:wfdid="w60" Font-Bold="True" Font-Size="12px"
                                                                                    ForeColor="Red" Text="-"></asp:Label>
                                                                            </td>
                                                                            <td align="right" width="50px">
                                                                                <asp:Label ID="lblVlDescontoReal" runat="server" __designer:wfdid="w39" Font-Bold="True"
                                                                                    Font-Size="11px" ForeColor="Red" Text='<%# Eval("vlDesconto") %>' Width="40px"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 88px">
                                                                                <asp:Label ID="lblQtdItem" runat="server" __designer:wfdid="w57" Text="Quantidade"
                                                                                    Width="80px"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label40" runat="server" __designer:wfdid="w60" Font-Bold="False" Font-Size="12px"
                                                                                    ForeColor="Black" Text="x"></asp:Label>
                                                                            </td>
                                                                            <td align="right" width="50px">
                                                                                <asp:TextBox ID="txtVlQuantidade" runat="server" Font-Bold="True" Font-Size="11px"
                                                                                    OnTextChanged="txtVlQuantidade_TextChanged1" Text='<%# Eval("vlQuantidade") %>'
                                                                                    Width="50px"></asp:TextBox>
                                                                                <asp:Label ID="lblVlQuantidade" runat="server" __designer:wfdid="w37" Font-Bold="True"
                                                                                    Font-Size="11px" Text='<%# Eval("vlQuantidade") %>' Width="20px"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 88px; border-top-width: 1px; border-top-style: solid; border-top-color: Gray">
                                                                                <asp:Label ID="lblVlrTotalItem" runat="server" __designer:wfdid="w58" Text="Vl Inscri"
                                                                                    Width="80px" Font-Bold="True"></asp:Label>
                                                                            </td>
                                                                            <td style="border-top-width: 1px; border-top-style: solid; border-top-color: Gray">
                                                                                <asp:Label ID="Label21" runat="server" __designer:wfdid="w62" Font-Bold="False" Font-Size="11px"
                                                                                    ForeColor="Blue" Text="="></asp:Label>
                                                                            </td>
                                                                            <td align="right" style="border-top-width: 1px; border-top-style: solid; border-top-color: Gray" width="50px">
                                                                                <asp:Label ID="lblVlTotInscri" runat="server" __designer:wfdid="w41" Font-Bold="True"
                                                                                    Font-Size="11px" ForeColor="Blue" Text='<%# Eval("vlMatricula") %>' Width="40px"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                            </div>
                                                            <br />--%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="noTipoAtividade" HeaderText="Tipo" Visible="False" />
                                        <asp:BoundField DataField="noTitulo" HeaderText="Atividade" Visible="False" />
                                        <asp:BoundField DataField="dsTema" HeaderText="Tema" Visible="False" />
                                        <asp:BoundField DataField="noLocal" HeaderText="Local" Visible="False" />
                                        <asp:BoundField DataField="dtIni" HeaderText="Início" Visible="False" />
                                        <asp:BoundField DataField="dtTermino" HeaderText="Término" Visible="False" />
                                        <asp:BoundField DataField="cdAtividade" HeaderText="cdAtividade" Visible="False" />
                                        <asp:BoundField DataField="noProfessor" HeaderText="noProfessor" Visible="False" />
                                        <asp:BoundField DataField="cdTipoAtiviade" HeaderText="cdTipoAtiviade" Visible="False" />
                                        <asp:BoundField DataField="vlAtividade" HeaderText="Valor" Visible="False" />
                                        <asp:BoundField DataField="flAtivo" HeaderText="flAtivo" Visible="False" />
                                        <asp:BoundField DataField="flUsado" HeaderText="flUsado" Visible="False" />
                                        <asp:BoundField DataField="dtMatricula" HeaderText="dtMatricula" Visible="False" />
                                        <asp:BoundField DataField="vlDesconto" HeaderText="Descto" Visible="False" />
                                        <asp:BoundField DataField="vlMatricula" HeaderText="vlMatricula" Visible="False" />
                                    </Columns>
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle Font-Bold="False" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <AlternatingRowStyle CssClass="grdlinhapar" />
                                </asp:GridView>
                            </div>
                        </div>
                     </div> 
                </asp:Panel>
                <br />
                <div id="carrinho_pedidos2">
                    
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <div id="resumo_carrinho2">
                                <asp:Button ID="btnAvancar2" runat="server" Text="Continuar" Visible="True" Width="96px" CssClass="botoesDestaque" OnClick="btnAvancar_Click1" />
                                
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </ContentTemplate>
            <triggers>
                <asp:AsyncPostBackTrigger ControlID="grdAtv" />
                <asp:AsyncPostBackTrigger ControlID="grdAtvParticipante" />
            </triggers>
        </asp:UpdatePanel>
        <br />
          
    
    <!--<script type="text/javascript">
        (function ($) {
            $.lockfixed("#carrinho_geral", { forcemargin: false, offset: { top: 0, bottom: 620} });

        })(jQuery);	
    </script>-->
    <%--<script type="text/jscript">
        var header = document.querySelector('.carrinhogeral');

       
        var origOffsetY = header.offsetTop;
        //(window.scrollY >= origOffsetY) &&
        function onScroll(e) {
            ( (window.scrollY > 200)) ? header.classList.add('sticky') :
                                  header.classList.remove('sticky');
        }

        document.addEventListener('scroll', onScroll);
    </script>--%>

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

    </div>
</asp:Content>
