using System.Web.Mvc;
using WebApiExplorer.Models;

namespace WebApiExplorer.Controllers
{
    public partial class MVCFlexChartController : Controller
    {
        public ActionResult TrendLine()
        {
            var mPoints = MathPoint.GetMathPointList(10);
            ViewBag.Options = _flexChartModel;
            return View(mPoints);
        }
    }
}
