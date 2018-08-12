using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace DiscordAchievementBot
{
    public class GlobalConfiguration
    {
        public const string CommandPrefix = "++";
        private readonly string ConfigurationFilePath = null;

        private GlobalConfiguration() { }

        public GlobalConfiguration(string path)
        {
            ConfigurationFilePath = path;
            Load();
        }

        // configuration
        private Configuration m_data = null;
        public Configuration Data { get { return m_data; } }

        public void Load()
        {
            // make serializer
            XmlSerializer serializer = new XmlSerializer(typeof(Configuration));
            // make file stream
            FileStream fs = new FileStream(ConfigurationFilePath, FileMode.Open);
            //deserialize
            m_data = (Configuration)serializer.Deserialize(fs);

            fs.Dispose();
        }
    }
}
