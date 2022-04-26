using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApiExplorer.Models;

namespace WebApiExplorer.Controllers.MVCOlap
{
    public partial class MVCOlapController : Controller
    {
        //
        // GET: /Index/

        public ActionResult Index()
        {
            ViewBag.DataSets = OlapData.GetDataSets();
            ViewBag.ShowTotals = OlapData.GetShowTotals();
            return View();
        }

    }
}
