using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DiscordAchievementBot
{
    [XmlRoot("Configuration")]
    public class Configuration
    {
        /// <summary>
        /// Connection token for this bot user
        /// </summary>
        [XmlElementAttribute("ConnectionToken")]
        public string ConnectionToken;

        // image stuff
        [XmlElementAttribute("ImageTemporaryDirectory")]
        public string ImageTemporaryDirectory;

        // paths to the background images for each of the achievement presets
        // in the Resources folder which should be configured to always copy to output directory
        public const string Path_AchievementXboxOneBackground = @"Resources\xboxone.png";
        public const string Path_AchievementXboxOneRareBackground = @"Resources\xboxone.png";
        public const string Path_AchievementXbox360Background = @"Resources\test.png";

    }
}
