using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace MercenaryEngine
{
    public class RemoteTarget : Remote
    {
        protected string os;
        protected string version;
        protected Dictionary<string, string> plugins;
        protected Dictionary<string, string> applications;

        public RemoteTarget(JObject json)
        {
            this.os = "";
            this.version = "";
            this.plugins = new Dictionary<string, string>();
            this.applications = new Dictionary<string, string>();

            this.Initialize(json);
        }

        protected void Initialize(JObject json)
        {
            JToken tokentarget = json["target"];
            if (tokentarget != null)
            {
                JToken tokenhost = tokentarget["host"];
                this.host = (tokenhost != null) ? tokenhost.ToString() : null;

                JToken tokenport = tokentarget["port"];
                this.port = (tokenport != null) ? tokenport.ToString() : "1234";

                JToken tokenos = tokentarget["os"];
                this.os = (tokenos != null) ? tokenos.ToString() : null;

                JToken tokenversion = tokentarget["version"];
                this.version = (tokenversion != null) ? tokenversion.ToString() : "";
            }

            JToken pluginstoken = json["plugins"];
            if (pluginstoken != null)
            {
                foreach (JToken plugintoken in pluginstoken.Children())
                {
                    if (plugintoken is JProperty)
                    {
                        var plugin = plugintoken as JProperty;
                        plugins.Add(plugin.Name.ToString(), plugin.Value.ToString());
                    }
                }
            }

            JToken appstoken = json["applications"];
            if (appstoken != null)
            {
                foreach (JToken apptoken in appstoken.Children())
                {
                    if (apptoken is JProperty)
                    {
                        var application = apptoken as JProperty;
                        applications.Add(application.Name.ToString(), application.Value.ToString());
                    }
                }
            }
        }

        public string OS
        {
            get { return this.os; }
        }

        public string Version
        {
            get { return this.version; }
        }

        public bool CanCompleteTask(Task task)
        {
            bool result = false;

            if ((plugins.ContainsKey(task.Plugin)) && (this.os.ToLower() == task.OS.ToLower()) && (this.version == task.Version))
            {
                result = true;
                foreach (KeyValuePair<string, string> entry in task.Dependencies)
                {
                    result = ((applications.ContainsKey(entry.Key)) && (applications[entry.Key] == entry.Value));
                    if (!result) { break; }
                }
            }

            return result;
        }

        public override void Retire()
        {
            throw new NotImplementedException();
        }
    }
}
