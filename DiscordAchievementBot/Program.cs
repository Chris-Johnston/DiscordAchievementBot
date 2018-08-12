using Discord;
using System;
using System.Reflection;

namespace DiscordAchievementBot
{
    public static class Program
    {
        private static string m_ConfigFile = null;
        public static GlobalConfiguration GlobalConfig { get; private set; }
        
        static void Main(string[] args)
        {
            Bot.Log(new LogMessage(LogSeverity.Info, "Program", "Discord Achievement Bot"));
            Bot.Log(new LogMessage(LogSeverity.Debug, "Program", $"Version {Assembly.GetEntryAssembly().GetName().Version}"));
            
            foreach(string arg in args)
            {
                if(arg.StartsWith("-config=", StringComparison.CurrentCultureIgnoreCase))
                {
                    m_ConfigFile = arg.Substring("-config=".Length);
                    Bot.Log(new LogMessage(LogSeverity.Info, "Program", $"Using config file {m_ConfigFile}"));
                }
            }

            if(m_ConfigFile == null)
            {
                Bot.Log(new LogMessage(LogSeverity.Critical, "Program", $"No config file was supplied."));
                throw new InvalidOperationException("The config file parameter was not supplied, or was not found.");
            }

            GlobalConfig = new GlobalConfiguration(m_ConfigFile);
            
            try
            {
                // run the bot
                new Bot().Start().GetAwaiter().GetResult();
            }
            catch(Exception e)
            {
                Bot.Log(new LogMessage(LogSeverity.Error, "Bot", "Encountered an Exception.", e));
            }
        }
    }
}