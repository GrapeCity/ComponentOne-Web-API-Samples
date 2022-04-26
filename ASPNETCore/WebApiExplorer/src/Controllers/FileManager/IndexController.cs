using Microsoft.AspNetCore.Mvc;

namespace WebApiExplorer.Controllers
{
    public partial class FileManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
