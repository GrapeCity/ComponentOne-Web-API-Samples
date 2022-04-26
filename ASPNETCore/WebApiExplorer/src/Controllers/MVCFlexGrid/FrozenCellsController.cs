using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using Microsoft.AspNetCore.Mvc;
using WebApiExplorer.Models;

namespace WebApiExplorer.Controllers
{
    public partial class MVCFlexGridController : Controller
    {
        private readonly GridExportImportOptions _flexGridFrozenCellsModel = new GridExportImportOptions
        {
            NeedExport = true,
            NeedImport = false,
            IncludeColumnHeaders = true
        };

        public IActionResult FrozenCells()
        {
            ViewBag.Options = _flexGridFrozenCellsModel;
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = new Dictionary<string, object[]>
                {
                    {"FrozenColumns", new object[]{1, 0, 2, 3}},
                    {"FrozenRows", new object[]{2, 0, 1, 3, 4, 5}}
                }
            };
            return View();
        }

        public IActionResult FrozenCells_Bind([C1JsonRequest] CollectionViewRequest<Sale> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, Sale.GetData(100)));
        }
    }
}