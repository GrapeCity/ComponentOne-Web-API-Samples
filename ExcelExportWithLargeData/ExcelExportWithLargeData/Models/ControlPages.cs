using System.Collections.Generic;
using System.Web;

namespace ExcelExportWithLargeData.Models
{
    public static class ControlPages
    {
        public static IDictionary<string, string> GetPageSources()
        {
            var pageSources = new Dictionary<string, string>();
            var controllerName = "Home";
            var actionName = "Index";
            var controllerFileName = controllerName + "Controller.cs";
            var controllerFilePath = HttpContext.Current.Server.MapPath(
                string.Format("~/Controllers/{0}", controllerFileName));
            var controllerFileHtml = GetFileAsHtmlContent(controllerFilePath);
            pageSources.Add(controllerFileName, controllerFileHtml);

            var viewFileName = actionName + ".cshtml";
            var viewFilePath = HttpContext.Current.Server.MapPath(
                string.Format("~/Views/{0}/{1}", controllerName, viewFileName));
            var viewFileHtml = GetFileAsHtmlContent(viewFilePath);
            pageSources.Add(viewFileName, viewFileHtml);

            var startup = "Startup.cs";
            var startupFilePath = HttpContext.Current.Server.MapPath(string.Format("~/{0}", startup));
            pageSources.Add(startup, GetFileAsHtmlContent(startupFilePath));

            return pageSources;
        }

        private static string GetFileAsHtmlContent(string controllerFilePath)
        {
            return System.IO.File.ReadAllText(controllerFilePath);
        }
    }
}