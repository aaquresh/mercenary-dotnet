using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace MercenaryEngine
{
    public class RemoteServer : Remote
    {
        public RemoteServer(JObject json)
        {
            this.Initialize(json);
        }

        protected void Initialize(JObject json)
        {
        }

        public override void Retire()
        {
            throw new NotImplementedException();
        }
    }
}
