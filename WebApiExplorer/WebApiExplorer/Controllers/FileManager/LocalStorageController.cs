using System.Web.Mvc;

namespace WebApiExplorer.Controllers
{
    public partial class FileManagerController : Controller
    {
        public ActionResult LocalStorage()
        {
            return View();
        }
    }
}