using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PluginFramework;
using Newtonsoft.Json.Linq;

namespace FrameworkTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [DeploymentItem(@"\FrameworkTests\config.json")]
        public void PluginFramework_1()
        {
            Assert.IsTrue("" == PluginFramework.PluginFramework.RunTest(@"

        {
    		""plugin"" : ""SoapUIPlugin"",
			""parameters"" :
			{
                ""test"" :
                {
                    ""case"" : ""getRepresentation TestCase_Grid"",
                    ""suite"" : ""CPOECommonHDDServiceSoap11Binding TestSuite"",
                    ""project"" : ""TestProject""
                },
                ""options"" : 
                {
                    ""ExportAllResults"" : ""true""
                }
			},
            ""results"" :
            {
                ""outcome"" : """",
                ""message"" : """",
                ""attachments"" :
                {
                }
            }
		}
"));
            
        }



        [TestMethod]
        [DeploymentItem(@"\FrameworkTests\config.json")]
        public void PluginFramework_2()
        {
            JsonPluginConfig jpc = new JsonPluginConfig(@"

{
    ""plugin"" : 
    [
        {
       ""namespace"" : ""SoapUIPlugin"",

        ""name"" : ""SoapUIPlugin"",

        ""dllLocation"" : ""C:\\Users\\cmnimnic\\Documents\\GitHub\\mercenary-dotnet\\Plugins\\SoapUIPlugin\\bin\\Debug\\SoapUIPlugin.dll"" 
        },
    	{
       ""namespace"" : ""RanorexPlugin"",

        ""name"" : ""RanorexPlugin"",

        ""dllLocation"" : ""C:\\Users\\cmnimnic\\Documents\\GitHub\\mercenary-dotnet\\Plugins\\RanorexPlugin\\bin\\Debug\\RanorexPlugin.dll"" 
        }
    ],
	""SoapUIProjects"" :
	[
		{
			""TestProject"" : ""C:\\Users\\cmnimnic\\Desktop\\FHH-4-1-soapui-project.xml""
		}
	]
    
}
            ");

            

        }
        /*

        {
			"plugin" : "soapui",
			"parameters" :
			{
                "test" :
                {
                    "case" : "",
                    "suite" : "",
                    "project" : ""
                }
                "options" : 
                {
                    "ExportAllResults" : ""
                }
			}
            "results" :
            {
                "outcome" : "",
                "attachments" 
                {
                    "log.txt" : "a;sljkdfweoijf23lsfo23jo32 base64 encoding crap" 
                }
            }
		}
        
        */

    }
}
