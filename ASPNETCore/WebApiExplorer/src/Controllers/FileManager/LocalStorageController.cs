using Microsoft.AspNetCore.Mvc;

namespace WebApiExplorer.Controllers
{
    public partial class FileManagerController : Controller
    {
        public IActionResult LocalStorage()
        {
            return View();
        }
    }
}
