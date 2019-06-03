<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AtividadesAbrasel_old.aspx.cs" Inherits="AtividadesAbrasel" title="Inscri&ccedil;&otilde;es Web" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="frmCad_auto">
    <div style="width:680px; ">
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
                    PostBackUrl="~/frmAtividadesParticipante.aspx" />
            </div>
        </div>
    <br />
        <center><asp:Label ID="Label6" runat="server" 
             Text="Prezado Senhor(a), configure aqui sua grade de atividades."></asp:Label><br /></center>
                <br />
    
    <div id="dados_nova_senha" style="width:680px">
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
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate> 
        <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" 
            Width="680px">
            <asp:TabPanel runat="server" HeaderText="Fórum + Workshop" ID="workshop" Visible="true">
            
                <ContentTemplate>
                    <br />
                    <div id="dias_WS" runat="server" align="center" >
        
                        <asp:RadioButton ID="chkDia13WS" runat="server" Text="  Dia 13/08" GroupName="dias" 
                                AutoPostBack="True" oncheckedchanged="chkDia13WS_CheckedChanged" 
                            Visible="False" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="chkDia14WS" runat="server" Text="  Dia 14/08" GroupName="dias" 
                                AutoPostBack="True" oncheckedchanged="chkDia13WS_CheckedChanged" 
                            Visible="False" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="chkTodosDiasWS" runat="server" Text="  Todos os Dias" 
                                GroupName="dias" AutoPostBack="True" 
                                oncheckedchanged="chkDia13WS_CheckedChanged" Visible="False" />
                        <br /><br />
                    </div>
                    <div id="d13_WS" runat="server" visible="False" class="Abrsel_dia13">
                        <br />
                            <center><asp:Label ID="Label1" runat="server" Text="Atividades do dia 13/08" CssClass="titulo"></asp:Label></center>
                        <br />
                        <br />
                        <asp:CheckBox ID="F_001604019" runat="server"                         
                            
                            Text="&lt;b&gt; Dia 13/08 (10:15 a 11:30 - Auditório) &lt;br/&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;Palestra 1: &lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="F_001604019_CheckedChanged" />
                        <br />
                        <br />
                        <asp:CheckBox ID="F_001604020" runat="server" 
                            
                            Text="&lt;b&gt; Dia 13/08 (11:30 a 12:30 - Auditório) &lt;br/&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;Palestra 2: &lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="F_001604020_CheckedChanged" />
                        <br />
                        <br />
                        <asp:CheckBox ID="W_001604011" runat="server" 
                        
                            
                            Text="&lt;b&gt; Dia 13/08 (11:30 a 12:30 - Sala IESB) &lt;br/&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;Workshop ABRASEL 1: &lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="W_001604011_CheckedChanged" />
                        <br />
                        <br />
                        <asp:CheckBox ID="F_001604021" runat="server" 
                        
                            
                            Text="&lt;b&gt; Dia 13/08 (14:00 a 15:15 - Auditório) &lt;br/&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;Palestra 3: &lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="F_001604021_CheckedChanged" />
                        <br />
                        <br />
                        <asp:CheckBox ID="W_001604072" runat="server"                         
                            
                            Text="&lt;b&gt; Dia 13/08 (14:00 a 15:15 - Sala IESB) &lt;br/&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;Workshop ABRASEL 2: &lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="W_001604072_CheckedChanged"  />
                        <br />
                        <br />
                        <asp:CheckBox ID="F_001604022" runat="server" 
                            
                            Text="&lt;b&gt; Dia 13/08 (15:15 a 16:15 - Auditório) &lt;br/&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;Palestra 4: &lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="F_001604022_CheckedChanged" />
                        <br />
                        <br />
                        <asp:CheckBox ID="W_001604012" runat="server"                         
                            
                            Text="&lt;b&gt; Dia 13/08 (15:15 a 16:15 - Sala IESB) &lt;br/&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;Workshop SEBRAE 1: &lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="W_001604012_CheckedChanged" />
                        <br />
                        <br />
                        <asp:CheckBox ID="F_001604023" runat="server" 
                            
                            Text="&lt;b&gt; Dia 13/08 (16:45 a 18:00 - Auditório) &lt;br/&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;Mesa Redonda: &lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="F_001604023_CheckedChanged"  />
                        <br />
                        <br />
                        <asp:CheckBox ID="W_001604013" runat="server" 
                        
                            
                            Text="&lt;b&gt; Dia 13/08 (16:45 a 17:45 - Sala IESB) &lt;br/&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;Workshop SEBRAE 2: &lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="W_001604013_CheckedChanged"  />
                        <br />
                        <br />
                        <asp:CheckBox ID="F_001604024" runat="server" 
                            
                            Text="&lt;b&gt; Dia 13/08 (18:00 a 19:00 - Auditório) &lt;br/&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;Palestra 5: &lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="F_001604024_CheckedChanged" />
                        <br />
                        <br />
                    </div>
                    <div id="d14_WS" runat="server" visible="False" class="Abrsel_dia14">
                        <br />
                            <center><asp:Label ID="Label2" runat="server" Text="Atividades do dia 14/08" CssClass="titulo"></asp:Label></center>
                        <br />
                        <br />
                        <asp:CheckBox ID="F_001604025" runat="server" 
                            
                            Text="&lt;b&gt; Dia 14/08 (10:15 a 11:30 - Auditório) <br/>&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;Palestra 6: &lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="F_001604025_CheckedChanged" />
                        <br />
                        <br />
                        <asp:CheckBox ID="W_001604014" runat="server"                         
                            
                            Text="<b> Dia 14/08 (10:15 a 11:30 - Sala IESB) <br/>&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;Workshop ABRASEL 3: </b>" 
                            AutoPostBack="True" oncheckedchanged="W_001604014_CheckedChanged" />
                        <br />
                        <br />
                        <asp:CheckBox ID="F_001604026" runat="server" 
                            
                            Text="&lt;b&gt; Dia 14/08 (11:30 a 12:30 - Auditório) &lt;br/&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;Palestra 7: &lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="F_001604026_CheckedChanged" />
                        <br />
                        <br />
                        <asp:CheckBox ID="W_001604015" runat="server" 
                            
                            Text="&lt;b&gt; Dia 14/08 (11:30 a 12:30 - Sala IESB) &lt;br/&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;Workshop ABRASEL 4: &lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="W_001604015_CheckedChanged"  />
                        <br />
                        <br />
                        <asp:CheckBox ID="F_001604027" runat="server"                         
                            
                            Text="&lt;b&gt; Dia 14/08 (14:00 a 15:15 - Auditório) &lt;br/&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;Palestra 8: &lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="F_001604027_CheckedChanged"  />
                        <br />
                        <br />
                        <asp:CheckBox ID="W_001604073" runat="server"                         
                            
                            Text="&lt;b&gt; Dia 14/08 (14:00 a 15:15 - Sala IESB) &lt;br/&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;Workshop ABRSEL 5: &lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="W_001604073_CheckedChanged"  />
                        <br />
                        <br />
                        <asp:CheckBox ID="F_001604028" runat="server" 
                            
                            Text="&lt;b&gt; Dia 14/08 (15:15 a 16:15 - Auditório) &lt;br/&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;Palestra 9: &lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="F_001604028_CheckedChanged" />
                        <br />
                        <br />
                        <asp:CheckBox ID="W_001604016" runat="server"                         
                            
                            Text="&lt;b&gt; Dia 14/08 (15:15 a 16:15 - Sala IESB) &lt;br/&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;Workshop SEBRAE 3: &lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="W_001604016_CheckedChanged" />
                        <br />
                        <br />
                        <asp:CheckBox ID="F_001604029" runat="server"                         
                            
                            Text="&lt;b&gt; Dia 14/08 (16:45 a 17:45 - Auditório) &lt;br/&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;Palestra 10: &lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="F_001604029_CheckedChanged"  />
                        <br />
                        <br />
                        <asp:CheckBox ID="W_001604017" runat="server" 
                        
                            
                            Text="&lt;b&gt; Dia 14/08 (16:45 a 17:45 - Sala IESB) &lt;br/&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;Workshop SEBRAE 4: &lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="W_001604017_CheckedChanged" />
                        <br />
                        <br />
                        <asp:CheckBox ID="F_001604030" runat="server" 
                            
                            Text="&lt;b&gt; Dia 14/08 (18:00 a 19:00 - Auditório) &lt;br/&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;Palestra 11: &lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="F_001604030_CheckedChanged" 
                            Visible="False" />
                        <br />
                        <br />
                    </div>                    
                </ContentTemplate>
            </asp:TabPanel>            
            <asp:TabPanel ID="Mesa" runat="server" HeaderText="Oficinas Gastronômicas" Visible="false">
                <ContentTemplate>
                    <br />
                    <div id="dias_F" runat="server" align="center" >
        
                        <asp:RadioButton ID="chkDia13M" runat="server" Text="  Dia 13/08" GroupName="dias" 
                                AutoPostBack="True" oncheckedchanged="chkDia13M_CheckedChanged" 
                            Visible="False" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="chkDia14M" runat="server" Text="  Dia 14/08" GroupName="dias" 
                                AutoPostBack="True" oncheckedchanged="chkDia13M_CheckedChanged" 
                            Visible="False" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="chkTodosDiasM" runat="server" Text="  Todos os Dias" 
                                GroupName="dias" AutoPostBack="True" 
                            oncheckedchanged="chkDia13M_CheckedChanged" Visible="False" />
                        <br /><br />
                    </div>
                    <div id="d13_M" runat="server" visible="False" class="Abrsel_dia13">
                        <br />
                            <center><asp:Label ID="Label3" runat="server" Text="Atividades do dia 13/08" CssClass="titulo"></asp:Label></center>
                        <br />
                        <br />
                        <asp:CheckBox ID="M_001604031" runat="server"                         
                            
                            Text="&lt;b&gt; Dia 13/08 (10:15 a 11:30 - COZINHA 2)  &lt;font color='navy'&gt;- Emerson Montavani&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="M_001604031_CheckedChanged" />
                        <br />
                        <br />
                        <asp:CheckBox ID="M_001604032" runat="server" 
                            
                            Text="&lt;b&gt; Dia 13/08 (11:30 a 11:45 - COZINHA 1)  &lt;font color='navy'&gt;- Sebastian Parasole&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="M_001604032_CheckedChanged" />
                        <br />
                        <br />
                        <asp:CheckBox ID="M_001604033" runat="server" 
                                                    
                            Text="&lt;b&gt; Dia 13/08 (12:15 a 12:30 - CONFEITARIA/PADARIA)  &lt;font color='navy'&gt;- Daniel Briand&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="M_001604033_CheckedChanged" />
                        <br />
                        <br />
                        <asp:CheckBox ID="M_001604034" runat="server" 
                        
                            
                            Text="&lt;b&gt; Dia 13/08 (12:30 a 12:45 - COZINHA 2)  &lt;font color='navy'&gt;- Alexandre Albanese&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="M_001604034_CheckedChanged" />
                        <br />
                        <br />
                        <asp:CheckBox ID="M_001604035" runat="server"                         
                            
                            Text="&lt;b&gt; Dia 13/08 (13:00 a 13:15 - COZINHA 1)  &lt;font color='navy'&gt;- Agenor Maia&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="M_001604035_CheckedChanged" />
                        <br />
                        <br />
                        <asp:CheckBox ID="M_001604036" runat="server" 
                                                    
                            Text="&lt;b&gt; Dia 13/08 (13:15 a 13:30 - CONFEITARIA/PADARIA)  &lt;font color='navy'&gt;- Dudu Camargo&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="M_001604036_CheckedChanged" />
                        <br />
                        <br />
                        <asp:CheckBox ID="M_001604037" runat="server" 
                            
                            Text="&lt;b&gt; Dia 13/08 (13:30 a 13:45 - COZINHA 2)  &lt;font color='navy'&gt;- Gil Guimarães&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="M_001604037_CheckedChanged"  />
                        <br />
                        <br />
                        <asp:CheckBox ID="M_001604038" runat="server" 
                                                    
                            Text="&lt;b&gt; Dia 13/08 (14:30 a 14:45 - COZINHA 1)  &lt;font color='navy'&gt;- Maria Victoria&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="M_001604038_CheckedChanged"  />
                        <br />
                        <br />
                        <asp:CheckBox ID="M_001604039" runat="server" 
                        
                            Text="&lt;b&gt; Dia 13/08 (14:45 a 15:00 - CONFEITARIA/PADARIA)  &lt;font color='navy'&gt;- Eduardo Sedelmaier&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="M_001604039_CheckedChanged" />
                        <br />
                        <br />
                        <asp:CheckBox ID="M_001604040" runat="server" 
                            
                            Text="&lt;b&gt; Dia 13/08 (15:00 a 15:15 - COZINHA 2)  &lt;font color='navy'&gt;- Francisco Assilero&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="M_001604040_CheckedChanged" />
                        <br />
                        <br />
                        <asp:CheckBox ID="M_001604041" runat="server"                         
                            
                            Text="&lt;b&gt; Dia 13/08 (16:00 a 16:15 - COZINHA 1)  &lt;font color='navy'&gt;- Renata Carvalho&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="M_001604041_CheckedChanged" />
                        <br />
                        <br />
                        <asp:CheckBox ID="M_001604042" runat="server" 
                            
                            Text="&lt;b&gt; Dia 13/08 (16:15 a 16:30 - CONFEITARIA/PADARIA)  &lt;font color='navy'&gt;- Fabiana Pinheiro&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="M_001604042_CheckedChanged" />
                        <br />
                        <br />
                        <asp:CheckBox ID="M_001604043" runat="server" 
                                                    
                            Text="&lt;b&gt; Dia 13/08 (16:30 a 16:45 - COZINHA 2)  &lt;font color='navy'&gt;- Paulo Mello&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="M_001604043_CheckedChanged" />
                        <br />
                        <br />
                        <asp:CheckBox ID="M_001604044" runat="server" 
                                                    
                            Text="&lt;b&gt; Dia 13/08 (17:15 a 17:30 - CONFEITARIA/PADARIA)  &lt;font color='navy'&gt;- Kaka Silva&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="M_001604044_CheckedChanged" />
                        <br />
                        <br />
                        <asp:CheckBox ID="M_001604045" runat="server"                         
                            
                            Text="&lt;b&gt; Dia 13/08 (17:30 a 17:45 - COZINHA 1)  &lt;font color='navy'&gt;- Manu Buffara&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="M_001604045_CheckedChanged" />
                        <br />
                        <br />
                        <asp:CheckBox ID="M_001604046" runat="server" 
                                                    
                            Text="&lt;b&gt; Dia 13/08 (17:45 a 18:00 - COZINHA 2)  &lt;font color='navy'&gt;- Juan Pratgineztos&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="M_001604046_CheckedChanged" />
                        <br />
                        <br />
                        <asp:CheckBox ID="M_001604047" runat="server" 
                            
                            Text="&lt;b&gt; Dia 13/08 (18:15 a 18:30 - CONFEITARIA/PADARIA)  &lt;font color='navy'&gt;- Vini Ferreira&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="M_001604047_CheckedChanged"  />
                        <br />
                        <br />
                        <asp:CheckBox ID="M_001604048" runat="server" 
                            
                            Text="&lt;b&gt; Dia 13/08 (18:45 a 19:00 - COZINHA 2)  &lt;font color='navy'&gt;- Ronnye Peterson&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="M_001604048_CheckedChanged"  />
                        <br />
                        <br />
                    </div>
                    <div id="d14_M" runat="server" visible="False" class="Abrsel_dia14">
                        <br />
                            <center><asp:Label ID="Label4" runat="server" Text="Atividades do dia 14/08" CssClass="titulo"></asp:Label></center>
                        <br />
                        <br />
                        <asp:CheckBox ID="M_001604049" runat="server"                         
                            
                            Text="&lt;b&gt; Dia 14/08 (11:00 a 11:15 - COZINHA 2)  &lt;font color='navy'&gt;- Lui Veronse&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="M_001604049_CheckedChanged" />
                        <br />
                        <br />
                        <asp:CheckBox ID="M_001604050" runat="server" 
                            
                            Text="&lt;b&gt; Dia 14/08 (11:30 a 11:45 - COZINHA 1)  &lt;font color='navy'&gt;- Mara Alcamin&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="M_001604050_CheckedChanged" />
                        <br />
                        <br />
                        <asp:CheckBox ID="M_001604051" runat="server" 
                                                    
                            Text="&lt;b&gt; Dia 14/08 (11:45 a 12:00 - CONFEITARIA/PADARIA)  &lt;font color='navy'&gt;- Ryoso Momiya&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="M_001604051_CheckedChanged" />
                        <br />
                        <br />
                        <asp:CheckBox ID="M_001604052" runat="server" 
                        
                            
                            Text="&lt;b&gt; Dia 14/08 (12:15 a 12:30 - COZINHA 2)  &lt;font color='navy'&gt;- Meg Suda&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="M_001604052_CheckedChanged" />
                        <br />
                        <br />
                        <asp:CheckBox ID="M_001604053" runat="server"                         
                            
                            Text="&lt;b&gt; Dia 14/08 (13:00 a 13:15 - COZINHA 1)  &lt;font color='navy'&gt;- Onildo Rocha&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="M_001604053_CheckedChanged" />
                        <br />
                        <br />
                        <asp:CheckBox ID="M_001604054" runat="server" 
                                                    
                            Text="&lt;b&gt; Dia 14/08 (13:15 a 13:30 - CONFEITARIA/PADARIA)  &lt;font color='navy'&gt;- Natasha Franco&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="M_001604054_CheckedChanged" />
                        <br />
                        <br />
                        <asp:CheckBox ID="M_001604055" runat="server" 
                            
                            Text="&lt;b&gt; Dia 14/08 (13:30 a 13:45 - COZINHA 2)  &lt;font color='navy'&gt;- Claude Capdeville&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="M_001604055_CheckedChanged"  />
                        <br />
                        <br />
                        <asp:CheckBox ID="M_001604056" runat="server" 
                                                    
                            Text="&lt;b&gt; Dia 14/08 (14:30 a 14:45 - COZINHA 1)  &lt;font color='navy'&gt;- Andréia Tinoco&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="M_001604056_CheckedChanged"  />
                        <br />
                        <br />
                        <asp:CheckBox ID="M_001604057" runat="server" 
                        
                            Text="&lt;b&gt; Dia 14/08 (14:30 a 14:45 - CONFEITARIA/PADARIA)  &lt;font color='navy'&gt;- Daniela Barreira&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="M_001604057_CheckedChanged" />
                        <br />
                        <br />
                        <asp:CheckBox ID="M_001604058" runat="server" 
                            
                            Text="&lt;b&gt; Dia 14/08 (14:45 a 15:00 - COZINHA 2)  &lt;font color='navy'&gt;- Marco Espinoza&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="M_001604058_CheckedChanged" />
                        <br />
                        <br />
                        <asp:CheckBox ID="M_001604059" runat="server"                         
                            
                            Text="&lt;b&gt; Dia 14/08 (15:45 a 16:00 - CONFEITARIA/PADARIA)  &lt;font color='navy'&gt;- Marcelo Petrarca&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="M_001604059_CheckedChanged" />
                        <br />
                        <br />
                        <asp:CheckBox ID="M_001604060" runat="server" 
                            
                            Text="&lt;b&gt; Dia 14/08 (16:00 a 16:15 - COZINHA 1)  &lt;font color='navy'&gt;- Marcos Livi&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="M_001604060_CheckedChanged" />
                        <br />
                        <br />
                        <asp:CheckBox ID="M_001604061" runat="server" 
                                                    
                            Text="&lt;b&gt; Dia 14/08 (16:15 a 16:30 - COZINHA 2)  &lt;font color='navy'&gt;- Rodrigo Cabral&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="M_001604061_CheckedChanged" />
                        <br />
                        <br />
                        <asp:CheckBox ID="M_001604062" runat="server" 
                                                    
                            Text="&lt;b&gt; Dia 14/08 (17:00 a 17:15 - CONFEITARIA/PADARIA)  &lt;font color='navy'&gt;- Rita Medeiros&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="M_001604062_CheckedChanged" />
                        <br />
                        <br />
                        <asp:CheckBox ID="M_001604063" runat="server"                         
                            
                            Text="&lt;b&gt; Dia 14/08 (17:15 a 17:30 - COZINHA 2)  &lt;font color='navy'&gt;- Marcelo Piucco&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="M_001604063_CheckedChanged" />
                        <br />
                        <br />
                        <asp:CheckBox ID="M_001604064" runat="server" 
                                                    
                            Text="&lt;b&gt; Dia 14/08 (17:30 a 17:45 - COZINHA 1)  &lt;font color='navy'&gt;- Junior Durski&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="M_001604064_CheckedChanged" />
                        <br />
                        <br />
                        <asp:CheckBox ID="M_001604065" runat="server" 
                            
                            Text="&lt;b&gt; Dia 14/08 (18:15 a 18:30 - CONFEITARIA/PADARIA)  &lt;font color='navy'&gt;- Rossário Tessier&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="M_001604065_CheckedChanged"  />
                        <br />
                        <br />
                        <asp:CheckBox ID="M_001604066" runat="server" 
                            
                            Text="&lt;b&gt; Dia 14/08 (18:30 a 18:45 - COZINHA 2)  &lt;font color='navy'&gt;- Willian Chen Yen&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="M_001604066_CheckedChanged"  />
                        <br />
                        <br />
                        <asp:CheckBox ID="M_001604067" runat="server" 
                            
                            Text="&lt;b&gt; Dia 14/08 (18:45 a 19:00 - COZINHA 1)  &lt;font color='navy'&gt;- Ian Baiocchi&lt;/font&gt;&lt;/b&gt;" 
                            AutoPostBack="True" oncheckedchanged="M_001604067_CheckedChanged"  />
                        <br />
                        <br />
                    </div>   
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="Visitas" runat="server" HeaderText="Visitas Técnicas" Visible="false">
                <ContentTemplate>
                    <br />
                    <br />
                    
                    <div id="d14_V" runat="server" class="Abrsel_dia14">
                        <br />
                            <center><asp:Label ID="Label5" runat="server" Text="Atividades do dia 14/08" CssClass="titulo"></asp:Label></center>
                        <br />
                        <br />
                        <asp:CheckBox ID="V_001604068" runat="server" 
                            
                            Text="&lt;b&gt; Dia 14/08 (07:30 a 12:30) #&lt;br/&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;VISITA TÉCNICA 1: &lt;font color='navy'&gt; Fazenda Malunga&lt;/font&gt;&lt;/b&gt;&lt;font color='navy'&gt;&lt;br/&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;- saída do IESB Sul às 7:30 &#8211; retorno saindo da Malunga as 11:30&lt;/font&gt;" 
                            AutoPostBack="True" oncheckedchanged="V_001604068_CheckedChanged" />
                        <br />
                        <br />
                        <asp:CheckBox ID="V_001604069" runat="server"                         
                            
                            Text="&lt;b&gt; Dia 14/08 (08:00 a 10:00) #&lt;br/&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;VISITA TÉCNICA 2: &lt;font color='navy'&gt; Restaurante Oliver&lt;/font&gt;&lt;/b&gt;&lt;font color='navy'&gt;&lt;br/&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;- saída do Iesb Sul às 8 hs &#8211; retorno as 10 hs&lt;/font&gt;" 
                            AutoPostBack="True" oncheckedchanged="V_001604069_CheckedChanged" />
                        <br />
                        <br />
                        <asp:CheckBox ID="V_001604070" runat="server" 
                            
                            Text="&lt;b&gt; Dia 14/08 (08:00 a 10:00) #&lt;br/&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;VISITA TÉCNICA 3: &lt;font color='navy'&gt; Restaurante Giraffas&lt;/font&gt;&lt;/b&gt;&lt;font color='navy'&gt;&lt;br/&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;- saída do Iesb Sul às 8 hs &#8211; retorno as 10 hs&lt;/font&gt;" 
                            AutoPostBack="True" oncheckedchanged="V_001604070_CheckedChanged" />
                        <br />
                        <br />
                        <asp:CheckBox ID="V_001604071" runat="server" 
                            
                            Text="&lt;b&gt; Dia 14/08 (08:00 a 10:00) #&lt;br/&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;VISITA TÉCNICA 4: &lt;font color='navy'&gt; Restaurante Rubayat&lt;/font&gt;&lt;/b&gt;&lt;font color='navy'&gt;&lt;br/&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;- saída do Iesb Sul às 8 hs &#8211; retorno as 10 hs&lt;/font&gt;" 
                            AutoPostBack="True" oncheckedchanged="V_001604071_CheckedChanged"  />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
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
                PostBackUrl="~/frmAtividadesParticipante.aspx" />
        </div>
        
    </div>
    </div>    
</div>
</asp:Content>

