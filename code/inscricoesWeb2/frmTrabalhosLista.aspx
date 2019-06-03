<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmTrabalhosLista.aspx.cs" Inherits="frmTrabalhosLista" Title="Inscri&ccedil;&otilde;es Web" %>

<%@ Register Assembly="ControlMessageBox" Namespace="ControlMessageBox" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="TSManager1" runat="server" EnableScriptLocalization="true"
        CombineScripts="false">
    </asp:ToolkitScriptManager>
    
    <div id="frmCad_auto">
        <h1>
            <asp:Label ID="lblTituloPagina" runat="server" CssClass="titulo" Text="Trabalhos Técnicos"></asp:Label>
            <br />
            <br />
        </h1>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblMsg" runat="server" Font-Size="Small" ForeColor="Red"></asp:Label>
                <br />
                <asp:GridView ID="grdTeses" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="None" OnRowDataBound="grdTeses_RowDataBound"
                    Width="100%" CellSpacing="1">
                    <RowStyle CssClass="grdlinhaimpar" />
                    <AlternatingRowStyle CssClass="grdlinhapar" />
                    <Columns>
                        <asp:BoundField DataField="cdTese" HeaderText=" ID" SortExpression="cdTese">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Height="22px" HorizontalAlign="Center" Width="30px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tpTese" HeaderText="  Modalidade" 
                            SortExpression="tpTese">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="noModalidadeTese1" HeaderText="  Sub Eixo" 
                            SortExpression="noModalidadeTese1">
                            <HeaderStyle Width="200px" />
                            <ItemStyle Width="200px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="dsAssunto" HeaderText="  Título/Assunto" 
                            SortExpression="dsAssunto">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="dsSituacao" HeaderText="  Situação" 
                            SortExpression="dsSituacao" >
                            <ItemStyle Width="150px" />
                        </asp:BoundField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/img/accept18x18_old.png" />
                            </ItemTemplate>
                            <ItemStyle Width="16px" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>

        <br />
        <br />

        <div style="font-weight:bold;">
            <asp:Label ID="lblSubTitulo" runat="server" Text="ATENÇÃO"></asp:Label></div>
        <div>
            <br />
            <asp:Label ID="lblTxtInstrucao" runat="server"></asp:Label>

        </div>
        
        <br />
        <br />
        
        <asp:Button ID="btnNovo" runat="server" CausesValidation="False" 
                CssClass="botoes" TabIndex="4" Text="Cadastrar Novo Trabalho" 
                onclick="btnNovo_Click" />
    </div>
</asp:Content>
