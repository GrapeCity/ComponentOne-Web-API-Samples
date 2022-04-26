using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.Mvc;

namespace ExcelToJSON.Models
{
    public class ClientSettingsModel
    {
        public virtual string ControlId
        {
            get { return "DemoControl"; }
        }

        public IDictionary<string, object[]> Settings { get; set; }

        public IDictionary<string, object> DefaultValues { get; set; }
    }

}