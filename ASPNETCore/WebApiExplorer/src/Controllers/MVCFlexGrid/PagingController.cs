using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Paging(IFormCollection data)
        {
            _gridPagingModel.LoadPostData(data);
            ViewBag.DemoOptions = _gridPagingModel;
            ViewBag.Options = _flexGridPagingModel;
            return View();
        }

        public ActionResult Paging_Bind([C1JsonRequest] CollectionViewRequest<Sale> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, Sale.GetData(500)));
        }
    }
}