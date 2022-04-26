using System.Web.Mvc;

namespace WebApiExplorer.Controllers
{
    public partial class Wijmo5FlexChartController : Controller
    {
        public ActionResult YFunctionSeries()
        {
            ViewBag.Options = _flexChartModel;
            return View();
        }
    }
}
