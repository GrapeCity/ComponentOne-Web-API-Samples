using System.Collections.Generic;
using WebApi.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Builder;
using IAppBuilder = Microsoft.AspNetCore.Builder.IApplicationBuilder;

namespace WebApi.Controllers
{
  /// <summary>
  /// Define a new controller which extends from <see cref="Controller"/>.
  /// </summary>
  public class C1Controller : Controller
  {
    internal static IList<DataEngineKey> DataEngineKeys = new List<DataEngineKey>();

    private static FormatterCollection<IOutputFormatter> Formatters = new FormatterCollection<IOutputFormatter>(new List<IOutputFormatter>()
    {
        new HTMLMediaTypeFormatter()
    });

    /// <summary>
    /// Overrides to response html content when accessing the "http://host/api/dataengine" url in the browser.
    /// </summary>
    /// <returns>The html content.</returns>
    [HttpGet]
    [Route("api/dataengine")]
    public IActionResult GetDataEngineIntroduction()
    {
      return GetResult(GetDataEngine());
    }

    private IActionResult GetResult(C1APIIntroduction value)
    {
      if (IsHtmlRequest(this))
      {
        return GetIntroductionPageResult(this, value);
      }
      return NotFound();
    }

    internal static IActionResult GetIntroductionPageResult(Controller controller, C1APIIntroduction value)
    {

      var result = controller.Ok(value);
      result.Formatters = Formatters;
      return result;
    }

    private static bool IsHtmlRequest(Controller controller)
    {
      var acceptedMediaType = "text/html";
      return controller.Request.Headers["Accept"].Any(item => item.Contains(acceptedMediaType));
    }

    internal static bool IsIntroductionRequest(Controller controller)
    {
      var isIntroductionUrl = !controller.Request.QueryString.HasValue;
      return isIntroductionUrl && IsHtmlRequest(controller);
    }

    internal static void CacheDataEngineDataKey(IAppBuilder app)
    {
      var providerManager = app.UseDataEngineProviders();
      foreach (var key in providerManager.Items.Keys)
      {
        DataEngineKeys.Add(new DataEngineKey
        {
          Name = key
        });
      }
    }

    private C1APIIntroduction GetDataEngine()
    {
      var c1APIIntroduction = new C1APIIntroduction();
      c1APIIntroduction.Name = "DataEngine";
      c1APIIntroduction.Abstract = "<p>" + Localization.Resource.DataEngine_Text1 + "</p>"
          + "<p>" + Localization.Resource.DataEngine_Text2 + "</p>"
          + "<h4>" + Localization.Resource.DataEngine_Title1 + "</h4>"
          + "<p>" + Localization.Resource.DataEngine_Text3 + "</p>";
      c1APIIntroduction.Details = PageTemplate.GetTree(DataEngineKeys, "dataSourceTree", "Name", "Children", true);
      return c1APIIntroduction;
    }

    private C1APIIntroduction GetImage()
    {
      var c1APIIntroduction = new C1APIIntroduction();
      c1APIIntroduction.Name = "Image";
      c1APIIntroduction.Abstract = "<p>" + Localization.Resource.Image_Text1 + "</p>"
          + "<p>" + Localization.Resource.Image_Text2 + "</p>";
      return c1APIIntroduction;
    }

    private C1APIIntroduction GetPdf()
    {
      var c1APIIntroduction = new C1APIIntroduction();
      c1APIIntroduction.Name = "Pdf";
      c1APIIntroduction.Abstract = "<p>" + Localization.Resource.Pdf_Text1 + "</p>"
          + "<p>" + Localization.Resource.Pdf_Text2 + "</p>"
          + "<h4>" + Localization.Resource.PdfFiles + "</h4>";
      c1APIIntroduction.Details = PageTemplate.GetTree(Documents.Pdfs.Items, "PDFFiles", "Title", "Children", true);
      return c1APIIntroduction;
    }
  }
}