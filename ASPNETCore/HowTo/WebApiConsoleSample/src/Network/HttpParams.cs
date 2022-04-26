using System;
using System.Collections.Generic;

namespace WebApiConsoleSample
{

    public class HttpParams
    {
        private Dictionary<string, string> Params = new Dictionary<string, string>();

        public HttpParams()
        {
        }

        public HttpParams Set(string key, string value)
        {
            if(key == null || key.Length == 0) return this;
            if (Params.ContainsKey(key))
            {
                Params[key] = Uri.EscapeDataString(value);
            }
            else
            {
                Params.Add(key, Uri.EscapeDataString(value));
            }
            return this;
        }
        public HttpParams Append(string key, string value)
        {
            if(key == null || key.Length == 0) return this;
            if (Params.ContainsKey(key))
            {
                Params[key] += String.Format("&{0}={1}", key, Uri.EscapeDataString(value));
            }
            else
            {
                Params.Add(key, Uri.EscapeDataString(value));
            }
            return this;
        }

        public override string ToString()
        {
            string prms = "";

            foreach (KeyValuePair<string, string> kvp in Params)
            {
                prms += String.Format("{0}{1}={2}", prms.Length == 0 ? "" : "&", kvp.Key, kvp.Value);
            }
            return prms;
        }
    }
}