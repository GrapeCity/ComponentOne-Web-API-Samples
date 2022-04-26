using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

        public ActionResult Grouping()
        {
            const string GROUP_BY = "GroupBy";
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = new Dictionary<string, object[]>
                {
                    {GROUP_BY, new object[]{"ShipCountry", "ShipCity", "ShipCountry and ShipCity", "Freight", "ShippedDate","None"}}
                },
                HeaderNames = new Dictionary<string, string>
                {
                    {GROUP_BY, Resources.Wijmo5FlexGrid.Grouping_GroupBy }
                }
            };
            ViewBag.Options = _flexGridGroupModel;
            var nwind = new C1NWindEntities();
            return View(nwind.Orders);
        }

    }
}
