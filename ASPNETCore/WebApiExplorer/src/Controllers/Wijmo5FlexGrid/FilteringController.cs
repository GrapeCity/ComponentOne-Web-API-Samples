using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Filtering()
        {
            ViewBag.Options = _flexGridFilteringModel;
            return View();
        }
    }
}
