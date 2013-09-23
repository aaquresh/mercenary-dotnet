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

        public void Run(string json)
        {

        }
    }
    
}
