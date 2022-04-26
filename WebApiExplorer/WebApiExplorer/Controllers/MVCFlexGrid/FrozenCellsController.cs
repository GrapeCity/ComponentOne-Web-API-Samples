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
        private readonly GridExportImportOptions _flexGridFrozenCellsModel = new GridExportImportOptions
        {
            NeedExport = true,
            NeedImport = false,
            IncludeColumnHeaders = true
        };

        public ActionResult FrozenCells()
        {
            ViewBag.Options = _flexGridFrozenCellsModel;
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = new Dictionary<string, object[]>
                {
                    {"FrozenColumns", new object[]{1, 0, 2, 3}},
                    {"FrozenRows", new object[]{2, 0, 1, 3, 4, 5}}
                },
                HeaderNames = new Dictionary<string, string> 
                {
                    {"FrozenColumns", Resources.MVCFlexGrid.FrozenCells_FrozenColumns},
                    {"FrozenRows", Resources.MVCFlexGrid.FrozenCells_FrozenRows}
                }
            };
            return View(Sale.GetData(100));
        }
    }
}
