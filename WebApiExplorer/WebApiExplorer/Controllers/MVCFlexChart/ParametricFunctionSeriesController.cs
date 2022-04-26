using System.Web.Mvc;

namespace WebApiExplorer.Controllers
{
    public partial class MVCFlexChartController : Controller
    {
        public ActionResult ParametricFunctionSeries()
        {
            ViewBag.Options = _flexChartModel;
            return View();
        }
    }
}
