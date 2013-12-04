using System;
using System.ServiceProcess;
using System.Diagnostics;
using System.Net;
using Newtonsoft.Json.Linq;
using PluginFramework;

namespace MercenaryEngine
{
    public class TargetEngine : Engine
    {
        protected Task task;
        protected JObject json;

        public TargetEngine()
        {
            this.type = "Target";
        }

        protected override void Initializer()
        {
            // Check plugins
            // Register with server
        }

        #region Engine Implementation

        public override void Terminate()
        {

        }

        protected override void HandleGet(HttpListenerContext context)
        {
            switch (context.Request.RawUrl)
            {
                case "/":
                case "/status":
                    this.StatusReport(context);
                    break;
                case "/ping":
                    this.PingResponse(context);
                    break;
                default:
                    this.FileRequest(context);
                    break;
            }
        }

        protected override void HandlePost(HttpListenerContext context)
        {
            switch (context.Request.RawUrl)
            {
                case "/task":
                    if (this.task == null)
                    {
                        this.json = this.GetJsonPayload(context.Request);
                        this.task = new Task(json);

                        System.Threading.Tasks.Task.Factory.StartNew(() => this.TaskExecution());
                        this.ResponseHandler(context.Response, 200, "ACK");
                    }
                    else
                    {
                        HttpListenerRequest request = context.Request;
                        this.ResponseHandler(context.Response, 418, "NAK");
                    }
                    break;
                default:
                    this.ResponseHandler(context.Response, 501, context.Request.HttpMethod + " : " + context.Request.RawUrl + " not implemented.");
                    break;
            }
        }

        protected override void HandlePut(HttpListenerContext context)
        {
            switch (context.Request.RawUrl)
            {
                default:
                    HttpListenerRequest request = context.Request;
                    this.ResponseHandler(context.Response, 501, request.HttpMethod + " : " + request.RawUrl + " not implemented.");
                    break;
            }
        }

        protected override void HandleDelete(HttpListenerContext context)
        {
            switch (context.Request.RawUrl)
            {
                case "/retire":
                    this.Retire(context);
                    this.service.Stop();
                    break;
                default:
                    HttpListenerRequest request = context.Request;
                    this.ResponseHandler(context.Response, 501, request.HttpMethod + " : " + request.RawUrl + " not implemented.");
                    break;
            }
        }

        protected void Retire(HttpListenerContext context)
        {
            this.NotImplemented(context);
        }

        #endregion

        #region Task Execution

        private void TaskExecution()
        {
            json = JObject.Parse(PluginFramework.PluginFramework.RunTest(json.ToString()));
            Console.WriteLine(json.ToString());

            // Scott: Send response here

            this.task = null;
            this.json = null;
        }

        #endregion

        #region Status

        private void StatusReport(HttpListenerContext context)
        {
            this.NotImplemented(context);
        }

        #endregion
    }
}
