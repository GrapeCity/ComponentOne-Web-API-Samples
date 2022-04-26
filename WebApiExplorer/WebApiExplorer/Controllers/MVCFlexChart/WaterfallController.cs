using System.Collections.Generic;
using System.Web.Mvc;
using WebApiExplorer.Models;

namespace WebApiExplorer.Controllers
{
    public partial class MVCFlexChartController : Controller
    {
        public ActionResult Waterfall()
        {
            var data = WaterfallData.GetData();
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = new Dictionary<string, object[]>
                {
                    {"RelativeData", new object[] {false, true}},
                    {"ConnectorLines", new object[] {true, false}},
                    {"ShowTotal", new object[] {true, false}},
                    {"ShowIntermediateTotal", new object[] {false, true}}
                }
            };
            ViewBag.Options = _flexChartModel;
            return View(data);
        }
    }
}
