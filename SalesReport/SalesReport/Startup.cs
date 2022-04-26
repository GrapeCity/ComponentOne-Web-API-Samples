using System;
using Microsoft.Owin;
using Owin;
using System.IO;

[assembly: OwinStartup(typeof(SalesReport.Startup))]

namespace SalesReport
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            app.UseReportProviders().AddFlexReportDiskStorage("",
                Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                @"content\reports")));
        }
    }
}
