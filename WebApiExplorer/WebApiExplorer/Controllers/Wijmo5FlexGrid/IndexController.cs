using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApiExplorer.Models;

namespace WebApiExplorer.Controllers
{
    public partial class Wijmo5FlexGridController : Controller
    {
        private readonly GridExportImportOptions _flexGridModel = new GridExportImportOptions
        {
            NeedExport = true,
            NeedImport = true,
            IncludeColumnHeaders = true
        };
        public ActionResult Index()
        {
            ViewBag.Options = _flexGridModel;
            return View();
        }
    }
}
