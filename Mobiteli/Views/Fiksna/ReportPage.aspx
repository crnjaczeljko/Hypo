<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Mobiteli.Models.vPotrosnjaFiksnoMjesec>>" %>
<%@ Import Namespace="Mobiteli" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" 
namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        var reportDataSource = new ReportDataSource { Name = "DataSet1", Value = Model};
        ReportViewer1.LocalReport.DataSources.Add(reportDataSource);
    }
    
</script>
<html>
<head runat="server">
    <title>ReportPage</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: auto; text-align: center; height: auto;">
       &nbsp;<asp:ScriptManager id='ScriptManager' runat='server' /> 

    <rsweb:ReportViewer ID="ReportViewer1" runat="server" AsyncRendering="False" 
        Font-Names="Verdana" Font-Size="8pt" Height="" 
        InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" 
        WaitMessageFont-Size="14pt" Width="705px" BorderStyle="Groove" 
            ShowFindControls="False" ShowParameterPrompts="False" ShowRefreshButton="False" 
            style="margin-left: 72px; margin-right: 0px">
        <LocalReport ReportPath="Rpt\potrosnjaFiksno.rdlc">
        </LocalReport>
    </rsweb:ReportViewer>    
    </div>
 
    </form>
</body>
</html>
