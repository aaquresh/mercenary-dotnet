using System;
using System.ServiceProcess;
using System.Diagnostics;

namespace MercenaryEngine
{
    public interface IEngine
    {
        void Initialize(ServiceBase sb, EventLog log);
        void StartListening();
        void StopListening();
        void Terminate();
    }
}
