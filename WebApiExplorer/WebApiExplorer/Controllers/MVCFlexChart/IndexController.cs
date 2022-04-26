using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApiExplorer.Models;

namespace WebApiExplorer.Controllers
{
    public partial class MVCFlexChartController : Controller
    {
        private readonly ImageExportOptions _flexChartModel = new ImageExportOptions
        {
            Exporter = "wijmo.chart.ImageExporter"
        };

        private static IDictionary<string, object[]> CreateIndexSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"ChartType", new object[]{"Column", "Bar", "Scatter", "Line", "LineSymbols", "Area", "Spline", "SplineSymbols", "SplineArea"}},
                {"Stacking", new object[]{"None", "Stacked", "Stacked 100%"}},
                {"Rotated", new object[]{false, true}},
                {"Palette", new object[]{"standard", "cocoa", "coral", "dark", "highcontrast", "light", "midnight", "minimal", "modern", "organic", "slate"}},
                {"GroupWidth", new object[]{"25%", "70%", "100%", "50 pixels"}}
            };
            return settings;
        }

        private static IDictionary<string, object> GetIndexDefaultValues()
        {
            var defaultValues = new Dictionary<string, object>
            {
                {"GroupWidth", "70%"}
            };

            return defaultValues;
        }

        public ActionResult Index()
        {
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = CreateIndexSettings(),
                DefaultValues = GetIndexDefaultValues()
            };

            ViewBag.Options = _flexChartModel;
            return View(Fruit.GetFruitsSales());
        }

    }
}
