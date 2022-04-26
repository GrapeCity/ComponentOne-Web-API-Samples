using System.Collections.Generic;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiExplorer.Models;

namespace WebApiExplorer.Controllers
{
    public partial class MVCFlexGridController : Controller
    {
        private readonly GridExportImportOptions _flexGridDisableServerReadModel = new GridExportImportOptions
        {
            NeedExport = true,
            NeedImport = false,
            IncludeColumnHeaders = true
        };

        private readonly ControlOptions _disableServerReadSetting = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Disable Server Read",new OptionItem{Values = new List<string> {"True", "False"},CurrentValue = "True"}}
            }
        };

        public IActionResult DisableServerRead(IFormCollection collection)
        {
            _disableServerReadSetting.LoadPostData(collection);
            ViewBag.DemoOptions = _disableServerReadSetting;
            ViewBag.Options = _flexGridDisableServerReadModel;
            return View();
        }

        public ActionResult DisableServerRead_Bind([C1JsonRequest] CollectionViewRequest<Sale> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, Sale.GetData(500)));
        }
    }
}