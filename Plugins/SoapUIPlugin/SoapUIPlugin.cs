using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interfaces;
using System.Configuration;
using System.IO;
using Newtonsoft.Json.Linq;
using PluginFramework;

namespace SoapUIPlugin
{
    public class SoapUIPlugin : IAbstractTestToolFactory
    {
        public IAbstractTestCaseProduct CreateTestCase(string json)
        {
            //Instantiates RunSoapUI object, passing in a json string
            IAbstractTestCaseProduct absTestCaseProduct = new RunSoapUI(json);
            return absTestCaseProduct;
        }
    }
    public class RunSoapUI : IAbstractTestCaseProduct
    {
        private string jsonTestFile;

        public RunSoapUI(string json)
        {
            this.jsonTestFile = json;
        }

        public string Run()
        {
            //Declare variables for json config file

            string testTool = "SoapUIPlugin";
            string appPath;
            string testSuite;
            string testCase;
            string testProjectPath;
            bool exportAllTestResults;
            string environment;

            try
            {
                JObject jTestFile = JObject.Parse(jsonTestFile);

                StreamReader sr = new StreamReader("config.json");

                string config = sr.ReadToEnd();

                JsonPluginConfig jpc = new JsonPluginConfig(config);
                foreach (JsonPlugin jp in jpc)
                {
                    if (jp.Name == testTool)
                    {
                        appPath = jp.AppPath;
                    }
                }

                testSuite = "";
                testCase = "";
                testProjectPath = "";
                exportAllTestResults = true;
                environment = "";




            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            //Create cmd text

            /*
            •	C:\Program Files\SmartBear\soapUI-Pro-4.5.1\bin> testrunner.bat -s"CPOECommonHDDServiceSoap11Binding TestSuite" -c"getRepresentation TestCase_Grid" -a -EDefault -I C:\___TRANING_SOAPUI\DEMO-soapui-project.xml
            */

            string strCmdText = strCmdText = @"-sGetSecurityTokens -cGetToken -a -EDefault -I C:\Users\cmnimnic\Desktop\FHH-4-1-soapui-project.xml >C:\share\test.log";


            //Create process pass required args
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();

            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;

            startInfo.Arguments = strCmdText;

            startInfo.FileName = @"C:\Program Files\SmartBear\soapUI-Pro-4.5.2\bin\testrunner.bat";
            process.StartInfo = startInfo;
            
            process.Start();
            process.WaitForExit();



            return "";
        }
    }

    public class SoapUIRequest
    {
        string appPath;
        string testSuite;
        string testCase;
        string testProjectPath;
        bool exportAllTestResults;
        string environment;

        public SoapUIRequest(string json)
        {

        }
    }
    
}
