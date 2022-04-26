using System.Web.Mvc;

namespace WebApiExplorer.Controllers
{
    public partial class Wijmo5FlexChartController : Controller
    {
        public ActionResult TrendLine()
        {
            ViewBag.Options = _flexChartModel;
            return View();
        }
    }
}
