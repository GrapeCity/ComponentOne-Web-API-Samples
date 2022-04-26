using C1.Web.Mvc.Chart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApiExplorer.Models;

namespace WebApiExplorer.Controllers
{
    public partial class MVCSunburstController : Controller
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
                Settings = CreateSettings(),
                DefaultValues = new Dictionary<string, object>
                {
                    {"DataLabel.Position", PieLabelPosition.Center}
                }
            };

            return View(HierarchicalData.GetData());
        }

        private static IDictionary<string, object[]> CreateSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"InnerRadius", new object[] {0, 0.25, 0.5, 0.75}},
                {"Offset", new object[] {0, 0.1, 0.2, 0.3, 0.4, 0.5}},
                {"StartAngle", new object[] {0, 90, 180, -90}},
                {"Reversed", new object[] {false, true}},
                {"Palette", new object[] {"standard", "cocoa", "coral", "dark", "highcontrast", "light", "midnight", "minimal", "modern", "organic", "slate"}},
                {"DataLabel.Position", Enum.GetValues(typeof(PieLabelPosition)).Cast<object>().ToArray()},
                {"DataLabel.Border", new object[] {false, true}},
            };
            return settings;
        }
    }
}
