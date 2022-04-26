using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApiExplorer.Models;

namespace WebApiExplorer.Controllers
{
    public partial class MVCFlexChartController : Controller
    {
        public ActionResult BoxWhisker()
        {
            var analysisData = SalesAnalysis.GetData();
            var width = new object[] { 0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1 };
            var boolValues = new object[] { false, true };
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = new Dictionary<string, object[]>
                {
                    {"GroupWidth", width},
                    {"GapWidth", width},
                    {"ShowMeanLine", boolValues},
                    {"ShowMeanMarker", boolValues},
                    {"ShowInnerPoints", boolValues},
                    {"ShowOutliers", boolValues},
                    {"Rotated", boolValues},
                    {"ShowLabel", boolValues},
                    {"QuartileCalculation", Enum.GetValues(typeof(C1.Web.Mvc.Chart.QuartileCalculation)).Cast<object>().ToArray()}
                },
                DefaultValues = new Dictionary<string, object>
                {
                    {"GapWidth", "0.1"},
                    {"GroupWidth", "0.8"}
                }
            };

            ViewBag.Options = _flexChartModel;
            return View(analysisData);
        }
    }
}
