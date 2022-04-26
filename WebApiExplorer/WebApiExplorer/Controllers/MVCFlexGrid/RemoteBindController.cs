using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using C1.Web.Mvc;
using C1.Web.Mvc.Serializition;
using WebApiExplorer.Models;

namespace WebApiExplorer.Controllers
{
    public partial class MVCFlexGridController : Controller
    {
        private readonly GridExportImportOptions _flexGridRemoteBindModel = new GridExportImportOptions
        {
            NeedExport = true,
            NeedImport = false,
            IncludeColumnHeaders = true
        };

        public ActionResult RemoteBind_Read([C1JsonRequest] CollectionViewRequest<Sale> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, Sale.GetData(500)));
        }

        public ActionResult RemoteBind()
        {
            ViewBag.Options = _flexGridRemoteBindModel;
            return View();
        }
    }
}
