using Microsoft.AspNetCore.Mvc;
using WebApiExplorer.Models;

namespace WebApiExplorer.Controllers.Wijmo5FlexGrid
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

        public IActionResult Paging()
        {
            ViewBag.Options = _flexGridPagingModel;
            return View();
        }
    }
}
