using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebApiConsoleSample
{
    public class ConfigService
    {
        private Config _config = new Config();
        
        private ConfigService()
        {}

        private static ConfigService _instance = null;

        public static ConfigService Instance()
        {
            if (_instance == null)
            {
                _instance = new ConfigService();
                _instance.LoadConfig();
            }
            return _instance;
        }

        private void LoadConfig()
        {
            try
            {
                string filepath = "./assets/config.json";
                string result = string.Empty;
                using (StreamReader r = new StreamReader(filepath))
                {
                    var json = r.ReadToEnd();
                    var jobj = JObject.Parse(json);
                    result = jobj.ToString();
                    _config = JsonConvert.DeserializeObject<Config>(json);
                }
            }
            catch (Exception exception)
            {
                _config = new Config();
                Console.WriteLine(exception.Message);
            }
        }

        public string WebApiServiceUrl
        {
            set
            {
                this._config.WebApiServiceUrl = value;
            }
            get
            {
                return this._config.WebApiServiceUrl;
            }
        }
    }
}