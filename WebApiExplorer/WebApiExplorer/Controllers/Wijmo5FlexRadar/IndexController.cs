using System.Collections.Generic;
using System.Web.Mvc;
using WebApiExplorer.Models;

namespace WebApiExplorer.Controllers
{
    public partial class Wijmo5FlexRadarController : Controller
    {
        private readonly ImageExportOptions _imageExportOptions = new ImageExportOptions
        {
            Exporter = "wijmo.chart.ImageExporter"
        };

        public ActionResult Index()
        {
            ViewBag.Options = _imageExportOptions;
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = CreateRadarSettings(),
                DefaultValues = GetIndexDefaultValues()
            };

            return View();
        }

        private static IDictionary<string, object[]> CreateRadarSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"ChartType", new object[]{"Column", "Scatter", "Line", "LineSymbols", "Area"}},
                {"Stacking", new object[]{"None", "Stacked", "Stacked100pc"}},
                {"StartAngle", new object[]{0, 60, 120, 180, 240, 300, 360}},
                {"TotalAngle", new object[]{60, 120, 180, 240, 300, 360}},
                {"Reversed", new object[]{false, true}}
            };

            return settings;
        }

        private static IDictionary<string, object> GetIndexDefaultValues()
        {
            var defaultValues = new Dictionary<string, object>
            {
                {"TotalAngle", 360}
            };

            return defaultValues;
        }
    }
}
