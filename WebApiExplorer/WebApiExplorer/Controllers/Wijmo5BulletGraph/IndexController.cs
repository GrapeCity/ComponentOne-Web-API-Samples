using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApiExplorer.Models;

namespace WebApiExplorer.Controllers
{
    public partial class Wijmo5BulletGraphController : Controller
    {
        private readonly ImageExportOptions _bulletGaugeModel = new ImageExportOptions
        {
            Exporter = "wijmo.gauge.ImageExporter"
        };

        private readonly ClientSettingsModel _demoSettingsModel = new ClientSettingsModel
        {
            Settings = new Dictionary<string, object[]>
            {
                {"Direction", new object[]{"Right", "Left", "Down", "Up"}},
                {"IsReadOnly", new object[]{false, true }},
                {"Step", new object[]{0.5, 1, 2}},
                {"ShowRanges", new object[]{true, false}},
                {"ShowText", new object[]{"All", "Value", "MinMax", "None"}}
            }
        };

        public ActionResult Index()
        {
            ViewBag.DemoSettingsModel = _demoSettingsModel;
            ViewBag.Options = _bulletGaugeModel;
            return View();
        }
    }
}
