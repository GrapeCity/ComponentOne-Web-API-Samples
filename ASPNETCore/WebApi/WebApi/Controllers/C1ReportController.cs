#if !ASPNETCORE
using IActionResult = System.Web.Http.IHttpActionResult;
#else
using Microsoft.AspNetCore.Mvc;
#endif
using C1.Web.Api.Report;
using WebApi.Models;
using System.Text;
using System.Collections;

namespace WebApi.Controllers
{
    /// <summary>
    /// Define a new controller which extends from <see cref="ReportController"/>.
    /// </summary>
    public class C1ReportController : ReportController
    {
        /// <summary>
        /// Overrides to response html content when accessing the "http://host/api/report" url in the browser.
        /// </summary>
        /// <returns>The html content.</returns>
        public override IActionResult GetCatalogInfo(string path, bool recursive = false)
        {
            if (C1Controller.IsIntroductionRequest(this))
            {
                return C1Controller.GetIntroductionPageResult(this, GetReport());
            }
            return base.GetCatalogInfo(path, recursive);
        }

        private string GetReportTree(string name, string title, IEnumerable source)
        {
            var sbContent = new StringBuilder();
            sbContent.AppendLine(string.Format("<h5>{0}</h5>", title));
            sbContent.Append(PageTemplate.GetTree(source, name, "Title", "Children", true));
            return sbContent.ToString();
        }

        private string GetDetails()
        {
            var sbContent = new StringBuilder();
            sbContent.Append(GetReportTree("FlexReports", Localization.Resource.FlexReports, ReportFiles.Reports));
            sbContent.Append(GetReportTree("SSRSReports", Localization.Resource.SSRSReports, Documents.SsrsReports.Items));
            return sbContent.ToString();
        }

        private C1APIIntroduction GetReport()
        {
            var c1APIIntroduction = new C1APIIntroduction();
            c1APIIntroduction.Name = "Report";
            c1APIIntroduction.Abstract = "<p>" + Localization.Resource.Report_Text1 + "</p>"
                + "<p>" + Localization.Resource.Report_Text2 + "</p>"
                + "<h4>" + Localization.Resource.Report_Text3 + "</h4>"
                + "<p>" + Localization.Resource.Report_Text4 + "</p>";
            c1APIIntroduction.Details = GetDetails();
            return c1APIIntroduction;
        }
    }
}