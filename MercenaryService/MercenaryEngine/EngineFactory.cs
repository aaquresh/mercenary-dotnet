using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MercenaryEngine
{
    public class EngineFactory
    {
        public static Engine CreateInstance()
        {
            EngineConfig config = EngineConfig.GetConfig();
            Engine engine;

            switch (config.Role)
            {
                case "server":
                    engine = new ServerEngine();
                    break;
                default:
                    engine = new TargetEngine();
                    break;
            }

            return engine;
        }
    }
}
