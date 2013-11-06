using System;
using System.Collections;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace ExcelReport
{
    public class ExcelReportPlugin
    {
    }

    public class ReportRequest : IEnumerable
    {
        //fields
        TestCase[] testCases;
        string error;
        string testSuite;
        int testSuiteID;
        DateTime testSuiteStartDate;
        DateTime testSuiteEndDate;

        //constructor - will refactor latter
        public ReportRequest(string jsonRequest)
        {
            JObject jConfig = JObject.Parse(jsonRequest);

            //set Test Suite
            testSuite = (string)jConfig["testsuite"];
            if (testSuite == "")
            {
                error = "Test Suite is not set to a value.";
                return;
            }
            if (testSuite == null)
            {
                error = "No test suite present in the json request file.";
                return;
            }

            //set Test Suite ID
            string tempTestSuiteID = (string)jConfig["testsuiteID"];
            if (tempTestSuiteID == "")
                tempTestSuiteID = "0";
            try
            {
                testSuiteID = Convert.ToInt16(tempTestSuiteID);
            }
            catch 
            {
                error = "Test Suite ID is not set to an integer.";
                return;
            }

            //set Test Suite Start Date
            string tempTestSuiteStartDate = (string)jConfig["testsuitestartdate"];
            if (tempTestSuiteStartDate != null && tempTestSuiteStartDate != "")
            {
                try
                {
                    testSuiteStartDate = Convert.ToDateTime(tempTestSuiteStartDate);
                }
                catch
                {
                    error = "Test Suite Start Date is not set to a DateTime.";
                    return;
                }
            }

            //set Test Suite End Date
            string tempTestSuiteEndDate = (string)jConfig["testsuiteenddate"];
            if (tempTestSuiteEndDate != null && tempTestSuiteEndDate != "")
            {
                try
                {
                    testSuiteEndDate = Convert.ToDateTime(tempTestSuiteEndDate);
                }
                catch
                {
                    error = "Test Suite End Date is not set to a DateTime.";
                    return;
                }
            }

            //Add TestCase to Report Request Object
            JArray Jray = (JArray)jConfig["testcases"];

            int count = Jray.Count;
            int position = 0;

            testCases = new TestCase[count];

            //If there are no test cases in the json request.
            //Exit and set error to "No test cases present in the json request file."
            if (count == 0)
            {
                error = "No test cases present in the json request file.";
                return;
            }

            while (position < count)
            {

                JToken jt = Jray[position];


                TestCase testCase = new TestCase(Jray[position].ToString());
                testCases[position] = testCase;
                position++;
            }

        }


        //properties
        public TestCase[] TestCases
        {
            get
            {
                return testCases;
            }
        }
        public string Error
        {
            get
            {
                return error;
            }
        }
        public string TestSuite
        {
            get
            {
                return testSuite;
            }
        }
        public int TestSuiteID
        {
            get
            {
                return testSuiteID;
            }
        }
        public DateTime TestSuiteStartDate
        {
            get
            {
                return testSuiteStartDate;
            }
        }
        public DateTime TestSuiteEndDate
        {
            get
            {
                return testSuiteEndDate;
            }
        }
        public int NumberofTests
        {
            get
            {
                return TestCases.Count();
            }
        }

        //Enumerator Code
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public ReportRequestEnum GetEnumerator()
        {
            return new ReportRequestEnum(testCases);
        }
    }

    public class ReportRequestEnum : IEnumerator
    {
        public TestCase[] testCases;
        private int position = -1;

        public ReportRequestEnum(TestCase[] testCases)
        {
            this.testCases = testCases;
        }

        public bool MoveNext()
        {
            position++;
            return (position < testCases.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public TestCase Current
        {
            get
            {
                try
                {
                    return testCases[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }

    public class TestCase
    {
        //fields
        TestParameters testParameters;
        string error;
        //constructor
        public TestCase(string jsonTestCase)
        {
            JObject jConfig = JObject.Parse(jsonTestCase);

            JToken jsonParameters = jConfig["parameters"];

            //Instantiate Test Parameters object, check to see if it's null and throw error

            if (jsonParameters == null)
            {
                error = "Test Case is missing Test Parameters, this test will not be included in the report.";
                return;
            }

            JToken jsonTestParameters = jsonParameters["test"];
            testParameters = new TestParameters(jsonTestParameters.ToString());


            JToken jsonOptionsParameters = jsonParameters["options"];
            

        }
        //properties
        public TestParameters TestParameters
        {
            get
            {
                return testParameters;
            }
        }
        public string Error
        {
            get
            {
                return error;
            }
        }
    }


    public class TestParameters
    {
        //fields
        string[,] test;
        string[,] options;
        //constructor
        public TestParameters(string jsonRequest)
        {

        }
        //properties
        public string[,] Test
        {
            get
            {
                return test;
            }
        }
    }

    


}
