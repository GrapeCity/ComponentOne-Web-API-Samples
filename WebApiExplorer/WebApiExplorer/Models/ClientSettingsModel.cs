using System.Collections.Generic;

namespace WebApiExplorer.Models
{
    public class ClientSettingsModel
    {

        public virtual string ControlId
        {
            get { return "DemoControl"; }
        }

        public IDictionary<string, object[]> Settings { get; set; }

        public IDictionary<string, object> DefaultValues { get; set; }

        public IDictionary<string, string> HeaderNames { get; set; }
    }
}