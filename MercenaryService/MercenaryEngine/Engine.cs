using System;
using System.IO;
using System.Text;
using System.ServiceProcess;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MercenaryEngine
{
    public abstract class Engine : IEngine
    {
        #region Class (Protected) Variable Declarations

        protected ServiceBase service;
        protected EventLog log;
        protected HttpListener listener;

        protected EngineConfig config;

        protected string brand;
        protected string type;
        protected bool listen = true;
        protected volatile bool running = false;

        #endregion

        #region IEngine Implementation

        public void Initialize(ServiceBase sb, EventLog log)
        {
            this.config = EngineConfig.GetConfig();
            this.brand = config.Brand;
            string source = brand + " " + type;

            if (!EventLog.SourceExists(source))
            {
                EventLog.CreateEventSource(source, brand);
            }

            log.Source = source;
            log.WriteEntry("Started");

            this.service = sb;
            this.log = log;

            this.Initializer();
        }

        public void StartListening()
        {
            listener = new HttpListener();
            listener.Prefixes.Add("http://*:" + config.Port + "/");
            listener.Start();

            System.Threading.Tasks.Task.Factory.StartNew(() => Listen());

            this.log.WriteEntry("Listening on port " + config.Port);
        }

        public void StopListening()
        {
            listen = false;
            try
            {
                this.listener.Stop();
            }
            catch (Exception e)
            {
                this.log.WriteEntry("Error attempting to stop service: " + e.Message);
            }

            this.log.WriteEntry("Stopped Listening");
        }

        #endregion

        #region Async Http Listening

        private void Listen()
        {
            while (listen)
            {
                try
                {
                    HttpListenerContext context = listener.GetContext();
                    System.Threading.Tasks.Task.Factory.StartNew(() => RequestHandler(context));
                }
                catch
                {
                    this.StopListening();
                }
            }
        }

        private void RequestHandler(HttpListenerContext context)
        {
            Console.WriteLine("Handeling");
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            switch (request.HttpMethod.ToLower())
            {
                case "post":
                    HandlePost(context);
                    break;
                case "put":
                    HandlePut(context);
                    break;
                case "delete":
                    HandleDelete(context);
                    break;
                default:
                    HandleGet(context);
                break;
            }
        }

        #endregion

        #region Generic Engine Methods

        protected void FileRequest(HttpListenerContext context)
        {
            this.NotImplemented(context);
        }

        protected void PingResponse(HttpListenerContext context)
        {
            HttpListenerResponse response = context.Response;
            response.StatusCode = 200;
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes("present");
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }

        protected void ResponseHandler(HttpListenerResponse response, int status, string msg)
        {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(msg);

            response.StatusCode = status;
            response.ContentLength64 = buffer.Length;

            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }

        protected void NotImplemented (HttpListenerContext context)
        {
            this.ResponseHandler(context.Response, 501, context.Request.HttpMethod + " : " + context.Request.RawUrl + " : not implemented by " + this.type);
        }

        protected JObject GetJsonPayload(HttpListenerRequest request)
        {
            JObject json = null;
            string jsonstring = null;

            try
            {
                using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
                {
                    jsonstring = reader.ReadToEnd();
                }
                json = JObject.Parse(jsonstring);
            }
            catch (Exception e)
            {
                StringBuilder msg = new StringBuilder("Error parsing JSON from " + request.RemoteEndPoint);
                msg.Append(Environment.NewLine + "Via : (" + request.HttpMethod + ") : " + request.RawUrl);
                msg.Append(Environment.NewLine + Environment.NewLine + e.GetType() + " : " + e.Message);

                if (jsonstring != null)
                {
                    msg.Append(Environment.NewLine + Environment.NewLine + jsonstring);
                }

                this.log.WriteEntry(msg.ToString());
            }

            return json;
        }

        #endregion

        #region Abstracts
        protected abstract void HandleGet(HttpListenerContext context);
        protected abstract void HandlePost(HttpListenerContext context);
        protected abstract void HandlePut(HttpListenerContext context);
        protected abstract void HandleDelete(HttpListenerContext context);
        protected abstract void Initializer();
        public abstract void Terminate();
        #endregion

        public static Engine CreateInstance()
        {
            EngineConfig config = EngineConfig.GetConfig();
            Engine engine;

            switch (config.Role)
            {
                case "server":
                    engine = new ServerEngine();
                    break;
                default:
                    engine = new TargetEngine();
                    break;
            }

            return engine;
        }
    }
}
