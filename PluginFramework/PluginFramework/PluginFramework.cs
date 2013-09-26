using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            //Pull test tool info from json config string
            try
            {
                JObject o = JObject.Parse(Json);


                string testTool = (string)o["tasks"][0]["plugin"];



            }
            catch (Exception ex)
            {
                return ex.ToString();
            }


            Assembly assembly = Assembly.LoadFrom(@"C:\Users\cmnimnic\Documents\GitHub\mercenary-dotnet\Plugins\SoapUIPlugin\bin\Debug\SoapUIPlugin.dll");

            Type type = assembly.GetType("SoapUIPlugin.SoapUIPlugin");

            IAbstractTestToolFactory instance = Activator.CreateInstance(type) as IAbstractTestToolFactory;

            IAbstractTestCaseProduct tcp = instance.CreateTestCase(Json);

            tcp.Run();




            return "";
        }
    }
}
