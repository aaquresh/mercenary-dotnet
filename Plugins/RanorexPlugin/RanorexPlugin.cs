using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interfaces;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace RanorexPlugin
{
    //public class RanorexPlugin : IAbstractTestToolFactory
    //{
    //    public IAbstractTestCaseProduct CreateTestCase(string json)
    //    {
    //        //Instantiates RunSoapUI object, passing in a json string
    //        JObject jsonObj = JObject.Parse(json);

    //        string testSuiteName = (string)jsonObj["parameters"]["test"]["testSuiteName"];
    //        string testSuitePath = (string)jsonObj["parameters"]["test"]["testSuitePath"];
    //        string testSuiteNamespace = (string)jsonObj["parameters"]["test"]["testSuiteNamespace"];






    //        IAbstractTestCaseProduct absTestCaseProduct = new RunSoapUI(json);
    //        return absTestCaseProduct;
    //    }
    //}

    //public class RunSoapUI : IAbstractTestCaseProduct
    //{
    //    public RunSoapUI(string json)
    //    {
    //    }
    //    public string Run()
    //    {
    //        return "";
    //    }
    //}


    public class RanorexPlugin : IAbstractTestToolFactory
    {

        public IAbstractTestCaseProduct CreateTestCase(string json)
        {
            JObject jsonObj = JObject.Parse(json);

            string testSuiteName = (string)jsonObj["parameters"]["test"]["testSuiteName"];
            string testSuitePath = (string)jsonObj["parameters"]["test"]["testSuitePath"];
            string testSuiteNamespace = (string)jsonObj["parameters"]["test"]["testSuiteNamespace"];

            Assembly assembly = Assembly.LoadFrom(testSuitePath);

            Type type = assembly.GetType(testSuiteNamespace + "." + testSuiteName);

            IAbstractTestCaseProduct instance = Activator.CreateInstance(type, json) as IAbstractTestCaseProduct;



            //Instantiates RunSoapUI object, passing in a json string

            return instance;
        }
    }
}
