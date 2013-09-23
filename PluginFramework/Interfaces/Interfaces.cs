using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{

    //Abstract Factory Interface
    public interface IAbstractTestToolFactory
    {
        IAbstractTestCaseProduct CreateTestCase(string Json);
    }

    //Abstract Product Interface
    public interface IAbstractTestCaseProduct
    {
        void Run(string Json);
    }
}