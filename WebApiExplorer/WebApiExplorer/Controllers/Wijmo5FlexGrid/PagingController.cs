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
        private readonly GridExportImportOptions _flexGridPagingModel = new GridExportImportOptions
        {
            NeedExport = true,
            NeedImport = false,
            IncludeColumnHeaders = true,
            OnlyCurrentPage = false
        };

        public ActionResult Paging()
        {
            ViewBag.Options = _flexGridPagingModel;
            return View();
        }
    }
}
