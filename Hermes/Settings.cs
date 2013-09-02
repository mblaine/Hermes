using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Hermes
{
    public class Settings
    {
        private static bool read = false;

        private static String path = String.Format("{0}{1}settings.xml", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), Path.DirectorySeparatorChar);

        public static Dictionary<String, String> TempStore = new Dictionary<String, String>();

        private static Dictionary<String, String> store = new Dictionary<String, String>();
        public static Dictionary<String, String> Store
        {
            get
            {
                if (!read)
                    Read();
                return store;
            }
        }

        private static void Read()
        {
            if (File.Exists(path))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);

                XmlNodeList settings = doc.GetElementsByTagName("setting");
                for (int i = 0; i < settings.Count; i++)
                {
                    store[settings[i].Attributes["key"].Value] = settings[i].InnerText;   
                }
            }

            read = true;
        }

        public static void Save()
        {
            if (!Directory.Exists(Path.GetDirectoryName(path)))
                Directory.CreateDirectory(Path.GetDirectoryName(path));

            XmlDocument doc = new XmlDocument();
            doc.AppendChild(doc.CreateXmlDeclaration("1.0", null, null));
            
            XmlNode settings = doc.CreateElement("settings");
            foreach (KeyValuePair<String, String> pair in store)
            {
                XmlNode setting = doc.CreateElement("setting");
                XmlAttribute attr = doc.CreateAttribute("key");
                attr.Value = pair.Key;
                setting.Attributes.Append(attr);
                setting.InnerText = pair.Value;
                settings.AppendChild(setting);
            }

            doc.AppendChild(settings);
            doc.Save(path);
        }
    }
}
