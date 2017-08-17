using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace DiscordAchievementBot
{
    public class GlobalConfiguration
    {
        // paths to the background images for each of the achievement presets
        // in the Resources folder which should be configured to always copy to output directory
        public const string Path_AchievementXboxOneBackground = @"Resources\xboxone.png";
        public const string Path_AchievementXboxOneRareBackground = @"Resources\xboxone.png";
        public const string Path_AchievementXbox360Background = @"Resources\test.png";

        private string m_Path = null;

        private GlobalConfiguration() { }

        public GlobalConfiguration(string path)
        {
            m_Path = path;
            Load();
        }

        // configuration
        private Configuration m_data = null;
        public Configuration Data { get { return m_data; } }

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
