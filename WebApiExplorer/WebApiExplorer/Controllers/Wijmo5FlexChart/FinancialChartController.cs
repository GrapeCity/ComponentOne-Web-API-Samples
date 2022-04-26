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
        private readonly ClientSettingsModel _financialChartSettingsModel = new ClientSettingsModel
        {
            Settings = new Dictionary<string, object[]>
            {
                {"ChartType", new object[]{"Candlestick", "HighLowOpenClose"}}
            }
        };

        public ActionResult FinancialChart()
        {
            ViewBag.DemoSettingsModel = _financialChartSettingsModel;
            ViewBag.Options = _flexChartModel;
            return View();
        }
    }
}
