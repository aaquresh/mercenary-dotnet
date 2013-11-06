using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using Newtonsoft.Json.Linq;

namespace MercenaryEngine
{
    public class EngineConfig
    {
        private static EngineConfig config;

        private Dictionary<string, JToken> plugins;
        private JObject json;
        private string role;
        private int port;
        private string path;
        private string brand;
        private string os;
        private string version;
        private string server;

        private EngineConfig()
        {
            this.plugins = new Dictionary<string, JToken>();

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
            JToken tokenrole = json["role"];
            this.role = ((tokenrole != null) && (tokenrole.Type == JTokenType.String)) ? tokenrole.ToString().ToLower() : "server";

            if (this.role.Equals("target"))
            {
                JToken tokenserver = json["server"];
                this.server = (tokenserver != null) ? tokenserver.ToString() : null;
            }

            JToken tokenport = json["port"];
            this.port = ((tokenport != null) && (tokenport.Type == JTokenType.Integer)) ? (int) tokenport : 1234;

            JToken tokenbrand = json["brand"];
            this.brand = ((tokenbrand != null) && (tokenbrand.Type == JTokenType.String)) ? tokenbrand.ToString() : "Mercenary";

            JToken tokenos = json["os"];
            this.os = (tokenos != null) ? tokenos.ToString() : null;

            JToken tokenversion = json["version"];
            this.version = (tokenversion != null) ? tokenversion.ToString() : "";

            JToken pluginstoken = json["plugins"];
            if (pluginstoken != null)
            {
                foreach (JToken plugintoken in pluginstoken.Children())
                {
                    if (plugintoken is JProperty)
                    {
                        var plugin = plugintoken as JProperty;
                        plugins.Add(plugin.Name.ToString(), plugin.Value as JToken);
                    }
                }
            }

            return;
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

        public JToken GetValue(string key)
        {
            JToken value = json[key];
            if (value != null)
            {
                return value;
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

        public string OS
        {
            get { return this.os + " " + this.version; }
        }

        public string Server
        {
            get { return this.server; }
        }

        public JObject Json
        {
            get { return this.json; }
        }

        public Dictionary<string, JToken> Plugins
        {
            get { return this.plugins; }
        }

        public JToken GetPlugin(string key)
        {
            return plugins[key];
        }
    }
}
