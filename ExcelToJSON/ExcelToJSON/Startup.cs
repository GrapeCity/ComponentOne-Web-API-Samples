using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using System.Web.Http;
using System.Configuration;
using System.Net;

[assembly: OwinStartupAttribute(typeof(ExcelToJSON.Startup))]
namespace ExcelToJSON
{
    public partial class Startup
    {
		private readonly HttpConfiguration config = GlobalConfiguration.Configuration;
		public void Configuration(IAppBuilder app)
        {
			// C1WEB-29052: Adding more the old SSL protocols explicitly to avoid error of HTTP requests in some cases.
			ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

			app.UseCors(CorsOptions.AllowAll);
			ConfigureAuth(app);

			#region Storage registration
			string applicationName = "C1WebApi";

			// Dropbox storage
			app.UseStorageProviders().AddDropBoxStorage("DropBox", ConfigurationManager.AppSettings["DropBoxStorageAccessToken"], applicationName);
            

			#endregion
		}
	}
}
