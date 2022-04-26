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
        public ActionResult FinancialChart()
        {
            List<FinanceData> financeDatas = new List<FinanceData>() { };

            DateTime startTime = new DateTime(2013, 1, 1);
            var rand = new Random();
            double high, low, open, close;
            for (int i = 0; i < 90; i++)
            {
                DateTime dt = startTime.AddDays(i);

                if (i > 0)
                    open = financeDatas[i - 1].Close;
                else
                    open = 1000;

                high = open + rand.NextDouble() * 50;
                low = open - rand.NextDouble() * 50;

                close = low + rand.NextDouble() * (high - low);
                financeDatas.Add(new FinanceData { X = dt, High = high, Low = low, Open = open, Close = close });
            }

            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = CreateFinacialChartSettings()
            };

            ViewBag.Options = _flexChartModel;
            return View(financeDatas);
        }

        private static IDictionary<string, object[]> CreateFinacialChartSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"ChartType", new object[]{"Candlestick", "HighLowOpenClose"}},
            };

            return settings;
        }
    }
}
