using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using Microsoft.AspNetCore.Mvc;
using WebApiExplorer.Models;

namespace WebApiExplorer.Controllers
{
    public partial class MVCFlexGridController : Controller
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

        public IActionResult Filtering_Bind([C1JsonRequest] CollectionViewRequest<Sale> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, Sale.GetData(100)));
        }
    }
}