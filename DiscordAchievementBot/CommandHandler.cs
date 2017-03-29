using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DiscordAchievementBot
{
    public class CommandHandler
    {
        private CommandService commands;
        private DiscordSocketClient client;
        private IDependencyMap map;

        public async Task Install(IDependencyMap _map)
        {
            client = _map.Get<DiscordSocketClient>();
            commands = new CommandService();
            commands.Log += Bot.Log;

            map = _map;

            await commands.AddModulesAsync(Assembly.GetEntryAssembly());

            client.MessageReceived += HandleCommand;
        }

        private async Task HandleCommand(SocketMessage parameterMessage)
        {
            // Don't handle the command if it is a system message
            var message = parameterMessage as SocketUserMessage;
            if (message == null) return;

            // Mark where the prefix ends and the command begins
            int argPos = 0;
            // Determine if the message has a valid prefix, adjust argPos 

            //todo update command handler stuff
            if (!(message.HasMentionPrefix(client.CurrentUser, ref argPos) || message.HasCharPrefix('+', ref argPos))) return;

            // Create a Command Context
            var context = new CommandContext(client, message);
            // Execute the Command, store the result
            var result = await commands.ExecuteAsync(context, argPos, map);

            // If the command failed, notify the user
            if (!result.IsSuccess)
            {
                Console.WriteLine("Command failed: {0}", result.ErrorReason);
                //don't think I should in this case
                //todo consider logging on HandleCommand failure.
            }
                //await message.Channel.SendMessageAsync($"**Error:** {result.ErrorReason}");
        }
    }
}
