using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace MercenaryEngine
{
    public abstract class Remote
    {
        protected string host = "";
        protected string port = "";
        protected bool available = false;

        public bool Ping()
        {
            return true;
        }

        public string Id
        {
            get { return this.host + ":" + this.port; }
        }

        public string Host
        {
            get { return this.host; }
        }

        public string Port
        {
            get { return port; }
        }

        public bool IsAvailable
        {
            get { return this.available; }
        }

        public static string MakeId(JObject json)
        {
            string host = "";
            string port = "";

            JToken target = json["target"];
            if (target != null)
            {
                JToken hvalue = target["host"];
                host = (hvalue != null) ? hvalue.ToString() : "";

                JToken pvalue = target["port"];
                port = (pvalue != null) ? pvalue.ToString() : "1234";
            }

            return host + ":" + port;
        }

        public abstract void Retire ();
    }
}
