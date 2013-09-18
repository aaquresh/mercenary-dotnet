using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{

    //Abstract Factory Interface
    public interface IAbstractTestToolFactory
    {
        IAbstractTestCaseProduct CreateTestCase(string FactoryTestCase, string TestCaseID, string SessionID);
    }

    //Abstract Product Interface
    public interface IAbstractTestCaseProduct
    {
        void Run(string TestCaseID, string SessionID, string args);
    }
}