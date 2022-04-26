using C1.Web.Mvc.Chart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApiExplorer.Models;

namespace WebApiExplorer.Controllers
{
    public partial class MVCTreeMapController : Controller
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
                    {"Type", "Squarified"},
                    {"MaxDepth", 2}
                }
            };

            return View(ThingSale.GetDate());
        }

        private static IDictionary<string, object[]> CreateSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"Type", new object[]{"Squarified", "Horizontal", "Vertical"}},
                {"MaxDepth", new object[]{ 0, 1, 2, 3, 4}},
            };
            return settings;
        }
    }
}
