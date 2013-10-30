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
            string testProject;
            string testProjectPath;
            JObject jTestFile;
            string soapUIPath;

            try
            {
                jTestFile = JObject.Parse(jsonTestFile);


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


                testSuite = "-s" + (string)jTestFile["parameters"]["test"]["suite"];
                testCase = "-c" + (string)jTestFile["parameters"]["test"]["case"];
                testProject = (string)jTestFile["parameters"]["test"]["project"];

                JObject jProject = JObject.Parse(config);

                testProjectPath = "-I " + (string)jProject["plugin"][0]["SoapUIProjects"][testProject];
                





            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            //Create cmd text

            /*
            •	C:\Program Files\SmartBear\soapUI-Pro-4.5.1\bin> testrunner.bat -s"CPOECommonHDDServiceSoap11Binding TestSuite" -c"getRepresentation TestCase_Grid" -a -EDefault -I C:\___TRANING_SOAPUI\DEMO-soapui-project.xml
            */

            string strCmdText = strCmdText = testSuite + " " + testCase + @" -a -EDefault " + testProjectPath + @" >test.log";


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


            //Add reading result file and getting test result

            StreamReader sreader = new StreamReader("test.log");
            while (sreader.Peek() >= 0)
            {
                string s = sreader.ReadLine();
                if (s.Contains("[FINISHED]"))
                {
                    //Add in code to edit json request and pass back pass/fail result and results byte[]
                    jTestFile["results"]["outcome"] = "Pass";
                    string byteArray = ConvertResultsToByteArray("test.log");
                    jTestFile["results"]["attachments"]["1"] = byteArray;

                    return jTestFile.ToString();
                }
                else if (s.Contains("[FAILED]"))
                {
                    //Add in code to edit json request and pass back pass/fail result and results byte[]
                    jTestFile["results"]["outcome"] = "Fail";
                    string byteArray = ConvertResultsToByteArray("test.log");
                    jTestFile["results"]["attachments"]["1"] = byteArray;
                    return jTestFile.ToString();
                }

            }



            return "";
        }

        private string ConvertResultsToByteArray(string file)
        {
            Stream s = File.OpenRead(file);
            byte[] b;
            string stream;

            s.Position = 0;
            using (StreamReader reader = new StreamReader(s, Encoding.UTF8))
            {
                stream = reader.ReadToEnd();
            }

            b = System.Text.Encoding.ASCII.GetBytes(stream);


            /*
            using (BinaryReader br = new BinaryReader(s))
            {
                b = br.ReadBytes((int)s.Length);
            }
            */

            //return Encoding.UTF8.GetString(b, 0, b.Length);
            return System.Convert.ToBase64String(b);
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
