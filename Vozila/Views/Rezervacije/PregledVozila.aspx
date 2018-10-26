<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="System.Web.Mvc.ViewPage<IEnumerable<Vozila.Models.vRezervacija>>" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Drawing" %>
<%@ Import Namespace="Vozila.Controllers" %>
<%@ Import Namespace="Vozila.Models" %>
<%@ Register assembly="DevExpress.XtraCharts.v11.2.Web, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts.Web" tagprefix="dxchartsui" %>
<%@ Register assembly="DevExpress.XtraCharts.v11.2, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="cc1" %>
<script runat="server">
    private void CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
    {
        e.LegendText = (string)"-";
    }
</script>




<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <form id="form1" runat="server">
    Pregled Vozila</form>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%
       
        Html.BeginForm();
        
        //ChartViewTypeDemoOptions options = (ChartViewTypeDemoOptions)Model; 
         
        ChartControlSettings settings = new ChartControlSettings();
        settings.Name = "chart";
        settings.BorderOptions.Visible = false;
        settings.Height = 600;
        settings.Width = 700;
       
        settings.SeriesTemplate.ChangeView(DevExpress.XtraCharts.ViewType.RangeArea);

        settings.Series.Add("Odobreno", DevExpress.XtraCharts.ViewType.RangeBar);
        settings.Series[0].Label.Visible = false;
        settings.Series[0].ShowInLegend = true;
        settings.Series[0].DataSource = Model;
        settings.Series[0].ArgumentDataMember = "automobil";
        settings.Series[0] .ValueScaleType = ScaleType.DateTime;
        settings.Series[0].ValueDataMembers[0] = "datum_polaska";
        settings.Series[0].ValueDataMembers[1] = "datum_dolaska";
        settings.Series[0].Tag = "automobil";
        
        settings.Series.Add("Nezakljucene", DevExpress.XtraCharts.ViewType.RangeBar);
        settings.Series[1].Label.Visible = false;
        settings.Series[1].ShowInLegend = true;
        settings.Series[1].DataSource = Model;
        settings.Series[1].ArgumentDataMember = "automobil";
        settings.Series[1].ValueScaleType = ScaleType.DateTime;
        settings.Series[1].ValueDataMembers[0] = "datum_polaska";
        settings.Series[1].ValueDataMembers[1] = "datum_dolaska";
        settings.Series[1].Tag = "automobil";
            
        settings.Series.Add("Zakljucene", DevExpress.XtraCharts.ViewType.RangeBar);
        settings.Series[2].Label.Visible = false;
        settings.Series[2].ShowInLegend = true;
        settings.Series[2].DataSource = Model;
        settings.Series[2].ArgumentDataMember = "automobil";
        settings.Series[2].ValueScaleType = ScaleType.DateTime;
        settings.Series[2].ValueDataMembers[0] = "datum_polaska";
        settings.Series[2].ValueDataMembers[1] = "datum_dolaska";
         settings.Series[2].Tag = "automobil";
            
        settings.Series[0].DataFilters.ClearAndAddRange(new[] {
            new DataFilter("Status", "System.Int", DataFilterCondition.Equal, 1)});
            
        settings.Series[1].DataFilters.ClearAndAddRange(new[] {
            new DataFilter("Status", "System.Int", DataFilterCondition.Equal, 3)});
       
            settings.Series[2].DataFilters.ClearAndAddRange(new[] {
            new DataFilter("Status", "System.Int", DataFilterCondition.Equal, 4)});
            
        XYDiagram xyDiagram1 = new XYDiagram {Rotated = true, EnableAxisXScrolling = true, EnableAxisYScrolling = true};

        xyDiagram1.AxisX.Color = Color.FromArgb(255, 255, 255);
        xyDiagram1.AxisX.GridLines.MinorVisible = true;
        xyDiagram1.AxisX.MinorCount = 1;
        xyDiagram1.AxisX.Range.ScrollingRange.Auto = false;
        xyDiagram1.AxisX.Range.ScrollingRange.MaxValueSerializable = "1";
        xyDiagram1.AxisX.Range.ScrollingRange.MinValueSerializable = "1";
        xyDiagram1.AxisX.Range.ScrollingRange.SideMarginsEnabled = true;
        xyDiagram1.AxisX.Range.SideMarginsEnabled = true;
        xyDiagram1.AxisX.Tickmarks.MinorVisible = false;
        xyDiagram1.AxisX.Title.Alignment = StringAlignment.Far;
        xyDiagram1.AxisX.Title.Font = new Font("Tahoma", 10F);
        xyDiagram1.AxisX.Title.Text = "Automobil";
        xyDiagram1.AxisX.Title.Visible = true;
        xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
        
        xyDiagram1.AxisY.DateTimeMeasureUnit = DateTimeMeasurementUnit.Hour;
        //xyDiagram1.AxisY.DateTimeOptions.Format = DateTimeFormat.ShortDate;
        xyDiagram1.AxisY.GridLines.MinorVisible = true;
        xyDiagram1.AxisY.Label.Angle = 45;
        xyDiagram1.AxisY.Label.Font = new Font("Tahoma", 10F, FontStyle.Regular, GraphicsUnit.Point, 238);
        xyDiagram1.AxisY.MinorCount = 3;
        xyDiagram1.AxisY.Range.ScrollingRange.SideMarginsEnabled = true;
        xyDiagram1.AxisY.Range.SideMarginsEnabled = true;

        xyDiagram1.AxisY.Title.Alignment = StringAlignment.Near;
        xyDiagram1.AxisY.Title.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
        xyDiagram1.AxisY.Title.Text = "Datum";
        xyDiagram1.AxisY.Title.Visible = true;
        xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
        
        
        settings.Diagram.Assign(xyDiagram1);

        settings.EnableClientSideAPI = true;
        settings.CallbackRouteValues = new { Controller = "Rezervacije", Action = "Details" };
        settings.ClientSideEvents.ObjectHotTracked = "function (s, e) { chart.SetCursor(e.hitInfo.inSeries ? 'pointer' : 'default'); }";
        settings.ClientSideEvents.ObjectSelected = "function (s, e) {var point = e.AdditionalObject ;" +
                                                 " if(e.hitInfo.inSeriesPoint) { document.location = '"
           + DevExpressHelper.GetUrl(new { Controller = "Rezervacije", Action = "Details" }) + "/id=' + e.parameter; } }";

        //axisX.DateTimeOptions.FormatString = "MMMM";
        //axisY.Range.AlwaysShowZeroLevel = false;
        //axisY.Range.SideMarginsEnabled = true;
        //axisY.Interlaced = true;
        //axisY.NumericOptions.Format = NumericFormat.Number;
        //axisY.NumericOptions.Precision = 0;
       // settings.EnableClientSideAPI = true;
       // settings.CustomDrawSeriesPoint = CustomDrawSeriesPoint;
        settings.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
        settings.Legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;
        settings.Legend.Direction = LegendDirection.LeftToRight;
        settings.Titles.Add(new ChartTitle() {
            Font = new Font("Tahoma", 18),
            Text = "Zauzetost vozila"
        });
        
        Html.DevExpress().Chart(
            settings
            
            ).Bind(Model)
        .Render();
        Html.EndForm();       
    %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Links" runat="server">
      <%: Html.ActionLink("Natrag", "Index") %>
</asp:Content>
