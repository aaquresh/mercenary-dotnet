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
    		""plugin"" : ""soapui"",
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
