using ImageMagick;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace DiscordAchievementBot
{
    public class ImageGenerator
    {
        private readonly Configuration m_config;

        public int GenerationCounter { get; private set; } = 0;

        public ImageGenerator(Configuration config)
        {
            m_config = config;
        }

        public string GenerateImagePath(ulong imageID)
        {
            // generate path in format
            // %path%/123456789.png
            string path = Path.Combine(Path.GetTempPath(), $"achievement{imageID}.png");
            // expand environment variables
            return Environment.ExpandEnvironmentVariables(path);
        }

        public void GenerateImage(string achievementName, long gs, AchievementType type, ulong imageID)
        {
            // first determine background image path
            string backgroundImagePath;

            switch(type)
            {
                case AchievementType.XboxOneRare:
                    backgroundImagePath = Configuration.PathAchievementXboxOneRareBackground;
                    break;
                case AchievementType.Xbox360:
                    backgroundImagePath = Configuration.PathAchievementXbox360Background;
                    break;
                default:
                    backgroundImagePath = Configuration.PathAchievementXboxOneBackground;
                    break;
            }

            // passing the relative path was breaking it, so now just going to pass it the file stream instead
            string path = Path.Combine(Directory.GetCurrentDirectory(), backgroundImagePath);

            using (var backgroundStream = new FileStream(path, FileMode.Open))
            {
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
                        var s = $"{gs} - {achievementName}";
                        headerLayer.Annotate(s, new MagickGeometry(225, 30, 700, 80), Gravity.West);
                    }
                    else if (type == AchievementType.XboxOneRare)
                    {
                        int rarePercent;
                        Random r = new Random();
                        rarePercent = r.Next(1, 5);
                        headerLayer.Annotate($"Rare achievement unlocked - {rarePercent}%", new MagickGeometry(155, 5, 400, 70), Gravity.West);
                        headerLayer.Annotate($"{gs} - {achievementName}", new MagickGeometry(195, 55, 400, 70), Gravity.West);
                    }

                    image.Composite(headerLayer, CompositeOperator.Over);
                    image.Write(GenerateImagePath(imageID));
                }
            }
            // increment the generation counter
            GenerationCounter++;
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
