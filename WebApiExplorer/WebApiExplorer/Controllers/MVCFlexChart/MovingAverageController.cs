using System.Web.Mvc;
using WebApiExplorer.Models;

namespace WebApiExplorer.Controllers
{
    public partial class MVCFlexChartController : Controller
    {
        public ActionResult MovingAverage()
        {
            var mPoints = MathPoint.GetMathPointList(30);
            ViewBag.Options = _flexChartModel;
            return View(mPoints);
        }
    }
}
