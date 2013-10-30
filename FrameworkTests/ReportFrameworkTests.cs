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
            jConfig["testsuite"] = "";

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
            jConfig["testsuite"] = "ABCDEF";

            ReportRequest reportRequest = new ReportRequest(jsonRequest);

            Assert.IsTrue(reportRequest.TestSuiteID == 0);
            Assert.IsTrue(reportRequest.TestCases == null);
            Assert.IsTrue(reportRequest.Error == "Test Suite ID is not set to an integer.");
        }


        /// <summary>
        /// Test TestSuiteStartDate unit tests
        /// </summary>

        [TestMethod]
        [DeploymentItem(@"FrameworkTests\JsonTestFiles\ReportRequest_TestSuiteStartDate_Exists.json")]
        public void ReportRequest_TestSuiteStartDate_Exists()
        {
            string jsonRequest = ReadJsonFile("ReportRequest_TestSuiteStartDate_Exists.json");

            ReportRequest reportRequest = new ReportRequest(jsonRequest);

            Assert.IsTrue(reportRequest.TestCases.Count() > 0);

            DateTime dt = new DateTime();

            Assert.IsTrue(dt != reportRequest.TestSuiteStartDate);
        }

        [TestMethod]
        [DeploymentItem(@"FrameworkTests\JsonTestFiles\ReportRequest_TestSuiteStartDate_Missing.json")]
        public void ReportRequest_TestSuiteStartDate_Missing()
        {
            string jsonRequest = ReadJsonFile("ReportRequest_TestSuiteStartDate_Missing.json");

            ReportRequest reportRequest = new ReportRequest(jsonRequest);

            DateTime dt = new DateTime();

            Assert.IsTrue(dt == reportRequest.TestSuiteStartDate);
            Assert.IsTrue(reportRequest.TestCases.Count() > 0);
        }

        [TestMethod]
        [DeploymentItem(@"FrameworkTests\JsonTestFiles\ReportRequest_TestSuiteStartDate_Blank.json")]
        public void ReportRequest_TestSuiteStartDate_Blank()
        {
            string jsonRequest = ReadJsonFile("ReportRequest_TestSuiteStartDate_Blank.json");

            ReportRequest reportRequest = new ReportRequest(jsonRequest);

            DateTime dt = new DateTime();

            Assert.IsTrue(reportRequest.TestSuiteStartDate == dt);
            Assert.IsTrue(reportRequest.TestCases.Count() > 0);
        }

        [TestMethod]
        [DeploymentItem(@"FrameworkTests\JsonTestFiles\ReportRequest_TestSuiteStartDate_NonDateTime.json")]
        public void ReportRequest_TestSuiteStartDate_NonDateTime()
        {
            string jsonRequest = ReadJsonFile("ReportRequest_TestSuiteStartDate_NonDateTime.json");

            ReportRequest reportRequest = new ReportRequest(jsonRequest);

            DateTime dt = new DateTime();

            Assert.IsTrue(reportRequest.TestSuiteStartDate == dt);
            Assert.IsTrue(reportRequest.TestCases == null);
            Assert.IsTrue(reportRequest.Error == "Test Suite Start Date is not set to a DateTime.");
        }


        /// <summary>
        /// Test TestSuiteEndDate unit tests
        /// </summary>

        [TestMethod]
        [DeploymentItem(@"FrameworkTests\JsonTestFiles\ReportRequest_TestSuiteEndDate_Exists.json")]
        public void ReportRequest_TestSuiteEndDate_Exists()
        {
            string jsonRequest = ReadJsonFile("ReportRequest_TestSuiteEndDate_Exists.json");

            ReportRequest reportRequest = new ReportRequest(jsonRequest);

            Assert.IsTrue(reportRequest.TestCases.Count() > 0);

            DateTime dt = new DateTime();

            Assert.IsTrue(dt != reportRequest.TestSuiteEndDate);
        }

        [TestMethod]
        [DeploymentItem(@"FrameworkTests\JsonTestFiles\ReportRequest_TestSuiteEndDate_Missing.json")]
        public void ReportRequest_TestSuiteEndDate_Missing()
        {
            string jsonRequest = ReadJsonFile("ReportRequest_TestSuiteEndDate_Missing.json");

            ReportRequest reportRequest = new ReportRequest(jsonRequest);

            DateTime dt = new DateTime();

            Assert.IsTrue(dt == reportRequest.TestSuiteEndDate);
            Assert.IsTrue(reportRequest.TestCases.Count() > 0);
        }

        [TestMethod]
        [DeploymentItem(@"FrameworkTests\JsonTestFiles\ReportRequest_TestSuiteEndDate_Blank.json")]
        public void ReportRequest_TestSuiteEndDate_Blank()
        {
            string jsonRequest = ReadJsonFile("ReportRequest_TestSuiteEndDate_Blank.json");

            ReportRequest reportRequest = new ReportRequest(jsonRequest);

            DateTime dt = new DateTime();

            Assert.IsTrue(reportRequest.TestSuiteEndDate == dt);
            Assert.IsTrue(reportRequest.TestCases.Count() > 0);
        }

        [TestMethod]
        [DeploymentItem(@"FrameworkTests\JsonTestFiles\ReportRequest_TestSuiteEndDate_NonDateTime.json")]
        public void ReportRequest_TestSuiteEndDate_NonDateTime()
        {
            string jsonRequest = ReadJsonFile("ReportRequest_TestSuiteEndDate_NonDateTime.json");

            ReportRequest reportRequest = new ReportRequest(jsonRequest);

            DateTime dt = new DateTime();

            Assert.IsTrue(reportRequest.TestSuiteEndDate == dt);
            Assert.IsTrue(reportRequest.TestCases == null);
            Assert.IsTrue(reportRequest.Error == "Test Suite End Date is not set to a DateTime.");
        }

        /// <summary>
        /// Test NumberofTests unit tests
        /// </summary>

        [TestMethod]
        [DeploymentItem(@"FrameworkTests\JsonTestFiles\ReportRequest_NumberofTests_Populates1.json")]
        [DeploymentItem(@"FrameworkTests\JsonTestFiles\ReportRequest_NumberofTests_Populates5.json")]
        [DeploymentItem(@"FrameworkTests\JsonTestFiles\ReportRequest_NumberofTests_Populates10.json")]
        public void ReportRequest_NumberofTests_Populates()
        {
            string jsonRequest1 = ReadJsonFile("ReportRequest_NumberofTests_Populates1.json");
            string jsonRequest2 = ReadJsonFile("ReportRequest_NumberofTests_Populates5.json");
            string jsonRequest3 = ReadJsonFile("ReportRequest_NumberofTests_Populates10.json");

            ReportRequest reportRequest1 = new ReportRequest(jsonRequest1);
            ReportRequest reportRequest2 = new ReportRequest(jsonRequest2);
            ReportRequest reportRequest3 = new ReportRequest(jsonRequest3);

            Assert.IsTrue(reportRequest1.TestCases.Count() == reportRequest1.NumberofTests);
            Assert.IsTrue(reportRequest2.TestCases.Count() == reportRequest2.NumberofTests);
            Assert.IsTrue(reportRequest3.TestCases.Count() == reportRequest3.NumberofTests);

        }

        /// <summary>
        /// Test Parameters unit tests
        /// </summary>

        [TestMethod]
        [DeploymentItem(@"FrameworkTests\JsonTestFiles\ReportRequest_TestParameters_Populates.json")]
        public void ReportRequest_TestParameters_Populates()
        {
            string jsonRequest = ReadJsonFile("ReportRequest_TestParameters_Populates.json");

            ReportRequest reportRequest = new ReportRequest(jsonRequest);

            foreach (TestCase tc in reportRequest)
            {
                Assert.IsTrue(tc.TestParameters != null);
            }

        }

        [TestMethod]
        [DeploymentItem(@"FrameworkTests\JsonTestFiles\ReportRequest_TestParameters_Missing.json")]
        public void ReportRequest_TestParameters_Missing()
        {
            string jsonRequest = ReadJsonFile("ReportRequest_TestParameters_Missing.json");

            ReportRequest reportRequest = new ReportRequest(jsonRequest);

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
        [DeploymentItem(@"FrameworkTests\JsonTestFiles\FrameworkTests_ReadJsonFile_ValidFile.json")]
        public void FrameworkTests_ReadJsonFile_ValidFile()
        {
            string jsonFile = ReadJsonFile("FrameworkTests_ReadJsonFile_ValidFile.json");
            Assert.AreNotEqual(jsonFile, "");
        }

    }
}
