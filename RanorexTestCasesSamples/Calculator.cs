using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Interfaces;
using MyTestProject;

namespace RanorexTestCaseSamples
{
    
    static class CleanUp
    {
        static public void Run()
        {
            Process[] workers = Process.GetProcessesByName("calc");
            foreach (Process worker in workers)
            {
                worker.Kill();
                worker.WaitForExit();
                worker.Dispose();
            }
        }
    }

    static class StartUp
    {
        static public void Run()
        {
            Process.Start("calc");
        }
    }

    class Test1 : IAbstractTestCaseProduct
    {
        string json;

        public Test1(string json)
        {
            this.json = json;
        }


        public string Run()
        {
            StartUp.Run();

            
            NewRepository repo = NewRepository.Instance;

            repo.Calculator.NumPad.Button1.Click();
            repo.Calculator.NumPad.Button2.Click();
            repo.Calculator.Operations.ButtonMultiply.Click();
            repo.Calculator.NumPad.Button1.Click();
            repo.Calculator.NumPad.Button3.Click();
            repo.Calculator.Operations.ButtonEquals.Click();

            


            CleanUp.Run();
            return json;
        }
    }
}

