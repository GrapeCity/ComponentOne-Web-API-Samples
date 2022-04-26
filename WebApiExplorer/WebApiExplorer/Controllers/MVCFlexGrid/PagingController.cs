using System.Collections.Generic;
using System.Web.Mvc;
using WebApiExplorer.Models;

namespace WebApiExplorer.Controllers
{
    public partial class MVCFlexGridController : Controller
    {
        private readonly GridExportImportOptions _flexGridPagingModel = new GridExportImportOptions
        {
            NeedExport = true,
            NeedImport = false,
            OnlyCurrentPage = false,
            IncludeColumnHeaders = true
        };

        private readonly ControlOptions _gridPagingModel = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Page Size", new OptionItem {Values = new List<string> {"10", "25", "50", "100"}, CurrentValue = "25"}},
            }
        };

        public ActionResult Paging(FormCollection data)
        {
            ViewBag.Options = _flexGridPagingModel;
            _gridPagingModel.LoadPostData(data);
            ViewBag.DemoOptions = _gridPagingModel;
            return View(Sale.GetData(500));
        }
    }
}
