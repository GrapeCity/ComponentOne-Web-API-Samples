using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApiExplorer.Models;

namespace WebApiExplorer.Controllers
{
    public partial class Wijmo5FlexChartController : Controller
    {
        private readonly ImageExportOptions _flexChartModel = new ImageExportOptions
        {
            Exporter = "wijmo.chart.ImageExporter"
        };

        private readonly ClientSettingsModel _demoSettingsModel = new ClientSettingsModel
        {
            Settings = new Dictionary<string, object[]>
            {
                {"ChartType", new object[]{"Column", "Bar", "Scatter", "Line", "LineSymbols", "Area", "Spline", "SplineSymbols", "SplineArea"}},
                {"Stacking", new object[]{"None", "Stacked", "Stacked 100%"}},
                {"Rotated", new object[]{false, true}},
                {"Palette", new object[]{"standard", "cocoa", "coral", "dark", "highcontrast", "light", "midnight", "minimal", "modern", "organic", "slate"}}
            }
        };

        public ActionResult Index()
        {
            ViewBag.DemoSettingsModel = _demoSettingsModel;
            ViewBag.Options = _flexChartModel;
            return View();
        }

    }
}
