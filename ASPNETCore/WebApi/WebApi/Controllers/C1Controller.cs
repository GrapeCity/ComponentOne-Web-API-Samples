using System.Collections.Generic;
using WebApi.Models;
using System.Linq;
#if !ASPNETCORE
using System.Web.Http.Results;
using Controller = System.Web.Http.ApiController;
using IActionResult = System.Web.Http.IHttpActionResult;
using System.Web.Http;
using System.Net.Http.Formatting;
using Owin;
#else
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Builder;
using IAppBuilder = Microsoft.AspNetCore.Builder.IApplicationBuilder;
#endif

namespace WebApi.Controllers
{
    /// <summary>
    /// Define a new controller which extends from <see cref="Controller"/>.
    /// </summary>
    public class C1Controller: Controller
    {
        internal static IList<DataEngineKey> DataEngineKeys = new List<DataEngineKey>();

#if !ASPNETCORE
        private static IEnumerable<MediaTypeFormatter> Formatters = new List<MediaTypeFormatter>
                    {
                        new HTMLMediaTypeFormatter()
                    };
#else
        private static FormatterCollection<IOutputFormatter> Formatters = new FormatterCollection<IOutputFormatter>(new List<IOutputFormatter>()
                    {
                        new HTMLMediaTypeFormatter()
                    });
#endif

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

        /// <summary>
        /// Overrides to response html content when accessing the "http://host/api/image" url in the browser.
        /// </summary>
        /// <returns>The html content.</returns>
        [HttpGet]
        [Route("api/image")]
        public IActionResult GetImageIntroduction()
        {
            return GetResult(GetImage());
        }

        /// <summary>
        /// Overrides to response html content when accessing the "http://host/api/pdf" url in the browser.
        /// </summary>
        /// <returns>The html content.</returns>
        [HttpGet]
        [Route("api/pdf")]
        public IActionResult GetPdfIntroduction()
        {
            return GetResult(GetPdf());
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
#if !ASPNETCORE
            var conneg = ServicesExtensions.GetContentNegotiator(controller.ControllerContext.Configuration.Services);
            return new OkNegotiatedContentResult<C1APIIntroduction>(value, conneg, controller.Request, Formatters);
#else
            var result = controller.Ok(value);
            result.Formatters = Formatters;
            return result;
#endif
        }

        private static bool IsHtmlRequest(Controller controller)
        {
            var acceptedMediaType = "text/html";
#if !ASPNETCORE
            return controller.Request.Headers.Accept.Any(item => item.MediaType == acceptedMediaType);
#else
            return controller.Request.Headers["Accept"].Any(item => item.Contains(acceptedMediaType));
#endif
        }

        internal static bool IsIntroductionRequest(Controller controller)
        {
            var isIntroductionUrl =
#if !ASPNETCORE
                string.IsNullOrEmpty(controller.Request.RequestUri.Query);
#else
                !controller.Request.QueryString.HasValue;
#endif
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