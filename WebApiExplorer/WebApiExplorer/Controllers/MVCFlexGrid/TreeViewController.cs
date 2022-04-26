using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApiExplorer.Models;

namespace WebApiExplorer.Controllers
{
    public partial class MVCFlexGridController : Controller
    {
        private readonly GridExportImportOptions _flexGridTreeViewModel = new GridExportImportOptions
        {
            NeedExport = true,
            NeedImport = false,
            IncludeColumnHeaders = true
        };

        public ActionResult TreeView()
        {
            ViewBag.Options = _flexGridTreeViewModel;
            var list = Folder.Create(Server.MapPath("~")).Children;
            return View(list);
        }

    }
}
