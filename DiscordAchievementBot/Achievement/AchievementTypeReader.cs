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
    public class AchievementTypeReader : TypeReader
    {
        public override Task<TypeReaderResult> ReadAsync(ICommandContext context, string input, IServiceProvider services)
        {
            if(Enum.TryParse(input, out AchievementType ret))
            {
                return Task.FromResult(TypeReaderResult.FromSuccess(ret));
            }
            return Task.FromResult(TypeReaderResult.FromError(CommandError.ParseFailed, "Input pattern type didn't match."));
        }
    }
}
