using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace MercenaryEngine
{
    public class Task
    {
        public enum Status { };

        protected string plugin;
        protected string os;
        protected string version;

        protected JToken json;
        
        protected Dictionary<string, string> dependencies;

        public Task(JToken json)
        {
            this.dependencies = new Dictionary<string, string>();

            JToken plugin = json["plugin"];
            this.plugin = (plugin != null) ? plugin.ToString() : "";

            JToken environment = json["environment"];
            if (environment != null)
            {
                JToken os = json["os"];
                this.os = (os != null) ? os.ToString() : "";

                JToken version = json["version"];
                this.version = (version != null) ? version.ToString() : "";
            }

            JToken dependencies = json["dependencies"];
            if (dependencies != null)
            {
                foreach (JToken dependency in dependencies.Children())
                {
                    if (dependency is JProperty)
                    {
                        var application = dependency as JProperty;
                        this.dependencies.Add(application.Name.ToString(), application.Value.ToString());
                    }
                }
            }

            this.json = json;
        }

        public string Plugin
        {
            get { return this.plugin; }
        }

        public string OS
        {
            get { return this.os; }
        }

        public string Version
        {
            get { return this.version; }
        }

        public JToken Json
        {
            get { return json;  }
        }

        public Dictionary<string, string> Dependencies
        {
            get { return this.dependencies; }
        }
    }

    
}
