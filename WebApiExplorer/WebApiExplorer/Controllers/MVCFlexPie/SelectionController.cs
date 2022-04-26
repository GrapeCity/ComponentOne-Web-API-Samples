using System.Collections.Generic;
using System.Web.Mvc;
using C1.Web.Mvc.Chart;
using WebApiExplorer.Models;

namespace WebApiExplorer.Controllers
{
    public partial class MVCFlexPieController : Controller
    {
        public ActionResult Selection()
        {
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = CreateSelectionSettings()
            };

            ViewBag.Options = _flexPieModel;
            return View(CustomerOrder.GetCountryGroupOrderData());
        }

        private static IDictionary<string, object[]> CreateSelectionSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"SelectedItemPosition", new object[]{Position.Top, Position.Bottom, Position.Left, Position.None, Position.Right}},
                {"SelectedItemOffset", new object[]{0, 0.1, 0.2}},
                {"IsAnimated", new object[]{true, false}}
            };

            return settings;
        }
    }
}