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
        public void PluginFramework_1()
        {
            Assert.IsTrue("" == PluginFramework.PluginFramework.RunTest(@"{
	""message"" : ""tasks"",

	""tasks"" :
	[
		{
			""plugin"" : ""soapui"",
			""environment"" :
			{
				""os"" : ""windows"",
				""version"" : 7
			},

			""dependencies"" :
			[
				{
					""product"" : ""ECIS Desktop"",
					""version"" : ""5.1.2""
				},
				{
					""product"" : ""cPOE"",
					""version"" : ""2.7""
				}
			],

			""install"" :
			[
				{
				}
			],

			""parameters"" :
			{
				""foo"" : [ ""parameters"", ""can"", ""be"", ""arrays"" ],
				""bar"" : { ""canBeObject"" : true },
				""baz"" : ""or maybe just a string""
			}
		}
	]
}"));
            
        }
    }
}
