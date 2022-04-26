using System.Web.Mvc;

namespace WebApiExplorer.Controllers
{
    public partial class Wijmo5FlexChartController : Controller
    {
        public ActionResult ParametricFunctionSeries()
        {
            ViewBag.Options = _flexChartModel;
            return View();
        }
    }
}
