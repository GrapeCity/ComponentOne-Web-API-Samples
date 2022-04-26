using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApiExplorer.Models;

namespace WebApiExplorer.Controllers
{
    public partial class Wijmo5RadialGaugeController : Controller
    {
        private readonly ImageExportOptions _radialGaugeModel = new ImageExportOptions
        {
            Exporter = "wijmo.gauge.ImageExporter"
        };

        private readonly ClientSettingsModel _demoSettingsModel = new ClientSettingsModel
        {
            Settings = new Dictionary<string, object[]>
            {
                {"IsReadOnly", new object[]{false, true }},
                {"Step", new object[]{0.5, 1, 2}},
                {"ShowRanges", new object[]{true, false}},
                {"ShowText", new object[]{"All", "Value", "MinMax", "None"}},
                {"SweepAngle", new object[]{180, 270, 360}},
                {"StartAngle", new object[]{0, 90, 180, 270, 360}}
            }
        };

        public ActionResult Index()
        {
            ViewBag.DemoSettingsModel = _demoSettingsModel;
            ViewBag.Options = _radialGaugeModel;
            return View();
        }
    }
}
