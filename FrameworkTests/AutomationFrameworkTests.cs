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
    public class AutomationFrameworkTests
    {
        /// <summary>
        /// Test Case uses valid json request and verifies that something is returned.
        /// </summary>
        [TestMethod]
        [DeploymentItem(@"\PluginFramework\PluginFramework\FHH-4-1-soapui-project.xml")]
        [DeploymentItem(@"\PluginFramework\PluginFramework\config.json")]
        public void RunTest_SoapUIPlugin_CorrectJsonRequest()
        {
            string jsonResponse = PluginFramework.PluginFramework.RunTest(@"

        {
    		""plugin"" : ""SoapUIPlugin"",
			""parameters"" :
			{
                ""test"" :
                {
                    ""case"" : ""GetToken"",
                    ""suite"" : ""GetSecurityTokens"",
                    ""project"" : ""TestProject1""
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
");
            Assert.AreNotEqual("", jsonResponse);
        }

        /// <summary>
        /// Test Case uses invalid json request and a valid exception is thrown.
        /// </summary>
        [TestMethod]
        [DeploymentItem(@"\PluginFramework\PluginFramework\config.json")]
        public void RunTest_SoapUIPlugin_BadJsonRequest()
        {
            string jsonResponse = PluginFramework.PluginFramework.RunTest(@"

        {
    		""plugin"" : ""SoapUIPlugin"",
			""parameters"" :
			{
                ""test"" :
                
                    ""case"" : ""GetToken"",
                    ""suite"" : ""GetSecurityTokens"",
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
");
            Assert.IsTrue(jsonResponse.Contains("Newtonsoft.Json.JsonReaderException:"));
            
        }

        /// <summary>
        /// Test Case uses valid json request with missing plugin value.
        /// </summary>
        [TestMethod]
        [DeploymentItem(@"\PluginFramework\PluginFramework\config.json")]
        public void RunTest_SoapUIPlugin_CorrectJsonRequest1()
        {
            string jsonResponse = PluginFramework.PluginFramework.RunTest(@"

        {
    		""plugin"" : ""SoapUIPlugin"",
			""parameters"" :
			{
                ""test"" :
                {
                    ""case"" : ""GetToken"",
                    ""suite"" : ""GetSecurityTokens"",
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
");
            Assert.AreNotEqual("", jsonResponse);
        }

        [TestMethod]
        [DeploymentItem(@"\PluginFramework\PluginFramework\config.json")]
        public void PluginFramework_2()
        {
            JsonPluginConfig jpc = new JsonPluginConfig(@"

{
    ""plugin"" : 
    [
        {
       ""namespace"" : ""SoapUIPlugin"",

        ""name"" : ""SoapUIPlugin"",

        ""dllLocation"" : ""C:\\Users\\cmnimnic\\Documents\\GitHub\\mercenary-dotnet\\Plugins\\SoapUIPlugin\\bin\\Debug\\SoapUIPlugin.dll"", 
        
        ""appPath"" : ""C:\\Program Files\\SmartBear\\soapUI-Pro-4.5.2\\bin"" 
        },
    	{
       ""namespace"" : ""RanorexPlugin"",

        ""name"" : ""RanorexPlugin"",

        ""dllLocation"" : ""C:\\Users\\cmnimnic\\Documents\\GitHub\\mercenary-dotnet\\Plugins\\RanorexPlugin\\bin\\Debug\\RanorexPlugin.dll"", 

        ""appPath"" : ""C:\\Program Files\\SmartBear\\soapUI-Pro-4.5.2\\bin"" 
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
                    "log.txt" : "a;sljkdfweoijf23lsfo23jo32 base64 encoding stuff" 
                }
            }
		}
        
        */

    }
}
