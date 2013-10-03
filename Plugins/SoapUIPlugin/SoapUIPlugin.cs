using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interfaces;
using System.Configuration;
using System.IO;

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
        private string jsonConfigFile;

        public RunSoapUI(string json)
        {
            this.jsonConfigFile = json;
        }

        public string Run()
        {
            //Declare variables for json config file

            /*
            •	C:\Program Files\SmartBear\soapUI-Pro-4.5.1\bin> testrunner.bat -s"CPOECommonHDDServiceSoap11Binding TestSuite" -c"getRepresentation TestCase_Grid" -a -EDefault -I C:\___TRANING_SOAPUI\DEMO-soapui-project.xml
            */

            string SoapUIAppPath;
            string SoapUITestSuite;
            string SoapUITestCase;
            string SoapUITestProjectPath;
            bool ExportAllTestResults;
            string SoapUIEnvironment;


            try
            {
                //Pull required variables from json config files
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            //Create cmd text

            //Create process pass required args
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();

            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;

            


            return "";
        }
    }
    
}
