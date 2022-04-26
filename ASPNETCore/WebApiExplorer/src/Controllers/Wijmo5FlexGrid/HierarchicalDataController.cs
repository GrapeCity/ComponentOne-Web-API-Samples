using Microsoft.AspNetCore.Mvc;
using WebApiExplorer.Models;

namespace WebApiExplorer.Controllers.Wijmo5FlexGrid
{
    public partial class Wijmo5FlexGridController : Controller
    {
        private readonly GridExportImportOptions _flexGridHierarchicalDataModel = new GridExportImportOptions
        {
            NeedExport = true,
            NeedImport = false,
            IncludeColumnHeaders = true
        };
        public IActionResult HierarchicalData()
        {
            ViewBag.Options = _flexGridHierarchicalDataModel;
            return View();
        }
    }
}
