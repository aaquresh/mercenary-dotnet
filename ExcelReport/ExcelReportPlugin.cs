using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace ExcelReport
{
    public class ExcelReportPlugin
    {
    }

    public class ReportRequest
    {
        //fields
        TestCase[] testCases;
        //constructor
        public ReportRequest(string jsonRequest)
        {
            JObject jConfig = JObject.Parse(jsonRequest);

        }
        //properties
        public TestCase[] TestCases
        {
            get
            {
                return testCases;
            }
        }

    }

    public class TestCase
    {
    }
}
