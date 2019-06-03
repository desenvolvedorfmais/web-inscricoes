<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AtividadesAbrasel_old2.aspx.cs" Inherits="AtividadesAbrasel_old2" title="Inscri&ccedil;&otilde;es Web" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="frmCad_auto">
    <div >
         <div id="meuspedidos_esq" style="width:100%">
            <h1>
                <asp:Label ID="lblTituloPagina" runat="server" Text="Seleção de Atividades" CssClass="titulo"></asp:Label><br />
                <br />
            </h1>
            <div style="padding: 5px">
                <asp:Label ID="lblID" runat="server" Width="100px" Text="ID:" CssClass="label"></asp:Label>
                <asp:Label ID="lblIdentificador" runat="server" Font-Bold="True" ForeColor="Navy"></asp:Label>
            </div>
            <div style="padding: 5px">
                <asp:Label ID="lblPart" runat="server" Width="100px" Text="Participante" CssClass="label"></asp:Label>
                <asp:Label ID="lblNoParticipante" runat="server" Font-Bold="True" ForeColor="Navy"></asp:Label>
            </div>
            <div style="padding: 5px">
                <asp:Label ID="lblCateg" runat="server" Width="100px" Text="Categoria" CssClass="label"></asp:Label>
                <asp:Label ID="lblCategoria" runat="server" Font-Bold="True" ForeColor="Navy"></asp:Label>
            </div>
            
            <div id="Div1" style="display:table; float:right" >
            &nbsp;<asp:Button ID="btnGravar2" runat="server" Text="Gravar" CssClass="botoes" 
                onclick="btnGravar_Click" />
            
            &nbsp;<asp:Button ID="btnVoltar2" runat="server" Text="Voltar" CssClass="botoes" 
                    PostBackUrl="~/frmAtividadesParticipante.aspx" Visible="False" />
            </div>
        </div>
    <br />
        <asp:Label ID="Label6" runat="server" 
             Text="Prezado Senhor(a), configure aqui sua grade de atividades."></asp:Label><br />
                <br />
    
    <div id="dados_nova_senha" >
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"  EnableScriptGlobalization="true"
            EnableScriptLocalization="true" CombineScripts="false">
        </asp:ToolkitScriptManager>
        <br />
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate> 
                <asp:Label ID="txtMsg" runat="server" ForeColor="Red"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
        
        <br />
        <div id="dias_WS" runat="server" align="center" >
            <asp:RadioButton ID="chkDia12WS" runat="server" Text="  Dia 17/08" GroupName="dias" 
                    AutoPostBack="True" oncheckedchanged="chkDia13WS_CheckedChanged" 
                Visible="False" />&nbsp;&nbsp;&nbsp; 
            <asp:RadioButton ID="chkDia13WS" runat="server" Text="  Dia 18/08" GroupName="dias" 
                    AutoPostBack="True" oncheckedchanged="chkDia13WS_CheckedChanged" 
                Visible="False" />&nbsp;&nbsp;&nbsp; 
            <asp:RadioButton ID="chkTodosDiasWS" runat="server" Text="  Todos os Dias" 
                    GroupName="dias" AutoPostBack="True" 
                    oncheckedchanged="chkDia13WS_CheckedChanged" Visible="False" /><br /><br />
        </div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate> 
        <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" 
            Width="98%">
            <asp:TabPanel runat="server" HeaderText="Fóruns" ID="workshop" Visible="false">
                <ContentTemplate>
                    <br />
                    <%--<div id="dias_WS" runat="server" align="center" >
                        <asp:RadioButton ID="chkDia12WS" runat="server" Text="  Dia 17/08" GroupName="dias" 
                                AutoPostBack="True" oncheckedchanged="chkDia13WS_CheckedChanged" 
                            Visible="False" />&nbsp;&nbsp;&nbsp; 
                        <asp:RadioButton ID="chkDia13WS" runat="server" Text="  Dia 18/08" GroupName="dias" 
                                AutoPostBack="True" oncheckedchanged="chkDia13WS_CheckedChanged" 
                            Visible="False" />&nbsp;&nbsp;&nbsp; 
                        <asp:RadioButton ID="chkTodosDiasWS" runat="server" Text="  Todos os Dias" 
                                GroupName="dias" AutoPostBack="True" 
                                oncheckedchanged="chkDia13WS_CheckedChanged" Visible="False" /><br /><br />
                    </div>--%>
                <div id="d12_WS" runat="server" visible="False" class="Abrsel_dia12"><br />
                    <center><asp:Label ID="Label1" runat="server" Text="Atividades do dia 17/08" CssClass="titulo"></asp:Label></center><br /><br />
                    <asp:CheckBox ID="F_001606019" runat="server"             
                            Text="&lt;b&gt; Dia 13/08 (10:15 a 11:30 - Auditório) &lt;br/&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;Palestra 1: &lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged" /><br /><br />
                    <asp:CheckBox ID="F_001606020" runat="server" 
                            Text="&lt;b&gt; Dia 13/08 (11:30 a 12:30 - Auditório) &lt;br/&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;Palestra 2: &lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged" /><br /><br />
                    <asp:CheckBox ID="F_001606021" runat="server" 
                            Text="&lt;b&gt; Dia 13/08 (11:30 a 12:30 - Sala IESB) &lt;br/&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;Workshop ABRASEL 1: &lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged" /><br /><br />
                    <asp:CheckBox ID="F_001606022" runat="server" 
                            Text="&lt;b&gt; Dia 13/08 (14:00 a 15:15 - Auditório) &lt;br/&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;Palestra 3: &lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged" /><br /><br />
                    <asp:CheckBox ID="F_001606023" runat="server"    
                            Text="&lt;b&gt; Dia 13/08 (14:00 a 15:15 - Sala IESB) &lt;br/&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;Workshop ABRASEL 2: &lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged"  /><br /><br /><asp:CheckBox ID="F_001606024" runat="server" 
                            
                            Text="&lt;b&gt; Dia 13/08 (15:15 a 16:15 - Auditório) &lt;br/&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;Palestra 4: &lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged" /><br /><br /><br /><br /></div>
                <div id="d13_WS" runat="server" visible="False" class="Abrsel_dia13"><br />
                    <center>
                        <asp:Label ID="Label2" runat="server" Text="Atividades do dia 18/08" CssClass="titulo"></asp:Label>
                    </center><br /><br />
                    <asp:CheckBox ID="F_001606025" runat="server" 
                            
                            Text="&lt;b&gt; Dia 14/08 (10:15 a 11:30 - Auditório) <br/>&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;Palestra 6: &lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged" /><br /><br />
                    <asp:CheckBox ID="F_001606026" runat="server"                         
                            
                            Text="<b> Dia 14/08 (10:15 a 11:30 - Sala IESB) <br/>&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;Workshop ABRASEL 3: </b>" 
                            AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged" /><br /><br />
                    <asp:CheckBox ID="F_001606027" runat="server" 
                            
                            Text="&lt;b&gt; Dia 14/08 (11:30 a 12:30 - Auditório) &lt;br/&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;Palestra 7: &lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged" /><br /><br />
                    <asp:CheckBox ID="F_001606028" runat="server" 
                            
                            Text="&lt;b&gt; Dia 14/08 (11:30 a 12:30 - Sala IESB) &lt;br/&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;Workshop ABRASEL 4: &lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged"  /><br /><br />
                    <asp:CheckBox ID="F_001606029" runat="server"                         
                            
                            Text="&lt;b&gt; Dia 14/08 (14:00 a 15:15 - Auditório) &lt;br/&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;Palestra 8: &lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged"  /><br /><br />
                    <asp:CheckBox ID="F_001606030" runat="server"                         
                            
                            Text="&lt;b&gt; Dia 14/08 (14:00 a 15:15 - Sala IESB) &lt;br/&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;Workshop ABRSEL 5: &lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged"  /><br /><br /><br />
                   </div>

                </ContentTemplate>

            </asp:TabPanel>            
            <asp:TabPanel ID="Mesa" runat="server" HeaderText="Aulas Show" Visible="false">
                <ContentTemplate><br />
                    <%--<div id="dias_F" runat="server" align="center" >
                        <asp:RadioButton ID="chkDia12M" runat="server" Text="  Dia 17/08" GroupName="dias" 
                                AutoPostBack="True" oncheckedchanged="chkDia13M_CheckedChanged" 
                            Visible="False" />&nbsp;&nbsp;&nbsp; 
                        <asp:RadioButton ID="chkDia13M" runat="server" Text="  Dia 18/08" GroupName="dias" 
                                AutoPostBack="True" oncheckedchanged="chkDia13M_CheckedChanged" 
                            Visible="False" />&nbsp;&nbsp;&nbsp; 
                        <asp:RadioButton ID="chkTodosDiasM" runat="server" Text="  Todos os Dias" 
                                GroupName="dias" AutoPostBack="True" 
                            oncheckedchanged="chkDia13M_CheckedChanged" Visible="False" /><br /><br />

                    </div>--%>
                    <div id="d12_M" runat="server" class="Abrsel_dia13"><br />
                        <center><asp:Label ID="Label3" runat="server" Text="Atividades do dia 17/08" CssClass="titulo"></asp:Label></center><br /><br />
                        <asp:CheckBox ID="M_001606031" runat="server"                         
                            
                            Text="&lt;b&gt; Dia 13/08 (10:15 a 11:30 - COZINHA 2)  &lt;font color='navy'&gt;- Emerson Montavani&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged" /><br /><br />
                        <asp:CheckBox ID="M_001606032" runat="server" 
                            
                            Text="&lt;b&gt; Dia 13/08 (11:30 a 11:45 - COZINHA 1)  &lt;font color='navy'&gt;- Sebastian Parasole&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged" /><br /><br />
                        <asp:CheckBox ID="M_001606033" runat="server" 
                                                    
                            Text="&lt;b&gt; Dia 13/08 (12:15 a 12:30 - CONFEITARIA/PADARIA)  &lt;font color='navy'&gt;- Daniel Briand&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged" /><br /><br />
                        <asp:CheckBox ID="M_001606034" runat="server" 
                        
                            
                            Text="&lt;b&gt; Dia 13/08 (12:30 a 12:45 - COZINHA 2)  &lt;font color='navy'&gt;- Alexandre Albanese&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged" /><br /><br />
                        <asp:CheckBox ID="M_001606035" runat="server"                         
                            
                            Text="&lt;b&gt; Dia 13/08 (13:00 a 13:15 - COZINHA 1)  &lt;font color='navy'&gt;- Agenor Maia&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged" /><br /><br />
                        <asp:CheckBox ID="M_001606036" runat="server" 
                                                    
                            Text="&lt;b&gt; Dia 13/08 (13:15 a 13:30 - CONFEITARIA/PADARIA)  &lt;font color='navy'&gt;- Dudu Camargo&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged" /><br /><br />
                        <asp:CheckBox ID="M_001606037" runat="server" 
                            
                            Text="&lt;b&gt; Dia 13/08 (13:30 a 13:45 - COZINHA 2)  &lt;font color='navy'&gt;- Gil Guimarães&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged"  /><br /><br />
                        <asp:CheckBox ID="M_001606038" runat="server" 
                                                    
                            Text="&lt;b&gt; Dia 13/08 (14:30 a 14:45 - COZINHA 1)  &lt;font color='navy'&gt;- Maria Victoria&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged"  /><br /><br />
                        <asp:CheckBox ID="M_001606039" runat="server" 
                        
                            Text="&lt;b&gt; Dia 13/08 (14:45 a 15:00 - CONFEITARIA/PADARIA)  &lt;font color='navy'&gt;- Eduardo Sedelmaier&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged" /><br /><br />
                        <asp:CheckBox ID="M_001606040" runat="server" 
                            
                            Text="&lt;b&gt; Dia 13/08 (15:00 a 15:15 - COZINHA 2)  &lt;font color='navy'&gt;- Francisco Assilero&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged" /><br /><br />
                        <asp:CheckBox ID="M_001606041" runat="server"                         
                            
                            Text="&lt;b&gt; Dia 13/08 (16:00 a 16:15 - COZINHA 1)  &lt;font color='navy'&gt;- Renata Carvalho&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged" /><br /><br />
                        <asp:CheckBox ID="M_001606042" runat="server" 
                            
                            Text="&lt;b&gt; Dia 13/08 (16:15 a 16:30 - CONFEITARIA/PADARIA)  &lt;font color='navy'&gt;- Fabiana Pinheiro&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged" /><br /><br />
                        <asp:CheckBox ID="M_001606043" runat="server" 
                                                    
                            Text="&lt;b&gt; Dia 13/08 (16:30 a 16:45 - COZINHA 2)  &lt;font color='navy'&gt;- Paulo Mello&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged" /><br /><br />
                        <asp:CheckBox ID="M_001606044" runat="server" 
                                                    
                            Text="&lt;b&gt; Dia 13/08 (17:15 a 17:30 - CONFEITARIA/PADARIA)  &lt;font color='navy'&gt;- Kaka Silva&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged" /><br /><br />
                        <asp:CheckBox ID="M_001606045" runat="server"                         
                            
                            Text="&lt;b&gt; Dia 13/08 (17:30 a 17:45 - COZINHA 1)  &lt;font color='navy'&gt;- Manu Buffara&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged" /><br /><br />
                        <asp:CheckBox ID="M_001606046" runat="server" 
                                                    
                            Text="&lt;b&gt; Dia 13/08 (17:45 a 18:00 - COZINHA 2)  &lt;font color='navy'&gt;- Juan Pratgineztos&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged" /><br /><br />
                        <asp:CheckBox ID="M_001606047" runat="server" 
                            
                            Text="&lt;b&gt; Dia 13/08 (18:15 a 18:30 - CONFEITARIA/PADARIA)  &lt;font color='navy'&gt;- Vini Ferreira&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged"  /><br /><br /><br /><br />

                </div>
                    <div id="d13_M" runat="server" class="Abrsel_dia14"><br />
                        <center>
                            <asp:Label ID="Label4" runat="server" Text="Atividades do dia 18/08" CssClass="titulo"></asp:Label>

                        </center><br /><br />
                        <asp:CheckBox ID="M_001606050" runat="server"                         
                            
                                Text="&lt;b&gt; Dia 14/08 (11:00 a 11:15 - COZINHA 2)  &lt;font color='navy'&gt;- Lui Veronse&lt;/font&gt;&lt;/b&gt;" 
                                AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged" /><br /><br />
                        <asp:CheckBox ID="M_001606051" runat="server" 
                            
                                Text="&lt;b&gt; Dia 14/08 (11:30 a 11:45 - COZINHA 1)  &lt;font color='navy'&gt;- Mara Alcamin&lt;/font&gt;&lt;/b&gt;" 
                                AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged" /><br /><br />
                        <asp:CheckBox ID="M_001606052" runat="server" 
                                                    
                                Text="&lt;b&gt; Dia 14/08 (11:45 a 12:00 - CONFEITARIA/PADARIA)  &lt;font color='navy'&gt;- Ryoso Momiya&lt;/font&gt;&lt;/b&gt;" 
                                AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged" /><br /><br />
                        <asp:CheckBox ID="M_001606053" runat="server" 
                        
                            
                                Text="&lt;b&gt; Dia 14/08 (12:15 a 12:30 - COZINHA 2)  &lt;font color='navy'&gt;- Meg Suda&lt;/font&gt;&lt;/b&gt;" 
                                AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged" /><br /><br />
                        <asp:CheckBox ID="M_001606054" runat="server"                         
                            
                                Text="&lt;b&gt; Dia 14/08 (13:00 a 13:15 - COZINHA 1)  &lt;font color='navy'&gt;- Onildo Rocha&lt;/font&gt;&lt;/b&gt;" 
                                AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged" /><br /><br />
                        <asp:CheckBox ID="M_001606055" runat="server" 
                                                    
                                Text="&lt;b&gt; Dia 14/08 (13:15 a 13:30 - CONFEITARIA/PADARIA)  &lt;font color='navy'&gt;- Natasha Franco&lt;/font&gt;&lt;/b&gt;" 
                                AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged" /><br /><br />
                        <asp:CheckBox ID="M_001606056" runat="server" 
                            
                                Text="&lt;b&gt; Dia 14/08 (13:30 a 13:45 - COZINHA 2)  &lt;font color='navy'&gt;- Claude Capdeville&lt;/font&gt;&lt;/b&gt;" 
                                AutoPostBack="True" oncheckedchanged="M_001606056_CheckedChanged"  /><br /><br />
                        <asp:CheckBox ID="M_001606057" runat="server" 
                                                    
                                Text="&lt;b&gt; Dia 14/08 (14:30 a 14:45 - COZINHA 1)  &lt;font color='navy'&gt;- Andréia Tinoco&lt;/font&gt;&lt;/b&gt;" 
                                AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged"  /><br /><br />
                        <asp:CheckBox ID="M_001606058" runat="server" 
                        
                                Text="&lt;b&gt; Dia 14/08 (14:30 a 14:45 - CONFEITARIA/PADARIA)  &lt;font color='navy'&gt;- Daniela Barreira&lt;/font&gt;&lt;/b&gt;" 
                                AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged" /><br /><br />
                        <asp:CheckBox ID="M_001606059" runat="server" 
                            
                                Text="&lt;b&gt; Dia 14/08 (14:45 a 15:00 - COZINHA 2)  &lt;font color='navy'&gt;- Marco Espinoza&lt;/font&gt;&lt;/b&gt;" 
                                AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged" /><br /><br />
                        <asp:CheckBox ID="M_001606060" runat="server"                         
                            
                                Text="&lt;b&gt; Dia 14/08 (15:45 a 16:00 - CONFEITARIA/PADARIA)  &lt;font color='navy'&gt;- Marcelo Petrarca&lt;/font&gt;&lt;/b&gt;" 
                                AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged" /><br /><br />
                        <asp:CheckBox ID="M_001606061" runat="server" 
                            
                                Text="&lt;b&gt; Dia 14/08 (16:00 a 16:15 - COZINHA 1)  &lt;font color='navy'&gt;- Marcos Livi&lt;/font&gt;&lt;/b&gt;" 
                                AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged" /><br /><br />
                        <asp:CheckBox ID="M_001606062" runat="server" 
                                                    
                                Text="&lt;b&gt; Dia 14/08 (16:15 a 16:30 - COZINHA 2)  &lt;font color='navy'&gt;- Rodrigo Cabral&lt;/font&gt;&lt;/b&gt;" 
                                AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged" /><br /><br />
                        <asp:CheckBox ID="M_001606063" runat="server" 
                                                    
                                Text="&lt;b&gt; Dia 14/08 (17:00 a 17:15 - CONFEITARIA/PADARIA)  &lt;font color='navy'&gt;- Rita Medeiros&lt;/font&gt;&lt;/b&gt;" 
                                AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged" /><br /><br />
                        <asp:CheckBox ID="M_001606064" runat="server"                         
                            
                                Text="&lt;b&gt; Dia 14/08 (17:15 a 17:30 - COZINHA 2)  &lt;font color='navy'&gt;- Marcelo Piucco&lt;/font&gt;&lt;/b&gt;" 
                                AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged" /><br /><br />
                        <asp:CheckBox ID="M_001606065" runat="server" 
                                                    
                                Text="&lt;b&gt; Dia 14/08 (17:30 a 17:45 - COZINHA 1)  &lt;font color='navy'&gt;- Junior Durski&lt;/font&gt;&lt;/b&gt;" 
                                AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged" /><br /><br />
                        <asp:CheckBox ID="M_001606066" runat="server" 
                            
                                Text="&lt;b&gt; Dia 14/08 (18:15 a 18:30 - CONFEITARIA/PADARIA)  &lt;font color='navy'&gt;- Rossário Tessier&lt;/font&gt;&lt;/b&gt;" 
                                AutoPostBack="True" oncheckedchanged="F_001606019_CheckedChanged"  /><br /><br /> 
                    </div>
                </ContentTemplate>

            </asp:TabPanel>
            
        </asp:TabContainer>
        </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <div id="btngrava" style="display:table; float:right" >
        &nbsp;<asp:Button ID="btnGravar" runat="server" Text="Gravar" CssClass="botoes" 
            onclick="btnGravar_Click" />
            
        &nbsp;<asp:Button ID="Button1" runat="server" Text="Voltar" CssClass="botoes" 
                PostBackUrl="~/frmAtividadesParticipante.aspx" Visible="False" />
        </div>
        
    </div>
    </div>    
</div>
</asp:Content>

