<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="rptEtiqueta.aspx.cs" Inherits="rptEtiqueta"%>

<%@ Register Assembly="Stimulsoft.Report.Web" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Fazendo Mais - Inscri&ccedil;&otilde;es Web - Administrativo</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/img/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/img/favicon.ico" type="image/ico" />
    <%--<link href="css/StyleSheet.css" rel="stylesheet" type="text/css" />--%>
    <link href="~/css/cssFmais.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="scripts/JSCFuncoes.js"></script>
    <style type="text/css">
        .style1
        {
            height: 46px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>

        <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Width="623px" 
            ShowExportToBmp="False" ShowExportToCsv="False" ShowExportToDbf="False" 
            ShowExportToExcel="False" ShowExportToExcel2007="False" 
            ShowExportToExcelXml="False" ShowExportToGif="False" ShowExportToHtml="False" 
            ShowExportToJpeg="False" ShowExportToMetafile="False" ShowExportToMht="False" 
            ShowExportToOds="False" ShowExportToOdt="False" ShowExportToPcx="False" 
            ShowExportToPng="False" ShowExportToPowerPoint="False" ShowExportToRtf="False" 
            ShowExportToText="False" ShowExportToTiff="False" ShowExportToWord2007="False" 
            ShowExportToXml="False" ShowExportToXps="False" ShowPrintButton="True" 
            ShowViewMode="False" ShowZoom="False"/>
        &nbsp;</div>
</form>
</body>
</html>
