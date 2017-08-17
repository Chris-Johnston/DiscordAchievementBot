using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace DiscordAchievementBot
{
    public class GlobalConfiguration
    {
        private string m_Path = null;

        private GlobalConfiguration() { }

        public GlobalConfiguration(string path)
        {
            m_Path = path;
            Load();
            m_imageGen = new ImageGenerator(m_data);
        }

        // configuration
        private Configuration m_data = null;
        public Configuration Data { get { return m_data; } }

        // image generator
        private ImageGenerator m_imageGen = null;
        public ImageGenerator ImageGenerator { get { return m_imageGen; } }

        // gamer score generator
        private GamerScoreGenerator m_gamerScoreGen = new GamerScoreGenerator();
        public GamerScoreGenerator GamerScoreGenerator {  get { return m_gamerScoreGen; } }

        public void Load()
        {
            // make serializer
            XmlSerializer serializer = new XmlSerializer(typeof(Configuration));
            // make file stream
            FileStream fs = new FileStream(m_Path, FileMode.Open);
            //deserialize
            m_data = (Configuration)serializer.Deserialize(fs);

            fs.Dispose();
        }
    }
}
