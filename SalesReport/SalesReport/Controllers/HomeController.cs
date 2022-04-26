using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesReport.Controllers
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
            ViewBag.Title = "Welcome";

            return View();
        }

        public ActionResult Files()
        {
            ViewBag.Title = "Report Files";
            ViewBag.Email = User.Identity.Name;
            var userId = User.Identity.GetUserId();
            var role = UserManager.GetRolesAsync(userId).Result.FirstOrDefault();
            ViewBag.Role = role;
            return View();
        }

        public ActionResult Report()
        {
            ViewBag.Title = "Report";
            ViewBag.Email = User.Identity.Name;
            var userId = User.Identity.GetUserId();
            var role = UserManager.GetRolesAsync(userId).Result.FirstOrDefault();
            ViewBag.Role = role;
            return View();
        }
    }
}
