using System;

namespace DiscordAchievementBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Discord Achievement Bot");

            if(args.Length == 0)
            {
                Console.WriteLine("Please specity a path to your Configuration XML file.");
                return;
            }

            // load Configuration
            string path = args[0];
            GlobalConfiguration.Load(path);

            try
            {
                // just generate some images
                ImageGeneration.GenerateImage("Wow great job!", 1234, AchievementType.Xbox360, @"D:\Amazon Drive\Amazon Drive\Visual Studio Projects\AchievementBot\test1.png");
                ImageGeneration.GenerateImage("Nice.", 5, AchievementType.XboxOneRare, @"D:\Amazon Drive\Amazon Drive\Visual Studio Projects\AchievementBot\test2.png");
                ImageGeneration.GenerateImage("Welcome to EB Games", 1337, AchievementType.XboxOne, @"D:\Amazon Drive\Amazon Drive\Visual Studio Projects\AchievementBot\test3.png");

                //new Bot().Start().GetAwaiter().GetResult();
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: {0}", e);
            }
        }
    }
}