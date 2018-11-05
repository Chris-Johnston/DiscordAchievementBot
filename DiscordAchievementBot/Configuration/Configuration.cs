using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DiscordAchievementBot
{
    [XmlRoot("Configuration")]
    public class Configuration
    {
        /// <summary>
        /// Connection token for this bot user.
        /// </summary>
        [XmlElement("ConnectionToken")]
        public string ConnectionToken { get; set; }

        // paths to the background images for each of the achievement presets
        // in the Resources folder which should be configured to always copy to output directory
        public static string PathAchievementXboxOneBackground
            => Path.Combine("Resources", "xboxone.png");
        public static string PathAchievementXboxOneRareBackground
            => Path.Combine("Resources", "xboxonerare.png");
        public static string PathAchievementXbox360Background
            => Path.Combine("Resources", "test.png");

    }
}
