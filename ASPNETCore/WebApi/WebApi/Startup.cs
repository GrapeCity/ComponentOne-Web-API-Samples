using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;
using WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.AspNetCore.Http.Features;
using WebApi.Controllers;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Globalization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Localization;
using Google.Apis.Drive.v3;
using Google.Apis.Auth.OAuth2;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Util.Store;
using System;

namespace WebApi
{
    public class Startup
    {
        private DateTime _expiry;
        public Startup(IHostingEnvironment env)
        {
            Environment = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public static IHostingEnvironment Environment { get; set; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc().ConfigureApplicationPartManager(manager =>
            {
                var afp = manager.FeatureProviders.First(iafp => iafp.GetType() == typeof(ControllerFeatureProvider));
                if(afp != null)
                {
                    manager.FeatureProviders.Remove(afp);
                }
                manager.FeatureProviders.Add(new CustomControllerFeatureProvider());
            });

            // CORS support
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAll"));
            });

            services.Configure<FormOptions>(options => options.ValueLengthLimit = int.MaxValue);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseStaticFiles();

            // do not change the name of defaultCulture
            var defaultCulture = "en-US";
            IList<CultureInfo> supportedCultures = new List<CultureInfo>
            {
                new CultureInfo(defaultCulture)
            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(defaultCulture),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseMvc();

            // Anything unhandled
            app.Run(context =>
            {
                var defaultPage = PageTemplate.IsJPCulture ? "/default.ja.html" : "/default.html";
                context.Response.Redirect(context.Request.PathBase + defaultPage);
                return Task.FromResult(0);
            });

            app.UseStorageProviders()
                .AddDiskStorage("ExcelRoot", Path.Combine(env.WebRootPath, "ExcelRoot"))
                .AddDiskStorage("PdfRoot", Path.Combine(env.WebRootPath, "PdfRoot"))
                .AddDiskStorage("LocalDocuments", Path.Combine(env.WebRootPath, "LocalDocuments"));

            var ssrsUrl = Configuration["AppSettings:SsrsUrl"];
            var ssrsUserName = Configuration["AppSettings:SsrsUserName"];
            var ssrsPassword = Configuration["AppSettings:SsrsPassword"];
            app.UseReportProviders()
                .AddFlexReportDiskStorage("ReportsRoot", Path.Combine(env.WebRootPath, "ReportsRoot"))
                .AddSsrsReportHost("c1ssrs", ssrsUrl, new System.Net.NetworkCredential(ssrsUserName, ssrsPassword));

            var oneDriveAccessToken = Configuration["AppSettings:OneDriveAccessToken"];
            app.UseStorageProviders().AddOneDriveStorage("OneDrive", oneDriveAccessToken);

            string[] scopes = { DriveService.Scope.Drive };
            string applicationName = "C1WebApi";

            // Azure storage
            app.UseStorageProviders().AddAzureStorage("Azure", Configuration["AppSettings:AzureStorageConnectionString"]);

	    // please uncomment this line when you want to test GoogleDrive service.
            // Google storage
            //app.UseStorageProviders().AddGoogleDriveStorage("GoogleDrive", GetUserCredential(scopes), applicationName);

            // Dropbox storage
            app.UseStorageProviders().AddDropBoxStorage("DropBox", Configuration["AppSettings:DropBoxStorageAccessToken"], applicationName);

            // AWS storage
            var aWSAccessToken = Configuration["AppSettings:AWSStorageAccessToken"];
            var secretKey = Configuration["AppSettings:AWSStorageSecretKey"];
            var bucketName = Configuration["AppSettings:AWSStorageBucketName"];
            string region = "us-east-1";
            app.UseStorageProviders().AddAWSStorage("AWS", aWSAccessToken, secretKey, bucketName, region);

            app.UseDataProviders()
                .AddItemsSource("Sales", () => Sale.GetData(10).ToList())
                .AddItemsSource("Orders", () => CustomerOrder.GetOrderData(20).ToList())
                .Add("Nwind", new SqlDataProvider(GetConnectionString(env)));

            var dataPath = Path.Combine(env.WebRootPath, "Data");

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

            C1Controller.CacheDataEngineDataKey(app);
        }

        private UserCredential GetUserCredential(string[] scopes)
        {
            UserCredential credential;
            var rootPath =  Environment.ContentRootPath;
            using (var fileStream =
                new FileStream(Path.Combine(rootPath,"credentials.json"), FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(fileStream).Secrets,
                    scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(Path.Combine(rootPath, credPath), true)).Result;
            }
            return credential;
        }

        private string GetConnectionString(IHostingEnvironment env)
        {
            var configConnectionString = Configuration["Data:DefaultConnection:ConnectionString"];
            configConnectionString = configConnectionString.Replace("|DataDirectory|", Path.Combine(env.WebRootPath, "App_Data"));
            return configConnectionString;
        }
    }
}
