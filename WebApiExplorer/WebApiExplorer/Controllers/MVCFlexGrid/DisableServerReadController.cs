using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApiExplorer.Models;
using C1.Web.Mvc;

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

        public ActionResult DisableServerRead(FormCollection collection)
        {
            IValueProvider data = collection;
            if (CallbackManager.CurrentIsCallback)
            {
                var request = CallbackManager.GetCurrentCallbackData<CollectionViewRequest<object>>();
                if (request != null && request.ExtraRequestData != null)
                {
                    var extraData = request.ExtraRequestData.Cast<DictionaryEntry>()
                        .ToDictionary(kvp => (string)kvp.Key, kvp => kvp.Value.ToString());
                    data = new DictionaryValueProvider<string>(extraData, CultureInfo.CurrentCulture);
                }
            }

            _disableServerReadSetting.LoadPostData(data);
            ViewBag.DemoOptions = _disableServerReadSetting;
            ViewBag.Options = _flexGridDisableServerReadModel;
            return View(Sale.GetData(500));
        }
    }
}
