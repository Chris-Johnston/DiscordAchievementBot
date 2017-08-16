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

        // xb1 rare achievement image path
        [XmlElementAttribute("AchievementXboxOneRare")]
        public string AchievementXboxOneRare;
        // xb1 regular achievement image path
        [XmlElementAttribute("AchievementXboxOne")]
        public string AchievementXboxOne;
        // xb360 achievement image path
        [XmlElementAttribute("AchievementXbox360")]
        public string AchievementXbox360;
    }
}
