using C1.Web.Api.DataEngine.Models;
using SalesAnalysis.Models;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;

namespace SalesAnalysis.Controllers
{
    [Authorize]
    public class DataEngineController : C1.Web.Api.DataEngine.DataEngineController
    {
        public override IHttpActionResult GetFields(string dataSourceKey)
        {
            var result = base.GetFields(dataSourceKey);
            var contentResult = result as OkNegotiatedContentResult<IEnumerable<Field>>;
            if (contentResult == null)
            {
                return result;
            }

            var content = contentResult.Content as IEnumerable<Field>;
            content = ViewPermission.VerifyFields(dataSourceKey, content, RequestContext.Principal);
            return Ok(content);
        }
    }
}