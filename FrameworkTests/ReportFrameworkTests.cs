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
        /// <summary>
        /// Test Case unit tests
        /// </summary>

        [TestMethod]
        [DeploymentItem(@"FrameworkTests\JsonTestFiles\ReportRequest_Default.json")]
        public void ReportRequest_TestCase_Populates()
        {
            string jsonRequest = ReadJsonFile("ReportRequest_Default.json");

            ReportRequest reportRequest = new ReportRequest(jsonRequest);

            Assert.IsTrue(reportRequest.TestCases.Count() > 0);
        }

        [TestMethod]
        [DeploymentItem(@"FrameworkTests\JsonTestFiles\ReportRequest_Default.json")]
        public void ReportRequest_TestCase_Missing()
        {
            string jsonRequest = ReadJsonFile("ReportRequest_Default.json");

            JObject jConfig = JObject.Parse(jsonRequest);

            JArray jRay = (JArray)jConfig["testcases"];

            int i = 0;
            while (i < jRay.Count)
            {
                jConfig["testcases"][i].Remove();
                i++;
            }
            ReportRequest reportRequest = new ReportRequest(jConfig.ToString());

            Assert.IsTrue(reportRequest.TestCases.Count() == 0);
            Assert.IsTrue(reportRequest.Error == "No test cases present in the json request file.");
        }

        /// <summary>
        /// Test Suite unit tests
        /// </summary>

        [TestMethod]
        [DeploymentItem(@"FrameworkTests\JsonTestFiles\ReportRequest_Default.json")]
        public void ReportRequest_TestSuite_Exists()
        {
            string jsonRequest = ReadJsonFile("ReportRequest_Default.json");

            ReportRequest reportRequest = new ReportRequest(jsonRequest);

            Assert.IsTrue(reportRequest.TestCases.Count() > 0);
            Assert.IsFalse(reportRequest.TestSuite == "");
            Assert.IsNotNull(reportRequest.TestSuite);
        }

        [TestMethod]
        [DeploymentItem(@"FrameworkTests\JsonTestFiles\ReportRequest_Default.json")]
        public void ReportRequest_TestSuite_Missing()
        {
            string jsonRequest = ReadJsonFile("ReportRequest_Default.json");

            JObject jConfig = JObject.Parse(jsonRequest);
            jConfig.Remove("testsuite");

            ReportRequest reportRequest = new ReportRequest(jConfig.ToString());

            Assert.IsTrue(reportRequest.TestSuite == null);
            Assert.IsTrue(reportRequest.TestCases == null);
            Assert.IsTrue(reportRequest.Error == "No test suite present in the json request file.");
        }

        [TestMethod]
        [DeploymentItem(@"FrameworkTests\JsonTestFiles\ReportRequest_Default.json")]
        public void ReportRequest_TestSuite_Blank()
        {
            string jsonRequest = ReadJsonFile("ReportRequest_Default.json");

            JObject jConfig = JObject.Parse(jsonRequest);
            jConfig["testsuite"] = "";

            ReportRequest reportRequest = new ReportRequest(jConfig.ToString());

            Assert.IsTrue(reportRequest.TestSuite == "");
            Assert.IsTrue(reportRequest.TestCases == null);
            Assert.IsTrue(reportRequest.Error == "Test Suite is not set to a value.");
        }

        /// <summary>
        /// Test SuiteID unit tests
        /// </summary>

        [TestMethod]
        [DeploymentItem(@"FrameworkTests\JsonTestFiles\ReportRequest_Default.json")]
        public void ReportRequest_TestSuiteID_Exists()
        {
            string jsonRequest = ReadJsonFile("ReportRequest_Default.json");

            ReportRequest reportRequest = new ReportRequest(jsonRequest);

            Assert.IsTrue(reportRequest.TestCases.Count() > 0);
            Assert.IsTrue(reportRequest.TestSuiteID > 0);
        }

        [TestMethod]
        [DeploymentItem(@"FrameworkTests\JsonTestFiles\ReportRequest_Default.json")]
        public void ReportRequest_TestSuiteID_Missing()
        {
            string jsonRequest = ReadJsonFile("ReportRequest_Default.json");

            JObject jConfig = JObject.Parse(jsonRequest);
            jConfig.Remove("testsuiteID");

            ReportRequest reportRequest = new ReportRequest(jConfig.ToString());

            Assert.IsTrue(reportRequest.TestSuiteID == 0);
            Assert.IsTrue(reportRequest.TestCases.Count() > 0);
        }

        [TestMethod]
        [DeploymentItem(@"FrameworkTests\JsonTestFiles\ReportRequest_Default.json")]
        public void ReportRequest_TestSuiteID_Blank()
        {
            string jsonRequest = ReadJsonFile("ReportRequest_Default.json");

            JObject jConfig = JObject.Parse(jsonRequest);
            jConfig["testsuiteID"] = "";

            ReportRequest reportRequest = new ReportRequest(jConfig.ToString());

            Assert.IsTrue(reportRequest.TestSuiteID == 0);
            Assert.IsTrue(reportRequest.TestCases.Count() > 0);
        }

        [TestMethod]
        [DeploymentItem(@"FrameworkTests\JsonTestFiles\ReportRequest_Default.json")]
        public void ReportRequest_TestSuiteID_NonInt()
        {
            string jsonRequest = ReadJsonFile("ReportRequest_Default.json");

            JObject jConfig = JObject.Parse(jsonRequest);
            jConfig["testsuiteID"] = "ABCDEF";

            ReportRequest reportRequest = new ReportRequest(jConfig.ToString());

            Assert.IsTrue(reportRequest.TestSuiteID == 0);
            Assert.IsTrue(reportRequest.TestCases == null);
            Assert.IsTrue(reportRequest.Error == "Test Suite ID is not set to an integer.");
        }


        /// <summary>
        /// Test TestSuiteStartDate unit tests
        /// </summary>

        [TestMethod]
        [DeploymentItem(@"FrameworkTests\JsonTestFiles\ReportRequest_Default.json")]
        public void ReportRequest_TestSuiteStartDate_Exists()
        {
            string jsonRequest = ReadJsonFile("ReportRequest_Default.json");

            JObject jConfig = JObject.Parse(jsonRequest);


            ReportRequest reportRequest = new ReportRequest(jConfig.ToString());

            Assert.IsTrue(reportRequest.TestCases.Count() > 0);

            DateTime dt = new DateTime();

            Assert.IsTrue(dt != reportRequest.TestSuiteStartDate);
        }

        [TestMethod]
        [DeploymentItem(@"FrameworkTests\JsonTestFiles\ReportRequest_Default.json")]
        public void ReportRequest_TestSuiteStartDate_Missing()
        {
            string jsonRequest = ReadJsonFile("ReportRequest_Default.json");

            JObject jConfig = JObject.Parse(jsonRequest);
            jConfig.Remove("testsuitestartdate");

            ReportRequest reportRequest = new ReportRequest(jConfig.ToString());

            DateTime dt = new DateTime();

            Assert.IsTrue(dt == reportRequest.TestSuiteStartDate);
            Assert.IsTrue(reportRequest.TestCases.Count() > 0);
        }

        [TestMethod]
        [DeploymentItem(@"FrameworkTests\JsonTestFiles\ReportRequest_Default.json")]
        public void ReportRequest_TestSuiteStartDate_Blank()
        {
            string jsonRequest = ReadJsonFile("ReportRequest_Default.json");

            JObject jConfig = JObject.Parse(jsonRequest);
            jConfig["testsuitestartdate"] = "";

            ReportRequest reportRequest = new ReportRequest(jConfig.ToString());

            DateTime dt = new DateTime();

            Assert.IsTrue(reportRequest.TestSuiteStartDate == dt);
            Assert.IsTrue(reportRequest.TestCases.Count() > 0);
        }

        [TestMethod]
        [DeploymentItem(@"FrameworkTests\JsonTestFiles\ReportRequest_Default.json")]
        public void ReportRequest_TestSuiteStartDate_NonDateTime()
        {
            string jsonRequest = ReadJsonFile("ReportRequest_Default.json");

            JObject jConfig = JObject.Parse(jsonRequest);
            jConfig["testsuitestartdate"] = "ABCDE";

            ReportRequest reportRequest = new ReportRequest(jConfig.ToString());

            DateTime dt = new DateTime();

            Assert.IsTrue(reportRequest.TestSuiteStartDate == dt);
            Assert.IsTrue(reportRequest.TestCases == null);
            Assert.IsTrue(reportRequest.Error == "Test Suite Start Date is not set to a DateTime.");
        }


        /// <summary>
        /// Test TestSuiteEndDate unit tests
        /// </summary>

        [TestMethod]
        [DeploymentItem(@"FrameworkTests\JsonTestFiles\ReportRequest_Default.json")]
        public void ReportRequest_TestSuiteEndDate_Exists()
        {
            string jsonRequest = ReadJsonFile("ReportRequest_Default.json");

            ReportRequest reportRequest = new ReportRequest(jsonRequest);

            Assert.IsTrue(reportRequest.TestCases.Count() > 0);

            DateTime dt = new DateTime();

            Assert.IsTrue(dt != reportRequest.TestSuiteEndDate);
        }

        [TestMethod]
        [DeploymentItem(@"FrameworkTests\JsonTestFiles\ReportRequest_Default.json")]
        public void ReportRequest_TestSuiteEndDate_Missing()
        {
            string jsonRequest = ReadJsonFile("ReportRequest_Default.json");

            JObject jConfig = JObject.Parse(jsonRequest);
            jConfig.Remove("testsuiteenddate");

            ReportRequest reportRequest = new ReportRequest(jConfig.ToString());

            DateTime dt = new DateTime();

            Assert.IsTrue(dt == reportRequest.TestSuiteEndDate);
            Assert.IsTrue(reportRequest.TestCases.Count() > 0);
        }

        [TestMethod]
        [DeploymentItem(@"FrameworkTests\JsonTestFiles\ReportRequest_Default.json")]
        public void ReportRequest_TestSuiteEndDate_Blank()
        {
            string jsonRequest = ReadJsonFile("ReportRequest_Default.json");

            JObject jConfig = JObject.Parse(jsonRequest);
            jConfig["testsuiteenddate"] = "";

            ReportRequest reportRequest = new ReportRequest(jConfig.ToString());

            DateTime dt = new DateTime();

            Assert.IsTrue(reportRequest.TestSuiteEndDate == dt);
            Assert.IsTrue(reportRequest.TestCases.Count() > 0);
        }

        [TestMethod]
        [DeploymentItem(@"FrameworkTests\JsonTestFiles\ReportRequest_Default.json")]
        public void ReportRequest_TestSuiteEndDate_NonDateTime()
        {
            string jsonRequest = ReadJsonFile("ReportRequest_Default.json");

            JObject jConfig = JObject.Parse(jsonRequest);
            jConfig["testsuiteenddate"] = "ABCDEF";

            ReportRequest reportRequest = new ReportRequest(jConfig.ToString());

            DateTime dt = new DateTime();

            Assert.IsTrue(reportRequest.TestSuiteEndDate == dt);
            Assert.IsTrue(reportRequest.TestCases == null);
            Assert.IsTrue(reportRequest.Error == "Test Suite End Date is not set to a DateTime.");
        }

        /// <summary>
        /// Test Parameters unit tests
        /// </summary>

        [TestMethod]
        [DeploymentItem(@"FrameworkTests\JsonTestFiles\ReportRequest_Default.json")]
        public void ReportRequest_TestParameters_Populates()
        {
            string jsonRequest = ReadJsonFile("ReportRequest_Default.json");

            ReportRequest reportRequest = new ReportRequest(jsonRequest);

            foreach (TestCase tc in reportRequest)
            {
                Assert.IsTrue(tc.TestParameters != null);
            }

        }

        [TestMethod]
        [DeploymentItem(@"FrameworkTests\JsonTestFiles\ReportRequest_Default.json")]
        public void ReportRequest_TestParameters_Missing()
        {
            string jsonRequest = ReadJsonFile("ReportRequest_Default.json");

            JObject jConfig = JObject.Parse(jsonRequest);

            JArray jRay = (JArray)jConfig["testcases"];

            int i = 0;
            while (i < jRay.Count)
            {
                JObject test = (JObject)jConfig["testcases"][i];

                test.Remove("parameters");
                i++;
            }

            ReportRequest reportRequest = new ReportRequest(jConfig.ToString());

            foreach (TestCase tc in reportRequest)
            {
                Assert.IsTrue(tc.TestParameters == null);
                Assert.IsTrue(tc.Error == "Test Case is missing Test Parameters, this test will not be included in the report.");
            }
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


        [TestMethod]
        [DeploymentItem(@"FrameworkTests\JsonTestFiles\ReportRequest_Default.json")]
        public void FrameworkTests_ReadJsonFile_ValidFile()
        {
            string jsonFile = ReadJsonFile("ReportRequest_Default.json");
            Assert.AreNotEqual(jsonFile, "");
        }

    }
}
