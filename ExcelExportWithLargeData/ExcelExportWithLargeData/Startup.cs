using System;
using Microsoft.Owin;
using Owin;
using System.IO;
using ExcelExportWithLargeData.Models;

[assembly: OwinStartup(typeof(ExcelExportWithLargeData.Startup))]

namespace ExcelExportWithLargeData
{
    public class Startup
    {
        public object Product { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            app.UseDataProviders().AddItemsSource("orders", () => Order.All);
            app.UseStorageProviders().AddDiskStorage("files",
                Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "files")));
        }
    }
}
