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
        private readonly GridExportImportOptions _flexGridFilteringModel = new GridExportImportOptions
        {
            NeedExport = true,
            NeedImport = false,
            IncludeColumnHeaders = true
        };
        public ActionResult Filtering()
        {
            ViewBag.Options = _flexGridFilteringModel;
            return View();
        }
    }
}
