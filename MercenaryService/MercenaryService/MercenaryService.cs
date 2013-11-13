using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using MercenaryEngine;

namespace MercenaryService
{
    public partial class MercenaryService : ServiceBase
    {
        private string brand = Properties.Resources.Branding.ToString();
        private Engine engine;

        public MercenaryService()
        {
            InitializeComponent();
            mEventLog.Log = brand;
        }

        protected override void OnStart(string[] args)
        {
            if (engine != null)
            {
                engine.Terminate();
            }

            engine = Engine.CreateInstance();
            engine.Initialize(this, mEventLog);
            engine.StartListening();
        }

        protected override void OnPause()
        {
            engine.StopListening();
        }

        protected override void OnContinue()
        {
            engine.StartListening();
        }

        protected override void OnStop()
        {
            engine.StopListening();
        }

        protected override void OnShutdown()
        {
            engine.StopListening();
            engine.Terminate();
            engine = null;
        }
    }
}
