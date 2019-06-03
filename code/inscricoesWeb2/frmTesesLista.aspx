<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmTesesLista.aspx.cs" Inherits="frmTesesLista" Title="Inscri&ccedil;&otilde;es Web" %>

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
            <asp:Label ID="lblTituloPagina" runat="server" CssClass="titulo" Text="Teses Cadastradas"></asp:Label>
            <br />
            <br />
        </h1>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblMsg" runat="server" Font-Size="Small" ForeColor="Red"></asp:Label>
                <br />
                <asp:GridView ID="grdTeses" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="None" OnRowDataBound="grdTeses_RowDataBound"
                    Width="767px" CellSpacing="1">
                    <RowStyle CssClass="grdlinhaimpar" />
                    <AlternatingRowStyle CssClass="grdlinhapar" />
                    <Columns>
                        <asp:BoundField DataField="cdTese" HeaderText=" ID" SortExpression="cdTese">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Height="22px" HorizontalAlign="Center" Width="30px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tpTese" HeaderText="Tipo" SortExpression="tpTese">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="dsAssunto" HeaderText=" Assunto" 
                            SortExpression="dsAssunto">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="dsSituacao" HeaderText="Situação" 
                            SortExpression="dsSituacao" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/img/accept18x18.png" />
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
        <span style="color: rgb(0, 0, 153); font-family: ArialMT; font-size: small; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); display: inline !important; float: none;">
        REGIMENTO INTERNO DO CONGRESSO</span><div 
            style="color: rgb(0, 0, 153); font-family: ArialMT; font-size: small; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255);">
            &nbsp;</div>
        <div style="color: rgb(0, 0, 153); font-family: ArialMT; font-size: small; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255);">
            &quot;Art. 22 &#8211; Cada tese deverá versar sobre assunto relacionado a UM eixo temático 
            do Congresso;</div>
        <div style="color: rgb(0, 0, 153); font-family: ArialMT; font-size: small; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255);">
            &nbsp;</div>
        <div style="color: rgb(0, 0, 153); font-family: ArialMT; font-size: small; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255);">
            §1º.Para ser aceita, a tese deverá ser apresentada por ao menos um participante 
            do Congresso com direito a voz ou voto, ser subscrita por, no mínimo, 10 
            servidores da Carreira Finanças e Controle e estar de acordo com o modelo 
            aprovado pela Comissão Organizadora.</div>
        <div style="color: rgb(0, 0, 153); font-family: ArialMT; font-size: small; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255);">
            &nbsp;</div>
        <div style="color: rgb(0, 0, 153); font-family: ArialMT; font-size: small; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255);">
            §2º.Cada participante poderá apresentar duas teses congressuais, vedada a 
            subscrição em mais de quatro teses.</div>
        <div style="color: rgb(0, 0, 153); font-family: ArialMT; font-size: small; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255);">
            &nbsp;</div>
        <div style="color: rgb(0, 0, 153); font-family: ArialMT; font-size: small; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255);">
            §3º.A Comissão Organizadora fará divulgar as teses aceitas, na ordem de envio 
            das mesmas.</div>
        <div style="color: rgb(0, 0, 153); font-family: ArialMT; font-size: small; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255);">
            &nbsp;</div>
        <div style="color: rgb(0, 0, 153); font-family: ArialMT; font-size: small; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255);">
            Art. 23 &#8211; O autor e/ou autores das teses apresentadas no Congresso, cederão seus 
            direitos autorais para efeito de divulgação das mesmas pelo UNACON Sindical.&quot;</div>
        <br />
        <br />

        <asp:Button ID="btnNovo" runat="server" CausesValidation="False" 
                CssClass="botoes" TabIndex="4" Text="Cadastrar Nova Tese" 
                onclick="btnNovo_Click" />
    </div>
</asp:Content>
