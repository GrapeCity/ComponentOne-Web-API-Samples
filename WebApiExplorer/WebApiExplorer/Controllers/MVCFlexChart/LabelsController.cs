using C1.Web.Mvc.Chart;
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
        const string CHART_TYPE = "ChartType";
        const string DATA_LABEL_POSITION = "DataLabel.Position";
        const string DATA_LABEL_BORDER = "DataLabel.Border";

        private static IDictionary<string, object[]> CreateLabelSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {CHART_TYPE, new object[]{"Column", "Bar", "Scatter", "Line", "LineSymbols", "Area", "Spline", "SplineSymbols", "SplineArea"}},
                {DATA_LABEL_POSITION, new object[]{LabelPosition.Top, LabelPosition.Right, LabelPosition.Bottom, LabelPosition.Left, LabelPosition.None}},
                {DATA_LABEL_BORDER, new object[]{false, true}},
            };

            return settings;
        }

        public ActionResult Labels()
        {
            var model = new ClientSettingsModel
            {
                Settings = CreateLabelSettings(),
                HeaderNames = new Dictionary<string, string>
                {
                    {CHART_TYPE, Resources.Wijmo5FlexChart.Labels_ChartType },
                    {DATA_LABEL_POSITION, Resources.Wijmo5FlexChart.Labels_LabelPosition },
                    {DATA_LABEL_BORDER, Resources.Wijmo5FlexChart.Labels_LabelsBorder }
                }
            };
            ViewBag.Options = _flexChartModel;
            return View(model);
        }
    }
}
