using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ServiceProcess;
using System.Diagnostics;
using System.Net;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace MercenaryEngine
{
    public class ServerEngine : Engine
    {
        protected ConcurrentDictionary<string, RemoteTarget> targets;
        protected ConcurrentDictionary<string, Task> tasks;

        public ServerEngine()
        {
            this.type = "Server"; 
            this.targets = new ConcurrentDictionary<string, RemoteTarget>();
            this.tasks = new ConcurrentDictionary<string, Task>();
        }

        #region Engine Implementation

        protected override void Initializer()
        {

        }

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
                case "/register":
                    this.RegisterTarget(context);
                    break;
                case "/tasks":
                    this.TaskRequest(context);
                    break;
                default:
                    HttpListenerRequest request = context.Request;
                    this.ResponseHandler(context.Response, 501, request.HttpMethod + " : " + request.RawUrl + " not implemented.");
                    break;
            }
        }

        protected override void HandlePut(HttpListenerContext context)
        {
            switch (context.Request.RawUrl)
            {
                case "/results":
                    this.TaskCompletion(context);
                    break;
                case "/retire":
                    this.RetireTarget(context);
                    break;
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
                    this.RetireAllTargets(context);
                    this.service.Stop();
                    break;
                default:
                    HttpListenerRequest request = context.Request;
                    this.ResponseHandler(context.Response, 501, request.HttpMethod + " : " + request.RawUrl + " not implemented.");
                    break;
            }
        }

        #endregion

        #region Target Management

        private void RegisterTarget(HttpListenerContext context)
        {
            JObject json = this.GetJsonPayload(context.Request);
            RemoteTarget target = new RemoteTarget(json);

            int status = 200;
            string msg = "ACK";

            if (target.Ping())
            {
                if (targets.ContainsKey(target.Id))
                {
                    RemoteTarget existing;
                    if (targets.TryGetValue(target.Id, out existing))
                    {
                        targets.TryUpdate(target.Id, target, existing);
                    }
                }
                else if (!targets.TryAdd(target.Id, target))
                {
                    status = 403; // Forbidden
                    msg = "NAK";
                }
            }
            else
            {
                status = 418; // I'm a teapot
                msg = "NAK";
            }

            this.ResponseHandler(context.Response, status, msg);
        }

        private void RetireTarget(HttpListenerContext context)
        {
            string id  = RemoteTarget.MakeId(this.GetJsonPayload(context.Request));
            int status = 200;
            string msg = "ACK";

            if (targets.ContainsKey(id))
            {
                RemoteTarget target;
                if (!targets.TryRemove(id, out target))
                {
                    status = 403; // Forbidden
                    msg = "NAK";
                }
            }

            this.ResponseHandler(context.Response, status, msg);
        }

        private void RetireAllTargets(HttpListenerContext context)
        {
            List<string> keys = new List<string>();

            foreach (string key in targets.Keys)
            {
                keys.Add(key);
            }

            foreach (string key in keys)
            {
                RemoteTarget target;
                if (targets.TryRemove(key, out target))
                {
                    target.Retire();
                }
            }
        }

        #endregion

        #region Task Management

        private void TaskRequest(HttpListenerContext context)
        {
            JObject json = this.GetJsonPayload(context.Request);
            JArray tasks = json["tasks"] as JArray;

            foreach (JToken jtask in tasks)
            {
                if (!AddToHopper(new Task(jtask)))
                {
                }
            }
        }

        private void TaskCompletion(HttpListenerContext context)
        {
            this.NotImplemented(context);
        }

        #endregion

        #region Server Status (HTML)

        private void StatusReport(HttpListenerContext context)
        {
            this.NotImplemented(context);
        }

        #endregion

        #region Hopper

        protected void StartHopper()
        {
            if (!running)
            {
                running = ((!targets.IsEmpty) && (!tasks.IsEmpty));

                while (running)
                {
                    if (targets.IsEmpty)
                    {
                        running = false;
                    }
                    else
                    {
                        if (tasks.IsEmpty)
                        {
                            running = false;
                        }
                        else
                        {
                            this.MatchTargetWithTask();
                        }
                    }
                }
            }
        }

        protected void MatchTargetWithTask()
        {
            foreach (string key in targets.Keys)
            {
                RemoteTarget target;
                if (targets.TryGetValue(key, out target))
                {
                    if (target.IsAvailable)
                    {
                        // iterate over tasks
                    }
                }
            }
        }

        protected bool AddToHopper(Task task)
        {
            return true;
        }

        #endregion
    }
}


//string responseString = "<HTML><BODY>POST : " + request.RawUrl + " not implemented</BODY></HTML>";
//byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
//response.ContentLength64 = buffer.Length;
//System.IO.Stream output = response.OutputStream;
//output.Write(buffer, 0, buffer.Length);
//output.Close();