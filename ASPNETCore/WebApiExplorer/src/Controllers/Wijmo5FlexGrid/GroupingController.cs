using Microsoft.AspNetCore.Mvc;
using WebApiExplorer.Models;

namespace WebApiExplorer.Controllers.Wijmo5FlexGrid
{
    public partial class Wijmo5FlexGridController : Controller
    {
        private readonly GridExportImportOptions _flexGridGroupingModel = new GridExportImportOptions
        {
            NeedExport = true,
            NeedImport = false,
            IncludeColumnHeaders = true
        };
        public IActionResult Grouping()
        {
            ViewBag.Options = _flexGridGroupingModel;
            return View();
        }
    }
}
