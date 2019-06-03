using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using Stimulsoft.Controls;
using Stimulsoft.Base.Drawing;
using Stimulsoft.Report;
using Stimulsoft.Report.Dialogs;
using Stimulsoft.Report.Components;
using cllEventos;
using CLLFuncoes;

namespace Reports
{

    public class BoletoPrt : Stimulsoft.Report.StiReport
    {

        cllEventos.MeuBoletoCad oBoletoCad = new MeuBoletoCad();
        CLLFuncoes.ClsFuncoes oclsfuncoes = new ClsFuncoes();

        public BoletoPrt()
        {
            this.InitializeComponent();
        }
        #region StiReport Designer generated code - do not modify
        public Stimulsoft.Report.Dictionary.StiDataRelation Parentboleto;
        public Stimulsoft.Report.Dictionary.StiDataRelation ParentEvento;
        public string wrkAceite;
        public string wrkDsEmpreendimento;
        public string wrkDsMsgCooperativa;
        public string wrkDtBaseCalc;
        public string wrkCnn;
        public Stimulsoft.Report.Components.StiPage Página1;
        public Stimulsoft.Report.Components.StiPageHeaderBand CabeçalhoPágina1;
        public Stimulsoft.Report.Components.StiImage Imagem3;
        public Stimulsoft.Report.Components.StiRichText TextoRico3;
        public Stimulsoft.Report.Components.StiHorizontalLinePrimitive LinhaHorizontal7;
        public Stimulsoft.Report.Components.StiPageFooterBand RodapéPágina1;
        public Stimulsoft.Report.Components.StiText Texto72;
        public Stimulsoft.Report.Components.StiText Texto61;
        public Stimulsoft.Report.Components.StiText Texto60;
        public Stimulsoft.Report.Components.StiText Texto59;
        public Stimulsoft.Report.Components.StiText Texto4;
        public Stimulsoft.Report.Components.StiText Texto6;
        public Stimulsoft.Report.Components.StiText Texto9;
        public Stimulsoft.Report.Components.StiText Texto12;
        public Stimulsoft.Report.Components.StiText Texto7;
        public Stimulsoft.Report.Components.StiText Texto11;
        public Stimulsoft.Report.Components.StiText Texto10;
        public Stimulsoft.Report.Components.StiText Texto5;
        public Stimulsoft.Report.Components.StiText Texto8;
        public Stimulsoft.Report.Components.StiText Texto3;
        public Stimulsoft.Report.BarCodes.StiBarCode CódigoBarras1;
        public Stimulsoft.Report.Components.StiText Texto17;
        public Stimulsoft.Report.Components.StiText Texto46;
        public Stimulsoft.Report.Components.StiText Texto18;
        public Stimulsoft.Report.Components.StiText Texto51;
        public Stimulsoft.Report.Components.StiText Texto19;
        public Stimulsoft.Report.Components.StiText Texto20;
        public Stimulsoft.Report.Components.StiText Texto21;
        public Stimulsoft.Report.Components.StiImage Imagem1;
        public Stimulsoft.Report.Components.StiText Texto2;
        public Stimulsoft.Report.Components.StiText Texto26;
        public Stimulsoft.Report.Components.StiText Texto13;
        public Stimulsoft.Report.Components.StiText Texto27;
        public Stimulsoft.Report.Components.StiText Texto22;
        public Stimulsoft.Report.Components.StiText Texto29;
        public Stimulsoft.Report.Components.StiText Texto14;
        public Stimulsoft.Report.Components.StiText Texto28;
        public Stimulsoft.Report.Components.StiText Texto23;
        public Stimulsoft.Report.Components.StiText Texto30;
        public Stimulsoft.Report.Components.StiText Texto15;
        public Stimulsoft.Report.Components.StiText Texto31;
        public Stimulsoft.Report.Components.StiText Texto32;
        public Stimulsoft.Report.Components.StiText Texto33;
        public Stimulsoft.Report.Components.StiText Texto34;
        public Stimulsoft.Report.Components.StiText Texto35;
        public Stimulsoft.Report.Components.StiText Texto24;
        public Stimulsoft.Report.Components.StiText Texto36;
        public Stimulsoft.Report.Components.StiText Texto37;
        public Stimulsoft.Report.Components.StiText Texto38;
        public Stimulsoft.Report.Components.StiText Texto39;
        public Stimulsoft.Report.Components.StiText Texto40;
        public Stimulsoft.Report.Components.StiText Texto16;
        public Stimulsoft.Report.Components.StiText Texto41;
        public Stimulsoft.Report.Components.StiText Texto42;
        public Stimulsoft.Report.Components.StiText Texto43;
        public Stimulsoft.Report.Components.StiText Texto44;
        public Stimulsoft.Report.Components.StiText Texto45;
        public Stimulsoft.Report.Components.StiText Texto25;
        public Stimulsoft.Report.Components.StiText Texto49;
        public Stimulsoft.Report.Components.StiText Texto50;
        public Stimulsoft.Report.Components.StiText Texto47;
        public Stimulsoft.Report.Components.StiText Texto70;
        public Stimulsoft.Report.Components.StiText Texto69;
        public Stimulsoft.Report.Components.StiText Texto74;
        public Stimulsoft.Report.Components.StiText Texto68;
        public Stimulsoft.Report.Components.StiText Texto66;
        public Stimulsoft.Report.Components.StiText Texto82;
        public Stimulsoft.Report.Components.StiText Texto83;
        public Stimulsoft.Report.Components.StiText Texto73;
        public Stimulsoft.Report.Components.StiText Texto75;
        public Stimulsoft.Report.Components.StiText Texto76;
        public Stimulsoft.Report.Components.StiText Texto79;
        public Stimulsoft.Report.Components.StiText Texto80;
        public Stimulsoft.Report.Components.StiText Texto81;
        public Stimulsoft.Report.Components.StiText Texto84;
        public Stimulsoft.Report.Components.StiText Texto85;
        public Stimulsoft.Report.Components.StiText Texto86;
        public Stimulsoft.Report.Components.StiText Texto87;
        public Stimulsoft.Report.Components.StiText Texto92;
        public Stimulsoft.Report.Components.StiText Texto101;
        public Stimulsoft.Report.Components.StiText Texto102;
        public Stimulsoft.Report.Components.StiText Texto58;
        public Stimulsoft.Report.Components.StiText Texto67;
        public Stimulsoft.Report.Components.StiText Texto89;
        public Stimulsoft.Report.Components.StiImage Imagem2;
        public Stimulsoft.Report.Components.StiText Texto71;
        public Stimulsoft.Report.Components.StiText Texto120;
        public Stimulsoft.Report.Components.StiText Texto121;
        public Stimulsoft.Report.Components.StiText Texto122;
        public Stimulsoft.Report.Components.StiText Texto123;
        public Stimulsoft.Report.Components.StiText Texto124;
        public Stimulsoft.Report.Components.StiText Texto125;
        public Stimulsoft.Report.Components.StiHorizontalLinePrimitive LinhaHorizontal2;
        public Stimulsoft.Report.Components.StiHorizontalLinePrimitive LinhaHorizontal1;
        public Stimulsoft.Report.Components.StiHorizontalLinePrimitive LinhaHorizontal4;
        public Stimulsoft.Report.Components.StiHorizontalLinePrimitive LinhaHorizontal3;
        public Stimulsoft.Report.Components.StiHorizontalLinePrimitive LinhaHorizontal5;
        public Stimulsoft.Report.Components.StiHorizontalLinePrimitive LinhaHorizontal6;
        public Stimulsoft.Report.Components.StiContainer Recipiente1;
        public Stimulsoft.Report.Components.StiDataBand Dados1;
        public Stimulsoft.Report.Components.StiText Texto1;
        public Stimulsoft.Report.Components.StiText Texto77;
        public Stimulsoft.Report.Components.StiText Texto93;
        public Stimulsoft.Report.Components.StiText Texto48;
        public Stimulsoft.Report.Components.StiText Texto52;
        public Stimulsoft.Report.Components.StiText Texto54;
        public Stimulsoft.Report.Components.StiText Texto57;
        public Stimulsoft.Report.Components.StiText Texto55;
        public Stimulsoft.Report.Components.StiText Texto53;
        public Stimulsoft.Report.Components.StiText Texto56;
        public Stimulsoft.Report.Components.StiText Texto62;
        public Stimulsoft.Report.Components.StiText Texto63;
        public Stimulsoft.Report.Components.StiText Texto64;
        public Stimulsoft.Report.Components.StiText Texto94;
        public Stimulsoft.Report.Components.StiText Texto115;
        public Stimulsoft.Report.Components.StiText Texto116;
        public Stimulsoft.Report.Components.StiHorizontalLinePrimitive LinhaHorizontal9;
        public Stimulsoft.Report.Components.StiDataBand Dados2;
        public Stimulsoft.Report.Components.StiText Texto65;
        public Stimulsoft.Report.Components.StiText Texto99;
        public Stimulsoft.Report.Components.StiText Texto118;
        public Stimulsoft.Report.Components.StiText Texto119;
        public Stimulsoft.Report.Components.StiContainer Recipiente2;
        public Stimulsoft.Report.Components.StiRichText TextoRico1;
        public Stimulsoft.Report.Components.StiRichText TextoRico2;
        public Stimulsoft.Report.Components.StiStartPointPrimitive StartPointPrimitive7;
        public Stimulsoft.Report.Components.StiEndPointPrimitive EndPointPrimitive2;
        public Stimulsoft.Report.Components.StiEndPointPrimitive EndPointPrimitive3;
        public Stimulsoft.Report.Components.StiStartPointPrimitive StartPointPrimitive8;
        public Stimulsoft.Report.Components.StiWatermark Página1_Watermark;
        public Stimulsoft.Report.Print.StiPrinterSettings Relatório_PrinterSettings;
        public EventoDataSource Evento;
        public EventosImgsDataSource EventosImgs;
        public DataHoraDataSource DataHora;
        public AtividadeBoletoDataSource AtividadeBoleto;
        public boletoDataSource boleto;

        public override void SaveState(System.String stateName)
        {
            base.SaveState(stateName);
            this.States.Push(stateName, this, "wrkAceite", this.wrkAceite);
            this.States.Push(stateName, this, "wrkDsEmpreendimento", this.wrkDsEmpreendimento);
            this.States.Push(stateName, this, "wrkDsMsgCooperativa", this.wrkDsMsgCooperativa);
            this.States.Push(stateName, this, "wrkDtBaseCalc", this.wrkDtBaseCalc);
            this.States.Push(stateName, this, "wrkCnn", this.wrkCnn);
        }

        public override void RestoreState(System.String stateName)
        {
            base.RestoreState(stateName);
            this.wrkAceite = ((string)(this.States.Pop(stateName, this, "wrkAceite")));
            this.wrkDsEmpreendimento = ((string)(this.States.Pop(stateName, this, "wrkDsEmpreendimento")));
            this.wrkDsMsgCooperativa = ((string)(this.States.Pop(stateName, this, "wrkDsMsgCooperativa")));
            this.wrkDtBaseCalc = ((string)(this.States.Pop(stateName, this, "wrkDtBaseCalc")));
            this.wrkCnn = ((string)(this.States.Pop(stateName, this, "wrkCnn")));
        }

        public void TextoRico3__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(@"__LP__\rtf1\ansi\ansicpg1252\deff0\deflang1046__LP__\fonttbl__LP__\f0\fnil\fcharset0 Microsoft Sans Serif;__RP____RP__
\viewkind4\uc1\pard\qc\b\f0\fs24 Instituto Brasiliense de Direito P\'fablico - IDP\b0\fs17\par
\fs14 SGAS Quadra 607 . M\'f3dulo 49 . Via L2 Sul . Bras\'edlia-DF . CEP 70200-670 . (61) 3535.6565\fs17\par
__RP__
 ");
        }

        public void Texto72__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "Cedente";
        }

        public void Texto61__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "(=)Valor do Documento";
        }

        public void Texto60__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "Nosso Número";
        }

        public void Texto59__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "Agência/Cód Cedente";
        }

        public void Texto4__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "Sacado:";
        }

        public void Texto6__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "CPF/CNPJ:";
        }

        public void Texto9__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, boleto.noParticipante, true);
        }

        public void Texto12__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, oclsfuncoes.CPFCNPJMascarar(boleto.nuCPFCNPJ), true);
        }

        public void Texto7__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "CEP:";
        }

        public void Texto11__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, IIF(boleto.nuCEP.Trim() != "",
boleto.nuCEP.Substring(0, 2) +
"." +
boleto.nuCEP.Substring(2, 3) +
"-" +
boleto.nuCEP.Substring(5, 3), ""), true);
        }

        public void Texto10__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, boleto.dsEndereco.Trim() + "-" +
boleto.noBairro.Trim(), true) + "\r\n" + ToString(sender, boleto.noCidade.Trim(), true);
        }

        public void Texto5__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "Sacador/Avalista:";
        }

        public void Texto8__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "CPF/CNPJ:";
        }

        public void Texto3__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "Autenticação Mecânica - Ficha de Compensação";
        }

        public void CódigoBarras1__GetBarCode(object sender, Stimulsoft.Report.Events.StiValueEventArgs e)
        {
            e.Value = oBoletoCad.FncCodBar(
   boleto.cdEvento,
   boleto.cdCarteira,
   boleto.cdBoleto.Substring(6, 6),
   Format("{0:dd/MM/yyyy}", boleto.dtVencimento),
   Format("{0:N2}", boleto.vlBoleto),
wrkCnn);
        }

        public void Texto17__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "(-)Descontos";
        }

        public void Texto46__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "Instruções";
        }

        public void Texto18__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "(-)Outras Deduções/Abatimentos";
        }

        public void Texto51__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "SENHOR(A) CAIXA, FAVOR NÃO RECEBER ESTE DOCUMENTO APÓS A DATA DE VENCIMENTO.";
        }

        public void Texto19__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "(+)Mora/Multa/Juros";
        }

        public void Texto20__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "(+)Outros Acréscimos";
        }

        public void Texto21__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "(=)Valor Cobrado";
        }

        public void Texto2__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, oBoletoCad.FncLinhaDigitavel(
   boleto.cdEvento,
   boleto.cdCarteira,
   boleto.cdBoleto.Substring(6, 6),
   Format("{0:dd/MM/yyyy}", boleto.dtVencimento),
   Format("{0:N2}", boleto.vlBoleto),
wrkCnn), true);
        }

        public void Texto26__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, boleto.cdBanco + "-" +
oclsfuncoes.DVGerarMod11(boleto.cdBanco), true);
        }

        public void Texto13__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "Vencimento";
        }

        public void Texto27__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "Local de Pagamento";
        }

        public void Texto22__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, Format("{0:dd/MM/yyyy}", boleto.dtVencimento), true);
        }

        public void Texto29__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "QUALQUER BANCO ATÉ O VENCIMENTO";
        }

        public void Texto14__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "Agência/Cód Cedente";
        }

        public void Texto28__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "Cedente";
        }

        public void Texto23__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, IIF(boleto.cdBanco != "001",
 boleto.cdAgencia + "/" + boleto.cdCedente + "-" + oclsfuncoes.DVGerarMod11(boleto.cdCedente),
 boleto.cdAgencia + "-" + oBoletoCad.DVGerarMod11_BB(boleto.cdAgencia) + "/" + boleto.cdCedente + "-" + oBoletoCad.DVGerarMod11_BB(boleto.cdCedente)), true);
        }

        public void Texto30__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, boleto.noCedente, true);
        }

        public void Texto15__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "Nosso Número";
        }

        public void Texto31__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "Data do Documento";
        }

        public void Texto32__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "Nr. do Documento";
        }

        public void Texto33__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "Espécie Docto.";
        }

        public void Texto34__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "Aceite";
        }

        public void Texto35__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "Data do Processamento";
        }

        public void Texto24__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, IIF(boleto.cdBanco == "104",
     "24/90000000" + boleto.cdCarteira + boleto.cdBoleto.Substring(6, 6) + "-" +
      oclsfuncoes.DVGerarMod11("2490000000" + boleto.cdCarteira + boleto.cdBoleto.Substring(6, 6)),
    IIF(boleto.cdBanco == "756",
        boleto.cdBoleto.Substring(6, 6) + "-" + oclsfuncoes.DVGerarMod11Peso3791(boleto.cdAgencia + boleto.cdCedente.PadLeft(9, '0') + oclsfuncoes.DVGerarMod11(boleto.cdCedente.PadLeft(9, '0')) + boleto.cdBoleto.Substring(6, 6)),
IIF(boleto.cdBanco == "001",
boleto.cdConvenio + boleto.cdBoleto.Substring(7, 5) + "-" + oBoletoCad.DVGerarMod11_BB(boleto.cdConvenio + boleto.cdBoleto.Substring(7, 5)),
boleto.cdBoleto.Substring(6, 6)))), true);
        }

        public void Texto36__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, Format("{0:dd/MM/yyyy}", DataHora.DataHora), true);
        }

        public void Texto37__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, Format("{0:dd/MM/yyyy}", DataHora.DataHora), true);
        }

        public void Texto38__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "N";
        }

        public void Texto39__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "DM";
        }

        public void Texto40__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "000000000000";
        }

        public void Texto16__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "(=)Valor do Documento";
        }

        public void Texto41__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "Uso do Banco";
        }

        public void Texto42__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "Carteira";
        }

        public void Texto43__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "Moeda";
        }

        public void Texto44__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "Quantidade";
        }

        public void Texto45__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "Valor";
        }

        public void Texto25__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, Format("{0:N2}", boleto.vlBoleto), true);
        }

        public void Texto49__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "R$";
        }

        public void Texto50__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, boleto.cdCarteira, true);
        }

        public void Texto47__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "Autenticação Mecânica - Recibo do Sacado";
        }

        public void Texto70__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, Format("{0:N2}", boleto.vlBoleto), true);
        }

        public void Texto69__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, IIF(boleto.cdBanco == "104",
     "24/90000000" + boleto.cdCarteira + boleto.cdBoleto.Substring(6, 6) + "-" +
      oclsfuncoes.DVGerarMod11("2490000000" + boleto.cdCarteira + boleto.cdBoleto.Substring(6, 6)),
    IIF(boleto.cdBanco == "756",
        boleto.cdBoleto.Substring(6, 6) + "-" + oclsfuncoes.DVGerarMod11Peso3791(boleto.cdAgencia + boleto.cdCedente.PadLeft(9, '0') + oclsfuncoes.DVGerarMod11(boleto.cdCedente.PadLeft(9, '0')) + boleto.cdBoleto.Substring(6, 6)),
IIF(boleto.cdBanco == "001",
boleto.cdConvenio + boleto.cdBoleto.Substring(7, 5) + "-" + oBoletoCad.DVGerarMod11_BB(boleto.cdConvenio + boleto.cdBoleto.Substring(7, 5)),
boleto.cdBoleto.Substring(6, 6)))), true);
        }

        public void Texto74__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, boleto.noCedente, true);
        }

        public void Texto68__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, IIF(boleto.cdBanco != "001",
 boleto.cdAgencia + "/" + boleto.cdCedente + "-" + oclsfuncoes.DVGerarMod11(boleto.cdCedente),
 boleto.cdAgencia + "-" + oBoletoCad.DVGerarMod11_BB(boleto.cdAgencia) + "/" + boleto.cdCedente + "-" + oBoletoCad.DVGerarMod11_BB(boleto.cdCedente)), true);
        }

        public void Texto66__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "Moeda";
        }

        public void Texto82__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "Nr. do Documento";
        }

        public void Texto83__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "000000000000";
        }

        public void Texto73__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "Aceite";
        }

        public void Texto75__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "N";
        }

        public void Texto76__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "R$";
        }

        public void Texto79__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "(-)Descontos";
        }

        public void Texto80__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "(-)Outras Deduções/Abatimentos";
        }

        public void Texto81__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "(+)Mora/Multa/Juros";
        }

        public void Texto84__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "(=)Valor Cobrado";
        }

        public void Texto85__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "(+)Outros Acréscimos";
        }

        public void Texto86__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, Format("{0:N2}", 0), true);
        }

        public void Texto87__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "0,00";
        }

        public void Texto92__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, Format("{0:N2}", 0), true);
        }

        public void Texto101__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, Format("{0:N2}", 0), true);
        }

        public void Texto102__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, Format("{0:N2}", boleto.vlBoleto), true);
        }

        public void Texto58__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "Vencimento";
        }

        public void Texto67__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, Format("{0:dd/MM/yyyy}", boleto.dtVencimento), true);
        }

        public void Texto89__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, boleto.cdBanco + "-" +
oclsfuncoes.DVGerarMod11(boleto.cdBanco), true);
        }

        public void Texto71__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, oBoletoCad.FncLinhaDigitavel(
   boleto.cdEvento,
   boleto.cdCarteira,
   boleto.cdBoleto.Substring(6, 6),
   Format("{0:dd/MM/yyyy}", boleto.dtVencimento),
   Format("{0:N2}", boleto.vlBoleto),
wrkCnn), true);
        }

        public void Texto120__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, Format("{0:N2}", boleto.vlBoleto), true);
        }

        public void Texto121__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, Format("{0:N2}", 0), true);
        }

        public void Texto122__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "0,00";
        }

        public void Texto123__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, Format("{0:N2}", 0), true);
        }

        public void Texto124__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, Format("{0:N2}", 0), true);
        }

        public void Texto1__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "Detalhamento da Cobrança";
        }

        public void Texto77__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = " Atividade";
        }

        public void Texto93__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "Total";
        }

        public void Texto48__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "CPF/CNPJ:";
        }

        public void Texto52__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, boleto.noParticipante, true);
        }

        public void Texto54__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, oclsfuncoes.CPFCNPJMascarar(boleto.nuCPFCNPJ), true);
        }

        public void Texto57__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, boleto.dsEndereco.Trim() + "-" +
boleto.noBairro.Trim(), true) + "\r\n" + ToString(sender, boleto.noCidade.Trim() + "-" + boleto.dsUF.Trim() + " - CEP: " +
IIF(boleto.nuCEP.Trim() != "",
boleto.nuCEP.Substring(0, 2) +
"." +
boleto.nuCEP.Substring(2, 3) +
"-" +
boleto.nuCEP.Substring(5, 3), ""), true);
        }

        public void Texto55__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "Inscrição nº: ";
        }

        public void Texto53__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "Categoria: ";
        }

        public void Texto62__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, boleto.cdParticipante, true);
        }

        public void Texto63__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, boleto.noCategoria, true);
        }

        public void Texto94__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "Sacado:";
        }

        public void Texto115__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = " Valor";
        }

        public void Texto116__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = " Desc";
        }

        private void Texto65_Conditions(object sender, System.EventArgs e)
        {
            if ((Line & 1) == 0)
            {
                ((Stimulsoft.Report.Components.IStiTextBrush)(sender)).TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
                ((Stimulsoft.Report.Components.IStiBrush)(sender)).Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Gainsboro);
                ((Stimulsoft.Report.Components.IStiFont)(sender)).Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
                ((Stimulsoft.Report.Components.StiComponent)(sender)).Enabled = true;
            }
        }

        public void Texto65__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, AtividadeBoleto.noTitulo, true);
        }

        private void Texto99_Conditions(object sender, System.EventArgs e)
        {
            if ((Line & 1) == 0)
            {
                ((Stimulsoft.Report.Components.IStiTextBrush)(sender)).TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
                ((Stimulsoft.Report.Components.IStiBrush)(sender)).Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Gainsboro);
                ((Stimulsoft.Report.Components.IStiFont)(sender)).Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
                ((Stimulsoft.Report.Components.StiComponent)(sender)).Enabled = true;
            }
        }

        public void Texto99__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, Format("{0:N2}", AtividadeBoleto.vlUnitario - AtividadeBoleto.vlUnitarioDesconto), true);
        }

        private void Texto118_Conditions(object sender, System.EventArgs e)
        {
            if ((Line & 1) == 0)
            {
                ((Stimulsoft.Report.Components.IStiTextBrush)(sender)).TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
                ((Stimulsoft.Report.Components.IStiBrush)(sender)).Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Gainsboro);
                ((Stimulsoft.Report.Components.IStiFont)(sender)).Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
                ((Stimulsoft.Report.Components.StiComponent)(sender)).Enabled = true;
            }
        }

        public void Texto118__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, Format("{0:N2}", AtividadeBoleto.vlUnitario), true);
        }

        private void Texto119_Conditions(object sender, System.EventArgs e)
        {
            if ((Line & 1) == 0)
            {
                ((Stimulsoft.Report.Components.IStiTextBrush)(sender)).TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
                ((Stimulsoft.Report.Components.IStiBrush)(sender)).Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Gainsboro);
                ((Stimulsoft.Report.Components.IStiFont)(sender)).Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
                ((Stimulsoft.Report.Components.StiComponent)(sender)).Enabled = true;
            }
        }

        public void Texto119__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, Format("{0:N2}", AtividadeBoleto.vlUnitarioDesconto), true);
        }

        public void TextoRico1__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(@"__LP__\rtf1\ansi\ansicpg1252\deff0\deflang1046__LP__\fonttbl__LP__\f0\fnil\fcharset0 Arial;__RP____LP__\f1\fnil\fcharset0 Microsoft Sans Serif;__RP____LP__\f2\fnil\fcharset0 Arial Narrow;__RP____RP__
__LP__\colortbl ;\red0\green0\blue255;__RP__
\viewkind4\uc1\pard\fs20\par
-O pagamento desta cobran\'e7a poder\'e1 ser efetuada na rede banc\'e1ria at\'e9 o vencimento.\par
  \par
-Para emiss\'e3o de segunda via do boleto acesse:\f1\fs17\par
\pard\qc\cf1\b\up6\f2\fs16    \ul\b0\up0\fs56 congressoidp\b\fs16 .com.br\cf0\ulnone\b0\f1\fs17  \par
__RP__
 ");
        }

        public void TextoRico2__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ConvertRtf("{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1046{\\fonttbl{\\f0\\fnil\\fcharset0 Arial;}}\r\n\\" +
"uc1\\pard\\fs20\\{}\r\n", ToString(wrkDsMsgCooperativa));
        }

        private void InitializeComponent()
        {
            this.boleto = new boletoDataSource();
            this.AtividadeBoleto = new AtividadeBoletoDataSource();
            this.DataHora = new DataHoraDataSource();
            this.EventosImgs = new EventosImgsDataSource();
            this.Evento = new EventoDataSource();
            this.Parentboleto = new Stimulsoft.Report.Dictionary.StiDataRelation("boleto_AtividadeBoleto", "boleto", "boleto", this.boleto, this.AtividadeBoleto, new System.String[] {
                        "cdPedido",
                        "cdEvento"}, new System.String[] {
                        "cdPedido",
                        "cdEvento"});
            this.ParentEvento = new Stimulsoft.Report.Dictionary.StiDataRelation("Relationship67", "Evento", "Evento", this.Evento, this.EventosImgs, new System.String[] {
                        "cdEvento"}, new System.String[] {
                        "cdEvento"});
            this.Dictionary.Variables.Add(new Stimulsoft.Report.Dictionary.StiVariable("", "wrkAceite", "wrkAceite", typeof(string), "", false, false));
            this.Dictionary.Variables.Add(new Stimulsoft.Report.Dictionary.StiVariable("", "wrkDsEmpreendimento", "wrkDsEmpreendimento", typeof(string), "", false, false));
            this.Dictionary.Variables.Add(new Stimulsoft.Report.Dictionary.StiVariable("", "wrkDsMsgCooperativa", "wrkDsMsgCooperativa", typeof(string), "", false, false));
            this.Dictionary.Variables.Add(new Stimulsoft.Report.Dictionary.StiVariable("", "wrkDtBaseCalc", "wrkDtBaseCalc", typeof(string), "", false, false));
            this.Dictionary.Variables.Add(new Stimulsoft.Report.Dictionary.StiVariable("", "wrkCnn", "wrkCnn", typeof(string), "Data Source=.\\\\sqlexpress;Initial Catalog=dbEventos_FM;Persist Security Info=True" +
";User ID=sa;Password=RaC982973", false, false));
            this.NeedsCompiling = false;
            // Variables init
            // Variables init
            this.wrkAceite = "";
            this.wrkDsEmpreendimento = "";
            this.wrkDsMsgCooperativa = "";
            this.wrkDtBaseCalc = "";
            this.wrkCnn = "Data Source=.\\\\sqlexpress;Initial Catalog=dbEventos_FM;Persist Security Info=True" +
";User ID=sa;Password=RaC982973";
            this.ReferencedAssemblies = new System.String[] {
                    "System.Dll",
                    "System.Drawing.Dll",
                    "System.Windows.Forms.Dll",
                    "System.Data.Dll",
                    "System.Xml.Dll",
                    "Stimulsoft.Controls.Dll",
                    "Stimulsoft.Base.Dll",
                    "Stimulsoft.Report.Dll",
                    "cllEventos.dll",
                    "CLLFuncoes.dll"};
            this.ReportAlias = "Relatório";
            // 
            // ReportChanged
            // 
            this.ReportChanged = new DateTime(2010, 8, 31, 9, 16, 7, 0);
            // 
            // ReportCreated
            // 
            this.ReportCreated = new DateTime(2008, 12, 17, 9, 35, 11, 0);
            this.ReportGuid = "c65aac97db154f43b06af7fe94561bd8";
            this.ReportName = "Relatório";
            this.ReportUnit = Stimulsoft.Report.StiReportUnitType.Centimeters;
            this.ScriptLanguage = Stimulsoft.Report.StiReportLanguageType.CSharp;
            // 
            // Página1
            // 
            this.Página1 = new Stimulsoft.Report.Components.StiPage();
            this.Página1.Guid = "fd3195960d594e1f950704ab0b01c796";
            this.Página1.Name = "Página1";
            this.Página1.PageHeight = 27.7;
            this.Página1.PageWidth = 19.5;
            this.Página1.PrintHeadersFootersFromPreviousPage = true;
            this.Página1.PrintOnPreviousPage = true;
            this.Página1.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 2, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Página1.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            // 
            // CabeçalhoPágina1
            // 
            this.CabeçalhoPágina1 = new Stimulsoft.Report.Components.StiPageHeaderBand();
            this.CabeçalhoPágina1.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 0.4, 19.5, 1);
            this.CabeçalhoPágina1.MaxHeight = 0;
            this.CabeçalhoPágina1.MinHeight = 0;
            this.CabeçalhoPágina1.Name = "CabeçalhoPágina1";
            this.CabeçalhoPágina1.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.CabeçalhoPágina1.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            // 
            // Imagem3
            // 
            this.Imagem3 = new Stimulsoft.Report.Components.StiImage();
            this.Imagem3.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 0, 3.8, 0.9);
            this.Imagem3.DataColumn = "EventosImgs.imgLogo";
            this.Imagem3.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Imagem3.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Imagem3.Name = "Imagem3";
            this.Imagem3.Stretch = true;
            this.Imagem3.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Imagem3.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Imagem3.Guid = null;
            this.Imagem3.Image = null;
            // 
            // TextoRico3
            // 
            this.TextoRico3 = new Stimulsoft.Report.Components.StiRichText();
            this.TextoRico3.BackColor = System.Drawing.Color.White;
            this.TextoRico3.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(4, 0, 15.4, 1);
            this.TextoRico3.DataColumn = "";
            this.TextoRico3.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.TextoRico3.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.TextoRico3.Name = "TextoRico3";
            this.TextoRico3.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.TextoRico3__GetValue);
            this.TextoRico3.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.TextoRico3.Guid = null;
            // 
            // LinhaHorizontal7
            // 
            this.LinhaHorizontal7 = new Stimulsoft.Report.Components.StiHorizontalLinePrimitive();
            this.LinhaHorizontal7.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 1, 19.5, 0.0254);
            this.LinhaHorizontal7.Color = System.Drawing.Color.Black;
            this.LinhaHorizontal7.Guid = "a7ee0e036ea043bea26cecf2dd968828";
            this.LinhaHorizontal7.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.LinhaHorizontal7.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.LinhaHorizontal7.Name = "LinhaHorizontal7";
            this.CabeçalhoPágina1.Guid = null;
            // 
            // RodapéPágina1
            // 
            this.RodapéPágina1 = new Stimulsoft.Report.Components.StiPageFooterBand();
            this.RodapéPágina1.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 13.5, 19.5, 14.2);
            this.RodapéPágina1.MaxHeight = 0;
            this.RodapéPágina1.MinHeight = 0;
            this.RodapéPágina1.Name = "RodapéPágina1";
            this.RodapéPágina1.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.RodapéPágina1.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            // 
            // Texto72
            // 
            this.Texto72 = new Stimulsoft.Report.Components.StiText();
            this.Texto72.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 1.1, 14.2, 0.6);
            this.Texto72.Guid = "48a8ee028d464ca59a8691dcf62a6a3e";
            this.Texto72.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto72.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto72.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto72.Name = "Texto72";
            this.Texto72.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto72__GetValue);
            this.Texto72.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto72.Border = new Stimulsoft.Base.Drawing.StiBorder((((Stimulsoft.Base.Drawing.StiBorderSides.None | Stimulsoft.Base.Drawing.StiBorderSides.Top)
                            | Stimulsoft.Base.Drawing.StiBorderSides.Right)
                            | Stimulsoft.Base.Drawing.StiBorderSides.Bottom), System.Drawing.Color.Black, 0.5, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto72.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto72.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto72.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto72.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto72.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto72.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto61
            // 
            this.Texto61 = new Stimulsoft.Report.Components.StiText();
            this.Texto61.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(14.2, 1.7, 5.3, 0.6);
            this.Texto61.Guid = "e8a8e8e004ed4c6cb22a92ce0debd462";
            this.Texto61.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto61.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto61.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto61.Name = "Texto61";
            this.Texto61.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto61__GetValue);
            this.Texto61.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto61.Border = new Stimulsoft.Base.Drawing.StiBorder((((Stimulsoft.Base.Drawing.StiBorderSides.None | Stimulsoft.Base.Drawing.StiBorderSides.Top)
                            | Stimulsoft.Base.Drawing.StiBorderSides.Left)
                            | Stimulsoft.Base.Drawing.StiBorderSides.Bottom), System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto61.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto61.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto61.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto61.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto61.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto61.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto60
            // 
            this.Texto60 = new Stimulsoft.Report.Components.StiText();
            this.Texto60.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(9.9, 1.7, 4.3, 0.6);
            this.Texto60.Guid = "d951f1a7580b4dea858db7fd2fa1db6d";
            this.Texto60.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto60.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto60.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto60.Name = "Texto60";
            this.Texto60.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto60__GetValue);
            this.Texto60.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto60.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto60.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto60.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto60.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto60.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto60.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto60.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto59
            // 
            this.Texto59 = new Stimulsoft.Report.Components.StiText();
            this.Texto59.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(5.5, 1.7, 4.4, 0.6);
            this.Texto59.Guid = "988c5068647546baa8dd0dd9ca2db026";
            this.Texto59.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto59.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto59.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto59.Name = "Texto59";
            this.Texto59.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto59__GetValue);
            this.Texto59.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto59.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto59.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto59.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto59.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto59.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto59.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto59.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto4
            // 
            this.Texto4 = new Stimulsoft.Report.Components.StiText();
            this.Texto4.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0.1, 10.9, 1.2, 0.4);
            this.Texto4.Guid = "713a72a082144fd6a0d3b9ea09d01fce";
            this.Texto4.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto4.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto4.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto4.Name = "Texto4";
            this.Texto4.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto4__GetValue);
            this.Texto4.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto4.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto4.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto4.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto4.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto4.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto4.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto4.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto6
            // 
            this.Texto6 = new Stimulsoft.Report.Components.StiText();
            this.Texto6.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(14.4, 10.9, 1.6, 0.4);
            this.Texto6.Guid = "d014035d65e94f09a8c29bae05d631f0";
            this.Texto6.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto6.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto6.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto6.Name = "Texto6";
            this.Texto6.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto6__GetValue);
            this.Texto6.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto6.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto6.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto6.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto6.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto6.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto6.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto6.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto9
            // 
            this.Texto9 = new Stimulsoft.Report.Components.StiText();
            this.Texto9.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(2, 10.9, 12, 0.4);
            this.Texto9.Guid = "67d30991af6443cb8075e00c7a60ed93";
            this.Texto9.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto9.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto9.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto9.Name = "Texto9";
            this.Texto9.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto9__GetValue);
            this.Texto9.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto9.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto9.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto9.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            this.Texto9.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto9.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto9.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto9.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto12
            // 
            this.Texto12 = new Stimulsoft.Report.Components.StiText();
            this.Texto12.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(16, 10.9, 3.4, 0.4);
            this.Texto12.Guid = "7e90e26e96c849b1a9cb6efdcb69bc8a";
            this.Texto12.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto12.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto12.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto12.Name = "Texto12";
            this.Texto12.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto12__GetValue);
            this.Texto12.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto12.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto12.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto12.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            this.Texto12.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto12.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto12.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto12.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto7
            // 
            this.Texto7 = new Stimulsoft.Report.Components.StiText();
            this.Texto7.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(14.4, 11.3, 1.2, 0.4);
            this.Texto7.Guid = "64b455b55a8140e2be3ed4017dc29a47";
            this.Texto7.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto7.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto7.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto7.Name = "Texto7";
            this.Texto7.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto7__GetValue);
            this.Texto7.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto7.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto7.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto7.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto7.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto7.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto7.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto7.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto11
            // 
            this.Texto11 = new Stimulsoft.Report.Components.StiText();
            this.Texto11.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(16, 11.3, 2.6, 0.4);
            this.Texto11.Guid = "2d5a8dd9daba4c7bbfd65cc166cfe99c";
            this.Texto11.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto11.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto11.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto11.Name = "Texto11";
            this.Texto11.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto11__GetValue);
            this.Texto11.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto11.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto11.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto11.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            this.Texto11.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto11.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto11.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto11.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto10
            // 
            this.Texto10 = new Stimulsoft.Report.Components.StiText();
            this.Texto10.CanBreak = true;
            this.Texto10.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(2, 11.3, 12, 0.8);
            this.Texto10.Guid = "c5d74ca2a8524000a4855058d62df165";
            this.Texto10.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto10.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto10.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto10.Name = "Texto10";
            this.Texto10.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto10__GetValue);
            this.Texto10.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto10.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto10.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto10.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            this.Texto10.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto10.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto10.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto10.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, true, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto5
            // 
            this.Texto5 = new Stimulsoft.Report.Components.StiText();
            this.Texto5.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0.1, 12.1, 6.4, 0.4);
            this.Texto5.Guid = "ed5eb3a6ee3a4d5eba3833f1953cc40b";
            this.Texto5.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto5.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto5.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto5.Name = "Texto5";
            this.Texto5.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto5__GetValue);
            this.Texto5.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto5.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto5.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto5.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto5.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto5.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto5.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto5.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto8
            // 
            this.Texto8 = new Stimulsoft.Report.Components.StiText();
            this.Texto8.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(14.4, 12.1, 1.6, 0.4);
            this.Texto8.Guid = "a86f9e36f91c421ba03ba875acbe8147";
            this.Texto8.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto8.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto8.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto8.Name = "Texto8";
            this.Texto8.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto8__GetValue);
            this.Texto8.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto8.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto8.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto8.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto8.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto8.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto8.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto8.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto3
            // 
            this.Texto3 = new Stimulsoft.Report.Components.StiText();
            this.Texto3.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(14, 12.7, 5.4, 0.6);
            this.Texto3.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto3.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto3.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto3.Name = "Texto3";
            this.Texto3.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto3__GetValue);
            this.Texto3.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto3.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto3.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto3.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto3.Guid = null;
            this.Texto3.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto3.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto3.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto3.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // CódigoBarras1
            // 
            this.CódigoBarras1 = new Stimulsoft.Report.BarCodes.StiBarCode();
            this.CódigoBarras1.AutoScale = true;
            this.CódigoBarras1.BackColor = System.Drawing.Color.White;
            this.CódigoBarras1.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 12.7, 10.3, 1.5);
            this.CódigoBarras1.GetBarCode += new Stimulsoft.Report.Events.StiValueEventHandler(this.CódigoBarras1__GetBarCode);
            this.CódigoBarras1.ForeColor = System.Drawing.Color.Black;
            this.CódigoBarras1.GrowToHeight = true;
            this.CódigoBarras1.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.CódigoBarras1.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.CódigoBarras1.Name = "CódigoBarras1";
            this.CódigoBarras1.ShowLabelText = false;
            this.CódigoBarras1.BarCodeType = new Stimulsoft.Report.BarCodes.StiInterleaved2of5BarCodeType(13F, 1F, 2.2F);
            this.CódigoBarras1.Guid = null;
            // 
            // Texto17
            // 
            this.Texto17 = new Stimulsoft.Report.Components.StiText();
            this.Texto17.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(14.2, 7.8, 5.3, 0.6);
            this.Texto17.Guid = "cc1ebfcacfbd445bb85fdd31e8d065c3";
            this.Texto17.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto17.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto17.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto17.Name = "Texto17";
            this.Texto17.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto17__GetValue);
            this.Texto17.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto17.Border = new Stimulsoft.Base.Drawing.StiBorder(((Stimulsoft.Base.Drawing.StiBorderSides.None | Stimulsoft.Base.Drawing.StiBorderSides.Top)
                            | Stimulsoft.Base.Drawing.StiBorderSides.Left), System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto17.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto17.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto17.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto17.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto17.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto17.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto46
            // 
            this.Texto46 = new Stimulsoft.Report.Components.StiText();
            this.Texto46.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 7.8, 3.8, 0.6);
            this.Texto46.Guid = "40bba881610f47ca85d82fb0aeb59275";
            this.Texto46.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto46.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto46.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto46.Name = "Texto46";
            this.Texto46.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto46__GetValue);
            this.Texto46.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto46.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto46.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto46.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto46.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto46.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto46.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto46.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto18
            // 
            this.Texto18 = new Stimulsoft.Report.Components.StiText();
            this.Texto18.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(14.2, 8.4, 5.3, 0.6);
            this.Texto18.Guid = "2f20297ad05045b79c53aa62bd5aa335";
            this.Texto18.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto18.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto18.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto18.Name = "Texto18";
            this.Texto18.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto18__GetValue);
            this.Texto18.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto18.Border = new Stimulsoft.Base.Drawing.StiBorder(((Stimulsoft.Base.Drawing.StiBorderSides.None | Stimulsoft.Base.Drawing.StiBorderSides.Top)
                            | Stimulsoft.Base.Drawing.StiBorderSides.Left), System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto18.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto18.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto18.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto18.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto18.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto18.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto51
            // 
            this.Texto51 = new Stimulsoft.Report.Components.StiText();
            this.Texto51.CanBreak = true;
            this.Texto51.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0.2, 8.5, 13.8, 0.5);
            this.Texto51.GrowToHeight = true;
            this.Texto51.Guid = "5a030b88b595477e941f0b3a7ee7eef6";
            this.Texto51.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto51.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto51.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto51.Name = "Texto51";
            this.Texto51.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto51__GetValue);
            this.Texto51.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto51.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto51.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto51.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.Texto51.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto51.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto51.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto51.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, true, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto19
            // 
            this.Texto19 = new Stimulsoft.Report.Components.StiText();
            this.Texto19.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(14.2, 9, 5.3, 0.6);
            this.Texto19.Guid = "44173a7050854836a500d7fe95291d9e";
            this.Texto19.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto19.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto19.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto19.Name = "Texto19";
            this.Texto19.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto19__GetValue);
            this.Texto19.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto19.Border = new Stimulsoft.Base.Drawing.StiBorder(((Stimulsoft.Base.Drawing.StiBorderSides.None | Stimulsoft.Base.Drawing.StiBorderSides.Top)
                            | Stimulsoft.Base.Drawing.StiBorderSides.Left), System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto19.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto19.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto19.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto19.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto19.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto19.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto20
            // 
            this.Texto20 = new Stimulsoft.Report.Components.StiText();
            this.Texto20.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(14.2, 9.6, 5.3, 0.6);
            this.Texto20.Guid = "384c0321b3094eb68a27ca55412c37d9";
            this.Texto20.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto20.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto20.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto20.Name = "Texto20";
            this.Texto20.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto20__GetValue);
            this.Texto20.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto20.Border = new Stimulsoft.Base.Drawing.StiBorder((((Stimulsoft.Base.Drawing.StiBorderSides.None | Stimulsoft.Base.Drawing.StiBorderSides.Top)
                            | Stimulsoft.Base.Drawing.StiBorderSides.Left)
                            | Stimulsoft.Base.Drawing.StiBorderSides.Bottom), System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto20.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto20.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto20.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto20.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto20.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto20.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto21
            // 
            this.Texto21 = new Stimulsoft.Report.Components.StiText();
            this.Texto21.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(14.2, 10.2, 5.3, 0.6);
            this.Texto21.Guid = "7918b0df8ea84c24ad603ca045e22765";
            this.Texto21.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto21.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto21.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto21.Name = "Texto21";
            this.Texto21.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto21__GetValue);
            this.Texto21.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto21.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.Left, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto21.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto21.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto21.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto21.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto21.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto21.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Imagem1
            // 
            this.Imagem1 = new Stimulsoft.Report.Components.StiImage();
            this.Imagem1.AspectRatio = true;
            this.Imagem1.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0.1, 4.5, 3.4, 0.9);
            this.Imagem1.HorAlignment = Stimulsoft.Base.Drawing.StiHorAlignment.Center;
            this.Imagem1.Image = StiImageConverter.StringToImage("Qk12LwAAAAAAADYAAAAoAAAAPwAAAD8AAAABABgAAAAAAAAAAADEDgAAxA4AAAAAAAAAAAAAIMziBsHWA" +
"LrPALvOALvOALTGAK28AKqyBbi5Abq4ALm5Aba/BrHHA67OAKzWAK7UALTIALbDALXEALLHBbDKBq/LB" +
"a7OA67OALLRALfVALrTALrRALvOALzLALXDAK62BcDEALq7Ari+DMDLEsTVDL/UA7fQALPQALDOALDOA" +
"LDOALHNALLKALLIALLHALLHALLHALLHALLHALLHALLHALPHALTHALTHALfKALPGALLGB7XLE7XNF6zGF" +
"aC7AAAAIdDkD8vdAMPUALzPALzTBMHaEcDaD73TBb3NAL7NAb7NBbzQCrnTBbjXALraALvYAL7RAL/PA" +
"L/PAMDPAMDPAL/QAL/QAL3RALfMALbOALPLAK/JALPMALrUALvSALnNAMHSALrKALTEALbGALnLALfKA" +
"LPGALDDAb3QAbzRAL3RAL3RAL7RAL/RAL/RAL/RAL7QAL7QAL3QAL3QAL3QAL7QAL7QAL/QAMDRAL7QA" +
"cDTC8LWD7fOBqS8BJWvAAAAIdPmGtboBNXlAM/gCdLnHdrzJtr3HtTyD9TwCtXwDNXwD9TwF9PvE9TvB" +
"dfvBdjtD9bsD9fqCNnpAdvoAN3mAN7lAN3lA9zlEt3qGN3tGdjsEdDpC9DsCtf2Bdz7BN78ANXyAtjzA" +
"9vyANbrANPkA9XjDtnmFtzoF9jlGNjlFdjmD9joCNnpANrqANvsANrsA9nqCtnpCtnpCtnpDNjpCtnpB" +
"9npB9npBdrpB9joEdvsGdjrEcLXAKC3AIylAAAAG87jGtnqE+XzE+v4Hu39I+r/IeH+Etn5EOj/B+n/C" +
"en/Dun/Gen/Gez/DvD9Eu/9IOv/Iuv/F+3+DvD8BPT5APb2Avb1B/X1DfDzGvP8IfP/H+z/F+f/E+j/D" +
"Ob/B+X/AN//Bun/CPD/A/D/BvL/EPj/GPr/HfX6IvD1J+72J+73Iu36F+79DO//AvD/AvD/C/D+EO/9E" +
"u/9Eu/9FO/9Eu/9EO/9DvD9D+78Eez6Gu/+I+z9FM3hAKC2AImhAAAAHtLrFdToCuLvFfT8Ivn/I/P/L" +
"fP/L/v/Du3/A+7/Ae3/Be7/Du//D/L/Cvb9D/T9H+//Ie3/GPH/D/T/Bff8APn6APv4APr4APDvBu/xC" +
"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" +
"pOcAAAAANDtANXxAN75Auj/BvD/CPT/BPL/AfL/AO/+APD8APD8APD+AO//AO//AO7/Au7/Cez9Dez7D" +
"ev9C+v9Cez/A+7+APH9APL8APT6DPf6Ivr5NfbzROniStPLS7qySqqkQqCfS7u7St/cPvPvIfbzDfP0D" +
"PT6Dfn/BvH7BPD8AfL7AfP5BPP4C/L2GvD1FvD2C/X/BfP+DPL5GPf5H/z4K/XuPeLfQdLQEJybKMHAS" +
"Pn2Q/b1OtbbDJqhCY2XAAAAANDrANfvAOH3Aev/BfH/BfT/BPP/APL9APX/APX/APX/APT/APT/APP/A" +
"PP/APL/Au//BO//B+//CvD/CvH/B/P/APX/APj/APT8APb6CPn6GP/9Kv/8Nvb1P+foO9reSe/2RPX8O" +
"Pv/KPn7E/L0B/HxBfX2CPv9Afj8APj9APf/APf/APX/BfT/D/L/E/L/Efb6D/f4EPb3Dvb3CPn4Efv7K" +
"fv/Ovv/LeTuKuDsNfb/M+34NNTmBZisAI2fAAAAANHnANjsBeP1Dez7DvD9DvD8DfD5C+73DvT7DvT7D" +
"vP8DvP8DvP+DvP+DvL/DfH/B/H/B/D/CvD/DvD/D/D/D/P/CfX/Bvf/Avb8APT2APPzA/TzEfX0H/b3M" +
"PT4L/P3Jvb8Iff8Ifb7HvX4GPH0EvHzCvTyBvf2BPj4BPf5BvX8B/P/B/P/B/H/C/D/EPH/FPHvHPXuH" +
"vXyF/b0CPP1BPP2D/P6HfP+L/n/IuXzJ+7+Lef5MtLqAJWvAI2nAAAACM3dEdbmHeLwJuz4K+/5K/D4K" +
"/H3KvD2J+7xJ+7xKe3xKe3zKez0Kez0Kev1I+v3G+39Fe7+Ge3+Guz8G+z8Gu37GO75FO/4F/X6F/b4F" +
"fX0FfLwGO/sIfHtLfbzLvv4Ee7sEu/tI+/wLvLyM/X1L/f2H/bzFvXzEPLxE/D0HO31IOv4Huv6Gev8D" +
"uz+Eu74IvLtMfTqOvPvNvXyIfX1F/PzGe7yHevxL/T8I+buMPL8Nuv7N9HqAJGvAIekAAAAJczZMNbjP" +
"+bxS/D5T/X8Uvb7VPf6Vfj7VPf5VPf5Vff5Vfb6VfX7VfX7VfX9VPX9Tvj8Svn8Svj+R/f+Rvb/Rvb/R" +
"vb/RfX/QvH7R/f+Tvz/Tfz/S/n5R/j1Sfv2Sf75P/77Pvv6Rvj5Tvn7U/r9Uvz+Svr7Rfb5Svb8T/T9V" +
"vP9WPL/U/L/RvT/OPb/Nvn9Q/z4UPr0YfT2YvT5Uvn8Svz9Svz9S/v7Sff3RfHxVP7/VPP9VdruGpq3A" +
"ImpAAAAUtXfXuHrbu/4evn/fvz/fvv/f/r8gPv9h///h///if//if//if//if//if7/jP//mP/7mP/4k" +
"f/7iv/9hf//gv7/hPz/ifr/iPH/kPP/lff/lfv/j/z/h/7/f//9fP79jf//iP//eP3/cf3/bv3/dfv/g" +
"Pj/jPP/off/qff/qff/ofn/lfz/if//fP//c///aP//dfv/kPX/m/P/kPb/i/v/iv//if//gP/1ePvwh" +
"P//g/n6jez7Yb/WNqvIAAAAcuPtfO33jfr/lP//lv//kv3/k/r9k/r8l/38l/38mPz+mPz+mPv/mPv/m" +
"Pr/ofv7sv/xtf/rrP/wov7zmfz6lvj+mvb/ofP/svj/tPL/su7/rvD/q/f/o/7/l///l//+pvn/ofj/j" +
"Pv/gP3/e/7/hPz/m/T/r/D/xO3/ze3/x+//vfL/rff9nvv6lP74h//7d/z/hfv/rfn/ufX/rfT/pPT7o" +
"fn5nv73mv/0kP3tlv/3lvr1tv//meb5b9LuAAAA");
            this.Imagem1.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Imagem1.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Imagem1.Name = "Imagem1";
            this.Imagem1.Stretch = true;
            this.Imagem1.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Imagem1.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Imagem1.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Imagem1.Guid = null;
            // 
            // Texto2
            // 
            this.Texto2 = new Stimulsoft.Report.Components.StiText();
            this.Texto2.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(5.2, 4.6, 14.3, 0.8);
            this.Texto2.Guid = "ba65ec4ef3dc46e38aa1f06df1099b13";
            this.Texto2.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
            this.Texto2.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto2.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto2.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto2.Name = "Texto2";
            this.Texto2.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto2__GetValue);
            this.Texto2.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto2.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Texto2.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto2.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto2.Font = new System.Drawing.Font("Arial Narrow", 16F, System.Drawing.FontStyle.Bold);
            this.Texto2.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto2.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto2.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto2.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto26
            // 
            this.Texto26 = new Stimulsoft.Report.Components.StiText();
            this.Texto26.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(3.6, 4.6, 1.6, 0.8);
            this.Texto26.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
            this.Texto26.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto26.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto26.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto26.Name = "Texto26";
            this.Texto26.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto26__GetValue);
            this.Texto26.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto26.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Texto26.Border = new Stimulsoft.Base.Drawing.StiBorder(((Stimulsoft.Base.Drawing.StiBorderSides.None | Stimulsoft.Base.Drawing.StiBorderSides.Left)
                            | Stimulsoft.Base.Drawing.StiBorderSides.Right), System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto26.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto26.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.Texto26.Guid = null;
            this.Texto26.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto26.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto26.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto26.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto13
            // 
            this.Texto13 = new Stimulsoft.Report.Components.StiText();
            this.Texto13.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(14.2, 5.4, 5.3, 0.6);
            this.Texto13.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto13.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto13.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto13.Name = "Texto13";
            this.Texto13.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto13__GetValue);
            this.Texto13.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto13.Border = new Stimulsoft.Base.Drawing.StiBorder(((Stimulsoft.Base.Drawing.StiBorderSides.None | Stimulsoft.Base.Drawing.StiBorderSides.Left)
                            | Stimulsoft.Base.Drawing.StiBorderSides.Bottom), System.Drawing.Color.Black, 0.5, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto13.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto13.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto13.Guid = null;
            this.Texto13.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto13.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto13.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto13.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto27
            // 
            this.Texto27 = new Stimulsoft.Report.Components.StiText();
            this.Texto27.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 5.4, 14.2, 0.6);
            this.Texto27.Guid = "d6399a21283e452181f69adf192d2541";
            this.Texto27.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto27.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto27.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto27.Name = "Texto27";
            this.Texto27.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto27__GetValue);
            this.Texto27.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto27.Border = new Stimulsoft.Base.Drawing.StiBorder(((Stimulsoft.Base.Drawing.StiBorderSides.None | Stimulsoft.Base.Drawing.StiBorderSides.Right)
                            | Stimulsoft.Base.Drawing.StiBorderSides.Bottom), System.Drawing.Color.Black, 0.5, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto27.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto27.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto27.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto27.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto27.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto27.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto22
            // 
            this.Texto22 = new Stimulsoft.Report.Components.StiText();
            this.Texto22.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(15.5, 5.6, 3.7, 0.4);
            this.Texto22.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Right;
            this.Texto22.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto22.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto22.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto22.Name = "Texto22";
            this.Texto22.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto22__GetValue);
            this.Texto22.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto22.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto22.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto22.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.Texto22.Guid = null;
            this.Texto22.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto22.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto22.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto22.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto29
            // 
            this.Texto29 = new Stimulsoft.Report.Components.StiText();
            this.Texto29.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0.2, 5.62, 9, 0.4);
            this.Texto29.Guid = "3cfe972360564104b335d5948e485095";
            this.Texto29.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto29.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto29.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto29.Name = "Texto29";
            this.Texto29.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto29__GetValue);
            this.Texto29.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto29.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto29.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto29.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.Texto29.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto29.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto29.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto29.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto14
            // 
            this.Texto14 = new Stimulsoft.Report.Components.StiText();
            this.Texto14.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(14.2, 6, 5.3, 0.6);
            this.Texto14.Guid = "b706e33cca6f4e079000ac91ccdf0e10";
            this.Texto14.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto14.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto14.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto14.Name = "Texto14";
            this.Texto14.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto14__GetValue);
            this.Texto14.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto14.Border = new Stimulsoft.Base.Drawing.StiBorder(((Stimulsoft.Base.Drawing.StiBorderSides.None | Stimulsoft.Base.Drawing.StiBorderSides.Top)
                            | Stimulsoft.Base.Drawing.StiBorderSides.Left), System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto14.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto14.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto14.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto14.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto14.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto14.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto28
            // 
            this.Texto28 = new Stimulsoft.Report.Components.StiText();
            this.Texto28.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 6, 14.2, 0.6);
            this.Texto28.Guid = "ad0a90444cfb4f0c9be36201df25251d";
            this.Texto28.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto28.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto28.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto28.Name = "Texto28";
            this.Texto28.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto28__GetValue);
            this.Texto28.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto28.Border = new Stimulsoft.Base.Drawing.StiBorder((((Stimulsoft.Base.Drawing.StiBorderSides.None | Stimulsoft.Base.Drawing.StiBorderSides.Top)
                            | Stimulsoft.Base.Drawing.StiBorderSides.Right)
                            | Stimulsoft.Base.Drawing.StiBorderSides.Bottom), System.Drawing.Color.Black, 0.5, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto28.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto28.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto28.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto28.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto28.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto28.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto23
            // 
            this.Texto23 = new Stimulsoft.Report.Components.StiText();
            this.Texto23.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(14.6, 6.2, 4.6, 0.4);
            this.Texto23.Guid = "7bba6ebf8d8a4d6193a3763db2c84754";
            this.Texto23.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Right;
            this.Texto23.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto23.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto23.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto23.Name = "Texto23";
            this.Texto23.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto23__GetValue);
            this.Texto23.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto23.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto23.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto23.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.Texto23.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto23.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto23.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto23.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto30
            // 
            this.Texto30 = new Stimulsoft.Report.Components.StiText();
            this.Texto30.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0.2, 6.22, 13.8, 0.4);
            this.Texto30.Guid = "269ffd9edd2645b6875bc680c37bc6ed";
            this.Texto30.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto30.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto30.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto30.Name = "Texto30";
            this.Texto30.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto30__GetValue);
            this.Texto30.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto30.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto30.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto30.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.Texto30.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto30.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto30.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto30.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto15
            // 
            this.Texto15 = new Stimulsoft.Report.Components.StiText();
            this.Texto15.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(14.2, 6.6, 5.3, 0.6);
            this.Texto15.Guid = "4b38c17e32784f0e87cdfe305d6a4fc0";
            this.Texto15.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto15.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto15.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto15.Name = "Texto15";
            this.Texto15.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto15__GetValue);
            this.Texto15.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto15.Border = new Stimulsoft.Base.Drawing.StiBorder(((Stimulsoft.Base.Drawing.StiBorderSides.None | Stimulsoft.Base.Drawing.StiBorderSides.Top)
                            | Stimulsoft.Base.Drawing.StiBorderSides.Left), System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto15.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto15.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto15.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto15.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto15.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto15.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto31
            // 
            this.Texto31 = new Stimulsoft.Report.Components.StiText();
            this.Texto31.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 6.6, 3.8, 0.6);
            this.Texto31.Guid = "e7512912208d475eafaba680213479ad";
            this.Texto31.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto31.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto31.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto31.Name = "Texto31";
            this.Texto31.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto31__GetValue);
            this.Texto31.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto31.Border = new Stimulsoft.Base.Drawing.StiBorder((((Stimulsoft.Base.Drawing.StiBorderSides.None | Stimulsoft.Base.Drawing.StiBorderSides.Top)
                            | Stimulsoft.Base.Drawing.StiBorderSides.Right)
                            | Stimulsoft.Base.Drawing.StiBorderSides.Bottom), System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto31.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto31.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto31.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto31.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto31.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto31.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto32
            // 
            this.Texto32 = new Stimulsoft.Report.Components.StiText();
            this.Texto32.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(3.8, 6.6, 3.3, 0.6);
            this.Texto32.Guid = "cbfd442bd82a45fb9a7a403f73bba637";
            this.Texto32.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto32.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto32.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto32.Name = "Texto32";
            this.Texto32.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto32__GetValue);
            this.Texto32.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto32.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto32.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto32.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto32.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto32.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto32.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto32.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto33
            // 
            this.Texto33 = new Stimulsoft.Report.Components.StiText();
            this.Texto33.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(7.1, 6.6, 2.5, 0.6);
            this.Texto33.Guid = "ef9a56e383f7414080f25d790b47ba75";
            this.Texto33.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto33.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto33.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto33.Name = "Texto33";
            this.Texto33.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto33__GetValue);
            this.Texto33.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto33.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto33.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto33.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto33.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto33.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto33.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto33.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto34
            // 
            this.Texto34 = new Stimulsoft.Report.Components.StiText();
            this.Texto34.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(9.6, 6.6, 0.9, 0.6);
            this.Texto34.Guid = "3df9bce2bea342569333169410a303da";
            this.Texto34.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto34.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto34.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto34.Name = "Texto34";
            this.Texto34.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto34__GetValue);
            this.Texto34.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto34.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto34.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto34.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto34.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto34.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto34.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto34.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto35
            // 
            this.Texto35 = new Stimulsoft.Report.Components.StiText();
            this.Texto35.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(10.5, 6.6, 3.7, 0.6);
            this.Texto35.Guid = "2c8f635ca1164f1a9f3c6ef3685f15f6";
            this.Texto35.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto35.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto35.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto35.Name = "Texto35";
            this.Texto35.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto35__GetValue);
            this.Texto35.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto35.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto35.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto35.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto35.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto35.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto35.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto35.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto24
            // 
            this.Texto24 = new Stimulsoft.Report.Components.StiText();
            this.Texto24.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(14.6, 6.82, 4.6, 0.4);
            this.Texto24.Guid = "1b83d86f98c048edad98ba9377073ccf";
            this.Texto24.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Right;
            this.Texto24.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto24.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto24.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto24.Name = "Texto24";
            this.Texto24.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto24__GetValue);
            this.Texto24.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto24.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto24.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto24.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.Texto24.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto24.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto24.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto24.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto36
            // 
            this.Texto36 = new Stimulsoft.Report.Components.StiText();
            this.Texto36.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0.2, 6.82, 2.5, 0.4);
            this.Texto36.Guid = "f6fa5f8f537e4dd996eb77ed5ff0e7db";
            this.Texto36.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto36.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto36.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto36.Name = "Texto36";
            this.Texto36.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto36__GetValue);
            this.Texto36.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto36.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto36.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto36.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.Texto36.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto36.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto36.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto36.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto37
            // 
            this.Texto37 = new Stimulsoft.Report.Components.StiText();
            this.Texto37.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(10.7, 6.82, 2.5, 0.4);
            this.Texto37.Guid = "572537a6b28f4172bf2f58bd6ff3082f";
            this.Texto37.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto37.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto37.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto37.Name = "Texto37";
            this.Texto37.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto37__GetValue);
            this.Texto37.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto37.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto37.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto37.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.Texto37.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto37.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto37.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto37.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto38
            // 
            this.Texto38 = new Stimulsoft.Report.Components.StiText();
            this.Texto38.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(9.7, 6.82, 0.7, 0.4);
            this.Texto38.Guid = "782512c369934b608bd26db1dd587664";
            this.Texto38.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
            this.Texto38.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto38.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto38.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto38.Name = "Texto38";
            this.Texto38.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto38__GetValue);
            this.Texto38.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto38.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto38.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto38.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.Texto38.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto38.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto38.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto38.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto39
            // 
            this.Texto39 = new Stimulsoft.Report.Components.StiText();
            this.Texto39.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(7.5, 6.82, 0.7, 0.4);
            this.Texto39.Guid = "2dbee67c0052480bbe06560c790148b9";
            this.Texto39.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto39.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto39.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto39.Name = "Texto39";
            this.Texto39.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto39__GetValue);
            this.Texto39.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto39.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto39.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto39.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.Texto39.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto39.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto39.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto39.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto40
            // 
            this.Texto40 = new Stimulsoft.Report.Components.StiText();
            this.Texto40.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(4, 6.82, 2.5, 0.4);
            this.Texto40.Guid = "c8549c034d604feeba23cf6cadb5e6fd";
            this.Texto40.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto40.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto40.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto40.Name = "Texto40";
            this.Texto40.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto40__GetValue);
            this.Texto40.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto40.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto40.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto40.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.Texto40.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto40.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto40.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto40.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto16
            // 
            this.Texto16 = new Stimulsoft.Report.Components.StiText();
            this.Texto16.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(14.2, 7.2, 5.3, 0.6);
            this.Texto16.Guid = "f4ee196b569c40aa9154f6221b0d48cc";
            this.Texto16.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto16.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto16.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto16.Name = "Texto16";
            this.Texto16.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto16__GetValue);
            this.Texto16.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto16.Border = new Stimulsoft.Base.Drawing.StiBorder((((Stimulsoft.Base.Drawing.StiBorderSides.None | Stimulsoft.Base.Drawing.StiBorderSides.Top)
                            | Stimulsoft.Base.Drawing.StiBorderSides.Left)
                            | Stimulsoft.Base.Drawing.StiBorderSides.Bottom), System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto16.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto16.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto16.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto16.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto16.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto16.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto41
            // 
            this.Texto41 = new Stimulsoft.Report.Components.StiText();
            this.Texto41.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 7.2, 3.8, 0.6);
            this.Texto41.Guid = "ea17e2cf17c6438e8645e349df6e2afb";
            this.Texto41.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto41.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto41.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto41.Name = "Texto41";
            this.Texto41.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto41__GetValue);
            this.Texto41.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto41.Border = new Stimulsoft.Base.Drawing.StiBorder((((Stimulsoft.Base.Drawing.StiBorderSides.None | Stimulsoft.Base.Drawing.StiBorderSides.Top)
                            | Stimulsoft.Base.Drawing.StiBorderSides.Right)
                            | Stimulsoft.Base.Drawing.StiBorderSides.Bottom), System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto41.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto41.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto41.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto41.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.InitializeComponent2();
        }

        public void InitializeComponent2()
        {
            this.Texto41.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto41.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto42
            // 
            this.Texto42 = new Stimulsoft.Report.Components.StiText();
            this.Texto42.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(3.8, 7.2, 1.7, 0.6);
            this.Texto42.Guid = "d564e1d317414716ad80227bebac8506";
            this.Texto42.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto42.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto42.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto42.Name = "Texto42";
            this.Texto42.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto42__GetValue);
            this.Texto42.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto42.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto42.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto42.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto42.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto42.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto42.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto42.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto43
            // 
            this.Texto43 = new Stimulsoft.Report.Components.StiText();
            this.Texto43.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(5.5, 7.2, 1.6, 0.6);
            this.Texto43.Guid = "a33a4ea1184e44f88ce6b1bdfe380fea";
            this.Texto43.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto43.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto43.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto43.Name = "Texto43";
            this.Texto43.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto43__GetValue);
            this.Texto43.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto43.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto43.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto43.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto43.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto43.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto43.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto43.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto44
            // 
            this.Texto44 = new Stimulsoft.Report.Components.StiText();
            this.Texto44.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(7.1, 7.2, 3.4, 0.6);
            this.Texto44.Guid = "953800a7dc484484b39e54c89e3950ab";
            this.Texto44.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto44.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto44.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto44.Name = "Texto44";
            this.Texto44.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto44__GetValue);
            this.Texto44.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto44.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto44.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto44.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto44.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto44.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto44.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto44.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto45
            // 
            this.Texto45 = new Stimulsoft.Report.Components.StiText();
            this.Texto45.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(10.5, 7.2, 3.7, 0.6);
            this.Texto45.Guid = "f4d693a2fbfa4e799a8a2b8be20825c9";
            this.Texto45.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto45.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto45.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto45.Name = "Texto45";
            this.Texto45.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto45__GetValue);
            this.Texto45.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto45.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto45.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto45.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto45.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto45.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto45.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto45.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto25
            // 
            this.Texto25 = new Stimulsoft.Report.Components.StiText();
            this.Texto25.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(14.6, 7.4, 4.6, 0.4);
            this.Texto25.Guid = "024cfc6309f443f5ae0d9644116d0ed7";
            this.Texto25.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Right;
            this.Texto25.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto25.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto25.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto25.Name = "Texto25";
            this.Texto25.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto25__GetValue);
            this.Texto25.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto25.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto25.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto25.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.Texto25.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto25.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto25.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto25.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto49
            // 
            this.Texto49 = new Stimulsoft.Report.Components.StiText();
            this.Texto49.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(5.8, 7.42, 0.7, 0.4);
            this.Texto49.Guid = "dd1fd8e2900144d79fd71a993d71b78e";
            this.Texto49.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto49.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto49.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto49.Name = "Texto49";
            this.Texto49.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto49__GetValue);
            this.Texto49.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto49.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto49.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto49.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.Texto49.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto49.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto49.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto49.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto50
            // 
            this.Texto50 = new Stimulsoft.Report.Components.StiText();
            this.Texto50.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(4, 7.42, 1.4, 0.4);
            this.Texto50.Guid = "d2adc52a79eb41e9acca1168bb0e8580";
            this.Texto50.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto50.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto50.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto50.Name = "Texto50";
            this.Texto50.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto50__GetValue);
            this.Texto50.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto50.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto50.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto50.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.Texto50.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto50.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto50.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto50.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto47
            // 
            this.Texto47 = new Stimulsoft.Report.Components.StiText();
            this.Texto47.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(14, 3, 5.4, 0.6);
            this.Texto47.Guid = "f1d46b15b1994b50b3d67a457b067917";
            this.Texto47.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Right;
            this.Texto47.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto47.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto47.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto47.Name = "Texto47";
            this.Texto47.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto47__GetValue);
            this.Texto47.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto47.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto47.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto47.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto47.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto47.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto47.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto47.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto70
            // 
            this.Texto70 = new Stimulsoft.Report.Components.StiText();
            this.Texto70.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(14.3, 1.9, 4.9, 0.4);
            this.Texto70.Guid = "d0532811e0dc44ca9da932b220ee05c4";
            this.Texto70.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Right;
            this.Texto70.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto70.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto70.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto70.Name = "Texto70";
            this.Texto70.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto70__GetValue);
            this.Texto70.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto70.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto70.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto70.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.Texto70.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto70.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto70.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto70.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto69
            // 
            this.Texto69 = new Stimulsoft.Report.Components.StiText();
            this.Texto69.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(10.1, 1.92, 3.9, 0.4);
            this.Texto69.Guid = "d9a8c982da99461ba076d38f2ba66f20";
            this.Texto69.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Right;
            this.Texto69.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto69.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto69.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto69.Name = "Texto69";
            this.Texto69.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto69__GetValue);
            this.Texto69.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto69.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto69.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto69.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.Texto69.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto69.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto69.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto69.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto74
            // 
            this.Texto74 = new Stimulsoft.Report.Components.StiText();
            this.Texto74.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0.2, 1.32, 12.3, 0.4);
            this.Texto74.Guid = "fa4486440be94c45a66d878ce92459dd";
            this.Texto74.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto74.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto74.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto74.Name = "Texto74";
            this.Texto74.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto74__GetValue);
            this.Texto74.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto74.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto74.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto74.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.Texto74.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto74.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto74.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto74.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto68
            // 
            this.Texto68 = new Stimulsoft.Report.Components.StiText();
            this.Texto68.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(5.6, 1.9, 4, 0.4);
            this.Texto68.Guid = "4da64ade7eb8487bba8aaa10fc90fe3d";
            this.Texto68.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Right;
            this.Texto68.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto68.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto68.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto68.Name = "Texto68";
            this.Texto68.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto68__GetValue);
            this.Texto68.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto68.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto68.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto68.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.Texto68.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto68.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto68.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto68.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto66
            // 
            this.Texto66 = new Stimulsoft.Report.Components.StiText();
            this.Texto66.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(3, 1.7, 1.6, 0.6);
            this.Texto66.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto66.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto66.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto66.Name = "Texto66";
            this.Texto66.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto66__GetValue);
            this.Texto66.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto66.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto66.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto66.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto66.Guid = null;
            this.Texto66.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto66.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto66.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto66.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto82
            // 
            this.Texto82 = new Stimulsoft.Report.Components.StiText();
            this.Texto82.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 1.7, 3, 0.6);
            this.Texto82.Guid = "fd660baffc104deeb575c5cff02e0a55";
            this.Texto82.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto82.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto82.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto82.Name = "Texto82";
            this.Texto82.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto82__GetValue);
            this.Texto82.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto82.Border = new Stimulsoft.Base.Drawing.StiBorder((((Stimulsoft.Base.Drawing.StiBorderSides.None | Stimulsoft.Base.Drawing.StiBorderSides.Top)
                            | Stimulsoft.Base.Drawing.StiBorderSides.Right)
                            | Stimulsoft.Base.Drawing.StiBorderSides.Bottom), System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto82.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto82.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto82.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto82.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto82.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto82.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto83
            // 
            this.Texto83 = new Stimulsoft.Report.Components.StiText();
            this.Texto83.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0.2, 1.92, 2.5, 0.4);
            this.Texto83.Guid = "084eba140c864080a973b5f64255ed30";
            this.Texto83.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto83.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto83.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto83.Name = "Texto83";
            this.Texto83.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto83__GetValue);
            this.Texto83.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto83.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto83.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto83.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.Texto83.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto83.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto83.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto83.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto73
            // 
            this.Texto73 = new Stimulsoft.Report.Components.StiText();
            this.Texto73.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(4.6, 1.7, 0.9, 0.6);
            this.Texto73.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto73.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto73.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto73.Name = "Texto73";
            this.Texto73.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto73__GetValue);
            this.Texto73.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto73.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.Black, 0.5, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto73.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto73.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto73.Guid = null;
            this.Texto73.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto73.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto73.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto73.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto75
            // 
            this.Texto75 = new Stimulsoft.Report.Components.StiText();
            this.Texto75.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(4.7, 1.92, 0.7, 0.4);
            this.Texto75.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
            this.Texto75.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto75.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto75.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto75.Name = "Texto75";
            this.Texto75.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto75__GetValue);
            this.Texto75.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto75.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto75.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto75.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.Texto75.Guid = null;
            this.Texto75.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto75.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto75.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto75.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto76
            // 
            this.Texto76 = new Stimulsoft.Report.Components.StiText();
            this.Texto76.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(3.4, 1.92, 0.6, 0.4);
            this.Texto76.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto76.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto76.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto76.Name = "Texto76";
            this.Texto76.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto76__GetValue);
            this.Texto76.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto76.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto76.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto76.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.Texto76.Guid = null;
            this.Texto76.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto76.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto76.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto76.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto79
            // 
            this.Texto79 = new Stimulsoft.Report.Components.StiText();
            this.Texto79.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 2.3, 3.6, 0.6);
            this.Texto79.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto79.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto79.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto79.Name = "Texto79";
            this.Texto79.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto79__GetValue);
            this.Texto79.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto79.Border = new Stimulsoft.Base.Drawing.StiBorder((((Stimulsoft.Base.Drawing.StiBorderSides.None | Stimulsoft.Base.Drawing.StiBorderSides.Top)
                            | Stimulsoft.Base.Drawing.StiBorderSides.Right)
                            | Stimulsoft.Base.Drawing.StiBorderSides.Bottom), System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto79.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto79.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto79.Guid = null;
            this.Texto79.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto79.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto79.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto79.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto80
            // 
            this.Texto80 = new Stimulsoft.Report.Components.StiText();
            this.Texto80.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(3.6, 2.3, 3.6, 0.6);
            this.Texto80.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto80.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto80.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto80.Name = "Texto80";
            this.Texto80.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto80__GetValue);
            this.Texto80.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto80.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto80.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto80.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto80.Guid = null;
            this.Texto80.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto80.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto80.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto80.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto81
            // 
            this.Texto81 = new Stimulsoft.Report.Components.StiText();
            this.Texto81.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(7.2, 2.3, 3.6, 0.6);
            this.Texto81.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto81.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto81.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto81.Name = "Texto81";
            this.Texto81.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto81__GetValue);
            this.Texto81.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto81.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto81.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto81.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto81.Guid = null;
            this.Texto81.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto81.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto81.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto81.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto84
            // 
            this.Texto84 = new Stimulsoft.Report.Components.StiText();
            this.Texto84.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(14.2, 2.3, 5.3, 0.6);
            this.Texto84.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto84.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto84.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto84.Name = "Texto84";
            this.Texto84.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto84__GetValue);
            this.Texto84.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto84.Border = new Stimulsoft.Base.Drawing.StiBorder((((Stimulsoft.Base.Drawing.StiBorderSides.None | Stimulsoft.Base.Drawing.StiBorderSides.Top)
                            | Stimulsoft.Base.Drawing.StiBorderSides.Left)
                            | Stimulsoft.Base.Drawing.StiBorderSides.Bottom), System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto84.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto84.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto84.Guid = null;
            this.Texto84.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto84.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto84.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto84.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto85
            // 
            this.Texto85 = new Stimulsoft.Report.Components.StiText();
            this.Texto85.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(10.8, 2.3, 3.4, 0.6);
            this.Texto85.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto85.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto85.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto85.Name = "Texto85";
            this.Texto85.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto85__GetValue);
            this.Texto85.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto85.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto85.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto85.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto85.Guid = null;
            this.Texto85.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto85.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto85.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto85.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto86
            // 
            this.Texto86 = new Stimulsoft.Report.Components.StiText();
            this.Texto86.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0.2, 2.5, 3.1, 0.4);
            this.Texto86.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Right;
            this.Texto86.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto86.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto86.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto86.Name = "Texto86";
            this.Texto86.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto86__GetValue);
            this.Texto86.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto86.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Bottom;
            this.Texto86.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto86.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto86.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.Texto86.Guid = null;
            this.Texto86.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto86.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto86.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto86.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto87
            // 
            this.Texto87 = new Stimulsoft.Report.Components.StiText();
            this.Texto87.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(3.8, 2.5, 3.1, 0.4);
            this.Texto87.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Right;
            this.Texto87.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto87.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto87.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto87.Name = "Texto87";
            this.Texto87.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto87__GetValue);
            this.Texto87.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto87.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Bottom;
            this.Texto87.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto87.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto87.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.Texto87.Guid = null;
            this.Texto87.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto87.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto87.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto87.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto92
            // 
            this.Texto92 = new Stimulsoft.Report.Components.StiText();
            this.Texto92.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(7.4, 2.5, 3.1, 0.4);
            this.Texto92.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Right;
            this.Texto92.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto92.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto92.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto92.Name = "Texto92";
            this.Texto92.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto92__GetValue);
            this.Texto92.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto92.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Bottom;
            this.Texto92.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto92.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto92.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.Texto92.Guid = null;
            this.Texto92.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto92.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto92.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto92.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto101
            // 
            this.Texto101 = new Stimulsoft.Report.Components.StiText();
            this.Texto101.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(11, 2.5, 3.1, 0.4);
            this.Texto101.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Right;
            this.Texto101.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto101.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto101.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto101.Name = "Texto101";
            this.Texto101.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto101__GetValue);
            this.Texto101.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto101.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Bottom;
            this.Texto101.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto101.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto101.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.Texto101.Guid = null;
            this.Texto101.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto101.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto101.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto101.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto102
            // 
            this.Texto102 = new Stimulsoft.Report.Components.StiText();
            this.Texto102.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(14.3, 2.5, 4.9, 0.4);
            this.Texto102.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Right;
            this.Texto102.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto102.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto102.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto102.Name = "Texto102";
            this.Texto102.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto102__GetValue);
            this.Texto102.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto102.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Bottom;
            this.Texto102.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto102.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto102.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.Texto102.Guid = null;
            this.Texto102.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto102.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto102.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto102.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto58
            // 
            this.Texto58 = new Stimulsoft.Report.Components.StiText();
            this.Texto58.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(14.2, 1.1, 5.3, 0.6);
            this.Texto58.Guid = "e6ed5b31ce2a4f5baa03a02118693f4d";
            this.Texto58.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto58.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto58.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto58.Name = "Texto58";
            this.Texto58.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto58__GetValue);
            this.Texto58.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto58.Border = new Stimulsoft.Base.Drawing.StiBorder((((Stimulsoft.Base.Drawing.StiBorderSides.None | Stimulsoft.Base.Drawing.StiBorderSides.Top)
                            | Stimulsoft.Base.Drawing.StiBorderSides.Left)
                            | Stimulsoft.Base.Drawing.StiBorderSides.Bottom), System.Drawing.Color.Black, 0.5, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto58.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto58.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto58.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto58.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto58.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto58.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto67
            // 
            this.Texto67 = new Stimulsoft.Report.Components.StiText();
            this.Texto67.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(15.9, 1.2, 3.3, 0.5);
            this.Texto67.Guid = "020abb5ecdeb4e7d803bf3aa6ff70f2d";
            this.Texto67.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto67.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto67.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto67.Name = "Texto67";
            this.Texto67.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto67__GetValue);
            this.Texto67.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto67.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto67.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto67.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.Texto67.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto67.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto67.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto67.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto89
            // 
            this.Texto89 = new Stimulsoft.Report.Components.StiText();
            this.Texto89.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(3.6, 0.3, 1.6, 0.8);
            this.Texto89.Guid = "ea1634e0fde14fb395ee018ead7e2f8c";
            this.Texto89.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
            this.Texto89.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto89.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto89.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto89.Name = "Texto89";
            this.Texto89.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto89__GetValue);
            this.Texto89.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto89.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Texto89.Border = new Stimulsoft.Base.Drawing.StiBorder(((Stimulsoft.Base.Drawing.StiBorderSides.None | Stimulsoft.Base.Drawing.StiBorderSides.Left)
                            | Stimulsoft.Base.Drawing.StiBorderSides.Right), System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto89.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto89.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.Texto89.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto89.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto89.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto89.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Imagem2
            // 
            this.Imagem2 = new Stimulsoft.Report.Components.StiImage();
            this.Imagem2.AspectRatio = true;
            this.Imagem2.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0.1, 0.2, 3.4, 0.9);
            this.Imagem2.HorAlignment = Stimulsoft.Base.Drawing.StiHorAlignment.Center;
            this.Imagem2.Image = StiImageConverter.StringToImage("Qk12LwAAAAAAADYAAAAoAAAAPwAAAD8AAAABABgAAAAAAAAAAADEDgAAxA4AAAAAAAAAAAAAIMziBsHWA" +
"LrPALvOALvOALTGAK28AKqyBbi5Abq4ALm5Aba/BrHHA67OAKzWAK7UALTIALbDALXEALLHBbDKBq/LB" +
"a7OA67OALLRALfVALrTALrRALvOALzLALXDAK62BcDEALq7Ari+DMDLEsTVDL/UA7fQALPQALDOALDOA" +
"LDOALHNALLKALLIALLHALLHALLHALLHALLHALLHALLHALPHALTHALTHALfKALPGALLGB7XLE7XNF6zGF" +
"aC7AAAAIdDkD8vdAMPUALzPALzTBMHaEcDaD73TBb3NAL7NAb7NBbzQCrnTBbjXALraALvYAL7RAL/PA" +
"L/PAMDPAMDPAL/QAL/QAL3RALfMALbOALPLAK/JALPMALrUALvSALnNAMHSALrKALTEALbGALnLALfKA" +
"LPGALDDAb3QAbzRAL3RAL3RAL7RAL/RAL/RAL/RAL7QAL7QAL3QAL3QAL3QAL7QAL7QAL/QAMDRAL7QA" +
"cDTC8LWD7fOBqS8BJWvAAAAIdPmGtboBNXlAM/gCdLnHdrzJtr3HtTyD9TwCtXwDNXwD9TwF9PvE9TvB" +
"dfvBdjtD9bsD9fqCNnpAdvoAN3mAN7lAN3lA9zlEt3qGN3tGdjsEdDpC9DsCtf2Bdz7BN78ANXyAtjzA" +
"9vyANbrANPkA9XjDtnmFtzoF9jlGNjlFdjmD9joCNnpANrqANvsANrsA9nqCtnpCtnpCtnpDNjpCtnpB" +
"9npB9npBdrpB9joEdvsGdjrEcLXAKC3AIylAAAAG87jGtnqE+XzE+v4Hu39I+r/IeH+Etn5EOj/B+n/C" +
"en/Dun/Gen/Gez/DvD9Eu/9IOv/Iuv/F+3+DvD8BPT5APb2Avb1B/X1DfDzGvP8IfP/H+z/F+f/E+j/D" +
"Ob/B+X/AN//Bun/CPD/A/D/BvL/EPj/GPr/HfX6IvD1J+72J+73Iu36F+79DO//AvD/AvD/C/D+EO/9E" +
"u/9Eu/9FO/9Eu/9EO/9DvD9D+78Eez6Gu/+I+z9FM3hAKC2AImhAAAAHtLrFdToCuLvFfT8Ivn/I/P/L" +
"fP/L/v/Du3/A+7/Ae3/Be7/Du//D/L/Cvb9D/T9H+//Ie3/GPH/D/T/Bff8APn6APv4APr4APDvBu/xC" +
"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" +
"pOcAAAAANDtANXxAN75Auj/BvD/CPT/BPL/AfL/AO/+APD8APD8APD+AO//AO//AO7/Au7/Cez9Dez7D" +
"ev9C+v9Cez/A+7+APH9APL8APT6DPf6Ivr5NfbzROniStPLS7qySqqkQqCfS7u7St/cPvPvIfbzDfP0D" +
"PT6Dfn/BvH7BPD8AfL7AfP5BPP4C/L2GvD1FvD2C/X/BfP+DPL5GPf5H/z4K/XuPeLfQdLQEJybKMHAS" +
"Pn2Q/b1OtbbDJqhCY2XAAAAANDrANfvAOH3Aev/BfH/BfT/BPP/APL9APX/APX/APX/APT/APT/APP/A" +
"PP/APL/Au//BO//B+//CvD/CvH/B/P/APX/APj/APT8APb6CPn6GP/9Kv/8Nvb1P+foO9reSe/2RPX8O" +
"Pv/KPn7E/L0B/HxBfX2CPv9Afj8APj9APf/APf/APX/BfT/D/L/E/L/Efb6D/f4EPb3Dvb3CPn4Efv7K" +
"fv/Ovv/LeTuKuDsNfb/M+34NNTmBZisAI2fAAAAANHnANjsBeP1Dez7DvD9DvD8DfD5C+73DvT7DvT7D" +
"vP8DvP8DvP+DvP+DvL/DfH/B/H/B/D/CvD/DvD/D/D/D/P/CfX/Bvf/Avb8APT2APPzA/TzEfX0H/b3M" +
"PT4L/P3Jvb8Iff8Ifb7HvX4GPH0EvHzCvTyBvf2BPj4BPf5BvX8B/P/B/P/B/H/C/D/EPH/FPHvHPXuH" +
"vXyF/b0CPP1BPP2D/P6HfP+L/n/IuXzJ+7+Lef5MtLqAJWvAI2nAAAACM3dEdbmHeLwJuz4K+/5K/D4K" +
"/H3KvD2J+7xJ+7xKe3xKe3zKez0Kez0Kev1I+v3G+39Fe7+Ge3+Guz8G+z8Gu37GO75FO/4F/X6F/b4F" +
"fX0FfLwGO/sIfHtLfbzLvv4Ee7sEu/tI+/wLvLyM/X1L/f2H/bzFvXzEPLxE/D0HO31IOv4Huv6Gev8D" +
"uz+Eu74IvLtMfTqOvPvNvXyIfX1F/PzGe7yHevxL/T8I+buMPL8Nuv7N9HqAJGvAIekAAAAJczZMNbjP" +
"+bxS/D5T/X8Uvb7VPf6Vfj7VPf5VPf5Vff5Vfb6VfX7VfX7VfX9VPX9Tvj8Svn8Svj+R/f+Rvb/Rvb/R" +
"vb/RfX/QvH7R/f+Tvz/Tfz/S/n5R/j1Sfv2Sf75P/77Pvv6Rvj5Tvn7U/r9Uvz+Svr7Rfb5Svb8T/T9V" +
"vP9WPL/U/L/RvT/OPb/Nvn9Q/z4UPr0YfT2YvT5Uvn8Svz9Svz9S/v7Sff3RfHxVP7/VPP9VdruGpq3A" +
"ImpAAAAUtXfXuHrbu/4evn/fvz/fvv/f/r8gPv9h///h///if//if//if//if//if7/jP//mP/7mP/4k" +
"f/7iv/9hf//gv7/hPz/ifr/iPH/kPP/lff/lfv/j/z/h/7/f//9fP79jf//iP//eP3/cf3/bv3/dfv/g" +
"Pj/jPP/off/qff/qff/ofn/lfz/if//fP//c///aP//dfv/kPX/m/P/kPb/i/v/iv//if//gP/1ePvwh" +
"P//g/n6jez7Yb/WNqvIAAAAcuPtfO33jfr/lP//lv//kv3/k/r9k/r8l/38l/38mPz+mPz+mPv/mPv/m" +
"Pr/ofv7sv/xtf/rrP/wov7zmfz6lvj+mvb/ofP/svj/tPL/su7/rvD/q/f/o/7/l///l//+pvn/ofj/j" +
"Pv/gP3/e/7/hPz/m/T/r/D/xO3/ze3/x+//vfL/rff9nvv6lP74h//7d/z/hfv/rfn/ufX/rfT/pPT7o" +
"fn5nv73mv/0kP3tlv/3lvr1tv//meb5b9LuAAAA");
            this.Imagem2.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Imagem2.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Imagem2.Name = "Imagem2";
            this.Imagem2.Stretch = true;
            this.Imagem2.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Imagem2.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Imagem2.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Imagem2.Guid = null;
            // 
            // Texto71
            // 
            this.Texto71 = new Stimulsoft.Report.Components.StiText();
            this.Texto71.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(5.2, 0.3, 14.3, 0.8);
            this.Texto71.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
            this.Texto71.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto71.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto71.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto71.Name = "Texto71";
            this.Texto71.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto71__GetValue);
            this.Texto71.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto71.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Texto71.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.Left, System.Drawing.Color.Black, 0.5, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto71.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto71.Font = new System.Drawing.Font("Arial Narrow", 16F, System.Drawing.FontStyle.Bold);
            this.Texto71.Guid = null;
            this.Texto71.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto71.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto71.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto71.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto120
            // 
            this.Texto120 = new Stimulsoft.Report.Components.StiText();
            this.Texto120.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(14.6, 10.4, 4.6, 0.4);
            this.Texto120.Guid = "00e0ef1e8f1649d998baa93403f72a44";
            this.Texto120.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Right;
            this.Texto120.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto120.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto120.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto120.Name = "Texto120";
            this.Texto120.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto120__GetValue);
            this.Texto120.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto120.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto120.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto120.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.Texto120.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto120.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto120.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto120.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto121
            // 
            this.Texto121 = new Stimulsoft.Report.Components.StiText();
            this.Texto121.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(16.1, 7.92, 3.1, 0.4);
            this.Texto121.Guid = "99c6b2f66d034f05b3f826badc04f669";
            this.Texto121.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Right;
            this.Texto121.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto121.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto121.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto121.Name = "Texto121";
            this.Texto121.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto121__GetValue);
            this.Texto121.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto121.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto121.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto121.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.Texto121.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto121.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto121.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto121.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto122
            // 
            this.Texto122 = new Stimulsoft.Report.Components.StiText();
            this.Texto122.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(16.1, 8.6, 3.1, 0.4);
            this.Texto122.Guid = "6c1cad64289940f586ea5f0f3e9444be";
            this.Texto122.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Right;
            this.Texto122.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto122.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto122.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto122.Name = "Texto122";
            this.Texto122.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto122__GetValue);
            this.Texto122.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto122.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto122.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto122.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.Texto122.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto122.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto122.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto122.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto123
            // 
            this.Texto123 = new Stimulsoft.Report.Components.StiText();
            this.Texto123.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(16.1, 9.12, 3.1, 0.4);
            this.Texto123.Guid = "f4e3771eea4c4ec39dbfa6cbe292dc80";
            this.Texto123.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Right;
            this.Texto123.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto123.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto123.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto123.Name = "Texto123";
            this.Texto123.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto123__GetValue);
            this.Texto123.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto123.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto123.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto123.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.Texto123.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto123.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto123.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto123.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto124
            // 
            this.Texto124 = new Stimulsoft.Report.Components.StiText();
            this.Texto124.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(16.1, 9.75, 3.1, 0.4);
            this.Texto124.Guid = "d873aeee82ce4675ad887b7489c940d7";
            this.Texto124.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Right;
            this.Texto124.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto124.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto124.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto124.Name = "Texto124";
            this.Texto124.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto124__GetValue);
            this.Texto124.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto124.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto124.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto124.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.Texto124.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto124.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto124.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto124.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto125
            // 
            this.Texto125 = new Stimulsoft.Report.Components.StiText();
            this.Texto125.CanBreak = true;
            this.Texto125.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0.2, 9.4, 13.8, 0.5);
            this.Texto125.GrowToHeight = true;
            this.Texto125.Guid = "b61bc6ff653c41c1a4325de2347252af";
            this.Texto125.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto125.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto125.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto125.Name = "Texto125";
            this.Texto125.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto125.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto125.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto125.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.Texto125.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto125.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto125.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto125.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, true, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // LinhaHorizontal2
            // 
            this.LinhaHorizontal2 = new Stimulsoft.Report.Components.StiHorizontalLinePrimitive();
            this.LinhaHorizontal2.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 10.8, 19.5, 0.0254);
            this.LinhaHorizontal2.Color = System.Drawing.Color.Black;
            this.LinhaHorizontal2.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.LinhaHorizontal2.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.LinhaHorizontal2.Name = "LinhaHorizontal2";
            this.LinhaHorizontal2.Guid = null;
            // 
            // LinhaHorizontal1
            // 
            this.LinhaHorizontal1 = new Stimulsoft.Report.Components.StiHorizontalLinePrimitive();
            this.LinhaHorizontal1.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 12.6, 19.5, 0.0254);
            this.LinhaHorizontal1.Color = System.Drawing.Color.Black;
            this.LinhaHorizontal1.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.LinhaHorizontal1.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.LinhaHorizontal1.Name = "LinhaHorizontal1";
            this.LinhaHorizontal1.Size = 1.2F;
            this.LinhaHorizontal1.Guid = null;
            // 
            // LinhaHorizontal4
            // 
            this.LinhaHorizontal4 = new Stimulsoft.Report.Components.StiHorizontalLinePrimitive();
            this.LinhaHorizontal4.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 5.4, 19.5, 0.0254);
            this.LinhaHorizontal4.Color = System.Drawing.Color.Black;
            this.LinhaHorizontal4.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.LinhaHorizontal4.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.LinhaHorizontal4.Name = "LinhaHorizontal4";
            this.LinhaHorizontal4.Size = 1.2F;
            this.LinhaHorizontal4.Guid = null;
            // 
            // LinhaHorizontal3
            // 
            this.LinhaHorizontal3 = new Stimulsoft.Report.Components.StiHorizontalLinePrimitive();
            this.LinhaHorizontal3.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 7.8, 14.2, 0.0254);
            this.LinhaHorizontal3.Color = System.Drawing.Color.Black;
            this.LinhaHorizontal3.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.LinhaHorizontal3.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.LinhaHorizontal3.Name = "LinhaHorizontal3";
            this.LinhaHorizontal3.Size = 1.2F;
            this.LinhaHorizontal3.Guid = null;
            // 
            // LinhaHorizontal5
            // 
            this.LinhaHorizontal5 = new Stimulsoft.Report.Components.StiHorizontalLinePrimitive();
            this.LinhaHorizontal5.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 4.2, 19.5, 0.0254);
            this.LinhaHorizontal5.Color = System.Drawing.Color.Black;
            this.LinhaHorizontal5.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.LinhaHorizontal5.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.LinhaHorizontal5.Name = "LinhaHorizontal5";
            this.LinhaHorizontal5.Style = Stimulsoft.Base.Drawing.StiPenStyle.Dash;
            this.LinhaHorizontal5.Guid = null;
            // 
            // LinhaHorizontal6
            // 
            this.LinhaHorizontal6 = new Stimulsoft.Report.Components.StiHorizontalLinePrimitive();
            this.LinhaHorizontal6.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 0, 19.5, 0.0254);
            this.LinhaHorizontal6.Color = System.Drawing.Color.Black;
            this.LinhaHorizontal6.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.LinhaHorizontal6.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.LinhaHorizontal6.Name = "LinhaHorizontal6";
            this.LinhaHorizontal6.Size = 0.5F;
            this.LinhaHorizontal6.Guid = null;
            this.RodapéPágina1.Guid = null;
            // 
            // Recipiente1
            // 
            this.Recipiente1 = new Stimulsoft.Report.Components.StiContainer();
            this.Recipiente1.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 1.8, 11.8, 10.9);
            this.Recipiente1.Dock = System.Windows.Forms.DockStyle.Left;
            this.Recipiente1.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Recipiente1.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Recipiente1.Name = "Recipiente1";
            this.Recipiente1.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Recipiente1.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            // 
            // Dados1
            // 
            this.Dados1 = new Stimulsoft.Report.Components.StiDataBand();
            this.Dados1.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 0.4, 11.8, 4.1);
            this.Dados1.DataRelationName = "";
            this.Dados1.DataSourceName = "Evento";
            this.Dados1.MaxHeight = 0;
            this.Dados1.MinHeight = 0;
            this.Dados1.Name = "Dados1";
            this.Dados1.Sort = new System.String[0];
            this.Dados1.StartNewPage = true;
            this.Dados1.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.Right, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Dados1.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            // 
            // Texto1
            // 
            this.Texto1 = new Stimulsoft.Report.Components.StiText();
            this.Texto1.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(3.4, 2, 4.6, 0.5);
            this.Texto1.Guid = "c5e82d4b24174c8e996efee1f9c9936c";
            this.Texto1.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
            this.Texto1.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto1.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto1.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto1.Name = "Texto1";
            this.Texto1.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto1__GetValue);
            this.Texto1.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto1.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto1.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.Texto1.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto1.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto1.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto1.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto77
            // 
            this.Texto77 = new Stimulsoft.Report.Components.StiText();
            this.Texto77.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0.1, 3.8, 8, 0.3);
            this.Texto77.Guid = "36b6784536f045d9a89c2dce7d127427";
            this.Texto77.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto77.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto77.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto77.Name = "Texto77";
            this.Texto77.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto77__GetValue);
            this.Texto77.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto77.Border = new Stimulsoft.Base.Drawing.StiBorder((((Stimulsoft.Base.Drawing.StiBorderSides.None | Stimulsoft.Base.Drawing.StiBorderSides.Top)
                            | Stimulsoft.Base.Drawing.StiBorderSides.Right)
                            | Stimulsoft.Base.Drawing.StiBorderSides.Bottom), System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto77.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto77.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            this.Texto77.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto77.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto77.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto77.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto93
            // 
            this.Texto93 = new Stimulsoft.Report.Components.StiText();
            this.Texto93.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(10.3, 3.8, 1.4, 0.3);
            this.Texto93.Guid = "ce88c846cd824fccb36768916ff5c534";
            this.Texto93.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto93.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto93.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto93.Name = "Texto93";
            this.Texto93.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto93__GetValue);
            this.Texto93.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto93.Border = new Stimulsoft.Base.Drawing.StiBorder((((Stimulsoft.Base.Drawing.StiBorderSides.None | Stimulsoft.Base.Drawing.StiBorderSides.Top)
                            | Stimulsoft.Base.Drawing.StiBorderSides.Left)
                            | Stimulsoft.Base.Drawing.StiBorderSides.Bottom), System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto93.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto93.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            this.Texto93.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto93.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto93.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto93.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto48
            // 
            this.Texto48 = new Stimulsoft.Report.Components.StiText();
            this.Texto48.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0.1, 1.5, 1.4, 0.4);
            this.Texto48.Guid = "78c2d5597d094f46b24889ab6cf25fca";
            this.Texto48.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto48.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto48.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto48.Name = "Texto48";
            this.Texto48.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto48__GetValue);
            this.Texto48.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto48.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto48.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto48.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto48.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto48.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto48.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto48.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto52
            // 
            this.Texto52 = new Stimulsoft.Report.Components.StiText();
            this.Texto52.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(1.5, 0.1, 9.7, 0.4);
            this.Texto52.Guid = "3a005a1949674ea79336ce6c40f26629";
            this.Texto52.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto52.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto52.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto52.Name = "Texto52";
            this.Texto52.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto52__GetValue);
            this.Texto52.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto52.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto52.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto52.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.Texto52.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto52.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto52.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto52.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto54
            // 
            this.Texto54 = new Stimulsoft.Report.Components.StiText();
            this.Texto54.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(1.5, 1.5, 3.4, 0.4);
            this.Texto54.Guid = "a166f2a777444c92ab2537467ff006a5";
            this.Texto54.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto54.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto54.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto54.Name = "Texto54";
            this.Texto54.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto54__GetValue);
            this.Texto54.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto54.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto54.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto54.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.Texto54.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto54.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto54.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto54.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto57
            // 
            this.Texto57 = new Stimulsoft.Report.Components.StiText();
            this.Texto57.CanBreak = true;
            this.Texto57.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(1.5, 0.5, 9.7, 1);
            this.Texto57.Guid = "ba6bbbde29b841cfab05c1caba0a2e5b";
            this.Texto57.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto57.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto57.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto57.Name = "Texto57";
            this.Texto57.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto57__GetValue);
            this.Texto57.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto57.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto57.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto57.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.Texto57.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto57.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto57.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto57.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, true, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto55
            // 
            this.Texto55 = new Stimulsoft.Report.Components.StiText();
            this.Texto55.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0.2, 2.5, 2, 0.35);
            this.Texto55.Guid = "3a847ed80dad42aa83875dd6c61891c5";
            this.Texto55.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Right;
            this.Texto55.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.InitializeComponent3();
        }

        public void InitializeComponent3()
        {
            this.Texto55.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto55.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto55.Name = "Texto55";
            this.Texto55.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto55__GetValue);
            this.Texto55.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto55.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto55.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto55.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto55.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto55.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto55.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto55.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto53
            // 
            this.Texto53 = new Stimulsoft.Report.Components.StiText();
            this.Texto53.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0.2, 2.85, 2, 0.35);
            this.Texto53.Guid = "ab459d7260e440b3bf7315746d536dae";
            this.Texto53.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Right;
            this.Texto53.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto53.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto53.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto53.Name = "Texto53";
            this.Texto53.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto53__GetValue);
            this.Texto53.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto53.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto53.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto53.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto53.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto53.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto53.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto53.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto56
            // 
            this.Texto56 = new Stimulsoft.Report.Components.StiText();
            this.Texto56.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0.2, 3.2, 2, 0.35);
            this.Texto56.Guid = "cd868360fa094e0787cd99189c12e362";
            this.Texto56.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Right;
            this.Texto56.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto56.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto56.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto56.Name = "Texto56";
            this.Texto56.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto56.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto56.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto56.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto56.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto56.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto56.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto56.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto62
            // 
            this.Texto62 = new Stimulsoft.Report.Components.StiText();
            this.Texto62.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(2.2, 2.5, 9, 0.35);
            this.Texto62.Guid = "ac0d1c38c8ca4b879c9ed48edeac259e";
            this.Texto62.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto62.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto62.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto62.Name = "Texto62";
            this.Texto62.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto62__GetValue);
            this.Texto62.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto62.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto62.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto62.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.Texto62.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto62.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto62.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto62.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto63
            // 
            this.Texto63 = new Stimulsoft.Report.Components.StiText();
            this.Texto63.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(2.2, 2.85, 9, 0.35);
            this.Texto63.Guid = "eecf9c39b9d549fba046409f153ec5cb";
            this.Texto63.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto63.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto63.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto63.Name = "Texto63";
            this.Texto63.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto63__GetValue);
            this.Texto63.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto63.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto63.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto63.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.Texto63.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto63.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto63.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto63.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto64
            // 
            this.Texto64 = new Stimulsoft.Report.Components.StiText();
            this.Texto64.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(2.2, 3.2, 9, 0.35);
            this.Texto64.Guid = "6a204489d2144ba49e981b1d54cba46b";
            this.Texto64.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto64.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto64.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto64.Name = "Texto64";
            this.Texto64.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto64.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto64.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto64.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.Texto64.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto64.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto64.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto64.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto94
            // 
            this.Texto94 = new Stimulsoft.Report.Components.StiText();
            this.Texto94.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0.1, 0.1, 1.2, 0.4);
            this.Texto94.Guid = "d60c594a17d64bb499a93c46db4ac6ca";
            this.Texto94.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto94.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto94.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto94.Name = "Texto94";
            this.Texto94.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto94__GetValue);
            this.Texto94.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto94.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto94.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto94.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto94.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto94.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto94.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto94.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto115
            // 
            this.Texto115 = new Stimulsoft.Report.Components.StiText();
            this.Texto115.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(8.1, 3.8, 1.1, 0.3);
            this.Texto115.Guid = "aae917245cc842da965c8fb93e1a77ec";
            this.Texto115.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto115.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto115.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto115.Name = "Texto115";
            this.Texto115.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto115__GetValue);
            this.Texto115.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto115.Border = new Stimulsoft.Base.Drawing.StiBorder((((Stimulsoft.Base.Drawing.StiBorderSides.None | Stimulsoft.Base.Drawing.StiBorderSides.Top)
                            | Stimulsoft.Base.Drawing.StiBorderSides.Right)
                            | Stimulsoft.Base.Drawing.StiBorderSides.Bottom), System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto115.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto115.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            this.Texto115.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto115.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto115.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto115.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto116
            // 
            this.Texto116 = new Stimulsoft.Report.Components.StiText();
            this.Texto116.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(9.2, 3.8, 1.1, 0.3);
            this.Texto116.Guid = "895021df0f0d48ce83c75dd69426bd3f";
            this.Texto116.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto116.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto116.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto116.Name = "Texto116";
            this.Texto116.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto116__GetValue);
            this.Texto116.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto116.Border = new Stimulsoft.Base.Drawing.StiBorder((((Stimulsoft.Base.Drawing.StiBorderSides.None | Stimulsoft.Base.Drawing.StiBorderSides.Top)
                            | Stimulsoft.Base.Drawing.StiBorderSides.Right)
                            | Stimulsoft.Base.Drawing.StiBorderSides.Bottom), System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto116.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto116.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            this.Texto116.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto116.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto116.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto116.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // LinhaHorizontal9
            // 
            this.LinhaHorizontal9 = new Stimulsoft.Report.Components.StiHorizontalLinePrimitive();
            this.LinhaHorizontal9.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 2, 11.8, 0.0254);
            this.LinhaHorizontal9.Color = System.Drawing.Color.Black;
            this.LinhaHorizontal9.Guid = "fe8380561e8c43289907aff42c0555c1";
            this.LinhaHorizontal9.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.LinhaHorizontal9.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.LinhaHorizontal9.Name = "LinhaHorizontal9";
            this.Dados1.Guid = null;
            this.Dados1.MasterComponent = null;
            // 
            // Dados2
            // 
            this.Dados2 = new Stimulsoft.Report.Components.StiDataBand();
            this.Dados2.CanGrow = false;
            this.Dados2.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 5.3, 11.8, 0.3);
            this.Dados2.DataRelationName = "boleto_AtividadeBoleto";
            this.Dados2.DataSourceName = "AtividadeBoleto";
            this.Dados2.Guid = "eba6c341a28c4101a8ad6df490faaeb2";
            this.Dados2.KeepFooterTogether = false;
            this.Dados2.KeepHeaderTogether = false;
            this.Dados2.MaxHeight = 0;
            this.Dados2.MinHeight = 0;
            this.Dados2.Name = "Dados2";
            this.Dados2.Sort = new System.String[0];
            this.Dados2.StartNewPageIfLessThan = 0F;
            this.Dados2.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.Right, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Dados2.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            // 
            // Texto65
            // 
            this.Texto65 = new Stimulsoft.Report.Components.StiText();
            this.Texto65.CanBreak = true;
            this.Texto65.CanGrow = true;
            this.Texto65.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0.1, 0, 8, 0.3);
            this.Texto65.BeforePrint += new System.EventHandler(this.Texto65_Conditions);
            this.Texto65.Guid = "8ed2e504c45b41b39baea42da414b760";
            this.Texto65.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto65.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto65.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto65.Name = "Texto65";
            this.Texto65.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto65__GetValue);
            this.Texto65.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto65.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto65.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto65.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto65.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto65.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto65.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto65.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, true, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto99
            // 
            this.Texto99 = new Stimulsoft.Report.Components.StiText();
            this.Texto99.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(10.3, 0, 1.4, 0.3);
            this.Texto99.BeforePrint += new System.EventHandler(this.Texto99_Conditions);
            this.Texto99.Guid = "b52047e2af454a589bf7e89c1279b469";
            this.Texto99.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Right;
            this.Texto99.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto99.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto99.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto99.Name = "Texto99";
            this.Texto99.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto99__GetValue);
            this.Texto99.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto99.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto99.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto99.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto99.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto99.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto99.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto99.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto118
            // 
            this.Texto118 = new Stimulsoft.Report.Components.StiText();
            this.Texto118.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(8.1, 0, 1.1, 0.3);
            this.Texto118.BeforePrint += new System.EventHandler(this.Texto118_Conditions);
            this.Texto118.Guid = "8f7e46d178f646db875ca8a46397a834";
            this.Texto118.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Right;
            this.Texto118.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto118.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto118.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto118.Name = "Texto118";
            this.Texto118.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto118__GetValue);
            this.Texto118.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto118.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto118.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto118.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto118.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto118.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto118.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto118.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Texto119
            // 
            this.Texto119 = new Stimulsoft.Report.Components.StiText();
            this.Texto119.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(9.2, 0, 1.1, 0.3);
            this.Texto119.BeforePrint += new System.EventHandler(this.Texto119_Conditions);
            this.Texto119.Guid = "07a8bc7746fc4243a5b57257772c2a37";
            this.Texto119.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Right;
            this.Texto119.LinesOfUnderline = Stimulsoft.Base.Drawing.StiPenStyle.None;
            this.Texto119.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto119.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Texto119.Name = "Texto119";
            this.Texto119.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Texto119__GetValue);
            this.Texto119.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Texto119.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Texto119.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Texto119.Font = new System.Drawing.Font("Arial", 7F);
            this.Texto119.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Texto119.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Texto119.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiGeneralFormatService();
            this.Texto119.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            this.Dados2.OddStyle = null;
            this.Recipiente1.Guid = null;
            // 
            // Recipiente2
            // 
            this.Recipiente2 = new Stimulsoft.Report.Components.StiContainer();
            this.Recipiente2.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(11.9, 1.8, 7.6, 10.9);
            this.Recipiente2.Dock = System.Windows.Forms.DockStyle.Right;
            this.Recipiente2.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Recipiente2.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.Recipiente2.Name = "Recipiente2";
            this.Recipiente2.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.Top, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.Recipiente2.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            // 
            // TextoRico1
            // 
            this.TextoRico1 = new Stimulsoft.Report.Components.StiRichText();
            this.TextoRico1.BackColor = System.Drawing.Color.White;
            this.TextoRico1.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 6.8, 7.6, 4.1);
            this.TextoRico1.DataColumn = "";
            this.TextoRico1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TextoRico1.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.TextoRico1.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.TextoRico1.Name = "TextoRico1";
            this.TextoRico1.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.TextoRico1__GetValue);
            this.TextoRico1.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.Right, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.TextoRico1.Guid = null;
            // 
            // TextoRico2
            // 
            this.TextoRico2 = new Stimulsoft.Report.Components.StiRichText();
            this.TextoRico2.BackColor = System.Drawing.Color.Transparent;
            this.TextoRico2.CanBreak = true;
            this.TextoRico2.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 0, 7.6, 6.8);
            this.TextoRico2.DataColumn = "";
            this.TextoRico2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextoRico2.Editable = true;
            this.TextoRico2.FullConvertExpression = true;
            this.TextoRico2.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.TextoRico2.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.TextoRico2.Name = "TextoRico2";
            this.TextoRico2.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.TextoRico2__GetValue);
            this.TextoRico2.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.Right, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black));
            this.TextoRico2.Guid = null;
            // 
            // StartPointPrimitive7
            // 
            this.StartPointPrimitive7 = new Stimulsoft.Report.Components.StiStartPointPrimitive();
            this.StartPointPrimitive7.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(3.4, 9.8, 0, 0);
            this.StartPointPrimitive7.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.StartPointPrimitive7.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.StartPointPrimitive7.Name = "StartPointPrimitive7";
            this.StartPointPrimitive7.ReferenceToGuid = "1ece2f06abf34825a7f19adb31dc3951";
            this.StartPointPrimitive7.Guid = null;
            // 
            // EndPointPrimitive2
            // 
            this.EndPointPrimitive2 = new Stimulsoft.Report.Components.StiEndPointPrimitive();
            this.EndPointPrimitive2.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(3.23, 9.6, 0, 0);
            this.EndPointPrimitive2.Guid = "799bf8a84c0f4921a99774ddda9e6808";
            this.EndPointPrimitive2.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.EndPointPrimitive2.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.EndPointPrimitive2.Name = "EndPointPrimitive2";
            this.EndPointPrimitive2.ReferenceToGuid = "1ece2f06abf34825a7f19adb31dc3951";
            // 
            // EndPointPrimitive3
            // 
            this.EndPointPrimitive3 = new Stimulsoft.Report.Components.StiEndPointPrimitive();
            this.EndPointPrimitive3.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(3.23, 10.8, 0, 0);
            this.EndPointPrimitive3.Guid = "e74a02e51c614192abf2d593f331e299";
            this.EndPointPrimitive3.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.EndPointPrimitive3.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.EndPointPrimitive3.Name = "EndPointPrimitive3";
            this.EndPointPrimitive3.ReferenceToGuid = "377c933b2a094874961dc6858517f54e";
            this.Recipiente2.Guid = null;
            // 
            // StartPointPrimitive8
            // 
            this.StartPointPrimitive8 = new Stimulsoft.Report.Components.StiStartPointPrimitive();
            this.StartPointPrimitive8.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(14.8, 12.8, 0, 0);
            this.StartPointPrimitive8.MaxSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.StartPointPrimitive8.MinSize = new Stimulsoft.Base.Drawing.SizeD(0, 0);
            this.StartPointPrimitive8.Name = "StartPointPrimitive8";
            this.StartPointPrimitive8.ReferenceToGuid = "377c933b2a094874961dc6858517f54e";
            this.StartPointPrimitive8.Guid = null;
            this.Página1.ExcelSheetValue = null;
            this.Página1.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Página1_Watermark = new Stimulsoft.Report.Components.StiWatermark();
            this.Página1_Watermark.Font = new System.Drawing.Font("Arial", 100F);
            this.Página1_Watermark.Image = null;
            this.Página1_Watermark.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.FromArgb(50, 0, 0, 0));
            this.Relatório_PrinterSettings = new Stimulsoft.Report.Print.StiPrinterSettings();
            this.PrinterSettings = this.Relatório_PrinterSettings;
            this.Página1.Page = this.Página1;
            this.Página1.Report = this;
            this.Página1.Watermark = this.Página1_Watermark;
            this.CabeçalhoPágina1.Page = this.Página1;
            this.CabeçalhoPágina1.Parent = this.Página1;
            this.Imagem3.Page = this.Página1;
            this.Imagem3.Parent = this.CabeçalhoPágina1;
            this.TextoRico3.Page = this.Página1;
            this.TextoRico3.Parent = this.CabeçalhoPágina1;
            this.LinhaHorizontal7.Page = this.Página1;
            this.LinhaHorizontal7.Parent = this.CabeçalhoPágina1;
            this.RodapéPágina1.Page = this.Página1;
            this.RodapéPágina1.Parent = this.Página1;
            this.Texto72.Page = this.Página1;
            this.Texto72.Parent = this.RodapéPágina1;
            this.Texto61.Page = this.Página1;
            this.Texto61.Parent = this.RodapéPágina1;
            this.Texto60.Page = this.Página1;
            this.Texto60.Parent = this.RodapéPágina1;
            this.Texto59.Page = this.Página1;
            this.Texto59.Parent = this.RodapéPágina1;
            this.Texto4.Page = this.Página1;
            this.Texto4.Parent = this.RodapéPágina1;
            this.Texto6.Page = this.Página1;
            this.Texto6.Parent = this.RodapéPágina1;
            this.Texto9.Page = this.Página1;
            this.Texto9.Parent = this.RodapéPágina1;
            this.Texto12.Page = this.Página1;
            this.Texto12.Parent = this.RodapéPágina1;
            this.Texto7.Page = this.Página1;
            this.Texto7.Parent = this.RodapéPágina1;
            this.Texto11.Page = this.Página1;
            this.Texto11.Parent = this.RodapéPágina1;
            this.Texto10.Page = this.Página1;
            this.Texto10.Parent = this.RodapéPágina1;
            this.Texto5.Page = this.Página1;
            this.Texto5.Parent = this.RodapéPágina1;
            this.Texto8.Page = this.Página1;
            this.Texto8.Parent = this.RodapéPágina1;
            this.Texto3.Page = this.Página1;
            this.Texto3.Parent = this.RodapéPágina1;
            this.CódigoBarras1.Page = this.Página1;
            this.CódigoBarras1.Parent = this.RodapéPágina1;
            this.Texto17.Page = this.Página1;
            this.Texto17.Parent = this.RodapéPágina1;
            this.Texto46.Page = this.Página1;
            this.Texto46.Parent = this.RodapéPágina1;
            this.Texto18.Page = this.Página1;
            this.Texto18.Parent = this.RodapéPágina1;
            this.Texto51.Page = this.Página1;
            this.Texto51.Parent = this.RodapéPágina1;
            this.Texto19.Page = this.Página1;
            this.Texto19.Parent = this.RodapéPágina1;
            this.Texto20.Page = this.Página1;
            this.Texto20.Parent = this.RodapéPágina1;
            this.Texto21.Page = this.Página1;
            this.Texto21.Parent = this.RodapéPágina1;
            this.Imagem1.Page = this.Página1;
            this.Imagem1.Parent = this.RodapéPágina1;
            this.Texto2.Page = this.Página1;
            this.Texto2.Parent = this.RodapéPágina1;
            this.Texto26.Page = this.Página1;
            this.Texto26.Parent = this.RodapéPágina1;
            this.Texto13.Page = this.Página1;
            this.Texto13.Parent = this.RodapéPágina1;
            this.Texto27.Page = this.Página1;
            this.Texto27.Parent = this.RodapéPágina1;
            this.Texto22.Page = this.Página1;
            this.Texto22.Parent = this.RodapéPágina1;
            this.Texto29.Page = this.Página1;
            this.Texto29.Parent = this.RodapéPágina1;
            this.Texto14.Page = this.Página1;
            this.Texto14.Parent = this.RodapéPágina1;
            this.Texto28.Page = this.Página1;
            this.Texto28.Parent = this.RodapéPágina1;
            this.Texto23.Page = this.Página1;
            this.Texto23.Parent = this.RodapéPágina1;
            this.Texto30.Page = this.Página1;
            this.Texto30.Parent = this.RodapéPágina1;
            this.Texto15.Page = this.Página1;
            this.Texto15.Parent = this.RodapéPágina1;
            this.Texto31.Page = this.Página1;
            this.Texto31.Parent = this.RodapéPágina1;
            this.Texto32.Page = this.Página1;
            this.Texto32.Parent = this.RodapéPágina1;
            this.Texto33.Page = this.Página1;
            this.Texto33.Parent = this.RodapéPágina1;
            this.Texto34.Page = this.Página1;
            this.Texto34.Parent = this.RodapéPágina1;
            this.Texto35.Page = this.Página1;
            this.Texto35.Parent = this.RodapéPágina1;
            this.Texto24.Page = this.Página1;
            this.Texto24.Parent = this.RodapéPágina1;
            this.Texto36.Page = this.Página1;
            this.Texto36.Parent = this.RodapéPágina1;
            this.Texto37.Page = this.Página1;
            this.Texto37.Parent = this.RodapéPágina1;
            this.Texto38.Page = this.Página1;
            this.Texto38.Parent = this.RodapéPágina1;
            this.Texto39.Page = this.Página1;
            this.Texto39.Parent = this.RodapéPágina1;
            this.Texto40.Page = this.Página1;
            this.Texto40.Parent = this.RodapéPágina1;
            this.Texto16.Page = this.Página1;
            this.Texto16.Parent = this.RodapéPágina1;
            this.Texto41.Page = this.Página1;
            this.Texto41.Parent = this.RodapéPágina1;
            this.Texto42.Page = this.Página1;
            this.Texto42.Parent = this.RodapéPágina1;
            this.Texto43.Page = this.Página1;
            this.Texto43.Parent = this.RodapéPágina1;
            this.Texto44.Page = this.Página1;
            this.Texto44.Parent = this.RodapéPágina1;
            this.Texto45.Page = this.Página1;
            this.Texto45.Parent = this.RodapéPágina1;
            this.Texto25.Page = this.Página1;
            this.Texto25.Parent = this.RodapéPágina1;
            this.Texto49.Page = this.Página1;
            this.Texto49.Parent = this.RodapéPágina1;
            this.Texto50.Page = this.Página1;
            this.Texto50.Parent = this.RodapéPágina1;
            this.Texto47.Page = this.Página1;
            this.Texto47.Parent = this.RodapéPágina1;
            this.Texto70.Page = this.Página1;
            this.Texto70.Parent = this.RodapéPágina1;
            this.Texto69.Page = this.Página1;
            this.Texto69.Parent = this.RodapéPágina1;
            this.Texto74.Page = this.Página1;
            this.Texto74.Parent = this.RodapéPágina1;
            this.Texto68.Page = this.Página1;
            this.Texto68.Parent = this.RodapéPágina1;
            this.Texto66.Page = this.Página1;
            this.Texto66.Parent = this.RodapéPágina1;
            this.Texto82.Page = this.Página1;
            this.Texto82.Parent = this.RodapéPágina1;
            this.Texto83.Page = this.Página1;
            this.Texto83.Parent = this.RodapéPágina1;
            this.Texto73.Page = this.Página1;
            this.Texto73.Parent = this.RodapéPágina1;
            this.Texto75.Page = this.Página1;
            this.Texto75.Parent = this.RodapéPágina1;
            this.Texto76.Page = this.Página1;
            this.Texto76.Parent = this.RodapéPágina1;
            this.Texto79.Page = this.Página1;
            this.Texto79.Parent = this.RodapéPágina1;
            this.Texto80.Page = this.Página1;
            this.Texto80.Parent = this.RodapéPágina1;
            this.Texto81.Page = this.Página1;
            this.Texto81.Parent = this.RodapéPágina1;
            this.Texto84.Page = this.Página1;
            this.Texto84.Parent = this.RodapéPágina1;
            this.Texto85.Page = this.Página1;
            this.Texto85.Parent = this.RodapéPágina1;
            this.Texto86.Page = this.Página1;
            this.Texto86.Parent = this.RodapéPágina1;
            this.Texto87.Page = this.Página1;
            this.Texto87.Parent = this.RodapéPágina1;
            this.Texto92.Page = this.Página1;
            this.Texto92.Parent = this.RodapéPágina1;
            this.Texto101.Page = this.Página1;
            this.Texto101.Parent = this.RodapéPágina1;
            this.Texto102.Page = this.Página1;
            this.Texto102.Parent = this.RodapéPágina1;
            this.Texto58.Page = this.Página1;
            this.Texto58.Parent = this.RodapéPágina1;
            this.Texto67.Page = this.Página1;
            this.Texto67.Parent = this.RodapéPágina1;
            this.Texto89.Page = this.Página1;
            this.Texto89.Parent = this.RodapéPágina1;
            this.Imagem2.Page = this.Página1;
            this.Imagem2.Parent = this.RodapéPágina1;
            this.Texto71.Page = this.Página1;
            this.Texto71.Parent = this.RodapéPágina1;
            this.Texto120.Page = this.Página1;
            this.Texto120.Parent = this.RodapéPágina1;
            this.Texto121.Page = this.Página1;
            this.Texto121.Parent = this.RodapéPágina1;
            this.Texto122.Page = this.Página1;
            this.Texto122.Parent = this.RodapéPágina1;
            this.Texto123.Page = this.Página1;
            this.Texto123.Parent = this.RodapéPágina1;
            this.Texto124.Page = this.Página1;
            this.Texto124.Parent = this.RodapéPágina1;
            this.Texto125.Page = this.Página1;
            this.Texto125.Parent = this.RodapéPágina1;
            this.LinhaHorizontal2.Page = this.Página1;
            this.LinhaHorizontal2.Parent = this.RodapéPágina1;
            this.LinhaHorizontal1.Page = this.Página1;
            this.LinhaHorizontal1.Parent = this.RodapéPágina1;
            this.LinhaHorizontal4.Page = this.Página1;
            this.LinhaHorizontal4.Parent = this.RodapéPágina1;
            this.LinhaHorizontal3.Page = this.Página1;
            this.LinhaHorizontal3.Parent = this.RodapéPágina1;
            this.LinhaHorizontal5.Page = this.Página1;
            this.LinhaHorizontal5.Parent = this.RodapéPágina1;
            this.LinhaHorizontal6.Page = this.Página1;
            this.LinhaHorizontal6.Parent = this.RodapéPágina1;
            this.Recipiente1.Page = this.Página1;
            this.Recipiente1.Parent = this.Página1;
            this.Dados1.Page = this.Página1;
            this.Dados1.Parent = this.Recipiente1;
            this.Texto1.Page = this.Página1;
            this.Texto1.Parent = this.Dados1;
            this.Texto77.Page = this.Página1;
            this.Texto77.Parent = this.Dados1;
            this.Texto93.Page = this.Página1;
            this.Texto93.Parent = this.Dados1;
            this.Texto48.Page = this.Página1;
            this.Texto48.Parent = this.Dados1;
            this.Texto52.Page = this.Página1;
            this.Texto52.Parent = this.Dados1;
            this.Texto54.Page = this.Página1;
            this.Texto54.Parent = this.Dados1;
            this.Texto57.Page = this.Página1;
            this.Texto57.Parent = this.Dados1;
            this.Texto55.Page = this.Página1;
            this.Texto55.Parent = this.Dados1;
            this.Texto53.Page = this.Página1;
            this.Texto53.Parent = this.Dados1;
            this.Texto56.Page = this.Página1;
            this.Texto56.Parent = this.Dados1;
            this.Texto62.Page = this.Página1;
            this.Texto62.Parent = this.Dados1;
            this.Texto63.Page = this.Página1;
            this.Texto63.Parent = this.Dados1;
            this.Texto64.Page = this.Página1;
            this.Texto64.Parent = this.Dados1;
            this.Texto94.Page = this.Página1;
            this.Texto94.Parent = this.Dados1;
            this.Texto115.Page = this.Página1;
            this.Texto115.Parent = this.Dados1;
            this.Texto116.Page = this.Página1;
            this.Texto116.Parent = this.Dados1;
            this.LinhaHorizontal9.Page = this.Página1;
            this.LinhaHorizontal9.Parent = this.Dados1;
            this.Dados2.MasterComponent = this.Dados1;
            this.Dados2.Page = this.Página1;
            this.Dados2.Parent = this.Recipiente1;
            this.Texto65.Page = this.Página1;
            this.Texto65.Parent = this.Dados2;
            this.Texto99.Page = this.Página1;
            this.Texto99.Parent = this.Dados2;
            this.Texto118.Page = this.Página1;
            this.Texto118.Parent = this.Dados2;
            this.Texto119.Page = this.Página1;
            this.Texto119.Parent = this.Dados2;
            this.Recipiente2.Page = this.Página1;
            this.Recipiente2.Parent = this.Página1;
            this.TextoRico1.Page = this.Página1;
            this.TextoRico1.Parent = this.Recipiente2;
            this.TextoRico2.Page = this.Página1;
            this.TextoRico2.Parent = this.Recipiente2;
            this.StartPointPrimitive7.Page = this.Página1;
            this.StartPointPrimitive7.Parent = this.Recipiente2;
            this.EndPointPrimitive2.Page = this.Página1;
            this.EndPointPrimitive2.Parent = this.Recipiente2;
            this.EndPointPrimitive3.Page = this.Página1;
            this.EndPointPrimitive3.Parent = this.Recipiente2;
            this.StartPointPrimitive8.Page = this.Página1;
            this.StartPointPrimitive8.Parent = this.Página1;
            // 
            // Add to CabeçalhoPágina1.Components
            // 
            this.CabeçalhoPágina1.Components.Clear();
            this.CabeçalhoPágina1.Components.AddRange(new Stimulsoft.Report.Components.StiComponent[] {
                        this.Imagem3,
                        this.TextoRico3,
                        this.LinhaHorizontal7});
            // 
            // Add to RodapéPágina1.Components
            // 
            this.RodapéPágina1.Components.Clear();
            this.RodapéPágina1.Components.AddRange(new Stimulsoft.Report.Components.StiComponent[] {
                        this.Texto72,
                        this.Texto61,
                        this.Texto60,
                        this.Texto59,
                        this.Texto4,
                        this.Texto6,
                        this.Texto9,
                        this.Texto12,
                        this.Texto7,
                        this.Texto11,
                        this.Texto10,
                        this.Texto5,
                        this.Texto8,
                        this.Texto3,
                        this.CódigoBarras1,
                        this.Texto17,
                        this.Texto46,
                        this.Texto18,
                        this.Texto51,
                        this.Texto19,
                        this.Texto20,
                        this.Texto21,
                        this.Imagem1,
                        this.Texto2,
                        this.Texto26,
                        this.Texto13,
                        this.Texto27,
                        this.Texto22,
                        this.Texto29,
                        this.Texto14,
                        this.Texto28,
                        this.Texto23,
                        this.Texto30,
                        this.Texto15,
                        this.Texto31,
                        this.Texto32,
                        this.Texto33,
                        this.Texto34,
                        this.Texto35,
                        this.Texto24,
                        this.Texto36,
                        this.Texto37,
                        this.Texto38,
                        this.Texto39,
                        this.Texto40,
                        this.Texto16,
                        this.Texto41,
                        this.Texto42,
                        this.Texto43,
                        this.Texto44,
                        this.Texto45,
                        this.Texto25,
                        this.Texto49,
                        this.Texto50,
                        this.Texto47,
                        this.Texto70,
                        this.Texto69,
                        this.Texto74,
                        this.Texto68,
                        this.Texto66,
                        this.Texto82,
                        this.Texto83,
                        this.Texto73,
                        this.Texto75,
                        this.Texto76,
                        this.Texto79,
                        this.Texto80,
                        this.Texto81,
                        this.Texto84,
                        this.Texto85,
                        this.Texto86,
                        this.Texto87,
                        this.Texto92,
                        this.Texto101,
                        this.Texto102,
                        this.Texto58,
                        this.Texto67,
                        this.Texto89,
                        this.Imagem2,
                        this.Texto71,
                        this.Texto120,
                        this.Texto121,
                        this.Texto122,
                        this.Texto123,
                        this.Texto124,
                        this.Texto125,
                        this.LinhaHorizontal2,
                        this.LinhaHorizontal1,
                        this.LinhaHorizontal4,
                        this.LinhaHorizontal3,
                        this.LinhaHorizontal5,
                        this.LinhaHorizontal6});
            // 
            // Add to Dados1.Components
            // 
            this.Dados1.Components.Clear();
            this.Dados1.Components.AddRange(new Stimulsoft.Report.Components.StiComponent[] {
                        this.Texto1,
                        this.Texto77,
                        this.Texto93,
                        this.Texto48,
                        this.Texto52,
                        this.Texto54,
                        this.Texto57,
                        this.Texto55,
                        this.Texto53,
                        this.Texto56,
                        this.Texto62,
                        this.Texto63,
                        this.Texto64,
                        this.Texto94,
                        this.Texto115,
                        this.Texto116,
                        this.LinhaHorizontal9});
            // 
            // Add to Dados2.Components
            // 
            this.Dados2.Components.Clear();
            this.Dados2.Components.AddRange(new Stimulsoft.Report.Components.StiComponent[] {
                        this.Texto65,
                        this.Texto99,
                        this.Texto118,
                        this.Texto119});
            // 
            // Add to Recipiente1.Components
            // 
            this.Recipiente1.Components.Clear();
            this.Recipiente1.Components.AddRange(new Stimulsoft.Report.Components.StiComponent[] {
                        this.Dados1,
                        this.Dados2});
            // 
            // Add to Recipiente2.Components
            // 
            this.Recipiente2.Components.Clear();
            this.Recipiente2.Components.AddRange(new Stimulsoft.Report.Components.StiComponent[] {
                        this.TextoRico1,
                        this.TextoRico2,
                        this.StartPointPrimitive7,
                        this.EndPointPrimitive2,
                        this.EndPointPrimitive3});
            // 
            // Add to Página1.Components
            // 
            this.Página1.Components.Clear();
            this.Página1.Components.AddRange(new Stimulsoft.Report.Components.StiComponent[] {
                        this.CabeçalhoPágina1,
                        this.RodapéPágina1,
                        this.Recipiente1,
                        this.Recipiente2,
                        this.StartPointPrimitive8});
            // 
            // Add to Pages
            // 
            this.Pages.Clear();
            this.Pages.AddRange(new Stimulsoft.Report.Components.StiPage[] {
                        this.Página1});
            this.Dictionary.Relations.Add(this.ParentEvento);
            this.Dictionary.Relations.Add(this.Parentboleto);
            this.Evento.Columns.AddRange(new Stimulsoft.Report.Dictionary.StiDataColumn[] {
                        new Stimulsoft.Report.Dictionary.StiDataColumn("cdEvento", "cdEvento", "cdEvento", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("cdCliente", "cdCliente", "cdCliente", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("noEvento", "noEvento", "noEvento", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("dsEvento", "dsEvento", "dsEvento", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("dtInicioEvento", "dtInicioEvento", "dtInicioEvento", typeof(DateTime)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("dtFinalEvento", "dtFinalEvento", "dtFinalEvento", typeof(DateTime)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("flAtivo", "flAtivo", "flAtivo", typeof(bool)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("flEventoComRecebimentos", "flEventoComRecebimentos", "flEventoComRecebimentos", typeof(bool))});
            this.DataSources.Add(this.Evento);
            this.EventosImgs.Columns.AddRange(new Stimulsoft.Report.Dictionary.StiDataColumn[] {
                        new Stimulsoft.Report.Dictionary.StiDataColumn("cdEvento", "cdEvento", "cdEvento", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("imgBanner", "imgBanner", "imgBanner", typeof(byte[])),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("imgLogo", "imgLogo", "imgLogo", typeof(byte[]))});
            this.DataSources.Add(this.EventosImgs);
            this.DataHora.Columns.AddRange(new Stimulsoft.Report.Dictionary.StiDataColumn[] {
                        new Stimulsoft.Report.Dictionary.StiDataColumn("DataHora", "DataHora", "DataHora", typeof(DateTime))});
            this.DataSources.Add(this.DataHora);
            this.AtividadeBoleto.Columns.AddRange(new Stimulsoft.Report.Dictionary.StiDataColumn[] {
                        new Stimulsoft.Report.Dictionary.StiDataColumn("cdPedido", "cdPedido", "cdPedido", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("vlUnitario", "vlUnitario", "vlUnitario", typeof(decimal)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("vlUnitarioDesconto", "vlUnitarioDesconto", "vlUnitarioDesconto", typeof(decimal)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("cdAtividade", "cdAtividade", "cdAtividade", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("cdEvento", "cdEvento", "cdEvento", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("cdTipoAtividade", "cdTipoAtividade", "cdTipoAtividade", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("noTitulo", "noTitulo", "noTitulo", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("noSubTitulo", "noSubTitulo", "noSubTitulo", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("dsTema", "dsTema", "dsTema", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("tpControle", "tpControle", "tpControle", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("flCertificacao", "flCertificacao", "flCertificacao", typeof(bool)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("vlAtividade", "vlAtividade", "vlAtividade", typeof(decimal)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("dtInicio", "dtInicio", "dtInicio", typeof(DateTime)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("dtTermino", "dtTermino", "dtTermino", typeof(DateTime)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("vlCargaHoraria", "vlCargaHoraria", "vlCargaHoraria", typeof(decimal)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("flAtivo", "flAtivo", "flAtivo", typeof(bool)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("flPagamento", "flPagamento", "flPagamento", typeof(bool)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("flVisivel_Inscricao", "flVisivel_Inscricao", "flVisivel_Inscricao", typeof(bool)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("cdLocal", "cdLocal", "cdLocal", typeof(string))});
            this.DataSources.Add(this.AtividadeBoleto);
            this.boleto.Columns.AddRange(new Stimulsoft.Report.Dictionary.StiDataColumn[] {
                        new Stimulsoft.Report.Dictionary.StiDataColumn("cdBoleto", "cdBoleto", "cdBoleto", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("cdPedido", "cdPedido", "cdPedido", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("cdEvento", "cdEvento", "cdEvento", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("dtEmissao", "dtEmissao", "dtEmissao", typeof(DateTime)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("dtVencimento", "dtVencimento", "dtVencimento", typeof(DateTime)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("vlBoleto", "vlBoleto", "vlBoleto", typeof(decimal)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("vlTaxaBancaria", "vlTaxaBancaria", "vlTaxaBancaria", typeof(decimal)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("flRecebido", "flRecebido", "flRecebido", typeof(bool)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("dtPedido", "dtPedido", "dtPedido", typeof(DateTime)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("cdEvento1", "cdEvento1", "cdEvento1", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("cdParticipante", "cdParticipante", "cdParticipante", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("cdCredencial", "cdCredencial", "cdCredencial", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("dtCredencial", "dtCredencial", "dtCredencial", typeof(DateTime)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("cdCategoria", "cdCategoria", "cdCategoria", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("noParticipante", "noParticipante", "noParticipante", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("noEtiqueta", "noEtiqueta", "noEtiqueta", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("nuCPFCNPJ", "nuCPFCNPJ", "nuCPFCNPJ", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("dsDocIdentificador", "dsDocIdentificador", "dsDocIdentificador", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("dsEmail", "dsEmail", "dsEmail", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("nuCEP", "nuCEP", "nuCEP", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("dsEndereco", "dsEndereco", "dsEndereco", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("noBairro", "noBairro", "noBairro", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("noCidade", "noCidade", "noCidade", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("dsUF", "dsUF", "dsUF", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("noPais", "noPais", "noPais", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("dsFone1", "dsFone1", "dsFone1", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("dsFone2", "dsFone2", "dsFone2", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("dsFone3", "dsFone3", "dsFone3", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("noInstituicao", "noInstituicao", "noInstituicao", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("noCargo", "noCargo", "noCargo", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("noAreaAtuacao", "noAreaAtuacao", "noAreaAtuacao", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("dsSexo", "dsSexo", "dsSexo", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("dsNascimento", "dsNascimento", "dsNascimento", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("dsObservacoes", "dsObservacoes", "dsObservacoes", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("dsAuxiliar1", "dsAuxiliar1", "dsAuxiliar1", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("dsAuxiliar2", "dsAuxiliar2", "dsAuxiliar2", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("dsAuxiliar3", "dsAuxiliar3", "dsAuxiliar3", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("dsAuxiliar4", "dsAuxiliar4", "dsAuxiliar4", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("dsAuxiliar5", "dsAuxiliar5", "dsAuxiliar5", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("dsAuxiliar6", "dsAuxiliar6", "dsAuxiliar6", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("dsAuxiliar7", "dsAuxiliar7", "dsAuxiliar7", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("dsAuxiliar8", "dsAuxiliar8", "dsAuxiliar8", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("dsAuxiliar9", "dsAuxiliar9", "dsAuxiliar9", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("dsAuxiliar10", "dsAuxiliar10", "dsAuxiliar10", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("dsAuxiliar11", "dsAuxiliar11", "dsAuxiliar11", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("dsAuxiliar12", "dsAuxiliar12", "dsAuxiliar12", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("dsAuxiliar13", "dsAuxiliar13", "dsAuxiliar13", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("dsAuxiliar14", "dsAuxiliar14", "dsAuxiliar14", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("dsAuxiliar15", "dsAuxiliar15", "dsAuxiliar15", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("dsAuxiliar16", "dsAuxiliar16", "dsAuxiliar16", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("dsAuxiliar17", "dsAuxiliar17", "dsAuxiliar17", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("dsAuxiliar18", "dsAuxiliar18", "dsAuxiliar18", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("dsAuxiliar19", "dsAuxiliar19", "dsAuxiliar19", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("flAtivo", "flAtivo", "flAtivo", typeof(bool)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("dtCadastro", "dtCadastro", "dtCadastro", typeof(DateTime)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("dsSenhaWeb", "dsSenhaWeb", "dsSenhaWeb", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("flEmailValidado", "flEmailValidado", "flEmailValidado", typeof(bool)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("noCategoria", "noCategoria", "noCategoria", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("cdEvento2", "cdEvento2", "cdEvento2", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("cdCarteira", "cdCarteira", "cdCarteira", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("noCedente", "noCedente", "noCedente", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("cdConvenio", "cdConvenio", "cdConvenio", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("cdCedente", "cdCedente", "cdCedente", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("nrCarteira", "nrCarteira", "nrCarteira", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("cdBanco", "cdBanco", "cdBanco", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("cdAgencia", "cdAgencia", "cdAgencia", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("nuContaCorrente", "nuContaCorrente", "nuContaCorrente", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("vlDiasBoletoVencido", "vlDiasBoletoVencido", "vlDiasBoletoVencido", typeof(int)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("cdNSA", "cdNSA", "cdNSA", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("nuCNPJCedente", "nuCNPJCedente", "nuCNPJCedente", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("vlTaxaBancaria1", "vlTaxaBancaria1", "vlTaxaBancaria1", typeof(decimal)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("flAtivo1", "flAtivo1", "flAtivo1", typeof(bool))});
            this.DataSources.Add(this.boleto);
            this.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("cnnEventos", "cnnEventos", "Password=Qu454rFm;Persist Security Info=False;Data Source=dbsq0012.whservidor.com" +
";Integrated Security=False;Initial Catalog=fazendomai2;User ID=fazendomai2", false));
        }

        #region Relation ParentEvento
        public class ParentEventoRelation : Stimulsoft.Report.Dictionary.StiDataRow
        {

            public ParentEventoRelation(Stimulsoft.Report.Dictionary.StiDataRow dataRow)
                :
                    base(dataRow)
            {
            }

            public virtual string cdEvento
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["cdEvento"], typeof(string), true)));
                }
            }

            public virtual string cdCliente
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["cdCliente"], typeof(string), true)));
                }
            }

            public virtual string noEvento
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["noEvento"], typeof(string), true)));
                }
            }

            public virtual string dsEvento
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsEvento"], typeof(string), true)));
                }
            }

            public virtual DateTime dtInicioEvento
            {
                get
                {
                    return ((DateTime)(StiReport.ChangeType(this["dtInicioEvento"], typeof(DateTime), true)));
                }
            }

            public virtual DateTime dtFinalEvento
            {
                get
                {
                    return ((DateTime)(StiReport.ChangeType(this["dtFinalEvento"], typeof(DateTime), true)));
                }
            }

            public virtual bool flAtivo
            {
                get
                {
                    return ((bool)(StiReport.ChangeType(this["flAtivo"], typeof(bool), true)));
                }
            }

            public virtual bool flEventoComRecebimentos
            {
                get
                {
                    return ((bool)(StiReport.ChangeType(this["flEventoComRecebimentos"], typeof(bool), true)));
                }
            }
        }
        #endregion Relation ParentEvento

        #region Relation Parentboleto
        public class ParentboletoRelation : Stimulsoft.Report.Dictionary.StiDataRow
        {

            public ParentboletoRelation(Stimulsoft.Report.Dictionary.StiDataRow dataRow)
                :
                    base(dataRow)
            {
            }

            public virtual string cdBoleto
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["cdBoleto"], typeof(string), true)));
                }
            }

            public virtual string cdPedido
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["cdPedido"], typeof(string), true)));
                }
            }

            public virtual string cdEvento
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["cdEvento"], typeof(string), true)));
                }
            }

            public virtual DateTime dtEmissao
            {
                get
                {
                    return ((DateTime)(StiReport.ChangeType(this["dtEmissao"], typeof(DateTime), true)));
                }
            }

            public virtual DateTime dtVencimento
            {
                get
                {
                    return ((DateTime)(StiReport.ChangeType(this["dtVencimento"], typeof(DateTime), true)));
                }
            }

            public virtual decimal vlBoleto
            {
                get
                {
                    return ((decimal)(StiReport.ChangeType(this["vlBoleto"], typeof(decimal), true)));
                }
            }

            public virtual decimal vlTaxaBancaria
            {
                get
                {
                    return ((decimal)(StiReport.ChangeType(this["vlTaxaBancaria"], typeof(decimal), true)));
                }
            }

            public virtual bool flRecebido
            {
                get
                {
                    return ((bool)(StiReport.ChangeType(this["flRecebido"], typeof(bool), true)));
                }
            }

            public virtual DateTime dtPedido
            {
                get
                {
                    return ((DateTime)(StiReport.ChangeType(this["dtPedido"], typeof(DateTime), true)));
                }
            }

            public virtual string cdEvento1
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["cdEvento1"], typeof(string), true)));
                }
            }

            public virtual string cdParticipante
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["cdParticipante"], typeof(string), true)));
                }
            }

            public virtual string cdCredencial
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["cdCredencial"], typeof(string), true)));
                }
            }

            public virtual DateTime dtCredencial
            {
                get
                {
                    return ((DateTime)(StiReport.ChangeType(this["dtCredencial"], typeof(DateTime), true)));
                }
            }

            public virtual string cdCategoria
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["cdCategoria"], typeof(string), true)));
                }
            }

            public virtual string noParticipante
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["noParticipante"], typeof(string), true)));
                }
            }

            public virtual string noEtiqueta
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["noEtiqueta"], typeof(string), true)));
                }
            }

            public virtual string nuCPFCNPJ
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["nuCPFCNPJ"], typeof(string), true)));
                }
            }

            public virtual string dsDocIdentificador
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsDocIdentificador"], typeof(string), true)));
                }
            }

            public virtual string dsEmail
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsEmail"], typeof(string), true)));
                }
            }

            public virtual string nuCEP
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["nuCEP"], typeof(string), true)));
                }
            }

            public virtual string dsEndereco
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsEndereco"], typeof(string), true)));
                }
            }

            public virtual string noBairro
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["noBairro"], typeof(string), true)));
                }
            }

            public virtual string noCidade
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["noCidade"], typeof(string), true)));
                }
            }

            public virtual string dsUF
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsUF"], typeof(string), true)));
                }
            }

            public virtual string noPais
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["noPais"], typeof(string), true)));
                }
            }

            public virtual string dsFone1
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsFone1"], typeof(string), true)));
                }
            }

            public virtual string dsFone2
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsFone2"], typeof(string), true)));
                }
            }

            public virtual string dsFone3
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsFone3"], typeof(string), true)));
                }
            }

            public virtual string noInstituicao
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["noInstituicao"], typeof(string), true)));
                }
            }

            public virtual string noCargo
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["noCargo"], typeof(string), true)));
                }
            }

            public virtual string noAreaAtuacao
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["noAreaAtuacao"], typeof(string), true)));
                }
            }

            public virtual string dsSexo
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsSexo"], typeof(string), true)));
                }
            }

            public virtual string dsNascimento
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsNascimento"], typeof(string), true)));
                }
            }

            public virtual string dsObservacoes
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsObservacoes"], typeof(string), true)));
                }
            }

            public virtual string dsAuxiliar1
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsAuxiliar1"], typeof(string), true)));
                }
            }

            public virtual string dsAuxiliar2
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsAuxiliar2"], typeof(string), true)));
                }
            }

            public virtual string dsAuxiliar3
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsAuxiliar3"], typeof(string), true)));
                }
            }

            public virtual string dsAuxiliar4
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsAuxiliar4"], typeof(string), true)));
                }
            }

            public virtual string dsAuxiliar5
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsAuxiliar5"], typeof(string), true)));
                }
            }

            public virtual string dsAuxiliar6
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsAuxiliar6"], typeof(string), true)));
                }
            }

            public virtual string dsAuxiliar7
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsAuxiliar7"], typeof(string), true)));
                }
            }

            public virtual string dsAuxiliar8
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsAuxiliar8"], typeof(string), true)));
                }
            }

            public virtual string dsAuxiliar9
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsAuxiliar9"], typeof(string), true)));
                }
            }

            public virtual string dsAuxiliar10
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsAuxiliar10"], typeof(string), true)));
                }
            }

            public virtual string dsAuxiliar11
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsAuxiliar11"], typeof(string), true)));
                }
            }

            public virtual string dsAuxiliar12
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsAuxiliar12"], typeof(string), true)));
                }
            }

            public virtual string dsAuxiliar13
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsAuxiliar13"], typeof(string), true)));
                }
            }

            public virtual string dsAuxiliar14
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsAuxiliar14"], typeof(string), true)));
                }
            }

            public virtual string dsAuxiliar15
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsAuxiliar15"], typeof(string), true)));
                }
            }

            public virtual string dsAuxiliar16
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsAuxiliar16"], typeof(string), true)));
                }
            }

            public virtual string dsAuxiliar17
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsAuxiliar17"], typeof(string), true)));
                }
            }

            public virtual string dsAuxiliar18
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsAuxiliar18"], typeof(string), true)));
                }
            }

            public virtual string dsAuxiliar19
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsAuxiliar19"], typeof(string), true)));
                }
            }

            public virtual bool flAtivo
            {
                get
                {
                    return ((bool)(StiReport.ChangeType(this["flAtivo"], typeof(bool), true)));
                }
            }

            public virtual DateTime dtCadastro
            {
                get
                {
                    return ((DateTime)(StiReport.ChangeType(this["dtCadastro"], typeof(DateTime), true)));
                }
            }

            public virtual string dsSenhaWeb
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsSenhaWeb"], typeof(string), true)));
                }
            }

            public virtual bool flEmailValidado
            {
                get
                {
                    return ((bool)(StiReport.ChangeType(this["flEmailValidado"], typeof(bool), true)));
                }
            }

            public virtual string noCategoria
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["noCategoria"], typeof(string), true)));
                }
            }

            public virtual string cdEvento2
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["cdEvento2"], typeof(string), true)));
                }
            }

            public virtual string cdCarteira
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["cdCarteira"], typeof(string), true)));
                }
            }

            public virtual string noCedente
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["noCedente"], typeof(string), true)));
                }
            }

            public virtual string cdConvenio
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["cdConvenio"], typeof(string), true)));
                }
            }

            public virtual string cdCedente
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["cdCedente"], typeof(string), true)));
                }
            }

            public virtual string nrCarteira
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["nrCarteira"], typeof(string), true)));
                }
            }

            public virtual string cdBanco
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["cdBanco"], typeof(string), true)));
                }
            }

            public virtual string cdAgencia
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["cdAgencia"], typeof(string), true)));
                }
            }

            public virtual string nuContaCorrente
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["nuContaCorrente"], typeof(string), true)));
                }
            }

            public virtual int vlDiasBoletoVencido
            {
                get
                {
                    return ((int)(StiReport.ChangeType(this["vlDiasBoletoVencido"], typeof(int), true)));
                }
            }

            public virtual string cdNSA
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["cdNSA"], typeof(string), true)));
                }
            }

            public virtual string nuCNPJCedente
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["nuCNPJCedente"], typeof(string), true)));
                }
            }

            public virtual decimal vlTaxaBancaria1
            {
                get
                {
                    return ((decimal)(StiReport.ChangeType(this["vlTaxaBancaria1"], typeof(decimal), true)));
                }
            }

            public virtual bool flAtivo1
            {
                get
                {
                    return ((bool)(StiReport.ChangeType(this["flAtivo1"], typeof(bool), true)));
                }
            }
        }
        #endregion Relation Parentboleto

        #region DataSource Evento
        public class EventoDataSource : Stimulsoft.Report.Dictionary.StiDataTableSource
        {

            public EventoDataSource()
                :
                    base("DSBloetosWeb.Evento", "Evento")
            {
            }

            public virtual string cdEvento
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["cdEvento"], typeof(string), true)));
                }
            }

            public virtual string cdCliente
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["cdCliente"], typeof(string), true)));
                }
            }

            public virtual string noEvento
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["noEvento"], typeof(string), true)));
                }
            }

            public virtual string dsEvento
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsEvento"], typeof(string), true)));
                }
            }

            public virtual DateTime dtInicioEvento
            {
                get
                {
                    return ((DateTime)(StiReport.ChangeType(this["dtInicioEvento"], typeof(DateTime), true)));
                }
            }

            public virtual DateTime dtFinalEvento
            {
                get
                {
                    return ((DateTime)(StiReport.ChangeType(this["dtFinalEvento"], typeof(DateTime), true)));
                }
            }

            public virtual bool flAtivo
            {
                get
                {
                    return ((bool)(StiReport.ChangeType(this["flAtivo"], typeof(bool), true)));
                }
            }

            public virtual bool flEventoComRecebimentos
            {
                get
                {
                    return ((bool)(StiReport.ChangeType(this["flEventoComRecebimentos"], typeof(bool), true)));
                }
            }
        }
        #endregion DataSource Evento

        #region DataSource EventosImgs
        public class EventosImgsDataSource : Stimulsoft.Report.Dictionary.StiDataTableSource
        {

            public EventosImgsDataSource()
                :
                    base("DSBloetosWeb.EventosImgs", "EventosImgs")
            {
            }

            public virtual string cdEvento
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["cdEvento"], typeof(string), true)));
                }
            }

            public virtual byte[] imgBanner
            {
                get
                {
                    return ((byte[])(StiReport.ChangeType(this["imgBanner"], typeof(byte[]), true)));
                }
            }

            public virtual byte[] imgLogo
            {
                get
                {
                    return ((byte[])(StiReport.ChangeType(this["imgLogo"], typeof(byte[]), true)));
                }
            }

            public virtual ParentEventoRelation Evento
            {
                get
                {
                    return new ParentEventoRelation(this.GetParentData("Relationship67"));
                }
            }
        }
        #endregion DataSource EventosImgs

        #region DataSource DataHora
        public class DataHoraDataSource : Stimulsoft.Report.Dictionary.StiDataTableSource
        {

            public DataHoraDataSource()
                :
                    base("DSBloetosWeb.DataHora", "DataHora")
            {
            }

            public virtual DateTime DataHora
            {
                get
                {
                    return ((DateTime)(StiReport.ChangeType(this["DataHora"], typeof(DateTime), true)));
                }
            }
        }
        #endregion DataSource DataHora

        #region DataSource AtividadeBoleto
        public class AtividadeBoletoDataSource : Stimulsoft.Report.Dictionary.StiDataTableSource
        {

            public AtividadeBoletoDataSource()
                :
                    base("DSBloetosWeb.AtividadeBoleto", "AtividadeBoleto")
            {
            }

            public virtual string cdPedido
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["cdPedido"], typeof(string), true)));
                }
            }

            public virtual decimal vlUnitario
            {
                get
                {
                    return ((decimal)(StiReport.ChangeType(this["vlUnitario"], typeof(decimal), true)));
                }
            }

            public virtual decimal vlUnitarioDesconto
            {
                get
                {
                    return ((decimal)(StiReport.ChangeType(this["vlUnitarioDesconto"], typeof(decimal), true)));
                }
            }

            public virtual string cdAtividade
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["cdAtividade"], typeof(string), true)));
                }
            }

            public virtual string cdEvento
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["cdEvento"], typeof(string), true)));
                }
            }

            public virtual string cdTipoAtividade
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["cdTipoAtividade"], typeof(string), true)));
                }
            }

            public virtual string noTitulo
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["noTitulo"], typeof(string), true)));
                }
            }

            public virtual string noSubTitulo
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["noSubTitulo"], typeof(string), true)));
                }
            }

            public virtual string dsTema
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsTema"], typeof(string), true)));
                }
            }

            public virtual string tpControle
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["tpControle"], typeof(string), true)));
                }
            }

            public virtual bool flCertificacao
            {
                get
                {
                    return ((bool)(StiReport.ChangeType(this["flCertificacao"], typeof(bool), true)));
                }
            }

            public virtual decimal vlAtividade
            {
                get
                {
                    return ((decimal)(StiReport.ChangeType(this["vlAtividade"], typeof(decimal), true)));
                }
            }

            public virtual DateTime dtInicio
            {
                get
                {
                    return ((DateTime)(StiReport.ChangeType(this["dtInicio"], typeof(DateTime), true)));
                }
            }

            public virtual DateTime dtTermino
            {
                get
                {
                    return ((DateTime)(StiReport.ChangeType(this["dtTermino"], typeof(DateTime), true)));
                }
            }

            public virtual decimal vlCargaHoraria
            {
                get
                {
                    return ((decimal)(StiReport.ChangeType(this["vlCargaHoraria"], typeof(decimal), true)));
                }
            }

            public virtual bool flAtivo
            {
                get
                {
                    return ((bool)(StiReport.ChangeType(this["flAtivo"], typeof(bool), true)));
                }
            }

            public virtual bool flPagamento
            {
                get
                {
                    return ((bool)(StiReport.ChangeType(this["flPagamento"], typeof(bool), true)));
                }
            }

            public virtual bool flVisivel_Inscricao
            {
                get
                {
                    return ((bool)(StiReport.ChangeType(this["flVisivel_Inscricao"], typeof(bool), true)));
                }
            }

            public virtual string cdLocal
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["cdLocal"], typeof(string), true)));
                }
            }

            public virtual ParentboletoRelation boleto
            {
                get
                {
                    return new ParentboletoRelation(this.GetParentData("boleto_AtividadeBoleto"));
                }
            }
        }
        #endregion DataSource AtividadeBoleto

        #region DataSource boleto
        public class boletoDataSource : Stimulsoft.Report.Dictionary.StiDataTableSource
        {

            public boletoDataSource()
                :
                    base("DSBloetosWeb.boleto", "boleto")
            {
            }

            public virtual string cdBoleto
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["cdBoleto"], typeof(string), true)));
                }
            }

            public virtual string cdPedido
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["cdPedido"], typeof(string), true)));
                }
            }

            public virtual string cdEvento
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["cdEvento"], typeof(string), true)));
                }
            }

            public virtual DateTime dtEmissao
            {
                get
                {
                    return ((DateTime)(StiReport.ChangeType(this["dtEmissao"], typeof(DateTime), true)));
                }
            }

            public virtual DateTime dtVencimento
            {
                get
                {
                    return ((DateTime)(StiReport.ChangeType(this["dtVencimento"], typeof(DateTime), true)));
                }
            }

            public virtual decimal vlBoleto
            {
                get
                {
                    return ((decimal)(StiReport.ChangeType(this["vlBoleto"], typeof(decimal), true)));
                }
            }

            public virtual decimal vlTaxaBancaria
            {
                get
                {
                    return ((decimal)(StiReport.ChangeType(this["vlTaxaBancaria"], typeof(decimal), true)));
                }
            }

            public virtual bool flRecebido
            {
                get
                {
                    return ((bool)(StiReport.ChangeType(this["flRecebido"], typeof(bool), true)));
                }
            }

            public virtual DateTime dtPedido
            {
                get
                {
                    return ((DateTime)(StiReport.ChangeType(this["dtPedido"], typeof(DateTime), true)));
                }
            }

            public virtual string cdEvento1
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["cdEvento1"], typeof(string), true)));
                }
            }

            public virtual string cdParticipante
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["cdParticipante"], typeof(string), true)));
                }
            }

            public virtual string cdCredencial
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["cdCredencial"], typeof(string), true)));
                }
            }

            public virtual DateTime dtCredencial
            {
                get
                {
                    return ((DateTime)(StiReport.ChangeType(this["dtCredencial"], typeof(DateTime), true)));
                }
            }

            public virtual string cdCategoria
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["cdCategoria"], typeof(string), true)));
                }
            }

            public virtual string noParticipante
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["noParticipante"], typeof(string), true)));
                }
            }

            public virtual string noEtiqueta
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["noEtiqueta"], typeof(string), true)));
                }
            }

            public virtual string nuCPFCNPJ
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["nuCPFCNPJ"], typeof(string), true)));
                }
            }

            public virtual string dsDocIdentificador
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsDocIdentificador"], typeof(string), true)));
                }
            }

            public virtual string dsEmail
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsEmail"], typeof(string), true)));
                }
            }

            public virtual string nuCEP
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["nuCEP"], typeof(string), true)));
                }
            }

            public virtual string dsEndereco
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsEndereco"], typeof(string), true)));
                }
            }

            public virtual string noBairro
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["noBairro"], typeof(string), true)));
                }
            }

            public virtual string noCidade
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["noCidade"], typeof(string), true)));
                }
            }

            public virtual string dsUF
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsUF"], typeof(string), true)));
                }
            }

            public virtual string noPais
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["noPais"], typeof(string), true)));
                }
            }

            public virtual string dsFone1
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsFone1"], typeof(string), true)));
                }
            }

            public virtual string dsFone2
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsFone2"], typeof(string), true)));
                }
            }

            public virtual string dsFone3
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsFone3"], typeof(string), true)));
                }
            }

            public virtual string noInstituicao
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["noInstituicao"], typeof(string), true)));
                }
            }

            public virtual string noCargo
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["noCargo"], typeof(string), true)));
                }
            }

            public virtual string noAreaAtuacao
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["noAreaAtuacao"], typeof(string), true)));
                }
            }

            public virtual string dsSexo
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsSexo"], typeof(string), true)));
                }
            }

            public virtual string dsNascimento
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsNascimento"], typeof(string), true)));
                }
            }

            public virtual string dsObservacoes
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsObservacoes"], typeof(string), true)));
                }
            }

            public virtual string dsAuxiliar1
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsAuxiliar1"], typeof(string), true)));
                }
            }

            public virtual string dsAuxiliar2
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsAuxiliar2"], typeof(string), true)));
                }
            }

            public virtual string dsAuxiliar3
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsAuxiliar3"], typeof(string), true)));
                }
            }

            public virtual string dsAuxiliar4
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsAuxiliar4"], typeof(string), true)));
                }
            }

            public virtual string dsAuxiliar5
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsAuxiliar5"], typeof(string), true)));
                }
            }

            public virtual string dsAuxiliar6
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsAuxiliar6"], typeof(string), true)));
                }
            }

            public virtual string dsAuxiliar7
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsAuxiliar7"], typeof(string), true)));
                }
            }

            public virtual string dsAuxiliar8
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsAuxiliar8"], typeof(string), true)));
                }
            }

            public virtual string dsAuxiliar9
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsAuxiliar9"], typeof(string), true)));
                }
            }

            public virtual string dsAuxiliar10
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsAuxiliar10"], typeof(string), true)));
                }
            }

            public virtual string dsAuxiliar11
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsAuxiliar11"], typeof(string), true)));
                }
            }

            public virtual string dsAuxiliar12
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsAuxiliar12"], typeof(string), true)));
                }
            }

            public virtual string dsAuxiliar13
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsAuxiliar13"], typeof(string), true)));
                }
            }

            public virtual string dsAuxiliar14
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsAuxiliar14"], typeof(string), true)));
                }
            }

            public virtual string dsAuxiliar15
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsAuxiliar15"], typeof(string), true)));
                }
            }

            public virtual string dsAuxiliar16
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsAuxiliar16"], typeof(string), true)));
                }
            }

            public virtual string dsAuxiliar17
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsAuxiliar17"], typeof(string), true)));
                }
            }

            public virtual string dsAuxiliar18
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsAuxiliar18"], typeof(string), true)));
                }
            }

            public virtual string dsAuxiliar19
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsAuxiliar19"], typeof(string), true)));
                }
            }

            public virtual bool flAtivo
            {
                get
                {
                    return ((bool)(StiReport.ChangeType(this["flAtivo"], typeof(bool), true)));
                }
            }

            public virtual DateTime dtCadastro
            {
                get
                {
                    return ((DateTime)(StiReport.ChangeType(this["dtCadastro"], typeof(DateTime), true)));
                }
            }

            public virtual string dsSenhaWeb
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["dsSenhaWeb"], typeof(string), true)));
                }
            }

            public virtual bool flEmailValidado
            {
                get
                {
                    return ((bool)(StiReport.ChangeType(this["flEmailValidado"], typeof(bool), true)));
                }
            }

            public virtual string noCategoria
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["noCategoria"], typeof(string), true)));
                }
            }

            public virtual string cdEvento2
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["cdEvento2"], typeof(string), true)));
                }
            }

            public virtual string cdCarteira
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["cdCarteira"], typeof(string), true)));
                }
            }

            public virtual string noCedente
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["noCedente"], typeof(string), true)));
                }
            }

            public virtual string cdConvenio
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["cdConvenio"], typeof(string), true)));
                }
            }

            public virtual string cdCedente
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["cdCedente"], typeof(string), true)));
                }
            }

            public virtual string nrCarteira
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["nrCarteira"], typeof(string), true)));
                }
            }

            public virtual string cdBanco
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["cdBanco"], typeof(string), true)));
                }
            }

            public virtual string cdAgencia
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["cdAgencia"], typeof(string), true)));
                }
            }

            public virtual string nuContaCorrente
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["nuContaCorrente"], typeof(string), true)));
                }
            }

            public virtual int vlDiasBoletoVencido
            {
                get
                {
                    return ((int)(StiReport.ChangeType(this["vlDiasBoletoVencido"], typeof(int), true)));
                }
            }

            public virtual string cdNSA
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["cdNSA"], typeof(string), true)));
                }
            }

            public virtual string nuCNPJCedente
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["nuCNPJCedente"], typeof(string), true)));
                }
            }

            public virtual decimal vlTaxaBancaria1
            {
                get
                {
                    return ((decimal)(StiReport.ChangeType(this["vlTaxaBancaria1"], typeof(decimal), true)));
                }
            }

            public virtual bool flAtivo1
            {
                get
                {
                    return ((bool)(StiReport.ChangeType(this["flAtivo1"], typeof(bool), true)));
                }
            }
        }
        #endregion DataSource boleto
        #endregion StiReport Designer generated code - do not modify
    }
}