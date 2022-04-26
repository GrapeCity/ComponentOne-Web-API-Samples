using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiExplorer.Models
{
    public class GridExportImportOptions : ClientSettingsModel
    {
        public bool NeedExport { get; set; }
        public bool NeedImport { get; set; }
        public bool? IncludeColumnHeaders { get; set; }
        public bool? OnlyCurrentPage { get; set; }
    }
}