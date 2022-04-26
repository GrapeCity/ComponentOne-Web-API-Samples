using System.Collections;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using C1.Web.Mvc;
using WebApiExplorer.Models;
using System.Collections.Generic;
using System;

namespace WebApiExplorer.Controllers
{
    public partial class MVCFlexGridController : Controller
    {
        private readonly GridExportImportOptions _flexGridModel = new GridExportImportOptions
        {
            NeedExport = true,
            NeedImport = true,
            IncludeColumnHeaders = true
        };

        private readonly ControlOptions _gridDataModel = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Items",new OptionItem{Values = new List<string> {"5", "50", "500", "5000"},CurrentValue = "500"}},
                {"Allow Sorting", new OptionItem {Values = new List<string> {"None", "SingleColumn","MultiColumn"}, CurrentValue = "None"}},
                {"Selection",new OptionItem{Values = new List<string> {"None", "Cell", "CellRange", "Row", "RowRange", "ListBox","MultiRange"},CurrentValue = "Cell"}},
                {"Formatting", new OptionItem {Values = new List<string> {"On", "Off"}, CurrentValue = "Off"}},
                {"Column Visibility",new OptionItem {Values = new List<string> {"Show", "Hide"}, CurrentValue = "Show"}},
                {"Column Resize", new OptionItem {Values = new List<string> {"100", "250"}, CurrentValue = "100"}}
            }
        };

        public ActionResult Index(FormCollection collection)
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

            _gridDataModel.LoadPostData(data);
            var model = Sale.GetData(Convert.ToInt32(_gridDataModel.Options["items"].CurrentValue));
            ViewBag.Options = _flexGridModel;
            ViewBag.DemoOptions = _gridDataModel;
            return View(model);
        }
    }
}
