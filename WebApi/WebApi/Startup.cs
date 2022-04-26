using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Collections.Generic;
using System.IO;
using WebApi.Models;
using System.Configuration;
using C1.Web.Api.Image;
using C1.Web.Api.BarCode;
using C1.Web.Api.Storage;
using C1.Web.Api.DataEngine;
using C1.Web.Api.Excel;
using C1.Web.Api.Report;
using C1.Web.Api.Pdf;
using WebApi.Controllers;
using Google.Apis.Auth.OAuth2;
using System.Web;
using System.Threading;
using Google.Apis.Util.Store;
using Google.Apis.Drive.v3;

[assembly: OwinStartup(typeof(WebApi.Startup))]

namespace WebApi
{
    /// <summary>
    /// Owin startup class.
    /// </summary>
    public class Startup
    {
        private readonly HttpConfiguration config = GlobalConfiguration.Configuration;
        private DateTime _expiry;

        public void Configuration(IAppBuilder app)
        {
            // CORS supports - does not apply to <form>
            app.UseCors(CorsOptions.AllowAll);

            // Web Api
            RegisterRoutes(config);
            app.UseWebApi(config);

            // Anything unhandled
            app.Run(context =>
            {
                var defaultPage = PageTemplate.IsJpCulture ? "/default.ja.html" : "/default.html";
                context.Response.Redirect(context.Request.PathBase + defaultPage);
                return Task.FromResult(0);
            });

            app.UseStorageProviders()
                .AddDiskStorage("ExcelRoot", GetFullRoot("ExcelRoot"))
                .AddDiskStorage("PdfRoot", GetFullRoot("PdfRoot"))
                .AddDiskStorage("LocalDocuments", GetFullRoot("LocalDocuments"));

            var ssrsUrl = ConfigurationManager.AppSettings["SsrsUrl"];
            var ssrsUserName = ConfigurationManager.AppSettings["SsrsUserName"];
            var ssrsPassword = ConfigurationManager.AppSettings["SsrsPassword"];
            app.UseReportProviders()
                .AddFlexReportDiskStorage("ReportsRoot", GetFullRoot("ReportsRoot"))
                .AddSsrsReportHost("c1ssrs", ssrsUrl, new System.Net.NetworkCredential(ssrsUserName, ssrsPassword));

      #region Storage registration
            string[] scopes = { DriveService.Scope.Drive };
            string applicationName = "C1WebApi";

            // Ondrive storage
            var oneDriveAccessToken = ConfigurationManager.AppSettings["OneDriveAccessToken"];
            app.UseStorageProviders().AddOneDriveStorage("OneDrive", oneDriveAccessToken);

            // Azure storage
            app.UseStorageProviders().AddAzureStorage("Azure", ConfigurationManager.AppSettings["AzureStorageConnectionString"]);

	    // please uncomment this line when you want to test GoogleDrive service.
            // Google storage
            //app.UseStorageProviders().AddGoogleDriveStorage("GoogleDrive", GetUserCredential(scopes), applicationName);

            // Dropbox storage
            app.UseStorageProviders().AddDropBoxStorage("DropBox", ConfigurationManager.AppSettings["DropBoxStorageAccessToken"], applicationName);

            // AWS storage
            var aWSAccessToken = ConfigurationManager.AppSettings["AWSStorageAccessToken"];
            var secretKey = ConfigurationManager.AppSettings["AWSStorageSecretKey"];
            var bucketName = ConfigurationManager.AppSettings["AWSStorageBucketName"];
            string region = "us-east-1";
            app.UseStorageProviders().AddAWSStorage("AWS", aWSAccessToken, secretKey, bucketName, region);

            #endregion

            app.UseDataProviders()
                      .AddItemsSource("Sales", () => Sale.GetData(10).ToList())
                      .AddItemsSource("Orders", () => CustomerOrder.GetOrderData(20).ToList())
                      .Add("Nwind", new SqlDataProvider(ConfigurationManager.ConnectionStrings["Nwind"].ConnectionString));

            app.UseDataEngineProviders()
                .AddDataEngine("complex10", () =>
                {
                    return ProductData.GetData(100000);
                })
                .AddDataEngine("complex50", () =>
                {
                    return ProductData.GetData(500000);
                })
                .AddDataEngine("complex100", () =>
                {
                    return ProductData.GetData(1000000);
                })
                .AddDataSource("dataset10", () => ProductData.GetData(100000).ToList())
                .AddDataSource("dataset50", () => ProductData.GetData(500000).ToList())
                .AddDataSource("dataset100", () => ProductData.GetData(1000000).ToList())
                .AddCube("cube",
                    @"Data Source=http://ssrs.componentone.com/OLAP/msmdpump.dll;Provider=msolap;Initial Catalog=AdventureWorksDW2012Multidimensional",
                    "Adventure Works");

            CacheDataEngineDataKey(app);
        }

    private UserCredential GetUserCredential(string[] scopes)
        {
          UserCredential credential;

          using (var fileStream =
              new FileStream(HttpContext.Current.Server.MapPath("credentials.json"), FileMode.Open, FileAccess.Read))
              {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(fileStream).Secrets,
                    scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(HttpContext.Current.Server.MapPath(credPath), true)).Result;
              }
              return credential;
        }

        private void CacheDataEngineDataKey(IAppBuilder app)
        {
            var providerManager = app.UseDataEngineProviders();
            foreach (var key in providerManager.Items.Keys)
            {
                C1Controller.DataEngineKeys.Add(new DataEngineKey
                {
                    Name = key
                });
            }
        }

        private static string GetFullRoot(string root)
        {
            var applicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            var fullRoot = Path.GetFullPath(Path.Combine(applicationBase, root));
            if (!fullRoot.EndsWith(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal))
            {
                // When we do matches in GetFullPath, we want to only match full directory names.
                fullRoot += Path.DirectorySeparatorChar;
            }
            return fullRoot;
        }

        private static void RegisterRoutes(HttpConfiguration config)
        {
            // Use APIs defined in the C1 library
            config.Services.Replace(typeof(IAssembliesResolver), new CustomAssembliesResolver());

            // Attribute Routes
            config.MapHttpAttributeRoutes(new CustomDirectRouteProvider());

            // default route
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Services.Replace(typeof(IHttpControllerTypeResolver), new ReportsControllerTypeResolver());
        }
    }

    internal class CustomAssembliesResolver : DefaultAssembliesResolver
    {
        public override ICollection<System.Reflection.Assembly> GetAssemblies()
        {
            var assemblies = base.GetAssemblies();
            var customControllersAssembly = typeof(ExcelController).Assembly;
            if (!assemblies.Contains(customControllersAssembly))
            {
                assemblies.Add(customControllersAssembly);
            }

            customControllersAssembly = typeof(ReportController).Assembly;
            if (!assemblies.Contains(customControllersAssembly))
            {
                assemblies.Add(customControllersAssembly);
            }

            customControllersAssembly = typeof(ImageController).Assembly;
            if (!assemblies.Contains(customControllersAssembly))
            {
                assemblies.Add(customControllersAssembly);
            }

            customControllersAssembly = typeof(BarCodeController).Assembly;
            if (!assemblies.Contains(customControllersAssembly))
            {
                assemblies.Add(customControllersAssembly);
            }

            customControllersAssembly = typeof(StorageController).Assembly;
            if (!assemblies.Contains(customControllersAssembly))
            {
                assemblies.Add(customControllersAssembly);
            }

            customControllersAssembly = typeof(DataEngineController).Assembly;
            if (!assemblies.Contains(customControllersAssembly))
            {
                assemblies.Add(customControllersAssembly);
            }

            customControllersAssembly = typeof(PdfController).Assembly;
            if (!assemblies.Contains(customControllersAssembly))
            {
                assemblies.Add(customControllersAssembly);
            }

            return assemblies;
        }
    }
}