#if !ASPNETCORE
using IActionResult = System.Web.Http.IHttpActionResult;
using FromQuery = C1.Web.Api.FromUriExAttribute;
#else
using Microsoft.AspNetCore.Mvc;
#endif
using C1.Web.Api.Excel;
using WebApi.Models;

namespace WebApi.Controllers
{
    /// <summary>
    /// Define a new controller which extends from <see cref="ExcelController"/>.
    /// </summary>
    public class C1ExcelController : ExcelController
    {
        /// <summary>
        /// Overrides to response html content when accessing the "http://host/api/excel" url in the browser.
        /// </summary>
        /// <returns>The html content.</returns>
        public override IActionResult Get([FromQuery] ExcelRequest re)
        {
            if (C1Controller.IsIntroductionRequest(this))
            {
                return C1Controller.GetIntroductionPageResult(this, GetExcel());
            }
            return base.Get(re);
        }
        private C1APIIntroduction GetExcel()
        {
            var c1APIIntroduction = new C1APIIntroduction();
            c1APIIntroduction.Name = "Excel";
            c1APIIntroduction.Abstract = "<p>"+ Resources.Resource.Excel_Text1 + "</p>"
                + "<p>" + Resources.Resource.Excel_Text2 + "</p>"
                + "<p>" + Resources.Resource.Excel_Text3 + "</p>";
            return c1APIIntroduction;
        }
    }
}