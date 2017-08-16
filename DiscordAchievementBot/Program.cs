using System;
using System.Reflection;

namespace DiscordAchievementBot
{
    class Program
    {
        private static string m_ConfigFile = null;
        public static GlobalConfiguration GlobalConfig = null;
        
        static void Main(string[] args)
        {
            Console.WriteLine("Discord Achievement Bot");
            Console.WriteLine("Version: " + Assembly.GetEntryAssembly().GetName().Version.ToString());

            foreach(string arg in args)
            {
                if(arg.StartsWith("-config="))
                {
                    m_ConfigFile = arg.Substring("-config=".Length);
                    Console.WriteLine("Using config file {0}", m_ConfigFile);
                }
            }

            if(m_ConfigFile == null)
            {
                Console.WriteLine("No config file.");
                throw new ArgumentNullException("-config=", "The config file parameter was not supplied, or was not found.");
            }

            GlobalConfig = new GlobalConfiguration(m_ConfigFile);
            
            try
            {
                // just generate some images
                ImageGeneration.GenerateImage("AchievementName", 1234, AchievementType.Xbox360, 123);
                ImageGeneration.GenerateImage("AchievementName", 5, AchievementType.XboxOneRare, 124);
                ImageGeneration.GenerateImage("AchievementName", 1337, AchievementType.XboxOne, 125);
                new Bot().Start().GetAwaiter().GetResult();
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: {0}", e);
            }
        }
    }
}