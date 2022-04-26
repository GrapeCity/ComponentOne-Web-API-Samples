using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using C1.AspNetCore.Api;
using C1.Web.Api.Visitor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
#if !NETCORE31
using Microsoft.AspNetCore.Mvc.Cors.Internal;
#endif
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApi.Controllers;
using WebApi.Models;
#if NETCORE31
using Microsoft.Extensions.Hosting;
#endif
using System.Data.Common;

namespace WebApi
{
	public class Startup
	{
#if NETCORE31
		public Startup(IWebHostEnvironment env, IConfiguration config)
#else
		public Startup(IHostingEnvironment env, IConfiguration config)
#endif
		{
			Environment = env;
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();
			Configuration = builder.Build();
		}
#if NETCORE31
		public static IWebHostEnvironment Environment { get; set; }
#else
		public static IHostingEnvironment Environment { get; set; }
#endif

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{

			services.AddMvc()
#if !NETCORE31
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
#endif
                ;

            // Add framework services.
            services.AddMvc().ConfigureApplicationPartManager(manager =>
            {
                var afp = manager.FeatureProviders.First(iafp => iafp.GetType() == typeof(ControllerFeatureProvider));
                if (afp != null)
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

#if NETCORE31
			// Add framework services.
			services.AddMvc(option => { option.EnableEndpointRouting = false; });
			services.AddC1Api();
#else
			services.Configure<MvcOptions>(options =>
			{
				options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAll"));
			});
#endif
			services.Configure<FormOptions>(options => options.ValueLengthLimit = int.MaxValue);

			services.ConfigureVisitor(builder =>
			{
				builder.UseGoogleMapLocation(Configuration["AppSettings:GoogleMapApiKey"]);
			});

            //services.ConfigureVisitor(builder =>
            //{
            //    builder.UseIp2Location(Configuration.GetConnectionString("IP2LocationConnectionString"));
            //});

            //services.ConfigureVisitor(builder =>
            //{
            //    builder.UseWithoutLocationProvider();
            //});

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConfiguration(Configuration.GetSection("Logging"));
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
            });
        }

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
#if NETCORE31
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
#else
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
#endif
		{
#if NETCORE31
			app.UseC1Api();
#endif
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

			app.UseStorageProviders()
			.AddDiskStorage("ExcelRoot", Path.Combine(env.WebRootPath, "ExcelRoot"))
			.AddDiskStorage("PdfRoot", Path.Combine(env.WebRootPath, "PdfRoot"));

			var ssrsUrl = Configuration["AppSettings:SsrsUrl"];
			var ssrsUserName = Configuration["AppSettings:SsrsUserName"];
			var ssrsPassword = Configuration["AppSettings:SsrsPassword"];
			app.UseReportProviders()
				.AddFlexReportDiskStorage("ReportsRoot", Path.Combine(env.WebRootPath, "ReportsRoot"))
				.AddSsrsReportHost("c1ssrs", ssrsUrl, new System.Net.NetworkCredential(ssrsUserName, ssrsPassword));
			DbProviderFactories.RegisterFactory("System.Data.Sqlite", System.Data.SQLite.SQLiteFactory.Instance);
			System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
			System.AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Path.Combine(env.WebRootPath, "App_Data"));

			var clientId = Configuration["AppSettings:ClientId"];
			var username = Configuration["AppSettings:UserName"];
			var password = Configuration["AppSettings:Password"];
			app.AddOneDriveStorage("OneDrive", clientId, "onedrive.readonly", username, password);

			string applicationName = "C1WebApi";

			// Dropbox storage
			app.UseStorageProviders().AddDropBoxStorage("DropBox", Configuration["AppSettings:DropBoxStorageAccessToken"], applicationName);

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
				;

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(Configuration.GetSection("MessageResponse").Value);
            });
            C1Controller.CacheDataEngineDataKey(app);
        }

#if NETCORE31
		private string GetConnectionString(IWebHostEnvironment env)
#else
		private string GetConnectionString(IHostingEnvironment env)
#endif
		{
			var configConnectionString = Configuration["Data:DefaultConnection:ConnectionString"];
			configConnectionString = configConnectionString.Replace("|DataDirectory|", Path.Combine(env.WebRootPath, "App_Data"));
			return configConnectionString;
		}
	}
}
