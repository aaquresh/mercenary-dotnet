using System;
using System.Collections;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace PluginFramework
{
    public class JsonPluginConfig : IEnumerable
    {
        JsonPlugin[] jpArray;

        public JsonPluginConfig(string json)
        {
            JObject jConfig = JObject.Parse(json);

            JArray Jray = (JArray)jConfig["plugin"];

            int count = Jray.Count;

            jpArray = new JsonPlugin[count];

            while (count > 0)
            {
                JsonPlugin jp = new JsonPlugin((string)jConfig["plugin"][count - 1]["name"], (string)jConfig["plugin"][count - 1]["dllLocation"], (string)jConfig["plugin"][count - 1]["namespace"]);
                jpArray[count - 1] = jp;
                count--;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public PluginEnum GetEnumerator()
        {
            return new PluginEnum(jpArray);
        }

    }

    public class PluginEnum : IEnumerator
    {
        public JsonPlugin[] jPlug;

        int position = -1;

        public PluginEnum(JsonPlugin[] list)
        {
            jPlug = list;
        }


        public bool MoveNext()
        {
            position++;
            return (position < jPlug.Length);
        }

        public void Reset()
        {
            position = -1;
        }


        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public JsonPlugin Current
        {
            get
            {
                try
                {
                    return jPlug[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }


    }

    public class JsonPlugin
    {
        string name;
        string path;
        string namespacePath;

        public JsonPlugin(string name, string path, string namespacePath)
        {
            this.name = name;
            this.path = path;
            this.namespacePath = namespacePath;
        }

        public string Name 
        {
            get
            {
                return name;
            }
        }
        public string Path
        {
            get
            {
                return path;
            }
        }
        public string NamespacePath
        {
            get
            {
                return namespacePath;
            }
        }

    }
}
