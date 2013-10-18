using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PluginFramework;
using Newtonsoft.Json.Linq;
using System.IO;
using ReportPluginFramework;
using ExcelReport;

namespace FrameworkTests
{
    [TestClass]
    public class ReportFrameworkTests
    {
        [TestMethod]
        [DeploymentItem(@"FrameworkTests\JsonTestFiles\ReportRequest_TestCase_Populates.json")]
        public void ReportRequest_TestCase_Populates()
        {
            string jsonRequest = ReadJsonFile("ReportRequest_TestCase_Populates.json");

            ReportRequest reportRequest = new ReportRequest(jsonRequest);

            Assert.IsTrue(reportRequest.TestCases.Count() > 0);
        }






        [TestMethod]
        [DeploymentItem(@"FrameworkTests\JsonTestFiles\FrameworkTests_ReadJsonFile_ValidFile.json")]
        public void FrameworkTests_ReadJsonFile_ValidFile()
        {
            string jsonFile = ReadJsonFile("FrameworkTests_ReadJsonFile_ValidFile.json");
            Assert.AreNotEqual(jsonFile, "");
        }

        

        public string ReadJsonFile(string path)
        {
            StreamReader sr;

            try
            {
                sr = new StreamReader(path);
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return sr.ReadToEnd();
        }
    }
}
