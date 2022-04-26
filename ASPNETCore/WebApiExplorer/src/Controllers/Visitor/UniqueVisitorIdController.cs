using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebApiExplorer.Controllers.Visitor
{
    public partial class VisitorController : Controller
    {
        public IActionResult UniqueVisitorId()
        {
            return View();
        }
    }
}