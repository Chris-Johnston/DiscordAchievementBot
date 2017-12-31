﻿using Discord;
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
        /// <summary>
        /// Generate Commmand
        /// Replies to the user with the image they want to generate
        /// </summary>
        /// <param name="achievementName">The name of the achievement</param>
        /// <param name="gamerScore">The score for the achievement</param>
        /// <param name="type"></param>
        /// <returns></returns>
        [Command("Get", RunMode = RunMode.Async), Alias("Generate", "New")]
        [RequireUserPermission(Discord.GuildPermission.ManageMessages)]
        [RequireBotPermission(Discord.GuildPermission.SendMessages)]
        public async Task Get(string achievementName, int gamerScore = -1, AchievementType type = AchievementType.XboxOne)
        {
            Console.WriteLine($"Generating image for user {Context.User.Id} in guild {Context.Guild.Id}");

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
                    gamerScore = Program.GlobalConfig.GamerScoreGenerator.GetRareGamerScore();
                }
                else
                {
                    gamerScore = Program.GlobalConfig.GamerScoreGenerator.GetGamerScore();
                }
            }

            // tell the user that I'm working on it
            IUserMessage ack = await ReplyAsync("Right away, " + Context.User.Mention + "!");

            // actually go and generate this
            Program.GlobalConfig.ImageGenerator.GenerateImage(achievementName, gamerScore, type, Context.Message.Id);

            // remove the ack once file has been generated
            // gotta be a better way for this
            await ack.DeleteAsync();

            // tell the user if the pattern they chose is WIP
            string prefix = "";
            if(workInProgress)
            {
                prefix = "That one is a work in progress.\n";
            }

            // reply with the image and some text
            await Context.Channel.SendFileAsync(Program.GlobalConfig.ImageGenerator.GenerateImagePath(Context.Message.Id), prefix + "Generated by " + Context.User.Mention + ".");

            // wait a second
            await Task.Delay(1000);

            // delete that old image
            try
            {
                Program.GlobalConfig.ImageGenerator.DeleteImage(Context.Message.Id);
            }
            catch (Exception e)
            {
                // do something
                Console.WriteLine("couldn't delete " + e.ToString());
            }
        }

        ///// <summary>
        ///// Ping/Pong Command
        ///// Replies back instantly with "Pong!"
        ///// </summary>
        ///// <returns></returns>
        //[Command("Ping", RunMode = RunMode.Async)]
        //[RequireUserPermission(GuildPermission.ManageMessages)]
        //[RequireBotPermission(GuildPermission.SendMessages)]
        //[Remarks("A simple ping/pong test command.")]
        //public async Task Ping()
        //{
        //    await ReplyAsync("Pong!");
        //}

        /// <summary>
        /// Replies back with the Invite URL for the current user
        /// </summary>
        /// <returns></returns>
        [Command("InviteLink", RunMode = RunMode.Async)]
        [Alias("Invite")]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        [RequireBotPermission(GuildPermission.SendMessages)]
        public async Task GetInviteLink()
        {
            string link = string.Format(@"Add me to a server with the following URL: <https://discordapp.com/oauth2/authorize?client_id={0}&scope=bot>", Context.Client.CurrentUser.Id);
            await ReplyAsync(link);
        }

        /// <summary>
        /// Replies back with about text for the bot, links back to the GitHub page
        /// </summary>
        /// <returns></returns>
        [Command("About", RunMode = RunMode.Async)]
        [Alias("GitHub", "Source")]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        [RequireBotPermission(GuildPermission.SendMessages)]
        public async Task About()
        {
            string aboutText =
                string.Format("Discord Achievement Bot\nhttps://github.com/Chris-Johnston/DiscordAchievementBot");
            await ReplyAsync(aboutText);
        }

        /// <summary>
        /// Replies back with some help text
        /// </summary>
        /// <returns></returns>
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
Most commands require that you have the permission `Manage Messages`.

Example:
`++Generate ""Opened the README"" 999 XboxOne`
";
            await ReplyAsync(helpText);
        }
    }
}
