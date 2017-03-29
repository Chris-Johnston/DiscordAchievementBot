using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace DiscordAchievementBot
{
    public static class GlobalConfiguration
    {

        private static Configuration data;

        public static void Load(string configFilePath)
        {
            // make serializer
            XmlSerializer serializer = new XmlSerializer(typeof(Configuration));
            // make file stream
            FileStream fs = new FileStream(configFilePath, FileMode.Open);
            //deserialize
            data = (Configuration)serializer.Deserialize(fs);

            fs.Dispose();
        }

        public static string ConnectionToken { get { return data.ConnectionToken; } }
        public static string ImageTemporaryDirectory { get { return data.ImageTemporaryDirectory; } }
        public static string AchievementXboxOneRare { get { return data.AchievementXboxOneRare; } }
        public static string AchievementXboxOne { get { return data.AchievementXboxOne; } }
        public static string AchievementXbox360 { get { return data.AchievementXbox360; } }

    }
}
