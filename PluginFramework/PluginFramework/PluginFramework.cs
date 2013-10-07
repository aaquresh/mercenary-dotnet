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
            string pluginName = "";
            string pluginPath = "";
            string pluginNamespace = "";

            string testTool;

            //Pull test tool info from json config string
            try
            {
                JObject o = JObject.Parse(Json);

                testTool = (string)o["plugin"];
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

                JsonPluginConfig jpc = new JsonPluginConfig(config);
                foreach (JsonPlugin jp in jpc)
                {
                    if (jp.Name == testTool)
                    {
                        pluginName = jp.Name;
                        pluginPath = jp.Path;
                        pluginNamespace = jp.NamespacePath;
                    }
                }

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
