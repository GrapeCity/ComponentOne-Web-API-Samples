using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            ViewBag.Options = _flexGridModel;
            return View();
        }
    }
}