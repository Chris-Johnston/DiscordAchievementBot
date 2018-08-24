using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DiscordAchievementBot
{
    [Group(""), Remarks("Commands for Achievement Generator.")]
    public class AchievementGeneratorCommands : ModuleBase
    {
        private readonly ImageGenerator imageGenerator;
        private readonly GamerScoreGenerator gamerScoreGenerator;

        public AchievementGeneratorCommands(ImageGenerator imageGenerator, GamerScoreGenerator gamerScoreGenerator)
        {
            this.imageGenerator = imageGenerator;
            this.gamerScoreGenerator = gamerScoreGenerator;
        }

        [Command("Get"), Alias("Generate", "New", "Gen")]
        [RequireBotPermission(GuildPermission.SendMessages | GuildPermission.AttachFiles)]
        [Priority(2)]
        public async Task GenerateHelp()
        {
            await ReplyAsync(message: "**Generate**\n" +
                "Example: `++Generate \"Read the Docs\" 100 XboxOne`\n" +
                "`++Generate <achievement text> [score] [type]`\n" +
                "Valid types are `XboxOne`, `XboxOneRare`, and `Xbox360`.").ConfigureAwait(false);
        }

        [Command("Get", RunMode = RunMode.Async), Alias("Generate", "New", "Gen")]
        [RequireBotPermission(GuildPermission.SendMessages | GuildPermission.AttachFiles)]
        [Priority(10)]
        public async Task GetNoParameters([Remainder] string name)
        {
            await Get(-1, AchievementType.XboxOne, name).ConfigureAwait(false);
        }

        [Command("Get", RunMode = RunMode.Async), Alias("Generate", "New", "Gen")]
        [RequireBotPermission(GuildPermission.SendMessages | GuildPermission.AttachFiles)]
        [Priority(11)]
        public async Task GetOnlyScore(long score, [Remainder] string name)
        {
            await Get(score, AchievementType.XboxOne, name).ConfigureAwait(false);
        }

        [Command("Get", RunMode = RunMode.Async), Alias("Generate", "New", "Gen")]
        [RequireBotPermission(GuildPermission.SendMessages | GuildPermission.AttachFiles)]
        [Priority(11)]
        public async Task GetAchievementType(AchievementType type, [Remainder] string name)
        {
            await Get(-1, type, name).ConfigureAwait(false);
        }

        [Command("Get", RunMode = RunMode.Async), Alias("Generate", "New", "Gen")]
        [RequireBotPermission(GuildPermission.SendMessages | GuildPermission.AttachFiles)]
        [Priority(50)]
        public async Task Get(AchievementType type, long gamerScore, [Remainder] string achievementName)
        {
            await Get(gamerScore, type, achievementName).ConfigureAwait(false);
        }

        /// <summary>
        /// Generate Commmand
        /// Replies to the user with the image they want to generate
        /// </summary>
        /// <param name="achievementName">The name of the achievement</param>
        /// <param name="gamerScore">The score for the achievement</param>
        /// <param name="type">The type of image to generate, converted with <see cref="AchievementTypeReader"/></param>
        [Command("Get", RunMode = RunMode.Async), Alias("Generate", "New", "Gen")]
        [RequireBotPermission(GuildPermission.SendMessages | GuildPermission.AttachFiles)]
        [Priority(50)]
        public async Task Get(long gamerScore, AchievementType type, [Remainder] string achievementName)
        {
            Bot.Log(new LogMessage(LogSeverity.Debug, "AchievementGeneratorCommands", $"Generating image for user {Context.User.Id} in {Context.Guild.Id}"));
            
            // for 'work in progress' patterns, set them to use the one that works
            bool workInProgress = type == AchievementType.Xbox360;
            if (workInProgress)
                type = AchievementType.XboxOne;

            // if gamerScore unset, pick something random
            if (gamerScore == -1)
            {
                // use a rare score
                if (type == AchievementType.XboxOneRare)
                {
                    gamerScore = gamerScoreGenerator.GetRareGamerScore;
                }
                else
                {
                    gamerScore = gamerScoreGenerator.GetGamerScore;
                }
            }

            // tell the user that I'm working on it
            IUserMessage ack = await ReplyAsync($"Right away, {Context.User.Mention}!").ConfigureAwait(true);

            await Task.Factory.StartNew(action: () =>
            {   
                // actually go and generate this
                imageGenerator.GenerateImage(achievementName, gamerScore, type, Context.Message.Id);
            }, 
            cancellationToken: CancellationToken.None,
            creationOptions: TaskCreationOptions.None,
            scheduler: TaskScheduler.Default)
            .ConfigureAwait(false);            

            // remove the ack once file has been generated
            // gotta be a better way for this
            await ack.DeleteAsync().ConfigureAwait(true);

            // tell the user if the pattern they chose is WIP
            string prefix = "";
            if(workInProgress)
            {
                prefix = "That one is a work in progress.\n";
            }

            // reply with the image and some text
            await Context.Channel.SendFileAsync(
                imageGenerator.GenerateImagePath(Context.Message.Id), 
                $"{prefix}Generated by {Context.User.Mention}.").ConfigureAwait(true);

            // wait a second
            await Task.Delay(1000).ConfigureAwait(true);

            // delete that old image from disk
            try
            {
                imageGenerator.DeleteImage(Context.Message.Id);
            }
            catch (Exception e)
            {
                // log errors when trying to delete the acknowledgement message
                Bot.Log(new LogMessage(LogSeverity.Warning, "AchievementGeneratorCommands", "Couldn't delete the message acknowledge message.", e));
            }
        }

        /// <summary>
        /// Replies back with the Invite URL for the current user
        /// </summary>
        [Command("InviteLink", RunMode = RunMode.Async)]
        [Alias("Invite")]
        [RequireBotPermission(GuildPermission.SendMessages)]
        public async Task GetInviteLink()
        {
            var link = $"Add me to a server with the following URL: https://discordapp.com/oauth2/authorize?client_id={Context.Client.CurrentUser.Id}&scope=bot";
            await ReplyAsync(link).ConfigureAwait(false);
        }

        /// <summary>
        /// Replies back with about text for the bot, links back to the GitHub page
        /// </summary>
        [Command("About", RunMode = RunMode.Async)]
        [Alias("GitHub", "Source")]
        [RequireBotPermission(GuildPermission.SendMessages)]
        public async Task About()
        {
            var aboutText = "Discord Achievement Bot\n" +
                "I'm Open Source Software! View source and contribute at: " +
                "https://github.com/Chris-Johnston/DiscordAchievementBot";
            await ReplyAsync(aboutText).ConfigureAwait(false);
        }

        /// <summary>
        /// Replies back with some help text
        /// </summary>
        [Command("Help", RunMode = RunMode.Async)]
        public async Task Help()
        {
            string helpText =
@"
```
++About
++InviteLink
++Generate <text> [score] [type]
```
An achievement with spaces in it must be surrounded with quotation marks. Valid types are `XboxOne`, `XboxOneRare`, and `Xbox360`.

Example:
`++Generate ""Opened the README"" 999 XboxOne`

Please post your feedback to <https://github.com/Chris-Johnston/DiscordAchievementBot>

In a server, I require the Send Messages and Attach Files permissions.
";
            await ReplyAsync(helpText).ConfigureAwait(false);
        }
    }
}
