using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SalesAnalysis.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesAnalysis.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager = _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Welcome - Sales Analysis";
            return View();
        }

        [Authorize]
        public ActionResult Analysis()
        {
            ViewBag.Title = "Sales Analysis";

            var userId = User.Identity.GetUserId();
            var role = UserManager.GetRolesAsync(userId).Result.FirstOrDefault();
            ViewBag.Email = User.Identity.Name;
            ViewBag.Role = role;

            var tables = ViewPermission.All.Where(i => i.Value.Roles.Any(r => User.IsInRole(r)))
                .Select(i => i.Key).ToList();
            ViewBag.Tables = tables;
            return View();
        }
    }
}
