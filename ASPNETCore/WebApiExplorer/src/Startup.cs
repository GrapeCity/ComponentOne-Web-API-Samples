using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApiExplorer.Models;
using System.Linq;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using System.Collections.Generic;

#if NETCORE31
using Microsoft.Extensions.Hosting;
#endif

namespace WebApiExplorer
{
    public class Startup
    {
#if NETCORE31
        public static IWebHostEnvironment Environment { get; set; }
#else
        public static IHostingEnvironment Environment { get; set; }
#endif

        public IConfigurationRoot Configuration { get; set; }
#if NETCORE31
        public Startup(IWebHostEnvironment env)
#else
        public Startup(IHostingEnvironment env)
#endif
        {
            Environment = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            C1.Web.Mvc.LicenseManager.Key = "c1v01S8KmxqTFv8K7xbZDw5lvw7fDqMSfwrTEq8KHw5DDnsOOxLXCucWAxLvDo8SixLYxw4A5w5vDn8K1VcOQxI7EoMKUxK/CkMKnxYhSxLHCq8SmwpTFp2bEqMKOfcKhxYx5w7HEj8K8w5/DucS7w5TDlMO0xLrFhMSnw6XDqcO8xL3FiMOLxJFrwrRCxazCk8SWwqDEqsOnxY7DgsShw7PDpsKgwpPEuMK0w65wwqrDq8SLxYXEgMOXxYfCk8K8xYjDpsKhwrDCvMWcVGvDvcObw6zDm8O1xKTDkMOewojDgldJf8SIWGbCtcKmxLHDu3HEnMS4xL/Fq8SYxaHEscSExZfDqw==";
            C1.Web.Mvc.Olap.LicenseManager.Key = "c1v01Q8WMxo/FtsSIxJjEpsKhxIfCgcSgxZTFgcS3woXDs8KRxIjEj3jCgcOJccK7wrrCqcK+xIkxxKHEi8SdwqHDusWMw4lQwqTCucSQw4zEnsS0xJ/CisKSxLHEn8KSxJnFj8KCxIzDqsS7xYbClcScxL0/xJvDiMOZwo3CucOubsWHwq7EgMOSxIvEl8S1xLDFgVvEvsWnxKnFoMKIxaLEoHjCnMSdUMSGZ8WLdcOnw6zEn8KbxIrCrsKLw6VRxIlsw7jCv17CisSWwqnErMWCdXQ0w7HEvsS2wojDrsOfw6TCqMKww43DqcK7xajCiMSBxJPEj8OtesWKesO7xZ/Cg8KIw53EmsWpeMKQwoQ=";
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<C1NWindEntities>(o => o.UseSqlite(GetConnectionString()));
            services.AddMvc();
            services.AddSession();
        }

        private string GetConnectionString()
        {
            var configConnectionString = Configuration["Data:DefaultConnection:ConnectionString"];
            const char folderSeparator = '\\';
            var dataFolderPath = Environment.WebRootPath.Replace('/', folderSeparator);
            if (dataFolderPath.Last() != folderSeparator)
            {
                dataFolderPath += folderSeparator;
            }

            var dataSourceText = "data source=";
            var index = configConnectionString.IndexOf(dataSourceText);
            if (index != -1)
            {
                return configConnectionString.Insert(index + dataSourceText.Length, dataFolderPath);
            }

            return configConnectionString;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
#if NETCORE31
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
#else
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
#endif
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseSession();

#if NETCORE31
            app.UseRouting();
#endif

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

#if NETCORE31
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Validation",
                    pattern: "{control}/UnobtrusiveValidation",
                    defaults: new { controller = "Validation", action = "Register" },
                    constraints: new { control = "(AutoComplete)|(ComboBox)|(MultiSelect)|(^Input.*)|(MultiAutoComplete)" }
                );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });
#else
            app.UseMvc(r => {
                r.MapRoute(
                    name: "Validation",
                    template: "{control}/UnobtrusiveValidation",
                    defaults: new { controller = "Validation", action = "Register" },
                    constraints: new { control = "(AutoComplete)|(ComboBox)|(MultiSelect)|(^Input.*)|(MultiAutoComplete)" }
                );

                r.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
#endif
        }
    }
}
