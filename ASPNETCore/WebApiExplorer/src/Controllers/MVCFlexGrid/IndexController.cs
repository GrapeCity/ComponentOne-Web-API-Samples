using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using WebApiExplorer.Models;
using C1.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using C1.Web.Mvc.Serialization;

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
                {"Column Resize", new OptionItem {Values = new List<string> {"100", "250"}, CurrentValue = "100"}},
            }
        };

        public IActionResult Index(IFormCollection collection)
        {
            _gridDataModel.LoadPostData(collection);
            ViewBag.Options = _flexGridModel;
            ViewBag.DemoOptions = _gridDataModel;
            return View();
        }

        public ActionResult Index_Bind([C1JsonRequest] CollectionViewRequest<Sale> requestData)
        {
            var extraData = requestData.ExtraRequestData
                 .ToDictionary(kvp => kvp.Key, kvp => new StringValues(kvp.Value.ToString()));
            var data = new FormCollection(extraData);
            _gridDataModel.LoadPostData(data);
            var model = Sale.GetData(Convert.ToInt32(_gridDataModel.Options["items"].CurrentValue));
            return this.C1Json(CollectionViewHelper.Read(requestData, model));
        }
    }
}