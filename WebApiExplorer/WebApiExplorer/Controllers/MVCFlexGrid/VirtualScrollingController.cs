using WebApiExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApiExplorer.Controllers
{
    public partial class MVCFlexGridController : Controller
    {
        private readonly GridExportImportOptions _flexGridVirtualScrollingModel = new GridExportImportOptions
        {
            NeedExport = true,
            NeedImport = false,
            IncludeColumnHeaders = true
        };

        public ActionResult VirtualScrolling()
        {
            ViewBag.Options = _flexGridVirtualScrollingModel;
            return View(Sale.GetData(1000));
        }
    }
}
