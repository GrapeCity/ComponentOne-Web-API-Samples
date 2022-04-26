using C1.Web.Mvc.Chart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApiExplorer.Models;

namespace WebApiExplorer.Controllers
{
    public partial class MVCFlexChartController : Controller
    {
        private List<PopulationByCountry> _populationByCountry = PopulationByCountry.GetData();
        public ActionResult ErrorBar()
        {
            const string DIRECTION = "Direction";
            const string ERROR_AMOUNT = "ErrorAmount";
            const string VALUE = "Value";
            const string END_STYLE = "EndStyle";

            ViewBag.Options = _flexChartModel;
            var width = new object[] { 0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1 };
            var boolValues = new object[] { false, true };
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = new Dictionary<string, object[]>
                {
                    {DIRECTION, Enum.GetValues(typeof(ErrorBarDirection)).Cast<object>().ToArray()},
                    {ERROR_AMOUNT, Enum.GetValues(typeof(ErrorAmount)).Cast<object>().ToArray()},
                    {VALUE, new object[]{50, 100, 150, 200}},
                    {END_STYLE, Enum.GetValues(typeof(ErrorBarEndStyle)).Cast<object>().ToArray()}
                },
                HeaderNames = new Dictionary<string, string>
                {
                    {DIRECTION, Resources.Wijmo5FlexChart.ErrorBar_Direction },
                    {ERROR_AMOUNT, Resources.MVCFlexChart.Error_ErrorAmount },
                    {VALUE, Resources.MVCFlexChart.Error_Value },
                    {END_STYLE, Resources.Wijmo5FlexChart.ErrorBar_EndStyle}
                }
            };

            return View(_populationByCountry);
        }
    }
}
