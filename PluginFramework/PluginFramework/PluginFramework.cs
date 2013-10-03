using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Interfaces;
using Newtonsoft.Json.Linq;
using System.Reflection;
//using SoapUIPlugin;

namespace PluginFramework
{
    public static class PluginFramework
    {
        static public string RunTest(string Json)
        {
            string pluginName;
            string pluginPath;
            string pluginNamespace;

            //Pull test tool info from json config string
            try
            {
                JObject o = JObject.Parse(Json);

                string testTool = (string)o["plugin"];
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            //Pull test tool info from local config file

            try
            {
                StreamReader sr = new StreamReader("config.json");

                string config = sr.ReadToEnd();

                JObject jConfig = JObject.Parse(config);

                //Need to fix this so that it cycles through possible plugins


                pluginName = (string)jConfig["plugin"][0]["name"];
                pluginPath = (string)jConfig["plugin"][0]["dllLocation"];
                pluginNamespace = (string)jConfig["plugin"][0]["namespace"];

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            Assembly assembly = Assembly.LoadFrom(pluginPath);

            Type type = assembly.GetType(pluginNamespace + "." + pluginName);

            IAbstractTestToolFactory instance = Activator.CreateInstance(type) as IAbstractTestToolFactory;

            IAbstractTestCaseProduct tcp = instance.CreateTestCase(Json);

            return tcp.Run();
        }
    }
}
