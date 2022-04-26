using C1.Web.Api.BarCode;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
  /// <summary>
  /// Define a new controller which extends from <see cref="BarCodeController"/>.
  /// </summary>
  /// 
  [Route("api/[controller]")]
  [ApiController]
  public class C1BarcodeController : BarCodeController
    {
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