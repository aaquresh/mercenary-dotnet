using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Diagnostics;
using MercenaryEngine;

namespace MercenaryService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase sb = new ServiceBase();
            EventLog log = new EventLog();

            Engine engine = Engine.CreateInstance();
            engine.Initialize(sb, log);
            engine.StartListening();

            Console.Write("Press <enter> to continue...");
            Console.ReadLine();

            /*
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new MercenaryService()
			};
            ServiceBase.Run(ServicesToRun);
            */
        }
    }
}
