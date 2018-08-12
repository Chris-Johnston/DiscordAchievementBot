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
        private IServiceProvider serviceProvider;

        public async Task Install(DiscordSocketClient client, IServiceProvider services)
        {
            m_client = client;
            serviceProvider = services;

            commands = new CommandService();
            commands.Log += Bot.LogAsync;
            
            await commands.AddModulesAsync(Assembly.GetEntryAssembly(), serviceProvider).ConfigureAwait(false);

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

            if (!(message.HasMentionPrefix(m_client.CurrentUser, ref argPos) || message.HasStringPrefix(GlobalConfiguration.CommandPrefix, ref argPos))) return;

            // Create a Command Context
            var context = new CommandContext(m_client, message);
            // Execute the Command, store the result
            var result = await commands.ExecuteAsync(context, argPos, serviceProvider).ConfigureAwait(false);

            // If the command failed
            if (!result.IsSuccess)
            {
                // result.Error will be unmetpreconditions if doesn't have permissions to attach files
                // could maybe prompt back about it

                // log the error
                Discord.LogMessage errorMessage = new Discord.LogMessage(Discord.LogSeverity.Warning, "CommandHandler", result.ErrorReason);
                Bot.Log(errorMessage);
                
                // don't actually reply back with the error
            }
        }
    }
}
