using Microsoft.AspNetCore.Mvc;
using WebApiExplorer.Models;

namespace WebApiExplorer.Controllers.MVCOlap
{
    public partial class MVCOlapController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.DataSets = OlapData.GetDataSets();
            ViewBag.ShowTotals = OlapData.GetShowTotals();
            return View();
        }
    }
}