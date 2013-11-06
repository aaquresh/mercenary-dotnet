using System;
using System.Reflection;
using System.IO;
using Newtonsoft.Json.Linq;

namespace MercenaryEngine
{
    public class EngineConfig
    {
        private static EngineConfig config;

        private JObject json;
        private string role;
        private int port;
        private string path;
        private string brand;

        private EngineConfig()
        {
            path = System.IO.Path.GetDirectoryName(Assembly.GetAssembly(typeof(EngineConfig)).CodeBase);
            string file = new Uri(path + System.IO.Path.DirectorySeparatorChar + "config.json").LocalPath;

            try
            {
                string data = File.ReadAllText(file);
                json = JObject.Parse(data);
                this.InitializeJson();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void InitializeJson()
        {
            JToken rvalue = json.GetValue("role");
            this.role = ((rvalue != null) && (rvalue.Type == JTokenType.String)) ? rvalue.ToString().ToLower() : "server";

            JToken pvalue = json.GetValue("port");
            this.port = ((pvalue != null) && (pvalue.Type == JTokenType.Integer)) ? (int) pvalue : 1234;

            JToken bvalue = json.GetValue("brand");
            this.brand = ((bvalue != null) && (bvalue.Type == JTokenType.String)) ? bvalue.ToString() : "Mercenary";
        }

        public static EngineConfig GetConfig ()
        {
            if (config == null)
            {
                try
                {
                    config = new EngineConfig();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return config;
        }

        public string GetValue(string key)
        {
            JToken value = json.SelectToken(key);
            if ((value != null) && (value.Type == JTokenType.String))
            {
                return value.ToString();
            }
            return null;
        }

        public JArray GetArray(string key)
        {
            JToken value = json.SelectToken(key);
            if ((value != null) && (value.Type == JTokenType.Array))
            {
                JArray.Parse(value.ToString());
            }
            return null;
        }

        public string Role
        {
            get { return this.role; }
        }

        public string Path
        {
            get { return this.path; }
        }

        public string Brand
        {
            get { return this.brand; }
        }

        public int Port
        {
            get { return this.port; }
        }
    }
}
