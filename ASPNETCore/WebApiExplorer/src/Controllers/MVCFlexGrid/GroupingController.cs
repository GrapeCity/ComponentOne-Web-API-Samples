using System.Collections.Generic;
using System.Linq;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using Microsoft.AspNetCore.Mvc;
using WebApiExplorer.Models;

namespace WebApiExplorer.Controllers
{
    public partial class MVCFlexGridController : Controller
    {
        private readonly GridExportImportOptions _flexGridGroupModel = new GridExportImportOptions
        {
            NeedExport = true,
            NeedImport = false,
            OnlyCurrentPage = false,
            IncludeColumnHeaders = true
        };

        public IActionResult Grouping()
        {
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = new Dictionary<string, object[]>
                {
                    {"GroupBy", new object[]{"ShipCountry", "ShipCity", "ShipCountry and ShipCity", "Freight", "ShippedDate","None"}}
                }
            };
            ViewBag.Options = _flexGridGroupModel;
            return View();
        }

        public IActionResult Grouping_Bind([C1JsonRequest] CollectionViewRequest<Order> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, _db.Orders.ToList()));
        }
    }
}