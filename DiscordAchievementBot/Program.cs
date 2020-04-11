using Discord;
using System;
using System.Reflection;

namespace DiscordAchievementBot
{
    public static class Program
    {
        private static string configFile = null;
        public static GlobalConfiguration GlobalConfig { get; private set; }
        
        static void Main(string[] args)
        {
            Bot.LogAsync(new LogMessage(LogSeverity.Info, "Program", "Discord Achievement Bot"));
            Bot.LogAsync(new LogMessage(LogSeverity.Debug, "Program", $"Version {Assembly.GetEntryAssembly().GetName().Version}"));
            
            foreach(string arg in args)
            {
                if(arg.StartsWith("-config=", StringComparison.CurrentCultureIgnoreCase))
                {
                    configFile = arg.Substring("-config=".Length);
                    Bot.LogAsync(new LogMessage(LogSeverity.Info, "Program", $"Using config file {configFile}"));
                }
            }

            if(configFile == null)
            {
                Bot.LogAsync(new LogMessage(LogSeverity.Critical, "Program", $"No config file was supplied."));
                throw new InvalidOperationException("The config file parameter was not supplied, or was not found.");
            }

            GlobalConfig = new GlobalConfiguration(configFile);
            
            try
            {
                new Bot().Start().GetAwaiter().GetResult();
            }
            catch(Exception e)
            {
                Bot.LogAsync(new LogMessage(LogSeverity.Error, "Bot", "Encountered an Exception.", e));
            }
        }
    }
}