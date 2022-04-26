using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using System.Web.Http;
using System.Configuration;

[assembly: OwinStartupAttribute(typeof(ExcelToJSON.Startup))]
namespace ExcelToJSON
{
    public partial class Startup
    {
		private readonly HttpConfiguration config = GlobalConfiguration.Configuration;
		public void Configuration(IAppBuilder app)
        {
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
