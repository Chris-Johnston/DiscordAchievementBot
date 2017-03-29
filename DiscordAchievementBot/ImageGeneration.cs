using ImageMagick;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordAchievementBot
{
    public class ImageGeneration
    {
        public static void GenerateImage(string achievementName, int gs, AchievementType type, string outputFilePath)
        {
            // first determine background image path
            string backgroundImagePath;

            switch(type)
            {
                default:
                case AchievementType.XboxOne:
                    backgroundImagePath = GlobalConfiguration.AchievementXboxOne;
                    break;
                case AchievementType.XboxOneRare:
                    backgroundImagePath = GlobalConfiguration.AchievementXboxOneRare;
                    break;
                case AchievementType.Xbox360:
                    backgroundImagePath = GlobalConfiguration.AchievementXbox360;
                    break;
            }

            // now do stuff with the image
            using (MagickImage image = new MagickImage(backgroundImagePath))
            {
                MagickImage headerLayer = new MagickImage(MagickColor.FromRgba(0, 0, 0, 0), image.Width, image.Height / 2);

                if (type == AchievementType.XboxOne || type == AchievementType.XboxOneRare)
                {
                    headerLayer.Settings.FontFamily = "Arial";
                    headerLayer.Settings.FontPointsize = 36;
                    headerLayer.Settings.TextGravity = Gravity.Southwest;
                    headerLayer.Settings.FillColor = MagickColor.FromRgb(255, 255, 255);
                }

                if (type == AchievementType.XboxOne || type == AchievementType.Xbox360)
                {
                    headerLayer.Annotate("Achievement Unlocked", Gravity.Southwest);
                }
                else if (type == AchievementType.XboxOneRare)
                {
                    int rarePercent;
                    Random r = new Random();
                    rarePercent = r.Next(1, 5);
                    headerLayer.Annotate("Rare achievement unlocked - " + rarePercent + "%", Gravity.Southwest);
                }


                //todo make this fancier later on
                image.Annotate(achievementName, Gravity.North);
                image.Annotate(gs.ToString(), Gravity.West);
                image.Annotate(type.ToString(), Gravity.East);

                image.Composite(headerLayer, CompositeOperator.Over);

                image.Write(outputFilePath);
            }
        }
    }
}
