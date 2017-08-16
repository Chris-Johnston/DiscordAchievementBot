using ImageMagick;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DiscordAchievementBot
{
    public class ImageGeneration
    {
        public static string GenerateImagePath(ulong imageID)
        {
            // generate path in format
            // %temp%/123456789.png
            string path = string.Format("{0}{1}.png", Program.GlobalConfig.Data.ImageTemporaryDirectory, imageID);
            // expand environment variables
            return Environment.ExpandEnvironmentVariables(path);
        }

        public static void GenerateImage(string achievementName, int gs, AchievementType type, ulong imageID)
        {
            // first determine background image path
            string backgroundImagePath;

            switch(type)
            {
                default:
                case AchievementType.XboxOne:
                    backgroundImagePath = Program.GlobalConfig.Data.AchievementXboxOne;
                    break;
                case AchievementType.XboxOneRare:
                    backgroundImagePath = Program.GlobalConfig.Data.AchievementXboxOneRare;
                    break;
                case AchievementType.Xbox360:
                    backgroundImagePath = Program.GlobalConfig.Data.AchievementXbox360;
                    break;
            }

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
                    //headerLayer.Annotate(s, new MagickGeometry(185,27,400,80), Gravity.West);
                    //headerLayer.Annotate(s, new MagickGeometry(225, 27, 400, 80), Gravity.West);
                    headerLayer.Annotate(s, new MagickGeometry(225, 30, 400, 80), Gravity.West);
                }
                else if (type == AchievementType.XboxOneRare)
                {
                    int rarePercent;
                    Random r = new Random();
                    rarePercent = r.Next(1, 5);
                    headerLayer.Annotate("Rare achievement unlocked - " + rarePercent + "%", Gravity.Southwest);
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
        public static void DeleteImage(ulong messageID)
        {
            File.Delete(GenerateImagePath(messageID));
        }
    }
}
