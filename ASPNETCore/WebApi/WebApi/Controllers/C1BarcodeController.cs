#if !ASPNETCORE
using IActionResult = System.Web.Http.IHttpActionResult;
using FromQuery = System.Web.Http.FromUriAttribute;
#else
using Microsoft.AspNetCore.Mvc;
#endif
using C1.Web.Api.BarCode;
using WebApi.Models;

namespace WebApi.Controllers
{
    /// <summary>
    /// Define a new controller which extends from <see cref="BarCodeController"/>.
    /// </summary>
    public class C1BarcodeController : BarCodeController
    {
        /// <summary>
        /// Overrides to response html content when accessing the "http://host/api/barcode" url in the browser.
        /// </summary>
        /// <param name="re">The request parameters</param>
        /// <returns>The barcode image</returns>
        public override IActionResult Get([FromQuery] BarCodeRequest re)
        {
            if (C1Controller.IsIntroductionRequest(this))
            {
                return C1Controller.GetIntroductionPageResult(this, GetBarcode());
            }
            return base.Get(re);
        }

        private C1APIIntroduction GetBarcode()
        {
            var c1APIIntroduction = new C1APIIntroduction();
            c1APIIntroduction.Name = "Barcode";
            c1APIIntroduction.Abstract = "Barcode";
            c1APIIntroduction.Abstract = "<p>" + Localization.Resource.Barcode_Text1 + "</p>"
                + "<p>" + Localization.Resource.Barcode_Text2 + "</p>";
            return c1APIIntroduction;
        }
    }
}