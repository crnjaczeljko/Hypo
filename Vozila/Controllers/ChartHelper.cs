using System.Collections.Generic;
using System.Web.Mvc;
using DevExpress.XtraCharts;

namespace Vozila.Controllers
{
    public class ChartHelper
    {
    }
    public class ChartViewTypeDemoOptions
    {
        DevExpress.XtraCharts.ViewType view;

        public DevExpress.XtraCharts.ViewType View
        {
            get { return view; }
            set { view = value; }
        }

        public ChartViewTypeDemoOptions()
        {
        }
    }
    public static class ChartDemoHelper
    {
        public const string OptionsKey = "Options";
        public const string CategoryKey = "Category";
        public const string StarKey = "star:";
        public const string CompletedDateKey = "CompletedDate";
        public const string ModelKey = "Model";

        public static bool IsSideBySideStackedView(DevExpress.XtraCharts.ViewType view)
        {
            return view == DevExpress.XtraCharts.ViewType.SideBySideStackedBar
                || view == DevExpress.XtraCharts.ViewType.SideBySideStackedBar3D
                || view == DevExpress.XtraCharts.ViewType.SideBySideFullStackedBar
                || view == DevExpress.XtraCharts.ViewType.SideBySideFullStackedBar3D;
        }
        public static bool IsPolarView(DevExpress.XtraCharts.ViewType view)
        {
            return view == DevExpress.XtraCharts.ViewType.PolarArea
                || view == DevExpress.XtraCharts.ViewType.PolarLine
                || view == DevExpress.XtraCharts.ViewType.PolarPoint;
        }
        public static List<SelectListItem> GetBarViews()
        {
            return new List<SelectListItem>() {
                new SelectListItem() { Text = "Bar", Value = DevExpress.XtraCharts.ViewType.Bar.ToString() },
                new SelectListItem() { Text = "Stacked Bar", Value = DevExpress.XtraCharts.ViewType.StackedBar.ToString() },
                new SelectListItem() { Text = "Full-Stacked Bar", Value = DevExpress.XtraCharts.ViewType.FullStackedBar.ToString() },
                new SelectListItem() { Text = "Side-by-Side Stacked Bar", Value = DevExpress.XtraCharts.ViewType.SideBySideStackedBar.ToString() },
                new SelectListItem() { Text = "Side-by-Side Full-Stacked Bar", Value = DevExpress.XtraCharts.ViewType.SideBySideFullStackedBar.ToString() },
                new SelectListItem() { Text = "3D Bar", Value = DevExpress.XtraCharts.ViewType.Bar3D.ToString() },
                new SelectListItem() { Text = "3D Manhattan Bar", Value = DevExpress.XtraCharts.ViewType.ManhattanBar.ToString() },
                new SelectListItem() { Text = "3D Stacked Bar", Value = DevExpress.XtraCharts.ViewType.StackedBar3D.ToString() },
                new SelectListItem() { Text = "3D Full-Stacked Bar", Value = DevExpress.XtraCharts.ViewType.FullStackedBar3D.ToString() },
                new SelectListItem() { Text = "3D Side-by-Side Stacked Bar", Value = DevExpress.XtraCharts.ViewType.SideBySideStackedBar3D.ToString() },
                new SelectListItem() { Text = "3D Side-by-Side Full-Stacked Bar", Value = DevExpress.XtraCharts.ViewType.SideBySideFullStackedBar3D.ToString() }
            };
        }
        public static List<SelectListItem> GetPointLineViews()
        {
            return new List<SelectListItem>() {
                new SelectListItem() { Text = "Point", Value = DevExpress.XtraCharts.ViewType.Point.ToString() },
                new SelectListItem() { Text = "Bubble", Value = DevExpress.XtraCharts.ViewType.Bubble.ToString() },
                new SelectListItem() { Text = "Line", Value = DevExpress.XtraCharts.ViewType.Line.ToString() },
                new SelectListItem() { Text = "Scatter Line", Value = DevExpress.XtraCharts.ViewType.ScatterLine.ToString() },
                new SelectListItem() { Text = "Step Line", Value = DevExpress.XtraCharts.ViewType.StepLine.ToString() },
                new SelectListItem() { Text = "Stacked Line", Value = DevExpress.XtraCharts.ViewType.StackedLine.ToString() },
                new SelectListItem() { Text = "Full-Stacked Line", Value = DevExpress.XtraCharts.ViewType.FullStackedLine.ToString() },
                new SelectListItem() { Text = "Spline", Value = DevExpress.XtraCharts.ViewType.Spline.ToString() },
                new SelectListItem() { Text = "3D Line", Value = DevExpress.XtraCharts.ViewType.Line3D.ToString() },
                new SelectListItem() { Text = "3D Step Line", Value = DevExpress.XtraCharts.ViewType.StepLine3D.ToString() },
                new SelectListItem() { Text = "3D Stacked Line", Value = DevExpress.XtraCharts.ViewType.StackedLine3D.ToString() },
                new SelectListItem() { Text = "3D Full-Stacked Line", Value = DevExpress.XtraCharts.ViewType.FullStackedLine3D.ToString() },
                new SelectListItem() { Text = "3D Spline", Value = DevExpress.XtraCharts.ViewType.Spline3D.ToString() }
            };
        }
        public static List<SelectListItem> GetAreaViews()
        {
            return new List<SelectListItem>() {
                new SelectListItem() { Text = "Area", Value = DevExpress.XtraCharts.ViewType.Area.ToString() },
                new SelectListItem() { Text = "Stacked Area", Value = DevExpress.XtraCharts.ViewType.StackedArea.ToString() },
                new SelectListItem() { Text = "Full-Stacked Area", Value = DevExpress.XtraCharts.ViewType.FullStackedArea.ToString() },
                new SelectListItem() { Text = "Spline Area", Value = DevExpress.XtraCharts.ViewType.SplineArea.ToString() },
                new SelectListItem() { Text = "Stacked Spline Area", Value = DevExpress.XtraCharts.ViewType.StackedSplineArea.ToString() },
                new SelectListItem() { Text = "Full-Stacked Spline Area", Value = DevExpress.XtraCharts.ViewType.FullStackedSplineArea.ToString() },
                new SelectListItem() { Text = "Step Area", Value = DevExpress.XtraCharts.ViewType.StepArea.ToString() },
                new SelectListItem() { Text = "3D Area", Value = DevExpress.XtraCharts.ViewType.Area3D.ToString() },
                new SelectListItem() { Text = "3D Stacked Area", Value = DevExpress.XtraCharts.ViewType.StackedArea3D.ToString() },
                new SelectListItem() { Text = "3D Full-Stacked Area", Value = DevExpress.XtraCharts.ViewType.FullStackedArea3D.ToString() },
                new SelectListItem() { Text = "3D Spline Area", Value = DevExpress.XtraCharts.ViewType.SplineArea3D.ToString() },
                new SelectListItem() { Text = "3D Stacked Spline Area", Value = DevExpress.XtraCharts.ViewType.StackedSplineArea3D.ToString() },
                new SelectListItem() { Text = "3D Full-Stacked Spline Area", Value = DevExpress.XtraCharts.ViewType.FullStackedSplineArea3D.ToString() },
                new SelectListItem() { Text = "3D Step Area", Value = DevExpress.XtraCharts.ViewType.StepArea3D.ToString() },
            };
        }
        public static List<SelectListItem> GetPieViews()
        {
            return new List<SelectListItem>() {
                new SelectListItem() { Text = "Pie", Value = DevExpress.XtraCharts.ViewType.Pie.ToString() },
                new SelectListItem() { Text = "Doughnut", Value = DevExpress.XtraCharts.ViewType.Doughnut.ToString() },
                new SelectListItem() { Text = "3D Pie", Value = DevExpress.XtraCharts.ViewType.Pie3D.ToString() },
                new SelectListItem() { Text = "3D Doughnut", Value = DevExpress.XtraCharts.ViewType.Doughnut3D.ToString() },
            };
        }
        public static List<SelectListItem> GetPieLabelPositions()
        {
            return new List<SelectListItem>() {
                new SelectListItem() { Text = "Radial", Value = PieSeriesLabelPosition.Radial.ToString() },
                new SelectListItem() { Text = "Inside", Value = PieSeriesLabelPosition.Inside.ToString() },
                new SelectListItem() { Text = "Outside", Value = PieSeriesLabelPosition.Outside.ToString() },
                new SelectListItem() { Text = "Two Columns", Value = PieSeriesLabelPosition.TwoColumns.ToString() },
            };
        }

        public static List<SelectListItem> GetFunnelViews()
        {
            return new List<SelectListItem>() {
                new SelectListItem() { Text = "Funnel", Value = DevExpress.XtraCharts.ViewType.Funnel.ToString() },
                new SelectListItem() { Text = "3D Funnel", Value = DevExpress.XtraCharts.ViewType.Funnel3D.ToString() },
            };
        }
        public static List<SelectListItem> GetFunnelLabelPositions()
        {
            return new List<SelectListItem>() {
                new SelectListItem() { Text = "Right", Value = FunnelSeriesLabelPosition.Right.ToString() },
                new SelectListItem() { Text = "Left", Value = FunnelSeriesLabelPosition.Left.ToString() },
                new SelectListItem() { Text = "Center", Value = FunnelSeriesLabelPosition.Center.ToString() },
                new SelectListItem() { Text = "Right Column", Value = FunnelSeriesLabelPosition.RightColumn.ToString() },
                new SelectListItem() { Text = "Left Column", Value = FunnelSeriesLabelPosition.LeftColumn.ToString() },
            };
        }
        public static List<SelectListItem> GetRadarPolarViews()
        {
            return new List<SelectListItem>() {
                new SelectListItem() { Text = "Radar Point", Value = DevExpress.XtraCharts.ViewType.RadarPoint.ToString() },
                new SelectListItem() { Text = "Radar Line", Value = DevExpress.XtraCharts.ViewType.RadarLine.ToString() },
                new SelectListItem() { Text = "Radar Area", Value = DevExpress.XtraCharts.ViewType.RadarArea.ToString() },
                new SelectListItem() { Text = "Polar Point", Value = DevExpress.XtraCharts.ViewType.PolarPoint.ToString() },
                new SelectListItem() { Text = "Polar Line", Value = DevExpress.XtraCharts.ViewType.PolarLine.ToString() },
                new SelectListItem() { Text = "Polar Area", Value = DevExpress.XtraCharts.ViewType.PolarArea.ToString() }
            };
        }
        public static List<SelectListItem> GetMarkerKinds()
        {
            return new List<SelectListItem>() {
                new SelectListItem() { Text = "Circle", Value = DevExpress.XtraCharts.MarkerKind.Circle.ToString() },
                new SelectListItem() { Text = "Cross", Value = DevExpress.XtraCharts.MarkerKind.Cross.ToString() },
                new SelectListItem() { Text = "Diamond", Value = DevExpress.XtraCharts.MarkerKind.Diamond.ToString() },
                new SelectListItem() { Text = "Hexagon", Value = DevExpress.XtraCharts.MarkerKind.Hexagon.ToString() },
                new SelectListItem() { Text = "Inverted Triangle", Value = DevExpress.XtraCharts.MarkerKind.InvertedTriangle.ToString() },
                new SelectListItem() { Text = "Pentagon", Value = DevExpress.XtraCharts.MarkerKind.Pentagon.ToString() },
                new SelectListItem() { Text = "Plus", Value = DevExpress.XtraCharts.MarkerKind.Plus.ToString() },
                new SelectListItem() { Text = "Square", Value = DevExpress.XtraCharts.MarkerKind.Square.ToString() },
                new SelectListItem() { Text = "Triangle", Value = DevExpress.XtraCharts.MarkerKind.Triangle.ToString() },
                new SelectListItem() { Text = "Star 3-points", Value = StarKey + "3" },
                new SelectListItem() { Text = "Star 4-points", Value = StarKey + "4" },
                new SelectListItem() { Text = "Star 5-points", Value = StarKey + "5" },
                new SelectListItem() { Text = "Star 6-points", Value = StarKey + "6" },
                new SelectListItem() { Text = "Star 10-points", Value = StarKey + "10" }
            };
        }
        public static List<SelectListItem> GetRangeViews()
        {
            return new List<SelectListItem>(){
                new SelectListItem() { Text = "Range Bar", Value = DevExpress.XtraCharts.ViewType.RangeBar.ToString() },
                new SelectListItem() { Text = "Side-by-Side Range Bar", Value = DevExpress.XtraCharts.ViewType.SideBySideRangeBar.ToString() },
                new SelectListItem() { Text = "Range Area", Value = DevExpress.XtraCharts.ViewType.RangeArea.ToString() },
                new SelectListItem() { Text = "3D Range Area", Value = DevExpress.XtraCharts.ViewType.RangeArea3D.ToString() },
            };
        }
        public static List<SelectListItem> GetGanttViews()
        {
            return new List<SelectListItem>() {
                new SelectListItem() { Text = "Gantt", Value = DevExpress.XtraCharts.ViewType.Gantt.ToString() },
                new SelectListItem() { Text = "Side-by-Side Gantt", Value = DevExpress.XtraCharts.ViewType.SideBySideGantt.ToString() }
            };
        }
        public static List<SelectListItem> GetFinancialViews()
        {
            return new List<SelectListItem>() {
                new SelectListItem() { Text = "Stock", Value = DevExpress.XtraCharts.ViewType.Stock.ToString() },
                new SelectListItem() { Text = "Candle Stick", Value = DevExpress.XtraCharts.ViewType.CandleStick.ToString() }
            };
        }
        public static List<SelectListItem> GetFinancialLabelLevels()
        {
            return new List<SelectListItem>() {
                new SelectListItem() { Text = "Close", Value = StockLevel.Close.ToString() },
                new SelectListItem() { Text = "High", Value = StockLevel.High.ToString() },
                new SelectListItem() { Text = "Low", Value = StockLevel.Low.ToString() },
                new SelectListItem() { Text = "Open", Value = StockLevel.Open.ToString() }
            };
        }
        public static List<SelectListItem> GetSortValues()
        {
            return new List<SelectListItem>() {
                new SelectListItem() { Text = "Products", Value = SeriesPointKey.Argument.ToString() },
                new SelectListItem() { Text = "Price", Value = SeriesPointKey.Value_1.ToString(), Selected = true }
            };
        }

        public static List<SelectListItem> GetRadarDiagramTypes()
        {
            return new List<SelectListItem>() {
                new SelectListItem() { Text = "Circle", Value = RadarDiagramDrawingStyle.Circle.ToString(), Selected = true },
                new SelectListItem() { Text = "Polygon", Value = RadarDiagramDrawingStyle.Polygon.ToString() }
            };
        }
        public static List<string> GetExportFormats()
        {
            return new List<string>() { "pdf", "xls", "xlsx", "rtf", "mht", "png", "jpeg", "bmp", "tiff", "gif" };
        }
        public static List<int> GetMarkerSizes()
        {
            return new List<int>() { 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30 };
        }
    }
}