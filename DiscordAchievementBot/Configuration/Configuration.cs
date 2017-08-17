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
    }
}
