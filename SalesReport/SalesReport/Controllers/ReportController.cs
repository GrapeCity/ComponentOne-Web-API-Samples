using System.Web.Http;

namespace SalesReport.Controllers
{
    public class ReportController : C1.Web.Api.Report.ReportController
    {
        [StorageAuthorize]
        public override IHttpActionResult GetCatalogInfo(string path, bool recursive = false)
        {
            return base.GetCatalogInfo(path, recursive);
        }
    }
}