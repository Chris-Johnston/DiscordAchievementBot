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

        /// <summary>
        /// The location where the images will be stored temporariliy on disk before being 
        /// deleted.
        /// Typically, this is set to %TEMP%.
        /// </summary>
        [XmlElement("ImageTemporaryDirectory")]
        public string ImageTemporaryDirectory { get; set; }

        // paths to the background images for each of the achievement presets
        // in the Resources folder which should be configured to always copy to output directory
        public string PathAchievementXboxOneBackground
            => Path.Combine("Resources", "xboxone.png");
        public string PathAchievementXboxOneRareBackground
            => Path.Combine("Resources", "xboxonerare.png");
        public string PathAchievementXbox360Background
            => Path.Combine("Resources", "test.png");

    }
}
