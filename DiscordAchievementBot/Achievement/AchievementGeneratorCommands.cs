using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordAchievementBot
{
    [Group(""), Remarks("Commands for Achievement Generator.")]
    public class AchievementGeneratorCommands : ModuleBase
    {
        //todo add proper help
        [Command("Get", RunMode = RunMode.Async), Alias("Generate")]
        public async Task Get(string achievementName, int gamerScore, AchievementType type = AchievementType.XboxOne)
        {
            Console.WriteLine("Get");
            //todo add permissions for this. possibly limit only to users who have a role on the server
            string filePath = GlobalConfiguration.ImageTemporaryDirectory + "aaa" + ".png";
            filePath = Environment.ExpandEnvironmentVariables(filePath);

            ImageGeneration.GenerateImage(achievementName, gamerScore, type, filePath);
        }
    }
}
