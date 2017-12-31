using ImageMagick;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DiscordAchievementBot
{
    public class ImageGenerator
    {
        private Configuration m_config;

        public ImageGenerator(Configuration config)
        {
            m_config = config;
        }

        public string GenerateImagePath(ulong imageID)
        {
            // generate path in format
            // %temp%/123456789.png
            string path = string.Format("{0}achievement{1}.png", m_config.ImageTemporaryDirectory, imageID);
            // expand environment variables
            return Environment.ExpandEnvironmentVariables(path);
        }

        public void GenerateImage(string achievementName, int gs, AchievementType type, ulong imageID)
        {
            // first determine background image path
            string backgroundImagePath;

            switch(type)
            {
                default:
                case AchievementType.XboxOne:
                    backgroundImagePath = Configuration.Path_AchievementXboxOneBackground;
                    break;
                case AchievementType.XboxOneRare:
                    backgroundImagePath = Configuration.Path_AchievementXboxOneRareBackground;
                    break;
                case AchievementType.Xbox360:
                    backgroundImagePath = Configuration.Path_AchievementXbox360Background;
                    break;
            }

            MagickNET.SetTempDirectory("%TEMP%");

            // now do stuff with the image
            using (MagickImage image = new MagickImage(backgroundImagePath))
            {
                MagickImage headerLayer = new MagickImage(MagickColor.FromRgba(0, 0, 0, 0), image.Width, image.Height);

                if (type == AchievementType.XboxOne || type == AchievementType.XboxOneRare)
                {
                    headerLayer.Settings.FontFamily = "Segoe UI";
                    headerLayer.Settings.FontPointsize = 36;
                    headerLayer.Settings.TextGravity = Gravity.Southwest;
                    headerLayer.Settings.FillColor = MagickColor.FromRgb(255, 255, 255);
                }

                if (type == AchievementType.XboxOne || type == AchievementType.Xbox360)
                {
                    string s = string.Format("{0} - {1}", gs.ToString(), achievementName);
                    headerLayer.Annotate(s, new MagickGeometry(225, 30, 700, 80), Gravity.West);
                }
                else if (type == AchievementType.XboxOneRare)
                {
                    int rarePercent;
                    Random r = new Random();
                    rarePercent = r.Next(1, 5);
                    headerLayer.Annotate("Rare achievement unlocked - " + rarePercent + "%", new MagickGeometry(155,5,400,70), Gravity.West);
                    headerLayer.Annotate($"{gs} - {achievementName}", new MagickGeometry(195, 55, 400, 70), Gravity.West);
                }

                //placeholder debug stuff
                //image.Annotate(achievementName, Gravity.North);
                //image.Annotate(gs.ToString(), Gravity.West);
                //image.Annotate(type.ToString(), Gravity.East);

                image.Composite(headerLayer, CompositeOperator.Over);

                image.Write(GenerateImagePath(imageID));
            }
        }

        /// <summary>
        /// Deletes an image
        /// </summary>
        /// <param name="messageID"></param>
        public void DeleteImage(ulong messageID)
        {
            File.Delete(GenerateImagePath(messageID));
        }
    }
}
