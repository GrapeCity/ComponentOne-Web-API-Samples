using System.Web.Mvc;

namespace WebApiExplorer.Controllers
{
    public partial class FileManagerController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}