using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using ExcelExportWithLargeData.Models;
using System.Web.Mvc;

namespace ExcelExportWithLargeData.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(Order.All);
        }

        public ActionResult OrdersData([C1JsonRequest]CollectionViewRequest<Order> request)
        {
            return this.C1Json(CollectionViewHelper.Read(request, Order.All));
        }
    }
}