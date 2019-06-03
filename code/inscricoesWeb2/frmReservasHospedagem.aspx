<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmReservasHospedagem.aspx.cs" Inherits="pages_frmReservasHospedagem"
    Title="Inscri&ccedil;&otilde;es Web - Administrativo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="frmCad_auto">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
            EnableScriptLocalization="true">
        </asp:ToolkitScriptManager>
        <h3>
            Reserva de Hospedagem<br />
        </h3>
        <div>
            <asp:Label id="Label4" runat="server" Text="ID" Width="100px" CssClass="label"></asp:Label>
                            <asp:Label ID="lblIdentificador" runat="server" ForeColor="Navy" Font-Bold="True"></asp:Label>
               
            </div>
            <div><asp:Label id="Label13" runat="server" Text="Participante" Width="100px" CssClass="label"></asp:Label>
                            <asp:Label ID="lblNoParticipante" runat="server" ForeColor="Navy" Font-Bold="True"></asp:Label>
            </div>
            <div><asp:Label id="Label22" runat="server" Text="Categoria" Width="100px" CssClass="label"></asp:Label>
                            <asp:Label ID="lblCategoria" runat="server" ForeColor="Navy" Font-Bold="True"></asp:Label>
            </div>

            <div>
                <br />
                <asp:Label id="Label1" runat="server" Text="Tipo" Width="100px" CssClass="label"></asp:Label>
                            <asp:Label ID="lblAtividade" runat="server" ForeColor="Red" Font-Bold="True" Font-Size="Large"></asp:Label>
            </div>
            <div>
                <asp:Label id="Label2part" runat="server" Text="2º Participante" Width="100px" CssClass="label" Visible="false"></asp:Label>
                            <asp:Label ID="lblSegundoParticipante" runat="server" ForeColor="Red" Font-Bold="True" Visible="false"></asp:Label>
            </div> 
            <br />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            <div>
            
                <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
            
            </div>
            <div> 
                <asp:Label ID="Label5" runat="server" Text="Hotel"></asp:Label>
                &nbsp;<br />
                <asp:Label ID="txtHotel" runat="server" Font-Bold="True" ForeColor="Navy"></asp:Label>
                <br />
                &nbsp;</div>
            <div> 
                <asp:Label ID="Label2" runat="server" Text="Tipo Acomodação"></asp:Label>
                <br />
                <asp:Label ID="txtTipoAcomodacao" runat="server" Font-Bold="True" 
                    ForeColor="Navy"></asp:Label>
                <br />
                &nbsp;</div>
            <div> 
                <asp:Label ID="Label6" runat="server" Text="Situação"></asp:Label>
                <br />
                 <asp:Label ID="lblSituacao" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                <br />
                &nbsp;</div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
                    <asp:Button ID="btnConfResrva" runat="server" CssClass="botoes"
                        TabIndex="2" Text="Confirmar Reserva" 
            onclick="btnConfResrva_Click" Visible="False" />
        </div>
       
        
    

</asp:Content>

