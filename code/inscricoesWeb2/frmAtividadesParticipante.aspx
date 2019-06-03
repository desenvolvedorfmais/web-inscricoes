<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmAtividadesParticipante.aspx.cs" Inherits="frmAtividadesParticipante" Title="Inscri&ccedil;&otilde;es Web" %>

<%@ Register Assembly="ControlMessageBox" Namespace="ControlMessageBox" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="frmCad_auto">
        <asp:ToolkitScriptManager ID="TSManager1" runat="server" EnableScriptLocalization="true"
            CombineScripts="false">
        </asp:ToolkitScriptManager>
        <h3><asp:Label ID="lblTituloPagina" runat="server" Text="Matrículas / Inscrições" CssClass="titulo"></asp:Label><br /><br /></h3>
        <div id="pedidos_esq">                 
            <div style="float: left;">
                <asp:Label ID="lblID" runat="server" Width="100px" CssClass="label" Text="Nr Cadastro:"></asp:Label>                    
                &nbsp;<asp:Label ID="lblIdentificador" runat="server" Font-Bold="False"></asp:Label></div>
            <div style="float: left;">
                <asp:Label ID="lblPart" runat="server" Width="100px" CssClass="label" Text="Participante:"></asp:Label>                    
                &nbsp;<asp:Label ID="lblNoParticipante" runat="server" Font-Bold="False"></asp:Label></div>
            <div style="float: left;">
                <asp:Label ID="lblCateg" runat="server" Width="100px" CssClass="label" Text="Categoria:"></asp:Label>                    
                &nbsp;<asp:Label ID="lblCategoria" runat="server" Font-Bold="False"></asp:Label></div>

            <div id="btnatvidadeABRASEL" style="display:table; float:right; padding: 5px">
                <asp:Button ID="btnAtvABRASEL" runat="server" PostBackUrl="~/AtividadesAbrasel.aspx" 
                Text="Configurar Atividades" CssClass="botoes" Visible="False" />
            </div>    
    
        </div>
        <br />
    <div>
    
     <div  id="itensDisponiveisGeral" class="clsitensDisponiveisGeral" runat="server" visible="true"><br /><br />&nbsp;
                <div id="TituloGrid2" class="clsTituloGrid" runat="server" visible="false">
                    <asp:Label ID="lblTituloGrid1" runat="server" CssClass="titulo2" Text="Itens do Pedido"></asp:Label>
                </div>
                <div id="divItensDisponiveis" class="clsDivItensDisponiveis">
                    <asp:GridView ID="grdAtvParticipante" runat="server" AutoGenerateColumns="False"
                        CellPadding="4" DataKeyNames="noTipoAtividade,noTitulo,dsTema,noLocal,dtIni,dtTermino,cdAtividade,noProfessor,cdTipoAtividade,vlAtividade,flAtivo,flUsado,dtMatricula,vlDesconto,vlMatricula,flInscricaoObrigatoria,flPodeChocarHorario,dsCaminhoImgWEB,vlQuantidade,flRequerQuantidade,nrLinha,vlQuantidadeMaxima,flPodeRepetirPedido,dsTurno"
                        ForeColor="#333333" GridLines="None" OnRowDataBound="grdAtvParticipante_RowDataBound"
                        OnRowDeleting="grdAtvParticipante_RowDeleting" ShowHeader="False" Visible="true"
                        Width="100%">
                        <RowStyle BorderColor="White" BorderStyle="Solid" BorderWidth="2px" CssClass="grdlinhaimpar" />
                        <Columns>                                        
                            <asp:TemplateField>
                                <ItemTemplate>
                                                                                                    
                                    <div id="imgAtividade" class="clsImgAtividade">
                                        <asp:Image ID="imgAtivProf" runat="server" Height="100px" ImageUrl='<%# Eval("dsCaminhoImgWEB") %>'
                                            Width="96px" />
                                    </div>
                                    <div id="informAtividade" class="clsInformAtividade">
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
                                            <div id="vlrQtdItemMat" class="clsVlrQtdItemMat" runat="server">
                                                <asp:Label ID="lblQtdItem" runat="server" Text="Quantidade: "></asp:Label>
                                                
                                                <asp:Label ID="lblVlQuantidade" runat="server" Font-Bold="False" Font-Strikeout="False" Text='<%# Eval("vlQuantidade") %>'></asp:Label>
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
                                            &nbsp;<asp:Label ID="lblDtIni" runat="server" __designer:wfdid="w31" Font-Bold="False" Text='<%# Eval("dtIni") %>'></asp:Label>
                                            <asp:Label ID="lblAteItem" runat="server" __designer:wfdid="w52" Text=" a: "></asp:Label>
                                            &nbsp;<asp:Label ID="lblDtTermino" runat="server" __designer:wfdid="w33" Font-Bold="False" Text='<%# Eval("dtTermino") %>'></asp:Label>                                                                    
                                        <asp:CheckBox ID="chkRequerQTD" runat="server" Checked='<%# Eval("flRequerQuantidade") %>'
                                            Visible="False" />
                                        </div>
                                    </div>                                                      
                                                
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
    <%--<asp:Panel ID="p1" runat="server" >
        <table style="width: 100%" cellspacing="5">
            <tbody>
                <tr>
                    <td valign="top">
                        <asp:GridView ID="grdAtvParticipante" runat="server" 
                            AutoGenerateColumns="False" CellPadding="4" 
                            DataKeyNames="noTipoAtividade,noTitulo,dsTema,noLocal,dtIni,dtTermino,cdAtividade,noProfessor,cdTipoAtividade,vlAtividade,flAtivo,flUsado,dtMatricula,vlDesconto,vlMatricula,flInscricaoObrigatoria,flPodeChocarHorario,dsCaminhoImgWEB,vlQuantidade,flRequerQuantidade,nrLinha,vlQuantidadeMaxima,flPodeRepetirPedido,dsTurno" 
                            ForeColor="#333333" GridLines="None" 
                            OnRowDataBound="grdAtvParticipante_RowDataBound" ShowHeader="False" 
                            Width="680px">
                            <RowStyle CssClass="grdlinhaimpar" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <table style="width:100%;">
                                            <tr>
                                                <td valign="middle">
                                                    <asp:Image ID="imgAtivProf" runat="server" Height="100px" 
                                                        ImageUrl='<%# Eval("dsCaminhoImgWEB") %>' Width="96px" />
                                                </td>
                                                <td >
                                                    <asp:Label ID="lblTpItem" runat="server" __designer:wfdid="w43" Text="Tipo:"></asp:Label>
                                                    <asp:Label ID="lblTipo" runat="server" __designer:wfdid="w25" Font-Bold="True" 
                                                        Font-Size="11px" Text='<%# Eval("noTipoAtividade") %>'></asp:Label>
                                                    <br />
                                                    <asp:Label ID="Label7" runat="server" __designer:wfdid="w45" Text="-"></asp:Label>
                                                    &nbsp;<asp:Label ID="lblAtividade" runat="server" __designer:wfdid="w27" 
                                                        Font-Size="11px" ForeColor="Blue" Text='<%# Eval("noTitulo") %>'></asp:Label>
                                                    <br />
                                                    <asp:Label ID="lblNoProfessor" runat="server" __designer:wfdid="w29" 
                                                        Font-Bold="True" Font-Size="11px" Text='<%# Eval("noProfessor") %>'></asp:Label>
                                                    <br />
                                                    <asp:Label ID="lblLocalItem" runat="server" __designer:wfdid="w48" 
                                                        Text="Local:"></asp:Label>
                                                    <asp:Label ID="lblLocal" runat="server" __designer:wfdid="w29" Font-Bold="True" 
                                                        Font-Size="11px" Text='<%# Eval("noLocal") %>'></asp:Label>
                                                    <br />
                                                    <asp:Label ID="lblDeitem" runat="server" __designer:wfdid="w50" Text="De: "></asp:Label>
                                                    &nbsp;<asp:Label ID="lblDtIni" runat="server" __designer:wfdid="w31" 
                                                        Font-Bold="True" Font-Size="10px" Text='<%# Eval("dtIni") %>'></asp:Label>
                                                    <asp:Label ID="lblAteItem" runat="server" __designer:wfdid="w52" Text=" a: "></asp:Label>
                                                    &nbsp;<asp:Label ID="lblDtTermino" runat="server" __designer:wfdid="w33" 
                                                        Font-Bold="True" Font-Size="10px" Text='<%# Eval("dtTermino") %>'></asp:Label>
                                                    <br />
                                                </td>
                                                
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False" Visible="False">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnRemove" runat="server" __designer:wfdid="w23" 
                                            CausesValidation="False" CommandName="Delete" ImageUrl="~/img/delete 18x18.png" 
                                            Text="Remover" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="noTipoAtividade" HeaderText="Tipo" Visible="False" />
                                <asp:BoundField DataField="noTitulo" HeaderText="Atividade" Visible="False" />
                                <asp:BoundField DataField="dsTema" HeaderText="Tema" Visible="False" />
                                <asp:BoundField DataField="noLocal" HeaderText="Local" Visible="False" />
                                <asp:BoundField DataField="dtIni" HeaderText="Início" Visible="False" />
                                <asp:BoundField DataField="dtTermino" HeaderText="Término" Visible="False" />
                                <asp:BoundField DataField="cdAtividade" HeaderText="cdAtividade" 
                                    Visible="False" />
                                <asp:BoundField DataField="noProfessor" HeaderText="noProfessor" 
                                    Visible="False" />
                                <asp:BoundField DataField="cdTipoAtiviade" HeaderText="cdTipoAtiviade" 
                                    Visible="False" />
                                <asp:BoundField DataField="vlAtividade" HeaderText="Valor" Visible="False" />
                                <asp:BoundField DataField="flAtivo" HeaderText="flAtivo" Visible="False" />
                                <asp:BoundField DataField="flUsado" HeaderText="flUsado" Visible="False" />
                                <asp:BoundField DataField="dtMatricula" HeaderText="dtMatricula" 
                                    Visible="False" />
                                <asp:BoundField DataField="vlDesconto" HeaderText="Descto" Visible="False" />
                                <asp:BoundField DataField="vlMatricula" HeaderText="vlMatricula" 
                                    Visible="False" />
                            </Columns>
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle CssClass="grdlinhapar" />
                        </asp:GridView>
                    </td>
                </tr>
            </tbody>
        </table>
    </asp:Panel>--%>
    <cc1:MessageBox ID="MessageBox1" runat="server" />
    </div>
    </div>
</asp:Content>
