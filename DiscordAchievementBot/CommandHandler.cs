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
        private DiscordSocketClient m_client;

        public async Task Install(DiscordSocketClient _client)
        {
            m_client = _client;

            commands = new CommandService();
            commands.Log += Bot.Log;
            
            await commands.AddModulesAsync(Assembly.GetEntryAssembly());

            m_client.MessageReceived += HandleCommand;
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
            if (!(message.HasMentionPrefix(m_client.CurrentUser, ref argPos) || message.HasCharPrefix('+', ref argPos))) return;

            // Create a Command Context
            var context = new CommandContext(m_client, message);
            // Execute the Command, store the result
            var result = await commands.ExecuteAsync(context, argPos);

            // If the command failed
            if (!result.IsSuccess)
            {
                // log the error
                Discord.LogMessage errorMessage = new Discord.LogMessage(Discord.LogSeverity.Warning, "CommandHandler", result.ErrorReason);
                await Bot.Log(errorMessage);
                
                // don't actually reply back with the error
            }
        }
    }
}
