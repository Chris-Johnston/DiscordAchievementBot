using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordAchievementBot
{
    /// <summary>
    /// TypeReader converter that converts an input string to the enum AchievementType
    /// </summary>
    class AchievementTypeReader : TypeReader
    {
        public override Task<TypeReaderResult> Read(ICommandContext context, string input, IServiceProvider services)
        {
            AchievementType ret;
            if(Enum.TryParse(input, out ret))
            {
                return Task.FromResult(TypeReaderResult.FromSuccess(ret));
            }
            return Task.FromResult(TypeReaderResult.FromError(CommandError.ParseFailed, "Input pattern type didn't match."));
        }
    }
}
