using System.Web.Mvc;

namespace WebApiExplorer.Controllers
{
    public partial class MVCFlexChartController : Controller
    {
        public ActionResult YFunctionSeries()
        {
            ViewBag.Options = _flexChartModel;
            return View();
        }
    }
}
